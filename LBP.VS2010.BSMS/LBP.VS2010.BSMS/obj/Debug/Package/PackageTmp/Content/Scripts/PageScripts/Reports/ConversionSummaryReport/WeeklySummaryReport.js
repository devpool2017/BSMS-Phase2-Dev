var WeekList;
$(async function () {
    hideError();
    loadingScreen(false, 0);
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
            var sessionDetails = data.currentDetails;
            var withDetails = (sessionDetails != null ? (sessionDetails.DateFrom != '' && sessionDetails.DateToday != '' ? true : false) : false);
            WeekList = data.WeekList;
            var isAdmn = data.isAdmin;
            var isBH = data.isBH;
            var isHead = data.isHead;
            if (UsersList.length > 0) UsersList.splice(0, 0, { description: '', value: '' });
            if (BranchesList.length > 0) BranchesList.splice(0, 0, { description: '', value: '' });
            if (YearList.length > 0) YearList.splice(0, 0, { description: '', value: '' });
            if (MonthList.length > 0) MonthList.splice(0, 0, { description: '', value: '' });
            if (WeekList.length > 0) WeekList.splice(0, 0, { description: '', value: '' });

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
            loadDropdown('ddl-week', WeekList, WeekList.length > 0);

            setDropdownDefaultValue('ddl-region', (withDetails ? sessionDetails.GroupCode : (isAdmn ? '01' : currentUser.GroupCode)));
            isAdmn ? loadBranch() : '';
            setDropdownDefaultValue('ddl-branch', (withDetails ? sessionDetails.BranchCode : (isBH ? currentUser.BranchCode : (isHead ? BranchesList[1].value : ''))));
            isHead ? loadUsers() : setDropdownDefaultValue('ddl-userFullName', (withDetails ? sessionDetails.Username : (isBH ? currentUser.Username : '')));
            setDropdownDefaultValue('ddl-year', (withDetails ? sessionDetails.Year : currentDateDefault.Year));
            withDetails ? setDropdownDefaultValue('ddl-Month', sessionDetails.MonthCode) : setDropdownDefaultText('ddl-Month', currentDateDefault.Month);
            setDropdownDefaultValue('ddl-week', (withDetails ? sessionDetails.WeekNumber : currentDateDefault.WeekNumber));
            toggleEventForElement('#ddl-region', 'change', AJAXWrapPageMethodCall, isAdmn, 'loadBranch');
            toggleEventForElement('#ddl-branch', 'change', AJAXWrapPageMethodCall, true, 'loadUsers');

            toggleEventForElement('#ddl-Month', 'change', AJAXWrapPageMethodCall, true, 'loadWeekList');
            if (getDropdownValue('ddl-Month') !== '') {
                toggleEventForElement('#ddl-year', 'change', AJAXWrapPageMethodCall, true, 'loadWeekList');
            }
            toggleEventForElement('#ddl-week', 'change', function () {
                disabledPrintButton(true);
            }, true);
            toggleEventForElement('#btn-ResetFilters', 'click', function () {
                setDropdownDefaultValue('ddl-userFullName', (isBH ? currentUser.Username : ''));
                setDropdownDefaultValue('ddl-region', (isAdmn ? '01' : currentUser.GroupCode));
                setDropdownDefaultValue('ddl-branch', (isBH ? currentUser.BranchCode : ''));
                setDropdownDefaultValue('ddl-year', '');
                setDropdownDefaultValue('ddl-Month', '');
                setDropdownDefaultValue('ddl-week', '');
                onLoadTables(null);
            }, true);

            toggleEventForElement('#btn-ViewTables', 'click', AJAXWrapPageMethodCall, true, 'validateFilters');
            toggleEventForElement('#btn-AddComment', 'click', AJAXWrapPageMethodCall, true, 'AddComment');
            toggleEventForElement('#btn-AddCommentForAdmin', 'click', AJAXWrapPageMethodCall, true, 'AddCommentForAdmin');
            await onLoadTables(sessionDetails);
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

async function getFiltersObj() {
    var ddlUserName = document.getElementById("ddl-userFullName");
    var ddlBranch = document.getElementById("ddl-branch");
    var ddlWeek = document.getElementById("ddl-week");

    var postData = {
        GroupCode: getDropdownValue('ddl-region') || '',
        Username: (ddlUserName && ddlUserName.value !== '') ? (typeof getDropdownValue("ddl-userFullName") !== 'undefined') ? getDropdownValue("ddl-userFullName") : "" : "",
        BranchCode: (ddlBranch && ddlBranch.value !== '') ? (typeof getDropdownValue("ddl-branch") !== 'undefined') ? getDropdownValue("ddl-branch") : "" : "",
        Year: getDropdownValue('ddl-year') || '',
        MonthCode: getDropdownValue('ddl-Month') || '',
        Month: getDropdownText('ddl-Month') || '',
        WeekNumber: (ddlWeek && ddlWeek.value !== '') ? (typeof getDropdownValue("ddl-week") !== 'undefined') ? getDropdownValue("ddl-week") : "" : "",
        BranchManagerName: (ddlUserName && ddlUserName.value !== '') ? (typeof getDropdownText("ddl-userFullName") !== 'undefined') ? getDropdownText("ddl-userFullName") : "" : ""
    };

    return postData;
}

async function searchTables(postData) {
    loadingScreen(false, 0);
    hideError();

    var validpostData = ((postData.Username == '' && postData.Year == '' && postData.Month == '' && postData.WeekNumber == '') ? false : true);
    showDiv(['tblOthers'], validpostData);
    try {
        PageMethods.GetDate(postData,
            async function (result) {
                if (result != null) {
                    await loadAllData(result);
                    loadingScreen(true, 0);
                }
                else {
                    showError(result);
                    loadingScreen(true, 0);
                }
            },
            function (err) {
                showErrorModal(err.get_message());
                loadingScreen(true, 0);
            });
    }
    catch (e) {
        showError(e);
        loadingScreen(true, 0);
    }
}

async function onLoadTables(obj) {
    loadingScreen(false, 0);
    hideError();
    var obj1 = {
        Username: '',
        DateFrom: '',
        DateToday: '',
        BranchCode: '',
        BranchManagerName: ''
    };
    if (obj == null) {
        obj = obj1
    }
    await loadAllData(obj);
}

async function loadAllData(obj) {
    var header = obj.BranchManagerName != '' ? "Weekly Summary Report of Branch Head " + obj.BranchManagerName : " ";
    setLabelText("lblReportHeader", header);
    disabledPrintButton(obj.DateFrom == '')
    try {
        await Promise.all([
            loadNewLeads(obj),
            loadPotentialAccounts(obj),
            loadRHWeeklyRemarks(obj)
        ]);
        loadingScreen(true, 0);
    } catch (e) {
        showError(e);
        loadingScreen(true, 0);
    }
}

async function loadNewLeads(obj) {
    var data = await getNewLeadsList(obj.Username, obj.DateFrom, obj.DateToday, obj.BranchCode);
    setTotalLeads(data.WeeklyListTotalDetails);
    createNewLeadsTable(data.WeeklyList);
}

async function loadPotentialAccounts(obj) {
    var data1 = await getPotentialAccountsList(obj.Username, obj.DateFrom, obj.DateToday, obj.BranchCode);
    document.getElementById('lbl-CPAHeader').innerHTML = ( 'Total Calls for the Week : ' + data1.WeeklyList.length);
    setTotalPotentialAccounts(data1.WeeklyListTotalDetails);
    createPotentialAccountsTable(data1.WeeklyList);
}

async function loadRHWeeklyRemarks(obj) {
    var data2 = await setRHWeeklyRemarks();
    setLabelText("lblWeeklyRemarks", data2);
}

function disabledPrintButton(toDisabled) {
    hideError();
    showDiv(['tblOthers'], !toDisabled);
    showDiv(['divNewLeadReport', 'divPARevisitReport'], false);
    showDiv(['collapseOne', 'collapseTwo'], !toDisabled);
    setLabelText("lblReportHeader", "");
    if (!toDisabled) {
        $('#collapseOne').removeClass('accordion-collapse collapse').addClass('accordion-collapse collapse show');
        $('#collapseTwo').removeClass('accordion-collapse collapse').addClass('accordion-collapse collapse show');
    } else {
        $('#collapseOne').removeClass('accordion-collapse collapse show').addClass('accordion-collapse collapse');
        $('#collapseTwo').removeClass('accordion-collapse collapse show').addClass('accordion-collapse collapse');

    }
    toggleEventForElement('#btn-PrintNewLeads', 'click', AJAXWrapPageMethodCall, true, 'generateNewLeadsReport');
    toggleEventForElement('#btn-PrintPotentialAccounts', 'click', AJAXWrapPageMethodCall, true, 'generatePARevisitReport');


}

function loadBranch() {
    loadingScreen(false, 0);
    hideError();
    region = getDropdownValue('ddl-region') || '';
    disabledPrintButton(true);
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
    disabledPrintButton(true);
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

async function loadWeekList() {
    loadingScreen(false, 0);
    hideError();
    disabledPrintButton(true);
    Year = getDropdownValue('ddl-year') || '';
    Month = getDropdownText('ddl-Month') || '';
    if (Month != '' && Year != '') {
        var weekDeferred = $.Deferred();
        PageMethods.GetWeek(Year, Month,
            function OnSuccess(result) {
                weekDeferred.resolve(result);
            },
            function OnError(err) {
                weekDeferred.reject(err.get_message());
            });

        $.when(weekDeferred).done(async function (data) {
            try {
                if (data.length > 0) data.splice(0, 0, { description: '', value: '' });
                loadDropdown('ddl-week', data, true);
                setDropdownDefaultValue('ddl-week', data[1].value);
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
    else {
        loadDropdown('ddl-week', WeekList, true);
        loadingScreen(true, 0);
    }

}

function getNewLeadsList(UploadedBy, DateFrom, DateToday, BranchCode) {
    var deferred = $.Deferred();
    PageMethods.GetNewLeadsList(UploadedBy, DateFrom, DateToday, BranchCode,
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

function createNewLeadsTable(data) {
    var index = 1;
    var itemsPerPage = 10;
    var list = [];
    $('#tBodyNewLeads').empty();
    for (var r = 0; r < data.length; r++) {
        list.push(data[r]);
    }
    secureStorage.setItem('listNewLeads', JSON.stringify(list));
    allItems = data.length;
    showDiv(['tBodyNewLeadsTotal'], (allItems > 0) ? true : false);
    createTable('divNewLeads', 'tblNewLeads', 'tBodyNewLeads', list, index, itemsPerPage, allItems, 'listNewLeads', 'ListNewLeads');
}

function setTotalLeads(data) {

    // PageMethods.GetTotalNewLeads(UploadedBy, DateFrom, DateToday,BranchCode,
    //        function OnSuccess(data) {
    setLabelText("lblTotalLead", data.TotalLead);
    setLabelText("lblTotalSuspect", data.TotalSuspect);
    setLabelText("lblTotalProspect", data.TotalProspect);
    setLabelText("lblTotalCustomer", data.TotalCustomer);
    setLabelText("lblTotalCASATypes", data.TotalCASATypes);
    setLabelText("lblTotalInitialAmountDeposit", data.TotalAmount);
    setLabelText("lblTotalInitialDeposit", data.TotalAmountOthers);
    setLabelText("lblTotalADB", data.TotalADB);
    setLabelText("lblTotalLost", data.TotalLost);
    setLabelText("lblLoanProducts", data.TotalLoansProductsAvailed);
    setLabelText("lblTotalLoanAmountReported", data.TotalLoanAmountReported);

    setLabelText("lblTotalLeadsGeneratedVersusTarget", data.TotalLeadsGeneratedVersusTarget);
    setLabelText("lblLeadsToSuspect", data.LeadsToSuspect);
    setLabelText("lblSuspectToProspect", data.SuspectToProspect);
    setLabelText("lblProspectToCustomer", data.ProspectToCustomer);
    setLabelText("lblGeneralClosingRatio", data.GeneralClosingRatio);
    setLabelText("lblCasaClosingRatio", data.CasaClosingRatio);
    setLabelText("lblLostSalesRatio", data.LostSalesRatio);
    //        },
    //        function OnError(err) {
    //            showErrorModal(err.get_message());
    //            loadingScreen(true, 0);
    //        });
}

function getPotentialAccountsList(UploadedBy, DateFrom, DateToday, BranchCode) {
    var deferred = $.Deferred();
    PageMethods.GetPotentialAccountsList(UploadedBy, DateFrom, DateToday, BranchCode,
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

function createPotentialAccountsTable(data) {
    var index = 1;
    var itemsPerPage = 10;
    var list = [];
    $('#tBodyPotentialAccounts').empty();
    for (var r = 0; r < data.length; r++) {
        list.push(data[r]);
    }
    secureStorage.setItem('listPotentialAccounts', JSON.stringify(list));
    allItems = data.length;
    showDiv(['tBodyPotentialAccountsTotal'], (allItems > 0) ? true : false);
    createTable('divPotentialAccounts', 'tblPotentialAccounts', 'tBodyPotentialAccounts', list, index, itemsPerPage, allItems, 'listPotentialAccounts', 'ListPotentialAccounts');
}

function setTotalPotentialAccounts(data) {
    // 
    // PageMethods.GetTotalPotentialAccounts(UploadedBy, DateFrom, DateToday,BranchCode,
    //        function OnSuccess(data) {
    ////        setLabelText("lblTotalLeadPA", data.TotalLead);
    ////        setLabelText("lblTotalSuspectPA", data.TotalSuspect);
    setLabelText("lblTotalProspectPA", data.TotalProspect);
    //        setLabelText("lblTotalCustomerPA", data.TotalCustomer);
    //        setLabelText("lblTotalCASATypesPA", data.TotalCASATypes);
    setLabelText("lblTotalInitialAmountDepositPA", data.TotalAmount);
    setLabelText("lblTotalInitialDepositPA", data.TotalAmountOthers);
    setLabelText("lblTotalADBPA", data.TotalADB);
    //        setLabelText("lblTotalLostPA", data.TotalLost);
    //        setLabelText("lblLoanProductsPA", data.TotalLoansProductsAvailed);
    setLabelText("lblTotalLoanAmountReportedPA", data.TotalLoanAmountReported);
    setLabelText("lblTotalVisitPA", data.TotalVisits);
    //        },
    //        function OnError(err) {
    //            showErrorModal(err.get_message());
    //            loadingScreen(true, 0);
    //        });
}

function setRHWeeklyRemarks() {
    var deferred = $.Deferred();
    PageMethods.GetRHWeeklyRemarks(
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
    PageMethods.GetAddWeeklyRemarks(getTextboxValue('txtRHNewWeeklyComment'), false,
        async function (result) {
            if (result.isSuccess) {
                var data2 = await setRHWeeklyRemarks();
                setLabelText("lblWeeklyRemarks", data2);
                setTextboxValue('txtRHNewWeeklyComment', '');
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
    PageMethods.GetAddWeeklyRemarks(getTextboxValue('txtRHNewWeeklyComment'), true,
        async function (result) {
            if (result.isSuccess) {
                var data2 = await setRHWeeklyRemarks();
                setLabelText("lblWeeklyRemarks", data2);
                setTextboxValue('txtRHNewWeeklyComment', '');
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

function generateNewLeadsReport() {
    hideError();
    loadingScreen(false, 0);
    var branchName = getDropdownText('ddl-branch');
    PageMethods.GenerateReport("NewLeads", branchName,
        function OnSuccess(result) {
            if (result == '') {
                $('iframe[name="ifrmReportViewer"]').attr('src', '../../../../../Views/Reports/ReportViewer.aspx');
                showDiv(['divNewLeadReport'], true);
                showDiv(['divPARevisitReport'], false);
                loadingScreen(true, 0);
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
function generatePARevisitReport() {
    hideError();
    loadingScreen(false, 0);
    var branchName = getDropdownText('ddl-branch');
    PageMethods.GenerateReport("PARevisit", branchName,
        function OnSuccess(result) {
            if (result == '') {
                $('iframe[name="ifrmReportViewer"]').attr('src', '../../../../../Views/Reports/ReportViewer.aspx');
                showDiv(['divPARevisitReport'], true);
                showDiv(['divNewLeadReport'], false);
                loadingScreen(true, 0);
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

function RedirectToUpdateListNewLeads(clientID) {
    loadingScreen(false, 0);

    PageMethods.RedirectToUpdateClient(clientID,
        function OnSuccess(url) {
            location.href = url;
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}

function RedirectToUpdateListPotentialAccounts(clientID) {
    loadingScreen(false, 0);

    PageMethods.RedirectToUpdateClient(clientID,
        function OnSuccess(url) {
            location.href = url;
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });

}