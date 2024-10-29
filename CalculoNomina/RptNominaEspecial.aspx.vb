Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Entities
Public Class RptNominaEspecial
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
    Private DiasPagados As Decimal = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request("e")) Then
                IdEmpresa.Value = Request("e")
            End If
            If Not String.IsNullOrEmpty(Request("ej")) Then
                IdEjercicio.Value = Request("ej")
            End If
            If Not String.IsNullOrEmpty(Request("t")) Then
                IdTipoNomina.Value = Request("t")
            End If
            If Not String.IsNullOrEmpty(Request("p")) Then
                IdPeriodo.Value = Request("p")
            End If

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

            lblPeriodoTitulo.Text = "NÓMINA ESPECIAL - AGUINALDOS " & IdEjercicio.Value.ToString

            Call MostrarPercepciones()
            Call MostrarDeducciones()
            Call MostrarTotalNeto()

        End If
    End Sub
    Sub MostrarPercepciones()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa.Value
        cNomina.Ejercicio = IdEjercicio.Value
        cNomina.TipoNomina = IdTipoNomina.Value
        cNomina.Periodo = IdPeriodo.Value
        cNomina.Tipo = "A"
        cNomina.TipoConcepto = "P"
        dtPercepciones = cNomina.ConsultarPercepcionesDeduccionesNominaEspecial()
        grdPercepciones.DataSource = dtPercepciones
        grdPercepciones.DataBind()
        cNomina = Nothing
    End Sub
    Sub MostrarDeducciones()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa.Value
        cNomina.Ejercicio = IdEjercicio.Value
        cNomina.TipoNomina = IdTipoNomina.Value
        cNomina.Periodo = IdPeriodo.Value
        cNomina.Tipo = "A"
        cNomina.TipoConcepto = "D"
        dtDeducciones = cNomina.ConsultarPercepcionesDeduccionesNominaEspecial()
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
        cNomina.TipoNomina = IdTipoNomina.Value
        cNomina.Periodo = IdPeriodo.Value
        cNomina.Tipo = "A"
        cNomina.TipoConcepto = "P"
        dtPercepciones = cNomina.ConsultarPercepcionesDeduccionesNominaEspecial()
        grdPercepciones.DataSource = dtPercepciones
        cNomina = Nothing
    End Sub
    Private Sub grdDeducciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdDeducciones.NeedDataSource
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa.Value
        cNomina.Ejercicio = IdEjercicio.Value
        cNomina.TipoNomina = IdTipoNomina.Value
        cNomina.Periodo = IdPeriodo.Value
        cNomina.Tipo = "A"
        cNomina.TipoConcepto = "D"
        dtDeducciones = cNomina.ConsultarPercepcionesDeduccionesNominaEspecial()
        grdDeducciones.DataSource = dtDeducciones
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
                    End If
                End If
        End Select
    End Sub
    Private Sub RadListView1_NeedDataSource(sender As Object, e As RadListViewNeedDataSourceEventArgs) Handles RadListView1.NeedDataSource
        Dim dt As New DataTable
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa.Value
        cNomina.Ejercicio = IdEjercicio.Value
        cNomina.TipoNomina = IdTipoNomina.Value
        cNomina.Periodo = IdPeriodo.Value
        cNomina.Tipo = "A"
        dt = cNomina.ConsultarEmpleadosNominaEspecial()
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
                Dim lblPuesto As Label = CType(e.Item.FindControl("lblPuesto"), Label)
                Dim lblCuotaDiaria As Label = CType(e.Item.FindControl("lblCuotaDiaria"), Label)
                Dim lblIntegradoImss As Label = CType(e.Item.FindControl("lblIntegradoImss"), Label)
                Dim lblDiasLaborados As Label = CType(e.Item.FindControl("lblDiasLaborados"), Label)

                Dim lblFechaInicial As Label = CType(e.Item.FindControl("lblFechaInicial"), Label)
                Dim lblFechaFinal As Label = CType(e.Item.FindControl("lblFechaFinal"), Label)

                Dim lblNeto As Label = CType(e.Item.FindControl("lblNeto"), Label)

                lblFechaInicial.Text = "15/12/" & IdEjercicio.Value.ToString
                lblFechaFinal.Text = "15/12/" & IdEjercicio.Value.ToString

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
                    lblPuesto.Text = cEmpleado.Puesto
                End If

                Call ConsultarNumeroDeDiasPagados(dataItem("NoEmpleado"))

                dtGridPercepciones = New DataTable()
                Dim cNomina As New Nomina()
                cNomina.IdEmpresa = IdEmpresa.Value
                cNomina.Ejercicio = IdEjercicio.Value
                cNomina.TipoNomina = IdTipoNomina.Value
                cNomina.Periodo = IdPeriodo.Value
                cNomina.TipoConcepto = "P"
                cNomina.Tipo = "A"
                cNomina.NoEmpleado = dataItem("NoEmpleado")
                dtGridPercepciones = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                grdPercepciones.DataSource = dtGridPercepciones
                grdPercepciones.DataBind()

                dtGridDeducciones = New DataTable()
                cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa.Value
                cNomina.Ejercicio = IdEjercicio.Value
                cNomina.TipoNomina = IdTipoNomina.Value
                cNomina.Periodo = IdPeriodo.Value
                cNomina.TipoConcepto = "D"
                cNomina.Tipo = "A"
                cNomina.NoEmpleado = dataItem("NoEmpleado")
                dtGridDeducciones = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
                grdDeducciones.DataSource = dtGridDeducciones
                grdDeducciones.DataBind()

                Dim dt As New DataTable()
                cNomina = New Nomina()

                cNomina.IdEmpresa = IdEmpresa.Value
                cNomina.Ejercicio = IdEjercicio.Value
                cNomina.TipoNomina = IdTipoNomina.Value
                cNomina.Periodo = IdPeriodo.Value
                cNomina.TipoConcepto = "DE"
                cNomina.CvoConcepto = 87
                cNomina.NoEmpleado = dataItem("NoEmpleado")
                dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()

                If dt.Rows.Count > 0 Then
                    lblIntegradoImss.Text = FormatCurrency(dt.Rows(0).Item("CuotaDiaria") * 1.0452, 2)
                End If

                dt = New DataTable()
                cNomina = New Nomina()
                cNomina.IdEmpresa = IdEmpresa.Value
                cNomina.Ejercicio = IdEjercicio.Value
                cNomina.TipoNomina = IdTipoNomina.Value
                cNomina.Periodo = IdPeriodo.Value
                cNomina.TipoConcepto = "DE"
                cNomina.CvoConcepto = 87
                cNomina.NoEmpleado = dataItem("NoEmpleado")
                dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()

                If dt.Rows.Count > 0 Then
                    lblCuotaDiaria.Text = FormatCurrency(dt.Rows(0).Item("CuotaDiaria"), 2)
                End If

                lblDiasLaborados.Text = DiasPagados.ToString

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
            Dim DiasLaboradosAnio As Decimal = 0

            Dim datos As New DataTable()
            Dim cNomina As New Nomina()
            cNomina.IdEmpresa = IdEmpresa.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.TipoNomina = IdTipoNomina.Value
            cNomina.Ejercicio = IdEjercicio.Value
            datos = cNomina.ConsultarDesgloseAguinaldo()
            cNomina = Nothing

            If datos.Rows.Count > 0 Then
                For Each oDataRow In datos.Rows
                    DiasLaboradosAnio = oDataRow("DiasLaboradosAnio")
                Next
            End If

            DiasPagados = Math.Round(((DiasLaboradosAnio * 15) / 365), 2)

        Catch oExcep As Exception
            Response.Write(oExcep.Message)
            Response.End()
        End Try
    End Sub

End Class