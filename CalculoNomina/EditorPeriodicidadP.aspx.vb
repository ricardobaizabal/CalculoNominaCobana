Public Class EditorPeriodicidadP
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request("id") Is Nothing Then
            registroId.Value = 0
        Else
            registroId.Value = Request.QueryString("id").ToString

            Dim cPeriodicidad As New Entities.Periodicidadpago
            cPeriodicidad.IdPeriodicidad = registroId.Value
            cPeriodicidad.ConsultarPeriodicidadpagoID()
            If cPeriodicidad.IdPeriodicidad > 0 Then
                registroId.Value = cPeriodicidad.IdPeriodicidad
                txtDescripcion.Text = cPeriodicidad.Descripcion
            End If
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim cPeriodicidad As New Entities.Periodicidadpago

        If registroId.Value > 0 Then
            cPeriodicidad.IdPeriodicidad = registroId.Value
            cPeriodicidad.Descripcion = txtDescripcion.Text.Trim
            cPeriodicidad.UpdatePeriodicidadpago()
        Else
            cPeriodicidad.Descripcion = txtDescripcion.Text.Trim
            cPeriodicidad.GuadarPeriodicidadpago()
        End If
        cPeriodicidad = Nothing
        resetControles()
        Response.Redirect("~/Periodicidadpago.aspx")
        'CargarGrid()
    End Sub
    Private Sub resetControles()
        txtDescripcion.Text = ""
        registroId.Value = 0
    End Sub
End Class