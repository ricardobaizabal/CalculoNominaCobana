Public Class EditorDepartamento
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

                Dim cDepartamento As New Entities.Departamento
                cDepartamento.IdDepartamento = registroId.Value
                cDepartamento.ConsultarDepartamentoID()
                If cDepartamento.IdDepartamento > 0 Then
                    registroId.Value = cDepartamento.IdDepartamento
                    cmbCliente.SelectedValue = cDepartamento.clienteid
                    txtDepartamento.Text = cDepartamento.Descripcion
                End If
            End If
        End If
    End Sub
    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim cDepartamento As New Entities.Departamento

        If registroId.Value > 0 Then
            cDepartamento.IdDepartamento = registroId.Value
            cDepartamento.clienteid = cmbCliente.SelectedValue
            cDepartamento.Descripcion = txtDepartamento.Text.Trim.ToUpper
            cDepartamento.UpdateDepartamento()
        Else
            cDepartamento.Descripcion = txtDepartamento.Text.Trim.ToUpper
            cDepartamento.GuadarDepartamento()
        End If
        cDepartamento = Nothing
        Response.Redirect("~/Departamento.aspx?cid=" & cmbCliente.SelectedValue)
    End Sub
    Private Sub resetControles()
        cmbCliente.SelectedValue = 0
        txtDepartamento.Text = ""
        registroId.Value = 0
    End Sub

End Class