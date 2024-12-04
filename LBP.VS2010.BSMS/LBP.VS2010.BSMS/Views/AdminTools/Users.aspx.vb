Imports System.Web.Services
Imports System.Web.Script.Services
Imports LBP.VS2010.BSMS.DataObjects

Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages.Validation
Imports System.Configuration.ConfigurationManager
Imports LBP.VS2010.WebCryptor

Public Class Users
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Users"

        If Not Page.IsPostBack Then
            Page.DataBind()
        End If
    End Sub

#Region "LOAD CONTENT"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetRoleList() As List(Of ReferencesForDropdown)
        Return ReferencesForDropdown.convertToReferencesList(UsersDO.GetRoleList, "RoleID", "RoleName", True)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function OnLoadData() As UsersOnLoad
        ''.RegionLst = ReferencesForDropdown.convertToReferencesList(UsersDO.GetRegionList, "RegionCode", "RegionName", True),
        Dim dt As New DataTable
        Dim GroupHead As String = String.Empty
        Dim BranchHead As String = String.Empty
        Dim TechnicalAssistant As String = String.Empty

        Return New UsersOnLoad With {
            .RoleLst = ReferencesForDropdown.convertToReferencesList(UsersDO.GetRoleList, "RoleID", "RoleName", True),
            .RegionLst = ReferencesForDropdown.convertToReferencesList(UsersDO.GetRegionList, "RegionCode", "RegionName", True),
            .BranchLst = ReferencesForDropdown.convertToReferencesList(UsersDO.GetBranchList, "BranchCode", "BranchDesc", True),
            .CanInsert = HasAccess("CanInsert"),
            .CanUpdate = HasAccess("CanUpdate"),
            .CanApprove = HasAccess("CanApprove"),
            .CanUnlock = False,
            .GH = GetSection("Commons")("GroupHead").ToString,
            .BH = GetSection("Commons")("BM").ToString,
            .TA = GetSection("Commons")("TA").ToString
            }

    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Shared Function GetBranchPerRegion(ByVal regionCode As String) As List(Of ReferencesForDropdown)
        Dim BranchLst As List(Of ReferencesForDropdown)
        BranchLst = ReferencesForDropdown.convertToReferencesList(UsersDO.GetBranchListPerRegion(regionCode), "BranchCode", "BranchDesc", True)
        Return BranchLst
    End Function



#End Region

#Region "GET/LIST"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetList(ByVal searchText As String, ByVal filterStatus As String, ByVal filterRole As String, ByVal filterRegion As String, ByVal filterBranch As String,
                                   ByVal filterStatusDesc As String, ByVal filterRoleName As String, ByVal filterRegionName As String, ByVal filterBranchName As String) As List(Of UsersDO)
        WriteActivityLog(ActivityLog.ActionTaken.SEARCH)

        Dim params As New UsersDO
        params.Username = IIf(String.IsNullOrWhiteSpace(searchText), Nothing, searchText)
        params.StatusId = IIf(String.IsNullOrWhiteSpace(filterStatus), Nothing, filterStatus)
        params.RoleId = IIf(String.IsNullOrWhiteSpace(filterRole), Nothing, filterRole)
        params.RegionCode = IIf(String.IsNullOrWhiteSpace(filterRegion), Nothing, filterRegion)
        params.BranchCode = IIf(String.IsNullOrWhiteSpace(filterBranch), Nothing, filterBranch)
        params.Status = filterStatusDesc
        params.RoleName = filterRoleName
        params.RegionName = filterRegionName
        params.Branch = filterBranchName

        HttpContext.Current.Session("currentUsersParameter") = params

        Return UsersDO.GetList(searchText, filterStatus, filterRole, filterRegion, filterBranch, GetModuleAccess())
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetTempList(ByVal searchText As String, ByVal filterStatus As String) As List(Of UsersDO)
        WriteActivityLog(ActivityLog.ActionTaken.SEARCH)

        Return UsersDO.GetTempList(searchText, filterStatus)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetDetails(ByVal username As String, ByVal action As String) As UsersDO

        WriteActivityLog(ActivityLog.ActionTaken.VIEW)
        Dim obj As New UsersDO
        obj = UsersDO.GetDetails(username, action)
        HttpContext.Current.Session("SelectedRole") = obj.RoleId
        Return obj
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetMergeDetails(ByVal username As String) As List(Of UsersDO.UserResult)
        WriteActivityLog(ActivityLog.ActionTaken.VIEW)

        Return UsersDO.GetMergeDetails(username)
    End Function
#End Region

#Region "SAVE/UPDATE"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateUser(ByVal obj As UsersDO) As String
        Dim message As String = String.Empty
        Dim ldap As New LDAPAuthentication
        Dim _webConfig As New WebConfigCryptor

        If Not (obj.isValid()) Then
            message = obj.errMsg
        Else
            If Not (obj.IsUserIDExist) Then
                message = obj.errMsg
            Else
                obj.Password = String.Empty
                If CBool(_webConfig.Decrypt("ADCheck", True)) Then
                    If Not (ldap.validateLoginADSecurity(obj.Username, obj.Password)) Then
                        message = ldap.ErrMsg
                    End If
                End If

            End If
        End If
        Return message
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SaveUser(ByVal obj As UsersDO) As String
        Dim message As String = String.Empty

        obj.LogonUser = HttpContext.Current.Session("LogonUser")

        If Not obj.SaveUser() Then
            message = obj.errMsg
        Else
            Dim actionType As ActivityLog.ActionTaken
            Select Case obj.Action
                Case "add"
                    actionType = ActivityLog.ActionTaken.CREATE
                Case "delete"
                    actionType = ActivityLog.ActionTaken.DELETE
                Case Else
                    actionType = ActivityLog.ActionTaken.UPDATE
            End Select

            WriteActivityLog(actionType)
        End If

        Return message
    End Function


    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function UnlockUser(ByVal username As String) As String
        Dim message As String = String.Empty
        Dim obj As New UsersDO
        obj.LogonUser = HttpContext.Current.Session("LogonUser")
        obj.Username = username
        If Not obj.UnlockUser() Then
            message = obj.errMsg
        Else
            Dim actionType As ActivityLog.ActionTaken
            Dim str As String
            actionType = ActivityLog.ActionTaken.UNLOCK
            str = " - " + obj.Username
            WriteActivityLog(actionType, str)
        End If

        Return message
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ApproveUser(ByVal obj As UsersDO) As String
        Dim message As String = String.Empty

        obj.LogonUser = HttpContext.Current.Session("LogonUser")
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim status As String = ""
        'get role of user for approval
        dt1 = UsersDO.GetUserRole(obj.Username)
        If Not IsNothing(dt1) Then
            For Each row In dt1.Rows
                obj.RoleId = row.Item("RoleID").ToString
            Next
        End If
        'get role status of user for approval
        dt = UsersDO.GetRoleStatus(obj.RoleId)
        If Not IsNothing(dt) Then
            For Each row In dt.Rows
                status = row.Item("Status").ToString
            Next
        End If

        If Not (status = GetSection("Commons")("ForDeactivation")) Then
            If Not obj.ApproveUser() Then
                message = obj.errMsg
            Else
                WriteActivityLog(ActivityLog.ActionTaken.APPROVE)
            End If
        Else
            message = Messages.ErrorMessages.Validation.getErrorMessage(ErrorType.NOT_ACTIVE_USERROLE, "", Nothing)
        End If

        Return message
    End Function
#End Region

#Region "DELETE/RESET"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function RejectUser(ByVal obj As UsersDO) As String
        Dim message As String = String.Empty

        obj.LogonUser = HttpContext.Current.Session("LogonUser")

        If Not obj.RejectUser() Then
            message = obj.errMsg
        Else
            WriteActivityLog(ActivityLog.ActionTaken.REJECT)
        End If

        Return message
    End Function
#End Region


#Region "API"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateUserAPI(ByVal username As String) As UsersDO
        Dim user As New UsersDO With {.Username = username}
        Dim userDO As New UsersDO
        If user.validateProperty("Username") Then
            Dim common As New Common

            Dim apiParams As New VerifyUserIDParams With {
                .Username = username,
                .IPAddress = common.GetIPAddress(True),
                .Browser = HttpContext.Current.Request.Browser.Type,
                .SystemName = GetSection("Commons")("SystemName")
            }

            user = userDO.GetAPIUserInfo(apiParams, username, apiParams)
        End If

        Return user
    End Function

    '<WebMethod(EnableSession:=True)> _
    '<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    'Public Shared Function GenerateUsersReport() As DataTable
    '    Dim obj As New UsersDO
    '    Dim dt As New DataTable

    '    dt = obj.GetUsersListReport()

    '    Return dt
    'End Function
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GenerateUsersReport() As String
        Dim message As String = String.Empty

        HttpContext.Current.Session("ReportName") = "UsersList_Report"

        Return message

    End Function

#End Region

    Class UsersOnLoad
        Property RoleLst As List(Of ReferencesForDropdown)
        Property BranchLst As List(Of ReferencesForDropdown)
        Property RegionLst As List(Of ReferencesForDropdown)
        Property CanInsert As Boolean
        Property CanUpdate As Boolean
        Property CanApprove As Boolean
        Property CanUnlock As Boolean

        Property CanActivate As Boolean

        Property GH As String
        Property TA As String
        Property BH As String

    End Class



End Class