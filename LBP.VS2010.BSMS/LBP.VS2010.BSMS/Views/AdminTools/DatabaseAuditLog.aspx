<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master"
    CodeBehind="DatabaseAuditLog.aspx.vb" Inherits="LBP.VS2010.BSMS.AuditLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Content/Scripts/PageScripts/AdminTools/DatabaseAuditLog.js" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            width: 21%;
        }
        .auto-style2 {
            width: 11%;
        }
        .auto-style3 {
            width: 12%;
        }
        .auto-style4 {
            width: 16%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="frmHorizontal" class="well">
        <div id="divSearch">
            <div class="mb-sm-3 row">
                <label class="col-sm-1 col-form-label label-font-standard" for="txtLastName">
                    Search By
                </label>
                <div class="col-sm-4">
                    <select id="ddlSearchBy" class="form-select form-select-sm hardcodedSelect">
                        <option value="All">All</option>
                        <option value="UserID">User ID</option>
                        <option value="TableName">Table Name</option>
                        <option value="ActionType">Action Type</option>
                        <option value="AuditTrailDate">Date</option>
                    </select>
                </div>
                <div id="divSearchValue" class="col-4">
                    <input type="text" id="txtSearchValue" class="form-control form-control-sm required" autocomplete="off" />
                </div>
                <div class="col-sm-auto">
                    <a id="btnSearch" class="lbpControl btn-sm searchButton">Search</a>
                </div>
            </div>
        </div>
        <div id="divAuditTrail" class="form-group pagerDiv">
            <table id="tblAuditTrail" class="table table-sm" align="center">
                <thead class="noScrollFixedHeader">
                    <tr valign="middle" align="center">
                        <th align="center" valign="middle" width="12%" data-name="UserID" data-alignment="center"
                            data-columnname="User ID">User ID
                        </th>
                        <th align="center" valign="middle" data-name="AuditTrailDate" data-alignment="center"
                            data-columnname="Audit Trail Date">Audit Trail Date
                        </th>
                        <th align="center" valign="middle" data-name="TableName" data-alignment="center"
                            data-columnname="Table Name">Table Name
                        </th>
                        <th align="center" valign="middle" data-name="ColumnAffected" data-alignment="center"
                            data-columnname="Column Affected">Column Affected
                        </th>
                        <th align="center" valign="middle" data-name="ActionType" data-alignment="center"
                            data-columnname="Action Type">Action Type
                        </th>
                        <th align="center" valign="middle" data-name="ColumnFrom" data-alignment="center"
                            data-columnname="Column From">Column From
                        </th>
                        <th align="center" valign="middle" data-name="ColumnTo" data-alignment="center"
                            data-columnname="Column To">Column To
                        </th>
                    </tr>
                </thead>
                <tbody id="tBodyAuditTrail" class="noScrollContent border align-middle">
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
