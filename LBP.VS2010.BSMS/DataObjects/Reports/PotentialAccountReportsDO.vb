Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.DataAccess
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables
Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.CustomValidators
Public Class PotentialAccountReportsDO
    Inherits MasterDO

#Region "INSTANTIATION"
    Sub New()

    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
#End Region

#Region "DECLARATION"

    <DisplayLabel("PotentialAccountReport")>
    <RequiredField(RequiredField.ControlType.SELECTION)>
    <ControlVariable(FieldVariables.CPA_REPORT)>
    Property SelectReport As String

    Property UploadedBy As String
    Property BrCode As String
    Property LoggedName As String
    Property ReportType As String

    Property ReportPage As String = "ReportViewer.aspx"

#End Region

#Region "FUNCTION"

    Public Function validateParam() As Boolean
        Dim success As Boolean = True

        If String.IsNullOrEmpty(SelectReport) Then
            success = False
            errMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.CPA_REPORT, Nothing, Nothing)
        End If

        Return success
    End Function


    Public Function GeneratePotentialAccountReports(ByVal brCode As String, ByVal reportType As String) As DataTable
        Dim dal As New PotentialAccountReportsDAL

        Return dal.GeneratePotentialAccountReports(brCode, reportType)

    End Function

    Public Function GenerateReport_CPA_NotLead() As DataTable
        Dim dal As New PotentialAccountReportsDAL

        Return dal.GenerateReport_CPA_NotLead(UploadedBy)

    End Function
    Public Function GenerateReport_CPA_Lead() As DataTable
        Dim dal As New PotentialAccountReportsDAL

        Return dal.GenerateReport_CPA_Lead(UploadedBy)

    End Function

#End Region


End Class
