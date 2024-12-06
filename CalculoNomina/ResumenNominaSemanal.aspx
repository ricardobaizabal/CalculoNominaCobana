<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ResumenNominaSemanal.aspx.vb" Inherits="CalculoNomina.ResumenNominaSemanal" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("GridNominas") > -1) || (arguments.get_eventTarget().indexOf("btnGenerarPDF") > -1)) {

                arguments.set_enableAjax(false);
            }
        }
        function OpenWindow(empresaid, ejercicioid, periodoid) {
            var oWnd = radopen("RptNominaSemanal.aspx?e=" + empresaid + "&ej=" + ejercicioid + "&p=" + periodoid, "wndReporte");
            oWnd.set_modal(true);
            oWnd.set_centerIfModal(true);
            oWnd.setSize(1024, 768);
            oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
            oWnd.set_autoSize(false);
            oWnd.set_title("Reporte Nómina Semanal")
            oWnd.center();
            oWnd.show();
        }
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
    <style type="text/css">
        .ruFileProgress {
            display: none !important;
        }

        .ruTimeSpeed {
            display: none !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <asp:HiddenField ID="periodoID" runat="server" Value="0" Visible="False" />
        <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
        <asp:HiddenField ID="serie" runat="server" Value="" />
        <asp:HiddenField ID="folio" runat="server" Value="0" />
        <asp:HiddenField ID="FolioUUID" runat="server" Value="" />
        <div class="ibox-content" style="border: solid 0px">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="m-t-none m-b">Resumen de nómina semanal</h1>
                    <hr class="demo-separator" />
                    <br />
                    <div class="form-horizontal">
                        <div class="form-group">
                            <table style="width: 100%; border-collapse: separate; border-spacing: 10px;">
                                <tr>
                                    <td style="width: 20%;">
                                        <label class="control-label">Seleccionar Cliente</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbCliente" runat="server" Filter="StartsWith"
                                            OnClientFocus="clearFilters" Width="500px"
                                            AutoPostBack="true"
                                            OnSelectedIndexChanged="cmbCliente_SelectedIndexChanged">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <%-- FILTRO POR FOLIO DE NOMINA --%>
                                <%--<tr >
                                    <td style="width: 20%;">
                                        <label class="control-label">Seleccionar Folio de Nómina</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbFolioNomina" runat="server" Filter="StartsWith" OnClientFocus="clearFilters" Width="300px" AutoPostBack="true">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>--%>
                                <tr>
                                    <td style="width: 20%;">
                                        <label class="control-label">Seleccionar Periodicidad</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbPeriodicidad" runat="server" AutoPostBack="true" Width="300px"></telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">
                                        <label class="control-label">Seleccionar Periodo</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbPeriodo" runat="server" AutoPostBack="true" Width="300px"></telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;"></td>
                                    <td>
                                        <telerik:RadButton ID="btnBuscarNominas" runat="server" Text="Buscar Nóminas" CssClass="rbPrimaryButton" CausesValidation="False"></telerik:RadButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12 text-right">
                    <telerik:RadButton ID="btnGenerarPDF" Enabled="false" runat="server" Text="Generar PDF" CssClass="rbPrimaryButton" CausesValidation="False"></telerik:RadButton>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <telerik:RadGrid ID="GridNominas" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" ShowFooter="False" ShowHeader="True" PageSize="15" CellSpacing="0" GridLines="None" Skin="Bootstrap" ExportSettings-ExportOnlyData="False">
                        <PagerStyle Mode="NumericPages" />
                        <ExportSettings ExportOnlyData="True" IgnorePaging="True" OpenInNewWindow="True" FileName="ResumenNomina" />
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                        </ClientSettings>
                        <MasterTableView NoMasterRecordsText="No hay registros para mostrar." CommandItemDisplay="Top">
                            <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="False" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                            <Columns>
                                <%--                                <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" HeaderText="Clave">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" Text='<%# Eval("Folio") %>' runat="server" CommandArgument='<%# Eval("Folio") %>' CommandName="cmdEdit" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Font-Underline="true" />
                                </telerik:GridTemplateColumn>--%>
                                <telerik:GridBoundColumn DataField="Clave" ItemStyle-HorizontalAlign="Left" HeaderText="CLAVE" UniqueName="Clave">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Nombre" ItemStyle-HorizontalAlign="Left" HeaderText="NOMBRE" UniqueName="Nombre">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SueldosSalarios" ItemStyle-HorizontalAlign="Right" HeaderText="SUELDOS Y SALARIOS" UniqueName="SueldosSalarios">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ISR_SUB" ItemStyle-HorizontalAlign="Right" HeaderText="ISR/SUB." UniqueName="ISR_SUB">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CuotaObrera" ItemStyle-HorizontalAlign="Right" HeaderText="CUOTA OBRERA" UniqueName="CuotaObrera">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="AmortizacionInfonavit" ItemStyle-HorizontalAlign="Right" HeaderText="AMORT. INFO." UniqueName="AmortizacionInfonavit">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Retencion" ItemStyle-HorizontalAlign="Right" HeaderText="RETEN." UniqueName="Retencion">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NetoPagar" ItemStyle-HorizontalAlign="Right" HeaderText="NETO A PAGAR" UniqueName="NetoPagar">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TotalIMSS" ItemStyle-HorizontalAlign="Right" HeaderText="TOTAL IMSS" UniqueName="TotalIMSS">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Comision" ItemStyle-HorizontalAlign="Right" HeaderText="COMISIÓN" UniqueName="Comision">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ISN" ItemStyle-HorizontalAlign="Right" HeaderText="ISN" UniqueName="ISN">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Subtotal" ItemStyle-HorizontalAlign="Right" HeaderText="SUBTOTAL" UniqueName="Subtotal">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ISR" ItemStyle-HorizontalAlign="Right" HeaderText="ISR" UniqueName="ISR">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </div>

        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
            <Windows>
                <telerik:RadWindow ID="wndReporte" runat="server">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
        <telerik:RadWindowManager ID="rwConfirmGeneraNomina" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>

    <asp:Button ID="btnConfirmarGeneraPDF" Text="" Style="display: none;" runat="server" />
</asp:Content>
