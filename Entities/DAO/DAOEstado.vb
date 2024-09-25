Imports System.Data.SqlClient
Partial Public Class Estado
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Function ConsultarEstado() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 55))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
End Class
