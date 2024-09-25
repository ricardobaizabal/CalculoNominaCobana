<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ReporteDeducciones.aspx.vb" Inherits="CalculoNomina.ReporteDeducciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .item {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10px;
        }

        .item-bold {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: 600;
        }

        .item-title {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
            font-weight: 600;
        }

        .item-subtitle {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
        }

        hr {
            border: 0;
            border-top: 1px solid #999;
            border-bottom: 1px solid #333;
            height: 0;
        }
    </style>
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("grdDeducciones") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">--%>
    <div>
        <table style="width: 100%;" border="0">
            <tr>
                <td align="right">
                    <span class="item-title">TOP SERVICIOS EMPRESARIALES SA DE CV</span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <span class="item-subtitle">ZAPOTLAN 227 MITRAS SUR C.P. 64020 MONTERREY, NUEVO LEON Tel:8183731435</span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <fieldset>
                        <legend>
                            <asp:Label ID="lblTitulo" CssClass="control-label" runat="server" />
                        </legend>
                    </fieldset>
                </td>
            </tr>
        </table>
        <table style="width: 100%;" border="0">
            <tr>
                <td style="width: 15%; text-align: left;">
                    <label class="control-label">Ejercicio</label>
                </td>
                <td style="width: 35%; text-align: left;">
                    <asp:DropDownList ID="cmbEjercicio" AutoPostBack="false" Width="97%" runat="server"></asp:DropDownList>
                </td>
                <td style="width: 50%;">&nbsp;
                        <asp:RequiredFieldValidator ID="valEjercicio" runat="server" ErrorMessage="Selecciona un ejercicio" ControlToValidate="cmbEjercicio" InitialValue="0" ForeColor="Red" CssClass="item" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: left;">
                    <label class="control-label">Tipo Nómina:</label>
                </td>
                <td style="width: 35%; text-align: left;">
                    <asp:DropDownList ID="cmbTipoNomina" AutoPostBack="true" Width="97%" runat="server">
                        <asp:ListItem Value="0" Text="--Seleccione--"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Semanal"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Catorcenal"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Quincenal"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 50%;">&nbsp;
                        <asp:RequiredFieldValidator ID="valTipoNomina" runat="server" ErrorMessage="Selecciona el tipo de nómina" ControlToValidate="cmbTipoNomina" InitialValue="0" ForeColor="Red" CssClass="item" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="width: 15%; text-align: left;">
                    <label class="control-label">Periodo</label>
                </td>
                <td style="width: 35%; text-align: left;">
                    <asp:DropDownList ID="cmbPeriodo" AutoPostBack="false" Width="97%" runat="server"></asp:DropDownList>
                </td>
                <td style="width: 50%;">&nbsp;
                        <telerik:RadButton ID="btnConsultar" RenderMode="Lightweight" runat="server" Skin="Silk" Text="Consultar" Font-Bold="false" Width="115px">
                        </telerik:RadButton>
                    <asp:RequiredFieldValidator ID="valPeriodo" runat="server" ErrorMessage="Selecciona un periodo" ControlToValidate="cmbPeriodo" InitialValue="0" ForeColor="Red" CssClass="item" SetFocusOnError="true"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
    <div>
        <h3 class="item-title">Reporte Deducciones</h3>
    </div>
    <div>
        <telerik:RadGrid ID="grdDeducciones" runat="server" AutoGenerateColumns="False" AllowPaging="True" ShowFooter="True" PageSize="50" CellSpacing="0" ExportSettings-ExportOnlyData="true" GridLines="None" Skin="Office2010Silver">
            <PagerStyle Mode="NextPrevAndNumeric" />
            <ExportSettings IgnorePaging="True" FileName="ReportePercepciones">
                <Excel Format="ExcelML" />
            </ExportSettings>
            <MasterTableView NoMasterRecordsText="No hay registros para mostrar." CommandItemDisplay="Top">
                <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="false" ShowExportToExcelButton="true" ShowExportToPdfButton="false" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                <Columns>
                    <telerik:GridBoundColumn DataField="NoEmpleado" ItemStyle-HorizontalAlign="Left" HeaderText="No. Empleado" UniqueName="NoEmpleado">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Empleado" ItemStyle-HorizontalAlign="Left" HeaderText="Empleado" UniqueName="Empleado">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Departamento" ItemStyle-HorizontalAlign="Left" HeaderText="Departamento" UniqueName="Departamento">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Puesto" ItemStyle-HorizontalAlign="Left" HeaderText="Puesto" UniqueName="Puesto">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Concepto" ItemStyle-HorizontalAlign="Left" HeaderText="Concepto" UniqueName="Concepto" AllowFiltering="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Unidad" ItemStyle-HorizontalAlign="Left" HeaderText="Unidad" UniqueName="Unidad" AllowFiltering="false">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Importe" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Importe" UniqueName="Importe">
                    </telerik:GridBoundColumn>
                    <telerik:GridBoundColumn DataField="Total" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Total" UniqueName="Total">
                    </telerik:GridBoundColumn>
                </Columns>
            </MasterTableView>
        </telerik:RadGrid>
    </div>
    <%--</telerik:RadAjaxPanel>--%>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Office2010Silver" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
