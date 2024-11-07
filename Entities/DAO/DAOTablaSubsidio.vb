Imports System.Data.SqlClient
Partial Public Class TablaSubsidioDiario
    Dim db As New DBManager.DataBase()
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Sub GuadarSubsidioMensual()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pLimiteInferior", LimiteInferior))
            p.Add(New SqlParameter("@pLimiteSuperior", LimiteSuperior))
            p.Add(New SqlParameter("@pCuotaFija", CuotaFija))

            db.ExecuteSPWithParams("pGuadarTablaSubsidioMensual", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub UpdateSubsidioMensual()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdTablaSubsidio", IdTablaSubsidio))
            p.Add(New SqlParameter("@pLimiteInferior", LimiteInferior))
            p.Add(New SqlParameter("@pLimiteSuperior", LimiteSuperior))
            p.Add(New SqlParameter("@pCuotaFija", CuotaFija))

            db.ExecuteSPWithParams("pUpdateTablaSubsidioMensual", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarSubsidioMensual() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pImporte", Importe))
        dt = db.ExecuteSP("pConsultarTablaSubsidioMensual", p)
        Return dt
    End Function
    Public Sub ConsultarSubsidioMensualID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdTablaSubsidio", IdTablaSubsidio))

            dt = db.ExecuteSP("pConsultarTablaSubsidioMensualID", p)

            If dt.Rows.Count > 0 Then
                LimiteInferior = dt.Rows(0).Item("LimiteInferior")
                LimiteSuperior = dt.Rows(0).Item("LimiteSuperior")
                CuotaFija = dt.Rows(0).Item("Subsidio")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GuadarSubsidioDiario()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pLimiteInferior", LimiteInferior))
            p.Add(New SqlParameter("@pLimiteSuperior", LimiteSuperior))
            p.Add(New SqlParameter("@pCuotaFija", CuotaFija))

            db.ExecuteSPWithParams("pGuadarTablaSubsidioDiario", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub UpdateSubsidioDiario()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdTablaSubsidio", IdTablaSubsidio))
            p.Add(New SqlParameter("@pLimiteInferior", LimiteInferior))
            p.Add(New SqlParameter("@pLimiteSuperior", LimiteSuperior))
            p.Add(New SqlParameter("@pCuotaFija", CuotaFija))

            db.ExecuteSPWithParams("pUpdateTablaSubsidioDiario", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarTablaSubsidioDiario() As DataTable
        p.Clear()
        dt = db.ExecuteSP("pConsultarTablaSubsidioDiario", p)
        Return dt
    End Function
    Public Function ConsultarSubsidioDiario() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pImporteDiario", Importe))
        dt = db.ExecuteSP("pConsultarSubsidioDiario", p)
        Return dt
    End Function
    Public Sub ConsultarSubsidioDiarioID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdTablaSubsidio", IdTablaSubsidio))

            dt = db.ExecuteSP("pConsultarTablaSubsidioDiarioID", p)

            If dt.Rows.Count > 0 Then
                LimiteInferior = dt.Rows(0).Item("LimiteInferior")
                LimiteSuperior = dt.Rows(0).Item("LimiteSuperior")
                CuotaFija = dt.Rows(0).Item("Subsidio")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class