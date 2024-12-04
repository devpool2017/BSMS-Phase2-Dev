Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Configuration
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.DataObjects
Imports System.Configuration.ConfigurationManager

Public Class Industries
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.DataBind()
    End Sub

#Region "Load Industries Gridview"
    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetIndustriesList() As List(Of IndustriesDO)
        Dim obj As New IndustriesDO
        Dim List As New List(Of IndustriesDO)
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        List = obj.ListIndustries()
        Return List
    End Function
#End Region

End Class