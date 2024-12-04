Public Class UsersActivityLogDAL
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
        MyBase.moduleName = "User Activity"
    End Sub
#End Region

    Public Function listUsersActivity(ByVal ModuleName As String, ByVal ActivityType As String, ByVal UserName As String, ByVal ActivityDate As String,
                                      ByVal Browser As String) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@ModuleName", IIf(ModuleName = "", DBNull.Value, ModuleName))
                .AddInputParam("@ActivityType", IIf(ActivityType = "", DBNull.Value, ActivityType))
                .AddInputParam("@UserName", IIf(UserName = "", DBNull.Value, UserName))
                .AddInputParam("@ActivityDate", IIf(ActivityDate = "", DBNull.Value, ActivityDate))
                .AddInputParam("@Browser", IIf(Browser = "", DBNull.Value, Browser))
                dt = .GetDataTable("ActivityLog_GetList", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function

End Class
