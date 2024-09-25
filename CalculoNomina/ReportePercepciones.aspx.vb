Imports Entities
Imports Telerik.Web.UI
Public Class ReportePercepciones
    Inherits System.Web.UI.Page

    Private dtPercepciones As New DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objData As New DataControl
            Dim cConcepto As New Entities.Catalogos
            objData.Catalogo(cmbEjercicio, 0, cConcepto.ConsultarEjercicio)
            objData = Nothing
            cConcepto = Nothing
            LlenaComboPeriodos(0)
        End If
    End Sub
    Private Sub LlenaComboPeriodos(ByVal sel As Integer)

        Dim cPeriodo As New Entities.Periodo
        cPeriodo.IdEjercicio = cmbEjercicio.SelectedValue
        cPeriodo.IdTipoNomina = cmbTipoNomina.SelectedValue
        cPeriodo.ExtraordinarioBit = 0
        Dim objData As New DataControl
        objData.Catalogo(cmbPeriodo, sel, cPeriodo.ConsultarPeriodos())
        cPeriodo = Nothing
        If sel > 0 Then
            cmbPeriodo.SelectedValue = sel
            lblTitulo.Text = "Periodo " & cmbPeriodo.SelectedItem.Text
        End If
    End Sub
    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click

        Dim cNomina As New Nomina()
        cNomina.Ejercicio = cmbEjercicio.SelectedValue
        cNomina.TipoNomina = cmbTipoNomina.SelectedValue
        cNomina.Periodo = cmbPeriodo.SelectedValue
        dtPercepciones = cNomina.ReportePercepciones()
        grdPercepciones.DataSource = dtPercepciones
        grdPercepciones.DataBind()
        cNomina = Nothing

    End Sub
    Private Sub grdPercepciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdPercepciones.NeedDataSource

        Dim cNomina As New Nomina()
        cNomina.Ejercicio = cmbEjercicio.SelectedValue
        cNomina.TipoNomina = cmbTipoNomina.SelectedValue
        cNomina.Periodo = cmbPeriodo.SelectedValue
        dtPercepciones = cNomina.ReportePercepciones()
        grdPercepciones.DataSource = dtPercepciones
        cNomina = Nothing

    End Sub
    Private Sub grdPercepciones_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles grdPercepciones.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Footer
                If dtPercepciones.Rows.Count > 0 Then
                    If Not IsDBNull(dtPercepciones.Compute("sum(Total)", "")) Then
                        e.Item.Cells(8).Text = "TOTAL:"
                        e.Item.Cells(8).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(8).Font.Bold = True

                        e.Item.Cells(9).Text = FormatCurrency(dtPercepciones.Compute("sum(Total)", ""), 2).ToString
                        e.Item.Cells(9).HorizontalAlign = HorizontalAlign.Right
                        e.Item.Cells(9).Font.Bold = True
                    End If
                End If
        End Select
    End Sub
    Private Sub cmbTipoNomina_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipoNomina.SelectedIndexChanged
        Call LlenaComboPeriodos(0)
    End Sub

End Class