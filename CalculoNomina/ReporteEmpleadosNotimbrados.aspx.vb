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



Public Class ReporteEmpleadosNotimbrados
    Inherits System.Web.UI.Page
    Dim ObjData As New DataControl()
    Private IdEjercicio As Integer = 0
    Private dtEmpleados As DataTable

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objCat As New DataControl(1)
            Dim cConcepto As New Entities.Catalogos
            objCat.CatalogoRad(cmbEmpresa, cConcepto.ConsultarMisClientes, True, False)
            objCat.CatalogoRad(cmbPeriodicidad, cConcepto.ConsultarPeriodoPago2, True, False)
            CargarGridEmpleadosNoTimbrados()
        End If
    End Sub


    Private Sub btnBuscarEmpleados_Click(sender As Object, e As EventArgs) Handles btnBuscarEmpleados.Click
        CargarGridEmpleadosNoTimbrados()
    End Sub


    Private Sub CargarGridEmpleadosNoTimbrados()

        Call CargarVariablesGenerales()
        Dim periodicidad, cliente, periodo As Integer

        If cmbPeriodicidad.SelectedValue.ToString() = "" Then
            periodicidad = 0
        Else
            periodicidad = cmbPeriodicidad.SelectedValue
        End If

        If cmbEmpresa.SelectedValue.ToString() = "" Then
            cliente = 0
        Else
            cliente = cmbEmpresa.SelectedValue
        End If

        If cmbPeriodo.SelectedValue.ToString() = "" Then
            periodo = 0
        Else
            periodo = cmbPeriodo.SelectedValue
        End If

        Dim cNomina As New Nomina()
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = periodicidad
        cNomina.Periodo = periodo
        cNomina.Cliente = cliente
        dtEmpleados = cNomina.ConsultarEmpleadosNoTimbrados()
        GridEmpleadosNoTimbrados.DataSource = dtEmpleados
        GridEmpleadosNoTimbrados.DataBind()
    End Sub



    Private Sub GridEmpleadosNoTimbrados_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridEmpleadosNoTimbrados.NeedDataSource

        Call CargarVariablesGenerales()
        Dim periodicidad, cliente, periodo As Integer

        If cmbPeriodicidad.SelectedValue.ToString() = "" Then
            periodicidad = 0
        Else
            periodicidad = cmbPeriodicidad.SelectedValue
        End If

        If cmbEmpresa.SelectedValue.ToString() = "" Then
            cliente = 0
        Else
            cliente = cmbEmpresa.SelectedValue
        End If

        If cmbPeriodo.SelectedValue.ToString() = "" Then
            periodo = 0
        Else
            periodo = cmbPeriodo.SelectedValue
        End If

        Dim cNomina As New Nomina()
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = periodicidad
        cNomina.Periodo = periodo
        cNomina.Cliente = cliente
        dtEmpleados = cNomina.ConsultarEmpleadosNoTimbrados()
        GridEmpleadosNoTimbrados.DataSource = dtEmpleados

        cNomina = Nothing

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