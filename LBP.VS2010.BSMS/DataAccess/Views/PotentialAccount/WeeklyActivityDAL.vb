Public Class WeeklyActivityDAL
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
        moduleName = "Weekly Activity"
    End Sub
#End Region

#Region "GET"
    Public Function GetGroupList() As DataTable
        With Me
            Return .GetDataTable("Region_GetList", True)
        End With
    End Function

    Public Function GetUsersList(ByVal groupCode As String,
                                 ByVal branchCode As String,
                                 ByVal RoleID As Int64) As DataTable
        With Me
            .AddInputParam("@RegionCode", If(Not String.IsNullOrWhiteSpace(groupCode), groupCode, Nothing))
            .AddInputParam("@BranchCode", If(Not String.IsNullOrWhiteSpace(branchCode), branchCode, Nothing))
            .AddInputParam("@RoleID", RoleID)
            Return .GetDataTable("GetUserByRegionBranch", True)
        End With
    End Function

    Public Function GetBranchesList(ByVal groupCode As String) As DataTable
        With Me
            .AddInputParam("@SelectedRegion", If(Not String.IsNullOrWhiteSpace(groupCode), groupCode, Nothing))
            Return .GetDataTable("FilteredGetBranchesWithRegionName", True)
        End With
    End Function

    Public Function GetWeeklyActivity(ByVal UploadedBy As String,
                                      ByVal YearNum As String) As DataTable
        With Me
            .AddInputParam("@uploadedby", UploadedBy)
            .AddInputParam("@yearnum", YearNum)
            Return .GetDataTable("GetWeeklyActivityByUploadedBy", True)
        End With
    End Function



    Public Function TotalsForWeeklySummary(ByVal UploadedBy As String,
                                           ByVal DateFrom As String,
                                           ByVal DateTo As String,
                                           ByVal BranchCode As String,
                                           Optional ByVal willCommit As Boolean = False) As DataTable
        With Me
            .AddInputParam("@upby", If(Not String.IsNullOrWhiteSpace(UploadedBy), UploadedBy, Nothing))
            .AddInputParam("@fromdate", If(Not String.IsNullOrWhiteSpace(DateFrom), DateFrom, Nothing))
            .AddInputParam("@todate", If(Not String.IsNullOrWhiteSpace(DateTo), DateTo, Nothing))
            .AddInputParam("@branchCode", If(Not String.IsNullOrWhiteSpace(BranchCode), BranchCode, Nothing))
            Return .GetDataTable("TotalsForWeeklySummaryPerBMSO", willCommit)
        End With
    End Function

    Public Function GetWeeklyActivityLeadCount(ByVal UploadedBy As String,
                                           ByVal YearNum As String,
                                           ByVal monthNum As String,
                                           ByVal WeekNum As String,
                                           Optional ByVal willCommit As Boolean = False) As String
        With Me
            .AddInputParam("@uploadedby", UploadedBy)
            .AddInputParam("@yearnum", YearNum)
            .AddInputParam("@monthnum", monthNum)
            .AddInputParam("@weeknum", WeekNum)
            Return .ExecScalar("GetWeeklyActivityLeadCount", willCommit)
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
            Return .GetDataTable("GetWeeklyActivityCPARevisitClients_WA", willCommit)
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


    Public Function OtherProductList() As DataTable
        With Me
            Return .GetDataTable("SummaryConversion_GetOtherProducts", True)
        End With
    End Function

    Public Function CASAProductList() As DataTable
        With Me
            Return .GetDataTable("SummaryConversion_GetCASAProducts", True)
        End With
    End Function

    Public Function GetLeadSource() As DataTable
        With Me
            Return .GetDataTable("GetLeadSource", True)
        End With
    End Function

#End Region

End Class
