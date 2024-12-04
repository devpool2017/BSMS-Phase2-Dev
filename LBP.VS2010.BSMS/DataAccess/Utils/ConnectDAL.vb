Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager
Imports LBP.VS2010.WebCryptor

Public Class ConnectDAL
    Implements IDisposable

    Property Connection As SqlConnection
    Dim _webConfig As New WebConfigCryptor

    Sub New()
        If CBool(GetSection("Commons")("enableWebConfigEncryption")) Then
            Connection = New SqlConnection(_webConfig.Decrypt(GetSection("Commons")("DefaultDatabaseConnection").ToString(), False))
        Else
            Dim conkey = GetSection("Commons")("DefaultDatabaseConnection").ToString()
            Connection = New SqlConnection(ConnectionStrings(conkey).ConnectionString)
        End If
    End Sub

    Sub New(ByVal conkey As String)
        If CBool(GetSection("Commons")("enableWebConfigEncryption")) Then
            Connection = New SqlConnection(_webConfig.Decrypt(conkey, False))
        Else
            Connection = New SqlConnection(ConnectionStrings(conkey).ConnectionString)
        End If
    End Sub

    Public Sub OpenConn()
        Connection.Open()
    End Sub

    Public Sub CloseConn()
        Connection.Close()
        Connection.Dispose()
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
                Connection = Nothing
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
