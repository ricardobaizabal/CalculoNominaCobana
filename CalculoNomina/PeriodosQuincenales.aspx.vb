Imports Entities
Imports Telerik.Web.UI

Public Class PeriodosQuincenales
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
        'cConfiguracion.IdEmpresa = Session("clienteid")
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
        Response.Redirect("~/AgregarPeriodosQuincenales.aspx")
    End Sub
    Private Sub GridPeriodosQuincenales_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridPeriodosQuincenales.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/EditorPeriodosQuincenales.aspx?id=" & e.CommandArgument)
            Case "cmdDelete"
                registroId.Value = e.CommandArgument
                rwConfirmEliminaPeriodo.RadConfirm("¿Está seguro de eliminar el periodo quincenal de la base de datos?", "confirmCallbackFn", 330, 180, Nothing, "Confirmar")
        End Select
    End Sub
    Private Sub GridPeriodosQuincenales_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridPeriodosQuincenales.ItemDataBound

    End Sub
    Private Sub GridPeriodosQuincenales_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridPeriodosQuincenales.NeedDataSource
        Dim cPeriodo As New Entities.Periodo
        'cPeriodo.IdEmpresa = Session("clienteid")
        cPeriodo.IdTipoNomina = 3 'Quincenal
        cPeriodo.IdEjercicio = ejercicioId.Value
        GridPeriodosQuincenales.DataSource = cPeriodo.ConsultarPeriodo
        cPeriodo = Nothing
    End Sub
    Private Sub HiddenButton_Click(sender As Object, e As EventArgs) Handles HiddenButton.Click
        Dim cPeriodo As New Entities.Periodo
        cPeriodo.IdPeriodo = registroId.Value
        cPeriodo.EliminaPeriodo()

        cPeriodo = New Entities.Periodo
        'cPeriodo.IdEmpresa = Session("clienteid")
        cPeriodo.IdTipoNomina = 3 'Quincenal
        cPeriodo.IdEjercicio = ejercicioId.Value
        GridPeriodosQuincenales.DataSource = cPeriodo.ConsultarPeriodo()
        GridPeriodosQuincenales.DataBind()
        cPeriodo = Nothing
    End Sub

End Class