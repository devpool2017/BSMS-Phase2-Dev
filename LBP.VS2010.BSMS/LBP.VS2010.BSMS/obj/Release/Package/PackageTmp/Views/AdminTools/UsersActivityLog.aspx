<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master" CodeBehind="UsersActivityLog.aspx.vb" Inherits="LBP.VS2010.BSMS.UsersActivityLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Content/Scripts/PageScripts/AdminTools/UsersActivityLog.js" type="text/javascript"></script>
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
                        <option value="ModuleName">Module Name</option>
                        <option value="ActivityType">Activity Type</option>
                        <option value="UserName">UserName</option>
                        <option value="ActivityDate">Activity Date</option>
                        <option value="Browser">Browser</option>
                    </select>
                </div>
                <div id="divSearchValue" class="col-sm-4">
                    <input type="text" id="txtSearchValue" class="lbpControl form-control form-control-sm required" autocomplete="off" />
                </div>
                <div class="col-sm-auto">
                    <a id="btnSearch" class="lbpControl btn-sm searchButton">Search</a>
                </div>
            </div>
        </div>
        <div id="divUserLog" class="form-group pagerDiv">
            <table id="tblUserLog" class="table table-sm col-sm-12" align="center">
                <thead class="noScrollFixedHeader">
                    <tr valign="middle" align="center">
                        <th align="center" valign="middle" width="15%" data-name="UserName" data-alignment="center"
                            data-columnname="User Name">UserName
                        </th>
                        <th align="center" valign="middle" width="20%" data-name="ModuleName" data-alignment="center"
                            data-columnname="Module Name">Module Name
                        </th>
                        <th align="center" valign="middle" width="10%" data-name="ActivityType" data-alignment="center"
                            data-columnname="Activity Type">Activity Type
                        </th>
                        <th align="center" valign="middle" width="20%" data-name="ActivityDate" data-alignment="center"
                            data-columnname="ActivityDate">Activity Date
                        </th>
                        <th align="center" valign="middle" width="10%" data-name="IPAddress" data-alignment="center"
                            data-columnname="IPAddress">IPAddress
                        </th>
                        <th align="center" valign="middle" width="20%" data-name="Browser" data-alignment="center"
                            data-columnname="Browser">Browser
                        </th>
                    </tr>
                </thead>
                <tbody id="tBodyUserLog" class="noScrollContent border align-middle">
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
