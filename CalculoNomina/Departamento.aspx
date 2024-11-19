<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Departamento.aspx.vb" Inherits="CalculoNomina.Departamento" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
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
            <h3 class="m-t-none m-b">Departamentos</h3>
            <hr class="demo-separator" />
            <br />
            <table style="width: 100%;" border="0">
                <tr>
                    <td style="width: 20%;">
                        <label class="control-label">Seleccionar Cliente</label>
                    </td>
                    <td style="width:30%;">
                        <telerik:RadComboBox ID="cmbCliente" runat="server" Filter="StartsWith" OnClientFocus="clearFilters" Width="460px" AutoPostBack="true"></telerik:RadComboBox>
                    </td>
                    <td style="width: 10%;">
                        <asp:RequiredFieldValidator ID="valCliente" runat="server" ControlToValidate="cmbCliente" ValidationGroup="vgDepartamento" InitialValue="--Seleccione--" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr style="vertical-align: top;">
                    <td style="width: 20%;">
                        <asp:Label ID="lblDepartamento" runat="server" CssClass="item" Font-Bold="True" Text="Departamento:"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <telerik:RadTextBox ID="txtDepartamento" runat="server" Width="460px" ValidationGroup="vgDepartamento"></telerik:RadTextBox>
                    </td>
                    <td style="width: 10%;">
                        <asp:RequiredFieldValidator ID="valDepartamento" runat="server" ControlToValidate="txtDepartamento" ValidationGroup="vgDepartamento" CssClass="item" ForeColor="Red" ErrorMessage=" Requerido" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td style="text-align: left;">
                        <telerik:RadButton ID="btnGuardar" runat="server" Text="Guardar" CssClass="rbPrimaryButton" CausesValidation="true" ValidationGroup="vgDepartamento"></telerik:RadButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="4">
                        <telerik:RadGrid ID="GridDepartamento" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="50"
                            CellSpacing="0" GridLines="None" Skin="Bootstrap">
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                            </ClientSettings>
                            <MasterTableView DataKeyNames="id"
                                NoMasterRecordsText="No hay registros para mostrar.">
                                <CommandItemSettings ExportToPdfText="Export to PDF" />
                                <Columns>
                                    <telerik:GridBoundColumn DataField="id" ItemStyle-HorizontalAlign="Left" HeaderText="Folio" UniqueName="folio">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Descripcion" DataField="nombre" SortExpression="nombre" UniqueName="nombre">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("nombre") %>' CausesValidation="false"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" HeaderText="Eliminar" UniqueName="TemplateColumn">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="eliminar" runat="server" CausesValidation="false"
                                                CommandArgument='<% #Eval("id")%>' CommandName="cmdDelete" BorderStyle="None"
                                                ImageUrl="~/images/action_delete.gif" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">&nbsp;</td>
                </tr>
            </table>
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
