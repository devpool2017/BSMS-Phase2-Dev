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
            var MonthList = data.MonthList;
            var currentUser = data.currentUser;
            var currentDateDefault = data.currentDateDefault;
            var isAdmn = data.isAdmin;
            var isBH = data.isBH;
            var isHead = data.isHead;

            if (UsersList.length > 0) UsersList.splice(0, 0, { description: '', value: '' });
            if (BranchesList.length > 0) BranchesList.splice(0, 0, { description: '', value: '' });
            if (YearList.length > 0) YearList.splice(0, 0, { description: '', value: '' });
            if (MonthList.length > 0) MonthList.splice(0, 0, { description: '', value: '' });

            showDiv(['div-region'], !(isBH));
            showDiv(['btn-AddCommentForAdmin'], !(isBH));
            showDiv(['btn-AddComment'], !(isAdmn));

            document.getElementById('lbl-ACA').innerHTML = (isAdmn ? 'Add Comment' : 'Add Comment for Sector Head');
            document.getElementById('lbl-AC').innerHTML = (isBH ? 'Add Comment' : 'Add Comment for Branch Head');

            loadDropdown('ddl-region', RegionList, (isHead ? false : true));
            loadDropdown('ddl-userFullName', UsersList, false);
            loadDropdown('ddl-branch', BranchesList, (isBH ? false : true));
            loadDropdown('ddl-year', YearList, YearList.length > 0);
            loadDropdown('ddl-Month', MonthList, MonthList.length > 0);

            setDropdownDefaultValue('ddl-region', (isAdmn ? '01' : currentUser.GroupCode));
            isAdmn ? loadBranch() : '';
            setDropdownDefaultValue('ddl-branch', (isBH ? currentUser.BranchCode : (isHead ? BranchesList[1].value : '')));
            isHead ? loadUsers() : setDropdownDefaultValue('ddl-userFullName', (isBH ? currentUser.Username : ''));
            var currentDate = new Date();
            var isNoDate = (currentDateDefault == null ? true : false);
            setDropdownDefaultValue('ddl-year', (isNoDate ? currentDate.getFullYear() : currentDateDefault.Year));
            isNoDate ? setDropdownDefaultValue('ddl-Month', currentDate.getMonth() + 1) : setDropdownDefaultText('ddl-Month', currentDateDefault.Month);

            toggleEventForElement('#ddl-branch', 'change', async function (event) {
                loadUsers();
            }, true);
            toggleEventForElement('#ddl-region', 'change', AJAXWrapPageMethodCall, isAdmn, 'loadBranch');
            toggleEventForElement('#btn-ResetFilters', 'click', function () {
                setDropdownDefaultValue('ddl-region', (isAdmn ? '01' : currentUser.GroupCode));
                setDropdownDefaultValue('ddl-userFullName', (isBH ? currentUser.Username : ''));
                setDropdownDefaultValue('ddl-branch', (isBH ? currentUser.BranchCode : ''));
                setDropdownDefaultValue('ddl-year', '');
                setDropdownDefaultValue('ddl-Month', '');
                onLoadDetails();
            }, true);
            toggleEventForElement('#ddl-year', 'change', AJAXWrapPageMethodCall, true, 'onLoadDetails');
            toggleEventForElement('#ddl-Month', 'change', AJAXWrapPageMethodCall, true, 'onLoadDetails');
            toggleEventForElement('#btn-ViewTables', 'click', AJAXWrapPageMethodCall, true, 'validateFilters');
            toggleEventForElement('#btn-AddComment', 'click', AJAXWrapPageMethodCall, true, 'AddComment');
            toggleEventForElement('#btn-AddCommentForAdmin', 'click', AJAXWrapPageMethodCall, true, 'AddCommentForAdmin');
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

async function validateFilters() {
    hideError();
    loadingScreen(false, 0);
    var obj = await getFiltersObj();
    PageMethods.ValidateSearchFilter(obj,
        function OnSuccess(result) {
            if (result == '') {
                searchTables(obj);
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

async function searchTables(postData) {
    loadingScreen(false, 0);
    hideError();
    try {
        setLabelText("lblReportHeader", (postData.BranchManagerName != '' ? "Monthly Summary Report of Branch Head " + postData.BranchManagerName : ''));
        var data = await SearchMonthlyReport(postData);
        createMonthlyReportTable(data);
        setTotalMonthlyReport(postData);

        var data2 = await setRHMonthlyRemarks();
        setLabelText("lblMonthlyRemarks", data2);

        showMonthlyDetails(true);
    }
    catch (e) {
        showError(e);
        loadingScreen(true, 0);
    }
}

async function onLoadDetails() {
    loadingScreen(false, 0);
    hideError();
    showMonthlyDetails(false);
    loadingScreen(true, 0);
}

function showMonthlyDetails(toShow) {
    showDiv(['divReport'], false);
    showDiv(['tblOthers', 'divMonthSummaryReport'], toShow);
    toggleEventForElement('#btn-PrintReport', 'click', AJAXWrapPageMethodCall, true, 'generateReport');
}

function loadBranch() {
    loadingScreen(false, 0);
    hideError();
    showMonthlyDetails(false);
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
    showMonthlyDetails(false);
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

function createMonthlyReportTable(data) {
    var index = 1;
    var itemsPerPage = 10;
    var list = [];
    $('#tBody-monthlyReport').empty();
    for (var r = 0; r < data.length; r++) {
        list.push(data[r]);
    }
    secureStorage.setItem('listMonthlyReport', JSON.stringify(list));
    allItems = data.length;
    createTable('div-monthlyReport', 'tbl-monthlyReport', 'tBody-monthlyReport', list, index, itemsPerPage, allItems, 'listMonthlyReport', 'ListMonthlyReport');
}

function SearchMonthlyReport(obj) {
    var deferred = $.Deferred();
    PageMethods.SearchMonthlyReport(obj,
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

function setTotalMonthlyReport(obj) {

    PageMethods.GetTotalMonthlyReport(obj,
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

function setRHMonthlyRemarks() {
    var deferred = $.Deferred();
    PageMethods.GetRHMonthlyRemarks(
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

async function AddComment() {
    loadingScreen(false, 0);
    PageMethods.GetAddMonthlyRemarks(getTextboxValue('txtRHNewMonthlyComment'), false,
        async function (result) {
            if (result.isSuccess) {
                var data2 = await setRHMonthlyRemarks();
                setLabelText("lblMonthlyRemarks", data2);
                setTextboxValue('txtRHNewMonthlyComment', '');
            }
            else {
                showError(result.errMsg);
            }
            loadingScreen(true, 0);
        },
        function (err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}

async function AddCommentForAdmin() {
    loadingScreen(false, 0);
    PageMethods.GetAddMonthlyRemarks(getTextboxValue('txtRHNewMonthlyComment'), true,
        async function (result) {
            if (result.isSuccess) {
                var data2 = await setRHMonthlyRemarks();
                setLabelText("lblMonthlyRemarks", data2);
                setTextboxValue('txtRHNewMonthlyComment', '');
            }
            else {
                showError(result.errMsg);
            }
            loadingScreen(true, 0);
        },
        function (err) {
            showError(err.get_message());
            loadingScreen(true, 0);
        });
}

function generateReport() {
    hideError();
    loadingScreen(false, 0);
    var ddlBranch = document.getElementById("ddl-branch");
    var branchName = (ddlBranch && ddlBranch.value !== '') ? (typeof getDropdownText("ddl-branch") !== 'undefined') ? getDropdownText("ddl-branch") : "" : "";
    PageMethods.GenerateReport(branchName,
        function OnSuccess(result) {
            if (result == '') {
                $('iframe[name="ifrmReportViewer"]').attr('src', '../../../../../Views/Reports/ReportViewer.aspx');
                loadingScreen(true, 3000);
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
function resizeIframe(obj) {
    obj.style.height = obj.contentWindow.document.documentElement.scrollHeight + 'px';
}


async function getFiltersObj() {
    var ddlUserName = document.getElementById("ddl-userFullName");
    var ddlBranch = document.getElementById("ddl-branch");

    var postData = {
        GroupCode: getDropdownValue('ddl-region') || '',
        Username: (ddlUserName && ddlUserName.value !== '') ? (typeof getDropdownValue("ddl-userFullName") !== 'undefined') ? getDropdownValue("ddl-userFullName") : "" : "",
        BranchCode: (ddlBranch && ddlBranch.value !== '') ? (typeof getDropdownValue("ddl-branch") !== 'undefined') ? getDropdownValue("ddl-branch") : "" : "",
        Year: getDropdownValue('ddl-year') || '',
        MonthCode: getDropdownValue('ddl-Month') || '',
        Month: getDropdownText('ddl-Month') || '',
        BranchManagerName: (ddlUserName && ddlUserName.value !== '') ? (typeof getDropdownText("ddl-userFullName") !== 'undefined') ? getDropdownText("ddl-userFullName") : "" : ""
    };

    return postData;

}