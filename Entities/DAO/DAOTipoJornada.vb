Imports System.Data.SqlClient
Partial Public Class TipoJornada
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Function ConsultarTipoJornada() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 54))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
End Class
