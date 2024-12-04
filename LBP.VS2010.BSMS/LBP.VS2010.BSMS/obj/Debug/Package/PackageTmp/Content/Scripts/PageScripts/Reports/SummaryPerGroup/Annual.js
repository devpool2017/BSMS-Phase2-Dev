var isAdmn;
$(async function () {
    hideError();
    loadingScreen(false, 0);
    showDiv(['div-ReportCASA', 'div-ReportLoan', 'divReport'], false);
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
            var ClientTypeList = data.ClientTypeList;
            var IndustryTypeList = data.IndustryTypeList;
            var YearList = data.YearList;
            var ADBRangeList = data.ADBRangeList;
            var GroupCode = data.currentGroupCode;
            isAdmn = data.isAdmin;
            var currentDateDefault = data.currentDateDefault;
            if (ClientTypeList.length > 0) ClientTypeList.splice(0, 0, { description: 'All', value: '' });
            if (IndustryTypeList.length > 0) IndustryTypeList.splice(0, 0, { description: 'All', value: '' });
            if (ADBRangeList.length > 0) ADBRangeList.splice(0, 0, { description: 'All', value: '' });
            if (YearList.length > 0) YearList.splice(0, 0, { description: '', value: '' });

            loadDropdown('ddl-group', RegionList, isAdmn);
            loadDropdown('ddl-year', YearList, true);
            loadDropdown('ddl-clientType', ClientTypeList, true);
            loadDropdown('ddl-industryType', IndustryTypeList, true);
            loadDropdown('ddl-ADBRange', ADBRangeList, true);

            setDropdownDefaultValue('ddl-year', currentDateDefault.Year);
            setDropdownDefaultValue('ddl-group', isAdmn ? RegionList[0].value : GroupCode);

            showDiv(['div-filters', 'div-AnnualPerGroupReport'], isAdmn);
//            showDiv(['div-printReport', 'div-AnnualPerBranchReport'], !isAdmn);
            showDiv(['div-AnnualPerBranchReport'], !isAdmn);
            if ($('#ddl-industryType').hasClass('es-input')) $('#ddl-industryType').attr("placeholder", "All");
            toggleEventForElement('#ddl-clientType', 'change', function () { showAnnualDetails(false); }, true);
            toggleEventForElement('#ddl-industryType', 'change', function () { showAnnualDetails(false); }, true);
            toggleEventForElement('#ddl-ADBRange', 'change', function () { showAnnualDetails(false); }, true);
            toggleEventForElement('#ddl-year', 'change', function () { showAnnualDetails(false); }, true);
            toggleEventForElement('#ddl-group', 'change', function () { showAnnualDetails(false); }, true);

            toggleEventForElement('#btn-ResetFilters', 'click', function () {
                setDropdownDefaultValue('ddl-group', isAdmn ? '' : GroupCode);
                setDropdownDefaultValue('ddl-year', '');
                setDropdownDefaultValue('ddl-clientType', '');
                setDropdownDefaultValue('ddl-industryType', '');
                setDropdownDefaultValue('ddl-ADBRange', '');
                onLoadDetails(isAdmn);
            }, true);
            toggleEventForElement('#btn-ViewTables', 'click', AJAXWrapPageMethodCall, true, 'searchDetails');
            await onLoadDetails(isAdmn);

        } catch (e) {
            showErrorModal(e);
        }

        loadingScreen(true, 0);
    }).fail(function (e) {
        showErrorModal(e);
        loadingScreen(true, 0);
    });
});

async function onLoadDetails(isAdmin) {
    loadingScreen(false, 0);
    hideError();
    showAnnualDetails(false);
    loadingScreen(true, 0);
}

function showAnnualDetails(isEnabled) {
    showDiv(['divReport'], false);
    showDiv(['divAnnualSummaryPerGrp'], isEnabled);
}

function setTotalAnnualReport(data, isAdmin) {
    setLabelText("lblTotalLeads", data.TotalLeads);
    setLabelText("lblTotalSuspects", data.TotalSuspects);
    setLabelText("lblTotalProspects", data.TotalProspects);
    setLabelText("lblTotalCustomers", data.TotalCustomers);
    setLabelText("lblTotalCASATypes", data.TotalCASA);
    setLabelText("lblTotalLoanProducts", data.TotalLoans);
    setLabelText("lblTotalADB", data.TotalADB);
    setLabelText("lblTotalInitialDeposit", data.TotalInitialDeposit);
    setLabelText("lblTotalLosts", data.TotalLosts);
    setLabelText("lblTotalLedgerBalance", data.TotalLedgerBalance);
    setLabelText("lblTotalLoanAmountReported", data.TotalLoanAmountReported);
    setLabelText("lblTotalReleaseLoanAmount", data.TotalLoanReleaseAmount);


    setLabelText("lblTotalLeadsBranch", data.TotalLeads);
    setLabelText("lblTotalSuspectsBranch", data.TotalSuspects);
    setLabelText("lblTotalProspectsBranch", data.TotalProspects);
    setLabelText("lblTotalCASATypesBranch", data.TotalCASA);
    setLabelText("lblTotalLoanProductsBranch", data.TotalLoans);
    setLabelText("lblTotalADBBranch", data.TotalADB);
    setLabelText("lblTotalInitialDepositBranch", data.TotalInitialDeposit);
    setLabelText("lblTotalLostsBranch", data.TotalLosts);
    setLabelText("lblTotalLedgerBalanceBranch", data.TotalLedgerBalance);
    setLabelText("lblTotalLoanAmountReportedBranch", data.TotalLoanAmountReported);
    setLabelText("lblTotalReleaseLoanAmountBranch", data.TotalLoanReleaseAmount);
}

async function searchDetails() {
    loadingScreen(false, 0);
    hideError();

    var postData = await getFiltersObj()

    try {
        PageMethods.ValidateSearchFilter(postData,
            async function OnSuccess(result) {
                if (result == '') {
                    showAnnualDetails(true);
                    secureStorage.setItem('validFilters', JSON.stringify(postData));
                    var data = await SearchAnnualReport(postData);
                    createAnnualReportTable(data.AnnualList, isAdmn);
                    setTotalAnnualReport(data.AnnualListTotalDetails, isAdmn);
                    var header = isAdmn ? "Annual Summary Per Group ( " + postData.GroupCodeText + " )" : "Annual Summary Per Branch ( " + postData.GroupCodeText + " )"
                    setLabelText("lblReportHeader", header);
                    toggleEventForElement('#btnPrintReport', 'click', function () { generateReport("All"); }, true);
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
    catch (e) {
        showError(e);
        loadingScreen(true, 0);
    }
}
function createAnnualReportTable(data, isAdmin) {
    var index = 1;
    var itemsPerPage = 100;
    var list = [];
    var code = isAdmin ? 'Group' : 'Branch';

    $('#tBody-AnnualPer' + code + 'Report').empty();
    for (var r = 0; r < data.length; r++) {
        list.push(data[r]);
    }
    secureStorage.setItem('listAnnuallyReport', JSON.stringify(list));
    allItems = data.length;
    createTable('div-AnnualPer' + code + 'Report', 'tbl-AnnualPer' + code + 'Report', 'tBody-AnnualPer' + code + 'Report', list, index, itemsPerPage, allItems, 'listAnnuallyReport', 'ListAnnuallyReport');
}

function SearchAnnualReport(obj) {
    var deferred = $.Deferred();
    setLabelText("lblReportHeader", "");
    PageMethods.GetAnnualDetails(obj,
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

async function getFiltersObj() {
    var ddlyear = document.getElementById("ddl-year");
    var ddlclientType = document.getElementById("ddl-clientType");
    var ddlindustryType = document.getElementById("ddl-industryType");
    var ddlADBRange = document.getElementById("ddl-ADBRange");
    var ddlgroup = document.getElementById("ddl-group");

    var postData = {
        Year: (ddlyear && ddlyear.value !== '') ? (typeof getDropdownValue("ddl-year") !== 'undefined') ? getDropdownValue("ddl-year") : "" : "",
        ClientType: (ddlclientType && ddlclientType.value !== '') ? (typeof getDropdownValue("ddl-clientType") !== 'undefined') ? getDropdownValue("ddl-clientType") : "" : "",
        ClientTypeDesc: (ddlclientType && ddlclientType.value !== '') ? (typeof getDropdownText("ddl-clientType") !== 'undefined') ? getDropdownText("ddl-clientType") : "All" : "All",
        IndustryType: (ddlindustryType && ddlindustryType.value !== '') ? (typeof getDropdownValue("ddl-industryType") !== 'undefined') ? getDropdownValue("ddl-industryType") : "" : "",
        IndustryTypeDesc: (ddlindustryType && ddlindustryType.value !== '') ? (typeof getDropdownText("ddl-industryType") !== 'undefined') ? getDropdownText("ddl-industryType") : "All" : "All",
        ADBRangeName: (ddlADBRange && ddlADBRange.value !== '') ? (typeof getDropdownValue("ddl-ADBRange") !== 'undefined') ? getDropdownValue("ddl-ADBRange") : "" : "",
        ADBRangeNameDesc: (ddlADBRange && ddlADBRange.value !== '') ? (typeof getDropdownText("ddl-ADBRange") !== 'undefined') ? getDropdownText("ddl-ADBRange") : "All" : "All",
        GroupCode: (ddlgroup && ddlgroup.value !== '') ? (typeof getDropdownValue("ddl-group") !== 'undefined') ? getDropdownValue("ddl-group") : "" : "",
        GroupCodeText: (ddlgroup && ddlgroup.value !== '') ? (typeof getDropdownText("ddl-group") !== 'undefined') ? getDropdownText("ddl-group") : "" : ""

    };

    return postData;
}

async function viewCASAListAnnuallyReport(BranchCode, BranchName, totalCASA) {
    loadingScreen(false, 0);
    if (totalCASA > 0) {
        showDiv(['divReport'], false);
        var obj = getSessionList('validFilters');
        var data = await SearchCASAType(BranchCode);
        showDiv(['div-ReportCASA', 'div-ReportLoan'], false);
        toggleEventForElement('#btn-PrintCASAReport', 'click', function () { generateReport("CASA"); }, true);
        toggleEventForElement('#btn-cancelC', 'click', function () { $('#modalCASA').modal('hide'); }, true);
        createCASATypeTable(data);
        setLabelText("lbl-Casa", "List of Clients of " + BranchName + " Branch <br> ( Year : " + obj.Year + " | Client Type : " + obj.ClientTypeDesc + " | Industry : " + obj.IndustryTypeDesc + " | ADB Range : " + obj.ADBRangeNameDesc + " )");
        $('#modalCASA').modal('show');
    }
    loadingScreen(true, 0);

}

function SearchCASAType(BranchCode) {
    var deferred = $.Deferred();
    PageMethods.GetCASATypesList(BranchCode,
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

function createCASATypeTable(data) {
    var index = 1;
    var itemsPerPage = 200;
    var list = [];
    $('#tBody-CASAType').empty();
    for (var r = 0; r < data.length; r++) {
        list.push(data[r]);
    }
    secureStorage.setItem('listCASAType', JSON.stringify(list));
    allItems = data.length;
    createTable('div-CASATypeTable', 'tbl-CASATypeTable', 'tBody-CASAType', list, index, itemsPerPage, allItems, 'listCASAType', 'ListCASAType');
}


async function viewLoanProductListAnnuallyReport(BranchCode, BranchName, totalLoans) {
    loadingScreen(false, 0);
    if (totalLoans > 0) {
        showDiv(['divReport'], false);
        var obj = getSessionList('validFilters');
        var data = await SearchLoanProduct(BranchCode);
        createLoanProductTable(data);

        showDiv(['div-ReportCASA', 'div-ReportLoan'], false);
        toggleEventForElement('#btn-PrintLoanReport', 'click', function () { generateReport("Loan"); }, true);
        toggleEventForElement('#btn-cancelL', 'click', function () { $('#modalLoan').modal('hide'); }, true);
        setLabelText("lbl-LoanHeader", "List of Clients of " + BranchName + " Branch <br> ( Year : " + obj.Year + " | Client Type : " + obj.ClientTypeDesc + " | Industry : " + obj.IndustryTypeDesc + " | ADB Range : " + obj.ADBRangeNameDesc + " )");

        $('#modalLoan').modal('show');
    }
    loadingScreen(true, 0);

}

function SearchLoanProduct(BranchCode) {
    var deferred = $.Deferred();
    PageMethods.GetLoanProductLists(BranchCode,
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

function createLoanProductTable(data) {
    var index = 1;
    var itemsPerPage = 200;
    var list = [];
    $('#tBody-loanProduct').empty();
    for (var r = 0; r < data.length; r++) {
        list.push(data[r]);
    }
    secureStorage.setItem('listLoanProduct', JSON.stringify(list));
    allItems = data.length;
    createTable('div-LoanProductTable', 'tbl-LoanProductTable', 'tBody-loanProduct', list, index, itemsPerPage, allItems, 'listLoanProduct', 'ListLoanProduct');
}


function generateReport(table) {
    hideError();
    loadingScreen(false, 0);
    PageMethods.GenerateReport(table,
        function OnSuccess(result) {
            if (result == '') {
                $('iframe[name="ifrmReportViewer"]').attr('src', '../../../../../Views/Reports/ReportViewer.aspx');
                 showDiv(['div-ReportLoan'], (table == 'Loan'));
                 showDiv(['div-ReportCASA'], (table == 'CASA'));
                 showDiv(['divReport'], (table == 'All'));
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