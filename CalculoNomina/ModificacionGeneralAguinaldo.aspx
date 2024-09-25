<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ModificacionGeneralAguinaldo.aspx.vb" Inherits="CalculoNomina.ModificacionGeneralAguinaldo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("grdEmpleadosAguinaldo") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
        function confirmCallbackFnEliminaEmpleado(arg) {
            if (arg) //the user clicked OK
            {
                __doPostBack("<%=btnEliminarEmpleado.UniqueID %>", "");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <asp:HiddenField ID="tiponominaId" runat="server" Value="0" Visible="False" />
        <asp:HiddenField ID="empleadoId" runat="server" Value="0" Visible="False" />
        <div class="ibox-content" style="border: solid 0px">
            <div class="row">
                <div class="form-group row">
                    <table style="width: 100%;" border="0">
                        <tr style="height: 30px;">
                            <td style="width: 10%;">
                                <label class="control-label">Ejercicio:</label>
                            </td>
                            <td style="width: 10%;">
                                <asp:Label ID="lblEjercicio" runat="server"></asp:Label>
                            </td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        <tr style="height: 30px;">
                            <td style="width: 10%;">
                                <label class="control-label">Tipo:</label>
                            </td>
                            <td style="width: 10%;">
                                <asp:Label ID="lblTipoNomina" runat="server"></asp:Label>
                            </td>
                            <td style="width: 10%;">&nbsp;</td>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                    </table>
                </div>
                <div class="form-group row">
                    <telerik:RadGrid ID="grdEmpleadosAguinaldo" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" ExportSettings-ExportOnlyData="false" ShowStatusBar="true" PageSize="20" CellSpacing="0" GridLines="None" Skin="Bootstrap" AllowFilteringByColumn="true">
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView AllowMultiColumnSorting="true" AllowFilteringByColumn="true" NoMasterRecordsText="No hay registros para mostrar." DataKeyNames="NoEmpleado, IdContrato, CuotaPeriodo, IntegradoIMSS, IdContrato">
                            <Columns>
                                <telerik:GridBoundColumn DataField="NoEmpleado" ItemStyle-HorizontalAlign="Left" HeaderText="No. Empleado" UniqueName="NoEmpleado" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Empleado" ItemStyle-HorizontalAlign="Left" HeaderText="Empleado" UniqueName="Empleado" AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TotalPercepciones" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Percepciones" UniqueName="TotalPercepciones" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TotalDucciones" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Ducciones" UniqueName="TotalDucciones" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Neto" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Neto" UniqueName="Neto" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="MetodoPago" ItemStyle-HorizontalAlign="Left" HeaderText="Método Pago" UniqueName="MetodoPago" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Banco" ItemStyle-HorizontalAlign="Left" HeaderText="Banco" UniqueName="Banco" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EstatusContrato" ItemStyle-HorizontalAlign="Left" HeaderText="Estatus" UniqueName="EstatusContrato" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="Aguinaldo" AllowFiltering="false">
                                    <HeaderTemplate>Aguinaldo</HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtAguinaldo" runat="server" MinValue="0" Value="0" Type="Currency" OnTextChanged="txtAguinaldo_TextChanged" AutoPostBack="true" Text='<%# Eval("Aguinaldo") %>' Skin="Default" Width="80px" TabIndex="0" AllowFiltering="false">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="ISPT" AllowFiltering="false">
                                    <HeaderTemplate>Impuesto (ISPT)</HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtImpuesto" runat="server" MinValue="0" Value="0" Type="Number" NumberFormat-DecimalDigits="2" OnTextChanged="txtImpuesto_TextChanged" AutoPostBack="true" Text='<%# Eval("ISPT") %>' Skin="Default" Width="80px" TabIndex="0" AllowFiltering="false">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="Eliminar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" CausesValidation="false" CommandArgument='<% #Eval("NoEmpleado")%>' CommandName="cmdDelete" BorderStyle="None" ImageUrl="~/images/action_delete.gif" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
            <div class="row text-right">
                <telerik:RadButton ID="btnRegresar" RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" Text="Regresar"></telerik:RadButton>
            </div>
        </div>
        <asp:Button ID="btnEliminarEmpleado" Text="" Style="display: none;" runat="server" />
        <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
        <telerik:RadWindowManager ID="rwConfirmEliminaEmpleado" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>

    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>
