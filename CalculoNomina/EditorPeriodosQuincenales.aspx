<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="EditorPeriodosQuincenales.aspx.vb" Inherits="CalculoNomina.EditorPeriodosQuincenales" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ibox-content">
        <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
        <br />
        <h3 class="m-t-none m-b">Edición Periodo Quincenal</h3>
        <hr class="demo-separator" />
        <br />
        <table style="width: 100%" border="0">
            <tr valign="top">
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
                <td>&nbsp;</td>
                <td align="right">
                    <telerik:RadButton ID="btnSave" RenderMode="Lightweight" runat="server" Skin="Bootstrap" CssClass="rbPrimaryButton" Text="Guadar" CausesValidation="true" ValidationGroup="vgGuardar"></telerik:RadButton>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>