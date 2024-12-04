<%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master" CodeBehind="RoleDetails.aspx.vb" Inherits="LBP.VS2010.BSMS.RoleDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Content/Scripts/PageScripts/AdminTools/RoleDetails.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card p-3">
        <div class="row mb-2">
            <label for="txtRoleName" class="col-sm-2 col-form-label label-font-standard">Role Name</label>
            <span class="col-sm-1 label-font-standard">:</span>
            <div class="col-sm-4">
                <input id="txtRoleName" type="text" class="form-control form-control-sm required requiredField alphanumeric" aria-label="RoleName" autocomplete="off"
                    maxlength='<%#getFieldLength(FieldVariables.ROLE_NAME, LengthSetting.MAX)%>' />
            </div>
        </div>

        <div class="row">
            <label for="txtRoleDescription" class="col-sm-2 col-form-label label-font-standard">Role Description</label>
            <span class="col-sm-1 label-font-standard">:</span>
            <div class="col-sm-4">
                <input id="txtRoleDescription" type="text" class="form-control form-control-sm required requiredField alphanumeric" aria-label="RoleDescription" autocomplete="off"
                    maxlength='<%#getFieldLength(FieldVariables.ROLE_DESCRIPTION, LengthSetting.MAX)%>' />
            </div>
        </div>

        <hr />

        <div class="mb-4">
            <table id="tblAccess" class="w-100">
                <tbody class="noScrollContent border"></tbody>
            </table>
        </div>

        <div id="divEditLabel" class="text-center">
            <span class="col-form-label label-font-standard">Edit is disabled due to pending request</span>
        </div>

        <div class="text-end">
            <a id="btnApprove" class="lbpControl btn-sm approveButton canApprove">APPROVE</a>
            <a id="btnReject" class="lbpControl btn-sm rejectButton canApprove">REJECT</a>
            <a id="btnViewOrig" class="lbpControl btn-sm viewButton">VIEW ORIGINAL</a>
            <a id="btnViewTemp" class="lbpControl btn-sm viewButton" hidden="hidden">VIEW FOR APPROVAL</a>
            <a id="btnSave" class="lbpControl btn-sm saveButton">SAVE</a>
            <a id="btnBack" class="lbpControl btn-sm backButton">BACK TO LIST</a>
        </div>
    </div>
</asp:Content>
