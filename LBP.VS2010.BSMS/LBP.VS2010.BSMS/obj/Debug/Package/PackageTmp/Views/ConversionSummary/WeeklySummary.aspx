<%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master"
    CodeBehind="WeeklySummary.aspx.vb" Inherits="LBP.VS2010.BSMS.WeeklySummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../../Content/Scripts/PageScripts/Reports/ConversionSummaryReport/WeeklySummaryReport.js"
        type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="frmHorizontal" class="card p-3">
        <div class="accordion" id="accordionPanelsStayOpenExample1">
            <div class="card p-3">
                <div class="row mb-sm-3" id="div-region">
                    <label class="col-sm-2 col-form-label label-font-standard" for="ddl-region">
                        Group
                    </label>
                    <div class="col-sm-3">
                        <select id="ddl-region" class="form-select form-select-sm required requiredField">
                        </select>
                    </div>
                </div>
                <div class="row mb-sm-3">
                    <label class="col-sm-2 col-form-label label-font-standard" for="ddl-branch">
                        Branch</label>
                    <div class="col-sm-3">
                        <select id="ddl-branch" class="form-select form-select-sm required requiredField">
                        </select>
                    </div>
                    <label class="col-sm-2 col-form-label label-font-standard" for="ddl-userFullName">
                        Branch Head
                    </label>
                    <div class="col-sm-3">
                        <select id="ddl-userFullName" class="form-select form-select-sm required requiredField">
                        </select>
                    </div>
                </div>
                <div class="row mb-3" id="divDateFilters">
                    <div class="col-sm-2">
                        <label class="col-sm-auto col-form-label label-font-standard">
                            Select Date
                        </label>
                    </div>
                    <div class="col-sm-2">
                        <select id="ddl-year" class="form-select form-select-sm required requiredField">
                        </select>
                    </div>
                    <div class="col-sm-2">
                        <select id="ddl-Month" class="form-select form-select-sm required requiredField">
                        </select>
                    </div>
                    <div class="col-sm-2">
                        <select id="ddl-week" class="form-select form-select-sm required requiredField">
                        </select>
                    </div>
                    <div class="text-end col-3">
                        <a id="btn-ViewTables" class="lbpControl btn-sm searchButton right">Search </a><a
                            id="btn-ResetFilters" class="lbpControl btn-sm clearButton right">Reset </a>
                    </div>
                </div>
            </div>
            <br />
            <div class="card p-3" id="divWeeklySummaryReport">
                <h6 class="label-font-standard" id="lblReportHeader" style="font-size: 18px;">
                    Weekly Summary Report of Branch Head
                </h6>
                <div id="divNewLeadsAccordion" data-bs-toggle="collapse" data-bs-target="#collapseOne">
                    <div class="card-header btn-lbp-green text-center rounded-0" id="divNewLeadsAccordionHeader">
                        <h6 class="mb-0">
                            New Leads
                        </h6>
                    </div>
                </div>
                <div id="collapseOne" class="accordion-collapse" aria-labelledby="headingOne">
                    <div class="accordion-body">
                        <div id="divNewLeads" class="form-group pagerDiv scrollHorizontalContent">
                            <table id="tblNewLeads" class="table table-sm" align="center">
                                <thead class="noScrollFixedHeader">
                                    <tr valign="middle" align="center">
                                        <th align="center" valign="middle" width="300px" data-name="Fullname" data-alignment="left"
                                            data-columnname=""  class="isLink" data-functions="RedirectToUpdate" data-pass="ClientID">
                                            Customer's Full Name
                                        </th>
                                        <th align="center" valign="middle" width="150px" data-name="ClientType" data-alignment="left"
                                            data-columnname="ClientType">
                                            Client Type
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="Lead" data-alignment="center"
                                            data-columnname="Lead">
                                            Lead
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="Suspect" data-alignment="center"
                                            data-columnname="Suspect">
                                            Suspect
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="Prospect" data-alignment="center"
                                            data-columnname="Prospect">
                                            Prospect
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="Customer" data-alignment="center"
                                            data-columnname="Customer">
                                            Customer
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="CASATypes" data-alignment="left"
                                            data-columnname="CASATypes">
                                            CASA Types
                                        </th>
                                        <th align="center" valign="middle" width="150px" data-name="Amount" data-alignment="right"
                                            data-columnname="Amount">
                                            Initial Amount of Deposit
                                        </th>
                                        <th align="center" valign="middle" width="150px" data-name="ADB" data-alignment="right"
                                            data-columnname="ADB">
                                            ADB
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="LoansProductsAvailed"
                                            data-alignment="left" data-columnname="LoansProductsAvailed">
                                            Loan Products
                                        </th>
                                        <th align="center" valign="middle" width="200px" data-name="LoanAmountReported" data-alignment="right"
                                            data-columnname="LoanAmountReported">
                                            Loan Amount Reported
                                        </th>
                                        <th align="center" valign="middle" width="150px" data-name="ProductsOffered" data-alignment="left"
                                            data-columnname="ProductsOffered">
                                            Products Offered
                                        </th>
                                        <th align="center" valign="middle" width="200px" data-name="OtherATypes" data-alignment="left"
                                            data-columnname="OtherATypes">
                                            Products & Services Availed
                                        </th>
                                        <th align="center" valign="middle" width="200px" data-name="AmountOthers" data-alignment="right"
                                            data-columnname="Initial Deposit (Products & Services)">
                                            Initial Deposit (Products & Services)
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="Visits" data-alignment="right"
                                            data-columnname="Visits">
                                            Visits
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="AccountNumbers" data-alignment="left"
                                            data-columnname="AccountNumbers">
                                            Account Number
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="LeadSource" data-alignment="left"
                                            data-columnname="LeadSource">
                                            Source of Lead
                                        </th>
                                        <th align="center" valign="middle" width="200px" data-name="IndustryType" data-alignment="left"
                                            data-columnname="IndustryType">
                                            Industry
                                        </th>
                                        <th align="center" valign="middle" width="300px" data-name="Remarks" data-alignment="left"
                                            data-columnname="Remarks">
                                            Remarks
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="Lost" data-alignment="center"
                                            data-columnname="Lost">
                                            Lost
                                        </th>
                                        <th align="center" valign="middle" width="200px" data-name="Reason" data-alignment="left"
                                            data-columnname="Reason">
                                            Reason
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="tBodyNewLeads" class="noScrollContent border align-middle">
                                </tbody>
                                <tbody id="tBodyNewLeadsTotal" class="noScrollContent" hidden>
                                    <tr id="totalLeads" valign="middle" align="center" style="background-color: #666666a1;
                                        font-weight: bold;">
                                        <td width="300px">
                                            TOTAL (Leads)
                                        </td>
                                        <td width="150px">
                                        </td>
                                        <td width="100px" id="lblTotalLead">
                                        </td>
                                        <td width="100px" id="lblTotalSuspect">
                                        </td>
                                        <td width="100px" id="lblTotalProspect">
                                        </td>
                                        <td width="100px" id="lblTotalCustomer">
                                        </td>
                                        <td width="100px" id="lblTotalCASATypes">
                                        </td>
                                        <td width="150px" id="lblTotalInitialAmountDeposit" align="right">
                                        </td>
                                        <td width="150px" id="lblTotalADB" align="right">
                                        </td>
                                        <td width="100px" id="lblLoanProducts">
                                        </td>
                                        <td width="200px" id="lblTotalLoanAmountReported" align="right">
                                        </td>
                                        <td width="150px">
                                        </td>
                                        <td width="200px">
                                        </td>
                                        <td width="200px" id="lblTotalInitialDeposit" align="right">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="200px">
                                        </td>
                                        <td width="300px">
                                        </td>
                                        <td width="100px" id="lblTotalLost">
                                            0
                                        </td>
                                        <td width="200px">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="text-end">
                            <a id="btn-PrintNewLeads" class="lbpControl btn-sm printButton right canPrint">Print New Leads
                                Report</a>
                        </div>
                        <div id="divNewLeadReport" class="mt-3">
                            <iframe name="ifrmReportViewer" width="100%" src="about:blank"></iframe>
                        </div>
                    </div>
                </div>
                <div id="divCPARevisitsAccordion" data-bs-toggle="collapse" data-bs-target="#collapseTwo">
                    <div class="card-header btn-lbp-green text-center rounded-0" id="Div2">
                        <h6 class="mb-0">
                            Potential Accounts Revisits
                        </h6>
                    </div>
                </div>
                <div id="collapseTwo" class="accordion-collapse" aria-labelledby="headingOne">
                    <div class="accordion-body">
                    <label class="col-sm-auto col-form-label label-font-standard" id="lbl-CPAHeader">
                    Total Calls for the Week : 
                        </label>
                        <div id="divPotentialAccounts" class="form-group pagerDiv scrollHorizontalContent">
                            <table id="tblPotentialAccounts" class="table table-sm" align="center">
                                <thead class="noScrollFixedHeader">
                                    <tr valign="middle" align="center">
                                        <th align="center" valign="middle" width="300px"  data-name="Fullname" data-alignment="left"
                                            data-columnname=""  class="isLink" data-functions="RedirectToUpdate" data-pass="ClientID">
                                            Customer's Full Name
                                        </th>
                                        <th align="center" valign="middle" width="150px" data-name="ClientType" data-alignment="left"
                                            data-columnname="ClientType">
                                            Client Type
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="Lead" data-alignment="center"
                                            data-columnname="Lead">
                                            Lead
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="Suspect" data-alignment="center"
                                            data-columnname="Suspect">
                                            Suspect
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="Prospect" data-alignment="center"
                                            data-columnname="Prospect">
                                            Prospect
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="Customer" data-alignment="center"
                                            data-columnname="Customer">
                                            Customer
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="CASATypes" data-alignment="center"
                                            data-columnname="CASATypes">
                                            CASA Types
                                        </th>
                                        <th align="center" valign="middle" width="150px" data-name="Amount" data-alignment="right"
                                            data-columnname="Amount">
                                            Initial Amount of Deposit
                                        </th>
                                        <th align="center" valign="middle" width="150px" data-name="ADB" data-alignment="right"
                                            data-columnname="ADB">
                                            ADB
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="LoansProductsAvailed"
                                            data-alignment="left" data-columnname="LoansProductsAvailed">
                                            Loan Products
                                        </th>
                                        <th align="center" valign="middle" width="200px" data-name="LoanAmountReported" data-alignment="right"
                                            data-columnname="LoanAmountReported">
                                            Loan Amount Reported
                                        </th>
                                        <th align="center" valign="middle" width="150px" data-name="ProductsOffered" data-alignment="left"
                                            data-columnname="ProductsOffered">
                                            Products Offered
                                        </th>
                                        <th align="center" valign="middle" width="200px" data-name="OtherATypes" data-alignment="left"
                                            data-columnname="OtherATypes">
                                            Products & Services Availed
                                        </th>
                                        <th align="center" valign="middle" width="200px" data-name="AmountOthers" data-alignment="right"
                                            data-columnname="Initial Deposit (Products & Services)">
                                            Initial Deposit (Products & Services)
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="Visits" data-alignment="right"
                                            data-columnname="Visits">
                                            Visits
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="AccountNumbers" data-alignment="left"
                                            data-columnname="AccountNumbers">
                                            Account Number
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="LeadSource" data-alignment="left"
                                            data-columnname="LeadSource">
                                            Source of Lead
                                        </th>
                                        <th align="center" valign="middle" width="200px" data-name="IndustryType" data-alignment="left"
                                            data-columnname="IndustryType">
                                            Industry
                                        </th>
                                        <th align="center" valign="middle" width="300px" data-name="Remarks" data-alignment="left"
                                            data-columnname="Remarks">
                                            Remarks
                                        </th>
                                        <th align="center" valign="middle" width="100px" data-name="Lost" data-alignment="center"
                                            data-columnname="Lost">
                                            Lost
                                        </th>
                                        <th align="center" valign="middle" width="200px" data-name="Reason" data-alignment="left"
                                            data-columnname="Reason">
                                            Reason
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="tBodyPotentialAccounts" class="noScrollContent border align-middle">
                                </tbody>
                                <tbody id="tBodyPotentialAccountsTotal" class="noScrollContent" hidden>
                                    <tr id="totalPotentialAccountRevisit" valign="middle" align="center" style="background-color: #666666a1;
                                        font-weight: bold;">
                                        <td width="300px">
                                            TOTAL (Potential Account Revisits)
                                        </td>
                                        <td width="150px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="100px" id="lblTotalProspectPA">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="150px" id="lblTotalInitialAmountDepositPA" align="right">
                                        </td>
                                     <td width="150px" id="lblTotalADBPA" align="right">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="200px" id="lblTotalLoanAmountReportedPA" align="right">
                                        </td>
                                        <td width="150px">
                                        </td>
                                        <td width="200px">
                                        </td>
                                        <td width="200px" id="lblTotalInitialDepositPA" align="right">
                                        </td>
                                        <td width="100px" id="lblTotalVisitPA" align="right">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="100px">
                                        </td>
                                        <td width="200px">
                                        </td>
                                        <td width="300px">
                                        </td>
                                        <td width="100px" id="lblTotalLostPA">
                                        </td>
                                        <td width="200px">
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="text-end">
                            <a id="btn-PrintPotentialAccounts" class="lbpControl btn-sm printButton right canPrint">Print
                                Potential Accounts Report</a>
                        </div>
                        <div id="divPARevisitReport" class="mt-3">
                            <iframe name="ifrmReportViewer" width="100%" src="about:blank"></iframe>
                        </div>
                    </div>
                </div>
                <table align="center" width="90%" id="tblOthers">
                    <tr>
                        <td align="left">
                            <table id="tbl-of-weeklyratios" class="label-font-standard" cellpadding="2" cellspacing="2"
                                bgcolor="#ffffff" frame="box" width="350px" align="center">
                                <tr>
                                    <td colspan="2" align="center">
                                        PROSPECTING RATIO
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Total Leads Generated Versus Target
                                    </td>
                                    <td width="100px" id="lblTotalLeadsGeneratedVersusTarget">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        CONVERSION PERCENTAGES
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Leads to Suspect
                                    </td>
                                    <td id="lblLeadsToSuspect">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Suspect to Prospect
                                    </td>
                                    <td id="lblSuspectToProspect">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Prospect to Customer
                                    </td>
                                    <td id="lblProspectToCustomer">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        General Closing Ratio<br />
                                        (Total Close/Total Leads)
                                    </td>
                                    <td id="lblGeneralClosingRatio">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        CASA Closing Ratio<br />
                                        (Total CASA/Total Leads)
                                    </td>
                                    <td id="lblCasaClosingRatio">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Lost Sales Ratio
                                    </td>
                                    <td id="lblLostSalesRatio">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <span id="lblregionheadcomment" class="label-font-standard">Group Head's Comments &
                                            Recommendations : </span>
                                        <br />
                                        <span id="lblWeeklyRemarks" style="display: inline-block; background-color: #EEE9E5;
                                            border-style: Inset; height: 100px; width: 550px; overflow: auto; overflow-x: hidden;
                                            font-size: 12px"></span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="panelAddRegionHeadComment" runat="server">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <textarea type="text" id="txtRHNewWeeklyComment" class="lbpControl form-control name"
                                                            rows="7" style="border-color: Silver; font-size: Small; width: 550px;" maxlength='<%#getFieldLength(FieldVariables.REMARKS, LengthSetting.MAX)%>'></textarea>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div class="text-end" id="buttonsComment">
                                                            <a id="btn-AddCommentForAdmin" class="lbpControl btn-sm addButton"><span id="lbl-ACA">
                                                            </a><a id="btn-AddComment" class="lbpControl btn-sm addButton"><span id="lbl-AC">
                                                            </a>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
