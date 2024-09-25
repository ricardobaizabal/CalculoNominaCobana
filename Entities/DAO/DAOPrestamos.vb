Imports System.Data.SqlClient
Partial Public Class Prestamos
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Function ConsultarEmpleadosConDescuentoInfonavit() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pIdEmpleado", IdEmpleado))
        dt = db.ExecuteSP("pConsultarEmpleadosConDescuentoPrestamo", p)
        Return dt
    End Function
End Class
