Imports System.Data.SqlClient
Imports Telerik.Web.UI

Public Class ModuloPrestamos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' Cargar el DropDownList con los clientes
            CargarCliente()
        End If
    End Sub

    Private Sub CargarCliente()
        Dim objCat As New DataControl(1)
        Dim cConcepto As New Entities.Catalogos
        objCat.Catalogo(ddCliente, 0, cConcepto.ConsultarMisClientes, True)
    End Sub

    Private Sub GridEmployees_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridEmployees.NeedDataSource
        Dim dt As DataTable = ConsultarEmpleadosConPrestamoPersonalFiltro(ddCliente.SelectedValue)
        GridEmployees.DataSource = dt
    End Sub

    Public Function ConsultarEmpleadosConPrestamoPersonalFiltro(clienteId As String) As DataTable
        Dim cEmpleado As New Entities.PrestamoPersonal
        Dim dt As DataTable = cEmpleado.ConsultarEmpleadosConPrestamoPersonalFiltro(clienteId)
        Return dt
    End Function

    Private Sub GridEmployees_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridEmployees.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("AgregarEditarEmpleado.aspx?tab=8&id=" & e.CommandArgument)

        End Select
    End Sub

    Protected Sub btnbuscador_Click(sender As Object, e As EventArgs) Handles btnbuscador.Click
        GridEmployees.Rebind()
    End Sub

End Class
