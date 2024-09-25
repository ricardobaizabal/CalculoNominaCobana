Public Class EditorTablaISPTMensual
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request("id") Is Nothing Then
                registroId.Value = 0
            Else
                registroId.Value = Request.QueryString("id").ToString

                Dim cTarifaMensual As New Entities.TarifaMensual
                cTarifaMensual.IdTarifa = registroId.Value
                cTarifaMensual.ConsultarTarifaMensualID()
                If cTarifaMensual.IdTarifa > 0 Then
                    registroId.Value = cTarifaMensual.IdTarifa
                    txtLimiteinferior.Text = cTarifaMensual.LimiteInferior
                    txtLimitesuperior.Text = cTarifaMensual.LimiteSuperior
                    txtCuotafija.Text = cTarifaMensual.CuotaFija
                    txtporclimiteinf.Text = cTarifaMensual.PorcSobreExcli
                End If
            End If
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim cTarifaMensual As New Entities.TarifaMensual

        If registroId.Value > 0 Then
            cTarifaMensual.IdTarifa = registroId.Value
            cTarifaMensual.LimiteInferior = txtLimiteinferior.Text
            cTarifaMensual.LimiteSuperior = txtLimitesuperior.Text
            cTarifaMensual.CuotaFija = txtCuotafija.Text
            cTarifaMensual.PorcSobreExcli = txtporclimiteinf.Text
            cTarifaMensual.UpdateTarifaMensual()
        Else
            cTarifaMensual.LimiteInferior = txtLimiteinferior.Text
            cTarifaMensual.LimiteSuperior = txtLimitesuperior.Text
            cTarifaMensual.CuotaFija = txtCuotafija.Text
            cTarifaMensual.PorcSobreExcli = txtporclimiteinf.Text
            cTarifaMensual.GuadarTarifaMensual()
        End If
        cTarifaMensual = Nothing
        resetControles()
        Response.Redirect("~/TablaISPTMensual.aspx")
    End Sub
    Private Sub resetControles()
        txtLimiteinferior.Text = ""
        txtLimitesuperior.Text = ""
        txtCuotafija.Text = ""
        txtporclimiteinf.Text = ""
        registroId.Value = 0
    End Sub
End Class