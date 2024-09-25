Public Class Estado
    Private _IdEstado As Int32
    Private _Descripcion As String

    Public Sub New()
        _IdEstado = 0
        _Descripcion = String.Empty
    End Sub
    Public Property IdEstado() As Int32
        Get
            Return _IdEstado
        End Get
        Set(ByVal value As Int32)
            _IdEstado = value
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
End Class
