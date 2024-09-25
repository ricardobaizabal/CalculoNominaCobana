Imports Telerik.Web.UI

Public Class Bancos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub GridBanco_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridBanco.NeedDataSource
        Dim cBanco As New Entities.Banco
        GridBanco.DataSource = cBanco.ConsultarBanco
        cBanco = Nothing
    End Sub
End Class