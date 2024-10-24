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
        Dim ReportParameter6 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter7 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Dim ReportParameter8 As Telerik.Reporting.ReportParameter = New Telerik.Reporting.ReportParameter()
        Me.pageHeaderSection1 = New Telerik.Reporting.PageHeaderSection()
        Me.txtLugarExpedicion = New Telerik.Reporting.TextBox()
        Me.txtRazonSocial = New Telerik.Reporting.TextBox()
        Me.TextBox1 = New Telerik.Reporting.TextBox()
        Me.txtTexto1 = New Telerik.Reporting.TextBox()
        Me.TextBox2 = New Telerik.Reporting.TextBox()
        Me.TextBox31 = New Telerik.Reporting.TextBox()
        Me.txtFirma = New Telerik.Reporting.TextBox()
        Me.txtTexto2 = New Telerik.Reporting.TextBox()
        Me.TextBox3 = New Telerik.Reporting.TextBox()
        Me.TextBox4 = New Telerik.Reporting.TextBox()
        Me.TextBox6 = New Telerik.Reporting.TextBox()
        Me.TextBox7 = New Telerik.Reporting.TextBox()
        Me.detail = New Telerik.Reporting.DetailSection()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'pageHeaderSection1
        '
        Me.pageHeaderSection1.Height = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Inch)
        Me.pageHeaderSection1.Items.AddRange(New Telerik.Reporting.ReportItemBase() {Me.txtLugarExpedicion, Me.txtRazonSocial, Me.TextBox1, Me.txtTexto1, Me.TextBox2, Me.TextBox31, Me.txtFirma, Me.txtTexto2, Me.TextBox3, Me.TextBox4, Me.TextBox6, Me.TextBox7})
        Me.pageHeaderSection1.Name = "pageHeaderSection1"
        Me.pageHeaderSection1.Style.BorderWidth.Default = New Telerik.Reporting.Drawing.Unit(0.5R, Telerik.Reporting.Drawing.UnitType.Point)
        '
        'txtLugarExpedicion
        '
        Me.txtLugarExpedicion.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.29999998211860657R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.30000004172325134R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtLugarExpedicion.Name = "txtLugarExpedicion"
        Me.txtLugarExpedicion.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.1236114501953125R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtLugarExpedicion.Style.Font.Bold = False
        Me.txtLugarExpedicion.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.txtLugarExpedicion.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right
        Me.txtLugarExpedicion.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.txtLugarExpedicion.Value = "= Parameters.LugarExpedicion.Value"
        '
        'txtRazonSocial
        '
        Me.txtRazonSocial.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.28999999165534973R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.75R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtRazonSocial.Name = "txtRazonSocial"
        Me.txtRazonSocial.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.1319441795349121R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtRazonSocial.Style.Font.Bold = True
        Me.txtRazonSocial.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.txtRazonSocial.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.txtRazonSocial.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.txtRazonSocial.Value = "= Parameters.RazonSocial.Value"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.2916666567325592R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(1.6802871227264404R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.13194465637207R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox1.Style.Font.Bold = False
        Me.TextBox1.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox1.Value = "PRESENTE"
        '
        'txtTexto1
        '
        Me.txtTexto1.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.28999999165534973R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(2.20277738571167R, Telerik.Reporting.Drawing.UnitType.Inch))
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
        Me.TextBox2.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.29166683554649353R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(7.4409775733947754R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.13194465637207R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox2.Style.Font.Bold = False
        Me.TextBox2.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox2.Value = "ATENTAMENTE"
        '
        'TextBox31
        '
        Me.TextBox31.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.2916666567325592R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(8.4999818801879883R, Telerik.Reporting.Drawing.UnitType.Inch))
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
        Me.txtFirma.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.2916666567325592R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(8.29990291595459R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtFirma.Name = "txtFirma"
        Me.txtFirma.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.1402792930603027R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtFirma.Style.Font.Bold = False
        Me.txtFirma.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.txtFirma.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.txtFirma.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.txtFirma.Value = "______________________________________________"
        '
        'txtTexto2
        '
        Me.txtTexto2.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.28999999165534973R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(2.632777214050293R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtTexto2.Name = "txtTexto2"
        Me.txtTexto2.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.13194465637207R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.txtTexto2.Style.Font.Bold = False
        Me.txtTexto2.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.txtTexto2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.txtTexto2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.txtTexto2.Value = "= Parameters.Texto2.Value"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.29166662693023682R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.375R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.1402792930603027R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox3.Style.Color = System.Drawing.Color.Silver
        Me.TextBox3.Style.Font.Bold = False
        Me.TextBox3.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox3.Value = "_________________________________________________________________________________" &
    "__________"
        '
        'TextBox4
        '
        Me.TextBox4.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.2916666567325592R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(1.0032037496566773R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.1319441795349121R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox4.Style.Font.Bold = True
        Me.TextBox4.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox4.Value = "= Parameters.Direccion1.Value"
        '
        'TextBox6
        '
        Me.TextBox6.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.2916666567325592R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(3.0642366409301758R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox6.Name = "TextBox6"
        Me.TextBox6.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.13194465637207R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox6.Style.Font.Bold = False
        Me.TextBox6.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox6.Value = "= Parameters.Texto3.Value"
        '
        'TextBox7
        '
        Me.TextBox7.Location = New Telerik.Reporting.Drawing.PointU(New Telerik.Reporting.Drawing.Unit(0.2916666567325592R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(1.2500193119049072R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox7.Name = "TextBox7"
        Me.TextBox7.Size = New Telerik.Reporting.Drawing.SizeU(New Telerik.Reporting.Drawing.Unit(7.1319441795349121R, Telerik.Reporting.Drawing.UnitType.Inch), New Telerik.Reporting.Drawing.Unit(0.19999992847442627R, Telerik.Reporting.Drawing.UnitType.Inch))
        Me.TextBox7.Style.Font.Bold = True
        Me.TextBox7.Style.Font.Size = New Telerik.Reporting.Drawing.Unit(10.0R, Telerik.Reporting.Drawing.UnitType.Point)
        Me.TextBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left
        Me.TextBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle
        Me.TextBox7.Value = "= Parameters.Direccion2.Value"
        '
        'detail
        '
        Me.detail.Height = New Telerik.Reporting.Drawing.Unit(0R, Telerik.Reporting.Drawing.UnitType.Inch)
        Me.detail.Name = "detail"
        Me.detail.Style.BackgroundColor = System.Drawing.Color.Empty
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
        ReportParameter4.AllowNull = True
        ReportParameter4.Name = "Texto3"
        ReportParameter5.Name = "NombreEmpleado"
        ReportParameter6.AllowNull = True
        ReportParameter6.Name = "RazonSocial"
        ReportParameter7.AllowNull = True
        ReportParameter7.Name = "Direccion1"
        ReportParameter8.AllowNull = True
        ReportParameter8.Name = "Direccion2"
        Me.ReportParameters.Add(ReportParameter1)
        Me.ReportParameters.Add(ReportParameter2)
        Me.ReportParameters.Add(ReportParameter3)
        Me.ReportParameters.Add(ReportParameter4)
        Me.ReportParameters.Add(ReportParameter5)
        Me.ReportParameters.Add(ReportParameter6)
        Me.ReportParameters.Add(ReportParameter7)
        Me.ReportParameters.Add(ReportParameter8)
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
    Friend WithEvents TextBox3 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox4 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox6 As Telerik.Reporting.TextBox
    Friend WithEvents TextBox7 As Telerik.Reporting.TextBox
End Class