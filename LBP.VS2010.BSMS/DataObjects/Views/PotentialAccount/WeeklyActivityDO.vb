Imports LBP.VS2010.BSMS.CustomValidators
Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.DataAccess
Imports LBP.VS2010.BSMS.Utilities
Imports System.Globalization.CultureInfo

Public Class WeeklyActivityDO
    Inherits MasterDO

#Region "INSTANTIATION"
    Sub New()
    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
#End Region

#Region "DECLARATION"
    <DisplayLabel("Group")>
    <RequiredConditional.ValueEquals("ViewPage", {"TableView"})>
    Property GroupCode As String

    <DisplayLabel("Branch Head")>
    <RequiredConditional.ValueEquals("ViewPage", {"TableView"})>
    Property BranchHeadName As String

    <DisplayLabel("User Name")>
    Property Username As String

    <DisplayLabel("Branch")>
    <DataColumnMapping("BranchName")>
    <RequiredConditional.ValueEquals("ViewPage", {"TableView"})>
    Property Branch As String

    <DisplayLabel("Year")>
    <DataColumnMapping("Year")>
    <RequiredField>
    Property YearNumber As String

    <DisplayLabel("Month")>
    <DataColumnMapping("MonthNum")>
    <RequiredConditional.ValueEquals("ViewPage", {"DetailView"})>
    Property MonthCode As String

    <DisplayLabel("MonthName")>
    <DataColumnMapping("MonthName")>
    Property Month As String

    <DisplayLabel("WeekNumber")>
    <DataColumnMapping("WeekNum")>
    <RequiredConditional.ValueEquals("ViewPage", {"DetailView"})>
    Property WeekNumber As String

    <DataColumnMapping("Week")>
    Property Week As String

    <DataColumnMapping("UploadedBy")>
    Property UploadedBy As String

    <DataColumnMapping("BrCode")>
    Property BranchCode As String

    <DataColumnMapping("Fullname")>
    Property Fullname As String
    Property Position As String
    Property UserID As String

    Property DateFrom As String
    Property DateTo As String

    <DataColumnMapping("ClientID")>
    Property ClientID As String

    <DataColumnMapping("ClientType")>
    Property ClientType As String

    <DataColumnMapping("Address")>
    Property Address As String

    <DataColumnMapping("ContactNo")>
    Property ContactNo As String

    <DisplayLabel("Lead")>
    <DataColumnMapping("Lead")>
    Property Lead As String

    <DisplayLabel("Suspect")>
    <DataColumnMapping("Suspect")>
    Property Suspect As String

    <DisplayLabel("Prospect")>
    <DataColumnMapping("Prospect")>
    Property Prospect As String

    <DisplayLabel("Revisit")>
    <DataColumnMapping("Revisit")>
    Property Revisit As String

    <DisplayLabel("Total")>
    <DataColumnMapping("Total")>
    Property Total As String

    <DataColumnMapping("Customer")>
    Property Customer As String

    <DataColumnMapping("CASATypes")>
    Property CASATypes As String

    <DataColumnMapping("Amount")>
    Property Amount As String

    <DataColumnMapping("AmountOthers")>
    Property AmountOthers As String

    <DataColumnMapping("ADB")>
    Property ADB As String

    <DataColumnMapping("Lost")>
    Property Lost As String

    <DataColumnMapping("Reason")>
    Property Reason As String

    <DataColumnMapping("OtherATypes")>
    Property OtherATypes As String

    <DataColumnMapping("DateEncoded")>
    Property DateEncoded As String

    <DataColumnMapping("Visits")>
    Property Visits As String

    <DataColumnMapping("AccountNumbers")>
    Property AccountNumbers As String

    <DataColumnMapping("LeadSource")>
    Property LeadSource As String

    <DataColumnMapping("IndustryType")>
    Property Industry As String

    <DataColumnMapping("Remarks")>
    Property Remarks As String

    <DataColumnMapping("ProductOffered")>
    Property ProductsOffered As String

    <DataColumnMapping("LoansProductsAvailed")>
    Property LoansProductsAvailed As String

    <DataColumnMapping("LoanAmountReported")>
    Property LoanAmountReported As String

    <DataColumnMapping("TotalSuspect")>
    Property TotalSuspect As String

    <DataColumnMapping("TotalProspect")>
    Property TotalProspect As String

    <DataColumnMapping("TotalCustomer")>
    Property TotalCustomer As String



    Property TotalLead As String
    Property RoleName As String
    Property TableName As String
    Property ViewPage As String
    Property ReportPage As String = "ReportViewer.aspx"
#End Region

#Region "FUNCTIONS"
#Region "GET"
    Public Function GetGroupList() As DataTable
        Dim dal As New WeeklyActivityDAL
        Dim dt As DataTable = dal.GetGroupList()
        dt.DefaultView.Sort = "RegionCode ASC"
        Return dt.DefaultView.ToTable
    End Function

    Public Function GetUsersList(ByVal GroupCode As String,
                                 ByVal BranchCode As String,
                                 ByVal RoleID As String) As DataTable
        Dim dal As New WeeklyActivityDAL
        Return dal.GetUsersList(GroupCode, BranchCode, Convert.ToInt64(RoleID))
    End Function

    Public Function GetBranchesList(ByVal groupCode As String) As DataTable
        Dim dal As New WeeklyActivityDAL
        Dim dt As DataTable = dal.GetBranchesList(groupCode)
        dt.DefaultView.Sort = "BranchCode ASC"
        Return dt.DefaultView.ToTable
    End Function

    Public Function GetWeeklyActivity(ByVal obj As WeeklyActivityDO) As List(Of WeeklyActivityDO)
        Dim dal As New WeeklyActivityDAL

        Try
            Dim lst As New List(Of WeeklyActivityDO)
            Dim dt As DataTable = dal.GetWeeklyActivity(obj.BranchHeadName, obj.YearNumber)

            If dt.Rows.Count > 0 Then
                Dim TotalLead As Integer = 0
                Dim TotalRevisit As Integer = 0
                Dim Grandtotal As Integer = 0
                Dim curMonthTotalLead As Integer = 0
                Dim curMonthTotalRevisit As Integer = 0
                Dim curMonthTotalSubTotal As Integer = 0

                Dim r As Integer = 0
                While r <> dt.Rows.Count - 1
                    Dim curMonth As String = dt(r)("MonthNum").ToString()
                    If curMonth <> "Sub-Totals" Then
                        curMonthTotalLead += CInt(dt(r)("Lead"))
                        curMonthTotalRevisit += CInt(dt(r)("Revisit"))
                        curMonthTotalSubTotal += CInt(dt(r)("Total"))
                        TotalLead += CInt(dt(r)("Lead"))
                        TotalRevisit += CInt(dt(r)("Revisit"))
                        Grandtotal += CInt(dt(r)("Total"))
                    End If

                    If (curMonth <> dt(r + 1)("MonthNum").ToString() And curMonth <> "Sub-Totals") Then
                        Dim dr As DataRow = dt.NewRow()
                        dr("MonthNum") = "Sub-Totals"
                        dr("Lead") = curMonthTotalLead
                        dr("Revisit") = curMonthTotalRevisit
                        dr("Total") = curMonthTotalSubTotal
                        dt.Rows.InsertAt(dr, r + 1)

                        curMonthTotalLead = 0
                        curMonthTotalRevisit = 0
                        curMonthTotalSubTotal = 0
                    End If

                    r += 1
                End While

                Dim dr1 As DataRow = dt.NewRow()
                dr1("MonthNum") = "Sub-Totals"
                dr1("Lead") = curMonthTotalLead
                dr1("Revisit") = curMonthTotalRevisit
                dr1("Total") = curMonthTotalSubTotal
                dt.Rows.InsertAt(dr1, dt.Rows.Count)

                Dim dr2 As DataRow = dt.NewRow()
                dr2("MonthNum") = "Total Leads"
                dr2("Lead") = TotalLead
                dr2("Revisit") = TotalRevisit
                dr2("Total") = Grandtotal
                dt.Rows.InsertAt(dr2, dt.Rows.Count)

                lst = listRecords(Of WeeklyActivityDO)(dt)
                Dim Month_FirstEntry As Boolean = False
                For z As Integer = 0 To lst.Count - 1
                    Month_FirstEntry = z = 0 Or lst(If(z = 0, 0, z - 1)).MonthCode = "Sub-Totals"
                    lst(z).Month = If(lst(z).MonthCode = "Sub-Totals" Or lst(z).MonthCode = "Total Leads", lst(z).MonthCode,
                                   If(Month_FirstEntry, CurrentCulture.DateTimeFormat.GetMonthName(lst(z).MonthCode), ""))
                    lst(z).Week = If(lst(z).MonthCode <> "Sub-Totals", "Week " + lst(z).WeekNumber, "")
                Next
            End If

            Return lst

        Catch ex As Exception
            Logger.writeLog("Weekly Activity", "GetWeeklyActivity", "", ex.Message)
        End Try
        Return Nothing
    End Function
#End Region

#Region "VIEW"

    Public Function GetClientsFilteredByDate(ByVal UploadedBy As String,
                                             ByVal DateFrom As String,
                                             ByVal DateTo As String,
                                             ByVal BranchCode As String) As List(Of WeeklyActivityDO)
        Dim dal As New WeeklyActivityDAL

        Try
            Dim dt As DataTable = dal.GetNewLeadsList(UploadedBy, DateFrom, DateTo, BranchCode)
            Dim lst As List(Of WeeklyActivityDO) = MasterDO.listRecords(Of WeeklyActivityDO)(dt)

            Dim ctr As Integer = 0
            While ctr < lst.Count
                If (lst(ctr).ClientType).Contains("Potential Account") Then
                    lst(ctr).CASATypes = GetDescription("CASATypes", If(lst(ctr).CASATypes Is Nothing, "", lst(ctr).CASATypes))
                    lst(ctr).ProductsOffered = GetDescription("ProductsOffered", If(lst(ctr).ProductsOffered Is Nothing, "", lst(ctr).ProductsOffered))
                    lst(ctr).OtherATypes = GetDescription("OtherATypes", If(lst(ctr).OtherATypes Is Nothing, "", lst(ctr).OtherATypes))
                    lst(ctr).TotalLead = CInt(lst(If(ctr = 0, 0, ctr - 1)).TotalLead) + 1


                    ctr += 1
                Else
                    lst.Remove(lst(ctr))
                    If lst.Count = 0 Then Exit While
                End If
            End While

            Return lst

        Catch ex As Exception
            Logger.writeLog("Weekly Activity", "GetClientsFilteredByDate", "", ex.Message)
        End Try
        Return Nothing
    End Function

    Public Function GetWeeklyActivityCPARevisitClients(ByVal UploadedBy As String,
                                                       ByVal DateFrom As String,
                                                       ByVal DateTo As String,
                                                       ByVal BranchCode As String) As List(Of WeeklyActivityDO)
        Dim dal As New WeeklyActivityDAL

        Try
            Dim dt As DataTable = dal.GetPotentialAccountsList(UploadedBy, DateFrom, DateTo, BranchCode)
            Dim lst As List(Of WeeklyActivityDO) = MasterDO.listRecords(Of WeeklyActivityDO)(dt)

            Dim ctr As Integer = 0
            While ctr < lst.Count
                If (lst(ctr).ClientType).Contains("Potential Account") Then
                    lst(ctr).CASATypes = GetDescription("CASATypes", If(lst(ctr).CASATypes Is Nothing, "", lst(ctr).CASATypes))
                    lst(ctr).ProductsOffered = GetDescription("ProductsOffered", If(lst(ctr).ProductsOffered Is Nothing, "", lst(ctr).ProductsOffered))
                    lst(ctr).OtherATypes = GetDescription("OtherATypes", If(lst(ctr).OtherATypes Is Nothing, "", lst(ctr).OtherATypes))
                    lst(ctr).TotalProspect = CInt(lst(If(ctr = 0, 0, ctr - 1)).TotalProspect)
                    lst(ctr).TotalSuspect = CInt(lst(If(ctr = 0, 0, ctr - 1)).TotalSuspect)
                    lst(ctr).TotalCustomer = CInt(lst(If(ctr = 0, 0, ctr - 1)).TotalCustomer)
                    ctr += 1
                Else
                    lst.Remove(lst(ctr))
                    If lst.Count = 0 Then Exit While
                End If
            End While

            Return lst

        Catch ex As Exception
            Logger.writeLog("Weekly Activity", "GetWeeklyActivityCPARevisitClients", "", ex.Message)
        End Try

        Return Nothing
    End Function

    Public Function GetDescription(ByVal table As String, ByVal value As String) As String
        Dim dal As New WeeklyActivityDAL
        Dim dt As New DataTable
        Dim description As String = String.Empty

        Try
            Dim codes() As String = value.Split(","c)

            If table = "ProductsOffered" Or table = "OtherATypes" Or table = "CASATypes" Then
                dt = If(table <> "CASATypes", dal.OtherProductList(), dal.CASAProductList())

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
            End If

        Catch ex As Exception
            Logger.writeLog("Weekly Activity", "GetDescription", "", ex.Message)
        End Try

        Return description
    End Function

#End Region

#End Region

End Class