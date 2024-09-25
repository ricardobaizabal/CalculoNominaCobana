<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MovIMSS.aspx.vb" Inherits="CalculoNomina.MovIMSS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <%--<link href="../StyleSheet1.css" rel="stylesheet" />--%>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="styles.css" rel="stylesheet" />

    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("movimientoList") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
            <Scripts>
                <%--Needed for JavaScript IntelliSense in VS2010--%>
                <%--For VS2008 replace RadScriptManager with ScriptManager--%>
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
                <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            </Scripts>
        </telerik:RadScriptManager>
        <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Label ID="Label1" Text="Agregar / Editar Movimiento del IMSS" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                </legend>
                <table border="0" width="100%">
                    <tr valign="top">
                        <td width="30%">
                            <asp:Label ID="lblTipoMovimiento" runat="server" CssClass="item" Font-Bold="True" Text="Tipo Movimiento:"></asp:Label>
                            <asp:RequiredFieldValidator ID="valTipoMovimiento" runat="server" ControlToValidate="cboMovimientoimss" ValidationGroup="vDatosGenerales" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                        <td width="70%">
                            <asp:Label ID="lblComentario" runat="server" CssClass="item" Font-Bold="True" Text="Comentario:"></asp:Label>
                            <asp:RequiredFieldValidator ID="valComentario" runat="server" ControlToValidate="txtComentario" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            <telerik:RadComboBox ID="cboMovimientoimss" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Value="0" Text="--Seleccione--" />
                                    <telerik:RadComboBoxItem Value="1" Text="Alta" />
                                    <telerik:RadComboBoxItem Value="2" Text="Modificación" />
                                    <telerik:RadComboBoxItem Value="3" Text="Baja" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="60%" rowspan="3" style="vertical-align: top;">
                            <asp:TextBox ID="txtComentario" runat="server" TextMode="MultiLine" Wrap="true" Rows="3" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td width="30%">
                            <asp:Label ID="lblFechaMovimiento" runat="server" CssClass="item" Font-Bold="True" Text="Fecha Movimiento:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="30%">
                            <telerik:RadDatePicker ID="calFechaMovimiento" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="valFechaMovimiento" runat="server" ControlToValidate="calFechaMovimiento" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td width="30%">
                            <asp:Label ID="lblSalarioDiario" runat="server" CssClass="item" Font-Bold="True" Text="Salario Diario:"></asp:Label>
                        </td>
                        <td width="70%">
                            <asp:Label ID="lblSalarioDiarioIntegrado" runat="server" CssClass="item" Font-Bold="True" Text="Salario Diario Integrado:"></asp:Label>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td width="30%">
                            <telerik:RadNumericTextBox ID="txtSalario" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="5" Width="100px" AutoPostBack="true"></telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="valSalario" runat="server" ControlToValidate="txtSalario" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                        <td width="70%">
                            <telerik:RadNumericTextBox ID="txtSalarioDiarioIntegrado" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="5" Width="100px" Enabled="false"></telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="valSalarioDiarioIntegrado" runat="server" ControlToValidate="txtSalarioDiarioIntegrado" ValidationGroup="vDatosGenerales" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left">
                            <telerik:RadButton ID="btnGuadar" RenderMode="Lightweight" runat="server" ValidationGroup="vDatosGenerales" Skin="Bootstrap" CssClass="rbPrimaryButton" Text="Guardar"></telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Label ID="Label6" Text="Movimiento del IMSS" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                </legend>
                <telerik:RadGrid ID="movimientoList" runat="server" AllowPaging="False"
                    AutoGenerateColumns="False" GridLines="None" AllowSorting="true"
                    ShowStatusBar="True" Width="100%">
                    <PagerStyle Mode="NextPrevAndNumeric" />
                    <MasterTableView AllowMultiColumnSorting="true" AllowSorting="true" DataKeyNames="id" Name="Contratos" Width="100%">
                        <Columns>
                            <telerik:GridBoundColumn DataField="id" HeaderText="Folio" UniqueName="id"></telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="fecha_mov" HeaderText="Fecha Movimiento" UniqueName="fecha_mov" DataFormatString="{0:dd/MM/yyyy}">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="tipomovimiento" HeaderText="Tipo Movimiento" UniqueName="tipomovimiento">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="solicita" HeaderText="Solicita" UniqueName="solicita">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="empleado" HeaderText="Personal" UniqueName="empleado">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="estatus" HeaderText="Estatus" UniqueName="estatus">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="salario" HeaderText="Salario" UniqueName="salario" DataFormatString="{0:C}">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="fecha_registro" HeaderText="Fecha Pedido" UniqueName="fecha_registro" DataFormatString="{0:dd/MM/yyyy}">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="fecha_proceso" HeaderText="Fecha Proceso" UniqueName="fecha_proceso">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="fecha_cierre" HeaderText="Fecha Cierre" UniqueName="fecha_cierre">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="comentario" HeaderText="Comentario" UniqueName="comentario">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn AllowFiltering="False" HeaderText="Anexo" UniqueName="Download">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDownload" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdDownload" ImageUrl="~/images/download.png" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="False" Visible="false" HeaderStyle-HorizontalAlign="Center" UniqueName="Edit">
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" ImageUrl="~/images/action_delete.gif" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn AllowFiltering="False" Visible="false" HeaderStyle-HorizontalAlign="Center" UniqueName="Delete">
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
                <br />
                <br />
                <br />
            </fieldset>
            <telerik:RadWindowManager ID="RadWindowManager2" runat="server">
            </telerik:RadWindowManager>
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
        </telerik:RadAjaxLoadingPanel>
    </form>
</body>
</html>

