Public Class EncodingDAL
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
        moduleName = "Encoding"
    End Sub
#End Region

#Region "List"
    Public Function CheckCPADateRange(ByVal Username As String,
                                    Optional ByVal willCommit As Boolean = True) As String
        With Me
            .AddInputParam("@uname", Username)
            Return .ExecScalar("CheckCPADateRange", willCommit)
        End With
    End Function


    Public Function GetAllCPAsByYearPerBM(ByVal branchCode As String, Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@BRCODE", branchCode)
            Return .GetDataTable("GetAllCPAsByYearPerBMSO", True)
        End With
    End Function


    Public Function listAccounts(ByVal branchCode As String, Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@BRCODE", branchCode)
            Return .GetDataTable("GetCPAList", True)
        End With
    End Function

    Public Function viewAccountDetails(ByVal clientID As String,
                                      Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@clientID", clientID)
            Return .GetDataTable("ViewCPADetail", True)
        End With
    End Function

    Public Function ddlIndustry(Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("GetIndustry", True)
        End With
    End Function

    Public Function getBranchByUserID(ByVal Lname As String,
                                      Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@Lname", Lname)
            Return .GetDataTable("getBranchByUserID", True)
        End With
    End Function

#End Region

#Region "Save"

    Public Function chckCPAName(ByVal CName As String,
                                Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@lname", CName)
            Return CBool(.ExecScalar("CheckCPANameExists", willCommit))
        End With
    End Function

    Public Function chckCPANames(ByVal CName As String,
                                 ByVal UserBranch As String,
                                 Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@lname", CName)
            .AddInputParam("@branch", UserBranch)
            'Return .ExecScalar("CheckCPANameExistsClients", willCommit)
            Return .ExecScalar("CheckCPANameExistsClients", True)
        End With
    End Function


    Public Function chkIfExistingChkCPANotLost(ByVal CName As String,
                                               Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@lname", CName)
            Return .ExecNonQuery("IfExistingChkCPANotLost", True)
        End With
    End Function

    Public Function getNewCPAID(ByVal UserBranch As String,
                                Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@branch", UserBranch)
            Return .GetDataTable("NewCPAID", True)
        End With
    End Function

    Public Function getCIndustryDesc(ByVal CIndustryID As String,
                                Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@CIndustryID", CIndustryID)
            Return .GetDataTable("GetIndustryDesc", True)
        End With
    End Function

    Public Function AddNewCPA(ByVal newCPAID As String,
                              ByVal CName As String,
                              ByVal CIndustry As String,
                              ByVal userName As String,
                                               Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@CPAID", newCPAID)
            .AddInputParam("@CPAName", CName)
            .AddInputParam("@Industry", CIndustry)
            .AddInputParam("@UploadedBy", userName)
            Return .ExecNonQuery("CreateCPA", True)
        End With
    End Function

    Public Function saveCPAUpdate(ByVal ClientID As String,
                              ByVal ClientName As String,
                              ByVal CName As String,
                              ByVal CIndustryID As String,
                                               Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@CPAID", ClientID)
            .AddInputParam("@CPAName", CName)
            .AddInputParam("@TempName", ClientName)
            .AddInputParam("@IndustryID", CIndustryID)
            Return .ExecNonQuery("EditCPA", True)
        End With
    End Function

#End Region

End Class