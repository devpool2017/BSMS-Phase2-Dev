Imports System.DirectoryServices
Imports System.DirectoryServices.AccountManagement
Imports System.Configuration
Imports LBP.VS2010.WebCryptor

Public Class LDAPAuthentication
    Dim _webConfig As New WebConfigCryptor

    Property Domains As String()
    Property Domain As String
    Property UserName As String
    Property Password As String
    Property UserProfileCode As String

    Property DisplayName As String
    Property LastName As String
    Property FirstName As String
    Property MiddleName As String
    Property BranchCode As String
    Property EmailAddress As String
    Property IsExpired As Boolean = False

    Property ErrMsg As String

    Public Function IsValidADUser(Optional ByVal domainIndex As Short = 0, Optional ByVal enabled As Boolean = True) As Boolean
        Dim Domain As String
        Domain = _webConfig.Decrypt("LDAPSource", True).ToString
        Dim Entry As DirectoryEntry = Nothing
        Dim Searcher As DirectorySearcher = Nothing
        Dim isValid As Boolean = False
        Dim server As String = "LDAP://" & Domain

        Try
            Entry = New DirectoryEntry(server, UserName, Password, AuthenticationTypes.Secure)
            Searcher = New DirectorySearcher(Entry)
            Searcher.SearchScope = SearchScope.OneLevel

            ErrMsg = String.Empty
            If CBool(_webConfig.Decrypt("ADCheck", True)) Then

                isValid = Not Searcher.FindOne Is Nothing

                If isValid Then
                    'Start of Comment
                    If validateLoginADSecurity(UserName, Password) Then

                        Entry = New DirectoryEntry(server)
                        Searcher = New DirectorySearcher(Entry)
                        Searcher.Filter = "(&(objectClass=user)(sAMAccountname=" & UserName & "))"
                        Searcher.PropertiesToLoad.Add("displayName")
                        Searcher.PropertiesToLoad.Add("sn")
                        Searcher.PropertiesToLoad.Add("givenName")
                        Searcher.PropertiesToLoad.Add("initials")
                        Searcher.PropertiesToLoad.Add("mail")
                        Dim result As SearchResult = Searcher.FindOne

                        If Not IsNothing(result) Then
                            DisplayName = result.Properties("displayName")(0).ToString
                            LastName = result.Properties("sn")(0).ToString
                            FirstName = result.Properties("givenName")(0).ToString
                            MiddleName = If(result.Properties.Contains("initials"), result.Properties("initials")(0).ToString, String.Empty)
                            EmailAddress = If(result.Properties.Contains("mail"), result.Properties("mail")(0).ToString, String.Empty)

                        Else
                            isValid = False
                            ErrMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.STANDARD_LOGIN_ERROR, Nothing, Nothing)
                            Logger.writeLog("Login", "AD Search user details", "", "no user details in AD")
                        End If

                    Else
                        isValid = False
                        Logger.writeLog("Login", "Security Group Validation", "", ErrMsg)
                    End If
                End If
                'End of Comment
            Else
                isValid = True
            End If


        Catch ex As Exception

            ErrMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.STANDARD_LOGIN_ERROR, Nothing, Nothing)
            Logger.writeLog("Login", "AD Validation", "", ex.Message)

        End Try

        If Not IsNothing(Entry) Then
            Entry.Dispose()
            Entry = Nothing
        End If
        If Not IsNothing(Searcher) Then
            Searcher.Dispose()
            Searcher = Nothing
        End If

        Return isValid

    End Function

    Public Function validateLoginADSecurity(ByVal Username As String, ByVal Password As String) As Boolean
        Dim authenticated As Boolean
        Dim Domain As String = _webConfig.Decrypt("LDAPSource", True).ToString
        Dim server As String = "LDAP://" & Domain

        Try
            
            'Dim entry As New DirectoryEntry(server, Username, Password)
            'Dim nativeObject As Object = entry.NativeObject

            Dim AD As New PrincipalContext(ContextType.Domain, Domain)
            Dim u As New UserPrincipal(AD)
            u.SamAccountName = Username

            Dim groupFound As Boolean = False
            Dim search As New PrincipalSearcher(u)
            Dim result As UserPrincipal = DirectCast(search.FindOne(), UserPrincipal)
            Dim userGroups = result.GetAuthorizationGroups()

            Dim SecurityGroup As String = _webConfig.Decrypt("SecurityGroup", True).ToString

            For Each group As Principal In userGroups
                If group.Name = SecurityGroup Then
                    groupFound = True
                    Exit For
                End If
            Next

            If groupFound Then
                authenticated = True
            Else
                ErrMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.SECURITYGROUP_LOGIN_ERROR, Nothing, Nothing)
                authenticated = False
                Logger.writeLog("Login/User", "Security Group Validation", "", "User not enrolled in the group")
            End If

            DisplayName = Convert.ToString(result)
            search.Dispose()

        Catch ex As Exception
            ErrMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.STANDARD_LOGIN_ERROR, Nothing, Nothing)
            Logger.writeLog("Login", "AD Validation", "", ex.InnerException.Message)
            Return False
        End Try


        Return authenticated
    End Function

    Public Function SearchUser(Optional ByVal domainIndex As Short = 0, Optional ByVal enabled As Boolean = True) As Boolean

        Dim Domain As String
        Domain = Domains(domainIndex)

        Dim Entry As DirectoryEntry = Nothing
        Dim Searcher As DirectorySearcher = Nothing
        Dim isValid As Boolean = False

        Try

            Entry = New DirectoryEntry("LDAP://" & Domain)
            Searcher = New DirectorySearcher(Entry)
            Searcher.Filter = "(&(objectClass=user)(sAMAccountname=" & UserName & "))"
            Searcher.PropertiesToLoad.Add("displayName")
            Searcher.PropertiesToLoad.Add("sn")
            Searcher.PropertiesToLoad.Add("givenName")
            Searcher.PropertiesToLoad.Add("initials")
            Searcher.PropertiesToLoad.Add("extensionAttribute5")
            Searcher.PropertiesToLoad.Add("mail")
            Dim result As SearchResult = Searcher.FindOne

            If Not IsNothing(result) Then
                DisplayName = result.Properties("displayName")(0).ToString
                LastName = result.Properties("sn")(0).ToString
                FirstName = result.Properties("givenName")(0).ToString
                MiddleName = If(result.Properties.Contains("initials"), result.Properties("initials")(0).ToString, String.Empty)
                BranchCode = If(result.Properties.Contains("extensionAttribute5"), result.Properties("extensionAttribute5")(0).ToString, String.Empty)
                EmailAddress = If(result.Properties.Contains("mail"), result.Properties("mail")(0).ToString, String.Empty)

                isValid = True
                result = Nothing
            Else
                isValid = False

                If domainIndex < Domains.Length - 1 Then
                    isValid = SearchUser(domainIndex + 1, enabled)
                Else

                    ErrMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.NOT_SYSTEM_USER, Nothing, Nothing)

                End If
            End If

            'End If
        Catch ex As Exception
            If domainIndex < Domains.Length - 1 Then
                isValid = SearchUser(domainIndex + 1, enabled)
            Else

                ErrMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.NOT_SYSTEM_USER, Nothing, Nothing)

            End If
            Logger.writeLog("Login", "AD Validation", "", ex.Message)
        End Try

        If Not IsNothing(Entry) Then
            Entry.Dispose()
            Entry = Nothing
        End If
        If Not IsNothing(Searcher) Then
            Searcher.Dispose()
            Searcher = Nothing
        End If

        Return isValid

    End Function


    Public Function GetName(ByVal UserName As String, Optional ByVal domainIndex As Short = 0, Optional ByVal enabled As Boolean = True) As Boolean
        Dim Domain As String
        Domain = _webConfig.Decrypt("LDAPSource", True).ToString

        Dim Entry As DirectoryEntry = Nothing
        Dim Searcher As DirectorySearcher = Nothing
        Dim isValid As Boolean = False

        Try
            Entry = New DirectoryEntry("LDAP://" & Domain)

            Searcher = New DirectorySearcher(Entry)
            Searcher.Filter = "(&(objectClass=user)(sAMAccountname=" & UserName & "))"

            Searcher.PropertiesToLoad.Add("displayName")
            Searcher.PropertiesToLoad.Add("description")
            Searcher.PropertiesToLoad.Add("sn")
            Searcher.PropertiesToLoad.Add("givenName")
            Searcher.PropertiesToLoad.Add("initials")
            Searcher.PropertiesToLoad.Add("extensionAttribute5")
            Searcher.PropertiesToLoad.Add("mail")

            Dim result As SearchResult = Searcher.FindOne

            If Not IsNothing(result) Then
                UserName = UserName
                DisplayName = result.Properties("displayName")(0).ToString
                LastName = result.Properties("sn")(0).ToString
                FirstName = result.Properties("givenName")(0).ToString
                MiddleName = If(result.Properties.Contains("initials"), result.Properties("initials")(0).ToString, String.Empty)
                EmailAddress = If(result.Properties.Contains("mail"), result.Properties("mail")(0).ToString, String.Empty)

                ' OVERWRITE DISPLAY NAME IF BRANCH USER
                'If domainIndex = 1 Then
                '    DisplayName = If(result.Properties.Contains("description"), result.Properties("description")(0).ToString, String.Empty)
                'End If

                isValid = True
                result = Nothing
                ErrMsg = Nothing
            Else
                'ErrMsg = "Not found in the domain " & Domain & "<br />"
                isValid = False

                ErrMsg += "Not found in the domain " & Domain & "<br />"

            End If
        Catch ex As Exception
            'Return {"GetName -error-" & ex.Message}
            ErrMsg += "GetName -error-" & ex.Message
            Logger.writeLog("User", "Validate User", "", ex.Message)
        End Try

        If Not IsNothing(Entry) Then
            Entry.Dispose()
            Entry = Nothing
        End If
        If Not IsNothing(Searcher) Then
            Searcher.Dispose()
            Searcher = Nothing
        End If

        'Return {LastName, FirstName, MiddleName, DisplayName, EmailAddress}

        Return isValid

    End Function
End Class
