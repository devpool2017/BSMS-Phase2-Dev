Public Class DatabaseAuditLogDAL
    Inherits MasterDAL
#Region "DECLARATION"
    Dim message As String

    ReadOnly Property errorMessage As String
        Get
            Return message
        End Get
    End Property
#End Region

#Region "INSTANTIATION/NEW"
    ' ALWAYS OVERRIDE THE MODULENAME PER DAL
    Sub New()
        MyBase.moduleName = "Audit Trail"
    End Sub
#End Region

    Public Function listAuditTrail(ByVal DomainID As String, ByVal TableName As String, ByVal ActionType As String, ByVal AuditTrailDate As String) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@Username", IIf(DomainID = "", DBNull.Value, DomainID))
                .AddInputParam("@TableName", IIf(TableName = "", DBNull.Value, TableName))
                .AddInputParam("@ActionType", IIf(ActionType = "", DBNull.Value, ActionType))
                .AddInputParam("@Date", IIf(AuditTrailDate = "", DBNull.Value, AuditTrailDate))
                dt = .GetDataTable("AuditTrail_GetList", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function

End Class
