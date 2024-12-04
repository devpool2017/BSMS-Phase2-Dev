Public Class SummaryBMDA
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
        moduleName = "Summary BM"
    End Sub
#End Region


    Public Function RegionList(ByVal role As String, ByVal region As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                dt = .GetDataTable("SummaryConversion_GetRegionList", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function GetListSummaryBM(ByVal role As String, ByVal region As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@Position", role)
                .AddInputParam("@Reg1", region)
                dt = .GetDataTable("GetBMSO", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function GetListSummaryPerBM(ByVal Lead As String, ByVal Prospect As String, ByVal Customer As String, ByVal UploadedBy As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@IsLead", IIf(Lead = "", DBNull.Value, Lead))
                .AddInputParam("@IsProspect", IIf(Prospect = "", DBNull.Value, Prospect))
                .AddInputParam("@IsCustomer", IIf(Customer = "", DBNull.Value, Customer))
                .AddInputParam("@UploadedBy", UploadedBy)
                dt = .GetDataTable("GetCPAByBMSO", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function GetSummarySearch(ByVal RoleID As String, ByVal Branch As String, ByVal RegionCode As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@region", RegionCode)
                .AddInputParam("@name", "")
                .AddInputParam("@branch", Branch)
                .AddInputParam("@pos", RoleID)
                dt = .GetDataTable("BBGRHFilteredBMSOSearch", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function DeleteCPA(ByVal CPAID As String, ByVal userID As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@CPAID", CPAID)
            .AddInputParam("@userID", userID)
            Return .ExecNonQuery("DeleteCPA", willCommit)
        End With
    End Function

    Public Function BranchListPerRegion(ByVal role As String, ByVal region As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@Position", role)
                .AddInputParam("@Reg1", region)
                dt = .GetDataTable("GetBMSO", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt

    End Function


End Class
