Imports System.Data.SqlClient
Partial Public Class TipoHorasExtra
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Function ConsultarTipoHorasExtra() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 57))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function

End Class
