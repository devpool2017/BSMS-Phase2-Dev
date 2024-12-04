Imports System.Configuration

Public Enum ValidationType
    RequiredField
    StringMaxLength
    InvalidValueList
End Enum
Public Class Messages
    Partial Class ErrorMessages
#Region "VALIDATION ERROR MESSAGES"
        Partial Class DataBase
            Shared Function getErrorMessage(ByVal pageFunction As PageControlVariables.PageFunctions, ByVal dbMessage As String) As String
                Dim errorMessage As String = String.Empty

                errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorSystemTransaction")

                Return errorMessage

            End Function

            Private Shared Function formatDBMessage(ByVal msg As String) As String

                msg = msg.Replace(ControlChars.Back, "")
                msg = msg.Replace(ControlChars.Cr, "")
                msg = msg.Replace(ControlChars.CrLf, "")
                msg = msg.Replace(ControlChars.FormFeed, "")
                msg = msg.Replace(ControlChars.Lf, "")
                msg = msg.Replace(ControlChars.NewLine, "")
                msg = msg.Replace(ControlChars.NullChar, "")
                msg = msg.Replace(ControlChars.Quote, "")
                msg = msg.Replace(ControlChars.Tab, "")
                msg = msg.Replace(ControlChars.VerticalTab, "")
                msg = msg.Replace("'", "")

                Return msg

            End Function
        End Class

        Partial Class Validation
            Enum ErrorType
                ' FOR REQUIRED FIELDS
                EMPTY
                NOTHING_SELECTED

                ' FOR INPUT LENGTHS
                TOO_LONG
                TOO_SHORT

                ' FOR INPUT VALUES
                VALUE_TOO_LARGE
                GREATER_VALUE
                LESS_THAN

                SAME_VALUES_NOT_ALLOWED

                ' COUNTS
                LEAST_NO_NOT_MET


                ' FOR CHARACTER VALIDATIONS
                INVALID_DATA
                MUST_BE_NUMERIC
                LETTERS_ONLY
                STRONG_PASSWORD

                ' DATES
                INVALID_CPADATES
                SHOULD_BE_BACKDATED
                SHOULD_BE_CURRENT_OR_BACKDATED
                SHOULD_BE_CURRENT_OR_THREE_MONTHS_BACKDATED

                ' DEPENDENCIES
                IN_USE
                DUPLICATE
                DOES_NOT_EXIST
                EXISTS

                ' MISC
                IS_LOCKED
                ALREADY_LOGGED_IN
                BRANCH_LOGGED_IN
                STANDARD_LOGIN_ERROR
                SECURITYGROUP_LOGIN_ERROR
                SECURITYGROUP_ISFOUND
                REQUIRED_LIST
                DEACTIVATED
                DOES_NOT_MATCH
                PASSWORD_STILL_IN_USE
                SAVE_ERROR
                STANDARD_CHANGE_PASSWORD_ERROR
                BAD_PASS_COUNTER

                ' BROWSERS
                EXPLORER
                CHROME
                SAFARI
                OPERA
                OTHER_BROWSER

                'CPA Report
                CPA_REPORT
                ' MISC

                NOT_SYSTEM_USER
                NOT_ACTIVE_USER
                NOT_ACTIVE_USERROLE
                NOT_EXISTS_IN_AD
                USER_IN_USE
                USER_NOT_EXIST
                MULTIPLE_LOGIN
                USER_EXISTS
                MAIN_ERROR
                IS_EXISTS
                NOT_MATCH
                CANNOT_BE_DELETED
                SYSTEM_PROBLEM

                ' TARGET MARKET
                INV_ACC_BRANCH
                LOAN_SAME_DAY
                ACC_INFO_REQUIRED
                UNAUTHORIZED
                REMARKS_LIMIT
                SEARCH_ERROR
                SEARCH_NO_RESULT
                ACC_MAX
                INV_USER_TARGET
                ADD_DETAILS

            End Enum

            Shared Function getErrorMessage(ByVal errorType As ErrorMessages.Validation.ErrorType, ByVal field As String, ByVal args As String()) As String
                Dim errorMessage As String = String.Empty

                Select Case errorType
                    ' FOR REQUIRED FIELDS
                    Case ErrorMessages.Validation.ErrorType.EMPTY
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorEmptyField")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                    Case ErrorMessages.Validation.ErrorType.NOTHING_SELECTED
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorNothingSelected")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                    Case ErrorMessages.Validation.ErrorType.CPA_REPORT
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorNothingSelectedCPA")

                        ' FOR INPUT LENGTHS
                    Case ErrorMessages.Validation.ErrorType.TOO_LONG
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorMaxLengthExceeded")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderMaxLength")), args(0))

                    Case ErrorMessages.Validation.ErrorType.TOO_SHORT
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorMinLength")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderMinLength")), args(0))


                        ' FOR INPUT LENGTH
                    Case ErrorMessages.Validation.ErrorType.VALUE_TOO_LARGE
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorExceedValue")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderMaxValue")), args(0))


                        ' INPUT VALUES
                    Case ErrorMessages.Validation.ErrorType.GREATER_VALUE
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorGreaterValue")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldValue")), args(0))


                    Case ErrorMessages.Validation.ErrorType.LESS_THAN
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorNotLessThan")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldValue")), args(0))

                    Case ErrorMessages.Validation.ErrorType.LEAST_NO_NOT_MET
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorMinimumNumberNotMet")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldValue")), args(0))

                    Case ErrorMessages.Validation.ErrorType.DOES_NOT_MATCH
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorDoesNotmatch")
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldValue")), args(0))

                        ' FOR CHARACTER VALIDATIONS/DATA TYPES
                    Case ErrorMessages.Validation.ErrorType.INVALID_DATA
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorInvalidData")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                    Case ErrorMessages.Validation.ErrorType.MUST_BE_NUMERIC
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorNonNumericField")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                    Case ErrorMessages.Validation.ErrorType.LETTERS_ONLY
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorNonAlphabeticField")

                    Case ErrorMessages.Validation.ErrorType.STRONG_PASSWORD
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorNonPasswordField")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                        ' DATES
                    Case ErrorMessages.Validation.ErrorType.SHOULD_BE_BACKDATED
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorShouldBeBackdated")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                    Case ErrorMessages.Validation.ErrorType.SHOULD_BE_CURRENT_OR_BACKDATED
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorShouldBeCurrentOrBackdated")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                    Case ErrorMessages.Validation.ErrorType.SHOULD_BE_CURRENT_OR_THREE_MONTHS_BACKDATED
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorShouldBeCurrentOrUpToThreeMonthsBackdated")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                        ' MISC
                    Case ErrorMessages.Validation.ErrorType.SAME_VALUES_NOT_ALLOWED
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorNotAllowedSameValues")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldValue")), args(0))

                    Case ErrorMessages.Validation.ErrorType.IN_USE
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorRecordInUse")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldValue")), args(0))
                    Case ErrorMessages.Validation.ErrorType.EXISTS
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorExists")
                    Case ErrorMessages.Validation.ErrorType.DUPLICATE
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorDuplicateRecord")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldValue")), args(0))


                    Case ErrorMessages.Validation.ErrorType.DOES_NOT_EXIST
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorDoesNotExist")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldValue")), args(0))


                        ' LOGIN
                    Case ErrorMessages.Validation.ErrorType.IS_LOCKED
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorUserLocked")


                    Case ErrorMessages.Validation.ErrorType.ALREADY_LOGGED_IN
                        errorMessage = ConfigurationManager.GetSection("Messages")("MessageErrorAlreadyLogin")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                    Case ErrorMessages.Validation.ErrorType.BRANCH_LOGGED_IN
                        errorMessage = ConfigurationManager.GetSection("Messages")("MessageErrorNoUserBrLogin")

                    Case ErrorMessages.Validation.ErrorType.STANDARD_LOGIN_ERROR
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorStandardLogin")

                    Case ErrorMessages.Validation.ErrorType.SECURITYGROUP_LOGIN_ERROR
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorSecurityGroupLogin")
                    Case ErrorMessages.Validation.ErrorType.SECURITYGROUP_ISFOUND
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorSecurityGroupValidation")

                    Case ErrorMessages.Validation.ErrorType.DEACTIVATED
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorUserDeactivated")

                    Case ErrorMessages.Validation.ErrorType.PASSWORD_STILL_IN_USE
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorPasswordAlreadyUsed")

                    Case ErrorMessages.Validation.ErrorType.REQUIRED_LIST
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorRequiredList")
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderMaxValue")), args(0))

                    Case ErrorMessages.Validation.ErrorType.STANDARD_CHANGE_PASSWORD_ERROR
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorChangePassword")

                    Case ErrorMessages.Validation.ErrorType.SAVE_ERROR
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorSave")
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                    Case errorType.BAD_PASS_COUNTER

                        ' FOR REQUIRED FIELDS
                    Case ErrorMessages.Validation.ErrorType.EMPTY
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorEmptyField")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                    Case ErrorMessages.Validation.ErrorType.NOTHING_SELECTED
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorNothingSelected")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field.ToLower)


                        ' FOR INPUT LENGTHS
                    Case ErrorMessages.Validation.ErrorType.TOO_LONG
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorMaxLengthExceeded")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderMaxLength")), args(0))

                    Case ErrorMessages.Validation.ErrorType.TOO_SHORT
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorMinLength")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderMinLength")), args(0))


                        ' FOR INPUT VALUES
                    Case ErrorMessages.Validation.ErrorType.VALUE_TOO_LARGE
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorMaxSmallIntValue")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderMaxValue")), args(0))

              
                    Case ErrorMessages.Validation.ErrorType.GREATER_VALUE
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorGreaterValue")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldValue")), args(0))

                  
                    Case ErrorMessages.Validation.ErrorType.LESS_THAN
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorNotLessThan")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldValue")), args(0))

                    Case ErrorMessages.Validation.ErrorType.SAME_VALUES_NOT_ALLOWED
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorNotAllowedSameValues")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldValue")), args(0))


                        ' COUNTS
                    Case ErrorMessages.Validation.ErrorType.LEAST_NO_NOT_MET
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorMinimumNumberNotMet")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldValue")), args(0))

                  
                        ' FOR CHARACTER VALIDATIONS
                    Case ErrorMessages.Validation.ErrorType.MUST_BE_NUMERIC
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorNonNumericField")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                    Case ErrorMessages.Validation.ErrorType.LETTERS_ONLY
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorLettersOnly")

                    Case ErrorMessages.Validation.ErrorType.INVALID_DATA
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorInvalidData")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                  
                        ' DATES
                    Case ErrorMessages.Validation.ErrorType.SHOULD_BE_BACKDATED
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorShouldBeBackdated")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                    Case ErrorMessages.Validation.ErrorType.SHOULD_BE_CURRENT_OR_BACKDATED
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorShouldBeCurrentOrBackdated")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)


                        ' DEPENDENCIES
                    Case ErrorMessages.Validation.ErrorType.CANNOT_BE_DELETED
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorCannotBeDeleted")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldValue")), args(0))


                    Case ErrorMessages.Validation.ErrorType.IN_USE
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorRecordInUse")
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)


                    Case ErrorMessages.Validation.ErrorType.DUPLICATE
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorDuplicateRecord")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldValue")), args(0))

                    Case ErrorMessages.Validation.ErrorType.IS_EXISTS
                        errorMessage = ConfigurationManager.GetSection("Messages")("MessageErrorExistsWithValue")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldValue")), args(0))

                    Case ErrorMessages.Validation.ErrorType.DOES_NOT_EXIST
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorDoesNotExist")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldValue")), args(0))

                    Case ErrorMessages.Validation.ErrorType.NOT_MATCH
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorNotMatch")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

               
                        ' MISC
                    Case ErrorMessages.Validation.ErrorType.IS_LOCKED
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorUserLocked")

                    Case ErrorMessages.Validation.ErrorType.NOT_SYSTEM_USER
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorNotSystemUser")

                    Case ErrorMessages.Validation.ErrorType.NOT_ACTIVE_USER
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorNotActiveUser")

                    Case ErrorMessages.Validation.ErrorType.NOT_ACTIVE_USERROLE
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorNotActiveUserRole")

                    Case ErrorMessages.Validation.ErrorType.USER_IN_USE
                        errorMessage = ConfigurationManager.GetSection("Messages")("MessageErrorCurrentlyLogin")
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                        'errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldValue")), args(0))

                    Case ErrorMessages.Validation.ErrorType.NOT_EXISTS_IN_AD
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorADCredentials")

                    Case ErrorMessages.Validation.ErrorType.USER_NOT_EXIST
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorUserNotExist")

                    Case ErrorMessages.Validation.ErrorType.MULTIPLE_LOGIN
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorMultipleLogin")

                    Case ErrorMessages.Validation.ErrorType.SYSTEM_PROBLEM
                        errorMessage = ConfigurationManager.GetSection("Messages")("MessageErrorSystemProblem")

                        'BROWSERS
                    Case ErrorMessages.Validation.ErrorType.EXPLORER, ErrorMessages.Validation.ErrorType.CHROME,
                        ErrorMessages.Validation.ErrorType.SAFARI, ErrorMessages.Validation.ErrorType.OPERA,
                        ErrorMessages.Validation.ErrorType.OTHER_BROWSER
                        errorMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageErrorBrowser")
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                  
                    Case ErrorMessages.Validation.ErrorType.USER_EXISTS
                        errorMessage = ConfigurationManager.GetSection("Messages")("MessageErrorAlreadyEnrolled")
                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                    Case ErrorMessages.Validation.ErrorType.INVALID_CPADATES
                        errorMessage = ConfigurationManager.GetSection("Messages")("MessageErrorCPADates")

                        'TARGET MARKET
                    Case ErrorMessages.Validation.ErrorType.INV_ACC_BRANCH
                        errorMessage = ConfigurationManager.GetSection("Messages")("MessageErrorAccountBranch")

                    Case ErrorMessages.Validation.ErrorType.LOAN_SAME_DAY
                        errorMessage = ConfigurationManager.GetSection("Messages")("MessageErrorLoanSameDay")

                    Case ErrorMessages.Validation.ErrorType.ACC_INFO_REQUIRED
                        errorMessage = ConfigurationManager.GetSection("Messages")("MessageErrorAccountInfoRequired")

                    Case ErrorMessages.Validation.ErrorType.UNAUTHORIZED
                        errorMessage = ConfigurationManager.GetSection("Messages")("MessageErrorUnauthorized")

                    Case ErrorMessages.Validation.ErrorType.REMARKS_LIMIT
                        errorMessage = ConfigurationManager.GetSection("Messages")("MessageErrorRemarksLimit")

                    Case ErrorMessages.Validation.ErrorType.SEARCH_ERROR
                        errorMessage = ConfigurationManager.GetSection("Messages")("MessageErrorSearch")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderSearchCount")), field)

                    Case ErrorMessages.Validation.ErrorType.SEARCH_NO_RESULT
                        errorMessage = ConfigurationManager.GetSection("Messages")("MessageErrorNoResult")

                    Case ErrorMessages.Validation.ErrorType.ACC_MAX
                        errorMessage = ConfigurationManager.GetSection("Messages")("MessageErrorMaxAccount")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderMaxValue")), args(0))

                    Case ErrorMessages.Validation.ErrorType.INV_USER_TARGET
                        errorMessage = ConfigurationManager.GetSection("Messages")("MessageErrorInvalidUSerTarget")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                    Case ErrorMessages.Validation.ErrorType.ADD_DETAILS
                        errorMessage = ConfigurationManager.GetSection("Messages")("MessageErrorAddDetails")

                        errorMessage = errorMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                End Select

                Return errorMessage
            End Function
        End Class


#End Region
    End Class

    Partial Class SuccessMessages
        Partial Class DataBase
            Shared Function getSuccessMessage(ByVal pageFunction As PageControlVariables.PageFunctions, ByVal field As String) As String
                Dim successMessage As String = String.Empty

                successMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageSuccessBasicFunction")

                successMessage = successMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                Return successMessage

            End Function
        End Class

        Partial Class Validation
            Enum SuccessType
                ADD
                DELETE
                SAVE
                APPROVED
                UPDATE
                CANCEL
                SUBMIT_COLLECTION

                'TARGET MARKET
                SEARCH

            End Enum

            Shared Function getSuccessMessage(ByVal successType As SuccessMessages.Validation.SuccessType, ByVal field As String, ByVal args As String()) As String
                Dim successMessage As String = String.Empty
                Select Case successType
                    Case SuccessMessages.Validation.SuccessType.ADD
                        successMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageAdd")

                        successMessage = successMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                    Case SuccessMessages.Validation.SuccessType.DELETE
                        successMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageDelete")

                        successMessage = successMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                    Case SuccessMessages.Validation.SuccessType.SAVE
                        successMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageSave")

                        successMessage = successMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                    Case SuccessMessages.Validation.SuccessType.APPROVED
                        successMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageSuccessApproved")

                        successMessage = successMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)

                    Case SuccessMessages.Validation.SuccessType.UPDATE
                        successMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("MessageUpdate")

                        successMessage = successMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderFieldName")), field)
                    Case SuccessMessages.Validation.SuccessType.CANCEL
                        successMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("SubmitCancel")
                    Case SuccessMessages.Validation.SuccessType.SUBMIT_COLLECTION
                        successMessage = System.Configuration.ConfigurationManager.GetSection("Messages")("SubmitCollection")

                        'TARGET MARKET
                    Case SuccessMessages.Validation.SuccessType.SEARCH
                        successMessage = ConfigurationManager.GetSection("Messages")("MessageSearchCount")
                        successMessage = successMessage.Replace((System.Configuration.ConfigurationManager.GetSection("Messages")("PlaceholderSearchCount")), field)
                End Select
                Return successMessage
            End Function
        End Class
    End Class




End Class
