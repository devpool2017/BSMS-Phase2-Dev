Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Configuration
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.DataObjects
Imports System.Configuration.ConfigurationManager

Public Class SummaryPerBM
    Inherits OptimizedPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.DataBind()
    End Sub

#Region "FOR DROPDOWN"
    <WebMethod(EnableSession:=True)> _
  <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function OnLoadData() As FiltersOnLoad
        Dim obj As New SummaryBMDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim username As String = IIf(currentUser.RoleID = GetRoleID.BH, currentUser.Username, String.Empty)
        Dim groupCode As String = If({GetRoleID.GH, GetRoleID.RO, GetRoleID.BH, GetRoleID.TA}.Contains(currentUser.RoleID), currentUser.GroupCode, String.Empty)
        obj.region = groupCode
        obj.RoleID = GetRoleID.BH

        Return New FiltersOnLoad With {
           .currentUser = currentUser,
           .RegionList = ReferencesForDropdown.convertToReferencesList(ConversionSummaryReportDO.GetRegionList(), "RegionCode", "RegionName"),
           .UsersList = ReferencesForDropdown.convertToReferencesList(SummaryBMDO.BranchListPerRegion(obj.RoleID, ""), "UserID", "Fullname"),
           .BranchesList = ReferencesForDropdown.convertToReferencesList(SummaryBMDO.BranchListPerRegion(obj.RoleID, currentUser.GroupCode), "BranchCode", "BranchName"),
           .SummaryBMList = obj.GetListSummaryBM(obj, GetModuleAccess()),
           .GroupCode = GetRoleID.GH & "," & GetRoleID.TA & "," & GetRoleID.RO,
           .BHCode = GetRoleID.BH,
           .AdminCode = GetRoleID.AppAdminA & "," & GetRoleID.AppAdminM & "," & GetRoleID.SH
}
    End Function

    <WebMethod(EnableSession:=True)> _
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function BranchesListRegion(ByVal refObj As SummaryBMDO) As FiltersOnLoad
        Dim obj As New SummaryBMDO
        Dim list As New List(Of SummaryBMDO)
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return New FiltersOnLoad With {
         .BranchesList = ReferencesForDropdown.convertToReferencesList(SummaryBMDO.BranchListPerRegion(refObj.RoleID, refObj.RegionCode), "BranchCode", "BranchName")
}
    End Function
#End Region

#Region "Load Gridview"

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SearchBranchFilter(ByVal refobj As SummaryBMDO) As List(Of SummaryBMDO)
        Dim obj As New SummaryBMDO
        Dim List As New List(Of SummaryBMDO)
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        refobj.RoleID = GetRoleID.BH
        List = obj.GetSummaryPerBMListSearch(refobj, GetModuleAccess())
        Return List
    End Function
#End Region

#Region "Save Session"
    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function SummaryPerBMDetails(ByVal refobj As SummaryBMDO) As String
        Dim obj As New SummaryBMDO
        Dim List As New List(Of SummaryBMDO)
        Dim message As String = String.Empty

        obj.Fullname = refobj.Fullname
        obj.Branch = refobj.Branch
        obj.Position = refobj.Position
        obj.UploadBy = refobj.UploadBy
        List.Add(
                   New SummaryBMDO With {
                   .Fullname = obj.Fullname,
                   .Branch = obj.Branch,
                   .Position = obj.Position,
                   .UploadBy = obj.UploadBy
                   })

        HttpContext.Current.Session("SummaryPerBMDetails") = List

        Return message

    End Function
#End Region

    Class FiltersOnLoad
        Property currentUser As Object
        Property GroupCode As String
        Property AdminCode As String
        Property BHCode As String
        Property RegionList As List(Of ReferencesForDropdown)
        Property UsersList As List(Of ReferencesForDropdown)
        Property BranchesList As List(Of ReferencesForDropdown)
        Property YearList As List(Of ReferencesForDropdown)
        Property MonthList As List(Of ReferencesForDropdown)
        Property SummaryBMList As List(Of SummaryBMDO)
    End Class

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