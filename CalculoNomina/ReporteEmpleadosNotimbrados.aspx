<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ReporteEmpleadosNotimbrados.aspx.vb" Inherits="CalculoNomina.ReporteEmpleadosNotimbrados" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("GridEmpleadosNoTimbrados") > -1)) {
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
    <br />
    <telerik:RadWindowManager ID="RadWindowManager2" runat="server">
    </telerik:RadWindowManager>

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <asp:HiddenField ID="periodoID" runat="server" Value="0" Visible="False" />
        <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
        <asp:HiddenField ID="serie" runat="server" Value="" />
        <asp:HiddenField ID="folio" runat="server" Value="0" />
        <asp:HiddenField ID="FolioUUID" runat="server" Value="" />
        <div class="ibox-content" style="border: solid 0px">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="m-t-none m-b">Reporte de Empleados sin Timbrar</h1>
                    <hr class="demo-separator" />
                    <br />
                    <div class="form-horizontal">
                        <div class="form-group">
                            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                                <AjaxSettings>
                                    <telerik:AjaxSetting AjaxControlID="btnBuscarEmpleados">
                                        <UpdatedControls>
                                            <telerik:AjaxUpdatedControl ControlID="cmbEmpresa" />
                                            <telerik:AjaxUpdatedControl ControlID="btnBuscarEmpleados" />
                                        </UpdatedControls>
                                    </telerik:AjaxSetting>
                                </AjaxSettings>
                            </telerik:RadAjaxManager>
                            <table style="width: 100%; border-collapse: separate; border-spacing: 10px;">
                                <tr>
                                    <td style="width: 20%;">
                                        <label class="control-label">Seleccionar Cliente</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbEmpresa" runat="server" Width="500px" AutoPostBack="true"></telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 20%;">
                                        <label class="control-label">Seleccionar Folio De Nomina</label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbFolioNomina" runat="server" AutoPostBack="true" Width="300px"></telerik:RadComboBox>
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
                                    <td style="width: 20%;">
                                        <label class="control-label">Buscar por Nombre de Empleado</label>
                                    </td>
                                    <td>
                                        <telerik:RadTextBox ID="txtNombreEmpleado" runat="server" Width="300px"></telerik:RadTextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="width: 20%;"></td>
                                    <td>
                                        <telerik:RadButton ID="btnBuscarEmpleados" runat="server" Text="Buscar Empleados" CssClass="rbPrimaryButton" CausesValidation="False"></telerik:RadButton>
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
            </div>
            <br />
            <div class="row">
                <div class="col-lg-12">
                    <telerik:RadGrid ID="GridEmpleadosNoTimbrados" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" ShowFooter="False" ShowHeader="True" PageSize="20" CellSpacing="0" GridLines="None" Skin="Bootstrap" ExportSettings-ExportOnlyData="False">
                        <PagerStyle Mode="NumericPages" />
                        <ExportSettings IgnorePaging="True" FileName="ReporteEmpleadosNoTimbrados">
                            <Excel Format="Biff" />
                        </ExportSettings>
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                        </ClientSettings>
                        <MasterTableView NoMasterRecordsText="No hay registros para mostrar." CommandItemDisplay="Top">
                            <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="False" ShowExportToExcelButton="true" ShowExportToPdfButton="False" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn DataField="FolioNomina" ItemStyle-HorizontalAlign="Center" HeaderText="Folio de Nomina" UniqueName="FolioNomina">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Ejercicio" ItemStyle-HorizontalAlign="Left" HeaderText="Ejercicio" UniqueName="Ejercicio">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FechaDePago" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-HorizontalAlign="Left" HeaderText="Fecha de Pago" UniqueName="FechaDePago">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NombreEmpleado" ItemStyle-HorizontalAlign="Left" HeaderText="Empleado" UniqueName="NombreEmpleado">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TipoDeNomina" ItemStyle-HorizontalAlign="Left" HeaderText="Tipo de Nomina" UniqueName="TipoDeNomina">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Observaciones" ItemStyle-HorizontalAlign="Left" HeaderText="Observaciones" UniqueName="Observaciones">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FechaDeError" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-HorizontalAlign="Left" HeaderText="Fecha de Error" UniqueName="FechaDeError">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ErrorTimbrado" ItemStyle-HorizontalAlign="Left" HeaderText="Error Timbrado" UniqueName="ErrorTimbrado">
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
        <%--<telerik:RadWindowManager ID="rwConfirmGeneraNomina" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>--%>

    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
