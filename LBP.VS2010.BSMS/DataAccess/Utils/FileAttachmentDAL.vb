Public Class FileAttachmentDAL
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
        MyBase.moduleName = "File Attachment"
    End Sub
#End Region

#Region "FUNCTION"
    Public Function GetAttachmentList(moduleId As String, acrfNo As String, tabId As String, otherDeterminant As String, Optional willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@moduleId", moduleId)
            .AddInputParam("@acrfNo", acrfNo)
            .AddInputParam("@tabId", tabId)
            .AddInputParam("@otherDeterminant", otherDeterminant)
            Return .GetDataTable("FileAttachments_GetList", willCommit)
        End With

        Return Nothing
    End Function
#End Region
End Class
