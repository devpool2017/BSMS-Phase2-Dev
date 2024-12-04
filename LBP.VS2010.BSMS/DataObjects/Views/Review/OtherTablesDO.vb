Imports LBP.VS2010.BSMS.DataAccess
Imports LBP.VS2010.BSMS.Utilities.Messages.ErrorMessages
Imports LBP.VS2010.BSMS.CustomValidators.LengthAndValueValidator
Imports LBP.VS2010.BSMS.CustomValidators.MasterValidator
Imports LBP.VS2010.BSMS.CustomValidators
Imports System.Configuration.ConfigurationManager
Imports LBP.VS2010.BSMS.Utilities.PageControlVariables
Imports LBP.VS2010.BSMS.Utilities
Imports System.Configuration

Public Class OtherTablesDO
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

    <DataColumnMapping("TableColumn")>
    Property TableColumn As String

    Property tableName As String

#End Region


#Region "LOAD"
    Shared Function ddlIndustry() As DataTable
        Dim dal As New EncodingDAL
        Dim dt As New DataTable

        dt = dal.ddlIndustry()

        Return dt
    End Function

    Shared Function checkDDLValue(ByVal tableName As String) As List(Of OtherTablesDO)

        Dim dal As New OtherTablesDA
        Dim dt As New DataTable

        dt = dal.checkDDLValue(tableName)

        If Not IsNothing(dt) Then
            Dim lst As New List(Of OtherTablesDO)
            If dt.Rows.Count > 0 Then
                For Each dtRow As DataRow In dt.Rows
                    lst.Add(
                        New OtherTablesDO With {
                        .TableColumn = IIf(dtRow.Item("TableColumn").ToString = "", "", dtRow.Item("TableColumn"))
                        })
                Next
            End If
            Return lst
        End If

        Return Nothing
    End Function

#End Region
    
End Class
