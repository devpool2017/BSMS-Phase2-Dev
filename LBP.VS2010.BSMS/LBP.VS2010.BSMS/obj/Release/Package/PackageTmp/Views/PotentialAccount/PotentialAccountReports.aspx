<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master"
    CodeBehind="PotentialAccountReports.aspx.vb" Inherits="LBP.VS2010.BSMS.PotentialAccountReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../../Content/Scripts/PageScripts/Reports/PotentialAccountReports/PotentialAccountReports.js"
        type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="card p-3">
        <div class="row mb-sm-3">
            <label class="col-sm-auto col-form-label label-font-standard" for="ddl-s">
                Select Potential Account Report</label>
            <div class="col-sm-5">
                <select id="ddl-s" class="form-select form-select-sm hardcodedSelect required requiredField">
                    <option value="">Please Select</option>
                    <option value="1">Potential Accounts (Not Yet a Lead)</option>
                    <option value="2">Potential Account (Not Yet a Customer/Lost)</option>
                </select>
            </div>
            <div class="col-sm-auto">
                <a id="btnGenerate" class="lbpControl printButton col-sm-12">Generate Report</a>
            </div>
          <%--    <div id="divReport">
            <iframe id="ifrmReportViewer" name="ifrmReportViewer" style=" margin: 0;display: block;"  data-src="ReportViewer.aspx" src="about:blank" frameborder="0">
            </iframe>
        </div>--%>
            <div id="divReport">
            <iframe name="ifrmReportViewer" width="100%" src="about:blank"></iframe>
        </div>
        </div>
        <br />
       
        <div id="divPotentialAccountReportError" class="lbpControl bg-danger text-center text-light">
            <label id="lblPotentialAccountReportErrorMessage">
            </label>
        </div>
    </div>
    </div>
</asp:Content>
