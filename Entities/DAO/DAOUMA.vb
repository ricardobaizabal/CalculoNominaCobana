Imports System
Imports System.Text
Imports System.Security.Cryptography
Imports System.Data
Imports System.Data.SqlClient
Partial Public Class UMA
    Dim db As New DBManager.DataBase()
    Dim p As New ArrayList
    Dim dt As New DataTable
    Dim ds As New DataSet

    Public Sub ActualizarUMA(ByVal Mes As Int32)
        p.Clear()

        Select Case Mes
            Case 1
                p.Add(New SqlParameter("@Enero", Enero))
            Case 2
                p.Add(New SqlParameter("@Febrero", Febrero))
            Case 3
                p.Add(New SqlParameter("@Marzo", Marzo))
            Case 4
                p.Add(New SqlParameter("@Abril", Abril))
            Case 5
                p.Add(New SqlParameter("@Mayo", Mayo))
            Case 6
                p.Add(New SqlParameter("@Junio", Junio))
            Case 7
                p.Add(New SqlParameter("@Julio", Julio))
            Case 8
                p.Add(New SqlParameter("@Agosto", Agosto))
            Case 9
                p.Add(New SqlParameter("@Septiembre", Septiembre))
            Case 10
                p.Add(New SqlParameter("@Octubre", Octubre))
            Case 11
                p.Add(New SqlParameter("@Noviembre", Noviembre))
            Case 12
                p.Add(New SqlParameter("@Diciembre", Diciembre))
            Case 13
                p.Add(New SqlParameter("@Mensual", Mensual))
            Case 14
                p.Add(New SqlParameter("@Anual", Anual))
        End Select

        p.Add(New SqlParameter("@IdUMA", id))
        db.ExecuteSPWithParams("pUMA_Actualizar", p)
    End Sub

    Public Sub ActualizarUMI(ByVal Mes As Int32)
        p.Clear()
        Select Case Mes
            Case 1
                p.Add(New SqlParameter("@Enero", Enero))
            Case 2
                p.Add(New SqlParameter("@Febrero", Febrero))
            Case 3
                p.Add(New SqlParameter("@Marzo", Marzo))
            Case 4
                p.Add(New SqlParameter("@Abril", Abril))
            Case 5
                p.Add(New SqlParameter("@Mayo", Mayo))
            Case 6
                p.Add(New SqlParameter("@Junio", Junio))
            Case 7
                p.Add(New SqlParameter("@Julio", Julio))
            Case 8
                p.Add(New SqlParameter("@Agosto", Agosto))
            Case 9
                p.Add(New SqlParameter("@Septiembre", Septiembre))
            Case 10
                p.Add(New SqlParameter("@Octubre", Octubre))
            Case 11
                p.Add(New SqlParameter("@Noviembre", Noviembre))
            Case 12
                p.Add(New SqlParameter("@Diciembre", Diciembre))
            Case 13
                p.Add(New SqlParameter("@Mensual", Mensual))
            Case 14
                p.Add(New SqlParameter("@Anual", Anual))
        End Select

        p.Add(New SqlParameter("@IdUMI", id))
        db.ExecuteSPWithParams("pUMI_Actualizar", p)
    End Sub

End Class