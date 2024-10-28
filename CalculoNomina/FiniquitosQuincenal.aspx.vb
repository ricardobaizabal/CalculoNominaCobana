Imports Telerik.Web.UI
Imports Entities
Public Class FiniquitosQuincenal
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub
    Private Sub CargarGridEmpleados(ByVal busqueda As String)
        Dim dtEmpleados As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 3 'Quincenal
        dtEmpleados = cNomina.ConsultarEmpleadosFiniquito(busqueda)
        grdEmpleadosQuincenal.DataSource = dtEmpleados
        grdEmpleadosQuincenal.DataBind()
        cNomina = Nothing
    End Sub
    Private Sub grdEmpleadosQuincenal_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles grdEmpleadosQuincenal.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/GeneracionDeFiniquitosQuincenal.aspx?id=" & e.CommandArgument.ToString, False)
        End Select
    End Sub
    Private Sub grdEmpleadosQuincenal_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdEmpleadosQuincenal.NeedDataSource
        Dim dtEmpleados As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 3 'Quincenal
        dtEmpleados = cNomina.ConsultarEmpleadosFiniquito()
        grdEmpleadosQuincenal.DataSource = dtEmpleados
        cNomina = Nothing
    End Sub
    Private Sub btnAll_Click(sender As Object, e As EventArgs) Handles btnAll.Click
        Call CargarGridEmpleados("")
    End Sub
    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Call CargarGridEmpleados(txtSearch.Text)
    End Sub

End Class