﻿Imports Telerik.Web.UI
Imports Entities
Imports System.Xml
Imports System.IO
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography
Imports Telerik.Reporting.Processing
Imports ThoughtWorks.QRCode.Codec
Imports System.Net.Mail
Imports Ionic.Zip
Imports System.Web.Services.Protocols
Imports System.Net.Security
Imports System.Data.SqlClient
Imports System.Data
Imports System.Globalization

Public Class GeneracionDeNominaExtraordinaria
    Inherits System.Web.UI.Page
    Dim ObjData As New DataControl()

    Private ImporteExento As Double
    Private ImporteGravado As Double
    Private ImporteDiario As Double
    Private ImportePeriodo As Double
    Private ImporteSeguroVivienda As Double
    Private Subsidio As Double

    Private IdEmpresa As Integer = 0
    Private IdEjercicio As Integer = 0
    Private SalarioDiarioIntegradoTrabajador As Double
    Private SalarioMinimoDiarioGeneral As Double
    Private UMA As Double
    Private IMSS As Double
    Private dtEmpleados As DataTable

    Private DiasVacaciones As Integer
    Private DiasCuotaPeriodo As Integer
    Private DiasHonorarioAsimilado As Integer
    Private DiasPagoPorHoras As Integer
    Private DiasComision As Integer
    Private DiasDestajo As Integer
    Private DiasFaltasPermisosIncapacidades As Integer
    Private SubsidioAbsolutoTotal As Double

    Private TotalPercepciones As Double
    Private TotalDeducciones As Double
    Private NetoAPagar As Double
    Private PercepcionesGravadas As Double
    Private PercepcionesExentas As Double
    Private Mensaje As String
    Private MensajeNulo As String

    Public CuotaPeriodo As Double
    Public HorasTriples As Double
    Public DescansoTrabajado As Double
    Public PrimaDominical As Double
    Public PrimaVacacional As Double
    Public Vacaciones As Double
    Public Aguinaldo As Double
    Public RepartoUtilidades As Double
    Public FondoAhorro As Double
    Public AyudaFuneral As Double
    Public PrevisionSocial As Double
    Public GrupoPercepcionesGravadasTotalmenteSinExentos As Double
    Public PagoPorHoras As Double
    Public Comisiones As Double
    Public Destajo As Double
    Public FaltasPermisosIncapacidades As Double
    Public NumeroDeDiasPagados As Double

    Public TiempoExtraordinarioDentroDelMargenLegal As Double
    Public TiempoExtraordinarioFueraDelMargenLegal As Double
    Public HonorarioAsimilado As Double

    Public HorasDoblesGravadas As Double
    Public HorasDoblesExentas As Double
    Public FestivoTrabajadoGravado As Double
    Public FestivoTrabajadoExento As Double
    Public DobleteGravado As Double
    Public DobleteExento As Double

    Public ImporteExentoTiempoExtraordinarioDentroDelMargenLegal As Double
    Public ImporteGravadoTiempoExtraordinarioDentroDelMargenLegal As Double
    Public ImporteExentoPrimaDominical As Double
    Public ImporteGravadoPrimaDominical As Double
    Public ImporteExentoAguinaldo As Double
    Public ImporteGravadoAguinaldo As Double
    Public ImporteExentoPrimaVacacional As Double
    Public ImporteGravadoPrimaVacacional As Double
    Public ImporteExentoRepartoUtilidades As Double
    Public ImporteGravadoRepartoUtilidades As Double
    Public ImporteExentoPrevisionSocial As Double
    Public ImporteGravadoPrevisionSocial As Double

    Public AyudaCulturalExento As Double
    Public AyudaCulturalGravado As Double
    Public AyudaDeportivaExento As Double
    Public AyudaDeportivaGravado As Double
    Public AyudaEducacionalExento As Double
    Public AyudaEducacionalGravado As Double
    Public AyudaEscolarExento As Double
    Public AyudaEscolarGravado As Double
    Public AyudaComidaExento As Double
    Public AyudaComidaGravado As Double
    Public ValesDespensaExento As Double
    Public ValesDespensaGravado As Double
    Public AyudaUniformeExento As Double
    Public AyudaUniformeGravado As Double
    Public BecasExento As Double
    Public BecasGravado As Double
    Public SubsidioIncapacidadExento As Double
    Public SubsidioIncapacidadGravado As Double
    Public AyudaMatrimonioExento As Double
    Public AyudaMatrimonioGravado As Double
    Public AyudaNacimientoExento As Double
    Public AyudaNacimientoGravado As Double
    Public ValesComedorExento As Double
    Public ValesComedorGravado As Double
    Public AyudaMedicamentoExento As Double
    Public AyudaMedicamentoGravado As Double
    Public ImporteExentoFondoAhorro As Double
    Public ImporteGravadoFondoAhorro As Double
    Public ImporteExentoAyudaFuneral As Double
    Public ImporteGravadoAyudaFuneral As Double

    Public Diferencias As Double
    Public Gratificacion As Double
    Public Bonificacion As Double
    Public Retroactivo As Double
    Public BonoProduccion As Double
    Public PremioProductividad As Double
    Public Incentivo As Double
    Public PremioAsistencia As Double
    Public PremioPuntualidad As Double
    Public Premio As Double
    Public Compensacion As Double
    Public BonoAntiguedad As Double
    Public Viaticos As Double
    Public Pasajes As Double
    Public AyudaTransporte As Double
    Public AyudaRenta As Double
    Public AyudaCarestia As Double
    Public DespensaEfectivo As Double
    Public HaberPorRetiro As Double

    Private Impuesto As Double
    Public IsrIndemnizacion As Double
    Public SubsidioAplicado As Double
    Public SubsidioEfectivo As Double
    Public CuotaImss As Double
    Public Faltas As Double
    Public Permisos As Double
    Public IncapacidadPorEnfermedad As Double
    Public DiasHorasNoTrabajadas As Double
    Public CuotaSindical As Double
    Public PensionAlimenticia As Double
    Public AdeudoFonacot As Double
    Public CreditoInfonavit As Double
    Public AdeudoCajaAhorro As Double
    Public AdeudoFondoAhorro As Double
    Public GastosSinComprobar As Double
    Public PagoIndebido As Double
    Public Anticipos As Double
    Public AdeudoSindical As Double
    Public AdeudoPatron As Double
    Public ViaticosDeduccion As Double
    Public AportacionCajaAhorro As Double
    Public AportacionFondoAhorro As Double
    Public AportacionDespensa As Double
    Public AportacionComedor As Double
    Public AportacionVoluntariaSar As Double
    Public AportacionVoluntariaInfonavit As Double
    Public FondoRetiro As Double
    Public PrimaSeguroVida As Double
    Public PrimaSeguroAuto As Double
    Public PrimaGastosMedicos As Double
    Public DevolucionIsr As Double
    Public AjusteIsr As Double
    Public AdeudoCuotaImss As Double
    Public OtrasDeducciones As Double
    Public IncapacidadPorMaternidad As Double
    Public IncapacidadPorAccidente As Double
    Public FolioXml As String

    Public NoEmpleadoImportarIndividual As Integer

    Const URI_SAT = "http://www.sat.gob.mx/cfd/4"
    Private m_xmlDOM As New XmlDocument
    Private urlnomina As String
    Private TablaUnidad As DataTable
    Private Unidad As String
    Private Agregar As Integer
    Private data As Byte()
    Private qrBackColor As Integer = System.Drawing.Color.FromArgb(255, 255, 255, 255).ToArgb
    Private qrForeColor As Integer = System.Drawing.Color.FromArgb(255, 0, 0, 0).ToArgb

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Console.Write(Format(Now()))

        Me.WinPeriodoSave.VisibleOnPageLoad = False
        Me.WinImportarMonto.VisibleOnPageLoad = False
        Me.WinImportarMontoIndividual.VisibleOnPageLoad = False

        If Not IsPostBack Then
            Dim objCat As New DataControl(1)
            Dim cConcepto As New Entities.Catalogos
            objCat.CatalogoRad(cmbCliente, cConcepto.ConsultarMisClientes, True, False)
            objCat.CatalogoRad(cmbPeriodicidad, cConcepto.ConsultarPeriodoPago2, True, False)
            'Call CargaPeriodos(0, 0)
            objCat = Nothing

            If Session("Folio") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Session("Folio").ToString()) Then
                Dim folio As Integer = Integer.Parse(Session("Folio").ToString())
                nominaID.Value = folio.ToString()

                ConsultarFolio(folio)
                cmbCliente.Enabled = False
                cmbPeriodicidad.Enabled = False
                cmbPeriodo.Enabled = False
                cmbPeriodo_SelectedIndexChanged(cmbPeriodo, EventArgs.Empty)
                'CargaPeriodos(cmbPeriodicidad.SelectedValue, folio)
                'cmbPeriodo.SelectedValue = Session("PeriodoIDNomina")

                ' Eliminar el folio de la sesión después de haberlo utilizado
                'Session.Remove("Folio")
                'Session("Folio") = Nothing
            ElseIf Not String.IsNullOrEmpty(Request("id")) And Not String.IsNullOrEmpty(Request("nid")) Then
                periodoID.Value = Request("id")
                nominaID.Value = Request("nid")
                cmbPeriodicidad.SelectedValue = Session("PeriodicidadIDNomina")
                'Call LlenaComboPeriodosSemanal(periodoID.Value)
                cmbCliente.SelectedValue = Session("ClienteIDNomina")
                CargaPeriodos(cmbPeriodicidad.SelectedValue, Session("PeriodoIDNomina"))
                cmbPeriodo.SelectedValue = Request("id")

                Dim cPeriodo As New Periodo
                cPeriodo.IdPeriodo = Session("PeriodoIDNomina")
                cPeriodo.ConsultarPeriodoID()

                If IsNothing(CDate(cPeriodo.FechaPago)) Then
                    fchPago.SelectedDate = CDate(cPeriodo.FechaPago)
                End If

                Call CargarDatos()
                lblTitulo.Text = "Periodo " & cmbPeriodo.SelectedItem.Text
                btnGeneraNomina.Enabled = False
                Call BloquearBotones()
            Else
                'LlenaComboPeriodosSemanal(0)
                'Call ValidaBtnCrearNomina()
            End If
        End If

        RadProgressArea1.Localization.TotalFiles = "Total empleados"
        RadProgressArea1.Localization.UploadedFiles = "Calculados"
        RadProgressArea1.Localization.CurrentFileName = "Calculando: "
        WinPeriodoSave.VisibleOnPageLoad = False
    End Sub
    Protected Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Session("Folio") = Nothing
        Response.Redirect("ListadoNominaExtraordinaria.aspx")
    End Sub
    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click

        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Entities.Nomina
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = cmbCliente.SelectedValue
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.EsEspecial = True
        cNomina.IdNomina = nominaID.Value
        dt = cNomina.ConsultarDetalleNominaExtraordinaria()

        ' Crear el contenido CSV
        Dim csvContent As New StringBuilder()
        csvContent.Append("Clave Empleado,")
        csvContent.Append("Nombre Empleado,")
        csvContent.Append("Monto,")
        csvContent.AppendLine()

        ' Añadir filas de datos
        For Each row As DataRow In dt.Rows
            csvContent.Append(row("NoEmpleado").ToString() & ",")
            csvContent.Append(row("Empleado").ToString() & ",")
            csvContent.AppendLine()
        Next

        ' Configurar la respuesta HTTP para la descarga del archivo
        Response.Clear()
        Response.Buffer = True
        Response.BufferOutput = True
        Response.ContentType = "text/csv"
        Response.AddHeader("Content-Disposition", "attachment;filename=EmpleadosMontos.csv")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.UTF8
        Response.Write(csvContent.ToString())
        Response.Flush()
        Response.End()
    End Sub
    Protected Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        WinImportarMonto.VisibleOnPageLoad = True
        WinImportarMonto.Title = "Importar Monto"
    End Sub
    Protected Sub btnEImportarCSV_Click(sender As Object, e As EventArgs) Handles btnEImportarCSV.Click
        If ImportarFile.HasFile Then
            Try
                ' Guardar el archivo subido temporalmente en el servidor
                Dim filePath As String = Server.MapPath("~/UploadsMontos/") & ImportarFile.FileName
                ImportarFile.SaveAs(filePath)

                Dim periodicidad As String = cmbPeriodicidad.SelectedValue 'Semanal
                Dim periodo As String = cmbPeriodo.SelectedValue
                Dim cliente As String = cmbCliente.SelectedValue

                ' Leer el archivo CSV
                Using reader As New StreamReader(filePath)
                    Dim line As String
                    Dim headers() As String = reader.ReadLine().Split(","c) ' Leer la primera línea para obtener los encabezados

                    ' Leer cada línea del CSV
                    While Not reader.EndOfStream
                        line = reader.ReadLine()
                        Dim values() As String = line.Split(","c)

                        Dim empleado As String = values(0).Trim()
                        Dim monto As String = values(2).Trim()
                        ActualizarImporteNominaEnBaseDeDatos(empleado, cliente, periodicidad, periodo, monto)
                    End While
                End Using

                ' Eliminar el archivo temporal después de procesarlo
                If File.Exists(filePath) Then
                    File.Delete(filePath)
                End If

                Session("ClienteIDNomina") = cmbCliente.SelectedValue
                Session("PeriodicidadIDNomina") = cmbPeriodicidad.SelectedValue
                Session("PeriodoIDNomina") = cmbPeriodo.SelectedValue
                Response.Redirect("~/GeneracionDeNominaExtraordinaria.aspx?id=" & cmbPeriodo.SelectedValue.ToString & "&nid=" & nominaID.Value.ToString)

            Catch ex As Exception
                WinImportarMonto.VisibleOnPageLoad = False
                rwAlerta.RadAlert("Ocurrió un error durante la importación: " & ex.Message & " Verifique que sea el archivo correcto.", 330, 180, "Alerta", "", "")
            End Try

        Else
            WinImportarMonto.VisibleOnPageLoad = False
            rwAlerta.RadAlert("Por favor, seleccione un archivo CSV.", 330, 180, "Alerta", "", "")
        End If
    End Sub
    Private Sub ActualizarImporteNominaEnBaseDeDatos(id_empleado As String, id_cliente As String, id_periodicidad As String, id_periodo As String, monto As String)
        Try
            Dim objCat As New DataControl(1)
            objCat.RunSQLQuery("UPDATE tblNominas SET Importe='" & monto & "', ImporteExento ='" & monto & "' WHERE TipoNomina='" & id_periodicidad & "' AND IdEmpresa='" & id_cliente & "' AND Periodo='" & id_periodo & "' AND NoEmpleado='" & id_empleado & "' AND es_especial=1")
        Catch ex As Exception
            WinImportarMonto.VisibleOnPageLoad = False
            Response.Write("Ocurrió un error durante la importación: " & ex.Message)
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
                SalarioMinimoDiarioGeneral = oDataRow("SalarioMinimoDiarioGeneral")
                ImporteSeguroVivienda = oDataRow("ImporteSeguroVivienda")
                UMA = oDataRow("UMA")
            Next
        End If
    End Sub
    Private Sub CargarDatos()
        Try
            Call CargarVariablesGenerales()

            lnkReporte.Attributes.Add("onclick", "javascript: OpenWindow('" & cmbCliente.SelectedValue & "', '" & IdEjercicio.ToString & "','" & cmbPeriodo.SelectedValue.ToString & "','" & cmbPeriodicidad.SelectedValue.ToString & "', '1'); return false;")

            Dim dt As New DataTable()
            Dim dt_Empleado As New DataTable()
            Dim cNomina As New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = cmbCliente.SelectedValue
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.EsEspecial = True
            dt = cNomina.ConsultarDatosGeneralesNomina()

            cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = cmbCliente.SelectedValue
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.EsEspecial = True
            cNomina.IdNomina = nominaID.Value
            dt_Empleado = cNomina.ConsultarDetalleNominaExtraordinaria()

            cNomina = Nothing
            If dt.Rows.Count > 0 And dt_Empleado.Rows.Count > 0 Then
                panelDatos.Visible = True
                btnExportar.Enabled = True
                btnImportar.Enabled = True
                For Each oDataRow In dt.Rows
                    Me.lblFolio.Text = oDataRow("Folio")
                    Me.lblEjercicio.Text = oDataRow("Ejercicio")
                    Me.lblTipoNomina.Text = "Semanal"
                    Me.lblFechaInicial.Text = oDataRow("FechaInicial")
                    Me.lblFechaFinal.Text = oDataRow("FechaFinal")
                    Me.lblDias.Text = oDataRow("Dias")
                    Me.lblRazonSocial.Text = oDataRow("Cliente")
                Next
                Call CargarGridEmpleadosSemanal()
            Else
                panelDatos.Visible = False
                Me.lblFolio.Text = ""
                Me.lblEjercicio.Text = ""
                Me.lblRazonSocial.Text = ""
                Me.lblTipoNomina.Text = ""
                Me.lblFechaInicial.Text = ""
                Me.lblFechaFinal.Text = ""
                Me.lblDias.Text = ""
            End If

            Call BloquearBotones()

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub ConsultarFolio(ByVal folio As Integer)
        Dim cNomina As New Nomina()
        Dim dtFolio As DataTable = cNomina.ConsultarNominaPorFolio(folio)

        ' Llenar los combos con los datos obtenidos
        If dtFolio IsNot Nothing AndAlso dtFolio.Rows.Count > 0 Then
            cmbPeriodicidad.SelectedValue = dtFolio.Rows(0)("TipoNomina").ToString()
            cmbCliente.SelectedValue = dtFolio.Rows(0)("IdCliente").ToString()
            CargaPeriodos(cmbPeriodicidad.SelectedValue, dtFolio.Rows(0)("Periodo"))
            cmbPeriodo.SelectedValue = dtFolio.Rows(0)("Periodo").ToString()
            txtObservaciones.Text = dtFolio.Rows(0)("Observaciones").ToString()
            CargarDatos()
            lblTitulo.Text = "Periodo " & cmbPeriodo.SelectedItem.Text
            btnGeneraNomina.Enabled = False
            BloquearBotones()
        End If

    End Sub
    Private Sub BloquearBotones()

        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = cmbCliente.SelectedValue
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.EsEspecial = True
        dt = cNomina.ConsultarDatosGeneralesNomina()

        Dim dt_Empleado As New DataTable()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = cmbCliente.SelectedValue
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.EsEspecial = True
        cNomina.IdNomina = nominaID.Value
        dt_Empleado = cNomina.ConsultarDetalleNominaExtraordinaria()

        If dt.Rows.Count > 0 And dt_Empleado.Rows.Count > 0 Then

            btnGeneraNomina.Enabled = False
            btnModificacionDeNomina.Enabled = True
            btnGenerarNominaElectronica.Enabled = True

            Dim rowTimbrado() As DataRow = dt.Select("Timbrado='S'")
            If (rowTimbrado.Length > 0) Then
                btnGeneraNomina.Enabled = False
                btnGeneraTxtDispersion.Enabled = True
                btnGenerarPDF.Enabled = True
                btnDescargarXMLS.Enabled = True
                btnBorrarNomina.Enabled = False
                If rowTimbrado.Length < dt.Rows.Count Then
                    btnTimbrarNominaSemanal.Enabled = True
                Else
                    btnTimbrarNominaSemanal.Enabled = False
                End If
            ElseIf rowTimbrado.Length = 0 Then
                btnBorrarNomina.Enabled = True
                btnTimbrarNominaSemanal.Enabled = True
                btnGenerarPDF.Enabled = False
                btnGeneraTxtDispersion.Enabled = False
            End If

            Dim rowGenerados() As DataRow = dt.Select("Generado='S'")
            If (rowGenerados.Length > 0) Then
                btnGenerarPDF.Enabled = True
                btnGeneraTxtDispersion.Enabled = True
                If rowGenerados.Length < dt.Rows.Count Then
                    btnModificacionDeNomina.Enabled = True
                    btnGenerarNominaElectronica.Enabled = True
                Else
                    btnModificacionDeNomina.Enabled = False
                    btnGenerarNominaElectronica.Enabled = False
                End If
            ElseIf rowGenerados.Length = 0 Then
                btnGenerarNominaElectronica.Enabled = True
            End If

            Dim rowPdf() As DataRow = dt.Select("Pdf='S'")
            If (rowPdf.Length > 0) Then
                btnDescargarPDFS.Enabled = True
                btnEnvioComprobantes.Enabled = True
                If rowPdf.Length < dt.Rows.Count Then
                    'btnGenerarPDF.Enabled = True
                    'btnDescargarPDFS.Enabled = False
                Else
                    btnGenerarPDF.Enabled = False
                    btnDescargarPDFS.Enabled = True
                End If
            ElseIf rowPdf.Length = 0 Then
                'btnGenerarPDF.Enabled = True
                btnEnvioComprobantes.Enabled = False
                btnDescargarPDFS.Enabled = False
            End If

        ElseIf dt.Rows.Count = 0 Or dt_Empleado.Rows.Count = 0 Then
            btnGeneraNomina.Enabled = True
            btnModificacionDeNomina.Enabled = False
            btnBorrarNomina.Enabled = False
            btnGenerarNominaElectronica.Enabled = False
            btnTimbrarNominaSemanal.Enabled = False
            btnGenerarPDF.Enabled = False
            btnEnvioComprobantes.Enabled = False
            btnDescargarXMLS.Enabled = False
            btnDescargarPDFS.Enabled = False
            btnGeneraTxtDispersion.Enabled = False
        End If
    End Sub
    Private Sub CargarGridEmpleadosSemanal()

        Call CargarVariablesGenerales()

        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = cmbCliente.SelectedValue
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.EsEspecial = True
        cNomina.IdNomina = nominaID.Value
        dtEmpleados = cNomina.ConsultarDetalleNominaExtraordinaria()
        grdEmpleadosSemanal.DataSource = dtEmpleados
        grdEmpleadosSemanal.DataBind()

        '' Ordena el DataTable por la columna fechaPago
        'Dim dv As New DataView(dtEmpleados)
        'dv.Sort = "FechaPago ASC"

        '' Obtén el primer valor de la columna fechaPago
        'Dim firstFechaPago As DateTime = CType(dv(0)("FechaPago"), DateTime)

        '' Guarda el primer valor en una variable
        'fchPago.SelectedDate = firstFechaPago
        'fchPago.Enabled = False

        cNomina = Nothing
    End Sub
    Private Sub btnGeneraNomina_Click(sender As Object, e As EventArgs) Handles btnGeneraNomina.Click
        If cmbPeriodo.SelectedValue = 0 Then
            rwAlerta.RadAlert("Seleccione un periodo.", 330, 180, "Alerta", "", "")
        ElseIf cmbCliente.SelectedValue = 0 Then
            rwAlerta.RadAlert("Seleccione un cliente.", 330, 180, "Alerta", "", "")
        Else
            rwConfirmGeneraNomina.RadConfirm("¿Está seguro de generar el cálculo de nómina sin incidencias y con fecha de pago del " & Format(fchPago.SelectedDate, "d \d\e MMMM \d\e yyyy") & "?", "confirmCallbackFnGenerarNomina", 330, 180, Nothing, "Confirmar")
        End If
    End Sub
    Private Sub btnCrearPeriodoID_Click(sender As Object, e As EventArgs) Handles btnCrearPeriodoID.Click
        If cmbPeriodicidad.SelectedIndex = 0 Then
            rwAlerta.RadAlert("Seleccione una periodicidad.", 330, 180, "Alerta", "", "")
        Else
            WinPeriodoSave.VisibleOnPageLoad = True
            WinPeriodoSave.Title = "Agregar periodo " & cmbPeriodicidad.SelectedItem.Text.ToLower.Trim
            periodicidadid.Value = cmbPeriodicidad.SelectedValue
            fchInicioPeriodo.SelectedDate = DateTime.Now
            fchFinPeriodo.SelectedDate = DateTime.Now
        End If

    End Sub
    Private Sub btnConfirmarGeneraNomina_Click(sender As Object, e As EventArgs) Handles btnConfirmarGeneraNomina.Click

        Call CargarVariablesGenerales()

        If IsNothing(fchPago.SelectedDate) Then
            rwAlerta.RadAlert("Seleccione una fecha de pago.", 330, 180, "Alerta", "", "")
            Return
        Else
            Session("FechaPago") = fchPago.SelectedDate

            Dim cPeriodo As New Entities.Periodo
            cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
            If Not fchPago.SelectedDate Is Nothing Then
                cPeriodo.FechaPago = String.Format("{0:MM/dd/yyyy}", fchPago.SelectedDate)
            End If
            cPeriodo.ActualizaFechaPagoPeriodo()
            cPeriodo = Nothing
        End If

        Dim dt As New DataTable
        Dim cNomina As New Entities.Nomina()
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = cmbCliente.SelectedValue
        cNomina.FechaPago = fchPago.SelectedDate
        cNomina.Observaciones = txtObservaciones.Text

        Dim _Nominaid As Integer = 0
        If Session("Folio") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Session("Folio").ToString()) Then
            _Nominaid = Integer.Parse(Session("Folio").ToString())
        Else
            Dim idNomina As DataTable = cNomina.InsertaCampoNomina()
            For Each row As DataRow In idNomina.Rows
                _Nominaid = Convert.ToInt32(row(0))
                Session("Folio") = Convert.ToInt32(row(0))
            Next
        End If

        nominaID.Value = _Nominaid

        dt = cNomina.ConsultarEmpleadosEspecial()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then

            Dim Total As Integer = dt.Rows.Count

            Dim progress As RadProgressContext = RadProgressContext.Current
            progress.Speed = "N/A"
            Dim i As Integer = 0
            For Each oDataRow In dt.Rows
                i += 1
                ImporteDiario = 0
                ImportePeriodo = oDataRow("Excedente_dinero")
                Subsidio = 0
                Impuesto = 0
                SubsidioAplicado = 0
                SubsidioEfectivo = 0
                SalarioDiarioIntegradoTrabajador = 0
                UMA = 0

                GuardarRegistro(oDataRow("NoEmpleado"), oDataRow("IdContrato"), ImportePeriodo, oDataRow("ClaveRegimenContratacion"), _Nominaid)

                progress.SecondaryTotal = Total
                progress.SecondaryValue = i
                progress.SecondaryPercent = Math.Round((i * 100 / Total), 0)

                progress.CurrentOperationText = "Empleado " & i.ToString()

                If Not Response.IsClientConnected Then
                    'Cancel button was clicked or the browser was closed, so stop processing
                    Exit For
                End If

                progress.TimeEstimated = (Total - i) * 100

            Next

            Session("ClienteIDNomina") = cmbCliente.SelectedValue
            Session("PeriodicidadIDNomina") = cmbPeriodicidad.SelectedValue
            Session("PeriodoIDNomina") = cmbPeriodo.SelectedValue

            Response.Redirect("~/GeneracionDeNominaExtraordinaria.aspx?id=" & cmbPeriodo.SelectedValue.ToString & "&nid=" & nominaID.Value.ToString)

        End If
    End Sub
    Private Sub btnModificacionDeNomina_Click(sender As Object, e As EventArgs) Handles btnModificacionDeNomina.Click
        If cmbPeriodo.SelectedValue > 0 Then
            Response.Redirect("~/ModificacionGeneralExtraordinaria.aspx?id=" & cmbPeriodo.SelectedValue.ToString)
        Else
            rwAlerta.RadAlert("Seleccione un periodo.", 330, 180, "Alerta", "", "")
        End If
    End Sub
    Private Sub AgregaConcepto(ByVal ImporteIncidencia, ByVal UnidadIncidencia, ByVal CuotaPeriodo, ByVal ImporteDiario, ByVal NoEmpleado, ByVal CvoConcepto, ByVal IdContrato)
        Try
            Dim Importe As Decimal = 0
            Dim Unidad As Integer = 0
            Try
                Importe = Convert.ToDecimal(ImporteIncidencia)
            Catch ex As Exception
                Importe = 0
            End Try
            Try
                Unidad = UnidadIncidencia
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

            Dim ClaveRegimenContratacion As Integer = 0
            Dim cEmpleado As New Entities.Empleado
            cEmpleado.IdEmpleado = NoEmpleado
            cEmpleado.ConsultarEmpleadoID()
            If cEmpleado.IdEmpleado > 0 Then
                ClaveRegimenContratacion = cEmpleado.IdRegimenContratacion
            End If

            If CvoConcepto.ToString = "5" And ClaveRegimenContratacion <> 9 Then
                rwAlerta.RadAlert("El regimen de contratacion de este trabajador no es honorarios asimilado a salarios!!", 330, 180, "Alerta", "", "")
                Exit Sub
            ElseIf CvoConcepto.ToString < 51 Or CvoConcepto.ToString = "57" Or CvoConcepto.ToString = "58" Or CvoConcepto.ToString = "59" Or CvoConcepto.ToString = "161" Or CvoConcepto.ToString = "162" Then
                If ClaveRegimenContratacion = 9 Then
                    rwAlerta.RadAlert("El régimen de contratación de este trabajador es asimilado a salarios, por lo mismo no se le debe agregar ningún otro tipo de percepción ni hacerle deducciones por faltas, dichas deducciones solo pueden ser por un aduedo distinto!!", 330, 180, "Alerta", "", "")
                    Exit Sub
                End If
            End If

            ImporteDiario = 0
            ImportePeriodo = 0
            ImporteExento = 0
            ImporteGravado = 0
            SubsidioAplicado = 0
            SubsidioEfectivo = 0
            Agregar = 1
            'IMSS
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
                If CvoConcepto.ToString < 51 Or CvoConcepto.ToString = "57" Or CvoConcepto.ToString = "58" Or CvoConcepto.ToString = "59" Or CvoConcepto.ToString = "161" Or CvoConcepto.ToString = "162" Then
                    'En este bloque no entran las deducciones por adeudos que no cambian la base del impuesto y por lo tanto no lo calculan, ejemplos de esto serian, adeudos por credito infonavit, cuotas sindicales, adeudos fonacot, adeudos con el patron, etc, (conceptos del 61 al 86)
                    BorrarDeducciones(NoEmpleado)
                    ChecarPercepcionesGravadas(NoEmpleado, CvoConcepto, Importe, Unidad)
                    CalcularImpuesto()
                    CalcularSubsidio()

                    Call CalcularImss()
                    'IMSS = IMSS * (DiasCuotaPeriodo + DiasVacaciones + DiasPagoPorHoras + DiasComision + DiasDestajo + DiasHonorarioAsimilado)
                    IMSS = IMSS * DiasCuotaPeriodo
                    IMSS = Math.Round(IMSS, 6)
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
            End If

            GuardarRegistro(NoEmpleado, IdContrato, 64, ImporteIncidencia, UnidadIncidencia)

            'SolicitarGeneracionXml(NoEmpleado)

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub GuardarRegistro(ByVal NoEmpleado, ByVal IdContrato, ByVal CvoConcepto, ByVal ImporteIncidencia, ByVal UnidadIncidencia, Optional ByVal IdCreditoFonacot = 0)
        Try
            Call CargarVariablesGenerales()

            Try
                ImporteIncidencia = Math.Round(Convert.ToDouble(ImporteIncidencia), 6)
            Catch ex As Exception
                ImporteIncidencia = 0
            End Try

            Try
                UnidadIncidencia = UnidadIncidencia
            Catch ex As Exception
                UnidadIncidencia = 0
            End Try

            'ConImpuesto = 1 cuando viene de agregar una percepcion y calcular impuestos guardando ambos
            'ConImpuesto = 2 cuando viene de quitar una percepcion y solo se procede a guardar los impuesto modificados sin esa percepcion

            Dim cNomina As New Nomina()

            If CvoConcepto <= 50 Then
                'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CVOCONCEPTO, TIPOCONCEPTO, UNIDAD, IMPORTE, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', '" + cmbConcepto.SelectedValue.ToString + "', 'P', '" + txtUnidadIncidencia.Text + "', " + txtImporteIncidencia.Text + ", 'N', 'N', 'N', 'A', 0, 0)"
                cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = periodoID.Value
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
            ElseIf CvoConcepto.ToString = "57" Or CvoConcepto.ToString = "58" Or CvoConcepto.ToString = "59" Or CvoConcepto.ToString = "161" Or CvoConcepto.ToString = "162" Then
                'Deducciones por faltas, permisos o incapacidades
                'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CVOCONCEPTO, TIPOCONCEPTO, UNIDAD, IMPORTE, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', '" + CboConceptos.SelectedValue.ToString + "', 'D', '" + TxtUnidadIncidencia.Text + "', " + TxtImporteIncidencia.Text + ", 'N', 'N', 'N', 'A', 0, " + TxtImporteIncidencia.Text + ")"
                cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = periodoID.Value
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
                'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CVOCONCEPTO, TIPOCONCEPTO, UNIDAD, IMPORTE, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', '" + CboConceptos.SelectedValue.ToString + "', 'D', '" + TxtUnidadIncidencia.Text + "', " + TxtImporteIncidencia.Text + ", 'N', 'N', 'N', 'A', 0, " + TxtImporteIncidencia.Text + ")"
                cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = periodoID.Value
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
                cNomina.IdCreditoFonacot = IdCreditoFonacot
                cNomina.GuadarNomina()
            End If

            'Aqui se guarda el impuesto, el total Gravado y el total exento, tanto cuando viene de agregar un concepto como cuando viene de quitar un concepto ya que de ambas maneras se recalcula
            'solo no entra en este bloque de codigo cuando viene de agregar una deduccion que no implica recalcular gravado, exento o impuesto(las unicas deducciones que recalculan gravado, exento e impuesto son la faltas, permisos e incapacidades, las demas deduccioens haciendo hincapie, no entran en este bloque)

            If CvoConcepto < 51 Or CvoConcepto.ToString = "57" Or CvoConcepto.ToString = "58" Or CvoConcepto.ToString = "59" Or CvoConcepto.ToString = "161" Or CvoConcepto.ToString = "162" Then
                'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CVOCONCEPTO, TIPOCONCEPTO, UNIDAD, IMPORTE, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', 52, 'D', 1, " + Impuesto.ToString + ", 'N', 'N', 'N', 'A', 0, " + Impuesto.ToString + ")"
                cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = periodoID.Value
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
                    'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CVOCONCEPTO, TIPOCONCEPTO, UNIDAD, IMPORTE, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', 54, 'P', 1, " + SubsidioAplicado.ToString + ", 'N', 'N', 'N', 'A', 0, " + SubsidioAplicado.ToString + ")"
                    cNomina = New Nomina()
                    cNomina.IdEmpresa = IdEmpresa
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                    cNomina.Periodo = periodoID.Value
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
                    'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CVOCONCEPTO, TIPOCONCEPTO, UNIDAD, IMPORTE, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', 55, 'P', 1, " + SubsidioEfectivo.ToString + ", 'N', 'N', 'N', 'A', 0, " + SubsidioEfectivo.ToString + ")"
                    cNomina = New Nomina()
                    cNomina.IdEmpresa = IdEmpresa
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                    cNomina.Periodo = periodoID.Value
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
                If IMSS > 0 Then
                    'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CVOCONCEPTO, TIPOCONCEPTO, UNIDAD, IMPORTE, GENERADO, TIMBRADO, ENVIADO, SITUACION, IMPORTEGRAVADO, IMPORTEEXENTO) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + NoEmpleado.ToString + "', 56, 'D', 1, " + IMSS.ToString + ", 'N', 'N', 'N', 'A', 0, " + IMSS.ToString + ")"
                    cNomina = New Nomina()
                    cNomina.IdEmpresa = IdEmpresa
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                    cNomina.Periodo = periodoID.Value
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
                    cNomina.GuadarNomina()
                End If
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Function ChecarSiExiste(ByVal NoEmpleado As Int64, ByVal CvoConcepto As Int32) As Boolean
        Try

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = cmbCliente.SelectedValue
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
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
    Private Function ChecarQueExistaLaCuotaPeriodo(ByVal NoEmpleado, ByVal ImporteIncidencia, ByVal UnidadIncidencia) As Boolean
        Try

            Call CargarVariablesGenerales()

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

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = cmbCliente.SelectedValue
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.NoEmpleado = NoEmpleado
            dt = cNomina.ConsultarConceptosEmpleado()

            If dt.Rows.Count = 0 Or dt.Compute("SUM(Importe)", "CvoConcepto=2") Is DBNull.Value Then
                ChecarQueExistaLaCuotaPeriodo = False
            ElseIf dt.Rows.Count >= 0 And dt.Compute("SUM(Importe)", "CvoConcepto=2") IsNot DBNull.Value Then
                If dt.Compute("SUM(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") IsNot DBNull.Value Then
                    If dt.Compute("SUM(Importe)", "CvoConcepto=2") < (dt.Compute("SUM(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") + ImporteIncidencia) Or dt.Compute("SUM(Unidad)", "CvoConcepto=2") < (dt.Compute("SUM(Unidad)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") + UnidadIncidencia) Then
                        ChecarQueExistaLaCuotaPeriodo = False
                    ElseIf dt.Compute("SUM(Importe)", "CvoConcepto=2") > (dt.Compute("SUM(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") + ImporteIncidencia) Or dt.Compute("SUM(Unidad)", "CvoConcepto=2") > (dt.Compute("SUM(Unidad)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") + UnidadIncidencia) Then
                        ChecarQueExistaLaCuotaPeriodo = True
                    End If
                ElseIf dt.Compute("SUM(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") Is DBNull.Value Then
                    If dt.Compute("SUM(Importe)", "CvoConcepto=2") < ImporteIncidencia Or dt.Compute("SUM(Unidad)", "CvoConcepto=2") < UnidadIncidencia Then
                        ChecarQueExistaLaCuotaPeriodo = False
                    ElseIf dt.Compute("SUM(Importe)", "CvoConcepto=2") > ImporteIncidencia Or dt.Compute("SUM(Unidad)", "CvoConcepto=2") > UnidadIncidencia Then
                        ChecarQueExistaLaCuotaPeriodo = True
                    End If
                End If
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Function
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
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = cmbCliente.SelectedValue
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.NoEmpleado = NoEmpleado
            dt = cNomina.ConsultarConceptosEmpleado()

            'PercepcionesGravadas
            If dt.Rows.Count > 0 Then
                If dt.Compute("SUM(Importe)", "CvoConcepto=2") IsNot DBNull.Value Then
                    DiasCuotaPeriodo = dt.Compute("SUM(UNIDAD)", "CvoConcepto=2")
                    CuotaPeriodo = dt.Compute("SUM(Importe)", "CvoConcepto=2")
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
                    DiasHonorarioAsimilado = dt.Compute("SUM(UNIDAD)", "CvoConcepto=5") * 7
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
                    DiasVacaciones = dt.Compute("SUM(UNIDAD)", "CvoConcepto=15")
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
                    PrevisionSocial = dt.Compute("SUM(Importe)", "CvoConcepto=33 OR CvoConcepto=34 OR CvoConcepto=35 OR CvoConcepto=36 OR CvoConcepto=37 OR CvoConcepto=38 OR CvoConcepto=40 OR CvoConcepto=41 OR CvoConcepto=43 OR CvoConcepto=45 OR CvoConcepto=46 OR CvoConcepto=47 OR CvoConcepto=48")
                End If
                '12,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,39,49
                If dt.Compute("SUM(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=18 OR CvoConcepto=19 OR CvoConcepto=20 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=28 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=32 OR CvoConcepto=39 OR CvoConcepto=49") IsNot DBNull.Value Then
                    GrupoPercepcionesGravadasTotalmenteSinExentos = dt.Compute("SUM(Importe)", "CvoConcepto=12 OR CvoConcepto=17 OR CvoConcepto=18 OR CvoConcepto=19 OR CvoConcepto=20 OR CvoConcepto=21 OR CvoConcepto=22 OR CvoConcepto=23 OR CvoConcepto=24 OR CvoConcepto=25 OR CvoConcepto=26 OR CvoConcepto=27 OR CvoConcepto=28 OR CvoConcepto=29 OR CvoConcepto=30 OR CvoConcepto=31 OR CvoConcepto=32 OR CvoConcepto=39 OR CvoConcepto=49")
                End If
                If dt.Compute("SUM(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") IsNot DBNull.Value Then
                    DiasFaltasPermisosIncapacidades = dt.Compute("SUM(UNIDAD)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162")
                    FaltasPermisosIncapacidades = dt.Compute("SUM(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162")
                End If
            End If
            'Agregar es igual a uno cuando venimos del boton agregar
            'Agregar es igual a cero cuando venimos del boton quitar
            'Si viene del boton quitar no se agrega en ningun caso el contenido de ImporteIncidencia, solo se checa lo exento y gravado que ya esta dentro de la base de datos y se recalculan los impuestos
            If Agregar = 1 Then
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
                Dias = dt.Compute("SUM(UNIDAD)", "CvoConcepto=13")
                If PrimaDominical < (SalarioMinimoDiarioGeneral * Dias) Then
                    ImporteExento = ImporteExento + PrimaDominical
                Else
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * Dias)
                    ImporteGravado = ImporteGravado + (PrimaDominical - (SalarioMinimoDiarioGeneral * Dias))
                End If
            ElseIf PrimaDominical > 0 And Agregar = 1 Then
                Try
                    ImporteIncidencia = Convert.ToDecimal(ImporteIncidencia)
                Catch ex As Exception
                    ImporteIncidencia = 0
                End Try

                Try
                    UnidadIncidencia = Convert.ToDecimal(ImporteIncidencia)
                Catch ex As Exception
                    UnidadIncidencia = 0
                End Try

                If CvoConcepto.ToString = "13" And ImporteIncidencia < (SalarioMinimoDiarioGeneral * UnidadIncidencia) Then
                    ImporteExento = ImporteExento + PrimaDominical
                ElseIf CvoConcepto.ToString = "13" And ImporteIncidencia > (SalarioMinimoDiarioGeneral * UnidadIncidencia) Then
                    ImporteExento = ImporteExento + (SalarioMinimoDiarioGeneral * UnidadIncidencia)
                    ImporteGravado = ImporteGravado + (ImporteIncidencia - (SalarioMinimoDiarioGeneral * UnidadIncidencia))
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
    Public Sub BorrarDeducciones(ByVal NoEmpleado As Int64)
        Try
            Call CargarVariablesGenerales()
            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=86 AND TIPOCONCEPTO='D'"
            Dim cNomina As New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 86
            cNomina.TipoConcepto = "D"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=54 AND TIPOCONCEPTO='P'"
            cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 54
            cNomina.TipoConcepto = "P"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=55 AND TIPOCONCEPTO='P'"
            cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 55
            cNomina.TipoConcepto = "P"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=56 AND TIPOCONCEPTO='D'"
            cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 56
            cNomina.TipoConcepto = "D"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=108 AND TIPOCONCEPTO='DE'"
            cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 108
            cNomina.TipoConcepto = "DE"
            cNomina.EliminaConceptoEmpleado()

            'CadenaSql = "DELETE FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO =" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO= " + TxtClaveEmpleado.Text.ToString + " AND CvoConcepto=109 AND TIPOCONCEPTO='DE'"
            cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 109
            cNomina.TipoConcepto = "DE"
            cNomina.EliminaConceptoEmpleado()

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
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
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = cmbCliente.SelectedValue
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = cmbPeriodo.SelectedValue
                cNomina.NoEmpleado = NoEmpleado
                cNomina.TipoConcepto = "P"
                dt = cNomina.ConsultarConceptosEmpleado()
            ElseIf Agregar = 0 Then
                ''''''''''''''''''''''PENDIENTE'''''''''''''''''''''''''''''
                'Me.oDataAdapterChecarPercepcionesGravadas = New OleDbDataAdapter("SELECT * FROM NOMINAS WHERE EJERCICIO='" + Ejerciciio.ToString + "' AND TIPONOMINA=1 AND PERIODO=" + TxtPeriodo.Text.ToString + " AND NOEMPLEADO=" + TxtClaveEmpleado.Text + " AND TIPOCONCEPTO='P' AND CVOCONCEPTO<>" + NumeroConcepto.ToString + "", oConexion)
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = cmbCliente.SelectedValue
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = cmbPeriodo.SelectedValue
                cNomina.NoEmpleado = NoEmpleado
                cNomina.TipoConcepto = "P"
                cNomina.DiferenteDe = 1
                cNomina.CvoConcepto = CvoConcepto
                dt = cNomina.ConsultarConceptosEmpleado()
            Else
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = cmbCliente.SelectedValue
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = cmbPeriodo.SelectedValue
                cNomina.NoEmpleado = NoEmpleado
                cNomina.TipoConcepto = "P"
                dt = cNomina.ConsultarConceptosEmpleado()
            End If

            ' PercepcionesGravadas
            If dt.Rows.Count > 0 Then
                If dt.Compute("SUM(Importe)", "CvoConcepto=2") IsNot DBNull.Value Then
                    If Agregar <> 3 Then
                        DiasCuotaPeriodo = dt.Compute("SUM(Unidad)", "CvoConcepto=2")
                        CuotaPeriodo = dt.Compute("SUM(Importe)", "CvoConcepto=2")
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
                    DiasHonorarioAsimilado = dt.Compute("SUM(Unidad)", "CvoConcepto=5") * 7
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

        IMSS = 0
        UMA = 0
        Call CargarVariablesGenerales()

        If SalarioDiarioIntegradoTrabajador <= SalarioMinimoDiarioGeneral Then
            IMSS = 0
        ElseIf SalarioDiarioIntegradoTrabajador > SalarioMinimoDiarioGeneral And SalarioDiarioIntegradoTrabajador < (UMA * 3) Then
            IMSS = SalarioDiarioIntegradoTrabajador * 0.02375
        ElseIf SalarioDiarioIntegradoTrabajador > (UMA * 3) And SalarioDiarioIntegradoTrabajador < (UMA * 25) Then
            IMSS = SalarioDiarioIntegradoTrabajador * 0.02375
            IMSS = IMSS + ((SalarioDiarioIntegradoTrabajador - (UMA * 3)) * 0.004)
        ElseIf SalarioDiarioIntegradoTrabajador > (UMA * 25) Then
            IMSS = (SalarioDiarioIntegradoTrabajador - (UMA * 25)) * 0.02375
            IMSS = IMSS + ((SalarioDiarioIntegradoTrabajador - (UMA * 22)) * 0.004)
        End If

    End Sub
    Private Sub GuardarRegistro(ByVal NoEmpleado, ByVal IdContrato, ByVal ImportePeriodo, ByVal RegimenContratacion, ByVal IdNomina)
        Try
            Call CargarVariablesGenerales()

            Dim cPeriodo As New Entities.Periodo()
            cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
            cPeriodo.ConsultarPeriodoID()

            Dim cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = cmbCliente.SelectedValue
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 82
            cNomina.IdContrato = IdContrato
            cNomina.TipoConcepto = "P"
            cNomina.Unidad = 1
            cNomina.ImporteExento = ImportePeriodo
            cNomina.Importe = ImportePeriodo
            cNomina.Generado = ""
            cNomina.Timbrado = ""
            cNomina.Enviado = ""
            cNomina.Situacion = "A"
            cNomina.EsEspecial = True
            cNomina.FechaIni = cPeriodo.FechaInicialDate
            cNomina.FechaFin = cPeriodo.FechaFinalDate
            cNomina.FechaPago = fchPago.SelectedDate
            cNomina.DiasPagados = cPeriodo.Dias
            cNomina.IdNomina = IdNomina
            cNomina.GuadarNominaPeriodo()
            cNomina = Nothing
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub cmbPeriodo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged

        Call CargarVariablesGenerales()

        periodoID.Value = cmbPeriodo.SelectedValue

        If cmbPeriodo.SelectedValue > 0 Then
            lblTitulo.Text = "Periodo " & cmbPeriodo.SelectedItem.Text
            Dim cPeriodo As New Entities.Periodo()
            cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
            cPeriodo.ConsultarPeriodoID()

            If cPeriodo.FechaPago.ToString.Length > 0 Then
                If Not IsNothing(CDate(cPeriodo.FechaPago)) Then
                    fchPago.SelectedDate = CDate(cPeriodo.FechaPago)
                    fchPago.Enabled = False
                End If
            End If
        Else
            btnModificacionDeNomina.Enabled = False
            btnBorrarNomina.Enabled = False
            btnGenerarNominaElectronica.Enabled = False
            btnTimbrarNominaSemanal.Enabled = False
            btnGenerarPDF.Enabled = False
            btnEnvioComprobantes.Enabled = False
            btnDescargarXMLS.Enabled = False
            btnDescargarPDFS.Enabled = False
            btnGeneraTxtDispersion.Enabled = False
        End If

        Dim cConfiguracion As New Entities.Configuracion
        cConfiguracion.IdEmpresa = IdEmpresa
        cConfiguracion.IdUsuario = Session("usuarioid")
        cConfiguracion.IdPeriodo = cmbPeriodo.SelectedValue
        cConfiguracion.ActualizaPeriodoNomina()
        cConfiguracion.ActualizaSalarioMinimoDiarioGeneral()
        cConfiguracion = Nothing

        Session("ClienteIDNomina") = cmbCliente.SelectedValue
        Session("PeriodicidadIDNomina") = cmbPeriodicidad.SelectedValue
        Session("PeriodoIDNomina") = cmbPeriodo.SelectedValue

        Call CargarDatos()

    End Sub
    Private Sub GetNominaDetailsById(ByVal id As Integer)

    End Sub
    Private Sub grdEmpleadosSemanal_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdEmpleadosSemanal.NeedDataSource

        Call CargarVariablesGenerales()

        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = cmbCliente.SelectedValue
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.EsEspecial = True
        cNomina.IdNomina = nominaID.Value
        dtEmpleados = cNomina.ConsultarDetalleNominaExtraordinaria()
        grdEmpleadosSemanal.DataSource = dtEmpleados
        cNomina = Nothing
    End Sub
    Private Sub grdEmpleadosSemanal_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles grdEmpleadosSemanal.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem

                Dim imgGenerado As Image = CType(e.Item.FindControl("imgGenerado"), Image)
                Dim imgTimbrado As ImageButton = CType(e.Item.FindControl("imgTimbrado"), ImageButton)
                Dim imgAlert As ImageButton = CType(e.Item.FindControl("imgAlert"), ImageButton)
                Dim imgXML As ImageButton = CType(e.Item.FindControl("imgXML"), ImageButton)
                Dim imgPDF As ImageButton = CType(e.Item.FindControl("imgPDF"), ImageButton)
                Dim imgPDFTimbrado As ImageButton = CType(e.Item.FindControl("imgPDFTimbrado"), ImageButton)
                Dim imgEnviar As ImageButton = CType(e.Item.FindControl("imgEnviar"), ImageButton)

                If (e.Item.DataItem("EstatusContrato") = "Baja") Then
                    e.Item.ForeColor = Drawing.Color.Red
                End If
                If (e.Item.DataItem("Generado") = "S") Then
                    imgGenerado.Visible = True
                End If
                If (e.Item.DataItem("Timbrado") = "S") Then
                    imgXML.Visible = True
                    If (e.Item.DataItem("Pdf") = "S") Then
                        imgPDFTimbrado.Visible = True
                    End If
                Else
                    If (e.Item.DataItem("Timbrado") = "N") Then
                        imgAlert.Visible = True
                    End If
                End If
                If (e.Item.DataItem("Pdf") = "S") Then
                    ' imgPDF.Visible = True
                    imgEnviar.Visible = True
                End If
                If (e.Item.DataItem("Enviado") = "S") Then
                    imgEnviar.ImageUrl = "~/images/envelopeok.jpg"
                    imgEnviar.ToolTip = "Fecha de envío: " & e.Item.DataItem("FechaEnviado")
                End If
                If (CDbl(e.Item.DataItem("Neto")) <= 1) Then
                    imgAlert.Visible = False
                    e.Item.ForeColor = Drawing.Color.Blue
                End If
            Case Telerik.Web.UI.GridItemType.Footer
                If Not IsNothing(dtEmpleados) Then
                    If dtEmpleados.Rows.Count > 0 Then
                        If Not IsDBNull(dtEmpleados.Compute("sum(Neto)", "")) Then
                            e.Item.Cells(5).Text = "TOTAL:"
                            e.Item.Cells(5).HorizontalAlign = HorizontalAlign.Right
                            e.Item.Cells(5).Font.Bold = True

                            e.Item.Cells(6).Text = FormatCurrency(dtEmpleados.Compute("sum(Neto)", ""), 2).ToString
                            e.Item.Cells(6).HorizontalAlign = HorizontalAlign.Right
                            e.Item.Cells(6).Font.Bold = True
                        End If
                    End If
                End If
        End Select
    End Sub
    Private Sub btnBorrarNomina_Click(sender As Object, e As EventArgs) Handles btnBorrarNomina.Click
        If cmbPeriodo.SelectedValue > 0 Then
            rwConfirmGeneraNomina.RadConfirm("¿Está seguro que desea borrar esta nómina?", "confirmCallbackFnBorrarNomina", 330, 180, Nothing, "Confirmar")
        Else
            rwAlerta.RadAlert("Seleccione un periodo.", 330, 180, "Alerta", "", "")
        End If
    End Sub
    Private Sub btnConfirmarBorrarNomina_Click(sender As Object, e As EventArgs) Handles btnConfirmarBorrarNomina.Click
        Call EliminarNomina()
    End Sub
    Private Sub EliminarNomina()
        Try

            Call CargarVariablesGenerales()

            Dim cNomina As New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = cmbCliente.SelectedValue
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.EliminaNomina()

            Response.Redirect("~/GeneracionDeNominaExtraordinaria.aspx?id=" & cmbPeriodo.SelectedValue.ToString & "&nid=" & nominaID.Value.ToString)

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub BorrarExentosYGravados()
        Try

            Call CargarVariablesGenerales()

            Dim cNomina As New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.EliminaEmpleado()

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub btnGenerarNominaElectronica_Click(sender As Object, e As EventArgs) Handles btnGenerarNominaElectronica.Click
        If cmbPeriodo.SelectedValue > 0 Then
            rwConfirmGeneraNomina.RadConfirm("¿Está seguro de generar el cálculo de nómina electrónica?", "confirmCallbackFnGeneraNominaElectronica", 330, 180, Nothing, "Confirmar")
        Else
            rwAlerta.RadAlert("Seleccione un periodo.", 330, 180, "Alerta", "", "")
        End If
    End Sub
    Private Sub ChecarDeducciones(ByVal NoEmpleado As Integer)
        Try
            Impuesto = 0
            IsrIndemnizacion = 0
            'SubsidioAplicado = 0
            'SubsidioEfectivo = 0
            CuotaImss = 0
            Faltas = 0
            Permisos = 0
            IncapacidadPorEnfermedad = 0
            DiasHorasNoTrabajadas = 0
            CuotaSindical = 0
            PensionAlimenticia = 0
            AdeudoFonacot = 0
            CreditoInfonavit = 0
            AdeudoCajaAhorro = 0
            AdeudoFondoAhorro = 0
            GastosSinComprobar = 0
            PagoIndebido = 0
            Anticipos = 0
            AdeudoSindical = 0
            AdeudoPatron = 0
            ViaticosDeduccion = 0
            AportacionCajaAhorro = 0
            AportacionFondoAhorro = 0
            AportacionDespensa = 0
            AportacionComedor = 0
            AportacionVoluntariaSar = 0
            AportacionVoluntariaInfonavit = 0
            FondoRetiro = 0
            PrimaSeguroVida = 0
            PrimaSeguroAuto = 0
            PrimaGastosMedicos = 0
            DevolucionIsr = 0
            AjusteIsr = 0
            AdeudoCuotaImss = 0
            OtrasDeducciones = 0
            IncapacidadPorMaternidad = 0
            IncapacidadPorAccidente = 0

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = cmbCliente.SelectedValue
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.NoEmpleado = NoEmpleado
            cNomina.TipoConcepto = "D"
            dt = cNomina.ConsultarConceptosEmpleado()

            If dt.Rows.Count > 0 Then
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=52") IsNot DBNull.Value Then
                    Impuesto = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=52")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=53") IsNot DBNull.Value Then
                    IsrIndemnizacion = (dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=53"))
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=54") IsNot DBNull.Value Then
                    SubsidioAplicado = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=54")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=55") IsNot DBNull.Value Then
                    SubsidioEfectivo = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=55")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=56") IsNot DBNull.Value Then
                    CuotaImss = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=56")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=57") IsNot DBNull.Value Then
                    Faltas = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=57")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=58") IsNot DBNull.Value Then
                    Permisos = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=58")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=59") IsNot DBNull.Value Then
                    IncapacidadPorEnfermedad = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=59")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=60") IsNot DBNull.Value Then
                    DiasHorasNoTrabajadas = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=60")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=61") IsNot DBNull.Value Then
                    CuotaSindical = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=61")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=62") IsNot DBNull.Value Then
                    PensionAlimenticia = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=62")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=63") IsNot DBNull.Value Then
                    AdeudoFonacot = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=63")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=64") IsNot DBNull.Value Then
                    CreditoInfonavit = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=64")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=65") IsNot DBNull.Value Then
                    AdeudoCajaAhorro = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=65")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=66") IsNot DBNull.Value Then
                    AdeudoFondoAhorro = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=66")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=67") IsNot DBNull.Value Then
                    GastosSinComprobar = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=67")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=68") IsNot DBNull.Value Then
                    PagoIndebido = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=68")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=69") IsNot DBNull.Value Then
                    Anticipos = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=69")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=70") IsNot DBNull.Value Then
                    AdeudoSindical = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=70")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=71") IsNot DBNull.Value Then
                    AdeudoPatron = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=71")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=72") IsNot DBNull.Value Then
                    ViaticosDeduccion = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=72")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=73") IsNot DBNull.Value Then
                    AportacionCajaAhorro = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=73")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=74") IsNot DBNull.Value Then
                    AportacionFondoAhorro = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=74")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=75") IsNot DBNull.Value Then
                    AportacionDespensa = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=75")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=76") IsNot DBNull.Value Then
                    AportacionComedor = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=76")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=77") IsNot DBNull.Value Then
                    AportacionVoluntariaSar = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=77")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=78") IsNot DBNull.Value Then
                    AportacionVoluntariaInfonavit = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=78")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=79") IsNot DBNull.Value Then
                    FondoRetiro = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=79")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=80") IsNot DBNull.Value Then
                    PrimaSeguroVida = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=80")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=81") IsNot DBNull.Value Then
                    PrimaSeguroAuto = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=81")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=82") IsNot DBNull.Value Then
                    PrimaGastosMedicos = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=82")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=83") IsNot DBNull.Value Then
                    DevolucionIsr = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=83")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=84") IsNot DBNull.Value Then
                    AjusteIsr = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=84")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=85") IsNot DBNull.Value Then
                    AdeudoCuotaImss = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=85")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=86") IsNot DBNull.Value Then
                    OtrasDeducciones = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=86")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=161") IsNot DBNull.Value Then
                    IncapacidadPorMaternidad = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=161")
                End If
                If dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=162") IsNot DBNull.Value Then
                    IncapacidadPorAccidente = dt.Compute("Sum(IMPORTE)", "CVOCONCEPTO=162")
                End If
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub GuardarExentoYGravado(ByVal NoEmpleado, ByVal NoConceptoGravado, ByVal NoConceptoExento, ByVal ImporteGravado, ByVal ImporteExento)
        Try
            Call CargarVariablesGenerales()

            If NoConceptoGravado.ToString.Length > 0 Then
                'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CVOCONCEPTO, TIPOCONCEPTO, UNIDAD, IMPORTE, GENERADO, TIMBRADO, ENVIADO, SITUACION) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + oDataRowTrabajador("NOEMPLEADO").ToString + "', '" + NoConceptoGravado.ToString + "', 'G', 1, " + ImporteGravado.ToString + ", 'N', 'N', 'N', 'A')"
                Dim cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = periodoID.Value
                cNomina.NoEmpleado = NoEmpleado
                cNomina.CvoConcepto = NoConceptoGravado
                cNomina.TipoConcepto = "G"
                cNomina.Unidad = 7
                cNomina.Importe = ImporteGravado
            End If
            If NoConceptoExento.ToString.Length > 0 Then
                'CadenaSql = "INSERT INTO NOMINAS(EJERCICIO, TIPONOMINA, PERIODO, NOEMPLEADO, CVOCONCEPTO, TIPOCONCEPTO, UNIDAD, IMPORTE, GENERADO, TIMBRADO, ENVIADO, SITUACION) VALUES('" + Ejerciciio.ToString + "', 1, '" + TxtPeriodo.Text + "', '" + oDataRowTrabajador("NOEMPLEADO").ToString + "', '" + NoConceptoExento.ToString + "', 'E', 1, " + ImporteExento.ToString + ", 'N', 'N', 'N', 'A')"
                Dim cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = periodoID.Value
                cNomina.NoEmpleado = NoEmpleado
                cNomina.CvoConcepto = NoConceptoGravado
                cNomina.TipoConcepto = "E"
                cNomina.Unidad = 7
                cNomina.Importe = ImporteExento
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Public Sub CrearCFDNominaSemanal(ByVal NoEmpleado As Integer, ByVal RutaXML As String)
        Dim Comprobante As XmlNode
        urlnomina = 0

        m_xmlDOM = CrearDOM()
        Comprobante = CrearNodoComprobante(NoEmpleado)

        m_xmlDOM.AppendChild(Comprobante)

        IndentarNodo(Comprobante, 1)

        CrearNodoEmisor(Comprobante)
        IndentarNodo(Comprobante, 1)

        CrearNodoReceptor(Comprobante, NoEmpleado)
        IndentarNodo(Comprobante, 1)

        CrearNodoConceptos(Comprobante)
        IndentarNodo(Comprobante, 1)

        CrearNodoComplemento(Comprobante, NoEmpleado)
        IndentarNodo(Comprobante, 0)

        If folio.Value > 0 Then
            Dim path As String = Server.MapPath("~/Certificado/") & CertificadoCliente() & ".cer"
            FolioXml = RutaXML & "\" & serie.Value.ToString & folio.Value.ToString & ".xml"
            SellarCFD(Comprobante, path)
            m_xmlDOM.InnerXml = (Replace(m_xmlDOM.InnerXml, "schemaLocation", "xsi:schemaLocation", , , CompareMethod.Text))
            m_xmlDOM.Save(FolioXml)
        End If

    End Sub
    Private Function CrearDOM() As XmlDocument
        Dim oDOM As New XmlDocument
        Dim Nodo As XmlNode
        Nodo = oDOM.CreateProcessingInstruction("xml", "version=""1.0"" encoding=""utf-8""")
        oDOM.AppendChild(Nodo)
        Nodo = Nothing
        CrearDOM = oDOM
    End Function
    Private Function CrearNodoComprobante(ByVal metodoDePago As String) As XmlNode
        Dim Comprobante As XmlElement
        Comprobante = m_xmlDOM.CreateElement("cfdi:Comprobante", URI_SAT)
        CrearAtributosComprobante(Comprobante, metodoDePago)
        CrearNodoComprobante = Comprobante
    End Function
    Private Sub CrearAtributosComprobante(ByVal Nodo As XmlElement, ByVal NoEmpleado As Integer)

        Call CargarVariablesGenerales()

        Dim LugarExpedicion As String = ""
        Dim dt As New DataTable
        Dim cNomina = New Nomina()
        cNomina.IdEmpresa = Session("IdEmpresa")
        dt = cNomina.ConsultarDatosEmisor()

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                LugarExpedicion = oDataRow("CodigoPostal")
            Next
        End If

        If folio.Value = 0 Then
            Call AsignaSerieFolio(NoEmpleado)
        End If

        dt = New DataTable
        cNomina = New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = cmbCliente.SelectedValue
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = periodoID.Value
        cNomina.TipoConcepto = "D"
        cNomina.Tipo = "N"
        cNomina.NoEmpleado = NoEmpleado
        dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
        cNomina = Nothing

        TotalDeducciones = 0

        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                If Convert.ToDecimal(dt.Rows.Item(i)("Importe")) > 0 Then
                    TotalDeducciones += MyRound(Convert.ToDecimal(dt.Rows.Item(i)("Importe")))
                End If
            Next
        End If

        Nodo.SetAttribute("xmlns:nomina12", "http://www.sat.gob.mx/nomina12")
        Nodo.SetAttribute("xmlns:cfdi", "http://www.sat.gob.mx/cfd/4")
        Nodo.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
        Nodo.SetAttribute("xsi:schemaLocation", "http://www.sat.gob.mx/cfd/4 http://www.sat.gob.mx/sitio_internet/cfd/4/cfdv40.xsd http://www.sat.gob.mx/nomina12 http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina12.xsd")
        Nodo.SetAttribute("Certificado", "")
        Nodo.SetAttribute("Fecha", Format(Now(), "yyyy-MM-ddThh:mm:ss")) 'yyyy-MM-ddTHH:mm:ss
        Nodo.SetAttribute("MetodoPago", "PUE") '(Pago en una sola exhibición)
        'Nodo.SetAttribute("FormaPago", "99") '(Por Definir).
        Nodo.SetAttribute("NoCertificado", "")
        Nodo.SetAttribute("Sello", "")
        Nodo.SetAttribute("SubTotal", MyRound(Convert.ToDecimal(TotalPercepciones)))
        Nodo.SetAttribute("Descuento", MyRound(Convert.ToDecimal(TotalDeducciones)))
        Nodo.SetAttribute("Total", MyRound(Convert.ToDecimal(TotalPercepciones)) - MyRound(Convert.ToDecimal(TotalDeducciones)))
        Nodo.SetAttribute("TipoDeComprobante", "N")
        Nodo.SetAttribute("LugarExpedicion", LugarExpedicion)
        Nodo.SetAttribute("Moneda", "MXN")
        Nodo.SetAttribute("Serie", serie.Value)
        Nodo.SetAttribute("Folio", folio.Value)
        Nodo.SetAttribute("Exportacion", "01")
        Nodo.SetAttribute("Version", "4.0")
    End Sub
    Private Sub AsignaSerieFolio(ByVal NoEmpleado As Integer)
        '
        '   Obtiene serie y folio
        '
        Dim dt As New DataTable()
        Dim cNomina = New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = periodoID.Value
        cNomina.Tipo = "N"
        cNomina.NoEmpleado = NoEmpleado
        dt = cNomina.AsignaSerieFolio()
        '
        If dt.Rows.Count > 0 Then
            serie.Value = dt.Rows.Item(0)("serie").ToString()
            folio.Value = dt.Rows.Item(0)("folio")
        End If
        '
    End Sub
    Private Sub IndentarNodo(ByVal Nodo As XmlNode, ByVal Nivel As Long)
        Nodo.AppendChild(m_xmlDOM.CreateTextNode(vbNewLine & New String(ControlChars.Tab, Nivel)))
    End Sub
    Private Sub CrearNodoEmisor(ByVal Nodo As XmlNode)
        Dim dtEmisor As New DataTable
        Dim cNomina = New Nomina()
        cNomina.IdEmpresa = Session("IdEmpresa")
        dtEmisor = cNomina.ConsultarDatosEmisor()

        Dim Emisor As XmlElement

        If dtEmisor.Rows.Count > 0 Then
            For Each oDataRow In dtEmisor.Rows
                Emisor = CrearNodo("cfdi:Emisor")
                Emisor.SetAttribute("Nombre", oDataRow("RazonSocial"))
                Emisor.SetAttribute("Rfc", oDataRow("RFC"))
                Emisor.SetAttribute("RegimenFiscal", oDataRow("RegimenId"))
                Nodo.AppendChild(Emisor)
            Next
        End If
    End Sub
    Private Sub CrearNodoReceptor(ByVal Nodo As XmlNode, ByVal NoEmpleado As Integer)

        Call CargarVariablesGenerales()

        Dim dtReceptor As New DataTable
        Dim cNomina As New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = periodoID.Value
        cNomina.Tipo = "N"
        cNomina.EsEspecial = True
        dtReceptor = cNomina.ConsultarDatosEmpleadosGenerados()

        Dim Receptor As XmlElement

        If dtReceptor.Rows.Count > 0 Then
            For Each oDataRow In dtReceptor.Rows
                Receptor = CrearNodo("cfdi:Receptor")
                Receptor.SetAttribute("Rfc", oDataRow("RFC"))
                Receptor.SetAttribute("Nombre", oDataRow("NOMBRE"))
                Receptor.SetAttribute("UsoCFDI", "CN01") 'Por definir
                Receptor.SetAttribute("DomicilioFiscalReceptor", oDataRow("CP"))
                Receptor.SetAttribute("RegimenFiscalReceptor", "605") '
                Nodo.AppendChild(Receptor)
            Next
        End If
    End Sub
    Private Function CrearNodo(ByVal nombre As String)
        If urlnomina = 0 Then
            CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, nombre, URI_SAT)
        Else
            CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, nombre, "http://www.sat.gob.mx/nomina12")
        End If
    End Function
    Private Sub CrearNodoConceptos(ByVal Nodo As XmlNode)
        Dim Conceptos As XmlElement
        Dim Concepto As XmlElement

        Conceptos = CrearNodo("cfdi:Conceptos")
        IndentarNodo(Conceptos, 2)

        Concepto = CrearNodo("cfdi:Concepto")
        Concepto.SetAttribute("ClaveProdServ", "84111505") 'Servicios de Contabilidad de Sueldos y Salarios
        Concepto.SetAttribute("Importe", Format(MyRound(Convert.ToDecimal(TotalPercepciones)), "#0.00"))
        Concepto.SetAttribute("Descuento", Format(MyRound(Convert.ToDecimal(TotalDeducciones)), "#0.00"))
        Concepto.SetAttribute("ValorUnitario", Format(MyRound(Convert.ToDecimal(TotalPercepciones)), "#0.00"))
        Concepto.SetAttribute("Descripcion", "Pago de nómina")
        Concepto.SetAttribute("ClaveUnidad", "ACT")
        Concepto.SetAttribute("Cantidad", "1")
        Concepto.SetAttribute("ObjetoImp", "01")
        Conceptos.AppendChild(Concepto)
        IndentarNodo(Conceptos, 2)
        Nodo.AppendChild(Conceptos)
    End Sub
    Private Sub CrearNodoComplemento(ByVal Nodo As XmlNode, ByVal NoEmpleado As Integer)

        Call CargarVariablesGenerales()

        Dim cPeriodo As New Entities.Periodo()
        cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
        cPeriodo.ConsultarPeriodoID()

        Dim dtEmpleado As New DataTable
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = cmbCliente.SelectedValue
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = periodoID.Value
        cNomina.Tipo = "N"
        cNomina.EsEspecial = True
        dtEmpleado = cNomina.ConsultarDatosEmpleadosGenerados()

        Dim Complemento As XmlElement
        Dim Nomina As XmlElement
        Dim Percepciones As XmlElement
        Dim Percepcion As XmlElement
        Dim Deducciones As XmlElement
        Dim Deduccion As XmlElement
        'Dim Incapacidades As XmlElement
        'Dim Incapacidad As XmlElement
        Dim SeparacionIndemnizacion As XmlElement
        Dim HorasExtra As XmlElement
        Dim Emisor As XmlElement
        Dim Receptor As XmlElement
        Dim OtrosPagos As XmlElement
        Dim OtroPago As XmlElement
        Dim SubsidioAlEmpleo As XmlElement

        Dim TotalExento As Decimal = 0
        Dim TotalGravado As Decimal = 0
        Dim TotalOtrasDeducciones As Decimal = 0
        Dim TotalImpuestosRetenidos As Decimal = 0
        Dim TotalPercepciones As Decimal = 0
        Dim TotalDeducciones As Decimal = 0
        Dim TotalHorasExtra As Decimal = 0
        Dim TotalOtrosPagos As Decimal = 0
        Dim TotalSeparacionIndemnizacion As Decimal = 0
        Dim ImporteExentoHorasExtra As Decimal = 0
        Dim ImporteGravadoHorasExtra As Decimal = 0
        Dim TotalGravadoHorasExtra As Decimal = 0
        Dim SubSidioEmpleo As Decimal = 0

        Dim formatoFecha As String = "dd/MM/yyyy"

        If dtEmpleado.Rows.Count > 0 Then
            For Each oDataRow In dtEmpleado.Rows

                Dim dt As New DataTable
                cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = cmbCliente.SelectedValue
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = periodoID.Value
                cNomina.TipoConcepto = "P"
                cNomina.Tipo = "N"
                cNomina.NoEmpleado = NoEmpleado
                cNomina.OtroPagoBit = 1
                dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                cNomina = Nothing

                If dt.Rows.Count > 0 Then
                    '- Otros pagos
                    '16 - Otros
                    '17 - Subsidio para el empleo
                    '* Horas extra
                    '19 - Horas extra
                    '* Indemnizaciones
                    '22 - Prima por antigüedad
                    '23 - Pagos por separación
                    '25 - Indemnizaciones
                    '* Jubilaciones
                    '39 - Jubilaciones, pensiones o haberes de retiro
                    '44 - Jubilaciones, pensiones o haberes de retiro en parcialidades
                    '* Otros pagos
                    '999 - PREVISION SOCIAL
                    If dt.Compute("SUM(Importe)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44' and CvoSAT<>'999'") IsNot DBNull.Value Then
                        TotalPercepciones = Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44' and CvoSAT<>'999'"))
                    End If
                    If dt.Compute("SUM(ImporteGravado)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44' and CvoSAT<>'999'") IsNot DBNull.Value Then
                        TotalGravado = Convert.ToDecimal(dt.Compute("SUM(ImporteGravado)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44' and CvoSAT<>'999'"))
                    End If
                    If dt.Compute("SUM(ImporteExento)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44' and CvoSAT<>'999'") IsNot DBNull.Value Then
                        TotalExento = Convert.ToDecimal(dt.Compute("SUM(ImporteExento)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44' and CvoSAT<>'999'"))
                    End If
                    If dt.Compute("SUM(Importe)", "CvoSAT='19'") IsNot DBNull.Value Then
                        TotalHorasExtra = Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT='19'"))
                    End If
                    If dt.Compute("SUM(ImporteExento)", "CvoSAT='19'") IsNot DBNull.Value Then
                        ImporteExentoHorasExtra = Convert.ToDecimal(dt.Compute("SUM(ImporteExento)", "CvoSAT='19'"))
                    End If
                    If dt.Compute("SUM(ImporteGravado)", "CvoSAT='19'") IsNot DBNull.Value Then
                        ImporteGravadoHorasExtra = Convert.ToDecimal(dt.Compute("SUM(ImporteGravado)", "CvoSAT='19'"))
                        TotalGravadoHorasExtra = Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT='19'"))

                        'Modificacion para que cuadre el total Gravado cuando lleva horas extra
                        'ImporteGravadoHorasExtra = Math.Round(TotalGravadoHorasExtra, 2, MidpointRounding.AwayFromZero) - Math.Round(Convert.ToDecimal(ImporteExentoHorasExtra), 2, MidpointRounding.AwayFromZero)
                        ImporteGravadoHorasExtra = MyRound(Convert.ToDecimal(TotalGravadoHorasExtra)) - MyRound(Convert.ToDecimal(ImporteExentoHorasExtra))
                    End If
                    'If dt.Compute("SUM(Importe)", "CvoSAT='16' or CvoSAT='17' or CvoSAT='18'") IsNot DBNull.Value Then
                    '    TotalOtrosPagos = Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT='16' or CvoSAT='17' or CvoSAT='18'"))
                    'End If
                    'If dt.Compute("SUM(Importe)", "CvoSAT='16'") IsNot DBNull.Value Then
                    '    TotalOtrosPagos = MyRound(Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT='16'")))
                    'End If
                    'If dt.Compute("SUM(Importe)", "CvoSAT='17'") IsNot DBNull.Value Then
                    '    TotalOtrosPagos = TotalOtrosPagos + MyRound(Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT='17'")))
                    'End If
                    'If dt.Compute("SUM(Importe)", "CvoSAT='18'") IsNot DBNull.Value Then
                    '    TotalOtrosPagos = TotalOtrosPagos + MyRound(Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT='18'")))
                    'End If
                    If dt.Compute("SUM(Importe)", "CvoSAT='999'") IsNot DBNull.Value Then
                        TotalOtrosPagos = TotalOtrosPagos + MyRound(Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT='999'")))
                    End If
                End If

                dt = New DataTable
                cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = cmbCliente.SelectedValue
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = periodoID.Value
                cNomina.TipoConcepto = "D"
                cNomina.Tipo = "N"
                cNomina.NoEmpleado = NoEmpleado
                dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                cNomina = Nothing

                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        If Convert.ToDecimal(dt.Rows.Item(i)("Importe")) > 0 Then
                            TotalDeducciones += MyRound(Convert.ToDecimal(dt.Rows.Item(i)("Importe")))
                            If dt.Rows.Item(i)("CvoSAT").ToString = "2" Then
                                TotalImpuestosRetenidos += MyRound(Convert.ToDecimal(dt.Rows.Item(i)("Importe")))
                            End If
                        End If
                    Next
                End If

                Complemento = CrearNodo("cfdi:Complemento")

                urlnomina = 1
                Nomina = CrearNodo("nomina12:Nomina")

                Call ConsultarNumeroDeDiasPagados(NoEmpleado)

                Dim FechaPago As Date = Date.ParseExact(oDataRow("FechaPago"), formatoFecha, System.Globalization.CultureInfo.InvariantCulture)
                Dim FechaInicialPago As Date = Date.ParseExact(oDataRow("FechaInicialPago"), formatoFecha, System.Globalization.CultureInfo.InvariantCulture)
                Dim FechaFinalPago As Date = Date.ParseExact(oDataRow("FechaFinalPago"), formatoFecha, System.Globalization.CultureInfo.InvariantCulture)
                Nomina.SetAttribute("Version", "1.2")
                Nomina.SetAttribute("TipoNomina", "E")
                Nomina.SetAttribute("FechaPago", Format(FechaPago, "yyyy-MM-dd"))
                Nomina.SetAttribute("FechaInicialPago", Format(FechaInicialPago, "yyyy-MM-dd"))
                Nomina.SetAttribute("FechaFinalPago", Format(FechaFinalPago, "yyyy-MM-dd"))
                Nomina.SetAttribute("NumDiasPagados", Format(CDbl(oDataRow("DiasPagados")), "0.000"))

                If MyRound(MyRound(TotalPercepciones) + MyRound(TotalHorasExtra)) Then
                    Nomina.SetAttribute("TotalPercepciones", MyRound(MyRound(TotalPercepciones) + MyRound(TotalHorasExtra)))
                End If

                If TotalDeducciones > 0 Then
                    Nomina.SetAttribute("TotalDeducciones", MyRound(TotalDeducciones))
                End If

                If TotalOtrosPagos > 0 Then
                    Nomina.SetAttribute("TotalOtrosPagos", MyRound(TotalOtrosPagos))
                End If

                Emisor = CrearNodo("nomina12:Emisor")

                Dim ObjData As New DataControl(0)
                Dim ds As New DataSet
                Dim registro_patronal As String = ""
                Dim fac_rfc As String = ""

                ds = ObjData.FillDataSet("exec pCliente @cmd=3, @clienteid='" & Session("IdEmpresa") & "'")

                If ds.Tables.Count > 0 Then
                    For Each row As DataRow In ds.Tables(0).Rows
                        registro_patronal = row("registro_patronal")
                        fac_rfc = row("rfc")
                    Next
                End If

                If oDataRow("RegistroPatronal").ToString <> "" Then
                    Emisor.SetAttribute("RegistroPatronal", registro_patronal)
                End If

                'Emisor.SetAttribute("RfcPatronOrigen", fac_rfc)

                'El atributo curp aplica solo cuando el regimen fiscal es 612 o 621
                'If Regimen = "612" or Regimen = "621" Then
                '   If oDataRow("curp_emisor").ToString <> "" Then
                '       Emisor.SetAttribute("Curp", oDataRow("curp_emisor"))
                '   End If
                'End If

                Nomina.AppendChild(Emisor)

                Dim FechaInicioRelLaboral = Date.ParseExact(oDataRow("FechaInicioRelLaboral"), formatoFecha, System.Globalization.CultureInfo.InvariantCulture)

                Receptor = CrearNodo("nomina12:Receptor")
                If oDataRow("Curp").ToString <> "" Then
                    Receptor.SetAttribute("Curp", oDataRow("Curp"))
                End If
                If oDataRow("NumSeguridadSocial").ToString <> "" Then
                    Receptor.SetAttribute("NumSeguridadSocial", oDataRow("NumSeguridadSocial"))
                End If
                Receptor.SetAttribute("FechaInicioRelLaboral", Format(FechaInicioRelLaboral, "yyyy-MM-dd"))
                Dim Antiguedad As String = ""
                cNomina = New Nomina()
                cNomina.Periodo = periodoID.Value
                Antiguedad = cNomina.ConsultarAntiguedad(FechaInicioRelLaboral)
                Receptor.SetAttribute("Antigüedad", Antiguedad)
                Receptor.SetAttribute("TipoContrato", oDataRow("TipoContrato"))
                Receptor.SetAttribute("TipoJornada", oDataRow("TipoJornada"))
                Receptor.SetAttribute("TipoRegimen", oDataRow("TipoRegimen"))
                Receptor.SetAttribute("NumEmpleado", oDataRow("NoEmpleado"))
                If oDataRow("Departamento").ToString <> "" Then
                    Receptor.SetAttribute("Departamento", oDataRow("Departamento"))
                End If
                If oDataRow("Puesto").ToString <> "" Then
                    Receptor.SetAttribute("Puesto", oDataRow("Puesto"))
                End If
                If oDataRow("RiesgoPuesto").ToString <> "" Then
                    Receptor.SetAttribute("RiesgoPuesto", oDataRow("RiesgoPuesto"))
                End If

                Receptor.SetAttribute("PeriodicidadPago", "99") 'Otra Periodicidad

                If oDataRow("ClaveMetodoPago") <> "1" Then
                    If oDataRow("Clabe").ToString.Length = 10 Or oDataRow("Clabe").ToString.Length = 11 Or oDataRow("Clabe").ToString.Length = 15 Or oDataRow("Clabe").ToString.Length = 16 Or oDataRow("Clabe").ToString.Length = 18 Then
                        If oDataRow("Clabe").ToString.Length = 18 Then
                            Receptor.SetAttribute("CuentaBancaria", oDataRow("Clabe"))
                        Else
                            Receptor.SetAttribute("CuentaBancaria", oDataRow("Clabe").ToString) 'El valor de este atributo debe tener una longitud de 10, 11, 16 ó 18 posiciones. Si se registra una cuenta CLABE (número con 18 posiciones), no debe existir el Catributo Banco.Se debe confirmar que el dígito de control es correcto. Si se registra una cuenta de tarjeta de débito a 16 posiciones o una cuenta bancaria a 11 posiciones o un número de teléfono celular a 10 posiciones, debe existir el banco.
                            Receptor.SetAttribute("Banco", oDataRow("Banco"))
                        End If
                    End If
                End If

                Receptor.SetAttribute("SalarioBaseCotApor", MyRound(Convert.ToDecimal(oDataRow("SalarioBase"))))
                Receptor.SetAttribute("SalarioDiarioIntegrado", MyRound(Convert.ToDecimal(oDataRow("SalarioDiarioIntegrado"))))
                Receptor.SetAttribute("ClaveEntFed", oDataRow("ClaveEstado"))

                'Dim SubContratacion As XmlElement
                'SubContratacion = CrearNodo("nomina12:SubContratacion")
                'SubContratacion.SetAttribute("RfcLabora", oDataRow("RfcLabora"))
                'SubContratacion.SetAttribute("PorcentajeTiempo", "100")
                'Receptor.AppendChild(SubContratacion)

                Nomina.AppendChild(Receptor)

                Percepciones = CrearNodo("nomina12:Percepciones")
                Percepciones.SetAttribute("TotalSueldos", MyRound(TotalGravado + TotalExento + TotalHorasExtra))
                Percepciones.SetAttribute("TotalGravado", MyRound(TotalGravado + ImporteGravadoHorasExtra))
                Percepciones.SetAttribute("TotalExento", MyRound(TotalExento + ImporteExentoHorasExtra))

                If (TotalGravado + TotalExento + TotalHorasExtra) > 0 Then
                    Nomina.AppendChild(Percepciones)
                End If


                'Atributo condicional para expresar el importe exento y gravado de las claves tipo percepción 022 Prima por Antigüedad, 023 Pagos por separación y 025 Indemnizaciones.
                If TotalSeparacionIndemnizacion > 0 Then
                    'Percepciones.SetAttribute("TotalSeparacionIndemnizacion", Math.Round(TotalSeparacionIndemnizacion, 2, MidpointRounding.AwayFromZero))
                    Percepciones.SetAttribute("TotalSeparacionIndemnizacion", MyRound(Convert.ToDecimal(TotalSeparacionIndemnizacion)))
                End If

                dt = New DataTable
                cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = cmbCliente.SelectedValue
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = periodoID.Value
                cNomina.TipoConcepto = "P"
                cNomina.Tipo = "N"
                cNomina.NoEmpleado = NoEmpleado
                cNomina.OtroPagoBit = 1
                dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                cNomina = Nothing
                '
                ' Percepciones
                '
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        If Convert.ToDecimal(dt.Rows.Item(i)("Importe").ToString) > 0 And dt.Rows.Item(i)("CvoSAT").ToString <> "999" Then
                            Percepcion = CrearNodo("nomina12:Percepcion")
                            Percepcion.SetAttribute("TipoPercepcion", String.Format("{0:000}", CInt(dt.Rows.Item(i)("CvoSAT"))))
                            Percepcion.SetAttribute("Clave", String.Format("{0:000}", CInt(dt.Rows.Item(i)("CvoConcepto").ToString)))
                            Percepcion.SetAttribute("Concepto", dt.Rows.Item(i)("Concepto").ToString)
                            Percepcion.SetAttribute("ImporteGravado", MyRound(Convert.ToDecimal(dt.Rows.Item(i)("ImporteGravado"))))
                            Percepcion.SetAttribute("ImporteExento", MyRound(Convert.ToDecimal(dt.Rows.Item(i)("ImporteExento"))))
                            Percepciones.AppendChild(Percepcion)
                        End If
                    Next
                End If
                '
                '   SeparacionIndemnizacion (Finiquitos)
                '
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        If Convert.ToDecimal(dt.Rows.Item(i)("Importe").ToString) > 0 And dt.Rows.Item(i)("CvoSAT").ToString = "22" Or dt.Rows.Item(i)("CvoSAT").ToString = "23" Or dt.Rows.Item(i)("CvoSAT").ToString = "25" Then
                            SeparacionIndemnizacion = CrearNodo("nomina12:SeparacionIndemnizacion")
                            SeparacionIndemnizacion.SetAttribute("TotalPagado", MyRound(Convert.ToDecimal(dt.Rows.Item(i)("Importe"))))
                            SeparacionIndemnizacion.SetAttribute("NumAñosServicio", 1)
                            SeparacionIndemnizacion.SetAttribute("UltimoSueldoMensOrd", MyRound(Convert.ToDecimal(dt.Rows.Item(i)("Importe"))))
                            SeparacionIndemnizacion.SetAttribute("IngresoAcumulable", 1)
                            SeparacionIndemnizacion.SetAttribute("IngresoNoAcumulable", 1)
                            Percepciones.AppendChild(SeparacionIndemnizacion)
                        End If
                    Next
                End If

                If TotalSeparacionIndemnizacion > 0 Then
                    Percepciones.SetAttribute("TotalSeparacionIndemnizacion", MyRound(Convert.ToDecimal(TotalSeparacionIndemnizacion)))
                End If

                '   Horas Extra

                dt = New DataTable
                cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = cmbCliente.SelectedValue
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = periodoID.Value
                cNomina.TipoConcepto = "P"
                cNomina.CvoSAT = "19"
                cNomina.Tipo = "N"
                cNomina.NoEmpleado = NoEmpleado
                dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                cNomina = Nothing

                If dt.Rows.Count > 0 Then

                    Percepcion = CrearNodo("nomina12:Percepcion")
                    Percepcion.SetAttribute("Clave", "019")
                    Percepcion.SetAttribute("Concepto", "Horas extra")
                    Percepcion.SetAttribute("ImporteExento", MyRound(Convert.ToDecimal(ImporteExentoHorasExtra)))
                    Percepcion.SetAttribute("ImporteGravado", MyRound(Convert.ToDecimal(ImporteGravadoHorasExtra)))
                    Percepcion.SetAttribute("TipoPercepcion", "019")

                    For i = 0 To dt.Rows.Count - 1
                        If Convert.ToDecimal(dt.Rows.Item(i)("Importe")) > 0 Then
                            HorasExtra = CrearNodo("nomina12:HorasExtra")
                            HorasExtra.SetAttribute("Dias", Convert.ToDecimal(dt.Rows.Item(i)("DiasHorasExtra")))
                            HorasExtra.SetAttribute("TipoHoras", dt.Rows.Item(i)("TipoHorasExtra"))
                            HorasExtra.SetAttribute("HorasExtra", Math.Ceiling(Convert.ToDecimal(dt.Rows.Item(i)("Unidad"))))
                            HorasExtra.SetAttribute("ImportePagado", MyRound(Convert.ToDecimal(dt.Rows.Item(i)("Importe"))))
                            Percepcion.AppendChild(HorasExtra)
                        End If
                    Next

                    Percepciones.AppendChild(Percepcion)

                End If

                If TotalPercepciones > 0 Then
                    Nomina.AppendChild(Percepciones)
                End If

                '
                '   Deducciones
                '
                If MyRound(MyRound(TotalDeducciones) - MyRound(TotalImpuestosRetenidos)) > 0 Then
                    Deducciones = CrearNodo("nomina12:Deducciones")
                    Deducciones.SetAttribute("TotalOtrasDeducciones", MyRound(MyRound(TotalDeducciones) - MyRound(TotalImpuestosRetenidos)))
                    If TotalImpuestosRetenidos > 0 Then
                        Deducciones.SetAttribute("TotalImpuestosRetenidos", MyRound(TotalImpuestosRetenidos))
                    End If

                    If MyRound(MyRound(TotalDeducciones) - MyRound(TotalImpuestosRetenidos)) > 0 Then
                        Nomina.AppendChild(Deducciones)
                    End If

                    dt = New DataTable
                    cNomina = New Nomina()
                    cNomina.IdEmpresa = IdEmpresa
                    cNomina.IdCliente = cmbCliente.SelectedValue
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                    cNomina.Periodo = periodoID.Value
                    cNomina.TipoConcepto = "D"
                    cNomina.Tipo = "N"
                    cNomina.NoEmpleado = NoEmpleado
                    dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                    cNomina = Nothing

                    If dt.Rows.Count > 0 Then
                        Dim oDataRowDeducciones As DataRow
                        For Each oDataRowDeducciones In dt.Rows
                            'If oDataRowDeducciones("CvoSAT").ToString <> "6" Then ' Deducciones diferentes a Incapacidad
                            ObtenerUnidad(NoEmpleado, oDataRowDeducciones("CvoConcepto").ToString)
                            Deduccion = CrearNodo("nomina12:Deduccion")
                            Deduccion.SetAttribute("TipoDeduccion", String.Format("{0:000}", Convert.ToInt32(oDataRowDeducciones("CvoSAT"))))
                            Deduccion.SetAttribute("Clave", String.Format("{0:000}", oDataRowDeducciones("CvoConcepto")))
                            Deduccion.SetAttribute("Concepto", oDataRowDeducciones("Concepto").ToString)
                            Deduccion.SetAttribute("Importe", MyRound(Convert.ToDecimal(oDataRowDeducciones("Importe"))))
                            Deducciones.AppendChild(Deduccion)
                            'End If
                        Next
                        Nomina.AppendChild(Deducciones)
                    End If
                End If
                '
                '   Otros pagos
                '
                dt = New DataTable
                cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Tipo = "N"
                cNomina.Periodo = periodoID.Value
                cNomina.TipoConcepto = "P"
                cNomina.NoEmpleado = NoEmpleado
                cNomina.EsEspecial = True
                dt = cNomina.ConsultarOtrosPagos()
                cNomina = Nothing

                OtrosPagos = CrearNodo("nomina12:OtrosPagos")

                If dt.Rows.Count > 0 Then

                    For i = 0 To dt.Rows.Count - 1

                        If dt.Rows.Item(i)("CvoSAT").ToString = "999" Then ' Nomina por fuera
                            OtroPago = CrearNodo("nomina12:OtroPago")
                            OtroPago.SetAttribute("TipoOtroPago", String.Format("{0:000}", CInt(dt.Rows.Item(i)("CvoSAT"))))
                            OtroPago.SetAttribute("Clave", String.Format("{0:000}", CInt(dt.Rows.Item(i)("CvoSAT"))))
                            OtroPago.SetAttribute("Concepto", dt.Rows.Item(i)("Concepto").ToString)
                            OtroPago.SetAttribute("Importe", MyRound(Convert.ToDecimal(dt.Rows.Item(i)("Importe"))))
                            OtrosPagos.AppendChild(OtroPago)
                        End If

                        OtroPago = CrearNodo("nomina12:OtroPago")
                        OtroPago.SetAttribute("TipoOtroPago", "002")
                        OtroPago.SetAttribute("Clave", "054")
                        OtroPago.SetAttribute("Concepto", "SUBSIDIO PARA EL EMPLEO")
                        OtroPago.SetAttribute("Importe", "0.00")

                        SubsidioAlEmpleo = CrearNodo("nomina12:SubsidioAlEmpleo")
                        SubsidioAlEmpleo.SetAttribute("SubsidioCausado", "0.00")
                        OtroPago.AppendChild(SubsidioAlEmpleo)
                        OtrosPagos.AppendChild(OtroPago)
                    Next

                    Nomina.AppendChild(OtrosPagos)

                Else
                    OtroPago = CrearNodo("nomina12:OtroPago")
                    OtroPago.SetAttribute("TipoOtroPago", "002")
                    OtroPago.SetAttribute("Clave", "054")
                    OtroPago.SetAttribute("Concepto", "SUBSIDIO PARA EL EMPLEO")
                    OtroPago.SetAttribute("Importe", "0.00")

                    SubsidioAlEmpleo = CrearNodo("nomina12:SubsidioAlEmpleo")
                    SubsidioAlEmpleo.SetAttribute("SubsidioCausado", "0.00")
                    OtroPago.AppendChild(SubsidioAlEmpleo)
                    OtrosPagos.AppendChild(OtroPago)

                    Nomina.AppendChild(OtrosPagos)

                End If
                '
                ' Incapacidad
                '
                dt = New DataTable
                cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.IdCliente = cmbCliente.SelectedValue
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = periodoID.Value
                cNomina.TipoConcepto = "D"
                cNomina.Tipo = "N"
                cNomina.CvoSAT = "6"
                cNomina.NoEmpleado = NoEmpleado
                dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                cNomina = Nothing

                IndentarNodo(Nomina, 1)
                Nodo.AppendChild(Nomina)

                Complemento.AppendChild(Nomina)
                IndentarNodo(Complemento, 1)
                urlnomina = 0

                IndentarNodo(Complemento, 1)
                Nodo.AppendChild(Complemento)

            Next
        End If

    End Sub
    Private Sub ObtenerUnidad(ByVal NoEmpleado As Integer, ByVal CvoConcepto As String)
        Try

            Call CargarVariablesGenerales()

            Dim cNomina As New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = cmbCliente.SelectedValue
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = CvoConcepto
            TablaUnidad = cNomina.ConsultarConceptosEmpleado()

            If TablaUnidad.Rows.Count > 0 Then
                Unidad = TablaUnidad.Rows(0).Item("Unidad").ToString
            End If
        Catch oExcep As Exception
            MsgBox(oExcep.Message)
        Finally

        End Try
    End Sub
    Private Sub SellarCFD(ByVal NodoComprobante As XmlElement, ByVal Certificado As String)
        Dim objCert As New X509Certificate2()
        Dim bRawData As Byte() = ReadFile(Certificado)
        objCert.Import(bRawData)

        Dim cadena As String = Convert.ToBase64String(bRawData)
        NodoComprobante.SetAttribute("NoCertificado", FormatearSerieCert(objCert.SerialNumber))
        NodoComprobante.SetAttribute("Certificado", Convert.ToBase64String(bRawData))
        NodoComprobante.SetAttribute("Sello", GenerarSello())
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
        Dim ArchivoPFX As String = Server.MapPath("~/PKI/") & CertificadoCliente() & ".pfx"
        Dim privateCert As New X509Certificate2(ArchivoPFX, ContrasenaPfx(), X509KeyStorageFlags.Exportable)
        Dim privateKey As RSACryptoServiceProvider = DirectCast(privateCert.PrivateKey, RSACryptoServiceProvider)
        Dim privateKey1 As New RSACryptoServiceProvider()
        privateKey1.ImportParameters(privateKey.ExportParameters(True))

        Dim Cadena As String = GetCadenaOriginal(m_xmlDOM.InnerXml)
        Dim stringCadenaOriginal() As Byte = System.Text.Encoding.UTF8.GetBytes(Cadena)
        Dim signature As Byte() = privateKey1.SignData(stringCadenaOriginal, "SHA256")
        Dim sello256 As String = Convert.ToBase64String(signature)
        ' Valida Sello
        Dim isValid As Boolean = privateKey1.VerifyData(stringCadenaOriginal, "SHA256", signature)
        If isValid = True Then
            GenerarSello = sello256
        Else
            GenerarSello = ""
        End If
    End Function
    Private Function CertificadoCliente() As String
        Dim Certificado As String = ""
        Dim ObjData As New DataControl(0)
        Certificado = ObjData.RunSQLScalarQueryString("select top 1 isnull(certificado,'') as certificado from tblCliente where id='" & Session("IdEmpresa").ToString & "'")
        Dim elements() As String = Certificado.Split(New Char() {"."c}, StringSplitOptions.RemoveEmptyEntries)
        ObjData = Nothing

        Return elements(0)

    End Function
    Private Function ContrasenaPfx() As String
        Dim contrasena_llave_privada As String = ""
        Dim ObjData As New DataControl(0)
        contrasena_llave_privada = ObjData.RunSQLScalarQueryString("select top 1 isnull(contrasena,'') as contrasena from tblCliente where id='" & Session("IdEmpresa").ToString & "'")
        ObjData = Nothing

        Return contrasena_llave_privada

    End Function
    Public Function GetCadenaOriginal(ByVal xmlCFD As String) As String
        Dim xslt As New Xsl.XslCompiledTransform
        Dim xmldoc As New XmlDocument
        Dim navigator As XPath.XPathNavigator
        Dim output As New IO.StringWriter
        xmldoc.LoadXml(xmlCFD)
        navigator = xmldoc.CreateNavigator()
        xslt.Load("http://www.sat.gob.mx/sitio_internet/cfd/4/cadenaoriginal_4_0/cadenaoriginal_4_0.xslt")
        xslt.Transform(navigator, Nothing, output)
        GetCadenaOriginal = output.ToString
    End Function
    Private Sub GrabarGeneracionXml(ByVal NoEmpleado As Integer)

        Call CargarVariablesGenerales()

        Dim cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Tipo = "N"
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.Generado = "S"
        cNomina.ActualizarEstatusGeneradoNomina()
    End Sub
    Private Sub btnTimbrarNominaSemanal_Click(sender As Object, e As EventArgs) Handles btnTimbrarNominaSemanal.Click

        If cmbPeriodo.SelectedValue > 0 Then
            rwConfirmGeneraNomina.RadConfirm("¿Está seguro de timbrar la nómina, una vez timbrada no podrá hacer modificaciones?", "confirmCallbackFnTimbrarNomina", 330, 180, Nothing, "Confirmar")
        Else
            rwAlerta.RadAlert("Seleccione un periodo.", 330, 180, "Alerta", "", "")
        End If

    End Sub
    Private Sub GrabarTimbrado(ByVal NoEmpleado As Integer, ByVal Timbrado As String, ByVal UUID As String)
        Call CargarVariablesGenerales()

        Dim cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.Timbrado = Timbrado
        cNomina.UUID = UUID
        cNomina.Tipo = "N"
        cNomina.ActualizarEstatusTimbradoNomina()
    End Sub
    Private Function FileToMemory(ByVal Filename As String) As IO.MemoryStream
        Dim FS As New System.IO.FileStream(Filename, IO.FileMode.Open)
        Dim MS As New System.IO.MemoryStream
        Dim BA(FS.Length - 1) As Byte
        FS.Read(BA, 0, BA.Length)
        FS.Close()
        MS.Write(BA, 0, BA.Length)
        Return MS
    End Function
    Private Sub btnGenerarPDF_Click(sender As Object, e As EventArgs) Handles btnGenerarPDF.Click

        If cmbPeriodo.SelectedValue > 0 Then
            rwConfirmGeneraNomina.RadConfirm("¿Está seguro de generar los comprobantes pdf?", "confirmCallbackFnGeneraPDF", 330, 180, Nothing, "Confirmar")
        Else
            rwAlerta.RadAlert("Seleccione un periodo.", 330, 180, "Alerta", "", "")
        End If

    End Sub
    Private Function GeneraPDF(ByVal NoEmpleado As Integer, ByVal UUID As String) As Telerik.Reporting.Report

        Call CargarVariablesGenerales()

        Dim plantillaid As Integer = 1
        Dim Folio As String = ""
        Dim Version As String = ""

        Dim CantidadTexto As String = ""
        Dim emp_nombre As String = ""
        Dim emp_horas_extra_dobles As String = ""
        Dim emp_horas_extra_triples As String = ""

        Dim regimen_fiscal As String = ""
        Dim lugar_expedicion As String = ""

        Dim Percepciones As Decimal = 0
        Dim Deducciones As Decimal = 0
        Dim Neto As Decimal = 0

        Dim ObjData As New DataControl(0)
        Dim ds As New DataSet

        ds = ObjData.FillDataSet("exec pCliente @cmd=3, @clienteid='" & Session("IdEmpresa") & "'")

        If ds.Tables.Count > 0 Then
            For Each row As DataRow In ds.Tables(0).Rows
                lugar_expedicion = "C. P." & row("fac_cp") & " - " & row("fac_municipio") & ", " & row("fac_pais")
                regimen_fiscal = row("regimen_fiscal")
                plantillaid = row("plantillaid")
            Next
        End If

        Dim reporte As New Formatos.formato_nomina
        'reporte.ReportParameters("conn").Value = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        reporte.ReportParameters("conn").Value = ConfigurationManager.ConnectionStrings("conn").ConnectionString

        Dim dt As New DataTable

        Dim cNomina As New Entities.Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = periodoID.Value
        dt = cNomina.ConsultarDatosPDF()

        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                Try
                    emp_horas_extra_dobles = row("emp_horas_extra_dobles")
                    emp_horas_extra_triples = row("emp_horas_extra_triples")
                    emp_nombre = row("emp_nombre")

                    cNomina = New Nomina()
                    cNomina.IdEmpresa = IdEmpresa
                    cNomina.IdCliente = cmbCliente.SelectedValue
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                    cNomina.Periodo = periodoID.Value
                    cNomina.TipoConcepto = "P"
                    cNomina.Tipo = "N"
                    cNomina.NoEmpleado = NoEmpleado
                    cNomina.OtroPagoBit = 1
                    dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                    cNomina = Nothing

                    If dt.Rows.Count > 0 Then
                        Percepciones = Math.Round(dt.Compute("SUM(Importe)", ""), 6)
                    End If

                    cNomina = New Nomina()
                    cNomina.IdEmpresa = IdEmpresa
                    cNomina.IdCliente = cmbCliente.SelectedValue
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                    cNomina.Periodo = periodoID.Value
                    cNomina.TipoConcepto = "D"
                    cNomina.Tipo = "N"
                    cNomina.NoEmpleado = NoEmpleado
                    dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                    cNomina = Nothing

                    If dt.Rows.Count > 0 Then
                        Deducciones = Math.Round(dt.Compute("SUM(Importe)", ""), 6)
                    End If

                    Neto = Percepciones - Deducciones

                    Dim largo = Len(CStr(Format(CDbl(Neto), "#,###.00")))
                    Dim decimales = Mid(CStr(Format(CDbl(Neto), "#,###.00")), largo - 2)

                    FolioXml = Server.MapPath("~/XmlTimbrados/E/").ToString & IdEjercicio & "\" & cmbPeriodo.SelectedValue.ToString & "\" & UUID & ".xml"

                    If Not File.Exists(FolioXml) Then
                        Dim cPeriodo As New Entities.Periodo()
                        cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
                        cPeriodo.ConsultarPeriodoID()

                        FolioXml = Server.MapPath("~\XmlTimbrados\E\").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString & "\" & row("emp_rfc").ToString & "_" & Format(CDate(cPeriodo.FechaInicialDate), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinalDate), "dd-MM-yyyy").ToString & "_" & UUID & ".xml"
                    End If

                    Folio = GetXmlAttribute(FolioXml, "folio", "cfdi:Comprobante").ToString

                    reporte.ReportParameters("plantillaId").Value = plantillaid
                    reporte.ReportParameters("NoEmpleado").Value = NoEmpleado
                    reporte.ReportParameters("Ejercicio").Value = IdEjercicio
                    reporte.ReportParameters("TipoNomina").Value = cmbPeriodicidad.SelectedValue
                    reporte.ReportParameters("Periodo").Value = cmbPeriodo.SelectedValue
                    reporte.ReportParameters("Tipo").Value = "N"

                    CantidadTexto = "( " + Num2Text(Neto - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"

                    Version = GetXmlAttribute(FolioXml, "Version", "cfdi:Comprobante")

                    reporte.ReportParameters("txtNoNomina").Value = "Nómina No. " & Folio.ToString
                    reporte.ReportParameters("txtTipoNomina").Value = GetXmlAttribute(FolioXml, "TipoNomina", "nomina12:Nomina").ToString & " - Ordinaria"
                    reporte.ReportParameters("txtLugarExpedicion1").Value = lugar_expedicion.ToString
                    reporte.ReportParameters("txtUUID").Value = GetXmlAttribute(FolioXml, "UUID", "tfd:TimbreFiscalDigital").ToString.ToUpper
                    reporte.ReportParameters("txtSerieEmisor").Value = GetXmlAttribute(FolioXml, "NoCertificado", "cfdi:Comprobante")
                    reporte.ReportParameters("txtSerieCertificadoSAT").Value = GetXmlAttribute(FolioXml, "NoCertificadoSAT", "tfd:TimbreFiscalDigital")


                    reporte.ReportParameters("txtRegimenEmisor").Value = GetXmlAttribute(FolioXml, "RegimenFiscal", "cfdi:Emisor") & " - " & regimen_fiscal


                    Try
                        reporte.ReportParameters("txtRegistroPatronal").Value = GetXmlAttribute(FolioXml, "RegistroPatronal", "nomina12:Emisor").ToString
                    Catch ex As Exception
                        reporte.ReportParameters("txtRegistroPatronal").Value = ""
                    End Try

                    reporte.ReportParameters("txtTipoContrato").Value = row("TipoNomina").ToString


                    reporte.ReportParameters("txtTipoComprobante").Value = GetXmlAttribute(FolioXml, "TipoDeComprobante", "cfdi:Comprobante").ToUpper & " - Nómina"


                    reporte.ReportParameters("txtFechaEmision").Value = GetXmlAttribute(FolioXml, "Fecha", "cfdi:Comprobante").ToString


                    reporte.ReportParameters("txtFormaPago").Value = GetXmlAttribute(FolioXml, "FormaPago", "cfdi:Comprobante").ToString & " - Por definir"



                    reporte.ReportParameters("txtMetodoPago").Value = GetXmlAttribute(FolioXml, "MetodoPago", "cfdi:Comprobante").ToString & " - Pago en una sola exhibición"


                    reporte.ReportParameters("txtEmpleadoNo").Value = GetXmlAttribute(FolioXml, "NumEmpleado", "nomina12:Receptor").ToString
                    reporte.ReportParameters("txtEmpleadoNombre").Value = emp_nombre.ToString
                    reporte.ReportParameters("txtEmpleadoRFC").Value = GetXmlAttribute(FolioXml, "Rfc", "cfdi:Receptor").ToString
                    reporte.ReportParameters("txtEmpleadoCURP").Value = GetXmlAttribute(FolioXml, "Curp", "nomina12:Receptor").ToString
                    reporte.ReportParameters("txtEmpleadoNoSeguroSocial").Value = GetXmlAttribute(FolioXml, "NumSeguridadSocial", "nomina12:Receptor").ToString
                    reporte.ReportParameters("txtEmpleadoDiasLaborados").Value = GetXmlAttribute(FolioXml, "NumDiasPagados", "nomina12:Nomina").ToString
                    reporte.ReportParameters("txtEmpleadoFechaIngreso").Value = GetXmlAttribute(FolioXml, "FechaInicioRelLaboral", "nomina12:Receptor").ToString
                    Try
                        reporte.ReportParameters("txtEmpleadoAntiguedad").Value = GetXmlAttribute(FolioXml, "Antigüedad", "nomina12:Receptor").ToString
                    Catch ex As Exception
                        reporte.ReportParameters("txtEmpleadoAntiguedad").Value = ""
                    End Try

                    reporte.ReportParameters("txtEmpleadoRegimen").Value = GetXmlAttribute(FolioXml, "TipoRegimen", "nomina12:Receptor").ToString & " - " & row("RegimenFiscalEmpleado").ToUpper

                    Try
                        reporte.ReportParameters("txtEmpleadoTipoRiesgo").Value = GetXmlAttribute(FolioXml, "RiesgoPuesto", "nomina12:Receptor").ToString
                    Catch ex As Exception
                        reporte.ReportParameters("txtEmpleadoTipoRiesgo").Value = ""
                    End Try
                    Try
                        reporte.ReportParameters("txtEmpleadoDepartamento").Value = GetXmlAttribute(FolioXml, "Departamento", "nomina12:Receptor").ToString
                    Catch ex As Exception
                        reporte.ReportParameters("txtEmpleadoDepartamento").Value = ""
                    End Try
                    Try
                        reporte.ReportParameters("txtEmpleadoPuesto").Value = GetXmlAttribute(FolioXml, "Puesto", "nomina12:Receptor").ToString
                    Catch ex As Exception
                        reporte.ReportParameters("txtEmpleadoPuesto").Value = ""
                    End Try
                    Try
                        reporte.ReportParameters("txtEmpleadoSalarioDiario").Value = FormatCurrency(CDbl(GetXmlAttribute(FolioXml, "SalarioBaseCotApor", "nomina12:Receptor")), 2).ToString
                    Catch ex As Exception
                        reporte.ReportParameters("txtEmpleadoSalarioDiario").Value = ""
                    End Try
                    Try
                        reporte.ReportParameters("txtEmpleadoSalarioDiarioIntegrado").Value = FormatCurrency(CDbl(GetXmlAttribute(FolioXml, "SalarioDiarioIntegrado", "nomina12:Receptor")), 2).ToString
                    Catch ex As Exception
                        reporte.ReportParameters("txtEmpleadoSalarioDiarioIntegrado").Value = ""
                    End Try

                    reporte.ReportParameters("txtTipoJornada").Value = GetXmlAttribute(FolioXml, "TipoJornada", "nomina12:Receptor").ToString & " - " & row("TipoJornada").ToString
                    reporte.ReportParameters("txtDiasPagados").Value = GetXmlAttribute(FolioXml, "NumDiasPagados", "nomina12:Nomina").ToString
                    reporte.ReportParameters("txtPeriocidadPago").Value = GetXmlAttribute(FolioXml, "PeriodicidadPago", "nomina12:Receptor").ToString & " - " & row("periodo_pago").ToString
                    reporte.ReportParameters("txtFechaInicial").Value = GetXmlAttribute(FolioXml, "FechaInicialPago", "nomina12:Nomina").ToString
                    reporte.ReportParameters("txtFechaFinal").Value = GetXmlAttribute(FolioXml, "FechaFinalPago", "nomina12:Nomina").ToString
                    reporte.ReportParameters("txtTotalPercepciones").Value = FormatCurrency(Percepciones, 2).ToString
                    reporte.ReportParameters("txtTotalDeducciones").Value = FormatCurrency(Deducciones, 2).ToString
                    reporte.ReportParameters("txtTotal").Value = FormatCurrency(Neto, 2).ToString

                    Dim FilePath = Server.MapPath("~/CBB/S/" & UUID.ToString & ".png")
                    If Not File.Exists(FilePath) Then
                        CodigoBidimensional(FolioXml)
                    End If

                    reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/CBB/S/" & UUID.ToString & ".png")
                    reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/logos/ImgBanner.jpg")
                    reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(FolioXml, "Sello", "cfdi:Comprobante")
                    reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(FolioXml, "SelloSAT", "tfd:TimbreFiscalDigital")
                    reporte.ReportParameters("txtFechaCertificacion").Value = GetXmlAttribute(FolioXml, "FechaTimbrado", "tfd:TimbreFiscalDigital")
                    reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString.ToUpper
                    reporte.ReportParameters("txtCadenaOriginal").Value = CadenaOriginalComplemento(FolioXml)

                    Dim leyenda As String = Resources.Resource.LeyendaNominaEspecial.ToUpper
                    reporte.ReportParameters("txtLeyenda").Value = leyenda

                    GrabarPDF(NoEmpleado, "S")

                Catch ex As Exception
                    GrabarPDF(NoEmpleado, "N")
                End Try
            Next
        Else
            GrabarPDF(NoEmpleado, "N")
        End If

        Return reporte

    End Function
    Private Function GeneraPDFNoTimbrado(ByVal NoEmpleado As Integer) As Telerik.Reporting.Report

        Call CargarVariablesGenerales()

        Dim reporte As New Formatos.formato_comisiones
        'reporte.ReportParameters("conn").Value = ConfigurationManager.ConnectionStrings("conn").ConnectionString
        reporte.ReportParameters("conn").Value = ConfigurationManager.ConnectionStrings("conn").ConnectionString

        Dim plantillaid As Integer = 1
        Dim numero_empleado As String = ""
        Dim periodo_pago As String = ""
        Dim fecha_inicial As String = ""
        Dim fecha_final As String = ""
        Dim regimen As String = ""
        Dim metodo_pago As String = ""
        Dim razonsocial As String = ""
        Dim fac_rfc As String = ""
        Dim registro_patronal As String = ""

        Dim emp_nombre As String = ""
        Dim emp_direccion As String = ""
        Dim emp_num_exterior As String = ""
        Dim emp_num_interior As String = ""
        Dim emp_colonia As String = ""
        Dim emp_codigo_postal As String = ""
        Dim emp_municipio As String = ""
        Dim emp_estado As String = ""
        Dim emp_pais As String = ""
        Dim emp_fecha_ingreso As String = ""
        Dim emp_antiguedad As String = ""
        Dim emp_rfc As String = ""
        Dim emp_curp As String = ""
        Dim emp_numero_seguro_social As String = ""
        Dim emp_regimen_contratacion As String = ""
        Dim emp_riesgo_puesto As String = ""
        Dim emp_salario_base As String = ""
        Dim emp_salario_diario_integrado As String = ""
        Dim emp_horas_extra_dobles As String = ""
        Dim emp_horas_extra_triples As String = ""
        Dim emp_tipo_jornada As String = ""
        Dim emp_departamento As String = ""
        Dim emp_puesto As String = ""
        Dim emp_dias_laborados As String = ""
        Dim emp_banco As String = ""
        Dim emp_clabe As String = ""
        Dim empleadoid As String = ""
        Dim CantidadTexto As String = ""
        Dim lugar_expedicion As String = ""
        Dim logo_formato As String = ""
        Dim total_percepciones As Decimal = 0
        Dim total_deducciones As Decimal = 0
        Dim total As Decimal = 0

        Dim ObjData As New DataControl(0)
        Dim ds As New DataSet

        ds = ObjData.FillDataSet("exec pCliente @cmd=3, @clienteid='" & Session("IdEmpresa") & "'")

        If ds.Tables.Count > 0 Then
            For Each row As DataRow In ds.Tables(0).Rows
                lugar_expedicion = "C. P." & row("fac_cp") & " - " & row("fac_municipio") & ", " & row("fac_pais")
                razonsocial = row("razonsocial")
                fac_rfc = row("rfc")
                registro_patronal = row("registro_patronal")
                plantillaid = row("plantillaid")
            Next
        End If

        Dim dt As DataTable = New DataTable()

        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = cmbCliente.SelectedValue
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = periodoID.Value
        cNomina.TipoConcepto = "P"
        cNomina.Tipo = "N"
        cNomina.NoEmpleado = NoEmpleado
        cNomina.OtroPagoBit = 1
        dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            total_percepciones = Math.Round(dt.Compute("SUM(Importe)", ""), 6)
        End If

        cNomina = New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = cmbCliente.SelectedValue
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = periodoID.Value
        cNomina.TipoConcepto = "D"
        cNomina.Tipo = "N"
        cNomina.NoEmpleado = NoEmpleado
        dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            total_deducciones = Math.Round(dt.Compute("SUM(Importe)", ""), 6)
        End If

        total = total_percepciones - total_deducciones

        cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = periodoID.Value
        dt = cNomina.ConsultarDatosPDF()

        Try

            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows

                    Call ConsultarNumeroDeDiasPagados(NoEmpleado)

                    numero_empleado = NoEmpleado
                    periodo_pago = row("periodo_pago")
                    fecha_inicial = row("fecha_inicial")
                    fecha_final = row("fecha_final")
                    'regimen = row("regimen")
                    metodo_pago = row("metodo_pago")

                    emp_nombre = row("emp_nombre")
                    emp_fecha_ingreso = row("emp_fecha_ingreso")
                    emp_rfc = row("emp_rfc")
                    emp_curp = row("emp_curp")
                    emp_numero_seguro_social = row("emp_numero_seguro_social")
                    emp_regimen_contratacion = row("emp_regimen_contratacion")
                    emp_riesgo_puesto = row("emp_riesgo_puesto")
                    emp_salario_base = row("emp_salario_base")
                    emp_salario_diario_integrado = row("emp_salario_diario_integrado")
                    emp_tipo_jornada = row("emp_tipo_jornada")
                    emp_departamento = row("emp_departamento")
                    emp_puesto = row("emp_puesto")
                    emp_dias_laborados = row("emp_dias_laborados")
                    emp_banco = row("emp_banco")
                    emp_clabe = row("emp_clabe")
                    empleadoid = row("empleadoid")

                    Dim largo = Len(CStr(Format(CDbl(total), "#,###.00")))
                    Dim decimales = Mid(CStr(Format(CDbl(total), "#,###.00")), largo - 2)

                    CantidadTexto = "( " + Num2Text(total - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"

                    'reporte.ReportParameters("IdEmpresa").Value = IdEmpresa
                    reporte.ReportParameters("NoEmpleado").Value = NoEmpleado
                    reporte.ReportParameters("Ejercicio").Value = IdEjercicio
                    reporte.ReportParameters("TipoNomina").Value = cmbPeriodicidad.SelectedValue
                    reporte.ReportParameters("Periodo").Value = cmbPeriodo.SelectedValue
                    reporte.ReportParameters("Tipo").Value = "N"
                    reporte.ReportParameters("plantillaId").Value = plantillaid
                    reporte.ReportParameters("empleadoid").Value = empleadoid.ToString
                    reporte.ReportParameters("txtNoNomina").Value = "Recibo de pago"
                    reporte.ReportParameters("txtLugarExpedicion1").Value = lugar_expedicion
                    reporte.ReportParameters("txtRazonSocialEmisor").Value = razonsocial
                    reporte.ReportParameters("txtRFCEmisor").Value = fac_rfc
                    reporte.ReportParameters("txtRegistroPatronal").Value = registro_patronal
                    reporte.ReportParameters("txtTipoComprobante").Value = "N - Nómina"
                    reporte.ReportParameters("txtFormaPago").Value = "99 - Por definir"

                    reporte.ReportParameters("txtEmpleadoNo").Value = numero_empleado.ToString
                    reporte.ReportParameters("txtEmpleadoNombre").Value = emp_nombre.ToString
                    reporte.ReportParameters("txtEmpleadoDireccion").Value = emp_direccion.ToString
                    reporte.ReportParameters("txtEmpleadoNumExterior").Value = emp_num_exterior.ToString
                    reporte.ReportParameters("txtEmpleadoNumInterior").Value = emp_num_interior.ToString
                    reporte.ReportParameters("txtEmpleadoColonia").Value = emp_colonia.ToString
                    reporte.ReportParameters("txtEmpleadoCodigoPostal").Value = emp_codigo_postal.ToString
                    reporte.ReportParameters("txtEmpleadoMunicipio").Value = emp_municipio.ToString
                    reporte.ReportParameters("txtEmpleadoEstado").Value = emp_estado.ToString
                    reporte.ReportParameters("txtEmpleadoPais").Value = emp_pais.ToString
                    reporte.ReportParameters("txtEmpleadoFechaIngreso").Value = emp_fecha_ingreso.ToString
                    reporte.ReportParameters("txtEmpleadoAntiguedad").Value = emp_antiguedad.ToString
                    reporte.ReportParameters("txtEmpleadoRFC").Value = emp_rfc.ToString
                    reporte.ReportParameters("txtEmpleadoCURP").Value = emp_curp.ToString
                    reporte.ReportParameters("txtEmpleadoNoSeguroSocial").Value = emp_numero_seguro_social.ToString

                    reporte.ReportParameters("txtEmpleadoRegimen").Value = emp_regimen_contratacion.ToString
                    reporte.ReportParameters("txtEmpleadoTipoRiesgo").Value = emp_riesgo_puesto.ToString
                    reporte.ReportParameters("txtEmpleadoDepartamento").Value = emp_departamento.ToString
                    reporte.ReportParameters("txtEmpleadoPuesto").Value = emp_puesto.ToString
                    'reporte.ReportParameters("txtEmpleadoSalarioDiario").Value = FormatCurrency(emp_salario_base, 2).ToString
                    'reporte.ReportParameters("txtEmpleadoSalarioDiarioIntegrado").Value = FormatCurrency(emp_salario_diario_integrado, 2).ToString
                    reporte.ReportParameters("txtTipoJornada").Value = emp_tipo_jornada.ToString
                    reporte.ReportParameters("txtDiasPagados").Value = NumeroDeDiasPagados.ToString

                    reporte.ReportParameters("txtPeriocidadPago").Value = periodo_pago.ToString
                    reporte.ReportParameters("txtFechaInicial").Value = fecha_inicial.ToString
                    reporte.ReportParameters("txtFechaFinal").Value = fecha_final.ToString
                    reporte.ReportParameters("txtMetodoPago").Value = "PUE - Pago en una sola exhibición"
                    reporte.ReportParameters("txtBanco").Value = emp_banco.ToString
                    reporte.ReportParameters("txtClabe").Value = emp_clabe.ToString

                    reporte.ReportParameters("txtTotalPercepciones").Value = FormatCurrency(total_percepciones, 2).ToString
                    reporte.ReportParameters("txtTotalDeducciones").Value = FormatCurrency(total_deducciones, 2).ToString
                    reporte.ReportParameters("txtTotal").Value = FormatCurrency(total, 2).ToString
                    reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString.ToUpper
                    reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/logos/ImgBanner.jpg")

                    Dim leyenda As String = Resources.Resource.LeyendaNominaEspecial.ToUpper
                    reporte.ReportParameters("txtLeyenda").Value = leyenda

                    Dim datos As New DataTable()
                    cNomina = New Nomina()
                    cNomina.IdEmpresa = IdEmpresa
                    cNomina.IdCliente = cmbCliente.SelectedValue
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                    cNomina.Periodo = periodoID.Value
                    cNomina.TipoConcepto = "DE"
                    cNomina.Tipo = "N"
                    cNomina.CvoConcepto = 87
                    cNomina.NoEmpleado = NoEmpleado
                    datos = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                    cNomina = Nothing

                    If datos.Rows.Count > 0 Then
                        reporte.ReportParameters("txtEmpleadoSalarioDiario").Value = FormatCurrency(datos.Rows(0).Item("CuotaDiaria"), 2).ToString
                    End If

                Next

                GrabarPDF(NoEmpleado, "S")

            End If
        Catch ex As Exception
            GrabarPDF(NoEmpleado, "N")
        Finally
        End Try

        Return reporte

    End Function
    Private Sub GuardaPDF(ByVal report As Telerik.Reporting.Report, ByVal fileName As String)
        Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
        Dim result As RenderingResult = reportProcessor.RenderReport("PDF", report, Nothing)
        Using fs As New FileStream(fileName, FileMode.Create)
            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length)
        End Using
    End Sub
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
    Private Sub GrabarPDF(ByVal NoEmpleado As Integer, ByVal Pdf As String)
        Call CargarVariablesGenerales()

        Dim cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.Pdf = Pdf
        cNomina.ActualizarEstatusPfNomina()
    End Sub
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
    Public Function GetXmlAttribute(ByVal url As String, campo As String, nodo As String) As String
        '
        '   Obtiene datos del cfdi para construir string del CBB
        '
        Dim valor As String = ""
        Dim FlujoReader As XmlTextReader = Nothing
        Dim i As Integer
        '
        '   Leer del fichero e ignorar los nodos vacios
        '
        FlujoReader = New XmlTextReader(url)
        FlujoReader.WhitespaceHandling = WhitespaceHandling.None
        Try
            While FlujoReader.Read()
                Select Case FlujoReader.NodeType
                    Case XmlNodeType.Element
                        If FlujoReader.Name = nodo Then
                            For i = 0 To FlujoReader.AttributeCount - 1
                                FlujoReader.MoveToAttribute(i)
                                If FlujoReader.Name.ToString.ToUpper = campo.ToUpper Then
                                    valor = FlujoReader.Value.ToString
                                End If
                            Next
                        End If
                End Select
            End While
        Catch ex As Exception
            valor = ""
        End Try
        Return valor
    End Function
    Private Function CadenaOriginalComplemento(ByVal RutaXml As String) As String
        Dim Version As String = ""
        Dim CadenaOriginalTimbre As String = ""
        Dim VersionTimbre As String = ""
        Dim UUID As String = ""
        Dim FechaTimbrado As String = ""
        Dim RfcProvCertif As String = ""
        Dim SelloCFD As String = ""
        Dim NoCertificadoSAT As String = ""

        Version = GetXmlAttribute(RutaXml, "Version", "cfdi:Comprobante")
        VersionTimbre = GetXmlAttribute(RutaXml, "version", "tfd:TimbreFiscalDigital")
        UUID = GetXmlAttribute(RutaXml, "version", "tfd:TimbreFiscalDigital")
        FechaTimbrado = GetXmlAttribute(RutaXml, "FechaTimbrado", "tfd:TimbreFiscalDigital")
        RfcProvCertif = GetXmlAttribute(RutaXml, "RfcProvCertif", "tfd:TimbreFiscalDigital")
        SelloCFD = GetXmlAttribute(RutaXml, "selloCFD", "tfd:TimbreFiscalDigital")
        NoCertificadoSAT = GetXmlAttribute(RutaXml, "noCertificadoSAT", "tfd:TimbreFiscalDigital")


        CadenaOriginalTimbre = "||" & VersionTimbre & "|" & UUID & "|" & FechaTimbrado & "|" & RfcProvCertif & "|" & SelloCFD & "|" & NoCertificadoSAT & "||"


        Return CadenaOriginalTimbre

    End Function
    Private Sub CodigoBidimensional(ByVal RutaXml As String)
        Dim Version As String = ""
        Dim CadenaCodigoBidimensional As String = ""
        Dim FinalSelloDigitalEmisor As String = ""
        Dim RFCEmisor As String = ""
        Dim RFCReceptor As String = ""
        Dim Total As String = ""
        Dim UUID As String = ""
        Dim SelloCFD As String = ""
        '
        '   Obtiene datos del cfdi para construir string del CBB
        ' 
        Version = GetXmlAttribute(RutaXml, "Version", "cfdi:Comprobante")
        Total = GetXmlAttribute(RutaXml, "Total", "cfdi:Comprobante")
        RFCEmisor = GetXmlAttribute(RutaXml, "Rfc", "cfdi:Emisor")
        RFCReceptor = GetXmlAttribute(RutaXml, "Rfc", "cfdi:Receptor")
        UUID = GetXmlAttribute(RutaXml, "UUID", "tfd:TimbreFiscalDigital")
        SelloCFD = GetXmlAttribute(RutaXml, "SelloCFD", "tfd:TimbreFiscalDigital")
        Try
            Total = Format(Convert.ToDouble(Total), "0000000000.000000")
        Catch ex As Exception
            Total = "0000000000.000000"
        End Try

        '
        '   Analizar el fichero y presentar cada nodo
        '
        If Version = "3.2" Then
            CadenaCodigoBidimensional = "?re=" & RFCEmisor & "&rr=" & RFCReceptor & "&tt=" & Total & "&id=" & UUID
        ElseIf Version = "3.3" Or Version = "4.0" Then
            FinalSelloDigitalEmisor = Mid(SelloCFD, (Len(SelloCFD) - 7))
            CadenaCodigoBidimensional = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx" & "?id=" & UUID & "&re=" & RFCEmisor & "&rr=" & RFCReceptor & "&tt=" & Total.ToString & "&fe=" & FinalSelloDigitalEmisor
        End If
        '
        '   Genera gráfico
        '
        Dim qrCodeEncoder As QRCodeEncoder = New QRCodeEncoder
        qrCodeEncoder.QRCodeEncodeMode = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ENCODE_MODE.BYTE
        qrCodeEncoder.QRCodeScale = 6
        qrCodeEncoder.QRCodeErrorCorrect = ThoughtWorks.QRCode.Codec.QRCodeEncoder.ERROR_CORRECTION.L
        'La versión "0" calcula automáticamente el tamaño
        qrCodeEncoder.QRCodeVersion = 0

        qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.FromArgb(qrBackColor)
        qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.FromArgb(qrForeColor)

        Dim CBidimensional As Drawing.Image
        CBidimensional = qrCodeEncoder.Encode(CadenaCodigoBidimensional, System.Text.Encoding.UTF8)
        CBidimensional.Save(Server.MapPath("~/CBB/S/") & UUID & ".png", System.Drawing.Imaging.ImageFormat.Png)
    End Sub
    Private Sub EliminarNominasNoGenerables(NoEmpleado As Integer, Ejercicio As Integer, Periodo As Integer, Periodicidad As Integer)
        Dim objCat As New DataControl(1)
        objCat.RunSQLQuery("DELETE FROM tblNominas WHERE Ejercicio = " & Ejercicio & " AND TipoNomina = " & Periodicidad & " AND Periodo=" & Periodo & " AND Tipo='N'" & " AND NoEmpleado=" & NoEmpleado & " AND Importe=0 AND es_especial=1")
    End Sub
    Private Sub btnConfirmarGeneraNominaElectronica_Click(sender As Object, e As EventArgs) Handles btnConfirmarGeneraNominaElectronica.Click

        Call CargarVariablesGenerales()

        If cmbPeriodo.SelectedValue > 0 Then

            Call BorrarExentosYGravados()

            Dim dt As New DataTable
            Dim cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = cmbCliente.SelectedValue
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.Tipo = "N"
            cNomina.EsEspecial = True
            dt = cNomina.ConsultarEmpleadosNoGeneradosNominaExtraordinaria()

            Dim rutaEmpresa As String = ""
            rutaEmpresa = Server.MapPath("~/XmlGenerados/E/").ToString & "\" & IdEjercicio.ToString

            If Not Directory.Exists(rutaEmpresa) Then
                Directory.CreateDirectory(rutaEmpresa)
            End If

            If dt.Rows.Count > 0 Then

                Dim Total As Integer = dt.Rows.Count
                Dim progress As RadProgressContext = RadProgressContext.Current
                progress.Speed = "N/A"
                Dim i As Integer = 0

                For Each oDataRow In dt.Rows

                    If oDataRow("Importe") = 0 Then
                        EliminarNominasNoGenerables(oDataRow("NoEmpleado"), oDataRow("Ejercicio"), oDataRow("Periodo"), cmbPeriodicidad.SelectedValue)
                    Else
                        i += 1

                        TotalPercepciones = 0
                        TotalDeducciones = 0
                        NetoAPagar = 0
                        PercepcionesGravadas = 0
                        PercepcionesExentas = 0

                        Call CargarVariablesGenerales()

                        Call CargarPercepciones(oDataRow("NoEmpleado"))
                        Call CargarDeducciones(oDataRow("NoEmpleado"))
                        NetoAPagar = (TotalPercepciones - TotalDeducciones)

                        CrearCFDNominaSemanal(oDataRow("NoEmpleado"), rutaEmpresa)
                        GrabarGeneracionXml(oDataRow("NoEmpleado"))

                        serie.Value = ""
                        folio.Value = 0

                        progress.SecondaryTotal = Total
                        progress.SecondaryValue = i
                        progress.SecondaryPercent = Math.Round((i * 100 / Total), 0)

                        progress.CurrentOperationText = "Empleado " & i.ToString()

                        If Not Response.IsClientConnected Then
                            'Cancel button was clicked or the browser was closed, so stop processing
                            Exit For
                        End If

                        progress.TimeEstimated = (Total - i) * 100

                        'Stall the current thread for 0.5 seconds
                        System.Threading.Thread.Sleep(500)

                    End If
                Next

                Response.Redirect("~/GeneracionDeNominaExtraordinaria.aspx?id=" & cmbPeriodo.SelectedValue.ToString & "&nid=" & nominaID.Value.ToString)

            ElseIf dt.Rows.Count = 0 Then
                rwAlerta.RadAlert("Esta nomina ya esta timbrada completamente", 330, 180, "Alerta", "", "")
            End If
        Else
            rwAlerta.RadAlert("Seleccione un periodo.", 330, 180, "Alerta", "", "")
        End If
    End Sub
    Private Sub CargarPercepciones(ByVal NoEmpleado As Integer)

        Call CargarVariablesGenerales()

        Dim dt As New DataTable
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = cmbCliente.SelectedValue
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = periodoID.Value
        cNomina.TipoConcepto = "P"
        cNomina.Tipo = "N"
        cNomina.NoEmpleado = NoEmpleado
        cNomina.OtroPagoBit = 1
        dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            If dt.Compute("Sum(Importe)", "") IsNot DBNull.Value Then
                TotalPercepciones = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
                PercepcionesGravadas = dt.Compute("Sum(ImporteGravado)", "")
                PercepcionesExentas = dt.Compute("Sum(ImporteExento)", "")
            Else
                TotalPercepciones = 0
            End If
        End If

    End Sub
    Private Sub CargarDeducciones(ByVal NoEmpleado As Integer)

        Call CargarVariablesGenerales()

        Dim dt As New DataTable
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = cmbCliente.SelectedValue
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = periodoID.Value
        cNomina.TipoConcepto = "D"
        cNomina.Tipo = "N"
        cNomina.NoEmpleado = NoEmpleado
        dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            If dt.Compute("Sum(Importe)", "") IsNot DBNull.Value Then
                TotalDeducciones = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
            Else
                TotalDeducciones = 0
            End If
        End If

    End Sub
    Private Sub btnConfirmarTimbraNomina_Click(sender As Object, e As EventArgs) Handles btnConfirmarTimbraNomina.Click
        If cmbPeriodo.SelectedValue > 0 Then

            Call BorrarExentosYGravados()

            Call CargarVariablesGenerales()

            Dim dt As New DataTable
            Dim cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = cmbCliente.SelectedValue
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.EsEspecial = True
            cNomina.Tipo = "N"
            dt = cNomina.ConsultarEmpleadosTimbrar()

            Dim FolioXmlTimbrado As String = ""
            Dim rutaEmpresa As String = ""

            rutaEmpresa = Server.MapPath("~/XmlGenerados/E/").ToString & IdEjercicio.ToString

            If Not Directory.Exists(rutaEmpresa) Then
                Directory.CreateDirectory(rutaEmpresa)
            End If

            If dt.Rows.Count > 0 Then

                Dim Total As Integer = dt.Rows.Count
                Dim progress As RadProgressContext = RadProgressContext.Current
                progress.Speed = "N/A"
                Dim i As Integer = 0

                For Each oDataRow In dt.Rows

                    i += 1

                    serie.Value = oDataRow("Serie")
                    folio.Value = oDataRow("Folio")

                    FolioXml = rutaEmpresa & "\" & oDataRow("Serie").ToString & oDataRow("Folio").ToString & ".xml"

                    If Not File.Exists(FolioXml) Then
                        '
                        '   Sellar Comprobante si no existe
                        '
                        TotalPercepciones = 0
                        TotalDeducciones = 0
                        NetoAPagar = 0
                        PercepcionesGravadas = 0
                        PercepcionesExentas = 0

                        Call CargarVariablesGenerales()

                        Call CargarPercepciones(oDataRow("NoEmpleado"))
                        Call CargarDeducciones(oDataRow("NoEmpleado"))
                        NetoAPagar = (TotalPercepciones - TotalDeducciones)

                        CrearCFDNominaSemanal(oDataRow("NoEmpleado"), rutaEmpresa)
                        GrabarGeneracionXml(oDataRow("NoEmpleado"))

                    End If
                    '
                    '   Vuelve a Sellar Comprobante por si se corrigió algun dato del empleado
                    ' 
                    TotalPercepciones = 0
                    TotalDeducciones = 0
                    NetoAPagar = 0
                    PercepcionesGravadas = 0
                    PercepcionesExentas = 0

                    Call CargarVariablesGenerales()

                    Call CargarPercepciones(oDataRow("NoEmpleado"))
                    Call CargarDeducciones(oDataRow("NoEmpleado"))
                    NetoAPagar = (TotalPercepciones - TotalDeducciones)

                    CrearCFDNominaSemanal(oDataRow("NoEmpleado"), rutaEmpresa)
                    GrabarGeneracionXml(oDataRow("NoEmpleado"))

                    Try
                        System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(s As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) True

                        Dim SIFEIUsuario As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIUsuario")
                        Dim SIFEIContrasena As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIContrasena")
                        Dim SIFEIIdEquipo As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIIdEquipo")
                        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType) Or DirectCast(768, System.Net.SecurityProtocolType) Or DirectCast(192, System.Net.SecurityProtocolType) Or DirectCast(48, System.Net.SecurityProtocolType)

                        'Pruebas
                        'Dim TimbreSifei As New SIFEIPruebasV33.SIFEIService()

                        'Producción
                        Dim TimbreSifei As New SIFEI33.SIFEIService()
                        Call Comprimir()

                        Dim bytes() As Byte
                        bytes = TimbreSifei.getCFDI(SIFEIUsuario, SIFEIContrasena, data, "", SIFEIIdEquipo)
                        Descomprimir(bytes, oDataRow("NoEmpleado"), oDataRow("RFC"))
                        '
                        '   Descontar Adeudo Fonacot
                        '
                        Dim dtCreditoFonacot As New DataTable
                        cNomina = New Nomina()
                        cNomina.IdEmpresa = IdEmpresa
                        cNomina.IdCliente = cmbCliente.SelectedValue
                        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                        cNomina.Periodo = cmbPeriodo.SelectedValue
                        cNomina.NoEmpleado = oDataRow("NoEmpleado")
                        cNomina.CvoConcepto = 63
                        dtCreditoFonacot = cNomina.ConsultarConceptosEmpleado()

                        Dim IdCreditoFonacot As Integer
                        Dim ImporteFonacot As Decimal
                        If dtCreditoFonacot.Rows.Count > 0 Then
                            IdCreditoFonacot = dtCreditoFonacot.Rows(0).Item("IdCreditoFonacot")
                            ImporteFonacot = dtCreditoFonacot.Rows(0).Item("Importe")

                            Dim cFonacot = New Fonacot()
                            cFonacot.IdCreditoFonacot = IdCreditoFonacot
                            cFonacot.Ejercicio = IdEjercicio
                            cFonacot.TipoNomina = cmbPeriodicidad.SelectedValue
                            cFonacot.Periodo = cmbPeriodo.SelectedValue
                            cFonacot.NoEmpleado = oDataRow("NoEmpleado")
                            cFonacot.CvoConcepto = 63
                            cFonacot.Importe = ImporteFonacot
                            cFonacot.Serie = serie.Value.ToString
                            cFonacot.Folio = folio.Value
                            cFonacot.UUID = FolioUUID.Value
                            cFonacot.AgregaCreditoFonacotDetalle()
                        End If
                        '
                        '   Descontar Prestamo Personal
                        ' 
                        Dim dtPrestamosPersonal As New DataTable
                        cNomina = New Nomina()
                        cNomina.IdEmpresa = IdEmpresa
                        cNomina.IdCliente = cmbCliente.SelectedValue
                        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                        cNomina.Periodo = cmbPeriodo.SelectedValue
                        cNomina.NoEmpleado = oDataRow("NoEmpleado")
                        cNomina.CvoConcepto = 71
                        dtPrestamosPersonal = cNomina.ConsultarConceptosEmpleado()

                        If dtPrestamosPersonal.Rows.Count > 0 Then
                            Dim dts As New DataTable
                            Dim cPrestamoPersonal = New Entities.PrestamoPersonal()
                            cPrestamoPersonal.NoEmpleado = oDataRow("NoEmpleado")
                            cPrestamoPersonal.Ejercicio = IdEjercicio
                            cPrestamoPersonal.TipoNomina = cmbPeriodicidad.SelectedValue
                            cPrestamoPersonal.Periodo = cmbPeriodo.SelectedValue
                            dts = cPrestamoPersonal.ConsultarEmpleadosConPrestamoPersonalNomina()
                            cPrestamoPersonal = Nothing

                            If dts.Rows.Count > 0 Then
                                For Each row In dts.Rows
                                    cPrestamoPersonal = New Entities.PrestamoPersonal()
                                    cPrestamoPersonal.IdPrestamoPersonal = row("IdPrestamoPersonal")
                                    cPrestamoPersonal.NoEmpleado = oDataRow("NoEmpleado")
                                    cPrestamoPersonal.Ejercicio = IdEjercicio
                                    cPrestamoPersonal.TipoNomina = cmbPeriodicidad.SelectedValue
                                    cPrestamoPersonal.Periodo = cmbPeriodo.SelectedValue
                                    cPrestamoPersonal.Importe = row("Importe")
                                    cPrestamoPersonal.Serie = serie.Value.ToString
                                    cPrestamoPersonal.Folio = folio.Value
                                    cPrestamoPersonal.UUID = FolioUUID.Value
                                    cPrestamoPersonal.ActualizarEmpleadosConPrestamoPersonalNomina()
                                    cPrestamoPersonal = Nothing
                                Next
                            End If
                        End If

                    Catch ex As SoapException
                        '
                        '   Vuelve a Sellar Comprobante por si se corrigió algun dato del empleado
                        '
                        TotalPercepciones = 0
                        TotalDeducciones = 0
                        NetoAPagar = 0
                        PercepcionesGravadas = 0
                        PercepcionesExentas = 0

                        Call CargarVariablesGenerales()

                        Call CargarPercepciones(oDataRow("NoEmpleado"))
                        Call CargarDeducciones(oDataRow("NoEmpleado"))
                        NetoAPagar = (TotalPercepciones - TotalDeducciones)

                        CrearCFDNominaSemanal(oDataRow("NoEmpleado"), rutaEmpresa)
                        GrabarGeneracionXml(oDataRow("NoEmpleado"))

                        Try
                            '
                            '   Vuelve a intentar timbrar el Comprobante Sellado
                            '

                            System.Net.ServicePointManager.ServerCertificateValidationCallback = Function(s As Object, certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) True

                            Dim SIFEIUsuario As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIUsuario")
                            Dim SIFEIContrasena As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIContrasena")
                            Dim SIFEIIdEquipo As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIIdEquipo")

                            'Pruebas
                            'Dim TimbreSifei As New SIFEIPruebasV33.SIFEIService()

                            'Producción
                            Dim TimbreSifei As New SIFEI33.SIFEIService()
                            Call Comprimir()

                            Dim bytes() As Byte
                            bytes = TimbreSifei.getCFDI(SIFEIUsuario, SIFEIContrasena, data, "", SIFEIIdEquipo)
                            Descomprimir(bytes, oDataRow("NoEmpleado"), oDataRow("RFC"))
                            '
                            '   Descontar Adeudo Fonacot
                            ' 
                            Dim dtCreditoFonacot As New DataTable
                            cNomina = New Nomina()
                            cNomina.IdEmpresa = IdEmpresa
                            cNomina.IdCliente = cmbCliente.SelectedValue
                            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                            cNomina.Periodo = cmbPeriodo.SelectedValue
                            cNomina.NoEmpleado = oDataRow("NoEmpleado")
                            cNomina.CvoConcepto = 63
                            dtCreditoFonacot = cNomina.ConsultarConceptosEmpleado()

                            Dim IdCreditoFonacot As Integer
                            Dim ImporteFonacot As Decimal
                            If dtCreditoFonacot.Rows.Count > 0 Then
                                IdCreditoFonacot = dtCreditoFonacot.Rows(0).Item("IdCreditoFonacot")
                                ImporteFonacot = dtCreditoFonacot.Rows(0).Item("Importe")

                                Dim cFonacot = New Fonacot()
                                cFonacot.IdCreditoFonacot = IdCreditoFonacot
                                cFonacot.Ejercicio = IdEjercicio
                                cFonacot.TipoNomina = cmbPeriodicidad.SelectedValue
                                cFonacot.Periodo = cmbPeriodo.SelectedValue
                                cFonacot.NoEmpleado = oDataRow("NoEmpleado")
                                cFonacot.CvoConcepto = 63
                                cFonacot.Importe = ImporteFonacot
                                cFonacot.Serie = serie.Value.ToString
                                cFonacot.Folio = folio.Value
                                cFonacot.UUID = FolioUUID.Value
                                cFonacot.AgregaCreditoFonacotDetalle()
                            End If
                            '
                            '   Descontar Prestamo Personal
                            ' 
                            Dim dtPrestamosPersonal As New DataTable
                            cNomina = New Nomina()
                            cNomina.IdEmpresa = IdEmpresa
                            cNomina.IdCliente = cmbCliente.SelectedValue
                            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                            cNomina.Periodo = cmbPeriodo.SelectedValue
                            cNomina.NoEmpleado = oDataRow("NoEmpleado")
                            cNomina.CvoConcepto = 71
                            dtPrestamosPersonal = cNomina.ConsultarConceptosEmpleado()

                            If dtPrestamosPersonal.Rows.Count > 0 Then
                                Dim dts As New DataTable
                                Dim cPrestamoPersonal = New Entities.PrestamoPersonal()
                                cPrestamoPersonal.NoEmpleado = oDataRow("NoEmpleado")
                                cPrestamoPersonal.Ejercicio = IdEjercicio
                                cPrestamoPersonal.TipoNomina = cmbPeriodicidad.SelectedValue
                                cPrestamoPersonal.Periodo = cmbPeriodo.SelectedValue
                                dts = cPrestamoPersonal.ConsultarEmpleadosConPrestamoPersonalNomina()
                                cPrestamoPersonal = Nothing

                                If dts.Rows.Count > 0 Then
                                    For Each row In dts.Rows
                                        cPrestamoPersonal = New Entities.PrestamoPersonal()
                                        cPrestamoPersonal.IdPrestamoPersonal = row("IdPrestamoPersonal")
                                        cPrestamoPersonal.NoEmpleado = oDataRow("NoEmpleado")
                                        cPrestamoPersonal.Ejercicio = IdEjercicio
                                        cPrestamoPersonal.TipoNomina = cmbPeriodicidad.SelectedValue
                                        cPrestamoPersonal.Periodo = cmbPeriodo.SelectedValue
                                        cPrestamoPersonal.Importe = row("Importe")
                                        cPrestamoPersonal.Serie = serie.Value.ToString
                                        cPrestamoPersonal.Folio = folio.Value
                                        cPrestamoPersonal.UUID = FolioUUID.Value
                                        cPrestamoPersonal.ActualizarEmpleadosConPrestamoPersonalNomina()
                                        cPrestamoPersonal = Nothing
                                    Next
                                End If
                            End If

                        Catch oExcep As SoapException
                            GrabarTimbrado(oDataRow("NoEmpleado"), "N", "")
                            Dim ErrorTimbrado = New ErrorTimbrado()
                            ErrorTimbrado.IdEmpresa = cmbCliente.SelectedValue
                            ErrorTimbrado.Ejercicio = IdEjercicio
                            ErrorTimbrado.TipoNomina = cmbPeriodicidad.SelectedValue
                            ErrorTimbrado.Periodo = cmbPeriodo.SelectedValue
                            ErrorTimbrado.NoEmpleado = oDataRow("NoEmpleado")
                            ErrorTimbrado.IdContrato = oDataRow("IdContrato")
                            ErrorTimbrado.Descripcion = ex.Detail.InnerText.ToString
                            ErrorTimbrado.IdUsuario = Session("usuarioid")
                            ErrorTimbrado.Tipo = "N"
                            ErrorTimbrado.GuadarError()
                        End Try
                    End Try

                    FolioUUID.Value = String.Empty
                    serie.Value = String.Empty
                    folio.Value = 0

                    progress.SecondaryTotal = Total
                    progress.SecondaryValue = i
                    progress.SecondaryPercent = Math.Round((i * 100 / Total), 0)

                    progress.CurrentOperationText = "Empleado " & i.ToString()

                    If Not Response.IsClientConnected Then
                        'Cancel button was clicked or the browser was closed, so stop processing
                        Exit For
                    End If

                    progress.TimeEstimated = (Total - i) * 100

                Next

                Response.Redirect("~/GeneracionDeNominaExtraordinaria.aspx?id=" & cmbPeriodo.SelectedValue.ToString & "&nid=" & nominaID.Value.ToString)

            ElseIf dt.Rows.Count = 0 Then
                rwAlerta.RadAlert("Esta nomina ya esta timbrada completamente!!", 330, 180, "Alerta", "", "")
            End If

        Else
            rwAlerta.RadAlert("Seleccione un periodo.", 330, 180, "Alerta", "", "")
        End If
    End Sub
    Private Function Comprimir()
        Dim zip As ZipFile = New ZipFile(serie.Value.ToString & folio.Value.ToString & ".zip")
        zip.AddFile(FolioXml, "")
        Dim ms As New MemoryStream()
        zip.Save(ms)
        data = ms.ToArray
    End Function
    Private Function Descomprimir(ByVal data5 As Byte(), ByVal NoEmpleado As Integer, ByVal RFC As String)
        Dim ms1 As New MemoryStream(data5)
        Dim zip1 As ZipFile = New ZipFile()
        zip1 = ZipFile.Read(ms1)

        Dim archivo As String = ""
        Dim DirectorioExtraccion As String = ""
        DirectorioExtraccion = Server.MapPath("~\XmlTimbrados\E\").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString & "\"

        If Not Directory.Exists(DirectorioExtraccion) Then
            Directory.CreateDirectory(DirectorioExtraccion)
        End If

        Dim e As ZipEntry
        For Each e In zip1
            archivo = zip1.Entries(0).FileName.ToString
            e.Extract(DirectorioExtraccion, ExtractExistingFileAction.OverwriteSilently)
        Next

        If File.Exists(DirectorioExtraccion & "\" & archivo) Then
            Dim UUID As String = ""
            Dim FolioXmlTimbrado As String = ""

            UUID = GetXmlAttribute(DirectorioExtraccion & archivo, "UUID", "tfd:TimbreFiscalDigital").ToString
            FolioUUID.Value = UUID

            Call GrabarTimbrado(NoEmpleado, "S", UUID)

            Dim cPeriodo As New Entities.Periodo()
            cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
            cPeriodo.ConsultarPeriodoID()

            FolioXmlTimbrado = DirectorioExtraccion & RFC.ToString & "_" & Format(CDate(cPeriodo.FechaInicialDate), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinalDate), "dd-MM-yyyy").ToString & "_" & UUID & ".xml"

            If File.Exists(FolioXml) Then
                My.Computer.FileSystem.CopyFile(DirectorioExtraccion & archivo, FolioXmlTimbrado)
                File.Delete(Server.MapPath("~\XmlGenerados\E\").ToString & IdEjercicio.ToString & "\" & serie.Value.ToString & folio.Value.ToString & ".xml")
            End If

        End If

    End Function
    Private Sub btnConfirmarGeneraPDF_Click(sender As Object, e As EventArgs) Handles btnConfirmarGeneraPDF.Click
        If cmbPeriodo.SelectedValue > 0 Then

            Call CargarVariablesGenerales()

            Dim dt As New DataTable

            Dim cNomina As New Entities.Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = cmbCliente.SelectedValue
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = periodoID.Value
            cNomina.Tipo = "N"
            dt = cNomina.ConsultarEmpleadosGenerados()

            If dt.Rows.Count > 0 Then

                Dim Total As Integer = dt.Rows.Count
                Dim progress As RadProgressContext = RadProgressContext.Current
                progress.Speed = "N/A"
                Dim i As Integer = 0

                For Each row As DataRow In dt.Rows
                    i += 1

                    Dim rutaEmpresa As String = Server.MapPath("~\PDF\E\ST\").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString
                    Dim FilePath = rutaEmpresa & "\" & row("RFC").ToString & ".pdf"

                    If Not Directory.Exists(rutaEmpresa) Then
                        Directory.CreateDirectory(rutaEmpresa)
                    End If

                    If Not File.Exists(FilePath) Then
                        Try
                            GuardaPDF(GeneraPDFNoTimbrado(CInt(row("NoEmpleado"))), FilePath)
                        Catch ex As Exception

                        End Try
                    End If

                    If row("UUID").ToString.Length > 0 Then

                        rutaEmpresa = Server.MapPath("~\PDF\E\T\").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString

                        If Not Directory.Exists(rutaEmpresa) Then
                            Directory.CreateDirectory(rutaEmpresa)
                        End If

                        Dim cPeriodo As New Entities.Periodo()
                        cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
                        cPeriodo.ConsultarPeriodoID()

                        FilePath = rutaEmpresa & "\" & row("RFC").ToString & "_" & Format(CDate(cPeriodo.FechaInicialDate), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinalDate), "dd-MM-yyyy").ToString & "_" & row("UUID") & ".pdf"
                        If Not File.Exists(FilePath) Then
                            GuardaPDF(GeneraPDF(CInt(row("NoEmpleado")), row("UUID")), FilePath)
                        End If

                    End If

                    progress.SecondaryTotal = Total
                    progress.SecondaryValue = i
                    progress.SecondaryPercent = Math.Round((i * 100 / Total), 0)

                    progress.CurrentOperationText = "Empleado " & i.ToString()

                    If Not Response.IsClientConnected Then
                        'Cancel button was clicked or the browser was closed, so stop processing
                        Exit For
                    End If

                    progress.TimeEstimated = (Total - i) * 100

                    'Stall the current thread for 0.5 seconds
                    System.Threading.Thread.Sleep(50)

                Next

                Response.Redirect("~/GeneracionDeNominaExtraordinaria.aspx?id=" & cmbPeriodo.SelectedValue.ToString & "&nid=" & nominaID.Value.ToString)

            End If

        Else
            rwAlerta.RadAlert("Seleccione un periodo.", 330, 180, "Alerta", "", "")
        End If
    End Sub
    Private Sub grdEmpleadosSemanal_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles grdEmpleadosSemanal.ItemCommand
        Select Case e.CommandName
            Case "cmdXML"
                Dim RFC As String = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("RFC"))
                Call DownloadXML(RFC, e.CommandArgument)
            Case "cmdPDF"
                Dim RFC As String = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("RFC"))
                Call DownloadPDF(RFC, e.CommandArgument)
            Case "cmdPDFTimbrado"
                Dim NoEmpleado As Int64 = Convert.ToInt64(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("NoEmpleado"))
                Dim RFC As String = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("RFC"))

                Call DownloadPDFTimbrado(RFC, NoEmpleado, e.CommandArgument)

            Case "cmdSend"
                Dim RFC As String = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("RFC"))
                Call EnviaEmail(e.CommandArgument)
            Case "cmdImportarM"
                NoEmpleadoImportarIndividual = Convert.ToInt64(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("NoEmpleado"))
                Session("NoEmpleadoImportarIndividual") = NoEmpleadoImportarIndividual
                WinImportarMontoIndividual.Title = "Importar Monto"
                WinImportarMontoIndividual.VisibleOnPageLoad = True
        End Select
    End Sub
    Protected Sub btnImportarIndividual_Click(sender As Object, e As EventArgs) Handles btnImportarIndividual.Click
        Dim objCat As New DataControl(1)
        Dim monto As String = txtMontoIndividual.Text
        Dim id_periodicidad As String = cmbPeriodicidad.SelectedValue
        Dim id_cliente As String = cmbCliente.SelectedValue
        Dim id_periodo As String = cmbPeriodo.SelectedValue
        NoEmpleadoImportarIndividual = Session("NoEmpleadoImportarIndividual")
        objCat.RunSQLQuery("UPDATE tblNominas SET Importe='" & monto & "', ImporteExento ='" & monto & "' WHERE TipoNomina='" & id_periodicidad & "' AND IdEmpresa='" & id_cliente & "' AND Periodo='" & id_periodo & "' AND NoEmpleado='" & NoEmpleadoImportarIndividual & "' AND es_especial=1")
        'WinImportarMontoIndividual.VisibleOnPageLoad = True
        Session("ClienteIDNomina") = cmbCliente.SelectedValue
        Session("PeriodicidadIDNomina") = cmbPeriodicidad.SelectedValue
        Session("PeriodoIDNomina") = cmbPeriodo.SelectedValue
        Response.Redirect("~/GeneracionDeNominaExtraordinaria.aspx?id=" & cmbPeriodo.SelectedValue.ToString & "&nid=" & nominaID.Value.ToString)
    End Sub
    Protected Sub btnCancelarIndividual_Click(sender As Object, e As EventArgs) Handles btnCancelarIndividual.Click
        WinImportarMontoIndividual.VisibleOnPageLoad = False
    End Sub
    Private Sub DownloadXML(ByVal RFC As String, ByVal UUID As String)

        Call CargarVariablesGenerales()

        Dim rutaEmpresa As String = ""

        rutaEmpresa = Server.MapPath("~\XmlTimbrados\E\").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString

        If Not Directory.Exists(rutaEmpresa) Then
            Directory.CreateDirectory(rutaEmpresa)
        End If

        Dim FilePath = rutaEmpresa & "\" & UUID & ".xml"
        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        Else

            Dim cPeriodo As New Entities.Periodo()
            cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
            cPeriodo.ConsultarPeriodoID()

            FilePath = rutaEmpresa & "\" & RFC.ToString & "_" & Format(CDate(cPeriodo.FechaInicialDate), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinalDate), "dd-MM-yyyy").ToString & "_" & UUID & ".xml"
            If File.Exists(FilePath) Then
                Dim FileName As String = Path.GetFileName(FilePath)
                Response.Clear()
                Response.ContentType = "application/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
                Response.Flush()
                Response.WriteFile(FilePath)
                Response.End()
            End If
        End If
    End Sub
    Private Sub DownloadPDF(ByVal RFC As String, ByVal NoEmpleado As Integer)

        Call CargarVariablesGenerales()

        Dim rutaEmpresa As String = ""
        rutaEmpresa = Server.MapPath("~\PDF\E\ST\").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString

        If Not Directory.Exists(rutaEmpresa) Then
            Directory.CreateDirectory(rutaEmpresa)
        End If

        Dim FilePathPDF = rutaEmpresa & "\" & RFC.ToString & ".pdf"

        If File.Exists(FilePathPDF) Then
            Dim FileName As String = Path.GetFileName(FilePathPDF)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePathPDF)
            Response.End()
        Else
            Call GuardaPDF(GeneraPDFNoTimbrado(NoEmpleado), FilePathPDF)
            Dim FileName As String = Path.GetFileName(FilePathPDF)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePathPDF)
            Response.End()
        End If
    End Sub
    Private Sub DownloadPDFTimbrado(ByVal RFC As String, ByVal NoEmpleado As Int64, ByVal UUID As String)

        Call CargarVariablesGenerales()

        Dim rutaEmpresa As String = ""
        rutaEmpresa = Server.MapPath("~\PDF\E\T\").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString

        If Not Directory.Exists(rutaEmpresa) Then
            Directory.CreateDirectory(rutaEmpresa)
        End If

        Dim FilePath = rutaEmpresa & "\" & UUID & ".pdf"

        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        Else

            Dim cPeriodo As New Entities.Periodo()
            cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
            cPeriodo.ConsultarPeriodoID()

            FilePath = rutaEmpresa & "\" & RFC.ToString & "_" & Format(CDate(cPeriodo.FechaInicialDate), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinalDate), "dd-MM-yyyy").ToString & "_" & UUID & ".pdf"
            If Not File.Exists(FilePath) Then
                Call GuardaPDF(GeneraPDF(NoEmpleado, UUID), FilePath)
                Dim FileName As String = Path.GetFileName(FilePath)
                Response.Clear()
                Response.ContentType = "application/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
                Response.Flush()
                Response.WriteFile(FilePath)
                Response.End()
            Else
                Dim FileName As String = Path.GetFileName(FilePath)
                Response.Clear()
                Response.ContentType = "application/octet-stream"
                Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
                Response.Flush()
                Response.WriteFile(FilePath)
                Response.End()
            End If
        End If
    End Sub
    Private Sub ConsultarNumeroDeDiasPagados(ByVal NoEmpleado As Integer)
        Try
            Dim DiasVacaciones As Double = 0
            Dim DiasCuotaPeriodo As Double = 0
            Dim DiasHonorarioAsimilado As Double = 0
            Dim DiasPagoPorHoras As Double = 0
            Dim DiasComision As Double = 0
            Dim DiasDestajo As Double = 0
            Dim DiasFaltasPermisosIncapacidades As Double = 0

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = cmbCliente.SelectedValue
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = periodoID.Value
            cNomina.NoEmpleado = NoEmpleado

            dt = cNomina.ConsultarConceptosEmpleado()

            If dt.Rows.Count > 0 Then
                If dt.Compute("Sum(Importe)", "CvoConcepto=2") IsNot DBNull.Value Then
                    DiasCuotaPeriodo = dt.Compute("Sum(UNIDAD)", "CvoConcepto=2")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=3") IsNot DBNull.Value Then
                    DiasComision = 7
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=4") IsNot DBNull.Value Then
                    DiasDestajo = 7
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=5") IsNot DBNull.Value Then
                    DiasHonorarioAsimilado = dt.Compute("Sum(UNIDAD)", "CvoConcepto=5") * 7
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=15") IsNot DBNull.Value Then
                    DiasVacaciones = dt.Compute("Sum(UNIDAD)", "CvoConcepto=15")
                End If
                If dt.Compute("Sum(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") IsNot DBNull.Value Then
                    DiasFaltasPermisosIncapacidades = dt.Compute("Sum(UNIDAD)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162")
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

        Catch oExcep As Exception
            MsgBox(oExcep.Message)
        End Try
    End Sub
    Private Sub btnEnvioComprobantes_Click(sender As Object, e As EventArgs) Handles btnEnvioComprobantes.Click
        If cmbPeriodo.SelectedValue > 0 Then
            rwConfirmGeneraNomina.RadConfirm("¿Está seguro de enviar TODOS los comprobantes generados?", "confirmCallbackFnEnvioPDF", 330, 180, Nothing, "Confirmar")
        Else
            rwAlerta.RadAlert("Seleccione un periodo.", 330, 180, "Alerta", "", "")
        End If
    End Sub
    Private Sub btnConfirmarEnvioPDF_Click(sender As Object, e As EventArgs) Handles btnConfirmarEnvioPDF.Click
        If cmbPeriodo.SelectedValue > 0 Then

            Dim validos As String = ""
            Dim novalidos As String = ""

            Call CargarVariablesGenerales()

            Dim dt As New DataTable
            Dim cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = cmbCliente.SelectedValue
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = periodoID.Value
            cNomina.Tipo = "N"
            dt = cNomina.ConsultarEmpleadosGenerados()

            If dt.Rows.Count > 0 Then

                Dim Total As Integer = dt.Rows.Count
                Dim progress As RadProgressContext = RadProgressContext.Current
                progress.Speed = "N/A"
                Dim i As Integer = 0

                For Each row In dt.Rows
                    i += 1

                    Dim rutaEmpresa As String = Server.MapPath("~\PDF\E\T\").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString

                    If Not Directory.Exists(rutaEmpresa) Then
                        Directory.CreateDirectory(rutaEmpresa)
                    End If

                    Dim cPeriodo As New Entities.Periodo()
                    cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
                    cPeriodo.ConsultarPeriodoID()

                    Dim FilePathPDF = rutaEmpresa & "\" & row("RFC").ToString & "_" & Format(CDate(cPeriodo.FechaInicialDate), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinalDate), "dd-MM-yyyy").ToString & "_" & row("UUID").ToString & ".pdf"

                    If Not File.Exists(FilePathPDF) Then
                        GuardaPDF(GeneraPDF(CInt(row("NoEmpleado")), row("UUID")), FilePathPDF)
                    End If

                    rutaEmpresa = Server.MapPath("~\XmlTimbrados\E\").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString

                    If Not Directory.Exists(rutaEmpresa) Then
                        Directory.CreateDirectory(rutaEmpresa)
                    End If

                    Dim FilePathXML = rutaEmpresa & "\" & row("RFC").ToString & "_" & Format(CDate(cPeriodo.FechaInicialDate), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinalDate), "dd-MM-yyyy").ToString & "_" & row("UUID").ToString & ".xml"

                    '
                    '   Obtiene datos de la persona
                    '
                    Dim mensaje As String = ""
                    Dim razonsocial As String = ""

                    Dim email_from As String = ""
                    Dim email_smtp_server As String = ""
                    Dim email_smtp_username As String = ""
                    Dim email_smtp_password As String = ""
                    Dim email_smtp_port As String = ""
                    '
                    Dim dtEnvioEmail As New DataTable
                    Dim cConfiguracion As New Entities.Configuracion
                    cConfiguracion.IdEmpresa = Session("IdEmpresa")
                    dtEnvioEmail = cConfiguracion.ConsultarDatosEnvioEmail()

                    If dtEnvioEmail.Rows.Count > 0 Then
                        '       
                        razonsocial = dtEnvioEmail.Rows(0)("razonsocial").ToString
                        email_from = dtEnvioEmail.Rows(0)("email_from_nomina").ToString
                        email_smtp_server = dtEnvioEmail.Rows(0)("email_smtp_server").ToString
                        email_smtp_username = dtEnvioEmail.Rows(0)("email_smtp_username").ToString
                        email_smtp_password = dtEnvioEmail.Rows(0)("email_smtp_password").ToString
                        email_smtp_port = dtEnvioEmail.Rows(0)("email_smtp_port").ToString
                        '
                    End If

                    dtEnvioEmail = Nothing
                    cConfiguracion = Nothing

                    Dim correo As String = row("Email").ToString.ToLower.Trim

                    Dim delimit As Char() = New Char() {";"c, ","c}

                    Dim objMM As New MailMessage

                    For Each splitTo As String In correo.Trim().Split(delimit)
                        If validarEmail(splitTo.Trim()) Then
                            objMM.To.Add(splitTo.Trim())
                            If validos = "" Then
                                validos += splitTo.Trim()
                            Else
                                validos += "," & splitTo.Trim()
                            End If
                        Else
                            If novalidos = "" Then
                                novalidos += splitTo.Trim()
                            Else
                                novalidos += "," & splitTo.Trim()
                            End If
                        End If
                    Next

                    'objMM.To.Add("gesquivel@linkium.mx")

                    If validos.Length > 0 Then
                        Dim SmtpMail As New SmtpClient
                        Try

                            Dim nombre_empleado As String = CStr(row("Nombre"))
                            Dim fecha_inicial As String = CStr(row("FechaInicial"))
                            Dim fecha_final As String = CStr(row("FechaFinal"))

                            mensaje = "Estimado(a) " & nombre_empleado.ToString & vbCrLf & vbCrLf
                            mensaje += "Adjunto a este correo estamos enviándole el comprobante XML y PDF de nómina correspondiente al periodo de pago: " & fecha_inicial & " al " & fecha_final & ". Cualquier duda o comentario,  escríbenos a " & email_from.ToString & vbCrLf & vbCrLf
                            mensaje += "Atentamente." & vbCrLf
                            mensaje += "Departamento Nóminas" & vbCrLf
                            'mensaje += razonsocial.ToString.ToUpper & vbCrLf

                            'objMM.From = New MailAddress(Session("email").ToString, razonsocial)
                            objMM.From = New MailAddress(email_from, "Departamento Nóminas")
                            objMM.IsBodyHtml = False
                            objMM.Priority = MailPriority.Normal
                            objMM.Subject = "Recibo de Nómina"
                            objMM.Body = mensaje
                            '
                            '   Agrega anexos
                            '
                            Dim AttachXML As Net.Mail.Attachment
                            Dim AttachPDF As Net.Mail.Attachment
                            AttachXML = New Net.Mail.Attachment(FilePathXML)
                            AttachPDF = New Net.Mail.Attachment(FilePathPDF)
                            objMM.Attachments.Add(AttachXML)
                            objMM.Attachments.Add(AttachPDF)
                            '
                            Dim SmtpUser As New Net.NetworkCredential
                            SmtpUser.UserName = email_smtp_username
                            SmtpUser.Password = email_smtp_password
                            'SmtpUser.Domain = email_smtp_server
                            SmtpMail.EnableSsl = True
                            SmtpMail.Port = email_smtp_port
                            SmtpMail.UseDefaultCredentials = False
                            SmtpMail.Credentials = SmtpUser
                            SmtpMail.Host = email_smtp_server
                            SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network
                            SmtpMail.Send(objMM)
                            '
                            '   Lo marca como enviado a nivel empleado
                            Call GrabarEnviado(row("NoEmpleado"), "S")
                            '
                            progress.SecondaryTotal = Total
                            progress.SecondaryValue = i
                            progress.SecondaryPercent = Math.Round((i * 100 / Total), 0)

                            progress.CurrentOperationText = "Empleado " & i.ToString()

                            If Not Response.IsClientConnected Then
                                'Cancel button was clicked or the browser was closed, so stop processing
                                Exit For
                            End If

                            progress.TimeEstimated = (Total - i) * 100

                            'Stall the current thread for 0.5 seconds
                            System.Threading.Thread.Sleep(50)
                            '
                        Catch ex As Exception
                            Call GrabarEnviado(row("NoEmpleado"), "N")
                        Finally
                            SmtpMail = Nothing
                        End Try
                        objMM = Nothing
                    Else
                        Call GrabarEnviado(row("NoEmpleado"), "N")
                    End If
                Next
            End If

            Call CargarVariablesGenerales()

            cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = cmbCliente.SelectedValue
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.EsEspecial = True
            cNomina.IdNomina = nominaID.Value
            grdEmpleadosSemanal.DataSource = cNomina.ConsultarDetalleNominaExtraordinaria()
            cNomina = Nothing

        End If

    End Sub
    Private Sub EnviaEmail(ByVal NoEmpleado As Integer)
        If cmbPeriodo.SelectedValue > 0 Then

            Dim validos As String = ""
            Dim novalidos As String = ""

            Call CargarVariablesGenerales()

            Dim dt As New DataTable
            Dim cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = cmbCliente.SelectedValue
            cNomina.NoEmpleado = NoEmpleado
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.NoEmpleado = NoEmpleado
            cNomina.Tipo = "N"
            dt = cNomina.ConsultarDatosEnvioPDF()

            If dt.Rows.Count > 0 Then
                For Each row In dt.Rows

                    Dim rutaEmpresa As String = Server.MapPath("~\PDF\E\T\").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString

                    If Not Directory.Exists(rutaEmpresa) Then
                        Directory.CreateDirectory(rutaEmpresa)
                    End If

                    Dim cPeriodo As New Entities.Periodo()
                    cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
                    cPeriodo.ConsultarPeriodoID()

                    Dim FilePathPDF = rutaEmpresa & "\" & row("RFC").ToString & "_" & Format(CDate(cPeriodo.FechaInicialDate), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinalDate), "dd-MM-yyyy").ToString & "_" & row("UUID").ToString & ".pdf"

                    If Not File.Exists(FilePathPDF) Then
                        GuardaPDF(GeneraPDF(CInt(row("NoEmpleado")), row("UUID")), FilePathPDF)
                    End If

                    rutaEmpresa = Server.MapPath("~\XmlTimbrados\E\").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString

                    If Not Directory.Exists(rutaEmpresa) Then
                        Directory.CreateDirectory(rutaEmpresa)
                    End If

                    Dim FilePathXML = rutaEmpresa & "\" & row("RFC").ToString & "_" & Format(CDate(cPeriodo.FechaInicialDate), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinalDate), "dd-MM-yyyy").ToString & "_" & row("UUID").ToString & ".xml"
                    '
                    '   Obtiene datos de la persona
                    '
                    Dim mensaje As String = ""
                    Dim razonsocial As String = ""

                    Dim email_from As String = ""
                    Dim email_smtp_server As String = ""
                    Dim email_smtp_username As String = ""
                    Dim email_smtp_password As String = ""
                    Dim email_smtp_port As String = ""
                    '
                    Dim dtEnvioEmail As New DataTable
                    Dim cConfiguracion As New Entities.Configuracion
                    cConfiguracion.IdEmpresa = Session("IdEmpresa")
                    dtEnvioEmail = cConfiguracion.ConsultarDatosEnvioEmail()

                    If dtEnvioEmail.Rows.Count > 0 Then
                        '       
                        razonsocial = dtEnvioEmail.Rows(0)("razonsocial")
                        email_from = dtEnvioEmail.Rows(0)("email_from_nomina")
                        email_smtp_server = dtEnvioEmail.Rows(0)("email_smtp_server")
                        email_smtp_username = dtEnvioEmail.Rows(0)("email_smtp_username")
                        email_smtp_password = dtEnvioEmail.Rows(0)("email_smtp_password")
                        email_smtp_port = dtEnvioEmail.Rows(0)("email_smtp_port")
                        '
                    End If
                    dtEnvioEmail = Nothing
                    cConfiguracion = Nothing

                    Dim correo As String = row("Email").ToString.ToLower.Trim

                    Dim delimit As Char() = New Char() {";"c, ","c}

                    Dim objMM As New MailMessage

                    For Each splitTo As String In correo.Trim().Split(delimit)
                        If validarEmail(splitTo.Trim()) Then
                            objMM.To.Add(splitTo.Trim())
                            If validos = "" Then
                                validos += splitTo.Trim()
                            Else
                                validos += "," & splitTo.Trim()
                            End If
                        Else
                            If novalidos = "" Then
                                novalidos += splitTo.Trim()
                            Else
                                novalidos += "," & splitTo.Trim()
                            End If
                        End If
                    Next

                    'objMM.To.Add("gesquivel@linkium.mx")

                    If validos.Length > 0 Then
                        Dim SmtpMail As New SmtpClient
                        Try

                            Dim nombre_empleado As String = CStr(row("Nombre"))
                        Dim fecha_inicial As String = CStr(row("FechaInicial"))
                        Dim fecha_final As String = CStr(row("FechaFinal"))

                        mensaje = "Estimado(a) " & nombre_empleado.ToString & vbCrLf & vbCrLf
                        mensaje += "Adjunto a este correo estamos enviándole el comprobante XML y PDF de nómina correspondiente al periodo de pago: " & fecha_inicial & " al " & fecha_final & ". Cualquier duda o comentario,  escríbenos a " & email_from.ToString & vbCrLf & vbCrLf
                        mensaje += "Atentamente." & vbCrLf
                        'mensaje += razonsocial.ToString.ToUpper & vbCrLf
                        mensaje += "Departamento Nóminas" & vbCrLf

                        'objMM.From = New MailAddress(Session("email").ToString, razonsocial)
                        objMM.From = New MailAddress(email_from, "Departamento Nóminas")
                        objMM.IsBodyHtml = False
                        objMM.Priority = MailPriority.Normal
                        objMM.Subject = "Recibo de Nómina"
                        objMM.Body = mensaje
                        '
                        '   Agrega anexos
                        '
                        Dim AttachXML As Net.Mail.Attachment
                        Dim AttachPDF As Net.Mail.Attachment
                        AttachXML = New Net.Mail.Attachment(FilePathXML)
                        AttachPDF = New Net.Mail.Attachment(FilePathPDF)
                        objMM.Attachments.Add(AttachXML)
                        objMM.Attachments.Add(AttachPDF)
                        '
                        Dim SmtpUser As New Net.NetworkCredential
                        SmtpUser.UserName = email_smtp_username
                        SmtpUser.Password = email_smtp_password
                        'SmtpUser.Domain = email_smtp_server
                        SmtpMail.EnableSsl = True
                        SmtpMail.Port = email_smtp_port
                        SmtpMail.UseDefaultCredentials = False
                        SmtpMail.Credentials = SmtpUser
                        SmtpMail.Host = email_smtp_server
                        SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network
                        SmtpMail.Send(objMM)
                        '
                        '   Lo marca como enviado a nivel empleado
                        '
                        Call GrabarEnviado(row("NoEmpleado"), "S")
                        '
                        rwAlerta.RadAlert("Correo enviado.", 330, 180, "Alerta", "", "")
                            '
                        Catch ex As Exception
                            Call GrabarEnviado(row("NoEmpleado"), "N")
                        Finally
                            SmtpMail = Nothing
                        End Try
                        objMM = Nothing
                    Else
                        Call GrabarEnviado(row("NoEmpleado"), "N")
                    End If
                Next
            End If
        End If
    End Sub
    Public Shared Function validarEmail(ByVal email As String) As Boolean
        Dim expresion As String = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        If Regex.IsMatch(email, expresion) Then
            If Regex.Replace(email, expresion, String.Empty).Length = 0 Then
                Return True
            Else
                Return False
            End If

        Else
            Return False
        End If

    End Function
    Private Sub GrabarEnviado(ByVal NoEmpleado As Integer, ByVal Enviado As String)
        Call CargarVariablesGenerales()

        Dim cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.Enviado = Enviado
        cNomina.ActualizarEstatusEnviadoNomina()
    End Sub
    Private Function MyRound(Importe As Decimal) As Decimal
        Dim r As Decimal = Math.Ceiling(Importe * 100D) / 100D
        Return r
    End Function
    Private Sub btnGeneraTxtDispersion_Click(sender As Object, e As EventArgs) Handles btnGeneraTxtDispersion.Click
        If cmbPeriodo.SelectedValue > 0 Then

            Call CargarVariablesGenerales()

            Dim folio_banorte As Integer = 0
            Dim folio_banamex As Integer = 0
            Dim ruta_banorte As String = ""
            Dim ruta_banamex As String = ""

            Dim FilePathBanorte As String = ""
            Dim FilePathBanamex As String = ""

            Dim dt As New DataTable
            Dim cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = cmbCliente.SelectedValue
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = periodoID.Value
            cNomina.EsEspecial = True
            cNomina.IdNomina = nominaID.Value
            dt = cNomina.ConsultarDetalleNominaExtraordinaria()

            Dim texto As String = ""

            If dt.Rows.Count > 0 Then

                Dim total_banorte As Decimal = 0
                Dim total_banamex As Decimal = 0
                Dim total_registros_banorte As Decimal = 0
                Dim total_registros_banamex As Decimal = 0

                For Each row In dt.Rows
                    If CStr(row("BancoId")) = "072" Then
                        total_registros_banorte = total_registros_banorte + 1
                        total_banorte = total_banorte + Math.Round(Convert.ToDecimal(row("Neto")), 2)
                    ElseIf CStr(row("BancoId")) = "002" Then
                        total_registros_banamex = total_registros_banamex + 1
                        total_banamex = total_banamex + Math.Round(Convert.ToDecimal(row("Neto")), 2)
                    End If
                Next

                If total_banorte > 0 Then

                    Dim cDispersionNomina As New DispersionNomina()
                    cDispersionNomina.clave_servicio = "NE"
                    cDispersionNomina.total_registros_enviados = total_registros_banorte
                    cDispersionNomina.importe_total_registros_enviados = total_banorte
                    cDispersionNomina.accion = 0
                    cDispersionNomina.periodoid = cmbPeriodo.SelectedValue
                    'cDispersionNomina.empresaid = IdEmpresa
                    cDispersionNomina.ejercicio = IdEjercicio
                    cDispersionNomina.tipo_nomina = cmbPeriodicidad.SelectedValue
                    cDispersionNomina.tipo = "N"
                    cDispersionNomina.userid = Session("usuarioid")
                    cDispersionNomina.bancoid = "072" 'Banorte
                    folio_banorte = cDispersionNomina.AgregaFolio()
                    cDispersionNomina = Nothing

                    'ruta_banorte = Server.MapPath("~/TXT/BANORTE/S/").ToString & IdEmpresa.ToString & "\" & IdEjercicio.ToString & "\" & Periodo.ToString & "\"
                    ruta_banorte = Server.MapPath("~/TXT/BANORTE/S/").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString & "\"

                    If Not Directory.Exists(ruta_banorte) Then
                        Directory.CreateDirectory(ruta_banorte)
                    End If

                End If

                If total_banamex > 0 Then

                    Dim cDispersionNomina As New DispersionNomina()
                    cDispersionNomina.clave_servicio = ""
                    cDispersionNomina.total_registros_enviados = total_registros_banamex
                    cDispersionNomina.importe_total_registros_enviados = total_banamex
                    cDispersionNomina.accion = 0
                    cDispersionNomina.periodoid = cmbPeriodo.SelectedValue
                    'cDispersionNomina.empresaid = IdEmpresa
                    cDispersionNomina.ejercicio = IdEjercicio
                    cDispersionNomina.tipo_nomina = cmbPeriodicidad.SelectedValue
                    cDispersionNomina.tipo = "N"
                    cDispersionNomina.userid = Session("usuarioid")
                    cDispersionNomina.bancoid = "002" 'Banamex
                    folio_banamex = cDispersionNomina.AgregaFolio()
                    cDispersionNomina = Nothing

                    'ruta_banamex = Server.MapPath("~/TXT/BANAMEX/S/").ToString & IdEmpresa.ToString & "\" & IdEjercicio.ToString & "\" & Periodo.ToString & "\"
                    ruta_banamex = Server.MapPath("~/TXT/BANAMEX/S/").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString & "\"

                    If Not Directory.Exists(ruta_banamex) Then
                        Directory.CreateDirectory(ruta_banamex)
                    End If

                End If
                '
                '   Dispersión Nómina Banorte
                '
                If folio_banorte > 0 Then

                    Dim datos As New DataTable

                    If total_banorte > 0 Then
                        Dim cDispersionNomina As New DispersionNomina()
                        cDispersionNomina.id = folio_banorte
                        datos = cDispersionNomina.ConsultarDispersionNomina()

                        If datos.Rows.Count > 0 Then

                            Dim archivo As String = ""

                            Dim stotal As String = ""
                            stotal = String.Format("{0:F2}", Math.Round(Convert.ToDecimal(total_banorte), 2)).ToString.Replace(".", "")
                            stotal = stotal.PadLeft(15, "0")

                            For Each registro In datos.Rows
                                ' 
                                '   ENCABEZADO
                                ' 
                                texto = String.Empty
                                texto = texto & "H"                                                             'Tipo de Registro                       Fijo = 'H' Indica que es Header
                                texto = texto & "NE"                                                            'Clave de Servicio                      NE = Nómina Empresarial
                                texto = texto & CInt(registro("emisora")).ToString                              'Emisora                                Número de Emisora que se le asignó a la empresa
                                texto = texto & CDate(registro("fecha_proceso")).ToString("yyyyMMdd").ToString  'Fecha de Proceso                       Nómina: Fecha de Aplicación. Formato AAAAMMDD   Cuando campo acción es 1 (Adelanto de Pago) se toma como fecha adelanto inicio la fecha de proceso.
                                texto = texto & String.Format("{0:00}", registro("consecutivo"))                'Consecutivo                            Consecutivo de archivos generados en el día  > 0 y < 99.
                                texto = texto & String.Format("{0:000000}", total_registros_banorte)            'Número total de registros Enviados     Nómina: Número total de pagos Enviados
                                texto = texto & stotal                                                          'Importe Total de Registros Enviados    Importe Total de Registros Enviados (13 Enteros y 2 Decimales) Nómina: Importe total de pagos Enviados
                                texto = texto & "000000"                                                        'Número total de ALTAS Enviados         Número total de ALTAS Enviados Nómina: Informar ceros
                                texto = texto & "000000000000000"                                               'Importe Total de ALTAS Enviados        Importe Total de ALTAS Enviados (13 Enteros y 2 Decimales) Nómina: Importe total de pagos Enviados
                                texto = texto & "000000"                                                        'Número total de BAJAS Enviados         Número total de BAJAS Enviados Nómina: Informar ceros
                                texto = texto & "000000000000000"                                               'Importe Total de BAJAS Enviados        Importe Total de BAJAS Enviados (13 Enteros y 2 Decimales) Nómina: Importe total de pagos Enviados
                                texto = texto & "000000"                                                        'Número total de Cuentas a Verificar    Número total de Cuentas a Verificar Nómina:     Informar ceros
                                texto = texto & registro("accion")                                              'Acción                                 Para clave de servicio NE (Nómina) los valores permitidos son: Se informa el codigo de accion (0=Pago Nomina, 1 = Adelanto de Nómina, 2 = Pago honorarios, 3 = Pago Aguinaldo, 4 = Pago prima vacacional, 5 = Pago utilidades, 6 = Pago fondo de ahorro, 7 = Gratificación, 8 = Otros
                                texto = texto & "0000"                                                          'CENTRO                                 Centro Contable
                                texto = texto & "00000000"                                                      'Fecha Adelanto Fin                     Para clave de servicio NE (Nómina empresarial). Cuando la descripción de abono es 1 (Adelanto de Pago) se debera informar la fecha adelanto fin con Formato AAAAMMDD. El periodo maximo permitido entre fecha adelanto inicio y fecha adelanto fin podra ser hasta 2.5 meses.
                                texto = texto & registro("cuenta_bancaria")                                     'Cuenta Cargo                           Para clave de servicio NE (Nómina empresarial). Numero de cuenta que utilzara la emisora para realizar su dispersion de nómina
                                texto = texto & "                                                       " & vbCrLf                  'Filler(59 espacios vacios)
                                '
                                '   DETALLE
                                ' 
                                For Each row In dt.Rows
                                    If CStr(row("BancoId")) = "072" Then

                                        Dim simporte As String = ""
                                        simporte = String.Format("{0:F2}", Math.Round(Convert.ToDecimal(row("Neto")), 2)).ToString.Replace(".", "")
                                        simporte = simporte.PadLeft(15, "0")

                                        texto = texto & "D"                                                                 'Tipo de Registro                       Fijo = 'D' Indica que es Detalle
                                        texto = texto & CDate(registro("fecha_proceso")).ToString("yyyyMMdd").ToString      'Fecha de Aplicación                    Nómina: Fecha de Aplicación. Formato AAAAMMDD   Cuando campo acción es 1 (Adelanto de Pago) se toma como fecha adelanto inicio la fecha de proceso.
                                        texto = texto & String.Format("{0:0000000000}", row("NoEmpleado"))                  'Número de Empleado                     Nómina: El que maneja la empresa para identificar al empleado.
                                        texto = texto & "                                        "                          'Referencia del Servicio                Nómina:  informar 40 espacios
                                        texto = texto & "                                        "                          'Referencia del Ordenante               Nómina: Considerar el campo descripcion de pago
                                        texto = texto & simporte                                                            'Importe                                Importe de la Operación (13 Enteros y 2 Decimales)
                                        texto = texto & row("BancoId")                                                      'Número de Banco Receptor               Nómina y cobranza domiciliada: Banco receptor del Cargo o Abono.
                                        texto = texto & "01"                                                                'Tipo de Cuenta                         Para el servicio NE (Nómina) aplican las siguientes tipos de cuenta: 01-Cheques 03-Tarjeta de Débito 40-Clabe
                                        texto = texto & row("num_cuenta").PadLeft(18, "0")                                  'Número de Cuenta                       Cuenta a la que se aplicará el Cargo ó Abono
                                        texto = texto & "0"                                                                 'Tipo de Movimiento                     Nómina: Informar ceros
                                        texto = texto & "0"                                                                 'Acción                                 Nómina: Informar ceros
                                        texto = texto & "00000000"                                                          'Importe IVA de la Operación            Nómina: Informar ceros
                                        texto = texto & "                  " & vbCrLf                                       'Filler                                 18 espacios vacios
                                    End If
                                Next

                                archivo = "NI" & CInt(registro("emisora")).ToString & String.Format("{0:00}", registro("consecutivo")) & ".PAG"

                                If System.IO.File.Exists(ruta_banorte & archivo) Then
                                    System.IO.File.Delete(ruta_banorte & archivo)
                                    Dim fs As FileStream = File.Create(ruta_banorte & archivo)
                                    Dim info As Byte() = New UTF8Encoding(True).GetBytes(texto)
                                    fs.Write(info, 0, info.Length)
                                    fs.Close()
                                Else
                                    Dim fs As FileStream = File.Create(ruta_banorte & archivo)
                                    Dim info As Byte() = New UTF8Encoding(True).GetBytes(texto)
                                    fs.Write(info, 0, info.Length)
                                    fs.Close()
                                End If

                                FilePathBanorte = ruta_banorte & archivo

                            Next
                        End If
                    End If
                End If
                '
                '   Dispersión Nómina Banamex
                '
                If folio_banamex > 0 Then

                    Dim datos As New DataTable

                    If total_banamex > 0 Then
                        Dim cDispersionNomina As New DispersionNomina()
                        cDispersionNomina.id = folio_banamex
                        datos = cDispersionNomina.ConsultarDispersionNomina()

                        If datos.Rows.Count > 0 Then

                            Dim archivo As String = ""

                            Dim stotal As String = ""
                            stotal = String.Format("{0:F2}", Math.Round(Convert.ToDecimal(total_banamex), 2)).ToString.Replace(".", "")
                            stotal = stotal.PadLeft(18, "0")

                            For Each registro In datos.Rows
                                '
                                '   LAYOUT "C" FORMATOS PARA IMPORTACIÓN (TEF / TEF INTELAR) BANAMEX
                                '
                                '   ENCABEZADO
                                '
                                texto = String.Empty
                                texto = texto & "1"                                                                     'Tipo de registro                               Registro de Control, es constante y siempre es = 1
                                texto = texto & String.Format("{0:000000000000}", CInt(registro("emisora")))            'Número de identificación del cliente           Número de contrato de B.E. justificado a la derecha y completar con ceros a la izquierda
                                texto = texto & CDate(registro("fecha_proceso")).ToString("ddMMyy").ToString            'Fecha de pago                                  Fecha en que se realizará el cargo a su cuenta
                                texto = texto & String.Format("{0:0000}", registro("consecutivo"))                      'Consecutivo                                    Número consecutivo del archivo, pueden ser de 0001 hasta 0099, No pueden repetirse secuenciales para la misma fecha de pago
                                texto = texto & registro("razonsocial").ToString.PadRight(36, " ")                      'Nombre del la empresa                          Razón social de la empresa, justificado a la izquierda y completar con espacios en blanco
                                texto = texto & registro("descripcion").ToString.PadRight(20, " ")                      'Descripción del archivo                        Descripción del tipo de pago que esta realizando. Esta descripción se mostrará en su estado de cuenta de Banca Electrónica e impreso. Justificado a la izquierda y completar con espacios en blanco
                                texto = texto & "05"                                                                    'Naturaleza del archivo                         05 = Pago de Nómina (Pagomático) a cuentas Banamex
                                texto = texto & "                                        "                              'Instrucciones para órdenes de pago             Campo opcional, sólo se utiliza para órdenes de pago, justificado a la izquierda y completar con blancos
                                texto = texto & "C"                                                                     'Versión del layout                             Es constante y siempre es = C (versión del layout)
                                texto = texto & "0"                                                                     'Volúmen                                        Constante y siempre es = 0 (cero)
                                texto = texto & "1" & vbCrLf                                                            'Características del archivo                    0 = Si es modificable (permite edición en el software) 1 = Si es de sólo lectura (no permite edición en el software)
                                '
                                '   REGISTRO GLOBAL
                                '
                                texto = texto & "2"                                                                     'Tipo de registro                               Es constante y siempre es = 2 (Registro global)
                                texto = texto & "1"                                                                     'Tipo de operación                              0 = Si es Abono (solo aplica para cobros) 1 = Si es Cargo
                                texto = texto & "001"                                                                   'Clave de la moneda                             Es constante y siempre es = 001 (Pesos M.N.)
                                texto = texto & stotal                                                                  'Importe a abonar o cargar                      Justificado a la derecha, no lleva punto decimal.
                                texto = texto & "01"                                                                    'Tipo de cuenta                                 01 = Si es Cheques
                                texto = texto & String.Format("{0:0000}", CInt(registro("sucursal")))                   'Número de sucursal                             Clave de sucursal de la cuenta de cargo o abono, justificar a la derecha y completar con ceros
                                texto = texto & String.Format("{0:00000000000000000000}", CInt(registro("cuenta_bancaria"))) '                                          Número de cuenta de cargo o abono, justificar a la derecha y completar con ceros
                                texto = texto & "                    " & vbCrLf                                         'Espacio en blanco
                                '
                                '   DETALLE
                                '
                                Dim referencia As Integer = 0
                                For Each row In dt.Rows
                                    If CStr(row("BancoId")) = "002" Then

                                        referencia = referencia + 1

                                        Dim simporte As String = ""
                                        simporte = String.Format("{0:F2}", Math.Round(Convert.ToDecimal(row("Neto")), 2)).ToString.Replace(".", "")
                                        simporte = simporte.PadLeft(18, "0")

                                        texto = texto & "3"                                                                     'Tipo de registro                       Es constante y siempre es = 3 (Registro de Movtos. Individuales)
                                        texto = texto & "0"                                                                     'Tipo de operación                      0 = Si es Abono 1 = Si es Cargo (solo aplica para cobros)
                                        texto = texto & "001"                                                                   'Clave de la moneda                     Es constante y siempre es = 001 (Pesos M.N.)
                                        texto = texto & simporte                                                                'Importe                                Importe a abonar o cargar, justificado a la derecha sin punto decimal.
                                        texto = texto & "03"                                                                    'Tipo de cuenta                         03=Plásticos
                                        texto = texto & String.Format("{0:00000000000000000000}", row("num_tarjeta"))           'Número de cuenta                       Número de cuenta de cargo o abono, justificar a la derecha y completar con ceros
                                        texto = texto & referencia.ToString.PadRight(40, " ")                                   'Referencia alfanumérica                Debe ser diferente de cero.
                                        texto = texto & row("nombre_completo").ToString.PadRight(55, " ")                       'Beneficiario                           Si es Persona Física deberá llenarse: Nombre(s), Apellido Paterno/Apellido Materno
                                        texto = texto & "".ToString.PadRight(40, " ")                                           'Instrucciones                          Este campo es la Referencia Alfanumérica que se reflejará en el estado de cuenta de Banca Electrónica e impreso de su beneficiario
                                        texto = texto & "".ToString.PadRight(24, " ")                                           'Descripción TEF                        Llenar con espacios en blanco.
                                        texto = texto & String.Format("{0:0000}", CInt(row("BancoId")))                         'Clave de Banco                         Clave de banco de la cuenta de abono, solo para pago interbancario
                                        'texto = texto & "".ToString.PadRight(7, " ")                                           'Referencia Numérica                    Llenar con espacios en blanco.
                                        texto = texto & "00" & vbCrLf                                                           'Plazo                                  Aplica únicamente para Pagos Interbancarios. Este campo indica la fecha en que se realizará el abono a la cuenta de sus beneficiarios con respecto al campo de Fecha Valor (encabezado del archivo)
                                    End If
                                Next
                                '
                                '   TOTALES
                                '
                                texto = texto & "4"                                                                     'Tipo de registro                               Es constante y siempre es = 4 (Registro de totales)
                                texto = texto & "001"                                                                   'Clave de la moneda                             Es constante y siempre es = 001 (Pesos M.N.)
                                texto = texto & String.Format("{0:000000}", referencia)                                 'Número de abonos                               Número total de abonos del archivo
                                texto = texto & stotal                                                                  'Importe total de abonos                        Sumatoria de los abonos

                                archivo = String.Format("{0:000000000000}", CInt(registro("emisora"))) & String.Format("{0:0000}", registro("consecutivo")) & ".dat"

                                If System.IO.File.Exists(ruta_banamex & archivo) Then
                                    System.IO.File.Delete(ruta_banamex & archivo)
                                    Dim fs As FileStream = File.Create(ruta_banamex & archivo)
                                    Dim info As Byte() = New UTF8Encoding(True).GetBytes(texto)
                                    fs.Write(info, 0, info.Length)
                                    fs.Close()
                                Else
                                    Dim fs As FileStream = File.Create(ruta_banamex & archivo)
                                    Dim info As Byte() = New UTF8Encoding(True).GetBytes(texto)
                                    fs.Write(info, 0, info.Length)
                                    fs.Close()
                                End If

                                FilePathBanamex = ruta_banamex & archivo

                            Next
                        End If
                    End If
                End If

                Using zip As ZipFile = New ZipFile()
                    Dim ruta As String = ""
                    'ruta = Server.MapPath("~\TXT\S\").ToString & IdEmpresa.ToString & "\" & IdEjercicio.ToString & "\" & Periodo.ToString & "\"
                    ruta = Server.MapPath("~\TXT\S\").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString & "\"

                    If Not Directory.Exists(ruta) Then
                        Directory.CreateDirectory(ruta)
                    End If

                    If folio_banorte > 0 Then
                        If File.Exists(FilePathBanorte) Then
                            zip.AddFile(FilePathBanorte, "Banorte")
                        End If
                    End If

                    If folio_banamex > 0 Then
                        If File.Exists(FilePathBanamex) Then
                            zip.AddFile(FilePathBanamex, "Banamex")
                        End If
                    End If

                    Dim cPeriodo As New Entities.Periodo()
                    cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
                    cPeriodo.ConsultarPeriodoID()

                    Dim FilePath As String = ruta & Format(CDate(cPeriodo.FechaInicialDate), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinalDate), "dd-MM-yyyy").ToString & ".rar"
                    zip.Save(FilePath)

                    If File.Exists(FilePath) Then
                        Dim FileName As String = Path.GetFileName(FilePath)
                        Response.Clear()
                        Response.ContentType = "application/octet-stream"
                        Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
                        Response.Flush()
                        Response.WriteFile(FilePath)
                        Response.End()
                    End If

                End Using

            End If
        End If
    End Sub
    Private Sub btnDescargarXMLS_Click(sender As Object, e As EventArgs) Handles btnDescargarXMLS.Click
        If cmbPeriodo.SelectedValue > 0 Then

            Call CargarVariablesGenerales()

            Dim dt As New DataTable
            Dim cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = periodoID.Value
            cNomina.Tipo = "N"
            dt = cNomina.ConsultarEmpleadosTimbrados()

            Dim cPeriodo As New Entities.Periodo()
            cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
            cPeriodo.ConsultarPeriodoID()

            If dt.Rows.Count > 0 Then
                Using zip As ZipFile = New ZipFile()

                    Dim rutaEmpresa As String = ""
                    For Each row In dt.Rows

                        rutaEmpresa = Server.MapPath("~\XmlTimbrados\E\").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString

                        Dim FilePathXML = rutaEmpresa & "\" & row("RFC").ToString & "_" & Format(CDate(cPeriodo.FechaInicialDate), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinalDate), "dd-MM-yyyy").ToString & "_" & row("UUID").ToString & ".xml"

                        If File.Exists(FilePathXML) Then
                            zip.AddFile(FilePathXML, "XML")
                        End If
                    Next

                    rutaEmpresa = Server.MapPath("~\XmlTimbrados\E\").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString
                    Dim FilePath As String = rutaEmpresa & "\XML_" & Format(CDate(cPeriodo.FechaInicialDate), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinalDate), "dd-MM-yyyy").ToString & ".rar"
                    zip.Save(FilePath)

                    If File.Exists(FilePath) Then
                        Dim FileName As String = Path.GetFileName(FilePath)
                        Response.Clear()
                        Response.ContentType = "application/octet-stream"
                        Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
                        Response.Flush()
                        Response.WriteFile(FilePath)
                        Response.End()
                    End If

                End Using
            End If
        Else
            rwAlerta.RadAlert("Seleccione un periodo.", 330, 180, "Alerta", "", "")
        End If
    End Sub
    Private Sub btnDescargarPDFS_Click(sender As Object, e As EventArgs) Handles btnDescargarPDFS.Click
        If cmbPeriodo.SelectedValue > 0 Then

            Call CargarVariablesGenerales()

            Dim dt As New DataTable
            Dim cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = periodoID.Value
            cNomina.Tipo = "N"
            dt = cNomina.ConsultarEmpleadosTimbrados()

            Dim cPeriodo As New Entities.Periodo()
            cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
            cPeriodo.ConsultarPeriodoID()

            If dt.Rows.Count > 0 Then

                Using zip As ZipFile = New ZipFile()

                    Dim rutaEmpresa As String = ""
                    For Each row In dt.Rows

                        rutaEmpresa = Server.MapPath("~\PDF\E\T\").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString

                        If Not Directory.Exists(rutaEmpresa) Then
                            Directory.CreateDirectory(rutaEmpresa)
                        End If

                        Dim FilePathPDF = rutaEmpresa & "\" & row("RFC").ToString & "_" & Format(CDate(cPeriodo.FechaInicialDate), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinalDate), "dd-MM-yyyy").ToString & "_" & row("UUID").ToString & ".pdf"

                        If Not File.Exists(FilePathPDF) Then
                            Call GuardaPDF(GeneraPDF(row("NoEmpleado"), row("UUID")), FilePathPDF)
                        End If

                        If File.Exists(FilePathPDF) Then
                            zip.AddFile(FilePathPDF, "PDF")
                        Else
                            FilePathPDF = rutaEmpresa & "\" & row("RFC").ToString & "_" & Format(CDate(cPeriodo.FechaInicialDate), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinalDate), "dd-MM-yyyy").ToString & "_" & row("UUID").ToString & ".pdf"
                            If File.Exists(FilePathPDF) Then
                                zip.AddFile(FilePathPDF, "PDF")
                            End If
                        End If
                    Next

                    rutaEmpresa = Server.MapPath("~\PDF\E\T\").ToString & IdEjercicio.ToString & "\" & cmbPeriodo.SelectedValue.ToString

                    Dim FilePath As String = rutaEmpresa & "\PDF_" & Format(CDate(cPeriodo.FechaInicialDate), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinalDate), "dd-MM-yyyy").ToString & ".rar"
                    zip.Save(FilePath)

                    If File.Exists(FilePath) Then
                        Dim FileName As String = Path.GetFileName(FilePath)
                        Response.Clear()
                        Response.ContentType = "application/octet-stream"
                        Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
                        Response.Flush()
                        Response.WriteFile(FilePath)
                        Response.End()
                    End If

                End Using
            End If
        Else
            rwAlerta.RadAlert("Seleccione un periodo.", 330, 180, "Alerta", "", "")
        End If
    End Sub
    Private Sub cmbPeriodicidad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPeriodicidad.SelectedIndexChanged
        CargaPeriodos(cmbPeriodicidad.SelectedValue, 0)
    End Sub
    Private Sub CargaPeriodos(ByVal IdTipoNomina As Integer, ByVal IdPeriodo As Integer)
        Call CargarVariablesGenerales()
        Dim cPeriodo As New Entities.Periodo
        cPeriodo.IdEmpresa = IdEmpresa
        cPeriodo.IdEjercicio = IdEjercicio
        cPeriodo.IdTipoNomina = IdTipoNomina
        cPeriodo.ExtraordinarioBit = True
        ObjData.CatalogoRad(cmbPeriodo, cPeriodo.ConsultarPeriodos(), True, False)
        cmbPeriodo.SelectedValue = IdPeriodo
    End Sub
    'Private Sub ValidaBtnCrearNomina()
    '    btnGeneraNomina.Enabled = False
    '    If cmbCliente.SelectedValue <> 0 And cmbPeriodicidad.SelectedValue <> 0 And cmbPeriodo.SelectedValue <> 0 Then
    '        btnGeneraNomina.Enabled = True
    '    End If
    'End Sub
    Private Sub cmbCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCliente.SelectedIndexChanged
        'ValidaBtnCrearNomina()
    End Sub
    Private Sub btnCrearPeriodoEspecial_Click(sender As Object, e As EventArgs) Handles btnCrearPeriodoEspecial.Click

        CargarVariablesGenerales()

        Dim cEmpresa As New Entities.Empresa
        cEmpresa.IdUsuario = Session("usuarioid")
        cEmpresa.ConsultarEjercicioID()

        Dim IdPeriodo As Integer = 0

        If cEmpresa.IdUsuario > 0 Then
            Dim cPeriodo As New Entities.Periodo
            cPeriodo.IdEmpresa = IdEmpresa
            cPeriodo.IdEjercicio = cEmpresa.IdEjercicio
            cPeriodo.IdTipoNomina = cmbPeriodicidad.SelectedValue
            cPeriodo.FechaInicialDate = fchInicioPeriodo.SelectedDate
            cPeriodo.FechaFinalDate = fchFinPeriodo.SelectedDate
            IdPeriodo = cPeriodo.GuadarPeriodoEspecial()
            cPeriodo = Nothing
            WinPeriodoSave.VisibleOnPageLoad = False
        End If
        cEmpresa = Nothing
        Call CargaPeriodos(cmbPeriodicidad.SelectedValue, IdPeriodo)
    End Sub
    Private Sub btnCancelarPeriodo_Click(sender As Object, e As EventArgs) Handles btnCancelarPeriodo.Click
        WinPeriodoSave.VisibleOnPageLoad = False
    End Sub
    Private Sub btnSalirImportar_Click(sender As Object, e As EventArgs) Handles btnSalirImportar.Click
        WinImportarMonto.VisibleOnPageLoad = False
    End Sub
    Private Sub txtObservaciones_TextChanged(sender As Object, e As EventArgs) Handles txtObservaciones.TextChanged
        If Session("Folio") IsNot Nothing AndAlso Not String.IsNullOrEmpty(Session("Folio").ToString()) Then
            Dim cNomina As New Entities.Nomina()
            cNomina.IdNomina = Session("Folio")
            cNomina.Observaciones = txtObservaciones.Text
            cNomina.ActualizarObservacionesNomina()
            cNomina = Nothing
        End If

    End Sub
    Private Sub btnAgregarNominaE_Click(sender As Object, e As EventArgs) Handles btnAgregarNominaE.Click
        Session("Folio") = Nothing
        Response.Redirect("GeneracionDeNominaExtraordinaria.aspx")
    End Sub

End Class