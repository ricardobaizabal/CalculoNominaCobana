Imports System.Data.SqlClient
Partial Public Class PrestamoPersonal
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Function ConsultarEmpleadosConPrestamoPersonal() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        dt = db.ExecuteSP("pConsultarEmpleadosConPrestamoPersonal", p)
        Return dt
    End Function

    Public Function ConsultarEmpleadosConPrestamoPersonalFiltro(IdEmpresa As Integer) As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        dt = db.ExecuteSP("pConsultarEmpleadosConPrestamoPersonal", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosConPrestamoPersonalNomina() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        dt = db.ExecuteSP("pConsultarEmpleadosConPrestamoPersonalNomina", p)
        Return dt
    End Function
    Public Function ConsultarPrestamosEmpleado() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        dt = db.ExecuteSP("pConsultarPrestamosEmpleado", p)
        Return dt
    End Function
    Public Sub AgregaPrestamoPersonalDetalle()
        p.Clear()
        p.Add(New SqlParameter("@pIdPrestamoPersonal", IdPrestamoPersonal))
        p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pCvoConcepto", CvoConcepto))
        p.Add(New SqlParameter("@pImporte", Importe))
        p.Add(New SqlParameter("@pSerie", Serie))
        p.Add(New SqlParameter("@pFolio", Folio))
        p.Add(New SqlParameter("@pUUID", UUID))
        db.ExecuteSP("pAgregaPrestamoPersonalDetalle", p)
    End Sub
    Public Sub ActualizarEmpleadosConPrestamoPersonalNomina()
        p.Clear()
        p.Add(New SqlParameter("@pIdPrestamoPersonal", IdPrestamoPersonal))
        p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        p.Add(New SqlParameter("@pEjercicio", Ejercicio))
        p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
        p.Add(New SqlParameter("@pPeriodo", Periodo))
        p.Add(New SqlParameter("@pImporte", Importe))
        p.Add(New SqlParameter("@pSerie", Serie))
        p.Add(New SqlParameter("@pFolio", Folio))
        p.Add(New SqlParameter("@pUUID", UUID))
        db.ExecuteSP("pActualizarEmpleadosConPrestamoPersonalNomina", p)
    End Sub

End Class