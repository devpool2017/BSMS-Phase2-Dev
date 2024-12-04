Imports System.Web.Services
Imports System.Web.Script.Services
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.DataObjects

Public Class AuditLog
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Database Audit Log"

        Page.DataBind()
    End Sub

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function LoadAuditTrail(ByVal param As DatabaseAuditLogDO)
        param.isSuccess = True

        If Not param.SearchBy = "All" Then
            If Not param.validateProperty(param.SearchBy) Then
                param.isSuccess = False
                Return param
            End If
        End If

        Return DatabaseAuditLogDO.ListAuditTrail(param.DomainID, param.TableName, param.ActionType, param.AuditTrailDate)
    End Function
End Class