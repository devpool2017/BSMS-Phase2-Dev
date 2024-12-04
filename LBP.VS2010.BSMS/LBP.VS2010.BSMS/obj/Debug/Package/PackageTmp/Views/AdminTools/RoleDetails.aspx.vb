Imports System.Web.Script.Services
Imports System.Web.Services
Imports LBP.VS2010.BSMS.DataObjects

Public Class RoleDetails
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Role Access Matrix"

        If Not Page.IsPostBack Then
            Page.DataBind()
        End If
    End Sub

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetDetails(mode As Integer?) As RoleDO
        Dim data = HttpContext.Current.Session("roleData")

        If Not IsNothing(data) Then
            Dim rdo As New RoleDO

            If data.mode = 1 Or mode = 1 Then
                rdo = RoleDO.GetDetails(data.roleId, data.action, GetModuleAccess())
            ElseIf data.mode = 2 Or mode = 2 Then
                rdo = RoleDO.GetTempDetails(data.tempId, data.action, GetModuleAccess())
            End If

            Return rdo
        End If

        Return Nothing
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function RedirectToHome() As String
        Return "/Views/AdminTools/Role.aspx?submenuid=" + HttpContext.Current.Session("SubMenuId")
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateRole(role As RoleDO) As String
        Dim mesasge As String = String.Empty

        If Not role.isValid() Then
            mesasge = role.errMsg
        End If

        Return mesasge
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SaveTemp(role As RoleDO) As String
        Dim mesasge As String = String.Empty

        role.LogonUser = HttpContext.Current.Session("LogonUser")

        If Not role.SaveTemp() Then
            mesasge = role.errMsg
        Else
            Dim actionType As ActivityLog.ActionTaken
            Select Case role.Action
                Case "6"
                    actionType = ActivityLog.ActionTaken.CREATE
                Case "4"
                    actionType = ActivityLog.ActionTaken.DELETE
                Case Else
                    actionType = ActivityLog.ActionTaken.UPDATE
            End Select

            WriteActivityLog(actionType)
        End If

        Return mesasge
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function Approve(role As RoleDO) As String
        Dim message As String = String.Empty

        role.LogonUser = HttpContext.Current.Session("LogonUser")

        If Not role.Approve() Then
            message = role.errMsg
        Else
            WriteActivityLog(ActivityLog.ActionTaken.APPROVE)
        End If

        Return message
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function Reject(roleTempId As String) As String
        WriteActivityLog(ActivityLog.ActionTaken.REJECT)

        Return RoleDO.Reject(roleTempId, HttpContext.Current.Session("LogonUser"))
    End Function
End Class