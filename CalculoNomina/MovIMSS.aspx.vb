Imports Telerik.Web.UI
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail
Imports System.Threading
Imports System.Globalization
Imports Telerik.Charting
Public Class MovIMSS
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            calFechaMovimiento.MinDate = Date.Now.AddYears(-100)
            cboMovimientoimss.SelectedValue = 2
            'txtSalario.Attributes.Add("OnKeyPress", "return SoloNumerosDec(event)")
        End If
    End Sub
    Private Sub btnGuadar_Click(sender As Object, e As EventArgs) Handles btnGuadar.Click
        If Page.IsValid Then

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")

            'Dim FechaNacimiento As DateTime
            Dim MovimientoId As String = ""
            'FechaNacimiento = calFechaNacimiento.SelectedDate  
            Dim objData As New DataControl(1)

            MovimientoId = objData.RunSQLScalarQueryString("EXEC pMovimientosIMSS @cmd=1, @IdContrato='" & Request("id").ToString & "',@IdTipoMov='" & cboMovimientoimss.SelectedValue & "', @IdUsuario='" & Session("userid") & "', @fecha='" & calFechaMovimiento.SelectedDate & "', @salario='" & txtSalario.Text & "',@salarioSDI='" & txtSalarioDiarioIntegrado.Text & "', @comentario='" & txtComentario.Text & "' ")

            If MovimientoId = "Ya existe un registro de alta" Then
                txtSalario.Text = 0
                txtComentario.Text = ""
                calFechaMovimiento.Clear()
                cboMovimientoimss.SelectedValue = 0

                objData = Nothing

                RadWindowManager2.RadAlert("Ya existe un registro de alta.", 330, 180, "Alert", "", "")

            ElseIf MovimientoId = "No puede solicitar este movimiento ya que no existe un alta previa" Then

                txtSalario.Text = 0
                txtComentario.Text = ""
                calFechaMovimiento.Clear()
                cboMovimientoimss.SelectedValue = 0

                objData = Nothing

                RadWindowManager2.RadAlert("No puede solicitar este movimiento ya que no existe un alta previa.", 330, 180, "Alert", "", "")
            ElseIf MovimientoId = "Ya existe un registro de baja" Then
                RadWindowManager2.RadAlert(MovimientoId, 330, 180, "Alert", "", "")

                txtSalario.Text = 0
                txtComentario.Text = ""
                calFechaMovimiento.Clear()
                cboMovimientoimss.SelectedValue = 0

                movimientoList.MasterTableView.NoMasterRecordsText = "No se encontraron registros."
                movimientoList.DataSource = ObtenerMovimientos()
                movimientoList.DataBind()
            Else
                RadWindowManager2.RadAlert("Solicitud aplicada con éxito.", 330, 180, "Alert", "", "")

                txtSalario.Text = 0
                txtComentario.Text = ""
                calFechaMovimiento.Clear()
                cboMovimientoimss.SelectedValue = 0

                movimientoList.MasterTableView.NoMasterRecordsText = "No se encontraron registros."
                movimientoList.DataSource = ObtenerMovimientos()
                movimientoList.DataBind()
            End If

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
            Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")

        End If
    End Sub
    Private Sub movimientoList_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles movimientoList.ItemCommand
        Select Case e.CommandName
            Case "cmdDownload"
                Call DescargaAnexo(e.CommandArgument)
        End Select
    End Sub
    Private Sub DescargaAnexo(ByVal movimientoid As Long)
        '
        '
        Dim nombreanexo As String = ""
        Dim conn As New SqlConnection(Session("conexion"))
        conn.Open()
        Dim SqlCommand As SqlCommand = New SqlCommand("exec pMovimientosIMSS @cmd=9, @Id='" & movimientoid.ToString & "'", conn)
        Dim rs = SqlCommand.ExecuteReader
        If rs.Read Then
            nombreanexo = rs("anexo")
        End If
        conn.Close()
        conn.Dispose()
        conn = Nothing
        '
        Dim path As String = Server.MapPath("~/rh/anexo/" & nombreanexo) 'get file object as FileInfo
        Dim file As System.IO.FileInfo = New System.IO.FileInfo(path)
        '
        '
        If file.Exists Then 'set appropriate headers
            Response.Clear()
            Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
            Response.AddHeader("Content-Length", file.Length.ToString())
            Response.ContentType = "application/octet-stream"
            Response.WriteFile(file.FullName)
            Response.End() 'if file does not exist
        End If
    End Sub
    Private Sub movimientoList_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles movimientoList.NeedDataSource
        movimientoList.MasterTableView.NoMasterRecordsText = "No se encontraron prospectos para mostrar"
        movimientoList.DataSource = ObtenerMovimientos()
    End Sub
    Function ObtenerMovimientos() As DataSet
        Dim user = Session("userid")

        Dim sql As String = ""
        sql = ("EXEC pMovimientosIMSS @cmd=4 , @IdContrato='" & Request("id").ToString & "' ")

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter(sql, conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString())
        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return ds

    End Function
    Private Sub movimientoList_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles movimientoList.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem

                Dim imgDownload As ImageButton = CType(e.Item.FindControl("btnDownload"), ImageButton)
                imgDownload.Visible = e.Item.DataItem("tieneanexo")
        End Select
    End Sub
    Private Sub txtSalario_TextChanged(sender As Object, e As EventArgs) Handles txtSalario.TextChanged
        Dim SDI As Double = 0
        Dim SalarioDiario As Double = 0
        SalarioDiario = txtSalario.Text
        SDI = SalarioDiario * 1.0452
        txtSalarioDiarioIntegrado.Text = SDI.ToString
    End Sub
End Class

