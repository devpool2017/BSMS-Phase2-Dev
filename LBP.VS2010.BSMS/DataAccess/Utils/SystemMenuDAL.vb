Public Class SystemMenuDAL
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
        MyBase.moduleName = "System Menu"
    End Sub
#End Region
#Region "FUNCTIONS"
    Public Function ListSystemMenu() As DataTable
        Dim dt As New DataTable

        Try
            With Me
                dt = .GetDataTable("SystemMenu_List", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt

        'dr1("SubMenuId") = "1"
        'dr1("SubMenuName") = "User"
        'dr1("MainMenuName") = "Maintenance"
        'dr1("Url") = "~/yeye"
        'dr1("IconSpan") = "<span class='fa fa-cogs'></span>"

        'dt.Rows.Add(dr1)

        'dt.Rows.Add(dr2)

        'dr3("SubMenuId") = "3"
        'dr3("SubMenuName") = "Report1"
        'dr3("MainMenuName") = "Reports"
        'dr3("Url") = "~/yeye"
        'dr3("IconSpan") = "<span class='fa fa-clipboard-list'></span>"

        'dt.Rows.Add(dr3)

        'dr4("SubMenuId") = "4"
        'dr4("SubMenuName") = "Report2"
        'dr4("MainMenuName") = "Reports"
        'dr4("Url") = "~/yeye"
        'dr4("IconSpan") = "<span class='fa fa-clipboard-list'></span>"

        'dt.Rows.Add(dr4)

        'dr5("SubMenuId") = "5"
        'dr5("SubMenuName") = "Sample CRUD"
        'dr5("MainMenuName") = "Sample CRUD"
        'dr5("Url") = "/Views/SampleCRUD/SampleCRUD.aspx"
        'dr5("IconSpan") = "<span class='fa fa-code'></span>"

        'dt.Rows.Add(dr5)

        'Return dt

    End Function
    Public Function ListSystemMenuByRole(ByVal RoleID As String) As DataTable
        Dim dt As New DataTable

        Try
            With Me
                .AddInputParam("@RoleID", RoleID)
                dt = .GetDataTable("SystemMenu_ListByRoleID", True)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function
    Public Function ListSystemMenuTabByRole(ByVal RoleID As String) As DataTable
        Dim dt As New DataTable

        Try
            With Me
                .AddInputParam("@RoleID", RoleID)
                dt = .GetDataTable("SystemMenuTab_ListByRoleID", True)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function
#End Region
End Class
