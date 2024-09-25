Imports Telerik.Web.UI

Public Class TablaISPTMensual
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Response.Redirect("~/EditorTablaISPTMensual.aspx")
    End Sub
    Private Sub GridTarifaMensual_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridTarifaMensual.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/EditorTablaISPTMensual.aspx?id=" & e.CommandArgument)
        End Select
    End Sub

    Private Sub GridTarifaMensual_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridTarifaMensual.NeedDataSource
        Dim cTarifaMensual As New Entities.TarifaMensual
        GridTarifaMensual.DataSource = cTarifaMensual.ConsultarTarifaMensual
        cTarifaMensual = Nothing
    End Sub

End Class