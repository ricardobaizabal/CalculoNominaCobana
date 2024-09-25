Public Class Infonavit
    Private _Id As Integer
    Private _IdEmpresa As Integer
    Private _IdEmpleado As Integer
    Private _NoCredito As String
    Private _InicioDescuento As DateTime
    Private _FinDescuento As DateTime
    Private _TipoDescuento As Integer
    Private _ValorDescuento As Decimal
    Private _IdPeriodo As Integer
    Public Sub New()
        _Id = 0
        _IdEmpresa = 0
        _IdEmpleado = 0
        _NoCredito = String.Empty
        _TipoDescuento = 0
        _ValorDescuento = 0
        _IdPeriodo = 0
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
    Public Property NoCredito() As String
        Get
            Return _NoCredito
        End Get
        Set(ByVal value As String)
            _NoCredito = value
        End Set
    End Property
    Public Property InicioDescuento() As DateTime
        Get
            Return _InicioDescuento
        End Get
        Set(ByVal value As DateTime)
            _InicioDescuento = value
        End Set
    End Property
    Public Property FinDescuento() As DateTime
        Get
            Return _FinDescuento
        End Get
        Set(ByVal value As DateTime)
            _FinDescuento = value
        End Set
    End Property
    Public Property TipoDescuento() As Integer
        Get
            Return _TipoDescuento
        End Get
        Set(ByVal value As Integer)
            _TipoDescuento = value
        End Set
    End Property
    Public Property ValorDescuento() As Decimal
        Get
            Return _ValorDescuento
        End Get
        Set(ByVal value As Decimal)
            _ValorDescuento = value
        End Set
    End Property
    Public Property IdPeriodo() As Integer
        Get
            Return _IdPeriodo
        End Get
        Set(ByVal value As Integer)
            _IdPeriodo = value
        End Set
    End Property

End Class
