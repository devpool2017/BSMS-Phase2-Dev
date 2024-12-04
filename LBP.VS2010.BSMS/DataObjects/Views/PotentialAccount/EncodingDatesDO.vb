Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.CustomValidators.LengthAndValueValidator
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages.Validation
Imports LBP.VS2010.BSMS.DataAccess
Imports System.Text.RegularExpressions
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.CustomValidators
Imports System.Configuration.ConfigurationManager
Imports System.Configuration


Public Class EncodingDatesDO
    Inherits MasterDO

    <DisplayLabel("Start Date")>
    <ControlVariable(FieldVariables.StartCPADate)>
    Property PAStartDate As String

    <DisplayLabel("End Date")>
    <ControlVariable(FieldVariables.EndCPADate)>
    Property PAEndDate As String

    Property InEdit As Boolean

    Property GroupCode As String
    Property LogonUser As String
    Property Action As String

    Property listOfEncodingDates As New List(Of EncodingDatesDO)


#Region "INSTANTIATION"
    Sub New()

    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection, ByVal accessObj As ProfileDO)
        MyBase.New(dr, columnNames, accessObj)
    End Sub
#End Region


    Partial Class EncodingDatesResult
        'Property Fields As String
        Property ValueFrom As String
        'Property ValueTo As String
    End Class

    Public Function GetCPADates(groupCode As String) As EncodingDatesDO
        Dim dal As New EncodingDatesDAL
        Dim dt As New DataTable
        Dim encodeDates As New EncodingDatesDO
        isSuccess = True

        Try
            dt = dal.GetCPADates(groupCode)

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    With encodeDates

                        .PAStartDate = dt.Rows(0).Item("ParameterValue").ToString
                        .PAEndDate = dt.Rows(1).Item("ParameterValue").ToString
                        .InEdit = dt.Rows(0).Item("InEdit")

                    End With
                End If
            End If

        Catch ex As Exception
            'objErrorMessage = String.Empty
            objErrorMessage += DataBase.getErrorMessage("Load CPA Dates", dal.ErrorMsg)
            isSuccess = False

        End Try

        Return encodeDates
    End Function

    Public Function EditCPADates() As Boolean
        Dim success As Boolean = True
        Dim dal As New EncodingDatesDAL

        PAStartDate = CDate(PAStartDate).ToString("yyyy-MM-dd")
        PAEndDate = CDate(PAEndDate).ToString("yyyy-MM-dd")

        If Not dal.EditCPADates(LogonUser, GroupCode, PAStartDate, PAEndDate) Then
            errMsg = dal.ErrorMsg
            success = False
        End If

        Return success
    End Function

    Public Function Validate() As Boolean
        isSuccess = True

        If PAStartDate = "Invalid Date" Then
            errMsg += Insertbreak() + Messages.ErrorMessages.Validation.getErrorMessage(ErrorType.EMPTY, "Start Date Range", {PAStartDate})
            isSuccess = False
        End If

        If PAEndDate = "Invalid Date" Then
            errMsg += Insertbreak() + Messages.ErrorMessages.Validation.getErrorMessage(ErrorType.EMPTY, "End Date Range", {PAEndDate})
            isSuccess = False
        End If

        Return isSuccess
    End Function


    Public Function ApproveCPADates() As Boolean
        Dim success As Boolean = True
        Dim dal As New EncodingDatesDAL

        If Not dal.ApproveCPADates(LogonUser, GroupCode, Action) Then
            errMsg = dal.ErrorMsg
            success = False
        End If

        Return success
    End Function

    Public Function RejectCPADates() As Boolean
        Dim success As Boolean = True
        Dim dal As New EncodingDatesDAL

        If Not dal.RejectCPADates(LogonUser, GroupCode, Action) Then
            errMsg = dal.ErrorMsg
            success = False
        End If

        Return success
    End Function


    Shared Function GetMergeDetails(ByVal regionCode As String) As List(Of EncodingDatesResult)
        Dim dal As New EncodingDatesDAL
        Dim dt As DataTable = dal.GetMergeDetails(regionCode)
        Dim mergeDetails As New List(Of EncodingDatesResult)

        Dim list As New List(Of String)
        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then

                For Each row In dt.Rows
                    list.Add(row.Item("ValueFrom").ToString)
                Next

                For i As Integer = 0 To list.Count - 1
                    Dim encodingDatesResult As New EncodingDatesResult With {
                        .ValueFrom = list(i)
                    }

                    mergeDetails.Add(encodingDatesResult)
                Next

                Return mergeDetails
            End If
        End If

        Return Nothing
    End Function

End Class
