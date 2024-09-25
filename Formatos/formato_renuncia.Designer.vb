Partial Class formato_renuncia

    'NOTE: The following procedure is required by the telerik Reporting Designer
    'It can be modified using the telerik Reporting Designer.  
    'Do not modify it using the code editor.
    Private Sub InitializeComponent()
        Dim ReportParameter1 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter2 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter3 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter4 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter5 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Me.pageHeaderSection1 = New Telerik.Reporting.PageHeaderSection()
        Me.txtLugarExpedicion = New Telerik.Reporting.TextBox()
        Me.txtRazonSocial = New Telerik.Reporting.TextBox()
        Me.TextBox1 = New Telerik.Reporting.TextBox()
        Me.txtTexto1 = New Telerik.Reporting.TextBox()
        Me.TextBox2 = New Telerik.Reporting.TextBox()
        Me.TextBox31 = New Telerik.Reporting.TextBox()
        Me.txtFirma = New Telerik.Reporting.TextBox()
        Me.detail = New Telerik.Reporting.DetailSection()
        Me.txtTexto2 = New Telerik.Reporting.TextBox()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'pageHeaderSection1
        '
        Me.pageHeaderSection1.Height = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Inch)
        Me.pageHeaderSection1.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.txtLugarExpedicion, Me.txtRazonSocial, Me.TextBox1, Me.txtTexto1, Me.TextBox2, Me.TextBox31, Me.txtFirma, Me.txtTexto2})
        Me.pageHeaderSection1.Name = "pageHeaderSection1"
        Me.pageHeaderSection1.Style.BorderWidth.Default = New Telerik.Reporting.Drawing.Unit(0.5R, Telerik.Reporting.Drawing.UnitType.Point)
        '
        'txtLugarExpedicion
        '
        Me.txtLugarExpedicion.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.29999998211860657R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.30000004172325134R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtLugarExpedicion.Name = "txtLugarExpedicion"
        Me.txtLugarExpedicion.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.13194465637207R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtLugarExpedicion.Style.Font.Bold = False
        Me.txtLugarExpedicion.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.txtLugarExpedicion.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.txtLugarExpedicion.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.txtLugarExpedicion.Value = "= Parameters.LugarExpedicion.Value"
        '
        'txtRazonSocial
        '
        Me.txtRazonSocial.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.30000004172325134R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.60000008344650269R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtRazonSocial.Name = "txtRazonSocial"
        Me.txtRazonSocial.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.1319441795349121R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtRazonSocial.Style.Font.Bold = False
        Me.txtRazonSocial.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.txtRazonSocial.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.txtRazonSocial.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.txtRazonSocial.Value = "= Parameters.RazonSocial.Value"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.2916666567325592R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.80007869005203247R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.13194465637207R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox1.Style.Font.Bold = False
        Me.TextBox1.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox1.Value = "Presente"
        '
        'txtTexto1
        '
        Me.txtTexto1.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.2999996542930603R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(1.4409708976745606R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtTexto1.Name = "txtTexto1"
        Me.txtTexto1.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.13194465637207R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtTexto1.Style.Font.Bold = False
        Me.txtTexto1.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.txtTexto1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.txtTexto1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.txtTexto1.Value = "= Parameters.Texto1.Value"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.29166683554649353R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(6.60763692855835R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.13194465637207R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox2.Style.Font.Bold = False
        Me.TextBox2.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox2.Value = "Atentamente"
        '
        'TextBox31
        '
        Me.TextBox31.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.2916666567325592R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(7.6666412353515625R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox31.Name = "TextBox31"
        Me.TextBox31.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.1402792930603027R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox31.Style.Font.Bold = False
        Me.TextBox31.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox31.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox31.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox31.Value = "= Parameters.NombreEmpleado.Value"
        '
        'txtFirma
        '
        Me.txtFirma.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.2916666567325592R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(7.4665622711181641R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtFirma.Name = "txtFirma"
        Me.txtFirma.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.1402792930603027R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtFirma.Style.Font.Bold = False
        Me.txtFirma.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.txtFirma.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.txtFirma.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.txtFirma.Value = "______________________________________________"
        '
        'detail
        '
        Me.detail.Height = New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Inch)
        Me.detail.Name = "detail"
        Me.detail.Style.BackgroundColor = System.Drawing.Color.Empty
        '
        'txtTexto2
        '
        Me.txtTexto2.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.2916666567325592R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(1.90625R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtTexto2.Name = "txtTexto2"
        Me.txtTexto2.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.13194465637207R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtTexto2.Style.Font.Bold = False
        Me.txtTexto2.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.txtTexto2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.txtTexto2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.txtTexto2.Value = "= Parameters.Texto2.Value"
        '
        'formato_renuncia
        '
        Me.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.pageHeaderSection1, Me.detail})
        Me.PageSettings.Landscape = False
        Me.PageSettings.Margins.Bottom = New Telerik.Reporting.Drawing.Unit(1.0R, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.PageSettings.Margins.Left = New Telerik.Reporting.Drawing.Unit(1.0R, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.PageSettings.Margins.Right = New Telerik.Reporting.Drawing.Unit(1.0R, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.PageSettings.Margins.Top = New Telerik.Reporting.Drawing.Unit(1.0R, Telerik.Reporting.Drawing.UnitType.Cm)
        Me.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter
        ReportParameter1.AllowNull = True
        ReportParameter1.Name = "LugarExpedicion"
        ReportParameter2.AllowNull = True
        ReportParameter2.Name = "Texto1"
        ReportParameter3.AllowNull = True
        ReportParameter3.Name = "Texto2"
        ReportParameter4.Name = "NombreEmpleado"
        ReportParameter5.AllowNull = True
        ReportParameter5.Name = "RazonSocial"
        Me.ReportParameters.Add(ReportParameter1)
        Me.ReportParameters.Add(ReportParameter2)
        Me.ReportParameters.Add(ReportParameter3)
        Me.ReportParameters.Add(ReportParameter4)
        Me.ReportParameters.Add(ReportParameter5)
        Me.Style.BackgroundColor = System.Drawing.Color.White
        Me.Style.BorderWidth.Default = New Telerik.Reporting.Drawing.Unit(0.5R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center
        Me.Width = New Telerik.Reporting.Drawing.Unit(19.615398406982422R, Telerik.Reporting.Drawing.UnitType.Cm)
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
    Friend WithEvents pageHeaderSection1 As Telerik.Reporting.PageHeaderSection
    Friend WithEvents detail As Telerik.Reporting.DetailSection
    Friend WithEvents txtLugarExpedicion As Telerik.Reporting.TextBox
    Friend WithEvents txtRazonSocial As Telerik.Reporting.TextBox
    Friend WithEvents TextBox1 As Telerik.Reporting.TextBox
    Friend WithEvents txtTexto1 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox2 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox31 As Telerik.Reporting.TextBox
    Friend WithEvents txtFirma As Telerik.Reporting.TextBox
    Friend WithEvents txtTexto2 As Telerik.Reporting.TextBox
End Class