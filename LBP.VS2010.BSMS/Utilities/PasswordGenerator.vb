Imports System.Configuration.ConfigurationManager

Public Class PasswordGenerator
    Shared Function GeneratePassword() As List(Of String)
        Dim password As String = String.Empty
        Dim result As New List(Of String)
        Dim Random As New Random()

        Try
            For c As Integer = 1 To 15
                Dim iAsc As String = Random.Next(1, 4)

                Select Case iAsc
                    Case 1
                        password += Chr(Asc("A"c) + Random.Next(0, 25))
                    Case 2
                        password += Chr(Asc("a"c) + Random.Next(0, 25))
                    Case 3
                        password += Random.Next(0, 9).ToString
                End Select
            Next c

            result.Add(True)
            result.Add(password)
        Catch ex As Exception
            result.Add(False)
            result.Add("PasswordGenerator has a problem.")
        End Try

        Return result
    End Function
End Class
