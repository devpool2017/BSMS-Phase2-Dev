Imports System.Web.Script.Services
Imports System.Web.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities
Imports System.Web.HttpContext

Public Class WeeklyActivityView
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Weekly Activity"
        If Not Page.IsPostBack Then
            Page.DataBind()
        End If
    End Sub

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function OnLoadData() As FiltersOnLoad
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim username As String = IIf(currentUser.RoleID = HttpContext.Current.GetSection("Commons")("BM").ToString(), currentUser.Username, String.Empty)
        Dim groupCode As String = If({Current.GetSection("Commons")("GroupHead").ToString(), Current.GetSection("Commons")("TA").ToString(), Current.GetSection("Commons")("RO").ToString()}.Contains(currentUser.RoleID), currentUser.GroupCode, String.Empty)
        Dim obj As WeeklyActivityDO = Current.Session("WeeklyActivityParams")

        Return New FiltersOnLoad With {
            .currentUser = currentUser,
            .YearList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetYear(), "Year", "Year"),
            .MonthList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetMonth(), "MonthCode", "Month"),
            .WeekList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetWeek(obj.YearNumber, MonthName(obj.MonthCode)), "WeekValue", "WeekDescription"),
            .ClientDetails = GetDate(obj)
        }
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateSearchFilter(ByVal obj As WeeklyActivityDO) As String
        Dim msg As String = String.Empty
        WriteActivityLog(ActivityLog.ActionTaken.SEARCH)
        If Not obj.isValid Then
            msg = obj.errMsg
        End If
        Return msg
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetWeek(ByVal year As String, ByVal month As String) As List(Of ReferencesForDropdown)
        Return ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetWeek(year, month), "WeekValue", "WeekDescription")
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetDate(ByVal obj As WeeklyActivityDO) As WeeklyActivityDO
        Dim objDate As DateMaintenanceDO = DateMaintenanceDO.GetDate(obj.YearNumber, MonthName(obj.MonthCode), obj.WeekNumber)
        obj.DateFrom = objDate.FromDate
        obj.DateTo = objDate.ToDate

        HttpContext.Current.Session("WeeklyActivityParams") = obj
        Return obj
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetClientsFilteredByDate(ByVal DateFrom As String,
                                                    ByVal DateTo As String) As List(Of WeeklyActivityDO)
        Dim dt As New DataTable
        Dim obj As WeeklyActivityDO = HttpContext.Current.Session("WeeklyActivityParams")
        Return obj.GetClientsFilteredByDate(obj.BranchHeadName, DateFrom, DateTo, obj.BranchCode)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetWeeklyActivityCPARevisitClients(ByVal DateFrom As String,
                                                              ByVal DateTo As String,
                                                              ByVal Year As String,
                                                              ByVal Month As String,
                                                              ByVal Week As String) As List(Of WeeklyActivityDO)
        Dim dt As New DataTable
        Dim obj As WeeklyActivityDO = HttpContext.Current.Session("WeeklyActivityParams")
        Return obj.GetWeeklyActivityCPARevisitClients(obj.BranchHeadName, DateFrom, DateTo, obj.BranchCode)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GenerateReport(ByVal TableName As String,
                                          ByVal obj1 As WeeklyActivityDO) As String
        Dim message As String = String.Empty
        Dim obj As WeeklyActivityDO = HttpContext.Current.Session("WeeklyActivityReportParams")
        obj.YearNumber = obj1.YearNumber
        obj.MonthCode = CInt(obj1.MonthCode).ToString("0#")
        obj.Month = obj1.Month
        obj.WeekNumber = obj1.WeekNumber
        obj.Week = obj1.Week
        GetDate(obj)

        If obj.isValid() Then
            obj.TableName = TableName
            obj.Week = obj.WeekNumber + If(obj.WeekNumber = 1, "st", If(obj.WeekNumber = 2, "nd", If(obj.WeekNumber = 3, "rd", "th")))
            HttpContext.Current.Session("ReportName") = "WeeklyActivity"
            HttpContext.Current.Session("ReportParams") = obj
        Else
            message = obj.errMsg
        End If

        Return message
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SaveDetails(ByVal clientID As String) As String
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim message As String = String.Empty
        Dim access As ProfileDO = GetModuleAccess()

        If Not clientID.Equals("") Then
            HttpContext.Current.Session("ConversionAccessMatrix") = access
            HttpContext.Current.Session("ClientID") = clientID
        Else
            message = "No Details"
        End If

        Return message
    End Function

    Class FiltersOnLoad
        Property currentUser As Object
        Property RegionList As List(Of ReferencesForDropdown)
        Property UsersList As List(Of ReferencesForDropdown)
        Property BranchesList As List(Of ReferencesForDropdown)
        Property YearList As List(Of ReferencesForDropdown)
        Property MonthList As List(Of ReferencesForDropdown)
        Property WeekList As List(Of ReferencesForDropdown)
        Property ClientDetails As WeeklyActivityDO
    End Class
End Class