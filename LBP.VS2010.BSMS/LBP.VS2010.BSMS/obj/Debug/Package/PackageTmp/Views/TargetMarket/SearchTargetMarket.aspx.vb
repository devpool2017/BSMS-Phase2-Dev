Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Configuration
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.DataObjects
Imports System.Configuration.ConfigurationManager

Public Class SearchTargetMarket
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Update Target Market List"

        If Not Page.IsPostBack Then
            HttpContext.Current.Session.Remove("ClientID")
            HttpContext.Current.Session.Remove("ClientDetails")
            HttpContext.Current.Session.Remove("ConversionAccessMatrix")
            HttpContext.Current.Session.Remove("SearchAccessMatrix")
            'HttpContext.Current.Session.Remove("SearchParams")
            Page.DataBind()
        End If
    End Sub

#Region "FOR DROPDOWN"
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function YearList() As List(Of ReferencesForDropdown)
        Dim obj As New SearchTargetMarketDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return obj.YearLists
    End Function

    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function ClientTypesList() As List(Of ReferencesForDropdown)
        Dim obj As New SearchTargetMarketDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return obj.ClientTypesLists
    End Function

    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function MinMaxWeekList(ByVal obj As SearchTargetMarketDO) As List(Of ReferencesForDropdown)
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return obj.WeekMinMaxLists(obj.Year, obj.month)
    End Function

    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function RegionList() As List(Of ReferencesForDropdown)
        Dim obj As New SearchTargetMarketDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return obj.RegionList()
    End Function

    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function BranchList(ByVal region As String) As List(Of ReferencesForDropdown)
        Dim obj As New SearchTargetMarketDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return obj.BranchList(IIf(region = String.Empty, Nothing, region))
    End Function

    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function GetWeekNum() As List(Of ReferencesForDropdown)
        Dim obj As New SearchTargetMarketDO
        Return obj.GetWeekNum()
    End Function
#End Region

#Region "LOAD UPDATE CLIENT LIST"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ListSearchClient(ByVal refobj As SearchTargetMarketDO) As ValuesOnSearch
        Dim newObj As New ValuesOnSearch
        Dim obj As New SearchTargetMarketDO
        Dim ListGrps As New List(Of SearchTargetMarketDO)
        Dim dtFromDate As DataTable
        Dim dtToDate As DataTable
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        obj = refobj
        obj.UploadedBy = currentUser.Username.ToString
        obj.Branch = currentUser.BranchCode.PadLeft(4, "0")

        'HttpContext.Current.Session("SearchParams") = refobj

        If refobj.month = "" Then
            obj.MonthName = "January"
            obj.WeekNum = "1"
            dtFromDate = obj.GetFromDate(obj)
            obj.FromDate = dtFromDate(0)("FromDate").ToString
            obj.MonthName = "December"
            obj.WeekNum = "4"
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
                obj.WeekNum = "1"
                dtFromDate = obj.GetFromDate(obj)
                obj.FromDate = dtFromDate(0)("FromDate").ToString
                obj.WeekNum = "4"
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

        ListGrps = obj.ListSearchClients(obj)
        Dim otherParams As OtherParametersDO = OtherParametersDO.GetOtherParameters().Item(0)
        Dim searchCount As Integer = ListGrps.Count
        Dim maxSearchCount As Integer = Integer.Parse(otherParams.MaximumSearchCount)

        Return New ValuesOnSearch With {
        .SearchClientList = ListGrps,
        .message = obj.GetMessage(searchCount, maxSearchCount),
        .searchCount = searchCount,
        .maxSearchCount = maxSearchCount
        }
    End Function
#End Region


#Region "Detail Session"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SaveDetailsSession(ByVal clientID As String) As String
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim access As ProfileDO = GetModuleAccess()
        Dim message As String = String.Empty

        If Not clientID.Equals("") Then
            HttpContext.Current.Session("ClientID") = Trim(clientID)
            HttpContext.Current.Session("SearchAccessMatrix") = access
        Else
            message = "No Details"
        End If

        Return message
    End Function

#End Region

    Class ValuesOnSearch
        Property SearchClientList As List(Of SearchTargetMarketDO)
        Property message As String
        Property searchCount As Integer
        Property maxSearchCount As Integer
    End Class

End Class