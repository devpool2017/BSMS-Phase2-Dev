Imports System.Web.Script.Services
Imports System.Web.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities
Imports System.Configuration.ConfigurationManager

Public Class Monthly
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Monthly Summary Per Group"
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
            .YearList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetYear(), "Year", "Year"),
            .MonthList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetMonth(), "MonthCode", "Month"),
            .RegionList = ReferencesForDropdown.convertToReferencesList(ConversionSummaryReportDO.GetRegionList(), "RegionCode", "RegionName"),
            .ClientTypeList = ReferencesForDropdown.convertToReferencesList(SummaryPerGroupDO.GetClientTypeList(), "ClientTypeCode", "ClientTypeName")
        }
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateSearchFilter(ByVal obj As SummaryPerGroupDO) As String
        Dim msg As String = String.Empty
        WriteActivityLog(ActivityLog.ActionTaken.SEARCH)
        obj.moduleName = "Monthly"
        If Not obj.isValid Then
            msg = obj.errMsg
        Else
            HttpContext.Current.Session("SummaryPerGroupDOParams") = obj
        End If
        Return msg
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetMonthlyDetails(ByVal obj As SummaryPerGroupDO) As MonthlyDetails
        obj.RoleID = GetSection("Commons")("BM").ToString
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        obj.isAdmin = If({GetSection("Commons")("AppAdminMaker").ToString, GetSection("Commons")("AppAdminApprover").ToString, GetSection("Commons")("SectorHead").ToString}.Contains(currentUser.RoleID), True, False)
        HttpContext.Current.Session("SummaryPerGroupDOParams") = obj
        Return New MonthlyDetails With {
           .MonthlyList = SummaryPerGroupDO.GetMonthlyList(obj),
           .MonthlyListTotalDetails = SummaryPerGroupDO.GetMonthlyListTotal(obj)
       }
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetLoanProductLists(ByVal obj As SummaryPerGroupDO) As List(Of SummaryPerGroupDO)
        HttpContext.Current.Session("SummaryPerGroupDOParams") = obj
        Return SummaryPerGroupDO.GetLoanProductList(obj)
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GenerateReport() As String
        Dim message As String = String.Empty
        Dim obj As SummaryPerGroupDO = HttpContext.Current.Session("SummaryPerGroupDOParams")
        obj.moduleName = "Monthly"
        If obj.isValid() Then
            obj.rptHeader = "( For the Month and Year : " + obj.Month + " " + obj.Year + " | Client Type : " + obj.ClientTypeDesc + " )"
            HttpContext.Current.Session("ReportName") = "SummaryPerBranch"
            HttpContext.Current.Session("ReportParams") = obj

        Else
            message = obj.errMsg
        End If

        Return message
    End Function

    Class FiltersOnLoad
        Property currentGroupCode As String
        Property currentDateDefault As New DateMaintenanceDO
        Property isAdmin As Boolean
        Property MonthList As List(Of ReferencesForDropdown)
        Property YearList As List(Of ReferencesForDropdown)
        Property RegionList As List(Of ReferencesForDropdown)
        Property ClientTypeList As List(Of ReferencesForDropdown)
        Property IndustryTypeList As List(Of ReferencesForDropdown)
        Property ADBRangeList As List(Of ReferencesForDropdown)
    End Class

    Class MonthlyDetails
        Property MonthlyList As List(Of SummaryPerGroupDO)
        Property MonthlyListTotalDetails As SummaryPerGroupDO
    End Class
End Class