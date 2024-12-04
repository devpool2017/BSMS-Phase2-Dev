Imports LBP.VS2010.BSMS.CustomValidators
Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.DataAccess

Public Class ChangeInPotentialAccountsDO
    Inherits MasterDO

#Region "DECLARATION"
    Property GroupCode As String
    Property BranchManagerName As String
    '<DisplayLabel("User Name")>
    '<RequiredField(RequiredField.ControlType.SELECTION)>
    Property Username As String
    Property BranchCode As String
    Property BranchName As String

    Property DisplayName As String

    <DisplayLabel("Year")>
    <RequiredField(RequiredField.ControlType.SELECTION)>
    Property Year As String
    'Property Month As String

    <DisplayLabel("Month")>
    <RequiredField(RequiredField.ControlType.SELECTION)>
    Property Month As String

    <DisplayLabel("Week")>
    <RequiredField(RequiredField.ControlType.SELECTION)>
    Property WeekNumber As String

    Property DateFrom As String
    Property DateToday As String

    'For Gridview
    <DataColumnMapping("Name")>
    Property PotentialAccountName As String

    <DataColumnMapping("Industry")>
    Property Industry As String

    <DataColumnMapping("ActionTaken")>
    Property ActionTaken As String

    <DataColumnMapping("ValueBefore")>
    Property ValueBefore As String

    <DataColumnMapping("ValueAfter")>
    Property ValueAfter As String

    <DataColumnMapping("ChangeDate")>
    Property ChangeDate As String
#End Region

#Region "FUNCTIONS"
    Public Function GetRegionList() As DataTable
        Dim dal As New ChangeInPotentialAccountsDAL

        Return dal.GetRegionList()
    End Function

    Public Function GetUsersList(ByVal RoleID As String) As DataTable
        Dim dal As New ChangeInPotentialAccountsDAL

        Return dal.GetUsersList(GroupCode, BranchCode, RoleID)
    End Function


    Public Function GetBranchesList(ByVal groupCode As String) As DataTable
        Dim dal As New ChangeInPotentialAccountsDAL
        Dim dt As DataTable = dal.GetBranchesList(groupCode)
        dt.DefaultView.Sort = "BranchCode ASC"
        Return dt.DefaultView.ToTable
    End Function

    Shared Function GetPotentialAccountsList(ByVal BranchCode As String,
                                    ByVal DateFrom As String,
                                    ByVal DateToday As String,
                                    ByVal Year As String,
                                    ByVal Month As String) As List(Of ChangeInPotentialAccountsDO)
        Dim dal As New ChangeInPotentialAccountsDAL
        Dim dt As DataTable = dal.GetPotentialAccountsList(BranchCode, DateFrom, DateToday, Year, Month)
        If Not IsNothing(dt) Then
            Dim lst As New List(Of ChangeInPotentialAccountsDO)
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    lst.Add(
                        New ChangeInPotentialAccountsDO With {
                        .PotentialAccountName = row.Item("Name").ToString,
                        .Industry = row.Item("Industry").ToString,
                        .ActionTaken = row.Item("ActionTaken").ToString,
                        .ValueBefore = row.Item("BeforeValue").ToString,
                        .ValueAfter = row.Item("AfterValue").ToString,
                        .ChangeDate = row.Item("ChangeDate").ToString,
                        .Username = row.Item("Username").ToString
                        })
                Next
            End If
            Return lst
        End If

        Return Nothing

    End Function

    Shared Function GetWeekNum(ByVal DateToday As String) As String
        Dim dal As New ChangeInPotentialAccountsDAL
        Dim dt As DataTable = dal.GetWeekNum(DateToday)

        Return IIf((dt.Rows(0).Item("WeekNumber").ToString <> ""), dt.Rows(0).Item("WeekNumber"), "")
    End Function

    Public Function GenerateReport(ByVal BranchCode As String,
                                   ByVal FromDate As String,
                                   ByVal ToDate As String,
                                   ByVal Year As String,
                                   ByVal Month As String) As DataTable
        Dim dal As New ChangeInPotentialAccountsDAL

        Return dal.GetPotentialAccountsList(BranchCode, FromDate, ToDate, Year, Month)

    End Function
#End Region
End Class
