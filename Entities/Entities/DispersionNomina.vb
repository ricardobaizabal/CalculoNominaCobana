Public Class DispersionNomina
    Private _id As Integer
    Private _clave_servicio As String
    Private _emisora As String
    Private _bancoid As String
    Private _fecha_proceso As DateTime
    Private _consecutivo As Integer
    Private _total_registros_enviados As Integer
    Private _importe_total_registros_enviados As Decimal
    Private _accion As Integer
    Private _periodoid As Integer
    Private _empresaid As Integer
    Private _ejercicio As Integer
    Private _tipo_nomina As Integer
    Private _tipo As String
    Private _mensaje_error As String
    Private _userid As Integer

    Public Sub New()
        _id = 0
        _clave_servicio = String.Empty
        _emisora = String.Empty
        _bancoid = String.Empty
        _consecutivo = 0
        _total_registros_enviados = 0
        _importe_total_registros_enviados = 0
        _accion = 0
        _periodoid = 0
        _empresaid = 0
        _ejercicio = 0
        _tipo_nomina = 0
        _tipo = String.Empty
        _mensaje_error = String.Empty
        _userid = 0
    End Sub

    Public Property id() As Integer
        Get
            Return _id
        End Get
        Set(ByVal value As Integer)
            _id = value
        End Set
    End Property

    Public Property clave_servicio() As String
        Get
            Return _clave_servicio
        End Get
        Set(ByVal value As String)
            _clave_servicio = value
        End Set
    End Property

    Public Property emisora() As String
        Get
            Return _emisora
        End Get
        Set(ByVal value As String)
            _emisora = value
        End Set
    End Property

    Public Property bancoid() As String
        Get
            Return _bancoid
        End Get
        Set(ByVal value As String)
            _bancoid = value
        End Set
    End Property

    Public Property fecha_proceso() As DateTime
        Get
            Return _fecha_proceso
        End Get
        Set(ByVal value As DateTime)
            _fecha_proceso = value
        End Set
    End Property

    Public Property consecutivo() As Integer
        Get
            Return _consecutivo
        End Get
        Set(ByVal value As Integer)
            _consecutivo = value
        End Set
    End Property

    Public Property total_registros_enviados() As Integer
        Get
            Return _total_registros_enviados
        End Get
        Set(ByVal value As Integer)
            _total_registros_enviados = value
        End Set
    End Property

    Public Property importe_total_registros_enviados() As Decimal
        Get
            Return _importe_total_registros_enviados
        End Get
        Set(ByVal value As Decimal)
            _importe_total_registros_enviados = value
        End Set
    End Property

    Public Property accion() As Integer
        Get
            Return _accion
        End Get
        Set(ByVal value As Integer)
            _accion = value
        End Set
    End Property

    Public Property periodoid() As Integer
        Get
            Return _periodoid
        End Get
        Set(ByVal value As Integer)
            _periodoid = value
        End Set
    End Property

    Public Property empresaid() As Integer
        Get
            Return _empresaid
        End Get
        Set(ByVal value As Integer)
            _empresaid = value
        End Set
    End Property

    Public Property ejercicio() As Integer
        Get
            Return _ejercicio
        End Get
        Set(ByVal value As Integer)
            _ejercicio = value
        End Set
    End Property

    Public Property tipo_nomina() As Integer
        Get
            Return _tipo_nomina
        End Get
        Set(ByVal value As Integer)
            _tipo_nomina = value
        End Set
    End Property

    Public Property tipo() As String
        Get
            Return _tipo
        End Get
        Set(ByVal value As String)
            _tipo = value
        End Set
    End Property

    Public Property mensaje_error() As String
        Get
            Return mensaje_error
        End Get
        Set(ByVal value As String)
            mensaje_error = value
        End Set
    End Property

    Public Property userid() As Integer
        Get
            Return _userid
        End Get
        Set(ByVal value As Integer)
            _userid = value
        End Set
    End Property

End Class