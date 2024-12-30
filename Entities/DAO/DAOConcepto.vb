Imports System.Data.SqlClient
Partial Public Class Concepto
    Dim db As New DBManager.DataBase()
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Sub GuadarConcepto()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pConcepto", Concepto))
            p.Add(New SqlParameter("@pClave", Clave))
            p.Add(New SqlParameter("@pTipo", Tipo))
            p.Add(New SqlParameter("@CvoSAT", CvoSAT))

            db.ExecuteSPWithParams("pGuadarConcepto", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarConcepto() As DataTable
        p.Clear()
        If Tipo.Length > 0 Then
            p.Add(New SqlParameter("@pTipo", Tipo))
        End If
        dt = db.ExecuteSP("pConsultarConcepto", p)
        Return dt
    End Function
    Public Function ConsultarConceptoPercepcionesFiniquito() As DataTable
        p.Clear()
        If Tipo.Length > 0 Then
            p.Add(New SqlParameter("@pTipo", Tipo))
        End If
        dt = db.ExecuteSP("pConsultarConceptoPercepcionesFiniquito", p)
        Return dt
    End Function
    Public Function ConsultarConceptoDeduccionesFiniquito() As DataTable
        p.Clear()
        If Tipo.Length > 0 Then
            p.Add(New SqlParameter("@pTipo", Tipo))
        End If
        dt = db.ExecuteSP("pConsultarConceptoDeduccionesFiniquito", p)
        Return dt
    End Function
    Public Function ConsultarConceptosIncidencia() As DataTable
        p.Clear()
        If Tipo.Length > 0 Then
            p.Add(New SqlParameter("@pTipo", Tipo))
        End If
        dt = db.ExecuteSP("pConsultarConceptosIncidencia", p)
        Return dt
    End Function

    Public Function ConsultarConceptosCliente() As DataTable
        p.Clear()
        If Tipo.Length > 0 Then
            p.Add(New SqlParameter("@pTipo", Tipo))
        End If
        dt = db.ExecuteSP("pConsultarConceptosCliente", p)
        Return dt
    End Function



    Public Function ConsultarConceptosComunes() As DataTable
        p.Clear()
        dt = db.ExecuteSP("pConsultarConceptosComunes", p)
        Return dt
    End Function
    Public Sub EliminarConcepto()
        p.Clear()
        p.Add(New SqlParameter("@pCvo", Cvo))
        db.ExecuteSPWithParams("pDeleteConcepto", p)
    End Sub
    Public Sub UpdateConcepto()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pCvo", Cvo))
            p.Add(New SqlParameter("@pConcepto", Concepto))
            p.Add(New SqlParameter("@pClave", Clave))
            p.Add(New SqlParameter("@pTipoId", Tipo))
            p.Add(New SqlParameter("@pCvoSAT", CvoSAT))

            db.ExecuteSPWithParams("pUpdateConcepto", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ConsultarConceptoID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pCvo", Cvo))

            dt = db.ExecuteSP("pConsultarConceptoID", p)

            If dt.Rows.Count > 0 Then
                Cvo = dt.Rows(0).Item("Cvo")
                Concepto = dt.Rows(0).Item("Concepto")
                Clave = dt.Rows(0).Item("Clave")
                Tipo = dt.Rows(0).Item("TipoId")
                CvoSAT = dt.Rows(0).Item("CvoSAT")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarTipoConceptos() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 56))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Function ConsultarTipos() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 1))
        dt = db.ExecuteSP("pCatalogos", p)
        Return dt
    End Function
End Class
