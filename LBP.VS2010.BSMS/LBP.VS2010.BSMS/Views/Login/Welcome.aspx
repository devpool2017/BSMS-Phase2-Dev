<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Site.Master" CodeBehind="Welcome.aspx.vb" Inherits="LBP.VS2010.BSMS.Welcome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="/Content/Styles/Commons/Welcome.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript">
         $(document).ready(function () {
             loadingScreen(true, 0);
         });   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron welcomeJumbotron mb-1">
        <h1 class="display-4 contenttext">Hello,
            <asp:Label ID="lblDisplayName" runat="server" Text=""></asp:Label>!
        </h1>

        <p runat="server" id="pWelcomeMsg" class="lead contenttext2">
        </p>
    </div>

   
</asp:Content>
