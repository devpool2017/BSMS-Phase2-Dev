Imports System.Web.Services
Imports System.Web.Script.Services
Imports System.Configuration
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.DataObjects
Imports System.Configuration.ConfigurationManager

Public Class ReviewGroups
    Inherits OptimizedPage
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.DataBind()
    End Sub


#Region "Get List Groups"
    <WebMethod(EnableSession:=True)>
     <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function ListGroups() As FiltersOnLoad
        Dim obj As New GroupsDO
        Dim dt As New DataTable
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        Dim lastRow As DataRow
        Dim lastIndex As Integer
        Dim ListGrps As New List(Of GroupsDO)
        'Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        'dt = obj.GroupList()
        'lastRow = dt.Rows(dt.Rows.Count - 1)
        'lastIndex = Convert.ToInt64(lastRow.Item(0).ToString)
        'HttpContext.Current.Session("lastIndex") = lastIndex
        'obj.lastIndex = lastIndex
        'ListGrps = GroupsDO.listRecords(Of GroupsDO)(dt)
        dt = GroupsDO.listRecordsToTable(Of GroupsDO)(obj.GroupList(lastIndex, GetModuleAccess()))
        lastRow = dt.Rows(dt.Rows.Count - 1)
        lastIndex = Convert.ToInt64(lastRow.Item(0).ToString)
        HttpContext.Current.Session("lastIndex") = lastIndex
        obj.lastIndex = lastIndex

        Return New FiltersOnLoad With {
        .currentUser = currentUser,
        .RegionList = obj.GroupList(obj.lastIndex, GetModuleAccess())
     }
    End Function
#End Region

#Region "Add New Groups"
    <WebMethod(EnableSession:=True)>
<ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function AddNewGroups(ByVal obj As GroupsDO) As String
        Dim refobj As New GroupsDO

        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")
        refobj.UserID = currentUser.Username
        refobj.GroupName = obj.GroupName
        refobj.GroupCode = obj.GroupCode

        Dim message As String = String.Empty
        If Not refobj.ValidateSearch() Then
            message = refobj.errMsg
        Else
            If Not refobj.CheckIfGroupCodeExist(refobj.GroupCode) Then
                message = "Group code already exists."
            Else
                If Not refobj.CheckIfGroupNameExist(refobj.GroupName) Then
                    message = "Group name already exists."
                Else
                    If Not refobj.SaveNewGroup(refobj) Then
                        refobj.errMsg = "Failed to save new Group."
                        message = refobj.errMsg
                    Else
                        message = ""
                    End If

                End If
            End If
        End If
        Return message
    End Function

#End Region

#Region "Update Groups"
    <WebMethod(EnableSession:=True)>
  <ScriptMethod(ResponseFormat:=ResponseFormat.Json)>
    Public Shared Function UpdateGroups(ByVal obj As GroupsDO) As String
        Dim refobj As New GroupsDO
        Dim currentUser As LoginUser = HttpContext.Current.Session("currentUser")

        refobj.UserID = currentUser.Username
        refobj.GroupName = obj.GroupName
        refobj.GroupCode = obj.GroupCode

        Dim message As String = String.Empty

        If Not refobj.ValidateSearch() Then
            message = refobj.errMsg
        Else
            If Not refobj.CheckIfGroupNameExist(obj.GroupName) Then
                message = "Group name already exists."
            Else
                If Not refobj.UpdateNewGroup(refobj) Then
                    message = "Failed to update Group."
                Else
                    message = ""
                    'WriteActivityLog(ActivityLog.ActionTaken.UPDATE)
                End If
            End If
        End If

        Return message
    End Function
#End Region

    Class FiltersOnLoad
        Property currentUser As Object
        Property RegionList As List(Of GroupsDO)
    End Class
End Class