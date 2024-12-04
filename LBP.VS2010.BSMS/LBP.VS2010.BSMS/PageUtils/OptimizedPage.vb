Imports System.Web.Script.Services
Imports System.Web.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.WebCryptor

Public Class OptimizedPage
    Inherits System.Web.UI.Page

    Dim _webConfig As New WebConfigCryptor
    Shared Property ModuleName As String

#Region "PAGE LOAD"
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If CBool(ConfigurationManager.GetSection("Commons")("enableWebConfigEncryption")) Then
            'Encrypt properties of web config
            _webConfig.EncryptWebConfig()
        End If
        ''-- checking of URL if existing in System Menus
        Dim pageURL As String = String.Empty
        If Request.Url IsNot Nothing Then
            pageURL = Request.Url.AbsolutePath
        End If
        HttpContext.Current.Session("previousURL") = pageURL
        If Not pageURL.Contains("Login") And Not pageURL.Contains("Welcome") Then
            Dim roleObj As RoleDO = CType(HttpContext.Current.Session("currUserAccessMatrix"), RoleDO)
            Dim accessObj As ProfileDO = roleObj.ProfileList.Where(Function(a) (a.URL = pageURL) And (a.CanView = "Y"))(0)

            If IsNothing(accessObj) Then
                Dim pageURLReferrer As String = String.Empty
                If Request.UrlReferrer Is Nothing Then

                    Response.Redirect("/Views/ErrorPages/NoAccessPages.aspx")
                End If
            Else
                Dim pageURLReferrer As String = String.Empty
                If Request.UrlReferrer Is Nothing Then
                    Response.Redirect("/Views/ErrorPages/NoAccessPages.aspx")
                Else
                    If Not accessObj.CanView = "Y" Then
                        Response.Redirect("/Views/ErrorPages/NoAccessPages.aspx")
                    End If
                End If
            End If
        End If

        HttpContext.Current.Session("currentURL") = pageURL
        Page.DataBind()
    End Sub

    Private Sub OptimizedPage_LoadComplete(ByVal sender As Object, ByVal e As EventArgs) Handles Me.LoadComplete
        WriteActivityLog(ActivityLog.ActionTaken.VIEW)
    End Sub
#End Region

#Region "SESSION"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Sub IsSessionExpired()
        ' VERIFY THAT THE SESSION CHECKED IS THE SESSION SET IN LOGIN

        If IsNothing(HttpContext.Current.Session("currentUser")) Then
            HttpContext.Current.Session.RemoveAll()
            Throw New Exception("Session Expired.")
        End If
    End Sub

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function PingServer() As String
        ' THIS METHOD IS JUST TO PING THE SERVER SO THAT JS SESSION WILL NOT TIMEOUT
        Dim pingMessage As String = "Your session has been extended."

        Return pingMessage
    End Function
#End Region

#Region "TABLE SORTER"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function sortTable(ByVal objList As List(Of Object), ByVal sortColumn As String, ByVal sortDirection As String, ByVal className As String) As IEnumerable
        Dim genType As Type
        Dim result As IEnumerable = Nothing

        Try
            If objList.Count > 0 Then

                result = objList

                genType = objList(0).GetType

                Return MasterDO.listRecordsAsEnumerable(MasterDO.sortRecordsToTable(result, genType, sortColumn, className, sortDirection), genType)
            End If
        Catch ex As Exception

        End Try

        Return result
    End Function
#End Region

#Region "FILE UPLOAD"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function getCSVKey() As List(Of String)
        Dim lst As New List(Of String)
        Dim keyString As String = WebCryptor.KeyString.GenerateKeyString(16)
        Dim ivString As String = WebCryptor.KeyString.GenerateKeyString(16)

        HttpContext.Current.Session("csvKey") = keyString
        HttpContext.Current.Session("csvIv") = ivString

        lst.Add(keyString)
        lst.Add(ivString)

        Return lst
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Function uploadEncryptedFile(ByVal encryptedCSV As String) As String
        Dim decrypt = _webConfig.Decrypt(encryptedCSV, True)
        Dim decryptArr As String() = decrypt.Split(vbNewLine)

        HttpContext.Current.Session("encryptedFileContents") = decryptArr
        HttpContext.Current.Session("csvKey") = Nothing
        HttpContext.Current.Session("csvIv") = Nothing

        Return ""
    End Function
#End Region

#Region "LOGIN CREADENTIALS STORAGE & RETRIEVAL"
    Public Sub SetLoginSessionValues(ByVal currentUser As LoginUser)
        ' SET THE USER OBJ TO SESSION
        HttpContext.Current.Session("currentUser") = currentUser
        Session("LogonUser") = currentUser.Username
        Session("Role") = currentUser.RoleID
        Session("GroupCode") = currentUser.GroupCode
        Session("BranchCode") = currentUser.BranchCode 'itdkpl 1/13/2024

        ' SET MENU TO SESSION
        Dim dt As DataTable = SystemMenu.ListSystemMenu
        dt = dt.AsEnumerable().GroupBy(Function(f) f.Field(Of Int32)("MainMenuID")).Select(Function(e) e.First).CopyToDataTable
        dt.Columns.Remove("IconSpan")
        dt.Columns.Remove("SubMenuID")
        dt.Columns.Remove("SubMenuName")
        dt.Columns.Remove("URL")
        HttpContext.Current.Session("AllsystemMenus") = dt
        HttpContext.Current.Session("MenuID") = Nothing
        HttpContext.Current.Session("MenuName") = Nothing
        HttpContext.Current.Session("MenuCount") = Nothing
        HttpContext.Current.Session("MenuCount") = dt.Rows.Count
        For Each row As DataRow In dt.Rows
            HttpContext.Current.Session("MenuID") += DirectCast(row("MainMenuID").ToString, String) + ","
            HttpContext.Current.Session("MenuName") += DirectCast(row("MainMenuName"), String) + ","
        Next

        ' SET ACCESS MATRIX (POLICY)
        HttpContext.Current.Session("currUserAccessMatrix") = RoleDO.GetRole(currentUser.RoleID)

        ' SET MENU TO SESSION
        HttpContext.Current.Session("systemMenus") = SystemMenu.ListSystemMenuByRole(currentUser.RoleID)
        ' HttpContext.CurrentSession("systemSubMenus") =
        GenerateStorageKey()
    End Sub

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function retrieveLoginDetails() As LoginUser
        If Not IsNothing(HttpContext.Current.Session("currentUser")) Then
            Dim loginuser As LoginUser = HttpContext.Current.Session("currentUser")
            Return loginuser
        End If

        Return Nothing
    End Function
#End Region

#Region "PAGE CONTROLS ACCESSIBILITY"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function HasAccess(ByVal colname As String) As Boolean
        Dim roleObj As RoleDO = CType(HttpContext.Current.Session("currUserAccessMatrix"), RoleDO)

        Dim accessObj As ProfileDO = roleObj.ProfileList.Where(Function(a) (a.SubMenuID = HttpContext.Current.Session("SubMenuId")))(0)
        'Dim accessObj As ProfileDO = roleObj.ProfileList.Where(Function(a) (a.URL = HttpContext.Current.Session("currentURL")))(0)  'itdkpl 01292024

        If accessObj Is Nothing Then
            Return False
        End If


        Dim value As String = accessObj.GetType.GetProperty(colname).GetValue(accessObj, Nothing)

        If value.Equals("Y") Or value.Equals("1") Then
            Return True
        Else
            Return False
        End If
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetModuleAccess() As ProfileDO
        Dim roleObj As RoleDO = CType(HttpContext.Current.Session("currUserAccessMatrix"), RoleDO)
        Dim accessObj As ProfileDO = roleObj.ProfileList.Where(Function(a) (a.SubMenuID = HttpContext.Current.Session("SubMenuId")))(0)
        'Dim accessObj As ProfileDO = roleObj.ProfileList.Where(Function(a) (a.URL = HttpContext.Current.Session("currentURL")))(0) 'itdkpl 01292024

        Return accessObj
    End Function
#End Region

#Region "SECURE STORAGE"
    Public Sub GenerateStorageKey()
        HttpContext.Current.Session("storageKey") = WebCryptor.KeyString.GenerateKeyString(8)
    End Sub

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function getStorageKey() As String
        Return HttpContext.Current.Session("storageKey").ToString
    End Function
#End Region

#Region "ACTIVITY LOGGING"
    Public Shared Sub WriteActivityLog(ByVal actionType As ActivityLog.ActionTaken, Optional ByVal str As String = "")
        Dim loginuser As LoginUser = HttpContext.Current.Session("currentUser")

        If Not IsNothing(loginuser) Then
            Dim act As New ActivityLog With {
                .ModuleName = ModuleName,
                .UserName = loginuser.Username,
                .ActivityType = actionType.ToString + str,
                .IPAddress = loginuser.IPAddress,
                .Browser = loginuser.Browser
            }

            act.WriteLog()
        End If
    End Sub
#End Region
End Class