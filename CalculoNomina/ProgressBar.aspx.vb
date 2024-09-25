Imports System
Imports Telerik.Web.UI
Imports Telerik.Web.UI.Upload
Public Class ProgressBar
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'Do not display SelectedFilesCount progress indicator.
            'RadProgressArea1.ProgressIndicators = RadProgressArea1.ProgressIndicators And Not ProgressIndicators.SelectedFiles
        End If
        RadProgressArea1.Localization.TotalFiles = "Total empleados"
        RadProgressArea1.Localization.UploadedFiles = "Calculados"
        RadProgressArea1.Localization.CurrentFileName = "Calculando: "
    End Sub

    Protected Sub buttonSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        UpdateProgressContext()
    End Sub

    Private Sub UpdateProgressContext()
        Const Total As Integer = 200

        Dim progress As RadProgressContext = RadProgressContext.Current
        'progress.Speed = "N/A"

        For i As Integer = 0 To Total - 1

            progress.SecondaryTotal = Total
            progress.SecondaryValue = i
            progress.SecondaryPercent = i * 100 / Total

            progress.CurrentOperationText = "Empleado " & i.ToString()

            If Not Response.IsClientConnected Then
                'Cancel button was clicked or the browser was closed, so stop processing
                Exit For
            End If

            progress.TimeEstimated = (Total - i) * 100
            'Stall the current thread for 0.1 seconds
            System.Threading.Thread.Sleep(100)
        Next
    End Sub

End Class