Imports System.Data.SqlClient
Partial Public Class Ejercicio
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable
    Public Function ConsultarEjercicio() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 7))
        dt = db.ExecuteSP("pEjercicio", p)
        Return dt
    End Function
    Public Function ConsultarEjercicios() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 2))
        dt = db.ExecuteSP("pEjercicio", p)
        Return dt
    End Function
    Public Sub UpdateEjercicio()
        Try
            p.Clear()
            p.Add(New SqlParameter("@cmd", 4))
            p.Add(New SqlParameter("@id", IdEjercicio))
            p.Add(New SqlParameter("@ejercicio", Descripcion))

            db.ExecuteSPWithParams("pEjercicio", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GuadarEjercicio()
        Try
            p.Clear()
            p.Add(New SqlParameter("@cmd", 3))
            p.Add(New SqlParameter("@ejercicio", Descripcion))

            db.ExecuteSPWithParams("pEjercicio", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ModificarEjercicioActivado()
        Try
            p.Clear()
            p.Add(New SqlParameter("@cmd", 6))
            p.Add(New SqlParameter("@ejercicio", Descripcion))
            p.Add(New SqlParameter("@userid", IdUsuario))

            db.ExecuteSPWithParams("pEjercicio", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ConsultarEjercicioID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@cmd", 5))
            p.Add(New SqlParameter("@id", IdEjercicio))

            dt = db.ExecuteSP("pEjercicio", p)

            If dt.Rows.Count > 0 Then
                IdEjercicio = dt.Rows(0).Item("id")
                Descripcion = dt.Rows(0).Item("annio")
            Else
                IdEjercicio = 0
                Descripcion = ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class