Imports System.Web.SessionState
Imports System.Configuration.ConfigurationManager
Imports LBP.VS2010.BSMS.Utilities

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started

    End Sub


    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started

        ' UNCOMMENT THIS IF YOU NEED TO OVERRIDE THE CURRENT 15 MINUTE TIMEOUT PERIOD
        ' ALSO NEED TO UNCOMMENT SETTING SessionTimer IN WEB.CONFIG

        ' You also need to sync this with sess_expirationMinutes variable in SessionTimeout.js

        Session.Timeout = GetSection("Commons")("SessionTimer").ToString
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
        Dim name As String = New Diagnostics.StackFrame(1).GetMethod.Name
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs

        Dim ctx As HttpContext = HttpContext.Current

        ' VERIFY THAT THE SESSION CHECKED I STHE SESSION SET IN LOGIN



        If IsNothing(ctx.Session("currentUser")) Then
            Dim ex As Exception = Server.GetLastError()
            If Not (IsNothing(ex.InnerException.Message)) Then
                Logger.writeLog("General", "", "", ex.InnerException.Message)
            End If
            Response.Redirect("/Views/ErrorPages/ExpiredSession.aspx")
            Return
        Else
            Try
                Dim exc As Exception = Server.GetLastError

                If Not IsNothing(exc) Then
                    Logger.writeLog(GetSection("Commons")("SystemCode").ToString(), "Application Error", New Diagnostics.StackFrame(1).GetMethod.Name, exc.InnerException.Message)
                End If
                Server.ClearError()
            Catch ex As Exception
                Server.ClearError()
            End Try
        End If
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
        Response.Redirect("/Views/ErrorPages/ExpiredSession.aspx")
        Logger.writeLog("General", "", "", "Session Ends.")
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class