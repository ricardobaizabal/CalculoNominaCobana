Imports System.Data.SqlClient
Partial Public Class Periodo
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable
    Public Sub GuadarPeriodoEspecial()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pEjercicio", IdEjercicio))
            p.Add(New SqlParameter("@pIdTiponomina", IdTipoNomina))
            p.Add(New SqlParameter("@pFechainicial", FechaInicialDate.ToString("yyyy-MM-dd 00:00:00")))
            p.Add(New SqlParameter("@pFechaFinal", FechaFinalDate.ToString("yyyy-MM-dd 00:00:00")))

            db.ExecuteSPWithParams("pGuadarPeriodoEspecial", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GuadarPeriodoSemanal()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pEjercicio", IdEjercicio))
            p.Add(New SqlParameter("@pIdTiponomina", IdTipoNomina))
            p.Add(New SqlParameter("@pFechainicial", FechaInicial))
            p.Add(New SqlParameter("@pGeneraPeriodos", GeneraPeriodos))

            db.ExecuteSPWithParams("pGuadarPeriodoSemanal", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GuadarPeriodoCatorcenal()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pEjercicio", IdEjercicio))
            p.Add(New SqlParameter("@pIdTiponomina", IdTipoNomina))
            p.Add(New SqlParameter("@pFechainicial", FechaInicial))
            p.Add(New SqlParameter("@pGeneraPeriodos", GeneraPeriodos))

            db.ExecuteSPWithParams("pGuadarPeriodoCatorcenal", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GuadarPeriodoQuincenal()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pEjercicio", IdEjercicio))
            p.Add(New SqlParameter("@pIdTiponomina", IdTipoNomina))
            p.Add(New SqlParameter("@pFechainicial", FechaInicial))
            p.Add(New SqlParameter("@pGeneraPeriodos", GeneraPeriodos))

            db.ExecuteSPWithParams("pGuadarPeriodoQuincenal", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub UpdatePeriodoQuincenal()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pEjercicio", IdEjercicio))
            p.Add(New SqlParameter("@pIdTiponomina", IdTipoNomina))
            p.Add(New SqlParameter("@pFechainicial", FechaInicial))
            p.Add(New SqlParameter("@pFechafinal", FechaFinal))
            p.Add(New SqlParameter("@pFechapago", FechaPago))
            p.Add(New SqlParameter("@pDias", Dias))
            p.Add(New SqlParameter("@pGeneraPeriodos", GeneraPeriodos))

            db.ExecuteSPWithParams("pGuadarPeriodoSemanal", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub UpdatePeriodoCatorcenal()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdPeriodo", IdPeriodo))
            p.Add(New SqlParameter("@pFechainicial", FechaInicial))
            p.Add(New SqlParameter("@pFechafinal", FechaFinal))

            db.ExecuteSPWithParams("pUpdatePeriodoCatorcenal", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub UpdatePeriodoSemanal()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdPeriodo", IdPeriodo))
            p.Add(New SqlParameter("@pNoPeriodo", NoPeriodo))
            p.Add(New SqlParameter("@pFechainicial", FechaInicial))
            p.Add(New SqlParameter("@pFechafinal", FechaFinal))
            If FechaPago.ToString.Length > 0 Then
                p.Add(New SqlParameter("@pFechaPago", FechaPago))
            End If

            db.ExecuteSPWithParams("pUpdatePeriodoSemanal", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ActualizaFechaPagoPeriodo()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdPeriodo", IdPeriodo))
            If FechaPago.ToString.Length > 0 Then
                p.Add(New SqlParameter("@pFechaPago", FechaPago))
            End If

            db.ExecuteSPWithParams("pActualizaFechaPagoPeriodo", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarPeriodo() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@IdEjercicio", IdEjercicio))
        p.Add(New SqlParameter("@pIdTipoNomina", IdTipoNomina))
        dt = db.ExecuteSP("pConsultarPeriodo", p)
        Return dt
    End Function
    Public Function ConsultarPeriodos() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@IdEjercicio", IdEjercicio))
        p.Add(New SqlParameter("@IdTipoNomina", IdTipoNomina))
        p.Add(New SqlParameter("@ExtraordinarioBit", ExtraordinarioBit))
        'If ExtraordinarioBit <> Nothing Then
        '    p.Add(New SqlParameter("@ExtraordinarioBit", ExtraordinarioBit))
        'End If
        dt = db.ExecuteSP("pConsultarPeriodos", p)
        Return dt
    End Function
    Public Function ConsultarPeriodosQuincenales() As DataTable
        p.Clear()
        dt = db.ExecuteSP("pConsultarPeriodosQuincenales", p)
        Return dt
    End Function
    Public Sub ConsultarPeriodoID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdPeriodo", IdPeriodo))

            dt = db.ExecuteSP("pConsultarPeriodoID", p)

            If dt.Rows.Count > 0 Then
                IdPeriodo = dt.Rows(0).Item("IdPeriodo")
                FechaInicial = dt.Rows(0).Item("Fechainicial")
                FechaFinal = dt.Rows(0).Item("Fechafinal")
                FechaPago = dt.Rows(0).Item("FechaPago")
                NoPeriodo = dt.Rows(0).Item("NoPeriodo")
                Dias = dt.Rows(0).Item("Dias")

                Dim formato As String = "dd/MM/yyyy"
                FechaInicialDate = Date.ParseExact(FechaInicial, formato, System.Globalization.CultureInfo.InvariantCulture)
                FechaFinalDate = Date.ParseExact(FechaFinal, formato, System.Globalization.CultureInfo.InvariantCulture)

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub EliminaPeriodo()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdPeriodo", IdPeriodo))

            db.ExecuteSPWithParams("pEliminarPeriodoSemanal", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class