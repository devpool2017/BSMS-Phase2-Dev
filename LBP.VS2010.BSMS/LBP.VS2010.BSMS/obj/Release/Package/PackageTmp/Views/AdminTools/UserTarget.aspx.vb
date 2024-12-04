Imports System.Web.Services
Imports System.Web.Script.Services
Imports LBP.VS2010.BSMS.DataObjects

Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages.Validation
Imports System.Configuration.ConfigurationManager

Public Class UserTarget
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "User Target"

        If Not Page.IsPostBack Then
            Page.DataBind()
        End If
    End Sub


    <WebMethod(EnableSession:=True)>
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function OnLoadData() As UserTargetOnLoad
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return New UserTargetOnLoad With {
            .currentUser = currentUser,
            .RoleLst = ReferencesForDropdown.convertToReferencesList(UsersDO.GetRoleList, "RoleID", "RoleName", True),
            .RegionLst = ReferencesForDropdown.convertToReferencesList(UsersDO.GetRegionList, "RegionCode", "RegionName", True),
            .BranchLst = ReferencesForDropdown.convertToReferencesList(UsersDO.GetBranchList, "BranchCode", "BranchDesc", True),
            .CanInsert = HasAccess("CanInsert"),
            .CanUpdate = HasAccess("CanUpdate"),
        .CanApprove = HasAccess("CanApprove")
        }
    End Function


    <WebMethod(EnableSession:=True)>
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Shared Function GetBranchPerRegion(ByVal regionCode As String) As List(Of ReferencesForDropdown)
        Dim BranchLst As List(Of ReferencesForDropdown)
        BranchLst = ReferencesForDropdown.convertToReferencesList(UsersDO.GetBranchListPerRegion(regionCode), "BranchCode", "BranchDesc", True)
        Return BranchLst
    End Function

    <WebMethod(EnableSession:=True)>
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetList(regionCode As String, brCode As String) As List(Of UserTargetDO)
        WriteActivityLog(ActivityLog.ActionTaken.SEARCH)

        Dim params As New UserTargetDO
        params.GroupCode = IIf(String.IsNullOrWhiteSpace(regionCode), Nothing, regionCode)
        params.BranchCode = IIf(String.IsNullOrWhiteSpace(brCode), Nothing, brCode)

        HttpContext.Current.Session("currentUsersParameter") = params

        Return UserTargetDO.GetList(regionCode, brCode, GetModuleAccess())
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetTempList(regionCode As String, brCode As String, filterStatus As String) As List(Of UserTargetDO)
        WriteActivityLog(ActivityLog.ActionTaken.SEARCH)

        Return UserTargetDO.GetTempList(regionCode, brCode, filterStatus)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetDetails(username As String, action As String) As UserTargetDO

        WriteActivityLog(ActivityLog.ActionTaken.VIEW)
        Dim obj As New UserTargetDO
        obj = UserTargetDO.GetDetails(username, action)
        Return obj
    End Function

    <WebMethod(EnableSession:=True)>
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetMergeDetails(username As String) As List(Of UserTargetDO.UserTargetResult)
        WriteActivityLog(ActivityLog.ActionTaken.VIEW)

        Return UserTargetDO.GetMergeDetails(username)
    End Function

    <WebMethod(EnableSession:=True)>
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ApproveUserTarget(obj As UserTargetDO) As String
        Dim message As String = String.Empty

        obj.LogonUser = HttpContext.Current.Session("LogonUser")
        If Not obj.ApproveUser() Then
            message = obj.errMsg
        Else
            WriteActivityLog(ActivityLog.ActionTaken.APPROVE)
        End If

    
        Return message
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function RejectUserTarget(obj As UserTargetDO) As String
        Dim message As String = String.Empty

        obj.LogonUser = HttpContext.Current.Session("LogonUser")

        If Not obj.RejectUserTarget() Then
            message = obj.errMsg
        Else
            WriteActivityLog(ActivityLog.ActionTaken.REJECT)
        End If

        Return message
    End Function

    <WebMethod(EnableSession:=True)>
  <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Shared Function BranchManagerListRegionBranch(ByVal refobj As UserTargetDO) As List(Of ReferencesForDropdown)
        Dim obj As New UserTargetDO
        obj.GroupCode = refobj.GroupCode
        obj.BranchCode = refobj.BranchCode
        obj.Role = refobj.Role
        Dim BranchHeadLst As List(Of ReferencesForDropdown)
        BranchHeadLst = ReferencesForDropdown.convertToReferencesList(obj.GetBHPerBrCode(), "UserID", "FullName", True)
        Return BranchHeadLst
    End Function

    <WebMethod(EnableSession:=True)>
  <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateUserTarget(obj As UserTargetDO) As String
        Dim message As String = String.Empty

        If Not (obj.isValid()) Then
            message = obj.errMsg
        Else
            If Not (obj.IsUserTargetExists(obj.Username)) Then
                message = obj.errMsg
            End If
        End If

            Return message
    End Function

    <WebMethod(EnableSession:=True)>
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SaveTarget(obj As UserTargetDO) As String
        Dim message As String = String.Empty

        obj.LogonUser = HttpContext.Current.Session("LogonUser")
        obj.Status = "1"
        If Not obj.SaveUserTarget() Then
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


    Class UserTargetOnLoad
        Property currentUser As Object
        Property RoleLst As List(Of ReferencesForDropdown)
        Property BranchLst As List(Of ReferencesForDropdown)
        Property RegionLst As List(Of ReferencesForDropdown)
        Property CanInsert As Boolean
        Property CanUpdate As Boolean
        Property CanApprove As Boolean

    End Class
End Class
