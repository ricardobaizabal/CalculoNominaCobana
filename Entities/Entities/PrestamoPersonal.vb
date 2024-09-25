Public Class PrestamoPersonal
    Private _IdPrestamoPersonal As Integer
    Private _IdEmpresa As Integer
    Private _Ejercicio As Integer
    Private _TipoNomina As Integer
    Private _Periodo As Integer
    Private _NoEmpleado As Integer
    Private _CvoConcepto As Integer
    Private _Importe As Decimal
    Private _Serie As String
    Private _Folio As Integer
    Private _UUID As String

    Public Sub New()
        _IdPrestamoPersonal = 0
        _IdEmpresa = 0
        _Ejercicio = 0
        _TipoNomina = 0
        _Periodo = 0
        _NoEmpleado = 0
        _CvoConcepto = 0
        _Importe = 0
        _Serie = String.Empty
        _Folio = 0
        _UUID = String.Empty
    End Sub
    Public Property IdPrestamoPersonal() As Integer
        Get
            Return _IdPrestamoPersonal
        End Get
        Set(ByVal value As Integer)
            _IdPrestamoPersonal = value
        End Set
    End Property
    Public Property IdEmpresa() As Integer
        Get
            Return _IdEmpresa
        End Get
        Set(ByVal value As Integer)
            _IdEmpresa = value
        End Set
    End Property
    Public Property Ejercicio() As Integer
        Get
            Return _Ejercicio
        End Get
        Set(ByVal value As Integer)
            _Ejercicio = value
        End Set
    End Property
    Public Property TipoNomina() As Integer
        Get
            Return _TipoNomina
        End Get
        Set(ByVal value As Integer)
            _TipoNomina = value
        End Set
    End Property
    Public Property Periodo() As Integer
        Get
            Return _Periodo
        End Get
        Set(ByVal value As Integer)
            _Periodo = value
        End Set
    End Property
    Public Property NoEmpleado() As Integer
        Get
            Return _NoEmpleado
        End Get
        Set(ByVal value As Integer)
            _NoEmpleado = value
        End Set
    End Property
    Public Property CvoConcepto() As Integer
        Get
            Return _CvoConcepto
        End Get
        Set(ByVal value As Integer)
            _CvoConcepto = value
        End Set
    End Property
    Public Property Importe() As Decimal
        Get
            Return _Importe
        End Get
        Set(ByVal value As Decimal)
            _Importe = value
        End Set
    End Property
    Public Property Serie() As String
        Get
            Return _Serie
        End Get
        Set(ByVal value As String)
            _Serie = value
        End Set
    End Property
    Public Property Folio() As Integer
        Get
            Return _Folio
        End Get
        Set(ByVal value As Integer)
            _Folio = value
        End Set
    End Property
    Public Property UUID() As String
        Get
            Return _UUID
        End Get
        Set(ByVal value As String)
            _UUID = value
        End Set
    End Property

End Class
