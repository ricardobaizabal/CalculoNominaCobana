Imports Telerik.Web.UI
Imports System.Data.SqlClient

Public Class Deducciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarClavesSat()
        End If
    End Sub

    Private Sub CargarGrid()
        Dim cDeducciones As New Entities.Deducciones
        GridDeducciones.DataSource = cDeducciones.ConsultarDeducciones
        GridDeducciones.DataBind()
        cDeducciones = Nothing
    End Sub

    Private Sub GridDeducciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridDeducciones.NeedDataSource
        Dim cDeducciones As New Entities.Deducciones

        GridDeducciones.DataSource = cDeducciones.ConsultarDeducciones
        cDeducciones = Nothing
    End Sub

    Private Sub GridDeducciones_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridDeducciones.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaDeducciones(e.CommandArgument.ToString)
            Case "cmdDelete"
                Dim cDeducciones As New Entities.Deducciones
                cDeducciones.EliminarDeducciones(e.CommandArgument.ToString)
                CargarGrid()
        End Select
    End Sub

    Private Sub EditaDeducciones(ByVal id As Integer)
        GridDeducciones.Visible = False
        panelDeducciones.Visible = True
        Try
            Dim cPercepciones As New Entities.Percepciones
            Dim dt As ArrayList
            dt = cPercepciones.ConsultarPercepcionID(id)
            If dt.Count > 0 Then
                Session("iddeduccion") = dt.Item(0)
                txtConcepto.Text = dt.Item(1)
                txtClave.Text = dt.Item(2)
                OptClaveSAT.SelectedValue = dt.Item(5)
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub CargarClavesSat()
        Dim conn As New SqlConnection(Session("conexion"))
        Try
            Dim cmd As New SqlCommand("SELECT id, ClaveSAT, Descripcion FROM tblClaveSAT WHERE Tipo='D'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()
            OptClaveSAT.Items.Clear()

            While rs.Read()
                Dim listItem As New ListItem()
                listItem.Text = rs("Descripcion").ToString()
                listItem.Value = rs("id").ToString()
                OptClaveSAT.Items.Add(listItem)
            End While

            rs.Close()
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub btnSaveDeduccion_Click(sender As Object, e As EventArgs) Handles btnSaveDeduccion.Click

        lblMensajeDeduccion.Text = ""
        txtConcepto.Text = ""
        txtClave.Text = ""

        Try
            If Session("iddeduccion") = Nothing Then
                Dim cDeducciones As New Entities.Deducciones
                cDeducciones.GuadarDeducciones(txtConcepto.Text.ToString, txtClave.Text.ToString, "D", OptClaveSAT.SelectedValue, OptClaveSAT.SelectedItem.Text)
            Else
                Dim cDeducciones As New Entities.Deducciones
                cDeducciones.UpdateDeducciones(Session("iddeduccion"), txtConcepto.Text.ToString, txtClave.Text.ToString, "D", OptClaveSAT.SelectedValue, OptClaveSAT.SelectedItem.Text)
                Session("iddeduccion") = Nothing
            End If

            lblMensajeDeduccion.ForeColor = Drawing.Color.Green
            lblMensajeDeduccion.Text = "Datos guardados exitosamente."

            CargarGrid()
            panelDeducciones.Visible = False
            GridDeducciones.Visible = True
            btnAgregar.Visible = True
        Catch ex As Exception
            lblMensajeDeduccion.ForeColor = Drawing.Color.Red
            lblMensajeDeduccion.Text = "Error inesperado: " & ex.Message.ToString
        End Try
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        GridDeducciones.Visible = False
        panelDeducciones.Visible = True
        btnAgregar.Visible = False
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        CargarGrid()
        panelDeducciones.Visible = False
        GridDeducciones.Visible = True
        btnAgregar.Visible = True
    End Sub

End Class