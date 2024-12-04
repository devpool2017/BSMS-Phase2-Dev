Imports System.Web.Services
Imports System.Web.Script.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities

Public Class PotentialAccountReports
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Potential Account Reports"

        If Not Page.IsPostBack Then
            Page.DataBind()
        End If
    End Sub

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GenerateReport(ByVal obj As PotentialAccountReportsDO) As String
        Dim message As String = String.Empty

        If obj.validateParam() Then
            Dim userobj As New LoginUser
            userobj = HttpContext.Current.Session("currentUser")
            obj.LoggedName = userobj.DisplayName
            obj.UploadedBy = HttpContext.Current.Session("Username")
            obj.BrCode = userobj.BranchCode
            HttpContext.Current.Session("currentParameter") = obj
            HttpContext.Current.Session("ReportName") = "PotentialAccount_Reports"
        Else
            message = obj.errMsg
        End If

        Return message

    End Function

End Class