Imports System.Web.Script.Services
Imports System.Web.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities
Imports System.Web.HttpContext
Imports System.Configuration.ConfigurationManager

Public Class WeeklyActivity
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Weekly Activity"
        If Not Page.IsPostBack Then
            HttpContext.Current.Session.Remove("WeeklyActivityParams")
            HttpContext.Current.Session.Remove("WeeklyActivityReportParams")
            Page.DataBind()
        End If
    End Sub

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function OnLoadData() As FiltersOnLoad
        Dim obj As New WeeklyActivityDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")

        Dim dtRegion As New DataTable
        dtRegion = obj.GetGroupList()

        Dim groupCode As String = If({Current.GetSection("Commons")("TA").ToString(), Current.GetSection("Commons")("GroupHead").ToString()}.Contains(currentUser.RoleID), currentUser.GroupCode, dtRegion.Rows(0).Item("RegionCode").ToString())
        'Dim groupCode As String = If({Current.GetSection("Commons")("GroupHead").ToString(), Current.GetSection("Commons")("TA").ToString(), Current.GetSection("Commons")("RO").ToString()}.Contains(currentUser.RoleID), currentUser.GroupCode, String.Empty)
        Dim branchCode As String = IIf(currentUser.RoleID = Current.GetSection("Commons")("BM").ToString(), currentUser.BranchCode, String.Empty)
        Return New FiltersOnLoad With {
             .currentUser = currentUser,
            .GroupList = ReferencesForDropdown.convertToReferencesList(dtRegion, "RegionCode", "RegionName"),
            .UsersList = ReferencesForDropdown.convertToReferencesList(obj.GetUsersList(groupCode, branchCode, Current.GetSection("Commons")("BM").ToString()), "Username", "DisplayName"),
            .BranchesList = ReferencesForDropdown.convertToReferencesList(obj.GetBranchesList(groupCode), "BranchCode", "BranchName", combineData:=True),
            .YearList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetYear(), "Year", "Year"),
            .GroupCode = Current.GetSection("Commons")("GroupHead").ToString() & "," & Current.GetSection("Commons")("TA").ToString() & "," & Current.GetSection("Commons")("RO").ToString()
        }
    End Function

#Region "GET"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetWeeklyActivity(ByVal obj As WeeklyActivityDO) As List(Of WeeklyActivityDO)
        Dim waDO As New WeeklyActivityDO
        HttpContext.Current.Session("WeeklyActivityReportParams") = obj
        HttpContext.Current.Session.Remove("ReportName")
        HttpContext.Current.Session.Remove("ReportParams")
        Return waDO.GetWeeklyActivity(obj)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetUsersList(ByVal groupCode As String, ByVal branchCode As String) As List(Of ReferencesForDropdown)
        Dim obj As New WeeklyActivityDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return ReferencesForDropdown.convertToReferencesList(obj.GetUsersList(groupCode, branchCode, Current.GetSection("Commons")("BM").ToString()), "Username", "DisplayName")
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetBranchesList(ByVal group As String) As List(Of ReferencesForDropdown)
        Dim obj As New WeeklyActivityDO
        Return ReferencesForDropdown.convertToReferencesList(obj.GetBranchesList(group), "BranchCode", "BranchName", combineData:=True)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateSearchFilter(ByVal obj As WeeklyActivityDO) As String
        Dim msg As String = String.Empty
        If Not obj.isValid Then
            msg = obj.errMsg
        End If
        Return msg
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ClientDetails(ByVal obj As WeeklyActivityDO) As WeeklyActivityDO
        HttpContext.Current.Session("WeeklyActivityParams") = obj
        Return HttpContext.Current.Session("WeeklyActivityParams")
    End Function

#End Region

    Class FiltersOnLoad
        Property currentUser As Object
        Property GroupList As List(Of ReferencesForDropdown)
        Property UsersList As List(Of ReferencesForDropdown)
        Property BranchesList As List(Of ReferencesForDropdown)
        Property YearList As List(Of ReferencesForDropdown)
        Property GroupCode As String
    End Class

End Class