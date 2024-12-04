Imports System.Web.Script.Services
Imports System.Web.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities
Imports System.Configuration.ConfigurationManager

Public Class Annual_YTD
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Annual-YTD Summary Per Group"
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
            .RegionList = ReferencesForDropdown.convertToReferencesList(ConversionSummaryReportDO.GetRegionList(), "RegionCode", "RegionName"),
            .ClientTypeList = ReferencesForDropdown.convertToReferencesList(SummaryPerGroupDO.GetClientTypeList(), "ClientTypeCode", "ClientTypeName"),
            .IndustryTypeList = ReferencesForDropdown.convertToReferencesList(SummaryPerGroupDO.GetIndustryTypeList(), "IndustryCode", "IndustryDesc"),
            .ADBRangeList = ReferencesForDropdown.convertToReferencesList(SummaryPerGroupDO.GetADBRangeList(), "ADBRangeName", "ADBRangeName")
        }
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateSearchFilter(ByVal obj As SummaryPerGroupDO) As String
        Dim msg As String = String.Empty
        WriteActivityLog(ActivityLog.ActionTaken.SEARCH)
        obj.moduleName = "Annual"
        If Not obj.isValid Then
            msg = obj.errMsg
        Else
            HttpContext.Current.Session("SummaryPerGroupDOParams") = obj
        End If
        Return msg
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAnnualDetails(ByVal obj As SummaryPerGroupDO) As AnnualDetails
        obj.RoleID = GetSection("Commons")("BM").ToString
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        obj.isAdmin = If({GetSection("Commons")("AppAdminMaker").ToString, GetSection("Commons")("AppAdminApprover").ToString, GetSection("Commons")("SectorHead").ToString}.Contains(currentUser.RoleID), True, False)
        HttpContext.Current.Session("SummaryPerGroupDOParams") = obj
        Return New AnnualDetails With {
           .AnnualList = SummaryPerGroupDO.GetAnnualList(obj),
           .AnnualListTotalDetails = SummaryPerGroupDO.GetAnnualListTotal(obj)
       }
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetCASATypesList(ByVal branchCode As String) As List(Of SummaryPerGroupDO)
        Dim obj As SummaryPerGroupDO = HttpContext.Current.Session("SummaryPerGroupDOParams")
        obj.BranchCode = branchCode
        Return SummaryPerGroupDO.GetCASATypesList(obj)
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetLoanProductLists(ByVal branchCode As String) As List(Of SummaryPerGroupDO)
        Dim obj As SummaryPerGroupDO = HttpContext.Current.Session("SummaryPerGroupDOParams")
        obj.BranchCode = branchCode
        Return SummaryPerGroupDO.GetLoanProductList(obj)
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GenerateReport(ByVal table As String) As String
        Dim message As String = String.Empty
        Dim obj As SummaryPerGroupDO = HttpContext.Current.Session("SummaryPerGroupDOParams")
        If obj.isValid() Then
            Dim rptName As String
            If table = "Loan" Then
                rptName = "SummaryPerGrp_LoansReport"
            ElseIf table = "CASA" Then
                rptName = "SummaryPerGrp_CASAReport"
            ElseIf table = "LeadSource" Then
                rptName = "SummaryLeadSourcePerBranch"
            Else
                obj.moduleName = "Annual"
                obj.rptHeader = "( For the Year : " + obj.Year + " | Client Type : " + obj.ClientTypeDesc + " )"
                rptName = "AnnualSummaryPerBranch"

            End If
            HttpContext.Current.Session("ReportName") = rptName
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
        Property YearList As List(Of ReferencesForDropdown)
        Property RegionList As List(Of ReferencesForDropdown)
        Property ClientTypeList As List(Of ReferencesForDropdown)
        Property IndustryTypeList As List(Of ReferencesForDropdown)
        Property ADBRangeList As List(Of ReferencesForDropdown)
    End Class

    Class AnnualDetails
        Property AnnualList As List(Of SummaryPerGroupDO)
        Property AnnualListTotalDetails As SummaryPerGroupDO
    End Class
End Class