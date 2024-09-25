Public Class UsuarioPerfil
    Private _IdPerfil As Int32
    Private _Nombre As String
    Private _FechaAlta As DateTime
    Private _FechaModificacion As DateTime
    Private _BajaBit As Boolean

    Public Sub New()
        _IdPerfil = 0
        _Nombre = String.Empty
        _FechaAlta = DateTime.Now
        _FechaModificacion = Nothing
        _BajaBit = False
    End Sub

    Public Property IdPerfil() As Int32
        Get
            Return _IdPerfil
        End Get
        Set(ByVal value As Int32)
            _IdPerfil = value
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
