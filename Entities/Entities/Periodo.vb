Public Class Periodo

    Private _cmd As Integer
    Private _IdPeriodo As Int32
    Private _Descripcion As String
    Private _NoPeriodo As String
    Private _Cliente As String
    Private _IdEjercicio As Integer
    Private _IdTipoNomina As Int32
    Private _FechaInicial As String
    Private _FechaFinal As String
    Private _FechaPago As String
    Private _Dias As Integer
    Private _IdUsuario As Int32
    Private _GeneraPeriodos As Int32
    Private _ExtraordinarioBit As Boolean
    Private _FechaInicialDate As Date
    Private _FechaFinalDate As Date
    Private _IdEmpresa As Integer
    Private _IdCliente As Integer
    Private _InicioMesBit As Boolean
    Private _FinMesBit As Boolean
    Private _InicioEjercicioBit As Boolean
    Private _FinEjercicioBit As Boolean
    Private _FechaBaja As Date

    Public Sub New()
        _cmd = 0
        _IdEmpresa = 0
        _IdCliente = 0
        _IdPeriodo = 0
        _Descripcion = String.Empty
        _NoPeriodo = String.Empty
        _Cliente = String.Empty
        _IdEjercicio = 0
        _IdTipoNomina = 0
        _FechaInicial = String.Empty
        _FechaFinal = String.Empty
        _FechaPago = String.Empty
        _Dias = 0
        _IdUsuario = 0
        _GeneraPeriodos = 0
        _ExtraordinarioBit = False
        _InicioMesBit = False
        _FinMesBit = False
        _InicioEjercicioBit = False
        _FinEjercicioBit = False
        _FechaBaja = Nothing
    End Sub
    Public Property cmd As Integer
        Get
            Return _cmd
        End Get
        Set(value As Integer)
            _cmd = value
        End Set
    End Property
    Public Property IdEmpresa As Integer
        Get
            Return _IdEmpresa
        End Get
        Set(value As Integer)
            _IdEmpresa = value
        End Set
    End Property
    Public Property IdCliente As Integer
        Get
            Return _IdCliente
        End Get
        Set(value As Integer)
            _IdCliente = value
        End Set
    End Property
    Public Property IdPeriodo() As Int32
        Get
            Return _IdPeriodo
        End Get
        Set(ByVal value As Int32)
            _IdPeriodo = value
        End Set
    End Property
    Public Property IdTipoNomina() As Int32
        Get
            Return _IdTiponomina
        End Get
        Set(ByVal value As Int32)
            _IdTiponomina = value
        End Set
    End Property
    Public Property Dias() As Integer
        Get
            Return _Dias
        End Get
        Set(ByVal value As Integer)
            _Dias = value
        End Set
    End Property
    Public Property IdEjercicio() As Integer
        Get
            Return _IdEjercicio
        End Get
        Set(ByVal value As Integer)
            _IdEjercicio = value
        End Set
    End Property
    Public Property Descripcion() As String
        Get
            Return _Descripcion
        End Get
        Set(ByVal value As String)
            _Descripcion = value
        End Set
    End Property
    Public Property NoPeriodo() As String
        Get
            Return _NoPeriodo
        End Get
        Set(ByVal value As String)
            _NoPeriodo = value
        End Set
    End Property
    Public Property Cliente() As String
        Get
            Return _Cliente
        End Get
        Set(ByVal value As String)
            _Cliente = value
        End Set
    End Property
    Public Property FechaInicial() As String
        Get
            Return _FechaInicial
        End Get
        Set(ByVal value As String)
            _FechaInicial = value
        End Set
    End Property
    Public Property FechaFinal() As String
        Get
            Return _FechaFinal
        End Get
        Set(ByVal value As String)
            _FechaFinal = value
        End Set
    End Property
    Public Property FechaPago() As String
        Get
            Return _FechaPago
        End Get
        Set(ByVal value As String)
            _FechaPago = value
        End Set
    End Property
    Public Property IdUsuario() As Int32
        Get
            Return _IdUsuario
        End Get
        Set(ByVal value As Int32)
            _IdUsuario = value
        End Set
    End Property
    Public Property GeneraPeriodos() As Int32
        Get
            Return _GeneraPeriodos
        End Get
        Set(ByVal value As Int32)
            _GeneraPeriodos = value
        End Set
    End Property
    Public Property ExtraordinarioBit() As Boolean
        Get
            Return _ExtraordinarioBit
        End Get
        Set(ByVal value As Boolean)
            _ExtraordinarioBit = value
        End Set
    End Property
    Public Property FechaInicialDate As Date
        Get
            Return _FechaInicialDate
        End Get
        Set(value As Date)
            _FechaInicialDate = value
        End Set
    End Property
    Public Property FechaFinalDate As Date
        Get
            Return _FechaFinalDate
        End Get
        Set(value As Date)
            _FechaFinalDate = value
        End Set
    End Property
    Public Property InicioMesBit() As Boolean
        Get
            Return _InicioMesBit
        End Get
        Set(ByVal value As Boolean)
            _InicioMesBit = value
        End Set
    End Property
    Public Property FinMesBit() As Boolean
        Get
            Return _FinMesBit
        End Get
        Set(ByVal value As Boolean)
            _FinMesBit = value
        End Set
    End Property
    Public Property InicioEjercicioBit() As Boolean
        Get
            Return _InicioEjercicioBit
        End Get
        Set(ByVal value As Boolean)
            _InicioEjercicioBit = value
        End Set
    End Property
    Public Property FinEjercicioBit() As Boolean
        Get
            Return _FinEjercicioBit
        End Get
        Set(ByVal value As Boolean)
            _FinEjercicioBit = value
        End Set
    End Property
    Public Property FechaBaja() As Date
        Get
            Return _FechaBaja
        End Get
        Set(ByVal value As Date)
            _FechaBaja = value
        End Set
    End Property

End Class