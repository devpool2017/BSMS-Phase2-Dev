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

Public Class RelationshipOfficerDO
    Inherits MasterDO

#Region "INSTANTIATION"
    Sub New()
    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
#End Region
#Region "TABLE COLUMNS"
    <DataColumnMapping("FirstName")>
   <DisplayLabel("[FirstName]")>
    Property [FirstName] As String

    <DataColumnMapping("MiddleInitial")>
 <DisplayLabel("MiddleInitial")>
    Property MiddleInitial As String
    <DataColumnMapping("LastName")>
<DisplayLabel("LastName")>
    Property LastName As String
    <DataColumnMapping("Fullname")>
<DisplayLabel("Fullname")>
    Property Fullname As String
    <DataColumnMapping("BrCode")>
<DisplayLabel("BrCode")>
    Property BrCode As String
    <DataColumnMapping("RegBrCode")>
<DisplayLabel("RegBrCode")>
    Property RegBrCode As String
    <DataColumnMapping("Username")>
<DisplayLabel("Username")>
    Property Username As String

    <DataColumnMapping("CompleteRegion")>
  <DisplayLabel("CompleteRegion")>
    Property CompleteRegion As String

    <DataColumnMapping("RoleName")>
  <DisplayLabel("RoleName")>
    Property RoleName As String

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
#End Region

    Public Function ListRelationships(ByVal RoleID As String) As List(Of RelationshipOfficerDO)
        Dim dal As New RelationshipOfficerDA
        Dim dt As DataTable
        Dim list As New List(Of RelationshipOfficerDO)

        dt = dal.GetListGroupHeads(RoleID)

        For Each dtRow As DataRow In dt.Rows
            Dim DORelationshipList As New RelationshipOfficerDO With {
             .FirstName = dtRow.Item("FirstName"),
             .MiddleInitial = dtRow.Item("MiddleInitial"),
             .LastName = dtRow.Item("LastName"),
             .Fullname = dtRow.Item("Fullname"),
             .BrCode = dtRow.Item("BrCode"),
             .RegBrCode = dtRow.Item("RegBrCode"),
             .Username = dtRow.Item("Username")
            }

            list.Add(DORelationshipList)
        Next
        Return list
    End Function



End Class
