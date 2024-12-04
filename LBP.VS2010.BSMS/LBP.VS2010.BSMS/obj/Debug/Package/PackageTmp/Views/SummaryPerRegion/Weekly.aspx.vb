Imports System.Web.Script.Services
Imports System.Web.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities
Imports System.Configuration.ConfigurationManager

Public Class Weekly
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Weekly Summary Per Group"
        If Not Page.IsPostBack Then
            HttpContext.Current.Session.Remove("SummaryPerGroupDOParams")
            Page.DataBind()
        End If
    End Sub

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function OnLoadData() As FiltersOnLoad
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return New FiltersOnLoad With {
            .currentGroupCode = If({GetSection("Commons")("GroupHead").ToString, GetSection("Commons")("TA").ToString, GetSection("Commons")("RO").ToString}.Contains(currentUser.RoleID), currentUser.GroupCode, String.Empty),
            .currentDateDefault = DateMaintenanceDO.GetCurrentDateDefaultValue(),
            .isAdmin = If({GetSection("Commons")("AppAdminMaker").ToString, GetSection("Commons")("AppAdminApprover").ToString, GetSection("Commons")("SectorHead").ToString}.Contains(currentUser.RoleID), True, False),
            .MonthList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetMonth(), "MonthCode", "Month"),
            .Week = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetWeek(DateTime.Now.Year, MonthName(DateTime.Now.Month)), "WeekValue", "WeekDescription"),
            .YearList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetYear(), "Year", "Year"),
            .RegionList = ReferencesForDropdown.convertToReferencesList(ConversionSummaryReportDO.GetRegionList(), "RegionCode", "RegionName"),
            .ClientTypeList = ReferencesForDropdown.convertToReferencesList(SummaryPerGroupDO.GetClientTypeList(), "ClientTypeCode", "ClientTypeName")
        }
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetWeek(ByVal year As String, ByVal month As String) As List(Of ReferencesForDropdown)
        Return ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetWeek(year, month), "WeekValue", "WeekDescription")
    End Function


    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateSearchFilter(ByVal obj As SummaryPerGroupDO) As String
        Dim msg As String = String.Empty
        obj.moduleName = "Weekly"
        WriteActivityLog(ActivityLog.ActionTaken.SEARCH)
        If Not obj.isValid Then
            msg = obj.errMsg
        End If
        Return msg
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetWeeklyDetails(ByVal obj As SummaryPerGroupDO) As WeeklyDetails
        obj.RoleID = GetSection("Commons")("BM").ToString
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        obj.isAdmin = If({GetSection("Commons")("AppAdminMaker").ToString, GetSection("Commons")("AppAdminApprover").ToString, GetSection("Commons")("SectorHead").ToString}.Contains(currentUser.RoleID), True, False)
        HttpContext.Current.Session("SummaryPerGroupDOParams") = obj
        Return New WeeklyDetails With {
           .WeeklyList = SummaryPerGroupDO.GetWeeklyList(obj),
           .WeeklyListTotalDetails = SummaryPerGroupDO.GetWeeklyListTotal(obj)
       }
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GenerateReport() As String
        Dim message As String = String.Empty
        Dim obj As SummaryPerGroupDO = HttpContext.Current.Session("SummaryPerGroupDOParams")
        obj.moduleName = "Weekly"
        If obj.isValid() Then
            obj.rptHeader = "( For the Week, Month and Year : Week " + obj.WeekNumber + " of " + obj.Month + " " + obj.Year + " | Client Type : " + obj.ClientTypeDesc + " )"
            HttpContext.Current.Session("ReportName") = "SummaryPerBranch"
            HttpContext.Current.Session("ReportParams") = obj

        Else
            message = obj.errMsg
        End If

        Return message
    End Function

    Class FiltersOnLoad
        Property isAdmin As Boolean
        Property currentGroupCode As String
        Property currentDateDefault As New DateMaintenanceDO
        Property Week As List(Of ReferencesForDropdown)
        Property MonthList As List(Of ReferencesForDropdown)
        Property YearList As List(Of ReferencesForDropdown)
        Property RegionList As List(Of ReferencesForDropdown)
        Property ClientTypeList As List(Of ReferencesForDropdown)
        Property IndustryTypeList As List(Of ReferencesForDropdown)
        Property ADBRangeList As List(Of ReferencesForDropdown)
    End Class

    Class WeeklyDetails
        Property WeeklyList As List(Of SummaryPerGroupDO)
        Property WeeklyListTotalDetails As SummaryPerGroupDO
    End Class
End Class