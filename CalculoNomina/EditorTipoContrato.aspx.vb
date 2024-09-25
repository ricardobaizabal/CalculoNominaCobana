Public Class EditorTipoContrato
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request("id") Is Nothing Then
                registroId.Value = 0
            Else
                registroId.Value = Request.QueryString("id").ToString

                Dim cTipoContrato As New Entities.TipoContrato
                cTipoContrato.IdTipoContrato = registroId.Value
                cTipoContrato.ConsultarTipoContratoID()
                If cTipoContrato.IdTipoContrato > 0 Then
                    registroId.Value = cTipoContrato.IdTipoContrato
                    txtDescripcion.Text = cTipoContrato.Descripcion
                End If
            End If
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim cTipoContrato As New Entities.TipoContrato

        If registroId.Value > 0 Then
            cTipoContrato.IdTipoContrato = registroId.Value
            cTipoContrato.Descripcion = txtDescripcion.Text.Trim
            cTipoContrato.UpdateTipoContrato()
        Else
            cTipoContrato.Descripcion = txtDescripcion.Text.Trim
            cTipoContrato.GuadarTipoContrato()
        End If
        cTipoContrato = Nothing
        resetControles()
        Response.Redirect("~/TipoContrato.aspx")
        'CargarGrid()
    End Sub
    Private Sub resetControles()
        txtDescripcion.Text = ""
        registroId.Value = 0
    End Sub
End Class