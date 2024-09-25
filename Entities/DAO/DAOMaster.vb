Imports System.Data.SqlClient
Partial Public Class Master
    Dim db As New DBManager.DataBase(0)
    Dim p As New ArrayList
    Dim dt As New DataTable
    Public Sub ConsultarEmpresaID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@cmd", 1))
            dt = db.ExecuteSP("pCliente", p)

            If dt.Rows.Count > 0 Then
                IdEmpresa = dt.Rows(0).Item("id")
                Nombre = dt.Rows(0).Item("nombre_comercial")
                Activo = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
