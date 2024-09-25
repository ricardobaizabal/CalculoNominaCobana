Public Class MetodoPago
    Private _IdMetodoPago As Int32
    Private _Descripcion As String

    Public Sub New()
        _IdMetodoPago = 0
        _Descripcion = String.Empty
    End Sub
    Public Property IdMetodoPago() As Int32
        Get
            Return _IdMetodoPago
        End Get
        Set(ByVal value As Int32)
            _IdMetodoPago = value
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
