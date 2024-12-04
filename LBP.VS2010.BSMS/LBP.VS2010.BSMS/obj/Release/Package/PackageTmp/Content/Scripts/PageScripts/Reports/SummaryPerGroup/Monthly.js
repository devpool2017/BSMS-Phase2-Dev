﻿var isAdmn;

$(async function () {
    hideError();
    loadingScreen(false, 0);

    var deferred = $.Deferred();
    showDiv(['divReport'], false);
    $('iframe[name="ifrmReportViewer"]').on('load', function () {
        resizeIframe(this);
        loadingScreen(true, 0);
    });


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
            var YearList = data.YearList;
            var MonthList = data.MonthList;
            var GroupCode = data.currentGroupCode;
            isAdmn = data.isAdmin;
            var currentDateDefault = data.currentDateDefault;

            showDiv(['div-MonthlyPerGroupReport'], isAdmn);
//            showDiv(['div-printReport', 'div-MonthlyPerBranchReport'], !isAdmn);
            showDiv(['div-MonthlyPerBranchReport'], !isAdmn);

            if (ClientTypeList.length > 0) ClientTypeList.splice(0, 0, { description: 'All', value: '' });
            if (MonthList.length > 0) MonthList.splice(0, 0, { description: '', value: '' });
            if (YearList.length > 0) YearList.splice(0, 0, { description: '', value: '' });
            loadDropdown('ddl-group', RegionList, isAdmn);
            loadDropdown('ddl-month', MonthList, true);
            loadDropdown('ddl-year', YearList, true);
            loadDropdown('ddl-clientType', ClientTypeList, true);

            if ($('#ddl-clientType').hasClass('es-input')) $('#ddl-clientType').attr("placeholder", "All");

            setDropdownDefaultValue('ddl-year', currentDateDefault.Year);
            setDropdownDefaultText('ddl-month', currentDateDefault.Month);
            setDropdownDefaultValue('ddl-group', isAdmn ? RegionList[0].value : GroupCode);

            toggleEventForElement('#ddl-clientType', 'change', function () { showAnnualDetails(false); }, true);
            toggleEventForElement('#ddl-month', 'change', function () { showAnnualDetails(false); }, true);
            toggleEventForElement('#ddl-year', 'change', function () { showAnnualDetails(false); }, true);
            toggleEventForElement('#ddl-group', 'change', function () { showAnnualDetails(false); }, true);

            toggleEventForElement('#btn-ResetFilters', 'click', function () {
                setDropdownDefaultValue('ddl-group', isAdmn ? '' : GroupCode);
                setDropdownDefaultValue('ddl-year', '');
                setDropdownDefaultValue('ddl-month', '');
                setDropdownDefaultValue('ddl-clientType', '');
                onLoadDetails();
            }, true);
            toggleEventForElement('#btn-ViewTables', 'click', AJAXWrapPageMethodCall, true, 'searchDetails');
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

async function onLoadDetails() {
    loadingScreen(false, 0);
    hideError();
    showAnnualDetails(false);
    loadingScreen(true, 0);

}

function showAnnualDetails(isEnabled) {
    showDiv(['divReport'], false);
    showDiv(['divMonthlySummaryPerGrp'], isEnabled);
}

function setTotalMonthlyReport(data) {
    setLabelText("lblCPARevisits", data.TotalCPARevisits);
    setLabelText("lblTotalLeads", data.TotalLeads);
    setLabelText("lblTotalSuspects", data.TotalSuspects);
    setLabelText("lblTotalProspects", data.TotalProspects);
    setLabelText("lblNewCustomerCASA", data.NewCustCasa);
    setLabelText("lblNewCustomerLoan", data.NewCustLoan);
    setLabelText("lblNewCustomerTotal", data.NewCustomerTotal);
    setLabelText("lblTotalADB", data.TotalADB);
    setLabelText("lblTotalInitialDeposit", data.TotalInitialDeposit);
    setLabelText("lblTotalLosts", data.TotalLosts);
    setLabelText("lblTotalLedgerBalance", data.TotalLedgerBalance);
    setLabelText("lblTotalLoanAmountReported", data.TotalLoanAmountReported);
    setLabelText("lblTotalReleaseLoanAmount", data.TotalLoanReleaseAmount);


    setLabelText("lblCPARevisitsBranch", data.TotalCPARevisits);
    setLabelText("lblTotalLeadsBranch", data.TotalLeads);
    setLabelText("lblTotalSuspectsBranch", data.TotalSuspects);
    setLabelText("lblTotalProspectsBranch", data.TotalProspects);
    setLabelText("lblNewCustomerCASABranch", data.NewCustCasa);
    setLabelText("lblNewCustomerLoanBranch", data.NewCustLoan);
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
                    var data = await SearchMonthlyReport(postData);
                    createMonthlyReportTable(data.MonthlyList);
                    setTotalMonthlyReport(data.MonthlyListTotalDetails);
                    var header = isAdmn ? "Monthly Summary Per Group ( " + postData.GroupCodeText + " )" : "Monthly Summary Per Branch ( " + postData.GroupCodeText + " )"
                    setLabelText("lblReportHeader", header);
                    toggleEventForElement('#btnPrintReport', 'click', AJAXWrapPageMethodCall, true, 'generateReport');
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
function createMonthlyReportTable(data) {
    var index = 1;
    var itemsPerPage = 100;
    var list = [];
    var code = isAdmn ? 'Group' : 'Branch';

    $('#tBody-MonthlyPer' + code + 'Report').empty();
    for (var r = 0; r < data.length; r++) {
        list.push(data[r]);
    }
    secureStorage.setItem('listMonthlyReport', JSON.stringify(list));
    allItems = data.length;
    createTable('div-MonthlyPer' + code + 'Report', 'tbl-MonthlyPer' + code + 'Report', 'tBody-MonthlyPer' + code + 'Report', list, index, itemsPerPage, allItems, 'listMonthlyReport', 'ListMonthlyReport');
}

function SearchMonthlyReport(obj) {
    setLabelText("lblReportHeader", "");
    var deferred = $.Deferred();
    PageMethods.GetMonthlyDetails(obj,
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
    var ddlmonth = document.getElementById("ddl-month");
    var ddlclientType = document.getElementById("ddl-clientType");
    var ddlgroup = document.getElementById("ddl-group");

    var postData = {
        Year: (ddlyear && ddlyear.value !== '') ? (typeof getDropdownValue("ddl-year") !== 'undefined') ? getDropdownValue("ddl-year") : "" : "",
        MonthCode: (ddlmonth && ddlmonth.value !== '') ? (typeof getDropdownValue("ddl-month") !== 'undefined') ? getDropdownValue("ddl-month") : "" : "",
        Month: (ddlmonth && ddlmonth.value !== '') ? (typeof getDropdownText("ddl-month") !== 'undefined') ? getDropdownText("ddl-month") : "" : "",
        ClientType: (ddlclientType && ddlclientType.value !== '') ? (typeof getDropdownValue("ddl-clientType") !== 'undefined') ? getDropdownValue("ddl-clientType") : "" : "",
        ClientTypeDesc: (ddlclientType && ddlclientType.value !== '') ? (typeof getDropdownText("ddl-clientType") !== 'undefined') ? getDropdownText("ddl-clientType") : "All" : "All",
        GroupCode: (ddlgroup && ddlgroup.value !== '') ? (typeof getDropdownValue("ddl-group") !== 'undefined') ? getDropdownValue("ddl-group") : "" : "",
        GroupCodeText: (ddlgroup && ddlgroup.value !== '') ? (typeof getDropdownText("ddl-group") !== 'undefined') ? getDropdownText("ddl-group") : "" : ""
    };

    return postData;
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

function resizeIframe(obj) {
    obj.style.height = obj.contentWindow.document.documentElement.scrollHeight + 'px';
}
