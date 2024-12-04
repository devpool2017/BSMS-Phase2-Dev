<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master" CodeBehind="Industries.aspx.vb" Inherits="LBP.VS2010.BSMS.Industries" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Content/Scripts/PageScripts/Review/Industries.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <div class="card p-3">
        <div class="accordion" id="accordionPanelsBranchProducts">
            <div id="DivBranch" data-bs-toggle="collapse" data-bs-target="#collapseOne">
                <div class="card-header btn-lbp-green text-center rounded-0" id="Div5">
                    <h6 class="mb-0">
                        List of Industries
                    </h6>
                </div>
            </div>
            <div id="collapseOne" class="accordion-collapse show" aria-labelledby="headingOne">
                <div class="accordion-body">
                    <div id="divIndustries" class="form-group pagerDiv" hidden>
                        <table id="tblIndustries" class="table table-sm col-12" align="center">
                            <thead class="noScrollFixedHeader">
                                <tr valign="middle" align="center">
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
                                </tr>
                            </thead>
                            <tbody id="tBodyIndustries" class="noScrollContent border align-middle">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
