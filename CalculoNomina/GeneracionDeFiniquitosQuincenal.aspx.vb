Imports Telerik.Web.UI
Imports Entities
Imports System.Xml
Imports System.IO
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography
Imports Telerik.Reporting.Processing
Public Class GeneracionDeFiniquitosQuincenal
    Inherits System.Web.UI.Page

    'Private IdEmpresa As Integer = 0
    Private IdEjercicio As Integer = 0
    Private IdPeriodo As Integer = 0
    Private SalarioMinimoDiarioGeneral As Decimal = 0
    Private ImporteSeguroVivienda As Decimal = 0

    Private ImporteDiario As Decimal = 0
    Private ImportePeriodo As Decimal = 0
    Private ImporteExento As Decimal = 0
    Private ImporteGravado As Decimal = 0
    Private Subsidio As Decimal = 0
    Private Impuesto As Decimal = 0
    Private SubsidioEfectivo As Decimal = 0
    Private SalarioDiarioIntegradoTrabajador As Decimal = 0
    Private DiasTrabajados As Decimal = 0
    Private FactorDiario As Decimal = 0

    Private CuotaDiaria As Decimal = 0
    Private CuotaPeriodo As Decimal = 0
    Private HorasTriples As Decimal = 0
    Private DescansoTrabajado As Decimal = 0
    Private PrimaDominical As Decimal = 0
    Private PrimaVacacional As Decimal = 0
    Private Vacaciones As Decimal = 0
    Private Aguinaldo As Decimal = 0
    Private RepartoUtilidades As Decimal = 0
    Private FondoAhorro As Decimal = 0
    Private AyudaFuneral As Decimal = 0
    Private PrevisionSocial As Decimal = 0
    Private GrupoPercepcionesGravadasTotalmenteSinExentos As Decimal = 0
    Private PagoPorHoras As Decimal = 0
    Private Comisiones As Decimal = 0
    Private Destajo As Decimal = 0
    Private FaltasPermisosIncapacidades As Decimal = 0
    Private NumeroDeDiasPagados As Decimal = 0

    Private TiempoExtraordinarioDentroDelMargenLegal As Decimal = 0
    Private TiempoExtraordinarioFueraDelMargenLegal As Decimal = 0
    Private Agregar As String
    Private DiasVacaciones As Decimal = 0
    Private DiasCuotaPeriodo As Decimal = 0
    Private DiasHonorarioAsimilado As Decimal = 0
    Private DiasPagoPorHoras As Decimal = 0
    Private DiasComision As Decimal = 0
    Private DiasDestajo As Decimal = 0
    Private DiasFaltasPermisosIncapacidades As Decimal = 0
    Private HonorarioAsimilado As Decimal = 0

    Private ImporteExentoTiempoExtraordinarioDentroDelMargenLegal As Decimal = 0
    Private ImporteGravadoTiempoExtraordinarioDentroDelMargenLegal As Decimal = 0
    Private ImporteExentoPrimaDominical As Decimal = 0
    Private ImporteGravadoPrimaDominical As Decimal = 0
    Private ImporteExentoAguinaldo As Decimal = 0
    Private ImporteGravadoAguinaldo As Decimal = 0
    Private ImporteExentoPrimaVacacional As Decimal = 0
    Private ImporteGravadoPrimaVacacional As Decimal = 0
    Private ImporteExentoRepartoUtilidades As Decimal = 0
    Private ImporteGravadoRepartoUtilidades As Decimal = 0
    Private ImporteExentoPrevisionSocial As Decimal = 0
    Private ImporteGravadoPrevisionSocial As Decimal = 0

    Private AyudaCulturalExento As Decimal = 0
    Private AyudaCulturalGravado As Decimal = 0
    Private AyudaDeportivaExento As Decimal = 0
    Private AyudaDeportivaGravado As Decimal = 0
    Private AyudaEducacionalExento As Decimal = 0
    Private AyudaEducacionalGravado As Decimal = 0
    Private AyudaEscolarExento As Decimal = 0
    Private AyudaEscolarGravado As Decimal = 0
    Private AyudaComidaExento As Decimal = 0
    Private AyudaComidaGravado As Decimal = 0
    Private ValesDespensaExento As Decimal = 0
    Private ValesDespensaGravado As Decimal = 0
    Private AyudaUniformeExento As Decimal = 0
    Private AyudaUniformeGravado As Decimal = 0
    Private BecasExento As Decimal = 0
    Private BecasGravado As Decimal = 0
    Private SubsidioIncapacidadExento As Decimal = 0
    Private SubsidioIncapacidadGravado As Decimal = 0
    Private AyudaMatrimonioExento As Decimal = 0
    Private AyudaMatrimonioGravado As Decimal = 0
    Private AyudaNacimientoExento As Decimal = 0
    Private AyudaNacimientoGravado As Decimal = 0
    Private ValesComedorExento As Decimal = 0
    Private ValesComedorGravado As Decimal = 0
    Private AyudaMedicamentoExento As Decimal = 0
    Private AyudaMedicamentoGravado As Decimal = 0
    Private ImporteExentoFondoAhorro As Decimal = 0
    Private ImporteGravadoFondoAhorro As Decimal = 0
    Private ImporteExentoAyudaFuneral As Decimal = 0
    Private ImporteGravadoAyudaFuneral As Decimal = 0

    Private Diferencias As Decimal = 0
    Private Gratificacion As Decimal = 0
    Private Bonificacion As Decimal = 0
    Private Retroactivo As Decimal = 0
    Private BonoProduccion As Decimal = 0
    Private PremioProductividad As Decimal = 0
    Private Incentivo As Decimal = 0
    Private PremioAsistencia As Decimal = 0
    Private PremioPuntualidad As Decimal = 0
    Private Premio As Decimal = 0
    Private Compensacion As Decimal = 0
    Private BonoAntiguedad As Decimal = 0
    Private Viaticos As Decimal = 0
    Private Pasajes As Decimal = 0
    Private AyudaTransporte As Decimal = 0
    Private AyudaRenta As Decimal = 0
    Private AyudaCarestia As Decimal = 0
    Private DespensaEfectivo As Decimal = 0
    Private HaberPorRetiro As Decimal = 0
    Private OtrasPercepciones As Decimal = 0

    Private HorasDoblesGravadas As Decimal = 0
    Private HorasDoblesExentas As Decimal = 0
    Private FestivoTrabajadoGravado As Decimal = 0
    Private FestivoTrabajadoExento As Decimal = 0
    Private DobleteGravado As Decimal = 0
    Private DobleteExento As Decimal = 0

    Private TotalPercepciones As Decimal = 0
    Private TotalDeducciones As Decimal = 0
    Private NetoAPagar As Decimal = 0

    Private PercepcionesExentas As Decimal = 0
    Private PercepcionesGravadas As Decimal = 0
    Private Imss As Decimal = 0
    Public FolioXml As String

    Const URI_SAT = "http://www.sat.gob.mx/cfd/3"
    Private m_xmlDOM As New XmlDocument
    Private urlnomina As String
    Private Unidad As String

    Private DiasProporcionalVacaciones As Decimal
    Private ProporcionalVacaciones As Decimal

    Private ImpuestoVacaciones As Decimal = 0
    Private SubsidioVacaciones As Decimal = 0
    Private SubsidioEfectivoVacaciones As Decimal = 0

    Private ImpuestoAguinaldo As Decimal = 0
    Private SubsidioAguinaldo As Decimal = 0
    Private SubsidioEfectivoAguinaldo As Decimal = 0

    Private ImpuestoIncidencias As Decimal = 0
    Private SubsidioIncidencias As Decimal = 0
    Private SubsidioEfectivoIncidencias As Decimal = 0

    Private ImpuestoSueldoMesAnterior As Decimal = 0
    Private SubsidioSueldoMesAnterior As Decimal = 0
    Private SubsidioEfectivoSueldoMesAnterior As Decimal = 0

    Private SubsidioPercepcionesEspecial As Decimal = 0
    Private ImpuestoPercepcionesEspecial As Decimal = 0
    Private SubsidioPorPagarSueldos As Decimal = 0
    Private ImpuestoPorPagarSueldos As Decimal = 0

    Private DiasMes As Decimal = 0
    Private ImporteGravadoFiniquito As Decimal = 0
    Private ImporteGravadoVacaciones As Decimal = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request("id")) Then
                Call LlenaComboPeriodos(0)
                Call MostrarDatosFiniquito()
                cmbConcepto.Items.Add(New ListItem("--Seleccione--", 0))
                cmbConcepto.Items.Add(New ListItem("DIAS PENDIENTES DE PAGO", 51)) '51
                cmbConcepto.Items.Add(New ListItem("GRATIFICACIÓN", 17)) '51
                cmbConcepto.DataBind()

                Dim cConfiguracion As New Entities.Configuracion
                'cConfiguracion.IdEmpresa = Session("clienteid")
                cConfiguracion.IdUsuario = Session("usuarioid")
                cConfiguracion.ActualizaSalarioMinimoDiarioGeneralFiniquitos()
                cConfiguracion = Nothing
            End If
        End If
    End Sub
    Private Sub LlenaCmbConcepto(ByVal sel As Integer, ByVal Tipo As String)
        Try
            Dim cConcepto As New Entities.Concepto
            If Tipo.Length > 0 Then
                cConcepto.Tipo = Tipo
            End If

            Dim ObjData As New DataControl()
            objData.Catalogo(cmbConcepto, sel, cConcepto.ConsultarConcepto)
            cConcepto = Nothing
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CargarVariablesGenerales()

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
                IdPeriodo = oDataRow("IdPeriodo")
                SalarioMinimoDiarioGeneral = oDataRow("SalarioMinimoDiarioGeneral")
                ImporteSeguroVivienda = oDataRow("ImporteSeguroVivienda")
            Next
        End If
    End Sub
    Private Sub ConsultarDiasMes()
        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.Id = Request("id")
        dt = cNomina.ConsultarEmpleadosFiniquito()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                'DiasMes = DateTime.DaysInMonth(CDate(oDataRow("FechaBaja")).Year, CDate(oDataRow("FechaBaja")).Month)
                DiasMes = 30.4
            Next
        End If
    End Sub
    Private Sub MostrarDatosFiniquito()
        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.Id = Request("id")
        dt = cNomina.ConsultarEmpleadosFiniquito()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                empleadoId.Value = oDataRow("NoEmpleado")
                contratoId.Value = oDataRow("IdContrato")
                lblNoEmpleado.Text = oDataRow("NoEmpleado").ToString
                lblNombreEmpleado.Text = oDataRow("NombreEmpleado").ToString
                lblFechaIngreso.Text = oDataRow("FechaIngreso").ToString
                lblFechaBaja.Text = oDataRow("FechaBaja").ToString
                lblSueldoDiario.Text = FormatCurrency(oDataRow("CuotaDiaria"), 2).ToString
                lblSueldoDiarioIntegrado.Text = FormatCurrency(oDataRow("IntegradoIMSS"), 2).ToString
                'txtUltimoSueldomensual.Text = oDataRow("CuotaDiaria") * 30.4
                SalarioDiarioIntegradoTrabajador = oDataRow("IntegradoIMSS")
            Next
        End If
    End Sub
    Private Sub ConsultarSalarioDiarioIntegradoTrabajador()
        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.Id = Request("id")
        dt = cNomina.ConsultarEmpleadosFiniquito()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                SalarioDiarioIntegradoTrabajador = oDataRow("IntegradoIMSS")
                ImporteDiario = oDataRow("CuotaDiaria")
            Next
        End If
    End Sub
    Private Sub ConsultarCuotaDiaria()
        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.Id = Request("id")
        dt = cNomina.ConsultarEmpleadosFiniquito()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                CuotaDiaria = oDataRow("CuotaDiaria")
            Next
        End If
    End Sub
    Private Sub MostrarDesgloceFiniquito()
        Dim DiasPagadosVacaciones As Integer = 0
        Try
            DiasPagadosVacaciones = Convert.ToInt32(txtDiasPagadosVacaciones.Text)
        Catch ex As Exception
            DiasPagadosVacaciones = 0
        End Try
        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.Id = Request("id")
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.DiasPagadosVacaciones = DiasPagadosVacaciones
        dt = cNomina.ConsultarDesgloseFiniquito()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                lblAntiguedadDias.Text = oDataRow("DiasLaborados").ToString
                lblDiasLaboradosAnio.Text = oDataRow("DiasLaboradosAnio").ToString
                lblDiasVacacionesProporcionales.Text = Math.Round(oDataRow("DiasProporcionalVacaciones"), 2).ToString
                lblVacacionesProporcionales.Text = FormatCurrency(oDataRow("ProporcionalVacaciones"), 2).ToString
                lblPorcentajePrimaVacacional.Text = oDataRow("PorcentajePrimaVacacional").ToString
                lblPrimaVacacionalProporcional.Text = FormatCurrency(oDataRow("PrimaVacaciones"), 2).ToString
                lblDiasAguinaldoAnio.Text = oDataRow("DiasAguinaldoAnio").ToString
                lblAguinaldoProporcional.Text = FormatCurrency(oDataRow("ProporcionalAguinaldo"), 2).ToString
            Next
        End If

    End Sub
    Private Sub LlenaComboPeriodos(ByVal sel As Integer)
        Call CargarVariablesGenerales()
        Dim ObjData As New DataControl()
        Dim cPeriodo As New Entities.Periodo
        'cPeriodo.IdEmpresa = IdEmpresa
        cPeriodo.IdEjercicio = IdEjercicio
        cPeriodo.IdTipoNomina = 3 'Quincenal
        cPeriodo.ExtraordinarioBit = 0
        ObjData.Catalogo(cmbPeriodo, sel, cPeriodo.ConsultarPeriodos())
        cPeriodo = Nothing
    End Sub
    Private Sub btnAceptar_Click(sender As Object, e As EventArgs) Handles btnAceptar.Click
        If cmbPeriodo.SelectedValue > 0 Then
            If cmbTipoFiniquito.SelectedValue > 0 Then
                rwConfirmarGeneraFiniquito.RadConfirm("¿Está seguro que desea generar el cálculo de finiquito?", "confirmCallbackFnGeneraFiniquito", 330, 180, Nothing, "Confirmar")
            Else
                rwAlerta.RadAlert("Seleccione un tipo de finiquito.", 330, 180, "Alerta", "", "")
            End If
        Else
            rwAlerta.RadAlert("Seleccione un periodo.", 330, 180, "Alerta", "", "")
        End If
    End Sub
    Private Sub btnConfirmarGeneraFiniquito_Click(sender As Object, e As EventArgs) Handles btnConfirmarGeneraFiniquito.Click

        Call CargarVariablesGenerales()

        Call EliminaConceptosFiniquito(Request("id"), 0)

        ImporteDiario = 0
        ImportePeriodo = 0
        Subsidio = 0
        Impuesto = 0
        SubsidioEfectivo = 0
        DiasTrabajados = 0
        ImporteGravado = 0
        ImporteGravadoVacaciones = 0
        ImporteGravadoFiniquito = 0

        ImpuestoVacaciones = 0
        SubsidioVacaciones = 0
        SubsidioEfectivoVacaciones = 0

        ImpuestoAguinaldo = 0
        SubsidioAguinaldo = 0
        SubsidioEfectivoAguinaldo = 0

        ImpuestoSueldoMesAnterior = 0
        SubsidioSueldoMesAnterior = 0
        SubsidioEfectivoSueldoMesAnterior = 0

        SubsidioPercepcionesEspecial = 0
        ImpuestoPercepcionesEspecial = 0
        SubsidioPorPagarSueldos = 0
        ImpuestoPorPagarSueldos = 0

        Dim Aguinaldo As Decimal = 0
        Dim Vacaciones As Decimal = 0
        Dim DiasProporcionalVacaciones As Decimal = 0
        Dim PrimaVacacional As Decimal = 0
        Dim ImporteExentoAguinaldo As Decimal = 0
        Dim ImporteGravadoAguinaldo As Decimal = 0
        Dim ImporteExentoPrimaVacacional As Decimal = 0
        Dim ImporteGravadoPrimaVacacional As Decimal = 0

        Dim DiferenciaSubsidioImpuestoVacaciones As Decimal = 0
        Dim FactorImpuestoSubsidioVacaciones As Decimal = 0
        Dim TasaSubsidioImpuestoVacaciones As Decimal = 0

        Dim DiferenciaSubsidioImpuestoAguinaldo As Decimal = 0
        Dim FactorImpuestoSubsidioAguinaldo As Decimal = 0
        Dim TasaSubsidioImpuestoAguinaldo As Decimal = 0

        Dim DiasPagadosVacaciones As Integer = 0
        Try
            DiasPagadosVacaciones = Convert.ToInt32(txtDiasPagadosVacaciones.Text)
        Catch ex As Exception
            DiasPagadosVacaciones = 0
        End Try

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.Id = Request("id")
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.DiasPagadosVacaciones = DiasPagadosVacaciones
        dt = cNomina.ConsultarDesgloseFiniquito()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                ImporteDiario = oDataRow("SueldoDiario")
                Aguinaldo = oDataRow("ProporcionalAguinaldo")
                Vacaciones = oDataRow("ProporcionalVacaciones")
                PrimaVacacional = oDataRow("PrimaVacaciones")
                DiasProporcionalVacaciones = Math.Round(oDataRow("DiasProporcionalVacaciones"), 2)
            Next
        End If

        cNomina = New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.CvoConcepto = 87
        cNomina.IdContrato = contratoId.Value
        cNomina.Tipo = "F"
        cNomina.TipoConcepto = "DE"
        cNomina.Unidad = 1
        cNomina.Importe = ImporteDiario
        cNomina.Generado = "S"
        cNomina.Timbrado = ""
        cNomina.Enviado = ""
        cNomina.Situacion = ""
        cNomina.IdMovimiento = Request("id")
        cNomina.GuadarNomina()

        If Aguinaldo > 0 Then
            If Aguinaldo > 0 And Aguinaldo < (SalarioMinimoDiarioGeneral * 30) Then
                ImporteExento = ImporteExento + Aguinaldo
                ImporteExentoAguinaldo = Aguinaldo
                ImporteGravadoAguinaldo = 0
            ElseIf Aguinaldo > 0 And Aguinaldo > (SalarioMinimoDiarioGeneral * 30) Then
                ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 30)
                ImporteGravadoFiniquito = ImporteGravadoFiniquito + (Aguinaldo - (SalarioMinimoDiarioGeneral * 30))
                ImporteExentoAguinaldo = SalarioMinimoDiarioGeneral * 30
                ImporteGravadoAguinaldo = Aguinaldo - (SalarioMinimoDiarioGeneral * 30)
            End If
            GuardarExentoYGravado(14, 1, Aguinaldo, ImporteGravadoAguinaldo, ImporteExentoAguinaldo, "P")
        End If

        If Vacaciones > 0 Then
            ImporteGravadoVacaciones = Vacaciones
            ImporteGravadoFiniquito = ImporteGravadoFiniquito + Vacaciones
            GuardarExentoYGravado(15, DiasProporcionalVacaciones, Vacaciones, Vacaciones, 0, "P")
        End If

        If PrimaVacacional > 0 Then
            If PrimaVacacional > 0 And PrimaVacacional < (SalarioMinimoDiarioGeneral * 15) Then
                ImporteExento = ImporteExento + PrimaVacacional
                ImporteExentoPrimaVacacional = PrimaVacacional
                ImporteGravadoPrimaVacacional = 0
            ElseIf PrimaVacacional > 0 And PrimaVacacional > (SalarioMinimoDiarioGeneral * 15) Then
                ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 15)
                ImporteGravadoFiniquito = ImporteGravadoFiniquito + (PrimaVacacional - (SalarioMinimoDiarioGeneral * 15))
                ImporteExentoPrimaVacacional = SalarioMinimoDiarioGeneral * 15
                ImporteGravadoPrimaVacacional = PrimaVacacional - (SalarioMinimoDiarioGeneral * 15)
            End If
            GuardarExentoYGravado(16, 1, PrimaVacacional, ImporteGravadoPrimaVacacional, ImporteExentoPrimaVacacional, "P")
        End If

        '1 Impuesto / Subsidio proporcional vacaciones
        Dim ImporteProporcionalVacaciones As Decimal
        Dim IngresoMensualOrdinario As Decimal
        Dim IngresoGravadoMes As Decimal

        Call ConsultarDiasMes()

        ImporteProporcionalVacaciones = (ImporteGravadoVacaciones / 365) * 30.4
        IngresoMensualOrdinario = (ImporteDiario * DiasMes)
        IngresoGravadoMes = ImporteProporcionalVacaciones + IngresoMensualOrdinario

        Call CalcularImpuestoVacaciones(IngresoGravadoMes)
        Call CalcularSubsidioVacaciones(IngresoGravadoMes)

        If ImpuestoVacaciones > SubsidioVacaciones Then
            SubsidioEfectivoVacaciones = 0
            ImpuestoVacaciones = ImpuestoVacaciones - SubsidioVacaciones
        ElseIf ImpuestoVacaciones < SubsidioVacaciones Then
            SubsidioEfectivoVacaciones = SubsidioVacaciones - ImpuestoVacaciones
            ImpuestoVacaciones = 0
        End If

        ImpuestoVacaciones = Math.Round(ImpuestoVacaciones, 6)
        SubsidioEfectivoVacaciones = Math.Round(SubsidioEfectivoVacaciones, 6)

        '2 Impuesto / Subsidio por sueldo ordinario
        Call CalcularImpuestoSueldoMesAnterior(IngresoMensualOrdinario)
        Call CalcularSubsidioSueldoMesAnterior(IngresoMensualOrdinario)

        If ImpuestoSueldoMesAnterior > SubsidioSueldoMesAnterior Then
            SubsidioEfectivoSueldoMesAnterior = 0
            ImpuestoSueldoMesAnterior = ImpuestoSueldoMesAnterior - SubsidioSueldoMesAnterior
        ElseIf ImpuestoSueldoMesAnterior < SubsidioSueldoMesAnterior Then
            SubsidioEfectivoSueldoMesAnterior = SubsidioSueldoMesAnterior - ImpuestoSueldoMesAnterior
            ImpuestoSueldoMesAnterior = 0
        End If

        ImpuestoSueldoMesAnterior = Math.Round(ImpuestoSueldoMesAnterior, 6)
        SubsidioEfectivoSueldoMesAnterior = Math.Round(SubsidioEfectivoSueldoMesAnterior, 6)

        If SubsidioEfectivoVacaciones > 0 And SubsidioEfectivoSueldoMesAnterior > 0 Then

            DiferenciaSubsidioImpuestoVacaciones = SubsidioEfectivoVacaciones - SubsidioEfectivoSueldoMesAnterior

            If DiferenciaSubsidioImpuestoVacaciones < 0 Then
                DiferenciaSubsidioImpuestoVacaciones = DiferenciaSubsidioImpuestoVacaciones * -1
            End If

            FactorImpuestoSubsidioVacaciones = DiferenciaSubsidioImpuestoVacaciones / ImporteProporcionalVacaciones
            TasaSubsidioImpuestoVacaciones = FactorImpuestoSubsidioVacaciones * 1 ' El 1 representa 100%
            SubsidioVacaciones = ImporteGravadoVacaciones * TasaSubsidioImpuestoVacaciones
        Else
            DiferenciaSubsidioImpuestoVacaciones = ImpuestoVacaciones - ImpuestoSueldoMesAnterior

            FactorImpuestoSubsidioVacaciones = DiferenciaSubsidioImpuestoVacaciones / ImporteProporcionalVacaciones
            TasaSubsidioImpuestoVacaciones = FactorImpuestoSubsidioVacaciones * 1 ' El 1 representa 100%
            ImpuestoVacaciones = ImporteGravadoVacaciones * TasaSubsidioImpuestoVacaciones
        End If

        If ImporteGravadoAguinaldo > 0 Then

            Dim ImporteProporcionalAguinaldo As Decimal

            Call ConsultarDiasMes()

            ImporteProporcionalAguinaldo = (ImporteGravadoAguinaldo / 365) * 30.4
            IngresoMensualOrdinario = (ImporteDiario * DiasMes)
            IngresoGravadoMes = ImporteProporcionalAguinaldo + IngresoMensualOrdinario

            '1 Impuesto / Subsidio proporcional aguinaldo
            Call CalcularImpuestoAguinaldo(IngresoGravadoMes)
            Call CalcularSubsidioAguinaldo(IngresoGravadoMes)

            If ImpuestoAguinaldo > SubsidioAguinaldo Then
                SubsidioEfectivoAguinaldo = 0
                ImpuestoAguinaldo = ImpuestoAguinaldo - SubsidioAguinaldo
            ElseIf ImpuestoAguinaldo < SubsidioAguinaldo Then
                SubsidioEfectivoAguinaldo = SubsidioAguinaldo - ImpuestoAguinaldo
                ImpuestoAguinaldo = 0
            End If

            ImpuestoAguinaldo = Math.Round(ImpuestoAguinaldo, 6)
            SubsidioEfectivoAguinaldo = Math.Round(SubsidioEfectivoAguinaldo, 6)

            If SubsidioEfectivoAguinaldo > 0 And SubsidioEfectivoSueldoMesAnterior > 0 Then
                DiferenciaSubsidioImpuestoAguinaldo = SubsidioEfectivoAguinaldo - SubsidioEfectivoSueldoMesAnterior
                If DiferenciaSubsidioImpuestoAguinaldo < 0 Then
                    DiferenciaSubsidioImpuestoAguinaldo = DiferenciaSubsidioImpuestoAguinaldo * -1
                End If

                FactorImpuestoSubsidioAguinaldo = DiferenciaSubsidioImpuestoAguinaldo / ImporteProporcionalAguinaldo
                TasaSubsidioImpuestoAguinaldo = FactorImpuestoSubsidioAguinaldo * 1 ' El 1 representa 100%
                SubsidioAguinaldo = ImporteGravadoAguinaldo * TasaSubsidioImpuestoAguinaldo
            Else
                DiferenciaSubsidioImpuestoAguinaldo = ImpuestoAguinaldo - ImpuestoSueldoMesAnterior

                FactorImpuestoSubsidioAguinaldo = DiferenciaSubsidioImpuestoAguinaldo / ImporteProporcionalAguinaldo
                TasaSubsidioImpuestoAguinaldo = FactorImpuestoSubsidioAguinaldo * 1 ' El 1 representa 100%
                ImpuestoAguinaldo = ImporteGravadoAguinaldo * TasaSubsidioImpuestoAguinaldo
            End If
        End If

        If ImpuestoVacaciones > 0 Or ImpuestoAguinaldo > 0 Then
            Impuesto = Math.Round(ImpuestoVacaciones + ImpuestoAguinaldo, 6)
        End If

        If SubsidioEfectivoVacaciones > 0 Or SubsidioEfectivoAguinaldo > 0 Then
            SubsidioEfectivo = Math.Round(SubsidioEfectivoVacaciones + SubsidioEfectivoAguinaldo, 6)
        End If

        If Impuesto > 0 Then
            cNomina = New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.CvoConcepto = 86
            cNomina.IdContrato = contratoId.Value
            cNomina.Tipo = "F"
            cNomina.TipoConcepto = "D"
            cNomina.Unidad = 1
            cNomina.Importe = Impuesto
            cNomina.ImporteGravado = 0
            cNomina.ImporteExento = Impuesto
            cNomina.Generado = "S"
            cNomina.Timbrado = ""
            cNomina.Enviado = ""
            cNomina.Situacion = ""
            cNomina.IdMovimiento = Request("id")
            cNomina.GuadarNomina()
        End If
        If SubsidioEfectivo > 0 Then
            cNomina = New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.CvoConcepto = 55
            cNomina.IdContrato = contratoId.Value
            cNomina.Tipo = "F"
            cNomina.TipoConcepto = "P"
            cNomina.Unidad = 1
            cNomina.Importe = SubsidioEfectivo
            cNomina.ImporteGravado = 0
            cNomina.ImporteExento = SubsidioEfectivo
            cNomina.Generado = "S"
            cNomina.Timbrado = ""
            cNomina.Enviado = ""
            cNomina.Situacion = ""
            cNomina.IdMovimiento = Request("id")
            cNomina.GuadarNomina()
        End If

        Call MostrarPercepciones()
        Call MostrarDeducciones()
        Call ChecarPercepcionesExentasYGravadasFiniquito()
        btnAceptar.Visible = False
        btnCancelar.Visible = False
        panelDatos.Visible = True
        cmbPeriodo.Enabled = False
        cmbTipoFiniquito.Enabled = False
    End Sub
    Private Sub GuardarExentoYGravado(ByVal CvoConcepto, ByVal Unidad, ByVal Importe, ByVal ImporteGravado, ByVal ImporteExento, ByVal TipoConcepto)
        Try
            Call CargarVariablesGenerales()

            Dim cNomina = New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.IdContrato = contratoId.Value
            cNomina.CvoConcepto = CvoConcepto
            cNomina.Tipo = "F"
            cNomina.TipoConcepto = TipoConcepto
            cNomina.Unidad = Unidad
            cNomina.Importe = Importe
            cNomina.ImporteGravado = ImporteGravado
            cNomina.ImporteExento = ImporteExento
            cNomina.Generado = "S"
            cNomina.IdMovimiento = Request("id")
            cNomina.GuardarExentoYGravadoFiniquito()

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub cmbTipoFiniquito_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipoFiniquito.SelectedIndexChanged
        If cmbTipoFiniquito.SelectedValue = 0 Then
            lblAntiguedadDias.Text = ""
            lblDiasLaboradosAnio.Text = ""
            lblDiasVacacionesProporcionales.Text = ""
            lblVacacionesProporcionales.Text = ""
            lblPorcentajePrimaVacacional.Text = ""
            lblPrimaVacacionalProporcional.Text = ""
            lblDiasAguinaldoAnio.Text = ""
            lblAguinaldoProporcional.Text = ""
        ElseIf cmbTipoFiniquito.SelectedValue = 1 Then
            Call MostrarDesgloceFiniquito()
        ElseIf cmbTipoFiniquito.SelectedValue = 2 Then

        End If
    End Sub
    Private Sub ConsultarProporcionalesVacaciones()

        Dim DiasPagadosVacaciones As Integer = 0
        Try
            DiasPagadosVacaciones = Convert.ToInt32(txtDiasPagadosVacaciones.Text)
        Catch ex As Exception
            DiasPagadosVacaciones = 0
        End Try

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.Id = Request("id")
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.DiasPagadosVacaciones = DiasPagadosVacaciones
        dt = cNomina.ConsultarDesgloseFiniquito()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                DiasProporcionalVacaciones = Math.Round(oDataRow("DiasProporcionalVacaciones"), 2)
                ProporcionalVacaciones = oDataRow("ProporcionalVacaciones")
            Next
        End If

    End Sub
    Private Sub CalcularImpuesto()
        Call ConsultarCuotaDiaria()
        Call ConsultarDiasMes()

        ImportePeriodo = 0
        ImportePeriodo = CuotaDiaria * DiasMes

        Try
            Impuesto = 0
            ImportePeriodo = ImporteGravadoFiniquito + ImportePeriodo
            Dim dt As New DataTable()
            Dim TarifaMensual As New TarifaMensual()
            TarifaMensual.CuotaFija = ImportePeriodo
            dt = TarifaMensual.ConsultarTarifaMensual()

            If dt.Rows.Count > 0 Then
                Impuesto = ((ImportePeriodo - dt.Rows(0).Item("LimiteInferior")) * (dt.Rows(0).Item("PorcSobreExcli") / 100)) + dt.Rows(0).Item("CuotaFija")
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CalcularSubsidio()

        Call ConsultarCuotaDiaria()
        Call ConsultarDiasMes()

        ImportePeriodo = 0
        ImportePeriodo = CuotaDiaria * DiasMes

        Try

            Subsidio = 0
            ImportePeriodo = ImporteGravadoFiniquito + ImportePeriodo
            Dim dt As New DataTable()
            Dim TablaSubsidioDiario As New TablaSubsidioDiario
            TablaSubsidioDiario.Importe = ImportePeriodo
            dt = TablaSubsidioDiario.ConsultarSubsidioMensual()

            If dt.Rows.Count > 0 Then
                Subsidio = dt.Rows(0).Item("Subsidio")
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CalcularImpuestoVacaciones(ByVal Importe As Decimal)
        Try
            ImpuestoVacaciones = 0
            Dim dt As New DataTable()
            Dim TarifaMensual As New TarifaMensual()
            TarifaMensual.CuotaFija = Importe
            dt = TarifaMensual.ConsultarTarifaMensual()

            If dt.Rows.Count > 0 Then
                ImpuestoVacaciones = ((Importe - dt.Rows(0).Item("LimiteInferior")) * (dt.Rows(0).Item("PorcSobreExcli") / 100)) + dt.Rows(0).Item("CuotaFija")
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CalcularSubsidioVacaciones(ByVal Importe As Decimal)
        Try
            SubsidioVacaciones = 0
            Dim dt As New DataTable()
            Dim TablaSubsidioDiario As New TablaSubsidioDiario
            TablaSubsidioDiario.Importe = Importe
            dt = TablaSubsidioDiario.ConsultarSubsidioMensual()

            If dt.Rows.Count > 0 Then
                SubsidioVacaciones = dt.Rows(0).Item("Subsidio")
            End If

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CalcularImpuestoSueldoMesAnterior(ByVal Importe As Decimal)
        Try
            ImpuestoSueldoMesAnterior = 0
            Dim dt As New DataTable()
            Dim TarifaMensual As New TarifaMensual()
            TarifaMensual.CuotaFija = Importe
            dt = TarifaMensual.ConsultarTarifaMensual()

            If dt.Rows.Count > 0 Then
                ImpuestoSueldoMesAnterior = ((Importe - dt.Rows(0).Item("LimiteInferior")) * (dt.Rows(0).Item("PorcSobreExcli") / 100)) + dt.Rows(0).Item("CuotaFija")
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CalcularSubsidioSueldoMesAnterior(ByVal Importe As Decimal)
        Try
            SubsidioSueldoMesAnterior = 0
            Dim dt As New DataTable()
            Dim TablaSubsidioDiario As New TablaSubsidioDiario
            TablaSubsidioDiario.Importe = Importe
            dt = TablaSubsidioDiario.ConsultarSubsidioMensual()

            If dt.Rows.Count > 0 Then
                SubsidioSueldoMesAnterior = dt.Rows(0).Item("Subsidio")
            End If

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CalcularImpuestoSueldosPagados(ByVal valida As Integer)

        Call ConsultarCuotaDiaria()
        Call ConsultarDiasMes()

        ImportePeriodo = 0
        ImportePeriodo = CuotaDiaria * DiasMes
        'If valida = 0 Then
        '    ImportePeriodo = ImportePeriodo + ImporteGravadoFiniquito
        'Else
        '    ImportePeriodo = ImportePeriodo + ImporteGravado - ImporteGravadoFiniquito
        'End If
        If valida = 1 Then
            ImportePeriodo = ImportePeriodo + ImporteGravado - ImporteGravadoFiniquito
        End If

        'Try
        '    ImpuestoSueldoPagado = 0
        '    Dim dt As New DataTable()
        '    Dim TarifaMensual As New TarifaMensual()
        '    TarifaMensual.CuotaFija = ImportePeriodo
        '    dt = TarifaMensual.ConsultarTarifaMensual()

        '    If dt.Rows.Count > 0 Then
        '        ImpuestoSueldoPagado = ((ImportePeriodo - dt.Rows(0).Item("LimiteInferior")) * (dt.Rows(0).Item("PorcSobreExcli") / 100)) + dt.Rows(0).Item("CuotaFija")
        '    End If
        'Catch oExcep As Exception
        '    rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        'End Try
    End Sub
    Private Sub CalcularSubsidioSueldoPagados(ByVal valida As Integer)

        Call ConsultarCuotaDiaria()
        Call ConsultarDiasMes()

        ImportePeriodo = 0
        ImportePeriodo = CuotaDiaria * DiasMes
        If valida = 0 Then
            ImportePeriodo = ImportePeriodo + ImporteGravadoFiniquito
        Else
            ImportePeriodo = ImportePeriodo + ImporteGravado - ImporteGravadoFiniquito
        End If

        'Try

        '    SubsidioSueldoPagado = 0
        '    Dim dt As New DataTable()
        '    Dim TablaSubsidioDiario As New TablaSubsidioDiario
        '    TablaSubsidioDiario.Importe = ImportePeriodo
        '    dt = TablaSubsidioDiario.ConsultarSubsidioMensual()

        '    If dt.Rows.Count > 0 Then
        '        SubsidioSueldoPagado = dt.Rows(0).Item("Subsidio")
        '    End If
        'Catch oExcep As Exception
        '    rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        'End Try
    End Sub
    Private Sub CalcularImpuestoAguinaldo(ByVal Importe As Decimal)
        Try
            ImpuestoAguinaldo = 0
            Dim dt As New DataTable()
            Dim TarifaMensual As New TarifaMensual()
            TarifaMensual.CuotaFija = Importe
            dt = TarifaMensual.ConsultarTarifaMensual()

            If dt.Rows.Count > 0 Then
                ImpuestoAguinaldo = ((Importe - dt.Rows(0).Item("LimiteInferior")) * (dt.Rows(0).Item("PorcSobreExcli") / 100)) + dt.Rows(0).Item("CuotaFija")
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CalcularSubsidioAguinaldo(ByVal Importe As Decimal)
        Try
            SubsidioAguinaldo = 0
            Dim dt As New DataTable()
            Dim TablaSubsidioDiario As New TablaSubsidioDiario
            TablaSubsidioDiario.Importe = Importe
            dt = TablaSubsidioDiario.ConsultarSubsidioMensual()

            If dt.Rows.Count > 0 Then
                SubsidioAguinaldo = dt.Rows(0).Item("Subsidio")
            End If

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CalcularImpuestoIncidencias(ByVal Importe As Decimal)
        Try
            ImpuestoIncidencias = 0
            Dim dt As New DataTable()
            Dim TarifaDiaria As New TarifaDiaria()
            TarifaDiaria.ImporteDiario = Importe
            dt = TarifaDiaria.ConsultarValoresTarifaDiaria()

            If dt.Rows.Count > 0 Then
                ImpuestoIncidencias = ((Importe - dt.Rows(0).Item("LimiteInferior")) * (dt.Rows(0).Item("PorcSobreExcli") / 100)) + dt.Rows(0).Item("CuotaFija")
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CalcularSubsidioIncidencias(ByVal Importe As Decimal)
        Try
            SubsidioIncidencias = 0
            Dim dt As New DataTable()
            Dim TablaSubsidioDiario As New TablaSubsidioDiario()
            TablaSubsidioDiario.Importe = Importe
            dt = TablaSubsidioDiario.ConsultarSubsidioDiario()

            If dt.Rows.Count > 0 Then
                SubsidioIncidencias = dt.Rows(0).Item("Subsidio")
            End If

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CalcularImss()
        Imss = 0
        Call ConsultarSalarioDiarioIntegradoTrabajador()

        If ImporteDiario <= SalarioMinimoDiarioGeneral Then
            Imss = 0
        ElseIf ImporteDiario > SalarioMinimoDiarioGeneral And ImporteDiario < (SalarioMinimoDiarioGeneral * 3) Then
            Imss = SalarioDiarioIntegradoTrabajador * 0.02375
        ElseIf ImporteDiario > (SalarioMinimoDiarioGeneral * 3) And ImporteDiario < (SalarioMinimoDiarioGeneral * 25) Then
            Imss = SalarioDiarioIntegradoTrabajador * 0.02775
            'Imss = Imss + ((SalarioDiarioIntegradoTrabajador - (SalarioMinimoDiarioGeneral * 3)) * 0.004)
        ElseIf ImporteDiario > (SalarioMinimoDiarioGeneral * 25) Then
            Imss = (SalarioDiarioIntegradoTrabajador - (SalarioMinimoDiarioGeneral * 25)) * 0.02375
            Imss = Imss + ((SalarioDiarioIntegradoTrabajador - (SalarioMinimoDiarioGeneral * 22)) * 0.004)
        End If
    End Sub
    Private Sub GridPercepciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridPercepciones.NeedDataSource

        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.Tipo = "F"
        cNomina.TipoConcepto = "P"
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.IdMovimiento = Request("id")
        dt = cNomina.ConsultarPercepcionesDeduccionesFiniquito()
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
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.Tipo = "F"
        cNomina.TipoConcepto = "D"
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.IdMovimiento = Request("id")
        dt = cNomina.ConsultarPercepcionesDeduccionesFiniquito()
        cNomina = Nothing
        GridDeduciones.DataSource = dt

        If dt.Rows.Count > 0 Then
            txtDeducciones.Text = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
        End If

    End Sub
    Private Sub MostrarPercepciones()
        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.Tipo = "F"
        cNomina.TipoConcepto = "P"
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.IdMovimiento = Request("id")
        dt = cNomina.ConsultarPercepcionesDeduccionesFiniquito()
        cNomina = Nothing
        GridPercepciones.DataSource = dt
        GridPercepciones.DataBind()

        If dt.Rows.Count > 0 Then
            If dt.Compute("Sum(Importe)", "") IsNot DBNull.Value Then
                TotalPercepciones = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
                PercepcionesGravadas = dt.Compute("Sum(ImporteGravado)", "")
                PercepcionesExentas = dt.Compute("Sum(ImporteExento)", "")
            Else
                TotalPercepciones = 0
            End If
            txtPercepciones.Text = Math.Round(TotalPercepciones, 6)
        End If
    End Sub
    Private Sub MostrarDeducciones()
        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.Tipo = "F"
        cNomina.TipoConcepto = "D"
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.IdMovimiento = Request("id")
        dt = cNomina.ConsultarPercepcionesDeduccionesFiniquito()
        cNomina = Nothing
        GridDeduciones.DataSource = dt
        GridDeduciones.DataBind()

        If dt.Rows.Count > 0 Then
            If dt.Compute("Sum(Importe)", "") IsNot DBNull.Value Then
                TotalDeducciones = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
            Else
                TotalDeducciones = 0
            End If
            txtDeducciones.Text = Math.Round(TotalDeducciones, 6)
        End If
    End Sub
    Private Sub ChecarPercepcionesExentasYGravadasFiniquito()
        Try
            Dim Percepciones, Deducciones As Decimal
            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Tipo = "F"
            cNomina.TipoConcepto = "P"
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.IdMovimiento = Request("id")
            dt = cNomina.ConsultarConceptosFiniquito()

            If dt.Rows.Count > 0 Then
                Percepciones = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
                PercepcionesGravadas = dt.Compute("Sum(ImporteGravado)", "")
                PercepcionesExentas = dt.Compute("Sum(ImporteExento)", "")
            End If

            txtGravadoISR.Text = PercepcionesGravadas
            txtExentoISR.Text = PercepcionesExentas

            dt = New DataTable()
            cNomina = New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Tipo = "F"
            cNomina.TipoConcepto = "D"
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.IdMovimiento = Request("id")
            dt = cNomina.ConsultarConceptosFiniquito()

            If dt.Rows.Count > 0 Then
                Deducciones = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
            End If

            txtNetoAPagar.Text = Percepciones - Deducciones

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Public Sub EliminaConceptosFiniquito(ByVal IdMovimiento As Int64, ByVal CvoConcepto As Int32)
        Call CargarVariablesGenerales()
        Try
            Dim cNomina As New Nomina()
            cNomina.Tipo = "F"
            cNomina.IdMovimiento = IdMovimiento
            cNomina.NoEmpleado = empleadoId.Value
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            If CvoConcepto > 0 Then
                cNomina.CvoConcepto = CvoConcepto
            End If
            cNomina.EliminaConceptosFiniquito()

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

        Call ConsultarCuotaDiaria()

        If cmbConcepto.SelectedValue.ToString = "6" And txtUnidadIncidencia.Text.Trim <> "" Then
            txtImporteIncidencia.Text = ((CuotaDiaria / 8) * 2) * txtUnidadIncidencia.Text
        ElseIf cmbConcepto.SelectedValue.ToString = "2" And txtUnidadIncidencia.Text.Trim <> "" Then
            txtImporteIncidencia.Text = CuotaDiaria * txtUnidadIncidencia.Text
        ElseIf cmbConcepto.SelectedValue.ToString = "3" And txtUnidadIncidencia.Text.Trim <> "" Then
            'txtImporteIncidencia.Text = oDataset.Tables("NominaEmpleados").Rows(iPosicFilaActual).Item("FACTORCOMISION") * txtUnidadIncidencia.Text
        ElseIf cmbConcepto.SelectedValue.ToString = "4" And txtUnidadIncidencia.Text.Trim <> "" Then
            txtImporteIncidencia.Text = FactorDestajo * txtUnidadIncidencia.Text
        ElseIf cmbConcepto.SelectedValue.ToString = "5" And txtUnidadIncidencia.Text.Trim <> "" Then
            txtImporteIncidencia.Text = AsimiladoTotalSemanal * txtUnidadIncidencia.Text
        ElseIf cmbConcepto.SelectedValue.ToString = "9" And txtUnidadIncidencia.Text.Trim <> "" Then
            txtImporteIncidencia.Text = (CuotaDiaria * 2) * txtUnidadIncidencia.Text
        ElseIf cmbConcepto.SelectedValue.ToString = "10" And txtUnidadIncidencia.Text.Trim <> "" Then
            txtImporteIncidencia.Text = (CuotaDiaria * 2) * txtUnidadIncidencia.Text
        ElseIf cmbConcepto.SelectedValue.ToString = "11" And txtUnidadIncidencia.Text.Trim <> "" Then
            txtImporteIncidencia.Text = PagoPorHora * txtUnidadIncidencia.Text
        ElseIf cmbConcepto.SelectedValue.ToString = "8" And txtUnidadIncidencia.Text.Trim <> "" Then
            txtImporteIncidencia.Text = (CuotaDiaria * 2) * txtUnidadIncidencia.Text
        ElseIf cmbConcepto.SelectedValue.ToString = "7" And txtUnidadIncidencia.Text.Trim <> "" Then
            txtImporteIncidencia.Text = ((CuotaDiaria / 8) * 3) * txtUnidadIncidencia.Text
        ElseIf cmbConcepto.SelectedValue.ToString = "13" And txtUnidadIncidencia.Text.Trim <> "" Then
            txtImporteIncidencia.Text = (CuotaDiaria / 4) * txtUnidadIncidencia.Text
        ElseIf cmbConcepto.SelectedValue.ToString = "14" And txtUnidadIncidencia.Text.Trim <> "" Then
            txtImporteIncidencia.Text = CuotaDiaria * txtUnidadIncidencia.Text
        ElseIf cmbConcepto.SelectedValue.ToString = "16" And txtUnidadIncidencia.Text.Trim <> "" Then
            txtImporteIncidencia.Text = (CuotaDiaria * txtUnidadIncidencia.Text) / 4
        ElseIf cmbConcepto.SelectedValue.ToString = "15" And txtUnidadIncidencia.Text.Trim <> "" Then
            txtImporteIncidencia.Text = CuotaDiaria * txtUnidadIncidencia.Text
        ElseIf cmbConcepto.SelectedValue.ToString = "51" And txtUnidadIncidencia.Text.Trim <> "" Then
            txtImporteIncidencia.Text = CuotaDiaria * txtUnidadIncidencia.Text
        ElseIf cmbConcepto.SelectedValue.ToString = "57" And txtUnidadIncidencia.Text.Trim <> "" Then
            txtImporteIncidencia.Text = CuotaDiaria * txtUnidadIncidencia.Text
        End If
    End Sub
    Private Sub rdoPercepcion_CheckedChanged(sender As Object, e As EventArgs) Handles rdoPercepcion.CheckedChanged
        cmbConcepto.Items.Clear()
        cmbConcepto.Items.Add(New ListItem("--Seleccione--", 0))
        cmbConcepto.Items.Add(New ListItem("DIAS PENDIENTES DE PAGO", 51)) '51
        cmbConcepto.Items.Add(New ListItem("GRATIFICACIÓN", 17)) '51
        cmbConcepto.DataBind()
    End Sub
    Private Sub rdoDeducción_CheckedChanged(sender As Object, e As EventArgs) Handles rdoDeducción.CheckedChanged
        LlenaCmbConcepto(0, "D")
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

            If Importe <= 0 Then
                rwAlerta.RadAlert("Favor de digitar un importe!!", 330, 180, "Alerta", "", "")
                Exit Sub
            ElseIf Unidad <= 0 Then
                rwAlerta.RadAlert("Favor de digitar un unidad!!", 330, 180, "Alerta", "", "")
                Exit Sub
            End If

            If rdoDeducción.Checked Then
                Call MostrarPercepciones()
                Call MostrarDeducciones()
                NetoAPagar = TotalPercepciones - TotalDeducciones

                If Importe > NetoAPagar Then
                    rwAlerta.RadAlert("El importe máximo para otras deducciones es: " & FormatCurrency(NetoAPagar, 2).ToString, 330, 180, "Alerta", "", "")
                    Exit Sub
                End If
            End If

            Dim ClaveRegimenContratacion As Integer = 0

            Dim cEmpleado As New Entities.Empleado
            cEmpleado.IdEmpleado = empleadoId.Value
            cEmpleado.ConsultarEmpleadoID()
            If cEmpleado.IdEmpleado > 0 Then
                ClaveRegimenContratacion = cEmpleado.IdRegimenContratacion
            End If

            If cmbConcepto.SelectedValue.ToString = "5" And ClaveRegimenContratacion <> 9 Then
                rwAlerta.RadAlert("El régimen de contratacion de este trabajador no es honorarios asimilado a salarios!!", 330, 180, "Alerta", "", "")
                Exit Sub
            ElseIf cmbConcepto.SelectedValue.ToString < 52 Or cmbConcepto.SelectedValue.ToString = "57" Or cmbConcepto.SelectedValue.ToString = "58" Or cmbConcepto.SelectedValue.ToString = "59" Or cmbConcepto.SelectedValue.ToString = "161" Or cmbConcepto.SelectedValue.ToString = "162" Then
                If ClaveRegimenContratacion = 9 Then
                    rwAlerta.RadAlert("El régimen de contratación de este trabajador es asimilado a salarios, por lo mismo no se le debe agregar ningún otro tipo de percepción ni hacerle deducciones por faltas, dichas deducciones solo pueden ser por un aduedo distinto!!", 330, 180, "Alerta", "", "")
                    Exit Sub
                End If
            End If

            If cmbConcepto.SelectedValue.ToString = "57" Or cmbConcepto.SelectedValue.ToString = "58" Or cmbConcepto.SelectedValue.ToString = "59" Or cmbConcepto.SelectedValue.ToString = "161" Or cmbConcepto.SelectedValue.ToString = "162" Then
                rwAlerta.RadAlert("Este concepto no se permite agregar en cálculo de finiquito.", 330, 180, "Alerta", "", "")
                Exit Sub
            End If

            If cmbConcepto.SelectedValue.ToString = "6" Then
                If txtUnidadIncidencia.Text > 9 Then
                    rwAlerta.RadAlert("Las horas extras no pueden ser mas de 9!!", 330, 180, "Alerta", "", "")
                    Exit Sub
                End If
            End If

            If ChecarSiExiste(cmbConcepto.SelectedValue) = True Then
                rwAlerta.RadAlert("Esa percepcion/deduccion ya existe!!", 330, 180, "Alerta", "", "")
                Exit Sub
            End If
            If ChecarQueExistaPercepcionesFiniquito() = False Then
                rwAlerta.RadAlert("No existen percepciones(AGUINALDO, VACACIONES, PRIMA VACACIONAL) o el importe del mismo es menor al importe de la deduccion o los dias a descontar son menores a los existentes, no se puede agregar esta deduccion!!", 330, 180, "Alerta", "", "")
                Exit Sub
            End If

            ImporteDiario = 0
            ImportePeriodo = 0
            ImporteExento = 0
            ImporteGravado = 0
            ImporteGravado = 0
            SubsidioEfectivo = 0
            ImpuestoVacaciones = 0
            SubsidioVacaciones = 0
            SubsidioEfectivoVacaciones = 0

            ImpuestoSueldoMesAnterior = 0
            SubsidioSueldoMesAnterior = 0
            SubsidioEfectivoSueldoMesAnterior = 0

            SubsidioPercepcionesEspecial = 0
            ImpuestoPercepcionesEspecial = 0
            SubsidioPorPagarSueldos = 0
            ImpuestoPorPagarSueldos = 0
            Agregar = 1

            If cmbConcepto.SelectedValue.ToString < 52 Then
                'En este bloque no entran las deducciones por adeudos que no cambian la base del impuesto y por lo tanto no lo calculan, ejemplos de esto serian, adeudos por credito infonavit, cuotas sindicales, adeudos fonacot, adeudos con el patron, etc, (conceptos del 61 al 86)
                Call BorrarDeduccionesFiniquito()

                Dim Aguinaldo As Decimal = 0
                Dim Vacaciones As Decimal = 0
                Dim DiasProporcionalVacaciones As Decimal = 0
                Dim PrimaVacacional As Decimal = 0
                Dim ImporteExentoAguinaldo As Decimal = 0
                Dim ImporteGravadoAguinaldo As Decimal = 0
                Dim ImporteExentoPrimaVacacional As Decimal = 0
                Dim ImporteGravadoPrimaVacacional As Decimal = 0

                Dim DiferenciaSubsidioImpuestoVacaciones As Decimal = 0
                Dim FactorImpuestoSubsidioVacaciones As Decimal = 0
                Dim TasaSubsidioImpuestoVacaciones As Decimal = 0

                Dim DiferenciaSubsidioImpuestoAguinaldo As Decimal = 0
                Dim FactorImpuestoSubsidioAguinaldo As Decimal = 0
                Dim TasaSubsidioImpuestoAguinaldo As Decimal = 0

                Dim DiferenciaSubsidioImpuestoIncidencias As Decimal = 0
                Dim FactorImpuestoSubsidioIncidencias As Decimal = 0
                Dim TasaSubsidioImpuestoIncidencias As Decimal = 0

                Dim DiasPagadosVacaciones As Integer = 0
                Try
                    DiasPagadosVacaciones = Convert.ToInt32(txtDiasPagadosVacaciones.Text)
                Catch ex As Exception
                    DiasPagadosVacaciones = 0
                End Try

                Dim dt As New DataTable()
                Dim cNomina As New Nomina()
                cNomina.Id = Request("id")
                'cNomina.IdEmpresa = Session("clienteid")
                cNomina.TipoNomina = 3 'Quincenal
                cNomina.DiasPagadosVacaciones = DiasPagadosVacaciones
                dt = cNomina.ConsultarDesgloseFiniquito()
                cNomina = Nothing

                If dt.Rows.Count > 0 Then
                    For Each oDataRow In dt.Rows
                        ImporteDiario = oDataRow("SueldoDiario")
                        Aguinaldo = oDataRow("ProporcionalAguinaldo")
                        Vacaciones = oDataRow("ProporcionalVacaciones")
                        PrimaVacacional = oDataRow("PrimaVacaciones")
                        DiasProporcionalVacaciones = Math.Round(oDataRow("DiasProporcionalVacaciones"), 2)
                    Next
                End If

                If Aguinaldo > 0 Then
                    If Aguinaldo > 0 And Aguinaldo < (SalarioMinimoDiarioGeneral * 30) Then
                        ImporteExento = ImporteExento + Aguinaldo
                        ImporteExentoAguinaldo = Aguinaldo
                        ImporteGravadoAguinaldo = 0
                    ElseIf Aguinaldo > 0 And Aguinaldo > (SalarioMinimoDiarioGeneral * 30) Then
                        ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 30)
                        ImporteGravadoFiniquito = ImporteGravadoFiniquito + (Aguinaldo - (SalarioMinimoDiarioGeneral * 30))
                        ImporteExentoAguinaldo = SalarioMinimoDiarioGeneral * 30
                        ImporteGravadoAguinaldo = Aguinaldo - (SalarioMinimoDiarioGeneral * 30)
                    End If
                End If

                If Vacaciones > 0 Then
                    ImporteGravadoVacaciones = Vacaciones
                    ImporteGravadoFiniquito = ImporteGravadoFiniquito + Vacaciones
                End If

                If PrimaVacacional > 0 Then
                    If PrimaVacacional > 0 And PrimaVacacional < (SalarioMinimoDiarioGeneral * 15) Then
                        ImporteExento = ImporteExento + PrimaVacacional
                        ImporteExentoPrimaVacacional = PrimaVacacional
                        ImporteGravadoPrimaVacacional = 0
                    ElseIf PrimaVacacional > 0 And PrimaVacacional > (SalarioMinimoDiarioGeneral * 15) Then
                        ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 15)
                        ImporteGravadoFiniquito = ImporteGravadoFiniquito + (PrimaVacacional - (SalarioMinimoDiarioGeneral * 15))
                        ImporteExentoPrimaVacacional = SalarioMinimoDiarioGeneral * 15
                        ImporteGravadoPrimaVacacional = PrimaVacacional - (SalarioMinimoDiarioGeneral * 15)
                    End If
                End If

                '/*-------------------------*/
                '1 Impuesto / Subsidio proporcional vacaciones
                Dim ImporteProporcionalVacaciones As Decimal
                Dim IngresoMensualOrdinario As Decimal
                Dim IngresoGravadoMes As Decimal

                Call ConsultarDiasMes()

                ImporteProporcionalVacaciones = (ImporteGravadoVacaciones / 365) * 30.4
                IngresoMensualOrdinario = (ImporteDiario * DiasMes)
                IngresoGravadoMes = ImporteProporcionalVacaciones + IngresoMensualOrdinario

                Call CalcularImpuestoVacaciones(IngresoGravadoMes)
                Call CalcularSubsidioVacaciones(IngresoGravadoMes)

                If ImpuestoVacaciones > SubsidioVacaciones Then
                    SubsidioEfectivoVacaciones = 0
                    ImpuestoVacaciones = ImpuestoVacaciones - SubsidioVacaciones
                ElseIf ImpuestoVacaciones < SubsidioVacaciones Then
                    SubsidioEfectivoVacaciones = SubsidioVacaciones - ImpuestoVacaciones
                    ImpuestoVacaciones = 0
                End If

                ImpuestoVacaciones = Math.Round(ImpuestoVacaciones, 6)
                SubsidioEfectivoVacaciones = Math.Round(SubsidioEfectivoVacaciones, 6)

                '2 Impuesto / Subsidio por sueldo ordinario
                Call CalcularImpuestoSueldoMesAnterior(IngresoMensualOrdinario)
                Call CalcularSubsidioSueldoMesAnterior(IngresoMensualOrdinario)

                If ImpuestoSueldoMesAnterior > SubsidioSueldoMesAnterior Then
                    SubsidioEfectivoSueldoMesAnterior = 0
                    ImpuestoSueldoMesAnterior = ImpuestoSueldoMesAnterior - SubsidioSueldoMesAnterior
                ElseIf ImpuestoSueldoMesAnterior < SubsidioSueldoMesAnterior Then
                    SubsidioEfectivoSueldoMesAnterior = SubsidioSueldoMesAnterior - ImpuestoSueldoMesAnterior
                    ImpuestoSueldoMesAnterior = 0
                End If

                ImpuestoSueldoMesAnterior = Math.Round(ImpuestoSueldoMesAnterior, 6)
                SubsidioEfectivoSueldoMesAnterior = Math.Round(SubsidioEfectivoSueldoMesAnterior, 6)

                If SubsidioEfectivoVacaciones > 0 And SubsidioEfectivoSueldoMesAnterior > 0 Then

                    DiferenciaSubsidioImpuestoVacaciones = SubsidioEfectivoVacaciones - SubsidioEfectivoSueldoMesAnterior

                    If DiferenciaSubsidioImpuestoVacaciones < 0 Then
                        DiferenciaSubsidioImpuestoVacaciones = DiferenciaSubsidioImpuestoVacaciones * -1
                    End If

                    FactorImpuestoSubsidioVacaciones = DiferenciaSubsidioImpuestoVacaciones / ImporteProporcionalVacaciones
                    TasaSubsidioImpuestoVacaciones = FactorImpuestoSubsidioVacaciones * 1 ' El 1 representa 100%
                    SubsidioVacaciones = ImporteGravadoVacaciones * TasaSubsidioImpuestoVacaciones
                Else
                    DiferenciaSubsidioImpuestoVacaciones = ImpuestoVacaciones - ImpuestoSueldoMesAnterior

                    FactorImpuestoSubsidioVacaciones = DiferenciaSubsidioImpuestoVacaciones / ImporteProporcionalVacaciones
                    TasaSubsidioImpuestoVacaciones = FactorImpuestoSubsidioVacaciones * 1 ' El 1 representa 100%
                    ImpuestoVacaciones = ImporteGravadoVacaciones * TasaSubsidioImpuestoVacaciones
                End If

                '/*------------------ Cambios 29/01/2018 ------------------*/
                'If ImporteGravadoAguinaldo > 0 Then

                '    Dim ImporteProporcionalAguinaldo As Decimal

                '    Call ConsultarDiasMes()

                '    ImporteProporcionalAguinaldo = (ImporteGravadoAguinaldo / 365) * 30.4
                '    IngresoMensualOrdinario = (ImporteDiario * DiasMes)
                '    IngresoGravadoMes = ImporteProporcionalAguinaldo + IngresoMensualOrdinario

                '    '1 Impuesto / Subsidio proporcional aguinaldo
                '    Call CalcularImpuestoAguinaldo(IngresoGravadoMes)
                '    Call CalcularSubsidioAguinaldo(IngresoGravadoMes)

                '    If ImpuestoAguinaldo > SubsidioIncidencias Then
                '        SubsidioEfectivoAguinaldo = 0
                '        ImpuestoAguinaldo = ImpuestoAguinaldo - SubsidioIncidencias
                '    ElseIf ImpuestoAguinaldo < SubsidioIncidencias Then
                '        SubsidioEfectivoAguinaldo = SubsidioIncidencias - ImpuestoAguinaldo
                '        ImpuestoAguinaldo = 0
                '    End If

                '    ImpuestoAguinaldo = Math.Round(ImpuestoAguinaldo, 6)
                '    SubsidioEfectivoAguinaldo = Math.Round(SubsidioEfectivoAguinaldo, 6)

                '    If SubsidioEfectivoAguinaldo > 0 And SubsidioEfectivoSueldoMesAnterior > 0 Then
                '        DiferenciaSubsidioImpuestoAguinaldo = SubsidioEfectivoAguinaldo - SubsidioEfectivoSueldoMesAnterior
                '        If DiferenciaSubsidioImpuestoAguinaldo < 0 Then
                '            DiferenciaSubsidioImpuestoAguinaldo = DiferenciaSubsidioImpuestoAguinaldo * -1
                '        End If

                '        FactorImpuestoSubsidioAguinaldo = DiferenciaSubsidioImpuestoAguinaldo / ImporteProporcionalAguinaldo
                '        TasaSubsidioImpuestoAguinaldo = FactorImpuestoSubsidioAguinaldo * 1 ' El 1 representa 100%
                '        SubsidioIncidencias = ImporteGravadoAguinaldo * TasaSubsidioImpuestoAguinaldo
                '    Else
                '        DiferenciaSubsidioImpuestoAguinaldo = ImpuestoAguinaldo - ImpuestoSueldoMesAnterior

                '        FactorImpuestoSubsidioAguinaldo = DiferenciaSubsidioImpuestoAguinaldo / ImporteProporcionalAguinaldo
                '        TasaSubsidioImpuestoAguinaldo = FactorImpuestoSubsidioAguinaldo * 1 ' El 1 representa 100%
                '        ImpuestoAguinaldo = ImporteGravadoAguinaldo * TasaSubsidioImpuestoAguinaldo
                '    End If
                'End If
                '/*-------------------------*/

                '/*------------------ Reemplazo 29/01/2018 ------------------*/
                If ImporteGravadoAguinaldo > 0 Then

                    Dim ImporteProporcionalAguinaldo As Decimal

                    Call ConsultarDiasMes()

                    ImporteProporcionalAguinaldo = (ImporteGravadoAguinaldo / 365) * 30.4
                    IngresoMensualOrdinario = (ImporteDiario * DiasMes)
                    IngresoGravadoMes = ImporteProporcionalAguinaldo + IngresoMensualOrdinario

                    '1 Impuesto / Subsidio proporcional aguinaldo
                    Call CalcularImpuestoAguinaldo(IngresoGravadoMes)
                    Call CalcularSubsidioAguinaldo(IngresoGravadoMes)

                    If ImpuestoAguinaldo > SubsidioAguinaldo Then
                        SubsidioEfectivoAguinaldo = 0
                        ImpuestoAguinaldo = ImpuestoAguinaldo - SubsidioAguinaldo
                    ElseIf ImpuestoAguinaldo < SubsidioAguinaldo Then
                        SubsidioEfectivoAguinaldo = SubsidioAguinaldo - ImpuestoAguinaldo
                        ImpuestoAguinaldo = 0
                    End If

                    ImpuestoAguinaldo = Math.Round(ImpuestoAguinaldo, 6)
                    SubsidioEfectivoAguinaldo = Math.Round(SubsidioEfectivoAguinaldo, 6)

                    If SubsidioEfectivoAguinaldo > 0 And SubsidioEfectivoSueldoMesAnterior > 0 Then
                        DiferenciaSubsidioImpuestoAguinaldo = SubsidioEfectivoAguinaldo - SubsidioEfectivoSueldoMesAnterior
                        If DiferenciaSubsidioImpuestoAguinaldo < 0 Then
                            DiferenciaSubsidioImpuestoAguinaldo = DiferenciaSubsidioImpuestoAguinaldo * -1
                        End If

                        FactorImpuestoSubsidioAguinaldo = DiferenciaSubsidioImpuestoAguinaldo / ImporteProporcionalAguinaldo
                        TasaSubsidioImpuestoAguinaldo = FactorImpuestoSubsidioAguinaldo * 1 ' El 1 representa 100%
                        SubsidioAguinaldo = ImporteGravadoAguinaldo * TasaSubsidioImpuestoAguinaldo
                    Else
                        DiferenciaSubsidioImpuestoAguinaldo = ImpuestoAguinaldo - ImpuestoSueldoMesAnterior

                        FactorImpuestoSubsidioAguinaldo = DiferenciaSubsidioImpuestoAguinaldo / ImporteProporcionalAguinaldo
                        TasaSubsidioImpuestoAguinaldo = FactorImpuestoSubsidioAguinaldo * 1 ' El 1 representa 100%
                        ImpuestoAguinaldo = ImporteGravadoAguinaldo * TasaSubsidioImpuestoAguinaldo
                    End If
                End If
                '/*-------------------------*/

                If ImpuestoVacaciones > 0 Or ImpuestoAguinaldo > 0 Then
                    Impuesto = Math.Round(ImpuestoVacaciones + ImpuestoAguinaldo, 6)
                End If

                If SubsidioEfectivoVacaciones > 0 Or SubsidioEfectivoAguinaldo > 0 Then
                    SubsidioEfectivo = Math.Round(SubsidioEfectivoVacaciones + SubsidioEfectivoAguinaldo, 6)
                End If

                Call ChecarPercepcionesGravadas(cmbConcepto.SelectedValue)

                If ImporteGravado > 0 Then

                    Dim ImporteImpuestoIncidencias As Decimal = 0
                    Dim ImporteSubsidioIncidencias As Decimal = 0
                    Dim ImporteDiaspendientes As Decimal = ImporteGravado

                    '1 Impuesto / Subsidio Dias Pendientes
                    Call CalcularImpuestoIncidencias(ImporteDiario)
                    Call CalcularSubsidioIncidencias(ImporteDiario)

                    ImporteImpuestoIncidencias = Math.Round(ImpuestoIncidencias, 6) * DiasCuotaPeriodo
                    ImporteSubsidioIncidencias = Math.Round(SubsidioIncidencias, 6) * DiasCuotaPeriodo

                    If ImporteImpuestoIncidencias > ImporteSubsidioIncidencias Then
                        ImpuestoIncidencias = ImporteImpuestoIncidencias - ImporteSubsidioIncidencias
                        SubsidioIncidencias = 0
                    ElseIf ImporteImpuestoIncidencias < ImporteSubsidioIncidencias Then
                        SubsidioIncidencias = ImporteSubsidioIncidencias - ImporteImpuestoIncidencias
                        ImpuestoIncidencias = 0
                    End If

                    '1 Impuesto / Subsidio proporcional incidencias
                    'Dim ImporteProporcionalIncidencias As Decimal
                    'Dim ImporteSubsidioIncidencias As Decimal = 0
                    'Dim ImporteImpuestoIncidencias As Decimal = 0

                    'Dim ImporteSubsidioIncidenciasOrdinario As Decimal = 0
                    'Dim ImporteImpuestoIncidenciasOrdinario As Decimal = 0

                    'Dim DiferenciaimpuestoSubsidio As Decimal = 0

                    'Call ConsultarDiasMes()

                    'ImporteProporcionalIncidencias = (ImporteGravado / 365) * 30.4
                    'IngresoMensualOrdinario = (ImporteDiario * DiasMes)
                    'IngresoGravadoMes = ImporteProporcionalIncidencias + IngresoMensualOrdinario

                    ''1 Impuesto / Subsidio Gravado mes
                    'Call CalcularImpuestoIncidencias(IngresoGravadoMes)
                    'Call CalcularSubsidioIncidencias(IngresoGravadoMes)

                    'If ImpuestoIncidencias > SubsidioIncidencias Then
                    '    SubsidioEfectivoIncidencias = 0
                    '    ImpuestoIncidencias = ImpuestoIncidencias - SubsidioIncidencias
                    'ElseIf ImpuestoIncidencias < SubsidioIncidencias Then
                    '    SubsidioEfectivoIncidencias = SubsidioIncidencias - ImpuestoIncidencias
                    '    ImpuestoIncidencias = 0
                    'End If

                    'ImporteImpuestoIncidencias = Math.Round(ImpuestoIncidencias, 6)
                    'ImporteSubsidioIncidencias = Math.Round(SubsidioEfectivoIncidencias, 6)

                    ''2 Impuesto / Subsidio Mensual Ordinario
                    'Call CalcularImpuestoIncidencias(IngresoMensualOrdinario)
                    'Call CalcularSubsidioIncidencias(IngresoMensualOrdinario)

                    'If ImpuestoIncidencias > SubsidioIncidencias Then
                    '    SubsidioEfectivoIncidencias = 0
                    '    ImpuestoIncidencias = ImpuestoIncidencias - SubsidioIncidencias
                    'ElseIf ImpuestoIncidencias < SubsidioIncidencias Then
                    '    SubsidioEfectivoIncidencias = SubsidioIncidencias - ImpuestoIncidencias
                    '    ImpuestoIncidencias = 0
                    'End If

                    'ImporteImpuestoIncidenciasOrdinario = Math.Round(ImpuestoIncidencias, 6)
                    'ImporteSubsidioIncidenciasOrdinario = Math.Round(SubsidioEfectivoIncidencias, 6)

                    'If ImporteImpuestoIncidencias > 0 And ImporteImpuestoIncidenciasOrdinario > 0 Then
                    '    DiferenciaimpuestoSubsidio = ImporteImpuestoIncidencias - ImporteImpuestoIncidenciasOrdinario
                    '    TasaSubsidioImpuestoIncidencias = DiferenciaimpuestoSubsidio / ImporteProporcionalIncidencias
                    '    ImpuestoIncidencias = (TasaSubsidioImpuestoIncidencias * 0.1) * IngresoGravadoMes
                    '    SubsidioIncidencias = 0
                    'End If

                    'If ImporteImpuestoIncidencias > 0 And ImporteSubsidioIncidenciasOrdinario > 0 Then
                    '    DiferenciaimpuestoSubsidio = ImporteImpuestoIncidencias - ImporteSubsidioIncidenciasOrdinario
                    '    TasaSubsidioImpuestoIncidencias = DiferenciaimpuestoSubsidio / ImporteProporcionalIncidencias
                    '    SubsidioIncidencias = (TasaSubsidioImpuestoIncidencias * 0.1) * IngresoGravadoMes
                    '    ImpuestoIncidencias = 0
                    'End If

                End If
            End If

            Impuesto = Impuesto + ImpuestoIncidencias
            SubsidioEfectivo = SubsidioEfectivo + SubsidioIncidencias

            If Impuesto > SubsidioEfectivo Then
                Impuesto = Impuesto - SubsidioEfectivo
                SubsidioEfectivo = 0
            ElseIf Impuesto < SubsidioEfectivo Then
                SubsidioEfectivo = SubsidioEfectivo - Impuesto
                Impuesto = 0
            End If

            Call GuardarRegistro(1)
            Call ChecarYGrabarPercepcionesExentasYGravadas(empleadoId.Value, 0)
            Call MostrarPercepciones()
            Call MostrarDeducciones()
            Call ChecarPercepcionesExentasYGravadasFiniquito()

            txtGravadoISR.Text = Math.Round(PercepcionesGravadas, 6)
            txtExentoISR.Text = Math.Round(PercepcionesExentas, 6)

            txtImporteIncidencia.Text = ""
            txtUnidadIncidencia.Text = ""

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Function ChecarSiExiste(ByVal CvoConcepto As Int32) As Boolean
        Try

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Tipo = "F"
            cNomina.TipoConcepto = "P"
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.IdMovimiento = Request("id")
            cNomina.CvoConcepto = CvoConcepto
            dt = cNomina.ConsultarConceptosFiniquito()

            If dt.Rows.Count = 0 Then
                ChecarSiExiste = False
            Else
                ChecarSiExiste = True
            End If

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Function
    Private Function ChecarQueExistaPercepcionesFiniquito() As Boolean
        Try

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Tipo = "F"
            cNomina.TipoConcepto = "P"
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.IdMovimiento = Request("id")
            dt = cNomina.ConsultarConceptosFiniquito()

            If dt.Rows.Count >= 0 And dt.Compute("Sum(Importe)", "CvoConcepto=14 OR CvoConcepto=15 OR CvoConcepto=16") IsNot DBNull.Value Then
                If dt.Compute("Sum(Importe)", "CvoConcepto=14 OR CvoConcepto=15 OR CvoConcepto=16") > 0 Then
                    ChecarQueExistaPercepcionesFiniquito = True
                Else
                    ChecarQueExistaPercepcionesFiniquito = False
                End If
            Else
                ChecarQueExistaPercepcionesFiniquito = False
            End If

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Function
    Private Sub GuardarRegistro(ByVal ConImpuesto)
        Try
            Call CargarVariablesGenerales()

            Dim ImporteIncidencia As Decimal = 0
            Dim UnidadIncidencia As Decimal = 0

            Try
                ImporteIncidencia = Math.Round(Convert.ToDecimal(txtImporteIncidencia.Text), 6)
            Catch ex As Exception
                UnidadIncidencia = 0
            End Try

            Try
                UnidadIncidencia = Convert.ToDecimal(txtUnidadIncidencia.Text)
            Catch ex As Exception
                UnidadIncidencia = 0
            End Try

            'ConImpuesto = 1 cuando viene de agregar una percepcion y calcular impuestos guardando ambos
            'ConImpuesto = 2 cuando viene de quitar una percepcion y solo se procede a guardar los impuesto modificados sin esa percepcion

            'Percepciones
            If ConImpuesto = 1 Then
                If cmbConcepto.SelectedValue < 52 Then
                    GuardarExentoYGravado(cmbConcepto.SelectedValue, UnidadIncidencia, ImporteIncidencia, ImporteIncidencia, 0, "P")
                ElseIf cmbConcepto.SelectedValue.ToString = "57" Or cmbConcepto.SelectedValue.ToString = "58" Or cmbConcepto.SelectedValue.ToString = "59" Or cmbConcepto.SelectedValue.ToString = "161" Or cmbConcepto.SelectedValue.ToString = "162" Then
                    'Deducciones por faltas, permisos o incapacidades
                    GuardarExentoYGravado(cmbConcepto.SelectedValue, UnidadIncidencia, ImporteIncidencia, 0, ImporteIncidencia, "D")
                ElseIf cmbConcepto.SelectedValue.ToString >= 61 Then
                    'Deducciones
                    GuardarExentoYGravado(cmbConcepto.SelectedValue, UnidadIncidencia, ImporteIncidencia, 0, ImporteIncidencia, "D")
                End If
            End If

            'Aqui se guarda el impuesto, el total Gravado y el total exento, tanto cuando viene de agregar un concepto como cuando viene de quitar un concepto ya que de ambas maneras se recalcula
            'Solo no entra en este bloque de codigo cuando viene de agregar una deduccion que no implica recalcular gravado, exento o impuesto(las unicas deducciones que recalculan gravado, exento e impuesto son la faltas, permisos e incapacidades, las demas deduccioens haciendo hincapie, no entran en este bloque)

            If ConImpuesto = 1 Then
                Dim cNomina As New Nomina()
                If cmbConcepto.SelectedValue < 52 Then

                    GuardarExentoYGravado(cmbConcepto.SelectedValue, 1, Impuesto, 0, Impuesto, "P")

                    If Impuesto > 0 Then
                        cNomina = New Nomina()
                        'cNomina.IdEmpresa = IdEmpresa
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 3 'Quincenal
                        cNomina.Periodo = cmbPeriodo.SelectedValue
                        cNomina.NoEmpleado = empleadoId.Value
                        cNomina.CvoConcepto = 86
                        cNomina.IdContrato = contratoId.Value
                        cNomina.Tipo = "F"
                        cNomina.TipoConcepto = "D"
                        cNomina.Unidad = 1
                        cNomina.Importe = Impuesto
                        cNomina.ImporteGravado = 0
                        cNomina.ImporteExento = Impuesto
                        cNomina.Generado = "S"
                        cNomina.Timbrado = ""
                        cNomina.Enviado = ""
                        cNomina.Situacion = ""
                        cNomina.IdMovimiento = Request("id")
                        cNomina.GuadarNomina()
                    End If
                    If SubsidioEfectivo > 0 Then
                        cNomina = New Nomina()
                        'cNomina.IdEmpresa = IdEmpresa
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 3 'Quincenal
                        cNomina.Periodo = cmbPeriodo.SelectedValue
                        cNomina.NoEmpleado = empleadoId.Value
                        cNomina.CvoConcepto = 55
                        cNomina.IdContrato = contratoId.Value
                        cNomina.Tipo = "F"
                        cNomina.TipoConcepto = "P"
                        cNomina.Unidad = 1
                        cNomina.Importe = SubsidioEfectivo
                        cNomina.ImporteGravado = 0
                        cNomina.ImporteExento = SubsidioEfectivo
                        cNomina.Generado = "S"
                        cNomina.Timbrado = ""
                        cNomina.Enviado = ""
                        cNomina.Situacion = ""
                        cNomina.IdMovimiento = Request("id")
                        cNomina.GuadarNomina()
                    End If

                    CalcularImss()
                    Imss = Imss * DiasCuotaPeriodo
                    Imss = Math.Round(Imss, 6)

                    If Imss > 0 Then
                        cNomina = New Nomina()
                        'cNomina.IdEmpresa = IdEmpresa
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 3 'Quincenal
                        cNomina.Periodo = cmbPeriodo.SelectedValue
                        cNomina.NoEmpleado = empleadoId.Value
                        cNomina.CvoConcepto = 56
                        cNomina.IdContrato = contratoId.Value
                        cNomina.Tipo = "F"
                        cNomina.TipoConcepto = "D"
                        cNomina.Unidad = 1
                        cNomina.Importe = Imss
                        cNomina.ImporteGravado = 0
                        cNomina.ImporteExento = Imss
                        cNomina.Generado = "S"
                        cNomina.Timbrado = ""
                        cNomina.Enviado = ""
                        cNomina.Situacion = ""
                        cNomina.IdMovimiento = Request("id")
                        cNomina.GuadarNomina()
                    End If
                    If cmbConcepto.SelectedValue = 2 Then
                        ''''''// Consultar SI tiene desuento de INFONAVIT //'''''''
                        Dim Valor As Decimal
                        Dim DescuentoInvonavit As Decimal
                        Dim datos As New DataTable
                        Dim Infonavit As New Entities.Infonavit()
                        Infonavit.IdEmpresa = Session("clienteid")
                        Infonavit.IdEmpleado = empleadoId.Value
                        datos = Infonavit.ConsultarEmpleadosConDescuentoInfonavit()
                        Infonavit = Nothing

                        If datos.Rows.Count > 0 Then
                            If datos.Rows(0)("tipo_descuento") = 1 Then
                                Valor = datos.Rows(0)("valor_descuento")
                                DescuentoInvonavit = ((Valor + ImporteSeguroVivienda) / 30.4) * UnidadIncidencia
                            ElseIf datos.Rows(0)("tipo_descuento") = 2 Then
                                Valor = datos.Rows(0)("valor_descuento")
                                DescuentoInvonavit = (((Valor * SalarioMinimoDiarioGeneral) + ImporteSeguroVivienda) / 30.4) * UnidadIncidencia
                            ElseIf datos.Rows(0)("tipo_descuento") = 3 Then
                                Valor = datos.Rows(0)("valor_descuento")
                                DescuentoInvonavit = ((SalarioDiarioIntegradoTrabajador * (Valor / 100)) + (ImporteSeguroVivienda / 30.4)) * UnidadIncidencia
                            End If

                            If DescuentoInvonavit > 0 Then
                                cNomina = New Nomina()
                                'cNomina.IdEmpresa = IdEmpresa
                                cNomina.Ejercicio = IdEjercicio
                                cNomina.TipoNomina = 3 'Quincenal
                                cNomina.Periodo = cmbPeriodo.SelectedValue
                                cNomina.NoEmpleado = empleadoId.Value
                                cNomina.CvoConcepto = 64
                                cNomina.IdContrato = contratoId.Value
                                cNomina.Tipo = "F"
                                cNomina.TipoConcepto = "D"
                                cNomina.Unidad = 1
                                cNomina.Importe = DescuentoInvonavit
                                cNomina.ImporteGravado = 0
                                cNomina.ImporteExento = DescuentoInvonavit
                                cNomina.Generado = "S"
                                cNomina.Timbrado = ""
                                cNomina.Enviado = ""
                                cNomina.Situacion = ""
                                cNomina.IdMovimiento = Request("id")
                                cNomina.GuadarNomina()
                            End If
                        End If
                    End If
                End If
            ElseIf ConImpuesto = 2 Then
                Dim cNomina As New Nomina()
                If cmbConcepto.SelectedValue < 52 Then
                    If Impuesto > 0 Then
                        cNomina = New Nomina()
                        'cNomina.IdEmpresa = IdEmpresa
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 3 'Quincenal
                        cNomina.Periodo = cmbPeriodo.SelectedValue
                        cNomina.NoEmpleado = empleadoId.Value
                        cNomina.CvoConcepto = 86
                        cNomina.IdContrato = contratoId.Value
                        cNomina.Tipo = "F"
                        cNomina.TipoConcepto = "D"
                        cNomina.Unidad = 1
                        cNomina.Importe = Impuesto
                        cNomina.ImporteGravado = 0
                        cNomina.ImporteExento = Impuesto
                        cNomina.Generado = "S"
                        cNomina.Timbrado = ""
                        cNomina.Enviado = ""
                        cNomina.Situacion = ""
                        cNomina.IdMovimiento = Request("id")
                        cNomina.GuadarNomina()
                    End If
                    If SubsidioEfectivo > 0 Then
                        cNomina = New Nomina()
                        'cNomina.IdEmpresa = IdEmpresa
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 3 'Quincenal
                        cNomina.Periodo = cmbPeriodo.SelectedValue
                        cNomina.NoEmpleado = empleadoId.Value
                        cNomina.CvoConcepto = 55
                        cNomina.IdContrato = contratoId.Value
                        cNomina.Tipo = "F"
                        cNomina.TipoConcepto = "P"
                        cNomina.Unidad = 1
                        cNomina.Importe = SubsidioEfectivo
                        cNomina.ImporteGravado = 0
                        cNomina.ImporteExento = SubsidioEfectivo
                        cNomina.Generado = "S"
                        cNomina.Timbrado = ""
                        cNomina.Enviado = ""
                        cNomina.Situacion = ""
                        cNomina.IdMovimiento = Request("id")
                        cNomina.GuadarNomina()
                    End If

                    CalcularImss()
                    Imss = Imss * DiasCuotaPeriodo
                    Imss = Math.Round(Imss, 6)

                    If Imss > 0 Then
                        cNomina = New Nomina()
                        'cNomina.IdEmpresa = IdEmpresa
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 3 'Quincenal
                        cNomina.Periodo = cmbPeriodo.SelectedValue
                        cNomina.NoEmpleado = empleadoId.Value
                        cNomina.CvoConcepto = 56
                        cNomina.IdContrato = contratoId.Value
                        cNomina.Tipo = "F"
                        cNomina.TipoConcepto = "D"
                        cNomina.Unidad = 1
                        cNomina.Importe = Imss
                        cNomina.ImporteGravado = 0
                        cNomina.ImporteExento = Imss
                        cNomina.Generado = "S"
                        cNomina.Timbrado = ""
                        cNomina.Enviado = ""
                        cNomina.Situacion = ""
                        cNomina.IdMovimiento = Request("id")
                        cNomina.GuadarNomina()
                    End If
                End If
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub BorrarDeduccionesFiniquito()
        Try
            Call CargarVariablesGenerales()

            Call EliminaConceptosFiniquito(Request("id"), 52) 'IMPUESTO

            Call EliminaConceptosFiniquito(Request("id"), 54) 'SUBSIDIO APLICADO

            Call EliminaConceptosFiniquito(Request("id"), 55) 'SUBSIDIO EFECTIVO

            Call EliminaConceptosFiniquito(Request("id"), 56) 'CUOTA IMSS

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub ChecarPercepcionesGravadas(ByVal CvoConcepto As Int32)
        Try
            Dim CuotaPeriodo As Decimal = 0
            Dim PrimaDominical As Decimal = 0
            Dim PrimaVacacional As Decimal = 0
            Dim Vacaciones As Decimal = 0
            Dim Aguinaldo As Decimal = 0
            Dim RepartoUtilidades As Decimal = 0
            Dim FondoAhorro As Decimal = 0
            Dim AyudaFuneral As Decimal = 0
            Dim PrevisionSocial As Decimal = 0
            Dim GrupoPercepcionesGravadasTotalmenteSinExentos As Decimal = 0
            Dim PagoPorHoras As Decimal = 0
            Dim Comisiones As Decimal = 0
            Dim Destajo As Decimal = 0
            Dim FaltasPermisosIncapacidades As Decimal = 0
            HonorarioAsimilado = 0
            DiasVacaciones = 0
            DiasCuotaPeriodo = 0
            DiasHonorarioAsimilado = 0
            DiasPagoPorHoras = 0
            DiasComision = 0
            DiasDestajo = 0
            DiasFaltasPermisosIncapacidades = 0
            TiempoExtraordinarioDentroDelMargenLegal = 0
            TiempoExtraordinarioFueraDelMargenLegal = 0
            ImporteGravado = 0

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Tipo = "F"
            cNomina.TipoConcepto = "P"
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.IdMovimiento = Request("id")
            dt = cNomina.ConsultarConceptosFiniquito()

            'PercepcionesGravadas
            If dt.Rows.Count > 0 Then
                If dt.Compute("Sum(Importe)", "CvoConcepto=85") IsNot DBNull.Value Then
                    DiasCuotaPeriodo = dt.Compute("Sum(UNIDAD)", "CvoConcepto=85")
                    CuotaPeriodo = dt.Compute("Sum(Importe)", "CvoConcepto=85")
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
                If dt.Compute("Sum(Importe)", "CvoConcepto=6 OR CvoConcepto=9 OR CvoConcepto=10") IsNot DBNull.Value Then
                    TiempoExtraordinarioDentroDelMargenLegal = dt.Compute("Sum(Importe)", "CvoConcepto=6 OR CvoConcepto=9 OR CvoConcepto=10")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=7 OR CvoConcepto=8") IsNot DBNull.Value Then
                    TiempoExtraordinarioFueraDelMargenLegal = dt.Compute("Sum(Importe)", "CvoConcepto=7 OR CvoConcepto=8")
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
                '33,34,35,36,37,38,40,41,43,45,46,47,48
                If dt.Compute("Sum(Importe)", "CvoConcepto=33 OR CvoConcepto=34 OR CvoConcepto=35 OR CvoConcepto=36 OR CvoConcepto=37 OR CvoConcepto=38 OR CvoConcepto=40 OR CvoConcepto=41 OR CvoConcepto=43 OR CvoConcepto=45 OR CvoConcepto=46 OR CvoConcepto=47 OR CvoConcepto=48") IsNot DBNull.Value Then
                    PrevisionSocial = dt.Compute("Sum(Importe)", "CvoConcepto=33 OR CvoConcepto=34 OR CvoConcepto=35 OR CvoConcepto=36 OR CvoConcepto=37 OR CvoConcepto=38 OR CvoConcepto=40 OR CvoConcepto=41 OR CvoConcepto=43 OR CvoConcepto=45 OR CvoConcepto=46 OR CvoConcepto=47 OR CvoConcepto=48")
                End If
                '12,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,39,49,51
                If dt.Compute("Sum(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=18 OR CvoConcepto=19 OR CvoConcepto=20 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=28 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=32 OR CvoConcepto=39 OR CvoConcepto=49 OR CvoConcepto=51") IsNot DBNull.Value Then
                    GrupoPercepcionesGravadasTotalmenteSinExentos = dt.Compute("Sum(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=18 OR CvoConcepto=19 OR CvoConcepto=20 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=28 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=32 OR CvoConcepto=39 OR CvoConcepto=49 OR CvoConcepto=51")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") IsNot DBNull.Value Then
                    DiasFaltasPermisosIncapacidades = dt.Compute("Sum(UNIDAD)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162")
                    FaltasPermisosIncapacidades = dt.Compute("Sum(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162")
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
                ElseIf CvoConcepto.ToString = "51" Then
                    DiasCuotaPeriodo = DiasCuotaPeriodo + UnidadIncidencia
                    CuotaPeriodo = CuotaPeriodo + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "33" Or CvoConcepto.ToString = "34" Or CvoConcepto.ToString = "35" Or CvoConcepto.ToString = "36" Or CvoConcepto.ToString = "37" Or CvoConcepto.ToString = "38" Or CvoConcepto.ToString = "40" Or CvoConcepto.ToString = "41" Or CvoConcepto.ToString = "43" Or CvoConcepto.ToString = "45" Or CvoConcepto.ToString = "46" Or CvoConcepto.ToString = "47" Or CvoConcepto.ToString = "48" Then
                    PrevisionSocial = PrevisionSocial + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "12" Or CvoConcepto.ToString = "17" Or CvoConcepto.ToString = "18" Or CvoConcepto.ToString = "19" Or CvoConcepto.ToString = "20" Or CvoConcepto.ToString = "21" Or CvoConcepto.ToString = "22" Or CvoConcepto.ToString = "23" Or CvoConcepto.ToString = "24" Or CvoConcepto.ToString = "25" Or CvoConcepto.ToString = "26" Or CvoConcepto.ToString = "27" Or CvoConcepto.ToString = "28" Or CvoConcepto.ToString = "29" Or CvoConcepto.ToString = "30" Or CvoConcepto.ToString = "31" Or CvoConcepto.ToString = "32" Or CvoConcepto.ToString = "39" Or CvoConcepto.ToString = "49" Or CvoConcepto.ToString = "51" Then
                    '12,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,39,49,51
                    GrupoPercepcionesGravadasTotalmenteSinExentos = GrupoPercepcionesGravadasTotalmenteSinExentos + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "57" Or CvoConcepto.ToString = "58" Or CvoConcepto.ToString = "59" Or CvoConcepto.ToString = "161" Or CvoConcepto.ToString = "162" Then
                    'deducciones
                    'ooooooooooooooooooooooooooooooooooooooooooooooo
                    ' aqui se descuenta de la cuota periodo el Importe de las faltas, permisos e incapacidades
                    DiasFaltasPermisosIncapacidades = DiasFaltasPermisosIncapacidades + UnidadIncidencia
                    FaltasPermisosIncapacidades = FaltasPermisosIncapacidades + ImporteIncidencia
                    'ooooooooooooooooooooooooooooooooooooooooooooooo
                End If
            End If
            'deducciones
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            DiasCuotaPeriodo = DiasCuotaPeriodo - DiasFaltasPermisosIncapacidades
            CuotaPeriodo = CuotaPeriodo - FaltasPermisosIncapacidades
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            If (TiempoExtraordinarioDentroDelMargenLegal / 2) < (SalarioMinimoDiarioGeneral * 5) Then
                ImporteExento = TiempoExtraordinarioDentroDelMargenLegal / 2
                ImporteGravado = TiempoExtraordinarioDentroDelMargenLegal / 2
            Else
                ImporteExento = SalarioMinimoDiarioGeneral * 5
                ImporteGravado = TiempoExtraordinarioDentroDelMargenLegal - (SalarioMinimoDiarioGeneral * 5)
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

                If CvoConcepto.ToString = "13" And ImporteIncidencia <= SalarioMinimoDiarioGeneral Then
                    ImporteExento = ImporteExento + PrimaDominical
                ElseIf CvoConcepto.ToString = "13" And ImporteIncidencia > SalarioMinimoDiarioGeneral Then
                    ImporteExento = ImporteExento + SalarioMinimoDiarioGeneral
                    ImporteGravado = ImporteGravado + (ImporteIncidencia - SalarioMinimoDiarioGeneral)
                End If
            End If
            'If Aguinaldo > 0 And Aguinaldo < (SalarioMinimoDiarioGeneral * 30) Then
            '    ImporteExento = ImporteExento + Aguinaldo
            'ElseIf Aguinaldo > 0 And Aguinaldo > (SalarioMinimoDiarioGeneral * 30) Then
            '    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 30)
            '    ImporteGravado = ImporteGravado + (Aguinaldo - (SalarioMinimoDiarioGeneral * 30))
            'End If
            'If PrimaVacacional > 0 And PrimaVacacional < (SalarioMinimoDiarioGeneral * 15) Then
            '    ImporteExento = ImporteExento + PrimaVacacional
            'ElseIf PrimaVacacional > 0 And PrimaVacacional > (SalarioMinimoDiarioGeneral * 15) Then
            '    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 15)
            '    ImporteGravado = ImporteGravado + (PrimaVacacional - (SalarioMinimoDiarioGeneral * 15))
            'End If
            If RepartoUtilidades > 0 And RepartoUtilidades < (SalarioMinimoDiarioGeneral * 15) Then
                ImporteExento = ImporteExento + RepartoUtilidades
            ElseIf RepartoUtilidades > 0 And RepartoUtilidades > (SalarioMinimoDiarioGeneral * 15) Then
                ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 15)
                ImporteGravado = ImporteGravado + (RepartoUtilidades - (SalarioMinimoDiarioGeneral * 15))
            End If
            'El tiempo extraordinario2 es gravado al 100%, no tiene nada exento igual que las vacaciones y la Cuota del periodo
            ImporteGravado = ImporteGravado + TiempoExtraordinarioFueraDelMargenLegal + CuotaPeriodo
            'ImporteGravado = ImporteGravado + TiempoExtraordinarioFueraDelMargenLegal + Vacaciones + CuotaPeriodo
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
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub ChecarYGrabarPercepcionesExentasYGravadas(ByVal NoEmpleado As Integer, ByVal CvoConcepto As Integer)
        Try
            CuotaPeriodo = 0
            HorasTriples = 0
            DescansoTrabajado = 0
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
            TiempoExtraordinarioDentroDelMargenLegal = 0
            TiempoExtraordinarioFueraDelMargenLegal = 0
            NumeroDeDiasPagados = 0

            'SubsidioAplicado = 0
            SubsidioEfectivo = 0

            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            ImporteExentoTiempoExtraordinarioDentroDelMargenLegal = 0
            ImporteGravadoTiempoExtraordinarioDentroDelMargenLegal = 0
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
            '-------------------------------------------------------------------------------
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
            BonoProduccion = 0
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
            AyudaCarestia = 0
            DespensaEfectivo = 0
            HaberPorRetiro = 0
            OtrasPercepciones = 0
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            HorasDoblesGravadas = 0
            HorasDoblesExentas = 0
            FestivoTrabajadoGravado = 0
            FestivoTrabajadoExento = 0
            DobleteGravado = 0
            DobleteExento = 0

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Tipo = "F"
            cNomina.TipoConcepto = "P"
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.IdMovimiento = Request("id")
            dt = cNomina.ConsultarConceptosFiniquito()

            If dt.Rows.Count > 0 Then
                If dt.Compute("Sum(Importe)", "CvoConcepto=85") IsNot DBNull.Value Then
                    DiasCuotaPeriodo = dt.Compute("Sum(UNIDAD)", "CvoConcepto=85")
                    CuotaPeriodo = dt.Compute("Sum(Importe)", "CvoConcepto=85")
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
                If dt.Compute("Sum(Importe)", "CvoConcepto=6 OR CvoConcepto=9 OR CvoConcepto=10") IsNot DBNull.Value Then
                    TiempoExtraordinarioDentroDelMargenLegal = dt.Compute("Sum(Importe)", "CvoConcepto=6 OR CvoConcepto=9 OR CvoConcepto=10")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=7 OR CvoConcepto=8") IsNot DBNull.Value Then
                    TiempoExtraordinarioFueraDelMargenLegal = dt.Compute("Sum(Importe)", "CvoConcepto=7 OR CvoConcepto=8")
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
                '33,34,35,36,37,38,40,41,43,45,46,47,48
                If dt.Compute("Sum(Importe)", "CvoConcepto=33 OR CvoConcepto=34 OR CvoConcepto=35 OR CvoConcepto=36 OR CvoConcepto=37 OR CvoConcepto=38 OR CvoConcepto=40 OR CvoConcepto=41 OR CvoConcepto=43 OR CvoConcepto=45 OR CvoConcepto=46 OR CvoConcepto=47 OR CvoConcepto=48") IsNot DBNull.Value Then
                    PrevisionSocial = dt.Compute("Sum(Importe)", "CvoConcepto=33 OR CvoConcepto=34 OR CvoConcepto=35 OR CvoConcepto=36 OR CvoConcepto=37 OR CvoConcepto=38 OR CvoConcepto=40 OR CvoConcepto=41 OR CvoConcepto=43 OR CvoConcepto=45 OR CvoConcepto=46 OR CvoConcepto=47 OR CvoConcepto=48")
                End If
                '12,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,39,49,51
                If dt.Compute("Sum(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=18 OR CvoConcepto=19 OR CvoConcepto=20 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=28 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=32 OR CvoConcepto=39 OR CvoConcepto=49 OR CvoConcepto=51") IsNot DBNull.Value Then
                    GrupoPercepcionesGravadasTotalmenteSinExentos = dt.Compute("Sum(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=18 OR CvoConcepto=19 OR CvoConcepto=20 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=28 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=32 OR CvoConcepto=39 OR CvoConcepto=49 OR CvoConcepto=51")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") IsNot DBNull.Value Then
                    DiasFaltasPermisosIncapacidades = dt.Compute("Sum(UNIDAD)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162")
                    FaltasPermisosIncapacidades = dt.Compute("Sum(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162")
                End If

                'If dt.Compute("Sum(Importe)", "CvoConcepto=54") IsNot DBNull.Value Then
                '    SubsidioAplicado = dt.Compute("Sum(Importe)", "CvoConcepto=54")
                'End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=55") IsNot DBNull.Value Then
                    SubsidioEfectivo = dt.Compute("Sum(Importe)", "CvoConcepto=55")
                End If
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
            If TiempoExtraordinarioDentroDelMargenLegal > 0 Then
                If (TiempoExtraordinarioDentroDelMargenLegal / 2) < (SalarioMinimoDiarioGeneral * 5) Then
                    ImporteExento = TiempoExtraordinarioDentroDelMargenLegal / 2
                    ImporteGravado = TiempoExtraordinarioDentroDelMargenLegal / 2
                    ImporteExentoTiempoExtraordinarioDentroDelMargenLegal = TiempoExtraordinarioDentroDelMargenLegal / 2
                    ImporteGravadoTiempoExtraordinarioDentroDelMargenLegal = TiempoExtraordinarioDentroDelMargenLegal / 2
                Else
                    ImporteExento = SalarioMinimoDiarioGeneral * 5
                    ImporteGravado = TiempoExtraordinarioDentroDelMargenLegal - (SalarioMinimoDiarioGeneral * 5)
                    ImporteExentoTiempoExtraordinarioDentroDelMargenLegal = SalarioMinimoDiarioGeneral * 5
                    ImporteGravadoTiempoExtraordinarioDentroDelMargenLegal = TiempoExtraordinarioDentroDelMargenLegal - (SalarioMinimoDiarioGeneral * 5)
                End If
            End If

            If CuotaPeriodo > 0 Then
                GuardarExentoYGravado(2, CuotaPeriodo - FaltasPermisosIncapacidades, FaltasPermisosIncapacidades, NoEmpleado)
            End If

            If PrimaDominical > 0 Then
                Dim Dias As Integer
                Dias = dt.Compute("Sum(UNIDAD)", "CvoConcepto=13")
                If PrimaDominical < (SalarioMinimoDiarioGeneral * Dias) Then
                    ImporteExento = ImporteExento + PrimaDominical
                    'ImporteExentoPrimaDominical = ImporteExento + PrimaDominical 'ESTO ESTA MAL?, REVISAR!!!!!
                    ImporteExentoPrimaDominical = PrimaDominical                  'EN ESTA LINEA YA CORREGI
                    ImporteGravadoPrimaDominical = 0
                Else
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * Dias)
                    ImporteGravado = ImporteGravado + (PrimaDominical - (SalarioMinimoDiarioGeneral * Dias))
                    ImporteExentoPrimaDominical = SalarioMinimoDiarioGeneral * Dias
                    ImporteGravadoPrimaDominical = PrimaDominical - (SalarioMinimoDiarioGeneral * Dias)
                End If
                GuardarExentoYGravado(13, ImporteGravadoPrimaDominical, ImporteExentoPrimaDominical, NoEmpleado)
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
                If PrimaVacacional > 0 And PrimaVacacional < (SalarioMinimoDiarioGeneral * 15) Then
                    ImporteExento = ImporteExento + PrimaVacacional
                    ImporteExentoPrimaVacacional = PrimaVacacional
                    ImporteGravadoPrimaVacacional = 0
                ElseIf PrimaVacacional > 0 And PrimaVacacional > (SalarioMinimoDiarioGeneral * 15) Then
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 15)
                    ImporteGravado = ImporteGravado + (PrimaVacacional - (SalarioMinimoDiarioGeneral * 15))
                    ImporteExentoPrimaVacacional = SalarioMinimoDiarioGeneral * 15
                    ImporteGravadoPrimaVacacional = PrimaVacacional - (SalarioMinimoDiarioGeneral * 15)
                End If
                GuardarExentoYGravado(16, ImporteGravadoPrimaVacacional, ImporteExentoPrimaVacacional, NoEmpleado)
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

            'distribuyendo las percepciones del GrupoPercepcionesGravadasTotalmenteSinExentos
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            '12,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,39,49,51
            If GrupoPercepcionesGravadasTotalmenteSinExentos > 0 Then
                If dt.Compute("Sum(Importe)", "CvoConcepto=12") IsNot DBNull.Value Then
                    Diferencias = dt.Compute("Sum(Importe)", "CvoConcepto=12")
                    GuardarExentoYGravado(12, Diferencias, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=17") IsNot DBNull.Value Then
                    Gratificacion = dt.Compute("Sum(Importe)", "CvoConcepto=17")
                    GuardarExentoYGravado(17, Gratificacion, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=18") IsNot DBNull.Value Then
                    Bonificacion = dt.Compute("Sum(Importe)", "CvoConcepto=18")
                    GuardarExentoYGravado(18, Bonificacion, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=19") IsNot DBNull.Value Then
                    Retroactivo = dt.Compute("Sum(Importe)", "CvoConcepto=19")
                    GuardarExentoYGravado(19, Retroactivo, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=20") IsNot DBNull.Value Then
                    BonoProduccion = dt.Compute("Sum(Importe)", "CvoConcepto=20")
                    GuardarExentoYGravado(20, BonoProduccion, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=21") IsNot DBNull.Value Then
                    PremioProductividad = dt.Compute("Sum(Importe)", "CvoConcepto=21")
                    GuardarExentoYGravado(21, PremioProductividad, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=22") IsNot DBNull.Value Then
                    Incentivo = dt.Compute("Sum(Importe)", "CvoConcepto=22")
                    GuardarExentoYGravado(22, Incentivo, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=23") IsNot DBNull.Value Then
                    PremioAsistencia = dt.Compute("Sum(Importe)", "CvoConcepto=23")
                    GuardarExentoYGravado(23, PremioAsistencia, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=24") IsNot DBNull.Value Then
                    PremioPuntualidad = dt.Compute("Sum(Importe)", "CvoConcepto=24")
                    GuardarExentoYGravado(24, PremioPuntualidad, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=25") IsNot DBNull.Value Then
                    Premio = dt.Compute("Sum(Importe)", "CvoConcepto=25")
                    GuardarExentoYGravado(25, Premio, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=26") IsNot DBNull.Value Then
                    Compensacion = dt.Compute("Sum(Importe)", "CvoConcepto=26")
                    GuardarExentoYGravado(26, Compensacion, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=27") IsNot DBNull.Value Then
                    BonoAntiguedad = dt.Compute("Sum(Importe)", "CvoConcepto=27")
                    GuardarExentoYGravado(27, BonoAntiguedad, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=28") IsNot DBNull.Value Then
                    Viaticos = dt.Compute("Sum(Importe)", "CvoConcepto=28")
                    GuardarExentoYGravado(28, Viaticos, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=29") IsNot DBNull.Value Then
                    Pasajes = dt.Compute("Sum(Importe)", "CvoConcepto=29")
                    GuardarExentoYGravado(29, Pasajes, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=30") IsNot DBNull.Value Then
                    AyudaTransporte = dt.Compute("Sum(Importe)", "CvoConcepto=30")
                    GuardarExentoYGravado(30, AyudaTransporte, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=31") IsNot DBNull.Value Then
                    AyudaRenta = dt.Compute("Sum(Importe)", "CvoConcepto=31")
                    GuardarExentoYGravado(31, AyudaRenta, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=32") IsNot DBNull.Value Then
                    AyudaCarestia = dt.Compute("Sum(Importe)", "CvoConcepto=32")
                    GuardarExentoYGravado(32, AyudaCarestia, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=39") IsNot DBNull.Value Then
                    DespensaEfectivo = dt.Compute("Sum(Importe)", "CvoConcepto=39")
                    GuardarExentoYGravado(39, DespensaEfectivo, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=49") IsNot DBNull.Value Then
                    HaberPorRetiro = dt.Compute("Sum(Importe)", "CvoConcepto=49")
                    GuardarExentoYGravado(49, HaberPorRetiro, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=51") IsNot DBNull.Value Then
                    OtrasPercepciones = dt.Compute("Sum(Importe)", "CvoConcepto=51")
                    GuardarExentoYGravado(51, OtrasPercepciones, 0, NoEmpleado)
                End If
            End If
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo

            'distribuyendo las horas extras fuera del margen legal
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            If TiempoExtraordinarioFueraDelMargenLegal > 0 Then
                If dt.Compute("Sum(Importe)", "CvoConcepto=7") IsNot DBNull.Value Then
                    HorasTriples = dt.Compute("Sum(Importe)", "CvoConcepto=7")
                    GuardarExentoYGravado(7, HorasTriples, 0, NoEmpleado)
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=8") IsNot DBNull.Value Then
                    DescansoTrabajado = dt.Compute("Sum(Importe)", "CvoConcepto=8")
                    GuardarExentoYGravado(8, DescansoTrabajado, 0, NoEmpleado)
                End If
            End If
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo

            'distribuyendo lo exento del grupo de percepciones TiempoExtraordinarioDentroDelMargenLegal en cada uno de sus elementos(6, 9 y 10)
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            If TiempoExtraordinarioDentroDelMargenLegal > 0 Then
                'HorasDoblesGravadas = 0
                'HorasDoblesExentas = 0
                'FestivoTrabajadoGravado = 0
                'FestivoTrabajadoExento = 0
                'DobleteGravado = 0
                'DobleteExento = 0
                If dt.Compute("Sum(Importe)", "CvoConcepto=6") IsNot DBNull.Value Then
                    If ImporteExentoTiempoExtraordinarioDentroDelMargenLegal > dt.Compute("Sum(Importe)", "CvoConcepto=6") / 2 Then
                        HorasDoblesGravadas = dt.Compute("Sum(Importe)", "CvoConcepto=6") / 2
                        HorasDoblesExentas = dt.Compute("Sum(Importe)", "CvoConcepto=6") / 2
                    Else
                        HorasDoblesGravadas = dt.Compute("Sum(Importe)", "CvoConcepto=6") - ImporteExentoTiempoExtraordinarioDentroDelMargenLegal
                        HorasDoblesExentas = ImporteExentoTiempoExtraordinarioDentroDelMargenLegal
                    End If
                    GuardarExentoYGravado(6, HorasDoblesGravadas, HorasDoblesExentas, NoEmpleado)
                End If
                ImporteExentoTiempoExtraordinarioDentroDelMargenLegal = ImporteExentoTiempoExtraordinarioDentroDelMargenLegal - HorasDoblesExentas
                If dt.Compute("Sum(Importe)", "CvoConcepto=9") IsNot DBNull.Value Then
                    If ImporteExentoTiempoExtraordinarioDentroDelMargenLegal = 0 Then
                        FestivoTrabajadoGravado = dt.Compute("Sum(Importe)", "CvoConcepto=9")
                        FestivoTrabajadoExento = 0
                    ElseIf ImporteExentoTiempoExtraordinarioDentroDelMargenLegal > 0 Then
                        If ImporteExentoTiempoExtraordinarioDentroDelMargenLegal > dt.Compute("Sum(Importe)", "CvoConcepto=9") Then
                            FestivoTrabajadoGravado = dt.Compute("Sum(Importe)", "CvoConcepto=9") / 2
                            FestivoTrabajadoExento = dt.Compute("Sum(Importe)", "CvoConcepto=9") / 2
                        Else
                            FestivoTrabajadoGravado = dt.Compute("Sum(Importe)", "CvoConcepto=9") - ImporteExentoTiempoExtraordinarioDentroDelMargenLegal
                            FestivoTrabajadoExento = ImporteExentoTiempoExtraordinarioDentroDelMargenLegal
                        End If
                    End If
                    GuardarExentoYGravado(9, FestivoTrabajadoGravado, FestivoTrabajadoExento, NoEmpleado)
                End If
                ImporteExentoTiempoExtraordinarioDentroDelMargenLegal = ImporteExentoTiempoExtraordinarioDentroDelMargenLegal - FestivoTrabajadoExento
                If dt.Compute("Sum(Importe)", "CvoConcepto=10") IsNot DBNull.Value Then
                    If ImporteExentoTiempoExtraordinarioDentroDelMargenLegal = 0 Then
                        DobleteGravado = dt.Compute("Sum(Importe)", "CvoConcepto=10")
                        DobleteExento = 0
                    ElseIf ImporteExentoTiempoExtraordinarioDentroDelMargenLegal > 0 Then
                        If ImporteExentoTiempoExtraordinarioDentroDelMargenLegal > dt.Compute("Sum(Importe)", "CvoConcepto=10") Then
                            DobleteGravado = dt.Compute("Sum(Importe)", "CvoConcepto=10") / 2
                            DobleteExento = dt.Compute("Sum(Importe)", "CvoConcepto=10") / 2
                        Else
                            DobleteGravado = dt.Compute("Sum(Importe)", "CvoConcepto=10") - ImporteExentoTiempoExtraordinarioDentroDelMargenLegal
                            DobleteExento = ImporteExentoTiempoExtraordinarioDentroDelMargenLegal
                        End If
                    End If
                    GuardarExentoYGravado(10, DobleteGravado, DobleteExento, NoEmpleado)
                End If
            End If
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo

            'distribuyendo lo exento del grupo de percepciones PrevisionSocial en cada uno de sus elementos(33,34,35,36,37,38,40,41,43,45,46,47,48)
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            If PrevisionSocial > 0 Then
                'If ImporteGravado + ImporteExento + PrevisionSocial < (SalarioMinimoDiarioGeneral * 7) Then
                If PrevisionSocial = ImporteExentoPrevisionSocial Then
                    If dt.Compute("Sum(Importe)", "CvoConcepto=33") IsNot DBNull.Value Then
                        AyudaCulturalExento = dt.Compute("Sum(Importe)", "CvoConcepto=33")
                        AyudaCulturalGravado = 0
                        GuardarExentoYGravado(33, AyudaCulturalGravado, AyudaCulturalExento, NoEmpleado)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=34") IsNot DBNull.Value Then
                        AyudaDeportivaExento = dt.Compute("Sum(Importe)", "CvoConcepto=34")
                        AyudaDeportivaGravado = 0
                        GuardarExentoYGravado(34, AyudaDeportivaGravado, AyudaDeportivaExento, NoEmpleado)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=35") IsNot DBNull.Value Then
                        AyudaEducacionalExento = dt.Compute("Sum(Importe)", "CvoConcepto=35")
                        AyudaEducacionalGravado = 0
                        GuardarExentoYGravado(35, AyudaEducacionalGravado, AyudaEducacionalExento, NoEmpleado)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=36") IsNot DBNull.Value Then
                        AyudaEscolarExento = dt.Compute("Sum(Importe)", "CvoConcepto=36")
                        AyudaEscolarGravado = 0
                        GuardarExentoYGravado(36, AyudaEscolarGravado, AyudaEscolarExento, NoEmpleado)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=37") IsNot DBNull.Value Then
                        AyudaComidaExento = dt.Compute("Sum(Importe)", "CvoConcepto=37")
                        AyudaComidaGravado = 0
                        GuardarExentoYGravado(37, AyudaComidaGravado, AyudaComidaExento, NoEmpleado)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=38") IsNot DBNull.Value Then
                        ValesDespensaExento = dt.Compute("Sum(Importe)", "CvoConcepto=38")
                        ValesDespensaGravado = 0
                        GuardarExentoYGravado(38, ValesDespensaGravado, ValesDespensaExento, NoEmpleado)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=40") IsNot DBNull.Value Then
                        AyudaUniformeExento = dt.Compute("Sum(Importe)", "CvoConcepto=40")
                        AyudaUniformeGravado = 0
                        GuardarExentoYGravado(40, AyudaUniformeGravado, AyudaUniformeExento, NoEmpleado)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=41") IsNot DBNull.Value Then
                        BecasExento = dt.Compute("Sum(Importe)", "CvoConcepto=41")
                        BecasGravado = 0
                        GuardarExentoYGravado(41, BecasGravado, BecasExento, NoEmpleado)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=43") IsNot DBNull.Value Then
                        SubsidioIncapacidadExento = dt.Compute("Sum(Importe)", "CvoConcepto=43")
                        SubsidioIncapacidadGravado = 0
                        GuardarExentoYGravado(43, SubsidioIncapacidadGravado, SubsidioIncapacidadExento, NoEmpleado)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=45") IsNot DBNull.Value Then
                        AyudaMatrimonioExento = dt.Compute("Sum(Importe)", "CvoConcepto=45")
                        AyudaMatrimonioGravado = 0
                        GuardarExentoYGravado(45, AyudaMatrimonioGravado, AyudaMatrimonioExento, NoEmpleado)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=46") IsNot DBNull.Value Then
                        AyudaNacimientoExento = dt.Compute("Sum(Importe)", "CvoConcepto=46")
                        AyudaNacimientoGravado = 0
                        GuardarExentoYGravado(46, AyudaNacimientoGravado, AyudaNacimientoExento, NoEmpleado)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=47") IsNot DBNull.Value Then
                        ValesComedorExento = dt.Compute("Sum(Importe)", "CvoConcepto=47")
                        ValesComedorGravado = 0
                        GuardarExentoYGravado(47, ValesComedorGravado, ValesComedorExento, NoEmpleado)
                    End If
                    If dt.Compute("Sum(Importe)", "CvoConcepto=48") IsNot DBNull.Value Then
                        AyudaMedicamentoExento = dt.Compute("Sum(Importe)", "CvoConcepto=48")
                        AyudaMedicamentoGravado = 0
                        GuardarExentoYGravado(48, AyudaMedicamentoGravado, AyudaMatrimonioExento, NoEmpleado)
                    End If
                ElseIf ImporteGravadoPrevisionSocial > 0 Then
                    If dt.Compute("Sum(Importe)", "CvoConcepto=33") IsNot DBNull.Value Then
                        If ImporteExentoPrevisionSocial > dt.Compute("Sum(Importe)", "CvoConcepto=33") Then
                            AyudaCulturalExento = dt.Compute("Sum(Importe)", "CvoConcepto=33")
                            AyudaCulturalGravado = 0
                        Else
                            AyudaCulturalExento = ImporteExentoPrevisionSocial
                            AyudaCulturalGravado = dt.Compute("Sum(Importe)", "CvoConcepto=33") - ImporteExentoPrevisionSocial
                        End If
                        GuardarExentoYGravado(33, AyudaCulturalGravado, AyudaCulturalExento, NoEmpleado)
                    End If
                    ImporteExentoPrevisionSocial = ImporteExentoPrevisionSocial - AyudaCulturalExento
                    If ImporteExentoPrevisionSocial <= 0 Then ImporteExentoPrevisionSocial = 0

                    If dt.Compute("Sum(Importe)", "CvoConcepto=34") IsNot DBNull.Value Then
                        If ImporteExentoPrevisionSocial > dt.Compute("Sum(Importe)", "CvoConcepto=34") Then
                            AyudaDeportivaExento = dt.Compute("Sum(Importe)", "CvoConcepto=34")
                            AyudaDeportivaGravado = 0
                        Else
                            AyudaDeportivaExento = ImporteExentoPrevisionSocial
                            AyudaDeportivaGravado = dt.Compute("Sum(Importe)", "CvoConcepto=34") - ImporteExentoPrevisionSocial
                        End If
                        GuardarExentoYGravado(34, AyudaDeportivaGravado, AyudaDeportivaExento, NoEmpleado)
                    End If
                    ImporteExentoPrevisionSocial = ImporteExentoPrevisionSocial - AyudaDeportivaExento
                    If ImporteExentoPrevisionSocial <= 0 Then ImporteExentoPrevisionSocial = 0

                    If dt.Compute("Sum(Importe)", "CvoConcepto=35") IsNot DBNull.Value Then
                        If ImporteExentoPrevisionSocial > dt.Compute("Sum(Importe)", "CvoConcepto=35") Then
                            AyudaEducacionalExento = dt.Compute("Sum(Importe)", "CvoConcepto=35")
                            AyudaEducacionalGravado = 0
                        Else
                            AyudaEducacionalExento = ImporteExentoPrevisionSocial
                            AyudaEducacionalGravado = dt.Compute("Sum(Importe)", "CvoConcepto=35") - ImporteExentoPrevisionSocial
                        End If
                        GuardarExentoYGravado(35, AyudaEducacionalGravado, AyudaEducacionalExento, NoEmpleado)
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
                        GuardarExentoYGravado(36, AyudaEscolarGravado, AyudaEscolarExento, NoEmpleado)
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
                        GuardarExentoYGravado(37, AyudaComidaGravado, AyudaComidaExento, NoEmpleado)
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
                        GuardarExentoYGravado(38, ValesDespensaGravado, ValesDespensaExento, NoEmpleado)
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
                        GuardarExentoYGravado(40, AyudaUniformeGravado, AyudaUniformeExento, NoEmpleado)
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
                        GuardarExentoYGravado(41, BecasGravado, BecasExento, NoEmpleado)
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
                        GuardarExentoYGravado(43, SubsidioIncapacidadGravado, SubsidioIncapacidadExento, NoEmpleado)
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
                        GuardarExentoYGravado(45, AyudaMatrimonioGravado, AyudaMatrimonioExento, NoEmpleado)
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
                        GuardarExentoYGravado(46, AyudaNacimientoGravado, AyudaNacimientoExento, NoEmpleado)
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
                        GuardarExentoYGravado(47, ValesComedorGravado, ValesComedorExento, NoEmpleado)
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
                        GuardarExentoYGravado(48, AyudaMedicamentoGravado, AyudaMatrimonioExento, NoEmpleado)
                    End If
                    ImporteExentoPrevisionSocial = ImporteExentoPrevisionSocial - AyudaMatrimonioExento
                    If ImporteExentoPrevisionSocial <= 0 Then ImporteExentoPrevisionSocial = 0
                End If
            End If
            'If CvoConcepto <> 64 Then
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            '''''''// Consultar SI tiene desuento de INFONAVIT //'''''''
            'Dim Valor As Decimal
            'Dim DescuentoInvonavit As Decimal
            'Dim datos As New DataTable
            'Dim Infonavit As New Entities.Infonavit()
            'Infonavit.IdEmpresa = Session("clienteid")
            'Infonavit.IdEmpleado = NoEmpleado
            'datos = Infonavit.ConsultarEmpleadosConDescuentoInfonavit()
            'Infonavit = Nothing

            'If datos.Rows.Count > 0 Then

            '    If datos.Rows(0)("tipo_descuento") = 1 Then
            '        Valor = datos.Rows(0)("valor_descuento")
            '        DescuentoInvonavit = ((Valor + ImporteSeguroVivienda) / 30.4) * NumeroDeDiasPagados
            '    ElseIf datos.Rows(0)("tipo_descuento") = 2 Then
            '        Valor = datos.Rows(0)("valor_descuento")
            '        DescuentoInvonavit = (((Valor * SalarioMinimoDiarioGeneral) + ImporteSeguroVivienda) / 30.4) * NumeroDeDiasPagados
            '    ElseIf datos.Rows(0)("tipo_descuento") = 3 Then
            '        Valor = datos.Rows(0)("valor_descuento")
            '        DescuentoInvonavit = ((SalarioDiarioIntegradoTrabajador * (Valor / 100)) + (ImporteSeguroVivienda / 30.4)) * NumeroDeDiasPagados
            '    End If

            '    QuitarConcepto(64)

            '    dt = New DataTable

            '    Dim Nomina As New Entities.Nomina()
            '    Nomina.IdEmpresa = Session("clienteid")
            '    Nomina.TipoNomina = 1 'Semanal
            '    Nomina.Periodo = periodoId.Value
            '    dt = Nomina.ConsultarEmpleadosSemanal()
            '    Nomina = Nothing

            '    If dt.Rows.Count > 0 Then
            '        ImporteDiario = dt.Rows(0)("CuotaDiaria")
            '        ImportePeriodo = dt.Rows(0)("CuotaDiaria") * 7
            '    End If
            '    ActualizarInfonavit(NoEmpleado, DescuentoInvonavit, 64)
            'End If
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            'End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub GuardarExentoYGravado(ByVal CvoConcepto, ByVal ImporteGravado, ByVal ImporteExento, ByVal NoEmpleado)
        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = IdEjercicio
        cNomina.CvoConcepto = CvoConcepto
        cNomina.ImporteGravado = ImporteGravado
        cNomina.ImporteExento = ImporteExento
        cNomina.IdMovimiento = Request("id")
        cNomina.ActualizarExentoYGravadoFiniquito()
        cNomina = Nothing
    End Sub
    Private Sub GridPercepciones_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridPercepciones.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                conceptoId.Value = e.CommandArgument
                rwConfirmEliminaConcepto.RadConfirm("¿Está seguro de eliminar este concepto?", "confirmCallbackFnEliminaConcepto", 330, 180, Nothing, "Confirmar")
        End Select
    End Sub
    Private Sub GridPercepciones_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridPercepciones.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim btnEliminar As ImageButton = CType(e.Item.FindControl("btnEliminar"), ImageButton)
            Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim CvoConcepto As Integer = item.OwnerTableView.DataKeyValues(item.ItemIndex)("CvoConcepto")
            If CvoConcepto = 14 Or CvoConcepto = 15 Or CvoConcepto = 16 Or CvoConcepto = 52 Or CvoConcepto = 54 Or CvoConcepto = 55 Or CvoConcepto = 56 Then
                btnEliminar.Visible = False
            End If
        End If
    End Sub
    Private Sub btnEliminarConcepto_Click(sender As Object, e As EventArgs) Handles btnEliminarConcepto.Click
        EliminarConcepto(conceptoId.Value)
    End Sub
    Private Sub EliminarConcepto(ByVal NumeroConcepto As Int32)
        ImporteDiario = 0
        ImportePeriodo = 0
        ImporteExento = 0
        ImporteGravado = 0
        SubsidioEfectivo = 0
        Agregar = 0

        'En esta parte se checa si el concepto que se esta agregando es percepcion(menor que 51) o es falta, permiso o incapacidad(57,58,59), siempre se borran los impuestos actuales y se recalculan, la demas deduccioen no entran aqui ya que no recalculan impuestos, simplemente se restan.
        If NumeroConcepto < 52 Or NumeroConcepto = "57" Or NumeroConcepto = "58" Or NumeroConcepto = "59" Or NumeroConcepto = "161" Or NumeroConcepto = "162" Then
            BorrarDeduccionesFiniquito()
        End If

        Call EliminaConceptosFiniquito(Request("id"), NumeroConcepto)

        If cmbConcepto.SelectedValue.ToString < 52 Then
            'En este bloque no entran las deducciones por adeudos que no cambian la base del impuesto y por lo tanto no lo calculan, ejemplos de esto serian, adeudos por credito infonavit, cuotas sindicales, adeudos fonacot, adeudos con el patron, etc, (conceptos del 61 al 86)
            BorrarDeduccionesFiniquito()

            Dim Aguinaldo As Decimal = 0
            Dim Vacaciones As Decimal = 0
            Dim DiasProporcionalVacaciones As Decimal = 0
            Dim PrimaVacacional As Decimal = 0
            Dim ImporteExentoAguinaldo As Decimal = 0
            Dim ImporteGravadoAguinaldo As Decimal = 0
            Dim ImporteExentoPrimaVacacional As Decimal = 0
            Dim ImporteGravadoPrimaVacacional As Decimal = 0

            Dim DiferenciaSubsidioImpuestoVacaciones As Decimal = 0
            Dim FactorImpuestoSubsidioVacaciones As Decimal = 0
            Dim TasaSubsidioImpuestoVacaciones As Decimal = 0

            Dim DiferenciaSubsidioImpuestoAguinaldo As Decimal = 0
            Dim FactorImpuestoSubsidioAguinaldo As Decimal = 0
            Dim TasaSubsidioImpuestoAguinaldo As Decimal = 0

            Dim DiferenciaSubsidioImpuestoIncidencias As Decimal = 0
            Dim FactorImpuestoSubsidioIncidencias As Decimal = 0
            Dim TasaSubsidioImpuestoIncidencias As Decimal = 0

            Dim DiasPagadosVacaciones As Integer = 0
            Try
                DiasPagadosVacaciones = Convert.ToInt32(txtDiasPagadosVacaciones.Text)
            Catch ex As Exception
                DiasPagadosVacaciones = 0
            End Try

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            cNomina.Id = Request("id")
            'cNomina.IdEmpresa = Session("clienteid")
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.DiasPagadosVacaciones = DiasPagadosVacaciones
            dt = cNomina.ConsultarDesgloseFiniquito()
            cNomina = Nothing

            If dt.Rows.Count > 0 Then
                For Each oDataRow In dt.Rows
                    ImporteDiario = oDataRow("SueldoDiario")
                    Aguinaldo = oDataRow("ProporcionalAguinaldo")
                    Vacaciones = oDataRow("ProporcionalVacaciones")
                    PrimaVacacional = oDataRow("PrimaVacaciones")
                    DiasProporcionalVacaciones = Math.Round(oDataRow("DiasProporcionalVacaciones"), 2)
                Next
            End If

            If Aguinaldo > 0 Then
                If Aguinaldo > 0 And Aguinaldo < (SalarioMinimoDiarioGeneral * 30) Then
                    ImporteExento = ImporteExento + Aguinaldo
                    ImporteExentoAguinaldo = Aguinaldo
                    ImporteGravadoAguinaldo = 0
                ElseIf Aguinaldo > 0 And Aguinaldo > (SalarioMinimoDiarioGeneral * 30) Then
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 30)
                    ImporteGravadoFiniquito = ImporteGravadoFiniquito + (Aguinaldo - (SalarioMinimoDiarioGeneral * 30))
                    ImporteExentoAguinaldo = SalarioMinimoDiarioGeneral * 30
                    ImporteGravadoAguinaldo = Aguinaldo - (SalarioMinimoDiarioGeneral * 30)
                End If
            End If

            If Vacaciones > 0 Then
                ImporteGravadoVacaciones = Vacaciones
                ImporteGravadoFiniquito = ImporteGravadoFiniquito + Vacaciones
            End If

            If PrimaVacacional > 0 Then
                If PrimaVacacional > 0 And PrimaVacacional < (SalarioMinimoDiarioGeneral * 15) Then
                    ImporteExento = ImporteExento + PrimaVacacional
                    ImporteExentoPrimaVacacional = PrimaVacacional
                    ImporteGravadoPrimaVacacional = 0
                ElseIf PrimaVacacional > 0 And PrimaVacacional > (SalarioMinimoDiarioGeneral * 15) Then
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 15)
                    ImporteGravadoFiniquito = ImporteGravadoFiniquito + (PrimaVacacional - (SalarioMinimoDiarioGeneral * 15))
                    ImporteExentoPrimaVacacional = SalarioMinimoDiarioGeneral * 15
                    ImporteGravadoPrimaVacacional = PrimaVacacional - (SalarioMinimoDiarioGeneral * 15)
                End If
            End If

            '/*-------------------------*/
            '1 Impuesto / Subsidio proporcional vacaciones
            Dim ImporteProporcionalVacaciones As Decimal
            Dim IngresoMensualOrdinario As Decimal
            Dim IngresoGravadoMes As Decimal

            Call ConsultarDiasMes()

            ImporteProporcionalVacaciones = (ImporteGravadoVacaciones / 365) * 30.4
            IngresoMensualOrdinario = (ImporteDiario * DiasMes)
            IngresoGravadoMes = ImporteProporcionalVacaciones + IngresoMensualOrdinario

            Call CalcularImpuestoVacaciones(IngresoGravadoMes)
            Call CalcularSubsidioVacaciones(IngresoGravadoMes)

            If ImpuestoVacaciones > SubsidioVacaciones Then
                SubsidioEfectivoVacaciones = 0
                ImpuestoVacaciones = ImpuestoVacaciones - SubsidioVacaciones
            ElseIf ImpuestoVacaciones < SubsidioVacaciones Then
                SubsidioEfectivoVacaciones = SubsidioVacaciones - ImpuestoVacaciones
                ImpuestoVacaciones = 0
            End If

            ImpuestoVacaciones = Math.Round(ImpuestoVacaciones, 6)
            SubsidioEfectivoVacaciones = Math.Round(SubsidioEfectivoVacaciones, 6)

            '2 Impuesto / Subsidio por sueldo ordinario
            Call CalcularImpuestoSueldoMesAnterior(IngresoMensualOrdinario)
            Call CalcularSubsidioSueldoMesAnterior(IngresoMensualOrdinario)

            If ImpuestoSueldoMesAnterior > SubsidioSueldoMesAnterior Then
                SubsidioEfectivoSueldoMesAnterior = 0
                ImpuestoSueldoMesAnterior = ImpuestoSueldoMesAnterior - SubsidioSueldoMesAnterior
            ElseIf ImpuestoSueldoMesAnterior < SubsidioSueldoMesAnterior Then
                SubsidioEfectivoSueldoMesAnterior = SubsidioSueldoMesAnterior - ImpuestoSueldoMesAnterior
                ImpuestoSueldoMesAnterior = 0
            End If

            ImpuestoSueldoMesAnterior = Math.Round(ImpuestoSueldoMesAnterior, 6)
            SubsidioEfectivoSueldoMesAnterior = Math.Round(SubsidioEfectivoSueldoMesAnterior, 6)

            If SubsidioEfectivoVacaciones > 0 And SubsidioEfectivoSueldoMesAnterior > 0 Then

                DiferenciaSubsidioImpuestoVacaciones = SubsidioEfectivoVacaciones - SubsidioEfectivoSueldoMesAnterior

                If DiferenciaSubsidioImpuestoVacaciones < 0 Then
                    DiferenciaSubsidioImpuestoVacaciones = DiferenciaSubsidioImpuestoVacaciones * -1
                End If

                FactorImpuestoSubsidioVacaciones = DiferenciaSubsidioImpuestoVacaciones / ImporteProporcionalVacaciones
                TasaSubsidioImpuestoVacaciones = FactorImpuestoSubsidioVacaciones * 1 ' El 1 representa 100%
                SubsidioVacaciones = ImporteGravadoVacaciones * TasaSubsidioImpuestoVacaciones
            Else
                DiferenciaSubsidioImpuestoVacaciones = ImpuestoVacaciones - ImpuestoSueldoMesAnterior

                FactorImpuestoSubsidioVacaciones = DiferenciaSubsidioImpuestoVacaciones / ImporteProporcionalVacaciones
                TasaSubsidioImpuestoVacaciones = FactorImpuestoSubsidioVacaciones * 1 ' El 1 representa 100%
                ImpuestoVacaciones = ImporteGravadoVacaciones * TasaSubsidioImpuestoVacaciones
            End If

            '/*------------------ Cambios 29/01/2018 ------------------*/
            'If ImporteGravadoAguinaldo > 0 Then

            '    Dim ImporteProporcionalAguinaldo As Decimal

            '    Call ConsultarDiasMes()

            '    ImporteProporcionalAguinaldo = (ImporteGravadoAguinaldo / 365) * 30.4
            '    IngresoMensualOrdinario = (ImporteDiario * DiasMes)
            '    IngresoGravadoMes = ImporteProporcionalAguinaldo + IngresoMensualOrdinario

            '    '1 Impuesto / Subsidio proporcional aguinaldo
            '    Call CalcularImpuestoAguinaldo(IngresoGravadoMes)
            '    Call CalcularSubsidioAguinaldo(IngresoGravadoMes)

            '    If ImpuestoAguinaldo > SubsidioIncidencias Then
            '        SubsidioEfectivoAguinaldo = 0
            '        ImpuestoAguinaldo = ImpuestoAguinaldo - SubsidioIncidencias
            '    ElseIf ImpuestoAguinaldo < SubsidioIncidencias Then
            '        SubsidioEfectivoAguinaldo = SubsidioIncidencias - ImpuestoAguinaldo
            '        ImpuestoAguinaldo = 0
            '    End If

            '    ImpuestoAguinaldo = Math.Round(ImpuestoAguinaldo, 6)
            '    SubsidioEfectivoAguinaldo = Math.Round(SubsidioEfectivoAguinaldo, 6)

            '    If SubsidioEfectivoAguinaldo > 0 And SubsidioEfectivoSueldoMesAnterior > 0 Then
            '        DiferenciaSubsidioImpuestoAguinaldo = SubsidioEfectivoAguinaldo - SubsidioEfectivoSueldoMesAnterior
            '        If DiferenciaSubsidioImpuestoAguinaldo < 0 Then
            '            DiferenciaSubsidioImpuestoAguinaldo = DiferenciaSubsidioImpuestoAguinaldo * -1
            '        End If

            '        FactorImpuestoSubsidioAguinaldo = DiferenciaSubsidioImpuestoAguinaldo / ImporteProporcionalAguinaldo
            '        TasaSubsidioImpuestoAguinaldo = FactorImpuestoSubsidioAguinaldo * 1 ' El 1 representa 100%
            '        SubsidioIncidencias = ImporteGravadoAguinaldo * TasaSubsidioImpuestoAguinaldo
            '    Else
            '        DiferenciaSubsidioImpuestoAguinaldo = ImpuestoAguinaldo - ImpuestoSueldoMesAnterior

            '        FactorImpuestoSubsidioAguinaldo = DiferenciaSubsidioImpuestoAguinaldo / ImporteProporcionalAguinaldo
            '        TasaSubsidioImpuestoAguinaldo = FactorImpuestoSubsidioAguinaldo * 1 ' El 1 representa 100%
            '        ImpuestoAguinaldo = ImporteGravadoAguinaldo * TasaSubsidioImpuestoAguinaldo
            '    End If
            'End If
            '/*-------------------------*/

            '/*------------------ Reemplazo 29/01/2018 ------------------*/
            If ImporteGravadoAguinaldo > 0 Then

                Dim ImporteProporcionalAguinaldo As Decimal

                Call ConsultarDiasMes()

                ImporteProporcionalAguinaldo = (ImporteGravadoAguinaldo / 365) * 30.4
                IngresoMensualOrdinario = (ImporteDiario * DiasMes)
                IngresoGravadoMes = ImporteProporcionalAguinaldo + IngresoMensualOrdinario

                '1 Impuesto / Subsidio proporcional aguinaldo
                Call CalcularImpuestoAguinaldo(IngresoGravadoMes)
                Call CalcularSubsidioAguinaldo(IngresoGravadoMes)

                If ImpuestoAguinaldo > SubsidioAguinaldo Then
                    SubsidioEfectivoAguinaldo = 0
                    ImpuestoAguinaldo = ImpuestoAguinaldo - SubsidioAguinaldo
                ElseIf ImpuestoAguinaldo < SubsidioAguinaldo Then
                    SubsidioEfectivoAguinaldo = SubsidioAguinaldo - ImpuestoAguinaldo
                    ImpuestoAguinaldo = 0
                End If

                ImpuestoAguinaldo = Math.Round(ImpuestoAguinaldo, 6)
                SubsidioEfectivoAguinaldo = Math.Round(SubsidioEfectivoAguinaldo, 6)

                If SubsidioEfectivoAguinaldo > 0 And SubsidioEfectivoSueldoMesAnterior > 0 Then
                    DiferenciaSubsidioImpuestoAguinaldo = SubsidioEfectivoAguinaldo - SubsidioEfectivoSueldoMesAnterior
                    If DiferenciaSubsidioImpuestoAguinaldo < 0 Then
                        DiferenciaSubsidioImpuestoAguinaldo = DiferenciaSubsidioImpuestoAguinaldo * -1
                    End If

                    FactorImpuestoSubsidioAguinaldo = DiferenciaSubsidioImpuestoAguinaldo / ImporteProporcionalAguinaldo
                    TasaSubsidioImpuestoAguinaldo = FactorImpuestoSubsidioAguinaldo * 1 ' El 1 representa 100%
                    SubsidioAguinaldo = ImporteGravadoAguinaldo * TasaSubsidioImpuestoAguinaldo
                Else
                    DiferenciaSubsidioImpuestoAguinaldo = ImpuestoAguinaldo - ImpuestoSueldoMesAnterior

                    FactorImpuestoSubsidioAguinaldo = DiferenciaSubsidioImpuestoAguinaldo / ImporteProporcionalAguinaldo
                    TasaSubsidioImpuestoAguinaldo = FactorImpuestoSubsidioAguinaldo * 1 ' El 1 representa 100%
                    ImpuestoAguinaldo = ImporteGravadoAguinaldo * TasaSubsidioImpuestoAguinaldo
                End If
            End If
            '/*-------------------------*/

            If ImpuestoVacaciones > 0 And ImpuestoAguinaldo > 0 Then
                Impuesto = Math.Round(ImpuestoVacaciones + ImpuestoAguinaldo, 6)
            End If

            If SubsidioEfectivoVacaciones > 0 And SubsidioEfectivoAguinaldo > 0 Then
                SubsidioEfectivo = Math.Round(SubsidioEfectivoVacaciones + SubsidioEfectivoAguinaldo, 6)
            End If

            Call ChecarPercepcionesGravadas(cmbConcepto.SelectedValue)

            If ImporteGravado > 0 Then

                Dim ImporteProporcionalIncidencias As Decimal

                Call ConsultarDiasMes()

                ImporteProporcionalIncidencias = (ImporteGravado / 365) * 30.4
                IngresoMensualOrdinario = (ImporteDiario * DiasMes)
                IngresoGravadoMes = ImporteProporcionalIncidencias + IngresoMensualOrdinario

                '1 Impuesto / Subsidio proporcional incidencias
                Call CalcularImpuestoIncidencias(IngresoGravadoMes)
                Call CalcularSubsidioIncidencias(IngresoGravadoMes)

                If ImpuestoIncidencias > SubsidioIncidencias Then
                    SubsidioEfectivoIncidencias = 0
                    ImpuestoIncidencias = ImpuestoIncidencias - SubsidioIncidencias
                ElseIf ImpuestoIncidencias < SubsidioIncidencias Then
                    SubsidioEfectivoIncidencias = SubsidioIncidencias - ImpuestoIncidencias
                    ImpuestoIncidencias = 0
                End If

                ImpuestoIncidencias = Math.Round(ImpuestoIncidencias, 6)
                SubsidioEfectivoIncidencias = Math.Round(SubsidioEfectivoIncidencias, 6)

                If SubsidioEfectivoIncidencias > 0 And SubsidioEfectivoSueldoMesAnterior > 0 Then
                    DiferenciaSubsidioImpuestoIncidencias = SubsidioEfectivoIncidencias - SubsidioEfectivoSueldoMesAnterior
                    If DiferenciaSubsidioImpuestoIncidencias < 0 Then
                        DiferenciaSubsidioImpuestoIncidencias = DiferenciaSubsidioImpuestoIncidencias * -1
                    End If

                    FactorImpuestoSubsidioIncidencias = DiferenciaSubsidioImpuestoIncidencias / ImporteProporcionalIncidencias
                    TasaSubsidioImpuestoIncidencias = FactorImpuestoSubsidioIncidencias * 1 ' El 1 representa 100%
                    SubsidioIncidencias = ImporteGravado * TasaSubsidioImpuestoIncidencias
                Else
                    DiferenciaSubsidioImpuestoIncidencias = ImpuestoIncidencias - ImpuestoSueldoMesAnterior

                    FactorImpuestoSubsidioIncidencias = DiferenciaSubsidioImpuestoIncidencias / ImporteProporcionalIncidencias
                    TasaSubsidioImpuestoIncidencias = FactorImpuestoSubsidioIncidencias * 1 ' El 1 representa 100%
                    ImpuestoIncidencias = ImporteGravado * TasaSubsidioImpuestoIncidencias
                End If
            End If
        End If

        Impuesto = Impuesto + ImpuestoIncidencias
        SubsidioEfectivo = SubsidioEfectivo + SubsidioIncidencias

        If NumeroConcepto = 2 Then
            Call EliminaConceptosFiniquito(Request("id"), 56) 'Imss
            Call EliminaConceptosFiniquito(Request("id"), 64) 'Infonavit
        End If

        Call GuardarRegistro(2)
        Call ChecarYGrabarPercepcionesExentasYGravadas(empleadoId.Value, 0)
        Call MostrarPercepciones()
        Call MostrarDeducciones()
        Call ChecarPercepcionesExentasYGravadasFiniquito()

        txtGravadoISR.Text = Math.Round(PercepcionesGravadas, 6)
        txtExentoISR.Text = Math.Round(PercepcionesExentas, 6)

    End Sub
    Private Sub ChecarSiExistenDiasEnPercepciones(ByVal NoEmpleado As Int64, ByVal CvoConcepto As Int32)
        Try
            Dim CuotaPeriodo As Decimal = 0
            Dim PrimaDominical As Decimal = 0
            Dim PrimaVacacional As Decimal = 0
            Dim Vacaciones As Decimal = 0
            Dim Aguinaldo As Decimal = 0
            Dim RepartoUtilidades As Decimal = 0
            Dim FondoAhorro As Decimal = 0
            Dim AyudaFuneral As Decimal = 0
            Dim PrevisionSocial As Decimal = 0
            Dim GrupoPercepcionesGravadasTotalmenteSinExentos As Decimal = 0
            Dim PagoPorHoras As Decimal = 0
            Dim Comisiones As Decimal = 0
            Dim Destajo As Decimal = 0
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
                'cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 3 'Quincenal
                cNomina.Tipo = "F"
                cNomina.TipoConcepto = "P"
                cNomina.NoEmpleado = empleadoId.Value
                cNomina.IdMovimiento = Request("id")
                dt = cNomina.ConsultarConceptosFiniquito()
            ElseIf Agregar = 0 Then
                'cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 3 'Quincenal
                cNomina.Tipo = "F"
                cNomina.TipoConcepto = "P"
                cNomina.NoEmpleado = empleadoId.Value
                cNomina.IdMovimiento = Request("id")
                cNomina.DiferenteDe = 1
                cNomina.CvoConcepto = CvoConcepto
                dt = cNomina.ConsultarConceptosFiniquito()
            Else
                'cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 3 'Quincenal
                cNomina.Tipo = "F"
                cNomina.TipoConcepto = "P"
                cNomina.NoEmpleado = empleadoId.Value
                cNomina.IdMovimiento = Request("id")
                dt = cNomina.ConsultarConceptosFiniquito()
            End If

            ' PercepcionesGravadas
            If dt.Rows.Count > 0 Then
                If dt.Compute("Sum(Importe)", "CvoConcepto=85") IsNot DBNull.Value Then
                    If Agregar <> 3 Then
                        DiasCuotaPeriodo = dt.Compute("Sum(Unidad)", "CvoConcepto=85")
                        CuotaPeriodo = dt.Compute("Sum(Importe)", "CvoConcepto=85")
                    ElseIf Agregar = 3 Then
                        'Try
                        '    DiasCuotaPeriodo = Convert.ToDecimal(txtDias.Text)
                        'Catch ex As Exception
                        '    DiasCuotaPeriodo = 0
                        'End Try
                        'CuotaPeriodo = DiasCuotaPeriodo * CuotaDiaria
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
                If dt.Compute("Sum(Importe)", "CvoConcepto=7 OR CvoConcepto=8") IsNot DBNull.Value Then
                    TiempoExtraordinarioFueraDelMargenLegal = dt.Compute("Sum(Importe)", "CvoConcepto=7 OR CvoConcepto=8")
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
                '33,34,35,36,37,38,40,41,43,45,46,47,48
                If dt.Compute("Sum(Importe)", "CvoConcepto=33 OR CvoConcepto=34 OR CvoConcepto=35 OR CvoConcepto=36 OR CvoConcepto=37 OR CvoConcepto=38 OR CvoConcepto=40 OR CvoConcepto=41 OR CvoConcepto=43 OR CvoConcepto=45 OR CvoConcepto=46 OR CvoConcepto=47 OR CvoConcepto=48") IsNot DBNull.Value Then
                    PrevisionSocial = dt.Compute("Sum(Importe)", "CvoConcepto=6 OR CvoConcepto=9 OR CvoConcepto=10")
                End If
                '12,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,39,49
                If dt.Compute("Sum(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=18 OR CvoConcepto=19 OR CvoConcepto=20 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=28 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=32 OR CvoConcepto=39 OR CvoConcepto=49") IsNot DBNull.Value Then
                    GrupoPercepcionesGravadasTotalmenteSinExentos = dt.Compute("Sum(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=18 OR CvoConcepto=19 OR CvoConcepto=20 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=28 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=32 OR CvoConcepto=39 OR CvoConcepto=49")
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
                ElseIf CvoConcepto.ToString = "33" Or CvoConcepto.ToString = "34" Or CvoConcepto.ToString = "35" Or CvoConcepto.ToString = "36" Or CvoConcepto.ToString = "37" Or CvoConcepto.ToString = "38" Or CvoConcepto.ToString = "40" Or CvoConcepto.ToString = "41" Or CvoConcepto.ToString = "43" Or CvoConcepto.ToString = "45" Or CvoConcepto.ToString = "46" Or CvoConcepto.ToString = "47" Or CvoConcepto.ToString = "48" Then
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
    Private Sub GridDeduciones_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridDeduciones.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                conceptoId.Value = e.CommandArgument
                rwConfirmEliminaConcepto.RadConfirm("¿Está seguro de eliminar este concepto?", "confirmCallbackFnEliminaConcepto", 330, 180, Nothing, "Confirmar")
        End Select
    End Sub
    Private Sub GridDeduciones_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridDeduciones.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim btnEliminar As ImageButton = CType(e.Item.FindControl("btnEliminar"), ImageButton)
            Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim CvoConcepto As Integer = item.OwnerTableView.DataKeyValues(item.ItemIndex)("CvoConcepto")
            If CvoConcepto = 14 Or CvoConcepto = 15 Or CvoConcepto = 16 Or CvoConcepto = 52 Or CvoConcepto = 54 Or CvoConcepto = 55 Or CvoConcepto = 56 Then
                btnEliminar.Visible = False
            End If
        End If
    End Sub
    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Call EliminaConceptosFiniquito(Request("id"), 0)
        Response.Redirect("~/FiniquitosQuincenal.aspx", False)
    End Sub
    Private Sub btnGenerarFiniquito_Click(sender As Object, e As EventArgs) Handles btnGenerarFiniquito.Click
        If cmbPeriodo.SelectedValue > 0 Then
            If cmbTipoFiniquito.SelectedValue > 0 Then
                rwConfirmarTimbraFiniquito.RadConfirm("¿Está seguro que desea timbrar el cálculo de finiquito?", "confirmCallbackFnTimbraFiniquito", 330, 180, Nothing, "Confirmar")
            Else
                rwAlerta.RadAlert("Seleccione un tipo de finiquito.", 330, 180, "Alerta", "", "")
            End If
        Else
            rwAlerta.RadAlert("Seleccione un periodo.", 330, 180, "Alerta", "", "")
        End If
    End Sub
    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Call EliminaConceptosFiniquito(Request("id"), 0)
        Response.Redirect("~/FiniquitosQuincenal.aspx", False)
    End Sub
    Private Sub btnConfirmarTimbraFiniquito_Click(sender As Object, e As EventArgs) Handles btnConfirmarTimbraFiniquito.Click
        Try
            Call CargarVariablesGenerales()
            'Dim FilePath1 = Server.MapPath("~/PDF/F/ST/") & String.Format("{0:00}", IdEmpresa) & "F" & String.Format("{0:00}", empleadoId.Value) & ".pdf"
            Dim FilePath1 = Server.MapPath("~/PDF/F/ST/") & "F" & String.Format("{0:00}", empleadoId.Value) & ".pdf"
            Call GuardaPDF(GeneraPDFFiniquito(), FilePath1)
            'Dim FilePath2 = Server.MapPath("~/PDF/F/ST/") & String.Format("{0:00}", IdEmpresa) & "R" & String.Format("{0:00}", empleadoId.Value) & ".pdf"
            Dim FilePath2 = Server.MapPath("~/PDF/F/ST/") & "R" & String.Format("{0:00}", empleadoId.Value) & ".pdf"
            Call GuardaPDF(GeneraPDFRenuncia(), FilePath2)

            btnDescargarPDFFiniquito.Visible = True
            btnDescargarPDFRenuncia.Visible = True
            btnGenerarFiniquito.Visible = False
            GrabarEstatusFiniquito(1)
        Catch oExcep As Exception
            GrabarEstatusFiniquito(0)
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Public Sub CrearCFDNomina(ByVal NoEmpleado As Integer, ByVal RutaXML As String)
        Dim Comprobante As XmlNode
        urlnomina = 0

        Call CargarVariablesGenerales()

        Dim dt As New DataTable
        Dim cEmpleado As New Entities.Empleado
        cEmpleado.IdEmpleado = NoEmpleado
        'cEmpleado.IdEmpresa = IdEmpresa
        cEmpleado.IdMovimiento = Request("id")
        dt = cEmpleado.ConsultarEmpleados()

        Dim MetodoPago As String = ""
        If dt.Rows.Count > 0 Then
            MetodoPago = dt.Rows(0)("CLAVEMETODOPAGO").ToString
        End If

        m_xmlDOM = CrearDOM()
        Comprobante = CrearNodoComprobante(MetodoPago)

        m_xmlDOM.AppendChild(Comprobante)

        IndentarNodo(Comprobante, 1)

        CrearNodoEmisor(Comprobante)
        IndentarNodo(Comprobante, 1)

        CrearNodoReceptor(Comprobante, NoEmpleado)
        IndentarNodo(Comprobante, 1)

        '***** Conceptos del Recibo de Nomina  ***
        CrearNodoConceptos(Comprobante)
        IndentarNodo(Comprobante, 1)

        '**** Atributos de los Impuestos  ****
        CrearNodoImpuestos(Comprobante)
        IndentarNodo(Comprobante, 1)

        CrearNodoComplemento(Comprobante, NoEmpleado)
        IndentarNodo(Comprobante, 0)

        Dim path As String = Server.MapPath("~/Certificado/") & "CSD01_AAA010101AAA.cer"

        SellarCFD(Comprobante, path)
        m_xmlDOM.InnerXml = (Replace(m_xmlDOM.InnerXml, "schemaLocation", "xsi:schemaLocation", , , CompareMethod.Text))
        m_xmlDOM.Save(RutaXML)
    End Sub
    Private Function CrearDOM() As XmlDocument
        Dim oDOM As New XmlDocument
        Dim Nodo As XmlNode
        Nodo = oDOM.CreateProcessingInstruction("xml", "version=""1.0"" encoding=""utf-8""")
        oDOM.AppendChild(Nodo)
        Nodo = Nothing
        CrearDOM = oDOM
    End Function
    Private Sub CrearAtributosComprobante(ByVal Nodo As XmlElement, ByVal metodoDePago As String)
        Dim LugarExpedicion As String = ""
        Dim dt As New DataTable
        Dim cNomina = New Nomina()
        dt = cNomina.ConsultarLugarExpedicion()

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                LugarExpedicion = oDataRow("LugarExpedicion")
            Next
        End If
        Nodo.SetAttribute("xmlns:nomina", "http://www.sat.gob.mx/nomina")
        Nodo.SetAttribute("xmlns:cfdi", "http://www.sat.gob.mx/cfd/3")
        Nodo.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
        Nodo.SetAttribute("xsi:schemaLocation", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd http://www.sat.gob.mx/nomina http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina11.xsd")
        Nodo.SetAttribute("certificado", "")
        Nodo.SetAttribute("fecha", Format(Now(), "yyyy-MM-ddTHH:mm:ss"))
        Nodo.SetAttribute("folio", FolioXml)
        Nodo.SetAttribute("formaDePago", "PAGO EN UNA SOLA EXHIBICION")
        'Nodo.SetAttribute("NumCtaPago", "Noaplica")
        Nodo.SetAttribute("metodoDePago", metodoDePago)
        Nodo.SetAttribute("noCertificado", "")
        Nodo.SetAttribute("sello", "")
        'Nodo.SetAttribute("subTotal", (TotalPercepciones + Math.Abs(SubsidioAplicado) + Math.Abs(SubsidioEfectivo))) 'suma grav y exento  de percepciones=======================================================
        Nodo.SetAttribute("subTotal", TotalPercepciones)
        'totalpercepciones + totaldeducciones - impuesto
        'Nodo.SetAttribute("descuento", (CuotaImss + AdeudoPatron)) 'suma deducciones sin isr===================================================================
        'Dim Descuento2 As Decimal = 0
        'Descuento2 = TotalDeducciones - (Math.Abs(SubsidioAplicado) + Math.Abs(SubsidioEfectivo) + Impuesto)
        'Nodo.SetAttribute("descuento", TotalDeducciones - (Math.Abs(SubsidioAplicado) + Math.Abs(SubsidioEfectivo) + Impuesto))
        Nodo.SetAttribute("descuento", TotalDeducciones - Impuesto)
        'Nodo.SetAttribute("descuento", Descuento2)
        Nodo.SetAttribute("motivoDescuento", "DEDUCCIONES NOMINA")
        Nodo.SetAttribute("tipoDeComprobante", "egreso")
        'total deducciones = seguro social  + adeudos diversos+ impuesto
        'Nodo.SetAttribute("total", ((TotalPercepciones + Math.Abs(SubsidioAplicado) + Math.Abs(SubsidioEfectivo))) - (CuotaImss + AdeudoPatron + Impuesto))  'gravado + exento de pércepciones -  gravado y exentos de deducciones, neto a recibir========
        'Nodo.SetAttribute("total", ((TotalPercepciones + Math.Abs(SubsidioAplicado) + Math.Abs(SubsidioEfectivo))) - (TotalDeducciones - Math.Abs(SubsidioAplicado) - Math.Abs(SubsidioEfectivo)))
        Nodo.SetAttribute("total", TotalPercepciones - TotalDeducciones)
        'total de percepcines + subsidio aplic + subsidio efec - deducciones 
        Nodo.SetAttribute("LugarExpedicion", LugarExpedicion)
        Nodo.SetAttribute("Moneda", "M.N.")
        'Nodo.setAttribute "serie", "1"
        Nodo.SetAttribute("version", "3.2")
    End Sub
    Private Function CrearNodoComprobante(ByVal metodoDePago As String) As XmlNode
        Dim Comprobante As XmlElement
        Comprobante = m_xmlDOM.CreateElement("cfdi:Comprobante", URI_SAT)
        CrearAtributosComprobante(Comprobante, metodoDePago)
        CrearNodoComprobante = Comprobante
    End Function
    Private Sub IndentarNodo(ByVal Nodo As XmlNode, ByVal Nivel As Long)
        Nodo.AppendChild(m_xmlDOM.CreateTextNode(vbNewLine & New String(ControlChars.Tab, Nivel)))
    End Sub
    Private Sub CrearNodoEmisor(ByVal Nodo As XmlNode)

        Dim dtEmisor As New DataTable
        Dim cNomina = New Nomina()
        cNomina.Id = Session("clienteid")
        dtEmisor = cNomina.ConsultarDatosEmisor()

        Dim Emisor As XmlElement
        Dim DomFiscal As XmlElement
        Dim ExpedidoEn As XmlElement
        Dim RegimenFiscal As XmlElement

        If dtEmisor.Rows.Count > 0 Then
            For Each oDataRow In dtEmisor.Rows
                Emisor = CrearNodo("cfdi:Emisor")
                Emisor.SetAttribute("nombre", oDataRow("RazonSocial"))
                Emisor.SetAttribute("rfc", oDataRow("RFC"))
                'Emisor.SetAttribute("rfc", "AAA010101AAA")

                IndentarNodo(Emisor, 2)

                DomFiscal = CrearNodo("cfdi:DomicilioFiscal")
                DomFiscal.SetAttribute("calle", oDataRow("Calle"))
                If oDataRow("NoExterior").Length > 0 Then
                    DomFiscal.SetAttribute("noExterior", oDataRow("NoExterior"))
                End If
                If oDataRow("NoInterior").Length > 0 Then
                    DomFiscal.SetAttribute("noInterior", oDataRow("NoInterior"))
                End If
                DomFiscal.SetAttribute("colonia", oDataRow("Colonia"))
                DomFiscal.SetAttribute("codigoPostal", oDataRow("CodigoPostal"))
                DomFiscal.SetAttribute("estado", oDataRow("Estado"))
                DomFiscal.SetAttribute("municipio", oDataRow("Municipio"))
                DomFiscal.SetAttribute("pais", "México")
                Emisor.AppendChild(DomFiscal)

                ExpedidoEn = CrearNodo("cfdi:ExpedidoEn")
                ExpedidoEn.SetAttribute("calle", oDataRow("Calle"))
                If oDataRow("NoExterior").Length > 0 Then
                    ExpedidoEn.SetAttribute("noExterior", oDataRow("NoExterior"))
                End If
                If oDataRow("NoInterior").Length > 0 Then
                    ExpedidoEn.SetAttribute("noInterior", oDataRow("NoInterior"))
                End If
                ExpedidoEn.SetAttribute("colonia", oDataRow("Colonia"))
                ExpedidoEn.SetAttribute("codigoPostal", oDataRow("CodigoPostal"))
                ExpedidoEn.SetAttribute("estado", oDataRow("Estado"))
                ExpedidoEn.SetAttribute("municipio", oDataRow("Municipio"))
                ExpedidoEn.SetAttribute("pais", "México")
                Emisor.AppendChild(ExpedidoEn)

                IndentarNodo(Emisor, 1)

                RegimenFiscal = CrearNodo("cfdi:RegimenFiscal")
                RegimenFiscal.SetAttribute("Regimen", oDataRow("Regimen"))
                Emisor.AppendChild(RegimenFiscal)
            Next
        End If

        IndentarNodo(Emisor, 2)

        Nodo.AppendChild(Emisor)

    End Sub
    Private Sub CrearNodoReceptor(ByVal Nodo As XmlNode, ByVal NoEmpleado As Integer)

        Call CargarVariablesGenerales()

        Dim dtReceptor As New DataTable
        Dim cEmpleado As New Entities.Empleado
        cEmpleado.IdEmpleado = NoEmpleado
        'cEmpleado.IdEmpresa = IdEmpresa
        cEmpleado.IdMovimiento = Request("id")
        dtReceptor = cEmpleado.ConsultarEmpleados()

        Dim Receptor As XmlElement
        Dim Domicilio As XmlElement

        If dtReceptor.Rows.Count > 0 Then
            For Each oDataRow In dtReceptor.Rows
                Receptor = CrearNodo("cfdi:Receptor")
                Receptor.SetAttribute("nombre", oDataRow("NOMBRE"))
                Receptor.SetAttribute("rfc", oDataRow("RFC"))
                IndentarNodo(Receptor, 2)

                Domicilio = CrearNodo("cfdi:Domicilio")
                Domicilio.SetAttribute("calle", oDataRow("CALLE"))
                If oDataRow("NOEXT").Length > 0 Then
                    Domicilio.SetAttribute("noExterior", oDataRow("NOEXT"))
                End If
                If oDataRow("NOINT").Length > 0 Then
                    Domicilio.SetAttribute("noInterior", oDataRow("NOINT"))
                End If
                Domicilio.SetAttribute("colonia", oDataRow("COLONIA"))
                Domicilio.SetAttribute("codigoPostal", oDataRow("CP"))
                Domicilio.SetAttribute("municipio", oDataRow("MUNICIPIO"))
                Domicilio.SetAttribute("estado", oDataRow("ESTADO"))
                Domicilio.SetAttribute("pais", oDataRow("PAIS"))
                Receptor.AppendChild(Domicilio)
            Next
        End If

        IndentarNodo(Receptor, 1)

        Nodo.AppendChild(Receptor)
    End Sub
    Private Function CrearNodo(ByVal nombre As String)
        If urlnomina = 0 Then
            CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, nombre, URI_SAT)
        ElseIf urlnomina = 1 Then
            CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, nombre, "http://www.sat.gob.mx/nomina")
        ElseIf urlnomina = 2 Then
            CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, nombre, "")
        End If
    End Function
    Private Sub CrearNodoConceptos(ByVal Nodo As XmlNode)
        Dim Conceptos As XmlElement
        Dim Concepto As XmlElement

        Conceptos = CrearNodo("cfdi:Conceptos")
        IndentarNodo(Conceptos, 2)

        Concepto = CrearNodo("cfdi:Concepto")
        'Concepto.SetAttribute("importe", TotalPercepciones + Math.Abs(SubsidioAplicado) + Math.Abs(SubsidioEfectivo))
        Concepto.SetAttribute("importe", TotalPercepciones)
        'aqui va el subtotal
        'Concepto.SetAttribute("valorUnitario", TotalPercepciones + Math.Abs(SubsidioAplicado) + Math.Abs(SubsidioEfectivo))
        Concepto.SetAttribute("valorUnitario", TotalPercepciones)
        'aqui tambien va el subtotal
        Concepto.SetAttribute("descripcion", "PAGO DE NOMINA")
        'Concepto.SetAttribute("noIdentificacion", Mid(FolioXml, 4, 2)) '=====================================================================
        Concepto.SetAttribute("unidad", "SERVICIO")
        Concepto.SetAttribute("cantidad", "1")

        Conceptos.AppendChild(Concepto)
        IndentarNodo(Conceptos, 2)
        Nodo.AppendChild(Conceptos)
    End Sub
    Private Sub CrearNodoImpuestos(ByVal Nodo As XmlNode)

        Dim Impuestos As XmlElement
        Dim Retenciones As XmlElement
        Dim Retencion As XmlElement

        Impuestos = CrearNodo("cfdi:Impuestos")
        Impuestos.SetAttribute("totalImpuestosRetenidos", Impuesto)
        IndentarNodo(Impuestos, 2)
        Retenciones = CrearNodo("cfdi:Retenciones")
        Retencion = CrearNodo("cfdi:Retencion")
        Retencion.SetAttribute("importe", Impuesto) 'ISR GRAVADO=============================================================================
        ' el impuesto - el subsidio es igual al retenido 
        Retencion.SetAttribute("impuesto", "ISR")

        Retenciones.AppendChild(Retencion)

        Impuestos.AppendChild(Retenciones)
        IndentarNodo(Impuestos, 2)
        Nodo.AppendChild(Impuestos)
    End Sub
    Private Sub CrearNodoComplemento(ByVal Nodo As XmlNode, ByVal NoEmpleado As Integer)

        Dim FechaPago As Date = Now.Date()
        Call CargarVariablesGenerales()

        Dim cPeriodo As New Entities.Periodo()
        cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
        cPeriodo.ConsultarPeriodoID()

        Dim dtEmpleado As New DataTable
        Dim cEmpleado As New Entities.Empleado
        cEmpleado.IdEmpleado = NoEmpleado
        'cEmpleado.IdEmpresa = IdEmpresa
        cEmpleado.IdMovimiento = Request("id")
        dtEmpleado = cEmpleado.ConsultarEmpleados()

        Dim complemento As XmlElement
        Dim Nomina As XmlElement
        Dim Percepciones As XmlElement
        Dim Percepcion As XmlElement
        Dim Deducciones As XmlElement
        Dim Deduccion As XmlElement
        Dim Incapacidades As XmlElement
        Dim Incapacidad As XmlElement
        Dim HorasExtras As XmlElement
        Dim HorasExtra As XmlElement

        If dtEmpleado.Rows.Count > 0 Then
            For Each oDataRow In dtEmpleado.Rows
                complemento = CrearNodo("cfdi:Complemento")
                IndentarNodo(complemento, 2)

                urlnomina = 1
                Nomina = CrearNodo("nomina:Nomina")
                IndentarNodo(Nomina, 2)

                Nomina.SetAttribute("xsi:schemaLocation", "http://www.sat.gob.mx/nomina  http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina11.xsd")
                Nomina.SetAttribute("Version", "1.1")
                Nomina.SetAttribute("RegistroPatronal", oDataRow("REGISTROPATRONAL"))
                Nomina.SetAttribute("NumEmpleado", oDataRow("NOEMPLEADO"))
                Nomina.SetAttribute("CURP", oDataRow("CURP"))
                Nomina.SetAttribute("TipoRegimen", oDataRow("CLAVEREGIMENCONTRATACION"))
                Nomina.SetAttribute("NumSeguridadSocial", oDataRow("IMSS"))
                Nomina.SetAttribute("FechaPago", Mid(FechaPago, 7, 4) + "-" + Mid(FechaPago, 4, 2) + "-" + Mid(FechaPago, 1, 2))
                Nomina.SetAttribute("FechaInicialPago", Format(CDate(cPeriodo.FechaInicial), "yyyy-MM-dd"))
                Nomina.SetAttribute("FechaFinalPago", Format(CDate(cPeriodo.FechaFinal), "yyyy-MM-dd"))
                Nomina.SetAttribute("NumDiasPagados", NumeroDeDiasPagados)
                'atributos opcionales
                Nomina.SetAttribute("Departamento", oDataRow("DEPARTAMENTO"))
                If oDataRow("CLAVEMETODOPAGO") <> "1" Then
                    If oDataRow("CLABE").ToString.Length > 0 Then
                        Nomina.SetAttribute("CLABE", oDataRow("CLABE"))
                    End If
                    'String.Format("{0:00}", oDataRow("NOEMPLEADO").ToString)
                    'String.Format("{0:000000}", Convert.ToInt32(TxtFactura.Text))
                    'Nomina.SetAttribute("Banco", String.Format("{0:000}", oDataRowTrabajador("NOBANCO").ToString))
                    Nomina.SetAttribute("Banco", oDataRow("NOBANCO"))
                End If
                Nomina.SetAttribute("FechaInicioRelLaboral", Format(CDate(oDataRow("FECHAINGRESO")), "yyyy-MM-dd"))
                Nomina.SetAttribute("Antiguedad", DateDiff("ww", CDate(oDataRow("FECHAINGRESO")), Now()))
                Nomina.SetAttribute("Puesto", oDataRow("PUESTO"))
                Nomina.SetAttribute("TipoContrato", oDataRow("TIPODECONTRATO"))
                Nomina.SetAttribute("TipoJornada", oDataRow("TIPODEJORNADA"))
                Nomina.SetAttribute("PeriodicidadPago", "Quincenal")
                Nomina.SetAttribute("SalarioBaseCotApor", oDataRow("CUOTADIARIA"))
                Nomina.SetAttribute("RiesgoPuesto", oDataRow("CLAVERIESGO"))
                Nomina.SetAttribute("SalarioDiarioIntegrado", oDataRow("INTEGRADOIMSS"))

                Percepciones = CrearNodo("nomina:Percepciones")
                Percepciones.SetAttribute("TotalGravado", PercepcionesGravadas)
                'Es la suma de percepciones gravadas - faltas, incapacidades y permisos
                'Percepciones.SetAttribute("TotalExento", PercepcionesExentas + Math.Abs(SubsidioAplicado) + Math.Abs(SubsidioEfectivo))
                Percepciones.SetAttribute("TotalExento", PercepcionesExentas + Math.Abs(SubsidioEfectivo))
                'Suma de percepciones exentas  + subsidio aplicado + subsidio efectivo
                Nomina.AppendChild(Percepciones)
                IndentarNodo(Percepciones, 1)

                Dim dt As New DataTable
                Dim cNomina As New Nomina()
                'cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 3 'Quincenal
                cNomina.Tipo = "F"
                cNomina.TipoConcepto = "P"
                cNomina.NoEmpleado = empleadoId.Value
                cNomina.IdMovimiento = Request("id")
                dt = cNomina.ConsultarPercepcionesDeduccionesFiniquito()
                cNomina = Nothing

                If dt.Rows.Count > 0 Then
                    Dim oDataRowpercepciones As DataRow
                    For Each oDataRowpercepciones In dt.Rows
                        ObtenerUnidad(NoEmpleado, oDataRowpercepciones("CvoConcepto").ToString)
                        Percepcion = CrearNodo("nomina:Percepcion")
                        Percepcion.SetAttribute("Clave", String.Format("{0:000}", oDataRowpercepciones("CvoConcepto")) + Unidad.ToString)
                        Percepcion.SetAttribute("Concepto", oDataRowpercepciones("Concepto").ToString)
                        Percepcion.SetAttribute("ImporteGravado", oDataRowpercepciones("ImporteGravado").ToString)
                        Percepcion.SetAttribute("ImporteExento", oDataRowpercepciones("ImporteExento").ToString)
                        Percepcion.SetAttribute("TipoPercepcion", String.Format("{0:000}", Convert.ToInt32(oDataRowpercepciones("CvoSAT"))))
                        Percepciones.AppendChild(Percepcion)
                        IndentarNodo(Percepciones, 4)
                        Percepcion = Nothing
                    Next
                End If

                Nomina.AppendChild(Percepciones)
                IndentarNodo(Nomina, 2)

                'Nomina.appendChild percepciones
                'Complemento.appendChild Nomina
                'Nodo.appendChild Complemento

                Deducciones = CrearNodo("nomina:Deducciones")
                'Deducciones.SetAttribute("TotalExento", TotalDeducciones - Math.Abs(SubsidioAplicado) - Math.Abs(SubsidioEfectivo))
                Deducciones.SetAttribute("TotalExento", Math.Round(TotalDeducciones, 6))
                'Va el total de deducciones
                Deducciones.SetAttribute("TotalGravado", "0.00")
                Nomina.AppendChild(Deducciones)
                IndentarNodo(Deducciones, 4)

                dt = New DataTable
                cNomina = New Nomina()
                'cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 3 'Quincenal
                cNomina.Tipo = "F"
                cNomina.TipoConcepto = "D"
                cNomina.NoEmpleado = empleadoId.Value
                cNomina.IdMovimiento = Request("id")
                dt = cNomina.ConsultarPercepcionesDeduccionesFiniquito()
                cNomina = Nothing

                If dt.Rows.Count > 0 Then
                    Dim oDataRowDeducciones As DataRow
                    For Each oDataRowDeducciones In dt.Rows
                        ObtenerUnidad(NoEmpleado, oDataRowDeducciones("CvoConcepto").ToString)
                        Deduccion = CrearNodo("nomina:Deduccion")
                        Deduccion.SetAttribute("Clave", String.Format("{0:000}", oDataRowDeducciones("CvoConcepto")) + Unidad.ToString)
                        Deduccion.SetAttribute("Concepto", oDataRowDeducciones("Concepto").ToString)
                        Deduccion.SetAttribute("ImporteGravado", oDataRowDeducciones("ImporteGravado").ToString)
                        Deduccion.SetAttribute("ImporteExento", oDataRowDeducciones("ImporteExento").ToString)
                        Deduccion.SetAttribute("TipoDeduccion", String.Format("{0:000}", Convert.ToInt32(oDataRowDeducciones("CvoSAT"))))
                        Deducciones.AppendChild(Deduccion)
                        IndentarNodo(Deducciones, 4)
                        Deduccion = Nothing
                    Next
                End If

                Nomina.AppendChild(Deducciones)

                IndentarNodo(Nomina, 1)
                Nodo.AppendChild(Nomina)

                complemento.AppendChild(Nomina)
                IndentarNodo(complemento, 1)
                urlnomina = 0

                IndentarNodo(complemento, 1)
                Nodo.AppendChild(complemento)

            Next
        End If
    End Sub
    Private Sub ObtenerUnidad(ByVal NoEmpleado As Integer, ByVal CvoConcepto As String)
        Try

            Call CargarVariablesGenerales()

            Dim dt As New DataTable
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Tipo = "F"
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.IdMovimiento = Request("id")
            cNomina.CvoConcepto = CvoConcepto
            dt = cNomina.ConsultarConceptosFiniquito()

            If dt.Rows.Count > 0 Then
                Unidad = dt.Rows(0).Item("Unidad").ToString
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub SellarCFD(ByVal NodoComprobante As XmlElement, ByVal Certificado As String)
        Dim objCert As New X509Certificate2()
        'Pasarle el nombre y ruta del Cerfificado para obtener la información en bytes
        Dim bRawData As Byte() = ReadFile(Certificado)
        objCert.Import(bRawData)
        Dim cadena As String = Convert.ToBase64String(bRawData)
        'Comentando las dos lineas siguientes no agrega el certificado al comprobante xml
        NodoComprobante.SetAttribute("noCertificado", FormatearSerieCert(objCert.SerialNumber))
        NodoComprobante.SetAttribute("certificado", Convert.ToBase64String(bRawData))
        'Comentando la siguiente linea no agregar el sello al comprobante xml
        NodoComprobante.SetAttribute("sello", GenerarSello())
    End Sub
    Function ReadFile(ByVal strArchivo As String) As Byte()
        Dim f As New FileStream(strArchivo, FileMode.Open, FileAccess.Read)
        Dim size As Integer = CInt(f.Length)
        Dim data As Byte() = New Byte(size - 1) {}
        size = f.Read(data, 0, size)
        f.Close()
        Return data
    End Function
    Public Function FormatearSerieCert(ByVal Serie As String) As String
        Dim Resultado As String = ""
        Dim I As Integer
        For I = 2 To Len(Serie) Step 2
            Resultado = Resultado & Mid(Serie, I, 1)
        Next
        FormatearSerieCert = Resultado
    End Function
    Private Function GenerarSello() As String
        Dim ArchivoPFX As String = Server.MapPath("~/PKI/") & "CSD01_AAA010101AAA.pfx"
        Dim objCertPfx As New X509Certificate2(ArchivoPFX, "12345678a")
        Dim lRSA As RSACryptoServiceProvider = objCertPfx.PrivateKey
        Dim lhasher As New SHA1CryptoServiceProvider()
        Dim bytesFirmados As Byte() = lRSA.SignData(System.Text.Encoding.UTF8.GetBytes(GetCadenaOriginal(m_xmlDOM.InnerXml)), lhasher)
        Return Convert.ToBase64String(bytesFirmados)
    End Function
    Public Function GetCadenaOriginal(ByVal xmlCFD As String) As String
        Dim xslt As New Xsl.XslCompiledTransform
        Dim xmldoc As New XmlDocument
        Dim navigator As XPath.XPathNavigator
        Dim output As New IO.StringWriter
        xmldoc.LoadXml(xmlCFD)
        navigator = xmldoc.CreateNavigator()
        xslt.Load(Server.MapPath("~/SAT/") & "cadenaoriginal_3_2.xslt")
        xslt.Transform(navigator, Nothing, output)
        GetCadenaOriginal = output.ToString
    End Function
    Private Function FileToMemory(ByVal Filename As String) As IO.MemoryStream
        Dim FS As New System.IO.FileStream(Filename, IO.FileMode.Open)
        Dim MS As New System.IO.MemoryStream
        Dim BA(FS.Length - 1) As Byte
        FS.Read(BA, 0, BA.Length)
        FS.Close()
        MS.Write(BA, 0, BA.Length)
        Return MS
    End Function
    Public Function Num2Text(ByVal nCifra As Object) As String
        ' Defino variables 
        Dim cifra, bloque, decimales, cadena As String
        Dim longituid, posision, unidadmil As Byte

        ' En caso de que unidadmil sea: 
        ' 0 = cientos 
        ' 1 = miles 
        ' 2 = millones 
        ' 3 = miles de millones 
        ' 4 = billones 
        ' 5 = miles de billones 

        ' Reemplazo el símbolo decimal por un punto (.) y luego guardo la parte entera y la decimal por separado 
        ' Es necesario poner el cero a la izquierda del punto así si el valor es de sólo decimales, se lo fuerza 
        ' a colocar el cero para que no genere error 
        cifra = Format(CType(nCifra, Decimal), "###############0.#0")
        decimales = Mid(cifra, Len(cifra) - 1, 2)
        cifra = Left(cifra, Len(cifra) - 3)

        ' Verifico que el valor no sea cero 
        If cifra = "0" Then
            Return IIf(decimales = "00", "cero", "cero con " & decimales & "/100")
        End If

        ' Evaluo su longitud (como mínimo una cadena debe tener 3 dígitos) 
        If Len(cifra) < 3 Then
            cifra = Rellenar(cifra, 3)
        End If

        ' Invierto la cadena 
        cifra = Invertir(cifra)

        ' Inicializo variables 
        posision = 1
        unidadmil = 0
        cadena = ""

        ' Selecciono bloques de a tres cifras empezando desde el final (de la cadena invertida) 
        Do While posision <= Len(cifra)
            ' Selecciono una porción del numero 
            bloque = Mid(cifra, posision, 3)

            ' Transformo el número a cadena 
            cadena = Convertir(bloque, unidadmil) & " " & cadena.Trim

            ' Incremento la cantidad desde donde seleccionar la subcadena 
            posision = posision + 3

            ' Incremento la posisión de la unidad de mil 
            unidadmil = unidadmil + 1
        Loop

        ' Cargo la función 
        Return IIf(decimales = "00", cadena.Trim.ToLower, cadena.Trim.ToLower & " con " & decimales & "/100")
    End Function
    Public Function Rellenar(ByVal valor As Object, ByVal cifras As Byte) As String
        ' Defino variables 
        Dim cadena As String

        ' Verifico el valor pasado 
        If Not IsNumeric(valor) Then
            valor = 0
        Else
            valor = CType(valor, Integer)
        End If

        ' Cargo la cadena 
        cadena = valor.ToString.Trim

        ' Relleno con los ceros que sean necesarios para llenar los dígitos pedidos 
        For puntero As Byte = (Len(cadena) + 1) To cifras
            cadena = "0" & cadena
        Next puntero

        ' Cargo la función 
        Return cadena
    End Function
    Public Function Invertir(ByVal cadena As String) As String
        ' Defino variables 
        Dim retornar As String

        ' Inviero la cadena 
        For posision As Short = cadena.Length To 1 Step -1
            retornar = retornar & cadena.Substring(posision - 1, 1)
        Next

        ' Retorno la cadena invertida
        Return retornar
    End Function
    Private Function Convertir(ByVal cadena As String, ByVal unidadmil As Byte) As String
        ' Defino variables 
        Dim centena, decena, unidad As Byte

        ' Invierto la subcadena (la original habia sido invertida en el procedimiento NumeroATexto) 
        cadena = Invertir(cadena)

        ' Determino la longitud de la cadena 
        If Len(cadena) < 3 Then
            cadena = Rellenar(cadena, 3)
        End If

        ' Verifico que la cadena no esté vacía (000) 
        If cadena = "000" Then
            Return ""
        End If

        ' Desarmo el numero (empiezo del dígito cero por el manejo de cadenas de VB.NET) 
        centena = CType(cadena.Substring(0, 1), Byte)
        decena = CType(cadena.Substring(1, 1), Byte)
        unidad = CType(cadena.Substring(2, 1), Byte)
        cadena = ""

        ' Calculo las centenas 
        If centena <> 0 Then
            Dim centenas() As String = {"", IIf(decena = 0 And unidad = 0, "cien", "ciento"), "doscientos", "trescientos", "cuatrocientos", "quinientos", "seiscientos", "setecientos", "ochocientos", "novecientos"}
            cadena = centenas(centena)
        End If

        ' Calculo las decenas 
        If decena <> 0 Then
            Dim decenas() As String = {"", IIf(unidad = 0, "diez", IIf(unidad >= 6, "dieci", IIf(unidad = 1, "once", IIf(unidad = 2, "doce", IIf(unidad = 3, "trece", IIf(unidad = 4, "catorce", "quince")))))), IIf(unidad = 0, "veinte", "venti"), "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa"}
            cadena = cadena & " " & decenas(decena)
        End If

        ' Calculo las unidades (no pregunten por que este IF es necesario ... simplemente funciona) 
        If decena = 1 And unidad < 6 Then
        Else
            Dim unidades() As String = {"", IIf(decena <> 1, IIf(unidadmil = 1, "un", "uno"), ""), "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve"}
            If decena >= 3 And unidad <> 0 Then
                cadena = cadena.Trim & " y "
            End If

            If decena = 0 Then
                cadena = cadena.Trim & " "
            End If
            cadena = cadena & unidades(unidad)
        End If

        ' Evaluo la posision de miles, millones, etc 
        If unidadmil <> 0 Then
            Dim agregado() As String = {"", "mil", IIf((centena = 0) And (decena = 0) And (unidad = 1), "millón", "millones"), "mil millones", "billones", "mil billones"}
            If (centena = 0) And (decena = 0) And (unidad = 1) And unidadmil = 2 Then
                cadena = "un"
            End If
            cadena = cadena & " " & agregado(unidadmil)
        End If

        ' Cargo la función 
        Return cadena.Trim
    End Function
    Private Function GeneraPDFFiniquito() As Telerik.Reporting.Report
        Dim reporte As New Formatos.formato_finiquito
        Dim CantidadTexto As String = ""
        Dim RazonSocial As String = ""
        Dim Municipio As String = ""
        Dim Estado As String = ""
        Dim NombreEmpleado As String = ""
        Dim Puesto As String = ""
        Dim FechaIngreso As String = ""
        Dim FechaBaja As String = ""

        Dim DiasLaborados As String = ""
        Dim DiasLaboradosAnio As String = ""
        Dim DiasVacacionesProporcionales As String = ""
        Dim ProporcionalVacaciones As String = ""
        Dim PorcentajePrimaVacacional As String = ""
        Dim PrimaVacaciones As String = ""
        Dim DiasAguinaldoAnio As String = ""
        Dim ProporcionalAguinaldo As String = ""
        Dim DiasPendientesPago As Decimal = 0
        Dim OtrasPercepcionesPendientes As Decimal = 0
        Dim SubsidioEmpleo As Decimal = 0
        Dim CuotasIMSS As Decimal = 0
        Dim ImpuestoISR As Decimal = 0

        Dim dt As New DataTable()
        Dim cNomina = New Nomina()
        cNomina.Id = Session("clienteid")
        dt = cNomina.ConsultarDatosEmisor()

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                RazonSocial = oDataRow("RazonSocial")
                Municipio = oDataRow("Municipio")
                Estado = oDataRow("Estado")
            Next
        End If

        Dim dtEmpleado As New DataTable
        Dim cEmpleado As New Entities.Empleado
        cEmpleado.IdEmpleado = empleadoId.Value
        'cEmpleado.IdEmpresa = IdEmpresa
        cEmpleado.IdMovimiento = Request("id")
        dtEmpleado = cEmpleado.ConsultarEmpleados()

        If dtEmpleado.Rows.Count > 0 Then
            For Each oDataRow In dtEmpleado.Rows
                NombreEmpleado = oDataRow("nombre")
                Puesto = oDataRow("puesto")
            Next
        End If

        'cNomina.IdEmpresa = IdEmpresa
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.Id = Request("id")
        dt = cNomina.ConsultarEmpleadosFiniquito()

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                FechaIngreso = oDataRow("FechaIngreso").ToString
                FechaBaja = oDataRow("FechaBaja").ToString
                CuotaDiaria = FormatCurrency(oDataRow("CuotaDiaria"), 2)
                SalarioDiarioIntegradoTrabajador = oDataRow("IntegradoIMSS")
            Next
        End If

        Dim DiasPagadosVacaciones As Integer = 0
        Try
            DiasPagadosVacaciones = Convert.ToInt32(txtDiasPagadosVacaciones.Text)
        Catch ex As Exception
            DiasPagadosVacaciones = 0
        End Try

        cNomina.Id = Request("id")
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.DiasPagadosVacaciones = DiasPagadosVacaciones
        dt = cNomina.ConsultarDesgloseFiniquito()

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                DiasLaborados = oDataRow("DiasLaborados").ToString
                DiasLaboradosAnio = oDataRow("DiasLaboradosAnio").ToString
                DiasVacacionesProporcionales = Math.Round(oDataRow("DiasProporcionalVacaciones"), 2).ToString
                ProporcionalVacaciones = FormatCurrency(oDataRow("ProporcionalVacaciones"), 2).ToString
                PorcentajePrimaVacacional = oDataRow("PorcentajePrimaVacacional").ToString
                PrimaVacaciones = FormatCurrency(oDataRow("PrimaVacaciones"), 2).ToString
                DiasAguinaldoAnio = oDataRow("DiasAguinaldoAnio").ToString
                ProporcionalAguinaldo = FormatCurrency(oDataRow("ProporcionalAguinaldo"), 2).ToString
            Next
        End If

        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.Tipo = "F"
        cNomina.TipoConcepto = "P"
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.IdMovimiento = Request("id")
        dt = cNomina.ConsultarPercepcionesDeduccionesFiniquito()

        If dt.Rows.Count > 0 Then
            If dt.Compute("Sum(Unidad)", "CvoConcepto=85 OR CvoConcepto=51") IsNot DBNull.Value Then
                DiasPendientesPago = dt.Compute("Sum(Unidad)", "CvoConcepto=85 OR CvoConcepto=51")
            End If
            If dt.Compute("Sum(Importe)", "CvoConcepto=55") IsNot DBNull.Value Then
                SubsidioEmpleo = dt.Compute("Sum(Importe)", "CvoConcepto=55")
            End If
            If dt.Compute("Sum(Importe)", "CvoConcepto<>2 AND CvoConcepto<>14 AND CvoConcepto<>15 AND CvoConcepto<>16 AND CvoConcepto<>55") IsNot DBNull.Value Then
                OtrasPercepcionesPendientes = dt.Compute("Sum(Importe)", "CvoConcepto<>2 AND CvoConcepto<>14 AND CvoConcepto<>15 AND CvoConcepto<>16 AND CvoConcepto<>55")
            End If
        End If

        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.Tipo = "F"
        cNomina.TipoConcepto = "D"
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.IdMovimiento = Request("id")
        dt = cNomina.ConsultarPercepcionesDeduccionesFiniquito()

        If dt.Rows.Count > 0 Then
            If dt.Compute("Sum(Importe)", "CvoConcepto=86") IsNot DBNull.Value Then
                ImpuestoISR = dt.Compute("Sum(Importe)", "CvoConcepto=86")
            End If
            If dt.Compute("Sum(Importe)", "CvoConcepto=56") IsNot DBNull.Value Then
                CuotasIMSS = dt.Compute("Sum(Importe)", "CvoConcepto=56")
            End If
        End If

        TotalPercepciones = 0
        TotalDeducciones = 0
        NetoAPagar = 0
        PercepcionesGravadas = 0
        PercepcionesExentas = 0

        Call MostrarPercepciones()
        Call MostrarDeducciones()
        NetoAPagar = (TotalPercepciones - TotalDeducciones)

        Dim largo = Len(CStr(Format(CDbl(NetoAPagar), "#,###.00")))
        Dim decimales = Mid(CStr(Format(CDbl(NetoAPagar), "#,###.00")), largo - 2)
        CantidadTexto = "( " + Num2Text(NetoAPagar - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"

        reporte.ReportParameters("LugarExpedicion").Value = Municipio.ToUpper & " " & Estado.ToUpper & ", " & "a " & CDate(FechaBaja).Day.ToString & " de " & MonthName(CDate(FechaBaja).Month).ToString & " de " & CDate(FechaBaja).Year.ToString
        reporte.ReportParameters("ImporteNeto").Value = FormatCurrency(NetoAPagar, 2).ToString
        reporte.ReportParameters("Texto1").Value = "Recibí de " & RazonSocial.ToUpper & " la cantidad de " & CantidadTexto.ToUpper.ToString & " por concepto de saldo finiquito por el trabajo prestado a esta empresa."
        reporte.ReportParameters("Texto2").Value = "Así mismo manifiesto que por así convenir a mis intereses renuncio voluntariamente y de manera irrevocable al puesto de " & Puesto & " que venía desempeñando en esta empresa; además hago constar que no se me adeuda ninguna cantidad por concepto de salarios, horas extras, bonos, comisiones, premios, séptimos dias, vacaciones, prima vacacional, accidentes o enfermedades profesionales, prima de antigüedad, reparto de utilidades, aguinaldo, ni por otro concepto nacido de la Ley o de mi contrato de trabajo, motivo por el cual la libero de toda responsabilidad, otorgandole el mas amplio finiquito y manifiesto que no me reservo ninguna acción o derecho en contra de " & RazonSocial.ToUpper & " o quien resulte responsable o propietario de la fuente de trabajo."
        reporte.ReportParameters("FechaAlta").Value = FechaIngreso
        reporte.ReportParameters("FechaBaja").Value = FechaBaja
        reporte.ReportParameters("SueldoDiario").Value = FormatCurrency(CuotaDiaria, 2).ToString
        reporte.ReportParameters("SueldoDiarioIntegrado").Value = FormatCurrency(SalarioDiarioIntegradoTrabajador, 2).ToString
        reporte.ReportParameters("DiasPendientesPago").Value = DiasPendientesPago
        reporte.ReportParameters("OtrasPercepcionesPendientes").Value = FormatCurrency(OtrasPercepcionesPendientes, 2).ToString
        reporte.ReportParameters("DiasLaborados").Value = DiasLaboradosAnio
        reporte.ReportParameters("AnosAntiguedadIndemnizacion").Value = 1
        reporte.ReportParameters("DiasVacacionesProporcionales").Value = DiasVacacionesProporcionales
        reporte.ReportParameters("VacacionesProporcionales").Value = ProporcionalVacaciones
        reporte.ReportParameters("PorcentajePrimaVacacional").Value = 25
        reporte.ReportParameters("PrimaVacacional").Value = PrimaVacaciones
        reporte.ReportParameters("DiasAguinaldoAnio").Value = 15
        reporte.ReportParameters("AguinaldoProporcional").Value = ProporcionalAguinaldo

        reporte.ReportParameters("TotalPercepciones").Value = FormatCurrency(TotalPercepciones - SubsidioEmpleo, 2).ToString
        reporte.ReportParameters("ImpuestoRetener").Value = FormatCurrency(ImpuestoISR, 2).ToString
        reporte.ReportParameters("SubsidioEmpleo").Value = FormatCurrency(SubsidioEmpleo, 2).ToString
        reporte.ReportParameters("CuotasIMSS").Value = FormatCurrency(CuotasIMSS, 2).ToString
        reporte.ReportParameters("TotalDeducciones").Value = FormatCurrency(TotalDeducciones, 2).ToString
        reporte.ReportParameters("NetoPagar").Value = FormatCurrency(NetoAPagar, 2).ToString
        reporte.ReportParameters("NombreEmpleado").Value = NombreEmpleado

        Return reporte

    End Function
    Private Function GeneraPDFRenuncia() As Telerik.Reporting.Report
        Dim reporte As New Formatos.formato_renuncia
        Dim FechaBaja As String = ""
        Dim CantidadTexto As String = ""
        Dim RazonSocial As String = ""
        Dim Municipio As String = ""
        Dim Estado As String = ""
        Dim NombreEmpleado As String = ""
        Dim Puesto As String = ""

        Dim dt As New DataTable()
        Dim cNomina = New Nomina()
        cNomina.Id = Session("clienteid")
        dt = cNomina.ConsultarDatosEmisor()

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                RazonSocial = oDataRow("RazonSocial")
                Municipio = oDataRow("Municipio")
                Estado = oDataRow("Estado")
            Next
        End If

        Dim dtEmpleado As New DataTable
        Dim cEmpleado As New Entities.Empleado
        cEmpleado.IdEmpleado = empleadoId.Value
        'cEmpleado.IdEmpresa = IdEmpresa
        cEmpleado.IdMovimiento = Request("id")
        dtEmpleado = cEmpleado.ConsultarEmpleados()

        If dtEmpleado.Rows.Count > 0 Then
            For Each oDataRow In dtEmpleado.Rows
                NombreEmpleado = oDataRow("nombre")
                Puesto = oDataRow("puesto")
            Next
        End If

        Dim DiasPagadosVacaciones As Integer = 0
        Try
            DiasPagadosVacaciones = Convert.ToInt32(txtDiasPagadosVacaciones.Text)
        Catch ex As Exception
            DiasPagadosVacaciones = 0
        End Try

        cNomina = New Nomina()
        cNomina.Id = Request("id")
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.DiasPagadosVacaciones = DiasPagadosVacaciones
        dt = cNomina.ConsultarDesgloseFiniquito()

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                FechaBaja = oDataRow("FechaBaja").ToString
            Next
        End If

        reporte.ReportParameters("LugarExpedicion").Value = Municipio.ToUpper & " " & Estado.ToUpper & ", " & "a " & CDate(FechaBaja).Day.ToString & " de " & MonthName(CDate(FechaBaja).Month).ToString & " de " & CDate(FechaBaja).Year.ToString
        reporte.ReportParameters("RazonSocial").Value = RazonSocial.ToUpper
        reporte.ReportParameters("Texto1").Value = "Por medio de la presente manifiesto que por así convenir a mis intereses renuncio voluntariamente y de manera irrevocable al puesto de " & Puesto & " que venía desempeñandome en esta empresa; así mismo hago constar que no se me adeuda ninguna cantidad por concepto de salarios, horas extras, bonos, comisiones, premios, séptimos dias, vacaciones, prima vacacional, accidentes o enfermedades profesionales, prima de antigüedad, reparto de utilidades, aguinaldo, ni por otro concepto nacido de la Ley o de mi contrato de trabajo, motivo por el cual la libero de toda responsabilidad, otorgandole el mas amplio finiquito y manifiesto que no me reservo ninguna acción o derecho en contra de " & RazonSocial.ToUpper & " o quien resulte responsable o propietario de la fuente de trabajo."
        reporte.ReportParameters("Texto2").Value = "Agradezco el apoyo y atención que siempre me brindo esta empresa."
        reporte.ReportParameters("NombreEmpleado").Value = NombreEmpleado

        Return reporte
    End Function
    Private Sub GuardaPDF(ByVal report As Telerik.Reporting.Report, ByVal fileName As String)
        Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
        Dim result As RenderingResult = reportProcessor.RenderReport("PDF", report, Nothing)
        Using fs As New FileStream(fileName, FileMode.Create)
            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length)
        End Using
    End Sub
    Private Sub GrabarEstatusFiniquito(ByVal IdEstatus As Integer)
        Call CargarVariablesGenerales()

        Dim cNomina = New Nomina()
        cNomina.Tipo = "F"
        cNomina.IdMovimiento = Request("id")
        cNomina.NoEmpleado = empleadoId.Value
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.IdEstatus = IdEstatus
        cNomina.ActualizarEstatusFiniquitoGenerado()
    End Sub
    Private Sub btnDescargarPDFFiniquito_Click(sender As Object, e As EventArgs) Handles btnDescargarPDFFiniquito.Click

        Call CargarVariablesGenerales()

        'Dim FilePath = Server.MapPath("~/PDF/F/ST/") & String.Format("{0:00}", IdEmpresa) & "F" & String.Format("{0:00}", empleadoId.Value) & ".pdf"
        Dim FilePath = Server.MapPath("~/PDF/F/ST/") & "F" & String.Format("{0:00}", empleadoId.Value) & ".pdf"
        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        End If
    End Sub
    Private Sub btnDescargarPDFRenuncia_Click(sender As Object, e As EventArgs) Handles btnDescargarPDFRenuncia.Click

        Call CargarVariablesGenerales()

        'Dim FilePath = Server.MapPath("~/PDF/F/ST/") & String.Format("{0:00}", IdEmpresa) & "R" & String.Format("{0:00}", empleadoId.Value) & ".pdf"
        Dim FilePath = Server.MapPath("~/PDF/F/ST/") & "R" & String.Format("{0:00}", empleadoId.Value) & ".pdf"
        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        End If
    End Sub

End Class