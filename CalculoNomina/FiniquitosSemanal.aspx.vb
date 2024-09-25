Imports Telerik.Web.UI
Imports Entities
Public Class FiniquitosSemanal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub

    Private Sub btnAll_Click(sender As Object, e As EventArgs) Handles btnAll.Click
        Call CargarGridEmpleados("")
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Call CargarGridEmpleados(txtSearch.Text)
    End Sub

    Private Sub CargarGridEmpleados(ByVal busqueda As String)
        Dim dtEmpleados As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 1 'Semanal
        dtEmpleados = cNomina.ConsultarEmpleadosFiniquito(busqueda)
        grdEmpleadosSemanal.DataSource = dtEmpleados
        grdEmpleadosSemanal.DataBind()
        cNomina = Nothing
    End Sub

    Private Sub grdEmpleadosSemanal_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles grdEmpleadosSemanal.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/GeneracionDeFiniquitosSemanal.aspx?id=" & e.CommandArgument.ToString, False)
        End Select
    End Sub

    Private Sub grdEmpleadosSemanal_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdEmpleadosSemanal.NeedDataSource
        Dim dtEmpleados As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 1 'Semanal
        dtEmpleados = cNomina.ConsultarEmpleadosFiniquito()
        grdEmpleadosSemanal.DataSource = dtEmpleados
        cNomina = Nothing
    End Sub

End Class