Imports System.Data.SqlClient
Partial Public Class TarifaSemanal
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Function ConsultarTarifa() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pImporteSemanal", ImporteSemanal))
        dt = db.ExecuteSP("pConsultarValoresTarifaSemanal", p)
        Return dt
    End Function

End Class
