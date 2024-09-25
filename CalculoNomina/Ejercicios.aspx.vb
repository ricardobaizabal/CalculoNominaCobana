Imports Telerik.Web.UI

Public Class Ejercicios
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            llenaComboEjercicio(0)
        End If
    End Sub
    Private Sub GridEjercicio_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridEjercicio.NeedDataSource
        Dim cEjercicio As New Entities.Ejercicio
        GridEjercicio.DataSource = cEjercicio.ConsultarEjercicios
        cEjercicio = Nothing
    End Sub
    Private Sub CargarGrid()
        Dim cEjercicio As New Entities.Ejercicio
        GridEjercicio.DataSource = cEjercicio.ConsultarEjercicios()
        GridEjercicio.DataBind()
        cEjercicio = Nothing
    End Sub
    Private Sub llenaComboEjercicio(ByVal sel As Integer)
        'Dim cEjercicio As New Entities.Ejercicio
        'objData.Catalogo(cmbEjercicio, sel, cEjercicio.ConsultarEjercicio())
        'cEjercicio = Nothing
    End Sub
    Private Sub GridEjercicio_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridEjercicio.ItemCommand
        Dim cEjercicio As New Entities.Ejercicio
        cEjercicio.IdEjercicio = e.CommandArgument
        cEjercicio.ConsultarEjercicioID()
        If cEjercicio.IdEjercicio > 0 Then
            registroId.Value = cEjercicio.IdEjercicio
            txtEjercicio.Text = cEjercicio.Descripcion
        End If
    End Sub
    Private Sub GridEjercicio_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridEjercicio.ItemDataBound
        Select Case e.Item.ItemType
            Case GridItemType.Item, GridItemType.AlternatingItem
                Dim seleccionado As ImageButton = CType(e.Item.FindControl("seleccionado"), ImageButton)
                seleccionado.Visible = e.Item.DataItem("activo")
        End Select
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim cEjercicio As New Entities.Ejercicio
        cEjercicio.IdEjercicio = txtEjercicio.Text.Trim
        cEjercicio.ConsultarEjercicioID()

        If cEjercicio.IdEjercicio > 0 Then
            RadWindowManager1.RadAlert("Este ejercicio ya existe", 330, 180, "Alerta", "", "")
        Else
            If registroId.Value > 0 Then
                cEjercicio.IdEjercicio = registroId.Value
                cEjercicio.Descripcion = txtEjercicio.Text.Trim
                cEjercicio.UpdateEjercicio()
            Else
                cEjercicio.Descripcion = txtEjercicio.Text.Trim
                cEjercicio.GuadarEjercicio()
            End If
            cEjercicio = Nothing
            resetControles()
            CargarGrid()
        End If

    End Sub
    Private Sub resetControles()
        txtEjercicio.Text = ""
        registroId.Value = 0
    End Sub
    Protected Sub ToggleRowSelection(ByVal sender As Object, ByVal e As EventArgs)
        Dim chk As CheckBox = DirectCast(sender, CheckBox)
        Dim itm As GridDataItem = DirectCast(chk.NamingContainer, GridDataItem)
        Dim crrntindex As Integer = itm.ItemIndex
        If chk.Checked = True Then
            For Each row As GridDataItem In GridEjercicio.MasterTableView.Items
                Dim chk1 As CheckBox = DirectCast(row.FindControl("itemcheckbox"), CheckBox)
                Dim index As Integer = row.ItemIndex
                If crrntindex = index Then
                    Continue For
                End If
                If chk1.Checked = True Then
                    chk1.Checked = False
                End If
                'Dim IdEjercicio As String = row.GetDataKeyValue("id").ToString
            Next
        End If
    End Sub
    Private Sub btnModificarEjercicio_Click(sender As Object, e As EventArgs) Handles btnModificarEjercicio.Click
        Dim cEjercicio As New Entities.Ejercicio
        For Each dataItem As Telerik.Web.UI.GridDataItem In GridEjercicio.MasterTableView.Items
            Dim IdEjercicio As String = dataItem.GetDataKeyValue("id").ToString
            If TryCast(dataItem.FindControl("itemcheckbox"), CheckBox).Checked Then
                cEjercicio.Descripcion = IdEjercicio.ToString
                cEjercicio.IdUsuario = Session("usuarioid")
                cEjercicio.ModificarEjercicioActivado()
                Session("clienteid") = Nothing
                Session("Cliente") = ""
                Response.Redirect("~/Home.aspx")
            End If
        Next
        CargarGrid()
    End Sub

End Class