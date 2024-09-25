Imports Telerik.Web.UI

Public Class Conceptos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    'Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    '    Dim cConcepto As New Entities.Concepto

    '    If registroId.Value > 0 Then
    '        cConcepto.IdConcepto = registroId.Value
    '        cConcepto.Concepto = txtConcepto.Text.Trim
    '        cConcepto.Clave = txtClave.Text.Trim
    '        cConcepto.Tipo = txtTipo.Text.Trim
    '        cConcepto.UpdateConcepto()
    '    Else
    '        cConcepto.Concepto = txtConcepto.Text.Trim
    '        cConcepto.Clave = txtClave.Text.Trim
    '        cConcepto.Tipo = txtTipo.Text.Trim
    '        cConcepto.GuadarConcepto()
    '    End If
    '    cConcepto = Nothing
    '    resetControles()
    '    CargarGrid()
    'End Sub
    'Private Sub resetControles()
    '    txtConcepto.Text = ""
    '    txtClave.Text = ""
    '    txtTipo.Text = ""
    '    registroId.Value = 0
    'End Sub
    Private Sub CargarGrid()
        Dim cConcepto As New Entities.Concepto
        GridConcepto.DataSource = cConcepto.ConsultarConcepto()
        GridConcepto.DataBind()
        cConcepto = Nothing
    End Sub
    Private Sub GridConcepto_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridConcepto.NeedDataSource
        Dim cConcepto As New Entities.Concepto
        GridConcepto.DataSource = cConcepto.ConsultarConcepto
        cConcepto = Nothing
    End Sub
    Private Sub GridConcepto_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridConcepto.ItemDataBound
        'Select Case e.Item.ItemType
        '    Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
        '        Dim eliminar As ImageButton = CType(e.Item.FindControl("eliminar"), ImageButton)
        '        eliminar.Attributes.Add("onclick", "javascript:return confirm('Va a borrar un registro. ¿Desea continuar?');")
        'End Select
    End Sub
    Private Sub GridConcepto_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridConcepto.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/EditorConceptos.aspx?id=" & e.CommandArgument)
                'Dim cConcepto As New Entities.Concepto
                'cConcepto.IdConcepto = e.CommandArgument
                'cConcepto.ConsultarConceptoID()
                'If cConcepto.IdConcepto > 0 Then
                '    registroId.Value = cConcepto.IdConcepto
                '    txtConcepto.Text = cConcepto.Concepto
                '    txtClave.Text = cConcepto.Clave
                '    txtTipo.Text = cConcepto.Tipo
                'End If
                'resetControles()
            Case "cmdDelete"
                Dim cConcepto As New Entities.Concepto
                cConcepto.Cvo = e.CommandArgument
                cConcepto.EliminarConcepto()
                Call CargarGrid()
        End Select
    End Sub
End Class