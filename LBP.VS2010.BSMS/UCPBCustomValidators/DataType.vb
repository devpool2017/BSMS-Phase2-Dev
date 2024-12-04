Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages
Imports System.Text.RegularExpressions
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables
Imports System.Globalization

Public Class DataType
    Inherits MasterValidator

    Public Class TypeNumeric
        Inherits DataType

        Public Shared Function validate(ByVal value As Object) As Boolean
            Dim strValue As String = TryCast(value, String)

            If Not String.IsNullOrEmpty(strValue) Then
                Try
                    Dim intValue As Int64 = Int64.Parse(strValue)

                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End If

            Return True
        End Function

        Public Overloads Shared Function getErrorMessage(ByVal fieldName As String) As String
            Return Validation.getErrorMessage(Validation.ErrorType.MUST_BE_NUMERIC, fieldName, Nothing)

        End Function

    End Class

    Public Class TypeDecimal
        Inherits DataType

        Public Shared Function validate(ByVal value As Object) As Boolean
            Dim strValue As String = TryCast(value, String)

            If Not String.IsNullOrEmpty(strValue) Then
                Try
                    Dim decValue As Decimal = Decimal.Parse(strValue)

                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End If

            Return True
        End Function

    End Class

    Public Class TypeEmail
        Inherits DataType

        Public Shared Function validate(ByVal value As Object) As Boolean
            Dim strValue As String = TryCast(value, String)

            If Not String.IsNullOrEmpty(strValue) Then
                If Not Regex.Match(strValue, getRegexValidator(REGEX_VALIDATOR.EMAIL)).Success Then
                    Return False
                End If
            End If

            Return True
        End Function

    End Class

    Public Class TypePassword
        Inherits DataType

        Public Shared Function validate(ByVal value As Object) As Boolean
            Dim strValue As String = TryCast(value, String)

            If Not String.IsNullOrEmpty(strValue) Then
                If Not Regex.Match(strValue, getRegexValidator(REGEX_VALIDATOR.PASSWORD)).Success Then
                    Return False
                End If
            End If

            Return True
        End Function

        Public Overloads Shared Function getErrorMessage(ByVal fieldName As String) As String
            Return Validation.getErrorMessage(Validation.ErrorType.STRONG_PASSWORD, fieldName, Nothing)

        End Function

    End Class

    Public Class TypeDate
        Inherits DataType

        Property allowBackDate As Boolean = True
        Property allowCurrentDate As Boolean = True
        Property allowFutureDate As Boolean = True

        Public Class NoBackDate
            Inherits TypeDate

            Sub New()
                allowBackDate = False

            End Sub

            Public Overloads Shared Function validate(ByVal value As Object) As Boolean
                Dim strValue As String = TryCast(value, String)

                If Not String.IsNullOrEmpty(strValue) Then
                    Try
                        Dim dateValue As Date = Date.Parse(strValue)

                        dateValue = DateTime.ParseExact(dateValue, "MM/dd/yyyy", CultureInfo.InvariantCulture)

                        If dateValue < Date.Today Then
                            Return False
                        End If
                    Catch ex As Exception
                        Return False
                    End Try
                End If

                Return True

            End Function

            Public Overloads Shared Function getErrorMessage(ByVal fieldName As String) As String

                Return Validation.getErrorMessage(Validation.ErrorType.GREATER_VALUE, fieldName, {"Current Date"})

            End Function

        End Class


        Public Class NoForwardDate
            Inherits TypeDate

            Sub New(Optional ByVal includeCurrentDate As Boolean = True)
                allowFutureDate = False
                allowCurrentDate = includeCurrentDate
            End Sub

            Public Overloads Shared Function validate(ByVal value As Object, ByVal allowCurrentDate As Boolean) As Boolean
                Dim strValue As String = TryCast(value, String)

                If Not String.IsNullOrEmpty(strValue) Then
                    Try
                        Dim dateValue As Date = Date.Parse(strValue)

                        If dateValue > Date.Today Then
                            Return False
                        ElseIf dateValue = Date.Today Then
                            If Not allowCurrentDate Then
                                Return False
                            End If
                        End If
                    Catch ex As Exception
                        Return False
                    End Try
                End If

                Return True

            End Function

            Public Overloads Shared Function getErrorMessage(ByVal fieldName As String, ByVal allowCurrentDate As Boolean) As String

                Dim errorType As Validation.ErrorType = Validation.ErrorType.SHOULD_BE_BACKDATED

                If allowCurrentDate Then
                    errorType = Validation.ErrorType.SHOULD_BE_CURRENT_OR_BACKDATED
                End If

                Return Validation.getErrorMessage(errorType, fieldName, Nothing)

            End Function
        End Class

        Public Shared Function validate(ByVal value As Object) As Boolean
            Dim strValue As String = TryCast(value, String)

            If Not String.IsNullOrEmpty(strValue) Then
                Try
                    Dim dateValue As Date = DateTime.ParseExact(strValue, "MM/dd/yyyy", CultureInfo.InvariantCulture)

                    Return True
                Catch ex As Exception
                    Return False
                End Try
            End If

            Return True
        End Function

    End Class


    Public Shared Function getErrorMessage(ByVal fieldName As String) As String
        Return Validation.getErrorMessage(Validation.ErrorType.INVALID_DATA, fieldName, Nothing)

    End Function


End Class
