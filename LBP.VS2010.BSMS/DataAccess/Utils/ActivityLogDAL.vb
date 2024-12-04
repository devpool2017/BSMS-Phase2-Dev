Public Class ActivityLogDAL
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
        MyBase.moduleName = "Activity Log"
    End Sub
#End Region
#Region "FUNCTIONS"
    Public Function WriteLog(ByVal ModuleName As String,
                             ByVal ActivityType As String,
                             ByVal UserName As String,
                             ByVal IPAddress As String,
                             ByVal Browser As String) As Boolean
        With Me
            .AddInputParam("@ModuleName", ModuleName)
            .AddInputParam("@ActivityType", ActivityType)
            .AddInputParam("@Username", UserName)
            .AddInputParam("@IPAddress", IPAddress)
            .AddInputParam("@Browser", Browser)
            Return .ExecNonQuery("ActivityLog_Insert", True)
        End With
    End Function
#End Region

End Class
