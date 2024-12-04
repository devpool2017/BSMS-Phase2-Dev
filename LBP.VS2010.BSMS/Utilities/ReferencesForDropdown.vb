Imports System.Configuration.ConfigurationManager


<Serializable()>
Public Class ReferencesForDropdown

    Property value As String
    Property description As String
    Property data As String
    'Property loadCurrentValue As String = String.Empty
    'Property listOfReference As List(Of ReferencesForDropdown)

    Shared Function convertToReferencesList(ByVal dt As DataTable, ByVal ddlValue As String, ByVal ddlText As String, Optional pleaseSelect As Boolean = False,
                                            Optional pleaseSelectText As String = "", Optional combineData As Boolean = False, Optional additionalData As String = "") As List(Of ReferencesForDropdown)
        Dim listRfd As New List(Of ReferencesForDropdown)

        For Each row As DataRow In dt.Rows
            listRfd.Add(New ReferencesForDropdown With {
                .value = row(ddlValue).ToString.Trim,
                .description = If(Not combineData, row(ddlText).ToString.Trim, .value + " - " + row(ddlText).ToString.Trim),
                .data = If(Not String.IsNullOrWhiteSpace(additionalData), row(additionalData).ToString.Trim, String.Empty)
            })
        Next
        If listRfd.Count < 11 Then
            If pleaseSelect Then
                listRfd.Insert(0, New ReferencesForDropdown With {
                    .description = If(Not String.IsNullOrWhiteSpace(pleaseSelectText), pleaseSelectText, GetSection("Commons")("ListItemSelect").ToString()),
                    .value = String.Empty
                })
            End If
        End If

        Return listRfd
    End Function
End Class
