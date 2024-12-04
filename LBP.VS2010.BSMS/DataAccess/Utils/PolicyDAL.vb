Public Class PolicyDAL
    Inherits MasterDAL

#Region "DECLARATION"
    Dim message As String

    ReadOnly Property errorMessage As String
        Get
            Return message
        End Get
    End Property
#End Region

#Region "INSTANTIATION/NEW"
    ' ALWAYS OVERRIDE THE MODULENAME PER DAL
    Sub New()
        MyBase.moduleName = "System Menu"
    End Sub
#End Region
#Region "FUNCTIONS"
    Public Function ListSystemMenu(ByVal ProfileID As String) As DataTable

        Dim dt As New DataTable
        dt.Columns.Add("Policy_FK")
        dt.Columns.Add("SubMenu_FK")
        dt.Columns.Add("CanView")
        dt.Columns.Add("CanInsert")
        dt.Columns.Add("CanUpdate")
        dt.Columns.Add("CanDelete")
        dt.Columns.Add("CanPrint")
        dt.Columns.Add("CanValidate")
        dt.Columns.Add("CanConsolidate")
        dt.Columns.Add("IsAdmin")

        Dim dr1 As DataRow = dt.NewRow()
        Dim dr2 As DataRow = dt.NewRow()
        Dim dr3 As DataRow = dt.NewRow()
        Dim dr4 As DataRow = dt.NewRow()
        Dim dr5 As DataRow = dt.NewRow()
        Dim dr6 As DataRow = dt.NewRow()

        dr1("Policy_FK") = "1"
        dr1("SubMenu_FK") = "1"
        dr1("CanView") = "Y"
        dr1("CanInsert") = "Y"
        dr1("CanUpdate") = "Y"
        dr1("CanDelete") = "Y"
        dr1("CanPrint") = "Y"
        dr1("CanValidate") = "Y"
        dr1("CanConsolidate") = "Y"
        dr1("IsAdmin") = "Y"

        dt.Rows.Add(dr1)

        dr2("Policy_FK") = "2"
        dr2("SubMenu_FK") = "2"
        dr2("CanView") = "Y"
        dr2("CanInsert") = "Y"
        dr2("CanUpdate") = "Y"
        dr2("CanDelete") = "Y"
        dr2("CanPrint") = "Y"
        dr2("CanValidate") = "Y"
        dr2("CanConsolidate") = "Y"
        dr2("IsAdmin") = "Y"

        dt.Rows.Add(dr2)

        dr3("Policy_FK") = "3"
        dr3("SubMenu_FK") = "3"
        dr3("CanView") = "Y"
        dr3("CanInsert") = "Y"
        dr3("CanUpdate") = "Y"
        dr3("CanDelete") = "Y"
        dr3("CanPrint") = "Y"
        dr3("CanValidate") = "Y"
        dr3("CanConsolidate") = "Y"
        dr3("IsAdmin") = "Y"

        dt.Rows.Add(dr3)

        dr4("Policy_FK") = "4"
        dr4("SubMenu_FK") = "4"
        dr4("CanView") = "Y"
        dr4("CanInsert") = "Y"
        dr4("CanUpdate") = "Y"
        dr4("CanDelete") = "Y"
        dr4("CanPrint") = "Y"
        dr4("CanValidate") = "Y"
        dr4("CanConsolidate") = "Y"
        dr4("IsAdmin") = "Y"

        dt.Rows.Add(dr4)

        dr5("Policy_FK") = "5"
        dr5("SubMenu_FK") = "5"
        dr5("CanView") = "Y"
        dr5("CanInsert") = "Y"
        dr5("CanUpdate") = "Y"
        dr5("CanDelete") = "Y"
        dr5("CanPrint") = "Y"
        dr5("CanValidate") = "Y"
        dr5("CanConsolidate") = "Y"
        dr5("IsAdmin") = "Y"

        dt.Rows.Add(dr5)

        dr6("Policy_FK") = "6"
        dr6("SubMenu_FK") = "6"
        dr6("CanView") = "Y"
        dr6("CanInsert") = "Y"
        dr6("CanUpdate") = "N"
        dr6("CanDelete") = "N"
        dr6("CanPrint") = "Y"
        dr6("CanValidate") = "Y"
        dr6("CanConsolidate") = "Y"
        dr6("IsAdmin") = "Y"

        dt.Rows.Add(dr6)

        Return dt
    End Function
#End Region
End Class
