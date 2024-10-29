Imports Entities
Imports Telerik.Web.UI

Public Class PeriodosSemanales
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Call CargarVariablesGenerales()
            registroId.Value = 0
        End If
    End Sub
    Private Sub CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cConfiguracion = New Configuracion()
        cConfiguracion.IdEmpresa = Session("IdEmpresa")
        cConfiguracion.IdUsuario = Session("usuarioid")
        dt = cConfiguracion.ConsultarConfiguracion()
        cConfiguracion = Nothing

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                ejercicioId.Value = oDataRow("IdEjercicio")
            Next
        End If
    End Sub
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Response.Redirect("~/AgregarPeriodosSemanales.aspx")
    End Sub
    Private Sub GridPeriodosSemanales_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridPeriodosSemanales.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/EditorPeriodosSemanales.aspx?id=" & e.CommandArgument)
            Case "cmdDelete"
                registroId.Value = e.CommandArgument
                rwConfirmEliminaPeriodo.RadConfirm("¿Está seguro de eliminar el periodo semanal de la base de datos?", "confirmCallbackFn", 330, 180, Nothing, "Confirmar")
        End Select
    End Sub
    Private Sub GridPeriodosSemanales_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridPeriodosSemanales.ItemDataBound

    End Sub
    Private Sub GridPeriodosSemanales_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridPeriodosSemanales.NeedDataSource
        Dim cPeriodo As New Entities.Periodo
        cPeriodo.IdEmpresa = Session("IdEmpresa")
        cPeriodo.IdTipoNomina = 1 'Semanal
        cPeriodo.IdEjercicio = ejercicioId.Value
        GridPeriodosSemanales.DataSource = cPeriodo.ConsultarPeriodo
        cPeriodo = Nothing
    End Sub
    Private Sub HiddenButton_Click(sender As Object, e As EventArgs) Handles HiddenButton.Click
        Dim cPeriodo As New Entities.Periodo
        cPeriodo.IdPeriodo = registroId.Value
        cPeriodo.EliminaPeriodo()

        Call CargarVariablesGenerales()

        cPeriodo = New Entities.Periodo
        cPeriodo.IdEmpresa = Session("IdEmpresa")
        cPeriodo.IdTipoNomina = 1 'Semanal
        cPeriodo.IdEjercicio = ejercicioId.Value
        GridPeriodosSemanales.DataSource = cPeriodo.ConsultarPeriodo()
        GridPeriodosSemanales.DataBind()
        cPeriodo = Nothing

    End Sub

End Class