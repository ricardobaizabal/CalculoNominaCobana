Imports Telerik.Web.UI
Imports Entities
Public Class ModificacionGeneralAguinaldo
    Inherits System.Web.UI.Page

    Private IdEjercicio As Integer = 0
    Private SalarioMinimoDiarioGeneral As Double

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Not String.IsNullOrEmpty(Request("id")) Then
                tiponominaId.Value = Request.QueryString("id").ToString
                Call CargarDatos()
            End If
        End If
    End Sub

    Private Sub CargarDatos()
        Try
            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = tiponominaId.Value
            cNomina.Periodo = 0
            dt = cNomina.ConsultarDatosGeneralesNominaAguinaldo()
            cNomina = Nothing
            If dt.Rows.Count > 0 Then
                For Each oDataRow In dt.Rows
                    Me.lblEjercicio.Text = oDataRow("Ejercicio")
                    Me.lblTipoNomina.Text = oDataRow("TipoNomina")
                Next
                Call CargarGridEmpleados()
            Else
                Me.lblEjercicio.Text = ""
                Me.lblTipoNomina.Text = ""
            End If
        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub

    Private Sub CargarVariablesGenerales()

        Dim dt As New DataTable()
        Dim cConfiguracion = New Configuracion()
        cConfiguracion.IdUsuario = Session("usuarioid")
        dt = cConfiguracion.ConsultarConfiguracion()
        cConfiguracion = Nothing

        If dt.Rows.Count > 0 Then
            For Each oDataRow In dt.Rows
                IdEjercicio = oDataRow("IdEjercicio")
                SalarioMinimoDiarioGeneral = oDataRow("SalarioMinimoDiarioGeneral")
            Next
        End If
    End Sub

    Private Sub CargarGridEmpleados()

        CargarVariablesGenerales()

        Dim cNomina As New Nomina()
        cNomina.Ejercicio = IdEjercicio
        cNomina.TipoNomina = tiponominaId.Value
        cNomina.Periodo = 0
        grdEmpleadosAguinaldo.DataSource = cNomina.ConsultarDetalleNominaAguinaldo()
        grdEmpleadosAguinaldo.DataBind()
        cNomina = Nothing
    End Sub

    Sub txtAguinaldo_TextChanged(sender As Object, e As EventArgs)

        Dim NoEmpleado = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("NoEmpleado")
        Dim IdContrato = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IdContrato")

        Dim txtAguinaldo As RadNumericTextBox = DirectCast(sender, RadNumericTextBox)

        Dim aguinaldo As Decimal = 0

        Try
            aguinaldo = Convert.ToDecimal(txtAguinaldo.Text)
        Catch ex As Exception
            aguinaldo = 0
        End Try

        AgregaConcepto(aguinaldo, NoEmpleado, 14, IdContrato)

        Call CargarGridEmpleados()

    End Sub

    Sub txtImpuesto_TextChanged(sender As Object, e As EventArgs)
        Dim NoEmpleado = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("NoEmpleado")
        Dim IdContrato = DirectCast(DirectCast(DirectCast(sender, System.Web.UI.Control).Parent, Telerik.Web.UI.GridTableCell).Item, Telerik.Web.UI.GridEditableItem).GetDataKeyValue("IdContrato")

        Dim txtImpuesto As RadNumericTextBox = DirectCast(sender, RadNumericTextBox)

        Dim impuesto As Decimal = 0

        Try
            impuesto = Convert.ToDecimal(txtImpuesto.Text)
        Catch ex As Exception
            impuesto = 0
        End Try

        AgregaConcepto(impuesto, NoEmpleado, 52, IdContrato)

        Call CargarGridEmpleados()
    End Sub

    Private Sub AgregaConcepto(ByVal ImporteIncidencia, ByVal NoEmpleado, ByVal CvoConcepto, ByVal IdContrato)
        Try
            ImporteIncidencia = Convert.ToDecimal(ImporteIncidencia)
        Catch ex As Exception
            ImporteIncidencia = 0
        End Try

        If ChecarSiExiste(NoEmpleado, CvoConcepto) = True Then
            ActualizarRegistro(NoEmpleado, ImporteIncidencia, CvoConcepto, 1, IdContrato)
        Else
            GuardarRegistro(NoEmpleado, ImporteIncidencia, CvoConcepto, 1, IdContrato)
        End If

    End Sub

    Private Function ChecarSiExiste(ByVal NoEmpleado As Integer, ByVal CvoConcepto As Int32) As Boolean
        Try

            Call CargarVariablesGenerales()

            Dim dt As New DataTable()
            Dim cNomina As New Nomina()
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = tiponominaId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = CvoConcepto
            dt = cNomina.ConsultarConceptosEmpleadoAguinaldo()

            If dt.Rows.Count = 0 Then
                ChecarSiExiste = False
            Else
                ChecarSiExiste = True
            End If

            Return ChecarSiExiste

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Function

    Private Sub GuardarRegistro(ByVal NoEmpleado As Int64, ByVal ImporteIncidencia As Decimal, ByVal CvoConcepto As Int32, ByVal UnidadIncidencia As Decimal, ByVal IdContrato As Integer)

        Call CargarVariablesGenerales()

        Dim cNomina As New Nomina()

        If CvoConcepto = 14 Then

            cNomina = New Nomina()
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = tiponominaId.Value
            cNomina.Periodo = 0
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = CvoConcepto
            cNomina.IdContrato = IdContrato
            cNomina.TipoConcepto = "P"
            cNomina.Unidad = UnidadIncidencia
            cNomina.Importe = ImporteIncidencia
            cNomina.ImporteGravado = 0
            cNomina.ImporteExento = ImporteIncidencia
            cNomina.Generado = ""
            cNomina.Timbrado = ""
            cNomina.Enviado = ""
            cNomina.Tipo = "A"
            cNomina.GuadarNomina()

        ElseIf CvoConcepto = 52 Then

            cNomina = New Nomina()
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = tiponominaId.Value
            cNomina.Periodo = 0
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = CvoConcepto
            cNomina.IdContrato = IdContrato
            cNomina.TipoConcepto = "D"
            cNomina.Unidad = UnidadIncidencia
            cNomina.Importe = ImporteIncidencia
            cNomina.ImporteGravado = 0
            cNomina.ImporteExento = ImporteIncidencia
            cNomina.Generado = ""
            cNomina.Timbrado = ""
            cNomina.Enviado = ""
            cNomina.Tipo = "A"
            cNomina.GuadarNomina()

        End If

    End Sub

    Private Sub ActualizarRegistro(ByVal NoEmpleado As Int64, ByVal ImporteIncidencia As Decimal, ByVal CvoConcepto As Int32, ByVal UnidadIncidencia As Decimal, ByVal IdContrato As Integer)

        Call CargarVariablesGenerales()

        Dim cNomina As New Nomina()

        If CvoConcepto = 14 Then

            cNomina = New Nomina()
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = tiponominaId.Value
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 14
            cNomina.TipoConcepto = "P"
            cNomina.Tipo = "A"
            cNomina.EliminaConceptoEmpleadoAguinaldo()

            Dim ImporteExentoAguinaldo As Decimal = 0
            Dim ImporteGravadoAguinaldo As Decimal = 0

            If ImporteIncidencia > 0 Then
                If ImporteIncidencia > 0 And ImporteIncidencia < (SalarioMinimoDiarioGeneral * 30) Then
                    ImporteExentoAguinaldo = ImporteIncidencia
                    ImporteGravadoAguinaldo = 0
                ElseIf ImporteIncidencia > 0 And ImporteIncidencia > (SalarioMinimoDiarioGeneral * 30) Then
                    ImporteExentoAguinaldo = SalarioMinimoDiarioGeneral * 30
                    ImporteGravadoAguinaldo = ImporteIncidencia - (SalarioMinimoDiarioGeneral * 30)
                End If

                cNomina = New Nomina()
                cNomina.Ejercicio = IdEjercicio
                cNomina.TipoNomina = tiponominaId.Value
                cNomina.Periodo = 0
                cNomina.NoEmpleado = NoEmpleado
                cNomina.IdContrato = IdContrato
                cNomina.CvoConcepto = 14
                cNomina.Tipo = "A"
                cNomina.TipoConcepto = "P"
                cNomina.Unidad = 1
                cNomina.Importe = ImporteIncidencia
                cNomina.ImporteGravado = ImporteGravadoAguinaldo
                cNomina.ImporteExento = ImporteExentoAguinaldo
                cNomina.Generado = ""
                cNomina.IdMovimiento = 0
                cNomina.GuardarExentoYGravadoFiniquito()

            End If

            'cNomina = New Nomina()
            'cNomina.Ejercicio = IdEjercicio
            'cNomina.TipoNomina = tiponominaId.Value
            'cNomina.NoEmpleado = NoEmpleado
            'cNomina.CvoConcepto = CvoConcepto
            'cNomina.IdContrato = IdContrato
            'cNomina.TipoConcepto = "P"
            'cNomina.Unidad = UnidadIncidencia
            'cNomina.Importe = ImporteIncidencia
            'cNomina.ImporteGravado = 0
            'cNomina.ImporteExento = ImporteIncidencia
            'cNomina.Generado = ""
            'cNomina.Timbrado = ""
            'cNomina.Enviado = ""
            'cNomina.Tipo = "A"
            'cNomina.GuadarNomina()

        ElseIf CvoConcepto = 52 Then

            cNomina = New Nomina()
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = tiponominaId.Value
            cNomina.Periodo = 0
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = 86
            cNomina.TipoConcepto = "D"
            cNomina.Tipo = "A"
            cNomina.EliminaConceptoEmpleadoAguinaldo()

            cNomina = New Nomina()
            cNomina.Ejercicio = IdEjercicio
            cNomina.TipoNomina = tiponominaId.Value
            cNomina.Periodo = 0
            cNomina.NoEmpleado = NoEmpleado
            cNomina.CvoConcepto = CvoConcepto
            cNomina.IdContrato = IdContrato
            cNomina.TipoConcepto = "D"
            cNomina.Unidad = UnidadIncidencia
            cNomina.Importe = ImporteIncidencia
            cNomina.ImporteGravado = 0
            cNomina.ImporteExento = ImporteIncidencia
            cNomina.Generado = ""
            cNomina.Timbrado = ""
            cNomina.Enviado = ""
            cNomina.Tipo = "A"
            cNomina.GuadarNomina()

        End If

    End Sub

    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        If Not String.IsNullOrEmpty(Request("id")) Then
            Response.Redirect("~/CalculoAguinaldo.aspx?id=" & tiponominaId.Value.ToString, False)
        Else
            Response.Redirect("~/CalculoAguinaldo.aspx", False)
        End If
    End Sub

    Private Sub grdEmpleadosAguinaldo_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles grdEmpleadosAguinaldo.ItemCommand
        Select Case e.CommandName
            Case "cmdDelete"
                empleadoId.Value = e.CommandArgument
                rwConfirmEliminaEmpleado.RadConfirm("¿Realmente desea eliminar al trabajador de esta nomina?", "confirmCallbackFnEliminaEmpleado", 330, 180, Nothing, "Confirmar")
        End Select
    End Sub

    Private Sub btnEliminarEmpleado_Click(sender As Object, e As EventArgs) Handles btnEliminarEmpleado.Click
        Call EliminarTrabajador()
        Call CargarGridEmpleados()
    End Sub

    Private Sub EliminarTrabajador()
        Try

            Call CargarVariablesGenerales()

            Dim cNomina As New Nomina()
            cNomina.Ejercicio = IdEjercicio
            cNomina.NoEmpleado = empleadoId.Value
            cNomina.TipoNomina = tiponominaId.Value
            cNomina.EliminaEmpleadoNominaAguinaldo()

        Catch oExcep As Exception
            rwAlerta.RadAlert(oExcep.Message.ToString, 330, 180, "Alerta", "", "")
        End Try
    End Sub

End Class