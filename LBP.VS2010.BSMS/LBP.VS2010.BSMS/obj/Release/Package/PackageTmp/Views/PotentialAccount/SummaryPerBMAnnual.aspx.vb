Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Configuration
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.DataObjects
Imports System.Configuration.ConfigurationManager


Public Class SummaryPerBMAnnual
    Inherits OptimizedPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.DataBind()
    End Sub

#Region "Get Session Details"
    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SessionSaveSummaryDetails() As List(Of SummaryBMDO)
        Dim obj As New SummaryBMDO
        Dim List As New List(Of SummaryBMDO)
        Dim List1 As New List(Of SummaryBMDO)
        Dim message As String = String.Empty
        Dim dt As New DataTable
        List = HttpContext.Current.Session("SummaryPerBMDetails")
        Return List
    End Function
    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SummaryPerBMDetails() As List(Of SummaryBMDO)
        Dim obj As New SummaryBMDO
        Dim List As New List(Of SummaryBMDO)
        Dim message As String = String.Empty
        Dim dt As New DataTable
        Dim roleApplicID As String
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")

        dt = SummaryBMDO.listRecordsToTable(HttpContext.Current.Session("SummaryPerBMDetails"))
        obj.UploadBy = dt.Rows(0)(11).ToString
        obj.Fullname = dt.Rows(0)(0).ToString
        HttpContext.Current.Session("UploadBy") = obj.UploadBy
        HttpContext.Current.Session("Fullname") = obj.Fullname

        roleApplicID = IIf(currentUser.RoleID = GetRoleID.AppAdminA Or currentUser.RoleID = GetRoleID.AppAdminM Or currentUser.RoleID = GetRoleID.RO, currentUser.RoleID, "")

        List = obj.GetListSummaryPerBM(obj, GetModuleAccess(), roleApplicID)
        Return List
    End Function
#End Region

#Region "SearchSummary"
    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SearchSummaryPerBMDetails(ByVal refObj As SummaryBMDO) As List(Of SummaryBMDO)
        Dim obj As New SummaryBMDO
        obj = refObj
        Dim List As New List(Of SummaryBMDO)
        Dim message As String = String.Empty
        obj.UploadBy = HttpContext.Current.Session("UploadBy")

        Dim roleApplicID As String
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")

        roleApplicID = IIf(currentUser.RoleID = GetRoleID.AppAdminA Or currentUser.RoleID = GetRoleID.AppAdminM Or currentUser.RoleID = GetRoleID.RO, currentUser.RoleID, "")

        List = obj.GetListSummaryPerBM(obj, GetModuleAccess(), roleApplicID)
        Return List
    End Function
#End Region

#Region "Report"
    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GenerateReport(ByVal refObj As SummaryBMDO) As String
        Dim message As String = String.Empty
        Dim obj As New SummaryBMDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        obj = refObj
        obj.UploadBy = HttpContext.Current.Session("UploadBy")
        HttpContext.Current.Session("ReportName") = "SummaryPerBM"
        HttpContext.Current.Session("ReportParams") = obj
        Return message
    End Function
#End Region

#Region "SaveDetailsForCPARevisitsPage"
    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SaveSessionDetail(ByVal refObj As SummaryBMDO) As String
        Dim lst As New List(Of SummaryBMDO)

        lst.Add(
                     New SummaryBMDO With {
                     .ClientID = refObj.ClientID,
                     .Fullname = refObj.Fullname,
                     .DateEncoded = refObj.DateEncoded,
                     .Lead = refObj.Lead,
                     .Prospect = refObj.Prospect,
                     .Customer = refObj.Customer,
                     .CPAID = refObj.CPAID,
                     .Industry = refObj.Industry,
                     .Tag = "Y"
                     })
        HttpContext.Current.Session("CPAFullame") = refObj.Fullname
        HttpContext.Current.Session("CPAClientID") = refObj.ClientID

        Return ""
    End Function
#End Region

#Region "Delete CPA"
    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function DeleteCPA(ByVal refObj As SummaryBMDO) As String
        Dim message As String = String.Empty
        Dim obj As New SummaryBMDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        If Not obj.DeleteCPA(refObj.CPAID.Trim, currentUser.Username) Then
            message = obj.errMsg
        End If

        Return message
    End Function
#End Region

    Public Shared Function GetRoleID() As PositionID
        Return New PositionID With {
       .BH = GetSection("Commons")("BM").ToString,
       .RO = GetSection("Commons")("RO").ToString,
       .GH = GetSection("Commons")("GroupHead").ToString,
       .SH = GetSection("Commons")("SectorHead").ToString,
       .AppAdminA = GetSection("Commons")("AppAdminApprover").ToString,
       .AppAdminM = GetSection("Commons")("AppAdminMaker").ToString
       }
    End Function
    Class PositionID
        Property BH As String
        Property RO As String
        Property GH As String
        Property SH As String
        Property AppAdminA As String
        Property AppAdminM As String
    End Class


End Class