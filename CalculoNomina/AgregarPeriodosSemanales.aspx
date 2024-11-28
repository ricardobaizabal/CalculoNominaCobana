<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="AgregarPeriodosSemanales.aspx.vb" Inherits="CalculoNomina.AgregarPeriodosSemanales" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
    <style type="text/css">
        th, td {
            padding: 10px;
        }
    </style>
    <script type="text/javascript">
        function clearFilters(sender, args) {
            if ($("#ctl00_ContentPlaceHolder1_cmbCliente_Input").val() != "") {
                var obj = jQuery.parseJSON($("#ctl00_ContentPlaceHolder1_cmbCliente_Input").val());
                if (obj.text === "") {
                    sender.set_cancel(true);
                }
            } else {
                eventArgs.set_cancel(true);
            }
        }
    </script>
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
                <td style="width: 10%;">
                    <label class="control-label">Cliente</label>
                </td>
                <td style="width: 25%;">
                    <telerik:RadComboBox ID="cmbCliente" runat="server" Filter="StartsWith" OnClientFocus="clearFilters" Width="500px" AutoPostBack="true"></telerik:RadComboBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="valCliente" runat="server" ControlToValidate="cmbCliente" ValidationGroup="vPeriodo" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 10%;">
                    <label class="control-label">Fecha inicial</label>
                </td>
                <td style="width: 25%;">
                    <telerik:RadDatePicker ID="calFechaInicio" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" Width="160px" Skin="Bootstrap" runat="server"></telerik:RadDatePicker>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="ValidarFechaInicio" runat="server" ControlToValidate="calFechaInicio" ValidationGroup="vPeriodo" CssClass="item" ErrorMessage=" Requerido" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 10%;">
                    <%--<label class="control-label">Generar periodos para el resto del año</label>--%>
                </td>
                <td style="width: 20%;">
                    <telerik:RadComboBox ID="cmbGeneraPeriodos" runat="server" Width="100%" Visible="false">
                        <Items>
                            <telerik:RadComboBoxItem Value="0" Text="--Seleccione--" />
                            <telerik:RadComboBoxItem Value="1" Text="Si" Selected="true" />
                            <telerik:RadComboBoxItem Value="2" Text="No" />
                        </Items>
                    </telerik:RadComboBox>
                </td>
                <td>&nbsp;<asp:RequiredFieldValidator ID="ValidarGeneraPeriodos" runat="server" ControlToValidate="cmbGeneraPeriodos" InitialValue="0" CssClass="Text" ErrorMessage="* Fecha requerida" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td align="right">
                    <telerik:RadButton ID="btnCancel" runat="server" Text="Cancelar" Width="90px" CssClass="rbPrimaryButton" CausesValidation="False"></telerik:RadButton>
                    &nbsp;
                    <telerik:RadButton ID="btnSave" RenderMode="Lightweight" runat="server" ValidationGroup="vPeriodo" Width="90px" Skin="Bootstrap" CssClass="rbPrimaryButton" Text="Guadar"></telerik:RadButton>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>