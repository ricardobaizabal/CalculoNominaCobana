<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PTU.aspx.vb" Inherits="CalculoNomina.PTU" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>

    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="font-awesome/css/font-awesome.css" rel="stylesheet">

    <!-- Toastr style -->
    <link href="css/plugins/toastr/toastr.min.css" rel="stylesheet">

    <!-- Gritter -->
    <link href="js/plugins/gritter/jquery.gritter.css" rel="stylesheet">


    <link href="css/animate.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <link href="styles.css" rel="stylesheet" />
    <script type="text/javascript">
        function confirmCallbackFnEliminaConcepto(arg) {
            if (arg) //the user clicked OK
            {
                __doPostBack("<%=btnEliminarConcepto.UniqueID %>", "");
            }
        }
        function setDisplayValue(sender, args) {
            sender.set_displayValue(sender.get_value());
        }
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("GridPercepciones") > -1) || (arguments.get_eventTarget().indexOf("GridDeduciones") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
    <style type="text/css">
        body {
            background-color: #ffffff !important;
        }

        td {
            padding: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        
        <asp:HiddenField ID="empleadoId" runat="server" Value="0" Visible="False" />
        <asp:HiddenField ID="contratoId" runat="server" Value="0" Visible="False" />
        <asp:HiddenField ID="empresaId" runat="server" Value="0" Visible="False" />
        <asp:HiddenField ID="ejercicioId" runat="server" Value="0" Visible="False" />
        <asp:HiddenField ID="tiponominaId" runat="server" Value="0" Visible="False" />
        <asp:HiddenField ID="conceptoId" runat="server" Value="0" Visible="False" />
        
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
            <div style="border: solid 0px; margin-left: 20px;">
                <br />
                <%--<div class="row">
                    <table style="width: 100%" border="1">
                        <tr style="height: 30px;">
                            <td style="width: 15%;" align="right">
                                <label class="control-label">Ejercicio:</label>
                            </td>
                            <td style="width: 30%;">
                                <asp:Label ID="lblEjercicio" runat="server"></asp:Label>
                            </td>
                            <td style="width: 15%;" align="right">
                                <label class="control-label">No. Periodo:</label>
                            </td>
                            <td style="width: 40%;">
                                <asp:Label ID="lblNoPeriodo" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td style="width: 15%;" align="right">
                                <label class="control-label">No. Empleado:</label>
                            </td>
                            <td style="width: 30%;">
                                <asp:Label ID="lblNumEmpleado" runat="server"></asp:Label>
                            </td>
                            <td style="width: 15%;" align="right">
                                <label class="control-label">F. Inicial:</label>
                            </td>
                            <td style="width: 40%;">
                                <asp:Label ID="lblFechaInicial" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%;" align="right">
                                <label class="control-label">Nombre:</label>
                            </td>
                            <td style="width: 30%;">
                                <asp:Label ID="lblNombreEmpleado" runat="server"></asp:Label>
                            </td>
                            <td style="width: 15%;" align="right">
                                <label class="control-label">F. Final:</label>
                            </td>
                            <td style="width: 40%;">
                                <asp:Label ID="lblFechaFinal" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%;" align="right">
                                <label class="control-label">RFC:</label>
                            </td>
                            <td style="width: 30%;">
                                <asp:Label ID="lblRFC" runat="server"></asp:Label>
                            </td>
                            <td colspan="2">&nbsp;</td>               
                        </tr>
                    </table>
                </div>--%>
                <br />
                <div class="row">
                    <div class="col-md-12" style="border: solid 0px;">
                        <label class="control-label">P E R C E P C I O N E S</label>
                        <telerik:RadGrid ID="GridPercepciones" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="50" CellSpacing="0" GridLines="None" Skin="Bootstrap">
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                            </ClientSettings>
                            <MasterTableView DataKeyNames="CvoConcepto" NoMasterRecordsText="No hay registros para mostrar.">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Unidad" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="Unidad" UniqueName="Unidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Concepto" ItemStyle-HorizontalAlign="Left" HeaderText="Concepto" UniqueName="Concepto">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Importe" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" HeaderText="Importe" UniqueName="Importe">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="Eliminar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEliminar" runat="server" CausesValidation="false"
                                                CommandArgument='<% #Eval("CvoConcepto")%>' CommandName="cmdDelete" BorderStyle="None"
                                                ImageUrl="~/images/action_delete.gif" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                    <div class="col-md-12" style="border: solid 0px;">
                        <label class="control-label">D E D U C C I O N E S</label>
                        <telerik:RadGrid ID="GridDeduciones" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="50" CellSpacing="0" GridLines="None" Skin="Bootstrap">
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                            </ClientSettings>
                            <MasterTableView DataKeyNames="CvoConcepto" NoMasterRecordsText="No hay registros para mostrar.">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Unidad" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="Unidad" UniqueName="Unidad">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Concepto" ItemStyle-HorizontalAlign="Left" HeaderText="Concepto" UniqueName="Concepto">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Importe" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" HeaderText="Importe" UniqueName="Importe">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="Eliminar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEliminar" runat="server" CausesValidation="false"
                                                CommandArgument='<% #Eval("CvoConcepto")%>' CommandName="cmdDelete" BorderStyle="None"
                                                ImageUrl="~/images/action_delete.gif" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="10%" />
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
                        <table style="width: 100%" border="0">
                            <tr style="height: 40px;">
                                <td colspan="8">
                                    <asp:RadioButton ID="rdoPercepcion" Text="Percepcion" GroupName="Conceptos" runat="server" AutoPostBack="true" Checked="true" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:RadioButton ID="rdoDeducción" Text="Deducción" GroupName="Conceptos" runat="server" AutoPostBack="true" />
                                </td>
                            </tr>
                            <tr style="height: 30px;">
                                <td style="width: 10%;">
                                    <label class="control-label">Concepto</label>
                                </td>
                                <td colspan="3">
                                    <asp:DropDownList ID="cmbConcepto" AutoPostBack="true" Width="92%" runat="server"></asp:DropDownList>
                                </td>
                                <td style="width: 10%;">
                                    <telerik:RadButton ID="btnAgregar" RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" Text="Agregar Concepto"></telerik:RadButton>
                                </td>
                                <td colspan="3">&nbsp;</td>
                            </tr>
                            <tr style="height: 30px;">
                                <td style="width: 10%;">
                                    <label class="control-label">Unidad</label>
                                </td>
                                <td style="width: 15%;">
                                    <telerik:RadNumericTextBox ID="txtUnidadIncidencia" runat="server" Value="1" Enabled="false" MinValue="0" Width="100px">
                                        <NumberFormat AllowRounding="false" KeepNotRoundedValue="true" />
                                        <ClientEvents OnValueChanged="setDisplayValue" OnLoad="setDisplayValue" />
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td style="width: 10%;">
                                    <label class="control-label">Importe</label>
                                </td>
                                <td style="width: 10%;">
                                    <telerik:RadNumericTextBox ID="txtImporteIncidencia" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="4" Type="Currency" Width="100px" AutoPostBack="false"></telerik:RadNumericTextBox>
                                </td>
                                <td colspan="4">&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <asp:Button ID="btnEliminarConcepto" Text="" Style="display: none;" runat="server" />
        </telerik:RadAjaxPanel>
        <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
        <telerik:RadWindowManager ID="rwConfirmEliminaConcepto" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
        </telerik:RadAjaxLoadingPanel>
    </form>
</body>
</html>
