Imports System.Data.SqlClient
Partial Public Class DispersionNomina
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Function AgregaFolio() As Integer
        Dim Folio As Integer = 0
        Try
            p.Clear()
            p.Add(New SqlParameter("@cmd", 1))
            p.Add(New SqlParameter("@clave_servicio", clave_servicio))
            p.Add(New SqlParameter("@total_registros_enviados", total_registros_enviados))
            p.Add(New SqlParameter("@importe_total_registros_enviados", importe_total_registros_enviados))
            p.Add(New SqlParameter("@accion", accion))
            p.Add(New SqlParameter("@periodoid", periodoid))
            p.Add(New SqlParameter("@empresaid", empresaid))
            p.Add(New SqlParameter("@ejercicio", ejercicio))
            p.Add(New SqlParameter("@tipo_nomina", tipo_nomina))
            p.Add(New SqlParameter("@tipo", tipo))
            p.Add(New SqlParameter("@error", mensaje_error))
            p.Add(New SqlParameter("@bancoid", bancoid))

            Folio = db.ExecuteNonQueryScalar("pDispersionNomina", p)

        Catch ex As Exception
            Throw ex
        End Try

        Return Folio

    End Function

    Public Function ConsultarDispersionNomina() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 2))
        p.Add(New SqlParameter("@id", id))
        dt = db.ExecuteSP("pDispersionNomina", p)

        Return dt

    End Function

End Class