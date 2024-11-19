Imports Telerik.Web.UI

Public Class Departamento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim objCat As New DataControl()
            Dim cConcepto As New Entities.Catalogos
            If Request("cid") Is Nothing Then
                objCat.CatalogoRad(cmbCliente, cConcepto.ConsultarMisClientes, True, False)
            Else
                objCat.CatalogoRad(cmbCliente, cConcepto.ConsultarMisClientes, True, False)
                cmbCliente.SelectedValue = Request("cid")

                Call CargarGrid()

            End If
            objCat = Nothing
        End If
    End Sub
    Private Sub CargarGrid()
        Dim cDepartamento As New Entities.Departamento
        cDepartamento.clienteid = cmbCliente.SelectedValue
        GridDepartamento.DataSource = cDepartamento.ConsultarDepartamento()
        GridDepartamento.DataBind()
        cDepartamento = Nothing
    End Sub
    Private Sub GridDepartamento_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridDepartamento.NeedDataSource
        Dim cDepartamento As New Entities.Departamento
        cDepartamento.clienteid = cmbCliente.SelectedValue
        GridDepartamento.DataSource = cDepartamento.ConsultarDepartamento
        cDepartamento = Nothing
    End Sub
    Private Sub GridDepartamento_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridDepartamento.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim eliminar As ImageButton = CType(e.Item.FindControl("eliminar"), ImageButton)
                eliminar.Attributes.Add("onclick", "javascript:return confirm('Va a borrar un registro. ¿Desea continuar?');")
                'eliminar.Visible = e.Item.DataItem("eliminabit")
        End Select
    End Sub
    Private Sub GridDepartamento_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridDepartamento.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/EditorDepartamento.aspx?id=" & e.CommandArgument)
            Case "cmdDelete"
                Dim cDepartamento As New Entities.Departamento
                cDepartamento.IdDepartamento = e.CommandArgument
                cDepartamento.EliminarDepartamento()
                Call CargarGrid()
        End Select
    End Sub
    Private Sub cmbCliente_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cmbCliente.SelectedIndexChanged
        Call CargarGrid()
    End Sub
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim cDepartamento As New Entities.Departamento
        cDepartamento.clienteid = cmbCliente.SelectedValue
        cDepartamento.Descripcion = txtDepartamento.Text.Trim.ToUpper
        cDepartamento.GuadarDepartamento()

        txtDepartamento.Text = ""

        Call CargarGrid()

    End Sub

End Class