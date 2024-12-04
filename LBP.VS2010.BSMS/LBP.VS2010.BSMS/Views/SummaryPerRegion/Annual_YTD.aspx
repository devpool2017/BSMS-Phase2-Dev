<%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master"
    CodeBehind="Annual_YTD.aspx.vb" Inherits="LBP.VS2010.BSMS.Annual_YTD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Content/Scripts/PageScripts/Reports/SummaryPerGroup/Annual.js"
        type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="frmHorizontal" class="card p-3">
        <div class="accordion" id="accordionPanelsStayOpenExample1">
            <div class="card p-3">
                <div class="row mb-sm-3">
                    <label class="col-sm-2 col-form-label label-font-standard" for="ddl-year">
                        Year
                    </label>
                    <div class="col-sm-3">
                        <select id="ddl-year" class="form-select form-select-sm required requiredField">
                        </select>
                    </div>
                </div>
                <div class="row mb-sm-3" id="div-filters">
                    <label class="col-sm-2 col-form-label label-font-standard" for="ddl-ADBRange">
                        ADB Range
                    </label>
                    <div class="col-sm-3">
                        <select id="ddl-ADBRange" class="form-select form-select-sm">
                        </select>
                    </div>
                    <label class="col-sm-2 col-form-label label-font-standard" for="ddl-industryType">
                        Industry Type
                    </label>
                    <div class="col-sm-3">
                        <select id="ddl-industryType" class="form-select form-select-sm">
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
            <div class="card p-3" id="divAnnualSummaryPerGrp" hidden>
                <h6 class="label-font-standard" id="lblReportHeader" style="font-size: 18px;">
                    Annually Summary Per Group
                </h6>
  <div id="div-AnnualPerGroupReport" class="form-group noPagerDiv scrollHorizontalContent">
                    <table id="tbl-AnnualPerGroupReport" class="table table-sm">
                        <thead class="noScrollFixedHeader">
                            <tr valign="middle" align="center">
                         <%--       <th align="center" valign="middle" width="90px" data-name="BranchCode" data-alignment="center"
                                    data-columnname="BranchCode">
                                    Branch Code
                                </th>--%>
                                <th align="center" valign="middle" width="150px" data-name="BranchName" data-alignment="left"
                                    data-columnname="BranchName">
                                    Branch Name
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
                                <th align="center" valign="middle" width="80px" data-name="TotalCustomers" data-alignment="right"
                                    data-columnname="TotalCustomers">
                                    Customers
                                </th>
                                <th align="center" valign="middle" width="80px" data-name="TotalCASA" data-alignment="right"
                                    data-columnname="TotalCASA" data-functions="viewCASA" class="isLink" data-pass="BranchCode,BranchName,TotalCASA">
                                    CASA Types
                                </th>
                                <th align="center" valign="middle" width="100px" data-name="TotalLoans" data-alignment="right"
                                    data-columnname="TotalLoans" data-functions="viewLoanProduct" class="isLink"
                                    data-pass="BranchCode,BranchName,TotalLoans">
                                    Loan Products
                                </th>
                                <th align="center" valign="middle" width="100px" data-name="TotalADB" data-alignment="right"
                                    data-columnname="TotalADB">
                                    ADB
                                </th>
                                <th align="center" valign="middle" width="120px" data-name="TotalInitialDeposit"
                                    data-alignment="right" data-columnname="TotalInitialDeposit">
                                    Initial Deposit
                                </th>
                                <th align="center" valign="middle" width="120px" data-name="TotalLedgerBalance" data-alignment="right"
                                    data-columnname="TotalLedgerBalance">
                                    Ledger Balance
                                </th>
                                <th align="center" valign="middle" width="80px" data-name="TotalLosts" data-alignment="right"
                                    data-columnname="TotalLosts">
                                    Losts
                                </th>
                                <th align="center" valign="middle" width="120px" data-name="LoanAmountReported" data-alignment="right"
                                    data-columnname="LoanAmountReported">
                                    Desired Loan Amount
                                </th>
                                <th align="center" valign="middle" width="120px" data-name="LoanReleaseAmount" data-alignment="right"
                                    data-columnname="LoanReleaseAmount">
                                    Loan Amount Availed
                                </th>
                            </tr>
                        </thead>
                        <tbody id="tBody-AnnualPerGroupReport" class="noScrollContent border align-middle">
                        </tbody>
                        <tbody id="tBody-AnnuallyReportTotal" class="noScrollContent">
                            <tr id="totalLeads" valign="middle" align="right" style="background-color: #666666a1;
                                font-weight: bold;">
                          <%--      <td width="90px">
                                </td>--%>
                                <td width="150px">
                                    Total
                                </td>
                                <td width="50px" id="lblTotalLeads">
                                </td>
                                <td width="60px" id="lblTotalSuspects">
                                </td>
                                <td width="60px" id="lblTotalProspects">
                                </td>
                                <td width="80px" id="lblTotalCustomers">
                                </td>
                                <td width="80px" id="lblTotalCASATypes">
                                </td>
                                <td width="100px" id="lblTotalLoanProducts">
                                </td>
                                <td width="100px" id="lblTotalADB">
                                </td>
                                <td width="120px" id="lblTotalInitialDeposit">
                                </td>
                                <td width="120px" id="lblTotalLedgerBalance">
                                </td>
                                <td width="80px" id="lblTotalLosts">
                                </td>
                                <td width="120px" id="lblTotalLoanAmountReported">
                                </td>
                                <td width="120px" id="lblTotalReleaseLoanAmount">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
  <div id="div-AnnualPerBranchReport" class="form-group noPagerDiv scrollHorizontalContent">
                    <table id="tbl-AnnualPerBranchReport" class="table table-sm">
                        <thead class="noScrollFixedHeader">
                            <tr valign="middle" align="center">
                                <th align="center" valign="middle" width="150px" data-name="BranchName" data-alignment="left"
                                    data-columnname="BranchName">
                                    Branch Name
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
                                            <th align="center" valign="middle" width="70px" data-name="TotalCASA" data-alignment="right"
                                    data-columnname="TotalCASA" data-functions="viewCASA" class="isLink" data-pass="BranchCode,BranchName,TotalCASA" style="border-top: 0px solid #fff; border-left: 0px solid #fff; border-right: 0px solid #fff;">
                                                CASA
                                            </th>
                                            <th align="center" valign="middle" width="70px" data-name="TotalLoans" data-alignment="right"
                                    data-columnname="TotalLoans" data-functions="viewLoanProduct" class="isLink"
                                    data-pass="BranchCode,BranchName,TotalLoans" style="border-top: 0px solid #fff; border-left: 0px solid #fff; border-right: 0px solid #fff;">
                                                Loans
                                            </th>
                                        </tr>
                                    </table>
                                </td>
                                <th align="center" valign="middle" width="100px" data-name="TotalADB" data-alignment="right"
                                    data-columnname="TotalADB">
                                    ADB
                                </th>
                                <th align="center" valign="middle" width="120px" data-name="TotalInitialDeposit"
                                    data-alignment="right" data-columnname="TotalInitialDeposit">
                                    Initial Deposit
                                </th>
                                <th align="center" valign="middle" width="120px" data-name="TotalLedgerBalance" data-alignment="right"
                                    data-columnname="TotalLedgerBalance">
                                    Ledger Balance
                                </th>
                                <th align="center" valign="middle" width="80px" data-name="TotalLosts" data-alignment="right"
                                    data-columnname="TotalLosts">
                                    Losts
                                </th>
                                <th align="center" valign="middle" width="120px" data-name="LoanAmountReported" data-alignment="right"
                                    data-columnname="LoanAmountReported">
                                    Loan Amount Reported
                                </th>
                                <th align="center" valign="middle" width="120px" data-name="LoanReleaseAmount" data-alignment="right"
                                    data-columnname="LoanReleaseAmount">
                                    Release Loan Amount
                                </th>
                            </tr>
                        </thead>
                        <tbody id="tBody-AnnualPerBranchReport" class="noScrollContent border align-middle">
                        </tbody>
                        <tbody id="tBody1" class="noScrollContent">
                            <tr id="Tr1" valign="middle" align="right" style="background-color: #666666a1;
                                font-weight: bold;">
                                <td width="150px">
                                    Total
                                </td>
                                <td width="50px" id="lblTotalLeadsBranch">
                                </td>
                                <td width="60px" id="lblTotalSuspectsBranch">
                                </td>
                                <td width="60px" id="lblTotalProspectsBranch">
                                </td>
                                <td width="70px" id="lblTotalCASATypesBranch">
                                </td>
                                <td width="70px" id="lblTotalLoanProductsBranch">
                                </td>
                                <td width="100px" id="lblTotalADBBranch">
                                </td>
                                <td width="120px" id="lblTotalInitialDepositBranch">
                                </td>
                                <td width="120px" id="lblTotalLedgerBalanceBranch">
                                </td>
                                <td width="80px" id="lblTotalLostsBranch">
                                </td>
                                <td width="120px" id="lblTotalLoanAmountReportedBranch">
                                </td>
                                <td width="120px" id="lblTotalReleaseLoanAmountBranch">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>

                <div class="text-end" id="div-printReport">
                <a id="btnPrintReport" class="lbpControl btn-sm printButton right canPrint">Print Summary Table</a>
                <%--<a id="btnPrintSourceLeadSummary" class="lbpControl btn-sm printButton right canPrint">Source of Lead Summary</a>--%>
            </div>
            <div id="divReport" class="mt-3">
                <iframe name="ifrmReportViewer" width="100%" src="about:blank"></iframe>
            </div>
            </div>
            <div id="modalCASA" class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false"
                tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <h5 class="label-font-standard" id="lbl-Casa" style="text-align: center; font-size: 16px;">
                                </h5>
                            </div>
                            <div class="form-group">
                                <div id="div-CASATypeTable" class="noPagerDiv">
                                    <table id="tbl-CASATypeTable" class="table table-sm" align="center">
                                        <thead class="fixedHeader">
                                            <tr valign="middle" align="center">
                                                <th align="center" valign="middle" width="4%" data-name="RowCount" data-alignment="center"
                                                    data-columnname="RowCount">
                                                </th>
                                                <th align="center" valign="middle" width="25%" data-name="SalesProspect" data-alignment="left"
                                                    data-columnname="SalesProspectName">
                                                    Target Market Name
                                                </th>
                                                <th align="center" valign="middle" width="13%" data-name="DateEncoded" data-alignment="center"
                                                    data-columnname="DateEncoded">
                                                    Lead / Encoded Date
                                                </th>
                                                <th align="center" valign="middle" width="13%" data-name="CustomerDate" data-alignment="center"
                                                    data-columnname="CustomerDate">
                                                    Customer Date
                                                </th>
                                                <th align="center" valign="middle" width="13%" data-name="AccountNumber" data-alignment="center"
                                                    data-columnname="AccountNumber">
                                                    Account Number
                                                </th>
                                                <th align="center" valign="middle" width="14%" data-name="ADB" data-alignment="right"
                                                    data-columnname="ADB">
                                                    ADB
                                                </th>
                                                <th align="center" valign="middle" width="18%" data-name="CASAProductsAvailed" data-alignment="left"
                                                    data-columnname="CASAProduct">
                                                    CASA Products Availed
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody id="tBody-CASAType" class="scrollContent border align-middle">
                                        </tbody>
                                    </table>
                                    <div class="form-group">
                                        <h5 class="label-font-standard">
                                            NOTE : The table above shows the list of tarket market tagged as customer and
                                            availed any CASA product.
                                        </h5>
                                    </div>
                                </div>
                                <div id="div-ReportCASA" class="mt-3">
                                    <iframe name="ifrmReportViewer" width="100%" src="about:blank"></iframe>
                                </div>
                                <br />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button id="btn-PrintCASAReport" type="button" class="lbpControl printButton canPrint">
                                Print List of Clients</button>
                            <button id="btn-cancelC" type="button" class="lbpControl cancelButton">
                                Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
            <div id="modalLoan" class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false"
                tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header alert alert-lbp">
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <h5 class="label-font-standard" id="lbl-LoanHeader" style="text-align: center; font-size: 16px;">
                                </h5>
                            </div>
                            <div class="form-group">
                                <div id="div-LoanProductTable" class="noPagerDiv">
                                    <table id="tbl-LoanProductTable" class="table table-sm" align="center">
                                        <thead class="noScrollFixedHeader">
                                            <tr valign="middle" align="center">
                                                <th align="center" valign="middle" width="4%" data-name="RowCount" data-alignment="left"
                                                    data-columnname="RowCount">
                                                </th>
                                                <th align="center" valign="middle" width="30%" data-name="SalesProspect" data-alignment="left"
                                                    data-columnname="SalesProspect">
                                                    Target Market Name
                                                </th>
                                                <th align="center" valign="middle" width="18%" data-name="DateEncoded" data-alignment="center"
                                                    data-columnname="LeadDate">
                                                    Lead / Encoded Date
                                                </th>
                                                <th align="center" valign="middle" width="18%" data-name="CustomerDate" data-alignment="center"
                                                    data-columnname="CustomerDate">
                                                    Customer Date
                                                </th>
                                                <th align="center" valign="middle" width="30%" data-name="LoansProductAvailed" data-alignment="left"
                                                    data-columnname="LoansProductAvailed">
                                                    Loan Products Availed
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody id="tBody-loanProduct" class="noScrollContent border align-middle">
                                        </tbody>
                                    </table>
                                    <div class="form-group">
                                        <h5 class="label-font-standard">
                                            NOTE : The table above shows the list of target market tagged as customer and
                                            availed any Loan product.
                                        </h5>
                                    </div>
                                </div>
                                <div id="div-ReportLoan" class="mt-3">
                                    <iframe name="ifrmReportViewer" width="100%" src="about:blank"></iframe>
                                </div>
                                <br />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button id="btn-PrintLoanReport" type="button" class="lbpControl printButton canPrint">
                                Print List of Clients</button>
                            <button id="btn-cancelL" type="button" class="lbpControl cancelButton">
                                Cancel</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
