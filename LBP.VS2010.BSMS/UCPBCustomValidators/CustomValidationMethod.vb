Public Class CustomValidationMethod
    Inherits MasterValidator

    Property functionToCall As String
    Property functionArguments As List(Of Object)

    Sub New(ByVal objFunction As String)
        functionToCall = objFunction

    End Sub
End Class
