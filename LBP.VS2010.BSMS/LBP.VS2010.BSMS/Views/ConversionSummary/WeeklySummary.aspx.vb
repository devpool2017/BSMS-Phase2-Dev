Imports System.Web.Script.Services
Imports System.Web.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities
Imports System.Configuration.ConfigurationManager


Public Class WeeklySummary
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Weekly Summary Report"
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
        Dim month, year As String
        Dim obj As ConversionSummaryReportDO = HttpContext.Current.Session("ConversionSummaryReportParams")
        If IsNothing(HttpContext.Current.Session("ConversionSummaryReportParams")) Then
            month = MonthName(DateTime.Now.Month)
            year = DateTime.Now.Year
        Else
            month = obj.Month
            year = obj.Year
        End If
        Dim dateDefault As New DateMaintenanceDO
        dateDefault = DateMaintenanceDO.GetCurrentDateDefaultValue()

        Return New FiltersOnLoad With {
            .isBH = If({role.BH}.Contains(currentUser.RoleID), True, False),
            .isHead = If({role.GH, role.RO, role.TA}.Contains(currentUser.RoleID), True, False),
            .isAdmin = If({role.AppAdminA, role.AppAdminM, role.SH}.Contains(currentUser.RoleID), True, False),
            .currentUser = currentUser,
            .currentDateDefault = dateDefault,
            .RegionList = ReferencesForDropdown.convertToReferencesList(ConversionSummaryReportDO.GetRegionList(), "RegionCode", "RegionName"),
            .UsersList = ReferencesForDropdown.convertToReferencesList(ConversionSummaryReportDO.GetUsersList(branchCode, role.BH), "Code", "Name"),
            .BranchesList = ReferencesForDropdown.convertToReferencesList(ConversionSummaryReportDO.GetBranchesList(groupCode), "Code", "Name"),
            .YearList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetYear(), "Year", "Year"),
            .MonthList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetMonth(), "MonthCode", "Month"),
            .WeekList = ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetWeek(year, dateDefault.Month), "WeekValue", "WeekDescription"),
            .currentDetails = HttpContext.Current.Session("ConversionSummaryReportParams")
            }
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
    Public Shared Function GetDate(ByVal obj As ConversionSummaryReportDO) As ConversionSummaryReportDO
        Dim objDate As DateMaintenanceDO = DateMaintenanceDO.GetDate(obj.Year, obj.Month, obj.WeekNumber)
        obj.DateFrom = objDate.FromDate
        obj.DateToday = objDate.ToDate

        HttpContext.Current.Session("ConversionSummaryReportParams") = obj
        Return obj
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
    Public Shared Function GetWeek(ByVal year As String, ByVal month As String) As List(Of ReferencesForDropdown)
        Return ReferencesForDropdown.convertToReferencesList(DateMaintenanceDO.GetWeek(year, month), "WeekValue", "WeekDescription")
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetNewLeadsList(ByVal UploadedBy As String, ByVal DateFrom As String, ByVal DateToday As String, ByVal BranchCode As String) As WeeklyDetails
        'Return ConversionSummaryReportDO.GetNewLeadsList(UploadedBy, DateFrom, DateToday, BranchCode)
        Dim Params As New ConversionSummaryReportDO
        If Not IsNothing(HttpContext.Current.Session("ConversionSummaryReportParams")) Then
            Params = HttpContext.Current.Session("ConversionSummaryReportParams")
            Params.Username = UploadedBy
            Params.DateFrom = DateFrom
            Params.DateToday = DateToday
            Params.BranchCode = BranchCode
            HttpContext.Current.Session("ConversionSummaryReportParams") = Params
        Else
            Params.Year = ""
            Params.Month = ""
        End If
        Return New WeeklyDetails With {
        .WeeklyList = ConversionSummaryReportDO.GetNewLeadsList(UploadedBy, DateFrom, DateToday, BranchCode),
        .WeeklyListTotalDetails = ConversionSummaryReportDO.GetTotalNewLeads(UploadedBy, DateFrom, DateToday, BranchCode, Params.Year, Params.Month)
    }
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetTotalNewLeads(ByVal UploadedBy As String, ByVal DateFrom As String, ByVal DateToday As String, ByVal BranchCode As String) As ConversionSummaryReportDO
        Dim Params As New ConversionSummaryReportDO
        If Not IsNothing(HttpContext.Current.Session("ConversionSummaryReportParams")) Then
            Params = HttpContext.Current.Session("ConversionSummaryReportParams")
            Params.Username = UploadedBy
            Params.DateFrom = DateFrom
            Params.DateToday = DateToday
            Params.BranchCode = BranchCode
            HttpContext.Current.Session("ConversionSummaryReportParams") = Params
        Else
            Params.Year = ""
            Params.Month = ""
        End If
        Return ConversionSummaryReportDO.GetTotalNewLeads(UploadedBy, DateFrom, DateToday, BranchCode, Params.Year, Params.Month)
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetPotentialAccountsList(ByVal UploadedBy As String, ByVal DateFrom As String, ByVal DateToday As String, ByVal BranchCode As String) As WeeklyDetails
        'Return ConversionSummaryReportDO.GetPotentialAccountsList(UploadedBy, DateFrom, DateToday, BranchCode)
        Dim Params As New ConversionSummaryReportDO
        If Not IsNothing(HttpContext.Current.Session("ConversionSummaryReportParams")) Then
            Params = HttpContext.Current.Session("ConversionSummaryReportParams")
            Params.Username = UploadedBy
            Params.DateFrom = DateFrom
            Params.DateToday = DateToday
            Params.BranchCode = BranchCode
            HttpContext.Current.Session("ConversionSummaryReportParams") = Params
        Else
            Params.Year = ""
            Params.Month = ""
        End If
        Return New WeeklyDetails With {
        .WeeklyList = ConversionSummaryReportDO.GetPotentialAccountsList(UploadedBy, DateFrom, DateToday, BranchCode),
        .WeeklyListTotalDetails = ConversionSummaryReportDO.GetTotalPotentialAccountsRevisit(UploadedBy, DateFrom, DateToday, BranchCode)
    }
    End Function
    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetTotalPotentialAccounts(ByVal UploadedBy As String, ByVal DateFrom As String, ByVal DateToday As String, ByVal BranchCode As String) As ConversionSummaryReportDO
        Return ConversionSummaryReportDO.GetTotalPotentialAccountsRevisit(UploadedBy, DateFrom, DateToday, BranchCode)
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetRHWeeklyRemarks() As String
        Dim obj As New ConversionSummaryReportDO
        Dim role As PositionID = GetRoleID()
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        If Not IsNothing(HttpContext.Current.Session("ConversionSummaryReportParams")) Then
            obj = HttpContext.Current.Session("ConversionSummaryReportParams")
        End If
        Dim TableName As String = If({role.AppAdminM, role.AppAdminA, role.SH, role.GH, role.RO, role.TA}.Contains(currentUser.RoleID), "WeeklyRemarks2", If(currentUser.RoleID = role.BH, "WeeklyRemarks", String.Empty))
        Dim WeeklyRemarks As String = String.Empty
        If obj.ChkWeeklyRemarksExists(TableName, obj.Year, obj.Month, obj.WeekNumber, obj.Username) Then
            WeeklyRemarks = obj.GetWeeklyRemarks(TableName, obj.Year, obj.Month, obj.WeekNumber, obj.Username)
        Else
            WeeklyRemarks = ""
        End If
        Return WeeklyRemarks
    End Function

    'FUNCTION FOR BM AND REGION HEAD USERS
    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetAddWeeklyRemarks(ByVal Comment As String, ByVal isForAdmin As Boolean) As ConversionSummaryReportDO
        Dim obj As New ConversionSummaryReportDO
        Dim role As PositionID = GetRoleID()
        If Not IsNothing(HttpContext.Current.Session("ConversionSummaryReportParams")) Then
            obj = HttpContext.Current.Session("ConversionSummaryReportParams")
        End If
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim TableName As String = If({role.AppAdminM, role.AppAdminA, role.SH, role.GH, role.RO, role.TA}.Contains(currentUser.RoleID), If(isForAdmin, "WeeklyRemarks2", "All"), If(currentUser.RoleID = role.BH, "All", String.Empty))
        Dim TableNameToSelect As String = If({role.AppAdminM, role.AppAdminA, role.SH, role.GH, role.RO, role.TA}.Contains(currentUser.RoleID), "WeeklyRemarks2", If(currentUser.RoleID = role.BH, "WeeklyRemarks", String.Empty))
        Dim fontcolor As String = If({role.GH, role.RO, role.TA}.Contains(currentUser.RoleID), "<font color=""orange"">", If({role.AppAdminM, role.AppAdminA, role.SH}.Contains(currentUser.RoleID), "<font color=""#003399"">", "<font color=""#007700"">"))
        Dim NewWeeklyComments, clientRemarks, OldWeeklyRemarks As String

        If obj.ChkWeeklyRemarksExists(TableNameToSelect, obj.Year, obj.Month, obj.WeekNumber, obj.Username) Then
            If TableName = "All" Then
                If UpdateWeeklyRemarksForAll(obj, Comment, "WeeklyRemarks2", fontcolor, currentUser.DisplayName).isSuccess Then
                    Return UpdateWeeklyRemarksForAll(obj, Comment, "WeeklyRemarks", fontcolor, currentUser.DisplayName)
                Else
                    Return UpdateWeeklyRemarksForAll(obj, Comment, "WeeklyRemarks2", fontcolor, currentUser.DisplayName)
                End If
            Else
                OldWeeklyRemarks = obj.GetWeeklyRemarks(TableNameToSelect, obj.Year, obj.Month, obj.WeekNumber, obj.Username)
                NewWeeklyComments = If((Not Comment = Nothing), _
                                                        OldWeeklyRemarks & fontcolor & currentUser.DisplayName & " said on " _
                                                        & My.Computer.Clock.LocalTime.ToString & " : " & Comment & "</font><br>", _
                                                       OldWeeklyRemarks & "")
                clientRemarks = NewWeeklyComments
                NewWeeklyComments = clientRemarks.Replace(";", ".")
                Return obj.UpdateWeeklyRemarks(TableName, obj.Year, obj.Month, obj.WeekNumber, obj.Username, NewWeeklyComments)
            End If
        Else
            NewWeeklyComments = fontcolor & currentUser.DisplayName & " said on " & My.Computer.Clock.LocalTime.ToString & " : " & Comment & "</font><br>" & ""
            clientRemarks = NewWeeklyComments
            NewWeeklyComments = clientRemarks.Replace(";", ".")
            obj.isSuccess = obj.AddWeeklyRemarks(TableName, obj.Year, obj.Month, obj.WeekNumber, obj.Username, NewWeeklyComments)
        End If

        Return obj
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function UpdateWeeklyRemarksForAll(ByVal obj As ConversionSummaryReportDO,
                                               ByVal Comment As String,
                                               ByVal TableName As String,
                                               ByVal fontcolor As String,
                                               ByVal DisplayName As String) As ConversionSummaryReportDO
        Dim NewWeeklyComments, clientRemarks, OldWeeklyRemarks As String
        OldWeeklyRemarks = obj.GetWeeklyRemarks(TableName, obj.Year, obj.Month, obj.WeekNumber, obj.Username)
        NewWeeklyComments = If((Not Comment = Nothing), _
                                                OldWeeklyRemarks & fontcolor & DisplayName & " said on " _
                                                & My.Computer.Clock.LocalTime.ToString & " : " & Comment & "</font><br>", _
                                               OldWeeklyRemarks & "")
        clientRemarks = NewWeeklyComments
        NewWeeklyComments = clientRemarks.Replace(";", ".")
        If OldWeeklyRemarks = String.Empty Then
            obj.isSuccess = obj.AddWeeklyRemarks(TableName, obj.Year, obj.Month, obj.WeekNumber, obj.Username, NewWeeklyComments)
        Else
            Return obj.UpdateWeeklyRemarks(TableName, obj.Year, obj.Month, obj.WeekNumber, obj.Username, NewWeeklyComments)
        End If
        Return obj
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GenerateReport(ByVal tableName As String,
                                          ByVal branchName As String) As String
        Dim message As String = String.Empty
        Dim obj As ConversionSummaryReportDO = HttpContext.Current.Session("ConversionSummaryReportParams")
        If Not IsNothing(obj) Then
            obj.BranchName = branchName
            If obj.isValid() Then
                If tableName = "NewLeads" Then
                    HttpContext.Current.Session("ReportName") = "Weekly_NewLeads"
                ElseIf tableName = "PARevisit" Then
                    HttpContext.Current.Session("ReportName") = "Weekly_PARevisit"
                End If
                HttpContext.Current.Session("ReportParams") = obj

                'WriteActivityLog(ActivityLog.ActionTaken.GENERATE)
            Else
                message = obj.errMsg
            End If

        End If

        Return message
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function RedirectToUpdateClient(ByVal ClientID As String) As String
        Dim submenuID As String = HttpContext.Current.Session("SubMenuId")
        Dim url As String = "/Views/TargetMarket/UpdateClientDetails.aspx?submenuid=" + submenuID
        Dim access As ProfileDO = GetModuleAccess()
        If Not ClientID.Equals("") Then
            HttpContext.Current.Session("ConversionAccessMatrix") = access
            HttpContext.Current.Session("ClientID") = ClientID
        End If
        Return url
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
        Property WeekList As List(Of ReferencesForDropdown)
        Property currentDetails As New ConversionSummaryReportDO
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

    Class WeeklyDetails
        Property WeeklyList As List(Of ConversionSummaryReportDO)
        Property WeeklyListTotalDetails As ConversionSummaryReportDO
    End Class
End Class