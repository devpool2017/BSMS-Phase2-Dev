Imports System.Configuration.ConfigurationManager
Imports Newtonsoft.Json
Imports System.Net
Imports LBP.VS2010.BSMS.Utilities
Imports System.IO

Public Class VerifyUserIDParams
    Property Salt As String
    Property Token As String
    Property Username As String
    Property Password As String
    Property IPAddress As String
    Property Browser As String
    Property SystemName As String
    Property PasswordHandler As String
End Class

Public Class UserValidationParams
    Property Username As String
    Property Password As String
    Property PasswordHandler As String
    Property IPAddress As String
    Property Browser As String
    Property SystemName As String
End Class

Public Class ChangePasswordParams
    Property Username As String
    Property Password As String
    Property NewPassword As String
    Property PasswordHandler As String
    Property IPAddress As String
    Property Browser As String
    Property SystemName As String
End Class

Public Class ApiGatewayParams
    Property Salt As String
    Property ServiceID As String
    Property Data As Object
End Class

Public Class GetUserInfoParams
    Property RequestedUsername As String
    Property IPAddress As String
    Property Browser As Object
    Property SystemName As Object
    Property Requestor As Object
End Class

Public Class ValidateUserIDRoleParams
    Property Username As String
    Property IPAddress As String
    Property Browser As Object
    Property SystemName As Object
End Class

Public Class ValidateUserIDRoleResponseParams
    Public Property StatusCode As String
    Public Property Message As String
    Public Property refreshedToken As String
    Public Property RequestValidation As Integer
End Class

Public Class GetUserInfoResponseParams
    Public Property HasError As Boolean = False
    Public Property ErrorMessage As String = ""
    Public Property Username As String
    Public Property LastName As String
    Public Property FirstName As String
    Public Property MiddleInitial As String
    Public Property Suffix As String
    Public Property Email As String
    Public Property StatusID As String
    Public Property UnitCode As String
    Public Property GroupCode As String
    Public Property EmployeeNumber As String
    Public Property AuthToken As String
    Public Property StatusCode As String
End Class

Public Class APIDO
    Inherits MasterDO

#Region "Functions"

#Region "GetOtherUserInfor"
    'Shared Function GetOtherUserInfo(ByVal ProfileID As String) As List(Of UsersDO)
    Shared Function GetOtherUserInfo(ByVal ProfileID As String) As String

        Return ""
    End Function

    Public Shared Function VerifyAppID(ByVal reqVerifyUserIDParams As VerifyUserIDParams) As HttpWebResponse
        Dim APIResponse As HttpWebResponse
        Dim APIdata As LoginUser = Nothing

        Dim reqGatewayParams As New ApiGatewayParams
        reqGatewayParams.Salt = GetSection("Commons")("SysSalt")
        reqGatewayParams.ServiceID = GetSection("Commons")("VerifyAppID")

        Dim data As String = APIEncrypt(JsonConvert.SerializeObject(reqVerifyUserIDParams), GetSection("Commons")("GenSysTkn"))
        reqGatewayParams.Data = data

        APIResponse = API.APICallGateway(JsonConvert.SerializeObject(reqGatewayParams))

        Return APIResponse
    End Function

    Shared Function ValidateUserIDRole(ByVal reqGetUserInfoParams As ValidateUserIDRoleParams, ByVal authToken As String) As HttpWebResponse
        Dim APIResponse As HttpWebResponse
        Dim APIdata As LoginUser = Nothing

        Dim reqGatewayParams As New ApiGatewayParams
        reqGatewayParams.Salt = GetSection("Commons")("SysSalt")
        reqGatewayParams.ServiceID = GetSection("Commons")("ValidateUserIDRole")

        Dim data As String = APIEncrypt(JsonConvert.SerializeObject(reqGetUserInfoParams), GetSection("Commons")("GenSysTkn"))
        reqGatewayParams.Data = data

        APIResponse = API.APICallGateway(JsonConvert.SerializeObject(reqGatewayParams), True, authToken)

        Return APIResponse
    End Function

    Shared Function GetUserInfoSolo(ByVal reqGetUserInfoParams As GetUserInfoParams, ByVal authToken As String) As HttpWebResponse
        Dim APIResponse As HttpWebResponse
        Dim APIdata As LoginUser = Nothing

        Dim reqGatewayParams As New ApiGatewayParams
        reqGatewayParams.Salt = GetSection("Commons")("SysSalt")
        reqGatewayParams.ServiceID = GetSection("Commons")("GetUserInfo")

        Dim data As String = APIEncrypt(JsonConvert.SerializeObject(reqGetUserInfoParams), GetSection("Commons")("GenSysTkn"))
        reqGatewayParams.Data = data

        APIResponse = API.APICallGateway(JsonConvert.SerializeObject(reqGatewayParams), True, authToken)

        Return APIResponse
    End Function

    Shared Function GetUserInfo(ByVal reqVerifyUserIDParams As VerifyUserIDParams, ByVal requestedUsername As String) As GetUserInfoResponseParams
        Dim APIResponse As HttpWebResponse = Nothing
        Dim APIdata As LoginUser = Nothing
        Dim theResponseData As New GetUserInfoResponseParams
        theResponseData.HasError = True
        theResponseData.ErrorMessage = GetSection("Messages")("MessageErrorSystemTransaction")


        Dim rdr As StreamReader
        Dim apiAuthToken As String = ""
        Dim data As String = ""

        Dim reqGatewayParams As New ApiGatewayParams
        reqGatewayParams.Salt = GetSection("Commons")("SysSalt")

        Dim apiValidateUserIDRoleReponse As ValidateUserIDRoleResponseParams = Nothing
        Dim apiGetUserInfoResponseParamsReponse As GetUserInfoResponseParams = Nothing
        Dim rawResponseData As String = ""

        Try

            Dim VerifyAppIDResponse As HttpWebResponse = VerifyAppID(reqVerifyUserIDParams)
            If VerifyAppIDResponse.StatusCode = 200 Then
                rdr = New StreamReader(VerifyAppIDResponse.GetResponseStream)
                apiAuthToken = rdr.ReadToEnd.Replace("""", "") 'API Response

                Dim reqValidateUserIDRole As New ValidateUserIDRoleParams
                reqValidateUserIDRole.Username = reqVerifyUserIDParams.Username
                reqValidateUserIDRole.IPAddress = reqVerifyUserIDParams.IPAddress
                reqValidateUserIDRole.Browser = reqVerifyUserIDParams.Browser
                reqValidateUserIDRole.SystemName = reqVerifyUserIDParams.SystemName

                APIResponse = ValidateUserIDRole(reqValidateUserIDRole, apiAuthToken)

                If APIResponse.StatusCode = 200 Then
                    Using reader As New StreamReader(APIResponse.GetResponseStream())
                        rawResponseData = reader.ReadToEnd()
                        apiValidateUserIDRoleReponse = Newtonsoft.Json.JsonConvert.DeserializeObject(Of ValidateUserIDRoleResponseParams)(rawResponseData)
                        theResponseData.HasError = False
                        theResponseData.ErrorMessage = ""
                    End Using

                    If apiValidateUserIDRoleReponse.StatusCode = "200" Or apiValidateUserIDRoleReponse.RequestValidation = 1 Then

                        Dim reqGetUserInfo As New GetUserInfoParams
                        reqGetUserInfo.RequestedUsername = requestedUsername
                        reqGetUserInfo.IPAddress = reqVerifyUserIDParams.IPAddress
                        reqGetUserInfo.Browser = reqVerifyUserIDParams.Browser
                        reqGetUserInfo.SystemName = reqVerifyUserIDParams.SystemName
                        reqGetUserInfo.Requestor = reqVerifyUserIDParams.Username

                        APIResponse = GetUserInfoSolo(reqGetUserInfo, apiAuthToken)

                        If APIResponse.StatusCode = 200 Then
                            Using reader As New StreamReader(APIResponse.GetResponseStream())
                                rawResponseData = reader.ReadToEnd()
                                apiGetUserInfoResponseParamsReponse = Newtonsoft.Json.JsonConvert.DeserializeObject(Of GetUserInfoResponseParams)(rawResponseData)
                                theResponseData = apiGetUserInfoResponseParamsReponse
                                If theResponseData.StatusCode = "525" Then
                                    theResponseData.HasError = True
                                    theResponseData.ErrorMessage = "Username does not exist."
                                End If
                            End Using
                        Else
                            Logger.writeAPIErrorLog("FunctionName: GetUserInfo; API Code: GetUserInfoSolo", "Status code != 200")
                        End If

                    Else
                        theResponseData.ErrorMessage = "Current user do not have permission to fetch user's basic information."
                    End If

                Else
                    Logger.writeAPIErrorLog("FunctionName: GetUserInfo; API Code: ValidateUserIDRole", "Status code != 200")
                End If

            Else
                Logger.writeAPIErrorLog("FunctionName: GetUserInfo; API Code: VerifyAppID", "Status code != 200")
            End If
        Catch ex As Exception
            Logger.writeAPIErrorLog("FunctionName: GetUserInfo;", "Exception Error: " + ex.Message)
        End Try

        Return theResponseData
    End Function

#End Region

#End Region
End Class