Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables

Public Class RequiredField
    Inherits MasterValidator

    Property fieldControlType As ControlType
    Property minCount As Integer

    Enum ControlType
        TEXT
        SELECTION
        LIST
        DECLARED_CLASS
    End Enum


    Sub New(Optional ByVal objControlType As ControlType = ControlType.TEXT, Optional ByVal countFieldVariable As FieldVariables = FieldVariables.DEFAULT_MIN_COUNT)
        fieldControlType = objControlType

        Try
            minCount = getFieldValue(countFieldVariable, LengthSetting.MIN)

            If minCount = 0 Then
                ' the fact that the field is required means there should be 1 . mincount = 0 only happens if there is no min setting for passed fieldvariable
                minCount = 1
            End If
        Catch ex As Exception
            minCount = 1
        End Try
    End Sub

    ' VALIDATION FOR TEXT, SELECTION
    Public Shared Function validate(ByVal value As Object) As Boolean

        Dim strValue As String = TryCast(value, String)

        If String.IsNullOrEmpty(strValue) Then
            Return False
        End If

        Return True

    End Function

    ' VALIDATION FOR LIST
    Public Shared Function validate(ByVal value As ICollection, ByVal objMinCount As Integer) As Boolean

        If value.Count < objMinCount Then
            Return False
        End If

        Return True

    End Function

    'Public Shared Function validate(Of genericType)(ByVal value As List(Of genericType), ByVal objMinCount As Integer) As Boolean

    '    Dim typ As Type = GetType(genericType)

    '    Dim objList As New List(Of genericType)
    '    objList = value


    '    If objList.Count < objMinCount Then
    '        Return False
    '    End If

    '    Return True

    'End Function

    Public Shared Function getErrorMessage(ByVal fieldName As String, ByVal objFiedlControlType As ControlType, Optional ByVal objMinCount As Integer = 0) As String
        Dim errorType As Validation.ErrorType

        Select Case objFiedlControlType
            Case ControlType.SELECTION
                errorType = Validation.ErrorType.NOTHING_SELECTED

            Case ControlType.LIST
                errorType = Validation.ErrorType.LEAST_NO_NOT_MET

            Case Else
                errorType = Validation.ErrorType.EMPTY

        End Select

        Return Validation.getErrorMessage(errorType, fieldName, {objMinCount})

    End Function

End Class
