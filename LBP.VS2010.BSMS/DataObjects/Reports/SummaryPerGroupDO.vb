Imports LBP.VS2010.BSMS.CustomValidators
Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.DataAccess
Imports LBP.VS2010.BSMS.Utilities

Public Class SummaryPerGroupDO
    Inherits MasterDO
#Region "DECLARATION"
    Property moduleName As String

    <DisplayLabel("Group")>
<RequiredField(RequiredField.ControlType.SELECTION)>
    Property GroupCode As String
    Property GroupCodeText As String

    <DisplayLabel("Year")>
<RequiredConditional.ValueEquals("moduleName", {"Weekly", "Monthly", "Annual"}, RequiredField.ControlType.SELECTION)>
    Property Year As String
    Property Month As String

    <DisplayLabel("Month")>
<RequiredConditional.ValueEquals("moduleName", {"Weekly", "Monthly"}, RequiredField.ControlType.SELECTION)>
    Property MonthCode As String

    <DisplayLabel("Week")>
<RequiredConditional.ValueEquals("moduleName", {"Weekly"}, RequiredField.ControlType.SELECTION)>
    Property WeekNumber As String

    Property DateFrom As String
    Property DateToday As String

    Property ClientType As String
    Property IndustryType As String
    Property ADBRangeName As String

    Property ClientTypeDesc As String
    Property IndustryTypeDesc As String
    Property ADBRangeNameDesc As String

    Property BranchCode As String
    Property BranchName As String
    Property TotalLeads As String
    Property TotalSuspects As String
    Property TotalProspects As String
    Property TotalCustomers As String
    Property TotalCASA As String
    Property TotalLoans As String
    Property TotalLosts As String
    Property TotalInitialDeposit As String
    Property TotalADB As String
    Property TotalLedgerBalance As String
    Property LoanAmountReported As String
    Property LoanReleaseAmount As String

    Property TotalLoanAmountReported As String
    Property TotalLoanReleaseAmount As String

    Property SalesProspect As String
    Property DateEncoded As String
    Property CustomerDate As String
    Property AccountNumber As String
    Property ADB As String
    Property CASAProductsAvailed As String

    Property LoansProductAvailed As String
    Property DateReleased As String
    Property LoanReleasedAmount As String
    Property ClientID As String

    Property TotalCPARevisits As String
    Property NewCustCasa As String
    Property NewCustLoan As String
    Property NewCustomerTotal As String
    Property RoleID As String
    Property isAdmin As Boolean

    Property rptHeader As String
#End Region


#Region "FUNCTION"
#Region "WEEKLY SUMMARY REPORT FUNCTION"
    Shared Function GetWeeklyList(ByVal obj As SummaryPerGroupDO) As List(Of SummaryPerGroupDO)
        Dim dal As New SummaryPerGroupDAL
        Dim dt As DataTable = dal.GetWeeklyList(obj.GroupCode, obj.Year, obj.Month, obj.MonthCode, obj.WeekNumber, obj.ClientType, obj.RoleID)
        If Not IsNothing(dt) Then
            Dim lst As New List(Of SummaryPerGroupDO)
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    lst.Add(
                        New SummaryPerGroupDO With {
                        .BranchName = row.Item("BranchName").ToString,
                        .TotalCPARevisits = row.Item("TotalCPARevisits").ToString,
                        .TotalLeads = row.Item("TotalLeads").ToString,
                        .TotalSuspects = row.Item("TotalSuspects").ToString,
                        .TotalProspects = row.Item("TotalProspects").ToString,
                        .TotalCustomers = row.Item("TotalCustomers").ToString,
                        .TotalCASA = row.Item("TotalCASA").ToString,
                        .TotalADB = If(row.Item("TotalADB") IsNot DBNull.Value, Decimal.Parse(row.Item("TotalADB")).ToString("N"), "0.00"),
                        .TotalInitialDeposit = If(row.Item("TotalInitialDeposit") IsNot DBNull.Value, Decimal.Parse(row.Item("TotalInitialDeposit")).ToString("N"), "0.00"),
                        .TotalLosts = If(row.Item("TotalLosts") IsNot DBNull.Value, row.Item("TotalLosts").ToString, "0"),
                        .TotalLedgerBalance = If(row.Item("TotalLedgerBalance") IsNot DBNull.Value, Decimal.Parse(row.Item("TotalLedgerBalance")).ToString("N"), "0.00"),
                        .LoanAmountReported = If(row.Item("LoanAmountReported") IsNot DBNull.Value, Decimal.Parse(row.Item("LoanAmountReported")).ToString("N"), "0.00"),
                        .LoanReleaseAmount = If(row.Item("LoanReleaseAmount") IsNot DBNull.Value, Decimal.Parse(row.Item("LoanReleaseAmount")).ToString("N"), "0.00"),
                        .NewCustCasa = row.Item("NewCustCasa").ToString,
                        .NewCustLoan = row.Item("NewCustLoan").ToString,
                        .NewCustomerTotal = row.Item("NewCustTotal").ToString
})
                Next
            End If
            Return lst
        End If

        Return Nothing

    End Function

    Shared Function GetWeeklyListTotal(ByVal param As SummaryPerGroupDO) As SummaryPerGroupDO
        Dim dal As New SummaryPerGroupDAL
        Dim dt As DataTable = dal.GetWeeklyList(param.GroupCode, param.Year, param.Month, param.MonthCode, param.WeekNumber, param.ClientType, param.RoleID)
        If Not IsNothing(dt) Then
            Dim obj As New SummaryPerGroupDO
            If dt.Rows.Count > 0 Then
                obj.TotalLeads = Convert.ToInt32(dt.Compute("SUM(TotalLeads)", String.Empty)).ToString("N0")
                obj.TotalCPARevisits = Convert.ToInt32(dt.Compute("SUM(TotalCPARevisits)", String.Empty)).ToString("N0")
                obj.TotalSuspects = Convert.ToInt32(dt.Compute("SUM(TotalSuspects)", String.Empty)).ToString("N0")
                obj.TotalProspects = Convert.ToInt32(dt.Compute("SUM(TotalProspects)", String.Empty)).ToString("N0")
                obj.TotalCustomers = Convert.ToInt32(dt.Compute("SUM(TotalCustomers)", String.Empty)).ToString("N0")
                obj.TotalCASA = Convert.ToInt32(dt.Compute("SUM(TotalCASA)", String.Empty)).ToString("N0")
                obj.NewCustCasa = Convert.ToInt32(dt.Compute("SUM(NewCustCasa)", String.Empty)).ToString("N0")
                obj.NewCustLoan = Convert.ToInt32(dt.Compute("SUM(NewCustLoan)", String.Empty)).ToString("N0")
                obj.NewCustomerTotal = Convert.ToInt32(dt.Compute("SUM(NewCustTotal)", String.Empty)).ToString("N0")
                obj.TotalADB = Convert.ToDecimal(dt.Compute("SUM(TotalADB)", String.Empty)).ToString("N")
                obj.TotalInitialDeposit = Convert.ToDecimal(dt.Compute("SUM(TotalInitialDeposit)", String.Empty)).ToString("N")
                obj.TotalLosts = Convert.ToInt32(dt.Compute("SUM(TotalLosts)", String.Empty)).ToString("N0")
                obj.TotalLedgerBalance = Convert.ToDecimal(dt.Compute("SUM(TotalLedgerBalance)", String.Empty)).ToString("N")
                obj.TotalLoanAmountReported = Convert.ToDecimal(dt.Compute("SUM(LoanAmountReported)", String.Empty)).ToString("N")
                obj.TotalLoanReleaseAmount = Convert.ToDecimal(dt.Compute("SUM(LoanReleaseAmount)", String.Empty)).ToString("N")
            End If
            Return obj
        End If

        Return Nothing

    End Function


#End Region

#Region "MONTHLY SUMMARY REPORT FUNCTION"
    Shared Function GetMonthlyList(ByVal obj As SummaryPerGroupDO) As List(Of SummaryPerGroupDO)
        Dim dal As New SummaryPerGroupDAL
        Dim dt As DataTable = dal.GetMonthlyList(obj.GroupCode, obj.Year, obj.Month, obj.MonthCode, obj.ClientType, obj.RoleID)
        If Not IsNothing(dt) Then
            Dim lst As New List(Of SummaryPerGroupDO)
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    lst.Add(
                        New SummaryPerGroupDO With {
                        .BranchName = row.Item("BranchName").ToString,
                        .TotalCPARevisits = row.Item("TotalCPARevisits").ToString,
                        .TotalLeads = row.Item("TotalLeads").ToString,
                        .TotalSuspects = row.Item("TotalSuspects").ToString,
                        .TotalProspects = row.Item("TotalProspects").ToString,
                        .TotalCustomers = row.Item("TotalCustomers").ToString,
                        .TotalCASA = row.Item("TotalCASA").ToString,
                        .TotalADB = If(row.Item("TotalADB") IsNot DBNull.Value, Decimal.Parse(row.Item("TotalADB")).ToString("N"), "0.00"),
                        .TotalInitialDeposit = If(row.Item("TotalInitialDeposit") IsNot DBNull.Value, Decimal.Parse(row.Item("TotalInitialDeposit")).ToString("N"), "0.00"),
                        .TotalLosts = If(row.Item("TotalLosts") IsNot DBNull.Value, row.Item("TotalLosts").ToString, "0"),
                        .TotalLedgerBalance = If(row.Item("TotalLedgerBalance") IsNot DBNull.Value, Decimal.Parse(row.Item("TotalLedgerBalance")).ToString("N"), "0.00"),
                        .LoanAmountReported = If(row.Item("LoanAmountReported") IsNot DBNull.Value, Decimal.Parse(row.Item("LoanAmountReported")).ToString("N"), "0.00"),
                        .LoanReleaseAmount = If(row.Item("LoanReleaseAmount") IsNot DBNull.Value, Decimal.Parse(row.Item("LoanReleaseAmount")).ToString("N"), "0.00"),
                        .NewCustCasa = row.Item("NewCustCasa").ToString,
                        .NewCustLoan = row.Item("NewCustLoan").ToString,
                        .NewCustomerTotal = row.Item("NewCustTotal").ToString
})
                Next
            End If
            Return lst
        End If

        Return Nothing

    End Function

    Shared Function GetMonthlyListTotal(ByVal param As SummaryPerGroupDO) As SummaryPerGroupDO
        Dim dal As New SummaryPerGroupDAL
        Dim dt As DataTable = dal.GetMonthlyList(param.GroupCode, param.Year, param.Month, param.MonthCode, param.ClientType, param.RoleID)
        If Not IsNothing(dt) Then
            Dim obj As New SummaryPerGroupDO
            If dt.Rows.Count > 0 Then
                obj.TotalCPARevisits = Convert.ToInt32(dt.Compute("SUM(TotalCPARevisits)", String.Empty)).ToString("N0")
                obj.TotalLeads = Convert.ToInt32(dt.Compute("SUM(TotalLeads)", String.Empty)).ToString("N0")
                obj.TotalSuspects = Convert.ToInt32(dt.Compute("SUM(TotalSuspects)", String.Empty)).ToString("N0")
                obj.TotalProspects = Convert.ToInt32(dt.Compute("SUM(TotalProspects)", String.Empty)).ToString("N0")
                obj.TotalCustomers = Convert.ToInt32(dt.Compute("SUM(TotalCustomers)", String.Empty)).ToString("N0")
                obj.TotalCASA = Convert.ToInt32(dt.Compute("SUM(TotalCASA)", String.Empty)).ToString("N0")
                obj.NewCustCasa = Convert.ToInt32(dt.Compute("SUM(NewCustCasa)", String.Empty)).ToString("N0")
                obj.NewCustLoan = Convert.ToInt32(dt.Compute("SUM(NewCustLoan)", String.Empty)).ToString("N0")
                obj.NewCustomerTotal = Convert.ToInt32(dt.Compute("SUM(NewCustTotal)", String.Empty)).ToString("N0")
                obj.TotalADB = Convert.ToDecimal(dt.Compute("SUM(TotalADB)", String.Empty)).ToString("N")
                obj.TotalInitialDeposit = Convert.ToDecimal(dt.Compute("SUM(TotalInitialDeposit)", String.Empty)).ToString("N")
                obj.TotalLosts = Convert.ToInt32(dt.Compute("SUM(TotalLosts)", String.Empty)).ToString("N0")
                obj.TotalLedgerBalance = Convert.ToDecimal(dt.Compute("SUM(TotalLedgerBalance)", String.Empty)).ToString("N")
                obj.TotalLoanAmountReported = Convert.ToDecimal(dt.Compute("SUM(LoanAmountReported)", String.Empty)).ToString("N")
                obj.TotalLoanReleaseAmount = Convert.ToDecimal(dt.Compute("SUM(LoanReleaseAmount)", String.Empty)).ToString("N")
            End If
            Return obj
        End If

        Return Nothing

    End Function


#End Region

#Region "ANNUAL SUMMARY REPORT FUNCTION"
    Shared Function GetClientTypeList() As DataTable
        Dim dal As New SummaryPerGroupDAL

        Return dal.GetClientTypes()
    End Function

    Shared Function GetIndustryTypeList() As DataTable
        Dim dal As New SummaryPerGroupDAL

        Return dal.GetAllIndustryType()
    End Function

    Shared Function GetADBRangeList() As DataTable
        Dim dal As New SummaryPerGroupDAL

        Return dal.GetADBSizingRangeName()
    End Function
    Shared Function GetAnnualList(ByVal obj As SummaryPerGroupDO) As List(Of SummaryPerGroupDO)
        Dim dal As New SummaryPerGroupDAL
        Dim dt As DataTable = dal.GetAnnualList(obj.GroupCode, obj.Year, obj.ClientType, obj.IndustryType, obj.ADBRangeName, obj.RoleID)
        If Not IsNothing(dt) Then
            Dim lst As New List(Of SummaryPerGroupDO)
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    lst.Add(
                        New SummaryPerGroupDO With {
                        .BranchCode = row.Item("BranchCode").ToString,
                        .BranchName = row.Item("BranchName").ToString,
                        .TotalLeads = row.Item("TotalLeads").ToString,
                        .TotalSuspects = row.Item("TotalSuspects").ToString,
                        .TotalProspects = row.Item("TotalProspects").ToString,
                        .TotalCustomers = row.Item("TotalCustomers").ToString,
                        .TotalCASA = row.Item("TotalCASA").ToString,
                        .TotalLoans = row.Item("TotalLoans").ToString,
                        .TotalADB = If(row.Item("TotalADB") IsNot DBNull.Value, Decimal.Parse(row.Item("TotalADB")).ToString("N"), "0.00"),
                        .TotalInitialDeposit = If(row.Item("TotalInitialDeposit") IsNot DBNull.Value, Decimal.Parse(row.Item("TotalInitialDeposit")).ToString("N"), "0.00"),
                        .TotalLosts = If(row.Item("TotalLosts") IsNot DBNull.Value, row.Item("TotalLosts").ToString, "0"),
                        .TotalLedgerBalance = If(row.Item("TotalLedgerBalance") IsNot DBNull.Value, Decimal.Parse(row.Item("TotalLedgerBalance")).ToString("N"), "0.00"),
                        .LoanAmountReported = If(row.Item("LoanAmountReported") IsNot DBNull.Value, Decimal.Parse(row.Item("LoanAmountReported")).ToString("N"), "0.00"),
                        .LoanReleaseAmount = If(row.Item("LoanReleaseAmount") IsNot DBNull.Value, Decimal.Parse(row.Item("LoanReleaseAmount")).ToString("N"), "0.00")
                        })
                Next
            End If
            Return lst
        End If

        Return Nothing

    End Function

    Shared Function GetAnnualListTotal(ByVal param As SummaryPerGroupDO) As SummaryPerGroupDO
        Dim dal As New SummaryPerGroupDAL
        Dim dt As DataTable = dal.GetAnnualTotalList(param.GroupCode, param.Year, param.ClientType, param.IndustryType, param.ADBRangeName, param.RoleID)
        If Not IsNothing(dt) Then
            Dim obj As New SummaryPerGroupDO
            If dt.Rows.Count > 0 Then
                obj.TotalLeads = dt.Rows(0)("TotalLeads").ToString
                obj.TotalSuspects = dt.Rows(0)("TotalSuspects").ToString
                obj.TotalProspects = dt.Rows(0)("TotalProspects").ToString
                obj.TotalCustomers = dt.Rows(0)("TotalCustomers").ToString
                obj.TotalCASA = dt.Rows(0)("TotalCASA").ToString
                obj.TotalLoans = dt.Rows(0)("TotalLoans").ToString
                obj.TotalADB = Decimal.Parse(dt.Rows(0)("TotalADB")).ToString("N")
                obj.TotalInitialDeposit = Decimal.Parse(dt.Rows(0)("TotalInitialDeposit")).ToString("N")
                obj.TotalLosts = dt.Rows(0)("TotalLosts").ToString
                obj.TotalLedgerBalance = Decimal.Parse(dt.Rows(0)("TotalLedgerBalance")).ToString("N")
                obj.TotalLoanAmountReported = Decimal.Parse(dt.Rows(0)("LoanAmountReported")).ToString("N")
                obj.TotalLoanReleaseAmount = Decimal.Parse(dt.Rows(0)("LoanReleaseAmount")).ToString("N")
            End If
            Return obj
        End If

        Return Nothing

    End Function


    Shared Function GetCASATypesList(ByVal obj As SummaryPerGroupDO) As List(Of SummaryPerGroupDO)
        Dim dal As New SummaryPerGroupDAL
        Dim dt As DataTable = dal.GetCASATypesList(obj.BranchCode, obj.Year, obj.ClientType, obj.IndustryType, obj.ADBRangeName, obj.RoleID)
        If Not IsNothing(dt) Then
            Dim lst As New List(Of SummaryPerGroupDO)
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    lst.Add(
                        New SummaryPerGroupDO With {
                        .SalesProspect = row.Item("SalesProspect").ToString,
                        .ClientType = row.Item("ClientType").ToString,
                        .DateEncoded = row.Item("DateEncoded").ToString,
                        .CustomerDate = row.Item("CustomerDate").ToString,
                        .AccountNumber = row.Item("AccountNumber").ToString,
                        .ADB = row.Item("ADB").ToString,
                        .CASAProductsAvailed = row.Item("CASAProductsAvailed").ToString
                        })
                Next
            End If
            Return lst
        End If

        Return Nothing

    End Function

    Shared Function GetLoanProductList(ByVal obj As SummaryPerGroupDO) As List(Of SummaryPerGroupDO)
        Dim dal As New SummaryPerGroupDAL
        Dim dt As DataTable = dal.GetLoansList(obj.BranchCode, obj.Year, obj.ClientType, obj.IndustryType, obj.RoleID)
        If Not IsNothing(dt) Then
            Dim lst As New List(Of SummaryPerGroupDO)
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    lst.Add(
                        New SummaryPerGroupDO With {
                        .SalesProspect = row.Item("SalesProspect").ToString,
                        .ClientType = row.Item("ClientType").ToString,
                        .DateEncoded = row.Item("DateEncoded").ToString,
                        .CustomerDate = row.Item("CustomerDate").ToString,
                        .LoansProductAvailed = row.Item("LoanProductsAvailed").ToString,
                        .DateReleased = row.Item("DateReleased").ToString,
                        .LoanReleasedAmount = row.Item("LoanReleasedAmount").ToString
                    })
                Next
            End If
            Return lst
        End If

        Return Nothing

    End Function
    Public Function GenerateReport(ByVal obj As SummaryPerGroupDO) As DataTable
        Dim dt As New DataTable

        If obj.moduleName = "Weekly" Then
            dt = ConvertListToDataTable(GetWeeklyList(obj))
        ElseIf obj.moduleName = "Monthly" Then
            dt = ConvertListToDataTable(GetMonthlyList(obj))
        ElseIf obj.moduleName = "Annual" Then
            dt = ConvertListToDataTable(GetAnnualList(obj))
        End If
        Return dt
    End Function

    Public Function GenerateAnnualReport(ByVal obj As SummaryPerGroupDO,
                                   ByVal table As String) As DataTable
        Dim dal As New SummaryPerGroupDAL
        Dim dt As New DataTable
        If table = "Loans" Then
            dt = dal.GetLoansList(obj.BranchCode, obj.Year, obj.ClientType, obj.IndustryType, obj.RoleID)
        ElseIf table = "CASA" Then
            dt = dal.GetCASATypesList(obj.BranchCode, obj.Year, obj.ClientType, obj.IndustryType, obj.ADBRangeName, obj.RoleID)
        Else
            dt = Nothing
        End If
        Return dt
    End Function

#End Region
#End Region
End Class
