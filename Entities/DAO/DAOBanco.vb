Imports System.Data.SqlClient
Partial Public Class Banco
    Dim db As New DBManager.DataBase()
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Function ConsultarBanco() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 52))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Sub ConsultarBancoID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdRiesgoPuesto", IdBanco))

            dt = db.ExecuteSP("pConsultarRiesgoPuestoID", p)

            If dt.Rows.Count > 0 Then
                IdBanco = dt.Rows(0).Item("IdRiesgoPuesto")
                Nombre = dt.Rows(0).Item("Nombre")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
