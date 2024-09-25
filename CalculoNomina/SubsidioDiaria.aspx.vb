Imports Telerik.Web.UI

Public Class SubsidioDiaria
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Response.Redirect("~/EditorSubsidioDiaria.aspx")
    End Sub
    Private Sub GridSubsidioDiaria_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridSubsidioDiaria.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/EditorSubsidioDiaria.aspx?id=" & e.CommandArgument)
        End Select
    End Sub
    'Private Sub GridSubsidioDiaria_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridSubsidioDiaria.NeedDataSource
    '    Dim cTablaSubsidioDiario As New Entities.TablaSubsidioDiario
    '    GridSubsidioDiaria.DataSource = cTablaSubsidioDiario.ConsultarSubsidioDiario
    '    cTablaSubsidioDiario = Nothing
    'End Sub

    Protected Sub GridSubsidioDiaria_ItemUpdated(source As Object, e As Telerik.Web.UI.GridUpdatedEventArgs) Handles GridSubsidioDiaria.ItemUpdated
        Dim item As GridEditableItem = DirectCast(e.Item, GridEditableItem)
        Dim id As [String] = item.GetDataKeyValue("IdTablaSubsidio").ToString()

        If e.Exception IsNot Nothing Then
            e.KeepInEditMode = True
            e.ExceptionHandled = True
            MsgBox("Product with ID " + id + " cannot be updated. Reason: " + e.Exception.Message)
        Else

        End If
    End Sub

End Class