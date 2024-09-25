Imports Telerik.Web.UI

Public Class ModuloInfonavit
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarCliente()
        End If
    End Sub

    Private Sub CargarCliente()
        Dim objCat As New DataControl(1)
        Dim cConcepto As New Entities.Catalogos
        objCat.Catalogo(ddCliente, 0, cConcepto.ConsultarMisClientes, True)
    End Sub

    Private Sub GridEmployees_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridEmployees.NeedDataSource
        Dim clienteId As Integer = Convert.ToInt32(ddCliente.SelectedValue)
        Dim dt As DataTable = ConsultarInfonavitFiltro(clienteId)
        GridEmployees.DataSource = dt
    End Sub

    Public Function ConsultarInfonavitFiltro(pIdEmpresa As Integer) As DataTable
        Dim cEmpleado As New Entities.Empleado
        Dim dt As DataTable = cEmpleado.ConsultarInfonavitFiltro(pIdEmpresa)
        Return dt
    End Function

    Private Sub GridEmployees_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridEmployees.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("AgregarEditarEmpleado.aspx?tab=3&id=" & e.CommandArgument)
        End Select
    End Sub

    Protected Sub btnbuscador_Click(sender As Object, e As EventArgs) Handles btnbuscador.Click
        GridEmployees.Rebind()
    End Sub
    Protected Sub GridEmployees_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridEmployees.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim item As GridDataItem = DirectCast(e.Item, GridDataItem)
            Dim inicioDescuento As String = item("inicio_descuento").Text
            Dim finDescuento As String = item("fin_descuento").Text

            If inicioDescuento = "01/01/1900" Then
                item("inicio_descuento").Text = ""
            End If

            If finDescuento = "01/01/1900" Then
                item("fin_descuento").Text = ""
            End If
        End If
    End Sub

End Class
