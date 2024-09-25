Imports System.Data.SqlClient
Partial Public Class TablaSubsidioSemanal
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Function ConsultarSubsidio() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pImporteSemanal", ImporteSemanal))
        dt = db.ExecuteSP("pConsultarSubsidioSemanal", p)
        Return dt
    End Function

End Class
