Imports System.Configuration.ConfigurationManager
Imports System.Web.Script.Services
Imports System.Web.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities

Public Class UpdateClientDetails
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Update Target Market"

        If Not Page.IsPostBack Then
            Page.DataBind()
        End If
    End Sub

#Region "LOAD CONTENT"
    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function OnLoadValues() As ValuesOnLoad
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim username As String = IIf(currentUser.RoleID = GetSection("Commons")("BM"), currentUser.Username, String.Empty)
        Dim canView As Boolean
        Dim canUpdate As Boolean

        Dim client As New ClientDO
        client.ClientID = HttpContext.Current.Session("ClientID")
        HttpContext.Current.Session("ClientDetails") = client.GetClientDetails()

        Dim accInfo As New AccountInfoDO
        accInfo.ClientID = client.ClientID

        Dim access As New ProfileDO
        access = HttpContext.Current.Session("ConversionAccessMatrix")

        Dim accessWA As New ProfileDO
        accessWA = HttpContext.Current.Session("WeeklyActivityAccessMatrix")

        Dim accessSearch As New ProfileDO
        accessSearch = HttpContext.Current.Session("SearchAccessMatrix")

        If Not IsNothing(access) Then
            canView = IIf(access.CanView = "Y", True, False)
            canUpdate = IIf(access.CanUpdate = "Y", True, False)
        ElseIf Not IsNothing(accessWA) Then
            canView = IIf(accessWA.CanView = "Y", True, False)
            canUpdate = IIf(accessWA.CanUpdate = "Y", True, False)
        ElseIf Not IsNothing(accessSearch) Then
            canView = IIf(accessSearch.CanView = "Y", True, False)
            canUpdate = IIf(accessSearch.CanUpdate = "Y", True, False)
        Else
            canView = HasAccess("CanView")
            canUpdate = HasAccess("CanUpdate")
        End If

        Return New ValuesOnLoad With {
            .currentUser = currentUser,
            .ClientTypeList = ReferencesForDropdown.convertToReferencesList(ClientDO.ClientTypeList, "ClientTypeCode", "ClientTypeName", False),
            .LeadSourceList = ReferencesForDropdown.convertToReferencesList(ClientDO.LeadSourceList, "LeadSourceCode", "LeadSourceDesc", False),
            .IndustryTypeList = ReferencesForDropdown.convertToReferencesList(ClientDO.IndustryList, "IndustryCode", "DescIndicator", False),
            .ReasonList = ReferencesForDropdown.convertToReferencesList(ClientDO.ReasonList, "ReasonDesc", "ReasonDesc", True, "Others"),
            .CASAProductList = ClientDO.CASAProductList,
            .LoanProductList = ClientDO.LoanProductList,
            .OtherProductList = ClientDO.OtherProductList,
            .LoanProductListDDL = ReferencesForDropdown.convertToReferencesList(ClientDO.LoanProductListDDL, "CASACodes", "CASAShortName", True, String.Empty),
            .ClientDetails = HttpContext.Current.Session("ClientDetails"),
            .AccountNumberList = accInfo.GetAccountNumber(),
            .LoansAvailedList = accInfo.GetLoansAvailed(),
            .OtherParameters = OtherParametersDO.GetOtherParameters().Item(0),
            .CanView = canView,
            .CanUpdate = canUpdate,
            .isBM = IIf(currentUser.RoleID = GetSection("Commons")("BM"), True, False)
            }
    End Function
#End Region

#Region "VALIDATE"

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateClient(ByVal obj As ClientDO) As String
        Dim message As String = String.Empty
        obj.Branch = HttpContext.Current.Session("currentUser").BranchCode.PadLeft(4, "0")

        If Not obj.isValid() Then
            message = obj.errMsg
        End If

        If (Not IsNothing(obj.Customer)) And (String.IsNullOrWhiteSpace(obj.CASATypes) And
                                              String.IsNullOrWhiteSpace(obj.LoansAvailed) And
                                              String.IsNullOrWhiteSpace(obj.OtherATypes)) Then
            message += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.ACC_INFO_REQUIRED, Nothing, {Nothing}) + "<br/>"
        End If

        Dim accountNumber As String() = {""}
        If Not String.IsNullOrWhiteSpace(obj.AccountNumbers) Then
            accountNumber = obj.AccountNumbers.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
        End If

        Dim max As String = System.Configuration.ConfigurationManager.GetSection("Commons")("MaxAccountNumber")

        If accountNumber.Count > Integer.Parse(max) Then
            message += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.ACC_MAX, Nothing, {max}) + "<br/>"
        End If

        Return message
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateAccountInfo(ByVal obj As AccountInfoDO) As String
        Dim message As String = String.Empty

        If Not obj.isValid() Then
            message = obj.errMsg
        End If

        Return message
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateCASA(ByVal obj As AccountInfoDO) As String
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim message As String = String.Empty
        Dim LBPChkDigit As New CHKDIGIT.LBPChkDigit
        Dim validAccount As Boolean = True

        If Not obj.ValidateCASA("Account Number", Nothing) Then
            message = obj.errMsg
        Else
            'If Not Integer.Parse(obj.AccountNumber.Substring(0, 3)) = Integer.Parse(currentUser.BranchCode) Then
            '    message += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.INV_ACC_BRANCH, Nothing, {Nothing}) + "<br/>"
            'Else
            validAccount = LBPChkDigit.GetCheckDigit(obj.AccountNumber)
            If Not validAccount Then
                message += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.INVALID_DATA, "Account Number", {Nothing}) + "<br/>"
            End If
            'End If
        End If

        Return message
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ValidateLoan(ByVal obj As AccountInfoDO) As String
        Dim message As String = String.Empty

        If Not obj.ValidateLoan("Loan Amount Reported", "Update") Then
            message = obj.errMsg
        End If

        Return message
    End Function

#End Region

#Region "SAVE/UPDATE"

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function UpdateClient(ByVal obj As ClientDO) As String
        Dim message As String = String.Empty
        Dim accountInfo As New AccountInfoDO
        Dim OldDetails As ClientDO = HttpContext.Current.Session("ClientDetails")
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")

        obj.Branch = currentUser.BranchCode.PadLeft(4, "0")
        obj.UploadedBy = currentUser.Username
        obj.RegionCode = currentUser.GroupCode
        obj.ClientID = OldDetails.ClientID
        obj.OldRemarks = OldDetails.Remarks
        obj.FName = OldDetails.FName
        obj.MName = OldDetails.MName
        obj.LName = OldDetails.LName
        obj.CPAID = OldDetails.CPAID
        obj.ClientType = OldDetails.ClientType

        If OldDetails.Remarks.Length + obj.Remarks.Length > 7800 Then
            message += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.REMARKS_LIMIT, Nothing, {Nothing}) + "<br/>"
        Else
            If Not obj.UpdateNewTargetMarket(obj, currentUser).Equals(String.Empty) Then
                message += obj.errMsg + "<br/>"
            End If
        End If
        

        Return message
    End Function

#End Region
    Class ValuesOnLoad
        Property currentUser As Object
        Property ClientTypeList As List(Of ReferencesForDropdown)
        Property LeadSourceList As List(Of ReferencesForDropdown)
        Property IndustryTypeList As List(Of ReferencesForDropdown)
        Property ReasonList As List(Of ReferencesForDropdown)
        Property CASAProductList As List(Of ClientDO)
        Property LoanProductList As List(Of ClientDO)
        Property OtherProductList As List(Of ClientDO)
        Property LoanProductListDDL As List(Of ReferencesForDropdown)
        Property ClientDetails As ClientDO
        Property AccountNumberList As List(Of AccountInfoDO)
        Property LoansAvailedList As List(Of AccountInfoDO)
        Property OtherParameters As OtherParametersDO
        Property CanView As Boolean
        Property CanUpdate As Boolean
        Property isBM As Boolean
    End Class
End Class