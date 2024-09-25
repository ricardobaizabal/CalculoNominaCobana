Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Net.Mail
Imports System.IO
Imports System.Threading
Imports System.Globalization
Imports System.Text


Public Class Empleado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim objCat As New DataControl(1)
            Dim cConcepto As New Entities.Catalogos
            Call CargarCliente()
            objCat.CatalogoRad(cmbCliente, cConcepto.ConsultarMisClientes, True, True)
            objCat = Nothing
        End If
    End Sub
    Private Sub btnAgregaEmpleado_Click(sender As Object, e As EventArgs) Handles btnAgregaEmpleado.Click
        Response.Redirect("AgregarEditarEmpleado.aspx?id=0")
    End Sub
    Private Sub GridEmployees_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridEmployees.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                'EditEmployee(e.CommandArgument)
                Response.Redirect("AgregarEditarEmpleado.aspx?id=" & e.CommandArgument)
        End Select
    End Sub
    Private Sub GridEmployees_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridEmployees.NeedDataSource
        Dim cEmpleado As New Entities.Empleado
        GridEmployees.DataSource = cEmpleado.ConsultarEmpleado()
        cEmpleado = Nothing
    End Sub
    Private Sub btnbuscador_Click(sender As Object, e As EventArgs) Handles btnbuscador.Click
        Dim cEmpleado As New Entities.Empleado
        GridEmployees.DataSource = cEmpleado.ConsultarEmpleadosFiltros(txtbuscador.Text, cmbCliente.SelectedValue)
        GridEmployees.DataBind()
        cEmpleado = Nothing
    End Sub
    Private Sub CargarCliente()
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCliente @cmd=4, @clienteId='" & Session("clienteid") & "'", conn)
        conn.Open()
        Dim rs As SqlDataReader
        rs = cmd.ExecuteReader()
        If rs.Read Then
            Session("Cliente") = rs("razonsocial")
        End If
        conn.Close()
        conn.Dispose()
    End Sub
    Protected Function GetImageUrl(ByVal sueldos_y_salarios As Object) As String
        If Convert.ToBoolean(sueldos_y_salarios) Then
            Return "~/images/action_check.gif"
        Else
            Return "~/images/action_delete.gif"
        End If
    End Function

End Class