Public Class Salir
    Inherits System.Web.UI.Page

    Private Sub form1_Init(sender As Object, e As EventArgs) Handles form1.Init
        Session.Abandon()
        Response.Redirect("~/Login.aspx")
    End Sub

End Class