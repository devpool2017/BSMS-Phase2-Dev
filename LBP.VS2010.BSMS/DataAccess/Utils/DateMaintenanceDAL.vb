Public Class DateMaintenanceDAL
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
        MyBase.moduleName = "Date Maintenance"
    End Sub
#End Region

#Region "FUNCTION"
    Public Function GetCurrentDateDefaultValue() As DataTable
        With Me
            Return .GetDataTable("SummaryConversion_GetDateDefaultValue", True)
        End With
    End Function
    Public Function GetYear(Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("GetYears", willCommit)
        End With
    End Function

    Public Function GetMaxWeekNum(ByVal year As String,
                                  ByVal month As String,
                                  Optional ByVal willCommit As Boolean = True) As String
        With Me
            .AddInputParam("@year", year)
            .AddInputParam("@month", month)
            Return .ExecScalar("GetMaxWeekNum", willCommit)
        End With
    End Function

    Public Function GetMinWeekNum(ByVal year As String,
                                  ByVal month As String,
                                  Optional ByVal willCommit As Boolean = True) As String
        With Me
            .AddInputParam("@year", year)
            .AddInputParam("@month", month)
            Return .ExecScalar("GetMinWeekNum", willCommit)
        End With
    End Function

    Public Function GetFromDate(ByVal year As String,
                                      ByVal month As String,
                                      ByVal week As String,
                                      Optional ByVal willCommit As Boolean = False) As String
        With Me
            .AddInputParam("@year", year)
            .AddInputParam("@month", month)
            .AddInputParam("@week", week)
            Return .ExecScalar("GetFromDate", willCommit)
        End With
    End Function

    Public Function GetToDate(ByVal year As String,
                                      ByVal month As String,
                                      ByVal week As String,
                                  Optional ByVal willCommit As Boolean = True) As String
        With Me
            .AddInputParam("@year", year)
            .AddInputParam("@month", month)
            .AddInputParam("@week", week)
            Return .ExecScalar("GetToDate", willCommit)
        End With
    End Function


#End Region
End Class
