Public Class BankProductDA
    Inherits MasterDAL
#Region "DECLARATION"
    Dim message As String
    ReadOnly Property errorMessage As String
        Get
            Return message
        End Get
    End Property
#End Region


    Public Function ProductCategoryList(Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("GetProductCategories", willCommit)
        End With
    End Function

    Public Function GetListBankProduct(ByVal ProductTypes As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@ProductType", ProductTypes)
                dt = .GetDataTable("GetProductListPerProductType", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

End Class
