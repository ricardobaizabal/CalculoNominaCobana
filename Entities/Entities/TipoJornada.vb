Public Class TipoJornada
    Private _TipoJornada As Int32
    Private _Descripcion As String

    Public Sub New()
        _TipoJornada = 0
        _Descripcion = String.Empty
    End Sub
    Public Property TipoJornada() As Int32
        Get
            Return _TipoJornada
        End Get
        Set(ByVal value As Int32)
            _TipoJornada = value
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
