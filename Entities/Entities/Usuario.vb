Public Class Usuario
    Private _IdUsuario As Int32
    Private _Nombre As String
    Private _Email As String
    Private _Contrasena As String
    Private _IdPerfil As Int32
    Private _FechaAlta As DateTime
    Private _FechaModificacion As DateTime
    Private _BajaBit As Boolean

    Public Sub New()
        _IdUsuario = 0
        _Nombre = String.Empty
        _Email = String.Empty
        _Contrasena = String.Empty
        _IdPerfil = 0
        _FechaAlta = DateTime.Now
        _FechaModificacion = Nothing
        _BajaBit = False
    End Sub

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

    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal value As String)
            _Email = value
        End Set
    End Property

    Public Property Contrasena() As String
        Get
            Return _Contrasena
        End Get
        Set(ByVal value As String)
            _Contrasena = value
        End Set
    End Property

    Public Property IdPerfil() As Int32
        Get
            Return _IdPerfil
        End Get
        Set(ByVal value As Int32)
            _IdPerfil = value
        End Set
    End Property

    Public Property FechaAlta() As DateTime
        Get
            Return _FechaAlta
        End Get
        Set(ByVal value As DateTime)
            _FechaAlta = value
        End Set
    End Property

    Public Property FechaModificacion() As DateTime
        Get
            Return _FechaModificacion
        End Get
        Set(ByVal value As DateTime)
            _FechaModificacion = value
        End Set
    End Property

    Public Property BajaBit() As Boolean
        Get
            Return _BajaBit
        End Get
        Set(ByVal value As Boolean)
            _BajaBit = value
        End Set
    End Property

End Class
