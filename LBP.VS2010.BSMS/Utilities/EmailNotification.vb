Imports System.Configuration.ConfigurationManager
Imports System.Data
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Configuration
Imports System.Net.Sockets
Imports System.Configuration

Public Class EmailNotification

#Region "Properties"
    Property moduleName As String
    Property body As String
    Property subject As String
    Property fromAddress As String
    Property assignedTo As String
    Property recipient As New List(Of String)
    Property ccrecipient As New List(Of String)
    Property notificationBody As String

    Property errorMsg As String
    Const _LINE_BREAK As String = "<br />"

    Dim message As MailMessage

    Property LoggerMessage As String = String.Empty
#End Region

#Region "INSTANTIATION/NEW"
    Sub New()
        message = New MailMessage
        message.IsBodyHtml = True
    End Sub

    Sub New(ByVal attachment As String)
        message = New MailMessage
        message.IsBodyHtml = True
        message.Attachments.Add(New Attachment(attachment))
    End Sub
#End Region


    Public Sub Send()
        Try
            message.Subject = subject

            message.Body = body
            message.From = New MailAddress(fromAddress)
            message.To.Clear()

            For Each emailadd As String In recipient
                message.To.Add(New MailAddress(emailadd))
            Next

            For Each emailadd As String In ccrecipient
                message.CC.Add(New MailAddress(emailadd))
            Next

            Dim smtp As New SmtpClient

            If CBool(ConfigurationManager.GetSection("Commons")("UseDevTestingSMTP")) Then
                smtp.EnableSsl = True
                smtp.Credentials = New  _
                Net.NetworkCredential(GetSection("Commons")("FromEmailAddress").ToString, GetSection("Commons")("FromEmailPassword").ToString)
            End If

            If Not IsNothing(message) Then
                smtp.Send(message)
            End If

        Catch ex As Exception
            errorMsg = ex.Message
            Logger.writeLog(moduleName, New Diagnostics.StackFrame(1).GetMethod.Name + LoggerMessage, "Send Email", ex.Message)
        End Try
    End Sub

End Class
