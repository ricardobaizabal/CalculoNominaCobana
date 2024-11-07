Imports System.Data.SqlClient
Partial Public Class TarifaMensual
    Dim db As New DBManager.DataBase()
    Dim p As New ArrayList
    Dim dt As New DataTable
    Public Sub GuadarTarifaMensual()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pLimiteInferior", LimiteInferior))
            p.Add(New SqlParameter("@pLimiteSuperior", LimiteSuperior))
            p.Add(New SqlParameter("@pCuotaFija", CuotaFija))
            p.Add(New SqlParameter("@pPorcSobreExcli", PorcSobreExcli))

            db.ExecuteSPWithParams("pGuadarTarifaMensual", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub UpdateTarifaMensual()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdTarifa", IdTarifa))
            p.Add(New SqlParameter("@pLimiteInferior", LimiteInferior))
            p.Add(New SqlParameter("@pLimiteSuperior", LimiteSuperior))
            p.Add(New SqlParameter("@pCuotaFija", CuotaFija))
            p.Add(New SqlParameter("@pPorcSobreExcli", PorcSobreExcli))

            db.ExecuteSPWithParams("pUpdateTarifaMensual", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarTarifaMensual() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pImportePeriodo", ImporteMensual))
        dt = db.ExecuteSP("pConsultarValoresTarifaMensual", p)
        Return dt
    End Function
    Public Sub ConsultarTarifaMensualID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdTarifa", IdTarifa))

            dt = db.ExecuteSP("pConsultarTarifaMensualID", p)

            If dt.Rows.Count > 0 Then
                LimiteInferior = dt.Rows(0).Item("LimiteInferior")
                LimiteSuperior = dt.Rows(0).Item("LimiteSuperior")
                CuotaFija = dt.Rows(0).Item("CuotaFija")
                PorcSobreExcli = dt.Rows(0).Item("PorcSobreExcli")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
