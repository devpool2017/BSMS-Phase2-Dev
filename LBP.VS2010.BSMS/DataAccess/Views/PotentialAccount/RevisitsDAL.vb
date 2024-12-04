Public Class RevisitsDAL
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
        moduleName = "Revisits"
    End Sub
#End Region

#Region "LIST"
    Public Function GetListAccount(ByVal UserID As String,
                                   ByVal Tag As String,
                                   Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@Tag", Tag)
                .AddInputParam("@UploadedBy", UserID)
                dt = .GetDataTable("GetCPAsTaggedAsProspect", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function AccountDetails(ByVal ClientID As String, Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            .AddInputParam("@clientid", ClientID)
            Return .GetDataTable("GetClientInfo", True)
        End With
    End Function

    Public Function GetListRevisit(ByVal ClientID As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@clientID", ClientID)
                dt = .GetDataTable("GetCPAProspectWeeklyActivityHistory", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function GetAllListRevisit(ByVal ClientID As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@clientID", ClientID)
                dt = .GetDataTable("GetCPAProspectWeeklyActivityHistoryAll", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function GetRevisitCount(ByVal ClientID As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@clientID", ClientID)
                dt = .GetDataTable("GetCPAProspectCount", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function GetRemarks(ByVal ClientID As String,
                                ByVal UserID As String,
                                Optional ByVal willCommit As Boolean = True) As String
        With Me
            .AddInputParam("@clientID", If(Not String.IsNullOrWhiteSpace(ClientID), ClientID, Nothing))
            .AddInputParam("@uploadedBy", If(Not String.IsNullOrWhiteSpace(UserID), UserID, Nothing))
            Return .ExecScalar("Revisits_GetRemarks", willCommit)
        End With
    End Function

    Public Function GetRemarksNotBM(ByVal ClientID As String,
                                Optional ByVal willCommit As Boolean = True) As String
        With Me
            .AddInputParam("@clientID", If(Not String.IsNullOrWhiteSpace(ClientID), ClientID, Nothing))
            Return .ExecScalar("Revisits_GetRemarksNotBM", willCommit)
        End With
    End Function

#End Region

#Region "ADD"
    Public Function addProspectDate(ByVal ClientID As String,
                                 ByVal UserID As String,
                                 ByVal Prospect As String,
                                 Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@clientID", ClientID)
            .AddInputParam("@uploadedBy", UserID)
            .AddInputParam("@prospectDate", Prospect)
            Return .ExecNonQuery("AddProspectActivity", willCommit)
        End With
    End Function

    Public Function updateCPAClient(ByVal ClientID As String,
                                 ByVal Prospect As String,
                                 ByVal Visits As String,
                                 ByVal NewComments As String,
                                 Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@clientID", ClientID)
            .AddInputParam("@prospect", Prospect)
            .AddInputParam("@visits", Visits)
            .AddInputParam("@remarks", NewComments)
            Return .ExecNonQuery("UpdateCPAClients", willCommit)
        End With
    End Function

    Public Function updateCPAClientComment(ByVal ClientID As String,
                                           ByVal UserID As String,
                                           ByVal NewComments As String,
                                           Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@clientID", ClientID)
            .AddInputParam("@uploadedBy", UserID)
            .AddInputParam("@remarks", NewComments)
            Return .ExecNonQuery("UpdateCPAClientsComment", willCommit)
        End With
    End Function

    Public Function updateCPAClientCommentNotBM(ByVal ClientID As String,
                                           ByVal NewComments As String,
                                           Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@clientID", ClientID)
            .AddInputParam("@remarks", NewComments)
            Return .ExecNonQuery("UpdateCPAClientsCommentNotBM", willCommit)
        End With
    End Function

#End Region

End Class
