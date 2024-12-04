<%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master" CodeBehind="ChangeInPotentialAccounts.aspx.vb" Inherits="LBP.VS2010.BSMS.ChangeInPotentialAccounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="../../Content/Scripts/PageScripts/CPA/ChangeInPotentialAccounts.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="frmHorizontal" class="card p-3">
        <div class="accordion" id="accordionPanelsStayOpenExample1">
            <div class="card p-3">
                <div class="row mb-sm-3" id="div-region">
                    <label class="col-sm-2 col-form-label label-font-standard" for="ddl-region">
                        Group
                    </label>
                    <div class="col-sm-5" id="div-region">
                        <select id="ddl-region" class="form-select form-select-sm">
                        </select>
                    </div>
                </div>
                <div class="row mb-sm-3">
                     <label class="col-sm-2 col-form-label label-font-standard" for="ddl-branch">
                        Branch</label>
                    <div class="col-sm-4">
                        <select id="ddl-branch" class="form-select form-select-sm">
                        </select>
                    </div>
                   <%-- <label class="col-2 col-form-label label-font-standard" for="ddl-userFullName">
                        Branch Head
                    </label>
                    <div class="col-sm-3">
                        <select id="ddl-userFullName" class="form-select form-select-sm">
                        </select>
                    </div>--%>
                </div>
                <div class="row mb-3" id="divDateFilters">
                    <div class="col-sm-2">
                        <label class="col-sm-auto col-form-label label-font-standard">
                            Select Date
                        </label>
                    </div>
                    <div class="col-sm-2">
                        <select id="ddl-year" class="form-select form-select-sm">
                        </select>
                    </div>
                    <div class="col-sm-2">
                        <select id="ddl-Month" class="form-select form-select-sm">
                        </select>
                    </div>
                    <div class="col-sm-2">
                        <select id="ddl-week" class="form-select form-select-sm">
                        </select>
                    </div>
                    <div class="text-end col-3">
                        <a id="btn-ViewTables" class="lbpControl btn-sm searchButton right">Search </a>
                        <a id="btn-ResetFilters" class="lbpControl btn-sm clearButton right">Reset </a>
                    </div>
                </div>
            </div>
            <br />
            <div class="card p-3" id="divChangesInPotentialAccounts">
                <h6 class="label-font-standard" id="lblReportHeader" style="font-size: 18px;">Change in Potential Accounts
                </h6>

                <div id="divPotentialAccounts" class="form-group pagerDiv scrollHorizontalContent">
                    <table id="tblPotentialAccounts" class="table table-sm" align="center">
                        <thead class="noScrollFixedHeader">
                            <tr valign="middle" align="center">
                                <th align="center" valign="middle" width="200px" data-name="PotentialAccountName" data-alignment="left"
                                    data-columnname="PotentialAccountName">Potential Account Name
                                </th>
                                <th align="center" valign="middle" width="200px" data-name="Industry" data-alignment="left"
                                    data-columnname="Industry">Industry
                                </th>
                                <th align="center" valign="middle" width="100px" data-name="ActionTaken" data-alignment="center"
                                    data-columnname="ActionTaken">Action Taken
                                </th>
                                <th align="center" valign="middle" width="150px" data-name="ValueBefore" data-alignment="center"
                                    data-columnname="ValueBefore">Value Before
                                </th>
                                <th align="center" valign="middle" width="150px" data-name="ValueAfter" data-alignment="center"
                                    data-columnname="ValueAfter">Value After
                                </th>
                                <th align="center" valign="middle" width="100px" data-name="ChangeDate" data-alignment="center"
                                    data-columnname="ChangeDate">Change Date
                                </th>
                                <th align="center" valign="middle" width="200px" data-name="Username" data-alignment="center"
                                    data-columnname="Username">Branch Head
                                </th>
                            </tr>
                        </thead>
                        <tbody id="tBodyPotentialAccounts" class="noScrollContent border align-middle">
                        </tbody>
                    </table>
                </div>
                <div class="text-end">
                    <a id="btn-Print" class="lbpControl btn-sm printButton right">Print Change In Potential Accounts Report</a>
                </div>
                 <div id="divReport" class="mt-3" hidden>
                            <iframe name="ifrmReportViewer" width="100%" height="500px" src="about:blank"></iframe>
                        </div>
            </div>
        </div>
    </div>
</asp:Content>
