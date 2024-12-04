<%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>

<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Login.Master" CodeBehind="Login.aspx.vb" Inherits="LBP.VS2010.BSMS.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="loginMainContent" runat="server">
         <script language="javascript" type="text/javascript">
             $(document).ready(function () {
                 formatPasswordControl();
             });

             var prm = Sys.WebForms.PageRequestManager.getInstance();

             prm.add_endRequest(function () {
                 // re-bind your jQuery events here            
                 formatPasswordControl();
             });

             function formatPasswordControl() {
                 $(".togglePassword").showHidePassword();
                 $(".show-hide-password").css("margin-top", "35px");
             }

             //Prevent Double Submit Events
             Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
             function BeginRequestHandler(sender, args) { var oControl = args.get_postBackElement(); oControl.disabled = true; }

          
         </script>
    <div id="main">
        <div id="login">
            <asp:Panel ID="pnlLog" runat="server">
                <asp:Label runat="server" ID="lblSystemName" class="login-label-system"></asp:Label>
                <div id="logFields">
                    <div class="mx-auto fieldContainer">
                        <asp:TextBox ID="txtUsername" runat="server" class="form-control-user alphaOnly" aria-label="Username" MaxLength='<%#getFieldLength(FieldVariables.USERNAME, LengthSetting.MAX)%>'></asp:TextBox>
                    </div>
                    <div class="mx-auto fieldContainer">
                        <asp:TextBox ID="txtPassword" runat="server" Class="form-control-pass togglePassword" TextMode="Password" aria-label="Password" MaxLength='<%#getFieldLength(FieldVariables.PASSWORD, LengthSetting.MAX)%>'></asp:TextBox>
                    </div>
                    <%--BUTTON CONTROL--%>
                    <div class="d-flex justify-content-center mt-3" style="padding-left: 113px">
                            <button runat="server" id="btnLogin" onserverclick="btnLogin_Click" class="btnLogin btn btn-block">
                            Login</button>
                        <button runat="server" id="btnReset" onserverclick="btnReset_Click" class="btnLogin btn btn-block" onclick="return btnReset_onclick()">
                            Reset</button>
                    </div>
                  
                </div>
            </asp:Panel>
            <br />
            <br />
            <br />
        </div>
        <div id="divError">
            <asp:Label ID="lblErrorMsg" runat="server" class="w-100 errmsg-pos badge bg-danger"
                Visible="False" Font-Size="14px" Style="font-family: Lato-Regular; color: White;"></asp:Label>
        </div>
        <div id="waiver">
            <span id="lblwaiver">
                You are attempting to access a Bank system. Unauthorized access, or access in excess of your authority, may
                <br />
                result in administrative and criminal penalties. Your activities are being logged and monitored.</span>
        </div>

        <div id="footer" class="footer">
            <asp:Label ID="lblDisclaimer" runat="server" class="label-font-standard"></asp:Label>
        </div>
    </div>
    
    <div runat="server" id="modalHelp" class="modal" tabindex="-1" role="dialog" style="display: block;" visible="false">
        <div class="modal-dialog" role="document">
            <div class="modal-content ">
                <div runat="server" id="devHelp"  visible="false">
                    <div class="modal-header alert alert-lbp">
                        <h5 class="modal-title"><span class="fa fa-check-square"></span>Login Credentials</h5>
                        <button id="Button1" runat="server" onserverclick="btnCloseModal_Click" type="button" class="btn-close"></button>
                    </div>
                    <div class="modal-body">
                        <p class="label-font-standard">Please use the following credentials. </p>
                        <span class="col-lg-8 offset-1 col-form-label label-font-standard ">Username : devpool</span>
                        <br />
                        <span class="col-lg-8 offset-1 col-form-label label-font-standard ">
                            Password : devpool 
                            <sup class="text-danger font-weight-bold">* default password</sup></span>
                    </div>
                </div>
                <div runat="server" id="deployedHelp" visible="true">
                    <div class="modal-header alert alert-lbp">
                        <h5 class="modal-title"><span class="fa fa-question-circle"></span>Help</h5>
                        <button id="Button2" runat="server" onserverclick="btnCloseModal_Click" type="button" class="btn-close"></button>
                    </div>
                    <div class="modal-body">
                        <span class="col-lg-8 offset-1 col-form-label label-font-standard">
                            Please contact RBSD-Task Management System administrator 
                            <span runat="server" id="at" class="label-font-standard" visible="false"> at </span>
                            <a runat="server" id="supportEmail" href="" class="label-font-standard" visible="false"></a>
                            <span class="label-font-standard"> for login and account concerns. <br><br>Thank you!</span>
                        </span>
                    </div>
                </div>
                
                <div class="modal-footer">
                    <button runat="server" id="btnSuccessOK" onserverclick="btnSuccessOK_Click" type="button" class="lbpControl btn btn-lbp-green">
                        <span><i class="fa fa-check-circle"></i></span><strong>OK. Great.</strong></button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
