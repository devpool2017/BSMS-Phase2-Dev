Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.CustomValidators
Imports LBP.VS2010.BSMS.DataAccess
Imports LBP.VS2010.BSMS.CustomValidators.LengthAndValueValidator
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables
Imports System.Configuration.ConfigurationManager


<Serializable()>
Public Class ClientDO
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

    <DataColumnMapping("ClientTypeCode")>
    <DisplayLabel("Client Type Code")>
    Property ClientTypeCode As String

    <DataColumnMapping("ClientTypeName")>
    <DisplayLabel("Client Type Name")>
    Property ClientTypeName As String

    <DataColumnMapping("LeadSourceCode")>
    <DisplayLabel("Lead Source Code")>
    Property LeadSourceCode As String

    <DataColumnMapping("LeadSourceDesc")>
    <DisplayLabel("Lead Source Desc")>
    Property LeadSourceDesc As String

    <DataColumnMapping("IndustryDesc")>
    <DisplayLabel("Industry Desc")>
    Property IndustryDesc As String

    <DataColumnMapping("DescIndicator")>
    <DisplayLabel("Desc Indicator")>
    Property DescIndicator As String

    <DataColumnMapping("ReasonDesc")>
    <DisplayLabel("Reason Desc")>
    Property ReasonDesc As String

    <DataColumnMapping("CASACodes")>
    <DisplayLabel("CASA Codes")>
    Property CASACodes As String

    <DataColumnMapping("CASAShortName")>
    <DisplayLabel("CASA Short Name")>
    Property CASAShortName As String

    <DataColumnMapping("ProductGroupNo")>
    <DisplayLabel("Product Group No")>
    Property ProductGroupNo As String

    <DataColumnMapping("OtherProductCode")>
    <DisplayLabel("Other Product Code")>
    Property OtherProductCode As String

    <DataColumnMapping("OtherShortName")>
    <DisplayLabel("Other Short Name")>
    Property OtherShortName As String

    <DataColumnMapping("CODE")>
    <DisplayLabel("CODE")>
    Property CODE As String

    <DataColumnMapping("LName")>
    <DisplayLabel("Last Name")>
    <ControlVariable(FieldVariables.CLIENT_LAST_NAME)>
    <RequiredField()>
    <MaxLengthCheck()>
    Property LName As String

    <DataColumnMapping("Mname")>
    <DisplayLabel("Middle Initial")>
    <ControlVariable(FieldVariables.CLIENT_MIDDLE_INITIAL)>
    <MaxLengthCheck()>
    Property MName As String

    <DataColumnMapping("Fname")>
    <DisplayLabel("First Name")>
    <RequiredConditional.ValueEquals("ClientType", {"Individual"})>
    <ControlVariable(FieldVariables.CLIENT_FIRST_NAME)>
    <MaxLengthCheck()>
    Property FName As String

    <DataColumnMapping("ClientType")>
    <DisplayLabel("Client Type")>
    Property ClientType As String

    <DataColumnMapping("Address")>
    <DisplayLabel("Home Address")>
    <ControlVariable(FieldVariables.CLIENT_ADDRESS)>
    <MaxLengthCheck()>
    Property Address As String

    <DataColumnMapping("ContactNo")>
    <DisplayLabel("Contact No")>
    <ControlVariable(FieldVariables.CLIENT_CONTACT_NO)>
    <MaxLengthCheck()>
    Property ContactNo As String

    <DataColumnMapping("Lead")>
    <DisplayLabel("Lead Date")>
    <RequiredField()>
    Property Lead As String

    <DataColumnMapping("Suspect")>
    <DisplayLabel("Suspect Date")>
    <CustomValidationMethod("ValidateSuspect")>
    Property Suspect As String

    <DataColumnMapping("Prospect")>
    <DisplayLabel("Prospect Date")>
    <CustomValidationMethod("ValidateProspect")>
    Property Prospect As String

    <DataColumnMapping("Customer")>
    <DisplayLabel("Customer Date")>
    <CustomValidationMethod("ValidateCustomer")>
    Property Customer As String

    <DataColumnMapping("CASATypes")>
    <DisplayLabel("CASA Types")>
    Property CASATypes As String

    <DataColumnMapping("Amount")>
    <DisplayLabel("Amount")>
    <ControlVariable(FieldVariables.AMOUNT_16)>
    <DataType.TypeDecimal()>
    <MaxValueCheck(FieldVariables.AMOUNT_16)>
    Property Amount As String

    <DataColumnMapping("AmountOthers")>
    <DisplayLabel("Amount Others")>
    <ControlVariable(FieldVariables.AMOUNT_16)>
    <DataType.TypeDecimal()>
    <MaxValueCheck(FieldVariables.AMOUNT_16)>
    Property AmountOthers As String

    <DataColumnMapping("ADB")>
    <DisplayLabel("ADB")>
    <ControlVariable(FieldVariables.AMOUNT_16)>
    <DataType.TypeDecimal()>
    <MaxValueCheck(FieldVariables.AMOUNT_16)>
    Property ADB As String

    <DataColumnMapping("Lost")>
    <DisplayLabel("Lost Date")>
    <CustomValidationMethod("ValidateLost")>
    Property Lost As String

    <DataColumnMapping("Reason")>
    <DisplayLabel("Reason")>
    <RequiredConditional.HasValue("Lost")>
    Property Reason As String

    <DataColumnMapping("OtherATypes")>
    <DisplayLabel("Other A Types")>
    Property OtherATypes As String

    <DataColumnMapping("UploadProc")>
    <DisplayLabel("Upload Proc")>
    Property UploadProc As String

    <DataColumnMapping("Remarks")>
    <DisplayLabel("Remarks")>
    <ControlVariable(FieldVariables.REMARKS)>
    <RequiredField()>
    <MaxLengthCheck()>
    Property Remarks As String

    <DataColumnMapping("DateEncoded")>
    <DisplayLabel("Date Encoded")>
    Property DateEncoded As String

    <DataColumnMapping("UploadedBy")>
    <DisplayLabel("Uploaded By")>
    Property UploadedBy As String

    <DataColumnMapping("Visits")>
    <DisplayLabel("Visits")>
    Property Visits As String

    <DataColumnMapping("AcountNumbers")>
    <DisplayLabel("Acount Numbers")>
    Property AccountNumbers As String

    <DataColumnMapping("LeadSource")>
    <DisplayLabel("Lead Source")>
    Property LeadSource As String

    <DataColumnMapping("IndustryType")>
    <DisplayLabel("Industry Type")>
    <RequiredConditional.ValueEquals("ClientType", {"CPA"})>
    Property IndustryType As String

    <DataColumnMapping("ProductsOffered")>
    <DisplayLabel("Products Offered")>
    Property ProductsOffered As String

    <DataColumnMapping("LoanReported")>
    <DisplayLabel("Loan Reported")>
    <ControlVariable(FieldVariables.AMOUNT_16)>
    <DataType.TypeDecimal()>
    <MaxValueCheck(FieldVariables.AMOUNT_16)>
    Property LoanReported As String

    <DataColumnMapping("LoansAvailed")>
    <DisplayLabel("Loans Availed")>
    Property LoansAvailed As String

    <DataColumnMapping("ClientID")>
    <DisplayLabel("Client ID")>
    Property ClientID As String

    <DataColumnMapping("SelectedProducts")>
    <DisplayLabel("Selected Products")>
    <CustomValidationMethod("ValidateSelectedProducts")>
    Property SelectedProducts As String

    <DataColumnMapping("CustomerName")>
    <DisplayLabel("Customer Name")>
    <CustomValidationMethod("ValidateCustomerName")>
    Property CustomerName As String

    <DataColumnMapping("Branch")>
    <DisplayLabel("Branch")>
    Property Branch As String

    <DataColumnMapping("CPAID")>
    <DisplayLabel("CPA ID")>
    Property CPAID As String

    <DataColumnMapping("RegionCode")>
    <DisplayLabel("Region Code")>
    Property RegionCode As String

    '<DataColumnMapping("LoansAvailedModal")>
    '<DisplayLabel("Loans Availed Modal")>
    'Property LoansAvailedModal As String

    <DataColumnMapping("OldRemarks")>
    Property OldRemarks As String

    <DataColumnMapping("LoanAmountReportedArr")>
    Property LoanAmountReportedArr As String

    <DataColumnMapping("PNNumberArr")>
    Property PNNumberArr As String

    Property DeletedAccountNumber As String
    Property DeletedLoansAvailed As String
    Property AddedAccountNumber As String
    Property AddedLoansAvailed As String
    Property UpdatedAccountNumber As String
    Property UpdatedLoansAvailed As String

#Region "MISC PROPRETIES"
    Property Password As String
    Property PasswordAttempt As Integer = 0
    Property TempStatusID As String
    Property InEdit As Integer = 0
    'Property IsAPI As Integer = Convert.ToInt32(GetSection("Commons")("isAPIValidation"))
    Property RequestedBy As String
    Property DateRequested As String
    Property Action As String
    Property LogonUser As String

#End Region

#End Region

#Region "DROPDOWN"
    Shared Function ClientTypeList() As DataTable
        Dim dal As New ClientDAL

        Return dal.ClientTypeList()
    End Function

    Shared Function LeadSourceList() As DataTable
        Dim dal As New ClientDAL

        Return dal.LeadSourceList()
    End Function

    Shared Function IndustryList() As DataTable
        Dim dal As New ClientDAL

        Return dal.IndustryList()
    End Function

    Shared Function ReasonList() As DataTable
        Dim dal As New ClientDAL

        Return dal.ReasonList()
    End Function

    Shared Function LoanProductListDDL() As DataTable
        Dim dal As New ClientDAL

        Return dal.LoanProductList
    End Function

    Shared Function CPANamesList(ByVal Branch As String) As DataTable
        Dim dal As New ClientDAL

        Return dal.CPANamesList(Branch)
    End Function

    Shared Function GetCPADetails(ByVal CPAID As String) As DataTable
        Dim dal As New ClientDAL

        Return dal.GetCPADetails(CPAID)
    End Function

    Shared Function GetOtherParameters() As DataTable
        Dim dal As New ClientDAL

        Return dal.GetOtherParameters()
    End Function

#End Region


#Region "Checkbox"
    Shared Function CASAProductList() As List(Of ClientDO)
        Dim dal As New ClientDAL

        Return listRecords(Of ClientDO)(dal.CASAProductList)
    End Function

    Shared Function LoanProductList() As List(Of ClientDO)
        Dim dal As New ClientDAL

        Return listRecords(Of ClientDO)(dal.LoanProductList)
    End Function

    Shared Function OtherProductList() As List(Of ClientDO)
        Dim dal As New ClientDAL

        Return listRecords(Of ClientDO)(dal.OtherProductList)
    End Function

#End Region

#Region "Validation"
    Function ValidateSelectedProducts(ByVal fieldLabel As String) As Boolean
        Dim success As Boolean = True

        If Integer.Parse(SelectedProducts) > 30 Then
            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.VALUE_TOO_LARGE, fieldLabel, {"30"}) + "<br/>"
            success = False
        End If

        Return success
    End Function

    Function ValidateCustomerName(ByVal fieldLabel As String) As Boolean
        Dim success As Boolean = True
        Dim dal As New ClientDAL

        If ClientType = "Individual" Then
            CustomerName = FName + " " + LName
        Else
            CustomerName = LName
        End If

        If dal.IsCustomerNameExist(FName, LName, Branch).Rows.Count > 0 Then

            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.DUPLICATE, fieldLabel, {CustomerName}) + "<br/>"
            success = False

        End If

        Return success
    End Function

    Function ValidateSuspect(ByVal fieldLabel As String) As Boolean
        Dim success As Boolean = True
        Dim dal As New ClientDAL
        Dim LeadDate As New Date
        Dim SuspectDate As New Date

        If Not IsNothing(Lead) And Not IsNothing(Suspect) Then
            LeadDate = Date.Parse(Lead, System.Globalization.CultureInfo.InvariantCulture)
            SuspectDate = Date.Parse(Suspect, System.Globalization.CultureInfo.InvariantCulture)

            If (SuspectDate - LeadDate).Days < 0 Then

                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.LESS_THAN, fieldLabel, {"Lead Date"}) + "<br/>"
                success = False

            End If
        End If

        Return success
    End Function

    Function ValidateProspect(ByVal fieldLabel As String) As Boolean
        Dim success As Boolean = True
        Dim dal As New ClientDAL
        Dim SuspectDate As New Date
        Dim ProspectDate As New Date

        If Not IsNothing(Suspect) And Not IsNothing(Prospect) Then
            SuspectDate = Date.Parse(Suspect, System.Globalization.CultureInfo.InvariantCulture)
            ProspectDate = Date.Parse(Prospect, System.Globalization.CultureInfo.InvariantCulture)

            If (ProspectDate - SuspectDate).Days < 0 Then
                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.LESS_THAN, fieldLabel, {"Suspect Date"}) + "<br/>"
                success = False
            End If
        End If

        Return success
    End Function

    Function ValidateCustomer(ByVal fieldLabel As String) As Boolean
        Dim success As Boolean = True
        Dim dal As New ClientDAL
        Dim ProspectDate As New Date
        Dim CustomerDate As New Date

        If Not IsNothing(Prospect) And Not IsNothing(Customer) Then
            ProspectDate = Date.Parse(Prospect, System.Globalization.CultureInfo.InvariantCulture)
            CustomerDate = Date.Parse(Customer, System.Globalization.CultureInfo.InvariantCulture)

            If (CustomerDate - ProspectDate).Days < 0 Then
                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.LESS_THAN, fieldLabel, {"Prospect Date"}) + "<br/>"
                success = False
            End If

        End If

        Return success
    End Function


    Function ValidateLost(ByVal fieldLabel As String) As Boolean
        Dim success As Boolean = True
        Dim dal As New ClientDAL
        Dim LostDate As Date
        Dim LastChangeDate As New Date
        Dim fieldLabel2 As String = String.Empty

        If Not IsNothing(Lost) Then
            LostDate = Date.Parse(Lost, System.Globalization.CultureInfo.InvariantCulture)

            If Not IsNothing(Customer) Then
                LastChangeDate = Date.Parse(Customer, System.Globalization.CultureInfo.InvariantCulture)
                fieldLabel2 = "Customer Date"
            ElseIf Not IsNothing(Prospect) Then
                LastChangeDate = Date.Parse(Prospect, System.Globalization.CultureInfo.InvariantCulture)
                fieldLabel2 = "Prospect Date"
            ElseIf Not IsNothing(Suspect) Then
                LastChangeDate = Date.Parse(Suspect, System.Globalization.CultureInfo.InvariantCulture)
                fieldLabel2 = "Suspect Date"
            ElseIf Not IsNothing(Lead) Then
                LastChangeDate = Date.Parse(Lead, System.Globalization.CultureInfo.InvariantCulture)
                fieldLabel2 = "Lead Date"
            End If


            If (LostDate - LastChangeDate).Days < 0 Then

                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.LESS_THAN, fieldLabel, {fieldLabel2}) + "<br/>"
                success = False

            End If

        End If

        Return success
    End Function
#End Region

#Region "Insert/Update"
    Public Function AddNewTargetMarket(ByVal obj As ClientDO, ByVal currentUser As LoginUser) As String
        Dim success As Boolean = True
        Dim accountNumber As String()
        Dim loanAmtReported As String()
        Dim loansAvailed As String()
        Dim PNNum As String()
        Dim accountInfo As New AccountInfoDO
        Dim dal As New ClientDAL
        Dim dal2 As New AccountInfoDAL

        If Not obj.AddClient(dal, currentUser.RoleID, currentUser.DisplayName, False) Then
            success = False
        End If

        If obj.ClientType.Equals("CPA") And (Not String.IsNullOrWhiteSpace(obj.IndustryType)) Then

            If Not obj.UpdateCPATag(dal, "Y", False) Then
                success = False
            End If

            If Not obj.UpdateCPA(dal, False) Then
                success = False
            End If

        End If

        If Not IsNothing(obj.Prospect) Then
            If Not obj.AddProspectActivity(dal, False) Then
                success = False
            End If
        End If

        If Not String.IsNullOrWhiteSpace(obj.AccountNumbers) Then
            accountNumber = obj.AccountNumbers.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            Dim LeadDate As Date = Date.Parse(obj.Lead, System.Globalization.CultureInfo.InvariantCulture)
            Dim leadYear As String = LeadDate.Year.ToString
            Dim leadMonth As String = MonthName(LeadDate.Month, False)
            Dim leadWeek As String = GetWeekNum(Lead)

            For Each account In accountNumber

                If Not accountInfo.AddAccountNumber(dal2, account, Today.Year, Today.Month, obj.ClientID, "0.00", "0.00", obj.UploadedBy,
                                             leadWeek, leadMonth, leadYear, obj.Lead, obj.FName, obj.MName, obj.LName, obj.RegionCode,
                                            obj.Branch, currentUser.RoleID, obj.ClientType, False) Then
                    success = False
                    Exit For
                End If
            Next

        End If

        If Not String.IsNullOrWhiteSpace(obj.LoansAvailed) Then
            loansAvailed = obj.LoansAvailed.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            loanAmtReported = obj.LoanAmountReportedArr.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            PNNum = obj.PNNumberArr.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            Dim LeadDate As Date = Date.Parse(obj.Lead, System.Globalization.CultureInfo.InvariantCulture)
            Dim leadYear As String = LeadDate.Year.ToString

            For x As Integer = 0 To loansAvailed.Length - 1 Step 1
                If loansAvailed.Length = PNNum.Length Then
                    If Not accountInfo.AddLoansAvailed(dal2, obj.ClientID, GetSection("Commons")("TargetMarketClientType"), loansAvailed(x), loanAmtReported(x), 0.0,
                                                   PNNum(x), String.Empty, False) Then
                        success = False
                        Exit For
                    End If
                Else
                    If Not accountInfo.AddLoansAvailed(dal2, obj.ClientID, GetSection("Commons")("TargetMarketClientType"), loansAvailed(x), loanAmtReported(x), 0.0,
                                                   String.Empty, String.Empty, False) Then
                        success = False
                        Exit For
                    End If
                End If

            Next

        End If

        If success Then
            dal.CommitTrans()
            dal2.CommitTrans()
        Else
            obj.errMsg += dal.errorMessage + "<br/>"
            obj.errMsg += dal2.errorMessage + "<br/>"
        End If

        Return obj.errMsg
    End Function

    Public Function UpdateNewTargetMarket(ByVal obj As ClientDO, ByVal currentUser As LoginUser) As String
        Dim success As Boolean = True
        Dim accountNumber As String()
        Dim addedAccountNumber As String()
        Dim loanAmtReported As String()
        Dim loansAvailed As String()
        Dim PNNum As String()
        Dim addedLoansAvailed As String()
        Dim accountInfo As New AccountInfoDO
        Dim dal As New ClientDAL
        Dim dal2 As New AccountInfoDAL

        If Not obj.UpdateClient(dal, currentUser.RoleID, currentUser.DisplayName, False) Then
            success = False
        End If

        If (Not String.IsNullOrWhiteSpace(obj.CPAID)) And (Not String.IsNullOrWhiteSpace(obj.IndustryType)) Then

            If Not obj.UpdateCPA(dal, False) Then
                success = False
            End If
        End If

        If Not IsNothing(obj.Prospect) Then
            If Not obj.AddProspectActivity(dal, False) Then
                success = False
            End If
        End If

        If Not String.IsNullOrWhiteSpace(obj.AccountNumbers) And Not String.IsNullOrWhiteSpace(obj.AddedAccountNumber) Then
            accountNumber = obj.AccountNumbers.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            addedAccountNumber = obj.AddedAccountNumber.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            Dim LeadDate As Date = Date.Parse(obj.Lead, System.Globalization.CultureInfo.InvariantCulture)
            Dim leadYear As String = LeadDate.Year.ToString
            Dim leadMonth As String = MonthName(LeadDate.Month, False)
            Dim leadWeek As String = GetWeekNum(Lead)

            For x As Integer = 0 To accountNumber.Length - 1 Step 1
                For y As Integer = 0 To addedAccountNumber.Length - 1 Step 1
                    If accountNumber(x).Equals(addedAccountNumber(y)) Then
                        If Not accountInfo.AddAccountNumber(dal2, accountNumber(x), Today.Year, Today.Month, obj.ClientID, "0.00", "0.00", obj.UploadedBy,
                                                     leadWeek, leadMonth, leadYear, obj.Lead, obj.FName, obj.MName, obj.LName, obj.RegionCode,
                                                    obj.Branch, currentUser.RoleID, obj.ClientType, False) Then
                            success = False
                            Exit For
                        End If
                    End If
                Next
            Next

        End If

        If Not String.IsNullOrWhiteSpace(obj.DeletedAccountNumber) Then
            Dim deleteAccNum = obj.DeletedAccountNumber.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

            For Each account In deleteAccNum

                If Not accountInfo.DeleteAccountNumber(dal2, account, obj.ClientID, False) Then
                    success = False
                    Exit For
                End If
            Next
        End If

        If Not String.IsNullOrWhiteSpace(obj.LoansAvailed) And Not String.IsNullOrWhiteSpace(obj.AddedLoansAvailed) Then
            loansAvailed = obj.LoansAvailed.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            loanAmtReported = obj.LoanAmountReportedArr.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            PNNum = obj.PNNumberArr.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
            addedLoansAvailed = obj.AddedLoansAvailed.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

            For x As Integer = 0 To loansAvailed.Length - 1 Step 1
                For y As Integer = 0 To addedLoansAvailed.Length - 1 Step 1
                    If loansAvailed(x).Equals(addedLoansAvailed(y)) Then
                        If loansAvailed.Length = PNNum.Length Then
                            If Not accountInfo.AddLoansAvailed(dal2, obj.ClientID, GetSection("Commons")("TargetMarketClientType"),
                                                               loansAvailed(x), loanAmtReported(x), 0.0, PNNum(x), String.Empty, False) Then
                                success = False
                                Exit For
                            End If
                        Else
                            If Not accountInfo.AddLoansAvailed(dal2, obj.ClientID, GetSection("Commons")("TargetMarketClientType"),
                                                               loansAvailed(x), loanAmtReported(x), 0.0, String.Empty, String.Empty, False) Then
                                success = False
                                Exit For
                            End If
                        End If
                    End If
                Next
            Next
        End If

        If Not String.IsNullOrWhiteSpace(obj.DeletedLoansAvailed) Then
            Dim deleteLoans = obj.DeletedLoansAvailed.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)

            For Each loans In deleteLoans

                If Not accountInfo.DeleteLoansAvailed(dal2, obj.ClientID, GetSection("Commons")("TargetMarketClientType"), loans, False) Then
                    success = False
                    Exit For
                End If
            Next
        End If

        If success Then
            dal.CommitTrans()
            dal2.CommitTrans()
        Else
            obj.errMsg += dal.errorMessage + "<br/>"
            obj.errMsg += dal2.errorMessage + "<br/>"
        End If

        Return obj.errMsg
    End Function


    Public Function AddClient(ByVal dal As ClientDAL, ByVal roleID As String, ByVal user As String, ByVal willCommit As Boolean) As Boolean
        Dim success As Boolean = True
        Dim remarksColor As String = String.Empty
        Dim remarksBy As String = String.Empty
        'Dim dal As New ClientDAL
        Dim time As String = DateTime.Now.ToString("h:mm:ss tt")

        remarksColor = "<font color=""#007700"">"

        remarksBy = user & " said on " & DateEncoded & " " & time
        Remarks = remarksColor & remarksBy & " : " & Remarks & "</font> <br>"

        If Not dal.AddClient(ClientID, LName, MName, FName, ClientType, Address, ContactNo,
                     Lead, Suspect, Prospect, Customer, CASATypes, Amount, AmountOthers,
                     ADB, Lost, Reason, OtherATypes, Remarks, DateEncoded, UploadedBy,
                     Visits, AccountNumbers, LeadSource, IndustryType, ProductsOffered,
                     LoanReported, LoansAvailed, Branch, RegionCode, willCommit) Then
            errMsg = dal.ErrorMsg
            success = False
        End If

        Return success
    End Function

    Public Function NewClientID() As String
        Dim success As Boolean = True
        Dim dal As New ClientDAL
        Dim increment As Integer

        ClientID = dal.NewClientID(Branch)
        If ClientID.Equals(String.Empty) Then
            ClientID = Branch & "-0000001"
        Else
            Integer.TryParse(ClientID, increment)

            ClientID = Branch & "-" & ((increment + 1).ToString.PadLeft(7, "0"))
        End If

        Return ClientID
    End Function

    Public Function UpdateCPATag(ByVal dal As ClientDAL, ByVal tag As String, ByVal willCommit As Boolean) As Boolean
        Dim success As Boolean = True
        'Dim dal As New ClientDAL

        If Not dal.UpdateCPATag(CPAID, tag, willCommit) Then
            errMsg = dal.ErrorMsg
            success = False
        End If

        Return success
    End Function

    Public Function UpdateCPA(ByVal dal As ClientDAL, ByVal willCommit As Boolean) As Boolean
        Dim success As Boolean = True
        'Dim dal As New ClientDAL

        If Not dal.UpdateCPA(LName, IndustryType, willCommit) Then
            errMsg = dal.ErrorMsg
            success = False
        End If

        Return success
    End Function

    Public Function AddProspectActivity(ByVal dal As ClientDAL, ByVal willCommit As Boolean) As Boolean
        Dim success As Boolean = True
        'Dim dal As New ClientDAL

        If Not dal.AddProspectActivity(ClientID, UploadedBy, Prospect, willCommit) Then
            errMsg = dal.ErrorMsg
            success = False
        End If

        Return success
    End Function

    Public Function GetWeekNum(ByVal leadDate As String) As String
        Dim dal As New ClientDAL
        Dim dt As DataTable = dal.GetWeekNum(leadDate)

        If dt.Rows.Count > 0 Then
            Return IIf((dt.Rows(0).Item("WeekNumber").ToString <> ""), dt.Rows(0).Item("WeekNumber"), "")
        Else
            Return String.Empty
        End If

    End Function

#End Region

#Region "Update"
    Public Function GetClientDetails() As ClientDO

        Dim dal As New ClientDAL
        Dim dt As DataTable
        'Dim obj As New ClientDO
        Dim list As New List(Of ClientDO)
        dt = dal.GetClientDetails(ClientID)

        '.Fullname = IIf((dtRow.Item("Fullname").ToString <> ""), dtRow.Item("Fullname"), ""),

        For Each dtRow As DataRow In dt.Rows
            Dim obj As New ClientDO With {
              .ClientID = IIf((dtRow.Item("ClientID").ToString <> ""), dtRow.Item("ClientID"), ""),
              .ClientType = dtRow.Item("ClientType"),
              .LName = IIf(dtRow.Item("Lname").ToString <> "", dtRow.Item("Lname"), ""),
              .FName = IIf(dtRow.Item("Fname").ToString <> "", dtRow.Item("Fname"), ""),
              .MName = IIf(dtRow.Item("Mname").ToString <> "", dtRow.Item("Mname"), ""),
              .Lead = IIf((dtRow.Item("Lead").ToString <> ""), dtRow.Item("Lead"), ""),
              .Suspect = IIf(dtRow.Item("Suspect").ToString <> "", dtRow.Item("Suspect"), ""),
              .Prospect = IIf(dtRow.Item("Prospect").ToString <> "", dtRow.Item("Prospect"), ""),
              .Customer = IIf(dtRow.Item("Customer").ToString <> "", dtRow.Item("Customer"), ""),
              .Lost = IIf(dtRow.Item("Lost").ToString <> "", dtRow.Item("Lost"), ""),
              .Address = IIf(dtRow.Item("Address").ToString <> "", dtRow.Item("Address"), ""),
              .ContactNo = IIf(dtRow.Item("ContactNo").ToString <> "", dtRow.Item("ContactNo"), ""),
              .CASATypes = IIf(dtRow.Item("CASATypes").ToString <> "", dtRow.Item("CASATypes"), ""),
              .Amount = IIf(dtRow.Item("Amount").ToString <> "", dtRow.Item("Amount"), ""),
              .AmountOthers = IIf(dtRow.Item("AmountOthers").ToString <> "", dtRow.Item("AmountOthers"), ""),
              .ADB = IIf(dtRow.Item("ADB").ToString <> "", dtRow.Item("ADB"), ""),
              .Reason = IIf(dtRow.Item("Reason").ToString <> "", dtRow.Item("Reason"), ""),
              .OtherATypes = IIf(dtRow.Item("OtherATypes").ToString <> "", dtRow.Item("OtherATypes"), ""),
              .UploadProc = IIf(dtRow.Item("UploadProc").ToString <> "", dtRow.Item("UploadProc"), ""),
              .Remarks = IIf(dtRow.Item("Remarks").ToString <> "", dtRow.Item("Remarks"), ""),
              .DateEncoded = IIf(dtRow.Item("DateEncoded").ToString <> "", dtRow.Item("DateEncoded"), ""),
              .UploadedBy = IIf(dtRow.Item("UploadedBy").ToString <> "", dtRow.Item("UploadedBy"), ""),
              .Visits = IIf(dtRow.Item("Visits").ToString <> "", dtRow.Item("Visits"), ""),
              .AccountNumbers = IIf(dtRow.Item("AccountNumbers").ToString <> "", dtRow.Item("AccountNumbers"), ""),
              .LeadSource = IIf(dtRow.Item("LeadSource").ToString <> "", dtRow.Item("LeadSource"), ""),
              .IndustryType = IIf(dtRow.Item("IndustryType").ToString <> "", dtRow.Item("IndustryType"), ""),
              .ProductsOffered = IIf(dtRow.Item("ProductsOffered").ToString <> "", dtRow.Item("ProductsOffered"), ""),
              .LoanReported = IIf(dtRow.Item("LoanReported").ToString <> "", dtRow.Item("LoanReported"), ""),
              .LoansAvailed = IIf(dtRow.Item("LoansAvailed").ToString <> "", dtRow.Item("LoansAvailed"), ""),
              .CPAID = IIf(dtRow.Item("CPAID").ToString <> "", dtRow.Item("CPAID"), "")
            }
            list.Add(obj)
        Next

        Return list.Item(0)
    End Function

    Public Function UpdateClient(ByVal dal As ClientDAL, ByVal roleID As String, ByVal user As String, ByVal willCommit As Boolean) As Boolean
        Dim success As Boolean = True
        Dim remarksColor As String = String.Empty
        Dim remarksBy As String = String.Empty
        'Dim dal As New ClientDAL
        Dim time As String = DateTime.Now.ToString("h:mm:ss tt")

        If roleID.Equals(GetSection("Commons")("GroupHead")) Then
            remarksColor = "<font color=""orange"">"
        ElseIf roleID.Equals(GetSection("Commons")("BM")) Then
            remarksColor = "<font color=""#007700"">"
        Else
            remarksColor = "<font color=""#003399"">"
        End If

        remarksBy = user & " said on " & DateEncoded & " " & time
        Remarks = remarksColor & remarksBy & " : " & Remarks & "</font> <br>"
        Remarks = OldRemarks & Remarks

        If Not dal.UpdateClient(ClientID, Address, ContactNo,
                     Lead, Suspect, Prospect, Customer, CASATypes, Amount, AmountOthers,
                     ADB, Lost, Reason, OtherATypes, Remarks, DateEncoded, UploadedBy,
                     Visits, AccountNumbers, LeadSource, IndustryType, ProductsOffered,
                     LoanReported, LoansAvailed, willCommit) Then
            errMsg = dal.ErrorMsg
            success = False
        End If

        Return success
    End Function


#End Region
End Class
