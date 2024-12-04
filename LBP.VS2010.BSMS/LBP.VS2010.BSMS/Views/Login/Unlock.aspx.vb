Imports System.Configuration.ConfigurationManager
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities

Class Unlock
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Page.DataBind()

            lblSystemName.Text = GetSection("Commons")("SystemName").ToString
            lblDisclaimer.Text = "The " + GetSection("Commons")("SystemName").ToString + " " + GetSection("Commons")("SystemCode").ToString + " is best viewed using Google Chrome with 1280 x 800 resolution."

            txtUsername.Focus()
            lblErrorMsg.Visible = False
        End If
    End Sub

    Protected Sub btnUnlock_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If (CBool(GetSection("Commons")("isAPIValidation"))) Then
            APIUnlock()
        Else
            Dim login As New LoginUser
            Dim common As New Common

            login.Username = txtUsername.Text.ToUpper
            login.Password = txtPassword.Text
            login.IPAddress = common.GetIPAddress(True)
            login.Browser = HttpContext.Current.Request.Browser.Type
            login.SystemName = GetSection("Commons")("SystemName")
            login.PasswordHandler = "Unlock"
            Dim userobj As New UsersDO
            userobj.Username = login.currentUser.Username
            userobj.Password = login.Password
            If userobj.ValidateLogin() Then
                SetLoginSessionValues(login)
                Response.Redirect("/Views/Login/Welcome.aspx")
            Else
                lblErrorMsg.Text = login.errMsg
                lblErrorMsg.Visible = True
            End If

        End If

    End Sub

    Protected Sub RedirectToLogin(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Redirect("/Views/Login/Login.aspx")
    End Sub

    Protected Sub lnkHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        modalHelp.Visible = True
    End Sub

    Protected Sub btnSuccessOK_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        modalHelp.Visible = False
    End Sub

    Protected Sub btnCloseModal_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        modalHelp.Visible = False
    End Sub

    Public Sub APIUnlock()
        Dim login As New LoginUser
        Dim common As New Common

        Dim token As String = GetSection("Commons")("GenSysTkn")

        login.Username = txtUsername.Text.ToUpper
        login.Password = MasterDO.APIEncrypt(txtPassword.Text, token)
        login.IPAddress = common.GetIPAddress(True)
        login.Browser = HttpContext.Current.Request.Browser.Type
        login.SystemName = GetSection("Commons")("SystemName")
        login.PasswordHandler = "Unlock"

        If login.APIValidateLogin() Then
            SetLoginSessionValues(login)
            Response.Redirect("/Views/Login/Welcome.aspx")
        Else
            lblErrorMsg.Text = login.errMsg
            lblErrorMsg.Visible = True
        End If
    End Sub
End Class