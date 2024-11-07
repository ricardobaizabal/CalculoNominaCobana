Imports System.Data.SqlClient
Partial Public Class RiesgoPuesto
    Dim db As New DBManager.DataBase()
    Dim p As New ArrayList
    Dim dt As New DataTable
    Public Sub GuadarRiesgoPuesto()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pNombre", Nombre))

            db.ExecuteSPWithParams("pGuadarRiesgoPuesto", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarRiesgoPuesto() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 50))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Sub EliminarRiesgoPuesto()
        p.Clear()
        p.Add(New SqlParameter("@pIdRiesgoPuesto", IdRiesgoPuesto))
        db.ExecuteSPWithParams("pDeleteRiesgoPuesto", p)
    End Sub
    Public Sub UpdateRiesgoPuesto()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdRiesgoPuesto", IdRiesgoPuesto))
            p.Add(New SqlParameter("@pNombre", Nombre))

            db.ExecuteSPWithParams("pUpdateRiesgoPuesto", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ConsultarRiesgoPuestoID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdRiesgoPuesto", IdRiesgoPuesto))

            dt = db.ExecuteSP("pConsultarRiesgoPuestoID", p)

            If dt.Rows.Count > 0 Then
                IdRiesgoPuesto = dt.Rows(0).Item("IdRiesgoPuesto")
                Nombre = dt.Rows(0).Item("Nombre")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
