Imports LBP.VS2010.BSMS.Utilities.PageControlVariables
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages

Public Class LengthAndValueValidator
    Inherits MasterValidator

    Enum ValidatorSetting
        LENGTH
        VALUE
    End Enum

    Public Class MinLengthCheck
        Inherits LengthAndValueValidator

    End Class

    Public Class MaxLengthCheck
        Inherits LengthAndValueValidator

    End Class

    Public Class MinValueCheck
        Inherits LengthAndValueValidator

        Property minValueControl As FieldVariables

        Sub New(ByVal objMinValue As FieldVariables)
            minValueControl = objMinValue
        End Sub
    End Class

    Public Class MaxValueCheck
        Inherits LengthAndValueValidator

        Property maxValueControl As FieldVariables

        Sub New(ByVal objMaxValue As FieldVariables)
            maxValueControl = objMaxValue
        End Sub
    End Class

    Public Shared Function validate(ByVal value As Object, ByVal ctrlVariable As FieldVariables, ByVal mode As LengthSetting, ByVal validatorType As ValidatorSetting) As Boolean
        Select Case validatorType
            Case ValidatorSetting.LENGTH
                Dim strValue As String = TryCast(value, String)

                If IsNothing(strValue) Then
                    strValue = String.Empty
                End If

                If mode = LengthSetting.MIN Then
                    If strValue.Length < getFieldLength(ctrlVariable, mode) Then
                        Return False
                    End If
                ElseIf mode = LengthSetting.MAX Then
                    If strValue.Length > getFieldLength(ctrlVariable, mode) Then
                        Return False
                    End If
                End If

            Case ValidatorSetting.VALUE
                Dim decValue As Decimal
                Decimal.TryParse(value, decValue)

                If IsNothing(decValue) Then
                    decValue = 0.0
                End If

                If mode = LengthSetting.MIN Then
                    If decValue <= getFieldValue(ctrlVariable, mode) Then
                        Return False
                    End If
                ElseIf mode = LengthSetting.MAX Then
                    If decValue > getFieldValue(ctrlVariable, mode) Then
                        Return False
                    End If
                End If
        End Select

        Return True
    End Function

    Public Shared Function getErrorMessage(ByVal fieldName As String, ByVal ctrlVariable As FieldVariables, ByVal mode As LengthSetting, ByVal validatorType As ValidatorSetting) As String
        Dim msg As String = String.Empty
        Dim errorType As Validation.ErrorType

        Select Case validatorType
            Case ValidatorSetting.LENGTH
                If mode = LengthSetting.MIN Then
                    errorType = Validation.ErrorType.TOO_SHORT
                ElseIf mode = LengthSetting.MAX Then
                    errorType = Validation.ErrorType.TOO_LONG
                End If

                msg = Validation.getErrorMessage(errorType, fieldName, {getFieldLength(ctrlVariable, mode)})

            Case ValidatorSetting.VALUE
                If mode = LengthSetting.MIN Then
                    errorType = Validation.ErrorType.GREATER_VALUE
                ElseIf mode = LengthSetting.MAX Then
                    errorType = Validation.ErrorType.VALUE_TOO_LARGE
                End If

                msg = Validation.getErrorMessage(errorType, fieldName, {getFieldValue(ctrlVariable, mode)})
        End Select

        Return msg
    End Function
End Class
