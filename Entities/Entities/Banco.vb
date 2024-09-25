Public Class Banco
    Private _IdBanco As Int32
    Private _Nombre As String
    Private _RazonSocial As String

    Public Sub New()
        _IdBanco = 0
        _Nombre = String.Empty
        _RazonSocial = String.Empty
    End Sub
    Public Property IdBanco() As Int32
        Get
            Return _IdBanco
        End Get
        Set(ByVal value As Int32)
            _IdBanco = value
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
    Public Property RazonSocial() As String
        Get
            Return _RazonSocial
        End Get
        Set(ByVal value As String)
            _RazonSocial = value
        End Set
    End Property
End Class
