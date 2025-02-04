Imports System.Configuration.ConfigurationManager
Imports System.Linq
Imports System.IO
Imports System.Data
Imports System.ComponentModel
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.CustomValidators.LengthAndValueValidator
Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables
Imports LBP.VS2010.BSMS.CustomValidators
Imports System.Configuration
Imports LBP.VS2010.BSMS.DataAccess

Public Class IndustriesDO
    Inherits MasterDO

#Region "INSTANTIATION"
    Sub New()
    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
#End Region

    Partial Class IndustryResult
        Property Fields As String
        Property ValueFrom As String
        Property ValueTo As String
    End Class

#Region "TABLE COLUMNS"
    <DataColumnMapping("DescIndicator")>
   <DisplayLabel("DescIndicator")>
    Property DescIndicator As String

    <DataColumnMapping("IndustryCode")>
 <DisplayLabel("IndustryCode")>
    Property IndustryCode As String

    <DataColumnMapping("IndustryDesc")>
<DisplayLabel("IndustryDesc")>
    Property IndustryDesc As String

    <DataColumnMapping("IndustryFlag")>
<DisplayLabel("IndustryFlag")>
    Property IndustryFlag As String

    <DataColumnMapping("IndustryType")>
<DisplayLabel("IndustryType")>
    Property IndustryType As String

    <DataColumnMapping("IndustryIndicator")>
<DisplayLabel("IndustryIndicator")>
    Property IndustryIndicator As String

    <DataColumnMapping("Status")>
    <DisplayLabel("Status")>
    Property Status As String

    Property UserID As String

    Property InEdit As Boolean
    Property InUse As Boolean
#End Region

#Region "MISC COLUMNS"
    '<RequiredField()>
    Property SearchBy As String
    '<RequiredField()>
    <DisplayLabel("Search Value")>
    Property SearchValue As String
#End Region

#Region "MISC VARIABLES"
    Property Search As String
    Property SearchBy1 As String
    Property SearchType As String
    Property errorMessage As String
    Property Action As String
    Property success As String
    Property message As String
    'Property isSuccess As Boolean = True
    Property LogonUser As String
    Property TempStatusID As String
#End Region


    'Public Function ListIndustries() As List(Of IndustriesDO)
    '    Dim dal As New IndustriesDA
    '    Dim dt As DataTable
    '    Dim list As New List(Of IndustriesDO)

    '    dt = dal.IndustriesList()

    '    For Each dtRow As DataRow In dt.Rows
    '        Dim IndustriesDO As New IndustriesDO With {
    '         .DescIndicator = dtRow.Item("DescIndicator"),
    '         .IndustryCode = dtRow.Item("IndustryCode"),
    '         .IndustryDesc = dtRow.Item("IndustryDesc"),
    '         .IndustryType = dtRow.Item("IndustryType"),
    '         .IndustryFlag = dtRow.Item("IndustryFlag"),
    '         .IndustryIndicator = dtRow.Item("IndustryIndicator")
    '        }
    '        list.Add(IndustriesDO)
    '    Next
    '    Return list
    'End Function

#Region "DROPDOWN"
    Shared Function GetIndustryTypeList() As DataTable
        Dim dal As New IndustriesDA

        Return dal.GetIndustryTypeList()
    End Function
#End Region

    Shared Function GetList(ByVal searchText As String, ByVal filterStatus As String, ByVal accessObj As ProfileDO) As List(Of IndustriesDO)
        Dim dal As New IndustriesDA
        Dim dt As DataTable
        Dim list As New List(Of IndustriesDO)

        dt = dal.GetList(searchText, filterStatus)

        If Not IsNothing(dt) Then
            Dim lst As New List(Of IndustriesDO)
            Dim canUpdate As Boolean = accessObj.CanUpdate = "Y"
            Dim canApprove As Boolean = accessObj.CanApprove = "Y"
            Dim canInsert As Boolean = accessObj.CanInsert = "Y"
            Dim canDelete As Boolean = accessObj.CanDelete = "Y"
            Dim canPrint As Boolean = accessObj.CanPrint = "Y"
            Dim canView As Boolean = accessObj.CanView = "Y"
            Dim canUnlock As Boolean = accessObj.CanUnlock = "Y"
            If dt.Rows.Count > 0 Then

                For Each row As DataRow In dt.Rows
                    Dim access As New ProfileDO With {
                    .CanView = accessObj.CanView,
                    .CanUpdate = IIf(row.Item("InEdit") = 0, accessObj.CanUpdate, "N"),
                    .CanInsert = IIf(row.Item("InEdit") = 0, accessObj.CanInsert, "N"),
                    .CanApprove = accessObj.CanApprove,
                    .CanDelete = IIf(row.Item("InEdit") = 0, accessObj.CanDelete, "N")
                   }

                    lst.Add(New IndustriesDO With {
                    .Access = access,
                    .IndustryCode = row.Item("IndustryCode").ToString,
                    .IndustryDesc = row.Item("IndustryDesc").ToString,
                    .IndustryType = row.Item("IndustryType").ToString,
                    .Status = row.Item("Status").ToString,
                    .InEdit = row.Item("InEdit").ToString,
                    .TempStatusID = row.Item("TempStatusID").ToString
                })

                Next
            End If

            Return lst
        End If

        Return Nothing
    End Function

    Shared Function GetDetails(ByVal industryCode As String, ByVal action As String) As IndustriesDO
        Dim dal As New IndustriesDA
        Dim industry As New IndustriesDO With {.Action = action}
        Dim dt As DataTable = dal.GetDetails(industryCode)

        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                With industry
                    .IndustryCode = dt.Rows(0)("IndustryCode").ToString
                    .IndustryDesc = dt.Rows(0)("IndustryDesc").ToString
                    .IndustryType = dt.Rows(0)("IndustryType").ToString
                    .Status = dt.Rows(0)("Status").ToString
                    .InEdit = IIf(dt.Rows(0)("InEdit") = "1", True, False)
                    .InUse = IIf(dt.Rows(0)("InUse") = "1", True, False)
                    .TempStatusID = dt.Rows(0)("TempStatusID").ToString
                End With
            End If
        End If

        Return industry
    End Function

    Shared Function GetMergeDetails(ByVal industryCode As String) As List(Of IndustryResult)
        Dim dal As New IndustriesDA
        Dim dt As DataTable = dal.GetMergeDetails(industryCode)

        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                Dim Fields, ValueFrom, ValueTo As String
                Dim mergeDetails As New List(Of IndustryResult)

                Fields = dt.Rows(0).Item("Fields").ToString
                ValueFrom = dt.Rows(0).Item("ValueFrom").ToString
                ValueTo = dt.Rows(0).Item("ValueTo").ToString

                Dim arrFields As String() = Fields.Split("|")
                Dim arrValueFrom As String() = If(String.IsNullOrWhiteSpace(ValueFrom), {}, ValueFrom.Split("|"))
                Dim arrValueTo As String() = If(String.IsNullOrWhiteSpace(ValueTo), {}, ValueTo.Split("|"))

                For i As Integer = 0 To arrFields.Count - 1
                    Dim industryResult As New IndustryResult With {
                        .Fields = arrFields(i),
                        .ValueFrom = If(Not String.IsNullOrEmpty(ValueFrom), arrValueFrom(i), ""),
                        .ValueTo = If(Not String.IsNullOrEmpty(ValueTo), arrValueTo(i), "")
                    }

                    mergeDetails.Add(industryResult)
                Next

                Return mergeDetails
            End If
        End If

        Return Nothing
    End Function

#Region "INSERT/UPDATE"
    Public Function SaveIndustry() As Boolean
        Dim success As Boolean = True
        Dim dal As New IndustriesDA
        Dim dal2 As New IndustriesDA
        Dim dt As New DataTable

        If IsNothing(Action) Then
            Action = String.Empty
        End If

        Select Case Action
            Case "add", String.Empty
                TempStatusID = GetSection("Commons")("ForInsert").ToString

            Case "edit"
                TempStatusID = GetSection("Commons")("ForUpdate").ToString

            Case "delete"
                TempStatusID = GetSection("Commons")("ForDelete").ToString

        End Select

        If Not Action.Equals("delete") Or Action.Equals(String.Empty) Then
            dt = dal2.CheckIfIndustryExists(IndustryCode, IndustryDesc)
            If Not dt.Rows.Count > 0 Then
                If Not dal.InsertIndustryTemp(IndustryCode, IndustryDesc, IndustryType, Status, LogonUser, TempStatusID) Then
                    errMsg = dal.ErrorMsg
                    success = False
                End If
            Else
                errMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.IS_EXISTS2, "Industry Code/Industry Desc", {Nothing})
                success = False
            End If
        Else
            If Not dal.InsertIndustryTemp(IndustryCode, IndustryDesc, IndustryType, Status, LogonUser, TempStatusID) Then
                errMsg = dal.ErrorMsg
                success = False
            End If
        End If

        Return success
    End Function
#End Region

#Region "APPROVE"
    Public Function ApproveIndustry() As Boolean
        Dim dal As New IndustriesDA
        Dim success As Boolean = True
        Dim addressList As New List(Of String)

        If Not String.IsNullOrWhiteSpace(TempStatusID) Then
            Select Case TempStatusID

                Case GetSection("Commons")("ForInsert").ToString
                    Dim dt As New DataTable
                    If Not dal.InsertIndustry(IndustryCode, LogonUser, False) Then
                        errMsg = dal.ErrorMsg
                        Return False
                    End If

                    success = True

                Case GetSection("Commons")("ForUpdate").ToString
                    If Not dal.UpdateIndustry(IndustryCode, LogonUser, False) Then
                        errMsg = dal.ErrorMsg
                        Return False
                    End If

                Case GetSection("Commons")("ForDelete").ToString
                    If Not dal.DeleteIndustry(IndustryCode, LogonUser, False) Then
                        errMsg = dal.ErrorMsg
                        Return False
                    End If
            End Select

            If Not dal.DeleteIndustryTemp(IndustryCode, LogonUser, False) Then
                errMsg = dal.ErrorMsg
                Return False
            End If

            If success Then
                dal.CommitTrans()
            Else
                dal.RollbackTrans()
            End If

        Else
            errMsg = Messages.ErrorMessages.DataBase.getErrorMessage(PageFunctions.SAVE, Nothing)
            success = False
        End If

        Return success
    End Function
#End Region

#Region "REJECT"
    Public Function RejectIndustry() As Boolean
        Dim dal As New IndustriesDA
        Dim success As Boolean = True

        If Not dal.DeleteIndustryTemp(IndustryCode, LogonUser) Then
            errMsg = dal.ErrorMsg
            Return False
        End If
        Return success
    End Function
#End Region

#Region "Report"
    Public Function GetIndustryListReport(ByVal searchText As String, ByVal filterStatus As String) As DataTable
        Dim dal As New IndustriesDA

        Return dal.GetList(searchText, filterStatus)
    End Function

#End Region

    Public Function CheckIfIndustryExists(ByVal industryCode As String, ByVal industryDesc As String) As DataTable
        Dim dal As New IndustriesDA

        Return dal.CheckIfIndustryExists(industryCode, industryDesc)
    End Function

End Class
