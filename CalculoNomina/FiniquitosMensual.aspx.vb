Imports Telerik.Web.UI
Imports Entities
Public Class FiniquitosMensual
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

        End If
    End Sub
    Private Sub CargarGridEmpleados()
        Dim dtEmpleados As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 4 'Mensual
        dtEmpleados = cNomina.ConsultarEmpleadosFiniquito()
        grdEmpleadosMensual.DataSource = dtEmpleados
        grdEmpleadosMensual.DataBind()
        cNomina = Nothing
    End Sub
    Private Sub grdEmpleadosSemanal_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles grdEmpleadosMensual.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/GeneracionDeFiniquitosMensual.aspx?id=" & e.CommandArgument.ToString, False)
        End Select
    End Sub
    Private Sub grdEmpleadosSemanal_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdEmpleadosMensual.NeedDataSource
        Dim dtEmpleados As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = Session("clienteid")
        cNomina.TipoNomina = 4 'Mensual
        dtEmpleados = cNomina.ConsultarEmpleadosFiniquito()
        grdEmpleadosMensual.DataSource = dtEmpleados
        cNomina = Nothing
    End Sub

End Class