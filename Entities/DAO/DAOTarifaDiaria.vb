Imports System.Data.SqlClient
Partial Public Class TarifaDiaria
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable
    Public Sub GuadarTarifaDiaria()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pLimiteInferior", LimiteInferior))
            p.Add(New SqlParameter("@pLimiteSuperior", LimiteSuperior))
            p.Add(New SqlParameter("@pCuotaFija", CuotaFija))
            p.Add(New SqlParameter("@pPorcSobreExcli", PorcSobreExcli))

            db.ExecuteSPWithParams("pGuadarTarifaDiaria", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub UpdateTarifaDiaria()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdTarifa", IdTarifa))
            p.Add(New SqlParameter("@pLimiteInferior", LimiteInferior))
            p.Add(New SqlParameter("@pLimiteSuperior", LimiteSuperior))
            p.Add(New SqlParameter("@pCuotaFija", CuotaFija))
            p.Add(New SqlParameter("@pPorcSobreExcli", PorcSobreExcli))

            db.ExecuteSPWithParams("pUpdateTarifaDiaria", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarValoresTarifaDiaria() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pImporteDiario", ImporteDiario))
        dt = db.ExecuteSP("pConsultarValoresTarifaDiaria", p)
        Return dt
    End Function
    Public Function ConsultarTarifaDiaria() As DataTable
        p.Clear()
        dt = db.ExecuteSP("pConsultarTarifaDiaria", p)
        Return dt
    End Function
    Public Sub ConsultarTarifaDiariaID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdTarifa", IdTarifa))

            dt = db.ExecuteSP("pConsultarTarifaDiariaID", p)

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