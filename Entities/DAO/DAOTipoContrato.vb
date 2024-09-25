Imports System.Data.SqlClient
Partial Public Class TipoContrato
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable
    Public Sub GuadarTipoContrato()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pDescripcion", Descripcion))

            db.ExecuteSPWithParams("pGuadarTipoContrato", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarTipoContrato() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 48))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Sub EliminarTipoContrato()
        p.Clear()
        p.Add(New SqlParameter("@pIdTipoContrato", IdTipoContrato))
        db.ExecuteSPWithParams("pDeleteTipoContrato", p)
    End Sub
    Public Sub UpdateTipoContrato()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdTipoContrato", IdTipoContrato))
            p.Add(New SqlParameter("@pDescripcion", Descripcion))

            db.ExecuteSPWithParams("pUpdateTipoContrato", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ConsultarTipoContratoID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdTipoContrato", IdTipoContrato))

            dt = db.ExecuteSP("pConsultarTipoContratoID", p)

            If dt.Rows.Count > 0 Then
                IdTipoContrato = dt.Rows(0).Item("IdTipoContrato")
                Descripcion = dt.Rows(0).Item("Descripcion")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
