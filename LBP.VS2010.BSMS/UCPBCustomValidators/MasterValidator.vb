Imports System.ComponentModel.DataAnnotations
Imports LBP.VS2010.BSMS.Utilities



Public Class MasterValidator
    Inherits Attribute


    Sub New()

    End Sub

    Sub New(ByVal fieldDisplay As String)
        fieldName = fieldDisplay
    End Sub

    Property errMsg As String
    Property fieldName As String

    Property fieldControlVariable As PageControlVariables.FieldVariables

    Public Class DisplayLabel
        Inherits MasterValidator

        Sub New(ByVal objFieldName As String)
            MyBase.New(objFieldName)
        End Sub
       
    End Class

    Public Class ControlVariable
        Inherits MasterValidator

        Sub New(ByVal objFieldControlVariable As PageControlVariables.FieldVariables)
            MyBase.fieldControlVariable = objFieldControlVariable
        End Sub
    End Class

    Public Class DataColumnMapping
        Inherits MasterValidator

        Property columnField As String

        Sub New(ByVal objColumnField As String)
            columnField = objColumnField
        End Sub
    End Class

    Public Class DeclaredClass
        Inherits MasterValidator

    End Class


End Class
