<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RptNominaCatorcenal.aspx.vb" Inherits="CalculoNomina.RptNominaCatorcenal" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="font-awesome-4.7.0/css/font-awesome.css" rel="stylesheet" />
    <link href="styles.css" rel="stylesheet" />

    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            //if ((arguments.get_eventTarget().indexOf("gridReporte") > -1)) {
            //    arguments.set_enableAjax(false);
            //}
        }
        function btnImprimir_Clicking(sender, eventArgs) {
            window.print();
            return false;
        }
    </script>

    <style type="text/css">
        .item {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10px;
        }

        .item-bold {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: 600;
        }

        .item-title {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 16px;
            font-weight: 600;
        }

        .item-subtitle {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: 600;
        }

        hr {
            border: 0;
            border-top: 1px solid #999;
            border-bottom: 1px solid #333;
            height: 0;
        }

        .h1-bold {
            font-family: Arial, Helvetica, sans-serif;
            font-weight: 600;
        }

        label {
            margin-bottom: 0px !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <asp:HiddenField ID="IdEmpresa" runat="server" Value="0" Visible="False" />
        <asp:HiddenField ID="IdCliente" runat="server" Value="0" Visible="False" />
        <asp:HiddenField ID="IdEjercicio" runat="server" Value="0" Visible="False" />
        <asp:HiddenField ID="IdPeriodo" runat="server" Value="0" Visible="False" />
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
            <br />
            <fieldset>
                <table style="width: 100%;" border="0">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblEmpresa" CssClass="item" Font-Size="Large" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblDireccion" CssClass="item" Font-Size="Large" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblPeriodoTitulo" CssClass="item" Font-Size="Large" Visible="false" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </fieldset>
            <fieldset>
                <legend>
                    <asp:Label ID="lblTitulo" CssClass="control-label" runat="server" />
                </legend>
                <div>
                    <table style="width: 100%;" border="0">
                        <tr style="height: 30px;">
                            <td style="width: 10%;">
                                <label class="control-label">Folio:</label>
                            </td>
                            <td style="width: 10%;">
                                <asp:Label ID="lblNoNomina" runat="server"></asp:Label>
                                <asp:Label ID="lblNoPeriodo" runat="server" Visible="false"></asp:Label>
                            </td>
                            <td style="width: 10%;">
                                <label class="control-label">Tipo:</label>
                            </td>
                            <td>
                                <asp:Label ID="lblTipoNomina" runat="server"></asp:Label>
                            </td>
                            <td style="width: 10%;">
                                <%--<label class="control-label">Dias:</label>--%>
                            </td>
                            <td>
                                <asp:Label ID="lblDias" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td style="width: 10%;">
                                <label class="control-label">Ejercicio:</label>
                            </td>
                            <td style="width: 10%;">
                                <asp:Label ID="lblEjercicio" runat="server"></asp:Label>
                            </td>
                            <td style="width: 10%;">
                                <label class="control-label">Cliente:</label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblRazonSocial" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td style="width: 10%;">
                                <label class="control-label">F. Inicial:</label>
                            </td>
                            <td style="width: 10%;">
                                <asp:Label ID="lblFechaInicial" runat="server"></asp:Label>
                            </td>
                            <td style="width: 10%;">
                                <label class="control-label">F. Final:</label>
                            </td>
                            <td style="width: 10%;">
                                <asp:Label ID="lblFechaFinal" runat="server"></asp:Label>
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </div>
            </fieldset>
            <fieldset>
                <div>
                    <h3 class="item-title">P E R C E P C I O N E S</h3>
                </div>
                <div>
                    <telerik:RadGrid ID="grdPercepciones" runat="server" AutoGenerateColumns="False" AllowPaging="True" ShowFooter="True" PageSize="20" CellSpacing="0" GridLines="None" Skin="Bootstrap">
                        <MasterTableView NoMasterRecordsText="No hay registros para mostrar.">
                            <Columns>
                                <telerik:GridBoundColumn DataField="CvoConcepto" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="Codigo" UniqueName="CvoConcepto">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TipoConcepto" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="TipoConcepto" UniqueName="TipoConcepto">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Concepto" ItemStyle-Width="60%" ItemStyle-HorizontalAlign="Left" HeaderText="Concepto" UniqueName="Concepto" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Total" ItemStyle-Width="20%" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Importe" UniqueName="Total">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div>
                    <h3 class="item-title">D E D U C C I O N E S</h3>
                </div>
                <div>
                    <telerik:RadGrid ID="grdDeducciones" runat="server" AutoGenerateColumns="False" AllowPaging="True" ShowFooter="True" PageSize="20" CellSpacing="0" GridLines="None" Skin="Bootstrap">
                        <MasterTableView NoMasterRecordsText="No hay registros para mostrar.">
                            <Columns>
                                <telerik:GridBoundColumn DataField="CvoConcepto" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="Codigo" UniqueName="CvoConcepto">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TipoConcepto" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="TipoConcepto" UniqueName="TipoConcepto">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Concepto" ItemStyle-Width="60%" ItemStyle-HorizontalAlign="Left" HeaderText="Concepto" UniqueName="Concepto" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Total" ItemStyle-Width="20%" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Importe" UniqueName="Total">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div>&nbsp;</div>
                <div>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 90%; text-align: right;" class="item-bold">
                                <asp:Label ID="lblTituloTotalNeto" Text="TOTAL:" CssClass="item" Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>&nbsp;
                            </td>
                            <td style="width: 10%; text-align: right;">
                                <asp:Label ID="lblTotalNeto" CssClass="item" Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td style="width: 90%; text-align: right;" class="item-bold">
                                <asp:Label ID="lblTituloTotalGravado" Text="TOTAL GRAVADO:" CssClass="item" Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>&nbsp;
                            </td>
                            <td style="width: 10%; text-align: right;">
                                <asp:Label ID="lblTotalGravado" CssClass="item" Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td style="width: 90%; text-align: right;" class="item-bold">
                                <asp:Label ID="lblTituloTotalExento" Text="TOTAL EXENTO:" CssClass="item" Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>&nbsp;
                            </td>
                            <td style="width: 10%; text-align: right;">
                                <asp:Label ID="lblTotalExento" CssClass="item" Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table style="width: 100%;" border="0">
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center;">
                                <h1 class="h1-bold">Listado de empledos</h1>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                    </table>
                </div>
                <div>
                    <telerik:RadListView ID="RadListView1" runat="server" RenderMode="Lightweight" DataKeyNames="NoEmpleado" AllowPaging="false">
                        <ItemTemplate>
                            <table style="width: 100%;" border="0">
                                <tr>
                                    <td colspan="3">
                                        <table style="width: 100%" border="0" cellspacing="1">
                                            <tr>
                                                <td style="width: 25%;">
                                                    <label class="item-bold">Ejercicio:</label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <label class="item-bold">No. Periodo:</label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <label class="item-bold">F. Inicial:</label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <label class="item-bold">F. Final:</label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%;">
                                                    <asp:Label ID="lblEjercicio" CssClass="item" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:Label ID="lblNoPeriodo" CssClass="item" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:Label ID="lblFechaInicial" CssClass="item" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:Label ID="lblFechaFinal" CssClass="item" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%;">
                                                    <label class="item-bold">No. Empleado:</label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <label class="item-bold">Nombre:</label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <label class="item-bold">RFC:</label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <label class="item-bold">IMSS:</label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%;">
                                                    <asp:Label ID="lblNumEmpleado" CssClass="item" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:Label ID="lblNombreEmpleado" CssClass="item" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:Label ID="lblRFC" CssClass="item" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:Label ID="lblNumImss" CssClass="item" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%;">
                                                    <label class="item-bold">Puesto:</label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <label class="item-bold">Salario Diario:</label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <label class="item-bold">Salario Diario Integrado:</label>
                                                </td>
                                                <td>
                                                    <label class="item-bold">Dias Laborados:</label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 25%;">
                                                    <asp:Label ID="lblPuesto" CssClass="item" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:Label ID="lblCuotaDiaria" CssClass="item" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <asp:Label ID="lblIntegradoImss" CssClass="item" runat="server"></asp:Label>
                                                </td>
                                                <td style="width: 25%;">
                                                    <%--<asp:Label ID="lblRegContratacion" CssClass="item" runat="server"></asp:Label>--%>
                                                    <asp:Label ID="lblDiasLaborados" CssClass="item" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">&nbsp;</td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                                <tr valign="top">
                                    <td style="width: 48%">
                                        <label class="item-title">P E R C E P C I O N E S</label>
                                        <telerik:RadGrid ID="GridPercepciones" runat="server" AutoGenerateColumns="False" AllowPaging="True" ShowFooter="true" PageSize="50" CellSpacing="0" GridLines="None" Skin="Bootstrap" OnItemDataBound="GridPercepciones_ItemDataBound">
                                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                            </ClientSettings>
                                            <MasterTableView DataKeyNames="CvoConcepto,Importe,NoEmpleado" NoMasterRecordsText="No hay registros para mostrar.">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Unidad" ItemStyle-HorizontalAlign="Left" DataType="System.Int32" HeaderText="Unidad" UniqueName="Unidad">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Concepto" ItemStyle-HorizontalAlign="Left" HeaderText="Concepto" UniqueName="Concepto">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Importe" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" HeaderText="Importe" UniqueName="Importe">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                    <td style="width: 4%">&nbsp;</td>
                                    <td style="width: 48%">
                                        <label class="item-title">D E D U C C I O N E S</label>
                                        <telerik:RadGrid ID="GridDeduciones" runat="server" AutoGenerateColumns="False" AllowPaging="True" ShowFooter="true" PageSize="50" CellSpacing="0" GridLines="None" Skin="Bootstrap" OnItemDataBound="GridDeduciones_ItemDataBound">
                                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                            </ClientSettings>
                                            <MasterTableView DataKeyNames="CvoConcepto,Importe,NoEmpleado" NoMasterRecordsText="No hay registros para mostrar.">
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="Unidad" ItemStyle-HorizontalAlign="Left" DataType="System.Int32" HeaderText="Unidad" UniqueName="Unidad">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Concepto" ItemStyle-HorizontalAlign="Left" HeaderText="Concepto" UniqueName="Concepto">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Importe" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C}" HeaderText="Importe" UniqueName="Importe">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" align="right">
                                        <asp:Label ID="lblNeto" CssClass="item" Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <hr />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <fieldset class="noRecordsFieldset">
                                <legend></legend>No se encontraron registros para mostrar.
                            </fieldset>
                        </EmptyDataTemplate>
                    </telerik:RadListView>
                </div>
                <br />
                <div>
                    <telerik:RadButton ID="btnImprimir" runat="server" Text="Imprimir" OnClientClicking="btnImprimir_Clicking" CssClass="rbPrimaryButton" AutoPostBack="False"></telerik:RadButton>
                </div>
                <br />
            </fieldset>
            <br />
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
        </telerik:RadAjaxLoadingPanel>
    </form>
</body>
</html>
