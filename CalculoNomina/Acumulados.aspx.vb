Imports Telerik.Web.UI
Imports Entities

Public Class Acumulados
    Inherits System.Web.UI.Page

    Private dtPercepciones As New DataTable
    Private dtDeducciones As New DataTable

    Private totalPercepciones As Double = 0
    Private totalDeducciones As Double = 0
    Private totalNetoPercepciones As Double = 0
    Private totalNetoDeducciones As Double = 0
    Private totalGravado As Double = 0
    Private totalExento As Double = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim cConcepto As New Entities.Catalogos
            Dim ObjData As New DataControl()
            objData.Catalogo(cmbEjercicio, 0, cConcepto.ConsultarEjercicio, True)
            objData.Catalogo(cmbRegistroPatronal, 0, cConcepto.ConsultarRegistroPatronal, True)
            objData.Catalogo(cmbCliente, 0, cConcepto.ConsultarClientesNomina, True)
            objData = Nothing
            cConcepto = Nothing

            Call MostrarTotalNeto()

        End If
    End Sub
    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Call ConsultarAcumuladosPercepciones()
        Call ConsultarAcumuladosDeducciones()
        Call MostrarTotalNeto()
    End Sub
    Private Sub grdPercepciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdPercepciones.NeedDataSource
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = cmbEjercicio.SelectedValue
        cNomina.Mes = cmbMes.SelectedValue
        cNomina.IdRegistroPatronal = cmbRegistroPatronal.SelectedValue
        'cNomina.IdEmpresa = cmbCliente.SelectedValue
        cNomina.TipoConcepto = "P"
        dtPercepciones = cNomina.ConsultarAcumulados()
        grdPercepciones.DataSource = dtPercepciones
        cNomina = Nothing
    End Sub
    Private Sub grdDeducciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdDeducciones.NeedDataSource
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = cmbEjercicio.SelectedValue
        cNomina.Mes = cmbMes.SelectedValue
        cNomina.IdRegistroPatronal = cmbRegistroPatronal.SelectedValue
        'cNomina.IdEmpresa = cmbCliente.SelectedValue
        cNomina.TipoConcepto = "D"
        dtDeducciones = cNomina.ConsultarAcumulados()
        grdDeducciones.DataSource = dtDeducciones
        cNomina = Nothing
    End Sub
    Private Sub ConsultarAcumuladosPercepciones()
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = cmbEjercicio.SelectedValue
        cNomina.Mes = cmbMes.SelectedValue
        cNomina.IdRegistroPatronal = cmbRegistroPatronal.SelectedValue
        'cNomina.IdEmpresa = cmbCliente.SelectedValue
        cNomina.TipoConcepto = "P"
        dtPercepciones = cNomina.ConsultarAcumulados()
        grdPercepciones.DataSource = dtPercepciones
        grdPercepciones.DataBind()
        cNomina = Nothing
    End Sub
    Private Sub ConsultarAcumuladosDeducciones()
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = cmbEjercicio.SelectedValue
        cNomina.Mes = cmbMes.SelectedValue
        cNomina.IdRegistroPatronal = cmbRegistroPatronal.SelectedValue
        'cNomina.IdEmpresa = cmbCliente.SelectedValue
        cNomina.TipoConcepto = "D"
        dtDeducciones = cNomina.ConsultarAcumulados()
        grdDeducciones.DataSource = dtDeducciones
        grdDeducciones.DataBind()
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
        Call MostrarTotalNeto()
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
        Call MostrarTotalNeto()
    End Sub
    Sub MostrarTotalNeto()
        lblTotalNeto.Text = "TOTAL: " & FormatCurrency(totalNetoPercepciones - totalNetoDeducciones, 2).ToString
        lblTotalGravado.Text = FormatCurrency(totalGravado, 2).ToString
        lblTotalExento.Text = FormatCurrency(totalExento, 2).ToString
    End Sub

End Class