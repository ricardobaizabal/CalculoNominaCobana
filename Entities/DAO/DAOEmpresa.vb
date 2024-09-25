Imports System.Data.SqlClient
Partial Public Class Empresa
    Dim db As New DBManager.DataBase(0)
    Dim db2 As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable
    Public Function ConsultarEmpresa() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 1))
        dt = db2.ExecuteSP("pMisClientes", p)
        Return dt
    End Function
    Public Function SearchEmpresa() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 11))
        p.Add(New SqlParameter("@txtSearch", Nombre))
        dt = db2.ExecuteSP("pMisClientes", p)
        Return dt
    End Function
    Public Sub ConsultarEmpresaID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@cmd", 2))
            p.Add(New SqlParameter("@clienteId", IdEmpresa))
            dt = db2.ExecuteSP("pMisClientes", p)

            If dt.Rows.Count > 0 Then
                IdEmpresa = dt.Rows(0).Item("id")
                Nombre = dt.Rows(0).Item("razonsocial")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ConsultarEjercicioID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@cmd", 1))
            'p.Add(New SqlParameter("@pEjercicio", IdEjercicio))
            'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            dt = db2.ExecuteSP("pConsultarEjercicioID", p)

            If dt.Rows.Count > 0 Then
                IdEjercicio = dt.Rows(0).Item("annio")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GuadarEjercicio()
        Try
            p.Clear()
            p.Add(New SqlParameter("@cmd", 2))
            p.Add(New SqlParameter("@pEjercicio", Ejercicio))

            db2.ExecuteSPWithParams("pConsultarEjercicioID", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ConsultarEmpresaID2()
        Try
            p.Clear()
            p.Add(New SqlParameter("@cmd", 5))
            p.Add(New SqlParameter("@clienteId", IdEmpresa))
            dt = db.ExecuteSP("pCliente", p)

            If dt.Rows.Count > 0 Then
                IdEmpresa = dt.Rows(0).Item("id")
                Nombre = dt.Rows(0).Item("razonsocial")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
