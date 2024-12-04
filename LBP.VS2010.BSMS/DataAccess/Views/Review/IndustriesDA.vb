Public Class IndustriesDA
    Inherits MasterDAL

#Region "DECLARATION"
    Dim message As String
    ReadOnly Property errorMessage As String
        Get
            Return message
        End Get
    End Property
#End Region


    Public Function IndustriesList(Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("GetIndustry", willCommit)
        End With
    End Function
End Class
