Imports Entities
Imports Telerik.Web.UI

Public Class PeriodosCatorcenales
    Inherits System.Web.UI.Page
    'Private IdEmpresa As Integer = 0
    Private IdEjercicio As Integer = 0
    Private Periodo As Integer = 0

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
                IdEjercicio = oDataRow("IdEjercicio")
            Next
        End If
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Response.Redirect("~/AgregarPeriodosCatorcenales.aspx")
    End Sub
    Private Sub GridPeriodosCatorcenales_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridPeriodosCatorcenales.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/EditorPeriodosCatorcenales.aspx?id=" & e.CommandArgument)
            Case "cmdDelete"
                registroId.Value = e.CommandArgument
                rwConfirmEliminaPeriodo.RadConfirm("¿Está seguro de eliminar el periodo catorcenal de la base de datos?", "confirmCallbackFn", 330, 180, Nothing, "Confirmar")
        End Select
    End Sub
    Private Sub GridPeriodosCatorcenales_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridPeriodosCatorcenales.ItemDataBound

    End Sub
    Private Sub GridPeriodosCatorcenales_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridPeriodosCatorcenales.NeedDataSource
        Dim cPeriodo As New Entities.Periodo
        'cPeriodo.IdEmpresa = Session("clienteid")
        cPeriodo.IdTipoNomina = 2 'Catorcenal
        cPeriodo.IdEjercicio = IdEjercicio
        GridPeriodosCatorcenales.DataSource = cPeriodo.ConsultarPeriodo
        cPeriodo = Nothing
    End Sub
    Private Sub HiddenButton_Click(sender As Object, e As EventArgs) Handles HiddenButton.Click
        Dim cPeriodo As New Entities.Periodo
        cPeriodo.IdPeriodo = registroId.Value
        cPeriodo.EliminaPeriodo()

        cPeriodo = New Entities.Periodo
        'cPeriodo.IdEmpresa = Session("clienteid")
        cPeriodo.IdTipoNomina = 2 'catorcenal

        GridPeriodosCatorcenales.DataSource = cPeriodo.ConsultarPeriodo()
        GridPeriodosCatorcenales.DataBind()
        cPeriodo = Nothing
    End Sub

End Class