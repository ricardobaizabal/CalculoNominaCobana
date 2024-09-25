Public Class Configuracion
    Private _IdConfiguracion As Integer
    Private _IdCliente As Integer
    Private _Fecha As DateTime
    Private _IdUsuario As Integer
    Private _IdEjercicio As Integer
    Private _IdPeriodo As Integer
    Private _SalarioMinimoDiarioGeneral As Decimal
    Private _ImporteSeguroVivienda As Decimal

    Public Sub New()
        _IdConfiguracion = 0
        _IdCliente = 0
        _Fecha = Nothing
        _IdUsuario = 0
        _IdEjercicio = 0
        _IdPeriodo = 0
        _SalarioMinimoDiarioGeneral = 0
        _ImporteSeguroVivienda = 0
    End Sub

    Public Property IdConfiguracion() As Int32
        Get
            Return _IdConfiguracion
        End Get
        Set(ByVal value As Int32)
            _IdConfiguracion = value
        End Set
    End Property

    Public Property IdCliente() As Int32
        Get
            Return _IdCliente
        End Get
        Set(ByVal value As Int32)
            _IdCliente = value
        End Set
    End Property

    Public Property Fecha() As DateTime
        Get
            Return _Fecha
        End Get
        Set(ByVal value As DateTime)
            _Fecha = value
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

    Public Property IdEjercicio() As Int32
        Get
            Return _IdEjercicio
        End Get
        Set(ByVal value As Int32)
            _IdEjercicio = value
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

    Public Property SalarioMinimoDiarioGeneral() As String
        Get
            Return _SalarioMinimoDiarioGeneral
        End Get
        Set(ByVal value As String)
            _SalarioMinimoDiarioGeneral = value
        End Set
    End Property

    Public Property ImporteSeguroVivienda() As String
        Get
            Return _ImporteSeguroVivienda
        End Get
        Set(ByVal value As String)
            _ImporteSeguroVivienda = value
        End Set
    End Property

End Class
