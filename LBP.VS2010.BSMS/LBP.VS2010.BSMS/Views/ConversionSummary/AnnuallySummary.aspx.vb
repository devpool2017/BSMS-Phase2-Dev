Imports System.Web.Script.Services
Imports System.Web.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities
Imports System.Configuration.ConfigurationManager


Public Class AnnuallySummary
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Annual Summary Report"
        If Not Page.IsPostBack Then
            HttpContext.Current.Session.Remove("ConversionSummaryReportParams")
            Page.DataBind()
        End If
    End Sub
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function OnLoadData() As FiltersOnLoad
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim role As PositionID = GetRoleID()
        Dim username As String = IIf(currentUser.RoleID = role.BH, currentUser.Username, String.Empty)
        Dim branchCode As String = IIf(currentUser.RoleID = role.BH, currentUser.BranchCode, String.Empty)
        Dim groupCode As String = If({role.GH, role.RO, role.BH, role.TA}.Contains(currentUser.RoleID), currentUser.GroupCode, String.Empty)
        Return New FiltersOnLoad With {
            .isBH = If({role.BH}.Contains(currentUser.RoleID), True, False),
            .isHead = If({role.GH, role.RO, role.TA}.Contains(currentUser.RoleID), True, False),
            .isAdmin = If({role.AppAdminA, role.AppAdminM, role.SH}.Contains(currentUser.RoleID), True, False),
             .currentUser = currentUser,
             .currentDateDefault = DateMaintenanceDO.GetCurrentDateDefaultValue(),
             .RegionList = ReferencesForDropdown.convertToReferencesList(ConversionSummaryReportDO.GetRegionList(), "RegionCode", "RegionName"),
             .UsersList = ReferencesForDropdown.convertToReferencesList(ConversionSummaryReportDO.GetUsersList(branchCode, role.BH), "Code", "Name"),
             .BranchesList = ReferencesForDropdown.convertToReferencesList(ConversionSummaryReportDO.GetBranchesList(groupCode), "Code", "Name"),
             .YearList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetYear(), "Year", "Year")
              }
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetUsersList(ByVal branchCode As String) As List(Of ReferencesForDropdown)
        Dim role As PositionID = GetRoleID()
        Return ReferencesForDropdown.convertToReferencesList(ConversionSummaryReportDO.GetUsersList(branchCode, role.BH), "Code", "Name")
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetBranchesList(ByVal groupCode As String) As List(Of ReferencesForDropdown)
        Return ReferencesForDropdown.convertToReferencesList(ConversionSummaryReportDO.GetBranchesList(groupCode), "Code", "Name")
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateSearchFilter(ByVal obj As ConversionSummaryReportDO) As String
        Dim msg As String = String.Empty
        obj.moduleName = "Annually"
        WriteActivityLog(ActivityLog.ActionTaken.SEARCH)
        If Not obj.isValid Then
            msg = obj.errMsg
        End If
        Return msg
    End Function


        <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
        Public Shared Function SearchAnnuallyReport(ByVal obj As ConversionSummaryReportDO) As List(Of ConversionSummaryReportDO)
        HttpContext.Current.Session("ConversionSummaryReportParams") = obj
        HttpContext.Current.Session.Remove("ReportName")
        HttpContext.Current.Session.Remove("ReportParams")
        Return ConversionSummaryReportDO.SearchAnnuallyReport(obj.Username, obj.BranchCode, obj.Year)
        End Function

        <WebMethod(EnableSession:=True)>
      <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
        Public Shared Function GetTotalAnnuallyReport(ByVal obj As ConversionSummaryReportDO) As ConversionSummaryReportDO
        Return ConversionSummaryReportDO.GetTotalAnnuallyReport(obj.Username, obj.BranchCode, obj.Year)
        End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GenerateReport() As String
        Dim message As String = String.Empty
        Dim obj As ConversionSummaryReportDO = HttpContext.Current.Session("ConversionSummaryReportParams")
        If obj.isValid() Then
            HttpContext.Current.Session("ReportName") = "Annually_CSR"
            HttpContext.Current.Session("ReportParams") = obj

            'WriteActivityLog(ActivityLog.ActionTaken.GENERATE)
        Else
            message = obj.errMsg
        End If

        Return message
    End Function

    Public Shared Function GetRoleID() As PositionID
        Return New PositionID With {
       .BH = GetSection("Commons")("BM").ToString,
       .RO = GetSection("Commons")("RO").ToString,
       .GH = GetSection("Commons")("GroupHead").ToString,
       .SH = GetSection("Commons")("SectorHead").ToString,
       .TA = GetSection("Commons")("TA").ToString,
       .AppAdminA = GetSection("Commons")("AppAdminApprover").ToString,
       .AppAdminM = GetSection("Commons")("AppAdminMaker").ToString
       }
    End Function

    Class FiltersOnLoad
        Property isAdmin As Boolean
        Property isBH As Boolean
        Property isHead As Boolean
        Property currentUser As Object
        Property currentDateDefault As New DateMaintenanceDO
        Property RegionList As List(Of ReferencesForDropdown)
        Property UsersList As List(Of ReferencesForDropdown)
        Property BranchesList As List(Of ReferencesForDropdown)
        Property YearList As List(Of ReferencesForDropdown)
    End Class

    Class PositionID
        Property BH As String
        Property RO As String
        Property GH As String
        Property SH As String
        Property TA As String
        Property AppAdminA As String
        Property AppAdminM As String
    End Class
End Class