Imports System.Configuration.ConfigurationManager
Imports System.Linq
Imports System.IO
Imports System.Data
Imports System.ComponentModel
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.CustomValidators.LengthAndValueValidator
Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables
Imports LBP.VS2010.BSMS.CustomValidators
Imports System.Configuration
Imports LBP.VS2010.BSMS.DataAccess

Public Class IndustriesDO
    Inherits MasterDO
#Region "INSTANTIATION"
    Sub New()
    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
#End Region

#Region "TABLE COLUMNS"
    <DataColumnMapping("DescIndicator")>
   <DisplayLabel("DescIndicator")>
    Property DescIndicator As String

    <DataColumnMapping("IndustryCode")>
 <DisplayLabel("IndustryCode")>
    Property IndustryCode As String

    <DataColumnMapping("IndustryDesc")>
<DisplayLabel("IndustryDesc")>
    Property IndustryDesc As String

    <DataColumnMapping("IndustryFlag")>
<DisplayLabel("IndustryFlag")>
    Property IndustryFlag As String

    <DataColumnMapping("IndustryType")>
<DisplayLabel("IndustryType")>
    Property IndustryType As String

    <DataColumnMapping("IndustryIndicator")>
<DisplayLabel("IndustryIndicator")>
    Property IndustryIndicator As String

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


    Public Function ListIndustries() As List(Of IndustriesDO)
        Dim dal As New IndustriesDA
        Dim dt As DataTable
        Dim list As New List(Of IndustriesDO)

        dt = dal.IndustriesList()

        For Each dtRow As DataRow In dt.Rows
            Dim IndustriesDO As New IndustriesDO With {
             .DescIndicator = dtRow.Item("DescIndicator"),
             .IndustryCode = dtRow.Item("IndustryCode"),
             .IndustryDesc = dtRow.Item("IndustryDesc"),
             .IndustryType = dtRow.Item("IndustryType"),
             .IndustryFlag = dtRow.Item("IndustryFlag"),
             .IndustryIndicator = dtRow.Item("IndustryIndicator")
            }
            list.Add(IndustriesDO)
        Next
        Return list
    End Function
End Class
