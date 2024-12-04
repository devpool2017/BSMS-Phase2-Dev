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
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages

Public Class DatesDO
    Inherits MasterDO
#Region "INSTANTIATION"
    Sub New()
    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
#End Region

#Region "TABLE COLUMNS"
    <DataColumnMapping("Year")>
   <DisplayLabel("Year")>
    Property Year As String

    <DataColumnMapping("Month")>
 <DisplayLabel("Month")>
    Property Month As String

    <DataColumnMapping("Week")>
<DisplayLabel("Week")>
    Property Week As String

    <DataColumnMapping("FromDate")>
<DisplayLabel("FromDate")>
    Property FromDate As String

    <DataColumnMapping("ToDate")>
<DisplayLabel("ToDate")>
    Property ToDate As String
    Property UserID As String

    Property list As New List(Of DatesDO)
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

    Function ValidateField() As Boolean
        Dim isValid As Boolean = True
        Try
            If String.IsNullOrEmpty(Year) Then
                errMsg += Validation.getErrorMessage(Validation.ErrorType.EMPTY, "Year", Nothing) + "<br/>"
                isValid = False
            End If

            If String.IsNullOrEmpty(Month) Then
                errMsg += Validation.getErrorMessage(Validation.ErrorType.EMPTY, "Month", Nothing) + "<br/>"
                isValid = False
            End If

            If String.IsNullOrEmpty(Week) Then
                errMsg += Validation.getErrorMessage(Validation.ErrorType.EMPTY, "Week", Nothing) + "<br/>"
                isValid = False
            End If
        Catch ex As Exception
            isValid = False
            errMsg = ex.ToString()
        End Try

        Return isValid

    End Function


    Public Function DatesLists() As List(Of ReferencesForDropdown)
        Dim dal As New DatesDA
        Return ReferencesForDropdown.convertToReferencesList(dal.DateList(True), "Year", "Year", True)
    End Function


    Public Function YearList() As List(Of ReferencesForDropdown)
        Dim dal As New DatesDA
        Return ReferencesForDropdown.convertToReferencesList(dal.DateList(True), "Year", "Year", True)
    End Function

    Public Function YearMonthWeekListAll() As List(Of DatesDO)
        Dim dal As New DatesDA
        Dim list As New List(Of DatesDO)
        Dim dt As New DataTable

        dt = dal.YearMonthWeekListAll()
        For Each dtRow As DataRow In dt.Rows
            Dim DatesDO As New DatesDO With {
              .Year = IIf((dtRow.Item("Year").ToString <> ""), dtRow.Item("Year"), ""),
              .Month = IIf((dtRow.Item("MonthName").ToString <> ""), dtRow.Item("MonthName"), ""),
              .Week = IIf((dtRow.Item("WeekNumber").ToString <> ""), dtRow.Item("WeekNumber"), ""),
              .FromDate = IIf(dtRow.Item("Fromdate").ToString <> "", dtRow.Item("Fromdate"), ""),
              .ToDate = IIf(dtRow.Item("ToDate").ToString <> "", dtRow.Item("ToDate"), "")
              }
            list.Add(DatesDO)
        Next
        Return list
    End Function


    Public Function WeekList(ByVal Year As String, ByVal Month As String, ByVal Week As String) As List(Of DatesDO)
        Dim dal As New DatesDA
        Dim list As New List(Of DatesDO)
        Dim dt As New DataTable

        dt = dal.WeekLists(Year, Month, Week)
        For Each dtRow As DataRow In dt.Rows
            Dim DatesDO As New DatesDO With {
              .Year = IIf((dtRow.Item("Year").ToString <> ""), dtRow.Item("Year"), ""),
              .Month = IIf((dtRow.Item("MonthName").ToString <> ""), dtRow.Item("MonthName"), ""),
              .Week = IIf((dtRow.Item("WeekNumber").ToString <> ""), dtRow.Item("WeekNumber"), ""),
              .FromDate = IIf(dtRow.Item("Fromdate").ToString <> "", dtRow.Item("Fromdate"), ""),
              .ToDate = IIf(dtRow.Item("ToDate").ToString <> "", dtRow.Item("ToDate"), "")
              }
            list.Add(DatesDO)
        Next
        Return list
    End Function

    Public Function CheckIfExist(ByVal Year As String,
                                    ByVal Month As String,
                                    ByVal Week As String) As Boolean

        Dim dal As New DatesDA
        success = True

        If dal.CheckIfExist(Year, Month, Week) Then
            errMsg = "Date already exists"
            success = False
        Else
            success = True

        End If

        Return success
    End Function

    Public Function SaveDateInsert(ByVal Year As String,
                                    ByVal Month As String,
                                    ByVal Week As String,
                                    ByVal FromDate As String,
                                    ByVal ToDate As String,
                                    ByVal addedByUsername As String,
                                    ByVal natureOfChange As String
                           ) As Boolean
        Dim success As Boolean = True
        Dim dal As New DatesDA

        If Not dal.SaveDateInsert(Year, Month, Week, FromDate, ToDate, addedByUsername, natureOfChange) Then
            message = dal.ErrorMsg
            success = False
        End If
        Return success
    End Function
End Class
