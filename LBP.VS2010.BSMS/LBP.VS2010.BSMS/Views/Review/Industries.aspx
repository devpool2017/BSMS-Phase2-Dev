<%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master" CodeBehind="Industries.aspx.vb" Inherits="LBP.VS2010.BSMS.Industries" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Content/Scripts/PageScripts/Review/Industries.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
        <div class="card p-3">
        <div class="accordion" id="acc-Main">
            <div id="acc-Industry" class="accordion-item">
                <div data-bs-toggle="collapse" data-bs-target="#pnlOne">
                    <div class="card-header btn-lbp-green text-center rounded-0">
                        <h6 class="mb-0">
                            List of Industries
                        </h6>
                    </div>
                </div>
                <div id="pnlOne" class="accordion-collapse collapse show" aria-labelledby="panelOneOpen">
                    <div class="accordion-body">
                        <div class="row mb-2">
                            <label for="txtIndustry" class="col-sm-4 col-form-label label-font-standard">
                                Industry Code/ Industry Description:
                            </label><div class="col-sm-4">
                                <input id="txtIndustry" type="text" class="form-control form-control-sm" aria-label="IndustryFilter"
                                    maxlength='<%#getFieldLength(FieldVariables.SEARCH_TEXT, LengthSetting.MAX)%>' />
                            </div>
                            <div class="col-sm-auto">
                                <a id="btnSearch" class="lbpControl btn-sm searchButton">Search</a> <a id="btnReset"
                                    class="lbpControl btn-sm clearButton">Reset</a>
                            </div>
                        </div>
                        <div class="row mb-2">
                            <label for="ddlStatusFilter" class="col-sm-4 col-form-label label-font-standard">
                                Filter by Status:
                            </label>
                            <div class="col-sm-4">
                                <select id="ddlStatusFilter" class="form-select form-select-sm hardcodedSelect">
                                    <option value="">All</option>
                                    <option value="1">Approved</option>
                                    <option value="2">For Approval</option>
                                </select>
                            </div>
                        </div>

                        <div id="divIndustries" class="form-group pagerDiv">
                            <table id="tblIndustries" class="table table-sm" align="center">
                                <thead class="noScrollFixedHeader">
                                    <tr valign="middle" align="center">
                                        <th align="center" valign="middle" width="25%" data-name="" data-functions="view,edit,delete"
                                            data-alignment="center" class="hasButtons" data-pass="IndustryCode,InEdit,Status,TempStatusID" data-access="CanView.Access,CanUpdate.Access,CanDelete.Access">
                                        </th>
                                        <th align="center" valign="middle" width="10%" data-name="IndustryCode" data-alignment="center"
                                            data-columnname="IndustryCode" data-functions="" >
                                            Industry Code
                                        </th>
                                        <th align="center" valign="middle" width="15%" data-name="IndustryDesc" data-alignment="center"
                                            data-columnname="IndustryDesc" data-functions="" >
                                           Industry Description
                                        </th>
                                        <th align="center" valign="middle" width="15%" data-name="IndustryType" data-alignment="center"
                                            data-columnname="IndustryType" data-functions="" >
                                           Industry Type
                                        </th>
                                        <th align="center" valign="middle" width="10%" data-name="Status" data-alignment="center"
                                            data-columnname="Status">
                                            Status
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="tBodyIndustries" class="noScrollContent border align-middle">
                                </tbody>
                            </table>
                        </div>
                        <div class="text-end">
                            <a id="btnAdd" class="lbpControl btn-sm addButton canInsert">ADD NEW INDUSTRY</a>
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

        </div>
    </div>

    <div id="modalIndustry" class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false"
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
                        <label class="col-sm-3 col-form-label label-font-standard" for="txtIndustryCode">
                            Industry Code</label>
                        <div class="col-sm-9">
                            <input type="text" id="txtIndustryCode" class="form-control form-control-sm required requiredField "
                                autocomplete="off" <%--maxlength='<%#getFieldLength(FieldVariables.USERNAME, LengthSetting.MAX)%>'--%> />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <label class="col-sm-3 col-form-label label-font-standard" for="txtIndustryDesc">
                            Industry Description</label>
                        <div class="col-sm-9">
                            <input type="text" id="txtIndustryDesc" class="form-control form-control-sm required requiredField "
                                autocomplete="off" <%--maxlength='<%#getFieldLength(FieldVariables.USERNAME, LengthSetting.MAX)%>'--%> />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <label class="col-sm-3 col-form-label label-font-standard" for="ddlIndustryType">
                            IndustryType</label>
                        <div class="col-sm-9">
                            <select id="ddlIndustryType" class="form-select form-select-sm required requiredField hardcodedSelect">
                                <%--<option value="">Please Select</option>--%>
                                <option value="Good">Good</option>
                            </select>
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
                </div>
                <div id="divUserError" class="lbpControl bg-danger text-center text-light">
                    <span id="lblUserErrorMessage"></span>
                </div>
                <div id="divEditLabel" class="text-center" hidden>
                    <span class="col-form-label label-font-standard">Edit is disabled due to pending request</span>
                </div>
                <div class="modal-footer">
                    <a id="btnValidate" class="lbpControl clearButton" hidden>Validate Industry</a>
                    <a id="btnSave" class="lbpControl saveButton" hidden>Save</a> 
                    <a id="btnModalCacel" data-bs-dismiss="modal"
                        class="lbpControl cancelButton">Cancel</a>
                </div>
            </div>
        </div>
    </div>


    <div id="modalIndustryTemp" class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false"
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
