<%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master"
    CodeBehind="UserTarget.aspx.vb" Inherits="LBP.VS2010.BSMS.UserTarget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Content/Scripts/PageScripts/AdminTools/UserTarget.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card p-3">
        <div class="accordion" id="acc-Main">
            <div id="acc-UserTarget" class="accordion-item">
                <div data-bs-toggle="collapse" data-bs-target="#pnlOne">
                    <div class="card-header btn-lbp-green text-center rounded-0">
                        <h6 class="mb-0">
                            User Targets
                        </h6>
                    </div>
                </div>
                <div id="pnlOne" class="accordion-collapse collapse show" aria-labelledby="panelOneOpen">
                    <div class="accordion-body">
                        <div class="form-group form-row">
                            <label class="col-1 col-form-label label-font-standard" for="ddlSearchRegion">
                                Group
                            </label>
                            <div class="col-sm-4">
                                <select id="ddlSearchRegion" class="form-select form-select-sm">
                                    <option value=""></option>
                                </select>
                            </div>
                            <label class="col-1 col-form-label label-font-standard" for="ddlSearchBranches">
                                Branch
                            </label>
                            <div class="col-xs-3 col-lg-3">
                                <select id="ddlSearchBranches" class="form-select hardcodedSelect">
                                    <option value=""></option>
                                </select>
                            </div>
                            <div id="divButtons" class="col-xs-3 col-lg-3">
                                <a id="btnSearch" class="lbpControl searchButton">Search</a>
                            </div>
                        </div>
                    </div>
                    <div id="divUserTarget" class="form-group pagerDiv">
                        <table id="tblUserTarget" class="table table-sm" align="center">
                            <thead class="noScrollFixedHeader">
                                <tr valign="middle" align="center">
                                    <th align="center" valign="middle" width="15%" data-name="" data-functions="view,edit"
                                        data-alignment="center" class="hasButtons" data-pass="Username" data-access="CanView.Access,CanUpdate.Access">
                                    </th>
                                    <th align="center" valign="middle" width="20%" data-name="Branch" data-alignment="center"
                                        data-columnname="Branch">
                                        Branch
                                    </th>
                                    <th align="center" valign="middle" width="10%" data-name="Username" data-alignment="center"
                                        data-columnname="Username">
                                        User ID
                                    </th>
                                    <th align="center" valign="middle" width="20%" data-name="Fullname" data-alignment="center"
                                        data-columnname="Fullname">
                                        Branch Head
                                    </th>
                                 
                                    <th align="center" valign="middle" width="10%" data-name="CASATarget" data-alignment="center"
                                        data-columnname="CASATarget">
                                        Target - CASA
                                    </th>
                                    <th align="center" valign="middle" width="10%" data-name="LoansTarget" data-alignment="center"
                                        data-columnname="LoansTarget">
                                        Target - Loans
                                    </th>
                                </tr>
                            </thead>
                            <tbody id="tBodyUserTarget" class="noScrollContent border align-middle">
                            </tbody>
                        </table>
                    </div>
                    <div class="text-end">
                        <a id="btnAdd" class="lbpControl btn-sm addButton canInsert">ADD NEW TARGET</a>
                    </div>
                </div>
            </div>
            <div id="acc-UserTargetTemp" class="accordion-item">
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
                            <label class="col-1 col-form-label label-font-standard" for="ddlRegionTemp">
                                Group
                            </label>
                            <div class="col-sm-4">
                                <select id="ddlRegionTemp" class="form-select form-select-sm">
                                    <option value=""></option>
                                </select>
                            </div>
                            <label class="col-1 col-form-label label-font-standard" for="ddlBranchTemp">
                                Branch
                            </label>
                            <div class="col-xs-3 col-lg-3">
                                <select id="ddlBranchTemp" class="form-select hardcodedSelect">
                                    <option value=""></option>
                                </select>
                            </div>
                            <div class="col-sm-auto">
                                <a id="btnSearchTemp" class="lbpControl btn-sm searchButton">Search</a> <a id="btnResetTemp"
                                    class="lbpControl btn-sm clearButton">Reset</a>
                            </div>
                        </div>
                        <div class="row mb-2">
                            <label class="col-sm-2 col-form-label label-font-standard">
                                Filter by Status:
                            </label>
                            <div class="col-sm-4">
                                <select id="ddl-s-t-f" class="form-select form-select-sm hardcodedSelect">
                                    <option value="">All</option>
                                    <option value="6">For Creation</option>
                                    <option value="5">For Update</option>
                                </select>
                            </div>
                        </div>
                        <div id="divUserTargetTemp" class="form-group pagerDiv">
                            <table id="tblUserTargetTemp" class="table table-sm" align="center">
                                <thead class="noScrollFixedHeader">
                                    <tr valign="middle" align="center">
                                        <th align="center" valign="middle" width="20%" data-name="" data-functions="view"
                                            data-alignment="center" class="hasButtons" data-pass="Username,TempStatusID,FullName">
                                        </th>
                                        <th align="center" valign="middle" width="25%" data-name="Branch" data-alignment="center"
                                            data-columnname="Branch Name">
                                            Branch
                                        </th>
                                        <th align="center" valign="middle" width="10%" data-name="Username" data-alignment="center"
                                            data-columnname="User ID">
                                            User ID
                                        </th>
                                        <th align="center" valign="middle" width="30%" data-name="Fullname" data-alignment="center"
                                            data-columnname="Fullname">
                                            Branch Head
                                        </th>
                                        <th align="center" valign="middle" width="10%" data-name="RequestedBy" data-alignment="center"
                                            data-columnname="Requested By">
                                            Requested By
                                        </th>
                                        <th align="center" valign="middle" width="15%" data-name="DateRequested" data-alignment="center"
                                            data-columnname="Date Requested">
                                            Date Requested
                                        </th>
                                        <th align="center" valign="middle" width="20%" data-name="Status" data-alignment="center"
                                            data-columnname="Status">
                                            Status
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="tBodyUserTargetTemp" class="noScrollContent border align-middle">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="modalUserTarget" class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false"
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
                            Branch</label>
                        <div class="col-sm-9">
                            <select id="ddlBranch" class="form-select form-select-sm required requiredField">
                            </select>
                        </div>
                     
                    </div>
                     <div class="row mb-3">
                        <label class="col-sm-3 col-form-label label-font-standard" for="ddlUserFullName">
                            Branch Head
                        </label>
                        <div class="col-sm-9">
                            <select id="ddlUserFullName" class="form-select form-select-sm required requiredField">

                            </select>
                        </div>
                     </div>
                    <div class="row mb-3">
                        <label class="col-sm-3 col-form-label label-font-standard" for="txtCasaTarget">
                            CASA Target</label>
                        <div class="col-3">
                            <input type="text" id="txtCasaTarget" class="form-control form-control-sm required requiredField wholenum" 
                                maxlength="<%#getFieldLength(FieldVariables.CASA_TARGET, LengthSetting.MAX)%>"/>
                        </div>
                        <label class="col-sm-3 col-form-label label-font-standard" for="txtLoansTarget">
                            Loans Target</label>
                        <div class="col-3">
                            <input type="text" id="txtLoansTarget" class="form-control form-control-sm required requiredField wholenum" 
                                maxlength="<%#getFieldLength(FieldVariables.LOANS_TARGET, LengthSetting.MAX)%>"/>
                        </div>
                    </div>
                </div>
                <div id="divUserTargetError" class="lbpControl bg-danger text-center text-light">
                    <span id="lblUserErrorMessage"></span>
                </div>
                <div id="divEditLabel" class="text-center" hidden>
                    <span class="col-form-label label-font-standard">Edit is disabled due to pending request</span>
                </div>
                <div class="modal-footer">
                    <a id="btnSave" class="lbpControl saveButton" hidden>Save</a> <a data-bs-dismiss="modal"
                        class="lbpControl cancelButton">Cancel</a>
                </div>
            </div>
        </div>
    </div>
    <div id="modalUserTargetTemp" class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false"
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
