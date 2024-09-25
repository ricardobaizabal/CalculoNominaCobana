Imports System.Data
Imports System.Data.SqlClient
Imports Entities
Imports Telerik.Web.UI
Public Class UMA
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            grdUMA.MasterTableView.NoMasterRecordsText = "No se encontraron registros."
            grdUMA.DataSource = GetUMA()
            grdUMA.DataBind()
        End If
    End Sub
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Panel2.Visible = True
        txtAnio.Text = ""
    End Sub
    Protected Sub ActualizarUMA(sender As Object, e As EventArgs)
        Dim txt As New RadNumericTextBox
        txt = TryCast(sender, RadNumericTextBox)

        Dim dgi As Telerik.Web.UI.GridDataItem = DirectCast(txt.Parent.Parent, Telerik.Web.UI.GridDataItem)
        Dim id As String = dgi.GetDataKeyValue("id").ToString
        Dim cUnidadUMA As New Entities.UMA
        cUnidadUMA.id = CInt(id)

        Select Case txt.ID
            Case "txtEnero"
                cUnidadUMA.Enero = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cUnidadUMA.ActualizarUMA(1)
            Case "txtFebrero"
                cUnidadUMA.Febrero = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cUnidadUMA.ActualizarUMA(2)
            Case "txtMarzo"
                cUnidadUMA.Marzo = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cUnidadUMA.ActualizarUMA(3)
            Case "txtAbril"
                cUnidadUMA.Abril = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cUnidadUMA.ActualizarUMA(4)
            Case "txtMayo"
                cUnidadUMA.Mayo = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cUnidadUMA.ActualizarUMA(5)
            Case "txtJunio"
                cUnidadUMA.Junio = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cUnidadUMA.ActualizarUMA(6)
            Case "txtJulio"
                cUnidadUMA.Julio = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cUnidadUMA.ActualizarUMA(7)
            Case "txtAgosto"
                cUnidadUMA.Agosto = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cUnidadUMA.ActualizarUMA(8)
            Case "txtSeptiembre"
                cUnidadUMA.Septiembre = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cUnidadUMA.ActualizarUMA(9)
            Case "txtOctubre"
                cUnidadUMA.Octubre = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cUnidadUMA.ActualizarUMA(10)
            Case "txtNoviembre"
                cUnidadUMA.Noviembre = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cUnidadUMA.ActualizarUMA(11)
            Case "txtDiciembre"
                cUnidadUMA.Diciembre = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cUnidadUMA.ActualizarUMA(12)
            Case "txtMensual"
                cUnidadUMA.Mensual = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cUnidadUMA.ActualizarUMA(13)
            Case "txtAnual"
                cUnidadUMA.Anual = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cUnidadUMA.ActualizarUMA(14)
        End Select
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim ds As DataSet = New DataSet
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("EXEC pAgregaAnioUMA @cmd=1, @anio='" & txtAnio.Text & "'")

        grdUMA.MasterTableView.NoMasterRecordsText = "No se encontraron registros."
        grdUMA.DataSource = ds
        grdUMA.DataBind()

        Panel2.Visible = False
        txtAnio.Text = ""
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Panel2.Visible = False
        txtAnio.Text = ""
    End Sub
    Private Sub grdUMA_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles grdUMA.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                Dim ObjData As New DataControl(1)
                ObjData.RunSQLQuery("exec pUMA_Delete @id='" & e.CommandArgument.ToString & "'")
                ObjData = Nothing
                grdUMA.DataSource = GetUMA()
                grdUMA.DataBind()
        End Select
    End Sub
    Private Function GetUMA() As DataSet
        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)
        Dim cmd As New SqlDataAdapter("SELECT id, isnull(anio,0) as anio, isnull(enero,0) as enero, isnull(febrero,0) as febrero, isnull(marzo,0) as marzo, isnull(abril,0) as abril, isnull(mayo,0) as mayo, isnull(junio,0) as junio, isnull(julio,0) as julio, isnull(agosto,0) as agosto, isnull(septiembre,0) as septiembre, isnull(octubre,0) as octubre, isnull(noviembre,0) as noviembre, isnull(diciembre,0) as diciembre, isnull(mensual,0) as mensual, isnull(anual,0) as anual FROM tblUMA where isnull(borradoBit,0)=0  order by anio desc", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return ds

    End Function
    Private Sub grdUMA_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles grdUMA.NeedDataSource
        grdUMA.MasterTableView.NoMasterRecordsText = "No se encontraron registros."
        grdUMA.DataSource = GetUMA()
    End Sub

End Class