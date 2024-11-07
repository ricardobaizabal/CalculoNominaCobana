Imports System.Data.SqlClient
Partial Public Class Empleado
    Dim db As New DBManager.DataBase()
    Dim p As New ArrayList
    Dim dt As New DataTable

    Public Sub GuadarEmpleado()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pNombre", Nombre))
            p.Add(New SqlParameter("@pDomicilio", Domicilio))
            p.Add(New SqlParameter("@pColonia", Colonia))
            p.Add(New SqlParameter("@pCiudad", Ciudad))
            p.Add(New SqlParameter("@pIdEstado", IdEmpleado))
            p.Add(New SqlParameter("@pCp", Cp))
            p.Add(New SqlParameter("@pTelefono", Telefono))
            p.Add(New SqlParameter("@pFechaIngreso", FechaIngreso))
            p.Add(New SqlParameter("@pRfc", Rfc))
            p.Add(New SqlParameter("@pCurp", Curp))
            p.Add(New SqlParameter("@pCorreo", Correo))
            p.Add(New SqlParameter("@pActivo", Activo))
            p.Add(New SqlParameter("@pImss", Imss))
            p.Add(New SqlParameter("@pIdContrato", IdContrato))
            p.Add(New SqlParameter("@pIdPeriodicidadpago", IdPeriodicidadpago))
            p.Add(New SqlParameter("@pIdPuesto", IdPuesto))
            p.Add(New SqlParameter("@pIdDepartamento", IdDepartamento))
            p.Add(New SqlParameter("@pPais", Pais))
            p.Add(New SqlParameter("@pIdRiesgo", IdRiesgo))
            p.Add(New SqlParameter("@pIdTipoJornada", IdTipoJornada))
            p.Add(New SqlParameter("@pIdRegimencontratacion", IdRegimencontratacion))
            p.Add(New SqlParameter("@pIdBanco", IdBanco))
            p.Add(New SqlParameter("@pClabe", Clabe))
            p.Add(New SqlParameter("@pCuotaDiaria", CuotaDiaria))
            p.Add(New SqlParameter("@pFactorIntegracion", FactorIntegracion))
            p.Add(New SqlParameter("@pIntegradoImss", IntegradoImss))
            p.Add(New SqlParameter("@pDescansosxsemana", Descansosxsemana))
            p.Add(New SqlParameter("@pAsimiladototalsemanal", Asimiladototalsemanal))
            p.Add(New SqlParameter("@pPorcentajeimptocedularestatal", Porcentajeimptocedularestatal))
            p.Add(New SqlParameter("@pPagoxhora", PagoPorHora))
            p.Add(New SqlParameter("@pFactorComision", FactorComision))
            p.Add(New SqlParameter("@pPromedioCuotavariable", PromedioCuotavariable))
            p.Add(New SqlParameter("@pFactorDestajo", FactorDestajo))
            p.Add(New SqlParameter("@pIntegradoTipo", IntegradoTipo))
            p.Add(New SqlParameter("@pHoraSaldia", HoraSaldia))
            p.Add(New SqlParameter("@pPromedioPercepcionesvariables", PromedioPercepcionesvariables))

            db.ExecuteSPWithParams("pGuadarEmpleado", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function ConsultarEmpleado() As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pIdEmpleado", IdEmpleado))
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        dt = db.ExecuteSP("pConsultarEmpleado", p)
        Return dt
    End Function
    Public Function ConsultarEmpleados() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        p.Add(New SqlParameter("@pIdEmpleado", IdEmpleado))
        p.Add(New SqlParameter("@pIdMovimiento", IdMovimiento))
        dt = db.ExecuteSP("pConsultarEmpleados", p)
        Return dt
    End Function
    Public Function ConsultarEmpleadosFiltros(filtro As String, Optional ByVal miClienteid As Integer = 0) As DataTable
        p.Clear()
        p.Add(New SqlParameter("@cmd", 26))
        p.Add(New SqlParameter("@mi_cliente_id", miClienteid))
        p.Add(New SqlParameter("@txtSearch", filtro))
        dt = db.ExecuteSP("pPersonalAdministrado", p)
        Return dt
    End Function
    Public Function ConsultarInfonavit() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", miClienteid))
        dt = db.ExecuteSP("pConsultarEmpleadoInfonavit", p)
        Return dt
    End Function
    Public Function ConsultarInfonavitFiltro(pIdEmpresa As Integer) As DataTable
        p.Clear()
        p.Add(New SqlParameter("@pIdEmpresa", pIdEmpresa))
        dt = db.ExecuteSP("pConsultarEmpleadoInfonavit", p)
        Return dt
    End Function
    Public Function ConsultarPrestamos() As DataTable
        p.Clear()
        'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
        dt = db.ExecuteSP("pConsultarEmpleadoPrestamos", p)
        Return dt
    End Function
    Public Sub EliminarEmpleado()
        p.Clear()
        p.Add(New SqlParameter("@pIdEmpleado", IdEmpleado))
        db.ExecuteSPWithParams("pDeleteConcepto", p)
    End Sub
    Public Sub UpdateEmpleado()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdEmpleado", IdEmpleado))
            p.Add(New SqlParameter("@pNombre", Nombre))
            p.Add(New SqlParameter("@pDomicilio", Domicilio))
            p.Add(New SqlParameter("@pColonia", Colonia))
            p.Add(New SqlParameter("@pCiudad", Ciudad))
            p.Add(New SqlParameter("@pIdEstado", IdEmpleado))
            p.Add(New SqlParameter("@pCp", Cp))
            p.Add(New SqlParameter("@pTelefono", Telefono))
            p.Add(New SqlParameter("@pFechaIngreso", FechaIngreso))
            p.Add(New SqlParameter("@pRfc", Rfc))
            p.Add(New SqlParameter("@pCurp", Curp))
            p.Add(New SqlParameter("@pCorreo", Correo))
            p.Add(New SqlParameter("@pActivo", Activo))
            p.Add(New SqlParameter("@pImss", Imss))
            p.Add(New SqlParameter("@pIdContrato", IdContrato))
            p.Add(New SqlParameter("@pIdPeriodicidadpago", IdPeriodicidadpago))
            p.Add(New SqlParameter("@pIdPuesto", IdPuesto))
            p.Add(New SqlParameter("@pIdDepartamento", IdDepartamento))
            p.Add(New SqlParameter("@pPais", Pais))
            p.Add(New SqlParameter("@pIdRiesgo", IdRiesgo))
            p.Add(New SqlParameter("@pIdTipoJornada", IdTipoJornada))
            p.Add(New SqlParameter("@pIdRegimencontratacion", IdRegimencontratacion))
            p.Add(New SqlParameter("@pIdBanco", IdBanco))
            p.Add(New SqlParameter("@pClabe", Clabe))
            p.Add(New SqlParameter("@pCuotaDiaria", CuotaDiaria))
            p.Add(New SqlParameter("@pFactorIntegracion", FactorIntegracion))
            p.Add(New SqlParameter("@pIntegradoImss", IntegradoImss))
            p.Add(New SqlParameter("@pDescansosxsemana", Descansosxsemana))
            p.Add(New SqlParameter("@pAsimiladototalsemanal", Asimiladototalsemanal))
            p.Add(New SqlParameter("@pPorcentajeimptocedularestatal", Porcentajeimptocedularestatal))
            p.Add(New SqlParameter("@pPagoxhora", PagoPorHora))
            p.Add(New SqlParameter("@pFactorComision", FactorComision))
            p.Add(New SqlParameter("@pPromedioCuotavariable", PromedioCuotavariable))
            p.Add(New SqlParameter("@pFactorDestajo", FactorDestajo))
            p.Add(New SqlParameter("@pIntegradoTipo", IntegradoTipo))
            p.Add(New SqlParameter("@pHoraSaldia", HoraSaldia))
            p.Add(New SqlParameter("@pPromedioPercepcionesvariables", PromedioPercepcionesvariables))

            db.ExecuteSPWithParams("pUpdateConcepto", p)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ConsultarEmpleadoID()
        Try
            p.Clear()
            'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            p.Add(New SqlParameter("@pIdEmpleado", IdEmpleado))

            dt = db.ExecuteSP("pConsultarEmpleado", p)

            If dt.Rows.Count > 0 Then
                IdEmpleado = dt.Rows(0).Item("id")
                Nombre = dt.Rows(0).Item("nombre")
                Rfc = dt.Rows(0).Item("rfc")
                Telefono = dt.Rows(0).Item("telefono")
                Correo = dt.Rows(0).Item("email1")
                Domicilio = dt.Rows(0).Item("calle")
                Colonia = dt.Rows(0).Item("colonia")
                Cp = dt.Rows(0).Item("codigo_postal")
                Ciudad = dt.Rows(0).Item("municipio")
                Pais = dt.Rows(0).Item("pais")
                Curp = dt.Rows(0).Item("curp")
                Imss = dt.Rows(0).Item("no_imss")
                Clabe = dt.Rows(0).Item("clabe")

                CuotaDiaria = dt.Rows(0).Item("sueldo_diario")
                IntegradoImss = dt.Rows(0).Item("salario_diario_integrado")
                Descansosxsemana = dt.Rows(0).Item("descansos_por_semana")
                Asimiladototalsemanal = dt.Rows(0).Item("asimilado_total_semanal")
                Porcentajeimptocedularestatal = dt.Rows(0).Item("porcentaje_impto_cedular_estatal")
                PagoPorHora = dt.Rows(0).Item("pago_por_hora")
                FactorComision = dt.Rows(0).Item("factor_comision")
                PromedioCuotavariable = dt.Rows(0).Item("promedio_cuota_variable")
                FactorDestajo = dt.Rows(0).Item("factor_destajo")
                IntegradoTipo = dt.Rows(0).Item("integrado_tipo")
                HoraSaldia = dt.Rows(0).Item("hora_saldia")
                PromedioPercepcionesvariables = dt.Rows(0).Item("promedio_percepciones_variables")

                IdMetodopago = dt.Rows(0).Item("metodopagoid")
                IdBanco = dt.Rows(0).Item("bancoid")
                IdRiesgo = dt.Rows(0).Item("riesgopuestoid")
                IdRegimencontratacion = dt.Rows(0).Item("regimencontratacionid")
                IdPuesto = dt.Rows(0).Item("puestoid")
                IdTipoContrato = dt.Rows(0).Item("tipo_contratoid")
                IdPeriodicidadpago = dt.Rows(0).Item("periodopagoid")

                IdEstado = dt.Rows(0).Item("estadoid")
                IdTipoJornada = dt.Rows(0).Item("tipo_jornadaid")
                FechaIngreso = dt.Rows(0).Item("fecha_alta")

                NumClabe = dt.Rows(0).Item("clabe")
                NumCuenta = dt.Rows(0).Item("num_cuenta")
                Puesto = dt.Rows(0).Item("puesto")
                RegimenContratacion = dt.Rows(0).Item("regimen_contratacion")
                'IdRegimen = dt.Rows(0).Item("tipo_contratoid")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ConsultarEmpleadoContratoID()
        Try
            p.Clear()
            p.Add(New SqlParameter("@pIdEmpleado", IdEmpleado))
            p.Add(New SqlParameter("@pIdContrato", IdContrato))

            dt = db.ExecuteSP("pConsultarEmpleado", p)

            If dt.Rows.Count > 0 Then
                IdEmpleado = dt.Rows(0).Item("id")
                Nombre = dt.Rows(0).Item("nombre")
                Rfc = dt.Rows(0).Item("rfc")
                Telefono = dt.Rows(0).Item("telefono")
                Correo = dt.Rows(0).Item("email1")
                Domicilio = dt.Rows(0).Item("calle")
                Colonia = dt.Rows(0).Item("colonia")
                Cp = dt.Rows(0).Item("codigo_postal")
                Ciudad = dt.Rows(0).Item("municipio")
                Pais = dt.Rows(0).Item("pais")
                Curp = dt.Rows(0).Item("curp")
                Imss = dt.Rows(0).Item("no_imss")
                Clabe = dt.Rows(0).Item("clabe")

                CuotaDiaria = dt.Rows(0).Item("sueldo_diario")
                IntegradoImss = dt.Rows(0).Item("sueldo_diario_integrado")
                Descansosxsemana = dt.Rows(0).Item("descansos_por_semana")
                AsimiladoTotalSemanal = dt.Rows(0).Item("asimilado_total_semanal")
                Porcentajeimptocedularestatal = dt.Rows(0).Item("porcentaje_impto_cedular_estatal")
                PagoPorHora = dt.Rows(0).Item("pago_por_hora")
                FactorComision = dt.Rows(0).Item("factor_comision")
                PromedioCuotavariable = dt.Rows(0).Item("promedio_cuota_variable")
                FactorDestajo = dt.Rows(0).Item("factor_destajo")
                IntegradoTipo = dt.Rows(0).Item("integrado_tipo")
                HoraSaldia = dt.Rows(0).Item("hora_saldia")
                PromedioPercepcionesvariables = dt.Rows(0).Item("promedio_percepciones_variables")

                IdMetodopago = dt.Rows(0).Item("metodopagoid")
                IdBanco = dt.Rows(0).Item("bancoid")
                IdRiesgo = dt.Rows(0).Item("riesgopuestoid")
                IdRegimenContratacion = dt.Rows(0).Item("regimencontratacionid")
                IdPuesto = dt.Rows(0).Item("puestoid")
                IdTipoContrato = dt.Rows(0).Item("tipo_contratoid")
                IdPeriodicidadpago = dt.Rows(0).Item("periodopagoid")

                IdEstado = dt.Rows(0).Item("estadoid")
                IdTipoJornada = dt.Rows(0).Item("tipo_jornadaid")
                FechaIngreso = dt.Rows(0).Item("fecha_alta")

                NumClabe = dt.Rows(0).Item("clabe")
                NumCuenta = dt.Rows(0).Item("num_cuenta")
                Puesto = dt.Rows(0).Item("puesto")
                RegimenContratacion = dt.Rows(0).Item("regimen_contratacion")
                'IdRegimen = dt.Rows(0).Item("tipo_contratoid")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ConsultarEmpleadoContratoActivo()
        Try
            p.Clear()
            'p.Add(New SqlParameter("@pIdEmpresa", IdEmpresa))
            p.Add(New SqlParameter("@pIdEmpleado", IdEmpleado))

            dt = db.ExecuteSP("pConsultarEmpleadoActivo", p)

            If dt.Rows.Count > 0 Then
                IdEmpleado = dt.Rows(0).Item("id")
                Nombre = dt.Rows(0).Item("nombre")
                Rfc = dt.Rows(0).Item("rfc")
                Telefono = dt.Rows(0).Item("telefono")
                Correo = dt.Rows(0).Item("email1")
                Domicilio = dt.Rows(0).Item("calle")
                Colonia = dt.Rows(0).Item("colonia")
                Cp = dt.Rows(0).Item("codigo_postal")
                Ciudad = dt.Rows(0).Item("municipio")
                Pais = dt.Rows(0).Item("pais")
                Curp = dt.Rows(0).Item("curp")
                Imss = dt.Rows(0).Item("no_imss")
                Clabe = dt.Rows(0).Item("clabe")

                CuotaDiaria = dt.Rows(0).Item("sueldo_diario")
                IntegradoImss = dt.Rows(0).Item("sueldo_diario_integrado")
                Descansosxsemana = dt.Rows(0).Item("descansos_por_semana")
                AsimiladoTotalSemanal = dt.Rows(0).Item("asimilado_total_semanal")
                Porcentajeimptocedularestatal = dt.Rows(0).Item("porcentaje_impto_cedular_estatal")
                PagoPorHora = dt.Rows(0).Item("pago_por_hora")
                FactorComision = dt.Rows(0).Item("factor_comision")
                PromedioCuotavariable = dt.Rows(0).Item("promedio_cuota_variable")
                FactorDestajo = dt.Rows(0).Item("factor_destajo")
                IntegradoTipo = dt.Rows(0).Item("integrado_tipo")
                HoraSaldia = dt.Rows(0).Item("hora_saldia")
                PromedioPercepcionesvariables = dt.Rows(0).Item("promedio_percepciones_variables")

                IdMetodopago = dt.Rows(0).Item("metodopagoid")
                IdBanco = dt.Rows(0).Item("bancoid")
                IdRiesgo = dt.Rows(0).Item("riesgopuestoid")
                IdRegimenContratacion = dt.Rows(0).Item("regimencontratacionid")
                IdPuesto = dt.Rows(0).Item("puestoid")
                IdTipoContrato = dt.Rows(0).Item("tipo_contratoid")
                IdPeriodicidadpago = dt.Rows(0).Item("periodopagoid")

                IdEstado = dt.Rows(0).Item("estadoid")
                IdTipoJornada = dt.Rows(0).Item("tipo_jornadaid")
                FechaIngreso = dt.Rows(0).Item("fecha_alta")

                NumClabe = dt.Rows(0).Item("clabe")
                NumCuenta = dt.Rows(0).Item("num_cuenta")
                Puesto = dt.Rows(0).Item("puesto")
                RegimenContratacion = dt.Rows(0).Item("regimen_contratacion")
                'IdRegimen = dt.Rows(0).Item("tipo_contratoid")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class