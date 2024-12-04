<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master" CodeBehind="OtherTables.aspx.vb" Inherits="LBP.VS2010.BSMS.OtherTables" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <script src="/Content/Scripts/PageScripts/Review/OtherTables.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<input type="hidden" id="hdnSession" data-value="<%=Session("LogonUser")%>" />
<div id="frmHorizontal" class="well">
    <div class="row mb-2">
        <label for="ddlTable" class="col-sm-4 col-form-label label-font-standard">
            Other Tables:
        </label>
        <div class="col-sm-4">
            <select id="ddlTable" class="form-select hardcodedSelect">
                <option value="ClientTypes">CLIENT TYPES</option>
                <option value="LeadSource">SOURCE OF LEADS</option>
                <option value="LostReason">LOST REASONS</option>
                <option value="IndustryType">INDUSTRY FLAG</option>
                <option value="ADBSizingRange">ADB</option>
            </select>
        </div>
        <div class="col-sm-auto">
            <a id="btnSearch" class="lbpControl btn-sm searchButton">Search</a>
        </div>
    </div>
    <div id="divTableList" class="form-group pagerDiv" hidden>
        <table id="tblTableList" class="table table-sm" align="center">
            <thead class="noScrollFixedHeader">
                <tr valign="middle" align="center">
                    <th align="center" valign="middle" data-name="TableColumn" data-alignment="center"
                        data-columnname="TableColumn" hidden>
                    </th>
                </tr>
            </thead>
            <tbody id="tBodyTableList" class="noScrollContent border align-middle">
            </tbody>
        </table>
    </div>
</div>

</asp:Content>
