Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.CustomValidators.LengthAndValueValidator
Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.CustomValidators
Imports LBP.VS2010.BSMS.DataAccess

Public Class SummaryPerGroupBranchDO
    Inherits MasterDO

#Region "INSTANTIATION"
    Sub New()
    End Sub

    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
#End Region

#Region "TABLE COLUMNS"
    <DataColumnMapping("CCode")>
    Property BranchCode As String

    <DataColumnMapping("CName")>
    Property BranchName As String

    <DataColumnMapping("CPACount")>
    Property TotalCPACount As String

    <DataColumnMapping("CPALeads")>
    Property TotalLeads As String

    <DataColumnMapping("CPARevisit")>
    Property TotalRevisits As String

    Property TotalCPACount_Group As String
    Property TotalLeads_Group As String
    Property TotalRevisits_Group As String

    Property RoleID As String
    Property IsGroupHead As Boolean = False

#End Region

#Region "MISC COLUMNS"
    <DisplayLabel("Year")>
    <RequiredField()>
    Property Year As String

    <DisplayLabel("Industry Type")>
    <RequiredField()>
    Property IndustryType As String

    Property IndustryTypeDesc As String

    <DataColumnMapping("RegionCode")>
    <DisplayLabel("Group")>
    <RequiredField()>
    Property GroupCode As String

    Property Group As String
#End Region

#Region "FUNCTIONS"
#Region "GET"
    Public Function GetIndustryList() As DataTable
        Dim dal As New SummaryPerGroupBranchDAL
        Return dal.GetIndustryList()
    End Function

    Public Function GetGroupList() As DataTable
        Dim dal As New SummaryPerGroupBranchDAL
        Dim dt As DataTable = dal.GetGroupList()
        dt.DefaultView.Sort = "RegionCode ASC"
        Return dt.DefaultView.ToTable
    End Function

    Public Function GetBranchesList(ByVal groupCode As String,
                                    Optional ByVal willCommit As Boolean = True) As DataTable
        Dim dal As New SummaryPerGroupBranchDAL
        Dim dt As DataTable = dal.GetBranchesList(groupCode, willCommit)
        dt.DefaultView.Sort = "BranchCode ASC"
        Return dt.DefaultView.ToTable
    End Function

    Public Function GetCPASummaryPerGroupBranch(ByVal obj As SummaryPerGroupBranchDO,
                                             ByVal RoleId As String) As List(Of SummaryPerGroupBranchDO)
        Dim dal As New SummaryPerGroupBranchDAL
        Dim lst As New List(Of SummaryPerGroupBranchDO)
        Dim dt As New DataTable

        Try
            Dim dtBranch As DataTable = GetBranchesList(obj.GroupCode, False)

            If dtBranch.Rows.Count > 0 Then
                dt.Columns.Add("CCode")
                dt.Columns.Add("CName")
                dt.Columns.Add("CPACount")
                dt.Columns.Add("CPALeads")
                dt.Columns.Add("CPARevisit")

                For Each r As DataRow In dtBranch.Rows
                    Dim dtCtrPerBranch As DataTable = dal.GetSummaryCountPerBranch(r("BranchCode").ToString(), obj.Year, obj.IndustryType, RoleId, False)

                    Dim dr As DataRow = dt.NewRow()
                    dr("CCode") = r("BranchCode").ToString()
                    dr("CName") = r("BranchName").ToString()

                    If dtCtrPerBranch IsNot Nothing AndAlso dtCtrPerBranch.Rows.Count > 0 Then
                        dr("CPACount") = If(dtCtrPerBranch(0)("CPACount").ToString() <> "", dtCtrPerBranch(0)("CPACount").ToString(), "0")
                        dr("CPALeads") = If(dtCtrPerBranch(0)("CPALeads").ToString() <> "", dtCtrPerBranch(0)("CPALeads").ToString(), "0")
                        dr("CPARevisit") = If(dtCtrPerBranch(0)("CPARevisit").ToString() <> "", dtCtrPerBranch(0)("CPARevisit").ToString(), "0")

                        TotalCPACount_Group += CInt(If(dtCtrPerBranch(0)("CPACount").ToString() <> "", dtCtrPerBranch(0)("CPACount").ToString(), "0"))
                        TotalLeads_Group += CInt(If(dtCtrPerBranch(0)("CPALeads").ToString() <> "", dtCtrPerBranch(0)("CPALeads").ToString(), "0"))
                        TotalRevisits_Group += CInt(If(dtCtrPerBranch(0)("CPARevisit").ToString() <> "", dtCtrPerBranch(0)("CPARevisit").ToString(), "0"))
                    Else
                        dr("CPACount") = "0"
                        dr("CPALeads") = "0"
                        dr("CPARevisit") = "0"
                    End If

                    dt.Rows.Add(dr)

                Next

                lst = listRecords(Of SummaryPerGroupBranchDO)(dt)

                'Dim dtTotalPerRegion As DataTable = dal.GetSummaryCountPerRegion(obj.GroupCode, obj.Year, obj.IndustryTypeDesc, RoleId, False)
                For Each l In lst
                    l.TotalCPACount_Group = CInt(TotalCPACount_Group)
                    l.TotalLeads_Group = CInt(TotalLeads_Group)
                    l.TotalRevisits_Group = CInt(TotalRevisits_Group)
                Next

            End If

        Catch ex As Exception
            Logger.writeLog("Summary per Group/Branch", "GetCPASummaryCountPerRegion", "", ex.Message)
        End Try

        Return lst

    End Function

#End Region
#End Region
End Class

