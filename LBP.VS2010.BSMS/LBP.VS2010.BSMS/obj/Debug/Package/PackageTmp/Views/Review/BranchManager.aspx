<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master"
    CodeBehind="BranchManager.aspx.vb" Inherits="LBP.VS2010.BSMS.BranchManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Content/Scripts/PageScripts/Review/BranchManager.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card p-3">
        <div class="accordion" id="accordionPanelsGroups">
            <div id="DivBranchProduct" data-bs-toggle="collapse" data-bs-target="#collapseOne">
                <div class="card-header btn-lbp-green text-center rounded-0" id="Div5">
                    <h6 class="mb-0">
                       List of Branch Heads
                    </h6>
                </div>
            </div>
            <div id="collapseOne" class="accordion-collapse show" aria-labelledby="headingOne">
                <div class="accordion-body">
                    <div id="divSearch">
                        <div class="form-group form-row">
                            <%--  <label class="col-1 col-form-label label-font-standard" for="ddlSearchBy">
                                Search By
                            </label>
                            <div class="col-xs-3 col-lg-3">
                                <select id="ddlSearchBy" class="form-control hardcodedSelect">
                                    <option value="">All</option>
                                    <option value="8">Branch Manager</option>
                                </select>
                            </div>--%>
                            <label class="col-1 col-form-label label-font-standard" for="ddlSearchBy">
                                Group
                            </label>
                            <div class="col-sm-4">
                                <select id="ddlRegion" class="form-select form-select-sm">
                                    <option value=""></option>
                                </select>
                            </div>
                            <label class="col-1 col-form-label label-font-standard" for="ddlSearchBy">
                                Branch
                            </label>
                            <div class="col-xs-3 col-lg-3">
                                <select id="ddlBranches" class="form-select hardcodedSelect">
                                    <option value=""></option>
                                </select>
                            </div>
                            <div id="divButtons" class="col-xs-3 col-lg-3">
                                <a id="btnSearch" class="lbpControl searchButton">Search</a>
                                <%-- <a id="btnClear" class="lbpControl clearButton">Clear</a>--%>
                            </div>
                        </div>
                    </div>
                    <div id="divGroupHead" class="form-group form-row" hidden>
                        <label class="col-1 col-form-label label-font-standard">
                            Group Head
                        </label>
                        <div class="col-xs-3 col-lg-3">
                            <input type="text" id="txtGroupHead" class="lbpControl form-control" disabled="true" />
                        </div>
                    </div>
                    <br />
                    <br />
                    <div id="divBranchManager" class="form-group pagerDiv">
                        <table id="tblBranchManager" class="table table-sm" align="center">
                            <thead class="noScrollFixedHeader">
                                <tr valign="middle" align="center">
                                    <%--<th align="center" valign="middle" width="15%" data-name="" data-functions="view"
                                        data-alignment="center" class="hasButtons" data-pass="FirstName,MiddleInitial,LastName,Username,RegBrCode"/>--%>
                                    <th align="center" valign="middle" width="15%" data-name="Branch" data-alignment="center"
                                        data-columnname="Branch">
                                        Branch
                                    </th>
                                    <th align="center" valign="middle" width="15%" data-name="Fullname" data-alignment="center"
                                        data-columnname="Fullname">
                                        Name
                                    </th>
                                    <th align="center" valign="middle" width="13%" data-name="Position" data-alignment="center"
                                        data-columnname="Position">
                                        Position
                                    </th>
                                    <th align="center" valign="middle" width="10%" data-name="UserID" data-alignment="center"
                                        data-columnname="UserID">
                                        UserID
                                    </th>
                                    <th align="center" valign="middle" width="10%" data-name="NewCASATarget" data-alignment="center"
                                        data-columnname="NewCASATarget">
                                        Target - CASA
                                    </th>
                                    <th align="center" valign="middle" width="10%" data-name="NewCBGTarget" data-alignment="center"
                                        data-columnname="NewCBGTarget">
                                        Target - Loans
                                    </th>
                                 <%--   <th align="center" valign="middle" width="10%" data-name="ExistingCASATarget" data-alignment="center"
                                        data-columnname="ExistingCASATarget">
                                        Target - Existing Clients CASA
                                    </th>
                                    <th align="center" valign="middle" width="10%" data-name="ExistingCBGTarget" data-alignment="center"
                                        data-columnname="ExistingCBGTarget">
                                        Target - Existing Clients CBG
                                    </th>--%>
                                </tr>
                            </thead>
                            <tbody id="tBodyBranchManager" class="noScrollContent border align-middle">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <%--   <div id="divAddGroupHead" class="text-end" hidden>
                <a id="btnAddGroupHead" class="lbpControl btn-sm addButton right">Create New Group Head</a>
            </div>--%>
        </div>
    </div>
</asp:Content>
