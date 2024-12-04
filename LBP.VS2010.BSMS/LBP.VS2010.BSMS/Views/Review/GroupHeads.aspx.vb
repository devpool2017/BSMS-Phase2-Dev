Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Configuration
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.DataObjects
Imports System.Configuration.ConfigurationManager


Public Class GroupHeads
    Inherits OptimizedPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.DataBind()
    End Sub



#Region "Get List Group Heads"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ListGroupHeads() As List(Of GroupsHeadsDO)
        Dim obj As New GroupsHeadsDO
        Return obj.ListGroupHeads(GetRoleID.GH)
    End Function
#End Region


#Region "FOR DROPDOWN"
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function GroupList() As List(Of ReferencesForDropdown)
        Dim obj As New GroupsHeadsDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return obj.GroupLists
    End Function
#End Region
#Region "Add New Group Head"
    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function AddNewGroups(ByVal obj As GroupsHeadsDO) As String
        Dim refobj As New GroupsHeadsDO

        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")

        refobj.UserID = currentUser.Username
        refobj.FirstName = obj.FirstName
        refobj.MiddleInitial = obj.MiddleInitial
        refobj.LastName = obj.LastName
        refobj.RegBrCode = obj.RegBrCode
        refobj.BrCode = obj.BrCode
        refobj.RoleName = obj.RoleName
        refobj.Username = obj.Username
        refobj.Fullname = obj.FirstName + " " + obj.MiddleInitial + " " + obj.LastName
        Dim message As String = String.Empty
        If Not refobj.ValidateSearch() Then
            message = refobj.errMsg
        Else
            If Not refobj.CheckIfUserExist(refobj.Username) Then
                message = "User ID already exists."
            Else
                If Not refobj.CheckIfGroupHeadExist(refobj.RoleName, refobj.RegBrCode) Then
                    message = "There is an existing region head for the selected group."
                Else
                    If Not refobj.SaveNewGroupHead(refobj) Then
                        refobj.errMsg = "Failed to save new Group Head."
                        message = refobj.errMsg
                    Else
                        message = ""
                    End If
                End If
            End If
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