﻿Imports Entities
Imports Telerik.Web.UI

Public Class PeriodosCatorcenales
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Dim objCat As New DataControl()
            Dim cConcepto As New Entities.Catalogos
            objCat.CatalogoRad(cmbCliente, cConcepto.ConsultarMisClientes, True, False)
            objCat = Nothing

            Call CargarVariablesGenerales()
            registroId.Value = 0
        End If
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
                ejercicioId.Value = oDataRow("IdEjercicio")
            Next
        End If
    End Sub
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Response.Redirect("~/AgregarPeriodosCatorcenales.aspx")
    End Sub
    Private Sub GridPeriodosCatorcenales_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridPeriodosCatorcenales.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/EditorPeriodosCatorcenales.aspx?id=" & e.CommandArgument)
            Case "cmdDelete"
                registroId.Value = e.CommandArgument
                rwConfirmEliminaPeriodo.RadConfirm("¿Está seguro de eliminar el periodo catorcenal?", "confirmCallbackFn", 330, 180, Nothing, "Confirmar")
        End Select
    End Sub
    Private Sub GridPeriodosCatorcenales_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridPeriodosCatorcenales.NeedDataSource
        Dim cPeriodo As New Entities.Periodo
        cPeriodo.IdEmpresa = Session("IdEmpresa")
        cPeriodo.IdCliente = cmbCliente.SelectedValue
        cPeriodo.IdTipoNomina = 2 'Catorcenal
        cPeriodo.IdEjercicio = ejercicioId.Value
        GridPeriodosCatorcenales.DataSource = cPeriodo.ConsultarPeriodo
        cPeriodo = Nothing
    End Sub
    Private Sub HiddenButton_Click(sender As Object, e As EventArgs) Handles HiddenButton.Click
        Dim cPeriodo As New Entities.Periodo
        cPeriodo.IdPeriodo = registroId.Value
        cPeriodo.EliminaPeriodo()

        cPeriodo = New Entities.Periodo
        cPeriodo.IdEmpresa = Session("IdEmpresa")
        cPeriodo.IdCliente = cmbCliente.SelectedValue
        cPeriodo.IdTipoNomina = 2 'catorcenal
        cPeriodo.IdEjercicio = ejercicioId.Value
        GridPeriodosCatorcenales.DataSource = cPeriodo.ConsultarPeriodo()
        GridPeriodosCatorcenales.DataBind()
        cPeriodo = Nothing
    End Sub
    Private Sub btnConsultar_Click(sender As Object, e As EventArgs) Handles btnConsultar.Click
        Dim cPeriodo As New Entities.Periodo
        cPeriodo.IdEmpresa = Session("IdEmpresa")
        cPeriodo.IdCliente = cmbCliente.SelectedValue
        cPeriodo.IdTipoNomina = 2 'catorcenal
        cPeriodo.IdEjercicio = ejercicioId.Value
        GridPeriodosCatorcenales.DataSource = cPeriodo.ConsultarPeriodo
        GridPeriodosCatorcenales.DataBind()
        cPeriodo = Nothing
    End Sub

End Class