Public Class Periodicidadpago
    Private _IdPeriodicidad As Int32
    Private _Descripcion As String

    Public Sub New()
        _IdPeriodicidad = 0
        _Descripcion = String.Empty
    End Sub
    Public Property IdPeriodicidad() As Int32
        Get
            Return _IdPeriodicidad
        End Get
        Set(ByVal value As Int32)
            _IdPeriodicidad = value
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
