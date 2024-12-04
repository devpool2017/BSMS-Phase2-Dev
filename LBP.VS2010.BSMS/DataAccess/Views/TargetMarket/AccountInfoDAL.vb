Public Class AccountInfoDAL
    Inherits MasterDAL

#Region "DECLARATION"
    Dim message As String

    ReadOnly Property errorMessage As String
        Get
            Return message
        End Get
    End Property
#End Region

#Region "INSTANTIATION/NEW"
    ' ALWAYS OVERRIDE THE MODULENAME PER DAL
    Sub New()
        MyBase.moduleName = "Create New Client"
    End Sub
#End Region

#Region "Validate"
    Public Function CheckEAccountNumberExists(ByVal accountNumber As String,
                                              ByVal clientID As String) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@AccountNumber", accountNumber)
                .AddInputParam("@ClientID", clientID)
                dt = .GetDataTable("CheckEAccountNumberExists", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function
#End Region

#Region "Insert/Update"

    Public Function AddAccountNumber(ByVal accno As String,
                                     ByVal year As Integer,
                                     ByVal month As String,
                                     ByVal cid As String,
                                     ByVal adb As Decimal,
                                     ByVal ledgerbalance As Decimal,
                                     ByVal uploadedby As String,
                                     ByVal weeknum As String,
                                     ByVal monthname As String,
                                     ByVal leadyear As String,
                                     ByVal leaddate As String,
                                     ByVal fname As String,
                                     ByVal mname As String,
                                     ByVal lname As String,
                                     ByVal regioncode As String,
                                     ByVal branchcode As String,
                                     ByVal roleID As String,
                                     ByVal clienttypecode As String,
                                     Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@accno", accno)
            .AddInputParam("@year", year)
            .AddInputParam("@month", month)
            .AddInputParam("@cid", cid)
            .AddInputParam("@adb", adb)
            .AddInputParam("@ledgerbalance", ledgerbalance)
            .AddInputParam("@uploadedby", uploadedby)
            .AddInputParam("@weeknum", weeknum)
            .AddInputParam("@monthname", monthname)
            .AddInputParam("@leadyear", leadyear)
            .AddInputParam("@leaddate", leaddate)
            .AddInputParam("@fname", fname)
            .AddInputParam("@mname", mname)
            .AddInputParam("@lname", lname)
            .AddInputParam("@regioncode", regioncode)
            .AddInputParam("@branchcode", branchcode)
            .AddInputParam("@roleID", roleID)
            .AddInputParam("@clienttypecode", clienttypecode)
            Return .ExecNonQuery("AddAccountNumber", willCommit)
        End With
    End Function

    Public Function DeleteAccountNumber(ByVal accno As String,
                             ByVal cid As String,
                             Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@accno", accno)
            .AddInputParam("@cid", cid)
            Return .ExecNonQuery("DeleteAccountNumber", willCommit)
        End With
    End Function

    Public Function DeleteLoansAvailed(ByVal cid As String,
                                 ByVal clientType As String,
                                 ByVal loanCode As String,
                                 Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@ClientID", cid)
            .AddInputParam("@ClientType", clientType)
            .AddInputParam("@LoanCodes", loanCode)
            Return .ExecNonQuery("DeleteLoansAvailed", willCommit)
        End With
    End Function

    Public Function GetAccountNumber(ByVal ClientID As String,
                             Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@ClientID", ClientID)
                dt = .GetDataTable("GetAccountNumberByClientID", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function GetLoansAvailed(ByVal ClientID As String,
                         ByVal ClientType As String,
                         Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@ClientID", ClientID)
                .AddInputParam("@ClientType", ClientType)
                dt = .GetDataTable("GetLoansAvailedByClientID", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function AddLoansAvailed(ByVal ClientID As String,
                                 ByVal ClientType As String,
                                 ByVal LoanCodes As String,
                                 ByVal LoanAmountReported As String,
                                 ByVal LoanReleasedAmount As String,
                                 ByVal PNNumber As String,
                                 ByVal CIFNumber As String,
                                 Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@ClientID", ClientID)
            .AddInputParam("@ClientType", ClientType)
            .AddInputParam("@LoanCodes", LoanCodes)
            .AddInputParam("@LoanAmountReported", LoanAmountReported)
            .AddInputParam("@LoanReleasedAmount", LoanReleasedAmount)
            .AddInputParam("@PNNumber", PNNumber)
            .AddInputParam("@CIFNumber", CIFNumber)
            Return .ExecNonQuery("AddLoansAvailed", willCommit)
        End With
    End Function

#End Region


End Class
