Public Class ClientDAL
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

#Region "OnLoad"


    Public Function ClientTypeList() As DataTable
        Dim dt As New DataTable
        Try
            With Me
                dt = .GetDataTable("GetClientTypes", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function

    Public Function LeadSourceList() As DataTable
        Dim dt As New DataTable
        Try
            With Me
                dt = .GetDataTable("GetLeadSource", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function

    Public Function IndustryList() As DataTable
        Dim dt As New DataTable
        Try
            With Me
                dt = .GetDataTable("GetIndustry", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function

    Public Function ReasonList() As DataTable
        Dim dt As New DataTable
        Try
            With Me
                dt = .GetDataTable("GetLostReason", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function

    Public Function CASAProductList() As DataTable
        Dim dt As New DataTable
        Try
            With Me
                dt = .GetDataTable("GetCASAProducts", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function

    Public Function LoanProductList() As DataTable
        Dim dt As New DataTable
        Try
            With Me
                dt = .GetDataTable("GetLoanProducts", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function

    Public Function OtherProductList() As DataTable
        Dim dt As New DataTable
        Try
            With Me
                dt = .GetDataTable("GetOtherProducts", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function

    Public Function CPANamesList(ByVal Branch As String) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@BrCode", Branch)
                dt = .GetDataTable("GetCPANamesByBranch", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function

    Public Function GetCPADetails(ByVal CPAID As String) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@CPAID ", CPAID)
                dt = .GetDataTable("GetCPADetails", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function

    Public Function GetOtherParameters() As DataTable
        Dim dt As New DataTable
        Try
            With Me
                dt = .GetDataTable("GetOtherParameters", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function

    Public Function GetWeekNum(ByVal leadDate As String,
                               Optional ByVal willCommit As Boolean = True) As DataTable

        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@date", leadDate)
                dt = .GetDataTable("GetWeekNum", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function
#End Region

#Region "Insert"
    Public Function IsCustomerNameExist(ByVal fname As String, ByVal lname As String, ByVal branch As String) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@fname", fname)
                .AddInputParam("@lname", lname)
                .AddInputParam("@branch", branch)
                .AddInputParam("@year", Today.Year)
                dt = .GetDataTable("DupeNameExists", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try

        Return dt
    End Function

    Public Function AddClient(ByVal clientid As String, ByVal lname As String, ByVal mname As String, ByVal fname As String, ByVal clienttype As String, _
                               ByVal address As String, ByVal contactno As String, ByVal lead As String, ByVal suspect As String, ByVal prospect As String, _
                               ByVal customer As String, ByVal casatype As String, ByVal amount As Decimal, ByVal amtothers As Decimal, ByVal adb As Decimal, ByVal lost As String, _
                               ByVal reason As String, ByVal otheratypes As String, ByVal remarks As String, ByVal datenc As String, ByVal uploadedby As String, _
                               ByVal visits As String, ByVal accnums As String, ByVal leadsrc As String, ByVal indtype As String, ByVal offered As String, _
                               ByVal loanreported As Decimal, ByVal loansavailed As String, ByVal branch As String, ByVal regionCode As String,
                                   Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@ClientID", clientid)
            .AddInputParam("@Lname", lname)
            .AddInputParam("@Mname", mname)
            .AddInputParam("@Fname", fname)
            .AddInputParam("@ClientType", clienttype)
            .AddInputParam("@Address", address)
            .AddInputParam("@ContactNo", contactno)
            .AddInputParam("@Lead", lead)
            .AddInputParam("@Suspect", suspect)
            .AddInputParam("@Prospect", prospect)
            .AddInputParam("@Customer", customer)
            .AddInputParam("@CASATypes", casatype)
            .AddInputParam("@Amount", amount)
            .AddInputParam("@AmountOthers", amtothers)
            .AddInputParam("@ADB", adb)
            .AddInputParam("@Lost", lost)
            .AddInputParam("@Reason", reason)
            .AddInputParam("@OtherATypes", otheratypes)
            .AddInputParam("@Remarks", remarks)
            .AddInputParam("@DateEncoded", datenc)
            .AddInputParam("@UploadedBy", uploadedby)
            .AddInputParam("@Visits", visits)
            .AddInputParam("@AccountNumbers", accnums)
            .AddInputParam("@LeadSource", leadsrc)
            .AddInputParam("@IndustryType", indtype)
            .AddInputParam("@ProductsOffered", offered)
            .AddInputParam("@LoanReported", loanreported)
            .AddInputParam("@LoansAvailed", loansavailed)
            .AddInputParam("@BrCode", branch)
            .AddInputParam("@RegionCode", regionCode)
            Return .ExecNonQuery("AddClient", willCommit)
        End With
    End Function

    Public Function NewClientID(ByVal branch As String,
                               Optional ByVal willCommit As Boolean = True) As String
        With Me
            .AddInputParam("@branch", branch)
            Return .ExecScalar("NewClientID", willCommit)
        End With
    End Function

    Public Function UpdateCPATag(ByVal CPAID As String, ByVal tag As String,
                       Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@CPAID", CPAID)
            .AddInputParam("@Tag", tag)
            Return .ExecNonQuery("UpdateCPATag", willCommit)
        End With
    End Function

    Public Function UpdateCPA(ByVal CPAName As String, ByVal Industry As String,
                           Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@CPAName", CPAName)
            .AddInputParam("@Industry", Industry)
            Return .ExecNonQuery("UpdateCPA", willCommit)
        End With
    End Function


    Public Function AddProspectActivity(ByVal clientid As String, ByVal uploadedBy As String, ByVal prospect As String,
                               Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@clientID", clientid)
            .AddInputParam("@uploadedBy", uploadedBy)
            .AddInputParam("@prospectDate", prospect)
            Return .ExecNonQuery("AddProspectActivity", willCommit)
        End With
    End Function

#End Region

#Region "Update"
    Public Function GetClientDetails(ByVal ClientID As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@clientid", ClientID)
                dt = .GetDataTable("GetClientDetails", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function UpdateClient(ByVal clientid As String, _
                           ByVal address As String, ByVal contactno As String, ByVal lead As String, ByVal suspect As String, ByVal prospect As String, _
                           ByVal customer As String, ByVal casatype As String, ByVal amount As Decimal, ByVal amtothers As Decimal, ByVal adb As Decimal, ByVal lost As String, _
                           ByVal reason As String, ByVal otheratypes As String, ByVal remarks As String, ByVal datenc As String, ByVal uploadedby As String, _
                           ByVal visits As String, ByVal accnums As String, ByVal leadsrc As String, ByVal indtype As String, ByVal offered As String, _
                           ByVal loanreported As Decimal, ByVal loansavailed As String, _
                           Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@ClientID", clientid)
            .AddInputParam("@Address", address)
            .AddInputParam("@ContactNo", contactno)
            .AddInputParam("@Lead", lead)
            .AddInputParam("@Suspect", suspect)
            .AddInputParam("@Prospect", prospect)
            .AddInputParam("@Customer", customer)
            .AddInputParam("@CASATypes", casatype)
            .AddInputParam("@Amount", amount)
            .AddInputParam("@AmountOthers", amtothers)
            .AddInputParam("@ADB", adb)
            .AddInputParam("@Lost", lost)
            .AddInputParam("@Reason", reason)
            .AddInputParam("@OtherATypes", otheratypes)
            .AddInputParam("@Remarks", remarks)
            .AddInputParam("@DateEncoded", datenc)
            .AddInputParam("@UploadedBy", uploadedby)
            .AddInputParam("@Visits", visits)
            .AddInputParam("@AccountNumbers", accnums)
            .AddInputParam("@LeadSource", leadsrc)
            .AddInputParam("@IndustryType", indtype)
            .AddInputParam("@ProductsOffered", offered)
            .AddInputParam("@LoanReported", loanreported)
            .AddInputParam("@LoansAvailed", loansavailed)
            Return .ExecNonQuery("UpdateClient", willCommit)
        End With
    End Function

#End Region

End Class
