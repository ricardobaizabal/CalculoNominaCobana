<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ModificacionGeneralSemanal.aspx.vb" Inherits="CalculoNomina.ModificacionGeneralSemanal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function OpenWindow(id, empleadoid, contratoid, nominaid) {
            var wnd = $find('<%=RadWindow1.ClientID%>');
            var oWnd = radopen("IncidenciasWindow.aspx?id=" + id + "&empleadoid=" + empleadoid + "&contratoid=" + contratoid + "&nominaid=" + nominaid, "RadWindow1");
            oWnd.set_modal(true);
            oWnd.set_centerIfModal(true);
            oWnd.set_visibleStatusbar(false);
            oWnd.setSize(1024, 850);
            oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Maximize);
            oWnd.set_autoSize(false);
            oWnd.set_title("Modificación de nómina ordinaria semanal - Incidencias")
            oWnd.center();
        }
        function confirmCallbackFnEliminaEmpleado(arg) {
            if (arg) //the user clicked OK
            {
                __doPostBack("<%=btnEliminarEmpleado.UniqueID %>", "");
            }
        }
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement && window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;
            return oWindow;
        }
        function CloseModal(page) {
            //alert('HI');
            setTimeout(function () {
                GetRadWindow().close();
                window.top.location = page;
            }, 0);
        }
        //function closeWindow(c) {
        //    if (c == 2) {
        //        top.location.href = top.location.href;
        //    }
        //    getRadWindow().close();
        //}
        function OnClientCloseHandler(sender, args) {
            var data = args.get_argument();
            if (data) {
                alert(data);
            }
        }
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("grdEmpleadosSemanal") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">

        <asp:HiddenField ID="periodoId" runat="server" Value="0" />
        <asp:HiddenField ID="empresaId" runat="server" Value="0" />
        <asp:HiddenField ID="empleadoId" runat="server" Value="0" />
        <asp:HiddenField ID="nominaID" runat="server" Value="0" />

        <div class="ibox-content" style="border: solid 0px">
            <div class="row">
                <div class="col-md-12">
                    <table style="width: 100%;" border="0">
                        <tr style="height: 30px;">
                            <td style="width: 10%;">
                                <label class="control-label">Ejercicio:</label>
                            </td>
                            <td style="width: 10%;">
                                <asp:Label ID="lblEjercicio" runat="server"></asp:Label>
                            </td>
                            <td style="width: 10%;">
                                <label class="control-label">Razón Social:</label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblRazonSocial" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td style="width: 10%;">
                                <label class="control-label">No. Periodo:</label>
                            </td>
                            <td style="width: 10%;">
                                <asp:Label ID="lblNoPeriodo" runat="server"></asp:Label>
                            </td>
                            <td style="width: 10%;">
                                <label class="control-label">Tipo:</label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="lblTipoNomina" runat="server"></asp:Label>
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
                            <td style="width: 10%;">
                                <label class="control-label">Dias:</label>
                            </td>
                            <td>
                                <asp:Label ID="lblDias" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td style="width: 10%;">
                                <label class="control-label" style="display: none;">Incidencias:</label>
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="cmbIncidencias" runat="server" AutoPostBack="true" Visible="false" Width="100%"></asp:DropDownList>
                            </td>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <telerik:RadGrid ID="grdEmpleadosSemanal" runat="server" AutoGenerateColumns="false" AllowPaging="false" AllowSorting="true" ShowFooter="true" ExportSettings-ExportOnlyData="false" ShowStatusBar="true" PageSize="20" CellSpacing="0" GridLines="None" Skin="Bootstrap" AllowFilteringByColumn="true">
                        <GroupingSettings CaseSensitive="false" />
                        <MasterTableView AllowMultiColumnSorting="true" AllowFilteringByColumn="true" NoMasterRecordsText="No hay registros para mostrar." DataKeyNames="NoEmpleado, IdContrato, CuotaPeriodo, IntegradoIMSS, IdContrato, idNomina">
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
                                <telerik:GridBoundColumn DataField="TotalPercepciones" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Percepciones" UniqueName="TotalPercepciones" AllowFiltering="false" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TotalDucciones" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Ducciones" UniqueName="TotalDucciones" AllowFiltering="false" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Neto" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Neto" UniqueName="Neto" AllowFiltering="false" Aggregate="Sum" FooterStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="MetodoPago" ItemStyle-HorizontalAlign="Left" HeaderText="Método Pago" UniqueName="MetodoPago" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Banco" ItemStyle-HorizontalAlign="Left" HeaderText="Banco" UniqueName="Banco" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EstatusContrato" ItemStyle-HorizontalAlign="Left" HeaderText="Estatus" UniqueName="EstatusContrato" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="Comisiones" AllowFiltering="false">
                                    <HeaderTemplate>Comisiones</HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtComisiones" runat="server" MinValue="0" Value="0" Type="Currency" OnTextChanged="txtComisiones_TextChanged" AutoPostBack="true" Text='<%# Eval("Comisiones") %>' Skin="Default" Width="80px" TabIndex="0" AllowFiltering="false">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="Faltas" AllowFiltering="false">
                                    <HeaderTemplate>Faltas</HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtFaltas" runat="server" MinValue="0" Value="0" Type="Number" NumberFormat-DecimalDigits="2" OnTextChanged="txtFaltas_TextChanged" AutoPostBack="true" Text='<%# Eval("Faltas") %>' Skin="Default" Width="80px" TabIndex="0" AllowFiltering="false">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="HorasDobles" AllowFiltering="false">
                                    <HeaderTemplate>Horas Dobles</HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtHorasDobles" runat="server" MinValue="0" Value="0" Type="Number" OnTextChanged="txtHorasDobles_TextChanged" AutoPostBack="true" Text='<%# Eval("HorasDobles") %>' Skin="Default" Width="80px" TabIndex="0" AllowFiltering="false">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="HorasTriples" AllowFiltering="false">
                                    <HeaderTemplate>Horas Triples</HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtHorasTriples" runat="server" MinValue="0" Value="0" Type="Number" OnTextChanged="txtHorasTriples_TextChanged" AutoPostBack="true" Text='<%# Eval("HorasTriples") %>' Skin="Default" Width="80px" TabIndex="0" AllowFiltering="false">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="PremioAsistencia" AllowFiltering="false">
                                    <HeaderTemplate>Premio Asistencia</HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtPremioAsistencia" runat="server" MinValue="0" Value="0" Type="Currency" OnTextChanged="txtPremioAsistencia_TextChanged" AutoPostBack="true" Text='<%# Eval("PremioAsistencia") %>' Skin="Default" Width="80px" TabIndex="0" AllowFiltering="false">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="PremioPuntualidad" AllowFiltering="false">
                                    <HeaderTemplate>Premio Puntualidad</HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtPremioPuntualidad" runat="server" MinValue="0" Value="0" Type="Currency" OnTextChanged="txtPremioPuntualidad_TextChanged" AutoPostBack="true" Text='<%# Eval("PremioPuntualidad") %>' Skin="Default" Width="80px" TabIndex="0" AllowFiltering="false">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="PrimaDominical" AllowFiltering="false">
                                    <HeaderTemplate>Prima Dominical</HeaderTemplate>
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="txtPrimaDominical" runat="server" MinValue="0" Value="0" Type="Number" OnTextChanged="txtPrimaDominical_TextChanged" AutoPostBack="true" Text='<%# Eval("PrimaDominical") %>' Skin="Default" Width="80px" TabIndex="0" AllowFiltering="false">
                                            <NumberFormat DecimalDigits="2" GroupSeparator="," />
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                                <%--<telerik:GridBoundColumn DataField="Generado" ItemStyle-HorizontalAlign="Left" HeaderText="Generado" UniqueName="Generado">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Timbrado" ItemStyle-HorizontalAlign="Left" HeaderText="Timbrado" UniqueName="Timbrado">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Situacion" ItemStyle-HorizontalAlign="Left" HeaderText="Situacion" UniqueName="Situacion">
                                    </telerik:GridBoundColumn>--%>
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
