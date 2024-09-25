Imports Telerik.Web.UI

Public Class TipoContrato
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    'Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    '    Dim cTipoContrato As New Entities.TipoContrato

    '    If registroId.Value > 0 Then
    '        cTipoContrato.IdTipoContrato = registroId.Value
    '        cTipoContrato.Descripcion = txtDescripcion.Text.Trim
    '        cTipoContrato.UpdateTipoContrato()
    '    Else
    '        cTipoContrato.Descripcion = txtDescripcion.Text.Trim
    '        cTipoContrato.GuadarTipoContrato()
    '    End If
    '    cTipoContrato = Nothing
    '    resetControles()
    '    CargarGrid()
    'End Sub
    'Private Sub resetControles()
    '    txtDescripcion.Text = ""
    '    registroId.Value = 0
    'End Sub
    Private Sub CargarGrid()
        Dim cTipoContrato As New Entities.TipoContrato
        GridTipoContrato.DataSource = cTipoContrato.ConsultarTipoContrato()
        GridTipoContrato.DataBind()
        cTipoContrato = Nothing
    End Sub
    Private Sub GridTipoContrato_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridTipoContrato.NeedDataSource
        Dim cTipoContrato As New Entities.TipoContrato
        GridTipoContrato.DataSource = cTipoContrato.ConsultarTipoContrato
        cTipoContrato = Nothing
    End Sub
    'Private Sub GridTipoContrato_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridTipoContrato.ItemDataBound
    '    Select Case e.Item.ItemType
    '        Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
    '            Dim eliminar As ImageButton = CType(e.Item.FindControl("eliminar"), ImageButton)
    '            eliminar.Attributes.Add("onclick", "javascript:return confirm('Va a borrar un registro. ¿Desea continuar?');")
    '    End Select
    'End Sub
    'Private Sub GridTipoContrato_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridTipoContrato.ItemCommand
    '    Select Case e.CommandName
    '        Case "cmdEdit"
    '            Response.Redirect("~/EditorTipoContrato.aspx?id=" & e.CommandArgument)
    '            'resetControles()
    '        Case "cmdDelete"
    '            Dim cTipoContrato As New Entities.TipoContrato
    '            cTipoContrato.IdTipoContrato = e.CommandArgument
    '            cTipoContrato.EliminarTipoContrato()
    '            Call CargarGrid()
    '    End Select
    'End Sub
End Class