Public Class GroupsDA
    Inherits MasterDAL
#Region "DECLARATION"
    Dim message As String
    ReadOnly Property errorMessage As String
        Get
            Return message
        End Get
    End Property
#End Region

#Region "LIST"
    Public Function GetListGroup(Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                dt = .GetDataTable("GetRegionInfo", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function
#End Region
#Region "Check if exists"
    Public Function CheckIfCodeExist(ByVal GroupCode As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            AddInputParam("@regioncode", GroupCode) '7142023
            Return .ExecScalar("CheckRegionExists", willCommit)
        End With
    End Function
    Public Function CheckIfNameExist(ByVal GroupName As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            AddInputParam("@regionname", GroupName) '7142023
            Return .ExecScalar("CheckRegionNameExists", willCommit)
        End With
    End Function
#End Region

#Region "Insert/Update"

    Public Function SaveGroup(ByVal GroupCode As String,
                                    ByVal GroupName As String,
                                    ByVal UserID As String,
                                   Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@regioncode", GroupCode)
            .AddInputParam("@regionname", GroupName)
            .AddInputParam("@userName", UserID)
            Return .ExecNonQuery("AddRegion", willCommit)
        End With
    End Function

    Public Function UpdateGroup(ByVal GroupCode As String,
                                 ByVal GroupName As String,
                                 ByVal UserID As String,
                                Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@regioncode", GroupCode)
            .AddInputParam("@regionname", GroupName)
            .AddInputParam("@userName", UserID)
            Return .ExecNonQuery("EditRegion", willCommit)
        End With
    End Function

#End Region
End Class
