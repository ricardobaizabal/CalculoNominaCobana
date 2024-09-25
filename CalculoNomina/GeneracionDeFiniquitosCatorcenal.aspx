<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="GeneracionDeFiniquitosCatorcenal.aspx.vb" Inherits="CalculoNomina.GeneracionDeFiniquitosCatorcenal" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("GridPercepciones") > -1) || (arguments.get_eventTarget().indexOf("GridDeduciones") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
        function confirmCallbackFnGeneraFiniquito(arg) {
            if (arg) //the user clicked OK
            {
                __doPostBack("<%=btnConfirmarGeneraFiniquito.UniqueID %>", "");
            }
        }
        function confirmCallbackFnTimbraFiniquito(arg) {
            if (arg) //the user clicked OK
            {
                __doPostBack("<%=btnConfirmarTimbraFiniquito.UniqueID %>", "");
            }
        }
        function confirmCallbackFnEliminaConcepto(arg) {
            if (arg) //the user clicked OK
            {
                __doPostBack("<%=btnEliminarConcepto.UniqueID %>", "");
            }
        }
        function setDisplayValue(sender, args) {
            sender.set_displayValue(sender.get_value());
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="conceptoId" runat="server" Value="0" Visible="False" />
    <asp:HiddenField ID="empleadoId" runat="server" Value="0" Visible="False" />
    <asp:HiddenField ID="contratoId" runat="server" Value="0" Visible="False" />
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">--%>
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 b-r">
            <h1 class="m-t-none m-b">Datos del Finiquito</h1>
        </div>
    </div>
    <div class="form-group row">
        <div class="col-md-12">
            <div class="col-md-12">
                <table style="width: 100%;" border="0">
                    <tr style="height: 30px;">
                        <td style="width: 20%;">
                            <label class="control-label">Clave Empleado:</label>
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:Label ID="lblNoEmpleado" runat="server"></asp:Label>
                        </td>
                        <td align="right" style="width: 30%;">
                            <label class="control-label">Antiguedad en días:</label>
                        </td>
                        <td style="width: 3%;">&nbsp;</td>
                        <td align="left" style="width: 17%;">
                            <asp:Label ID="lblAntiguedadDias" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td style="width: 20%;">
                            <label class="control-label">Nombre Empleado:</label>
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:Label ID="lblNombreEmpleado" runat="server"></asp:Label>
                        </td>
                        <td align="right" style="width: 30%;">
                            <label class="control-label">Dias laborados en el año:</label>
                        </td>
                        <td style="width: 3%;">&nbsp;</td>
                        <td align="left" style="width: 17%;">
                            <asp:Label ID="lblDiasLaboradosAnio" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td style="width: 20%;">
                            <label class="control-label">Sueldo Diario:</label>
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:Label ID="lblSueldoDiario" runat="server"></asp:Label>
                        </td>
                        <td align="right" style="width: 30%;">
                            <label class="control-label">Dias de vacaciones proporcionales:</label>
                        </td>
                        <td style="width: 3%;">&nbsp;</td>
                        <td align="left" style="width: 17%;">
                            <asp:Label ID="lblDiasVacacionesProporcionales" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td style="width: 20%;">
                            <label class="control-label">Sueldo Diario Integrado:</label>
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:Label ID="lblSueldoDiarioIntegrado" runat="server"></asp:Label>
                        </td>
                        <td align="right" style="width: 30%;">
                            <label class="control-label">Vacaciones proporcionales:</label>
                        </td>
                        <td style="width: 3%;">&nbsp;</td>
                        <td align="left" style="width: 17%;">
                            <asp:Label ID="lblVacacionesProporcionales" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td style="width: 20%;">
                            <label class="control-label">Fecha Ingreso:</label>
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:Label ID="lblFechaIngreso" runat="server"></asp:Label>
                        </td>
                        <td align="right" style="width: 30%;">
                            <label class="control-label">Porcentaje de prima vacacional:</label>
                        </td>
                        <td style="width: 3%;">&nbsp;</td>
                        <td align="left" style="width: 17%;">
                            <asp:Label ID="lblPorcentajePrimaVacacional" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td style="width: 20%;">
                            <label class="control-label">Fecha Baja:</label>
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:Label ID="lblFechaBaja" runat="server"></asp:Label>
                        </td>
                        <td align="right" style="width: 30%;">
                            <label class="control-label">Prima vacacional proporcional:</label>
                        </td>
                        <td style="width: 3%;">&nbsp;</td>
                        <td align="left" style="width: 17%;">
                            <asp:Label ID="lblPrimaVacacionalProporcional" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td style="width: 20%;">
                            <label class="control-label">Dias pagados vacaciones:</label>
                        </td>
                        <td align="left" style="width: 20%;">
                            <telerik:RadNumericTextBox ID="txtDiasPagadosVacaciones" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="0" Type="Number" Width="100px" AutoPostBack="false"></telerik:RadNumericTextBox>
                        </td>
                        <td align="right" style="width: 30%;">
                            <label class="control-label">Dias de aguinaldo en el año:</label>
                        </td>
                        <td style="width: 3%;">&nbsp;</td>
                        <td align="left" style="width: 17%;">
                            <asp:Label ID="lblDiasAguinaldoAnio" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td style="width: 20%;">
                            <label class="control-label">Periodo:</label>
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:DropDownList ID="cmbPeriodo" runat="server" Width="235px"></asp:DropDownList>
                        </td>
                        <td align="right" style="width: 30%;">
                            <label class="control-label">Aguinaldo proporcional:</label>
                        </td>
                        <td style="width: 3%;">&nbsp;</td>
                        <td align="left" style="width: 17%;">
                            <asp:Label ID="lblAguinaldoProporcional" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 30px;">
                        <td style="width: 20%;">
                            <label class="control-label">Tipo Finiquito:</label>
                        </td>
                        <td align="left" style="width: 20%;">
                            <asp:DropDownList ID="cmbTipoFiniquito" runat="server" AutoPostBack="true" Width="235px">
                                <asp:ListItem Text="--Seleccione--" Value="0" />
                                <asp:ListItem Text="Renuncia Voluntaria" Value="1" />
                                <asp:ListItem Text="Liquidación" Value="2" />
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width: 30%;">&nbsp;</td>
                        <td style="width: 3%;">&nbsp;</td>
                        <td align="left" style="width: 17%;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="5">&nbsp;</td>
                    </tr>
                    <tr style="height: 30px;">
                        <td style="width: 20%;" align="left">
                            <telerik:RadButton ID="btnAceptar" RenderMode="Lightweight" Text="Aceptar" Width="100px" Skin="Bootstrap" runat="server">
                            </telerik:RadButton>
                        </td>
                        <td align="left" colspan="4">
                            <telerik:RadButton ID="btnCancelar" RenderMode="Lightweight" Text="Cancelar" Width="100px" Skin="Bootstrap" runat="server">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:Panel runat="server" ID="panelDatos" Visible="false">
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
                                <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="Eliminar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" CausesValidation="false"
                                            CommandArgument='<% #Eval("CvoConcepto")%>' CommandName="cmdDelete" BorderStyle="None"
                                            ImageUrl="~/images/action_delete.gif" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </telerik:GridTemplateColumn>
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
                                <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="Eliminar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" CausesValidation="false"
                                            CommandArgument='<% #Eval("CvoConcepto")%>' CommandName="cmdDelete" BorderStyle="None"
                                            ImageUrl="~/images/action_delete.gif" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </telerik:GridTemplateColumn>
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
            <div class="row" style="border: solid 0px">
                <div class="col-md-12" style="border: solid 0px;">
                    <table style="width: 75%" border="0">
                        <tr style="height: 40px;">
                            <td colspan="5">
                                <asp:RadioButton ID="rdoPercepcion" Text="Percepcion" GroupName="Conceptos" runat="server" AutoPostBack="true" Checked="true" />&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="rdoDeducción" Text="Deducción" GroupName="Conceptos" runat="server" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td style="width: 10%;">
                                <label class="control-label">Concepto</label>
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="cmbConcepto" AutoPostBack="true" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                            <td style="width: 15%;">
                                <telerik:RadButton ID="btnAgregar" RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" Text="Agregar Concepto"></telerik:RadButton>
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td style="width: 10%;">
                                <label class="control-label">Unidad</label>
                            </td>
                            <td style="width: 15%;">
                                <telerik:RadNumericTextBox ID="txtUnidadIncidencia" runat="server" Value="0" MinValue="0" Width="100px" AutoPostBack="true">
                                    <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                    <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                </telerik:RadNumericTextBox>
                            </td>
                            <td style="width: 10%;">
                                <label class="control-label">Importe</label>
                            </td>
                            <td style="width: 15%;">
                                <telerik:RadNumericTextBox ID="txtImporteIncidencia" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="4" Type="Currency" Width="100px" AutoPostBack="false"></telerik:RadNumericTextBox>
                            </td>
                            <td style="width: 15%;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="5">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="5">&nbsp;</td>
                        </tr>
                        <tr style="height: 30px;">
                            <td>&nbsp;</td>
                            <td style="text-align: right">
                                <telerik:RadButton ID="btnRegresar" RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" Text="Regresar"></telerik:RadButton>
                            </td>
                            <td style="text-align: right; width: 15%;">
                                <telerik:RadButton ID="btnGenerarFiniquito" RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" Text="Generar Finiquito"></telerik:RadButton>
                            </td>
                            <td style="text-align: right; width: 15%;">
                                <telerik:RadButton ID="btnDescargarPDFFiniquito" RenderMode="Lightweight" runat="server" Visible="false" Width="100%" Skin="Bootstrap" Text="Descargar Finiquito"></telerik:RadButton>
                            </td>
                            <td style="text-align: right; width: 15%;">
                                <telerik:RadButton ID="btnDescargarPDFRenuncia" RenderMode="Lightweight" runat="server" Visible="false" Width="90%" Skin="Bootstrap" Text="Descargar Renuncia"></telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </asp:Panel>
    </div>
    <asp:Button ID="btnConfirmarGeneraFiniquito" Text="" Style="display: none;" runat="server" />
    <asp:Button ID="btnConfirmarTimbraFiniquito" Text="" Style="display: none;" runat="server" />
    <asp:Button ID="btnEliminarConcepto" Text="" Style="display: none;" runat="server" />
    <%--</telerik:RadAjaxPanel>--%>

    <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rwConfirmEliminaConcepto" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rwConfirmarGeneraFiniquito" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rwConfirmarTimbraFiniquito" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
</asp:Content>
