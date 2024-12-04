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

<Serializable()>
Public Class SearchTargetMarketDO
    Inherits MasterDO
#Region "INSTANTIATION"
    Sub New()
    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
#End Region
#Region "TABLE COLUMNS"

    <DataColumnMapping("ClientID")>
    <DisplayLabel("ClientID")>
    Property ClientID As String

    <DataColumnMapping("Fullname")>
    <DisplayLabel("Fullname")>
    Property Fullname As String

    <DataColumnMapping("ClientType")>
    <DisplayLabel("ClientType")>
    Property ClientType As String

    <DataColumnMapping("Lead")>
    <DisplayLabel("Lead")>
    Property Lead As String

    <DataColumnMapping("Suspect")>
    <DisplayLabel("Suspect")>
    Property Suspect As String

    <DataColumnMapping("Prospect")>
    <DisplayLabel("Prospect")>
    Property Prospect As String

    <DataColumnMapping("Customer")>
    <DisplayLabel("Customer")>
    Property Customer As String

    <DataColumnMapping("Lost")>
    <DisplayLabel("Lost")>
    Property Lost As String

    <DataColumnMapping("Lname")>
    <DisplayLabel("Lname")>
    Property Lname As String

    <DataColumnMapping("Fname")>
    <DisplayLabel("Fname")>
    Property Fname As String

    <DataColumnMapping("Mname")>
    <DisplayLabel("Mname")>
    Property Mname As String

    Property Year As String
    Property MinWeek As String
    Property MaxWeek As String
    Property month As String
    Property ProductTypes As String
    Property UserID As String
    Property value As String
    Property description As String
    Property Address As String
    Property ContactNo As String
    Property Amount As String
    Property AmountOthers As String
    Property ADB As String

    Property OtherATypes As String
    Property UploadProc As String

    Property Remarks As String
    Property DateEncoded As String
    Property UploadedBy As String
    Property Visits As String
    Property AccountNumbers As String
    Property LeadSource As String
    Property IndustryType As String
    Property ProductsOffered As String
    Property LoanReported As String
    Property LoansAvailed As String
    Property CASATypes As String
    Property Reason As String
    Property ReasonDesc As String

    Property DescIndicator As String
    Property IndustryCode As String
    Property IndustryDesc As String
    Property IndustryFlag As String
    Property IndustryIndicator As String
    Property MonthName As String
    Property WeekNum As String
    Property FromDate As String
    Property Todate As String
    Property LoanCodes As String
    Property Status As String
    Property OtherATypeModalList As List(Of String)
    Property ProductsOfferedList As List(Of String)
    Property listSearchTargetMarketDO As New List(Of SearchTargetMarketDO)

    Property LoanShortName As String
    Property OriginalLoanAmount As String
    Property LoanAmountReported As String
    Property PNNumber As String
    Property LoanReleasedAmount As String
    Property DateReleased As String
    Property DateReported As String
    Property StaggeredDateReported As String
    Property Category As String
    Property Tag As String
    Property AvailedID As String
    Property SortBy As String
    Property AssDes As String
    Property Branch As String

#Region "Details"
    Property ProductGroup As String
    Property ProductGroupNo As String
    Property CASACodes As String
    Property CASAType As String
    Property CASAShortName As String
    Property ProductCategory As String
    Property OtherProductCode As String
    Property OtherProductType As String
    Property OtherShortName As String
    Property CODE As String

#End Region

#End Region

#Region "MISC COLUMNS"
    <DataColumnMapping("SearchCol")>
    Property SearchCol As String

    <DataColumnMapping("SearchText")>
    Property SearchText As String

    <DataColumnMapping("Where1")>
    Property Where1 As String

    <DataColumnMapping("Where2")>
    Property Where2 As String

    <DataColumnMapping("Where3")>
    Property Where3 As String

    <DataColumnMapping("Where3")>
    Property Where4 As String

    <DataColumnMapping("FilterText1")>
    Property FilterText1 As String

    <DataColumnMapping("FilterText2")>
    Property FilterText2 As String

    <DataColumnMapping("FilterText3")>
    Property FilterText3 As String

    <DataColumnMapping("FilterText4")>
    Property FilterText4 As String

    <DataColumnMapping("FilterCol1")>
    Property FilterCol1 As String

    <DataColumnMapping("FilterCol2")>
    Property FilterCol2 As String

    <DataColumnMapping("FilterCol3")>
    Property FilterCol3 As String

    <DataColumnMapping("FilterCol4")>
    Property FilterCol4 As String

    <DataColumnMapping("SortBy1")>
    Property SortBy1 As String

    <DataColumnMapping("SortBy2")>
    Property SortBy2 As String

    <DataColumnMapping("SortBy3")>
    Property SortBy3 As String

    <DataColumnMapping("SortBy3")>
    Property SortBy4 As String

    <DataColumnMapping("Order1")>
    Property Order1 As String

    <DataColumnMapping("Order2")>
    Property Order2 As String

    <DataColumnMapping("Order3")>
    Property Order3 As String

    <DataColumnMapping("Order3")>
    Property Order4 As String

    <DataColumnMapping("BranchName")>
    Property BranchName As String

    <DataColumnMapping("UserFullName")>
    Property UserFullName As String

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

#Region "Load Dropdown"
    Public Function YearLists() As List(Of ReferencesForDropdown)
        Dim dal As New SearchTargetMarketDAL
        Return ReferencesForDropdown.convertToReferencesList(dal.YearList(True), "Year", "Year", False)
    End Function

    Public Function ClientTypesLists() As List(Of ReferencesForDropdown)
        Dim dal As New SearchTargetMarketDAL
        Return ReferencesForDropdown.convertToReferencesList(dal.ClientTypesList(True), "ClientTypeCode", "ClientTypeName", False)
    End Function

    Public Function WeekMinMaxLists(ByVal year As String, ByVal month As String) As List(Of ReferencesForDropdown)
        Dim dal As New SearchTargetMarketDAL
        Dim dt As New DataTable
        Dim listRfd As New List(Of ReferencesForDropdown)
        Dim intnum As Integer
        dt = dal.WeekMinMaxList(year, month, True)
        intnum = Convert.ToInt64(dt(0)(0).ToString())

        listRfd.Add(New ReferencesForDropdown With {
                .value = "",
                .description = "None",
                .data = ""
            })

        For index As Integer = 1 To intnum
            listRfd.Add(New ReferencesForDropdown With {
                .value = index.ToString,
                .description = "Week " + index.ToString,
                .data = ""
            })
        Next
        Return listRfd
    End Function

    Public Function SouceLeadLists() As List(Of ReferencesForDropdown)
        Dim dal As New SearchTargetMarketDAL
        Return ReferencesForDropdown.convertToReferencesList(dal.SouceLeadList(True), "LeadSourceCode", "LeadSourceDesc", True)
    End Function

    Public Function IndustryTypeLists() As List(Of ReferencesForDropdown)
        Dim dal As New SearchTargetMarketDAL
        Return ReferencesForDropdown.convertToReferencesList(dal.IndustryTypeList(True), "IndustryCode", "IndustryDesc", True)
    End Function

    Public Function LostReasonLists() As List(Of ReferencesForDropdown)
        Dim dal As New SearchTargetMarketDAL
        Return ReferencesForDropdown.convertToReferencesList(dal.LostReasonList(True), "ReasonDesc", "ReasonDesc", True)
    End Function

    Public Function RegionList() As List(Of ReferencesForDropdown)
        Dim dal As New SearchTargetMarketDAL
        Return ReferencesForDropdown.convertToReferencesList(dal.RegionList(True), "RegionCode", "RegionName", False, Nothing, True)
    End Function

    Public Function BranchList(ByVal region As String) As List(Of ReferencesForDropdown)
        Dim dal As New ChangeInPotentialAccountsDAL
        Return ReferencesForDropdown.convertToReferencesList(dal.GetBranchesList(region), "BranchCode", "BranchName", False)
    End Function

    Public Function GetWeekNum() As List(Of ReferencesForDropdown)
        Dim dal As New SearchTargetMarketDAL
        Return ReferencesForDropdown.convertToReferencesList(dal.GetWeekNum(), "Year", "MonthName", False, Nothing, Nothing, "WeekNumber")
    End Function
#End Region


#Region "Load Gridview"
    Public Function ListSearchClients(ByVal obj As SearchTargetMarketDO) As List(Of SearchTargetMarketDO)
        Dim dal As New SearchTargetMarketDAL
        Dim dt As DataTable
        Dim list As New List(Of SearchTargetMarketDO)

        dt = dal.SearchClientList(obj.FromDate, obj.Todate, obj.SearchCol, obj.SearchText, obj.Where1, obj.Where2, obj.Where3, obj.Where4,
                                  obj.FilterCol1, obj.FilterCol2, obj.FilterCol3, obj.FilterCol4, obj.FilterText1, obj.FilterText2,
                                  obj.FilterText3, obj.FilterText4, obj.SortBy1, obj.SortBy2, obj.SortBy3, obj.SortBy4,
                                  obj.Order1, obj.Order2, obj.Order3, obj.Order4)

        If Not IsNothing(dt) Then
            For Each dtRow As DataRow In dt.Rows
                Dim SearchTargetMarketDO As New SearchTargetMarketDO With {
                 .UploadedBy = IIf((dtRow.Item("UploadedBy").ToString <> ""), dtRow.Item("UploadedBy"), ""),
                 .ClientID = IIf((dtRow.Item("ClientID").ToString <> ""), dtRow.Item("ClientID"), ""),
                 .Fullname = IIf((dtRow.Item("Fullname").ToString <> ""), dtRow.Item("Fullname"), ""),
                 .ClientType = IIf((dtRow.Item("ClientType").ToString <> ""), dtRow.Item("ClientType"), ""),
                 .Lead = IIf((dtRow.Item("Lead").ToString <> ""), dtRow.Item("Lead"), ""),
                 .Suspect = IIf(dtRow.Item("Suspect").ToString <> "", dtRow.Item("Suspect"), ""),
                 .Prospect = IIf(dtRow.Item("Prospect").ToString <> "", dtRow.Item("Prospect"), ""),
                 .Customer = IIf(dtRow.Item("Customer").ToString <> "", dtRow.Item("Customer"), ""),
                 .Lost = IIf(dtRow.Item("Lost").ToString <> "", dtRow.Item("Lost"), ""),
                 .BranchName = IIf(dtRow.Item("BranchName").ToString <> "", dtRow.Item("BranchName"), ""),
                 .UserFullName = IIf(dtRow.Item("UserFullName").ToString <> "", dtRow.Item("UserFullName"), "")
                }
                list.Add(SearchTargetMarketDO)
            Next
        End If
        Return list
    End Function

    Public Function GetMessage(ByVal searchCount As Integer, ByVal maxSearch As Integer) As String
        Dim message As String = String.Empty

        If searchCount = 0 Then
            message += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.SEARCH_NO_RESULT, Nothing, {Nothing})
        ElseIf searchCount <= maxSearch Then
            message += Messages.SuccessMessages.Validation.getSuccessMessage(Messages.SuccessMessages.Validation.SuccessType.SEARCH, searchCount, {Nothing})
        Else
            message += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.SEARCH_ERROR, maxSearch, {Nothing})
        End If

        Return message
    End Function

#End Region


#Region "Details"
    Public Function GetFromDate(ByVal obj As SearchTargetMarketDO) As DataTable
        Dim dal As New SearchTargetMarketDAL
        Dim dt As DataTable
        dt = dal.GetFromDate(obj.Year, obj.MonthName, obj.WeekNum)
        Return dt
    End Function

    Public Function GetToDate(ByVal obj As SearchTargetMarketDO) As DataTable
        Dim dal As New SearchTargetMarketDAL
        Dim dt As DataTable
        dt = dal.GetToDate(obj.Year, obj.MonthName, obj.WeekNum)
        Return dt
    End Function

    Public Function GetMinWeekNum(ByVal obj As SearchTargetMarketDO) As String
        Dim dal As New SearchTargetMarketDAL
        Return dal.GetMinWeekNum(obj.Year, obj.MonthName)
    End Function

    Public Function GetMaxWeekNum(ByVal obj As SearchTargetMarketDO) As String
        Dim dal As New SearchTargetMarketDAL
        Return dal.GetMaxWeekNum(obj.Year, obj.MonthName)
    End Function
#End Region
End Class
