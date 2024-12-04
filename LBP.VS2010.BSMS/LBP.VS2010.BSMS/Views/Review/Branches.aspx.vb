Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Configuration
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.DataObjects
Imports System.Configuration.ConfigurationManager


Public Class Branches
    Inherits OptimizedPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.DataBind()
    End Sub

#Region "FOR DROPDOWN"

#End Region

#Region "For Gridview"
    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function OnLoadData() As FiltersOnLoad
        Dim obj As New BranchesDO
        Dim role As PositionID = GetRoleID()
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim username As String = IIf(currentUser.RoleID = role.BH, currentUser.Username, String.Empty)
        Dim grpCode As String = If({role.GH, role.RO, role.TA}.Contains(currentUser.RoleID), currentUser.GroupCode, String.Empty)
        obj.RegionCode = grpCode

        Return New FiltersOnLoad With {
           .currentUser = currentUser,
           .RegionList = ReferencesForDropdown.convertToReferencesList(BranchesDO.GetRegionList(), "RegionCode", "RegionName"),
           .AllBranchList = obj.GetListAllBranches(obj, GetModuleAccess()),
           .GroupCode = GetSection("Commons")("GroupHead").ToString() & "," & GetSection("Commons")("TA").ToString() & "," & GetSection("Commons")("RO").ToString(),
           .BMCode = GetSection("Commons")("BM").ToString(),
           .AdminCode = GetSection("Commons")("AppAdminMaker").ToString() & "," & GetSection("Commons")("AppAdminApprover").ToString() & "," & GetSection("Commons")("SectorHead").ToString()
        }
    End Function

    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function SearchBranchPerReg(ByVal refObj As BranchesDO) As FiltersOnLoad
        Dim obj As New BranchesDO
        Dim role As PositionID = GetRoleID()
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim username As String = IIf(currentUser.RoleID = role.BH, currentUser.Username, String.Empty)
        obj.RegionCode = refObj.RegionCode

        Return New FiltersOnLoad With {
           .currentUser = currentUser,
           .RegionList = ReferencesForDropdown.convertToReferencesList(BranchesDO.GetRegionList(), "RegionCode", "RegionName"),
           .AllBranchList = obj.GetListAllBranches(obj, GetModuleAccess()),
           .GroupCode = GetSection("Commons")("GroupHead").ToString() & "," & GetSection("Commons")("TA").ToString() & "," & GetSection("Commons")("RO").ToString(),
           .BMCode = GetSection("Commons")("BM").ToString(),
           .AdminCode = GetSection("Commons")("AppAdminMaker").ToString() & "," & GetSection("Commons")("AppAdminApprover").ToString() & "," & GetSection("Commons")("SectorHead").ToString()
        }
    End Function
#End Region

    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function AddUpdateBranch(ByVal refObj As BranchesDO) As BranchesDO
        Dim obj As New BranchesDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        obj = refObj
        If obj.ValidateSearch(obj) Then


            If obj.message = "For Add" Then
                If Not obj.ChkBranchCodeExists(obj.BranchCode) Then
                    If Not obj.ChkBranchNameExists(obj.BranchName) Then
                        obj.success = True
                        If obj.message = "For Add" Then
                            If obj.AddNewBranch(obj.RegionCode, obj.BranchCode, obj.BranchName, currentUser.Username) Then
                                obj.success = True
                                obj.errMsg = ""
                            End If
                        Else
                            obj.errMsg = "Branch Name already exists."
                        End If
                    Else
                        obj.errMsg = "Branch Code already exists."
                    End If
                End If
            ElseIf obj.message = "For Update" Then
                If Not obj.ChkBranchNameExists(obj.BranchName) Then
                    obj.success = True
                    If obj.UpdateBranch(obj.RegionCode, obj.BranchCode, obj.BranchName, currentUser.Username) Then
                        obj.success = True
                        obj.errMsg = ""
                    End If
                Else
                    obj.errMsg = "Branch Name already exists."
                End If
            End If
        Else
            obj.errMsg = obj.errMsg
        End If


        Return obj
    End Function

    <WebMethod(EnableSession:=True)> _
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function DeleteBranches(ByVal refObj As BranchesDO) As BranchesDO
        Dim obj As New BranchesDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        obj = refObj

        If Not obj.DeleteBranch(obj.RegionCode, obj.BranchCode, obj.BranchName) Then
            obj.message = obj.errMsg
        End If
        Return obj
    End Function

    Class FiltersOnLoad
        Property currentUser As Object
        Property RegionList As List(Of ReferencesForDropdown)
        Property BranchesList As List(Of ReferencesForDropdown)
        Property AllBranchList As List(Of BranchesDO)
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