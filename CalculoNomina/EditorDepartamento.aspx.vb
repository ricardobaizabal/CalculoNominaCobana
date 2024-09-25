Public Class EditorDepartamento
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request("id") Is Nothing Then
                registroId.Value = 0
            Else
                registroId.Value = Request.QueryString("id").ToString

                Dim cDepartamento As New Entities.Departamento
                cDepartamento.IdDepartamento = registroId.Value
                cDepartamento.ConsultarDepartamentoID()
                If cDepartamento.IdDepartamento > 0 Then
                    registroId.Value = cDepartamento.IdDepartamento
                    txtDescripcion.Text = cDepartamento.Descripcion
                End If
            End If
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim cDepartamento As New Entities.Departamento

        If registroId.Value > 0 Then
            cDepartamento.IdDepartamento = registroId.Value
            cDepartamento.Descripcion = txtDescripcion.Text.Trim
            cDepartamento.UpdateDepartamento()
        Else
            cDepartamento.Descripcion = txtDescripcion.Text.Trim
            cDepartamento.GuadarDepartamento()
        End If
        cDepartamento = Nothing
        Response.Redirect("~/Departamento.aspx")
        'resetControles()
    End Sub
    Private Sub resetControles()
        txtDescripcion.Text = ""
        registroId.Value = 0
    End Sub
End Class