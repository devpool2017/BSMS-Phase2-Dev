<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master"
    CodeBehind="Groups.aspx.vb" Inherits="LBP.VS2010.BSMS.ReviewGroups" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Content/Scripts/PageScripts/Review/Groups.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card p-3">
        <div class="accordion" id="accordionPanelsGroups">
            <div id="DivAddEditDeleteGroups" data-bs-toggle="collapse" data-bs-target="#collapseOne">
                <div class="card-header btn-lbp-green text-center rounded-0" id="Div5">
                    <h6 class="mb-0">
                         List of Groups
                    </h6>
                </div>
            </div>
            <div id="collapseOne" class="accordion-collapse show" aria-labelledby="headingOne">
                <div class="accordion-body">
                  <div id="divGroupsUpdateDelete" class="form-group pagerDiv" hidden>
                        <table id="tblGroupUpdateDelete" class="table table-sm" align="center">
                            <thead class="noScrollFixedHeader">
                                <tr valign="middle" align="center">
                                 <th align="center" valign="middle" width="10%" data-name="" data-functions="edit,delete"
                                        data-alignment="center" class="hasButtons" data-pass="GroupCode,GroupName"></th>
                                        <th align="center" valign="middle" width="15%" data-name="GroupCode" data-alignment="center"
                                            data-columnname="Group Code">
                                            Group Code
                                        </th>
                                        <th align="center" valign="middle" width="15%" data-name="GroupName" data-alignment="center"
                                            data-columnname="Group Name">
                                            Group Name
                                        </th>
                                </tr>
                            </thead>
                            <tbody id="tBodyGroupsUpdateDelete" class="noScrollContent border align-middle">
                            </tbody>
                        </table>
                    </div>
                     <div id="divGroups" class="form-group pagerDiv" hidden>
                        <table id="tblGroup" class="table table-sm" align="center">
                            <thead class="noScrollFixedHeader">
                                <tr valign="middle" align="center">
                                        <th align="center" valign="middle" width="15%" data-name="GroupCode" data-alignment="center"
                                            data-columnname="Group Code">
                                            Group Code
                                        </th>
                                        <th align="center" valign="middle" width="15%" data-name="GroupName" data-alignment="center"
                                            data-columnname="Group Name">
                                            Group Name
                                        </th>
                                </tr>
                            </thead>
                            <tbody id="tBodyGroups" class="noScrollContent border align-middle">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div id="divAddGroup" class="text-end canInsert" hidden>
                <a id="btnAddGroup" class="lbpControl btn-sm addButton right canInsert">Create New Group</a>
            </div>
        </div>
    </div>
    <%--MODAL Add Group--%>
    <div id="divAddGroupModal" class="modal fade detailsModal" tabindex="-1" role="dialog"
        aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header alert alert-lbp">
                    <h5>
                        <span class="fa fa-cubes" style="vertical-align: -webkit-baseline-middle; color: #FFFFFF">
                        </span>&nbsp&nbsp</h5>
                    <h4 class="modal-title" style="color: #FFFFFF; text-align: left;" id="H1">
                        Add New Group
                    </h4>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" style="float: right;">
                    </button>
                    <label id="Label1" class="col-3 col-form-label label-font-standard" for="lblAction"
                        hidden>
                    </label>
                </div>
                <div id="div2" class="modal-body">
                    <div id="div3">
                        <div class="form-group">
                            <div class="form-group form-row">
                                <label class="col-3 col-form-label label-font-standard">
                                    Group Code
                                </label>
                                <div class="col-xs-2 col-lg-2">
                                    <input type="text" id="txtAddGroupCode" class="lbpControl form-control required"
                                        autocomplete="off" disabled="true" />
                                </div>
                            </div>
                            <div class="form-group form-row">
                                <label class="col-3 col-form-label label-font-standard">
                                    Group Name
                                </label>
                                <div class="col-xs-5 col-lg-5">
                                    <input type="text" id="txtAddGroupName" class="lbpControl form-control required"
                                        autocomplete="off" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divModalAddError" class="form-group lbpControl" tabindex="-1">
                    <label id="lblModalAddErrorMessage" style="display: none; cursor: default; width: 100%;
                        text-align: center" class="lbpControl btn btn-danger btn-block">
                    </label>
                </div>
                <div class="modal-footer" id="div6">
                    <a id="btnAddSave" class="lbpControl saveButton">Save</a> <a id="btnAddCancel" class="lbpControl cancelButton">
                        Cancel</a>
                </div>
            </div>
        </div>
    </div>
    <%--MODAL Edit Group--%>
    <div id="divEditGroupModal" class="modal fade detailsModal" tabindex="-1" role="dialog"
        aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header alert alert-lbp">
                    <h5>
                        <span class="fa fa-cubes" style="vertical-align: -webkit-baseline-middle; color: #FFFFFF">
                        </span>&nbsp&nbsp</h5>
                    <h4 class="modal-title" style="color: #FFFFFF; text-align: left;" id="modalHeader">
                        Edit Group
                    </h4>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" style="float: right;">
                    </button>
                    <label id="lblAction" class="col-3 col-form-label label-font-standard" for="lblAction"
                        hidden>
                    </label>
                </div>
                <div id="divModalBody" class="modal-body">
                    <div id="divDetail">
                        <div class="form-group">
                            <div class="form-group form-row">
                                <label class="col-3 col-form-label label-font-standard">
                                    Group Code
                                </label>
                                <div class="col-xs-2 col-lg-2">
                                    <input type="text" id="txtEditGroupCode" class="lbpControl form-control" autocomplete="off"
                                        disabled="true" />
                                </div>
                            </div>
                            <div class="form-group form-row">
                                <label class="col-3 col-form-label label-font-standard">
                                    Group Name
                                </label>
                                <div class="col-xs-5 col-lg-5">
                                    <input type="text" id="txtEditGroupName" class="lbpControl form-control required"
                                        autocomplete="off" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divModalEditError" class="form-group lbpControl" tabindex="-1">
                    <label id="lblModalEditErrorMessage" style="display: none; cursor: default; width: 100%;
                        text-align: center" class="lbpControl btn btn-danger btn-block">
                    </label>
                </div>
                <div class="modal-footer" id="divButtonsmodal">
                    <a id="btnEditSave" class="lbpControl saveButton">Save</a> <a id="btnEditCancel"
                        class="lbpControl cancelButton">Cancel</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
