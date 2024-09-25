Public Class TarifaSemanal
    Private _IdTarifa As Int32
    Private _LimiteInferior As Double
    Private _LimiteSuperior As Double
    Private _CuotaFija As Double
    Private _PorcSobreExcli As Double
    Private _ImporteSemanal As Decimal

    Public Sub New()
        _IdTarifa = 0
        _LimiteInferior = 0
        _LimiteSuperior = 0
        _CuotaFija = 0
        _PorcSobreExcli = 0
        _ImporteSemanal = 0
    End Sub
    Public Property IdTarifa() As Int32
        Get
            Return _IdTarifa
        End Get
        Set(ByVal value As Int32)
            _IdTarifa = value
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
    Public Property PorcSobreExcli() As Double
        Get
            Return _PorcSobreExcli
        End Get
        Set(ByVal value As Double)
            _PorcSobreExcli = value
        End Set
    End Property
    Public Property ImporteSemanal() As Decimal
        Get
            Return _ImporteSemanal
        End Get
        Set(ByVal value As Decimal)
            _ImporteSemanal = value
        End Set
    End Property
End Class
