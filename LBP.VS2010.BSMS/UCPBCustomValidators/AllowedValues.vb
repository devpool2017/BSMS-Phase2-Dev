Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages
Imports System.Configuration.ConfigurationManager

Public Class AllowedValues
    Inherits MasterValidator

    Property allowedValuesList As New List(Of String)

    Sub New()

    End Sub

    Sub New(ByVal objAllowedValueList As List(Of String))
        allowedValuesList = objAllowedValueList
    End Sub

    Sub New(ByVal objAllowedValueListString As String, Optional ByVal areValuesHardcoded As Boolean = False)
        Dim allowedValuesString As String = String.Empty

        If areValuesHardcoded Then ' values passed are harcoded
            allowedValuesString = objAllowedValueListString
        Else ' values are not hardocde, meaning passed value is config key
            allowedValuesString = GetSection("Commons")(objAllowedValueListString).ToString
        End If
        allowedValuesList = allowedValuesString.Split(",").ToList
    End Sub

    Public Shared Function validate(ByVal value As Object, ByVal objAllowedValueList As List(Of String)) As Boolean

        Dim strValue As String = TryCast(value, String)

        If Not String.IsNullOrEmpty(strValue) Then
            If Not objAllowedValueList.Contains(strValue) Then
                Return False
            End If
        End If

        Return True

    End Function
End Class
