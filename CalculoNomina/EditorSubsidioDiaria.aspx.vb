Public Class EditorSubsidioDiaria
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request("id") Is Nothing Then
                registroId.Value = 0
            Else
                registroId.Value = Request.QueryString("id").ToString

                Dim cTablaSubsidioDiaria As New Entities.TablaSubsidioDiario
                cTablaSubsidioDiaria.IdTablaSubsidio = registroId.Value
                cTablaSubsidioDiaria.ConsultarSubsidioDiarioID()
                If cTablaSubsidioDiaria.IdTablaSubsidio > 0 Then
                    registroId.Value = cTablaSubsidioDiaria.IdTablaSubsidio
                    txtLimiteinferior.Text = cTablaSubsidioDiaria.LimiteInferior
                    txtLimitesuperior.Text = cTablaSubsidioDiaria.LimiteSuperior
                    txtCuotafija.Text = cTablaSubsidioDiaria.CuotaFija
                End If
            End If
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim cTablaSubsidioDiaria As New Entities.TablaSubsidioDiario

        If registroId.Value > 0 Then
            cTablaSubsidioDiaria.IdTablaSubsidio = registroId.Value
            cTablaSubsidioDiaria.LimiteInferior = txtLimiteinferior.Text
            cTablaSubsidioDiaria.LimiteSuperior = txtLimitesuperior.Text
            cTablaSubsidioDiaria.CuotaFija = txtCuotafija.Text
            cTablaSubsidioDiaria.UpdateSubsidioDiario()
        Else
            cTablaSubsidioDiaria.LimiteInferior = txtLimiteinferior.Text
            cTablaSubsidioDiaria.LimiteSuperior = txtLimitesuperior.Text
            cTablaSubsidioDiaria.CuotaFija = txtCuotafija.Text
            cTablaSubsidioDiaria.GuadarSubsidioDiario()
        End If
        cTablaSubsidioDiaria = Nothing
        resetControles()
        Response.Redirect("~/SubsidioDiaria.aspx")
    End Sub
    Private Sub resetControles()
        txtLimiteinferior.Text = ""
        txtLimitesuperior.Text = ""
        txtCuotafija.Text = ""
        registroId.Value = 0
    End Sub
End Class