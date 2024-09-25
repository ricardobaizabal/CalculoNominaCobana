<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="FiniquitosQuincenal.aspx.vb" Inherits="CalculoNomina.FiniquitosQuincenal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-xs-12 col-sm-12 col-md-12 b-r">
            <fieldset>
                <h1 class="m-t-none m-b">Generación de Finiquitos Quincenal</h1>
                <hr class="demo-separator" />
                <br />
                <div class="form-group row">
                    <telerik:RadGrid ID="grdEmpleadosQuincenal" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" ShowFooter="True" ShowHeader="True" PageSize="50" CellSpacing="0" GridLines="None" Skin="Bootstrap" ExportSettings-ExportOnlyData="False">
                        <PagerStyle Mode="NumericPages" />
                        <ExportSettings IgnorePaging="True" FileName="ReporteBajasQuincenal">
                            <Excel Format="Biff" />
                        </ExportSettings>
                        <MasterTableView NoMasterRecordsText="No hay registros para mostrar." DataKeyNames="id" CommandItemDisplay="Top">
                            <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToExcelButton="True" ShowExportToPdfButton="False" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
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
            </fieldset>
        </div>
    </div>
</asp:Content>