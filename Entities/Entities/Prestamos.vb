Public Class Prestamos
    Private _Id As Integer
    Private _IdEmpresa As Integer
    Private _IdEmpleado As Integer
    Private _InicioPrestamo As DateTime
    Private _MontoTotal As Decimal
    Private _Saldo As Decimal
    Private _Descuneto As Decimal
    Private _TipoPrestamo As Integer

    Public Sub New()
        _Id = 0
        _IdEmpresa = 0
        _IdEmpleado = 0
        _MontoTotal = 0
        _Saldo = 0
        _Descuneto = 0
        _TipoPrestamo = 0
    End Sub
    Public Property Id() As Integer
        Get
            Return _Id
        End Get
        Set(ByVal value As Integer)
            _Id = value
        End Set
    End Property
    Public Property IdEmpresa() As Integer
        Get
            Return _IdEmpleado
        End Get
        Set(ByVal value As Integer)
            _IdEmpleado = value
        End Set
    End Property
    Public Property IdEmpleado() As Integer
        Get
            Return _IdEmpresa
        End Get
        Set(ByVal value As Integer)
            _IdEmpresa = value
        End Set
    End Property
    Public Property MontoTotal() As Decimal
        Get
            Return _MontoTotal
        End Get
        Set(ByVal value As Decimal)
            _MontoTotal = value
        End Set
    End Property
    Public Property Saldo() As Decimal
        Get
            Return _Saldo
        End Get
        Set(ByVal value As Decimal)
            _Saldo = value
        End Set
    End Property
    Public Property Descuneto() As Decimal
        Get
            Return _Descuneto
        End Get
        Set(ByVal value As Decimal)
            _Descuneto = value
        End Set
    End Property
    Public Property TipoPrestamo() As Integer
        Get
            Return _TipoPrestamo
        End Get
        Set(ByVal value As Integer)
            _TipoPrestamo = value
        End Set
    End Property

End Class
