﻿Imports System.Data.SqlClient
Partial Public Class Puesto
    Dim db As New DBManager.DataBase()
    Dim p As New ArrayList
    Dim dt As New DataTable
    Public Sub GuadarPuesto()
        Try
            p.Clear()
            p.Add(New SqlParameter("@clienteid", clienteid))
            p.Add(New SqlParameter("@nombre", Descripcion))
            db.ExecuteSPWithParams("pGuadarPuesto", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarPuesto() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 74))
        p.Add(New SqlParameter("@clienteid", clienteid))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Sub EliminarPuesto()
        p.Clear()
        p.Add(New SqlParameter("@id", IdPuesto))
        db.ExecuteSPWithParams("pDeletePuesto", p)
    End Sub
    Public Sub UpdatePuesto()
        Try
            p.Clear()
            p.Add(New SqlParameter("@clienteid", clienteid))
            p.Add(New SqlParameter("@id", IdPuesto))
            p.Add(New SqlParameter("@nombre", Descripcion))
            db.ExecuteSPWithParams("pUpdatePuesto", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ConsultarPuestoID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@id", IdPuesto))

            dt = db.ExecuteSP("pConsultarPuestoID", p)

            If dt.Rows.Count > 0 Then
                clienteid = dt.Rows(0).Item("clienteid")
                IdPuesto = dt.Rows(0).Item("id")
                Descripcion = dt.Rows(0).Item("nombre")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
