Imports System.Web.Services
Imports System.Web.Script.Services
Imports LBP.VS2010.BSMS.DataObjects

Public Class Role
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Role Access Matrix"

        If Not Page.IsPostBack Then
            Page.DataBind()
        End If
    End Sub

#Region "GET/LIST"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetList(searchText As String, filterStat As String) As List(Of RoleDO)
        WriteActivityLog(ActivityLog.ActionTaken.SEARCH)

        Return RoleDO.GetList(searchText, filterStat, GetModuleAccess())
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetTempList(searchText As String, filterStatus As String) As List(Of RoleDO)
        WriteActivityLog(ActivityLog.ActionTaken.SEARCH)

        Return RoleDO.GetTempList(searchText, filterStatus)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetDetails(roleId As String) As RoleDO
        WriteActivityLog(ActivityLog.ActionTaken.VIEW)

        Return RoleDO.GetDetails(roleId)
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
                Case "1"
                    actionType = ActivityLog.ActionTaken.CREATE
                Case "3"
                    actionType = ActivityLog.ActionTaken.DEACTIVATE
                Case "5"
                    actionType = ActivityLog.ActionTaken.UPDATE
                Case Else
                    actionType = ActivityLog.ActionTaken.ACTIVATE
            End Select

            WriteActivityLog(actionType)
        End If

        Return mesasge
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function RedirectToDetails(roleId As String, tempId As String, action As String, mode As Integer) As String
        Dim data = New With {
            roleId,
            tempId,
            action,
            mode
        }

        HttpContext.Current.Session("roleData") = data

        Return "/Views/AdminTools/RoleDetails.aspx?submenuid=" + HttpContext.Current.Session("SubMenuId")
    End Function
#End Region
End Class