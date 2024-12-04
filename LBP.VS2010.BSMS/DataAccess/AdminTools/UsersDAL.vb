Public Class UsersDAL
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
        MyBase.moduleName = "Users"
    End Sub
#End Region
#Region "FUNCTIONS"
#Region "LOGIN"
#Region "GET"
    Public Function GetLogonDetails(ByVal username As String) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@Username", username)
                dt = .GetDataTable("Users_GetDetails", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function GetUserRole(ByVal username As String) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@Username", username)
                dt = .GetDataTable("UsersTemp_GetUserRole", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function GetLoginDetails(ByVal Username As String) As DataTable
        Dim dt As New DataTable

        Try
            With Me
                .AddInputParam("@Username", Username)
                dt = .GetDataTable("Users_GetLoginDetails", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function

    Public Function GetPasswordHistory(ByVal Username As String) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@Username", Username)
                dt = (GetDataTable("PasswordHistory_ListPasswordByUsername", False))
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Function ValidateIfInUse(ByVal Username As String, ByVal Workstation As String) As Boolean
        With Me
            .AddInputParam("@Username", Username)
            .AddInputParam("@Workstation", Workstation)
            Return .ExecScalar("Users_IsInUse", False)
        End With
    End Function

#End Region
#Region "UPDATE"
    Public Function updateLoginAttempt(ByVal Username As String, ByVal LoginAttempt As String, ByVal MaxAttempt As String) As Boolean
        With Me
            .AddInputParam("@Username", Username)
            .AddInputParam("@PasswordAttempt", LoginAttempt)
            .AddInputParam("@MaxPasswordAttempt", MaxAttempt)
            Return .ExecNonQuery("Users_UpdateLoginAttempt", True)
        End With
    End Function

    Public Function updateSuccessfulLogin(ByVal Username As String, ByVal IPAddress As String) As Boolean
        With Me
            .AddInputParam("@Username", Username)
            .AddInputParam("@IPAddress", IPAddress)
            Return .ExecNonQuery("Users_UpdateSuccessfulLogin", True)
        End With
    End Function
    Public Function updateLogout(ByVal Username As String) As Boolean
        With Me
            .AddInputParam("@Username", Username)
            Return .ExecNonQuery("Users_UpdateLogout", True)
        End With
    End Function

    'Public Function UpdatePassword(ByVal Username As String, ByVal HashPassword As String, ByVal mode As Integer) As Boolean
    '    With Me
    '        .AddInputParam("@Username", Username)
    '        .AddInputParam("@Password", HashPassword)
    '        .AddInputParam("@mode", mode)
    '        Return .ExecNonQuery("Users_UpdatePassword", True)
    '    End With
    'End Function
#End Region

    Public Function LoginUser(ByVal username As String, ByVal currIPAddress As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@Username", username)
            .AddInputParam("@IPAddress", currIPAddress)
            Return .ExecNonQuery("Users_Login", willCommit)
        End With
    End Function

    Public Function IncrementLoginAttempt(ByVal username As String, ByVal maxloginAttempt As Integer, Optional ByVal willCommit As Boolean = True) As String
        With Me
            .AddInputParam("@Username", username)
            .AddInputParam("@MaxLoginAttempt", maxloginAttempt)
            Return .ExecScalar("Users_IncrementLoginAttempt", willCommit)
        End With
    End Function

    Public Function LogoutUser(ByVal username As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@Username", username)
            Return .ExecNonQuery("Users_Logout", willCommit)
        End With
    End Function
    Public Function ForceLogoutUser(ByVal username As DataTable, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@Username", username)
            Return .ExecNonQuery("Users_ForceLogout", willCommit)
        End With
    End Function


    Public Function InsertLog(ByVal curUser As String, ByVal desc As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@Username", curUser)
            .AddInputParam("@Description", desc)
            Return .ExecNonQuery("AuditTrail_Insert", willCommit)
        End With
    End Function
#End Region

#Region "MAINTENANCE"
    Public Function RegionGetList(Optional willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("Region_GetList", willCommit)
        End With
    End Function

    Public Function BranchGetList(Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable

        With Me
            '.AddInputParam("@regionCode", regionCode)
            dt = .GetDataTable("Branch_GetList", willCommit)
        End With

        Return dt
    End Function

    Public Function BranchGetListPerRegion(ByVal regionCode As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@regionCode", IIf(regionCode = String.Empty, Nothing, regionCode))
            dt = .GetDataTable("Branch_GetListPerRegion", willCommit)
        End With

        Return dt
    End Function

    Public Function GetList(searchText As String, filterStatus As String, filterRole As String, filterRegion As String, filterBranch As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@searchText", If(Not String.IsNullOrWhiteSpace(searchText), searchText, Nothing))
            .AddInputParam("@filterStatus", If(Not String.IsNullOrWhiteSpace(filterStatus), filterStatus, Nothing))
            .AddInputParam("@filterRole", If(Not String.IsNullOrWhiteSpace(filterRole), filterRole, Nothing))
            .AddInputParam("@filterRegion", If(Not String.IsNullOrWhiteSpace(filterRegion), filterRegion, Nothing))
            .AddInputParam("@filterBranch", If(Not String.IsNullOrWhiteSpace(filterBranch), filterBranch, Nothing))
            dt = .GetDataTable("Users_GetList", willCommit)
        End With

        Return dt
    End Function

    Public Function GetTempList(searchText As String, filterStatus As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@searchText", If(Not String.IsNullOrWhiteSpace(searchText), searchText, Nothing))
            .AddInputParam("@filterStatus", If(Not String.IsNullOrWhiteSpace(filterStatus), filterStatus, Nothing))
            dt = .GetDataTable("UsersTemp_GetList", False)
        End With

        Return dt
    End Function

    Public Function GetDetails(username As String) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@username", username)
            dt = .GetDataTable("Users_GetDetails", False)
        End With

        Return dt
    End Function

    Public Function GetMergeDetails(username As String) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@username", username)
            dt = .GetDataTable("UsersTemp_GetMergeDetails", False)
        End With

        Return dt
    End Function

    Public Function IsUserExists(username As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@username", username)
            Return CBool(.ExecScalar("Users_IsExists", willCommit))
        End With
    End Function

    Public Function IsUserIDExists(userID As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@userID", userID)
            Return CBool(.ExecScalar("User_CheckIDIfExists", willCommit))
        End With
    End Function

    Public Function InsertUserTemp(ByVal username As String, ByVal role As String, ByVal firstName As String, ByVal lastName As String, ByVal middleInitial As String,
                                   ByVal displayName As String, ByVal branchCode As String, ByVal RegionCode As String, ByVal ipAddress As String, ByVal emailAddress As String,
                                   ByVal status As String,
                                   ByVal tempStatus As String, ByVal logonUser As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@username", username)
            .AddInputParam("@role", role)
            .AddInputParam("@firstName", If(Not String.IsNullOrWhiteSpace(firstName), firstName, Nothing))
            .AddInputParam("@lastName", If(Not String.IsNullOrWhiteSpace(lastName), lastName, Nothing))
            .AddInputParam("@middleInitial", If(Not String.IsNullOrWhiteSpace(middleInitial), middleInitial, Nothing))
            .AddInputParam("@branchCode", branchCode)
            .AddInputParam("@regionCode", RegionCode)
            .AddInputParam("@ipAddress", If(Not String.IsNullOrWhiteSpace(ipAddress), ipAddress, Nothing))
            .AddInputParam("@emailAddress", If(Not String.IsNullOrWhiteSpace(emailAddress), emailAddress, Nothing))
            .AddInputParam("@status", status)
            '.AddInputParam("@CASATarget", CASATarget)
            '.AddInputParam("@CBGTarget", CBGTarget)
            .AddInputParam("@tempStatus", tempStatus)
            .AddInputParam("@logonUser", logonUser)
            Return .ExecNonQuery("UsersTemp_InsertDetails", willCommit)
        End With
    End Function

    Public Function InsertUser(username As String, logonUser As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@username", username)
            .AddInputParam("@logonUser", logonUser)
            Return .ExecNonQuery("Users_Insert", willCommit)
        End With
    End Function

    Public Function UpdateUser(username As String, logonUser As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@username", username)
            .AddInputParam("@logonUser", logonUser)
            Return .ExecNonQuery("Users_Update", willCommit)
        End With
    End Function

    Public Function ResetUser(username As String, password As String, logonUser As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@username", username)
            .AddInputParam("@password", password)
            .AddInputParam("@logonUser", logonUser)
            Return .ExecNonQuery("Users_Reset", willCommit)
        End With
    End Function

    Public Function DeleteUser(username As String, logonUser As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@username", username)
            .AddInputParam("@logonUser", logonUser)
            Return .ExecNonQuery("Users_Delete", willCommit)
        End With
    End Function

    Public Function UnlockUser(username As String, logonUser As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@username", username)
            .AddInputParam("@logonUser", logonUser)
            Return .ExecNonQuery("Users_Unlock", willCommit)
        End With
    End Function

    Public Function DeleteUserTemp(username As String, logonUser As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@Username", username)
            .AddInputParam("@LogonUser", logonUser)
            Return .ExecNonQuery("UsersTemp_Delete", willCommit)
        End With
    End Function

    Public Function GetEmail(ByVal Username As String, ByVal TableName As String, Optional ByVal willCommit As Boolean = True) As String
        With Me
            .AddInputParam("@Username", Username)
            .AddInputParam("@TableName", TableName)
            Return .ExecScalar("Users_GetEmail", willCommit)
        End With
    End Function
#End Region
    Public Function GetUsersListReport(ByVal username As String,
                                       ByVal statusID As String,
                                       ByVal roleID As String,
                                       ByVal regionCode As String,
                                       ByVal branchCode As String) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@Username", username)
                .AddInputParam("@StatusID", statusID)
                .AddInputParam("@RoleID", roleID)
                .AddInputParam("@RegionCode", regionCode)
                .AddInputParam("@BranchCode", branchCode)
                dt = .GetDataTable("Users_Report", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function
    '#Region "REPORTS"
    '    Public Function GenerateReport(ByVal Role As String, ByVal Status As String, ByVal DateType As String,
    '                                   ByVal DateFrom As String, ByVal DateTo As String) As DataTable
    '        Dim dt As New DataTable
    '        With Me
    '            .AddInputParam("@Role", IIf(Not String.IsNullOrEmpty(Role), Role, DBNull.Value))
    '            .AddInputParam("@Status", IIf(Not String.IsNullOrEmpty(Status), Status, DBNull.Value))
    '            .AddInputParam("@DateType", IIf(Not String.IsNullOrEmpty(DateType), DateType, DBNull.Value))
    '            .AddInputParam("@DateFrom", IIf(Not String.IsNullOrEmpty(DateFrom), DateFrom, DBNull.Value))
    '            .AddInputParam("@DateTo", IIf(Not String.IsNullOrEmpty(DateTo), DateTo, DBNull.Value))
    '            dt = .GetDataTable("Users_GenerateReport", False)
    '        End With

    '        Return dt
    '    End Function
    '#End Region
#End Region
End Class
