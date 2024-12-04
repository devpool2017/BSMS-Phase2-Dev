Imports System.Linq
Imports System.IO
Imports System.Data
Imports System.ComponentModel
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.CustomValidators.LengthAndValueValidator
Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables
Imports System.Configuration.ConfigurationManager
Imports LBP.VS2010.BSMS.CustomValidators
Imports System.Configuration
Imports LBP.VS2010.BSMS.DataAccess

Public Class BankProductDO
    Inherits MasterDO
#Region "INSTANTIATION"
    Sub New()
    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
#End Region
#Region "TABLE COLUMNS"
    <DataColumnMapping("CODE")>
   <DisplayLabel("CODE")>
    Property CODE As String

    <DataColumnMapping("CASACodes")>
 <DisplayLabel("CASACodes")>
    Property CASACodes As String

    <DataColumnMapping("ProductType")>
<DisplayLabel("ProductType")>
    Property ProductType As String

    <DataColumnMapping("ShortName")>
<DisplayLabel("ShortName")>
    Property ShortName As String

    <DataColumnMapping("ProductCategory")>
<DisplayLabel("ProductCategory")>
    Property ProductCategory As String

    <DataColumnMapping("ProductGroupNo")>
<DisplayLabel("ProductGroupNo")>
    Property ProductGroupNo As String

    <DataColumnMapping("ProductGroup")>
<DisplayLabel("ProductGroup")>
    Property ProductGroup As String

    Property ProductTypes As String

    Property UserID As String
#End Region

#Region "MISC COLUMNS"
    <RequiredField()>
    Property SearchBy As String
    <RequiredField()>
    <DisplayLabel("Search Value")>
    Property SearchValue As String
#End Region

#Region "MISC VARIABLES"
    Property Search As String
    Property SearchBy1 As String
    Property SearchType As String
    Property errorMessage As String
    Property Action As String
    Property success As String
    Property message As String
    'Property isSuccess As Boolean = True
#End Region
#Region "VALIDATION"
    Public Function ValidateSearch(ByVal obj As BankProductDO) As Boolean
        Dim success As Boolean = True
        'If String.IsNullOrEmpty(FirstName) Then
        '    errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, "First Name", Nothing) + "<br/>"
        '    success = False
        'End If
        If String.IsNullOrEmpty(obj.ProductType) Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, "Product Type", Nothing) + "<br/>"
            success = False
        End If
        Return success
    End Function

#End Region

    Public Function ProductCategoryLists() As List(Of ReferencesForDropdown)
        Dim dal As New BankProductDA
        Return ReferencesForDropdown.convertToReferencesList(dal.ProductCategoryList(True), "ProductCategory", "ProductCategory", True)
    End Function

    Public Function ListBankProduct(ByVal ProductTypes As String) As List(Of BankProductDO)
        Dim dal As New BankProductDA
        Dim dt As DataTable
        Dim list As New List(Of BankProductDO)

        dt = dal.GetListBankProduct(ProductTypes)

        For Each dtRow As DataRow In dt.Rows
            Dim BankProductDO As New BankProductDO With {
             .CASACodes = dtRow.Item("CASACodes"),
             .ProductType = dtRow.Item("ProductType"),
             .ShortName = dtRow.Item("ShortName"),
             .ProductCategory = dtRow.Item("ProductCategory"),
             .ProductGroupNo = dtRow.Item("ProductGroupNo"),
             .ProductGroup = dtRow.Item("ProductGroup")
            }
            list.Add(BankProductDO)
        Next
        Return list
    End Function
End Class
