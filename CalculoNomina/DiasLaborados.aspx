<%@ Page Language="vb" MasterPageFile="~/MasterPage.Master" AutoEventWireup="false" CodeBehind="DiasLaborados.aspx.vb" Inherits="CalculoNomina.DiasLaborados" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ibox-content" style="border: solid 0px; min-height: 585px;">
        <table style="width: 100%; border-collapse: separate; border-spacing: 3px;" border="0">
            <tr>
                <td style="width: 10%; text-align: left;">
                    <label>Ejercicio:</label>
                </td>
                <td style="width: 15%; text-align: left;">
                    <asp:DropDownList ID="cmbEjercicio" AutoPostBack="false" Width="92%" runat="server"></asp:DropDownList>
                </td>
                <td style="text-align: left">
                    <telerik:RadButton ID="btnConsultar" RenderMode="Lightweight" CausesValidation="true" Text="Consultar" Width="100px" Skin="Metro" runat="server">
                    </telerik:RadButton>
                </td>
            </tr>
            <tr>
                <td colspan="3">&nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
                    <telerik:RadGrid ID="grdReporteDiasLaborados" runat="server" AutoGenerateColumns="false" Width="100%" AllowPaging="True" ShowFooter="True" ExportSettings-ExportOnlyData="false" PageSize="50" CellSpacing="0" GridLines="None" Skin="Office2010Silver">
                        <ExportSettings IgnorePaging="True" FileName="ReporteDiasLaborados">
                            <Excel Format="Biff" />
                        </ExportSettings>
                        <MasterTableView NoMasterRecordsText="No hay registros para mostrar." CommandItemDisplay="Top">
                            <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn DataField="NoEmpleado" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="50px" HeaderText="No Empleado" UniqueName="NoEmpleado">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Empleado" ItemStyle-HorizontalAlign="Left" HeaderText="Empleado" UniqueName="Empleado">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FechaAlta" ItemStyle-HorizontalAlign="Left" HeaderText="Fecha Alta" UniqueName="FechaAlta">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FechaBaja" ItemStyle-HorizontalAlign="Left" HeaderText="Fecha Baja" UniqueName="FechaBaja">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DiasLaborados" ItemStyle-HorizontalAlign="Right" HeaderText="Dias Laborados" UniqueName="DiasLaborados">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
