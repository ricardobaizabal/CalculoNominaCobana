Public Class UMA
    Private _id As Int32
    Private _Anio As Int32
    Private _Enero As Decimal
    Private _Febrero As Decimal
    Private _Marzo As Decimal
    Private _Abril As Decimal
    Private _Mayo As Decimal
    Private _Junio As Decimal
    Private _Julio As Decimal
    Private _Agosto As Decimal
    Private _Septiembre As Decimal
    Private _Octubre As Decimal
    Private _Noviembre As Decimal
    Private _Diciembre As Decimal
    Private _Mensual As Decimal
    Private _Anual As Decimal

    Public Sub New()
        _id = 0
        _Anio = 0
        _Enero = 0
        _Febrero = 0
        _Marzo = 0
        _Abril = 0
        _Mayo = 0
        _Junio = 0
        _Julio = 0
        _Agosto = 0
        _Septiembre = 0
        _Octubre = 0
        _Noviembre = 0
        _Diciembre = 0
        _Anual = 0
        _Mensual = 0
    End Sub
    Public Property id() As Int32
        Get
            Return _id
        End Get
        Set(ByVal value As Int32)
            _id = value
        End Set
    End Property
    Public Property Anio() As Int32
        Get
            Return _Anio
        End Get
        Set(ByVal value As Int32)
            _Anio = value
        End Set
    End Property
    Public Property Enero() As Decimal
        Get
            Return _Enero
        End Get
        Set(ByVal value As Decimal)
            _Enero = value
        End Set
    End Property
    Public Property Febrero() As Decimal
        Get
            Return _Febrero
        End Get
        Set(ByVal value As Decimal)
            _Febrero = value
        End Set
    End Property
    Public Property Marzo() As Decimal
        Get
            Return _Marzo
        End Get
        Set(ByVal value As Decimal)
            _Marzo = value
        End Set
    End Property
    Public Property Abril() As Decimal
        Get
            Return _Abril
        End Get
        Set(ByVal value As Decimal)
            _Abril = value
        End Set
    End Property
    Public Property Mayo() As Decimal
        Get
            Return _Mayo
        End Get
        Set(ByVal value As Decimal)
            _Mayo = value
        End Set
    End Property
    Public Property Junio() As Decimal
        Get
            Return _Junio
        End Get
        Set(ByVal value As Decimal)
            _Junio = value
        End Set
    End Property
    Public Property Julio() As Decimal
        Get
            Return _Julio
        End Get
        Set(ByVal value As Decimal)
            _Julio = value
        End Set
    End Property
    Public Property Agosto() As Decimal
        Get
            Return _Agosto
        End Get
        Set(ByVal value As Decimal)
            _Agosto = value
        End Set
    End Property
    Public Property Septiembre() As Decimal
        Get
            Return _Septiembre
        End Get
        Set(ByVal value As Decimal)
            _Septiembre = value
        End Set
    End Property
    Public Property Octubre() As Decimal
        Get
            Return _Octubre
        End Get
        Set(ByVal value As Decimal)
            _Octubre = value
        End Set
    End Property
    Public Property Noviembre() As Decimal
        Get
            Return _Noviembre
        End Get
        Set(ByVal value As Decimal)
            _Noviembre = value
        End Set
    End Property
    Public Property Diciembre() As Decimal
        Get
            Return _Diciembre
        End Get
        Set(ByVal value As Decimal)
            _Diciembre = value
        End Set
    End Property
    Public Property Mensual() As Decimal
        Get
            Return _Mensual
        End Get
        Set(ByVal value As Decimal)
            _Mensual = value
        End Set
    End Property
    Public Property Anual() As Decimal
        Get
            Return _Anual
        End Get
        Set(ByVal value As Decimal)
            _Anual = value
        End Set
    End Property

End Class