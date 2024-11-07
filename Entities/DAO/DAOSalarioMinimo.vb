Imports System
Imports System.Text
Imports System.Security.Cryptography
Imports System.Data
Imports System.Data.SqlClient
Partial Public Class SalarioMinimo
    Dim db As New DBManager.DataBase()
    Dim p As New ArrayList
    Dim dt As New DataTable
    Dim ds As New DataSet

    Public Sub ActualizarSalarioMinimo(ByVal Mes As Int32)
        p.Clear()
        Select Case Mes
            Case 1
                p.Add(New SqlParameter("@Enero_SM", Enero_SM))
            Case 2
                p.Add(New SqlParameter("@Febrero_SM", Febrero_SM))
            Case 3
                p.Add(New SqlParameter("@Marzo_SM", Marzo_SM))
            Case 4
                p.Add(New SqlParameter("@Abril_SM", Abril_SM))
            Case 5
                p.Add(New SqlParameter("@Mayo_SM", Mayo_SM))
            Case 6
                p.Add(New SqlParameter("@Junio_SM", Junio_SM))
            Case 7
                p.Add(New SqlParameter("@Julio_SM", Julio_SM))
            Case 8
                p.Add(New SqlParameter("@Agosto_SM", Agosto_SM))
            Case 9
                p.Add(New SqlParameter("@Septiembre_SM", Septiembre_SM))
            Case 10
                p.Add(New SqlParameter("@Octubre_SM", Octubre_SM))
            Case 11
                p.Add(New SqlParameter("@Noviembre_SM", Noviembre_SM))
            Case 12
                p.Add(New SqlParameter("@Diciembre_SM", Diciembre_SM))
            Case 13
                p.Add(New SqlParameter("@Enero_SMF", Enero_SMF))
            Case 14
                p.Add(New SqlParameter("@Febrero_SMF", Febrero_SMF))
            Case 15
                p.Add(New SqlParameter("@Marzo_SMF", Marzo_SMF))
            Case 16
                p.Add(New SqlParameter("@Abril_SMF", Abril_SMF))
            Case 17
                p.Add(New SqlParameter("@Mayo_SMF", Mayo_SMF))
            Case 18
                p.Add(New SqlParameter("@Junio_SMF", Junio_SMF))
            Case 19
                p.Add(New SqlParameter("@Julio_SMF", Julio_SMF))
            Case 20
                p.Add(New SqlParameter("@Agosto_SMF", Agosto_SMF))
            Case 21
                p.Add(New SqlParameter("@Septiembre_SMF", Septiembre_SMF))
            Case 22
                p.Add(New SqlParameter("@Octubre_SMF", Octubre_SMF))
            Case 23
                p.Add(New SqlParameter("@Noviembre_SMF", Noviembre_SMF))
            Case 24
                p.Add(New SqlParameter("@Diciembre_SMF", Diciembre_SMF))
        End Select

        p.Add(New SqlParameter("@IdSalarioMinimo", id))
        db.ExecuteSPWithParams("pSalarioMinimo_Actualizar", p)
    End Sub

End Class
