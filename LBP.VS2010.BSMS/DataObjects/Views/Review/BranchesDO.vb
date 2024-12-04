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
Imports System.Web

Public Class BranchesDO
    Inherits MasterDO

#Region "INSTANTIATION"
    Sub New()
    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
#End Region

#Region "TABLE COLUMNS"
  
    <DataColumnMapping("BranchCode")>
<DisplayLabel("BranchCode")>
    Property BranchCode As String

    <DataColumnMapping("BranchName")>
<DisplayLabel("BranchName")>
    Property BranchName As String

    <DataColumnMapping("RegionCode")>
<DisplayLabel("RegionCode")>
    Property RegionCode As String

    <DataColumnMapping("RegionName")>
<DisplayLabel("RegionName")>
    Property RegionName As String

    Property SubMenuID As String
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
    Public Function ValidateSearch(ByVal obj As BranchesDO) As Boolean
        Dim success As Boolean = True
        If String.IsNullOrEmpty(obj.BranchCode) Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, "Branch Code", Nothing) + "<br/>"
            success = False
        End If
        If String.IsNullOrEmpty(obj.BranchName) Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, "Branch Name", Nothing) + "<br/>"
            success = False
        End If
        If String.IsNullOrEmpty(obj.RegionCode) Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, "Region Name", Nothing) + "<br/>"
            success = False
        End If
        Return success
    End Function
#End Region

#Region "Get Details"
    Shared Function GetRegionList() As DataTable
        Dim dal As New BranchesDA
        Return dal.GetRegionList()
    End Function

    Public Function GetListAllBranches(ByVal obj As BranchesDO, ByVal accessObj As ProfileDO) As List(Of BranchesDO)
        Dim dal As New BranchesDA
        Dim dt As DataTable
        Dim list As New List(Of BranchesDO)
        Dim user As LoginUser = HttpContext.Current.Session("currentUser")
        dt = dal.GetAllBranches(obj.RegionCode)

        If Not IsNothing(dt) Then
            Dim lst As New List(Of BranchesDO)
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
                        New BranchesDO With {
                        .Access = access,
                        .BranchCode = IIf(Convert.ToString(dtRow.Item("BranchCode")) = "", "", dtRow.Item("BranchCode")),
                        .BranchName = IIf(Convert.ToString(dtRow.Item("BranchName")) = "", "", dtRow.Item("BranchName")),
                        .RegionName = IIf(Convert.ToString(dtRow.Item("RegionName")) = "", "", dtRow.Item("RegionName")),
                        .RegionCode = IIf(Convert.ToString(dtRow.Item("RegionCode")) = "", "", dtRow.Item("RegionCode"))
                    })
                Next
            End If
            Return lst
        End If
        Return Nothing
    End Function

    Public Function ChkRegionCodeExists(ByVal RegionCode As String) As Boolean
        Dim dal As New BranchesDA
        Return dal.ChkRegionCodeExists(RegionCode)
    End Function

    Public Function ChkBranchCodeExists(ByVal BranchCode As String) As Boolean
        Dim dal As New BranchesDA
        Return dal.ChkBranchCodeExists(BranchCode)
    End Function

    Public Function ChkBranchNameExists(ByVal BranchName As String) As Boolean
        Dim dal As New BranchesDA
        Return dal.ChkBranchNameExists(BranchName)
    End Function

    Public Function AddNewBranch(ByVal RegionCode As String,
                                   ByVal BranchCode As String,
                                   ByVal BranchName As String,
                                   ByVal Username As String) As Boolean
        Dim dal As New BranchesDA
        Return dal.AddNewBranch(RegionCode, BranchCode, BranchName, Username)
    End Function


    Public Function UpdateBranch(ByVal RegionCode As String,
                               ByVal BranchCode As String,
                               ByVal BranchName As String,
                               ByVal Username As String) As Boolean
        Dim dal As New BranchesDA
        Return dal.UpdateBranch(RegionCode, BranchCode, BranchName, Username)
    End Function


    Public Function DeleteBranch(ByVal RegionCode As String,
                                ByVal BranchCode As String,
                                ByVal BranchName As String) As Boolean
        Dim dal As New BranchesDA
        Dim success As Boolean

        If Not dal.DeleteBranch(RegionCode, BranchCode, BranchName) Then
            errMsg = dal.ErrorMsg
            success = False
        End If
        Return success
    End Function
#End Region
 

End Class
