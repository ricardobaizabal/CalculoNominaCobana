Imports System
Imports Telerik.Web.UI
Imports System.Collections
Public Class TablaISPTDiaria
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Response.Redirect("~/EditorTablaISPTDiaria.aspx")
    End Sub
    Private Sub GridTarifaDiaria_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridTarifaDiaria.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                Response.Redirect("~/EditorTablaISPTDiaria.aspx?id=" & e.CommandArgument)
        End Select
    End Sub
    Protected Sub GridTarifaDiaria_ItemUpdated(source As Object, e As Telerik.Web.UI.GridUpdatedEventArgs) Handles GridTarifaDiaria.ItemUpdated
        Dim item As GridEditableItem = DirectCast(e.Item, GridEditableItem)
        Dim id As [String] = item.GetDataKeyValue("IdTarifa").ToString()

        If e.Exception IsNot Nothing Then
            e.KeepInEditMode = True
            e.ExceptionHandled = True
            MsgBox("Product with ID " + id + " cannot be updated. Reason: " + e.Exception.Message)
        End If
    End Sub

End Class