Imports System.Configuration.ConfigurationManager
Imports LBP.VS2010.BSMS.DataObjects

Public Class Site
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            litTitle.Text = GetSection("Commons")("SystemName").ToString()
            lblSystemName.Text = GetSection("Commons")("SystemName").ToString()

            Dim currentUser As LoginUser = Session("currentUser")

            lblUserFullName.Text = currentUser.DisplayName
            lblRoleName.Text = currentUser.RoleName
            lblDateToday.Text = Format(Now, "MMMM dd, yyyy")
            Dim BM As String = GetSection("Commons")("BM")
            Dim RO As String = GetSection("Commons")("RO")
            Dim GH As String = GetSection("Commons")("GroupHead")
            Dim SH As String = GetSection("Commons")("SectorHead")
            Dim TA As String = GetSection("Commons")("TA")
            Dim AdminMaker As String = GetSection("Commons")("AppAdminMaker")
            Dim AdminApprover As String = GetSection("Commons")("AppAdminApprover")
            Dim ITSOMaker As String = GetSection("Commons")("ITSOMaker")
            Dim ITSOApprover As String = GetSection("Commons")("ITSOApprover")


            If currentUser.RoleID = SH Or currentUser.RoleID = AdminMaker Or currentUser.RoleID = AdminApprover Or currentUser.RoleID = ITSOApprover Or currentUser.RoleID = ITSOMaker Then
                lblBranch.Text = " |"
            ElseIf currentUser.RoleID = GH Or currentUser.RoleID = TA Then
                lblBranch.Text = "| " + currentUser.GroupCode + " - " + currentUser.RegionName + " |"
            ElseIf currentUser.RoleID = BM Then
                lblBranch.Text = "| " + currentUser.Branch + " |"
            End If
            Dim dt As DataTable = Session("systemMenus")

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    rptMenu.DataSource = dt.DefaultView.ToTable(True, {"MainMenuName", "IconSpan"})
                    rptMenu.DataBind()

                    createBreadCrumbs(dt)
                End If
            Else
                Response.Redirect("/Views/ErrorPages/ExpiredSession.aspx")
            End If

        End If



    End Sub


    Protected Sub rptMenu_OnItemBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles rptMenu.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or
           e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim rptSubMenu As Repeater = DirectCast(e.Item.FindControl("rptChildMenu"), Repeater)
            Dim lblMenuName As String = DirectCast(e.Item.FindControl("lblMenuName"), Label).Text

            Dim dt As DataTable = HttpContext.Current.Session("systemMenus")

            Dim q As String = "MainMenuName = '" + lblMenuName + "'"
            Dim dtSelect() As DataRow
            dtSelect = dt.Select(q)

            If dtSelect.Length > 0 Then
                rptSubMenu.DataSource = dtSelect.CopyToDataTable()
                rptSubMenu.DataBind()
            End If
        End If
    End Sub

    ' ROUTINE TO CREATE BREADCRUMBS

    Private Sub createBreadCrumbs(ByVal dt As DataTable)
        Dim pageURL As String = "~" & Request.Url.AbsolutePath

        Dim hasHeader As Boolean = False
        For i As Integer = 0 To dt.Rows.Count - 1
            If Not String.IsNullOrWhiteSpace(dt.Rows(i).Item("URL").ToString) Then
                If pageURL.Contains(GetSection("Commons")("WelcomePagePath").ToString) Then
                    btnBreadCrumbIcon.Text = GetSection("Commons")("HomeIcon").ToString
                    lblBreadCrumb.Text = "Home"
                    siteHeader.CssClass = "lblMainMenuHome"
                    hasHeader = True
                    Exit For
                ElseIf pageURL.Contains(dt.Rows(i).Item("URL").ToString) Then
                    btnBreadCrumbIcon.Text = dt.Rows(i).Item("IconSpan").ToString
                    lblBreadCrumb.Text = dt.Rows(i).Item("MainMenuName").ToString + "   >   " + dt.Rows(i).Item("SubMenuName").ToString
                    'Get URL of selected submenu from sidemenu
                    'Session("URL") = dt.Rows(i).Item("URL").ToString

                    Session("SubMenuID") = dt.Rows(i).Item("SubMenuID").ToString
                    hasHeader = True
                    Exit For
                End If
            End If
        Next

        If Not hasHeader Then
            If Not IsNothing(Session("SubMenuID")) Then
                Dim dr As DataRow() = dt.Select("SubMenuID =" & Session("SubMenuID").ToString)
                If dr.Length > 0 Then
                    btnBreadCrumbIcon.Text = dr(0).Item("IconSpan").ToString
                    lblBreadCrumb.Text = dr(0).Item("MainMenuName").ToString + "   >   " + dr(0).Item("SubMenuName").ToString
                End If
                '-->ITDJTC 20230816
                Dim dtTabs As DataTable = HttpContext.Current.Session("systemMenusTab")
                If Not IsNothing(dtTabs) Then
                    For i As Integer = 0 To dtTabs.Rows.Count - 1
                        If pageURL.Contains(dtTabs.Rows(i).Item("Url").ToString) Then
                            btnBreadCrumbIcon.Text = dtTabs.Rows(i).Item("IconSpan").ToString
                            lblBreadCrumb.Text = dtTabs.Rows(i).Item("MainMenuName").ToString + "   >   " + dtTabs.Rows(i).Item("SubMenuName").ToString
                            Session("SubMenuID") = dtTabs.Rows(i).Item("SubMenuIDTabID").ToString
                            Exit For
                        End If
                    Next
                End If
                '<--
                'Else
                '    Response.Redirect("/Views/Error/NoAccess.aspx")
            End If
        End If
    End Sub
End Class