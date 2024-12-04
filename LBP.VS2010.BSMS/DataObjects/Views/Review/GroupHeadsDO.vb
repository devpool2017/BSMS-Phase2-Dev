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


Public Class GroupsHeadsDO
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
#Region "VALIDATION"
    Public Function ValidateSearch() As Boolean
        Dim success As Boolean = True
        If String.IsNullOrEmpty(FirstName) Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, "First Name", Nothing) + "<br/>"
            success = False
        End If
        If String.IsNullOrEmpty(LastName) Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, "Last Name", Nothing) + "<br/>"
            success = False
        End If
        If String.IsNullOrEmpty(RegBrCode) Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, "Group Name", Nothing) + "<br/>"
            success = False
        End If
        If String.IsNullOrEmpty(Username) Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, "User Name", Nothing) + "<br/>"
            success = False
        End If

        Return success
    End Function

#End Region

#Region "List"
    Public Function ListGroupHeads(ByVal RoleID As String) As List(Of GroupsHeadsDO)
        Dim dal As New GroupHeadsDA
        Dim dt As DataTable
        Dim list As New List(Of GroupsHeadsDO)

        dt = dal.GetListGroupHeads(RoleID)

        For Each dtRow As DataRow In dt.Rows
            Dim DOGroupList As New GroupsHeadsDO With {
             .FirstName = dtRow.Item("FirstName"),
             .MiddleInitial = dtRow.Item("MiddleInitial"),
             .LastName = dtRow.Item("LastName"),
             .Fullname = dtRow.Item("Fullname"),
             .BrCode = dtRow.Item("BrCode"),
             .RegBrCode = dtRow.Item("RegBrCode"),
             .Username = dtRow.Item("Username")
            }

            list.Add(DOGroupList)
        Next
        Return list
    End Function

    Public Function GroupLists() As List(Of ReferencesForDropdown)
        Dim dal As New GroupHeadsDA
        Return ReferencesForDropdown.convertToReferencesList(dal.GroupList(True), "RegionCode", "CompleteRegion", True)
    End Function
#End Region

#Region "Check If exists"
    Public Function CheckIfUserExist(ByVal userName As String) As Boolean
        Dim dal As New GroupHeadsDA
        success = True

        If Not dal.CheckIfUserNameExist(userName) Then
            success = True
        Else
            success = False
        End If

        Return success

    End Function

    Public Function CheckIfGroupHeadExist(ByVal RoleName As String, ByVal RegBrCode As String) As Boolean
        Dim dal As New GroupHeadsDA
        success = True

        If Not dal.CheckIfHeadGroupExist(RoleName, RegBrCode) Then
            success = True
        Else
            success = False
        End If

        Return success

    End Function
#End Region

#Region "INSERT/UPDATE"
    Public Function SaveNewGroupHead(ByVal obj As GroupsHeadsDO) As Boolean
        Dim dal As New GroupHeadsDA
        If dal.SaveGroupHead(obj.FirstName, obj.MiddleInitial, obj.LastName, obj.Fullname, obj.RegBrCode, obj.BrCode, obj.RoleName, obj.Username, obj.UserID, False) Then
            success = True
        Else
            success = False
        End If
        Return success
    End Function


    Public Function UpdateNewGroup(ByVal obj As GroupsDO) As Boolean
        Dim dal As New GroupHeadsDA
        If dal.UpdateGroup(obj.GroupCode, obj.GroupName, obj.UserID, False) Then
            success = True
        Else
            success = False
        End If
        Return success
    End Function

    Public Function DeleteGroupHead(ByVal obj As GroupsDO) As Boolean
        Dim dal As New GroupHeadsDA
        If dal.UpdateGroup(obj.GroupCode, obj.GroupName, obj.UserID, False) Then
            success = True
        Else
            success = False
        End If
        Return success
    End Function
#End Region




End Class
