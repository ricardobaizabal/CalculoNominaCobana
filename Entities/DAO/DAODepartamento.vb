Imports System.Data.SqlClient
Partial Public Class Departamento
    Dim db As New DBManager.DataBase()
    Dim p As New ArrayList
    Dim dt As New DataTable
    Public Sub GuadarDepartamento()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pDescripcion", Descripcion))

            db.ExecuteSPWithParams("pGuadarDepartamento", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarDepartamento() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@clienteid", clienteid))
        'dt = db.ExecuteSP("pConsultarDepartamento", p)
        dt = db.ExecuteSP("pBuscar_Departamento", p)
        Return dt
    End Function
    Public Sub EliminarDepartamento()
        p.Clear()
        p.Add(New SqlParameter("@pIdDepartamento", IdDepartamento))
        db.ExecuteSPWithParams("pDeleteDepartamento", p)
    End Sub
    Public Sub UpdateDepartamento()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdDepartamento", IdDepartamento))
            p.Add(New SqlParameter("@pDescripcion", Descripcion))

            db.ExecuteSPWithParams("pUpdateDepartamento", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ConsultarDepartamentoID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdDepartamento", IdDepartamento))

            dt = db.ExecuteSP("pConsultarDepartamentoID", p)

            If dt.Rows.Count > 0 Then
                IdDepartamento = dt.Rows(0).Item("IdDepartamento")
                Descripcion = dt.Rows(0).Item("Descripcion")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
