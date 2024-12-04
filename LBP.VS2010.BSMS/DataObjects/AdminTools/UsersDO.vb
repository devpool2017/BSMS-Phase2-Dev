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
Imports LBP.VS2010.WebCryptor

Public Class UsersDO
    Inherits MasterDO
    Dim _webConfig As New WebConfigCryptor

    Partial Class UserResult
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
    <DisplayLabel("User ID")>
    <RequiredField()>
    <ControlVariable(FieldVariables.USERNAME)>
    <MaxLengthCheck()>
    Property Username As String

    <DisplayLabel("Role")>
    <RequiredField(RequiredField.ControlType.SELECTION)>
    Property RoleId As String

    Property Name As String
    Property EmailAddress As String

    Property DisplayName As String

    <DisplayLabel("First Name")>
    <RequiredField()>
    <ControlVariable(FieldVariables.FIRST_NAME)>
    <MaxLengthCheck()>
    Property FirstName As String

    <DisplayLabel("Last Name")>
    <RequiredField()>
    <ControlVariable(FieldVariables.LAST_NAME)>
    <MaxLengthCheck()>
    Property LastName As String

    <DisplayLabel("Middle Initial")>
    <ControlVariable(FieldVariables.MIDDLE_INITIAL)>
    <MaxLengthCheck()>
    Property MiddleInitial As String

    '<CustomValidationMethod("ValidateIPAddress")>
    <DisplayLabel("IP Address")>
    <ControlVariable(FieldVariables.IP)>
    <MaxLengthCheck()>
    Property IPAddress As String

    <DisplayLabel("Status")>
    <RequiredField(RequiredField.ControlType.SELECTION)>
    Property Status As String


    Property RoleName As String

    <DisplayLabel("Branch")>
    <ControlVariable(FieldVariables.BRANCH)>
    <RequiredConditional.ValueEquals("RoleId", {"8"}, RequiredField.ControlType.SELECTION)>
    Property BranchCode As String

    <DisplayLabel("Group")>
    <ControlVariable(FieldVariables.GROUP)>
    <RequiredConditional.ValueEquals("RoleId", {"8", "6", "9"}, RequiredField.ControlType.SELECTION)>
    Property RegionCode As String


    '<DisplayLabel("CASA Target")>
    '<RequiredField()>
    '<ControlVariable(FieldVariables.CASA_TARGET)>
    'Property CASATarget As String

    '<DisplayLabel("Loans Target")>
    '<RequiredField()>
    '<ControlVariable(FieldVariables.LOANS_TARGET)>
    'Property CBGTarget As String


    Property GroupCode As String
    Property IsActive As String
    Property InUse As String
    Property Browser As String
    Property IsLocked As String
    Property LoginAttempt As String
    Property DateLastLogin As String
    Property StatusId As String
    Property DateApproved As String
    Property ApprovedBy As String
    Property DateCreated As String
    Property CreatedBy As String
    Property DateModified As String
    Property ModifiedBy As String
    Property IsUsersEdit As Boolean
    Property Branch As String


    Property RegionName As String
    Property curUser As String
    Property listOfUsers As New List(Of UsersDO)
#End Region

#Region "LOGIN"
    Private Sub ValidateUserId()
        IsTextEmpty(Username, "User ID")
    End Sub

    Private Sub ValidatePassword()
        IsTextEmpty(Password, "Password")
    End Sub

    Public Function IsSystemUser(ByVal Username As String) As Boolean
        Dim dal As New UsersDAL

        isSuccess = True

        If Not dal.IsUserExists(Username) Then
            objErrorMessage += Insertbreak() + Messages.ErrorMessages.Validation.getErrorMessage(ErrorType.STANDARD_LOGIN_ERROR, "", Nothing)
            isSuccess = False

        End If

        Return isSuccess


    End Function


    Public Function IsUserActive(ByVal IsActive As Boolean) As Boolean
        isSuccess = True

        If Not IsActive.Equals(True) Then
            objErrorMessage += Insertbreak() + Messages.ErrorMessages.Validation.getErrorMessage(ErrorType.NOT_ACTIVE_USER, "", Nothing)
            isSuccess = False
        End If

        Return isSuccess
    End Function

    Public Function IsUserNotLocked(ByVal IsLocked As Boolean) As Boolean
        isSuccess = True

        If IsLocked.Equals(True) Then
            objErrorMessage += Insertbreak() + Messages.ErrorMessages.Validation.getErrorMessage(ErrorType.IS_LOCKED, "", Nothing)
            isSuccess = False
        End If

        Return isSuccess
    End Function


    Public Function IsUserWithBranch() As Boolean
        isSuccess = True

        If RoleId = GetSection("Commons")("BM").ToString Then
            If IsNothing(Branch) Or IsNothing(RegionCode) Then

                objErrorMessage += Insertbreak() + Messages.ErrorMessages.Validation.getErrorMessage(ErrorType.BRANCH_LOGGED_IN, "", Nothing)
                isSuccess = False

            End If
        End If


        Return isSuccess
    End Function

    Public Function IsUserNotInUse(ByVal currIPAddress As String, ByVal InUse As Boolean) As Boolean
        isSuccess = True
        If ((Not (currIPAddress = IPAddress)) And (InUse = True)) Then
            objErrorMessage = Messages.ErrorMessages.Validation.getErrorMessage(ErrorType.ALREADY_LOGGED_IN, Username, Nothing)
            isSuccess = False

        End If

        Return isSuccess
    End Function

    Public Function ProcessLogin(ByVal currIPAddress As String, ByVal Username As String) As Boolean
        Dim dal As New UsersDAL

        isSuccess = True

        If Not (dal.LoginUser(Username, currIPAddress)) Then

            objErrorMessage = String.Empty
            objErrorMessage += DataBase.getErrorMessage(PageControlVariables.PageFunctions.SAVE, dal.ErrorMsg)
            isSuccess = False
        End If

        Return isSuccess
    End Function

    Public Sub IncrementLoginAttempt(ByVal maxLoginAttempt As Integer)
        Dim dal As New UsersDAL

        Dim loginAttempt As String = dal.IncrementLoginAttempt(Username, maxLoginAttempt)

        If Not String.IsNullOrWhiteSpace(loginAttempt) Then

            If CInt(loginAttempt) = maxLoginAttempt Then
                objErrorMessage = String.Empty
                objErrorMessage += Validation.getErrorMessage(ErrorType.IS_LOCKED, "", Nothing)
            End If

        End If

    End Sub


    Public Function LogoutUser(ByVal action As String)  'Example action: "User Logout"/"User Reset"
        Dim dal As New UsersDAL

        isSuccess = True

        If Not (dal.LogoutUser(Username)) Then

            objErrorMessage = String.Empty
            objErrorMessage += DataBase.getErrorMessage(PageControlVariables.PageFunctions.UNLOCK, dal.ErrorMsg)
            isSuccess = False
        End If

        Return isSuccess
    End Function

#End Region

#Region "MISC PROPRETIES"
    Property Password As String
    Property PasswordAttempt As Integer = 0
    Property TempStatusID As String
    Property InEdit As Integer = 0
    Property IsAPI As Integer = Convert.ToInt32(GetSection("Commons")("isAPIValidation"))
    Property RequestedBy As String
    Property DateRequested As String
    Property Action As String
    Property LogonUser As String
#End Region


#Region "FUNCTIONS"
#Region "DROPDOWN"


    Shared Function GetRoleList() As DataTable
        Dim dal As New RoleDAL

        Return dal.GetUserRoleList(String.Empty)
    End Function

    Shared Function GetRegionList() As DataTable
        Dim dal As New UsersDAL

        Return dal.RegionGetList()
    End Function

    Shared Function GetBranchList() As DataTable
        Dim dal As New UsersDAL

        Return dal.BranchGetList()
    End Function

#End Region

#Region "VALIDATION"

    Public Function ValidateLogin() As Boolean
        Dim success As Boolean = True
        ValidateUserId()
        ValidatePassword()

        Return ValidationResult()
    End Function

    Function IsUserIDExist() As Boolean
        Dim success As Boolean = True
        Dim dal As New UsersDAL

        If String.Equals(Action, "add") Then
            If dal.IsUserIDExists(Username) Then
                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.DUPLICATE, Username, {Username}) + "<br/>"
                success = False
            End If
        End If

        Return success
    End Function

    Function IsUserExist(ByVal fieldLabel As String) As Boolean
        Dim success As Boolean = True
        Dim dal As New UsersDAL

        If String.Equals(Action, "add") Then
            If dal.IsUserExists(Username) Then
                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.DUPLICATE, fieldLabel, {Username}) + "<br/>"
                success = False
            End If
        End If

        Return success
    End Function

    Public Function ValidateIPAddress(ByVal fieldLabel As String) As Boolean
        Dim success As Boolean = True

        If Not String.IsNullOrWhiteSpace(IPAddress) Then
            If Not Regex.Match(IPAddress, getRegexValidator(REGEX_VALIDATOR.IPADDRESS)).Success Then
                success = False
                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.INVALID_DATA, fieldLabel, {}) + "<br/>"
            End If
        End If

        Return success
    End Function
#End Region

#Region "LIST/GET"

    Public Function GetLogonDetails(ByVal username As String) As Boolean
        Dim dal As New UsersDAL
        Dim dt As New DataTable

        isSuccess = True

        If Not String.IsNullOrWhiteSpace(username) Then
            username = username
        End If

        Try
            dt = dal.GetLogonDetails(username)

            If Not IsNothing(dt) Then
                If dt.Rows.Count > 0 Then
                    DisplayName = dt.Rows(0).Item("DisplayName").ToString.Trim
                    username = dt.Rows(0).Item("UserName").ToString.Trim.ToUpper
                    RoleId = dt.Rows(0).Item("RoleId").ToString.Trim
                    RoleName = dt.Rows(0).Item("RoleName").ToString.Trim
                    IsActive = dt.Rows(0).Item("IsActive").ToString.Trim
                    InUse = dt.Rows(0).Item("IsInUse").ToString.Trim
                    IPAddress = dt.Rows(0).Item("IPAddress").ToString.Trim
                    IsLocked = dt.Rows(0).Item("IsLocked").ToString.Trim
                    LoginAttempt = dt.Rows(0).Item("PasswordAttempt").ToString.Trim
                    Branch = dt.Rows(0).Item("Branch").ToString.Trim
                    RegionCode = dt.Rows(0).Item("RegionCode").ToString.Trim
                    GroupCode = dt.Rows(0).Item("RegionCode").ToString.Trim
                    RegionName = dt.Rows(0).Item("RegionName").ToString.Trim
                    BranchCode = dt.Rows(0).Item("BrCode").ToString.Trim
                End If
            End If

        Catch ex As Exception
            objErrorMessage = ex.InnerException.Message.ToString
            Logger.writeLog("Login", "Get Logon Details", "", objErrorMessage)
            isSuccess = False


        End Try

        Return isSuccess
    End Function

    Shared Function GetList(ByVal searchText As String, ByVal filterStatus As String, ByVal filterRole As String, ByVal filterRegion As String, ByVal filterBranch As String, ByVal accessObj As ProfileDO) As List(Of UsersDO)
        Dim dal As New UsersDAL
        Dim dt As DataTable
        Dim list As New List(Of SummaryBMDO)

        dt = dal.GetList(searchText, filterStatus, filterRole, filterRegion, filterBranch)

        If Not IsNothing(dt) Then
            Dim lst As New List(Of UsersDO)
            Dim canUpdate As Boolean = accessObj.CanUpdate = "Y"
            Dim canApprove As Boolean = accessObj.CanApprove = "Y"
            Dim canInsert As Boolean = accessObj.CanInsert = "Y"
            Dim canDelete As Boolean = accessObj.CanDelete = "Y"
            Dim canPrint As Boolean = accessObj.CanPrint = "Y"
            Dim canView As Boolean = accessObj.CanView = "Y"
            Dim canUnlock As Boolean = accessObj.CanUnlock = "Y"
            If dt.Rows.Count > 0 Then

                For Each row As DataRow In dt.Rows
                    Dim unlock As String = "N"
                    If (row.Item("Islocked") = True) Then
                        unlock = "Y"
                    End If

                    Dim access As New ProfileDO With {
                    .CanView = accessObj.CanView,
                    .CanUpdate = accessObj.CanUpdate,
                    .CanInsert = accessObj.CanInsert,
                    .CanApprove = accessObj.CanApprove,
                    .CanUnlock = unlock
                       }


                    lst.Add(New UsersDO With {
                    .Access = access,
                    .Username = row.Item("Username").ToString,
                    .Name = row.Item("Name").ToString,
                    .RoleId = row.Item("RoleID").ToString,
                    .RoleName = row.Item("Role").ToString,
                    .Status = row.Item("Status").ToString
                })
                Next
            End If

            Return lst
        End If

        Return Nothing
    End Function

    Shared Function GetRoleStatus(roleId As String, Optional accessObj As ProfileDO = Nothing) As DataTable
        Dim dal As New RoleDAL
        Dim dalProfile As New ProfileDAL
        Dim successMsg As String = ""
        Dim dt As New DataTable

        dt = dal.GetRoleStatus(roleId)

        Return dt
    End Function

    Shared Function GetUserRole(username As String) As DataTable
        Dim dal As New UsersDAL

        Dim dt As New DataTable

        dt = dal.GetUserRole(username)

        Return dt
    End Function

    Shared Function GetTempList(ByVal searchText As String, ByVal filterStatus As String) As List(Of UsersDO)
        Dim dal As New UsersDAL
        Dim dt As DataTable = dal.GetTempList(searchText, filterStatus)

        If Not IsNothing(dt) Then
            Dim lst As New List(Of UsersDO)

            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    lst.Add(New UsersDO With {
                        .Username = row.Item("Username").ToString,
                        .Name = row.Item("Name").ToString,
                        .RequestedBy = row.Item("RequestedBy").ToString,
                        .DateRequested = row.Item("DateRequested").ToString,
                        .TempStatusID = row.Item("TempStatusID").ToString,
                        .Status = row.Item("Status").ToString,
                        .FirstName = row.Item("FirstName").ToString
                    })
                Next
            End If

            Return lst
        End If

        Return Nothing
    End Function

    Shared Function GetDetails(ByVal username As String, ByVal action As String) As UsersDO
        Dim dal As New UsersDAL
        Dim user As New UsersDO With {.Action = action}
        Dim dt As DataTable = dal.GetDetails(username)

        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                With user
                    .Username = dt.Rows(0)("Username").ToString
                    .RoleId = dt.Rows(0)("RoleID").ToString
                    .FirstName = dt.Rows(0)("FirstName").ToString
                    .LastName = dt.Rows(0)("LastName").ToString
                    .MiddleInitial = dt.Rows(0)("MiddleInitial").ToString
                    .Name = dt.Rows(0)("DisplayName").ToString
                    .EmailAddress = dt.Rows(0)("Email").ToString
                    .IPAddress = dt.Rows(0)("IPAddress").ToString
                    .Status = dt.Rows(0)("StatusID").ToString
                    .PasswordAttempt = Convert.ToInt32(dt.Rows(0)("PasswordAttempt"))
                    .InEdit = Convert.ToInt32(dt.Rows(0)("InEdit"))
                    .BranchCode = dt.Rows(0)("BrCode").ToString
                    .RegionCode = dt.Rows(0)("RegionCode").ToString
                    '.CASATarget = dt.Rows(0)("CASATarget").ToString
                    '.CBGTarget = dt.Rows(0)("CBGTarget").ToString
                    .InEdit = dt.Rows(0)("InEdit").ToString
                End With
            End If
        End If

        Return user
    End Function

    Shared Function GetMergeDetails(ByVal Username As String) As List(Of UserResult)
        Dim dal As New UsersDAL
        Dim dt As DataTable = dal.GetMergeDetails(Username)

        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                Dim Fields, ValueFrom, ValueTo As String
                Dim mergeDetails As New List(Of UserResult)

                Fields = dt.Rows(0).Item("Fields").ToString
                ValueFrom = dt.Rows(0).Item("ValueFrom").ToString
                ValueTo = dt.Rows(0).Item("ValueTo").ToString

                Dim arrFields As String() = Fields.Split("|")
                Dim arrValueFrom As String() = If(String.IsNullOrWhiteSpace(ValueFrom), {}, ValueFrom.Split("|"))
                Dim arrValueTo As String() = If(String.IsNullOrWhiteSpace(ValueTo), {}, ValueTo.Split("|"))

                For i As Integer = 0 To arrFields.Count - 1
                    Dim userResult As New UserResult With {
                        .Fields = arrFields(i),
                        .ValueFrom = If(Not String.IsNullOrEmpty(ValueFrom), arrValueFrom(i), ""),
                        .ValueTo = If(Not String.IsNullOrEmpty(ValueTo), arrValueTo(i), "")
                    }

                    mergeDetails.Add(userResult)
                Next

                Return mergeDetails
            End If
        End If

        Return Nothing
    End Function

    Public Function GetAPIUserInfo(ByVal reqVerifyUserIDParams As VerifyUserIDParams, ByVal requestedUsername As String, ByVal apiParams As VerifyUserIDParams) As UsersDO

        Dim userdo As New UsersDO
        Dim ldap As New LDAPAuthentication
        ldap.UserName = reqVerifyUserIDParams.Username
        If CBool(_webConfig.Decrypt("ADCheck", True)) Then

            If userdo.ValidateADUser(ldap.UserName) Then

                With userdo
                    .Action = "add"
                    .Username = ldap.UserName
                    .FirstName = userdo.FirstName
                    .LastName = userdo.LastName
                    .Name = userdo.Name
                    .MiddleInitial = userdo.MiddleInitial
                    .EmailAddress = userdo.EmailAddress
                    .IPAddress = apiParams.IPAddress
                    .Browser = apiParams.Browser

                End With
            Else
                userdo.errMsg = ldap.ErrMsg
            End If
        Else
            With userdo
                .Action = "add"
                .Username = requestedUsername
                .FirstName = "TestFname"
                .LastName = "TestLname"
                .Name = "TestFname TestLname"
                .MiddleInitial = "P"
                .EmailAddress = GetSection("Commons")("TestEmailAdd").ToString
                .IPAddress = apiParams.IPAddress
                .Browser = apiParams.Browser
            End With
        End If


        Return userdo
    End Function


    Public Function ValidateADUser(ByVal userName As String) As Boolean
        Dim isValid As Boolean = False

        Dim ADLogin As New LDAPAuthentication

        ADLogin.UserName = userName

        ADLogin.Domain = _webConfig.Decrypt("LDAPSource", True).ToString
        'ADLogin.GetName()

        If ADLogin.GetName(userName) Then
            userName = ADLogin.UserName
            Name = ADLogin.DisplayName
            LastName = ADLogin.LastName
            FirstName = ADLogin.FirstName
            MiddleInitial = ADLogin.MiddleName
            EmailAddress = If(String.IsNullOrWhiteSpace(ADLogin.EmailAddress), EmailAddress, ADLogin.EmailAddress)

            isValid = True
        Else
            errMsg = ADLogin.ErrMsg
        End If

        Return isValid

    End Function

    Public Function IsUserExists() As Boolean
        Dim dal As New UsersDAL
        Dim isExists As Boolean = False

        Try
            If dal.IsUserExists(Username) And Action.Equals("add") Then
                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(ErrorType.MULTIPLE_LOGIN, "Username", {Username}) + "<br/>"
                isExists = True
            End If
        Catch ex As Exception

        End Try

        Return isExists
    End Function
#End Region

#Region "INSERT/UPDATE"
    Public Function SaveUser() As Boolean
        Dim success As Boolean = True
        Dim dal As New UsersDAL

        Select Case Action
            Case "add"
                TempStatusID = GetSection("Commons")("ForInsert").ToString

            Case "edit"
                TempStatusID = GetSection("Commons")("ForUpdate").ToString

            Case "delete"
                TempStatusID = GetSection("Commons")("ForDelete").ToString

            Case "unlock"
                TempStatusID = GetSection("Commons")("ForUnlock").ToString

            Case "reset"
                TempStatusID = GetSection("Commons")("ForReset").ToString
        End Select

        If Not dal.InsertUserTemp(Username, RoleId, FirstName, LastName, MiddleInitial, Name, BranchCode, RegionCode, IPAddress, EmailAddress, Status,
                             TempStatusID, LogonUser) Then
            errMsg = dal.ErrorMsg
            success = False
        End If

        Return success
    End Function
#End Region

#Region "UNLOCK"
    Public Function UnlockUser() As Boolean


        Dim success As Boolean = True
        Dim dal As New UsersDAL

        If Not dal.UnlockUser(Username, LogonUser) Then
            errMsg = dal.ErrorMsg
            success = False
        End If

        Return success
    End Function
#End Region

#Region "APPROVE"
    Public Function ApproveUser() As Boolean
        Dim dal As New UsersDAL
        Dim success As Boolean = True
        Dim addressList As New List(Of String)

        If Not String.IsNullOrWhiteSpace(TempStatusID) Then
            Select Case TempStatusID

                Case GetSection("Commons")("ForInsert").ToString
                    addressList.Add(dal.GetEmail(Username, "UsersTemp", False))

                    'Dim PasswordResult As List(Of String) = PasswordGenerator.GeneratePassword()

                    'If Not CBool(PasswordResult(0)) Then
                    '    errMsg = PasswordResult(1)
                    '    Return False
                    'End If

                    'Password = PasswordResult(1)
                    Dim dt As New DataTable
                    'dt = GetRoleDetails()
                    If Not dal.InsertUser(Username, LogonUser, False) Then
                        errMsg = dal.ErrorMsg
                        Return False
                    End If

                    success = True

                Case GetSection("Commons")("ForUpdate").ToString
                    If Not dal.UpdateUser(Username, LogonUser, False) Then
                        errMsg = dal.ErrorMsg
                        Return False
                    End If

                Case GetSection("Commons")("ForDelete").ToString
                    If Not dal.DeleteUser(Username, LogonUser, False) Then
                        errMsg = dal.ErrorMsg
                        Return False
                    End If

                Case GetSection("Commons")("ForReset").ToString
                    addressList.Add(dal.GetEmail(Username, "UsersTemp", False))

                    Dim PasswordResult As List(Of String) = PasswordGenerator.GeneratePassword()
                    Password = PasswordResult(1)
                    If CBool(PasswordResult(0)) Then
                        If Not dal.ResetUser(Username, Encrypt(Password), LogonUser, False) Then
                            errMsg = dal.ErrorMsg
                            Return False
                        End If
                    Else
                        errMsg = PasswordResult(1)
                        Return False
                    End If

                    'success = SendEmail(addressList)

                Case GetSection("Commons")("ForUnlock").ToString
                    addressList.Add(dal.GetEmail(Username, "Users", False))

                    If Not dal.UnlockUser(Username, LogonUser, False) Then
                        errMsg = dal.ErrorMsg
                        Return False
                    End If

                    ' success = SendEmail(addressList)
            End Select

            If Not dal.DeleteUserTemp(Username, LogonUser, False) Then
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
    Public Function RejectUser() As Boolean
        Dim dal As New UsersDAL
        Dim success As Boolean = True

        If Not dal.DeleteUserTemp(Username, LogonUser) Then
            errMsg = dal.ErrorMsg
            Return False
        End If

        'AddressList.Add(dal.GetEmail(LogonUser, "Users"))

        'If SendEmail("Reject") Then
        '    dal.CommitTrans()
        'End If

        Return success
    End Function
#End Region

    Public Function GetUsersListReport(ByVal username As String, ByVal statusID As String, ByVal roleID As String, ByVal regionCode As String, ByVal branchCode As String) As DataTable
        Dim dal As New UsersDAL

        Return dal.GetUsersListReport(username, statusID, roleID, regionCode, branchCode)
    End Function

    Shared Function GetBranchListPerRegion(ByVal regionCode As String) As DataTable
        Dim dal As New UsersDAL

        Return dal.BranchGetListPerRegion(regionCode)
    End Function

    Shared Function GetBranchListPerRegion() As DataTable
        Dim dal As New UsersDAL

        Return dal.BranchGetList()
    End Function
#End Region
End Class
