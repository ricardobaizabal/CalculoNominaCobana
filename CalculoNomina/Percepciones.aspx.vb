Imports Telerik.Web.UI
Imports System.Data.SqlClient

Public Class Percepciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarClavesSat("P")
        End If
    End Sub

    Private Sub CargarGrid()
        Dim cPercepciones As New Entities.Percepciones
        GridPercepciones.DataSource = cPercepciones.ConsultarPercepciones
        GridPercepciones.DataBind()
        cPercepciones = Nothing
    End Sub

    Private Sub GridPercepciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridPercepciones.NeedDataSource
        Dim cPercepciones As New Entities.Percepciones

        GridPercepciones.DataSource = cPercepciones.ConsultarPercepciones
        cPercepciones = Nothing
    End Sub

    Private Sub CargarClavesSat(Type As String)
        Dim conn As New SqlConnection(Session("conexion"))
        Try
            Dim cmd As New SqlCommand("SELECT id, ClaveSAT, Descripcion FROM tblClaveSAT WHERE Tipo='" & Type & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()
            OptClaveSAT.Items.Clear()
            Dim MaxString As Integer = 40

            While rs.Read()
                Dim listItem As New ListItem()
                If rs("Descripcion").ToString().Length > MaxString Then
                    listItem.Text = rs("Descripcion").ToString.Substring(0, MaxString) + "..."
                Else
                    listItem.Text = rs("Descripcion").ToString()
                End If
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

    Protected Sub checkExcedente_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles checkExcedente.CheckedChanged
        If checkExcedente.Checked Then
            CargarClavesSat("O")
        Else
            CargarClavesSat("P")
        End If
    End Sub

    Private Sub GridPercepciones_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridPercepciones.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaPercepciones(e.CommandArgument.ToString)
            Case "cmdDelete"
                Dim cPercepciones As New Entities.Percepciones
                cPercepciones.EliminarPercepciones(e.CommandArgument.ToString)
                CargarGrid()
        End Select
    End Sub

    Private Sub EditaPercepciones(ByVal id As Integer)
        GridPercepciones.Visible = False
        panelPerpeciones.Visible = True
        Try
            Dim cPercepciones As New Entities.Percepciones
            Dim dt As ArrayList
            dt = cPercepciones.ConsultarPercepcionID(id)
            If dt.Count > 0 Then
                Session("idpercepcion") = dt.Item(0)
                txtConcepto.Text = dt.Item(1)
                txtClave.Text = dt.Item(2)

                If dt.Item(6).ToString = "O" Then
                    checkExcedente.Checked = True
                    CargarClavesSat("O")
                Else
                    checkExcedente.Checked = False
                    CargarClavesSat("P")
                End If

                OptClaveSAT.SelectedValue = dt.Item(5)
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnSavePercepcion_Click(sender As Object, e As EventArgs) Handles btnSavePercepcion.Click
        lblMensajePercepcion.Text = ""
        txtConcepto.Text = ""
        txtClave.Text = ""

        Try
            If Session("idpercepcion") = Nothing Then
                Dim cPercepciones As New Entities.Percepciones
                cPercepciones.GuadarPercepcion(txtConcepto.Text.ToString, txtClave.Text.ToString, "P", OptClaveSAT.SelectedValue, OptClaveSAT.SelectedItem.Text)
            Else
                Dim cPercepciones As New Entities.Percepciones
                cPercepciones.UpdatePercepcion(Session("idpercepcion"), txtConcepto.Text.ToString, txtClave.Text.ToString, "P", OptClaveSAT.SelectedValue, OptClaveSAT.SelectedItem.Text)
                Session("idpercepcion") = Nothing
            End If

            lblMensajePercepcion.ForeColor = Drawing.Color.Green
            lblMensajePercepcion.Text = "Datos guardados exitosamente."
            CargarGrid()
            panelPerpeciones.Visible = False
            GridPercepciones.Visible = True
            btnAgregar.Visible = True
        Catch ex As Exception
            lblMensajePercepcion.ForeColor = Drawing.Color.Red
            lblMensajePercepcion.Text = "Error inesperado: " & ex.Message.ToString
        End Try
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        GridPercepciones.Visible = False
        panelPerpeciones.Visible = True
        btnAgregar.Visible = False
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        CargarGrid()
        panelPerpeciones.Visible = False
        GridPercepciones.Visible = True
        btnAgregar.Visible = True
    End Sub

End Class