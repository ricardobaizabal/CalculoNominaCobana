Public Class TipoContrato
    Private _IdTipoContrato As Int32
    Private _Descripcion As String

    Public Sub New()
        _IdTipoContrato = 0
        _Descripcion = String.Empty
    End Sub
    Public Property IdTipoContrato() As Int32
        Get
            Return _IdTipoContrato
        End Get
        Set(ByVal value As Int32)
            _IdTipoContrato = value
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
