Imports LBP.VS2010.BSMS.DataAccess
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages
Imports LBP.VS2010.BSMS.CustomValidators.LengthAndValueValidator
Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.CustomValidators
Imports System.Configuration.ConfigurationManager
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables
Imports LBP.VS2010.BSMS.Utilities
Imports System.Configuration

<Serializable()>
Public Class EncodingDO
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


    <DataColumnMapping("ClientID")>
    Property ClientID As String

    <DataColumnMapping("CID")>
    Property CID As String

    <DataColumnMapping("ClientName")>
    Property ClientName As String

    <DataColumnMapping("ClientIndustry")>
    Property ClientIndustry As String

    <DataColumnMapping("DateEncoded")>
    Property DateEncoded As String

    <DataColumnMapping("CName")>
    Property CName As String

    <DataColumnMapping("CIndustry")>
    Property CIndustry As String

    <DataColumnMapping("CIndustryID")>
    Property CIndustryID As String

    <DataColumnMapping("CIndustryCode")>
    Property CIndustryCode As String

    Property userName As String
    Property lastCPAID As String
    Property newCPAID As String
    Property Action As String
    Property userBranch As String
    Property branchCode As String
    Property DBranchID As String
    Property DBranchDesc As String
    Property Tag As String
    Property CPAID As String
    Property DateNotRange As String
#End Region

#Region "MISC VARIABLES"
    Property Search As String
    Property SearchBy1 As String
    Property SearchType As String
    Property errorMessage As String
    Property success As String
    Property message As String
    'Property isSuccess As Boolean = True

#End Region
#Region "LOAD"

    Shared Function CheckCPADateRange(ByVal UserID As String) As Boolean
        Dim dal As New EncodingDAL
        Dim dt As New DataTable
        Dim success As Boolean
        Dim result As String = String.Empty
        result = dal.CheckCPADateRange(UserID)
        If result = "0" Then
            success = False
        Else
            success = True

        End If
        Return success
    End Function

    Shared Function GetAllCPAsByYearPerBM(ByVal branchCode As String, ByVal accessObj As ProfileDO) As List(Of EncodingDO)
        Dim dal As New EncodingDAL
        Dim dt As DataTable
        dt = dal.GetAllCPAsByYearPerBM(branchCode)
        If Not IsNothing(dt) Then
            Dim lst As New List(Of EncodingDO)
            If dt.Rows.Count > 0 Then
                Dim canUpdate As Boolean = accessObj.CanUpdate = "Y"
                Dim canApprove As Boolean = accessObj.CanApprove = "Y"
                Dim canInsert As Boolean = accessObj.CanInsert = "Y"
                Dim canDelete As Boolean = accessObj.CanDelete = "Y"
                Dim canPrint As Boolean = accessObj.CanPrint = "Y"
                Dim canView As Boolean = accessObj.CanView = "Y"

                For Each dtRow As DataRow In dt.Rows
                    Dim access As New ProfileDO With {
                        .CanView = IIf(dtRow.Item("Tag").ToString = "Y", "", ""),
                        .CanUpdate = IIf(dtRow.Item("Tag").ToString = "N", accessObj.CanUpdate, ""),
                        .CanInsert = accessObj.CanInsert,
                        .CanDelete = accessObj.CanDelete
                    }
                    lst.Add(
                        New EncodingDO With {
                        .Access = access,
                        .ClientID = IIf(dtRow.Item("CPAID").ToString = "", "", dtRow.Item("CPAID")),
                        .ClientName = IIf(dtRow.Item("Name").ToString = "", "", dtRow.Item("Name")),
                        .ClientIndustry = IIf(dtRow.Item("Industry").ToString = "", "", dtRow.Item("Industry")),
                        .DateEncoded = IIf(dtRow.Item("DateEncoded").ToString = "", "", dtRow.Item("DateEncoded")),
                        .Tag = IIf(dtRow.Item("Tag").ToString = "", "", dtRow.Item("Tag"))
                        })
                Next
            End If
            Return lst
        End If
        Return Nothing

        ' Return listRecords(Of EncodingDO)(dal.GetAllCPAsByYearPerBM(userName), accessObj)

    End Function
    Shared Function loadAccounts(ByVal userName As String, ByVal accessObj As ProfileDO)
        Dim dal As New EncodingDAL

        Return listRecords(Of EncodingDO)(dal.listAccounts(userName), accessObj)

    End Function

    Shared Function viewAccountDetails(ByVal ClientID As String) As EncodingDO
        Dim dal As New EncodingDAL
        Dim dt As New DataTable

        dt = dal.viewAccountDetails(ClientID)

        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                Return New EncodingDO(dt.Rows(0), dt.Columns)
            End If
        End If

        Return Nothing
    End Function

    Shared Function ddlIndustry() As DataTable
        Dim dal As New EncodingDAL
        Dim dt As New DataTable

        dt = dal.ddlIndustry()

        Return dt
    End Function

#End Region


#Region "SAVE"

    Public Function validateParams(ByVal param As EncodingDO) As Boolean
        Dim success As Boolean = True

        If param.CName.Equals("") Or param.CIndustryID.Equals("") Then
            If param.CIndustryID.Equals("") Then
                success = False
                errMsg = ConfigurationManager.GetSection("Messages")("CIndIDErrorMessage").ToString() + "<br/>"
            Else
                success = False
                errMsg = errMsg + ConfigurationManager.GetSection("Messages")("CNameErrorMessage").ToString() + "<br/>"
            End If
        Else
            If Not param.checkAction(param) Then
                success = False
                'errMsg = errMsg + "Error Saving Potential Account" + "<br/>"
            End If
        End If

        Return success
    End Function


    Public Function checkAction(ByVal param As EncodingDO) As Boolean
        Dim success As Boolean = True

        If param.Action.Equals("add") Then
            If param.CheckCPANameExists(param.CName, param.userBranch, param.userName, param.CIndustryID) Then
                'If param.CheckCPANameClients(param.CName, param.userBranch) Then
                If Not param.CheckCPANameNotLost(param.CName) Then
                    success = False
                    errMsg = errMsg + "Error Saving Potential Account" + "<br/>"
                End If
                'Else
                '  success = False
                'End If
            Else
                success = False
            End If

        ElseIf param.Action.Equals("update") Then
            If (param.ClientName = param.CName) Then
                If param.CheckCPANameClients(param.CName, param.userBranch) Then
                    If Not param.saveUpdate(param.ClientID, param.ClientName, param.CName, param.CIndustryID) Then
                        success = False
                        errMsg = errMsg + "Error Saving Potential Account" + "<br/>"
                    End If
                Else
                    success = False
                End If
            Else
                If param.CheckCPANameExists(param.CName, param.userBranch, param.userName, param.CIndustry) Then
                    'If param.CheckCPANameClients(param.CName, param.userBranch) Then
                    If Not param.saveUpdate(param.ClientID, param.ClientName, param.CName, param.CIndustryID) Then
                        success = False
                        errMsg = errMsg + "Error Saving Potential Account" + "<br/>"
                    End If
                    ' Else
                    '   success = False
                    ' End If
                Else
                    success = False
                End If
            End If
        End If
        Return success
    End Function

    Public Function CheckCPANameExists(ByVal CName As String, ByVal userBranch As String, ByVal userName As String, ByVal CIndustry As String) As Boolean
        Dim success As Boolean = True
        Dim dal As New EncodingDAL


        If dal.chckCPAName(CName, userBranch) Then
            getBranchByUserID(CName)
            success = False
            errMsg = "Potential Account already exists in Branch " + DBranchDesc + "."
        End If

        Return success
    End Function

    Public Function CheckCPANameClients(ByVal CName As String, ByVal userBranch As String) As Boolean
        Dim success As Boolean = True
        Dim dal As New EncodingDAL


        If dal.chckCPANames(CName, userBranch) Then
            getBranchByUserID(CName)
            success = False
            errMsg = "Potential Account already exists in Branch " + DBranchDesc + "."
        End If

        Return success

    End Function

    Public Function getBranchByUserID(ByVal CName As String) As DataTable
        Dim dal As New EncodingDAL
        Dim dt As New DataTable

        dt = dal.getBranchByUserID(CName)

        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                DBranchDesc = dt.Rows(0).ItemArray(0).ToString
            End If
        End If

        Return Nothing
    End Function

    Public Function CheckCPANameNotLost(ByVal CName As String) As Boolean
        Dim success As Boolean = True
        Dim lastCPAIDint As Integer
        Dim lastCPAIDStr As String
        Dim IndustryDesc As String
        Dim dal As New EncodingDAL


        If Not dal.chkIfExistingChkCPANotLost(CName) Then
            errMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.IS_EXISTS, "is existing ", {"existing"}) + "<br/>"
            success = False
        Else
            getNewCPAID(userBranch)

            If lastCPAID.Equals("") Then
                IndustryDesc = CIndustry.ToString().Replace("**-", "")
                newCPAID = userBranch + "-" + "00001"
                If Not AddNewCPA(newCPAID, CName, CIndustryID, userName) Then
                    success = False
                End If
            Else
                lastCPAIDint = Int(lastCPAID)
                lastCPAIDint = lastCPAIDint + 1
                lastCPAIDStr = lastCPAIDint.ToString

                IndustryDesc = CIndustry.ToString().Replace("**-", "")
                CIndustry = IndustryDesc
                If lastCPAIDStr.Length = 1 Then
                    newCPAID = userBranch + "-0000" + lastCPAIDStr

                    If Not AddNewCPA(newCPAID, CName, CIndustryID, userName) Then
                        success = False
                        errMsg = errMsg + "Error Saving Potential Account" + "<br/>"
                    End If
                ElseIf lastCPAIDStr.Length = 2 Then
                    newCPAID = userBranch + "-000" + lastCPAIDStr
                    If Not AddNewCPA(newCPAID, CName, CIndustryID, userName) Then
                        success = False
                        errMsg = errMsg + "Error Saving Potential Account" + "<br/>"
                    End If
                ElseIf lastCPAIDStr.Length = 3 Then
                    newCPAID = userBranch + "-00" + lastCPAIDStr
                    If Not AddNewCPA(newCPAID, CName, CIndustryID, userName) Then
                        success = False
                        errMsg = errMsg + "Error Saving Potential Account" + "<br/>"
                    End If
                ElseIf lastCPAIDStr.Length = 4 Then
                    newCPAID = userBranch + "-0" + lastCPAIDStr
                    If Not AddNewCPA(newCPAID, CName, CIndustryID, userName) Then
                        success = False
                        errMsg = errMsg + "Error Saving Potential Account" + "<br/>"
                    End If
                ElseIf lastCPAIDStr.Length = 5 Then
                    newCPAID = userBranch + "-" + lastCPAIDStr
                    If Not AddNewCPA(newCPAID, CName, CIndustryID, userName) Then
                        success = False
                        errMsg = errMsg + "Error Saving Potential Account" + "<br/>"
                    End If
                End If

            End If

        End If


        Return success
    End Function

    Public Function getNewCPAID(ByVal usersBranch As String) As DataTable
        Dim dal As New EncodingDAL
        Dim dt As New DataTable

        dt = dal.getNewCPAID(usersBranch)
        lastCPAID = dt.Rows(0).ItemArray(0).ToString

        Return Nothing
    End Function

    Public Function getCIndustryDesc(ByVal CIndustryID As String) As DataTable
        Dim dal As New EncodingDAL
        Dim dt As New DataTable

        dt = dal.getCIndustryDesc(CIndustryID)
        CIndustry = dt.Rows(0).ItemArray(0).ToString

        Return Nothing
    End Function

    Public Function AddNewCPA(ByVal ClientID As String, ByVal CName As String, ByVal CIndustry As String, ByVal userName As String) As Boolean
        Dim success As Boolean = True
        Dim dal As New EncodingDAL

        If Not dal.AddNewCPA(newCPAID, CName, CIndustry, userName) Then
            success = False
            errMsg = errMsg + "Error Saving Potential Account" + "<br/>"
        End If

        Return success
    End Function

    Public Function saveUpdate(ByVal ClientID As String, ByVal ClientName As String, ByVal CName As String, ByVal CIndustryID As String) As Boolean
        Dim success As Boolean = True
        Dim dal As New EncodingDAL

        If Not dal.saveCPAUpdate(ClientID, ClientName, CName, CIndustryID) Then
            success = False
            errMsg = errMsg + "Error Saving Potential Account" + "<br/>"
        End If

        Return success
    End Function

#End Region

End Class