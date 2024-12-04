
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master"
    CodeBehind="Branches.aspx.vb" Inherits="LBP.VS2010.BSMS.Branches" %>

    <%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Content/Scripts/PageScripts/Review/Branches.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card p-3">
        <div class="accordion" id="accordionPanelsGroups">
            <div id="DivAddEditDeleteGroups" data-bs-toggle="collapse" data-bs-target="#collapseOne">
                <div class="card-header btn-lbp-green text-center rounded-0" id="Div5">
                    <h6 class="mb-0">
                        List of Branches
                    </h6>
                </div>
            </div>
            <div id="collapseOne" class="accordion-collapse show" aria-labelledby="headingOne">
                <div class="accordion-body">
                    <br />
                    <div class="form-group form-row">
                        <label class="col-sm-2 col-form-label label-font-standard" for="ddlRegion">
                            Group
                        </label>
                        <div class="col-sm-4">
                            <select id="ddlRegion" class="form-select form-select-sm hardcodedSelect">
                            </select>
                        </div>
                        <a id="btnSearch" class="lbpControl searchButton btn-sm-1">Search</a>
                    </div>
                    <br />
                   <div id="divBranchesGH" class="form-group pagerDiv" hidden>
                        <table id="tblBranchesGH" class="table table-sm" align="center">
                            <thead class="noScrollFixedHeader">
                                <tr valign="middle" align="center">
                                    <th align="center" valign="middle" width="15%" data-name="" data-functions="edit,delete"
                                        data-access="CanUpdate.Access,CanDelete.Access" data-alignment="center"
                                        class="hasButtons" data-pass="BranchCode,BranchName,RegionName,RegionCode">
                                        <th align="center" valign="middle" width="15%" data-name="BranchCode" data-alignment="center"
                                            data-columnname="BranchCode">
                                            Branch Code
                                        </th>
                                        <th align="center" valign="middle" width="15%" data-name="BranchName" data-alignment="center"
                                            data-columnname="BranchName">
                                            Branch Name
                                        </th>
                                        <th align="center" valign="middle" width="15%" data-name="RegionName" data-alignment="center"
                                            data-columnname="RegionName">
                                            Group Name
                                        </th>
                                </tr>
                            </thead>
                            <tbody id="tBodyBranchesGH" class="noScrollContent border align-middle">
                            </tbody>
                        </table>
                    </div>
                      <div id="divBranchesSH" class="form-group pagerDiv" hidden>
                        <table id="tblBranchesSH" class="table table-sm" align="center">
                            <thead class="noScrollFixedHeader">
                                <tr valign="middle" align="center">
                                    <%--<th align="center" valign="middle" width="15%" data-name="" data-functions="edit,delete"
                                        data-access="CanUpdate.Access,CanDelete.Access" data-alignment="center"
                                        class="hasButtons" data-pass="BranchCode,BranchName,RegionName,RegionCode">--%>
                                        <th align="center" valign="middle" width="15%" data-name="BranchCode" data-alignment="center"
                                            data-columnname="BranchCode">
                                            Branch Code
                                        </th>
                                        <th align="center" valign="middle" width="15%" data-name="BranchName" data-alignment="center"
                                            data-columnname="BranchName">
                                            Branch Name
                                        </th>
                                        <th align="center" valign="middle" width="15%" data-name="RegionName" data-alignment="center"
                                            data-columnname="RegionName">
                                            Group Name
                                        </th>
                                </tr>
                            </thead>
                            <tbody id="tBodyBranchesSH" class="noScrollContent border align-middle">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div id="divAddBranches" class="text-end canInsert" hidden>
                <a id="btnAddBranches" class="lbpControl btn-sm addButton right canInsert">Create New Branches</a>
            </div>
        </div>
    </div>
    <%--MODAL Add Group--%>
    <div id="divAddBranchModal" class="modal fade detailsModal" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header alert alert-lbp">
                    <h5>
                        <span class="fa fa-cubes" style="vertical-align: -webkit-baseline-middle; color: #FFFFFF">
                        </span>&nbsp&nbsp</h5>
                    <h4 class="modal-title" style="color: #FFFFFF; text-align: left;" id="modalTitle">
                       
                    </h4>
                    <button type="button" class="btn-close" id="btnModalclose" data-bs-dismiss="modal" style="float: right;">
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
                                    Region
                                </label>
                                <div class="col-xs-4 col-lg-4">
                                    <select id="ddlAddRegionModal" class="form-select form-select-sm hardcodedSelect required alphanumeric">
                                    </select>
                                </div>
                            </div>
                            <div class="form-group form-row">
                                <label class="col-3 col-form-label label-font-standard">
                                   Branch Code
                                </label>
                                <div class="col-xs-4 col-lg-4">
                                    <input type="text" id="txtBranchCodeModal" class="lbpControl form-control required wholenum"
                                       autocomplete="off" maxlength="<%# getFieldLength(FieldVariables.BranchCode, LengthSetting.MAX) %>" />
                                </div>
                            </div>
                            <div class="form-group form-row">
                                <label class="col-3 col-form-label label-font-standard">
                                   Branch Name
                                </label>
                                <div class="col-xs-4 col-lg-4">
                                    <input type="text" id="txtBranchNameModal" class="lbpControl form-control required"
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
                    <a id="btnAddModalSave" class="lbpControl saveButton">Save</a>
                    <a id="btnUpdateModalSave" class="lbpControl saveButton" hidden>Update</a>
                    <a id="btnAddModalCancel" class="lbpControl cancelButton">
                        Cancel</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
