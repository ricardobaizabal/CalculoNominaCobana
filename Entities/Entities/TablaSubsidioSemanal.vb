Public Class TablaSubsidioSemanal
    Private _IdTablaSubsidio As Int32
    Private _LimiteInferior As Double
    Private _LimiteSuperior As Double
    Private _CuotaFija As Double
    Private _ImporteSemanal As Double
    Public Sub New()
        _IdTablaSubsidio = 0
        _LimiteInferior = 0
        _LimiteSuperior = 0
        _CuotaFija = 0
        _ImporteSemanal = 0
    End Sub
    Public Property IdTablaSubsidio() As Int32
        Get
            Return _IdTablaSubsidio
        End Get
        Set(ByVal value As Int32)
            _IdTablaSubsidio = value
        End Set
    End Property
    Public Property LimiteInferior() As Double
        Get
            Return _LimiteInferior
        End Get
        Set(ByVal value As Double)
            _LimiteInferior = value
        End Set
    End Property
    Public Property LimiteSuperior() As Double
        Get
            Return _LimiteSuperior
        End Get
        Set(ByVal value As Double)
            _LimiteSuperior = value
        End Set
    End Property
    Public Property CuotaFija() As Double
        Get
            Return _CuotaFija
        End Get
        Set(ByVal value As Double)
            _CuotaFija = value
        End Set
    End Property
    Public Property ImporteSemanal() As Double
        Get
            Return _ImporteSemanal
        End Get
        Set(ByVal value As Double)
            _ImporteSemanal = value
        End Set
    End Property
End Class
