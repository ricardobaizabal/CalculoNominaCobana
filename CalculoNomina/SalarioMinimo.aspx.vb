Imports System.Data
Imports System.Data.SqlClient
Imports Entities
Imports Telerik.Web.UI

Public Class SalarioMinimo
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            grdSalarioMinimo.MasterTableView.NoMasterRecordsText = "No se encontraron registros."
            grdSalarioMinimo.DataSource = GetSalarioMinimo()
            grdSalarioMinimo.DataBind()
        End If
    End Sub
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Panel2.Visible = True
        txtAnio.Text = ""
    End Sub
    Protected Sub ActualizarSalarioMinimo(sender As Object, e As EventArgs)
        Dim txt As New RadNumericTextBox
        txt = TryCast(sender, RadNumericTextBox)

        Dim dgi As Telerik.Web.UI.GridDataItem = DirectCast(txt.Parent.Parent, Telerik.Web.UI.GridDataItem)
        Dim id As String = dgi.GetDataKeyValue("id").ToString
        Dim cSalarioMinimo As New Entities.SalarioMinimo
        cSalarioMinimo.ID = CInt(id)

        Select Case txt.ID
            Case "txtEnero_SM"
                cSalarioMinimo.Enero_SM = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(1)
            Case "txtFebrero_SM"
                cSalarioMinimo.Febrero_SM = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(2)
            Case "txtMarzo_SM"
                cSalarioMinimo.Marzo_SM = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(3)
            Case "txtAbril_SM"
                cSalarioMinimo.Abril_SM = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(4)
            Case "txtMayo_SM"
                cSalarioMinimo.Mayo_SM = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(5)
            Case "txtJunio_SM"
                cSalarioMinimo.Junio_SM = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(6)
            Case "txtJulio_SM"
                cSalarioMinimo.Julio_SM = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(7)
            Case "txtAgosto_SM"
                cSalarioMinimo.Agosto_SM = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(8)
            Case "txtSeptiembre_SM"
                cSalarioMinimo.Septiembre_SM = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(9)
            Case "txtOctubre_SM"
                cSalarioMinimo.Octubre_SM = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(10)
            Case "txtNoviembre_SM"
                cSalarioMinimo.Noviembre_SM = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(11)
            Case "txtDiciembre_SM"
                cSalarioMinimo.Diciembre_SM = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(12)
            Case "txtEnero_SMF"
                cSalarioMinimo.Enero_SMF = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(13)
            Case "txtFebrero_SMF"
                cSalarioMinimo.Febrero_SMF = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(14)
            Case "txtMarzo_SMF"
                cSalarioMinimo.Marzo_SMF = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(15)
            Case "txtAbril_SMF"
                cSalarioMinimo.Abril_SMF = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(16)
            Case "txtMayo_SMF"
                cSalarioMinimo.Mayo_SMF = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(17)
            Case "txtJunio_SMF"
                cSalarioMinimo.Junio_SMF = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(18)
            Case "txtJulio_SMF"
                cSalarioMinimo.Julio_SMF = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(19)
            Case "txtAgosto_SMF"
                cSalarioMinimo.Agosto_SMF = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(20)
            Case "txtSeptiembre_SMF"
                cSalarioMinimo.Septiembre_SMF = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(21)
            Case "txtOctubre_SMF"
                cSalarioMinimo.Octubre_SMF = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(22)
            Case "txtNoviembre_SMF"
                cSalarioMinimo.Noviembre_SMF = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(23)
            Case "txtDiciembre_SMF"
                cSalarioMinimo.Diciembre_SMF = IIf(txt.Text = String.Empty, 0, txt.Text)
                Call cSalarioMinimo.ActualizarSalarioMinimo(24)
        End Select
    End Sub
    Private Function GetSalarioMinimo() As DataSet
        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)
        Dim cmd As New SqlDataAdapter("SELECT id, isnull(anio,0) as anio, isnull(enero_sm,0) as enero_sm, isnull(enero_smf,0) as enero_smf, isnull(febrero_sm,0) as febrero_sm, isnull(febrero_smf,0) as febrero_smf, isnull(marzo_sm,0) as marzo_sm, isnull(marzo_smf,0) as marzo_smf, isnull(abril_sm,0) as abril_sm, isnull(abril_smf,0) as abril_smf, isnull(mayo_sm,0) as mayo_sm,isnull(mayo_smf,0) as mayo_smf, isnull(junio_sm,0) as junio_sm, isnull(junio_smf,0) as junio_smf, isnull(julio_sm,0) as julio_sm, isnull(julio_smf,0) as julio_smf, isnull(agosto_sm,0) as agosto_sm, isnull(agosto_smf,0) as agosto_smf, isnull(septiembre_sm,0) as septiembre_sm, isnull(septiembre_smf,0) as septiembre_smf, isnull(octubre_sm,0) as octubre_sm, isnull(octubre_smf,0) as octubre_smf, isnull(noviembre_sm,0) as noviembre_sm, isnull(noviembre_smf,0) as noviembre_smf, isnull(diciembre_sm,0) as diciembre_sm, isnull(diciembre_smf,0) as diciembre_smf FROM tblSalarioMinimo where isnull(borradoBit,0)=0  order by anio desc", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return ds
    End Function
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim ds As DataSet = New DataSet
        Dim ObjData As New DataControl(1)
        ds = ObjData.FillDataSet("EXEC pAgregaAnio_SM @cmd=1, @anio='" & txtAnio.Text & "'")

        grdSalarioMinimo.MasterTableView.NoMasterRecordsText = "No se encontraron registros."
        grdSalarioMinimo.DataSource = ds
        grdSalarioMinimo.DataBind()

        Panel2.Visible = False
        txtAnio.Text = ""
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Panel2.Visible = False
        txtAnio.Text = ""
    End Sub
    Private Sub grdSalarioMinimo_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles grdSalarioMinimo.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                Dim ObjData As New DataControl(1)
                ObjData.RunSQLQuery("exec pSalarioMinimo_Delete @id='" & e.CommandArgument.ToString & "'")
                ObjData = Nothing
                grdSalarioMinimo.DataSource = GetSalarioMinimo()
                grdSalarioMinimo.DataBind()
        End Select
    End Sub

End Class