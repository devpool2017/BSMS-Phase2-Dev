Imports LBP.VS2010.BSMS.DataObjects
Imports System.Web
Imports System.Configuration.ConfigurationManager
Imports Microsoft.Reporting.WebForms

Public Class ReportViewer
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            GenerateReport()

        End If
    End Sub
    Private Sub GenerateReport()
        Dim strPath As String = AppDomain.CurrentDomain.BaseDirectory
        Dim reportName As String = HttpContext.Current.Session("ReportName")
        Select Case reportName
            Case "PotentialAccount_Reports"
                Dim obj As PotentialAccountReportsDO = HttpContext.Current.Session("currentParameter")
                Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")

                Dim ds As New DataSet
                Dim dt As New DataTable

                With rptReport.LocalReport
                    rptReport.ProcessingMode = ProcessingMode.Local
                    .DataSources.Clear()
                    .ReportPath = GetSection("Commons")("ReportPath") & GetSection("Commons")("PotentialAccountReports")
                    .SetParameters(New List(Of ReportParameter) From {
                       New ReportParameter("BrCode", obj.BrCode),
                       New ReportParameter("ReportType", obj.SelectReport),
                       New ReportParameter("LogonUser", obj.LoggedName)
                   })

                    dt = obj.GeneratePotentialAccountReports(obj.BrCode,
                                             obj.SelectReport)
                    'DataSets Set
                    dt.TableName = "DataSet1"
                    ds.Tables.Add(dt)
                    .DataSources.Add(New ReportDataSource(dt.TableName, dt))
                    .DisplayName = "Potential_Account_Reports" + Date.Today
                    .Refresh()

                End With
            Case "UsersList_Report"
                Dim obj As New UsersDO

                Dim ds As New DataSet
                Dim dt As New DataTable

                Dim params As UsersDO = HttpContext.Current.Session("currentUsersParameter")

                With rptReport.LocalReport
                    .DataSources.Clear()
                    .ReportPath = GetSection("Commons")("ReportPath") & GetSection("Commons")("UsersListReport")
                    .SetParameters(New List(Of ReportParameter) From {
                        New ReportParameter("Status", params.Status),
                        New ReportParameter("Role", params.RoleName),
                        New ReportParameter("Group", params.RegionName),
                        New ReportParameter("Branch", params.Branch)
                    })

                    'dt = obj.GetUsersListReport()
                    dt = obj.GetUsersListReport(params.Username, params.StatusId, params.RoleId, params.RegionCode, params.BranchCode)
                    dt.TableName = "DataSet1" 'datasets
                    ds.Tables.Add(dt)
                    .DataSources.Add(New ReportDataSource(dt.TableName, dt))
                    .Refresh()

                End With
            Case "Weekly_NewLeads"
                Dim obj As ConversionSummaryReportDO = Session("ReportParams")
                Dim ds As New DataSet
                Dim dt As New DataTable

                With rptReport.LocalReport
                    .DataSources.Clear()
                    .ReportPath = "Views/Reports/RDLCFiles/NewLeads_WeeklySummaryReport.rdlc"
                    .SetParameters(New List(Of ReportParameter) From {
                        New ReportParameter("upby", obj.Username),
                        New ReportParameter("fromdate", obj.DateFrom),
                        New ReportParameter("todate", obj.DateToday),
                        New ReportParameter("branchCode", obj.BranchCode),
                        New ReportParameter("branchName", obj.BranchName),
                        New ReportParameter("BranchManager", obj.BranchManagerName),
                        New ReportParameter("WeekNumber", obj.WeekNumber),
                        New ReportParameter("Month", obj.Month),
                        New ReportParameter("Year", obj.Year)
                    })
                    dt = obj.GenerateReport(obj.Username,
                                             obj.DateFrom,
                                             obj.DateToday,
                                             obj.BranchCode, "NewLeads")
                    dt.TableName = "DataSet1"
                    ds.Tables.Add(dt)
                    .DataSources.Add(New ReportDataSource(dt.TableName, dt))
                    .DisplayName = "WeeklySummaryReport_NewLeads" + Date.Today
                    .Refresh()

                End With

            Case "Weekly_PARevisit"
                Dim obj As ConversionSummaryReportDO = Session("ReportParams")
                Dim ds As New DataSet
                Dim dt As New DataTable
                With rptReport.LocalReport
                    .DataSources.Clear()
                    .ReportPath = "Views/Reports/RDLCFiles/PARevisits_WeeklySummaryReport.rdlc"
                    .SetParameters(New List(Of ReportParameter) From {
                        New ReportParameter("upby", obj.Username),
                        New ReportParameter("fromdate", obj.DateFrom),
                        New ReportParameter("todate", obj.DateToday),
                        New ReportParameter("branchCode", obj.BranchCode),
                        New ReportParameter("branchName", obj.BranchName),
                        New ReportParameter("BranchManager", obj.BranchManagerName),
                        New ReportParameter("WeekNumber", obj.WeekNumber),
                        New ReportParameter("Month", obj.Month),
                        New ReportParameter("Year", obj.Year)
                    })
                    dt = obj.GenerateReport(obj.Username,
                                             obj.DateFrom,
                                             obj.DateToday,
                                             obj.BranchCode, "PARevisit")
                    dt.TableName = "DataSet1"
                    ds.Tables.Add(dt)
                    .DataSources.Add(New ReportDataSource(dt.TableName, dt))
                    .DisplayName = "WeeklySummaryReport_PARevisit" + Date.Today
                    .Refresh()

                End With

            Case "Monthly_CSR"
                Dim obj As ConversionSummaryReportDO = Session("ReportParams")
                Dim ds, ds1 As New DataSet
                Dim dt, dt1 As New DataTable
                With rptReport.LocalReport
                    .DataSources.Clear()
                    .ReportPath = "Views/Reports/RDLCFiles/MonthlySummaryReport.rdlc"
                    .SetParameters(New List(Of ReportParameter) From {
                        New ReportParameter("upby", obj.Username),
                        New ReportParameter("fromdate", obj.DateFrom),
                        New ReportParameter("todate", obj.DateToday),
                        New ReportParameter("branchCode", obj.BranchCode),
                        New ReportParameter("branchName", obj.BranchName),
                        New ReportParameter("BranchManager", obj.BranchManagerName),
                        New ReportParameter("Month", obj.Month),
                        New ReportParameter("Year", obj.Year)
                    })
                    dt = obj.GenerateMonthlyReport(obj.Username,
                                             obj.BranchCode,
                                             obj.Year,
                                             obj.Month,
                                             obj.MonthCode)
                    dt.TableName = "DataSet1"
                    ds.Tables.Add(dt)

                    dt1 = obj.GenerateTotalMonthlyReport(obj.Username,
                                         obj.BranchCode,
                                         obj.Year,
                                         obj.Month,
                                         obj.MonthCode)
                    dt1.TableName = "DataSet2"
                    ds1.Tables.Add(dt1)
                    .DataSources.Add(New ReportDataSource(dt.TableName, dt))
                    .DataSources.Add(New ReportDataSource(dt1.TableName, dt1))
                    .DisplayName = "MonthlySummaryReport_" + Date.Today
                    .Refresh()

                End With


            Case "Annually_CSR"
                Dim obj As ConversionSummaryReportDO = Session("ReportParams")
                Dim ds, ds1 As New DataSet
                Dim dt, dt1 As New DataTable
                With rptReport.LocalReport
                    .DataSources.Clear()
                    .ReportPath = "Views/Reports/RDLCFiles/AnnuallySummaryReport.rdlc"
                    .SetParameters(New List(Of ReportParameter) From {
                        New ReportParameter("upby", obj.Username),
                        New ReportParameter("branchCode", obj.BranchCode),
                        New ReportParameter("branchName", obj.BranchName),
                        New ReportParameter("BranchManager", obj.BranchManagerName),
                        New ReportParameter("Year", obj.Year)
                    })
                    dt = ConversionSummaryReportDO.GetAnnuallyReport(obj.Username,
                                             obj.BranchCode,
                                             obj.Year)
                    dt.TableName = "DataSet1"
                    ds.Tables.Add(dt)

                    dt1 = obj.GenerateTotalAnnuallyReport(obj.Username,
                                         obj.BranchCode,
                                         obj.Year)
                    dt1.TableName = "DataSet2"
                    ds1.Tables.Add(dt1)
                    .DataSources.Add(New ReportDataSource(dt.TableName, dt))
                    .DataSources.Add(New ReportDataSource(dt1.TableName, dt1))
                    .DisplayName = "AnnualSummaryReport_" + Date.Today
                    .Refresh()

                End With
            Case "SummaryPerBM"
                Dim obj As SummaryBMDO = Session("ReportParams")
                Dim ds, ds1 As New DataSet
                Dim dt, dt1 As New DataTable
                With rptReport.LocalReport
                    .DataSources.Clear()
                    .ReportPath = "Views/Reports/RDLCFiles/AnnualSummary.rdlc"
                    .SetParameters(New List(Of ReportParameter) From {
                        New ReportParameter("UploadedBy", obj.UploadBy),
                        New ReportParameter("IsLead", obj.Lead),
                        New ReportParameter("IsProspect", obj.Prospect),
                        New ReportParameter("IsCustomer", obj.Customer)
                    })
                    dt = obj.GenerateSummaryPerBM(obj)
                    dt.TableName = "DataSet1"
                    ds.Tables.Add(dt)
                    .DataSources.Add(New ReportDataSource(dt.TableName, dt))
                    .DataSources.Add(New ReportDataSource(dt1.TableName, dt1))
                    .DisplayName = "PotentialAccountsPerBH_" + Date.Today
                    .Refresh()
                End With
            Case "ChangeInPotentialAccounts"
                Dim obj As ChangeInPotentialAccountsDO = Session("ReportParams")
                Dim ds As New DataSet
                Dim dt As New DataTable
                With rptReport.LocalReport
                    .DataSources.Clear()
                    .ReportPath = "Views/Reports/RDLCFiles/ChangeInPotentialAccounts.rdlc"
                    .SetParameters(New List(Of ReportParameter) From {
                        New ReportParameter("Year", obj.Year),
                        New ReportParameter("Month", obj.Month),
                        New ReportParameter("Week", obj.WeekNumber),
                        New ReportParameter("BranchName", obj.BranchName),
                        New ReportParameter("BranchCode", obj.BranchCode),
                        New ReportParameter("FromDate", obj.DateFrom),
                        New ReportParameter("ToDate", obj.DateToday)
                    })
                    dt = obj.GenerateReport(obj.BranchCode, obj.DateFrom, obj.DateToday, obj.Year, obj.Month)
                    dt.TableName = "DataSet1"
                    ds.Tables.Add(dt)
                    .DataSources.Add(New ReportDataSource(dt.TableName, dt))
                    .DisplayName = "ChangeInPotentialAccounts_" + Date.Today
                    .Refresh()
                End With

            Case "WeeklyActivity"
                Dim obj As WeeklyActivityDO = Session("ReportParams")
                Dim ds, ds1 As New DataSet
                Dim dt, dt1 As New DataTable
                With rptReport.LocalReport
                    .DataSources.Clear()
                    .ReportPath = "Views/Reports/RDLCFiles/WeeklyActivity.rdlc"
                    .SetParameters(New List(Of ReportParameter) From {
                        New ReportParameter("upby", obj.BranchHeadName),
                        New ReportParameter("fromdate", obj.DateFrom),
                        New ReportParameter("todate", obj.DateTo),
                        New ReportParameter("branchCode", obj.BranchCode),
                        New ReportParameter("branchName", obj.Branch),
                        New ReportParameter("BranchHead", obj.UploadedBy),
                        New ReportParameter("YearNumber", obj.YearNumber),
                        New ReportParameter("Month", obj.Month),
                        New ReportParameter("Week", obj.Week),
                        New ReportParameter("TableName", obj.TableName)
                    })
                    If obj.TableName = "Leads" Then
                        dt = MasterDO.listRecordsToTable(Of WeeklyActivityDO)(obj.GetClientsFilteredByDate(obj.BranchHeadName, obj.DateFrom, obj.DateTo, obj.BranchCode))
                    ElseIf obj.TableName = "PARevisit" Then
                        dt = MasterDO.listRecordsToTable(Of WeeklyActivityDO)(obj.GetWeeklyActivityCPARevisitClients(obj.BranchHeadName, obj.DateFrom, obj.DateTo, obj.BranchCode))
                    End If

                    dt.TableName = "DataSet1"
                    ds.Tables.Add(dt)
                    .DataSources.Add(New ReportDataSource(dt.TableName, dt))
                    .DisplayName = "WeeklyActivityReport_" + Date.Now.ToString("MMddyyyy_hhmmss")
                    .Refresh()
                End With

            Case "SummaryPerGrp_LoansReport"
                Dim obj As SummaryPerGroupDO = Session("ReportParams")
                Dim ds As New DataSet
                Dim dt As New DataTable
                With rptReport.LocalReport
                    .DataSources.Clear()
                    .ReportPath = "Views/Reports/RDLCFiles/SummaryPerGroup_LoansReport.rdlc"
                    .SetParameters(New List(Of ReportParameter) From {
                        New ReportParameter("BranchCode", obj.BranchCode),
                        New ReportParameter("Year", obj.Year),
                        New ReportParameter("BranchName", obj.BranchName),
                        New ReportParameter("ClientType", obj.ClientType),
                        New ReportParameter("IndustryType", obj.IndustryType),
                        New ReportParameter("ADBRangeName", obj.ADBRangeName),
                        New ReportParameter("IndustryTypeDesc", obj.IndustryTypeDesc),
                         New ReportParameter("ClientTypeDesc", obj.ClientTypeDesc),
                        New ReportParameter("ADBRangeNameDesc", obj.ADBRangeNameDesc)
                    })
                    dt = obj.GenerateAnnualReport(obj, "Loans")
                    dt.TableName = "DataSet1"
                    ds.Tables.Add(dt)
                    .DataSources.Add(New ReportDataSource(dt.TableName, dt))
                    .DisplayName = "SummaryPerGrp_LoansReport_" + Date.Now.ToString("MMddyyyy_hhmmss")
                    .Refresh()
                End With

            Case "SummaryPerGrp_CASAReport"
                Dim obj As SummaryPerGroupDO = Session("ReportParams")
                Dim ds As New DataSet
                Dim dt As New DataTable
                With rptReport.LocalReport
                    .DataSources.Clear()
                    .ReportPath = "Views/Reports/RDLCFiles/SummaryPerGroup_CASAReport.rdlc"
                    .SetParameters(New List(Of ReportParameter) From {
                        New ReportParameter("BranchCode", obj.BranchCode),
                        New ReportParameter("Year", obj.Year),
                        New ReportParameter("BranchName", obj.BranchName),
                        New ReportParameter("ClientType", obj.ClientType),
                        New ReportParameter("IndustryType", obj.IndustryType),
                        New ReportParameter("ADBRangeName", obj.ADBRangeName),
                        New ReportParameter("IndustryTypeDesc", obj.IndustryTypeDesc),
                        New ReportParameter("ClientTypeDesc", obj.ClientTypeDesc),
                        New ReportParameter("ADBRangeNameDesc", obj.ADBRangeNameDesc)
                    })
                    dt = obj.GenerateAnnualReport(obj, "CASA")
                    dt.TableName = "DataSet1"
                    ds.Tables.Add(dt)
                    .DataSources.Add(New ReportDataSource(dt.TableName, dt))
                    .DisplayName = "SummaryPerGrp_CASAReport_" + Date.Now.ToString("MMddyyyy_hhmmss")
                    .Refresh()

                End With
            Case "SummaryPerBranch"
                Dim obj As SummaryPerGroupDO = Session("ReportParams")
                Dim ds As New DataSet
                Dim dt As New DataTable
                With rptReport.LocalReport
                    .DataSources.Clear()
                    .ReportPath = "Views/Reports/RDLCFiles/SummaryPerBranch.rdlc"
                    .SetParameters(New List(Of ReportParameter) From {
                        New ReportParameter("module", obj.moduleName),
                        New ReportParameter("RegionName", obj.GroupCodeText),
                        New ReportParameter("header", obj.rptHeader),
                        New ReportParameter("RegionCode", obj.GroupCode),
                        New ReportParameter("Year", obj.Year),
                        New ReportParameter("MonthName", obj.Month),
                        New ReportParameter("Month", obj.MonthCode),
                        New ReportParameter("Week", obj.WeekNumber),
                        New ReportParameter("ClientType", obj.ClientType),
                        New ReportParameter("isAdmin", obj.isAdmin)
                    })
                    dt = obj.GenerateReport(obj)
                    dt.TableName = "DataSet1"
                    ds.Tables.Add(dt)
                    .DataSources.Add(New ReportDataSource(dt.TableName, dt))
                    .DisplayName = obj.moduleName + "SummaryPerBranch_" + Date.Now.ToString("MMddyyyy_hhmmss")
                    .Refresh()

                End With
            Case "AnnualSummaryPerBranch"
                Dim obj As SummaryPerGroupDO = Session("ReportParams")
                Dim ds As New DataSet
                Dim dt As New DataTable
                With rptReport.LocalReport
                    .DataSources.Clear()
                    .ReportPath = "Views/Reports/RDLCFiles/AnnualSummaryPerBranch.rdlc"
                    .SetParameters(New List(Of ReportParameter) From {
                        New ReportParameter("module", obj.moduleName),
                        New ReportParameter("RegionName", obj.GroupCodeText),
                        New ReportParameter("header", obj.rptHeader),
                        New ReportParameter("RegionCode", obj.GroupCode),
                        New ReportParameter("Year", obj.Year),
                        New ReportParameter("ClientType", obj.ClientType),
                        New ReportParameter("isAdmin", obj.isAdmin)
                    })
                    dt = obj.GenerateReport(obj)
                    dt.TableName = "DataSet1"
                    ds.Tables.Add(dt)
                    .DataSources.Add(New ReportDataSource(dt.TableName, dt))
                    .DisplayName = obj.moduleName + "SummaryPerBranch_" + Date.Now.ToString("MMddyyyy_hhmmss")
                    .Refresh()

                End With

            Case "SummaryPerGroupBranch"
                Dim obj As SummaryPerGroupBranchDO = Session("ReportParams")
                Dim ds As New DataSet
                Dim dt As New DataTable
                With rptReport.LocalReport
                    .DataSources.Clear()
                    .ReportPath = "Views/Reports/RDLCFiles/SummaryPerGroupBranchReport.rdlc"
                    .SetParameters(New List(Of ReportParameter) From {
                        New ReportParameter("groupName", obj.GroupCode & " - " & obj.Group),
                        New ReportParameter("year", obj.Year),
                        New ReportParameter("industrytype", obj.IndustryTypeDesc),
                        New ReportParameter("position", obj.RoleID),
                        New ReportParameter("tag", ""),
                        New ReportParameter("IsGroupHead", obj.IsGroupHead)
                    })
                    Dim lst As List(Of SummaryPerGroupBranchDO) = obj.GetCPASummaryPerGroupBranch(obj, obj.RoleID)
                    dt = MasterDO.listRecordsToTable(Of SummaryPerGroupBranchDO)(lst)
                    dt.TableName = "DataSet1"
                    ds.Tables.Add(dt)
                    .DataSources.Add(New ReportDataSource(dt.TableName, dt))
                    .DisplayName = "SummaryPerGroupBranchReport" + Date.Now.ToString("MMddyyyy_hhmmss")
                    .Refresh()
                End With

        End Select

        rptReport.Visible = True
        rptReport.ZoomMode = ZoomMode.PageWidth
    End Sub

End Class