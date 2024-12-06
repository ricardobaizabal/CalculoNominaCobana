Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Entities
Imports System.Xml
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography
Imports Telerik.Reporting.Processing
Imports ThoughtWorks.QRCode.Codec
Imports System.Net.Mail
Imports Ionic.Zip
Imports System.Web.Services.Protocols
Imports System.Linq
Imports System.Drawing
Imports Telerik.Charting

Public Class ResumenNominaSemanal
    Inherits System.Web.UI.Page

    Private ObjData As New DataControl()
    Private IdEmpresa As Integer = 0
    Private IdEjercicio As Integer = 0
    Private dtEmpleados As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objCat As New DataControl(1)
            Dim cConcepto As New Entities.Catalogos

            ' Cargar catálogo de clientes y periodos
            objCat.CatalogoRad(cmbCliente, cConcepto.ConsultarMisClientes, True, False)
            objCat.CatalogoRad(cmbPeriodicidad, cConcepto.ConsultarPeriodoPago2, True, False)

            ' ---- CATALOGO DE ESTADO Y MUNICIPIO ----
            'objCat.Catalogo(estadoid, 0, cConcepto.ConsultarEstado, True, False)
            'objCat.Catalogo(estadoid, 0, cConcepto.ConsultaMunicipio, True, False)

            ' Aquí inicializamos el combo de folios de nómina con la opción predeterminada "Seleccionar Folio"
            'cmbFolioNomina.Items.Clear()
            cmbPeriodo.Items.Clear()
            Dim defaultItem As New Telerik.Web.UI.RadComboBoxItem("--Seleccione--", "")
            'cmbFolioNomina.Items.Add(defaultItem)
            cmbPeriodo.Items.Add(defaultItem)

            cmbPeriodicidad.SelectedValue = 1
            'CargarGridNominas()
            'CargaPeriodos(cmbPeriodicidad.SelectedValue)
            cmbPeriodicidad.Enabled = False

            'Call CargarCliente()

            Session("Folio") = Nothing
        End If
    End Sub
    Protected Sub cmbCliente_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)

        Dim defaultItem As New Telerik.Web.UI.RadComboBoxItem("--Seleccione--", "")
        cmbPeriodo.Items.Clear()
        cmbPeriodo.Items.Add(defaultItem)

        Call CargarVariablesGenerales()

        Dim ObjData As New DataControl()
        Dim cConcepto As New Entities.Catalogos
        Dim cPeriodo As New Entities.Periodo
        cPeriodo.IdEjercicio = IdEjercicio
        cPeriodo.IdTipoNomina = 1
        cPeriodo.ExtraordinarioBit = False
        cPeriodo.cmd = 1
        cPeriodo.IdEmpresa = IdEmpresa
        cPeriodo.IdCliente = cmbCliente.SelectedValue
        ObjData.CatalogoRad(cmbPeriodo, cPeriodo.ConsultarPeriodosResumen(), True, False)
        ObjData = Nothing

    End Sub
    ' Declarar la tabla temporal en la clase
    Private dtTempNominas As DataTable
    Private Sub CargarGridNominas()

        Call CargarVariablesGenerales()

        Dim periodicidad, cliente, periodo As Integer

        If cmbPeriodicidad.SelectedValue.ToString() = "" Then
            periodicidad = 0
        Else
            periodicidad = cmbPeriodicidad.SelectedValue
        End If

        If cmbCliente.SelectedValue.ToString() = "" Then
            cliente = 0
        Else
            cliente = cmbCliente.SelectedValue
        End If

        If cmbPeriodo.SelectedValue.ToString() = "" Then
            periodo = 0
        Else
            periodo = cmbPeriodo.SelectedValue
        End If

        ' Obtener el periodo seleccionado (nombre)
        Dim periodoSeleccionado As String = cmbPeriodo.SelectedItem.Text

        ' Crear la tabla temporal
        dtTempNominas = New DataTable()

        Dim cNomina As New Nomina()
        cNomina.cmd = 1
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = periodicidad
        cNomina.Periodo = periodo
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = cliente
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

        ' Asignar los datos al Grid
        GridNominas.DataSource = dtTempNominas
        GridNominas.DataBind()

        ' Verificar si hay filas en el DataSource para habilitar o deshabilitar el botón
        If GridNominas.DataSource IsNot Nothing AndAlso CType(GridNominas.DataSource, DataTable).Rows.Count > 0 AndAlso periodoSeleccionado <> "--Seleccione--" Then
            btnGenerarPDF.Enabled = True ' Habilitar el botón si hay filas
        Else
            btnGenerarPDF.Enabled = False ' Deshabilitar el botón si no hay filas
        End If
    End Sub
    Private Sub GridNominas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridNominas.NeedDataSource
        'Private Sub GridNominas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs)
        Call CargarVariablesGenerales()

        Dim periodicidad, cliente, periodo As Integer

        If cmbPeriodicidad.SelectedValue.ToString() = "" Then
            periodicidad = 0
        Else
            periodicidad = cmbPeriodicidad.SelectedValue
        End If

        If cmbCliente.SelectedValue.ToString() = "" Then
            cliente = 0
        Else
            cliente = cmbCliente.SelectedValue
        End If

        If cmbPeriodo.SelectedValue.ToString() = "" Then
            periodo = 0
        Else
            periodo = cmbPeriodo.SelectedValue
        End If


        ' Obtener el periodo seleccionado (nombre)
        Dim periodoSeleccionado As String = cmbPeriodo.SelectedItem.Text


        Dim cNomina As New Nomina()
        cNomina.cmd = 1
        cNomina.Ejercicio = IdEjercicio
        'cNomina.EsEspecial = False
        cNomina.TipoNomina = periodicidad
        cNomina.Periodo = periodo
        cNomina.IdEmpresa = cliente
        GridNominas.DataSource = cNomina.ConsultarResumenNominas()
        cNomina = Nothing

        ' Verificar si hay filas en el DataSource para habilitar o deshabilitar el botón
        If GridNominas.DataSource IsNot Nothing AndAlso CType(GridNominas.DataSource, DataTable).Rows.Count > 0 AndAlso periodoSeleccionado <> "--Seleccione--" Then
            btnGenerarPDF.Enabled = True ' Habilitar el botón si hay filas
        Else
            btnGenerarPDF.Enabled = False ' Deshabilitar el botón si no hay filas
        End If

    End Sub
    Private Sub GridNominas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridNominas.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Dim folio As String = e.CommandArgument.ToString()
                Session("Folio") = folio
                Response.Redirect("GeneracionDeNominaNormal.aspx")
        End Select
    End Sub
    Private Sub btnBuscarNominas_Click(sender As Object, e As EventArgs) Handles btnBuscarNominas.Click
        CargarGridNominas()
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
    Protected Sub btnGenerarPDF_Click(sender As Object, e As EventArgs) Handles btnGenerarPDF.Click
        CargarGridNominas() ' Asegúrate de que los datos se carguen primero
        GenerarPDF()
    End Sub
    Private Sub GenerarPDF()


        ' Obtener el cliente seleccionado (nombre)
        Dim clienteSeleccionado As String = cmbCliente.SelectedItem.Text

        ' Obtener el periodo seleccionado (nombre)
        Dim periodoSeleccionado As String = cmbPeriodo.SelectedItem.Text

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

        FilePath = Server.MapPath("~/ResumenNominaPDF/Semanal/")



        ' Validar si la carpeta ya existe
        If Not Directory.Exists(FilePath) Then
            Directory.CreateDirectory(FilePath) ' Crear la carpeta si no existe
        End If


        Dim archivoPdf As String = Path.Combine(FilePath, "ResumenNominaSemanal " + periodoSimboloCambiado + ".pdf")


        PdfWriter.GetInstance(documento, New FileStream(archivoPdf, FileMode.Create))


        ' Abrir el documento para escribir
        documento.Open()


        ' Agregar un título
        Dim titulo As New Paragraph("RESUMEN DE NÓMINA SEMANAL", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, New BaseColor(39, 17, 149)))
        titulo.Alignment = Element.ALIGN_LEFT ' Alineación a la izquierda
        documento.Add(titulo)

        ' Agregar un salto de línea de la mitad del valor actual (6 puntos)
        documento.Add(New Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 6)) With {.SpacingBefore = 1.0F})


        ' Información del cliente
        Dim infoCliente As New Paragraph("MEDIOS AVANZADOS EN COMUNICACIÓN SA DE CV", FontFactory.GetFont(FontFactory.HELVETICA, 10, New BaseColor(39, 17, 149)))
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

        tablaCliente.AddCell(New PdfPCell(New Phrase("PLAZA:", FontFactory.GetFont(FontFactory.HELVETICA, 10, New BaseColor(39, 17, 149)))) With {.BorderWidth = 0, .padding = paddingCliente})
        tablaCliente.AddCell(New PdfPCell(New Phrase("NUEVO LEON", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, New BaseColor(39, 17, 149)))) With {.BorderWidth = 0, .padding = paddingCliente})

        tablaCliente.AddCell(New PdfPCell(New Phrase("SUCURSAL:", FontFactory.GetFont(FontFactory.HELVETICA, 10, New BaseColor(39, 17, 149)))) With {.BorderWidth = 0, .padding = paddingCliente})
        tablaCliente.AddCell(New PdfPCell(New Phrase("MONTERREY", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, New BaseColor(39, 17, 149)))) With {.BorderWidth = 0, .padding = paddingCliente})

        ' Agregar la tabla al documento
        documento.Add(tablaCliente)



        ' Agregar un salto de línea de la mitad del valor actual (6 puntos)
        documento.Add(New Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 6)) With {.SpacingBefore = 1.0F})


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
        documento.Add(New Paragraph(" ", FontFactory.GetFont(FontFactory.HELVETICA, 8)) With {.SpacingBefore = 6.0F})

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

        ' Añadir encabezados
        For Each col As DataColumn In dtTempNominas.Columns
            Dim nombreEncabezado As String = If(nuevosEncabezados.ContainsKey(col.ColumnName), nuevosEncabezados(col.ColumnName), col.ColumnName)
            Dim cell As New PdfPCell(New Phrase(nombreEncabezado, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, BaseColor.WHITE))) ' Tamaño de fuente 9, color blanco
            cell.BackgroundColor = New BaseColor(39, 17, 149) ' Color de fondo RGB #3d6ade
            cell.HorizontalAlignment = Element.ALIGN_CENTER
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.BorderWidth = 0
            tabla.AddCell(cell)
        Next

        ' Obtener la última fila
        Dim ultimaFila As DataRow = dtTempNominas.Rows(dtTempNominas.Rows.Count - 1)

        ' Añadir la última fila al principio
        Dim isGrayBackground As Boolean = False ' Bandera para alternar el color de fondo
        For i As Integer = 0 To dtTempNominas.Columns.Count - 1
            Dim cell As New PdfPCell(New Phrase(ultimaFila(i).ToString(), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10))) ' Tamaño de fuente 10, negrita
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.PaddingTop = 5
            cell.PaddingBottom = 5
            cell.PaddingRight = 5
            cell.PaddingLeft = 5
            cell.BackgroundColor = If(isGrayBackground, New BaseColor(223, 223, 223), BaseColor.WHITE) ' Color de fondo alternativo
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
                    Dim cell As New PdfPCell(New Phrase(fila(i).ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8))) ' Tamaño de fuente 8
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE
                    cell.PaddingTop = 5
                    cell.PaddingBottom = 5
                    cell.PaddingRight = 5
                    cell.PaddingLeft = 5
                    cell.BackgroundColor = If(isGrayBackground, New BaseColor(223, 223, 223), BaseColor.WHITE) ' Color de fondo alternativo
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
            Dim cell As New PdfPCell(New Phrase(ultimaFila(i).ToString(), FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10))) ' Tamaño de fuente 10, negrita
            cell.VerticalAlignment = Element.ALIGN_MIDDLE
            cell.PaddingTop = 5
            cell.PaddingBottom = 5
            cell.PaddingRight = 5
            cell.PaddingLeft = 5
            cell.BackgroundColor = If(isGrayBackground, New BaseColor(223, 223, 223), BaseColor.WHITE) ' Color de fondo alternativo
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
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_LEFT, New Phrase("Página " & i.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 8)), 10.0F, 10.0F, 0)
                    Next
                End Using

                bytes = stream.ToArray()
            End Using

            'File.WriteAllBytes(archivoPdf, bytes)
            Dim FileName As String = Path.GetFileName(archivoPdf)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=" & FileName)
            Response.Flush()
            Response.WriteFile(archivoPdf)
            Response.End()
        End If
    End Sub
    Private Sub cmbPeriodicidad_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cmbPeriodicidad.SelectedIndexChanged

        Dim defaultItem As New Telerik.Web.UI.RadComboBoxItem("--Seleccione--", "")
        cmbPeriodo.Items.Clear()
        cmbPeriodo.Items.Add(defaultItem)

        Call CargarVariablesGenerales()

        Dim ObjData As New DataControl()
        Dim cConcepto As New Entities.Catalogos
        Dim cPeriodo As New Entities.Periodo
        cPeriodo.IdEjercicio = IdEjercicio
        cPeriodo.IdTipoNomina = 1
        cPeriodo.ExtraordinarioBit = False
        cPeriodo.cmd = 1
        cPeriodo.IdEmpresa = IdEmpresa
        cPeriodo.IdCliente = cmbCliente.SelectedValue
        ObjData.CatalogoRad(cmbPeriodo, cPeriodo.ConsultarPeriodosResumen(), True, False)
        ObjData = Nothing

    End Sub

End Class