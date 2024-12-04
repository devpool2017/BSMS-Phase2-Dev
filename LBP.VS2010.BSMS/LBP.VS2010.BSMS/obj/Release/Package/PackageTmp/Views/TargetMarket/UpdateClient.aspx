<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master"
    CodeBehind="UpdateClient.aspx.vb" Inherits="LBP.VS2010.BSMS.UpdateClient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Content/Scripts/PageScripts/TargetMarket/UpdateClient.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card p-3">
        <div class="accordion" id="accordionPanelsBranchProducts">
            <div id="DivBranch" data-bs-toggle="collapse" data-bs-target="#collapseOne">
                <div class="card-header btn-lbp-green text-center rounded-0" id="Div5">
                    <h6 class="mb-0">Update New Clients
                    </h6>
                </div>
            </div>
            <div id="collapseOne" class="accordion-collapse show" aria-labelledby="headingOne">
                <div class="accordion-body">
                    <div class="form-group form-row">
                        <label class="col-sm-1 col-form-label label-font-standard" for="ddlReportList">
                            Year
                        </label>
                        <div class="col-sm-2">
                            <select id="ddlYear" class="form-select form-select-sm hardcodedSelect">
                            </select>
                        </div>
                        <label class="col-sm-1 col-form-label label-font-standard" for="ddlApplication">
                            Month
                        </label>
                        <div class="col-sm-2">
                            <select id="ddlMonth" class="form-select form-select-sm hardcodedSelect">
                                <option value="">None</option>
                                <option value="1">January</option>
                                <option value="2">February</option>
                                <option value="3">March</option>
                                <option value="4">April</option>
                                <option value="5">May</option>
                                <option value="6">June</option>
                                <option value="7">July</option>
                                <option value="8">August</option>
                                <option value="9">September</option>
                                <option value="10">October</option>
                                <option value="11">November</option>
                                <option value="12">December</option>
                            </select>
                        </div>
                        <label class="col-sm-1 col-form-label label-font-standard" for="ddlApplication">
                            Week
                        </label>
                        <div class="col-sm-2">
                            <select id="ddlWeek" class="form-select form-select-sm hardcodedSelect">
                            </select>
                        </div>
                        <a id="btnSearch" class="lbpControl searchButton btn-sm-1">Search</a>
                        <a id="btnAddFilter" class="lbpControl addButton col-sm-1">Filter</a>
                        <a id="btnSubFilter" class="lbpControl subButton col-sm-1" hidden>Filter</a>
                    </div>

                    <div id="divFilters" style="display: none;">

                        <div id="divAddFilter" class="form-group form-row">
                            <label class="col-sm-1 col-form-label label-font-standard">
                                Filter By
                            </label>
                            <div class="col-sm-2">
                                <select id="ddlFilter" class="form-select form-select-sm hardcodedSelect">
                                    <option value="">Please Select</option>
                                    <option value="ClientType">Client Type</option>
                                    <option value="Status">Status</option>
                                </select>
                            </div>
                            <div id="divClientType" class="col-sm-2" hidden>
                                <select id="ddlClientType" class="form-select form-select-sm hardcodedSelect">
                                </select>
                            </div>
                            <div id="divStatus" class="col-sm-2" >
                                <select id="ddlStatus" class="form-select form-select-sm hardcodedSelect" disabled>
                                    <option value="">Please Select</option>
                                    <option value="Lead">Lead</option>
                                    <option value="Suspect">Suspect</option>
                                    <option value="Prospect">Prospect</option>
                                    <option value="Customer">Customer</option>
                                    <option value="Lost">Lost</option>
                                </select>
                            </div>

                            <div class="col-sm-1">
                            </div>

                            <label id="lbl1" class="col-sm-1 col-form-label label-font-standard">
                                Sort By
                            </label>
                            <div id="divSort" class="col-sm-2">
                                <select id="ddlSort" class="form-select form-select-sm hardcodedSelect">
                                    <option value="">Please Select</option>
                                    <option value="Fullname">Fullname</option>
                                    <option value="ClientType">Client Type</option>
                                    <option value="Lead">Lead</option>
                                    <option value="Suspect">Suspect</option>
                                    <option value="Prospect">Prospect</option>
                                    <option value="Customer">Customer</option>
                                    <option value="Lost">Lost</option>
                                </select>
                            </div>
                            <div id="divAssDes" class="col-sm-2">
                                <select id="ddlAssDes" class="form-select form-select-sm hardcodedSelect" disabled>
                                    <option value="ASC">Ascending</option>
                                    <option value="DESC">Descending</option>
                                </select>
                            </div>
                        </div>

                        <div id="divAddFilter1" class="form-group form-row">
                            <div class="col-sm-1">
                                <select id="ddlWhere1" class="form-select form-select-sm hardcodedSelect" disabled>
                                    <option value="AND">AND</option>
                                    <option value="OR">OR</option>
                                </select>
                            </div>
                            <div class="col-sm-2">
                                <select id="ddlFilter1" class="form-select form-select-sm hardcodedSelect" disabled>
                                    <option value="">Please Select</option>
                                    <option value="ClientType">Client Type</option>
                                    <option value="Status">Status</option>
                                </select>
                            </div>
                            <div id="divClientType1" class="col-sm-2" hidden>
                                <select id="ddlClientType1" class="form-select form-select-sm hardcodedSelect">
                                </select>
                            </div>
                            <div id="divStatus1" class="col-sm-2" >
                                <select id="ddlStatus1" class="form-select form-select-sm hardcodedSelect" disabled>
                                    <option value="">Please Select</option>
                                    <option value="Lead">Lead</option>
                                    <option value="Suspect">Suspect</option>
                                    <option value="Prospect">Prospect</option>
                                    <option value="Customer">Customer</option>
                                    <option value="Lost">Lost</option>
                                </select>
                            </div>

                            <div class="col-sm-1">
                            </div>

                            <label id="lbl2" class="col-sm-1 col-form-label label-font-standard" >
                                Then By
                            </label>
                            <div id="divSort1" class="col-sm-2" >
                                <select id="ddlSort1" class="form-select form-select-sm hardcodedSelect" disabled>
                                    <option value="">Please Select</option>
                                    <option value="Fullname">Fullname</option>
                                    <option value="ClientType">Client Type</option>
                                    <option value="Lead">Lead</option>
                                    <option value="Suspect">Suspect</option>
                                    <option value="Prospect">Prospect</option>
                                    <option value="Customer">Customer</option>
                                    <option value="Lost">Lost</option>
                                </select>
                            </div>
                            <div id="divAssDes1" class="col-sm-2" >
                                <select id="ddlAssDes1" class="form-select form-select-sm hardcodedSelect" disabled>
                                    <option value="ASC">Ascending</option>
                                    <option value="DESC">Descending</option>
                                </select>
                            </div>
                        </div>
                        <div id="divAddFilter2" class="form-group form-row">
                            <div class="col-sm-1">
                                <select id="ddlWhere2" class="form-select form-select-sm hardcodedSelect" disabled>
                                    <option value="AND">AND</option>
                                    <option value="OR">OR</option>
                                </select>
                            </div>
                            <div class="col-sm-2">
                                <select id="ddlFilter2" class="form-select form-select-sm hardcodedSelect" disabled>
                                    <option value="">Please Select</option>
                                    <option value="ClientType">Client Type</option>
                                    <option value="Status">Status</option>
                                </select>
                            </div>
                            <div id="divClientType2" class="col-sm-2" hidden>
                                <select id="ddlClientType2" class="form-select form-select-sm hardcodedSelect">
                                </select>
                            </div>
                            <div id="divStatus2" class="col-sm-2">
                                <select id="ddlStatus2" class="form-select form-select-sm hardcodedSelect" disabled>
                                    <option value="">Please Select</option>
                                    <option value="Lead">Lead</option>
                                    <option value="Suspect">Suspect</option>
                                    <option value="Prospect">Prospect</option>
                                    <option value="Customer">Customer</option>
                                    <option value="Lost">Lost</option>
                                </select>
                            </div>

                            <div class="col-sm-1">
                            </div>

                            <label id="lbl3" class="col-sm-1 col-form-label label-font-standard" >
                                Then By
                            </label>
                            <div id="divSort2" class="col-sm-2" >
                                <select id="ddlSort2" class="form-select form-select-sm hardcodedSelect" disabled>
                                    <option value="">Please Select</option>
                                    <option value="Fullname">Fullname</option>
                                    <option value="ClientType">Client Type</option>
                                    <option value="Lead">Lead</option>
                                    <option value="Suspect">Suspect</option>
                                    <option value="Prospect">Prospect</option>
                                    <option value="Customer">Customer</option>
                                    <option value="Lost">Lost</option>
                                </select>
                            </div>
                            <div id="divAssDes2" class="col-sm-2" >
                                <select id="ddlAssDes2" class="form-select form-select-sm hardcodedSelect" disabled>
                                    <option value="ASC">Ascending</option>
                                    <option value="DESC">Descending</option>
                                </select>
                            </div>
                        </div>

                        <div id="divName" class="form-group form-row">
                            <label class="col-sm-4 col-form-label label-font-standard">
                                Search Customer (by First Name or Last Name)
                            </label>
                            <div class="col-sm-4">
                                <input type="text" id="txtName" class="form-control form-control-sm" autocomplete="off" />
                            </div>
                        </div>

                    </div>
                    <br />
                    <div id="divUpdateClientList" class="form-group pagerDiv" hidden>
                        <table id="tblUpdateClientList" class="table table-sm" align="center">
                            <thead class="noScrollFixedHeader">
                                <tr valign="middle" align="center">
                                    <%--   <th align="center" valign="middle" width="10%" data-name="" data-functions="edit"
                                        data-alignment="center" class="hasButtons" data-pass="ClientID,Fullname,ClientType,Lead,Suspect,Prospect,Customer,Lost,Lname,Fname,Mname" />--%>
                                    <th align="center" valign="middle" width="10%" data-name="" data-functions="edit"
                                        data-alignment="center" class="hasButtons" data-pass="ClientID" />
                                    <th align="center" valign="middle" width="15%" data-name="Fullname" data-alignment="center"
                                        data-columnname="Fullname">Full Name
                                    </th>
                                    <th align="center" valign="middle" width="15%" data-name="ClientType" data-alignment="center"
                                        data-columnname="ClientType">Client Type
                                    </th>
                                    <th align="center" valign="middle" width="10%" data-name="Lead" data-alignment="center"
                                        data-columnname="Lead">Lead
                                    </th>
                                    <th align="center" valign="middle" width="10%" data-name="Suspect" data-alignment="center"
                                        data-columnname="Suspect">Suspect
                                    </th>
                                    <th align="center" valign="middle" width="10%" data-name="Prospect" data-alignment="center"
                                        data-columnname="Prospect">Prospect
                                    </th>
                                    <th align="center" valign="middle" width="10%" data-name="Customer" data-alignment="center"
                                        data-columnname="Customer">Customer
                                    </th>
                                    <th align="center" valign="middle" width="10%" data-name="Lost" data-alignment="center"
                                        data-columnname="Lost">Lost
                                    </th>
                                </tr>
                            </thead>
                            <tbody id="tBodyUpdateClientList" class="noScrollContent border align-middle">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
