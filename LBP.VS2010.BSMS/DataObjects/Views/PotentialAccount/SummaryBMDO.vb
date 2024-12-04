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

Public Class SummaryBMDO
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

    <DataColumnMapping("Industry")>
 <DisplayLabel("Industry")>
    Property Industry As String

    <DataColumnMapping("DateEncoded")>
 <DisplayLabel("DateEncoded")>
    Property DateEncoded As String

    <DataColumnMapping("Lead")>
 <DisplayLabel("Lead")>
    Property Lead As String

    <DataColumnMapping("Prospect")>
 <DisplayLabel("Prospect")>
    Property Prospect As String

    <DataColumnMapping("Customer")>
 <DisplayLabel("Customer")>
    Property Customer As String

    <DataColumnMapping("ClientID")>
 <DisplayLabel("ClientID")>
    Property ClientID As String

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

    <DataColumnMapping("UploadBy")>
   <DisplayLabel("UploadBy")>
    Property UploadBy As String

    <DataColumnMapping("CPAID")>
   <DisplayLabel("CPAID")>
    Property CPAID As String


    Property UserID As String
    Property role As String
    Property region As String

    Property TotalLead As Integer
    Property TotalProspect As Integer
    Property TotalCustomer As Integer
    Property Tag As String
    Property RoleID As String
    Property BranchManagerName As String

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

    Public Function RegionList() As List(Of ReferencesForDropdown)
        Dim dal As New SummaryBMDA

        Return ReferencesForDropdown.convertToReferencesList(dal.RegionList("", ""), "Code", "Name", True)
    End Function

    Shared Function BranchListPerRegion(ByVal RoleID As String, ByVal RegionCode As String) As DataTable
        Dim dal As New SummaryBMDA
        Return dal.BranchListPerRegion(RoleID, RegionCode)
    End Function


    Public Function GetListSummaryBM(ByVal obj As SummaryBMDO, ByVal accessObj As ProfileDO) As List(Of SummaryBMDO)
        Dim dal As New SummaryBMDA
        Dim dt As DataTable
        Dim list As New List(Of SummaryBMDO)
        Dim user As LoginUser = HttpContext.Current.Session("currentUser")
        dt = dal.GetListSummaryBM(obj.RoleID, obj.region)

        If Not IsNothing(dt) Then
            Dim lst As New List(Of SummaryBMDO)
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
                        New SummaryBMDO With {
                        .Access = access,
                        .Fullname = IIf(dtRow.Item("Fullname").ToString = "", "", dtRow.Item("Fullname")),
                        .Branch = IIf(dtRow.Item("BranchName").ToString = "", "", dtRow.Item("BranchName")),
                        .Position = IIf(dtRow.Item("Position").ToString = "", "", dtRow.Item("Position")),
                        .RegionName = IIf(dtRow.Item("RegionName").ToString = "", "", dtRow.Item("RegionName")),
                        .UserID = IIf(dtRow.Item("UserID").ToString = "", "", dtRow.Item("UserID"))
                        })
                Next
            End If
            Return lst
        End If
        Return Nothing
    End Function



    Public Function GetListSummaryPerBM(ByVal obj As SummaryBMDO, ByVal accessObj As ProfileDO, ByVal roleID As String) As List(Of SummaryBMDO)
        Dim dal As New SummaryBMDA
        Dim dt As DataTable
        Dim list As New List(Of SummaryBMDO)
        Dim user As LoginUser = HttpContext.Current.Session("currentUser")
        dt = dal.GetListSummaryPerBM(obj.Lead, obj.Prospect, obj.Customer, obj.UploadBy)

        If Not IsNothing(dt) Then
            Dim lst As New List(Of SummaryBMDO)
            If dt.Rows.Count > 0 Then
                Dim canUpdate As Boolean = IIf(accessObj.CanUpdate = "Y", True, False)
                Dim canApprove As Boolean = IIf(accessObj.CanApprove = "Y", True, False)
                Dim canInsert As Boolean = IIf(accessObj.CanInsert = "Y", True, False)
                Dim canDelete As Boolean = IIf(accessObj.CanDelete = "Y", True, False)
                Dim canPrint As Boolean = IIf(accessObj.CanPrint = "Y", True, False)
                Dim canView As Boolean = IIf(accessObj.CanView = "Y", True, False)

                For Each dtRow As DataRow In dt.Rows
                    Dim access As New ProfileDO With {
                        .CanView = accessObj.CanView,
                        .CanUpdate = If((dtRow.Item("LeadTag").ToString.Contains("Y") And dtRow.Item("ProspectTag").ToString.Contains("Y") And roleID = ""), "Y", accessObj.CanUpdate),
                        .CanInsert = accessObj.CanInsert,
                        .CanDelete = If((dtRow.Item("LeadTag").ToString.Contains("N") And dtRow.Item("ProspectTag").ToString.Contains("N") And dtRow.Item("CustomerTag").ToString.Contains("N") And roleID = ""), "Y", accessObj.CanDelete)
                    }
                    lst.Add(
                        New SummaryBMDO With {
                        .Access = access,
                        .ClientID = IIf(dtRow.Item("ClientID").ToString = "", "", dtRow.Item("ClientID")),
                        .Fullname = IIf(dtRow.Item("Name").ToString = "", "", dtRow.Item("Name")),
                        .DateEncoded = IIf(dtRow.Item("DateEncoded").ToString = "", "", dtRow.Item("DateEncoded")),
                        .Lead = IIf(dtRow.Item("LeadTag").ToString = "", "", dtRow.Item("LeadTag")),
                        .Prospect = IIf(dtRow.Item("ProspectTag").ToString = "", "", dtRow.Item("ProspectTag")),
                        .Customer = IIf(dtRow.Item("CustomerTag").ToString = "", "", dtRow.Item("CustomerTag")),
                        .CPAID = IIf(dtRow.Item("CPAID").ToString = "", "", dtRow.Item("CPAID")),
                        .Industry = IIf(dtRow.Item("Industry").ToString = "", "", dtRow.Item("Industry")),
                        .BranchManagerName = IIf(dtRow.Item("DisplayName").ToString = "", "", dtRow.Item("DisplayName"))
})
                Next
            End If
            Return lst
        End If
        Return Nothing
    End Function

    Public Function GetSummaryPerBMList(ByVal obj As SummaryBMDO, ByVal accessObj As ProfileDO) As List(Of SummaryBMDO)
        Dim dal As New SummaryBMDA
        Dim dt As DataTable
        Dim list As New List(Of SummaryBMDO)
        Dim user As LoginUser = HttpContext.Current.Session("currentUser")
        dt = dal.GetListSummaryPerBM(obj.Lead, obj.Prospect, obj.Customer, obj.UploadBy)

        If Not IsNothing(dt) Then
            Dim lst As New List(Of SummaryBMDO)
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
                        .CanUpdate = If((dtRow.Item("LeadTag").ToString.Contains("Y") And dtRow.Item("ProspectTag").ToString.Contains("Y")), "Y", accessObj.CanUpdate),
                        .CanInsert = accessObj.CanInsert,
                        .CanDelete = If((dtRow.Item("LeadTag").ToString.Contains("N") And dtRow.Item("ProspectTag").ToString.Contains("N") And dtRow.Item("CustomerTag").ToString.Contains("N")), "Y", accessObj.CanDelete)
                    }

                    lst.Add(
                        New SummaryBMDO With {
                        .Access = access,
                        .ClientID = IIf(dtRow.Item("ClientID").ToString = "", "", dtRow.Item("ClientID")),
                        .Fullname = IIf(dtRow.Item("Name").ToString = "", "", dtRow.Item("Name")),
                        .DateEncoded = IIf(dtRow.Item("DateEncoded").ToString = "", "", dtRow.Item("DateEncoded")),
                        .Lead = IIf(dtRow.Item("LeadTag").ToString = "", "", dtRow.Item("LeadTag")),
                        .Prospect = IIf(dtRow.Item("ProspectTag").ToString = "", "", dtRow.Item("ProspectTag")),
                        .Customer = IIf(dtRow.Item("CustomerTag").ToString = "", "", dtRow.Item("CustomerTag")),
                        .CPAID = IIf(dtRow.Item("CPAID").ToString = "", "", dtRow.Item("CPAID")),
                        .Industry = IIf(dtRow.Item("Industry").ToString = "", "", dtRow.Item("Industry"))
                        })
                Next
            End If
            Return lst
        End If
        Return Nothing
    End Function

    Public Function GetSummaryPerBMListSearch(ByVal obj As SummaryBMDO, ByVal accessObj As ProfileDO) As List(Of SummaryBMDO)
        Dim dal As New SummaryBMDA
        Dim dt As DataTable
        Dim list As New List(Of SummaryBMDO)
        Dim user As LoginUser = HttpContext.Current.Session("currentUser")
        dt = dal.GetSummarySearch(obj.RoleID, obj.Branch, obj.RegionCode)

        If Not IsNothing(dt) Then
            Dim lst As New List(Of SummaryBMDO)
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
                        New SummaryBMDO With {
                        .Access = access,
                        .Fullname = IIf(dtRow.Item("Fullname").ToString = "", "", dtRow.Item("Fullname")),
                        .Branch = IIf(dtRow.Item("BranchName").ToString = "", "", dtRow.Item("BranchName")),
                        .Position = IIf(dtRow.Item("Position").ToString = "", "", dtRow.Item("Position")),
                        .RegionName = IIf(dtRow.Item("RegionName").ToString = "", "", dtRow.Item("RegionName")),
                        .UserID = IIf(dtRow.Item("Username").ToString = "", "", dtRow.Item("Username"))
                        })
                Next
            End If
            Return lst
        End If
        Return Nothing
    End Function
    Public Function GenerateSummaryPerBM(ByVal obj As SummaryBMDO) As DataTable
        Dim dt As DataTable
        Dim dal As New SummaryBMDA
        Dim objList As New List(Of SummaryBMDO)

        objList = HttpContext.Current.Session("SummaryPerBMDetails")
        dt = SummaryBMDO.listRecordsToTable(objList)
        obj.UploadBy = dt.Rows(0)(11).ToString 'Get the selected username
        dt = dal.GetListSummaryPerBM(obj.Lead, obj.Prospect, obj.Customer, obj.UploadBy)
        Return dt
    End Function

    Public Function DeleteCPA(ByVal CPAID As String, ByVal userID As String) As Boolean
        Dim success As Boolean = True
        Dim dal As New SummaryBMDA

        If Not dal.DeleteCPA(CPAID, userID) Then
            errMsg = dal.ErrorMsg
            success = False
        End If

        Return success
    End Function



End Class
