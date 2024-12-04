Imports System.Web.Services
Imports System.Web.Script.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities
Imports System.Configuration.ConfigurationManager

Public Class OtherTables
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Other Tables"

        If Not Page.IsPostBack Then
            Page.DataBind()
        End If
    End Sub

#Region "LOAD CONTENT"

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function checkDDLValue(ByVal tableName As String) As List(Of OtherTablesDO)
        Dim List As New List(Of OtherTablesDO)
        List = OtherTablesDO.checkDDLValue(tableName)
        Return List
    End Function


#End Region

    
End Class