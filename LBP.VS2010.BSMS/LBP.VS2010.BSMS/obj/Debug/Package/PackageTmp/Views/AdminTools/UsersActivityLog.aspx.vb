Imports System.Web.Services
Imports System.Web.Script.Services
Imports LBP.VS2010.BSMS.DataObjects

Public Class UsersActivityLog
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Users Activity Log"

        If Not Page.IsPostBack Then
            Page.DataBind()
        End If
    End Sub

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function LoadUserActivity(ByVal param As UsersActivityLogDO)
        param.isSuccess = True

        If Not param.SearchBy = "All" Then
            If Not param.validateProperty(param.SearchBy) Then
                param.isSuccess = False
                Return param
            End If
        End If

        Return UsersActivityLogDO.ListUsersActivity(param.ModuleName, param.ActivityType, param.UserName, param.ActivityDate, param.Browser)
    End Function
End Class