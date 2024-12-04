Public Class RoleDAL
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
        moduleName = "Role"
    End Sub
#End Region

#Region "NEW FUNCTION"

#Region "MAIN"
    Public Function GetDetails(ByVal roleId As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@roleId", roleId)
            dt = .GetDataTable("Roles_GetDetails", willCommit)
        End With

        Return dt
    End Function


    Public Function GetRoleStatus(ByVal roleId As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@roleId", roleId)
            dt = .GetDataTable("Roles_Get", willCommit)
        End With

        Return dt
    End Function

    Public Function GetList(searchText As String, filterStat As String, Optional willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@searchText", If(Not String.IsNullOrWhiteSpace(searchText), searchText, Nothing))
            .AddInputParam("@filterStat", If(Not String.IsNullOrWhiteSpace(filterStat), filterStat, Nothing))
            Return .GetDataTable("Roles_GetList", willCommit)
        End With
    End Function
    Public Function GetUserRoleList(searchText As String, Optional willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@searchText", If(Not String.IsNullOrWhiteSpace(searchText), searchText, Nothing))
            Return .GetDataTable("UserRoles_GetList", willCommit)
        End With
    End Function

    Public Function IsNameExists(ByVal roleName As String, Optional willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@RoleName", roleName)
            Return CBool(.ExecScalar("Roles_IsNameExists", willCommit))
        End With
    End Function

    Public Function InsertRole(roleTempId As String, logonUser As String, Optional willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@roleTempId", roleTempId)
            .AddInputParam("@logonUser", logonUser)
            Return .ExecNonQuery("RolesProfiles_Insert", willCommit)
        End With
    End Function

    Public Function UpdateRole(roleId As String, roleTempId As String, logonUser As String, Optional willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@roleId", roleId)
            .AddInputParam("@roleTempId", roleTempId)
            .AddInputParam("@logonUser", logonUser)
            Return .ExecNonQuery("RolesProfiles_Update", willCommit)
        End With
    End Function

    Public Function DeactivateRole(roleId As String, stat As String, logonUser As String, Optional willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@roleId", roleId)
            .AddInputParam("@stat", stat)
            .AddInputParam("@logonUser", logonUser)
            Return .ExecNonQuery("RolesProfiles_Deactivate", willCommit)
        End With
    End Function
#End Region

#Region "TEMP"
    Public Function GetTempDetails(ByVal roleId As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@roleId", roleId)
            dt = .GetDataTable("RolesTemp_Get", willCommit)
        End With

        Return dt
    End Function

    Public Function GetTempList(searchText As String, filterStatus As String, Optional willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@searchText", If(Not String.IsNullOrWhiteSpace(searchText), searchText, Nothing))
            .AddInputParam("@filterStatus", If(Not String.IsNullOrWhiteSpace(filterStatus), filterStatus, Nothing))

            Return .GetDataTable("RolesTemp_GetList", willCommit)
        End With
    End Function

    Public Function SaveTemp(roleId As String, roleName As String, roleDescription As String, status As String, tempStatus As String,
                                 dtProfiles As DataTable, logonUser As String, Optional willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@roleId", If(Not String.IsNullOrWhiteSpace(roleId), roleId, Nothing))
            .AddInputParam("@roleName", roleName)
            .AddInputParam("@roleDescription", roleDescription)
            .AddInputParam("@status", status)
            .AddInputParam("@tempStatus", tempStatus)
            .AddInputParam("@logonUser", logonUser)
            .AddInputParam("@dtProfiles", dtProfiles)
            '.AddInputParam("@dtTabprofiles", dtTabprofiles)
            Return .ExecNonQuery("RolesProfilesTemp_Insert", willCommit)
        End With
    End Function

    Public Function DeleteTemp(roleTempId As String, logonUser As String, Optional willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@roleTempId", roleTempId)
            .AddInputParam("@logonUser", logonUser)
            Return .ExecNonQuery("RolesProfilesTemp_Delete", willCommit)
        End With
    End Function
#End Region
#End Region



















    '#Region "FUNCTIONS"

    '#Region "VALIDATION"
    '    Public Function IsRoleInUsed(ByVal roleID As String) As Boolean
    '        Dim inUsed As Boolean = False

    '        Try
    '            With Me
    '                .AddInputParam("@RoleID", roleID)
    '                inUsed = CBool(.ExecScalar("Users_IsRoleInUsed", True))
    '            End With
    '        Catch ex As Exception
    '            message = ex.Message
    '        End Try

    '        Return inUsed
    '    End Function



    '    Public Function IsRoleForApproval(ByVal roleID As String, ByVal roleName As String) As Boolean
    '        Dim isExists As Boolean = False

    '        Try
    '            With Me
    '                .AddInputParam("@RoleID", If(String.IsNullOrWhiteSpace(roleID), DBNull.Value, roleID))
    '                .AddInputParam("@RoleName", roleName)
    '                isExists = CBool(.ExecScalar("RolesTemp_IsExists", True))
    '            End With
    '        Catch ex As Exception
    '            message = ex.Message
    '        End Try

    '        Return isExists
    '    End Function
    '#End Region

    '#Region "LIST/GET"
    '    Public Function List() As DataTable
    '        Dim dt As New DataTable

    '        Try
    '            With Me
    '                dt = .GetDataTable("Roles_List", True)
    '            End With
    '        Catch ex As Exception
    '            message = ex.Message
    '        End Try
    '        Return dt
    '    End Function

    '    Public Function Search(ByVal RoleName As String) As DataTable
    '        Dim dt As New DataTable

    '        Try
    '            With Me
    '                .AddInputParam("@RoleName", RoleName)
    '                dt = .GetDataTable("Roles_Search", True)
    '            End With
    '        Catch ex As Exception
    '            message = ex.Message
    '        End Try
    '        Return dt
    '    End Function

    '    Public Function SearchForApproval(ByVal RoleName As String) As DataTable
    '        Dim dt As New DataTable

    '        Try
    '            With Me
    '                .AddInputParam("@RoleName", RoleName)
    '                dt = .GetDataTable("RolesTemp_Search", True)
    '            End With
    '        Catch ex As Exception
    '            message = ex.Message
    '        End Try
    '        Return dt
    '    End Function

    '    Public Function GetRole(ByVal roleID As String, Optional ByVal willCommit As Boolean = True) As DataTable
    '        Dim dt As New DataTable

    '        Try
    '            With Me
    '                .AddInputParam("@RoleID", roleID)
    '                dt = .GetDataTable("Roles_Get", willCommit)
    '            End With
    '        Catch ex As Exception
    '            message = ex.Message
    '        End Try
    '        Return dt
    '    End Function

    '    Public Function GetRoleTemp(ByVal roleName As String, Optional ByVal willCommit As Boolean = True) As DataTable
    '        Dim dt As New DataTable

    '        Try
    '            With Me
    '                .AddInputParam("@RoleName", roleName)
    '                dt = .GetDataTable("RolesTemp_Get", willCommit)
    '            End With
    '        Catch ex As Exception
    '            message = ex.Message
    '        End Try
    '        Return dt
    '    End Function

    '#End Region

    '#Region "SAVE/INSERT"
    '    Public Function SaveRoleTemp(ByVal dtRole As DataTable, Optional ByVal willCommit As Boolean = True) As Boolean
    '        With Me
    '            .AddInputParam("@RoleList", dtRole)
    '            Return .ExecNonQuery("RolesTemp_Insert", willCommit)
    '        End With
    '    End Function

    '    Public Function InsertRole(ByVal roleName As String, ByVal approvedBy As String, Optional ByVal willCommit As Boolean = True) As String
    '        With Me
    '            .AddInputParam("@RoleName", roleName)
    '            .AddInputParam("@ApprovedBy", approvedBy)
    '            Return .ExecScalar("Roles_Insert", willCommit)
    '        End With
    '    End Function
    '#End Region


    '#Region "UPDATE"
    '    Public Function UpdateRole(ByVal roleID As String, ByVal approvedBy As String, Optional ByVal willCommit As Boolean = True) As Boolean
    '        With Me
    '            .AddInputParam("@RoleID", roleID)
    '            .AddInputParam("@ApprovedBy", approvedBy)
    '            Return .ExecNonQuery("Roles_Update", willCommit)
    '        End With
    '    End Function
    '#End Region


    '#Region "DELETE"
    '    Public Function DeleteRole(ByVal roleID As String, Optional ByVal willCommit As Boolean = True) As Boolean
    '        With Me
    '            .AddInputParam("@RoleID", roleID)
    '            Return .ExecNonQuery("Roles_Delete", willCommit)
    '        End With
    '    End Function

    '    Public Function DeleteRoleTemp(ByVal roleName As String, Optional ByVal willCommit As Boolean = True) As Boolean
    '        With Me
    '            .AddInputParam("@RoleName", roleName)
    '            Return .ExecNonQuery("RolesTemp_Delete", willCommit)
    '        End With
    '    End Function
    '#End Region

    '#End Region
End Class
