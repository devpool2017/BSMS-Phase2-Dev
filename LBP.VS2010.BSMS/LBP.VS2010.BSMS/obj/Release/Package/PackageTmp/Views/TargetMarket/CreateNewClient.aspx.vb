Imports System.Configuration.ConfigurationManager
Imports System.Web.Script.Services
Imports System.Web.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.CHKDIGIT.LBPChkDigit

Public Class CreateNewClient
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Create Target Market"

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
        Dim dt As DataTable = ClientDO.GetOtherParameters

        Return New ValuesOnLoad With {
            .currentUser = currentUser,
            .ClientTypeList = ReferencesForDropdown.convertToReferencesList(ClientDO.ClientTypeList, "ClientTypeCode", "ClientTypeName", False),
            .LeadSourceList = ReferencesForDropdown.convertToReferencesList(ClientDO.LeadSourceList, "LeadSourceCode", "LeadSourceDesc", False),
            .IndustryTypeList = ReferencesForDropdown.convertToReferencesList(ClientDO.IndustryList, "IndustryCode", "DescIndicator", False),
            .ReasonList = ReferencesForDropdown.convertToReferencesList(ClientDO.ReasonList, "ReasonDesc", "ReasonDesc", True, "Others"),
            .CPANames = ReferencesForDropdown.convertToReferencesList(ClientDO.CPANamesList(currentUser.BranchCode.PadLeft(4, "0")), "CPAID", "Name", False),
            .CASAProductList = ClientDO.CASAProductList,
            .LoanProductList = ClientDO.LoanProductList,
            .OtherProductList = ClientDO.OtherProductList,
            .LoanProductListDDL = ReferencesForDropdown.convertToReferencesList(ClientDO.LoanProductListDDL, "CASACodes", "CASAShortName", True, String.Empty),
            .DaysBefore = dt.Rows(0).Item("DaysBefore")
            }
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetCPADetails(ByVal CPAID As String) As String
        Dim dt As DataTable = ClientDO.GetCPADetails(CPAID)

        Return dt.Rows(0).Item(1)
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

        If Not obj.ValidateLoan("Desired Loan Amount", "Create") Then
            message = obj.errMsg
        End If

        Return message
    End Function

#End Region

#Region "SAVE/UPDATE"

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function AddClient(ByVal obj As ClientDO) As String
        Dim message As String = String.Empty
        Dim accountInfo As New AccountInfoDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")

        If Not currentUser.BranchCode.Equals(String.Empty) Then

            obj.Branch = currentUser.BranchCode.PadLeft(4, "0")
            obj.ClientID = obj.NewClientID()
            obj.UploadedBy = currentUser.Username
            obj.RegionCode = currentUser.GroupCode

            If Not obj.AddNewTargetMarket(obj, currentUser).Equals(String.Empty) Then
                message += obj.errMsg + "<br/>"
            End If

        Else
            message += Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.UNAUTHORIZED, Nothing, {Nothing}) + "<br/>"
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
        Property CPANames As List(Of ReferencesForDropdown)
        Property CASAProductList As List(Of ClientDO)
        Property LoanProductList As List(Of ClientDO)
        Property OtherProductList As List(Of ClientDO)
        Property LoanProductListDDL As List(Of ReferencesForDropdown)
        Property DaysBefore As String
    End Class
End Class