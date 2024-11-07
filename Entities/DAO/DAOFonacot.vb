Imports System.Data.SqlClient
Partial Public Class Fonacot
    Dim db As New DBManager.DataBase()
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Function ConsultarEmpleadosConCreditoFonacot() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
        dt = db.ExecuteSP("pConsultarEmpleadosConCreditoFonacot", p)
        Return dt
    End Function
    Public Sub AgregaCreditoFonacotDetalle()
        p.Clear()
        p.Add(New SqlParameter("@pIdCreditoFonacot", IdCreditoFonacot))
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
        db.ExecuteSP("pAgregaCreditoFonacotDetalle", p)
    End Sub

End Class
