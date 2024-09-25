Public Class Nomina
    Private _Id As Long
    'Private _IdEmpresa As Integer
    Private _Ejercicio As Integer
    Private _Cliente As Integer
    Private _TipoNomina As Integer
    Private _Periodo As Integer
    Private _EsEspecial As Boolean
    Private _NoEmpleado As Integer
    Private _IdContrato As Integer
    Private _CvoConcepto As Integer
    Private _CvoSAT As String
    Private _Tipo As String
    Private _TipoConcepto As String
    Private _Unidad As Decimal
    Private _Importe As Double
    Private _ImporteGravado As Double
    Private _ImporteExento As Double
    Private _Generado As String
    Private _Timbrado As String
    Private _Pdf As String
    Private _Enviado As String
    Private _Situacion As String
    Private _DiferenteDe As Integer
    Private _IdMovimiento As Integer
    Private _IdEstatus As Integer
    Private _DiasHorasExtra As Integer
    Private _TipoHorasExtra As String
    Private _UUID As String
    Private _Mes As Integer
    Private _IdRegistroPatronal As Integer
    Private _DiasPagadosVacaciones As Integer
    Private _FechaIni As Date
    Private _FechaFin As Date
    Private _IdCreditoFonacot As Integer
    Private _Mes1 As Integer
    Private _Mes2 As Integer
    Private _FechaPago As Date
    Private _DiasPagados As Integer
    Private _idNomina As Integer
    Private _OtroPagoBit As Integer

    Public Sub New()
        _Id = 0
        '_IdEmpresa = 0
        _Ejercicio = 0
        _TipoNomina = 0
        _Periodo = 0
        _EsEspecial = False
        _NoEmpleado = 0
        _CvoConcepto = 0
        _Cliente = 0
        _CvoSAT = String.Empty
        _Tipo = String.Empty
        _TipoConcepto = String.Empty
        _Unidad = 0
        _Importe = 0
        _ImporteGravado = 0
        _ImporteExento = 0
        _Generado = String.Empty
        _Timbrado = String.Empty
        _Pdf = String.Empty
        _Enviado = String.Empty
        _Situacion = String.Empty
        _DiferenteDe = 0
        _IdMovimiento = 0
        _IdEstatus = 0
        _DiasHorasExtra = 0
        _TipoHorasExtra = String.Empty
        _UUID = String.Empty
        _Mes = 0
        _IdRegistroPatronal = 0
        _DiasPagadosVacaciones = 0
        _FechaIni = Date.Now
        _FechaFin = Date.Now
        _IdCreditoFonacot = 0
        _Mes1 = 0
        _Mes2 = 0
        _idNomina = 0
        _OtroPagoBit = 0
    End Sub
    Public Property IdNomina As Integer
        Get
            Return _idNomina
        End Get
        Set(value As Integer)
            _idNomina = value
        End Set
    End Property
    Public Property Id() As Long
        Get
            Return _Id
        End Get
        Set(ByVal value As Long)
            _Id = value
        End Set
    End Property
    'Public Property IdEmpresa() As Int32
    '    Get
    '        Return _IdEmpresa
    '    End Get
    '    Set(ByVal value As Int32)
    '        _IdEmpresa = value
    '    End Set
    'End Property
    Public Property Ejercicio() As Int32
        Get
            Return _Ejercicio
        End Get
        Set(ByVal value As Int32)
            _Ejercicio = value
        End Set
    End Property
    Public Property Cliente() As Int32
        Get
            Return _Cliente
        End Get
        Set(ByVal value As Int32)
            _Cliente = value
        End Set
    End Property
    Public Property TipoNomina() As Integer
        Get
            Return _TipoNomina
        End Get
        Set(ByVal value As Integer)
            _TipoNomina = value
        End Set
    End Property
    Public Property EsEspecial() As Boolean
        Get
            Return _EsEspecial
        End Get
        Set(ByVal value As Boolean)
            _EsEspecial = value
        End Set
    End Property
    Public Property Periodo() As Integer
        Get
            Return _Periodo
        End Get
        Set(ByVal value As Integer)
            _Periodo = value
        End Set
    End Property
    Public Property NoEmpleado() As Integer
        Get
            Return _NoEmpleado
        End Get
        Set(ByVal value As Integer)
            _NoEmpleado = value
        End Set
    End Property
    Public Property IdContrato() As Integer
        Get
            Return _IdContrato
        End Get
        Set(ByVal value As Integer)
            _IdContrato = value
        End Set
    End Property
    Public Property CvoConcepto() As Integer
        Get
            Return _CvoConcepto
        End Get
        Set(ByVal value As Integer)
            _CvoConcepto = value
        End Set
    End Property
    Public Property CvoSAT() As String
        Get
            Return _CvoSAT
        End Get
        Set(ByVal value As String)
            _CvoSAT = value
        End Set
    End Property
    Public Property Tipo() As String
        Get
            Return _Tipo
        End Get
        Set(ByVal value As String)
            _Tipo = value
        End Set
    End Property
    Public Property TipoConcepto() As String
        Get
            Return _TipoConcepto
        End Get
        Set(ByVal value As String)
            _TipoConcepto = value
        End Set
    End Property
    Public Property Unidad() As Decimal
        Get
            Return _Unidad
        End Get
        Set(ByVal value As Decimal)
            _Unidad = value
        End Set
    End Property
    Public Property Importe() As Decimal
        Get
            Return _Importe
        End Get
        Set(ByVal value As Decimal)
            _Importe = value
        End Set
    End Property
    Public Property ImporteGravado() As Decimal
        Get
            Return _ImporteGravado
        End Get
        Set(ByVal value As Decimal)
            _ImporteGravado = value
        End Set
    End Property
    Public Property ImporteExento() As Decimal
        Get
            Return _ImporteExento
        End Get
        Set(ByVal value As Decimal)
            _ImporteExento = value
        End Set
    End Property
    Public Property Generado() As String
        Get
            Return _Generado
        End Get
        Set(ByVal value As String)
            _Generado = value
        End Set
    End Property
    Public Property Timbrado() As String
        Get
            Return _Timbrado
        End Get
        Set(ByVal value As String)
            _Timbrado = value
        End Set
    End Property
    Public Property Pdf() As String
        Get
            Return _Pdf
        End Get
        Set(ByVal value As String)
            _Pdf = value
        End Set
    End Property
    Public Property Enviado() As String
        Get
            Return _Enviado
        End Get
        Set(ByVal value As String)
            _Enviado = value
        End Set
    End Property
    Public Property Situacion() As String
        Get
            Return _Situacion
        End Get
        Set(ByVal value As String)
            _Situacion = value
        End Set
    End Property
    Public Property DiferenteDe() As Integer
        Get
            Return _DiferenteDe
        End Get
        Set(ByVal value As Integer)
            _DiferenteDe = value
        End Set
    End Property
    Public Property IdMovimiento() As Integer
        Get
            Return _IdMovimiento
        End Get
        Set(ByVal value As Integer)
            _IdMovimiento = value
        End Set
    End Property
    Public Property IdEstatus() As Integer
        Get
            Return _IdEstatus
        End Get
        Set(ByVal value As Integer)
            _IdEstatus = value
        End Set
    End Property
    Public Property DiasHorasExtra() As Integer
        Get
            Return _DiasHorasExtra
        End Get
        Set(ByVal value As Integer)
            _DiasHorasExtra = value
        End Set
    End Property
    Public Property TipoHorasExtra() As String
        Get
            Return _TipoHorasExtra
        End Get
        Set(ByVal value As String)
            _TipoHorasExtra = value
        End Set
    End Property
    Public Property UUID() As String
        Get
            Return _UUID
        End Get
        Set(ByVal value As String)
            _UUID = value
        End Set
    End Property
    Public Property Mes() As Integer
        Get
            Return _Mes
        End Get
        Set(ByVal value As Integer)
            _Mes = value
        End Set
    End Property
    Public Property IdRegistroPatronal() As Integer
        Get
            Return _IdRegistroPatronal
        End Get
        Set(ByVal value As Integer)
            _IdRegistroPatronal = value
        End Set
    End Property
    Public Property DiasPagadosVacaciones() As Integer
        Get
            Return _DiasPagadosVacaciones
        End Get
        Set(ByVal value As Integer)
            _DiasPagadosVacaciones = value
        End Set
    End Property
    Public Property FechaIni() As Date
        Get
            Return _FechaIni
        End Get
        Set(ByVal value As Date)
            _FechaIni = value
        End Set
    End Property
    Public Property FechaFin() As Date
        Get
            Return _FechaFin
        End Get
        Set(ByVal value As Date)
            _FechaFin = value
        End Set
    End Property
    Public Property IdCreditoFonacot() As Int32
        Get
            Return _IdCreditoFonacot
        End Get
        Set(ByVal value As Int32)
            _IdCreditoFonacot = value
        End Set
    End Property
    Public Property Mes1() As Integer
        Get
            Return _Mes1
        End Get
        Set(ByVal value As Integer)
            _Mes1 = value
        End Set
    End Property
    Public Property Mes2() As Integer
        Get
            Return _Mes2
        End Get
        Set(ByVal value As Integer)
            _Mes2 = value
        End Set
    End Property
    Public Property FechaPago As Date
        Get
            Return _FechaPago
        End Get
        Set(value As Date)
            _FechaPago = value
        End Set
    End Property
    Public Property DiasPagados As Integer
        Get
            Return _DiasPagados
        End Get
        Set(value As Integer)
            _DiasPagados = value
        End Set
    End Property
    Public Property OtroPagoBit() As Integer
        Get
            Return _OtroPagoBit
        End Get
        Set(ByVal value As Integer)
            _OtroPagoBit = value
        End Set
    End Property

End Class