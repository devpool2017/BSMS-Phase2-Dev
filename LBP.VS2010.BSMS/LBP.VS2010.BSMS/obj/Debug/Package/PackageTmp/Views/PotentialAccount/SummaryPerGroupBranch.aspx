<%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master" CodeBehind="SummaryPerGroupBranch.aspx.vb" Inherits="LBP.VS2010.BSMS.SummaryPerGroupBranch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Content/Scripts/PageScripts/CPA/SummaryPerGroupBranch.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <input type="hidden" id="hdnError" runat="server"/>
    <div id="frmHorizontal" class="card p-3">
        <div class="card p-3">
            <div class="row mb-sm-3">
                <label class="col-sm-2 col-form-label label-font-standard">Year</label>
                <div class="col-sm-2">
                    <select id="ddl-year" class="form-select form-select-sm required requiredField"></select>
                </div>
            </div>
            <div class="row mb-sm-3">
                <label class="col-sm-2 col-form-label label-font-standard" for="ddl-industryType">Industry Type</label>
                <div class="col-sm-4">
                    <select id="ddl-industryType" class="form-select form-select-sm required requiredField"></select>
                </div>
            </div>
            <div class="row mb-sm-12">
                <label class="col-sm-2 col-form-label label-font-standard" for="ddl-group">Group</label>
                <div class="col-sm-3">
                    <select id="ddl-group" class="form-select form-select-sm required requiredField"></select>
                </div>
                <div class="col-3 offset-1">
                    <a id="btnSearch" class="lbpControl btn-sm searchButton right">Search</a>
                    <a id="btnReset" class="lbpControl btn-sm clearButton right">Reset</a>
                </div>
            </div>
        </div>
        <br />
        <div class="card p-3" id="divTblSummaryPerGroupBranch" hidden>
            <h6 class="label-font-standard" id="lblReportHeader" style="font-size: 18px;">Potential Account Summary per Group</h6>
            <div id="div-SummaryPerGroupBranchReport" class="form-group noPagerDiv">
                <table id="tbl-SummaryPerGroupBranchReport" class="table table-sm">
                    <thead class="noScrollFixedHeader">
                        <tr valign="middle" align="center">
                            <th align="center" valign="middle" width="15%" data-name="BranchCode" data-alignment="left"
                                data-columnname="BranchCode">Branch Code</th>
                            <th align="center" valign="middle" width="30%" data-name="BranchName" data-alignment="left"
                                data-columnname="BranchName">Branch Name</th>
                            <th align="center" valign="middle" width="20%" data-name="TotalCPACount" data-alignment="right"
                                data-columnname="TotalCPACount">Total Potential Account</th>
                            <th align="center" valign="middle" width="20%" data-name="TotalLeads" data-alignment="right"
                                data-columnname="TotalLeads">Tagged as Leads</th>
                        </tr>
                    </thead>
                    <tbody id="tBody-SummaryPerGroupBranchReport" class="noScrollContent border align-middle">
                    </tbody>
                    <tbody id="tBody-SummaryReportTotal" class="noScrollContent">
                        <tr id="Tr1" valign="middle" align="right" style="background-color: #666666a1;
                            font-weight: bold;">
                            <td width="15%"></td>
                            <td width="30%" align="left">Total</td>
                            <td width="20%" id="lblTotalCPACount"></td>
                            <td width="20%" id="lblTotalLeads"></td>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div id="divReportDetails">
                <div id="divBottomButton" class="text-end">
                    <a id="btnPrintReport" class="lbpControl btn-sm printButton right canPrint">Print Summary Table</a>
                </div>
                <div id="divReport" class="mt-3" hidden>
                    <iframe name="ifrmReportViewer" width="100%" src="about:blank"></iframe>
                </div>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
