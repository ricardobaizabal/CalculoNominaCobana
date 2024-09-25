<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="EditorPeriodosSemanales.aspx.vb" Inherits="CalculoNomina.EditorPeriodosSemanales" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ibox-content">
        <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
        <br />
        <h3 class="m-t-none m-b">Edición Periodo Semanal</h3>
        <hr class="demo-separator" />
        <br />
        <table style="width: 100%" border="0">
            <tr style="display: none;">
                <td style="width: 15%;">
                    <label class="control-label">No. Periodo</label>
                </td>
                <td style="width: 20%;">
                    <telerik:RadTextBox ID="txtNoPeriodo" runat="server" Width="100%"></telerik:RadTextBox>
                </td>
                <td>&nbsp;&nbsp;
                    <%--<asp:RequiredFieldValidator ID="valNoPeriodo" runat="server" ControlToValidate="txtNoPeriodo" CssClass="Text" ErrorMessage="Requerido" ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 15%;">
                    <label class="control-label">Fecha Inicial</label>
                </td>
                <td style="width: 20%;">
                    <telerik:RadDatePicker ID="calFechaInicio" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" Width="100%" runat="server"></telerik:RadDatePicker>
                </td>
                <td>&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="valFechaInicio" runat="server" ControlToValidate="calFechaInicio" CssClass="Text" ErrorMessage="Requerido" ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 15%;">
                    <label class="control-label">Fecha Final</label>
                </td>
                <td style="width: 20%;">
                    <telerik:RadDatePicker ID="calFechaFin" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" Width="100%" runat="server"></telerik:RadDatePicker>
                </td>
                <td>&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="valFechaFInal" runat="server" ControlToValidate="calFechaFin" CssClass="Text" ErrorMessage="Requerido" ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 15%;">
                    <label class="control-label">Fecha Pago</label>
                </td>
                <td style="width: 20%;">
                    <telerik:RadDatePicker ID="calfechaPago" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" Width="100%" runat="server"></telerik:RadDatePicker>
                </td>
                <td>&nbsp;&nbsp;
                    <asp:RequiredFieldValidator ID="valFechapago" runat="server" ControlToValidate="calfechaPago" CssClass="Text" ErrorMessage="Requerido" ForeColor="Red" SetFocusOnError="True" ValidationGroup="vgGuardar"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td style="text-align: right;">
                    <telerik:RadButton ID="btnSave" RenderMode="Lightweight" runat="server" Skin="Bootstrap" CssClass="rbPrimaryButton" Text="Guadar" CausesValidation="true" ValidationGroup="vgGuardar"></telerik:RadButton>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
