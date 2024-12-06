<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RptNominaMensual.aspx.vb" Inherits="CalculoNomina.RptNominaMensual" %>

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
            if ((arguments.get_eventTarget().indexOf("GridNominas") > -1) || (arguments.get_eventTarget().indexOf("btnGenerarPDF") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
        function btnImprimir_Clicking(sender, eventArgs) {
            window.print();
            return false;
        }
    </script>
    <style type="text/css">
        .item {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
        }

        .item-bold {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
            font-weight: 600;
        }

        .item-title {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 16px;
            font-weight: 600;
        }

        .item-subtitle {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
            font-weight: 600;
        }

        hr {
            border: 0;
            border-top: 1px solid #999;
            border-bottom: 1px solid #333;
            height: 0;
        }

        .RadGrid_Bootstrap .rgHeader, .RadGrid_Bootstrap .rgHeader a {
            font-weight: bold !important;
        }

        fieldset {
            border: 1px groove #ddd !important;
            padding: 0 1.4em 1.4em 1.4em !important;
            margin: 0 0 1.5em 0 !important;
            -webkit-box-shadow: 0px 0px 0px 0px #000;
            box-shadow: 0px 0px 0px 0px #000;
        }

        legend {
            font-size: 1.2em !important;
            font-weight: bold !important;
            text-align: left !important;
            width: auto;
            padding: 0 10px;
            border-bottom: none;
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
        <asp:HiddenField ID="IdPeriodicidad" runat="server" Value="0" Visible="False" />
        <asp:HiddenField ID="EspecialBool" runat="server" Value="0" Visible="False" />

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
                <div class="row">
                    <div class="col-lg-12">
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
                                <td style="width: 10%;">&nbsp;</td>
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
                </div>
                <div class="row">
                    <div class="col-lg-12 text-right">
                        <telerik:RadButton ID="btnGenerarPDF" Enabled="false" runat="server" Text="Generar PDF" CssClass="rbPrimaryButton" CausesValidation="False"></telerik:RadButton>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-lg-12">
                        <telerik:RadGrid ID="GridNominas" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" ShowFooter="True" ShowHeader="True" PageSize="15" CellSpacing="0" GridLines="None" Skin="Bootstrap" ExportSettings-ExportOnlyData="False">
                            <PagerStyle Mode="NumericPages" />
                            <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True" FileName="ResumenNomina" />
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                            </ClientSettings>
                            <MasterTableView NoMasterRecordsText="No hay registros para mostrar." CommandItemDisplay="Top">
                                <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="False" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="Clave" ItemStyle-HorizontalAlign="Left" HeaderText="CLAVE" UniqueName="Clave">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Nombre" ItemStyle-HorizontalAlign="Left" HeaderText="NOMBRE" UniqueName="Nombre">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SueldosSalarios" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" HeaderText="SUELDOS Y SALARIOS" UniqueName="SueldosSalarios" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ISR_SUB" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" HeaderText="ISR/SUB." UniqueName="ISR_SUB" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CuotaObrera" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" HeaderText="CUOTA OBRERA" UniqueName="CuotaObrera" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="AmortizacionInfonavit" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" HeaderText="AMORT. INFO." UniqueName="AmortizacionInfonavit" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Retencion" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" HeaderText="RETEN." UniqueName="Retencion" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NetoPagar" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" HeaderText="NETO A PAGAR" UniqueName="NetoPagar" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TotalIMSS" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" HeaderText="TOTAL IMSS" UniqueName="TotalIMSS" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Comision" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" HeaderText="COMISIÓN" UniqueName="Comision" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ISN" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" HeaderText="ISN" UniqueName="ISN" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Subtotal" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" HeaderText="SUBTOTAL" UniqueName="Subtotal" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ISR" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" HeaderText="ISR" UniqueName="ISR" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
            </fieldset>
            <%--<div style="display: none">
                <fieldset>
                    <legend>
                        <asp:Label ID="lblPercepciones" Text="P E R C E P C I O N E S" CssClass="item-title" runat="server"></asp:Label>
                    </legend>
                    <br />
                    <telerik:RadGrid ID="grdPercepciones" runat="server" AutoGenerateColumns="False" AllowPaging="True" ShowFooter="True" PageSize="20" CellSpacing="0" GridLines="None" Skin="Bootstrap">
                        <MasterTableView NoMasterRecordsText="No hay registros para mostrar.">
                            <Columns>
                                <telerik:GridBoundColumn DataField="CvoConcepto" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="Codigo" UniqueName="CvoConcepto">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TipoConcepto" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="TipoConcepto" UniqueName="TipoConcepto">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Concepto" ItemStyle-Width="60%" ItemStyle-HorizontalAlign="Left" HeaderText="Concepto" UniqueName="Concepto" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Total" ItemStyle-Width="20%" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" HeaderText="Total" UniqueName="Total">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <br />
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 90%; text-align: right;" class="item-bold">
                                <asp:Label runat="server" Font-Size="Large" Text="TOTAL GRAVADO:" CssClass="item-bold"></asp:Label>
                            </td>
                            <td style="width: 10%; text-align: right;">
                                <asp:Label ID="lblTotalGravado" Font-Size="Large" CssClass="item-bold" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 90%; text-align: right;" class="item-bold">
                                <asp:Label runat="server" Font-Size="Large" Text="TOTAL EXENTO:" CssClass="item-bold"></asp:Label>
                            </td>
                            <td style="width: 10%; text-align: right;">
                                <asp:Label ID="lblTotalExento" Font-Size="Large" CssClass="item-bold" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                </fieldset>
                <fieldset>
                    <legend>
                        <asp:Label ID="lblDeducciones" Text="D E D U C C I O N E S" CssClass="item-title" runat="server"></asp:Label>
                    </legend>
                    <br />
                    <telerik:RadGrid ID="grdDeducciones" runat="server" AutoGenerateColumns="False" AllowPaging="True" ShowFooter="True" PageSize="20" CellSpacing="0" GridLines="None" Skin="Bootstrap">
                        <MasterTableView NoMasterRecordsText="No hay registros para mostrar.">
                            <Columns>
                                <telerik:GridBoundColumn DataField="CvoConcepto" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="Codigo" UniqueName="CvoConcepto">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TipoConcepto" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="TipoConcepto" UniqueName="TipoConcepto">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Concepto" ItemStyle-Width="60%" ItemStyle-HorizontalAlign="Left" HeaderText="Concepto" UniqueName="Concepto" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Total" ItemStyle-Width="20%" DataFormatString="{0:N}" ItemStyle-HorizontalAlign="Right" HeaderText="Total" UniqueName="Total">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </fieldset>
                <div>&nbsp;</div>
                <div align="right">
                    <asp:Label ID="lblTotalNeto" CssClass="item" Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>
                </div>
            </div>--%>
            <%--<fieldset>
                <br />
                <div>
                    <telerik:RadGrid ID="gridReporte" runat="server" AutoGenerateColumns="False" ShowHeader="true" AllowPaging="false" ShowFooter="true" CellSpacing="0" GridLines="None" Skin="Bootstrap">
                        <PagerStyle Mode="NumericPages" />
                        <ExportSettings IgnorePaging="True" FileName="ReporteNominaSemanalOrdinaria">
                            <Excel Format="Biff" />
                        </ExportSettings>
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                        </ClientSettings>
                        <MasterTableView NoMasterRecordsText="No hay registros para mostrar." CommandItemDisplay="Top">
                            <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="False" ShowExportToExcelButton="true" ShowExportToPdfButton="False" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn DataField="NoEmpleado" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="No. Empleado" UniqueName="NoEmpleado">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Empleado" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="Empleado" UniqueName="Empleado">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="RFC" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="RFC" UniqueName="RFC">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NoIMSS" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="IMSS" UniqueName="NoIMSS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PuestoNombre" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="Puesto" UniqueName="PuestoNombre">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IntegradoIMSS" DataFormatString="{0:N}" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" HeaderText="Integrado IMSS" UniqueName="IntegradoIMSS" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TotalPercepciones" DataFormatString="{0:N}" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" HeaderText="Percepciones" UniqueName="TotalPercepciones" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TotalDucciones" DataFormatString="{0:N}" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" HeaderText="Deducciones" UniqueName="TotalDucciones" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Neto" DataFormatString="{0:N}" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Right" HeaderText="Total a pagar" UniqueName="Neto" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <br />
                <table style="width: 100%;" border="0">
                    <tr>
                        <td style="width: 25%;">
                            <asp:Label CssClass="item-bold" Font-Size="Large" Font-Bold="true" runat="server">Percepciones totales:</asp:Label>
                        </td>
                        <td style="width: 25%;">
                            <asp:Label CssClass="item" Font-Size="Large" Font-Bold="true" runat="server">Deducciones totales:</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 25%;">
                            <asp:Label ID="lblPercepcionesTotales" CssClass="item" Font-Size="Large" Font-Bold="true" runat="server">$0.00</asp:Label>
                        </td>
                        <td style="width: 25%;">
                            <asp:Label ID="lblDeduccionesTotales" CssClass="item" Font-Size="Large" Font-Bold="true" runat="server">$0.00</asp:Label>
                        </td>
                    </tr>
                </table>
                <div>&nbsp;</div>
                <div style="display: none">
                    <telerik:RadListView ID="RadListView1" runat="server" RenderMode="Lightweight" DataKeyNames="NoEmpleado" AllowPaging="false" Visible="true">
                        <ItemTemplate>
                            <fieldset>
                                <legend>
                                    <asp:Label ID="lblNombreEmpleado" CssClass="item-title" runat="server"></asp:Label>
                                </legend>
                                <table style="width: 100%;" border="0">
                                    <tr>
                                        <td colspan="3">
                                            <table style="width: 100%" border="0" cellspacing="1">
                                                <tr>
                                                    <td colspan="4">&nbsp;</td>
                                                </tr>
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
                                                        <label class="item-bold">RFC:</label>
                                                    </td>
                                                    <td style="width: 25%;">
                                                        <label class="item-bold">IMSS:</label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%;">
                                                        <asp:Label ID="lblNumEmpleado" CssClass="item" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 25%;">
                                                        <asp:Label ID="lblRFC" CssClass="item" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="width: 25%;">
                                                        <asp:Label ID="lblNumImss" CssClass="item" runat="server"></asp:Label>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%;">
                                                        <label class="item-bold">Puesto:</label>
                                                    </td>
                                                    <td style="width: 25%;">
                                                        <label class="item-bold">Cuota Diaria:</label>
                                                    </td>
                                                    <td style="width: 25%;">
                                                        <label class="item-bold">Integrado IMSS:</label>
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
                                                        <asp:Label ID="lblDiasLaborados" CssClass="item" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
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
                                                        <telerik:GridBoundColumn DataField="Importe" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" HeaderText="Importe" UniqueName="Importe">
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
                                                        <telerik:GridBoundColumn DataField="Importe" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N}" HeaderText="Importe" UniqueName="Importe">
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
                                </table>
                            </fieldset>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <fieldset class="noRecordsFieldset">
                                <legend></legend>No se encontraron registros para mostrar.
                            </fieldset>
                        </EmptyDataTemplate>
                    </telerik:RadListView>
                </div>
                <div>&nbsp;</div>
                <div style="text-align: right;">
                    <telerik:RadButton ID="btnImprimir" runat="server" Text="Imprimir" OnClientClicking="btnImprimir_Clicking" CssClass="rbPrimaryButton" AutoPostBack="False"></telerik:RadButton>
                </div>
            </fieldset>--%>
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
        </telerik:RadAjaxLoadingPanel>
    </form>
</body>
</html>