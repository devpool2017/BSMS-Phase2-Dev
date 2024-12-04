Imports LBP.VS2010.BSMS.DataAccess
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages
Imports LBP.VS2010.BSMS.CustomValidators.LengthAndValueValidator
Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.CustomValidators
Imports System.Configuration.ConfigurationManager
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables

<Serializable()>
Public Class RoleDO
    Inherits MasterDO

    ' ALWAYS INCLUDE THESE INSTATIATIONS
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
#Region "TABLE COLUMNS"
    Property RoleId As String
    Property RoleTempId As String

    <DisplayLabel("Role Name")>
    <RequiredField()>
    <CustomValidationMethod("IsNameExists")>
    <ControlVariable(FieldVariables.ROLE_NAME)>
    <MaxLengthCheck()>
    Property RoleName As String

    <DisplayLabel("Role Description")>
    <RequiredField()>
    <ControlVariable(FieldVariables.ROLE_DESCRIPTION)>
    <MaxLengthCheck()>
    Property RoleDescription As String

    Property Status As String
#End Region

#Region "MISC COLUMNS"
    Property TempStatus As String
    Property RequestedBy As String
    Property DateRequested As String
    Property InEdit As String
    Property Action As String
    Property LogonUser As String
#End Region
#End Region

#Region "MISC VARIABLES"
    <DisplayLabel("Module")>
    <CustomValidationMethod("IsModuleSelected")>
    Property ProfileList As List(Of ProfileDO)

    Property TabProfileList As List(Of ProfileDO.TabProfiles)
#End Region

#Region "FUNCTION"
#Region "GET/LIST"
    Shared Function GetList(searchText As String, filterStat As String, accessObj As ProfileDO) As List(Of RoleDO)
        Dim dal As New RoleDAL
        Dim dt As DataTable = dal.GetList(searchText, filterStat)

        If Not IsNothing(dt) Then
            Dim lst As New List(Of RoleDO)

            If dt.Rows.Count > 0 Then
                Dim canUpdate As Boolean = accessObj.CanUpdate = "Y"
                Dim canApprove As Boolean = accessObj.CanApprove = "Y"
                Dim canInsert As Boolean = accessObj.CanInsert = "Y"
                Dim canDelete As Boolean = accessObj.CanDelete = "Y"
                Dim canPrint As Boolean = accessObj.CanPrint = "Y"
                Dim canView As Boolean = accessObj.CanView = "Y"
                Dim canActivate As Boolean = accessObj.CanActivate = "Y"

                For Each dtRow As DataRow In dt.Rows
                    Dim access As New ProfileDO With {
                        .CanView = accessObj.CanView,
                        .CanUpdate = If((dtRow.Item("Status") = "Deactivated"), "N", accessObj.CanUpdate),
                        .CanInsert = accessObj.CanInsert,
                        .CanDelete = If((dtRow.Item("Status") = "Active") And (dtRow.Item("InEdit") = False), "Y", "N"),
                        .CanActivate = If((Not (dtRow.Item("Status") = "Active")) And (dtRow.Item("InEdit") = False), "Y", "N")
                    }


                    lst.Add(New RoleDO With {
                    .Access = access,
                    .RoleId = dtRow.Item("RoleId").ToString,
                    .RoleName = dtRow.Item("RoleName").ToString,
                    .RoleDescription = dtRow.Item("RoleDescription").ToString,
                    .Status = dtRow.Item("Status").ToString
                })
                Next
            End If

            Return lst
        End If

        Return Nothing
    End Function

    Shared Function GetTempList(searchText As String, filterStatus As String) As List(Of RoleDO)
        Dim dal As New RoleDAL
        Dim dt As DataTable = dal.GetTempList(searchText, filterStatus)

        If Not IsNothing(dt) Then
            Dim lst As New List(Of RoleDO)

            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    lst.Add(New RoleDO With {
                    .RoleId = row.Item("RoleId").ToString,
                    .RoleTempId = row.Item("RoleTempId").ToString,
                    .RoleName = row.Item("RoleName").ToString,
                    .RoleDescription = row.Item("RoleDescription").ToString,
                    .Status = row.Item("Status").ToString,
                    .TempStatus = row.Item("TempStatus").ToString,
                    .RequestedBy = row.Item("RequestedBy").ToString,
                    .DateRequested = row.Item("DateRequested").ToString
                })
                Next
            End If

            Return lst
        End If

        Return Nothing
    End Function

    Shared Function GetDetails(roleId As String, Optional action As String = "", Optional accessObj As ProfileDO = Nothing) As RoleDO
        Dim dal As New RoleDAL
        Dim dalProfile As New ProfileDAL
        Dim dt As DataTable = dal.GetDetails(roleId)

        If Not IsNothing(dt) Then
            Dim dtProfile As DataTable = dalProfile.GetProfile(roleId, False)
            Dim lstProfile As New List(Of ProfileDO)

            For Each row As DataRow In dtProfile.Rows
                Dim tabProfiles As New List(Of ProfileDO.TabProfiles)


                lstProfile.Add(New ProfileDO With {
                    .MainMenuID = row.Item("MainMenuID").ToString,
                    .SubMenuID = row.Item("SubMenuID").ToString,
                    .MainMenuName = row.Item("MainMenuName").ToString,
                    .SubMenuName = row.Item("SubMenuName").ToString,
                    .CanView = row.Item("CanView").ToString,
                    .CanInsert = row.Item("CanInsert").ToString,
                    .CanUpdate = row.Item("CanUpdate").ToString,
                    .CanDelete = row.Item("CanDelete").ToString,
                    .CanPrint = row.Item("CanPrint").ToString,
                    .CanApprove = row.Item("CanApprove").ToString
             })
            Next

            If dt.Rows.Count > 0 Then
                Return New RoleDO With {
                    .Access = accessObj,
                    .Action = action,
                    .RoleId = dt.Rows(0)("RoleId").ToString,
                    .RoleName = dt.Rows(0)("RoleName").ToString,
                    .RoleDescription = dt.Rows(0)("RoleDescription").ToString,
                    .InEdit = dt.Rows(0)("InEdit").ToString,
                    .ProfileList = lstProfile
                }
            End If

            Return New RoleDO With {
                .Access = accessObj,
                .Action = action,
                .ProfileList = lstProfile
            }
        End If

        Return Nothing
    End Function

    Shared Function GetTempDetails(roleId As String, action As String, accessObj As ProfileDO) As RoleDO
        Dim dal As New RoleDAL
        Dim dalProfile As New ProfileDAL
        Dim dt As DataTable = dal.GetTempDetails(roleId)

        If Not IsNothing(dt) Then
            Dim dtProfile As DataTable = dalProfile.GetProfileTemp(roleId, False)
            Dim lstProfile As New List(Of ProfileDO)

            For Each row As DataRow In dtProfile.Rows
                Dim tabProfiles As New List(Of ProfileDO.TabProfiles)
                ' Dim dtTab As DataTable = dalProfile.GetTabProfileTemp(roleId, row.Item("SubMenuID").ToString, False)

                'If Not IsNothing(dtTab) Then
                '    For Each tabRow As DataRow In dtTab.Rows
                '        tabProfiles.Add(New ProfileDO.TabProfiles With {
                '        .TabID = tabRow.Item("TabID").ToString,
                '        .TabName = tabRow.Item("TabName").ToString,
                '        .SubMenuID = tabRow.Item("SubMenuID").ToString,
                '        .CanInsert = tabRow.Item("CanInsert").ToString,
                '        .CanUpdate = tabRow.Item("CanUpdate").ToString,
                '        .CanDelete = tabRow.Item("CanDelete").ToString,
                '        .CanPrint = tabRow.Item("CanPrint").ToString,
                '        .CanApprove = tabRow.Item("CanApprove").ToString
                '    })
                '    Next
                'End If

                lstProfile.Add(New ProfileDO With {
                    .MainMenuID = row.Item("MainMenuID").ToString,
                    .SubMenuID = row.Item("SubMenuID").ToString,
                    .MainMenuName = row.Item("MainMenuName").ToString,
                    .SubMenuName = row.Item("SubMenuName").ToString,
                    .CanView = row.Item("CanView").ToString,
                    .CanInsert = row.Item("CanInsert").ToString,
                    .CanUpdate = row.Item("CanUpdate").ToString,
                    .CanDelete = row.Item("CanDelete").ToString,
                    .CanPrint = row.Item("CanPrint").ToString,
                    .CanApprove = row.Item("CanApprove").ToString
                 })
            Next

            If dt.Rows.Count > 0 Then
                Return New RoleDO With {
                    .Access = accessObj,
                    .Action = action,
                    .RoleId = dt.Rows(0)("RoleId").ToString,
                    .RoleTempId = dt.Rows(0)("RoleTempId").ToString,
                    .RoleName = dt.Rows(0)("RoleName").ToString,
                    .RoleDescription = dt.Rows(0)("RoleDescription").ToString,
                    .ProfileList = lstProfile
                }
            End If

            Return New RoleDO With {
                .Access = accessObj,
                .Action = action,
                .ProfileList = lstProfile
            }
        End If

        Return Nothing
    End Function

    Shared Function GetRole(roleId As String) As RoleDO
        Dim dal As New RoleDAL
        Dim dalProfile As New ProfileDAL
        Dim dt As DataTable = dal.GetDetails(roleId)

        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then

            Dim dtProfile As DataTable = dalProfile.GetProfile(roleId, False)
            'Dim dtTabProfile As DataTable = dalProfile.GetTabProfileAll(roleId)
            'Dim tabProfiles As New List(Of ProfileDO.TabProfiles)
            'Dim tabProfiles2 As New List(Of ProfileDO)
            'Dim dtTab As DataTable = dalProfile.GetTabProfileAll(roleId)

            Dim subMenuProfiles As New List(Of ProfileDO)


            If Not IsNothing(dtProfile) Then
                For Each row As DataRow In dtProfile.Rows
                    subMenuProfiles.Add(New ProfileDO With {
                        .RoleID = roleId.ToString,
                        .MainMenuID = row.Item("MainMenuID").ToString,
                        .SubMenuID = row.Item("SubMenuID").ToString,
                        .MainMenuName = row.Item("MainMenuName").ToString,
                        .SubMenuName = row.Item("SubMenuName").ToString,
                        .CanView = row.Item("CanView").ToString,
                        .CanInsert = row.Item("CanInsert").ToString,
                        .CanUpdate = row.Item("CanUpdate").ToString,
                        .CanDelete = row.Item("CanDelete").ToString,
                        .CanPrint = row.Item("CanPrint").ToString,
                        .CanApprove = row.Item("CanApprove").ToString,
                        .URL = row.Item("URL").ToString
                    })
                Next
            End If

            Dim rdo As New RoleDO With {
                .RoleId = dt.Rows(0)("RoleId").ToString,
                .RoleName = dt.Rows(0)("RoleName").ToString,
                .RoleDescription = dt.Rows(0)("RoleDescription").ToString,
                .ProfileList = subMenuProfiles.ToList()
             }

            Return rdo
        End If

        Return Nothing
    End Function
#End Region

#Region "VALIDATION"
    Public Function IsNameExists(ByVal fieldLabel As String) As Boolean
        Dim dal As New RoleDAL

        If Action.Equals("1") AndAlso dal.IsNameExists(RoleName) Then
            errMsg += Validation.getErrorMessage(Validation.ErrorType.DUPLICATE, fieldLabel, {RoleName}) + "<br/>"
            Return False
        End If

        Return True
    End Function

    Public Function IsModuleSelected(ByVal fieldLabel As String) As Boolean
        Dim isSelected As Boolean = True
        Dim dt As DataTable = listRecordsToTable(ProfileList)
        Dim dtColumns As DataColumn() = dt.Columns.Cast(Of DataColumn).ToArray()

        isSelected = dt.AsEnumerable().Any(Function(row) dtColumns.Any(Function(col) row(col).ToString() = "Y"))
        If Not isSelected Then
            errMsg += Validation.getErrorMessage(Validation.ErrorType.NOTHING_SELECTED, fieldLabel, Nothing) + "<br/>"
            isSelected = False
        End If

        Return isSelected
    End Function
#End Region

#Region "MAIN"
    Public Function SaveTemp() As Boolean
        Dim dal As New RoleDAL
        Dim success As Boolean = True
        Dim dtProfiles As DataTable = listRecordsToTable(ProfileList, sortDirection:=String.Empty)
        Dim tabProfiles As New List(Of ProfileDO.TabProfiles)

        '--------------------
        Select Case Action
            Case "1"
                TempStatus = GetSection("Commons")("ForInsert").ToString
            Case "3"
                TempStatus = GetSection("Commons")("ForUpdate").ToString
            Case "4"
                TempStatus = GetSection("Commons")("ForDeactivation").ToString
            Case "10"
                TempStatus = GetSection("Commons")("ForActivation").ToString
        End Select

        If Not dal.SaveTemp(RoleId, RoleName, RoleDescription, 1, TempStatus, dtProfiles, LogonUser, False) Then
            success = False
            errMsg = dal.ErrorMsg
        End If

        If success Then
            dal.CommitTrans()
        Else
            dal.RollbackTrans()
        End If

        Return success
    End Function

    Public Function Approve() As Boolean
        Dim dal As New RoleDAL
        Dim success As Boolean = True

        Select Case TempStatus
            Case GetSection("Commons")("ForInsert").ToString
                If Not dal.InsertRole(RoleTempId, LogonUser, False) Then
                    errMsg = dal.ErrorMsg
                    Return False
                End If

            Case GetSection("Commons")("ForUpdate").ToString
                If Not dal.UpdateRole(RoleId, RoleTempId, LogonUser, False) Then
                    errMsg = dal.ErrorMsg
                    Return False
                End If

            Case GetSection("Commons")("ForDeactivation").ToString
                Dim stat As String = GetSection("Commons")("ForDeactivation").ToString
                If Not dal.DeactivateRole(RoleId, stat, LogonUser, False) Then
                    errMsg = dal.ErrorMsg
                    Return False
                End If
            Case GetSection("Commons")("ForActivation").ToString
                Dim stat As String = GetSection("Commons")("ForActivation").ToString
                If Not dal.DeactivateRole(RoleId, stat, LogonUser, False) Then
                    errMsg = dal.ErrorMsg
                    Return False
                End If
        End Select

        If success Then
            If Not dal.DeleteTemp(RoleTempId, LogonUser, False) Then
                errMsg = dal.ErrorMsg
                success = False
            End If
        End If

        If success Then
            dal.CommitTrans()
        Else
            dal.RollbackTrans()
        End If

        Return success
    End Function

    Shared Function Reject(roleTempId As String, logonUser As String) As String
        Dim dal As New RoleDAL
        Dim message As String = String.Empty

        If Not dal.DeleteTemp(roleTempId, logonUser) Then
            message = dal.ErrorMsg
        End If

        Return message
    End Function
#End Region
#End Region
End Class