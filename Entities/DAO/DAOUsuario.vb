Imports System.Data.SqlClient
Partial Public Class Usuario
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Sub InicioSesion()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pEmail", Email))
            p.Add(New SqlParameter("@pContrasena", Contrasena))

            If IdPerfil > 0 Then
                p.Add(New SqlParameter("@pIdPerfil", IdPerfil))
            End If

            dt = db.ExecuteSP("pInicioSesion", p)

            If dt.Rows.Count > 0 Then
                IdUsuario = dt.Rows(0).Item("IdUsuario")
                Nombre = dt.Rows(0).Item("Nombre")
                IdPerfil = dt.Rows(0).Item("IdPerfil")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function getSHA1Hash(ByVal strToHash As String) As String

        Dim sha1Obj As New System.Security.Cryptography.SHA1CryptoServiceProvider

        Dim bytesToHash() As Byte = System.Text.Encoding.ASCII.GetBytes(strToHash)

        bytesToHash = sha1Obj.ComputeHash(bytesToHash)

        Dim strResult As String = ""

        For Each b As Byte In bytesToHash
            strResult += b.ToString("x2")
        Next

        Return strResult

    End Function

End Class
