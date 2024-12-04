Public Class SearchTargetMarketDAL
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

    Public Function RegionList(Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("GetRegions", willCommit)
        End With
    End Function

    'Public Function BranchList(ByVal region As String,
    '                           Optional ByVal willCommit As Boolean = True) As DataTable
    '    With Me
    '        .AddInputParam("@region", region)
    '        Return .GetDataTable("GetBranchUnder", willCommit)
    '    End With
    'End Function

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
    Public Function SearchClientList(ByVal FromDate As String,
                                        ByVal ToDate As String,
                                        ByVal searchCol As String,
                                        ByVal searchText As String,
                                        ByVal where1 As String,
                                        ByVal where2 As String,
                                        ByVal where3 As String,
                                        ByVal where4 As String,
                                        ByVal filtercol1 As String,
                                        ByVal filtercol2 As String,
                                        ByVal filtercol3 As String,
                                        ByVal filtercol4 As String,
                                        ByVal filtertext1 As String,
                                        ByVal filtertext2 As String,
                                        ByVal filtertext3 As String,
                                        ByVal filtertext4 As String,
                                        ByVal sortby1 As String,
                                        ByVal sortby2 As String,
                                        ByVal sortby3 As String,
                                        ByVal sortby4 As String,
                                        ByVal order1 As String,
                                        ByVal order2 As String,
                                        ByVal order3 As String,
                                        ByVal order4 As String,
                                        Optional ByVal willCommit As Boolean = True) As DataTable

        FromDate = Convert.ToString(FromDate).Substring(0, 10)
        ToDate = Convert.ToString(ToDate).Substring(0, 10)

        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@FromDate", FromDate)
                .AddInputParam("@ToDate", ToDate)
                .AddInputParam("@where1", where1)
                .AddInputParam("@where2", where2)
                .AddInputParam("@where3", where3)
                .AddInputParam("@where4", where4)
                .AddInputParam("@filtercol1", filtercol1)
                .AddInputParam("@filtercol2", filtercol2)
                .AddInputParam("@filtercol3", filtercol3)
                .AddInputParam("@filtercol4", filtercol4)
                .AddInputParam("@filtertext1", filtertext1)
                .AddInputParam("@filtertext2", filtertext2)
                .AddInputParam("@filtertext3", filtertext3)
                .AddInputParam("@filtertext4", filtertext4)
                .AddInputParam("@sortby1", sortby1)
                .AddInputParam("@sortby2", sortby2)
                .AddInputParam("@sortby3", sortby3)
                .AddInputParam("@sortby4", sortby4)
                .AddInputParam("@order1", order1)
                .AddInputParam("@order2", order2)
                .AddInputParam("@order3", order3)
                .AddInputParam("@order4", order4)
                .AddInputParam("@searchcol", searchCol)
                .AddInputParam("@searchtext", searchText)
                dt = .GetDataTable("SearchClientList", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function


    Public Function GetClientAllAcctPrevMonthADB(ByVal ClientID As String, ByVal Year As String, ByVal Month As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@clientID", ClientID)
                .AddInputParam("@previousMonthYear", Year)
                .AddInputParam("@previousMonth", Month)
                dt = .GetDataTable("GetClientAllAcctPrevMonthADB", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function GetClientYearlyValuesADB(ByVal ClientID As String, ByVal Year As String, ByVal Month As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@cid", ClientID)
                .AddInputParam("@year", Year)
                .AddInputParam("@monthnum", Month)
                dt = .GetDataTable("GetClientYearlyValues", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function GetAvailedLoansByClientID(ByVal ClientID As String, ByVal ClientType As String, Optional ByVal LoanCodes As String = Nothing, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@ClientID", ClientID)
                .AddInputParam("@ClientType", ClientType)
                .AddInputParam("@LoanCodes", LoanCodes)
                dt = .GetDataTable("GetAvailedLoansByClientID", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function
#End Region

#Region "Details"
    Public Function CASAProductList() As DataTable
        Dim dt As New DataTable
        Try
            With Me
                dt = .GetDataTable("GetCASAProducts", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function


    Public Function LoanProductList() As DataTable
        Dim dt As New DataTable
        Try
            With Me
                dt = .GetDataTable("GetLoanProducts", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function

    Public Function OtherProductList() As DataTable
        Dim dt As New DataTable
        Try
            With Me
                dt = .GetDataTable("GetOtherProducts", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function
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
