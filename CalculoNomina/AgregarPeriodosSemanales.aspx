<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="AgregarPeriodosSemanales.aspx.vb" Inherits="CalculoNomina.AgregarPeriodosSemanales" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ibox-content">
        <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
        <br />
        <h3 class="m-t-none m-b">Generación de Periodos Semanales</h3>
        <hr class="demo-separator" />
        <br />
        <table style="width: 100%" border="0">
            <tr>
                <td style="width: 25%;">
                    <label class="control-label">Fecha inicial</label>
                </td>
                <td style="width: 20%;">
                    <telerik:RadDatePicker ID="calFechaInicio" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" Width="100%" Skin="Bootstrap" runat="server"></telerik:RadDatePicker>
                </td>
                <td>&nbsp;<asp:RequiredFieldValidator ID="ValidarFechaInicio" runat="server" ControlToValidate="calFechaInicio" CssClass="Text" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td style="width: 25%;">
                    <label class="control-label">Generar periodos para el resto del año</label>
                </td>
                <td style="width: 20%;">
                    <telerik:RadComboBox ID="cmbGeneraPeriodos" runat="server" Width="100%">
                        <Items>
                            <telerik:RadComboBoxItem Value="0" Text="--Seleccione--" />
                            <telerik:RadComboBoxItem Value="1" Text="Si" />
                            <telerik:RadComboBoxItem Value="2" Text="No" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td>&nbsp;<asp:RequiredFieldValidator ID="ValidarGeneraPeriodos" runat="server" ControlToValidate="cmbGeneraPeriodos" InitialValue="0" CssClass="Text" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
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
