Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.DataAccess


Public Class ActivityLog
    Inherits MasterDO
#Region "INSTANTIATION"
    Sub New()

    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection, ByVal pol As ProfileDO)
        MyBase.New(dr, columnNames, pol)
    End Sub

#End Region

#Region "DECLARATION"
#Region "TABLE COLUMNS"


    <DataColumnMapping("ActivityLogID")>
    Property ActivityLogID As String

    <DataColumnMapping("ModuleName")>
    Property ModuleName As String

    <DataColumnMapping("ActivityType")>
    Property ActivityType As String

    <DataColumnMapping("UserName")>
    Property UserName As String

    <DataColumnMapping("ActivityDate")>
    Property ActivityDate As String

    <DataColumnMapping("IPAddress")>
    Property IPAddress As String

    <DataColumnMapping("Browser")>
    Property Browser As String
#End Region
#Region "MISC VARIABLES"
    Enum ActionTaken
        VIEW
        CREATE
        UPDATE
        DELETE
        LOGIN
        FAILED_LOGIN
        APPROVE
        REJECT
        SEARCH
        LIST
        LOGOUT
        INVALID_ACCESS
        UNLOCK
        DEACTIVATE
        ACTIVATE
    End Enum
#End Region
#End Region
#Region "FUNCTIONS"
    Public Function WriteLog() As Boolean
        Dim dal As New ActivityLogDAL

        If Not dal.WriteLog(ModuleName, ActivityType, UserName, IPAddress, Browser) Then
            errMsg = dal.ErrorMsg

            Return False
        End If

        Return True
    End Function

#End Region

End Class
