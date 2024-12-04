Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.CustomValidators
Imports LBP.VS2010.BSMS.DataAccess
Imports LBP.VS2010.BSMS.CustomValidators.LengthAndValueValidator
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables
Imports System.Configuration.ConfigurationManager

<Serializable()>
Public Class AccountInfoDO
    Inherits MasterDO

    ' ALWAYS INCLUDE THESE INSTATIATIONS
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


#Region "DECLARATION"

    <DataColumnMapping("CASAType")>
    <DisplayLabel("CASA Type")>
    <CustomValidationMethod("ValidateCASAType")>
    Property CASAType As String

    <DataColumnMapping("CASATypesAvailed")>
    <DisplayLabel("CASA Types Availed")>
    Property CASATypesAvailed As String

    <DataColumnMapping("InitialCASADeposits")>
    <DisplayLabel("Initial Amount (CASA Deposits)")>
    <RequiredConditional.ValueEquals("CASAType", {"True"})>
    Property InitialCASADeposits As String

    <DataColumnMapping("AccountNumber")>
    <DisplayLabel("Account Number")>
    <RequiredConditional.ValueEquals("CASAType", {"True"})>
    Property AccountNumber As String

    <DataColumnMapping("AccountNumberAdded")>
    <DisplayLabel("Account Number Added")>
    Property AccountNumberAdded As String

    <DataColumnMapping("LoanType")>
    <DisplayLabel("Loan Type")>
    <CustomValidationMethod("ValidateLoanType")>
    Property LoanType As String

    <DataColumnMapping("LoanProductsAvailed")>
    <DisplayLabel("Loan Products Availed")>
    Property LoanProductsAvailed As String

    '<DataColumnMapping("DateReported")>
    '<DisplayLabel("Date Reported")>
    'Property DateReported As String

    <DataColumnMapping("OrigLoanAmount")>
    <DisplayLabel("Loan Amount Availed")>
    Property OrigLoanAmount As String

    <DataColumnMapping("LoanAmountReported")>
    <DisplayLabel("Desired Loan Amount")>
    Property LoanAmountReported As String

    <DataColumnMapping("PNNumber")>
    <DisplayLabel("PN Number")>
    Property PNNumber As String

    <DataColumnMapping("ProductsServices")>
    <DisplayLabel("Products & Services")>
    <CustomValidationMethod("ValidateProductsServices")>
    Property ProductsServices As String

    <DataColumnMapping("ProductServiceAvailed")>
    <DisplayLabel("Product Service Availed")>
    Property ProductsServicesAvailed As String

    <DataColumnMapping("InitialCASADeposits2")>
    <DisplayLabel("Initial Amount (Products & Services)")>
    <RequiredConditional.ValueEquals("ProductsServices", {"True"})>
    Property InitialCASADeposits2 As String

    <DataColumnMapping("ProductTypeSelected")>
    <DisplayLabel("Product Type Availed")>
    <CustomValidationMethod("ValidateProductTypeSelected")>
    Property ProductTypeSelected As String

    <DataColumnMapping("SelectedProducts")>
    <DisplayLabel("Selected Products")>
    <CustomValidationMethod("ValidateSelectedProducts")>
    Property SelectedProducts As String

    <DataColumnMapping("LoanProductSelected")>
    <DisplayLabel("Loan Product Selected")>
    Property LoanProductSelected As String

    <DataColumnMapping("LoanProductSelectedDate")>
    <DisplayLabel("Loan Product Selected Date")>
    Property LoanProductSelectedDate As String

    <DataColumnMapping("ADB")>
    <DisplayLabel("ADB")>
    Property ADB As String

    <DataColumnMapping("ClientID")>
    <DisplayLabel("Client ID")>
    Property ClientID As String

    <DataColumnMapping("LoanCodes")>
    <DisplayLabel("Loan Codes")>
    Property LoanCodes As String

    <DataColumnMapping("LoanShortName")>
    <DisplayLabel("Loan Short Name")>
    Property LoanShortName As String

    <DataColumnMapping("LoanReleasedAmount")>
    <DisplayLabel("Loan Released Amount")>
    Property LoanReleasedAmount As String

    <DataColumnMapping("LoanDateReported")>
    <DisplayLabel("Loan Date Reported")>
    Property LoanDateReported As String

#End Region

#Region "Update"
    Public Function GetAccountNumber() As List(Of AccountInfoDO)
        Dim dal As New AccountInfoDAL
        Dim dt As DataTable
        'Dim accountNumber As String()
        Dim list As New List(Of AccountInfoDO)

        'If Not IsNothing(accnum) Then
        dt = dal.GetAccountNumber(ClientID)
        'accountNumber = accnum.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

        If dt.Rows.Count > 0 Then
            'For Each account In accountNumber
            For Each dtRow As DataRow In dt.Rows
                Dim obj As New AccountInfoDO With {
                  .AccountNumber = IIf((dtRow.Item("AccountNumber").ToString <> ""), dtRow.Item("AccountNumber"), ""),
                  .ADB = IIf((dtRow.Item("ADB").ToString <> ""), dtRow.Item("ADB"), "0.00")
                }
                list.Add(obj)
            Next
            'Next
        End If
        'End If

        Return list
    End Function

    Public Function GetLoansAvailed() As List(Of AccountInfoDO)
        Dim dal As New AccountInfoDAL
        Dim dt As DataTable
        Dim list As New List(Of AccountInfoDO)

        If Not IsNothing(ClientID) Then
            dt = dal.GetLoansAvailed(ClientID, GetSection("Commons")("TargetMarketClientType"))

            If dt.Rows.Count > 0 Then
                For Each dtRow As DataRow In dt.Rows
                    Dim obj As New AccountInfoDO With {
                      .LoanCodes = IIf((dtRow.Item("LoanCodes").ToString <> ""), dtRow.Item("LoanCodes"), ""),
                      .LoanShortName = IIf((dtRow.Item("LoanShortName").ToString <> ""), dtRow.Item("LoanShortName"), ""),
                      .LoanAmountReported = IIf((dtRow.Item("LoanAmountReported").ToString <> ""), dtRow.Item("LoanAmountReported"), "0.00"),
                      .LoanReleasedAmount = IIf((dtRow.Item("LoanReleasedAmount").ToString <> ""), dtRow.Item("LoanReleasedAmount"), "0.00"),
                      .LoanDateReported = IIf((dtRow.Item("DateReported").ToString <> ""), dtRow.Item("DateReported"), "0.00"),
                      .PNNumber = IIf((dtRow.Item("PNNumber").ToString <> ""), dtRow.Item("PNNumber"), "")
                    }
                    list.Add(obj)
                Next
            End If

        End If

        Return list
    End Function


#End Region

#Region "Validation"

    Function ValidateCASAType(ByVal fieldLabel As String) As Boolean
        Dim success As Boolean = True

        If Boolean.Parse(CASAType) And String.IsNullOrWhiteSpace(CASATypesAvailed) Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.ADD_DETAILS, "account number", {"1"}) + "<br/>"
            success = False
        End If

        Return success
    End Function

    Function ValidateLoanType(ByVal fieldLabel As String) As Boolean
        Dim success As Boolean = True

        If Boolean.Parse(LoanType) And String.IsNullOrWhiteSpace(LoanProductsAvailed) Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.ADD_DETAILS, "loan details", {}) + "<br/>"
            success = False
        End If

        Return success
    End Function

    Function ValidateProductsServices(ByVal fieldLabel As String) As Boolean
        Dim success As Boolean = True

        If Boolean.Parse(ProductsServices) And String.IsNullOrWhiteSpace(ProductsServicesAvailed) Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.NOTHING_SELECTED, fieldLabel, {"1"}) + "<br/>"
            success = False
        End If

        Return success
    End Function

    Function ValidateProductTypeSelected(ByVal fieldLabel As String) As Boolean
        Dim success As Boolean = True

        If Not Boolean.Parse(CASAType) And Not Boolean.Parse(LoanType) And Not Boolean.Parse(ProductsServices) Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.NOTHING_SELECTED, fieldLabel, {"1"}) + "<br/>"
            success = False
        End If

        Return success
    End Function

    Function ValidateSelectedProducts(ByVal fieldLabel As String) As Boolean
        Dim success As Boolean = True

        If Integer.Parse(SelectedProducts) > 30 Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.VALUE_TOO_LARGE, fieldLabel, {"30"}) + "<br/>"
            success = False
        End If

        Return success
    End Function

    Function ValidateAmount(ByVal value As String, ByVal fieldLabel As String) As Boolean
        Dim success As Boolean = True
        Dim amount As Double = 0.0

        If String.IsNullOrEmpty(value) Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, fieldLabel, {Nothing}) + "<br/>"
            success = False

        Else
            If Double.TryParse(value, amount) Then
                If amount = 0 Then
                    errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, fieldLabel, {Nothing}) + "<br/>"
                    success = False
                End If
            Else
                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.INVALID_DATA, fieldLabel, {Nothing}) + "<br/>"
                success = False
            End If
        End If

        Return success
    End Function

    Function ValidateCASA(ByVal fieldLabel As String, ByVal clientID As String) As Boolean
        Dim success As Boolean = True
        Dim dal As New AccountInfoDAL
        Dim accNum As String() = AccountNumberAdded.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

        success = ValidateAmount(InitialCASADeposits, "Initial Amount (CASA Deposits)")

        If AccountNumber.Equals(String.Empty) Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, fieldLabel, {AccountNumber}) + "<br/>"
            success = False
        Else
            If Not (AccountNumber.Length = 10 Or AccountNumber.Length = 14) Then
                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.INVALID_DATA, fieldLabel, {AccountNumber}) + "<br/>"
                success = False
            Else
                If dal.CheckEAccountNumberExists(AccountNumber, Nothing).Rows.Count > 0 Then
                    errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.DUPLICATE, fieldLabel, {AccountNumber}) + "<br/>"
                    success = False
                    'Else
                    '    If Not LBPChkDigit.GetCheckDigit(AccountNumber) Then
                    '        errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.INVALID_DATA, fieldLabel, {AccountNumber}) + "<br/>"
                    '        success = False
                    '    End If
                Else
                    If accNum.Length > 0 Then
                        For x As Integer = 0 To accNum.Length - 1 Step 1
                            If AccountNumber.Equals(accNum(x)) Then
                                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.DUPLICATE, fieldLabel, {AccountNumber}) + "<br/>"
                                success = False
                                Exit For
                            End If
                        Next

                    End If

                End If
            End If
        End If

        Return success
    End Function

    Function ValidateLoan(ByVal fieldLabel As String, ByVal action As String) As Boolean
        Dim success As Boolean = True
        Dim dal As New AccountInfoDAL
        Dim loans As String() = LoanProductSelected.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

        'If LoanAmountReported.Equals("0.00") Then
        '    errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, fieldLabel, {LoanAmountReported}) + "<br/>"
        '    success = False
        'End If

        success = ValidateAmount(LoanAmountReported, "Desired Loan Amount")

        If loans.Length > 0 Then
            For x As Integer = 0 To loans.Length - 1 Step 1
                If action = "Create" Then
                    If LoanProductsAvailed.Equals(loans(x)) Then
                        errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.LOAN_SAME_DAY, Nothing, {Nothing}) + "<br/>"
                        success = False
                        Exit For
                    End If
                ElseIf action = "Update" Then
                    Dim loansDate As String() = LoanProductSelectedDate.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    Dim loanDate2 = Date.Parse(LoanDateReported, System.Globalization.CultureInfo.InvariantCulture)
                    Dim loanDateOrig As New DateTime
                    If LoanProductsAvailed.Equals(loans(x)) Then
                        loanDateOrig = Date.Parse(loansDate(x), System.Globalization.CultureInfo.InvariantCulture)
                        If loanDateOrig = loanDate2 Then
                            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.LOAN_SAME_DAY, Nothing, {Nothing}) + "<br/>"
                            success = False
                            Exit For
                        End If
                    End If
                End If
            Next
        End If

        'If LoanProductsAvailed.Equals("L7") Or LoanProductsAvailed.Equals("L8") Or LoanProductsAvailed.Equals("L9") Then
        '    If Double.Parse(LoanAmountReported) > Double.Parse(OrigLoanAmount) Then
        '        errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.GREATER_VALUE, "Original Loan Amount", {"Loan Amount Reported"}) + "<br/>"
        '        success = False
        '    End If
        'End If

        If Not String.IsNullOrWhiteSpace(PNNumber) Then
            If PNNumber.Length > 20 Then
                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.TOO_LONG, "P.N. Number", {"20"}) + "<br/>"
                success = False
            End If
        End If

        Return success
    End Function

#End Region

#Region "Insert/Update"

    Public Function AddAccountNumber(ByVal dal As AccountInfoDAL,
                                     ByVal accno As String,
                                     ByVal year As Integer,
                                     ByVal month As String,
                                     ByVal cid As String,
                                     ByVal adb As Decimal,
                                     ByVal ledgerbalance As Decimal,
                                     ByVal uploadedby As String,
                                     ByVal weeknum As String,
                                     ByVal monthname As String,
                                     ByVal leadyear As String,
                                     ByVal leaddate As String,
                                     ByVal fname As String,
                                     ByVal mname As String,
                                     ByVal lname As String,
                                     ByVal regioncode As String,
                                     ByVal branchcode As String,
                                     ByVal roleID As String,
                                     ByVal clienttypecode As String,
                                     ByVal willCommit As Boolean) As Boolean
        Dim success As Boolean = True
        'Dim dal As New AccountInfoDAL

        If Not dal.AddAccountNumber(accno, year, month, cid, adb, ledgerbalance, uploadedby, weeknum, monthname, leadyear,
                                    leaddate, fname, mname, lname, regioncode, branchcode, roleID, clienttypecode, willCommit) Then
            errMsg = dal.ErrorMsg
            success = False
        End If

        Return success
    End Function

    Public Function DeleteAccountNumber(ByVal dal As AccountInfoDAL,
                                        ByVal accno As String,
                                        ByVal cid As String,
                                        ByVal willCommit As Boolean) As Boolean
        Dim success As Boolean = True
        'Dim dal As New AccountInfoDAL

        If Not dal.DeleteAccountNumber(accno, cid, willCommit) Then
            errMsg = dal.ErrorMsg
            success = False
        End If

        Return success
    End Function

    Public Function AddLoansAvailed(ByVal dal As AccountInfoDAL,
                                    ByVal ClientID As String,
                                    ByVal ClientType As String,
                                    ByVal LoanCodes As String,
                                    ByVal LoanAmountReported As String,
                                    ByVal LoanReleasedAmount As String,
                                    ByVal PNNumber As String,
                                    ByVal CIFNumber As String,
                                    ByVal willCommit As Boolean) As Boolean
        Dim success As Boolean = True
        'Dim dal As New AccountInfoDAL

        If Not dal.AddLoansAvailed(ClientID, ClientType, LoanCodes, LoanAmountReported, LoanReleasedAmount, PNNumber, CIFNumber, willCommit) Then
            errMsg = dal.ErrorMsg
            success = False
        End If

        Return success
    End Function

    Public Function DeleteLoansAvailed(ByVal dal As AccountInfoDAL,
                                       ByVal cid As String,
                                       ByVal clientType As String,
                                       ByVal loanCode As String,
                                       ByVal willCommit As Boolean) As Boolean
        Dim success As Boolean = True
        'Dim dal As New AccountInfoDAL

        If Not dal.DeleteLoansAvailed(cid, clientType, loanCode, willCommit) Then
            errMsg = dal.ErrorMsg
            success = False
        End If

        Return success
    End Function

#End Region

End Class
