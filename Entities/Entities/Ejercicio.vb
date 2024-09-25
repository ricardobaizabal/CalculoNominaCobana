Public Class Ejercicio
    Private _IdEjercicio As Int32
    Private _Descripcion As String
    Private _IdUsuario As Int32

    Public Sub New()
        _IdEjercicio = 0
        _Descripcion = String.Empty
        _IdUsuario = 0
    End Sub
    Public Property IdEjercicio() As Int32
        Get
            Return _IdEjercicio
        End Get
        Set(ByVal value As Int32)
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
    Public Property IdUsuario() As Int32
        Get
            Return _IdUsuario
        End Get
        Set(ByVal value As Int32)
            _IdUsuario = value
        End Set
    End Property
End Class
