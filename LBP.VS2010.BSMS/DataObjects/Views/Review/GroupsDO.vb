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


Public Class GroupsDO
    Inherits MasterDO
#Region "INSTANTIATION"
    Sub New()
    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
#End Region
#Region "TABLE COLUMNS"
    <DataColumnMapping("RegionCode")>
   <DisplayLabel("RegionCode")>
    Property GroupCode As String

    <DataColumnMapping("RegionName")>
 <DisplayLabel("RegionName")>
    Property GroupName As String


    Property UserID As String
    Property lastIndex As String
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
    Public Function ValidateSearch() As Boolean
        Dim success As Boolean = True
        If String.IsNullOrEmpty(GroupCode) Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, "Group Code", Nothing) + "<br/>"
            success = False
        End If
        If String.IsNullOrEmpty(GroupName) Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, "Group Name", Nothing) + "<br/>"
            success = False
        End If

        Return success
    End Function

#End Region

#Region "List"
    'Public Function GroupList() As DataTable
    '    Dim dal As New GroupsDA
    '    Dim dt As DataTable
    '    Dim list As New List(Of GroupsDO)

    '    dt = dal.GetListGroup()

    '    Return dt
    'End Function
    Public Function GroupList(ByVal lastIndex As String, ByVal accessObj As ProfileDO) As List(Of GroupsDO)
        Dim dal As New GroupsDA
        Dim dt As DataTable
        Dim list As New List(Of GroupsDO)

        dt = dal.GetListGroup()
        If Not IsNothing(dt) Then
            Dim lst As New List(Of GroupsDO)
            If dt.Rows.Count > 0 Then
                Dim canUpdate As Boolean = accessObj.CanUpdate = "Y"
                Dim canApprove As Boolean = accessObj.CanApprove = "Y"
                Dim canInsert As Boolean = accessObj.CanInsert = "Y"
                Dim canDelete As Boolean = accessObj.CanDelete = "Y"
                Dim canPrint As Boolean = accessObj.CanPrint = "Y"
                Dim canView As Boolean = accessObj.CanView = "Y"

                For Each dtRow As DataRow In dt.Rows
                    Dim access As New ProfileDO With {
                        .CanView = accessObj.CanView,
                        .CanUpdate = accessObj.CanUpdate,
                        .CanInsert = accessObj.CanInsert,
                        .CanDelete = accessObj.CanDelete
                    }
                    lst.Add(
                        New GroupsDO With {
                        .Access = access,
                          .GroupCode = dtRow.Item("RegionCode"),
                          .GroupName = dtRow.Item("RegionName"),
                          .lastIndex = lastIndex
                        })
                Next
            End If
            Return lst
        End If
        Return Nothing
    End Function
#End Region

#Region "Check If exists"
    Public Function CheckIfGroupNameExist(ByVal GroupName As String) As Boolean
        Dim dal As New GroupsDA
        success = True

        If Not dal.CheckIfNameExist(GroupName) Then
            success = True
        Else
            success = False
        End If

        Return success

    End Function

    Public Function CheckIfGroupCodeExist(ByVal GroupCode As String) As Boolean
        Dim dal As New GroupsDA
        success = True

        If Not dal.CheckIfCodeExist(GroupCode) Then
            success = True
        Else
            success = False
        End If

        Return success

    End Function
#End Region

#Region "INSERT/UPDATE"
    Public Function SaveNewGroup(ByVal obj As GroupsDO) As Boolean
        Dim dal As New GroupsDA
        If dal.SaveGroup(obj.GroupCode, obj.GroupName, obj.UserID, False) Then
            success = True
        Else
            success = False
        End If
        Return success
    End Function


    Public Function UpdateNewGroup(ByVal obj As GroupsDO) As Boolean
        Dim dal As New GroupsDA
        If dal.UpdateGroup(obj.GroupCode, obj.GroupName, obj.UserID, False) Then
            success = True
        Else
            success = False
        End If
        Return success
    End Function
#End Region



End Class
