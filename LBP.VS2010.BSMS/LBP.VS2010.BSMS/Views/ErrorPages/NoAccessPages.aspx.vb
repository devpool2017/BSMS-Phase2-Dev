Imports System.Configuration.ConfigurationManager
Imports LBP.VS2010.BSMS.Utilities

Public Class NoAccessPages
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        litTitle.Text = GetSection("Commons")("SystemName").ToString() + " - Expired Session"
        litSystem.Text = GetSection("Commons")("SystemName").ToString()

        Dim url As String
        url = HttpContext.Current.Session("previousURL")
        Dim IPAddress As String = GetIPAddress()
        Dim errMsg As String
        errMsg = "No access for user: " + HttpContext.Current.Session("Username") + " with IP Address: " + IPAddress
        Logger.writeLog(url, "Invalid Access", "", errMsg)
        Session.Clear()

    End Sub
    Private Shared Function GetIPAddress() As String
        Dim sIPAddress As String = HttpContext.Current.Request.ServerVariables("HTTP_X_FORWARDED_FOR")
        Dim ipAddress As String = String.Empty
        If String.IsNullOrEmpty(sIPAddress) Then
            ipAddress = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR")
        End If
        Return If(Not IsNothing(sIPAddress), sIPAddress, ipAddress)
    End Function


End Class