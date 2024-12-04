Public Class BranchesDA
    Inherits MasterDAL
#Region "DECLARATION"
    Dim message As String
    ReadOnly Property errorMessage As String
        Get
            Return message
        End Get
    End Property
#End Region
#Region "GetDetails"
    Public Function GetRegionList() As DataTable
        With Me
            Return .GetDataTable("Region_GetList", True)
        End With
    End Function


    Public Function GetAllBranches(ByVal RegionCode As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@regionCode", RegionCode)
                dt = .GetDataTable("Branch_GetList", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function ChkRegionCodeExists(ByVal RegionCode As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@regioncode", If(Not String.IsNullOrWhiteSpace(RegionCode), RegionCode, Nothing))
            Return CBool(.ExecScalar("CheckRegionExists", willCommit))
        End With
    End Function

    Public Function ChkBranchCodeExists(ByVal BranchCode As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@branchcode", If(Not String.IsNullOrWhiteSpace(BranchCode), BranchCode, Nothing))
            Return CBool(.ExecScalar("CheckBranchExists", willCommit))
        End With
    End Function

    Public Function ChkBranchNameExists(ByVal BranchName As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@branchname", If(Not String.IsNullOrWhiteSpace(BranchName), BranchName, Nothing))
            Return CBool(.ExecScalar("CheckBranchNameExists", willCommit))
        End With
    End Function

    Public Function AddNewBranch(ByVal RegionCode As String,
                                   ByVal BranchCode As String,
                                   ByVal BranchName As String,
                                   ByVal Username As String) As Boolean
        With Me
            .AddInputParam("@regioncode", If(Not String.IsNullOrWhiteSpace(RegionCode), RegionCode, Nothing))
            .AddInputParam("@branchcode", If(Not String.IsNullOrWhiteSpace(BranchCode), BranchCode, Nothing))
            .AddInputParam("@branchname", If(Not String.IsNullOrWhiteSpace(BranchName), BranchName, Nothing))
            .AddInputParam("@userName", If(Not String.IsNullOrWhiteSpace(Username), Username, Nothing))
            Return .ExecNonQuery("AddBranch", True)
        End With
    End Function

    Public Function UpdateBranch(ByVal RegionCode As String,
                                  ByVal BranchCode As String,
                                  ByVal BranchName As String,
                                  ByVal Username As String) As Boolean
        With Me
            .AddInputParam("@regioncode", If(Not String.IsNullOrWhiteSpace(RegionCode), RegionCode, Nothing))
            .AddInputParam("@branchcode", If(Not String.IsNullOrWhiteSpace(BranchCode), BranchCode, Nothing))
            .AddInputParam("@branchname", If(Not String.IsNullOrWhiteSpace(BranchName), BranchName, Nothing))
            .AddInputParam("@userName", If(Not String.IsNullOrWhiteSpace(Username), Username, Nothing))
            Return .ExecNonQuery("EditBranch", True)
        End With
    End Function

    Public Function DeleteBranch(ByVal RegionCode As String,
                               ByVal BranchCode As String,
                               ByVal BranchName As String) As Boolean
        With Me
            .AddInputParam("@regioncode", If(Not String.IsNullOrWhiteSpace(RegionCode), RegionCode, Nothing))
            .AddInputParam("@branchcode", If(Not String.IsNullOrWhiteSpace(BranchCode), BranchCode, Nothing))
            .AddInputParam("@branchname", If(Not String.IsNullOrWhiteSpace(BranchName), BranchName, Nothing))
            Return .ExecNonQuery("DeleteBranch", True)
        End With
    End Function
#End Region
  
End Class
