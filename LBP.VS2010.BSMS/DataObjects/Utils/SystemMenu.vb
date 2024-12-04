Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.CustomValidators.LengthAndValueValidator
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables
Imports LBP.VS2010.BSMS.Utilities
Imports LBP.VS2010.BSMS.CustomValidators
Imports LBP.VS2010.BSMS.DataAccess

Public Class SystemMenu
    Inherits MasterDO

    ' ALWAYS INCLUDE THESE INSTATIATIONS
#Region "INSTANTIATION"
    Sub New()

    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub


#End Region
#Region "DECLARATION"
    <DataColumnMapping("MainMenuID")>
    Property MainMenuID As String

    <DataColumnMapping("MainMenuName")>
    Property MainMenuName As String

    <DataColumnMapping("IconSpan")>
    Property IconSpan As String

    <DataColumnMapping("SubMenu_PK")>
    Property SubMenuID As String

    <DataColumnMapping("SubMenuName")>
    Property SubMenuName As String

    <DataColumnMapping("URL")>
    Property URL As String

#End Region

#Region "FUNCTIONS"
#Region "LIST/GET"
    Shared Function ListSystemMenu() As DataTable
        Dim dal As New SystemMenuDAL

        Return dal.ListSystemMenu
    End Function
    Shared Function ListSystemMenuByRole(ByVal roleID As String) As DataTable
        Dim dal As New SystemMenuDAL

        Return dal.ListSystemMenuByRole(RoleID)
    End Function
    Shared Function ListSystemMenuTabByRole(ByVal roleID As String) As DataTable
        Dim dal As New SystemMenuDAL

        Return dal.ListSystemMenuTabByRole(roleID)
    End Function
#End Region
#End Region
End Class
