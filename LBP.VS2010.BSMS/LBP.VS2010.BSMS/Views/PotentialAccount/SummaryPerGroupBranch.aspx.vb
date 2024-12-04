Imports System.Web.Script.Services
Imports System.Web.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities
Imports System.Web.HttpContext
Public Class SummaryPerGroupBranch
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Summary Per Group/Branch"
        hdnError.Value = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.SYSTEM_PROBLEM, Nothing, Nothing)
        If Not Page.IsPostBack Then
            HttpContext.Current.Session.Remove("ReportParams")
            Page.DataBind()
        End If
    End Sub

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function OnLoadData() As FiltersOnLoad
        Dim obj As New SummaryPerGroupBranchDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return New FiltersOnLoad With {
             .currentUser = currentUser,
            .YearList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetYear(), "Year", "Year"),
            .IndustryList = ReferencesForDropdown.convertToReferencesList(obj.GetIndustryList(), "IndustryCode", "IndustryCodeDesc"),
            .GroupList = ReferencesForDropdown.convertToReferencesList(obj.GetGroupList(), "RegionCode", "RegionName"),
            .GroupCode = Current.GetSection("Commons")("GroupHead").ToString() & "," & Current.GetSection("Commons")("RO").ToString() & "," & Current.GetSection("Commons")("TA").ToString()
        }
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateSearchFilter(ByVal obj As SummaryPerGroupBranchDO) As String
        Dim msg As String = String.Empty
        If Not obj.isValid Then
            msg = obj.errMsg
        End If
        Return msg
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetSummaryPerGroupBranch(ByVal obj As SummaryPerGroupBranchDO) As List(Of SummaryPerGroupBranchDO)
        Dim gpDO As New SummaryPerGroupBranchDO
        Dim branchHeadRoleID As String = Current.GetSection("Commons")("BM").ToString()
        Return gpDO.GetCPASummaryPerGroupBranch(obj, branchHeadRoleID)
    End Function


    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GenerateReport(ByVal obj As SummaryPerGroupBranchDO) As String
        Dim message As String = String.Empty

        Try
            Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
            obj.RoleID = currentUser.RoleID
            obj.IsGroupHead = ({Current.GetSection("Commons")("GroupHead").ToString(), Current.GetSection("Commons")("RO").ToString(), Current.GetSection("Commons")("TA").ToString()}.Contains(currentUser.RoleID))

            If obj.isValid() Then
                HttpContext.Current.Session("ReportName") = "SummaryPerGroupBranch"
                HttpContext.Current.Session("ReportParams") = obj
            Else
                message = obj.errMsg
            End If

        Catch ex As Exception
            message = ex.Message
            Logger.writeLog(ModuleName, "Generate Report", "", ex.Message)
        End Try

        Return message
    End Function

    Class FiltersOnLoad
        Property currentUser As Object
        Property GroupCode As String
        Property GroupList As List(Of ReferencesForDropdown)
        Property BranchList As List(Of ReferencesForDropdown)
        Property YearList As List(Of ReferencesForDropdown)
        Property IndustryList As List(Of ReferencesForDropdown)
    End Class
End Class