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
Public Class UpdateClientDO
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
    Property listUpdateClientDo As New List(Of UpdateClientDO)

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
    <RequiredField()>
    Property SearchBy As String
    <RequiredField()>
    <DisplayLabel("Search Value")>
    Property SearchValue As String


    <DataColumnMapping("SearchText")>
    Property SearchText As String

    <DataColumnMapping("Where1")>
    Property Where1 As String

    <DataColumnMapping("Where2")>
    Property Where2 As String

    <DataColumnMapping("Where3")>
    Property Where3 As String

    <DataColumnMapping("FilterText1")>
    Property FilterText1 As String

    <DataColumnMapping("FilterText2")>
    Property FilterText2 As String

    <DataColumnMapping("FilterText3")>
    Property FilterText3 As String

    <DataColumnMapping("SortBy1")>
    Property SortBy1 As String

    <DataColumnMapping("SortBy2")>
    Property SortBy2 As String

    <DataColumnMapping("SortBy3")>
    Property SortBy3 As String

    <DataColumnMapping("Order1")>
    Property Order1 As String

    <DataColumnMapping("Order2")>
    Property Order2 As String

    <DataColumnMapping("Order3")>
    Property Order3 As String

    <DataColumnMapping("Status1")>
    Property Status1 As String

    <DataColumnMapping("Status2")>
    Property Status2 As String

    <DataColumnMapping("Status3")>
    Property Status3 As String

    <DataColumnMapping("Status1B")>
    Property Status1B As String

    <DataColumnMapping("Status2B")>
    Property Status2B As String

    <DataColumnMapping("Status3B")>
    Property Status3B As String
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
        Dim dal As New UpdateClientDA
        Return ReferencesForDropdown.convertToReferencesList(dal.YearList(True), "Year", "Year", False)
    End Function

    Public Function ClientTypesLists() As List(Of ReferencesForDropdown)
        Dim dal As New UpdateClientDA
        Return ReferencesForDropdown.convertToReferencesList(dal.ClientTypesList(True), "ClientTypeCode", "ClientTypeName", False)
    End Function

    Public Function WeekMinMaxLists(ByVal year As String, ByVal month As String) As List(Of ReferencesForDropdown)
        Dim dal As New UpdateClientDA
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
        Dim dal As New UpdateClientDA
        Return ReferencesForDropdown.convertToReferencesList(dal.SouceLeadList(True), "LeadSourceCode", "LeadSourceDesc", True)
    End Function

    Public Function IndustryTypeLists() As List(Of ReferencesForDropdown)
        Dim dal As New UpdateClientDA
        Return ReferencesForDropdown.convertToReferencesList(dal.IndustryTypeList(True), "IndustryCode", "IndustryDesc", True)
    End Function

    Public Function LostReasonLists() As List(Of ReferencesForDropdown)
        Dim dal As New UpdateClientDA
        Return ReferencesForDropdown.convertToReferencesList(dal.LostReasonList(True), "ReasonDesc", "ReasonDesc", True)
    End Function

    Public Function GetWeekNum() As List(Of ReferencesForDropdown)
        Dim dal As New UpdateClientDA
        Return ReferencesForDropdown.convertToReferencesList(dal.GetWeekNum(), "Year", "MonthName", False, Nothing, Nothing, "WeekNumber")
    End Function
#End Region


#Region "Load Gridview"
 
    Public Function ListUpdateClients(ByVal obj As UpdateClientDO) As List(Of UpdateClientDO)
        Dim dal As New UpdateClientDA
        Dim dt As DataTable
        Dim list As New List(Of UpdateClientDO)

        dt = dal.GetListUpdateClient(obj.UploadedBy, obj.FromDate, obj.Todate, obj.Branch, obj.SearchText, obj.Where1, obj.Where2, obj.Where3, obj.FilterText1,
                                     obj.FilterText2, obj.FilterText3, obj.SortBy1, obj.SortBy2, obj.SortBy3, obj.Order1, obj.Order2, obj.Order3,
                                     obj.Status1, obj.Status2, obj.Status3, obj.Status1B, obj.Status2B, obj.Status3B)

        If Not IsNothing(dt) Then
            For Each dtRow As DataRow In dt.Rows
                Dim UpdateClientDO As New UpdateClientDO With {
                 .ClientID = IIf((dtRow.Item("ClientID").ToString <> ""), dtRow.Item("ClientID"), ""),
                 .Fullname = IIf((dtRow.Item("Fullname").ToString <> ""), dtRow.Item("Fullname"), ""),
                 .ClientType = IIf((dtRow.Item("ClientType").ToString <> ""), dtRow.Item("ClientType"), ""),
                 .Lead = IIf((dtRow.Item("Lead").ToString <> ""), dtRow.Item("Lead"), ""),
                 .Suspect = IIf(dtRow.Item("Suspect").ToString <> "", dtRow.Item("Suspect"), ""),
                 .Prospect = IIf(dtRow.Item("Prospect").ToString <> "", dtRow.Item("Prospect"), ""),
                 .Customer = IIf(dtRow.Item("Customer").ToString <> "", dtRow.Item("Customer"), ""),
                 .Lost = IIf(dtRow.Item("Lost").ToString <> "", dtRow.Item("Lost"), ""),
                 .Lname = IIf(dtRow.Item("Lname").ToString <> "", dtRow.Item("Lname"), ""),
                 .Fname = IIf(dtRow.Item("Fname").ToString <> "", dtRow.Item("Fname"), ""),
                 .Mname = IIf(dtRow.Item("Mname").ToString <> "", dtRow.Item("Mname"), "")
                }
                list.Add(UpdateClientDO)
            Next
        End If
        Return list
    End Function

    Public Function GetStatusB(ByVal status As String) As String
        Dim statusB As String = Nothing

        If Not IsNothing(status) Then
            If status.Equals("Lead") Then
                statusB = "Suspect"
            ElseIf status.Equals("Suspect") Then
                statusB = "Prospect"
            ElseIf status.Equals("Prospect") Then
                statusB = "Customer"
            ElseIf status.Equals("Customer") Then
                statusB = "Lost"
            ElseIf status.Equals("Lost") Then
                statusB = "NULL"
            End If
        End If

        Return statusB
    End Function

#End Region


#Region "Details"
    Public Function GetFromDate(ByVal obj As UpdateClientDO) As DataTable
        Dim dal As New UpdateClientDA
        Dim dt As DataTable
        dt = dal.GetFromDate(obj.Year, obj.MonthName, obj.WeekNum)
        Return dt
    End Function

    Public Function GetToDate(ByVal obj As UpdateClientDO) As DataTable
        Dim dal As New UpdateClientDA
        Dim dt As DataTable
        dt = dal.GetToDate(obj.Year, obj.MonthName, obj.WeekNum)
        Return dt
    End Function

    Public Function GetMinWeekNum(ByVal obj As UpdateClientDO) As String
        Dim dal As New UpdateClientDA
        Return dal.GetMinWeekNum(obj.Year, obj.MonthName)
    End Function

    Public Function GetMaxWeekNum(ByVal obj As UpdateClientDO) As String
        Dim dal As New UpdateClientDA
        Return dal.GetMaxWeekNum(obj.Year, obj.MonthName)
    End Function

#End Region
End Class
