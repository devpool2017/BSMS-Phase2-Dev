Imports LBP.VS2010.BSMS.DataObjects

Public Class Logout
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' PUT CODE HERE TO LOGOUT USER

        Dim user As LoginUser = HttpContext.Current.Session("currentUser")

        user.logout()

        WriteActivityLog(ActivityLog.ActionTaken.LOGOUT)
        HttpContext.Current.Session.RemoveAll()
        Response.Redirect("/Views/Login/Login.aspx")
    End Sub

End Class