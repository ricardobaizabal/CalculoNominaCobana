Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports System.Net.Mail
Imports System.IO
Imports System.Threading
Imports System.Globalization
Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf

Public Class AgregarEditarEmpleado
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim cConcepto As New Entities.Catalogos
        Dim objCat As New DataControl(1)

        If Not IsPostBack Then
            objCat.CatalogoRad(ddlEstado, cConcepto.ConsultarEstado, True)
            objCat.CatalogoRad(ddlEjecutivo, cConcepto.ConsultarEjecutivo, True)
            objCat.CatalogoRad(ddlPuesto, cConcepto.ConsultarPuesto, True)
            objCat.CatalogoRad(ddlEstadoBeneficiario, cConcepto.ConsultarEstado, True)
            objCat.CatalogoRad(ddlMunicipio, cConcepto.ConsultaMunicipio(ddlEstado.SelectedValue), True)
            objCat.CatalogoRad(ddlEstadoCivil, cConcepto.ConsultarEdoCivil, True)
            objCat.CatalogoRad(ddlFormaPago, cConcepto.ConsultarFormaPago, True)
            objCat.CatalogoRad(ddlBanco, cConcepto.ConsultarBanco, True)
            objCat.CatalogoRad(ddlParentescoBeneficiario, cConcepto.ConsultarParentesco, True)
            objCat.CatalogoRad(cboAnexo, cConcepto.ConsultarAnexo, True)
            objCat.CatalogoRad(ddlPlaza, cConcepto.ConsultarPlaza(Session("clienteid")), True)
            objCat.CatalogoRad(ddlTipoSalario, cConcepto.ConsultarSalario, True)
            objCat.CatalogoRad(ddlRiesgoPuesto, cConcepto.ConsultarRiesgoPuesto, True)
            objCat.CatalogoRad(ddlRegistroPatronal, cConcepto.ConsultarRiesgoPatronal2, True)
            objCat.CatalogoRad(ddlHorario, cConcepto.ConsultarHorarios, True)
            objCat.CatalogoRad(ddlPeriodoPago, cConcepto.ConsultarPeriodoPago2, True)
            objCat.CatalogoRad(ddlTipoNomina, cConcepto.ConsultarTipoNomina, True)
            objCat.CatalogoRad(ddlTipoBaja, cConcepto.ConsultarTipoBaja, True)
            objCat.CatalogoRad(ddlMotivoNoRecomendable, cConcepto.ConsultarMotivo, True)
            objCat.CatalogoRad(ddlDepartamento, cConcepto.ConsultarDepartamento, True)
            objCat.CatalogoRad(ddCliente, cConcepto.ConsultarMisClientes, True, False)
            objCat = Nothing

            Call NuevosCatalogos()

            If Request.QueryString("id").ToString() = 0 Then
                EmployeeID.Value = 0
                clearItems()
                ClearItemsContrato()
                clearItemsDBeneficiarios()
                clearItemsDIMSS()
                clearItemsDInfonavit()
                'clearItemsDPrestamos()
                ValidaTabs()
                panel1.Visible = False
                panelEmployeeRegistration.Visible = True
            Else
                EditEmployee(Request.QueryString("id"))
            End If

            RadTabStrip1.Tabs(0).Selected = True
            RadMultiPage1.SelectedIndex = 0

        End If

    End Sub
    Private Sub anexosList_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles anexosList.ItemCommand
        Dim ObjData As New DataControl(1)
        Select Case e.CommandName
            Case "cmdDelete"
                ObjData.RunSQLQuery("exec pPersonalAdministrado @cmd=14, @anexoid='" & e.CommandArgument & "'")
                If Not System.IO.File.Exists(Server.MapPath("~\anexos\") & anexosList.MasterTableView.DataKeyValues(e.Item.ItemIndex)("documento")) _
                    Then Exit Sub
                File.Delete(Server.MapPath("~\anexos\") & anexosList.MasterTableView.DataKeyValues(e.Item.ItemIndex)("documento"))
                '
                anexosList.MasterTableView.NoMasterRecordsText = "No hay registros para mostrar."
                anexosList.DataSource = muestraDocumentos()
                anexosList.DataBind()

            Case "cmdDownload"
                Try
                    Dim strPhysicalPath As String
                    Dim objFileInfo As System.IO.FileInfo

                    strPhysicalPath = Server.MapPath("~\anexos\") & e.CommandArgument

                    If Not System.IO.File.Exists(strPhysicalPath) _
                    Then Exit Sub
                    objFileInfo = New System.IO.FileInfo(strPhysicalPath)
                    Response.Clear()
                    Response.AddHeader("Content-Disposition", "attachment; filename=" &
                    objFileInfo.Name)
                    Response.AddHeader("Content-Length", objFileInfo.Length.ToString())
                    Response.ContentType = "application/octet-stream"
                    Response.WriteFile(objFileInfo.FullName)

                Catch ex As Exception
                    Response.Write(ex.Message.ToString)
                Finally
                    '
                End Try
        End Select
        ObjData = Nothing
    End Sub
    Sub clearItems()
        'ddlCliente.SelectedValue = 0
        txtNombre.Text = ""
        txtApellidoPaterno.Text = ""
        txtApellidoMaterno.Text = ""
        rblSexo.SelectedValue = 0
        ddlEstadoCivil.SelectedValue = 0
        txtRFC.Text = ""
        txtCURP.Text = ""
        txtCalle.Text = ""
        txtNoExterior.Text = ""
        txtNoInterior.Text = ""
        txtReferencia.Text = ""
        txtColonia.Text = ""
        'txtLocalidad.Text = ""
        'txtMunicipio.Text = ""
        txtCP.Text = ""
        ddlEstado.SelectedValue = 0
        txtPais.Text = ""
        txtTelefono.Text = ""
        txtTelefonoTrabajo.Text = ""
        txtCelular.Text = ""
        txtEmail1.Text = ""
        txtEmail2.Text = ""
        txtEmail3.Text = ""
        calFechaNacimiento.Clear()
        txtLugarNacimiento.Text = ""
        txtNombrePadre.Text = ""
        txtNombreMadre.Text = ""
        txtRelojChecadorId.Text = ""
        ddlHorario.SelectedValue = 0
        ddlFormaPago.SelectedValue = 0
        ddlMunicipio.SelectedValue = 0
        ddlBanco.SelectedValue = 0
        txtSucursal.Text = ""
        txtClabe.Text = ""
    End Sub
    Sub ClearItemsContrato()
        'ddlCliente.SelectedValue = 0
        ddlEjecutivo.SelectedValue = 0
        'ddlTipoJornada.SelectedValue = 0
        dllMesesEventual.SelectedValue = 0
        ddlTipoSalario.SelectedValue = 0
        'ddlTipoContrato.SelectedValue = 0
        'ddlPeriodoPago.SelectedValue = 0
        'ddlRegimenContratacion.SelectedValue = 0
        ddlDepartamento.SelectedValue = 0
        ddlPuesto.SelectedValue = 0
        ddlPlaza.SelectedValue = 0
        calFechaIngreso.Clear()
        calFechaBaja.Clear()
        txtAnosAntiguedad.Text = 0
        ddlTipoBaja.SelectedValue = 0
        txtSueldoDiario.Text = 0
        txtSueldoDiarioIntegrado.Text = 0
        txtPercepcionesAsimilables.Text = 0
        'lblMensajeContrato.Text = ""
        lblValidaFechaBaja.Text = ""
        chkRecomendable.Checked = False
        txtComentariosNoRecomendable.Text = ""
        ddlMotivoNoRecomendable.SelectedValue = 0
        ddlRiesgoPuesto.SelectedValue = 0
        'ddlRegistroPatronal.SelectedValue = 0
        txtRelojChecadorId.Text = ""
        ddlHorario.SelectedValue = 0
    End Sub
    Sub clearItemsDBeneficiarios()
        txtNombreBeneficiario.Text = ""
        ddlParentescoBeneficiario.SelectedValue = 0
        txtOtroParentesco.Text = ""
        calfechaNacimientoBeneficiario.Clear()
        txtPorcentajeBeneficiario.Text = 0
        txtCalleBeneficiario.Text = ""
        txtNoInteriorBeneficiario.Text = ""
        txtNoExteriorBeneficiario.Text = ""
        txtColoniaBeneficiario.Text = ""
        txtMunicipioBeneficiario.Text = ""
        txtCPBeneficiario.Text = ""
        ddlEstadoBeneficiario.SelectedValue = 0
        txtPaisBeneficiario.Text = ""
    End Sub
    Sub clearItemsDIMSS()
        txtNoSegSocial.Text = ""
        txtDelegacion.Text = ""
        txtSubDelegacion.Text = ""
        txtUnidadMedicoFamiliar.Text = ""
    End Sub
    Sub clearItemsDInfonavit()
        txtNoCredito.Text = ""
        calInicioDescuento.Clear()
        calFinDescuento.Clear()
        ddlTipoDescuento.SelectedValue = 0
        txtValorDescuento.Text = 0
        lblMensajeDatosInfonavit.Text = ""
    End Sub
    Private Sub ValidaTabs()
        If EmployeeID.Value = 0 Then
            RadTabStrip1.Tabs(0).Enabled = True
            RadTabStrip1.Tabs(1).Enabled = False
            RadTabStrip1.Tabs(2).Enabled = False
            RadTabStrip1.Tabs(3).Enabled = False
            RadTabStrip1.Tabs(4).Enabled = False
            RadTabStrip1.Tabs(5).Enabled = False
            RadTabStrip1.Tabs(6).Enabled = False
            RadTabStrip1.Tabs(7).Enabled = False
            'RadTabStrip1.Tabs(8).Enabled = False
            'RadTabStrip1.Tabs(9).Enabled = False
            'RadTabStrip1.Tabs(10).Enabled = False
        Else
            RadTabStrip1.Tabs(0).Enabled = True
            RadTabStrip1.Tabs(1).Enabled = True
            RadTabStrip1.Tabs(2).Enabled = True
            RadTabStrip1.Tabs(3).Enabled = True
            RadTabStrip1.Tabs(4).Enabled = True
            RadTabStrip1.Tabs(5).Enabled = True
            RadTabStrip1.Tabs(6).Enabled = True
            RadTabStrip1.Tabs(7).Enabled = True
            'RadTabStrip1.Tabs(8).Enabled = True
            'RadTabStrip1.Tabs(9).Enabled = True
            'RadTabStrip1.Tabs(10).Enabled = False
        End If
    End Sub
    Private Sub ddlEstado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEstado.SelectedIndexChanged
        Dim cConcepto As New Entities.Catalogos
        Dim objCat As New DataControl(1)
        'objCat.Catalogo(ddlMunicipio, 0, cConcepto.ConsultaMunicipio(ddlEstado.SelectedValue), True)
        objCat.CatalogoRad(ddlMunicipio, cConcepto.ConsultaMunicipio(ddlEstado.SelectedValue), True, False)
        objCat = Nothing
    End Sub
    Private Sub btnGuardarEmpleado_Click(sender As Object, e As EventArgs) Handles btnGuardarEmpleado.Click
        If Page.IsValid Then
            Dim empleadoId As Integer = 0
            Dim FechaNacimiento As DateTime
            FechaNacimiento = calFechaNacimiento.SelectedDate

            Dim sql As String = ""

            Dim RFC As String
            Dim excedente_dinero As Decimal = 0

            RFC = LTrim(RTrim(txtRFC.Text))

            Try
                excedente_dinero = Convert.ToDecimal(txtExcedenteMonetario.ToString)
            Catch ex As Exception
                excedente_dinero = 0
            End Try

            Dim objData As New DataControl(1)
            If EmployeeID.Value = 0 Then
                Dim conn As New SqlConnection(Session("conexion"))
                Try
                    Dim cmd As New SqlCommand("EXEC pPersonalAdministrado @cmd=20, @curp='" & LTrim(RTrim(txtCURP.Text)).ToString & "'", conn)
                    conn.Open()

                    Dim rs As SqlDataReader
                    rs = cmd.ExecuteReader()

                    If rs.Read Then
                        RadWindowManager2.RadAlert("Ya existe un empleado registrado con el CURP: " & LTrim(RTrim(txtCURP.Text)).ToString, 330, 180, "Alert", "", "")
                        clearItems()
                        EmployeeID.Value = 0
                        panelEmployeeRegistration.Visible = False
                        panel1.Visible = False
                        RadTabStrip1.Tabs(0).Selected = True
                        RadMultiPage1.SelectedIndex = 0
                    Else
                        sql = "EXEC pPersonalAdministrado @cmd=1, @nombre='" & txtNombre.Text & "', @apellido_paterno='" & txtApellidoPaterno.Text & "', @apellido_materno='" & txtApellidoMaterno.Text & "', @rfc='" & RFC & "', @curp='" & LTrim(RTrim(txtCURP.Text)).ToString & "', @pais='" & txtPais.Text & "', @estadoid='" & ddlEstado.SelectedValue & "', @calle='" & txtCalle.Text & "', @no_ext='" & txtNoExterior.Text & "', @no_int='" & txtNoInterior.Text & "', @referencia='" & txtReferencia.Text & "', @colonia='" & txtColonia.Text & "', @municipioid='" & ddlMunicipio.SelectedValue & "', @codigo_postal='" & txtCP.Text & "', @telefono='" & txtTelefono.Text & "', @telefono_trabajo='" & txtTelefonoTrabajo.Text & "', @celular='" & txtCelular.Text & "', @email1='" & txtEmail1.Text & "', @email2='" & txtEmail2.Text & "', @email3='" & txtEmail3.Text & "', @sexo='" & rblSexo.SelectedValue.ToString & "', @lugar_nacimiento='" & txtLugarNacimiento.Text & "', @fecha_nacimiento='" & FechaNacimiento.ToString("yyyyMMdd") & "', @nombre_padre='" & txtNombrePadre.Text & "', @nombre_madre='" & txtNombreMadre.Text & "', @estado_civil='" & ddlEstadoCivil.SelectedValue.ToString & "', @formapagoid='" & ddlFormaPago.SelectedValue.ToString & "', @bancoid='" & ddlBanco.SelectedValue.ToString & "', @sucursal='" & txtSucursal.Text & "', @clabe='" & txtClabe.Text & "',@numtarjeta='" & txtNumTarjeta.Text & "',  @numcuenta='" & txtCuenta.Text & "',@userid='" & Session("usuarioid") & "', @mi_cliente_id = '" & ddCliente.SelectedValue & "', @sueldos_y_salarios='" & checkSueldosYSalarios.Checked & "', @excedente='" & checkExcedente.Checked & "', @excedente_dinero='" & excedente_dinero.ToString & "'"
                        EmployeeID.Value = objData.RunSQLScalarQuery(sql)
                        objData = Nothing
                        'EditEmployee(EmployeeID.Value)
                        Response.Redirect("AgregarEditarEmpleado.aspx?id=" & EmployeeID.Value.ToString)
                    End If
                Catch ex As Exception
                    Response.Write(ex.Message.ToString)
                Finally
                    conn.Close()
                    conn.Dispose()
                End Try
            Else
                objData.RunSQLScalarQuery("EXEC pPersonalAdministrado @cmd=5, @nombre='" & txtNombre.Text & "', @apellido_paterno='" & txtApellidoPaterno.Text & "', @apellido_materno='" & txtApellidoMaterno.Text & "', @rfc='" & RFC & "', @curp='" & LTrim(RTrim(txtCURP.Text)).ToString & "', @pais='" & txtPais.Text & "', @estadoid='" & ddlEstado.SelectedValue & "', @calle='" & txtCalle.Text & "', @no_ext='" & txtNoExterior.Text & "', @no_int='" & txtNoInterior.Text & "', @referencia='" & txtReferencia.Text & "', @colonia='" & txtColonia.Text & "', @municipioid='" & ddlMunicipio.SelectedValue & "', @codigo_postal='" & txtCP.Text & "', @telefono='" & txtTelefono.Text & "', @telefono_trabajo='" & txtTelefonoTrabajo.Text & "', @celular='" & txtCelular.Text & "', @email1='" & txtEmail1.Text & "', @email2='" & txtEmail2.Text & "', @email3='" & txtEmail3.Text & "', @sexo='" & rblSexo.SelectedValue.ToString & "', @lugar_nacimiento='" & txtLugarNacimiento.Text & "', @fecha_nacimiento='" & FechaNacimiento.ToString("yyyyMMdd") & "', @nombre_padre='" & txtNombrePadre.Text & "', @nombre_madre='" & txtNombreMadre.Text & "', @estado_civil='" & ddlEstadoCivil.SelectedValue.ToString & "' , @empleadoid='" & EmployeeID.Value.ToString & "', @formapagoid='" & ddlFormaPago.SelectedValue.ToString & "', @bancoid='" & ddlBanco.SelectedValue.ToString & "', @sucursal='" & txtSucursal.Text & "', @clabe='" & txtClabe.Text & "',@numtarjeta='" & txtNumTarjeta.Text & "',  @numcuenta='" & txtCuenta.Text & "', @mi_cliente_id = '" & ddCliente.SelectedValue & "' , @sueldos_y_salarios='" & checkSueldosYSalarios.Checked & "' , @excedente='" & checkExcedente.Checked & "', @excedente_dinero='" & excedente_dinero.ToString & "'")
                rwAlerta.RadAlert("Datos guardados exitosamente.", 330, 180, "Alerta", "", "")
                'lblMensajeDatosGenerales.ForeColor = Drawing.Color.Green
                'lblMensajeDatosGenerales.Text = "Datos guardados exitosamente."
                'Response.AppendHeader("Refresh", "3;url=Empleado.aspx")
            End If
            objData = Nothing

            'RadTabStrip1.Tabs(1).Selected = True
            'RadMultiPage1.SelectedIndex = 1

        End If
    End Sub
    Private Sub EditEmployee(ByVal id As Integer)

        RadTabStrip1.Tabs(0).Selected = True
        RadMultiPage1.SelectedIndex = 0

        Call ClearItemsContrato()
        panelContratos.Visible = False
        Call clearItemsDBeneficiarios()
        panelBeneficiarios.Visible = False

        Dim userid As String = Nothing
        userid = Session("usuarioid")

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pPersonalAdministrado @cmd=3, @empleadoid='" & id.ToString & "', @userid='" & userid.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                lblNoEmpleado.Text = "No. de Empleado: " + rs("clave")
                EmpresaID.Value = rs("clienteid")
                txtNoClienteFonacot.Text = rs("no_cliente_fonacot")
                txtNombre.Text = rs("nombre")
                txtApellidoPaterno.Text = rs("apellido_paterno")
                txtApellidoMaterno.Text = rs("apellido_materno")
                txtRFC.Text = rs("rfc")
                txtCURP.Text = rs("curp")
                '
                lblNombreValue.Text = rs("nombre") & " " & rs("apellido_paterno") & " " & rs("apellido_materno")
                lblClienteValue.Text = rs("cliente")
                lblRFCValue.Text = rs("rfc")
                lblNoSegSocialValue.Text = rs("no_imss")
                '
                txtNoSegSocial.Text = rs("no_imss")
                txtDelegacion.Text = rs("delegacion")
                txtSubDelegacion.Text = rs("subdelegacion")
                txtUnidadMedicoFamiliar.Text = rs("unidad_medico_familiar")
                lblZonaGeográficaValue.Text = rs("zona_geografica")
                '
                ddlEstado.SelectedValue = rs("estadoid")
                txtCalle.Text = rs("calle")
                txtNoExterior.Text = rs("no_ext")
                txtNoInterior.Text = rs("no_int")
                txtReferencia.Text = rs("referencia")
                txtColonia.Text = rs("colonia")
                'txtLocalidad.Text = rs("localidad")
                'txtMunicipio.Text = rs("municipio")
                txtCP.Text = rs("codigo_postal")
                txtPais.Text = rs("pais")
                txtTelefono.Text = rs("telefono")
                txtTelefonoTrabajo.Text = rs("telefono_trabajo")
                txtCelular.Text = rs("celular")
                txtEmail1.Text = rs("email1")
                txtEmail2.Text = rs("email2")
                txtEmail3.Text = rs("email3")
                rblSexo.SelectedValue = rs("sexo")
                txtLugarNacimiento.Text = rs("lugar_nacimiento")
                If rs("fecha_nacimiento").ToString <> "" Then
                    calFechaNacimiento.SelectedDate = CDate(rs("fecha_nacimiento"))
                End If
                txtNombrePadre.Text = rs("nombre_padre")
                txtNombreMadre.Text = rs("nombre_madre")
                txtSucursal.Text = rs("sucursal")
                txtClabe.Text = rs("clabe")
                txtNumTarjeta.Text = rs("numtarjeta")
                txtCuenta.Text = rs("numcuenta")
                ddlEstadoCivil.SelectedValue = rs("estado_civil")
                ddlFormaPago.SelectedValue = rs("formapagoid")
                ddCliente.SelectedValue = rs("mi_cliente_id")
                txtExcedenteMonetario.Text = rs("excedente_dinero")

                If Convert.ToBoolean(rs("sueldos_y_salarios")) Then
                    checkSueldosYSalarios.Checked = True
                Else
                    checkSueldosYSalarios.Checked = False
                End If

                If Convert.ToBoolean(rs("excedente")) Then
                    checkExcedente.Checked = True
                Else
                    checkExcedente.Checked = False
                End If

                btnAgregaContrato.Enabled = True

                btnFiniquito.Enabled = rs("altafiniquito")

                If rs("finiquitoId") = "" Then
                    FiniquitoId.Value = 0
                Else
                    FiniquitoId.Value = rs("finiquitoId")
                End If

                panelFiniquito.Visible = False
                '
                '   Datos Bancarios
                '
                Dim ObjData As New DataControl(1)
                Dim ds As New DataSet
                ds = ObjData.FillDataSet("exec pBeneficiarios @cmd=2, @empleadoid='" & id.ToString & "'")
                beneficiariosList.MasterTableView.NoMasterRecordsText = "No hay registros para mostrar."
                beneficiariosList.DataSource = ds
                beneficiariosList.DataBind()
                ds = ObjData.FillDataSet("exec pContrato @cmd=2, @empleadoid='" & id.ToString & "'")
                contratosList.MasterTableView.NoMasterRecordsText = "No hay registros para mostrar."
                contratosList.DataSource = ds
                contratosList.DataBind()
                '
                '   Infonavit
                ' 
                If rs("descuentoBit") Then
                    chkDescuentoAutomatico.Checked = True
                Else
                    chkDescuentoAutomatico.Checked = False
                End If

                txtNoCredito.Text = rs("credito_infonavit")
                If rs("inicio_descuento").ToString.Length > 0 Then
                    calInicioDescuento.SelectedDate = CDate(rs("inicio_descuento"))
                End If
                If rs("fin_descuento").ToString.Length > 0 Then
                    calFinDescuento.SelectedDate = CDate(rs("fin_descuento"))
                End If

                Dim cConcepto As New Entities.Catalogos

                Dim ObjCat As New DataControl(1)
                ObjCat.CatalogoRad(ddlTipoDescuento, "select id, nombre from tblTipoDescuentoInfonavit order by nombre asc", True)
                ObjCat.CatalogoRad(ddlMunicipio, cConcepto.ConsultaMunicipio(ddlEstado.SelectedValue), True)
                'ObjCat.CatalogoStr(ddlBanco, "select id,nombre from tblBanco order by id asc", rs("bancoid"))
                'ObjCat.CatalogoRad(ddlBanco, cConcepto.ConsultarBanco, True, True)
                ObjCat = Nothing
                cConcepto = Nothing

                ddlTipoDescuento.SelectedValue = rs("tipo_descuento")
                ddlMunicipio.SelectedValue = rs("municipioid")
                ddlBanco.SelectedValue = rs("bancoid")

                If ddlFormaPago.SelectedValue = 1 Then
                    ddlBanco.Enabled = False
                    txtSucursal.Enabled = False
                    'txtClabe.Enabled = False
                    valReqBanco.Enabled = False
                    'valReqClabe.Enabled = False
                    'valClabe.Enabled = False
                ElseIf ddlFormaPago.SelectedValue = 2 Or ddlFormaPago.SelectedValue = 4 Then
                    ddlBanco.Enabled = True
                    txtSucursal.Enabled = True
                    'txtClabe.Enabled = False
                    valReqBanco.Enabled = True
                    'valReqClabe.Enabled = False
                    'valClabe.Enabled = False
                ElseIf ddlFormaPago.SelectedValue = 3 Then
                    ddlBanco.Enabled = True
                    txtSucursal.Enabled = True
                    'txtClabe.Enabled = True
                    valReqBanco.Enabled = True
                    'valReqClabe.Enabled = True
                    'valClabe.Enabled = True
                End If
                '
                txtValorDescuento.Text = rs("valor_descuento")
                txtComentarioImss.Text = rs("comentarioBaja")

                panelEmployeeRegistration.Visible = True

                panel1.Visible = True

                EmployeeID.Value = rs("id")

                anexosList.MasterTableView.NoMasterRecordsText = "No hay registros para mostrar."
                anexosList.DataSource = muestraDocumentos()
                anexosList.DataBind()

                ValidaTabs()
                '
                '   Creditos Fonacot
                '
                creditosFonacotList.MasterTableView.NoMasterRecordsText = "No hay registros para mostrar."
                creditosFonacotList.DataSource = ObjData.FillDataSet("EXEC pCreditoFonacot  @cmd=1, @empleadoid='" & EmployeeID.Value.ToString & "'")
                creditosFonacotList.DataBind()
                '
                '   Prestamos Personales
                '
                prestamosPersonalesList.MasterTableView.NoMasterRecordsText = "No hay registros para mostrar."
                prestamosPersonalesList.DataSource = ObjData.FillDataSet("EXEC pPrestamoPersonal @cmd=1, @empleadoid='" & EmployeeID.Value.ToString & "'")
                prestamosPersonalesList.DataBind()

                panelCreditoFonacot.Visible = False
                panelPrestamoPersonal.Visible = False
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub
    Function muestraDocumentos() As DataSet

        Dim conn As New SqlConnection(Session("conexion"))
        Dim cmd As New SqlDataAdapter("EXEC pPersonalAdministrado  @cmd=13, @empleadoid='" & EmployeeID.Value.ToString & "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception


        Finally

            conn.Close()
            conn.Dispose()

        End Try

        Return ds

    End Function
    Private Sub btnGuardarDIMSS_Click(sender As Object, e As EventArgs) Handles btnGuardarDIMSS.Click
        Try
            Dim ObjData As New DataControl(1)
            ObjData.RunSQLScalarQuery("EXEC pPersonalAdministrado @cmd=15, @empleadoid='" & EmployeeID.Value.ToString & "', @no_imss='" & txtNoSegSocial.Text & "', @delegacion='" & txtDelegacion.Text & "', @subdelegacion='" & txtSubDelegacion.Text & "', @unidad_medico_familiar='" & txtUnidadMedicoFamiliar.Text & "'")
            ObjData = Nothing
            rwAlerta.RadAlert("Datos guardados exitosamente.", 330, 180, "Alerta", "", "")
            'lblMensajeDatosIMSS.ForeColor = Drawing.Color.Green
            'lblMensajeDatosIMSS.Text = "Datos guardados exitosamente."
        Catch ex As Exception
            rwAlerta.RadAlert("Error inesperado: " & ex.Message.ToString, 330, 180, "Alerta", "", "")
            'lblMensajeDatosIMSS.ForeColor = Drawing.Color.Red
            'lblMensajeDatosIMSS.Text = "Error inesperado: " & ex.Message.ToString
        End Try
        'RadTabStrip1.Tabs(3).Selected = True
        'RadMultiPage1.SelectedIndex = 3
    End Sub
    Private Sub btnCancelarDIMSS_Click(sender As Object, e As EventArgs) Handles btnCancelarDIMSS.Click
        'clearItemsDIMSS()
        'RadTabStrip1.Tabs(3).Selected = True
        'RadMultiPage1.SelectedIndex = 3
        Response.Redirect("Empleado.aspx")
    End Sub
    Private Sub beneficiariosList_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles beneficiariosList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaBeneficiario(e.CommandArgument)
            Case "cmdDelete"
                Dim ObjData As New DataControl(1)
                ObjData.RunSQLQuery("exec pBeneficiarios @cmd=5, @beneficiarioid='" & e.CommandArgument.ToString & "'")
                Dim ds As New DataSet
                ds = ObjData.FillDataSet("exec pBeneficiarios @cmd=2, @empleadoid='" & EmployeeID.Value.ToString & "'")
                beneficiariosList.MasterTableView.NoMasterRecordsText = "No hay registros para mostrar."
                beneficiariosList.DataSource = ds
                beneficiariosList.DataBind()
                ObjData = Nothing
        End Select
    End Sub
    Private Sub EditaBeneficiario(ByVal id As Integer)

        Dim conn As New SqlConnection(Session("conexion"))

        Try
            Dim cmd As New SqlCommand("EXEC pBeneficiarios @cmd=3, @beneficiarioid ='" & id.ToString & "'", conn)
            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                txtNombreBeneficiario.Text = rs("nombre")
                If rs("fecha_nacimiento").ToString <> "" Then
                    calfechaNacimientoBeneficiario.SelectedDate = CDate(rs("fecha_nacimiento"))
                End If
                txtPorcentajeBeneficiario.Text = rs("porcentaje")
                txtCalleBeneficiario.Text = rs("calle")
                txtNoInteriorBeneficiario.Text = rs("num_int")
                txtNoExteriorBeneficiario.Text = rs("num_ext")
                txtColoniaBeneficiario.Text = rs("colonia")
                txtMunicipioBeneficiario.Text = rs("municipio")
                txtCPBeneficiario.Text = rs("cp")
                ddlParentescoBeneficiario.SelectedValue = rs("parentescoid")
                ddlEstadoBeneficiario.SelectedValue = rs("estadoid")
                txtOtroParentesco.Text = rs("otro")
                If ddlParentescoBeneficiario.SelectedValue = 10 Then
                    txtOtroParentesco.Enabled = True
                Else
                    txtOtroParentesco.Enabled = False
                End If
                txtPaisBeneficiario.Text = rs("pais")
                BeneficiarioID.Value = id
                panelBeneficiarios.Visible = True
            End If

            rs.Close()

            Dim ObjData As New DataControl(1)
            Dim ds As New DataSet
            ds = ObjData.FillDataSet("exec pBeneficiarios @cmd=2, @empleadoid='" & EmployeeID.Value.ToString & "'")
            beneficiariosList.MasterTableView.NoMasterRecordsText = "No hay registros para mostrar."
            beneficiariosList.DataSource = ds
            beneficiariosList.DataBind()
            ObjData = Nothing

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub
    Private Sub beneficiariosList_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles beneficiariosList.ItemDataBound
        If TypeOf e.Item Is GridDataItem Then
            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)
            If e.Item.OwnerTableView.Name = "Beneficiarios" Then
                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('¿Está seguro que desea borrar este beneficiario de la base de datos?');")
            End If
        End If
    End Sub
    Private Sub btnAgregaBeneficiario_Click(sender As Object, e As EventArgs) Handles btnAgregaBeneficiario.Click
        BeneficiarioID.Value = 0
        clearItemsDBeneficiarios()
        panelBeneficiarios.Visible = True
    End Sub
    Private Sub btnCancelarDBeneficiarios_Click(sender As Object, e As EventArgs) Handles btnCancelarDBeneficiarios.Click
        'BeneficiarioID.Value = 0
        'clearItemsDBeneficiarios()
        'panelBeneficiarios.Visible = False
        'RadTabStrip1.Tabs(3).Selected = True
        'RadMultiPage1.SelectedIndex = 3
        Response.Redirect("Empleado.aspx")
    End Sub
    Private Sub btnGuardarDBeneficiarios_Click(sender As Object, e As EventArgs) Handles btnGuardarDBeneficiarios.Click
        Try
            Dim FechaNacimiento As DateTime
            FechaNacimiento = calfechaNacimientoBeneficiario.SelectedDate
            Dim ObjData As New DataControl(1)
            If BeneficiarioID.Value = 0 Then
                BeneficiarioID.Value = ObjData.RunSQLScalarQuery("EXEC pBeneficiarios @cmd=1, @empleadoid='" & EmployeeID.Value.ToString & "', @nombre='" & txtNombreBeneficiario.Text & "', @parentescoid='" & ddlParentescoBeneficiario.SelectedValue.ToString & "', @otro_parentesco='" & txtOtroParentesco.Text & "', @fecha_nacimiento='" & FechaNacimiento.ToString("yyyyMMdd") & "', @porcentaje='" & txtPorcentajeBeneficiario.Text & "', @calle='" & txtCalleBeneficiario.Text & "', @num_int='" & txtNoInteriorBeneficiario.Text & "', @num_ext='" & txtNoExteriorBeneficiario.Text & "', @colonia='" & txtColoniaBeneficiario.Text & "', @municipio='" & txtMunicipioBeneficiario.Text & "', @cp='" & txtCPBeneficiario.Text & "', @estadoid='" & ddlEstadoBeneficiario.SelectedValue.ToString & "', @pais='" & txtPaisBeneficiario.Text.ToString & "'")
            Else
                ObjData.RunSQLQuery("EXEC pBeneficiarios @cmd=4, @beneficiarioid='" & BeneficiarioID.Value.ToString & "', @nombre='" & txtNombreBeneficiario.Text & "', @parentescoid='" & ddlParentescoBeneficiario.SelectedValue.ToString & "', @otro_parentesco='" & txtOtroParentesco.Text & "', @fecha_nacimiento='" & FechaNacimiento.ToString("yyyyMMdd") & "', @porcentaje='" & txtPorcentajeBeneficiario.Text & "', @calle='" & txtCalleBeneficiario.Text & "', @num_int='" & txtNoInteriorBeneficiario.Text & "', @num_ext='" & txtNoExteriorBeneficiario.Text & "', @colonia='" & txtColoniaBeneficiario.Text & "', @municipio='" & txtMunicipioBeneficiario.Text & "', @cp='" & txtCPBeneficiario.Text & "', @estadoid='" & ddlEstadoBeneficiario.SelectedValue.ToString & "', @pais='" & txtPaisBeneficiario.Text.ToString & "'")
            End If
            Dim ds As New DataSet
            ds = ObjData.FillDataSet("exec pBeneficiarios @cmd=2, @empleadoid='" & EmployeeID.Value.ToString & "'")
            beneficiariosList.MasterTableView.NoMasterRecordsText = "No hay registros para mostrar."
            beneficiariosList.DataSource = ds
            beneficiariosList.DataBind()
            ObjData = Nothing

            rwAlerta.RadAlert("Datos guardados exitosamente.", 330, 180, "Alerta", "", "")
            'lblMensajeDatosBeneficiario.ForeColor = Drawing.Color.Green
            'lblMensajeDatosBeneficiario.Text = "Datos guardados exitosamente."
        Catch ex As Exception
            rwAlerta.RadAlert("Error inesperado: " & ex.Message.ToString, 330, 180, "Alerta", "", "")
            'lblMensajeDatosBeneficiario.ForeColor = Drawing.Color.Red
            'lblMensajeDatosBeneficiario.Text = "Error inesperado: " & ex.Message.ToString
        End Try
        clearItemsDBeneficiarios()
        'RadTabStrip1.Tabs(2).Selected = True
        'RadMultiPage1.SelectedIndex = 2
    End Sub
    Private Sub btnCancelarDInfonavit_Click(sender As Object, e As EventArgs) Handles btnCancelarDInfonavit.Click
        'clearItemsDInfonavit()
        'RadTabStrip1.Tabs(4).Selected = True
        'RadMultiPage1.SelectedIndex = 4
        Response.Redirect("Empleado.aspx")
    End Sub
    Private Sub btnGuardarDInfonavit_Click(sender As Object, e As EventArgs) Handles btnGuardarDInfonavit.Click

        Dim empleadoid As Integer = 0
        Dim ObjData As New DataControl(1)
        Dim FechaInicio, FechaFin As DateTime
        Dim DescuentoBit As Integer = 0

        If calFinDescuento.SelectedDate.ToString.Length > 0 Then
            FechaFin = calFinDescuento.SelectedDate
        End If

        If chkDescuentoAutomatico.Checked Then
            DescuentoBit = 1
        End If

        FechaInicio = calInicioDescuento.SelectedDate

        empleadoid = ObjData.RunSQLScalarQuery("EXEC pPersonalAdministrado @cmd=11, @empleadoid='" & EmployeeID.Value.ToString & "'")

        Try
            If empleadoid = 0 Then
                If calFinDescuento.SelectedDate.ToString.Length > 0 Then
                    ObjData.RunSQLScalarQuery("EXEC pPersonalAdministrado @cmd=9, @empleadoid='" & EmployeeID.Value.ToString & "', @credito_infonavit='" & txtNoCredito.Text & "', @inicio_descuento='" & FechaInicio.ToString("yyyyMMdd") & "', @fin_descuento='" & FechaFin.ToString("yyyyMMdd") & "', @tipo_descuento='" & ddlTipoDescuento.SelectedValue.ToString & "', @valor_descuento='" & txtValorDescuento.Text & "', @descuentoBit='" & DescuentoBit.ToString & "'")
                Else
                    ObjData.RunSQLScalarQuery("EXEC pPersonalAdministrado @cmd=9, @empleadoid='" & EmployeeID.Value.ToString & "', @credito_infonavit='" & txtNoCredito.Text & "', @inicio_descuento='" & FechaInicio.ToString("yyyyMMdd") & "', @tipo_descuento='" & ddlTipoDescuento.SelectedValue.ToString & "', @valor_descuento='" & txtValorDescuento.Text & "', @descuentoBit='" & DescuentoBit.ToString & "'")
                End If
            Else
                If calFinDescuento.SelectedDate.ToString.Length > 0 Then
                    ObjData.RunSQLScalarQuery("EXEC pPersonalAdministrado @cmd=10, @empleadoid='" & EmployeeID.Value.ToString & "', @credito_infonavit='" & txtNoCredito.Text & "', @inicio_descuento='" & FechaInicio.ToString("yyyyMMdd") & "', @fin_descuento='" & FechaFin.ToString("yyyyMMdd") & "', @tipo_descuento='" & ddlTipoDescuento.SelectedValue.ToString & "', @valor_descuento='" & txtValorDescuento.Text & "', @descuentoBit='" & DescuentoBit.ToString & "'")
                Else
                    ObjData.RunSQLScalarQuery("EXEC pPersonalAdministrado @cmd=10, @empleadoid='" & EmployeeID.Value.ToString & "', @credito_infonavit='" & txtNoCredito.Text & "', @inicio_descuento='" & FechaInicio.ToString("yyyyMMdd") & "', @fin_descuento='" & "', @tipo_descuento='" & ddlTipoDescuento.SelectedValue.ToString & "', @valor_descuento='" & txtValorDescuento.Text & "', @descuentoBit='" & DescuentoBit.ToString & "'")
                End If
            End If
            ObjData = Nothing
            rwAlerta.RadAlert("Datos guardados exitosamente.", 330, 180, "Alerta", "", "")
            'lblMensajeDatosInfonavit.ForeColor = Drawing.Color.Green
            'lblMensajeDatosInfonavit.Text = "Datos guardados exitosamente."
        Catch ex As Exception
            rwAlerta.RadAlert("Error inesperado: " & ex.Message.ToString, 330, 180, "Alerta", "", "")
            'lblMensajeDatosInfonavit.ForeColor = Drawing.Color.Red
            'lblMensajeDatosInfonavit.Text = "Error inesperado: " & ex.Message.ToString
        End Try
        'RadTabStrip1.Tabs(4).Selected = True
        'RadMultiPage1.SelectedIndex = 4
    End Sub
    Private Sub btnGuardarCreditoFonacot_Click(sender As Object, e As EventArgs) Handles btnGuardarCreditoFonacot.Click
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        Try
            If txtNoClienteFonacot.Text.Trim = "" Then
                rwAlerta.RadAlert("Proporciona el No. de Cliente Fonacot", 330, 180, "Alerta", "", "")
            Else
                Dim creditoid As Integer = 0
                Dim ObjData As New DataControl(1)
                Dim ds As New DataSet
                ds = ObjData.FillDataSet("EXEC pCreditoFonacot  @cmd=3, @empleadoid='" & EmployeeID.Value.ToString & "'")

                If ds.Tables(0).Rows.Count > 0 Then
                    creditoid = ds.Tables(0).Rows(0)("id")
                End If

                Dim activobit As Integer = 0
                If chkVigenteCrditoFonacot.Checked Then
                    activobit = 1
                End If

                If CreditoFonacotID.Value = 0 And creditoid = 0 Then
                    ObjData.RunSQLQuery("EXEC pCreditoFonacot @cmd=2, @empleadoid='" & EmployeeID.Value.ToString & "', @clienteid='" & EmpresaID.Value.ToString & "', @fecha_credito='" & calFechaCreditoFonacot.SelectedDate.Value.ToShortDateString & "', @no_cliente='" & txtNoClienteFonacot.Text & "', @no_credito='" & txtNoCreditoFonacot.Text & "', @monto_credito='" & txtMontoCreditoFonacot.Text & "', @pago_minimo='" & txtPagoMinimoCreditoFonacot.Text & "', @saldo_insoluto='" & txtSaldoInsolutoCreditoFonacot.Text & "', @activobit='" & activobit.ToString & "'")
                    rwAlerta.RadAlert("Crédito agregado exitosamente", 330, 180, "Alerta", "", "")
                Else
                    If creditoid > 0 And activobit = 1 Then
                        rwAlerta.RadAlert("Actualmente el empleado cuenta con un crédito vigente.", 330, 180, "Alerta", "", "")
                    Else
                        ObjData.RunSQLQuery("EXEC pCreditoFonacot @cmd=5, @empleadoid='" & EmployeeID.Value.ToString & "', @clienteid='" & EmpresaID.Value.ToString & "', @fecha_credito='" & calFechaCreditoFonacot.SelectedDate.Value.ToShortDateString & "', @no_cliente='" & txtNoClienteFonacot.Text & "', @no_credito='" & txtNoCreditoFonacot.Text & "', @monto_credito='" & txtMontoCreditoFonacot.Text & "', @pago_minimo='" & txtPagoMinimoCreditoFonacot.Text & "', @saldo_insoluto='" & txtSaldoInsolutoCreditoFonacot.Text & "', @activobit='" & activobit.ToString & "', @id='" & CreditoFonacotID.Value.ToString & "'")
                        rwAlerta.RadAlert("Crédito actualizado exitosamente", 330, 180, "Alerta", "", "")
                    End If
                End If

                creditosFonacotList.MasterTableView.NoMasterRecordsText = "No hay registros para mostrar."
                creditosFonacotList.DataSource = ObjData.FillDataSet("EXEC pCreditoFonacot  @cmd=1, @empleadoid='" & EmployeeID.Value.ToString & "'")
                creditosFonacotList.DataBind()

                CreditoFonacotID.Value = 0
                panelCreditoFonacot.Visible = False

            End If
        Catch ex As Exception
            rwAlerta.RadAlert("Alerta: " & ex.Message.ToString.Replace(Chr(39), "").ToString, 330, 180, "Alerta", "", "")
        End Try
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
    End Sub
    Private Sub creditosFonacotList_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles creditosFonacotList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaCreditoFonacot(e.CommandArgument)
        End Select
    End Sub
    Private Sub EditaCreditoFonacot(ByVal creditoid As Integer)
        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet
        ds = ObjData.FillDataSet("EXEC pCreditoFonacot @cmd=4, @id='" & creditoid.ToString & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            chkVigenteCrditoFonacot.Checked = CBool(ds.Tables(0).Rows(0)("activobit"))
            panelCreditoFonacot.Visible = True
            txtSaldoInsolutoCreditoFonacot.Enabled = False
            CreditoFonacotID.Value = ds.Tables(0).Rows(0)("id")
            txtNoCreditoFonacot.Text = ds.Tables(0).Rows(0)("no_credito")
            calFechaCreditoFonacot.SelectedDate = CDate(ds.Tables(0).Rows(0)("fecha_credito"))
            txtMontoCreditoFonacot.Text = ds.Tables(0).Rows(0)("monto_credito")
            txtPagoMinimoCreditoFonacot.Text = ds.Tables(0).Rows(0)("pago_minimo")
            txtSaldoInsolutoCreditoFonacot.Text = ds.Tables(0).Rows(0)("saldo_insoluto")
        End If
        ObjData = Nothing
    End Sub
    Private Sub btnAgregaPrestamoFonacot_Click(sender As Object, e As EventArgs) Handles btnAgregaPrestamoFonacot.Click
        CreditoFonacotID.Value = 0
        chkVigenteCrditoFonacot.Checked = False
        txtSaldoInsolutoCreditoFonacot.Enabled = True
        calFechaCreditoFonacot.Clear()
        txtNoCreditoFonacot.Focus()
        txtNoCreditoFonacot.Text = ""
        txtMontoCreditoFonacot.Text = 0
        txtPagoMinimoCreditoFonacot.Text = 0
        txtSaldoInsolutoCreditoFonacot.Text = 0
        panelCreditoFonacot.Visible = True
    End Sub
    Private Sub btnCancelarCreditoFonacot_Click(sender As Object, e As EventArgs) Handles btnCancelarCreditoFonacot.Click
        'CreditoFonacotID.Value = 0
        'chkVigenteCrditoFonacot.Checked = False
        'txtNoCreditoFonacot.Text = ""
        'txtMontoCreditoFonacot.Text = 0
        'txtPagoMinimoCreditoFonacot.Text = 0
        'txtSaldoInsolutoCreditoFonacot.Text = 0
        'panelCreditoFonacot.Visible = False
        Response.Redirect("Empleado.aspx")
    End Sub
    Private Sub btnGuardarPrestamoPersonal_Click(sender As Object, e As EventArgs) Handles btnGuardarPrestamoPersonal.Click
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
        Try

            Dim ObjData As New DataControl(1)

            Dim activobit As Integer = 0
            If chkVigentePrestamo.Checked Then
                activobit = 1
            End If

            If PrestamoPersonalID.Value = 0 Then
                ObjData.RunSQLQuery("EXEC pPrestamoPersonal @cmd=2, @empleadoid='" & EmployeeID.Value.ToString & "', @clienteid='" & EmpresaID.Value.ToString & "', @fecha_prestamo='" & calFechaPrestamo.SelectedDate.Value.ToShortDateString & "', @monto_prestamo='" & txtMontoPrestamo.Text & "', @pago_minimo='" & txtPagoMinimoPrestamo.Text & "', @saldo_insoluto='" & txtSaldoInsolutoPrestamo.Text & "', @activobit='" & activobit.ToString & "'")
                rwAlerta.RadAlert("Préstamo personal agregado exitosamente", 330, 180, "Alerta", "", "")
            Else
                ObjData.RunSQLQuery("EXEC pPrestamoPersonal @cmd=4, @empleadoid='" & EmployeeID.Value.ToString & "', @clienteid='" & EmpresaID.Value.ToString & "', @fecha_prestamo='" & calFechaPrestamo.SelectedDate.Value.ToShortDateString & "', @monto_prestamo='" & txtMontoPrestamo.Text & "', @pago_minimo='" & txtPagoMinimoPrestamo.Text & "', @saldo_insoluto='" & txtSaldoInsolutoPrestamo.Text & "', @activobit='" & activobit.ToString & "', @id='" & PrestamoPersonalID.Value.ToString & "'")
                rwAlerta.RadAlert("Préstamo personal actualizado exitosamente", 330, 180, "Alerta", "", "")
            End If

            prestamosPersonalesList.MasterTableView.NoMasterRecordsText = "No hay registros para mostrar."
            prestamosPersonalesList.DataSource = ObjData.FillDataSet("EXEC pPrestamoPersonal @cmd=1, @empleadoid='" & EmployeeID.Value.ToString & "'")
            prestamosPersonalesList.DataBind()

            PrestamoPersonalID.Value = 0
            panelPrestamoPersonal.Visible = False

        Catch ex As Exception
            rwAlerta.RadAlert("Alerta: " & ex.Message.ToString.Replace(Chr(39), "").ToString, 330, 180, "Alerta", "", "")
        End Try
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")
    End Sub
    Private Sub prestamosPersonalesList_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles prestamosPersonalesList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaPrestamoPersonal(e.CommandArgument)
        End Select
    End Sub
    Private Sub EditaPrestamoPersonal(ByVal prestamoid As Integer)
        Dim ObjData As New DataControl(1)
        Dim ds As New DataSet
        ds = ObjData.FillDataSet("EXEC pPrestamoPersonal @cmd=3, @id='" & prestamoid.ToString & "'")
        If ds.Tables(0).Rows.Count > 0 Then
            chkVigentePrestamo.Checked = CBool(ds.Tables(0).Rows(0)("activobit"))
            panelPrestamoPersonal.Visible = True
            txtSaldoInsolutoPrestamo.Enabled = False
            PrestamoPersonalID.Value = ds.Tables(0).Rows(0)("id")
            calFechaPrestamo.SelectedDate = CDate(ds.Tables(0).Rows(0)("fecha_prestamo"))
            txtMontoPrestamo.Text = ds.Tables(0).Rows(0)("monto_prestamo")
            txtPagoMinimoPrestamo.Text = ds.Tables(0).Rows(0)("pago_minimo")
            txtSaldoInsolutoPrestamo.Text = ds.Tables(0).Rows(0)("saldo_insoluto")
        End If
        ObjData = Nothing
    End Sub
    Private Sub btnCancelarPrestamoPersonal_Click(sender As Object, e As EventArgs) Handles btnCancelarPrestamoPersonal.Click
        'PrestamoPersonalID.Value = 0
        'chkVigentePrestamo.Checked = False
        'calFechaPrestamo.Focus()
        'calFechaPrestamo.Clear()
        'txtMontoPrestamo.Text = 0
        'txtPagoMinimoPrestamo.Text = 0
        'txtSaldoInsolutoPrestamo.Text = 0
        'panelPrestamoPersonal.Visible = False
        Response.Redirect("Empleado.aspx")
    End Sub
    Private Sub btnGuardarContrato_Click(sender As Object, e As EventArgs) Handles btnGuardarContrato.Click
        If Page.IsValid Then

            Dim conn As New SqlConnection(Session("conexion"))
            Try
                Dim cmd As New SqlCommand("EXEC pPersonalAdministrado @cmd=22, @empleadoid='" & EmployeeID.Value.ToString & "'", conn)
                conn.Open()

                Dim rs As SqlDataReader
                rs = cmd.ExecuteReader()

                If rs.Read Then
                    SaveContrato()
                Else
                    RadWindowManager2.RadAlert("Es Requerido Registrar un Beneficiario antes de Registrar un Contrato.", 330, 180, "Alert", "", "")
                End If
            Catch ex As Exception
                Response.Write(ex.Message.ToString)
            Finally
                conn.Close()
                conn.Dispose()
            End Try
        End If
    End Sub
    Private Sub contratosList_ItemCommand(sender As Object, e As GridCommandEventArgs) Handles contratosList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                EditaContrato(e.CommandArgument)
            Case "cmdDownload"
                DescargarContrato(e.CommandArgument)
                'Case "cmdMovimiento"
                '    Response.Redirect("~/rh/MovIMSS.aspx?id=" & e.CommandArgument)
        End Select
    End Sub
    Private Sub EditaContrato(ByVal id As Integer)

        Thread.CurrentThread.CurrentCulture = New CultureInfo("es-MX")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("es-MX")

        'lblMensajeContrato.Text = ""
        lblValidaFechaBaja.Text = ""

        Dim conn As New SqlConnection(Session("conexion"))

        Try

            Dim cmd As New SqlCommand("EXEC pContrato @cmd=4, @contratoid='" & id.ToString & "', @userid='" & Session("userid") & "'", conn)
            'Dim cmd As New SqlCommand("EXEC pContrato @cmd=4, @contratoid='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                Dim ObjCat As New DataControl(1)

                'ddlCliente.SelectedValue = rs("clienteid")
                ddlEjecutivo.SelectedValue = rs("ejecutivoid")
                'ddlTipoJornada.SelectedValue = rs("tipo_jornadaid")
                ddlTipoSalario.SelectedValue = rs("tipo_salarioid")
                'ddlTipoNomina.SelectedValue = rs("tiponominaid")
                'ddlTipoContrato.SelectedValue = rs("tipo_contratoid")
                ddlHorario.SelectedValue = rs("IdHorario")

                If rs("tipo_contratoid") = "0" Or rs("tipo_contratoid") = "1" Or rs("tipo_contratoid") = "2" Then
                    'ObjCat.CatalogoStr(ddlTipoContrato, "select id, nombre from tblTipoContrato", rs("tipo_contratoid"))
                    ObjCat.CatalogoRad(ddlTipoContrato, "select id, nombre from tblTipoContrato order by nombre", True)
                    ddlTipoContrato.SelectedValue = rs("tipo_contratoid")
                Else
                    'ObjCat.CatalogoStr(ddlTipoContrato, "select isnull(tipo,'') as id, nombre from tblCTipoContrato order by id", rs("tipo_contratoid"))
                    ObjCat.CatalogoRad(ddlTipoContrato, "select isnull(tipo,'') as id, nombre from tblCTipoContrato order by nombre", True)
                    ddlTipoContrato.SelectedValue = rs("tipo_contratoid")
                End If

                If rs("tipo_jornadaid") = "0" Or rs("tipo_jornadaid") = "1" Or rs("tipo_jornadaid") = "2" Then
                    'ObjCat.CatalogoStr(ddlTipoJornada, "select id, nombre from tblTipoJornada order by id", rs("tipo_jornadaid"))
                    ObjCat.CatalogoRad(ddlTipoJornada, "select id, nombre from tblTipoJornada order by nombre", True)
                    ddlTipoJornada.SelectedValue = rs("tipo_jornadaid")
                Else
                    'ObjCat.Catalogo(ddlTipoJornada, "select id, nombre from tblCTipoJornada order by id", rs("tipo_jornadaid"))
                    ObjCat.CatalogoRad(ddlTipoJornada, "select isnull(tipo,'') as id, nombre from tblCTipoJornada order by nombre", True)
                    ddlTipoJornada.SelectedValue = rs("tipo_jornadaid")
                End If

                If rs("regimencontratacionid") = "0" Or rs("regimencontratacionid") = "1" Or rs("regimencontratacionid") = "2" Or rs("regimencontratacionid") = "3" Or rs("regimencontratacionid") = "4" Or rs("regimencontratacionid") = "5" Or rs("regimencontratacionid") = "6" Or rs("regimencontratacionid") = "7" Or rs("regimencontratacionid") = "8" Or rs("regimencontratacionid") = "9" Or rs("regimencontratacionid") = "10" Then
                    'ObjCat.CatalogoStr(ddlRegimenContratacion, "select id,nombre from tblRegimenContratacion order by id asc", rs("regimencontratacionid"))
                    ObjCat.CatalogoRad(ddlRegimenContratacion, "select id,nombre from tblRegimenContratacion order by id asc", True)
                    ddlRegimenContratacion.SelectedValue = rs("regimencontratacionid")
                Else
                    'ObjCat.CatalogoStr(ddlRegimenContratacion, "select id,nombre from tblCRegimenContratacion order by id asc", rs("regimencontratacionid"))
                    ObjCat.CatalogoRad(ddlRegimenContratacion, "select id,nombre from tblCRegimenContratacion order by id asc", True)
                    ddlRegimenContratacion.SelectedValue = rs("regimencontratacionid")
                End If

                ddlPeriodoPago.SelectedValue = rs("periodopagoid")
                'If rs("periodopagoid") = "0" Or rs("periodopagoid") = "1" Or rs("periodopagoid") = "2" Or rs("periodopagoid") = "3" Or rs("periodopagoid") = "4" Then
                '    ObjCat.CatalogoStr(ddlPeriodoPago, "select id, nombre from tblPeriodoPago order by id asc", rs("periodopagoid"))
                'Else
                '    ObjCat.CatalogoStr(ddlPeriodoPago, "select id, nombre from tblCPeriodoPago order by id", rs("periodopagoid"))
                'End If

                If ddlTipoContrato.SelectedValue = 1 Then
                    dllMesesEventual.Enabled = True
                Else
                    dllMesesEventual.Enabled = False
                End If

                'DESABILITAR CAMPOS CON ACCESO A CONTRATO'
                'ddlCliente.Enabled = rs("tieneacceso")
                ddlRegistroPatronal.Enabled = rs("tieneacceso")
                ddlRegimenContratacion.Enabled = rs("tieneacceso")
                calFechaIngreso.Enabled = rs("tieneacceso")
                ddlTipoJornada.Enabled = rs("tieneacceso")
                ddlTipoSalario.Enabled = rs("tieneacceso")
                ddlTipoContrato.Enabled = rs("tieneacceso")
                dllMesesEventual.Enabled = rs("tieneacceso")
                txtSueldoDiario.Enabled = rs("tieneacceso")
                ddlPeriodoPago.Enabled = rs("tieneacceso")
                txtPercepcionesAsimilables.Enabled = rs("tieneacceso")
                '''''''''''''''''''''''''''''''''''''''

                dllMesesEventual.SelectedValue = rs("mesesid")

                Dim cConcepto As New Entities.Catalogos
                'ObjCat.CatalogoStr(ddlTipoContratonomina, "select isnull(tipo,'') as id, nombre from tblCTipoContrato order by id", rs("tipo_contratonominaid"))
                'ObjCat.CatalogoStr(ddlTipoJornadanomina, "select id, nombre from tblCTipoJornada order by id", rs("tipo_jornadanominaid"))
                'ObjCat.CatalogoStr(ddlRegimenContratacionnomina, "select id,nombre from tblCRegimenContratacion order by id asc", rs("regimencontratacionnominaid"))
                'ObjCat.CatalogoStr(ddlTipoNomina, "select isnull(tipo,'') as id, nombre from tblCTipoNomina", rs("tiponominaid"))
                ObjCat.CatalogoRad(ddlPlaza, cConcepto.ConsultarPlaza(Session("clienteid")), True)
                'ObjCat.CatalogoStr(ddlDepartamento, "select id, nombre from tblDepartamentoCliente order by id ", rs("departamentoid"))
                'ObjCat.CatalogoStr(ddlMotivoNoRecomendable, "select id, nombre from tblMotivoNorecomendable order by nombre asc", rs("motivoid"))
                'ObjCat.CatalogoStr(ddlRiesgoPuesto, "select id,nombre from tblRiesgoPuesto order by id asc", rs("riesgopuestoid"))
                'ObjCat.CatalogoStr(ddlRegistroPatronal, "select id, nombre from tblRegistroPatronal order by id asc", rs("registropatronalid"))
                'ObjCat.CatalogoStr(ddlPlaza, "select id, nombre from tblPlazasCliente order by id asc", rs("plazaid"))
                'ObjCat.Catalogo(ddlRegimenContratacion, "select id,nombre from tblRegimenContratacion order by id asc", rs("regimencontratacionid"))
                'ObjCat.Catalogo(ddlPeriodoPago, "select id, nombre from tblPeriodoPago order by id asc", rs("periodopagoid"))
                'ObjCat.Catalogo(ddlGrupoPeriodoPago, "select id, nombre from tblGrupoPeriodoPago where isnull(borradobit,0)=0 and clienteid='" & Session("clienteid") & "' order by id asc", rs("grupoid"))
                ' ObjCat.Catalogo(ddlCentrodeCosto, "exec pCatalogo  @cmd=38, @clienteid='" & Session("clienteid") & "'", rs("IdCentrodecosto"))
                cConcepto = Nothing

                ddlTipoContratonomina.SelectedValue = rs("tipo_contratonominaid")
                ddlTipoJornadanomina.SelectedValue = rs("tipo_jornadanominaid")
                ddlRegimenContratacionnomina.SelectedValue = rs("regimencontratacionnominaid")
                ddlTipoNomina.SelectedValue = rs("tiponominaid")
                ddlPlaza.SelectedValue = rs("plazaid")
                ddlDepartamento.SelectedValue = rs("departamentoid")
                ddlMotivoNoRecomendable.SelectedValue = rs("motivoid")
                ddlRiesgoPuesto.SelectedValue = rs("riesgopuestoid")
                ddlRegistroPatronal.SelectedValue = rs("registropatronalid")
                ddlPuesto.SelectedValue = rs("puestoid")

                'If ddlPeriodoPago.SelectedValue > 0 Then
                '    ddlGrupoPeriodoPago.Enabled = True
                'Else
                '    ddlGrupoPeriodoPago.SelectedValue = 0
                '    ddlGrupoPeriodoPago.Enabled = False
                'End If

                ObjCat = Nothing

                If rs("fecha_alta").ToString.Length > 0 Then
                    calFechaIngreso.SelectedDate = CDate(rs("fecha_alta"))
                End If

                If rs("fecha_baja").ToString.Length > 0 Then
                    calFechaBaja.SelectedDate = CDate(rs("fecha_baja"))
                    HFechaBaja.Value = 1
                Else
                    HFechaBaja.Value = 0
                End If

                txtAnosAntiguedad.Text = rs("anos_antiguedad")
                ddlTipoBaja.SelectedValue = rs("tipo_baja")
                txtSueldoDiario.Text = rs("sueldo_diario")
                txtSueldoDiarioIntegrado.Text = rs("sueldo_diario_integrado")
                txtSalarioDiarioSJornadaReducida.Text = rs("salario_diario_jornada_reducida")
                txtPercepcionesAsimilables.Text = rs("otras_percepciones_asimilables")

                txtDescansosxsemana.Text = rs("Descansosxsemana")
                txtAsimiladoTotalS.Text = rs("Asimiladototalsemanal")
                txtImptoCedular.Text = rs("Porcentajeimptocedularestatal")
                txtPagoxHora.Text = rs("Pagoxhora")
                txtFactorComision.Text = rs("Factorcomision")
                txtPromCuotaV.Text = rs("Promediocuotavariable")
                txtFactorDestajo.Text = rs("Factordestajo")
                txtIntegradoTipo.Text = rs("Integradotipo")
                txtHorasDia.Text = rs("Horasaldia")
                txtPromedioPercepV.Text = rs("Promediopercepcionesvariables")
                chkRecomendable.Checked = rs("no_recomendableBit")
                txtComentariosNoRecomendable.Text = rs("comentario")
                txtComentariosAlta.Text = rs("comentario_alta")
                txtRelojChecadorId.Text = rs("RelojChecadorId")
                ContratoID.Value = id

                panelContratos.Visible = True
                panelBajaContrato.Visible = True

                'RadTabStrip1.Tabs(6).Selected = True
                'RadMultiPage1.SelectedIndex = 6
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-US")
        Thread.CurrentThread.CurrentUICulture = New CultureInfo("en-US")
    End Sub
    Private Sub DescargarContrato(ByVal contratoId As Long)

        Dim razonsocial As String = ""
        Dim representante_legal As String = ""
        Dim calle As String = ""
        Dim num_ext As String = ""
        Dim num_int As String = ""
        Dim pais As String = ""
        Dim estado As String = ""
        Dim municipio As String = ""
        Dim colonia As String = ""
        Dim cp As String = ""

        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlCommand("EXEC pCliente @cmd=4, @clienteId='" & Session("clienteid") & "'", conn)

        Try
            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                razonsocial = rs("razonsocial")
                representante_legal = rs("representante_legal")
                calle = rs("fac_calle")
                num_ext = rs("fac_num_ext")
                num_int = rs("fac_num_int")
                pais = rs("fac_pais")
                estado = rs("fac_estado")
                municipio = rs("fac_municipio")
                colonia = rs("fac_colonia")
                cp = rs("fac_cp")
            End If

            conn.Close()
            'conn.Dispose()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            'conn.Dispose()
        End Try

        Dim rfc_empleado As String = ""
        Dim nombre_empleado As String = ""
        Dim fecha_nacimiento_empleado As String = ""
        Dim estado_civil_empleado As String = ""
        Dim puesto_empleado As String = ""
        Dim domicilio_empleado As String = ""
        Dim fecha_inicio_labores As String = ""
        Dim fecha_contrato As String = ""
        Dim fecha_letras As String = ""
        Dim sueldo_empleado As String = ""
        Dim forma As String = ""

        'Creamos el objeto documento PDF
        Dim documentoPDF As New Document(PageSize.LETTER, 60, 60, 60, 60)

        conn = New SqlConnection(HttpContext.Current.Session("conexion").ToString)
        Try
            cmd = New SqlCommand("EXEC pContrato @cmd=5, @contratoid='" & contratoId.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                rfc_empleado = rs("rfc_empleado")
                nombre_empleado = rs("nombre_empleado")
                fecha_nacimiento_empleado = rs("fecha_nacimiento_empleado")
                estado_civil_empleado = rs("estado_civil_empleado")
                puesto_empleado = rs("puesto_empleado")
                domicilio_empleado = rs("domicilio_empleado")
                fecha_contrato = rs("fecha_contrato")
                fecha_inicio_labores = rs("fecha_inicio_labores")
                fecha_letras = rs("fecha_letras")
                sueldo_empleado = rs("sueldo_empleado")
                forma = rs("forma")
            End If
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Dim FilePath As String

        FilePath = Server.MapPath("~\rh\contratos\") & rfc_empleado & "-" & contratoId & ".pdf"

        'If Not File.Exists(FilePath) Then

        PdfWriter.GetInstance(documentoPDF, New FileStream(FilePath, FileMode.Create))
        documentoPDF.Open()

        Dim FonCouriertBold As Font = FontFactory.GetFont(FontFactory.COURIER, 10, iTextSharp.text.Font.BOLD)
        Dim FonCouriertNormal As Font = FontFactory.GetFont(FontFactory.COURIER, 10, iTextSharp.text.Font.NORMAL)

        Dim parrafo As New Paragraph("Contrato individual de trabajo por tiempo indeterminado que celebran por una parte " & razonsocial & ", representada en este acto por " & representante_legal & ", en su carácter de Representante Legal, a quien en lo sucesivo se le denominará ""LA EMPRESA"", y por la otra el (la) C. " & nombre_empleado & " a quien en lo sucesivo se le denominará ""EL TRABAJADOR"", sujetando su voluntad al tenor de las siguientes declaraciones y cláusulas:", FonCouriertBold)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("DECLARACIONES", FonCouriertBold)
        parrafo.Alignment = Element.ALIGN_CENTER
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("I.	La EMPRESA declara a través de su representante:", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("a)  Que es una sociedad mercantil constituida conforme a las leyes y cuyo objeto social le permite la celebración del presente contrato en los términos pactados en este mismo instrumento. ", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)

        parrafo = New Paragraph("b)  Que requiere del TRABAJADOR la prestación de los servicios que más adelante se especificarán.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)

        parrafo = New Paragraph("c)  Que es su deseo el contratar por tiempo indeterminado los servicios del TRABAJADOR en atención a las necesidades que prevalecen en LA EMPRESA y dado que requiere de los servicios de personal que cuente con los conocimientos, experiencia y disponibilidad para que observando las políticas establecidas y los procedimientos de trabajo pueda llegar a establecer LA EMPRESA. ", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)

        'parrafo = New Paragraph("d) Que tiene su domicilio en: Zapotlán 227-A colonia Mitras Sur; Monterrey, Nuevo León; C.P. 64020", FonCouriertNormal)
        parrafo = New Paragraph("d) Que tiene su domicilio en: " & calle & " " & num_ext & num_int & " Colonia " & colonia & "; " & municipio & ", " & estado & "; C.P. " & cp, FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("II.	El TRABAJADOR declara:", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("a) 	Que es de nacionalidad Mexicana.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)

        parrafo = New Paragraph("b) 	Que su fecha de nacimiento es el " & fecha_nacimiento_empleado, FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)

        parrafo = New Paragraph("c) 	Que su estado civil es " & estado_civil_empleado, FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)

        parrafo = New Paragraph("d) 	Que posee los conocimientos, capacidades, experiencia, aptitudes, habilidades y eficiencia requeridos por la EMPRESA para desempeñar el puesto de " & puesto_empleado, FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)

        parrafo = New Paragraph("e) 	Que tiene su domicilio " & domicilio_empleado, FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("Ambas partes declaran que se reconocen mutua y recíprocamente la personalidad para suscribir este instrumento, no mediando entre ellas incapacidad legal o vicio de consentimiento alguno; mismo que sujetan al tenor de las siguientes: ", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("CLÁUSULAS", FonCouriertBold)
        parrafo.Alignment = Element.ALIGN_CENTER
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("PRIMERA.- EL TRABAJADOR se obliga a prestar a LA EMPRESA bajo su dirección, dependencia y subordinación sus servicios personales por tiempo indeterminado, desempeñando el puesto de " & puesto_empleado & " en las instalaciones que tiene a su cargo LA EMPRESA. ", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("SEGUNDA.- Menciona EL TRABAJADOR bajo protesta de decir verdad, que tiene la capacidad y conocimientos necesarios para desempeñar con eficacia el trabajo contratado. ", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("Si antes de la fecha a que hace mención la Fracción I del artículo 47 de la Ley Federal del Trabajo, EL TRABAJADOR no demuestra su capacidad y conocimiento en el desarrollo del trabajo contratado, se dará por terminada la relación laboral, sin responsabilidad para LA EMPRESA en los términos de ese mismo artículo. Atendiendo a la naturaleza del presente instrumento, el mismo se celebra por tiempo indeterminado, comenzando sus efectos legales a la firma del presente contrato.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("TERCERA.- El TRABAJADOR laborará ya sea en el domicilio o los domicilios de la EMPRESA, o en los lugares o domicilios que ésta le señale. Esto, ya sea en el país o en el extranjero, y temporal o permanentemente. Lo anterior, conforme a las necesidades del trabajo. ", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("CUARTA.- EL TRABAJADOR se obliga a prestar sus servicios con el carácter señalado, en la inteligencia de que la determinación de estos servicios se hace en forma enunciativa y no limitativa y, por tanto, EL TRABAJADOR deberá desempeñar todas las labores anexas y conexas con su obligación principal, igualmente, se obliga a realizar cualquier otro trabajo que le ordene LA EMPRESA o sus representantes. ", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("QUINTA.- La duración de la jornada de trabajo será máxima de 48 ocho horas semanales, asignando la jornada diaria comprendida de las 9:00 a las 13:00 y de las 14:00 a las 18:00 horas; con una hora para alimentación y reposo fuera del lugar de trabajo y sin estar a disposición de LA EMPRESA, de las 13:00 a las 14:00 horas, este horario de Lunes a Viernes, y los días sábados de las 9:00 a las 14:00 horas; conviniendo igualmente en que dicho horario podrá modificarse, pero sólo mediante orden escrita de LA EMPRESA o quien haga sus veces, tomando en cuenta las necesidades del trabajo. Quedara establecido que por cada 6 días laborados EL TRABAJADOR se tomara un día de descanso semanal, programado de acuerdo a las necesidades de LA EMPRESA.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("Queda prohibido para EL TRABAJADOR laborar tiempo extraordinario. Esto, salvo que EL TRABAJADOR recabe de la EMPRESA una autorización por escrito en tal sentido. Al no presentar  la correspondiente orden escrita, se entenderá que EL TRABAJADOR no fue autorizado a laborar tiempo extraordinario y LA EMPRESA no tendrá obligación de cubrir importe alguno por dicho concepto.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("SEXTA.- El TRABAJADOR deberá avisar a la EMPRESA de las causas justificadas que le impidan concurrir a su trabajo. Esto, dentro de las 2 (dos) horas siguientes al inicio de la jornada. Lo anterior, deberá hacerse por conducto del Departamento de Personal o recursos humanos. El aviso no justificará la falta dado que EL TRABAJADOR deberá acreditar la justificación de su ausencia con el comprobante respectivo. Esto  al regresar a sus labores.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("SÉPTIMA.- Para los casos de enfermedades o accidentes será válido únicamente el certificado de incapacidad que expida el Instituto Mexicano del Seguro Social. La EMPRESA analizará si la justificación o los elementos comprobatorios de dicha justificación otorgados por EL TRABAJADOR son realmente válidos, de no considerarse así la falta será injustificada.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("OCTAVA.- El TRABAJADOR recabará de la EMPRESA la constancia escrita que autorice algún permiso para faltar a sus labores. Sin tal constancia la o las inasistencias al trabajo serán injustificadas. Desde luego que cualquier permiso que se otorgue será sin goce de sueldo.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("NOVENA.- La  EMPRESA podrá rescindir la relación laboral, sin ninguna responsabilidad, cuando EL TRABAJADOR incurra en alguna de las causas establecidas por el artículo 47 de la Ley Federal del Trabajo. Tales motivos son, entre otros, los siguientes: a) Incurrir en faltas de probidad u honradez, actos de violencia, amagos, injurias o malos tratamientos en contra de los accionistas y/o del personal directivo o administrativo de la EMPRESA, de los familiares de estos y/o de sus compañeros de trabajo b) Ocasionar, intencionalmente y en el desempeño de las labores o con motivo de ellas, perjuicios materiales en los edificios, obras, maquinaria, instrumentos, materias primas y demás objetos relacionados c) Ocasionar los perjuicios anteriores siempre que sean graves, sin dolo, pero con negligencia tal que ella sea la única causa del perjuicio d) Comprometer, por su imprudencia o descuido inexcusable, la seguridad del establecimiento o de las personas que se encuentren en él e) Cometer actos inmorales en el lugar de trabajo. f) Revelar los secretos comerciales y/o industriales o los asuntos de carácter reservado g) Tener más de tres faltas de asistencia en un periodo de treinta días, sin permiso de la EMPRESA o sin causa justificada h) Desobedecer a la EMPRESA o a sus representantes, sin causa justificada i) Negarse a adoptar las medidas preventivas o a seguir los procedimientos indicados para evitar accidentes o enfermedades j) Concurrir a sus labores en estado de embriaguez o bajo la influencia de algún narcótico o droga enervante, salvo que en este último caso, exista prescripción médica k) Cuando se le haya impuesto, mediante sentencia ejecutoriada, una pena de prisión que le impida el cumplimiento de sus labores l) Las análogas a las anteriores, de igual manera graves y de consecuencias semejantes.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("DÉCIMA.- El TRABAJADOR no realizará actividad alguna que directa o indirectamente dañe o compita con la EMPRESA. Esto, aunque la efectuara a través de terceros, siendo responsable de los daños y perjuicios que ocasionare. ", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("DÉCIMA PRIMERA.- La EMPRESA proporcionará al TRABAJADOR todo el material y equipo que requiera para la prestación de sus servicios. El TRABAJADOR estará obligado a cuidar y dar buen uso a dichos instrumentos de trabajo. ", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("DÉCIMA SEGUNDA.- El TRABAJADOR informará a la EMPRESA sobre cualquiera obra, descubrimiento, o mejora que realice con motivo de su trabajo. La EMPRESA será la única propietaria de las obras, descubrimientos, invenciones y/o mejoras y de los derechos de explotación de las patentes respectivas, que sean producto del trabajo prestado por EL TRABAJADOR a la EMPRESA. Lo anterior, en cualquier tiempo o lugar.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("DÉCIMA TERCERA.-   El TRABAJADOR podría conocer, aprender y/o desarrollar información confidencial o secretos técnicos, administrativos, comerciales y/o de fabricación de la EMPRESA, sus compañías tenedoras, afiliadas y subsidiarias y/o de los clientes de las mismas. Tal información o secretos pueden ser listas de clientes, proyectos, cotizaciones de los mismos, políticas y procedimientos administrativos, contables, financieros, de ventas y/o personales, entre otros. El TRABAJADOR se obliga a no utilizar dicha información o secretos para su beneficio personal o divulgarla a terceras personas en ningún tiempo o lugar. Lo anterior, aún y cuando la relación laboral hubiese sido terminada o suspendida, siendo responsable de los daños y perjuicios que ocasionare.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("DÉCIMA CUARTA.- El TRABAJADOR pagará a la EMPRESA los daños y perjuicios que le ocasione y/o que le pudiera ocasionar, con motivo de cualquiera de las disposiciones del presente contrato.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("DÉCIMA QUINTA.- El TRABAJADOR entregará a la EMPRESA todos los documentos, discos y/o cualesquiera otros instrumentos que contengan información propiedad de la EMPRESA, sus compañías tenedoras, afiliadas y subsidiarias y/o de los clientes de las mismas. El TRABAJADOR también entregará a la EMPRESA otros bienes propiedad de aquellas que tuviera en posesión. Lo anterior, cuando él mismo o la EMPRESA suspenda, rescinda o termine la relación laboral o inclusive cuando esta última así se lo requiera. El TRABAJADOR se obliga a no retener copias de documento, disco y/o instrumento alguno.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("DÉCIMA SEXTA.- Las partes convienen en que EL TRABAJADOR será inscrito ante el Ins-tituto Mexicano del Seguro Social y gozará de los servicios médicos y beneficios que otorga esa Institución a sus asegurados. Lo anterior, durante el tiempo que dure la relación laboral. Por lo tanto, la EMPRESA descontará del salario del TRABAJADOR el importe de las cuotas correspondientes.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("DÉCIMA SÉPTIMA.-  La EMPRESA se obliga a capacitar y/o adiestrar al TRABAJADOR, en los términos de la Ley Federal del Trabajo, así como de los planes y programas establecidos o que se establezcan al respecto en la EMPRESA.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("DÉCIMA OCTAVA.- EL TRABAJADOR estará obligado a someterse a los reconocimientos médicos que requiera la EMPRESA, para comprobar que no padece alguna adicción, incapacidad o enfermedad contagiosa o incurable. Lo anterior, en cualquier tiempo y lugar.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("DÉCIMA NOVENA.- Para los efectos del presente convenio, incluyendo envíos de avisos, correspondencia, documentos y pagos, las partes señalan como sus domicilios los mencionados en los incisos I y II de las Declaraciones, quedando obligados de notificar cualquier cambio a los mismos dentro de las 24 (veinticuatro) horas siguientes a dicho cambio.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("EL TRABAJADOR se obliga a informar a la EMPRESA de todo cambio de domicilio, en la inteligencia de que si no lo hiciera, todas las notificaciones se le hará, en el registrado en este contrato.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("VIGÉSIMA.- Ambas partes reconocen que el TRABAJADOR inició a laborar para la EMPRESA el día " & fecha_inicio_labores & ". Asimismo, como constancia de conformidad, firmamos los interesados. ", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("VIGÉSIMA PRIMERA.- Se conviene como pago que LA EMPRESA deberá cubrir al EL TRABAJADOR por los servicios prestados en los términos de este contrato, un sueldo  de: " & sueldo_empleado & " Mensual, el cual le será cubierto en forma " & forma & ", mediante efectivo, cheque, depósito y/o transferencias efectuadas a la cuenta bancaria del TRABAJADOR, que será contratada por LA EMPRESA en la institución bancaria que mejor se adapte a las necesidades administrativas y operativas de LA EMPRESA.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("La compensación mencionada anteriormente incluye el pago de séptimos días, descansos obligatorios, y cuando EL TRABAJADOR faltare injustificadamente a sus labores LA EMPRESA se encontrará facultada a reducirle el salario durante la ausencia y la parte proporcional del séptimo día.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("Asimismo, el TRABAJADOR firmará los recibos salariales correspondientes y que deberá remitir a la EMPRESA inmediatamente.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("VIGÉSIMA SEGUNDA.- EL TRABAJADOR disfrutará de los días de descanso obligatorios que contempla la Ley, percibiendo íntegramente su salario, quedando estrictamente prohibido al mismo laborarlos en beneficio de la Empresa, sin que medie previamente orden por escrito de este último en tal sentido.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("VIGÉSIMA TERCERA.- EL TRABAJADOR tendrá derecho a un periodo de vacaciones y la prima correspondiente al 25% de los días de vacaciones, de acuerdo con los artículos 76 y 80 de la Ley Federal del Trabajo. ", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("VIGÉSIMA CUARTA.- EL TRABAJADOR percibirá un aguinaldo anual equivalente a 15 días de salario o su parte proporcional en caso de haber laborado menos de un año.  El aguinaldo se pagará cada año antes del 20 de diciembre.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("VIGÉSIMA QUINTA.- Las partes se obligan al cumplimiento del reglamento interior de trabajo, en esa virtud las violaciones al presente contrato o a la ley, que no ameriten rescisión del vínculo laboral, serán sancionadas de uno a ocho días de suspensión sin goce de sueldo, según la gravedad de la falta, previa intervención del afectado.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("VIGÉSIMA SEXTA.-  Para la interpretación y cumplimiento de este contrato, las partes se someten expresamente a la jurisdicción y competencia de los jueces y tribunales con residencia en la Ciudad de Monterrey, N.L., renunciando a cualquier otro fuero que en razón de sus domicilios presentes o futuros, pudiera corresponderles.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        If fecha_contrato.Length > 0 Then
            Dim fecha As String() = fecha_contrato.Split(New Char() {"/"c})
            parrafo = New Paragraph("Bien enteradas del contenido y alcance del presente contrato, las partes lo firman por duplicado en la Ciudad de " & municipio & ", " & estado & " a " & FechaHoyLetras(Convert.ToInt32(fecha(0)), Convert.ToInt32(fecha(1)), Convert.ToInt32(fecha(2))), FonCouriertNormal)
        Else
            parrafo = New Paragraph("Bien enteradas del contenido y alcance del presente contrato, las partes lo firman por duplicado en la Ciudad de " & municipio & ", " & estado, FonCouriertNormal)
        End If

        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("              LA EMPRESA                           EL TRABAJADOR", FonCouriertBold)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))
        documentoPDF.Add(New Paragraph(" "))
        documentoPDF.Add(New Paragraph(" "))


        parrafo = New Paragraph("   ________________________________        ________________________________", FonCouriertNormal)
        documentoPDF.Add(parrafo)
        parrafo = New Paragraph("       " & representante_legal & "              " & nombre_empleado, FonCouriertNormal)
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        documentoPDF.NewPage()

        parrafo = New Paragraph(municipio & ", " & estado & " a " & fecha_letras, FonCouriertBold)
        parrafo.Alignment = Element.ALIGN_RIGHT
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        documentoPDF.Add(New Paragraph(" "))
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph(razonsocial, FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("PRESENTE.-", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("       Por la presente manifiesto que estoy de acuerdo que mi contrato de trabajo es en forma temporal y en virtud de que el suscrito no es un trabajador de planta y dada las cargas extraordinarias de trabajo de la empresa, estoy consciente en que cubriré única y exclusivamente el periodo durante el cual persistan dichas cargas de trabajo extraordinarias, por lo cual una vez concluidas las funciones por las cuales se me contrató, estoy de acuerdo en que terminará mi relación de trabajo con la empresa y automáticamente sin necesidad de aviso alguno  ni  de  ningún otro  requisito, se dará por terminada la relación de trabajo entre las partes, y cesarán todos los efectos del presente  en los términos de la fracción III del artículo 53 de la Ley Federal del Trabajo.", FonCouriertNormal)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))
        documentoPDF.Add(New Paragraph(" "))
        documentoPDF.Add(New Paragraph(" "))
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("A T E N T A M E N T E", FonCouriertBold)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))
        documentoPDF.Add(New Paragraph(" "))
        documentoPDF.Add(New Paragraph(" "))
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("*TRABAJADOR*", FonCouriertBold)
        parrafo.Alignment = Element.ALIGN_JUSTIFIED
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))
        documentoPDF.Add(New Paragraph(" "))
        documentoPDF.Add(New Paragraph(" "))
        documentoPDF.Add(New Paragraph(" "))

        parrafo = New Paragraph("   ________________________________        ________________________________", FonCouriertNormal)
        documentoPDF.Add(parrafo)
        parrafo = New Paragraph("               Nombre                                     Firma", FonCouriertNormal)
        documentoPDF.Add(parrafo)
        documentoPDF.Add(New Paragraph(" "))

        documentoPDF.AddAuthor("Recursos Humanos")
        documentoPDF.AddCreator(razonsocial)
        documentoPDF.AddKeywords("Folio: " & contratoId.ToString)
        documentoPDF.AddSubject("Contrato Laboral")
        documentoPDF.AddTitle("Contrato Laboral")
        documentoPDF.AddCreationDate()
        'Cerramos el objeto documento, guardamos y creamos el PDF
        documentoPDF.Close()
        documentoPDF = Nothing

        If File.Exists(FilePath) Then
            Dim FileName As String = Path.GetFileName(FilePath)
            Response.Clear()
            Response.ContentType = "application/octet-stream"
            Response.AddHeader("Content-Disposition", "attachment; filename=""" & FileName & """")
            Response.Flush()
            Response.WriteFile(FilePath)
            Response.End()
        End If

    End Sub
    Public Function FechaHoyLetras(ByVal d As Integer, ByVal m As Integer, ByVal a As Integer) As String
        Dim Mes() As String = {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"}
        Return Trim(d).ToString + " de " + Mes(m - 1).ToString + " del " + Trim(a).ToString
    End Function
    Private Sub contratosList_ItemDataBound(sender As Object, e As GridItemEventArgs) Handles contratosList.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem

                Dim lnkMovimiento As LinkButton = CType(e.Item.FindControl("lnkMovimiento"), LinkButton)
                lnkMovimiento.Attributes.Add("onClick", "javascript:openRadWindowMovimientosIMSS(" & e.Item.DataItem("id").ToString() & "); return false;")
                lnkMovimiento.CausesValidation = False

                Dim lnk As LinkButton = CType(e.Item.FindControl("lnkDescargarContrato"), LinkButton)
                Dim lnkAcceso As LinkButton = CType(e.Item.FindControl("lnkEdit"), LinkButton)
                lnk.Enabled = e.Item.DataItem("tienealta")
                'lnkAcceso.Enabled = e.Item.DataItem("tieneacceso")
                '

                Dim imgSend As ImageButton = CType(e.Item.FindControl("imgSend"), ImageButton)
                imgSend.Attributes.Add("onClick", "javascript:openRadWindow(" & e.Item.DataItem("id").ToString() & "); return false;")
                imgSend.CausesValidation = False

                imgSend.Enabled = e.Item.DataItem("tienealta")
        End Select
    End Sub
    Private Sub btnAgregaContrato_Click(sender As Object, e As EventArgs) Handles btnAgregaContrato.Click
        ContratoID.Value = 0
        ClearItemsContrato()
        NuevosCatalogos()
        CamposDesabilitar()
        panelContratos.Visible = True
    End Sub
    Private Sub NuevosCatalogos()
        Dim cConcepto As New Entities.Catalogos
        Dim ObjCat As New DataControl(1)
        ObjCat.CatalogoRad(ddlTipoContrato, cConcepto.ConsultaTipoContrato, True)
        ObjCat.CatalogoRad(ddlTipoJornada, cConcepto.ConsultarJornada, True)
        ObjCat.CatalogoRad(ddlRegimenContratacion, cConcepto.ConsultarRegimenContratacion, True)
        ObjCat.CatalogoRad(ddlPeriodoPago, cConcepto.ConsultarPeriodoPago, True)
        ObjCat.CatalogoRad(ddlTipoContratonomina, cConcepto.ConsultarCTipoContrato, True)
        ObjCat.CatalogoRad(ddlTipoJornadanomina, cConcepto.ConsultarCTipoJornada, True)
        ObjCat.CatalogoRad(ddlRegimenContratacionnomina, cConcepto.ConsultarCRegimenContratacion, True)
        ObjCat = Nothing
    End Sub
    Private Sub SaveContrato()
        Dim userid As String = Nothing
        Dim FechaAlta, FechaBaja As DateTime
        Dim ValidaFechaBaja As Boolean = True
        FechaAlta = calFechaIngreso.SelectedDate
        userid = Session("usuarioid")

        Dim NoRecomendableBit As Integer = 0
        If calFechaBaja.SelectedDate.ToString.Length > 0 Then
            FechaBaja = calFechaBaja.SelectedDate
        End If
        If chkRecomendable.Checked Then
            NoRecomendableBit = 1
        Else
            NoRecomendableBit = 0
        End If

        Dim ObjData As New DataControl(1)
        Try
            If txtRelojChecadorId.Text.Length = 0 Then txtRelojChecadorId.Text = "0"
            Dim sql As String = ""

            If ContratoID.Value = 0 Then
                If calFechaBaja.SelectedDate.ToString.Length > 0 Then
                    'sql = "EXEC pContrato @cmd=1, @empleadoid='" & EmployeeID.Value.ToString & "', @clienteid='" & ddlCliente.SelectedValue.ToString & "', @plazaid='" & ddlPlaza.SelectedValue.ToString & "', @ejecutivoid='" & ddlEjecutivo.SelectedValue.ToString & "', @tipo_jornadaid='" & ddlTipoJornada.SelectedValue.ToString & "', @tipo_salarioid='" & ddlTipoSalario.SelectedValue.ToString & "', @tipo_contratoid='" & ddlTipoContrato.SelectedValue.ToString & "', @mesesid='" & dllMesesEventual.SelectedValue.ToString & "', @departamentoid='" & ddlDepartamento.SelectedValue.ToString & "', @puestoid='" & ddlPuesto.SelectedValue.ToString & "', @fecha_alta='" & FechaAlta.ToString("yyyyMMdd") & "', @fecha_baja='" & FechaBaja.ToString("yyyyMMdd") & "', @anos_antiguedad='" & txtAnosAntiguedad.Text & "', @tipo_baja='" & ddlTipoBaja.SelectedValue.ToString & "', @sueldo_diario='" & txtSueldoDiario.Text.ToString & "', @sueldo_diario_integrado='" & txtSueldoDiarioIntegrado.Text.ToString & "', @otras_percepciones_asimilables='" & txtPercepcionesAsimilables.Text.ToString & "', @regimencontratacionnominaid='" & ddlRegimenContratacionnomina.SelectedValue.ToString & "', @tipo_jornadanominaid='" & ddlTipoJornadanomina.SelectedValue.ToString & "', @tipo_contratonominaid='" & ddlTipoContratonomina.SelectedValue.ToString & "', @no_recomendableBit='" & NoRecomendableBit.ToString & "', @motivoid='" & ddlMotivoNoRecomendable.SelectedValue.ToString & "', @comentario='" & txtComentariosNoRecomendable.Text.ToString & "', @riesgopuestoid='" & ddlRiesgoPuesto.SelectedValue.ToString & "', @registropatronalid='" & ddlRegistroPatronal.SelectedValue.ToString & "', @regimencontratacionid='" & ddlRegimenContratacion.SelectedValue.ToString & "',@tiponominaid='" & ddlTipoNomina.SelectedValue.ToString & "', @periodopagoid='" & ddlPeriodoPago.SelectedValue.ToString & "', @grupoid='" & ddlGrupoPeriodoPago.SelectedValue.ToString & "', @RelojChecadorId='" & txtRelojChecadorId.Text & "', @IdHorario='" & ddlHorario.SelectedValue.ToString & "', @IdCentrodecosto='" & ddlCentrodeCosto.SelectedValue.ToString & "',@IdUsuario='" & userid.ToString & "',@comentarioAlta='" & txtComentariosAlta.Text & "',@Descansosxsemana='" & txtDescansosxsemana.Text & "',@Asimiladototalsemanal='" & txtAsimiladoTotalS.Text & "',@Porcentajeimptocedularestatal='" & txtImptoCedular.Text & "',@Pagoxhora='" & txtPagoxHora.Text & "',@Factorcomision='" & txtFactorComision.Text & "',@Promediocuotavariable='" & txtPromCuotaV.Text & "', @Factordestajo='" & txtFactorDestajo.Text & "',@Integradotipo='" & txtIntegradoTipo.Text & "',@Horasaldia='" & txtHorasDia.Text & "',@Promediopercepcionesvariables='" & txtPromedioPercepV.Text & "'"
                    ObjData.RunSQLScalarQuery("EXEC pContrato @cmd=1, @empleadoid='" & EmployeeID.Value.ToString & "', @clienteid='" & Session("clienteid") & "', @plazaid='" & ddlPlaza.SelectedValue.ToString & "', @ejecutivoid='" & ddlEjecutivo.SelectedValue.ToString & "', @tipo_jornadaid='" & ddlTipoJornada.SelectedValue.ToString & "', @tipo_salarioid='" & ddlTipoSalario.SelectedValue.ToString & "', @tipo_contratoid='" & ddlTipoContrato.SelectedValue.ToString & "', @mesesid='" & dllMesesEventual.SelectedValue.ToString & "', @departamentoid='" & ddlDepartamento.SelectedValue.ToString & "', @puestoid='" & ddlPuesto.SelectedValue.ToString & "', @fecha_alta='" & FechaAlta.ToString("yyyyMMdd") & "', @fecha_baja='" & FechaBaja.ToString("yyyyMMdd") & "', @anos_antiguedad='" & txtAnosAntiguedad.Text & "', @tipo_baja='" & ddlTipoBaja.SelectedValue.ToString & "', @sueldo_diario='" & txtSueldoDiario.Text.ToString & "', @sueldo_diario_integrado='" & txtSueldoDiarioIntegrado.Text.ToString & "', @otras_percepciones_asimilables='" & txtPercepcionesAsimilables.Text.ToString & "', @regimencontratacionnominaid='" & ddlRegimenContratacionnomina.SelectedValue.ToString & "', @tipo_jornadanominaid='" & ddlTipoJornadanomina.SelectedValue.ToString & "', @tipo_contratonominaid='" & ddlTipoContratonomina.SelectedValue.ToString & "', @no_recomendableBit='" & NoRecomendableBit.ToString & "', @motivoid='" & ddlMotivoNoRecomendable.SelectedValue.ToString & "', @comentario='" & txtComentariosNoRecomendable.Text.ToString & "', @riesgopuestoid='" & ddlRiesgoPuesto.SelectedValue.ToString & "', @registropatronalid='" & ddlRegistroPatronal.SelectedValue.ToString & "', @regimencontratacionid='" & ddlRegimenContratacion.SelectedValue.ToString & "',@tiponominaid='" & ddlTipoNomina.SelectedValue.ToString & "', @periodopagoid='" & ddlPeriodoPago.SelectedValue.ToString & "', @RelojChecadorId='" & txtRelojChecadorId.Text & "', @IdHorario='" & ddlHorario.SelectedValue.ToString & "', @IdUsuario='" & userid.ToString & "',@comentarioAlta='" & txtComentariosAlta.Text & "',@Descansosxsemana='" & txtDescansosxsemana.Text & "',@Asimiladototalsemanal='" & txtAsimiladoTotalS.Text & "',@Porcentajeimptocedularestatal='" & txtImptoCedular.Text & "',@Pagoxhora='" & txtPagoxHora.Text & "',@Factorcomision='" & txtFactorComision.Text & "',@Promediocuotavariable='" & txtPromCuotaV.Text & "', @Factordestajo='" & txtFactorDestajo.Text & "',@Integradotipo='" & txtIntegradoTipo.Text & "', @Horasaldia='" & txtHorasDia.Text & "', @Promediopercepcionesvariables='" & txtPromedioPercepV.Text & "', @salario_diario_jornada_reducida='" & txtSalarioDiarioSJornadaReducida.Text & "'")
                Else
                    'sql = "EXEC pContrato @cmd=1, @empleadoid='" & EmployeeID.Value.ToString & "', @clienteid='" & ddlCliente.SelectedValue.ToString & "', @plazaid='" & ddlPlaza.SelectedValue.ToString & "', @ejecutivoid='" & ddlEjecutivo.SelectedValue.ToString & "', @tipo_jornadaid='" & ddlTipoJornada.SelectedValue.ToString & "', @tipo_salarioid='" & ddlTipoSalario.SelectedValue.ToString & "', @tipo_contratoid='" & ddlTipoContrato.SelectedValue.ToString & "', @mesesid='" & dllMesesEventual.SelectedValue.ToString & "', @departamentoid='" & ddlDepartamento.SelectedValue.ToString & "', @puestoid='" & ddlPuesto.SelectedValue.ToString & "', @fecha_alta='" & FechaAlta.ToString("yyyyMMdd") & "', @anos_antiguedad='" & txtAnosAntiguedad.Text & "', @tipo_baja='" & ddlTipoBaja.SelectedValue.ToString & "', @sueldo_diario='" & txtSueldoDiario.Text.ToString & "', @sueldo_diario_integrado='" & txtSueldoDiarioIntegrado.Text.ToString & "', @otras_percepciones_asimilables='" & txtPercepcionesAsimilables.Text.ToString & "', @regimencontratacionnominaid='" & ddlRegimenContratacionnomina.SelectedValue.ToString & "', @tipo_jornadanominaid='" & ddlTipoJornadanomina.SelectedValue.ToString & "', @tipo_contratonominaid='" & ddlTipoContratonomina.SelectedValue.ToString & "', @no_recomendableBit='" & NoRecomendableBit.ToString & "', @motivoid='" & ddlMotivoNoRecomendable.SelectedValue.ToString & "', @comentario='" & txtComentariosNoRecomendable.Text.ToString & "', @riesgopuestoid='" & ddlRiesgoPuesto.SelectedValue.ToString & "', @registropatronalid='" & ddlRegistroPatronal.SelectedValue.ToString & "', @regimencontratacionid='" & ddlRegimenContratacion.SelectedValue.ToString & "', @tiponominaid='" & ddlTipoNomina.SelectedValue.ToString & "', @periodopagoid='" & ddlPeriodoPago.SelectedValue.ToString & "', @grupoid='" & ddlGrupoPeriodoPago.SelectedValue.ToString & "', @RelojChecadorId='" & txtRelojChecadorId.Text & "', @IdHorario='" & ddlHorario.SelectedValue.ToString & "',@IdCentrodecosto='" & ddlCentrodeCosto.SelectedValue.ToString & "',@IdUsuario='" & userid.ToString & "',@comentarioAlta='" & txtComentariosAlta.Text & "',@Descansosxsemana='" & txtDescansosxsemana.Text & "',@Asimiladototalsemanal='" & txtAsimiladoTotalS.Text & "',@Porcentajeimptocedularestatal='" & txtImptoCedular.Text & "',@Pagoxhora='" & txtPagoxHora.Text & "',@Factorcomision='" & txtFactorComision.Text & "',@Promediocuotavariable='" & txtPromCuotaV.Text & "', @Factordestajo='" & txtFactorDestajo.Text & "',@Integradotipo='" & txtIntegradoTipo.Text & "',@Horasaldia='" & txtHorasDia.Text & "',@Promediopercepcionesvariables='" & txtPromedioPercepV.Text & "'"
                    ObjData.RunSQLScalarQuery("EXEC pContrato @cmd=1, @empleadoid='" & EmployeeID.Value.ToString & "', @clienteid='" & Session("clienteid") & "', @plazaid='" & ddlPlaza.SelectedValue.ToString & "', @ejecutivoid='" & ddlEjecutivo.SelectedValue.ToString & "', @tipo_jornadaid='" & ddlTipoJornada.SelectedValue.ToString & "', @tipo_salarioid='" & ddlTipoSalario.SelectedValue.ToString & "', @tipo_contratoid='" & ddlTipoContrato.SelectedValue.ToString & "', @mesesid='" & dllMesesEventual.SelectedValue.ToString & "', @departamentoid='" & ddlDepartamento.SelectedValue.ToString & "', @puestoid='" & ddlPuesto.SelectedValue.ToString & "', @fecha_alta='" & FechaAlta.ToString("yyyyMMdd") & "', @anos_antiguedad='" & txtAnosAntiguedad.Text & "', @tipo_baja='" & ddlTipoBaja.SelectedValue.ToString & "', @sueldo_diario='" & txtSueldoDiario.Text.ToString & "', @sueldo_diario_integrado='" & txtSueldoDiarioIntegrado.Text.ToString & "', @otras_percepciones_asimilables='" & txtPercepcionesAsimilables.Text.ToString & "', @regimencontratacionnominaid='" & ddlRegimenContratacionnomina.SelectedValue.ToString & "', @tipo_jornadanominaid='" & ddlTipoJornadanomina.SelectedValue.ToString & "', @tipo_contratonominaid='" & ddlTipoContratonomina.SelectedValue.ToString & "', @no_recomendableBit='" & NoRecomendableBit.ToString & "', @motivoid='" & ddlMotivoNoRecomendable.SelectedValue.ToString & "', @comentario='" & txtComentariosNoRecomendable.Text.ToString & "', @riesgopuestoid='" & ddlRiesgoPuesto.SelectedValue.ToString & "', @registropatronalid='" & ddlRegistroPatronal.SelectedValue.ToString & "', @regimencontratacionid='" & ddlRegimenContratacion.SelectedValue.ToString & "', @tiponominaid='" & ddlTipoNomina.SelectedValue.ToString & "', @periodopagoid='" & ddlPeriodoPago.SelectedValue.ToString & "', @RelojChecadorId='" & txtRelojChecadorId.Text & "', @IdHorario='" & ddlHorario.SelectedValue.ToString & "',@IdUsuario='" & userid.ToString & "',@comentarioAlta='" & txtComentariosAlta.Text & "',@Descansosxsemana='" & txtDescansosxsemana.Text & "',@Asimiladototalsemanal='" & txtAsimiladoTotalS.Text & "',@Porcentajeimptocedularestatal='" & txtImptoCedular.Text & "',@Pagoxhora='" & txtPagoxHora.Text & "',@Factorcomision='" & txtFactorComision.Text & "',@Promediocuotavariable='" & txtPromCuotaV.Text & "', @Factordestajo='" & txtFactorDestajo.Text & "',@Integradotipo='" & txtIntegradoTipo.Text & "',@Horasaldia='" & txtHorasDia.Text & "',@Promediopercepcionesvariables='" & txtPromedioPercepV.Text & "', @salario_diario_jornada_reducida='" & txtSalarioDiarioSJornadaReducida.Text & "'")
                End If
            Else
                If calFechaBaja.SelectedDate.ToString.Length > 0 Then
                    'Validar la fecha de baja
                    Dim minDate As DateTime = DateTime.Today.AddDays(-7).ToShortDateString
                    Dim maxDate As DateTime = DateTime.Today.ToShortDateString
                    Dim dt As DateTime = calFechaBaja.SelectedDate.Value.ToShortDateString

                    'Dim ValidaFechaBaja As Boolean = True

                    If HFechaBaja.Value = 0 Then
                        If (dt >= minDate And dt <= maxDate) Then
                            ValidaFechaBaja = True
                        Else
                            ValidaFechaBaja = False
                        End If
                    Else
                        ValidaFechaBaja = True
                    End If

                    If ValidaFechaBaja = True Then
                        'lblValidaFechaBaja.Text = "La fecha de baja es válida"
                        ObjData.RunSQLScalarQuery("EXEC pContrato @cmd=3, @contratoid='" & ContratoID.Value.ToString & "', @clienteid='" & Session("clienteid") & "', @plazaid='" & ddlPlaza.SelectedValue.ToString & "', @ejecutivoid='" & ddlEjecutivo.SelectedValue.ToString & "', @tipo_jornadaid='" & ddlTipoJornada.SelectedValue.ToString & "', @tipo_salarioid='" & ddlTipoSalario.SelectedValue.ToString & "', @tipo_contratoid='" & ddlTipoContrato.SelectedValue.ToString & "', @mesesid='" & dllMesesEventual.SelectedValue.ToString & "', @departamentoid='" & ddlDepartamento.SelectedValue.ToString & "', @puestoid='" & ddlPuesto.SelectedValue.ToString & "', @fecha_alta='" & FechaAlta.ToString("yyyyMMdd") & "', @fecha_baja='" & FechaBaja.ToString("yyyyMMdd") & "', @anos_antiguedad='" & txtAnosAntiguedad.Text & "', @tipo_baja='" & ddlTipoBaja.SelectedValue.ToString & "', @sueldo_diario='" & txtSueldoDiario.Text.ToString & "', @sueldo_diario_integrado='" & txtSueldoDiarioIntegrado.Text.ToString & "', @otras_percepciones_asimilables='" & txtPercepcionesAsimilables.Text.ToString & "', @regimencontratacionnominaid='" & ddlRegimenContratacionnomina.SelectedValue.ToString & "', @tipo_jornadanominaid='" & ddlTipoJornadanomina.SelectedValue.ToString & "', @tipo_contratonominaid='" & ddlTipoContratonomina.SelectedValue.ToString & "', @no_recomendableBit='" & NoRecomendableBit.ToString & "', @motivoid='" & ddlMotivoNoRecomendable.SelectedValue.ToString & "', @comentario='" & txtComentariosNoRecomendable.Text.ToString & "',@comentarioAlta='" & txtComentariosAlta.Text & "', @riesgopuestoid='" & ddlRiesgoPuesto.SelectedValue.ToString & "', @registropatronalid='" & ddlRegistroPatronal.SelectedValue.ToString & "', @regimencontratacionid='" & ddlRegimenContratacion.SelectedValue.ToString & "',@tiponominaid='" & ddlTipoNomina.SelectedValue.ToString & "', @periodopagoid='" & ddlPeriodoPago.SelectedValue.ToString & "', @RelojChecadorId='" & txtRelojChecadorId.Text & "', @IdHorario='" & ddlHorario.SelectedValue.ToString & "',@IdUsuario='" & userid.ToString & "',@comentarioImss='" & txtComentarioImss.Text & "',@Descansosxsemana='" & txtDescansosxsemana.Text & "',@Asimiladototalsemanal='" & txtAsimiladoTotalS.Text & "',@Porcentajeimptocedularestatal='" & txtImptoCedular.Text & "',@Pagoxhora='" & txtPagoxHora.Text & "',@Factorcomision='" & txtFactorComision.Text & "',@Promediocuotavariable='" & txtPromCuotaV.Text & "', @Factordestajo='" & txtFactorDestajo.Text & "',@Integradotipo='" & txtIntegradoTipo.Text & "',@Horasaldia='" & txtHorasDia.Text & "',@Promediopercepcionesvariables='" & txtPromedioPercepV.Text & "', @salario_diario_jornada_reducida='" & txtSalarioDiarioSJornadaReducida.Text & "'")
                    Else
                        lblValidaFechaBaja.Text = "La fecha de baja debe ser menor máximo 7 dias al día de hoy"
                    End If
                Else
                    ObjData.RunSQLScalarQuery("EXEC pContrato @cmd=3, @contratoid='" & ContratoID.Value.ToString & "', @clienteid='" & Session("clienteid") & "', @plazaid='" & ddlPlaza.SelectedValue.ToString & "', @ejecutivoid='" & ddlEjecutivo.SelectedValue.ToString & "', @tipo_jornadaid='" & ddlTipoJornada.SelectedValue.ToString & "', @tipo_salarioid='" & ddlTipoSalario.SelectedValue.ToString & "', @tipo_contratoid='" & ddlTipoContrato.SelectedValue.ToString & "', @mesesid='" & dllMesesEventual.SelectedValue.ToString & "', @departamentoid='" & ddlDepartamento.SelectedValue.ToString & "', @puestoid='" & ddlPuesto.SelectedValue.ToString & "', @fecha_alta='" & FechaAlta.ToString("yyyyMMdd") & "', @anos_antiguedad='" & txtAnosAntiguedad.Text & "', @tipo_baja='" & ddlTipoBaja.SelectedValue.ToString & "', @sueldo_diario='" & txtSueldoDiario.Text.ToString & "', @sueldo_diario_integrado='" & txtSueldoDiarioIntegrado.Text.ToString & "', @otras_percepciones_asimilables='" & txtPercepcionesAsimilables.Text.ToString & "', @regimencontratacionnominaid='" & ddlRegimenContratacionnomina.SelectedValue.ToString & "', @tipo_jornadanominaid='" & ddlTipoJornadanomina.SelectedValue.ToString & "', @tipo_contratonominaid='" & ddlTipoContratonomina.SelectedValue.ToString & "', @no_recomendableBit='" & NoRecomendableBit.ToString & "', @motivoid='" & ddlMotivoNoRecomendable.SelectedValue.ToString & "', @comentario='" & txtComentariosNoRecomendable.Text.ToString & "',@comentarioAlta='" & txtComentariosAlta.Text & "', @riesgopuestoid='" & ddlRiesgoPuesto.SelectedValue.ToString & "', @registropatronalid='" & ddlRegistroPatronal.SelectedValue.ToString & "', @regimencontratacionid='" & ddlRegimenContratacion.SelectedValue.ToString & "', @tiponominaid='" & ddlTipoNomina.SelectedValue.ToString & "', @periodopagoid='" & ddlPeriodoPago.SelectedValue.ToString & "',  @RelojChecadorId='" & txtRelojChecadorId.Text & "', @IdHorario='" & ddlHorario.SelectedValue.ToString & "',@IdUsuario='" & userid.ToString & "',@comentarioImss='" & txtComentarioImss.Text & "',@Descansosxsemana='" & txtDescansosxsemana.Text & "',@Asimiladototalsemanal='" & txtAsimiladoTotalS.Text & "',@Porcentajeimptocedularestatal='" & txtImptoCedular.Text & "',@Pagoxhora='" & txtPagoxHora.Text & "',@Factorcomision='" & txtFactorComision.Text & "',@Promediocuotavariable='" & txtPromCuotaV.Text & "', @Factordestajo='" & txtFactorDestajo.Text & "',@Integradotipo='" & txtIntegradoTipo.Text & "',@Horasaldia='" & txtHorasDia.Text & "',@Promediopercepcionesvariables='" & txtPromedioPercepV.Text & "', @salario_diario_jornada_reducida='" & txtSalarioDiarioSJornadaReducida.Text & "'")
                End If
                Call CamposDesabilitar()
            End If
            rwAlerta.RadAlert("Datos guardados exitosamente.", 330, 180, "Alerta", "", "")
            'lblMensajeContrato.ForeColor = Drawing.Color.Green
            'lblMensajeContrato.Text = "Datos guardados exitosamente."
        Catch ex As Exception
            rwAlerta.RadAlert("Error inesperado: " & ex.Message.ToString, 330, 180, "Alerta", "", "")
            'lblMensajeContrato.ForeColor = Drawing.Color.Red
            'lblMensajeContrato.Text = "Error inesperado: " & ex.Message.ToString
        End Try

        'If calFechaBaja.SelectedDate.ToString.Length > 0 Then
        '    ObjData.RunSQLScalarQuery("EXEC pContrato @cmd=1, @empleadoid='" & EmployeeID.Value.ToString & "'")
        'End If

        'employeeslist.MasterTableView.NoMasterRecordsText = "No hay registros para mostrar."
        'employeeslist.DataSource = GetEmployees()
        'employeeslist.DataBind()

        Dim ds As New DataSet
        ds = ObjData.FillDataSet("exec pContrato @cmd=2, @empleadoid='" & EmployeeID.Value.ToString & "'")
        contratosList.MasterTableView.NoMasterRecordsText = "No hay registros para mostrar."
        contratosList.DataSource = ds
        contratosList.DataBind()
        ObjData = Nothing

        If ValidaFechaBaja = True Then
            ContratoID.Value = 0
            Call ClearItemsContrato()
            panelContratos.Visible = False
            'RadTabStrip1.Tabs(8).Selected = True
            'lblMensajeDatosGenerales.Text = ""
            'RadMultiPage1.SelectedIndex = 8
            Call CamposDesabilitar()
            Call BorrarCamposFiniquito()
            'ContratoID.Value = 0
            'ClearItemsContrato()
            'panelContratos.Visible = False
            'RadTabStrip1.Tabs(6).Selected = True
            'RadMultiPage1.SelectedIndex = 6
            'lblValidaFechaBaja.Text = ""
        End If

    End Sub
    Private Sub btnCancelarContrato_Click(sender As Object, e As EventArgs) Handles btnCancelarContrato.Click
        'ContratoID.Value = 0
        'ClearItemsContrato()
        'panelContratos.Visible = False
        'RadTabStrip1.Tabs(8).Selected = True
        'lblMensajeDatosGenerales.Text = ""
        'RadMultiPage1.SelectedIndex = 8
        'Call CamposDesabilitar()
        'BorrarCamposFiniquito()
        Response.Redirect("Empleado.aspx")
    End Sub
    Sub CamposDesabilitar()
        ddlRegistroPatronal.Enabled = True
        ddlRegimenContratacion.Enabled = True
        calFechaIngreso.Enabled = True
        ddlTipoJornada.Enabled = True
        ddlTipoSalario.Enabled = True
        ddlTipoContrato.Enabled = True
        dllMesesEventual.Enabled = True
        txtSueldoDiario.Enabled = True
        ddlPeriodoPago.Enabled = True
        txtPercepcionesAsimilables.Enabled = True
        'ddlCliente.Enabled = True
    End Sub
    Private Sub BorrarCamposFiniquito()

        lblMensajeFiniquito.Text = ""
        cmbEjecutivo.SelectedValue = 0
        cmbEtapa.SelectedValue = 0
        calFiniquito.Clear()
        lnkDownload.Text = ""
        panelFiniquito.Visible = False
    End Sub
    Private Sub btnCancelarAnexo_Click(sender As Object, e As EventArgs) Handles btnCancelarAnexo.Click
        'cboAnexo.SelectedValue = 0
        'lblMensajeAnexo.Text = ""
        'RadTabStrip1.Tabs(4).Selected = True
        'RadMultiPage1.SelectedIndex = 4
        Response.Redirect("Empleado.aspx")
    End Sub
    Private Sub btnGuardarAnexo_Click(sender As Object, e As EventArgs) Handles btnGuardarAnexo.Click
        If Page.IsValid Then
            Dim newFileName As String
            Dim i As Integer = 0
            Dim strAnexo As String = ""
            If anexo.HasFile Then
                newFileName = i & "_" & anexo.FileName
                While File.Exists(Server.MapPath("~\anexos\") & newFileName)
                    i = i + 1
                    newFileName = i & "_" & anexo.FileName
                End While

                strAnexo = newFileName

                anexo.SaveAs(Server.MapPath("~\anexos\") & strAnexo)
                Dim ObjData As New DataControl(1)
                ObjData.RunSQLQuery("exec pPersonalAdministrado @cmd=12, @empleadoid='" & EmployeeID.Value.ToString & "', @idtipoDocumento='" & cboAnexo.SelectedValue & "', @documento='" & strAnexo.ToString & "', @userid='" & Session("usuarioid").ToString & "'")
                ObjData = Nothing

                rwAlerta.RadAlert("Datos guardados exitosamente.", 330, 180, "Alerta", "", "")
                'lblMensajeAnexo.ForeColor = Drawing.Color.Green
                'lblMensajeAnexo.Text = "Datos guardados exitosamente."

                anexosList.MasterTableView.NoMasterRecordsText = "No hay registros para mostrar."
                anexosList.DataSource = muestraDocumentos()
                anexosList.DataBind()

            End If
        End If
        'RadTabStrip1.Tabs(6).Selected = True
        'RadMultiPage1.SelectedIndex = 6
    End Sub
    Private Sub btnCerrarContrato_Click(sender As Object, e As EventArgs) Handles btnCerrarContrato.Click
        Dim cEmpleado As New Entities.Empleado
        EmployeeID.Value = 0
        'lblMensajeContrato.Text = ""
        lblValidaFechaBaja.Text = ""
        panelEmployeeRegistration.Visible = False
        'panelListaEmpleados.Visible = True
        panel1.Visible = False
        RadTabStrip1.Tabs(0).Selected = True
        RadMultiPage1.SelectedIndex = 0

        Response.Redirect("Empleado.aspx")
    End Sub
    Private Sub btnCerrarBeneficiario_Click(sender As Object, e As EventArgs) Handles btnCerrarBeneficiario.Click
        Dim cEmpleado As New Entities.Empleado
        clearItems()
        EmployeeID.Value = 0
        panelEmployeeRegistration.Visible = False
        panel1.Visible = False
        lblMensajeDatosBeneficiario.Text = ""
        RadTabStrip1.Tabs(0).Selected = True
        RadMultiPage1.SelectedIndex = 0

        Response.Redirect("Empleado.aspx")
    End Sub
    'Private Sub btnCerrarDIMSS_Click(sender As Object, e As EventArgs) Handles btnCerrarDIMSS.Click
    '    Dim cEmpleado As New Entities.Empleado
    '    clearItems()
    '    EmployeeID.Value = 0
    '    panelEmployeeRegistration.Visible = False
    '    panel1.Visible = False
    '    lblMensajeDatosIMSS.Text = ""
    '    RadTabStrip1.Tabs(0).Selected = True
    '    RadMultiPage1.SelectedIndex = 0
    '    Response.Redirect("Empleado.aspx")
    'End Sub
    'Private Sub btnCerrarDInfonavit_Click(sender As Object, e As EventArgs) Handles btnCerrarDInfonavit.Click
    '    Dim cEmpleado As New Entities.Empleado
    '    clearItems()
    '    EmployeeID.Value = 0
    '    panelEmployeeRegistration.Visible = False
    '    panel1.Visible = False
    '    lblMensajeDatosInfonavit.Text = ""
    '    RadTabStrip1.Tabs(0).Selected = True
    '    RadMultiPage1.SelectedIndex = 0


    '    Response.Redirect("Empleado.aspx")
    'End Sub
    'Private Sub btnCerrarAnexo_Click(sender As Object, e As EventArgs) Handles btnCerrarAnexo.Click
    '    Dim cEmpleado As New Entities.Empleado
    '    clearItems()
    '    EmployeeID.Value = 0
    '    panelEmployeeRegistration.Visible = False
    '    panel1.Visible = False
    '    lblMensajeAnexo.Text = ""
    '    RadTabStrip1.Tabs(0).Selected = True
    '    RadMultiPage1.SelectedIndex = 0

    '    Response.Redirect("Empleado.aspx")
    'End Sub
    Private Sub btnAgregarPrestamoPersonal_Click(sender As Object, e As EventArgs) Handles btnAgregarPrestamoPersonal.Click
        PrestamoPersonalID.Value = 0
        chkVigentePrestamo.Checked = False
        txtSaldoInsolutoPrestamo.Enabled = True
        calFechaPrestamo.Focus()
        calFechaPrestamo.Clear()
        txtMontoPrestamo.Text = 0
        txtPagoMinimoPrestamo.Text = 0
        txtSaldoInsolutoPrestamo.Text = 0
        panelPrestamoPersonal.Visible = True
    End Sub
    Private Sub btnCancelarGuardarEmpleados_Click(sender As Object, e As EventArgs) Handles btnCancelarGuardarEmpleados.Click
        Response.Redirect("Empleado.aspx")
    End Sub
    Private Sub txtSueldoDiario_TextChanged(sender As Object, e As EventArgs) Handles txtSueldoDiario.TextChanged
        Dim SDI As Double = 0
        Dim SalarioDiario As Double = 0
        SalarioDiario = txtSueldoDiario.Text
        SDI = SalarioDiario * 1.0452
        txtSueldoDiarioIntegrado.Text = SDI.ToString
    End Sub

End Class