Public Class OtherParametersDAL
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
        MyBase.moduleName = "Other Parameters"
    End Sub
#End Region

#Region "FUNCTIONS"
    Public Function GetOtherParameters() As DataTable
        With Me
            Return .GetDataTable("GetOtherParameters", True)
        End With
    End Function

    Public Function GetOtherParametersRH(ByVal RegionCode As String) As DataTable
        With Me
            .AddInputParam("@RegionCode", If(Not String.IsNullOrWhiteSpace(RegionCode), RegionCode, Nothing))
            Return .GetDataTable("GetCPADatesRH", True)
        End With
    End Function
#End Region
End Class
