﻿<%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master"
    CodeBehind="AnnuallySummary.aspx.vb" Inherits="LBP.VS2010.BSMS.AnnuallySummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Content/Scripts/PageScripts/Reports/ConversionSummaryReport/AnnuallySummaryReport.js"
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
                            Select Year
                        </label>
                    </div>
                    <div class="col-sm-2">
                        <select id="ddl-year" class="form-select form-select-sm required requiredField">
                        </select>
                    </div>
                    <div class="text-end col-3">
                        <a id="btn-ViewTables" class="lbpControl btn-sm searchButton right">Search </a><a
                            id="btn-ResetFilters" class="lbpControl btn-sm clearButton right">Reset </a>
                    </div>
                </div>
            </div>
            <br />
            <div class="card p-3" id="divAnnualSummaryReport" hidden>
                <h6 class="label-font-standard" id="lblReportHeader" style="font-size: 18px;">
                    Annual Summary Report of Branch Head
                </h6>
                <div id="div-AnnuallyReport" class="form-group noPagerDiv">
                    <table id="tbl-AnnuallyReport" class="table table-sm">
                        <thead class="fixedHeader">
                            <tr valign="middle" align="center">
                                <th align="center" valign="middle" width="15%" data-name="Month" data-alignment="center"
                                    data-columnname="Month">
                                    Month
                                </th>
                                <th align="center" valign="middle" width="15%" data-name="WeekNumber" data-alignment="center"
                                    data-columnname="WeekNumber">
                                    Week
                                </th>
                                <th align="center" valign="middle" width="15%" data-name="Lead" data-alignment="center"
                                    data-columnname="Lead">
                                    Leads
                                </th>
                                <th align="center" valign="middle" width="15%" data-name="Suspect" data-alignment="center"
                                    data-columnname="Suspect">
                                    Suspects
                                </th>
                                <th align="center" valign="middle" width="15%" data-name="Prospect" data-alignment="center"
                                    data-columnname="Prospect">
                                    Prospects
                                </th>
                                <td align="center" valign="middle" width="30%" style="background: #666666; border-top: 1px solid #fff;
                                    color: #fff; font-size: 12px;">
                                    New Customers
                                    <table>
                                        <tr>
                                            <th align="center" valign="middle" width="15%" data-name="NewCASA" data-alignment="center"
                                                data-columnname="NewCASA" style="border-top: 0px solid #fff; border-left: 0px solid #fff;
                                                border-right: 0px solid #fff;">
                                                CASA
                                            </th>
                                            <th align="center" valign="middle" width="15%" data-name="NewLoans" data-alignment="center"
                                                data-columnname="NewLoans" style="border-top: 0px solid #fff; border-left: 0px solid #fff;
                                                border-right: 0px solid #fff;">
                                                Loans
                                            </th>
                                        </tr>
                                    </table>
                                </td>
                                <th align="center" valign="middle" width="15%" data-name="Lost" data-alignment="center"
                                    data-columnname="Losts">
                                    Losts
                                </th>
                            </tr>
                        </thead>
                        <tbody id="tBody-AnnuallyReport" class="scrollContent border align-middle">
                        </tbody>
                        <tbody id="tBody-AnnuallyReportTotal" class="noScrollContent">
                            <tr id="totalLeads" valign="middle" align="center" style="background-color: #666666a1;
                                font-weight: bold;">
                                <td width="15%">
                                    Annual Total
                                </td>
                                <td width="15%">
                                </td>
                                <td width="15%" id="lblTotalLead">
                                </td>
                                <td width="15%" id="lblTotalSuspect">
                                </td>
                                <td width="15%" id="lblTotalProspect">
                                </td>
                                <td width="15%" id="lblNewCASA">
                                </td>
                                <td width="15%" id="lblNewLoans">
                                </td>
                                <td width="15%" id="lblTotalLost">
                                </td>
                                <td width="2%" id="Td1">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <br />
                <div class="text-end">
                    <a id="btn-PrintReport" class="lbpControl btn-sm printButton right canPrint">Print Report</a>
                </div>
                <div id="divReport" class="mt-3" hidden>
                    <iframe name="ifrmReportViewer" width="100%" src="about:blank"></iframe>
                </div>
                <br />
                <table align="left" width="500px" id="tblOthers">
                    <tr>
                        <td align="left">
                            <table id="tbl-of-Annuallyratios" class="label-font-standard" cellpadding="2" cellspacing="2"
                                bgcolor="#ffffff" frame="box" width="400px" align="center">
                                <tr>
                                    <td colspan="2" align="center">
                                        ANNUAL PROSPECTING RATIO
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Target Leads
                                    </td>
                                    <td width="100px" id="lblTargetLeads">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Actual Leads Generated
                                    </td>
                                    <td width="100px" id="lblActualLeads">
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
                                        ANNUAL CONVERSION PERCENTAGES
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Total Leads to Total Suspect
                                    </td>
                                    <td id="lblLeadsToSuspect">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Total Suspect to Total Prospect
                                    </td>
                                    <td id="lblSuspectToProspect">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Total Prospect to Total Customer
                                    </td>
                                    <td id="lblProspectToCustomer">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Target New Account Closed
                                    </td>
                                    <td id="lblTargetNewAccountClosed">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Actual New Account Closed
                                    </td>
                                    <td id="lblActualNewAccountClosed">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Actual versus Target
                                    </td>
                                    <td id="lblActualVSTarget">
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
                                        Total Lost Sales Ratio
                                    </td>
                                    <td id="lblLostSalesRatio">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        ANNUAL ADB RATIO
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Target ADB
                                    </td>
                                    <td width="100px" id="lblTargetADB">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Actual ADB Generated
                                    </td>
                                    <td width="100px" id="lblActualADB">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Total ADB Generated Versus Target
                                    </td>
                                    <td width="100px" id="lblTotalADB">
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
