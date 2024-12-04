Public Class DatesDA
    Inherits MasterDAL
#Region "DECLARATION"
    Dim message As String
    ReadOnly Property errorMessage As String
        Get
            Return message
        End Get
    End Property
#End Region

#Region "DatesDA"
    Public Function DateList(Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("GetYears", willCommit)
        End With
    End Function


    Public Function YearList(Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("GetYears", willCommit)
        End With
    End Function

    Public Function YearMonthWeekListAll(Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("GetListDates", willCommit)
        End With
    End Function


    Public Function WeekLists(ByVal Year As String, ByVal Month As String, ByVal Week As String, Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@SelectedYear", Year)
            .AddInputParam("@SelectedMonth", Month)
            .AddInputParam("@SelectedWeek", Week)
            Return .GetDataTable("FilteredViewDates", willCommit)
        End With
    End Function

    Public Function CheckIfExist(ByVal Year As String, ByVal Month As String, ByVal Week As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            AddInputParam("@month", Year)
            AddInputParam("@year", Month)
            AddInputParam("@week", Week)
            Return .ExecScalar("CheckDateExists", willCommit)
        End With
    End Function

    Public Function SaveDateInsert(ByVal Year As String,
                                    ByVal Month As String,
                                    ByVal Week As String,
                                    ByVal FromDate As String,
                                    ByVal ToDate As String,
                                    ByVal addedByUsername As String,
                                    ByVal natureOfChange As String,
                       Optional ByVal willCommit As Boolean = True) As Boolean

        With Me
            .AddInputParam("@year", Year)
            .AddInputParam("@month", Month)
            .AddInputParam("@week", Week)
            .AddInputParam("@fromdate", FromDate)
            .AddInputParam("@todate", ToDate)
            .AddInputParam("@addedByUsername", addedByUsername)
            .AddInputParam("@natureOfChange", natureOfChange)
            Return .ExecNonQuery("AddDate", willCommit)
        End With
    End Function

#End Region
End Class
