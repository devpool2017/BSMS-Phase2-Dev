$(async function () {
    hideError();
    loadingScreen(false, 0);
    showDiv(['divReport'], false);
    $('iframe[name="ifrmReportViewer"]').on('load', function () {
        resizeIframe(this);
        loadingScreen(true, 0);
    });

    var deferred = $.Deferred();

    PageMethods.OnLoadData(
        function OnSuccess(result) {
            deferred.resolve(result);
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });

    $.when(deferred).done(async function (data) {
        try {
            var RegionList = data.RegionList;
            var UsersList = data.UsersList;
            var BranchesList = data.BranchesList;
            var YearList = data.YearList;
            var currentUser = data.currentUser;
            var currentDateDefault = data.currentDateDefault;
            var isAdmn = data.isAdmin;
            var isBH = data.isBH;
            var isHead = data.isHead;

            if (UsersList.length > 0) UsersList.splice(0, 0, { description: '', value: '' });
            if (BranchesList.length > 0) BranchesList.splice(0, 0, { description: '', value: '' });
            if (YearList.length > 0) YearList.splice(0, 0, { description: 'Please Select', value: '' });

            showDiv(['div-region'], !(isBH));
            showDiv(['btn-AddCommentForAdmin'], !(isBH));
            showDiv(['btn-AddComment'], !(isAdmn));

            loadDropdown('ddl-region', RegionList, (isHead ? false : true));
            loadDropdown('ddl-userFullName', UsersList, false);
            loadDropdown('ddl-branch', BranchesList, (isBH ? false : true));
            loadDropdown('ddl-year', YearList, YearList.length > 0);

            setDropdownDefaultValue('ddl-region', (isAdmn ? '01' : currentUser.GroupCode));
            isAdmn ? loadBranch() : '';
            setDropdownDefaultValue('ddl-branch', (isBH ? currentUser.BranchCode : (isHead ? BranchesList[1].value : '')));
            isHead ? loadUsers() : setDropdownDefaultValue('ddl-userFullName', (isBH ? currentUser.Username : ''));
            setDropdownDefaultValue('ddl-year', currentDateDefault.Year);

            toggleEventForElement('#ddl-branch', 'change', async function (event) {
                loadUsers();
            }, true);

            toggleEventForElement('#ddl-region', 'change', AJAXWrapPageMethodCall, isAdmn, 'loadBranch');
            toggleEventForElement('#btn-ResetFilters', 'click', function () {
                setDropdownDefaultValue('ddl-region', (isAdmn ? '01' : currentUser.GroupCode));
                setDropdownDefaultValue('ddl-userFullName', (isBH ? currentUser.Username : ''));
                setDropdownDefaultValue('ddl-branch', (isBH ? currentUser.BranchCode : ''));
                setDropdownDefaultValue('ddl-year', '');
                onLoadDetails();
            }, true);
            toggleEventForElement('#ddl-year', 'change', 'click', function () {
                showAnnualDetails(false);
            }, true);
            toggleEventForElement('#btn-ViewTables', 'click', AJAXWrapPageMethodCall, true, 'searchTables');
            await onLoadDetails();
        } catch (e) {
            showErrorModal(e);
        }

        loadingScreen(true, 0);
    }).fail(function (e) {
        showErrorModal(e);
        loadingScreen(true, 0);
    });
});


async function searchTables() {
    loadingScreen(false, 0);
    hideError();
    var postData = await getFiltersObj();
    try {
        PageMethods.ValidateSearchFilter(postData,
            async function OnSuccess(result) {
                if (result == '') {
                    showAnnualDetails(true);
                    setLabelText("lblReportHeader", (postData.BranchManagerName != '' ? "Annual Summary Report of Branch Head " + postData.BranchManagerName : ''));
                    var data = await SearchAnnualReport(postData);
                    createAnnualReportTable(data);
                    setTotalAnnualReport(postData);
                }
                else {
                    showError(result);
                    loadingScreen(true, 0);
                }
            },
            function OnError(err) {
                showErrorModal(err.get_message());
                loadingScreen(true, 0);
            });
    }
    catch (e) {
        showError(e);
        loadingScreen(true, 0);
    }
}

async function onLoadDetails() {
    loadingScreen(false, 0);
    hideError();
    showAnnualDetails(false);
    loadingScreen(true, 0);
}

function loadBranch() {
    loadingScreen(false, 0);
    hideError();
    showAnnualDetails(false);
    region = getDropdownValue('ddl-region') || '';
    var branchDeferred = $.Deferred();
    PageMethods.GetBranchesList(region,
        function OnSuccess(result) {
            branchDeferred.resolve(result);
        },
        function OnError(err) {
            branchDeferred.reject(err.get_message());
        });

    $.when(branchDeferred).done(async function (data) {
        try {
            if (data.length > 0) data.splice(0, 0, { description: '', value: '' });
            loadDropdown('ddl-branch', data, data.length > 0);
            setDropdownDefaultValue('ddl-branch', data.length > 0 ? data[1].value : '');
        } catch (e) {
            showErrorModal(e);
        }

        loadingScreen(true, 0);
    })
        .fail(function (e) {
            showErrorModal(e);
            loadingScreen(true, 0);
        });
}

function loadUsers() {
    loadingScreen(false, 0);
    hideError();
    showAnnualDetails(false);
    var ddlbranch = document.getElementById("ddl-branch");
    branch = (ddlbranch && ddlbranch.value !== '') ? (typeof getDropdownValue("ddl-branch") !== 'undefined') ? getDropdownValue("ddl-branch") : "" : "";
    var branchDeferred = $.Deferred();
    PageMethods.GetUsersList(branch,
        function OnSuccess(result) {
            branchDeferred.resolve(result);
        },
        function OnError(err) {
            branchDeferred.reject(err.get_message());
        });

    $.when(branchDeferred).done(async function (data) {
        try {
            setDropdownDefaultValue('ddl-userFullName', '');
            if (data.length > 0) data.splice(0, 0, { description: '', value: '' });
            if (data.length > 0) {
                loadDropdown('ddl-userFullName', data, false);
                setDropdownDefaultValue('ddl-userFullName', (branch != '' ? data[1].value : ''));
            }
        } catch (e) {
            showErrorModal(e);
        }

        loadingScreen(true, 0);
    })
        .fail(function (e) {
            showErrorModal(e);
            loadingScreen(true, 0);
        });
}
function createAnnualReportTable(data) {
    var index = 1;
    var itemsPerPage = 100;
    var list = [];
    $('#tBody-AnnuallyReport').empty();
    for (var r = 0; r < data.length; r++) {
        list.push(data[r]);
    }
    secureStorage.setItem('listAnnuallyReport', JSON.stringify(list));
    allItems = data.length;
    createTable('div-AnnuallyReport', 'tbl-AnnuallyReport', 'tBody-AnnuallyReport', list, index, itemsPerPage, allItems, 'listAnnuallyReport', 'ListAnnuallyReport');
}

function showAnnualDetails(toShow) {
    showDiv(['divReport'], false);
    showDiv(['divAnnualSummaryReport'], toShow);
    showDiv(['tblOthers'], toShow);
    setLabelText("lblReportHeader", "");
    toggleEventForElement('#btn-PrintReport', 'click', AJAXWrapPageMethodCall, true, 'generateReport');
}

function SearchAnnualReport(obj) {
    var deferred = $.Deferred();
    PageMethods.SearchAnnuallyReport(obj,
        function OnSuccess(result) {
            if (result != null) {
                deferred.resolve(result);
            }
            else {
                deferred.reject('There was a system problem while processing your request.');
            }
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });

    return deferred.promise();
}

function setTotalAnnualReport(obj) {

    PageMethods.GetTotalAnnuallyReport(obj,
        function OnSuccess(data) {
            setLabelText("lblTotalLead", data.TotalLead);
            setLabelText("lblTotalSuspect", data.TotalSuspect);
            setLabelText("lblTotalProspect", data.TotalProspect);
            setLabelText("lblNewCASA", data.TotalNewCASA);
            setLabelText("lblNewLoans", data.TotalNewLoans);
            setLabelText("lblTotalLost", data.TotalLost);

            setLabelText("lblTargetLeads", data.TargetLeads);
            setLabelText("lblActualLeads", data.ActualLeadGenerated);
            setLabelText("lblTotalLeadsGeneratedVersusTarget", data.TotalLeadsGeneratedVersusTarget);
            setLabelText("lblLeadsToSuspect", data.LeadsToSuspect);
            setLabelText("lblSuspectToProspect", data.SuspectToProspect);
            setLabelText("lblProspectToCustomer", data.ProspectToCustomer);

            setLabelText("lblTargetNewAccountClosed", data.TargetNewAccountClosed);
            setLabelText("lblActualNewAccountClosed", data.ActualNewAccountClosed);
            setLabelText("lblActualVSTarget", data.ActualVersusTargetNewAccountClosed);
            setLabelText("lblGeneralClosingRatio", data.GeneralClosingRatio);
            setLabelText("lblCasaClosingRatio", data.CasaClosingRatio);
            setLabelText("lblLostSalesRatio", data.LostSalesRatio);
            setLabelText("lblTargetADB", data.TargetADB);

            setLabelText("lblActualADB", data.ActualADBGenerated);
            setLabelText("lblTotalADB", data.TotalADBGeneratedVersusTarget);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}

function generateReport() {
    hideError();
    loadingScreen(false, 0);
    PageMethods.GenerateReport(
        function OnSuccess(result) {
            if (result == '') {
                $('iframe[name="ifrmReportViewer"]').attr('src', '../../../../../Views/Reports/ReportViewer.aspx');
                loadingScreen(true, 7000);
                showDiv(['divReport'], true);
            }
            else {
                showError(result);
                loadingScreen(true, 0);
            }
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}

async function getFiltersObj() {
    var ddlUserName = document.getElementById("ddl-userFullName");
    var ddlBranch = document.getElementById("ddl-branch");

    var postData = {
        GroupCode: getDropdownValue('ddl-region') || '',
        Username: (ddlUserName && ddlUserName.value !== '') ? (typeof getDropdownValue("ddl-userFullName") !== 'undefined') ? getDropdownValue("ddl-userFullName") : "" : "",
        BranchCode: (ddlBranch && ddlBranch.value !== '') ? (typeof getDropdownValue("ddl-branch") !== 'undefined') ? getDropdownValue("ddl-branch") : "" : "",
        BranchName: (ddlBranch && ddlBranch.value !== '') ? (typeof getDropdownText("ddl-branch") !== 'undefined') ? getDropdownText("ddl-branch") : "" : "",
        Year: getDropdownValue('ddl-year') || '',
        BranchManagerName: (ddlUserName && ddlUserName.value !== '') ? (typeof getDropdownText("ddl-userFullName") !== 'undefined') ? getDropdownText("ddl-userFullName") : "" : ""
    };

    return postData;
}

function resizeIframe(obj) {
    obj.style.height = obj.contentWindow.document.documentElement.scrollHeight + 'px';
}