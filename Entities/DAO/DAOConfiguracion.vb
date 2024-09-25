Imports System.Data.SqlClient
Partial Public Class Configuracion
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Sub GuadarConfiguracion()
        Try
            p.Clear()
            p.Add(New SqlParameter("@cmd", 1))
            p.Add(New SqlParameter("@pIdUsuario", IdUsuario))
            'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))

            db.ExecuteSPWithParams("pConfiguracion", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ActualizaPeriodoNomina()
        Try
            p.Clear()
            p.Add(New SqlParameter("@cmd", 2))
            ' p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            p.Add(New SqlParameter("@pIdUsuario", IdUsuario))
            p.Add(New SqlParameter("@pIdPeriodo", IdPeriodo))

            db.ExecuteSPWithParams("pConfiguracion", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ActualizaSalarioMinimoDiarioGeneral()
        Try
            p.Clear()
            p.Add(New SqlParameter("@cmd", 4))
            'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            p.Add(New SqlParameter("@pIdUsuario", IdUsuario))
            p.Add(New SqlParameter("@pIdPeriodo", IdPeriodo))

            db.ExecuteSPWithParams("pConfiguracion", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ActualizaSalarioMinimoDiarioGeneralFiniquitos()
        Try
            p.Clear()
            p.Add(New SqlParameter("@cmd", 5))
            'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            p.Add(New SqlParameter("@pIdUsuario", IdUsuario))

            db.ExecuteSPWithParams("pConfiguracion", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ConsultarConfiguracion() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 3))
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pIdUsuario", IdUsuario))
        dt = db.ExecuteSP("pConfiguracion", p)
        Return dt
    End Function

    Public Function ConsultarDatosEnvioEmail() As DataTable
        p.Clear()
        db = New DBManager.DataBase(0)
        p.Add(New SqlParameter("@clienteid", IdCliente))
        dt = db.ExecuteSP("pDatosEmail", p)
        Return dt
    End Function

End Class
