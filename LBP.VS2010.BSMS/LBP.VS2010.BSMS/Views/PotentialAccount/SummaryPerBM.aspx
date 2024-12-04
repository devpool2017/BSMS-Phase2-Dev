<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master"
    CodeBehind="SummaryPerBM.aspx.vb" Inherits="LBP.VS2010.BSMS.SummaryPerBM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Content/Scripts/PageScripts/CPA/SummaryPerBM.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card p-3">
        <div class="accordion" id="accordionPanelsSummaryBM">
            <div id="DivBranchManager" data-bs-toggle="collapse" data-bs-target="#collapseOne">
                <div class="card-header btn-lbp-green text-center rounded-0" id="Div5">
                    <h6 class="mb-0">
                        Potential Accounts Per Branch Head
                    </h6>
                </div>
            </div>
        </div>
        <div id="collapseOne" class="accordion-collapse show" aria-labelledby="headingOne">
            <div class="accordion-body">
                <br />
                <div class="form-group form-row">
                    <label class="col-sm-1 col-form-label label-font-standard" for="ddlReportList">
                        Group
                    </label>
                    <div class="col-sm-4">
                        <select id="ddlRegion" class="form-select form-   select-sm hardcodedSelect">
                        </select>
                    </div>
                      <label class="col-sm-1 col-form-label label-font-standard" for="ddlReportList">
                        Branch
                    </label>
                    <div class="col-sm-4">
                        <select id="ddlBranches" class="form-select form-select-sm hardcodedSelect">
                        </select>
                    </div>
                      <a id="btnSearch" class="lbpControl searchButton btn-sm-1">Search</a> <a id="btnFilter"
                        class="lbpControl addButton btn-sm-1" hidden>Add Filter</a> <a id="btnRemoveFilter"
                            class="lbpControl subButton btn-sm-1" hidden>Remove Filter</a>
                </div>
                <br />
                <div id="divSummaryBM" class="form-group pagerDiv">
                    <table id="tblSummaryBM" class="table table-sm" align="center">
                        <thead class="noScrollFixedHeader">
                            <tr valign="middle" align="center">
                                <th align="center" valign="middle" width="15%" data-name="" data-functions="view"
                                    data-access="CanView.Access" data-alignment="center" class="hasButtons" data-pass="Fullname,Branch,Position,UserID">
                                    Action
                                </th>
                                <th align="center" valign="middle" width="13%" data-name="Fullname" data-alignment="center"
                                    data-columnname="Fullname">
                                    Name
                                </th>
                                 <th align="center" valign="middle" width="10%" data-name="RegionName" data-alignment="center"
                                    data-columnname=" RegionName">
                                    Region Name
                                </th>
                                <th align="center" valign="middle" width="15%" data-name="Branch" data-alignment="center"
                                    data-columnname=" Branch">
                                    Branch Name
                                </th>
                                <th align="center" valign="middle" width="10%" data-name="Position" data-alignment="center"
                                    data-columnname=" Position">
                                    Position
                                </th>
                                <th align="center" valign="middle" width="8%" data-name="UserID" data-alignment="center"
                                    data-columnname="UserID">
                                    User ID
                                </th>
                            </tr>
                        </thead>
                        <tbody id="tBodySummaryBM" class="noScrollContent border align-middle">
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
