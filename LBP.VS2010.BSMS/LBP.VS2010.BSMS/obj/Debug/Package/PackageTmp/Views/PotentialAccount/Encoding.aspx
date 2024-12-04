<%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master" CodeBehind="Encoding.aspx.vb" Inherits="LBP.VS2010.BSMS.Encoding" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Content/Scripts/PageScripts/CPA/Encoding.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <input type="hidden" id="hdnSession" data-value="<%=Session("LogonUser")%>" />
    <%--<input type="hidden" id="hdnSession1" data-value="<%=Session("GroupCode")%>" />--%>
    <input type="hidden" id="hdnSession1" data-value="<%=Session("BranchCode")%>" />

<div id="frmHorizontal" class="well">
<label id="lblAction" class="col-3 col-form-label label-font-standard" for="lblAction" hidden></label>
    <div id="divCPAccounts" class="form-group pagerDiv" >
        <table id="tblCPAccounts" class="table table-sm col-sm-12" align="center">
            <thead class="noScrollFixedHeader">
                <tr valign="middle" align="center">
                    <th align="center" valign="middle" width="20%" data-name="" data-functions="view,edit" data-access="CanView.Access,CanUpdate.Access"
                        data-alignment="center" class="hasButtons hideOnView reviseButton" data-pass="ClientID">
                    </th>
                    <th align="center" valign="middle" width="30%" data-name="ClientName" data-alignment="center"
                        data-columnname="ClientName">
                        Name
                    </th>
                    <th align="center" valign="middle" width="35%" data-name="ClientIndustry" data-alignment="center"
                        data-columnname="ClientIndustry">
                        Industry
                    </th>
                    <th align="center" valign="middle" width="15%" data-name="DateEncoded" data-alignment="center"
                        data-columnname="DateEncoded">
                        Date Encoded
                    </th>
                </tr>
            </thead>
            <tbody id="tBodyCPAccounts" class="noScrollContent border align-middle">
            </tbody>
        </table>
    </div>
    <div id="divNotAllowed" class="form-group" hidden>
        <label id = "lblError" style="cursor:default; width: 100%; text-align: center; color:White;" class="lbpControl btn btn-danger btn-block "></label>
    </div>
    <div id="divbtnAdd" class="text-end">
        <a id="btnAdd" class="btn btn-sm lbpControl addButton canInsert">
            ADD</a>
    </div>
    <div id="divCPADetails" class="form-group pagerDiv" align="left">
        <label id="lblAcctName" class="col-3 col-form-label label-font-standard" for="lblAcctName" hidden></label>
        <label id="lblIndID" class="col-3 col-form-label label-font-standard" for="lblIndID" hidden></label>
        <label id="lblCPAID" class="col-3 col-form-label label-font-standard" for="lblCPAID" hidden></label>
            <div class="row-form-group">
                <div class="row mb-3">
                    <label for="txtAccountName" class="col-sm-2 col-form-label label-font-standard">
                        Potential Account Name:
                    </label>
                <div class="col-sm-6">
                    <input id="txtAccountName" class="lbpControl form-control form-control-sm required requiredField name" autocomplete="off" 
                           maxlength="<%#getFieldLength(FieldVariables.CPANAME_MAX, LengthSetting.MAX)%>" />
                </div>
                </div>
           
                <div class="row mb-3">
                    <label for="ddlIndustry" class="col-sm-2 col-form-label label-font-standard">
                        Industry:
                    </label>
                <div class="col-sm-6">
                    <input id="txtIndustry" style="width:40%;" />
                    <select id="ddlIndustry" class="form-select form-select-sm hardcodedSelect" >
                    </select>
                </div>
                </div>
            </div>
            <div class="text-end">
                <a id="btnSave" class="lbpControl btn-sm saveButton canSave">Save</a>
                <a id="btnCancel" class="btn btn-sm lbpControl cancelButton">Cancel</a>
            </div>
    </div>
</div>
</asp:Content>
    