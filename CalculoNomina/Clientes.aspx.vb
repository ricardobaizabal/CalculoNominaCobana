Imports System.Data
Imports System.Data.SqlClient
Imports Telerik.Web.UI
Imports Entities

Public Class clientes
    Inherits System.Web.UI.Page

#Region "Load Initial Values"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ''''''''''''''
        'Window Title'
        ''''''''''''''

        Me.Title = Resources.Resource.WindowsTitle

        If Not IsPostBack Then

            'Dim DataControl As New DataControl
            'txtZipCod.DataSource = ObtenerCodigoPostal()
            'DataControl = Nothing

            '''''''''''''''''''
            'Fieldsets Legends'
            '''''''''''''''''''

            lblClientsListLegend.Text = Resources.Resource.lblClientsListLegend
            lblClientEditLegend.Text = Resources.Resource.lblClientEditLegend

            '''''''''''''''''''''''''''''''''
            'Combobox Values & Empty Message'
            '''''''''''''''''''''''''''''''''

            ''''''''''''''
            'Label Titles'
            ''''''''''''''

            lblContact.Text = Resources.Resource.lblContact
            lblContactEmail.Text = Resources.Resource.lblContactEmail
            lblContactPhone.Text = Resources.Resource.lblContactPhone
            lblStreet.Text = Resources.Resource.lblStreet
            lblExtNumber.Text = Resources.Resource.lblExtNumber
            lblIntNumber.Text = Resources.Resource.lblIntNumber
            lblColony.Text = Resources.Resource.lblColony
            lblCountry.Text = Resources.Resource.lblCountry
            lblState.Text = Resources.Resource.lblState
            lblTownship.Text = Resources.Resource.lblTownship
            lblZipCode.Text = Resources.Resource.lblZipCode
            lblRFC.Text = Resources.Resource.lblRFC
            'lblMetodoPago.Text = Resources.Resource.lblMetodoPago
            lblNumCtaPago.Text = Resources.Resource.lblNumCtaPago

            '''''''''''''''''''
            'Validators Titles'
            '''''''''''''''''''

            'RequiredFieldValidator1.Text = Resources.Resource.validatorMessage
            'RequiredFieldValidator2.Text = Resources.Resource.validatorMessage
            'RequiredFieldValidator3.Text = Resources.Resource.validatorMessage
            'RequiredFieldValidator4.Text = Resources.Resource.validatorMessage
            'valZipCode1.Text = Resources.Resource.validatorMessage
            'RequiredFieldValidator6.Text = Resources.Resource.validatorMessage
            'RequiredFieldValidator7.Text = Resources.Resource.validatorMessage
            'RequiredFieldValidator8.Text = Resources.Resource.validatorMessage
            'RequiredFieldValidator9.Text = Resources.Resource.validatorMessage
            'valTipoContribuyente.Text = Resources.Resource.validatorMessage

            RegularExpressionValidator1.Text = Resources.Resource.validatorMessageEmail
            valRFC.Text = Resources.Resource.validatorMessageRfc

            ''''''''''''''''
            'Buttons Titles'
            ''''''''''''''''

            btnAddClient.Text = Resources.Resource.btnAddClient
            btnSaveClient.Text = Resources.Resource.btnSave
            btnCancel.Text = Resources.Resource.btnCancel

            Dim ObjData As New DataControl(0)
            ObjData.CatalogoRad(condicionesid, "select id, nombre from tblCondiciones", True)
            ObjData.CatalogoRad(tipoContribuyenteid, "select id, nombre from tblTipoContribuyente", True)
            ObjData.CatalogoRad(formapagoid, "select id, id + ' - ' + nombre as nombre from tblFormaPago order by nombre", True)
            ObjData.CatalogoRad(paisid, "EXEC pCatalogoPais @cmd=6", True)
            ObjData.CatalogoRad(estadoid, "select id, nombre from tblEstado order by nombre", True)
            ObjData = Nothing

            Dim ObjData2 As New DataControl(1)
            ObjData2.Catalogo(concepto_base, "select id, nombre from tblConceptoBase", 0)
            ObjData2.Catalogo(calculo_comision, "select id, nombre from tblCalculoComision", 0)
            ObjData2.Catalogo(condicionIMSS, "select id, nombre from tblCondicionIMSSCliente", 0)
            ObjData2.Catalogo(condicionIMSSPatronal, "select id, nombre from tblCondicionPatronalIMSSCliente", 0)
            ObjData2.Catalogo(CondicionISR, "select id, nombre from tblCondicionISRCliente", 0)
            ObjData2.Catalogo(CondicionISN, "select id, nombre from tblCondicionISNCliente", 0)
            ObjData2.Catalogo(CondicionIMSSEmpleado, "select id, nombre from tblCondicionIMSSEmpleado", 0)
            ObjData2.Catalogo(CondicionIMSSPatronalEmpleado, "select id, nombre from tblCondicionPatronalIMSSEmpleado", 0)
            ObjData2.Catalogo(CondicionISREmpleado, "select id, nombre from tblCondicionISREmpleado", 0)
            ObjData2 = Nothing

            DetallesClienteList.DataSource = ObtenerDetallesClientes()
            DetallesClienteList.DataBind()
            ListadoIMSSISR.DataSource = ObtenerIMSSISR()
            ListadoIMSSISR.DataBind()
            ListIMSSISREmpleado.DataSource = ObtenerIMSSISREmpleados()
            ListIMSSISREmpleado.DataBind()
            ListTasaIva.DataSource = ObtenerTasaIVA()
            ListTasaIva.DataBind()

        End If

    End Sub

    Function ObtenerCodigoPostal() As DataSet
        Dim conn As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("conn").ConnectionString)
        Dim cmd As New SqlDataAdapter("EXEC pCatalogos @cmd=6, @estadoid='" & estadoid.SelectedValue & "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return ds

    End Function

#End Region

#Region "Load List Of Clients"

    Function GetClients() As DataSet

        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)
        Dim cmd As New SqlDataAdapter("EXEC pMisClientes  @cmd=1, @txtSearch='" & txtSearch.Text & "'", conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()

            cmd.Fill(ds)

            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return ds

    End Function

#End Region

#Region "Telerik Grid Clients Loading Events"

    Protected Sub clientslist_NeedDataSource(ByVal source As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles clientslist.NeedDataSource

        clientslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
        clientslist.DataSource = GetClients()

    End Sub

#End Region

#Region "Telerik Grid Language Modification(Spanish)"

    Protected Sub clientslist_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles clientslist.Init

        clientslist.PagerStyle.NextPagesToolTip = "Ver mas"
        clientslist.PagerStyle.NextPageToolTip = "Siguiente"
        clientslist.PagerStyle.PrevPagesToolTip = "Ver más"
        clientslist.PagerStyle.PrevPageToolTip = "Atrás"
        clientslist.PagerStyle.LastPageToolTip = "Última Página"
        clientslist.PagerStyle.FirstPageToolTip = "Primera Página"
        clientslist.PagerStyle.PagerTextFormat = "{4}    Página {0} de {1}, Registros {2} al {3} de {5}"
        clientslist.SortingSettings.SortToolTip = "Ordernar"
        clientslist.SortingSettings.SortedAscToolTip = "Ordenar Asc"
        clientslist.SortingSettings.SortedDescToolTip = "Ordenar Desc"


        Dim menu As Telerik.Web.UI.GridFilterMenu = clientslist.FilterMenu
        Dim i As Integer = 0

        While i < menu.Items.Count

            If menu.Items(i).Text = "NoFilter" Or menu.Items(i).Text = "Contains" Then
                i = i + 1
            Else
                menu.Items.RemoveAt(i)
            End If

        End While

        Call ModificaIdiomaGrid()

    End Sub

    Private Sub ModificaIdiomaGrid()

        clientslist.GroupingSettings.CaseSensitive = False

        Dim Menu As Telerik.Web.UI.GridFilterMenu = clientslist.FilterMenu
        Dim item As Telerik.Web.UI.RadMenuItem

        For Each item In Menu.Items

            ''''''''''''''''''''''''''''''''''''''''''''''
            'Change The Text For The StartsWith Menu Item'
            ''''''''''''''''''''''''''''''''''''''''''''''

            If item.Text = "StartsWith" Then
                item.Text = "Empieza con"
            End If

            If item.Text = "NoFilter" Then
                item.Text = "Sin Filtro"
            End If

            If item.Text = "Contains" Then
                item.Text = "Contiene"
            End If

            If item.Text = "EndsWith" Then
                item.Text = "Termina con"
            End If

        Next

    End Sub

#End Region

#Region "Telerik Grid Clients Editing & Deleting Events"

    Protected Sub clientslist_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles clientslist.ItemDataBound

        If TypeOf e.Item Is GridDataItem Then

            Dim dataItem As GridDataItem = CType(e.Item, GridDataItem)

            If e.Item.OwnerTableView.Name = "Clients" Then

                Dim lnkdel As ImageButton = CType(dataItem("Delete").FindControl("btnDelete"), ImageButton)
                lnkdel.Attributes.Add("onclick", "return confirm ('" & Resources.Resource.ClientsDeleteConfirmationMessage & "');")

            End If

        End If

    End Sub

    Protected Sub clientslist_ItemCommand(ByVal source As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles clientslist.ItemCommand

        Select Case e.CommandName

            Case "cmdEdit"
                EditClient(e.CommandArgument)

            Case "cmdDelete"
                DeleteClient(e.CommandArgument)

        End Select

    End Sub

    Private Sub DeleteClient(ByVal id As Integer)

        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)

        Try

            Dim cmd As New SqlCommand("EXEC pMisClientes @cmd='3', @clienteId ='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            panelClientRegistration.Visible = False

            clientslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
            clientslist.DataSource = GetClients()
            clientslist.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub EditClient(ByVal id As Integer)

        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)

        Try

            Dim cmd As New SqlCommand("EXEC pMisClientes @cmd='2', @clienteId='" & id & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then

                txtDenominacionRazonSocial.Text = rs("denominacion_razon_social")
                txtContact.Text = rs("contacto")
                txtContactEmail.Text = rs("email_contacto")
                txtContactPhone.Text = rs("telefono_contacto")
                txtStreet.Text = rs("fac_calle")
                txtExtNumber.Text = rs("fac_num_ext")
                txtIntNumber.Text = rs("fac_num_int")
                txtColony.Text = rs("fac_colonia")
                txtTownship.Text = rs("fac_municipio")
                txtZipCode.Text = rs("fac_cp")
                txtRFC.Text = rs("rfc")
                txtNumCtaPago.Text = rs("numctapago")

                condicionesid.SelectedValue = rs("condicionesid")
                tipoContribuyenteid.SelectedValue = rs("tipoContribuyenteid")
                formapagoid.SelectedValue = rs("formapagoid")
                paisid.SelectedValue = rs("fac_paisid")
                estadoid.SelectedValue = rs("fac_estadoid")

                Call SetCmbRegFiscal(rs("tipoContribuyenteid"), rs("regimenfiscalid"))

                If paisid.SelectedValue <> 1 Then
                    txtStates.Text = rs("fac_estado")
                    estadoid.SelectedValue = 0
                    'ValidarPais()
                Else
                    'txtZipCod.Entries.Add(New AutoCompleteBoxEntry(rs("fac_cp"), rs("fac_cp")))
                    ''txtZipCod.SelectedValue = rs("fac_cp")

                    'txtZipCod.DataSource = ObtenerCodigoPostal()
                    'txtZipCod.DataBind()

                    'ValidarPais()
                End If

                InsertOrUpdate.Value = 1
                ClientsID.Value = id

                panelClientRegistration.Visible = True
                panelListadoClientes.Visible = False

                RadMultiPage1.SelectedIndex = 0
                RadTabStrip1.SelectedIndex = 0
                RadTabStrip1.Tabs(0).Selected = True
                RadTabStrip1.Tabs(0).Enabled = True
                RadTabStrip1.Tabs(1).Enabled = True
                RadTabStrip1.Tabs(2).Enabled = True
                RadTabStrip1.Tabs(3).Enabled = True
                RadTabStrip1.Tabs(4).Enabled = True
                RadTabStrip1.Tabs(5).Enabled = True

                cuentasList.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
                cuentasList.DataSource = ObtenerCuentas()
                cuentasList.DataBind()

            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

#End Region


#Region "Display Client Data Panel"

    Protected Sub btnAddClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddClient.Click

        InsertOrUpdate.Value = 0

        txtContact.Text = ""
        txtContactEmail.Text = ""
        txtContactPhone.Text = ""
        txtStreet.Text = ""
        txtExtNumber.Text = ""
        txtIntNumber.Text = ""
        txtColony.Text = ""
        txtTownship.Text = ""
        txtZipCode.Text = ""
        txtRFC.Text = ""
        txtDenominacionRazonSocial.Text = ""
        regimenid.SelectedValue = 0
        condicionesid.SelectedValue = 0
        tipoContribuyenteid.SelectedValue = 0
        formapagoid.SelectedValue = 0
        estadoid.SelectedValue = 0

        panelClientRegistration.Visible = True
        panelListadoClientes.Visible = False

        RadMultiPage1.SelectedIndex = 0
        RadTabStrip1.SelectedIndex = 0
        RadTabStrip1.Tabs(0).Selected = True
        RadTabStrip1.Tabs(0).Enabled = True
        RadTabStrip1.Tabs(1).Enabled = False
        RadTabStrip1.Tabs(2).Enabled = False
        RadTabStrip1.Tabs(3).Enabled = False
        RadTabStrip1.Tabs(4).Enabled = False
        RadTabStrip1.Tabs(5).Enabled = False

    End Sub

#End Region

#Region "Save Client"

    Protected Sub btnSaveClient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveClient.Click

        Dim fac_cp As String = ""

        'If paisid.SelectedValue <> 1 Then
        '    fac_cp = txtZipCode.Text.Trim.ToString
        'Else
        '    fac_cp = Replace(txtZipCod.Text.Trim.ToString, ";", "")
        'End If

        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)

        Try

            If InsertOrUpdate.Value = 0 Then

                InsertOrUpdate.Value = 1

                Dim cmd As New SqlCommand("EXEC pMisClientes @cmd=4, @clienteUnionId='" & Session("clienteid").ToString & "', @razonsocial='" & txtDenominacionRazonSocial.Text & "', @contacto='" & txtContact.Text & "', @email_contacto='" & txtContactEmail.Text & "', @telefono_contacto='" & txtContactPhone.Text & "', @fac_calle='" & txtStreet.Text & "', @fac_num_int='" & txtIntNumber.Text & "', @fac_num_ext='" & txtExtNumber.Text & "', @fac_colonia='" & txtColony.Text & "', @fac_municipio='" & txtTownship.Text & "', @fac_paisid='" & paisid.SelectedValue.ToString & "', @fac_estadoid='" & estadoid.SelectedValue.ToString & "', @fac_estado='" & txtStates.Text & "', @fac_cp='" & fac_cp.ToString & "', @fac_rfc='" & txtRFC.Text & "', @condicionesid='" & condicionesid.SelectedValue.ToString & "', @tipocontribuyenteid='" & tipoContribuyenteid.SelectedValue.ToString & "', @formapagoid='" & formapagoid.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "', @regimenfiscalid='" & regimenid.SelectedValue.ToString & "', @denominacion_razon_social='" & txtDenominacionRazonSocial.Text & "'", conn)

                conn.Open()

                ClientsID.Value = cmd.ExecuteScalar()

                'panelClientRegistration.Visible = False

                clientslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                clientslist.DataSource = GetClients()
                clientslist.DataBind()

                conn.Close()
                conn.Dispose()

                RadTabStrip1.Tabs(0).Enabled = True
                RadTabStrip1.Tabs(1).Enabled = True
                RadTabStrip1.Tabs(2).Enabled = True
                RadTabStrip1.Tabs(3).Enabled = True
                RadTabStrip1.Tabs(4).Enabled = True
                RadTabStrip1.Tabs(5).Enabled = True

                rwAlerta.RadAlert("Datos de cliente agregados exitosamente.", 330, 180, "Alerta", "", "")

            Else

                Dim cmd As New SqlCommand("EXEC pMisClientes @cmd=5, @clienteid='" & ClientsID.Value & "', @razonsocial='" & txtDenominacionRazonSocial.Text & "', @contacto='" & txtContact.Text & "', @email_contacto='" & txtContactEmail.Text & "', @telefono_contacto='" & txtContactPhone.Text & "', @fac_calle='" & txtStreet.Text & "', @fac_num_int='" & txtIntNumber.Text & "', @fac_num_ext='" & txtExtNumber.Text & "', @fac_colonia='" & txtColony.Text & "', @fac_municipio='" & txtTownship.Text & "', @fac_paisid='" & paisid.SelectedValue.ToString & "', @fac_estadoid='" & estadoid.SelectedValue.ToString & "', @fac_estado='" & txtStates.Text & "', @fac_cp='" & fac_cp & "', @fac_rfc='" & txtRFC.Text & "', @condicionesid='" & condicionesid.SelectedValue.ToString & "', @tipocontribuyenteid='" & tipoContribuyenteid.SelectedValue.ToString & "', @formapagoid='" & formapagoid.SelectedValue.ToString & "', @numctapago='" & txtNumCtaPago.Text & "', @regimenfiscalid='" & regimenid.SelectedValue.ToString & "', @denominacion_razon_social='" & txtDenominacionRazonSocial.Text & "'", conn)

                conn.Open()

                cmd.ExecuteReader()

                'panelClientRegistration.Visible = False

                clientslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
                clientslist.DataSource = GetClients()
                clientslist.DataBind()

                conn.Close()
                conn.Dispose()

                rwAlerta.RadAlert("Datos de cliente actualizados exitosamente.", 330, 180, "Alerta", "", "")

            End If

            panelClientRegistration.Visible = True
            panelListadoClientes.Visible = False

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

#End Region

#Region "Cancel Client (Save/Edit)"

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        InsertOrUpdate.Value = 0

        txtDenominacionRazonSocial.Text = ""
        txtContact.Text = ""
        txtContactEmail.Text = ""
        txtContactPhone.Text = ""
        txtStreet.Text = ""
        txtExtNumber.Text = ""
        txtIntNumber.Text = ""
        txtColony.Text = ""
        txtTownship.Text = ""
        'txtZipCode.Text = ""
        txtRFC.Text = ""

        condicionesid.SelectedValue = 0
        tipoContribuyenteid.SelectedValue = 0
        formapagoid.SelectedValue = 0
        estadoid.SelectedValue = 0

        panelClientRegistration.Visible = False
        panelListadoClientes.Visible = True

    End Sub

#End Region

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        panelClientRegistration.Visible = False
        panelListadoClientes.Visible = True

        clientslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
        clientslist.DataSource = GetClients()
        clientslist.DataBind()
    End Sub

    Protected Sub btnAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAll.Click

        panelClientRegistration.Visible = False
        panelListadoClientes.Visible = True

        txtSearch.Text = ""
        clientslist.MasterTableView.NoMasterRecordsText = Resources.Resource.ClientsEmptyGridMessage
        clientslist.DataSource = GetClients()
        clientslist.DataBind()
    End Sub

    Protected Sub paisid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles paisid.SelectedIndexChanged
        'ValidarPais()
    End Sub

    'Public Sub ValidarPais()
    '    If paisid.SelectedValue <> 1 Then
    '        estadoid.SelectedValue = 0
    '        estadoid.Visible = False
    '        RequiredFieldValidator6.Enabled = False
    '        txtStates.Visible = True
    '        txtStates.Enabled = True
    '        RequiredFieldValidator10.Enabled = True

    '        txtZipCode.Visible = True
    '        valZipCode1.Enabled = True

    '        valZipCode2.Enabled = False
    '        txtZipCod.Visible = False
    '    Else
    '        'Mexico
    '        paisid.Visible = True
    '        estadoid.Visible = True
    '        RequiredFieldValidator6.Enabled = True

    '        txtStates.Visible = False
    '        txtStates.Enabled = False
    '        RequiredFieldValidator10.Enabled = False

    '        txtZipCode.Visible = False
    '        valZipCode1.Enabled = False

    '        valZipCode2.Enabled = True
    '        txtZipCod.Visible = True
    '    End If
    'End Sub

    'Private Sub txtZipCod_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtZipCod.Load
    '    Dim ObjData As New DataControl
    '    txtZipCod.DataSource = ObtenerCodigoPostal()
    '    txtZipCod.DataBind()
    '    ObjData = Nothing
    'End Sub

    'Private Sub estadoid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles estadoid.SelectedIndexChanged
    '    Dim ObjData As New DataControl
    '    txtZipCod.DataSource = ObtenerCodigoPostal()
    '    txtZipCod.DataBind()
    '    ObjData = Nothing
    'End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        If Page.IsValid Then
            Dim objData As New DataControl(1)
            Dim sql As String = ""
            Dim IdCuentas As Integer = 0

            If CuentaID.Value = 0 Then
                IdCuentas = objData.RunSQLScalarQuery("EXEC pCatalogoCuentas @cmd=1, @banconacional='" & txtBanco.Text & "', @bancoextranjero='" & txtBancoExtr.Text & "',@rfc='" & txtRFCBAK.Text & "', @numctapago='" & txtCuenta.Text & "', @predeterminadoBit='" & chkPredeterminado.Checked & "', @clienteid='" & ClientsID.Value & "'")
                clearItems()
            Else
                objData.RunSQLScalarQuery("EXEC pCatalogoCuentas @cmd=4, @banconacional='" & txtBanco.Text & "', @bancoextranjero='" & txtBancoExtr.Text & "', @rfc='" & txtRFCBAK.Text & "', @numctapago='" & txtCuenta.Text & "',@predeterminadoBit='" & chkPredeterminado.Checked & "', @clienteid='" & ClientsID.Value & "', @id='" & CuentaID.Value & "'")
                clearItems()
                objData = Nothing
            End If

            cuentasList.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
            cuentasList.DataSource = ObtenerCuentas()
            cuentasList.DataBind()
        End If
    End Sub

    Function ObtenerCuentas() As DataSet

        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)
        Dim qry As String = "EXEC pCatalogoCuentas @cmd=5, @clienteid='" & ClientsID.Value & "'"

        Dim cmd As New SqlDataAdapter(qry, conn)

        Dim ds As DataSet = New DataSet

        Try

            conn.Open()
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

        Return ds

    End Function

    Private Sub DeleteCuenta(ByVal id As Integer)

        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoCuentas @cmd=3, @id='" & id.ToString & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader

            rs = cmd.ExecuteReader()
            rs.Close()

            conn.Close()

            cuentasList.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
            cuentasList.DataSource = ObtenerCuentas()
            cuentasList.DataBind()

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub CargarCuenta()

        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)

        Try

            Dim cmd As New SqlCommand("EXEC pCatalogoCuentas @cmd=2, @id='" & CuentaID.Value & "'", conn)

            conn.Open()

            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()

            If rs.Read Then
                txtBanco.Text = rs("banconacional")
                txtBancoExtr.Text = rs("bancoextranjero")
                txtCuenta.Text = rs("numctapago")
                txtRFCBAK.Text = rs("rfc")
                chkPredeterminado.Checked = rs("predeterminadoBit")
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        clearItems()
    End Sub

    Private Sub cuentasList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles cuentasList.ItemCommand
        Select Case e.CommandName
            Case "cmdEdit"
                CuentaID.Value = e.CommandArgument
                Call CargarCuenta()
                btnCancelar.Visible = True
            Case "cmdDelete"
                Call DeleteCuenta(e.CommandArgument)
        End Select
    End Sub

    Private Sub cuentasList_ItemDataBound(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridItemEventArgs) Handles cuentasList.ItemDataBound
        Select Case e.Item.ItemType
            Case Telerik.Web.UI.GridItemType.Item, Telerik.Web.UI.GridItemType.AlternatingItem
                Dim imgPredeterminado As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imgPredeterminado"), System.Web.UI.WebControls.Image)
                imgPredeterminado.Visible = e.Item.DataItem("predeterminadoBit")
        End Select
    End Sub

    Sub clearItems()
        txtBanco.Text = ""
        txtBancoExtr.Text = ""
        txtRFCBAK.Text = ""
        txtCuenta.Text = ""
        chkPredeterminado.Checked = False
        txtBanco.Focus()
        CuentaID.Value = 0
    End Sub

    Private Sub cuentasList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles cuentasList.NeedDataSource
        cuentasList.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
        cuentasList.DataSource = ObtenerCuentas()
    End Sub

    Private Sub SetCmbRegFiscal(Optional ByVal contribuyenteid As Integer = 0, Optional ByVal sel As Integer = 0)
        Dim ObjData As New DataControl(0)
        Dim sqlwhere As String
        Select Case tipoContribuyenteid.SelectedValue
            Case 1
                sqlwhere = "where fisica='Sí' "
            Case 2
                sqlwhere = "where moral='Sí' "
            Case Else
                sqlwhere = ""
        End Select
        ObjData.CatalogoRad(regimenid, "select id, id + ' - ' + nombre as descripcion " & "from tblRegimenFiscal " & sqlwhere & " order by nombre ", True)
        ObjData = Nothing

        regimenid.SelectedValue = sel

    End Sub

    Protected Sub tipoContribuyenteid_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tipoContribuyenteid.SelectedIndexChanged
        SetCmbRegFiscal(tipoContribuyenteid.SelectedValue)
    End Sub

    Function ObtenerDetallesClientes() As DataSet

        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)
        Dim qry As String = "SELECT A.id, A.concepto_base, A.calculo_comision, A.comision_cliente, A.comision_empleado, CB.nombre AS concepto_base_descripcion, CAL.nombre AS calculo_comision_descripcion FROM tblDetallesCliente AS A LEFT JOIN tblConceptoBase CB ON A.concepto_base = CB.id LEFT JOIN tblCalculoComision CAL ON A.calculo_comision = CAL.id WHERE A.borradoBit = 0 AND A.cliente_detalle='" & ClientsID.Value & "'"

        Dim cmd As New SqlDataAdapter(qry, conn)
        Dim ds As DataSet = New DataSet
        Try
            conn.Open()
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        Return ds
    End Function

    Function ObtenerIMSSISR() As DataSet

        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)
        Dim qry As String = "SELECT A.id, A.condicion_imss_id, A.cobro_imss, A.condicion_patronal_imss_id, A.cobro_patronal_imss, A.condicion_isr_id, A.cobro_isr, A.condicion_isn_id, CIM.nombre AS condicion_imss_descripcion, CPIM.nombre AS condicion_patronal_imss_descripcion, CI.nombre AS condicion_isr_descripcion, CISN.nombre AS condicion_isn_descripcion FROM tblClienteDetallesIMSS AS A LEFT JOIN tblCondicionIMSSCliente CIM ON A.condicion_imss_id = CIM.id  LEFT JOIN tblCondicionPatronalIMSSCliente CPIM ON A.condicion_patronal_imss_id = CPIM.id LEFT JOIN tblCondicionISRCliente CI ON A.condicion_isr_id = CI.id LEFT JOIN tblCondicionISNCliente CISN ON A.condicion_isn_id = CISN.id WHERE A.borradoBit = 0 AND A.cliente_id='" & ClientsID.Value & "'"

        Dim cmd As New SqlDataAdapter(qry, conn)
        Dim ds As DataSet = New DataSet
        Try
            conn.Open()
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        Return ds
    End Function

    Function ObtenerIMSSISREmpleados() As DataSet

        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)
        Dim qry As String = "SELECT A.id, A.condicion_imss_id, A.cobro_imss, A.condicion_patronal_imss_id, A.cobro_patronal_imss, A.condicion_isr_id, A.cobro_isr, CIM.nombre AS condicion_imss_descripcion, CPIM.nombre AS condicion_patronal_imss_descripcion, CI.nombre AS condicion_isr_descripcion FROM tblEmpleadoDetallesIMSS AS A LEFT JOIN tblCondicionIMSSEmpleado CIM ON A.condicion_imss_id = CIM.id  LEFT JOIN tblCondicionPatronalIMSSEmpleado CPIM ON A.condicion_patronal_imss_id = CPIM.id LEFT JOIN tblCondicionISREmpleado CI ON A.condicion_isr_id = CI.id WHERE A.borradoBit = 0 AND A.cliente_id='" & ClientsID.Value & "'"

        Dim cmd As New SqlDataAdapter(qry, conn)
        Dim ds As DataSet = New DataSet
        Try
            conn.Open()
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        Return ds
    End Function

    Function ObtenerTasaIVA() As DataSet

        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)
        Dim qry As String = "SELECT id, TIRemuneracion, TIComision, TICuotaObrera, TICuotaPatronal, TIISN, TIInfonavit, TIISR FROM tblTasaIVA WHERE borradoBit = 0 AND cliente_id='" & ClientsID.Value & "'"

        Dim cmd As New SqlDataAdapter(qry, conn)
        Dim ds As DataSet = New DataSet
        Try
            conn.Open()
            cmd.Fill(ds)
            conn.Close()
            conn.Dispose()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
        Return ds
    End Function

    Protected Sub btnGuardarDetalles_Click(sender As Object, e As EventArgs) Handles btnGuardarDetalles.Click
        If Page.IsValid Then
            Dim objData As New DataControl(1)
            Dim sql As String = ""
            Dim IdCuentas As Integer = 0

            If Session("DetalleClienteID") = Nothing Then
                IdCuentas = objData.RunSQLScalarQuery("INSERT INTO tblDetallesCliente (concepto_base, calculo_comision, comision_cliente, comision_empleado, cliente_detalle) VALUES('" & concepto_base.SelectedValue & "', '" & calculo_comision.SelectedValue & "', '" & comision_cliente.Text & "', '" & comision_empleado.Text & "', '" & ClientsID.Value & "')")
                clearItems()
                objData = Nothing
            Else
                objData.RunSQLScalarQuery("UPDATE tblDetallesCliente SET concepto_base='" & concepto_base.SelectedValue & "', calculo_comision='" & calculo_comision.SelectedValue & "', comision_cliente='" & comision_cliente.Text & "', comision_empleado='" & comision_empleado.Text & "' WHERE  id='" & Session("DetalleClienteID") & "'")
                Session("DetalleClienteID") = Nothing
                clearItems()
                objData = Nothing
            End If

            concepto_base.SelectedValue = 0
            calculo_comision.SelectedValue = 0
            comision_cliente.Text = ""
            comision_empleado.Text = ""

            DetallesClienteList.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
            DetallesClienteList.DataSource = ObtenerDetallesClientes()
            DetallesClienteList.DataBind()
        End If
    End Sub

    Protected Sub btnGuardarIMSSISR_Click(sender As Object, e As EventArgs) Handles btnGuardarIMSSISR.Click
        If Page.IsValid Then
            Dim objData As New DataControl(1)
            Dim sql As String = ""
            Dim IdCuentas As Integer = 0

            If Session("IMSS_ISR_ClienteID") = Nothing Then
                IdCuentas = objData.RunSQLScalarQuery("INSERT INTO tblClienteDetallesIMSS (condicion_imss_id, cobro_imss, condicion_patronal_imss_id, cobro_patronal_imss, condicion_isr_id, cobro_isr, condicion_isn_id, cliente_id) VALUES('" & condicionIMSS.SelectedValue & "', '" & CobroIMSS.Text & "', '" & condicionIMSSPatronal.SelectedValue & "', '" & CobroIMSSPatronal.Text & "', '" & CondicionISR.SelectedValue & "', '" & CobroISR.Text & "', '" & CondicionISN.SelectedValue & "', '" & ClientsID.Value & "')")
                clearItems()
                objData = Nothing
            Else
                objData.RunSQLScalarQuery("UPDATE tblClienteDetallesIMSS SET condicion_imss_id='" & condicionIMSS.SelectedValue & "', cobro_imss='" & CobroIMSS.Text & "', condicion_patronal_imss_id='" & condicionIMSSPatronal.SelectedValue & "', cobro_patronal_imss='" & CobroIMSSPatronal.Text & "', condicion_isr_id='" & CondicionISR.SelectedValue & "', cobro_isr='" & CobroISR.Text & "', condicion_isn_id='" & CondicionISN.SelectedValue & "' WHERE  id='" & Session("IMSS_ISR_ClienteID") & "'")
                Session("IMSS_ISR_ClienteID") = Nothing
                clearItems()
                objData = Nothing
            End If

            condicionIMSS.SelectedValue = 0
            condicionIMSSPatronal.SelectedValue = 0
            CondicionISR.SelectedValue = 0
            CondicionISN.SelectedValue = 0
            CobroIMSS.Text = ""
            CobroIMSSPatronal.Text = ""
            CobroISR.Text = ""

            ListadoIMSSISR.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
            ListadoIMSSISR.DataSource = ObtenerIMSSISR()
            ListadoIMSSISR.DataBind()
        End If
    End Sub

    Protected Sub btnGuardarIMSSISREmpleado_Click(sender As Object, e As EventArgs) Handles btnGuardarIMSSISREmpleado.Click
        If Page.IsValid Then
            Dim objData As New DataControl(1)
            Dim sql As String = ""
            Dim IdCuentas As Integer = 0

            If Session("IMSS_ISR_EmpleadoID") = Nothing Then
                IdCuentas = objData.RunSQLScalarQuery("INSERT INTO tblEmpleadoDetallesIMSS (condicion_imss_id, cobro_imss, condicion_patronal_imss_id, cobro_patronal_imss, condicion_isr_id, cobro_isr, cliente_id) VALUES('" & CondicionIMSSEmpleado.SelectedValue & "', '" & CobroIMSSEmpleado.Text & "', '" & CondicionIMSSPatronalEmpleado.SelectedValue & "', '" & CobroIMSSPatronalEmpleado.Text & "', '" & CondicionISREmpleado.SelectedValue & "', '" & CobroISREmpleado.Text & "', '" & ClientsID.Value & "')")
                clearItems()
                objData = Nothing
            Else
                objData.RunSQLScalarQuery("UPDATE tblEmpleadoDetallesIMSS SET condicion_imss_id='" & CondicionIMSSEmpleado.SelectedValue & "', cobro_imss='" & CobroIMSSEmpleado.Text & "', condicion_patronal_imss_id='" & CondicionIMSSPatronalEmpleado.SelectedValue & "', cobro_patronal_imss='" & CobroIMSSPatronalEmpleado.Text & "', condicion_isr_id='" & CondicionISREmpleado.SelectedValue & "', cobro_isr='" & CobroISREmpleado.Text & "' WHERE  id='" & Session("IMSS_ISR_EmpleadoID") & "'")
                Session("IMSS_ISR_EmpleadoID") = Nothing
                clearItems()
                objData = Nothing
            End If

            CondicionIMSSEmpleado.SelectedValue = 0
            CondicionIMSSPatronalEmpleado.SelectedValue = 0
            CondicionISREmpleado.SelectedValue = 0
            CobroIMSSEmpleado.Text = ""
            CobroIMSSPatronalEmpleado.Text = ""
            CobroISREmpleado.Text = ""

            ListIMSSISREmpleado.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
            ListIMSSISREmpleado.DataSource = ObtenerIMSSISREmpleados()
            ListIMSSISREmpleado.DataBind()
        End If
    End Sub

    Protected Sub btnGuardarTasaIVA_Click(sender As Object, e As EventArgs) Handles btnGuardarTasaIVA.Click
        If Page.IsValid Then
            Dim objData As New DataControl(1)
            Dim sql As String = ""
            Dim IdCuentas As Integer = 0

            If Session("Tasa_IVA_ID") = Nothing Then
                IdCuentas = objData.RunSQLScalarQuery("INSERT INTO tblTasaIVA (TIRemuneracion, TIComision, TICuotaObrera, TICuotaPatronal, TIISN, TIInfonavit, TIISR, cliente_id) VALUES('" & TIRemuneracion.Text & "', '" & TIComision.Text & "', '" & TICuotaObrera.Text & "', '" & TICuotaPatronal.Text & "', '" & TIISN.Text & "', '" & TIInfonavit.Text & "', '" & TIISR.Text & "', '" & ClientsID.Value & "')")
                clearItems()
                objData = Nothing
            Else
                objData.RunSQLScalarQuery("UPDATE tblTasaIVA SET TIRemuneracion='" & TIRemuneracion.Text & "', TIComision='" & TIComision.Text & "', TICuotaObrera='" & TICuotaObrera.Text & "', TICuotaPatronal='" & TICuotaPatronal.Text & "', TIISN='" & TIISN.Text & "', TIInfonavit='" & TIInfonavit.Text & "', TIISR='" & TIISR.Text & "' WHERE  id='" & Session("Tasa_IVA_ID") & "'")
                Session("Tasa_IVA_ID") = Nothing
                clearItems()
                objData = Nothing
            End If

            TIRemuneracion.Text = ""
            TIComision.Text = ""
            TICuotaObrera.Text = ""
            TICuotaPatronal.Text = ""
            TIISN.Text = ""
            TIInfonavit.Text = ""
            TIISR.Text = ""

            ListTasaIva.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
            ListTasaIva.DataSource = ObtenerTasaIVA()
            ListTasaIva.DataBind()
        End If
    End Sub

    Private Sub DetallesClienteList_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles DetallesClienteList.NeedDataSource
        DetallesClienteList.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
        DetallesClienteList.DataSource = ObtenerDetallesClientes()
    End Sub

    Private Sub ListadoIMSSISR_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles ListadoIMSSISR.NeedDataSource
        ListadoIMSSISR.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
        ListadoIMSSISR.DataSource = ObtenerIMSSISR()
    End Sub

    Private Sub ListIMSSISREmpleado_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles ListIMSSISREmpleado.NeedDataSource
        ListIMSSISREmpleado.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
        ListIMSSISREmpleado.DataSource = ObtenerIMSSISREmpleados()
    End Sub

    Private Sub ListTasaIva_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles ListTasaIva.NeedDataSource
        ListTasaIva.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
        ListTasaIva.DataSource = ObtenerTasaIVA()
    End Sub

    Private Sub DetallesClienteList_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles DetallesClienteList.ItemCommand
        Select Case e.CommandName
            Case "cmdEditDetalle"
                Session("DetalleClienteID") = e.CommandArgument
                CargarDetalleCliente()
                btnCancelar.Visible = True
            Case "cmdDeleteDetalle"
                DeleteDetalleCliente(e.CommandArgument)
        End Select
    End Sub

    Private Sub ListadoIMSSISR_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles ListadoIMSSISR.ItemCommand
        Select Case e.CommandName
            Case "cmdEditDetalle"
                Session("IMSS_ISR_ClienteID") = e.CommandArgument
                CargarDetalleIMSSISR()
                btnCancelar.Visible = True
            Case "cmdDeleteDetalle"
                DeleteDetalleIMSSIRS(e.CommandArgument)
        End Select
    End Sub

    Private Sub ListIMSSISREmpleado_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles ListIMSSISREmpleado.ItemCommand
        Select Case e.CommandName
            Case "cmdEditDetalle"
                Session("IMSS_ISR_EmpleadoID") = e.CommandArgument
                CargarDetalleIMSSISREmpleado()
                btnCancelar.Visible = True
            Case "cmdDeleteDetalle"
                DeleteDetalleIMSSIRSEmpleado(e.CommandArgument)
        End Select
    End Sub

    Private Sub ListTasaIva_ItemCommand(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridCommandEventArgs) Handles ListTasaIva.ItemCommand
        Select Case e.CommandName
            Case "cmdEditDetalle"
                Session("Tasa_IVA_ID") = e.CommandArgument
                CargarDetalleTasaIVA()
                btnCancelar.Visible = True
            Case "cmdDeleteDetalle"
                DeleteDetalleTasaIVA(e.CommandArgument)
        End Select
    End Sub

    Private Sub DeleteDetalleCliente(ByVal id As Integer)
        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)
        Try
            Dim cmd As New SqlCommand("UPDATE tblDetallesCliente SET borradoBit=1 WHERE id='" & id.ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()
            rs.Close()
            conn.Close()
            DetallesClienteList.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
            DetallesClienteList.DataSource = ObtenerDetallesClientes()
            DetallesClienteList.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub DeleteDetalleIMSSIRS(ByVal id As Integer)
        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)
        Try
            Dim cmd As New SqlCommand("UPDATE tblClienteDetallesIMSS SET borradoBit=1 WHERE id='" & id.ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()
            rs.Close()
            conn.Close()
            ListadoIMSSISR.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
            ListadoIMSSISR.DataSource = ObtenerIMSSISR()
            ListadoIMSSISR.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub DeleteDetalleIMSSIRSEmpleado(ByVal id As Integer)
        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)
        Try
            Dim cmd As New SqlCommand("UPDATE tblEmpleadoDetallesIMSS SET borradoBit=1 WHERE id='" & id.ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()
            rs.Close()
            conn.Close()
            ListIMSSISREmpleado.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
            ListIMSSISREmpleado.DataSource = ObtenerIMSSISREmpleados()
            ListIMSSISREmpleado.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub DeleteDetalleTasaIVA(ByVal id As Integer)
        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)
        Try
            Dim cmd As New SqlCommand("UPDATE tblTasaIVA SET borradoBit=1 WHERE id='" & id.ToString & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()
            rs.Close()
            conn.Close()
            ListTasaIva.MasterTableView.NoMasterRecordsText = "No se encontraron datos para mostrar"
            ListTasaIva.DataSource = ObtenerTasaIVA()
            ListTasaIva.DataBind()
        Catch ex As Exception
            Response.Write(ex.Message.ToString)
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub CargarDetalleCliente()
        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)
        Try
            Dim cmd As New SqlCommand("SELECT id, concepto_base, calculo_comision, comision_cliente, comision_empleado FROM tblDetallesCliente WHERE id='" & Session("DetalleClienteID") & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()
            If rs.Read Then
                concepto_base.SelectedValue = rs("concepto_base")
                calculo_comision.SelectedValue = rs("calculo_comision")
                comision_cliente.Text = rs("comision_cliente")
                comision_empleado.Text = rs("comision_empleado")
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub CargarDetalleIMSSISR()
        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)
        Try
            Dim cmd As New SqlCommand("SELECT id, condicion_imss_id, cobro_imss, condicion_patronal_imss_id, cobro_patronal_imss, condicion_isr_id, cobro_isr, condicion_isn_id FROM tblClienteDetallesIMSS WHERE id='" & Session("IMSS_ISR_ClienteID") & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()
            If rs.Read Then
                condicionIMSS.SelectedValue = rs("condicion_imss_id")
                condicionIMSSPatronal.SelectedValue = rs("condicion_patronal_imss_id")
                CondicionISR.SelectedValue = rs("condicion_isr_id")
                CondicionISN.SelectedValue = rs("condicion_isn_id")

                CobroIMSS.Text = rs("cobro_imss")
                CobroIMSSPatronal.Text = rs("cobro_patronal_imss")
                CobroISR.Text = rs("cobro_isr")
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub CargarDetalleIMSSISREmpleado()
        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)
        Try
            Dim cmd As New SqlCommand("SELECT id, condicion_imss_id, cobro_imss, condicion_patronal_imss_id, cobro_patronal_imss, condicion_isr_id, cobro_isr FROM tblEmpleadoDetallesIMSS WHERE id='" & Session("IMSS_ISR_EmpleadoID") & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()
            If rs.Read Then
                CondicionIMSSEmpleado.SelectedValue = rs("condicion_imss_id")
                CondicionIMSSPatronalEmpleado.SelectedValue = rs("condicion_patronal_imss_id")
                CondicionISREmpleado.SelectedValue = rs("condicion_isr_id")

                CobroIMSSEmpleado.Text = rs("cobro_imss")
                CobroIMSSPatronalEmpleado.Text = rs("cobro_patronal_imss")
                CobroISREmpleado.Text = rs("cobro_isr")
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub CargarDetalleTasaIVA()
        Dim conn As New SqlConnection(HttpContext.Current.Session("conexion").ToString)
        Try
            Dim cmd As New SqlCommand("SELECT id, TIRemuneracion, TIComision, TICuotaObrera, TICuotaPatronal, TIISN, TIInfonavit, TIISR FROM tblTasaIVA WHERE id='" & Session("Tasa_IVA_ID") & "'", conn)
            conn.Open()
            Dim rs As SqlDataReader
            rs = cmd.ExecuteReader()
            If rs.Read Then
                TIRemuneracion.Text = rs("TIRemuneracion")
                TIComision.Text = rs("TIComision")
                TICuotaObrera.Text = rs("TICuotaObrera")
                TICuotaPatronal.Text = rs("TICuotaPatronal")
                TIISN.Text = rs("TIISN")
                TIInfonavit.Text = rs("TIInfonavit")
                TIISR.Text = rs("TIISR")
            End If

        Catch ex As Exception
            Response.Write(ex.Message.ToString)
            Response.End()
        Finally
            conn.Close()
            conn.Dispose()
        End Try
    End Sub

    Private Sub CancelarEdicion()
        Response.Redirect("~/Clientes.aspx")
    End Sub

    Protected Sub btnCancelarDetalle_Click(sender As Object, e As EventArgs) Handles btnCancelarDetalle.Click
        CancelarEdicion()
    End Sub

    Protected Sub btnCancelarIMSSISR_Click(sender As Object, e As EventArgs) Handles btnCancelarIMSSISR.Click
        CancelarEdicion()
    End Sub

    Protected Sub btnCancelarIMSSISREmpleado_Click(sender As Object, e As EventArgs) Handles btnCancelarIMSSISREmpleado.Click
        CancelarEdicion()
    End Sub

    Protected Sub btnCancelarTasaIVA_Click(sender As Object, e As EventArgs) Handles btnCancelarTasaIVA.Click
        CancelarEdicion()
    End Sub

End Class
