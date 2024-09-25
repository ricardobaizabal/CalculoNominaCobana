Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Net.Mail
Imports System.IO
Imports System.Threading
Imports System.Globalization
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf


Public Class Puesto
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    'Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
    '    Dim cPuesto As New Entities.Puesto

    '    If registroId.Value > 0 Then
    '        cPuesto.IdPuesto = registroId.Value
    '        cPuesto.Descripcion = txtDescripcion.Text.Trim
    '        cPuesto.UpdatePuesto()
    '    Else
    '        cPuesto.Descripcion = txtDescripcion.Text.Trim
    '        cPuesto.GuadarPuesto()
    '    End If
    '    cPuesto = Nothing
    '    resetControles()
    '    CargarGrid()
    'End Sub
    'Private Sub resetControles()
    '    txtDescripcion.Text = ""
    '    registroId.Value = 0
    'End Sub

    Private Sub CargarGrid()
        Dim cPuesto As New Entities.Puesto
        GridPuesto.DataSource = cPuesto.ConsultarPuesto()
        GridPuesto.DataBind()
        cPuesto = Nothing
    End Sub
    Private Sub GridPuesto_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridPuesto.NeedDataSource
        Dim cPuesto As New Entities.Puesto
        GridPuesto.DataSource = cPuesto.ConsultarPuesto()
        cPuesto = Nothing
    End Sub
    'Private Sub GridPuesto_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridPuesto.ItemDataBound
    '    Select Case e.Item.ItemType
    '        Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
    '            Dim eliminar As ImageButton = CType(e.Item.FindControl("eliminar"), ImageButton)
    '            eliminar.Attributes.Add("onclick", "javascript:return confirm('Va a borrar un registro. ¿Desea continuar?');")
    '    End Select
    'End Sub
    'Private Sub GridPuesto_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridPuesto.ItemCommand
    '    Select Case e.CommandName
    '        Case "cmdEdit"
    '            Response.Redirect("~/EditorPuesto.aspx?id=" & e.CommandArgument)
    '        Case "cmdDelete"
    '            Dim cPuesto As New Entities.Puesto
    '            cPuesto.IdPuesto = e.CommandArgument
    '            cPuesto.EliminarPuesto()
    '            Call CargarGrid()
    '    End Select
    'End Sub

    Private Sub GridPuesto_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridPuesto.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaPuesto(e.CommandArgument)
            Case "cmdDelete"
                Dim ObjData As New DataControl(1)
                ObjData.RunSQLQuery("exec pPuestos @cmd=3, @puestoid='" & e.CommandArgument.ToString & "'")
                CargarGrid()
        End Select
    End Sub

    Private Sub EditaPuesto(ByVal id As Integer)
        GridPuesto.Visible = False
        panelPuesto.Visible = True
        Dim conn As New SqlConnection(Session("conexion"))

        Try
            Dim cmd As New SqlCommand("EXEC pPuestos @cmd=2, @puestoid='" & id.ToString & "'", conn)
            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                txtCodigo.Text = rs("id")
                txtPuesto.Text = rs("nombre")
                Session("idpuesto") = rs("id")
            End If

            rs.Close()



        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub btnSavePuesto_Click(sender As Object, e As EventArgs) Handles btnSavePuesto.Click
        Try

            If Session("idpuesto") = Nothing Then
                Dim ObjData As New DataControl(1)
                ObjData.RunSQLScalarQuery("EXEC pPuestos @cmd=4, @puestoid='" & txtCodigo.Text.ToString & "', @nombre='" & txtPuesto.Text & "'")
                ObjData = Nothing
            Else
                Dim ObjData As New DataControl(1)
                ObjData.RunSQLScalarQuery("EXEC pPuestos @cmd=5, @puestoid='" & txtCodigo.Text.ToString & "', @nombre='" & txtPuesto.Text & "'")
                ObjData = Nothing
            End If


            'employeeslist.MasterTableView.NoMasterRecordsText = "No hay registros para mostrar."
            'employeeslist.DataSource = GetEmployees()
            'employeeslist.DataBind()


            lblMensajepuesto.ForeColor = Drawing.Color.Green
            lblMensajepuesto.Text = "Datos guardados exitosamente."
        Catch ex As Exception
            lblMensajepuesto.ForeColor = Drawing.Color.Red
            lblMensajepuesto.Text = "Error inesperado: " & ex.Message.ToString
        End Try
        CargarGrid()
        panelPuesto.Visible = False
        GridPuesto.Visible = True
        btnAgregar.Visible = True
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        GridPuesto.Visible = False
        panelPuesto.Visible = True
        btnAgregar.Visible = False
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        CargarGrid()
    End Sub
End Class