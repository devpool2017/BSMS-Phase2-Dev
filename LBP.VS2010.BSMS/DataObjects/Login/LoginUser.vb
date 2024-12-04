Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.CustomValidators.LengthAndValueValidator
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.CustomValidators
Imports LBP.VS2010.BSMS.DataAccess
Imports System.Configuration.ConfigurationManager
Imports Newtonsoft.Json
Imports System.Net
Imports System.IO

Public Class LoginUser
    Inherits MasterDO

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

#Region "DECLARATION"
#Region "DATA COLUMNS"
    <DataColumnMapping("Username")>
    <DisplayLabel("Username")>
    <ControlVariable(FieldVariables.USERNAME)>
    <RequiredField()>
    <MaxLengthCheck()>
    Property Username As String

    <DisplayLabel("Password")>
    <RequiredField()>
    Property Password As String

    <DataColumnMapping("RoleID")>
    Property RoleID As String

    <DataColumnMapping("DisplayName")>
    Property DisplayName As String

    <DataColumnMapping("StatusID")>
    Property Status As String

    <DataColumnMapping("InitialLogin")>
    Property InitialLogin As String

    <DataColumnMapping("PasswordAttempt")>
    Property PasswordAttempt As String

    <DataColumnMapping("DatePasswordExpiry")>
    Property DatePasswordExpiry As String

    Property HashPassword As String

    Property StatusID As String
    Property isLoginSuccess As Boolean
    Property SystemName As String
    Property AuthToken As String
    Property UnitCode As String
    Property GroupCode As String
    Property GroupMembership As List(Of GrpMembership)
    Property StatusAPI As String
    Property InitialLoginAPI As String
    Property UnitCodeAPI As String
    Property GroupCodeAPI As String
    Property DatePasswordExpiryAPI As String
    Property Branch As String
    Property RegionName As String
    Property BranchCode As String
#End Region

#Region "MISC COLUMNS"
    <DataColumnMapping("RoleName")>
    Property RoleName As String
#End Region

#Region "MISC PROPERTIES"
    Property currentUser As LoginUser
    Property IPAddress As String
    Property Browser As String
    Property Workstation As String

    <DisplayLabel("New Password")>
    <ControlVariable(FieldVariables.PASSWORD)>
    <RequiredField()>
    <DataType.TypePassword()>
    <MaxLengthCheck()>
    <MinLengthCheck()>
    <CustomValidationMethod("compareOldAndNewPasswords")>
    Property NewPassword As String

    <DisplayLabel("Confirm Password")>
    <ControlVariable(FieldVariables.PASSWORD)>
    <RequiredField()>
    <MaxLengthCheck()>
    <MinLengthCheck()>
    <CustomValidationMethod("compareNewPasswords")>
    Property ConfirmPassword As String

    Property IsPasswordExpired As Boolean = False
    Property IsForChangePassword As Boolean = False
    Property IsChangePassRedirectFromLogin As Boolean = False
    Property PasswordHandler As String
    Property isNewPasswordinHistory As Boolean

    Partial Class GrpMembership
        Property GroupCode As String
        Property Description As String
        Property FullDescription As String
        Property ParentGroupCode As String
    End Class
#End Region
#End Region

#Region "FUNCTIONS"
#Region "PASSWORD VALIDATION"
    Public Function validateChangePassword() As Boolean
        Dim isSuccess As Boolean = True
        Dim lst As New List(Of String) From {
            "Username",
            "Password",
            "NewPassword",
            "ConfirmPassword"
        }

        If validateProperties(lst) Then
            Dim dal As New UsersDAL
            Dim dt As DataTable = dal.GetLoginDetails(Username)

            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                RoleID = dt.Rows(0)("RoleID")
                DisplayName = dt.Rows(0)("DisplayName")
                RoleName = dt.Rows(0)("RoleName")
                Status = dt.Rows(0)("StatusID")
                InitialLogin = dt.Rows(0)("InitialLogin")
                DatePasswordExpiry = dt.Rows(0)("DatePasswordExpiry")
                HashPassword = dt.Rows(0)("Password")
                PasswordAttempt = Integer.Parse(dt.Rows(0)("PasswordAttempt"))

                Dim ApprovedBy As String = dt.Rows(0)("ApprovedBy").ToString
                Dim DateApproved As String = dt.Rows(0)("DateApproved").ToString

                If Password = Decrypt(HashPassword) Then
                    If Not VerifyApproval(ApprovedBy, DateApproved) Then Return False

                    If Status = "1" Then
                        If Not (NewPassword.ToLower).Contains(Username.ToLower) Then
                            isSuccess = Not CheckIfPasswordExistInHistory()
                        Else
                            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.STANDARD_CHANGE_PASSWORD_ERROR, Nothing, Nothing)
                            isSuccess = False
                        End If
                    Else
                        isSuccess = CheckLoginCredentials()
                    End If
                Else
                    incrementLoginAttempt()

                    isSuccess = False
                End If
            Else
                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.STANDARD_LOGIN_ERROR, Nothing, Nothing)
                isSuccess = False
            End If
        Else
            isSuccess = False
        End If

        Return isSuccess
    End Function

    Public Function CheckIfPasswordExistInHistory() As Boolean
        Dim dalPassword As New UsersDAL
        Dim dtPasswordList As DataTable = dalPassword.GetPasswordHistory(Username)
        Dim str As String = String.Empty
        If dtPasswordList.Rows.Count > 0 Then
            For Each item In dtPasswordList.Rows
                If NewPassword = Decrypt(item("Password")) Then
                    errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.PASSWORD_STILL_IN_USE, Nothing, Nothing)
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Public Function compareOldAndNewPasswords(ByVal fieldLabel As String) As Boolean
        If Password = NewPassword Then
            errMsg = errMsg + Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.SAME_VALUES_NOT_ALLOWED, fieldLabel, {"Current Password"}) + "<br/>"
            Return False
        End If

        Return True
    End Function

    Public Function compareNewPasswords(ByVal fieldLabel As String) As Boolean
        If Not NewPassword = ConfirmPassword Then
            errMsg = errMsg + Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.DOES_NOT_MATCH, fieldLabel, {"New Password"}) + "<br/>"
            Return False
        End If

        'Dim dalPassword As New UsersDAL
        'Dim dtPasswordList As DataTable = dalPassword.GetPasswordHistory(Username)

        'If dtPasswordList.Rows.Count > 0 Then
        '    For Each item In dtPasswordList.Rows
        '        If NewPassword = Decrypt(item("Password")) Then
        '            errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.PASSWORD_STILL_IN_USE, Nothing, Nothing)
        '            Return False
        '        End If
        '    Next
        'End If

        Return True
    End Function
#End Region

#Region "LOGIN VALIDATION"
    Public Function validateLoginUser(ByVal userobj As UsersDO, ByVal currIPAddress As String) As Boolean
        Dim isSuccess = False

        If userobj.IsSystemUser(userobj.Username) Then
            'Get User Details
            If userobj.GetLogonDetails(userobj.Username) Then

                'Validate if userRole BM has branch in users table
                If userobj.IsUserWithBranch() Then

                    ''If userobj.IsUserActive(userobj.IsActive) Then

                    'Validate if User is Locked
                    If userobj.IsUserNotLocked(userobj.IsLocked) Then
                        'Validate if User is InUse adn try to login with same IP Add
                        isSuccess = True
                        'If userobj.IsUserNotInUse(currIPAddress, userobj.InUse) Then
                        '    userobj.LogoutUser("User Reset")

                        '    isSuccess = True
                        'End If
                    End If
                    ''End If
                Else
                    isSuccess = False
                End If
           
            End If
        End If


        Return isSuccess

    End Function


#End Region

#Region "LOGIN ATTEMPT"
    Private Sub incrementLoginAttempt()
        Dim LoginAttempt As Integer = Integer.Parse(PasswordAttempt)
        Dim maxAttempt As Integer = GetSection("Commons")("LoginCounter")

        If Not CBool(GetSection("Commons")("isAPIValidation")) Then
            Dim dal As New UsersDAL
            LoginAttempt += 1

            If Not dal.updateLoginAttempt(Username, LoginAttempt, maxAttempt) Then
                errMsg = dal.ErrorMsg
                Exit Sub
            End If
        End If

        If LoginAttempt >= maxAttempt Then
            errMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.IS_LOCKED, Nothing, Nothing)
        Else
            errMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.BAD_PASS_COUNTER, Nothing, {maxAttempt - LoginAttempt})
        End If
    End Sub
#End Region

#Region "CHECK LOGIN CREDENTIALS"
    Private Function VerifyAvailability(ByVal InUse As String, ByVal UserIP As String, ByVal DateLastActivity As String) As Boolean
        If InUse = "1" And Not IPAddress = UserIP Then
            If Not String.IsNullOrWhiteSpace(DateLastActivity) Then
                Dim sessionTimerAgo As Date = Date.Now.AddMinutes(-(CInt(GetSection("Commons")("SessionTimer"))))
                Dim lastActivity As Date = Convert.ToDateTime(DateLastActivity)

                If lastActivity > sessionTimerAgo Then
                    errMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.ALREADY_LOGGED_IN, Username, Nothing)
                    Return False
                End If
            End If
        End If

        Return True
    End Function

    Private Function VerifyApproval(ByVal ApprovedBy As String, ByVal DateApproved As String) As Boolean
        If String.IsNullOrWhiteSpace(ApprovedBy) Or String.IsNullOrWhiteSpace(DateApproved) Then
            errMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.STANDARD_LOGIN_ERROR, Nothing, Nothing)
            Return False
        End If

        Return True
    End Function

    Private Function CheckLoginCredentials() As Boolean
        Select Case Status
            Case "1"
                If InitialLogin = "1" Then ' Redirect to change pass
                    IsForChangePassword = True
                End If

            Case "2"
                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.DEACTIVATED, Nothing, Nothing)
                Return False

            Case "3"
                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.IS_LOCKED, Nothing, Nothing)
                Return False

            Case "4"
                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.STANDARD_LOGIN_ERROR, Nothing, Nothing)
                Return False
        End Select

        Return True
    End Function

    Private Function CheckIfPasswordExpired() As Boolean
        If CBool(GetSection("Commons")("isAPIValidation")) Then
            If Date.Now > Date.ParseExact(DatePasswordExpiryAPI, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture) Then
                IsPasswordExpired = True
            End If

        Else
            If Date.Now > Date.ParseExact(DatePasswordExpiry, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture) Then
                If InitialLogin = "1" Then
                    'Update status to disabled
                    errMsg = "Expired Temp Password."
                    Return False
                End If

                IsPasswordExpired = True
            End If
        End If

        Return True
    End Function
#End Region

#Region "PASSWORD CHANGE"
    Public Function ValidatePasswordStr() As Boolean
        If (New Text.RegularExpressions.Regex("[a-z]")).IsMatch(Password) Then
            Return False
        End If

        If (New Text.RegularExpressions.Regex("[A-Z]")).IsMatch(Password) Then
            Return False
        End If

        If (New Text.RegularExpressions.Regex("[0-9]")).IsMatch(Password) Then
            Return False
        End If

        If (New Text.RegularExpressions.Regex(GetSection("PageControlVariables")("RegexPWSpecialChars"))).IsMatch(Password) Then
            Return False
        End If

        If NewPassword.Length < CInt(GetSection("PageControlVariables")("PasswordMinLen")) Then
            Return False
        End If

        If (NewPassword.ToLower).Contains(Username.ToLower) Then
            Return False
        End If

        If Not (New Text.RegularExpressions.Regex("^[0-9\s,]*$")).IsMatch(Password) Then
            Return False
        End If

        If NewPassword <> ConfirmPassword Then
            Return False
        End If

        Dim dalPassword As New UsersDAL
        Dim dtPasswordList As DataTable = dalPassword.GetPasswordHistory(Username)
        If dtPasswordList.Rows.Count > 0 Then
            For Each item In dtPasswordList.Rows
                If NewPassword = Decrypt(item("Password")) Then
                    Return False
                End If
            Next
        End If

        Return True
    End Function

#End Region

#Region "LOGOUT"
    Public Function logout() As Boolean
        Dim success As Boolean
        Dim dal As New UsersDAL

        If dal.updateLogout(Username) Then
            success = True
        Else
            errMsg = dal.ErrorMsg

            success = False
        End If

        Return success
    End Function
#End Region

#Region "API FUNCTIONS"
#Region "API - LOGIN"
    Public Function API_GetUserDetails()
        Dim reqUserValidationParams As New UserValidationParams
        reqUserValidationParams.Username = Username
        reqUserValidationParams.Password = Password
        reqUserValidationParams.PasswordHandler = PasswordHandler
        reqUserValidationParams.SystemName = SystemName
        reqUserValidationParams.Browser = Browser
        reqUserValidationParams.IPAddress = IPAddress

        Dim reqGatewayParams As New ApiGatewayParams
        reqGatewayParams.Salt = GetSection("Commons")("SysSalt")
        reqGatewayParams.ServiceID = GetSection("Commons")("ValidateUser")

        Dim data As String = APIEncrypt(JsonConvert.SerializeObject(reqUserValidationParams), GetSection("Commons")("GenSysTkn"))
        reqGatewayParams.Data = data

        Return API.APICallGateway(JsonConvert.SerializeObject(reqGatewayParams), True, AuthToken)
    End Function

    Public Function APIValidateLogin() As Boolean
        Try
            Dim lst As New List(Of String)
            lst.Add("Username")
            lst.Add("Password")

            If Not validateProperties(lst) Then Return False

            Dim APIResponse As HttpWebResponse
            Dim rdr As StreamReader
            Dim dt As New DataTable
            Dim dal As New UsersDAL
            Dim APIdata As LoginUser = Nothing
            Dim reqVerifyUserIDParams As New VerifyUserIDParams
            Dim tempSalt As String = String.Empty
            Dim tempToken As String = String.Empty

            tempSalt = APIDecrypt(GetSection("Commons")("SysSalt"), GetSection("Commons")("GenSysTkn"))
            tempToken = APIDecrypt(GetSection("Commons")("SysUniqTkn"), tempSalt)
            reqVerifyUserIDParams.Salt = GetSection("Commons")("SysSalt")
            reqVerifyUserIDParams.Token = APIEncrypt(tempSalt + tempToken, tempSalt)
            reqVerifyUserIDParams.Password = Password
            reqVerifyUserIDParams.Username = Username.ToUpper
            reqVerifyUserIDParams.IPAddress = IPAddress
            reqVerifyUserIDParams.SystemName = SystemName
            reqVerifyUserIDParams.Browser = Browser

            Dim reqGatewayParams As New ApiGatewayParams
            reqGatewayParams.Salt = GetSection("Commons")("SysSalt")
            reqGatewayParams.ServiceID = GetSection("Commons")("VerifyAppID")

            Dim data As String = APIEncrypt(JsonConvert.SerializeObject(reqVerifyUserIDParams), GetSection("Commons")("GenSysTkn"))
            reqGatewayParams.Data = data

            APIResponse = API.APICallGateway(JsonConvert.SerializeObject(reqGatewayParams))

            If APIResponse.StatusCode = 200 Then
                rdr = New StreamReader(APIResponse.GetResponseStream)
                AuthToken = rdr.ReadToEnd.Replace("""", "") 'API Response
            Else
                errMsg = GetSection("Messages")("MessageErrorSystemTransaction")
                Return False
            End If

            APIResponse = API_GetUserDetails()
            rdr = New StreamReader(APIResponse.GetResponseStream)

            If APIResponse.StatusCode = 401 Then
                errMsg += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.STANDARD_LOGIN_ERROR, Nothing, Nothing)
                Return False
            ElseIf APIResponse.StatusCode = 200 Then
                APIdata = JsonConvert.DeserializeObject(Of LoginUser)(rdr.ReadToEnd)
            Else
                errMsg = GetSection("Messages")("MessageErrorSystemTransaction")
                Return False
            End If

            If APIdata.isLoginSuccess Then
                Username = APIdata.Username.ToString()
                StatusAPI = APIdata.StatusID.ToString()
                InitialLoginAPI = APIdata.InitialLogin.ToString()
                AuthToken = APIdata.AuthToken.ToString()
                UnitCode = APIdata.UnitCode.ToString()
                GroupCode = APIdata.GroupCode.ToString()
                GroupMembership = JsonConvert.DeserializeObject(Of List(Of GrpMembership))(JsonConvert.SerializeObject(APIdata.GroupMembership))
                DatePasswordExpiryAPI = APIdata.DatePasswordExpiry.ToString()

                dt = dal.GetLoginDetails(Username)

                If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                    RoleID = dt.Rows(0)("RoleID").ToString
                    DisplayName = dt.Rows(0)("DisplayName").ToString
                    RoleName = dt.Rows(0)("RoleName").ToString
                    Status = dt.Rows(0)("StatusID").ToString
                    InitialLogin = dt.Rows(0)("InitialLogin").ToString
                    DatePasswordExpiry = dt.Rows(0)("DatePasswordExpiry").ToString
                    PasswordAttempt = APIdata.PasswordAttempt.ToString()

                    Dim InUse As String = dt.Rows(0)("InitialLogin").ToString
                    Dim DateLastLogon As String = dt.Rows(0)("DateLastLogon").ToString
                    Dim UserIP As String = dt.Rows(0)("IPAddress").ToString

                    If Not CheckLoginCredentialsAPI() Then Return False

                    If Not VerifyAvailability(InUse, UserIP, DateLastLogon) Then Return False

                    If Not CheckIfPasswordExpired() Then Return False
                Else
                    errMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.STANDARD_LOGIN_ERROR, Nothing, Nothing)
                    Return False
                End If

            Else
                If Not String.IsNullOrEmpty(APIdata.PasswordAttempt) Then
                    PasswordAttempt = APIdata.PasswordAttempt.ToString()
                    incrementLoginAttempt()
                Else
                    errMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.STANDARD_LOGIN_ERROR, Nothing, Nothing)
                End If
                Return False
            End If

            Return True
        Catch ex As Exception
            errMsg = GetSection("Messages")("MessageErrorSystemTransaction")
            Return False
        End Try
    End Function

    Function ValidateIfInUse() As Boolean
        Dim dal As New UsersDAL

        If dal.ValidateIfInUse(Username, Workstation) Then
            errMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.ALREADY_LOGGED_IN, Username, Nothing)
            Return False
        End If

        Return True
    End Function

    Private Function CheckLoginCredentialsAPI() As Boolean
        If CheckLoginCredentials() Then 'Checking if user is active on local DB

            If StatusAPI = "1" Then
                If InitialLoginAPI = "1" Then 'Redirect to change pass
                    IsForChangePassword = True
                Else
                    IsForChangePassword = False
                End If
            End If

            If StatusAPI = "2" Then
                errMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.IS_LOCKED, Nothing, Nothing)
                Return False
            End If

            If StatusAPI = "3" Then
                errMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.DEACTIVATED, Nothing, Nothing)
                Return False
            End If

        Else
            IsForChangePassword = False
        End If

        Return True
    End Function
#End Region
#End Region
#End Region
End Class
