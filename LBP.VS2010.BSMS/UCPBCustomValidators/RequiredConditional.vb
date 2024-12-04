Imports LBP.VS2010.BSMS.CustomValidators.RequiredField
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables

Public Class RequiredConditional
    Inherits MasterValidator

    Property dependentPropertyName As String
    Property dependentPropertyNameList As String()
    Property compareValues As String()

    Property reqField As RequiredField



    Public Shared Function needToValidate(ByVal objDepPropValue As String, Optional ByVal objCompareValues As String() = Nothing) As Boolean
        ' check if property is not empty
        If String.IsNullOrEmpty(objDepPropValue) Then
            ' if empty, no need to validate
            Return False
        Else
            ' if not empty check if there is value to compare
            If IsNothing(objCompareValues) Then
                ' if nothing then validate property 
                Return True
            Else
                ' check value. if not included in compare values then no need to validate
                If objCompareValues.Contains(objDepPropValue) Then
                    Return True
                Else
                    Return False
                End If
            End If

        End If



    End Function


    Public Class HasValue
        Inherits RequiredConditional


        Sub New(ByVal propName As String, Optional ByVal objControlType As ControlType = ControlType.TEXT, Optional ByVal countFieldVariable As FieldVariables = FieldVariables.DEFAULT_MIN_COUNT)
            dependentPropertyName = propName
            compareValues = Nothing

            reqField = New RequiredField(objControlType, countFieldVariable)
        End Sub
       
    End Class

    Public Class HasNoValue
        Inherits RequiredConditional

        Sub New(ByVal propName As String, Optional ByVal objControlType As ControlType = ControlType.TEXT, Optional ByVal countFieldVariable As FieldVariables = FieldVariables.DEFAULT_MIN_COUNT)
            dependentPropertyNameList = {}
            dependentPropertyName = propName

            reqField = New RequiredField(objControlType, countFieldVariable)
        End Sub
        Sub New(ByVal propNameList As String(), Optional ByVal objControlType As ControlType = ControlType.TEXT, Optional ByVal countFieldVariable As FieldVariables = FieldVariables.DEFAULT_MIN_COUNT)
            dependentPropertyNameList = propNameList
            compareValues = Nothing

            reqField = New RequiredField(objControlType, countFieldVariable)
        End Sub
        Public Overloads Shared Function needToValidate(ByVal objDepPropValue As String, Optional ByVal objCompareValues As String() = Nothing) As Boolean
            If String.IsNullOrEmpty(objDepPropValue) Then
                Return True
            End If

            Return False
        End Function
    End Class

    Public Class ValueEquals
        Inherits RequiredConditional

        Sub New(ByVal propName As String, ByVal valueToCompare As String(), Optional ByVal objControlType As ControlType = ControlType.TEXT, Optional ByVal countFieldVariable As FieldVariables = FieldVariables.DEFAULT_MIN_COUNT)
            dependentPropertyName = propName
            compareValues = valueToCompare

            reqField = New RequiredField(objControlType, countFieldVariable)
        End Sub
    End Class
End Class
