Public Class EditorPeriodosCatorcenales
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request("id") Is Nothing Then
                registroId.Value = 0
            Else
                registroId.Value = Request.QueryString("id").ToString
                Dim cPeriodo As New Entities.Periodo
                cPeriodo.IdPeriodo = registroId.Value
                cPeriodo.ConsultarPeriodoID()
                If cPeriodo.IdPeriodo > 0 Then
                    lblNoPeriodo.Text = cPeriodo.NoPeriodo
                    lblCliente.Text = cPeriodo.Cliente
                    registroId.Value = cPeriodo.IdPeriodo
                    calFechaInicio.SelectedDate = cPeriodo.FechaInicial
                    calFechaFin.SelectedDate = cPeriodo.FechaFinal
                    If cPeriodo.FechaPago.ToString.Length > 0 Then
                        calfechaPago.SelectedDate = cPeriodo.FechaPago
                    End If
                    chkInicioMesBit.Checked = cPeriodo.InicioMesBit
                    chkFinMesBit.Checked = cPeriodo.FinMesBit
                    chkInicioEjercicioBit.Checked = cPeriodo.InicioEjercicioBit
                    chkFinEjercicioBit.Checked = cPeriodo.FinEjercicioBit
                    calfechaPago.Focus()
                End If
            End If
        End If
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If Page.IsValid Then
            Dim cPeriodo As New Entities.Periodo
            If registroId.Value > 0 Then
                cPeriodo.IdPeriodo = registroId.Value
                cPeriodo.NoPeriodo = lblNoPeriodo.Text
                cPeriodo.FechaInicial = String.Format("{0:MM/dd/yyyy}", calFechaInicio.SelectedDate)
                cPeriodo.FechaFinal = String.Format("{0:MM/dd/yyyy}", calFechaFin.SelectedDate)
                If Not calfechaPago.SelectedDate Is Nothing Then
                    cPeriodo.FechaPago = String.Format("{0:MM/dd/yyyy}", calfechaPago.SelectedDate)
                End If
                cPeriodo.InicioMesBit = chkInicioMesBit.Checked
                cPeriodo.FinMesBit = chkFinMesBit.Checked
                cPeriodo.InicioEjercicioBit = chkInicioEjercicioBit.Checked
                cPeriodo.FinEjercicioBit = chkFinEjercicioBit.Checked
                cPeriodo.UpdatePeriodoCatorcenal()
            End If
            cPeriodo = Nothing
            Response.Redirect("~/PeriodosCatorcenales.aspx")
        End If
    End Sub
    Private Sub resetControles()
        calFechaInicio.Clear()
        calFechaFin.Clear()
        calfechaPago.Clear()
        chkInicioMesBit.Checked = False
        chkFinMesBit.Checked = False
        chkInicioEjercicioBit.Checked = False
        chkFinEjercicioBit.Checked = False
        registroId.Value = 0
    End Sub
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Response.Redirect("PeriodosCatorcenales.aspx")
    End Sub

End Class