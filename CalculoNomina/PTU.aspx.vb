Imports Telerik.Web.UI
Imports Entities
Public Class PTU
    Inherits System.Web.UI.Page

    Private PercepcionesExentas As Double
    Private PercepcionesGravadas As Double

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request("empid")) And Not String.IsNullOrEmpty(Request("cid")) And Not String.IsNullOrEmpty(Request("eid")) Then
                empleadoId.Value = Request.QueryString("empid").ToString
                contratoId.Value = Request.QueryString("cid").ToString
                empresaId.Value = Request.QueryString("eid").ToString
                ejercicioId.Value = Request.QueryString("ejid").ToString
                tiponominaId.Value = Request.QueryString("tnid").ToString

                Call ChecarPercepcionesExentasYGravadas()
                Call CargarPercepcionesYDeducciones()

                txtImporteIncidencia.Focus()

            End If
            Call LlenaCmbConcepto(50, "P")
        End If
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Try
            Dim Importe As Decimal = 0
            Dim Unidad As Decimal = 0
            Try
                Importe = Convert.ToDecimal(txtImporteIncidencia.Text)
            Catch ex As Exception
                Importe = 0
            End Try
            Try
                Unidad = Convert.ToDecimal(txtUnidadIncidencia.Text)
            Catch ex As Exception
                Unidad = 0
            End Try

            If Importe <= 0 Then
                rwAlerta.RadAlert("Favor de digitar un importe!!", 330, 180, "Alerta", "", "")
                Exit Sub
            ElseIf Unidad <= 0 Then
                rwAlerta.RadAlert("Favor de digitar un unidad!!", 330, 180, "Alerta", "", "")
                Exit Sub
            End If

            Call GuardarRegistro()

            Call CargarPercepciones()
            Call CargarDeducciones()

            Call ChecarPercepcionesExentasYGravadas()
            Call CargarPercepcionesYDeducciones()

            txtGravadoISR.Text = Math.Round(PercepcionesGravadas, 6)
            txtExentoISR.Text = Math.Round(PercepcionesExentas, 6)
            txtImporteIncidencia.Text = 0
            txtImporteIncidencia.Focus()

        Catch ex As Exception

        End Try
    End Sub

    Private Sub GridDeduciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridDeduciones.NeedDataSource
        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = empresaId.Value
        cNomina.Ejercicio = ejercicioId.Value
        cNomina.TipoNomina = tiponominaId.Value
        cNomina.Periodo = 0
        cNomina.TipoConcepto = "D"
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.CvoConcepto = 86
        dt = cNomina.ConsultarDeduccionesEmpleado()
        cNomina = Nothing
        GridDeduciones.DataSource = dt

        If dt.Rows.Count > 0 Then
            txtDeducciones.Text = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
        End If

    End Sub

    Private Sub GridPercepciones_NeedDataSource(sender As Object, e As GridNeedDataSourceEventArgs) Handles GridPercepciones.NeedDataSource
        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = empresaId.Value
        cNomina.Ejercicio = ejercicioId.Value
        cNomina.TipoNomina = tiponominaId.Value
        cNomina.Periodo = 0
        cNomina.TipoConcepto = "P"
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.CvoConcepto = 50
        dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
        cNomina = Nothing
        GridPercepciones.DataSource = dt

        If dt.Rows.Count > 0 Then
            txtPercepciones.Text = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
        End If

    End Sub

    Private Sub GuardarRegistro()
        Try

            Dim ImporteIncidencia As Decimal = 0
            Dim UnidadIncidencia As Decimal = 0

            Try
                ImporteIncidencia = Convert.ToDecimal(txtImporteIncidencia.Text)
            Catch ex As Exception
                ImporteIncidencia = 0
            End Try

            Try
                UnidadIncidencia = Convert.ToDecimal(txtUnidadIncidencia.Text)
            Catch ex As Exception
                UnidadIncidencia = 0
            End Try

            Dim cNomina = New Nomina()
            'cNomina.IdEmpresa = empresaId.Value
            cNomina.Ejercicio = ejercicioId.Value
            cNomina.TipoNomina = tiponominaId.Value
            cNomina.Periodo = 0
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.CvoConcepto = cmbConcepto.SelectedValue.ToString
            cNomina.IdContrato = contratoId.Value
            cNomina.Unidad = UnidadIncidencia
            cNomina.Importe = ImporteIncidencia
            cNomina.ImporteGravado = 0
            cNomina.ImporteExento = ImporteIncidencia
            cNomina.Generado = ""
            cNomina.Timbrado = ""
            cNomina.Enviado = ""
            cNomina.Situacion = "A"
            cNomina.Tipo = "U"

            If cmbConcepto.SelectedValue = 50 Then
                cNomina.TipoConcepto = "P"
                Call BorrarPTU()
            ElseIf cmbConcepto.SelectedValue = 52 Then
                cNomina.TipoConcepto = "D"
                Call BorrarImpuesto()
            End If

            cNomina.GuadarNomina()

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub

    Private Sub LlenaCmbConcepto(ByVal sel As Integer, ByVal Tipo As String)
        Try
            Dim cConcepto As New Entities.Concepto
            If Tipo.Length > 0 Then
                cConcepto.Tipo = Tipo
            End If

            Dim ObjData As New DataControl()
            ObjData.Catalogo(cmbConcepto, sel, cConcepto.ConsultarConcepto)
            ObjData = Nothing
            cConcepto = Nothing
            cmbConcepto.Enabled = False

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub

    Private Sub rdoDeducción_CheckedChanged(sender As Object, e As EventArgs) Handles rdoDeducción.CheckedChanged
        Call LlenaCmbConcepto(52, "D")
    End Sub

    Private Sub rdoPercepcion_CheckedChanged(sender As Object, e As EventArgs) Handles rdoPercepcion.CheckedChanged
        Call LlenaCmbConcepto(50, "P")
    End Sub

    Private Sub ChecarPercepcionesExentasYGravadas()
        Try

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = empresaId.Value
            cNomina.Ejercicio = ejercicioId.Value
            cNomina.TipoNomina = tiponominaId.Value
            cNomina.Periodo = 0
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.TipoConcepto = "P"
            dt = cNomina.ConsultarConceptosEmpleado()

            If dt.Rows.Count > 0 Then
                PercepcionesGravadas = dt.Compute("Sum(ImporteGravado)", "")
                PercepcionesExentas = dt.Compute("Sum(ImporteExento)", "")
            End If

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub

    Private Sub CargarPercepcionesYDeducciones()
        Try

            Dim Percepciones, Deducciones As Decimal

            ' Percepciones
            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            'cNomina.IdEmpresa = empresaId.Value
            cNomina.Ejercicio = ejercicioId.Value
            cNomina.TipoNomina = tiponominaId.Value
            cNomina.Periodo = 0
            cNomina.TipoConcepto = "P"
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.CvoConcepto = 50
            dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()

            If dt.Rows.Count > 0 Then
                Percepciones = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
                txtPercepciones.Text = Percepciones

                If dt.Rows(0).Item("TIMBRADO") = "S" Then
                    btnAgregar.Enabled = False
                Else
                    btnAgregar.Enabled = True
                End If

            End If

            ' Deducciones
            dt = New DataTable()
            cNomina = New Nomina()
            'cNomina.IdEmpresa = empresaId.Value
            cNomina.Ejercicio = ejercicioId.Value
            cNomina.TipoNomina = tiponominaId.Value
            cNomina.Periodo = 0
            cNomina.TipoConcepto = "D"
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.CvoConcepto = 86
            dt = cNomina.ConsultarDeduccionesEmpleado()
            cNomina = Nothing

            If dt.Rows.Count > 0 Then
                Deducciones = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
                txtDeducciones.Text = Deducciones

                txtNetoAPagar.Text = Percepciones - Deducciones
            Else
                txtDeducciones.Text = 0
                txtNetoAPagar.Text = Percepciones - Deducciones
            End If

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub

    Private Sub CargarPercepciones()
        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = empresaId.Value
        cNomina.Ejercicio = ejercicioId.Value
        cNomina.TipoNomina = tiponominaId.Value
        cNomina.Periodo = 0
        cNomina.TipoConcepto = "P"
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.CvoConcepto = 50
        dt = cNomina.ConsultarPercepcionesDeduccionesEmpleado()
        cNomina = Nothing
        GridPercepciones.DataSource = dt
        GridPercepciones.DataBind()

        If dt.Rows.Count > 0 Then
            txtPercepciones.Text = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
        End If
    End Sub

    Private Sub CargarDeducciones()
        Dim dt As New DataTable()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = empresaId.Value
        cNomina.Ejercicio = ejercicioId.Value
        cNomina.TipoNomina = tiponominaId.Value
        cNomina.Periodo = 0
        cNomina.TipoConcepto = "D"
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.CvoConcepto = 86
        dt = cNomina.ConsultarDeduccionesEmpleado()
        cNomina = Nothing
        GridDeduciones.DataSource = dt
        GridDeduciones.DataBind()

        If dt.Rows.Count > 0 Then
            txtDeducciones.Text = Math.Round(dt.Compute("Sum(Importe)", ""), 6)
        End If
    End Sub

    Public Sub BorrarImpuesto()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = empresaId.Value
        cNomina.Ejercicio = ejercicioId.Value
        cNomina.TipoNomina = tiponominaId.Value
        cNomina.Periodo = 0
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.CvoConcepto = 86
        cNomina.TipoConcepto = "D"
        cNomina.Tipo = "U"
        cNomina.EliminaConceptoEmpleado()
    End Sub

    Public Sub BorrarPTU()
        Dim cNomina As New Nomina()
        'cNomina.IdEmpresa = empresaId.Value
        cNomina.Ejercicio = ejercicioId.Value
        cNomina.TipoNomina = tiponominaId.Value
        cNomina.Periodo = 0
        cNomina.NoEmpleado = empleadoId.Value
        cNomina.CvoConcepto = 50
        cNomina.TipoConcepto = "P"
        cNomina.Tipo = "U"
        cNomina.EliminaConceptoEmpleado()
    End Sub

    Private Sub GridPercepciones_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridPercepciones.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                conceptoId.Value = e.CommandArgument
                rwConfirmEliminaConcepto.RadConfirm("¿Está seguro de eliminar este concepto?", "confirmCallbackFnEliminaConcepto", 330, 180, Nothing, "Confirmar")
        End Select
    End Sub

    Private Sub GridDeduciones_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles GridDeduciones.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                conceptoId.Value = e.CommandArgument
                rwConfirmEliminaConcepto.RadConfirm("¿Está seguro de eliminar este concepto?", "confirmCallbackFnEliminaConcepto", 330, 180, Nothing, "Confirmar")
        End Select
    End Sub

    Private Sub btnEliminarConcepto_Click(sender As Object, e As EventArgs) Handles btnEliminarConcepto.Click
        If conceptoId.Value = 50 Then
            Call BorrarPTU()
        ElseIf conceptoId.Value = 52 Then
            Call BorrarImpuesto()
        End If

        Call CargarPercepciones()
        Call CargarDeducciones()
        Call ChecarPercepcionesExentasYGravadas()
        Call CargarPercepcionesYDeducciones()

    End Sub

End Class