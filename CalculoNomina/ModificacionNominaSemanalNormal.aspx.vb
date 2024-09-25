Imports Telerik.Web.UI

Public Class ModificacionNominaSemanalNormal
    Inherits System.Web.UI.Page
    Dim ObjData As New DataControl()
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request("emp") Is Nothing Then
                registroId.Value = 0
                llenaCombocmbConcepto(0)
            Else
                registroId.Value = Request.QueryString("emp").ToString
                Dim IdPeriodo = Request.QueryString("p").ToString
                Dim IdEjercicio As Integer

                Dim cPeriodo As New Entities.Periodo
                cPeriodo.IdPeriodo = IdPeriodo.ToString
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
                    txtNumPeriodo.Text = IdPeriodo
                    txtNumEmpleado.Text = registroId.Value
                    txtRrc.Text = cEmpleado.Rfc
                    txtNombre.Text = cEmpleado.Nombre
                    txtImssNom.Text = cEmpleado.Imss
                    txtRegimencontrat.Text = cEmpleado.RegimenContratacion
                    calFechaIngreso.SelectedDate = cEmpleado.FechaIngreso

                    'txtCuotaDiaria1.Text = cEmpleado.CuotaDiaria

                    txtPuesto.Text = cEmpleado.Puesto

                End If
                llenaCombocmbConcepto(0)
                    cEmpleado = Nothing
                End If
            End If
    End Sub
    Private Sub llenaCombocmbConcepto(ByVal sel As Integer)
        Dim cConcepto As New Entities.Concepto
        objData.Catalogo(cmbConcepto, sel, cConcepto.ConsultarConcepto)
        cConcepto = Nothing
    End Sub
    Private Sub GridDeduciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridDeduciones.NeedDataSource
        Dim cDepartamento As New Entities.Departamento
        GridDeduciones.DataSource = cDepartamento.ConsultarDepartamento
        cDepartamento = Nothing
    End Sub
    Private Sub GridPercepciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridPercepciones.NeedDataSource
        Dim cDepartamento As New Entities.Departamento
        GridPercepciones.DataSource = cDepartamento.ConsultarDepartamento
        cDepartamento = Nothing
    End Sub
End Class