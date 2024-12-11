Imports System.Configuration.ConfigurationManager

Public Class ExpiredSession
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        litTitle.Text = GetSection("Commons")("SystemName").ToString() + " - Expired Session"
        litSystem.Text = GetSection("Commons")("SystemName").ToString()
        Session.Clear()
        HttpContext.Current.Session.RemoveAll()
    End Sub

End Class