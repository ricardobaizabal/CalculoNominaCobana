Imports System.Data.SqlClient
Public Class Percepciones
    Dim db As New DBManager.DataBase()
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Function ConsultarPercepciones() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pTipo", "P"))
        dt = db.ExecuteSP("pConsultarConcepto", p)
        Return dt
    End Function

    Public Sub GuadarPercepcion(Concepto As String, Clave As String, Tipo As String, CvoSAT As Integer, DescSAT As String)
        Try
            p.Clear()
            db.ExecuteQuery("INSERT INTO tblConceptos(Concepto, Clave, Tipo, CvoSAT, DescripcionSAT, ClaveSATid) VALUES('" & Concepto & "','" & Clave & "','" & Tipo & "','" & CvoSAT & "','" & DescSAT & "','" & CvoSAT & "')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub EliminarPercepciones(Cvo As Integer)
        p.Clear()
        db.ExecuteQuery("DELETE tblConceptos WHERE Cvo=" & Cvo)
    End Sub

    Public Sub UpdatePercepcion(Cvo As Integer, Concepto As String, Clave As String, Tipo As String, CvoSAT As Integer, DescSAT As String)
        Try
            db.ExecuteQuery("UPDATE tblConceptos SET Concepto='" & Concepto & "', Clave='" & Clave & "', Tipo='" & Tipo & "', ClaveSATid='" & CvoSAT & "', DescripcionSAT='" & DescSAT & "' WHERE Cvo= '" & Cvo & "'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ConsultarPercepcionID(Cvo As Integer) As ArrayList
        Dim dtPercepcion As New ArrayList
        Try
            p.Clear()
            p.Add(New SqlParameter("@pCvo", Cvo))
            dt = db.ExecuteQuery("SELECT C.Cvo AS Cvo, C.Concepto As Concepto, C.Clave As Clave, C.Tipo As Tipo, C.CvoSAT As CvoSAT, C.DescripcionSAT, C.ClaveSATid As ClaveSATid, CSAT.Tipo AS TipoSAT FROM tblConceptos AS C LEFT JOIN tblClaveSAT CSAT ON CSAT.id = ClaveSATid WHERE C.Cvo = " & Cvo.ToString)
            If dt.Rows.Count > 0 Then
                dtPercepcion.Add(dt.Rows(0).Item("Cvo"))
                dtPercepcion.Add(dt.Rows(0).Item("Concepto"))
                dtPercepcion.Add(dt.Rows(0).Item("Clave"))
                dtPercepcion.Add(dt.Rows(0).Item("Tipo"))
                dtPercepcion.Add(dt.Rows(0).Item("CvoSAT"))
                dtPercepcion.Add(dt.Rows(0).Item("ClaveSATid"))
                dtPercepcion.Add(dt.Rows(0).Item("TipoSAT"))
            End If
        Catch ex As Exception
            Throw ex
        End Try

        Return dtPercepcion
    End Function

End Class
