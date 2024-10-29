Public Class AgregarPeriodosQuincenales
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim cEmpresa As New Entities.Empresa
        cEmpresa.IdUsuario = Session("usuarioid")
        cEmpresa.ConsultarEjercicioID()

        If cEmpresa.IdUsuario > 0 Then
            Dim cPeriodo As New Entities.Periodo
            cPeriodo.IdEmpresa = Session("IdEmpresa")
            cPeriodo.IdEjercicio = cEmpresa.IdEjercicio
            cPeriodo.IdTipoNomina = 3 'Quincenal
            cPeriodo.FechaInicial = String.Format("{0:MM/dd/yyyy}", calFechaInicio.SelectedDate)
            cPeriodo.GeneraPeriodos = cmbGeneraPeriodos.SelectedValue
            cPeriodo.GuadarPeriodoQuincenal()
            cPeriodo = Nothing
        End If
        cEmpresa = Nothing
        resetControles()
        Response.Redirect("~/PeriodosQuincenales.aspx")
    End Sub
    Private Sub resetControles()
        calFechaInicio.Clear()
        registroId.Value = 0
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("PeriodosQuincenales.aspx")
    End Sub

End Class