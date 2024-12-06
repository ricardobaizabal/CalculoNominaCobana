Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Entities
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Public Class RptNominaQuincenal
    Inherits System.Web.UI.Page

    Private dtPercepciones As New DataTable
    Private dtDeducciones As New DataTable

    Private dtGridPercepciones As New DataTable
    Private dtGridDeducciones As New DataTable

    Private dtTempNominas As New DataTable

    Private totalPercepciones As Double = 0
    Private totalDeducciones As Double = 0
    Private totalNetoPercepciones As Double = 0
    Private totalNetoDeducciones As Double = 0
    Private totalGravado As Double = 0
    Private totalExento As Double = 0
    Private NoEmpleado As Integer = 0
    Private NumeroDeDiasPagados As Double = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request("e")) Then
                IdEmpresa.Value = Request("e")
            End If
            If Not String.IsNullOrEmpty(Request("c")) Then
                IdCliente.Value = Request("c")
            End If
            If Not String.IsNullOrEmpty(Request("ej")) Then
                IdEjercicio.Value = Request("ej")
            End If
            If Not String.IsNullOrEmpty(Request("ej")) Then
                IdEjercicio.Value = Request("ej")
            End If
            If Not String.IsNullOrEmpty(Request("periodicidad")) Then
                IdPeriodicidad.Value = Request("periodicidad")
            End If
            If Not String.IsNullOrEmpty(Request("especial")) Then
                EspecialBool.Value = Request("especial")
            End If

            If Not String.IsNullOrEmpty(Request("p")) Then

                IdPeriodo.Value = Request("p")

                Dim ObjData As New DataControl(0)
                Dim ds As New DataSet
                Dim p As New ArrayList
                p.Add(New SqlParameter("@cmd", 2))
                p.Add(New SqlParameter("@clienteid", IdCliente.Value))
                ds = ObjData.FillDataSet("pMisClientes", p)
                ObjData = Nothing

                If ds.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In ds.Tables(0).Rows
                        lblEmpresa.Text = row("razonsocial")
                        lblDireccion.Text = row("expedicionLinea")
                    Next
                End If

                Dim cPeriodo As New Entities.Periodo
                cPeriodo.IdPeriodo = IdPeriodo.Value
                cPeriodo.ConsultarPeriodoID()

                Dim nomina_name As String = ""

                If EspecialBool.Value = "1" Then
                    nomina_name = " EXTRAORDINARIA "
                Else
                    If IdPeriodicidad.Value = "1" Then
                        nomina_name = " Quincenal "
                    ElseIf IdPeriodicidad.Value = "2" Then
                        nomina_name = " QUINCENAL "
                    ElseIf IdPeriodicidad.Value = "3" Then
                        nomina_name = " QUINCENAL "
                    ElseIf IdPeriodicidad.Value = "4" Then
                        nomina_name = " MENSUAL "
                    End If
                End If

                Dim dt As New DataTable()
                Dim cNomina As New Nomina()
                cNomina.IdEmpresa = IdEmpresa.Value
                cNomina.IdCliente = IdCliente.Value
                cNomina.Ejercicio = IdEjercicio.Value
                cNomina.TipoNomina = IdPeriodicidad.Value 'Quincenal
                cNomina.Periodo = IdPeriodo.Value
                cNomina.EsEspecial = False
                dt = cNomina.ConsultarDatosGeneralesNomina()

                lblPeriodoTitulo.Text = "NÓMINA" & nomina_name & "DEL PERIODO: " & cPeriodo.FechaInicial.ToString & " AL " & cPeriodo.FechaFinal.ToString

                If dt.Rows.Count > 0 Then
                    Me.lblTitulo.Text = "Periodo " & dt.Rows(0)("RangoFecha").ToString()
                    Me.lblNoNomina.Text = Session("Folio").ToString
                    Me.lblEjercicio.Text = dt.Rows(0)("Ejercicio").ToString()
                    Me.lblRazonSocial.Text = dt.Rows(0)("Cliente").ToString()
                    Me.lblNoPeriodo.Text = dt.Rows(0)("Periodo").ToString()
                    Me.lblTipoNomina.Text = "Quincenal"
                    Me.lblFechaInicial.Text = dt.Rows(0)("FechaInicial").ToString()
                    Me.lblFechaFinal.Text = dt.Rows(0)("FechaFinal").ToString()
                    Me.lblDias.Text = dt.Rows(0)("Dias").ToString()
                End If

                Call CargarGridNominas()

                'Call MostrarPercepciones()
                'Call MostrarDeducciones()
                'Call MostrarTotalNeto()

            End If
        End If
    End Sub
    'Private Sub CargarVariablesGenerales()

    '    Dim dt As New DataTable()
    '    Dim cConfiguracion = New Configuracion()
    '    cConfiguracion.IdEmpresa = Session("IdEmpresa")
    '    cConfiguracion.IdUsuario = Session("usuarioid")
    '    dt = cConfiguracion.ConsultarConfiguracion()
    '    cConfiguracion = Nothing

    '    If dt.Rows.Count > 0 Then
    '        For Each oDataRow In dt.Rows
    '            IdEmpresa.Value = oDataRow("IdEmpresa")
    '            IdEjercicio.Value = oDataRow("IdEjercicio")
    '        Next
    '    End If
    'End Sub
    Private Sub CargarGridNominas()

        'Call CargarVariablesGenerales()

        ' Crear la tabla temporal
        dtTempNominas = New DataTable()

        Dim cNomina As New Nomina()
        cNomina.cmd = 1
        cNomina.Ejercicio = IdEjercicio.Value
        cNomina.TipoNomina = IdPeriodicidad.Value 'Quincenal
        cNomina.Periodo = IdPeriodo.Value
        cNomina.IdEmpresa = IdEmpresa.Value
        cNomina.IdCliente = IdCliente.Value
        ' Obtener los datos de nómina
        dtTempNominas = cNomina.ConsultarResumenNominas()

        'If dtTempNominas.Rows.Count <> 0 Then
        '    ' Calcular la suma de las columnas, ignorando "Nombre" y "Clave"
        '    Dim totalRow As DataRow = dtTempNominas.NewRow()
        '    For Each col As DataColumn In dtTempNominas.Columns
        '        If col.ColumnName <> "Nombre" And col.ColumnName <> "Clave" Then
        '            Dim total As Decimal = 0
        '            For Each row As DataRow In dtTempNominas.Rows
        '                Dim cellValue As Decimal
        '                ' Manejar valores null como 0.00
        '                If Not IsDBNull(row(col)) Then
        '                    cellValue = Convert.ToDecimal(row(col))
        '                Else
        '                    cellValue = 0.00D
        '                End If
        '                total += cellValue
        '            Next
        '            totalRow(col) = total ' Asignar el total a la nueva fila
        '        End If
        '    Next
        '    ' Añadir la fila de totales al DataTable
        '    dtTempNominas.Rows.Add(totalRow)
        'End If

        ' Asignar los datos al Grid
        GridNominas.DataSource = dtTempNominas
        GridNominas.DataBind()

        ' Verificar si hay filas en el DataSource para habilitar o deshabilitar el botón
        If GridNominas.DataSource IsNot Nothing AndAlso CType(GridNominas.DataSource, DataTable).Rows.Count > 0 Then
            btnGenerarPDF.Enabled = True ' Habilitar el botón si hay filas
        Else
            btnGenerarPDF.Enabled = False ' Deshabilitar el botón si no hay filas
        End If
    End Sub
    Private Sub btnGenerarPDF_Click(sender As Object, e As EventArgs) Handles btnGenerarPDF.Click
        'CargarGridNominas() ' Asegúrate de que los datos se carguen primero
        GenerarPDF()
    End Sub
    Private Sub GenerarPDF()

        ' Obtener el cliente seleccionado (nombre)
        Dim clienteSeleccionado As String = ""

        ' Obtener el periodo seleccionado (nombre)
        Dim periodoSeleccionado As String = ""

        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa.Value
        cNomina.IdCliente = IdCliente.Value
        cNomina.Ejercicio = IdEjercicio.Value
        cNomina.TipoNomina = IdPeriodicidad.Value 'Quincenal
        cNomina.Periodo = IdPeriodo.Value
        dt = cNomina.ConsultarDatosGeneralesNomina()
        cNomina = Nothing

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                clienteSeleccionado = oDataRow("Cliente")
                periodoSeleccionado = oDataRow("RangoFecha")
            Next
        End If

        dtTempNominas = New DataTable()

        cNomina = New Nomina()
        cNomina.cmd = 1
        cNomina.Ejercicio = IdEjercicio.Value
        cNomina.TipoNomina = IdPeriodicidad.Value 'Quincenal
        cNomina.Periodo = IdPeriodo.Value
        cNomina.IdEmpresa = IdEmpresa.Value
        cNomina.IdCliente = IdCliente.Value
        ' Obtener los datos de nómina
        dtTempNominas = cNomina.ConsultarResumenNominas()

        If dtTempNominas.Rows.Count <> 0 Then
            ' Calcular la suma de las columnas, ignorando "Nombre" y "Clave"
            Dim totalRow As DataRow = dtTempNominas.NewRow()
            For Each col As DataColumn In dtTempNominas.Columns
                If col.ColumnName <> "Nombre" And col.ColumnName <> "Clave" Then
                    Dim total As Decimal = 0
                    For Each row As DataRow In dtTempNominas.Rows
                        Dim cellValue As Decimal
                        ' Manejar valores null como 0.00
                        If Not IsDBNull(row(col)) Then
                            cellValue = Convert.ToDecimal(row(col))
                        Else
                            cellValue = 0.00D
                        End If
                        total += cellValue
                    Next
                    totalRow(col) = total ' Asignar el total a la nueva fila
                End If
            Next
            ' Añadir la fila de totales al DataTable
            dtTempNominas.Rows.Add(totalRow)
        End If

        ' Reemplazar el símbolo "/" por "-"
        Dim periodoSimboloCambiado As String = periodoSeleccionado.Replace("/", "-")

        ' Verificar si dtTempNominas es Nothing
        If dtTempNominas Is Nothing OrElse dtTempNominas.Rows.Count = 0 Then
            ' Mostrar un mensaje de error o manejar el caso donde no hay datos
            Response.Write("<script>alert('No hay datos para generar el PDF.');</script>")
            Return
        End If

        ' Crear un documento PDF en orientación horizontal (landscape)
        Dim documento As New Document(PageSize.LEGAL.Rotate())

        Dim FilePath As String

        FilePath = Server.MapPath("~/ResumenNominaPDF/Quincenal/")

        ' Validar si la carpeta ya existe
        If Not Directory.Exists(FilePath) Then
            Directory.CreateDirectory(FilePath) ' Crear la carpeta si no existe
        End If

        Dim archivoPdf As String = Path.Combine(FilePath, "ResumenNominaCatorcenal " + periodoSimboloCambiado + ".pdf")

        PdfWriter.GetInstance(documento, New FileStream(archivoPdf, FileMode.Create))

        ' Abrir el documento para escribir
        documento.Open()

        ' Agregar un título
        Dim titulo As New Paragraph("RESUMEN DE NÓMINA QUINCENAL", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, New BaseColor(23, 59, 95)))
        titulo.Alignment = Element.ALIGN_LEFT ' Alineación a la izquierda
        documento.Add(titulo)

        ' Agregar un salto de línea de la mitad del valor actual (6 puntos)
        'documento.Add(New Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 6)) With {.SpacingBefore = 1.0F})

        ' Información del cliente
        Dim infoCliente As New Paragraph("MEDIOS AVANZADOS EN COMUNICACIÓN SA DE CV", FontFactory.GetFont(FontFactory.HELVETICA, 10, New BaseColor(23, 59, 95)))
        infoCliente.Alignment = Element.ALIGN_LEFT ' Alineación a la izquierda
        documento.Add(infoCliente)

        ' Agregar un salto de línea de la mitad del valor actual (6 puntos)
        documento.Add(New Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 2)) With {.SpacingBefore = 0.5F})

        ' Crear una tabla con 2 columnas
        Dim tablaCliente As New PdfPTable(2)
        tablaCliente.WidthPercentage = 30 ' Ancho al 30%
        tablaCliente.HorizontalAlignment = Element.ALIGN_LEFT

        ' Definir los anchos de las columnas
        Dim anchosCliente() As Single = {1.0F, 2.0F} ' Ajusta los anchos según sea necesario
        tablaCliente.SetWidths(anchosCliente)

        ' Añadir la información del cliente
        Dim paddingCliente As Single = 1.0F ' Ajusta el padding para que las celdas estén más juntas

        tablaCliente.AddCell(New PdfPCell(New Phrase("PLAZA:", FontFactory.GetFont(FontFactory.HELVETICA, 10, New BaseColor(23, 59, 95)))) With {.BorderWidth = 0, .Padding = paddingCliente})
        tablaCliente.AddCell(New PdfPCell(New Phrase("NUEVO LEON", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, New BaseColor(23, 59, 95)))) With {.BorderWidth = 0, .Padding = paddingCliente})

        tablaCliente.AddCell(New PdfPCell(New Phrase("SUCURSAL:", FontFactory.GetFont(FontFactory.HELVETICA, 10, New BaseColor(23, 59, 95)))) With {.BorderWidth = 0, .Padding = paddingCliente})
        tablaCliente.AddCell(New PdfPCell(New Phrase("MONTERREY", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, New BaseColor(23, 59, 95)))) With {.BorderWidth = 0, .Padding = paddingCliente})

        ' Agregar la tabla al documento
        documento.Add(tablaCliente)

        ' Agregar un salto de línea de la mitad del valor actual (6 puntos)
        'documento.Add(New Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 6)) With {.SpacingBefore = 1.0F})

        ' Crear una tabla con 2 columnas
        Dim tablaInformacion As New PdfPTable(2)
        tablaInformacion.WidthPercentage = 30 ' Ancho al 30%
        tablaInformacion.HorizontalAlignment = Element.ALIGN_LEFT

        ' Definir los anchos de las columnas (ajusta los valores para que estén más juntas)
        Dim anchosInformacion() As Single = {1.5F, 2.5F} ' Ajusta el ancho según sea necesario
        tablaInformacion.SetWidths(anchosInformacion)

        ' Añadir los encabezados y valores
        ' Reducir el padding para que las celdas estén más juntas
        Dim padding As Single = 1.5F ' Puedes reducir este valor

        tablaInformacion.AddCell(New PdfPCell(New Phrase("CLIENTE:", FontFactory.GetFont(FontFactory.HELVETICA, 8))) With {.BorderWidth = 0, .PaddingRight = padding})
        tablaInformacion.AddCell(New PdfPCell(New Phrase(clienteSeleccionado, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8))) With {.BorderWidth = 0, .PaddingLeft = padding})

        'tablaInformacion.AddCell(New PdfPCell(New Phrase("NUM. DE NÓMINA:", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8))) With {.BorderWidth = 0, .PaddingRight = padding})
        'tablaInformacion.AddCell(New PdfPCell(New Phrase("59", FontFactory.GetFont(FontFactory.HELVETICA, 8))) With {.BorderWidth = 0, .PaddingLeft = padding})

        tablaInformacion.AddCell(New PdfPCell(New Phrase("PERIODO:", FontFactory.GetFont(FontFactory.HELVETICA, 8))) With {.BorderWidth = 0, .PaddingRight = padding})
        tablaInformacion.AddCell(New PdfPCell(New Phrase(periodoSeleccionado, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8))) With {.BorderWidth = 0, .PaddingLeft = padding})

        tablaInformacion.AddCell(New PdfPCell(New Phrase("TIPO DE NÓMINA:", FontFactory.GetFont(FontFactory.HELVETICA, 8))) With {.BorderWidth = 0, .PaddingRight = padding})
        tablaInformacion.AddCell(New PdfPCell(New Phrase("ORDINARIA", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8))) With {.BorderWidth = 0, .PaddingLeft = padding})

        tablaInformacion.AddCell(New PdfPCell(New Phrase("MONEDA:", FontFactory.GetFont(FontFactory.HELVETICA, 8))) With {.BorderWidth = 0, .PaddingRight = padding})
        tablaInformacion.AddCell(New PdfPCell(New Phrase("MXP PESOS", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8))) With {.BorderWidth = 0, .PaddingLeft = padding})

        tablaInformacion.AddCell(New PdfPCell(New Phrase("CENTRO DE COSTOS:", FontFactory.GetFont(FontFactory.HELVETICA, 8))) With {.BorderWidth = 0, .PaddingRight = padding})
        tablaInformacion.AddCell(New PdfPCell(New Phrase("TODOS LOS CENTROS DE COSTO", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8))) With {.BorderWidth = 0, .PaddingLeft = padding})

        ' Agregar la tabla al documento
        documento.Add(tablaInformacion)

        ' Agregar un salto de línea de la mitad del valor actual (6 puntos)
        'documento.Add(New Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 8)) With {.SpacingBefore = 6.0F})

        ' Crear tabla para los datos de las nóminas
        Dim tabla As New PdfPTable(dtTempNominas.Columns.Count)
        tabla.WidthPercentage = 100

        ' Definir los anchos de las columnas
        Dim anchos(dtTempNominas.Columns.Count - 1) As Single
        For i As Integer = 0 To dtTempNominas.Columns.Count - 1
            If dtTempNominas.Columns(i).ColumnName = "Nombre" Then
                anchos(i) = 2.5F ' Ancho más grande para la columna "Nombre"
            Else
                anchos(i) = 1.0F ' Ancho estándar para las columnas numéricas
            End If
        Next
        tabla.SetWidths(anchos)

        ' Diccionario para los nuevos nombres de encabezados
        Dim nuevosEncabezados As New Dictionary(Of String, String) From {
            {"Clave", "CLAVE"},
            {"Nombre", "NOMBRE DEL EMPLEADO"},
            {"SueldosSalarios", "SUELDOS Y SALARIOS"},
            {"ISR_SUB", "ISR/SUB"},
            {"CuotaObrera", "CUOTA OBRERA"},
            {"AmortizacionInfonavit", "AMORT. INFO."},
            {"Retencion", "RETEN."},
            {"NetoPagar", "NETO A PAGAR"},
            {"TotalIMSS", "TOTAL IMSS"},
            {"Comision", "COMISION"},
            {"Subtotal", "SUBTOTAL"}
        }

        'For Each col As DataColumn In dtTempNominas.Columns

        Dim cell As New PdfPCell()
        cell = New PdfPCell(New Phrase("", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, BaseColor.WHITE))) ' Tamaño de fuente 8, color blanco
        cell.BackgroundColor = New BaseColor(23, 59, 95) ' Color de fondo RGB #173b5f
        cell.HorizontalAlignment = Element.ALIGN_CENTER
        cell.VerticalAlignment = Element.ALIGN_MIDDLE
        cell.BorderWidth = 0
        cell.FixedHeight = 25.0F
        tabla.AddCell(cell)

        cell = New PdfPCell(New Phrase("", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, BaseColor.WHITE))) ' Tamaño de fuente 8, color blanco
        cell.BackgroundColor = New BaseColor(23, 59, 95) ' Color de fondo RGB #173b5f
        cell.HorizontalAlignment = Element.ALIGN_CENTER
        cell.VerticalAlignment = Element.ALIGN_MIDDLE
        cell.BorderWidth = 0
        cell.FixedHeight = 25.0F
        tabla.AddCell(cell)

        cell = New PdfPCell(New Phrase("SUELDOS", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, BaseColor.WHITE))) ' Tamaño de fuente 8, color blanco
        cell.BackgroundColor = New BaseColor(39, 88, 145) ' Color de fondo RGB #275891
        cell.HorizontalAlignment = Element.ALIGN_CENTER
        cell.VerticalAlignment = Element.ALIGN_MIDDLE
        cell.BorderWidth = 0
        cell.Colspan = 5
        cell.FixedHeight = 25.0F
        tabla.AddCell(cell)

        cell = New PdfPCell(New Phrase("", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, BaseColor.WHITE))) ' Tamaño de fuente 8, color blanco
        cell.BackgroundColor = New BaseColor(23, 59, 95) ' Color de fondo RGB #173b5f
        cell.HorizontalAlignment = Element.ALIGN_CENTER
        cell.VerticalAlignment = Element.ALIGN_MIDDLE
        cell.BorderWidth = 0
        cell.Colspan = 7
        cell.FixedHeight = 25.0F
        tabla.AddCell(cell)

        ' Añadir encabezados
        For Each col As DataColumn In dtTempNominas.Columns
            Dim nombreEncabezado As String = If(nuevosEncabezados.ContainsKey(col.ColumnName), nuevosEncabezados(col.ColumnName), col.ColumnName)
            cell = New PdfPCell(New Phrase(nombreEncabezado, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, BaseColor.WHITE))) ' Tamaño de fuente 8, color blanco
            cell.BackgroundColor = New BaseColor(23, 59, 95) ' Color de fondo RGB #173b5f
            cell.HorizontalAlignment = Element.ALIGN_CENTER
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.BorderWidth = 0
            cell.FixedHeight = 25.0F
            tabla.AddCell(cell)
        Next

        ' Obtener la última fila
        Dim ultimaFila As DataRow = dtTempNominas.Rows(dtTempNominas.Rows.Count - 1)

        ' Añadir la última fila al principio
        Dim isGrayBackground As Boolean = True ' Bandera para alternar el color de fondo
        For i As Integer = 0 To dtTempNominas.Columns.Count - 1

            If dtTempNominas.Columns(i).DataType.Name.ToString = "Decimal" Then
                cell = New PdfPCell(New Phrase(FormatNumber(ultimaFila(i), 2).ToString(), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8))) ' Tamaño de fuente 8
            Else
                If i = 0 Then
                    cell = New PdfPCell(New Phrase("MATRIZ (" & (dtTempNominas.Rows.Count - 1).ToString & ")", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8))) ' Tamaño de fuente 8
                Else
                    cell = New PdfPCell(New Phrase(ultimaFila(i).ToString(), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8))) ' Tamaño de fuente 8
                End If
            End If

            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.PaddingTop = 5
            cell.PaddingBottom = 5
            cell.PaddingRight = 5
            cell.PaddingLeft = 5
            cell.BackgroundColor = If(isGrayBackground, New BaseColor(241, 244, 244), BaseColor.WHITE) ' Color de fondo alternativo
            cell.BorderWidth = 0
            If dtTempNominas.Columns(i).ColumnName = "Nombre" Then
                cell.HorizontalAlignment = Element.ALIGN_LEFT ' Alinear a la izquierda
            Else
                cell.HorizontalAlignment = Element.ALIGN_RIGHT ' Alinear a la derecha
            End If
            tabla.AddCell(cell)
        Next

        ' Añadir filas de datos con fuente más pequeña
        isGrayBackground = False ' Reiniciar la bandera para las filas de datos
        For Each fila As DataRow In dtTempNominas.Rows
            If Not fila.Equals(ultimaFila) Then ' Evitar agregar la última fila de nuevo
                For i As Integer = 0 To dtTempNominas.Columns.Count - 1

                    cell = New PdfPCell
                    If dtTempNominas.Columns(i).DataType.Name.ToString = "Decimal" Then
                        cell = New PdfPCell(New Phrase(FormatNumber(fila(i), 2).ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8))) ' Tamaño de fuente 8
                    Else
                        cell = New PdfPCell(New Phrase(fila(i).ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8))) ' Tamaño de fuente 8
                    End If

                    cell.VerticalAlignment = Element.ALIGN_MIDDLE
                    cell.PaddingTop = 5
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 5
                    cell.PaddingLeft = 5
                    cell.BackgroundColor = If(isGrayBackground, New BaseColor(241, 244, 244), BaseColor.WHITE) '#004dcd Color de fondo alternativo
                    cell.BorderWidth = 0
                    If dtTempNominas.Columns(i).ColumnName = "Nombre" Then
                        cell.HorizontalAlignment = Element.ALIGN_LEFT ' Alinear a la izquierda
                    Else
                        cell.HorizontalAlignment = Element.ALIGN_RIGHT ' Alinear a la derecha
                    End If
                    tabla.AddCell(cell)
                Next
                isGrayBackground = Not isGrayBackground ' Cambiar el estado para la próxima fila
            End If
        Next

        ' Añadir la última fila al final
        isGrayBackground = False ' Reiniciar la bandera para la última fila
        For i As Integer = 0 To dtTempNominas.Columns.Count - 1

            cell = New PdfPCell
            If dtTempNominas.Columns(i).DataType.Name.ToString = "Decimal" Then
                cell = New PdfPCell(New Phrase(FormatNumber(ultimaFila(i), 2).ToString(), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8))) ' Tamaño de fuente 8
            Else
                cell = New PdfPCell(New Phrase(ultimaFila(i).ToString(), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8))) ' Tamaño de fuente 8
            End If

            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.PaddingTop = 5
            cell.PaddingBottom = 5
            cell.PaddingRight = 5
            cell.PaddingLeft = 5
            cell.BackgroundColor = If(isGrayBackground, New BaseColor(241, 244, 244), BaseColor.WHITE) '#004dcd Color de fondo alternativo
            cell.BorderWidth = 0
            If dtTempNominas.Columns(i).ColumnName = "Nombre" Then
                cell.HorizontalAlignment = Element.ALIGN_LEFT ' Alinear a la izquierda
            Else
                cell.HorizontalAlignment = Element.ALIGN_RIGHT ' Alinear a la derecha
            End If
            tabla.AddCell(cell)
        Next

        ' Agregar la tabla al documento
        documento.Add(tabla)
        ' Cerrar el documento
        documento.Close()
        documento = Nothing

        If File.Exists(archivoPdf) Then
            Dim bytes As Byte() = File.ReadAllBytes(archivoPdf)
            Using stream As New MemoryStream()
                Dim reader As New PdfReader(bytes)
                Using stamper As New PdfStamper(reader, stream)
                    Dim pages As Integer = reader.NumberOfPages
                    For i As Integer = 1 To pages
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, New Phrase("Página " & i.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8)), 510.0F, 10.0F, 0)
                    Next
                End Using

                bytes = stream.ToArray()
            End Using

            File.WriteAllBytes(archivoPdf, bytes)

            Dim FileName As String = Path.GetFileName(archivoPdf)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & FileName)
            Response.Flush()
            Response.WriteFile(archivoPdf)
            Response.End()
        End If
    End Sub
    Private Sub GridNominas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridNominas.NeedDataSource

        dtTempNominas = New DataTable()

        Dim cNomina As New Nomina()
        cNomina.cmd = 1
        cNomina.Ejercicio = IdEjercicio.Value
        cNomina.TipoNomina = IdPeriodicidad.Value 'Quincenal
        cNomina.Periodo = IdPeriodo.Value
        cNomina.IdEmpresa = IdEmpresa.Value
        cNomina.IdCliente = IdCliente.Value
        dtTempNominas = cNomina.ConsultarResumenNominas()
        GridNominas.DataSource = dtTempNominas
    End Sub

    'Sub MostrarPercepciones()
    '    Dim cNomina As New Nomina()
    '    cNomina.IdEmpresa = IdEmpresa.Value
    '    cNomina.IdCliente = IdCliente.Value
    '    cNomina.Ejercicio = IdEjercicio.Value
    '    cNomina.TipoNomina = IdPeriodicidad.Value 'Quincenal
    '    cNomina.Periodo = IdPeriodo.Value
    '    dtPercepciones = cNomina.ConsultarPercepcionesCorrida()
    '    grdPercepciones.DataSource = dtPercepciones
    '    grdPercepciones.DataBind()
    '    cNomina = Nothing
    'End Sub
    'Sub MostrarDeducciones()
    '    Dim cNomina As New Nomina()
    '    cNomina.IdEmpresa = IdEmpresa.Value
    '    cNomina.IdCliente = IdCliente.Value
    '    cNomina.Ejercicio = IdEjercicio.Value
    '    cNomina.TipoNomina = IdPeriodicidad.Value 'Quincenal
    '    cNomina.Periodo = IdPeriodo.Value
    '    dtDeducciones = cNomina.ConsultarDeduccionesCorridaCatorcenal()
    '    grdDeducciones.DataSource = dtDeducciones
    '    grdDeducciones.DataBind()
    '    cNomina = Nothing
    'End Sub
    'Sub MostrarTotalNeto()
    '    lblTotalNeto.Text = "TOTAL: " & FormatCurrency(totalNetoPercepciones - totalNetoDeducciones, 2).ToString
    '    lblTotalGravado.Text = FormatCurrency(totalGravado, 2).ToString
    '    lblTotalExento.Text = FormatCurrency(totalExento, 2).ToString
    'End Sub
    'Private Sub grdPercepciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdPercepciones.NeedDataSource

    '    Dim cNomina As New Nomina()
    '    cNomina.IdEmpresa = IdEmpresa.Value
    '    cNomina.IdCliente = IdCliente.Value
    '    cNomina.Ejercicio = IdEjercicio.Value
    '    cNomina.TipoNomina = IdPeriodicidad.Value 'Quincenal
    '    cNomina.Periodo = IdPeriodo.Value
    '    dtPercepciones = cNomina.ConsultarPercepcionesCorrida()
    '    grdPercepciones.DataSource = dtPercepciones
    '    cNomina = Nothing

    'End Sub
    'Private Sub grdDeducciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdDeducciones.NeedDataSource
    '    Dim cNomina As New Nomina()
    '    cNomina.IdEmpresa = IdEmpresa.Value
    '    cNomina.IdCliente = IdCliente.Value
    '    cNomina.Ejercicio = IdEjercicio.Value
    '    cNomina.TipoNomina = IdPeriodicidad.Value 'Quincenal
    '    cNomina.Periodo = IdPeriodo.Value
    '    dtDeducciones = cNomina.ConsultarDeduccionesCorrida()
    '    grdDeducciones.DataSource = dtDeducciones
    '    cNomina = Nothing
    'End Sub
    'Private Sub ListarPercepciones()
    '    Dim cNomina As New Nomina()
    '    cNomina.IdEmpresa = IdEmpresa.Value
    '    cNomina.IdCliente = IdCliente.Value
    '    cNomina.Ejercicio = IdEjercicio.Value
    '    cNomina.TipoNomina = IdPeriodicidad.Value 'Quincenal
    '    cNomina.Periodo = IdPeriodo.Value
    '    dtPercepciones = cNomina.ConsultarPercepcionesCorrida()
    '    grdPercepciones.DataSource = dtPercepciones
    '    grdPercepciones.DataBind()
    '    cNomina = Nothing
    'End Sub
    'Private Sub ListarDeducciones()
    '    Dim cNomina As New Nomina()
    '    cNomina.IdEmpresa = IdEmpresa.Value
    '    cNomina.IdCliente = IdCliente.Value
    '    cNomina.Ejercicio = IdEjercicio.Value
    '    cNomina.TipoNomina = IdPeriodicidad.Value 'Quincenal
    '    cNomina.Periodo = IdPeriodo.Value
    '    dtDeducciones = cNomina.ConsultarDeduccionesCorrida()
    '    grdDeducciones.DataSource = dtDeducciones
    '    grdDeducciones.DataBind()
    '    cNomina = Nothing
    'End Sub
    'Private Sub grdPercepciones_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles grdPercepciones.ItemDataBound
    '    Select Case e.Item.ItemType
    '        Case Telerik.Web.UI.GridItemType.Footer
    '            If dtPercepciones.Rows.Count > 0 Then
    '                If Not IsDBNull(dtPercepciones.Compute("sum(Total)", "")) Then
    '                    e.Item.Cells(4).Text = "TOTAL:"
    '                    e.Item.Cells(4).HorizontalAlign = HorizontalAlign.Right
    '                    e.Item.Cells(4).Font.Bold = True

    '                    e.Item.Cells(5).Text = FormatCurrency(dtPercepciones.Compute("sum(Total)", ""), 2).ToString
    '                    e.Item.Cells(5).HorizontalAlign = HorizontalAlign.Right
    '                    e.Item.Cells(5).Font.Bold = True
    '                    totalNetoPercepciones = dtPercepciones.Compute("sum(Total)", "")
    '                    totalGravado = dtPercepciones.Compute("sum(TotalGravado)", "")
    '                    totalExento = dtPercepciones.Compute("sum(TotalExento)", "")
    '                    lblPercepcionesTotales.Text = FormatCurrency(totalNetoPercepciones, 2).ToString
    '                End If
    '            End If
    '    End Select
    'End Sub
    'Private Sub grdDeducciones_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles grdDeducciones.ItemDataBound
    '    Select Case e.Item.ItemType
    '        Case Telerik.Web.UI.GridItemType.Footer
    '            If dtDeducciones.Rows.Count > 0 Then
    '                If Not IsDBNull(dtDeducciones.Compute("sum(Total)", "")) Then
    '                    e.Item.Cells(4).Text = "TOTAL:"
    '                    e.Item.Cells(4).HorizontalAlign = HorizontalAlign.Right
    '                    e.Item.Cells(4).Font.Bold = True

    '                    e.Item.Cells(5).Text = FormatCurrency(dtDeducciones.Compute("sum(Total)", ""), 2).ToString
    '                    e.Item.Cells(5).HorizontalAlign = HorizontalAlign.Right
    '                    e.Item.Cells(5).Font.Bold = True
    '                    totalNetoDeducciones = dtDeducciones.Compute("sum(Total)", "")
    '                    lblDeduccionesTotales.Text = FormatCurrency(totalNetoDeducciones, 2).ToString
    '                End If
    '            End If
    '    End Select
    'End Sub
    'Private Sub RadListView1_NeedDataSource(sender As Object, e As RadListViewNeedDataSourceEventArgs) Handles RadListView1.NeedDataSource

    '    Dim dt As New DataTable
    '    Dim cNomina As New Nomina()
    '    cNomina.IdEmpresa = IdEmpresa.Value
    '    cNomina.IdCliente = IdCliente.Value
    '    cNomina.Ejercicio = IdEjercicio.Value
    '    cNomina.TipoNomina = IdPeriodicidad.Value 'Quincenal
    '    cNomina.Periodo = IdPeriodo.Value
    '    dt = cNomina.ConsultarEmpleadosCorridaCatorcenal()
    '    RadListView1.DataSource = dt
    '    cNomina = Nothing
    'End Sub
    'Private Sub RadListView1_ItemDataBound(sender As Object, e As RadListViewItemEventArgs) Handles RadListView1.ItemDataBound
    '    Select Case e.Item.ItemType
    '        Case RadListViewItemType.DataItem, RadListViewItemType.AlternatingItem
    '            Dim dataItem = DirectCast(e.Item, RadListViewDataItem).DataItem

    '            Dim grdPercepciones As RadGrid = CType(e.Item.FindControl("GridPercepciones"), RadGrid)
    '            Dim grdDeducciones As RadGrid = CType(e.Item.FindControl("GridDeduciones"), RadGrid)

    '            Dim lblEjercicio As Label = CType(e.Item.FindControl("lblEjercicio"), Label)
    '            Dim lblNoPeriodo As Label = CType(e.Item.FindControl("lblNoPeriodo"), Label)
    '            Dim lblNumEmpleado As Label = CType(e.Item.FindControl("lblNumEmpleado"), Label)
    '            Dim lblRFC As Label = CType(e.Item.FindControl("lblRFC"), Label)
    '            Dim lblNombreEmpleado As Label = CType(e.Item.FindControl("lblNombreEmpleado"), Label)
    '            Dim lblNumImss As Label = CType(e.Item.FindControl("lblNumImss"), Label)
    '            'Dim lblRegContratacion As Label = CType(e.Item.FindControl("lblRegContratacion"), Label)
    '            Dim lblPuesto As Label = CType(e.Item.FindControl("lblPuesto"), Label)
    '            Dim lblCuotaDiaria As Label = CType(e.Item.FindControl("lblCuotaDiaria"), Label)
    '            Dim lblIntegradoImss As Label = CType(e.Item.FindControl("lblIntegradoImss"), Label)
    '            Dim lblDiasLaborados As Label = CType(e.Item.FindControl("lblDiasLaborados"), Label)

    '            Dim lblFechaFinal As Label = CType(e.Item.FindControl("lblFechaFinal"), Label)
    '            Dim lblFechaInicial As Label = CType(e.Item.FindControl("lblFechaInicial"), Label)

    '            Dim lblNeto As Label = CType(e.Item.FindControl("lblNeto"), Label)

    '            Dim cPeriodo As New Entities.Periodo
    '            cPeriodo.IdPeriodo = IdPeriodo.Value
    '            cPeriodo.ConsultarPeriodoID()

    '            lblFechaInicial.Text = cPeriodo.FechaInicial.ToString
    '            lblFechaFinal.Text = cPeriodo.FechaFinal.ToString

    '            Dim cEmpleado As New Entities.Empleado
    '            cEmpleado.IdEmpleado = dataItem("NoEmpleado")
    '            cEmpleado.ConsultarEmpleadoID()
    '            If cEmpleado.IdEmpleado > 0 Then
    '                lblEjercicio.Text = IdEjercicio.Value.ToString
    '                lblNoPeriodo.Text = IdPeriodo.Value.ToString
    '                lblNumEmpleado.Text = dataItem("NoEmpleado")
    '                lblRFC.Text = cEmpleado.Rfc
    '                lblNombreEmpleado.Text = cEmpleado.Nombre
    '                lblNumImss.Text = cEmpleado.Imss
    '                'lblRegContratacion.Text = cEmpleado.RegimenContratacion
    '                lblPuesto.Text = cEmpleado.Puesto
    '                'lblCuotaDiaria.Text = FormatCurrency(cEmpleado.CuotaDiaria, 2)
    '                lblIntegradoImss.Text = FormatCurrency(cEmpleado.IntegradoImss, 2)
    '            End If

    '            Call ConsultarNumeroDeDiasPagados(dataItem("NoEmpleado"))

    '            dtGridPercepciones = New DataTable()
    '            Dim cNomina As New Nomina()
    '            cNomina.IdEmpresa = IdEmpresa.Value
    '            cNomina.IdCliente = IdCliente.Value
    '            cNomina.Ejercicio = IdEjercicio.Value
    '            cNomina.TipoNomina = IdPeriodicidad.Value 'Quincenal
    '            cNomina.Periodo = IdPeriodo.Value
    '            cNomina.TipoConcepto = "P"
    '            cNomina.Tipo = "N"
    '            cNomina.NoEmpleado = dataItem("NoEmpleado")
    '            dtGridPercepciones = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
    '            grdPercepciones.DataSource = dtGridPercepciones
    '            grdPercepciones.DataBind()

    '            dtGridDeducciones = New DataTable()
    '            cNomina = New Nomina()
    '            cNomina.IdEmpresa = IdEmpresa.Value
    '            cNomina.IdCliente = IdCliente.Value
    '            cNomina.Ejercicio = IdEjercicio.Value
    '            cNomina.TipoNomina = IdPeriodicidad.Value 'Quincenal
    '            cNomina.Periodo = IdPeriodo.Value
    '            cNomina.TipoConcepto = "D"
    '            cNomina.Tipo = "N"
    '            cNomina.NoEmpleado = dataItem("NoEmpleado")
    '            dtGridDeducciones = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
    '            grdDeducciones.DataSource = dtGridDeducciones
    '            grdDeducciones.DataBind()

    '            Dim dt As New DataTable()
    '            'cNomina = New Nomina()
    '            'cNomina.IdEmpresa = IdEmpresa.Value
    '            'cNomina.Ejercicio = IdEjercicio.Value
    '            'cNomina.TipoNomina = 1 'Quincenal
    '            'cNomina.Periodo = IdPeriodo.Value
    '            'cNomina.TipoConcepto = "DE"
    '            'cNomina.CvoConcepto = 87
    '            'cNomina.NoEmpleado = dataItem("NoEmpleado")
    '            'dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()

    '            'If dt.Rows.Count > 0 Then
    '            '    lblIntegradoImss.Text = FormatCurrency(dt.Rows(0).Item("CuotaDiaria") * 1.0452, 2)
    '            'End If

    '            dt = New DataTable()
    '            cNomina = New Nomina()
    '            cNomina.IdEmpresa = IdEmpresa.Value
    '            cNomina.IdCliente = IdCliente.Value
    '            cNomina.Ejercicio = IdEjercicio.Value
    '            cNomina.TipoNomina = IdPeriodicidad.Value 'Quincenal
    '            cNomina.Periodo = IdPeriodo.Value
    '            cNomina.TipoConcepto = "DE"
    '            cNomina.CvoConcepto = 87
    '            cNomina.NoEmpleado = dataItem("NoEmpleado")
    '            dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()

    '            If dt.Rows.Count > 0 Then
    '                lblCuotaDiaria.Text = FormatCurrency(dt.Rows(0).Item("CuotaDiaria"), 2)
    '            End If

    '            lblDiasLaborados.Text = NumeroDeDiasPagados.ToString

    '            lblNeto.Text = "NETO A PAGAR: " & FormatCurrency(totalPercepciones - totalDeducciones, 2).ToString

    '            totalPercepciones = 0
    '            totalDeducciones = 0
    '            cNomina = Nothing

    '    End Select
    'End Sub
    'Sub GridPercepciones_ItemDataBound(sender As Object, e As GridItemEventArgs)
    '    If (TypeOf e.Item Is GridDataItem) Then
    '        Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '        Dim Importe As String = dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("Importe").ToString()
    '        Dim fieldValue As Double = Double.Parse(Importe)
    '        totalPercepciones = (totalPercepciones + fieldValue)
    '    End If
    '    If (TypeOf e.Item Is GridFooterItem) Then
    '        Dim footerItem As GridFooterItem = CType(e.Item, GridFooterItem)
    '        footerItem("Concepto").Text = "TOTAL:"
    '        footerItem("Concepto").HorizontalAlign = HorizontalAlign.Right
    '        footerItem("Concepto").Font.Bold = True

    '        footerItem("Importe").Text = FormatCurrency(totalPercepciones, 2).ToString
    '        footerItem("Importe").HorizontalAlign = HorizontalAlign.Right
    '        footerItem("Importe").Font.Bold = True
    '    End If
    'End Sub
    'Sub GridDeduciones_ItemDataBound(sender As Object, e As GridItemEventArgs)
    '    If (TypeOf e.Item Is GridDataItem) Then
    '        Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
    '        Dim Importe As String = dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("Importe").ToString()
    '        Dim fieldValue As Double = Double.Parse(Importe)
    '        totalDeducciones = (totalDeducciones + fieldValue)
    '    End If
    '    If (TypeOf e.Item Is GridFooterItem) Then
    '        Dim footerItem As GridFooterItem = CType(e.Item, GridFooterItem)
    '        footerItem("Concepto").Text = "TOTAL:"
    '        footerItem("Concepto").HorizontalAlign = HorizontalAlign.Right
    '        footerItem("Concepto").Font.Bold = True

    '        footerItem("Importe").Text = FormatCurrency(totalDeducciones, 2).ToString
    '        footerItem("Importe").HorizontalAlign = HorizontalAlign.Right
    '        footerItem("Importe").Font.Bold = True
    '    End If
    'End Sub
    'Private Sub ConsultarNumeroDeDiasPagados(ByVal NoEmpleado As Integer)
    '    Try
    '        Dim DiasVacaciones As Double = 0
    '        Dim DiasCuotaPeriodo As Double = 0
    '        Dim DiasHonorarioAsimilado As Double = 0
    '        Dim DiasPagoPorHoras As Double = 0
    '        Dim DiasComision As Double = 0
    '        Dim DiasDestajo As Double = 0
    '        Dim DiasFaltasPermisosIncapacidades As Double = 0

    '        Dim dt As New DataTable()
    '        Dim cNomina As New Nomina()
    '        cNomina.IdEmpresa = IdEmpresa.Value
    '        cNomina.IdCliente = IdCliente.Value
    '        cNomina.Ejercicio = IdEjercicio.Value
    '        cNomina.TipoNomina = IdPeriodicidad.Value 'Quincenal
    '        cNomina.Periodo = IdPeriodo.Value
    '        cNomina.NoEmpleado = NoEmpleado

    '        dt = cNomina.ConsultarConceptosEmpleado()

    '        If dt.Rows.Count > 0 Then
    '            If dt.Compute("Sum(Importe)", "CvoConcepto=85") IsNot DBNull.Value Then
    '                DiasCuotaPeriodo = dt.Compute("Sum(UNIDAD)", "CvoConcepto=85")
    '            End If
    '            If dt.Compute("Sum(Importe)", "CvoConcepto=3") IsNot DBNull.Value Then
    '                DiasComision = 7
    '            End If
    '            If dt.Compute("Sum(Importe)", "CvoConcepto=4") IsNot DBNull.Value Then
    '                DiasDestajo = 7
    '            End If
    '            If dt.Compute("Sum(Importe)", "CvoConcepto=5") IsNot DBNull.Value Then
    '                DiasHonorarioAsimilado = dt.Compute("Sum(UNIDAD)", "CvoConcepto=5") * 7
    '            End If
    '            If dt.Compute("Sum(Importe)", "CvoConcepto=15") IsNot DBNull.Value Then
    '                DiasVacaciones = dt.Compute("Sum(UNIDAD)", "CvoConcepto=15")
    '            End If
    '            If dt.Compute("Sum(Importe)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162") IsNot DBNull.Value Then
    '                DiasFaltasPermisosIncapacidades = dt.Compute("Sum(UNIDAD)", "CvoConcepto=57 OR CvoConcepto=58 OR CvoConcepto=59 OR CvoConcepto=161 OR CvoConcepto=162")
    '            End If
    '        End If
    '        If DiasCuotaPeriodo > 0 Then
    '            DiasPagoPorHoras = 0
    '            DiasComision = 0
    '            DiasDestajo = 0
    '        ElseIf DiasPagoPorHoras > 0 Then
    '            DiasCuotaPeriodo = 0
    '            DiasComision = 0
    '            DiasDestajo = 0
    '        ElseIf DiasComision > 0 Then
    '            DiasPagoPorHoras = 0
    '            DiasCuotaPeriodo = 0
    '            DiasDestajo = 0
    '        ElseIf DiasDestajo > 0 Then
    '            DiasComision = 0
    '            DiasPagoPorHoras = 0
    '            DiasCuotaPeriodo = 0
    '        End If

    '        NumeroDeDiasPagados = DiasCuotaPeriodo + DiasVacaciones + DiasComision + DiasPagoPorHoras + DiasDestajo + DiasHonorarioAsimilado - DiasFaltasPermisosIncapacidades

    '    Catch oExcep As Exception
    '        MsgBox(oExcep.Message)
    '    End Try
    'End Sub
    'Private Sub gridReporte_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridReporte.NeedDataSource

    '    Dim dt As New DataTable
    '    Dim cNomina As New Nomina()
    '    cNomina.IdNomina = Session("Folio")
    '    cNomina.IdEmpresa = IdEmpresa.Value
    '    cNomina.IdCliente = IdCliente.Value
    '    cNomina.Ejercicio = IdEjercicio.Value
    '    cNomina.TipoNomina = IdPeriodicidad.Value
    '    cNomina.Periodo = IdPeriodo.Value
    '    cNomina.EsEspecial = EspecialBool.Value
    '    dt = cNomina.ConsultarDetalleNominaExtraordinaria()
    '    gridReporte.DataSource = dt
    '    cNomina = Nothing

    'End Sub

End Class