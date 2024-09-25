Imports System.Data.SqlClient
Partial Public Class RegimenContratacion
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable
    Public Sub GuadarRegimenContratacion()
        Try
            p.Clear()
            'p.Add(New SqlParameter("@pIdRegimenContratacion", IdRegimenContratacion))
            p.Add(New SqlParameter("@pDescripcion", Descripcion))

            db.ExecuteSPWithParams("pSaveRegimen", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarRegimen() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 51))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Function MostrarRegimen() As DataTable
        p.Clear()
        dt = db.ExecuteSP("pConsultarRegimen", p)

        Return dt
    End Function
    Public Sub EliminarRegimen()
        p.Clear()
        p.Add(New SqlParameter("@pIdRegimenContratacion", IdRegimenContratacion))
        db.ExecuteSPWithParams("pRegimenEliminar", p)
    End Sub
    Public Sub UpdateRegimenContratacion()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdRegimenContratacion", IdRegimenContratacion))
            p.Add(New SqlParameter("@pDescripcion", Descripcion))

            db.ExecuteSPWithParams("pRegimenUpdate", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ConsultarRegimenID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdRegimenContratacion", IdRegimenContratacion))

            dt = db.ExecuteSP("pConsultarRegimenID", p)

            If dt.Rows.Count > 0 Then
                IdRegimenContratacion = dt.Rows(0).Item("IdRegimenContratacion")
                Descripcion = dt.Rows(0).Item("Descripcion")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
