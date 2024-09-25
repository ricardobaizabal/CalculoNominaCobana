Imports Telerik.Web.UI
Imports Entities

Public Class ListadoNominaMensual
    Inherits System.Web.UI.Page
    Dim ObjData As New DataControl()
    Private IdEjercicio As Integer = 0
    Private dtEmpleados As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objCat As New DataControl(1)
            Dim cConcepto As New Entities.Catalogos
            objCat.Catalogo(cmbCliente, 0, cConcepto.ConsultarMisClientes, True)
            objCat.Catalogo(cmbPeriodicidad, 4, cConcepto.ConsultarPeriodoPago2, True)
            CargarGridNominas()
            cargaPeriodos(cmbPeriodicidad.SelectedValue)
            cmbPeriodicidad.Enabled = False
        End If
    End Sub

    Private Sub CargarGridNominas()
        Call CargarVariablesGenerales()
        Dim periodicidad, cliente, periodo As Integer

        If cmbPeriodicidad.SelectedValue.ToString() = "" Then
            periodicidad = 0
        Else
            periodicidad = cmbPeriodicidad.SelectedValue
        End If

        If cmbCliente.SelectedValue.ToString() = "" Then
            cliente = 0
        Else
            cliente = cmbCliente.SelectedValue
        End If

        If cmbPeriodo.SelectedValue.ToString() = "" Then
            periodo = 0
        Else
            periodo = cmbPeriodo.SelectedValue
        End If

        Dim dt_nominas As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = IdEjercicio
        cNomina.EsEspecial = False
        cNomina.TipoNomina = periodicidad
        cNomina.Periodo = periodo
        cNomina.Cliente = cliente
        GridNominas.DataSource = cNomina.ConsultarTodasNominaExtraordinaria()
        GridNominas.DataBind()
    End Sub

    Private Sub GridNominas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridNominas.NeedDataSource
        Call CargarVariablesGenerales()

        Dim periodicidad, cliente, periodo As Integer

        If cmbPeriodicidad.SelectedValue.ToString() = "" Then
            periodicidad = 0
        Else
            periodicidad = cmbPeriodicidad.SelectedValue
        End If

        If cmbCliente.SelectedValue.ToString() = "" Then
            cliente = 0
        Else
            cliente = cmbCliente.SelectedValue
        End If

        If cmbPeriodo.SelectedValue.ToString() = "" Then
            periodo = 0
        Else
            periodo = cmbPeriodo.SelectedValue
        End If

        Dim cNomina As New Nomina()
        cNomina.Ejercicio = IdEjercicio
        cNomina.EsEspecial = False
        cNomina.TipoNomina = periodicidad
        cNomina.Periodo = periodo
        cNomina.Cliente = cliente
        GridNominas.DataSource = cNomina.ConsultarTodasNominaExtraordinaria()
        cNomina = Nothing
    End Sub

    Private Sub GridNominas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridNominas.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Dim folio As String = e.CommandArgument.ToString()
                Session("Folio") = folio
                Response.Redirect("GeneracionDeNominaNormal.aspx")
        End Select
    End Sub

    Private Sub btnAgregarNominaE_Click(sender As Object, e As EventArgs) Handles btnAgregarNominaE.Click
        Response.Redirect("GeneracionDeNominaNormal.aspx")
    End Sub

    Private Sub btnBuscarNominas_Click(sender As Object, e As EventArgs) Handles btnBuscarNominas.Click
        CargarGridNominas()
    End Sub

    Private Sub cargaPeriodos(Optional ByVal IdTipoNomina As Integer = 0)
        Call CargarVariablesGenerales()
        Dim cPeriodo As New Entities.Periodo
        cPeriodo.IdEjercicio = IdEjercicio
        cPeriodo.IdTipoNomina = IdTipoNomina
        cPeriodo.ExtraordinarioBit = Nothing
        ObjData.Catalogo(cmbPeriodo, 0, cPeriodo.ConsultarPeriodos())
    End Sub

    Private Sub CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cConfiguracion = New Configuracion()
        cConfiguracion.IdUsuario = Session("usuarioid")
        dt = cConfiguracion.ConsultarConfiguracion()
        cConfiguracion = Nothing

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                IdEjercicio = oDataRow("IdEjercicio")
            Next
        End If
    End Sub

End Class