<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Empresa.aspx.vb" Inherits="CalculoNomina.Empresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $('.input-file').change(function () {
            $('.lblLlavesPrivadas').text($('.input-file').val());
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hIdCliente" runat="server" />
    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Height="100%" Width="100%">
        <telerik:RadPageView ID="RadPageView1" runat="server" Width="100%">
            <br />
            <table border="0" style="width: 100%;">
                <tr>
                    <td style="width: 33%;">
                        <asp:Label ID="lblNombreComercial" runat="server" CssClass="item" Font-Bold="True" Text="Nombre Comercial"></asp:Label>
                    </td>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">
                        <asp:Label ID="lblContribuyente" runat="server" Text="Tipo de contribuyente:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 66%">
                        <telerik:RadTextBox ID="txtNombreComercial" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 33%;">
                        <asp:DropDownList ID="tipoContribuyenteid" runat="server" CssClass="box"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 66%">
                        <asp:RequiredFieldValidator ID="valNombreComercial" runat="server" ForeColor="Red" SetFocusOnError="true" Text="Requerido" ControlToValidate="txtNombreComercial" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 33%;">
                        <asp:RequiredFieldValidator ID="valTipoContribuyente" runat="server" InitialValue="0" ForeColor="Red" SetFocusOnError="true" Text="Requerido" ControlToValidate="tipoContribuyenteid" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <asp:Label ID="lblSocialReason" runat="server" CssClass="item" Font-Bold="True" Text="Razón Social:"></asp:Label>
                    </td>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 66%">
                        <telerik:RadTextBox ID="txtSocialReason" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 33%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <asp:RequiredFieldValidator ID="valSocialReason" runat="server" ForeColor="Red" Text="Requerido" SetFocusOnError="true" ControlToValidate="txtSocialReason" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <asp:Label ID="lblRepresentanteLegal" runat="server" CssClass="item" Font-Bold="True" Text="Representante Legal:"></asp:Label>
                    </td>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 66%">
                        <telerik:RadTextBox ID="txtRepresentanteLegal" runat="server" Width="92%">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 33%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <asp:RequiredFieldValidator ID="valRepresentanteLegal" runat="server" ForeColor="Red" Text="Requerido" SetFocusOnError="true" ControlToValidate="txtRepresentanteLegal" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <asp:Label ID="lblContact" runat="server" CssClass="item" Font-Bold="True" Text="Contacto:"></asp:Label>
                    </td>
                    <td style="width: 33%;">
                        <asp:Label ID="lblContactEmail" runat="server" CssClass="item" Font-Bold="True" Text="Email del Contacto:"></asp:Label>
                    </td>
                    <td style="width: 33%;">
                        <asp:Label ID="lblContactPhone" runat="server" CssClass="item" Font-Bold="True" Text="Teléfono del Contacto:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <telerik:RadTextBox ID="txtContact" runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 33%;">
                        <telerik:RadTextBox ID="txtContactEmail" runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 33%;">
                        <telerik:RadTextBox ID="txtContactPhone" runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ForeColor="Red" SetFocusOnError="true" ControlToValidate="txtContactEmail" CssClass="item" ValidationExpression=".*@.*\..*"></asp:RegularExpressionValidator>
                    </td>
                    <td style="width: 33%;"></td>
                </tr>
                <tr>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <asp:Label ID="lblStreet" runat="server" CssClass="item" Font-Bold="True" Text="Calle:"></asp:Label>
                    </td>
                    <td style="width: 33%;">
                        <asp:Label ID="lblExtNumber" runat="server" CssClass="item" Font-Bold="True" Text="No. Ext."></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblIntNumber" runat="server" CssClass="item" Font-Bold="True" Text="No. Int."></asp:Label>
                    </td>
                    <td style="width: 33%;">
                        <asp:Label ID="lblColony" runat="server" CssClass="item" Font-Bold="True" Text="Colonia:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <telerik:RadTextBox ID="txtStreet" runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 33%;">
                        <telerik:RadTextBox ID="txtExtNumber" runat="server" Width="35%">
                        </telerik:RadTextBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <telerik:RadTextBox ID="txtIntNumber" runat="server" Width="35%">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 33%;">
                        <telerik:RadTextBox ID="txtColony" runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <asp:RequiredFieldValidator ID="valStreet" runat="server" ForeColor="Red" SetFocusOnError="true" Text="Requerido" ControlToValidate="txtStreet" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 33%;">
                        <asp:RequiredFieldValidator ID="valExtNumber" runat="server" ForeColor="Red" SetFocusOnError="true" Text="Requerido" ControlToValidate="txtExtNumber" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 33%;">
                        <asp:RequiredFieldValidator ID="valColony" runat="server" ForeColor="Red" SetFocusOnError="true" Text="Requerido" ControlToValidate="txtColony" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <asp:Label ID="lblCountry" runat="server" CssClass="item" Font-Bold="True" Text="País:"></asp:Label>
                    </td>
                    <td style="width: 33%;">
                        <asp:Label ID="lblState" runat="server" CssClass="item" Font-Bold="True" Text="Estado:"></asp:Label>
                    </td>

                    <td style="width: 33%;">
                        <asp:Label ID="lblTownship" runat="server" CssClass="item" Font-Bold="True" Text="Municipio:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <telerik:RadTextBox ID="txtCountry" runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 33%;">
                        <asp:DropDownList ID="estadoid" runat="server" CssClass="item"></asp:DropDownList>
                    </td>

                    <td style="width: 33%;">
                        <telerik:RadTextBox ID="txtTownship" runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <asp:RequiredFieldValidator ID="valCountry" runat="server" ForeColor="Red" SetFocusOnError="true" Text="Requerido" ControlToValidate="txtCountry" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 33%;">
                        <asp:RequiredFieldValidator ID="valEstado" runat="server" ForeColor="Red" SetFocusOnError="true" Text="Requerido" ControlToValidate="estadoid" InitialValue="0" CssClass="item"></asp:RequiredFieldValidator>
                    </td>

                    <td style="width: 33%;">
                        <asp:RequiredFieldValidator ID="valTownship" runat="server" ForeColor="Red" SetFocusOnError="true" Text="Requerido" ControlToValidate="txtTownship" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <asp:Label ID="lblZipCode" runat="server" CssClass="item" Font-Bold="True" Text="Código Postal:"></asp:Label>
                    </td>
                    <td style="width: 33%;">
                        <asp:Label ID="lblRFC" runat="server" CssClass="item" Font-Bold="True" Text="R.F.C.::"></asp:Label>
                    </td>
                    <td style="width: 33%;">
                        <asp:Label ID="lblRegistroPatronal" runat="server" CssClass="item" Text="Registro patronal:" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <telerik:RadTextBox ID="txtZipCode" runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 33%;">
                        <telerik:RadTextBox ID="txtRFC" runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                    <td style="width: 33%;">
                        <telerik:RadTextBox ID="txtRegistroPatronal" runat="server" Width="55%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%;">
                        <asp:RequiredFieldValidator ID="valZipCode" runat="server" ForeColor="Red" SetFocusOnError="true" Text="Requerido" ControlToValidate="txtZipCode" CssClass="item"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 33%;">
                        <asp:RequiredFieldValidator ID="valRFCReq" runat="server" ForeColor="Red" SetFocusOnError="true" Text="Requerido" ControlToValidate="txtRFC" CssClass="item"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="valRFC" CssClass="item" runat="server" ForeColor="Red" ControlToValidate="txtRFC" SetFocusOnError="True" ValidationExpression="^([a-zA-Z]{3,4})\d{6}([a-zA-Z\w]{3})$"></asp:RegularExpressionValidator>
                    </td>
                    <td style="width: 33%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLlavePrivada" runat="server" Text="Llave Privada:" CssClass="item" Font-Bold="True"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCertificado" runat="server" CssClass="item" Text="Certificado:" Font-Bold="true"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblContrasena" runat="server" CssClass="item" Font-Bold="true" Text="Contraseña:"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 36%;">
                        <asp:FileUpload ID="Documento1" runat="server" CssClass="input-file" /><br />
                        <asp:Label ID="lblLlavesPrivadas" runat="server" CssClass="item"></asp:Label>
                    </td>
                    <td style="width: 36%;">
                        <asp:FileUpload ID="Documento2" runat="server" /><br />
                        <asp:Label ID="lblCertificados" runat="server" CssClass="item"></asp:Label>
                    </td>
                    <td style="width: 28%;">
                        <telerik:RadTextBox ID="txtContrasena" runat="server" Width="85%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">&nbsp;</td>
                    <td style="width: 33%;">&nbsp;</td>
                </tr>
                <tr>
                    <td valign="bottom" colspan="3" style="text-align: right">
                        <%--<asp:Button ID="btnSaveClient" runat="server" CssClass="rbPrimaryButton" Text="Guardar" />&nbsp;
                        <asp:Button ID="btnCancel" runat="server" CssClass="rbPrimaryButton" CausesValidation="False" Text="Cancelar" />--%>
                        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" CssClass="rbPrimaryButton" CausesValidation="True"></telerik:RadButton>
                        &nbsp;&nbsp;&nbsp;
                        <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="False"></telerik:RadButton>
                    </td>
                </tr>
            </table>
        </telerik:RadPageView>
    </telerik:RadMultiPage>
    <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Metro" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight">
    </telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
