Imports LBP.VS2010.BSMS.DataAccess
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages
Imports LBP.VS2010.BSMS.CustomValidators.LengthAndValueValidator
Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.CustomValidators
Imports System.Configuration.ConfigurationManager
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables
Imports LBP.VS2010.BSMS.Utilities

<Serializable()>
Public Class RevisitsDO
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

    <DataColumnMapping("ClientName")>
    Property ClientName As String

    <DataColumnMapping("Remarks")>
    Property Remarks As String

    <DataColumnMapping("Prospect")>
    Property Prospect As String

    <DataColumnMapping("RevisitCount")>
    Property RevisitCount As String

    <DataColumnMapping("Visits")>
    Property Visits As String


    Property NewRemarks As String
    Property NewComments As String
    Property UserID As String
    Property ClientID As String
    Property Tag As String
    Property isChecked As Boolean

#End Region

#Region "List"

    Shared Function AccountList(ByVal param As RevisitsDO) As DataTable
        Dim dal As New RevisitsDAL
        Dim dt As New DataTable

        dt = dal.GetListAccount(param.UserID, param.Tag)

        Return dt
    End Function


    Shared Function AccountDetails(ByVal ClientID As String, ByVal accessObj As ProfileDO) As List(Of RevisitsDO)
        Dim dal As New RevisitsDAL
        Dim dt As New DataTable

        If Not (ClientID = "") Then
            If Not (ClientID.Contains("-")) Then
                Dim cientIDLength As Int16 = ClientID.Length
                ClientID = ClientID.Substring(0, 4) + "-" + ClientID.Substring(4, (cientIDLength - 4))
            End If
        End If

            dt = dal.AccountDetails(ClientID)

        If Not IsNothing(dt) Then
            Dim lst As New List(Of RevisitsDO)
            If dt.Rows.Count > 0 Then
                Dim canView As Boolean = accessObj.CanView = "Y"
                Dim canUpdate As Boolean = accessObj.CanUpdate = "Y"
                Dim canInsert As Boolean = accessObj.CanInsert = "Y"
                Dim canDelete As Boolean = accessObj.CanDelete = "Y"
                For Each dtRow As DataRow In dt.Rows
                    Dim access As New ProfileDO With {
                        .CanView = accessObj.CanView,
                        .CanUpdate = accessObj.CanUpdate,
                        .CanInsert = accessObj.CanInsert,
                        .CanDelete = accessObj.CanDelete
                    }
                    lst.Add(
                        New RevisitsDO With {
                        .Access = access,
                        .ClientName = IIf(dtRow.Item("ClientName").ToString = "", "", dtRow.Item("ClientName")),
                        .Remarks = IIf(dtRow.Item("Remarks").ToString = "", "", dtRow.Item("Remarks")),
                        .Prospect = IIf(dtRow.Item("Prospect").ToString = "", "", dtRow.Item("Prospect")),
                        .Visits = IIf(dtRow.Item("Visits").ToString = "", "", dtRow.Item("Visits"))
                        })
                Next
            End If
            Return lst
        End If
        Return Nothing
    End Function

    Shared Function AccountDetailsList(ByVal ClientID As String) As RevisitsDO
        Dim dal As New RevisitsDAL
        Dim dt As New DataTable

        If Not (ClientID = "") Then
            If Not (ClientID.Contains("-")) Then
                Dim cientIDLength As Int16 = ClientID.Length
                ClientID = ClientID.Substring(0, 4) + "-" + ClientID.Substring(4, (cientIDLength - 4))
            End If
        End If

            dt = dal.AccountDetails(ClientID)

        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                Return New RevisitsDO(dt.Rows(0), dt.Columns)
            End If
        End If

        Return Nothing
    End Function

    Shared Function RevisitList(ByVal ClientID As String) As DataTable
        Dim dal As New RevisitsDAL
        Dim dt As New DataTable

        If Not (ClientID = "") Then
            If Not (ClientID.Contains("-")) Then
                Dim cientIDLength As Int16 = ClientID.Length
                ClientID = ClientID.Substring(0, 4) + "-" + ClientID.Substring(4, (cientIDLength - 4))
            End If
        End If

        dt = dal.GetListRevisit(ClientID)

        Return dt
    End Function

    Shared Function RevisitAllList(ByVal ClientID As String) As DataTable
        Dim dal As New RevisitsDAL
        Dim dt As New DataTable

        If Not (ClientID = "") Then
            If Not (ClientID.Contains("-")) Then
                Dim cientIDLength As Int16 = ClientID.Length
                ClientID = ClientID.Substring(0, 4) + "-" + ClientID.Substring(4, (cientIDLength - 4))
            End If

        End If

        dt = dal.GetAllListRevisit(ClientID)

        Return dt
    End Function

    Shared Function CountRevisit(ByVal ClientID As String) As RevisitsDO
        Dim dal As New RevisitsDAL
        Dim dt As New DataTable
        If Not (ClientID = "") Then
            If Not (ClientID.Contains("-")) Then
                Dim cientIDLength As Int16 = ClientID.Length
                ClientID = ClientID.Substring(0, 4) + "-" + ClientID.Substring(4, (cientIDLength - 4))
            End If
        End If

            dt = dal.GetRevisitCount(ClientID)

        If Not IsNothing(dt) Then
            If dt.Rows.Count > 0 Then
                Return New RevisitsDO(dt.Rows(0), dt.Columns)
            End If
        End If

        Return Nothing
    End Function


    Public Function GetOldRemarks(ByVal ClientID As String,
                                       ByVal UserID As String) As String
        Dim dal As New RevisitsDAL

        If Not (ClientID = "") Then
            If Not (ClientID.Contains("-")) Then
                Dim cientIDLength As Int16 = ClientID.Length
                ClientID = ClientID.Substring(0, 4) + "-" + ClientID.Substring(4, (cientIDLength - 4))
            End If
        End If

            Return dal.GetRemarks(ClientID, UserID)
    End Function

    Public Function GetOldRemarksNotBM(ByVal ClientID As String) As String
        Dim dal As New RevisitsDAL

        If Not (ClientID = "") Then
            If Not (ClientID.Contains("-")) Then
                Dim cientIDLength As Int16 = ClientID.Length
                ClientID = ClientID.Substring(0, 4) + "-" + ClientID.Substring(4, (cientIDLength - 4))
            End If
        End If

            Return dal.GetRemarksNotBM(ClientID)
    End Function

#End Region

#Region "Add"

    Public Function addProspectDate() As Boolean
        Dim success As Boolean = True
        Dim dal As New RevisitsDAL

        'If Not (ClientID = "") Then
        '    Dim cientIDLength As Int16 = ClientID.Length
        '    ClientID = ClientID.Substring(0, 4) + "-" + ClientID.Substring(4, (cientIDLength - 4))
        'End If

        If Not dal.addProspectDate(ClientID, UserID, Prospect) Then
            errMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.SAVE_ERROR, "saving of records", {"saving of records"}) + "<br/>"
            success = False
        End If

        Return success
    End Function

    Public Function updateCPAClient() As Boolean
        Dim success As Boolean = True
        Dim dal As New RevisitsDAL

        If Not (ClientID = "") Then
            If Not (ClientID.Contains("-")) Then
                Dim cientIDLength As Int16 = ClientID.Length
                ClientID = ClientID.Substring(0, 4) + "-" + ClientID.Substring(4, (cientIDLength - 4))
            End If
        End If

            If Not dal.updateCPAClient(ClientID, Prospect, Visits, NewComments) Then
            errMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.SAVE_ERROR, "saving of records", {"saving of records"}) + "<br/>"
            success = False
        End If

        Return success
    End Function

    Public Function updateCPAClientComment() As Boolean
        Dim success As Boolean = True
        Dim dal As New RevisitsDAL

        If Not (ClientID = "") Then
            If Not (ClientID.Contains("-")) Then
                Dim cientIDLength As Int16 = ClientID.Length
                ClientID = ClientID.Substring(0, 4) + "-" + ClientID.Substring(4, (cientIDLength - 4))
            End If
        End If

            If Not dal.updateCPAClientComment(ClientID, UserID, NewComments) Then
            errMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.SAVE_ERROR, "saving of records", {"saving of records"}) + "<br/>"
            success = False
        End If

        Return success
    End Function

    Public Function updateCPAClientCommentNotBM() As Boolean
        Dim success As Boolean = True
        Dim dal As New RevisitsDAL

        If Not (ClientID = "") Then
            If Not (ClientID.Contains("-")) Then
                Dim cientIDLength As Int16 = ClientID.Length
                ClientID = ClientID.Substring(0, 4) + "-" + ClientID.Substring(4, (cientIDLength - 4))
            End If
        End If

            If Not dal.updateCPAClientCommentNotBM(ClientID, NewComments) Then
            errMsg = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.SAVE_ERROR, "saving of records", {"saving of records"}) + "<br/>"
            success = False
        End If

        Return success
    End Function


#End Region
End Class
