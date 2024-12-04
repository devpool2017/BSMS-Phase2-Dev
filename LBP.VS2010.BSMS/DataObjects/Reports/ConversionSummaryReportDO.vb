Imports LBP.VS2010.BSMS.CustomValidators
Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.DataAccess
Imports LBP.VS2010.BSMS.Utilities
Imports System.Configuration.ConfigurationManager
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages

Public Class ConversionSummaryReportDO
    Inherits MasterDO

#Region "DECLARATION"
    Property moduleName As String

    <DisplayLabel("Group")>
    <RequiredField(RequiredField.ControlType.SELECTION)>
    Property GroupCode As String
    Property BranchManagerName As String

    <DisplayLabel("Branch Head")>
<RequiredField(RequiredField.ControlType.SELECTION)>
    Property Username As String

    <DisplayLabel("Branch")>
<RequiredField(RequiredField.ControlType.SELECTION)>
    Property BranchCode As String
    Property BranchName As String

    <DisplayLabel("Year")>
<RequiredConditional.ValueEquals("moduleName", {"Weekly", "Monthly", "Annually"}, RequiredField.ControlType.SELECTION)>
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

    Property Position As String


    Property ClientID As String
    Property Fullname As String
    Property ClientType As String
    Property Address As String
    Property ContactNo As String
    Property Lead As String
    Property Suspect As String
    Property Prospect As String
    Property Customer As String
    Property CASATypes As String
    Property Amount As String
    Property AmountOthers As String
    Property ADB As String
    Property Lost As String
    Property Reason As String
    Property OtherATypes As String
    Property DateEncoded As String
    Property UploadedBy As String
    Property Visits As String
    Property AccountNumbers As String
    Property LeadSource As String
    Property IndustryType As String
    Property Remarks As String
    Property ProductsOffered As String
    Property LoansProductsAvailed As String
    Property LoanAmountReported As String

    Property TotalLead As String
    Property TotalSuspect As String
    Property TotalProspect As String
    Property TotalCustomer As String
    Property TotalCASATypes As String
    Property TotalAmount As String
    Property TotalAmountOthers As String
    Property TotalADB As String
    Property TotalLost As String
    Property TotalLoansProductsAvailed As String
    Property TotalLoanAmountReported As String
    Property TotalVisits As String

    Property WeeklyTargetLeads As String
    Property TargetLeads As String
    Property TotalLeadsGeneratedVersusTarget As String
    Property LeadsToSuspect As String
    Property SuspectToProspect As String
    Property ProspectToCustomer As String
    Property GeneralClosingRatio As String
    Property CasaClosingRatio As String
    Property LostSalesRatio As String
    Property maxweeknum As String


    Property NewCASA As String
    Property NewLoans As String
    Property TotalNewCASA As String
    Property TotalNewLoans As String

    Property ActualLeadGenerated As String
    Property TargetNewAccountClosed As String
    Property ActualNewAccountClosed As String
    Property ActualVersusTargetNewAccountClosed As String
    Property TargetADB As String
    Property ActualADBGenerated As String
    Property TotalADBGeneratedVersusTarget As String

    Property ReportPage As String = "ReportViewer.aspx"
#End Region

#Region "FUNCTION"
#Region "WEEKLY SUMMARY REPORT FUNCTION"
    Shared Function GetRegionList() As DataTable
        Dim dal As New ConversionSummaryDAL

        Return dal.GetRegionList()
    End Function

    Shared Function GetUsersList(ByVal branchCode As String, ByVal roleID As String) As DataTable
        Dim dal As New ConversionSummaryDAL

        Return dal.GetUsersList(branchCode, roleID)
    End Function

    Shared Function GetBranchesList(ByVal groupCode As String) As DataTable
        Dim dal As New ConversionSummaryDAL

        Return dal.GetBranchesList(groupCode)
    End Function

    Shared Function GetNewLeadsList(ByVal UploadedBy As String,
                                    ByVal DateFrom As String,
                                    ByVal DateToday As String,
                                    ByVal branchCode As String) As List(Of ConversionSummaryReportDO)
        Dim dal As New ConversionSummaryDAL
        Dim dt As DataTable = dal.GetNewLeadsList(UploadedBy, DateFrom, DateToday, branchCode)
        If Not IsNothing(dt) Then
            Dim lst As New List(Of ConversionSummaryReportDO)
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    lst.Add(
                        New ConversionSummaryReportDO With {
                        .ClientID = row.Item("ClientID").ToString,
                        .Fullname = row.Item("Fullname").ToString,
                        .ClientType = row.Item("ClientType").ToString,
                        .Address = row.Item("Address").ToString,
                        .ContactNo = row.Item("ContactNo").ToString,
                        .Lead = row.Item("Lead").ToString,
                        .Suspect = row.Item("Suspect").ToString,
                        .Prospect = row.Item("Prospect").ToString,
                        .Customer = row.Item("Customer").ToString,
                        .CASATypes = GetDescription("CASAType", row.Item("CASATypes").ToString),
                        .Amount = IIf(row.Item("Amount").ToString = "", "0.00", row.Item("Amount").ToString),
                        .AmountOthers = IIf(row.Item("AmountOthers").ToString = "", "0.00", row.Item("AmountOthers").ToString),
                        .ADB = IIf(row.Item("ADB").ToString = "", "0.00", row.Item("ADB").ToString),
                        .Lost = row.Item("Lost").ToString,
                        .Reason = row.Item("Reason").ToString,
                        .OtherATypes = GetDescription("OtherATypes", row.Item("OtherATypes").ToString),
                        .DateEncoded = row.Item("DateEncoded").ToString,
                        .UploadedBy = row.Item("UploadedBy").ToString,
                        .Visits = row.Item("Visits").ToString,
                        .AccountNumbers = row.Item("AccountNumbers").ToString,
                        .LeadSource = row.Item("LeadSource").ToString,
                        .IndustryType = row.Item("IndustryType").ToString,
                        .Remarks = row.Item("Remarks").ToString,
                        .ProductsOffered = GetDescription("ProductsOffered", row.Item("ProductsOffered").ToString),
                        .LoansProductsAvailed = row.Item("LoansProductsAvailed").ToString,
                        .LoanAmountReported = IIf(row.Item("LoanAmountReported").ToString = "", "0.00", row.Item("LoanAmountReported").ToString)
                        })
                Next
            End If
            Return lst
        End If

        Return Nothing

    End Function

    Shared Function GetDescription(ByVal table As String, ByVal value As String) As String
        Dim dal As New ConversionSummaryDAL
        Dim dt As New DataTable
        Dim description As String = String.Empty
        If table = "CASAType" Then
            dt = dal.CASAProductList()
        End If
        If table = "ProductsOffered" Then
            dt = dal.OtherProductList()
        End If
        If table = "OtherATypes" Then
            dt = dal.AllProductList()
        End If
        Dim codes() As String = value.Split(","c)
        For Each code As String In codes
            Dim trimmedCode As String = code.Trim()
            Dim matchingRows() As DataRow = dt.AsEnumerable().Where(Function(row) row.Field(Of String)("Code") = trimmedCode).ToArray()
            If matchingRows.Length > 0 Then
                If Not (description = String.Empty) Then
                    description += " , "
                End If
                description += matchingRows(0)("Description").ToString()
            End If
        Next

        Return description
    End Function

    Shared Function GetTotalNewLeads(ByVal UploadedBy As String,
                                ByVal DateFrom As String,
                                ByVal DateToday As String,
                                ByVal branchCode As String,
                                 ByVal Year As String,
                                ByVal Month As String) As ConversionSummaryReportDO
        Dim dal As New ConversionSummaryDAL
        Dim dalA As New DateMaintenanceDAL
        Dim dt As DataTable = dal.GetNewLeadsList(UploadedBy, DateFrom, DateToday, branchCode, False)
        If Not IsNothing(dt) Then
            Dim obj As New ConversionSummaryReportDO
            If dt.Rows.Count > 0 Then
                obj.TotalLead = dt.Compute("COUNT(Lead)", String.Empty)
                obj.TotalSuspect = Convert.ToString(dt.Compute("COUNT(Suspect)", String.Empty))
                obj.TotalProspect = Convert.ToString(dt.Compute("COUNT(Prospect)", String.Empty))
                obj.TotalCustomer = Convert.ToString(dt.Compute("COUNT(Customer)", String.Empty))
                obj.TotalCASATypes = Convert.ToString(dt.Compute("COUNT(CASATypes)", "CASATypes <> ''"))
                obj.TotalLost = Convert.ToString(dt.Compute("COUNT(Lost)", String.Empty))
                obj.TotalLoansProductsAvailed = Convert.ToString(dt.Compute("COUNT(LoansProductsAvailed)", String.Empty))
                obj.TotalADB = (dt.AsEnumerable().Sum(Function(row) If(Decimal.TryParse(row.Field(Of String)("ADB"), 0), Decimal.Parse(row.Field(Of String)("ADB")), 0))).ToString("N")
                obj.TotalAmount = (dt.AsEnumerable().Sum(Function(row) If(Decimal.TryParse(row.Field(Of String)("Amount"), 0), Decimal.Parse(row.Field(Of String)("Amount")), 0))).ToString("N")
                obj.TotalAmountOthers = (dt.AsEnumerable().Sum(Function(row) If(Decimal.TryParse(row.Field(Of String)("AmountOthers"), 0), Decimal.Parse(row.Field(Of String)("AmountOthers")), 0))).ToString("N")
                obj.TotalLoanAmountReported = (dt.AsEnumerable().Sum(Function(row) If(Decimal.TryParse(row.Field(Of String)("LoanAmountReported"), 0), Decimal.Parse(row.Field(Of String)("LoanAmountReported")), 0))).ToString("N")
                obj.maxweeknum = dalA.GetMaxWeekNum(Year, Month)

                obj.TargetLeads = dal.GetTargetLeads(UploadedBy, False)
                obj.WeeklyTargetLeads = (If(obj.TargetLeads = "", 0, CInt(obj.TargetLeads))) / (If(obj.maxweeknum = "", 0, CInt(obj.maxweeknum)))

                obj.TotalLeadsGeneratedVersusTarget = FormatNumber(((obj.TotalLead / obj.WeeklyTargetLeads) * 100), 2) + " %"
                obj.LeadsToSuspect = FormatNumber(((obj.TotalSuspect / obj.TotalLead) * 100), 2) + " %"
                obj.SuspectToProspect = FormatNumber(((obj.TotalProspect / obj.TotalSuspect) * 100), 2) + " %"
                obj.ProspectToCustomer = FormatNumber(((obj.TotalCustomer / obj.TotalProspect) * 100), 2) + " %"
                obj.GeneralClosingRatio = FormatNumber(((obj.TotalCustomer / obj.TotalLead) * 100), 2) + " %"
                obj.CasaClosingRatio = FormatNumber(((obj.TotalCASATypes / obj.TotalLead) * 100), 2) + " %"
                obj.LostSalesRatio = FormatNumber(((obj.Lost / obj.Lead) * 100), 2) + " %"

                'Set default values when computed values are equal to "NaN" or "Infinity OR when the length of computed values are long"
                If obj.TotalLeadsGeneratedVersusTarget = "NaN %" Or obj.LeadsToSuspect = "Infinity %" Or obj.TotalLeadsGeneratedVersusTarget = "∞ %" Then
                    obj.TotalLeadsGeneratedVersusTarget = "-"
                End If

                If obj.LeadsToSuspect = "NaN %" Or obj.LeadsToSuspect = "Infinity %" Then
                    obj.LeadsToSuspect = "-"
                End If

                If obj.SuspectToProspect = "NaN %" Or obj.SuspectToProspect = "Infinity %" Then
                    obj.SuspectToProspect = "-"
                End If

                If obj.ProspectToCustomer = "NaN %" Or obj.ProspectToCustomer = "Infinity %" Then
                    obj.ProspectToCustomer = "-"
                End If

                If obj.GeneralClosingRatio = "NaN %" Or obj.GeneralClosingRatio = "Infinity %" Then
                    obj.GeneralClosingRatio = "-"
                End If

                If obj.CasaClosingRatio = "NaN %" Or obj.CasaClosingRatio = "Infinity %" Then
                    obj.CasaClosingRatio = "-"
                End If

                If obj.LostSalesRatio = "NaN %" Or obj.LostSalesRatio = "Infinity %" Then
                    obj.LostSalesRatio = "-"
                End If

            End If
            Return obj
        End If

        Return Nothing

    End Function

    Shared Function GetPotentialAccountsList(ByVal UploadedBy As String,
                                    ByVal DateFrom As String,
                                    ByVal DateToday As String,
                                    ByVal branchCode As String) As List(Of ConversionSummaryReportDO)
        Dim dal As New ConversionSummaryDAL
        Dim dt As DataTable = dal.GetPotentialAccountsList(UploadedBy, DateFrom, DateToday, branchCode)
        If Not IsNothing(dt) Then
            Dim lst As New List(Of ConversionSummaryReportDO)
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    lst.Add(
                        New ConversionSummaryReportDO With {
                        .ClientID = row.Item("ClientID").ToString,
                        .Fullname = row.Item("Fullname").ToString,
                        .ClientType = row.Item("ClientType").ToString,
                        .Address = row.Item("Address").ToString,
                        .ContactNo = row.Item("ContactNo").ToString,
                        .Lead = row.Item("Lead").ToString,
                        .Suspect = row.Item("Suspect").ToString,
                        .Prospect = row.Item("Prospect").ToString,
                        .Customer = row.Item("Customer").ToString,
                        .CASATypes = GetDescription("CASAType", row.Item("CASATypes").ToString),
                        .Amount = IIf(row.Item("Amount").ToString = "", "0.00", row.Item("Amount").ToString),
                        .AmountOthers = IIf(row.Item("AmountOthers").ToString = "", "0.00", row.Item("AmountOthers").ToString),
                        .ADB = IIf(row.Item("ADB").ToString = "", "0.00", row.Item("ADB").ToString),
                        .Lost = row.Item("Lost").ToString,
                        .Reason = row.Item("Reason").ToString,
                        .OtherATypes = GetDescription("OtherATypes", row.Item("OtherATypes").ToString),
                        .DateEncoded = row.Item("DateEncoded").ToString,
                        .UploadedBy = row.Item("UploadedBy").ToString,
                        .Visits = row.Item("Visits").ToString,
                        .AccountNumbers = row.Item("AccountNumbers").ToString,
                        .LeadSource = row.Item("LeadSource").ToString,
                        .IndustryType = row.Item("IndustryType").ToString,
                        .Remarks = row.Item("Remarks").ToString,
                        .ProductsOffered = GetDescription("ProductsOffered", row.Item("ProductsOffered").ToString),
                        .LoansProductsAvailed = row.Item("LoansProductsAvailed").ToString,
                        .LoanAmountReported = IIf(row.Item("LoanAmountReported").ToString = "", "0.00", row.Item("LoanAmountReported").ToString)
                        })
                Next
            End If
            Return lst
        End If

        Return Nothing

    End Function

    Shared Function GetTotalPotentialAccountsRevisit(ByVal UploadedBy As String,
                            ByVal DateFrom As String,
                            ByVal DateToday As String,
                            ByVal branchCode As String) As ConversionSummaryReportDO
        Dim dal As New ConversionSummaryDAL
        Dim dt As DataTable = dal.GetPotentialAccountsList(UploadedBy, DateFrom, DateToday, branchCode)
        If Not IsNothing(dt) Then
            Dim obj As New ConversionSummaryReportDO
            If dt.Rows.Count > 0 Then
                obj.TotalLead = dt.Compute("COUNT(Lead)", String.Empty)
                obj.TotalSuspect = Convert.ToString(dt.Compute("COUNT(Suspect)", String.Empty))
                obj.TotalProspect = Convert.ToString(dt.Compute("COUNT(Prospect)", String.Empty))
                obj.TotalCustomer = Convert.ToString(dt.Compute("COUNT(Customer)", String.Empty))
                obj.TotalCASATypes = Convert.ToString(dt.Compute("COUNT(CASATypes)", String.Empty))
                obj.TotalLost = Convert.ToString(dt.Compute("COUNT(Lost)", String.Empty))
                obj.TotalLoansProductsAvailed = Convert.ToString(dt.Compute("COUNT(LoansProductsAvailed)", String.Empty))
                obj.TotalADB = (dt.AsEnumerable().Sum(Function(row) If(Decimal.TryParse(row.Field(Of String)("ADB"), 0), Decimal.Parse(row.Field(Of String)("ADB")), 0))).ToString("N")
                obj.TotalAmount = (dt.AsEnumerable().Sum(Function(row) If(Decimal.TryParse(row.Field(Of String)("Amount"), 0), Decimal.Parse(row.Field(Of String)("Amount")), 0))).ToString("N")
                obj.TotalAmountOthers = (dt.AsEnumerable().Sum(Function(row) If(Decimal.TryParse(row.Field(Of String)("AmountOthers"), 0), Decimal.Parse(row.Field(Of String)("AmountOthers")), 0))).ToString("N")
                obj.TotalLoanAmountReported = (dt.AsEnumerable().Sum(Function(row) If(Decimal.TryParse(row.Field(Of String)("LoanAmountReported"), 0), Decimal.Parse(row.Field(Of String)("LoanAmountReported")), 0))).ToString("N")
                obj.TotalVisits = Convert.ToString(dt.Compute("COUNT(Visits)", String.Empty))
            End If
            Return obj
        End If

        Return Nothing

    End Function

    Public Function ChkWeeklyRemarksExists(ByVal TableName As String,
                                           ByVal Year As String,
                                           ByVal MonthName As String,
                                           ByVal WeekNum As String,
                                           ByVal Username As String) As Boolean
        Dim dal As New ConversionSummaryDAL
        Return dal.ChkWeeklyRemarksExists(TableName, Year, MonthName, WeekNum, Username)
    End Function

    Public Function GetWeeklyRemarks(ByVal TableName As String,
                                       ByVal Year As String,
                                       ByVal MonthName As String,
                                       ByVal WeekNum As String,
                                       ByVal Username As String) As String
        Dim dal As New ConversionSummaryDAL
        Return dal.GetWeeklyRemarks(TableName, Year, MonthName, WeekNum, Username)
    End Function

    Public Function AddWeeklyRemarks(ByVal TableName As String,
                                       ByVal Year As String,
                                       ByVal MonthName As String,
                                       ByVal WeekNum As String,
                                       ByVal Username As String,
                                       ByVal Comment As String) As Boolean
        Dim dal As New ConversionSummaryDAL
        Return dal.AddWeeklyRemarks(TableName, Year, MonthName, WeekNum, Username, Comment)
    End Function

    Public Function UpdateWeeklyRemarks(ByVal TableName As String,
                                     ByVal Year As String,
                                     ByVal MonthName As String,
                                     ByVal WeekNum As String,
                                     ByVal Username As String,
                                     ByVal Comment As String) As ConversionSummaryReportDO

        Dim dal As New ConversionSummaryDAL
        Dim obj As New ConversionSummaryReportDO
        If ValidateRemarksLength(Comment).isSuccess Then
            obj.isSuccess = dal.UpdateWeeklyRemarks(TableName, Year, MonthName, WeekNum, Username, Comment)
        Else
            Return ValidateRemarksLength(Comment)
        End If
        Return obj
    End Function

    Public Function GenerateReport(ByVal UploadedBy As String,
                                    ByVal DateFrom As String,
                                    ByVal DateToday As String,
                                    ByVal branchCode As String,
                                    ByVal table As String) As DataTable
        Dim dal As New ConversionSummaryDAL
        Dim dt As New DataTable
        If table = "NewLeads" Then
            dt = ConvertListToDataTable(GetNewLeadsList(UploadedBy, DateFrom, DateToday, branchCode))
        ElseIf table = "PARevisit" Then
            dt = ConvertListToDataTable(GetPotentialAccountsList(UploadedBy, DateFrom, DateToday, branchCode))
        End If
        Return dt
    End Function
#End Region

#Region "MONTHLY SUMMARY REPORT FUNCTION"

    Shared Function SearchMonthlyReport(ByVal UploadedBy As String,
                               ByVal branchCode As String,
                                ByVal Year As String,
                               ByVal Month As String,
                               ByVal MonthCode As String) As List(Of ConversionSummaryReportDO)

        Dim dt As DataTable = GetMonthlyReport(UploadedBy, branchCode, Year, Month, MonthCode)
        If Not IsNothing(dt) Then
            Dim lst As New List(Of ConversionSummaryReportDO)
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    lst.Add(
                        New ConversionSummaryReportDO With {
                        .WeekNumber = row.Item("WeekNumber").ToString,
                        .Lead = row.Item("Leads").ToString,
                        .Suspect = row.Item("Suspects").ToString,
                        .Prospect = row.Item("Prospects").ToString,
                        .NewCASA = row.Item("NewCASA").ToString,
                        .NewLoans = row.Item("NewLoans").ToString,
                        .Lost = row.Item("Losts").ToString,
                        .Customer = row.Item("Customers").ToString
                        })
                Next
            End If
            Return lst
        End If

        Return Nothing
    End Function

    Shared Function GetMonthlyReport(ByVal UploadedBy As String,
                           ByVal branchCode As String,
                            ByVal Year As String,
                           ByVal Month As String,
                           ByVal MonthCode As String) As DataTable
        Dim dalDates As New DateMaintenanceDAL
        Dim dal As New ConversionSummaryDAL
        Dim maxweek As String = dalDates.GetMaxWeekNum(Year, Month)
        Dim minweek As String = dalDates.GetMinWeekNum(Year, Month)
        If minweek = Nothing Or minweek <> "1" Then
            minweek = "1"
        End If

        If maxweek = Nothing Or maxweek = minweek Then
            maxweek = "4"
        End If

        Dim dt As New DataTable
        For week As Integer = CInt(minweek) To CInt(maxweek)
            Dim obj As DateMaintenanceDO = DateMaintenanceDO.GetDate(Year, Month, week)
            Dim commit As Boolean = False
            If week = CInt(maxweek) Then
                commit = True
            End If
            Dim dtPerWeekDetails As DataTable = dal.TotalsForWeeklySummary(UploadedBy, obj.FromDate, obj.ToDate, branchCode, commit)
            dtPerWeekDetails.Columns.Add("WeekNumber", GetType(String))
            dtPerWeekDetails.Columns.Add("MonthName", GetType(String))
            dtPerWeekDetails.Columns.Add("MonthCode", GetType(String))
            Dim newRow As DataRow = dtPerWeekDetails.NewRow()
            For Each row As DataRow In dtPerWeekDetails.Rows
                row("WeekNumber") = "Week " & week
                row("MonthCode") = MonthCode
                If week = CInt(minweek) Then
                    row("MonthName") = Month
                End If
            Next
            dt.Merge(dtPerWeekDetails, False, MissingSchemaAction.Add)
        Next

        Return dt

    End Function

    Shared Function GetTotalMonthlyReport(ByVal BranchManager As String,
                           ByVal branchCode As String,
                           ByVal Year As String,
                           ByVal MonthName As String,
                           ByVal MonthCode As String) As ConversionSummaryReportDO
        Dim dal As New ConversionSummaryDAL
        Dim dt As DataTable = ConversionSummaryReportDO.GetMonthlyReport(BranchManager, branchCode, Year, MonthName, MonthCode)
        If Not IsNothing(dt) Then
            Dim obj As New ConversionSummaryReportDO
            If dt.Rows.Count > 0 Then
                obj.TotalLead = Convert.ToString(dt.Compute("SUM(Leads)", String.Empty))
                obj.TotalSuspect = Convert.ToString(dt.Compute("SUM(Suspects)", String.Empty))
                obj.TotalProspect = Convert.ToString(dt.Compute("SUM(Prospects)", String.Empty))
                obj.TotalNewCASA = Convert.ToString(dt.Compute("SUM(NewCASA)", String.Empty))
                obj.TotalNewLoans = Convert.ToString(dt.Compute("SUM(NewLoans)", String.Empty))
                obj.TotalLost = Convert.ToString(dt.Compute("SUM(Losts)", String.Empty))
                obj.TotalCustomer = Convert.ToString(dt.Compute("SUM(Customers)", String.Empty))
            End If

            Dim actualADBGenerated As String = If(dal.GetMonthlyActualADBGeneratedPerBM(BranchManager, Year, MonthCode, False) = "", 0, CInt(dal.GetMonthlyActualADBGeneratedPerBM(BranchManager, Year, MonthCode, False)))
            obj.TargetLeads = If(dal.GetTargetLeads(BranchManager, False) = "", 0, CInt(dal.GetTargetLeads(BranchManager, False)))
            obj.ActualLeadGenerated = obj.TotalLead
            obj.TotalLeadsGeneratedVersusTarget = FormatNumber(((CInt(obj.ActualLeadGenerated) / obj.TargetLeads) * 100), 2) + " %"
            obj.LeadsToSuspect = FormatNumber((CInt(obj.TotalSuspect) / CInt(obj.TotalLead)) * 100, 2) + " %"
            obj.SuspectToProspect = FormatNumber((CInt(obj.TotalProspect) / CInt(obj.TotalSuspect)) * 100, 2) + " %"
            obj.ProspectToCustomer = FormatNumber((CInt(obj.TotalCustomer) / CInt(obj.TotalProspect)) * 100, 2) + " %"
            obj.TargetNewAccountClosed = If(dal.GetTargetNewAccountsClosed(False) = "", 0, CInt(dal.GetTargetNewAccountsClosed(False)))
            obj.ActualNewAccountClosed = obj.TotalNewLoans
            obj.ActualVersusTargetNewAccountClosed = FormatNumber((CInt(obj.ActualNewAccountClosed) / CInt(obj.TargetNewAccountClosed)) * 100, 2).ToString + " %"
            obj.GeneralClosingRatio = FormatNumber((CInt(obj.TotalNewLoans) / CInt(obj.TotalLead)) * 100, 2) + " %"
            obj.CasaClosingRatio = FormatNumber((CInt(obj.TotalNewCASA) / CInt(obj.TotalLead)) * 100, 2) + " %"
            obj.LostSalesRatio = FormatNumber((CInt(obj.TotalLost) / CInt(obj.TotalLead)) * 100, 2) + " %"
            obj.TargetADB = dal.GetTargetADB()
            obj.TargetADB = FormatNumber(If(obj.TargetADB = "", 0, obj.TargetADB), 2)
            obj.ActualADBGenerated = FormatNumber(actualADBGenerated, 2)
            obj.TotalADBGeneratedVersusTarget = FormatNumber((CInt(obj.ActualADBGenerated) / CInt(obj.TargetADB)) * 100, 2) + " %"

            If obj.TotalLeadsGeneratedVersusTarget = "NaN %" Or obj.TotalLeadsGeneratedVersusTarget = "∞ %" Then
                obj.TotalLeadsGeneratedVersusTarget = "-"
            End If
            If obj.LeadsToSuspect = "NaN %" Or obj.LeadsToSuspect = "Infinity %" Or obj.LeadsToSuspect = "∞ %" Then
                obj.LeadsToSuspect = "-"
            End If

            If obj.SuspectToProspect = "NaN %" Or obj.SuspectToProspect = "Infinity %" Or obj.SuspectToProspect = "∞ %" Then
                obj.SuspectToProspect = "-"
            End If

            If obj.ProspectToCustomer = "NaN %" Or obj.ProspectToCustomer = "Infinity %" Or obj.ProspectToCustomer = "∞ %" Then
                obj.ProspectToCustomer = "-"
            End If

            If obj.ActualVersusTargetNewAccountClosed = "NaN %" Or obj.ActualVersusTargetNewAccountClosed = "Infinity %" Or obj.ActualVersusTargetNewAccountClosed = "∞ %" Then
                obj.ActualVersusTargetNewAccountClosed = "-"
            End If

            If obj.GeneralClosingRatio = "NaN %" Or obj.GeneralClosingRatio = "Infinity %" Or obj.GeneralClosingRatio = "∞ %" Then
                obj.GeneralClosingRatio = "-"
            End If

            If obj.CasaClosingRatio = "NaN %" Or obj.CasaClosingRatio = "Infinity %" Or obj.CasaClosingRatio = "∞ %" Then
                obj.CasaClosingRatio = "-"
            End If

            If obj.LostSalesRatio = "NaN %" Or obj.LostSalesRatio = "Infinity %" Or obj.LostSalesRatio = "∞ %" Then
                obj.LostSalesRatio = "-"
            End If

            If obj.TotalADBGeneratedVersusTarget = "NaN %" Or obj.TotalADBGeneratedVersusTarget = "Infinity %" Or obj.TotalADBGeneratedVersusTarget = "∞ %" Then
                obj.TotalADBGeneratedVersusTarget = "-"
            End If

            Return obj
        End If

        Return Nothing

    End Function

    Public Function ChkMonthlyRemarksExists(ByVal TableName As String,
                                          ByVal Year As String,
                                          ByVal MonthName As String,
                                          ByVal Username As String) As Boolean
        Dim dal As New ConversionSummaryDAL
        Return dal.ChkMonthlyRemarksExists(TableName, Year, MonthName, Username)
    End Function

    Public Function GetMonthlyRemarks(ByVal TableName As String,
                                       ByVal Year As String,
                                       ByVal MonthName As String,
                                       ByVal Username As String) As String
        Dim dal As New ConversionSummaryDAL
        Return dal.GetMonthlyRemarks(TableName, Year, MonthName, Username)
    End Function

    Public Function AddMonthlyRemarks(ByVal TableName As String,
                                       ByVal Year As String,
                                       ByVal MonthName As String,
                                       ByVal Username As String,
                                       ByVal Comment As String) As Boolean
        Dim dal As New ConversionSummaryDAL
        Return dal.AddMonthlyRemarks(TableName, Year, MonthName, Username, Comment)
    End Function

    Public Function UpdateMonthlyRemarks(ByVal TableName As String,
                                     ByVal Year As String,
                                     ByVal MonthName As String,
                                     ByVal Username As String,
                                     ByVal Comment As String) As ConversionSummaryReportDO
        Dim dal As New ConversionSummaryDAL
        Dim obj As New ConversionSummaryReportDO
        If ValidateRemarksLength(Comment).isSuccess Then
            obj.isSuccess = dal.UpdateMonthlyRemarks(TableName, Year, MonthName, Username, Comment)
        Else
            Return ValidateRemarksLength(Comment)
        End If
        Return obj
    End Function

    Public Function GenerateMonthlyReport(ByVal UploadedBy As String,
                        ByVal branchCode As String,
                         ByVal Year As String,
                        ByVal Month As String,
                        ByVal MonthCode As String) As DataTable


        Return GetMonthlyReport(UploadedBy, branchCode, Year, Month, MonthCode)

    End Function

    Public Function GenerateTotalMonthlyReport(ByVal BranchManager As String,
                           ByVal branchCode As String,
                           ByVal Year As String,
                           ByVal MonthName As String,
                           ByVal MonthCode As String) As DataTable


        Dim obj As ConversionSummaryReportDO = GetTotalMonthlyReport(BranchManager,
                                                                     branchCode,
                                                                     Year,
                                                                     MonthName,
                                                                     MonthCode)
        Dim objList As New List(Of ConversionSummaryReportDO)
        objList.Add(obj)
        Dim dt As DataTable = ConvertListToDataTable(objList)

        Return dt
    End Function

#End Region

#Region "ANNUALLY SUMMARY REPORT FUNCTION"

    Shared Function SearchAnnuallyReport(ByVal UploadedBy As String,
                               ByVal branchCode As String,
                                ByVal Year As String) As List(Of ConversionSummaryReportDO)

        Dim dt As DataTable = GetAnnuallyReport(UploadedBy, branchCode, Year)
        If Not IsNothing(dt) Then
            Dim lst As New List(Of ConversionSummaryReportDO)
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    lst.Add(
                        New ConversionSummaryReportDO With {
                        .MonthCode = row.Item("MonthCode").ToString,
                        .Month = row.Item("MonthName").ToString,
                        .WeekNumber = row.Item("WeekNumber").ToString,
                        .Lead = row.Item("Leads").ToString,
                        .Suspect = row.Item("Suspects").ToString,
                        .Prospect = row.Item("Prospects").ToString,
                        .NewCASA = row.Item("NewCASA").ToString,
                        .NewLoans = row.Item("NewLoans").ToString,
                        .Lost = row.Item("Losts").ToString
                        })
                Next
            End If
            Return lst
        End If

        Return Nothing
    End Function

    Shared Function GetAnnuallyReport(ByVal UploadedBy As String,
                           ByVal branchCode As String,
                            ByVal Year As String) As DataTable
        Dim dtAnnually As New DataTable
        For i As Integer = 1 To 12
            Dim dtPerMonthDetails As DataTable = GetMonthlyReport(UploadedBy, branchCode, Year, MonthName(i), i)
            Dim row As DataRow = dtPerMonthDetails.NewRow()
            row("MonthCode") = i
            row("MonthName") = "Sub-Totals"
            row("Leads") = Convert.ToString(dtPerMonthDetails.Compute("SUM(Leads)", String.Empty))
            row("Suspects") = Convert.ToString(dtPerMonthDetails.Compute("SUM(Suspects)", String.Empty))
            row("Prospects") = Convert.ToString(dtPerMonthDetails.Compute("SUM(Prospects)", String.Empty))
            row("NewCASA") = Convert.ToString(dtPerMonthDetails.Compute("SUM(NewCASA)", String.Empty))
            row("NewLoans") = Convert.ToString(dtPerMonthDetails.Compute("SUM(NewLoans)", String.Empty))
            row("Losts") = Convert.ToString(dtPerMonthDetails.Compute("SUM(Losts)", String.Empty))

            dtPerMonthDetails.Rows.Add(row)
            dtAnnually.Merge(dtPerMonthDetails, False, MissingSchemaAction.Add)
        Next
        Return dtAnnually

    End Function

    Shared Function GetTotalAnnuallyReport(ByVal BranchManager As String,
                           ByVal branchCode As String,
                           ByVal Year As String) As ConversionSummaryReportDO
        Dim dal As New ConversionSummaryDAL
        Dim dt As DataTable = ConversionSummaryReportDO.GetAnnuallyReport(BranchManager, branchCode, Year)
        If Not IsNothing(dt) Then
            Dim obj As New ConversionSummaryReportDO
            Dim condition As String = "MonthName = 'Sub-Totals'"
            If dt.Rows.Count > 0 Then
                obj.TotalLead = Convert.ToString(dt.Compute("SUM(Leads)", condition))
                obj.TotalSuspect = Convert.ToString(dt.Compute("SUM(Suspects)", condition))
                obj.TotalProspect = Convert.ToString(dt.Compute("SUM(Prospects)", condition))
                obj.TotalNewCASA = Convert.ToString(dt.Compute("SUM(NewCASA)", condition))
                obj.TotalNewLoans = Convert.ToString(dt.Compute("SUM(NewLoans)", condition))
                obj.TotalLost = Convert.ToString(dt.Compute("SUM(Losts)", condition))
            End If
            Dim actualADBGenerated As Decimal = If(dal.GetAnnualActualADBGeneratedPerBMSO(BranchManager, Year, False) = "", "0.0000", CDec(dal.GetAnnualActualADBGeneratedPerBMSO(BranchManager, Year, False)))
            obj.TargetLeads = If(dal.GetTargetLeads(BranchManager, False) = "", 0, CInt(dal.GetTargetLeads(BranchManager, False))) * 12
            obj.ActualLeadGenerated = obj.TotalLead
            obj.TotalLeadsGeneratedVersusTarget = FormatNumber(((CInt(obj.ActualLeadGenerated) / obj.TargetLeads) * 100), 2) + " %"
            obj.LeadsToSuspect = FormatNumber((CInt(obj.TotalSuspect) / CInt(obj.TotalLead)) * 100, 2) + " %"
            obj.SuspectToProspect = FormatNumber((CInt(obj.TotalProspect) / CInt(obj.TotalSuspect)) * 100, 2) + " %"
            obj.ProspectToCustomer = FormatNumber((CInt(obj.TotalNewLoans) / CInt(obj.TotalProspect)) * 100, 2) + " %"
            obj.TargetNewAccountClosed = If(dal.GetTargetNewAccountsClosed(False) = "", 0, CInt(dal.GetTargetNewAccountsClosed(False))) * 12
            obj.ActualNewAccountClosed = obj.TotalNewLoans
            obj.ActualVersusTargetNewAccountClosed = FormatNumber((CInt(obj.ActualNewAccountClosed) / CInt(obj.TargetNewAccountClosed)) * 100, 2).ToString + " %"
            obj.GeneralClosingRatio = FormatNumber((CInt(obj.TotalNewLoans) / CInt(obj.TotalLead)) * 100, 2) + " %"
            obj.CasaClosingRatio = FormatNumber((CInt(obj.TotalNewCASA) / CInt(obj.TotalLead)) * 100, 2) + " %"
            obj.LostSalesRatio = FormatNumber((CInt(obj.TotalLost) / CInt(obj.TotalLead)) * 100, 2) + " %"
            obj.TargetADB = dal.GetTargetADB()
            obj.TargetADB = FormatNumber(If(obj.TargetADB = "", 0, CInt(obj.TargetADB)) * 12, 2)
            obj.ActualADBGenerated = FormatNumber(actualADBGenerated, 2)
            obj.TotalADBGeneratedVersusTarget = FormatNumber((CInt(obj.ActualADBGenerated) / CInt(obj.TargetADB)) * 100, 2) + " %"


            If obj.TotalLeadsGeneratedVersusTarget = "NaN %" Or obj.TotalLeadsGeneratedVersusTarget = "∞ %" Then
                obj.TotalLeadsGeneratedVersusTarget = "-"
            End If
            If obj.LeadsToSuspect = "NaN %" Or obj.LeadsToSuspect = "Infinity %" Then
                obj.LeadsToSuspect = "-"
            End If

            If obj.SuspectToProspect = "NaN %" Or obj.SuspectToProspect = "Infinity %" Then
                obj.SuspectToProspect = "-"
            End If

            If obj.ProspectToCustomer = "NaN %" Or obj.ProspectToCustomer = "Infinity %" Then
                obj.ProspectToCustomer = "-"
            End If

            If obj.ActualVersusTargetNewAccountClosed = "NaN %" Or obj.ActualVersusTargetNewAccountClosed = "Infinity %" Then
                obj.ActualVersusTargetNewAccountClosed = "-"
            End If

            If obj.GeneralClosingRatio = "NaN %" Or obj.GeneralClosingRatio = "Infinity %" Then
                obj.GeneralClosingRatio = "-"
            End If

            If obj.CasaClosingRatio = "NaN %" Or obj.CasaClosingRatio = "Infinity %" Then
                obj.CasaClosingRatio = "-"
            End If

            If obj.LostSalesRatio = "NaN %" Or obj.LostSalesRatio = "Infinity %" Then
                obj.LostSalesRatio = "-"
            End If

            If obj.TotalADBGeneratedVersusTarget = "NaN %" Or obj.TotalADBGeneratedVersusTarget = "Infinity %" Then
                obj.TotalADBGeneratedVersusTarget = "-"
            End If

            Return obj
        End If

        Return Nothing

    End Function

    Public Function GenerateTotalAnnuallyReport(ByVal BranchManager As String,
                           ByVal branchCode As String,
                           ByVal Year As String) As DataTable


        Dim obj As ConversionSummaryReportDO = GetTotalAnnuallyReport(BranchManager,
                                                                     branchCode,
                                                                     Year)
        Dim objList As New List(Of ConversionSummaryReportDO)
        objList.Add(obj)
        Dim dt As DataTable = ConvertListToDataTable(objList)

        Return dt
    End Function
#End Region

    Public Function ValidateRemarksLength(ByVal NewComment As String) As ConversionSummaryReportDO
        Dim dal As New ConversionSummaryDAL
        Dim obj As New ConversionSummaryReportDO
        If Len(NewComment) > GetSection("PageControlVariables")("LostRemarksMaxLen") Then
            obj.isSuccess = False
            obj.errMsg = Validation.getErrorMessage(Validation.ErrorType.TOO_LONG, "Comments", {GetSection("PageControlVariables")("LostRemarksMaxLen")})
        Else
            obj.isSuccess = True
        End If
        Return obj
    End Function
#End Region

End Class