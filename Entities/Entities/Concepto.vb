Public Class Concepto
    Private _Cvo As Int32
    Private _Concepto As String
    Private _Clave As String
    Private _Tipo As String
    Private _CvoSAT As String

    Public Sub New()
        _Cvo = 0
        _Concepto = String.Empty
        _Clave = String.Empty
        _Tipo = String.Empty
        _CvoSAT = String.Empty
    End Sub
    Public Property Cvo() As Int32
        Get
            Return _Cvo
        End Get
        Set(ByVal value As Int32)
            _Cvo = value
        End Set
    End Property
    Public Property Concepto() As String
        Get
            Return _Concepto
        End Get
        Set(ByVal value As String)
            _Concepto = value
        End Set
    End Property
    Public Property Clave() As String
        Get
            Return _Clave
        End Get
        Set(ByVal value As String)
            _Clave = value
        End Set
    End Property
    Public Property Tipo() As String
        Get
            Return _Tipo
        End Get
        Set(ByVal value As String)
            _Tipo = value
        End Set
    End Property
    Public Property CvoSAT() As String
        Get
            Return _CvoSAT
        End Get
        Set(ByVal value As String)
            _CvoSAT = value
        End Set
    End Property

End Class
