<%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master" 
    CodeBehind="Revisits.aspx.vb" Inherits="LBP.VS2010.BSMS.Revisits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="/Content/Scripts/PageScripts/CPA/Revisits.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<input type="hidden" id="hdnSession" data-value="<%=Session("LogonUser")%>" />
<input type="hidden" id="hdnSession1" data-value="Y" />
<div id="frmHorizontal" class="well">

    <div class="row mb-2">
        <label for="ddlAcctName" class="col-sm-4 col-form-label label-font-standard">
            Potential Account Name:
        </label>
        <div class="col-sm-4">
            <select id="ddlAcctName" class="form-select form-select-sm">
            </select>
              <input type="text" id="txtAccountName" class="lbpControl form-control form-control-sm" hidden />
        </div>
        <div class="col-sm-auto">
            <a id="btnSearch" class="lbpControl btn-sm searchButton">Search</a>
            <!--<a id="btnReset" class="lbpControl btn-sm clearButton">Reset</a> -->
        </div>
    </div>

<div id="divDetails">
    <div class="row mb-1">
        <div class="card-header btn-lbp-green text-center rounded-0">
            <b><h6 id="txtAcctName" class="mb-1"></h6></b>
        </div>
        <div class="row mb-2">
        </div>
        <table style="width:500px;">
            <tr> 
                <td style="width:100px;">        
                    <label for="ddlRevisitHistory" class="col-form-label label-font-standard">
                        Revisit History
                    </label>
                </td> 
                <td style="width:10px;"></td> 
                <td style="width:75%;"> 
                    <select id="ddlRevisitHistory" class="form-select form-select-sm" size="4">
                    </select>
                </td>
            </tr>
                <tr>
                    <td style="width:150px;">
                        <label class="col-form-label label-font-standard">
                            Last Revisit
                        </label>
                    </td>
                    <td></td>
                    <td>
                        <p id="txtLastRevisit" />
                    </td>
                </tr>
            <tr>
                <td style="width:150px;">
                    <label class="col-form-label label-font-standard">
                    Number of Visits
                    </label>
                </td>
                <td>
                    <input id="chkAddVisit" type="checkbox"/>
                </td>
                <td>
                    <input id="txtNumberVisit" style="width:30px;" maxlength="2" />
                </td>
            </tr>
            </table>
            <table>
            <tr >
                <td style="width:125px;">
                    <label class="col-form-label label-font-standard">
                        Chat History
                    </label>
                </td>
                <td>
                <!--    <label id="lblChatHistory" class="form-select form-select-sm" disabled>
                    </label> -->
                    <span id="lblChatHistory" style="display: inline-block; background-color: #FFFFFF;
                    border-style: Inset; height: 100px; width: 100%; overflow: auto; overflow-x: hidden;
                    font-size: 13px"></span>

                </td>
            </tr>
            <tr>
        
                <td>
                    <label for="txtCreateFeedback" class="col-form-label label-font-standard" >
                        Create Feedback
                    </label>
                </td>
                <td>
                <textarea id="txtCreateFeedback" rows="5" cols="130" style="text-align: left;" class="lbpControl form-control form-control-sm required requiredField"
                maxlength='<%#getFieldLength(FieldVariables.REMARKS, LengthSetting.MAX)%>'>
                </textarea>
                </td>

            </tr>
        </table>
</div>


<div class="col mb-2">
    <div class="col-sm-auto" align="right">
        <a id="btnUpdateCPA" class="lbpControl btn-sm saveButton">Update Target Market</a>
        <a id="btnUpdateCPA2" class="lbpControl btn-sm saveButton">Update Target Market</a>
        <a id="btnCancel" class="lbpControl btn-sm cancelButton">Cancel</a>
    </div>
</div>

</div>
</div>
</asp:Content>
