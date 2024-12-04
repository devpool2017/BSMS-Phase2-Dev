$(async function () {
    hideError();
    loadingScreen(false, 0);
    $('iframe[name="ifrmReportViewer_Leads"]').on('load', function () {
        resizeIframe(this);
    });
    $('iframe[name="ifrmReportViewer_PARevisit"]').on('load', function () {
        resizeIframe(this);
    });

    var deferred = $.Deferred();
    PageMethods.OnLoadData(
        function OnSuccess(result) {
            deferred.resolve(result);
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });
        +
    $.when(deferred).done(async function (data) {
        try {
            var YearList = data.YearList;
            var MonthList = data.MonthList;
            var ClientDetails = data.ClientDetails;
            var WeekList = data.WeekList;
            secureStorage.setItem('ClientDetails', JSON.stringify(data.ClientDetails));

            loadDropdown('ddl-year', YearList, YearList.length > 0, ClientDetails.YearNumber);
            loadDropdown('ddl-month', MonthList, MonthList.length > 0, ClientDetails.MonthCode.substring(0, 1) == '0' ? ClientDetails.MonthCode.substring(1) : ClientDetails.MonthCode);
            loadDropdown('ddl-week', WeekList, WeekList.length > 0, ClientDetails.WeekNumber);
            
            toggleEventForElement('#ddl-month', 'change', AJAXWrapPageMethodCall, true, 'loadWeekList');
            if (getDropdownValue('ddl-month') !== '') {
                toggleEventForElement('#ddl-year', 'change', AJAXWrapPageMethodCall, true, 'loadWeekList');
            }

            toggleEventForElement('#btnReset', 'click', function () {
                setDropdownDefaultValue('ddl-year', ClientDetails.YearNumber);
                setDropdownDefaultValue('ddl-month', ClientDetails.MonthCode.substring(0, 1) == '0' ? ClientDetails.MonthCode.substring(1) : ClientDetails.MonthCode);
                setDropdownDefaultValue('ddl-week', ClientDetails.WeekNumber);
                onLoadTables(ClientDetails);
            }, true);

            toggleEventForElement('#btnSearch', 'click', AJAXWrapPageMethodCall, true, 'ValidateSearch');

            toggleEventForElement('#btnBack', 'click', BackToWeeklyActivityCount, true);
            toggleEventForElement('#btnPrintNewLeads', 'click', GenerateReport, true, 'Leads');
            toggleEventForElement('#btnPrintPotentialAccounts', 'click', GenerateReport, true, 'PARevisit');
            await onLoadTables(ClientDetails); 
        } catch (e) {
            showErrorModal(e);
        }

        loadingScreen(true, 0);
    }).fail(function (e) {
        showErrorModal(e);
        loadingScreen(true, 0);
    });
});

async function loadWeekList() {
    loadingScreen(false, 0);
    hideError();
    Year = getDropdownValue('ddl-year') || '';
    Month = getDropdownText('ddl-month') || '';
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
                loadDropdown('ddl-week', data, true, data[0].value);
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
//        loadDropdown('ddl-week', WeekList, true, '');
        loadingScreen(true, 0);
    }
}

async function onLoadTables(obj) {
    loadingScreen(false, 0);
    hideError();
    setLabelText("lblReportHeader", (obj.UploadedBy != '' ? "Weekly Activity : List of Target Market (Potential Accounts) of Branch Head " + obj.UploadedBy : ''));
    
    var data = await GetNewLeadsList(obj.DateFrom, obj.DateTo);
    createNewLeadsTable(data);

    var data1 = await GetPotentialRevisitList(obj, obj.DateFrom, obj.DateTo);
    createPotentialAccountsTable(data1);
    
    showDiv(['divNewLeadReport', 'divPARevisitReport'], false);
    loadingScreen(true, 0);
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
    showDiv(['tBodyNewLeads', 'tBodyNewLeadsTotal'], (allItems > 0));
    setLabelText("lblTotalLead", data.length > 0 ? data[data.length - 1].TotalLead : '');
    createTable('divNewLeads', 'tblNewLeads', 'tBodyNewLeads', list, index, itemsPerPage, allItems, 'listNewLeads', 'NewLeads');
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
    showDiv(['tBodyPotentialAccounts', 'tBodyPotentialAccountsTotal'], (allItems > 0));
    createTable('divPotentialAccounts', 'tblPotentialAccounts', 'tBodyPotentialAccounts', list, index, itemsPerPage, allItems, 'listPotentialAccounts', 'PotentialAccounts');
    setLabelText("lblSuspect", data.length > 0 ? data[data.length - 1].TotalSuspect : '');
    setLabelText("lblProspect", data.length > 0 ? data[data.length - 1].TotalProspect : '');
    setLabelText("lblCustomer", data.length > 0 ? data[data.length - 1].TotalCustomer : '');
}

async function getFiltersObj() {
    var weekNum = document.getElementById("ddl-week");
    var ClientDetails = getSessionList('ClientDetails');
    var postData = {
        Branch: ClientDetails.Branch,
        BranchCode: ClientDetails.BranchCode,
        BranchHeadName: ClientDetails.BranchHeadName,
        UploadedBy: ClientDetails.UploadedBy,
        GroupCode: ClientDetails.GroupCode,
        YearNumber: (typeof getDropdownValue("ddl-year") !== 'undefined') ? getDropdownValue("ddl-year") : "",
        MonthCode: (typeof getDropdownValue("ddl-month") !== 'undefined') ? getDropdownValue('ddl-month') : "",
        Month: (typeof getDropdownText("ddl-month") !== 'undefined') ? getDropdownText('ddl-month') : "",
        WeekNumber: (weekNum && weekNum.value !== '') ? (typeof getDropdownValue("ddl-week") !== 'undefined') ? getDropdownValue("ddl-week") : "" : "",
        ViewPage: "DetailView"
    };

    return postData;
}

async function ValidateSearch() {
    hideError();
    loadingScreen(false, 0);
    showDiv(['divNewLeadReport', 'divPARevisitReport'], false);
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

function GetNewLeadsList(DateFrom, DateTo) {
    var deferred = $.Deferred();
    PageMethods.GetClientsFilteredByDate(DateFrom, DateTo,
        function OnSuccess(result) {
            if (result != null) {
                deferred.resolve(result);
            }
            else {
                deferred.reject($("[id$=hdnError]").val());
            }
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });

    return deferred.promise();
}

function GetPotentialRevisitList(obj, DateFrom, DateTo) {
    var deferred = $.Deferred();
    PageMethods.GetWeeklyActivityCPARevisitClients(DateFrom, DateTo, obj.YearNumber, obj.MonthCode, obj.WeekNumber,
        function OnSuccess(result) {
            if (result != null) {
                deferred.resolve(result);
            }
            else {
                deferred.reject($("[id$=hdnError]").val());
            }
        },
        function OnError(err) {
            deferred.reject(err.get_message());
        });

    return deferred.promise();
}

async function searchTables(postData) {
    loadingScreen(false, 0);
    hideError();
    
    try {
        PageMethods.GetDate(postData,
            async function (result) {
                if (result != null) {
                    var ClientDetails = getSessionList('ClientDetails');
                    setLabelText("lblReportHeader", (ClientDetails.UploadedBy != '' ? "Weekly Activity : List of Target Market (Potential Accounts) of Branch Head " + ClientDetails.UploadedBy : ''));

                    var data = await GetNewLeadsList(result.DateFrom, result.DateTo);
                    createNewLeadsTable(data);

                    var data1 = await GetPotentialRevisitList(postData, result.DateFrom, result.DateTo);
                    createPotentialAccountsTable(data1);

                }
                else {
                    showError(result);
                }
                    loadingScreen(true, 0);
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

async function GenerateReport(t, tableName) {
    hideError();
    loadingScreen(false, 0);
    var obj = await getFiltersObj();
    PageMethods.GenerateReport(tableName, obj,
        function OnSuccess(result) {
            if (result == '') {
                $('iframe[name="ifrmReportViewer_' + (tableName == "Leads" ? "Leads"  : "PARevisit") + '"]').attr('src', '/Views/Reports/ReportViewer.aspx');
                showDiv(['divNewLeadReport'], tableName == "Leads");
                showDiv(['divPARevisitReport'], tableName == "PARevisit");
            }
            else {
                showError(result);
            }
            loadingScreen(true, 0);
        },
        function OnError(err) {
            showErrorModal(err.get_message());
            loadingScreen(true, 0);
        });
}

function RedirectToUpdateClient(clientId) {
    hideError();
    loadingScreen(false, 0);
    PageMethods.SaveDetails(clientId,
        function OnSuccess(result) {
            if (result == '') {
                window.location.href = "/Views/TargetMarket/UpdateClientDetails.aspx";
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

function viewNewLeads(clientId) {
    RedirectToUpdateClient(clientId);
}

function viewPotentialAccounts(clientId) {
    RedirectToUpdateClient(clientId);
}

function BackToWeeklyActivityCount() {
    window.location = "/Views/PotentialAccount/WeeklyActivity.aspx";
}

function resizeIframe(obj) {
    obj.style.height = obj.contentWindow.document.documentElement.scrollHeight + 'px';
    loadingScreen(true, 0);
}