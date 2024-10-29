Imports Telerik.Web.UI
Imports Entities
Imports System.Xml
Imports System.IO
Imports System.Security.Cryptography.X509Certificates
Imports System.Security.Cryptography
Imports Telerik.Reporting.Processing
Imports ThoughtWorks.QRCode.Codec
Imports System.Net.Mail
Imports Ionic.Zip
Imports System.Web.Services.Protocols
Imports System.Net.Security
Imports System.Data.SqlClient
Imports System.Data
Imports System.Globalization

Public Class ListadoNominaExtraordinaria
    Inherits System.Web.UI.Page
    Dim ObjData As New DataControl()
    Private IdEjercicio As Integer = 0
    Private dtEmpleados As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objCat As New DataControl(1)
            Dim cConcepto As New Entities.Catalogos
            objCat.CatalogoRad(cmbCliente, cConcepto.ConsultarMisClientes, True, False)
            objCat.CatalogoRad(cmbPeriodicidad, cConcepto.ConsultarPeriodoPago2, True, False)
            CargarGridNominas()
        End If
    End Sub
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

        Dim dt_nominas As New DataTable()
        Dim cNomina As New Nomina()
        cNomina.Ejercicio = IdEjercicio
        cNomina.EsEspecial = True
        cNomina.TipoNomina = periodicidad
        cNomina.Periodo = periodo
        cNomina.Cliente = cliente
        GridNominas.DataSource = cNomina.ConsultarTodasNominaExtraordinaria()
        GridNominas.DataBind()
    End Sub
    Private Sub GridNominas_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridNominas.NeedDataSource
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

        Dim cNomina As New Nomina()
        cNomina.Ejercicio = IdEjercicio
        cNomina.EsEspecial = True
        cNomina.TipoNomina = periodicidad
        cNomina.Periodo = periodo
        cNomina.Cliente = cliente
        GridNominas.DataSource = cNomina.ConsultarTodasNominaExtraordinaria()
        cNomina = Nothing
    End Sub
    Protected Sub GridNominas_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles GridNominas.ItemDataBound
        'If TypeOf e.Item Is GridDataItem Then
        '    Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

        '    Dim generadoValue As String = dataItem("Generado").Text
        '    Dim TimbradoValue As String = dataItem("Timbrado").Text

        '    If String.IsNullOrEmpty(generadoValue) OrElse generadoValue = "N" OrElse generadoValue = "&nbsp;" Then
        '        dataItem("Generado").Text = "No"
        '    ElseIf generadoValue = "S" Then
        '        dataItem("Generado").Text = "Sí"
        '    End If

        '    If String.IsNullOrEmpty(TimbradoValue) OrElse TimbradoValue = "N" OrElse TimbradoValue = "&nbsp;" Then
        '        dataItem("Timbrado").Text = "No"
        '    ElseIf TimbradoValue = "S" Then
        '        dataItem("Timbrado").Text = "Sí"
        '    End If
        'End If
    End Sub
    Private Sub GridNominas_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridNominas.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Dim folio As String = e.CommandArgument.ToString()
                Session("Folio") = folio
                Response.Redirect("GeneracionDeNominaExtraordinaria.aspx")
        End Select
    End Sub
    Private Sub btnAgregarNominaE_Click(sender As Object, e As EventArgs) Handles btnAgregarNominaE.Click
        Session("Folio") = Nothing
        Response.Redirect("GeneracionDeNominaExtraordinaria.aspx")
    End Sub
    Private Sub btnBuscarNominas_Click(sender As Object, e As EventArgs) Handles btnBuscarNominas.Click
        CargarGridNominas()
    End Sub
    Private Sub cmbPeriodicidad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPeriodicidad.SelectedIndexChanged
        CargaPeriodos(cmbPeriodicidad.SelectedValue)
    End Sub
    Private Sub CargaPeriodos(Optional ByVal IdTipoNomina As Integer = 0)
        Call CargarVariablesGenerales()
        Dim cPeriodo As New Entities.Periodo
        cPeriodo.IdEjercicio = IdEjercicio
        cPeriodo.IdTipoNomina = IdTipoNomina
        cPeriodo.ExtraordinarioBit = False
        ObjData.CatalogoRad(cmbPeriodo, cPeriodo.ConsultarPeriodos(), True, False)
        ObjData = Nothing
    End Sub
    Private Sub CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cConfiguracion = New Configuracion()
        cConfiguracion.IdUsuario = Session("usuarioid")
        dt = cConfiguracion.ConsultarConfiguracion()
        cConfiguracion = Nothing

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                IdEjercicio = oDataRow("IdEjercicio")
            Next
        End If
    End Sub

End Class