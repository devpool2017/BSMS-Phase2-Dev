<%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master" CodeBehind="CreateNewClient.aspx.vb" Inherits="LBP.VS2010.BSMS.CreateNewClient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Content/Scripts/PageScripts/TargetMarket/CreateNewClient.js" type="text/javascript"></script>
    <script src="../../Content/Scripts/Commons/js-big-decimal.min.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="card p-3" style="overflow: scroll; max-height: 500px; width: 100%;">
        <div class="accordion" id="accordionPanelsStayOpenExample1">
            <div id="Div4" data-bs-toggle="collapse" data-bs-target="#collapseOne">
                <div class="card-header btn-lbp-green text-center rounded-0" id="Div5">
                    <h6 class="mb-0">Create New Client
                    </h6>
                </div>
            </div>
            <div id="collapseOne" class="accordion-collapse show" aria-labelledby="headingOne">
                <div class="accordion-body">

                    <div class="row mb-3">
                        <label class="col-sm-2 col-form-label label-font-standard" for="ddlClientType">
                            Client Type
                        </label>
                        <div class="col-sm-3">
                            <select id="ddlClientType" class="lbpControl form-select form-select-sm hardcodedSelect">
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <div class="form-check form-check-inline col-sm-2">
                            <input class="form-check-input required requiredField" type="checkbox" id="chkLead" value="1" name="chkStatus" />
                            <%--name="chkStatus" onclick="getCheckBoxStatus(this.checked, this.value)" />--%>
                            <label class="form-check-label label-font-standard" for="chkLead">
                                Lead</label>
                        </div>
                        <div class="col-sm-2">
                            <input type="text" id="txtLead" class="lbpControl form-control form-control-sm noForwardDate1Week
                                required requiredField"
                                placeholder="Date" autocomplete="off" readonly="readonly" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <label class="col-sm-2 col-form-label label-font-standard" for="txtFname" id="lblFName">
                            First Name
                        </label>
                        <div class="col-sm-3" id="divName">
                            <%--<input type="text" id="txtFname" class="lbpControl form-control form-control-sm required requiredField"
                                autocomplete="off" maxlength='<%#getFieldLength(FieldVariables.CLIENT_FIRST_NAME, LengthSetting.MAX)%>' />--%>
                        </div>
                        <div class="col-sm-1"></div>
                        <div class="form-check form-check-inline col-sm-2">
                            <input class="form-check-input" type="checkbox" id="chkSuspect" value="2" name="chkStatus" />
                            <%--onclick="getCheckBoxStatus(this.checked, this.value)" />--%>
                            <label class="form-check-label label-font-standard" for="chkSuspect">
                                Suspect</label>
                        </div>
                        <div class="col-sm-2">
                            <input type="text" id="txtSuspect" class="lbpControl form-control form-control-sm noForwardDate1Week"
                                placeholder="Date" autocomplete="off" readonly="readonly" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <label class="col-sm-2 col-form-label label-font-standard" for="txtMname" id="lblMiddleInitial">
                            Middle Initial
                        </label>
                        <div class="col-sm-1" id="divMI">
                            <%--<input type="text" id="txtMname" class="lbpControl form-control form-control-sm"
                                autocomplete="off" maxlength='<%#getFieldLength(FieldVariables.CLIENT_MIDDLE_INITIAL, LengthSetting.MAX)%>' />--%>
                        </div>
                        <div class="col-sm-3"></div>
                        <div class="form-check form-check-inline col-sm-2">
                            <input class="form-check-input" type="checkbox" id="chkProspect" value="3" name="chkStatus" />
                            <%--onclick="getCheckBoxStatus(this.checked, this.value)" />--%>
                            <label class="form-check-label label-font-standard" for="chkProspect">
                                Prospect</label>
                        </div>
                        <div class="col-sm-2">
                            <input type="text" id="txtProspect" class="lbpControl form-control form-control-sm noForwardDate1Week"
                                placeholder="Date" autocomplete="off" readonly="readonly" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <label class="col-sm-2 col-form-label label-font-standard" for="txtLname" id="lblLName">
                            Last Name
                        </label>
                        <div class="col-sm-3">
                            <input type="text" id="txtLname" class="lbpControl form-control form-control-sm required requiredField name"
                                autocomplete="off" maxlength='<%#getFieldLength(FieldVariables.CLIENT_LAST_NAME, LengthSetting.MAX)%>' />
                        </div>
                        <div class="col-sm-1"></div>
                        <div class="form-check form-check-inline col-sm-2">
                            <%--<input class="form-check-input" type="checkbox" name="rdDate" id="Checkbox2" value="" />--%>
                            <label class="form-check-label label-font-standard" for="txtVisits">
                                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp Visits</label>
                        </div>
                        <div class="col-sm-1">
                            <input type="text" id="txtVisits" class="lbpControl form-control form-control-sm wholenum"
                                maxlength='<%#getFieldLength(FieldVariables.CLIENT_VISITS, LengthSetting.MAX)%>'
                                readonly="readonly" value="0" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <label class="col-sm-2 col-form-label label-font-standard" for="ddlSource">
                            Source of Lead
                        </label>
                        <div class="col-sm-3">
                            <select id="ddlSource" class="lbpControl form-select form-select-sm hardcodedSelect">
                            </select>
                        </div>
                        <div class="col-sm-1"></div>
                        <div class="form-check form-check-inline col-sm-2">
                            <input class="form-check-input" type="checkbox" id="chkCustomer" value="4" name="chkStatus" />
                            <%-- onclick="getCheckBoxStatus(this.checked, this.value)" />--%>
                            <label class="form-check-label label-font-standard" for="chkCustomer">
                                Customer</label>
                        </div>
                        <div class="col-sm-2">
                            <input type="text" id="txtCustomer" class="lbpControl form-control form-control-sm noForwardDate1Week"
                                placeholder="Date" autocomplete="off" readonly="readonly" />
                        </div>

                    </div>

                    <div class="row mb-3">
                        <label class="col-sm-2 col-form-label label-font-standard" for="txtAddress">
                            Address
                        </label>
                        <div class="col-sm-6">
                            <input type="text" id="txtAddress" class="lbpControl form-control form-control-sm alphanumericWithSpecialChar"
                                autocomplete="off" maxlength='<%#getFieldLength(FieldVariables.CLIENT_ADDRESS, LengthSetting.MAX)%>' />
                        </div>

                        <div class="col-sm-3" id="divAccountInfo">
                            <a id="btnAccountInfo" class="btn btn-sm lbpControl viewButton" data-bs-toggle="modal"
                                data-bs-target="#accountInfoModal">Account Information</a>
                        </div>

                        <%--                        <div class="col-sm-3" id="divAccountInfo">
                            &nbsp&nbsp&nbsp&nbsp<a id="btnAccountInfo" class="btn btn-sm lbpControl viewButton" >Account Information</a>
                        </div>--%>
                    </div>

                    <div class="row mb-3">
                        <label class="col-sm-2 col-form-label label-font-standard" for="txtContactNo">
                            Contact No
                        </label>
                        <div class="col-sm-3">
                            <input type="text" id="txtContactNo" class="lbpControl form-control form-control-sm alphanumericWithSpecialChar"
                                autocomplete="off" maxlength='<%#getFieldLength(FieldVariables.CLIENT_CONTACT_NO, LengthSetting.MAX)%>' />
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-2 col-form-label label-font-standard" for="ddlIndustryType">
                            Industry Type
                        </label>
                        <div class="col-sm-4">
                            <select id="ddlIndustryType" class="lbpControl form-select form-select-sm hardcodedSelect">
                            </select>
                        </div>
                    </div>

                    <div class="row mb-3">
                        <label class="col-sm-2 col-form-label label-font-standard" for="txtAmount">
                            Amount
                        </label>
                        <div class="col-sm-3">
                            <input type="text" id="txtAmount" class="lbpControl form-control form-control-sm currency"
                                autocomplete="off" disabled="disabled" />
                        </div>
                        <div class="col-sm-1"></div>
                        <label class="col-sm-2 col-form-label label-font-standard" for="txtADB">
                            ADB
                        </label>
                        <div class="col-sm-3">
                            <input type="text" id="txtADB" class="lbpControl form-control form-control-sm currency"
                                autocomplete="off" disabled="disabled" />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <label class="col-sm-2 col-form-label label-font-standard" for="txtAmount">
                            Loan Amount Reported
                        </label>
                        <div class="col-sm-3">
                            <input type="text" id="txtLoanReported" class="lbpControl form-control form-control-sm currency"
                                autocomplete="off" disabled="disabled" />
                        </div>
                    </div>








                    <div class="row">
                        <label class="col-3 col-form-label label-font-title">
                            CASA Types Offered
                        </label>
                    </div>

                    <div class="row mb-3">
                        <div class="col-sm-2"></div>
                        <div id="divCASAProducts" class="row col-sm-10">

                            <%--<input type="checkbox" id="Radio2" class="requiredCheckbox" />
                            <label class="col-3 col-form-label label-font-standard" for="txtLastName">
                                SA-ATM
                            </label>
                            <input type="checkbox" id="Checkbox5" class="requiredCheckbox" />
                            <label class="col-3 col-form-label label-font-standard" for="txtLastName">
                                SA-PBK
                            </label>
                            <input type="checkbox" id="Checkbox6" class="requiredCheckbox" />
                            <label class="col-5 col-form-label label-font-standard" for="txtLastName">
                                SA-PBK/ATM
                            </label>
                            <input type="checkbox" id="Checkbox7" class="requiredCheckbox" />
                            <label class="col-3 col-form-label label-font-standard" for="txtLastName">
                                CA-REGULAR
                            </label>
                            <input type="checkbox" id="Checkbox8" class="requiredCheckbox" />
                            <label class="col-3 col-form-label label-font-standard" for="txtLastName">
                                MULTI-ONE
                            </label>
                            <input type="checkbox" id="Checkbox9" class="requiredCheckbox" />
                            <label class="col-5 col-form-label label-font-standard" for="txtLastName">
                                S2S
                            </label>
                            <input type="checkbox" id="Checkbox10" class="requiredCheckbox" />
                            <label class="col-3 col-form-label label-font-standard" for="txtLastName">
                                CHKSTRTR
                            </label>--%>
                        </div>
                    </div>


                    <div class="row">
                        <label class="col-3 col-form-label label-font-title">
                            Loan Products Offered
                        </label>
                    </div>
                    <div class="row mb-3">
                        <div class="col-sm-2"></div>
                        <%--<div class="form-group form-row">
                        <label class="col-2 col-form-label label-font-standard">
                            Loan Products Offered
                        </label>--%>
                        <div id="divLoanProducts" class='row col-sm-10'>
                            <%--<input type="checkbox" id="Checkbox11" class="requiredCheckbox" />
                            <label class="col-3 col-form-label label-font-standard" for="txtLastName">
                                CBG-HOME
                            </label>
                            <input type="checkbox" id="Checkbox12" class="requiredCheckbox" />
                            <label class="col-3 col-form-label label-font-standard" for="txtLastName">
                                CBG-AUTO
                            </label>
                            <input type="checkbox" id="Checkbox13" class="requiredCheckbox" />
                            <label class="col-5 col-form-label label-font-standard" for="txtLastName">
                                CBG-PL
                            </label>
                            <input type="checkbox" id="Checkbox14" class="requiredCheckbox" />
                            <label class="col-3 col-form-label label-font-standard" for="txtLastName">
                                CBG-SALARY
                            </label>
                            <input type="checkbox" id="Checkbox15" class="requiredCheckbox" />
                            <label class="col-3 col-form-label label-font-standard" for="txtLastName">
                                CBG-SBL
                            </label>
                            <input type="checkbox" id="Checkbox16" class="requiredCheckbox" />
                            <label class="col-5 col-form-label label-font-standard" for="txtLastName">
                                CBG-YES
                            </label>
                            <input type="checkbox" id="Checkbox17" class="requiredCheckbox" />
                            <label class="col-3 col-form-label label-font-standard" for="txtLastName">
                                CBG-SBL2
                            </label>
                            <input type="checkbox" id="Checkbox18" class="requiredCheckbox" />
                            <label class="col-3 col-form-label label-font-standard" for="txtLastName">
                                CBG-HOME2
                            </label>
                            <input type="checkbox" id="Checkbox19" class="requiredCheckbox" />
                            <label class="col-5 col-form-label label-font-standard" for="txtLastName">
                                CBG-SLRY2
                            </label>
                            <input type="checkbox" id="Checkbox20" class="requiredCheckbox" />
                            <label class="col-3 col-form-label label-font-standard" for="txtLastName">
                                FRANCHISE
                            </label>--%>
                        </div>
                    </div>


                    <div class="row">
                        <label class="col-3 col-form-label label-font-title">
                            Products & Services Offered
                        </label>
                    </div>

                    <div id="divProducts" class="form-group pagerDiv row mb-3">
                        <table id="tblProducts" class="table table-sm scrollTable" align="center">
                            <thead class="noScrollFixedHeader" id="tHeadProducts">
                                <tr valign="middle" align="center">
                                    <th align="center" valign="middle" width="15%" data-name="Traditional" data-alignment="center"
                                        data-columnname="Traditional">Traditional
                                    </th>
                                    <th align="center" valign="middle" width="15%" data-name="EBankingCore" data-alignment="center"
                                        data-columnname="E-Banking Core">E-Banking Core
                                    </th>
                                    <th align="center" valign="middle" width="15%" data-name="EBankingOthers" data-alignment="center"
                                        data-columnname="E-Banking Others">E-Banking (Others)
                                    </th>
                                    <th align="center" valign="middle" width="16%" data-name="CrossSelling" data-alignment="center"
                                        data-columnname="Cross Selling">Cross Selling
                                    </th>
                                </tr>
                            </thead>
                            <tbody id="tBodyProducts" class="scrollContent border align-middle" style="height: 150px; position: relative; overflow: auto; overflow-x: hidden;">
                            </tbody>
                        </table>
                    </div>




                    <%-- <div id="Div2" data-bs-toggle="collapse" data-bs-target="#collapseTwo">
                        <div class="card-header btn-lbp-green text-center rounded-0" id="Div3">
                            <h6 class="mb-0">Products & Services Offered
                            </h6>
                        </div>
                    </div>--%>
                    <%--                    <div id="collapseTwo" class="accordion-collapse show" aria-labelledby="headingTwo">
                        <div class="accordion-body">
                            <div id="divSMR" class="form-group pagerDiv">
                                <table id="tblSMR" class="table table-sm" align="center">
                                    <thead class="noScrollFixedHeader">
                                        <tr valign="middle" align="center">--%>
                    <%-- <th align="center" valign="middle" width="10%" data-name="" data-functions="check"
                                        data-access="CanView.Access,CanUpdate.Access" data-alignment="center" class="chkALL"
                                        data-pass="SMRID">
</th>--%>
                    <%--                                            <th align="center" valign="middle" width="15%" data-name="SMRCode" data-alignment="center"
                                                data-columnname="SMR Code">Traditional
                                            </th>
                                            <th align="center" valign="middle" width="15%" data-name="SMRDescription" data-alignment="center"
                                                data-columnname="SMR Description">Cash Management
                                            </th>
                                            <th align="center" valign="middle" width="15%" data-name="Status" data-alignment="center"
                                                data-columnname="Status">E-Banking
                                            </th>
                                            <th align="center" valign="middle" width="15%" data-name="Application" data-alignment="center"
                                                data-columnname="Application">Cross Selling
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody id="tBodySMR" class="noScrollContent border align-middle">
                                        <tr>
                                            <td style="text-align: center; width: 15%;">
                                                <input type="checkbox" id="Checkbox21" class="requiredCheckbox" />
                                                <label class="col-6 col-form-label label-font-standard" for="txtLastName">
                                                    CTD-PESO
                                                </label>
                                            </td>
                                            <td style="text-align: center; width: 15%;">
                                                <input type="checkbox" id="Checkbox22" class="requiredCheckbox" />
                                                <label class="col-7 col-form-label label-font-standard" for="txtLastName">
                                                    AUTOCOLECT
                                                </label>
                                            </td>
                                            <td style="text-align: center; width: 15%;">
                                                <input type="checkbox" id="Checkbox23" class="requiredCheckbox" />
                                                <label class="col-7 col-form-label label-font-standard" for="txtLastName">
                                                    CONNECT
                                                </label>
                                            </td>
                                            <td style="text-align: center; width: 15%;">
                                                <input type="checkbox" id="Checkbox24" class="requiredCheckbox" />
                                                <label class="col-7 col-form-label label-font-standard" for="txtLastName">
                                                    CREDITFACI
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; width: 15%;">
                                                <input type="checkbox" id="Checkbox25" class="requiredCheckbox" />
                                                <label class="col-6 col-form-label label-font-standard" for="txtLastName">
                                                    CTDFCDU($)
                                                </label>
                                            </td>
                                            <td style="text-align: center; width: 15%;">
                                                <input type="checkbox" id="Checkbox26" class="requiredCheckbox" />
                                                <label class="col-7 col-form-label label-font-standard" for="txtLastName">
                                                    OTCCF
                                                </label>
                                            </td>
                                            <td style="text-align: center; width: 15%;">
                                                <input type="checkbox" id="Checkbox27" class="requiredCheckbox" />
                                                <label class="col-7 col-form-label label-font-standard" for="txtLastName">
                                                    CM.BIZ
                                                </label>
                                            </td>
                                            <td style="text-align: center; width: 15%;">
                                                <input type="checkbox" id="Checkbox28" class="requiredCheckbox" />
                                                <label class="col-7 col-form-label label-font-standard" for="txtLastName">
                                                    TRUST
                                                </label>
                                            </td>
                                        </tr>--%>
                    <%-- <tr>
                                            <td style="text-align: center; width: 15%;">
                                                <input type="checkbox" id="Checkbox29" class="requiredCheckbox" />
                                                <label class="col-6 col-form-label label-font-standard" for="txtLastName">
                                                    SA-FCDU($)
                                                </label>
                                            </td>
                                            <td style="text-align: center; width: 15%;">
                                                <input type="checkbox" id="Checkbox30" class="requiredCheckbox" />
                                                <label class="col-7 col-form-label label-font-standard" for="txtLastName">
                                                    OWDA PLUS
                                                </label>
                                            </td>
                                            <td style="text-align: center; width: 15%;">
                                                <input type="checkbox" id="Checkbox31" class="requiredCheckbox" />
                                                <label class="col-7 col-form-label label-font-standard" for="txtLastName">
                                                    BIR-eFPS
                                                </label>
                                            </td>
                                            <td style="text-align: center; width: 15%;">
                                                <input type="checkbox" id="Checkbox32" class="requiredCheckbox" />
                                                <label class="col-7 col-form-label label-font-standard" for="txtLastName">
                                                    TREASURY
                                                </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; width: 15%;">
                                                <input type="checkbox" id="Checkbox33" class="requiredCheckbox" />
                                                <label class="col-6 col-form-label label-font-standard" for="txtLastName">
                                                    CM PAYROLL
                                                </label>
                                            </td>
                                            <td style="text-align: center; width: 15%;">
                                                <input type="checkbox" id="Checkbox34" class="requiredCheckbox" />
                                                <label class="col-7 col-form-label label-font-standard" for="txtLastName">
                                                    PDC WHOUSE
                                                </label>
                                            </td>
                                            <td style="text-align: center; width: 15%;">
                                                <input type="checkbox" id="Checkbox35" class="requiredCheckbox" />
                                                <label class="col-7 col-form-label label-font-standard" for="txtLastName">
                                                    1-TIME PAY
                                                </label>
                                            </td>
                                            <td style="text-align: center; width: 15%;">
                                                <input type="checkbox" id="Checkbox36" class="requiredCheckbox" />
                                                <label class="col-7 col-form-label label-font-standard" for="txtLastName">
                                                    OTHERS
                                                </label>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>--%>
                    <%--accordion body--%>


                    <div class="row mb-3">
                        <div class="form-check form-check-inline col-sm-2" id="divLost">
                            <input class="form-check-input" type="checkbox" id="chkLostCustomer" value="0" disabled="disabled"/>
                            <label class="form-check-label label-font-title" for="chkLostCustomer">
                                Lost Customer</label>
                        </div>
                    </div>

                    <div class="row mb-3" id="divLostFields">
                        <label class="col-sm-1 col-form-label label-font-standard" for="txtDateLost">
                            Date Lost
                        </label>
                        <div class="col-sm-2">
                            <input type="text" id="txtDateLost" class="lbpControl form-control form-control-sm noForwardDate1Week" disabled="disabled"
                                placeholder="Date" autocomplete="off" readonly="readonly" />
                        </div>

                        <%--<div class="col-sm-3"></div> --%>

                        <label class="col-sm-1 col-form-label label-font-standard" for="ddlReason">
                            Reason
                        </label>
                        <div class="col-sm-2">
                            <select id="ddlReason" class="lbpControl form-select form-select-sm hardcodedSelect" disabled="disabled">
                            </select>
                        </div>

                        <div class="col-sm-5">
                            <input type="text" id="txtReason" class="lbpControl form-control form-control-sm"
                                autocomplete="off" disabled="disabled" maxlength='<%#getFieldLength(FieldVariables.LOST_REASON, LengthSetting.MAX)%>' />
                        </div>
                    </div>

                    <div class="row mb-3">
                        <label class="col-sm-2 col-form-label label-font-standard" for="txtChat">
                            Chat History
                        </label>
                        <div class="col-sm-9">
                            <%--<textarea id="txtChat" rows="5" cols="130" style="text-align: left;" class="lbpControl form-control form-control-sm required requiredField" disabled="disabled">
                            </textarea>--%>
                            <span id="txtChat" style="display: inline-block; background-color: #EEEEEE; border: 1px solid #ced4da;
                                font-size: Small; height: 100px; width: 100%; overflow-x: hidden; overflow: auto;">
                            </span>
                        </div>
                    </div>


                    <div class="row mb-3">
                        <label class="col-sm-2 col-form-label label-font-standard" for="txtFeedback">
                            Create Feedback
                        </label>
                        <div class="col-sm-9">
                            <textarea id="txtFeedback" rows="5" cols="130" style="text-align: left;" class="lbpControl form-control form-control-sm required requiredField"
                                maxlength='<%#getFieldLength(FieldVariables.REMARKS, LengthSetting.MAX)%>'>
                            </textarea>
                        </div>
                    </div>


                    <div class="form-groupt" align="right">
                        <a id="btnAdd" class="lbpControl saveButton">Add Target Market</a>
                        <a id="btnCancel" class="lbpControl cancelButton">Cancel</a>
                    </div>


                </div>
            </div>



            <!-- Modal -->
            <div id="accountInfoModal" class="modal fade" data-bs-backdrop="static" data-bs-keyboard="false"
                tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header btn-lbp-gray text-light">
                            <h5 class="modal-title">Account Information</h5>
                            <button type="button" class="btn-close" aria-label="Close" id="btnModalClose">
                            </button>
                        </div>
                        <div class="modal-body px-5 py-4">
                            <div id="divAccInfoContainer">

                                <div class="row mb-2">

                                    <div class="form-check form-check-inline col-sm-5">
                                        <input class="form-check-input required requiredField" type="checkbox" id="chkModalCASA"
                                            value="divModalCASA" name="chkModal"/>
                                        <label class="form-check-label label-font-title" for="chkModalCASA">
                                            CASA Types Availed</label>
                                    </div>
                                </div>

                                <div class="form-group" id="divModalCASA">

                                    <div class="row mb-3">

                                        <div id="modalCASAProducts" class="row col-sm-12">
                                        </div>
                                    </div>
                                    <div class="row mb-3">
                                        <label class="col-sm-3 col-form-label label-font-standard" for="txtModalDeposit">
                                            Initial Amount (CASA Deposits)
                                        </label>
                                        <div class="col-sm-5">
                                            <input type="text" id="txtModalDeposit" class="lbpControl form-control form-control-sm currency"
                                                autocomplete="off" value="0.00" maxlength='<%#getFieldLength(FieldVariables.AMOUNT_16, LengthSetting.MAX)%>'/>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-sm-3 col-form-label label-font-standard" for="txtModalAccount">
                                            Account Numbers
                                        </label>
                                        <div class="col-sm-5">
                                            <input type="text" id="txtModalAccount" class="lbpControl form-control form-control-sm wholenum"
                                                autocomplete="off" maxlength='<%#getFieldLength(FieldVariables.ACCOUNTNO, LengthSetting.MAX)%>' />
                                        </div>

                                        <div class="col-sm-3">
                                            <a id="btnAddAccNo" class="btn btn-sm lbpControl saveButton">Add Account</a>
                                        </div>

                                        <%--               <div class="text-center mb-3">

                                        <div class="btn-sm" style="margin-right: 6%">
                                            <a id="btnAddAccNo" class="btn btn-sm lbpControl viewButton">Add Account</a>
                                            <a id="btnRemoveAccNo" class="btn btn-sm lbpControl viewButton">Remove Account</a>
                                        </div>
                                    </div>--%>
                                    </div>

                                    <div class="form-group" id="divModalCASAAccounts">
                                    </div>

                                </div>


                                <div class="row">
                                    <div class="form-check form-check-inline col-sm-5">
                                        <input class="form-check-input required requiredField" type="checkbox" id="chkModalLoan"
                                            value="divModalLoanSelect" name="chkModal"/>
                                        <label class="form-check-label label-font-title" for="chkModalLoan">
                                            Loan Products Availed</label>
                                    </div>
                                </div>

                                <div class="form-group" id="divModalLoan">
                                    <div class="row mb-3">
                                        <div class="col-sm-3" id="divModalLoanSelect">
                                            <select id="ddlModalLoanProducts" class="lbpControl form-select form-select-sm hardcodedSelect">
                                            </select>
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <label class="col-sm-3 col-form-label label-font-standard" for="txtModalDateReported">
                                            Date Reported
                                        </label>
                                        <div class="col-sm-5">
                                            <input type="text" id="txtModalDateReported" class="form-control form-control-sm noForwardBackwardDate " readonly="readonly"
                                                placeholder="Date" autocomplete="off" readonly="readonly" />
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <label class="col-sm-3 col-form-label label-font-standard" for="txtModalOrigAmt" disabled>
                                            Loan Amount Availed
                                        </label>
                                        <div class="col-sm-5">
                                            <input type="text" id="txtModalOrigAmt" class="lbpControl form-control form-control-sm currency"
                                                autocomplete="off" value="0.00"/>
                                        </div>
                                    </div>

                                    <div class="row mb-3">
                                        <label class="col-sm-3 col-form-label label-font-standard" for="txtModalLoanAmt">
                                            Desired Loan Amount
                                        </label>
                                        <div class="col-sm-5">
                                            <input type="text" id="txtModalLoanAmt" class="lbpControl form-control form-control-sm currency"
                                                autocomplete="off" value="0.00" maxlength='<%#getFieldLength(FieldVariables.AMOUNT_14, LengthSetting.MAX)%>'/>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <label class="col-sm-3 col-form-label label-font-standard" for="txtModalPN">
                                            P.N. Number
                                        </label>
                                        <div class="col-sm-5">
                                            <input type="text" id="txtModalPN" class="lbpControl form-control form-control-sm alphanumeric"
                                                autocomplete="off" maxlength='<%#getFieldLength(FieldVariables.PNNUMBER, LengthSetting.MAX)%>'/>
                                        </div>

                                        <div class="col-sm-3">
                                            <a id="btnAddPN" class="btn btn-sm lbpControl saveButton">Add Loan Product</a>
                                        </div>

                                        <%--                                    <div class="text-center mb-3">
                                        <div class="col-sm-3"></div>
                                        <div class="modal-footer">
                                        <a id="btnAddPN" class="btn btn-sm lbpControl viewButton">Add PN</a>
                                        <a id="btnRemovePN" class="btn btn-sm lbpControl viewButton">Remove PN</a>
                                        </div>
                                    </div>--%>
                                    </div>

                                    <div class="form-group" id="divModalLoanPN">
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="form-check form-check-inline col-sm-5">
                                        <input class="form-check-input required requiredField" type="checkbox" id="chkModalProducts"
                                            value="divModalOther" name="chkModal"/>
                                        <label class="form-check-label label-font-title" for="chkModalProducts">
                                            Products & Services Availed</label>
                                    </div>
                                </div>

                                <div class="form-group" id="divModalOther">

                                    <div id="divModalProducts" class="form-group pagerDiv row mb-3">
                                        <table id="tblModalProducts" class="table table-sm scrollTable" align="center">
                                            <thead class="noScrollFixedHeader">
                                                <tr valign="middle" align="center">
                                                    <th align="center" valign="middle" width="15%" data-name="Traditional" data-alignment="center"
                                                        data-columnname="Traditional">Traditional
                                                    </th>
                                                    <th align="center" valign="middle" width="15%" data-name="EBankingCore" data-alignment="center"
                                                        data-columnname="E-Banking Core">E-Banking Core
                                                    </th>
                                                    <th align="center" valign="middle" width="15%" data-name="EBankingOthers" data-alignment="center"
                                                        data-columnname="E-Banking Others">E-Banking (Others)
                                                    </th>
                                                    <th align="center" valign="middle" width="16.6%" data-name="CrossSelling" data-alignment="center"
                                                        data-columnname="Cross Selling">Cross Selling
                                                    </th>
                                                </tr>
                                            </thead>
                                            <tbody id="tBodyModalProducts" class="scrollContent border align-middle" style="height: 100px; position: relative; overflow: auto; overflow-x: hidden;">
                                            </tbody>
                                        </table>
                                    </div>

                                    <div class="row mb-3">
                                        <label class="col-sm-3 col-form-label label-font-standard" for="txtModalDepositOther">
                                            Initial Amount (Products & Services)
                                        </label>
                                        <div class="col-sm-5" style="margin-top: 1.8%;">
                                            <input type="text" id="txtModalDepositOther" class="lbpControl form-control form-control-sm currency"
                                                autocomplete="off" value="0.00" maxlength='<%#getFieldLength(FieldVariables.AMOUNT_16, LengthSetting.MAX)%>' />
                                        </div>
                                    </div>

                                </div>

                            </div>
                            <div id="divModalError" class="lbpControl bg-danger text-center text-light">
                                <label id="lblModalErrorMessage"></label>
                            </div>

                            <div class="modal-footer">
                                <%--<a id="btnModalSubmit" class="lbpControl saveButton" data-bs-toggle="modal" data-bs-target="#accountInfoModal">Submit</a>--%>
                                <a id="btnModalSubmit" class="lbpControl saveButton">Submit</a>
                                <a id="btnModalCancel" class="lbpControl cancelButton">Cancel</a>
                            </div>
                        </div>
                    </div>

                </div>
            </div>



        </div>
    </div>
</asp:Content>
