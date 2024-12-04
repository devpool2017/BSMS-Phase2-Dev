Public Class RelationshipOfficerDA
    Inherits MasterDAL
#Region "DECLARATION"
    Dim message As String
    ReadOnly Property errorMessage As String
        Get
            Return message
        End Get
    End Property
#End Region

    Public Function GetListGroupHeads(ByVal RoleID As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@roleID", RoleID)
                dt = .GetDataTable("GetGroupHeadList", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function
End Class
