Public Class ConversionSummaryDAL
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
        MyBase.moduleName = "Conversion Summary Report"
    End Sub
#End Region

#Region "WEEKLY SUMMARY REPORT"
    Public Function GetRegionList() As DataTable
        With Me
            Return .GetDataTable("SummaryConversion_GetRegionList", True)
        End With
    End Function

    Public Function GetUsersList(ByVal branchCode As String,
                                 ByVal roleID As String) As DataTable
        With Me
            .AddInputParam("@roleID", roleID)
            .AddInputParam("@branchCode", branchCode)
            Return .GetDataTable("SummaryConversion_GetUsersList", True)
        End With
    End Function

    Public Function GetBranchesList(ByVal groupCode As String) As DataTable
        With Me
            '.AddInputParam("@username", If(Not String.IsNullOrWhiteSpace(userName), userName, Nothing))
            .AddInputParam("@regionCode", If(Not String.IsNullOrWhiteSpace(groupCode), groupCode, Nothing))
            Return .GetDataTable("SummaryConversion_GetBranchesList", True)
        End With
    End Function

    Public Function CASAProductList() As DataTable
        With Me
            Return .GetDataTable("SummaryConversion_GetCASAProducts", True)
        End With
    End Function

    Public Function OtherProductList() As DataTable
        With Me
            Return .GetDataTable("SummaryConversion_GetOtherProducts", True)
        End With
    End Function

    Public Function LoanProductList() As DataTable
        With Me
            Return .GetDataTable("SummaryConversion_GetLoanProducts", True)
        End With
    End Function

    Public Function AllProductList() As DataTable
        With Me
            Return .GetDataTable("SummaryConversion_GetOtherATypes", True)
        End With
    End Function

    Public Function GetNewLeadsList(ByVal UploadedBy As String,
                         ByVal DateFrom As String,
                         ByVal DateToday As String,
                         ByVal branchCode As String,
                         Optional ByVal willCommit As Boolean = False) As DataTable
        With Me
            .AddInputParam("@upby", If(Not String.IsNullOrWhiteSpace(UploadedBy), UploadedBy, Nothing))
            .AddInputParam("@fromdate", If(Not String.IsNullOrWhiteSpace(DateFrom), DateFrom, Nothing))
            .AddInputParam("@todate", If(Not String.IsNullOrWhiteSpace(DateToday), DateToday, Nothing))
            .AddInputParam("@branchCode", If(Not String.IsNullOrWhiteSpace(branchCode), branchCode, Nothing))
            Return .GetDataTable("GetClientsFilteredByDate", willCommit)
        End With
    End Function

    Public Function GetPotentialAccountsList(ByVal UploadedBy As String,
                     ByVal DateFrom As String,
                     ByVal DateToday As String,
                     ByVal branchCode As String,
                     Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@upby", If(Not String.IsNullOrWhiteSpace(UploadedBy), UploadedBy, Nothing))
            .AddInputParam("@fromdate", If(Not String.IsNullOrWhiteSpace(DateFrom), DateFrom, Nothing))
            .AddInputParam("@todate", If(Not String.IsNullOrWhiteSpace(DateToday), DateToday, Nothing))
            .AddInputParam("@branchCode", If(Not String.IsNullOrWhiteSpace(branchCode), branchCode, Nothing))
            Return .GetDataTable("GetWeeklyActivityCPARevisitClients", willCommit)
        End With
    End Function

    Public Function ChkWeeklyRemarksExists(ByVal TableName As String,
                                           ByVal Year As String,
                                           ByVal MonthName As String,
                                           ByVal WeekNum As String,
                                           ByVal Username As String, Optional ByVal willCommit As Boolean = False) As Boolean
        With Me
            .AddInputParam("@year", If(Not String.IsNullOrWhiteSpace(Year), Year, Nothing))
            .AddInputParam("@monthName", If(Not String.IsNullOrWhiteSpace(MonthName), MonthName, Nothing))
            .AddInputParam("@weekNum", If(Not String.IsNullOrWhiteSpace(WeekNum), WeekNum, Nothing))
            .AddInputParam("@username", If(Not String.IsNullOrWhiteSpace(Username), Username, Nothing))
            .AddInputParam("@tableName", If(Not String.IsNullOrWhiteSpace(TableName), TableName, Nothing))
            Return CBool(.ExecScalar("SummaryConversion_ChkWeeklyRemarksExists", willCommit))
        End With
    End Function

    Public Function GetWeeklyRemarks(ByVal TableName As String,
                                       ByVal Year As String,
                                       ByVal MonthName As String,
                                       ByVal WeekNum As String,
                                       ByVal Username As String,
                                       Optional ByVal willCommit As Boolean = True) As String
        With Me
            .AddInputParam("@year", If(Not String.IsNullOrWhiteSpace(Year), Year, Nothing))
            .AddInputParam("@monthName", If(Not String.IsNullOrWhiteSpace(MonthName), MonthName, Nothing))
            .AddInputParam("@weekNum", If(Not String.IsNullOrWhiteSpace(WeekNum), WeekNum, Nothing))
            .AddInputParam("@username", If(Not String.IsNullOrWhiteSpace(Username), Username, Nothing))
            .AddInputParam("@tableName", If(Not String.IsNullOrWhiteSpace(TableName), TableName, Nothing))
            Return .ExecScalar("SummaryConversion_GetWeeklyRemarks", willCommit)
        End With
    End Function

    Public Function AddWeeklyRemarks(ByVal TableName As String,
                                   ByVal Year As String,
                                   ByVal MonthName As String,
                                   ByVal WeekNum As String,
                                   ByVal Username As String,
                                   ByVal Comment As String) As Boolean
        With Me
            .AddInputParam("@year", If(Not String.IsNullOrWhiteSpace(Year), Year, Nothing))
            .AddInputParam("@monthName", If(Not String.IsNullOrWhiteSpace(MonthName), MonthName, Nothing))
            .AddInputParam("@weekNum", If(Not String.IsNullOrWhiteSpace(WeekNum), WeekNum, Nothing))
            .AddInputParam("@username", If(Not String.IsNullOrWhiteSpace(Username), Username, Nothing))
            .AddInputParam("@tableName", If(Not String.IsNullOrWhiteSpace(TableName), TableName, Nothing))
            .AddInputParam("@weeklycomment", If(Not String.IsNullOrWhiteSpace(Comment), Comment, Nothing))
            Return .ExecNonQuery("SummaryConversion_AddWeeklyRemarks", True)
        End With
    End Function

    Public Function UpdateWeeklyRemarks(ByVal TableName As String,
                                   ByVal Year As String,
                                   ByVal MonthName As String,
                                   ByVal WeekNum As String,
                                   ByVal Username As String,
                                   ByVal Comment As String) As Boolean
        With Me
            .AddInputParam("@year", If(Not String.IsNullOrWhiteSpace(Year), Year, Nothing))
            .AddInputParam("@monthName", If(Not String.IsNullOrWhiteSpace(MonthName), MonthName, Nothing))
            .AddInputParam("@weekNum", If(Not String.IsNullOrWhiteSpace(WeekNum), WeekNum, Nothing))
            .AddInputParam("@username", If(Not String.IsNullOrWhiteSpace(Username), Username, Nothing))
            .AddInputParam("@tableName", If(Not String.IsNullOrWhiteSpace(TableName), TableName, Nothing))
            .AddInputParam("@weeklycomment", If(Not String.IsNullOrWhiteSpace(Comment), Comment, Nothing))
            Return .ExecNonQuery("SummaryConversion_UpdateWeeklyRemarks", True)
        End With
    End Function

    Public Function GetTargetLeads(ByVal UploadedBy As String,
Optional ByVal willCommit As Boolean = True) As String
        With Me
            .AddInputParam("@username", If(Not String.IsNullOrWhiteSpace(UploadedBy), UploadedBy, Nothing))
            Return .ExecScalar("GetTargetLeads", willCommit)
        End With
    End Function
#End Region
#Region "MONTHLY SUMMARY REPORT"
    Public Function TotalsForWeeklySummary(ByVal UploadedBy As String,
                        ByVal DateFrom As String,
                        ByVal DateToday As String,
                        ByVal branchCode As String,
                        Optional ByVal willCommit As Boolean = False) As DataTable
        With Me
            .AddInputParam("@upby", If(Not String.IsNullOrWhiteSpace(UploadedBy), UploadedBy, Nothing))
            .AddInputParam("@fromdate", If(Not String.IsNullOrWhiteSpace(DateFrom), DateFrom, Nothing))
            .AddInputParam("@todate", If(Not String.IsNullOrWhiteSpace(DateToday), DateToday, Nothing))
            .AddInputParam("@branchCode", If(Not String.IsNullOrWhiteSpace(branchCode), branchCode, Nothing))
            Return .GetDataTable("TotalsForWeeklySummaryPerBMSO", willCommit)
        End With
    End Function
    Public Function ChkMonthlyRemarksExists(ByVal TableName As String,
                                           ByVal Year As String,
                                           ByVal MonthName As String,
                                           ByVal Username As String, Optional ByVal willCommit As Boolean = False) As Boolean
        With Me
            .AddInputParam("@year", If(Not String.IsNullOrWhiteSpace(Year), Year, Nothing))
            .AddInputParam("@monthName", If(Not String.IsNullOrWhiteSpace(MonthName), MonthName, Nothing))
            .AddInputParam("@username", If(Not String.IsNullOrWhiteSpace(Username), Username, Nothing))
            .AddInputParam("@tableName", If(Not String.IsNullOrWhiteSpace(TableName), TableName, Nothing))
            Return CBool(.ExecScalar("SummaryConversion_ChkMonthlyRemarksExists", willCommit))
        End With
    End Function

    Public Function GetMonthlyRemarks(ByVal TableName As String,
                                       ByVal Year As String,
                                       ByVal MonthName As String,
                                       ByVal Username As String,
                                       Optional ByVal willCommit As Boolean = True) As String
        With Me
            .AddInputParam("@year", If(Not String.IsNullOrWhiteSpace(Year), Year, Nothing))
            .AddInputParam("@monthName", If(Not String.IsNullOrWhiteSpace(MonthName), MonthName, Nothing))
            .AddInputParam("@username", If(Not String.IsNullOrWhiteSpace(Username), Username, Nothing))
            .AddInputParam("@tableName", If(Not String.IsNullOrWhiteSpace(TableName), TableName, Nothing))
            Return .ExecScalar("SummaryConversion_GetMonthlyRemarks", willCommit)
        End With
    End Function
    Public Function AddMonthlyRemarks(ByVal TableName As String,
                                   ByVal Year As String,
                                   ByVal MonthName As String,
                                   ByVal Username As String,
                                   ByVal Comment As String) As Boolean
        With Me
            .AddInputParam("@year", If(Not String.IsNullOrWhiteSpace(Year), Year, Nothing))
            .AddInputParam("@monthName", If(Not String.IsNullOrWhiteSpace(MonthName), MonthName, Nothing))
            .AddInputParam("@username", If(Not String.IsNullOrWhiteSpace(Username), Username, Nothing))
            .AddInputParam("@tableName", If(Not String.IsNullOrWhiteSpace(TableName), TableName, Nothing))
            .AddInputParam("@Monthlycomment", If(Not String.IsNullOrWhiteSpace(Comment), Comment, Nothing))
            Return .ExecNonQuery("SummaryConversion_AddMonthlyRemarks", True)
        End With
    End Function

    Public Function UpdateMonthlyRemarks(ByVal TableName As String,
                                   ByVal Year As String,
                                   ByVal MonthName As String,
                                   ByVal Username As String,
                                   ByVal Comment As String) As Boolean
        With Me
            .AddInputParam("@year", If(Not String.IsNullOrWhiteSpace(Year), Year, Nothing))
            .AddInputParam("@monthName", If(Not String.IsNullOrWhiteSpace(MonthName), MonthName, Nothing))
            .AddInputParam("@username", If(Not String.IsNullOrWhiteSpace(Username), Username, Nothing))
            .AddInputParam("@tableName", If(Not String.IsNullOrWhiteSpace(TableName), TableName, Nothing))
            .AddInputParam("@Monthlycomment", If(Not String.IsNullOrWhiteSpace(Comment), Comment, Nothing))
            Return .ExecNonQuery("SummaryConversion_UpdateMonthlyRemarks", True)
        End With
    End Function
    Public Function GetTargetADB(Optional ByVal willCommit As Boolean = True) As String
        With Me
            Return .ExecScalar("GetTargetADB", willCommit)
        End With
    End Function
    Public Function GetTargetNewAccountsClosed(Optional ByVal willCommit As Boolean = True) As String
        With Me
            Return .ExecScalar("GetTargetNewAccountsClosed", willCommit)
        End With
    End Function

    Public Function GetMonthlyActualADBGeneratedPerBM(ByVal BranchManager As String,
                                                      ByVal Year As String,
                                                      ByVal MonthNumber As String,
                                                      Optional ByVal willCommit As Boolean = True) As String
        With Me
            .AddInputParam("@bmsoUsername", If(Not String.IsNullOrWhiteSpace(BranchManager), BranchManager, Nothing))
            .AddInputParam("@year", If(Not String.IsNullOrWhiteSpace(Year), Year, Nothing))
            .AddInputParam("@monthnumber", If(Not String.IsNullOrWhiteSpace(MonthNumber), MonthNumber, Nothing))
            Return .ExecScalar("GetMonthlyActualADBGeneratedPerBMSO", willCommit)
        End With
    End Function

#End Region
#Region "ANNUALLY SUMMARY REPORT"
    Public Function GetAnnualActualADBGeneratedPerBMSO(ByVal BranchManager As String,
                                                      ByVal Year As String,
                                                      Optional ByVal willCommit As Boolean = True) As String
        With Me
            .AddInputParam("@bmsoUsername", If(Not String.IsNullOrWhiteSpace(BranchManager), BranchManager, Nothing))
            .AddInputParam("@year", If(Not String.IsNullOrWhiteSpace(Year), Year, Nothing))
            Return .ExecScalar("GetAnnualActualADBGeneratedPerBMSO", willCommit)
        End With
    End Function

#End Region
End Class
