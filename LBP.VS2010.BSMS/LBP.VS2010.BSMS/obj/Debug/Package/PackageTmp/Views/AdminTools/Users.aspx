<%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master"
    CodeBehind="Users.aspx.vb" Inherits="LBP.VS2010.BSMS.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Content/Scripts/PageScripts/AdminTools/Users.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card p-3">
        <div class="accordion" id="acc-Main">
            <div id="acc-Users" class="accordion-item">
                <div data-bs-toggle="collapse" data-bs-target="#pnlOne">
                    <div class="card-header btn-lbp-green text-center rounded-0">
                        <h6 class="mb-0">
                            Active/Disable
                        </h6>
                    </div>
                </div>
                <div id="pnlOne" class="accordion-collapse collapse show" aria-labelledby="panelOneOpen">
                    <div class="accordion-body">
                        <div class="row mb-2">
                            <label for="txtUser" class="col-sm-4 col-form-label label-font-standard">
                                User ID/ Name:
                            </label><div class="col-sm-4">
                                <input id="txtUser" type="text" class="form-control form-control-sm" aria-label="UserFilter"
                                    maxlength='<%#getFieldLength(FieldVariables.SEARCH_TEXT, LengthSetting.MAX)%>' />
                            </div>
                            <div class="col-sm-auto">
                                <a id="btnSearch" class="lbpControl btn-sm searchButton">Search</a> <a id="btnReset"
                                    class="lbpControl btn-sm clearButton">Reset</a>
                            </div>
                        </div>
                        <div class="row mb-2">
                            <label for="ddl-s-f" class="col-sm-4 col-form-label label-font-standard">
                                Filter by Status:
                            </label>
                            <div class="col-sm-4">
                                <select id="ddl-s-f" class="form-select form-select-sm hardcodedSelect">
                                    <option value="">All</option>
                                    <option value="1">Active</option>
                                    <option value="2">Disable</option>

                                </select>
                            </div>
                        </div>
                        <div class="row mb-2">
                            <label for="ddl-r-f" class="col-sm-4 col-form-label label-font-standard">
                                Filter by Role:
                            </label>
                            <div class="col-sm-4">
                                <select id="ddl-r-f" class="form-select form-select-sm">
                                </select>
                            </div>
                        </div>
                         <div class="row mb-2">
                            <label for="ddl-r-f" class="col-sm-4 col-form-label label-font-standard">
                                Filter by Group:
                            </label>
                            <div class="col-sm-4">
                                <select id="ddlSearchRegion" class="form-select form-select-sm">
                                </select>
                            </div>
                            
                        </div>
                        <div class="row mb-2">
                         <label for="ddl-r-f" class="col-sm-4 col-form-label label-font-standard">
                                Filter by Branch:
                            </label>
                            <div class="col-sm-4">
                                <select id="ddlSearchBranch" class="form-select form-select-sm">
                                </select>
                            </div>
                        </div>
                        <div id="divUsers" class="form-group pagerDiv">
                            <table id="tblUsers" class="table table-sm" align="center">
                                <thead class="noScrollFixedHeader">
                                    <tr valign="middle" align="center">
                                        <th align="center" valign="middle" width="25%" data-name="" data-functions="view,edit,unlock"
                                            data-alignment="center" class="hasButtons" data-pass="Username" data-access="CanView.Access,CanUpdate.Access,CanUnlock.Access">
                                        </th>
                                        <th align="center" valign="middle" width="20%" data-name="Username" data-alignment="center"
                                            data-columnname="Username">
                                            User ID
                                        </th>
                                        <th align="center" valign="middle" width="25%" data-name="Name" data-alignment="center"
                                            data-columnname="Name">
                                            Name
                                        </th>
                                        <th align="center" valign="middle" width="20%" data-name="RoleName" data-alignment="center"
                                            data-columnname="RoleName">
                                            Role
                                        </th>
                                        <th align="center" valign="middle" width="10%" data-name="Status" data-alignment="center"
                                            data-columnname="Status">
                                            Status
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="tBodyUsers" class="noScrollContent border align-middle">
                                </tbody>
                            </table>
                        </div>
                        <div class="text-end">
                            <a id="btnAdd" class="lbpControl btn-sm addButton canInsert">ADD NEW USER</a>
                             <a id="btnDownload" class="lbpControl btn-sm downloadButton">DOWNLOAD REPORT</a>
                        </div>
                    </div>
                </div>
            </div>
            <%--invisible Report Print div--%>
            <div id="divReport" hidden>
             <iframe id="ifrmReportViewer" name="ifrmReportViewer" width="100%" height="600 px"
                src="about:blank" frameborder="0"></iframe>
            </div>
            <%--    invisible Report Print div--%>
            <div id="acc-UsersTemp" class="accordion-item">
                <div data-bs-toggle="collapse" data-bs-target="#pnlTwo">
                    <div class="card-header btn-lbp-green text-center rounded-0">
                        <h6 class="mb-0">
                            For Approval
                        </h6>
                    </div>
                </div>
                <div id="pnlTwo" class="accordion-collapse collapse show" aria-labelledby="panelTwoOpen">
                    <div class="accordion-body">
                        <div class="row mb-2">
                            <label for="txtUserTemp" class="col-sm-4 col-form-label label-font-standard">
                                User ID/ Name:
                            </label>
                            <div class="col-sm-4">
                                <input id="txtUserTemp" type="text" class="form-control form-control-sm" aria-label="UserFilter"
                                    maxlength='<%#getFieldLength(FieldVariables.SEARCH_TEXT, LengthSetting.MAX)%>' />
                            </div>
                            <div class="col-sm-auto">
                                <a id="btnSearchTemp" class="lbpControl btn-sm searchButton">Search</a> <a id="btnResetTemp"
                                    class="lbpControl btn-sm clearButton">Reset</a>
                            </div>
                        </div>
                        <div class="row mb-2">
                            <label for="ddl-s-t-f" class="col-sm-4 col-form-label label-font-standard">
                                Filter by Status:
                            </label>
                            <div class="col-sm-4">
                                <select id="ddl-s-t-f" class="form-select form-select-sm hardcodedSelect">
                                    <option value="">All</option>
                                    <option value="6">For Creation</option>
                                    <option value="5">For Update</option>
                               <%--     <option value="7">For Deletion</option>
                                    <option value="8">For Unlock</option>--%>
                                </select>
                            </div>
                        </div>
                        <div id="divUsersTemp" class="form-group pagerDiv">
                            <table id="tblUsersTemp" class="table table-sm" align="center">
                                <thead class="noScrollFixedHeader">
                                    <tr valign="middle" align="center">
                                        <th align="center" valign="middle" width="20%" data-name="" data-functions="view"
                                            data-alignment="center" class="hasButtons" data-pass="Username,TempStatusID,Name,FirstName">
                                        </th>
                                        <th align="center" valign="middle" width="20%" data-name="Username" data-alignment="center"
                                            data-columnname="Username">
                                            User ID
                                        </th>
                                        <th align="center" valign="middle" width="20%" data-name="RequestedBy" data-alignment="center"
                                            data-columnname="Requested By">
                                            Requested By
                                        </th>
                                        <th align="center" valign="middle" width="20%" data-name="DateRequested" data-alignment="center"
                                            data-columnname="Date Requested">
                                            Date Requested
                                        </th>
                                        <th align="center" valign="middle" width="20%" data-name="Status" data-alignment="center"
                                            data-columnname="Status">
                                            Status
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="tBodyUsersTemp" class="noScrollContent border align-middle">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="modalUser" class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false"
        tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog" style="max-width: 650px; margin: auto;">
            <div class="modal-content">
                <div class="modal-header btn-lbp-gray text-light">
                    <h5 class="modal-title">
                        <span id="span-u-a"></span>
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                    </button>
                </div>
                <div class="modal-body px-5 py-4">
                    <div class="row mb-3">
                        <label class="col-sm-3 col-form-label label-font-standard" for="txtUserName">
                            User ID</label>
                        <div class="col-sm-9">
                            <input type="text" id="txtUserName" class="form-control form-control-sm required requiredField alphanumeric"
                                autocomplete="off" maxlength='<%#getFieldLength(FieldVariables.USERNAME, LengthSetting.MAX)%>' />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label class="col-sm-3 col-form-label label-font-standard" for="ddlRole">
                            Role
                        </label>
                        <div class="col-sm-9">
                            <select id="ddlRole" class="form-select form-select-sm required requiredField">
                            </select>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label class="col-sm-3 col-form-label label-font-standard" for="txt-f-n">
                            First Name</label>
                        <div class="col-sm-9">
                            <input type="text" id="txt-f-n" class="form-control form-control-sm required requiredField name"
                                autocomplete="off" maxlength='<%#getFieldLength(FieldVariables.FIRST_NAME, LengthSetting.MAX)%>' />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label class="col-sm-3 col-form-label label-font-standard" for="txt-l-n">
                            Last Name</label>
                        <div class="col-sm-9">
                            <input type="text" id="txt-l-n" class="form-control form-control-sm required requiredField name"
                                autocomplete="off" maxlength='<%#getFieldLength(FieldVariables.LAST_NAME, LengthSetting.MAX)%>' />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label class="col-sm-3 col-form-label label-font-standard" for="txt-m-i">
                            Middle Initial</label>
                        <div class="col-sm-9">
                            <input type="text" id="txt-m-i" class="form-control form-control-sm name"
                                autocomplete="off" maxlength='<%#getFieldLength(FieldVariables.MIDDLE_INITIAL, LengthSetting.MAX)%>' />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label class="col-sm-3 col-form-label label-font-standard" for="txt-name">
                            Display Name</label>
                        <div class="col-sm-9">
                            <input type="text" id="txt-name" class="form-control form-control-sm name" autocomplete="off" />
                        </div>
                    </div>
                  
                    <div class="row mb-3">
                        <label class="col-sm-3 col-form-label label-font-standard" for="ddlRegion">
                            Group
                        </label>
                        <div class="col-sm-9">
                            <select id="ddlRegion" class="form-select form-select-sm required requiredField">
                            </select>
                        </div>
                    </div>
                      <div class="row mb-3">
                        <label class="col-sm-3 col-form-label label-font-standard" for="ddlBranch">
                            Branch
                        </label>
                        <div class="col-sm-9">
                            <select id="ddlBranch" class="form-select form-select-sm required requiredField">
                            </select>
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label class="col-sm-3 col-form-label label-font-standard" for="txtIPAddress">
                            IP Address</label>
                        <div class="col-sm-9">
                            <input type="text" id="txtIPAddress" class="form-control form-control-sm" autocomplete="off"
                                maxlength='<%#getFieldLength(FieldVariables.IP, LengthSetting.MAX)%>' />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label class="col-sm-3 col-form-label label-font-standard" for="txtEmail">
                            Email</label>
                        <div class="col-sm-9">
                            <input type="text" id="txtEmail" class="form-control form-control-sm"
                                autocomplete="off" maxlength='<%#getFieldLength(FieldVariables.EMAIL, LengthSetting.MAX)%>' />
                        </div>
                    </div>
                    <div class="row mb-3">
                        <label class="col-sm-3 col-form-label label-font-standard" for="ddlStatus">
                            Status</label>
                        <div class="col-sm-9">
                            <select id="ddlStatus" class="form-select form-select-sm required requiredField hardcodedSelect">
                                <option value="">Please Select</option>
                                <option value="1">Active</option>
                                <option value="2">Disable</option>
                            </select>
                        </div>
                    </div>
                <%--    <div class="row mb-9">
                        <label class="col-sm-3 col-form-label label-font-standard" for="txtEcasaTarget">
                            ECASA Target</label>
                        <div class="col-3">
                            <input type="text" id="txtEcasaTarget" class="lbpControl form-control required requiredField numeric" />
                        </div>
                        <label class="col-sm-3 col-form-label label-font-standard" for="txtEcbgTarget">
                            ECBG Target</label>
                        <div class="col-3">
                            <input type="text" id="txtEcbgTarget" class="lbpControl form-control required requiredField numeric" />
                        </div>
                    </div>--%>
                    <%--<div class="row mb-3">
                    </div>
                    <div class="row mb-9">
                        <label class="col-sm-3 col-form-label label-font-standard" for="txtCasaTarget">
                            CASA Target</label>
                        <div class="col-3">
                            <input type="text" id="txtCasaTarget" class="form-control form-control-sm required requiredField wholenum" />
                        </div>
                        <label class="col-sm-3 col-form-label label-font-standard" for="txtCbgTarget">
                            Loans Target</label>
                        <div class="col-3">
                            <input type="text" id="txtCbgTarget" class="form-control form-control-sm required requiredField wholenum" />
                        </div>
                    </div>--%>
                </div>
                <div id="divUserError" class="lbpControl bg-danger text-center text-light">
                    <span id="lblUserErrorMessage"></span>
                </div>
                <div id="divEditLabel" class="text-center" hidden>
                    <span class="col-form-label label-font-standard">Edit is disabled due to pending request</span>
                </div>
                <div class="modal-footer">
                    <a id="btnValidate" class="lbpControl clearButton" hidden>Validate User</a>
                    <%--  <a id="btnUnlock" class="lbpControl unlockButton" hidden>Unlock</a> 
                    <a id="btnRstPword" class="lbpControl clearButton" hidden>Reset Password</a> --%>
                    <a id="btnSave" class="lbpControl saveButton" hidden>Save</a> <a data-bs-dismiss="modal"
                        class="lbpControl cancelButton">Cancel</a>
                </div>
            </div>
        </div>
    </div>
    <div id="modalUserTemp" class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false"
        tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog" style="max-width: 650px; margin: auto;">
            <div class="modal-content">
                <div class="modal-header btn-lbp-gray text-light">
                    <h5 class="modal-title">
                        <span id="span-u-t-a"></span>
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close">
                    </button>
                </div>
                <div class="modal-body px-5 py-4">
                    <div id="divApproval" class="form-group noPagerDiv">
                        <table id="tblApproval" class="table table-sm col-sm-12" align="center">
                            <thead class="noScrollFixedHeader">
                                <tr valign="middle" align="center">
                                    <th align="center" valign="middle" width="30%" data-name="Fields" data-alignment="center"
                                        data-columnname="Columns">
                                        Columns
                                    </th>
                                    <th align="center" valign="middle" width="35%" data-name="ValueFrom" data-alignment="center"
                                        data-columnname="Value From">
                                        Value From
                                    </th>
                                    <th align="center" valign="middle" width="35%" data-name="ValueTo" data-alignment="center"
                                        data-columnname="Value To">
                                        Value To
                                    </th>
                                </tr>
                            </thead>
                            <tbody id="tBodyApproval" class="noScrollContent border align-middle">
                            </tbody>
                        </table>
                    </div>
                    <div id="divUnlocked" class="row">
                        <label class="col-sm-3 col-form-label label-font-standard" for="chkUnlocked">
                            Unlock</label>
                        <div id="chkboxUnlocked" class="col-auto">
                            <div class="form-check">
                                <input class="form-check-input" type="checkbox" id="chkUnlocked" checked disabled />
                            </div>
                        </div>
                    </div>
                    <div id="divRstPword" class="row">
                        <label class="col-sm-3 col-form-label label-font-standard" for="chkRstPword">
                            Reset Password</label>
                        <div id="chkboxRstPword" class="col-auto">
                            <div class="form-check">
                                <input class="form-check-input" type="checkbox" id="chkRstPword" checked disabled />
                            </div>
                        </div>
                    </div>
                </div>
                <div id="div-e-t-m" class="lbpControl bg-danger text-center text-light">
                    <span id="lbl-e-t-m"></span>
                </div>
                <div class="modal-footer">
                    <a id="btnApprove" class="lbpControl approveButton canApprove">Approve</a> <a id="btnReject"
                        class="lbpControl rejectButton canApprove">Reject</a> <a data-bs-dismiss="modal"
                            class="lbpControl cancelButton">Cancel</a>
                </div>
            </div>
        </div>
    </div>
 
</asp:Content>
