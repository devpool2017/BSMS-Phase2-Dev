Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.DataAccess
Imports LBP.VS2010.BSMS.CustomValidators

Public Class UsersActivityLogDO
    Inherits MasterDO
#Region "INSTANTIATION"
    Sub New()

    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
#End Region

    <DisplayLabel("Module Name")>
    <RequiredField()>
    <DataColumnMapping("ModuleName")>
    Property ModuleName As String

    <DisplayLabel("Activity Type")>
    <RequiredField()>
    <DataColumnMapping("ActivityType")>
    Property ActivityType As String

    <DisplayLabel("UserName")>
    <RequiredField()>
    <DataColumnMapping("UserName")>
    Property UserName As String

    <DisplayLabel("Activity Date")>
    <RequiredField()>
    <DataColumnMapping("ActivityDate")>
    Property ActivityDate As String

    <DataColumnMapping("IPAddress")>
    Property IPAddress As String

    <DisplayLabel("Browser")>
    <RequiredField()>
    <DataColumnMapping("Browser")>
    Property Browser As String

    Property SearchBy As String
    ' Property isSuccess As Boolean

#Region "LIST/GET"
    Shared Function ListUsersActivity(ByVal ModuleName As String, ByVal ActivityType As String, ByVal UserName As String, ByVal ActivityDate As String, ByVal Browser As String) As List(Of UsersActivityLogDO)
        Dim dal As New UsersActivityLogDAL
        Dim dt As DataTable = dal.listUsersActivity(ModuleName, ActivityType, UserName, ActivityDate, Browser)

        If Not IsNothing(dt) Then
            Dim lst As New List(Of UsersActivityLogDO)

            If dt.Rows.Count > 0 Then

                For Each row As DataRow In dt.Rows
                    lst.Add(New UsersActivityLogDO With {
                            .ModuleName = row.Item("ModuleName").ToString,
                            .ActivityType = row.Item("ActivityType").ToString,
                            .UserName = row.Item("UserName").ToString,
                            .ActivityDate = row.Item("ActivityDate").ToString,
                            .IPAddress = row.Item("IPAddress").ToString,
                            .Browser = row.Item("Browser").ToString
                    })
                Next
            End If

            Return lst
        End If

        Return Nothing
    End Function
#End Region
End Class
