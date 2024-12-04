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

Public Class BranchManagerDO
    Inherits MasterDO
#Region "INSTANTIATION"
    Sub New()
    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
#End Region
#Region "TABLE COLUMNS"
    <DataColumnMapping("Fullname")>
   <DisplayLabel("Fullname")>
    Property Fullname As String

    <DataColumnMapping("NewCASATarget")>
<DisplayLabel("NewCASATarget")>
    Property NewCASATarget As String

    <DataColumnMapping("NewCBGTarget")>
<DisplayLabel("NewCBGTarget")>
    Property NewCBGTarget As String

    <DataColumnMapping("ExistingCASATarget")>
<DisplayLabel("ExistingCASATarget")>
    Property ExistingCASATarget As String

    <DataColumnMapping("ExistingCBGTarget")>
<DisplayLabel("ExistingCBGTarget")>
    Property ExistingCBGTarget As String

    <DataColumnMapping("Branch")>
<DisplayLabel("Branch")>
    Property Branch As String

    <DataColumnMapping("Position")>
<DisplayLabel("Position")>
    Property Position As String

    <DataColumnMapping("RegionName")>
<DisplayLabel("RegionName")>
    Property RegionName As String


    <DataColumnMapping("RegionCode")>
<DisplayLabel("RegionCode")>
    Property RegionCode As String

    <DataColumnMapping("UserID")>
<DisplayLabel("UserID")>
    Property UserID As String

    Property role As String
    Property region As String
    Property GroupHead As String
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
    Public Function ValidateSearch(ByVal obj As BranchManagerDO) As Boolean
        Dim success As Boolean = True
        'If String.IsNullOrEmpty(FirstName) Then
        '    errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, "First Name", Nothing) + "<br/>"
        '    success = False
        'End If
        If String.IsNullOrEmpty(obj.role) Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, "Product Type", Nothing) + "<br/>"
            success = False
        End If
        Return success
    End Function
#End Region

    Shared Function RegionLists() As DataTable
        Dim dal As New BranchManagerDA
        Return dal.GetRegionList()
    End Function

    'Public Function RegionLists() As List(Of ReferencesForDropdown)
    '    Dim dal As New BranchManagerDA
    '    Return ReferencesForDropdown.convertToReferencesList(dal.GetRegionList(), "RegionCode", "RegionName", True)
    'End Function
    'Shared Function GetBranchPerRegionLists(ByVal RegionCode As String) As DataTable
    '    Dim dal As New BranchManagerDA
    '    Return dal.GetBranchPerRegionLists(RegionCode)
    'End Function

    Public Function GetBranchPerRegionLists(ByVal RegionCode As String) As List(Of ReferencesForDropdown)
        Dim dal As New BranchManagerDA
        Return ReferencesForDropdown.convertToReferencesList(dal.GetBranchPerRegionLists(RegionCode), "BranchCode", "BrBranch", True)
    End Function

    Public Function GetGroupHeadPerRegion(ByVal refobj As BranchManagerDO) As BranchManagerDO
        Dim dal As New BranchManagerDA
        Dim dt As DataTable
        Dim obj As New BranchManagerDO
        dt = dal.GetGroupHeadPerRegion(refobj.region)
        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                For Each dtRow As DataRow In dt.Rows
                    obj.GroupHead = IIf(Convert.ToString(dtRow.Item("DisplayName")) = "", "", dtRow.Item("DisplayName"))
                Next
                Return obj
            End If
        End If

        Return Nothing
    End Function


    Public Function ListBranchManagers(ByVal obj As BranchManagerDO, ByVal accessObj As ProfileDO) As List(Of BranchManagerDO)
        Dim dal As New BranchManagerDA
        Dim dt As DataTable
        Dim list As New List(Of BranchManagerDO)
        Dim user As LoginUser = HttpContext.Current.Session("currentUser")

        dt = dal.GetListRegionBranchManager(obj.role, obj.region)

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
                    list.Add(
                        New BranchManagerDO With {
                         .Access = access,
                         .Fullname = dtRow.Item("Fullname"),
                         .NewCASATarget = dtRow.Item("NewCASATarget"),
                         .NewCBGTarget = dtRow.Item("NewCBGTarget"),
                         .ExistingCASATarget = dtRow.Item("ExistingCASATarget"),
                         .ExistingCBGTarget = dtRow.Item("ExistingCBGTarget"),
                         .Branch = dtRow.Item("BranchName"),
                         .Position = dtRow.Item("Position"),
                         .RegionName = dtRow.Item("RegionName"),
                         .UserID = dtRow.Item("UserID")
                    })
                Next
            End If
            Return list
        End If
        Return Nothing
    End Function



    Public Function ListBMRegionBranch(ByVal obj As BranchManagerDO) As List(Of BranchManagerDO)
        Dim dal As New BranchManagerDA
        Dim dt As DataTable
        Dim list As New List(Of BranchManagerDO)
        Dim user As LoginUser = HttpContext.Current.Session("currentUser")

        dt = dal.GetBMRegionBranch(obj.role, obj.region, obj.Branch)

        If Not IsNothing(dt) Then
            Dim lst As New List(Of BranchesDO)
            If dt.Rows.Count > 0 Then
                For Each dtRow As DataRow In dt.Rows
                    list.Add(
                        New BranchManagerDO With {
                         .Access = access,
                         .Fullname = dtRow.Item("Fullname"),
                         .NewCASATarget = dtRow.Item("NewCASATarget"),
                         .NewCBGTarget = dtRow.Item("NewCBGTarget"),
                         .ExistingCASATarget = dtRow.Item("ExistingCASATarget"),
                         .ExistingCBGTarget = dtRow.Item("ExistingCBGTarget"),
                         .Branch = dtRow.Item("BranchName"),
                         .Position = dtRow.Item("Position"),
                         .RegionName = dtRow.Item("RegionName"),
                         .UserID = dtRow.Item("UserID")
                    })
                Next
            End If
            Return list
        End If
        Return Nothing
    End Function
End Class
