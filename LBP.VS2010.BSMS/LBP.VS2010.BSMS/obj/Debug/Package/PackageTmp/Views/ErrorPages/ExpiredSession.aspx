<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ExpiredSession.aspx.vb" Inherits="LBP.VS2010.BSMS.ExpiredSession" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><asp:Literal runat="server" ID="litTitle"></asp:Literal></title>   
    
    <link id="Link1" runat="server" rel="shortcut icon" href="/Content/Styles/images/favicon.ico" type="image/x-icon" />
    <link href="/Content/Styles/Commons/Error.css" rel="stylesheet" type="text/css" />

    <script src="/Content/Scripts/jquery/jquery-3.6.0.min.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Commons/Timeout.js" type="text/javascript"></script>
    
</head>
<body>
    <form id="frmError1" runat="server">
	<div id="mainContent" class="mainContent">
        <div id="errorPage" class="errorPage">
        
		    <div style="text-align: left; margin: 20px">
                    <asp:Literal runat="server" ID="litSystem"></asp:Literal>
                    <br />
				    Session Expired...
				    <br />
				    <br />
		    </div>

             <div style="font-size: 15px; font-weight:300;">
				Your session has already expired.
				<br />
				<br />
				You will be redirected to the login page in <span id="clock">10 seconds. </span>
				<br />
				<br />
				<br />
				If you are not redirected, you can click <a href="/Views/Login/Login.aspx">HERE</a>
			</div>
			                                       
		</div>
		<span class="errorSpan"></span>
	</div>
	</form>
</body>
</html>
