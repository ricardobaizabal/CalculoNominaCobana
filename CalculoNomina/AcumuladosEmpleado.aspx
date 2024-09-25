<%@ Page Language="vb" MasterPageFile="~/MasterPage.Master" AutoEventWireup="false" CodeBehind="AcumuladosEmpleado.aspx.vb" Inherits="CalculoNomina.AcumuladosEmpleado" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">--%>
    <div class="ibox-content" style="border: solid 0px; min-height: 585px;">
        <table style="width: 100%; border-collapse: separate; border-spacing: 3px;" border="0">
            <tr>
                <td style="width: 10%; text-align: left;">
                    <label class="control-label">Ejercicio:</label>
                </td>
                <td style="width: 15%; text-align: left;">
                    <asp:DropDownList ID="cmbEjercicio" AutoPostBack="true" Width="92%" runat="server"></asp:DropDownList>
                </td>
                <td style="width: 10%; text-align: left;">
                    <label class="control-label">Desde:</label>
                </td>
                <td style="width: 15%; text-align: left;">
                    <asp:DropDownList ID="cmbMes1" AutoPostBack="false" Width="92%" runat="server">
                        <asp:ListItem Value="0" Text="--Seleccione--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Enero"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Febrero"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Marzo"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Abril"></asp:ListItem>
                        <asp:ListItem Value="5" Text="Mayo"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Junio"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Julio"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Agosto"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Septiembre"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Octubre"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Noviembre"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Diciembre"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 10%; text-align: left;">
                    <label class="control-label">Hasta:</label>
                </td>
                <td style="width: 15%; text-align: left;">
                    <asp:DropDownList ID="cmbMes2" AutoPostBack="false" Width="92%" runat="server">
                        <asp:ListItem Value="0" Text="--Seleccione--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Enero"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Febrero"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Marzo"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Abril"></asp:ListItem>
                        <asp:ListItem Value="5" Text="Mayo"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Junio"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Julio"></asp:ListItem>
                        <asp:ListItem Value="8" Text="Agosto"></asp:ListItem>
                        <asp:ListItem Value="9" Text="Septiembre"></asp:ListItem>
                        <asp:ListItem Value="10" Text="Octubre"></asp:ListItem>
                        <asp:ListItem Value="11" Text="Noviembre"></asp:ListItem>
                        <asp:ListItem Value="12" Text="Diciembre"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <telerik:RadButton ID="btnConsultar" RenderMode="Lightweight" CausesValidation="true" Text="Consultar" Width="100px" Skin="Metro" runat="server">
                    </telerik:RadButton>
                </td>
            </tr>
            <tr>
                <td colspan="7">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="7">
                    <div class="row">
                        <telerik:RadGrid ID="grdReporteNomina" runat="server" AutoGenerateColumns="True" Height="800" Width="850" AllowPaging="True" ShowFooter="True" ExportSettings-ExportOnlyData="false" PageSize="50" CellSpacing="0" GridLines="None" Skin="Office2010Silver">
                            <ClientSettings>
                                <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" FrozenColumnsCount="5"></Scrolling>
                            </ClientSettings>
                            <ExportSettings IgnorePaging="True" FileName="ReporteAcumuladosEmpleado">
                                <Excel Format="Biff" />
                            </ExportSettings>
                            <MasterTableView NoMasterRecordsText="No hay registros para mostrar." CommandItemDisplay="Top">
                                <CommandItemSettings ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="7">&nbsp;</td>
            </tr>
        </table>
    </div>
    <%--</telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Metro" Width="100%">
    </telerik:RadAjaxLoadingPanel>--%>
</asp:Content>
