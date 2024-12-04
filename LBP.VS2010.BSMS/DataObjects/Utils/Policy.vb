Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.DataAccess

Public Class Policy
    Inherits MasterDO

    ' ALWAYS INCLUDE THESE INSTATIATIONS
#Region "INSTATNTIATION"
    Sub New()

    End Sub
    Sub New(ByVal dr As DataRow, ByVal columnNames As DataColumnCollection)
        MyBase.New(dr, columnNames)
    End Sub


#End Region

#Region "DECLARATION"

#Region "TABLE COLUMNS"
    <DataColumnMapping("Policy_FK")>
    Property PolicyID As String

    <DataColumnMapping("SubMenu_FK")>
    Property SubMenuID As String

    <DataColumnMapping("CanView")>
    Property CanView As String

    <DataColumnMapping("CanInsert")>
    Property CanInsert As String

    <DataColumnMapping("CanUpdate")>
    Property CanUpdate As String

    <DataColumnMapping("CanDelete")>
    Property CanDelete As String

    <DataColumnMapping("CanPrint")>
    Property CanPrint As String

    <DataColumnMapping("CanValidate")>
    Property CanValidate As String

    <DataColumnMapping("CanConsolidate")>
    Property CanConsolidate As String

    <DataColumnMapping("IsAdmin")>
    Property IsAdmin As String
#End Region

#Region "MISC COLUMNS"
    <DataColumnMapping("SubMenuName")>
    Property SubMenuName As String
#End Region
#Region "FUNCTIONS"
#Region "LIST"
    Shared Function ListPolicyForProfile(ByVal ProfileID As String) As List(Of ProfileDO)
        Dim dal As New PolicyDAL

        Return MasterDO.listRecords(Of ProfileDO)(dal.ListSystemMenu(ProfileID))
    End Function
#End Region
#Region "GET POLICY ADD ACCESS"
    Shared Function GetAllCRUDAccess() As Policy
        Dim pol As New Policy

        pol.CanView = "Y"
        pol.CanInsert = "Y"
        pol.CanUpdate = "Y"
        pol.CanDelete = "Y"

        Return pol
    End Function
#End Region

#End Region

#End Region
End Class
