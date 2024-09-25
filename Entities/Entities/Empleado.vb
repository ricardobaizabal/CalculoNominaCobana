Public Class Empleado
    'Private _IdEmpresa As Int32
    Private _IdEmpleado As Int32
    Private _IdRegimen As Int32
    Private _Nombre As String
    Private _Domicilio As String
    Private _Colonia As String
    Private _Ciudad As String
    Private _IdEstado As Int32
    Private _Cp As String
    Private _Telefono As String
    Private _FechaIngreso As String
    Private _Rfc As String
    Private _Curp As String
    Private _Correo As String
    Private _Activo As String
    Private _Imss As String
    Private _IdContrato As Int32
    Private _IdPeriodicidadpago As Int32
    Private _IdPuesto As Int32
    Private _IdDepartamento As Int32
    Private _Pais As String
    Private _clavemp As Int32
    Private _IdRiesgo As Int32
    Private _IdTipoJornada As Int32
    Private _IdRegimenContratacion As Int32
    Private _IdBanco As String
    Private _Clabe As String
    Private _CuotaDiaria As Double
    Private _FactorIntegracion As String
    Private _IntegradoImss As Double
    Private _Descansosxsemana As String
    Private _AsimiladoTotalSemanal As Int32
    Private _Porcentajeimptocedularestatal As Int32
    Private _PagoPorHora As Int32
    Private _FactorComision As Int32
    Private _PromedioCuotavariable As Int32
    Private _FactorDestajo As Int32
    Private _IntegradoTipo As String
    Private _HoraSaldia As String
    Private _NumClabe As String
    Private _NumCuenta As String
    Private _Puesto As String
    Private _PromedioPercepcionesvariables As Int32
    Private _IdMetodopago As Int32
    Private _IdTipoContrato As Int32
    Private _RegimenContratacion As String
    Private _IdMovimiento As Integer

    Public Sub New()
        '_IdEmpresa = 0
        _IdEmpleado = 0
        _IdMetodopago = 0
        _IdTipoContrato = 0
        _NumClabe = String.Empty
        _Clabe = String.Empty
        _NumCuenta = String.Empty
        _Nombre = String.Empty
        _Domicilio = String.Empty
        _Colonia = String.Empty
        _Ciudad = String.Empty
        _IdEstado = 0
        _Cp = String.Empty
        _Telefono = String.Empty
        '_FechaIngreso = String.Empty
        _Rfc = String.Empty
        _Curp = String.Empty
        _Correo = String.Empty
        _Activo = String.Empty
        _Imss = String.Empty
        _IdContrato = 0
        _IdPeriodicidadpago = 0
        _IdPuesto = 0
        _IdDepartamento = 0
        _Pais = String.Empty
        _clavemp = 0
        _IdRiesgo = 0
        _IdRegimen = 0
        _IdTipoJornada = 0
        _IdRegimenContratacion = 0
        _IdBanco = 0
        _Clabe = String.Empty
        _RegimenContratacion = String.Empty
        _CuotaDiaria = 0
        _FactorIntegracion = String.Empty
        _IntegradoImss = 0
        _Descansosxsemana = String.Empty
        _AsimiladoTotalSemanal = 0
        _Porcentajeimptocedularestatal = 0
        _PagoPorHora = 0
        _FactorComision = 0
        _PromedioCuotavariable = 0
        _FactorDestajo = 0
        _IntegradoTipo = String.Empty
        _HoraSaldia = String.Empty
        _PromedioPercepcionesvariables = 0
        _Puesto = String.Empty
        _IdMovimiento = 0
    End Sub

    'Public Property IdEmpresa() As Int32
    '    Get
    '        Return _IdEmpresa
    '    End Get
    '    Set(ByVal value As Int32)
    '        _IdEmpresa = value
    '    End Set
    'End Property
    Public Property IdMetodopago() As Int32
        Get
            Return _IdMetodopago
        End Get
        Set(ByVal value As Int32)
            _IdMetodopago = value
        End Set
    End Property
    Public Property IdTipoContrato() As Int32
        Get
            Return _IdTipoContrato
        End Get
        Set(ByVal value As Int32)
            _IdTipoContrato = value
        End Set
    End Property
    Public Property IdEmpleado() As Int32
        Get
            Return _IdEmpleado
        End Get
        Set(ByVal value As Int32)
            _IdEmpleado = value
        End Set
    End Property
    Public Property IdRegimen() As Int32
        Get
            Return _IdRegimen
        End Get
        Set(ByVal value As Int32)
            _IdRegimen = value
        End Set
    End Property
    Public Property Puesto() As String
        Get
            Return _Puesto
        End Get
        Set(ByVal value As String)
            _Puesto = value
        End Set
    End Property
    Public Property Nombre() As String
        Get
            Return _Nombre
        End Get
        Set(ByVal value As String)
            _Nombre = value
        End Set
    End Property
    Public Property RegimenContratacion() As String
        Get
            Return _RegimenContratacion
        End Get
        Set(ByVal value As String)
            _RegimenContratacion = value
        End Set
    End Property
    Public Property NumCuenta() As String
        Get
            Return _NumCuenta
        End Get
        Set(ByVal value As String)
            _NumCuenta = value
        End Set
    End Property
    Public Property Domicilio() As String
        Get
            Return _Domicilio
        End Get
        Set(ByVal value As String)
            _Domicilio = value
        End Set
    End Property
    Public Property Colonia() As String
        Get
            Return _Colonia
        End Get
        Set(ByVal value As String)
            _Colonia = value
        End Set
    End Property
    Public Property NumClabe() As String
        Get
            Return _NumClabe
        End Get
        Set(ByVal value As String)
            _NumClabe = value
        End Set
    End Property
    Public Property Ciudad() As String
        Get
            Return _Ciudad
        End Get
        Set(ByVal value As String)
            _Ciudad = value
        End Set
    End Property
    Public Property IdEstado() As Int32
        Get
            Return _IdEstado
        End Get
        Set(ByVal value As Int32)
            _IdEstado = value
        End Set
    End Property
    Public Property Cp() As String
        Get
            Return _Cp
        End Get
        Set(ByVal value As String)
            _Cp = value
        End Set
    End Property
    Public Property Telefono() As String
        Get
            Return _Telefono
        End Get
        Set(ByVal value As String)
            _Telefono = value
        End Set
    End Property
    Public Property FechaIngreso() As String
        Get
            Return _FechaIngreso
        End Get
        Set(ByVal value As String)
            _FechaIngreso = value
        End Set
    End Property
    Public Property Rfc() As String
        Get
            Return _Rfc
        End Get
        Set(ByVal value As String)
            _Rfc = value
        End Set
    End Property
    Public Property Curp() As String
        Get
            Return _Curp
        End Get
        Set(ByVal value As String)
            _Curp = value
        End Set
    End Property
    Public Property Correo() As String
        Get
            Return _Correo
        End Get
        Set(ByVal value As String)
            _Correo = value
        End Set
    End Property
    Public Property Activo() As String
        Get
            Return _Activo
        End Get
        Set(ByVal value As String)
            _Activo = value
        End Set
    End Property
    Public Property Imss() As String
        Get
            Return _Imss
        End Get
        Set(ByVal value As String)
            _Imss = value
        End Set
    End Property
    Public Property IdContrato() As Int32
        Get
            Return _IdContrato
        End Get
        Set(ByVal value As Int32)
            _IdContrato = value
        End Set
    End Property
    Public Property IdPeriodicidadpago() As Int32
        Get
            Return _IdPeriodicidadpago
        End Get
        Set(ByVal value As Int32)
            _IdPeriodicidadpago = value
        End Set
    End Property
    Public Property IdPuesto() As Int32
        Get
            Return _IdPuesto
        End Get
        Set(ByVal value As Int32)
            _IdPuesto = value
        End Set
    End Property
    Public Property IdDepartamento() As Int32
        Get
            Return _IdDepartamento
        End Get
        Set(ByVal value As Int32)
            _IdDepartamento = value
        End Set
    End Property
    Public Property Pais() As String
        Get
            Return _Pais
        End Get
        Set(ByVal value As String)
            _Pais = value
        End Set
    End Property
    Public Property IdRiesgo() As Int32
        Get
            Return _IdRiesgo
        End Get
        Set(ByVal value As Int32)
            _IdRiesgo = value
        End Set
    End Property
    Public Property IdTipoJornada() As Int32
        Get
            Return _IdTipoJornada
        End Get
        Set(ByVal value As Int32)
            _IdTipoJornada = value
        End Set
    End Property
    Public Property IdRegimenContratacion() As Int32
        Get
            Return _IdRegimenContratacion
        End Get
        Set(ByVal value As Int32)
            _IdRegimenContratacion = value
        End Set
    End Property
    Public Property IdBanco() As String
        Get
            Return _IdBanco
        End Get
        Set(ByVal value As String)
            _IdBanco = value
        End Set
    End Property
    Public Property Clabe() As String
        Get
            Return _Clabe
        End Get
        Set(ByVal value As String)
            _Clabe = value
        End Set
    End Property
    Public Property CuotaDiaria() As Double
        Get
            Return _CuotaDiaria
        End Get
        Set(ByVal value As Double)
            _CuotaDiaria = value
        End Set
    End Property
    Public Property FactorIntegracion() As Double
        Get
            Return _FactorIntegracion
        End Get
        Set(ByVal value As Double)
            _FactorIntegracion = value
        End Set
    End Property
    Public Property IntegradoImss() As Double
        Get
            Return _IntegradoImss
        End Get
        Set(ByVal value As Double)
            _IntegradoImss = value
        End Set
    End Property
    Public Property Descansosxsemana() As String
        Get
            Return _Descansosxsemana
        End Get
        Set(ByVal value As String)
            _Descansosxsemana = value
        End Set
    End Property
    Public Property AsimiladoTotalSemanal() As Int32
        Get
            Return _AsimiladoTotalSemanal
        End Get
        Set(ByVal value As Int32)
            _AsimiladoTotalSemanal = value
        End Set
    End Property
    Public Property Porcentajeimptocedularestatal() As Int32
        Get
            Return _Porcentajeimptocedularestatal
        End Get
        Set(ByVal value As Int32)
            _Porcentajeimptocedularestatal = value
        End Set
    End Property
    Public Property PagoPorHora() As Int32
        Get
            Return _PagoPorHora
        End Get
        Set(ByVal value As Int32)
            _PagoPorHora = value
        End Set
    End Property
    Public Property FactorComision() As Int32
        Get
            Return _FactorComision
        End Get
        Set(ByVal value As Int32)
            _FactorComision = value
        End Set
    End Property
    Public Property PromedioCuotavariable() As Int32
        Get
            Return _PromedioCuotavariable
        End Get
        Set(ByVal value As Int32)
            _PromedioCuotavariable = value
        End Set
    End Property
    Public Property FactorDestajo() As Int32
        Get
            Return _FactorDestajo
        End Get
        Set(ByVal value As Int32)
            _FactorDestajo = value
        End Set
    End Property
    Public Property IntegradoTipo() As String
        Get
            Return _IntegradoTipo
        End Get
        Set(ByVal value As String)
            _IntegradoTipo = value
        End Set
    End Property
    Public Property HoraSaldia() As String
        Get
            Return _HoraSaldia
        End Get
        Set(ByVal value As String)
            _HoraSaldia = value
        End Set
    End Property
    Public Property PromedioPercepcionesvariables() As Int32
        Get
            Return _PromedioPercepcionesvariables
        End Get
        Set(ByVal value As Int32)
            _PromedioPercepcionesvariables = value
        End Set
    End Property
    Public Property IdMovimiento() As Int32
        Get
            Return _IdMovimiento
        End Get
        Set(ByVal value As Int32)
            _IdMovimiento = value
        End Set
    End Property

End Class
