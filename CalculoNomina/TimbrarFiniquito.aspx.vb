Imports Telerik.Web.UI
Imports Entities
Imports System.Xml
Imports System.IO
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography
Imports Ionic.Zip
Imports System.Web.Services.Protocols
Public Class TimbrarFiniquito
    Inherits System.Web.UI.Page

    Private TotalPercepciones As Decimal = 0
    Private TotalDeducciones As Decimal = 0
    Private NetoAPagar As Decimal = 0
    Private PercepcionesExentas As Decimal = 0
    Private PercepcionesGravadas As Decimal = 0
    Private Unidad As String
    Public NumeroDeDiasPagados As Double
    Public FolioXml As String

    Const URI_SAT = "http://www.sat.gob.mx/cfd/3"
    Private m_xmlDOM As New XmlDocument
    Private urlnomina As String
    Private data As Byte()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Call MostrarDatosFiniquito()
            Call ChecarPercepcionesExentasYGravadasFiniquito()
        End If
    End Sub
    Private Sub MostrarDatosFiniquito()
        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Tipo = "F"
        cNomina.IdContrato = Request("id")
        dt = cNomina.ConsultarEmpleadosFiniquitoGenerado()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                lblNoEmpleado.Text = oDataRow("NoEmpleado").ToString
                lblNombreEmpleado.Text = oDataRow("NombreEmpleado").ToString
                lblFechaIngreso.Text = oDataRow("FechaIngreso").ToString
                lblFechaBaja.Text = oDataRow("FechaBaja").ToString
                lblSueldoDiario.Text = FormatCurrency(oDataRow("CuotaDiaria"), 2).ToString
                lblSueldoDiarioIntegrado.Text = FormatCurrency(oDataRow("IntegradoIMSS"), 2).ToString
            Next
        End If

    End Sub
    Private Sub GridPercepciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridPercepciones.NeedDataSource

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Tipo = "F"
        cNomina.TipoConcepto = "P"
        cNomina.IdContrato = Request("id")
        dt = cNomina.ConsultarPercepcionesDeduccionesFiniquitoGenerado()
        cNomina = Nothing
        GridPercepciones.DataSource = dt

        If dt.Rows.Count > 0 Then
            txtPercepciones.Text = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
        End If
    End Sub
    Private Sub GridDeduciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridDeduciones.NeedDataSource

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Tipo = "F"
        cNomina.TipoConcepto = "D"
        cNomina.IdContrato = Request("id")
        dt = cNomina.ConsultarPercepcionesDeduccionesFiniquitoGenerado()
        cNomina = Nothing
        GridDeduciones.DataSource = dt

        If dt.Rows.Count > 0 Then
            txtDeducciones.Text = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
        End If

    End Sub
    Private Sub ChecarPercepcionesExentasYGravadasFiniquito()
        Try

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = Session("clienteid")
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Tipo = "F"
            cNomina.TipoConcepto = "P"
            cNomina.IdContrato = Request("id")
            dt = cNomina.ConsultarPercepcionesDeduccionesFiniquitoGenerado()

            If dt.Rows.Count > 0 Then
                TotalPercepciones = Math.Round(dt.Compute("Sum(Importe)", ""), 2)
                PercepcionesGravadas = dt.Compute("Sum(ImporteGravado)", "")
                PercepcionesExentas = dt.Compute("Sum(ImporteExento)", "")
            End If

            txtGravadoISR.Text = PercepcionesGravadas
            txtExentoISR.Text = PercepcionesExentas

            dt = New DataTable()
            cNomina = New Nomina()
            'cNomina.IdEmpresa = Session("clienteid")
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Tipo = "F"
            cNomina.TipoConcepto = "D"
            cNomina.IdContrato = Request("id")
            dt = cNomina.ConsultarPercepcionesDeduccionesFiniquitoGenerado()

            If dt.Rows.Count > 0 Then
                TotalDeducciones = Math.Round(dt.Compute("Sum(Importe)", ""), 2)
            End If

            NetoAPagar = TotalPercepciones - TotalDeducciones
            txtNetoAPagar.Text = NetoAPagar

        Catch oExcep As Exception
            Response.Write(oExcep.Message.ToString)
            Response.End()
        End Try
    End Sub
    Private Sub btnTimbrarFiniquito_Click(sender As Object, e As EventArgs) Handles btnTimbrarFiniquito.Click
        rwConfirmTimbrarFiniquito.RadConfirm("¿Está seguro de timbrar el finiquito, una vez timbrada no podrá hacer modificaciones?", "confirmCallbackFnTimbrarFiniquito", 330, 180, Nothing, "Confirmar")
    End Sub
    Private Sub btnConfirmarTimbraFiniquito_Click(sender As Object, e As EventArgs) Handles btnConfirmarTimbraFiniquito.Click
        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Tipo = "F"
        cNomina.IdContrato = Request("id")
        dt = cNomina.ConsultarEmpleadosFiniquitoGenerado()
        cNomina = Nothing

        Dim rutaEmpresa As String = ""
        Dim rutaXmlGenerado As String = ""

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                rutaEmpresa = Server.MapPath("~/XmlGenerados/F/C").ToString & "/" & Session("clienteid").ToString & "/" & oDataRow("Ejercicio").ToString

                If Not Directory.Exists(rutaEmpresa) Then
                    Directory.CreateDirectory(rutaEmpresa)
                End If

                serie.Value = oDataRow("Serie")
                folio.Value = oDataRow("Folio")

                CrearCFDNomina(rutaEmpresa, oDataRow("NoEmpleado"), oDataRow("Ejercicio"), oDataRow("Periodo"))
                GrabarGeneracionXml(oDataRow("NoEmpleado"), oDataRow("Ejercicio"), oDataRow("Periodo"))

                dt = New DataTable()
                cNomina = New Nomina()
                'cNomina.IdEmpresa = Session("clienteid")
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Tipo = "F"
                cNomina.IdContrato = Request("id")
                dt = cNomina.ConsultarEmpleadosFiniquitoGenerado()

                If dt.Rows.Count > 0 Then
                    For Each row In dt.Rows
                        If row("Generado") = "S" Then
                            Try
                                Dim SIFEIUsuario As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIUsuario")
                                Dim SIFEIContrasena As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIContrasena")
                                Dim SIFEIIdEquipo As String = System.Configuration.ConfigurationManager.AppSettings("SIFEIIdEquipo")

                                'Pruebas
                                'Dim TimbreSifeiVersion33 As New SIFEIPruebasV33.SIFEIService()
                                'Producción
                                Dim TimbreSifeiVersion33 As New SIFEI33.SIFEIService()
                                Call Comprimir()

                                Dim bytes() As Byte
                                bytes = TimbreSifeiVersion33.getCFDI(SIFEIUsuario, SIFEIContrasena, data, "", SIFEIIdEquipo)
                                Descomprimir(bytes, row("NoEmpleado"), row("Ejercicio"), row("Periodo"))
                            Catch ex As SoapException
                                GrabarTimbrado(row("NoEmpleado"), row("Ejercicio"), row("Periodo"), "N", "")
                                Dim ErrorTimbrado = New ErrorTimbrado()
                                ErrorTimbrado.IdEmpresa = Session("clienteid")
                                ErrorTimbrado.Ejercicio = row("Ejercicio")
                                ErrorTimbrado.TipoNomina = 2 'Catorcenal
                                ErrorTimbrado.Periodo = row("Periodo")
                                ErrorTimbrado.NoEmpleado = row("NoEmpleado")
                                ErrorTimbrado.IdContrato = row("IdContrato")
                                ErrorTimbrado.Descripcion = ex.Detail.InnerText.ToString
                                ErrorTimbrado.IdUsuario = Session("usuarioid")
                                ErrorTimbrado.Tipo = "F"
                                ErrorTimbrado.GuadarError()
                            End Try
                        End If
                    Next
                End If
            Next
        End If

        Response.Redirect("~/FiniquitosCatorcenal.aspx")

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
    Public Sub CrearCFDNomina(ByVal RutaXML As String, ByVal NoEmpleado As Integer, ByVal Ejercicio As Integer, ByVal Periodo As Integer)
        Dim Comprobante As XmlNode
        urlnomina = 0

        m_xmlDOM = CrearDOM()
        Comprobante = CrearNodoComprobante(NoEmpleado, Ejercicio, Periodo)

        m_xmlDOM.AppendChild(Comprobante)

        IndentarNodo(Comprobante, 1)

        CrearNodoEmisor(Comprobante)
        IndentarNodo(Comprobante, 1)

        CrearNodoReceptor(Comprobante, NoEmpleado)
        IndentarNodo(Comprobante, 1)

        CrearNodoConceptos(Comprobante)
        IndentarNodo(Comprobante, 1)

        CrearNodoComplemento(Comprobante, NoEmpleado, Ejercicio, Periodo)
        IndentarNodo(Comprobante, 0)

        If folio.Value > 0 Then
            Dim path As String = Server.MapPath("~/Certificado/") & "00001000000404558338.cer"
            'Dim path As String = Server.MapPath("~/Certificado/") & "CSD_Pruebas_CFDI_LAN7008173R5.cer"
            FolioXml = RutaXML & "/" & serie.Value.ToString & folio.Value.ToString & ".xml"
            SellarCFD(Comprobante, path)
            m_xmlDOM.InnerXml = (Replace(m_xmlDOM.InnerXml, "schemaLocation", "xsi:schemaLocation", , , CompareMethod.Text))
            m_xmlDOM.Save(FolioXml)
        End If
    End Sub
    Private Sub IndentarNodo(ByVal Nodo As XmlNode, ByVal Nivel As Long)
        Nodo.AppendChild(m_xmlDOM.CreateTextNode(vbNewLine & New String(ControlChars.Tab, Nivel)))
    End Sub
    Private Function CrearDOM() As XmlDocument
        Dim oDOM As New XmlDocument
        Dim Nodo As XmlNode
        Nodo = oDOM.CreateProcessingInstruction("xml", "version=""1.0"" encoding=""utf-8""")
        oDOM.AppendChild(Nodo)
        Nodo = Nothing
        CrearDOM = oDOM
    End Function
    Private Function CrearNodo(ByVal nombre As String)
        If urlnomina = 0 Then
            CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, nombre, URI_SAT)
        Else
            CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, nombre, "http://www.sat.gob.mx/nomina12")
        End If
    End Function
    Private Function CrearNodoComprobante(ByVal NoEmpleado As Integer, ByVal Ejercicio As Integer, ByVal Periodo As Integer) As XmlNode
        Dim Comprobante As XmlElement
        Comprobante = m_xmlDOM.CreateElement("cfdi:Comprobante", URI_SAT)
        CrearAtributosComprobante(Comprobante, NoEmpleado, Ejercicio, Periodo)
        CrearNodoComprobante = Comprobante
    End Function
    Private Sub AsignaSerieFolio(ByVal NoEmpleado As Integer, ByVal Ejercicio As Integer, ByVal Periodo As Integer)
        '
        '   Obtiene serie y folio
        '
        Dim dt As New DataTable()
        Dim cNomina = New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.Ejercicio = Ejercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = Periodo
        cNomina.Tipo = "F"
        cNomina.NoEmpleado = NoEmpleado
        dt = cNomina.AsignaSerieFolio()
        '
        If dt.Rows.Count > 0 Then
            serie.Value = dt.Rows.Item(0)("serie").ToString()
            folio.Value = dt.Rows.Item(0)("folio")
        End If
        '
    End Sub
    Private Sub CrearAtributosComprobante(ByVal Nodo As XmlElement, ByVal NoEmpleado As Integer, ByVal Ejercicio As Integer, ByVal Periodo As Integer)
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
            Call AsignaSerieFolio(NoEmpleado, Ejercicio, Periodo)
        End If

        dt = New DataTable
        cNomina = New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Tipo = "F"
        cNomina.TipoConcepto = "P"
        cNomina.IdContrato = Request("id")
        dt = cNomina.ConsultarPercepcionesDeduccionesFiniquitoGenerado()

        If dt.Rows.Count > 0 Then
            For i = 0 To dt.Rows.Count - 1
                If Convert.ToDecimal(dt.Rows.Item(i)("Importe")) > 0 Then
                    TotalPercepciones += MyRound(Convert.ToDecimal(dt.Rows.Item(i)("Importe")))
                End If
            Next
        End If

        dt = New DataTable
        cNomina = New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Tipo = "F"
        cNomina.TipoConcepto = "D"
        cNomina.IdContrato = Request("id")
        dt = cNomina.ConsultarPercepcionesDeduccionesFiniquitoGenerado()
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
        Nodo.SetAttribute("xmlns:cfdi", "http://www.sat.gob.mx/cfd/3")
        Nodo.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
        Nodo.SetAttribute("xsi:schemaLocation", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd http://www.sat.gob.mx/nomina12 http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina12.xsd")
        Nodo.SetAttribute("Certificado", "")
        Nodo.SetAttribute("Fecha", Format(Now(), "yyyy-MM-ddThh:mm:ss")) 'yyyy-MM-ddTHH:mm:ss
        Nodo.SetAttribute("MetodoPago", "PUE") '(Pago en una sola exhibición)
        Nodo.SetAttribute("FormaPago", "99") '(Por Definir).
        Nodo.SetAttribute("NoCertificado", "")
        Nodo.SetAttribute("Sello", "")
        Nodo.SetAttribute("SubTotal", MyRound(Convert.ToDecimal(TotalPercepciones)))
        Nodo.SetAttribute("Descuento", MyRound(TotalDeducciones))
        Nodo.SetAttribute("Total", MyRound(TotalPercepciones) - MyRound(TotalDeducciones))
        Nodo.SetAttribute("TipoDeComprobante", "N")
        Nodo.SetAttribute("LugarExpedicion", LugarExpedicion)
        Nodo.SetAttribute("Moneda", "MXN")
        Nodo.SetAttribute("Serie", serie.Value)
        Nodo.SetAttribute("Folio", folio.Value)
        Nodo.SetAttribute("Version", "3.3")
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
                'Emisor.SetAttribute("Rfc", "LAN7008173R5")
                'Emisor.SetAttribute("RegimenFiscal", "601")
                Nodo.AppendChild(Emisor)
            Next
        End If
    End Sub
    Private Sub CrearNodoReceptor(ByVal Nodo As XmlNode, ByVal NoEmpleado As Integer)

        Dim dtReceptor As New DataTable
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Tipo = "F"
        cNomina.IdContrato = Request("id")
        dtReceptor = cNomina.ConsultarEmpleadosFiniquitoGenerado()

        Dim Receptor As XmlElement

        If dtReceptor.Rows.Count > 0 Then
            For Each oDataRow In dtReceptor.Rows
                Receptor = CrearNodo("cfdi:Receptor")
                Receptor.SetAttribute("Rfc", oDataRow("Rfc"))
                Receptor.SetAttribute("Nombre", oDataRow("NombreEmpleado"))
                Receptor.SetAttribute("UsoCFDI", "P01") 'Por definir
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
        Concepto.SetAttribute("ClaveProdServ", "84111505")
        Concepto.SetAttribute("Importe", Format(MyRound(Convert.ToDecimal(TotalPercepciones)), "#0.00"))
        Concepto.SetAttribute("Descuento", Format(MyRound(Convert.ToDecimal(TotalDeducciones)), "#0.00"))
        Concepto.SetAttribute("ValorUnitario", Format(MyRound(Convert.ToDecimal(TotalPercepciones)), "#0.00"))
        Concepto.SetAttribute("Descripcion", "Pago de nómina")
        Concepto.SetAttribute("ClaveUnidad", "ACT")
        Concepto.SetAttribute("Cantidad", "1")
        Conceptos.AppendChild(Concepto)
        IndentarNodo(Conceptos, 2)
        Nodo.AppendChild(Conceptos)
    End Sub
    Private Sub CrearNodoComplemento(ByVal Nodo As XmlNode, ByVal NoEmpleado As Integer, ByVal Ejercicio As Integer, ByVal Periodo As Integer)

        Dim dtEmpleado As New DataTable
        Dim cNomina As New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.Ejercicio = Ejercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = Periodo
        cNomina.Tipo = "F"
        dtEmpleado = cNomina.ConsultarDatosEmpleadosGenerados()

        Dim Complemento As XmlElement
        Dim Nomina As XmlElement
        Dim Percepciones As XmlElement
        Dim Percepcion As XmlElement
        Dim Deducciones As XmlElement
        Dim Deduccion As XmlElement
        Dim Incapacidades As XmlElement
        Dim Incapacidad As XmlElement
        Dim SeparacionIndemnizacion As XmlElement
        Dim HorasExtra As XmlElement
        Dim Emisor As XmlElement
        Dim Receptor As XmlElement
        Dim OtrosPagos As XmlElement
        Dim OtroPago As XmlElement
        Dim SubsidioAlEmpleo As XmlElement

        'Dim TotalSueldos As Decimal = 0
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

        If dtEmpleado.Rows.Count > 0 Then
            For Each oDataRow In dtEmpleado.Rows

                Dim cPeriodo As New Entities.Periodo()
                cPeriodo.IdPeriodo = Periodo
                cPeriodo.ConsultarPeriodoID()

                Dim dt As New DataTable
                cNomina = New Nomina()
                'cNomina.IdEmpresa = Session("clienteid")
                cNomina.Ejercicio = Ejercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = Periodo
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
                    'If dt.Compute("SUM(Importe)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'") IsNot DBNull.Value Then
                    '    TotalSueldos = Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'"))
                    'End If
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
                        TotalGravadoHorasExtra = Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT='19'"))

                        'Modificacion para que cuadre el total Gravado cuando lleva horas extra
                        'ImporteGravadoHorasExtra = Math.Round(TotalGravadoHorasExtra, 2, MidpointRounding.AwayFromZero) - Math.Round(Convert.ToDecimal(ImporteExentoHorasExtra), 2, MidpointRounding.AwayFromZero)
                        ImporteGravadoHorasExtra = MyRound(Convert.ToDecimal(TotalGravadoHorasExtra)) - MyRound(Convert.ToDecimal(ImporteExentoHorasExtra))
                    End If
                    If dt.Compute("SUM(Importe)", "CvoSAT='16' or CvoSAT='17' or CvoSAT='18'") IsNot DBNull.Value Then
                        TotalOtrosPagos = Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT='16' or CvoSAT='17' or CvoSAT='18'"))
                    End If
                End If

                dt = New DataTable
                cNomina = New Nomina()
                'cNomina.IdEmpresa = Session("clienteid")
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Tipo = "F"
                cNomina.TipoConcepto = "D"
                cNomina.IdContrato = Request("id")
                dt = cNomina.ConsultarPercepcionesDeduccionesFiniquitoGenerado()
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

                Call ConsultarNumeroDeDiasPagados(NoEmpleado, Ejercicio, Periodo)

                Nomina.SetAttribute("Version", "1.2")
                Nomina.SetAttribute("TipoNomina", "E")
                Nomina.SetAttribute("FechaPago", Mid(CDate(cPeriodo.FechaFinal), 7, 4) + "-" + Mid(CDate(cPeriodo.FechaFinal), 4, 2) + "-" + Mid(CDate(cPeriodo.FechaFinal), 1, 2))
                Nomina.SetAttribute("FechaInicialPago", Format(CDate(cPeriodo.FechaInicial), "yyyy-MM-dd"))
                Nomina.SetAttribute("FechaFinalPago", Format(CDate(cPeriodo.FechaFinal), "yyyy-MM-dd"))
                Nomina.SetAttribute("NumDiasPagados", Format(CDbl(NumeroDeDiasPagados), "0.000"))
                Nomina.SetAttribute("TotalPercepciones", MyRound(MyRound(TotalPercepciones) + MyRound(TotalHorasExtra)))
                If TotalDeducciones > 0 Then
                    Nomina.SetAttribute("TotalDeducciones", MyRound(TotalDeducciones))
                End If
                If TotalOtrosPagos > 0 Then
                    Nomina.SetAttribute("TotalOtrosPagos", MyRound(TotalOtrosPagos))
                End If

                Emisor = CrearNodo("nomina12:Emisor")
                If oDataRow("RegistroPatronal").ToString <> "" Then
                    Emisor.SetAttribute("RegistroPatronal", oDataRow("RegistroPatronal"))
                End If
                Emisor.SetAttribute("RfcPatronOrigen", oDataRow("RfcPatronOrigen"))

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
                cNomina.Periodo = Periodo
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

                'Receptor.SetAttribute("PeriodicidadPago", "03") 'Catorcenal
                Receptor.SetAttribute("PeriodicidadPago", "99") 'Por ser nómina extraordinaria

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

                Dim SubContratacion As XmlElement
                SubContratacion = CrearNodo("nomina12:SubContratacion")
                SubContratacion.SetAttribute("RfcLabora", oDataRow("RfcLabora"))
                SubContratacion.SetAttribute("PorcentajeTiempo", "100")
                Receptor.AppendChild(SubContratacion)

                Nomina.AppendChild(Receptor)

                Percepciones = CrearNodo("nomina12:Percepciones")
                Percepciones.SetAttribute("TotalSueldos", MyRound(TotalGravado + TotalExento + TotalHorasExtra))
                Percepciones.SetAttribute("TotalGravado", MyRound(TotalGravado + ImporteGravadoHorasExtra))
                Percepciones.SetAttribute("TotalExento", MyRound(TotalExento + ImporteExentoHorasExtra))

                Nomina.AppendChild(Percepciones)

                'Atributo condicional para expresar el importe exento y gravado de las claves tipo percepción 022 Prima por Antigüedad, 023 Pagos por separación y 025 Indemnizaciones.
                If TotalSeparacionIndemnizacion > 0 Then
                    Percepciones.SetAttribute("TotalSeparacionIndemnizacion", MyRound(Convert.ToDecimal(TotalSeparacionIndemnizacion)))
                End If

                dt = New DataTable
                cNomina = New Nomina()
                'cNomina.IdEmpresa = Session("clienteid")
                cNomina.Ejercicio = Ejercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = Periodo
                cNomina.TipoConcepto = "P"
                cNomina.Tipo = "F"
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
                            Percepcion.SetAttribute("ImporteGravado", MyRound(Convert.ToDecimal(dt.Rows.Item(i)("ImporteGravado"))))
                            Percepcion.SetAttribute("ImporteExento", MyRound(Convert.ToDecimal(dt.Rows.Item(i)("ImporteExento"))))
                            Percepciones.AppendChild(Percepcion)
                        End If
                    Next
                End If
                '
                ' SeparacionIndemnizacion (Finiquitos)
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
                '
                '   Horas Extra
                '
                dt = New DataTable
                cNomina = New Nomina()
                'cNomina.IdEmpresa = Session("clienteid")
                cNomina.Ejercicio = Ejercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = Periodo
                cNomina.TipoConcepto = "P"
                cNomina.CvoSAT = "19"
                cNomina.Tipo = "F"
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
                            HorasExtra.SetAttribute("HorasExtra", Convert.ToDecimal(dt.Rows.Item(i)("Unidad")))
                            HorasExtra.SetAttribute("ImportePagado", MyRound(Convert.ToDecimal(dt.Rows.Item(i)("Importe"))))
                            Percepcion.AppendChild(HorasExtra)
                        End If
                    Next

                    Percepciones.AppendChild(Percepcion)

                End If

                Nomina.AppendChild(Percepciones)

                If TotalDeducciones > 0 Then
                    '
                    ' Deducciones
                    '
                    Deducciones = CrearNodo("nomina12:Deducciones")
                    Deducciones.SetAttribute("TotalOtrasDeducciones", MyRound(MyRound(TotalDeducciones) - MyRound(TotalImpuestosRetenidos)))
                    If TotalImpuestosRetenidos > 0 Then
                        Deducciones.SetAttribute("TotalImpuestosRetenidos", MyRound(TotalImpuestosRetenidos))
                    End If

                    Nomina.AppendChild(Deducciones)

                    dt = New DataTable
                    cNomina = New Nomina()
                    'cNomina.IdEmpresa = Session("clienteid")
                    cNomina.Ejercicio = Ejercicio
                    cNomina.TipoNomina = 2 'Catorcenal
                    cNomina.Periodo = Periodo
                    cNomina.TipoConcepto = "D"
                    cNomina.Tipo = "F"
                    cNomina.NoEmpleado = NoEmpleado
                    dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                    cNomina = Nothing

                    If dt.Rows.Count > 0 Then
                        Dim oDataRowDeducciones As DataRow
                        For Each oDataRowDeducciones In dt.Rows
                            If oDataRowDeducciones("CvoSAT").ToString <> "6" Then ' Deducciones diferentes a Incapacidad
                                ObtenerUnidad(oDataRowDeducciones("CvoConcepto"), NoEmpleado, Ejercicio, Periodo)
                                Deduccion = CrearNodo("nomina12:Deduccion")
                                Deduccion.SetAttribute("TipoDeduccion", String.Format("{0:000}", Convert.ToInt32(oDataRowDeducciones("CvoSAT"))))
                                Deduccion.SetAttribute("Clave", String.Format("{0:000}", oDataRowDeducciones("CvoConcepto")))
                                Deduccion.SetAttribute("Concepto", oDataRowDeducciones("Concepto").ToString)
                                Deduccion.SetAttribute("Importe", MyRound(Convert.ToDecimal(oDataRowDeducciones("Importe"))))
                                Deducciones.AppendChild(Deduccion)
                            End If
                        Next
                        Nomina.AppendChild(Deducciones)
                    End If
                End If
                '
                '   Otros pagos
                '
                dt = New DataTable
                cNomina = New Nomina()
                'cNomina.IdEmpresa = Session("clienteid")
                cNomina.Ejercicio = Ejercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Tipo = "F"
                cNomina.Periodo = Periodo
                cNomina.TipoConcepto = "P"
                cNomina.NoEmpleado = NoEmpleado
                dt = cNomina.ConsultarOtrosPagos()
                cNomina = Nothing

                If dt.Rows.Count > 0 Then
                    If TotalOtrosPagos > 0 Then
                        OtrosPagos = CrearNodo("nomina12:OtrosPagos")
                    End If

                    For i = 0 To dt.Rows.Count - 1
                        If dt.Rows.Item(i)("CvoSAT").ToString = "16" Then ' Otros (Cátalogo Anterior)
                            OtroPago = CrearNodo("nomina12:OtroPago")
                            OtroPago.SetAttribute("TipoOtroPago", "999")
                            OtroPago.SetAttribute("Clave", "016")
                            OtroPago.SetAttribute("Concepto", dt.Rows.Item(i)("Concepto").ToString)
                            OtroPago.SetAttribute("Importe", MyRound(Convert.ToDecimal(dt.Rows.Item(i)("Importe"))))
                            OtrosPagos.AppendChild(OtroPago)
                        End If

                        If dt.Rows.Item(i)("CvoSAT").ToString = "17" Then ' Otros (Subsidio para el empleo)
                            OtroPago = CrearNodo("nomina12:OtroPago")
                            OtroPago.SetAttribute("TipoOtroPago", "002")
                            OtroPago.SetAttribute("Clave", "017")
                            OtroPago.SetAttribute("Concepto", dt.Rows.Item(i)("Concepto").ToString)
                            OtroPago.SetAttribute("Importe", MyRound(Convert.ToDecimal(dt.Rows.Item(i)("Importe"))))

                            SubsidioAlEmpleo = CrearNodo("nomina12:SubsidioAlEmpleo")
                            SubsidioAlEmpleo.SetAttribute("SubsidioCausado", MyRound(Convert.ToDecimal(dt.Rows.Item(i)("Importe"))))
                            OtroPago.AppendChild(SubsidioAlEmpleo)
                            OtrosPagos.AppendChild(OtroPago)
                        End If
                    Next

                    Nomina.AppendChild(OtrosPagos)

                End If
                '
                ' Incapacidad
                '
                dt = New DataTable
                cNomina = New Nomina()
                'cNomina.IdEmpresa = Session("clienteid")
                cNomina.Ejercicio = Ejercicio
                cNomina.TipoNomina = 2 'Catorcenal
                cNomina.Periodo = Periodo
                cNomina.TipoConcepto = "D"
                cNomina.Tipo = "F"
                cNomina.CvoSAT = "6"
                cNomina.NoEmpleado = NoEmpleado
                dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                cNomina = Nothing
                '
                If dt.Rows.Count > 0 Then

                    Incapacidades = CrearNodo("nomina12:Incapacidades")

                    For Each oDataRowDeducciones In dt.Rows
                        If oDataRowDeducciones("CvoConcepto").ToString = "59" Then ' INCAPACIDAD POR ENFERMEDAD
                            ObtenerUnidad(oDataRowDeducciones("CvoConcepto").ToString, NoEmpleado, Ejercicio, Periodo)
                            Incapacidad = CrearNodo("nomina12:Incapacidad")
                            Incapacidad.SetAttribute("DiasIncapacidad", Convert.ToDecimal(oDataRowDeducciones("Unidad")))
                            Incapacidad.SetAttribute("TipoIncapacidad", "02")
                            Incapacidad.SetAttribute("ImporteMonetario", MyRound(Convert.ToDecimal(oDataRowDeducciones("Importe"))))
                            Incapacidades.AppendChild(Incapacidad)
                        End If
                        If oDataRowDeducciones("CvoConcepto").ToString = "161" Then ' INCAPACIDAD POR MATERNIDAD
                            ObtenerUnidad(oDataRowDeducciones("CvoConcepto").ToString, NoEmpleado, Ejercicio, Periodo)
                            Incapacidad = CrearNodo("nomina12:Incapacidad")
                            Incapacidad.SetAttribute("DiasIncapacidad", Convert.ToDecimal(oDataRowDeducciones("Unidad")))
                            Incapacidad.SetAttribute("TipoIncapacidad", "03")
                            Incapacidad.SetAttribute("ImporteMonetario", MyRound(Convert.ToDecimal(oDataRowDeducciones("Importe"))))
                            Incapacidades.AppendChild(Incapacidad)
                        End If
                    Next
                    Nomina.AppendChild(Incapacidades)
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
    Private Sub ObtenerUnidad(ByVal CvoConcepto As String, ByVal NoEmpleado As Integer, ByVal Ejercicio As Integer, ByVal Periodo As Integer)
        Try

            Dim TablaUnidad As DataTable
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = Session("clienteid")
            cNomina.Ejercicio = Ejercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = Periodo
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
    Private Sub ConsultarNumeroDeDiasPagados(ByVal NoEmpleado As Integer, ByVal Ejercicio As Integer, ByVal Periodo As Integer)
        Try
            Dim DiasVacaciones As Double = 0
            Dim DiasCuotaPeriodo As Double = 0
            Dim DiasHonorarioAsimilado As Double = 0
            Dim DiasPagoPorHoras As Double = 0
            Dim DiasComision As Double = 0
            Dim DiasDestajo As Double = 0
            Dim DiasFaltasPermisosIncapacidades As Double = 0

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = Session("clienteid")
            cNomina.Ejercicio = Ejercicio
            cNomina.TipoNomina = 2 'Catorcenal
            cNomina.Periodo = Periodo
            cNomina.NoEmpleado = NoEmpleado

            dt = cNomina.ConsultarConceptosEmpleado()

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
        'Dim ArchivoPFX = Server.MapPath("~/PKI/") & "CSD_Pruebas_CFDI_LAN7008173R5.pfx"
        'Dim ContrasenaPfx As String = "12345678a"
        Dim ArchivoPFX As String = Server.MapPath("~/PKI/") & "00001000000404558338.pfx"
        Dim ContrasenaPfx As String = "TSE10120"

        Dim privateCert As New X509Certificate2(ArchivoPFX, ContrasenaPfx, X509KeyStorageFlags.Exportable)
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
        'xslt.Load("http://www.sat.gob.mx/sitio_internet/cfd/3/cadenaoriginal_3_3/cadenaoriginal_3_3.xslt")
        xslt.Load(Server.MapPath("~/SAT/") & "cadenaoriginal_3_3.xslt")
        xslt.Transform(navigator, Nothing, output)
        GetCadenaOriginal = output.ToString
    End Function
    Private Sub GrabarGeneracionXml(ByVal NoEmpleado As Integer, ByVal Ejercicio As Integer, ByVal Periodo As Integer)

        Dim cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.Ejercicio = Ejercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Tipo = "F"
        cNomina.Periodo = Periodo
        cNomina.Generado = "S"
        cNomina.ActualizarEstatusGeneradoNomina()
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
    Private Sub GrabarTimbrado(ByVal NoEmpleado As Integer, ByVal Ejercicio As Integer, ByVal Periodo As Integer, ByVal Timbrado As String, ByVal UUID As String)

        Dim cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.Ejercicio = Ejercicio
        cNomina.TipoNomina = 2 'Catorcenal
        cNomina.Periodo = Periodo
        cNomina.Timbrado = Timbrado
        cNomina.UUID = UUID
        cNomina.Tipo = "F"
        cNomina.ActualizarEstatusTimbradoNomina()
    End Sub
    Private Function Comprimir()
        Dim zip As ZipFile = New ZipFile(serie.Value.ToString & folio.Value.ToString & ".zip")
        zip.AddFile(FolioXml, "")
        Dim ms As New MemoryStream()
        zip.Save(ms)
        Data = ms.ToArray
    End Function
    Private Function Descomprimir(ByVal data5 As Byte(), ByVal NoEmpleado As Integer, ByVal Ejercicio As Integer, ByVal Periodo As Integer)
        Dim ms1 As New MemoryStream(data5)
        Dim zip1 As ZipFile = New ZipFile()
        zip1 = ZipFile.Read(ms1)

        Dim archivo As String = ""
        Dim DirectorioExtraccion As String = ""
        DirectorioExtraccion = Server.MapPath("~\XmlTimbrados\F\").ToString & Session("clienteid").ToString & "\" & Ejercicio.ToString & "\" & Periodo.ToString & "\"

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

            Call GrabarTimbrado(NoEmpleado, Ejercicio, Periodo, "S", UUID)

            FolioXmlTimbrado = DirectorioExtraccion & "\" & UUID.ToString & ".xml"

            If File.Exists(FolioXml) Then
                My.Computer.FileSystem.CopyFile(DirectorioExtraccion & archivo, FolioXmlTimbrado)
                File.Delete(Server.MapPath("~/XmlGenerados/F/C").ToString & "/" & Session("clienteid").ToString & "/" & Ejercicio.ToString & "/" & serie.Value.ToString & folio.Value.ToString & ".xml")
            End If

        End If

    End Function
    Private Function MyRound(Importe As Decimal) As Decimal
        Dim r As Decimal = Math.Ceiling(Importe * 100D) / 100D
        Return r
    End Function

End Class