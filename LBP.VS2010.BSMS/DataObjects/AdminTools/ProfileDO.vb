Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports System.Reflection

<Serializable()>
Public Class ProfileDO
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
    <DataColumnMapping("RoleID")>
    Property RoleID As String

    Property RoleName As String

    <DataColumnMapping("SubMenuID")>
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

    <DataColumnMapping("CanApprove")>
    Property CanApprove As String

    Property CanUnlock As String

    Property CanActivate As String

    <DataColumnMapping("URL")>
    Property URL As String
    'Property TabProfile As List(Of TabProfiles)
#End Region

#Region "MISC COLUMNS"
    Property SubMenuName As String
    Property MainMenuID As String
    Property MainMenuName As String
#End Region
#End Region

#Region "GET ACCESS MATRIX"
    Public Shared Function GetDefaultAccess(ByVal dr As DataRow) As ProfileDO
        Dim access As New ProfileDO
        Dim properties As IEnumerable(Of PropertyInfo) = access.GetType.GetProperties().Where(Function(prop) prop.IsDefined(GetType(DataColumnMapping), False))
        Dim columnPropertyMapper As New Dictionary(Of String, PropertyInfo)

        For Each prop In properties
            columnPropertyMapper.Add(CType(Attribute.GetCustomAttribute(prop, GetType(DataColumnMapping)), DataColumnMapping).columnField, prop)
        Next

        Dim columnNames As DataColumnCollection = dr.Table.Columns
        For Each dtColumn In columnNames
            Try
                Dim colName As String = dtColumn.ToString()
                Dim propName As String = Trim(colName).Substring(0, Trim(colName).Length - 7)
                If colName.Contains(".Access") Then
                    columnPropertyMapper(propName).SetValue(access, dr(colName).ToString, Nothing)
                End If
                columnPropertyMapper(dtColumn.ToString()).SetValue(access, dr(dtColumn.ToString()).ToString, Nothing)
            Catch ex As Exception

            End Try
        Next

        Return access
    End Function
#End Region

#Region "CLASS"
    Partial Public Class TabProfiles
        <DataColumnMapping("TabID")>
        Property TabID As Integer

        <DataColumnMapping("RoleID")>
        Property RoleID As String

        <DataColumnMapping("SubMenuID")>
        Property SubMenuID As Integer

        Property TabName As String

        <DataColumnMapping("CanInsert")>
        Property CanInsert As String

        <DataColumnMapping("CanUpdate")>
        Property CanUpdate As String

        <DataColumnMapping("CanDelete")>
        Property CanDelete As String

        <DataColumnMapping("CanPrint")>
        Property CanPrint As String

        <DataColumnMapping("CanApprove")>
        Property CanApprove As String
    End Class
#End Region
End Class

