Imports System.Threading
Imports System.Globalization
Imports Telerik.Web.UI
Imports Entities

Public Class DiasLaborados
    Inherits System.Web.UI.Page

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
        grdReporteDiasLaborados.DataSource = cNomina.ReporteDiasLaborados()
        grdReporteDiasLaborados.DataBind()
        cNomina = Nothing
    End Sub

    Private Sub grdReporteDiasLaborados_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdReporteDiasLaborados.NeedDataSource
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = cmbEjercicio.SelectedValue
        grdReporteDiasLaborados.DataSource = cNomina.ReporteDiasLaborados()
        cNomina = Nothing
    End Sub

End Class