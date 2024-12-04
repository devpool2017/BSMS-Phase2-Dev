Imports System.Web.Script.Services
Imports System.Web.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities

Public Class OtherParameters
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.DataBind()
    End Sub

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetOtherParameters() As List(Of OtherParametersDO)
        Return OtherParametersDO.GetOtherParameters()
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetGroup() As List(Of ReferencesForDropdown)
        Dim PotentialAccountsDO As New ChangeInPotentialAccountsDO

        Return ReferencesForDropdown.convertToReferencesList(PotentialAccountsDO.GetRegionList(), "RegionCode", "RegionName")
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetOtherParametersRH(ByVal RegionCode As String) As List(Of OtherParametersDO)
        Return OtherParametersDO.GetOtherParametersRH(RegionCode)
    End Function
End Class