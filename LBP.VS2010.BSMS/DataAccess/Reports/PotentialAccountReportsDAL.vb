Public Class PotentialAccountReportsDAL
    Inherits MasterDAL

#Region "DECLARATION"
    Dim message As String

    ReadOnly Property errorMessage As String
        Get
            Return message
        End Get
    End Property
#End Region

#Region "INSTANTIATION/NEW"
    ' ALWAYS OVERRIDE THE MODULENAME PER DAL
    Sub New()
        MyBase.moduleName = "PotentialAccountReports"
    End Sub
#End Region
#Region "FUNCTIONS"
#Region "REPORTS"
    Public Function GeneratePotentialAccountReports(ByVal BrCode As String, ByVal ReportType As String) As DataTable
        Dim dt As New DataTable
        With Me
            .AddInputParam("@BrCode", BrCode)
            .AddInputParam("@ReportType", ReportType)
            dt = .GetDataTable("GetPotentialAccountReports", False)
        End With

        Return dt
    End Function

    Public Function GenerateReport_CPA_NotLead(ByVal UploadedBy As String) As DataTable
        Dim dt As New DataTable
        With Me
            .AddInputParam("@UploadedBy", IIf(Not String.IsNullOrEmpty(UploadedBy), UploadedBy, DBNull.Value))
            dt = .GetDataTable("Report_CPAsNotYetLeads", False)
        End With

        Return dt
    End Function
    Public Function GenerateReport_CPA_Lead(ByVal UploadedBy As String) As DataTable
        Dim dt As New DataTable
        With Me
            .AddInputParam("@UploadedBy", IIf(Not String.IsNullOrEmpty(UploadedBy), UploadedBy, DBNull.Value))
            dt = .GetDataTable("Report_CPAsTaggedAsLead", False)
        End With

        Return dt
    End Function
#End Region
#End Region
End Class
