Public Class RegimenContratacion
    Private _IdRegimenContratacion As Int32
    Private _Descripcion As String

    Public Sub New()
        _IdRegimenContratacion = 0
        _Descripcion = String.Empty
    End Sub
    Public Property IdRegimenContratacion() As Int32
        Get
            Return _IdRegimenContratacion
        End Get
        Set(ByVal value As Int32)
            _IdRegimenContratacion = value
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
