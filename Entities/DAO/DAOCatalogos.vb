Imports System.Data.SqlClient
Partial Public Class Catalogos
    Dim db As New DBManager.DataBase(1)
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Function ConsultarEjercicio() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 58))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function

    Public Function ConsultarRegistroPatronal() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 59))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function

    Public Function ConsultarClientesNomina() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 60))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function

    Public Function ConsultarEstado() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 55))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Function ConsultarContribuyente() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 64))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Function ConsultarEdoCivil() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 7))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Function ConsultarBanco() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 52))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Function ConsultarFormaPago() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 53))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Function ConsultaMunicipio(estado As Integer) As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 47))
        p.Add(New SqlParameter("@estadoid", estado))
        dt = db.ExecuteSP("pCatalogo", p)

        Return dt
    End Function

    Public Function ConsultaTipoContrato() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 48))
        dt = db.ExecuteSP("pCatalogo", p)

        Return dt
    End Function
    Public Function ConsultarJornada() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 54))
        dt = db.ExecuteSP("pCatalogo", p)

        Return dt
    End Function
    Public Function ConsultarRegimenContratacion() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 51))
        dt = db.ExecuteSP("pCatalogo", p)

        Return dt
    End Function
    Public Function ConsultarPeriodoPago() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 49))
        dt = db.ExecuteSP("pCatalogo", p)

        Return dt
    End Function
    Public Function ConsultarCTipoContrato() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 65))
        dt = db.ExecuteSP("pCatalogo", p)

        Return dt
    End Function
    Public Function ConsultarCTipoJornada() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 66))
        dt = db.ExecuteSP("pCatalogo", p)

        Return dt
    End Function
    Public Function ConsultarCRegimenContratacion() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 67))
        dt = db.ExecuteSP("pCatalogo", p)

        Return dt
    End Function
    Public Function ConsultarParentesco() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 6))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Function ConsultarAnexo() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 68))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Function ConsultarPlaza(cliente As Integer) As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 69))
        p.Add(New SqlParameter("@clienteid", cliente))

        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Function ConsultarClienteid() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 70))


        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Function ConsultarEjecutivo() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 71))


        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Function ConsultarSalario() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 72))


        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function

    Public Function ConsultarDepartamento() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 73))
        'p.Add(New SqlParameter("@clienteid", cliente))

        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Function ConsultarPuesto() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 74))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function

    Public Function ConsultarRiesgoPuesto() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 75))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function

    Public Function ConsultarRiesgoPatronal2() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 76))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function

    Public Function ConsultarHorarios() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 77))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function

    Public Function ConsultarPeriodoPago2() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 78))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
    Public Function ConsultarCentrodeCosto(cliente As Integer) As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 38))
        p.Add(New SqlParameter("@clienteid", cliente))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function

    Public Function ConsultarTipoNomina() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 79))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function

    Public Function ConsultarTipoBaja() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 80))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function

    Public Function ConsultarMotivo() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 81))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function

    Public Function ConsultarGrupoPeriodoPago(cliente As Integer, periodopago As Integer) As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 82))
        p.Add(New SqlParameter("@clienteid", cliente))
        p.Add(New SqlParameter("@periodopagoid", periodopago))
        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function

    Public Function ConsultarGrupoPeriodoPago2(cliente As Integer) As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 83))
        p.Add(New SqlParameter("@clienteid", cliente))

        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function

    Public Function ConsultarMisClientes() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 84))

        dt = db.ExecuteSP("pCatalogo", p)
        Return dt
    End Function
End Class
