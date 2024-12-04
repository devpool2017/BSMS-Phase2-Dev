<%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master" 
    CodeBehind="WeeklyActivity.aspx.vb" Inherits="LBP.VS2010.BSMS.WeeklyActivity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Content/Scripts/PageScripts/CPA/WeeklyActivity.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="frmHorizontal" class="card p-3">
        <div class="accordion" id="accordionPanelsStayOpenExample1">
            <div class="card p-3">
            <div class="row mb-sm-3" id="div-group">
                    <label class="col-sm-2 col-form-label label-font-standard" for="ddl-group">Group</label>
                    <div class="col-sm-3">
                        <select id="ddl-group" class="form-select form-select-sm required requiredField"></select>
                    </div>
                </div>
                <div class="row mb-sm-4">
                    <label class="col-sm-2 col-form-label label-font-standard" for="ddl-branch">Branch</label>
                    <div class="col-sm-4">
                        <select id="ddl-branch" class="form-select form-select-sm required requiredField"></select>
                    </div>
                    <label class="col-sm-2 col-form-label label-font-standard" for="ddl-BranchHead">Branch Head</label>
                    <div class="col-sm-3">
                        <select id="ddl-BranchHead" class="form-select form-select-sm required requiredField"></select>
                    </div>
                </div>
                <div class="row mb-3" id="divDateFilters">
                    <div class="col-sm-2">
                        <label class="col-sm-auto col-form-label label-font-standard">Select Year</label>
                    </div>
                    <div class="col-sm-2">
                        <select id="ddl-year" class="form-select form-select-sm required requiredField"></select>
                    </div>
                    <div class="offset-1 col-3">
                        <a id="btnSearch" class="lbpControl btn-sm searchButton right">Search</a>
                        <a id="btnReset" class="lbpControl btn-sm clearButton right">Reset</a>
                    </div>
                </div>
            </div>
            <br />
            <div class="card p-3" id="divTblWeeklyActivity" hidden>
                <h6 class="label-font-standard" id="lblReportHeader" style="font-size: 18px;"></h6>
                <div id="div-WeeklyActivity" class="noPagerDiv">
                    <table id="tbl-WeeklyActivity" class="table table-sm">
                        <thead class="noScrollFixedHeader">
                            <tr valign="middle" align="center">
                                <th align="center" valign="middle" width="15%" data-name="Month" data-alignment="left"
                                    data-columnname="Month">Month</th>
                                <th align="center" valign="middle" width="15%" data-name="Week" data-alignment="left"
                                    data-columnname="Week" data-functions="view" class="isLink" 
                                    data-pass="MonthCode,WeekNumber">Week</th>
                                <th align="center" valign="middle" width="15%" data-name="Lead" data-alignment="right"
                                    data-columnname="Lead">Lead</th>
                                <th align="center" valign="middle" width="15%" data-name="Revisit" data-alignment="right"
                                    data-columnname="Revisits">Revisits</th>
                                <th align="center" valign="middle" width="15%" data-name="Total" data-alignment="right"
                                    data-columnname="Total">Total</th>
                            </tr>
                        </thead>
                        <tbody id="tBody-WeeklyActivity" class="noScrollContent border align-middle">
                        </tbody>
                        <tbody id="tBodyWeeklyActivityTotal" class="noScrollContent">
                            <tr id="totalLeads" valign="middle" align="center" style="background-color: #666666a1; font-weight: bold;" class="">
                                <td width="15%" align="left">Annual Total</td>
                                <td width="15%"></td>
                                <td width="15%" id="lblTotalLead" align="right"></td>
                                <td width="15%" id="lblTotalRevisit" align="right"></td>
                                <td width="15%" id="lblGrandTotal" align="right"></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="hdnError" runat="server"/>
</asp:Content>
