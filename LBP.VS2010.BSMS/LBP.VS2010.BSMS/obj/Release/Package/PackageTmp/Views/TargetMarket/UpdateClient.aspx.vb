Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Configuration
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.DataObjects
Imports System.Configuration.ConfigurationManager

Public Class UpdateClient
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Update Target Market List"

        If Not Page.IsPostBack Then
            HttpContext.Current.Session.Remove("ClientID")
            HttpContext.Current.Session.Remove("ClientDetails")
            HttpContext.Current.Session.Remove("ConversionAccessMatrix")
            Page.DataBind()
        End If
    End Sub

#Region "FOR DROPDOWN"
    <WebMethod(EnableSession:=True)> _
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function YearList() As List(Of ReferencesForDropdown)
        Dim obj As New UpdateClientDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return obj.YearLists
    End Function

    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function ClientTypesList() As List(Of ReferencesForDropdown)
        Dim obj As New UpdateClientDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return obj.ClientTypesLists
    End Function

    <WebMethod(EnableSession:=True)> _
  <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function MinMaxWeekList(ByVal obj As UpdateClientDO) As List(Of ReferencesForDropdown)
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return obj.WeekMinMaxLists(obj.Year, obj.month)
    End Function

    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function GetWeekNum() As List(Of ReferencesForDropdown)
        Dim obj As New UpdateClientDO
        Return obj.GetWeekNum()
    End Function
#End Region

#Region "LOAD UPDATE CLIENT LIST"
    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ListUpdateClient(ByVal refobj As UpdateClientDO) As List(Of UpdateClientDO)
        Dim obj As New UpdateClientDO
        Dim ListGrps As New List(Of UpdateClientDO)
        Dim dtFromDate As DataTable
        Dim dtToDate As DataTable
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        obj = refobj
        obj.UploadedBy = currentUser.Username.ToString
        obj.Branch = currentUser.BranchCode.PadLeft(4, "0")

        If refobj.month = "" Then
            obj.MonthName = "January"
            obj.WeekNum = obj.GetMinWeekNum(obj)
            dtFromDate = obj.GetFromDate(obj)
            obj.FromDate = dtFromDate(0)("FromDate").ToString
            obj.MonthName = "December"
            obj.WeekNum = obj.GetMaxWeekNum(obj)
            dtToDate = obj.GetToDate(obj)
            obj.Todate = dtToDate(0)("ToDate").ToString

        ElseIf refobj.Year = "" Then

            refobj.Year = Year(Now)
            obj.MonthName = refobj.month
            obj.WeekNum = refobj.WeekNum
            dtFromDate = obj.GetFromDate(obj)
            obj.FromDate = dtFromDate(0)("FromDate").ToString
            dtToDate = obj.GetToDate(obj)
            obj.Todate = dtToDate(0)("ToDate").ToString

        Else
            obj.MonthName = refobj.month
            If refobj.WeekNum = "" Then
                obj.WeekNum = obj.GetMinWeekNum(obj)
                dtFromDate = obj.GetFromDate(obj)
                obj.FromDate = dtFromDate(0)("FromDate").ToString
                obj.WeekNum = obj.GetMaxWeekNum(obj)
                dtToDate = obj.GetToDate(obj)
                obj.Todate = dtToDate(0)("ToDate").ToString
            Else
                obj.WeekNum = refobj.WeekNum
                dtFromDate = obj.GetFromDate(obj)
                obj.FromDate = dtFromDate(0)("FromDate").ToString
                dtToDate = obj.GetToDate(obj)
                obj.Todate = dtToDate(0)("ToDate").ToString
            End If
        End If

        obj.Status1B = obj.GetStatusB(obj.Status1)
        obj.Status2B = obj.GetStatusB(obj.Status2)
        obj.Status3B = obj.GetStatusB(obj.Status3)

        ListGrps = obj.ListUpdateClients(obj)

        Return ListGrps
    End Function
#End Region


#Region "Detail Session"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SaveDetailsSession(ByVal clientID As String) As String
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim message As String = String.Empty

        If Not clientID.Equals("") Then
            HttpContext.Current.Session("ClientID") = Trim(clientID)
        Else
            message = "No Details"
        End If

        Return message
    End Function

#End Region
End Class