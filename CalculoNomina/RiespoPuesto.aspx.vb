Imports Telerik.Web.UI

Public Class RiespoPuesto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub CargarGrid()
        Dim cRiesgo As New Entities.RiesgoPuesto
        GridRiesgoPuesto.DataSource = cRiesgo.ConsultarRiesgoPuesto
        GridRiesgoPuesto.DataBind()
        cRiesgo = Nothing
    End Sub
    Private Sub GridRiesgoPuesto_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridRiesgoPuesto.NeedDataSource
        Dim cRiesgo As New Entities.RiesgoPuesto
        GridRiesgoPuesto.DataSource = cRiesgo.ConsultarRiesgoPuesto
        cRiesgo = Nothing
    End Sub
    'Private Sub GridRiesgoPuesto_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridRiesgoPuesto.ItemCommand
    '    Select Case e.CommandName
    '        Case "cmdEdit"
    '            Response.Redirect("~/EditorRiesgoPuesto.aspx?id=" & e.CommandArgument)
    '        Case "cmdDelete"
    '            Dim cRiesgo As New Entities.RiesgoPuesto
    '            cRiesgo.IdRiesgoPuesto = e.CommandArgument
    '            cRiesgo.EliminarRiesgoPuesto()
    '            Call CargarGrid()
    '    End Select
    'End Sub
    'Private Sub GridRiesgoPuesto_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridRiesgoPuesto.ItemDataBound
    '    Select Case e.Item.ItemType
    '        Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
    '            Dim eliminar As ImageButton = CType(e.Item.FindControl("eliminar"), ImageButton)
    '            eliminar.Attributes.Add("onclick", "javascript:return confirm('Va a borrar un registro. ¿Desea continuar?');")
    '    End Select
    'End Sub
End Class