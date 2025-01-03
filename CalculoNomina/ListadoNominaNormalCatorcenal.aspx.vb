﻿Imports Telerik.Web.UI
Imports Entities

Public Class ListadoNominaNormalCatorcenal
    Inherits System.Web.UI.Page
    Dim ObjData As New DataControl()
    Private IdEmpresa As Integer = 0
    Private IdEjercicio As Integer = 0
    Private dtEmpleados As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objCat As New DataControl()
            Dim cConcepto As New Entities.Catalogos
            objCat.CatalogoRad(cmbCliente, cConcepto.ConsultarMisClientes, True, True)
            objCat.CatalogoRad(cmbPeriodicidad, cConcepto.ConsultarPeriodoPago2, True, True)
            cmbPeriodicidad.SelectedValue = 2
            CargarGridNominas()
            CargaPeriodos(cmbPeriodicidad.SelectedValue)
            cmbPeriodicidad.Enabled = False

            Session("Folio") = Nothing

        End If
    End Sub

    Private Sub CargarGridNominas()

        Call CargarVariablesGenerales()

        Dim TipoNomina, IdCliente, Periodo As Integer

        If cmbPeriodicidad.SelectedValue.ToString() = "" Then
            TipoNomina = 0
        Else
            TipoNomina = cmbPeriodicidad.SelectedValue
        End If

        If cmbCliente.SelectedValue.ToString() = "" Then
            IdCliente = 0
        Else
            IdCliente = cmbCliente.SelectedValue
        End If

        If cmbPeriodo.SelectedValue.ToString() = "" Then
            Periodo = 0
        Else
            Periodo = cmbPeriodo.SelectedValue
        End If

        Dim dt_nominas As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = IdCliente
        cNomina.Ejercicio = IdEjercicio
        cNomina.EsEspecial = False
        cNomina.TipoNomina = TipoNomina
        cNomina.Periodo = Periodo
        GridNominas.DataSource = cNomina.ConsultarTodasNominaExtraordinaria()
        GridNominas.DataBind()

    End Sub

    Private Sub GridNominas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridNominas.NeedDataSource

        Call CargarVariablesGenerales()

        Dim TipoNomina, IdCliente, Periodo As Integer

        If cmbPeriodicidad.SelectedValue.ToString() = "" Then
            TipoNomina = 0
        Else
            TipoNomina = cmbPeriodicidad.SelectedValue
        End If

        If cmbCliente.SelectedValue.ToString() = "" Then
            IdCliente = 0
        Else
            IdCliente = cmbCliente.SelectedValue
        End If

        If cmbPeriodo.SelectedValue.ToString() = "" Then
            Periodo = 0
        Else
            Periodo = cmbPeriodo.SelectedValue
        End If

        Dim dt_nominas As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.IdEmpresa = IdEmpresa
        cNomina.IdCliente = IdCliente
        cNomina.Ejercicio = IdEjercicio
        cNomina.EsEspecial = False
        cNomina.TipoNomina = TipoNomina
        cNomina.Periodo = Periodo
        GridNominas.DataSource = cNomina.ConsultarTodasNominaExtraordinaria()
        cNomina = Nothing
    End Sub

    Private Sub GridNominas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridNominas.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Dim folio As String = e.CommandArgument.ToString()
                Session("Folio") = folio
                Response.Redirect("GeneracionDeNominaCatorcenalNormal.aspx")
        End Select
    End Sub

    Private Sub btnAgregarNominaE_Click(sender As Object, e As EventArgs) Handles btnAgregarNominaE.Click
        Session("Folio") = Nothing
        Response.Redirect("GeneracionDeNominaCatorcenalNormal.aspx")
    End Sub

    Private Sub btnBuscarNominas_Click(sender As Object, e As EventArgs) Handles btnBuscarNominas.Click
        CargarGridNominas()
    End Sub

    Private Sub CargaPeriodos(Optional ByVal IdTipoNomina As Integer = 0)
        Call CargarVariablesGenerales()
        Dim cPeriodo As New Entities.Periodo
        cPeriodo.IdEmpresa = IdEmpresa
        cPeriodo.IdCliente = cmbCliente.SelectedValue
        cPeriodo.IdEjercicio = IdEjercicio
        cPeriodo.IdTipoNomina = IdTipoNomina
        cPeriodo.ExtraordinarioBit = False
        ObjData.CatalogoRad(cmbPeriodo, cPeriodo.ConsultarPeriodos(), True, True)
        ObjData = Nothing
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

    Private Sub cmbCliente_SelectedIndexChanged(sender As Object, e As RadComboBoxSelectedIndexChangedEventArgs) Handles cmbCliente.SelectedIndexChanged
        CargaPeriodos(cmbPeriodicidad.SelectedValue)
    End Sub

End Class