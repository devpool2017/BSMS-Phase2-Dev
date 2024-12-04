 <%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master" CodeBehind="Monthly.aspx.vb" Inherits="LBP.VS2010.BSMS.Monthly" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Content/Scripts/PageScripts/Reports/SummaryPerGroup/Monthly.js"
        type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="frmHorizontal" class="card p-3">
        <div class="accordion" id="accordionPanelsStayOpenExample1">
            <div class="card p-3">
                <div class="row mb-sm-3">
                    <label class="col-sm-2 col-form-label label-font-standard" for="ddl-year">
                        Year & Month
                    </label>
                 <div class="col-sm-2">
                        <select id="ddl-year" class="form-select form-select-sm required requiredField">
                        </select>
                    </div>
                    <div class="col-sm-2">
                        <select id="ddl-month" class="form-select form-select-sm required requiredField">
                        </select>
                    </div>
                </div>
                <div class="row mb-sm-3">
                    <label class="col-sm-2 col-form-label label-font-standard" for="ddl-clientType">
                        Client Type
                    </label>
                    <div class="col-sm-3">
                        <select id="ddl-clientType" class="form-select form-select-sm">
                        </select>
                    </div>
                    <label class="col-sm-2 col-form-label label-font-standard" for="ddl-group">
                        Group
                    </label>
                    <div class="col-sm-3">
                        <select id="ddl-group" class="form-select form-select-sm required requiredField">
                        </select>
                    </div>
                </div>
                <div class="text-end col-12">
                    <a id="btn-ViewTables" class="lbpControl btn-sm searchButton right">Search </a><a
                        id="btn-ResetFilters" class="lbpControl btn-sm clearButton right">Reset </a>
                </div>
            </div>
            <br />
            <div class="card p-3" id="divMonthlySummaryPerGrp" hidden>
                <h6 class="label-font-standard" id="lblReportHeader" style="font-size: 18px;">
                    Monthly Summary Per Group
                </h6>
                <div id="div-MonthlyPerGroupReport" class="form-group noPagerDiv scrollHorizontalContent">
                    <table id="tbl-MonthlyPerGroupReport" class="table table-sm">
                        <thead class="noScrollFixedHeader">
                            <tr valign="middle" align="center">
                                <th align="center" valign="middle" width="180px" data-name="BranchName" data-alignment="left"
                                    data-columnname="BranchName">
                                    Branch Name
                                </th>
                                <th align="center" valign="middle" width="80px" data-name="TotalCPARevisits" data-alignment="right"
                                data-columnname="TotalCPARevisits">
                                Revisits
                            </th>
                                <th align="center" valign="middle" width="50px" data-name="TotalLeads" data-alignment="right"
                                    data-columnname="TotalLeads">
                                    Leads
                                </th>
                                <th align="center" valign="middle" width="60px" data-name="TotalSuspects" data-alignment="right"
                                    data-columnname="TotalSuspects">
                                    Suspects
                                </th>
                                <th align="center" valign="middle" width="60px" data-name="TotalProspects" data-alignment="right"
                                    data-columnname="TotalProspects">
                                    Prospects
                                </th>
                                <td align="center" valign="middle" width="150px" style="background: #666666; border-top: 1px solid #fff;
                                    color: #fff; font-size: 12px;">
                                    New Customers
                                    <table>
                                        <tr>
                                            <th align="center" valign="middle" width="50px" data-name="NewCustCasa" data-alignment="right"
                                                data-columnname="NewCustCasa" style="border-top: 0px solid #fff; border-left: 0px solid #fff; border-right: 0px solid #fff;">
                                                CASA
                                            </th>
                                            <th align="center" valign="middle" width="50px" data-name="NewCustLoan" data-alignment="right"
                                                data-columnname="NewCustLoan" style="border-top: 0px solid #fff; border-left: 0px solid #fff; border-right: 0px solid #fff;">
                                                Loans
                                            </th>
                                            <th align="center" valign="middle" width="50px" data-name="NewCustomerTotal" data-alignment="right"
                                                data-columnname="NewCustomerTotal" style="border-top: 0px solid #fff; border-left: 0px solid #fff; border-right: 0px solid #fff;">
                                                Total
                                            </th>
                                        </tr>
                                    </table>
                                </td>
                                <th align="center" valign="middle" width="100px" data-name="TotalADB" data-alignment="right"
                                    data-columnname="TotalADB">
                                    ADB
                                </th>
                                <th align="center" valign="middle" width="50px" data-name="TotalLosts" data-alignment="right"
                                    data-columnname="TotalLosts">
                                    Losts
                                </th>
                                <th align="center" valign="middle" width="100px" data-name="TotalInitialDeposit"
                                    data-alignment="right" data-columnname="TotalInitialDeposit">
                                    Initial Deposit
                                </th>
                                <th align="center" valign="middle" width="100px" data-name="TotalLedgerBalance" data-alignment="right"
                                    data-columnname="TotalLedgerBalance">
                                    Ledger Balance
                                </th>
                                <th align="center" valign="middle" width="100px" data-name="LoanAmountReported" data-alignment="right"
                                    data-columnname="LoanAmountReported">
                                    Loan Amount Reported
                                </th>
                                <th align="center" valign="middle" width="100px" data-name="LoanReleaseAmount" data-alignment="right"
                                    data-columnname="LoanReleaseAmount">
                                    Release Loan Amount
                                </th>
                            </tr>
                        </thead>
                        <tbody id="tBody-MonthlyPerGroupReport" class="noScrollContent border align-middle">
                        </tbody>
                        <tbody id="tBody-MonthlyReportTotal" class="noScrollContent">
                            <tr id="totalMonthly" valign="middle" align="right" style="background-color: #666666a1;
                                font-weight: bold;">
                                <td width="180px">
                                    Total
                                </td>
                                  <td width="80px" id="lblCPARevisits">
                            </td>
                                <td width="50px" id="lblTotalLeads">
                                </td>
                                <td width="60px" id="lblTotalSuspects">
                                </td>
                                <td width="60px" id="lblTotalProspects">
                                </td>
                                <td width="50px" id="lblNewCustomerCASA">
                                </td>
                                <td width="50px" id="lblNewCustomerLoan">
                                </td>
                                <td width="50px" id="lblNewCustomerTotal">
                                </td>
                                <td width="100px" id="lblTotalADB">
                                </td>
                                  <td width="50px" id="lblTotalLosts">
                                </td>
                                <td width="100px" id="lblTotalInitialDeposit">
                                </td>
                                <td width="100px" id="lblTotalLedgerBalance">
                                </td>
                                <td width="100px" id="lblTotalLoanAmountReported">
                                </td>
                                <td width="100px" id="lblTotalReleaseLoanAmount">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                     <div id="div-MonthlyPerBranchReport" class="form-group noPagerDiv scrollHorizontalContent">
                    <table id="tbl-MonthlyPerBranchReport" class="table table-sm">
                        <thead class="noScrollFixedHeader">
                            <tr valign="middle" align="center">
                                <th align="center" valign="middle" width="180px" data-name="BranchName" data-alignment="left"
                                    data-columnname="BranchName">
                                    Branch Name
                                </th>
                                <th align="center" valign="middle" width="80px" data-name="TotalCPARevisits" data-alignment="right"
                                data-columnname="TotalCPARevisits">
                                Revisits
                            </th>
                                <th align="center" valign="middle" width="50px" data-name="TotalLeads" data-alignment="right"
                                    data-columnname="TotalLeads">
                                    Leads
                                </th>
                                <th align="center" valign="middle" width="60px" data-name="TotalSuspects" data-alignment="right"
                                    data-columnname="TotalSuspects">
                                    Suspects
                                </th>
                                <th align="center" valign="middle" width="60px" data-name="TotalProspects" data-alignment="right"
                                    data-columnname="TotalProspects">
                                    Prospects
                                </th>
                                <td align="center" valign="middle" width="140px" style="background: #666666; border-top: 1px solid #fff;
                                    color: #fff; font-size: 12px;">
                                    New Customers
                                    <table>
                                        <tr>
                                            <th align="center" valign="middle" width="70px" data-name="NewCustCasa" data-alignment="right"
                                                data-columnname="NewCustCasa" style="border-top: 0px solid #fff; border-left: 0px solid #fff; border-right: 0px solid #fff;">
                                                CASA
                                            </th>
                                            <th align="center" valign="middle" width="70px" data-name="NewCustLoan" data-alignment="right"
                                                data-columnname="NewCustLoan" style="border-top: 0px solid #fff; border-left: 0px solid #fff; border-right: 0px solid #fff;">
                                                Loans
                                            </th>
                                        </tr>
                                    </table>
                                </td>
                                <th align="center" valign="middle" width="50px" data-name="TotalLosts" data-alignment="right"
                                    data-columnname="TotalLosts">
                                    Losts
                                </th>
                                <th align="center" valign="middle" width="100px" data-name="TotalInitialDeposit"
                                    data-alignment="right" data-columnname="TotalInitialDeposit">
                                    Initial Deposit
                                </th>
                                <th align="center" valign="middle" width="100px" data-name="TotalLedgerBalance" data-alignment="right"
                                    data-columnname="TotalLedgerBalance">
                                    Ledger Balance
                                </th>
                                <th align="center" valign="middle" width="100px" data-name="LoanAmountReported" data-alignment="right"
                                    data-columnname="LoanAmountReported">
                                    Loan Amount Reported
                                </th>
                                <th align="center" valign="middle" width="100px" data-name="LoanReleaseAmount" data-alignment="right"
                                    data-columnname="LoanReleaseAmount">
                                    Release Loan Amount
                                </th>
                            </tr>
                        </thead>
                        <tbody id="tBody-MonthlyPerBranchReport" class="noScrollContent border align-middle">
                        </tbody>
                        <tbody id="tBody1" class="noScrollContent">
                            <tr id="Tr1" valign="middle" align="right" style="background-color: #666666a1;
                                font-weight: bold;">
                                <td width="180px">
                                    Total
                                </td>
                                  <td width="80px" id="lblCPARevisitsBranch">
                            </td>
                                <td width="50px" id="lblTotalLeadsBranch">
                                </td>
                                <td width="60px" id="lblTotalSuspectsBranch">
                                </td>
                                <td width="60px" id="lblTotalProspectsBranch">
                                </td>
                                <td width="70px" id="lblNewCustomerCASABranch">
                                </td>
                                <td width="70px" id="lblNewCustomerLoanBranch">
                                </td>
                                  <td width="50px" id="lblTotalLostsBranch">
                                </td>
                                <td width="100px" id="lblTotalInitialDepositBranch">
                                </td>
                                <td width="100px" id="lblTotalLedgerBalanceBranch">
                                </td>
                                <td width="100px" id="lblTotalLoanAmountReportedBranch">
                                </td>
                                <td width="100px" id="lblTotalReleaseLoanAmountBranch">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                      <div class="text-end" id="div-printReport">
                <a id="btnPrintReport" class="lbpControl btn-sm printButton right canPrint">Print Summary Table</a>
            </div>
            <div id="divReport" class="mt-3">
                <iframe name="ifrmReportViewer" width="100%" src="about:blank"></iframe>
            </div>
            </div>
        </div>
    </div>
</asp:Content>
