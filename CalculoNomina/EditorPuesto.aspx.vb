Public Class EditorPuesto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim objCat As New DataControl()
            Dim cConcepto As New Entities.Catalogos
            objCat.CatalogoRad(cmbCliente, cConcepto.ConsultarMisClientes, True, False)
            objCat = Nothing

            If Request("id") Is Nothing Then
                registroId.Value = 0
            Else
                registroId.Value = Request.QueryString("id").ToString

                Dim cPuesto As New Entities.Puesto
                cPuesto.IdPuesto = registroId.Value
                cPuesto.ConsultarPuestoID()
                If cPuesto.IdPuesto > 0 Then
                    registroId.Value = cPuesto.IdPuesto
                    cmbCliente.SelectedValue = cPuesto.clienteid
                    txtPuesto.Text = cPuesto.Descripcion
                End If
            End If
        End If
    End Sub
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim cPuesto As New Entities.Puesto
        If registroId.Value > 0 Then
            cPuesto.IdPuesto = registroId.Value
            cPuesto.clienteid = cmbCliente.SelectedValue
            cPuesto.Descripcion = txtPuesto.Text.Trim.ToUpper
            cPuesto.UpdatePuesto()
        Else
            cPuesto.Descripcion = txtPuesto.Text.Trim.ToUpper
            cPuesto.GuadarPuesto()
        End If
        cPuesto = Nothing
        Response.Redirect("~/Puesto.aspx?cid=" & cmbCliente.SelectedValue)
    End Sub
    Private Sub resetControles()
        txtPuesto.Text = ""
        cmbCliente.SelectedValue = 0
        registroId.Value = 0
    End Sub

End Class