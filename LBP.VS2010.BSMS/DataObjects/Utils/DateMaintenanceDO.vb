Imports LBP.VS2010.BSMS.CustomValidators
Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.DataAccess
Imports LBP.VS2010.BSMS.Utilities

Public Class DateMaintenanceDO
    Inherits MasterDO

#Region "DECLARATION"
    Property Year As String
    Property Month As String
    Property WeekNumber As String
    Property FromDate As String
    Property ToDate As String
    Property DateToday As String
#End Region

#Region "FUNCTION"

    Shared Function GetCurrentDateDefaultValue() As DateMaintenanceDO
        Dim dal As New DateMaintenanceDAL
        Dim dt As DataTable = dal.GetCurrentDateDefaultValue()
        If Not dt.Rows.Count = 0 Then
            Dim obj As New DateMaintenanceDO
            Dim row As DataRow = dt.Rows(0)
            obj.WeekNumber = row("WeekNumber").ToString()
            obj.Month = row("MonthName").ToString()
            obj.Year = row("Year").ToString()
            Return obj
        End If

        Return Nothing
    End Function

    Shared Function GetYear() As DataTable
        Dim dal As New DateMaintenanceDAL
        Return dal.GetYear()
    End Function

    Shared Function GetMonth() As DataTable
        Dim dt As New DataTable
        dt.Columns.Add("MonthCode")
        dt.Columns.Add("Month")
        For i As Integer = 1 To 12
            Dim dr As DataRow = dt.NewRow
            dr("MonthCode") = i
            dr("Month") = MonthName(i)
            dt.Rows.Add(dr)
        Next
        Return dt
    End Function

    Shared Function GetWeek(ByVal year As String,
                            ByVal month As String) As DataTable
        Dim dal As New DateMaintenanceDAL
        Dim maxweek As String = dal.GetMaxWeekNum(year, month)
        Dim minweek As String = dal.GetMinWeekNum(year, month)

        If minweek = Nothing Or minweek <> "1" Then
            minweek = "1"
        End If

        If maxweek = Nothing Or maxweek = minweek Then
            maxweek = "4"
        End If

        Dim dt As New DataTable
        dt.Columns.Add("WeekValue")
        dt.Columns.Add("WeekDescription")
        For week As Integer = CInt(minweek) To CInt(maxweek)
            Dim dr As DataRow = dt.NewRow
            dr("WeekDescription") = "Week " & week
            dr("WeekValue") = week
            dt.Rows.Add(dr)
        Next

        Return dt
    End Function

    Shared Function GetDate(ByVal year As String,
                            ByVal month As String,
                            ByVal week As String) As DateMaintenanceDO
        Dim dal As New DateMaintenanceDAL
        Dim obj As New DateMaintenanceDO
        obj.FromDate = dal.GetFromDate(year, month, week)
        obj.ToDate = dal.GetToDate(year, month, week)
        Return obj
    End Function
#End Region
End Class
