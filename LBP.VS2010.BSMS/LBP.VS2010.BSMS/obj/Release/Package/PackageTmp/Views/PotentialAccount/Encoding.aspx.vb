Imports System.Web.Services
Imports System.Web.Script.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities
Imports System.Configuration.ConfigurationManager

Public Class Encoding
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Encoding"

        If Not Page.IsPostBack Then
            Page.DataBind()
        End If
    End Sub

#Region "LOAD CONTENT"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function CheckCPADateRange() As EncodingDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim obj As New EncodingDO
        If EncodingDO.CheckCPADateRange(currentUser.Username) Then
            obj.errorMessage = ""
        Else
            obj.errorMessage = "Encoding of Potential Accounts not allowed."

        End If
        Return obj
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function loadAccounts() As FiltersOnLoad
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim obj As New EncodingDO
        Return New FiltersOnLoad With {
       .currentUser = currentUser,
       .EncodingList = EncodingDO.GetAllCPAsByYearPerBM(currentUser.BranchCode, GetModuleAccess())
    }
    End Function


    '<WebMethod(EnableSession:=True)>
    '<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    'Public Shared Function loadAccounts(ByVal param As EncodingDO)
    '    param.isSuccess = True

    '    'Dim list As List(Of EncodingDO) = EncodingDO.loadAccounts(param.userName, GetModuleAccess())
    '    Dim list As List(Of EncodingDO) = EncodingDO.GetAllCPAsByYearPerBM(param.userName, GetModuleAccess())
    '    Return list
    'End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function viewAccount(ByVal ClientID As String) As EncodingDO
        Dim list As EncodingDO = EncodingDO.viewAccountDetails(ClientID)
        HttpContext.Current.Session("AccountDetails") = list
        Return list
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ddlIndustry() As List(Of ReferencesForDropdown)
        Dim dt As DataTable
        dt = EncodingDO.ddlIndustry()

        Return ReferencesForDropdown.convertToReferencesList(dt, "IndustryCode", "DescIndicator", False, "Please Select")

    End Function

#End Region

#Region "SAVE CONTENT"

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function validateSave(ByVal param As EncodingDO)
        Dim message As String = String.Empty

        If Not param.validateParams(param) Then
            message = param.errMsg
        End If

        Return message
    End Function

#End Region


    Class FiltersOnLoad
        Property currentUser As Object
        Property EncodingList As List(Of EncodingDO)
        Property BranchManager As Object
    End Class


End Class