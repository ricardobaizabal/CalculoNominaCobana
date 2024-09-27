<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Empleado.aspx.vb" Inherits="CalculoNomina.Empleado" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("GridEmployees") > -1) || (arguments.get_eventTarget().indexOf("btnGuardarAnexo") > -1) || (arguments.get_eventTarget().indexOf("contratosList") > -1) || (arguments.get_eventTarget().indexOf("imgSend") > -1) || (arguments.get_eventTarget().indexOf("lnkVerContratos") > -1) || (arguments.get_eventTarget().indexOf("anexoFiniquito") > -1) || (arguments.get_eventTarget().indexOf("lnkDownload") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
        function openRadWindow(id) {
            var oWnd = radopen("envio_contrato.aspx?contratoid=" + id, "SendWindow");
            oWnd.SetWidth(1024);
            oWnd.SetHeight(650);
            oWnd.center();
        }
        function openRadWindowMovimientosIMSS(id) {
            var oWnd = radopen("MovIMSS.aspx?id=" + id, "MovIMSSWindow");
            oWnd.SetWidth(1024);
            oWnd.SetHeight(650);
            oWnd.center();
        }
        function openRadWindowOpenIMSS(id) {
            var oWnd = radopen("MovIMSS.aspx?id=" + id, "MovIMSSWindow");
            oWnd.SetWidth(1024);
            oWnd.SetHeight(650);
            oWnd.center();
        }
        function OnClientClose(sender, args) {
            document.location.href = document.location.href;
        }
        function KeyPress(sender, args) {
            if (args.KeyCode == 45 || args.KeyCode == 46) {
                return false;
            }
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
    <style>
        .ulwrapper {
            background: #569705;
            margin: auto;
            padding: 0;
            width: 108px;
            height: 20px;
            position: relative;
        }

        .ulwrapperblue {
            background: #40ADD4;
            margin: auto;
            padding: 0;
            width: 108px;
            height: 20px;
            position: relative;
        }

        .TitleBaja {
            display: block;
            text-align: center;
            line-height: 150%;
            font-size: 9pt;
            font-family: Verdana;
            text-decoration: none;
            color: white;
        }

        .auto-style1 {
            width: 50%;
            height: 17px;
        }

        .auto-style2 {
            width: 25%;
            height: 17px;
        }

        .RadGrid_Bootstrap .rgHeader, .RadGrid_Bootstrap .rgHeader a {
            font-weight: bold !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <telerik:RadWindowManager ID="RadWindowManager2" runat="server">
        </telerik:RadWindowManager>
        <asp:Panel ID="panelListaEmpleados" runat="server">
            <div class="row">
                <div class="col-lg-12">
                    <fieldset>
                        <legend style="padding-right: 6px; color: Black">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/buscador_03.jpg" ImageAlign="AbsMiddle" />&nbsp;<asp:Label ID="lblFiltros" Text="Buscador" runat="server" Font-Bold="true" CssClass="item"></asp:Label>
                        </legend>
                        <asp:Label ID="lblBuscador" runat="server" CssClass="item" Font-Bold="True" Text="Buscar por nómbre o clave de empleado, número de NSS o RFC:"></asp:Label>&nbsp;&nbsp;
                        <telerik:RadTextBox ID="txtbuscador" runat="server" Width="300px"></telerik:RadTextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblBuscadorCliente" runat="server" CssClass="item" Font-Bold="True" Text="Cliente:"></asp:Label>&nbsp;&nbsp;
                        <telerik:RadComboBox ID="cmbCliente" runat="server" Width="400px" OnClientFocus="clearFilters" Filter="StartsWith" AutoPostBack="false"></telerik:RadComboBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <telerik:RadButton ID="btnbuscador" runat="server" Text="Buscar" CssClass="rbPrimaryButton" Width="90px" CausesValidation="False"></telerik:RadButton>
                    </fieldset>
                </div>

            </div>
            <fieldset>
                <legend style="padding-right: 6px; color: Black">
                    <asp:Label ID="lblEmpleadosListLegend" runat="server" Font-Bold="true" Text="Listado de Empleados" CssClass="item"></asp:Label>
                </legend>
                <div class="row">
                    <div class="col-lg-12 text-right">
                        <telerik:RadButton ID="btnAgregaEmpleado" runat="server" Width="150px" Text="Agregar empleado" CssClass="rbPrimaryButton" CausesValidation="False"></telerik:RadButton>
                    </div>
                </div>
                <div class="row" style="height: 20px;">&nbsp;</div>
                <div class="row">
                    <div class="col-lg-12">
                        <telerik:RadGrid ID="GridEmployees" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="50" AllowSorting="true" ExportSettings-ExportOnlyData="true" CellSpacing="0" GridLines="None" Skin="Bootstrap">
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                            <ExportSettings IgnorePaging="true" FileName="Empleados">
                                <Excel Format="ExcelML" />
                            </ExportSettings>
                            <MasterTableView DataKeyNames="id" NoMasterRecordsText="No hay registros para mostrar." CommandItemDisplay="Top">
                                <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToPdfButton="false" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" HeaderText="Editar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" ImageUrl="~/images/action_edit.png" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn DataField="clave" HeaderText="Clave Emp" UniqueName="id"></telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="nombre" HeaderText="Nómbre" UniqueName="nombre"></telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="cliente" HeaderText="Cliente" UniqueName="cliente"></telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="municipio" HeaderText="Municipio" UniqueName="municipio">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="departamento" HeaderText="Departamento" UniqueName="departamento">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="no_imss" HeaderText="No. Seguro Social (NSS)" UniqueName="no_imss">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="fecha_alta" HeaderText="Fecha alta" UniqueName="fecha_alta" DataFormatString="{0:d}">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn DataField="salario" HeaderText="Sueldo Mensual" UniqueName="salario" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridTemplateColumn DataField="sueldos_y_salarios" HeaderText="Sueldos y Salarios" UniqueName="estatus" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Image ID="IMG_SYS" runat="server" ImageUrl='<%# GetImageUrl(Eval("sueldos_y_salarios")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn DataField="excedente" HeaderText="Excedente" UniqueName="estatus" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Image ID="IMG_EXC" runat="server" ImageUrl='<%# GetImageUrl(Eval("excedente")) %>' />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridBoundColumn DataField="estatus" HeaderText="Estatus" UniqueName="estatus">
                                    </telerik:GridBoundColumn>

                                </Columns>
                            </MasterTableView>

                        </telerik:RadGrid>
                    </div>
                </div>
            </fieldset>
        </asp:Panel>
        <br />
        <br />
        <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Default" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" VisibleOnPageLoad="false" Skin="Default" VisibleStatusbar="False" Behavior="Default" Height="80px" InitialBehavior="None" Left="" Top="" Style="z-index: 8000">
            <Windows>
                <telerik:RadWindow ID="SendWindow" runat="server" VisibleOnPageLoad="false" ShowContentDuringLoad="False" Modal="True" ReloadOnShow="True" Skin="Default" VisibleStatusbar="False" Behavior="Close" BackColor="Gray" Style="display: none; z-index: 1000;" Behaviors="Close" InitialBehavior="Close">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <telerik:RadWindowManager ID="RadWindowManager3" runat="server" VisibleOnPageLoad="false" Skin="Default" VisibleStatusbar="False" Behaviors="Close" Left="" Top="" Style="z-index: 8000">
            <Windows>
                <telerik:RadWindow ID="MovIMSSWindow" runat="server" VisibleOnPageLoad="false" ShowContentDuringLoad="False" Modal="True" ReloadOnShow="True" Skin="Default" VisibleStatusbar="False" Behaviors="Close" BackColor="Gray" Style="display: none; z-index: 1000;">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
