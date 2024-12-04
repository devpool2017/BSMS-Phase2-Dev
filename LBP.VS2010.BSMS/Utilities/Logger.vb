Imports System.Configuration
Imports System.IO

Public Class Logger
    Public Shared Sub writeLog(ByVal moduleName As String,
                        ByVal action As String,
                        ByVal SPName As String,
                        ByVal errorMsg As String)
        Dim filePath = ConfigurationManager.GetSection("Commons")("ErrorLogFilePath").ToString()
        'Dim fileName = DateTime.Today.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("hhmmss") + ".txt"
        Dim fileName = DateTime.Today.ToString("yyyyMMdd") + ".txt"

        If (Not Directory.Exists(filePath)) Then
            Directory.CreateDirectory(filePath)

            If Not File.Exists(filePath + "\" + fileName) Then
                File.Create(filePath + "\" + fileName).Close()
            End If
        End If

        Dim fs As New FileStream(filePath + "\" + fileName, FileMode.Append)

        Using writer As New StreamWriter(fs)
            writer.WriteLine("Module: " + moduleName)
            writer.WriteLine("Action: " + action)
            writer.WriteLine("Date: " + DateTime.Today.ToString("MM/dd/yyyy"))
            writer.WriteLine("Time: " + DateTime.Now.ToString("hh:mm:ss"))
            writer.WriteLine("Stored Procedure: " + SPName)
            writer.WriteLine("Error Message: " + errorMsg)
            writer.WriteLine("*************************************************************************")
            writer.WriteLine("")
        End Using
    End Sub

    Public Shared Sub writeAPIErrorLog(ByVal apiEndpoint As String, ByVal errorMsg As String)
        Dim filePath = ConfigurationManager.GetSection("Commons")("ErrorLogFilePath").ToString() + "\ErrorLog_ApiConnection"
        Dim fileName = DateTime.Today.ToString("yyyyMMdd") + ".txt"
        'Dim fileName = DateTime.Today.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("hhmmss") + ".txt"

        If (Not Directory.Exists(filePath)) Then
            Directory.CreateDirectory(filePath)

            If Not File.Exists(filePath + "\" + fileName) Then
                File.Create(filePath + "\" + fileName).Close()
            End If
        End If

        Dim fs As New FileStream(filePath + "\" + fileName, FileMode.Append)
        Using writer As New StreamWriter(fs)
            writer.WriteLine("API Endpoint: " + apiEndpoint)
            writer.WriteLine("Error Message: " + errorMsg)
            writer.WriteLine("Date Time: " + DateTime.Today.ToString("MM/dd/yyyy") + "-" + DateTime.Now.ToString("hh:mm:ss"))
            writer.WriteLine("*************************************************************************")
            writer.WriteLine("")
        End Using
    End Sub

    Public Shared Sub writeAPIConnectionLog(ByVal apiEndpoint As String, ByVal theMessage As String)
        Dim filePath = ConfigurationManager.GetSection("Commons")("ErrorLogFilePath").ToString() + "\ApiConnectionLogs"
        Dim fileName = DateTime.Today.ToString("yyyyMMdd") + ".txt"
        'Dim fileName = DateTime.Today.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("hhmmss") + ".txt"

        If (Not Directory.Exists(filePath)) Then
            Directory.CreateDirectory(filePath)

            If Not File.Exists(filePath + "\" + fileName) Then
                File.Create(filePath + "\" + fileName).Close()
            End If
        End If

        Dim fs As New FileStream(filePath + "\" + fileName, FileMode.Append)
        Using writer As New StreamWriter(fs)
            writer.WriteLine("API Endpoint: " + apiEndpoint)
            writer.WriteLine("Status/Message: " + theMessage)
            writer.WriteLine("Date Time: " + DateTime.Today.ToString("MM/dd/yyyy") + "-" + DateTime.Now.ToString("hh:mm:ss:fff"))
            writer.WriteLine("*************************************************************************")
            writer.WriteLine("")
        End Using
    End Sub

End Class
