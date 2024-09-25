Public Class EditorSubsidioMensual
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request("id") Is Nothing Then
                registroId.Value = 0
            Else
                registroId.Value = Request.QueryString("id").ToString

                Dim cTablaSubsidioMensual As New Entities.TablaSubsidioDiario
                cTablaSubsidioMensual.IdTablaSubsidio = registroId.Value
                cTablaSubsidioMensual.ConsultarSubsidioMensualID()
                If cTablaSubsidioMensual.IdTablaSubsidio > 0 Then
                    registroId.Value = cTablaSubsidioMensual.IdTablaSubsidio
                    txtLimiteinferior.Text = cTablaSubsidioMensual.LimiteInferior
                    txtLimitesuperior.Text = cTablaSubsidioMensual.LimiteSuperior
                    txtCuotafija.Text = cTablaSubsidioMensual.CuotaFija
                End If
            End If
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim cTablaSubsidioMensual As New Entities.TablaSubsidioDiario

        If registroId.Value > 0 Then
            cTablaSubsidioMensual.IdTablaSubsidio = registroId.Value
            cTablaSubsidioMensual.LimiteInferior = txtLimiteinferior.Text
            cTablaSubsidioMensual.LimiteSuperior = txtLimitesuperior.Text
            cTablaSubsidioMensual.CuotaFija = txtCuotafija.Text
            cTablaSubsidioMensual.UpdateSubsidioMensual()
        Else
            cTablaSubsidioMensual.LimiteInferior = txtLimiteinferior.Text
            cTablaSubsidioMensual.LimiteSuperior = txtLimitesuperior.Text
            cTablaSubsidioMensual.CuotaFija = txtCuotafija.Text
            cTablaSubsidioMensual.GuadarSubsidioMensual()
        End If
        cTablaSubsidioMensual = Nothing
        resetControles()
        Response.Redirect("~/SubsidioMensual.aspx")
    End Sub
    Private Sub resetControles()
        txtLimiteinferior.Text = ""
        txtLimitesuperior.Text = ""
        txtCuotafija.Text = ""
        registroId.Value = 0
    End Sub

End Class