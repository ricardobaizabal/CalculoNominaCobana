Public Class EditorPuesto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request("id") Is Nothing Then
                registroId.Value = 0
            Else
                registroId.Value = Request.QueryString("id").ToString

                Dim cPuesto As New Entities.Puesto
                cPuesto.IdPuesto = registroId.Value
                cPuesto.ConsultarPuestoID()
                If cPuesto.IdPuesto > 0 Then
                    registroId.Value = cPuesto.IdPuesto
                    txtDescripcion.Text = cPuesto.Descripcion
                End If
            End If
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim cPuesto As New Entities.Puesto

        If registroId.Value > 0 Then
            cPuesto.IdPuesto = registroId.Value
            cPuesto.Descripcion = txtDescripcion.Text.Trim
            cPuesto.UpdatePuesto()
        Else
            cPuesto.Descripcion = txtDescripcion.Text.Trim
            cPuesto.GuadarPuesto()
        End If
        cPuesto = Nothing
        resetControles()
        Response.Redirect("~/Puesto.aspx")
        'CargarGrid()
    End Sub
    Private Sub resetControles()
        txtDescripcion.Text = ""
        registroId.Value = 0
    End Sub
End Class