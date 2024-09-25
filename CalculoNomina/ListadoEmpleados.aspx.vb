Imports Telerik.Web.UI
Public Class ListadoEmpleados
    Inherits System.Web.UI.Page
    Dim ObjData As New DataControl()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub
    Private Sub GridEmployees_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridEmployees.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                IncidenciasWindow.VisibleOnPageLoad = True
                registroId.Value = e.CommandArgument
                periodoId.Value = Request.QueryString("id").ToString
                Dim IdEjercicio As Integer

                Dim cPeriodo As New Entities.Periodo
                cPeriodo.IdPeriodo = periodoId.Value
                cPeriodo.ConsultarPeriodoID()

                If cPeriodo.IdPeriodo > 0 Then
                    txtDia.Text = cPeriodo.Dias
                    calFechaInt.SelectedDate = cPeriodo.Fechainicial
                    calFechaFin.SelectedDate = cPeriodo.Fechafinal
                End If
                cPeriodo = Nothing

                Dim cEmpresa As New Entities.Empresa
                cEmpresa.ConsultarEjercicioID()
                If cEmpresa.IdEjercicio > 0 Then
                    IdEjercicio = cEmpresa.IdEjercicio
                    txtEjercicio.Text = cEmpresa.IdEjercicio
                Else
                    IdEjercicio = Date.Now.Year
                    txtEjercicio.Text = IdEjercicio
                End If
                cEmpresa = Nothing

                Dim cEmpleado As New Entities.Empleado
                cEmpleado.IdEmpleado = registroId.Value
                cEmpleado.ConsultarEmpleadoID()
                If cEmpleado.IdEmpleado > 0 Then

                    registroId.Value = cEmpleado.IdEmpleado
                    txtNumPeriodo.Text = periodoId.Value
                    txtNumEmpleado.Text = registroId.Value
                    txtRrc.Text = cEmpleado.Rfc
                    txtNombre.Text = cEmpleado.Nombre
                    txtImssNom.Text = cEmpleado.Imss
                    txtRegimencontrat.Text = cEmpleado.RegimenContratacion
                    calFechaIngreso.SelectedDate = cEmpleado.FechaIngreso

                    txtPuesto.Text = cEmpleado.Puesto

                End If
                llenaCombocmbConcepto(0)
                cEmpleado = Nothing

                'Response.Redirect("~/ModificacionNominaSemanalNormal.aspx?p=" + Request.QueryString("id").ToString + "&emp=" + e.CommandArgument)
        End Select
    End Sub
    Private Sub llenaCombocmbConcepto(ByVal sel As Integer)
        Dim cConcepto As New Entities.Concepto
        objData.Catalogo(cmbConcepto, sel, cConcepto.ConsultarConcepto)
        cConcepto = Nothing
    End Sub
    Private Sub GridEmployees_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles GridEmployees.ItemDataBound

        'If TypeOf e.Item Is GridDataItem Then

        '    Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

        '    If e.Item.OwnerTableView.Name = "Employees" Then
        '        Dim lnkEdit As LinkButton = CType(dataItem("ver").FindControl("lnkEdit"), LinkButton)
        '        lnkEdit.Attributes.Add("onClick", "javascript:openRadWindow(" & e.Item.DataItem("id").ToString() & "); return false;")
        '        lnkEdit.CausesValidation = False

        '    End If

        'End If
    End Sub
    Private Sub GridEmployees_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridEmployees.NeedDataSource
        Dim cEmpleado As New Entities.Empleado
        If Session("clienteid") = "" Then
            cEmpleado.IdEmpleado = 0
        Else
            cEmpleado.IdEmpleado = Session("clienteid")
        End If
        GridEmployees.DataSource = cEmpleado.ConsultarEmpleado()
        cEmpleado = Nothing
    End Sub
    Private Sub GridPercepciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridPercepciones.NeedDataSource
        Dim cPuesto As New Entities.Puesto
        GridPercepciones.DataSource = cPuesto.ConsultarPuesto()
        cPuesto = Nothing
    End Sub
    Private Sub GridDeduciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridDeduciones.NeedDataSource
        Dim cDepartamento As New Entities.Departamento
        GridDeduciones.DataSource = cDepartamento.ConsultarDepartamento
        cDepartamento = Nothing
    End Sub
End Class