Imports System.Data
Imports System.Data.SqlClient
Imports LBP.VS2010.BSMS.Utilities


Public Class MasterDAL
    Implements IDisposable

    Dim cmd As SqlCommand
    Dim Params As List(Of SqlParameter)
    Dim Conn As ConnectDAL
    Dim Trans As SqlTransaction

    Dim ErrMsg As String
    ReadOnly Property ErrorMsg As String
        Get
            Return ErrMsg
        End Get
    End Property


    Property moduleName As String
    Property action As String


    Sub New()
        ErrMsg = ""
        Conn = New ConnectDAL
        If Conn.Connection.State = ConnectionState.Open Then
            Conn.CloseConn()
        End If
        Conn.OpenConn()
        Trans = Conn.Connection.BeginTransaction
        cmd = New SqlCommand
    End Sub

    Sub New(ByVal conkey As String)
        ErrMsg = ""
        Conn = New ConnectDAL(conkey)
        If Conn.Connection.State = ConnectionState.Open Then
            Conn.CloseConn()
        End If
        Conn.OpenConn()
        Trans = Conn.Connection.BeginTransaction
        cmd = New SqlCommand
    End Sub

    ' USED FOR SCRIPTS THAT DOESNT NEED TO RETURN ANYTHING
    Protected Function ExecNonQuery(ByVal SPname As String, ByVal willCommit As Boolean) As Boolean
        If Not Conn.Connection.State = ConnectionState.Closed Then
            Dim success As Boolean = False

            Try
                With cmd
                    .CommandText = SPname
                    .Connection = Conn.Connection
                    .Transaction = Trans
                    .CommandType = CommandType.StoredProcedure
                    SetParams(cmd)
                    .ExecuteNonQuery()
                End With
                success = True
            Catch ex As Exception
                success = False
                ErrMsg = ex.Message
                Logger.writeLog(moduleName, New Diagnostics.StackFrame(1).GetMethod.Name, SPname, ErrMsg)
            Finally
                If success Then
                    If willCommit Then
                        Trans.Commit()
                        Conn.CloseConn()
                    End If
                Else
                    Trans.Rollback()
                    Conn.CloseConn()
                End If

                FlushParams()
            End Try

            Return success
        Else
            Return False
        End If
    End Function

    ' USED TO GET SINGLE VALUE ITEM
    Protected Function ExecScalar(ByVal SPname As String, ByVal willCommit As Boolean) As String
        Dim ret As String = ""
        If Not Conn.Connection.State = ConnectionState.Closed Then
            Dim success As Boolean = False
            Try
                With cmd
                    .CommandText = SPname
                    .Connection = Conn.Connection
                    .Transaction = Trans
                    .CommandType = CommandType.StoredProcedure
                    SetParams(cmd)
                    Dim x As Object = .ExecuteScalar()
                    ret = If(IsNothing(x), "", x.ToString)
                End With
                success = True
            Catch ex As Exception
                ret = ""
                success = False
                ErrMsg = ex.Message
                Logger.writeLog(moduleName, New Diagnostics.StackFrame(1).GetMethod.Name, SPname, ErrMsg)
            Finally
                If success Then
                    If willCommit Then
                        Trans.Commit()
                        Conn.CloseConn()
                    End If
                Else
                    Trans.Rollback()
                    Conn.CloseConn()
                End If

                FlushParams()
            End Try

            Return ret
        Else
            Return ""
        End If
    End Function

    ' USED TO GET LIST OF RECORDS/ROWS
    Protected Function GetDataTable(ByVal SPname As String, ByVal willCommit As Boolean) As DataTable
        Dim dt As New DataTable
        If Not Conn.Connection.State = ConnectionState.Closed Then
            Dim success As Boolean = False
            Try
                With cmd
                    .CommandText = SPname
                    .Connection = Conn.Connection
                    .Transaction = Trans
                    .CommandType = CommandType.StoredProcedure
                    SetParams(cmd)
                    dt.Load(.ExecuteReader)
                End With

                success = True
            Catch ex As Exception
                dt = Nothing
                success = False
                ErrMsg = ex.Message
                Logger.writeLog(moduleName, New Diagnostics.StackFrame(1).GetMethod.Name, SPname, ErrMsg)

            Finally
                If success Then
                    If willCommit Then
                        Trans.Commit()
                        Conn.CloseConn()
                    End If
                Else
                    Trans.Rollback()
                    Conn.CloseConn()
                End If

                FlushParams()
            End Try

            Return dt
        Else
            Return Nothing
        End If
    End Function

    Public Function CommitTrans() As Boolean
        If Not Conn.Connection.State = ConnectionState.Closed Then
            Trans.Commit()
            Conn.CloseConn()
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub RollbackTrans()
        If Not Conn.Connection.State = ConnectionState.Closed Then
            Trans.Rollback()
            Conn.CloseConn()
        End If
    End Sub

    Protected Sub AddInputParam(ByVal paramName As String, ByVal value As Object)
        If IsNothing(Params) Then
            Params = New List(Of SqlParameter)
        End If

        Params.Add(New SqlParameter(paramName, If(IsNothing(value), DBNull.Value, value)))
    End Sub
    Protected Sub AddInputParam(ByVal paramName As String, ByVal value As Object, ByVal typ As SqlDbType)
        If IsNothing(Params) Then
            Params = New List(Of SqlParameter)
        End If

        Dim p As New SqlParameter(paramName, typ)
        p.Direction = ParameterDirection.Input
        p.Value = value
        Params.Add(p)

    End Sub


    Private Sub SetParams(ByVal cmd As SqlCommand)
        Dim Parameter As New SqlParameter
        If Not Params Is Nothing Then
            For Each param In Params
                cmd.Parameters.Add(param)
            Next
        End If
        Parameter = Nothing
    End Sub
    Public Sub FlushParams()
        If Not IsNothing(Params) Then
            For Each param In Params
                param = Nothing
            Next
            Params.Clear()
            cmd.Parameters.Clear()
        End If
    End Sub

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects)
                FlushParams()
                Params = Nothing
                Trans.Dispose()
                Conn.CloseConn()
                Conn.Dispose()
                cmd.Dispose()
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

