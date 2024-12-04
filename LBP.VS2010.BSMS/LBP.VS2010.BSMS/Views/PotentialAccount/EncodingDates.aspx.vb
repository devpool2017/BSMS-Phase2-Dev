Imports System.Web.Services
Imports System.Web.Script.Services
Imports LBP.VS2010.BSMS.DataObjects

Imports LBP.VS2010.BSMS.Utilities
Imports System.Configuration.ConfigurationManager

Public Class EncodingDates
    Inherits OptimizedPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ModuleName = "Encoding Dates"
        If Not Page.IsPostBack Then
            Page.DataBind()
        End If
    End Sub

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function OnLoadData() As EncodingDatesOnLoad
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Return New EncodingDatesOnLoad With {
            .currentUser = currentUser,
            .CanUpdate = HasAccess("CanUpdate"),
            .CanApprove = HasAccess("CanApprove")
        }
    End Function

    <WebMethod(EnableSession:=True)>
   <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetCPADates() As EncodingDatesDO

        Dim encodeDates As New EncodingDatesDO
        encodeDates.GroupCode = HttpContext.Current.Session("GroupCode")
        WriteActivityLog(ActivityLog.ActionTaken.LIST)



        Return encodeDates.GetCPADates(encodeDates.GroupCode)
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function EditCPADates(obj As EncodingDatesDO) As String
        Dim message As String = String.Empty

        obj.LogonUser = HttpContext.Current.Session("LogonUser")
        obj.GroupCode = HttpContext.Current.Session("GroupCode")
        If obj.Validate() Then
            If Convert.ToDateTime(obj.PAEndDate) >= Convert.ToDateTime(obj.PAStartDate) Then

                If Not obj.EditCPADates() Then
                    message = obj.errMsg

                End If
            Else
                message = Messages.ErrorMessages.Validation.getErrorMessage(Messages.ErrorMessages.Validation.ErrorType.INVALID_CPADATES, "", Nothing) + "</br>"
            End If
        Else
            message = obj.errMsg
        End If

        Return message
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ApproveCPADates() As String
        Dim message As String = String.Empty
        Dim obj As New EncodingDatesDO
        obj.LogonUser = HttpContext.Current.Session("LogonUser")
        obj.GroupCode = HttpContext.Current.Session("GroupCode")
        obj.Action = "Approve"

        If Not obj.ApproveCPADates() Then
            message = obj.errMsg

        End If

        Return message
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function RejectCPADates() As String
        Dim message As String = String.Empty
        Dim obj As New EncodingDatesDO
        obj.LogonUser = HttpContext.Current.Session("LogonUser")
        obj.GroupCode = HttpContext.Current.Session("GroupCode")
        obj.Action = "Reject"

        If Not obj.RejectCPADates() Then
            message = obj.errMsg

        End If

        Return message
    End Function

    <WebMethod(EnableSession:=True)>
    <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function GetMergeDetails(regionCode As String) As List(Of EncodingDatesDO.EncodingDatesResult)
        ' WriteActivityLog(ActivityLog.ActionTaken.VIEW)

        Return EncodingDatesDO.GetMergeDetails(regionCode)
    End Function


    Class EncodingDatesOnLoad
        Property currentUser As Object
        Property CanUpdate As Boolean
        Property CanApprove As Boolean

    End Class
End Class