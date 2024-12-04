Public Class SummaryPerGroupDAL
    Inherits MasterDAL

#Region "DECLARATION"
    Dim message As String
    ReadOnly Property errorMessage As String
        Get
            Return message
        End Get
    End Property
#End Region
#Region "FUNCTIONS"
    Public Function GetClientTypes() As DataTable
        With Me
            Return .GetDataTable("GetClientTypes", True)
        End With
    End Function

    Public Function GetAllIndustryType() As DataTable
        With Me
            Return .GetDataTable("GetAllIndustryDesc", True)
        End With
    End Function

    Public Function GetADBSizingRangeName() As DataTable
        With Me
            Return .GetDataTable("GetADBSizingRangeName", True)
        End With
    End Function

#Region "WEEKLY"
    Public Function GetWeeklyList(ByVal GroupCode As String,
                   ByVal Year As String,
                   ByVal MonthName As String,
                   ByVal Month As String,
                   ByVal Week As String,
                    ByVal ClientType As String,
                    ByVal RoleID As String,
                   Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@regioncode", If(Not String.IsNullOrWhiteSpace(GroupCode), GroupCode, Nothing))
            .AddInputParam("@year", If(Not String.IsNullOrWhiteSpace(Year), Year, Nothing))
            .AddInputParam("@monthname", If(Not String.IsNullOrWhiteSpace(MonthName), MonthName, Nothing))
            .AddInputParam("@monthnum", If(Not String.IsNullOrWhiteSpace(Month), Month, Nothing))
            .AddInputParam("@weeknum", If(Not String.IsNullOrWhiteSpace(Week), Week, Nothing))
            .AddInputParam("@clienttype", If(Not String.IsNullOrWhiteSpace(ClientType), ClientType, Nothing))
            .AddInputParam("@roleID", If(Not String.IsNullOrWhiteSpace(RoleID), RoleID, Nothing))
            Return .GetDataTable("WeeklySummaryPerRegion_GetDetails", willCommit)
        End With
    End Function

#End Region


#Region "MONTHLY"
    Public Function GetMonthlyList(ByVal GroupCode As String,
                   ByVal Year As String,
                   ByVal MonthName As String,
                   ByVal Month As String,
                    ByVal ClientType As String,
                   ByVal RoleID As String,
                   Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@regioncode", If(Not String.IsNullOrWhiteSpace(GroupCode), GroupCode, Nothing))
            .AddInputParam("@year", If(Not String.IsNullOrWhiteSpace(Year), Year, Nothing))
            .AddInputParam("@monthname", If(Not String.IsNullOrWhiteSpace(MonthName), MonthName, Nothing))
            .AddInputParam("@monthnum", If(Not String.IsNullOrWhiteSpace(Month), Month, Nothing))
            .AddInputParam("@clienttype", If(Not String.IsNullOrWhiteSpace(ClientType), ClientType, Nothing))
            .AddInputParam("@roleID", If(Not String.IsNullOrWhiteSpace(RoleID), RoleID, Nothing))
            Return .GetDataTable("MonthlySummaryPerRegion_GetDetails", willCommit)
        End With
    End Function

#End Region
#Region "ANNUAL"
    Public Function GetAnnualList(ByVal GroupCode As String,
                   ByVal Year As String,
                   ByVal ClientType As String,
                   ByVal IndustryType As String,
                   ByVal ADBRangeName As String,
                   ByVal RoleID As String,
                   Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@regioncode", If(Not String.IsNullOrWhiteSpace(GroupCode), GroupCode, Nothing))
            .AddInputParam("@year", If(Not String.IsNullOrWhiteSpace(Year), Year, Nothing))
            .AddInputParam("@clienttype", If(Not String.IsNullOrWhiteSpace(ClientType), ClientType, Nothing))
            .AddInputParam("@industrytype", If(Not String.IsNullOrWhiteSpace(IndustryType), IndustryType, Nothing))
            .AddInputParam("@adbrangename", If(Not String.IsNullOrWhiteSpace(ADBRangeName), ADBRangeName, Nothing))
            .AddInputParam("@roleID", If(Not String.IsNullOrWhiteSpace(RoleID), RoleID, Nothing))
            Return .GetDataTable("AnnualSummaryPerRegion_GetDetails", willCommit)
        End With
    End Function
    Public Function GetAnnualTotalList(ByVal GroupCode As String,
                  ByVal Year As String,
                  ByVal ClientType As String,
                  ByVal IndustryType As String,
                  ByVal ADBRangeName As String,
                  ByVal RoleID As String,
                  Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@regioncode", If(Not String.IsNullOrWhiteSpace(GroupCode), GroupCode, Nothing))
            .AddInputParam("@year", If(Not String.IsNullOrWhiteSpace(Year), Year, Nothing))
            .AddInputParam("@clienttype", If(Not String.IsNullOrWhiteSpace(ClientType), ClientType, Nothing))
            .AddInputParam("@industrytype", If(Not String.IsNullOrWhiteSpace(IndustryType), IndustryType, Nothing))
            .AddInputParam("@adbrangename", If(Not String.IsNullOrWhiteSpace(ADBRangeName), ADBRangeName, Nothing))
            .AddInputParam("@roleID", If(Not String.IsNullOrWhiteSpace(RoleID), RoleID, Nothing))
            Return .GetDataTable("AnnualSummaryPerRegion_GetTotalDetails", willCommit)
        End With
    End Function

    Public Function GetCASATypesList(ByVal BranchCode As String,
                       ByVal Year As String,
                       ByVal ClientType As String,
                       ByVal IndustryType As String,
                       ByVal ADBRangeName As String,
                   ByVal RoleID As String) As DataTable
        With Me
            .AddInputParam("@branchcode", If(Not String.IsNullOrWhiteSpace(BranchCode), BranchCode, Nothing))
            .AddInputParam("@year", If(Not String.IsNullOrWhiteSpace(Year), Year, Nothing))
            .AddInputParam("@clienttype", If(Not String.IsNullOrWhiteSpace(ClientType), ClientType, Nothing))
            .AddInputParam("@industrytype", If(Not String.IsNullOrWhiteSpace(IndustryType), IndustryType, Nothing))
            .AddInputParam("@adbrangename", If(Not String.IsNullOrWhiteSpace(ADBRangeName), ADBRangeName, Nothing))
            .AddInputParam("@roleID", If(Not String.IsNullOrWhiteSpace(RoleID), RoleID, Nothing))
            Return .GetDataTable("GetAnnualSummaryListOfAllClients", True)
        End With
    End Function

    Public Function GetLoansList(ByVal BranchCode As String,
                   ByVal Year As String,
                   ByVal ClientType As String,
                   ByVal IndustryType As String,
                   ByVal RoleID As String) As DataTable
        With Me
            .AddInputParam("@branchcode", If(Not String.IsNullOrWhiteSpace(BranchCode), BranchCode, Nothing))
            .AddInputParam("@year", If(Not String.IsNullOrWhiteSpace(Year), Year, Nothing))
            .AddInputParam("@clienttype", If(Not String.IsNullOrWhiteSpace(ClientType), ClientType, Nothing))
            .AddInputParam("@industrytype", If(Not String.IsNullOrWhiteSpace(IndustryType), IndustryType, Nothing))
            .AddInputParam("@roleID", If(Not String.IsNullOrWhiteSpace(RoleID), RoleID, Nothing))
            Return .GetDataTable("GetAnnualSummaryListOfLoans", True)
        End With
    End Function

#End Region
#End Region
End Class
