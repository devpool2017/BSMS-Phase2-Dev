Public Class IndustriesDA
    Inherits MasterDAL

#Region "DECLARATION"
    Dim message As String
    ReadOnly Property errorMessage As String
        Get
            Return message
        End Get
    End Property
#End Region


    'Public Function IndustriesList(Optional ByVal willCommit As Boolean = True) As DataTable
    '    With Me
    '        Return .GetDataTable("GetIndustry", willCommit)
    '    End With
    'End Function

    Public Function GetIndustryTypeList() As DataTable
        Dim dt As New DataTable
        Try
            With Me
                dt = .GetDataTable("GetIndustryType", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function

    Public Function CheckIfIndustryExists(ByVal industryCode As String, ByVal industryDesc As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@industrycode", If(Not String.IsNullOrWhiteSpace(industryCode), industryCode, Nothing))
            .AddInputParam("@industrydesc", If(Not String.IsNullOrWhiteSpace(industryDesc), industryDesc, Nothing))
            dt = .GetDataTable("Industry_CheckIfExist", willCommit)
        End With

        Return dt
    End Function

    Public Function GetList(ByVal searchText As String, ByVal filterStatus As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@searchText", If(Not String.IsNullOrWhiteSpace(searchText), searchText, Nothing))
            .AddInputParam("@filterStatus", If(Not String.IsNullOrWhiteSpace(filterStatus), filterStatus, Nothing))
            dt = .GetDataTable("Industry_GetList", willCommit)
        End With

        Return dt
    End Function

    Public Function GetDetails(ByVal industryCode As String) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@industryCode", industryCode)
            dt = .GetDataTable("Industry_GetDetails", False)
        End With

        Return dt
    End Function

    Public Function InsertIndustryTemp(ByVal industryCode As String, ByVal industryDesc As String, ByVal industryType As String,
                                       ByVal status As String, ByVal logonUser As String, ByVal tempStatus As String,
                                       Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@industryCode", industryCode)
            .AddInputParam("@industryDesc", industryDesc)
            .AddInputParam("@industryType", industryType)
            .AddInputParam("@status", status)
            .AddInputParam("@logonUser", logonUser)
            .AddInputParam("@tempStatus", tempStatus)
            Return .ExecNonQuery("IndustryTemp_InsertDetails", willCommit)
        End With
    End Function

    Public Function GetMergeDetails(ByVal industryCode As String) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@IndustryCode", industryCode)
            dt = .GetDataTable("IndustryTemp_GetMergeDetails", False)
        End With

        Return dt
    End Function

    Public Function InsertIndustry(ByVal industryCode As String, ByVal logonUser As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@industryCode", industryCode)
            .AddInputParam("@logonUser", logonUser)
            Return .ExecNonQuery("Industry_Insert", willCommit)
        End With
    End Function

    Public Function UpdateIndustry(ByVal industryCode As String, ByVal logonUser As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@industryCode", industryCode)
            .AddInputParam("@logonUser", logonUser)
            Return .ExecNonQuery("Industry_Update", willCommit)
        End With
    End Function

    Public Function DeleteIndustry(ByVal industryCode As String, ByVal logonUser As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@industryCode", industryCode)
            .AddInputParam("@logonUser", logonUser)
            Return .ExecNonQuery("Industry_Delete", willCommit)
        End With
    End Function

    Public Function DeleteIndustryTemp(ByVal industryCode As String, ByVal logonUser As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@industryCode", industryCode)
            .AddInputParam("@LogonUser", logonUser)
            Return .ExecNonQuery("IndustryTemp_Delete", willCommit)
        End With
    End Function

End Class
