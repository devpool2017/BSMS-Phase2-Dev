<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Oops.aspx.vb" Inherits="LBP.VS2010.BSMS.Oops" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title><asp:Literal runat="server" ID="errPage"></asp:Literal></title>   
    
    <link id="Link1" runat="server" rel="shortcut icon" href="/Content/Styles/images/favicon.ico" type="image/x-icon" />
    <link href="/Content/Styles/Commons/Error.css" rel="stylesheet" type="text/css" />

    <script src="/Content/Scripts/jquery/jquery-3.6.0.min.js" type="text/javascript"></script>
    <script src="/Content/Scripts/Commons/Error.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
<div id="mainContent" class="mainContent">
        <div id="errorPage" class="errorPage">
        
		    <div style="text-align: left; margin: 20px">
                    <asp:Literal runat="server" ID="litSystemErr"></asp:Literal>
                    <br />
				    Sorry an error has occured...
				    <br />
				    <br />
		    </div>

             <div style="font-size: 15px; font-weight:300;">
				An unexpected error occured on our site. The error has been logged.
				<br />
				<br />
				You will be redirected to the home page in <span id="clock">10 seconds. </span>
				<br />
				<br />
				If you are not redirected, you can click <a href="/Views/Login/Welcome.aspx">HERE</a>
                <br />
				<br />
                Thank you.
			</div>
			                                       
		</div>
		<span class="errorSpan"></span>
	</div>
    </form>
</body>
</html>
