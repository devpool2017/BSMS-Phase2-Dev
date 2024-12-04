Imports LBP.VS2010.BSMS.DataAccess

Public Class OtherParametersDO
    Inherits MasterDO

#Region "INSTANTIATION"
    Sub New()
    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
#End Region

#Region "TABLE COLUMNS"
    Property TargetLeads As String
    Property TargetADB As String
    Property TargetNewAccountsClosed As String
    Property DaysBefore As String
    Property CustomerDaysBefore As String
    Property EditVisitTag As String
    Property CPAYear As String
    Property MaximumSearchCount As String

    Property ParameterValue As String
#End Region

#Region "FUNCTIONS"
    Shared Function GetOtherParameters() As List(Of OtherParametersDO)
        Dim dal As New OtherParametersDAL
        Dim dt As DataTable = dal.GetOtherParameters()
        If Not IsNothing(dt) Then
            Dim lst As New List(Of OtherParametersDO)
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    lst.Add(
                        New OtherParametersDO With {
                        .TargetLeads = row.Item("TargetLeads").ToString,
                        .TargetADB = row.Item("TargetADB").ToString,
                        .TargetNewAccountsClosed = row.Item("TargetNewAccountsClosed").ToString,
                        .DaysBefore = row.Item("DaysBefore").ToString,
                        .CustomerDaysBefore = row.Item("CustomerDaysBefore").ToString,
                        .EditVisitTag = row.Item("EditVisitTag").ToString,
                        .CPAYear = row.Item("CPAYear").ToString,
                        .MaximumSearchCount = row.Item("MaximumSearchCount").ToString
                        })
                Next
            End If
            Return lst
        End If

        Return Nothing

    End Function

    Shared Function GetOtherParametersRH(ByVal RegionCode As String) As List(Of OtherParametersDO)
        Dim dal As New OtherParametersDAL
        Dim dt As DataTable = dal.GetOtherParametersRH(RegionCode)
        If Not IsNothing(dt) Then
            Dim lst As New List(Of OtherParametersDO)
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    lst.Add(
                        New OtherParametersDO With {
                        .ParameterValue = row.Item("ParameterValue").ToString
                        })
                Next
            End If
            Return lst
        End If

        Return Nothing
    End Function
#End Region
End Class
