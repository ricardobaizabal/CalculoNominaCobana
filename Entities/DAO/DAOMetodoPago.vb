Imports System.Data.SqlClient
Partial Public Class MetodoPago
    Dim db As New DBManager.DataBase()
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Function ConsultarMetodoPago() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 53))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
End Class
