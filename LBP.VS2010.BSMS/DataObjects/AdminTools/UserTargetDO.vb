Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.CustomValidators.LengthAndValueValidator
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages.Validation
Imports LBP.VS2010.BSMS.DataAccess
Imports System.Text.RegularExpressions
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.CustomValidators
Imports System.Configuration.ConfigurationManager
Imports System.Configuration

Public Class UserTargetDO
    Inherits MasterDO

    Partial Class UserTargetResult
        Property Fields As String
        Property ValueFrom As String
        Property ValueTo As String
    End Class


#Region "INSTANTIATION"
    Sub New()

    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection, ByVal accessObj As ProfileDO)
        MyBase.New(dr, columnNames, accessObj)
    End Sub
#End Region


#Region "ACCESS"
    Property HasAccess As String

    Property AllowToUnlock As Boolean = False
#End Region

#Region "DECLARATION"

    <DisplayLabel("Branch Head")>
    <RequiredField(RequiredField.ControlType.SELECTION)>
    Property Username As String

    <DisplayLabel("Role")>
    Property RoleName As String


    Property Fullname As String

    <DisplayLabel("Status")>
    Property Status As String

    Property Role As String

    Property Branch As String

    Property StatusId As String

    <DisplayLabel("Group")>
    <RequiredField(RequiredField.ControlType.SELECTION)>
    Property GroupCode As String

    <DisplayLabel("Branch")>
    <RequiredField(RequiredField.ControlType.SELECTION)>
    Property BranchCode As String

    <DisplayLabel("CASA Target")>
    <RequiredField()>
    <ControlVariable(FieldVariables.CASA_TARGET)>
    <CustomValidationMethod("ValidateCASATarget")>
    Property CASATarget As String

    <DisplayLabel("Loans Target")>
    <RequiredField()>
    <ControlVariable(FieldVariables.LOANS_TARGET)>
    <CustomValidationMethod("ValidateLoansTarget")>
    Property LoansTarget As String

    Property RegionName As String

    Property RequestedBy As String
    Property DateRequested As String
    Property TempStatusID As String
    Property LogonUser As String
    Property UserStatus As String
    Property InEdit As Integer = 0
    Property Action As String
    Property listOfUserTarget As New List(Of UserTargetDO)
#End Region

    Shared Function GetList(ByVal regionCode As String, ByVal brCode As String, ByVal accessObj As ProfileDO) As List(Of UserTargetDO)
        Dim dal As New UserTargetDAL
        Dim dt As DataTable
       
        dt = dal.GetList(regionCode, brCode)

        If Not IsNothing(dt) Then
            Dim lst As New List(Of UserTargetDO)
            Dim canUpdate As Boolean = accessObj.CanUpdate = "Y"
            Dim canApprove As Boolean = accessObj.CanApprove = "Y"
            Dim canInsert As Boolean = accessObj.CanInsert = "Y"
            Dim canDelete As Boolean = accessObj.CanDelete = "Y"
            If dt.Rows.Count > 0 Then

                For Each row As DataRow In dt.Rows
                 
                    Dim access As New ProfileDO With {
                    .CanView = accessObj.CanView,
                    .CanUpdate = accessObj.CanUpdate,
                    .CanInsert = accessObj.CanInsert,
                    .CanApprove = accessObj.CanApprove
                    }


                    lst.Add(New UserTargetDO With {
                    .Access = access,
                    .Username = row.Item("UserID").ToString,
                    .Fullname = row.Item("Fullname").ToString,
                    .RoleName = row.Item("Position").ToString,
                    .Branch = row.Item("BranchName").ToString,
                    .CASATarget = row.Item("CASATarget").ToString,
                    .LoansTarget = row.Item("LoansTarget").ToString
                })
                Next
            End If

            Return lst
        End If

        Return Nothing
    End Function

    Shared Function GetTempList(ByVal regionCode As String, ByVal brCode As String, ByVal filterStatus As String) As List(Of UserTargetDO)
        Dim dal As New UserTargetDAL
        Dim dt As DataTable = dal.GetTempList(regionCode, brCode, filterStatus)

        If Not IsNothing(dt) Then
            Dim lst As New List(Of UserTargetDO)

            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    lst.Add(New UserTargetDO With {
                        .Username = row.Item("UserID").ToString,
                        .Fullname = row.Item("Fullname").ToString,
                        .RoleName = row.Item("Position").ToString,
                        .Branch = row.Item("BranchName").ToString,
                        .Status = row.Item("Status").ToString,
                        .CASATarget = row.Item("CASATarget").ToString,
                        .LoansTarget = row.Item("LoansTarget").ToString,
                        .RequestedBy = row.Item("RequestedBy").ToString,
                        .DateRequested = row.Item("DateRequested").ToString,
                        .TempStatusID = row.Item("TempStatusID").ToString
                    })
                Next
            End If

            Return lst
        End If

        Return Nothing
    End Function

    Shared Function GetDetails(ByVal username As String, ByVal action As String) As UserTargetDO
        Dim dal As New UserTargetDAL
        Dim user As New UserTargetDO With {.Action = action}
        Dim dt As DataTable = dal.GetDetails(username)

        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                With user
                    .Username = dt.Rows(0)("UserID").ToString
                    .Fullname = dt.Rows(0)("FullName").ToString
                    .GroupCode = dt.Rows(0)("GroupCode").ToString.Trim
                    .RegionName = dt.Rows(0)("RegionName").ToString
                    .BranchCode = dt.Rows(0)("BranchCode").ToString
                    .Branch = dt.Rows(0)("BranchName").ToString
                    .RoleName = dt.Rows(0)("Position").ToString
                    .Status = dt.Rows(0)("StatusID").ToString
                    .CASATarget = dt.Rows(0)("CASATarget").ToString
                    .LoansTarget = dt.Rows(0)("LoansTarget").ToString
                    .UserStatus = dt.Rows(0)("UserStatus").ToString
                    .InEdit = Convert.ToInt32(dt.Rows(0)("InEdit"))


                End With
            End If
        End If

        Return user
    End Function


    Public Function GetBHPerBrCode() As DataTable
        Dim dal As New UserTargetDAL

        Return dal.GetBHPerBrCode(GroupCode, BranchCode, Role)
    End Function

    Shared Function GetMergeDetails(ByVal Username As String) As List(Of UserTargetResult)
        Dim dal As New UserTargetDAL
        Dim dt As DataTable = dal.GetMergeDetails(Username)

        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                Dim Fields, ValueFrom, ValueTo As String
                Dim mergeDetails As New List(Of UserTargetResult)

                Fields = dt.Rows(0).Item("Fields").ToString
                ValueFrom = dt.Rows(0).Item("ValueFrom").ToString
                ValueTo = dt.Rows(0).Item("ValueTo").ToString

                Dim arrFields As String() = Fields.Split("|")
                Dim arrValueFrom As String() = If(String.IsNullOrWhiteSpace(ValueFrom), {}, ValueFrom.Split("|"))
                Dim arrValueTo As String() = If(String.IsNullOrWhiteSpace(ValueTo), {}, ValueTo.Split("|"))

                For i As Integer = 0 To arrFields.Count - 1
                    Dim userTargetResult As New UserTargetResult With {
                        .Fields = arrFields(i),
                        .ValueFrom = If(Not String.IsNullOrEmpty(ValueFrom), arrValueFrom(i), ""),
                        .ValueTo = If(Not String.IsNullOrEmpty(ValueTo), arrValueTo(i), "")
                    }

                    mergeDetails.Add(userTargetResult)
                Next

                Return mergeDetails
            End If
        End If

        Return Nothing
    End Function

    Public Function IsUserTargetExists(ByVal Username As String) As Boolean
        Dim dal As New UserTargetDAL

        isSuccess = True
        If Action = "add" Then
            If dal.IsUserTargetExists(Username) Then
                errMsg += Insertbreak() + Messages.ErrorMessages.Validation.getErrorMessage(ErrorType.EXISTS, Nothing, Nothing)
                isSuccess = False

            End If
        End If

        Return isSuccess


    End Function

    Public Function ValidateCASATarget(ByVal fieldLabel As String) As Boolean
        Dim dal As New UserTargetDAL
        Dim MinCASATarget As Integer = Integer.Parse(System.Configuration.ConfigurationManager.GetSection("Commons")("MinCASATarget"))

        isSuccess = True
        If CASATarget < MinCASATarget Then
            errMsg += Insertbreak() + Messages.ErrorMessages.Validation.getErrorMessage(ErrorType.INV_USER_TARGET, fieldLabel, Nothing) + "<br/>"
            isSuccess = False

        End If

        Return isSuccess

    End Function

    Public Function ValidateLoansTarget(ByVal fieldLabel As String) As Boolean
        Dim dal As New UserTargetDAL
        Dim MinLoansTarget As Integer = Integer.Parse(System.Configuration.ConfigurationManager.GetSection("Commons")("MinLoansTarget"))

        isSuccess = True
        If LoansTarget < MinLoansTarget Then
            errMsg += Insertbreak() + Messages.ErrorMessages.Validation.getErrorMessage(ErrorType.INV_USER_TARGET, fieldLabel, Nothing) + "<br/>"
            isSuccess = False

        End If

        Return isSuccess

    End Function

#Region "INSERT/UPDATE"
    Public Function SaveUserTarget() As Boolean
        Dim success As Boolean = True
        Dim dal As New UserTargetDAL

        Select Case Action
            Case "add"
                TempStatusID = GetSection("Commons")("ForInsert").ToString

            Case "edit"
                TempStatusID = GetSection("Commons")("ForUpdate").ToString

            Case "delete"
                TempStatusID = GetSection("Commons")("ForDelete").ToString

        End Select

        If Not dal.InsertUserTargetTemp(Username, BranchCode, GroupCode, Status, CASATarget, LoansTarget, TempStatusID, LogonUser) Then
            errMsg = dal.ErrorMsg
            success = False
        End If

        Return success
    End Function
#End Region


#Region "APPROVE"
    Public Function ApproveUser() As Boolean
        Dim dal As New UserTargetDAL
        Dim success As Boolean = True
        
        If Not String.IsNullOrWhiteSpace(TempStatusID) Then
            Select Case TempStatusID

                Case GetSection("Commons")("ForInsert").ToString
                  
                    Dim dt As New DataTable
                    'dt = GetRoleDetails()
                    If Not dal.InsertUserTarget(Username, LogonUser, False) Then
                        errMsg = dal.ErrorMsg
                        Return False
                    End If

                    success = True

                Case GetSection("Commons")("ForUpdate").ToString
                    If Not dal.UpdateUserTarget(Username, LogonUser, False) Then
                        errMsg = dal.ErrorMsg
                        Return False
                    End If

            End Select

            If Not dal.DeleteUserTargetTemp(Username, LogonUser, False) Then
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
    Public Function RejectUserTarget() As Boolean
        Dim dal As New UserTargetDAL
        Dim success As Boolean = True

        If Not dal.DeleteUserTargetTemp(Username, LogonUser) Then
            errMsg = dal.ErrorMsg
            Return False
        End If

        Return success
    End Function
#End Region

End Class
