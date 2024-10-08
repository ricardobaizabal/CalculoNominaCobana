Imports Telerik.Web.UI
Imports Entities
Public Class IncidenciasCatorcenal
    Inherits System.Web.UI.Page

    Private IdEmpresa As Integer = 0
    Private IdEjercicio As Integer = 0
    Private Periodo As Integer = 0

    Private CuotaPeriodo As Double
    Private PrimaDominical As Double
    Private PrimaVacacional As Double
    Private Vacaciones As Double
    Private Aguinaldo As Double
    Private RepartoUtilidades As Double
    Private FondoAhorro As Double
    Private AyudaFuneral As Double
    Private PrevisionSocial As Double
    Private GrupoPercepcionesGravadasTotalmenteSinExentos As Double
    Private PagoPorHoras As Double
    Private Comisiones As Double
    Private Destajo As Double
    Private FaltasPermisosIncapacidades As Double
    Private NumeroDeDiasPagados As Double

    Private ImporteExentoPrimaDominical As Double
    Private ImporteGravadoPrimaDominical As Double
    Private ImporteExentoAguinaldo As Double
    Private ImporteGravadoAguinaldo As Double
    Private ImporteExentoPrimaVacacional As Double
    Private ImporteGravadoPrimaVacacional As Double
    Private ImporteExentoRepartoUtilidades As Double
    Private ImporteGravadoRepartoUtilidades As Double
    Private ImporteExentoPrevisionSocial As Double
    Private ImporteGravadoPrevisionSocial As Double

    Private PrestamoExento As Double
    Private PrestamoGravado As Double
    Private TelecomunicacionExento As Double
    Private TelecomunicacionGravado As Double
    Private OtrasPercepcionesExento As Double
    Private OtrasPercepcionesGravado As Double
    Private AyudaCulturalExento As Double
    Private AyudaCulturalGravado As Double
    Private AyudaDeportivaExento As Double
    Private AyudaDeportivaGravado As Double
    Private AyudaEducacionalExento As Double
    Private AyudaEducacionalGravado As Double
    Private AyudaEscolarExento As Double
    Private AyudaEscolarGravado As Double
    Private AyudaComidaExento As Double
    Private AyudaComidaGravado As Double
    Private ValesDespensaExento As Double
    Private ValesDespensaGravado As Double
    Private AyudaUniformeExento As Double
    Private AyudaUniformeGravado As Double
    Private BecasExento As Double
    Private BecasGravado As Double
    Private SubsidioIncapacidadExento As Double
    Private SubsidioIncapacidadGravado As Double
    Private AyudaMatrimonioExento As Double
    Private AyudaMatrimonioGravado As Double
    Private AyudaNacimientoExento As Double
    Private AyudaNacimientoGravado As Double
    Private ValesComedorExento As Double
    Private ValesComedorGravado As Double
    Private AyudaMedicamentoExento As Double
    Private AyudaMedicamentoGravado As Double
    Private ImporteExentoFondoAhorro As Double
    Private ImporteGravadoFondoAhorro As Double
    Private ImporteExentoAyudaFuneral As Double
    Private ImporteGravadoAyudaFuneral As Double

    Private DevolucionDescuentoInfonavit As Double

    Private Diferencias As Double
    Private Gratificacion As Double
    Private Bonificacion As Double
    Private Retroactivo As Double
    Private Telecomunicaciones As Double
    Private PremioProductividad As Double
    Private Incentivo As Double
    Private PremioAsistencia As Double
    Private PremioPuntualidad As Double
    Private Premio As Double
    Private Compensacion As Double
    Private BonoAntiguedad As Double
    Private Viaticos As Double
    Private Pasajes As Double
    Private AyudaTransporte As Double
    Private AyudaRenta As Double
    Private BonoI As Double
    Private AyudaCarestia As Double
    Private DespensaEfectivo As Double
    Private HaberPorRetiro As Double
    Private OtrasPercepciones As Double

    Private DescansoTrabajado As Double
    Private DescansoTrabajadoGravado As Double
    Private DescansoTrabajadoExento As Double
    Private FestivoTrabajado As Double
    Private FestivoTrabajadoGravado As Double
    Private FestivoTrabajadoExento As Double
    Private HorasExtraDobles As Double
    Private HorasExtraDoblesGravadas As Double
    Private HorasExtraDoblesExentas As Double
    Private HorasExtraTriples As Double
    Private ExentoHorasExtra As Double
    Private ExentoDescansoTrabajado As Double
    Private ExentoFestivoTrabajado As Double

    Private ImporteDiario As Double
    Private ImportePeriodo As Double
    Private Impuesto As Double
    Private Subsidio As Double
    Private SubsidioAplicado As Double
    Private SubsidioEfectivo As Double
    Private SalarioMinimoDiarioGeneral As Double
    Private ImporteExento As Double
    Private ImporteGravado As Double
    Private Agregar As String
    Private DiasVacaciones As Double
    Private DiasCuotaPeriodo As Double
    Private DiasHonorarioAsimilado As Double
    Private DiasPagoPorHoras As Double
    Private DiasComision As Double
    Private DiasDestajo As Double
    Private DiasFaltasPermisosIncapacidades As Double
    Private HonorarioAsimilado As Double

    Private PercepcionesExentas As Double
    Private PercepcionesGravadas As Double

    Private UMA As Double
    Private UMAMensual As Double
    Private UMI As Double
    Private SalarioDiarioIntegradoTrabajador As Double
    Private CuotaDiaria As Decimal
    Private Imss As Double
    Private ImporteSeguroVivienda As Double

    Private TiempoExtraordinarioDentroDelMargenLegal As Double
    Private TiempoExtraordinarioFueraDelMargenLegal As Double

    Private BaseGravableMensualSubsidio As Double
    Private BaseGravableMensualSubsidioDiario As Double
    Private BaseGravableMensualSubsidioSemanal As Double
    Private FactorSubsidio As Double
    Private SubsidioMensual As Double
    Private SubsidioDiario As Double
    Private ImporteDiarioGravado As Double

    Private FactorDiarioPromedio As Double

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Not String.IsNullOrEmpty(Request("id")) And Not String.IsNullOrEmpty(Request("empleadoid")) Then

                CargarVariablesGenerales()

                periodoId.Value = Request.QueryString("id").ToString
                empleadoId.Value = Request.QueryString("empleadoid").ToString
                contratoId.Value = Request.QueryString("contratoid").ToString
                nominaId.Value = Request.QueryString("nominaid").ToString

                Dim dt As New DataTable

                Dim Nomina As New Entities.Nomina()
                dt = Nomina.ConsultarNominaPorFolio(nominaId.Value)

                If dt.Rows.Count > 0 Then
                    For Each oDataRow In dt.Rows
                        empresaId.Value = oDataRow("idEmpresa")
                    Next
                End If

                Dim cPeriodo As New Entities.Periodo
                cPeriodo.IdPeriodo = periodoId.Value
                cPeriodo.ConsultarPeriodoID()

                If cPeriodo.IdPeriodo > 0 Then
                    lblFechaInicial.Text = cPeriodo.FechaInicial
                    lblFechaFinal.Text = cPeriodo.FechaFinal
                End If

                cPeriodo = Nothing

                lblEjercicio.Text = IdEjercicio.ToString

                Dim cEmpleado As New Entities.Empleado
                cEmpleado.IdEmpleado = empleadoId.Value
                cEmpleado.ConsultarEmpleadoID()
                If cEmpleado.IdEmpleado > 0 Then
                    lblNoPeriodo.Text = periodoId.Value
                    lblNumEmpleado.Text = empleadoId.Value
                    lblRFC.Text = cEmpleado.Rfc
                    lblNombreEmpleado.Text = cEmpleado.Nombre
                    lblNumImss.Text = cEmpleado.Imss
                    lblRegContratacion.Text = cEmpleado.RegimenContratacion
                    lblFechaIngreso.Text = cEmpleado.FechaIngreso
                    lblPuesto.Text = cEmpleado.Puesto
                    txtCuotaDiaria.Text = cEmpleado.CuotaDiaria
                    txtIntegradoImss.Text = cEmpleado.IntegradoImss
                    CuotaDiaria = cEmpleado.CuotaDiaria
                    Call CargarPercepcionesYDeducciones()
                    Call ChecarPercepcionesExentasYGravadas()
                    txtGravadoISR.Text = Math.Round(PercepcionesGravadas, 6)
                    txtExentoISR.Text = Math.Round(PercepcionesExentas, 6)
                End If

                Call LlenaCmbConcepto(0, "P")
                'Call LlenaCmbTipoHorasExtra(0)
                cEmpleado = Nothing

                Call MostrarDiasLaborados()
                Call MostrarCuotasDiaria()
                'Call MostrarSueldoDiarioIntegrado()

                Page.SetFocus(cmbConcepto)


            End If
        End If
    End Sub
    Private Sub MostrarDiasLaborados()
        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = periodoId.Value
        cNomina.TipoConcepto = "P"
        cNomina.CvoConcepto = 2
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.Tipo = "N"
        dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            txtDias.Text = dt.Rows(0).Item("Unidad")
        End If
    End Sub
    Private Sub MostrarSueldoDiarioIntegrado()
        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = periodoId.Value
        cNomina.TipoConcepto = "DE"
        cNomina.CvoConcepto = 87
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.Tipo = "N"
        dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            txtIntegradoImss.Text = dt.Rows(0).Item("CuotaDiaria") * 1.0452
        End If

    End Sub
    Private Sub MostrarCuotasDiaria()
        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = periodoId.Value
        cNomina.TipoConcepto = "DE"
        cNomina.CvoConcepto = 87
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.Tipo = "N"
        dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            txtCuotaDiaria.Text = dt.Rows(0).Item("CuotaDiaria")
        End If
    End Sub
    Private Sub LlenaCmbConcepto(ByVal sel As Integer, ByVal Tipo As String)
        Try
            Dim cConcepto As New Entities.Concepto
            If Tipo.Length > 0 Then
                cConcepto.Tipo = Tipo
            End If

            Dim objData As New DataControl
            objData.CatalogoRad(cmbConcepto, cConcepto.ConsultarConceptosIncidencia, True, False)
            objData = Nothing
            cConcepto = Nothing

            cmbConcepto.SelectedValue = sel

            If cmbConcepto.SelectedValue = 10 Then
                lblDiasHorasExtra.Visible = True
                txtDiasHorasExtra.Visible = True
                lblTipoHorasExtra.Visible = True
                cmbTipoHorasExtra.Visible = True
            Else
                lblDiasHorasExtra.Visible = False
                txtDiasHorasExtra.Visible = False
                lblTipoHorasExtra.Visible = False
                cmbTipoHorasExtra.Visible = False
                cmbTipoHorasExtra.SelectedValue = 0
                txtDiasHorasExtra.Text = ""
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub LlenaCmbTipoHorasExtra(ByVal sel As String)
        Try
            Dim cTipoHorasExtra As New Entities.TipoHorasExtra

            Dim objData As New DataControl
            objData.CatalogoRad(cmbTipoHorasExtra, cTipoHorasExtra.ConsultarTipoHorasExtra, True, False)
            objData = Nothing
            cTipoHorasExtra = Nothing

            cmbTipoHorasExtra.SelectedValue = sel

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub GridPercepciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridPercepciones.NeedDataSource

        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = periodoId.Value
        cNomina.TipoConcepto = "P"
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.Tipo = "N"
        cNomina.OtroPagoBit = 0
        dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
        cNomina = Nothing
        GridPercepciones.DataSource = dt

        If dt.Rows.Count > 0 Then
            txtPercepciones.Text = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
        End If
    End Sub
    Private Sub GridDeduciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridDeduciones.NeedDataSource

        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = periodoId.Value
        cNomina.TipoConcepto = "D"
        cNomina.NoEmpleado = empleadoId.Value
        dt = cNomina.ConsultarDeduccionesEmpleado()
        cNomina = Nothing
        GridDeduciones.DataSource = dt

        If dt.Rows.Count > 0 Then
            txtDeducciones.Text = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
        End If

    End Sub
    Private Sub GridOtrosPagos_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridOtrosPagos.NeedDataSource
        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = periodoId.Value
        cNomina.TipoConcepto = "P"
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.Tipo = "N"
        cNomina.OtroPagoBit = 1
        dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
        cNomina = Nothing
        GridOtrosPagos.DataSource = dt

        If dt.Rows.Count > 0 Then
            txtOtrosPagos.Text = Math.Round(dt.Compute("Sum(Importe)", "CvoSAT=002 OR CvoSAT=999"), 6)
        End If
    End Sub
    Private Sub CargarVariablesGenerales()
        Try
            Dim dt As New DataTable()
            Dim cConfiguracion = New Configuracion()
            'cConfiguracion.IdEmpresa = Session("clienteid")
            cConfiguracion.IdUsuario = Session("usuarioid")
            dt = cConfiguracion.ConsultarConfiguracion()
            cConfiguracion = Nothing

            If dt.Rows.Count > 0 Then
                For Each oDataRow In dt.Rows
                    'IdEmpresa = oDataRow("IdEmpresa")
                    IdEjercicio = oDataRow("IdEjercicio")
                    Periodo = oDataRow("IdPeriodo")
                    SalarioMinimoDiarioGeneral = oDataRow("SalarioMinimoDiarioGeneral")
                    ImporteSeguroVivienda = oDataRow("ImporteSeguroVivienda")
                    BaseGravableMensualSubsidio = oDataRow("BaseGravableMensualSubsidio")
                    FactorSubsidio = oDataRow("FactorSubsidio")
                    FactorDiarioPromedio = oDataRow("FactorDiarioPromedio")
                    UMA = oDataRow("UMA")
                    UMI = oDataRow("UMI")
                Next
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CargarPercepcionesYDeducciones()
        Try

            Call CargarVariablesGenerales()

            Dim Percepciones, Deducciones, OtrosPagos, Subsidio As Decimal

            ' Percepciones
            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.TipoConcepto = "P"
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.Tipo = "N"
            cNomina.OtroPagoBit = 0
            dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()

            Percepciones = 0
            Deducciones = 0
            OtrosPagos = 0

            If dt.Rows.Count > 0 Then
                Percepciones = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
                txtPercepciones.Text = Percepciones

                If dt.Rows(0).Item("TIMBRADO") = "S" Then
                    btnAgregar.Enabled = False
                Else
                    btnAgregar.Enabled = True
                End If

            End If

            ' Deducciones
            dt = New DataTable()
            cNomina = New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.TipoConcepto = "D"
            cNomina.NoEmpleado = empleadoId.Value
            dt = cNomina.ConsultarDeduccionesEmpleado()
            cNomina = Nothing

            If dt.Rows.Count > 0 Then
                Deducciones = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
                txtDeducciones.Text = Deducciones
            End If

            ' Otros pagos
            dt = New DataTable()
            cNomina = New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.TipoConcepto = "P"
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.Tipo = "N"
            cNomina.OtroPagoBit = 1
            dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
            cNomina = Nothing

            If dt.Rows.Count > 0 Then
                If dt.Compute("Sum(Importe)", "CvoConcepto=54") IsNot DBNull.Value Then
                    Subsidio = dt.Compute("Sum(Importe)", "CvoConcepto=54")
                End If

                If dt.Compute("Sum(Importe)", "CvoSAT=999") IsNot DBNull.Value Then
                    OtrosPagos = dt.Compute("Sum(Importe)", "CvoSAT=999")
                End If

                txtOtrosPagos.Text = Math.Round(Subsidio + OtrosPagos, 6)
            End If

            txtNetoAPagar.Text = Percepciones - Deducciones + OtrosPagos

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub ChecarPercepcionesExentasYGravadas()
        Try
            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.TipoConcepto = "P"
            dt = cNomina.ConsultarConceptosEmpleado()

            If dt.Rows.Count > 0 Then
                PercepcionesGravadas = dt.Compute("Sum(ImporteGravado)", "")
                PercepcionesExentas = dt.Compute("Sum(ImporteExento)", "")
            End If

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub rdoDeduccion_CheckedChanged(sender As Object, e As EventArgs) Handles rdoDeduccion.CheckedChanged
        LlenaCmbConcepto(0, "D")
    End Sub
    Private Sub rdoPercepcion_CheckedChanged(sender As Object, e As EventArgs) Handles rdoPercepcion.CheckedChanged
        LlenaCmbConcepto(0, "P")
    End Sub
    Private Sub GridDeduciones_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridDeduciones.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                conceptoId.Value = e.CommandArgument
                rwConfirmEliminaConcepto.RadConfirm("¿Está seguro de eliminar este concepto?", "confirmCallbackFnEliminaConcepto", 330, 180, Nothing, "Confirmar")
        End Select
    End Sub
    Private Sub GridPercepciones_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridPercepciones.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
                conceptoId.Value = e.CommandArgument
                tipohorasextraId.Value = item.GetDataKeyValue("TipoHorasExtra").ToString
                rwConfirmEliminaConcepto.RadConfirm("¿Está seguro de eliminar este concepto?", "confirmCallbackFnEliminaConcepto", 330, 180, Nothing, "Confirmar")
        End Select
    End Sub
    Private Sub EliminarConcepto(ByVal NumeroConcepto As Int32, ByVal TipoHorasExtra As String)
        ImporteDiario = 0
        ImportePeriodo = 0
        ImporteExento = 0
        ImporteGravado = 0
        Agregar = 0
        'imss
        SalarioDiarioIntegradoTrabajador = 0
        SalarioDiarioIntegradoTrabajador = txtIntegradoImss.Text
        'oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
        ChecarSiExistenDiasEnPercepciones(empleadoId.Value, NumeroConcepto)
        'en este caso, si se pagan conceptos tales como comisiones, en teoria estas se generan posterior a que el trabajador tiene un sueldo, es decir una cuaota del periodo actual, ese es el motivo del mensaje.
        If DiasCuotaPeriodo = 0 And DiasVacaciones = 0 And DiasComision = 0 And DiasPagoPorHoras = 0 And DiasDestajo = 0 And DiasHonorarioAsimilado = 0 Then
            rwAlerta.RadAlert("Esta percepcion no puede quitarse sin que exista alguna de las siguientes: 1.- CuotaPeriodo. 2.- Vacaciones. 3.- Honorario Asimilado. 4.- Pago Por Horas. 5.- Comisión. 6.- Destajo. Pues estas van acompañadas implicitamente por los 7 dias correspondientes al periodo semanal lo cual es base necesaria para el cálculo del impuesto, lo que si puede hacer es cambiar el número de dias o eliminar completamente el empleado en este periodo!!!", 490, 210, "Alerta", "", "")
            Exit Sub
        End If

        'En esta parte se checa si el concepto que se esta agregando es percepcion(menor que 51) o es falta, permiso o incapacidad(57,58,59), siempre se borran los impuestos actuales y se recalculan, la demas deduccioen no entran aqui ya que no recalculan impuestos, simplemente se restan.
        'If cmbConcepto.SelectedValue.ToString < 52 Or cmbConcepto.SelectedValue.ToString = "57" Or cmbConcepto.SelectedValue.ToString = "58" Or cmbConcepto.SelectedValue.ToString = "59" Then
        If NumeroConcepto < 52 Or NumeroConcepto = "57" Or NumeroConcepto = "58" Or NumeroConcepto = "59" Or NumeroConcepto = "161" Or NumeroConcepto = "162" Or NumeroConcepto = "167" Or NumeroConcepto = "168" Or NumeroConcepto = "169" Or NumeroConcepto = "170" Or NumeroConcepto = "171" Then
            BorrarDeducciones(empleadoId.Value)
        End If

        QuitarConcepto(NumeroConcepto, TipoHorasExtra)

        If NumeroConcepto < 52 Or NumeroConcepto = "57" Or NumeroConcepto = "58" Or NumeroConcepto = "59" Or NumeroConcepto = "161" Or NumeroConcepto = "162" Or NumeroConcepto = "167" Or NumeroConcepto = "168" Or NumeroConcepto = "169" Or NumeroConcepto = "170" Or NumeroConcepto = "171" Then

            Call QuitarConcepto(52, "") 'IMPUESTO
            Call QuitarConcepto(54, "") 'SUBSIDIO
            Call QuitarConcepto(56, "") 'CUOTA IMSS

            Call ChecarPercepcionesGravadas(empleadoId.Value, NumeroConcepto)
            Call ChecarYGrabarPercepcionesExentasYGravadas(empleadoId.Value, 0)
            Call ChecarPercepcionesExentasYGravadas()

            Dim cPeriodo As New Entities.Periodo()
            cPeriodo.IdPeriodo = periodoId.Value
            cPeriodo.ConsultarPeriodoID()

            Call CalcularImss()

            Imss = Imss * NumeroDeDiasPagados
            Imss = Math.Round(Imss, 6)

            If Imss > 0 Then
                Dim cNomina = New Nomina()
                cNomina.Cliente = empresaId.Value
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = Periodo
                cNomina.NoEmpleado = empleadoId.Value
                cNomina.CvoConcepto = 56
                cNomina.IdContrato = contratoId.Value
                cNomina.TipoConcepto = "D"
                cNomina.Unidad = 1
                cNomina.Importe = Imss
                cNomina.ImporteGravado = 0
                cNomina.ImporteExento = Imss
                cNomina.Generado = ""
                cNomina.Timbrado = ""
                cNomina.Enviado = ""
                cNomina.Situacion = "A"
                cNomina.EsEspecial = False
                cNomina.FechaIni = cPeriodo.FechaInicialDate
                cNomina.FechaFin = cPeriodo.FechaFinalDate
                cNomina.FechaPago = cPeriodo.FechaPago
                cNomina.DiasPagados = cPeriodo.Dias
                cNomina.IdNomina = nominaId.Value
                cNomina.GuadarNominaPeriodo()
            End If

            Call CalcularImpuesto()

            Impuesto = Impuesto * NumeroDeDiasPagados
            Impuesto = Math.Round(Impuesto, 6)

            SubsidioAplicado = 0
            ImporteDiarioGravado = 0
            BaseGravableMensualSubsidioDiario = (BaseGravableMensualSubsidio / FactorDiarioPromedio)
            ImporteDiarioGravado = PercepcionesGravadas / NumeroDeDiasPagados

            If ImporteDiarioGravado <= BaseGravableMensualSubsidioDiario Then
                UMAMensual = UMA * FactorDiarioPromedio
                SubsidioMensual = UMAMensual * (FactorSubsidio / 100)
                SubsidioDiario = SubsidioMensual / FactorDiarioPromedio

                If (Impuesto > 0 And (Impuesto < (SubsidioDiario * NumeroDeDiasPagados))) Then
                    SubsidioAplicado = Impuesto
                Else
                    SubsidioAplicado = (SubsidioDiario * NumeroDeDiasPagados)
                End If

                If Impuesto > SubsidioAplicado Then
                    Impuesto = Impuesto - SubsidioAplicado
                ElseIf Impuesto < SubsidioAplicado Then
                    SubsidioAplicado = SubsidioAplicado - Impuesto
                    Impuesto = 0
                End If

            End If

            If Impuesto > 0 Then
                Dim cNomina = New Nomina()
                cNomina.Cliente = empresaId.Value
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = Periodo
                cNomina.NoEmpleado = empleadoId.Value
                cNomina.CvoConcepto = 52
                cNomina.IdContrato = contratoId.Value
                cNomina.TipoConcepto = "D"
                cNomina.Unidad = 1
                cNomina.Importe = Impuesto
                cNomina.ImporteGravado = 0
                cNomina.ImporteExento = Impuesto
                cNomina.Generado = ""
                cNomina.Timbrado = ""
                cNomina.Enviado = ""
                cNomina.Situacion = "A"
                cNomina.EsEspecial = False
                cNomina.FechaIni = cPeriodo.FechaInicialDate
                cNomina.FechaFin = cPeriodo.FechaFinalDate
                cNomina.FechaPago = cPeriodo.FechaPago
                cNomina.DiasPagados = cPeriodo.Dias
                cNomina.IdNomina = nominaId.Value
                cNomina.GuadarNominaPeriodo()
            End If

            If SubsidioAplicado > 0 Then
                Dim cNomina = New Nomina()
                cNomina.Cliente = empresaId.Value
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = Periodo
                cNomina.NoEmpleado = empleadoId.Value
                cNomina.CvoConcepto = 54
                cNomina.IdContrato = contratoId.Value
                cNomina.TipoConcepto = "P"
                cNomina.Unidad = 1
                cNomina.Importe = SubsidioAplicado
                cNomina.ImporteGravado = 0
                cNomina.ImporteExento = SubsidioAplicado
                cNomina.Generado = ""
                cNomina.Timbrado = ""
                cNomina.Enviado = ""
                cNomina.Situacion = "A"
                cNomina.EsEspecial = False
                cNomina.FechaIni = cPeriodo.FechaInicialDate
                cNomina.FechaFin = cPeriodo.FechaFinalDate
                cNomina.FechaPago = cPeriodo.FechaPago
                cNomina.DiasPagados = cPeriodo.Dias
                cNomina.IdNomina = nominaId.Value
                cNomina.GuadarNominaPeriodo()
            End If
        End If

        Try
            CuotaDiaria = Convert.ToDecimal(txtCuotaDiaria.Text)
        Catch ex As Exception
            CuotaDiaria = 0
        End Try

        Call GuardarRegistro(CuotaDiaria, 2)
        Call ChecarYGrabarPercepcionesExentasYGravadas(empleadoId.Value, NumeroConcepto)
        Call SolicitarGeneracionXml(empleadoId.Value, "")
        Call CargarPercepcionesYDeducciones()
        Call CargarPercepciones()
        Call CargarDeducciones()
        Call CargarOtrosPagos()
        Call ChecarPercepcionesExentasYGravadas()
        txtGravadoISR.Text = Math.Round(PercepcionesGravadas, 6)
        txtExentoISR.Text = Math.Round(PercepcionesExentas, 6)

    End Sub
    Private Sub ChecarSiExistenDiasEnPercepciones(ByVal NoEmpleado As Int64, ByVal CvoConcepto As Int32)
        Try
            Dim CuotaPeriodo As Double = 0
            Dim PrimaDominical As Double = 0
            Dim PrimaVacacional As Double = 0
            Dim Vacaciones As Double = 0
            Dim Aguinaldo As Double = 0
            Dim RepartoUtilidades As Double = 0
            Dim FondoAhorro As Double = 0
            Dim AyudaFuneral As Double = 0
            Dim PrevisionSocial As Double = 0
            Dim GrupoPercepcionesGravadasTotalmenteSinExentos As Double = 0
            Dim PagoPorHoras As Double = 0
            Dim Comisiones As Double = 0
            Dim Destajo As Double = 0
            HonorarioAsimilado = 0
            DiasVacaciones = 0
            DiasCuotaPeriodo = 0
            DiasHonorarioAsimilado = 0
            DiasPagoPorHoras = 0
            DiasComision = 0
            DiasDestajo = 0
            TiempoExtraordinarioDentroDelMargenLegal = 0
            TiempoExtraordinarioFueraDelMargenLegal = 0

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()

            If Agregar = 1 Then
                'Me.oDataAdapterChecarPercepcionesGravadasSql = New SqlDataAdapter("SELECT * FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO=" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO=" + TxtClaveEmpleado.Text + " AND TIPOCONCEPTO='P'", oConexionSql)
                'cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = periodoId.Value
                cNomina.NoEmpleado = NoEmpleado
                cNomina.TipoConcepto = "P"
                dt = cNomina.ConsultarConceptosEmpleado()
            ElseIf Agregar = 0 Then
                'Me.oDataAdapterChecarPercepcionesGravadas = New OleDbDataAdapter("SELECT * FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO=" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO=" + TxtClaveEmpleado.Text + " AND TIPOCONCEPTO='P' AND CVOCONCEPTO<>" + NumeroConcepto.ToString + "", oConexion)
                'cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = periodoId.Value
                cNomina.NoEmpleado = NoEmpleado
                cNomina.TipoConcepto = "P"
                cNomina.DiferenteDe = 1
                cNomina.CvoConcepto = CvoConcepto
                dt = cNomina.ConsultarConceptosEmpleado()
            Else
                'cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = periodoId.Value
                cNomina.NoEmpleado = NoEmpleado
                cNomina.TipoConcepto = "P"
                dt = cNomina.ConsultarConceptosEmpleado()
            End If

            ' PercepcionesGravadas
            If dt.Rows.Count > 0 Then
                If dt.Compute("Sum(Importe)", "CvoConcepto=2") IsNot DBNull.Value Then
                    If Agregar <> 3 Then
                        DiasCuotaPeriodo = dt.Compute("Sum(Unidad)", "CvoConcepto=2")
                        CuotaPeriodo = dt.Compute("Sum(Importe)", "CvoConcepto=2")
                    ElseIf Agregar = 3 Then
                        Try
                            DiasCuotaPeriodo = Convert.ToDecimal(txtDias.Text)
                        Catch ex As Exception
                            DiasCuotaPeriodo = 0
                        End Try
                        CuotaPeriodo = DiasCuotaPeriodo * CuotaDiaria
                    End If
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=3") IsNot DBNull.Value Then
                    DiasComision = 7
                    Comisiones = dt.Compute("Sum(Importe)", "CvoConcepto=3")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=4") IsNot DBNull.Value Then
                    DiasDestajo = 7
                    Destajo = dt.Compute("Sum(Importe)", "CvoConcepto=4")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=5") IsNot DBNull.Value Then
                    DiasHonorarioAsimilado = dt.Compute("Sum(Unidad)", "CvoConcepto=5") * 7
                    HonorarioAsimilado = dt.Compute("Sum(Importe)", "CvoConcepto=5")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=6 OR CvoConcepto=9 OR CvoConcepto=10") IsNot DBNull.Value Then
                    TiempoExtraordinarioDentroDelMargenLegal = dt.Compute("Sum(Importe)", "CvoConcepto=6 OR CvoConcepto=9 OR CvoConcepto=10")
                End If
                'If dt.Compute("Sum(Importe)", "CvoConcepto=7 OR CvoConcepto=8") IsNot DBNull.Value Then
                '    TiempoExtraordinarioFueraDelMargenLegal = dt.Compute("Sum(Importe)", "CvoConcepto=7 OR CvoConcepto=8")
                'End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=7") IsNot DBNull.Value Then
                    HorasExtraTriples = dt.Compute("Sum(Importe)", "CvoConcepto=7")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=8") IsNot DBNull.Value Then
                    DescansoTrabajado = dt.Compute("Sum(Importe)", "CvoConcepto=8")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=11") IsNot DBNull.Value Then
                    DiasPagoPorHoras = 7
                    PagoPorHoras = dt.Compute("Sum(Importe)", "CvoConcepto=11")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=13") IsNot DBNull.Value Then
                    PrimaDominical = dt.Compute("Sum(Importe)", "CvoConcepto=13")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=14") IsNot DBNull.Value Then
                    Aguinaldo = dt.Compute("Sum(Importe)", "CvoConcepto=14")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=16") IsNot DBNull.Value Then
                    PrimaVacacional = dt.Compute("Sum(Importe)", "CvoConcepto=16")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=15") IsNot DBNull.Value Then
                    DiasVacaciones = dt.Compute("Sum(Unidad)", "CvoConcepto=15")
                    Vacaciones = dt.Compute("Sum(Importe)", "CvoConcepto=15")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=50") IsNot DBNull.Value Then
                    RepartoUtilidades = dt.Compute("Sum(Importe)", "CvoConcepto=50")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=42") IsNot DBNull.Value Then
                    FondoAhorro = dt.Compute("Sum(Importe)", "CvoConcepto=42")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=44") IsNot DBNull.Value Then
                    AyudaFuneral = dt.Compute("Sum(Importe)", "CvoConcepto=44")
                End If
                '20,33,35,36,37,38,40,41,43,45,46,47,48
                If dt.Compute("Sum(Importe)", "CvoConcepto=20 OR CvoConcepto=33 OR CvoConcepto=35 OR CvoConcepto=36 OR CvoConcepto=37 OR CvoConcepto=38 OR CvoConcepto=40 OR CvoConcepto=41 OR CvoConcepto=43 OR CvoConcepto=45 OR CvoConcepto=46 OR CvoConcepto=47 OR CvoConcepto=48") IsNot DBNull.Value Then
                    PrevisionSocial = dt.Compute("CvoConcepto=20 OR Sum(Importe)", "CvoConcepto=33 OR CvoConcepto=35 OR CvoConcepto=36 OR CvoConcepto=37 OR CvoConcepto=38 OR CvoConcepto=40 OR CvoConcepto=41 OR CvoConcepto=43 OR CvoConcepto=45 OR CvoConcepto=46 OR CvoConcepto=47 OR CvoConcepto=48")
                End If
                '12,17,18,19,21,22,23,24,25,26,27,28,29,30,31,32,34,39,49
                '167,168,169,170,171
                If dt.Compute("Sum(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=18 OR CvoConcepto=19 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=28 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=32 OR CvoConcepto=39 OR CvoConcepto=49 OR CvoConcepto=167 OR CvoConcepto=168 OR CvoConcepto=169 OR CvoConcepto=170 OR CvoConcepto=171") IsNot DBNull.Value Then
                    GrupoPercepcionesGravadasTotalmenteSinExentos = dt.Compute("Sum(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=18 OR CvoConcepto=19 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=28 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=32 OR CvoConcepto=39 OR CvoConcepto=49 OR CvoConcepto=167 OR CvoConcepto=168 OR CvoConcepto=169 OR CvoConcepto=170 OR CvoConcepto=171")
                End If
            End If

            'Agregar es igual a uno cuando venimos del boton agregar
            'Agregar es igual a cero cuando venimos del boton quitar
            'Si viene del boton quitar no se agrega en ningun caso el contenido de ImporteIncidencia, solo se checa lo exento y gravado que ya esta dentro de la base de datos y se recalculan los impuestos
            If Agregar = 1 Then

                Dim ImporteIncidencia As Decimal = 0
                Dim UnidadIncidencia As Decimal = 0
                Try
                    ImporteIncidencia = Convert.ToDecimal(txtImporteIncidencia.Text)
                Catch ex As Exception
                    ImporteIncidencia = 0
                End Try

                Try
                    UnidadIncidencia = Convert.ToDecimal(txtUnidadIncidencia.Text)
                Catch ex As Exception
                    UnidadIncidencia = 0
                End Try

                If CvoConcepto.ToString = "6" Or CvoConcepto.ToString = "9" Or CvoConcepto.ToString = "10" Then
                    '6=horas exrras dobles, 9=festivo trabajado y 10=doblete(cuando termina la jornada del trabajador y por x circunstancia cubre el siguiente turno)
                    TiempoExtraordinarioDentroDelMargenLegal = TiempoExtraordinarioDentroDelMargenLegal + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "3" Then
                    DiasComision = 7
                    Comisiones = Comisiones + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "4" Then
                    DiasDestajo = 7
                    Destajo = Destajo + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "5" Then
                    DiasHonorarioAsimilado = DiasCuotaPeriodo + (UnidadIncidencia * 7)
                    HonorarioAsimilado = HonorarioAsimilado + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "7" Or CvoConcepto.ToString = "8" Then
                    '7=horas extras triples y 8=festivo trabajado
                    TiempoExtraordinarioFueraDelMargenLegal = TiempoExtraordinarioFueraDelMargenLegal + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "2" Then
                    DiasCuotaPeriodo = DiasCuotaPeriodo + UnidadIncidencia
                    CuotaPeriodo = CuotaPeriodo + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "11" Then
                    DiasPagoPorHoras = 7
                    PagoPorHoras = PagoPorHoras + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "13" Then
                    PrimaDominical = PrimaDominical + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "14" Then
                    Aguinaldo = Aguinaldo + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "16" Then
                    PrimaVacacional = PrimaVacacional + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "15" Then
                    DiasVacaciones = DiasVacaciones + UnidadIncidencia
                    Vacaciones = Vacaciones + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "50" Then
                    RepartoUtilidades = RepartoUtilidades + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "42" Then
                    FondoAhorro = FondoAhorro + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "44" Then
                    AyudaFuneral = AyudaFuneral + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "20" Or CvoConcepto.ToString = "33" Or CvoConcepto.ToString = "34" Or CvoConcepto.ToString = "35" Or CvoConcepto.ToString = "36" Or CvoConcepto.ToString = "37" Or CvoConcepto.ToString = "38" Or CvoConcepto.ToString = "40" Or CvoConcepto.ToString = "41" Or CvoConcepto.ToString = "43" Or CvoConcepto.ToString = "45" Or CvoConcepto.ToString = "46" Or CvoConcepto.ToString = "47" Or CvoConcepto.ToString = "48" Then
                    PrevisionSocial = PrevisionSocial + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "12" Or CvoConcepto.ToString = "17" Or CvoConcepto.ToString = "18" Or CvoConcepto.ToString = "19" Or CvoConcepto.ToString = "20" Or CvoConcepto.ToString = "21" Or CvoConcepto.ToString = "22" Or CvoConcepto.ToString = "23" Or CvoConcepto.ToString = "24" Or CvoConcepto.ToString = "25" Or CvoConcepto.ToString = "26" Or CvoConcepto.ToString = "27" Or CvoConcepto.ToString = "28" Or CvoConcepto.ToString = "29" Or CvoConcepto.ToString = "30" Or CvoConcepto.ToString = "31" Or CvoConcepto.ToString = "32" Or CvoConcepto.ToString = "39" Or CvoConcepto.ToString = "49" Then
                    '12,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,39,49
                    GrupoPercepcionesGravadasTotalmenteSinExentos = GrupoPercepcionesGravadasTotalmenteSinExentos + ImporteIncidencia
                End If
            End If

            'Estos tipos de percepcion son independientes unos de otros, en teoria no deberian ir juntos nunca, como conllevan implicitamente 7 dias del periodo actual, si estan juntos solo se toman en cuenta 7 dias para todos
            'El enfoque de que no deberian ir juntos nunca es que a un trabajador de sueldo no se le paga por horas, ni por comision, ni por destajo, si se hace se procede con los dias como se explica en el renglon anterior
            If DiasCuotaPeriodo > 0 Then
                DiasPagoPorHoras = 0
                DiasComision = 0
                DiasDestajo = 0
            ElseIf DiasPagoPorHoras > 0 Then
                DiasCuotaPeriodo = 0
                DiasComision = 0
                DiasDestajo = 0
            ElseIf DiasComision > 0 Then
                DiasPagoPorHoras = 0
                DiasCuotaPeriodo = 0
                DiasDestajo = 0
            ElseIf DiasDestajo > 0 Then
                DiasComision = 0
                DiasPagoPorHoras = 0
                DiasCuotaPeriodo = 0
            End If
            If ImporteGravado > 0 And (DiasCuotaPeriodo + DiasVacaciones + DiasComision + DiasPagoPorHoras + DiasDestajo + DiasHonorarioAsimilado > 0) Then
                ImporteDiario = ImporteGravado / (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo + DiasHonorarioAsimilado)
            Else
                ImporteDiario = 0
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Public Sub BorrarDeducciones(ByVal NoEmpleado As Int64)
        Try
            Call CargarVariablesGenerales()
            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=86 AND TIPOCONCEPTO='D'"
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 86
            cNomina.TipoConcepto = "D"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=54 AND TIPOCONCEPTO='P'"
            cNomina = New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 54
            cNomina.TipoConcepto = "P"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=55 AND TIPOCONCEPTO='P'"
            cNomina = New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 55
            cNomina.TipoConcepto = "P"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=56 AND TIPOCONCEPTO='D'"
            cNomina = New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 56
            cNomina.TipoConcepto = "D"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=108 AND TIPOCONCEPTO='DE'"
            cNomina = New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 108
            cNomina.TipoConcepto = "DE"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=109 AND TIPOCONCEPTO='DE'"
            cNomina = New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 109
            cNomina.TipoConcepto = "DE"
            cNomina.EliminaConceptoEmpleado()

            cNomina = New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 87
            cNomina.TipoConcepto = "DE"
            cNomina.EliminaConceptoEmpleado()

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub QuitarConcepto(ByVal NumeroConcepto As Int32, ByVal TipoHorasExtra As String)
        Try

            Call CargarVariablesGenerales()

            Dim cNomina As New Nomina()

            If NumeroConcepto <= 51 Or NumeroConcepto = 54 Or NumeroConcepto = 82 Or NumeroConcepto = 165 Or NumeroConcepto = 167 Or NumeroConcepto = 168 Or NumeroConcepto = 169 Or NumeroConcepto = 170 Or NumeroConcepto = 171 Then
                'cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = periodoId.Value
                cNomina.NoEmpleado = empleadoId.Value
                cNomina.CvoConcepto = NumeroConcepto
                cNomina.TipoConcepto = "P"
                cNomina.TipoHorasExtra = TipoHorasExtra
                cNomina.EliminaConceptoEmpleado()
            ElseIf NumeroConcepto >= 61 And NumeroConcepto <= 87 Or NumeroConcepto = 52 Or NumeroConcepto = 56 Or NumeroConcepto = 57 Or NumeroConcepto = 58 Or NumeroConcepto = 59 Or NumeroConcepto = 161 Or NumeroConcepto = 162 Then
                'Deducciones
                'cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = periodoId.Value
                cNomina.NoEmpleado = empleadoId.Value
                cNomina.CvoConcepto = NumeroConcepto
                cNomina.TipoConcepto = "D"
                cNomina.EliminaConceptoEmpleado()
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub ChecarPercepcionesGravadas(ByVal NoEmpleado As Int64, ByVal CvoConcepto As Int32)
        Try
            Dim CuotaPeriodo As Double = 0
            Dim PrimaDominical As Double = 0
            Dim PrimaVacacional As Double = 0
            Dim Vacaciones As Double = 0
            Dim Aguinaldo As Double = 0
            Dim Telecomunicaciones As Double = 0
            Dim RepartoUtilidades As Double = 0
            Dim FondoAhorro As Double = 0
            Dim AyudaFuneral As Double = 0
            Dim PrevisionSocial As Double = 0
            Dim GrupoPercepcionesGravadasTotalmenteSinExentos As Double = 0
            Dim PagoPorHoras As Double = 0
            Dim Comisiones As Double = 0
            Dim Destajo As Double = 0
            Dim FaltasPermisosIncapacidades As Double = 0
            HonorarioAsimilado = 0
            DiasVacaciones = 0
            DiasCuotaPeriodo = 0
            DiasHonorarioAsimilado = 0
            DiasPagoPorHoras = 0
            DiasComision = 0
            DiasDestajo = 0
            DiasFaltasPermisosIncapacidades = 0

            DescansoTrabajado = 0
            DescansoTrabajadoGravado = 0
            DescansoTrabajadoExento = 0
            FestivoTrabajado = 0
            FestivoTrabajadoGravado = 0
            FestivoTrabajadoExento = 0
            HorasExtraDobles = 0
            HorasExtraDoblesGravadas = 0
            HorasExtraDoblesExentas = 0
            HorasExtraTriples = 0
            ExentoHorasExtra = 0
            ExentoDescansoTrabajado = 0
            ExentoFestivoTrabajado = 0
            'TiempoExtraordinarioFueraDelMargenLegal = 0
            ImporteGravado = 0
            ImporteDiario = 0

            Dim ImporteIncidencia As Decimal = 0
            Dim UnidadIncidencia As Decimal = 0
            Try
                ImporteIncidencia = Convert.ToDecimal(txtImporteIncidencia.Text)
            Catch ex As Exception
                ImporteIncidencia = 0
            End Try

            Try
                UnidadIncidencia = Convert.ToDecimal(txtUnidadIncidencia.Text)
            Catch ex As Exception
                UnidadIncidencia = 0
            End Try

            Try
                CuotaDiaria = Convert.ToDecimal(txtCuotaDiaria.Text)
            Catch ex As Exception
                CuotaDiaria = 0
            End Try

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.Tipo = "N"
            dt = cNomina.ConsultarConceptosEmpleado()

            'PercepcionesGravadas
            If dt.Rows.Count > 0 Then
                If dt.Compute("Sum(Importe)", "CvoConcepto=2") IsNot DBNull.Value Then
                    DiasCuotaPeriodo = dt.Compute("Sum(UNIDAD)", "CvoConcepto=2")
                    CuotaPeriodo = dt.Compute("Sum(Importe)", "CvoConcepto=2")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=3") IsNot DBNull.Value Then
                    DiasComision = 7
                    Comisiones = dt.Compute("Sum(Importe)", "CvoConcepto=3")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=4") IsNot DBNull.Value Then
                    DiasDestajo = 7
                    Destajo = dt.Compute("Sum(Importe)", "CvoConcepto=4")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=5") IsNot DBNull.Value Then
                    DiasHonorarioAsimilado = dt.Compute("Sum(UNIDAD)", "CvoConcepto=5") * 7
                    HonorarioAsimilado = dt.Compute("Sum(Importe)", "CvoConcepto=5")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=8") IsNot DBNull.Value Then
                    DescansoTrabajado = dt.Compute("Sum(Importe)", "CvoConcepto=8")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=9") IsNot DBNull.Value Then
                    FestivoTrabajado = dt.Compute("Sum(Importe)", "CvoConcepto=9")
                End If
                '****************** Modificacion HORAS EXTRA 17/05/2017 '******************
                If dt.Compute("Sum(Importe)", "CvoConcepto=10") IsNot DBNull.Value Then
                    If dt.Compute("Sum(Importe)", "CvoConcepto=10 AND TipoHorasExtra='01'") IsNot DBNull.Value Then
                        HorasExtraDobles = dt.Compute("Sum(Importe)", "CvoConcepto=10 AND TipoHorasExtra='01'")
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=10 AND TipoHorasExtra='02'") IsNot DBNull.Value Then
                        HorasExtraTriples = dt.Compute("Sum(Importe)", "CvoConcepto=10 AND TipoHorasExtra='02'")
                    End If
                End If
                '****************** Modificacion HORAS EXTRA 17/05/2017 '******************
                If dt.Compute("Sum(Importe)", "CvoConcepto=11") IsNot DBNull.Value Then
                    DiasPagoPorHoras = 7
                    PagoPorHoras = dt.Compute("Sum(Importe)", "CvoConcepto=11")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=13") IsNot DBNull.Value Then
                    PrimaDominical = dt.Compute("Sum(Importe)", "CvoConcepto=13")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=14") IsNot DBNull.Value Then
                    Aguinaldo = dt.Compute("Sum(Importe)", "CvoConcepto=14")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=16") IsNot DBNull.Value Then
                    PrimaVacacional = dt.Compute("Sum(Importe)", "CvoConcepto=16")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=20") IsNot DBNull.Value Then
                    Telecomunicaciones = dt.Compute("Sum(Importe)", "CvoConcepto=20")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=15") IsNot DBNull.Value Then
                    DiasVacaciones = dt.Compute("Sum(UNIDAD)", "CvoConcepto=15")
                    Vacaciones = dt.Compute("Sum(Importe)", "CvoConcepto=15")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=50") IsNot DBNull.Value Then
                    RepartoUtilidades = dt.Compute("Sum(Importe)", "CvoConcepto=50")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=42") IsNot DBNull.Value Then
                    FondoAhorro = dt.Compute("Sum(Importe)", "CvoConcepto=42")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=44") IsNot DBNull.Value Then
                    AyudaFuneral = dt.Compute("Sum(Importe)", "CvoConcepto=44")
                End If
                '18,20,28,33,35,36,37,38,40,41,43,45,46,47,48
                If dt.Compute("Sum(Importe)", "CvoConcepto=18 OR CvoConcepto=20 OR CvoConcepto=28 OR CvoConcepto=33 OR CvoConcepto=35 OR CvoConcepto=36 OR CvoConcepto=37 OR CvoConcepto=38 OR CvoConcepto=40 OR CvoConcepto=41 OR CvoConcepto=43 OR CvoConcepto=45 OR CvoConcepto=46 OR CvoConcepto=47 OR CvoConcepto=48") IsNot DBNull.Value Then
                    PrevisionSocial = dt.Compute("Sum(Importe)", "CvoConcepto=18 OR CvoConcepto=20 OR CvoConcepto=28 OR CvoConcepto=33 OR CvoConcepto=35 OR CvoConcepto=36 OR CvoConcepto=37 OR CvoConcepto=38 OR CvoConcepto=40 OR CvoConcepto=41 OR CvoConcepto=43 OR CvoConcepto=45 OR CvoConcepto=46 OR CvoConcepto=47 OR CvoConcepto=48")
                End If
                '12,17,19,21,22,23,24,25,26,27,29,30,31,34,39,49,51
                '167,168,169,170,171
                If dt.Compute("Sum(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=19 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=34 OR CvoConcepto=39 OR CvoConcepto=49 OR CvoConcepto=51 OR CvoConcepto=167 OR CvoConcepto=168 OR CvoConcepto=169 OR CvoConcepto=170 OR CvoConcepto=171") IsNot DBNull.Value Then
                    GrupoPercepcionesGravadasTotalmenteSinExentos = dt.Compute("Sum(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=19 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=34 OR CvoConcepto=39 OR CvoConcepto=49 OR CvoConcepto=51 OR CvoConcepto=167 OR CvoConcepto=168 OR CvoConcepto=169 OR CvoConcepto=170 OR CvoConcepto=171")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") IsNot DBNull.Value Then
                    DiasFaltasPermisosIncapacidades = dt.Compute("Sum(UNIDAD)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162")
                    FaltasPermisosIncapacidades = dt.Compute("Sum(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=54") IsNot DBNull.Value Then
                    SubsidioAplicado = dt.Compute("Sum(Importe)", "CvoConcepto=54")
                End If
                'If dt.Compute("Sum(Importe)", "CvoConcepto=55") IsNot DBNull.Value Then
                '    SubsidioEfectivo = dt.Compute("Sum(Importe)", "CvoConcepto=55")
                'End If
            End If
            'Agregar es igual a uno cuando venimos del boton agregar
            'Agregar es igual a cero cuando venimos del boton quitar
            'Si viene del boton quitar no se agrega en ningun caso el contenido de ImporteIncidencia, solo se checa lo exento y gravado que ya esta dentro de la base de datos y se recalculan los impuestos
            'If Agregar = 1 Then
            '    If CvoConcepto.ToString = "10" Then
            '        '6=horas exrras dobles, 9=festivo trabajado y 10=doblete(cuando termina la jornada del trabajador y por x circunstancia cubre el siguiente turno)
            '        'TiempoExtraordinarioDentroDelMargenLegal = TiempoExtraordinarioDentroDelMargenLegal + ImporteIncidencia
            '        'TiempoExtraordinarioDentroDelMargenLegal = ImporteIncidencia
            '    ElseIf CvoConcepto.ToString = "3" Then
            '        DiasComision = 7
            '        Comisiones = Comisiones + ImporteIncidencia
            '    ElseIf CvoConcepto.ToString = "4" Then
            '        DiasDestajo = 7
            '        Destajo = Destajo + ImporteIncidencia
            '    ElseIf CvoConcepto.ToString = "5" Then
            '        DiasHonorarioAsimilado = DiasCuotaPeriodo + (UnidadIncidencia * 7)
            '        HonorarioAsimilado = HonorarioAsimilado + ImporteIncidencia
            '    ElseIf CvoConcepto.ToString = "7" Or CvoConcepto.ToString = "8" Then
            '        '7=horas extras triples y 8=festivo trabajado
            '        'TiempoExtraordinarioFueraDelMargenLegal = TiempoExtraordinarioFueraDelMargenLegal + ImporteIncidencia
            '    ElseIf CvoConcepto.ToString = "2" Then
            '        DiasCuotaPeriodo = DiasCuotaPeriodo + UnidadIncidencia
            '        CuotaPeriodo = CuotaPeriodo + ImporteIncidencia
            '    ElseIf CvoConcepto.ToString = "11" Then
            '        DiasPagoPorHoras = 7
            '        PagoPorHoras = PagoPorHoras + ImporteIncidencia
            '    ElseIf CvoConcepto.ToString = "13" Then
            '        PrimaDominical = PrimaDominical + ImporteIncidencia
            '    ElseIf CvoConcepto.ToString = "14" Then
            '        Aguinaldo = Aguinaldo + ImporteIncidencia
            '    ElseIf CvoConcepto.ToString = "16" Then
            '        PrimaVacacional = PrimaVacacional + ImporteIncidencia
            '    ElseIf CvoConcepto.ToString = "15" Then
            '        DiasVacaciones = DiasVacaciones + UnidadIncidencia
            '        Vacaciones = Vacaciones + ImporteIncidencia
            '    ElseIf CvoConcepto.ToString = "50" Then
            '        RepartoUtilidades = RepartoUtilidades + ImporteIncidencia
            '    ElseIf CvoConcepto.ToString = "42" Then
            '        FondoAhorro = FondoAhorro + ImporteIncidencia
            '    ElseIf CvoConcepto.ToString = "44" Then
            '        AyudaFuneral = AyudaFuneral + ImporteIncidencia
            '    ElseIf CvoConcepto.ToString = "33" Or CvoConcepto.ToString = "35" Or CvoConcepto.ToString = "36" Or CvoConcepto.ToString = "37" Or CvoConcepto.ToString = "38" Or CvoConcepto.ToString = "40" Or CvoConcepto.ToString = "41" Or CvoConcepto.ToString = "43" Or CvoConcepto.ToString = "45" Or CvoConcepto.ToString = "46" Or CvoConcepto.ToString = "47" Or CvoConcepto.ToString = "48" Then
            '        PrevisionSocial = PrevisionSocial + ImporteIncidencia
            '    ElseIf CvoConcepto.ToString = "12" Or CvoConcepto.ToString = "17" Or CvoConcepto.ToString = "18" Or CvoConcepto.ToString = "19" Or CvoConcepto.ToString = "20" Or CvoConcepto.ToString = "21" Or CvoConcepto.ToString = "22" Or CvoConcepto.ToString = "23" Or CvoConcepto.ToString = "24" Or CvoConcepto.ToString = "25" Or CvoConcepto.ToString = "26" Or CvoConcepto.ToString = "27" Or CvoConcepto.ToString = "28" Or CvoConcepto.ToString = "29" Or CvoConcepto.ToString = "30" Or CvoConcepto.ToString = "31" Or CvoConcepto.ToString = "32" Or CvoConcepto.ToString = "39" Or CvoConcepto.ToString = "49" Or CvoConcepto.ToString = "51" Or CvoConcepto.ToString = "167" Or CvoConcepto.ToString = "168" Or CvoConcepto.ToString = "169" Or CvoConcepto.ToString = "170" Or CvoConcepto.ToString = "171" Then
            '        '12,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,39,49,51
            '        '167,168,169,170,171
            '        GrupoPercepcionesGravadasTotalmenteSinExentos = GrupoPercepcionesGravadasTotalmenteSinExentos + ImporteIncidencia
            '    ElseIf CvoConcepto.ToString = "57" Or CvoConcepto.ToString = "58" Or CvoConcepto.ToString = "59" Or CvoConcepto.ToString = "161" Or CvoConcepto.ToString = "162" Then
            '        'deducciones
            '        'ooooooooooooooooooooooooooooooooooooooooooooooo
            '        ' aqui se descuenta de la cuota periodo el Importe de las faltas, permisos e incapacidades
            '        DiasFaltasPermisosIncapacidades = DiasFaltasPermisosIncapacidades + UnidadIncidencia
            '        FaltasPermisosIncapacidades = FaltasPermisosIncapacidades + ImporteIncidencia
            '        'ooooooooooooooooooooooooooooooooooooooooooooooo
            '    End If
            'End If

            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            DiasCuotaPeriodo = DiasCuotaPeriodo - DiasFaltasPermisosIncapacidades
            CuotaPeriodo = CuotaPeriodo - FaltasPermisosIncapacidades
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooo

            If DescansoTrabajado > 0 Then
                '****************** Modificacion DESCANSO TRABAJADO 17/06/2021 '******************
                ExentoDescansoTrabajado = UMA * 5
                If CuotaDiaria > SalarioMinimoDiarioGeneral Then
                    If (DescansoTrabajado / 2) < ExentoDescansoTrabajado Then
                        ImporteExento = DescansoTrabajado / 2
                        ImporteGravado = DescansoTrabajado / 2
                        DescansoTrabajadoExento = DescansoTrabajado / 2
                        DescansoTrabajadoGravado = DescansoTrabajado / 2
                    ElseIf (DescansoTrabajado / 2) >= ExentoDescansoTrabajado Then
                        ImporteExento = ExentoDescansoTrabajado
                        ImporteGravado = DescansoTrabajado - ExentoDescansoTrabajado
                        DescansoTrabajadoExento = ExentoDescansoTrabajado
                        DescansoTrabajadoGravado = DescansoTrabajado - ExentoDescansoTrabajado
                    End If
                Else
                    ImporteExento = DescansoTrabajado
                    ImporteGravado = 0
                    DescansoTrabajadoExento = DescansoTrabajado
                    DescansoTrabajadoGravado = 0
                End If
                'If (DescansoTrabajado / 2) < (SalarioMinimoDiarioGeneral * 5) Then
                '    ImporteExento = DescansoTrabajado / 2
                '    ImporteGravado = DescansoTrabajado / 2
                '    DescansoTrabajadoExento = DescansoTrabajado / 2
                '    DescansoTrabajadoGravado = DescansoTrabajado / 2
                'Else
                '    ImporteExento = SalarioMinimoDiarioGeneral * 5
                '    ImporteGravado = DescansoTrabajado - (SalarioMinimoDiarioGeneral * 5)
                '    DescansoTrabajadoExento = SalarioMinimoDiarioGeneral * 5
                '    DescansoTrabajadoGravado = DescansoTrabajado - (SalarioMinimoDiarioGeneral * 5)
                'End If
                GuardarExentoYGravado(8, DescansoTrabajadoGravado, DescansoTrabajadoExento, NoEmpleado, 0)
            End If

            If FestivoTrabajado > 0 Then
                '****************** Modificacion DESCANSO TRABAJADO 17/06/2021 '******************
                ExentoFestivoTrabajado = UMA * 5
                If CuotaDiaria > SalarioMinimoDiarioGeneral Then
                    If (FestivoTrabajado / 2) < ExentoFestivoTrabajado Then
                        ImporteExento = FestivoTrabajado / 2
                        ImporteGravado = FestivoTrabajado / 2
                        FestivoTrabajadoExento = FestivoTrabajado / 2
                        FestivoTrabajadoGravado = FestivoTrabajado / 2
                    ElseIf (FestivoTrabajado / 2) >= ExentoFestivoTrabajado Then
                        ImporteExento = ExentoFestivoTrabajado
                        ImporteGravado = FestivoTrabajado - ExentoFestivoTrabajado
                        FestivoTrabajadoExento = ExentoFestivoTrabajado
                        FestivoTrabajadoGravado = FestivoTrabajado - ExentoFestivoTrabajado
                    End If
                Else
                    ImporteExento = FestivoTrabajado
                    ImporteGravado = 0
                    FestivoTrabajadoExento = FestivoTrabajado
                    FestivoTrabajadoGravado = 0
                End If
                'If (FestivoTrabajado / 2) < (SalarioMinimoDiarioGeneral * 5) Then
                '    ImporteExento = FestivoTrabajado / 2
                '    ImporteGravado = FestivoTrabajado / 2
                '    FestivoTrabajadoExento = FestivoTrabajado / 2
                '    FestivoTrabajadoGravado = FestivoTrabajado / 2
                'Else
                '    ImporteExento = SalarioMinimoDiarioGeneral * 5
                '    ImporteGravado = FestivoTrabajado - (SalarioMinimoDiarioGeneral * 5)
                '    FestivoTrabajadoExento = SalarioMinimoDiarioGeneral * 5
                '    FestivoTrabajadoGravado = FestivoTrabajado - (SalarioMinimoDiarioGeneral * 5)
                'End If
                GuardarExentoYGravado(9, FestivoTrabajadoGravado, FestivoTrabajadoExento, NoEmpleado, 0)
            End If

            If HorasExtraDobles > 0 Then
                '****************** Modificacion HORAS EXTRA 17/06/2021 '******************
                ExentoHorasExtra = UMA * 5
                If CuotaDiaria > SalarioMinimoDiarioGeneral Then
                    If (HorasExtraDobles / 2) < ExentoHorasExtra Then
                        ImporteExento = HorasExtraDobles / 2
                        ImporteGravado = HorasExtraDobles / 2
                        HorasExtraDoblesExentas = HorasExtraDobles / 2
                        HorasExtraDoblesGravadas = HorasExtraDobles / 2
                    ElseIf (HorasExtraDobles / 2) >= ExentoHorasExtra Then
                        ImporteExento = ExentoHorasExtra
                        ImporteGravado = HorasExtraDobles - ExentoHorasExtra
                        HorasExtraDoblesExentas = ExentoHorasExtra
                        HorasExtraDoblesGravadas = HorasExtraDobles - ExentoHorasExtra
                    End If
                Else
                    ImporteExento = HorasExtraDobles
                    ImporteGravado = 0
                    HorasExtraDoblesExentas = HorasExtraDobles
                    HorasExtraDoblesGravadas = 0
                End If
                'If (HorasExtraDobles / 2) < (SalarioMinimoDiarioGeneral * 5) Then
                '    ImporteExento = HorasExtraDobles / 2
                '    ImporteGravado = HorasExtraDobles / 2
                '    HorasExtraDoblesExentas = HorasExtraDobles / 2
                '    HorasExtraDoblesGravadas = HorasExtraDobles / 2
                'Else
                '    ImporteExento = SalarioMinimoDiarioGeneral * 5
                '    ImporteGravado = HorasExtraDobles - (SalarioMinimoDiarioGeneral * 5)
                '    HorasExtraDoblesExentas = SalarioMinimoDiarioGeneral * 5
                '    HorasExtraDoblesGravadas = HorasExtraDobles - (SalarioMinimoDiarioGeneral * 5)
                'End If
                GuardarExentoYGravado(10, HorasExtraDoblesGravadas, HorasExtraDoblesExentas, NoEmpleado, "01")
            End If

            If HorasExtraTriples > 0 Then
                'ImporteGravado = ImporteGravado + HorasExtraTriples
                GuardarExentoYGravado(10, HorasExtraTriples, 0, NoEmpleado, "02")
            End If

            If PrimaDominical > 0 And Agregar = 0 Then
                Dim Dias As Integer
                Dias = dt.Compute("Sum(UNIDAD)", "CvoConcepto=13")
                If PrimaDominical < (SalarioMinimoDiarioGeneral * Dias) Then
                    ImporteExento = ImporteExento + PrimaDominical
                Else
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * Dias)
                    ImporteGravado = ImporteGravado + (PrimaDominical - (SalarioMinimoDiarioGeneral * Dias))
                End If
            ElseIf PrimaDominical > 0 And Agregar = 1 Then
                If CvoConcepto.ToString = "13" And ImporteIncidencia <= SalarioMinimoDiarioGeneral Then
                    ImporteExento = ImporteExento + PrimaDominical
                ElseIf CvoConcepto.ToString = "13" And ImporteIncidencia > SalarioMinimoDiarioGeneral Then
                    ImporteExento = ImporteExento + SalarioMinimoDiarioGeneral
                    ImporteGravado = ImporteGravado + (ImporteIncidencia - SalarioMinimoDiarioGeneral)
                End If
            End If
            If Aguinaldo > 0 And Aguinaldo < (SalarioMinimoDiarioGeneral * 30) Then
                ImporteExento = ImporteExento + Aguinaldo
            ElseIf Aguinaldo > 0 And Aguinaldo > (SalarioMinimoDiarioGeneral * 30) Then
                ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 30)
                ImporteGravado = ImporteGravado + (Aguinaldo - (SalarioMinimoDiarioGeneral * 30))
            End If
            If PrimaVacacional > 0 And PrimaVacacional < (SalarioMinimoDiarioGeneral * 15) Then
                ImporteExento = ImporteExento + PrimaVacacional
            ElseIf PrimaVacacional > 0 And PrimaVacacional > (SalarioMinimoDiarioGeneral * 15) Then
                ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 15)
                ImporteGravado = ImporteGravado + (PrimaVacacional - (SalarioMinimoDiarioGeneral * 15))
            End If
            If RepartoUtilidades > 0 And RepartoUtilidades < (SalarioMinimoDiarioGeneral * 15) Then
                ImporteExento = ImporteExento + RepartoUtilidades
            ElseIf RepartoUtilidades > 0 And RepartoUtilidades > (SalarioMinimoDiarioGeneral * 15) Then
                ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 15)
                ImporteGravado = ImporteGravado + (RepartoUtilidades - (SalarioMinimoDiarioGeneral * 15))
            End If
            'Las horas extra triples son gravado al 100%, no tiene nada exento igual que las vacaciones y la Cuota del periodo
            ImporteGravado = ImporteGravado + HorasExtraTriples + Vacaciones + CuotaPeriodo
            'El fondo de ahorro y ayuda para funeral son exentos total siempre asi que se van directo a ImporteExento sin ningun chequeo mas
            ImporteExento = ImporteExento + FondoAhorro + AyudaFuneral

            If GrupoPercepcionesGravadasTotalmenteSinExentos > 0 Then
                ImporteGravado = ImporteGravado + GrupoPercepcionesGravadasTotalmenteSinExentos
            End If
            If HonorarioAsimilado > 0 Then
                ImporteGravado = ImporteGravado + HonorarioAsimilado
            End If
            If PagoPorHoras > 0 Then
                ImporteGravado = ImporteGravado + PagoPorHoras
            End If
            If Comisiones > 0 Then
                ImporteGravado = ImporteGravado + Comisiones
            End If
            If Destajo > 0 Then
                ImporteGravado = ImporteGravado + Destajo
            End If
            'checar
            'If PrevisionSocial > 0 Then
            '    If ImporteGravado + ImporteExento + PrevisionSocial < (SalarioMinimoDiarioGeneral * 7) Then
            '        ImporteExento = ImporteExento + PrevisionSocial
            '    ElseIf ImporteGravado + ImporteExento + PrevisionSocial > (SalarioMinimoDiarioGeneral * 7) And ImporteGravado + ImporteExento > (SalarioMinimoDiarioGeneral * 7) Then
            '        ImporteExento = ImporteExento + SalarioMinimoDiarioGeneral
            '        ImporteGravado = ImporteGravado + (PrevisionSocial - SalarioMinimoDiarioGeneral)
            '    ElseIf ImporteGravado + ImporteExento + PrevisionSocial > (SalarioMinimoDiarioGeneral * 7) And ImporteGravado + ImporteExento + SalarioMinimoDiarioGeneral < (SalarioMinimoDiarioGeneral * 7) Then
            '        ImporteExento = ImporteExento + ((SalarioMinimoDiarioGeneral * 7) - (ImporteGravado + ImporteExento))
            '        ImporteGravado = ImporteGravado + (PrevisionSocial - ((SalarioMinimoDiarioGeneral * 7) - (ImporteGravado + ImporteExento)))
            '    ElseIf ImporteGravado + ImporteExento + PrevisionSocial > (SalarioMinimoDiarioGeneral * 7) And ((SalarioMinimoDiarioGeneral * 7) - (ImporteGravado + ImporteExento) < SalarioMinimoDiarioGeneral) Then
            '        ImporteExento = ImporteExento + SalarioMinimoDiarioGeneral
            '    End If
            'End If
            If PrevisionSocial > 0 Then
                If PrevisionSocial < (SalarioMinimoDiarioGeneral * 7) Then
                    ImporteExento = ImporteExento + PrevisionSocial
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial < ((SalarioMinimoDiarioGeneral * 7) * 7) Then
                    ImporteExento = ImporteExento + PrevisionSocial
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial > ((SalarioMinimoDiarioGeneral * 7) * 7) And ImporteGravado + ImporteExento > ((SalarioMinimoDiarioGeneral * 7) * 7) Then
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 7)
                    ImporteGravado = ImporteGravado + (PrevisionSocial - (SalarioMinimoDiarioGeneral * 7))
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial > ((SalarioMinimoDiarioGeneral * 7) * 7) And ImporteGravado + ImporteExento + (SalarioMinimoDiarioGeneral * 7) < ((SalarioMinimoDiarioGeneral * 7) * 7) Then
                    ImporteExento = ImporteExento + (((SalarioMinimoDiarioGeneral * 7) * 7) - (ImporteGravado + ImporteExento))
                    ImporteGravado = ImporteGravado + (PrevisionSocial - (((SalarioMinimoDiarioGeneral * 7) * 7) - (ImporteGravado + ImporteExento)))
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial > ((SalarioMinimoDiarioGeneral * 7) * 7) And (((SalarioMinimoDiarioGeneral * 7) * 7) - (ImporteGravado + ImporteExento) < SalarioMinimoDiarioGeneral * 7) Then
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 7)
                    ImporteGravado = ImporteGravado + (PrevisionSocial - (SalarioMinimoDiarioGeneral * 7))
                End If
            End If
            'Estos tipos de percepcion son independientes unos de otros, en teoria no deberian ir juntos nunca, como conllevan implicitamente 7 dias del periodo actual, si estan juntos solo se toman en cuenta 7 dias para todos
            'El enfoque de que no deberian ir juntos nunca es que a un trabajador de sueldo no se le paga por horas, ni por comision, ni por destajo, si se hace se procede con los dias como se explica en el renglon anterior
            If DiasCuotaPeriodo > 0 Then
                DiasPagoPorHoras = 0
                DiasComision = 0
                DiasDestajo = 0
            ElseIf DiasPagoPorHoras > 0 Then
                DiasCuotaPeriodo = 0
                DiasComision = 0
                DiasDestajo = 0
            ElseIf DiasComision > 0 Then
                DiasPagoPorHoras = 0
                DiasCuotaPeriodo = 0
                DiasDestajo = 0
            ElseIf DiasDestajo > 0 Then
                DiasComision = 0
                DiasPagoPorHoras = 0
                DiasCuotaPeriodo = 0
            End If
            If ImporteGravado > 0 And (DiasCuotaPeriodo + DiasVacaciones + DiasComision + DiasPagoPorHoras + DiasDestajo + DiasHonorarioAsimilado > 0) Then
                ImporteDiario = ImporteGravado / (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo + DiasHonorarioAsimilado)
            Else
                ImporteDiario = 0
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    'Private Sub CalcularImpuesto()
    '    Try
    '        Impuesto = 0
    '        Dim ImporteSemanal As Decimal
    '        ImporteSemanal = ImporteDiario * (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo + DiasHonorarioAsimilado)
    '        Dim dt As New DataTable()
    '        Dim TarifaSemanal As New TarifaSemanal()
    '        TarifaSemanal.ImporteSemanal = ImporteSemanal
    '        dt = TarifaSemanal.ConsultarTarifa()

    '        If dt.Rows.Count > 0 Then
    '            Impuesto = ((ImporteSemanal - dt.Rows(0).Item("LimiteInferior")) * (dt.Rows(0).Item("PorcSobreExcli") / 100)) + dt.Rows(0).Item("CuotaFija")
    '        End If
    '    Catch oExcep As Exception
    '        rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
    '    End Try
    'End Sub
    Private Sub CalcularImpuesto()
        Try
            Impuesto = 0
            Dim dt As New DataTable()
            Dim TarifaDiaria As New TarifaDiaria()
            TarifaDiaria.ImporteDiario = ImporteDiario
            dt = TarifaDiaria.ConsultarValoresTarifaDiaria()

            If dt.Rows.Count > 0 Then
                Impuesto = ((ImporteDiario - dt.Rows(0).Item("LimiteInferior")) * (dt.Rows(0).Item("PorcSobreExcli") / 100)) + dt.Rows(0).Item("CuotaFija")
            End If

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    'Private Sub CalcularSubsidio()
    '    Try
    '        If HonorarioAsimilado > 0 And ImporteGravado < HonorarioAsimilado Then
    '            ImporteGravado = ImporteGravado - HonorarioAsimilado
    '            ImporteDiario = ImporteGravado / (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo)
    '        End If

    '        Subsidio = 0
    '        Dim ImporteSemanal As Decimal
    '        ImporteSemanal = ImporteDiario * (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo + DiasHonorarioAsimilado)
    '        Dim dt As New DataTable()
    '        Dim TablaSubsidioSemanal As New TablaSubsidioSemanal()
    '        TablaSubsidioSemanal.ImporteSemanal = ImporteSemanal
    '        dt = TablaSubsidioSemanal.ConsultarSubsidio()

    '        If dt.Rows.Count > 0 Then
    '            Subsidio = dt.Rows(0).Item("Subsidio")
    '        End If
    '        If HonorarioAsimilado > 0 Then
    '            ImporteGravado = ImporteGravado + HonorarioAsimilado
    '            ImporteDiario = ImporteGravado / (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo + DiasHonorarioAsimilado)
    '        End If
    '    Catch oExcep As Exception
    '        rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
    '    End Try
    'End Sub
    Private Sub CalcularSubsidio()
        Try
            Subsidio = 0
            Dim dt As New DataTable()
            Dim TablaSubsidioDiario As New TablaSubsidioDiario()
            TablaSubsidioDiario.Importe = ImporteDiario
            dt = TablaSubsidioDiario.ConsultarSubsidioDiario()

            If dt.Rows.Count > 0 Then
                Subsidio = dt.Rows(0).Item("Subsidio")
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CalcularImss()

        Imss = 0
        Call CargarVariablesGenerales()

        If SalarioDiarioIntegradoTrabajador <= SalarioMinimoDiarioGeneral Then
            Imss = 0
        ElseIf SalarioDiarioIntegradoTrabajador > SalarioMinimoDiarioGeneral And SalarioDiarioIntegradoTrabajador < (SalarioMinimoDiarioGeneral * 3) Then
            Imss = SalarioDiarioIntegradoTrabajador * 0.02375
        ElseIf SalarioDiarioIntegradoTrabajador > (SalarioMinimoDiarioGeneral * 3) And SalarioDiarioIntegradoTrabajador < (SalarioMinimoDiarioGeneral * 25) Then
            Imss = SalarioDiarioIntegradoTrabajador * 0.02375
            Imss = Imss + ((SalarioDiarioIntegradoTrabajador - (SalarioMinimoDiarioGeneral * 3)) * 0.004)
        ElseIf SalarioDiarioIntegradoTrabajador > (SalarioMinimoDiarioGeneral * 25) Then
            Imss = (SalarioDiarioIntegradoTrabajador - (SalarioMinimoDiarioGeneral * 25)) * 0.02375
            Imss = Imss + ((SalarioDiarioIntegradoTrabajador - (SalarioMinimoDiarioGeneral * 22)) * 0.004)
        End If

    End Sub
    Private Sub GuardarRegistro(ByVal CuotaDiaria, ByVal ConImpuesto)
        Try

            Dim cPeriodo As New Entities.Periodo()
            cPeriodo.IdPeriodo = periodoId.Value
            cPeriodo.ConsultarPeriodoID()

            Call CargarVariablesGenerales()

            Dim ImporteIncidencia As Decimal = 0
            Dim UnidadIncidencia As Decimal = 0

            Try
                ImporteIncidencia = Math.Round(Convert.ToDecimal(txtImporteIncidencia.Text), 6)
            Catch ex As Exception
                ImporteIncidencia = 0
            End Try

            Try
                UnidadIncidencia = Convert.ToDecimal(txtUnidadIncidencia.Text)
            Catch ex As Exception
                UnidadIncidencia = 0
            End Try

            'ConImpuesto = 1 cuando viene de agregar una percepcion y calcular impuestos guardando ambos
            'ConImpuesto = 2 cuando viene de quitar una percepcion y solo se procede a guardar los impuesto modificados sin esa percepcion

            Dim cNomina As New Nomina()
            cNomina = New Nomina()
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = Periodo
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.CvoConcepto = 87
            cNomina.TipoConcepto = "DE"
            cNomina.EliminaConceptoEmpleado()

            cNomina = New Nomina()
            cNomina.Cliente = empresaId.Value
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = Periodo
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.CvoConcepto = 87
            cNomina.IdContrato = contratoId.Value
            cNomina.TipoConcepto = "DE"
            cNomina.Unidad = 1
            cNomina.Importe = CuotaDiaria
            cNomina.Generado = ""
            cNomina.Timbrado = ""
            cNomina.Enviado = ""
            cNomina.Situacion = "A"
            cNomina.EsEspecial = False
            cNomina.FechaIni = cPeriodo.FechaInicialDate
            cNomina.FechaFin = cPeriodo.FechaFinalDate
            cNomina.FechaPago = cPeriodo.FechaPago
            cNomina.DiasPagados = cPeriodo.Dias
            cNomina.IdNomina = nominaId.Value
            cNomina.GuadarNominaPeriodo()

            If ConImpuesto = 1 Then
                If cmbConcepto.SelectedValue <= 51 Or cmbConcepto.SelectedValue = 165 Or cmbConcepto.SelectedValue = 166 Or cmbConcepto.SelectedValue = 167 Or cmbConcepto.SelectedValue = 168 Or cmbConcepto.SelectedValue = 169 Or cmbConcepto.SelectedValue = 170 Or cmbConcepto.SelectedValue = 171 Then
                    cNomina = New Nomina()
                    cNomina.Cliente = empresaId.Value
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 2 'Catorcenal
                    cNomina.Periodo = Periodo
                    cNomina.NoEmpleado = empleadoId.Value
                    cNomina.CvoConcepto = cmbConcepto.SelectedValue.ToString
                    cNomina.IdContrato = contratoId.Value
                    cNomina.TipoConcepto = "P"
                    cNomina.Unidad = UnidadIncidencia
                    cNomina.Importe = ImporteIncidencia
                    cNomina.ImporteGravado = 0
                    If cmbConcepto.SelectedValue = 32 Or cmbConcepto.SelectedValue = 165 Or cmbConcepto.SelectedValue = 166 Then
                        cNomina.ImporteExento = ImporteIncidencia
                    Else
                        cNomina.ImporteExento = 0
                    End If
                    cNomina.Generado = ""
                    cNomina.Timbrado = ""
                    cNomina.Enviado = ""
                    cNomina.Situacion = "A"
                    If cmbConcepto.SelectedValue = 10 Then
                        cNomina.DiasHorasExtra = txtDiasHorasExtra.Text
                        cNomina.TipoHorasExtra = cmbTipoHorasExtra.SelectedValue
                    End If
                    cNomina.EsEspecial = False
                    cNomina.FechaIni = cPeriodo.FechaInicialDate
                    cNomina.FechaFin = cPeriodo.FechaFinalDate
                    cNomina.FechaPago = cPeriodo.FechaPago
                    cNomina.DiasPagados = cPeriodo.Dias
                    cNomina.IdNomina = nominaId.Value
                    cNomina.GuadarNominaPeriodo()
                ElseIf cmbConcepto.SelectedValue = 82 Then
                    cNomina = New Nomina()
                    cNomina.Cliente = empresaId.Value
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 2 'Catorcenal
                    cNomina.Periodo = Periodo
                    cNomina.NoEmpleado = empleadoId.Value
                    cNomina.CvoConcepto = cmbConcepto.SelectedValue.ToString
                    cNomina.IdContrato = contratoId.Value
                    cNomina.TipoConcepto = "P"
                    cNomina.Unidad = 1
                    cNomina.Importe = ImporteIncidencia
                    cNomina.ImporteExento = ImporteIncidencia
                    cNomina.Generado = ""
                    cNomina.Timbrado = ""
                    cNomina.Enviado = ""
                    cNomina.Situacion = "A"
                    cNomina.EsEspecial = False
                    cNomina.FechaIni = cPeriodo.FechaInicialDate
                    cNomina.FechaFin = cPeriodo.FechaFinalDate
                    cNomina.FechaPago = cPeriodo.FechaPago
                    cNomina.DiasPagados = cPeriodo.Dias
                    cNomina.IdNomina = nominaId.Value
                    cNomina.GuadarNominaPeriodo()
                    cNomina = Nothing
                ElseIf cmbConcepto.SelectedValue.ToString = "57" Or cmbConcepto.SelectedValue.ToString = "58" Or cmbConcepto.SelectedValue.ToString = "59" Or cmbConcepto.SelectedValue.ToString = "161" Or cmbConcepto.SelectedValue.ToString = "162" Then
                    cNomina = New Nomina()
                    cNomina.Cliente = empresaId.Value
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 2 'Catorcenal
                    cNomina.Periodo = Periodo
                    cNomina.NoEmpleado = empleadoId.Value
                    cNomina.CvoConcepto = cmbConcepto.SelectedValue.ToString
                    cNomina.IdContrato = contratoId.Value
                    cNomina.TipoConcepto = "D"
                    cNomina.Unidad = UnidadIncidencia
                    cNomina.Importe = ImporteIncidencia
                    cNomina.ImporteGravado = 0
                    cNomina.ImporteExento = ImporteIncidencia
                    cNomina.Generado = ""
                    cNomina.Timbrado = ""
                    cNomina.Enviado = ""
                    cNomina.Situacion = "A"
                    cNomina.EsEspecial = False
                    cNomina.FechaIni = cPeriodo.FechaInicialDate
                    cNomina.FechaFin = cPeriodo.FechaFinalDate
                    cNomina.FechaPago = cPeriodo.FechaPago
                    cNomina.DiasPagados = cPeriodo.Dias
                    cNomina.IdNomina = nominaId.Value
                    cNomina.GuadarNominaPeriodo()
                ElseIf cmbConcepto.SelectedValue.ToString >= 61 Then
                    cNomina = New Nomina()
                    cNomina.Cliente = empresaId.Value
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 2 'Catorcenal
                    cNomina.Periodo = Periodo
                    cNomina.NoEmpleado = empleadoId.Value
                    cNomina.CvoConcepto = cmbConcepto.SelectedValue.ToString
                    cNomina.IdContrato = contratoId.Value
                    cNomina.TipoConcepto = "D"
                    cNomina.Unidad = UnidadIncidencia
                    cNomina.Importe = ImporteIncidencia
                    cNomina.ImporteGravado = 0
                    cNomina.ImporteExento = ImporteIncidencia
                    cNomina.Generado = ""
                    cNomina.Timbrado = ""
                    cNomina.Enviado = ""
                    cNomina.Situacion = "A"
                    cNomina.EsEspecial = False
                    cNomina.FechaIni = cPeriodo.FechaInicialDate
                    cNomina.FechaFin = cPeriodo.FechaFinalDate
                    cNomina.FechaPago = cPeriodo.FechaPago
                    cNomina.DiasPagados = cPeriodo.Dias
                    cNomina.IdNomina = nominaId.Value
                    cNomina.GuadarNominaPeriodo()
                End If
            End If

            'Aqui se guarda el impuesto, el total Gravado y el total exento, tanto cuando viene de agregar un concepto como cuando viene de quitar un concepto ya que de ambas maneras se recalcula
            'solo no entra en este bloque de codigo cuando viene de agregar una deduccion que no implica recalcular gravado, exento o impuesto(las unicas deducciones que recalculan gravado, exento e impuesto son la faltas, permisos e incapacidades, las demas deduccioens haciendo hincapie, no entran en este bloque)
            If ConImpuesto = 1 Then
                If cmbConcepto.SelectedValue < 52 Or cmbConcepto.SelectedValue.ToString = "57" Or cmbConcepto.SelectedValue.ToString = "58" Or cmbConcepto.SelectedValue.ToString = "59" Or cmbConcepto.SelectedValue.ToString = "161" Or cmbConcepto.SelectedValue.ToString = "162" Or cmbConcepto.SelectedValue.ToString = "167" Or cmbConcepto.SelectedValue.ToString = "168" Or cmbConcepto.SelectedValue.ToString = "169" Or cmbConcepto.SelectedValue.ToString = "170" Or cmbConcepto.SelectedValue.ToString = "171" Then
                    If Impuesto > 0 Then
                        cNomina = New Nomina()
                        cNomina.Cliente = empresaId.Value
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 2 'Catorcenal
                        cNomina.Periodo = Periodo
                        cNomina.NoEmpleado = empleadoId.Value
                        cNomina.CvoConcepto = 52
                        cNomina.IdContrato = contratoId.Value
                        cNomina.TipoConcepto = "D"
                        cNomina.Unidad = 1
                        cNomina.Importe = Impuesto
                        cNomina.ImporteGravado = 0
                        cNomina.ImporteExento = Impuesto
                        cNomina.Generado = ""
                        cNomina.Timbrado = ""
                        cNomina.Enviado = ""
                        cNomina.Situacion = "A"
                        cNomina.EsEspecial = False
                        cNomina.FechaIni = cPeriodo.FechaInicialDate
                        cNomina.FechaFin = cPeriodo.FechaFinalDate
                        cNomina.FechaPago = cPeriodo.FechaPago
                        cNomina.DiasPagados = cPeriodo.Dias
                        cNomina.IdNomina = nominaId.Value
                        cNomina.GuadarNominaPeriodo()
                    End If

                    If Imss > 0 Then
                        cNomina = New Nomina()
                        cNomina.Cliente = empresaId.Value
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 2 'Catorcenal
                        cNomina.Periodo = Periodo
                        cNomina.NoEmpleado = empleadoId.Value
                        cNomina.CvoConcepto = 56
                        cNomina.IdContrato = contratoId.Value
                        cNomina.TipoConcepto = "D"
                        cNomina.Unidad = 1
                        cNomina.Importe = Imss
                        cNomina.ImporteGravado = 0
                        cNomina.ImporteExento = Imss
                        cNomina.Generado = ""
                        cNomina.Timbrado = ""
                        cNomina.Enviado = ""
                        cNomina.Situacion = "A"
                        cNomina.EsEspecial = False
                        cNomina.FechaIni = cPeriodo.FechaInicialDate
                        cNomina.FechaFin = cPeriodo.FechaFinalDate
                        cNomina.FechaPago = cPeriodo.FechaPago
                        cNomina.DiasPagados = cPeriodo.Dias
                        cNomina.IdNomina = nominaId.Value
                        cNomina.GuadarNominaPeriodo()
                    End If
                End If
            ElseIf ConImpuesto = 2 Then
                If cmbConcepto.SelectedValue = "82" Then
                    cNomina = New Nomina()
                    cNomina.Cliente = empresaId.Value
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 2 'Catorcenal
                    cNomina.Periodo = Periodo
                    cNomina.NoEmpleado = empleadoId.Value
                    cNomina.CvoConcepto = cmbConcepto.SelectedValue.ToString
                    cNomina.IdContrato = contratoId.Value
                    cNomina.TipoConcepto = "P"
                    cNomina.Unidad = 1
                    cNomina.Importe = ImporteIncidencia
                    cNomina.ImporteExento = ImporteIncidencia
                    cNomina.Generado = ""
                    cNomina.Timbrado = ""
                    cNomina.Enviado = ""
                    cNomina.Situacion = "A"
                    cNomina.EsEspecial = False
                    cNomina.FechaIni = cPeriodo.FechaInicialDate
                    cNomina.FechaFin = cPeriodo.FechaFinalDate
                    cNomina.FechaPago = cPeriodo.FechaPago
                    cNomina.DiasPagados = cPeriodo.Dias
                    cNomina.IdNomina = nominaId.Value
                    cNomina.GuadarNominaPeriodo()
                    cNomina = Nothing
                ElseIf cmbConcepto.SelectedValue.ToString >= 61 Then
                    cNomina = New Nomina()
                    cNomina.Cliente = empresaId.Value
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 2 'Catorcenal
                    cNomina.Periodo = Periodo
                    cNomina.NoEmpleado = empleadoId.Value
                    cNomina.CvoConcepto = cmbConcepto.SelectedValue.ToString
                    cNomina.IdContrato = contratoId.Value
                    cNomina.TipoConcepto = "D"
                    cNomina.Unidad = UnidadIncidencia
                    cNomina.Importe = ImporteIncidencia
                    cNomina.ImporteGravado = 0
                    cNomina.ImporteExento = ImporteIncidencia
                    cNomina.Generado = ""
                    cNomina.Timbrado = ""
                    cNomina.Enviado = ""
                    cNomina.Situacion = "A"
                    cNomina.EsEspecial = False
                    cNomina.FechaIni = cPeriodo.FechaInicialDate
                    cNomina.FechaFin = cPeriodo.FechaFinalDate
                    cNomina.FechaPago = cPeriodo.FechaPago
                    cNomina.DiasPagados = cPeriodo.Dias
                    cNomina.IdNomina = nominaId.Value
                    cNomina.GuadarNominaPeriodo()
                End If
            End If

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub txtUnidadIncidencia_TextChanged(sender As Object, e As EventArgs) Handles txtUnidadIncidencia.TextChanged
        Dim FactorDestajo, AsimiladoTotalSemanal, PagoPorHora As Decimal
        Dim cEmpleado As New Entities.Empleado
        cEmpleado.IdEmpleado = empleadoId.Value
        cEmpleado.ConsultarEmpleadoID()

        If cEmpleado.IdEmpleado > 0 Then
            FactorDestajo = cEmpleado.FactorDestajo
            AsimiladoTotalSemanal = cEmpleado.AsimiladoTotalSemanal
            PagoPorHora = cEmpleado.PagoPorHora
        End If

        Try
            CuotaDiaria = Convert.ToDecimal(txtCuotaDiaria.Text)
        Catch ex As Exception
            CuotaDiaria = 0
        End Try

        Dim UnidadIncidencia As Decimal = 0
        Try
            UnidadIncidencia = Convert.ToDecimal(txtUnidadIncidencia.Text)
        Catch ex As Exception
            UnidadIncidencia = 0
        End Try

        If cmbConcepto.SelectedValue.ToString = "2" And UnidadIncidencia > 0 Then
            txtImporteIncidencia.Text = CuotaDiaria * UnidadIncidencia
        ElseIf cmbConcepto.SelectedValue.ToString = "4" And UnidadIncidencia > 0 Then
            txtImporteIncidencia.Text = FactorDestajo * UnidadIncidencia
        ElseIf cmbConcepto.SelectedValue.ToString = "5" And UnidadIncidencia > 0 Then
            txtImporteIncidencia.Text = AsimiladoTotalSemanal * UnidadIncidencia
        ElseIf cmbConcepto.SelectedValue.ToString = "6" And UnidadIncidencia > 0 Then
            txtImporteIncidencia.Text = ((CuotaDiaria / 8) * 2) * UnidadIncidencia
        ElseIf cmbConcepto.SelectedValue.ToString = "7" And UnidadIncidencia > 0 Then
            txtImporteIncidencia.Text = ((CuotaDiaria / 8) * 3) * UnidadIncidencia
        ElseIf cmbConcepto.SelectedValue.ToString = "8" And UnidadIncidencia > 0 Then
            txtImporteIncidencia.Text = (CuotaDiaria * 2) * UnidadIncidencia
        ElseIf cmbConcepto.SelectedValue.ToString = "9" And UnidadIncidencia > 0 Then
            txtImporteIncidencia.Text = (CuotaDiaria * 2) * UnidadIncidencia
        ElseIf cmbConcepto.SelectedValue.ToString = "10" And UnidadIncidencia > 0 Then
            Try
                CuotaDiaria = Convert.ToDecimal(txtCuotaDiaria.Text)
            Catch ex As Exception
                CuotaDiaria = 0
            End Try

            If cmbTipoHorasExtra.SelectedValue = "01" Then 'Dobles
                txtImporteIncidencia.Text = ((CuotaDiaria / 8) * 2) * UnidadIncidencia
            ElseIf cmbTipoHorasExtra.SelectedValue = "02" Then 'Triples
                txtImporteIncidencia.Text = ((CuotaDiaria / 8) * 3) * UnidadIncidencia
            ElseIf cmbTipoHorasExtra.SelectedValue = "03" Then 'Simples
                txtImporteIncidencia.Text = (CuotaDiaria / 8) * UnidadIncidencia
            End If
        ElseIf cmbConcepto.SelectedValue.ToString = "11" And UnidadIncidencia > 0 Then
            txtImporteIncidencia.Text = PagoPorHora * UnidadIncidencia
        ElseIf cmbConcepto.SelectedValue.ToString = "13" And UnidadIncidencia > 0 Then
            txtImporteIncidencia.Text = (CuotaDiaria / 4) * UnidadIncidencia
        ElseIf cmbConcepto.SelectedValue.ToString = "14" And UnidadIncidencia > 0 Then
            txtImporteIncidencia.Text = CuotaDiaria * UnidadIncidencia
        ElseIf cmbConcepto.SelectedValue.ToString = "15" And UnidadIncidencia > 0 Then
            txtImporteIncidencia.Text = CuotaDiaria * UnidadIncidencia
        ElseIf cmbConcepto.SelectedValue.ToString = "16" And UnidadIncidencia > 0 Then
            txtImporteIncidencia.Text = ((CuotaDiaria * UnidadIncidencia) / 4)
        ElseIf cmbConcepto.SelectedValue.ToString = "51" And UnidadIncidencia > 0 Then
            txtImporteIncidencia.Text = CuotaDiaria * UnidadIncidencia
        ElseIf cmbConcepto.SelectedValue.ToString = "57" And UnidadIncidencia > 0 Then
            txtImporteIncidencia.Text = CuotaDiaria * UnidadIncidencia
        ElseIf cmbConcepto.SelectedValue.ToString = "59" And UnidadIncidencia > 0 Then
            txtImporteIncidencia.Text = CuotaDiaria * UnidadIncidencia
        ElseIf cmbConcepto.SelectedValue.ToString = "161" And UnidadIncidencia > 0 Then
            txtImporteIncidencia.Text = CuotaDiaria * UnidadIncidencia
        ElseIf cmbConcepto.SelectedValue.ToString = "162" And UnidadIncidencia > 0 Then
            txtImporteIncidencia.Text = CuotaDiaria * UnidadIncidencia
        End If
        Page.SetFocus(txtImporteIncidencia)
    End Sub
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            Dim Importe As Decimal = 0
            Dim Unidad As Decimal = 0
            Try
                Importe = Convert.ToDecimal(txtImporteIncidencia.Text)
            Catch ex As Exception
                Importe = 0
            End Try
            Try
                Unidad = Convert.ToDecimal(txtUnidadIncidencia.Text)
            Catch ex As Exception
                Unidad = 0
            End Try
            Try
                CuotaDiaria = Convert.ToDecimal(txtCuotaDiaria.Text)
            Catch ex As Exception
                CuotaDiaria = 0
            End Try

            If Importe <= 0 Then
                rwAlerta.RadAlert("Favor de digitar un importe!!", 330, 180, "Alerta", "", "")
                Exit Sub
            ElseIf Unidad <= 0 Then
                rwAlerta.RadAlert("Favor de digitar un unidad!!", 330, 180, "Alerta", "", "")
                Exit Sub
            End If

            Dim ClaveRegimenContratacion As Integer = 0

            Dim cEmpleado As New Entities.Empleado
            cEmpleado.IdEmpleado = empleadoId.Value
            cEmpleado.ConsultarEmpleadoID()

            If cEmpleado.IdEmpleado > 0 Then
                ClaveRegimenContratacion = cEmpleado.IdRegimenContratacion
                SalarioDiarioIntegradoTrabajador = cEmpleado.IntegradoImss
            End If

            If cmbConcepto.SelectedValue.ToString = "5" And ClaveRegimenContratacion <> 9 Then
                rwAlerta.RadAlert("El regimen de contratacion de este trabajador no es honorarios asimilado a salarios!!", 330, 180, "Alerta", "", "")
                Exit Sub
            ElseIf cmbConcepto.SelectedValue.ToString < 52 Or cmbConcepto.SelectedValue.ToString = "57" Or cmbConcepto.SelectedValue.ToString = "58" Or cmbConcepto.SelectedValue.ToString = "59" Or cmbConcepto.SelectedValue.ToString = "161" Or cmbConcepto.SelectedValue.ToString = "162" Then
                If ClaveRegimenContratacion = 9 Then
                    rwAlerta.RadAlert("El régimen de contratación de este trabajador es asimilado a salarios, por lo mismo no se le debe agregar ningún otro tipo de percepción ni hacerle deducciones por faltas, dichas deducciones solo pueden ser por un aduedo distinto!!", 330, 180, "Alerta", "", "")
                    Exit Sub
                End If
            End If

            If cmbConcepto.SelectedValue.ToString = "10" Then
                If cmbTipoHorasExtra.SelectedValue = "01" Then
                    If txtUnidadIncidencia.Text > 9 Then
                        rwAlerta.RadAlert("Las horas extras no pueden ser mas de 9!!", 330, 180, "Alerta", "", "")
                        Exit Sub
                    End If
                End If
            End If

            If ChecarSiExiste(empleadoId.Value, cmbConcepto.SelectedValue) = True Then
                rwAlerta.RadAlert("Esa percepcion/deduccion ya existe!!", 330, 180, "Alerta", "", "")
                Exit Sub
            End If

            'oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            'oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            If cmbConcepto.SelectedValue.ToString = "57" Or cmbConcepto.SelectedValue.ToString = "58" Or cmbConcepto.SelectedValue.ToString = "59" Or cmbConcepto.SelectedValue.ToString = "161" Or cmbConcepto.SelectedValue.ToString = "162" Then
                'si es descuento ya sea por falta, permiso o incapacidad checar si existe el concepto de cuota del periodo ya que de el se debe descontar estas deducciones
                If ChecarQueExistaLaCuotaPeriodo(empleadoId.Value) = False Then
                    rwAlerta.RadAlert("No existe la cuota del periodo o el importe del mismo es menor al importe de la deduccion o los dias a descontar son menores a los existentes, no se puede agregar esta deduccion!!", 330, 180, "Alerta", "", "")
                    Exit Sub
                    'Dim Respuesta As Integer
                    'Respuesta = MsgBox("No existe la cuota del periodo o el importe del mismo es menor al importe de la deduccion o los dias a descontar son menores a los existentes, no se puede agregar esta deduccion a menos que desee dejar el sobre en ceros, No=SALIR Y RECTIFICAR, Si=DEJAR SOBRE EN CEROS", vbYesNo + vbExclamation + vbDefaultButton2, "Alerta")
                    'If Respuesta = vbNo Then
                    '    Exit Sub
                    'End If
                End If
            End If
            'oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            'oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo

            ImporteDiario = 0
            ImportePeriodo = 0
            ImporteExento = 0
            ImporteGravado = 0
            Agregar = 1

            'oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            'Pasos para agregar una percepcion
            'Checar si la incidencia es exenta o gravada para ver si procede el calculo O se guarda el registro directo sin pasar por el calculo
            'Borrar deducciones menos el imss del empleado
            'iterar sobre las percepciones e ir haciendo el calculo de exentos y gravados sumando conceptos
            'hacer el calculo del ispt
            'guardar las nuevas deducciones
            'cargar las nuevas percepciones y deducciones

            ChecarSiExistenDiasEnPercepciones(empleadoId.Value, cmbConcepto.SelectedValue)
            If DiasCuotaPeriodo = 0 And DiasVacaciones = 0 And DiasComision = 0 And DiasPagoPorHoras = 0 And DiasDestajo = 0 And DiasHonorarioAsimilado = 0 Then
                'en este caso, si se pagan conceptos tales como comisiones, en teoria estas se generan posterior a que el trabajador tiene un sueldo, es decir una cuaota del periodo actual, ese es el motivo del mensaje.
                '    MsgBox("Esta percepcion no puede agregarse sin que exista alguna de las siguientes:" + vbCrLf + "CuotaPeriodo" + vbCrLf + "Vacaciones" + vbCrLf + "HonorarioAsimilado" + vbCrLf + "PagoPorHoras" + vbCrLf + "Comision" + vbCrLf + "Destajo" + vbCrLf + "pues estas van acompañadas implicitamente por los 7 dias correspondientes al periodo semanal lo cual es base necesaria para el calculo del impuesto,  lo que si puede hacer es cambiar el numero de dias o eliminar completamente el empleado en este periodo!!!")
                '    Exit Sub
                'End If
                BorrarDeducciones(empleadoId.Value)
            Else
                If cmbConcepto.SelectedValue.ToString < 52 Or cmbConcepto.SelectedValue.ToString = "57" Or cmbConcepto.SelectedValue.ToString = "58" Or cmbConcepto.SelectedValue.ToString = "59" Or cmbConcepto.SelectedValue.ToString = "161" Or cmbConcepto.SelectedValue.ToString = "162" Or cmbConcepto.SelectedValue.ToString = "167" Or cmbConcepto.SelectedValue.ToString = "168" Or cmbConcepto.SelectedValue.ToString = "169" Or cmbConcepto.SelectedValue.ToString = "170" Or cmbConcepto.SelectedValue.ToString = "171" Then
                    'En este bloque no entran las deducciones por adeudos que no cambian la base del impuesto y por lo tanto no lo calculan, ejemplos de esto serian, adeudos por credito infonavit, cuotas sindicales, adeudos fonacot, adeudos con el patron, etc, (conceptos del 61 al 86)
                    Call BorrarDeducciones(empleadoId.Value)
                    Call GuardarRegistro(CuotaDiaria, 1)

                    Call QuitarConcepto(52, "") 'IMPUESTO
                    Call QuitarConcepto(54, "") 'SUBSIDIO
                    Call QuitarConcepto(56, "") 'CUOTA IMSS

                    Call ChecarPercepcionesGravadas(empleadoId.Value, cmbConcepto.SelectedValue)
                    Call ChecarYGrabarPercepcionesExentasYGravadas(empleadoId.Value, 0)
                    Call ChecarPercepcionesExentasYGravadas()

                    Call CalcularImss()

                    Imss = Imss * NumeroDeDiasPagados
                    Imss = Math.Round(Imss, 6)

                    Dim cPeriodo As New Entities.Periodo()
                    cPeriodo.IdPeriodo = periodoId.Value
                    cPeriodo.ConsultarPeriodoID()

                    If Imss > 0 Then
                        Dim cNomina = New Nomina()
                        cNomina.Cliente = empresaId.Value
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 2 'Catorcenal
                        cNomina.Periodo = Periodo
                        cNomina.NoEmpleado = empleadoId.Value
                        cNomina.CvoConcepto = 56
                        cNomina.IdContrato = contratoId.Value
                        cNomina.TipoConcepto = "D"
                        cNomina.Unidad = 1
                        cNomina.Importe = Imss
                        cNomina.ImporteGravado = 0
                        cNomina.ImporteExento = Imss
                        cNomina.Generado = ""
                        cNomina.Timbrado = ""
                        cNomina.Enviado = ""
                        cNomina.Situacion = "A"
                        cNomina.EsEspecial = False
                        cNomina.FechaIni = cPeriodo.FechaInicialDate
                        cNomina.FechaFin = cPeriodo.FechaFinalDate
                        cNomina.FechaPago = cPeriodo.FechaPago
                        cNomina.DiasPagados = cPeriodo.Dias
                        cNomina.IdNomina = nominaId.Value
                        cNomina.GuadarNominaPeriodo()
                    End If

                    Call CalcularImpuesto()

                    Impuesto = Impuesto * NumeroDeDiasPagados
                    Impuesto = Math.Round(Impuesto, 6)

                    SubsidioAplicado = 0
                    ImporteDiarioGravado = 0
                    BaseGravableMensualSubsidioDiario = (BaseGravableMensualSubsidio / FactorDiarioPromedio)
                    ImporteDiarioGravado = PercepcionesGravadas / NumeroDeDiasPagados

                    If ImporteDiarioGravado <= BaseGravableMensualSubsidioDiario Then
                        UMAMensual = UMA * FactorDiarioPromedio
                        SubsidioMensual = UMAMensual * (FactorSubsidio / 100)
                        SubsidioDiario = SubsidioMensual / FactorDiarioPromedio

                        If (Impuesto > 0 And (Impuesto < (SubsidioDiario * NumeroDeDiasPagados))) Then
                            SubsidioAplicado = Impuesto
                        Else
                            SubsidioAplicado = (SubsidioDiario * NumeroDeDiasPagados)
                        End If

                        If Impuesto > SubsidioAplicado Then
                            Impuesto = Impuesto - SubsidioAplicado
                        ElseIf Impuesto < SubsidioAplicado Then
                            SubsidioAplicado = SubsidioAplicado - Impuesto
                            Impuesto = 0
                        End If

                    End If

                    If Impuesto > 0 Then
                        Dim cNomina = New Nomina()
                        cNomina.Cliente = empresaId.Value
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 2 'Catorcenal
                        cNomina.Periodo = Periodo
                        cNomina.NoEmpleado = empleadoId.Value
                        cNomina.CvoConcepto = 52
                        cNomina.IdContrato = contratoId.Value
                        cNomina.TipoConcepto = "D"
                        cNomina.Unidad = 1
                        cNomina.Importe = Impuesto
                        cNomina.ImporteGravado = 0
                        cNomina.ImporteExento = Impuesto
                        cNomina.Generado = ""
                        cNomina.Timbrado = ""
                        cNomina.Enviado = ""
                        cNomina.Situacion = "A"
                        cNomina.EsEspecial = False
                        cNomina.FechaIni = cPeriodo.FechaInicialDate
                        cNomina.FechaFin = cPeriodo.FechaFinalDate
                        cNomina.FechaPago = cPeriodo.FechaPago
                        cNomina.DiasPagados = cPeriodo.Dias
                        cNomina.IdNomina = nominaId.Value
                        cNomina.GuadarNominaPeriodo()
                    End If

                    If SubsidioAplicado > 0 Then
                        Dim cNomina = New Nomina()
                        cNomina.Cliente = empresaId.Value
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 2 'Catorcenal
                        cNomina.Periodo = Periodo
                        cNomina.NoEmpleado = empleadoId.Value
                        cNomina.CvoConcepto = 54
                        cNomina.IdContrato = contratoId.Value
                        cNomina.TipoConcepto = "P"
                        cNomina.Unidad = 1
                        cNomina.Importe = SubsidioAplicado
                        cNomina.ImporteGravado = 0
                        cNomina.ImporteExento = SubsidioAplicado
                        cNomina.Generado = ""
                        cNomina.Timbrado = ""
                        cNomina.Enviado = ""
                        cNomina.Situacion = "A"
                        cNomina.EsEspecial = False
                        cNomina.FechaIni = cPeriodo.FechaInicialDate
                        cNomina.FechaFin = cPeriodo.FechaFinalDate
                        cNomina.FechaPago = cPeriodo.FechaPago
                        cNomina.DiasPagados = cPeriodo.Dias
                        cNomina.IdNomina = nominaId.Value
                        cNomina.GuadarNominaPeriodo()
                    End If
                Else
                    Call GuardarRegistro(CuotaDiaria, 1)
                End If
            End If

            'Call GuardarRegistro(CuotaDiaria, 2)

            '*******************************************************************************************************
            '*******************************************************************************************************
            Call ChecarYGrabarPercepcionesExentasYGravadas(empleadoId.Value, 0)
            '*******************************************************************************************************
            '*******************************************************************************************************

            Call SolicitarGeneracionXml(empleadoId.Value, "")
            Call CargarPercepcionesYDeducciones()
            Call CargarPercepciones()
            Call CargarDeducciones()
            Call CargarOtrosPagos()
            Call ChecarPercepcionesExentasYGravadas()
            Me.txtGravadoISR.Text = Math.Round(PercepcionesGravadas, 6)
            Me.txtExentoISR.Text = Math.Round(PercepcionesExentas, 6)

            'Call LlenaCmbConcepto(0, "P")
            'Call LlenaCmbTipoHorasExtra(0)

            Me.txtUnidadIncidencia.Text = 0
            Me.txtImporteIncidencia.Text = 0
            Me.txtDiasHorasExtra.Text = 0
            Me.cmbTipoHorasExtra.SelectedValue = 0

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub ChecarYGrabarPercepcionesExentasYGravadas(ByVal NoEmpleado As Integer, ByVal CvoConcepto As Integer)
        Try
            CuotaPeriodo = 0
            PrimaDominical = 0
            PrimaVacacional = 0
            Vacaciones = 0
            Aguinaldo = 0
            RepartoUtilidades = 0
            FondoAhorro = 0
            AyudaFuneral = 0
            PrevisionSocial = 0
            GrupoPercepcionesGravadasTotalmenteSinExentos = 0
            PagoPorHoras = 0
            Comisiones = 0
            Destajo = 0
            FaltasPermisosIncapacidades = 0
            HonorarioAsimilado = 0
            DiasVacaciones = 0
            DiasCuotaPeriodo = 0
            DiasHonorarioAsimilado = 0
            DiasPagoPorHoras = 0
            DiasComision = 0
            DiasDestajo = 0
            DiasFaltasPermisosIncapacidades = 0
            NumeroDeDiasPagados = 0
            SubsidioAplicado = 0
            ImporteExentoPrimaDominical = 0
            ImporteGravadoPrimaDominical = 0
            ImporteExentoAguinaldo = 0
            ImporteGravadoAguinaldo = 0
            ImporteExentoPrimaVacacional = 0
            ImporteGravadoPrimaVacacional = 0
            ImporteExentoRepartoUtilidades = 0
            ImporteGravadoRepartoUtilidades = 0
            ImporteExentoPrevisionSocial = 0
            ImporteGravadoPrevisionSocial = 0

            AyudaCulturalExento = 0
            AyudaCulturalGravado = 0
            AyudaDeportivaExento = 0
            AyudaDeportivaGravado = 0
            AyudaEducacionalExento = 0
            AyudaEducacionalGravado = 0
            AyudaEscolarExento = 0
            AyudaEscolarGravado = 0
            AyudaComidaExento = 0
            AyudaComidaGravado = 0
            ValesDespensaExento = 0
            ValesDespensaGravado = 0
            AyudaUniformeExento = 0
            AyudaUniformeGravado = 0
            BecasExento = 0
            BecasGravado = 0
            SubsidioIncapacidadExento = 0
            SubsidioIncapacidadGravado = 0
            AyudaMatrimonioExento = 0
            AyudaMatrimonioGravado = 0
            AyudaNacimientoExento = 0
            AyudaNacimientoGravado = 0
            ValesComedorExento = 0
            ValesComedorGravado = 0
            AyudaMedicamentoExento = 0
            AyudaMedicamentoGravado = 0
            ImporteExentoFondoAhorro = 0
            ImporteGravadoFondoAhorro = 0
            ImporteExentoAyudaFuneral = 0
            ImporteGravadoAyudaFuneral = 0

            Diferencias = 0
            Gratificacion = 0
            Bonificacion = 0
            Retroactivo = 0
            Telecomunicaciones = 0
            PremioProductividad = 0
            Incentivo = 0
            PremioAsistencia = 0
            PremioPuntualidad = 0
            Premio = 0
            Compensacion = 0
            BonoAntiguedad = 0
            Viaticos = 0
            Pasajes = 0
            AyudaTransporte = 0
            AyudaRenta = 0
            BonoI = 0
            DevolucionDescuentoInfonavit = 0
            DespensaEfectivo = 0
            HaberPorRetiro = 0
            OtrasPercepciones = 0

            DescansoTrabajado = 0
            DescansoTrabajadoGravado = 0
            DescansoTrabajadoExento = 0
            FestivoTrabajado = 0
            FestivoTrabajadoGravado = 0
            FestivoTrabajadoExento = 0
            HorasExtraDobles = 0
            HorasExtraDoblesGravadas = 0
            HorasExtraDoblesExentas = 0
            HorasExtraTriples = 0
            ExentoHorasExtra = 0
            ExentoDescansoTrabajado = 0
            ExentoFestivoTrabajado = 0

            '/* Ajuste 07/04/2020*/
            ImporteGravado = 0

            Dim ImporteIncidencia As Decimal = 0
            Try
                ImporteIncidencia = Convert.ToDecimal(txtImporteIncidencia.Text)
            Catch ex As Exception
                ImporteIncidencia = 0
            End Try

            Try
                CuotaDiaria = Convert.ToDecimal(txtCuotaDiaria.Text)
            Catch ex As Exception
                CuotaDiaria = 0
            End Try

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado

            dt = cNomina.ConsultarConceptosEmpleado()

            If dt.Rows.Count > 0 Then
                If dt.Compute("Sum(Importe)", "CvoConcepto=2") IsNot DBNull.Value Then
                    DiasCuotaPeriodo = dt.Compute("Sum(UNIDAD)", "CvoConcepto=2")
                    CuotaPeriodo = dt.Compute("Sum(Importe)", "CvoConcepto=2")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=3") IsNot DBNull.Value Then
                    DiasComision = 7
                    Comisiones = dt.Compute("Sum(Importe)", "CvoConcepto=3")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=4") IsNot DBNull.Value Then
                    DiasDestajo = 7
                    Destajo = dt.Compute("Sum(Importe)", "CvoConcepto=4")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=5") IsNot DBNull.Value Then
                    DiasHonorarioAsimilado = dt.Compute("Sum(UNIDAD)", "CvoConcepto=5") * 7
                    HonorarioAsimilado = dt.Compute("Sum(Importe)", "CvoConcepto=5")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=8") IsNot DBNull.Value Then
                    DescansoTrabajado = dt.Compute("Sum(Importe)", "CvoConcepto=8")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=9") IsNot DBNull.Value Then
                    FestivoTrabajado = dt.Compute("Sum(Importe)", "CvoConcepto=9")
                End If
                '****************** Modificacion HORAS EXTRA 17/05/2017 '******************
                If dt.Compute("Sum(Importe)", "CvoConcepto=10") IsNot DBNull.Value Then
                    If dt.Compute("Sum(Importe)", "CvoConcepto=10 AND TipoHorasExtra='01'") IsNot DBNull.Value Then
                        HorasExtraDobles = dt.Compute("Sum(Importe)", "CvoConcepto=10 AND TipoHorasExtra='01'")
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=10 AND TipoHorasExtra='02'") IsNot DBNull.Value Then
                        HorasExtraTriples = dt.Compute("Sum(Importe)", "CvoConcepto=10 AND TipoHorasExtra='02'")
                    End If
                End If
                '****************** Modificacion HORAS EXTRA 17/05/2017 '******************
                If dt.Compute("Sum(Importe)", "CvoConcepto=11") IsNot DBNull.Value Then
                    DiasPagoPorHoras = 7
                    PagoPorHoras = dt.Compute("Sum(Importe)", "CvoConcepto=11")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=13") IsNot DBNull.Value Then
                    PrimaDominical = dt.Compute("Sum(Importe)", "CvoConcepto=13")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=14") IsNot DBNull.Value Then
                    Aguinaldo = dt.Compute("Sum(Importe)", "CvoConcepto=14")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=16") IsNot DBNull.Value Then
                    PrimaVacacional = dt.Compute("Sum(Importe)", "CvoConcepto=16")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=20") IsNot DBNull.Value Then
                    Telecomunicaciones = dt.Compute("Sum(Importe)", "CvoConcepto=20")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=15") IsNot DBNull.Value Then
                    DiasVacaciones = dt.Compute("Sum(UNIDAD)", "CvoConcepto=15")
                    Vacaciones = dt.Compute("Sum(Importe)", "CvoConcepto=15")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=50") IsNot DBNull.Value Then
                    RepartoUtilidades = dt.Compute("Sum(Importe)", "CvoConcepto=50")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=42") IsNot DBNull.Value Then
                    FondoAhorro = dt.Compute("Sum(Importe)", "CvoConcepto=42")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=44") IsNot DBNull.Value Then
                    AyudaFuneral = dt.Compute("Sum(Importe)", "CvoConcepto=44")
                End If
                '18,20,28,33,35,36,37,38,40,41,43,45,46,47,48
                If dt.Compute("Sum(Importe)", "CvoConcepto=18 OR CvoConcepto=20 OR CvoConcepto=28 OR CvoConcepto=33 OR CvoConcepto=35 OR CvoConcepto=36 OR CvoConcepto=37 OR CvoConcepto=38 OR CvoConcepto=40 OR CvoConcepto=41 OR CvoConcepto=43 OR CvoConcepto=45 OR CvoConcepto=46 OR CvoConcepto=47 OR CvoConcepto=48") IsNot DBNull.Value Then
                    PrevisionSocial = dt.Compute("Sum(Importe)", "CvoConcepto=18 OR CvoConcepto=20 OR CvoConcepto=28 OR CvoConcepto=33 OR CvoConcepto=35 OR CvoConcepto=36 OR CvoConcepto=37 OR CvoConcepto=38 OR CvoConcepto=40 OR CvoConcepto=41 OR CvoConcepto=43 OR CvoConcepto=45 OR CvoConcepto=46 OR CvoConcepto=47 OR CvoConcepto=48")
                End If
                '12,17,19,21,22,23,24,25,26,27,29,30,31,34,39,49,51
                '167,168,169,170,171
                If dt.Compute("Sum(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=19 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=34 OR CvoConcepto=39 OR CvoConcepto=49 OR CvoConcepto=51 OR CvoConcepto=167 OR CvoConcepto=168 OR CvoConcepto=169 OR CvoConcepto=170 OR CvoConcepto=171") IsNot DBNull.Value Then
                    GrupoPercepcionesGravadasTotalmenteSinExentos = dt.Compute("Sum(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=19 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=34 OR CvoConcepto=39 OR CvoConcepto=49 OR CvoConcepto=51 OR CvoConcepto=167 OR CvoConcepto=168 OR CvoConcepto=169 OR CvoConcepto=170 OR CvoConcepto=171")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=57 Or CvoConcepto=58 Or CvoConcepto=59 Or CvoConcepto=161 Or CvoConcepto=162") IsNot DBNull.Value Then
                    DiasFaltasPermisosIncapacidades = dt.Compute("Sum(UNIDAD)", "CvoConcepto=57 Or CvoConcepto=58 Or CvoConcepto=59 Or CvoConcepto=161 Or CvoConcepto=162")
                    FaltasPermisosIncapacidades = dt.Compute("Sum(Importe)", "CvoConcepto=57 Or CvoConcepto=58 Or CvoConcepto=59 Or CvoConcepto=161 Or CvoConcepto=162")
                End If

                If dt.Compute("Sum(Importe)", "CvoConcepto=54") IsNot DBNull.Value Then
                    SubsidioAplicado = dt.Compute("Sum(Importe)", "CvoConcepto=54")
                End If
                'If dt.Compute("Sum(Importe)", "CvoConcepto=55") IsNot DBNull.Value Then
                '    SubsidioEfectivo = dt.Compute("Sum(Importe)", "CvoConcepto=55")
                'End If
            End If

            If DiasCuotaPeriodo > 0 Then
                DiasPagoPorHoras = 0
                DiasComision = 0
                DiasDestajo = 0
            ElseIf DiasPagoPorHoras > 0 Then
                DiasCuotaPeriodo = 0
                DiasComision = 0
                DiasDestajo = 0
            ElseIf DiasComision > 0 Then
                DiasPagoPorHoras = 0
                DiasCuotaPeriodo = 0
                DiasDestajo = 0
            ElseIf DiasDestajo > 0 Then
                DiasComision = 0
                DiasPagoPorHoras = 0
                DiasCuotaPeriodo = 0
            End If

            NumeroDeDiasPagados = DiasCuotaPeriodo + DiasVacaciones + DiasComision + DiasPagoPorHoras + DiasDestajo + DiasHonorarioAsimilado - DiasFaltasPermisosIncapacidades

            If DescansoTrabajado > 0 Then
                '****************** Modificacion DESCANSO TRABAJADO 17/06/2021 '******************
                ExentoDescansoTrabajado = UMA * 5
                If CuotaDiaria > SalarioMinimoDiarioGeneral Then
                    If (DescansoTrabajado / 2) < ExentoDescansoTrabajado Then
                        ImporteExento = DescansoTrabajado / 2
                        ImporteGravado = DescansoTrabajado / 2
                        DescansoTrabajadoExento = DescansoTrabajado / 2
                        DescansoTrabajadoGravado = DescansoTrabajado / 2
                    ElseIf (DescansoTrabajado / 2) >= ExentoDescansoTrabajado Then
                        ImporteExento = ExentoDescansoTrabajado
                        ImporteGravado = DescansoTrabajado - ExentoDescansoTrabajado
                        DescansoTrabajadoExento = ExentoDescansoTrabajado
                        DescansoTrabajadoGravado = DescansoTrabajado - ExentoDescansoTrabajado
                    End If
                Else
                    ImporteExento = DescansoTrabajado
                    ImporteGravado = 0
                    DescansoTrabajadoExento = DescansoTrabajado
                    DescansoTrabajadoGravado = 0
                End If
                'If (DescansoTrabajado / 2) < (SalarioMinimoDiarioGeneral * 5) Then
                '    ImporteExento = DescansoTrabajado / 2
                '    ImporteGravado = DescansoTrabajado / 2
                '    DescansoTrabajadoExento = DescansoTrabajado / 2
                '    DescansoTrabajadoGravado = DescansoTrabajado / 2
                'Else
                '    ImporteExento = SalarioMinimoDiarioGeneral * 5
                '    ImporteGravado = DescansoTrabajado - (SalarioMinimoDiarioGeneral * 5)
                '    DescansoTrabajadoExento = SalarioMinimoDiarioGeneral * 5
                '    DescansoTrabajadoGravado = DescansoTrabajado - (SalarioMinimoDiarioGeneral * 5)
                'End If
                GuardarExentoYGravado(8, DescansoTrabajadoGravado, DescansoTrabajadoExento, NoEmpleado, 0)
            End If

            If FestivoTrabajado > 0 Then
                '****************** Modificacion DESCANSO TRABAJADO 17/06/2021 '******************
                ExentoFestivoTrabajado = UMA * 5
                If CuotaDiaria > SalarioMinimoDiarioGeneral Then
                    If (FestivoTrabajado / 2) < ExentoFestivoTrabajado Then
                        ImporteExento = FestivoTrabajado / 2
                        ImporteGravado = FestivoTrabajado / 2
                        FestivoTrabajadoExento = FestivoTrabajado / 2
                        FestivoTrabajadoGravado = FestivoTrabajado / 2
                    ElseIf (FestivoTrabajado / 2) >= ExentoFestivoTrabajado Then
                        ImporteExento = ExentoFestivoTrabajado
                        ImporteGravado = FestivoTrabajado - ExentoFestivoTrabajado
                        FestivoTrabajadoExento = ExentoFestivoTrabajado
                        FestivoTrabajadoGravado = FestivoTrabajado - ExentoFestivoTrabajado
                    End If
                Else
                    ImporteExento = FestivoTrabajado
                    ImporteGravado = 0
                    FestivoTrabajadoExento = FestivoTrabajado
                    FestivoTrabajadoGravado = 0
                End If
                'If (FestivoTrabajado / 2) < (SalarioMinimoDiarioGeneral * 5) Then
                '    ImporteExento = FestivoTrabajado / 2
                '    ImporteGravado = FestivoTrabajado / 2
                '    FestivoTrabajadoExento = FestivoTrabajado / 2
                '    FestivoTrabajadoGravado = FestivoTrabajado / 2
                'Else
                '    ImporteExento = SalarioMinimoDiarioGeneral * 5
                '    ImporteGravado = FestivoTrabajado - (SalarioMinimoDiarioGeneral * 5)
                '    FestivoTrabajadoExento = SalarioMinimoDiarioGeneral * 5
                '    FestivoTrabajadoGravado = FestivoTrabajado - (SalarioMinimoDiarioGeneral * 5)
                'End If
                GuardarExentoYGravado(9, FestivoTrabajadoGravado, FestivoTrabajadoExento, NoEmpleado, 0)
            End If

            If HorasExtraDobles > 0 Then
                '****************** Modificacion HORAS EXTRA 17/06/2021 '******************
                ExentoHorasExtra = UMA * 5
                If CuotaDiaria > SalarioMinimoDiarioGeneral Then
                    If (HorasExtraDobles / 2) < ExentoHorasExtra Then
                        ImporteExento = HorasExtraDobles / 2
                        ImporteGravado = HorasExtraDobles / 2
                        HorasExtraDoblesExentas = HorasExtraDobles / 2
                        HorasExtraDoblesGravadas = HorasExtraDobles / 2
                    ElseIf (HorasExtraDobles / 2) >= ExentoHorasExtra Then
                        ImporteExento = ExentoHorasExtra
                        ImporteGravado = HorasExtraDobles - ExentoHorasExtra
                        HorasExtraDoblesExentas = ExentoHorasExtra
                        HorasExtraDoblesGravadas = HorasExtraDobles - ExentoHorasExtra
                    End If
                Else
                    ImporteExento = HorasExtraDobles
                    ImporteGravado = 0
                    HorasExtraDoblesExentas = HorasExtraDobles
                    HorasExtraDoblesGravadas = 0
                End If
                'If (HorasExtraDobles / 2) < (SalarioMinimoDiarioGeneral * 5) Then
                '    ImporteExento = HorasExtraDobles / 2
                '    ImporteGravado = HorasExtraDobles / 2
                '    HorasExtraDoblesExentas = HorasExtraDobles / 2
                '    HorasExtraDoblesGravadas = HorasExtraDobles / 2
                'Else
                '    ImporteExento = SalarioMinimoDiarioGeneral * 5
                '    ImporteGravado = HorasExtraDobles - (SalarioMinimoDiarioGeneral * 5)
                '    HorasExtraDoblesExentas = SalarioMinimoDiarioGeneral * 5
                '    HorasExtraDoblesGravadas = HorasExtraDobles - (SalarioMinimoDiarioGeneral * 5)
                'End If
                GuardarExentoYGravado(10, HorasExtraDoblesGravadas, HorasExtraDoblesExentas, NoEmpleado, "01")
            End If

            If HorasExtraTriples > 0 Then
                GuardarExentoYGravado(10, HorasExtraTriples, 0, NoEmpleado, "02")
            End If

            If CuotaPeriodo > 0 Then
                GuardarExentoYGravado(2, CuotaPeriodo - FaltasPermisosIncapacidades, FaltasPermisosIncapacidades, NoEmpleado)
            End If

            If PrimaDominical > 0 Then
                Dim Dias As Integer
                Dias = dt.Compute("Sum(UNIDAD)", "CvoConcepto=13")
                If PrimaDominical < (SalarioMinimoDiarioGeneral * Dias) Then
                    ImporteExento = ImporteExento + PrimaDominical
                    ImporteExentoPrimaDominical = PrimaDominical
                    ImporteGravadoPrimaDominical = 0
                Else
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * Dias)
                    ImporteGravado = ImporteGravado + (PrimaDominical - (SalarioMinimoDiarioGeneral * Dias))
                    ImporteExentoPrimaDominical = SalarioMinimoDiarioGeneral * Dias
                    ImporteGravadoPrimaDominical = PrimaDominical - (SalarioMinimoDiarioGeneral * Dias)
                End If
                GuardarExentoYGravado(13, ImporteGravadoPrimaDominical, ImporteExentoPrimaDominical, NoEmpleado, 0)
            End If

            If Aguinaldo > 0 Then
                If Aguinaldo > 0 And Aguinaldo < (SalarioMinimoDiarioGeneral * 30) Then
                    ImporteExento = ImporteExento + Aguinaldo
                    ImporteExentoAguinaldo = Aguinaldo
                    ImporteGravadoAguinaldo = 0
                ElseIf Aguinaldo > 0 And Aguinaldo > (SalarioMinimoDiarioGeneral * 30) Then
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 30)
                    ImporteGravado = ImporteGravado + (Aguinaldo - (SalarioMinimoDiarioGeneral * 30))
                    ImporteExentoAguinaldo = SalarioMinimoDiarioGeneral * 30
                    ImporteGravadoAguinaldo = Aguinaldo - (SalarioMinimoDiarioGeneral * 30)
                End If
                GuardarExentoYGravado(14, ImporteGravadoAguinaldo, ImporteExentoAguinaldo, NoEmpleado)
            End If

            If PrimaVacacional > 0 Then
                If PrimaVacacional > 0 And PrimaVacacional < (UMA * 15) Then
                    ImporteExento = ImporteExento + PrimaVacacional
                    ImporteExentoPrimaVacacional = PrimaVacacional
                    ImporteGravadoPrimaVacacional = 0
                ElseIf PrimaVacacional > 0 And PrimaVacacional > (UMA * 15) Then
                    ImporteExento = ImporteExento + (UMA * 15)
                    ImporteGravado = ImporteGravado + (PrimaVacacional - (UMA * 15))
                    ImporteExentoPrimaVacacional = UMA * 15
                    ImporteGravadoPrimaVacacional = PrimaVacacional - (UMA * 15)
                End If
                GuardarExentoYGravado(16, ImporteGravadoPrimaVacacional, ImporteExentoPrimaVacacional, NoEmpleado, 0)
            End If

            If RepartoUtilidades > 0 Then
                If RepartoUtilidades > 0 And RepartoUtilidades < (SalarioMinimoDiarioGeneral * 15) Then
                    ImporteExento = ImporteExento + RepartoUtilidades
                    ImporteExentoRepartoUtilidades = RepartoUtilidades
                    ImporteGravadoRepartoUtilidades = 0
                ElseIf RepartoUtilidades > 0 And RepartoUtilidades > (SalarioMinimoDiarioGeneral * 15) Then
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 15)
                    ImporteGravado = ImporteGravado + (RepartoUtilidades - (SalarioMinimoDiarioGeneral * 15))
                    ImporteExentoRepartoUtilidades = SalarioMinimoDiarioGeneral * 15
                    ImporteGravadoRepartoUtilidades = RepartoUtilidades - (SalarioMinimoDiarioGeneral * 15)
                End If
                GuardarExentoYGravado(50, ImporteGravadoRepartoUtilidades, ImporteExentoRepartoUtilidades, NoEmpleado)
            End If

            'El fondo de ahorro y ayuda para funeral son exentos total siempre asi que se van directo a ImporteExento sin ningun chequeo mas
            'ImporteExento = ImporteExento + FondoAhorro + AyudaFuneral
            If FondoAhorro > 0 Then
                ImporteExento = ImporteExento + FondoAhorro
                ImporteExentoFondoAhorro = FondoAhorro
                ImporteGravadoFondoAhorro = 0
                GuardarExentoYGravado(42, ImporteGravadoFondoAhorro, ImporteExentoFondoAhorro, NoEmpleado)
            End If

            If AyudaFuneral > 0 Then
                ImporteExento = ImporteExento + AyudaFuneral
                ImporteExentoAyudaFuneral = AyudaFuneral
                ImporteGravadoAyudaFuneral = 0
                GuardarExentoYGravado(44, ImporteGravadoAyudaFuneral, ImporteExentoAyudaFuneral, NoEmpleado)
            End If

            'El tiempo extraordinario2 es gravado al 100%, no tiene nada exento igual que las vacaciones y la Cuota del periodo
            ImporteGravado = ImporteGravado + TiempoExtraordinarioFueraDelMargenLegal + Vacaciones + CuotaPeriodo
            If GrupoPercepcionesGravadasTotalmenteSinExentos > 0 Then
                ImporteGravado = ImporteGravado + GrupoPercepcionesGravadasTotalmenteSinExentos
            End If
            If HonorarioAsimilado > 0 Then
                ImporteGravado = ImporteGravado + HonorarioAsimilado 'ESTO ESTA MAL? CHECAR
                GuardarExentoYGravado(5, HonorarioAsimilado, 0, NoEmpleado)
            End If
            If PagoPorHoras > 0 Then
                ImporteGravado = ImporteGravado + PagoPorHoras
                GuardarExentoYGravado(11, PagoPorHoras, 0, NoEmpleado)
            End If
            If Comisiones > 0 Then
                ImporteGravado = ImporteGravado + Comisiones 'ESTO ESTA MAL? CHECAR
                GuardarExentoYGravado(3, Comisiones, 0, NoEmpleado)
            End If
            If Destajo > 0 Then
                ImporteGravado = ImporteGravado + Destajo
                GuardarExentoYGravado(4, Destajo, 0, NoEmpleado)
            End If
            If PrevisionSocial > 0 Then
                If PrevisionSocial < (SalarioMinimoDiarioGeneral * 7) Then
                    ImporteExento = ImporteExento + PrevisionSocial
                    ImporteExentoPrevisionSocial = PrevisionSocial
                    ImporteGravadoPrevisionSocial = 0
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial < ((SalarioMinimoDiarioGeneral * 7) * 7) Then
                    ImporteExento = ImporteExento + PrevisionSocial
                    ImporteExentoPrevisionSocial = PrevisionSocial
                    ImporteGravadoPrevisionSocial = 0
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial > ((SalarioMinimoDiarioGeneral * 7) * 7) And ImporteGravado + ImporteExento > ((SalarioMinimoDiarioGeneral * 7) * 7) Then
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 7)
                    ImporteGravado = ImporteGravado + (PrevisionSocial - (SalarioMinimoDiarioGeneral * 7))
                    ImporteExentoPrevisionSocial = SalarioMinimoDiarioGeneral * 7
                    ImporteGravadoPrevisionSocial = PrevisionSocial - (SalarioMinimoDiarioGeneral * 7)
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial > ((SalarioMinimoDiarioGeneral * 7) * 7) And ImporteGravado + ImporteExento + SalarioMinimoDiarioGeneral < ((SalarioMinimoDiarioGeneral * 7) * 7) Then
                    ImporteExento = ImporteExento + (((SalarioMinimoDiarioGeneral * 7) * 7) - (ImporteGravado + ImporteExento))
                    ImporteGravado = ImporteGravado + (PrevisionSocial - (((SalarioMinimoDiarioGeneral * 7) * 7) - (ImporteGravado + ImporteExento)))
                    ImporteExentoPrevisionSocial = ((SalarioMinimoDiarioGeneral * 7) * 7) - (ImporteGravado + ImporteExento)
                    ImporteGravadoPrevisionSocial = PrevisionSocial - (((SalarioMinimoDiarioGeneral * 7) * 7) - (ImporteGravado + ImporteExento))
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial > ((SalarioMinimoDiarioGeneral * 7) * 7) And (((SalarioMinimoDiarioGeneral * 7) * 7) - (ImporteGravado + ImporteExento) < SalarioMinimoDiarioGeneral * 7) Then
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 7)
                    ImporteExentoPrevisionSocial = SalarioMinimoDiarioGeneral * 7
                    ImporteGravadoPrevisionSocial = 0
                End If
            End If

            ' Distribuyendo las percepciones del GrupoPercepcionesGravadasTotalmenteSinExentos
            '12,15,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,34,39,49,51
            '167,168,169,170,171
            If GrupoPercepcionesGravadasTotalmenteSinExentos > 0 Then
                If dt.Compute("Sum(Importe)", "CvoConcepto=12") IsNot DBNull.Value Then
                    Vacaciones = dt.Compute("Sum(Importe)", "CvoConcepto=12")
                    GuardarExentoYGravado(12, Diferencias, 0, NoEmpleado, 0)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=15") IsNot DBNull.Value Then
                    Diferencias = dt.Compute("Sum(Importe)", "CvoConcepto=15")
                    GuardarExentoYGravado(15, Vacaciones, 0, NoEmpleado, 0)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=17") IsNot DBNull.Value Then
                    Gratificacion = dt.Compute("Sum(Importe)", "CvoConcepto=17")
                    GuardarExentoYGravado(17, Gratificacion, 0, NoEmpleado, 0)
                End If
                'If dt.Compute("Sum(Importe)", "CvoConcepto=18") IsNot DBNull.Value Then
                '    Bonificacion = dt.Compute("Sum(Importe)", "CvoConcepto=18")
                '    GuardarExentoYGravado(18, Bonificacion, 0, NoEmpleado, 0)
                'End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=19") IsNot DBNull.Value Then
                    Retroactivo = dt.Compute("Sum(Importe)", "CvoConcepto=19")
                    GuardarExentoYGravado(19, Retroactivo, 0, NoEmpleado, 0)
                End If
                'If dt.Compute("Sum(Importe)", "CvoConcepto=20") IsNot DBNull.Value Then
                '    BonoProduccion = dt.Compute("Sum(Importe)", "CvoConcepto=20")
                '    GuardarExentoYGravado(20, BonoProduccion, 0, NoEmpleado, 0)
                'End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=21") IsNot DBNull.Value Then
                    PremioProductividad = dt.Compute("Sum(Importe)", "CvoConcepto=21")
                    GuardarExentoYGravado(21, PremioProductividad, 0, NoEmpleado, 0)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=22") IsNot DBNull.Value Then
                    Incentivo = dt.Compute("Sum(Importe)", "CvoConcepto=22")
                    GuardarExentoYGravado(22, Incentivo, 0, NoEmpleado, 0)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=23") IsNot DBNull.Value Then
                    PremioAsistencia = dt.Compute("Sum(Importe)", "CvoConcepto=23")
                    GuardarExentoYGravado(23, PremioAsistencia, 0, NoEmpleado, 0)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=24") IsNot DBNull.Value Then
                    PremioPuntualidad = dt.Compute("Sum(Importe)", "CvoConcepto=24")
                    GuardarExentoYGravado(24, PremioPuntualidad, 0, NoEmpleado, 0)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=25") IsNot DBNull.Value Then
                    Premio = dt.Compute("Sum(Importe)", "CvoConcepto=25")
                    GuardarExentoYGravado(25, Premio, 0, NoEmpleado, 0)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=26") IsNot DBNull.Value Then
                    Compensacion = dt.Compute("Sum(Importe)", "CvoConcepto=26")
                    GuardarExentoYGravado(26, Compensacion, 0, NoEmpleado, 0)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=27") IsNot DBNull.Value Then
                    BonoAntiguedad = dt.Compute("Sum(Importe)", "CvoConcepto=27")
                    GuardarExentoYGravado(27, BonoAntiguedad, 0, NoEmpleado, 0)
                End If
                'If dt.Compute("Sum(Importe)", "CvoConcepto=28") IsNot DBNull.Value Then
                '    Viaticos = dt.Compute("Sum(Importe)", "CvoConcepto=28")
                '    GuardarExentoYGravado(28, Viaticos, 0, NoEmpleado, 0)
                'End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=29") IsNot DBNull.Value Then
                    Pasajes = dt.Compute("Sum(Importe)", "CvoConcepto=29")
                    GuardarExentoYGravado(29, Pasajes, 0, NoEmpleado, 0)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=30") IsNot DBNull.Value Then
                    AyudaTransporte = dt.Compute("Sum(Importe)", "CvoConcepto=30")
                    GuardarExentoYGravado(30, AyudaTransporte, 0, NoEmpleado, 0)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=31") IsNot DBNull.Value Then
                    AyudaRenta = dt.Compute("Sum(Importe)", "CvoConcepto=31")
                    GuardarExentoYGravado(31, AyudaRenta, 0, NoEmpleado, 0)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=32") IsNot DBNull.Value Then
                    DevolucionDescuentoInfonavit = dt.Compute("Sum(Importe)", "CvoConcepto=32")
                    GuardarExentoYGravado(32, 0, DevolucionDescuentoInfonavit, NoEmpleado, 0)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=34") IsNot DBNull.Value Then
                    BonoI = dt.Compute("Sum(Importe)", "CvoConcepto=34")
                    GuardarExentoYGravado(34, BonoI, 0, NoEmpleado, 0)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=39") IsNot DBNull.Value Then
                    DespensaEfectivo = dt.Compute("Sum(Importe)", "CvoConcepto=39")
                    GuardarExentoYGravado(39, DespensaEfectivo, 0, NoEmpleado, 0)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=49") IsNot DBNull.Value Then
                    HaberPorRetiro = dt.Compute("Sum(Importe)", "CvoConcepto=49")
                    GuardarExentoYGravado(49, HaberPorRetiro, 0, NoEmpleado, 0)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=51") IsNot DBNull.Value Then
                    OtrasPercepciones = dt.Compute("Sum(Importe)", "CvoConcepto=51")
                    GuardarExentoYGravado(51, OtrasPercepciones, 0, NoEmpleado, 0)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=167") IsNot DBNull.Value Then
                    OtrasPercepciones = dt.Compute("Sum(Importe)", "CvoConcepto=167")
                    GuardarExentoYGravado(167, OtrasPercepciones, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=168") IsNot DBNull.Value Then
                    OtrasPercepciones = dt.Compute("Sum(Importe)", "CvoConcepto=168")
                    GuardarExentoYGravado(168, OtrasPercepciones, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=169") IsNot DBNull.Value Then
                    OtrasPercepciones = dt.Compute("Sum(Importe)", "CvoConcepto=169")
                    GuardarExentoYGravado(169, OtrasPercepciones, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=170") IsNot DBNull.Value Then
                    OtrasPercepciones = dt.Compute("Sum(Importe)", "CvoConcepto=170")
                    GuardarExentoYGravado(170, OtrasPercepciones, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=171") IsNot DBNull.Value Then
                    OtrasPercepciones = dt.Compute("Sum(Importe)", "CvoConcepto=171")
                    GuardarExentoYGravado(171, OtrasPercepciones, 0, NoEmpleado)
                End If
            End If

            ' Distribuyendo lo exento del grupo de percepciones PrevisionSocial en cada uno de sus elementos(33,35,36,37,38,40,41,43,45,46,47,48)
            If PrevisionSocial > 0 Then
                'If ImporteGravado + ImporteExento + PrevisionSocial < (SalarioMinimoDiarioGeneral * 7) Then
                If PrevisionSocial = ImporteExentoPrevisionSocial Then
                    If dt.Compute("Sum(Importe)", "CvoConcepto=18") IsNot DBNull.Value Then
                        PrestamoExento = dt.Compute("Sum(Importe)", "CvoConcepto=18")
                        PrestamoGravado = 0
                        GuardarExentoYGravado(18, PrestamoGravado, PrestamoExento, NoEmpleado)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=20") IsNot DBNull.Value Then
                        TelecomunicacionExento = dt.Compute("Sum(Importe)", "CvoConcepto=20")
                        TelecomunicacionGravado = 0
                        GuardarExentoYGravado(20, TelecomunicacionGravado, TelecomunicacionExento, NoEmpleado)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=28") IsNot DBNull.Value Then
                        OtrasPercepcionesExento = dt.Compute("Sum(Importe)", "CvoConcepto=28")
                        OtrasPercepcionesGravado = 0
                        GuardarExentoYGravado(28, OtrasPercepcionesGravado, OtrasPercepcionesExento, NoEmpleado)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=33") IsNot DBNull.Value Then
                        AyudaCulturalExento = dt.Compute("Sum(Importe)", "CvoConcepto=33")
                        AyudaCulturalGravado = 0
                        GuardarExentoYGravado(33, AyudaCulturalGravado, AyudaCulturalExento, NoEmpleado, 0)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=35") IsNot DBNull.Value Then
                        AyudaEducacionalExento = dt.Compute("Sum(Importe)", "CvoConcepto=35")
                        AyudaEducacionalGravado = 0
                        GuardarExentoYGravado(35, AyudaEducacionalGravado, AyudaEducacionalExento, NoEmpleado, 0)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=36") IsNot DBNull.Value Then
                        AyudaEscolarExento = dt.Compute("Sum(Importe)", "CvoConcepto=36")
                        AyudaEscolarGravado = 0
                        GuardarExentoYGravado(36, AyudaEscolarGravado, AyudaEscolarExento, NoEmpleado, 0)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=37") IsNot DBNull.Value Then
                        AyudaComidaExento = dt.Compute("Sum(Importe)", "CvoConcepto=37")
                        AyudaComidaGravado = 0
                        GuardarExentoYGravado(37, AyudaComidaGravado, AyudaComidaExento, NoEmpleado, 0)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=38") IsNot DBNull.Value Then
                        ValesDespensaExento = dt.Compute("Sum(Importe)", "CvoConcepto=38")
                        ValesDespensaGravado = 0
                        GuardarExentoYGravado(38, ValesDespensaGravado, ValesDespensaExento, NoEmpleado, 0)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=40") IsNot DBNull.Value Then
                        AyudaUniformeExento = dt.Compute("Sum(Importe)", "CvoConcepto=40")
                        AyudaUniformeGravado = 0
                        GuardarExentoYGravado(40, AyudaUniformeGravado, AyudaUniformeExento, NoEmpleado, 0)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=41") IsNot DBNull.Value Then
                        BecasExento = dt.Compute("Sum(Importe)", "CvoConcepto=41")
                        BecasGravado = 0
                        GuardarExentoYGravado(41, BecasGravado, BecasExento, NoEmpleado, 0)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=43") IsNot DBNull.Value Then
                        SubsidioIncapacidadExento = dt.Compute("Sum(Importe)", "CvoConcepto=43")
                        SubsidioIncapacidadGravado = 0
                        GuardarExentoYGravado(43, SubsidioIncapacidadGravado, SubsidioIncapacidadExento, NoEmpleado, 0)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=45") IsNot DBNull.Value Then
                        AyudaMatrimonioExento = dt.Compute("Sum(Importe)", "CvoConcepto=45")
                        AyudaMatrimonioGravado = 0
                        GuardarExentoYGravado(45, AyudaMatrimonioGravado, AyudaMatrimonioExento, NoEmpleado, 0)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=46") IsNot DBNull.Value Then
                        AyudaNacimientoExento = dt.Compute("Sum(Importe)", "CvoConcepto=46")
                        AyudaNacimientoGravado = 0
                        GuardarExentoYGravado(46, AyudaNacimientoGravado, AyudaNacimientoExento, NoEmpleado, 0)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=47") IsNot DBNull.Value Then
                        ValesComedorExento = dt.Compute("Sum(Importe)", "CvoConcepto=47")
                        ValesComedorGravado = 0
                        GuardarExentoYGravado(47, ValesComedorGravado, ValesComedorExento, NoEmpleado, 0)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=48") IsNot DBNull.Value Then
                        AyudaMedicamentoExento = dt.Compute("Sum(Importe)", "CvoConcepto=48")
                        AyudaMedicamentoGravado = 0
                        GuardarExentoYGravado(48, AyudaMedicamentoGravado, AyudaMatrimonioExento, NoEmpleado, 0)
                    End If
                ElseIf ImporteGravadoPrevisionSocial > 0 Then
                    If dt.Compute("Sum(Importe)", "CvoConcepto=20") IsNot DBNull.Value Then
                        If ImporteExentoPrevisionSocial > dt.Compute("Sum(Importe)", "CvoConcepto=20") Then
                            TelecomunicacionExento = dt.Compute("Sum(Importe)", "CvoConcepto=20")
                            TelecomunicacionGravado = 0
                        Else
                            TelecomunicacionExento = ImporteExentoPrevisionSocial
                            TelecomunicacionGravado = dt.Compute("Sum(Importe)", "CvoConcepto=20") - ImporteExentoPrevisionSocial
                        End If
                        GuardarExentoYGravado(20, TelecomunicacionGravado, TelecomunicacionExento, NoEmpleado)
                    End If
                    ImporteExentoPrevisionSocial = ImporteExentoPrevisionSocial - TelecomunicacionExento
                    If ImporteExentoPrevisionSocial <= 0 Then ImporteExentoPrevisionSocial = 0

                    If dt.Compute("Sum(Importe)", "CvoConcepto=33") IsNot DBNull.Value Then
                        If ImporteExentoPrevisionSocial > dt.Compute("Sum(Importe)", "CvoConcepto=33") Then
                            AyudaCulturalExento = dt.Compute("Sum(Importe)", "CvoConcepto=33")
                            AyudaCulturalGravado = 0
                        Else
                            AyudaCulturalExento = ImporteExentoPrevisionSocial
                            AyudaCulturalGravado = dt.Compute("Sum(Importe)", "CvoConcepto=33") - ImporteExentoPrevisionSocial
                        End If
                        GuardarExentoYGravado(33, AyudaCulturalGravado, AyudaCulturalExento, NoEmpleado, 0)
                    End If
                    ImporteExentoPrevisionSocial = ImporteExentoPrevisionSocial - AyudaCulturalExento
                    If ImporteExentoPrevisionSocial <= 0 Then ImporteExentoPrevisionSocial = 0

                    If dt.Compute("Sum(Importe)", "CvoConcepto=35") IsNot DBNull.Value Then
                        If ImporteExentoPrevisionSocial > dt.Compute("Sum(Importe)", "CvoConcepto=35") Then
                            AyudaEducacionalExento = dt.Compute("Sum(Importe)", "CvoConcepto=35")
                            AyudaEducacionalGravado = 0
                        Else
                            AyudaEducacionalExento = ImporteExentoPrevisionSocial
                            AyudaEducacionalGravado = dt.Compute("Sum(Importe)", "CvoConcepto=35") - ImporteExentoPrevisionSocial
                        End If
                        GuardarExentoYGravado(35, AyudaEducacionalGravado, AyudaEducacionalExento, NoEmpleado, 0)
                    End If
                    ImporteExentoPrevisionSocial = ImporteExentoPrevisionSocial - AyudaEducacionalExento
                    If ImporteExentoPrevisionSocial <= 0 Then ImporteExentoPrevisionSocial = 0

                    If dt.Compute("Sum(Importe)", "CvoConcepto=36") IsNot DBNull.Value Then
                        If ImporteExentoPrevisionSocial > dt.Compute("Sum(Importe)", "CvoConcepto=36") Then
                            AyudaEscolarExento = dt.Compute("Sum(Importe)", "CvoConcepto=36")
                            AyudaEscolarGravado = 0
                        Else
                            AyudaEscolarExento = ImporteExentoPrevisionSocial
                            AyudaEscolarGravado = dt.Compute("Sum(Importe)", "CvoConcepto=36") - ImporteExentoPrevisionSocial
                        End If
                        GuardarExentoYGravado(36, AyudaEscolarGravado, AyudaEscolarExento, NoEmpleado, 0)
                    End If
                    ImporteExentoPrevisionSocial = ImporteExentoPrevisionSocial - AyudaEscolarExento
                    If ImporteExentoPrevisionSocial <= 0 Then ImporteExentoPrevisionSocial = 0

                    If dt.Compute("Sum(Importe)", "CvoConcepto=37") IsNot DBNull.Value Then
                        If ImporteExentoPrevisionSocial > dt.Compute("Sum(Importe)", "CvoConcepto=37") Then
                            AyudaComidaExento = dt.Compute("Sum(Importe)", "CvoConcepto=37")
                            AyudaComidaGravado = 0
                        Else
                            AyudaComidaExento = ImporteExentoPrevisionSocial
                            AyudaComidaGravado = dt.Compute("Sum(Importe)", "CvoConcepto=37") - ImporteExentoPrevisionSocial
                        End If
                        GuardarExentoYGravado(37, AyudaComidaGravado, AyudaComidaExento, NoEmpleado, 0)
                    End If
                    ImporteExentoPrevisionSocial = ImporteExentoPrevisionSocial - AyudaComidaExento
                    If ImporteExentoPrevisionSocial <= 0 Then ImporteExentoPrevisionSocial = 0

                    If dt.Compute("Sum(Importe)", "CvoConcepto=38") IsNot DBNull.Value Then
                        If ImporteExentoPrevisionSocial > dt.Compute("Sum(Importe)", "CvoConcepto=38") Then
                            ValesDespensaExento = dt.Compute("Sum(Importe)", "CvoConcepto=38")
                            ValesDespensaGravado = 0
                        Else
                            ValesDespensaExento = ImporteExentoPrevisionSocial
                            ValesDespensaGravado = dt.Compute("Sum(Importe)", "CvoConcepto=38") - ImporteExentoPrevisionSocial
                        End If
                        GuardarExentoYGravado(38, ValesDespensaGravado, ValesDespensaExento, NoEmpleado, 0)
                    End If
                    ImporteExentoPrevisionSocial = ImporteExentoPrevisionSocial - ValesDespensaExento
                    If ImporteExentoPrevisionSocial <= 0 Then ImporteExentoPrevisionSocial = 0

                    If dt.Compute("Sum(Importe)", "CvoConcepto=40") IsNot DBNull.Value Then
                        If ImporteExentoPrevisionSocial > dt.Compute("Sum(Importe)", "CvoConcepto=40") Then
                            AyudaUniformeExento = dt.Compute("Sum(Importe)", "CvoConcepto=40")
                            AyudaUniformeGravado = 0
                        Else
                            AyudaUniformeExento = ImporteExentoPrevisionSocial
                            AyudaUniformeGravado = dt.Compute("Sum(Importe)", "CvoConcepto=40") - ImporteExentoPrevisionSocial
                        End If
                        GuardarExentoYGravado(40, AyudaUniformeGravado, AyudaUniformeExento, NoEmpleado, 0)
                    End If
                    ImporteExentoPrevisionSocial = ImporteExentoPrevisionSocial - AyudaUniformeExento
                    If ImporteExentoPrevisionSocial <= 0 Then ImporteExentoPrevisionSocial = 0

                    If dt.Compute("Sum(Importe)", "CvoConcepto=41") IsNot DBNull.Value Then
                        If ImporteExentoPrevisionSocial > dt.Compute("Sum(Importe)", "CvoConcepto=41") Then
                            BecasExento = dt.Compute("Sum(Importe)", "CvoConcepto=41")
                            BecasGravado = 0
                        Else
                            BecasExento = ImporteExentoPrevisionSocial
                            BecasGravado = dt.Compute("Sum(Importe)", "CvoConcepto=41") - ImporteExentoPrevisionSocial
                        End If
                        GuardarExentoYGravado(41, BecasGravado, BecasExento, NoEmpleado, 0)
                    End If
                    ImporteExentoPrevisionSocial = ImporteExentoPrevisionSocial - BecasExento
                    If ImporteExentoPrevisionSocial <= 0 Then ImporteExentoPrevisionSocial = 0

                    If dt.Compute("Sum(Importe)", "CvoConcepto=43") IsNot DBNull.Value Then
                        If ImporteExentoPrevisionSocial > dt.Compute("Sum(Importe)", "CvoConcepto=43") Then
                            SubsidioIncapacidadExento = dt.Compute("Sum(Importe)", "CvoConcepto=43")
                            SubsidioIncapacidadGravado = 0
                        Else
                            SubsidioIncapacidadExento = ImporteExentoPrevisionSocial
                            SubsidioIncapacidadGravado = dt.Compute("Sum(Importe)", "CvoConcepto=43") - ImporteExentoPrevisionSocial
                        End If
                        GuardarExentoYGravado(43, SubsidioIncapacidadGravado, SubsidioIncapacidadExento, NoEmpleado, 0)
                    End If
                    ImporteExentoPrevisionSocial = ImporteExentoPrevisionSocial - SubsidioIncapacidadExento
                    If ImporteExentoPrevisionSocial <= 0 Then ImporteExentoPrevisionSocial = 0

                    If dt.Compute("Sum(Importe)", "CvoConcepto=45") IsNot DBNull.Value Then
                        If ImporteExentoPrevisionSocial > dt.Compute("Sum(Importe)", "CvoConcepto=45") Then
                            AyudaMatrimonioExento = dt.Compute("Sum(Importe)", "CvoConcepto=45")
                            AyudaMatrimonioGravado = 0
                        Else
                            AyudaMatrimonioExento = ImporteExentoPrevisionSocial
                            AyudaMatrimonioGravado = dt.Compute("Sum(Importe)", "CvoConcepto=45") - ImporteExentoPrevisionSocial
                        End If
                        GuardarExentoYGravado(45, AyudaMatrimonioGravado, AyudaMatrimonioExento, NoEmpleado, 0)
                    End If
                    ImporteExentoPrevisionSocial = ImporteExentoPrevisionSocial - AyudaMatrimonioExento
                    If ImporteExentoPrevisionSocial <= 0 Then ImporteExentoPrevisionSocial = 0

                    If dt.Compute("Sum(Importe)", "CvoConcepto=46") IsNot DBNull.Value Then
                        If ImporteExentoPrevisionSocial > dt.Compute("Sum(Importe)", "CvoConcepto=46") Then
                            AyudaNacimientoExento = dt.Compute("Sum(Importe)", "CvoConcepto=46")
                            AyudaNacimientoGravado = 0
                        Else
                            AyudaNacimientoExento = AyudaNacimientoExento
                            AyudaNacimientoGravado = dt.Compute("Sum(Importe)", "CvoConcepto=46") - AyudaNacimientoExento
                        End If
                        GuardarExentoYGravado(46, AyudaNacimientoGravado, AyudaNacimientoExento, NoEmpleado, 0)
                    End If
                    ImporteExentoPrevisionSocial = ImporteExentoPrevisionSocial - AyudaNacimientoExento
                    If ImporteExentoPrevisionSocial <= 0 Then ImporteExentoPrevisionSocial = 0

                    If dt.Compute("Sum(Importe)", "CvoConcepto=47") IsNot DBNull.Value Then
                        If ImporteExentoPrevisionSocial > dt.Compute("Sum(Importe)", "CvoConcepto=47") Then
                            ValesComedorExento = dt.Compute("Sum(Importe)", "CvoConcepto=47")
                            ValesComedorGravado = 0
                        Else
                            ValesComedorExento = ImporteExentoPrevisionSocial
                            ValesComedorGravado = dt.Compute("Sum(Importe)", "CvoConcepto=47") - ImporteExentoPrevisionSocial
                        End If
                        GuardarExentoYGravado(47, ValesComedorGravado, ValesComedorExento, NoEmpleado, 0)
                    End If
                    ImporteExentoPrevisionSocial = ImporteExentoPrevisionSocial - ValesComedorExento
                    If ImporteExentoPrevisionSocial <= 0 Then ImporteExentoPrevisionSocial = 0

                    If dt.Compute("Sum(Importe)", "CvoConcepto=48") IsNot DBNull.Value Then
                        If ImporteExentoPrevisionSocial > dt.Compute("Sum(Importe)", "CvoConcepto=48") Then
                            AyudaMedicamentoExento = dt.Compute("Sum(Importe)", "CvoConcepto=48")
                            AyudaMedicamentoGravado = 0
                        Else
                            AyudaMedicamentoExento = ImporteExentoPrevisionSocial
                            AyudaMedicamentoGravado = dt.Compute("Sum(Importe)", "CvoConcepto=48") - ImporteExentoPrevisionSocial
                        End If
                        GuardarExentoYGravado(48, AyudaMedicamentoGravado, AyudaMatrimonioExento, NoEmpleado, 0)
                    End If
                    ImporteExentoPrevisionSocial = ImporteExentoPrevisionSocial - AyudaMatrimonioExento
                    If ImporteExentoPrevisionSocial <= 0 Then ImporteExentoPrevisionSocial = 0
                End If
            End If
            If CvoConcepto <> 64 Then
                ''''''// Consultar SI tiene desuento de INFONAVIT //'''''''
                Dim Valor As Decimal
                Dim DescuentoInvonavit As Decimal
                Dim datos As New DataTable
                Dim Infonavit As New Entities.Infonavit()
                Infonavit.IdEmpresa = Session("clienteid")
                Infonavit.IdEmpleado = NoEmpleado
                Infonavit.IdPeriodo = periodoId.Value
                datos = Infonavit.ConsultarEmpleadosConDescuentoInfonavit()
                Infonavit = Nothing

                If datos.Rows.Count > 0 Then

                    If datos.Rows(0)("tipo_descuento") = 1 Then 'Cuota fija
                        Valor = datos.Rows(0)("valor_descuento")
                        'DescuentoInvonavit = ((Valor + ImporteSeguroVivienda) / FactorDiarioPromedio) * Dias
                        DescuentoInvonavit = ((Valor * 2) / datos.Rows(0)("dias")) * NumeroDeDiasPagados
                    ElseIf datos.Rows(0)("tipo_descuento") = 2 Then 'Veces el salario mínimo
                        Valor = datos.Rows(0)("valor_descuento")
                        'DescuentoInvonavit = (((Valor * UMA) + ImporteSeguroVivienda) / FactorDiarioPromedio) * Dias
                        DescuentoInvonavit = (((Valor * UMI) * 2) / datos.Rows(0)("dias")) * NumeroDeDiasPagados
                    ElseIf datos.Rows(0)("tipo_descuento") = 3 Then 'Porcentaje
                        Valor = datos.Rows(0)("valor_descuento")
                        DescuentoInvonavit = ((SalarioDiarioIntegradoTrabajador * (Valor / 100)) + (ImporteSeguroVivienda / FactorDiarioPromedio)) * NumeroDeDiasPagados
                    End If

                    'If datos.Rows(0)("tipo_descuento") = 1 Then
                    '    Valor = datos.Rows(0)("valor_descuento")
                    '    DescuentoInvonavit = (Valor / FactorDiarioPromedio) * NumeroDeDiasPagados
                    'ElseIf datos.Rows(0)("tipo_descuento") = 2 Then
                    '    Valor = datos.Rows(0)("valor_descuento")
                    '    DescuentoInvonavit = ((Valor * SalarioMinimoDiarioGeneral) / FactorDiarioPromedio) * NumeroDeDiasPagados
                    'ElseIf datos.Rows(0)("tipo_descuento") = 3 Then
                    '    Valor = datos.Rows(0)("valor_descuento")
                    '    DescuentoInvonavit = ((SalarioDiarioIntegradoTrabajador * (Valor / 100)) * NumeroDeDiasPagados)
                    'End If

                    Call QuitarConcepto(64, "")
                    Call ActualizarInfonavit(NoEmpleado, DescuentoInvonavit, 64)
                End If
                ''''''
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub ActualizarInfonavit(ByVal NoEmpleado As Integer, ByVal ImporteIncidencia As Decimal, ByVal CvoConcepto As Integer)

        Call CargarVariablesGenerales()

        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = Periodo
        cNomina.NoEmpleado = NoEmpleado
        cNomina.CvoConcepto = CvoConcepto
        cNomina.IdContrato = contratoId.Value
        cNomina.TipoConcepto = "D"
        cNomina.Unidad = 1
        cNomina.Importe = ImporteIncidencia
        cNomina.ImporteGravado = 0
        cNomina.ImporteExento = ImporteIncidencia
        cNomina.Generado = ""
        cNomina.Timbrado = ""
        cNomina.Enviado = ""
        cNomina.Situacion = "A"
        cNomina.GuadarNomina()
    End Sub
    Private Function ChecarSiExiste(ByVal NoEmpleado As Int64, ByVal CvoConcepto As Int32) As Boolean
        Try

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = CvoConcepto
            dt = cNomina.ConsultarConceptosEmpleado()

            If dt.Rows.Count = 0 Then
                ChecarSiExiste = False
            Else
                ChecarSiExiste = True
            End If

            If CvoConcepto = 10 And ChecarSiExiste = True Then
                If dt.Rows(0).Item("TipoHorasExtra").ToString.Trim = "01" And cmbTipoHorasExtra.SelectedValue.ToString = "01" Then
                    ChecarSiExiste = True
                ElseIf dt.Rows(0).Item("TipoHorasExtra").ToString.Trim = "02" And cmbTipoHorasExtra.SelectedValue.ToString = "02" Then
                    ChecarSiExiste = True
                ElseIf dt.Rows(0).Item("TipoHorasExtra").ToString.Trim = "03" And cmbTipoHorasExtra.SelectedValue.ToString = "03" Then
                    ChecarSiExiste = True
                Else
                    ChecarSiExiste = False
                End If
            End If

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Function
    Private Function ChecarQueExistaLaCuotaPeriodo(ByVal NoEmpleado As Int64) As Boolean
        Try

            Call CargarVariablesGenerales()

            Dim ImporteIncidencia As Decimal
            Dim UnidadIncidencia As Decimal

            Try
                ImporteIncidencia = Convert.ToDecimal(txtImporteIncidencia.Text)
            Catch ex As Exception
                ImporteIncidencia = 0
            End Try

            Try
                UnidadIncidencia = Convert.ToDecimal(txtUnidadIncidencia.Text)
            Catch ex As Exception
                UnidadIncidencia = 0
            End Try

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            dt = cNomina.ConsultarConceptosEmpleado()

            If dt.Rows.Count = 0 Or dt.Compute("Sum(Importe)", "CvoConcepto=2") Is DBNull.Value Then
                ChecarQueExistaLaCuotaPeriodo = False
            ElseIf dt.Rows.Count >= 0 And dt.Compute("Sum(Importe)", "CvoConcepto=2") IsNot DBNull.Value Then
                If dt.Compute("Sum(Importe)", "CvoConcepto=57 Or CvoConcepto=58 Or CvoConcepto=59 Or CvoConcepto=161 Or CvoConcepto=162") IsNot DBNull.Value Then
                    If dt.Compute("Sum(Importe)", "CvoConcepto=2") < (dt.Compute("Sum(Importe)", "CvoConcepto=57 Or CvoConcepto=58 Or CvoConcepto=59 Or CvoConcepto=161 Or CvoConcepto=162") + ImporteIncidencia) Or dt.Compute("Sum(Unidad)", "CvoConcepto=2") < (dt.Compute("Sum(Unidad)", "CvoConcepto=57 Or CvoConcepto=58 Or CvoConcepto=59 Or CvoConcepto=161 Or CvoConcepto=162") + UnidadIncidencia) Then
                        ChecarQueExistaLaCuotaPeriodo = False
                    ElseIf dt.Compute("Sum(Importe)", "CvoConcepto=2") > (dt.Compute("Sum(Importe)", "CvoConcepto=57 Or CvoConcepto=58 Or CvoConcepto=59 Or CvoConcepto=161 Or CvoConcepto=162") + ImporteIncidencia) Or dt.Compute("Sum(Unidad)", "CvoConcepto=2") > (dt.Compute("Sum(Unidad)", "CvoConcepto=57 Or CvoConcepto=58 Or CvoConcepto=59 Or CvoConcepto=161 Or CvoConcepto=162") + UnidadIncidencia) Then
                        ChecarQueExistaLaCuotaPeriodo = True
                    End If
                ElseIf dt.Compute("Sum(Importe)", "CvoConcepto=57 Or CvoConcepto=58 Or CvoConcepto=59 Or CvoConcepto=161 Or CvoConcepto=162") Is DBNull.Value Then
                    If dt.Compute("Sum(Importe)", "CvoConcepto=2") < ImporteIncidencia Or dt.Compute("Sum(Unidad)", "CvoConcepto=2") < UnidadIncidencia Then
                        ChecarQueExistaLaCuotaPeriodo = False
                    ElseIf dt.Compute("Sum(Importe)", "CvoConcepto=2") > ImporteIncidencia Or dt.Compute("Sum(Unidad)", "CvoConcepto=2") > UnidadIncidencia Then
                        ChecarQueExistaLaCuotaPeriodo = True
                    End If
                End If
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Function
    Private Sub SolicitarGeneracionXml(ByVal NoEmpleado As Int64, ByVal Generado As String)

        Call CargarVariablesGenerales()

        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Tipo = "N"
        cNomina.Periodo = periodoId.Value
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Generado = Generado
        cNomina.ActualizarEstatusGeneradoNomina()
        cNomina = Nothing

    End Sub
    Private Sub CargarPercepciones()

        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = periodoId.Value
        cNomina.TipoConcepto = "P"
        cNomina.NoEmpleado = empleadoId.Value
        dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
        cNomina = Nothing
        GridPercepciones.DataSource = dt
        GridPercepciones.DataBind()

        If dt.Rows.Count > 0 Then
            txtPercepciones.Text = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
        End If
    End Sub
    Private Sub CargarDeducciones()

        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = periodoId.Value
        cNomina.TipoConcepto = "D"
        cNomina.NoEmpleado = empleadoId.Value
        dt = cNomina.ConsultarDeduccionesEmpleado()
        cNomina = Nothing
        GridDeduciones.DataSource = dt
        GridDeduciones.DataBind()

        If dt.Rows.Count > 0 Then
            txtDeducciones.Text = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
        End If
    End Sub
    Private Sub CargarOtrosPagos()

        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = periodoId.Value
        cNomina.TipoConcepto = "P"
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.Tipo = "N"
        cNomina.OtroPagoBit = 1
        dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
        cNomina = Nothing
        GridOtrosPagos.DataSource = dt
        GridOtrosPagos.DataBind()

        If dt.Rows.Count > 0 Then
            txtOtrosPagos.Text = Math.Round(dt.Compute("Sum(Importe)", "CvoSAT=002 OR CvoSAT=999"), 6)
        End If
    End Sub
    Private Sub btnEliminarConcepto_Click(sender As Object, e As EventArgs) Handles btnEliminarConcepto.Click
        EliminarConcepto(conceptoId.Value, tipohorasextraId.Value)
    End Sub
    Private Sub txtDias_TextChanged(sender As Object, e As EventArgs) Handles txtDias.TextChanged

        Dim Dias As Decimal = 0

        Try
            Dias = Convert.ToDecimal(txtDias.Text)
        Catch ex As Exception
            Dias = 0
        End Try

        If Dias = 0 Then
            rwAlerta.RadAlert("Ingresa una cantidad de dias válidos!!!", 490, 210, "Alerta", "", "")
        Else
            Dim NumeroConcepto As Integer
            ImporteDiario = 0
            ImportePeriodo = 0
            ImporteExento = 0
            ImporteGravado = 0
            Agregar = 3
            NumeroConcepto = 2
            CuotaDiaria = 0

            Try
                CuotaDiaria = Convert.ToDecimal(txtCuotaDiaria.Text)
            Catch ex As Exception
                CuotaDiaria = 0
            End Try

            Try
                SalarioDiarioIntegradoTrabajador = Convert.ToDecimal(txtIntegradoImss.Text)
            Catch ex As Exception
                SalarioDiarioIntegradoTrabajador = 0
            End Try

            'oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            ChecarSiExistenDiasEnPercepciones(empleadoId.Value, NumeroConcepto)
            'en este caso, si se pagan conceptos tales como comisiones, en teoria estas se generan posterior a que el trabajador tiene un sueldo, es decir una cuaota del periodo actual, ese es el motivo del mensaje.
            If DiasCuotaPeriodo = 0 And DiasVacaciones = 0 And DiasComision = 0 And DiasPagoPorHoras = 0 And DiasDestajo = 0 And DiasHonorarioAsimilado = 0 Then
                rwAlerta.RadAlert("Esta percepcion no puede quitarse sin que exista alguna de las siguientes: 1.- CuotaPeriodo. 2.- Vacaciones. 3.- Honorario Asimilado. 4.- Pago Por Horas. 5.- Comisión. 6.- Destajo. Pues estas van acompañadas implicitamente por los 7 dias correspondientes al periodo semanal lo cual es base necesaria para el cálculo del impuesto, lo que si puede hacer es cambiar el número de dias o eliminar completamente el empleado en este periodo!!!", 490, 210, "Alerta", "", "")
                Exit Sub
            End If

            Call ActualizarDiasYCuotaPeriodo()
            'en esta parte se checa si el concepto que se esta agregando es percepcion(menor que 51) o es falta, permiso o incapacidad(57,58,59), siempre se borran los impuestos actuales y se recalculan, la demas deduccioen no entran aqui ya que no recalculan impuestos, simplemente se restan
            If NumeroConcepto < 52 Or NumeroConcepto = "57" Or NumeroConcepto = "58" Or NumeroConcepto = "59" Or NumeroConcepto = "161" Or NumeroConcepto = "162" Then
                BorrarDeducciones(empleadoId.Value)
            End If
            'QuitarConcepto()
            'If cmbConcepto.SelectedValue.ToString < 52 Or cmbConcepto.SelectedValue.ToString = "57" Or cmbConcepto.SelectedValue.ToString = "58" Or cmbConcepto.SelectedValue.ToString = "59" Then
            If NumeroConcepto < 52 Or NumeroConcepto = "57" Or NumeroConcepto = "58" Or NumeroConcepto = "59" Or NumeroConcepto = "161" Or NumeroConcepto = "162" Then

                Call QuitarConcepto(52, "") 'IMPUESTO
                Call QuitarConcepto(54, "") 'SUBSIDIO
                Call QuitarConcepto(56, "") 'CUOTA IMSS

                Call ChecarPercepcionesGravadas(empleadoId.Value, NumeroConcepto)
                Call ChecarYGrabarPercepcionesExentasYGravadas(empleadoId.Value, 0)
                Call ChecarPercepcionesExentasYGravadas()

                Dim cPeriodo As New Entities.Periodo()
                cPeriodo.IdPeriodo = periodoId.Value
                cPeriodo.ConsultarPeriodoID()

                Call CalcularImss()

                Imss = Imss * NumeroDeDiasPagados
                Imss = Math.Round(Imss, 6)

                If Imss > 0 Then
                    Dim cNomina = New Nomina()
                    cNomina.Cliente = empresaId.Value
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 2 'Catorcenal
                    cNomina.Periodo = Periodo
                    cNomina.NoEmpleado = empleadoId.Value
                    cNomina.CvoConcepto = 56
                    cNomina.IdContrato = contratoId.Value
                    cNomina.TipoConcepto = "D"
                    cNomina.Unidad = 1
                    cNomina.Importe = Imss
                    cNomina.ImporteGravado = 0
                    cNomina.ImporteExento = Imss
                    cNomina.Generado = ""
                    cNomina.Timbrado = ""
                    cNomina.Enviado = ""
                    cNomina.Situacion = "A"
                    cNomina.EsEspecial = False
                    cNomina.FechaIni = cPeriodo.FechaInicialDate
                    cNomina.FechaFin = cPeriodo.FechaFinalDate
                    cNomina.FechaPago = cPeriodo.FechaPago
                    cNomina.DiasPagados = cPeriodo.Dias
                    cNomina.IdNomina = nominaId.Value
                    cNomina.GuadarNominaPeriodo()
                End If

                Call CalcularImpuesto()

                Impuesto = Math.Round(Impuesto, 6)

                SubsidioAplicado = 0
                ImporteDiarioGravado = 0
                BaseGravableMensualSubsidioDiario = (BaseGravableMensualSubsidio / FactorDiarioPromedio)
                ImporteDiarioGravado = PercepcionesGravadas / NumeroDeDiasPagados

                If ImporteDiarioGravado <= BaseGravableMensualSubsidioDiario Then
                    UMAMensual = UMA * FactorDiarioPromedio
                    SubsidioMensual = UMAMensual * (FactorSubsidio / 100)
                    SubsidioDiario = SubsidioMensual / FactorDiarioPromedio

                    If (Impuesto > 0 And (Impuesto < (SubsidioDiario * NumeroDeDiasPagados))) Then
                        SubsidioAplicado = Impuesto
                    Else
                        SubsidioAplicado = (SubsidioDiario * NumeroDeDiasPagados)
                    End If

                    If Impuesto > SubsidioAplicado Then
                        Impuesto = Impuesto - SubsidioAplicado
                    ElseIf Impuesto < SubsidioAplicado Then
                        SubsidioAplicado = SubsidioAplicado - Impuesto
                        Impuesto = 0
                    End If

                End If

                If Impuesto > 0 Then
                    Dim cNomina = New Nomina()
                    cNomina.Cliente = empresaId.Value
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 2 'Catorcenal
                    cNomina.Periodo = Periodo
                    cNomina.NoEmpleado = empleadoId.Value
                    cNomina.CvoConcepto = 52
                    cNomina.IdContrato = contratoId.Value
                    cNomina.TipoConcepto = "D"
                    cNomina.Unidad = 1
                    cNomina.Importe = Impuesto
                    cNomina.ImporteGravado = 0
                    cNomina.ImporteExento = Impuesto
                    cNomina.Generado = ""
                    cNomina.Timbrado = ""
                    cNomina.Enviado = ""
                    cNomina.Situacion = "A"
                    cNomina.EsEspecial = False
                    cNomina.FechaIni = cPeriodo.FechaInicialDate
                    cNomina.FechaFin = cPeriodo.FechaFinalDate
                    cNomina.FechaPago = cPeriodo.FechaPago
                    cNomina.DiasPagados = cPeriodo.Dias
                    cNomina.IdNomina = nominaId.Value
                    cNomina.GuadarNominaPeriodo()
                End If

                If SubsidioAplicado > 0 Then
                    Dim cNomina = New Nomina()
                    cNomina.Cliente = empresaId.Value
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 2 'Catorcenal
                    cNomina.Periodo = Periodo
                    cNomina.NoEmpleado = empleadoId.Value
                    cNomina.CvoConcepto = 54
                    cNomina.IdContrato = contratoId.Value
                    cNomina.TipoConcepto = "P"
                    cNomina.Unidad = 1
                    cNomina.Importe = SubsidioAplicado
                    cNomina.ImporteGravado = 0
                    cNomina.ImporteExento = SubsidioAplicado
                    cNomina.Generado = ""
                    cNomina.Timbrado = ""
                    cNomina.Enviado = ""
                    cNomina.Situacion = "A"
                    cNomina.EsEspecial = False
                    cNomina.FechaIni = cPeriodo.FechaInicialDate
                    cNomina.FechaFin = cPeriodo.FechaFinalDate
                    cNomina.FechaPago = cPeriodo.FechaPago
                    cNomina.DiasPagados = cPeriodo.Dias
                    cNomina.IdNomina = nominaId.Value
                    cNomina.GuadarNominaPeriodo()
                End If

            End If

            Call GuardarRegistro(CuotaDiaria, 2)

            '*******************************************************************************************************
            '*******************************************************************************************************
            Call ChecarYGrabarPercepcionesExentasYGravadas(empleadoId.Value, 0)
            '*******************************************************************************************************
            '*******************************************************************************************************

            Call SolicitarGeneracionXml(empleadoId.Value, "")
            Call CargarPercepcionesYDeducciones()
            Call CargarPercepciones()
            Call CargarDeducciones()
            Call CargarOtrosPagos()
            Call ChecarPercepcionesExentasYGravadas()
            Me.txtGravadoISR.Text = Math.Round(PercepcionesGravadas, 6)
            Me.txtExentoISR.Text = Math.Round(PercepcionesExentas, 6)
            NumeroConcepto = 0
        End If
    End Sub
    Private Sub ActualizarDiasYCuotaPeriodo()

        Dim Dias As Decimal = 0

        Try
            Dias = Convert.ToDecimal(txtDias.Text)
        Catch ex As Exception
            Dias = 0
        End Try

        If Dias = 0 Then
            rwAlerta.RadAlert("Ingresa una cantidad de dias válidos!!!", 490, 210, "Alerta", "", "")
        Else
            Call CargarVariablesGenerales()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.CvoConcepto = 85
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.Unidad = Dias
            cNomina.Importe = Dias * Convert.ToDecimal(CuotaDiaria)
            cNomina.ActualizarDiasYCuotaPeriodo()
            cNomina = Nothing
        End If
    End Sub
    Private Sub GuardarExentoYGravado(ByVal CvoConcepto, ByVal ImporteGravado, ByVal ImporteExento, ByVal NoEmpleado, Optional ByVal TipoHorasExtra = 0)
        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = periodoId.Value
        cNomina.NoEmpleado = NoEmpleado
        cNomina.CvoConcepto = CvoConcepto
        cNomina.ImporteGravado = ImporteGravado
        cNomina.ImporteExento = ImporteExento
        If CvoConcepto = 10 Then
            cNomina.TipoHorasExtra = TipoHorasExtra
        End If
        cNomina.ActualizarExentoYGravado()
        cNomina = Nothing
    End Sub
    Private Sub txtCuotaDiaria_TextChanged(sender As Object, e As EventArgs) Handles txtCuotaDiaria.TextChanged

        Try
            CuotaDiaria = Convert.ToDecimal(txtCuotaDiaria.Text)
        Catch ex As Exception
            CuotaDiaria = 0
        End Try
        If CuotaDiaria = 0 Then
            rwAlerta.RadAlert("Ingresa un importe de cuota diaria válido!!!", 490, 210, "Alerta", "", "")
        Else
            Dim NumeroConcepto As Integer
            ImporteDiario = 0
            ImportePeriodo = 0
            ImporteExento = 0
            ImporteGravado = 0
            SubsidioAplicado = 0
            SubsidioEfectivo = 0
            Agregar = 3
            NumeroConcepto = 2

            Dim cEmpleado As New Entities.Empleado
            cEmpleado.IdEmpleado = empleadoId.Value
            cEmpleado.ConsultarEmpleadoID()
            If cEmpleado.IdEmpleado > 0 Then
                SalarioDiarioIntegradoTrabajador = cEmpleado.IntegradoImss
            End If
            'oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            ChecarSiExistenDiasEnPercepciones(empleadoId.Value, NumeroConcepto)
            'en este caso, si se pagan conceptos tales como comisiones, en teoria estas se generan posterior a que el trabajador tiene un sueldo, es decir una cuaota del periodo actual, ese es el motivo del mensaje.
            If DiasCuotaPeriodo = 0 And DiasVacaciones = 0 And DiasComision = 0 And DiasPagoPorHoras = 0 And DiasDestajo = 0 And DiasHonorarioAsimilado = 0 Then
                rwAlerta.RadAlert("Esta percepcion no puede quitarse sin que exista alguna de las siguientes: 1.- CuotaPeriodo. 2.- Vacaciones. 3.- Honorario Asimilado. 4.- Pago Por Horas. 5.- Comisión. 6.- Destajo. Pues estas van acompañadas implicitamente por los 7 dias correspondientes al periodo semanal lo cual es base necesaria para el cálculo del impuesto, lo que si puede hacer es cambiar el número de dias o eliminar completamente el empleado en este periodo!!!", 490, 210, "Alerta", "", "")
                Exit Sub
            End If

            Call ActualizarDiasYCuotaPeriodo()
            'en esta parte se checa si el concepto que se esta agregando es percepcion(menor que 51) o es falta, permiso o incapacidad(57,58,59), siempre se borran los impuestos actuales y se recalculan, la demas deduccioen no entran aqui ya que no recalculan impuestos, simplemente se restan
            If NumeroConcepto < 52 Or NumeroConcepto = "57" Or NumeroConcepto = "58" Or NumeroConcepto = "59" Or NumeroConcepto = "161" Or NumeroConcepto = "162" Then
                BorrarDeducciones(empleadoId.Value)
            End If
            'QuitarConcepto()
            'If cmbConcepto.SelectedValue.ToString < 52 Or cmbConcepto.SelectedValue.ToString = "57" Or cmbConcepto.SelectedValue.ToString = "58" Or cmbConcepto.SelectedValue.ToString = "59" Then
            If NumeroConcepto < 52 Or NumeroConcepto = "57" Or NumeroConcepto = "58" Or NumeroConcepto = "59" Or NumeroConcepto = "161" Or NumeroConcepto = "162" Then
                ChecarPercepcionesGravadas(empleadoId.Value, NumeroConcepto)
                CalcularImpuesto()
                CalcularSubsidio()

                Call CalcularImss()
                'Imss = Imss * (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo + DiasHonorarioAsimilado)
                Imss = Imss * DiasCuotaPeriodo
                Imss = Math.Round(Imss, 6)

                Impuesto = Impuesto * (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo + DiasHonorarioAsimilado)
                Subsidio = Subsidio * (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo)
                If Impuesto > Subsidio Then
                    SubsidioEfectivo = 0
                    Impuesto = Impuesto - Subsidio
                ElseIf Impuesto < Subsidio Then
                    SubsidioAplicado = 0
                    SubsidioEfectivo = Subsidio - Impuesto
                    Impuesto = 0
                End If
                Impuesto = Math.Round(Impuesto, 6)
                SubsidioAplicado = Math.Round(SubsidioAplicado, 6)
                SubsidioEfectivo = Math.Round(SubsidioEfectivo, 6)
            End If

            GuardarRegistro(CuotaDiaria, 2)
            'Catalogo fusionado
            '*******************************************************************************************************
            '*******************************************************************************************************
            ChecarYGrabarPercepcionesExentasYGravadas(empleadoId.Value, 0)
            '*******************************************************************************************************
            '*******************************************************************************************************
            SolicitarGeneracionXml(empleadoId.Value, "")
            Call CargarPercepcionesYDeducciones()
            Call CargarPercepciones()
            Call CargarDeducciones()

            Call ChecarPercepcionesExentasYGravadas()
            txtGravadoISR.Text = Math.Round(PercepcionesGravadas, 6)
            txtExentoISR.Text = Math.Round(PercepcionesExentas, 6)
            NumeroConcepto = 0
        End If
    End Sub
    Private Sub txtIntegradoImss_TextChanged(sender As Object, e As EventArgs) Handles txtIntegradoImss.TextChanged

        Try
            CuotaDiaria = Convert.ToDecimal(txtCuotaDiaria.Text)
        Catch ex As Exception
            CuotaDiaria = 0
        End Try

        Try
            SalarioDiarioIntegradoTrabajador = Convert.ToDecimal(txtIntegradoImss.Text)
        Catch ex As Exception
            SalarioDiarioIntegradoTrabajador = 0
        End Try

        If CuotaDiaria = 0 Then
            rwAlerta.RadAlert("Ingresa un importe de cuota diaria válido!!!", 490, 210, "Alerta", "", "")
        Else
            If SalarioDiarioIntegradoTrabajador = 0 Then
                rwAlerta.RadAlert("Ingresa un importe de Integrado IMSS válido!!!", 490, 210, "Alerta", "", "")
            Else

                QuitarConcepto(56, "")

                'oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
                'ChecarSiExistenDiasEnPercepciones(empleadoId.Value, 56)
                ChecarPercepcionesGravadas(empleadoId.Value, 56)
                'en este caso, si se pagan conceptos tales como comisiones, en teoria estas se generan posterior a que el trabajador tiene un sueldo, es decir una cuaota del periodo actual, ese es el motivo del mensaje.
                If DiasCuotaPeriodo = 0 And DiasVacaciones = 0 And DiasComision = 0 And DiasPagoPorHoras = 0 And DiasDestajo = 0 And DiasHonorarioAsimilado = 0 Then
                    rwAlerta.RadAlert("Esta percepcion no puede quitarse sin que exista alguna de las siguientes: 1.- CuotaPeriodo. 2.- Vacaciones. 3.- Honorario Asimilado. 4.- Pago Por Horas. 5.- Comisión. 6.- Destajo. Pues estas van acompañadas implicitamente por los 7 dias correspondientes al periodo semanal lo cual es base necesaria para el cálculo del impuesto, lo que si puede hacer es cambiar el número de dias o eliminar completamente el empleado en este periodo!!!", 490, 210, "Alerta", "", "")
                    Exit Sub
                End If

                Call CalcularImss()
                'Imss = Imss * (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo + DiasHonorarioAsimilado)
                Imss = Imss * DiasCuotaPeriodo
                Imss = Math.Round(Imss, 6)

                If Imss > 0 Then
                    Dim cNomina = New Nomina()
                    'cNomina.IdEmpresa = IdEmpresa
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 2 'Catorcenal
                    cNomina.Periodo = Periodo
                    cNomina.NoEmpleado = empleadoId.Value
                    cNomina.CvoConcepto = 56
                    cNomina.IdContrato = contratoId.Value
                    cNomina.TipoConcepto = "D"
                    cNomina.Unidad = 1
                    cNomina.Importe = Imss
                    cNomina.ImporteGravado = 0
                    cNomina.ImporteExento = Imss
                    cNomina.Generado = ""
                    cNomina.Timbrado = ""
                    cNomina.Enviado = ""
                    cNomina.Situacion = "A"
                    cNomina.GuadarNomina()
                End If

                'Catalogo fusionado
                '*******************************************************************************************************
                '*******************************************************************************************************
                ChecarYGrabarPercepcionesExentasYGravadas(empleadoId.Value, 0)
                '*******************************************************************************************************
                '*******************************************************************************************************
                SolicitarGeneracionXml(empleadoId.Value, "")
                Call CargarPercepcionesYDeducciones()
                Call CargarPercepciones()
                Call CargarDeducciones()

                Call ChecarPercepcionesExentasYGravadas()
                txtGravadoISR.Text = Math.Round(PercepcionesGravadas, 6)
                txtExentoISR.Text = Math.Round(PercepcionesExentas, 6)


            End If
        End If
    End Sub
    Private Sub cmbConcepto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbConcepto.SelectedIndexChanged
        If cmbConcepto.SelectedValue = 10 Then
            lblDiasHorasExtra.Visible = True
            txtDiasHorasExtra.Visible = True
            lblTipoHorasExtra.Visible = True
            cmbTipoHorasExtra.Visible = True
            Call LlenaCmbTipoHorasExtra(0)
            txtDiasHorasExtra.Text = ""
        Else
            lblDiasHorasExtra.Visible = False
            txtDiasHorasExtra.Visible = False
            lblTipoHorasExtra.Visible = False
            cmbTipoHorasExtra.Visible = False
        End If
    End Sub
    Private Sub cmbTipoHorasExtra_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipoHorasExtra.SelectedIndexChanged
        Try
            CuotaDiaria = Convert.ToDecimal(txtCuotaDiaria.Text)
        Catch ex As Exception
            CuotaDiaria = 0
        End Try
        Dim Unidad As Decimal = 0
        Try
            Unidad = Convert.ToDecimal(txtUnidadIncidencia.Text)
        Catch ex As Exception
            Unidad = 0
        End Try

        If cmbTipoHorasExtra.SelectedValue = "01" Then
            txtImporteIncidencia.Text = ((CuotaDiaria / 8) * 2) * Unidad
        ElseIf cmbTipoHorasExtra.SelectedValue = "02" Then
            txtImporteIncidencia.Text = ((CuotaDiaria / 8) * 3) * Unidad
        ElseIf cmbTipoHorasExtra.SelectedValue = "03" Then
            txtImporteIncidencia.Text = (CuotaDiaria / 8) * Unidad
        End If
    End Sub
    Private Sub GridOtrosPagos_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridOtrosPagos.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                conceptoId.Value = e.CommandArgument
                rwConfirmEliminaConcepto.RadConfirm("¿Está seguro de eliminar este concepto?", "confirmCallbackFnEliminaConcepto", 330, 180, Nothing, "Confirmar")
        End Select
    End Sub
    Private Sub GridPercepciones_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridPercepciones.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim btnEliminar As ImageButton = CType(e.Item.FindControl("btnEliminar"), ImageButton)
                If (e.Item.DataItem("CvoConcepto") = "002") Or (e.Item.DataItem("CvoConcepto") = "054") Then
                    btnEliminar.Visible = False
                End If
        End Select
    End Sub
    Private Sub GridDeduciones_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridDeduciones.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim btnEliminar As ImageButton = CType(e.Item.FindControl("btnEliminar"), ImageButton)
                If (e.Item.DataItem("CvoConcepto") = "052") Or (e.Item.DataItem("CvoConcepto") = "056") Then
                    btnEliminar.Visible = False
                End If
        End Select
    End Sub
    Private Sub GridOtrosPagos_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridOtrosPagos.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim btnEliminar As ImageButton = CType(e.Item.FindControl("btnEliminar"), ImageButton)
                If (e.Item.DataItem("CvoConcepto") = "054") Then
                    btnEliminar.Visible = False
                End If
        End Select
    End Sub
    Private Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
        Dim pagina = "ModificacionGeneralCatorcenal.aspx"
        If Not String.IsNullOrEmpty(Request("page")) Then
            pagina = Request("page")
        End If

        If Not String.IsNullOrEmpty(Request("id")) Then
            pagina = pagina & "?id=" & Request("id")
        End If

        'If Not String.IsNullOrEmpty(Request("nominaid")) Then
        '    pagina = pagina & "&cid=" & Request("nominaid")
        'End If

        pagina = pagina & "&cid=" & empresaId.Value.ToString

        ScriptManager.RegisterStartupScript(Me, GetType(RadWindow), "close", "CloseModal('" & pagina & "');", True)

    End Sub

End Class