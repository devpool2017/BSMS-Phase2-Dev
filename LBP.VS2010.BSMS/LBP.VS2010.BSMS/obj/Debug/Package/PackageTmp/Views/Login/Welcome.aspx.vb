Imports System.Configuration.ConfigurationManager
Imports System.Web.Script.Services
Imports System.Web.Services
Imports LBP.VS2010.BSMS.DataObjects

Public Class Welcome
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Welcome"

        If Not Page.IsPostBack Then
            Dim obj As LoginUser = Session("currentUser")

            lblDisplayName.Text = obj.DisplayName
            pWelcomeMsg.InnerText = "Welcome to " + GetSection("Commons")("SystemName").ToString() + " " + GetSection("Commons")("SystemCode").ToString()
        End If
    End Sub

#Region "LOAD CONTENT"
    '<WebMethod(EnableSession:=True)>
    '<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    'Public Shared Function GetSummary() As TaskSummaryDO
    '    Return TaskSummaryDO.GetSummary()
    'End Function
#End Region

End Class