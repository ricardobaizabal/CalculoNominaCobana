Imports System.Threading
Imports System.Globalization
Imports Telerik.Web.UI
Imports Entities
Public Class AcumuladosEmpleado
    Inherits System.Web.UI.Page

    Private dtReporte As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim cConcepto As New Entities.Catalogos
            Dim ObjData As New DataControl()
            ObjData.Catalogo(cmbEjercicio, 0, cConcepto.ConsultarEjercicio)
            ObjData = Nothing
            cConcepto = Nothing
        End If
    End Sub

    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = cmbEjercicio.SelectedValue
        cNomina.Mes1 = cmbMes1.SelectedValue
        cNomina.Mes2 = cmbMes2.SelectedValue
        dtReporte = cNomina.ReporteAcumuladosEmpleado()
        grdReporteNomina.DataSource = dtReporte
        grdReporteNomina.DataBind()
        cNomina = Nothing
    End Sub

    Private Sub ExportarDataTableAExcel(ByVal Tabla As DataTable)
        Try
            'Creamos las variables
            Dim exApp As New Microsoft.Office.Interop.Excel.Application
            Dim exLibro As Microsoft.Office.Interop.Excel.Workbook
            Dim exHoja As Microsoft.Office.Interop.Excel.Worksheet
            Dim filaTabla As System.Data.DataRow

            'Añadimos el Libro al programa, y la hoja al libro
            exLibro = exApp.Workbooks.Add
            exHoja = exLibro.Worksheets.Add()

            ' ¿Cuantas columnas y cuantas filas?
            Dim NCol As Integer = Tabla.Columns.Count
            Dim NRow As Integer = Tabla.Rows.Count

            'Aqui recorremos todas las filas, y por cada fila todas las columnas y vamos escribiendo.
            For i As Integer = 1 To NCol
                exHoja.Cells.Item(1, i) = Tabla.Columns(i - 1).Caption
                'exHoja.Cells.Item(1, i).HorizontalAlignment = 3
            Next

            For Fila As Integer = 0 To NRow - 1
                filaTabla = Tabla.Rows(Fila)
                For Col As Integer = 0 To NCol - 1
                    exHoja.Cells.Item(Fila + 2, Col + 1) = filaTabla(Col)
                Next
            Next

            'Titulo en negrita, Alineado al centro y que el tamaño de la columna se ajuste al texto
            exHoja.Rows.Item(1).Font.Bold = 1
            exHoja.Rows.Item(1).HorizontalAlignment = 3
            exHoja.Columns.AutoFit()

            'Aplicación visible
            exApp.Application.Visible = True

            exHoja = Nothing
            exLibro = Nothing
            exApp = Nothing

            'ExportarDataTableAExcel = True
        Catch ex As Exception
            'ExportarDataTableAExcel = False
            Response.Write(ex.Message.ToString)
            Response.End()
        End Try
    End Sub

    Private Sub grdReporteNomina_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdReporteNomina.NeedDataSource
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = cmbEjercicio.SelectedValue
        cNomina.Mes1 = cmbMes1.SelectedValue
        cNomina.Mes2 = cmbMes2.SelectedValue
        dtReporte = cNomina.ReporteAcumuladosEmpleado()
        grdReporteNomina.DataSource = dtReporte
        cNomina = Nothing
    End Sub

End Class