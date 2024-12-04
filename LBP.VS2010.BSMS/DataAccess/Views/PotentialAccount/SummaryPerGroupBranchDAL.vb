Public Class SummaryPerGroupBranchDAL
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
        moduleName = "Summary Per Group/Branch"
    End Sub
#End Region

#Region "GET"
    Public Function GetIndustryList() As DataTable
        With Me
            Return .GetDataTable("GetIndustry", True)
        End With
    End Function

    Public Function GetGroupList() As DataTable
        With Me
            Return .GetDataTable("GetRegions", True)
        End With
    End Function

    Public Function GetBranchesList(ByVal groupCode As String,
                                    Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@SelectedRegion", If(Not String.IsNullOrWhiteSpace(groupCode), groupCode, Nothing))
            Return .GetDataTable("FilteredGetBranchesWithRegionName", willCommit)
        End With
    End Function

    Public Function GetSummaryCountPerBranch(ByVal BranchCode As String,
                                             ByVal Year As String,
                                             ByVal IndustryTypeDesc As String,
                                             ByVal RoleID As String,
                                             Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@branchcode", BranchCode)
            .AddInputParam("@year", Year)
            .AddInputParam("@industrytype", If(IndustryTypeDesc = "All", "", IndustryTypeDesc))
            .AddInputParam("@position", RoleID)
            .AddInputParam("@tag", "")
            Return .GetDataTable("GetCPASummaryCountPerBranch", willCommit)
        End With
    End Function

    Public Function GetSummaryCountPerRegion(ByVal RegionCode As String,
                                             ByVal Year As String,
                                             ByVal IndustryTypeDesc As String,
                                             ByVal RoleID As String,
                                             Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@regioncode", RegionCode)
            .AddInputParam("@year", Year)
            .AddInputParam("@industrytype", If(IndustryTypeDesc = "All", "", IndustryTypeDesc))
            .AddInputParam("@position", RoleID)
            .AddInputParam("@tag", "")
            Return .GetDataTable("GetCPASummaryCountPerRegion", willCommit)
        End With
    End Function

#End Region

End Class
