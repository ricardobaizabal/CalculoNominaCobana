<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ModificacionGeneralCatorcenal.aspx.vb" Inherits="CalculoNomina.ModificacionGeneralCatorcenal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OpenWindow(id, empleadoid, contratoid, nominaid) {
            var wnd = $find('<%=RadWindow1.ClientID%>');
            var oWnd = radopen("IncidenciasCatorcenal.aspx?id=" + id + "&empleadoid=" + empleadoid + "&contratoid=" + contratoid + "&nominaid=" + nominaid, "RadWindow1");
            oWnd.set_modal(true);
            oWnd.set_centerIfModal(true);
            oWnd.set_visibleStatusbar(true);
            oWnd.setSize(1024, 850);
            oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Maximize);
            oWnd.set_autoSize(false);
            oWnd.set_title("Modificación de nómina ordinaria catorcenal - Incidencias")
            oWnd.center();
        }
        function confirmCallbackFnEliminaEmpleado(arg) {
            if (arg) //the user clicked OK
            {
                __doPostBack("<%=btnEliminarEmpleado.UniqueID %>", "");
            }
        }
        function getRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        }
        function CloseModal(page) {
            setTimeout(function () {
                GetRadWindow().close();
                window.top.location = page;
            }, 0);
        }
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("grdEmpleadosCatorcenal") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
    <style type="text/css">
        .RadWindow_Bootstrap .rwStatusBar {
            display: none !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">

        <asp:HiddenField ID="periodoId" runat="server" Value="0" />
        <asp:HiddenField ID="clienteId" runat="server" Value="0" />
        <asp:HiddenField ID="empleadoId" runat="server" Value="0" />
        <asp:HiddenField ID="nominaID" runat="server" Value="0" />

        <div class="ibox-content" style="border: solid 0px">
            <fieldset>
                <legend>
                    <asp:Label ID="lblTitulo" CssClass="control-label" runat="server" />
                </legend>
                <div class="row">
                    <div class="col-md-12">
                        <table style="width: 100%;" border="0">
                            <tr style="height: 30px;">
                                <td style="width: 10%;">
                                    <label class="control-label">Folio:</label>
                                </td>
                                <td style="width: 10%;">
                                    <asp:Label ID="lblNoNomina" runat="server"></asp:Label>
                                    <asp:Label ID="lblNoPeriodo" runat="server" Visible="false"></asp:Label>
                                </td>
                                <td style="width: 10%;">
                                    <label class="control-label">Tipo:</label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTipoNomina" runat="server"></asp:Label>
                                </td>
                                <td style="width: 10%;">
                                    <%--<label class="control-label">Dias:</label>--%>
                                </td>
                                <td>
                                    <asp:Label ID="lblDias" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height: 30px;">
                                <td style="width: 10%;">
                                    <label class="control-label">Ejercicio:</label>
                                </td>
                                <td style="width: 10%;">
                                    <asp:Label ID="lblEjercicio" runat="server"></asp:Label>
                                </td>
                                <td style="width: 10%;">
                                    <label class="control-label">Cliente:</label>
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="lblRazonSocial" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height: 30px;">
                                <td style="width: 10%;">
                                    <label class="control-label">F. Inicial:</label>
                                </td>
                                <td style="width: 10%;">
                                    <asp:Label ID="lblFechaInicial" runat="server"></asp:Label>
                                </td>
                                <td style="width: 10%;">
                                    <label class="control-label">F. Final:</label>
                                </td>
                                <td style="width: 10%;">
                                    <asp:Label ID="lblFechaFinal" runat="server"></asp:Label>
                                </td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </div>
                    <div class="col-md-12">&nbsp;</div>
                    <div class="col-md-12">
                        <telerik:RadGrid ID="grdEmpleadosCatorcenal" runat="server" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="true" ShowFooter="true" ExportSettings-ExportOnlyData="false" ShowStatusBar="true" PageSize="20" CellSpacing="0" GridLines="None" Skin="Bootstrap" AllowFilteringByColumn="true">
                            <GroupingSettings CaseSensitive="false" />
                            <MasterTableView AllowMultiColumnSorting="true" AllowFilteringByColumn="true" NoMasterRecordsText="No hay registros para mostrar." DataKeyNames="NoEmpleado, IdContrato, CuotaDiaria, IntegradoIMSS, IdContrato, idNomina">
                                <Columns>
                                    <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" HeaderText="Editar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEditar" runat="server" ImageUrl="~/images/action_edit.png" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="NoEmpleado" ItemStyle-HorizontalAlign="Left" HeaderText="No. Empleado" UniqueName="NoEmpleado" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Empleado" ItemStyle-HorizontalAlign="Left" HeaderText="Empleado" UniqueName="Empleado" AllowFiltering="true" AutoPostBackOnFilter="true" CurrentFilterFunction="Contains" ShowFilterIcon="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="INFONAVIT" AllowFiltering="false">
                                        <HeaderTemplate>INFONAVIT</HeaderTemplate>
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtINFONAVIT" runat="server" MinValue="0" Value="0" Type="Number" NumberFormat-DecimalDigits="2" OnTextChanged="txtINFONAVIT_TextChanged" AutoPostBack="true" Text='<%# Eval("INFONAVIT") %>' Skin="Default" Width="80px" TabIndex="0" MaxLength="7" AllowFiltering="false">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="," PositivePattern="$n" />
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="Faltas" AllowFiltering="false">
                                        <HeaderTemplate>Faltas</HeaderTemplate>
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtFaltas" runat="server" MinValue="0" Value="0" Type="Number" NumberFormat-DecimalDigits="2" OnTextChanged="txtFaltas_TextChanged" AutoPostBack="true" Text='<%# Eval("Faltas") %>' Skin="Default" Width="80px" TabIndex="1" MaxValue="7" MaxLength="1" AllowFiltering="false">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="IncapacidadEG" AllowFiltering="false">
                                        <HeaderTemplate>Incapacidad EG</HeaderTemplate>
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtIncapacidadEG" runat="server" MinValue="0" Value="0" Type="Number" NumberFormat-DecimalDigits="2" OnTextChanged="txtIncapacidadEG_TextChanged" AutoPostBack="true" Text='<%# Eval("IncapacidadEG") %>' Skin="Default" Width="80px" TabIndex="2" MaxValue="7" MaxLength="1" AllowFiltering="false">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="IncapacidadRT" AllowFiltering="false">
                                        <HeaderTemplate>Incapacidad RT</HeaderTemplate>
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtIncapacidadRT" runat="server" MinValue="0" Value="0" Type="Number" NumberFormat-DecimalDigits="2" OnTextChanged="txtIncapacidadRT_TextChanged" AutoPostBack="true" Text='<%# Eval("IncapacidadRT") %>' Skin="Default" Width="80px" TabIndex="3" MaxValue="7" MaxLength="1" AllowFiltering="false">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="IncapacidadMaterna" AllowFiltering="false">
                                        <HeaderTemplate>Incapacidad Materna</HeaderTemplate>
                                        <ItemTemplate>
                                            <telerik:RadNumericTextBox ID="txtIncapacidadMaterna" runat="server" MinValue="0" Value="0" Type="Number" NumberFormat-DecimalDigits="2" OnTextChanged="IncapacidadMaterna_TextChanged" AutoPostBack="true" Text='<%# Eval("IncapacidadMaterna") %>' Skin="Default" Width="80px" TabIndex="4" MaxValue="7" MaxLength="1" AllowFiltering="false">
                                                <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                                <EnabledStyle HorizontalAlign="Right" />
                                            </telerik:RadNumericTextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="TotalPercepciones" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Percepciones" UniqueName="TotalPercepciones" AllowFiltering="false" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TotalDucciones" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Ducciones" UniqueName="TotalDucciones" AllowFiltering="false" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Neto" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Neto" UniqueName="Neto" AllowFiltering="false" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="EstatusContrato" ItemStyle-HorizontalAlign="Left" HeaderText="Estatus" UniqueName="EstatusContrato" AllowFiltering="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="Eliminar" UniqueName="Eliminar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEliminar" runat="server" CausesValidation="false" CommandArgument='<% #Eval("NoEmpleado")%>' CommandName="cmdDelete" BorderStyle="None" ImageUrl="~/images/action_delete.gif" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>
                </div>
                <div class="row">&nbsp;</div>
                <div class="row">
                    <div class="col-md-12 text-right">
                        <telerik:RadButton ID="btnRegresar" RenderMode="Lightweight" runat="server" Width="120px" Skin="Bootstrap" CssClass="rbPrimaryButton" Text="Regresar"></telerik:RadButton>
                    </div>
                </div>
            </fieldset>
        </div>

        <asp:Button ID="btnEliminarEmpleado" Text="" Style="display: none;" runat="server" />
        <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
        <telerik:RadWindowManager ID="rwConfirmEliminaEmpleado" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>

        <telerik:RadWindowManager ID="RadWindowManager1" VisibleStatusbar="false" runat="server" OnClientClose="CloseModal">
            <Windows>
                <telerik:RadWindow ID="RadWindow1" VisibleStatusbar="false" runat="server" OnClientClose="CloseModal" Style="z-index: 7001" BackColor="White" RenderMode="Lightweight" Skin="Bootstrap">
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>

    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>

</asp:Content>
