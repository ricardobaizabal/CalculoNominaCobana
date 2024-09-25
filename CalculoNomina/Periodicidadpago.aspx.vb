Imports Telerik.Web.UI

Public Class Periodicidadpago
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    'Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    '    Dim cPeriodicidad As New Entities.Periodicidadpago

    '    If registroId.Value > 0 Then
    '        cPeriodicidad.IdPeriodicidad = registroId.Value
    '        cPeriodicidad.Descripcion = txtDescripcion.Text.Trim
    '        cPeriodicidad.UpdatePeriodicidadpago()
    '    Else
    '        cPeriodicidad.Descripcion = txtDescripcion.Text.Trim
    '        cPeriodicidad.GuadarPeriodicidadpago()
    '    End If
    '    cPeriodicidad = Nothing
    '    resetControles()
    '    CargarGrid()
    'End Sub
    'Private Sub resetControles()
    '    txtDescripcion.Text = ""
    '    registroId.Value = 0
    'End Sub

    Private Sub CargarGrid()
        Dim cPeriodicidad As New Entities.Periodicidadpago
        GridPeriodicidadpago.DataSource = cPeriodicidad.ConsultarPeriodicidadpago()
        GridPeriodicidadpago.DataBind()
        cPeriodicidad = Nothing
    End Sub
    Private Sub GridPeriodicidadpago_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridPeriodicidadpago.NeedDataSource
        Dim cPeriodicidad As New Entities.Periodicidadpago
        GridPeriodicidadpago.DataSource = cPeriodicidad.ConsultarPeriodicidadpago
        cPeriodicidad = Nothing
    End Sub
    'Private Sub GridPeriodicidadpago_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridPeriodicidadpago.ItemCommand
    '    Select Case e.CommandName
    '        Case "cmdEdit"
    '            Response.Redirect("~/EditorPeriodicidadP.aspx?id=" & e.CommandArgument)
    '        Case "cmdDelete"
    '            Dim cPeriodicidad As New Entities.Periodicidadpago
    '            cPeriodicidad.IdPeriodicidad = e.CommandArgument
    '            cPeriodicidad.EliminarPeriodicidadpago()
    '            Call CargarGrid()
    '    End Select
    'End Sub
    'Private Sub GridPeriodicidadpago_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridPeriodicidadpago.ItemDataBound
    '    Select Case e.Item.ItemType
    '        Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
    '            Dim eliminar As ImageButton = CType(e.Item.FindControl("eliminar"), ImageButton)
    '            eliminar.Attributes.Add("onclick", "javascript:return confirm('Va a borrar un registro. ¿Desea continuar?');")
    '    End Select
    'End Sub
End Class