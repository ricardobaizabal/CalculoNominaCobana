Public Class EditorRiesgoPuesto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request("id") Is Nothing Then
                registroId.Value = 0
            Else
                registroId.Value = Request.QueryString("id").ToString

                Dim cRiesgo As New Entities.RiesgoPuesto
                cRiesgo.IdRiesgoPuesto = registroId.Value
                cRiesgo.ConsultarRiesgoPuestoID()
                If cRiesgo.IdRiesgoPuesto > 0 Then
                    registroId.Value = cRiesgo.IdRiesgoPuesto
                    txtNombre.Text = cRiesgo.Nombre
                End If
            End If
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim cRiesgo As New Entities.RiesgoPuesto

        If registroId.Value > 0 Then
            cRiesgo.IdRiesgoPuesto = registroId.Value
            cRiesgo.Nombre = txtNombre.Text.Trim
            cRiesgo.UpdateRiesgoPuesto()
        Else
            cRiesgo.Nombre = txtNombre.Text.Trim
            cRiesgo.GuadarRiesgoPuesto()
        End If
        cRiesgo = Nothing
        resetControles()
        Response.Redirect("~/RiespoPuesto.aspx")
    End Sub
    Private Sub resetControles()
        txtNombre.Text = ""
        registroId.Value = 0
    End Sub
End Class