Imports Telerik.Web.UI
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
Public Class CalculoAguinaldo
    Inherits System.Web.UI.Page
    Dim ObjData As New DataControl()
    Private SalarioMinimoDiarioGeneral As Decimal = 0
    Private ImpuestoAguinaldo As Decimal = 0
    Private SubsidioAguinaldo As Decimal = 0
    Private ImpuestoSueldoMesOrdinario As Decimal = 0
    Private SubsidioSueldoMesOrdinario As Decimal = 0

    Private TotalPercepciones As Double
    Private TotalDeducciones As Double
    Private NetoAPagar As Double
    Private PercepcionesGravadas As Double
    Private PercepcionesExentas As Double

    Private NumeroDeDiasPagados As Double
    Private Unidad As String
    Public FolioXml As String
    Private data As Byte()
    Const URI_SAT = "http://www.sat.gob.mx/cfd/4"
    Private m_xmlDOM As New XmlDocument
    Private urlnomina As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request("id")) Then
                cmbPeriodicidad.SelectedValue = Request("id")
                Call ConsultarDetalleNominaAguinaldo()
                Call CargarGridEmpleadosAguinaldo()
            End If
        End If
            lblEjercicio.Text = ConsultarEjercicio().ToString
        RadProgressArea1.Localization.TotalFiles = "Total empleados"
        RadProgressArea1.Localization.UploadedFiles = "Calculados"
        RadProgressArea1.Localization.CurrentFileName = "Calculando: "
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
                'IdEmpresa = oDataRow("IdEmpresa")
                'IdEjercicio = oDataRow("IdEjercicio")
                'Periodo = oDataRow("IdPeriodo")
                SalarioMinimoDiarioGeneral = oDataRow("SalarioMinimoDiarioGeneral")
            Next
        End If
    End Sub
    Public Function ConsultarEjercicio() As Integer

        Dim Ejercicio As Integer = 0

        Dim dt As New DataTable()
        Dim cEjercicio = New Ejercicio()
        dt = cEjercicio.ConsultarEjercicio()
        cEjercicio = Nothing

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                Ejercicio = oDataRow("annio")
            Next
        End If

        Return Ejercicio

    End Function
    Public Sub ConsultarDetalleNominaAguinaldo()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = ConsultarEjercicio()
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        dt = cNomina.ConsultarDetalleNominaAguinaldo()

        If dt.Rows.Count > 0 Then

            panelDatos.Visible = True
            btnModificacionDeNomina.Enabled = True
            btnGeneraNomina.Enabled = False
            btnTimbrarNomina.Enabled = False

            Dim rows() As DataRow = dt.Select("Timbrado='S'")
            If (rows.Length > 0) Then
                btnBorrarNomina.Enabled = False
                If rows.Length < dt.Rows.Count Then
                    btnGenerarNominaElectronica.Enabled = True
                    btnTimbrarNomina.Enabled = True
                Else
                    btnGenerarNominaElectronica.Enabled = False
                    btnTimbrarNomina.Enabled = False
                End If
            Else
                btnBorrarNomina.Enabled = True
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
                btnEnvioComprobantes.Enabled = True
            End If

            If rows.Length = dt.Rows.Count Then
                btnGenerarNominaElectronica.Enabled = False
                btnTimbrarNomina.Enabled = False
            End If

            If rowPdf.Length = dt.Rows.Count Then
                btnGenerarPDF.Enabled = False
            End If

            If rows.Length = dt.Rows.Count Then
                btnTimbrarNomina.Enabled = False
            End If

        ElseIf dt.Rows.Count = 0 Then
            btnGeneraNomina.Enabled = True
            btnModificacionDeNomina.Enabled = False
            btnBorrarNomina.Enabled = False
            btnGenerarNominaElectronica.Enabled = False
            btnTimbrarNomina.Enabled = False
            btnGenerarPDF.Enabled = False
            btnEnvioComprobantes.Enabled = False
            btnGeneraTxtDispersion.Enabled = False
        End If
    End Sub
    Private Sub CargarGridEmpleadosAguinaldo()

        lnkReporte.Attributes.Add("onclick", "javascript: OpenWindow('" & Session("clienteid").ToString & "','" & ConsultarEjercicio().ToString & "','" & cmbPeriodicidad.SelectedValue.ToString & "'); return false;")

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = ConsultarEjercicio()
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        dt = cNomina.ConsultarDetalleNominaAguinaldo()
        grdEmpleadosAguinaldo.DataSource = dt
        grdEmpleadosAguinaldo.DataBind()
        cNomina = Nothing
    End Sub
    Private Sub btnGeneraNomina_Click(sender As Object, e As EventArgs) Handles btnGeneraNomina.Click
        If cmbPeriodicidad.SelectedValue = 0 Then
            rwAlerta.RadAlert("Selecciona un periodo de pago", 330, 180, "Alerta", "", "")
        Else
            rwConfirm.RadConfirm("¿Está seguro de generar el cálculo de aguinaldo?", "confirmCallbackFnGenerarNomina", 330, 180, Nothing, "Confirmar")
        End If
    End Sub
    Private Sub btnConfirmarGeneraNomina_Click(sender As Object, e As EventArgs) Handles btnConfirmarGeneraNomina.Click
        Dim dt As New DataTable
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = ConsultarEjercicio()
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        dt = cNomina.ConsultarEmpleadosActivos()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then

            Dim Total As Integer = dt.Rows.Count

            Dim progress As RadProgressContext = RadProgressContext.Current
            progress.Speed = "N/A"
            Dim i As Integer = 0
            For Each oDataRow In dt.Rows
                i += 1

                GuardarRegistro(oDataRow("IdEmpresa"), oDataRow("NoEmpleado"), oDataRow("TipoNomina"), oDataRow("IdContrato"), Math.Round(oDataRow("CuotaDiaria"), 6))

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

            Call ConsultarDetalleNominaAguinaldo()
            Call CargarGridEmpleadosAguinaldo()

        End If
    End Sub
    Private Sub GuardarRegistro(ByVal IdEmpresa, ByVal NoEmpleado, ByVal TipoNomina, ByVal IdContrato, ByVal CuotaDiaria)

        Call GuardarCalculoAguinaldo(IdEmpresa, NoEmpleado, TipoNomina, IdContrato, CuotaDiaria)

        Try
            Dim cNomina = New Nomina()
            cNomina.Ejercicio = ConsultarEjercicio()
            cNomina.TipoNomina = TipoNomina
            cNomina.Periodo = 0
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 87
            cNomina.IdContrato = IdContrato
            cNomina.TipoConcepto = "DE"
            cNomina.Unidad = 1
            cNomina.Importe = CuotaDiaria
            cNomina.Generado = ""
            cNomina.Timbrado = ""
            cNomina.Enviado = ""
            cNomina.Tipo = "A"
            cNomina.GuadarNomina()
            cNomina = Nothing
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try

    End Sub
    Private Sub GuardarCalculoAguinaldo(ByVal IdEmpresa, ByVal NoEmpleado, ByVal TipoNomina, ByVal IdContrato, ByVal ImporteDiario)
        Try
            Dim Aguinaldo As Decimal = 0

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            cNomina.NoEmpleado = NoEmpleado
            cNomina.TipoNomina = TipoNomina
            cNomina.Ejercicio = ConsultarEjercicio()
            dt = cNomina.ConsultarDesgloseAguinaldo()
            cNomina = Nothing

            If dt.Rows.Count > 0 Then
                For Each oDataRow In dt.Rows
                    Aguinaldo = oDataRow("ProporcionalAguinaldo")
                Next
            End If

            Call CargarVariablesGenerales()

            Dim ImporteExentoAguinaldo As Decimal = 0
            Dim ImporteGravadoAguinaldo As Decimal = 0

            If Aguinaldo > 0 Then
                If Aguinaldo > 0 And Aguinaldo < (SalarioMinimoDiarioGeneral * 30) Then
                    ImporteExentoAguinaldo = Aguinaldo
                    ImporteGravadoAguinaldo = 0
                ElseIf Aguinaldo > 0 And Aguinaldo > (SalarioMinimoDiarioGeneral * 30) Then
                    ImporteExentoAguinaldo = SalarioMinimoDiarioGeneral * 30
                    ImporteGravadoAguinaldo = Aguinaldo - (SalarioMinimoDiarioGeneral * 30)
                End If

                cNomina = New Nomina()
                cNomina.Ejercicio = ConsultarEjercicio()
                cNomina.TipoNomina = TipoNomina
                cNomina.Periodo = 0
                cNomina.NoEmpleado = NoEmpleado
                cNomina.IdContrato = IdContrato
                cNomina.CvoConcepto = 14
                cNomina.Tipo = "A"
                cNomina.TipoConcepto = "P"
                cNomina.Unidad = 1
                cNomina.Importe = Aguinaldo
                cNomina.ImporteGravado = ImporteGravadoAguinaldo
                cNomina.ImporteExento = ImporteExentoAguinaldo
                cNomina.Generado = ""
                cNomina.IdMovimiento = 0
                cNomina.GuardarExentoYGravadoFiniquito()

            End If

            If ImporteGravadoAguinaldo > 0 Then

                Dim IngresoMensualOrdinario As Decimal = 0
                Dim ImporteProporcionalAguinaldo As Decimal = 0
                Dim IngresoGravadoMes As Decimal = 0

                ImporteProporcionalAguinaldo = (ImporteGravadoAguinaldo / 365) * 30.4
                IngresoMensualOrdinario = (ImporteDiario * 30.4)
                IngresoGravadoMes = ImporteProporcionalAguinaldo + IngresoMensualOrdinario

                '1 Impuesto / Subsidio proporcional aguinaldo
                Call CalcularImpuestoAguinaldo(IngresoGravadoMes)
                Call CalcularSubsidioAguinaldo(IngresoGravadoMes)

                If ImpuestoAguinaldo > SubsidioAguinaldo Then
                    SubsidioAguinaldo = 0
                    ImpuestoAguinaldo = ImpuestoAguinaldo - SubsidioAguinaldo
                ElseIf ImpuestoAguinaldo < SubsidioAguinaldo Then
                    SubsidioAguinaldo = SubsidioAguinaldo - ImpuestoAguinaldo
                    ImpuestoAguinaldo = 0
                End If

                ImpuestoAguinaldo = Math.Round(ImpuestoAguinaldo, 6)
                SubsidioAguinaldo = Math.Round(SubsidioAguinaldo, 6)

                '2 Impuesto / Subsidio por sueldo ordinario
                Dim DiferenciaSubsidioImpuestoAguinaldo As Decimal = 0
                Dim FactorImpuestoSubsidioAguinaldo As Decimal = 0
                Dim TasaSubsidioImpuestoAguinaldo As Decimal = 0

                Call CalcularImpuestoSueldoMesOrdinario(IngresoMensualOrdinario)
                Call CalcularSubsidioSueldoMesOrdinario(IngresoMensualOrdinario)

                If ImpuestoSueldoMesOrdinario > SubsidioSueldoMesOrdinario Then
                    SubsidioSueldoMesOrdinario = 0
                    ImpuestoSueldoMesOrdinario = ImpuestoSueldoMesOrdinario - SubsidioSueldoMesOrdinario
                ElseIf ImpuestoSueldoMesOrdinario < SubsidioSueldoMesOrdinario Then
                    SubsidioSueldoMesOrdinario = SubsidioSueldoMesOrdinario - ImpuestoSueldoMesOrdinario
                    ImpuestoSueldoMesOrdinario = 0
                End If

                ImpuestoSueldoMesOrdinario = Math.Round(ImpuestoSueldoMesOrdinario, 6)
                SubsidioSueldoMesOrdinario = Math.Round(SubsidioSueldoMesOrdinario, 6)

                If SubsidioAguinaldo > 0 Then
                    DiferenciaSubsidioImpuestoAguinaldo = SubsidioAguinaldo - SubsidioSueldoMesOrdinario
                    If DiferenciaSubsidioImpuestoAguinaldo < 0 Then
                        DiferenciaSubsidioImpuestoAguinaldo = DiferenciaSubsidioImpuestoAguinaldo * -1
                    End If

                    FactorImpuestoSubsidioAguinaldo = DiferenciaSubsidioImpuestoAguinaldo / ImporteProporcionalAguinaldo
                    TasaSubsidioImpuestoAguinaldo = FactorImpuestoSubsidioAguinaldo * 1 ' El 1 representa 100%
                    SubsidioAguinaldo = ImporteGravadoAguinaldo * TasaSubsidioImpuestoAguinaldo
                Else
                    DiferenciaSubsidioImpuestoAguinaldo = ImpuestoAguinaldo - ImpuestoSueldoMesOrdinario

                    FactorImpuestoSubsidioAguinaldo = DiferenciaSubsidioImpuestoAguinaldo / ImporteProporcionalAguinaldo
                    TasaSubsidioImpuestoAguinaldo = FactorImpuestoSubsidioAguinaldo * 1 ' El 1 representa 100%
                    ImpuestoAguinaldo = ImporteGravadoAguinaldo * TasaSubsidioImpuestoAguinaldo
                End If

                ImpuestoAguinaldo = Math.Round(ImpuestoAguinaldo, 6)
                SubsidioAguinaldo = Math.Round(SubsidioAguinaldo, 6)

                If ImpuestoAguinaldo > 0 Then
                    cNomina = New Nomina()
                    cNomina.Ejercicio = ConsultarEjercicio()
                    cNomina.TipoNomina = TipoNomina
                    cNomina.Periodo = 0
                    cNomina.NoEmpleado = NoEmpleado
                    cNomina.CvoConcepto = 86
                    cNomina.IdContrato = IdContrato
                    cNomina.Tipo = "A"
                    cNomina.TipoConcepto = "D"
                    cNomina.Unidad = 1
                    cNomina.Importe = ImpuestoAguinaldo
                    cNomina.ImporteGravado = 0
                    cNomina.ImporteExento = ImpuestoAguinaldo
                    cNomina.Generado = ""
                    cNomina.Timbrado = ""
                    cNomina.Enviado = ""
                    cNomina.GuadarNomina()
                End If
                If SubsidioAguinaldo > 0 Then
                    cNomina = New Nomina()
                    cNomina.Ejercicio = ConsultarEjercicio()
                    cNomina.TipoNomina = TipoNomina
                    cNomina.Periodo = 0
                    cNomina.NoEmpleado = NoEmpleado
                    cNomina.CvoConcepto = 55
                    cNomina.IdContrato = IdContrato
                    cNomina.Tipo = "A"
                    cNomina.TipoConcepto = "P"
                    cNomina.Unidad = 1
                    cNomina.Importe = SubsidioAguinaldo
                    cNomina.ImporteGravado = 0
                    cNomina.ImporteExento = SubsidioAguinaldo
                    cNomina.Generado = ""
                    cNomina.Timbrado = ""
                    cNomina.Enviado = ""
                    cNomina.GuadarNomina()
                End If

            End If

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
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
    Private Sub CalcularImpuestoSueldoMesOrdinario(ByVal Importe As Decimal)
        Try
            ImpuestoSueldoMesOrdinario = 0
            Dim dt As New DataTable()
            Dim TarifaMensual As New TarifaMensual()
            TarifaMensual.CuotaFija = Importe
            dt = TarifaMensual.ConsultarTarifaMensual()

            If dt.Rows.Count > 0 Then
                ImpuestoSueldoMesOrdinario = ((Importe - dt.Rows(0).Item("LimiteInferior")) * (dt.Rows(0).Item("PorcSobreExcli") / 100)) + dt.Rows(0).Item("CuotaFija")
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub CalcularSubsidioSueldoMesOrdinario(ByVal Importe As Decimal)
        Try
            SubsidioSueldoMesOrdinario = 0
            Dim dt As New DataTable()
            Dim TablaSubsidioDiario As New TablaSubsidioDiario
            TablaSubsidioDiario.Importe = Importe
            dt = TablaSubsidioDiario.ConsultarSubsidioMensual()

            If dt.Rows.Count > 0 Then
                SubsidioSueldoMesOrdinario = dt.Rows(0).Item("Subsidio")
            End If

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub btnBorrarNomina_Click(sender As Object, e As EventArgs) Handles btnBorrarNomina.Click
        If cmbPeriodicidad.SelectedValue = 0 Then
            rwAlerta.RadAlert("Selecciona un periodo de pago", 330, 180, "Alerta", "", "")
        Else
            rwConfirm.RadConfirm("¿Está seguro que desea borrar esta nómina especial de Aguinaldo?", "confirmCallbackFnBorrarNomina", 330, 180, Nothing, "Confirmar")
        End If
    End Sub
    Private Sub btnConfirmarBorrarNomina_Click(sender As Object, e As EventArgs) Handles btnConfirmarBorrarNomina.Click
        Call EliminarNomina()
        Call ConsultarDetalleNominaAguinaldo()
        Call CargarGridEmpleadosAguinaldo()
    End Sub
    Private Sub EliminarNomina()
        Try
            Dim cNomina As New Nomina()
            cNomina.Ejercicio = ConsultarEjercicio()
            cNomina.Tipo = "A"
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.EliminaNominaAguinaldo()

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub btnGenerarNominaElectronica_Click(sender As Object, e As EventArgs) Handles btnGenerarNominaElectronica.Click
        If cmbPeriodicidad.SelectedValue = 0 Then
            rwAlerta.RadAlert("Selecciona un periodo de pago", 330, 180, "Alerta", "", "")
        Else
            rwConfirm.RadConfirm("¿Está seguro de generar el cálculo de nómina especial de Aguinaldo?", "confirmCallbackFnGeneraNominaElectronica", 330, 180, Nothing, "Confirmar")
        End If
    End Sub
    Private Sub btnConfirmarGeneraNominaElectronica_Click(sender As Object, e As EventArgs) Handles btnConfirmarGeneraNominaElectronica.Click

        Dim RfcEmisor As String = ""

        Dim dt As New DataTable
        Dim cNomina = New Nomina()
        cNomina.Ejercicio = ConsultarEjercicio()
        cNomina.Periodo = 0
        cNomina.Tipo = "A"
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        dt = cNomina.ConsultarEmpleadosNoGeneradosNominaEspecial()

        Dim dtEmisor As New DataTable
        cNomina = New Nomina()
        cNomina.Id = Session("clienteid")
        dtEmisor = cNomina.ConsultarDatosEmisor()

        If dtEmisor.Rows.Count > 0 Then
            For Each oDataRow In dtEmisor.Rows
                RfcEmisor = oDataRow("RFC")
            Next
        End If

        Dim ruta As String = ""
        ruta = Server.MapPath("~/XmlGenerados/" & RfcEmisor & "/A/").ToString & ConsultarEjercicio().ToString

        If Not Directory.Exists(ruta) Then
            Directory.CreateDirectory(ruta)
        End If

        If dt.Rows.Count > 0 Then

            Dim Total As Integer = dt.Rows.Count
            Dim progress As RadProgressContext = RadProgressContext.Current
            progress.Speed = "N/A"
            Dim i As Integer = 0
            For Each oDataRow In dt.Rows
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

                CrearCFDNominaEspecial(oDataRow("NoEmpleado"), ruta)

                GrabarGeneracionXml(oDataRow("NoEmpleado"), oDataRow("Ejercicio"), oDataRow("TipoNomina"))

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
                System.Threading.Thread.Sleep(50)

            Next

            Call ConsultarDetalleNominaAguinaldo()
            Call CargarGridEmpleadosAguinaldo()

        ElseIf dt.Rows.Count = 0 Then
            rwAlerta.RadAlert("Esta nómina ya esta timbrada completamente!!!", 330, 180, "Alerta", "", "")
        End If
    End Sub
    Private Sub CargarPercepciones(ByVal NoEmpleado As Integer)
        Dim dt As New DataTable
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = ConsultarEjercicio().ToString
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = 0
        cNomina.TipoConcepto = "P"
        cNomina.Tipo = "A"
        cNomina.NoEmpleado = NoEmpleado
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
        Dim dt As New DataTable
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = ConsultarEjercicio().ToString
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = 0
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
    Public Sub CrearCFDNominaEspecial(ByVal NoEmpleado As Integer, ByVal RutaXML As String)
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
            FolioXml = RutaXML & "/" & serie.Value.ToString & folio.Value.ToString & ".xml"
            SellarCFD(Comprobante, path)
            m_xmlDOM.InnerXml = (Replace(m_xmlDOM.InnerXml, "schemaLocation", "xsi:schemaLocation", , , CompareMethod.Text))
            m_xmlDOM.Save(FolioXml)
        End If

    End Sub
    Private Function CertificadoCliente() As String
        Dim Certificado As String = ""
        Dim ObjData As New DataControl(0)
        Certificado = ObjData.RunSQLScalarQueryString("select top 1 isnull(certificado,'') as certificado from tblCliente where id='" & Session("clienteid").ToString & "'")
        Dim elements() As String = Certificado.Split(New Char() {"."c}, StringSplitOptions.RemoveEmptyEntries)
        ObjData = Nothing

        Return elements(0)

    End Function
    Private Function ContrasenaPfx() As String
        Dim contrasena_llave_privada As String = ""
        Dim ObjData As New DataControl(0)
        contrasena_llave_privada = ObjData.RunSQLScalarQueryString("select top 1 isnull(contrasena,'') as contrasena from tblCliente where id='" & Session("clienteid").ToString & "'")
        ObjData = Nothing

        Return contrasena_llave_privada

    End Function
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
        Dim LugarExpedicion As String = ""
        Dim dt As New DataTable
        Dim cNomina = New Nomina()
        cNomina.Id = Session("clienteid")
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
        cNomina.Ejercicio = ConsultarEjercicio()
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = 0
        cNomina.TipoConcepto = "D"
        cNomina.Tipo = "A"
        cNomina.NoEmpleado = NoEmpleado
        dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
        cNomina = Nothing

        TotalDeducciones = 0

        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                If Convert.ToDecimal(dt.Rows.Item(i)("Importe")) > 0 Then
                    TotalDeducciones += Convert.ToDecimal(Math.Round(dt.Rows.Item(i)("Importe"), 2))
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
    Private Sub IndentarNodo(ByVal Nodo As XmlNode, ByVal Nivel As Long)
        Nodo.AppendChild(m_xmlDOM.CreateTextNode(vbNewLine & New String(ControlChars.Tab, Nivel)))
    End Sub
    Private Sub CrearNodoEmisor(ByVal Nodo As XmlNode)
        Dim dtEmisor As New DataTable
        Dim cNomina = New Nomina()
        cNomina.Id = Session("clienteid")
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
        cNomina.Ejercicio = ConsultarEjercicio()
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = 0
        cNomina.Tipo = "A"
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
    Private Sub AsignaSerieFolio(ByVal NoEmpleado As Integer)
        '
        '   Obtiene serie y folio
        '
        Dim dt As New DataTable()
        Dim cNomina = New Nomina()
        cNomina.Ejercicio = ConsultarEjercicio()
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = 0
        cNomina.Tipo = "A"
        cNomina.NoEmpleado = NoEmpleado
        dt = cNomina.AsignaSerieFolio()

        If dt.Rows.Count > 0 Then
            serie.Value = dt.Rows.Item(0)("serie").ToString()
            folio.Value = dt.Rows.Item(0)("folio")
        End If

    End Sub
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
    Private Sub CrearNodoImpuestos(ByVal Nodo As XmlNode)
        Dim Impuestos As XmlElement
        Impuestos = CrearNodo("cfdi:Impuestos")
        Nodo.AppendChild(Impuestos)
    End Sub
    Private Sub CrearNodoComplemento(ByVal Nodo As XmlNode, ByVal NoEmpleado As Integer)

        Call CargarVariablesGenerales()

        Dim dtEmpleado As New DataTable
        Dim cNomina As New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = ConsultarEjercicio()
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = 0
        cNomina.Tipo = "A"
        dtEmpleado = cNomina.ConsultarDatosEmpleadosGenerados()

        Dim Complemento As XmlElement
        Dim Nomina As XmlElement
        Dim Percepciones As XmlElement
        Dim Percepcion As XmlElement
        Dim Deducciones As XmlElement
        Dim Deduccion As XmlElement
        Dim Emisor As XmlElement
        Dim Receptor As XmlElement

        Dim TotalSueldos As Decimal = 0
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
        Dim SubSidioEmpleo As Decimal = 0

        If dtEmpleado.Rows.Count > 0 Then
            For Each oDataRow In dtEmpleado.Rows

                Dim dt As New DataTable
                cNomina = New Nomina()
                cNomina.Ejercicio = ConsultarEjercicio()
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = 0
                cNomina.TipoConcepto = "P"
                cNomina.Tipo = "A"
                cNomina.NoEmpleado = NoEmpleado
                dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                cNomina = Nothing

                If dt.Rows.Count > 0 Then
                    '- Otros pagos
                    '16 - Otros
                    '17 - Subsidio para el empleo
                    '- Horas extra
                    '19 - Horas extra
                    '- Indemnizaciones
                    '22 - Prima por antigüedad
                    '23 - Pagos por separación
                    '25 - Indemnizaciones
                    '- Jubilaciones
                    '39 - Jubilaciones, pensiones o haberes de retiro
                    '44 - Jubilaciones, pensiones o haberes de retiro en parcialidades
                    If dt.Compute("SUM(Importe)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'") IsNot DBNull.Value Then
                        TotalSueldos = Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'"))
                    End If
                    If dt.Compute("SUM(Importe)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'") IsNot DBNull.Value Then
                        TotalPercepciones = Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'"))
                    End If
                    If dt.Compute("SUM(ImporteGravado)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'") IsNot DBNull.Value Then
                        TotalGravado = Convert.ToDecimal(dt.Compute("SUM(ImporteGravado)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'"))
                    End If
                    If dt.Compute("SUM(ImporteExento)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'") IsNot DBNull.Value Then
                        TotalExento = Convert.ToDecimal(dt.Compute("SUM(ImporteExento)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'"))
                    End If
                    If dt.Compute("SUM(Importe)", "CvoSAT='19'") IsNot DBNull.Value Then
                        TotalHorasExtra = Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT='19'"))
                    End If
                    If dt.Compute("SUM(ImporteExento)", "CvoSAT='19'") IsNot DBNull.Value Then
                        ImporteExentoHorasExtra = Convert.ToDecimal(dt.Compute("SUM(ImporteExento)", "CvoSAT='19'"))
                    End If
                    If dt.Compute("SUM(ImporteGravado)", "CvoSAT='19'") IsNot DBNull.Value Then
                        ImporteGravadoHorasExtra = Convert.ToDecimal(dt.Compute("SUM(ImporteGravado)", "CvoSAT='19'"))
                    End If
                    If dt.Compute("SUM(Importe)", "CvoSAT='16' or CvoSAT='17' or CvoSAT='18'") IsNot DBNull.Value Then
                        TotalOtrosPagos = Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT='16' or CvoSAT='17' or CvoSAT='18'"))
                    End If
                End If

                dt = New DataTable
                cNomina = New Nomina()
                cNomina.Ejercicio = ConsultarEjercicio()
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = 0
                cNomina.TipoConcepto = "D"
                cNomina.Tipo = "A"
                cNomina.NoEmpleado = NoEmpleado
                dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                cNomina = Nothing

                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        If Convert.ToDecimal(dt.Rows.Item(i)("Importe")) > 0 Then
                            TotalDeducciones += Convert.ToDecimal(Math.Round(dt.Rows.Item(i)("Importe"), 2))
                            If dt.Rows.Item(i)("CvoSAT").ToString = "2" Then
                                TotalImpuestosRetenidos += Convert.ToDecimal(dt.Rows.Item(i)("Importe").ToString)
                            End If
                        End If
                    Next
                End If

                Complemento = CrearNodo("cfdi:Complemento")

                urlnomina = 1
                Nomina = CrearNodo("nomina12:Nomina")

                Dim DiasLaboradosAnio As Decimal = 0

                Dim datos As New DataTable()
                cNomina = New Nomina()
                cNomina.NoEmpleado = NoEmpleado
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Ejercicio = ConsultarEjercicio()
                datos = cNomina.ConsultarDesgloseAguinaldo()
                cNomina = Nothing

                If datos.Rows.Count > 0 Then
                    For Each row In datos.Rows
                        DiasLaboradosAnio = row("DiasLaboradosAnio")
                    Next
                End If

                NumeroDeDiasPagados = (DiasLaboradosAnio * 15) / 365

                Dim FechaPago As Date = CDate(Date.Now)
                Nomina.SetAttribute("Version", "1.2")
                Nomina.SetAttribute("TipoNomina", "E")
                Nomina.SetAttribute("FechaPago", Mid(FechaPago, 7, 4) + "-" + Mid(FechaPago, 4, 2) + "-" + Mid(FechaPago, 1, 2))
                Nomina.SetAttribute("FechaInicialPago", Format(CDate(Date.Now), "yyyy-MM-dd"))
                Nomina.SetAttribute("FechaFinalPago", Format(CDate(Date.Now), "yyyy-MM-dd"))
                Nomina.SetAttribute("NumDiasPagados", Format(CDbl(NumeroDeDiasPagados), "0.000"))
                Nomina.SetAttribute("TotalPercepciones", Math.Round(TotalPercepciones + TotalHorasExtra, 2))
                If Math.Round(TotalDeducciones, 2) > 0 Then
                    Nomina.SetAttribute("TotalDeducciones", Math.Round(TotalDeducciones, 2))
                End If
                If Math.Round(TotalOtrosPagos, 2) > 0 Then
                    Nomina.SetAttribute("TotalOtrosPagos", Math.Round(TotalOtrosPagos, 2))
                End If

                Emisor = CrearNodo("nomina12:Emisor")
                If oDataRow("RegistroPatronal").ToString <> "" Then
                    Emisor.SetAttribute("RegistroPatronal", oDataRow("RegistroPatronal"))
                End If
                'Emisor.SetAttribute("RfcPatronOrigen", oDataRow("RfcPatronOrigen"))

                Nomina.AppendChild(Emisor)

                Receptor = CrearNodo("nomina12:Receptor")
                If oDataRow("Curp").ToString <> "" Then
                    Receptor.SetAttribute("Curp", oDataRow("Curp"))
                End If
                If oDataRow("NumSeguridadSocial").ToString <> "" Then
                    Receptor.SetAttribute("NumSeguridadSocial", oDataRow("NumSeguridadSocial"))
                End If

                Receptor.SetAttribute("FechaInicioRelLaboral", Format(CDate(oDataRow("FechaInicioRelLaboral")), "yyyy-MM-dd"))

                Dim Antiguedad As String = ""

                cNomina = New Nomina()
                Antiguedad = cNomina.ConsultarAntiguedadNominaEspecial(Convert.ToDateTime(oDataRow("FechaInicioRelLaboral")).ToString("yyyy-MM-dd"))

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
                'If cmbPeriodicidad.SelectedValue = 1 Then
                '    Receptor.SetAttribute("PeriodicidadPago", "02") 'Semanal
                'ElseIf cmbPeriodicidad.SelectedValue = 2 Then
                '    Receptor.SetAttribute("PeriodicidadPago", "03") 'Catorcenal
                'ElseIf cmbPeriodicidad.SelectedValue = 3 Then
                '    Receptor.SetAttribute("PeriodicidadPago", "04") 'Quincenal
                'End If

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

                Receptor.SetAttribute("SalarioBaseCotApor", Math.Round(oDataRow("SalarioBase"), 2))
                Receptor.SetAttribute("SalarioDiarioIntegrado", Math.Round(oDataRow("SalarioDiarioIntegrado"), 2))
                Receptor.SetAttribute("ClaveEntFed", oDataRow("ClaveEstado"))

                'Dim SubContratacion As XmlElement
                'SubContratacion = CrearNodo("nomina12:SubContratacion")
                'SubContratacion.SetAttribute("RfcLabora", oDataRow("RfcLabora"))
                'SubContratacion.SetAttribute("PorcentajeTiempo", "100")
                'Receptor.AppendChild(SubContratacion)

                Nomina.AppendChild(Receptor)

                Percepciones = CrearNodo("nomina12:Percepciones")
                Percepciones.SetAttribute("TotalSueldos", Math.Round((TotalSueldos + TotalHorasExtra), 2))
                Percepciones.SetAttribute("TotalGravado", Math.Round((TotalGravado + ImporteGravadoHorasExtra), 2))
                Percepciones.SetAttribute("TotalExento", Math.Round((TotalExento + ImporteExentoHorasExtra), 2))

                Nomina.AppendChild(Percepciones)

                'Atributo condicional para expresar el importe exento y gravado de las claves tipo percepción 022 Prima por Antigüedad, 023 Pagos por separación y 025 Indemnizaciones.
                If TotalSeparacionIndemnizacion > 0 Then
                    Percepciones.SetAttribute("TotalSeparacionIndemnizacion", Convert.ToDecimal(TotalSeparacionIndemnizacion))
                End If

                dt = New DataTable
                cNomina = New Nomina()
                cNomina.Ejercicio = ConsultarEjercicio()
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = 0
                cNomina.TipoConcepto = "P"
                cNomina.Tipo = "A"
                cNomina.NoEmpleado = NoEmpleado
                dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                cNomina = Nothing
                '
                ' Percepciones
                '
                If dt.Rows.Count > 0 Then
                    For i = 0 To dt.Rows.Count - 1
                        If Convert.ToDecimal(dt.Rows.Item(i)("Importe").ToString) > 0 And dt.Rows.Item(i)("CvoSAT").ToString <> "16" And dt.Rows.Item(i)("CvoSAT").ToString <> "17" And dt.Rows.Item(i)("CvoSAT").ToString <> "19" And dt.Rows.Item(i)("CvoSAT").ToString <> "22" And dt.Rows.Item(i)("CvoSAT").ToString <> "23" And dt.Rows.Item(i)("CvoSAT").ToString <> "25" Then

                            Percepcion = CrearNodo("nomina12:Percepcion")
                            Percepcion.SetAttribute("TipoPercepcion", String.Format("{0:000}", CInt(dt.Rows.Item(i)("CvoSAT"))))
                            Percepcion.SetAttribute("Clave", String.Format("{0:000}", CInt(dt.Rows.Item(i)("CvoConcepto").ToString)))
                            Percepcion.SetAttribute("Concepto", dt.Rows.Item(i)("Concepto").ToString)
                            Percepcion.SetAttribute("ImporteGravado", Math.Round(Convert.ToDecimal(dt.Rows.Item(i)("ImporteGravado")), 2))
                            Percepcion.SetAttribute("ImporteExento", Math.Round(Convert.ToDecimal(dt.Rows.Item(i)("ImporteExento")), 2))
                            Percepciones.AppendChild(Percepcion)
                        End If
                    Next
                End If

                Nomina.AppendChild(Percepciones)
                '
                ' Deducciones
                '
                If Math.Round(TotalDeducciones, 2) > 0 Then
                    Deducciones = CrearNodo("nomina12:Deducciones")
                    If Math.Round(TotalDeducciones - TotalImpuestosRetenidos, 2) > 0 Then
                        Deducciones.SetAttribute("TotalOtrasDeducciones", Math.Round(TotalDeducciones - TotalImpuestosRetenidos, 2))
                    End If
                    If TotalImpuestosRetenidos > 0 Then
                        Deducciones.SetAttribute("TotalImpuestosRetenidos", Math.Round(TotalImpuestosRetenidos, 2))
                    End If
                    If MyRound(MyRound(TotalDeducciones) - MyRound(TotalImpuestosRetenidos)) > 0 Then
                        Nomina.AppendChild(Deducciones)
                    End If
                End If

                dt = New DataTable
                cNomina = New Nomina()
                cNomina.Ejercicio = ConsultarEjercicio()
                cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                cNomina.Periodo = 0
                cNomina.TipoConcepto = "D"
                cNomina.Tipo = "A"
                cNomina.NoEmpleado = NoEmpleado
                dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                cNomina = Nothing

                If dt.Rows.Count > 0 Then
                    Dim oDataRowDeducciones As DataRow
                    For Each oDataRowDeducciones In dt.Rows
                        If oDataRowDeducciones("CvoSAT").ToString <> "6" Then ' Deducciones diferentes a Incapacidad
                            ObtenerUnidad(NoEmpleado, oDataRowDeducciones("CvoConcepto").ToString)
                            Deduccion = CrearNodo("nomina12:Deduccion")
                            Deduccion.SetAttribute("TipoDeduccion", String.Format("{0:000}", Convert.ToInt32(oDataRowDeducciones("CvoSAT"))))
                            Deduccion.SetAttribute("Clave", String.Format("{0:000}", oDataRowDeducciones("CvoConcepto")))
                            Deduccion.SetAttribute("Concepto", oDataRowDeducciones("Concepto").ToString)
                            Deduccion.SetAttribute("Importe", Math.Round(Convert.ToDecimal(oDataRowDeducciones("Importe")), 2))
                            Deducciones.AppendChild(Deduccion)
                        End If
                    Next
                    Nomina.AppendChild(Deducciones)
                End If

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
    Public Function GetCadenaOriginal(ByVal xmlCFD As String) As String
        Dim xslt As New Xsl.XslCompiledTransform
        Dim xmldoc As New XmlDocument
        Dim navigator As XPath.XPathNavigator
        Dim output As New IO.StringWriter
        xmldoc.LoadXml(xmlCFD)
        navigator = xmldoc.CreateNavigator()
        'xslt.Load(Server.MapPath("~/SAT/") & "cadenaoriginal_3_3.xslt")
        xslt.Load("http://www.sat.gob.mx/sitio_internet/cfd/4/cadenaoriginal_4_0/cadenaoriginal_4_0.xslt")
        xslt.Transform(navigator, Nothing, output)
        GetCadenaOriginal = output.ToString
    End Function
    Private Sub GrabarGeneracionXml(ByVal NoEmpleado As Integer, ByVal IdEjercicio As Integer, ByVal TipoNomina As Integer)
        Dim cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = TipoNomina
        cNomina.Periodo = 0
        cNomina.Tipo = "A"
        cNomina.Generado = "S"
        cNomina.ActualizarEstatusGeneradoNominaEspecial()
    End Sub
    Private Sub btnGenerarPDF_Click(sender As Object, e As EventArgs) Handles btnGenerarPDF.Click
        If cmbPeriodicidad.SelectedValue = 0 Then
            rwAlerta.RadAlert("Selecciona un periodo de pago", 330, 180, "Alerta", "", "")
        Else
            rwConfirm.RadConfirm("¿Está seguro de generar los comprobantes pdf?", "confirmCallbackFnGeneraPDF", 330, 180, Nothing, "Confirmar")
        End If
    End Sub
    Private Sub btnConfirmarGeneraPDF_Click(sender As Object, e As EventArgs) Handles btnConfirmarGeneraPDF.Click
        Dim dt As New DataTable
        Dim cNomina = New Nomina()
        cNomina.Ejercicio = ConsultarEjercicio()
        cNomina.Periodo = 0
        cNomina.Tipo = "A"
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        dt = cNomina.ConsultarEmpleadosGeneradosNominaEspecial()

        Dim dtEmisor As New DataTable
        cNomina = New Nomina()
        cNomina.Id = Session("clienteid")
        dtEmisor = cNomina.ConsultarDatosEmisor()

        Dim RfcEmisor As String = ""
        If dtEmisor.Rows.Count > 0 Then
            For Each oDataRow In dtEmisor.Rows
                RfcEmisor = oDataRow("RFC")
            Next
        End If

        If dt.Rows.Count > 0 Then

            Dim Total As Integer = dt.Rows.Count
            Dim progress As RadProgressContext = RadProgressContext.Current
            progress.Speed = "N/A"
            Dim i As Integer = 0

            For Each row As DataRow In dt.Rows
                i += 1

                Dim ruta As String = ""
                'ruta = Server.MapPath("~/PDF/A/").ToString & "/" & ConsultarEjercicio().ToString & "/" & Session("clienteid").ToString
                ruta = Server.MapPath("~/PDF/" & RfcEmisor & "/A/").ToString & "/" & ConsultarEjercicio().ToString

                If Not Directory.Exists(ruta) Then
                    Directory.CreateDirectory(ruta)
                End If

                GuardaPDF(GeneraPDFNoTimbrado(row("IdEmpresa"), row("Ejercicio"), row("TipoNomina"), row("NoEmpleado")), ruta & "/" & row("RFC") & ".pdf")

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

            Call ConsultarDetalleNominaAguinaldo()
            Call CargarGridEmpleadosAguinaldo()

        End If
    End Sub
    Private Function GeneraPDFNoTimbrado(ByVal IdEmpresa As Integer, ByVal Ejercicio As Integer, ByVal TipoNomina As String, ByVal NoEmpleado As Integer) As Telerik.Reporting.Report

        Dim reporte As New Formatos.formato_comisiones
        reporte.ReportParameters("conn").Value = Session("conexion").ToString

        Dim numero_empleado As String = ""
        Dim periodo_pago As String = ""
        Dim fecha_inicial As String = ""
        Dim fecha_final As String = ""
        Dim regimen As String = ""
        Dim forma_pago As String = ""
        Dim razonsocial As String = ""
        Dim fac_rfc As String = ""

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
        Dim emp_registro_patronal As String = ""
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
        Dim lugar_expedicion1 As String = ""
        Dim lugar_expedicion2 As String = ""
        Dim lugar_expedicion3 As String = ""
        Dim logo_formato As String = ""
        Dim total_percepciones As Decimal = 0
        Dim total_deducciones As Decimal = 0
        Dim total As Decimal = 0

        Dim plantillaid As Integer = 1
        Dim regimen_fiscal As String = ""
        Dim lugar_expedicion As String = ""
        Dim logo As String = ""

        Dim ObjData As New DataControl(0)
        Dim ds As New DataSet

        ds = ObjData.FillDataSet("exec pCliente @cmd=4, @clienteid='" & Session("clienteid") & "'")

        If ds.Tables.Count > 0 Then
            For Each row As DataRow In ds.Tables(0).Rows
                lugar_expedicion = "C. P." & row("fac_cp") & " - " & row("fac_municipio") & ", " & row("fac_pais")
                razonsocial = row("razonsocial")
                fac_rfc = row("rfc")
                plantillaid = row("plantillaid")
                logo = row("logo")
            Next
        End If

        Dim dt As DataTable = New DataTable()

        Dim cNomina As New Nomina()
        cNomina.Ejercicio = Ejercicio
        cNomina.TipoNomina = TipoNomina
        cNomina.Periodo = 0
        cNomina.Tipo = "A"
        cNomina.TipoConcepto = "P"
        cNomina.NoEmpleado = NoEmpleado
        cNomina.CvoConcepto = 50
        dt = cNomina.ConsultarDeduccionesEmpleado()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            total_percepciones = Math.Round(dt.Compute("SUM(Importe)", ""), 6)
        End If

        cNomina = New Nomina()
        cNomina.Ejercicio = Ejercicio
        cNomina.TipoNomina = TipoNomina
        cNomina.Periodo = 0
        cNomina.Tipo = "A"
        cNomina.TipoConcepto = "D"
        cNomina.NoEmpleado = NoEmpleado
        cNomina.CvoConcepto = 86
        dt = cNomina.ConsultarDeduccionesEmpleado()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            total_deducciones = Math.Round(dt.Compute("SUM(Importe)", ""), 6)
        End If

        total = total_percepciones - total_deducciones

        cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = Ejercicio
        cNomina.Periodo = 0
        cNomina.Tipo = "A"
        dt = cNomina.ConsultarDatosNominaEspecial()

        Try

            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    numero_empleado = NoEmpleado
                    periodo_pago = row("periodo_pago")
                    fecha_inicial = row("fecha_inicial")
                    fecha_final = row("fecha_final")
                    regimen = row("regimen")
                    forma_pago = row("forma_pago")
                    lugar_expedicion1 = row("lugar_expedicion1")
                    lugar_expedicion2 = row("lugar_expedicion2")
                    lugar_expedicion3 = row("lugar_expedicion3")

                    emp_nombre = row("emp_nombre")
                    emp_fecha_ingreso = row("emp_fecha_ingreso")
                    emp_rfc = row("emp_rfc")
                    emp_curp = row("emp_curp")
                    emp_numero_seguro_social = row("emp_numero_seguro_social")
                    emp_registro_patronal = row("emp_registro_patronal")
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

                    reporte.ReportParameters("NoEmpleado").Value = NoEmpleado
                    reporte.ReportParameters("Ejercicio").Value = Ejercicio
                    reporte.ReportParameters("TipoNomina").Value = TipoNomina
                    reporte.ReportParameters("Periodo").Value = 0
                    reporte.ReportParameters("Tipo").Value = "A"

                    reporte.ReportParameters("plantillaId").Value = plantillaid
                    reporte.ReportParameters("empleadoid").Value = empleadoid.ToString
                    reporte.ReportParameters("txtNoNomina").Value = "Recibo de pago"
                    reporte.ReportParameters("txtLugarExpedicion1").Value = lugar_expedicion
                    reporte.ReportParameters("txtRazonSocialEmisor").Value = razonsocial.ToString
                    reporte.ReportParameters("txtRFCEmisor").Value = fac_rfc.ToString
                    reporte.ReportParameters("txtRegistroPatronal").Value = emp_registro_patronal.ToString
                    reporte.ReportParameters("txtTipoComprobante").Value = "EGRESO"
                    reporte.ReportParameters("txtFormaPago").Value = "Pago en una sola exhibición"

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

                    Dim DiasPagados As Decimal = 0
                    Dim DiasLaboradosAnio As Decimal = 0

                    Dim datos As New DataTable()
                    cNomina = New Nomina()
                    cNomina.NoEmpleado = NoEmpleado
                    cNomina.TipoNomina = TipoNomina
                    cNomina.Ejercicio = ConsultarEjercicio()
                    datos = cNomina.ConsultarDesgloseAguinaldo()
                    cNomina = Nothing

                    If datos.Rows.Count > 0 Then
                        For Each oDataRow In datos.Rows
                            DiasLaboradosAnio = oDataRow("DiasLaboradosAnio")
                        Next
                    End If

                    DiasPagados = (DiasLaboradosAnio * 15) / 365

                    reporte.ReportParameters("txtDiasPagados").Value = Math.Round(DiasPagados, 2).ToString
                    reporte.ReportParameters("txtPeriocidadPago").Value = periodo_pago.ToString
                    reporte.ReportParameters("txtFechaInicial").Value = Date.Now.ToShortDateString
                    reporte.ReportParameters("txtFechaFinal").Value = Date.Now.ToShortDateString
                    reporte.ReportParameters("txtMetodoPago").Value = forma_pago.ToString
                    reporte.ReportParameters("txtBanco").Value = emp_banco.ToString
                    reporte.ReportParameters("txtClabe").Value = emp_clabe.ToString

                    reporte.ReportParameters("txtTotalPercepciones").Value = FormatCurrency(total_percepciones, 2).ToString
                    reporte.ReportParameters("txtTotalDeducciones").Value = FormatCurrency(total_deducciones, 2).ToString
                    reporte.ReportParameters("txtTotal").Value = FormatCurrency(total, 2).ToString
                    reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString.ToUpper
                    reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/logos/" & logo)

                    datos = New DataTable()
                    cNomina = New Nomina()
                    cNomina.Ejercicio = Ejercicio
                    cNomina.TipoNomina = TipoNomina
                    cNomina.Periodo = 0
                    cNomina.Tipo = "A"
                    cNomina.TipoConcepto = "DE"
                    cNomina.CvoConcepto = 87
                    cNomina.NoEmpleado = NoEmpleado
                    datos = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                    cNomina = Nothing

                    If datos.Rows.Count > 0 Then
                        reporte.ReportParameters("txtEmpleadoSalarioDiario").Value = FormatCurrency(datos.Rows(0).Item("CuotaDiaria"), 2).ToString
                    End If

                Next

                Call GrabarPDF(NoEmpleado, IdEmpresa, Ejercicio, TipoNomina, "S")

            End If
        Catch ex As Exception
            Call GrabarPDF(NoEmpleado, IdEmpresa, Ejercicio, TipoNomina, "N")
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
    Private Sub GrabarPDF(ByVal NoEmpleado As Integer, ByVal IdEmpresa As Integer, ByVal IdEjercicio As Integer, ByVal TipoNomina As Integer, ByVal Pdf As String)
        Dim cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = TipoNomina
        cNomina.Periodo = 0
        cNomina.Tipo = "A"
        cNomina.Pdf = Pdf
        cNomina.ActualizarEstatusPfNominaEspecial()
    End Sub
    Private Sub grdEmpleadosAguinaldo_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles grdEmpleadosAguinaldo.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem

                Dim imgGenerado As Image = CType(e.Item.FindControl("imgGenerado"), Image)
                Dim imgTimbrado As ImageButton = CType(e.Item.FindControl("imgTimbrado"), ImageButton)
                Dim imgAlert As ImageButton = CType(e.Item.FindControl("imgAlert"), ImageButton)
                Dim imgXML As ImageButton = CType(e.Item.FindControl("imgXML"), ImageButton)
                Dim imgPDF As ImageButton = CType(e.Item.FindControl("imgPDF"), ImageButton)
                Dim imgEnviar As ImageButton = CType(e.Item.FindControl("imgEnviar"), ImageButton)
                Dim btnEliminar As ImageButton = CType(e.Item.FindControl("btnEliminar"), ImageButton)

                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)
                'Dim lnkEditar As LinkButton = DirectCast(e.Item.FindControl("lnkEditar"), LinkButton)
                'lnkEditar.Attributes.Add("onclick", "javascript: OpenWindow('" & item.GetDataKeyValue("NoEmpleado").ToString & "','" & item.GetDataKeyValue("IdContrato").ToString & "','" & item.GetDataKeyValue("IdEmpresa").ToString & "','" & item.GetDataKeyValue("IdEjercicio").ToString & "','" & item.GetDataKeyValue("TipoNomina").ToString & "'); return false;")

                Dim cell As TableCell = DirectCast(item("EstatusContrato"), TableCell)

                If item("EstatusContrato").Text = "Activo" Then
                    cell.ForeColor = Drawing.Color.Green
                    cell.Font.Bold = True
                Else
                    cell.ForeColor = Drawing.Color.Red
                    cell.Font.Bold = True
                End If

                If (e.Item.DataItem("EstatusContrato") = "Baja") Then
                    e.Item.ForeColor = Drawing.Color.Red
                End If
                If (e.Item.DataItem("Generado") = "S") Then
                    btnEliminar.Visible = False
                    imgGenerado.Visible = True
                    'lnkEditar.Visible = False
                End If
                If (e.Item.DataItem("Timbrado") = "S") Then
                    btnEliminar.Visible = False
                    imgXML.Visible = True
                Else
                    If (e.Item.DataItem("Timbrado") = "N") Then
                        imgAlert.Visible = True
                    End If
                End If
                If (e.Item.DataItem("Pdf") = "S") Then
                    btnEliminar.Visible = False
                    imgPDF.Visible = True
                    imgEnviar.Visible = True
                End If
                If (e.Item.DataItem("Enviado") = "S") Then
                    imgEnviar.ImageUrl = "~/images/envelopeok.jpg"
                End If
                'Case Telerik.Web.UI.GridItemType.Footer
                '    If Not IsNothing(dtEmpleados) Then
                '        If dtEmpleados.Rows.Count > 0 Then
                '            If Not IsDBNull(dtEmpleados.Compute("sum(Neto)", "")) Then
                '                e.Item.Cells(5).Text = "TOTAL:"
                '                e.Item.Cells(5).HorizontalAlign = HorizontalAlign.Right
                '                e.Item.Cells(5).Font.Bold = True

                '                e.Item.Cells(6).Text = FormatCurrency(dtEmpleados.Compute("sum(Neto)", ""), 2).ToString
                '                e.Item.Cells(6).HorizontalAlign = HorizontalAlign.Right
                '                e.Item.Cells(6).Font.Bold = True
                '            End If
                '        End If
                '    End If
        End Select
    End Sub
    Private Sub grdEmpleadosAguinaldo_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles grdEmpleadosAguinaldo.ItemCommand
        Select Case e.CommandName
            Case "cmdXML"
                Call DownloadXML(e.CommandArgument)
            Case "cmdPDF"
                Dim IdEmpresa As Integer = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("IdEmpresa"))
                Dim NoEmpleado As Integer = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("NoEmpleado"))
                Dim RFC As String = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("RFC"))
                Call DownloadPDF(IdEmpresa, NoEmpleado, RFC)
            Case "cmdSend"
                Dim IdEmpresa As Integer = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("IdEmpresa"))
                Dim NoEmpleado As Integer = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("NoEmpleado"))
                Call EnviaEmail(IdEmpresa, NoEmpleado)
                Call CargarGridEmpleadosAguinaldo()
            Case "cmdDelete"
                empleadoId.Value = e.CommandArgument
                rwConfirm.RadConfirm("¿Realmente desea eliminar al trabajador de esta nomina?", "confirmCallbackFnEliminaEmpleado", 330, 180, Nothing, "Confirmar")
        End Select
    End Sub
    Private Sub DownloadPDF(ByVal IdEmpresa As Long, ByVal NoEmpleado As Long, ByVal RFC As String)

        Dim FilePath = Server.MapPath("~/PDF/A/").ToString & ConsultarEjercicio().ToString & "/" & String.Format("{0:00}", IdEmpresa) & "/" & String.Format("{0:00}", NoEmpleado) & ".pdf"

        Dim dtEmisor As New DataTable
        Dim cNomina = New Nomina()
        cNomina.Id = Session("clienteid")
        dtEmisor = cNomina.ConsultarDatosEmisor()

        Dim RfcEmisor As String = ""
        If dtEmisor.Rows.Count > 0 Then
            For Each oDataRow In dtEmisor.Rows
                RfcEmisor = oDataRow("RFC")
            Next
        End If

        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        Else
            Dim ruta As String = ""
            ruta = Server.MapPath("~/PDF/" & RfcEmisor & "/A/").ToString & "/" & ConsultarEjercicio().ToString

            FilePath = ruta & "/" & RFC & ".pdf"
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
    Private Sub DownloadXML(ByVal UUID As String)

        Call CargarVariablesGenerales()

        Dim rutaEmpresa As String = ""
        rutaEmpresa = Server.MapPath("~\XmlTimbrados\A\").ToString & ConsultarEjercicio().ToString & "\" & Session("clienteid").ToString

        If Not Directory.Exists(rutaEmpresa) Then
            Directory.CreateDirectory(rutaEmpresa)
        End If

        Dim dtEmisor As New DataTable
        Dim cNomina = New Nomina()
        cNomina.Id = Session("clienteid")
        dtEmisor = cNomina.ConsultarDatosEmisor()

        Dim RfcEmisor As String = ""
        If dtEmisor.Rows.Count > 0 Then
            For Each oDataRow In dtEmisor.Rows
                RfcEmisor = oDataRow("RFC")
            Next
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
            rutaEmpresa = Server.MapPath("~\XmlTimbrados\" & RfcEmisor & "\A\").ToString & ConsultarEjercicio().ToString & "\"
            FilePath = rutaEmpresa & "\" & UUID & ".xml"
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
    Private Sub EnviaEmail(ByVal IdEmpresa As Long, ByVal NoEmpleado As Long)
        Dim validos As String = ""
        Dim novalidos As String = ""

        Dim dtEmisor As New DataTable
        Dim cNomina = New Nomina()
        cNomina.Id = Session("clienteid")
        dtEmisor = cNomina.ConsultarDatosEmisor()

        Dim RfcEmisor As String = ""
        If dtEmisor.Rows.Count > 0 Then
            For Each oDataRow In dtEmisor.Rows
                RfcEmisor = oDataRow("RFC")
            Next
        End If

        Dim dt As New DataTable
        cNomina = New Nomina()
        cNomina.Ejercicio = ConsultarEjercicio()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Periodo = 0
        cNomina.Tipo = "A"
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        dt = cNomina.ConsultarEmpleadosGeneradosNominaEspecial()

        If dt.Rows.Count > 0 Then

            Dim Total As Integer = dt.Rows.Count
            Dim progress As RadProgressContext = RadProgressContext.Current
            progress.Speed = "N/A"
            Dim i As Integer = 0

            For Each row In dt.Rows
                i += 1
                Dim FilePath = Server.MapPath("~/PDF/A/").ToString & ConsultarEjercicio().ToString & "/" & String.Format("{0:00}", IdEmpresa) & "/" & String.Format("{0:00}", NoEmpleado) & ".pdf"
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
                cConfiguracion.IdEmpresa = Session("clienteid")
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

                'Dim correo As String = row("Email").ToString.ToLower.Trim
                Dim correo As String = "gesquivel@linkium.mx"

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

                If validos.Length > 0 Then
                    Dim SmtpMail As New SmtpClient
                    Try
                        mensaje = "Estimado(a) " & CStr(row("Nombre")).ToString & vbCrLf & vbCrLf
                        mensaje = "Adjunto a este correo estamos enviándole el Comprobante de Nómina correspondiente al pago de aguinaldo. Cualquier duda o comentario estamos a sus órdenes." & vbCrLf
                        mensaje += "Atentamente." & vbCrLf & vbCrLf
                        mensaje += razonsocial.ToString.ToUpper & vbCrLf
                        mensaje += "Tel. (81) 83 73 14 35"

                        objMM.From = New MailAddress(email_from, razonsocial)
                        objMM.IsBodyHtml = False
                        objMM.Priority = MailPriority.Normal
                        objMM.Subject = razonsocial & " - Recibo de Nómina"
                        objMM.Body = mensaje
                        '
                        '   Agrega anexos
                        '
                        Dim AttachPDF As Net.Mail.Attachment
                        If File.Exists(FilePath) Then
                            AttachPDF = New Net.Mail.Attachment(FilePath)
                            objMM.Attachments.Add(AttachPDF)
                        Else
                            Dim ruta As String = ""
                            ruta = Server.MapPath("~/PDF/" & RfcEmisor & "/A").ToString & "/" & ConsultarEjercicio().ToString
                            FilePath = ruta & "/" & row("RFC") & ".pdf"
                            If File.Exists(FilePath) Then
                                AttachPDF = New Net.Mail.Attachment(FilePath)
                                objMM.Attachments.Add(AttachPDF)
                            End If
                        End If
                        '
                        Dim SmtpUser As New Net.NetworkCredential
                        SmtpUser.UserName = email_smtp_username
                        SmtpUser.Password = email_smtp_password
                        SmtpUser.Domain = email_smtp_server
                        SmtpMail.UseDefaultCredentials = False
                        SmtpMail.Credentials = SmtpUser
                        SmtpMail.Host = email_smtp_server
                        SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network
                        SmtpMail.Send(objMM)
                        '
                        '   Lo marca como enviado a nivel empleado
                        Call GrabarEnviado(row("NoEmpleado"), row("Ejercicio"), "S")
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
                        Call GrabarEnviado(row("NoEmpleado"), row("Ejercicio"), "N")
                    Finally
                        SmtpMail = Nothing
                    End Try
                    objMM = Nothing
                Else
                    Call GrabarEnviado(row("NoEmpleado"), row("Ejercicio"), "N")
                End If
            Next
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
    Private Sub GrabarEnviado(ByVal NoEmpleado As Integer, ByVal Ejercicio As Integer, ByVal Enviado As String)
        Dim cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = Ejercicio
        cNomina.Periodo = 0
        cNomina.Tipo = "A"
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Enviado = Enviado
        cNomina.ActualizarEstatusEnviadoNominaPTU()
    End Sub
    Private Sub btnEnvioComprobantes_Click(sender As Object, e As EventArgs) Handles btnEnvioComprobantes.Click
        If cmbPeriodicidad.SelectedValue = 0 Then
            rwAlerta.RadAlert("Selecciona un periodo de pago", 330, 180, "Alerta", "", "")
        Else
            rwConfirm.RadConfirm("¿Está seguro de enviar TODOS los comprobantes generados?", "confirmCallbackFnEnvioPDF", 330, 180, Nothing, "Confirmar")
        End If
    End Sub
    Private Sub btnConfirmarEnvioPDF_Click(sender As Object, e As EventArgs) Handles btnConfirmarEnvioPDF.Click
        Dim validos As String = ""
        Dim novalidos As String = ""

        Dim dtEmisor As New DataTable
        Dim cNomina = New Nomina()
        cNomina.Id = Session("clienteid")
        dtEmisor = cNomina.ConsultarDatosEmisor()

        Dim RfcEmisor As String = ""
        If dtEmisor.Rows.Count > 0 Then
            For Each oDataRow In dtEmisor.Rows
                RfcEmisor = oDataRow("RFC")
            Next
        End If

        Dim dt As New DataTable
        cNomina = New Nomina()
        cNomina.Ejercicio = ConsultarEjercicio()
        cNomina.Periodo = 0
        cNomina.Tipo = "A"
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        dt = cNomina.ConsultarEmpleadosGeneradosNominaEspecial()

        If dt.Rows.Count > 0 Then

            Dim Total As Integer = dt.Rows.Count
            Dim progress As RadProgressContext = RadProgressContext.Current
            progress.Speed = "N/A"
            Dim i As Integer = 0

            For Each row In dt.Rows
                i += 1
                Dim FilePath = Server.MapPath("~/PDF/A/").ToString & ConsultarEjercicio().ToString & "/" & String.Format("{0:00}", row("IdEmpresa")) & "/" & String.Format("{0:00}", row("NoEmpleado")) & ".pdf"
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
                cConfiguracion.IdEmpresa = Session("clienteid")
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

                'Dim correo As String = row("Email").ToString.ToLower.Trim
                Dim correo As String = "gesquivel@linkium.mx"

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

                If validos.Length > 0 Then
                    Dim SmtpMail As New SmtpClient
                    Try
                        mensaje = "Estimado(a) " & CStr(row("Nombre")).ToString & vbCrLf & vbCrLf
                        mensaje = "Adjunto a este correo estamos enviándole el Comprobante de Nómina correspondiente al pago de aguinaldo. Cualquier duda o comentario estamos a sus órdenes." & vbCrLf
                        mensaje += "Atentamente." & vbCrLf & vbCrLf
                        mensaje += razonsocial.ToString.ToUpper & vbCrLf
                        mensaje += "Tel. (81) 83 73 14 35"

                        objMM.From = New MailAddress(email_from, razonsocial)
                        objMM.IsBodyHtml = False
                        objMM.Priority = MailPriority.Normal
                        objMM.Subject = razonsocial & " - Recibo de Nómina"
                        objMM.Body = mensaje
                        '
                        '   Agrega anexos
                        '
                        Dim AttachPDF As Net.Mail.Attachment
                        If File.Exists(FilePath) Then
                            AttachPDF = New Net.Mail.Attachment(FilePath)
                            objMM.Attachments.Add(AttachPDF)
                        Else
                            Dim ruta As String = ""
                            ruta = Server.MapPath("~/PDF/" & RfcEmisor & "/A").ToString & "/" & ConsultarEjercicio().ToString
                            FilePath = ruta & "/" & row("RFC") & ".pdf"
                            If File.Exists(FilePath) Then
                                AttachPDF = New Net.Mail.Attachment(FilePath)
                                objMM.Attachments.Add(AttachPDF)
                            End If
                        End If
                        '
                        Dim SmtpUser As New Net.NetworkCredential
                        SmtpUser.UserName = email_smtp_username
                        SmtpUser.Password = email_smtp_password
                        SmtpUser.Domain = email_smtp_server
                        SmtpMail.UseDefaultCredentials = False
                        SmtpMail.Credentials = SmtpUser
                        SmtpMail.Host = email_smtp_server
                        SmtpMail.DeliveryMethod = SmtpDeliveryMethod.Network
                        SmtpMail.Send(objMM)
                        '
                        '   Lo marca como enviado a nivel empleado
                        '
                        Call GrabarEnviado(row("NoEmpleado"), row("Ejercicio"), "S")
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
                        Call GrabarEnviado(row("NoEmpleado"), row("Ejercicio"), "N")
                    Finally
                        SmtpMail = Nothing
                    End Try
                    objMM = Nothing
                Else
                    Call GrabarEnviado(row("NoEmpleado"), row("Ejercicio"), "N")
                End If
            Next
        End If
    End Sub
    Private Sub cmbPeriodicidad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPeriodicidad.SelectedIndexChanged
        If cmbPeriodicidad.SelectedValue > 0 Then
            Call ConsultarDetalleNominaAguinaldo()
            Call CargarGridEmpleadosAguinaldo()
        Else
            grdEmpleadosAguinaldo.DataSource = String.Empty
            grdEmpleadosAguinaldo.DataBind()
            btnGeneraNomina.Enabled = True
            btnBorrarNomina.Enabled = False
            btnGenerarNominaElectronica.Enabled = False
            btnTimbrarNomina.Enabled = False
            btnGenerarPDF.Enabled = False
            btnEnvioComprobantes.Enabled = False
            btnGeneraTxtDispersion.Enabled = False
        End If
    End Sub
    Private Sub ObtenerUnidad(ByVal NoEmpleado As Integer, ByVal CvoConcepto As String)
        Try

            Call CargarVariablesGenerales()

            Dim dt As DataTable
            Dim cNomina As New Nomina()
            cNomina.Ejercicio = ConsultarEjercicio()
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = 0
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = CvoConcepto
            dt = cNomina.ConsultarConceptosEmpleado()

            If dt.Rows.Count > 0 Then
                Unidad = dt.Rows(0).Item("Unidad").ToString
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub
    Private Sub btnTimbrarNomina_Click(sender As Object, e As EventArgs) Handles btnTimbrarNomina.Click
        If cmbPeriodicidad.SelectedValue = 0 Then
            rwAlerta.RadAlert("Selecciona un periodo de pago", 330, 180, "Alerta", "", "")
        Else
            rwAlerta.RadConfirm("¿Está seguro de timbrar la nómina, una vez timbrada no podrá hacer modificaciones?", "confirmCallbackFnTimbrarNomina", 330, 180, Nothing, "Confirmar")
        End If
    End Sub
    Private Sub btnConfirmarTimbraNomina_Click(sender As Object, e As EventArgs) Handles btnConfirmarTimbraNomina.Click
        If cmbPeriodicidad.SelectedValue > 0 Then

            Dim RfcEmisor As String = ""

            Call CargarVariablesGenerales()

            Dim dt As New DataTable
            Dim cNomina = New Nomina()
            cNomina.Ejercicio = ConsultarEjercicio()
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            cNomina.Periodo = 0
            cNomina.Tipo = "A"
            dt = cNomina.ConsultarEmpleadosTimbrar()

            Dim FolioXmlTimbrado As String = ""

            Dim dtEmisor As New DataTable
            cNomina = New Nomina()
            cNomina.Id = Session("clienteid")
            dtEmisor = cNomina.ConsultarDatosEmisor()

            If dtEmisor.Rows.Count > 0 Then
                For Each oDataRow In dtEmisor.Rows
                    RfcEmisor = oDataRow("RFC")
                Next
            End If

            Dim ruta As String = ""
            ruta = Server.MapPath("~/XmlGenerados/" & RfcEmisor & "/A/").ToString & "/" & ConsultarEjercicio().ToString

            If Not Directory.Exists(ruta) Then
                Directory.CreateDirectory(ruta)
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

                    FolioXml = ruta & "/" & oDataRow("Serie").ToString & oDataRow("Folio").ToString & ".xml"

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

                        CrearCFDNominaEspecial(oDataRow("NoEmpleado"), ruta)
                        GrabarGeneracionXml(oDataRow("NoEmpleado"), oDataRow("Ejercicio"), oDataRow("TipoNomina"))

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

                    CrearCFDNominaEspecial(oDataRow("NoEmpleado"), ruta)
                    GrabarGeneracionXml(oDataRow("NoEmpleado"), oDataRow("Ejercicio"), oDataRow("TipoNomina"))

                    Try
                        Dim SIFEIUsuario As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIUsuario")
                        Dim SIFEIContrasena As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIContrasena")
                        Dim SIFEIIdEquipo As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIIdEquipo")
                        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType) Or DirectCast(768, System.Net.SecurityProtocolType) Or DirectCast(192, System.Net.SecurityProtocolType) Or DirectCast(48, System.Net.SecurityProtocolType)
                        'Pruebas
                        'Dim TimbreSifeiVersion33 As New SIFEIPruebasV33.SIFEIService()

                        'Producción
                        Dim TimbreSifeiVersion33 As New SIFEI33.SIFEIService()
                        Call Comprimir()

                        Dim bytes() As Byte
                        bytes = TimbreSifeiVersion33.getCFDI(SIFEIUsuario, SIFEIContrasena, Data, "", SIFEIIdEquipo)
                        Descomprimir(bytes, oDataRow("NoEmpleado"), oDataRow("RFC"))
                        '
                        '   Descontar Adeudo Fonacot
                        '
                        Dim dtCreditoFonacot As New DataTable
                        cNomina = New Nomina()
                        cNomina.Ejercicio = ConsultarEjercicio()
                        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                        cNomina.Periodo = 0
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
                            cFonacot.Ejercicio = ConsultarEjercicio()
                            cFonacot.TipoNomina = cmbPeriodicidad.SelectedValue
                            cFonacot.Periodo = 0
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
                        cNomina.Ejercicio = ConsultarEjercicio()
                        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                        cNomina.Periodo = 0
                        cNomina.NoEmpleado = oDataRow("NoEmpleado")
                        cNomina.CvoConcepto = 71
                        dtPrestamosPersonal = cNomina.ConsultarConceptosEmpleado()

                        If dtPrestamosPersonal.Rows.Count > 0 Then
                            Dim dts As New DataTable
                            Dim cPrestamoPersonal = New Entities.PrestamoPersonal()
                            cPrestamoPersonal.NoEmpleado = oDataRow("NoEmpleado")
                            cPrestamoPersonal.Ejercicio = ConsultarEjercicio()
                            cPrestamoPersonal.TipoNomina = cmbPeriodicidad.SelectedValue
                            cPrestamoPersonal.Periodo = 0
                            dts = cPrestamoPersonal.ConsultarEmpleadosConPrestamoPersonalNomina()
                            cPrestamoPersonal = Nothing

                            If dts.Rows.Count > 0 Then
                                For Each row In dts.Rows
                                    cPrestamoPersonal = New Entities.PrestamoPersonal()
                                    cPrestamoPersonal.IdPrestamoPersonal = row("IdPrestamoPersonal")
                                    cPrestamoPersonal.NoEmpleado = oDataRow("NoEmpleado")
                                    cPrestamoPersonal.Ejercicio = ConsultarEjercicio()
                                    cPrestamoPersonal.TipoNomina = cmbPeriodicidad.SelectedValue
                                    cPrestamoPersonal.Periodo = 0
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

                        CrearCFDNominaEspecial(oDataRow("NoEmpleado"), ruta)
                        GrabarGeneracionXml(oDataRow("NoEmpleado"), oDataRow("Ejercicio"), oDataRow("TipoNomina"))

                        Try
                            '
                            '   Vuelve a intentar timbrar el Comprobante Sellado
                            '
                            Dim SIFEIUsuario As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIUsuario")
                            Dim SIFEIContrasena As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIContrasena")
                            Dim SIFEIIdEquipo As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIIdEquipo")

                            'Pruebas
                            'Dim TimbreSifeiVersion33 As New SIFEIPruebasV33.SIFEIService()

                            'Producción
                            Dim TimbreSifeiVersion33 As New SIFEI33.SIFEIService()
                            Call Comprimir()

                            Dim bytes() As Byte
                            bytes = TimbreSifeiVersion33.getCFDI(SIFEIUsuario, SIFEIContrasena, Data, "", SIFEIIdEquipo)
                            Descomprimir(bytes, oDataRow("NoEmpleado"), oDataRow("RFC"))
                            '
                            '   Descontar Adeudo Fonacot
                            '
                            Dim dtCreditoFonacot As New DataTable
                            cNomina = New Nomina()
                            cNomina.Ejercicio = ConsultarEjercicio()
                            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                            cNomina.Periodo = 0
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
                                cFonacot.Ejercicio = ConsultarEjercicio()
                                cFonacot.TipoNomina = cmbPeriodicidad.SelectedValue
                                cFonacot.Periodo = 0
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
                            cNomina.Ejercicio = ConsultarEjercicio()
                            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
                            cNomina.Periodo = 0
                            cNomina.NoEmpleado = oDataRow("NoEmpleado")
                            cNomina.CvoConcepto = 71
                            dtPrestamosPersonal = cNomina.ConsultarConceptosEmpleado()

                            If dtPrestamosPersonal.Rows.Count > 0 Then
                                Dim dts As New DataTable
                                Dim cPrestamoPersonal = New Entities.PrestamoPersonal()
                                cPrestamoPersonal.NoEmpleado = oDataRow("NoEmpleado")
                                cPrestamoPersonal.Ejercicio = ConsultarEjercicio()
                                cPrestamoPersonal.TipoNomina = cmbPeriodicidad.SelectedValue
                                cPrestamoPersonal.Periodo = 0
                                dts = cPrestamoPersonal.ConsultarEmpleadosConPrestamoPersonalNomina()
                                cPrestamoPersonal = Nothing

                                If dts.Rows.Count > 0 Then
                                    For Each row In dts.Rows
                                        cPrestamoPersonal = New Entities.PrestamoPersonal()
                                        cPrestamoPersonal.IdPrestamoPersonal = row("IdPrestamoPersonal")
                                        cPrestamoPersonal.NoEmpleado = oDataRow("NoEmpleado")
                                        cPrestamoPersonal.Ejercicio = ConsultarEjercicio()
                                        cPrestamoPersonal.TipoNomina = cmbPeriodicidad.SelectedValue
                                        cPrestamoPersonal.Periodo = 0
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
                            ErrorTimbrado.Ejercicio = ConsultarEjercicio()
                            ErrorTimbrado.TipoNomina = cmbPeriodicidad.SelectedValue
                            ErrorTimbrado.Periodo = 0
                            ErrorTimbrado.NoEmpleado = oDataRow("NoEmpleado")
                            ErrorTimbrado.IdContrato = oDataRow("IdContrato")
                            ErrorTimbrado.Descripcion = ex.Detail.InnerText.ToString
                            ErrorTimbrado.IdUsuario = Session("usuarioid")
                            ErrorTimbrado.Tipo = "A"
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

                Call ConsultarDetalleNominaAguinaldo()
                Call CargarGridEmpleadosAguinaldo()

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

        Dim RfcEmisor As String = ""

        Dim ms1 As New MemoryStream(data5)
        Dim zip1 As ZipFile = New ZipFile()
        zip1 = ZipFile.Read(ms1)

        Dim dtEmisor As New DataTable
        Dim cNomina As New Entities.Nomina()
        cNomina.Id = Session("clienteid")
        dtEmisor = cNomina.ConsultarDatosEmisor()

        If dtEmisor.Rows.Count > 0 Then
            For Each oDataRow In dtEmisor.Rows
                RfcEmisor = oDataRow("RFC")
            Next
        End If

        Dim archivo As String = ""
        Dim DirectorioExtraccion As String = ""
        DirectorioExtraccion = Server.MapPath("~\tmpXmlTimbrados\" & RfcEmisor & "\A\").ToString & ConsultarEjercicio().ToString & "\"

        If Not Directory.Exists(DirectorioExtraccion) Then
            Directory.CreateDirectory(DirectorioExtraccion)
        End If

        Dim e As ZipEntry
        For Each e In zip1
            archivo = zip1.Entries(0).FileName.ToString
            e.Extract(DirectorioExtraccion, ExtractExistingFileAction.OverwriteSilently)
        Next

        If File.Exists(DirectorioExtraccion & archivo) Then
            Dim UUID As String = ""
            Dim FolioXmlTimbrado As String = ""

            UUID = GetXmlAttribute(DirectorioExtraccion & archivo, "UUID", "tfd:TimbreFiscalDigital").ToString
            FolioUUID.Value = UUID

            Call GrabarTimbrado(NoEmpleado, "S", UUID)

            FolioXmlTimbrado = Server.MapPath("~\XmlTimbrados\" & RfcEmisor & "\A\").ToString & ConsultarEjercicio().ToString & "\" & UUID & ".xml"

            If File.Exists(FolioXml) Then
                My.Computer.FileSystem.CopyFile(DirectorioExtraccion & archivo, FolioXmlTimbrado)
                File.Delete(FolioXml)
            End If

        End If

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
    Private Sub GrabarTimbrado(ByVal NoEmpleado As Integer, ByVal Timbrado As String, ByVal UUID As String)
        Call CargarVariablesGenerales()

        Dim cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = ConsultarEjercicio()
        cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
        cNomina.Periodo = 0
        cNomina.Timbrado = Timbrado
        cNomina.UUID = UUID
        cNomina.Tipo = "A"
        cNomina.ActualizarEstatusTimbradoNomina()

    End Sub
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
                                If FlujoReader.Name = campo Then
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
    Private Function MyRound(Importe As Decimal) As Decimal
        Dim r As Decimal = Math.Ceiling(Importe * 100D) / 100D
        Return r
    End Function
    Private Sub btnGeneraTxtDispersion_Click(sender As Object, e As EventArgs) Handles btnGeneraTxtDispersion.Click
        If cmbPeriodicidad.SelectedValue > 0 Then

            Dim dtEmisor As New DataTable
            Dim cNomina = New Nomina()
            cNomina.Id = Session("clienteid")
            dtEmisor = cNomina.ConsultarDatosEmisor()

            Dim RfcEmisor As String = ""
            If dtEmisor.Rows.Count > 0 Then
                For Each oDataRow In dtEmisor.Rows
                    RfcEmisor = oDataRow("RFC")
                Next
            End If

            Call CargarVariablesGenerales()

            Dim folio_banorte As Integer = 0
            Dim folio_banamex As Integer = 0
            Dim folio_santander As Integer = 0

            Dim ruta_banorte As String = ""
            Dim ruta_banamex As String = ""
            Dim ruta_santander As String = ""

            Dim FilePathBanorte As String = ""
            Dim FilePathBanamex As String = ""
            Dim FilePathSantander As String = ""

            Dim dt As New DataTable()
            cNomina = New Nomina()
            cNomina.Ejercicio = ConsultarEjercicio()
            cNomina.TipoNomina = cmbPeriodicidad.SelectedValue
            dt = cNomina.ConsultarDetalleNominaAguinaldo()

            Dim texto As String = ""
            Dim Periodo As String = ""

            If cmbPeriodicidad.SelectedValue = 1 Then
                Periodo = "S"
            ElseIf cmbPeriodicidad.SelectedValue = 2 Then
                Periodo = "C"
            ElseIf cmbPeriodicidad.SelectedValue = 3 Then
                Periodo = "Q"
            ElseIf cmbPeriodicidad.SelectedValue = 4 Then
                Periodo = "M"
            End If

            If dt.Rows.Count > 0 Then

                Dim total_banorte As Decimal = 0
                Dim total_banamex As Decimal = 0
                Dim total_santander As Decimal = 0

                Dim total_registros_banorte As Decimal = 0
                Dim total_registros_banamex As Decimal = 0
                Dim total_registros_santander As Decimal = 0

                For Each row In dt.Rows
                    If CStr(row("BancoId")) = "072" And Math.Round(Convert.ToDecimal(row("Neto")), 2) > 0 Then
                        total_registros_banorte = total_registros_banorte + 1
                        total_banorte = total_banorte + Math.Round(Convert.ToDecimal(row("Neto")), 2)
                    ElseIf CStr(row("BancoId")) = "002" And Math.Round(Convert.ToDecimal(row("Neto")), 2) > 0 Then
                        total_registros_banamex = total_registros_banamex + 1
                        total_banamex = total_banamex + Math.Round(Convert.ToDecimal(row("Neto")), 2)
                    ElseIf CStr(row("BancoId")) = "014" And Math.Round(Convert.ToDecimal(row("Neto")), 2) > 0 Then
                        total_registros_santander = total_registros_santander + 1
                        total_santander = total_santander + Math.Round(Convert.ToDecimal(row("Neto")), 2)
                    End If
                Next

                If total_banorte > 0 Then

                    Dim cDispersionNomina As New DispersionNomina()
                    cDispersionNomina.clave_servicio = "NE"
                    cDispersionNomina.total_registros_enviados = total_registros_banorte
                    cDispersionNomina.importe_total_registros_enviados = total_banorte
                    cDispersionNomina.accion = 0
                    cDispersionNomina.periodoid = 0
                    cDispersionNomina.empresaid = Session("clienteid")
                    cDispersionNomina.ejercicio = ConsultarEjercicio()
                    cDispersionNomina.tipo_nomina = cmbPeriodicidad.SelectedValue
                    cDispersionNomina.tipo = "A"
                    cDispersionNomina.userid = Session("usuarioid")
                    cDispersionNomina.bancoid = "072" 'Banorte
                    folio_banorte = cDispersionNomina.AgregaFolio()
                    cDispersionNomina = Nothing

                    ruta_banorte = Server.MapPath("~/TXT/" & RfcEmisor & "/BANORTE/A/").ToString & Periodo.ToString & "/" & ConsultarEjercicio().ToString & "/"

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
                    cDispersionNomina.periodoid = 0
                    cDispersionNomina.empresaid = Session("clienteid")
                    cDispersionNomina.ejercicio = ConsultarEjercicio()
                    cDispersionNomina.tipo_nomina = cmbPeriodicidad.SelectedValue
                    cDispersionNomina.tipo = "A"
                    cDispersionNomina.userid = Session("usuarioid")
                    cDispersionNomina.bancoid = "002" 'Banamex
                    folio_banamex = cDispersionNomina.AgregaFolio()
                    cDispersionNomina = Nothing

                    ruta_banamex = Server.MapPath("~/TXT/" & RfcEmisor & "/BANAMEX/A/").ToString & Periodo.ToString & "/" & ConsultarEjercicio().ToString & "/"

                    If Not Directory.Exists(ruta_banamex) Then
                        Directory.CreateDirectory(ruta_banamex)
                    End If

                End If

                If total_santander > 0 Then

                    Dim cDispersionNomina As New DispersionNomina()
                    cDispersionNomina.clave_servicio = ""
                    cDispersionNomina.total_registros_enviados = total_registros_santander
                    cDispersionNomina.importe_total_registros_enviados = total_santander
                    cDispersionNomina.accion = 0
                    cDispersionNomina.periodoid = 0
                    cDispersionNomina.empresaid = Session("clienteid")
                    cDispersionNomina.ejercicio = ConsultarEjercicio()
                    cDispersionNomina.tipo_nomina = cmbPeriodicidad.SelectedValue
                    cDispersionNomina.tipo = "A"
                    cDispersionNomina.userid = Session("usuarioid")
                    cDispersionNomina.bancoid = "014" 'Santander
                    folio_santander = cDispersionNomina.AgregaFolio()
                    cDispersionNomina = Nothing

                    ruta_santander = Server.MapPath("~/TXT/" & RfcEmisor & "/SANTANDER/A/").ToString & Periodo.ToString & "/" & ConsultarEjercicio().ToString & "/"

                    If Not Directory.Exists(ruta_santander) Then
                        Directory.CreateDirectory(ruta_santander)
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
                                texto = texto & registro("cuenta_bancaria").ToString.Trim.TrimStart.TrimEnd     'Cuenta Cargo                           Para clave de servicio NE (Nómina empresarial). Numero de cuenta que utilzara la emisora para realizar su dispersion de nómina
                                texto = texto & "                                                       " & vbCrLf                  'Filler(59 espacios vacios)
                                '
                                '   DETALLE
                                '
                                For Each row In dt.Rows
                                    If CStr(row("BancoId")) = "072" And Math.Round(Convert.ToDecimal(row("Neto")), 2) > 0 Then

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
                                texto = texto & "Aguinaldo " & CDate(registro("fecha_proceso")).ToString("ddMMyy").Trim.TrimStart.TrimEnd.PadRight(10, " ")                      'Descripción del archivo                        Descripción del tipo de pago que esta realizando. Esta descripción se mostrará en su estado de cuenta de Banca Electrónica e impreso. Justificado a la izquierda y completar con espacios en blanco
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
                                texto = texto & String.Format("{0:00000000000000000000}", CInt(registro("cuenta_bancaria").ToString.Trim.TrimStart.TrimEnd)) '                                          Número de cuenta de cargo o abono, justificar a la derecha y completar con ceros
                                texto = texto & "                    " & vbCrLf                                         'Espacio en blanco
                                '
                                '   DETALLE
                                '
                                Dim referencia As Integer = 0
                                For Each row In dt.Rows
                                    If CStr(row("BancoId")) = "002" And Math.Round(Convert.ToDecimal(row("Neto")), 2) > 0 Then

                                        referencia = referencia + 1

                                        Dim simporte As String = ""
                                        simporte = String.Format("{0:F2}", Math.Round(Convert.ToDecimal(row("Neto")), 2)).ToString.Replace(".", "")
                                        simporte = simporte.PadLeft(18, "0")

                                        texto = texto & "3"                                                                     'Tipo de registro                       Es constante y siempre es = 3 (Registro de Movtos. Individuales)
                                        texto = texto & "0"                                                                     'Tipo de operación                      0 = Si es Abono (solo aplica para cobros) 1 = Si es Cargo
                                        texto = texto & "001"                                                                   'Clave de la moneda                     Es constante y siempre es = 001 (Pesos M.N.)
                                        texto = texto & simporte                                                                'Importe                                Importe a abonar o cargar, justificado a la derecha sin punto decimal.
                                        texto = texto & "03"                                                                    'Tipo de cuenta                         03=Plásticos
                                        texto = texto & String.Format("{0:00000000000000000000}", Convert.ToDecimal(row("num_tarjeta")))     'Número de cuenta                       Número de cuenta de cargo o abono, justificar a la derecha y completar con ceros
                                        texto = texto & referencia.ToString.PadRight(40, " ")                                   'Referencia alfanumérica                Debe ser diferente de cero.
                                        texto = texto & row("nombre_completo").ToString.PadRight(55, " ")                       'Beneficiario                           Si es Persona Física deberá llenarse: Nombre(s), Apellido Paterno/Apellido Materno
                                        texto = texto & "".ToString.PadRight(40, " ")                                           'Instrucciones                          Este campo es la Referencia Alfanumérica que se reflejará en el estado de cuenta de Banca Electrónica e impreso de su beneficiario
                                        texto = texto & "".ToString.PadRight(24, " ")                                           'Descripción TEF                        Llenar con espacios en blanco.
                                        texto = texto & String.Format("{0:0000}", CInt(row("BancoId")))                         'Clave de Banco                         Clave de banco de la cuenta de abono, solo para pago interbancario
                                        texto = texto & "".ToString.PadRight(7, " ")                                            'Referencia Numérica                    Llenar con espacios en blanco.
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
                                texto = texto & String.Format("{0:000000}", 1)                                          'Número de cargos                               Número total de cargos del archivo
                                texto = texto & stotal                                                                  'Importe total de cargos                        Sumatoria de los cargos

                                archivo = CDate(registro("fecha_proceso")).ToString("ddMMyyyy").ToString & String.Format("{0:0000}", registro("consecutivo")) & ".txt"

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
                '
                '   Dispersión Nómina Santander
                '
                If folio_santander > 0 Then

                    Dim datos As New DataTable

                    If total_santander > 0 Then
                        Dim cDispersionNomina As New DispersionNomina()
                        cDispersionNomina.id = folio_santander
                        datos = cDispersionNomina.ConsultarDispersionNomina()

                        If datos.Rows.Count > 0 Then

                            Dim archivo As String = ""

                            Dim stotal As String = ""
                            stotal = String.Format("{0:F2}", Math.Round(Convert.ToDecimal(total_santander), 2)).ToString.Replace(".", "")
                            stotal = stotal.PadLeft(18, "0")

                            Dim secuencia As Integer = 1
                            For Each registro In datos.Rows
                                '
                                '   ENCABEZADO
                                '
                                texto = String.Empty
                                texto = texto & "1"                                                                         'Registro encabezado
                                texto = texto & String.Format("{0:00000}", secuencia)                                       'Incremento de unidad por registro
                                texto = texto & "E"                                                                         'Archivo de salida de Enlace
                                texto = texto & Date.Now.ToString("MMddyyyy").ToString                                      'Fecha de generación del archivo
                                texto = texto & registro("cuenta_bancaria").ToString.Trim.TrimStart.TrimEnd.PadRight(16, " ")                      'Cuenta de cargo, alineada a la izquierda y se rellena con blancos
                                texto = texto & CDate(registro("fecha_proceso")).ToString("MMddyyyy").ToString & vbCrLf     'Fecha de aplicacion del pago
                                '
                                '   DETALLE
                                '
                                Dim referencia As Integer = 0
                                For Each row In dt.Rows
                                    If CStr(row("BancoId")) = "014" And Math.Round(Convert.ToDecimal(row("Neto")), 2) > 0 Then

                                        referencia = referencia + 1
                                        secuencia = secuencia + 1

                                        Dim simporte As String = ""
                                        simporte = String.Format("{0:F2}", Math.Round(Convert.ToDecimal(row("Neto")), 2)).ToString.Replace(".", "")
                                        simporte = simporte.PadLeft(18, "0")

                                        texto = texto & "2"                                                                         'Registro de detalle
                                        texto = texto & String.Format("{0:00000}", secuencia)                                       'Incremento de unidad por registro
                                        texto = texto & row("NoEmpleado").ToString.PadRight(7, " ")                                 'Número de empleados
                                        texto = texto & row("apellido_paterno").ToString.PadRight(30, " ")                          'Apellido paterno del empleado
                                        texto = texto & row("apellido_materno").ToString.PadRight(20, " ")                          'Apellido materno del empleado
                                        texto = texto & row("nombre").ToString.PadRight(30, " ")                                    'Nómbre del empleado
                                        texto = texto & row("num_cuenta").ToString.PadRight(16, " ")                                'Cuenta de abono, alineada a la izquierda y se rellena con blancos
                                        texto = texto & simporte & vbCrLf                                                           'Importe, 16 enteros y dos decimales sin punto
                                    End If
                                Next
                                secuencia = secuencia + 1
                                '
                                '   TOTALES
                                '
                                texto = texto & "3"                                                                         'Registro sumario
                                texto = texto & String.Format("{0:00000}", secuencia)                                       'Incremento de unidad por registro
                                texto = texto & String.Format("{0:00000}", referencia)                                      'Total de registros enviados
                                texto = texto & stotal                                                                      'Importe total, 16 enteros y dos decimales sin punto

                                archivo = CDate(registro("fecha_proceso")).ToString("ddMMyyyy").ToString & String.Format("{0:0000}", registro("consecutivo")) & ".txt"

                                If System.IO.File.Exists(ruta_santander & archivo) Then
                                    System.IO.File.Delete(ruta_santander & archivo)
                                    Dim fs As FileStream = File.Create(ruta_santander & archivo)
                                    Dim info As Byte() = New UTF8Encoding(True).GetBytes(texto)
                                    fs.Write(info, 0, info.Length)
                                    fs.Close()
                                Else
                                    Dim fs As FileStream = File.Create(ruta_santander & archivo)
                                    Dim info As Byte() = New UTF8Encoding(True).GetBytes(texto)
                                    fs.Write(info, 0, info.Length)
                                    fs.Close()
                                End If

                                FilePathSantander = ruta_santander & archivo

                            Next

                        End If
                    End If
                End If

                Using zip As ZipFile = New ZipFile()
                    Dim ruta As String = ""
                    ruta = Server.MapPath("~/TXT/" & RfcEmisor & "/A/").ToString & Periodo.ToString & "/" & ConsultarEjercicio().ToString & "/"

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

                    If folio_santander > 0 Then
                        If File.Exists(FilePathSantander) Then
                            zip.AddFile(FilePathSantander, "Santander")
                        End If
                    End If

                    Dim FilePath As String = ruta & RfcEmisor & "-" & ConsultarEjercicio().ToString & ".rar"
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
    Private Sub btnModificacionDeNomina_Click(sender As Object, e As EventArgs) Handles btnModificacionDeNomina.Click
        If cmbPeriodicidad.SelectedValue > 0 Then
            Response.Redirect("~/ModificacionGeneralAguinaldo.aspx?id=" & cmbPeriodicidad.SelectedValue.ToString)
        Else
            rwAlerta.RadAlert("Seleccione un periodo.", 330, 180, "Alerta", "", "")
        End If
    End Sub

End Class