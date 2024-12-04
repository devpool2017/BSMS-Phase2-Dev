Public Class EncodingDatesDAL
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
        MyBase.moduleName = "Encoding Dates"
    End Sub
#End Region

    Public Function GetCPADates(groupCode As String) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@regionCode", groupCode)
                dt = .GetDataTable("GetCPADatesRH", False)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function EditCPADates(username As String, groupCode As String, PAStartDate As String, PAEndDate As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@CPAStartDate", PAStartDate)
            .AddInputParam("@CPAEndDate", PAEndDate)
            .AddInputParam("@username", username)
            .AddInputParam("@regionCode", groupCode)

            Return .ExecNonQuery("EditCPADatesRHTemp", willCommit)
        End With
    End Function

    Public Function ApproveCPADates(username As String, groupCode As String, action As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me

            .AddInputParam("@regionCode", groupCode)
            .AddInputParam("@action", action)
            .AddInputParam("@username", username)
            Return .ExecNonQuery("EditCPADatesRH", willCommit)
        End With
    End Function


    Public Function RejectCPADates(username As String, groupCode As String, action As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me

            .AddInputParam("@regionCode", groupCode)
            .AddInputParam("@action", action)
            .AddInputParam("@username", username)
            Return .ExecNonQuery("DeleteCPADatesRHTemp", willCommit)
        End With
    End Function

    Public Function GetMergeDetails(regionCode As String) As DataTable
        Dim dt As New DataTable

        With Me
            .AddInputParam("@RegionCode", regionCode)
            dt = .GetDataTable("CPADates_GetMergeDetails", False)
        End With

        Return dt
    End Function
End Class
