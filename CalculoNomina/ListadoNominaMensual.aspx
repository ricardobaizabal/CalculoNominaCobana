<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ListadoNominaMensual.aspx.vb" Inherits="CalculoNomina.ListadoNominaMensual" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("GridNominas") > -1)) {
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
                    <h1 class="m-t-none m-b">Listado de nómina ordinaria mensual</h1>
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
                                        <telerik:RadComboBox ID="cmbCliente" runat="server" Filter="StartsWith" OnClientFocus="clearFilters" Width="500px" AutoPostBack="true"></telerik:RadComboBox>
                                    </td>
                                </tr>
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
                <div class="col-lg-12">
                    <div class="demo-container size-narrow">
                        <telerik:RadProgressManager ID="RadProgressManager1" runat="server" Skin="Bootstrap" />
                        <telerik:RadProgressArea HeaderText="Favor de esperar..." RenderMode="Lightweight" Skin="Bootstrap" ID="RadProgressArea1" runat="server" Width="100%" />
                        <br />
                    </div>
                </div>
                <div class="col-lg-12 text-right">
                    <telerik:RadButton ID="btnAgregarNominaE" runat="server" Text="Agregar Nómina" CssClass="rbPrimaryButton" CausesValidation="False"></telerik:RadButton>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <telerik:RadGrid ID="GridNominas" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" ShowFooter="False" ShowHeader="True" PageSize="10" CellSpacing="0" GridLines="None" ExportSettings-ExportOnlyData="False">
                        <PagerStyle Mode="NumericPages" />
                        <ExportSettings IgnorePaging="True" FileName="ReporteNominaExtraordinaria">
                            <Excel Format="Biff" />
                        </ExportSettings>
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                        </ClientSettings>
                        <MasterTableView NoMasterRecordsText="No hay registros para mostrar." CommandItemDisplay="Top">
                            <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="False" ShowExportToExcelButton="true" ShowExportToPdfButton="False" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                            <Columns>
                                <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" HeaderText="Folio">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" Text='<%# Eval("Folio") %>' runat="server" CommandArgument='<%# Eval("Folio") %>' CommandName="cmdEdit" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Font-Underline="true" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Ejercicio" ItemStyle-HorizontalAlign="Left" HeaderText="Ejercicio" UniqueName="Ejercicio">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FechaDePago" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-HorizontalAlign="Left" HeaderText="Fecha de Pago" UniqueName="FechaDePago">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NombreCliente" ItemStyle-HorizontalAlign="Left" HeaderText="Cliente" UniqueName="NombreCliente">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FechaRango" ItemStyle-HorizontalAlign="Right" HeaderText="Periodo de Pago" UniqueName="PeriodoDePago">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TipoDeNomina" ItemStyle-HorizontalAlign="Right" HeaderText="Tipo de Nomina" UniqueName="TipoDeNomina">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Fecha" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-HorizontalAlign="Right" HeaderText="Fecha" UniqueName="Fecha">
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
</asp:Content>
