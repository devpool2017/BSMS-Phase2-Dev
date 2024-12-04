Public Class ChangeInPotentialAccountsDAL
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
        MyBase.moduleName = "Changes In Potential Accounts"
    End Sub
#End Region

#Region "FUNCTIONS"
    Public Function GetRegionList() As DataTable
        With Me
            Return .GetDataTable("GetRegions", True)
        End With
    End Function

    Public Function GetUsersList(ByVal groupCode As String, ByVal branchCode As String, ByVal RoleID As String) As DataTable
        With Me
            .AddInputParam("@RegionCode", If(Not String.IsNullOrWhiteSpace(groupCode), groupCode, Nothing))
            .AddInputParam("@BranchCode", If(Not String.IsNullOrWhiteSpace(branchCode), branchCode, Nothing))
            .AddInputParam("@RoleID", If(Not String.IsNullOrWhiteSpace(RoleID), RoleID, Nothing))
            Return .GetDataTable("GetUserByRegionBranch", True)
        End With
    End Function

    Public Function GetBranchesList(ByVal groupCode As String) As DataTable
        With Me
            .AddInputParam("@RegionCode", If(Not String.IsNullOrWhiteSpace(groupCode), groupCode, Nothing))
            Return .GetDataTable("GetBranchUnder", True)
        End With
    End Function

    Public Function GetPotentialAccountsList(ByVal BranchCode As String,
                     ByVal DateFrom As String,
                     ByVal DateToday As String,
                    ByVal Year As String,
                    ByVal Month As String,
                     Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@BranchCode", If(Not String.IsNullOrWhiteSpace(BranchCode), BranchCode, Nothing))
            .AddInputParam("@FromDate", If(Not String.IsNullOrWhiteSpace(DateFrom), DateFrom, Nothing))
            .AddInputParam("@ToDate", If(Not String.IsNullOrWhiteSpace(DateToday), DateToday, Nothing))
            .AddInputParam("@Year", If(Not String.IsNullOrWhiteSpace(Year), Year, Nothing))
            .AddInputParam("@Month", If(Not String.IsNullOrWhiteSpace(Month), Month, Nothing))
            Return .GetDataTable("GetCPAActivityByBMSO", willCommit)
        End With
    End Function

    Public Function GetWeekNum(ByVal DateToday As String, Optional ByVal willCommit As Boolean = True) As DataTable

        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@date", If(Not String.IsNullOrWhiteSpace(DateToday), DateToday, Nothing))
                dt = .GetDataTable("GetWeekNum", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function
#End Region
End Class
