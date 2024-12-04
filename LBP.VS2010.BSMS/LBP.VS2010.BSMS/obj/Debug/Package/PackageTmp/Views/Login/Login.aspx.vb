Imports System.Configuration.ConfigurationManager
Imports System.Web.Script.Services
Imports System.Web.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.WebCryptor

Public Class Login
    Inherits OptimizedPage
    Dim _webConfig As New WebConfigCryptor

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Page.DataBind()
            lblSystemName.Text = GetSection("Commons")("SystemName").ToString
            lblDisclaimer.Text = "The " + GetSection("Commons")("SystemName").ToString + " " + GetSection("Commons")("SystemCode").ToString + " is best viewed using Google Chrome with 1280 x 800 resolution."

            txtUsername.Focus()
            lblErrorMsg.Visible = False

            Session.Remove("LoginUsername")
        End If
    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim newUser As New LoginUser
        lblErrorMsg.Visible = False

        newUser.Username = txtUsername.Text.ToUpper
        newUser.Password = txtPassword.Text

        newUser.IPAddress = GetIPAddress()
        newUser.Browser = HttpContext.Current.Request.Browser.Type

        Dim userobj As New UsersDO
        userobj.Username = newUser.Username
        userobj.Password = newUser.Password
        If userobj.ValidateLogin() Then
            Dim ldap As New LDAPAuthentication
            ldap.UserName = newUser.Username
            ldap.Password = newUser.Password
            Dim ldapValid As Boolean = True
            ldap.Domains = _webConfig.Decrypt("LDAPSource", True).Split("|")


            If CBool(_webConfig.Decrypt("ADCheck", True)) Then
                ldapValid = ldap.IsValidADUser
            Else

                ldapValid = True
            End If

            Dim currIPAddress As String = GetIPAddress()
            If ldapValid Then
                'Validate if User is a system user
                userobj.Username = newUser.Username
                userobj.Name = ldap.DisplayName
                userobj.FirstName = ldap.FirstName
                currIPAddress = newUser.IPAddress

                If newUser.validateLoginUser(userobj, currIPAddress) Then
                    'Process User Login
                    If userobj.ProcessLogin(currIPAddress, userobj.Username) Then
                        HttpContext.Current.Session("LoggedUserDO") = userobj
                        HttpContext.Current.Session("Username") = ldap.UserName
                        ldap.BranchCode = userobj.Branch
                        'HttpContext.Current.Session("BranchCode") = userobj.Branch

                        Dim logUser As New LoginUser
                        logUser.Username = ldap.UserName
                        'logUser.DisplayName = ldap.DisplayName
                        logUser.DisplayName = userobj.DisplayName
                        logUser.RoleID = userobj.RoleId
                        logUser.RoleName = userobj.RoleName
                        logUser.Branch = userobj.Branch
                        logUser.GroupCode = userobj.GroupCode 'itdkpl
                        logUser.RegionName = userobj.RegionName
                        logUser.GroupCode = userobj.RegionCode
                        logUser.BranchCode = userobj.BranchCode
                        HttpContext.Current.Session("currentUser") = ldap
                        SetLoginSessionValues(logUser)
                        Response.Redirect("/Views/Login/Welcome.aspx")

                    Else
                        lblErrorMsg.Text = userobj.objErrorMessage
                        lblErrorMsg.Visible = True
                    End If
                Else
                    lblErrorMsg.Text = userobj.objErrorMessage
                    lblErrorMsg.Visible = True
                End If
            Else
                userobj.objErrorMessage = ldap.ErrMsg       'Set the error message from LDAP.

                    'Validate if User is a system user
                    If userobj.IsSystemUser(userobj.Username) Then
                        'If System User, increment loginAttempt
                    userobj.IncrementLoginAttempt(_webConfig.Decrypt("MaxLoginAttempt", True))
                    Else
                        userobj.objErrorMessage = ldap.ErrMsg   'Set again to overwrite the message if not system user.
                    End If

                    userobj.isSuccess = False
                    lblErrorMsg.Text = userobj.objErrorMessage
                    lblErrorMsg.Visible = True
                End If
            Else
                lblErrorMsg.Text = userobj.objErrorMessage
            lblErrorMsg.Visible = True

        End If
    End Sub

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function UnlockUser(ByVal userObj As UsersDO) As UsersDO
        Dim _webConfig As New WebConfigCryptor

        If userObj.ValidateLogin() Then
            Dim ldap As New LDAPAuthentication
            ldap.UserName = userObj.Username
            ldap.Password = userObj.Password
            Dim ldapValid As Boolean = True
            ldap.Domains = _webConfig.Decrypt("LDAPSource", True).Split("|")

            'Validate User in Active Directory
            If CBool(_webConfig.Decrypt("ADCheck", True)) Then
                ldapValid = ldap.IsValidADUser
            Else
                ldapValid = ldap.SearchUser
            End If

            If ldapValid Then
                'Validate if User is a system user
                userObj.Name = ldap.DisplayName
                userObj.FirstName = ldap.FirstName
                If userObj.IsSystemUser(userObj.Username) Then
                    userObj.LogoutUser("User Reset")
                    HttpContext.Current.Session.Clear()
                    HttpContext.Current.Session.RemoveAll()
                End If
            Else
                userObj.isSuccess = False
                userObj.objErrorMessage = ldap.ErrMsg
            End If
        End If

        Return userObj
    End Function


#Region "RESET"
    Protected Sub btnReset_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        txtUsername.Text = String.Empty
        txtPassword.Text = String.Empty

        txtUsername.Focus()

        lblErrorMsg.Visible = False
    End Sub
#End Region


#Region "HELP MODAL"
    Protected Sub lnkHelp_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        modalHelp.Visible = True
        If CBool(GetSection("Commons")("isInDevelopmentPhase")) Then
            devHelp.Visible = True
            deployedHelp.Visible = False
        Else
            devHelp.Visible = False
            deployedHelp.Visible = True
            If Not GetSection("Commons")("AdminSupportEmail").ToString = "" Then
                at.Visible = True
                supportEmail.Visible = True
                supportEmail.HRef = "mailto:" + GetSection("Commons")("AdminSupportEmail").ToString
                supportEmail.InnerHtml = GetSection("Commons")("AdminSupportEmail").ToString
            End If
        End If
    End Sub

    Protected Sub btnSuccessOK_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        modalHelp.Visible = False
    End Sub

    Protected Sub btnCloseModal_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        modalHelp.Visible = False
    End Sub
#End Region


#Region "MISC"
    Private Shared Function GetIPAddress() As String
        Dim sIPAddress As String = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        Dim ipAddress As String = String.Empty
        If String.IsNullOrEmpty(sIPAddress) Then
            ipAddress = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
        End If
        Return If(Not IsNothing(sIPAddress), sIPAddress, ipAddress)
    End Function
#End Region
End Class