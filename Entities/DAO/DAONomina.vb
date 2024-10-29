Imports System.Data.SqlClient
Partial Public Class Nomina
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Sub GuadarNomina()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdEmpresa", Cliente))
            p.Add(New SqlParameter("@pEjercicio", Ejercicio))
            p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
            p.Add(New SqlParameter("@pPeriodo", Periodo))
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
            p.Add(New SqlParameter("@pCvoConcepto", CvoConcepto))
            p.Add(New SqlParameter("@pIdContrato", IdContrato))
            p.Add(New SqlParameter("@pTipoConcepto", TipoConcepto))
            p.Add(New SqlParameter("@pUnidad", Unidad))
            p.Add(New SqlParameter("@pImporte", Importe))
            p.Add(New SqlParameter("@pImporteGravado", ImporteGravado))
            p.Add(New SqlParameter("@pImporteExento", ImporteExento))
            p.Add(New SqlParameter("@pGenerado", Generado))
            p.Add(New SqlParameter("@pTimbrado", Timbrado))
            p.Add(New SqlParameter("@pEnviado", Enviado))
            p.Add(New SqlParameter("@pSituacion", Situacion))
            p.Add(New SqlParameter("@pIdMovimiento", IdMovimiento))
            p.Add(New SqlParameter("@pDiasHorasExtra", DiasHorasExtra))
            p.Add(New SqlParameter("@pTipoHorasExtra", TipoHorasExtra))
            p.Add(New SqlParameter("@pEsEspecial", EsEspecial))
            p.Add(New SqlParameter("@FechaInicialPago", FechaIni))
            p.Add(New SqlParameter("@FechaFinalPago", FechaFin))
            p.Add(New SqlParameter("@FechaPago", FechaPago))
            p.Add(New SqlParameter("@DiasPagados", DiasPagados))

            If IdCreditoFonacot > 0 Then
                p.Add(New SqlParameter("@pIdCreditoFonacot", IdCreditoFonacot))
            End If

            If Tipo.Length > 0 Then
                p.Add(New SqlParameter("@pTipo", Tipo))
            End If

            db.ExecuteSPWithParams("pAgregaNomina", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GuadarNominaPeriodo()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdEmpresa", Cliente))
            p.Add(New SqlParameter("@pEjercicio", Ejercicio))
            p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
            p.Add(New SqlParameter("@pPeriodo", Periodo))
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
            p.Add(New SqlParameter("@pCvoConcepto", CvoConcepto))
            p.Add(New SqlParameter("@pIdContrato", IdContrato))
            p.Add(New SqlParameter("@pTipoConcepto", TipoConcepto))
            p.Add(New SqlParameter("@pUnidad", Unidad))
            p.Add(New SqlParameter("@pImporte", Importe))
            p.Add(New SqlParameter("@pImporteGravado", ImporteGravado))
            p.Add(New SqlParameter("@pImporteExento", ImporteExento))
            p.Add(New SqlParameter("@pGenerado", Generado))
            p.Add(New SqlParameter("@pTimbrado", Timbrado))
            p.Add(New SqlParameter("@pEnviado", Enviado))
            p.Add(New SqlParameter("@pSituacion", Situacion))
            p.Add(New SqlParameter("@pIdMovimiento", IdMovimiento))
            p.Add(New SqlParameter("@pDiasHorasExtra", DiasHorasExtra))
            p.Add(New SqlParameter("@pTipoHorasExtra", TipoHorasExtra))
            p.Add(New SqlParameter("@pEsEspecial", EsEspecial))
            p.Add(New SqlParameter("@FechaInicialPago", FechaIni))
            p.Add(New SqlParameter("@FechaFinalPago", FechaFin))
            p.Add(New SqlParameter("@FechaPago", FechaPago))
            p.Add(New SqlParameter("@DiasPagados", DiasPagados))
            p.Add(New SqlParameter("@IdNomina", IdNomina))

            If IdCreditoFonacot > 0 Then
                p.Add(New SqlParameter("@pIdCreditoFonacot", IdCreditoFonacot))
            End If

            If Tipo.Length > 0 Then
                p.Add(New SqlParameter("@pTipo", Tipo))
            End If

            db.ExecuteSPWithParams("pAgregaNominaPeriodo", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarEmpleadosSemanal() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pCliente", Cliente))
        dt = db.ExecuteSP("pConsultarEmpleadosSemanal", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosEspecial(ByVal mi_cliente_id As Integer) As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@mi_cliente_id", mi_cliente_id))
        dt = db.ExecuteSP("pConsultarEmpleadosEspecial", p)
        Return dt
    End Function
    Public Function InsertaCampoNomina(ByVal mi_cliente_id As Integer) As DataTable
        p.Clear()
        p.Add(New SqlParameter("@mi_cliente_id", mi_cliente_id))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pFechaPago", FechaPago))
        p.Add(New SqlParameter("@pObservaciones", Observaciones))
        dt = db.ExecuteSP("pAgregarCampoNomina", p)
        Return dt
    End Function
    Public Sub ActualizarObservacionesNomina()
        p.Clear()
        p.Add(New SqlParameter("@pIdNomina", IdNomina))
        p.Add(New SqlParameter("@pObservaciones", Observaciones))
        db.ExecuteSPWithParams("pActualizarObservacionesNomina", p)
    End Sub
    Public Function ConsultarNominaPorFolio(ByVal folio As Integer) As DataTable
        p.Clear()
        p.Add(New SqlParameter("@ID", folio))
        Dim dt As DataTable = db.ExecuteSP("pConsultarPorIdNominaExtraordinaria", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosCatorcenal() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pCliente", Cliente))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        dt = db.ExecuteSP("pConsultarEmpleadosCatorcenal", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosQuincenal() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pCliente", Cliente))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        dt = db.ExecuteSP("pConsultarEmpleadosQuincenal", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosMensual() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pCliente", Cliente))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        dt = db.ExecuteSP("pConsultarEmpleadosMensual", p)
        Return dt
    End Function
    Public Function ConsultarDatosGeneralesNomina() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pIdEmpresa", Cliente))
        p.Add(New SqlParameter("@pExtraordinarioBit", EsEspecial))
        dt = db.ExecuteSP("pConsultarDatosGeneralesNomina", p)
        Return dt
    End Function
    Public Function ConsultarDatosGeneralesNominaAguinaldo() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        dt = db.ExecuteSP("pConsultarDatosGeneralesNominaAguinaldo", p)
        Return dt
    End Function
    Public Function ConsultarDetalleNomina() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pIdEmpresa", Cliente))
        dt = db.ExecuteSP("pConsultarDetalleNomina", p)
        Return dt
    End Function
    Public Function ConsultarDetalleNominaExtraordinaria() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pCliente", Cliente))
        p.Add(New SqlParameter("@pEsEspecial", EsEspecial))
        p.Add(New SqlParameter("@pIdNomina", IdNomina))
        dt = db.ExecuteSP("pConsultarDetalleNominaExtraordinaria", p)
        Return dt
    End Function
    Public Function ConsultarDetalleNominaExtraordinariaFolio() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pFolio", IdNomina))

        dt = db.ExecuteSP("pConsultarDetalleNominaExtraordinaria", p)
        Return dt
    End Function
    Public Function ConsultarTodasNominaExtraordinaria() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pCliente", Cliente))
        p.Add(New SqlParameter("@pEsEspecial", EsEspecial))
        dt = db.ExecuteSP("pConsultarTodasLasNominasExtraordinarias", p)
        Return dt
    End Function
    Public Function ConsultarConceptosEmpleado() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        If TipoConcepto.Length > 0 Then
            p.Add(New SqlParameter("@pTipoConcepto", TipoConcepto))
        End If
        If CvoConcepto > 0 Then
            p.Add(New SqlParameter("@pCvoConcepto", CvoConcepto))
        End If
        If DiferenteDe > 0 Then
            p.Add(New SqlParameter("@pDiferente", DiferenteDe))
        End If
        dt = db.ExecuteSP("pConsultarConceptosEmpleado", p)
        Return dt
    End Function
    Public Function ConsultarConceptosEmpleadoAguinaldo() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pCvoConcepto", CvoConcepto))
        If TipoConcepto.Length > 0 Then
            p.Add(New SqlParameter("@pTipoConcepto", TipoConcepto))
        End If
        dt = db.ExecuteSP("pConsultarConceptosEmpleadoAguinaldo", p)
        Return dt
    End Function
    Public Function ConsultarConceptosFiniquito() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pTipo", Tipo))
        p.Add(New SqlParameter("@pIdMovimiento", IdMovimiento))
        If TipoConcepto.Length > 0 Then
            p.Add(New SqlParameter("@pTipoConcepto", TipoConcepto))
        End If
        If CvoConcepto > 0 Then
            p.Add(New SqlParameter("@pCvoConcepto", CvoConcepto))
        End If
        If DiferenteDe > 0 Then
            p.Add(New SqlParameter("@pDiferente", DiferenteDe))
        End If
        dt = db.ExecuteSP("pConsultarConceptosFiniquito", p)
        Return dt
    End Function
    Public Function ConsultarPercepcionesDeduccionesEmpleado() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        If TipoConcepto.Length > 0 Then
            p.Add(New SqlParameter("@pTipoConcepto", TipoConcepto))
        End If
        If CvoSAT.Length > 0 Then
            p.Add(New SqlParameter("@pCvoSAT", CvoSAT))
        End If
        If CvoConcepto > 0 Then
            p.Add(New SqlParameter("@pCvoConcepto", CvoConcepto))
        End If
        If Tipo.Length > 0 Then
            p.Add(New SqlParameter("@pTipo", Tipo))
        End If
        If OtroPagoBit > 0 Then
            p.Add(New SqlParameter("@pOtroPagoBit", OtroPagoBit))
        End If
        dt = db.ExecuteSP("pConsultarPercepcionesEmpleado", p)
        Return dt
    End Function
    Public Function ConsultarTotalPercepcionesEmpleado() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pTipoConcepto", TipoConcepto))
        dt = db.ExecuteSP("pConsultarTotalPercepcionesEmpleado", p)
        Return dt
    End Function
    Public Function ConsultarOtrosPagos() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pTipo", Tipo))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pEspecial ", EsEspecial))
        dt = db.ExecuteSP("pConsultarOtrosPagos", p)
        Return dt
    End Function
    Public Function ConsultarPercepcionesDeduccionesFiniquito() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pTipo", Tipo))
        p.Add(New SqlParameter("@pTipoConcepto", TipoConcepto))
        p.Add(New SqlParameter("@pIdMovimiento", IdMovimiento))
        dt = db.ExecuteSP("pConsultarPercepcionesDeduccionesFiniquito", p)
        Return dt
    End Function
    Public Function ConsultarPercepcionesDeduccionesFiniquitoGenerado() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pTipo", Tipo))
        p.Add(New SqlParameter("@pTipoConcepto", TipoConcepto))
        If IdContrato > 0 Then
            p.Add(New SqlParameter("@pIdContrato", IdContrato))
        End If
        If IdMovimiento > 0 Then
            p.Add(New SqlParameter("@pIdMovimiento", IdMovimiento))
        End If
        dt = db.ExecuteSP("pConsultarPercepcionesDeduccionesFiniquitoGenerado", p)
        Return dt
    End Function
    Public Function ConsultarDeduccionesEmpleado() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        If TipoConcepto.Length > 0 Then
            p.Add(New SqlParameter("@pTipoConcepto", TipoConcepto))
        End If
        If Tipo.Length > 0 Then
            p.Add(New SqlParameter("@pTipo", Tipo))
        End If
        dt = db.ExecuteSP("pConsultarDeduccionesEmpleado", p)
        Return dt
    End Function
    Public Sub EliminaConceptoEmpleado()
        p.Clear()
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        If TipoConcepto.Length > 0 Then
            p.Add(New SqlParameter("@pTipoConcepto", TipoConcepto))
        End If
        If CvoConcepto > 0 Then
            p.Add(New SqlParameter("@pCvoConcepto", CvoConcepto))
        End If
        If Tipo.Length > 0 Then
            p.Add(New SqlParameter("@pTipo", Tipo))
        End If
        If TipoHorasExtra.Length > 0 Then
            p.Add(New SqlParameter("@pTipoHorasExtra", TipoHorasExtra))
        End If
        db.ExecuteSP("pEliminaConceptoEmpleado", p)
    End Sub
    Public Sub EliminaConceptoEmpleadoAguinaldo()
        p.Clear()
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pTipoConcepto", TipoConcepto))
        p.Add(New SqlParameter("@pCvoConcepto", CvoConcepto))
        p.Add(New SqlParameter("@pTipo", Tipo))
        db.ExecuteSP("pEliminaConceptoEmpleadoAguinaldo", p)
    End Sub
    Public Sub EliminaConceptosFiniquito()
        p.Clear()
        p.Add(New SqlParameter("@pTipo", Tipo))
        p.Add(New SqlParameter("@pIdMovimiento", IdMovimiento))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        If CvoConcepto > 0 Then
            p.Add(New SqlParameter("@pCvoConcepto", CvoConcepto))
        End If

        db.ExecuteSP("pEliminaConceptosFiniquito", p)
    End Sub
    Public Sub ActualizaNominaGenerado()
        Try
            p.Clear()
            'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
            p.Add(New SqlParameter("@pEjercicio", Ejercicio))
            p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
            p.Add(New SqlParameter("@pPeriodo", Periodo))

            db.ExecuteSPWithParams("pUpdateNominaGenerado", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub EliminaEmpleado()
        p.Clear()
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        dt = db.ExecuteSP("pEliminaEmpleadoNomina", p)
    End Sub
    Public Sub EliminaEmpleadoNominaAguinaldo()
        p.Clear()
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        dt = db.ExecuteSP("pEliminaEmpleadoNominaAguinaldo", p)
    End Sub
    Public Sub EliminaNomina()
        p.Clear()
        p.Add(New SqlParameter("@pIdEmpresa", Cliente))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        dt = db.ExecuteSP("pEliminaNomina", p)
    End Sub
    Public Sub EliminaNominaPTU()
        p.Clear()
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pTipo", Tipo))
        dt = db.ExecuteSP("pEliminaNominaPTU", p)
    End Sub
    Public Sub EliminaNominaAguinaldo()
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipo", Tipo))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        dt = db.ExecuteSP("pEliminaNominaAguinaldo", p)
    End Sub
    Public Sub ActualizarDiasYCuotaPeriodo()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
            'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            p.Add(New SqlParameter("@pEjercicio", Ejercicio))
            p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
            p.Add(New SqlParameter("@pPeriodo", Periodo))
            p.Add(New SqlParameter("@pCvoConcepto", CvoConcepto))
            p.Add(New SqlParameter("@pUnidad", Unidad))
            p.Add(New SqlParameter("@pImporte", Importe))

            db.ExecuteSP("pActualizarDiasYCuotaPeriodo", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub EliminarExentosYGravados()
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        dt = db.ExecuteSP("pEliminarExentosYGravados", p)
    End Sub
    Public Function ConsultarEmpleadosNomina() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        dt = db.ExecuteSP("pConsultarEmpleadosNomina", p)
        Return dt
    End Function
    Public Function ConsultarDatosEmpleados() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        dt = db.ExecuteSP("pConsultarDatosEmpleados", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosNoGenerados() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pEsEspecial", EsEspecial))
        p.Add(New SqlParameter("@pCliente", Cliente))
        If Tipo.Length > 0 Then
            p.Add(New SqlParameter("@pTipo", Tipo))
        End If
        dt = db.ExecuteSP("pConsultarEmpleadosNoGenerados", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosNoGeneradosNominaExtraordinaria() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pEsEspecial", EsEspecial))
        p.Add(New SqlParameter("@pCliente", Cliente))
        If Tipo.Length > 0 Then
            p.Add(New SqlParameter("@pTipo", Tipo))
        End If
        dt = db.ExecuteSP("pConsultarEmpleadosNoGeneradosNominaExtraordinaria", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosGenerados() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        If Tipo.Length > 0 Then
            p.Add(New SqlParameter("@pTipo", Tipo))
        End If
        dt = db.ExecuteSP("pConsultarEmpleadosGenerados", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosTimbrar() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pEsEspecial", EsEspecial))
        p.Add(New SqlParameter("@pCliente", Cliente))
        If Timbrado.Length > 0 Then
            p.Add(New SqlParameter("@pTimbrado", Timbrado))
        End If
        If Tipo.Length > 0 Then
            p.Add(New SqlParameter("@pTipo", Tipo))
        End If
        dt = db.ExecuteSP("pConsultarEmpleadosTimbrar", p)
        Return dt
    End Function
    Public Sub GuardarExentoYGravadoFiniquito()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdEmpresa", Cliente))
            p.Add(New SqlParameter("@pEjercicio", Ejercicio))
            p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
            p.Add(New SqlParameter("@pPeriodo", Periodo))
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
            p.Add(New SqlParameter("@pCvoConcepto", CvoConcepto))
            p.Add(New SqlParameter("@pIdContrato", IdContrato))
            p.Add(New SqlParameter("@pTipo", Tipo))
            p.Add(New SqlParameter("@pTipoConcepto", TipoConcepto))
            p.Add(New SqlParameter("@pUnidad", Unidad))
            p.Add(New SqlParameter("@pImporte", Importe))
            p.Add(New SqlParameter("@pImporteGravado", ImporteGravado))
            p.Add(New SqlParameter("@pImporteExento", ImporteExento))
            p.Add(New SqlParameter("@pGenerado", Generado))
            p.Add(New SqlParameter("@pIdMovimiento", IdMovimiento))
            p.Add(New SqlParameter("@pFechaInicialPago", FechaIni))
            p.Add(New SqlParameter("@pFechaFinalPago", FechaFin))
            p.Add(New SqlParameter("@pFechaPago", FechaPago))
            db.ExecuteSPWithParams("pGuardarExentoYGravadoFiniquito", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ActualizarExentoYGravado()
        Try
            p.Clear()
            'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
            p.Add(New SqlParameter("@pEjercicio", Ejercicio))
            p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
            p.Add(New SqlParameter("@pPeriodo", Periodo))
            p.Add(New SqlParameter("@pCvoConcepto", CvoConcepto))
            p.Add(New SqlParameter("@pImporteGravado", ImporteGravado))
            p.Add(New SqlParameter("@pImporteExento", ImporteExento))
            If TipoHorasExtra.ToString.Length > 0 Then
                p.Add(New SqlParameter("@pTipoHorasExtra", TipoHorasExtra))
            End If

            db.ExecuteSP("pActualizarExentoYGravado", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ActualizarExentoYGravadoFiniquito()
        Try
            p.Clear()
            'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
            p.Add(New SqlParameter("@pEjercicio", Ejercicio))
            p.Add(New SqlParameter("@pCvoConcepto", CvoConcepto))
            p.Add(New SqlParameter("@pImporteGravado", ImporteGravado))
            p.Add(New SqlParameter("@pImporteExento", ImporteExento))
            p.Add(New SqlParameter("@pIdMovimiento", IdMovimiento))
            p.Add(New SqlParameter("@pPeriodo", Periodo))
            p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
            db.ExecuteSP("pActualizarExentoYGravadoFiniquito", p)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarDatosEmisor() As DataTable
        p.Clear()
        db = New DBManager.DataBase(0)
        p.Add(New SqlParameter("@clienteid", Id))
        dt = db.ExecuteSP("pConsultarDatosEmisor", p)
        Return dt
    End Function
    Public Function ConsultarDatosCliente() As DataTable
        p.Clear()
        db = New DBManager.DataBase(1)
        p.Add(New SqlParameter("@clienteid", Id))
        dt = db.ExecuteSP("pConsultarDatosCliente", p)
        Return dt
    End Function
    Public Function ConsultarLugarExpedicion() As DataTable
        p.Clear()
        dt = db.ExecuteSP("pConsultarLugarExpedicion", p)
        Return dt
    End Function
    Public Sub ActualizarEstatusGeneradoNomina()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
            'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            p.Add(New SqlParameter("@pEjercicio", Ejercicio))
            p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
            p.Add(New SqlParameter("@pTipo", Tipo))
            p.Add(New SqlParameter("@pPeriodo", Periodo))
            p.Add(New SqlParameter("@pGenerado", Generado))

            db.ExecuteSP("pActualizarEstatusGeneradoNomina", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ActualizarEstatusTimbradoNomina()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
            'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            p.Add(New SqlParameter("@pEjercicio", Ejercicio))
            p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
            p.Add(New SqlParameter("@pPeriodo", Periodo))
            p.Add(New SqlParameter("@pTimbrado", Timbrado))
            p.Add(New SqlParameter("@pTipo", Tipo))
            p.Add(New SqlParameter("@pUUID", UUID))

            db.ExecuteSP("pActualizarEstatusTimbradoNomina", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ActualizarEstatusPfNomina()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
            'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            p.Add(New SqlParameter("@pEjercicio", Ejercicio))
            p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
            p.Add(New SqlParameter("@pPeriodo", Periodo))
            p.Add(New SqlParameter("@pPdf", Pdf))

            db.ExecuteSP("pActualizarEstatusPdfNomina", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ActualizarEstatusPfNominaEspecial()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
            p.Add(New SqlParameter("@pEjercicio", Ejercicio))
            p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
            p.Add(New SqlParameter("@pPeriodo", Periodo))
            p.Add(New SqlParameter("@pTipo", Tipo))
            p.Add(New SqlParameter("@pPdf", Pdf))

            db.ExecuteSP("pActualizarEstatusPdfNominaEspecial", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ActualizarEstatusEnviadoNomina()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
            'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            p.Add(New SqlParameter("@pEjercicio", Ejercicio))
            p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
            p.Add(New SqlParameter("@pPeriodo", Periodo))
            p.Add(New SqlParameter("@pEnviado", Enviado))

            db.ExecuteSP("pActualizarEstatusEnviadoNomina", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarPercepcionesCorridaSemanal() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        dt = db.ExecuteSP("pConsultarPercepcionesCorridaSemanal", p)
        Return dt
    End Function
    Public Function ConsultarPercepcionesDeduccionesNominaEspecial() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pTipoConcepto", TipoConcepto))
        p.Add(New SqlParameter("@pTipo", Tipo))
        dt = db.ExecuteSP("pConsultarPercepcionesDeduccionesNominaEspecial", p)
        Return dt
    End Function
    Public Function ConsultarDeduccionesCorridaSemanal() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        dt = db.ExecuteSP("pConsultarDeduccionesCorridaSemanal", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosCorridaSemanal() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        dt = db.ExecuteSP("pConsultarEmpleadosCorridaSemanal", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosNominaEspecial() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pTipo", Tipo))
        dt = db.ExecuteSP("pConsultarEmpleadosNominaEspecial", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosCorridaCatorcenal() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        dt = db.ExecuteSP("pConsultarEmpleadosCorridaSemanal", p)
        Return dt

    End Function
    Public Function ConsultarEmpleadosTimbradosCorridaSemanal() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        dt = db.ExecuteSP("pConsultarEmpleadosTimbradosCorridaSemanal", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosTimbrados() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pTipo", Tipo))
        dt = db.ExecuteSP("pConsultarEmpleadosTimbrados", p)
        Return dt
    End Function
    Public Function ConsultarCentroCostosEmpleados() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pTipo", Tipo))
        dt = db.ExecuteSP("pConsultarCentroCostosEmpleados", p)
        Return dt
    End Function
    Public Function ConsultarDatosPDF() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        dt = db.ExecuteSP("pConsultarDatosPDF", p)
        Return dt
    End Function
    Public Function ConsultarDatosEnvioPDF() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pTipo", Tipo))
        dt = db.ExecuteSP("pConsultarDatosEnvioPDF", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosFiniquito(Optional ByVal busqueda As String = "") As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        If Id > 0 Then
            p.Add(New SqlParameter("@pId", Id))
        End If

        If busqueda.Length > 0 Then
            p.Add(New SqlParameter("@txtSearch", busqueda))
        End If

        db = New DBManager.DataBase(1)
        dt = db.ExecuteSP("pConsultarEmpleadosFiniquito", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosFiniquitoGenerado() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pTipo", Tipo))
        If IdContrato > 0 Then
            p.Add(New SqlParameter("@pIdContrato", IdContrato))
        End If
        dt = db.ExecuteSP("pConsultarEmpleadosFiniquitoGenerado", p)
        Return dt
    End Function
    Public Function ConsultarDesgloseFiniquito() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pId", Id))
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pDiasPagadosVacaciones", DiasPagadosVacaciones))

        dt = db.ExecuteSP("pConsultarDesgloseFiniquito", p)
        Return dt
    End Function
    Public Function ConsultarDesgloseFiniquitoGenerado() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pId", Id))
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pDiasPagadosVacaciones", DiasPagadosVacaciones))

        dt = db.ExecuteSP("pConsultarDesgloseFiniquitoGenerado", p)
        Return dt
    End Function
    Public Function ConsultarDesgloseAguinaldo() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))

        dt = db.ExecuteSP("pConsultarDesgloseAguinaldo", p)
        Return dt
    End Function
    Public Sub ActualizarEstatusFiniquitoGenerado()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pTipo", Tipo))
            p.Add(New SqlParameter("@pIdMovimiento", IdMovimiento))
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
            'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            p.Add(New SqlParameter("@pEjercicio", Ejercicio))
            p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
            p.Add(New SqlParameter("@pIdEstatus", IdEstatus))

            db.ExecuteSP("pActualizarEstatusFiniquitoGenerado", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarDatosEmpleadosGenerados() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pTipo", Tipo))
        p.Add(New SqlParameter("@pEspecial", EsEspecial))
        dt = db.ExecuteSP("pConsultarDatosEmpleadosGenerados", p)
        Return dt
    End Function
    Public Function ConsultarAntiguedad(ByVal FechaIngreso As DateTime) As String
        Dim Value As String
        p.Clear()
        p.Add(New SqlParameter("@pFechaIngreso", FechaIngreso))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        Value = db.ExecuteNonQueryScalarString("pConsultarAntiguedad", p)
        Return Value
    End Function
    Public Function ConsultarAntiguedadNominaEspecial(ByVal FechaIngreso As DateTime) As String
        Dim Value As String
        p.Clear()
        p.Add(New SqlParameter("@pFechaIngreso", FechaIngreso))
        Value = db.ExecuteNonQueryScalarString("pConsultarAntiguedadNominaEspecial", p)
        Return Value
    End Function
    Public Function AsignaSerieFolio() As DataTable
        Try
            p.Clear()
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
            'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            p.Add(New SqlParameter("@pEjercicio", Ejercicio))
            p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
            p.Add(New SqlParameter("@pPeriodo", Periodo))
            p.Add(New SqlParameter("@pTipo", Tipo))
            dt = db.ExecuteSP("pAsignaSerieFolio", p)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function ConsultarEmpleadosActivos() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        dt = db.ExecuteSP("pConsultarEmpleadosActivos", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosActivosPTU() As DataTable
        p.Clear()
        dt = db.ExecuteSP("pConsultarEmpleadosActivosPTU", p)
        Return dt
    End Function
    Public Function ConsultarDetalleNominaPTU() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        dt = db.ExecuteSP("pConsultarDetalleNominaPTU", p)
        Return dt
    End Function
    Public Function ConsultarDetalleNominaAguinaldo() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        dt = db.ExecuteSP("pConsultarDetalleNominaAguinaldo", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosNoGeneradosNominaEspecial() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pTipo", Tipo))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        dt = db.ExecuteSP("pConsultarEmpleadosNoGeneradosNominaEspecial", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosGeneradosNominaEspecial() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pTipo", Tipo))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        If NoEmpleado > 0 Then
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        End If
        dt = db.ExecuteSP("pConsultarEmpleadosGeneradosNominaEspecial", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosGeneradosFiniquito() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipo", Tipo))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        If NoEmpleado > 0 Then
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        End If
        dt = db.ExecuteSP("pConsultarEmpleadosGeneradosFiniquito", p)
        Return dt
    End Function
    Public Sub ActualizarEstatusGeneradoNominaEspecial()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
            'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            p.Add(New SqlParameter("@pEjercicio", Ejercicio))
            p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
            p.Add(New SqlParameter("@pPeriodo", Periodo))
            p.Add(New SqlParameter("@pTipo", Tipo))
            p.Add(New SqlParameter("@pGenerado", Generado))

            db.ExecuteSP("pActualizarEstatusGeneradoNominaEspecial", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarDatosNominaEspecial() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pTipo", Tipo))
        dt = db.ExecuteSP("pConsultarDatosNominaEspecial", p)
        Return dt
    End Function
    Public Function ConsultarDatosEnvioUtilidadesPDF() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pTipo", Tipo))
        dt = db.ExecuteSP("pConsultarDatosEnvioUtilidadesPDF", p)
        Return dt
    End Function
    Public Sub ActualizarEstatusEnviadoNominaPTU()
        p.Clear()
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pTipo", Tipo))
        p.Add(New SqlParameter("@pEnviado", Enviado))
        db.ExecuteSP("pActualizarEstatusEnviadoNominaPTU", p)
    End Sub
    Public Sub ActualizarEstatusEnviadoNominaEspecial()
        p.Clear()
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pTipo", Tipo))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pEnviado", Enviado))
        db.ExecuteSP("pActualizarEstatusEnviadoNominaEspecial", p)
    End Sub
    Public Sub EliminaEmpleadoPTU()
        p.Clear()
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pTipo", Tipo))
        dt = db.ExecuteSP("pEliminaEmpleadoNominaPTU", p)
    End Sub
    Public Function ConsultarAcumulados() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pMes", Mes))
        p.Add(New SqlParameter("@pRegistroPatronal", IdRegistroPatronal))
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pTipoConcepto", TipoConcepto))
        dt = db.ExecuteSP("pConsultarAcumulados", p)
        Return dt
    End Function
    Public Function ConsultarAcumuladosEmpleado() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pFechaIni", FechaIni))
        p.Add(New SqlParameter("@pFechaFin", FechaFin))
        p.Add(New SqlParameter("@pRegistroPatronal", IdRegistroPatronal))
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        dt = db.ExecuteSP("pConsultarAcumuladosEmpleado", p)

        Return dt

    End Function
    Public Function ReportePercepciones() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        dt = db.ExecuteSP("pReportePercepciones", p)

        Return dt

    End Function
    Public Function ReporteDeducciones() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        dt = db.ExecuteSP("pReporteDeducciones", p)

        Return dt

    End Function
    Public Function ReporteAcumuladosEmpleado() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pAnnio", Ejercicio))
        p.Add(New SqlParameter("@pMes1", Mes1))
        p.Add(New SqlParameter("@pMes2", Mes2))
        dt = db.ExecuteSP("pConsultarAcumuladosEmpleado", p)
        Return dt
    End Function
    Public Function ReporteDiasLaborados() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pAnnio", Ejercicio))
        dt = db.ExecuteSP("pConsultarDiasLaborados", p)
        Return dt
    End Function
    Public Function ConsultarResumenNominas() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", cmd))
        p.Add(New SqlParameter("@Ejercicio", Ejercicio))
        p.Add(New SqlParameter("@TipoNomina", TipoNomina))
        p.Add(New SqlParameter("@Periodo", Periodo))
        p.Add(New SqlParameter("@IdEmpresa", IdEmpresa))
        dt = db.ExecuteSP("pConsultarResumenNominas", p)
        Return dt
    End Function


End Class