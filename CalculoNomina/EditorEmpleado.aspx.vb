
Public Class EditorEmpleado
    Inherits System.Web.UI.Page
    Dim ObjData As New DataControl()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request("id") Is Nothing Then
                registroId.Value = 0
            Else
                registroId.Value = Request.QueryString("id").ToString

                Dim cEmpleado As New Entities.Empleado
                cEmpleado.IdEmpleado = registroId.Value
                cEmpleado.ConsultarEmpleadoID()
                If cEmpleado.IdEmpleado > 0 Then
                    registroId.Value = cEmpleado.IdEmpleado
                    txtClaveEmp.Text = cEmpleado.IdEmpleado
                    txtNombre.Text = cEmpleado.Nombre
                    txtDomicilio.Text = cEmpleado.Domicilio
                    txtColonia.Text = cEmpleado.Colonia
                    txtrfc.Text = cEmpleado.Rfc
                    txtCiudad.Text = cEmpleado.Ciudad
                    txtCurp.Text = cEmpleado.Curp
                    'txtEstado.Text = cEmpleado.IdEstado
                    txtImss.Text = cEmpleado.Imss
                    txtPais.Text = cEmpleado.Pais
                    txtTelefono.Text = cEmpleado.Telefono
                    txtCp.Text = cEmpleado.Cp
                    txtClabe.Text = cEmpleado.Clabe
                    txtCorreo.Text = cEmpleado.Correo

                    If cEmpleado.FechaIngreso.ToString.Length > 0 Then
                        calFechaIngreso.SelectedDate = CDate(cEmpleado.FechaIngreso)
                    End If
                    'calFechaIngreso.SelectedDate = cEmpleado.FechaIngreso
                    'llenaCombocmbRegimen(0)
                    llenaCombocmbTipoContrato(cEmpleado.IdTipoContrato)
                    llenaCombocmbPeriodicidad(cEmpleado.IdPeriodicidadpago)
                    llenaCombocmbPuesto(cEmpleado.IdPuesto)
                    llenaCombocmbRiesgo(cEmpleado.IdRiesgo)
                    llenaCombocmbBanco(cEmpleado.IdBanco)
                    llenaCombocmbRegimenContratacion(cEmpleado.IdRegimencontratacion)
                    llenaCombocmbMetodoPago(cEmpleado.IdMetodopago)
                    llenaCombocmbEstado(cEmpleado.IdEstado)
                    llenaCombocmbTipoJornada(cEmpleado.IdTipoJornada)
                    'llenaCombocmbTipoJornada(0)

                    txtSueldoDiario.Text = cEmpleado.CuotaDiaria
                    txtAsimiladoTotalS.Text = cEmpleado.Asimiladototalsemanal
                    txtFactorIntegracion.Text = "1.0452"
                    txtImptoCedular.Text = cEmpleado.Porcentajeimptocedularestatal
                    txtSueldoDiarioIntegrado.Text = cEmpleado.IntegradoImss
                    txtDescansosxSemana.Text = cEmpleado.Descansosxsemana
                    txtPagoxHora.Text = cEmpleado.PagoPorHora
                    txtIntegradoTipo.Text = cEmpleado.IntegradoTipo
                    txtFactorComision.Text = cEmpleado.FactorComision
                    txtPromCuotaV.Text = cEmpleado.PromedioCuotavariable
                    txtFactorDestajo.Text = cEmpleado.FactorDestajo
                    txtPromedioPercepV.Text = cEmpleado.PromedioPercepcionesvariables
                    txtHorasDia.Text = cEmpleado.HoraSaldia
                    txtCorreo.Text = cEmpleado.Correo

                    txtClabe.Text = cEmpleado.NumClabe
                    txtNumCuenta.Text = cEmpleado.NumCuenta
                    'txtDescripcion.Text = cDepartamento.Descripcion
                End If
            End If
        End If
    End Sub
    Private Sub llenaCombocmbRegimen(ByVal sel As Integer)
        Dim cRegimen As New Entities.RegimenContratacion
        objData.Catalogo(cmbRegimen, sel, cRegimen.MostrarRegimen())
        cRegimen = Nothing
    End Sub
    Private Sub llenaCombocmbTipoContrato(ByVal sel As Integer)
        Dim cContrato As New Entities.TipoContrato
        objData.Catalogo(cmbTipoContrato, sel, cContrato.ConsultarTipoContrato())
        cContrato = Nothing
    End Sub
    Private Sub llenaCombocmbPeriodicidad(ByVal sel As Integer)
        Dim cPeriodicidad As New Entities.Periodicidadpago
        objData.Catalogo(cmbPeriodicidadP, sel, cPeriodicidad.ConsultarPeriodicidadpago())
        cPeriodicidad = Nothing
    End Sub
    Private Sub llenaCombocmbPuesto(ByVal sel As Integer)
        Dim cPuesto As New Entities.Puesto
        objData.Catalogo(cmbPuesto, sel, cPuesto.ConsultarPuesto())
        cPuesto = Nothing
    End Sub
    Private Sub llenaCombocmbRiesgo(ByVal sel As Integer)
        Dim cRPuesto As New Entities.RiesgoPuesto
        objData.Catalogo(cmbRiesgo, sel, cRPuesto.ConsultarRiesgoPuesto())
        cRPuesto = Nothing
    End Sub
    Private Sub llenaCombocmbBanco(ByVal sel As Integer)
        Dim cBanco As New Entities.Banco
        objData.Catalogo(cmbBanco, sel, cBanco.ConsultarBanco())
        cBanco = Nothing
    End Sub
    Private Sub llenaCombocmbRegimenContratacion(ByVal sel As Integer)
        Dim cRegimen As New Entities.RegimenContratacion
        objData.Catalogo(cmbRegimen, sel, cRegimen.ConsultarRegimen())
        cRegimen = Nothing
    End Sub
    Private Sub llenaCombocmbMetodoPago(ByVal sel As Integer)
        Dim cMetodoP As New Entities.MetodoPago
        objData.Catalogo(cmbMetodoPago, sel, cMetodoP.ConsultarMetodoPago())
        cMetodoP = Nothing
    End Sub
    Private Sub llenaCombocmbEstado(ByVal sel As Integer)
        Dim cEstado As New Entities.Estado
        objData.Catalogo(cmbEstado, sel, cEstado.ConsultarEstado())
        cEstado = Nothing
    End Sub

    Private Sub llenaCombocmbTipoJornada(ByVal sel As Integer)
        Dim cTipoJornada As New Entities.TipoJornada
        objData.Catalogo(cmbTipoj, sel, cTipoJornada.ConsultarTipoJornada())
        cTipoJornada = Nothing
    End Sub
End Class