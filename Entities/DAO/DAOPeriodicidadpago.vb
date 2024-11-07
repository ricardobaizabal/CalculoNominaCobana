Imports System.Data.SqlClient
Partial Public Class Periodicidadpago
    Dim db As New DBManager.DataBase()
    Dim p As New ArrayList
    Dim dt As New DataTable
    Public Sub GuadarPeriodicidadpago()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pDescripcion", Descripcion))

            db.ExecuteSPWithParams("pGuadarPeriodicidadpago", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarPeriodicidadpago() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 49))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Sub EliminarPeriodicidadpago()
        p.Clear()
        p.Add(New SqlParameter("@pIdPeriodicidad", IdPeriodicidad))
        db.ExecuteSPWithParams("pDeletePeriodicidadpago", p)
    End Sub
    Public Sub UpdatePeriodicidadpago()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdPeriodicidad", IdPeriodicidad))
            p.Add(New SqlParameter("@pDescripcion", Descripcion))

            db.ExecuteSPWithParams("pUpdatePeriodicidadpago", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ConsultarPeriodicidadpagoID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdPeriodicidad", IdPeriodicidad))

            dt = db.ExecuteSP("pConsultarPeriodicidadpagoID", p)

            If dt.Rows.Count > 0 Then
                IdPeriodicidad = dt.Rows(0).Item("IdPeriodicidad")
                Descripcion = dt.Rows(0).Item("Descripcion")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
