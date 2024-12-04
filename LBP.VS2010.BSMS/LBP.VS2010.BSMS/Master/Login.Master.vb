Imports System.Configuration.ConfigurationManager

Public Class Login2
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            litTitle.Text = GetSection("Commons")("SystemName").ToString
        End If
    End Sub

End Class