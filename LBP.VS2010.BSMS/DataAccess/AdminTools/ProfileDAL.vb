Public Class ProfileDAL
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
        MyBase.moduleName = "Profile"
    End Sub
#End Region

#Region "NEW FUNCTION"
    Public Function GetProfile(ByVal roleId As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@RoleID", roleId)
            dt = .GetDataTable("Profiles_Get", willCommit)
        End With
        Return dt
    End Function

    Public Function GetProfileTemp(ByVal roleID As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@roleID", roleID)
            dt = .GetDataTable("ProfilesTemp_Get", willCommit)
        End With
        Return dt
    End Function

    'Public Function GetTabProfileAll(ByVal roleId As String, Optional ByVal willCommit As Boolean = True) As DataTable
    '    Dim dt As New DataTable

    '    With Me
    '        .AddInputParam("@roleId", roleId)
    '        dt = .GetDataTable("TabProfilesAll_Get", willCommit)
    '    End With

    '    Return dt
    'End Function

    'Public Function GetTabProfile(ByVal roleId As String, ByVal subMenuId As String, Optional ByVal willCommit As Boolean = True) As DataTable
    '    Dim dt As New DataTable

    '    With Me
    '        .AddInputParam("@roleId", roleId)
    '        .AddInputParam("@subMenuId", subMenuId)
    '        dt = .GetDataTable("TabProfiles_Get", willCommit)
    '    End With

    '    Return dt
    'End Function

    'Public Function GetTabProfileTemp(ByVal roleId As String, ByVal subMenuId As String, Optional ByVal willCommit As Boolean = True) As DataTable
    '    Dim dt As New DataTable

    '    With Me
    '        .AddInputParam("@roleId", roleId)
    '        .AddInputParam("@subMenuId", subMenuId)
    '        dt = .GetDataTable("TabProfilesTemp_Get", willCommit)
    '    End With

    '    Return dt
    'End Function

    Public Function SaveProfileTemp(dt As DataTable, Optional willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@dtProfiles", dt)
            Return .ExecNonQuery("ProfilesTemp_Save", willCommit)
        End With
    End Function

    'Public Function SaveTabProfilesTemp(dt As DataTable, Optional willCommit As Boolean = True) As Boolean
    '    With Me
    '        .AddInputParam("@dtTabProfiles", dt)
    '        Return .ExecNonQuery("TabProfilesTemp_Save", willCommit)
    '    End With
    'End Function
#End Region


















#Region "FUNCTIONS"

#Region "LIST/GET"
    'Public Function ListNewProfile(Optional ByVal willCommit As Boolean = True) As DataTable
    '    Dim dt As New DataTable

    '    Try
    '        With Me
    '            dt = .GetDataTable("Profiles_New", willCommit)
    '        End With
    '    Catch ex As Exception
    '        message = ex.Message
    '    End Try
    '    Return dt
    'End Function


#End Region

#Region "SAVE/INSERT"
    'Public Function SaveProfileTemp(ByVal dtProfile As DataTable, Optional ByVal willCommit As Boolean = True) As Boolean
    '    With Me
    '        .AddInputParam("@dtProfilesTemp", dtProfile)
    '        Return .ExecNonQuery("ProfilesTemp_Insert", willCommit)
    '    End With
    'End Function

    'Public Function SaveProfile(ByVal roleID As String, ByVal dtProfile As DataTable, Optional ByVal willCommit As Boolean = True) As Boolean
    '    With Me
    '        .AddInputParam("@RoleID", roleID)
    '        .AddInputParam("@ProfileList", dtProfile)
    '        Return .ExecNonQuery("Profiles_Insert", willCommit)
    '    End With
    'End Function

    'Public Function InsertProfile(ByVal roleID As String, ByVal roleName As String, Optional ByVal willCommit As Boolean = True) As Boolean
    '    With Me
    '        .AddInputParam("@RoleID", roleID)
    '        .AddInputParam("@RoleName", roleName)
    '        Return .ExecNonQuery("Profiles_Insert", willCommit)
    '    End With
    'End Function
#End Region


    '#Region "DELETE"
    '    Public Function DeleteProfile(ByVal roleID As String, ByVal Approvedby As String, Optional ByVal willCommit As Boolean = True) As Boolean
    '        With Me
    '            .AddInputParam("@RoleID", roleID)
    '            .AddInputParam("@Approvedby", Approvedby)
    '            Return .ExecNonQuery("Profiles_Delete", willCommit)
    '        End With
    '    End Function

    '    Public Function DeleteProfileTemp(ByVal roleID As String, ByVal Approvedby As String, Optional ByVal willCommit As Boolean = True) As Boolean
    '        With Me
    '            .AddInputParam("@RoleID", roleID)
    '            .AddInputParam("@Approvedby", Approvedby)
    '            Return .ExecNonQuery("ProfilesTemp_Delete", willCommit)
    '        End With
    '    End Function
    '#End Region
#End Region
End Class
