<%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master"
    CodeBehind="WeeklyActivityView.aspx.vb" Inherits="LBP.VS2010.BSMS.WeeklyActivityView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../../Content/Scripts/PageScripts/CPA/WeeklyActivityView.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="frmHorizontal" class="card p-3">
        <div class="accordion" id="accordionPanels">
            <div class="card p-3">
                <div class="row mb-3" id="divDateFilters">
                    <div class="col-sm-2">
                        <label class="col-sm-auto col-form-label label-font-standard">Select Date</label>
                    </div>
                    <div class="col-sm-2">
                        <select id="ddl-year" class="form-select form-select-sm required requiredField"></select>
                    </div>
                    <div class="col-sm-2">
                        <select id="ddl-month" class="form-select form-select-sm required requiredField"></select>
                    </div>
                    <div class="col-sm-2">
                        <select id="ddl-week" class="form-select form-select-sm required requiredField"></select>
                    </div>
                    <div class="text-end col-3">
                        <a id="btnSearch" class="lbpControl btn-sm searchButton right">Search</a>
                        <a id="btnReset" class="lbpControl btn-sm clearButton right">Reset</a>
                        <a id="btnBack" class="lbpControl btn-sm backButton right">Back</a>
                    </div>
                </div>
            </div>
            <br />
            <div class="card p-3" id="divWeeklySummaryReport">
                <h6 class="label-font-standard" id="lblReportHeader" style="font-size: 18px;"></h6>
                <div id="divNewLeadsAccordion" data-bs-toggle="collapse" data-bs-target="#collapseOne">
                    <div class="card-header btn-lbp-green text-center rounded-0" id="divNewLeadsAccordionHeader">
                        <h6 class="mb-0">New Leads</h6>
                    </div>
                </div>
                <div id="collapseOne" class="accordion-collapse show" aria-labelledby="headingOne">
                    <div class="accordion-body">
                        <div id="divNewLeads" class="form-group pagerDiv scrollHorizontalContent">
                            <table id="tblNewLeads" class="table table-sm" align="center">
                                <thead class="fixedHeader">
                                    <tr valign="middle" align="center">
                                        <th align="center" valign="middle" width="150px" data-name="Fullname" data-alignment="left"
                                            data-columnname=""  class="isLink" data-functions="view" data-pass="ClientID">
                                            Client's Full Name</th>
                                        <th align="center" valign="middle" width="120px" data-name="ClientType" data-alignment="left"
                                            data-columnname="ClientType">Client Type</th>
                                        <th align="center" valign="middle" width="120px" data-name="Lead" data-alignment="left"
                                            data-columnname="Lead">Lead</th>
                                        <th align="center" valign="middle" width="120px" data-name="Suspect" data-alignment="left"
                                            data-columnname="Suspect">Suspect</th>
                                        <th align="center" valign="middle" width="120px" data-name="Prospect" data-alignment="left"
                                            data-columnname="Prospect">Prospect</th>
                                        <th align="center" valign="middle" width="120px" data-name="Customer" data-alignment="left"
                                            data-columnname="Customer">Customer</th>
                                        <th align="center" valign="middle" width="150px" data-name="CASATypes" data-alignment="left"
                                            data-columnname="CASATypes">CASA Product Availed</th>
                                        <th align="center" valign="middle" width="120px" data-name="Amount" data-alignment="right"
                                            data-columnname="Amount">Amount</th>
                                        <th align="center" valign="middle" width="120px" data-name="ADB" data-alignment="right"
                                            data-columnname="ADB">ADB</th>
                                        <th align="center" valign="middle" width="120px" data-name="LoansProductsAvailed" data-alignment="left"
                                            data-columnname="LoansProductsAvailed">Loan Products Availed</th>
                                        <th align="center" valign="middle" width="120px" data-name="LoanAmountReported" data-alignment="left" 
                                            data-columnname="LoanAmountReported">Loan Amount Reported</th>
                                        <th align="center" valign="middle" width="120px" data-name="Lost" data-alignment="left" 
                                            data-columnname="Lost">Lost</th>
                                        <th align="center" valign="middle" width="150px" data-name="Reason" data-alignment="left"
                                            data-columnname="Reason">Reason</th>
                                        <th align="center" valign="middle" width="150px" data-name="ProductsOffered" data-alignment="left"
                                            data-columnname="ProductsOffered">Products Offered</th>
                                        <th align="center" valign="middle" width="150px" data-name="OtherATypes" data-alignment="left"
                                            data-columnname="OtherATypes">Products & Services Availed</th>
                                        <th align="center" valign="middle" width="120px" data-name="AmountOthers" data-alignment="right"
                                            data-columnname="AmountOthers">Inital Deposit (Products & Services)</th>
                                        <th align="center" valign="middle" width="60px" data-name="Visits" data-alignment="right"
                                            data-columnname="Visits">No. Of Visits</th>
                                        <th align="center" valign="middle" width="120px" data-name="AccountNumbers" data-alignment="right"
                                            data-columnname="AccountNumbers">Account Number</th>
                                        <th align="center" valign="middle" width="120px" data-name="LeadSource" data-alignment="left"
                                            data-columnname="LeadSource">Source Of Lead</th>
                                        <th align="center" valign="middle" width="150px" data-name="Industry" data-alignment="left"
                                            data-columnname="Industry">Industry</th>
                                    </tr>
                                </thead>
                                <tbody id="tBodyNewLeads" class="noScrollContent border align-middle">
                                </tbody>
                                <tbody id="tBodyNewLeadsTotal" class="noScrollContent" hidden>
                                    <tr id="totalLeads" valign="middle" align="center" style="background-color: #666666a1;
                                        font-weight: bold;">
                                        <td width="150px" align="left">Total Leads</td>
                                        <td width="120px"></td>
                                        <td width="120px" id="lblTotalLead" align="right"></td>
                                        <td width="120px"></td>
                                        <td width="120px"></td>
                                        <td width="120px"></td>`
                                        <td width="150px"></td>
                                        <td width="120px"></td>
                                        <td width="120px"></td>
                                        <td width="120px"></td>
                                        <td width="120px"></td>
                                        <td width="120px"></td>
                                        <td width="150px"></td>
                                        <td width="150px"></td>
                                        <td width="150px"></td>
                                        <td width="120px"></td>
                                        <td width="60px"></td>
                                        <td width="120px"></td>
                                        <td width="120px"></td>
                                        <td width="150px"></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="text-end">
                            <a id="btnPrintNewLeads" class="lbpControl btn-sm printButton right">Print New Leads Report</a>
                        </div>
                        <div id="divNewLeadReport">
                            <iframe name="ifrmReportViewer_Leads" width="100%" src="about:blank"></iframe>
                        </div>
                    </div>
                </div>

                <div id="divCPARevisitsAccordion" data-bs-toggle="collapse" data-bs-target="#collapseTwo">
                    <div class="card-header btn-lbp-green text-center rounded-0" id="Div2">
                        <h6 class="mb-0">Potential Account Visits</h6>
                    </div>
                </div>
                <div id="collapseTwo" class="accordion-collapse show" aria-labelledby="headingOne">
                    <div class="accordion-body">
                        <div id="divPotentialAccounts" class="form-group pagerDiv scrollHorizontalContent">
                            <table id="tblPotentialAccounts" class="table table-sm" align="center">
                                <thead class="fixedHeader">
                                    <tr valign="middle" align="center">
                                        <th align="center" valign="middle" width="150px" data-name="Fullname" data-alignment="left"
                                            data-columnname=""  class="isLink" data-functions="view" data-pass="ClientID">
                                            Client's Full Name</th>
                                        <th align="center" valign="middle" width="120px" data-name="ClientType" data-alignment="left"
                                            data-columnname="ClientType">Client Type</th>
                                        <th align="center" valign="middle" width="120px" data-name="Lead" data-alignment="left"
                                            data-columnname="Lead">Lead</th>
                                        <th align="center" valign="middle" width="120px" data-name="Suspect" data-alignment="left"
                                            data-columnname="Suspect">Suspect</th>
                                        <th align="center" valign="middle" width="120px" data-name="Prospect" data-alignment="left"
                                            data-columnname="Prospect">Prospect</th>
                                        <th align="center" valign="middle" width="120px" data-name="Customer" data-alignment="left"
                                            data-columnname="Customer">Customer</th>
                                        <th align="center" valign="middle" width="150px" data-name="CASATypes" data-alignment="left"
                                            data-columnname="CASATypes">CASA Product Availed</th>
                                        <th align="center" valign="middle" width="120px" data-name="Amount" data-alignment="right"
                                            data-columnname="Amount">Amount</th>
                                        <th align="center" valign="middle" width="120px" data-name="ADB" data-alignment="right"
                                            data-columnname="ADB">ADB</th>
                                        <th align="center" valign="middle" width="120px" data-name="LoansProductsAvailed" data-alignment="left"
                                            data-columnname="LoansProductsAvailed">Loan Products Availed</th>
                                        <th align="center" valign="middle" width="120px" data-name="LoanAmountReported" data-alignment="left" 
                                            data-columnname="LoanAmountReported">Loan Amount Reported</th>
                                        <th align="center" valign="middle" width="120px" data-name="Lost" data-alignment="left" 
                                            data-columnname="Lost">Lost</th>
                                        <th align="center" valign="middle" width="150px" data-name="Reason" data-alignment="left"
                                            data-columnname="Reason">Reason</th>
                                        <th align="center" valign="middle" width="150px" data-name="ProductsOffered" data-alignment="left"
                                            data-columnname="ProductsOffered">Products Offered</th>
                                        <th align="center" valign="middle" width="150px" data-name="OtherATypes" data-alignment="left"
                                            data-columnname="OtherATypes">Products & Services Availed</th>
                                        <th align="center" valign="middle" width="120px" data-name="AmountOthers" data-alignment="right"
                                            data-columnname="AmountOthers">Inital Deposit (Products & Services)</th>
                                        <th align="center" valign="middle" width="60px" data-name="Visits" data-alignment="right"
                                            data-columnname="Visits">No. Of Visits</th>
                                        <th align="center" valign="middle" width="120px" data-name="AccountNumbers" data-alignment="right"
                                            data-columnname="AccountNumbers">Account Number</th>
                                        <th align="center" valign="middle" width="120px" data-name="LeadSource" data-alignment="left"
                                            data-columnname="LeadSource">Source Of Lead</th>
                                        <th align="center" valign="middle" width="150px" data-name="Industry" data-alignment="left"
                                            data-columnname="Industry">Industry</th>
                                    </tr>
                                </thead>
                                <tbody id="tBodyPotentialAccounts" class="noScrollContent border align-middle">
                                </tbody>
                                <tbody id="tBodyPotentialAccountsTotal" class="noScrollContent" hidden>
                                    <tr valign="middle" align="center" style="background-color: #666666a1;
                                        font-weight: bold;">
                                        <td width="150px">Total Revisits</td>
                                        <td width="120px"></td>
                                        <td width="120px"></td>
                                        <td width="120px" id="lblSuspect" align="right"></td>
                                        <td width="120px" id="lblProspect" align="right"></td>
                                        <td width="120px" id="lblCustomer" align="right"></td>
                                        <td width="150px"></td>
                                        <td width="120px"></td>
                                        <td width="120px"></td>
                                        <td width="120px"></td>
                                        <td width="120px"></td>
                                        <td width="120px"></td>
                                        <td width="150px"></td>
                                        <td width="150px"></td>
                                        <td width="150px"></td>
                                        <td width="120px"></td>
                                        <td width="60px"></td>
                                        <td width="120px"></td>
                                        <td width="120px"></td>
                                        <td width="150px"></td>
                                </tbody>
                            </table>
                        </div>
                        <div class="text-end">
                            <a id="btnPrintPotentialAccounts" class="lbpControl btn-sm printButton right">Print Potential Account Report</a>
                        </div>
                        <div id="divPARevisitReport" class="mt-3">
                            <iframe name="ifrmReportViewer_PARevisit" width="100%" src="about:blank"></iframe>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    <input type="hidden" id="hdnError" runat="server"/>
    </div>
</asp:Content>
