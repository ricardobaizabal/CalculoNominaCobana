<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="FiniquitosSemanal.aspx.vb" Inherits="CalculoNomina.FiniquitosSemanal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("btnSearch") > -1) || (arguments.get_eventTarget().indexOf("btnAll") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12">
                <div class="row">
                    <div class="col-xs-12 col-sm-12 col-md-12">
                        <fieldset>
                            <legend style="padding-right: 6px; color: Black">
                                <asp:Label ID="tblTitulo" Text="Generación de finiquitos semanal" runat="server" Font-Bold="True" CssClass="item"></asp:Label>
                            </legend>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12">
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div class="col-lg-1">
                                                <label class="control-label">Empleado</label>
                                            </div>
                                            <label class="col-lg-3">
                                                <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtSearch" Width="95%" class="form-control"></telerik:RadTextBox>
                                            </label>
                                            <div class="col-lg-1">
                                                <telerik:RadButton ID="btnSearch" RenderMode="Lightweight" runat="server" Skin="Bootstrap" CssClass="rbPrimaryButton" Text="Buscar"></telerik:RadButton>
                                            </div>
                                            <div class="col-lg-7">
                                                <telerik:RadButton ID="btnAll" RenderMode="Lightweight" runat="server" Skin="Bootstrap" CssClass="rbPrimaryButton" Text="Ver todo"></telerik:RadButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">&nbsp;</div>
                            <div class="row">
                                <div class="col-xs-12 col-sm-12 col-md-12">
                                    <telerik:RadGrid ID="grdEmpleadosSemanal" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" ShowFooter="True" ShowHeader="True" PageSize="50" CellSpacing="0" GridLines="None" Skin="Bootstrap" ExportSettings-ExportOnlyData="False">
                                        <PagerStyle Mode="NumericPages" />
                                        <ExportSettings IgnorePaging="True" FileName="ReporteBajasSemanal">
                                            <Excel Format="Biff" />
                                        </ExportSettings>
                                        <MasterTableView NoMasterRecordsText="No hay registros para mostrar." DataKeyNames="id" CommandItemDisplay="Top">
                                            <CommandItemSettings ShowAddNewRecordButton="False" ShowRefreshButton="false" ShowExportToExcelButton="True" ShowExportToPdfButton="False" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                                            <Columns>
                                                <telerik:GridBoundColumn DataField="NoEmpleado" ItemStyle-HorizontalAlign="Left" HeaderText="No. Empleado" UniqueName="NoEmpleado">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NombreEmpleado" ItemStyle-HorizontalAlign="Left" HeaderText="Empleado" UniqueName="NombreEmpleado">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CuotaDiaria" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Sueldo Diario" UniqueName="CuotaDiaria">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="IntegradoIMSS" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Sueldo Diario Integrado" UniqueName="IntegradoIMSS">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaIngreso" ItemStyle-HorizontalAlign="Left" HeaderText="Fecha Ingreso" UniqueName="FechaIngreso" AllowFiltering="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="FechaBaja" ItemStyle-HorizontalAlign="Left" HeaderText="Fecha Baja" UniqueName="FechaBaja" AllowFiltering="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="TipoMovimiento" ItemStyle-HorizontalAlign="Left" HeaderText="Movimiento" UniqueName="TipoMovimiento" AllowFiltering="false">
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="Seleccionar" UniqueName="Seleccionar">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkSeleccionar" runat="server" CssClass="glyphicon glyphicon-edit" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" CausesValidation="false">
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="center" />
                                                </telerik:GridTemplateColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
