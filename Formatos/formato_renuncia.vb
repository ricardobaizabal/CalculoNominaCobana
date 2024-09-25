Imports Telerik.Reporting.Drawing
Imports System.Data.SqlClient

Partial Public Class formato_renuncia
    Inherits Telerik.Reporting.Report

    Public Sub New()
        InitializeComponent()
        detail.Height = Unit.Inch(0)
    End Sub

    Private Sub formato_cfdi_NeedDataSource(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.NeedDataSource

    End Sub

End Class