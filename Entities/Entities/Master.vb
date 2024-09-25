Public Class Master
    Private _IdEmpresa As Int32
    Private _Nombre As String
    Private _Activo As Boolean

    Public Sub New()
        _IdEmpresa = 0
        _Nombre = String.Empty
        _Activo = 0
    End Sub
    Public Property IdEmpresa() As Int32
        Get
            Return _IdEmpresa
        End Get
        Set(ByVal value As Int32)
            _IdEmpresa = value
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
    Public Property Activo() As Boolean
        Get
            Return _Activo
        End Get
        Set(ByVal value As Boolean)
            _Activo = value
        End Set
    End Property
End Class
