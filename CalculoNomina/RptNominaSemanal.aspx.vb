Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Entities
Public Class RptNominaSemanal
    Inherits System.Web.UI.Page
    Private dtPercepciones As New DataTable
    Private dtDeducciones As New DataTable

    Private dtGridPercepciones As New DataTable
    Private dtGridDeducciones As New DataTable

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
                p.Add(New SqlParameter("@cmd", 1))
                p.Add(New SqlParameter("@clienteid", Session("clienteid")))
                ds = ObjData.FillDataSet("pCliente", p)
                ObjData = Nothing

                If ds.Tables(0).Rows.Count > 0 Then
                    For Each row As DataRow In ds.Tables(0).Rows
                        lblEmpresa.Text = row("nombre_comercial")
                        lblDireccion.Text = row("expedicionLinea3")
                    Next
                End If

                Dim cPeriodo As New Entities.Periodo
                cPeriodo.IdPeriodo = IdPeriodo.Value
                cPeriodo.ConsultarPeriodoID()

                Dim nomina_name As String

                If EspecialBool.Value = "1" Then
                    nomina_name = " EXTRAORDINARIA "
                Else
                    If IdPeriodicidad.Value = "1" Then
                        nomina_name = " SEMANAL "
                    End If
                    If IdPeriodicidad.Value = "2" Then
                        nomina_name = " CATORCENAL "
                    End If
                    If IdPeriodicidad.Value = "3" Then
                        nomina_name = " QUINCENAL "
                    End If
                End If


                lblPeriodoTitulo.Text = "NÓMINA" & nomina_name & "DEL PERIODO: " & cPeriodo.FechaInicial.ToString & " AL " & cPeriodo.FechaFinal.ToString

                Call MostrarPercepciones()
                Call MostrarDeducciones()
                Call MostrarTotalNeto()

            End If
        End If
    End Sub
    Sub MostrarPercepciones()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa.Value
        cNomina.Ejercicio = IdEjercicio.Value
        cNomina.TipoNomina = IdPeriodicidad.Value 'Semanal
        cNomina.Periodo = IdPeriodo.Value
        dtPercepciones = cNomina.ConsultarPercepcionesCorridaSemanal()
        grdPercepciones.DataSource = dtPercepciones
        grdPercepciones.DataBind()
        cNomina = Nothing
    End Sub
    Sub MostrarDeducciones()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa.Value
        cNomina.Ejercicio = IdEjercicio.Value
        cNomina.TipoNomina = IdPeriodicidad.Value 'Semanal
        cNomina.Periodo = IdPeriodo.Value
        dtDeducciones = cNomina.ConsultarDeduccionesCorridaSemanal()
        grdDeducciones.DataSource = dtDeducciones
        grdDeducciones.DataBind()
        cNomina = Nothing
    End Sub
    Sub MostrarTotalNeto()
        lblTotalNeto.Text = "TOTAL: " & FormatCurrency(totalNetoPercepciones - totalNetoDeducciones, 2).ToString
        lblTotalGravado.Text = FormatCurrency(totalGravado, 2).ToString
        lblTotalExento.Text = FormatCurrency(totalExento, 2).ToString
    End Sub
    Private Sub grdPercepciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdPercepciones.NeedDataSource
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa.Value
        cNomina.Ejercicio = IdEjercicio.Value
        cNomina.TipoNomina = IdPeriodicidad.Value 'Semanal
        cNomina.Periodo = IdPeriodo.Value
        dtPercepciones = cNomina.ConsultarPercepcionesCorridaSemanal()
        grdPercepciones.DataSource = dtPercepciones
        cNomina = Nothing
    End Sub
    Private Sub grdDeducciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdDeducciones.NeedDataSource
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa.Value
        cNomina.Ejercicio = IdEjercicio.Value
        cNomina.TipoNomina = IdPeriodicidad.Value 'Semanal
        cNomina.Periodo = IdPeriodo.Value
        dtDeducciones = cNomina.ConsultarDeduccionesCorridaSemanal()
        grdDeducciones.DataSource = dtDeducciones
        cNomina = Nothing
    End Sub
    Private Sub ListarPercepciones()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa.Value
        cNomina.Ejercicio = IdEjercicio.Value
        cNomina.TipoNomina = IdPeriodicidad.Value 'Semanal
        cNomina.Periodo = IdPeriodo.Value
        dtPercepciones = cNomina.ConsultarPercepcionesCorridaSemanal()
        grdPercepciones.DataSource = dtPercepciones
        grdPercepciones.DataBind()
        cNomina = Nothing
    End Sub
    Private Sub ListarDeducciones()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa.Value
        cNomina.Ejercicio = IdEjercicio.Value
        cNomina.TipoNomina = IdPeriodicidad.Value 'Semanal
        cNomina.Periodo = IdPeriodo.Value
        dtDeducciones = cNomina.ConsultarDeduccionesCorridaSemanal()
        grdPercepciones.DataSource = dtDeducciones
        grdPercepciones.DataBind()
        cNomina = Nothing
    End Sub
    Private Sub grdPercepciones_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles grdPercepciones.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Footer
                If dtPercepciones.Rows.Count > 0 Then
                    If Not IsDBNull(dtPercepciones.Compute("sum(Total)", "")) Then
                        e.Item.Cells(4).Text = "TOTAL:"
                        e.Item.Cells(4).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(4).Font.Bold = True

                        e.Item.Cells(5).Text = FormatCurrency(dtPercepciones.Compute("sum(Total)", ""), 2).ToString
                        e.Item.Cells(5).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(5).Font.Bold = True
                        totalNetoPercepciones = dtPercepciones.Compute("sum(Total)", "")
                        totalGravado = dtPercepciones.Compute("sum(TotalGravado)", "")
                        totalExento = dtPercepciones.Compute("sum(TotalExento)", "")
                        lblPercepcionesTotales.Text = "$" + totalNetoPercepciones.ToString
                    End If
                End If
        End Select
    End Sub
    Private Sub grdDeducciones_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles grdDeducciones.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Footer
                If dtDeducciones.Rows.Count > 0 Then
                    If Not IsDBNull(dtDeducciones.Compute("sum(Total)", "")) Then
                        e.Item.Cells(4).Text = "TOTAL:"
                        e.Item.Cells(4).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(4).Font.Bold = True

                        e.Item.Cells(5).Text = FormatCurrency(dtDeducciones.Compute("sum(Total)", ""), 2).ToString
                        e.Item.Cells(5).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(5).Font.Bold = True
                        totalNetoDeducciones = dtDeducciones.Compute("sum(Total)", "")
                        lblDeduccionesTotales.Text = "$" + totalNetoDeducciones.ToString
                    End If
                End If
        End Select
    End Sub
    Private Sub RadListView1_NeedDataSource(sender As Object, e As RadListViewNeedDataSourceEventArgs) Handles RadListView1.NeedDataSource

        Dim dt As New DataTable
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa.Value
        cNomina.Ejercicio = IdEjercicio.Value
        cNomina.TipoNomina = IdPeriodicidad.Value 'Semanal
        cNomina.Periodo = IdPeriodo.Value
        dt = cNomina.ConsultarEmpleadosCorridaSemanal()
        RadListView1.DataSource = dt
        cNomina = Nothing
    End Sub
    Private Sub RadListView1_ItemDataBound(sender As Object, e As RadListViewItemEventArgs) Handles RadListView1.ItemDataBound
        Select Case e.Item.ItemType
            Case RadListViewItemType.DataItem, RadListViewItemType.AlternatingItem
                Dim dataItem = DirectCast(e.Item, RadListViewDataItem).DataItem

                Dim grdPercepciones As RadGrid = CType(e.Item.FindControl("GridPercepciones"), RadGrid)
                Dim grdDeducciones As RadGrid = CType(e.Item.FindControl("GridDeduciones"), RadGrid)

                Dim lblEjercicio As Label = CType(e.Item.FindControl("lblEjercicio"), Label)
                Dim lblNoPeriodo As Label = CType(e.Item.FindControl("lblNoPeriodo"), Label)
                Dim lblNumEmpleado As Label = CType(e.Item.FindControl("lblNumEmpleado"), Label)
                Dim lblRFC As Label = CType(e.Item.FindControl("lblRFC"), Label)
                Dim lblNombreEmpleado As Label = CType(e.Item.FindControl("lblNombreEmpleado"), Label)
                Dim lblNumImss As Label = CType(e.Item.FindControl("lblNumImss"), Label)
                'Dim lblRegContratacion As Label = CType(e.Item.FindControl("lblRegContratacion"), Label)
                Dim lblPuesto As Label = CType(e.Item.FindControl("lblPuesto"), Label)
                Dim lblCuotaDiaria As Label = CType(e.Item.FindControl("lblCuotaDiaria"), Label)
                Dim lblIntegradoImss As Label = CType(e.Item.FindControl("lblIntegradoImss"), Label)
                Dim lblDiasLaborados As Label = CType(e.Item.FindControl("lblDiasLaborados"), Label)

                Dim lblFechaFinal As Label = CType(e.Item.FindControl("lblFechaFinal"), Label)
                Dim lblFechaInicial As Label = CType(e.Item.FindControl("lblFechaInicial"), Label)

                Dim lblNeto As Label = CType(e.Item.FindControl("lblNeto"), Label)

                Dim cPeriodo As New Entities.Periodo
                cPeriodo.IdPeriodo = IdPeriodo.Value
                cPeriodo.ConsultarPeriodoID()

                lblFechaInicial.Text = cPeriodo.FechaInicial.ToString
                lblFechaFinal.Text = cPeriodo.FechaFinal.ToString

                Dim cEmpleado As New Entities.Empleado
                cEmpleado.IdEmpleado = dataItem("NoEmpleado")
                cEmpleado.ConsultarEmpleadoID()
                If cEmpleado.IdEmpleado > 0 Then
                    lblEjercicio.Text = IdEjercicio.Value.ToString
                    lblNoPeriodo.Text = IdPeriodo.Value.ToString
                    lblNumEmpleado.Text = dataItem("NoEmpleado")
                    lblRFC.Text = cEmpleado.Rfc
                    lblNombreEmpleado.Text = cEmpleado.Nombre
                    lblNumImss.Text = cEmpleado.IMSS
                    'lblRegContratacion.Text = cEmpleado.RegimenContratacion
                    lblPuesto.Text = cEmpleado.Puesto


                    'lblCuotaDiaria.Text = FormatCurrency(cEmpleado.CuotaDiaria, 2)
                    lblIntegradoImss.Text = FormatCurrency(cEmpleado.IntegradoImss, 2)
                End If

                lblFechaInicial_.Text = cPeriodo.FechaInicial.ToString
                lblFechaFinal_.Text = cPeriodo.FechaFinal.ToString

                Call ConsultarNumeroDeDiasPagados(dataItem("NoEmpleado"))

                dtGridPercepciones = New DataTable()
                Dim cNomina As New Nomina()
                cNomina.IdEmpresa = IdEmpresa.Value
                cNomina.Ejercicio = IdEjercicio.Value
                cNomina.TipoNomina = IdPeriodicidad.Value 'Semanal
                cNomina.Periodo = IdPeriodo.Value
                cNomina.TipoConcepto = "P"
                cNomina.Tipo = "N"
                cNomina.NoEmpleado = dataItem("NoEmpleado")
                dtGridPercepciones = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                grdPercepciones.DataSource = dtGridPercepciones
                grdPercepciones.DataBind()

                dtGridDeducciones = New DataTable()
                cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa.Value
                cNomina.Ejercicio = IdEjercicio.Value
                cNomina.TipoNomina = IdPeriodicidad.Value 'Semanal
                cNomina.Periodo = IdPeriodo.Value
                cNomina.TipoConcepto = "D"
                cNomina.Tipo = "N"
                cNomina.NoEmpleado = dataItem("NoEmpleado")
                dtGridDeducciones = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                grdDeducciones.DataSource = dtGridDeducciones
                grdDeducciones.DataBind()

                Dim dt As New DataTable()
                cNomina = New Nomina()

                cNomina.IdEmpresa = IdEmpresa.Value
                'cNomina.Ejercicio = IdEjercicio.Value
                'cNomina.TipoNomina = 1 'Semanal
                'cNomina.Periodo = IdPeriodo.Value
                'cNomina.TipoConcepto = "DE"
                'cNomina.CvoConcepto = 87
                'cNomina.NoEmpleado = dataItem("NoEmpleado")
                'dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()

                'If dt.Rows.Count > 0 Then
                '    lblIntegradoImss.Text = FormatCurrency(dt.Rows(0).Item("CuotaDiaria") * 1.0452, 2)
                'End If

                dt = New DataTable()
                cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa.Value
                cNomina.Ejercicio = IdEjercicio.Value
                cNomina.TipoNomina = IdPeriodicidad.Value 'Semanal
                cNomina.Periodo = IdPeriodo.Value
                cNomina.TipoConcepto = "DE"
                cNomina.CvoConcepto = 87
                cNomina.NoEmpleado = dataItem("NoEmpleado")
                dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()

                If dt.Rows.Count > 0 Then
                    lblCuotaDiaria.Text = FormatCurrency(dt.Rows(0).Item("CuotaDiaria"), 2)
                End If

                lblDiasLaborados.Text = NumeroDeDiasPagados.ToString

                lblNeto.Text = "NETO A PAGAR: " & FormatCurrency(totalPercepciones - totalDeducciones, 2).ToString

                totalPercepciones = 0
                totalDeducciones = 0
                cNomina = Nothing

        End Select
    End Sub
    Sub GridPercepciones_ItemDataBound(sender As Object, e As GridItemEventArgs)
        If (TypeOf e.Item Is GridDataItem) Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
            Dim Importe As String = dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("Importe").ToString()
            Dim fieldValue As Double = Double.Parse(Importe)
            totalPercepciones = (totalPercepciones + fieldValue)
        End If
        If (TypeOf e.Item Is GridFooterItem) Then
            Dim footerItem As GridFooterItem = CType(e.Item, GridFooterItem)
            footerItem("Concepto").Text = "TOTAL:"
            footerItem("Concepto").HorizontalAlign = HorizontalAlign.Right
            footerItem("Concepto").Font.Bold = True

            footerItem("Importe").Text = FormatCurrency(totalPercepciones, 2).ToString
            footerItem("Importe").HorizontalAlign = HorizontalAlign.Right
            footerItem("Importe").Font.Bold = True
        End If
    End Sub

    Sub GridDeduciones_ItemDataBound(sender As Object, e As GridItemEventArgs)
        If (TypeOf e.Item Is GridDataItem) Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
            Dim Importe As String = dataItem.OwnerTableView.DataKeyValues(dataItem.ItemIndex)("Importe").ToString()
            Dim fieldValue As Double = Double.Parse(Importe)
            totalDeducciones = (totalDeducciones + fieldValue)
        End If
        If (TypeOf e.Item Is GridFooterItem) Then
            Dim footerItem As GridFooterItem = CType(e.Item, GridFooterItem)
            footerItem("Concepto").Text = "TOTAL:"
            footerItem("Concepto").HorizontalAlign = HorizontalAlign.Right
            footerItem("Concepto").Font.Bold = True

            footerItem("Importe").Text = FormatCurrency(totalDeducciones, 2).ToString
            footerItem("Importe").HorizontalAlign = HorizontalAlign.Right
            footerItem("Importe").Font.Bold = True
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

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            cNomina.IdEmpresa = IdEmpresa.Value
            cNomina.Ejercicio = IdEjercicio.Value
            cNomina.TipoNomina = IdPeriodicidad.Value 'Semanal
            cNomina.Periodo = IdPeriodo.Value
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
    Private Sub gridReporte_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles gridReporte.NeedDataSource
        Dim dt As New DataTable
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa.Value
        cNomina.IdCliente = IdEmpresa.Value
        cNomina.Ejercicio = IdEjercicio.Value
        cNomina.TipoNomina = IdPeriodicidad.Value 'Semanal = 1
        cNomina.Periodo = IdPeriodo.Value

        cNomina.EsEspecial = EspecialBool.Value
        lblEjercicio_.Text = IdEjercicio.Value.ToString
        lblPeriodo_.Text = IdPeriodo.Value.ToString
        dt = cNomina.ConsultarDetalleNominaExtraordinaria()
        If dt.Rows.Count > 0 Then
            For Each row As DataRow In dt.Rows
                lblCliente_.Text = row("NombreCliente")
                If EspecialBool.Value = True Then
                    lblNoNomina.Text = row("idNomina")
                End If
            Next
        End If

        Dim cPeriodo As New Entities.Periodo
        cPeriodo.IdPeriodo = IdPeriodo.Value
        cPeriodo.ConsultarPeriodoID()

        lblFechaInicial_.Text = cPeriodo.FechaInicial.ToString
        lblFechaFinal_.Text = cPeriodo.FechaFinal.ToString

        gridReporte.DataSource = dt
        cNomina = Nothing
    End Sub

End Class