Public Class Empresa
    Private _IdEmpresa As Int32
    Private _Nombre As String
    Private _RazonSocial As String
    Private _IdUsuario As Int32
    Private _IdEjercicio As Int32
    Private _Ejercicio As String
    Public Sub New()
        _IdEmpresa = 0
        _Nombre = String.Empty
        _RazonSocial = String.Empty
    End Sub
    Public Property IdEmpresa() As Int32
        Get
            Return _IdEmpresa
        End Get
        Set(ByVal value As Int32)
            _IdEmpresa = value
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
    Public Property Ejercicio() As String
        Get
            Return _Ejercicio
        End Get
        Set(ByVal value As String)
            _Ejercicio = value
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
    Public Property Nombre() As String
        Get
            Return _Nombre
        End Get
        Set(ByVal value As String)
            _Nombre = value
        End Set
    End Property
    Public Property RazonSocial() As String
        Get
            Return _RazonSocial
        End Get
        Set(ByVal value As String)
            _RazonSocial = value
        End Set
    End Property
End Class
