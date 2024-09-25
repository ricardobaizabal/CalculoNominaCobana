Public Class AgregarPeriodosCatorcenales
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim cPeriodo As New Entities.Periodo

        Dim cEmpresa As New Entities.Empresa
        cEmpresa.IdEmpresa = Session("clienteid")
        cEmpresa.IdUsuario = Session("usuarioid")
        cEmpresa.ConsultarEjercicioID()
        If cEmpresa.IdUsuario > 0 Then
            'cPeriodo.IdEmpresa = Session("clienteid")
            cPeriodo.IdEjercicio = cEmpresa.IdEjercicio
            cPeriodo.IdTipoNomina = 2 'Catorcenal
            cPeriodo.FechaInicial = String.Format("{0:MM/dd/yyyy}", calFechaInicio.SelectedDate)
            cPeriodo.GeneraPeriodos = cmbGeneraPeriodos.SelectedValue
            cPeriodo.GuadarPeriodoCatorcenal()
        End If
        cEmpresa = Nothing
        cPeriodo = Nothing
        resetControles()
        Response.Redirect("~/PeriodosCatorcenales.aspx")
    End Sub
    Private Sub resetControles()
        calFechaInicio.Clear()
        registroId.Value = 0
    End Sub
End Class