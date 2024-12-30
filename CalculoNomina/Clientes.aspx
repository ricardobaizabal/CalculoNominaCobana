<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.Master" AutoEventWireup="false" Inherits="CalculoNomina.clientes" CodeBehind="Clientes.aspx.vb" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("clientslist") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
    <style type="text/css">
        .style4 {
            height: 17px;
        }

        .style5 {
            height: 14px;
        }

        .style6 {
            height: 21px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <fieldset>
            <legend style="padding-right: 6px; color: Black">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/images/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
            </legend>
            <asp:Panel runat="server" DefaultButton="btnSearch">
                <span class="item" style="font-weight: bold;">Palabra clave:</span>
                <telerik:RadTextBox ID="txtSearch" runat="server" EmptyMessage="Razón Social, RFC" Width="300px">
                </telerik:RadTextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <telerik:RadButton ID="btnSearch" RenderMode="Lightweight" runat="server" Width="100px" Skin="Bootstrap" CssClass="rbPrimaryButton" CausesValidation="false" Text="Buscar"></telerik:RadButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <telerik:RadButton ID="btnAll" RenderMode="Lightweight" runat="server" Width="100px" Skin="Bootstrap" CssClass="rbPrimaryButton" CausesValidation="false" Text="Ver todo"></telerik:RadButton>
            </asp:Panel>
        </fieldset>
        <asp:Panel ID="panelListadoClientes" runat="server" Visible="True">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Label ID="lblClientsListLegend" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                </legend>
                <table width="100%">
                    <tr>
                        <td align="right" style="height: 5px">
                            <telerik:RadButton ID="btnAddClient" RenderMode="Lightweight" runat="server" Width="130px" Skin="Bootstrap" CssClass="rbPrimaryButton" CausesValidation="false" Text="Cancelar"></telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 5px">
                            <telerik:RadGrid ID="clientslist" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" GridLines="None" AllowSorting="true"
                                PageSize="20" ShowStatusBar="True" ExportSettings-ExportOnlyData="true"
                                Skin="Bootstrap" Width="100%">
                                <PagerStyle Mode="NextPrevAndNumeric" />
                                <ExportSettings IgnorePaging="True" FileName="CatalogoClientes">
                                    <Excel Format="ExcelML" />
                                </ExportSettings>
                                <MasterTableView DataKeyNames="id" AllowMultiColumnSorting="false" AllowSorting="true" NoDetailRecordsText="No se encontraron registros." Name="Clients" Width="100%" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="False" ShowExportToExcelButton="true" ShowExportToPdfButton="False" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Editar" Exportable="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" CausesValidation="false" ImageUrl="~/images/action_edit.png" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn DataField="id" HeaderText="No. Cliente" UniqueName="id"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="razonsocial" HeaderText="Razón Social" UniqueName="razonsocial">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="registro_patronal" HeaderText="Registro patronal" UniqueName="registro_patronal">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="contacto" HeaderText="Contacto" UniqueName="contacto">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="telefono_contacto" HeaderText="Teléfono" UniqueName="telefono_contacto">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="rfc" HeaderText="RFC" UniqueName="rfc">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete" Exportable="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="false" CommandArgument='<%# Eval("id") %>' CommandName="cmdDelete" ImageUrl="~/images/action_delete.gif" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 5px">&nbsp;</td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="panelClientRegistration" runat="server" Visible="False">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Label ID="lblClientEditLegend" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                </legend>
                <br />
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Bootstrap" MultiPageID="RadMultiPage1" SelectedIndex="0" CausesValidation="False">
                    <Tabs>
                        <telerik:RadTab Text="Datos Generales" TabIndex="0" Value="0" Enabled="true" Selected="true">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Cuentas Bancarias" TabIndex="1" Value="1" Enabled="false">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Detalles" TabIndex="2" Value="2" Enabled="false">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Conceptos" TabIndex="3" Value="3" Enabled="false">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Cobros y Condiciones IMSS/ISR Cliente" TabIndex="4" Value="4" Enabled="false">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Cobros y Condiciones IMSS/ISR Empleado" TabIndex="5" Value="5" Enabled="false">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Tasa IVA" TabIndex="6" Value="6" Enabled="false">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0" Height="100%" Width="100%">
                    <telerik:RadPageView ID="RadPageView1" runat="server" Width="100%" Selected="true">
                        <br />
                        <table width="100%" border="0">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblDenominacionRazonSocial" runat="server" CssClass="item" Font-Bold="True" Text="Denominación/Razón Social:" />
                                </td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="txtDenominacionRazonSocial" runat="server" Width="92%"></telerik:RadTextBox>
                                </td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:RequiredFieldValidator ID="valDenominacionRazonSocial" runat="server" ValidationGroup="DGenerales" SetFocusOnError="true" ControlToValidate="txtDenominacionRazonSocial" ErrorMessage="Requerido" ForeColor="Red" CssClass="item"></asp:RequiredFieldValidator>
                                </td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="33%">&nbsp;</td>
                                <td width="33%">&nbsp;</td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style4" width="33%">
                                    <asp:Label ID="lblContact" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td class="style4" width="33%">
                                    <asp:Label ID="lblContactEmail" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td class="style4" width="33%">
                                    <asp:Label ID="lblContactPhone" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="33%">
                                    <telerik:RadTextBox ID="txtContact" runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                                <td class="style4" width="33%">
                                    <telerik:RadTextBox ID="txtContactEmail" runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                                <td class="style4" width="33%">
                                    <telerik:RadTextBox ID="txtContactPhone" runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style4" width="33%">&nbsp;</td>
                                <td class="style4" width="33%">
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="txtContactEmail" CssClass="item"
                                        ValidationExpression=".*@.*\..*"></asp:RegularExpressionValidator>
                                </td>
                                <td class="style4" width="33%"></td>
                            </tr>
                            <tr>
                                <td width="33%" class="style5"></td>
                                <td width="33%" class="style5"></td>
                                <td width="33%" class="style5"></td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:Label ID="lblStreet" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="33%" align="left">
                                    <asp:Label ID="lblExtNumber" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblIntNumber" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td align="left" width="33%">
                                    <asp:Label ID="lblColony" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <telerik:RadTextBox ID="txtStreet" runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="33%">
                                    <telerik:RadTextBox ID="txtExtNumber" runat="server" Width="35%">
                                    </telerik:RadTextBox>
                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <telerik:RadTextBox ID="txtIntNumber" runat="server" Width="35%">
                                </telerik:RadTextBox>
                                </td>
                                <td align="left" width="33%">
                                    <telerik:RadTextBox ID="txtColony" runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="DGenerales" SetFocusOnError="true" ControlToValidate="txtStreet" ForeColor="Red"  CssClass="item"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td width="33%">
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ValidationGroup="DGenerales" SetFocusOnError="true" ControlToValidate="txtExtNumber" ForeColor="Red" CssClass="item"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td align="left" width="33%">
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="DGenerales" SetFocusOnError="true" ControlToValidate="txtColony" ForeColor="Red" CssClass="item"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%" class="style6"></td>
                                <td width="33%" class="style6"></td>
                                <td width="33%" class="style6"></td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:Label ID="lblCountry" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="33%">
                                    <asp:Label ID="lblState" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="33%">
                                    <asp:Label ID="lblTownship" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <%--<asp:DropDownList ID="paisid" runat="server" CssClass="box" AutoPostBack="true"></asp:DropDownList>--%>
                                    <telerik:RadComboBox ID="paisid" runat="server" AutoPostBack="true" Width="85%"></telerik:RadComboBox>
                                </td>
                                <td width="33%">
                                    <%--<asp:DropDownList ID="estadoid" runat="server" CssClass="box" AutoPostBack="true"></asp:DropDownList>--%>
                                    <telerik:RadComboBox ID="estadoid" runat="server" AutoPostBack="false" Width="85%"></telerik:RadComboBox>
                                    <telerik:RadTextBox ID="txtStates" runat="server" Width="85%" Visible="false"></telerik:RadTextBox>
                                </td>
                                <td width="33%">
                                    <telerik:RadTextBox ID="txtTownship" runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ValidationGroup="DGenerales" SetFocusOnError="true" ControlToValidate="paisid" CssClass="item" ForeColor="Red"  InitialValue="0"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td width="33%">
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="DGenerales" SetFocusOnError="true" ControlToValidate="estadoid" InitialValue="0" ForeColor="Red"  CssClass="item"></asp:RequiredFieldValidator>--%>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ValidationGroup="DGenerales" SetFocusOnError="true" ControlToValidate="txtStates" CssClass="item" ForeColor="Red"  Enabled="false"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td width="33%">
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="DGenerales" SetFocusOnError="true" ControlToValidate="txtTownship" ForeColor="Red"  CssClass="item"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">&nbsp;</td>
                                <td width="33%">&nbsp;</td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:Label ID="lblZipCode" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="33%">
                                    <asp:Label ID="lblRFC" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td width="33%">
                                    <asp:Label ID="lblCondiciones" runat="server" CssClass="item" Font-Bold="true" Text="Condiciones:"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <telerik:RadTextBox ID="txtZipCode" runat="server" Width="85%" Visible="true">
                                    </telerik:RadTextBox>
                                    <%--<telerik:RadAutoCompleteBox TextSettings-SelectionMode="Single" runat="server" ID="txtZipCod"
                                        DataTextField="codigo" DataValueField="id" InputType="Text" Width="290px" DropDownWidth="150px">
                                        <TokensSettings AllowTokenEditing="true" />
                                    </telerik:RadAutoCompleteBox>--%>
                                </td>
                                <td width="33%">
                                    <telerik:RadTextBox ID="txtRFC" runat="server" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="33%">
                                    <%--<asp:DropDownList ID="condicionesid" runat="server" CssClass="box"></asp:DropDownList>--%>
                                    <telerik:RadComboBox ID="condicionesid" runat="server" Width="85%"></telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <%--<asp:RequiredFieldValidator ID="valZipCode1" runat="server" ValidationGroup="DGenerales" ControlToValidate="txtZipCode" SetFocusOnError="true" ErrorMessage="Requerido" ForeColor="Red"  CssClass="item"></asp:RequiredFieldValidator>--%>
                                    <%--<asp:RequiredFieldValidator ID="valZipCode2" runat="server" ValidationGroup="DGenerales" Enabled="false" InitialValue="" ControlToValidate="txtZipCod" SetFocusOnError="true" ErrorMessage="Requerido" CssClass="item"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="DGenerales" SetFocusOnError="true" ControlToValidate="txtRFC" ForeColor="Red" ErrorMessage="Requerido" CssClass="item"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="valRFC" CssClass="item" runat="server" ValidationGroup="DGenerales" SetFocusOnError="true" ControlToValidate="txtRFC" ForeColor="Red" ErrorMessage="Formato no válido" ValidationExpression="^([a-zA-Z&]{3,4})\d{6}([a-zA-Z\w]{3})$"></asp:RegularExpressionValidator>
                                </td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="33%" class="style6"></td>
                                <td width="33%" class="style6"></td>
                                <td width="33%" class="style6"></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblContribuyente" runat="server" Text="Tipo de contribuyente / Honorarios" CssClass="item" Font-Bold="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFormaPago" runat="server" CssClass="item" Font-Bold="true" Text="Forma de pago:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblNumCtaPago" runat="server" CssClass="item" Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <%--<asp:DropDownList ID="tipoContribuyenteid" runat="server" CssClass="box" AutoPostBack="true"></asp:DropDownList>--%>
                                    <telerik:RadComboBox ID="tipoContribuyenteid" runat="server" AutoPostBack="true" Width="85%"></telerik:RadComboBox>
                                </td>
                                <td>
                                    <%--<asp:DropDownList ID="formapagoid" runat="server" CssClass="box"></asp:DropDownList>--%>
                                    <telerik:RadComboBox ID="formapagoid" runat="server" Width="85%"></telerik:RadComboBox>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtNumCtaPago" runat="server" Width="55%">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <%--<asp:RequiredFieldValidator ID="valTipoContribuyente" runat="server" ValidationGroup="DGenerales" SetFocusOnError="true" InitialValue="0" ControlToValidate="tipoContribuyenteid" ForeColor="Red" CssClass="item"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td width="33%">&nbsp;</td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <asp:Label ID="lblRegimen" runat="server" CssClass="item" Font-Bold="true" Text="Régimen fiscal:"></asp:Label>
                                </td>
                                <td width="33%">&nbsp;
                                    <asp:Label ID="lblRegistroPatronal" runat="server" CssClass="item" Text="Registro patronal:" Font-Bold="true"></asp:Label>
                                </td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <telerik:RadComboBox ID="regimenid" runat="server" Width="85%"></telerik:RadComboBox>
                                </td>
                                <td style="width: 33%;">
                                    <telerik:RadTextBox ID="txtRegistroPatronal" runat="server" Width="55%">
                                    </telerik:RadTextBox>
                                </td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="33%">
                                    <%--<asp:RequiredFieldValidator ID="valRegimen" runat="server" ValidationGroup="DGenerales" InitialValue="0" ControlToValidate="regimenid" CssClass="item" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Requerido"></asp:RequiredFieldValidator>--%>
                                </td>
                                <td width="33%">
                                    <asp:RequiredFieldValidator ID="valRegistroPatronal" runat="server" ValidationGroup="DGenerales" ControlToValidate="txtRegistroPatronal" CssClass="item" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Requerido"></asp:RequiredFieldValidator>
                                </td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td width="33%">&nbsp;</td>
                                <td width="33%">&nbsp;</td>
                                <td width="33%">&nbsp;</td>
                            </tr>
                            <tr>
                                <td valign="bottom" colspan="3">
                                    <telerik:RadButton ID="btnSaveClient" RenderMode="Lightweight" runat="server" Width="100px" Skin="Bootstrap" CssClass="rbPrimaryButton" ValidationGroup="DGenerales" Text="Guardar"></telerik:RadButton>
                                    &nbsp;&nbsp;&nbsp;
                                    <telerik:RadButton ID="btnCancel" RenderMode="Lightweight" runat="server" Width="100px" Skin="Bootstrap" CssClass="rbPrimaryButton" CausesValidation="false" Text="Cancelar"></telerik:RadButton>
                                    &nbsp;&nbsp;&nbsp;
                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" style="width: 66%; height: 5px;">
                                    <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                                    <asp:HiddenField ID="ClientsID" runat="server" Value="0" />
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView2" runat="server">
                        <br />
                        <br />
                        <fieldset>
                            <legend style="padding-right: 6px; color: Black">
                                <asp:Label ID="Label6" runat="server" Text="Agregar / Editar Cuentas Bancarias" Font-Bold="true" CssClass="item"></asp:Label>
                            </legend>
                            <table border="0" style="width: 100%;">
                                <tr>
                                    <td colspan="7">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 30%;">
                                        <asp:Label ID="lblBanco" runat="server" CssClass="item" Font-Bold="True" Text="Banco Nacional:"></asp:Label>
                                    </td>
                                    <td style="width: 30%;">
                                        <asp:Label ID="lblBancoExtr" runat="server" CssClass="item" Font-Bold="True" Text="Banco Extranjero:"></asp:Label>
                                    </td>
                                    <td style="width: 30%;">
                                        <asp:Label ID="lblRfc1" runat="server" CssClass="item" Font-Bold="True" Text="RFC:"></asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" CssClass="item" runat="server" ValidationGroup="vDatosCuenta" ControlToValidate="txtRFCBAK" SetFocusOnError="True" ErrorMessage="Formató no válido" ValidationExpression="^([a-zA-Z&]{3,4})\d{6}([a-zA-Z\w]{3})$" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </td>
                                    <td style="width: 10%;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 30%;">
                                        <telerik:RadTextBox ID="txtBanco" runat="server" Width="95%">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td style="width: 30%;">
                                        <telerik:RadTextBox ID="txtBancoExtr" runat="server" Width="95%">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td style="width: 30%;">
                                        <telerik:RadTextBox ID="txtRFCBAK" runat="server" Width="85%">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td style="width: 10%;"></td>
                                </tr>
                                <tr>
                                    <td style="width: 30%;">&nbsp;</td>
                                    <td style="width: 30%;">&nbsp;</td>
                                    <td style="width: 30%;">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ValidationGroup="vDatosCuenta" ControlToValidate="txtRFCBAK" SetFocusOnError="True" CssClass="item" Text="Requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 30%;">
                                        <asp:Label ID="lblNonCuenta" runat="server" CssClass="item" Font-Bold="True" Text="Número de Cuenta:"></asp:Label>
                                    </td>
                                    <td style="width: 30%;">
                                        <asp:Label ID="Label7" runat="server" CssClass="item" Font-Bold="True" Text="Predeterminado:"></asp:Label>
                                    </td>
                                    <td style="width: 30%;">&nbsp;</td>
                                    <td style="width: 10%;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 30%;">
                                        <telerik:RadTextBox ID="txtCuenta" runat="server" Width="96%">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td style="width: 30%;">
                                        <asp:CheckBox runat="server" ID="chkPredeterminado" style="zoom: 1.5;" />

                                    </td>
                                    <td style="width: 30%;">&nbsp;</td>
                                    <td style="width: 10%;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 30%;">
                                        <asp:RequiredFieldValidator ID="valCuenta" runat="server" SetFocusOnError="true" ControlToValidate="txtCuenta" ValidationGroup="vDatosCuenta" Text="Requerido" ForeColor="Red" CssClass="item"></asp:RequiredFieldValidator>
                                    </td>
                                    <td style="width: 30%;">&nbsp;</td>
                                    <td style="width: 30%;">&nbsp;</td>
                                    <td style="width: 30%;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 30%;">
                                        <telerik:RadButton ID="btnGuardar" RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" CssClass="rbPrimaryButton" ValidationGroup="vDatosCuenta" Text="Guardar"></telerik:RadButton>
                                        &nbsp;&nbsp;&nbsp;
                                        <telerik:RadButton ID="btnCancelar" RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" CssClass="rbPrimaryButton" ValidationGroup="vDatosCuenta" Text="Cancelar"></telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 66%; height: 5px;">
                                        <asp:HiddenField ID="CuentaID" runat="server" Value="0" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                        <fieldset>
                            <legend style="padding-right: 6px; color: Black">
                                <asp:Label ID="lblSucursalesListLegend" runat="server" Text="Listado de Cuentas Bancarias" Font-Bold="true" CssClass="item"></asp:Label>
                            </legend>
                            <table border="0" style="width: 100%;">
                                <tr>
                                    <td style="height: 5px">
                                        <telerik:RadGrid ID="cuentasList" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" GridLines="None"
                                            PageSize="20" ShowStatusBar="True"
                                            Skin="Bootstrap" Width="100%">
                                            <PagerStyle Mode="NumericPages" />
                                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="Cuentas" NoMasterRecordsText="No se encontraron datos para mostrar" Width="100%">
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" ImageUrl="~/images/action_edit.png" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>

                                                    <telerik:GridBoundColumn DataField="id" HeaderText="Folio" UniqueName="id" Visible="false">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="banconacional" HeaderText="Banco Nacional" UniqueName="banconacional">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="bancoextranjero" HeaderText="Banco Extranjero" UniqueName="bancoextranjero">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="rfc" HeaderText="RFC" UniqueName="rfc">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="numctapago" HeaderText="Cuenta Bancaria" UniqueName="numctapago">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Predeterminado" UniqueName="">
                                                        <ItemTemplate>
                                                            <asp:Image ID="imgPredeterminado" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/images/action_check.gif" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
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
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </fieldset>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView3" runat="server">
                        <br />
                        <br />

                        <fieldset>
                            <legend style="padding-right: 6px; color: Black">
                                <asp:Label ID="Label1" runat="server" Text="Agregar / Editar Detalles Cliente" Font-Bold="true" CssClass="item"></asp:Label>
                            </legend>
                            <table border="0" width="100%">
                                <tr>
                                    <td colspan="7">&nbsp;</td>
                                </tr>
                                <!-- Encabezados de columnas -->
                                <tr>
                                    <td width="20%">
                                        <asp:Label ID="Label2" runat="server" CssClass="item" Font-Bold="True" Text="Concepto base:"></asp:Label>
                                    </td>
                                    <td width="20%">
                                        <asp:Label ID="Label3" runat="server" CssClass="item" Font-Bold="True" Text="Calculo comisión:"></asp:Label>
                                    </td>
                                    <td width="2%">
                                        <asp:Label ID="lblSpace" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                                    </td>
                                    <td width="20%">
                                        <asp:Label ID="Label4" runat="server" CssClass="item" Font-Bold="True" Text="Comisión cliente:"></asp:Label>
                                    </td>
                                    <td width="20%">
                                        <asp:Label ID="Label10" runat="server" CssClass="item" Font-Bold="True" Text="Comisión empleado:"></asp:Label>
                                    </td>
                                    <td width="20%">&nbsp;</td>
                                </tr>
                                <!-- Controles en las columnas -->
                                <tr>
                                    <td>
                                        <telerik:RadComboBox ID="concepto_base" runat="server" Width="90%" AutoPostBack="true" ValidationGroup="vDetallesCliente"></telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="calculo_comision" runat="server" Width="90%" AutoPostBack="true" ValidationGroup="vDetallesCliente"></telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtSpace" runat="server" CssClass="item" Font-Bold="True"  Width="2%"></asp:Label>
                                    </td>
                                    <td>
                                        <div style="display: flex; align-items: center;">
                                            <telerik:RadNumericTextBox ID="comision_cliente" runat="server" Value="0" MinValue="0" MaxValue="100" Width="50%" ValidationGroup="vDetallesCliente">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblPorcentajeCliente" runat="server" CssClass="item" Font-Bold="True" Text="%" Style="margin-left: 5px;"></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <div style="display: flex; align-items: center;">
                                            <telerik:RadNumericTextBox ID="comision_empleado" runat="server"  Value="0"  MinValue="0"  MaxValue="100" Width="50%" ValidationGroup="vDetallesCliente">
                                                <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                            </telerik:RadNumericTextBox>
                                            <asp:Label ID="lblPorcentajeEmpleado" runat="server" CssClass="item" Font-Bold="True" Text="%" Style="margin-left: 5px;"></asp:Label>
                                        </div>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="5">&nbsp;</td>
                                </tr>
                                <!-- Botones -->
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="btnGuardarDetalles" RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" CssClass="rbPrimaryButton" Text="Guardar" ValidationGroup="vDetallesCliente" OnClick="btnGuardarDetalles_Click"></telerik:RadButton>
                                        &nbsp;&nbsp;&nbsp;
                                        <telerik:RadButton ID="btnCancelarDetalle"  RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" CssClass="rbPrimaryButton" Text="Cancelar" ValidationGroup="vDetallesCliente"></telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" height="5px;">
                                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                    </td>
                                </tr>
                            </table>

                        </fieldset>
                        <br />
                        <fieldset>
                            <legend style="padding-right: 6px; color: Black">
                                <asp:Label ID="Label9" runat="server" Text="Listado de Detalles" Font-Bold="true" CssClass="item"></asp:Label>
                            </legend>
                            <table border="0" style="width: 100%;">
                                <tr>
                                    <td style="height: 5px">
                                        <telerik:RadGrid ID="DetallesClienteList" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" GridLines="None"
                                            PageSize="20" ShowStatusBar="True"
                                            Skin="Bootstrap" Width="100%">
                                            <PagerStyle Mode="NumericPages" />
                                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="DetallesCliente" NoMasterRecordsText="No se encontraron datos para mostrar" Width="100%">
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkEditDetalle" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEditDetalle" ImageUrl="~/images/action_edit.png" ValidationGroup="vDetallesCliente" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>

                                                    <telerik:GridBoundColumn DataField="id" HeaderText="Folio" UniqueName="id" Visible="false">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="concepto_base_descripcion" HeaderText="Concepto Base" UniqueName="banconacional">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="calculo_comision_descripcion" HeaderText="Calculo Comision" UniqueName="bancoextranjero">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="comision_cliente" HeaderText="Comision Cliente" UniqueName="rfc">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="comision_empleado" HeaderText="Comision Empleado" UniqueName="numctapago">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="DeleteDetalle">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDeleteDetalle" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDeleteDetalle" ImageUrl="~/images/action_delete.gif" ValidationGroup="vDetallesCliente" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </fieldset>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView7" runat="server">
                        <br />
                        <br />

                        <fieldset>
                            <legend style="padding-right: 6px; color: Black">
                                <asp:Label ID="LblConceptosCliente" runat="server" Text="Agregar / Editar Conceptos Cliente" Font-Bold="true" CssClass="item"></asp:Label>
                            </legend>
                            <table border="0" style="width: 100%;">
                                <tr>
                                    <td colspan="7">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td width="15%">
                                        <asp:Label ID="LblTipoConcepto" runat="server" CssClass="item" Font-Bold="True" Text="Tipo de Concepto:"></asp:Label>
                                    </td>
                                    <td width="15%">
                                        <asp:Label ID="LblCvoSAT" runat="server" CssClass="item" Font-Bold="True" Text="CvoSAT:"></asp:Label>
                                    </td>
                                    <td width="30%">
                                        <asp:Label ID="LblNombreConcepto" runat="server" CssClass="item" Font-Bold="True" Text="Nombre del Concepto:"></asp:Label>
                                    </td>
<%--                                    <td width="30%">
                                        <asp:Label ID="LblDescripcionSAT" runat="server" CssClass="item" Font-Bold="True" Text="Descripcion SAT:"></asp:Label>
                                    </td>--%>
                                </tr>
                                <tr>
<%--                                    <td width="15%">
                                        <telerik:RadComboBox ID="TipoConceptoid" runat="server" Width="60%" ValidationGroup="vDetallesCliente"></telerik:RadComboBox>
                                    </td>--%>
                                    <td style="width: 15%;">
                                        <asp:RadioButton ID="rdoPercepcion" Text="Percepcion" GroupName="ConceptosCliente" runat="server" TabIndex="1" AutoPostBack="true" Checked="true" />&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdoDeduccion" Text=" Deducción" GroupName="ConceptosCliente" runat="server" TabIndex="2" AutoPostBack="true" />
                                    </td>
                                    <td style="width: 25%;">
                                        <telerik:RadComboBox ID="cmbCvoSAT" runat="server" AutoPostBack="true" TabIndex="3" Width="70%" ValidationGroup="vDetallesCliente"></telerik:RadComboBox>
                                    </td>
                                    <td style="width: 30%;">
                                        <telerik:RadTextBox ID="txtNombreConcepto" runat="server" Width="50%" ValidationGroup="vDetallesCliente"></telerik:RadTextBox>
                                    </td>
<%--                                    <td style="width: 27%;">
                                        <telerik:RadTextBox ID="txtDescripcionSAT" runat="server" Width="80%" ValidationGroup="vDetallesCliente"></telerik:RadTextBox>
                                    </td>--%>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 45%;">&nbsp;</td>
                                    <td colspan="2" style="width: 55%;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td valign="bottom" colspan="2">
                                        <telerik:RadButton ID="btnGuardarConcepto" RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" CssClass="rbPrimaryButton" ValidationGroup="vConceptosCliente" Text="Guardar"  OnClick="btnGuardarConcepto_Click"></telerik:RadButton>
                                        &nbsp;&nbsp;&nbsp;
                                        <telerik:RadButton ID="btnCancelarConcepto" RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" CssClass="rbPrimaryButton" CausesValidation="false" Text="Cancelar"></telerik:RadButton>
                                    </td>
                                    <td colspan="2" style="width: 30%;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 66%; height: 5px;">
                                        <asp:HiddenField ID="HiddenField5" runat="server" Value="0" />
                                    </td>
                                </tr>
                            </table>

                        </fieldset>
                        <br />
                        <fieldset>
                            <legend style="padding-right: 6px; color: Black">
                                <asp:Label ID="LblListConceptos" runat="server" Text="Listado de Conceptos" Font-Bold="true" CssClass="item"></asp:Label>
                            </legend>
                            <table border="0" style="width: 100%;">
                                <tr>
                                    <td style="height: 5px">
                                        <telerik:RadGrid ID="ConceptosClienteList" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" GridLines="None"
                                            PageSize="20" ShowStatusBar="True"
                                            Skin="Bootstrap" Width="100%">
                                            <PagerStyle Mode="NumericPages" />
                                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="ConceptosCliente" NoMasterRecordsText="No se encontraron datos para mostrar" Width="100%">
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkEditConcepto" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEditConcepto" ImageUrl="~/images/action_edit.png" ValidationGroup="vConceptosCliente" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                    
                                                    <telerik:GridBoundColumn DataField="id" HeaderText="Folio" UniqueName="id" Visible="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CvoSAT" HeaderText="Cvo" UniqueName="CvoSAT">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="NombreConcepto" HeaderText="Nombre Concepto" UniqueName="NombreConcepto">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TipoConcepto" HeaderText="Tipo de Concepto" UniqueName="TipoConcepto">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="DeleteConcepto">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDeleteConcepto" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDeleteConcepto" ImageUrl="~/images/action_delete.gif" ValidationGroup="vConceptosCliente" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </fieldset>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView4" runat="server">
                        <br />
                        <br />

                        <fieldset>
                            <legend style="padding-right: 6px; color: Black">
                                <asp:Label ID="Label5" runat="server" Text="Agregar / Editar Cobros y Condiciones IMSS/ISR Cliente" Font-Bold="true" CssClass="item"></asp:Label>
                            </legend>
                            <table border="0" style="width: 100%;">
                                <tr>
                                    <td colspan="4" style="height: 10px;"></td>
                                </tr>
                                <tr>
                                    <!-- Cabeceras de las columnas -->
                                    <td style="width: 25%; text-align: left;">
                                        <asp:Label ID="Label8" runat="server" CssClass="item" Font-Bold="True" Text="Condicion IMSS:"></asp:Label>
                                    </td>
                                    <td style="width: 25%; text-align: left;">
                                        <asp:Label ID="Label11" runat="server" CssClass="item" Font-Bold="True" Text="Condicion IMSS Patronal:"></asp:Label>
                                    </td>
                                    <td style="width: 25%; text-align: left;">
                                        <asp:Label ID="Label12" runat="server" CssClass="item" Font-Bold="True" Text="Condicion ISR:"></asp:Label>
                                    </td>
                                    <td style="width: 25%; text-align: left;">
                                        <asp:Label ID="Label13" runat="server" CssClass="item" Font-Bold="True" Text="Condicion ISN:"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <!-- Primera columna -->
                                    <td style="width: 25%; vertical-align: top;">
                                        <telerik:RadComboBox ID="condicionIMSS" runat="server" Width="85%" AutoPostBack="true" ValidationGroup="vDetallesCliente"></telerik:RadComboBox>
                                        <br /><br />
                                        <asp:Label ID="Label15" runat="server" CssClass="item" Font-Bold="True" Text="Cobro IMSS:"></asp:Label><br />
                                        <telerik:RadNumericTextBox ID="CobroIMSS" runat="server" Value="0" MinValue="0" MaxValue="100" Width="30%" ValidationGroup="vDetallesCliente">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                        </telerik:RadNumericTextBox>
                                        <asp:Label ID="lblPorcentaje" runat="server" CssClass="item" Font-Bold="True" Text="%" Style="margin-left: 5px;"></asp:Label>
                                    </td>
                                    <!-- Segunda columna -->
                                    <td style="width: 25%; vertical-align: top;">
                                        <telerik:RadComboBox ID="condicionIMSSPatronal" runat="server" Width="85%" AutoPostBack="true" ValidationGroup="vDetallesCliente"></telerik:RadComboBox>
                                        <br /><br />
                                        <asp:Label ID="Label16" runat="server" CssClass="item" Font-Bold="True" Text="Cobro IMSS Patronal:"></asp:Label><br />
                                        <telerik:RadNumericTextBox ID="CobroIMSSPatronal" runat="server" Value="0" MinValue="0" MaxValue="100" Width="30%" ValidationGroup="vDetallesCliente">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                        </telerik:RadNumericTextBox>
                                        <asp:Label ID="Label36" runat="server" CssClass="item" Font-Bold="True" Text="%" Style="margin-left: 5px;"></asp:Label>
                                    </td>
                                    <!-- Tercera columna -->
                                    <td style="width: 25%; vertical-align: top;">
                                        <telerik:RadComboBox ID="CondicionISR" runat="server" Width="85%" AutoPostBack="true" ValidationGroup="vDetallesCliente"></telerik:RadComboBox>
                                        <br /><br />
                                        <asp:Label ID="Label17" runat="server" CssClass="item" Font-Bold="True" Text="Cobro ISR:"></asp:Label><br />
                                        <telerik:RadNumericTextBox ID="CobroISR" runat="server" Value="0" MinValue="0" MaxValue="100" Width="30%" ValidationGroup="vDetallesCliente">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                        </telerik:RadNumericTextBox>
                                        <asp:Label ID="Label37" runat="server" CssClass="item" Font-Bold="True" Text="%" Style="margin-left: 5px;"></asp:Label>
                                    </td>
                                    <!-- Cuarta columna -->
                                    <td style="width: 25%; vertical-align: top;">
                                        <telerik:RadComboBox ID="CondicionISN" runat="server" Width="85%" AutoPostBack="true" ValidationGroup="vDetallesCliente"></telerik:RadComboBox>
                                        <br /><br />
                                        <asp:Label ID="Label35" runat="server" CssClass="item" Font-Bold="True" Text="Cobro ISN:"></asp:Label><br />
                                        <telerik:RadNumericTextBox ID="CobroISN" runat="server" Value="0" MinValue="0" MaxValue="100" Width="30%" ValidationGroup="vDetallesCliente">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                        </telerik:RadNumericTextBox>
                                        <asp:Label ID="Label38" runat="server" CssClass="item" Font-Bold="True" Text="%" Style="margin-left: 5px;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 45%;">&nbsp;</td>
                                    <td colspan="2" style="width: 55%;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="btnGuardarIMSSISR" RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" CssClass="rbPrimaryButton" ValidationGroup="vDetallesCliente" Text="Guardar" OnClick="btnGuardarDetalles_Click"></telerik:RadButton>
                                        &nbsp;&nbsp;&nbsp;
                                        <telerik:RadButton ID="btnCancelarIMSSISR" RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" CssClass="rbPrimaryButton" CausesValidation="false" Text="Cancelar" ValidationGroup="vDetallesCliente"></telerik:RadButton>
                                    </td>
                                </tr>
                            </table>

                        </fieldset>
                        <br />
                        <fieldset>
                            <legend style="padding-right: 6px; color: Black">
                                <asp:Label ID="Label14" runat="server" Text="Listado de Cobros y Condiciones IMSS/ISR Cliente" Font-Bold="true" CssClass="item"></asp:Label>
                            </legend>
                            <table border="0" style="width: 100%;">
                                <tr>
                                    <td style="height: 5px">
                                        <telerik:RadGrid ID="ListadoIMSSISR" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" GridLines="None"
                                            PageSize="20" ShowStatusBar="True"
                                            Skin="Bootstrap" Width="100%">
                                            <PagerStyle Mode="NumericPages" />
                                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="IMSSISR" NoMasterRecordsText="No se encontraron datos para mostrar" Width="100%">
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkEditImIs" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEditDetalle" ImageUrl="~/images/action_edit.png" ValidationGroup="vDetallesCliente" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>

                                                    <telerik:GridBoundColumn DataField="id" HeaderText="Folio" UniqueName="id" Visible="false">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="condicion_imss_descripcion" HeaderText="Condicion IMSS" UniqueName="banconacional">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="cobro_imss" HeaderText="Cobro IMSS" UniqueName="bancoextranjero">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="condicion_patronal_imss_descripcion" HeaderText="Condicion Patronal IMSS" UniqueName="rfc">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="cobro_patronal_imss" HeaderText="Cobro Patronal IMSS" UniqueName="numctapago">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="condicion_isr_descripcion" HeaderText="Condicion ISR" UniqueName="numctapago">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="cobro_isr" HeaderText="Cobro ISR" UniqueName="numctapago">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="condicion_isn_descripcion" HeaderText="Condicion ISN" UniqueName="numctapago">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="DeleteDetalle">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDeleteIm_is" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDeleteDetalle" ImageUrl="~/images/action_delete.gif" ValidationGroup="vDetallesCliente" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </fieldset>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView5" runat="server">
                        <br />
                        <br />

                        <fieldset>
                            <legend style="padding-right: 6px; color: Black">
                                <asp:Label ID="Label18" runat="server" Text="Agregar / Editar Cobros y Condiciones IMSS/ISR Empleado" Font-Bold="true" CssClass="item"></asp:Label>
                            </legend>
                            <table border="0" style="width: 100%;">
                                <tr>
                                    <td colspan="7">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">
                                        <asp:Label ID="Label19" runat="server" CssClass="item" Font-Bold="True" Text="Condicion IMSS:"></asp:Label>
                                    </td>
                                    <td style="width: 20%;">
                                        <asp:Label ID="Label20" runat="server" CssClass="item" Font-Bold="True" Text="Condicion IMSS Patronal:"></asp:Label>
                                    </td>
                                    <td style="width: 20%;">
                                        <asp:Label ID="Label21" runat="server" CssClass="item" Font-Bold="True" Text="Condicion ISR:"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="30%">
                                        <telerik:RadComboBox ID="CondicionIMSSEmpleado" runat="server" Width="80%" AutoPostBack="true" ValidationGroup="vDetallesCliente"></telerik:RadComboBox>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label23" runat="server" CssClass="item" Font-Bold="True" Text="Cobro IMSS:"></asp:Label><br />
                                        <telerik:RadNumericTextBox ID="CobroIMSSEmpleado" runat="server"  Value="0"  MinValue="0"  MaxValue="100" Width="25%" ValidationGroup="">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                        </telerik:RadNumericTextBox>
                                        <asp:Label ID="Label41" runat="server" CssClass="item" Font-Bold="True" Text="%" Style="margin-left: 5px;"></asp:Label>
                                    </td>

                                    <td width="20%">
                                        <telerik:RadComboBox ID="CondicionIMSSPatronalEmpleado" runat="server" Width="80%" AutoPostBack="true" ValidationGroup="vDetallesCliente"></telerik:RadComboBox>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label24" runat="server" CssClass="item" Font-Bold="True" Text="Cobro IMSS Patronal:"></asp:Label><br />
                                        <telerik:RadNumericTextBox ID="CobroIMSSPatronalEmpleado" runat="server"  Value="0"  MinValue="0"  MaxValue="100" Width="30%" ValidationGroup="">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                        </telerik:RadNumericTextBox>
                                        <asp:Label ID="Label40" runat="server" CssClass="item" Font-Bold="True" Text="%" Style="margin-left: 5px;"></asp:Label>
                                    </td>

                                    <td style="width: 20%;">
                                        <telerik:RadComboBox ID="CondicionISREmpleado" runat="server" Width="80%" AutoPostBack="true" ValidationGroup="vDetallesCliente"></telerik:RadComboBox>
                                        <br />
                                        <br />
                                        <asp:Label ID="Label25" runat="server" CssClass="item" Font-Bold="True" Text="Cobro ISR:"></asp:Label><br />
                                        <telerik:RadNumericTextBox ID="CobroISREmpleado" runat="server"  Value="0"  MinValue="0"  MaxValue="100" Width="30%" ValidationGroup="">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                        </telerik:RadNumericTextBox>
                                        <asp:Label ID="Label39" runat="server" CssClass="item" Font-Bold="True" Text="%" Style="margin-left: 5px;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 45%;">&nbsp;</td>
                                    <td colspan="2" style="width: 55%;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td valign="bottom" colspan="2">
                                        <telerik:RadButton ID="btnGuardarIMSSISREmpleado" RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" CssClass="rbPrimaryButton" ValidationGroup="vDetallesCliente" Text="Guardar"  OnClick="btnGuardarDetalles_Click"></telerik:RadButton>
                                        &nbsp;&nbsp;&nbsp;
                                        <telerik:RadButton ID="btnCancelarIMSSISREmpleado" RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" CssClass="rbPrimaryButton" CausesValidation="false" Text="Cancelar" ValidationGroup="vDetallesCliente"></telerik:RadButton>
                                    </td>
                                    <td colspan="2" style="width: 30%;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="width: 66%; height: 5px;">
                                        <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                        <fieldset>
                            <legend style="padding-right: 6px; color: Black">
                                <asp:Label ID="Label26" runat="server" Text="Listado de Cobros y Condiciones IMSS/ISR Empleado" Font-Bold="true" CssClass="item"></asp:Label>
                            </legend>
                            <table border="0" style="width: 100%;">
                                <tr>
                                    <td style="height: 5px">
                                        <telerik:RadGrid ID="ListIMSSISREmpleado" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" GridLines="None"
                                            PageSize="20" ShowStatusBar="True"
                                            Skin="Bootstrap" Width="100%">
                                            <PagerStyle Mode="NumericPages" />
                                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="IMSSISR" NoMasterRecordsText="No se encontraron datos para mostrar" Width="100%">
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkEditImIs" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEditDetalle" ImageUrl="~/images/action_edit.png" ValidationGroup="vDetallesCliente" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>

                                                    <telerik:GridBoundColumn DataField="id" HeaderText="Folio" UniqueName="id" Visible="false">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="condicion_imss_descripcion" HeaderText="Condicion IMSS" UniqueName="banconacional">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="cobro_imss" HeaderText="Cobro IMSS" UniqueName="bancoextranjero">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="condicion_patronal_imss_descripcion" HeaderText="Condicion Patronal IMSS" UniqueName="rfc">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="cobro_patronal_imss" HeaderText="Cobro Patronal IMSS" UniqueName="numctapago">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="condicion_isr_descripcion" HeaderText="Condicion ISR" UniqueName="numctapago">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="cobro_isr" HeaderText="Cobro ISR" UniqueName="numctapago">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="DeleteDetalle">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDeleteIm_is" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDeleteDetalle" ImageUrl="~/images/action_delete.gif" ValidationGroup="vDetallesCliente" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </fieldset>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView6" runat="server">
                        <br />
                        <br />

                        <fieldset>
                            <legend style="padding-right: 6px; color: Black">
                                <asp:Label ID="Label22" runat="server" Text="Agregar / Editar Tasa IVA" Font-Bold="true" CssClass="item"></asp:Label>
                            </legend>
                            <table border="0" style="width: 100%;">
                                <tr>
                                    <td colspan="4">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="width: 25%;">
                                        <asp:Label ID="Label27" runat="server" CssClass="item" Font-Bold="True" Text="Tasa IVA Remuneración:"></asp:Label>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:Label ID="Label28" runat="server" CssClass="item" Font-Bold="True" Text="Tasa IVA Comisión:"></asp:Label>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:Label ID="Label29" runat="server" CssClass="item" Font-Bold="True" Text="Tasa IVA Cuota Obrera:"></asp:Label>
                                    </td>
                                    <td style="width: 25%;">
                                        <asp:Label ID="Label30" runat="server" CssClass="item" Font-Bold="True" Text="Tasa IVA Cuota Patronal:"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadNumericTextBox ID="TIRemuneracion" runat="server" Value="0" MinValue="0" MaxValue="100" Width="50%" ValidationGroup="vDetallesCliente">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                        </telerik:RadNumericTextBox>
                                        <asp:Label ID="Label46" runat="server" CssClass="item" Font-Bold="True" Text="%" Style="margin-left: 5px;"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="TIComision" runat="server" Value="0" MinValue="0" MaxValue="100" Width="50%" ValidationGroup="vDetallesCliente">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                        </telerik:RadNumericTextBox>
                                        <asp:Label ID="Label48" runat="server" CssClass="item" Font-Bold="True" Text="%" Style="margin-left: 5px;"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="TICuotaObrera" runat="server" Value="0" MinValue="0" MaxValue="100" Width="50%" ValidationGroup="vDetallesCliente">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                        </telerik:RadNumericTextBox>
                                        <asp:Label ID="Label47" runat="server" CssClass="item" Font-Bold="True" Text="%" Style="margin-left: 5px;"></asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadNumericTextBox ID="TICuotaPatronal" runat="server" Value="0" MinValue="0" MaxValue="100" Width="50%" ValidationGroup="vDetallesCliente">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                        </telerik:RadNumericTextBox>
                                        <asp:Label ID="Label45" runat="server" CssClass="item" Font-Bold="True" Text="%" Style="margin-left: 5px;"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <br />
                                        <asp:Label ID="Label34" runat="server" CssClass="item" Font-Bold="True" Text="Tasa IVA ISN:"></asp:Label><br />
                                        <telerik:RadNumericTextBox ID="TIISN" runat="server" Value="0" MinValue="0" MaxValue="100" Width="50%" ValidationGroup="vDetallesCliente">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                        </telerik:RadNumericTextBox>
                                        <asp:Label ID="Label44" runat="server" CssClass="item" Font-Bold="True" Text="%" Style="margin-left: 5px;"></asp:Label>
                                    </td>
                                    <td>
                                        <br />
                                        <asp:Label ID="Label31" runat="server" CssClass="item" Font-Bold="True" Text="Tasa IVA Infonavit:"></asp:Label><br />
                                        <telerik:RadNumericTextBox ID="TIInfonavit" runat="server" Value="0" MinValue="0" MaxValue="100" Width="50%" ValidationGroup="vDetallesCliente">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                        </telerik:RadNumericTextBox>
                                        <asp:Label ID="Label43" runat="server" CssClass="item" Font-Bold="True" Text="%" Style="margin-left: 5px;"></asp:Label>
                                    </td>
                                    <td>
                                        <br />
                                        <asp:Label ID="Label32" runat="server" CssClass="item" Font-Bold="True" Text="Tasa IVA ISR:"></asp:Label><br />
                                        <telerik:RadNumericTextBox ID="TIISR" runat="server" Value="0" MinValue="0" MaxValue="100" Width="50%" ValidationGroup="vDetallesCliente">
                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                        </telerik:RadNumericTextBox>
                                        <asp:Label ID="Label42" runat="server" CssClass="item" Font-Bold="True" Text="%" Style="margin-left: 5px;"></asp:Label>
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="width: 45%;">&nbsp;</td>
                                    <td colspan="2" style="width: 55%;">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="btnGuardarTasaIVA" RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" CssClass="rbPrimaryButton" ValidationGroup="vDetallesCliente" Text="Guardar" OnClick="btnGuardarDetalles_Click"></telerik:RadButton>
                                        &nbsp;&nbsp;&nbsp;
                                        <telerik:RadButton ID="btnCancelarTasaIVA" RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" CssClass="rbPrimaryButton" CausesValidation="false" Text="Cancelar" ValidationGroup="vDetallesCliente"></telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 5px;">
                                        <asp:HiddenField ID="HiddenField3" runat="server" Value="0" />
                                    </td>
                                </tr>
                            </table>

                        </fieldset>
                        <br />
                        <fieldset>
                            <legend style="padding-right: 6px; color: Black">
                                <asp:Label ID="Label33" runat="server" Text="Listado de Tasas de IVA" Font-Bold="true" CssClass="item"></asp:Label>
                            </legend>
                            <table border="0" style="width: 100%;">
                                <tr>
                                    <td style="height: 5px">
                                        <telerik:RadGrid ID="ListTasaIva" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" GridLines="None"
                                            PageSize="20" ShowStatusBar="True"
                                            Skin="Bootstrap" Width="100%">
                                            <PagerStyle Mode="NumericPages" />
                                            <MasterTableView AllowMultiColumnSorting="False" DataKeyNames="id" Name="IMSSISR" NoMasterRecordsText="No se encontraron datos para mostrar" Width="100%">
                                                <Columns>
                                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkEditImIs" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEditDetalle" ImageUrl="~/images/action_edit.png" ValidationGroup="vDetallesCliente" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>

                                                    <telerik:GridBoundColumn DataField="id" HeaderText="Folio" UniqueName="id" Visible="false">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="TIRemuneracion" HeaderText="Tasa IVA Remuneracion" UniqueName="banconacional">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TIComision" HeaderText="Tasa IVA Comision" UniqueName="bancoextranjero">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TICuotaObrera" HeaderText="Tasa IVA CuotaObrera" UniqueName="rfc">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TICuotaPatronal" HeaderText="Tasa IVA CuotaPatronal" UniqueName="numctapago">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TIISN" HeaderText="Tasa IVA ISN" UniqueName="numctapago">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TIInfonavit" HeaderText="Tasa IVA Infonavit" UniqueName="numctapago">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="TIISR" HeaderText="Tasa IVA ISR" UniqueName="numctapago">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" UniqueName="DeleteDetalle">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDeleteIm_is" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDeleteDetalle" ImageUrl="~/images/action_delete.gif" ValidationGroup="vDetallesCliente" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </fieldset>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </fieldset>
        </asp:Panel>
        <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
