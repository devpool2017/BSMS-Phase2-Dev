Public Class OtherTablesDA
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
        moduleName = "Other Tables"
    End Sub
#End Region

#Region "List"

    Public Function ddlIndustry(Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("GetIndustry", True)
        End With
    End Function

    Public Function checkDDLValue(ByVal tableName As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@tableName", tableName)
                dt = .GetDataTable("OtherTables_GetTableList", True)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

#End Region

End Class
