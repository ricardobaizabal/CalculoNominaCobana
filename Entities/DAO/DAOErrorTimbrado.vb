Imports System.Data.SqlClient
Partial Public Class ErrorTimbrado
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Sub GuadarError()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            p.Add(New SqlParameter("@pEjercicio", Ejercicio))
            p.Add(New SqlParameter("@pTipoNomina", TipoNomina))
            p.Add(New SqlParameter("@pPeriodo", Periodo))
            p.Add(New SqlParameter("@pNoEmpleado", NoEmpleado))
            p.Add(New SqlParameter("@pIdContrato", IdContrato))
            p.Add(New SqlParameter("@pDescripcion", Descripcion))
            p.Add(New SqlParameter("@pIdUsuario", IdUsuario))
            p.Add(New SqlParameter("@pTipo", Tipo))

            db.ExecuteSPWithParams("pGuardarErrorTimbrado", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
