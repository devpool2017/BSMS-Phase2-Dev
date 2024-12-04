Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Configuration
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.DataObjects
Imports System.Configuration.ConfigurationManager

Public Class BranchManager
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.DataBind()
    End Sub

#Region "FOR DROPDOWN"
    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function OnLoadData() As FiltersOnLoad
        Dim obj As New BranchManagerDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim role As PositionID = GetRoleID()
        Dim username As String = IIf(currentUser.RoleID = role.BH, currentUser.Username, String.Empty)
        Dim groupCde As String = If({role.GH, role.RO, role.TA}.Contains(currentUser.RoleID), currentUser.GroupCode, String.Empty)
        obj.role = GetRoleID.BH
        obj.region = currentUser.GroupCode
        Return New FiltersOnLoad With {
           .currentUser = currentUser,
           .RegionList = ReferencesForDropdown.convertToReferencesList(BranchManagerDO.RegionLists(), "RegionCode", "RegionName"),
           .BranchManagerList = obj.ListBranchManagers(obj, GetModuleAccess()),
           .GroupCode = GetSection("Commons")("GroupHead").ToString() & "," & GetSection("Commons")("TA").ToString() & "," & GetSection("Commons")("RO").ToString(),
           .BMCode = GetSection("Commons")("BM").ToString(),
           .AdminCode = GetSection("Commons")("AppAdminMaker").ToString() & "," & GetSection("Commons")("AppAdminApprover").ToString() & "," & GetSection("Commons")("SectorHead").ToString()
        }
    End Function
  
    <WebMethod(EnableSession:=True)>
 <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetBranchPerRegion(ByVal refobj As BranchManagerDO) As List(Of ReferencesForDropdown)
        Dim obj As New BranchManagerDO

        Return obj.GetBranchPerRegionLists(refobj.region)
    End Function
#End Region


#Region "Load Bank Product Gridview"
    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetGroupHeadPerRegion(ByVal refobj As BranchManagerDO) As BranchManagerDO
        Dim obj As New BranchManagerDO

        Return obj.GetGroupHeadPerRegion(refobj)
    End Function

    <WebMethod(EnableSession:=True)>
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function BranchManagerList(ByVal refobj As BranchManagerDO) As List(Of BranchManagerDO)
        Dim obj As New BranchManagerDO
        Dim ListGrps As New List(Of BranchManagerDO)
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        obj.role = GetRoleID.BH
        ListGrps = obj.ListBranchManagers(obj, GetModuleAccess())
        Return ListGrps
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function BranchManagerListRegionBranch(ByVal refobj As BranchManagerDO) As List(Of BranchManagerDO)
        Dim obj As New BranchManagerDO
        Dim ListGrps As New List(Of BranchManagerDO)
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        ListGrps = obj.ListBMRegionBranch(refobj)
        Return ListGrps
    End Function
#End Region

    Class FiltersOnLoad
        Property currentUser As Object
        Property RegionList As List(Of ReferencesForDropdown)
        Property BranchesList As List(Of ReferencesForDropdown)
        Property BranchManagerList As List(Of BranchManagerDO)
        Property GroupCode As String
        Property BMCode As String
        Property AdminCode As String
    End Class


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