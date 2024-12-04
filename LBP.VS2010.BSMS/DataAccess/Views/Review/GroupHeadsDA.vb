Public Class GroupHeadsDA
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
    Public Function GetListGroupHeads(ByVal RoleID As String, Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dt As New DataTable
        Try
            With Me
                .AddInputParam("@roleID", RoleID)
                dt = .GetDataTable("GetGroupHeadList", willCommit)
            End With
        Catch ex As Exception
            message = ex.Message
        End Try
        Return dt
    End Function

    Public Function GroupList(Optional ByVal willCommit As Boolean = True) As DataTable
        With Me
            Return .GetDataTable("GetRegionInfo", willCommit)
        End With
    End Function
#End Region
#Region "Check if exists"
    Public Function CheckIfHeadGroupExist(ByVal RoleName As String, ByVal RegBrCode As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            AddInputParam("@position", RoleName)
            AddInputParam("@regbrcode", RegBrCode)
            Return .ExecScalar("CheckRegionHead", willCommit)
        End With
    End Function
    Public Function CheckIfUserNameExist(ByVal GroupName As String, Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            AddInputParam("@username", GroupName) '7142023
            Return .ExecScalar("CheckUserExists", willCommit)
        End With
    End Function
#End Region

#Region "Insert/Update"

    Public Function SaveGroupHead(ByVal Firstname As String,
                                    ByVal MiddleInitial As String,
                                      ByVal Lastname As String,
                                      ByVal Fullname As String,
                                       ByVal RegBrCode As String,
                                           ByVal BrCode As String,
                                            ByVal RoleName As String,
                                            ByVal UserName As String,
                                    ByVal UserID As String,
                                   Optional ByVal willCommit As Boolean = True) As Boolean
        With Me
            .AddInputParam("@username", UserName)
            .AddInputParam("@password", "")
            .AddInputParam("@position", "6")
            .AddInputParam("@firstname", Firstname)
            .AddInputParam("@middleinitial", MiddleInitial)
            .AddInputParam("@lastname", Lastname)
            .AddInputParam("@fullname", Fullname)
            .AddInputParam("@brCode", BrCode)
            .AddInputParam("@regbrcode", RegBrCode)
            'cmd.Parameters.AddWithValue("@targetleads", targetleads)
            .AddInputParam("@targetleadsNCASA", "")
            .AddInputParam("@targetleadsNCBG", "")
            .AddInputParam("@targetleadsECASA", "")
            .AddInputParam("@targetleadsECBG", "")
            .AddInputParam("@addedByUserName", UserID)
            .AddInputParam("@computerName", "")

            Return .ExecNonQuery("AddUser", willCommit)
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
