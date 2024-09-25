<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="TimbrarFiniquito.aspx.vb" Inherits="CalculoNomina.TimbrarFiniquito" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function confirmCallbackFnTimbrarFiniquito(arg) {
            if (arg) {
                __doPostBack("<%=btnConfirmarTimbraFiniquito.UniqueID %>", "");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField id="serie" runat="server" Value=""/>
    <asp:HiddenField id="folio" runat="server" Value="0"/>
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 b-r">
            <h1 class="m-t-none m-b">Datos del Empleado</h1>
        </div>
    </div>
    <div class="form-group row">
        <div class="col-md-12">
            <table style="width: 100%;" border="0">
                <tr style="height: 30px;">
                    <td style="width: 20%;">
                        <label class="control-label">Clave Empleado:</label>
                    </td>
                    <td align="left" style="width: 80%;">
                        <asp:Label ID="lblNoEmpleado" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td style="width: 20%;">
                        <label class="control-label">Nombre Empleado:</label>
                    </td>
                    <td align="left" style="width: 20%;">
                        <asp:Label ID="lblNombreEmpleado" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td style="width: 20%;">
                        <label class="control-label">Sueldo Diario:</label>
                    </td>
                    <td align="left" style="width: 80%;">
                        <asp:Label ID="lblSueldoDiario" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td style="width: 20%;">
                        <label class="control-label">Sueldo Diario Integrado:</label>
                    </td>
                    <td align="left" style="width: 80%;">
                        <asp:Label ID="lblSueldoDiarioIntegrado" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td style="width: 20%;">
                        <label class="control-label">Fecha Ingreso:</label>
                    </td>
                    <td align="left" style="width: 80%;">
                        <asp:Label ID="lblFechaIngreso" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 30px;">
                    <td style="width: 20%;">
                        <label class="control-label">Fecha Baja:</label>
                    </td>
                    <td align="left" style="width: 80%;">
                        <asp:Label ID="lblFechaBaja" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
            </table>
        </div>
    </div>
    <%--<div class="form-group row">
        <div class="col-md-12">
            <div class="demo-container size-narrow">
                <telerik:RadProgressManager ID="RadProgressManager1" runat="server" Skin="Bootstrap" />
                <telerik:RadProgressArea HeaderText="Favor de esperar..." RenderMode="Lightweight" Skin="Bootstrap" ID="RadProgressArea1" runat="server" Width="100%" />
                <br />
            </div>
        </div>
    </div>--%>
    <div class="form-group row">
        <div class="col-md-12">
            <asp:Panel runat="server" ID="panelDatos" Visible="true">
                <div class="row">
                    <div class="col-md-6" style="border: solid 0px;">
                        <label class="control-label">P E R C E P C I O N E S</label>
                        <telerik:RadGrid ID="GridPercepciones" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="50" CellSpacing="0" GridLines="None" Skin="Bootstrap">
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                            </ClientSettings>
                            <MasterTableView DataKeyNames="CvoConcepto" NoMasterRecordsText="No hay registros para mostrar.">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Unidad" ItemStyle-HorizontalAlign="Left" HeaderText="Unidad" UniqueName="Unidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Concepto" ItemStyle-HorizontalAlign="Left" HeaderText="Concepto" UniqueName="Concepto">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Importe" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" HeaderText="Importe" UniqueName="Importe">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                    <div class="col-md-6" style="border: solid 0px;">
                        <label class="control-label">D E D U C C I O N E S</label>
                        <telerik:RadGrid ID="GridDeduciones" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="50" CellSpacing="0" GridLines="None" Skin="Bootstrap">
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                            </ClientSettings>
                            <MasterTableView DataKeyNames="CvoConcepto" NoMasterRecordsText="No hay registros para mostrar.">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Unidad" ItemStyle-HorizontalAlign="Left" HeaderText="Unidad" UniqueName="Unidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Concepto" ItemStyle-HorizontalAlign="Left" HeaderText="Concepto" UniqueName="Concepto">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Importe" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" HeaderText="Importe" UniqueName="Importe">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
                <br />
                <div class="row" style="border: solid 0px">
                    <div class="col-md-6" style="border: solid 0px;">
                        <table style="width: 100%" border="0">
                            <tr style="height: 30px;">
                                <td style="width: 25%;">
                                    <label class="control-label">Percepciones</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtPercepciones" runat="server" Enabled="false" Value="0" MinValue="0" NumberFormat-DecimalDigits="2" Type="Currency" Width="100px" AutoPostBack="false"></telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr style="height: 30px;">
                                <td style="width: 25%;">
                                    <label class="control-label">Gravado I.S.R.</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtGravadoISR" runat="server" Enabled="false" Value="0" MinValue="0" NumberFormat-DecimalDigits="2" Type="Currency" Width="100px" AutoPostBack="false"></telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr style="height: 30px;">
                                <td style="width: 25%;">
                                    <label class="control-label">Exento I.S.R.</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtExentoISR" runat="server" Enabled="false" Value="0" MinValue="0" NumberFormat-DecimalDigits="2" Type="Currency" Width="100px" AutoPostBack="false"></telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-6" style="border: solid 0px;">
                        <table style="width: 100%" border="0">
                            <tr style="height: 30px;">
                                <td style="width: 25%;">
                                    <label class="control-label">Deducciones</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtDeducciones" runat="server" Enabled="false" Value="0" MinValue="0" NumberFormat-DecimalDigits="2" Type="Currency" Width="100px" AutoPostBack="false"></telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr style="height: 30px;">
                                <td style="width: 25%;">
                                    <label class="control-label">Neto a pagar</label>
                                </td>
                                <td>
                                    <telerik:RadNumericTextBox ID="txtNetoAPagar" runat="server" Enabled="false" Value="0" MinValue="0" NumberFormat-DecimalDigits="2" Type="Currency" Width="100px" AutoPostBack="false"></telerik:RadNumericTextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
    <div class="form-group row">
        <div class="col-md-10">&nbsp;</div>
        <div class="col-md-2">
            <div class="form-group">
                <telerik:RadButton ID="btnTimbrarFiniquito" RenderMode="Lightweight" runat="server" Width="115px" Height="50px">
                    <Image IsBackgroundImage="True" ImageUrl="images/TIMBRARNOM.jpg"></Image>
                </telerik:RadButton>
            </div>
        </div>
    </div>
    <div class="form-group row">
        <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
        <telerik:RadWindowManager ID="rwConfirmTimbrarFiniquito" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
        <asp:Button ID="btnConfirmarTimbraFiniquito" Text="" Style="display: none;" runat="server" />
    </div>
</asp:Content>
