<%@ Import Namespace="LBP.VS2010.BSMS.Utilities.PageControlVariables" %>
<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Master/Login.Master" CodeBehind="Unlock.aspx.vb" Inherits="LBP.VS2010.BSMS.Unlock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
            $(".show-hide-password").css("margin-top", "40px");
        }

        //Prevent Double Submit Events
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequestHandler);
        function BeginRequestHandler(sender, args) { var oControl = args.get_postBackElement(); oControl.disabled = true; }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="loginMainContent" runat="server">
       <div id="main">
        <div id="login">
            <asp:Label runat="server" ID="lblSystemName" class="login-label-system"></asp:Label>
            <asp:Panel ID="pnlLog" runat="server">
                <div id="logFields" style="padding-top:320px">
                    <div class="offset-2 col-lg-8">
                        <asp:TextBox ID="txtUsername" runat="server" class="form-control-user alphaOnly" MaxLength='<%#getFieldLength(FieldVariables.USERNAME, LengthSetting.MAX)%>'></asp:TextBox>
                    </div>
                    <div class="offset-2 col-lg-8">
                        <asp:TextBox ID="txtPassword" runat="server" Class="form-control-pass togglePassword" TextMode="Password"></asp:TextBox>
                    </div>
                    <%--BUTTON CONTROL--%>
                    <div class="d-flex justify-content-center mt-3">
                        <button runat="server" id="btnUnlock" onserverclick="btnUnlock_Click" class="btnLogin btn btn-block">Unlock User</button>
                    </div>
                    <div class="d-flex justify-content-center mt-3">                     
                        <asp:LinkButton runat="server" class="btn btn-link login-link" Style="padding: 0px;" OnClick="RedirectToLogin">Return to Login Page</asp:LinkButton> &nbsp;
                        <asp:LinkButton runat="server" ID="lnkHelp" class="btn btn-link login-link" Style="padding: 0px;" OnClick="lnkHelp_Click"><span class="fa fa-question-circle"></span>Help</asp:LinkButton>
                    </div>
                </div>
            </asp:Panel>
            <br />
            <br />
            <br />
        </div>
        <div style="width: 95%">
            <asp:Label ID="lblErrorMsg" runat="server" class="errmsg-pos badge bg-danger  col-12"
                Visible="False" Font-Size="14px" Style="font-family: Lato-Regular; color: White;"></asp:Label>
        </div>        
        <div id="waiver">  
            <label id="lblwaiver">You are attempting to access a Bank system. Unauthorized access, or access in excess of your authority, may <br />
            result in administrative and criminal penalties. Your activities are being logged and monitored.</label>
         </div>

        <div id="footer" class="form-group form-row footer">
            <asp:Label ID="lblDisclaimer" runat="server" class="label-font-standard col-9" style="text-align:center; width: 1000px"></asp:Label>
        </div>
    </div>
    
    <div runat="server" id="modalSuccess" class="modal " tabindex="-1" role="dialog" style="display: block;" visible="false">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header alert alert-lbp">
                    <h5 class="modal-title"><span class="fa fa-check-square"></span>    Success</h5>
                    <button runat="server" onserverclick="btnCloseModal_Click" type="button" class="btn-close"></button>
                </div>
              <div class="modal-body">
                <p class="label-font-standard">Unlock successfully.</p>
                <br />
              </div>
              <div class="modal-footer">                
                    <button runat="server" onserverclick="RedirectToLogin" type="button" class="btn btn-success">OK. Great.</button>             
              </div>
            </div>
        </div>
    </div>    

    <div runat="server" id="modalHelp" class="modal" tabindex="-1" role="dialog" style="display: block;" visible="false">
        <div class="modal-dialog" role="document">
            <div class="modal-content ">
                <div class="modal-header alert alert-lbp">
                    <h5 class="modal-title"><span class="fa fa-check-square"></span>Login Credentials</h5>
                    <button runat="server" onserverclick="btnCloseModal_Click" type="button" class="btn-close"></button>
                </div>
                <div class="modal-body">
                    <p class="label-font-standard">Please use the following credentials. </p>
                    <label class="col-lg-8 offset-1 col-form-label label-font-standard ">Username : devpool</label> <br />
                    <label class="col-lg-8 offset-1 col-form-label label-font-standard ">Password : devpool 
                        <sup class="text-danger font-weight-bold">* default password</sup></label>
                </div>
                <div class="modal-footer">
                    <button runat="server" id="btnSuccessOK" onserverclick="btnSuccessOK_Click" type="button" class="lbpControl btn btn-lbp-green">
                        <span><i class="fa fa-check-circle"></i></span><strong>OK. Great.</strong></button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
