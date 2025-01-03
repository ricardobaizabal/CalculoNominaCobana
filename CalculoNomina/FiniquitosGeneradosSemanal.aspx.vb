﻿Imports System.Data
Imports System.Data.SqlClient
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
Public Class FiniquitosGeneradosSemanal
    Inherits System.Web.UI.Page

    Dim ObjData As New DataControl()
    Private IdEmpresa As Integer = 0
    Private IdEjercicio As Integer = 0

    Private TotalPercepciones As Double
    Private TotalDeducciones As Double
    Private NetoAPagar As Double
    Private PercepcionesGravadas As Double
    Private PercepcionesExentas As Double
    Private CuotaDiaria As Decimal = 0
    Private SalarioDiarioIntegradoTrabajador As Decimal = 0

    Private NumeroDeDiasPagados As Double
    Private Unidad As String
    Public FolioXml As String

    Const URI_SAT = "http://www.sat.gob.mx/cfd/4"
    Private m_xmlDOM As New XmlDocument
    Private urlnomina As String
    Private data As Byte()
    Private qrBackColor As Integer = System.Drawing.Color.FromArgb(255, 255, 255, 255).ToArgb
    Private qrForeColor As Integer = System.Drawing.Color.FromArgb(255, 0, 0, 0).ToArgb

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            Dim objCat As New DataControl()
            Dim cConcepto As New Entities.Catalogos
            objCat.CatalogoRad(cmbCliente, cConcepto.ConsultarMisClientes, True, False)
            'objCat.CatalogoRad(cmbPeriodicidad, cConcepto.ConsultarPeriodoPago2, True, False)
            objCat = Nothing

            Call CargarVariablesGenerales()
            Call CargaPeriodos(1)
            Call CargarGridEmpleados()

            lblEjercicio.Text = IdEjercicio.ToString

        End If

        RadProgressArea1.Localization.TotalFiles = "Total empleados"
        RadProgressArea1.Localization.UploadedFiles = "Calculados"
        RadProgressArea1.Localization.CurrentFileName = "Calculando: "

    End Sub
    Private Sub CargaPeriodos(Optional ByVal IdTipoNomina As Integer = 0)
        Call CargarVariablesGenerales()
        Dim cPeriodo As New Entities.Periodo
        cPeriodo.IdEmpresa = IdEmpresa
        cPeriodo.IdCliente = cmbCliente.SelectedValue
        cPeriodo.IdEjercicio = IdEjercicio
        cPeriodo.IdTipoNomina = IdTipoNomina
        cPeriodo.ExtraordinarioBit = False
        ObjData.CatalogoRad(cmbPeriodo, cPeriodo.ConsultarPeriodos(), True, False)
    End Sub
    Private Sub CargarGridEmpleados()

        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = cmbCliente.SelectedValue
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Tipo = "F"
        cNomina.Periodo = cmbPeriodo.SelectedValue
        dt = cNomina.ConsultarEmpleadosGeneradosFiniquito()
        grdEmpleados.DataSource = dt
        grdEmpleados.DataBind()
        cNomina = Nothing

        Call BloquearBotones()

    End Sub
    Private Sub grdEmpleados_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles grdEmpleados.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem

                Dim imgGenerado As Image = CType(e.Item.FindControl("imgGenerado"), Image)
                Dim imgTimbrado As ImageButton = CType(e.Item.FindControl("imgTimbrado"), ImageButton)
                Dim imgAlert As ImageButton = CType(e.Item.FindControl("imgAlert"), ImageButton)
                Dim imgXML As ImageButton = CType(e.Item.FindControl("imgXML"), ImageButton)
                'Dim imgPDF As ImageButton = CType(e.Item.FindControl("imgPDF"), ImageButton)
                Dim imgPDFTimbrado As ImageButton = CType(e.Item.FindControl("imgPDFTimbrado"), ImageButton)
                Dim imgEnviar As ImageButton = CType(e.Item.FindControl("imgEnviar"), ImageButton)

                Dim item As GridDataItem = TryCast(e.Item, GridDataItem)

                If (e.Item.DataItem("Generado") = "S") Then
                    imgGenerado.Visible = True
                End If

                If (e.Item.DataItem("Timbrado") = "S") Then
                    imgGenerado.Visible = True
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
                    'imgPDF.Visible = True
                    imgEnviar.Visible = True
                End If

                If (e.Item.DataItem("Enviado") = "S") Then
                    imgEnviar.ImageUrl = "~/images/envelopeok.jpg"
                End If

        End Select
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
            Next
        End If
    End Sub
    Private Sub cmbPeriodo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPeriodo.SelectedIndexChanged
        Call CargarGridEmpleados()
    End Sub
    Public Sub BloquearBotones()

        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Tipo = "F"
        cNomina.Periodo = cmbPeriodo.SelectedValue
        dt = cNomina.ConsultarEmpleadosGeneradosFiniquito()

        If dt.Rows.Count > 0 Then

            panelDatos.Visible = True
            btnTimbrarNomina.Enabled = False
            btnGenerarNominaElectronica.Enabled = True

            Dim rowGenerados() As DataRow = dt.Select("Generado='S'")
            If (rowGenerados.Length > 0) Then
                If rowGenerados.Length < dt.Rows.Count Then
                    btnGenerarNominaElectronica.Enabled = True
                Else
                    btnGenerarNominaElectronica.Enabled = False
                End If
            ElseIf rowGenerados.Length = 0 Then
                btnGenerarNominaElectronica.Enabled = True
                btnTimbrarNomina.Enabled = False
            End If

            Dim rowTimbrado() As DataRow = dt.Select("Timbrado='S'")
            If (rowTimbrado.Length > 0) Then
                btnGenerarNominaElectronica.Enabled = False
                btnGenerarPDF.Enabled = True

                If rowGenerados.Length < dt.Rows.Count Then
                    btnTimbrarNomina.Enabled = False
                Else
                    If rowTimbrado.Length < dt.Rows.Count Then
                        btnTimbrarNomina.Enabled = True
                    Else
                        btnTimbrarNomina.Enabled = False
                    End If
                End If

            ElseIf rowTimbrado.Length = 0 Then
                If (rowGenerados.Length > 0) Then
                    btnTimbrarNomina.Enabled = True
                Else
                    btnTimbrarNomina.Enabled = False
                    btnGenerarPDF.Enabled = False
                End If
            End If

            Dim rowPdf() As DataRow = dt.Select("Pdf='S'")
            If (rowPdf.Length > 0) Then
                btnEnvioComprobantes.Enabled = True
                If rowPdf.Length < dt.Rows.Count Then
                    'btnGenerarPDF.Enabled = True
                Else
                    btnGenerarPDF.Enabled = False
                End If
            ElseIf rowPdf.Length = 0 Then
                'btnGenerarPDF.Enabled = True
                btnEnvioComprobantes.Enabled = False
            End If

        ElseIf dt.Rows.Count = 0 Then
            btnGenerarNominaElectronica.Enabled = False
            btnTimbrarNomina.Enabled = False
            btnGenerarPDF.Enabled = False
            btnEnvioComprobantes.Enabled = False
        End If
    End Sub
    Private Sub btnGenerarNominaElectronica_Click(sender As Object, e As EventArgs) Handles btnGenerarNominaElectronica.Click
        If cmbPeriodo.SelectedValue = 0 Then
            rwAlerta.RadAlert("Selecciona un periodo de pago", 330, 180, "Alerta", "", "")
        Else
            rwConfirm.RadConfirm("¿Está seguro de generar el cálculo de finiquitos?", "confirmCallbackFnGeneraNominaElectronica", 330, 180, Nothing, "Confirmar")
        End If
    End Sub
    Private Sub btnConfirmarGeneraNominaElectronica_Click(sender As Object, e As EventArgs) Handles btnConfirmarGeneraNominaElectronica.Click

        Call CargarVariablesGenerales()

        Dim dt As New DataTable
        Dim cNomina = New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.Tipo = "F"
        cNomina.TipoNomina = 1 'Semanal
        dt = cNomina.ConsultarEmpleadosNoGeneradosNominaEspecial()

        Dim RfcEmisor As String = ""
        Dim RfcCliente As String = ""
        Dim rutaEmpresa As String = ""

        If dt.Rows.Count > 0 Then

            Dim Total As Integer = dt.Rows.Count
            Dim progress As RadProgressContext = RadProgressContext.Current
            progress.Speed = "N/A"
            Dim i As Integer = 0
            For Each oDataRow In dt.Rows

                Dim dtEmisor As New DataTable
                cNomina = New Nomina()
                cNomina.IdEmpresa = Session("IdEmpresa")
                dtEmisor = cNomina.ConsultarDatosEmisor()

                If dtEmisor.Rows.Count > 0 Then
                    For Each oRow In dtEmisor.Rows
                        RfcEmisor = oRow("RFC")
                    Next
                End If

                Dim dtCliente As New DataTable
                cNomina = New Nomina()
                cNomina.Id = oDataRow("IdCliente")
                dtCliente = cNomina.ConsultarDatosCliente()

                If dtCliente.Rows.Count > 0 Then
                    For Each row In dtCliente.Rows
                        RfcCliente = row("RFC")
                    Next
                End If

                rutaEmpresa = Server.MapPath("~/XmlGenerados/").ToString & RfcEmisor.ToString & "/" & RfcCliente.ToString & "/S/" & IdEjercicio.ToString & "/F/" & cmbPeriodo.SelectedValue.ToString

                If Not Directory.Exists(rutaEmpresa) Then
                    Directory.CreateDirectory(rutaEmpresa)
                End If

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

                CrearCFDNominaEspecial(oDataRow("NoEmpleado"), rutaEmpresa)

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

            Call CargarGridEmpleados()

        ElseIf dt.Rows.Count = 0 Then
            rwAlerta.RadAlert("Esta nómina ya esta timbrada completamente!!!", 330, 180, "Alerta", "", "")
        End If
    End Sub
    Private Sub btnTimbrarNomina_Click(sender As Object, e As EventArgs) Handles btnTimbrarNomina.Click
        If cmbPeriodo.SelectedValue = 0 Then
            rwAlerta.RadAlert("Selecciona un periodo de pago", 330, 180, "Alerta", "", "")
        Else
            rwAlerta.RadConfirm("¿Está seguro de timbrar la nómina, una vez timbrada no podrá hacer modificaciones?", "confirmCallbackFnTimbrarNomina", 330, 180, Nothing, "Confirmar")
        End If
    End Sub
    Private Sub btnConfirmarTimbraNomina_Click(sender As Object, e As EventArgs) Handles btnConfirmarTimbraNomina.Click
        If cmbPeriodo.SelectedValue > 0 Then

            Call CargarVariablesGenerales()

            Dim dt As New DataTable
            Dim cNomina = New Nomina()
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 1 'Semanal
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.Tipo = "F"
            dt = cNomina.ConsultarEmpleadosTimbrar()

            Dim RfcEmisor As String = ""
            Dim RfcCliente As String = ""

            Dim FolioXmlTimbrado As String = ""
            Dim rutaEmpresa As String = ""

            If dt.Rows.Count > 0 Then

                Dim Total As Integer = dt.Rows.Count
                Dim progress As RadProgressContext = RadProgressContext.Current
                progress.Speed = "N/A"
                Dim i As Integer = 0

                For Each oDataRow In dt.Rows

                    i += 1

                    Dim dtEmisor As New DataTable
                    cNomina = New Nomina()
                    cNomina.IdEmpresa = Session("IdEmpresa")
                    dtEmisor = cNomina.ConsultarDatosEmisor()

                    If dtEmisor.Rows.Count > 0 Then
                        For Each oRow In dtEmisor.Rows
                            RfcEmisor = oRow("RFC")
                        Next
                    End If

                    Dim dtCliente As New DataTable
                    cNomina = New Nomina()
                    cNomina.Id = oDataRow("IdCliente")
                    dtCliente = cNomina.ConsultarDatosCliente()

                    If dtCliente.Rows.Count > 0 Then
                        For Each row In dtCliente.Rows
                            RfcCliente = row("RFC")
                        Next
                    End If

                    serie.Value = oDataRow("Serie")
                    folio.Value = oDataRow("Folio")

                    rutaEmpresa = Server.MapPath("~/XmlGenerados/").ToString & RfcEmisor.ToString & "/" & RfcCliente.ToString & "/S/" & IdEjercicio.ToString & "/F/" & cmbPeriodo.SelectedValue.ToString

                    If Not Directory.Exists(rutaEmpresa) Then
                        Directory.CreateDirectory(rutaEmpresa)
                    End If

                    FolioXml = rutaEmpresa & "/" & oDataRow("Serie").ToString & oDataRow("Folio").ToString & ".xml"

                    If Not File.Exists(FolioXml) Then
                        'Sellar Comprobante si no existe
                        TotalPercepciones = 0
                        TotalDeducciones = 0
                        NetoAPagar = 0
                        PercepcionesGravadas = 0
                        PercepcionesExentas = 0

                        Call CargarVariablesGenerales()

                        Call CargarPercepciones(oDataRow("NoEmpleado"))
                        Call CargarDeducciones(oDataRow("NoEmpleado"))
                        NetoAPagar = (TotalPercepciones - TotalDeducciones)

                        CrearCFDNominaEspecial(oDataRow("NoEmpleado"), rutaEmpresa)
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

                    CrearCFDNominaEspecial(oDataRow("NoEmpleado"), rutaEmpresa)
                    GrabarGeneracionXml(oDataRow("NoEmpleado"), oDataRow("Ejercicio"), oDataRow("TipoNomina"))

                    Try
                        Dim SIFEIUsuario As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIUsuario")
                        Dim SIFEIContrasena As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIContrasena")
                        Dim SIFEIIdEquipo As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIIdEquipo")
                        System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType) Or DirectCast(768, System.Net.SecurityProtocolType) Or DirectCast(192, System.Net.SecurityProtocolType) Or DirectCast(48, System.Net.SecurityProtocolType)

                        'Pruebas
                        Dim TimbreSifeiVersion33 As New SIFEIPruebasV33.SIFEIService()

                        'Producción
                        'Dim TimbreSifeiVersion33 As New SIFEI33.SIFEIService()
                        Call Comprimir()

                        Dim bytes() As Byte
                        bytes = TimbreSifeiVersion33.getCFDI(SIFEIUsuario, SIFEIContrasena, data, "", SIFEIIdEquipo)
                        Descomprimir(bytes, oDataRow("NoEmpleado"), oDataRow("RFC"), oDataRow("IdCliente"))
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

                        CrearCFDNominaEspecial(oDataRow("NoEmpleado"), rutaEmpresa)
                        GrabarGeneracionXml(oDataRow("NoEmpleado"), oDataRow("Ejercicio"), oDataRow("TipoNomina"))

                        Try
                            '
                            '   Vuelve a intentar timbrar el Comprobante Sellado
                            '
                            Dim SIFEIUsuario As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIUsuario")
                            Dim SIFEIContrasena As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIContrasena")
                            Dim SIFEIIdEquipo As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIIdEquipo")

                            'Pruebas
                            Dim TimbreSifeiVersion33 As New SIFEIPruebasV33.SIFEIService()

                            'Producción
                            'Dim TimbreSifeiVersion33 As New SIFEI33.SIFEIService()
                            Call Comprimir()

                            Dim bytes() As Byte
                            bytes = TimbreSifeiVersion33.getCFDI(SIFEIUsuario, SIFEIContrasena, data, "", SIFEIIdEquipo)
                            Descomprimir(bytes, oDataRow("NoEmpleado"), oDataRow("RFC"), oDataRow("IdCliente"))
                        Catch oExcep As SoapException
                            GrabarTimbrado(oDataRow("NoEmpleado"), "N", "")
                            Dim ErrorTimbrado = New ErrorTimbrado()
                            'ErrorTimbrado.IdEmpresa = IdEmpresa
                            ErrorTimbrado.Ejercicio = IdEjercicio
                            ErrorTimbrado.TipoNomina = 1 'Semanal
                            ErrorTimbrado.Periodo = cmbPeriodo.SelectedValue
                            ErrorTimbrado.NoEmpleado = oDataRow("NoEmpleado")
                            ErrorTimbrado.IdContrato = oDataRow("IdContrato")
                            ErrorTimbrado.Descripcion = ex.Detail.InnerText.ToString
                            ErrorTimbrado.IdUsuario = Session("usuarioid")
                            ErrorTimbrado.Tipo = "F"
                            ErrorTimbrado.GuadarError()
                        End Try
                    End Try

                    folio.Value = 0

                    progress.SecondaryTotal = Total
                    progress.SecondaryValue = i
                    progress.SecondaryPercent = Math.Round((i * 100 / Total), 0)

                    progress.CurrentOperationText = "Empleado " & i.ToString()

                    If Not Response.IsClientConnected Then
                        Exit For
                    End If

                    progress.TimeEstimated = (Total - i) * 100

                Next

                Call CargarGridEmpleados()

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
    Private Function Descomprimir(ByVal data5 As Byte(), ByVal NoEmpleado As Integer, ByVal RFC As String, ByVal IdCliente As Integer)

        Dim RfcEmisor As String = ""
        Dim RfcCliente As String = ""

        Dim dtEmisor As New DataTable
        Dim cNomina = New Nomina()
        cNomina = New Nomina()
        cNomina.IdEmpresa = Session("IdEmpresa")
        dtEmisor = cNomina.ConsultarDatosEmisor()

        If dtEmisor.Rows.Count > 0 Then
            For Each oDataRow In dtEmisor.Rows
                RfcEmisor = oDataRow("RFC")
            Next
        End If

        Dim dtCliente As New DataTable
        cNomina = New Nomina()
        cNomina.Id = IdCliente
        dtCliente = cNomina.ConsultarDatosCliente()

        If dtCliente.Rows.Count > 0 Then
            For Each oDataRow In dtCliente.Rows
                RfcCliente = oDataRow("RFC")
            Next
        End If

        Dim ms1 As New MemoryStream(data5)
        Dim zip1 As ZipFile = New ZipFile()
        zip1 = ZipFile.Read(ms1)

        Dim archivo As String = ""
        Dim DirectorioExtraccion As String = ""
        DirectorioExtraccion = Server.MapPath("~/XmlTimbrados/" & RfcEmisor & "/" & RfcCliente.ToString & "/S/").ToString & IdEjercicio.ToString & "/F/" & cmbPeriodo.SelectedValue.ToString & "/"

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

            Call GrabarTimbrado(NoEmpleado, "S", UUID)

            Dim cPeriodo As New Entities.Periodo()
            cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
            cPeriodo.ConsultarPeriodoID()

            FolioXmlTimbrado = DirectorioExtraccion & RFC.ToString & "_" & Format(CDate(cPeriodo.FechaInicial), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinal), "dd-MM-yyyy").ToString & "_" & UUID & ".xml"
            'FolioXmlTimbrado = DirectorioExtraccion & "/" & UUID.ToString & ".xml"

            If File.Exists(FolioXml) Then
                My.Computer.FileSystem.CopyFile(DirectorioExtraccion & "/" & archivo, FolioXmlTimbrado)
                File.Delete(Server.MapPath("~/XmlGenerados/").ToString & RfcEmisor.ToString & "/" & RfcCliente.ToString & "/S/" & IdEjercicio.ToString & "/F/" & cmbPeriodo.SelectedValue.ToString & "/" & serie.Value.ToString & folio.Value.ToString & ".xml")
            End If

        End If

    End Function
    Private Sub CargarPercepciones(ByVal NoEmpleado As Integer)

        Call CargarVariablesGenerales()

        Dim dt As New DataTable
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.TipoConcepto = "P"
        cNomina.Tipo = "F"
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

        Call CargarVariablesGenerales()

        Dim dt As New DataTable
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.TipoConcepto = "D"
        cNomina.Tipo = "F"
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
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.TipoConcepto = "D"
        cNomina.Tipo = "F"
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
    Private Sub AsignaSerieFolio(ByVal NoEmpleado As Integer)

        Call CargarVariablesGenerales()

        '
        '   Obtiene serie y folio
        '
        Dim dt As New DataTable()
        Dim cNomina = New Nomina()
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.Tipo = "F"
        cNomina.NoEmpleado = NoEmpleado
        dt = cNomina.AsignaSerieFolio()

        If dt.Rows.Count > 0 Then
            serie.Value = dt.Rows.Item(0)("serie").ToString()
            folio.Value = dt.Rows.Item(0)("folio")
        End If

    End Sub
    Private Sub IndentarNodo(ByVal Nodo As XmlNode, ByVal Nivel As Long)
        Nodo.AppendChild(m_xmlDOM.CreateTextNode(vbNewLine & New String(ControlChars.Tab, Nivel)))
    End Sub
    Private Function CrearNodo(ByVal nombre As String)
        If urlnomina = 0 Then
            CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, nombre, URI_SAT)
        Else
            CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, nombre, "http://www.sat.gob.mx/nomina12")
        End If
    End Function
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
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.Tipo = "F"
        dtReceptor = cNomina.ConsultarDatosEmpleadosGenerados()

        Dim Receptor As XmlElement

        If dtReceptor.Rows.Count > 0 Then
            For Each oDataRow In dtReceptor.Rows
                Receptor = CrearNodo("cfdi:Receptor")
                Receptor.SetAttribute("Rfc", oDataRow("RFC"))
                Receptor.SetAttribute("Nombre", oDataRow("NOMBRE"))
                Receptor.SetAttribute("UsoCFDI", "CN01") 'Por definir
                Receptor.SetAttribute("DomicilioFiscalReceptor", oDataRow("CP"))
                Receptor.SetAttribute("RegimenFiscalReceptor", "605")
                Nodo.AppendChild(Receptor)
            Next
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
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.Tipo = "F"
        dtEmpleado = cNomina.ConsultarDatosEmpleadosGenerados()

        Dim Complemento As XmlElement
        Dim Nomina As XmlElement
        Dim Percepciones As XmlElement
        Dim Percepcion As XmlElement
        Dim Deducciones As XmlElement
        Dim Deduccion As XmlElement
        Dim Emisor As XmlElement
        Dim Receptor As XmlElement
        Dim OtrosPagos As XmlElement
        Dim OtroPago As XmlElement
        Dim SubsidioAlEmpleo As XmlElement

        Dim TotalSueldos As Decimal = 0
        Dim TotalExento As Decimal = 0
        Dim TotalGravado As Decimal = 0
        Dim TotalOtrasDeducciones As Decimal = 0
        Dim TotalImpuestosRetenidos As Decimal = 0
        Dim TotalPercepciones As Decimal = 0
        Dim TotalDeducciones As Decimal = 0
        Dim TotalHorasExtra As Decimal = 0
        Dim TotalOtrosPagos As Decimal = 0
        Dim SubsidioCausado As Decimal = 0
        Dim TotalSeparacionIndemnizacion As Decimal = 0
        Dim ImporteExentoHorasExtra As Decimal = 0
        Dim ImporteGravadoHorasExtra As Decimal = 0
        Dim SubSidioEmpleo As Decimal = 0

        If dtEmpleado.Rows.Count > 0 Then
            For Each oDataRow In dtEmpleado.Rows

                Dim dt As New DataTable
                cNomina = New Nomina()
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 1 'Semanal
                cNomina.Periodo = cmbPeriodo.SelectedValue
                cNomina.TipoConcepto = "P"
                cNomina.Tipo = "F"
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
                    'If dt.Compute("SUM(Importe)", "CvoSAT='16' or CvoSAT='17' or CvoSAT='18'") IsNot DBNull.Value Then
                    '    TotalOtrosPagos = Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT='16' or CvoSAT='17' or CvoSAT='18'"))
                    'End If
                    If dt.Compute("SUM(Importe)", "CvoSAT='999'") IsNot DBNull.Value Then
                        TotalOtrosPagos = TotalOtrosPagos + MyRound(Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT='999'")))
                    End If
                    If dt.Compute("SUM(Importe)", "CvoSAT='002' AND OtroPagoBit=True") IsNot DBNull.Value Then
                        SubsidioCausado = SubsidioCausado + MyRound(Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT='002' AND OtroPagoBit=True")))
                    End If
                End If

                dt = New DataTable
                cNomina = New Nomina()
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 1 'Semanal
                cNomina.Periodo = cmbPeriodo.SelectedValue
                cNomina.TipoConcepto = "D"
                cNomina.Tipo = "F"
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

                Call ConsultarNumeroDeDiasPagados(NoEmpleado)

                Dim cPeriodo As New Entities.Periodo()
                cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
                cPeriodo.ConsultarPeriodoID()

                Dim FechaPago As Date = CDate(cPeriodo.FechaFinal)

                Nomina.SetAttribute("Version", "1.2")
                'Nomina.SetAttribute("TipoNomina", oDataRow("TipoNomina"))
                Nomina.SetAttribute("TipoNomina", "E")
                Nomina.SetAttribute("FechaPago", Mid(FechaPago, 7, 4) + "-" + Mid(FechaPago, 4, 2) + "-" + Mid(FechaPago, 1, 2))
                Nomina.SetAttribute("FechaInicialPago", Format(CDate(cPeriodo.FechaInicial), "yyyy-MM-dd"))
                Nomina.SetAttribute("FechaFinalPago", Format(CDate(cPeriodo.FechaFinal), "yyyy-MM-dd"))
                Nomina.SetAttribute("NumDiasPagados", Format(CDbl(NumeroDeDiasPagados), "0.000"))
                Nomina.SetAttribute("TotalPercepciones", Math.Round(TotalPercepciones + TotalHorasExtra, 2))
                If Math.Round(TotalDeducciones, 2) > 0 Then
                    Nomina.SetAttribute("TotalDeducciones", Math.Round(TotalDeducciones, 2))
                End If

                Nomina.SetAttribute("TotalOtrosPagos", Math.Round(TotalOtrosPagos, 2))

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
                cNomina.Periodo = cmbPeriodo.SelectedValue
                Antiguedad = cNomina.ConsultarAntiguedad(Convert.ToDateTime(oDataRow("FechaInicioRelLaboral")).ToString("yyyy-MM-dd"))

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
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 1 'Semanal
                cNomina.Periodo = cmbPeriodo.SelectedValue
                cNomina.TipoConcepto = "P"
                cNomina.Tipo = "F"
                cNomina.NoEmpleado = NoEmpleado
                dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                cNomina = Nothing
                '
                '   Percepciones
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
                '   Deducciones
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
                'cNomina.IdEmpresa = Session("clienteid").ToString
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 1 'Semanal
                cNomina.Periodo = cmbPeriodo.SelectedValue
                cNomina.TipoConcepto = "D"
                cNomina.Tipo = "F"
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
                '
                '   Otros pagos
                '
                dt = New DataTable
                cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = 1 'Semanal
                cNomina.Tipo = "F"
                cNomina.Periodo = cmbPeriodo.SelectedValue
                cNomina.TipoConcepto = "P"
                cNomina.NoEmpleado = NoEmpleado
                dt = cNomina.ConsultarOtrosPagos()
                cNomina = Nothing

                Dim SubsidioCausadoBit As Boolean = False
                OtrosPagos = CrearNodo("nomina12:OtrosPagos")

                If dt.Rows.Count > 0 Then

                    For i = 0 To dt.Rows.Count - 1
                        If dt.Rows.Item(i)("CvoSAT").ToString = "999" Then '16 Otros (Cátalogo Anterior)
                            OtroPago = CrearNodo("nomina12:OtroPago")
                            OtroPago.SetAttribute("TipoOtroPago", "999")
                            OtroPago.SetAttribute("Clave", String.Format("{0:000}", dt.Rows.Item(i)("CvoConcepto")))
                            OtroPago.SetAttribute("Concepto", dt.Rows.Item(i)("Concepto"))
                            OtroPago.SetAttribute("Importe", MyRound(Convert.ToDecimal(dt.Rows.Item(i)("Importe"))))
                            OtrosPagos.AppendChild(OtroPago)
                        End If

                        If (dt.Rows.Item(i)("CvoSAT").ToString = "002" And CBool(dt.Rows.Item(i)("OtroPagoBit")) = True) And SubsidioCausado > 0 Then ' Otros (Subsidio para el empleo)

                            SubsidioCausadoBit = True

                            OtroPago = CrearNodo("nomina12:OtroPago")
                            OtroPago.SetAttribute("TipoOtroPago", dt.Rows.Item(i)("CvoSAT").ToString)
                            OtroPago.SetAttribute("Clave", String.Format("{0:000}", dt.Rows.Item(i)("CvoConcepto")))
                            OtroPago.SetAttribute("Concepto", dt.Rows.Item(i)("Concepto").ToString)
                            OtroPago.SetAttribute("Importe", "0.00")

                            SubsidioAlEmpleo = CrearNodo("nomina12:SubsidioAlEmpleo")
                            SubsidioAlEmpleo.SetAttribute("SubsidioCausado", MyRound(Convert.ToDecimal(dt.Rows.Item(i)("Importe"))))
                            OtroPago.AppendChild(SubsidioAlEmpleo)
                            OtrosPagos.AppendChild(OtroPago)
                        End If
                    Next

                    Nomina.AppendChild(OtrosPagos)
                Else

                    SubsidioCausadoBit = True

                    OtroPago = CrearNodo("nomina12:OtroPago")
                    OtroPago.SetAttribute("TipoOtroPago", "002")
                    OtroPago.SetAttribute("Clave", "054")
                    OtroPago.SetAttribute("Concepto", "SUBSIDIO AL EMPLEO")
                    OtroPago.SetAttribute("Importe", "0.00")

                    SubsidioAlEmpleo = CrearNodo("nomina12:SubsidioAlEmpleo")
                    SubsidioAlEmpleo.SetAttribute("SubsidioCausado", "0.00")
                    OtroPago.AppendChild(SubsidioAlEmpleo)
                    OtrosPagos.AppendChild(OtroPago)
                    Nomina.AppendChild(OtrosPagos)
                End If
                '
                If SubsidioCausadoBit = False Then
                    OtroPago = CrearNodo("nomina12:OtroPago")
                    OtroPago.SetAttribute("TipoOtroPago", "002")
                    OtroPago.SetAttribute("Clave", "054")
                    OtroPago.SetAttribute("Concepto", "SUBSIDIO AL EMPLEO")
                    OtroPago.SetAttribute("Importe", "0.00")

                    SubsidioAlEmpleo = CrearNodo("nomina12:SubsidioAlEmpleo")
                    SubsidioAlEmpleo.SetAttribute("SubsidioCausado", "0.00")
                    OtroPago.AppendChild(SubsidioAlEmpleo)
                    OtrosPagos.AppendChild(OtroPago)
                    Nomina.AppendChild(OtrosPagos)
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
    Private Sub ObtenerUnidad(ByVal NoEmpleado As Integer, ByVal CvoConcepto As String)
        Try

            Call CargarVariablesGenerales()

            Dim dt As DataTable
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = Session("clienteid").ToString
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 1 'Semanal
            cNomina.Periodo = cmbPeriodo.SelectedValue
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
            'cNomina.IdEmpresa = Session("clienteid").ToString
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 1 'Semanal
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.TipoConcepto = "P"
            cNomina.Tipo = "F"
            cNomina.NoEmpleado = NoEmpleado
            dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
            cNomina = Nothing

            If dt.Rows.Count > 0 Then
                If dt.Compute("Sum(Importe)", "CvoConcepto=85") IsNot DBNull.Value Then
                    DiasCuotaPeriodo = dt.Compute("Sum(UNIDAD)", "CvoConcepto=85")
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
    Private Sub GrabarGeneracionXml(ByVal NoEmpleado As Integer, ByVal IdEjercicio As Integer, ByVal TipoNomina As Integer)
        Dim cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = TipoNomina
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.Tipo = "F"
        cNomina.Generado = "S"
        cNomina.ActualizarEstatusGeneradoNominaEspecial()
    End Sub
    Private Sub GrabarTimbrado(ByVal NoEmpleado As Integer, ByVal Timbrado As String, ByVal UUID As String)
        Call CargarVariablesGenerales()

        Dim cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.Timbrado = Timbrado
        cNomina.UUID = UUID
        cNomina.Tipo = "F"
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
    Public Function GetXmlAttribute(ByVal url As String, campo As String, nodo As String) As String
        '
        '   Obtiene datos del xml
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
                        If FlujoReader.Name.ToString.ToUpper = nodo.ToUpper Then
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
    Private Sub grdEmpleados_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles grdEmpleados.ItemCommand
        Select Case e.CommandName
            Case "cmdXML"
                Dim NoEmpleado As Integer = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("NoEmpleado"))
                Dim RFC As String = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("RFC"))
                Dim IdCliente As String = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("IdCliente"))
                Call DownloadXML(NoEmpleado, RFC, e.CommandArgument, IdCliente)
            Case "cmdPDFTimbrado"
                Dim NoEmpleado As Integer = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("NoEmpleado"))
                Dim RFC As String = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("RFC"))
                Dim IdCliente As String = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("IdCliente"))
                Call DownloadPDFTimbrado(NoEmpleado, RFC, e.CommandArgument, IdCliente)
            Case "cmdSend"
                Dim NoEmpleado As Integer = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("NoEmpleado"))
                Dim RFC As String = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("RFC"))
                Dim IdCliente As String = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("IdCliente"))
                Call EnviaEmail(NoEmpleado, RFC, e.CommandArgument, IdCliente)
                Call CargarGridEmpleados()
            Case "cmdFiniquito"
                Dim commandArgs As String() = e.CommandArgument.ToString().Split(New Char() {"|"c})
                Dim NoEmpleado As Integer = CInt(commandArgs(0))
                Dim IdMovimiento As String = CInt(commandArgs(1))
                Dim IdCliente As String = CInt(commandArgs(2))
                Dim Periodo As String = CInt(commandArgs(3))

                Call CargarVariablesGenerales()

                Dim RfcEmisor As String = ""
                Dim RfcCliente As String = ""

                Dim dtEmisor As New DataTable
                Dim cNomina = New Nomina()
                cNomina = New Nomina()
                cNomina.IdEmpresa = Session("IdEmpresa")
                dtEmisor = cNomina.ConsultarDatosEmisor()

                If dtEmisor.Rows.Count > 0 Then
                    For Each oDataRow In dtEmisor.Rows
                        RfcEmisor = oDataRow("RFC")
                    Next
                End If

                Dim dtCliente As New DataTable
                cNomina = New Nomina()
                cNomina.Id = IdCliente
                dtCliente = cNomina.ConsultarDatosCliente()

                If dtCliente.Rows.Count > 0 Then
                    For Each oDataRow In dtCliente.Rows
                        RfcCliente = oDataRow("RFC")
                    Next
                End If

                Dim DirectorioExtraccion As String = ""
                DirectorioExtraccion = Server.MapPath("~/PDF/").ToString & RfcEmisor.ToString & "/" & RfcCliente.ToString & "/S/" & IdEjercicio.ToString & "/F/" & Periodo.ToString

                If Not Directory.Exists(DirectorioExtraccion) Then
                    Directory.CreateDirectory(DirectorioExtraccion)
                End If

                Dim FilePath = Server.MapPath("~/PDF/").ToString & RfcEmisor.ToString & "/" & RfcCliente.ToString & "/S/" & IdEjercicio.ToString & "/F/" & Periodo.ToString & "/" & "F" & String.Format("{0:00}", NoEmpleado) & ".pdf"

                If Not File.Exists(FilePath) Then
                    GuardaPDF(GeneraPDFFiniquito(NoEmpleado, IdMovimiento, IdCliente), FilePath)
                End If

                If File.Exists(FilePath) Then
                    Dim FileName As String = Path.GetFileName(FilePath)
                    Response.Clear()
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
                    Response.Flush()
                    Response.WriteFile(FilePath)
                    Response.End()
                End If
            Case "cmdRenuncia"
                Dim commandArgs As String() = e.CommandArgument.ToString().Split(New Char() {"|"c})
                Dim NoEmpleado As Integer = CInt(commandArgs(0))
                Dim IdMovimiento As String = CInt(commandArgs(1))
                Dim IdCliente As String = CInt(commandArgs(2))
                Dim Periodo As String = CInt(commandArgs(3))

                Call CargarVariablesGenerales()

                Dim RfcEmisor As String = ""
                Dim RfcCliente As String = ""

                Dim dtEmisor As New DataTable
                Dim cNomina = New Nomina()
                cNomina = New Nomina()
                cNomina.IdEmpresa = Session("IdEmpresa")
                dtEmisor = cNomina.ConsultarDatosEmisor()

                If dtEmisor.Rows.Count > 0 Then
                    For Each oDataRow In dtEmisor.Rows
                        RfcEmisor = oDataRow("RFC")
                    Next
                End If

                Dim dtCliente As New DataTable
                cNomina = New Nomina()
                cNomina.Id = IdCliente
                dtCliente = cNomina.ConsultarDatosCliente()

                If dtCliente.Rows.Count > 0 Then
                    For Each oDataRow In dtCliente.Rows
                        RfcCliente = oDataRow("RFC")
                    Next
                End If

                Dim DirectorioExtraccion As String = ""
                DirectorioExtraccion = Server.MapPath("~/PDF/").ToString & RfcEmisor.ToString & "/" & RfcCliente.ToString & "/S/" & IdEjercicio.ToString & "/F/" & Periodo.ToString

                If Not Directory.Exists(DirectorioExtraccion) Then
                    Directory.CreateDirectory(DirectorioExtraccion)
                End If

                Dim FilePath = Server.MapPath("~/PDF/").ToString & RfcEmisor.ToString & "/" & RfcCliente.ToString & "/S/" & IdEjercicio.ToString & "/F/" & Periodo.ToString & "/" & "R" & String.Format("{0:00}", NoEmpleado) & ".pdf"

                If Not File.Exists(FilePath) Then
                    GuardaPDF(GeneraPDFRenuncia(NoEmpleado, IdMovimiento), FilePath)
                End If

                If File.Exists(FilePath) Then
                    Dim FileName As String = Path.GetFileName(FilePath)
                    Response.Clear()
                    Response.ContentType = "application/octet-stream"
                    Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
                    Response.Flush()
                    Response.WriteFile(FilePath)
                    Response.End()
                End If
        End Select
    End Sub
    Private Sub DownloadPDFTimbrado(ByVal NoEmpleado As Integer, ByVal RFC As String, ByVal UUID As String, ByVal IdCliente As Integer)

        Call CargarVariablesGenerales()

        Dim RfcEmisor As String = ""
        Dim RfcCliente As String = ""

        Dim dtEmisor As New DataTable
        Dim cNomina = New Nomina()
        cNomina = New Nomina()
        cNomina.IdEmpresa = Session("IdEmpresa")
        dtEmisor = cNomina.ConsultarDatosEmisor()

        If dtEmisor.Rows.Count > 0 Then
            For Each oDataRow In dtEmisor.Rows
                RfcEmisor = oDataRow("RFC")
            Next
        End If

        Dim dtCliente As New DataTable
        cNomina = New Nomina()
        cNomina.Id = IdCliente
        dtCliente = cNomina.ConsultarDatosCliente()

        If dtCliente.Rows.Count > 0 Then
            For Each oDataRow In dtCliente.Rows
                RfcCliente = oDataRow("RFC")
            Next
        End If

        Dim rutaEmpresa As String = ""
        rutaEmpresa = Server.MapPath("~/PDF/").ToString & RfcEmisor.ToString & "/" & RfcCliente.ToString & "/S/" & IdEjercicio.ToString & "/F/" & cmbPeriodo.SelectedValue.ToString

        If Not Directory.Exists(rutaEmpresa) Then
            Directory.CreateDirectory(rutaEmpresa)
        End If

        Dim cPeriodo As New Entities.Periodo()
        cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
        cPeriodo.ConsultarPeriodoID()

        Dim FilePath = rutaEmpresa & "/" & RFC.ToString & "_" & Format(CDate(cPeriodo.FechaInicial), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinal), "dd-MM-yyyy").ToString & "_" & UUID & ".pdf"

        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        Else
            Call GuardaPDF(GeneraPDF(NoEmpleado, UUID, IdCliente), FilePath)
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        End If
    End Sub
    Private Sub DownloadXML(ByVal NoEmpleado As Integer, ByVal RFC As String, ByVal UUID As String, ByVal IdCliente As Integer)

        Call CargarVariablesGenerales()

        Dim RfcEmisor As String = ""
        Dim RfcCliente As String = ""

        Dim dtEmisor As New DataTable
        Dim cNomina = New Nomina()
        cNomina.IdEmpresa = Session("IdEmpresa")
        dtEmisor = cNomina.ConsultarDatosEmisor()

        If dtEmisor.Rows.Count > 0 Then
            For Each oDataRow In dtEmisor.Rows
                RfcEmisor = oDataRow("RFC")
            Next
        End If

        Dim dtCliente As New DataTable
        cNomina = New Nomina()
        cNomina.Id = IdCliente
        dtCliente = cNomina.ConsultarDatosCliente()

        If dtCliente.Rows.Count > 0 Then
            For Each oDataRow In dtCliente.Rows
                RfcCliente = oDataRow("RFC")
            Next
        End If

        Dim cPeriodo As New Entities.Periodo()
        cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
        cPeriodo.ConsultarPeriodoID()

        Dim rutaEmpresa As String = ""
        rutaEmpresa = Server.MapPath("~/XmlTimbrados/" & RfcEmisor & "/" & RfcCliente.ToString & "/S/").ToString & IdEjercicio.ToString & "/F/" & cmbPeriodo.SelectedValue.ToString & "/"

        If Not Directory.Exists(rutaEmpresa) Then
            Directory.CreateDirectory(rutaEmpresa)
        End If

        Dim FilePath As String = ""
        FilePath = rutaEmpresa & RFC & "_" & Format(CDate(cPeriodo.FechaInicial), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinal), "dd-MM-yyyy").ToString & "_" & UUID & ".xml"

        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        Else
            FilePath = rutaEmpresa & "/" & RFC.ToString & "_" & Format(CDate(cPeriodo.FechaInicial), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinal), "dd-MM-yyyy").ToString & "_" & UUID & ".xml"
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
    Private Function MyRound(Importe As Decimal) As Decimal
        Dim r As Decimal = Math.Ceiling(Importe * 100D) / 100D
        Return r
    End Function
    Private Sub GuardaPDF(ByVal report As Telerik.Reporting.Report, ByVal fileName As String)
        Dim reportProcessor As New Telerik.Reporting.Processing.ReportProcessor()
        Dim result As RenderingResult = reportProcessor.RenderReport("PDF", report, Nothing)
        Using fs As New FileStream(fileName, FileMode.Create)
            fs.Write(result.DocumentBytes, 0, result.DocumentBytes.Length)
        End Using
    End Sub
    Private Function GeneraPDF(ByVal NoEmpleado As Integer, ByVal UUID As String, ByVal IdCliente As Integer) As Telerik.Reporting.Report

        Call CargarVariablesGenerales()

        Dim Folio As String = ""
        Dim Version As String = ""

        Dim CantidadTexto As String = ""
        Dim emp_regimen_contratacion As String = ""
        Dim emp_horas_extra_dobles As String = ""
        Dim emp_horas_extra_triples As String = ""
        Dim emp_nombre As String = ""

        Dim Percepciones As Decimal = 0
        Dim Deducciones As Decimal = 0
        Dim Neto As Decimal = 0

        Dim plantillaid As Integer = 1
        Dim regimen_fiscal As String = ""
        Dim lugar_expedicion As String = ""
        Dim logo As String = ""

        Dim ObjData As New DataControl(0)
        Dim ds As New DataSet
        ds = ObjData.FillDataSet("exec pCliente @cmd=3, @clienteid='" & Session("IdEmpresa") & "'")

        If ds.Tables.Count > 0 Then
            For Each row As DataRow In ds.Tables(0).Rows
                lugar_expedicion = "C. P." & row("fac_cp") & " - " & row("fac_municipio") & ", " & row("fac_pais")
                regimen_fiscal = row("regimen_fiscal")
                plantillaid = row("plantillaid")
                logo = row("logo")
            Next
        End If

        Dim reporte As New Formatos.formato_nomina

        Dim dt As New DataTable
        Dim cNomina As New Entities.Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Periodo = cmbPeriodo.SelectedValue
        dt = cNomina.ConsultarDatosPDF()

        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                Try
                    emp_regimen_contratacion = row("emp_regimen_contratacion").ToString
                    emp_horas_extra_dobles = row("emp_horas_extra_dobles")
                    emp_horas_extra_triples = row("emp_horas_extra_triples")
                    emp_nombre = row("emp_nombre")

                    cNomina = New Nomina()
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 1 'Semanal
                    cNomina.Periodo = cmbPeriodo.SelectedValue
                    cNomina.TipoConcepto = "P"
                    cNomina.Tipo = "F"
                    cNomina.NoEmpleado = NoEmpleado
                    dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                    cNomina = Nothing

                    If dt.Rows.Count > 0 Then
                        Percepciones = Math.Round(dt.Compute("SUM(Importe)", ""), 6)
                    End If

                    cNomina = New Nomina()
                    cNomina.Ejercicio = IdEjercicio
                    cNomina.TipoNomina = 1 'Semanal
                    cNomina.Periodo = cmbPeriodo.SelectedValue
                    cNomina.TipoConcepto = "D"
                    cNomina.Tipo = "F"
                    cNomina.NoEmpleado = NoEmpleado
                    dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                    cNomina = Nothing

                    If dt.Rows.Count > 0 Then
                        Deducciones = Math.Round(dt.Compute("SUM(Importe)", ""), 6)
                    End If

                    Neto = Percepciones - Deducciones

                    Dim largo = Len(CStr(Format(CDbl(Neto), "#,###.00")))
                    Dim decimales = Mid(CStr(Format(CDbl(Neto), "#,###.00")), largo - 2)

                    Dim RfcEmisor As String = ""
                    Dim RfcCliente As String = ""

                    Dim dtEmisor As New DataTable
                    cNomina = New Nomina()
                    cNomina.IdEmpresa = Session("IdEmpresa")
                    dtEmisor = cNomina.ConsultarDatosEmisor()

                    If dtEmisor.Rows.Count > 0 Then
                        For Each oDataRow In dtEmisor.Rows
                            RfcEmisor = oDataRow("RFC")
                        Next
                    End If

                    Dim dtCliente As New DataTable
                    cNomina = New Nomina()
                    cNomina.Id = IdCliente
                    dtCliente = cNomina.ConsultarDatosCliente()

                    If dtCliente.Rows.Count > 0 Then
                        For Each oDataRow In dtCliente.Rows
                            RfcCliente = oDataRow("RFC")
                        Next
                    End If

                    Dim cPeriodo As New Entities.Periodo()
                    cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
                    cPeriodo.ConsultarPeriodoID()

                    Dim rutaEmpresa As String = ""
                    rutaEmpresa = Server.MapPath("~/XmlTimbrados/" & RfcEmisor & "/" & RfcCliente.ToString & "/S/").ToString & IdEjercicio.ToString & "/F/" & cmbPeriodo.SelectedValue.ToString & "/"
                    FolioXml = rutaEmpresa & row("emp_rfc") & "_" & Format(CDate(cPeriodo.FechaInicial), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinal), "dd-MM-yyyy").ToString & "_" & UUID & ".xml"

                    Folio = GetXmlAttribute(FolioXml, "folio", "cfdi:Comprobante").ToString

                    reporte.ReportParameters("plantillaId").Value = plantillaid
                    reporte.ReportParameters("NoEmpleado").Value = NoEmpleado
                    reporte.ReportParameters("Ejercicio").Value = IdEjercicio
                    reporte.ReportParameters("TipoNomina").Value = 1 'Semanal
                    reporte.ReportParameters("Periodo").Value = cmbPeriodo.SelectedValue
                    reporte.ReportParameters("Tipo").Value = "F"

                    CantidadTexto = "( " + Num2Text(Neto - decimales) & " pesos " & Mid(decimales, Len(decimales) - 1) & "/100 M.N. )"

                    Version = GetXmlAttribute(FolioXml, "Version", "cfdi:Comprobante")

                    reporte.ReportParameters("conn").Value = ConfigurationManager.ConnectionStrings("conn").ConnectionString
                    reporte.ReportParameters("txtNoNomina").Value = "Nómina No. " & Folio.ToString
                    reporte.ReportParameters("txtTipoNomina").Value = GetXmlAttribute(FolioXml, "TipoNomina", "nomina12:Nomina").ToString & " - Extraordinaria"
                    reporte.ReportParameters("txtTipoContrato").Value = GetXmlAttribute(FolioXml, "TipoContrato", "nomina12:Receptor").ToString & " - " & row("TipoContrato").ToString
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

                    reporte.ReportParameters("txtNoPeriodoPago").Value = cmbPeriodo.SelectedValue.ToString
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

                    'Try
                    '    reporte.ReportParameters("txtEmpleadoAntiguedad").Value = GetXmlAttribute(FolioXml, "Antigüedad", "nomina12:Receptor").ToString
                    'Catch ex As Exception
                    '    reporte.ReportParameters("txtEmpleadoAntiguedad").Value = ""
                    'End Try

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

                    Dim FilePath = Server.MapPath("~/CBB/S/F/" & UUID.ToString & ".png")
                    If Not File.Exists(FilePath) Then
                        CodigoBidimensional(FolioXml)
                    End If

                    reporte.ReportParameters("paramImgCBB").Value = Server.MapPath("~/CBB/S/F/" & UUID.ToString & ".png")
                    reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/logos/" & logo)
                    reporte.ReportParameters("txtSelloDigitalCFDI").Value = GetXmlAttribute(FolioXml, "Sello", "cfdi:Comprobante")
                    reporte.ReportParameters("txtSelloDigitalSAT").Value = GetXmlAttribute(FolioXml, "SelloSAT", "tfd:TimbreFiscalDigital")
                    reporte.ReportParameters("txtFechaCertificacion").Value = GetXmlAttribute(FolioXml, "FechaTimbrado", "tfd:TimbreFiscalDigital")
                    reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString.ToUpper
                    reporte.ReportParameters("txtCadenaOriginal").Value = CadenaOriginalComplemento(FolioXml)

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

        If Version = "3.2" Then
            CadenaOriginalTimbre = "||" & VersionTimbre & "|" & UUID & "|" & FechaTimbrado & "|" & SelloCFD & "|" & NoCertificadoSAT & "||"
        ElseIf Version = "3.3" Then
            CadenaOriginalTimbre = "||" & VersionTimbre & "|" & UUID & "|" & FechaTimbrado & "|" & RfcProvCertif & "|" & SelloCFD & "|" & NoCertificadoSAT & "||"
        End If

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
        Total = Format(Convert.ToDouble(Total), "0000000000.000000")
        '
        '   Analizar el fichero y presentar cada nodo
        '
        FinalSelloDigitalEmisor = Mid(SelloCFD, (Len(SelloCFD) - 7))
        CadenaCodigoBidimensional = "https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx" & "?id=" & UUID & "&re=" & RFCEmisor & "&rr=" & RFCReceptor & "&tt=" & Total.ToString & "&fe=" & FinalSelloDigitalEmisor
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

        If Not Directory.Exists(Server.MapPath("~/CBB/S/F/")) Then
            Directory.CreateDirectory(Server.MapPath("~/CBB/S/F/"))
        End If

        Dim CBidimensional As Drawing.Image
        CBidimensional = qrCodeEncoder.Encode(CadenaCodigoBidimensional, System.Text.Encoding.UTF8)
        CBidimensional.Save(Server.MapPath("~/CBB/S/F/") & UUID & ".png", System.Drawing.Imaging.ImageFormat.Png)
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
    Private Sub btnGenerarPDF_Click(sender As Object, e As EventArgs) Handles btnGenerarPDF.Click
        If cmbPeriodo.SelectedValue > 0 Then
            rwConfirm.RadConfirm("¿Está seguro de generar los comprobantes pdf?", "confirmCallbackFnGeneraPDF", 330, 180, Nothing, "Confirmar")
        Else
            rwAlerta.RadAlert("Seleccione un periodo.", 330, 180, "Alerta", "", "")
        End If
    End Sub
    Private Sub btnConfirmarGeneraPDF_Click(sender As Object, e As EventArgs) Handles btnConfirmarGeneraPDF.Click
        If cmbPeriodo.SelectedValue > 0 Then

            Call CargarVariablesGenerales()

            Dim dt As New DataTable

            Dim cNomina As New Entities.Nomina()
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 1 'Semanal
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.Tipo = "F"
            dt = cNomina.ConsultarEmpleadosGenerados()

            If dt.Rows.Count > 0 Then

                Dim Total As Integer = dt.Rows.Count
                Dim progress As RadProgressContext = RadProgressContext.Current
                progress.Speed = "N/A"
                Dim i As Integer = 0

                Dim RfcEmisor As String = ""
                Dim RfcCliente As String = ""

                For Each row As DataRow In dt.Rows

                    i += 1

                    Dim dtEmisor As New DataTable
                    cNomina = New Entities.Nomina()
                    cNomina.IdEmpresa = Session("IdEmpresa")
                    dtEmisor = cNomina.ConsultarDatosEmisor()

                    If dtEmisor.Rows.Count > 0 Then
                        For Each oDataRow In dtEmisor.Rows
                            RfcEmisor = oDataRow("RFC")
                        Next
                    End If

                    Dim dtCliente As New DataTable
                    cNomina = New Nomina()
                    cNomina.Id = row("IdCliente")
                    dtCliente = cNomina.ConsultarDatosCliente()

                    If dtCliente.Rows.Count > 0 Then
                        For Each oDataRow In dtCliente.Rows
                            RfcCliente = oDataRow("RFC")
                        Next
                    End If

                    If row("UUID").ToString.Length > 0 Then

                        Dim rutaEmpresa As String = ""
                        rutaEmpresa = Server.MapPath("~/PDF/").ToString & RfcEmisor.ToString & "/" & RfcCliente.ToString & "/S/" & IdEjercicio.ToString & "/F/" & cmbPeriodo.SelectedValue.ToString

                        If Not Directory.Exists(rutaEmpresa) Then
                            Directory.CreateDirectory(rutaEmpresa)
                        End If

                        Dim cPeriodo As New Entities.Periodo()
                        cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
                        cPeriodo.ConsultarPeriodoID()

                        Dim FilePath As String = ""
                        FilePath = rutaEmpresa & "/" & row("RFC").ToString & "_" & Format(CDate(cPeriodo.FechaInicial), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinal), "dd-MM-yyyy").ToString & "_" & row("UUID") & ".pdf"

                        If Not File.Exists(FilePath) Then
                            GuardaPDF(GeneraPDF(CInt(row("NoEmpleado")), row("UUID"), CInt(row("IdCliente"))), FilePath)
                        Else
                            GrabarPDF(CInt(row("NoEmpleado")), "S")
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

                Call CargarGridEmpleados()

            End If

        Else
            rwAlerta.RadAlert("Seleccione un periodo.", 330, 180, "Alerta", "", "")
        End If
    End Sub
    Private Sub btnEnvioComprobantes_Click(sender As Object, e As EventArgs) Handles btnEnvioComprobantes.Click
        If cmbPeriodo.SelectedValue > 0 Then
            rwConfirm.RadConfirm("¿Está seguro de enviar TODOS los comprobantes generados?", "confirmCallbackFnEnvioPDF", 330, 180, Nothing, "Confirmar")
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
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 1 'Semanal
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.Tipo = "F"
            dt = cNomina.ConsultarEmpleadosGenerados()

            If dt.Rows.Count > 0 Then

                Dim Total As Integer = dt.Rows.Count
                Dim progress As RadProgressContext = RadProgressContext.Current
                progress.Speed = "N/A"
                Dim i As Integer = 0

                Dim RfcEmisor As String = ""
                Dim RfcCliente As String = ""

                For Each row In dt.Rows

                    i += 1

                    Dim dtEmisor As New DataTable
                    cNomina = New Entities.Nomina()
                    cNomina.IdEmpresa = Session("IdEmpresa")
                    dtEmisor = cNomina.ConsultarDatosEmisor()

                    If dtEmisor.Rows.Count > 0 Then
                        For Each oDataRow In dtEmisor.Rows
                            RfcEmisor = oDataRow("RFC")
                        Next
                    End If

                    Dim dtCliente As New DataTable
                    cNomina = New Nomina()
                    cNomina.Id = row("IdCliente")
                    dtCliente = cNomina.ConsultarDatosCliente()

                    If dtCliente.Rows.Count > 0 Then
                        For Each oDataRow In dtCliente.Rows
                            RfcCliente = oDataRow("RFC")
                        Next
                    End If

                    Dim rutaEmpresa As String = ""
                    rutaEmpresa = Server.MapPath("~/XmlTimbrados/" & RfcEmisor & "/" & RfcCliente.ToString & "/S/").ToString & IdEjercicio.ToString & "/F/" & cmbPeriodo.SelectedValue.ToString & "/"

                    If Not Directory.Exists(rutaEmpresa) Then
                        Directory.CreateDirectory(rutaEmpresa)
                    End If

                    Dim cPeriodo As New Entities.Periodo()
                    cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
                    cPeriodo.ConsultarPeriodoID()

                    Dim FilePathXML = rutaEmpresa & row("RFC").ToString & "_" & Format(CDate(cPeriodo.FechaInicial), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinal), "dd-MM-yyyy").ToString & "_" & row("UUID").ToString & ".xml"

                    rutaEmpresa = Server.MapPath("~/PDF/" & RfcEmisor & "/" & RfcCliente.ToString & "/S/").ToString & IdEjercicio.ToString & "/F/" & cmbPeriodo.SelectedValue.ToString & "/"
                    Dim FilePathPDF = rutaEmpresa & row("RFC").ToString & "_" & Format(CDate(cPeriodo.FechaInicial), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinal), "dd-MM-yyyy").ToString & "_" & row("UUID").ToString & ".pdf"

                    If Not File.Exists(FilePathPDF) Then
                        GuardaPDF(GeneraPDF(CInt(row("NoEmpleado")), row("UUID"), CInt(row("IdCliente"))), FilePathPDF)
                    End If
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
                    cConfiguracion.IdCliente = Session("clienteid")
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
                        If ValidarEmail(splitTo.Trim()) Then
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
                            mensaje = "Adjunto a este correo estamos enviándole el comprobante XML y PDF de nómina correspondiente al finiquito laboral. Cualquier duda o comentario,  escríbenos a " & email_from & vbCrLf & vbCrLf
                            mensaje += "Atentamente." & vbCrLf
                            mensaje += "Departamento Nóminas" & vbCrLf
                            'mensaje += razonsocial.ToString.ToUpper & vbCrLf

                            'objMM.From = New MailAddress(Session("email").ToString, razonsocial)
                            objMM.From = New MailAddress(email_from, "Departamento Nóminas")
                            objMM.IsBodyHtml = False
                            objMM.Priority = MailPriority.Normal
                            objMM.Subject = razonsocial & " - Recibo de Nómina Finiquito"
                            objMM.Body = mensaje
                            '
                            '   Agrega anexos
                            '
                            Dim AttachXML As Net.Mail.Attachment
                            Dim AttachPDF As Net.Mail.Attachment
                            AttachXML = New Net.Mail.Attachment(FilePathXML.Replace("/", "\"))
                            AttachPDF = New Net.Mail.Attachment(FilePathPDF.Replace("/", "\"))
                            objMM.Attachments.Add(AttachXML)
                            objMM.Attachments.Add(AttachPDF)
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

            Call CargarGridEmpleados()

        End If
    End Sub
    Public Shared Function ValidarEmail(ByVal email As String) As Boolean
        'Dim expresion As String = "/w+([-+.']/w+)*@/w+([-.]/w+)*/./w+([-.]/w+)*"
        Dim expresion As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
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
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.Tipo = "F"
        cNomina.Enviado = Enviado
        cNomina.ActualizarEstatusEnviadoNominaEspecial()
    End Sub
    Private Sub GrabarPDF(ByVal NoEmpleado As Integer, ByVal Pdf As String)

        Call CargarVariablesGenerales()

        Dim cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Periodo = cmbPeriodo.SelectedValue
        cNomina.Tipo = "F"
        cNomina.Pdf = Pdf
        cNomina.ActualizarEstatusPfNominaEspecial()
    End Sub
    Private Sub EnviaEmail(ByVal NoEmpleado As Integer, ByVal RFC As String, ByVal UUID As String, ByVal IdCliente As Integer)
        If cmbPeriodo.SelectedValue > 0 Then

            Dim validos As String = ""
            Dim novalidos As String = ""

            Call CargarVariablesGenerales()

            Dim dt As New DataTable
            Dim cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
            cNomina.IdCliente = IdCliente
            cNomina.NoEmpleado = NoEmpleado
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = 1 'Semanal
            cNomina.Periodo = cmbPeriodo.SelectedValue
            cNomina.NoEmpleado = NoEmpleado
            cNomina.Tipo = "F"
            dt = cNomina.ConsultarDatosEnvioPDF()

            If dt.Rows.Count > 0 Then

                Dim RfcEmisor As String = ""
                Dim RfcCliente As String = ""

                For Each row In dt.Rows
                    Dim dtEmisor As New DataTable
                    cNomina = New Entities.Nomina()
                    cNomina.IdEmpresa = Session("IdEmpresa")
                    dtEmisor = cNomina.ConsultarDatosEmisor()

                    If dtEmisor.Rows.Count > 0 Then
                        For Each oDataRow In dtEmisor.Rows
                            RfcEmisor = oDataRow("RFC")
                        Next
                    End If

                    Dim dtCliente As New DataTable
                    cNomina = New Nomina()
                    cNomina.Id = IdCliente
                    dtCliente = cNomina.ConsultarDatosCliente()

                    If dtCliente.Rows.Count > 0 Then
                        For Each oDataRow In dtCliente.Rows
                            RfcCliente = oDataRow("RFC")
                        Next
                    End If

                    Dim rutaEmpresa As String = ""
                    rutaEmpresa = Server.MapPath("~/XmlTimbrados/" & RfcEmisor & "/" & RfcCliente.ToString & "/S/").ToString & IdEjercicio.ToString & "/F/" & cmbPeriodo.SelectedValue.ToString & "/"

                    If Not Directory.Exists(rutaEmpresa) Then
                        Directory.CreateDirectory(rutaEmpresa)
                    End If

                    Dim cPeriodo As New Entities.Periodo()
                    cPeriodo.IdPeriodo = cmbPeriodo.SelectedValue
                    cPeriodo.ConsultarPeriodoID()

                    Dim FilePathXML = rutaEmpresa & RFC.ToString & "_" & Format(CDate(cPeriodo.FechaInicial), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinal), "dd-MM-yyyy").ToString & "_" & row("UUID").ToString & ".xml"

                    rutaEmpresa = Server.MapPath("~/PDF/" & RfcEmisor & "/" & RfcCliente.ToString & "/S/").ToString & IdEjercicio.ToString & "/F/" & cmbPeriodo.SelectedValue.ToString & "/"
                    Dim FilePathPDF = rutaEmpresa & RFC.ToString & "_" & Format(CDate(cPeriodo.FechaInicial), "dd-MM-yyyy").ToString & "_" & Format(CDate(cPeriodo.FechaFinal), "dd-MM-yyyy").ToString & "_" & row("UUID").ToString & ".pdf"

                    If Not File.Exists(FilePathPDF) Then
                        GuardaPDF(GeneraPDF(CInt(row("NoEmpleado")), row("UUID"), IdCliente), FilePathPDF)
                    End If
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
                    cConfiguracion.IdCliente = IdCliente
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

                    Dim correo As String = row("Email").ToString.Trim
                    Dim delimit As Char() = New Char() {";"c, ","c}

                    Dim objMM As New MailMessage

                    For Each splitTo As String In correo.Trim().Split(delimit)
                        If ValidarEmail(splitTo.Trim()) Then
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
                            mensaje = "Adjunto a este correo estamos enviándole el comprobante XML y PDF de nómina correspondiente al finiquito laboral. Cualquier duda o comentario,  escríbenos a " & email_from & vbCrLf & vbCrLf
                            mensaje += "Atentamente." & vbCrLf
                            mensaje += "Departamento Nóminas" & vbCrLf
                            'mensaje += razonsocial.ToString.ToUpper & vbCrLf

                            'objMM.From = New MailAddress(Session("email").ToString, razonsocial)
                            objMM.From = New MailAddress(email_from, "Departamento Nóminas")
                            objMM.IsBodyHtml = False
                            objMM.Priority = MailPriority.Normal
                            objMM.Subject = razonsocial & " - Recibo de Nómina Finiquito"
                            objMM.Body = mensaje
                            '
                            '   Agrega anexos
                            '
                            Dim AttachXML As Net.Mail.Attachment
                            Dim AttachPDF As Net.Mail.Attachment
                            AttachXML = New Net.Mail.Attachment(FilePathXML.Replace("/", "\"))
                            AttachPDF = New Net.Mail.Attachment(FilePathPDF.Replace("/", "\"))
                            objMM.Attachments.Add(AttachXML)
                            objMM.Attachments.Add(AttachPDF)
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
    Private Function GeneraPDFFiniquito(ByVal NoEmpleado As Integer, ByVal IdMovimiento As Integer, ByVal IdCliente As Integer) As Telerik.Reporting.Report
        Dim reporte As New Formatos.formato_finiquito
        Dim CantidadTexto As String = ""
        Dim RazonSocial As String = ""
        Dim Municipio As String = ""
        Dim Estado As String = ""
        Dim NombreEmpleado As String = ""
        Dim Puesto As String = ""
        Dim FechaIngreso As String = ""
        Dim FechaBaja As String = ""
        Dim DiasPagadosVacaciones As Decimal = 0

        Dim DiasLaborados As String = ""
        Dim DiasLaboradosAnio As String = ""
        Dim DiasVacacionesProporcionales As String = ""
        Dim ProporcionalVacaciones As String = ""
        Dim PorcentajePrimaVacacional As String = ""
        Dim PrimaVacaciones As String = ""
        Dim DiasAguinaldoAnio As String = ""
        Dim DiasAguinaldoProporcionales As String = ""
        Dim ProporcionalAguinaldo As String = ""
        Dim DiasPendientesPago As Decimal = 0
        Dim OtrasPercepcionesPendientes As Decimal = 0
        Dim SubsidioEmpleo As Decimal = 0
        Dim CuotasIMSS As Decimal = 0
        Dim ImpuestoISR As Decimal = 0

        Dim dt As New DataTable()
        Dim cNomina = New Nomina()
        cNomina.Id = IdCliente
        dt = cNomina.ConsultarDatosCliente()

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                RazonSocial = oDataRow("RazonSocial")
                Municipio = oDataRow("Municipio")
                Estado = oDataRow("Estado")
            Next
        End If

        Dim dtEmpleado As New DataTable
        Dim cEmpleado As New Entities.Empleado
        cEmpleado.IdEmpleado = NoEmpleado
        'cEmpleado.IdEmpresa = IdEmpresa
        cEmpleado.IdMovimiento = IdMovimiento
        dtEmpleado = cEmpleado.ConsultarEmpleados()

        If dtEmpleado.Rows.Count > 0 Then
            For Each oDataRow In dtEmpleado.Rows
                NombreEmpleado = oDataRow("nombre")
                Puesto = oDataRow("puesto")
            Next
        End If

        cNomina = New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Id = IdMovimiento
        dt = cNomina.ConsultarEmpleadosFiniquito()

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                FechaIngreso = oDataRow("FechaIngreso").ToString
                FechaBaja = oDataRow("FechaBaja").ToString
                CuotaDiaria = FormatCurrency(oDataRow("CuotaDiaria"), 2)
                SalarioDiarioIntegradoTrabajador = oDataRow("IntegradoIMSS")
                DiasPagadosVacaciones = oDataRow("DiasPagadosVacaciones")
            Next
        End If

        cNomina = New Nomina()
        cNomina.Id = IdMovimiento
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 1 'Semanal
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
                DiasAguinaldoProporcionales = oDataRow("DiasAguinaldoProporcionales").ToString
                ProporcionalAguinaldo = FormatCurrency(oDataRow("ProporcionalAguinaldo"), 2).ToString
            Next
        End If

        cNomina = New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = IdCliente
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Tipo = "F"
        cNomina.TipoConcepto = "P"
        cNomina.NoEmpleado = NoEmpleado
        cNomina.IdMovimiento = IdMovimiento
        dt = cNomina.ConsultarPercepcionesDeduccionesFiniquitoGenerado()

        If dt.Rows.Count > 0 Then
            If dt.Compute("Sum(Unidad)", "CvoConcepto=51") IsNot DBNull.Value Then
                DiasPendientesPago = dt.Compute("Sum(Unidad)", "CvoConcepto=51")
            End If
            If dt.Compute("Sum(Importe)", "CvoConcepto=54") IsNot DBNull.Value Then
                SubsidioEmpleo = dt.Compute("Sum(Importe)", "CvoConcepto=54")
            End If
            If dt.Compute("Sum(Importe)", "CvoConcepto<>2 AND CvoConcepto<>14 AND CvoConcepto<>15 AND CvoConcepto<>16 AND CvoConcepto<>55") IsNot DBNull.Value Then
                OtrasPercepcionesPendientes = dt.Compute("Sum(Importe)", "CvoConcepto<>2 AND CvoConcepto<>14 AND CvoConcepto<>15 AND CvoConcepto<>16 AND CvoConcepto<>55")
            End If
        End If

        cNomina = New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = IdCliente
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Tipo = "F"
        cNomina.TipoConcepto = "D"
        cNomina.NoEmpleado = NoEmpleado
        cNomina.IdMovimiento = IdMovimiento
        dt = cNomina.ConsultarPercepcionesDeduccionesFiniquitoGenerado()

        If dt.Rows.Count > 0 Then
            If dt.Compute("Sum(Importe)", "CvoConcepto=52") IsNot DBNull.Value Then
                ImpuestoISR = dt.Compute("Sum(Importe)", "CvoConcepto=52")
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

        Call MostrarPercepciones(NoEmpleado, IdCliente, IdMovimiento)
        Call MostrarDeducciones(NoEmpleado, IdCliente, IdMovimiento)

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
        reporte.ReportParameters("DiasAguinaldoProporcionales").Value = DiasAguinaldoProporcionales
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
    Private Function GeneraPDFRenuncia(ByVal NoEmpleado As Integer, ByVal IdMovimiento As Integer) As Telerik.Reporting.Report
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
        cNomina.IdEmpresa = Session("IdEmpresa")
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
        cEmpleado.IdEmpleado = NoEmpleado
        'cEmpleado.IdEmpresa = IdEmpresa
        cEmpleado.IdMovimiento = IdMovimiento
        dtEmpleado = cEmpleado.ConsultarEmpleados()

        If dtEmpleado.Rows.Count > 0 Then
            For Each oDataRow In dtEmpleado.Rows
                NombreEmpleado = oDataRow("nombre")
                Puesto = oDataRow("puesto")
            Next
        End If

        cNomina = New Nomina()
        cNomina.Id = IdMovimiento
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 1 'Semanal
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
    Private Sub MostrarPercepciones(ByVal NoEmpleado As Integer, ByVal IdCliente As Integer, ByVal IdMovimiento As Integer)

        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = IdCliente
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Tipo = "F"
        cNomina.TipoConcepto = "P"
        cNomina.IdMovimiento = IdMovimiento
        dt = cNomina.ConsultarPercepcionesDeduccionesFiniquitoGenerado()
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
    Private Sub MostrarDeducciones(ByVal NoEmpleado As Integer, ByVal IdCliente As Integer, ByVal IdMovimiento As Integer)

        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = IdCliente
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Tipo = "F"
        cNomina.TipoConcepto = "D"
        cNomina.IdMovimiento = IdMovimiento
        dt = cNomina.ConsultarPercepcionesDeduccionesFiniquitoGenerado()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            If dt.Compute("Sum(Importe)", "") IsNot DBNull.Value Then
                TotalDeducciones = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
            Else
                TotalDeducciones = 0
            End If
        End If
    End Sub
    Private Sub cmbCliente_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cmbCliente.SelectedIndexChanged
        Call CargarVariablesGenerales()
        Call CargaPeriodos(1)
        Call CargarGridEmpleados()
    End Sub
    Private Sub grdEmpleados_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdEmpleados.NeedDataSource
        Call CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = cmbCliente.SelectedValue
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = 1 'Semanal
        cNomina.Tipo = "F"
        cNomina.Periodo = cmbPeriodo.SelectedValue
        dt = cNomina.ConsultarEmpleadosGeneradosFiniquito()
        grdEmpleados.DataSource = dt
        cNomina = Nothing
    End Sub

End Class