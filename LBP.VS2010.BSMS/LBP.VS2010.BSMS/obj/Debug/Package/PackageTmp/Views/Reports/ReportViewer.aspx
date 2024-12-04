<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReportViewer.aspx.vb"
    Inherits="LBP.VS2010.BSMS.ReportViewer" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<html>
<head runat="server">
    <title></title>
    <link type="text/css" href="/Content/Styles/bootstrap-5.2.0-dist/css/bootstrap.min.css"
        rel="stylesheet" />
</head>
<body>
    <form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    <rsweb:ReportViewer Visible="true" runat="server" ID="rptReport" SizeToReportContent="True"
        AsyncRendering="false" ShowPrintButton="true" ProcessingMode="Local" Width="100%"
        Height="900px">
    </rsweb:ReportViewer>
    </div>
  
    </form>
</body>
</html>
