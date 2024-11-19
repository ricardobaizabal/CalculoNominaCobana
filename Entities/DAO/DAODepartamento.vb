Imports System.Data.SqlClient
Partial Public Class Departamento
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable
    Public Sub GuadarDepartamento()
        Try
            p.Clear()
            p.Add(New SqlParameter("@clienteid", clienteid))
            p.Add(New SqlParameter("@nombre", Descripcion))
            db.ExecuteSPWithParams("pGuadarDepartamento", p)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarDepartamento() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@clienteid", clienteid))
        dt = db.ExecuteSP("pBuscar_Departamento", p)
        Return dt
    End Function
    Public Sub EliminarDepartamento()
        p.Clear()
        p.Add(New SqlParameter("@id", IdDepartamento))
        db.ExecuteSPWithParams("pDeleteDepartamento", p)
    End Sub
    Public Sub UpdateDepartamento()
        Try
            p.Clear()
            p.Add(New SqlParameter("@clienteid", clienteid))
            p.Add(New SqlParameter("@id", IdDepartamento))
            p.Add(New SqlParameter("@nombre", Descripcion))
            db.ExecuteSPWithParams("pUpdateDepartamento", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ConsultarDepartamentoID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@id", IdDepartamento))
            dt = db.ExecuteSP("pConsultarDepartamentoID", p)

            If dt.Rows.Count > 0 Then
                clienteid = dt.Rows(0).Item("clienteid")
                IdDepartamento = dt.Rows(0).Item("id")
                Descripcion = dt.Rows(0).Item("nombre")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class