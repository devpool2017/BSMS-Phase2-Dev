Imports System.Web.Script.Services
Imports System.Web.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities
Imports System.Configuration.ConfigurationManager


Public Class MonthlySummary
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Monthly Summary Report"
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
            .YearList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetYear(), "Year", "Year"),
            .MonthList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetMonth(), "MonthCode", "Month")
           }
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateSearchFilter(ByVal obj As ConversionSummaryReportDO) As String
        Dim msg As String = String.Empty
        WriteActivityLog(ActivityLog.ActionTaken.SEARCH)
        obj.moduleName = "Monthly"
        If Not obj.isValid Then
            msg = obj.errMsg
        End If
        Return msg
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetBranchesList(ByVal groupCode As String) As List(Of ReferencesForDropdown)
        Return ReferencesForDropdown.convertToReferencesList(ConversionSummaryReportDO.GetBranchesList(groupCode), "Code", "Name")
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetUsersList(ByVal branchCode As String) As List(Of ReferencesForDropdown)
        Dim role As PositionID = GetRoleID()
        Return ReferencesForDropdown.convertToReferencesList(ConversionSummaryReportDO.GetUsersList(branchCode, role.BH), "Code", "Name")
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SearchMonthlyReport(ByVal obj As ConversionSummaryReportDO) As List(Of ConversionSummaryReportDO)
        HttpContext.Current.Session("ConversionSummaryReportParams") = obj
        Return ConversionSummaryReportDO.SearchMonthlyReport(obj.Username, obj.BranchCode, obj.Year, obj.Month, obj.MonthCode)
    End Function

    <WebMethod(EnableSession:=True)>
  <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetTotalMonthlyReport(ByVal obj As ConversionSummaryReportDO) As ConversionSummaryReportDO
        Return ConversionSummaryReportDO.GetTotalMonthlyReport(obj.Username, obj.BranchCode, obj.Year, obj.Month, obj.MonthCode)
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetRHMonthlyRemarks() As String
        Dim obj As New ConversionSummaryReportDO
        Dim role As PositionID = GetRoleID()
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        If Not IsNothing(HttpContext.Current.Session("ConversionSummaryReportParams")) Then
            obj = HttpContext.Current.Session("ConversionSummaryReportParams")
        End If
        Dim TableName As String = If({role.AppAdminM, role.AppAdminA, role.SH, role.GH, role.RO, role.TA}.Contains(currentUser.RoleID), "MonthlyRemarks2", If(currentUser.RoleID = role.BH, "MonthlyRemarks", String.Empty))
        Dim MonthlyRemarks As String = String.Empty
        If obj.ChkMonthlyRemarksExists(TableName, obj.Year, obj.Month, obj.Username) Then
            MonthlyRemarks = obj.GetMonthlyRemarks(TableName, obj.Year, obj.Month, obj.Username)
        Else
            MonthlyRemarks = ""
        End If
        Return MonthlyRemarks
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAddMonthlyRemarks(ByVal Comment As String, ByVal isForAdmin As Boolean) As ConversionSummaryReportDO
        Dim obj As New ConversionSummaryReportDO
        Dim role As PositionID = GetRoleID()
        If Not IsNothing(HttpContext.Current.Session("ConversionSummaryReportParams")) Then
            obj = HttpContext.Current.Session("ConversionSummaryReportParams")
        End If
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim TableName As String = If({role.AppAdminM, role.AppAdminA, role.SH, role.GH, role.RO, role.TA}.Contains(currentUser.RoleID), If(isForAdmin, "MonthlyRemarks2", "All"), If(currentUser.RoleID = role.BH, "All", String.Empty))
        Dim TableNameToSelect As String = If({role.AppAdminM, role.AppAdminA, role.SH, role.GH, role.RO, role.TA}.Contains(currentUser.RoleID), "MonthlyRemarks2", If(currentUser.RoleID = role.BH, "MonthlyRemarks", String.Empty))
        Dim fontcolor As String = If({role.GH, role.RO}.Contains(currentUser.RoleID), "<font color=""orange"">", If({role.AppAdminA, role.AppAdminM, role.SH}.Contains(currentUser.RoleID), "<font color=""#003399"">", "<font color=""#007700"">"))
        Dim NewMonthlyComments, clientRemarks, OldMonthlyRemarks As String

        If obj.ChkMonthlyRemarksExists(TableNameToSelect, obj.Year, obj.Month, obj.Username) Then
            If TableName = "All" Then
                If UpdateMonthlyRemarksForAll(obj, Comment, "MonthlyRemarks2", fontcolor, currentUser.DisplayName).isSuccess Then
                    Return UpdateMonthlyRemarksForAll(obj, Comment, "MonthlyRemarks", fontcolor, currentUser.DisplayName)
                Else
                    Return UpdateMonthlyRemarksForAll(obj, Comment, "MonthlyRemarks2", fontcolor, currentUser.DisplayName)
                End If
            Else
                OldMonthlyRemarks = obj.GetMonthlyRemarks(TableNameToSelect, obj.Year, obj.Month, obj.Username)
                NewMonthlyComments = If((Not Comment = Nothing), _
                                                        OldMonthlyRemarks & fontcolor & currentUser.DisplayName & " said on " _
                                                        & My.Computer.Clock.LocalTime.ToString & " : " & Comment & "</font><br>", _
                                                       OldMonthlyRemarks & "")
                clientRemarks = NewMonthlyComments
                NewMonthlyComments = clientRemarks.Replace(";", ".")
                Return obj.UpdateMonthlyRemarks(TableName, obj.Year, obj.Month, obj.Username, NewMonthlyComments)
            End If
        Else
            NewMonthlyComments = fontcolor & currentUser.DisplayName & " said on " & My.Computer.Clock.LocalTime.ToString & " : " & Comment & "</font><br>" & ""
            clientRemarks = NewMonthlyComments
            NewMonthlyComments = clientRemarks.Replace(";", ".")
            obj.isSuccess = obj.AddMonthlyRemarks(TableName, obj.Year, obj.Month, obj.Username, NewMonthlyComments)
        End If

        Return obj
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function UpdateMonthlyRemarksForAll(ByVal obj As ConversionSummaryReportDO,
                                               ByVal Comment As String,
                                               ByVal TableName As String,
                                               ByVal fontcolor As String,
                                               ByVal DisplayName As String) As ConversionSummaryReportDO
        Dim NewMonthlyComments, clientRemarks, OldMonthlyRemarks As String
        OldMonthlyRemarks = obj.GetMonthlyRemarks(TableName, obj.Year, obj.Month, obj.Username)
        NewMonthlyComments = If((Not Comment = Nothing), _
                                                OldMonthlyRemarks & fontcolor & DisplayName & " said on " _
                                                & My.Computer.Clock.LocalTime.ToString & " : " & Comment & "</font><br>", _
                                               OldMonthlyRemarks & "")
        clientRemarks = NewMonthlyComments
        NewMonthlyComments = clientRemarks.Replace(";", ".")
        If OldMonthlyRemarks = String.Empty Then
            obj.isSuccess = obj.AddMonthlyRemarks(TableName, obj.Year, obj.Month, obj.Username, NewMonthlyComments)
        Else
            Return obj.UpdateMonthlyRemarks(TableName, obj.Year, obj.Month, obj.Username, NewMonthlyComments)
        End If

        Return obj
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GenerateReport(ByVal branchName As String) As String
        Dim message As String = String.Empty
        Dim obj As ConversionSummaryReportDO = HttpContext.Current.Session("ConversionSummaryReportParams")
        If Not IsNothing(obj) Then
            obj.BranchName = branchName
            If obj.isValid() Then
                HttpContext.Current.Session("ReportName") = "Monthly_CSR"
                HttpContext.Current.Session("ReportParams") = obj
            Else
                message = obj.errMsg
            End If

        End If
        Return message
    End Function

    Public Shared Function GetRoleID() As PositionID
        Return New PositionID With {
       .BH = GetSection("Commons")("BM").ToString,
       .RO = GetSection("Commons")("RO").ToString,
       .TA = GetSection("Commons")("TA").ToString,
       .GH = GetSection("Commons")("GroupHead").ToString,
       .SH = GetSection("Commons")("SectorHead").ToString,
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
        Property MonthList As List(Of ReferencesForDropdown)
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