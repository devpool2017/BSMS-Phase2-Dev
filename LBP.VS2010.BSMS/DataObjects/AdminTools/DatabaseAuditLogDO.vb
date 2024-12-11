Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.DataAccess
Imports LBP.VS2010.BSMS.CustomValidators

Public Class DatabaseAuditLogDO
    Inherits MasterDO
#Region "INSTANTIATION"
    Sub New()

    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
#End Region

    <DisplayLabel("User ID")>
    <RequiredField()>
    <DataColumnMapping("UserID")>
    Property UserID As String

    <DisplayLabel("Date")>
    <RequiredField()>
    <DataColumnMapping("AuditTrailDate")>
    Property AuditTrailDate As String

    <DisplayLabel("Table Name")>
    <RequiredField()>
    <DataColumnMapping("TableName")>
    Property TableName As String

    <DataColumnMapping("ColumnAffected")>
    Property ColumnAffected As String

    <DisplayLabel("Action Type")>
    <RequiredField()>
    <DataColumnMapping("ActionType")>
    Property ActionType As String

    <DataColumnMapping("ColumnFrom")>
    Property ColumnFrom As String

    <DataColumnMapping("ColumnTo")>
    Property ColumnTo As String

    Property SearchBy As String
    ' Property isSuccess As Boolean

#Region "LIST/GET"
    Shared Function ListAuditTrail(ByVal DomainID As String, ByVal TableName As String, ByVal ActionType As String, ByVal AuditTrailDate As String) As List(Of DatabaseAuditLogDO)
        Dim dal As New DatabaseAuditLogDAL
        Dim dt As DataTable = dal.listAuditTrail(DomainID, TableName, ActionType, AuditTrailDate)

        If Not IsNothing(dt) Then
            Dim lst As New List(Of DatabaseAuditLogDO)

            If dt.Rows.Count > 0 Then

                For Each row As DataRow In dt.Rows
                    lst.Add(New DatabaseAuditLogDO With {
                            .UserID = row.Item("UserID").ToString,
                            .AuditTrailDate = row.Item("AuditTrailDate").ToString,
                            .TableName = row.Item("TableName").ToString,
                            .ColumnAffected = row.Item("ColumnAffected").ToString,
                            .ActionType = row.Item("ActionType").ToString,
                            .ColumnFrom = row.Item("ColumnFrom").ToString,
                            .ColumnTo = row.Item("ColumnTo").ToString
                    })
                Next
            End If

            Return lst
        End If

        Return Nothing
    End Function
#End Region
End Class
