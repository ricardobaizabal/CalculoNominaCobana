Public Class Catalogos
    Private _IdCatalogo As Int32
    Private _IdCatalogoStr As String
    Private _Nombre As String

    Public Sub New()
        _IdCatalogo = 0
        _IdCatalogoStr = String.Empty
        _Nombre = String.Empty
    End Sub
    Public Property IdCatalogo() As Int32
        Get
            Return _IdCatalogo
        End Get
        Set(ByVal value As Int32)
            _IdCatalogo = value
        End Set
    End Property
    Public Property IdCatalogoStr() As String
        Get
            Return _IdCatalogoStr
        End Get
        Set(ByVal value As String)
            _IdCatalogoStr = value
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