<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master"
    CodeBehind="EncodingDates.aspx.vb" Inherits="LBP.VS2010.BSMS.EncodingDates" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../../Content/Scripts/PageScripts/CPA/EncodingDates.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card p-3" id="divForTA">
        <div class="row mb-sm-3">
            <label class="col-sm-auto col-form-label label-font-standard">
                Date Range for the Input of Potential Account</label>
        </div>
        <div id="divLDetails" class="well">
            <div class="row mb-2">
                <label class="col-2 col-form-label label-font-standard">
                    Start Date Range</label>
                <div class="col-sm-4">
                    <input type="text" id="txtStartDate" class="lbpControl form-control form-control-sm shortdate disabled
                                required requiredField"
                        placeholder="Date" autocomplete="off" readonly="readonly" />
                </div>
            </div>
            <div class="row mb-2">
                <label class="col-2 col-form-label label-font-standard">
                    End Date Range</label>
                <div class="col-sm-4">

                    <input type="text" id="txtEndDate" class="lbpControl form-control form-control-sm shortdate disabled
                                required requiredField"
                        placeholder="Date" autocomplete="off" readonly="readonly" />
                </div>
            </div>
            <div id="divFooter" hidden>

              <%--  <div id="divDatesError" class="lbpControl bg-danger text-center text-light">
                    <span id="lblDatesErrorMessage"></span>
                </div>--%>
                <div id="divEdit" class="col-sm-auto">
                    <a id="btnEditDate" class="lbpControl btn-sm editButton canUpdate">EDIT</a>
                </div>
                <div id="divSave" class="col-sm-auto hidden">
                    <a id="btnSaveDate" class="lbpControl btn-sm saveButton canUpdate">SAVE</a> <a id="btnCancel"
                        class="lbpControl btn-sm cancelButton">CANCEL</a>
                </div>
            </div>
        </div>
    </div>

    <div class="card p-3" id="divForGH" hidden>
        <div class="row mb-sm-3">
            <label class="col-sm-auto col-form-label label-font-standard">
                Encoding Dates For Approval</label>
        </div>
        <div id="divApproval" class="well">
            <table id="tblApproval" class="table table-sm col-sm-12" align="center">
                <thead class="noScrollFixedHeader">
                    <tr valign="middle" align="center">
                  
                        <th align="center" valign="middle" width="35%" data-name="ValueFrom" data-alignment="center"
                            data-columnname="Potential Account Dates"> Potential Account Dates For Approval
                        </th>
                   
                    </tr>
                </thead>
                <tbody id="tBodyApproval" class="noScrollContent border align-middle">
                </tbody>
            </table>


        </div>
             <div id="divFooter2">

            <%--    <div id="divDatesApprovalError" class="lbpControl bg-danger text-center text-light">
                    <span id="lblDateApprovalErrorMessage"></span>
                </div>--%>
                <div class="col-sm-auto">
            
                  <a id="btnApprove" class="lbpControl btn-sm saveButton canApprove">Approve</a> 
                  <a id="btnReject" class="lbpControl rejectButton canApprove">Reject</a>
                </div>
               
            </div>
    </div>
</asp:Content>
