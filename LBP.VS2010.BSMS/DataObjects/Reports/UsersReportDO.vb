Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.DataAccess

Public Class UsersReportDO
    Inherits MasterDO

#Region "INSTANTIATION"
    Sub New()

    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
#End Region

#Region "DECLARATION"

    Property Role As String
    Property Status As String
    Property DateType As String
    Property DateFrom As String
    Property DateTo As String

    Property ReportPage As String = "ReportViewer.aspx"

#End Region

#Region "FUNCTION"

    Public Function validateParam() As Boolean
        Dim success As Boolean = True

        If Not String.IsNullOrEmpty(DateType) Then

            If String.IsNullOrEmpty(DateFrom) Then
                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, "Date From", {}) + "<br/>"
                success = False
            End If

            If String.IsNullOrEmpty(DateTo) Then
                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.EMPTY, "Date To", {}) + "<br/>"
                success = False
            End If

            If success Then
                If CDate(DateFrom) > CDate(DateTo) Then
                    errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.GREATER_VALUE, "Date To", {"Date From"}) + "<br/>"
                    success = False
                End If
            End If
        End If

        Return success
    End Function

    'Public Function GenerateReport() As DataTable
    '    Dim dal As New UsersDAL

    '    Return dal.GenerateReport(Role, Status, DateType, DateFrom, DateTo)

    'End Function

#End Region


End Class
