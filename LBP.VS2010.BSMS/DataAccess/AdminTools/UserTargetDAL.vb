Public Class UserTargetDAL
    Inherits MasterDAL

    Public Function GetList(regionCode As String, brCode As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@regionCode", If(Not String.IsNullOrWhiteSpace(regionCode), regionCode, Nothing))
            .AddInputParam("@brCode", If(Not String.IsNullOrWhiteSpace(brCode), brCode, Nothing))
            dt = .GetDataTable("UserTarget_GetList", willCommit)
        End With

        Return dt
    End Function

    Public Function GetTempList(regionCode As String, brCode As String, filterStatus As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@regionCode", If(Not String.IsNullOrWhiteSpace(regionCode), regionCode, Nothing))
            .AddInputParam("@brCode", If(Not String.IsNullOrWhiteSpace(brCode), brCode, Nothing))
            .AddInputParam("@filterStatus", If(Not String.IsNullOrWhiteSpace(filterStatus), filterStatus, Nothing))
            dt = .GetDataTable("UserTargetTemp_GetList", False)
        End With

        Return dt
    End Function
    Public Function GetDetails(username As String) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@username", username)
            dt = .GetDataTable("UserTarget_GetDetails", False)
        End With

        Return dt
    End Function

    Public Function GetBHPerBrCode(regionCode As String, brCode As String, role As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@regionCode", If(Not String.IsNullOrWhiteSpace(regionCode), regionCode, Nothing))
            .AddInputParam("@brCode", If(Not String.IsNullOrWhiteSpace(brCode), brCode, Nothing))
            .AddInputParam("@role", role, Nothing)
            dt = .GetDataTable("Users_GetAtiveBHPerGroup", willCommit)
        End With

        Return dt
    End Function


    Public Function InsertUserTargetTemp(ByVal username As String, ByVal branchCode As String, ByVal RegionCode As String,
                                   ByVal status As String, ByVal casaTarget As String, ByVal loansTarget As String,
                                   ByVal tempStatus As String, ByVal logonUser As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@username", username)
            .AddInputParam("@branchCode", branchCode)
            .AddInputParam("@regionCode", RegionCode)
            .AddInputParam("@status", status)
            .AddInputParam("@CASATarget", casaTarget)
            .AddInputParam("@LoansTarget", loansTarget)
            .AddInputParam("@tempStatus", tempStatus)
            .AddInputParam("@logonUser", logonUser)
            Return .ExecNonQuery("UserTargetTemp_InsertDetails", willCommit)
        End With
    End Function

    Public Function GetMergeDetails(username As String) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@username", username)
            dt = .GetDataTable("UserTargetTemp_GetMergeDetails", False)
        End With

        Return dt
    End Function
    Public Function InsertUserTarget(username As String, logonUser As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@username", username)
            .AddInputParam("@logonUser", logonUser)
            Return .ExecNonQuery("UserTarget_Insert", willCommit)
        End With
    End Function

    Public Function UpdateUserTarget(username As String, logonUser As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@username", username)
            .AddInputParam("@logonUser", logonUser)
            Return .ExecNonQuery("UserTarget_Update", willCommit)
        End With
    End Function


    Public Function DeleteUserTargetTemp(username As String, logonUser As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@Username", username)
            .AddInputParam("@LogonUser", logonUser)
            Return .ExecNonQuery("UserTargetTemp_Delete", willCommit)
        End With
    End Function

    Public Function IsUserTargetExists(username As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@username", username)
            Return CBool(.ExecScalar("UserTarget_IsExists", willCommit))
        End With
    End Function
End Class
