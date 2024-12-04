<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master"
    CodeBehind="BankProduct.aspx.vb" Inherits="LBP.VS2010.BSMS.BankProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Content/Scripts/PageScripts/Review/BankProduct.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card p-3">
        <div class="accordion" id="accordionPanelsBranchProducts">
            <div id="DivBranch" data-bs-toggle="collapse" data-bs-target="#collapseOne">
                <div class="card-header btn-lbp-green text-center rounded-0" id="Div5">
                    <h6 class="mb-0">
                        List of Bank Products
                    </h6>
                </div>
            </div>
            <div id="collapseOne" class="accordion-collapse show" aria-labelledby="headingOne">
                <div class="accordion-body">
                       <div id="divSearch">
                        <div class="form-group form-row">
                            <label class="col-1 col-form-label label-font-standard" for="ddlSearchBy">
                                Search By
                            </label>
                            <div class="col-xs-5 col-lg-5">
                                <select id="ddlSearchBy" class="form-select hardcodedSelect">
                                 <%--   <option value="All">All</option>--%>
                                </select>
                            </div>
                          <%--  <div id="divButtons" class="col-xs-4 col-lg-4">
                                <a id="btnSearch" class="lbpControl searchButton">Search</a>
                            </div>--%>
                        </div>
                    </div>
                       <br />
                    <div id="divBankProduct" class="form-group pagerDiv" hidden>
                        <table id="tblBankProduct" class="table table-sm col-12" align="center">
                            <thead class="noScrollFixedHeader">
                                <tr valign="middle" align="center">
                                    <th align="center" valign="middle" width="10%" data-name="CASACodes" data-alignment="center"
                                        data-columnname="CASACodes" data-functions="" >
                                        BSMS Codes
                                    </th>
                                    <th align="center" valign="middle" width="15%" data-name="ProductType" data-alignment="center"
                                        data-columnname="ProductType" data-functions="" >
                                        Bank Products and Services
                                    </th>
                                    <th align="center" valign="middle" width="15%" data-name="ShortName" data-alignment="center"
                                        data-columnname="ShortName" data-functions="" >
                                        Short Name
                                    </th>
                                    <th align="center" valign="middle" width="15%" data-name="ProductGroup" data-alignment="center"
                                        data-columnname="ProductGroup" data-functions="" >
                                        Product Group
                                    </th>
                                    <th align="center" valign="middle" width="15%" data-name="ProductCategory" data-alignment="center"
                                        data-columnname="ProductCategory" data-functions="" >
                                        Product Category
                                    </th>
                                </tr>
                            </thead>
                            <tbody id="tBodyBankProduct" class="noScrollContent border align-middle">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
