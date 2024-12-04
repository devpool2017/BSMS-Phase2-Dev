Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Configuration
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.DataObjects
Imports System.Configuration.ConfigurationManager


Public Class BankProduct
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.DataBind()
    End Sub

#Region "FOR DROPDOWN"
    <WebMethod(EnableSession:=True)> _
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)> _
    Public Shared Function ProductCategoryList() As List(Of ReferencesForDropdown)
        Dim obj As New BankProductDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return obj.ProductCategoryLists
    End Function
#End Region

#Region "ValidateSearch"

    <WebMethod(EnableSession:=True)>
  <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateSearch(ByVal refobj As BankProductDO) As String
        Dim message As String = String.Empty
        Dim obj As New BankProductDO
        obj.SearchBy = refobj.SearchBy
        obj.SearchValue = refobj.SearchValue
        If Not obj.ValidateSearch(refobj) Then
            message = obj.errMsg
        End If

        Return message
    End Function
#End Region


#Region "Load Bank Product Gridview"
    <WebMethod(EnableSession:=True)>
 <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function LoadAllBankProducts() As List(Of BankProductDO)
        Dim obj As New BankProductDO
        Dim ListGrps As New List(Of BankProductDO)
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        ListGrps = obj.ListBankProduct("All")
        Return ListGrps
    End Function
    <WebMethod(EnableSession:=True)>
  <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function LoadBankProducts(ByVal refobj As BankProductDO) As List(Of BankProductDO)
        Dim obj As New BankProductDO
        Dim ListGrps As New List(Of BankProductDO)
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        ListGrps = obj.ListBankProduct(refobj.ProductType)
        Return ListGrps
    End Function
#End Region
End Class