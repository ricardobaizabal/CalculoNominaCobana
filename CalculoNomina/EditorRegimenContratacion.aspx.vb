Public Class EditorRegimenContratacion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request("id") Is Nothing Then
                registroId.Value = 0
            Else
                registroId.Value = Request.QueryString("id").ToString

                Dim cRegimen As New Entities.RegimenContratacion
                cRegimen.IdRegimenContratacion = registroId.Value
                cRegimen.ConsultarRegimenID()
                If cRegimen.IdRegimenContratacion > 0 Then
                    registroId.Value = cRegimen.IdRegimenContratacion
                    txtDescripcion.Text = cRegimen.Descripcion
                End If
            End If
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim cRegimen As New Entities.RegimenContratacion

        If registroId.Value > 0 Then
            cRegimen.IdRegimenContratacion = registroId.Value
            cRegimen.Descripcion = txtDescripcion.Text.Trim
            cRegimen.UpdateRegimenContratacion()
        Else
            cRegimen.Descripcion = txtDescripcion.Text.Trim
            cRegimen.GuadarRegimenContratacion()
        End If
        cRegimen = Nothing
        resetControles()
        Response.Redirect("~/RegimenContratacion.aspx")
    End Sub
    Private Sub resetControles()
        txtDescripcion.Text = ""
        registroId.Value = 0
    End Sub
End Class