<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master"
    CodeBehind="Dates.aspx.vb" Inherits="LBP.VS2010.BSMS.Dates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Content/Scripts/PageScripts/Review/Dates.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card p-3">
        <div class="accordion" id="accordionPanelsGroups">
            <div id="DivBranchProduct" data-bs-toggle="collapse" data-bs-target="#collapseOne">
                <div class="card-header btn-lbp-green text-center rounded-0" id="Div5">
                    <h6 class="mb-0">
                        Dates
                    </h6>
                </div>
            </div>
            <div id="collapseOne" class="accordion-collapse show" aria-labelledby="headingOne">
                <div class="accordion-body">
                    <div id="divSearch">
                        <div class="form-group form-row">
                            <label class="col-1 col-form-label label-font-standard" for="ddlSearchBy">
                                Year
                            </label>
                            <div class="col-xs-3 col-lg-3">
                                <select id="ddlSearchBy" class="form-select hardcodedSelect">
                                </select>
                            </div>
                            <div id="divButtons" class="col-xs-3 col-lg-3">
                                <a id="btnAddDate" class="lbpControl addButton">Add Date</a>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div id="divDatesLists" class="form-group pagerDiv">
                        <table id="tblDatesLists" class="table table-sm" align="center">
                            <thead class="noScrollFixedHeader">
                                <tr valign="middle" align="center">
                                        <th align="center" valign="middle" width="15%" data-name="Year" data-alignment="center"
                                            data-columnname="Year">
                                            Year
                                        </th>
                                        <th align="center" valign="middle" width="15%" data-name="Month" data-alignment="center"
                                            data-columnname="Month">
                                            Month
                                        </th>
                                        <th align="center" valign="middle" width="15%" data-name="Week" data-alignment="center"
                                            data-columnname="Week">
                                            Week
                                        </th>
                                        <th align="center" valign="middle" width="15%" data-name="FromDate" data-alignment="center"
                                            data-columnname="FromDate">
                                            From Date
                                        </th>
                                        <th align="center" valign="middle" width="15%" data-name="ToDate" data-alignment="center"
                                            data-columnname="ToDate">
                                            To Date
                                        </th>
                                </tr>
                            </thead>
                            <tbody id="tBodyDatesList" class="noScrollContent border align-middle">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%--MODAL Add Group--%>' 
    <div id="divAddDateModal" class="modal fade " data-bs-backdrop="static" data-bs-keyboard="false"
                tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header alert alert-lbp">
                    <h5>
                        <span class="fa fa-cubes" style="vertical-align: -webkit-baseline-middle; color: #FFFFFF">
                        </span>&nbsp&nbsp</h5>
                    <h4 class="modal-title" style="color: #FFFFFF; text-align: left;" id="H1">
                        Add Dates
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
                                    Year
                                </label>
                                <div class="col-xs-2 col-lg-3">
                                    <select id="ddlYear" class="form-control hardcodedSelect">
                                    </select>
                                </div>
                            </div>
                            <div class="form-group form-row">
                                <label class="col-3 col-form-label label-font-standard">
                                    Month
                                </label>
                                <div class="col-xs-5 col-lg-5">
                                    <select id="ddlMonth" class="form-control hardcodedSelect" disabled="true">
                                        <option value="">Please Select Month</option>
                                        <option value="01">January</option>
                                        <option value="02">February</option>
                                        <option value="03">March</option>
                                        <option value="04">April</option>
                                        <option value="05">May</option>
                                        <option value="06">June</option>
                                        <option value="07">July</option>
                                        <option value="08">August</option>
                                        <option value="09">September</option>
                                        <option value="10">October</option>
                                        <option value="11">November</option>
                                        <option value="12">December</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group form-row">
                                <label class="col-3 col-form-label label-font-standard">
                                    Week
                                </label>
                                <div class="col-xs-5 col-lg-5">
                                    <select id="ddlWeek" class="form-control hardcodedSelect" disabled="disabled">
                                        <option value="">Please Select Week</option>
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                        <option value="3">3</option>
                                        <option value="4">4</option>
                                        <option value="5">5</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group form-row">
                                <label class="col-3 col-form-label label-font-standard">
                                    From Date
                                </label>
                                <div class="col-xs-5 col-lg-5">
                                    <input type="text" id="txtFromDate" class="lbpControl form-control" disabled="disabled"
                                        autocomplete="off" />
                                </div>
                            </div>
                            <div class="form-group form-row">
                                <label class="col-3 col-form-label label-font-standard">
                                    To Date
                                </label>
                                <div class="col-xs-5 col-lg-5">
                                    <input type="text" id="txtToDate" class="lbpControl form-control" disabled="disabled"
                                        autocomplete="off" />
                                </div>
                            </div>
                            <div id="divModalDate" class="form-group pagerDiv" style="height:250px; position: relative; overflow: auto; overflow-x: hidden;" hidden> 
                                <table id="tblModalDate" class="table table-sm" align="center">
                                    <thead class="noScrollFixedHeader">
                                        <tr valign="middle" align="center">
                                                <th align="center" valign="middle" width="15%" data-name="Year" data-alignment="center"
                                                    data-columnname="Year">
                                                    Year
                                                </th>
                                                <th align="center" valign="middle" width="15%" data-name="Month" data-alignment="center"
                                                    data-columnname="Month">
                                                    Month
                                                </th>
                                                <th align="center" valign="middle" width="15%" data-name="Week" data-alignment="center"
                                                    data-columnname="Week">
                                                    Week
                                                </th>
                                                <th align="center" valign="middle" width="15%" data-name="FromDate" data-alignment="center"
                                                    data-columnname="FromDate">
                                                    From Date
                                                </th>
                                                <th align="center" valign="middle" width="15%" data-name="ToDate" data-alignment="center"
                                                    data-columnname="ToDate">
                                                    To Date
                                                </th>
                                        </tr>
                                    </thead>
                                    <tbody id="tBodyModalDate" class="noScrollContent border align-middle">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                 <div id="divModalError" class="form-group lbpControl" tabindex="-1">
                        <label id="lblModalErrorMessage" style="display: none; cursor: default; width: 100%; text-align: center"
                            class="lbpControl btn btn-danger btn-block">
                        </label>
                    </div>
                <div class="modal-footer" id="div6">
                    <a id="btnAddSave" class="lbpControl saveButton">Save</a> <a id="btnAddCancel" class="lbpControl cancelButton">
                        Cancel</a><a id="btnAddReset" class="lbpControl clearButton"> Reset</a>
                </div>
            </div>
        </div>
    </div>

       <script type="text/javascript">
           sessionStorage.Role = '<%=Session("Role")%>'
    </script>
</asp:Content>
