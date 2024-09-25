Public Class Puesto
    Private _IdPuesto As Int32
    Private _Descripcion As String

    Public Sub New()
        _IdPuesto = 0
        _Descripcion = String.Empty
    End Sub
    Public Property IdPuesto() As Int32
        Get
            Return _IdPuesto
        End Get
        Set(ByVal value As Int32)
            _IdPuesto = value
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
