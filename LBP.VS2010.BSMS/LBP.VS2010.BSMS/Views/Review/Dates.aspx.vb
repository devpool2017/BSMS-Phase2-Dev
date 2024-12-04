Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Configuration
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.DataObjects
Imports System.Configuration.ConfigurationManager

Public Class Dates
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.DataBind()
    End Sub


#Region "FOR DROPDOWN"
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function DatesList() As List(Of ReferencesForDropdown)
        Dim obj As New DatesDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return obj.DatesLists
    End Function

    <WebMethod(EnableSession:=True)> _
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function YearList() As List(Of ReferencesForDropdown)
        Dim obj As New DatesDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return obj.YearList
    End Function

    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function YearMonthWeekListAll() As List(Of DatesDO)
        Dim obj As New DatesDO
        Dim List As New List(Of DatesDO)
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        List = obj.YearMonthWeekListAll()
        'If obj.list.Count = 0 Then
        '    obj.errorMessage = "No record found!"
        'End If
        Return List
    End Function

    <WebMethod(EnableSession:=True)> _
 <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function YearMonthWeekListByMonth(ByVal refobj As DatesDO) As List(Of DatesDO)
        Dim obj As New DatesDO
        Dim List As New List(Of DatesDO)
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")

        List = obj.WeekList(refobj.Year, refobj.Month, refobj.Week)

        'If obj.list.Count = 0 Then
        '    obj.errorMessage = "No record found!"
        'End If

        Return List
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function Validatefields(ByVal refobj As DatesDO) As String
        Dim obj As New DatesDO
        Dim message As String = String.Empty

        obj.Year = refobj.Year
        obj.Month = refobj.Month
        obj.Week = refobj.Week

        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        If Not obj.ValidateField() Then
            obj.errorMessage = obj.errMsg
        End If
        Return obj.errorMessage
    End Function
#End Region

#Region "Load All Dates"
    <WebMethod(EnableSession:=True)>
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function AllDatesList() As List(Of DatesDO)
        Dim obj As New DatesDO
        Dim List As New List(Of DatesDO)
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        List = obj.YearMonthWeekListAll()
        Return List
    End Function

    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function AllDatesListByYear(ByVal refobj As DatesDO) As List(Of DatesDO)
        Dim obj As New DatesDO
        Dim list As New List(Of DatesDO)
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        list = obj.WeekList(refobj.Year, refobj.Month, refobj.Week)
        Return list
    End Function
#End Region

#Region "Save"
    <WebMethod(EnableSession:=True)>
 <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function CheckIfExistSave(ByVal refobj As DatesDO) As String
        Dim obj As New DatesDO
        Dim message As String = String.Empty
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        obj.UserID = currentUser.Username
        If Not obj.CheckIfExist(refobj.Year, refobj.Month, refobj.Week) Then
            obj.errorMessage = obj.errMsg
        Else
            If Not obj.SaveDateInsert(refobj.Year, refobj.Month, refobj.Week, refobj.FromDate, refobj.ToDate, obj.UserID, "") Then
                obj.errorMessage = obj.errMsg
            End If
        End If
        Return obj.errorMessage
    End Function
#End Region
End Class