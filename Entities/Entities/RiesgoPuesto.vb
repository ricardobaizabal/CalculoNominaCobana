Public Class RiesgoPuesto
    Private _IdRiesgoPuesto As Int32
    Private _Nombre As String

    Public Sub New()
        _IdRiesgoPuesto = 0
        _Nombre = String.Empty
    End Sub
    Public Property IdRiesgoPuesto() As Int32
        Get
            Return _IdRiesgoPuesto
        End Get
        Set(ByVal value As Int32)
            _IdRiesgoPuesto = value
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
End Class
