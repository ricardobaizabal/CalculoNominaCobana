Public Class ErrorTimbrado

    Private _IdError As Integer
    Private _IdEmpresa As Integer
    Private _Ejercicio As Integer
    Private _TipoNomina As Integer
    Private _Periodo As Integer
    Private _NoEmpleado As Integer
    Private _IdContrato As Integer
    Private _Descripcion As String
    Private _IdUsuario As Integer
    Private _Fecha As DateTime
    Private _Tipo As String

    Public Sub New()
        _IdError = 0
        _IdEmpresa = 0
        _Ejercicio = 0
        _TipoNomina = 0
        _Periodo = 0
        _NoEmpleado = 0
        _IdContrato = 0
        _Descripcion = 0
        _IdUsuario = 0
        _Fecha = DateTime.Now
        _Tipo = String.Empty
    End Sub
    Public Property IdError() As Integer
        Get
            Return _IdError
        End Get
        Set(ByVal value As Integer)
            _IdError = value
        End Set
    End Property
    Public Property IdEmpresa() As Integer
        Get
            Return _IdEmpresa
        End Get
        Set(ByVal value As Integer)
            _IdEmpresa = value
        End Set
    End Property
    Public Property Ejercicio() As Integer
        Get
            Return _Ejercicio
        End Get
        Set(ByVal value As Integer)
            _Ejercicio = value
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
    Public Property Descripcion() As String
        Get
            Return _Descripcion
        End Get
        Set(ByVal value As String)
            _Descripcion = value
        End Set
    End Property
    Public Property IdUsuario() As Integer
        Get
            Return _IdUsuario
        End Get
        Set(ByVal value As Integer)
            _IdUsuario = value
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
    Public Property Tipo() As String
        Get
            Return _Tipo
        End Get
        Set(ByVal value As String)
            _Tipo = value
        End Set
    End Property

End Class
