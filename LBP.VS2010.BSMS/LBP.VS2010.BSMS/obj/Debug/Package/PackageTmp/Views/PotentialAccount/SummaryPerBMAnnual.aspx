<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master"
    CodeBehind="SummaryPerBMAnnual.aspx.vb" Inherits="LBP.VS2010.BSMS.SummaryPerBMAnnual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Content/Scripts/PageScripts/CPA/SummaryPerBMAnnual.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card p-3">
        <div class="accordion" id="accordionPanelsSummaryAnnual">
            <div id="divAnnualSummary" data-bs-toggle="collapse" data-bs-target="#collapseTwo">
                <div class="card-header btn-lbp-green text-center rounded-0" id="Div3">
                    <h6 class="mb-0">
                        <label id="lblBranchManager" class="col-sm-10 col-form-label" style="color: White;font-weight:bold">
                        </label>
                    </h6>
                </div>
            </div>
        </div>
        <div id="collapseTwo" class="accordion-collapse show" aria-labelledby="headingTwo">
            <div class="accordion-body">
                <br />
                <div class="form-group form-row">
                    <label id="Label1" class="col-sm-1 col-form-label" style="color: Red; font-size: small">
                        NOTE:
                    </label>
                    <label id="Label2" class="col-sm-6 col-form-label" style="color: Black; font-size: small">
                        Only Potential Accounts tagged as Prospect for the current year are allowed to be selected.
                    </label>
                </div>
                <div class="form-group form-row">
                    <label class="col-sm-1 col-form-label label-font-standard" for="ddlReportList">
                        Lead
                    </label>
                    <div class="col-sm-2">
                        <select id="ddlLead" class="form-select form-select-sm hardcodedSelect">
                            <option value="">All</option>
                            <option value="No">No</option>
                            <option value="Yes">Yes</option>
                        </select>
                    </div>
                    <label class="col-sm-1 col-form-label label-font-standard" for="ddlReportList">
                        Prospect
                    </label>
                    <div class="col-sm-2">
                        <select id="ddlProspect" class="form-select form-select-sm hardcodedSelect">
                            <option value="">All</option>
                            <option value="No">No</option>
                            <option value="Yes">Yes</option>
                        </select>
                    </div>
                    <label class="col-sm-1 col-form-label label-font-standard" for="ddlReportList">
                        Customer
                    </label>
                    <div class="col-sm-2">
                        <select id="ddlCustomer" class="form-select form-select-sm hardcodedSelect">
                            <option value="">All</option>
                            <option value="No">No</option>
                            <option value="Yes">Yes</option>
                        </select>
                    </div>
                    <a id="btnSearch" class="lbpControl searchButton btn-sm-1">Search</a>
                </div>
                <div id="divSummaryBMAnnual" class="form-group pagerDiv">
                    <table id="tblSummaryBMAnnual" class="table table-sm" align="center">
                        <thead class="noScrollFixedHeader">
                            <tr valign="middle" align="center">
                                <th align="center" valign="middle" width="8%" data-name="" data-functions="edit,delete"
                                    data-access="CanUpdate.Access,CanDelete.Access" data-alignment="center" class="hasButtons"
                                    data-pass="CPAID,Fullname,Industry,DateEncoded,Lead,Prospect,Customer,ClientID">
                                </th>
                                <th align="center" valign="middle" width="15%" data-name="Fullname" data-alignment="center"
                                    data-columnname="Fullname">
                                    Name
                                </th>
                                <th align="center" valign="middle" width="15%" data-name="Industry" data-alignment="center"
                                    data-columnname="Industry">
                                    Industry
                                </th>
                                <th align="center" valign="middle" width="8%" data-name="DateEncoded" data-alignment="center"
                                    data-columnname=" DateEncoded">
                                    Date Encoded
                                </th>
                                <th align="center" valign="middle" width="8%" data-name="Lead" data-alignment="center"
                                    data-columnname="Lead">
                                    IsLead?
                                </th>
                                <th align="center" valign="middle" width="10%" data-name="Prospect" data-alignment="center"
                                    data-columnname="Prospect">
                                    Already a Prospect?
                                </th>
                                <th align="center" valign="middle" width="10%" data-name="Customer" data-alignment="center"
                                    data-columnname="Customer">
                                    Already a Customer?
                                </th>
                            <%--    <th align="center" valign="middle" width="10%" data-name="ClientID" data-alignment="center"
                                    data-columnname="ClientID">
                                    ID
                                </th>--%>
                            </tr>
                        </thead>
                        <tbody id="tBodySummaryBMAnnual" class="noScrollContent border align-middle">
                        </tbody>
                        <tbody id="tBody-AnnuallyReportTotal" class="noScrollContent">
                            <tr id="totalLeads" valign="middle" align="center" style="background-color: #666666a1;
                                font-weight: bold;">
                                <td width="8%">
                                    Total
                                </td>
                                <td width="15%">
                                </td>
                                <td width="15%">
                                </td>
                                <td width="8%">
                                </td>
                                <td width="8%" id="lblTotalLead">
                                </td>
                                <td width="10%" id="lblTotalProspect">
                                </td>
                                <td width="10%" id="lblTotalCustomer">
                                </td>
                              <%--  <td width="10%">
                                </td>--%>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <br />
                <div id="div1" class="text-end">
                    <a id="btn-PrintReport" class="lbpControl btn-sm printButton right canPrint">Print Summary Table</a>
                    <a id="btn-Back" class="lbpControl btn-sm backButton right">Back</a>
                </div>
                <div id="divReport" class="mt-4">
                    <iframe name="ifrmReportViewer" width="100%" src="about:blank"></iframe>
                </div>
                <%--   <div class="form-group form-row">
                    <label class="col-sm-2 col-form-label label-font-standard" for="ddlReportList">
                        Total Lead
                    </label>
                    <div class="col-sm-1">
                        <input type="text" id="txtTotalLead" class="lbpControl form-control form-control-sm"
                            autocomplete="off" />
                    </div>
                    <label class="col-sm-2 col-form-label label-font-standard" for="ddlReportList">
                        Total Prospect
                    </label>
                    <div class="col-sm-1">
                        <input type="text" id="txtTotalProspect" class="lbpControl form-control form-control-sm"
                            autocomplete="off" />
                    </div>
                    <label class="col-sm-2 col-form-label label-font-standard" for="ddlReportList">
                        Total Customer
                    </label>
                    <div class="col-sm-1">
                        <input type="text" id="txtTotalCustomer" class="lbpControl form-control form-control-sm"
                            autocomplete="off" />
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
</asp:Content>
