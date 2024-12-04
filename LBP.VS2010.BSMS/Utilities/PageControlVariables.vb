Imports System.Configuration.ConfigurationManager
Imports System.Configuration

Public Class PageControlVariables
#Region "ENUMERATIONS"
    Enum FieldVariables

#Region "MISC VARIABLES"
        ' Put variables here that are generic or shared across all modules
        USERNAME
        PASSWORD

        AMOUNT_12
        AMOUNT_ZERO

        DEFAULT_MIN_COUNT
#End Region

#Region "LOGIN CREDENTIALS"
        PASSWORD_ATTEMPT
#End Region

#Region "PAGE VARIABLES"
        ' put variables that are used per module. Use #region to indicate module
        APP_CODE
        APP_NAME

        FIRST_NAME
        LAST_NAME
        MIDDLE_INITIAL
        SUFFIX
        IP
        EMAIL
        CASA_TARGET
        LOANS_TARGET

        ROLE_NAME
        ROLE_DESCRIPTION

        BRANCH
        GROUP

        SEARCH_TEXT

        WIF_CODE
        CHANGE_CODE
        CHANGE_DESCRIPTION
        REMARKS

        DESCRIPTION_OF_CHANGE

        PMR_FEEDBACK_FORM_NO
        PMR_TR_NUMBER
        PMR_DESCRIPTION_OF_CHANGE
        PMR_COMMENTS
        PMR_ANALYSIS
        PMR_RECOMMENDATION
        PMR_OTHER_CONCERNS

        INITIATOR
        POSITION
        DEPARTMENT
        LOCAL
        ACRF_DESCRIPTION_CHANGE
        REASON
        VERIFICATION_PROCESS
        ACRF_WIF
        ACRF_TCR
        ACRFNO
        PROJNAME
        ACRF_REMARKS

        WIF_PROJ_TITLE
        WIF_DESCRIPTION
        WIF_JUSTIFICATION
        WIF_TCR
        WIF_REMARK

        REMINDER_TIME

        BranchCode
        StartCPADate
        EndCPADate

        CPANAME_MAX
        CPA_REPORT
#End Region

#Region "CREATE NEW CLIENT"

        CLIENT_FIRST_NAME
        CLIENT_MIDDLE_INITIAL
        CLIENT_LAST_NAME
        'CLIENT_TYPE
        CLIENT_ADDRESS
        CLIENT_CONTACT_NO
        'CLIENT_AMOUNT
        'CLIENT_LOAN_AMOUNT_REPORTED
        CLIENT_VISITS
        'CLIENT_ADB

        'LEAD_DATE
        'SUSPECT_DATE
        'PROSPECT_DATE
        'CUSTOMER_DATE

        'DATE_LOST
        LOST_REASON
        LOST_REMARKS

        PNNUMBER
        ACCOUNTNO
        AMOUNT_16
        AMOUNT_14

#End Region						   
    End Enum

    Enum LengthSetting
        MIN
        MAX
    End Enum

    Enum PageFunctions
        SAVE
        DELETE
        UPDATE
        APPROVE
        UNLOCK
        REJECT
        FORWARD
        RESET
        SUBMIT
    End Enum

    Enum REGEX_VALIDATOR
        EMAIL
        URL
        IPADDRESS
        PHONE
        PASSWORD
        NAME
        ALPHANUMERIC
        ALPHA
        SPECIAL_CHARS
    End Enum
#End Region

#Region "GET VARIABLE LENGTH "
    Shared Function getFieldLength(ByVal controlType As FieldVariables, ByVal minMaxSetting As LengthSetting) As String
        Dim pcv As New PageControlVariables

        Dim len As String = String.Empty

        Select Case minMaxSetting
            Case LengthSetting.MIN
                Return pcv.getMinLength(controlType)

            Case LengthSetting.MAX
                Return pcv.getMaxLength(controlType)

            Case Else
                Return String.Empty
        End Select

        pcv = Nothing

        Return len
    End Function

    Private Function getMinLength(ByVal controlType As FieldVariables) As String
        Dim minLength As String = String.Empty

        Select Case controlType
            Case FieldVariables.PASSWORD
                minLength = GetSection("PageControlVariables")("PasswordMinLen")
        End Select

        Return minLength
    End Function

    Private Function getMaxLength(ByVal controlType As FieldVariables) As String
        Dim maxLength As String = String.Empty

        Select Case controlType
            Case FieldVariables.USERNAME
                maxLength = GetSection("PageControlVariables")("UsernameMaxLen")

            Case FieldVariables.PASSWORD
                maxLength = GetSection("PageControlVariables")("PasswordMaxLen")

            Case FieldVariables.APP_CODE
                maxLength = GetSection("PageControlVariables")("AppCodeMaxLen")

            Case FieldVariables.APP_NAME
                maxLength = GetSection("PageControlVariables")("AppNameMaxLen")

            Case FieldVariables.SEARCH_TEXT
                maxLength = GetSection("PageControlVariables")("SearchMaxLen")

            Case FieldVariables.FIRST_NAME
                maxLength = GetSection("PageControlVariables")("FirstNameMaxLen")

            Case FieldVariables.LAST_NAME
                maxLength = GetSection("PageControlVariables")("LastNameMaxLen")

            Case FieldVariables.MIDDLE_INITIAL
                maxLength = GetSection("PageControlVariables")("MiddleInitialMaxLen")

            Case FieldVariables.SUFFIX
                maxLength = GetSection("PageControlVariables")("SuffixMaxLen")

            Case FieldVariables.IP
                maxLength = GetSection("PageControlVariables")("IPMaxLen")

            Case FieldVariables.EMAIL
                maxLength = GetSection("PageControlVariables")("EmailMaxLen")

            Case FieldVariables.CHANGE_CODE
                maxLength = GetSection("PageControlVariables")("ChangeCodeMaxLen")

            Case FieldVariables.WIF_CODE
                maxLength = GetSection("PageControlVariables")("WIFCodeMaxLen")

            Case FieldVariables.CHANGE_DESCRIPTION
                maxLength = GetSection("PageControlVariables")("ChangeDescriptionMaxLen")

            Case FieldVariables.REMARKS
                maxLength = GetSection("PageControlVariables")("RemarksMaxLen")

            Case FieldVariables.DESCRIPTION_OF_CHANGE
                maxLength = GetSection("PageControlVariables")("DescriptionOfChangeMaxLen")

            Case FieldVariables.INITIATOR
                maxLength = GetSection("PageControlVariables")("InitiatorMaxLen")

            Case FieldVariables.POSITION
                maxLength = GetSection("PageControlVariables")("PositionMaxLen")

            Case FieldVariables.DEPARTMENT
                maxLength = GetSection("PageControlVariables")("DeptMaxLen")

            Case FieldVariables.LOCAL
                maxLength = GetSection("PageControlVariables")("LocalMaxLen")

            Case FieldVariables.ACRF_DESCRIPTION_CHANGE
                maxLength = GetSection("PageControlVariables")("ACRFChangeDescriptionMaxLen")

            Case FieldVariables.REASON
                maxLength = GetSection("PageControlVariables")("ReasonMaxLen")

            Case FieldVariables.VERIFICATION_PROCESS
                maxLength = GetSection("PageControlVariables")("VerificationProcessMaxLen")

            Case FieldVariables.ACRF_WIF
                maxLength = GetSection("PageControlVariables")("ACRFWIFMaxLen")

            Case FieldVariables.ACRF_TCR
                maxLength = GetSection("PageControlVariables")("ACRFTCRMaxLen")

            Case FieldVariables.ACRFNO
                maxLength = GetSection("PageControlVariables")("ACRFNoMaxLen")

            Case FieldVariables.PROJNAME
                maxLength = GetSection("PageControlVariables")("ProjNameMaxLen")

            Case FieldVariables.ACRF_REMARKS
                maxLength = GetSection("PageControlVariables")("ARCFRemarksMaxLen")

            Case FieldVariables.WIF_PROJ_TITLE
                maxLength = GetSection("PageControlVariables")("WIFProjTitleMaxLen")

            Case FieldVariables.WIF_DESCRIPTION
                maxLength = GetSection("PageControlVariables")("WIFDescriptionMaxLen")

            Case FieldVariables.WIF_JUSTIFICATION
                maxLength = GetSection("PageControlVariables")("WIFJustificationMaxLen")

            Case FieldVariables.WIF_TCR
                maxLength = GetSection("PageControlVariables")("WIFTCRNoMaxLen")

            Case FieldVariables.WIF_REMARK
                maxLength = GetSection("PageControlVariables")("WIFRemarksMaxLen")

            Case FieldVariables.ROLE_NAME
                maxLength = GetSection("PageControlVariables")("RoleMaxLen")

            Case FieldVariables.ROLE_DESCRIPTION
                maxLength = GetSection("PageControlVariables")("RoleDescMaxLen")

            Case FieldVariables.REMINDER_TIME
                maxLength = GetSection("PageControlVariables")("ReminderTimeMaxLen")

            Case FieldVariables.PMR_FEEDBACK_FORM_NO
                maxLength = GetSection("PageControlVariables")("PMRFeedbackNoMaxLen")

            Case FieldVariables.PMR_TR_NUMBER
                maxLength = GetSection("PageControlVariables")("PMRTRNumberMaxLen")

            Case FieldVariables.PMR_DESCRIPTION_OF_CHANGE
                maxLength = GetSection("PageControlVariables")("PMRDescriptionChangeMaxLen")

            Case FieldVariables.PMR_COMMENTS
                maxLength = GetSection("PageControlVariables")("PMRCommentsMaxLen")

            Case FieldVariables.PMR_ANALYSIS
                maxLength = GetSection("PageControlVariables")("PMRAnalysisMaxLen")

            Case FieldVariables.PMR_RECOMMENDATION
                maxLength = GetSection("PageControlVariables")("PMRRecommendationMaxLen")

            Case FieldVariables.PMR_OTHER_CONCERNS
                maxLength = GetSection("PageControlVariables")("PMROtherConcernsMaxLen")

            Case FieldVariables.CLIENT_FIRST_NAME
                maxLength = GetSection("PageControlVariables")("ClientFirstNameMaxLen")

            Case FieldVariables.CLIENT_MIDDLE_INITIAL
                maxLength = GetSection("PageControlVariables")("ClientMiddleInitialMaxLen")

            Case FieldVariables.CLIENT_LAST_NAME
                maxLength = GetSection("PageControlVariables")("ClientLastNameMaxLen")

            Case FieldVariables.CLIENT_ADDRESS
                maxLength = GetSection("PageControlVariables")("ClientAddressMaxLen")

            Case FieldVariables.CLIENT_CONTACT_NO
                maxLength = GetSection("PageControlVariables")("ClientContactNoMaxLen")

            Case FieldVariables.CLIENT_VISITS
                maxLength = GetSection("PageControlVariables")("ClientVisitsMaxLen")

            Case FieldVariables.LOST_REASON
                maxLength = GetSection("PageControlVariables")("LostReasonMaxLen")

            Case FieldVariables.LOST_REMARKS
                maxLength = GetSection("PageControlVariables")("LostRemarksMaxLen")

            Case FieldVariables.BranchCode
                maxLength = GetSection("PageControlVariables")("BranchCodeMaxLen")

            Case FieldVariables.PNNUMBER
                maxLength = GetSection("PageControlVariables")("PNNumberMaxLen")

            Case FieldVariables.AMOUNT_16
                maxLength = GetSection("PageControlVariables")("Amount16MaxLen")

            Case FieldVariables.AMOUNT_12
                maxLength = GetSection("PageControlVariables")("Amount12MaxLen")

            Case FieldVariables.AMOUNT_14
                maxLength = GetSection("PageControlVariables")("Amount14MaxLen")

            Case FieldVariables.ACCOUNTNO
                maxLength = GetSection("PageControlVariables")("ClientAccountNoMaxLen")

            Case FieldVariables.CPANAME_MAX
                maxLength = GetSection("PageControlVariables")("CPANameMaxLen")
            Case FieldVariables.CASA_TARGET
                maxLength = GetSection("PageControlVariables")("TargetMaxLen")
            Case FieldVariables.LOANS_TARGET
                maxLength = GetSection("PageControlVariables")("TargetMaxLen")
        End Select

        Return maxLength
    End Function
#End Region

#Region "GET FIELD VALUES"
    Shared Function getFieldValue(ByVal controlType As FieldVariables, ByVal minMaxSetting As LengthSetting) As String
        Dim pcv As New PageControlVariables

        Select Case minMaxSetting
            Case LengthSetting.MIN
                Return pcv.getMinValue(controlType)
            Case LengthSetting.MAX
                Return pcv.getMaxValue(controlType)
            Case Else
                Return String.Empty
        End Select

        pcv = Nothing
    End Function

    Private Function getMinValue(ByVal controlType As FieldVariables) As String
        Dim minValue As String = String.Empty

        minValue = "0"

        Select Case controlType
            Case FieldVariables.AMOUNT_ZERO
                minValue = GetSection("PageControlVariables")("ZeroValue")

            Case FieldVariables.DEFAULT_MIN_COUNT
                minValue = "1"

        End Select

        Return minValue
    End Function

    Private Function getMaxValue(ByVal controlType As FieldVariables) As String
        Dim maxValue As String = String.Empty

        maxValue = "0"

        Select Case controlType
            Case FieldVariables.AMOUNT_12
                maxValue = GetSection("PageControlVariables")("UsernameMaxLen")

            Case FieldVariables.PASSWORD_ATTEMPT
                maxValue = GetSection("PageControlVariables")("PasswordAttemptMaxValue")

            Case FieldVariables.AMOUNT_16
                maxValue = GetSection("PageControlVariables")("Amount16MaxValue")

            Case FieldVariables.AMOUNT_14
                maxValue = GetSection("PageControlVariables")("Amount14MaxValue")
        End Select

        Return maxValue
    End Function
#End Region

#Region "GET REGEX VALIDATOR PATTERN"
    Shared Function getRegexValidator(ByVal validatorType As REGEX_VALIDATOR) As String
        Dim regexExpression As String = String.Empty

        Select Case validatorType
            Case REGEX_VALIDATOR.EMAIL
                regexExpression = GetSection("PageControlVariables")("RegexEmail")

            Case REGEX_VALIDATOR.URL
                regexExpression = GetSection("PageControlVariables")("RegexURL")

            Case REGEX_VALIDATOR.IPADDRESS
                regexExpression = GetSection("PageControlVariables")("RegexIPAddress")

            Case REGEX_VALIDATOR.PASSWORD
                regexExpression = GetSection("PageControlVariables")("RegexStrongPassword")

            Case REGEX_VALIDATOR.NAME
                regexExpression = GetSection("PageControlVariables")("RegexName")

            Case REGEX_VALIDATOR.ALPHANUMERIC
                regexExpression = GetSection("PageControlVariables")("RegexAlphanumeric")

            Case REGEX_VALIDATOR.ALPHA
                regexExpression = GetSection("PageControlVariables")("RegexAlpha")

            Case REGEX_VALIDATOR.SPECIAL_CHARS
                regexExpression = GetSection("PageControlVariables")("RegexPWSpecialChars")
        End Select

        Return regexExpression
    End Function
#End Region

#Region "CONVERT DATES"
    Shared Function getMMDDYY(ByVal dateParameter As DateTime) As String
        Dim mmddyy As String = String.Empty
        Try
            mmddyy = dateParameter.ToString("MMddyy")
        Catch ex As Exception

        End Try

        Return mmddyy
    End Function

    Shared Function getJulianDate(ByVal dateParameter As DateTime) As String
        Dim julianDate As String = String.Empty
        julianDate = dateParameter.Year.ToString + dateParameter.DayOfYear.ToString("D3")
        Return julianDate
    End Function

    Shared Function getMMddyyyy(ByVal dateParameter As DateTime) As String
        Dim MMddyyyy As String = String.Empty
        MMddyyyy = dateParameter.ToString("MMddyyyy")
        Return MMddyyyy
    End Function

    Shared Function getCCYYMMdd(ByVal dateParameter As DateTime) As String
        Dim CCYYMMdd As String = String.Empty
        CCYYMMdd = dateParameter.ToString("yyyyMMdd")
        Return CCYYMMdd
    End Function

    Shared Function getMonth(ByVal dateParameter As DateTime) As String
        Dim MM As String = String.Empty
        MM = dateParameter.Month.ToString
        Return MM
    End Function

    Shared Function getYear(ByVal dateParameter As DateTime) As String
        Dim YYYY As String = String.Empty
        YYYY = dateParameter.Year.ToString
        Return YYYY
    End Function
#End Region

#Region "WEB SERVICE AUTHENTICATION ERROR"
    Public Shared Function isValidAccessWebService(ByVal errorMessage As String) As String
        If errorMessage.IndexOf("Authentication Error.") > -1 Then
            Return "Authentication Error."
        Else
            Return errorMessage
        End If
    End Function
#End Region
End Class



