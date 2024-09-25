Imports Telerik.Web.UI
Imports Entities
Public Class ModificacionGeneralQuincenal
    Inherits System.Web.UI.Page

    Private IdEmpresa As Integer = 0
    Private IdEjercicio As Integer = 0
    Private Periodo As Integer = 0

    Private CuotaPeriodo As Double
    Private HorasTriples As Double
    Private DescansoTrabajado As Double
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
    Private FestivoTrabajadoGravado As Double
    Private FestivoTrabajadoExento As Double
    Private DobleteGravado As Double
    Private DobleteExento As Double

    Private ImporteDiario As Double
    Private ImportePeriodo As Double
    Private Impuesto As Double
    Private Subsidio As Double
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
    Private Imss As Double
    Private ImporteSeguroVivienda As Double

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request("id")) Then

                periodoId.Value = Request.QueryString("id").ToString
                empresaId.Value = Request.QueryString("cid").ToString

                Call CargarDatos()
                Call LlenaConceptosComunes(0)

                grdEmpleadosQuincenal.MasterTableView.GetColumn("Comisiones").Display = False
                grdEmpleadosQuincenal.MasterTableView.GetColumn("Faltas").Display = False
                grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasDobles").Display = False
                grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasTriples").Display = False
                grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioAsistencia").Display = False
                grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioPuntualidad").Display = False
                grdEmpleadosQuincenal.MasterTableView.GetColumn("PrimaDominical").Display = False
            End If
        End If
    End Sub
    Private Sub LlenaConceptosComunes(ByVal sel As Integer)
        Dim cConcepto As New Entities.Concepto
        Dim ObjData As New DataControl()
        objData.Catalogo(cmbIncidencias, sel, cConcepto.ConsultarConceptosComunes())
        cConcepto = Nothing
        objData = Nothing
    End Sub
    Private Sub CargarDatos()
        Try
            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            cNomina.Cliente = empresaId.Value
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Periodo = periodoId.Value
            dt = cNomina.ConsultarDatosGeneralesNomina()
            cNomina = Nothing
            If dt.Rows.Count > 0 Then
                For Each oDataRow In dt.Rows
                    Me.lblEjercicio.Text = oDataRow("Ejercicio")
                    Me.lblRazonSocial.Text = oDataRow("Cliente")
                    Me.lblNoPeriodo.Text = oDataRow("Periodo")
                    Me.lblTipoNomina.Text = "Quincenal"
                    Me.lblFechaInicial.Text = oDataRow("FechaInicial")
                    Me.lblFechaFinal.Text = oDataRow("FechaFinal")
                    Me.lblDias.Text = oDataRow("Dias")
                Next
                Call CargarGridEmpleadosQuincenal()
            Else
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
            Next
        End If
    End Sub
    Private Sub CargarGridEmpleadosQuincenal()

        CargarVariablesGenerales()

        Dim cNomina As New Nomina()
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.Periodo = periodoId.Value
        grdEmpleadosQuincenal.DataSource = cNomina.ConsultarDetalleNominaExtraordinaria()
        grdEmpleadosQuincenal.DataBind()
        cNomina = Nothing
    End Sub
    Private Sub grdEmpleadosQuincenal_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles grdEmpleadosQuincenal.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then

            Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
            Dim lnkEditar As LinkButton = DirectCast(e.Item.FindControl("lnkEditar"), LinkButton)
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
    End Sub
    Private Sub grdEmpleadosQuincenal_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdEmpleadosQuincenal.NeedDataSource

        CargarVariablesGenerales()

        Dim cNomina As New Nomina()
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.Periodo = periodoId.Value
        grdEmpleadosQuincenal.DataSource = cNomina.ConsultarDetalleNominaExtraordinaria()
        cNomina = Nothing
    End Sub
    Private Sub cmbIncidencias_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbIncidencias.SelectedIndexChanged
        If cmbIncidencias.SelectedValue = 0 Then
            grdEmpleadosQuincenal.MasterTableView.GetColumn("Comisiones").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("Faltas").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasDobles").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasTriples").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioAsistencia").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioPuntualidad").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PrimaDominical").Display = False
        ElseIf cmbIncidencias.SelectedValue = 3 Then    'COMISION
            grdEmpleadosQuincenal.MasterTableView.GetColumn("Comisiones").Display = True
            grdEmpleadosQuincenal.MasterTableView.GetColumn("Faltas").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasDobles").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasTriples").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioAsistencia").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioPuntualidad").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PrimaDominical").Display = False
        ElseIf cmbIncidencias.SelectedValue = 6 Then    'HORAS DOBLES
            grdEmpleadosQuincenal.MasterTableView.GetColumn("Comisiones").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("Faltas").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasDobles").Display = True
            grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasTriples").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioAsistencia").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioPuntualidad").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PrimaDominical").Display = False
        ElseIf cmbIncidencias.SelectedValue = 7 Then    'HORAS TRIPLES
            grdEmpleadosQuincenal.MasterTableView.GetColumn("Comisiones").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("Faltas").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasDobles").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasTriples").Display = True
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioAsistencia").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioPuntualidad").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PrimaDominical").Display = False
        ElseIf cmbIncidencias.SelectedValue = 13 Then    'PRIMA DOMINICAL
            grdEmpleadosQuincenal.MasterTableView.GetColumn("Comisiones").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("Faltas").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasDobles").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasTriples").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioAsistencia").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioPuntualidad").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PrimaDominical").Display = True
        ElseIf cmbIncidencias.SelectedValue = 23 Then    'PREMIO ASISTENCIA
            grdEmpleadosQuincenal.MasterTableView.GetColumn("Comisiones").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("Faltas").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasDobles").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasTriples").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioAsistencia").Display = True
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioPuntualidad").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PrimaDominical").Display = False
        ElseIf cmbIncidencias.SelectedValue = 24 Then    'PREMIO PUNTUALIDAD
            grdEmpleadosQuincenal.MasterTableView.GetColumn("Comisiones").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("Faltas").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasDobles").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasTriples").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioAsistencia").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioPuntualidad").Display = True
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PrimaDominical").Display = False
        ElseIf cmbIncidencias.SelectedValue = 57 Then    'FALTAS
            grdEmpleadosQuincenal.MasterTableView.GetColumn("Comisiones").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("Faltas").Display = True
            grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasDobles").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("HorasTriples").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioAsistencia").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PremioPuntualidad").Display = False
            grdEmpleadosQuincenal.MasterTableView.GetColumn("PrimaDominical").Display = False
        End If
    End Sub
    Private Function ChecarSiExiste(ByVal NoEmpleado As Integer, ByVal CvoConcepto As Int32) As Boolean
        Try

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
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
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            dt = cNomina.ConsultarConceptosEmpleado()

            If dt.Rows.Count = 0 Or dt.Compute("SUM(Importe)", "CvoConcepto=85") Is DBNull.Value Then
                ChecarQueExistaLaCuotaPeriodo = False
            ElseIf dt.Rows.Count >= 0 And dt.Compute("SUM(Importe)", "CvoConcepto=85") IsNot DBNull.Value Then
                If dt.Compute("SUM(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") IsNot DBNull.Value Then
                    If dt.Compute("SUM(Importe)", "CvoConcepto=85") < (dt.Compute("SUM(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") + ImporteIncidencia) Or dt.Compute("SUM(Unidad)", "CvoConcepto=85") < (dt.Compute("SUM(Unidad)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") + UnidadIncidencia) Then
                        ChecarQueExistaLaCuotaPeriodo = False
                    ElseIf dt.Compute("SUM(Importe)", "CvoConcepto=85") > (dt.Compute("SUM(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") + ImporteIncidencia) Or dt.Compute("SUM(Unidad)", "CvoConcepto=85") > (dt.Compute("SUM(Unidad)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") + UnidadIncidencia) Then
                        ChecarQueExistaLaCuotaPeriodo = True
                    End If
                ElseIf dt.Compute("SUM(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") Is DBNull.Value Then
                    If dt.Compute("SUM(Importe)", "CvoConcepto=85") < ImporteIncidencia Or dt.Compute("SUM(Unidad)", "CvoConcepto=85") < UnidadIncidencia Then
                        ChecarQueExistaLaCuotaPeriodo = False
                    ElseIf dt.Compute("SUM(Importe)", "CvoConcepto=85") > ImporteIncidencia Or dt.Compute("SUM(Unidad)", "CvoConcepto=85") > UnidadIncidencia Then
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
            TiempoExtraordinarioFueraDelMargenLegal = 0

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()

            If Agregar = 1 Then
                'Me.oDataAdapterChecarPercepcionesGravadasSql = New SqlDataAdapter("SELECT * FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO=" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO=" + TxtClaveEmpleado.Text + " AND TIPOCONCEPTO='P'", oConexionSql)
                'cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 3 'Quincenal
                cNomina.Periodo = periodoId.Value
                cNomina.NoEmpleado = NoEmpleado
                cNomina.TipoConcepto = "P"
                dt = cNomina.ConsultarConceptosEmpleado()
            ElseIf Agregar = 0 Then
                'Me.oDataAdapterChecarPercepcionesGravadas = New OleDbDataAdapter("SELECT * FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO=" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO=" + TxtClaveEmpleado.Text + " AND TIPOCONCEPTO='P' AND CVOCONCEPTO<>" + NumeroConcepto.ToString + "", oConexion)
                'cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 3 'Quincenal
                cNomina.Periodo = periodoId.Value
                cNomina.NoEmpleado = NoEmpleado
                cNomina.TipoConcepto = "P"
                cNomina.DiferenteDe = 1
                cNomina.CvoConcepto = CvoConcepto
                dt = cNomina.ConsultarConceptosEmpleado()
            Else
                'cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 3 'Quincenal
                cNomina.Periodo = periodoId.Value
                cNomina.NoEmpleado = NoEmpleado
                cNomina.TipoConcepto = "P"
                dt = cNomina.ConsultarConceptosEmpleado()
            End If

            ' PercepcionesGravadas
            If dt.Rows.Count > 0 Then
                If dt.Compute("SUM(Importe)", "CvoConcepto=85") IsNot DBNull.Value Then
                    If Agregar <> 3 Then
                        DiasCuotaPeriodo = dt.Compute("SUM(Unidad)", "CvoConcepto=85")
                        CuotaPeriodo = dt.Compute("SUM(Importe)", "CvoConcepto=85")
                    ElseIf Agregar = 3 Then
                        ''''''''''''''''''''''PENDIENTE'''''''''''''''''''''''''''''
                        'DiasCuotaPeriodo = TextBox(0).Text
                        'CuotaPeriodo = TextBoxV(0).Text
                    End If
                End If
                If dt.Compute("SUM(Importe)", "CvoConcepto=3") IsNot DBNull.Value Then
                    DiasComision = 7
                    Comisiones = dt.Compute("SUM(Importe)", "CvoConcepto=3")
                End If
                If dt.Compute("SUM(Importe)", "CvoConcepto=4") IsNot DBNull.Value Then
                    DiasDestajo = 7
                    Destajo = dt.Compute("SUM(Importe)", "CvoConcepto=4")
                End If
                If dt.Compute("SUM(Importe)", "CvoConcepto=5") IsNot DBNull.Value Then
                    DiasHonorarioAsimilado = dt.Compute("SUM(Unidad)", "CvoConcepto=5") * 15
                    HonorarioAsimilado = dt.Compute("SUM(Importe)", "CvoConcepto=5")
                End If
                If dt.Compute("SUM(Importe)", "CvoConcepto=6 OR CvoConcepto=9 OR CvoConcepto=10") IsNot DBNull.Value Then
                    TiempoExtraordinarioDentroDelMargenLegal = dt.Compute("SUM(Importe)", "CvoConcepto=6 OR CvoConcepto=9 OR CvoConcepto=10")
                End If
                If dt.Compute("SUM(Importe)", "CvoConcepto=7 OR CvoConcepto=8") IsNot DBNull.Value Then
                    TiempoExtraordinarioFueraDelMargenLegal = dt.Compute("SUM(Importe)", "CvoConcepto=7 OR CvoConcepto=8")
                End If
                If dt.Compute("SUM(Importe)", "CvoConcepto=11") IsNot DBNull.Value Then
                    DiasPagoPorHoras = 7
                    PagoPorHoras = dt.Compute("SUM(Importe)", "CvoConcepto=11")
                End If
                If dt.Compute("SUM(Importe)", "CvoConcepto=13") IsNot DBNull.Value Then
                    PrimaDominical = dt.Compute("SUM(Importe)", "CvoConcepto=13")
                End If
                If dt.Compute("SUM(Importe)", "CvoConcepto=14") IsNot DBNull.Value Then
                    Aguinaldo = dt.Compute("SUM(Importe)", "CvoConcepto=14")
                End If
                If dt.Compute("SUM(Importe)", "CvoConcepto=16") IsNot DBNull.Value Then
                    PrimaVacacional = dt.Compute("SUM(Importe)", "CvoConcepto=16")
                End If
                If dt.Compute("SUM(Importe)", "CvoConcepto=15") IsNot DBNull.Value Then
                    DiasVacaciones = dt.Compute("SUM(Unidad)", "CvoConcepto=15")
                    Vacaciones = dt.Compute("SUM(Importe)", "CvoConcepto=15")
                End If
                If dt.Compute("SUM(Importe)", "CvoConcepto=50") IsNot DBNull.Value Then
                    RepartoUtilidades = dt.Compute("SUM(Importe)", "CvoConcepto=50")
                End If
                If dt.Compute("SUM(Importe)", "CvoConcepto=42") IsNot DBNull.Value Then
                    FondoAhorro = dt.Compute("SUM(Importe)", "CvoConcepto=42")
                End If
                If dt.Compute("SUM(Importe)", "CvoConcepto=44") IsNot DBNull.Value Then
                    AyudaFuneral = dt.Compute("SUM(Importe)", "CvoConcepto=44")
                End If
                '33,34,35,36,37,38,40,41,43,45,46,47,48
                If dt.Compute("SUM(Importe)", "CvoConcepto=33 OR CvoConcepto=34 OR CvoConcepto=35 OR CvoConcepto=36 OR CvoConcepto=37 OR CvoConcepto=38 OR CvoConcepto=40 OR CvoConcepto=41 OR CvoConcepto=43 OR CvoConcepto=45 OR CvoConcepto=46 OR CvoConcepto=47 OR CvoConcepto=48") IsNot DBNull.Value Then
                    PrevisionSocial = dt.Compute("SUM(Importe)", "CvoConcepto=6 OR CvoConcepto=9 OR CvoConcepto=10")
                End If
                '12,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,39,49
                If dt.Compute("SUM(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=18 OR CvoConcepto=19 OR CvoConcepto=20 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=28 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=32 OR CvoConcepto=39 OR CvoConcepto=49") IsNot DBNull.Value Then
                    GrupoPercepcionesGravadasTotalmenteSinExentos = dt.Compute("SUM(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=18 OR CvoConcepto=19 OR CvoConcepto=20 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=28 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=32 OR CvoConcepto=39 OR CvoConcepto=49")
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
                    DiasHonorarioAsimilado = DiasCuotaPeriodo + (UnidadIncidencia * 15)
                    HonorarioAsimilado = HonorarioAsimilado + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "7" Or CvoConcepto.ToString = "8" Then
                    '7=horas extras triples y 8=festivo trabajado
                    TiempoExtraordinarioFueraDelMargenLegal = TiempoExtraordinarioFueraDelMargenLegal + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "2" Then
                    DiasCuotaPeriodo = DiasCuotaPeriodo + UnidadIncidencia
                    CuotaPeriodo = CuotaPeriodo + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "11" Then
                    DiasPagoPorHoras = 15
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
    Public Sub BorrarDeducciones(ByVal NoEmpleado As Int64)
        Try
            Call CargarVariablesGenerales()
            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=86 AND TIPOCONCEPTO='D'"
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 86
            cNomina.TipoConcepto = "D"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=54 AND TIPOCONCEPTO='P'"
            cNomina = New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 54
            cNomina.TipoConcepto = "P"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=55 AND TIPOCONCEPTO='P'"
            cNomina = New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 55
            cNomina.TipoConcepto = "P"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=56 AND TIPOCONCEPTO='D'"
            cNomina = New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 56
            cNomina.TipoConcepto = "D"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=108 AND TIPOCONCEPTO='DE'"
            cNomina = New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 108
            cNomina.TipoConcepto = "DE"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=109 AND TIPOCONCEPTO='DE'"
            cNomina = New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 109
            cNomina.TipoConcepto = "DE"
            cNomina.EliminaConceptoEmpleado()

        Catch oExcep As Exception
            MsgBox(oExcep.Message)
        End Try
    End Sub
    Private Sub ChecarPercepcionesGravadas(ByVal NoEmpleado As Int64, ByVal CvoConcepto As Int32, ByVal ImporteIncidencia As Decimal, ByVal UnidadIncidencia As Decimal)
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

            ''''''''' Pendiente leer '''''''''
            'SalarioMinimoDiarioGeneral = 73.04

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado
            dt = cNomina.ConsultarConceptosEmpleado()
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
                    DiasHonorarioAsimilado = dt.Compute("Sum(UNIDAD)", "CvoConcepto=5") * 15
                    HonorarioAsimilado = dt.Compute("Sum(Importe)", "CvoConcepto=5")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=6 OR CvoConcepto=9 OR CvoConcepto=10") IsNot DBNull.Value Then
                    TiempoExtraordinarioDentroDelMargenLegal = dt.Compute("Sum(Importe)", "CvoConcepto=6 OR CvoConcepto=9 OR CvoConcepto=10")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=7 OR CvoConcepto=8") IsNot DBNull.Value Then
                    TiempoExtraordinarioFueraDelMargenLegal = dt.Compute("Sum(Importe)", "CvoConcepto=7 OR CvoConcepto=8")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=11") IsNot DBNull.Value Then
                    DiasPagoPorHoras = 15
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
                    DiasHonorarioAsimilado = DiasCuotaPeriodo + (UnidadIncidencia * 15)
                    HonorarioAsimilado = HonorarioAsimilado + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "7" Or CvoConcepto.ToString = "8" Then
                    '7=horas extras triples y 8=festivo trabajado
                    TiempoExtraordinarioFueraDelMargenLegal = TiempoExtraordinarioFueraDelMargenLegal + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "2" Then
                    DiasCuotaPeriodo = DiasCuotaPeriodo + UnidadIncidencia
                    CuotaPeriodo = CuotaPeriodo + ImporteIncidencia
                ElseIf CvoConcepto.ToString = "11" Then
                    DiasPagoPorHoras = 15
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
                If PrevisionSocial < (SalarioMinimoDiarioGeneral * 15) Then
                    ImporteExento = ImporteExento + PrevisionSocial
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial < ((SalarioMinimoDiarioGeneral * 15) * 15) Then
                    ImporteExento = ImporteExento + PrevisionSocial
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial > ((SalarioMinimoDiarioGeneral * 15) * 15) And ImporteGravado + ImporteExento > ((SalarioMinimoDiarioGeneral * 15) * 15) Then
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 15)
                    ImporteGravado = ImporteGravado + (PrevisionSocial - (SalarioMinimoDiarioGeneral * 15))
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial > ((SalarioMinimoDiarioGeneral * 15) * 15) And ImporteGravado + ImporteExento + (SalarioMinimoDiarioGeneral * 15) < ((SalarioMinimoDiarioGeneral * 15) * 15) Then
                    ImporteExento = ImporteExento + (((SalarioMinimoDiarioGeneral * 15) * 15) - (ImporteGravado + ImporteExento))
                    ImporteGravado = ImporteGravado + (PrevisionSocial - (((SalarioMinimoDiarioGeneral * 15) * 15) - (ImporteGravado + ImporteExento)))
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial > ((SalarioMinimoDiarioGeneral * 15) * 15) And (((SalarioMinimoDiarioGeneral * 15) * 15) - (ImporteGravado + ImporteExento) < SalarioMinimoDiarioGeneral * 15) Then
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 15)
                    ImporteGravado = ImporteGravado + (PrevisionSocial - (SalarioMinimoDiarioGeneral * 15))
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
    Private Sub ChecarYGrabarPercepcionesExentasYGravadas(ByVal NoEmpleado As Integer, ByVal IdContrato As Integer)
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

            ''''''''' Pendiente leer '''''''''
            'SalarioMinimoDiarioGeneral = 73.04

            'Me.oDataAdapterChecarPercepcionesGravadas = New OleDbDataAdapter("SELECT * FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO=" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO=" + oDataRow("NOEMPLEADO").ToString + "", oConexion)
            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = NoEmpleado

            dt = cNomina.ConsultarConceptosEmpleado()

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
                    DiasHonorarioAsimilado = dt.Compute("Sum(UNIDAD)", "CvoConcepto=5") * 15
                    HonorarioAsimilado = dt.Compute("Sum(Importe)", "CvoConcepto=5")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=6 OR CvoConcepto=9 OR CvoConcepto=10") IsNot DBNull.Value Then
                    TiempoExtraordinarioDentroDelMargenLegal = dt.Compute("Sum(Importe)", "CvoConcepto=6 OR CvoConcepto=9 OR CvoConcepto=10")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=7 OR CvoConcepto=8") IsNot DBNull.Value Then
                    TiempoExtraordinarioFueraDelMargenLegal = dt.Compute("Sum(Importe)", "CvoConcepto=7 OR CvoConcepto=8")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=11") IsNot DBNull.Value Then
                    DiasPagoPorHoras = 15
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
                If PrevisionSocial < (SalarioMinimoDiarioGeneral * 15) Then
                    ImporteExento = ImporteExento + PrevisionSocial
                    ImporteExentoPrevisionSocial = PrevisionSocial
                    ImporteGravadoPrevisionSocial = 0
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial < ((SalarioMinimoDiarioGeneral * 15) * 15) Then
                    ImporteExento = ImporteExento + PrevisionSocial
                    ImporteExentoPrevisionSocial = PrevisionSocial
                    ImporteGravadoPrevisionSocial = 0
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial > ((SalarioMinimoDiarioGeneral * 15) * 15) And ImporteGravado + ImporteExento > ((SalarioMinimoDiarioGeneral * 15) * 15) Then
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 15)
                    ImporteGravado = ImporteGravado + (PrevisionSocial - (SalarioMinimoDiarioGeneral * 15))
                    ImporteExentoPrevisionSocial = SalarioMinimoDiarioGeneral * 15
                    ImporteGravadoPrevisionSocial = PrevisionSocial - (SalarioMinimoDiarioGeneral * 15)
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial > ((SalarioMinimoDiarioGeneral * 15) * 15) And ImporteGravado + ImporteExento + SalarioMinimoDiarioGeneral < ((SalarioMinimoDiarioGeneral * 15) * 15) Then
                    ImporteExento = ImporteExento + (((SalarioMinimoDiarioGeneral * 15) * 15) - (ImporteGravado + ImporteExento))
                    ImporteGravado = ImporteGravado + (PrevisionSocial - (((SalarioMinimoDiarioGeneral * 15) * 15) - (ImporteGravado + ImporteExento)))
                    ImporteExentoPrevisionSocial = ((SalarioMinimoDiarioGeneral * 15) * 15) - (ImporteGravado + ImporteExento)
                    ImporteGravadoPrevisionSocial = PrevisionSocial - (((SalarioMinimoDiarioGeneral * 15) * 15) - (ImporteGravado + ImporteExento))
                ElseIf ImporteGravado + ImporteExento + PrevisionSocial > ((SalarioMinimoDiarioGeneral * 15) * 15) And (((SalarioMinimoDiarioGeneral * 15) * 15) - (ImporteGravado + ImporteExento) < SalarioMinimoDiarioGeneral * 15) Then
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * 15)
                    ImporteExentoPrevisionSocial = SalarioMinimoDiarioGeneral * 15
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
            Infonavit.IdEmpresa = Session("clienteid")
            Infonavit.IdEmpleado = NoEmpleado
            datos = Infonavit.ConsultarEmpleadosConDescuentoInfonavit()
            Infonavit = Nothing

            ''''''''' Pendiente leer '''''''''
            'ImporteSeguroVivienda = 15

            If datos.Rows.Count > 0 Then
                If datos.Rows(0)("tipo_descuento") = 1 Then
                    Valor = datos.Rows(0)("valor_descuento")
                    DescuentoInvonavit = ((Valor + ImporteSeguroVivienda) / 30.4) * NumeroDeDiasPagados
                ElseIf datos.Rows(0)("tipo_descuento") = 2 Then
                    Valor = datos.Rows(0)("valor_descuento")
                    DescuentoInvonavit = (((Valor * SalarioMinimoDiarioGeneral) + ImporteSeguroVivienda) / 30.4) * NumeroDeDiasPagados
                ElseIf datos.Rows(0)("tipo_descuento") = 3 Then
                    Valor = datos.Rows(0)("valor_descuento")
                    DescuentoInvonavit = ((SalarioDiarioIntegradoTrabajador * (Valor / 100)) + (ImporteSeguroVivienda / 30.4)) * NumeroDeDiasPagados
                End If

                'If datos.Rows(0)("tipo_descuento") = 1 Then
                '    Valor = datos.Rows(0)("valor_descuento")
                '    DescuentoInvonavit = (Valor / 30.4) * NumeroDeDiasPagados
                'ElseIf datos.Rows(0)("tipo_descuento") = 2 Then
                '    Valor = datos.Rows(0)("valor_descuento")
                '    DescuentoInvonavit = ((Valor * SalarioMinimoDiarioGeneral) / 30.4) * NumeroDeDiasPagados
                'ElseIf datos.Rows(0)("tipo_descuento") = 3 Then
                '    Valor = datos.Rows(0)("valor_descuento")
                '    DescuentoInvonavit = ((SalarioDiarioIntegradoTrabajador * (Valor / 100)) * NumeroDeDiasPagados)
                'End If

                QuitarConcepto(64, NoEmpleado)

                dt = New DataTable

                Dim Nomina As New Entities.Nomina()
                'Nomina.IdEmpresa = Session("clienteid")
                Nomina.TipoNomina = 3 'Quincenal
                Nomina.Periodo = periodoId.Value
                dt = Nomina.ConsultarEmpleadosSemanal()
                Nomina = Nothing

                If dt.Rows.Count > 0 Then
                    ImporteDiario = dt.Rows(0)("CuotaDiaria")
                    ImportePeriodo = dt.Rows(0)("CuotaDiaria") * 14
                End If
                GuardarRegistro(NoEmpleado, 1, DescuentoInvonavit, 64, 1, IdContrato)
            End If
            ''''''
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
        Catch oExcep As Exception
            MsgBox(oExcep.Message)

        End Try
    End Sub
    Private Sub QuitarConcepto(ByVal NumeroConcepto As Int32, ByVal NoEmpleado As Int32)
        Try

            Call CargarVariablesGenerales()

            Dim cNomina As New Nomina()

            If NumeroConcepto <= 50 Then
                'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO=" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO=" + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=85 AND TIPOCONCEPTO='P'"
                'cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 3 'Quincenal
                cNomina.Periodo = periodoId.Value
                cNomina.NoEmpleado = NoEmpleado
                cNomina.CvoConcepto = NumeroConcepto
                cNomina.TipoConcepto = "P"
                cNomina.EliminaConceptoEmpleado()
            ElseIf NumeroConcepto >= 61 And NumeroConcepto <= 87 Or NumeroConcepto = "57" Or NumeroConcepto = "58" Or NumeroConcepto = "59" Or NumeroConcepto = "161" Or NumeroConcepto = "162" Then
                'Deducciones
                'cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 3 'Quincenal
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
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 3 'Quincenal
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
    Private Sub CalcularImpuesto()
        Try
            Impuesto = 0
            Dim ImporteQuincenal As Decimal
            ImporteQuincenal = ImporteDiario * (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo + DiasHonorarioAsimilado)
            Dim dt As New DataTable()
            Dim TarifaSemanal As New TarifaSemanal()
            TarifaSemanal.ImporteSemanal = ImporteQuincenal
            dt = TarifaSemanal.ConsultarTarifa()

            If dt.Rows.Count > 0 Then
                Impuesto = ((ImporteQuincenal - dt.Rows(0).Item("LimiteInferior")) * (dt.Rows(0).Item("PorcSobreExcli") / 100)) + dt.Rows(0).Item("CuotaFija")
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
    '        Dim dt As New DataTable()
    '        Dim TablaSubsidioDiario As New TablaSubsidioDiario()
    '        TablaSubsidioDiario.ImporteDiario = ImporteDiario
    '        dt = TablaSubsidioDiario.ConsultarSubsidioDiario()

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
            If HonorarioAsimilado > 0 And ImporteGravado < HonorarioAsimilado Then
                ImporteGravado = ImporteGravado - HonorarioAsimilado
                ImporteDiario = ImporteGravado / (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo)
            End If

            Subsidio = 0
            Dim ImporteQuincenal As Decimal
            ImporteQuincenal = ImporteDiario * (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo + DiasHonorarioAsimilado)
            Dim dt As New DataTable()
            Dim TablaSubsidioSemanal As New TablaSubsidioSemanal()
            TablaSubsidioSemanal.ImporteSemanal = ImporteQuincenal
            dt = TablaSubsidioSemanal.ConsultarSubsidio()

            If dt.Rows.Count > 0 Then
                Subsidio = dt.Rows(0).Item("Subsidio")
            End If
            If HonorarioAsimilado > 0 Then
                ImporteGravado = ImporteGravado + HonorarioAsimilado
                ImporteDiario = ImporteGravado / (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo + DiasHonorarioAsimilado)
            End If

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CalcularImss()
        Imss = 0
        ''''''''' Pendiente leer '''''''''
        'SalarioMinimoDiarioGeneral = 73.04

        If ImporteDiario <= SalarioMinimoDiarioGeneral Then
            Imss = 0
        ElseIf ImporteDiario > SalarioMinimoDiarioGeneral And ImporteDiario < (SalarioMinimoDiarioGeneral * 3) Then
            Imss = SalarioDiarioIntegradoTrabajador * 0.02375
        ElseIf ImporteDiario > (SalarioMinimoDiarioGeneral * 3) And ImporteDiario < (SalarioMinimoDiarioGeneral * 25) Then
            Imss = SalarioDiarioIntegradoTrabajador * 0.02375
            Imss = Imss + ((SalarioDiarioIntegradoTrabajador - (SalarioMinimoDiarioGeneral * 3)) * 0.004)
        ElseIf ImporteDiario > (SalarioMinimoDiarioGeneral * 25) Then
            Imss = (SalarioDiarioIntegradoTrabajador - (SalarioMinimoDiarioGeneral * 25)) * 0.02375
            Imss = Imss + ((SalarioDiarioIntegradoTrabajador - (SalarioMinimoDiarioGeneral * 22)) * 0.004)
        End If
    End Sub
    Private Sub GuardarRegistro(ByVal NoEmpleado As Int64, ByVal ConImpuesto As Int32, ByVal ImporteIncidencia As Decimal, ByVal CvoConcepto As Int32, ByVal UnidadIncidencia As Decimal, ByVal IdContrato As Integer)
        Try
            Call CargarVariablesGenerales()

            'ConImpuesto = 1 cuando viene de agregar una percepcion y calcular impuestos guardando ambos
            'ConImpuesto = 2 cuando viene de quitar una percepcion y solo se procede a guardar los impuesto modificados sin esa percepcion

            Dim cNomina As New Nomina()

            If ConImpuesto = 1 Then
                If CvoConcepto <= 50 Then
                    'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CvoConcepto, TIPOCONCEPTO, UNIDAD, Importe, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', '" + cmbConcepto.SelectedValue.ToString + "', 'P', '" + txtUnidadIncidencia.Text + "', " + txtImporteIncidencia.Text + ", 'N', 'N', 'N', 'A', 0, 0)"
                    cNomina = New Nomina()
                    'cNomina.IdEmpresa = IdEmpresa
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 3 'Quincenal
                    cNomina.Periodo = Periodo
                    cNomina.NoEmpleado = NoEmpleado
                    cNomina.CvoConcepto = CvoConcepto
                    cNomina.IdContrato = IdContrato
                    cNomina.TipoConcepto = "P"
                    cNomina.Unidad = UnidadIncidencia
                    cNomina.Importe = ImporteIncidencia
                    cNomina.ImporteGravado = 0
                    cNomina.ImporteExento = 0
                    cNomina.Generado = ""
                    cNomina.Timbrado = ""
                    cNomina.Enviado = ""
                    cNomina.Situacion = "A"
                    cNomina.GuadarNomina()
                ElseIf CvoConcepto = 57 Or CvoConcepto = 58 Or CvoConcepto = 59 Or CvoConcepto = 161 Or CvoConcepto = 162 Then
                    'Deducciones por faltas, permisos o incapacidades
                    'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CvoConcepto, TIPOCONCEPTO, UNIDAD, Importe, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', '" + CboConceptos.SelectedValue.ToString + "', 'D', '" + TxtUnidadIncidencia.Text + "', " + TxtImporteIncidencia.Text + ", 'N', 'N', 'N', 'A', 0, " + TxtImporteIncidencia.Text + ")"
                    cNomina = New Nomina()
                    'cNomina.IdEmpresa = IdEmpresa
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 3 'Quincenal
                    cNomina.Periodo = Periodo
                    cNomina.NoEmpleado = NoEmpleado
                    cNomina.CvoConcepto = CvoConcepto
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
                    cNomina.GuadarNomina()
                ElseIf CvoConcepto >= 61 Then
                    'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CvoConcepto, TIPOCONCEPTO, UNIDAD, Importe, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', '" + CboConceptos.SelectedValue.ToString + "', 'D', '" + TxtUnidadIncidencia.Text + "', " + TxtImporteIncidencia.Text + ", 'N', 'N', 'N', 'A', 0, " + TxtImporteIncidencia.Text + ")"
                    cNomina = New Nomina()
                    'cNomina.IdEmpresa = IdEmpresa
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 3 'Quincenal
                    cNomina.Periodo = Periodo
                    cNomina.NoEmpleado = NoEmpleado
                    cNomina.CvoConcepto = CvoConcepto
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
                    cNomina.GuadarNomina()
                End If
            End If

            'Aqui se guarda el impuesto, el total Gravado y el total exento, tanto cuando viene de agregar un concepto como cuando viene de quitar un concepto ya que de ambas maneras se recalcula
            'solo no entra en este bloque de codigo cuando viene de agregar una deduccion que no implica recalcular gravado, exento o impuesto(las unicas deducciones que recalculan gravado, exento e impuesto son la faltas, permisos e incapacidades, las demas deduccioens haciendo hincapie, no entran en este bloque)
            If ConImpuesto = 1 Then
                If CvoConcepto < 51 Or CvoConcepto = 57 Or CvoConcepto = 58 Or CvoConcepto = 59 Or CvoConcepto = 161 Or CvoConcepto = 162 Then
                    'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CvoConcepto, TIPOCONCEPTO, UNIDAD, Importe, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', 52, 'D', 1, " + Impuesto.ToString + ", 'N', 'N', 'N', 'A', 0, " + Impuesto.ToString + ")"
                    cNomina = New Nomina()
                    'cNomina.IdEmpresa = IdEmpresa
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 3 'Quincenal
                    cNomina.Periodo = Periodo
                    cNomina.NoEmpleado = NoEmpleado
                    cNomina.CvoConcepto = 86
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
                    cNomina.GuadarNomina()

                    If SubsidioAplicado > 0 Then
                        'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CvoConcepto, TIPOCONCEPTO, UNIDAD, Importe, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', 54, 'P', 1, " + SubsidioAplicado.ToString + ", 'N', 'N', 'N', 'A', 0, " + SubsidioAplicado.ToString + ")"
                        cNomina = New Nomina()
                        'cNomina.IdEmpresa = IdEmpresa
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 3 'Quincenal
                        cNomina.Periodo = Periodo
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
                        cNomina.GuadarNomina()
                    End If
                    If SubsidioEfectivo > 0 Then
                        'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CvoConcepto, TIPOCONCEPTO, UNIDAD, Importe, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', 55, 'P', 1, " + SubsidioEfectivo.ToString + ", 'N', 'N', 'N', 'A', 0, " + SubsidioEfectivo.ToString + ")"
                        cNomina = New Nomina()
                        'cNomina.IdEmpresa = IdEmpresa
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 3 'Quincenal
                        cNomina.Periodo = Periodo
                        cNomina.NoEmpleado = NoEmpleado
                        cNomina.CvoConcepto = 55
                        cNomina.IdContrato = IdContrato
                        cNomina.TipoConcepto = "P"
                        cNomina.Unidad = 1
                        cNomina.Importe = SubsidioEfectivo
                        cNomina.ImporteGravado = 0
                        cNomina.ImporteExento = SubsidioEfectivo
                        cNomina.Generado = ""
                        cNomina.Timbrado = ""
                        cNomina.Enviado = ""
                        cNomina.Situacion = "A"
                        cNomina.GuadarNomina()
                    End If
                    If Imss > 0 Then
                        'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CvoConcepto, TIPOCONCEPTO, UNIDAD, Importe, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', 56, 'D', 1, " + Imss.ToString + ", 'N', 'N', 'N', 'A', 0, " + Imss.ToString + ")"
                        cNomina = New Nomina()
                        'cNomina.IdEmpresa = IdEmpresa
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 3 'Quincenal
                        cNomina.Periodo = Periodo
                        cNomina.NoEmpleado = NoEmpleado
                        cNomina.CvoConcepto = 56
                        cNomina.IdContrato = IdContrato
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
                End If
            ElseIf ConImpuesto = 2 Then
                If CvoConcepto < 51 Or CvoConcepto = 57 Or CvoConcepto = 58 Or CvoConcepto = 59 Or CvoConcepto = 58 Or CvoConcepto = 59 Or CvoConcepto = 161 Or CvoConcepto = 162 Then
                    'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CvoConcepto, TIPOCONCEPTO, UNIDAD, Importe, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', 52, 'D', 1, " + Impuesto.ToString + ", 'N', 'N', 'N', 'A', 0, " + Impuesto.ToString + ")"
                    cNomina = New Nomina()
                    'cNomina.IdEmpresa = IdEmpresa
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 3 'Quincenal
                    cNomina.Periodo = Periodo
                    cNomina.NoEmpleado = NoEmpleado
                    cNomina.CvoConcepto = 86
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
                    cNomina.GuadarNomina()
                    If SubsidioAplicado > 0 Then
                        'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CvoConcepto, TIPOCONCEPTO, UNIDAD, Importe, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', 54, 'P', 1, " + SubsidioAplicado.ToString + ", 'N', 'N', 'N', 'A', 0, " + SubsidioAplicado.ToString + ")"
                        cNomina = New Nomina()
                        'cNomina.IdEmpresa = IdEmpresa
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 3 'Quincenal
                        cNomina.Periodo = Periodo
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
                        cNomina.GuadarNomina()
                    End If
                    If SubsidioEfectivo > 0 Then
                        'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CvoConcepto, TIPOCONCEPTO, UNIDAD, Importe, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', 55, 'P', 1, " + SubsidioEfectivo.ToString + ", 'N', 'N', 'N', 'A', 0, " + SubsidioEfectivo.ToString + ")"
                        cNomina = New Nomina()
                        'cNomina.IdEmpresa = IdEmpresa
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 3 'Quincenal
                        cNomina.Periodo = Periodo
                        cNomina.NoEmpleado = NoEmpleado
                        cNomina.CvoConcepto = 55
                        cNomina.IdContrato = IdContrato
                        cNomina.TipoConcepto = "P"
                        cNomina.Unidad = 1
                        cNomina.Importe = SubsidioEfectivo
                        cNomina.ImporteGravado = 0
                        cNomina.ImporteExento = SubsidioEfectivo
                        cNomina.Generado = ""
                        cNomina.Timbrado = ""
                        cNomina.Enviado = ""
                        cNomina.Situacion = "A"
                        cNomina.GuadarNomina()
                    End If
                    If Imss > 0 Then
                        'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CvoConcepto, TIPOCONCEPTO, UNIDAD, Importe, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', 56, 'D', 1, " + Imss.ToString + ", 'N', 'N', 'N', 'A', 0, " + Imss.ToString + ")"
                        cNomina = New Nomina()
                        'cNomina.IdEmpresa = IdEmpresa
                        cNomina.Ejercicio = IdEjercicio
                        cNomina.TipoNomina = 3 'Quincenal
                        cNomina.Periodo = Periodo
                        cNomina.NoEmpleado = NoEmpleado
                        cNomina.CvoConcepto = 56
                        cNomina.IdContrato = IdContrato
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
                End If
            End If

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub SolicitarGeneracionXml(ByVal NoEmpleado As Int64, ByVal Generado As String)

        Call CargarVariablesGenerales()

        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 3 'Quincenal
        cNomina.Tipo = "N"
        cNomina.Periodo = periodoId.Value
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Generado = Generado
        cNomina.ActualizarEstatusGeneradoNomina()
        cNomina = Nothing

    End Sub
    Private Sub AgregaConcepto(ByVal ImporteIncidencia, ByVal UnidadIncidencia, ByVal CuotaPeriodo, ByVal IntegradoIMSS, ByVal NoEmpleado, ByVal CvoConcepto, ByVal IdContrato)

        Try
            ImporteIncidencia = Convert.ToDecimal(ImporteIncidencia)
        Catch ex As Exception
            ImporteIncidencia = 0
        End Try
        Try
            UnidadIncidencia = Convert.ToDecimal(UnidadIncidencia)
        Catch ex As Exception
            UnidadIncidencia = 0
        End Try

        Try
            CuotaPeriodo = Convert.ToDecimal(CuotaPeriodo)
        Catch ex As Exception
            CuotaPeriodo = 0
        End Try

        Try
            IntegradoIMSS = Convert.ToDecimal(IntegradoIMSS)
        Catch ex As Exception
            IntegradoIMSS = 0
        End Try

        If CvoConcepto = 57 Then 'Faltas
            ImporteIncidencia = CuotaPeriodo * UnidadIncidencia
        End If

        If ImporteIncidencia <= 0 Then
            rwAlerta.RadAlert("Favor de digitar un importe!!", 330, 180, "Alerta", "", "")
            Exit Sub
        ElseIf UnidadIncidencia <= 0 Then
            rwAlerta.RadAlert("Favor de digitar un unidad!!", 330, 180, "Alerta", "", "")
            Exit Sub
        End If

        Dim ClaveRegimenContratacion As Integer = 0

        Dim cEmpleado As New Entities.Empleado
        cEmpleado.IdEmpleado = NoEmpleado
        cEmpleado.ConsultarEmpleadoID()
        If cEmpleado.IdEmpleado > 0 Then
            ClaveRegimenContratacion = cEmpleado.IdRegimencontratacion
        End If

        If CvoConcepto.ToString = "5" And ClaveRegimenContratacion <> 9 Then
            rwAlerta.RadAlert("El regimen de contratacion de este trabajador no es honorarios asimilado a salarios!!", 330, 180, "Alerta", "", "")
            Exit Sub
        ElseIf CvoConcepto < 51 Or CvoConcepto.ToString = "57" Or CvoConcepto.ToString = "58" Or CvoConcepto.ToString = "59" Or CvoConcepto.ToString = "161" Or CvoConcepto.ToString = "162" Then
            If ClaveRegimenContratacion = 9 Then
                rwAlerta.RadAlert("El régimen de contratación de este trabajador es asimilado a salarios, por lo mismo no se le debe agregar ningún otro tipo de percepción ni hacerle deducciones por faltas, dichas deducciones solo pueden ser por un aduedo distinto!!", 330, 180, "Alerta", "", "")
                Exit Sub
            End If
        End If

        If CvoConcepto.ToString = "6" Then
            If UnidadIncidencia > 9 Then
                rwAlerta.RadAlert("Las horas extras no pueden ser mas de 9!!", 330, 180, "Alerta", "", "")
                Exit Sub
            End If
        End If

        If UnidadIncidencia > 0 Then

            If ChecarSiExiste(NoEmpleado, CvoConcepto) = True Then
                rwAlerta.RadAlert("Esa percepción/deducción ya existe!!", 330, 180, "Alerta", "", "")
                Exit Sub
            End If
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            'pasos para agregar faltas y permisos
            'checar que exista la cuota del periodo, es decir el sueldo y que la deduccion sea menor al importe existente
            'checar si ya existen mas deducciones agregadas anteriormente por faltas, permisos o incapacidades
            'restarle al sueldo la cantidad correspondiente a las faltas, permisos o incapacidades y a los 7 dias del periodo, los dias correspondientes a las faltas
            'proceder normalmente para el recalculo del impuesto y guardado de la clave de la deduccion
            'deducciones
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            If CvoConcepto.ToString = "57" Or CvoConcepto.ToString = "58" Or CvoConcepto.ToString = "59" Or CvoConcepto.ToString = "161" Or CvoConcepto.ToString = "162" Then
                'si es descuento ya sea por falta, permiso o incapacidad checar si existe el concepto de cuota del periodo ya que de el se debe descontar estas deducciones
                If ChecarQueExistaLaCuotaPeriodo(NoEmpleado, ImporteIncidencia, UnidadIncidencia) = False Then
                    rwAlerta.RadAlert("No existe la cuota del periodo o el importe del mismo es menor al importe de la deduccion o los dias a descontar son menores a los existentes, no se puede agregar esta deduccion!!", 330, 180, "Alerta", "", "")
                    Exit Sub
                End If
            End If
            'ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            ImporteDiario = 0
            ImportePeriodo = 0
            ImporteExento = 0
            ImporteGravado = 0
            SubsidioAplicado = 0
            SubsidioEfectivo = 0
            Agregar = 1
            'IMSS
            SalarioDiarioIntegradoTrabajador = IntegradoIMSS
            'oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            'Pasos para agregar una percepcion
            'Checar si la incidencia es exenta o gravada para ver si procede el calculo O se guarda el registro directo sin pasar por el calculo
            'Borrar deducciones menos el imss del empleado
            'iterar sobre las percepciones e ir haciendo el calculo de exentos y gravados sumando conceptos
            'hacer el calculo del ispt
            'guardar las nuevas deducciones
            'cargar las nuevas percepciones y deducciones
            'oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
            ChecarSiExistenDiasEnPercepciones(NoEmpleado, CvoConcepto, ImporteIncidencia, UnidadIncidencia)
            If DiasCuotaPeriodo = 0 And DiasVacaciones = 0 And DiasComision = 0 And DiasPagoPorHoras = 0 And DiasDestajo = 0 And DiasHonorarioAsimilado = 0 Then
                'En este caso, si se pagan conceptos tales como comisiones, en teoria estas se generan posterior a que el trabajador tiene un sueldo, es decir una cuaota del periodo actual, ese es el motivo del mensaje.
                'rwAlerta.RadAlert("Esta percepcion no puede agregarse sin que exista alguna de las siguientes:" + vbCrLf + "CuotaPeriodo" + vbCrLf + "Vacaciones" + vbCrLf + "HonorarioAsimilado" + vbCrLf + "PagoPorHoras" + vbCrLf + "Comision" + vbCrLf + "Destajo" + vbCrLf + "pues estas van acompañadas implicitamente por los 7 dias correspondientes al periodo semanal lo cual es base necesaria para el calculo del impuesto,  lo que si puede hacer es cambiar el numero de dias o eliminar completamente el empleado en este periodo!!!", 330, 180, "Alerta", "", "")
                BorrarDeducciones(NoEmpleado)
            End If
            If CvoConcepto.ToString < 51 Or CvoConcepto.ToString = "57" Or CvoConcepto.ToString = "58" Or CvoConcepto.ToString = "59" Or CvoConcepto.ToString = "161" Or CvoConcepto.ToString = "162" Then
                'En este bloque no entran las deducciones por adeudos que no cambian la base del impuesto y por lo tanto no lo calculan, ejemplos de esto serian, adeudos por credito infonavit, cuotas sindicales, adeudos fonacot, adeudos con el patron, etc, (conceptos del 61 al 86)
                BorrarDeducciones(NoEmpleado)
                ChecarPercepcionesGravadas(NoEmpleado, CvoConcepto, ImporteIncidencia, UnidadIncidencia)
                CalcularImpuesto()
                CalcularSubsidio()

                CalcularImss()
                Imss = Imss * (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo + DiasHonorarioAsimilado)
                Imss = Math.Round(Imss, 6)
                'Impuesto = Impuesto * (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo + DiasHonorarioAsimilado)
                'Subsidio = Subsidio * (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo)
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

            'Impuesto = Math.Round(Impuesto, 2)
            'SubsidioAplicado = Math.Round(SubsidioAplicado, 2)
            'SubsidioEfectivo = Math.Round(SubsidioEfectivo, 2)
            GuardarRegistro(NoEmpleado, 1, ImporteIncidencia, CvoConcepto, UnidadIncidencia, IdContrato)
            'Catalogo fusionado
            '*******************************************************************************************************
            '*******************************************************************************************************
            ChecarYGrabarPercepcionesExentasYGravadas(NoEmpleado, IdContrato)
            '*******************************************************************************************************
            '*******************************************************************************************************
            SolicitarGeneracionXml(NoEmpleado, "")

            'ChecarPercepcionesExentasYGravadas()


        End If
    End Sub
    Sub txtComisiones_TextChanged(sender As Object, e As EventArgs)
        Dim NoEmpleado = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("NoEmpleado")
        Dim CuotaPeriodo = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("CuotaPeriodo")
        Dim IntegradoIMSS = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IntegradoIMSS")
        Dim IdContrato = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IdContrato")

        Dim txtComisiones As RadNumericTextBox = DirectCast(sender, RadNumericTextBox)

        AgregaConcepto(txtComisiones.Text, 1, CuotaPeriodo, IntegradoIMSS, NoEmpleado, 3, IdContrato)

        Call CargarDatos()
    End Sub
    Sub txtFaltas_TextChanged(sender As Object, e As EventArgs)
        Dim NoEmpleado = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("NoEmpleado")
        Dim CuotaPeriodo = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("CuotaPeriodo")
        Dim IntegradoIMSS = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IntegradoIMSS")
        Dim IdContrato = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IdContrato")

        Dim txtFaltas As RadNumericTextBox = DirectCast(sender, RadNumericTextBox)

        AgregaConcepto(0, txtFaltas.Text, CuotaPeriodo, IntegradoIMSS, NoEmpleado, 57, IdContrato)

        Call CargarDatos()
    End Sub
    Sub txtHorasDobles_TextChanged(sender As Object, e As EventArgs)
        Dim NoEmpleado = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("NoEmpleado")
        Dim CuotaPeriodo = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("CuotaPeriodo")
        Dim IntegradoIMSS = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IntegradoIMSS")
        Dim IdContrato = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IdContrato")

        Dim txtHorasDobles As RadNumericTextBox = DirectCast(sender, RadNumericTextBox)

        Dim Importe As Decimal = 0
        Importe = ((CuotaPeriodo / 8) * 2) * txtHorasDobles.Text

        AgregaConcepto(Importe, txtHorasDobles.Text, CuotaPeriodo, IntegradoIMSS, NoEmpleado, 6, IdContrato)

        Call CargarDatos()
    End Sub
    Sub txtHorasTriples_TextChanged(sender As Object, e As EventArgs)
        Dim NoEmpleado = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("NoEmpleado")
        Dim CuotaPeriodo = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("CuotaPeriodo")
        Dim IntegradoIMSS = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IntegradoIMSS")
        Dim IdContrato = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IdContrato")

        Dim txtHorasTriples As RadNumericTextBox = DirectCast(sender, RadNumericTextBox)

        Dim Importe As Decimal = 0
        Importe = ((CuotaPeriodo / 8) * 3) * txtHorasTriples.Text

        AgregaConcepto(Importe, txtHorasTriples.Text, CuotaPeriodo, IntegradoIMSS, NoEmpleado, 7, IdContrato)

        Call CargarDatos()
    End Sub
    Sub txtPremioAsistencia_TextChanged(sender As Object, e As EventArgs)
        Dim NoEmpleado = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("NoEmpleado")
        Dim CuotaPeriodo = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("CuotaPeriodo")
        Dim IntegradoIMSS = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IntegradoIMSS")
        Dim IdContrato = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IdContrato")

        Dim txtPremioAsistencia As RadNumericTextBox = DirectCast(sender, RadNumericTextBox)

        AgregaConcepto(txtPremioAsistencia.Text, 1, CuotaPeriodo, IntegradoIMSS, NoEmpleado, 23, IdContrato)

        Call CargarDatos()
    End Sub
    Sub txtPremioPuntualidad_TextChanged(sender As Object, e As EventArgs)
        Dim NoEmpleado = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("NoEmpleado")
        Dim CuotaPeriodo = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("CuotaPeriodo")
        Dim IntegradoIMSS = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IntegradoIMSS")
        Dim IdContrato = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IdContrato")

        Dim txtPremioPuntualidad As RadNumericTextBox = DirectCast(sender, RadNumericTextBox)

        AgregaConcepto(txtPremioPuntualidad.Text, 1, CuotaPeriodo, IntegradoIMSS, NoEmpleado, 24, IdContrato)

        Call CargarDatos()
    End Sub
    Sub txtPrimaDominical_TextChanged(sender As Object, e As EventArgs)
        Dim NoEmpleado = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("NoEmpleado")
        Dim CuotaPeriodo = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("CuotaPeriodo")
        Dim IntegradoIMSS = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IntegradoIMSS")
        Dim IdContrato = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IdContrato")

        Dim txtPrimaDominical As RadNumericTextBox = DirectCast(sender, RadNumericTextBox)

        Dim Importe As Decimal = 0
        Importe = (CuotaPeriodo / 4) * txtPrimaDominical.Text

        AgregaConcepto(Importe, txtPrimaDominical.Text, CuotaPeriodo, IntegradoIMSS, NoEmpleado, 13, IdContrato)

        Call CargarDatos()
    End Sub
    Private Sub grdEmpleadosQuincenal_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles grdEmpleadosQuincenal.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                empleadoId.Value = e.CommandArgument
                rwConfirmEliminaEmpleado.RadConfirm("¿Realmente desea eliminar al trabajador de esta nomina?", "confirmCallbackFnEliminaEmpleado", 330, 180, Nothing, "Confirmar")
        End Select
    End Sub
    Private Sub btnEliminarEmpleado_Click(sender As Object, e As EventArgs) Handles btnEliminarEmpleado.Click
        Call EliminarTrabajador()
        Call CargarGridEmpleadosQuincenal()
    End Sub
    Private Sub EliminarTrabajador()
        Try

            Call CargarVariablesGenerales()

            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 3 'Quincenal
            cNomina.Periodo = periodoId.Value
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.EliminaEmpleado()

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        If Not String.IsNullOrEmpty(Request("id")) Then
            Response.Redirect("~/GeneracionDeNominaQuincenalNormal.aspx?id=" & periodoId.Value.ToString, False)
        Else
            Response.Redirect("~/GeneracionDeNominaQuincenalNormal.aspx", False)
        End If
    End Sub

End Class