Imports System.Data.SqlClient
Partial Public Class Infonavit
    Dim db As New DBManager.DataBase()
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Function ConsultarEmpleadosConDescuentoInfonavit() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pIdCliente", IdCliente))
        p.Add(New SqlParameter("@pIdEmpleado", IdEmpleado))
        p.Add(New SqlParameter("@pIdPeriodo", IdPeriodo))
        dt = db.ExecuteSP("pConsultarEmpleadosConDescuentoInfonavit", p)
        Return dt
    End Function

End Class