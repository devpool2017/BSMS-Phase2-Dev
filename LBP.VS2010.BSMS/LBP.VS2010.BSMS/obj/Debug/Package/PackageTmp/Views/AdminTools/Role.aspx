<%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master" CodeBehind="Role.aspx.vb" Inherits="LBP.VS2010.BSMS.Role" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Content/Scripts/PageScripts/AdminTools/Role.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card p-3">
        <div class="accordion" id="acc-Main">
            <div id="acc-App" class="accordion-item">
                <div data-bs-toggle="collapse" data-bs-target="#pnlOne">
                    <div class="card-header btn-lbp-green text-center rounded-0">
                        <h6 class="mb-0">Active/Deactivated
                        </h6>
                    </div>
                </div>

                <div id="pnlOne" class="accordion-collapse collapse show" aria-labelledby="panelOneOpen">
                    <div class="accordion-body">
                        <div class="row mb-2">
                            <label for="txtRole" class="col-sm-4 col-form-label label-font-standard">Role Name/ Role Description: </label>
                            <div class="col-sm-4">
                                <input id="txtRole" type="text" class="form-control form-control-sm" aria-label="RoleFilter" maxlength='<%#getFieldLength(FieldVariables.SEARCH_TEXT, LengthSetting.MAX)%>' />
                            </div>
                             
                            <div class="col-sm-auto">
                                <a id="btnSearch" class="lbpControl btn-sm searchButton">Search</a>
                                <a id="btnReset" class="lbpControl btn-sm clearButton">Reset</a>
                            </div>
                        </div>
                         <div class="row mb-2">
                                <label for="ddl-s-f" class="col-sm-4 col-form-label label-font-standard">
                                    Filter by Status:
                                </label>
                                <div class="col-sm-4">
                                    <select id="ddlStat" class="form-select form-select-sm hardcodedSelect">
                                        <option value="">All</option>
                                        <option value="1">Active</option>
                                        <option value="3">Deactivated</option>

                                    </select>
                                </div>
                            </div>

                        <div id="divRole" class="form-group pagerDiv">
                            <table id="tblRole" class="table table-sm" align="center">
                                <thead class="noScrollFixedHeader">
                                    <tr valign="middle" align="center">
                                        <th align="center" valign="middle" width="30%" data-name="" data-functions="view,edit,deactivate,activate" data-access="CanView.Access,CanUpdate.Access,CanDelete.Access,CanActivate.Access"
                                            data-alignment="center" class="hasButtons" data-pass="RoleId"></th>

                                        <th align="center" valign="middle" width="25%" data-name="RoleName" data-alignment="center"
                                            data-columnname="Role Name">Role Name
                                        </th>

                                        <th align="center" valign="middle" width="30%" data-name="RoleDescription" data-alignment="center"
                                            data-columnname="Role Description">Role Description
                                        </th>


                                        <th align="center" valign="middle" width="15%" data-name="Status" data-alignment="center"
                                            data-columnname="Status">Status
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="tBodyRole" class="noScrollContent border align-middle">
                                </tbody>
                            </table>
                        </div>

                        <div class="text-end">
                            <a id="btnAdd" class="lbpControl btn-sm addButton canInsert">ADD NEW ROLE</a>
                        </div>
                    </div>
                </div>
            </div>

            <div id="acc-Temp" class="accordion-item">
                <div data-bs-toggle="collapse" data-bs-target="#pnlTwo">
                    <div class="card-header btn-lbp-green text-center rounded-0">
                        <h6 class="mb-0">For Approval
                        </h6>
                    </div>
                </div>
                <div id="pnlTwo" class="accordion-collapse collapse show" aria-labelledby="panelTwoOpen">
                    <div class="accordion-body">
                        <div class="row mb-2">
                            <label for="txtRoleTemp" class="col-sm-4 col-form-label label-font-standard">Role Name/ Role Description: </label>
                            <div class="col-sm-4">
                                <input id="txtRoleTemp" type="text" class="form-control form-control-sm" aria-label="RoleFilter" maxlength='<%#getFieldLength(FieldVariables.SEARCH_TEXT, LengthSetting.MAX)%>' />
                            </div>
                          
                            <div class="col-sm-auto">
                                <a id="btnSearchTemp" class="lbpControl btn-sm searchButton">Search</a>
                                <a id="btnResetTemp" class="lbpControl btn-sm clearButton">Reset</a>
                            </div>
                        </div>

                        <div class="row mb-2">
                            <label for="ddlFilterTemp" class="col-sm-4 col-form-label label-font-standard">Filter by Status: </label>
                            <div class="col-sm-4">
                                <select id="ddlFilterTemp" class="form-select form-select-sm hardcodedSelect">
                                    <option value="">All</option>
                                    <option value="6">For Creation</option>
                                    <option value="5">For Update</option>
                                    <option value="3">For Deactivation</option>
                                     <option value="1">For Activation</option>
                                </select>
                            </div>
                        </div>

                        <div id="divRoleTemp" class="form-group pagerDiv">
                            <table id="tblRoleTemp" class="table table-sm" align="center">
                                <thead class="noScrollFixedHeader">
                                    <tr valign="middle" align="center">
                                        <th align="center" valign="middle" width="10%" data-name="" data-functions="view"
                                            data-alignment="center" class="hasButtons" data-pass="RoleId,RoleTempId,TempStatus"></th>

                                        <th align="center" valign="middle" width="20%" data-name="RoleName" data-alignment="center"
                                            data-columnname="Role Name">Role Name
                                        </th>

                                        <th align="center" valign="middle" width="25%" data-name="RoleDescription" data-alignment="center"
                                            data-columnname="Role Description">Role Description
                                        </th>

                                        <th align="center" valign="middle" width="15%" data-name="RequestedBy" data-alignment="center"
                                            data-columnname="Requested By">Requested By
                                        </th>

                                        <th align="center" valign="middle" width="15%" data-name="DateRequested" data-alignment="center"
                                            data-columnname="Date Requested">Date Requested
                                        </th>

                                        <th align="center" valign="middle" width="15%" data-name="Status" data-alignment="center"
                                            data-columnname="Status">Status
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="tBodyRoleTemp" class="noScrollContent border align-middle">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
