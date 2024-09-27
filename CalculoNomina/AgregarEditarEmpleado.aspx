<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="AgregarEditarEmpleado.aspx.vb" Inherits="CalculoNomina.AgregarEditarEmpleado" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<meta http-equiv="Refresh" content="5;url=Empleado.aspx" />--%>
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("employeeslist") > -1) || (arguments.get_eventTarget().indexOf("btnGuardarAnexo") > -1) || (arguments.get_eventTarget().indexOf("contratosList") > -1) || (arguments.get_eventTarget().indexOf("imgSend") > -1) || (arguments.get_eventTarget().indexOf("lnkVerContratos") > -1) || (arguments.get_eventTarget().indexOf("anexoFiniquito") > -1) || (arguments.get_eventTarget().indexOf("lnkDownload") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
        function openRadWindow(id) {
            var oWnd = radopen("envio_contrato.aspx?contratoid=" + id, "SendWindow");
            oWnd.set_modal(true);
            oWnd.set_centerIfModal(true);
            oWnd.set_visibleStatusbar(false);
            oWnd.setSize(1024, 768);
            oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
            oWnd.set_autoSize(false);
            oWnd.center();
        }
        function openRadWindowMovimientosIMSS(id) {
            var oWnd = radopen("MovIMSS.aspx?id=" + id, "MovIMSSWindow");
            oWnd.set_modal(true);
            oWnd.set_centerIfModal(true);
            oWnd.set_visibleStatusbar(false);
            oWnd.setSize(1024, 768);
            oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
            oWnd.set_autoSize(false);
            oWnd.center();
        }
        function openRadWindowOpenIMSS(id) {
            var oWnd = radopen("MovIMSS.aspx?id=" + id, "MovIMSSWindow");
            oWnd.set_modal(true);
            oWnd.set_centerIfModal(true);
            oWnd.set_visibleStatusbar(false);
            oWnd.setSize(1024, 768);
            oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
            oWnd.set_autoSize(false);
            oWnd.center();
        }
        function OnClientClose(sender, args) {
            document.location.href = document.location.href;
        }
        function KeyPress(sender, args) {
            if (args.KeyCode == 45 || args.KeyCode == 46) {
                return false;
            }
        }
        function clearFilters(sender, args) {
            if ($("#ctl00_ContentPlaceHolder1_ddCliente_Input").val() != "") {
                var obj = jQuery.parseJSON($("#ctl00_ContentPlaceHolder1_ddCliente_Input").val());
                if (obj.text === "") {
                    sender.set_cancel(true);
                }
            } else {
                eventArgs.set_cancel(true);
            }
        }
    </script>
    <style>
        .ulwrapper {
            background: #569705;
            margin: auto;
            padding: 0;
            width: 108px;
            height: 20px;
            position: relative;
        }

        .ulwrapperblue {
            background: #40ADD4;
            margin: auto;
            padding: 0;
            width: 108px;
            height: 20px;
            position: relative;
        }

        .TitleBaja {
            display: block;
            text-align: center;
            line-height: 150%;
            font-size: 9pt;
            font-family: Verdana;
            text-decoration: none;
            color: white;
        }

        .auto-style1 {
            width: 50%;
            height: 26px;
        }

        .auto-style2 {
            width: 25%;
            height: 26px;
        }

        td {
            height: 37px;
            vertical-align: middle !important;
        }

        #ContentPlaceHolder1_rblSexo_1 {
            margin-left: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">--%>
    <telerik:RadWindowManager ID="RadWindowManager2" runat="server">
    </telerik:RadWindowManager>
    <asp:Panel ID="panelEmployeeRegistration" runat="server" Visible="False">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Label ID="lblEmployeeEditLegend" runat="server" Text="Agregar/Editar Empleado" Font-Bold="True" CssClass="item"></asp:Label>
            </legend>
            <asp:Panel ID="panel1" runat="server" Visible="false" BorderWidth="0px" BorderColor="#000000">
                <fieldset>
                    <table border="0" style="width: 100%" cellpadding="5" cellspacing="5">
                        <tr>
                            <td style="width: 30%;">
                                <asp:Label ID="lblNombreTitle" runat="server" CssClass="item" Font-Bold="True" Text="Nombre:"></asp:Label>
                            </td>
                            <td style="width: 20%;">
                                <asp:Label ID="lblRFCTitle" runat="server" CssClass="item" Font-Bold="True" Text="RFC:"></asp:Label>
                            </td>
                            <td style="width: 20%;">
                                <asp:Label ID="lblNoSegSocialTitle" runat="server" CssClass="item" Font-Bold="True" Text="No. Seguro Social:"></asp:Label>
                            </td>
                            <td style="width: 20%;">
                                <asp:Label ID="lblClienteTitle" runat="server" CssClass="item" Font-Bold="True" Text="Cliente:"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNombreValue" runat="server" CssClass="item" Font-Bold="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblRFCValue" runat="server" CssClass="item" Font-Bold="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNoSegSocialValue" runat="server" CssClass="item" Font-Bold="false"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblClienteValue" runat="server" CssClass="item" Font-Bold="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </asp:Panel>
            <br />
            <table border="0" style="width: 100%" cellpadding="5" cellspacing="5">
                <tr>
                    <td>
                        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" SelectedIndex="0" Skin="Bootstrap" CausesValidation="false" MultiPageID="RadMultiPage1">
                            <Tabs>
                                <telerik:RadTab runat="server" Text="Datos Generales" Enabled="True" Selected="true">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Text="Datos Beneficiarios" Enabled="False">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Text="IMSS" Enabled="False">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Text="Infonavit" Enabled="False" Selected="true">
                                </telerik:RadTab>
                                <%--<telerik:RadTab runat="server" Text="Prestamos y Descuentos" Enabled="False">
                                    </telerik:RadTab>--%>
                                <%--<telerik:RadTab runat="server" Text="Incapacidades" Enabled="False">
                                </telerik:RadTab>--%>
                                <telerik:RadTab runat="server" Text="Anexos" Enabled="False">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Text="Contratos" Enabled="False">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Text="Crédito Fonacot" Enabled="False" Visible="false">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Text="Prestamos Personales" Enabled="False" Visible="false">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                    </td>
                </tr>
            </table>
            <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                <telerik:RadPageView ID="DatosGeneralesView" runat="server" Selected="true">
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblDatosGenerales" Text="Datos Generales" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                        </legend>
                        <table border="0" style="width: 100%" cellpadding="5" cellspacing="5">
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="lblNoEmpleado" runat="server" CssClass="item" Font-Bold="True" Text="No. de Empleado:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <telerik:RadNumericTextBox ID="txtNoEmpleado" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" runat="server"></telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="valNoEmpleado" runat="server" ControlToValidate="txtNoEmpleado" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblNombre" runat="server" CssClass="item" Font-Bold="True" Text="Nombre:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblApellidoPaterno" runat="server" CssClass="item" Font-Bold="True" Text="Apellido Paterno:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblApellidoMaterno" runat="server" CssClass="item" Font-Bold="True" Text="Apellido Materno:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblSexo" runat="server" CssClass="item" Font-Bold="True" Text="Sexo:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtNombre" Width="300px" runat="server"></telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtApellidoPaterno" Width="300px" runat="server"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtApellidoPaterno" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtApellidoMaterno" Width="300px" runat="server"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtApellidoMaterno" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td width="25%" class="item">
                                    <asp:RadioButtonList ID="rblSexo" RepeatDirection="Horizontal" runat="server">
                                        <asp:ListItem Value="0" Text="Masculino" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Femenino"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblRFC" runat="server" CssClass="item" Font-Bold="True" Text="RFC:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblCURP" runat="server" CssClass="item" Font-Bold="True" Text="CURP:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblEstadoCivil" runat="server" CssClass="item" Font-Bold="True" Text="Estado Civil:"></asp:Label>
                                </td>
                                <td style="width: 25%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtRFC" Width="300px" runat="server"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRFC" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="valRFC" runat="server" ControlToValidate="txtRFC" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Formato no válido" SetFocusOnError="True" ValidationExpression="^([a-zA-Z]{3,4})\d{6}([a-zA-Z\w]{3})$"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtCURP" Width="300px" runat="server"></telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCURP" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadComboBox ID="ddlEstadoCivil" runat="server" Width="300px"></telerik:RadComboBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlEstadoCivil" ValidationGroup="vDatosGenerales" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td style="width: 25%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblCalle" runat="server" CssClass="item" Font-Bold="True" Text="Calle:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblNoExterior" runat="server" CssClass="item" Font-Bold="True" Text="No. Exterior:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblNoInterior" runat="server" CssClass="item" Font-Bold="True" Text="No. Interior:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblReferencia" runat="server" CssClass="item" Font-Bold="True" Text="Referencia:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtCalle" Width="300px" runat="server"></telerik:RadTextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCalle" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtNoExterior" Width="100px" runat="server"></telerik:RadTextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtNoExterior" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtNoInterior" Width="100px" runat="server"></telerik:RadTextBox>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtReferencia" Width="300px" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblColonia" runat="server" CssClass="item" Font-Bold="True" Text="Colonia:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblPais" runat="server" CssClass="item" Font-Bold="True" Text="País:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblEstado" runat="server" CssClass="item" Font-Bold="True" Text="Estado:"></asp:Label>
                                </td>
                                <td style="width: 25%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtColonia" Width="300px" runat="server"></telerik:RadTextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtColonia" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtPais" Text="México" Width="300px" runat="server"></telerik:RadTextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtPais" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadComboBox ID="ddlEstado" runat="server" Width="300px" AutoPostBack="true"></telerik:RadComboBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlEstado" ValidationGroup="vDatosGenerales" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td style="width: 25%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblMunicipio" runat="server" CssClass="item" Font-Bold="True" Text="Municipio:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblCP" runat="server" CssClass="item" Font-Bold="True" Text="Código Postal:"></asp:Label>
                                </td>
                                <td style="width: 25%;">&nbsp;</td>
                                <td style="width: 25%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <telerik:RadComboBox ID="ddlMunicipio" runat="server" Width="300px"></telerik:RadComboBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlMunicipio" ValidationGroup="vDatosGenerales" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtCP" Width="100px" runat="server"></telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtCP" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%;">&nbsp;</td>
                                <td style="width: 25%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblTelefonoCasa" runat="server" CssClass="item" Font-Bold="True" Text="Teléfono Casa:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblTelefonoTrabajo" runat="server" CssClass="item" Font-Bold="True" Text="Telefono Trabajo:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblTelefonoCelular" runat="server" CssClass="item" Font-Bold="True" Text="Celular:"></asp:Label>
                                </td>
                                <td style="width: 25%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtTelefono" Text="" Width="300px" runat="server"></telerik:RadTextBox>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtTelefonoTrabajo" Text="" Width="300px" runat="server"></telerik:RadTextBox>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtCelular" Text="" Width="300px" runat="server"></telerik:RadTextBox>
                                </td>
                                <td style="width: 25%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblEmail1" runat="server" CssClass="item" Font-Bold="True" Text="Email 1:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblEmail2" runat="server" CssClass="item" Font-Bold="True" Text="Email 2:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblEmail3" runat="server" CssClass="item" Font-Bold="True" Text="Email 3:"></asp:Label>
                                </td>
                                <td style="width: 25%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtEmail1" Text="" Width="300px" runat="server"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtEmail1" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail1" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ValidationExpression=".*@.*\..*" ErrorMessage="Formato Inválido"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtEmail2" Text="" Width="300px" runat="server"></telerik:RadTextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail2" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ValidationExpression=".*@.*\..*" ErrorMessage="Formato Inválido"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtEmail3" Text="" Width="300px" runat="server"></telerik:RadTextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtEmail3" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ValidationExpression=".*@.*\..*" ErrorMessage="Formato Inválido"></asp:RegularExpressionValidator>
                                </td>
                                <td style="width: 25%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblFechaNacimiento" runat="server" CssClass="item" Font-Bold="True" Text="Fecha Nacimiento:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblLugarNacimiento" runat="server" CssClass="item" Font-Bold="True" Text="Lugar de Nacimiento:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblNombrePadre" runat="server" CssClass="item" Font-Bold="True" Text="Nombre del padre:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblNombreMadre" runat="server" CssClass="item" Font-Bold="True" Text="Nombre de la madre:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <telerik:RadDatePicker ID="calFechaNacimiento" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server" MinDate="01/01/1900"></telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="calFechaNacimiento" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtLugarNacimiento" Width="300px" runat="server"></telerik:RadTextBox>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtNombrePadre" Width="300px" runat="server"></telerik:RadTextBox>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtNombreMadre" Width="300px" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblMetodoPago" runat="server" CssClass="item" Font-Bold="True" Text="Método de Pago:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblBanco" runat="server" CssClass="item" Font-Bold="True" Text="Banco:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblSucursal" runat="server" CssClass="item" Font-Bold="True" Text="Sucursal:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblClabe" runat="server" CssClass="item" Font-Bold="True" Text="CLABE:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <telerik:RadComboBox ID="ddlFormaPago" runat="server" Width="300px" AutoPostBack="true"></telerik:RadComboBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ValidationGroup="vDatosGenerales" ControlToValidate="ddlFormaPago" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadComboBox ID="ddlBanco" runat="server" Width="300px"></telerik:RadComboBox>
                                    <%--<asp:RequiredFieldValidator ID="valReqBanco" ValidationGroup="vDatosGenerales" runat="server" ControlToValidate="ddlBanco" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtSucursal" Width="300px" runat="server"></telerik:RadTextBox>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtClabe" Width="300px" runat="server"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblNoTarjeta" runat="server" CssClass="item" Font-Bold="True" Text="No. de Tarjeta Bancaria:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblCuenta" runat="server" CssClass="item" Font-Bold="True" Text="No. de Cuenta:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="Label1" runat="server" CssClass="item" Font-Bold="True" Text="Cliente:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="Label16" runat="server" CssClass="item" Font-Bold="True" Text="Sueldos y Salarios"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtNumTarjeta" Width="300px" runat="server"></telerik:RadTextBox>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadTextBox ID="txtCuenta" Width="300px" runat="server"></telerik:RadTextBox>
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadComboBox ID="ddCliente" runat="server" Filter="StartsWith" OnClientFocus="clearFilters" Width="95%"></telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="valCliente" ValidationGroup="vDatosGenerales" runat="server" ControlToValidate="ddCliente" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%;">
                                    <asp:CheckBox ID="checkSueldosYSalarios" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblExcedente" runat="server" CssClass="item" Font-Bold="True" Text="Excedente"></asp:Label>
                                </td>
                                <td style="width: 25%;">&nbsp;
                                <asp:Label ID="lblExcedenteMonetario" runat="server" CssClass="item" Font-Bold="True" Text="Excedente Monetario"></asp:Label>
                                </td>
                                <td style="width: 25%;">&nbsp;</td>
                                <td style="width: 25%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <asp:CheckBox ID="checkExcedente" runat="server" />
                                </td>
                                <td style="width: 25%;">
                                    <telerik:RadNumericTextBox ID="txtExcedenteMonetario" Width="300px" runat="server"></telerik:RadNumericTextBox>
                                </td>
                                <td style="width: 25%;">&nbsp;</td>
                                <td style="width: 25%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3" width="75%" align="left">
                                    <%--<asp:Label ID="lblMensajeDatosGenerales" runat="server" CssClass="item"></asp:Label>--%>
                                </td>
                                <td width="25%" align="right">
                                    <telerik:RadButton ID="btnCancelarGuardarEmpleados" runat="server" Text="Cancelar" Width="90px" CssClass="rbPrimaryButton" CausesValidation="False"></telerik:RadButton>
                                    &nbsp;
                                    <telerik:RadButton ID="btnGuardarEmpleado" runat="server" ValidationGroup="vDatosGenerales" Text="Guardar" CssClass="rbPrimaryButton" Width="90px" CausesValidation="True"></telerik:RadButton>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:Label ID="LblMensaje" runat="server" CssClass="item" Font-Bold="True" ForeColor="Red" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:HiddenField ID="EmployeeID" runat="server" Value="0" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <br />
                </telerik:RadPageView>
                <telerik:RadPageView ID="DatosBeneficiariosView" runat="server">
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblDatosBeneficiarios" Text="Datos de Beneficiarios" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <telerik:RadGrid ID="beneficiariosList" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" GridLines="None" AllowSorting="true"
                            PageSize="15" ShowStatusBar="True"
                            Skin="Bootstrap" Width="100%">
                            <PagerStyle Mode="NextPrevAndNumeric" />
                            <MasterTableView AllowMultiColumnSorting="true" AllowSorting="true" DataKeyNames="id" Name="Beneficiarios" Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="id" HeaderText="Clave" UniqueName="id"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Nombre" DataField="nombre" SortExpression="nombre" UniqueName="nombre">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("nombre") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="parentesco" HeaderText="Parentesco" UniqueName="parentesco">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="porcentaje" HeaderText="Porcentaje" UniqueName="porcentaje">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <br />
                        <div style="float: right;">
                            <telerik:RadButton ID="btnCerrarBeneficiario" runat="server" Text="Cancelar" CssClass="rbPrimaryButton" CausesValidation="False"></telerik:RadButton>
                            &nbsp;
                            <telerik:RadButton ID="btnAgregaBeneficiario" runat="server" CausesValidation="false" CssClass="rbPrimaryButton" Text="Agregar Beneficiario"></telerik:RadButton>
                        </div>
                        <br />
                        <br />
                        <asp:Panel ID="panelBeneficiarios" runat="server" Visible="False">
                            <fieldset>
                                <legend style="padding-right: 6px; color: Black">
                                    <asp:Label ID="Label19" Text="Agregar/Editar Beneficiario" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                                </legend>
                                <table style="width: 100%;" border="0">
                                    <tr>
                                        <td colspan="4">
                                            <asp:Label ID="lblNombreBeneficiario" runat="server" CssClass="item" Font-Bold="True" Text="Nombre Beneficiario:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <telerik:RadTextBox ID="txtNombreBeneficiario" Width="300px" runat="server"></telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator42" runat="server" ControlToValidate="txtNombreBeneficiario" ValidationGroup="vDatosBeneficiario" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblParentesco" runat="server" CssClass="item" Font-Bold="True" Text="Parentesco:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblOtroParentesco" runat="server" CssClass="item" Font-Bold="True" Text="Otro:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblFechaNacimientoBeneficiario" runat="server" CssClass="item" Font-Bold="True" Text="Fecha Nacimiento:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblPorcentaje" runat="server" CssClass="item" Font-Bold="True" Text="Porcentaje:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="ddlParentescoBeneficiario" runat="server" Width="300px"></telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ControlToValidate="ddlParentescoBeneficiario" ValidationGroup="vDatosBeneficiario" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadTextBox ID="txtOtroParentesco" Enabled="false" Width="300px" runat="server"></telerik:RadTextBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadDatePicker ID="calfechaNacimientoBeneficiario" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server" MinDate="01/01/1900"></telerik:RadDatePicker>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator45" runat="server" ControlToValidate="calfechaNacimientoBeneficiario" ValidationGroup="vDatosBeneficiario" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadNumericTextBox ID="txtPorcentajeBeneficiario" runat="server" Type="Percent" MinValue="0" Width="100px"></telerik:RadNumericTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator44" runat="server" ControlToValidate="txtPorcentajeBeneficiario" ValidationGroup="vDatosBeneficiario" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblCalleBeneficiario" runat="server" CssClass="item" Font-Bold="True" Text="Calle:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblNoExteriorBeneficiario" runat="server" CssClass="item" Font-Bold="True" Text="No. Exterior:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblNoInteriorBeneficiario" runat="server" CssClass="item" Font-Bold="True" Text="No. Interior:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <telerik:RadTextBox ID="txtCalleBeneficiario" Width="300px" runat="server"></telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="txtCalleBeneficiario" ValidationGroup="vDatosBeneficiario" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadTextBox ID="txtNoExteriorBeneficiario" Width="100px" runat="server"></telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="txtNoExteriorBeneficiario" ValidationGroup="vDatosBeneficiario" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadTextBox ID="txtNoInteriorBeneficiario" Width="100px" runat="server"></telerik:RadTextBox>
                                        </td>
                                        <td style="width: 25%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblColoniaBeneficiario" runat="server" CssClass="item" Font-Bold="True" Text="Colonia:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblMunicipioBeneficiario" runat="server" CssClass="item" Font-Bold="True" Text="Municipio:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblCPBeneficiario" runat="server" CssClass="item" Font-Bold="True" Text="Código Postal:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <telerik:RadTextBox ID="txtColoniaBeneficiario" Width="300px" runat="server"></telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="txtColoniaBeneficiario" ValidationGroup="vDatosBeneficiario" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadTextBox ID="txtMunicipioBeneficiario" Width="300px" runat="server"></telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server" ControlToValidate="txtMunicipioBeneficiario" ValidationGroup="vDatosBeneficiario" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadTextBox ID="txtCPBeneficiario" Width="100px" runat="server"></telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txtCPBeneficiario" ValidationGroup="vDatosBeneficiario" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblEstadoBeneficiario" runat="server" CssClass="item" Font-Bold="True" Text="Estado:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblPaisBeneficiario" runat="server" CssClass="item" Font-Bold="True" Text="País:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">&nbsp;</td>
                                        <td style="width: 25%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="ddlEstadoBeneficiario" runat="server" Width="300px"></telerik:RadComboBox>
                                            <asp:RequiredFieldValidator ID="valEstadoBeneficiario" runat="server" ControlToValidate="ddlEstadoBeneficiario" ValidationGroup="vDatosBeneficiario" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadTextBox ID="txtPaisBeneficiario" Text="México" Width="300px" runat="server"></telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator41" runat="server" ControlToValidate="txtPaisBeneficiario" ValidationGroup="vDatosBeneficiario" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">&nbsp;</td>
                                        <td style="width: 25%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" width="75%" align="left">
                                            <asp:Label ID="lblMensajeDatosBeneficiario" runat="server" CssClass="item"></asp:Label>
                                        </td>
                                        <td width="25%" align="right">
                                            <telerik:RadButton ID="btnCancelarDBeneficiarios" runat="server" Text="Cancelar" CssClass="rbPrimaryButton" CausesValidation="False"></telerik:RadButton>
                                            &nbsp;
                                        <telerik:RadButton ID="btnGuardarDBeneficiarios" ValidationGroup="vDatosBeneficiario" runat="server" CssClass="rbPrimaryButton" Text="Guardar"></telerik:RadButton>
                                        </td>
                                    </tr>
                                    <tr style="width: 20px;">
                                        <td colspan="4">
                                            <asp:HiddenField ID="BeneficiarioID" runat="server" Value="0" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:Panel>
                        <br />
                        <br />
                    </fieldset>
                    <br />
                </telerik:RadPageView>
                <telerik:RadPageView ID="DatosIMSS" runat="server">
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lbldatosIMSS" Text="Datos IMSS" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <table style="width: 100%;" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="lblNoSegSocial" runat="server" CssClass="item" Font-Bold="True" Text="No. Seguro Social:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblDelegacion" runat="server" CssClass="item" Font-Bold="True" Text="Delegación:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblSubDelegacion" runat="server" CssClass="item" Font-Bold="True" Text="Sub-Delegación:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblUnidadMedicoFamiliar" runat="server" CssClass="item" Font-Bold="True" Text="Unidad Médico Familiar:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblZonaGeografica" runat="server" CssClass="item" Font-Bold="True" Text="Zona:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadTextBox ID="txtNoSegSocial" Width="190px" runat="server"></telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNoSegSocial" ValidationGroup="vDatosIMSS" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtDelegacion" Width="190px" runat="server"></telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtSubDelegacion" Width="190px" runat="server"></telerik:RadTextBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtUnidadMedicoFamiliar" Width="190px" runat="server"></telerik:RadTextBox>
                                </td>
                                <td>
                                    <asp:Label ID="lblZonaGeográficaValue" runat="server" CssClass="item" Font-Bold="True" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%;" border="0">
                            <tr style="width: 20px;">
                                <td colspan="2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="75%" align="left">
                                    <asp:Label ID="lblMensajeDatosIMSS" runat="server" CssClass="item"></asp:Label>
                                </td>
                                <td width="25%" align="right">
                                    <%--<telerik:RadButton ID="btnCerrarDIMSS" runat="server" Text="Cerrar" CausesValidation="False" />--%>
                                    <telerik:RadButton ID="btnCancelarDIMSS" runat="server" CssClass="rbPrimaryButton" Text="Cancelar" CausesValidation="False" />
                                    &nbsp;
                                    <telerik:RadButton ID="btnGuardarDIMSS" ValidationGroup="txtNoSegSocial" CssClass="rbPrimaryButton" runat="server" Text="Guardar" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                    </fieldset>
                    <br />
                </telerik:RadPageView>
                <telerik:RadPageView ID="DatosInfonavitView" runat="server">
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblDatosInfonavit" Text="Datos Infonavit" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <table style="width: 100%;" border="0">
                            <tr>
                                <td style="width: 16%;">
                                    <asp:Label ID="lblDescuentoAutomatico" runat="server" CssClass="item" Font-Bold="True" Text="Descuento Automático:"></asp:Label>
                                </td>
                                <td style="width: 16%;">
                                    <asp:Label ID="lblNoCredito" runat="server" CssClass="item" Font-Bold="True" Text="No. Crédito:"></asp:Label>
                                </td>
                                <td style="width: 16%;">
                                    <asp:Label ID="lblInicioDescuento" runat="server" CssClass="item" Font-Bold="True" Text="Inicio Descuento:"></asp:Label>
                                </td>
                                <td style="width: 16%;">
                                    <asp:Label ID="lblFinDescuento" runat="server" CssClass="item" Font-Bold="True" Text="Fin Descuento:"></asp:Label>
                                </td>
                                <td style="width: 16%;">
                                    <asp:Label ID="lblTipoDescuento" runat="server" CssClass="item" Font-Bold="True" Text="Tipo Descuento:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblValor" runat="server" CssClass="item" Font-Bold="True" Text="Valor:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:CheckBox ID="chkDescuentoAutomatico" Text="" CssClass="item" Font-Bold="True" runat="server" />
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtNoCredito" Width="127px" runat="server"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtNoCredito" ValidationGroup="vDatosInfonavit" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="calInicioDescuento" CultureInfo="Español (México)" MinDate="01/01/1800" DateInput-DateFormat="dd/MM/yyyy" runat="server" Width="130px"></telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="calInicioDescuento" ValidationGroup="vDatosInfonavit" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="calFinDescuento" CultureInfo="Español (México)" MinDate="01/01/1800" DateInput-DateFormat="dd/MM/yyyy" runat="server" Width="130px"></telerik:RadDatePicker>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="ddlTipoDescuento" runat="server"></telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="ddlTipoDescuento" InitialValue="--Seleccione--" ValidationGroup="vDatosInfonavit" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtValorDescuento" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="6" Width="100px"></telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator46" runat="server" ControlToValidate="txtValorDescuento" ValidationGroup="vDatosInfonavit" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%;" border="0">
                            <tr style="width: 20px;">
                                <td colspan="2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="75%" align="left">
                                    <asp:Label ID="lblMensajeDatosInfonavit" runat="server" CssClass="item"></asp:Label>
                                </td>
                                <td width="25%" align="right">
                                    <%--<telerik:RadButton ID="btnCerrarDInfonavit" runat="server" Text="Cerrar" CausesValidation="False" />--%>
                                    <telerik:RadButton ID="btnCancelarDInfonavit" runat="server" Text="Cancelar" CssClass="rbPrimaryButton" CausesValidation="False" />
                                    &nbsp;
                                    <telerik:RadButton ID="btnGuardarDInfonavit" ValidationGroup="vDatosInfonavit" CssClass="rbPrimaryButton" runat="server" Text="Guardar" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                    </fieldset>
                    <br />
                </telerik:RadPageView>
                <%--<telerik:RadPageView ID="Incapacidades" runat="server">
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="Label24" Text="Incapacidades" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <table style="width: 100%;" border="0">
                            <tr>
                                <td style="width: 16%;">
                                    <asp:Label ID="Label25" runat="server" CssClass="item" Font-Bold="True" Text="Descuento Automático:"></asp:Label>
                                </td>
                                <td style="width: 16%;">
                                    <asp:Label ID="Label26" runat="server" CssClass="item" Font-Bold="True" Text="No. Crédito:"></asp:Label>
                                </td>
                                <td style="width: 16%;">
                                    <asp:Label ID="Label27" runat="server" CssClass="item" Font-Bold="True" Text="Inicio Descuento:"></asp:Label>
                                </td>
                                <td style="width: 16%;">
                                    <asp:Label ID="Label28" runat="server" CssClass="item" Font-Bold="True" Text="Fin Descuento:"></asp:Label>
                                </td>
                                <td style="width: 16%;">
                                    <asp:Label ID="Label29" runat="server" CssClass="item" Font-Bold="True" Text="Tipo Descuento:"></asp:Label>
                                </td>
                                <td style="width: 16%;">
                                    <asp:Label ID="Label30" runat="server" CssClass="item" Font-Bold="True" Text="Valor:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:CheckBox ID="chkDescuentoAutomaticoIncapacidad" Text="" CssClass="item" Font-Bold="True" runat="server" />
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtNoIncapacidad" Width="127px" runat="server"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="valNoIncapacidad" runat="server" ControlToValidate="txtNoIncapacidad" ValidationGroup="vgDescuentoIncapacidad" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="radFechaInicio" CultureInfo="Español (México)" MinDate="01/01/1800" DateInput-DateFormat="dd/MM/yyyy" runat="server" Width="120px"></telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="valFechaInicioIncapacidad" runat="server" ControlToValidate="radFechaInicio" ValidationGroup="vgDescuentoIncapacidad" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="valFechaFinIncapacidad" CultureInfo="Español (México)" MinDate="01/01/1800" DateInput-DateFormat="dd/MM/yyyy" runat="server" Width="120px"></telerik:RadDatePicker>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlRamoSeguro" Width="170px" Enabled="true" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="valRamoSeguro" runat="server" ControlToValidate="ddlRamoSeguro" InitialValue="--Seleccione--" ValidationGroup="vgDescuentoIncapacidad" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="radnDiasAuto" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="6" Width="100px"></telerik:RadNumericTextBox>
                                    <asp:RequiredFieldValidator ID="valDiasAuto" runat="server" ControlToValidate="radnDiasAuto" ValidationGroup="vgDescuentoIncapacidad" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%;" border="0">
                            <tr style="width: 20px;">
                                <td colspan="2">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="75%" align="left">
                                    <asp:Label ID="Label31" runat="server" CssClass="item"></asp:Label>
                                </td>
                                <td width="25%" align="right">
                                    <telerik:RadButton ID="btnCerrarIncapacidades" runat="server" Text="Cerrar" CausesValidation="False" />
                                    &nbsp;
                                        <telerik:RadButton ID="btnCancelarIncapacidades" runat="server" Text="Cancelar" CausesValidation="False" />
                                    &nbsp;
                                        <telerik:RadButton ID="btnGuardarIncapacidades" runat="server" Text="Guardar" CssClass="rbPrimaryButton" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                    </fieldset>
                    <br />
                </telerik:RadPageView>--%>
                <telerik:RadPageView ID="AnexosView" runat="server">
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblAnexos" Text="Anexos" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <table style="width: 100%;" border="0">
                            <tr>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblAnexo" runat="server" CssClass="item" Font-Bold="True" Text="Anexo:"></asp:Label>
                                </td>
                                <td style="width: 25%;">
                                    <asp:Label ID="lblDocumento" runat="server" CssClass="item" Font-Bold="True" Text="Documento:"></asp:Label>
                                </td>
                                <td style="width: 25%;">&nbsp;</td>
                                <td style="width: 25%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">
                                    <telerik:RadComboBox ID="cboAnexo" runat="server" Width="80%"></telerik:RadComboBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="cboAnexo" ValidationGroup="vAnexoContrato" CssClass="item" ForeColor="Red" ErrorMessage="Requerido" SetFocusOnError="true" InitialValue="">
                                    </asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%;">
                                    <asp:FileUpload ID="anexo" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="anexo" ValidationGroup="vAnexoContrato" CssClass="item" ForeColor="Red" ErrorMessage="Seleccione un documento" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                                <td style="width: 25%;">&nbsp;</td>
                                <td style="width: 25%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 25%;">&nbsp;</td>
                                <td style="width: 25%;">&nbsp;</td>
                                <td style="width: 25%;">&nbsp;</td>
                                <td style="width: 25%;">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4" align="left">
                                    <%--<telerik:RadButton ID="btnCerrarAnexo" runat="server" Text="Cerrar" CausesValidation="False" />--%>
                                    <telerik:RadButton ID="btnCancelarAnexo" runat="server" Text="Cancelar" CssClass="rbPrimaryButton" CausesValidation="False" />
                                    &nbsp;
                                    <telerik:RadButton ID="btnGuardarAnexo" ValidationGroup="vAnexoContrato" runat="server" Text="Guardar" CssClass="rbPrimaryButton" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="left">
                                    <asp:Label ID="lblMensajeAnexo" runat="server" CssClass="item"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table style="width: 100%;" border="0">
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="anexosList" Width="50%" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" Skin="Bootstrap">
                                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                        </ClientSettings>
                                        <MasterTableView DataKeyNames="id,documento,documento" NoMasterRecordsText="No hay registros para mostrar.">
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="id" HeaderStyle-Width="10px" HeaderText="Folio" UniqueName="id">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="nombre" HeaderStyle-Width="10px" HeaderText="Nombre" UniqueName="id">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Anexo" UniqueName="anexo">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="DownloadAnexo" runat="server" CommandArgument='<% #Eval("documento")%>' CommandName="cmdDownload" ImageUrl="~/images/download.png" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="eliminar">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="eliminar" runat="server" CommandArgument='<% #Eval("id")%>' CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                    </fieldset>
                    <br />
                </telerik:RadPageView>
                <telerik:RadPageView ID="ContratoView" runat="server">
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblDatosContrato" Text="Datos Contrato" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <telerik:RadGrid ID="contratosList" runat="server" AllowPaging="true"
                            AutoGenerateColumns="false" GridLines="None" AllowSorting="false"
                            PageSize="20" ShowStatusBar="True"
                            Skin="Bootstrap" Width="100%">
                            <PagerStyle Mode="NextPrevAndNumeric" />
                            <MasterTableView AllowMultiColumnSorting="false" AllowSorting="false" DataKeyNames="id ,tienealta" Name="Contratos" Width="100%">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="id" HeaderText="Clave" UniqueName="id"></telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Fecha Alta" DataField="fecha_alta" SortExpression="fecha_alta" UniqueName="fecha_alta">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("fecha_alta") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="empleado" HeaderText="Empleado" UniqueName="empleado">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ejecutivo" HeaderText="Ejecutivo" UniqueName="ejecutivo">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="estatus" HeaderText="Estatus" UniqueName="estatus">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="etapaFiniquito" HeaderText="Etapa Finiquito" UniqueName="etapa">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="fecha_finiquito" HeaderText="Fecha de pago" UniqueName="fechapago">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="tipo_jornada" HeaderText="Tipo Jornada" UniqueName="tipo_jornada">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="tipo_salario" HeaderText="Tipo Salario" UniqueName="tipo_salario">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="sueldo_diario" HeaderText="Salario Diario" UniqueName="sueldo_diario" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="sueldo_diario_integrado" HeaderText="Salario Diario Integrado" UniqueName="sueldo_diario_integrado" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="tipo_contrato" HeaderText="Tipo Contrato" UniqueName="tipo_contrato">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Contrato" UniqueName="contrato" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDescargarContrato" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDownload" Text="Descargar" CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Movimiento IMSS" UniqueName="MovimientosIMSS">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkMovimiento" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdMovimiento" Text="Ver" CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" Visible="false" HeaderText="Enviar" UniqueName="Send">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgSend" runat="server" ImageUrl="~/images/envelope.jpg" CommandArgument='<%# Eval("id") %>' CommandName="cmdSend"></asp:ImageButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="False" Visible="false" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <br />
                        <table style="width: 100%;" border="0">
                            <tr>
                                <td style="text-align: right;">
                                    <telerik:RadButton ID="btnCerrarContrato" runat="server" Text="Cerrar" CssClass="rbPrimaryButton" CausesValidation="False" />
                                    &nbsp;
                                    <telerik:RadButton ID="btnAgregaContrato" runat="server" Text="Agregar Contrato" CssClass="rbPrimaryButton" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:Panel ID="panelContratos" runat="server" Visible="False">
                            <fieldset>
                                <legend style="padding-right: 6px; color: Black">
                                    <asp:Label ID="lblAgregarEditarContrato" Text="Agregar/Editar Contrato" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                                </legend>
                                <table border="0" cellpadding="3" width="100%">
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblTipoJornada" runat="server" CssClass="item" Font-Bold="True" Text="Tipo de Jornada:"></asp:Label>
                                            <asp:RequiredFieldValidator ID="valTipoJornada" runat="server" ControlToValidate="ddlTipoJornada" ValidationGroup="vDatosContrato" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblTipoSalario" runat="server" CssClass="item" Font-Bold="True" Text="Tipo de Salario:"></asp:Label>
                                            <asp:RequiredFieldValidator ID="valTipoSalario" runat="server" ControlToValidate="ddlTipoSalario" ValidationGroup="vDatosContrato" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblTipoContrato" runat="server" CssClass="item" Font-Bold="True" Text="Tipo de Contrato:"></asp:Label>
                                            <asp:RequiredFieldValidator ID="valTipoContrato" runat="server" ControlToValidate="ddlTipoSalario" ValidationGroup="vDatosContrato" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblMeses" runat="server" CssClass="item" Font-Bold="True" Text="Meses:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="ddlTipoJornada" runat="server" Width="300px">
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="0" Text="--Seleccione--" />
                                                    <telerik:RadComboBoxItem Value="1" Text="Permanente" />
                                                    <telerik:RadComboBoxItem Value="2" Text="Variable" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="ddlTipoSalario" runat="server" Width="300px"></telerik:RadComboBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="ddlTipoContrato" runat="server" AutoPostBack="true" Width="300px"></telerik:RadComboBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="dllMesesEventual" runat="server" Width="300px">
                                                <Items>
                                                    <telerik:RadComboBoxItem Value="0" Text="--Seleccione--" />
                                                    <telerik:RadComboBoxItem Value="1" Text="1 mes" />
                                                    <telerik:RadComboBoxItem Value="2" Text="2 meses" />
                                                    <telerik:RadComboBoxItem Value="3" Text="3 meses" />
                                                    <telerik:RadComboBoxItem Value="4" Text="4 meses" />
                                                    <telerik:RadComboBoxItem Value="5" Text="5 meses" />
                                                    <telerik:RadComboBoxItem Value="6" Text="6 meses" />
                                                    <telerik:RadComboBoxItem Value="7" Text="7 meses" />
                                                    <telerik:RadComboBoxItem Value="8" Text="8 meses" />
                                                    <telerik:RadComboBoxItem Value="9" Text="9 meses" />
                                                    <telerik:RadComboBoxItem Value="10" Text="10 meses" />
                                                    <telerik:RadComboBoxItem Value="11" Text="11 meses" />
                                                    <telerik:RadComboBoxItem Value="12" Text="12 meses" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblDepartamento" runat="server" CssClass="item" Font-Bold="True" Text="Departamento:"></asp:Label>
                                            <asp:RequiredFieldValidator ID="valDepartamento" runat="server" ControlToValidate="ddlDepartamento" ValidationGroup="vDatosContrato" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblPuesto" runat="server" CssClass="item" Font-Bold="True" Text="Puesto:"></asp:Label>
                                            <asp:RequiredFieldValidator ID="valPuesto" runat="server" ControlToValidate="ddlPuesto" ValidationGroup="vDatosContrato" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblRiesgoPuesto" runat="server" CssClass="item" Font-Bold="True" Text="Riesgo Puesto:"></asp:Label>
                                            <asp:RequiredFieldValidator ID="valRiesgoPuesto" runat="server" ControlToValidate="ddlRiesgoPuesto" ValidationGroup="vDatosContrato" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblFechaAlta" runat="server" CssClass="item" Font-Bold="True" Text="Fecha Ingreso:"></asp:Label>
                                            <asp:RequiredFieldValidator ID="valFechaIngreso" runat="server" ControlToValidate="calFechaIngreso" ValidationGroup="vDatosContrato" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="ddlDepartamento" runat="server" Width="300px"></telerik:RadComboBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="ddlPuesto" runat="server" Width="300px"></telerik:RadComboBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="ddlRiesgoPuesto" runat="server" Width="300px"></telerik:RadComboBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadDatePicker ID="calFechaIngreso" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblRegistroPatronal" runat="server" CssClass="item" Font-Bold="True" Text="Registro Patronal:"></asp:Label>
                                            <asp:RequiredFieldValidator ID="valRegistroPatronal" runat="server" ControlToValidate="ddlRegistroPatronal" ValidationGroup="vDatosContrato" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblRegimenContratacion" runat="server" CssClass="item" Font-Bold="True" Text="Régimen de Contratación:"></asp:Label>
                                            <asp:RequiredFieldValidator ID="valRegimenContratacion" ValidationGroup="vDatosContrato" runat="server" ControlToValidate="ddlRegimenContratacion" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblAnosAntiguedad" runat="server" CssClass="item" Font-Bold="True" Text="Años Antiguedad:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 50%">
                                                        <asp:Label ID="Label2" runat="server" CssClass="item" Font-Bold="True" Text="Reloj Checador Id:"></asp:Label>
                                                    </td>
                                                    <td style="width: 50%">
                                                        <asp:Label ID="Label3" runat="server" CssClass="item" Font-Bold="True" Text="Horario:"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="ddlRegistroPatronal" runat="server" Width="300px"></telerik:RadComboBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="ddlRegimenContratacion" runat="server" Width="300px"></telerik:RadComboBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadNumericTextBox ID="txtAnosAntiguedad" runat="server" Value="0" MinValue="0" Width="100px"></telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <table style="width: 100%">
                                                <tr>
                                                    <td style="width: 50%">
                                                        <telerik:RadNumericTextBox ID="txtRelojChecadorId" runat="server" Value="0" MinValue="0" Width="100px">
                                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                                            <ClientEvents OnKeyPress="KeyPress" />
                                                        </telerik:RadNumericTextBox>
                                                    </td>
                                                    <td style="width: 50%">
                                                        <telerik:RadComboBox ID="ddlHorario" runat="server" Width="100%"></telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblSueldoDiario" runat="server" CssClass="item" Font-Bold="True" Text="Salario Diario:"></asp:Label>
                                            <asp:RequiredFieldValidator ID="valSueldoDiario" runat="server" ControlToValidate="txtSueldoDiario" ValidationGroup="vDatosContrato" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblSuledoDiarioIntegrado" runat="server" CssClass="item" Font-Bold="True" Text="Salario Diario Integrado:"></asp:Label>
                                            <asp:RequiredFieldValidator ID="valSueldoDiarioIntegrado" runat="server" ControlToValidate="txtSueldoDiarioIntegrado" ValidationGroup="vDatosContrato" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblPercepcionesAsimilables" runat="server" CssClass="item" Font-Bold="True" Text="Otras Percepciones Asimilables:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblSalarioDiarioSJornadaReducida" runat="server" CssClass="item" Font-Bold="True" Text="Salario Diario Jor. Redu:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <telerik:RadNumericTextBox ID="txtSueldoDiario" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="5" Width="100px" AutoPostBack="true"></telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadNumericTextBox ID="txtSueldoDiarioIntegrado" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="5" Width="100px"></telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadNumericTextBox ID="txtPercepcionesAsimilables" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="5" Width="100px"></telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadNumericTextBox ID="txtSalarioDiarioSJornadaReducida" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="5" Width="100px"></telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblPeriodoPago" runat="server" CssClass="item" Font-Bold="True" Text="Período de Pago:"></asp:Label>
                                            <asp:RequiredFieldValidator ID="valPeriodoPago" ValidationGroup="vDatosContrato" runat="server" ControlToValidate="ddlPeriodoPago" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblNumeroReferenciaCliente" runat="server" CssClass="item" Font-Bold="True" Text="Número de Referencia Cliente:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">&nbsp;</td>
                                        <td style="width: 25%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="ddlPeriodoPago" runat="server" Width="300px"></telerik:RadComboBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadTextBox ID="txtNumeroReferenciaCliente" runat="server"></telerik:RadTextBox>
                                        </td>
                                        <td style="width: 25%;">&nbsp;</td>
                                        <td style="width: 25%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:Label ID="Label7" runat="server" CssClass="item" Font-Bold="True" Text="Descansos x Semana:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="Label9" runat="server" CssClass="item" Font-Bold="True" Text="Asimilado Total Semanal:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="Label5" runat="server" CssClass="item" Font-Bold="True" Text="% Impto. Cedular Est."></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="Label6" runat="server" CssClass="item" Font-Bold="True" Text="Pago x Hora:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <telerik:RadNumericTextBox ID="txtDescansosxsemana" runat="server" Value="0" MinValue="0" Width="100px">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadNumericTextBox ID="txtAsimiladoTotalS" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="5" Width="100px">
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadNumericTextBox ID="txtImptoCedular" runat="server" Value="0" MinValue="0" Width="100px">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadNumericTextBox ID="txtPagoxHora" runat="server" Value="0" MinValue="0" Width="100px">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:Label ID="Label10" runat="server" CssClass="item" Font-Bold="True" Text="Factor de Comisión:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="Label11" runat="server" CssClass="item" Font-Bold="True" Text="Factor Destajo:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="Label12" runat="server" CssClass="item" Font-Bold="True" Text="Horas al día:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="Label13" runat="server" CssClass="item" Font-Bold="True" Text="Integrado Tipo:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <telerik:RadNumericTextBox ID="txtFactorComision" runat="server" Value="0" MinValue="0" Width="100px">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadNumericTextBox ID="txtFactorDestajo" runat="server" Value="0" MinValue="0" Width="100px">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadNumericTextBox ID="txtHorasDia" runat="server" Value="0" MinValue="0" Width="100px">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadNumericTextBox ID="txtIntegradoTipo" runat="server" Value="0" MinValue="0" Width="100px">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:Label ID="Label14" runat="server" CssClass="item" Font-Bold="True" Text="Promedio Cuota Variable:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="Label15" runat="server" CssClass="item" Font-Bold="True" Text="Promedio Percep. Variables:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblTipoNomina" runat="server" CssClass="item" Font-Bold="True" Text="Tipo de Nomina:"></asp:Label>
                                            <asp:RequiredFieldValidator ID="valTipoNomina" runat="server" ControlToValidate="ddlTipoNomina" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true" ValidationGroup="vDatosContrato"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <telerik:RadNumericTextBox ID="txtPromCuotaV" runat="server" Value="0" MinValue="0" Width="100px">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadNumericTextBox ID="txtPromedioPercepV" runat="server" Value="0" MinValue="0" Width="100px">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                            </telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="ddlTipoNomina" runat="server" Width="300px"></telerik:RadComboBox>
                                        </td>
                                        <td style="width: 25%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblTipoDeContrato" runat="server" CssClass="item" Font-Bold="True" Text="Tipo de Contrato:(Nómina)"></asp:Label>
                                            <asp:RequiredFieldValidator ID="valTipoContratonomina" runat="server" ControlToValidate="ddlTipoContratonomina" ValidationGroup="vDatosContrato" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="TipoDeJornada" runat="server" CssClass="item" Font-Bold="True" Text="Tipo de Jornada:(Nomina)"></asp:Label>
                                            <asp:RequiredFieldValidator ID="valTipoJornadanomina" runat="server" ControlToValidate="ddlTipoJornadanomina" ValidationGroup="vDatosContrato" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblRegimenDeContratacion" runat="server" CssClass="item" Font-Bold="True" Text="Régimen de Contratación:(Nomina)"></asp:Label>
                                            <asp:RequiredFieldValidator ID="valRegimenContratacionnomina" runat="server" ControlToValidate="ddlRegimenContratacionnomina" ValidationGroup="vDatosContrato" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <%-- <asp:Label ID="Label19" runat="server" CssClass="item" Font-Bold="True" Text="Tipo de Jornada:"></asp:Label>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="ddlTipoContratonomina" runat="server" Width="300px"></telerik:RadComboBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="ddlTipoJornadanomina" runat="server" Width="300px"></telerik:RadComboBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="ddlRegimenContratacionnomina" runat="server" Width="300px"></telerik:RadComboBox>
                                        </td>
                                        <td style="width: 25%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblEjecutivo" runat="server" CssClass="item" Font-Bold="True" Text="Ejecutivo:"></asp:Label>
                                            <asp:RequiredFieldValidator ID="valEjecutivo" runat="server" ControlToValidate="ddlEjecutivo" ValidationGroup="vDatosContrato" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        </td>
                                        <td style="width: 25%;">
                                            <%--<asp:Label ID="lblPlaza" runat="server" CssClass="item" Font-Bold="True" Text="Plaza:"></asp:Label>--%>
                                        </td>
                                        <td style="width: 25%;">&nbsp;</td>
                                        <td style="width: 25%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="ddlEjecutivo" runat="server" Width="300px"></telerik:RadComboBox>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadComboBox ID="ddlPlaza" runat="server" Width="300px" Visible="false"></telerik:RadComboBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" ControlToValidate="ddlPlaza" ValidationGroup="vDatosContrato" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </td>
                                        <td style="width: 25%;">&nbsp;</td>
                                        <td style="width: 25%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Label ID="lblComentariosNoRecomendable" runat="server" CssClass="item" Font-Bold="True" Text="Comentarios Generales:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblComentariosAlta" runat="server" CssClass="item" Font-Bold="True" Text="Comentarios de la alta:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <telerik:RadTextBox ID="txtComentariosNoRecomendable" Width="90%" Height="80px" TextMode="MultiLine" runat="server"></telerik:RadTextBox>
                                        </td>
                                        <td colspan="2">
                                            <telerik:RadTextBox ID="txtComentariosAlta" Width="90%" Height="80px" TextMode="MultiLine" runat="server"></telerik:RadTextBox>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </fieldset>
                            <br />
                            <asp:Panel ID="panelBajaContrato" runat="server" Visible="False">
                                <fieldset>
                                    <legend style="padding-right: 6px; color: Black">
                                        <asp:Label ID="lblBajaContrato" Text="Sección de Baja de Contrato" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                                    </legend>
                                    <table style="width: 100%" border="0">
                                        <tr>
                                            <td style="width: 25%;">
                                                <asp:Label ID="lblFechaBaja" runat="server" CssClass="item" Font-Bold="True" Text="Fecha Baja:"></asp:Label>
                                            </td>
                                            <td style="width: 25%;">
                                                <asp:Label ID="lblTipoBaja" runat="server" CssClass="item" Font-Bold="True" Text="Tipo Baja:"></asp:Label>
                                            </td>
                                            <td colspan="3" width="50%">
                                                <asp:Label ID="lblComentarioImss" runat="server" CssClass="item" Font-Bold="True" Text="Comentarios de la baja:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%;">
                                                <telerik:RadDatePicker ID="calFechaBaja" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                                                <%--<asp:CustomValidator ID="valDateRange" ControlToValidate="calFechaBaja" runat="server" ValidationGroup="vDatosContrato" CssClass="item" ForeColor="Red" ErrorMessage="La fecha de baja debe ser menor máximo 5 dias al día de hoy"></asp:CustomValidator>--%>
                                            </td>
                                            <td style="width: 25%;">
                                                <telerik:RadComboBox ID="ddlTipoBaja" runat="server" Width="300px"></telerik:RadComboBox>
                                            </td>
                                            <td colspan="3" rowspan="2" style="vertical-align: top;">
                                                <telerik:RadTextBox ID="txtComentarioImss" Width="90%" Height="80px" TextMode="MultiLine" runat="server"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%;">
                                                <asp:CheckBox ID="chkRecomendable" Text="No es recomendable" CssClass="item" Font-Bold="True" runat="server" />
                                            </td>
                                            <td style="width: 25%;">
                                                <asp:Label ID="lblMotivoNoRecomendable" runat="server" CssClass="item" Font-Bold="True" Text="Motivo:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 25%;">&nbsp;</td>
                                            <td style="width: 25%;">
                                                <telerik:RadComboBox ID="ddlMotivoNoRecomendable" runat="server" Width="300px"></telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </asp:Panel>
                            <br />
                            <table style="width: 100%;" border="0">
                                <tr>
                                    <td colspan="4">
                                        <asp:Label ID="lblValidaFechaBaja" runat="server" CssClass="item" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="text-align: right;">
                                        <telerik:RadButton ID="btnFiniquito" runat="server" Text="Finiquitos" CssClass="rbPrimaryButton" CausesValidation="False" Visible="false" />
                                        &nbsp;&nbsp;&nbsp;
                                            <telerik:RadButton ID="btnCancelarContrato" runat="server" Text="Cancelar" CssClass="rbPrimaryButton" CausesValidation="False" />
                                        &nbsp;&nbsp;&nbsp;
                                            <telerik:RadButton ID="btnGuardarContrato" ValidationGroup="vDatosContrato" runat="server" CausesValidation="true" Text="Guardar" CssClass="rbPrimaryButton" />
                                    </td>
                                </tr>
                                <tr style="width: 20px;">
                                    <td colspan="4">
                                        <asp:HiddenField ID="ContratoID" runat="server" Value="0" />
                                        <asp:HiddenField ID="HFechaBaja" runat="server" Value="0" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="panelFiniquito" runat="server" Visible="False">
                            <fieldset>
                                <legend style="padding-right: 6px; color: Black">
                                    <asp:Label ID="lblSeccionFiniquitos" Text="Sección de Finiquitos" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                                </legend>
                                <asp:HiddenField ID="FiniquitoId" runat="server" Value="0" />
                                <table border="0" cellpadding="3" width="100%">
                                    <tr>
                                        <td colspan="8" class="ulwrapperblue">
                                            <asp:Label ID="Label8" runat="server" CssClass="TitleBaja" Font-Bold="True" Text="Sección de Finiquitos"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr style="height: 10px;">
                                        <td colspan="7">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%;">
                                            <asp:Label ID="lblejecutivof" runat="server" CssClass="item" Font-Bold="True" Text="Ejecutivo:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblfechadepago" runat="server" CssClass="item" Font-Bold="True" Text="Fecha de pago:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:Label ID="lblestapa" runat="server" CssClass="item" Font-Bold="True" Text="Etapa:"></asp:Label>
                                        </td>
                                        <td style="width: 25%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%;">
                                            <asp:DropDownList ID="cmbEjecutivo" runat="server"></asp:DropDownList>
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadDatePicker ID="calFiniquito" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server" Width="160px"></telerik:RadDatePicker>
                                            <%--<asp:RequiredFieldValidator ID="valfechaFiniquito" runat="server" ControlToValidate="calFiniquito" ValidationGroup="vDatosFiniquito" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>
                                        </td>
                                        <td style="width: 25%;">
                                            <asp:DropDownList ID="cmbEtapa" runat="server"></asp:DropDownList>
                                        </td>
                                        <td style="width: 25%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style1">
                                            <asp:Label ID="lblAnexoFiniq" runat="server" CssClass="item" Font-Bold="True" Text=" Anexo:"></asp:Label>
                                        </td>
                                        <td class="auto-style2">
                                            <asp:Label ID="lblMontop" runat="server" CssClass="item" Font-Bold="True" Text="Monto pagado:"></asp:Label>
                                        </td>
                                        <td class="auto-style2"></td>
                                        <td class="auto-style2"></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:FileUpload ID="anexoFiniquito" runat="server" />
                                        </td>
                                        <td style="width: 25%;">
                                            <telerik:RadNumericTextBox ID="txtMontoFiniquito" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="5" Width="100px"></telerik:RadNumericTextBox>
                                        </td>
                                        <td style="width: 25%;">&nbsp;</td>
                                        <td style="width: 25%;">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:LinkButton ID="lnkDownload" runat="server" Visible="false" CssClass="textGeneral" /></td>
                                        <td style="width: 25%;">&nbsp;</td>
                                        <td style="width: 25%;">&nbsp;</td>
                                        <td style="width: 25%;">&nbsp;</td>
                                    </tr>
                                </table>
                                <table style="width: 100%;">
                                    <tr>
                                        <td width="75%" align="left">
                                            <asp:Label ID="lblMensajeFiniquito" runat="server" CssClass="item"></asp:Label>
                                        </td>
                                        <td width="25%" align="right">
                                            <telerik:RadButton ID="btnSaveFiniquito" runat="server" Text="Guardar" CssClass="rbPrimaryButton" CausesValidation="False" />
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </asp:Panel>
                        <br />
                        <br />
                    </fieldset>
                    <br />
                </telerik:RadPageView>
                <telerik:RadPageView ID="CreditosFonacotView" runat="server">
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblCreditosFonacot" Text="Crédito FONACOT" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <table style="width: 100%;" border="0">
                            <tr>
                                <td style="width: 15%;">
                                    <asp:Label ID="lblNoClienteNonacot" runat="server" CssClass="item" Font-Bold="True" Text="No. Cliente Fonacot:"></asp:Label>
                                </td>
                                <td style="width: 20%;">
                                    <telerik:RadTextBox ID="txtNoClienteFonacot" runat="server" ValidationGroup="vNoClienteFonacot"></telerik:RadTextBox>
                                </td>
                                <td style="width: 65%;">
                                    <telerik:RadButton ID="btnGuardarNoClienteFonacot" runat="server" CssClass="rbPrimaryButton" Text="Guardar" ValidationGroup="vNoClienteFonacot" Visible="false" />
                                    &nbsp;
                                        <asp:RequiredFieldValidator ID="valNoClienteFonacot" runat="server" ControlToValidate="txtNoClienteFonacot" ValidationGroup="vNoClienteFonacot" CssClass="item" ForeColor="Red" ErrorMessage=" Proporcione No. Cliente Fonacot" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table style="width: 100%;" border="0">
                                        <tr>
                                            <td>
                                                <telerik:RadGrid ID="creditosFonacotList" Width="100%" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" Skin="Bootstrap">
                                                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                                    </ClientSettings>
                                                    <MasterTableView DataKeyNames="id" NoMasterRecordsText="No hay registros para mostrar.">
                                                        <Columns>
                                                            <telerik:GridTemplateColumn HeaderText="Folio" DataField="id" SortExpression="id" UniqueName="id">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("id") %>' CausesValidation="false"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn DataField="no_cliente" HeaderText="No. Cliente Fonacot" UniqueName="no_cliente">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="no_credito" HeaderText="No. Crédito" UniqueName="no_credito">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="fecha_credito" HeaderText="Fecha Crédito" UniqueName="fecha_prestamo">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="monto_credito" HeaderText="Monto Crédito" DataFormatString="{0:C}" UniqueName="monto_credito" ItemStyle-HorizontalAlign="Right">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="pago_minimo" HeaderText="Pago Mínimo" DataFormatString="{0:C}" UniqueName="pago_minimo" ItemStyle-HorizontalAlign="Right">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn DataField="saldo_insoluto" HeaderText="Saldo Insoluto" DataFormatString="{0:C}" UniqueName="saldo_insoluto" ItemStyle-HorizontalAlign="Right">
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridTemplateColumn HeaderText="Vigente" UniqueName="activobit">
                                                                <ItemTemplate>
                                                                    <asp:Image ID="imgVigente" runat="server" ImageAlign="AbsMiddle" Visible="false" ImageUrl="~/images/arrow.gif" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                            </telerik:GridTemplateColumn>
                                                            <%--<telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="eliminar">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="eliminar" runat="server" CommandArgument='<% #Eval("id")%>' CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
                                                                    </ItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                                </telerik:GridTemplateColumn>--%>
                                                        </Columns>
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: right;">
                                                <telerik:RadButton ID="btnAgregaPrestamoFonacot" runat="server" CssClass="rbPrimaryButton" Text="Agregar Crédito" CausesValidation="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <asp:Panel ID="panelCreditoFonacot" runat="server" Visible="False">
                                        <table style="width: 100%;" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblNoCreditoFonacot" runat="server" CssClass="item" Font-Bold="True" Text="No. Crédito Fonacot:"></asp:Label>
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblVigenteCrditoFonacot" runat="server" CssClass="item" Font-Bold="True" Text="Crédito Vigente:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFechaCreditoFonacot" runat="server" CssClass="item" Font-Bold="True" Text="Fecha Crédito:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMontoCreditoFonacot" runat="server" CssClass="item" Font-Bold="True" Text="Monto Crédito:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPagoMinimoCreditoFonacot" runat="server" CssClass="item" Font-Bold="True" Text="Pago Mínimo:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSaldoInsolutoCreditoFonacot" runat="server" CssClass="item" Font-Bold="True" Text="Saldo Insoluto:"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <telerik:RadTextBox ID="txtNoCreditoFonacot" runat="server" ValidationGroup="vDatosCreditoFonacot"></telerik:RadTextBox>
                                                </td>
                                                <td style="text-align: center;">
                                                    <asp:CheckBox ID="chkVigenteCrditoFonacot" Text="" CssClass="item" Font-Bold="True" runat="server" />
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="calFechaCreditoFonacot" CultureInfo="Español (México)" MinDate="01/01/1800" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtMontoCreditoFonacot" runat="server" Type="Currency" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtPagoMinimoCreditoFonacot" runat="server" Type="Currency" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtSaldoInsolutoCreditoFonacot" runat="server" Type="Currency" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="valNoCreditoFonacot" runat="server" ControlToValidate="txtNoCreditoFonacot" ValidationGroup="vCreditoFonacot" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="valFechaCreditoFonacot" runat="server" ControlToValidate="calFechaCreditoFonacot" ValidationGroup="vCreditoFonacot" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="valMontoCreditoFonacot" runat="server" ControlToValidate="txtMontoCreditoFonacot" InitialValue="--Seleccione--" ValidationGroup="vCreditoFonacot" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="valPagoMinimoCreditoFonacot" runat="server" ControlToValidate="txtPagoMinimoCreditoFonacot" InitialValue="--Seleccione--" ValidationGroup="vCreditoFonacot" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="valSaldoInsolutoCreditoFonacot" runat="server" ControlToValidate="txtSaldoInsolutoCreditoFonacot" InitialValue="--Seleccione--" ValidationGroup="vCreditoFonacot" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="text-align: right;">
                                                    <telerik:RadButton ID="btnGuardarCreditoFonacot" runat="server" CssClass="rbPrimaryButton" Text="Guardar" ValidationGroup="vCreditoFonacot" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <telerik:RadButton ID="btnCancelarCreditoFonacot" runat="server" CssClass="rbPrimaryButton" Text="Cancelar" CausesValidation="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <asp:HiddenField ID="EmpresaID" runat="server" Value="0" />
                                                    <asp:HiddenField ID="CreditoFonacotID" runat="server" Value="0" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                    <br />
                </telerik:RadPageView>
                <telerik:RadPageView ID="PrestamosPersonalesView" runat="server">
                    <br />
                    <br />
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Label ID="lblPrestamosPersonales" Text="Prestamos Personales" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                        </legend>
                        <br />
                        <table style="width: 100%;" border="0">
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="prestamosPersonalesList" Width="100%" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellSpacing="0" GridLines="None" Skin="Bootstrap">
                                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                        </ClientSettings>
                                        <MasterTableView DataKeyNames="id" NoMasterRecordsText="No hay registros para mostrar.">
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="Folio" DataField="id" SortExpression="id" UniqueName="id">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("id") %>' CausesValidation="false"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="fecha_prestamo" HeaderText="Fecha Prestamo" UniqueName="fecha_prestamo">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="monto_prestamo" HeaderText="Monto Prestamo" DataFormatString="{0:C}" UniqueName="monto_prestamo" ItemStyle-HorizontalAlign="Right">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="pago_minimo" HeaderText="Pago Mínimo" DataFormatString="{0:C}" UniqueName="pago_minimo" ItemStyle-HorizontalAlign="Right">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="saldo_insoluto" HeaderText="Saldo Insoluto" DataFormatString="{0:C}" UniqueName="saldo_insoluto" ItemStyle-HorizontalAlign="Right">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Vigente" UniqueName="activobit">
                                                    <ItemTemplate>
                                                        <asp:Image ID="Image1" runat="server" ImageAlign="AbsMiddle" Visible="false" ImageUrl="~/images/arrow.gif" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </telerik:GridTemplateColumn>
                                                <%--<telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="eliminar">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="eliminar" runat="server" CommandArgument='<% #Eval("id")%>' CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                    </telerik:GridTemplateColumn>--%>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">
                                    <telerik:RadButton ID="btnAgregarPrestamoPersonal" runat="server" CssClass="rbPrimaryButton" Text="Agregar Prestamo" CausesValidation="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Panel ID="panelPrestamoPersonal" runat="server" Visible="False">
                                        <table style="width: 100%;" border="0">
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:Label ID="lblVigentePrestamo" runat="server" CssClass="item" Font-Bold="True" Text="Prestamo Vigente:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFechaPrestamo" runat="server" CssClass="item" Font-Bold="True" Text="Fecha Prestamo:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblMontoPrestamo" runat="server" CssClass="item" Font-Bold="True" Text="Monto Prestamo:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblPagoMinimo" runat="server" CssClass="item" Font-Bold="True" Text="Pago Mínimo:"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblSaldoInsoluto" runat="server" CssClass="item" Font-Bold="True" Text="Saldo Insoluto:"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: center;">
                                                    <asp:CheckBox ID="chkVigentePrestamo" Text="" CssClass="item" Font-Bold="True" runat="server" />
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="calFechaPrestamo" CultureInfo="Español (México)" MinDate="01/01/1800" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtMontoPrestamo" runat="server" Type="Currency" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtPagoMinimoPrestamo" runat="server" Type="Currency" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                                </td>
                                                <td>
                                                    <telerik:RadNumericTextBox ID="txtSaldoInsolutoPrestamo" runat="server" Type="Currency" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="valFechaPrestamo" runat="server" ControlToValidate="calFechaPrestamo" ValidationGroup="vPrestamoPersonal" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="valMontoPrestamo" runat="server" ControlToValidate="txtMontoPrestamo" InitialValue="--Seleccione--" ValidationGroup="vPrestamoPersonal" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="valPagoMinimoPrestamo" runat="server" ControlToValidate="txtPagoMinimoPrestamo" InitialValue="--Seleccione--" ValidationGroup="vPrestamoPersonal" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </td>
                                                <td>
                                                    <asp:RequiredFieldValidator ID="valSaldoInsolutoPrestamo" runat="server" ControlToValidate="txtSaldoInsolutoPrestamo" InitialValue="--Seleccione--" ValidationGroup="vPrestamoPersonal" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="text-align: right;">
                                                    <telerik:RadButton ID="btnGuardarPrestamoPersonal" runat="server" CssClass="rbPrimaryButton" Text="Guardar" ValidationGroup="vPrestamoPersonal" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <telerik:RadButton ID="btnCancelarPrestamoPersonal" runat="server" CssClass="rbPrimaryButton" Text="Cancelar" CausesValidation="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5">
                                                    <asp:HiddenField ID="PrestamoPersonalID" runat="server" Value="0" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </telerik:RadPageView>
            </telerik:RadMultiPage>
        </fieldset>
    </asp:Panel>
    <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" VisibleOnPageLoad="false" Skin="Bootstrap" VisibleStatusbar="False" Behavior="Default" Height="80px" InitialBehavior="None" Left="" Top="" Style="z-index: 8000">
        <Windows>
            <telerik:RadWindow ID="SendWindow" runat="server" VisibleOnPageLoad="false" ShowContentDuringLoad="False" Modal="True" ReloadOnShow="True" Skin="Bootstrap" VisibleStatusbar="False" Behavior="Close" BackColor="Gray" Style="display: none; z-index: 1000;" Behaviors="Close" InitialBehavior="Close">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadWindowManager ID="RadWindowManager3" runat="server" VisibleOnPageLoad="false" Skin="Bootstrap" VisibleStatusbar="False" Behaviors="Close" Left="" Top="" Style="z-index: 8000">
        <Windows>
            <telerik:RadWindow ID="MovIMSSWindow" runat="server" VisibleOnPageLoad="false" ShowContentDuringLoad="False" Modal="True" ReloadOnShow="True" Skin="Bootstrap" VisibleStatusbar="False" Behaviors="Close" BackColor="Gray" Style="display: none; z-index: 1000;">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <%-- </telerik:RadAjaxPanel>--%>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
