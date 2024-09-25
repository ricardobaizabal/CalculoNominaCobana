<%@ Page Language="vb" MasterPageFile="~/MasterPage.Master" AutoEventWireup="false" CodeBehind="Acumulados.aspx.vb" Inherits="CalculoNomina.Acumulados" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function printPage() {
            window.print();
        }
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("grdPercepciones") > -1) || (arguments.get_eventTarget().indexOf("grdDeducciones") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <div class="ibox-content" style="border: solid 0px; min-height: 585px;">
            <table style="width: 100%; border-collapse: separate; border-spacing: 10px;" border="0">
                <tr>
                    <td style="width: 10%; text-align: left;">
                        <label class="control-label">Ejercicio</label>
                    </td>
                    <td style="width: 20%; text-align: left;">
                        <asp:DropDownList ID="cmbEjercicio" AutoPostBack="true" Width="92%" runat="server"></asp:DropDownList>
                    </td>
                    <td style="width: 5%; text-align: left;">
                        <label class="control-label">Mes</label>
                    </td>
                    <td style="width: 20%; text-align: left;">
                        <asp:DropDownList ID="cmbMes" AutoPostBack="true" Width="92%" runat="server">
                            <asp:ListItem Text="--Todos--" Value="0" />
                            <asp:ListItem Text="Enero" Value="1" />
                            <asp:ListItem Text="Febrero" Value="2" />
                            <asp:ListItem Text="Marzo" Value="3" />
                            <asp:ListItem Text="Abril" Value="4" />
                            <asp:ListItem Text="Mayo" Value="5" />
                            <asp:ListItem Text="Junio" Value="6" />
                            <asp:ListItem Text="Julio" Value="7" />
                            <asp:ListItem Text="Agosto" Value="8" />
                            <asp:ListItem Text="Septiembre" Value="9" />
                            <asp:ListItem Text="Octubre" Value="10" />
                            <asp:ListItem Text="Noviembre" Value="11" />
                            <asp:ListItem Text="Diciembre" Value="12" />
                        </asp:DropDownList>
                    </td>
                    <td style="width: 15%; text-align: left;">
                        <label class="control-label">Registro Patronal</label>
                    </td>
                    <td style="width: 20%; text-align: left;">
                        <asp:DropDownList ID="cmbRegistroPatronal" AutoPostBack="true" Width="92%" runat="server"></asp:DropDownList>
                    </td>
                    <td style="width: 10%;">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 10%; text-align: left;">
                        <label class="control-label">Cliente</label>
                    </td>
                    <td colspan="3">
                        <asp:DropDownList ID="cmbCliente" AutoPostBack="true" Width="97%" runat="server"></asp:DropDownList>
                    </td>
                    <td colspan="2" style="text-align: left;">
                        <telerik:RadButton ID="btnConsultar" RenderMode="Lightweight" Text="Consultar" Width="100px" Skin="Bootstrap" runat="server">
                        </telerik:RadButton>
                    </td>
                    <td style="width: 10%;">&nbsp;</td>
                </tr>
            </table>
            <div class="row">&nbsp;</div>
            <div class="row">
                <h3 class="item-title">Percepciones</h3>
            </div>
            <div class="row">
                <telerik:RadGrid ID="grdPercepciones" runat="server" AutoGenerateColumns="False" AllowPaging="True" ShowFooter="True" ExportSettings-ExportOnlyData="false" PageSize="50" CellSpacing="0" GridLines="None" Skin="Bootstrap">
                    <ExportSettings IgnorePaging="True" FileName="ReporteAcumuladosPercepciones">
                        <Excel Format="Biff" />
                    </ExportSettings>
                    <MasterTableView NoMasterRecordsText="No hay registros para mostrar." CommandItemDisplay="Top">
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                        <Columns>
                            <telerik:GridBoundColumn DataField="CvoConcepto" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="Codigo" UniqueName="CvoConcepto">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TipoConcepto" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="TipoConcepto" UniqueName="TipoConcepto">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Concepto" ItemStyle-Width="60%" ItemStyle-HorizontalAlign="Left" HeaderText="Concepto" UniqueName="Concepto" AllowFiltering="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Total" ItemStyle-Width="20%" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Total" UniqueName="Total">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="row">
                <h3 class="item-title">Deducciones</h3>
            </div>
            <div class="row">
                <telerik:RadGrid ID="grdDeducciones" runat="server" AutoGenerateColumns="False" AllowPaging="True" ShowFooter="True" ExportSettings-ExportOnlyData="false" PageSize="50" CellSpacing="0" GridLines="None" Skin="Bootstrap">
                    <ExportSettings IgnorePaging="True" FileName="ReporteAcumuladosDeducciones">
                        <Excel Format="Biff" />
                    </ExportSettings>
                    <MasterTableView NoMasterRecordsText="No hay registros para mostrar." CommandItemDisplay="Top">
                        <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                        <Columns>
                            <telerik:GridBoundColumn DataField="CvoConcepto" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="Codigo" UniqueName="CvoConcepto">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TipoConcepto" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderText="TipoConcepto" UniqueName="TipoConcepto">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Concepto" ItemStyle-Width="60%" ItemStyle-HorizontalAlign="Left" HeaderText="Concepto" UniqueName="Concepto" AllowFiltering="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Total" ItemStyle-Width="20%" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Total" UniqueName="Total">
                            </telerik:GridBoundColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row" style="text-align: right;">
                <asp:Label ID="lblTotalNeto" CssClass="item" Font-Size="Large" Font-Bold="true" runat="server"></asp:Label>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row" style="text-align: right;">
                <h3 class="item-title">Gravado y Exento</h3>
            </div>
            <div class="row">
                <table style="width: 100%;" border="0">
                    <tr>
                        <td style="width: 88%; text-align: right;" class="item-bold">TOTAL GRAVADO:</td>
                        <td style="width: 12%; text-align: right;">
                            <asp:Label ID="lblTotalGravado" CssClass="item-bold" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 88%; text-align: right;" class="item-bold">TOTAL EXENTO:</td>
                        <td style="width: 12%; text-align: right;">
                            <asp:Label ID="lblTotalExento" CssClass="item-bold" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="row">&nbsp;</div>
            <div class="row" style="text-align: right;">
                <button onclick="printPage()">Imprimir</button>
            </div>
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>