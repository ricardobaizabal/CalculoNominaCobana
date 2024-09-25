Imports System.Data.SqlClient

Public Class Deducciones
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Function ConsultarDeducciones() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pTipo", "D"))
        dt = db.ExecuteSP("pConsultarConcepto", p)
        Return dt
    End Function

    Public Sub GuadarDeducciones(Concepto As String, Clave As String, Tipo As String, CvoSAT As Integer, DescSAT As String)
        Try
            p.Clear()
            db.ExecuteQuery("INSERT INTO tblConceptos(Concepto, Clave, Tipo, CvoSAT, DescripcionSAT, ClaveSATid) VALUES('" & Concepto & "','" & Clave & "','" & Tipo & "','" & CvoSAT & "','" & DescSAT & "','" & CvoSAT & "')")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub EliminarDeducciones(Cvo As Integer)
        p.Clear()
        db.ExecuteQuery("DELETE tblConceptos WHERE Cvo=" & Cvo)
    End Sub

    Public Sub UpdateDeducciones(Cvo As Integer, Concepto As String, Clave As String, Tipo As String, CvoSAT As Integer, DescSAT As String)
        Try

            db.ExecuteQuery("UPDATE tblConceptos SET Concepto='" & Concepto & "', Clave='" & Clave & "', Tipo='" & Tipo & "', ClaveSATid='" & CvoSAT & "', DescripcionSAT='" & DescSAT & "' WHERE Cvo= '" & Cvo & "'")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function ConsultarDeduccionID(Cvo As Integer) As ArrayList
        Dim dtDeduccion As New ArrayList
        Try
            p.Clear()
            p.Add(New SqlParameter("@pCvo", Cvo))
            dt = db.ExecuteQuery("SELECT Cvo, Concepto, Clave, Tipo, CvoSAT, DescripcionSAT, ClaveSATid FROM tblConceptos WHERE Cvo = " & Cvo.ToString)
            If dt.Rows.Count > 0 Then
                dtDeduccion.Add(dt.Rows(0).Item("Cvo"))
                dtDeduccion.Add(dt.Rows(0).Item("Concepto"))
                dtDeduccion.Add(dt.Rows(0).Item("Clave"))
                dtDeduccion.Add(dt.Rows(0).Item("TipoId"))
                dtDeduccion.Add(dt.Rows(0).Item("CvoSAT"))
                dtDeduccion.Add(dt.Rows(0).Item("ClaveSATid"))
            End If
        Catch ex As Exception
            Throw ex
        End Try

        Return dtDeduccion
    End Function

End Class
