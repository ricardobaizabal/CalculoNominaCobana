Imports Telerik.Web.UI

Public Class Departamento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub CargarGrid()
        Dim cDepartamento As New Entities.Departamento
        cDepartamento.clienteid = Session("clienteid")
        GridDepartamento.DataSource = cDepartamento.ConsultarDepartamento()
        GridDepartamento.DataBind()
        cDepartamento = Nothing
    End Sub
    Private Sub GridDepartamento_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridDepartamento.NeedDataSource
        Dim cDepartamento As New Entities.Departamento
        cDepartamento.clienteid = Session("clienteid")
        GridDepartamento.DataSource = cDepartamento.ConsultarDepartamento
        cDepartamento = Nothing
    End Sub
    Private Sub GridDepartamento_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridDepartamento.ItemDataBound
        'Select Case e.Item.ItemType
        '    Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
        '        Dim eliminar As ImageButton = CType(e.Item.FindControl("eliminar"), ImageButton)
        '        eliminar.Attributes.Add("onclick", "javascript:return confirm('Va a borrar un registro. ¿Desea continuar?');")
        '        eliminar.Visible = e.Item.DataItem("eliminabit")
        'End Select
    End Sub
    Private Sub GridDepartamento_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridDepartamento.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/EditorDepartamento.aspx?id=" & e.CommandArgument)
                'Dim cDepartamento As New Entities.Departamento
                'cDepartamento.IdDepartamento = e.CommandArgument
                'cDepartamento.ConsultarDepartamentoID()
                'If cDepartamento.IdDepartamento > 0 Then
                '    registroId.Value = cDepartamento.IdDepartamento
                '    txtDescripcion.Text = cDepartamento.Descripcion
                'End If
                'resetControles()
                'Case "cmdDelete"
                '    Dim cDepartamento As New Entities.Departamento
                '    cDepartamento.IdDepartamento = e.CommandArgument
                '    cDepartamento.EliminarDepartamento()
                '    Call CargarGrid()
        End Select
    End Sub
End Class