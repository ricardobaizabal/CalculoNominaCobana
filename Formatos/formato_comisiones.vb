Imports Telerik.Reporting.Drawing
Imports System.Data.SqlClient

Partial Public Class formato_comisiones
    Inherits Telerik.Reporting.Report

    Public Sub New()
        InitializeComponent()
        detail.Height = Unit.Inch(0)
    End Sub

    Private Sub formato_cfdi_NeedDataSource(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.NeedDataSource

        Dim NoEmpleado As Long = Me.ReportParameters("NoEmpleado").Value
        Dim Ejercicio As Long = Me.ReportParameters("Ejercicio").Value
        Dim TipoNomina As Long = Me.ReportParameters("TipoNomina").Value
        Dim Periodo As Long = Me.ReportParameters("Periodo").Value
        Dim Tipo As String = Me.ReportParameters("Tipo").Value

        Dim sql As String = ""
        Dim conn As New SqlConnection(Me.ReportParameters("conn").Value.ToString)
        Dim report As Telerik.Reporting.Processing.Report = DirectCast(sender, Telerik.Reporting.Processing.Report)

        If Periodo = 0 Then
            sql = "EXEC pConsultarConceptosUtilidadesPDF @pNoEmpleado='" & NoEmpleado.ToString & "', @pEjercicio='" & Ejercicio.ToString & "', @pTipo='" & Tipo.ToString & "', @pPeriodo='" & Periodo.ToString & "'"
        Else
            sql = "EXEC pConsultarConceptosPDF @pNoEmpleado='" & NoEmpleado.ToString & "', @pEjercicio='" & Ejercicio.ToString & "', @pTipoNomina='" & TipoNomina.ToString & "', @pPeriodo='" & Periodo.ToString & "', @pTipo='" & Tipo.ToString & "'"
        End If

        Dim cmd As New SqlDataAdapter(sql, conn)
        cmd.SelectCommand.CommandTimeout = 3600
        Dim ds As DataSet = New DataSet()
        conn.Open()
        cmd.Fill(ds)

        If ds.Tables(0).Rows.Count > 0 Then
            Dim processingReport = CType(sender, Telerik.Reporting.Processing.Report)
            processingReport.DataSource = ds.Tables(0)
        End If

    End Sub

End Class