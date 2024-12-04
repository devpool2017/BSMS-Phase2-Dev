Public Class BranchManagerDA
    Inherits MasterDAL
#Region "DECLARATION"
    Dim message As String
    ReadOnly Property errorMessage As String
        Get
            Return message
        End Get
    End Property
#End Region

    Public Function GetRegionList(Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("GetRegionInfo", willCommit)
        End With
    End Function

    Public Function GetBranchPerRegionLists(ByVal RegionCode As String, Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@UserRegion", RegionCode)
            Return .GetDataTable("GetBranchInfo", willCommit)
        End With
    End Function

    Public Function BranchManagerList(Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("GetBMSO", willCommit)
        End With
    End Function

    Public Function GetGroupHeadPerRegion(ByVal RegionCode As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@RegionCode", RegionCode)
                dt = .GetDataTable("Users_GetDetailperRegion", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function GetListRegionBranchManager(ByVal role As String, ByVal region As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@Position", IIf(role = "", DBNull.Value, role))
                .AddInputParam("@Reg1", IIf(region = "", DBNull.Value, region))
                dt = .GetDataTable("GetBMSO", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function


    Public Function GetBMRegionBranch(ByVal role As String, ByVal region As String, ByVal branch As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@Position", IIf(role = "", DBNull.Value, role))
                .AddInputParam("@Reg1", IIf(region = "", DBNull.Value, region))
                .AddInputParam("@branchCode", IIf(branch = "", DBNull.Value, branch))
                dt = .GetDataTable("GetBMRegionBranch", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function
End Class
