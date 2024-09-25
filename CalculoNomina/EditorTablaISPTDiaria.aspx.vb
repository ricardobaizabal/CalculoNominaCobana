Public Class EditorTablaISPTDiaria
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request("id") Is Nothing Then
                registroId.Value = 0
            Else
                registroId.Value = Request.QueryString("id").ToString

                Dim cTarifaDiaria As New Entities.TarifaDiaria
                cTarifaDiaria.IdTarifa = registroId.Value
                cTarifaDiaria.ConsultarTarifaDiariaID()
                If cTarifaDiaria.IdTarifa > 0 Then
                    registroId.Value = cTarifaDiaria.IdTarifa
                    txtLimiteinferior.Text = cTarifaDiaria.LimiteInferior
                    txtLimitesuperior.Text = cTarifaDiaria.LimiteSuperior
                    txtCuotafija.Text = cTarifaDiaria.CuotaFija
                    txtporclimiteinf.Text = cTarifaDiaria.PorcSobreExcli
                End If
            End If
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim cTarifaDiaria As New Entities.TarifaDiaria

        If registroId.Value > 0 Then
            cTarifaDiaria.IdTarifa = registroId.Value
            cTarifaDiaria.LimiteInferior = txtLimiteinferior.Text
            cTarifaDiaria.LimiteSuperior = txtLimitesuperior.Text
            cTarifaDiaria.CuotaFija = txtCuotafija.Text
            cTarifaDiaria.PorcSobreExcli = txtporclimiteinf.Text
            cTarifaDiaria.UpdateTarifaDiaria()
        Else
            cTarifaDiaria.LimiteInferior = txtLimiteinferior.Text
            cTarifaDiaria.LimiteSuperior = txtLimitesuperior.Text
            cTarifaDiaria.CuotaFija = txtCuotafija.Text
            cTarifaDiaria.PorcSobreExcli = txtporclimiteinf.Text
            cTarifaDiaria.GuadarTarifaDiaria()
        End If
        cTarifaDiaria = Nothing
        resetControles()
        Response.Redirect("~/TablaISPTDiaria.aspx")
    End Sub
    Private Sub resetControles()
        txtLimiteinferior.Text = ""
        txtLimitesuperior.Text = ""
        txtCuotafija.Text = ""
        txtporclimiteinf.Text = ""
        registroId.Value = 0
    End Sub
End Class