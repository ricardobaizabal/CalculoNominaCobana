Imports Telerik.Web.UI

Public Class RegimenContratacion
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack Then
        End If
    End Sub
    Private Sub GridRegimen_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridRegimen.NeedDataSource
        Dim cRegimen As New Entities.RegimenContratacion
        GridRegimen.DataSource = cRegimen.ConsultarRegimen()
        cRegimen = Nothing
    End Sub
    Private Sub CargarGrid()
        Dim cRegimen As New Entities.RegimenContratacion
        GridRegimen.DataSource = cRegimen.ConsultarRegimen()
        GridRegimen.DataBind()
        cRegimen = Nothing
    End Sub
    'Private Sub GridRegimen_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridRegimen.ItemCommand
    '    Select Case e.CommandName
    '        Case "cmdEdit"
    '            Response.Redirect("~/EditorRegimenContratacion.aspx?id=" & e.CommandArgument)
    '        Case "cmdDelete"
    '            Dim cRegimen As New Entities.RegimenContratacion
    '            cRegimen.IdRegimenContratacion = e.CommandArgument
    '            cRegimen.EliminarRegimen()
    '            Call CargarGrid()
    '    End Select
    'End Sub
    'Private Sub GridRegimen_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridRegimen.ItemDataBound
    '    Select Case e.Item.ItemType
    '        Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
    '            Dim eliminar As ImageButton = CType(e.Item.FindControl("eliminar"), ImageButton)
    '            eliminar.Attributes.Add("onclick", "javascript:return confirm('Va a borrar un registro. ¿Desea continuar?');")
    '    End Select
    'End Sub
End Class