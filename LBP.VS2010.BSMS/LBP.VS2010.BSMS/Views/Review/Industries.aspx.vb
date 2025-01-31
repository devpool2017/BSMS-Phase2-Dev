Imports System.Web.Services
Imports System.Web.Script.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages.Validation
Imports System.Configuration.ConfigurationManager
Imports LBP.VS2010.WebCryptor

Public Class Industries
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Industries"

        If Not Page.IsPostBack Then
            Page.DataBind()
        End If
    End Sub

    '#Region "Load Industries Gridview"
    '    <WebMethod(EnableSession:=True)>
    '<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    '    Public Shared Function GetIndustriesList() As List(Of IndustriesDO)
    '        Dim obj As New IndustriesDO
    '        Dim List As New List(Of IndustriesDO)
    '        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
    '        List = obj.ListIndustries()
    '        Return List
    '    End Function
    '#End Region

#Region "LOAD CONTENT"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function OnLoadData() As IndustriesOnLoad
        Dim dt As New DataTable
        Dim GroupHead As String = String.Empty
        Dim BranchHead As String = String.Empty
        Dim TechnicalAssistant As String = String.Empty

        Return New IndustriesOnLoad With {
            .IndustryTypeList = ReferencesForDropdown.convertToReferencesList(IndustriesDO.GetIndustryTypeList(), "IndustryType", "IndustryType", False),
            .CanInsert = HasAccess("CanInsert"),
            .CanUpdate = HasAccess("CanUpdate"),
            .CanApprove = HasAccess("CanApprove"),
            .CanDelete = HasAccess("CanDelete"),
            .GH = GetSection("Commons")("GroupHead").ToString,
            .BH = GetSection("Commons")("BM").ToString,
            .TA = GetSection("Commons")("TA").ToString
            }

    End Function
#End Region

#Region "GET/LIST"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetList(ByVal searchText As String, ByVal filterStatus As String) As List(Of IndustriesDO)
        WriteActivityLog(ActivityLog.ActionTaken.SEARCH)

        Dim params As New IndustriesDO
        params.SearchBy = IIf(String.IsNullOrWhiteSpace(searchText), Nothing, searchText)
        params.Status = IIf(String.IsNullOrWhiteSpace(filterStatus), Nothing, filterStatus)

        HttpContext.Current.Session("currentIndustryParameter") = params

        Return IndustriesDO.GetList(searchText, filterStatus, GetModuleAccess())
    End Function


    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetDetails(ByVal industryCode As String, ByVal action As String) As IndustriesDO

        WriteActivityLog(ActivityLog.ActionTaken.VIEW)
        Dim obj As New IndustriesDO
        obj = IndustriesDO.GetDetails(industryCode, action)
        Return obj
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetMergeDetails(ByVal industryCode As String) As List(Of IndustriesDO.IndustryResult)
        'WriteActivityLog(ActivityLog.ActionTaken.VIEW)

        Return IndustriesDO.GetMergeDetails(industryCode)
    End Function
#End Region

#Region "SAVE/UPDATE"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateIndustry(ByVal obj As IndustriesDO) As String
        Dim message As String = String.Empty
        Dim ldap As New LDAPAuthentication
        Dim _webConfig As New WebConfigCryptor

        If Not (obj.isValid()) Then
            message = obj.errMsg
        End If

        Return message
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SaveIndustry(ByVal obj As IndustriesDO) As String
        Dim message As String = String.Empty

        obj.LogonUser = HttpContext.Current.Session("LogonUser")

        If Not obj.SaveIndustry() Then
            message = obj.errMsg
        Else
            Dim actionType As ActivityLog.ActionTaken
            Select Case obj.Action
                Case "add"
                    actionType = ActivityLog.ActionTaken.CREATE
                Case "delete"
                    actionType = ActivityLog.ActionTaken.DELETE
                Case Else
                    actionType = ActivityLog.ActionTaken.UPDATE
            End Select

            WriteActivityLog(actionType)
        End If

        Return message
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ApproveIndustry(ByVal obj As IndustriesDO) As String
        Dim message As String = String.Empty

        obj.LogonUser = HttpContext.Current.Session("LogonUser")
        Dim dt As New DataTable
        Dim dt1 As New DataTable
        Dim status As String = ""
        If Not obj.ApproveIndustry() Then
            message = obj.errMsg
        Else
            WriteActivityLog(ActivityLog.ActionTaken.APPROVE)
        End If

        Return message
    End Function

#End Region


#Region "DELETE/RESET"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function RejectIndustry(ByVal obj As IndustriesDO) As String
        Dim message As String = String.Empty

        obj.LogonUser = HttpContext.Current.Session("LogonUser")

        If Not obj.RejectIndustry() Then
            message = obj.errMsg
        Else
            WriteActivityLog(ActivityLog.ActionTaken.REJECT)
        End If

        Return message
    End Function
#End Region

#Region "Report"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GenerateIndustryReport() As String
        Dim message As String = String.Empty

        HttpContext.Current.Session("ReportName") = "IndustryList_Report"

        Return message

    End Function
#End Region


    Class IndustriesOnLoad
        Property IndustryTypeList As List(Of ReferencesForDropdown)
        Property CanInsert As Boolean
        Property CanUpdate As Boolean
        Property CanApprove As Boolean
        Property CanDelete As Boolean

        Property GH As String
        Property TA As String
        Property BH As String

    End Class

End Class