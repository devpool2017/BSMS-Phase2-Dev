Imports System.Web.Services
Imports System.Web.Script.Services
Imports LBP.VS2010.BSMS.DataObjects
Imports LBP.VS2010.BSMS.Utilities
Imports System.Configuration.ConfigurationManager

Public Class Revisits
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Revisits"

        If Not Page.IsPostBack Then
            Page.DataBind()
        End If
    End Sub

#Region "LOAD CONTENT"
    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function checkCPA() As String
        Dim CPAFullname As String
        CPAFullname = HttpContext.Current.Session("CPAFullame")
        Return CPAFullname
    End Function

    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function AccountDetailsFromSummary() As FiltersOnLoad

        Dim ClientID As String = HttpContext.Current.Session("CPAClientID")

        'Dim list As RevisitsDO

        Return New FiltersOnLoad With {
         .RevisitList = ReferencesForDropdown.convertToReferencesList(RevisitsDO.RevisitList(ClientID), "ProspectName", "Prospect"),
         .Revisits = RevisitsDO.AccountDetails(ClientID, GetModuleAccess())
}
        'HttpContext.Current.Session("AccountDetails") = list

    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ListAccounts(ByVal param As RevisitsDO) As List(Of ReferencesForDropdown)
        Dim dt As DataTable
        dt = RevisitsDO.AccountList(param)

        Return ReferencesForDropdown.convertToReferencesList(dt, "ClientID", "Name", True, "Please Select")

    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function AccountDetails(ByVal ClientID As String) As FiltersOnLoad
        'Dim list As RevisitsDO = RevisitsDO.AccountDetails(ClientID)

        'HttpContext.Current.Session("AccountDetails") = list

        Return New FiltersOnLoad With {
         .RevisitList = ReferencesForDropdown.convertToReferencesList(RevisitsDO.RevisitList(ClientID), "ProspectName", "Prospect"),
         .Revisits = RevisitsDO.AccountDetails(ClientID, GetModuleAccess())
}
        'Return list
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function AccountDetailsList(ByVal ClientID As String) As RevisitsDO
        Dim list As RevisitsDO = RevisitsDO.AccountDetailsList(ClientID)

        HttpContext.Current.Session("AccountDetails") = list

        Return list
    End Function


    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ListRevisit(ByVal ClientID As String) As List(Of ReferencesForDropdown)
        Dim dt As DataTable

        Dim FromSummaryClientID As String = HttpContext.Current.Session("CPAClientID")

        If FromSummaryClientID <> "" Then
            ClientID = FromSummaryClientID
        End If
        dt = RevisitsDO.RevisitList(ClientID)

        Return ReferencesForDropdown.convertToReferencesList(dt, "ProspectName", "Prospect", False)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ListAllRevisit(ByVal ClientID As String) As List(Of ReferencesForDropdown)
        Dim dt As DataTable
        dt = RevisitsDO.RevisitAllList(ClientID)

        Return ReferencesForDropdown.convertToReferencesList(dt, "ProspectName", "Prospect", False)

    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function CountRevisit(ByVal ClientID As String) As RevisitsDO
        Dim list As RevisitsDO = RevisitsDO.CountRevisit(ClientID)

        HttpContext.Current.Session("AccountDetails") = list
        Return list
    End Function

#End Region


#Region "UPDATE CONTENT"


    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function addProspect(ByVal param As RevisitsDO) As String
        Dim message As String = String.Empty
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim fontcolor As String = If({GetRoleID.SH, GetRoleID.GH, GetRoleID.RO}.Contains(currentUser.RoleID), "<font color=""orange"">", If({GetRoleID.AppAdminA, GetRoleID.AppAdminM}.Contains(currentUser.RoleID), "<font color=""#003399"">", "<font color=""#007700"">"))
        Dim OldRemarks As String

        OldRemarks = param.GetOldRemarks(param.ClientID, param.UserID)
        param.NewComments = If((Not param.NewRemarks = Nothing), _
                                                OldRemarks & fontcolor & currentUser.DisplayName & " said on " _
                                                & My.Computer.Clock.LocalTime.ToString & " : " & param.NewRemarks & "</font><br>", _
                                               OldRemarks & "")

        If param.isChecked.Equals(True) Then
            If Not param.NewRemarks.Equals("") Then
                If Not param.NewComments.Length >= 8000 Then
                    If param.updateCPAClient() Then
                        If Not param.addProspectDate() Then
                            message = param.errMsg
                        End If
                    End If
                Else
                    message = "Updating not successful. Remarks will not be saved."
                End If
            Else
                message = "Updating not successful. Feedback is Required"
            End If

        ElseIf {GetRoleID.SH, GetRoleID.GH, GetRoleID.RO}.Contains(currentUser.RoleID) Then
            If param.ClientID.Equals("0") Then
                param.ClientID = HttpContext.Current.Session("CPAClientID")
                OldRemarks = param.GetOldRemarksNotBM(param.ClientID)
                param.NewComments = If((Not param.NewRemarks = Nothing), _
                                                        OldRemarks & fontcolor & currentUser.DisplayName & " said on " _
                                                        & My.Computer.Clock.LocalTime.ToString & " : " & param.NewRemarks & "</font><br>", _
                                                        OldRemarks & "")
                If Not param.NewRemarks.Equals("") Then
                    If Not param.NewComments.Length >= 8000 Then
                        If Not param.updateCPAClientCommentNotBM() Then
                            message = param.errMsg
                        End If
                    Else
                        message = "Updating not successful. Remarks will not be saved."
                    End If
                Else
                    message = "Updating not successful. Feedback is Required"
                End If
            Else
                message = "Updating not successful. No changes made"
            End If
        Else
            message = "Updating not successful. Feedback is Required"
        End If
        Return message

    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function addComment(ByVal param As RevisitsDO) As String
        Dim message As String = String.Empty
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim fontcolor As String = If({GetRoleID.SH, GetRoleID.GH, GetRoleID.RO}.Contains(currentUser.RoleID), "<font color=""orange"">", If({GetRoleID.AppAdminA, GetRoleID.AppAdminM}.Contains(currentUser.RoleID), "<font color=""#003399"">", "<font color=""#007700"">"))
        Dim OldRemarks As String

        OldRemarks = param.GetOldRemarks(param.ClientID, param.UserID)
        param.NewComments = If((Not param.NewRemarks = Nothing), _
                                                OldRemarks & fontcolor & currentUser.DisplayName & " said on " _
                                                & My.Computer.Clock.LocalTime.ToString & " : " & param.NewRemarks & "</font><br>", _
                                               OldRemarks & "")
        If {GetRoleID.SH, GetRoleID.GH, GetRoleID.RO}.Contains(currentUser.RoleID) Then
            If param.ClientID.Equals("0") Then
                param.ClientID = HttpContext.Current.Session("CPAClientID")
                OldRemarks = param.GetOldRemarksNotBM(param.ClientID)
                param.NewComments = If((Not param.NewRemarks = Nothing), _
                                                        OldRemarks & fontcolor & currentUser.DisplayName & " said on " _
                                                        & My.Computer.Clock.LocalTime.ToString & " : " & param.NewRemarks & "</font><br>", _
                                                        OldRemarks & "")
                If Not param.NewRemarks.Equals("") Then
                    If Not param.NewComments.Length >= 8000 Then
                        If Not param.updateCPAClientCommentNotBM() Then
                            message = param.errMsg
                        End If
                    Else
                        message = "Updating not successful. Remarks will not be saved."
                    End If
                Else
                    message = "Updating not successful. Feedback is Required"
                End If
            End If
        Else
            If Not param.NewRemarks.Equals("") Then
                If Not param.NewComments.Length >= 8000 Then
                    If Not param.updateCPAClientComment() Then
                        message = param.errMsg
                    End If
                Else
                    message = "Updating not successful. Remarks will not be saved."
                End If
            Else
                message = "Updating not successful. Feedback is Required"
            End If
        End If

        Return message

    End Function

#End Region

    Public Shared Function GetRoleID() As PositionID
        Return New PositionID With {
       .BH = GetSection("Commons")("BM").ToString,
       .RO = GetSection("Commons")("RO").ToString,
       .GH = GetSection("Commons")("GroupHead").ToString,
       .SH = GetSection("Commons")("SectorHead").ToString,
       .AppAdminA = GetSection("Commons")("AppAdminApprover").ToString,
       .AppAdminM = GetSection("Commons")("AppAdminMaker").ToString
       }
    End Function
    Class PositionID
        Property BH As String
        Property RO As String
        Property GH As String
        Property SH As String
        Property AppAdminA As String
        Property AppAdminM As String
    End Class

    Class FiltersOnLoad
        Property RevisitList As List(Of ReferencesForDropdown)
        'Property Revisits As RevisitsDO
        Property Revisits As List(Of RevisitsDO)
    End Class

End Class