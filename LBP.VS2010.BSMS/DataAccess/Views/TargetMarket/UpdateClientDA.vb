Public Class UpdateClientDA
    Inherits MasterDAL

#Region "DECLARATION"
    Dim message As String
    ReadOnly Property errorMessage As String
        Get
            Return message
        End Get
    End Property
#End Region

#Region "Load Dropdown"
    Public Function YearList(Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("GetYears", willCommit)
        End With
    End Function
    Public Function ClientTypesList(Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("GetClientTypes", willCommit)
        End With
    End Function

    Public Function WeekMinMaxList(ByVal year As String,
                                   ByVal month As String, Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@year", year)
            .AddInputParam("@month", month)
            Return .GetDataTable("GetMaxWeekNum", willCommit)
        End With
    End Function

    Public Function SouceLeadList(Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("GetLeadSource", willCommit)
        End With
    End Function

    Public Function IndustryTypeList(Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("GetIndustry", willCommit)
        End With
    End Function

    Public Function LostReasonList(Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("GetLostReason", willCommit)
        End With
    End Function

    Public Function GetWeekNum(Optional ByVal willCommit As Boolean = True) As DataTable

        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@date", Today.Date.ToString("MM-dd-yyyy"))
                dt = .GetDataTable("GetWeekNum", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function
#End Region

#Region "Load Gridview"
    Public Function GetClientDetails(ByVal ClientID As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@clientid", ClientID)
                dt = .GetDataTable("GetClientInfo", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function
    Public Function GetListUpdateClient(ByVal uploadby As String,
                                        ByVal FromDate As String,
                                        ByVal ToDate As String,
                                        ByVal BrCode As String,
                                        ByVal searchText As String,
                                        ByVal where1 As String,
                                        ByVal where2 As String,
                                        ByVal where3 As String,
                                        ByVal filtertext1 As String,
                                        ByVal filtertext2 As String,
                                        ByVal filtertext3 As String,
                                        ByVal sortby1 As String,
                                        ByVal sortby2 As String,
                                        ByVal sortby3 As String,
                                        ByVal order1 As String,
                                        ByVal order2 As String,
                                        ByVal order3 As String,
                                        ByVal status1 As String,
                                        ByVal status2 As String,
                                        ByVal status3 As String,
                                        ByVal status1b As String,
                                        ByVal status2b As String,
                                        ByVal status3b As String,
                                        Optional ByVal willCommit As Boolean = True) As DataTable

        FromDate = Convert.ToString(FromDate).Substring(0, 10)
        ToDate = Convert.ToString(ToDate).Substring(0, 10)

        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@UploadedBy", uploadby)
                .AddInputParam("@FromDate", FromDate)
                .AddInputParam("@ToDate", ToDate)
                .AddInputParam("@BrCode", BrCode)
                .AddInputParam("@searchText", searchText)
                .AddInputParam("@where1", where1)
                .AddInputParam("@where2", where2)
                .AddInputParam("@where3", where3)
                .AddInputParam("@filtertext1", filtertext1)
                .AddInputParam("@filtertext2", filtertext2)
                .AddInputParam("@filtertext3", filtertext3)
                .AddInputParam("@sortby1", sortby1)
                .AddInputParam("@sortby2", sortby2)
                .AddInputParam("@sortby3", sortby3)
                .AddInputParam("@order1", order1)
                .AddInputParam("@order2", order2)
                .AddInputParam("@order3", order3)
                .AddInputParam("@status1", status1)
                .AddInputParam("@status2", status2)
                .AddInputParam("@status3", status3)
                .AddInputParam("@status1b", status1b)
                .AddInputParam("@status2b", status2b)
                .AddInputParam("@status3b", status3b)
                dt = .GetDataTable("GetClientList", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function
#End Region

#Region "Details"
    Public Function GetFromDate(ByVal year As String, ByVal month As String, ByVal week As String) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@year", year)
                .AddInputParam("@month", month)
                .AddInputParam("@week", week)
                dt = .GetDataTable("GetFromDate", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function
    Public Function GetToDate(ByVal year As String, ByVal month As String, ByVal week As String) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@year", year)
                .AddInputParam("@month", month)
                .AddInputParam("@week", week)
                dt = .GetDataTable("GetToDate", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function

    Public Function GetMinWeekNum(ByVal Year As String,
                              ByVal Month As String,
                              Optional ByVal willCommit As Boolean = True) As String
        With Me
            .AddInputParam("@year", Year)
            .AddInputParam("@month", Month)
            Return .ExecScalar("GetMinWeekNum", willCommit)
        End With
    End Function

    Public Function GetMaxWeekNum(ByVal Year As String,
                              ByVal Month As String,
                              Optional ByVal willCommit As Boolean = True) As String

        With Me
            .AddInputParam("@year", Year)
            .AddInputParam("@month", Month)
            Return .ExecScalar("GetMaxWeekNum", willCommit)
        End With
    End Function
#End Region
End Class
