<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="EditorPeriodosMensuales.aspx.vb" Inherits="CalculoNomina.EditorPeriodosMensuales" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ibox-content">
        <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
        <br />
        <h1 class="m-t-none m-b">Edición Periodo Mensual</h1>
        <hr class="demo-separator" />
        <br />
        <table style="width: 100%" border="0">
            <tr>
                <td style="width: 15%;">
                    <label class="control-label">No. Periodo</label>
                </td>
                <td style="width: 20%;">
                    <asp:Label ID="lblNoPeriodo" runat="server" CssClass="item" Text=""></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 15%;">
                    <label class="control-label">No. Periodo</label>
                </td>
                <td style="width: 20%;">
                    <asp:Label ID="lblCliente" runat="server" CssClass="item" Text=""></asp:Label>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 25%;">
                    <label class="control-label">Fecha Inicial</label>
                </td>
                <td style="width: 20%;">
                    <telerik:RadDatePicker ID="calFechaInicio" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" Width="100%" runat="server"></telerik:RadDatePicker>
                </td>
                <td>&nbsp;<asp:RequiredFieldValidator ID="valFechaInicio" runat="server" ControlToValidate="calFechaInicio" CssClass="Text" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr valign="top">
                <td style="width: 25%;">
                    <label class="control-label">Fecha Final</label>
                </td>
                <td style="width: 20%;">
                    <telerik:RadDatePicker ID="calFechaFin" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" Width="100%" runat="server"></telerik:RadDatePicker>
                </td>
                <td>&nbsp;<asp:RequiredFieldValidator ID="valFechaFInal" runat="server" ControlToValidate="calFechaFin" CssClass="Text" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr valign="top">
                <td style="width: 25%;">
                    <label class="control-label">Fecha Pago</label>
                </td>
                <td style="width: 20%;">
                    <telerik:RadDatePicker ID="calfechaPago" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" Width="100%" runat="server"></telerik:RadDatePicker>
                </td>
                <td>&nbsp;<asp:RequiredFieldValidator ID="valFechapago" runat="server" ControlToValidate="calfechaPago" CssClass="Text" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 15%;">
                    <label class="control-label">Inicio de mes</label>
                </td>
                <td style="width: 20%;">
                    <asp:CheckBox ID="chkInicioMesBit" runat="server" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 15%;">
                    <label class="control-label">Fin de mes</label>
                </td>
                <td style="width: 20%;">
                    <asp:CheckBox ID="chkFinMesBit" runat="server" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 15%;">
                    <label class="control-label">Inicio de ejercicio</label>
                </td>
                <td style="width: 20%;">
                    <asp:CheckBox ID="chkInicioEjercicioBit" runat="server" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 15%;">
                    <label class="control-label">Fin de ejercicio</label>
                </td>
                <td style="width: 20%;">
                    <asp:CheckBox ID="chkFinEjercicioBit" runat="server" />
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td align="right">
                    <telerik:RadButton ID="btnCancel" runat="server" Text="Cancelar" Width="90px" CssClass="rbPrimaryButton" CausesValidation="False"></telerik:RadButton>
                    &nbsp;
                    <telerik:RadButton ID="btnSave" RenderMode="Lightweight" runat="server" Width="90px" Skin="Bootstrap" CssClass="rbPrimaryButton" Text="Guadar"></telerik:RadButton>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
