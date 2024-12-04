Imports System.Web.Script.Services
Imports System.Web.Services
Imports LBP.VS2010.BSMS.AnnuallySummary
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities
Imports System.Configuration.ConfigurationManager

Public Class ChangeInPotentialAccounts
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Changes in Potential Accounts"
        If Not Page.IsPostBack Then
            HttpContext.Current.Session.Remove("ChangInPotentialAccountsReportParams")
            Page.DataBind()
        End If
    End Sub

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function OnLoadData() As FiltersOnLoad
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")

        Dim PotentialAccountsDO As New ChangeInPotentialAccountsDO
        Dim dtRegion As New DataTable
        dtRegion = PotentialAccountsDO.GetRegionList()

        PotentialAccountsDO.Username = currentUser.Username
        PotentialAccountsDO.GroupCode = If({GetSection("Commons")("TA").ToString(), GetSection("Commons")("GroupHead").ToString()}.Contains(currentUser.RoleID), currentUser.GroupCode, dtRegion.Rows(0).Item("RegionCode").ToString())
        PotentialAccountsDO.BranchCode = currentUser.BranchCode


        Return New FiltersOnLoad With {
         .currentUser = currentUser,
         .RegionList = ReferencesForDropdown.convertToReferencesList(dtRegion, "RegionCode", "RegionName"),
         .UsersList = ReferencesForDropdown.convertToReferencesList(PotentialAccountsDO.GetUsersList(GetSection("Commons")("BM").ToString()), "Username", "DisplayName"),
         .BranchesList = ReferencesForDropdown.convertToReferencesList(PotentialAccountsDO.GetBranchesList(PotentialAccountsDO.GroupCode), "BranchCode", "BranchName"),
         .YearList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetYear(), "Year", "Year"),
         .MonthList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetMonth(), "MonthCode", "Month"),
         .WeekList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetWeek(Date.Today.Year.ToString, DateTime.Today.ToString("MMMM")), "WeekValue", "WeekDescription"),
         .GroupCode = GetSection("Commons")("GroupHead").ToString() & "," & GetSection("Commons")("TA").ToString(),
         .SectorCode = GetSection("Commons")("SectorHead").ToString() & "," & GetSection("Commons")("AppAdminMaker").ToString() & "," & GetSection("Commons")("AppAdminApprover").ToString()
         }
    End Function



    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetBranchesList(ByVal group As String) As List(Of ReferencesForDropdown)
        Dim obj As New ChangeInPotentialAccountsDO
        Return ReferencesForDropdown.convertToReferencesList(obj.GetBranchesList(group), "BranchCode", "BranchName")
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetUsersList(ByVal groupCode As String, ByVal branchCode As String) As List(Of ReferencesForDropdown)
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        'currentUser.GroupCode = groupCode
        'currentUser.GroupCode = "06"    'this is only for testing'

        Dim PotentialAccountsDO As New ChangeInPotentialAccountsDO
        PotentialAccountsDO.GroupCode = groupCode
        PotentialAccountsDO.BranchCode = branchCode

        Return ReferencesForDropdown.convertToReferencesList(PotentialAccountsDO.GetUsersList(GetSection("Commons")("BM").ToString()), "Username", "DisplayName")
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetWeek(ByVal year As String, ByVal month As String) As List(Of ReferencesForDropdown)
        Return ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetWeek(year, month), "WeekValue", "WeekDescription")
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetDate(ByVal obj As ChangeInPotentialAccountsDO) As DateMaintenanceDO
        Dim objDate As DateMaintenanceDO = DateMaintenanceDO.GetDate(obj.Year, obj.Month, obj.WeekNumber)
        obj.DateFrom = objDate.FromDate
        obj.DateToday = objDate.ToDate
        objDate.Year = obj.Year
        objDate.Month = obj.Month

        HttpContext.Current.Session("ChangInPotentialAccountsReportParams") = obj
        Return objDate
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateSearchFilter(ByVal obj As ConversionSummaryReportDO) As String
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
    Public Shared Function GetWeekNumber(ByVal DateToday As String)
        Return ChangeInPotentialAccountsDO.GetWeekNum(DateToday)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetPotentialAccountsList(ByVal BranchCode As String, ByVal DateFrom As String, ByVal DateToday As String, ByVal Year As String, ByVal Month As String) As List(Of ChangeInPotentialAccountsDO)
        Return ChangeInPotentialAccountsDO.GetPotentialAccountsList(BranchCode, DateFrom, DateToday, Year, Month)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GenerateReport() As String
        Dim message As String = String.Empty
        Dim obj As ChangeInPotentialAccountsDO = HttpContext.Current.Session("ChangInPotentialAccountsReportParams")
        If Not IsNothing(obj) Then
            ' obj.BranchName = branchName
            If obj.isValid() Then
                HttpContext.Current.Session("ReportName") = "ChangeInPotentialAccounts"
                HttpContext.Current.Session("ReportParams") = obj


                'WriteActivityLog(ActivityLog.ActionTaken.GENERATE)
            Else
                message = obj.errMsg
            End If

        End If

        Return message
    End Function



    'Filters for Change in Potential Accounts Module
    Class FiltersOnLoad
        Property currentUser As Object
        Property RegionList As List(Of ReferencesForDropdown)
        Property UsersList As List(Of ReferencesForDropdown)
        Property BranchesList As List(Of ReferencesForDropdown)
        Property YearList As List(Of ReferencesForDropdown)
        Property MonthList As List(Of ReferencesForDropdown)
        Property WeekList As List(Of ReferencesForDropdown)
        Property GroupCode As String
        Property SectorCode As String
    End Class
End Class