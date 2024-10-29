Imports Telerik.Web.UI
Imports Entities
Imports System.Xml
Imports System.IO
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography
Imports Telerik.Reporting.Processing
Imports ThoughtWorks.QRCode.Codec
Imports System.Net.Mail
Public Class CalculoUtilidades
    Inherits System.Web.UI.Page
    Dim ObjData As New DataControl()
    Private TotalPercepciones As Double
    Private TotalDeducciones As Double
    Private NetoAPagar As Double
    Private PercepcionesGravadas As Double
    Private PercepcionesExentas As Double
    Private Impuesto As Double

    Public FolioXml As String

    Const URI_SAT = "http://www.sat.gob.mx/cfd/4"
    Private m_xmlDOM As New XmlDocument
    Private urlnomina As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Call ConsultarDetalleNominaPTU()
            Call CargarGridEmpleadosPTU()
        End If
        lblEjercicio.Text = ConsultarEjercicio().ToString
        RadProgressArea1.Localization.TotalFiles = "Total empleados"
        RadProgressArea1.Localization.UploadedFiles = "Calculados"
        RadProgressArea1.Localization.CurrentFileName = "Calculando: "
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

    Public Sub ConsultarDetalleNominaPTU()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = ConsultarEjercicio()
        dt = cNomina.ConsultarDetalleNominaPTU()

        If dt.Rows.Count > 0 Then
            panelDatos.Visible = True
            btnGeneraNomina.Enabled = False
            btnTimbrarNomina.Enabled = False
            'btnModificacionDeNomina.Enabled = True
            'btnGenerarNominaElectronica.Enabled = True
            Dim rows() As DataRow = dt.Select("Timbrado='S'")
            If (rows.Length > 0) Then
                btnBorrarNomina.Enabled = False
                If rows.Length < dt.Rows.Count Then
                    btnGenerarNominaElectronica.Enabled = True
                    'btnTimbrarNomina.Enabled = True
                Else
                    btnGenerarNominaElectronica.Enabled = False
                    'btnTimbrarNomina.Enabled = False'
                End If
            Else
                btnBorrarNomina.Enabled = True
            End If
            Dim rowsCorreos() As DataRow = dt.Select("Enviado='S'")
            If rows.Length > 0 And rows.Length > rowsCorreos.Length Then
                ''BtnGenerarPdf.Enabled = True
                'BtnEnviarCorreo.Enabled = True
            ElseIf rows.Length > 0 And rows.Length = rowsCorreos.Length Then
                ''BtnGenerarPdf.Enabled = False
                'BtnEnviarCorreo.Enabled = False
            End If
            Dim rowGenerados() As DataRow = dt.Select("Generado='S'")
            If (rowGenerados.Length > 0) Then
                'btnModificacionDeNomina.Enabled = False
                btnGenerarNominaElectronica.Enabled = False
                'btnTimbrarNomina.Enabled = True
                btnGenerarPDF.Enabled = True
                btnBorrarNomina.Enabled = False
            Else
                btnGenerarNominaElectronica.Enabled = True
                btnGenerarPDF.Enabled = False
            End If
            Dim rowPdf() As DataRow = dt.Select("Pdf='S'")
            If (rowPdf.Length > 0) Then
                btnEnvioComprobantes.Enabled = True
            End If
        ElseIf dt.Rows.Count = 0 Then
            btnGeneraNomina.Enabled = True
            btnBorrarNomina.Enabled = False
            btnGenerarNominaElectronica.Enabled = False
            'btnTimbrarNomina.Enabled = False
            'btnModificacionDeNomina.Enabled = False
            btnGenerarPDF.Enabled = False
            btnEnvioComprobantes.Enabled = False
        End If
    End Sub

    Private Sub btnGeneraNomina_Click(sender As Object, e As EventArgs) Handles btnGeneraNomina.Click
        rwConfirm.RadConfirm("¿Está seguro de generar el cálculo de reparto de utilidades?", "confirmCallbackFnGenerarNomina", 330, 180, Nothing, "Confirmar")
    End Sub

    Private Sub btnConfirmarGeneraNomina_Click(sender As Object, e As EventArgs) Handles btnConfirmarGeneraNomina.Click
        Dim dt As New DataTable

        Dim Nomina As New Entities.Nomina()
        dt = Nomina.ConsultarEmpleadosActivosPTU()
        Nomina = Nothing

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

            Call ConsultarDetalleNominaPTU()
            Call CargarGridEmpleadosPTU()

        End If
    End Sub

    Private Sub GuardarRegistro(ByVal IdEmpresa, ByVal NoEmpleado, ByVal TipoNomina, ByVal IdContrato, ByVal CuotaDiaria)
        Try

            Dim cNomina = New Nomina()
            cNomina.IdEmpresa = IdEmpresa
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
            cNomina.Tipo = "U"
            cNomina.Situacion = "A"
            cNomina.GuadarNomina()
            cNomina = Nothing

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub

    Private Sub CargarGridEmpleadosPTU()

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = ConsultarEjercicio()
        dt = cNomina.ConsultarDetalleNominaPTU()
        grdEmpleadosPTU.DataSource = dt
        grdEmpleadosPTU.DataBind()
        cNomina = Nothing
    End Sub

    Private Sub grdEmpleadosPTU_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles grdEmpleadosPTU.ItemDataBound
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
                Dim lnkEditar As LinkButton = DirectCast(e.Item.FindControl("lnkEditar"), LinkButton)
                lnkEditar.Attributes.Add("onclick", "javascript: OpenWindow('" & item.GetDataKeyValue("NoEmpleado").ToString & "','" & item.GetDataKeyValue("IdContrato").ToString & "','" & item.GetDataKeyValue("IdEmpresa").ToString & "','" & item.GetDataKeyValue("IdEjercicio").ToString & "','" & item.GetDataKeyValue("TipoNomina").ToString & "'); return false;")

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
                    lnkEditar.Visible = False
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

    Private Sub btnBorrarNomina_Click(sender As Object, e As EventArgs) Handles btnBorrarNomina.Click
        rwConfirm.RadConfirm("¿Está seguro que desea borrar esta nómina especial?", "confirmCallbackFnBorrarNomina", 330, 180, Nothing, "Confirmar")
    End Sub

    Private Sub btnConfirmarBorrarNomina_Click(sender As Object, e As EventArgs) Handles btnConfirmarBorrarNomina.Click
        Call EliminarNomina()
        Call ConsultarDetalleNominaPTU()
        Call CargarGridEmpleadosPTU()
    End Sub

    Private Sub EliminarNomina()
        Try
            Dim cNomina As New Nomina()
            cNomina.Ejercicio = ConsultarEjercicio()
            cNomina.Periodo = 0
            cNomina.Tipo = "U"
            cNomina.EliminaNominaPTU()

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub

    Private Sub btnGenerarNominaElectronica_Click(sender As Object, e As EventArgs) Handles btnGenerarNominaElectronica.Click
        rwConfirm.RadConfirm("¿Está seguro de generar el cálculo de nómina especial PTU?", "confirmCallbackFnGeneraNominaElectronica", 330, 180, Nothing, "Confirmar")
    End Sub

    Private Sub btnConfirmarGeneraNominaElectronica_Click(sender As Object, e As EventArgs) Handles btnConfirmarGeneraNominaElectronica.Click
        Dim dt As New DataTable
        Dim cNomina = New Nomina()
        cNomina.Ejercicio = ConsultarEjercicio()
        cNomina.Periodo = 0
        cNomina.Tipo = "U"
        dt = cNomina.ConsultarEmpleadosNoGeneradosNominaEspecial()

        Dim ruta As String = ""

        ruta = Server.MapPath("~/XmlGenerados/U/").ToString & "/" & ConsultarEjercicio().ToString

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

                'Call CargarPercepciones(oDataRow("IdEmpresa"), oDataRow("Ejercicio"), oDataRow("TipoNomina"), oDataRow("NoEmpleado"))
                'Call CargarDeducciones(oDataRow("IdEmpresa"), oDataRow("Ejercicio"), oDataRow("TipoNomina"), oDataRow("NoEmpleado"))
                'NetoAPagar = (TotalPercepciones - TotalDeducciones)

                'FolioXml = "1" & IdEmpresa.ToString & IdEjercicio.ToString & cmbPeriodo.SelectedValue.ToString & oDataRow("NoEmpleado").ToString
                'CrearCFDNominaSemanal(oDataRow("NoEmpleado"), rutaEmpresa & "/" & String.Format("{0:00}", IdEmpresa) & IdEjercicio.ToString & "S" & String.Format("{0:00}", Convert.ToInt32(cmbPeriodo.SelectedValue)) & String.Format("{0:00}", oDataRow("NOEMPLEADO")) & ".xml")

                GrabarGeneracionXml(oDataRow("NoEmpleado"), oDataRow("IdEmpresa"), oDataRow("Ejercicio"), oDataRow("TipoNomina"))

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

            Response.Redirect("~/CalculoUtilidades.aspx")

        ElseIf dt.Rows.Count = 0 Then
            rwAlerta.RadAlert("Esta nomina ya esta timbrada completamente!!!", 330, 180, "Alerta", "", "")
        End If
    End Sub

    Private Sub GrabarGeneracionXml(ByVal NoEmpleado As Integer, ByVal IdEmpresa As Integer, ByVal IdEjercicio As Integer, ByVal TipoNomina As Integer)

        Dim cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = TipoNomina
        cNomina.Periodo = 0
        cNomina.Tipo = "U"
        cNomina.Generado = "S"
        cNomina.ActualizarEstatusGeneradoNominaEspecial()
    End Sub

    Private Sub GrabarPDF(ByVal NoEmpleado As Integer, ByVal IdEmpresa As Integer, ByVal IdEjercicio As Integer, ByVal TipoNomina As Integer, ByVal Pdf As String)

        Dim cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = TipoNomina
        cNomina.Periodo = 0
        cNomina.Tipo = "U"
        cNomina.Pdf = Pdf
        cNomina.ActualizarEstatusPfNominaEspecial()
    End Sub

    Private Sub CargarPercepciones(ByVal IdEmpresa As Integer, ByVal Ejercicio As Integer, ByVal TipoNomina As String, ByVal NoEmpleado As Integer)
        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = Ejercicio
        cNomina.TipoNomina = TipoNomina
        cNomina.Periodo = 0
        cNomina.TipoConcepto = "P"
        cNomina.NoEmpleado = NoEmpleado
        cNomina.CvoConcepto = 50
        dt = cNomina.ConsultarDeduccionesEmpleado()
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

    Private Sub CargarDeducciones(ByVal IdEmpresa As Integer, ByVal Ejercicio As Integer, ByVal TipoNomina As String, ByVal NoEmpleado As Integer)
        Dim dt As New DataTable
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = Ejercicio
        cNomina.TipoNomina = TipoNomina
        cNomina.Periodo = 0
        cNomina.TipoConcepto = "D"
        cNomina.NoEmpleado = NoEmpleado
        cNomina.CvoConcepto = 86
        dt = cNomina.ConsultarDeduccionesEmpleado()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            If dt.Compute("Sum(Importe)", "") IsNot DBNull.Value Then
                TotalDeducciones = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
            Else
                TotalDeducciones = 0
            End If
        End If

    End Sub

    Private Sub btnGenerarPDF_Click(sender As Object, e As EventArgs) Handles btnGenerarPDF.Click
        rwConfirm.RadConfirm("¿Está seguro de generar los comprobantes pdf?", "confirmCallbackFnGeneraPDF", 330, 180, Nothing, "Confirmar")
    End Sub

    Private Sub btnConfirmarGeneraPDF_Click(sender As Object, e As EventArgs) Handles btnConfirmarGeneraPDF.Click
        Dim dt As New DataTable

        Dim cNomina As New Entities.Nomina()
        cNomina.Ejercicio = ConsultarEjercicio()
        cNomina.Periodo = 0
        cNomina.Tipo = "U"
        dt = cNomina.ConsultarEmpleadosGeneradosNominaEspecial()

        If dt.Rows.Count > 0 Then

            Dim Total As Integer = dt.Rows.Count
            Dim progress As RadProgressContext = RadProgressContext.Current
            progress.Speed = "N/A"
            Dim i As Integer = 0

            For Each row As DataRow In dt.Rows
                i += 1

                'Dim FilePath = Server.MapPath("~/PDF/U/") & String.Format("{0:00}", row("IdEmpresa")) & ConsultarEjercicio() & String.Format("{0:00}", row("NoEmpleado")) & ".pdf"

                Dim ruta As String = ""

                ruta = Server.MapPath("~/PDF/U/").ToString & "/" & ConsultarEjercicio().ToString

                If Not Directory.Exists(ruta) Then
                    Directory.CreateDirectory(ruta)
                End If

                GuardaPDF(GeneraPDFNoTimbrado(row("IdEmpresa"), row("Ejercicio"), row("TipoNomina"), row("NoEmpleado")), ruta & "/" & String.Format("{0:00}", row("IdEmpresa")) & "_" & String.Format("{0:00}", row("NoEmpleado")) & ".pdf")

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

            Response.Redirect("~/CalculoUtilidades.aspx")

        End If
    End Sub

    Private Function GeneraPDFNoTimbrado(ByVal IdEmpresa As Integer, ByVal Ejercicio As Integer, ByVal TipoNomina As String, ByVal NoEmpleado As Integer) As Telerik.Reporting.Report

        Dim reporte As New Formatos.formato_comisiones

        Dim numero_empleado As String = ""
        Dim periodo_pago As String = ""
        Dim fecha_inicial As String = ""
        Dim fecha_final As String = ""
        Dim regimen As String = ""
        Dim metodo_pago As String = ""
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

        Dim dt As DataTable = New DataTable()

        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = Ejercicio
        cNomina.TipoNomina = TipoNomina
        cNomina.Periodo = 0
        cNomina.TipoConcepto = "P"
        cNomina.NoEmpleado = NoEmpleado
        cNomina.CvoConcepto = 50
        dt = cNomina.ConsultarDeduccionesEmpleado()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            total_percepciones = Math.Round(dt.Compute("SUM(Importe)", ""), 6)
        End If

        cNomina = New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.Ejercicio = Ejercicio
        cNomina.TipoNomina = TipoNomina
        cNomina.Periodo = 0
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
        cNomina.Tipo = "U"
        dt = cNomina.ConsultarDatosNominaEspecial()

        Try

            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows

                    'Call ConsultarNumeroDeDiasPagados(NoEmpleado)

                    numero_empleado = NoEmpleado
                    periodo_pago = row("periodo_pago")
                    fecha_inicial = row("fecha_inicial")
                    fecha_final = row("fecha_final")
                    regimen = row("regimen")
                    metodo_pago = row("metodo_pago")
                    lugar_expedicion1 = row("lugar_expedicion1")
                    lugar_expedicion2 = row("lugar_expedicion2")
                    lugar_expedicion3 = row("lugar_expedicion3")
                    razonsocial = row("razonsocial")
                    fac_rfc = row("fac_rfc")

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

                    reporte.ReportParameters("IdEmpresa").Value = IdEmpresa
                    reporte.ReportParameters("NoEmpleado").Value = NoEmpleado
                    reporte.ReportParameters("Ejercicio").Value = Ejercicio
                    reporte.ReportParameters("TipoNomina").Value = 1 'Semanal
                    reporte.ReportParameters("Periodo").Value = 0

                    reporte.ReportParameters("plantillaId").Value = 4
                    reporte.ReportParameters("empleadoid").Value = empleadoid.ToString
                    reporte.ReportParameters("txtNoNomina").Value = "Recibo de pago"
                    reporte.ReportParameters("txtLugarExpedicion1").Value = lugar_expedicion1.ToString
                    reporte.ReportParameters("txtLugarExpedicion2").Value = lugar_expedicion2.ToString
                    reporte.ReportParameters("txtLugarExpedicion3").Value = lugar_expedicion3.ToString
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
                    reporte.ReportParameters("txtDiasPagados").Value = "0"

                    reporte.ReportParameters("txtPeriocidadPago").Value = periodo_pago.ToString
                    reporte.ReportParameters("txtFechaInicial").Value = fecha_inicial.ToString
                    reporte.ReportParameters("txtFechaFinal").Value = fecha_final.ToString
                    reporte.ReportParameters("txtMetodoPago").Value = metodo_pago.ToString
                    reporte.ReportParameters("txtBanco").Value = emp_banco.ToString
                    reporte.ReportParameters("txtClabe").Value = emp_clabe.ToString

                    reporte.ReportParameters("txtTotalPercepciones").Value = FormatCurrency(total_percepciones, 2).ToString
                    reporte.ReportParameters("txtTotalDeducciones").Value = FormatCurrency(total_deducciones, 2).ToString
                    reporte.ReportParameters("txtTotal").Value = FormatCurrency(total, 2).ToString
                    reporte.ReportParameters("txtCantidadLetra").Value = CantidadTexto.ToString.ToUpper
                    reporte.ReportParameters("paramImgBanner").Value = Server.MapPath("~/logos/ImgBanner.jpg")

                    Dim datos As New DataTable()
                    cNomina = New Nomina()
                    cNomina.IdEmpresa = IdEmpresa
                    cNomina.Ejercicio = Ejercicio
                    cNomina.TipoNomina = TipoNomina
                    cNomina.Periodo = 0
                    cNomina.TipoConcepto = "DE"
                    cNomina.CvoConcepto = 87
                    cNomina.NoEmpleado = NoEmpleado
                    datos = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                    cNomina = Nothing

                    If datos.Rows.Count > 0 Then
                        reporte.ReportParameters("txtEmpleadoSalarioDiario").Value = FormatCurrency(datos.Rows(0).Item("Importe"), 2).ToString
                    End If

                Next

                Call GrabarPDF(NoEmpleado, IdEmpresa, Ejercicio, TipoNomina, "S")

            End If
        Catch ex As Exception
            Call GrabarPDF(NoEmpleado, IdEmpresa, Ejercicio, TipoNomina, "N")
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

    Private Sub grdEmpleadosPTU_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles grdEmpleadosPTU.ItemCommand
        Select Case e.CommandName
            Case "cmdXML"
                'Call DownloadXML(e.CommandArgument)
            Case "cmdPDF"
                Dim IdEmpresa As Integer = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("IdEmpresa"))
                Dim NoEmpleado As Integer = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("NoEmpleado"))
                Call DownloadPDF(IdEmpresa, NoEmpleado)
            Case "cmdSend"
                Dim IdEmpresa As Integer = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("IdEmpresa"))
                Dim NoEmpleado As Integer = Convert.ToString(e.Item.OwnerTableView.DataKeyValues(e.Item.ItemIndex)("NoEmpleado"))
                Call EnviaEmail(IdEmpresa, NoEmpleado)
            Case "cmdDelete"
                empleadoId.Value = e.CommandArgument
                rwConfirm.RadConfirm("¿Realmente desea eliminar al trabajador de esta nomina?", "confirmCallbackFnEliminaEmpleado", 330, 180, Nothing, "Confirmar")
        End Select
    End Sub

    Private Sub EnviaEmail(ByVal IdEmpresa As Long, ByVal NoEmpleado As Long)

        Dim validos As String = ""
        Dim novalidos As String = ""

        Dim dt As New DataTable
        Dim cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = ConsultarEjercicio()
        cNomina.Periodo = 0
        cNomina.Tipo = "U"
        dt = cNomina.ConsultarDatosEnvioUtilidadesPDF()

        If dt.Rows.Count > 0 Then

            Dim Total As Integer = dt.Rows.Count
            Dim progress As RadProgressContext = RadProgressContext.Current
            progress.Speed = "N/A"
            Dim i As Integer = 0

            For Each row In dt.Rows
                i += 1
                Dim FilePath = Server.MapPath("~/PDF/U/").ToString & "/" & ConsultarEjercicio().ToString & "/" & String.Format("{0:00}", IdEmpresa) & "_" & String.Format("{0:00}", NoEmpleado) & ".pdf"
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

                Dim correo As String = row("Email").ToString.Trim
                'Dim correo As String = "ctorres@humantop.mx"
                Dim delimit As Char() = New Char() {";"c, ","c}

                Dim objMM As New MailMessage
                'objMM.To.Add("gesquivel@linkium.mx")
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

                        Dim nombre_empleado As String = CStr(row("Nombre"))
                        Dim fecha_inicial As String = CStr(row("FechaInicial"))
                        Dim fecha_final As String = CStr(row("FechaFinal"))

                        mensaje = "Estimado(a) " & nombre_empleado.ToString & vbCrLf & vbCrLf
                        mensaje = "Adjunto a este correo estamos enviándole el Comprobante de Nómina correspondiente al pago de utilidades. Cualquier duda o comentario estamos a sus órdenes." & vbCrLf
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
                        'Dim AttachXML As Net.Mail.Attachment
                        Dim AttachPDF As Net.Mail.Attachment
                        AttachPDF = New Net.Mail.Attachment(FilePath)
                        'objMM.Attachments.Add(AttachXML)
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

    Private Sub DownloadPDF(ByVal IdEmpresa As Long, ByVal NoEmpleado As Long)
        Dim FilePath = Server.MapPath("~/PDF/U/").ToString & "/" & ConsultarEjercicio().ToString & "/" & String.Format("{0:00}", IdEmpresa) & "_" & String.Format("{0:00}", NoEmpleado) & ".pdf"
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

    Private Sub btnEnvioComprobantes_Click(sender As Object, e As EventArgs) Handles btnEnvioComprobantes.Click
        rwConfirm.RadConfirm("¿Está seguro de enviar TODOS los comprobantes generados?", "confirmCallbackFnEnvioPDF", 330, 180, Nothing, "Confirmar")
    End Sub

    Private Sub GrabarEnviado(ByVal NoEmpleado As Integer, ByVal Ejercicio As Integer, ByVal Enviado As String)
        Dim cNomina = New Nomina()
        cNomina.NoEmpleado = NoEmpleado
        cNomina.Ejercicio = Ejercicio
        cNomina.Periodo = 0
        cNomina.Tipo = "U"
        cNomina.Enviado = Enviado
        cNomina.ActualizarEstatusEnviadoNominaPTU()
    End Sub

    Private Sub btnConfirmarEnvioPDF_Click(sender As Object, e As EventArgs) Handles btnConfirmarEnvioPDF.Click
        Dim validos As String = ""
        Dim novalidos As String = ""

        Dim dt As New DataTable
        Dim cNomina = New Nomina()
        cNomina.Ejercicio = ConsultarEjercicio()
        cNomina.Periodo = 0
        cNomina.Tipo = "U"
        dt = cNomina.ConsultarEmpleadosGeneradosNominaEspecial()

        If dt.Rows.Count > 0 Then

            Dim Total As Integer = dt.Rows.Count
            Dim progress As RadProgressContext = RadProgressContext.Current
            progress.Speed = "N/A"
            Dim i As Integer = 0

            For Each row In dt.Rows
                i += 1
                Dim FilePath = Server.MapPath("~/PDF/U/") & ConsultarEjercicio().ToString & "/" & String.Format("{0:00}", row("IdEmpresa")) & "_" & String.Format("{0:00}", row("NoEmpleado")) & ".pdf"
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
                'Dim correo As String = "ctorres@humantop.mx"
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

                        Dim nombre_empleado As String = CStr(row("Nombre"))
                        Dim fecha_inicial As String = CStr(row("FechaInicial"))
                        Dim fecha_final As String = CStr(row("FechaFinal"))

                        mensaje = "Estimado(a) " & nombre_empleado.ToString & vbCrLf & vbCrLf
                        mensaje = "Adjunto a este correo estamos enviándole el Comprobante de Nómina correspondiente al pago de utilidades. Cualquier duda o comentario estamos a sus órdenes." & vbCrLf
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
                        'Dim AttachXML As Net.Mail.Attachment
                        Dim AttachPDF As Net.Mail.Attachment

                        'AttachXML = New Net.Mail.Attachment(Server.MapPath("~\cfd_storage\nomina\") & "link_" & serie.ToString & folio.ToString & "_timbrado.xml")
                        AttachPDF = New Net.Mail.Attachment(FilePath)
                        'objMM.Attachments.Add(AttachXML)
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

    Private Sub btnEliminarEmpleado_Click(sender As Object, e As EventArgs) Handles btnEliminarEmpleado.Click
        Call EliminarTrabajador()
        Call CargarGridEmpleadosPTU()
    End Sub

    Private Sub EliminarTrabajador()
        Try

            Dim cNomina As New Nomina()
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.Ejercicio = ConsultarEjercicio()
            cNomina.Periodo = 0
            cNomina.Tipo = "U"
            cNomina.EliminaEmpleadoPTU()

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub


    'Public Sub CrearCFDNominEspecial(ByVal NoEmpleado As Integer, ByVal RutaXML As String)
    '    Dim Comprobante As XmlNode
    '    urlnomina = 0

    '    Dim dt As New DataTable
    '    Dim cEmpleado As New Entities.Empleado
    '    cEmpleado.IdEmpleado = NoEmpleado
    '    dt = cEmpleado.ConsultarEmpleados()

    '    Dim MetodoPago As String = ""
    '    If dt.Rows.Count > 0 Then
    '        MetodoPago = dt.Rows(0)("CLAVEMETODOPAGO").ToString
    '    End If

    '    m_xmlDOM = CrearDOM()
    '    Comprobante = CrearNodoComprobante(MetodoPago)

    '    m_xmlDOM.AppendChild(Comprobante)

    '    IndentarNodo(Comprobante, 1)

    '    CrearNodoEmisor(Comprobante)
    '    IndentarNodo(Comprobante, 1)

    '    CrearNodoReceptor(Comprobante, NoEmpleado)
    '    IndentarNodo(Comprobante, 1)

    '    '***** Conceptos del Recibo de Nomina  ***
    '    CrearNodoConceptos(Comprobante)
    '    IndentarNodo(Comprobante, 1)

    '    '**** Atributos de los Impuestos  ****
    '    CrearNodoImpuestos(Comprobante, NoEmpleado)
    '    IndentarNodo(Comprobante, 1)

    '    CrearNodoComplemento(Comprobante, NoEmpleado)
    '    IndentarNodo(Comprobante, 0)

    '    Dim path As String = Server.MapPath("~/Certificado/") & "CSD01_AAA010101AAA.cer"

    '    SellarCFD(Comprobante, path)
    '    m_xmlDOM.InnerXml = (Replace(m_xmlDOM.InnerXml, "schemaLocation", "xsi:schemaLocation", , , CompareMethod.Text))
    '    m_xmlDOM.Save(RutaXML)
    'End Sub

    'Private Function CrearDOM() As XmlDocument
    '    Dim oDOM As New XmlDocument
    '    Dim Nodo As XmlNode
    '    Nodo = oDOM.CreateProcessingInstruction("xml", "version=""1.0"" encoding=""utf-8""")
    '    oDOM.AppendChild(Nodo)
    '    Nodo = Nothing
    '    CrearDOM = oDOM
    'End Function

    'Private Function CrearNodoComprobante(ByVal metodoDePago As String) As XmlNode
    '    Dim Comprobante As XmlElement
    '    Comprobante = m_xmlDOM.CreateElement("cfdi:Comprobante", URI_SAT)
    '    CrearAtributosComprobante(Comprobante, metodoDePago)
    '    CrearNodoComprobante = Comprobante
    'End Function

    'Private Sub CrearAtributosComprobante(ByVal Nodo As XmlElement, ByVal metodoDePago As String)
    '    Dim LugarExpedicion As String = ""
    '    Dim dt As New DataTable
    '    Dim cNomina = New Nomina()
    '    dt = cNomina.ConsultarLugarExpedicion()

    '    If dt.Rows.Count > 0 Then
    '        For Each oDataRow In dt.Rows
    '            LugarExpedicion = oDataRow("LugarExpedicion")
    '        Next
    '    End If
    '    Nodo.SetAttribute("xmlns:nomina", "http://www.sat.gob.mx/nomina")
    '    Nodo.SetAttribute("xmlns:cfdi", "http://www.sat.gob.mx/cfd/3")
    '    Nodo.SetAttribute("xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance")
    '    Nodo.SetAttribute("xsi:schemaLocation", "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv32.xsd http://www.sat.gob.mx/nomina http://www.sat.gob.mx/sitio_internet/cfd/nomina/nomina11.xsd")
    '    Nodo.SetAttribute("certificado", "")
    '    Nodo.SetAttribute("fecha", Format(Now(), "yyyy-MM-ddTHH:mm:ss"))
    '    Nodo.SetAttribute("folio", FolioXml)
    '    Nodo.SetAttribute("formaDePago", "PAGO EN UNA SOLA EXHIBICION")
    '    Nodo.SetAttribute("metodoDePago", metodoDePago)
    '    Nodo.SetAttribute("noCertificado", "")
    '    Nodo.SetAttribute("sello", "")
    '    Nodo.SetAttribute("subTotal", TotalPercepciones)
    '    Nodo.SetAttribute("descuento", TotalDeducciones - Impuesto)
    '    Nodo.SetAttribute("motivoDescuento", "DEDUCCIONES NOMINA")
    '    Nodo.SetAttribute("tipoDeComprobante", "egreso")
    '    Nodo.SetAttribute("total", TotalPercepciones - TotalDeducciones)
    '    Nodo.SetAttribute("LugarExpedicion", LugarExpedicion)
    '    Nodo.SetAttribute("Moneda", "MXN")
    '    Nodo.SetAttribute("version", "3.2")
    'End Sub

    'Private Sub IndentarNodo(ByVal Nodo As XmlNode, ByVal Nivel As Long)
    '    Nodo.AppendChild(m_xmlDOM.CreateTextNode(vbNewLine & New String(ControlChars.Tab, Nivel)))
    'End Sub

    'Private Sub CrearNodoEmisor(ByVal Nodo As XmlNode)

    '    Dim dtEmisor As New DataTable
    '    Dim cNomina = New Nomina()
    '    dtEmisor = cNomina.ConsultarDatosEmisor()

    '    Dim Emisor As XmlElement
    '    Dim DomFiscal As XmlElement
    '    Dim ExpedidoEn As XmlElement
    '    Dim RegimenFiscal As XmlElement

    '    If dtEmisor.Rows.Count > 0 Then
    '        For Each oDataRow In dtEmisor.Rows
    '            Emisor = CrearNodo("cfdi:Emisor")
    '            Emisor.SetAttribute("nombre", oDataRow("RazonSocial"))
    '            'Emisor.SetAttribute("rfc", oDataRow("RFC"))
    '            Emisor.SetAttribute("rfc", "AAA010101AAA")

    '            IndentarNodo(Emisor, 2)

    '            DomFiscal = CrearNodo("cfdi:DomicilioFiscal")
    '            DomFiscal.SetAttribute("calle", oDataRow("Calle"))
    '            If oDataRow("NoExterior").Length > 0 Then
    '                DomFiscal.SetAttribute("noExterior", oDataRow("NoExterior"))
    '            End If
    '            If oDataRow("NoInterior").Length > 0 Then
    '                DomFiscal.SetAttribute("noInterior", oDataRow("NoInterior"))
    '            End If
    '            DomFiscal.SetAttribute("colonia", oDataRow("Colonia"))
    '            DomFiscal.SetAttribute("codigoPostal", oDataRow("CodigoPostal"))
    '            DomFiscal.SetAttribute("estado", oDataRow("Estado"))
    '            DomFiscal.SetAttribute("municipio", oDataRow("Municipio"))
    '            DomFiscal.SetAttribute("pais", "México")
    '            Emisor.AppendChild(DomFiscal)

    '            ExpedidoEn = CrearNodo("cfdi:ExpedidoEn")
    '            ExpedidoEn.SetAttribute("calle", oDataRow("Calle"))
    '            If oDataRow("NoExterior").Length > 0 Then
    '                ExpedidoEn.SetAttribute("noExterior", oDataRow("NoExterior"))
    '            End If
    '            If oDataRow("NoInterior").Length > 0 Then
    '                ExpedidoEn.SetAttribute("noInterior", oDataRow("NoInterior"))
    '            End If
    '            ExpedidoEn.SetAttribute("colonia", oDataRow("Colonia"))
    '            ExpedidoEn.SetAttribute("codigoPostal", oDataRow("CodigoPostal"))
    '            ExpedidoEn.SetAttribute("estado", oDataRow("Estado"))
    '            ExpedidoEn.SetAttribute("municipio", oDataRow("Municipio"))
    '            ExpedidoEn.SetAttribute("pais", "México")
    '            Emisor.AppendChild(ExpedidoEn)

    '            IndentarNodo(Emisor, 1)

    '            RegimenFiscal = CrearNodo("cfdi:RegimenFiscal")
    '            RegimenFiscal.SetAttribute("Regimen", oDataRow("Regimen"))
    '            Emisor.AppendChild(RegimenFiscal)
    '        Next
    '    End If

    '    IndentarNodo(Emisor, 2)

    '    Nodo.AppendChild(Emisor)

    'End Sub

    'Private Sub CrearNodoReceptor(ByVal Nodo As XmlNode, ByVal NoEmpleado As Integer)

    '    '

    '    Dim dtReceptor As New DataTable
    '    Dim cEmpleado As New Entities.Empleado
    '    cEmpleado.IdEmpleado = NoEmpleado
    '    'cEmpleado.IdEmpresa = IdEmpresa
    '    dtReceptor = cEmpleado.ConsultarEmpleados()

    '    Dim Receptor As XmlElement
    '    Dim Domicilio As XmlElement

    '    If dtReceptor.Rows.Count > 0 Then
    '        For Each oDataRow In dtReceptor.Rows
    '            Receptor = CrearNodo("cfdi:Receptor")
    '            Receptor.SetAttribute("nombre", oDataRow("NOMBRE"))
    '            Receptor.SetAttribute("rfc", oDataRow("RFC"))
    '            IndentarNodo(Receptor, 2)

    '            Domicilio = CrearNodo("cfdi:Domicilio")
    '            Domicilio.SetAttribute("calle", oDataRow("CALLE"))
    '            If oDataRow("NOEXT").Length > 0 Then
    '                Domicilio.SetAttribute("noExterior", oDataRow("NOEXT"))
    '            End If
    '            If oDataRow("NOINT").Length > 0 Then
    '                Domicilio.SetAttribute("noInterior", oDataRow("NOINT"))
    '            End If
    '            Domicilio.SetAttribute("colonia", oDataRow("COLONIA"))
    '            Domicilio.SetAttribute("codigoPostal", oDataRow("CP"))
    '            Domicilio.SetAttribute("municipio", oDataRow("MUNICIPIO"))
    '            Domicilio.SetAttribute("estado", oDataRow("ESTADO"))
    '            Domicilio.SetAttribute("pais", oDataRow("PAIS"))
    '            Receptor.AppendChild(Domicilio)
    '        Next
    '    End If

    '    IndentarNodo(Receptor, 1)

    '    Nodo.AppendChild(Receptor)
    'End Sub

    'Private Function CrearNodo(ByVal nombre As String)
    '    If urlnomina = 0 Then
    '        CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, nombre, URI_SAT)
    '    ElseIf urlnomina = 1 Then
    '        CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, nombre, "http://www.sat.gob.mx/nomina")
    '    ElseIf urlnomina = 2 Then
    '        CrearNodo = m_xmlDOM.CreateNode(XmlNodeType.Element, nombre, "")
    '    End If
    'End Function

    'Private Sub CrearNodoConceptos(ByVal Nodo As XmlNode)
    '    Dim Conceptos As XmlElement
    '    Dim Concepto As XmlElement

    '    Conceptos = CrearNodo("cfdi:Conceptos")
    '    IndentarNodo(Conceptos, 2)

    '    Concepto = CrearNodo("cfdi:Concepto")
    '    'Concepto.SetAttribute("importe", TotalPercepciones + Math.Abs(SubsidioAplicado) + Math.Abs(SubsidioEfectivo))
    '    Concepto.SetAttribute("importe", TotalPercepciones)
    '    'aqui va el subtotal
    '    'Concepto.SetAttribute("valorUnitario", TotalPercepciones + Math.Abs(SubsidioAplicado) + Math.Abs(SubsidioEfectivo))
    '    Concepto.SetAttribute("valorUnitario", TotalPercepciones)
    '    'aqui tambien va el subtotal
    '    Concepto.SetAttribute("descripcion", "PAGO DE NOMINA")
    '    'Concepto.SetAttribute("noIdentificacion", Mid(FolioXml, 4, 2)) '=====================================================================
    '    Concepto.SetAttribute("unidad", "SERVICIO")
    '    Concepto.SetAttribute("cantidad", "1")

    '    Conceptos.AppendChild(Concepto)
    '    IndentarNodo(Conceptos, 2)
    '    Nodo.AppendChild(Conceptos)
    'End Sub

    'Private Sub CrearNodoImpuestos(ByVal Nodo As XmlNode, ByVal NoEmpleado As Integer)

    '    Dim Impuestos As XmlElement
    '    Dim Retenciones As XmlElement
    '    Dim Retencion As XmlElement

    '    '

    '    Dim dt As New DataTable()
    '    Dim cNomina As New Nomina()
    '    cNomina.IdEmpresa = IdEmpresa
    '    cNomina.Ejercicio = IdEjercicio
    '    cNomina.TipoNomina = 1 'Semanal
    '    cNomina.Periodo = cmbPeriodo.SelectedValue
    '    cNomina.NoEmpleado = NoEmpleado
    '    cNomina.CvoConcepto = 86
    '    dt = cNomina.ConsultarConceptosEmpleado()

    '    If dt.Rows.Count >= 0 And dt.Compute("SUM(Importe)", "CvoConcepto=86") IsNot DBNull.Value Then
    '        Impuesto = dt.Compute("SUM(Importe)", "CvoConcepto=86")
    '    End If

    '    Impuestos = CrearNodo("cfdi:Impuestos")
    '    Impuestos.SetAttribute("totalImpuestosRetenidos", Impuesto)
    '    IndentarNodo(Impuestos, 2)
    '    Retenciones = CrearNodo("cfdi:Retenciones")
    '    Retencion = CrearNodo("cfdi:Retencion")
    '    Retencion.SetAttribute("importe", Impuesto) 'ISR GRAVADO=============================================================================
    '    ' el impuesto - el subsidio es igual al retenido 
    '    Retencion.SetAttribute("impuesto", "ISR")

    '    Retenciones.AppendChild(Retencion)

    '    Impuestos.AppendChild(Retenciones)
    '    IndentarNodo(Impuestos, 2)
    '    Nodo.AppendChild(Impuestos)
    'End Sub

    'Private Sub CrearNodoComplemento(ByVal Nodo As XmlNode, ByVal NoEmpleado As Integer)

    '    Dim FechaPago As Date = Now.Date()
    '    '

    '    Dim cPeriodo As New Entities.Periodo()
    '    cPeriodo.IdPeriodo = Periodo
    '    cPeriodo.ConsultarPeriodoID()

    '    Dim dtEmpleado As New DataTable
    '    Dim cNomina As New Nomina()
    '    cNomina.NoEmpleado = NoEmpleado
    '    cNomina.IdEmpresa = IdEmpresa
    '    cNomina.Ejercicio = IdEjercicio
    '    cNomina.TipoNomina = 1 'Semanal
    '    cNomina.Periodo = Periodo
    '    dtEmpleado = cNomina.ConsultarDatosEmpleadosGenerados()

    '    Dim Complemento As XmlElement
    '    Dim Nomina As XmlElement
    '    Dim Percepciones As XmlElement
    '    Dim Percepcion As XmlElement
    '    Dim Deducciones As XmlElement
    '    Dim Deduccion As XmlElement
    '    Dim Incapacidades As XmlElement
    '    Dim Incapacidad As XmlElement
    '    Dim SeparacionIndemnizacion As XmlElement
    '    Dim HorasExtra As XmlElement
    '    Dim Emisor As XmlElement
    '    Dim Receptor As XmlElement
    '    Dim OtrosPagos As XmlElement
    '    Dim OtroPago As XmlElement
    '    Dim SubsidioAlEmpleo As XmlElement

    '    Dim TotalSueldos As Decimal = 0
    '    Dim TotalExento As Decimal = 0
    '    Dim TotalGravado As Decimal = 0
    '    Dim TotalOtrasDeducciones As Decimal = 0
    '    Dim TotalImpuestosRetenidos As Decimal = 0
    '    Dim TotalPercepciones As Decimal = 0
    '    Dim TotalDeducciones As Decimal = 0
    '    Dim TotalHorasExtra As Decimal = 0
    '    Dim TotalOtrosPagos As Decimal = 0
    '    Dim TotalSeparacionIndemnizacion As Decimal = 0
    '    Dim ImporteExentoHorasExtra As Decimal = 0
    '    Dim ImporteGravadoHorasExtra As Decimal = 0
    '    Dim SubSidioEmpleo As Decimal = 0

    '    If dtEmpleado.Rows.Count > 0 Then
    '        For Each oDataRow In dtEmpleado.Rows

    '            Dim dt As New DataTable
    '            cNomina = New Nomina()
    '            cNomina.IdEmpresa = IdEmpresa
    '            cNomina.Ejercicio = IdEjercicio
    '            cNomina.TipoNomina = 1 'Semanal
    '            cNomina.Periodo = Periodo
    '            cNomina.TipoConcepto = "P"
    '            cNomina.NoEmpleado = NoEmpleado
    '            dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
    '            cNomina = Nothing

    '            If dt.Rows.Count > 0 Then
    '                '- Otros pagos
    '                '16 - Otros
    '                '17 - Subsidio para el empleo
    '                '- Horas extra
    '                '19 - Horas extra
    '                '- Indemnizaciones
    '                '22 - Prima por antigüedad
    '                '23 - Pagos por separación
    '                '25 - Indemnizaciones
    '                '- Jubilaciones
    '                '39 - Jubilaciones, pensiones o haberes de retiro
    '                '44 - Jubilaciones, pensiones o haberes de retiro en parcialidades
    '                If dt.Compute("SUM(Importe)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'") IsNot DBNull.Value Then
    '                    TotalSueldos = Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'"))
    '                End If
    '                If dt.Compute("SUM(Importe)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'") IsNot DBNull.Value Then
    '                    TotalPercepciones = Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'"))
    '                End If
    '                If dt.Compute("SUM(ImporteGravado)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'") IsNot DBNull.Value Then
    '                    TotalGravado = Convert.ToDecimal(dt.Compute("SUM(ImporteGravado)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'"))
    '                End If
    '                If dt.Compute("SUM(ImporteExento)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'") IsNot DBNull.Value Then
    '                    TotalExento = Convert.ToDecimal(dt.Compute("SUM(ImporteExento)", "CvoSAT<>'16' and CvoSAT<>'17' and CvoSAT<>'19' and CvoSAT<>'22' and CvoSAT<>'23' and CvoSAT<>'25' and CvoSAT<>'39' and CvoSAT<>'44'"))
    '                End If
    '                If dt.Compute("SUM(Importe)", "CvoSAT='19'") IsNot DBNull.Value Then
    '                    TotalHorasExtra = Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT='19'"))
    '                End If
    '                If dt.Compute("SUM(ImporteExento)", "CvoSAT='19'") IsNot DBNull.Value Then
    '                    ImporteExentoHorasExtra = Convert.ToDecimal(dt.Compute("SUM(ImporteExento)", "CvoSAT='19'"))
    '                End If
    '                If dt.Compute("SUM(ImporteGravado)", "CvoSAT='19'") IsNot DBNull.Value Then
    '                    ImporteGravadoHorasExtra = Convert.ToDecimal(dt.Compute("SUM(ImporteGravado)", "CvoSAT='19'"))
    '                End If
    '                If dt.Compute("SUM(Importe)", "CvoSAT='16' or CvoSAT='17' or CvoSAT='18'") IsNot DBNull.Value Then
    '                    TotalOtrosPagos = Convert.ToDecimal(dt.Compute("SUM(Importe)", "CvoSAT='16' or CvoSAT='17' or CvoSAT='18'"))
    '                End If
    '            End If

    '            dt = New DataTable
    '            cNomina = New Nomina()
    '            cNomina.IdEmpresa = IdEmpresa
    '            cNomina.Ejercicio = IdEjercicio
    '            cNomina.TipoNomina = 1 'Semanal
    '            cNomina.Periodo = Periodo
    '            cNomina.TipoConcepto = "D"
    '            cNomina.NoEmpleado = NoEmpleado
    '            dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
    '            cNomina = Nothing

    '            If dt.Rows.Count > 0 Then
    '                For i = 0 To dt.Rows.Count - 1
    '                    If Convert.ToDecimal(dt.Rows.Item(i)("Importe")) > 0 Then
    '                        TotalDeducciones += Convert.ToDecimal(Math.Round(dt.Rows.Item(i)("Importe"), 2))
    '                        If dt.Rows.Item(i)("CvoSAT").ToString = "2" Then
    '                            TotalImpuestosRetenidos += Convert.ToDecimal(dt.Rows.Item(i)("Importe").ToString)
    '                        End If
    '                    End If
    '                Next
    '            End If

    '            Complemento = CrearNodo("cfdi:Complemento")

    '            urlnomina = 1
    '            Nomina = CrearNodo("nomina12:Nomina")

    '            Call ConsultarNumeroDeDiasPagados(NoEmpleado)

    '            Nomina.SetAttribute("Version", "1.2")
    '            Nomina.SetAttribute("TipoNomina", oDataRow("TIPONOMINA"))
    '            Nomina.SetAttribute("FechaPago", Mid(FechaPago, 7, 4) + "-" + Mid(FechaPago, 4, 2) + "-" + Mid(FechaPago, 1, 2))
    '            Nomina.SetAttribute("FechaInicialPago", Format(CDate(cPeriodo.FechaInicial), "yyyy-MM-dd"))
    '            Nomina.SetAttribute("FechaFinalPago", Format(CDate(cPeriodo.FechaFinal), "yyyy-MM-dd"))
    '            Nomina.SetAttribute("NumDiasPagados", NumeroDeDiasPagados)
    '            Nomina.SetAttribute("TotalPercepciones", Math.Round(TotalPercepciones, 2))
    '            Nomina.SetAttribute("TotalDeducciones", Math.Round(TotalDeducciones, 2))
    '            Nomina.SetAttribute("TotalOtrosPagos", Math.Round(TotalOtrosPagos, 2))

    '            Emisor = CrearNodo("nomina12:Emisor")
    '            If oDataRow("REGISTROPATRONAL").ToString <> "" Then
    '                Emisor.SetAttribute("RegistroPatronal", oDataRow("REGISTROPATRONAL"))
    '            End If
    '            Emisor.SetAttribute("RfcPatronOrigen", oDataRow("RfcPatronOrigen"))

    '            'El atributo curp aplica solo cuando el regimen fiscal es 612 o 621
    '            'If Regimen = "612" or Regimen = "621" Then
    '            '   If oDataRow("curp_emisor").ToString <> "" Then
    '            '       Emisor.SetAttribute("Curp", oDataRow("curp_emisor"))
    '            '   End If
    '            'End If

    '            Nomina.AppendChild(Emisor)

    '            Receptor = CrearNodo("nomina12:Receptor")
    '            If oDataRow("CURP").ToString <> "" Then
    '                Receptor.SetAttribute("Curp", oDataRow("CURP"))
    '            End If
    '            If oDataRow("IMSS").ToString <> "" Then
    '                Receptor.SetAttribute("NumSeguridadSocial", oDataRow("IMSS"))
    '            End If
    '            Receptor.SetAttribute("FechaInicioRelLaboral", Format(CDate(oDataRow("FECHAINGRESO")), "yyyy-MM-dd"))
    '            Dim Antiguedad As String = ""
    '            cNomina = New Nomina()
    '            cNomina.Periodo = Periodo
    '            Antiguedad = cNomina.ConsultarAntiguedad(Convert.ToDateTime(oDataRow("FECHAINGRESO")).ToString("yyyy-MM-dd"))
    '            Receptor.SetAttribute("Antigüedad", Antiguedad)
    '            Receptor.SetAttribute("TipoContrato", oDataRow("TIPODECONTRATO"))
    '            Receptor.SetAttribute("TipoJornada", oDataRow("TIPODEJORNADA"))
    '            Receptor.SetAttribute("TipoRegimen", oDataRow("TIPOREGIMEN"))

    '            Receptor.SetAttribute("NumEmpleado", oDataRow("NOEMPLEADO"))
    '            If oDataRow("DEPARTAMENTO").ToString <> "" Then
    '                Receptor.SetAttribute("Departamento", oDataRow("DEPARTAMENTO"))
    '            End If
    '            If oDataRow("PUESTO").ToString <> "" Then
    '                Receptor.SetAttribute("Puesto", oDataRow("PUESTO"))
    '            End If
    '            If oDataRow("CLAVERIESGO").ToString <> "" Then
    '                Receptor.SetAttribute("RiesgoPuesto", oDataRow("CLAVERIESGO"))
    '            End If

    '            Receptor.SetAttribute("PeriodicidadPago", "02") '"Semanal"

    '            If oDataRow("CLAVEMETODOPAGO") <> "1" Then
    '                If oDataRow("CLABE").ToString.Length = 10 Or oDataRow("CLABE").ToString.Length = 11 Or oDataRow("CLABE").ToString.Length = 15 Or oDataRow("CLABE").ToString.Length = 16 Or oDataRow("CLABE").ToString.Length = 18 Then
    '                    If oDataRow("CLABE").ToString.Length = 18 Then
    '                        Receptor.SetAttribute("CuentaBancaria", oDataRow("CLABE"))
    '                    Else
    '                        Receptor.SetAttribute("CuentaBancaria", oDataRow("CLABE").ToString) 'El valor de este atributo debe tener una longitud de 10, 11, 16 ó 18 posiciones. Si se registra una cuenta CLABE (número con 18 posiciones), no debe existir el Catributo Banco.Se debe confirmar que el dígito de control es correcto. Si se registra una cuenta de tarjeta de débito a 16 posiciones o una cuenta bancaria a 11 posiciones o un número de teléfono celular a 10 posiciones, debe existir el banco.
    '                        Receptor.SetAttribute("Banco", oDataRow("BANCO"))
    '                    End If
    '                End If
    '            End If

    '            Receptor.SetAttribute("SalarioBaseCotApor", Math.Round(oDataRow("CUOTADIARIA")))
    '            Receptor.SetAttribute("SalarioDiarioIntegrado", Math.Round(oDataRow("INTEGRADOIMSS")))
    '            Receptor.SetAttribute("ClaveEntFed", oDataRow("clave_estado"))

    '            Dim SubContratacion As XmlElement
    '            SubContratacion = CrearNodo("nomina12:SubContratacion")
    '            SubContratacion.SetAttribute("RfcLabora", oDataRow("RfcLabora"))
    '            SubContratacion.SetAttribute("PorcentajeTiempo", "100")
    '            Receptor.AppendChild(SubContratacion)

    '            Nomina.AppendChild(Receptor)

    '            Percepciones = CrearNodo("nomina12:Percepciones")
    '            Percepciones.SetAttribute("TotalSueldos", Math.Round(TotalSueldos + TotalHorasExtra, 2))
    '            Percepciones.SetAttribute("TotalGravado", Math.Round(TotalGravado + ImporteGravadoHorasExtra, 2))
    '            Percepciones.SetAttribute("TotalExento", Math.Round(TotalExento + ImporteExentoHorasExtra, 2))

    '            Nomina.AppendChild(Percepciones)

    '            'Atributo condicional para expresar el importe exento y gravado de las claves tipo percepción 022 Prima por Antigüedad, 023 Pagos por separación y 025 Indemnizaciones.
    '            If TotalSeparacionIndemnizacion > 0 Then
    '                Percepciones.SetAttribute("TotalSeparacionIndemnizacion", Math.Round(TotalSeparacionIndemnizacion, 2))
    '            End If

    '            dt = New DataTable
    '            cNomina = New Nomina()
    '            cNomina.IdEmpresa = IdEmpresa
    '            cNomina.Ejercicio = IdEjercicio
    '            cNomina.TipoNomina = 1 'Semanal
    '            cNomina.Periodo = Periodo
    '            cNomina.TipoConcepto = "P"
    '            cNomina.NoEmpleado = NoEmpleado
    '            dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
    '            cNomina = Nothing
    '            '
    '            ' Percepciones
    '            '
    '            If dt.Rows.Count > 0 Then
    '                For i = 0 To dt.Rows.Count - 1
    '                    If Convert.ToDecimal(dt.Rows.Item(i)("Importe").ToString) > 0 And dt.Rows.Item(i)("CvoSAT").ToString <> "16" And dt.Rows.Item(i)("CvoSAT").ToString <> "17" And dt.Rows.Item(i)("CvoSAT").ToString <> "19" And dt.Rows.Item(i)("CvoSAT").ToString <> "22" And dt.Rows.Item(i)("CvoSAT").ToString <> "23" And dt.Rows.Item(i)("CvoSAT").ToString <> "25" Then

    '                        Percepcion = CrearNodo("nomina12:Percepcion")
    '                        Percepcion.SetAttribute("TipoPercepcion", String.Format("{0:000}", CInt(dt.Rows.Item(i)("CvoSAT"))))
    '                        Percepcion.SetAttribute("Clave", String.Format("{0:000}", CInt(dt.Rows.Item(i)("CvoConcepto").ToString)))
    '                        Percepcion.SetAttribute("Concepto", dt.Rows.Item(i)("Concepto").ToString)
    '                        Percepcion.SetAttribute("ImporteGravado", Math.Round(Convert.ToDecimal(dt.Rows.Item(i)("ImporteGravado")), 2))
    '                        Percepcion.SetAttribute("ImporteExento", Math.Round(Convert.ToDecimal(dt.Rows.Item(i)("ImporteExento")), 2))
    '                        Percepciones.AppendChild(Percepcion)
    '                    End If
    '                Next
    '            End If
    '            '
    '            ' SeparacionIndemnizacion (Finiquitos)
    '            '
    '            If dt.Rows.Count > 0 Then
    '                For i = 0 To dt.Rows.Count - 1
    '                    If Convert.ToDecimal(dt.Rows.Item(i)("Importe").ToString) > 0 And dt.Rows.Item(i)("CvoSAT").ToString = "22" Or dt.Rows.Item(i)("CvoSAT").ToString = "23" Or dt.Rows.Item(i)("CvoSAT").ToString = "25" Then
    '                        SeparacionIndemnizacion = CrearNodo("nomina12:SeparacionIndemnizacion")
    '                        SeparacionIndemnizacion.SetAttribute("TotalPagado", Math.Round(Convert.ToDecimal(dt.Rows.Item(i)("Importe")), 2))
    '                        SeparacionIndemnizacion.SetAttribute("NumAñosServicio", 1)
    '                        SeparacionIndemnizacion.SetAttribute("UltimoSueldoMensOrd", Math.Round(Convert.ToDecimal(dt.Rows.Item(i)("Importe")), 2))
    '                        SeparacionIndemnizacion.SetAttribute("IngresoAcumulable", 1)
    '                        SeparacionIndemnizacion.SetAttribute("IngresoNoAcumulable", 1)
    '                        Percepciones.AppendChild(SeparacionIndemnizacion)
    '                    End If
    '                Next
    '            End If

    '            If TotalSeparacionIndemnizacion > 0 Then
    '                Percepciones.SetAttribute("TotalSeparacionIndemnizacion", Math.Round(TotalSeparacionIndemnizacion, 2))
    '            End If

    '            ' Horas Extra

    '            dt = New DataTable
    '            cNomina = New Nomina()
    '            cNomina.IdEmpresa = IdEmpresa
    '            cNomina.Ejercicio = IdEjercicio
    '            cNomina.TipoNomina = 1 'Semanal
    '            cNomina.Periodo = Periodo
    '            cNomina.TipoConcepto = "P"
    '            cNomina.CvoSAT = "19"
    '            cNomina.NoEmpleado = NoEmpleado
    '            dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
    '            cNomina = Nothing

    '            If dt.Rows.Count > 0 Then
    '                Percepcion = CrearNodo("nomina12:Percepcion")
    '                Percepcion.SetAttribute("Clave", "019")
    '                Percepcion.SetAttribute("Concepto", "Horas extra")
    '                Percepcion.SetAttribute("ImporteExento", Math.Round(ImporteExentoHorasExtra, 2))
    '                Percepcion.SetAttribute("ImporteGravado", Math.Round(ImporteGravadoHorasExtra, 2))
    '                Percepcion.SetAttribute("TipoPercepcion", "019")

    '                For i = 0 To dt.Rows.Count - 1
    '                    If Convert.ToDecimal(dt.Rows.Item(i)("Importe")) > 0 Then
    '                        HorasExtra = CrearNodo("nomina12:HorasExtra")
    '                        HorasExtra.SetAttribute("Dias", Convert.ToDecimal(dt.Rows.Item(i)("DiasHorasExtra")))
    '                        HorasExtra.SetAttribute("TipoHoras", dt.Rows.Item(i)("TipoHorasExtra"))
    '                        HorasExtra.SetAttribute("HorasExtra", Convert.ToDecimal(dt.Rows.Item(i)("Unidad")))
    '                        HorasExtra.SetAttribute("ImportePagado", Math.Round(Convert.ToDecimal(dt.Rows.Item(i)("Importe")), 2))
    '                        Percepcion.AppendChild(HorasExtra)
    '                    End If
    '                Next

    '                Percepciones.AppendChild(Percepcion)

    '            End If

    '            Nomina.AppendChild(Percepciones)
    '            '
    '            ' Deducciones
    '            '
    '            Deducciones = CrearNodo("nomina12:Deducciones")
    '            Deducciones.SetAttribute("TotalOtrasDeducciones", Math.Round(TotalDeducciones - TotalImpuestosRetenidos, 2))
    '            If TotalImpuestosRetenidos > 0 Then
    '                Deducciones.SetAttribute("TotalImpuestosRetenidos", Math.Round(TotalImpuestosRetenidos, 2))
    '            End If

    '            Nomina.AppendChild(Deducciones)

    '            dt = New DataTable
    '            cNomina = New Nomina()
    '            cNomina.IdEmpresa = IdEmpresa
    '            cNomina.Ejercicio = IdEjercicio
    '            cNomina.TipoNomina = 1 'Semanal
    '            cNomina.Periodo = Periodo
    '            cNomina.TipoConcepto = "D"
    '            cNomina.NoEmpleado = NoEmpleado
    '            dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
    '            cNomina = Nothing

    '            If dt.Rows.Count > 0 Then
    '                Dim oDataRowDeducciones As DataRow
    '                For Each oDataRowDeducciones In dt.Rows
    '                    ObtenerUnidad(NoEmpleado, oDataRowDeducciones("CvoConcepto").ToString)
    '                    Deduccion = CrearNodo("nomina12:Deduccion")
    '                    Deduccion.SetAttribute("TipoDeduccion", String.Format("{0:000}", Convert.ToInt32(oDataRowDeducciones("CvoSAT"))))
    '                    Deduccion.SetAttribute("Clave", String.Format("{0:000}", oDataRowDeducciones("CvoConcepto")) + Unidad.ToString)
    '                    Deduccion.SetAttribute("Concepto", oDataRowDeducciones("Concepto").ToString)
    '                    Deduccion.SetAttribute("Importe", Math.Round(Convert.ToDecimal(oDataRowDeducciones("Importe")), 2))
    '                    Deducciones.AppendChild(Deduccion)
    '                Next
    '                Nomina.AppendChild(Deducciones)
    '            End If
    '            '
    '            ' Otros pagos
    '            '
    '            dt = New DataTable
    '            cNomina = New Nomina()
    '            cNomina.IdEmpresa = IdEmpresa
    '            cNomina.Ejercicio = IdEjercicio
    '            cNomina.TipoNomina = 1 'Semanal
    '            cNomina.Periodo = Periodo
    '            cNomina.TipoConcepto = "P"
    '            cNomina.NoEmpleado = NoEmpleado
    '            dt = cNomina.ConsultarOtrosPagos()
    '            cNomina = Nothing

    '            If dt.Rows.Count > 0 Then
    '                If TotalOtrosPagos > 0 Then
    '                    OtrosPagos = CrearNodo("nomina12:OtrosPagos")
    '                End If

    '                For i = 0 To dt.Rows.Count - 1
    '                    If dt.Rows.Item(i)("CvoSAT").ToString = "16" Then ' Otros (Cátalogo Anterior)
    '                        OtroPago = CrearNodo("nomina12:OtroPago")
    '                        OtroPago.SetAttribute("TipoOtroPago", "999")
    '                        OtroPago.SetAttribute("Clave", "016")
    '                        OtroPago.SetAttribute("Concepto", dt.Rows.Item(i)("Concepto").ToString)
    '                        OtroPago.SetAttribute("Importe", Math.Round(Convert.ToDecimal(dt.Rows.Item(i)("Importe")), 2))
    '                        OtrosPagos.AppendChild(OtroPago)
    '                    End If

    '                    If dt.Rows.Item(i)("CvoSAT").ToString = "17" Then ' Otros (Subsidio para el empleo)
    '                        OtroPago = CrearNodo("nomina12:OtroPago")
    '                        OtroPago.SetAttribute("TipoOtroPago", "002")
    '                        OtroPago.SetAttribute("Clave", "017")
    '                        OtroPago.SetAttribute("Concepto", dt.Rows.Item(i)("Concepto").ToString)
    '                        OtroPago.SetAttribute("Importe", Math.Round(Convert.ToDecimal(dt.Rows.Item(i)("Importe")), 2))

    '                        SubsidioAlEmpleo = CrearNodo("nomina12:SubsidioAlEmpleo")
    '                        SubsidioAlEmpleo.SetAttribute("SubsidioCausado", Math.Round(Convert.ToDecimal(dt.Rows.Item(i)("Importe")), 2))
    '                        OtroPago.AppendChild(SubsidioAlEmpleo)
    '                        OtrosPagos.AppendChild(OtroPago)
    '                    End If
    '                Next

    '                Nomina.AppendChild(OtrosPagos)

    '            End If

    '            IndentarNodo(Nomina, 1)
    '            Nodo.AppendChild(Nomina)

    '            Complemento.AppendChild(Nomina)
    '            IndentarNodo(Complemento, 1)
    '            urlnomina = 0

    '            IndentarNodo(Complemento, 1)
    '            Nodo.AppendChild(Complemento)

    '        Next
    '    End If
    'End Sub

    'Private Sub SellarCFD(ByVal NodoComprobante As XmlElement, ByVal Certificado As String)
    '    Dim objCert As New X509Certificate2()
    '    'Pasarle el nombre y ruta del Cerfificado para obtener la información en bytes
    '    Dim bRawData As Byte() = ReadFile(Certificado)
    '    objCert.Import(bRawData)
    '    Dim cadena As String = Convert.ToBase64String(bRawData)
    '    'Comentando las dos lineas siguientes no agrega el certificado al comprobante xml
    '    NodoComprobante.SetAttribute("noCertificado", FormatearSerieCert(objCert.SerialNumber))
    '    NodoComprobante.SetAttribute("certificado", Convert.ToBase64String(bRawData))
    '    'Comentando la siguiente linea no agregar el sello al comprobante xml
    '    NodoComprobante.SetAttribute("sello", GenerarSello())
    'End Sub

    'Function ReadFile(ByVal strArchivo As String) As Byte()
    '    Dim f As New FileStream(strArchivo, FileMode.Open, FileAccess.Read)
    '    Dim size As Integer = CInt(f.Length)
    '    Dim data As Byte() = New Byte(size - 1) {}
    '    size = f.Read(data, 0, size)
    '    f.Close()
    '    Return data
    'End Function

    'Public Function FormatearSerieCert(ByVal Serie As String) As String
    '    Dim Resultado As String = ""
    '    Dim I As Integer
    '    For I = 2 To Len(Serie) Step 2
    '        Resultado = Resultado & Mid(Serie, I, 1)
    '    Next
    '    FormatearSerieCert = Resultado
    'End Function

    'Private Function GenerarSello() As String
    '    'Dim ArchivoPFX As String = Server.MapPath("~/PKI/") & "00001000000404558338.pfx"
    '    Dim ArchivoPFX As String = Server.MapPath("~/PKI/") & "CSD01_AAA010101AAA.pfx"
    '    Dim objCertPfx As New X509Certificate2(ArchivoPFX, "12345678a")
    '    Dim lRSA As RSACryptoServiceProvider = objCertPfx.PrivateKey
    '    Dim lhasher As New SHA1CryptoServiceProvider()
    '    Dim bytesFirmados As Byte() = lRSA.SignData(System.Text.Encoding.UTF8.GetBytes(GetCadenaOriginal(m_xmlDOM.InnerXml)), lhasher)
    '    Return Convert.ToBase64String(bytesFirmados)
    'End Function

    'Public Function GetCadenaOriginal(ByVal xmlCFD As String) As String
    '    Dim xslt As New Xsl.XslCompiledTransform
    '    Dim xmldoc As New XmlDocument
    '    Dim navigator As XPath.XPathNavigator
    '    Dim output As New IO.StringWriter
    '    xmldoc.LoadXml(xmlCFD)
    '    navigator = xmldoc.CreateNavigator()
    '    xslt.Load(Server.MapPath("~/SAT/") & "cadenaoriginal_3_2.xslt")
    '    xslt.Transform(navigator, Nothing, output)
    '    GetCadenaOriginal = output.ToString
    'End Function

End Class