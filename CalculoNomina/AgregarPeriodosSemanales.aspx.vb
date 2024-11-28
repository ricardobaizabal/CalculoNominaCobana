Public Class AgregarPeriodosSemanales
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objCat As New DataControl()
            Dim cConcepto As New Entities.Catalogos
            objCat.CatalogoRad(cmbCliente, cConcepto.ConsultarMisClientes, True, False)
            objCat = Nothing
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Dim cEmpresa As New Entities.Empresa
        cEmpresa.IdEmpresa = Session("IdEmpresa")
        cEmpresa.IdUsuario = Session("usuarioid")
        cEmpresa.ConsultarEjercicioID()

        If cEmpresa.IdUsuario > 0 Then
            Dim cPeriodo As New Entities.Periodo
            cPeriodo.IdEmpresa = Session("IdEmpresa")
            cPeriodo.IdCliente = cmbCliente.SelectedValue
            cPeriodo.IdEjercicio = cEmpresa.IdEjercicio
            cPeriodo.IdTipoNomina = 1 'Semanal
            cPeriodo.FechaInicial = String.Format("{0:MM/dd/yyyy}", calFechaInicio.SelectedDate)
            cPeriodo.GeneraPeriodos = cmbGeneraPeriodos.SelectedValue
            cPeriodo.GuadarPeriodoSemanal()
            cPeriodo = Nothing
        End If
        cEmpresa = Nothing
        resetControles()
        Response.Redirect("~/PeriodosSemanales.aspx")
    End Sub
    Private Sub resetControles()
        cmbCliente.SelectedValue = 0
        calFechaInicio.Clear()
        registroId.Value = 0
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("PeriodosSemanales.aspx")
    End Sub

End Class