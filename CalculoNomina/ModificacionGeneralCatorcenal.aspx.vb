Imports Telerik.Web.UI
Imports Entities
Public Class ModificacionGeneralCatorcenal
    Inherits System.Web.UI.Page

    Private IdEmpresa As Integer = 0
    Private IdEjercicio As Integer = 0
    Private IdPeriodo As Integer = 0
    Private MesAcumula As Integer = 0
    Private FinMesBit As Boolean = False

    'Private CuotaPeriodo As Double
    Private HorasTriples As Double
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

    Private ImporteExentoTiempoExtraordinarioDentroDelMargenLegal As Double
    Private ImporteGravadoTiempoExtraordinarioDentroDelMargenLegal As Double
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

    Private Diferencias As Double
    Private Gratificacion As Double
    Private Bonificacion As Double
    Private Retroactivo As Double
    Private BonoProduccion As Double
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
    Private AyudaCarestia As Double
    Private DespensaEfectivo As Double
    Private HaberPorRetiro As Double

    Private HorasDoblesGravadas As Double
    Private HorasDoblesExentas As Double
    Private DobleteGravado As Double
    Private DobleteExento As Double

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
    Private ImpuestoMes As Double
    Private Subsidio As Double
    Private SubsidioMes As Double
    Private SubsidioAplicado As Double
    Private SubsidioEfectivo As Double
    Private SalarioMinimoDiarioGeneral As Double
    Private ImporteExento As Double
    Private ImporteGravado As Double
    Private TiempoExtraordinarioDentroDelMargenLegal As Double
    Private TiempoExtraordinarioFueraDelMargenLegal As Double
    Private Agregar As String
    Private DiasVacaciones As Integer
    Private DiasCuotaPeriodo As Integer
    Private DiasHonorarioAsimilado As Integer
    Private DiasPagoPorHoras As Integer
    Private DiasComision As Integer
    Private DiasDestajo As Integer
    Private DiasFaltasPermisosIncapacidades As Integer
    Private HonorarioAsimilado As Double

    Private SalarioMinimoIntegradoGeneral As Double
    Private SalarioDiarioIntegradoTrabajador As Double
    Private IMSS As Double
    Private ImporteSeguroVivienda As Double

    Private PercepcionesExentas As Double
    Private PercepcionesGravadas As Double

    Private UMA As Double
    Private UMAMensual As Double
    Private UMI As Double
    'Private CuotaDiaria As Decimal

    Private BaseGravableMensualSubsidio As Double
    Private BaseGravableMensualSubsidioDiario As Double
    Private BaseGravableMensualSubsidioSemanal As Double
    Private FactorSubsidio As Double
    Private SubsidioMensual As Double
    Private SubsidioDiario As Double
    Private ImporteDiarioGravado As Double
    Private FactorDiarioPromedio As Double

    Private dtEmpleados As New DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request("id")) Then

                periodoId.Value = Request.QueryString("id").ToString
                clienteId.Value = Request.QueryString("cid").ToString

                If Session("Folio") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Session("Folio").ToString()) Then
                    Me.nominaID.Value = Integer.Parse(Session("Folio").ToString())
                End If

                Call CargarDatos()

            End If
        End If
    End Sub
    Private Sub CargarDatos()
        Try
            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = clienteId.Value
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            dt = cNomina.ConsultarDatosGeneralesNomina()
            cNomina = Nothing
            If dt.Rows.Count > 0 Then
                For Each oDataRow In dt.Rows
                    Me.lblTitulo.Text = oDataRow("RangoFecha")
                    Me.lblNoNomina.Text = Session("Folio").ToString
                    Me.lblEjercicio.Text = oDataRow("Ejercicio")
                    Me.lblRazonSocial.Text = oDataRow("Cliente")
                    Me.lblNoPeriodo.Text = oDataRow("Periodo")
                    Me.lblTipoNomina.Text = "Catorcenal"
                    Me.lblFechaInicial.Text = oDataRow("FechaInicial")
                    Me.lblFechaFinal.Text = oDataRow("FechaFinal")
                    Me.lblDias.Text = oDataRow("Dias")
                Next
                Call CargarGridEmpleados()
            Else
                Me.lblTitulo.Text = ""
                Me.lblNoNomina.Text = ""
                Me.lblEjercicio.Text = ""
                Me.lblRazonSocial.Text = ""
                Me.lblNoPeriodo.Text = ""
                Me.lblTipoNomina.Text = ""
                Me.lblFechaInicial.Text = ""
                Me.lblFechaFinal.Text = ""
                Me.lblDias.Text = ""
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cConfiguracion = New Configuracion()
        cConfiguracion.IdEmpresa = Session("IdEmpresa")
        cConfiguracion.IdUsuario = Session("usuarioid")
        dt = cConfiguracion.ConsultarConfiguracion()
        cConfiguracion = Nothing

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                IdEmpresa = oDataRow("IdEmpresa")
                IdEjercicio = oDataRow("IdEjercicio")
                IdPeriodo = oDataRow("IdPeriodo")
                SalarioMinimoDiarioGeneral = oDataRow("SalarioMinimoDiarioGeneral")
                ImporteSeguroVivienda = oDataRow("ImporteSeguroVivienda")
                BaseGravableMensualSubsidio = oDataRow("BaseGravableMensualSubsidio")
                FactorSubsidio = oDataRow("FactorSubsidio")
                FactorDiarioPromedio = oDataRow("FactorDiarioPromedio")
                UMA = oDataRow("UMA")
                FinMesBit = CBool(oDataRow("FinMesBit"))
                MesAcumula = oDataRow("MesAcumula")
            Next
        End If
    End Sub
    Private Sub CargarGridEmpleados()

        CargarVariablesGenerales()

        Dim cNomina As New Nomina()
        cNomina.IdNomina = nominaID.Value
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = clienteId.Value
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = periodoId.Value
        cNomina.EsEspecial = False
        dtEmpleados = cNomina.ConsultarDetalleNominaExtraordinaria()
        grdEmpleadosCatorcenal.DataSource = dtEmpleados
        grdEmpleadosCatorcenal.DataBind()
        cNomina = Nothing
    End Sub
    Private Sub grdEmpleadosCatorcenal_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles grdEmpleadosCatorcenal.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim lnkEditar As ImageButton = DirectCast(e.Item.FindControl("lnkEditar"), ImageButton)
            lnkEditar.Attributes.Add("onclick", "javascript: OpenWindow('" & periodoId.Value.ToString & "','" & item.GetDataKeyValue("NoEmpleado").ToString & "','" & item.GetDataKeyValue("IdContrato").ToString & "','" & item.GetDataKeyValue("idNomina").ToString & "'); return false;")

            Dim cell As TableCell = DirectCast(item("EstatusContrato"), TableCell)

            If item("EstatusContrato").Text = "Activo" Then
                cell.ForeColor = Drawing.Color.Green
                cell.Font.Bold = True
            Else
                cell.ForeColor = Drawing.Color.Red
                cell.Font.Bold = True
            End If

        End If
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Footer
                If dtEmpleados.Rows.Count > 0 Then
                    If Not IsDBNull(dtEmpleados.Compute("sum(INFONAVIT)", "")) Then
                        e.Item.Cells(5).Text = FormatCurrency(dtEmpleados.Compute("sum(INFONAVIT)", ""), 2).ToString
                        e.Item.Cells(5).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(5).Font.Bold = True
                    End If
                    If Not IsDBNull(dtEmpleados.Compute("sum(Faltas)", "")) Then
                        e.Item.Cells(6).Text = FormatNumber(dtEmpleados.Compute("sum(Faltas)", ""), 2).ToString
                        e.Item.Cells(6).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(6).Font.Bold = True
                    End If
                    If Not IsDBNull(dtEmpleados.Compute("sum(IncapacidadEG)", "")) Then
                        e.Item.Cells(7).Text = FormatNumber(dtEmpleados.Compute("sum(IncapacidadEG)", ""), 2).ToString
                        e.Item.Cells(7).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(7).Font.Bold = True
                    End If
                    If Not IsDBNull(dtEmpleados.Compute("sum(IncapacidadRT)", "")) Then
                        e.Item.Cells(8).Text = FormatNumber(dtEmpleados.Compute("sum(IncapacidadRT)", ""), 2).ToString
                        e.Item.Cells(8).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(8).Font.Bold = True
                    End If
                    If Not IsDBNull(dtEmpleados.Compute("sum(IncapacidadMaterna)", "")) Then
                        e.Item.Cells(9).Text = FormatNumber(dtEmpleados.Compute("sum(IncapacidadMaterna)", ""), 2).ToString
                        e.Item.Cells(9).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(9).Font.Bold = True
                    End If
                End If
        End Select
    End Sub
    Private Sub grdEmpleadosCatorcenal_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdEmpleadosCatorcenal.NeedDataSource

        CargarVariablesGenerales()

        Dim cNomina As New Nomina()
        cNomina.IdNomina = nominaID.Value
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = clienteId.Value
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = periodoId.Value
        cNomina.EsEspecial = False
        dtEmpleados = cNomina.ConsultarDetalleNominaExtraordinaria()
        grdEmpleadosCatorcenal.DataSource = dtEmpleados
        cNomina = Nothing
    End Sub
    Private Function ChecarSiExiste(ByVal NoEmpleado As Integer, ByVal CvoConcepto As Int32) As Boolean
        Try

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = clienteId.Value
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

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Function
    Private Function ChecarQueExistaLaCuotaPeriodo(ByVal NoEmpleado As Int64, ByVal ImporteIncidencia As Decimal, ByVal UnidadIncidencia As Decimal) As Boolean
        Try

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = clienteId.Value
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
    Private Sub ChecarSiExistenDiasEnPercepciones(ByVal NoEmpleado As Int64, ByVal CvoConcepto As Int32, ByVal ImporteIncidencia As Decimal, ByVal UnidadIncidencia As Decimal)
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
            'TiempoExtraordinarioFueraDelMargenLegal = 0

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()

            If Agregar = 1 Then
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = clienteId.Value
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = periodoId.Value
                cNomina.NoEmpleado = NoEmpleado
                cNomina.TipoConcepto = "P"
                cNomina.Tipo = "N"
                dt = cNomina.ConsultarConceptosEmpleado()
            ElseIf Agregar = 0 Then
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = clienteId.Value
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = periodoId.Value
                cNomina.NoEmpleado = NoEmpleado
                cNomina.TipoConcepto = "P"
                cNomina.DiferenteDe = 1
                cNomina.CvoConcepto = CvoConcepto
                cNomina.Tipo = "N"
                dt = cNomina.ConsultarConceptosEmpleado()
            Else
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = clienteId.Value
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = periodoId.Value
                cNomina.NoEmpleado = NoEmpleado
                cNomina.TipoConcepto = "P"
                cNomina.Tipo = "N"
                dt = cNomina.ConsultarConceptosEmpleado()
            End If

            ' PercepcionesGravadas
            If dt.Rows.Count > 0 Then
                If dt.Compute("Sum(Importe)", "CvoConcepto=2") IsNot DBNull.Value Then
                    DiasCuotaPeriodo = dt.Compute("Sum(Unidad)", "CvoConcepto=2")
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

                'Dim ImporteIncidencia As Decimal = 0
                'Dim UnidadIncidencia As Decimal = 0
                'Try
                '    ImporteIncidencia = Convert.ToDecimal(txtImporteIncidencia.Text)
                'Catch ex As Exception
                '    ImporteIncidencia = 0
                'End Try

                'Try
                '    UnidadIncidencia = Convert.ToDecimal(txtUnidadIncidencia.Text)
                'Catch ex As Exception
                '    UnidadIncidencia = 0
                'End Try

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
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = clienteId.Value
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 86
            cNomina.TipoConcepto = "D"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=54 AND TIPOCONCEPTO='P'"
            cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = clienteId.Value
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 54
            cNomina.TipoConcepto = "P"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=55 AND TIPOCONCEPTO='P'"
            cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = clienteId.Value
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 55
            cNomina.TipoConcepto = "P"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=56 AND TIPOCONCEPTO='D'"
            cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = clienteId.Value
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 56
            cNomina.TipoConcepto = "D"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=108 AND TIPOCONCEPTO='DE'"
            cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = clienteId.Value
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 108
            cNomina.TipoConcepto = "DE"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=109 AND TIPOCONCEPTO='DE'"
            cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = clienteId.Value
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 109
            cNomina.TipoConcepto = "DE"
            cNomina.EliminaConceptoEmpleado()

        Catch oExcep As Exception
            MsgBox(oExcep.Message)
        End Try
    End Sub
    Private Sub ChecarYGrabarPercepcionesExentasYGravadas(ByVal NoEmpleado As Integer, ByVal IdContrato As Integer, ByVal CuotaDiaria As Decimal)
        Dim CuotaPeriodo As Double
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

            SubsidioAplicado = 0
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
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = clienteId.Value
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado

            dt = cNomina.ConsultarConceptosEmpleado()

            If dt.Rows.Count > 0 Then
                If dt.Compute("Sum(Importe)", "CvoConcepto=2") IsNot DBNull.Value Then
                    DiasCuotaPeriodo = dt.Compute("Sum(Unidad)", "CvoConcepto=2")
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
                '12,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,39,49
                If dt.Compute("Sum(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=18 OR CvoConcepto=19 OR CvoConcepto=20 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=28 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=32 OR CvoConcepto=39 OR CvoConcepto=49") IsNot DBNull.Value Then
                    GrupoPercepcionesGravadasTotalmenteSinExentos = dt.Compute("Sum(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=18 OR CvoConcepto=19 OR CvoConcepto=20 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=28 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=32 OR CvoConcepto=39 OR CvoConcepto=49")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") IsNot DBNull.Value Then
                    DiasFaltasPermisosIncapacidades = dt.Compute("Sum(UNIDAD)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162")
                    FaltasPermisosIncapacidades = dt.Compute("Sum(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162")
                End If

                If dt.Compute("Sum(Importe)", "CvoConcepto=54") IsNot DBNull.Value Then
                    SubsidioAplicado = dt.Compute("Sum(Importe)", "CvoConcepto=54")
                End If
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
                'GuardarExentoYGravado(2, CuotaPeriodo, 0, NoEmpleado)
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
            '12,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,39,49
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
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            '''''''// Consultar SI tiene desuento de INFONAVIT //'''''''
            Dim Valor As Decimal
            Dim DescuentoInvonavit As Decimal
            Dim datos As New DataTable
            Dim Infonavit As New Entities.Infonavit()
            Infonavit.IdCliente = clienteId.Value
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

                Call QuitarConcepto(64, NoEmpleado)

                Call GuardarRegistro(CuotaDiaria, 1, DescuentoInvonavit, 1, IdContrato, NoEmpleado, 64)

            End If
        Catch oExcep As Exception
            MsgBox(oExcep.Message)
        End Try
    End Sub
    Private Sub ChecarPercepcionesGravadas(ByVal NoEmpleado As Int64, ByVal CvoConcepto As Int32, ByVal ImporteIncidencia As Decimal, ByVal UnidadIncidencia As Decimal, ByVal CuotaDiaria As Decimal)
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
            Dim FaltasPermisosIncapacidades As Double = 0
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

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = clienteId.Value
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            dt = cNomina.ConsultarConceptosEmpleado()

            'PercepcionesGravadas
            If dt.Rows.Count > 0 Then
                If dt.Compute("Sum(Importe)", "CvoConcepto=2") IsNot DBNull.Value Then
                    DiasCuotaPeriodo = dt.Compute("Sum(Unidad)", "CvoConcepto=2")
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
                    DiasHonorarioAsimilado = dt.Compute("Sum(UNIDAD)", "CvoConcepto=5") * 14
                    HonorarioAsimilado = dt.Compute("Sum(Importe)", "CvoConcepto=5")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=6 OR CvoConcepto=9 OR CvoConcepto=10") IsNot DBNull.Value Then
                    TiempoExtraordinarioDentroDelMargenLegal = dt.Compute("Sum(Importe)", "CvoConcepto=6 OR CvoConcepto=9 OR CvoConcepto=10")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=7 OR CvoConcepto=8") IsNot DBNull.Value Then
                    TiempoExtraordinarioFueraDelMargenLegal = dt.Compute("Sum(Importe)", "CvoConcepto=7 OR CvoConcepto=8")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=11") IsNot DBNull.Value Then
                    DiasPagoPorHoras = 14
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
                '12,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,39,49
                If dt.Compute("Sum(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=18 OR CvoConcepto=19 OR CvoConcepto=20 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=28 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=32 OR CvoConcepto=39 OR CvoConcepto=49") IsNot DBNull.Value Then
                    GrupoPercepcionesGravadasTotalmenteSinExentos = dt.Compute("Sum(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=18 OR CvoConcepto=19 OR CvoConcepto=20 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=28 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=32 OR CvoConcepto=39 OR CvoConcepto=49")
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
                    DiasHonorarioAsimilado = DiasCuotaPeriodo + (UnidadIncidencia * 14)
                    HonorarioAsimilado = HonorarioAsimilado + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "7" Or CvoConcepto.ToString = "8" Then
                    '7=horas extras triples y 8=festivo trabajado
                    TiempoExtraordinarioFueraDelMargenLegal = TiempoExtraordinarioFueraDelMargenLegal + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "2" Then
                    DiasCuotaPeriodo = DiasCuotaPeriodo + UnidadIncidencia
                    CuotaPeriodo = CuotaPeriodo + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "11" Then
                    DiasPagoPorHoras = 14
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
                ElseIf CvoConcepto.ToString = "57" Or CvoConcepto.ToString = "58" Or CvoConcepto.ToString = "59" Or CvoConcepto.ToString = "161" Or CvoConcepto.ToString = "162" Then
                    'deducciones
                    'ooooooooooooooooooooooooooooooooooooooooooooooo
                    ' aqui se descuenta de la cuota periodo el importe de las faltas, permisos e incapacidades
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
            'El tiempo extraordinario2 es gravado al 100%, no tiene nada exento igual que las vacaciones y la Cuota del periodo
            ImporteGravado = ImporteGravado + TiempoExtraordinarioFueraDelMargenLegal + Vacaciones + CuotaPeriodo
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
                If PrevisionSocial < (SalarioMinimoDiarioGeneral * 14) Then
                    ImporteExento = ImporteExento + PrevisionSocial
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial < ((SalarioMinimoDiarioGeneral * 14) * 14) Then
                    ImporteExento = ImporteExento + PrevisionSocial
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial > ((SalarioMinimoDiarioGeneral * 14) * 14) And ImporteGravado + ImporteExento > ((SalarioMinimoDiarioGeneral * 14) * 14) Then
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 14)
                    ImporteGravado = ImporteGravado + (PrevisionSocial - (SalarioMinimoDiarioGeneral * 14))
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial > ((SalarioMinimoDiarioGeneral * 14) * 14) And ImporteGravado + ImporteExento + (SalarioMinimoDiarioGeneral * 14) < ((SalarioMinimoDiarioGeneral * 14) * 14) Then
                    ImporteExento = ImporteExento + (((SalarioMinimoDiarioGeneral * 14) * 14) - (ImporteGravado + ImporteExento))
                    ImporteGravado = ImporteGravado + (PrevisionSocial - (((SalarioMinimoDiarioGeneral * 14) * 14) - (ImporteGravado + ImporteExento)))
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial > ((SalarioMinimoDiarioGeneral * 14) * 14) And (((SalarioMinimoDiarioGeneral * 14) * 14) - (ImporteGravado + ImporteExento) < SalarioMinimoDiarioGeneral * 14) Then
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 14)
                    ImporteGravado = ImporteGravado + (PrevisionSocial - (SalarioMinimoDiarioGeneral * 14))
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
    Private Sub QuitarConcepto(ByVal NumeroConcepto As Int32, ByVal NoEmpleado As Int32)
        Try

            Call CargarVariablesGenerales()

            Dim cNomina As New Nomina()

            If NumeroConcepto <= 51 Or NumeroConcepto = 54 Or NumeroConcepto = 82 Or NumeroConcepto = 165 Or NumeroConcepto = 167 Or NumeroConcepto = 168 Or NumeroConcepto = 169 Or NumeroConcepto = 170 Or NumeroConcepto = 171 Then
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = clienteId.Value
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = periodoId.Value
                cNomina.NoEmpleado = NoEmpleado
                cNomina.CvoConcepto = NumeroConcepto
                cNomina.TipoConcepto = "P"
                cNomina.EliminaConceptoEmpleado()
            ElseIf NumeroConcepto >= 61 And NumeroConcepto <= 87 Or NumeroConcepto = 52 Or NumeroConcepto = 56 Or NumeroConcepto = 57 Or NumeroConcepto = 58 Or NumeroConcepto = 59 Or NumeroConcepto = 161 Or NumeroConcepto = 162 Then
                'Deducciones
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = clienteId.Value
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = periodoId.Value
                cNomina.NoEmpleado = NoEmpleado
                cNomina.CvoConcepto = NumeroConcepto
                cNomina.TipoConcepto = "D"
                cNomina.EliminaConceptoEmpleado()
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub GuardarExentoYGravado(ByVal CvoConcepto, ByVal ImporteGravado, ByVal ImporteExento, ByVal NoEmpleado)
        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = clienteId.Value
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = periodoId.Value
        cNomina.NoEmpleado = NoEmpleado
        cNomina.CvoConcepto = CvoConcepto
        cNomina.ImporteGravado = ImporteGravado
        cNomina.ImporteExento = ImporteExento
        cNomina.ActualizarExentoYGravado()
        cNomina = Nothing
    End Sub
    'Private Sub CalcularImpuesto()
    '    Try
    '        Impuesto = 0
    '        Dim dt As New DataTable()
    '        Dim TarifaDiaria As New TarifaDiaria()
    '        TarifaDiaria.ImporteDiario = ImporteDiario
    '        dt = TarifaDiaria.ConsultarValoresTarifaDiaria()

    '        If dt.Rows.Count > 0 Then
    '            Impuesto = ((ImporteDiario - dt.Rows(0).Item("LimiteInferior")) * (dt.Rows(0).Item("PorcSobreExcli") / 100)) + dt.Rows(0).Item("CuotaFija")
    '        End If

    '    Catch oExcep As Exception
    '        rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
    '    End Try
    'End Sub
    Private Sub CalcularImpuesto(ByVal NoEmpleado)

        Call CargarVariablesGenerales()

        Dim DiasTarifaISR As Decimal = 14
        Dim BaseGravadaPeriodo As Decimal = 0
        Dim BaseCalculoISR As Decimal = 0

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = clienteId.Value
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = periodoId.Value
        cNomina.NoEmpleado = NoEmpleado
        cNomina.TipoConcepto = "P"
        cNomina.Tipo = "N"
        dt = cNomina.ConsultarConceptosEmpleado()

        If dt.Rows.Count > 0 Then
            If dt.Compute("Sum(ImporteGravado)", "") IsNot DBNull.Value Then
                BaseGravadaPeriodo = dt.Compute("Sum(ImporteGravado)", "")
            End If
            BaseCalculoISR = (BaseGravadaPeriodo / DiasTarifaISR) * FactorDiarioPromedio
        End If

        Try
            Impuesto = 0
            Dim TarifaMensual As New TarifaMensual()
            TarifaMensual.ImporteMensual = BaseCalculoISR
            dt = TarifaMensual.ConsultarTarifaMensual()

            If dt.Rows.Count > 0 Then
                Impuesto = ((BaseCalculoISR - dt.Rows(0).Item("LimiteInferior")) * (dt.Rows(0).Item("PorcSobreExcli") / 100)) + dt.Rows(0).Item("CuotaFija")
                Impuesto = (Impuesto / FactorDiarioPromedio) * DiasTarifaISR
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CalcularImpuestoFinMes(ByVal NoEmpleado)

        Call CargarVariablesGenerales()

        Dim BaseGravadaMes As Decimal = 0

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = clienteId.Value
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.MesAcumula = MesAcumula
        cNomina.TipoConcepto = "P"
        cNomina.Tipo = "N"
        dt = cNomina.ConsultarPercepcionesGravadasMesEmpleado()

        If dt.Rows.Count > 0 Then
            BaseGravadaMes = dt.Rows(0).Item("Importe")
        End If

        Try
            ImpuestoMes = 0
            dt = New DataTable()
            Dim TarifaMensual As New TarifaMensual()
            TarifaMensual.ImporteMensual = BaseGravadaMes
            dt = TarifaMensual.ConsultarTarifaMensual()

            If dt.Rows.Count > 0 Then
                ImpuestoMes = ((BaseGravadaMes - dt.Rows(0).Item("LimiteInferior")) * (dt.Rows(0).Item("PorcSobreExcli") / 100)) + dt.Rows(0).Item("CuotaFija")
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CalcularSubsidio(ByVal NoEmpleado)

        Call CargarVariablesGenerales()

        Dim DiasTarifaSubsidio As Decimal = 14
        Dim BaseGravadaPeriodo As Decimal = 0
        Dim BaseCalculoSubsidio As Decimal = 0

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = clienteId.Value
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = periodoId.Value
        cNomina.NoEmpleado = NoEmpleado
        cNomina.TipoConcepto = "P"
        cNomina.Tipo = "N"
        dt = cNomina.ConsultarConceptosEmpleado()

        If dt.Rows.Count > 0 Then
            If dt.Compute("Sum(ImporteGravado)", "") IsNot DBNull.Value Then
                BaseGravadaPeriodo = dt.Compute("Sum(ImporteGravado)", "")
            End If
            BaseCalculoSubsidio = (BaseGravadaPeriodo / DiasTarifaSubsidio) * FactorDiarioPromedio
        End If

        Try
            Subsidio = 0
            Dim TablaSubsidioDiario As New TablaSubsidioDiario
            TablaSubsidioDiario.Importe = BaseCalculoSubsidio
            dt = TablaSubsidioDiario.ConsultarSubsidioMensual()

            If dt.Rows.Count > 0 Then
                Subsidio = dt.Rows(0).Item("Subsidio")
                SubsidioAplicado = (Subsidio / FactorDiarioPromedio) * DiasTarifaSubsidio
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CalcularSubsidioFinMes(ByVal NoEmpleado)

        Call CargarVariablesGenerales()

        Dim BaseGravadaMes As Decimal = 0

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = clienteId.Value
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.MesAcumula = MesAcumula
        cNomina.TipoConcepto = "P"
        cNomina.Tipo = "N"
        dt = cNomina.ConsultarPercepcionesGravadasMesEmpleado()

        If dt.Rows.Count > 0 Then
            BaseGravadaMes = dt.Rows(0).Item("Importe")
        End If

        Try
            SubsidioMes = 0
            dt = New DataTable()
            Dim TablaSubsidioDiario As New TablaSubsidioDiario
            TablaSubsidioDiario.Importe = BaseGravadaMes
            dt = TablaSubsidioDiario.ConsultarSubsidioMensual()

            If dt.Rows.Count > 0 Then
                SubsidioMes = dt.Rows(0).Item("Subsidio")
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CalcularImss()
        IMSS = 0
        If ImporteDiario <= SalarioMinimoDiarioGeneral Then
            IMSS = 0
        ElseIf ImporteDiario > SalarioMinimoDiarioGeneral And ImporteDiario < (SalarioMinimoDiarioGeneral * 3) Then
            IMSS = SalarioDiarioIntegradoTrabajador * 0.02375
        ElseIf ImporteDiario > (SalarioMinimoDiarioGeneral * 3) And ImporteDiario < (SalarioMinimoDiarioGeneral * 25) Then
            IMSS = SalarioDiarioIntegradoTrabajador * 0.02375
            IMSS = IMSS + ((SalarioDiarioIntegradoTrabajador - (SalarioMinimoDiarioGeneral * 3)) * 0.004)
        ElseIf ImporteDiario > (SalarioMinimoDiarioGeneral * 25) Then
            IMSS = (SalarioDiarioIntegradoTrabajador - (SalarioMinimoDiarioGeneral * 25)) * 0.02375
            IMSS = IMSS + ((SalarioDiarioIntegradoTrabajador - (SalarioMinimoDiarioGeneral * 22)) * 0.004)
        End If
    End Sub
    Private Sub SolicitarGeneracionXml(ByVal NoEmpleado As Int64, ByVal Generado As String)

        Call CargarVariablesGenerales()

        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = clienteId.Value
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Tipo = "N"
        cNomina.Periodo = periodoId.Value
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Generado = Generado
        cNomina.ActualizarEstatusGeneradoNomina()
        cNomina = Nothing

    End Sub
    Private Sub AgregaConcepto(ByVal ImporteIncidencia, ByVal UnidadIncidencia, ByVal CuotaDiaria, ByVal IntegradoIMSS, ByVal NoEmpleado, ByVal CvoConcepto, ByVal IdContrato)
        Try
            Dim Importe As Decimal = 0
            Dim Unidad As Decimal = 0

            Try
                Importe = Convert.ToDecimal(ImporteIncidencia)
            Catch ex As Exception
                Importe = 0
            End Try
            Try
                Unidad = Convert.ToDecimal(UnidadIncidencia)
            Catch ex As Exception
                Unidad = 0
            End Try
            Try
                CuotaDiaria = Convert.ToDecimal(CuotaDiaria)
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
            cEmpleado.IdEmpleado = NoEmpleado
            cEmpleado.ConsultarEmpleadoID()

            If cEmpleado.IdEmpleado > 0 Then
                ClaveRegimenContratacion = cEmpleado.IdRegimenContratacion
                SalarioDiarioIntegradoTrabajador = cEmpleado.IntegradoImss
            End If

            If CvoConcepto.ToString = "5" And ClaveRegimenContratacion <> 9 Then
                rwAlerta.RadAlert("El regimen de contratacion de este trabajador no es honorarios asimilado a salarios!!", 330, 180, "Alerta", "", "")
                Exit Sub
            ElseIf CvoConcepto.ToString < 52 Or CvoConcepto.ToString = "57" Or CvoConcepto.ToString = "58" Or CvoConcepto.ToString = "59" Or CvoConcepto.ToString = "161" Or CvoConcepto.ToString = "162" Then
                If ClaveRegimenContratacion = 9 Then
                    rwAlerta.RadAlert("El régimen de contratación de este trabajador es asimilado a salarios, por lo mismo no se le debe agregar ningún otro tipo de percepción ni hacerle deducciones por faltas, dichas deducciones solo pueden ser por un aduedo distinto!!", 330, 180, "Alerta", "", "")
                    Exit Sub
                End If
            End If

            'If CvoConcepto.ToString = "10" Then
            '    If cmbTipoHorasExtra.SelectedValue = "01" Then
            '        If txtUnidadIncidencia.Text > 9 Then
            '            rwAlerta.RadAlert("Las horas extras no pueden ser mas de 9!!", 330, 180, "Alerta", "", "")
            '            Exit Sub
            '        End If
            '    End If
            'End If

            If ChecarSiExiste(NoEmpleado, CvoConcepto) = True Then
                rwAlerta.RadAlert("Esa percepcion/deduccion ya existe.", 330, 180, "Alerta", "", "")
                Exit Sub
            End If

            ''oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            ''oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            'If CvoConcepto.ToString = "57" Or CvoConcepto.ToString = "58" Or CvoConcepto.ToString = "59" Or CvoConcepto.ToString = "161" Or CvoConcepto.ToString = "162" Then
            '    'si es descuento ya sea por falta, permiso o incapacidad checar si existe el concepto de cuota del periodo ya que de el se debe descontar estas deducciones
            '    If ChecarQueExistaLaCuotaPeriodo(NoEmpleado, Importe, Unidad) = False Then
            '        rwAlerta.RadAlert("No existe la cuota del periodo o el importe del mismo es menor al importe de la deduccion o los dias a descontar son menores a los existentes, no se puede agregar esta deduccion!!", 330, 180, "Alerta", "", "")
            '        Exit Sub
            '        'Dim Respuesta As Integer
            '        'Respuesta = MsgBox("No existe la cuota del periodo o el importe del mismo es menor al importe de la deduccion o los dias a descontar son menores a los existentes, no se puede agregar esta deduccion a menos que desee dejar el sobre en ceros, No=SALIR Y RECTIFICAR, Si=DEJAR SOBRE EN CEROS", vbYesNo + vbExclamation + vbDefaultButton2, "Alerta")
            '        'If Respuesta = vbNo Then
            '        '    Exit Sub
            '        'End If
            '    End If
            'End If
            ''oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            ''oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo

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

            ChecarSiExistenDiasEnPercepciones(NoEmpleado, CvoConcepto, Importe, Unidad)
            If DiasCuotaPeriodo = 0 And DiasVacaciones = 0 And DiasComision = 0 And DiasPagoPorHoras = 0 And DiasDestajo = 0 And DiasHonorarioAsimilado = 0 Then
                'en este caso, si se pagan conceptos tales como comisiones, en teoria estas se generan posterior a que el trabajador tiene un sueldo, es decir una cuaota del periodo actual, ese es el motivo del mensaje.
                '    MsgBox("Esta percepcion no puede agregarse sin que exista alguna de las siguientes:" + vbCrLf + "CuotaPeriodo" + vbCrLf + "Vacaciones" + vbCrLf + "HonorarioAsimilado" + vbCrLf + "PagoPorHoras" + vbCrLf + "Comision" + vbCrLf + "Destajo" + vbCrLf + "pues estas van acompañadas implicitamente por los 7 dias correspondientes al periodo semanal lo cual es base necesaria para el calculo del impuesto,  lo que si puede hacer es cambiar el numero de dias o eliminar completamente el empleado en este periodo!!!")
                '    Exit Sub
                'End If
                BorrarDeducciones(NoEmpleado)
            Else
                If CvoConcepto.ToString < 52 Or CvoConcepto.ToString = "57" Or CvoConcepto.ToString = "58" Or CvoConcepto.ToString = "59" Or CvoConcepto.ToString = "161" Or CvoConcepto.ToString = "162" Or CvoConcepto.ToString = "167" Or CvoConcepto.ToString = "168" Or CvoConcepto.ToString = "169" Or CvoConcepto.ToString = "170" Or CvoConcepto.ToString = "171" Then
                    'En este bloque no entran las deducciones por adeudos que no cambian la base del impuesto y por lo tanto no lo calculan, ejemplos de esto serian, adeudos por credito infonavit, cuotas sindicales, adeudos fonacot, adeudos con el patron, etc, (conceptos del 61 al 86)
                    Call BorrarDeducciones(NoEmpleado)
                    Call GuardarRegistro(CuotaDiaria, 1, Importe, Unidad, IdContrato, NoEmpleado, CvoConcepto)

                    Call QuitarConcepto(52, NoEmpleado) 'IMPUESTO
                    Call QuitarConcepto(54, NoEmpleado) 'SUBSIDIO
                    Call QuitarConcepto(56, NoEmpleado) 'CUOTA IMSS

                    Call ChecarPercepcionesGravadas(NoEmpleado, CvoConcepto, Importe, Unidad, CuotaDiaria)
                    Call ChecarYGrabarPercepcionesExentasYGravadas(NoEmpleado, IdContrato, CuotaDiaria)
                    Call ChecarPercepcionesExentasYGravadas(NoEmpleado)

                    Dim cPeriodo As New Entities.Periodo()
                    cPeriodo.IdPeriodo = periodoId.Value
                    cPeriodo.ConsultarPeriodoID()

                    If ChecarQueExistaLaCuotaPeriodo(NoEmpleado, Importe, Unidad) = True Then

                        Call CalcularImss()

                        IMSS = IMSS * NumeroDeDiasPagados
                        IMSS = Math.Round(IMSS, 6)

                        If IMSS > 0 Then
                            Dim cNomina = New Nomina()
                            cNomina.IdEmpresa = IdEmpresa
                            cNomina.IdCliente = clienteId.Value
                            cNomina.Ejercicio = IdEjercicio
                            cNomina.TipoNomina = 2 'Catorcenal
                            cNomina.Periodo = IdPeriodo
                            cNomina.NoEmpleado = NoEmpleado
                            cNomina.CvoConcepto = 56
                            cNomina.IdContrato = IdContrato
                            cNomina.TipoConcepto = "D"
                            cNomina.Unidad = 1
                            cNomina.Importe = IMSS
                            cNomina.ImporteGravado = 0
                            cNomina.ImporteExento = IMSS
                            cNomina.Generado = ""
                            cNomina.Timbrado = ""
                            cNomina.Enviado = ""
                            cNomina.Situacion = "A"
                            cNomina.EsEspecial = False
                            cNomina.FechaIni = cPeriodo.FechaInicialDate
                            cNomina.FechaFin = cPeriodo.FechaFinalDate
                            cNomina.FechaPago = cPeriodo.FechaPago
                            cNomina.DiasPagados = cPeriodo.Dias
                            cNomina.IdNomina = nominaID.Value
                            cNomina.GuadarNominaPeriodo()
                        End If
                    End If

                    Impuesto = 0
                    SubsidioAplicado = 0

                    'CÁLCULO DEL ISR DIRECTO DEL PERIODO VIGENTE
                    Call CalcularImpuesto(NoEmpleado)
                    Impuesto = Math.Round(Impuesto, 6)

                    'CÁLCULO DEL SUBSIDIO CAUSADO DEL PERIODO VIGENTE
                    Call CalcularSubsidio(NoEmpleado)
                    SubsidioAplicado = Math.Round(SubsidioAplicado, 6)

                    'CÁLCULO DE IMPUESTOS  A RETENER
                    If Impuesto > SubsidioAplicado Then
                        Impuesto = Impuesto - SubsidioAplicado
                    ElseIf Impuesto < SubsidioAplicado Then
                        SubsidioAplicado = Impuesto
                        Impuesto = 0
                    End If

                    If Impuesto > 0 Then
                        Dim cNomina = New Nomina()
                        cNomina.IdEmpresa = IdEmpresa
                        cNomina.IdCliente = clienteId.Value
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 2 'Catorcenal
                        cNomina.Periodo = IdPeriodo
                        cNomina.NoEmpleado = NoEmpleado
                        cNomina.CvoConcepto = 52
                        cNomina.IdContrato = IdContrato
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
                        cNomina.IdNomina = nominaID.Value
                        cNomina.GuadarNominaPeriodo()
                    End If

                    If SubsidioAplicado > 0 Then
                        Dim cNomina = New Nomina()
                        cNomina.IdEmpresa = IdEmpresa
                        cNomina.IdCliente = clienteId.Value
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 2 'Catorcenal
                        cNomina.Periodo = IdPeriodo
                        cNomina.NoEmpleado = NoEmpleado
                        cNomina.CvoConcepto = 54
                        cNomina.IdContrato = IdContrato
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
                        cNomina.IdNomina = nominaID.Value
                        cNomina.GuadarNominaPeriodo()
                    End If

                    If FinMesBit = True Then

                        'CÁLCULO DEL IMPUESTO/SUBSIDIO MENSUAL (OBJETIVO A LLEGAR) CUANDO ES FIN DE MES
                        Call CalcularImpuestoFinMes(NoEmpleado)
                        Call CalcularSubsidioFinMes(NoEmpleado)

                        'ACUMULADOS DE LO QUE SE LLEVA EN EL MES (incluyendo el periodo vigente)
                        Dim SubsidioCausado As Double = 0
                        Dim ISRRetenido As Double = 0
                        Dim SubsidioCausadoMayorAlQueLeCorrespondia As Double = 0
                        Dim ISRAjusteMensual As Double = 0
                        Dim ISRAjustadoPorSubsidio As Double = 0

                        Dim dt As New DataTable()
                        Dim cNomina As New Nomina()
                        cNomina.IdEmpresa = IdEmpresa
                        cNomina.IdCliente = clienteId.Value
                        cNomina.NoEmpleado = empleadoId.Value
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 2 'Catorcenal
                        cNomina.MesAcumula = MesAcumula
                        cNomina.TipoConcepto = "P"
                        cNomina.Tipo = "N"
                        dt = cNomina.ConsultarSubsidioCausadoMesEmpleado()

                        'Acumulado del mes (incluyendo periodo vigente) del Subsidio Causado
                        If dt.Rows.Count > 0 Then
                            SubsidioCausado = dt.Rows(0).Item("Importe")
                        End If

                        cNomina = New Nomina()
                        cNomina.IdEmpresa = IdEmpresa
                        cNomina.IdCliente = clienteId.Value
                        cNomina.NoEmpleado = NoEmpleado
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 2 'Catorcenal
                        cNomina.MesAcumula = MesAcumula
                        cNomina.TipoConcepto = "D"
                        cNomina.Tipo = "N"
                        dt = cNomina.ConsultarISRRetenidoMesEmpleado()

                        'Acumulado del mes (incluyendo periodo vigente) del ISR retenido
                        If dt.Rows.Count > 0 Then
                            ISRRetenido = dt.Rows(0).Item("Importe")
                        End If

                        'CÁLCULO DE AJUSTES CUANDO ES FIN DE MES
                        'Subsidio causado mayor al que le correspondía
                        If SubsidioCausado > SubsidioMes Then
                            SubsidioCausadoMayorAlQueLeCorrespondia = SubsidioCausado - SubsidioMes
                        End If

                        'ISR AJUSTADO POR SUBSIDIO
                        If SubsidioCausadoMayorAlQueLeCorrespondia > 0 Then
                            cNomina = New Nomina()
                            cNomina.IdEmpresa = IdEmpresa
                            cNomina.IdCliente = clienteId.Value
                            cNomina.Ejercicio = IdEjercicio
                            cNomina.TipoNomina = 2 'Catorcenal
                            cNomina.Periodo = IdPeriodo
                            cNomina.NoEmpleado = NoEmpleado
                            cNomina.CvoConcepto = 7
                            cNomina.IdContrato = IdContrato
                            cNomina.TipoConcepto = "D"
                            cNomina.Unidad = 1
                            cNomina.Importe = SubsidioCausadoMayorAlQueLeCorrespondia
                            cNomina.ImporteGravado = 0
                            cNomina.ImporteExento = SubsidioCausadoMayorAlQueLeCorrespondia
                            cNomina.Generado = ""
                            cNomina.Timbrado = ""
                            cNomina.Enviado = ""
                            cNomina.Situacion = "A"
                            cNomina.EsEspecial = False
                            cNomina.FechaIni = cPeriodo.FechaInicialDate
                            cNomina.FechaFin = cPeriodo.FechaFinalDate
                            cNomina.FechaPago = cPeriodo.FechaPago
                            cNomina.DiasPagados = cPeriodo.Dias
                            cNomina.IdNomina = nominaID.Value
                            cNomina.GuadarNominaPeriodo()

                            Call QuitarConcepto(54, "") 'SUBSIDIO

                        End If
                    End If

                Else
                    Call GuardarRegistro(CuotaDiaria, 1, Importe, Unidad, IdContrato, NoEmpleado, CvoConcepto)
                End If
            End If

            '*******************************************************************************************************
            '*******************************************************************************************************
            Call ChecarYGrabarPercepcionesExentasYGravadas(NoEmpleado, IdContrato, CuotaDiaria)
            '*******************************************************************************************************
            '*******************************************************************************************************

            Call SolicitarGeneracionXml(NoEmpleado, "")

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Sub txtINFONAVIT_TextChanged(sender As Object, e As EventArgs)
        Dim NoEmpleado = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("NoEmpleado")
        Dim CuotaDiaria = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("CuotaDiaria")
        Dim IntegradoIMSS = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IntegradoIMSS")
        Dim IdContrato = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IdContrato")

        Dim txtINFONAVIT As RadNumericTextBox = DirectCast(sender, RadNumericTextBox)

        Dim UnidadIncidencia As Decimal = 1
        Dim Importe As Decimal = 0

        Try
            Importe = Convert.ToDecimal(txtINFONAVIT.Text)
        Catch ex As Exception
            Importe = 0
        End Try

        If ChecarSiExiste(NoEmpleado, 64) = True Then
            Call EliminarConcepto(64, "", Importe, UnidadIncidencia, CuotaDiaria, IntegradoIMSS, NoEmpleado, IdContrato)
            If Importe > 0 Then
                Call AgregaConcepto(Importe, UnidadIncidencia, CuotaDiaria, IntegradoIMSS, NoEmpleado, 64, IdContrato)
            End If
        Else
            Call AgregaConcepto(Importe, UnidadIncidencia, CuotaDiaria, IntegradoIMSS, NoEmpleado, 64, IdContrato)
        End If

        Call CargarDatos()

    End Sub
    Sub txtFaltas_TextChanged(sender As Object, e As EventArgs)
        Dim NoEmpleado = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("NoEmpleado")
        Dim CuotaDiaria = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("CuotaDiaria")
        Dim IntegradoIMSS = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IntegradoIMSS")
        Dim IdContrato = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IdContrato")

        Dim txtFaltas As RadNumericTextBox = DirectCast(sender, RadNumericTextBox)

        Dim UnidadIncidencia As Decimal = 0
        Dim Importe As Decimal = 0

        Try
            UnidadIncidencia = Convert.ToDecimal(txtFaltas.Text)
        Catch ex As Exception
            UnidadIncidencia = 0
        End Try

        Importe = CuotaDiaria * UnidadIncidencia

        If UnidadIncidencia = 0 Then
            Call EliminarConcepto(57, "", Importe, UnidadIncidencia, CuotaDiaria, IntegradoIMSS, NoEmpleado, IdContrato)
        Else
            AgregaConcepto(Importe, UnidadIncidencia, CuotaDiaria, IntegradoIMSS, NoEmpleado, 57, IdContrato)
        End If

        Call CargarDatos()

    End Sub
    Sub txtIncapacidadEG_TextChanged(sender As Object, e As EventArgs)
        Dim NoEmpleado = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("NoEmpleado")
        Dim CuotaDiaria = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("CuotaDiaria")
        Dim IntegradoIMSS = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IntegradoIMSS")
        Dim IdContrato = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IdContrato")

        Dim txtIncapacidadEG As RadNumericTextBox = DirectCast(sender, RadNumericTextBox)

        Dim UnidadIncidencia As Decimal = 0
        Dim Importe As Decimal = 0

        Try
            UnidadIncidencia = Convert.ToDecimal(txtIncapacidadEG.Text)
        Catch ex As Exception
            UnidadIncidencia = 0
        End Try

        Importe = CuotaDiaria * UnidadIncidencia

        If UnidadIncidencia = 0 Then
            Call EliminarConcepto(59, "", Importe, UnidadIncidencia, CuotaDiaria, IntegradoIMSS, NoEmpleado, IdContrato)
        Else
            AgregaConcepto(Importe, UnidadIncidencia, CuotaDiaria, IntegradoIMSS, NoEmpleado, 59, IdContrato)
        End If

        Call CargarDatos()

    End Sub
    Sub txtIncapacidadRT_TextChanged(sender As Object, e As EventArgs)
        Dim NoEmpleado = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("NoEmpleado")
        Dim CuotaDiaria = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("CuotaDiaria")
        Dim IntegradoIMSS = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IntegradoIMSS")
        Dim IdContrato = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IdContrato")

        Dim txtIncapacidadRT As RadNumericTextBox = DirectCast(sender, RadNumericTextBox)

        Dim UnidadIncidencia As Decimal = 0
        Dim Importe As Decimal = 0

        Try
            UnidadIncidencia = Convert.ToDecimal(txtIncapacidadRT.Text)
        Catch ex As Exception
            UnidadIncidencia = 0
        End Try

        Importe = CuotaDiaria * UnidadIncidencia

        If UnidadIncidencia = 0 Then
            Call EliminarConcepto(162, "", Importe, UnidadIncidencia, CuotaDiaria, IntegradoIMSS, NoEmpleado, IdContrato)
        Else
            AgregaConcepto(Importe, UnidadIncidencia, CuotaDiaria, IntegradoIMSS, NoEmpleado, 162, IdContrato)
        End If

        Call CargarDatos()

    End Sub
    Sub IncapacidadMaterna_TextChanged(sender As Object, e As EventArgs)
        Dim NoEmpleado = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("NoEmpleado")
        Dim CuotaDiaria = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("CuotaDiaria")
        Dim IntegradoIMSS = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IntegradoIMSS")
        Dim IdContrato = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IdContrato")

        Dim txtIncapacidadMaterna As RadNumericTextBox = DirectCast(sender, RadNumericTextBox)

        Dim UnidadIncidencia As Decimal = 0
        Dim Importe As Decimal = 0

        Try
            UnidadIncidencia = Convert.ToDecimal(txtIncapacidadMaterna.Text)
        Catch ex As Exception
            UnidadIncidencia = 0
        End Try

        Importe = CuotaDiaria * UnidadIncidencia

        If UnidadIncidencia = 0 Then
            Call EliminarConcepto(161, "", Importe, UnidadIncidencia, CuotaDiaria, IntegradoIMSS, NoEmpleado, IdContrato)
        Else
            AgregaConcepto(Importe, UnidadIncidencia, CuotaDiaria, IntegradoIMSS, NoEmpleado, 161, IdContrato)
        End If

        Call CargarDatos()

    End Sub
    Private Sub grdEmpleadosCatorcenal_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles grdEmpleadosCatorcenal.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                empleadoId.Value = e.CommandArgument
                rwConfirmEliminaEmpleado.RadConfirm("¿Realmente desea eliminar al trabajador de esta nomina?", "confirmCallbackFnEliminaEmpleado", 330, 180, Nothing, "Confirmar")
        End Select
    End Sub
    Private Sub btnEliminarEmpleado_Click(sender As Object, e As EventArgs) Handles btnEliminarEmpleado.Click
        Call EliminarTrabajador()
        Call CargarGridEmpleados()
    End Sub
    Private Sub EliminarTrabajador()
        Try

            Call CargarVariablesGenerales()

            Dim cNomina As New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = clienteId.Value
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.EliminaEmpleado()

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub GuardarRegistro(ByVal CuotaDiaria As Decimal, ByVal ConImpuesto As Integer, ByVal ImporteIncidencia As Decimal, ByVal UnidadIncidencia As Decimal, ByVal IdContrato As Integer, ByVal NoEmpleado As Integer, ByVal CvoConcepto As Integer, Optional ByVal DiasHorasExtra As Integer = 0, Optional ByVal TipoHorasExtra As String = "")

        Try

            Dim cPeriodo As New Entities.Periodo()
            cPeriodo.IdPeriodo = periodoId.Value
            cPeriodo.ConsultarPeriodoID()

            Call CargarVariablesGenerales()

            'ConImpuesto = 1 cuando viene de agregar una percepcion y calcular impuestos guardando ambos
            'ConImpuesto = 2 cuando viene de quitar una percepcion y solo se procede a guardar los impuesto modificados sin esa percepcion

            Dim cNomina As New Nomina()
            cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = clienteId.Value
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = IdPeriodo
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 87
            cNomina.TipoConcepto = "DE"
            cNomina.EliminaConceptoEmpleado()

            cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = clienteId.Value
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = IdPeriodo
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 87
            cNomina.IdContrato = IdContrato
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
            cNomina.IdNomina = nominaID.Value
            cNomina.GuadarNominaPeriodo()

            If ConImpuesto = 1 Then
                If CvoConcepto <= 51 Or CvoConcepto = 165 Or CvoConcepto = 166 Or CvoConcepto = 167 Or CvoConcepto = 168 Or CvoConcepto = 169 Or CvoConcepto = 170 Or CvoConcepto = 171 Then
                    cNomina = New Nomina()
                    cNomina.IdEmpresa = IdEmpresa
                    cNomina.IdCliente = clienteId.Value
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 2 'Catorcenal
                    cNomina.Periodo = IdPeriodo
                    cNomina.NoEmpleado = NoEmpleado
                    cNomina.CvoConcepto = CvoConcepto.ToString
                    cNomina.IdContrato = IdContrato
                    cNomina.TipoConcepto = "P"
                    cNomina.Unidad = UnidadIncidencia
                    cNomina.Importe = ImporteIncidencia
                    cNomina.ImporteGravado = 0
                    If CvoConcepto = 32 Or CvoConcepto = 165 Or CvoConcepto = 166 Then
                        cNomina.ImporteExento = ImporteIncidencia
                    Else
                        cNomina.ImporteExento = 0
                    End If
                    cNomina.Generado = ""
                    cNomina.Timbrado = ""
                    cNomina.Enviado = ""
                    cNomina.Situacion = "A"
                    If CvoConcepto = 10 Then
                        cNomina.DiasHorasExtra = DiasHorasExtra
                        cNomina.TipoHorasExtra = TipoHorasExtra
                    End If
                    cNomina.EsEspecial = False
                    cNomina.FechaIni = cPeriodo.FechaInicialDate
                    cNomina.FechaFin = cPeriodo.FechaFinalDate
                    cNomina.FechaPago = cPeriodo.FechaPago
                    cNomina.DiasPagados = cPeriodo.Dias
                    cNomina.IdNomina = nominaID.Value
                    cNomina.GuadarNominaPeriodo()
                ElseIf CvoConcepto = 82 Then
                    cNomina = New Nomina()
                    cNomina.IdEmpresa = IdEmpresa
                    cNomina.IdCliente = clienteId.Value
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 2 'Catorcenal
                    cNomina.Periodo = IdPeriodo
                    cNomina.NoEmpleado = NoEmpleado
                    cNomina.CvoConcepto = CvoConcepto.ToString
                    cNomina.IdContrato = IdContrato
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
                    cNomina.IdNomina = nominaID.Value
                    cNomina.GuadarNominaPeriodo()
                    cNomina = Nothing
                ElseIf CvoConcepto.ToString = "57" Or CvoConcepto.ToString = "58" Or CvoConcepto.ToString = "59" Or CvoConcepto.ToString = "161" Or CvoConcepto.ToString = "162" Then
                    cNomina = New Nomina()
                    cNomina.IdEmpresa = IdEmpresa
                    cNomina.IdCliente = clienteId.Value
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 2 'Catorcenal
                    cNomina.Periodo = IdPeriodo
                    cNomina.NoEmpleado = NoEmpleado
                    cNomina.CvoConcepto = CvoConcepto.ToString
                    cNomina.IdContrato = IdContrato
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
                    cNomina.IdNomina = nominaID.Value
                    cNomina.GuadarNominaPeriodo()
                ElseIf CvoConcepto.ToString >= 61 Then
                    cNomina = New Nomina()
                    cNomina.IdEmpresa = IdEmpresa
                    cNomina.IdCliente = clienteId.Value
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 2 'Catorcenal
                    cNomina.Periodo = IdPeriodo
                    cNomina.NoEmpleado = NoEmpleado
                    cNomina.CvoConcepto = CvoConcepto.ToString
                    cNomina.IdContrato = IdContrato
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
                    cNomina.IdNomina = nominaID.Value
                    cNomina.GuadarNominaPeriodo()
                End If
            End If

            'Aqui se guarda el impuesto, el total Gravado y el total exento, tanto cuando viene de agregar un concepto como cuando viene de quitar un concepto ya que de ambas maneras se recalcula
            'solo no entra en este bloque de codigo cuando viene de agregar una deduccion que no implica recalcular gravado, exento o impuesto(las unicas deducciones que recalculan gravado, exento e impuesto son la faltas, permisos e incapacidades, las demas deduccioens haciendo hincapie, no entran en este bloque)
            If ConImpuesto = 1 Then
                If CvoConcepto < 52 Or CvoConcepto.ToString = "57" Or CvoConcepto.ToString = "58" Or CvoConcepto.ToString = "59" Or CvoConcepto.ToString = "161" Or CvoConcepto.ToString = "162" Or CvoConcepto.ToString = "167" Or CvoConcepto.ToString = "168" Or CvoConcepto.ToString = "169" Or CvoConcepto.ToString = "170" Or CvoConcepto.ToString = "171" Then
                    If Impuesto > 0 Then
                        cNomina = New Nomina()
                        cNomina.IdEmpresa = IdEmpresa
                        cNomina.IdCliente = clienteId.Value
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 2 'Catorcenal
                        cNomina.Periodo = IdPeriodo
                        cNomina.NoEmpleado = NoEmpleado
                        cNomina.CvoConcepto = 52
                        cNomina.IdContrato = IdContrato
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
                        cNomina.IdNomina = nominaID.Value
                        cNomina.GuadarNominaPeriodo()
                    End If

                    If IMSS > 0 Then
                        cNomina = New Nomina()
                        cNomina.IdEmpresa = IdEmpresa
                        cNomina.IdCliente = clienteId.Value
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 2 'Catorcenal
                        cNomina.Periodo = IdPeriodo
                        cNomina.NoEmpleado = NoEmpleado
                        cNomina.CvoConcepto = 56
                        cNomina.IdContrato = IdContrato
                        cNomina.TipoConcepto = "D"
                        cNomina.Unidad = 1
                        cNomina.Importe = IMSS
                        cNomina.ImporteGravado = 0
                        cNomina.ImporteExento = IMSS
                        cNomina.Generado = ""
                        cNomina.Timbrado = ""
                        cNomina.Enviado = ""
                        cNomina.Situacion = "A"
                        cNomina.EsEspecial = False
                        cNomina.FechaIni = cPeriodo.FechaInicialDate
                        cNomina.FechaFin = cPeriodo.FechaFinalDate
                        cNomina.FechaPago = cPeriodo.FechaPago
                        cNomina.DiasPagados = cPeriodo.Dias
                        cNomina.IdNomina = nominaID.Value
                        cNomina.GuadarNominaPeriodo()
                    End If
                End If
            ElseIf ConImpuesto = 2 Then
                'If cmbConcepto.SelectedValue = "82" Then
                'cNomina = New Nomina()
                'cNomina.IdCliente = empresaId.Value
                'cNomina.Ejercicio = IdEjercicio
                'cNomina.TipoNomina = 2 'Catorcenal
                'cNomina.Periodo = IdPeriodo
                'cNomina.NoEmpleado = empleadoId.Value
                'cNomina.CvoConcepto = cmbConcepto.SelectedValue.ToString
                'cNomina.IdContrato = contratoId.Value
                'cNomina.TipoConcepto = "P"
                'cNomina.Unidad = 1
                'cNomina.Importe = ImporteIncidencia
                'cNomina.ImporteExento = ImporteIncidencia
                'cNomina.Generado = ""
                'cNomina.Timbrado = ""
                'cNomina.Enviado = ""
                'cNomina.Situacion = "A"
                'cNomina.EsEspecial = False
                'cNomina.FechaIni = cPeriodo.FechaInicialDate
                'cNomina.FechaFin = cPeriodo.FechaFinalDate
                'cNomina.FechaPago = cPeriodo.FechaPago
                'cNomina.DiasPagados = cPeriodo.Dias
                'cNomina.IdNomina = nominaId.Value
                'cNomina.GuadarNominaPeriodo()
                'cNomina = Nothing
                'ElseIf cmbConcepto.SelectedValue.ToString >= 61 Then
                'cNomina = New Nomina()
                'cNomina.IdCliente = empresaId.Value
                'cNomina.Ejercicio = IdEjercicio
                'cNomina.TipoNomina = 2 'Catorcenal
                'cNomina.Periodo = IdPeriodo
                'cNomina.NoEmpleado = empleadoId.Value
                'cNomina.CvoConcepto = cmbConcepto.SelectedValue.ToString
                'cNomina.IdContrato = contratoId.Value
                'cNomina.TipoConcepto = "D"
                'cNomina.Unidad = UnidadIncidencia
                'cNomina.Importe = ImporteIncidencia
                'cNomina.ImporteGravado = 0
                'cNomina.ImporteExento = ImporteIncidencia
                'cNomina.Generado = ""
                'cNomina.Timbrado = ""
                'cNomina.Enviado = ""
                'cNomina.Situacion = "A"
                'cNomina.EsEspecial = False
                'cNomina.FechaIni = cPeriodo.FechaInicialDate
                'cNomina.FechaFin = cPeriodo.FechaFinalDate
                'cNomina.FechaPago = cPeriodo.FechaPago
                'cNomina.DiasPagados = cPeriodo.Dias
                'cNomina.IdNomina = nominaId.Value
                'cNomina.GuadarNominaPeriodo()
                'End If
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub EliminarConcepto(ByVal NumeroConcepto As Int32, ByVal TipoHorasExtra As String, ByVal ImporteIncidencia As Decimal, ByVal UnidadIncidencia As Decimal, ByVal CuotaDiaria As Decimal, ByVal IntegradoIMSS As Decimal, ByVal NoEmpleado As Int32, ByVal IdContrato As Int32)
        ImporteDiario = 0
        ImportePeriodo = 0
        ImporteExento = 0
        ImporteGravado = 0
        Agregar = 0
        SalarioDiarioIntegradoTrabajador = 0
        SalarioDiarioIntegradoTrabajador = IntegradoIMSS
        'oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
        ChecarSiExistenDiasEnPercepciones(NoEmpleado, NumeroConcepto, ImporteIncidencia, UnidadIncidencia)
        'en este caso, si se pagan conceptos tales como comisiones, en teoria estas se generan posterior a que el trabajador tiene un sueldo, es decir una cuaota del periodo actual, ese es el motivo del mensaje.
        If DiasCuotaPeriodo = 0 And DiasVacaciones = 0 And DiasComision = 0 And DiasPagoPorHoras = 0 And DiasDestajo = 0 And DiasHonorarioAsimilado = 0 Then
            rwAlerta.RadAlert("Esta percepcion no puede quitarse sin que exista alguna de las siguientes: 1.- CuotaPeriodo. 2.- Vacaciones. 3.- Honorario Asimilado. 4.- Pago Por Horas. 5.- Comisión. 6.- Destajo. Pues estas van acompañadas implicitamente por los 7 dias correspondientes al periodo semanal lo cual es base necesaria para el cálculo del impuesto, lo que si puede hacer es cambiar el número de dias o eliminar completamente el empleado en este periodo!!!", 490, 210, "Alerta", "", "")
            Exit Sub
        End If

        'En esta parte se checa si el concepto que se esta agregando es percepcion(menor que 51) o es falta, permiso o incapacidad(57,58,59), siempre se borran los impuestos actuales y se recalculan, la demas deduccioen no entran aqui ya que no recalculan impuestos, simplemente se restan.
        'If cmbConcepto.SelectedValue.ToString < 52 Or cmbConcepto.SelectedValue.ToString = "57" Or cmbConcepto.SelectedValue.ToString = "58" Or cmbConcepto.SelectedValue.ToString = "59" Then
        If NumeroConcepto < 52 Or NumeroConcepto = "57" Or NumeroConcepto = "58" Or NumeroConcepto = "59" Or NumeroConcepto = "161" Or NumeroConcepto = "162" Or NumeroConcepto = "167" Or NumeroConcepto = "168" Or NumeroConcepto = "169" Or NumeroConcepto = "170" Or NumeroConcepto = "171" Then
            BorrarDeducciones(NoEmpleado)
        End If

        QuitarConcepto(NumeroConcepto, NoEmpleado)

        If NumeroConcepto < 52 Or NumeroConcepto = "57" Or NumeroConcepto = "58" Or NumeroConcepto = "59" Or NumeroConcepto = "161" Or NumeroConcepto = "162" Or NumeroConcepto = "167" Or NumeroConcepto = "168" Or NumeroConcepto = "169" Or NumeroConcepto = "170" Or NumeroConcepto = "171" Then

            Call QuitarConcepto(52, NoEmpleado) 'IMPUESTO
            Call QuitarConcepto(54, NoEmpleado) 'SUBSIDIO
            Call QuitarConcepto(56, NoEmpleado) 'CUOTA IMSS

            Call ChecarPercepcionesGravadas(NoEmpleado, NumeroConcepto, ImporteIncidencia, UnidadIncidencia, CuotaDiaria)
            Call ChecarYGrabarPercepcionesExentasYGravadas(NoEmpleado, IdContrato, CuotaDiaria)
            Call ChecarPercepcionesExentasYGravadas(NoEmpleado)
            Call CalcularImss()

            IMSS = IMSS * NumeroDeDiasPagados
            IMSS = Math.Round(IMSS, 6)

            Dim cPeriodo As New Entities.Periodo()
            cPeriodo.IdPeriodo = periodoId.Value
            cPeriodo.ConsultarPeriodoID()

            If IMSS > 0 Then
                Dim cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = clienteId.Value
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = IdPeriodo
                cNomina.NoEmpleado = NoEmpleado
                cNomina.CvoConcepto = 56
                cNomina.IdContrato = IdContrato
                cNomina.TipoConcepto = "D"
                cNomina.Unidad = 1
                cNomina.Importe = IMSS
                cNomina.ImporteGravado = 0
                cNomina.ImporteExento = IMSS
                cNomina.Generado = ""
                cNomina.Timbrado = ""
                cNomina.Enviado = ""
                cNomina.Situacion = "A"
                cNomina.EsEspecial = False
                cNomina.FechaIni = cPeriodo.FechaInicialDate
                cNomina.FechaFin = cPeriodo.FechaFinalDate
                cNomina.FechaPago = cPeriodo.FechaPago
                cNomina.DiasPagados = cPeriodo.Dias
                cNomina.IdNomina = nominaID.Value
                cNomina.GuadarNominaPeriodo()
            End If

            Impuesto = 0
            SubsidioAplicado = 0

            Call CalcularImpuesto(NoEmpleado)
            Impuesto = Math.Round(Impuesto, 6)

            Call CalcularSubsidio(NoEmpleado)
            SubsidioAplicado = Math.Round(SubsidioAplicado, 6)

            If Impuesto > SubsidioAplicado Then
                Impuesto = Impuesto - SubsidioAplicado
            ElseIf Impuesto < SubsidioAplicado Then
                SubsidioAplicado = Impuesto
                Impuesto = 0
            End If

            If Impuesto > 0 Then
                Dim cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = clienteId.Value
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = IdPeriodo
                cNomina.NoEmpleado = NoEmpleado
                cNomina.CvoConcepto = 52
                cNomina.IdContrato = IdContrato
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
                cNomina.IdNomina = nominaID.Value
                cNomina.GuadarNominaPeriodo()
            End If

            If SubsidioAplicado > 0 Then
                Dim cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = clienteId.Value
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = IdPeriodo
                cNomina.NoEmpleado = NoEmpleado
                cNomina.CvoConcepto = 54
                cNomina.IdContrato = IdContrato
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
                cNomina.IdNomina = nominaID.Value
                cNomina.GuadarNominaPeriodo()
            End If

            If FinMesBit = True Then

                'CÁLCULO DEL IMPUESTO/SUBSIDIO MENSUAL (OBJETIVO A LLEGAR) CUANDO ES FIN DE MES
                Call CalcularImpuestoFinMes(NoEmpleado)
                Call CalcularSubsidioFinMes(NoEmpleado)

                'ACUMULADOS DE LO QUE SE LLEVA EN EL MES (incluyendo el periodo vigente)
                Dim SubsidioCausado As Double = 0
                Dim ISRRetenido As Double = 0
                Dim SubsidioCausadoMayorAlQueLeCorrespondia As Double = 0
                Dim ISRAjusteMensual As Double = 0
                Dim ISRAjustadoPorSubsidio As Double = 0

                Dim dt As New DataTable()
                Dim cNomina As New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = clienteId.Value
                cNomina.NoEmpleado = empleadoId.Value
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.MesAcumula = MesAcumula
                cNomina.TipoConcepto = "P"
                cNomina.Tipo = "N"
                dt = cNomina.ConsultarSubsidioCausadoMesEmpleado()

                'Acumulado del mes (incluyendo periodo vigente) del Subsidio Causado
                If dt.Rows.Count > 0 Then
                    SubsidioCausado = dt.Rows(0).Item("Importe")
                End If

                cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = clienteId.Value
                cNomina.NoEmpleado = NoEmpleado
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.MesAcumula = MesAcumula
                cNomina.TipoConcepto = "D"
                cNomina.Tipo = "N"
                dt = cNomina.ConsultarISRRetenidoMesEmpleado()

                'Acumulado del mes (incluyendo periodo vigente) del ISR retenido
                If dt.Rows.Count > 0 Then
                    ISRRetenido = dt.Rows(0).Item("Importe")
                End If

                'CÁLCULO DE AJUSTES CUANDO ES FIN DE MES
                'Subsidio causado mayor al que le correspondía
                If SubsidioCausado > SubsidioMes Then
                    SubsidioCausadoMayorAlQueLeCorrespondia = SubsidioCausado - SubsidioMes
                End If

                'ISR AJUSTADO POR SUBSIDIO
                If SubsidioCausadoMayorAlQueLeCorrespondia > 0 Then
                    cNomina = New Nomina()
                    cNomina.IdEmpresa = IdEmpresa
                    cNomina.IdCliente = clienteId.Value
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 2 'Catorcenal
                    cNomina.Periodo = IdPeriodo
                    cNomina.NoEmpleado = NoEmpleado
                    cNomina.CvoConcepto = 7
                    cNomina.IdContrato = IdContrato
                    cNomina.TipoConcepto = "D"
                    cNomina.Unidad = 1
                    cNomina.Importe = SubsidioCausadoMayorAlQueLeCorrespondia
                    cNomina.ImporteGravado = 0
                    cNomina.ImporteExento = SubsidioCausadoMayorAlQueLeCorrespondia
                    cNomina.Generado = ""
                    cNomina.Timbrado = ""
                    cNomina.Enviado = ""
                    cNomina.Situacion = "A"
                    cNomina.EsEspecial = False
                    cNomina.FechaIni = cPeriodo.FechaInicialDate
                    cNomina.FechaFin = cPeriodo.FechaFinalDate
                    cNomina.FechaPago = cPeriodo.FechaPago
                    cNomina.DiasPagados = cPeriodo.Dias
                    cNomina.IdNomina = nominaID.Value
                    cNomina.GuadarNominaPeriodo()

                    Call QuitarConcepto(54, "") 'SUBSIDIO

                End If
            End If

        End If

        Call GuardarRegistro(CuotaDiaria, 2, ImporteIncidencia, UnidadIncidencia, IdContrato, NoEmpleado, NumeroConcepto)
        Call ChecarYGrabarPercepcionesExentasYGravadas(NoEmpleado, IdContrato, CuotaDiaria)
        Call SolicitarGeneracionXml(NoEmpleado, "")

    End Sub
    Private Sub ChecarPercepcionesExentasYGravadas(ByVal NoEmpleado As Int32)
        Try
            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = clienteId.Value
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.TipoConcepto = "P"
            cNomina.Tipo = "N"
            dt = cNomina.ConsultarConceptosEmpleado()

            If dt.Rows.Count > 0 Then
                PercepcionesGravadas = dt.Compute("Sum(ImporteGravado)", "")
                PercepcionesExentas = dt.Compute("Sum(ImporteExento)", "")
            End If

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        If Not String.IsNullOrEmpty(Request("id")) Then
            Response.Redirect("~/GeneracionDeNominaCatorcenalNormal.aspx?id=" & periodoId.Value.ToString, False)
        Else
            Response.Redirect("~/GeneracionDeNominaCatorcenalNormal.aspx", False)
        End If
    End Sub

End Class