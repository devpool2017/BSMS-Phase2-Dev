Imports System.Net
Imports System.Configuration

Public Class API
    Public Shared Function APICall(ByVal requestData As String, ByVal APIName As String, Optional ByVal isAuth As Boolean = False,
                                   Optional ByVal AuthToken As String = "")
        Dim Req As HttpWebRequest
        Dim Response As HttpWebResponse = Nothing

        ''TEMP ACCEPT ALL CERT
        System.Net.ServicePointManager.ServerCertificateValidationCallback =
          Function(se As Object,
          cert As System.Security.Cryptography.X509Certificates.X509Certificate,
          chain As System.Security.Cryptography.X509Certificates.X509Chain,
          sslerror As System.Net.Security.SslPolicyErrors) True

        'Dim cert As New X509Certificate2("C:\Nix\UMS_API_SSL5.pfx", "Pass@word1")
        'Dim store As New X509Store(StoreName.My, StoreLocation.CurrentUser)
        'store.Open(OpenFlags.ReadWrite)
        'store.Add(cert)
        'store.Close()

        Req = HttpWebRequest.Create(ConfigurationManager.GetSection("Commons")("ApiEndPointDev").ToString() + APIName)

        Try
            Req.Method = "POST"
            Req.ContentType = "application/json"
            Req.Accept = "application/json"
            Req.PreAuthenticate = True

            If isAuth = True Then
                Req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + AuthToken)
            End If

            Logger.writeAPIConnectionLog(Req.RequestUri.ToString(), "START OF TRANSACTION.") 'API TRANSACTION LOGGER

            Req.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(requestData), 0, System.Text.Encoding.UTF8.GetBytes(requestData).Count)
            Response = Req.GetResponse

            Logger.writeAPIConnectionLog(Req.RequestUri.ToString(), "END OF TRANSACTION. " + "Status: " + Response.StatusCode.ToString()) 'API TRANSACTION LOGGER

            'Response.Close()
        Catch ex As WebException
            If ex.Status = WebExceptionStatus.ProtocolError AndAlso ex.Response IsNot Nothing Then
                Response = DirectCast(ex.Response, HttpWebResponse)
            End If
            Logger.writeAPIErrorLog(Req.RequestUri.ToString(), ex.Message + " StackTrace: " + ex.StackTrace) 'API ERROR TRANSACTION LOGGER
        End Try

        Return Response


    End Function


    Public Shared Function APICallGateway(ByVal requestData As String, Optional ByVal isAuth As Boolean = False,
                                   Optional ByVal AuthToken As String = "")
        Dim Req As HttpWebRequest
        Dim Response As HttpWebResponse = Nothing

        ''TEMP ACCEPT ALL CERT
        System.Net.ServicePointManager.ServerCertificateValidationCallback =
          Function(se As Object,
          cert As System.Security.Cryptography.X509Certificates.X509Certificate,
          chain As System.Security.Cryptography.X509Certificates.X509Chain,
          sslerror As System.Net.Security.SslPolicyErrors) True

        'Dim cert As New X509Certificate2("Path/to/Storage.pfx", "Pass@word1")
        'Dim store As New X509Store(StoreName.My, StoreLocation.CurrentUser)
        'store.Open(OpenFlags.ReadWrite)
        'store.Add(cert)
        'store.Close()

        Req = HttpWebRequest.Create(ConfigurationManager.GetSection("Commons")("ApiGatewayEndpoint").ToString())

        Try
            Req.Method = "Post"
            Req.ContentType = "application/json"
            Req.Accept = "application/json"
            Req.PreAuthenticate = True

            If isAuth = True Then
                Req.Headers.Add(HttpRequestHeader.Authorization, "Bearer " + AuthToken)
            End If

            Logger.writeAPIConnectionLog(Req.RequestUri.ToString(), "START OF TRANSACTION.") 'API TRANSACTION LOGGER

            Req.GetRequestStream.Write(System.Text.Encoding.UTF8.GetBytes(requestData), 0, System.Text.Encoding.UTF8.GetBytes(requestData).Count)
            Response = Req.GetResponse

            Logger.writeAPIConnectionLog(Req.RequestUri.ToString(), "END OF TRANSACTION. " + "Status: " + Response.StatusCode.ToString()) 'API TRANSACTION LOGGER

            'Response.Close()
        Catch ex As WebException
            If ex.Status = WebExceptionStatus.ProtocolError AndAlso ex.Response IsNot Nothing Then
                Response = DirectCast(ex.Response, HttpWebResponse)
            End If
            Logger.writeAPIErrorLog(Req.RequestUri.ToString(), ex.Message + " StackTrace: " + ex.StackTrace) 'API ERROR TRANSACTION LOGGER
        End Try

        Return Response


    End Function
End Class
