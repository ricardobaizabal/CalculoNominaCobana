<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="EditorPuesto.aspx.vb" Inherits="CalculoNomina.EditorPuesto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
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
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="col-lg-12 b-r">
            <br />
            <br />
            <h3 class="m-t-none m-b">Editar Puesto</h3>
            <hr class="demo-separator" />
            <br />
            <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
            <table style="width: 100%;" border="0">
                <tr>
                    <td style="width: 20%;">
                        <label class="control-label">Seleccionar Cliente</label>
                    </td>
                    <td style="width: 30%;">
                        <telerik:RadComboBox ID="cmbCliente" runat="server" Filter="StartsWith" OnClientFocus="clearFilters" Width="350px" AutoPostBack="true"></telerik:RadComboBox>
                        &nbsp;
                            <asp:RequiredFieldValidator ID="valCliente" runat="server" ControlToValidate="cmbCliente" ValidationGroup="vgPuesto" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr style="vertical-align: top;">
                    <td style="width: 20%;">
                        <asp:Label ID="lblPuesto" runat="server" CssClass="item" Font-Bold="True" Text="Puesto:"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <telerik:RadTextBox ID="txtPuesto" runat="server" Width="350px" ValidationGroup="vgPuesto"></telerik:RadTextBox>&nbsp;
                            <asp:RequiredFieldValidator ID="valPuesto" runat="server" ControlToValidate="txtPuesto" ValidationGroup="vgPuesto" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align: left;">
                        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" Skin="Bootstrap" CssClass="rbPrimaryButton" ValidationGroup="vgPuesto" CausesValidation="true">
                        </telerik:RadButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
            </table>
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
