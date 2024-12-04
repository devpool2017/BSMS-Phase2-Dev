<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master" CodeBehind="RelationshipOfficer.aspx.vb" Inherits="LBP.VS2010.BSMS.RelationshipOfficer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Content/Scripts/PageScripts/Review/RelationshipOfficer.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="card p-3">
        <div class="accordion" id="accordionPanelsGroups">
            <div id="DivAddEditDeleteGroups" data-bs-toggle="collapse" data-bs-target="#collapseOne">
                <div class="card-header btn-lbp-green text-center rounded-0" id="Div5">
                    <h6 class="mb-0">
                         List of Relationship Officers
                    </h6>
                </div>
            </div>
            <div id="collapseOne" class="accordion-collapse show" aria-labelledby="headingOne">
                <div class="accordion-body">
                    <div id="divRelationshipOfficer" class="form-group pagerDiv">
                        <table id="tblRelationshipOfficer" class="table table-sm" align="center">
                            <thead class="noScrollFixedHeader">
                                <tr valign="middle" align="center">
                                    <th align="center" valign="middle" width="15%" data-name="" data-functions="view"
                                        data-alignment="center" class="hasButtons" data-pass="FirstName,MiddleInitial,LastName,Username,RegBrCode">
                                        <th align="center" valign="middle" width="15%" data-name="RegBrCode" data-alignment="center"
                                            data-columnname="RegBrCode">
                                            Group
                                        </th>
                                        <th align="center" valign="middle" width="15%" data-name="Fullname" data-alignment="center"
                                            data-columnname="Fullname">
                                            User's Fullname
                                        </th>
                                        <th align="center" valign="middle" width="15%" data-name="Username" data-alignment="center"
                                            data-columnname="User name">
                                            UserID
                                        </th>
                                </tr>
                            </thead>
                            <tbody id="tBodyRelationshipOfficer" class="noScrollContent border align-middle">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <div id="divAddGroupHead" class="text-end" hidden>
                <a id="btnAddGroupHead" class="lbpControl btn-sm addButton right">Create New Group Head</a>
            </div>
        </div>
          <%--MODAL View Group--%>
        <div id="divViewRelationshipOfficerModal" class="modal fade detailsModal" tabindex="-1" role="dialog"
            aria-hidden="true">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header alert alert-lbp">
                        <h5>
                            <span class="fa fa-cubes" style="vertical-align: -webkit-baseline-middle; color: #FFFFFF">
                            </span>&nbsp&nbsp</h5>
                        <h4 class="modal-title" style="color: #FFFFFF; text-align: left;" id="H3">
                            View Relationship Officer 
                        </h4>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" style="float: right;">
                        </button>
                        <label id="Label4" class="col-3 col-form-label label-font-standard" for="lblAction"
                            hidden>
                        </label>
                    </div>
                    <div id="div10" class="modal-body">
                        <div id="div11">
                            <div class="form-group">
                                <div class="form-group form-row">
                                    <label class="col-3 col-form-label label-font-standard">
                                        Position
                                    </label>
                                    <div class="col-xs-3 col-lg-3">
                                        <input type="text" id="txtViewPosition" class="lbpControl form-control" autocomplete="off"
                                            disabled="true" />
                                    </div>
                                </div>
                                <div class="form-group form-row">
                                    <label class="col-3 col-form-label label-font-standard">
                                        First Name
                                    </label>
                                    <div class="col-xs-5 col-lg-5">
                                        <input type="text" id="txtViewFirstName" class="lbpControl form-control required"
                                            autocomplete="off" disabled="true" />
                                    </div>
                                </div>
                                <div class="form-group form-row">
                                    <label class="col-3 col-form-label label-font-standard">
                                        Middle Initial
                                    </label>
                                    <div class="col-xs-5 col-lg-5">
                                        <input type="text" id="txtViewMiddleInitial" class="lbpControl form-control required"
                                            autocomplete="off"  disabled="true"/>
                                    </div>
                                </div>
                                <div class="form-group form-row">
                                    <label class="col-3 col-form-label label-font-standard">
                                        Last Name
                                    </label>
                                    <div class="col-xs-5 col-lg-5">
                                        <input type="text" id="txtViewLastName" class="lbpControl form-control required"
                                            autocomplete="off"  disabled="true"/>
                                    </div>
                                </div>
                                <div class="form-group form-row">
                                    <label class="col-3 col-form-label label-font-standard">
                                        Group
                                    </label>
                                    <div class="col-xs-5 col-lg-5">
                                        <input type="text" id="txtViewGroup" class="lbpControl form-control required"
                                            autocomplete="off" disabled="true" />
                                    </div>
                                </div>
                                <div class="form-group form-row">
                                    <label class="col-3 col-form-label label-font-standard">
                                        User ID
                                    </label>
                                    <div class="col-xs-5 col-lg-5">
                                        <input type="text" id="txtViewUserID" class="lbpControl form-control required" autocomplete="off" disabled="true" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="div12" class="form-group lbpControl" tabindex="-1">
                        <label id="Label5" style="display: none; cursor: default; width: 100%; text-align: center"
                            class="lbpControl btn btn-danger btn-block">
                        </label>
                    </div>
                    
                </div>
            </div>
        </div>
    </div>
</asp:Content>
