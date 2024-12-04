Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Configuration
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.DataObjects
Imports System.Configuration.ConfigurationManager

Public Class RelationshipOfficer
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.DataBind()
    End Sub

    <WebMethod(EnableSession:=True)>
      <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ListRelationships() As List(Of RelationshipOfficerDO)
        Dim obj As New RelationshipOfficerDO
        Return obj.ListRelationships(GetRoleID.RO)
    End Function


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