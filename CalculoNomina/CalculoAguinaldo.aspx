<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="CalculoAguinaldo.aspx.vb" Inherits="CalculoNomina.CalculoAguinaldo" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function confirmCallbackFnGenerarNomina(arg) {
            if (arg) {
                __doPostBack("<%=btnConfirmarGeneraNomina.UniqueID %>", "");
            }
        }
        function confirmCallbackFnBorrarNomina(arg) {
            if (arg) {
                __doPostBack("<%=btnConfirmarBorrarNomina.UniqueID %>", "");
            }
        }
        function confirmCallbackFnGeneraNominaElectronica(arg) {
            if (arg) {
                __doPostBack("<%=btnConfirmarGeneraNominaElectronica.UniqueID %>", "");
            }
        }
        function confirmCallbackFnGeneraPDF(arg) {
            if (arg) {
                __doPostBack("<%=btnConfirmarGeneraPDF.UniqueID %>", "");
            }
        }
        function confirmCallbackFnEnvioPDF(arg) {
            if (arg) {
                __doPostBack("<%=btnConfirmarEnvioPDF.UniqueID %>", "");
            }
        }
        function confirmCallbackFnEliminaEmpleado(arg) {
            if (arg) {
                __doPostBack("<%=btnEliminarEmpleado.UniqueID %>", "");
            }
        }
        function confirmCallbackFnTimbrarNomina(arg) {
            if (arg) {
                __doPostBack("<%=btnConfirmarTimbraNomina.UniqueID %>", "");
            }
        }

        function OpenWindow(empresaid, ejercicioid, tiponomina) {
            var oWnd = radopen("RptNominaEspecial.aspx?e=" + empresaid + "&ej=" + ejercicioid + "&p=0&t=" + tiponomina, "wndReporte");
            oWnd.set_modal(true);
            oWnd.set_centerIfModal(true);
            oWnd.setSize(1024, 768);
            oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
            oWnd.set_autoSize(false);
            oWnd.set_title("Reporte Nómina Especial - Aguinaldo")
            oWnd.center();
            oWnd.show();
        }

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement && window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;
            return oWindow;
        }
        function CloseModal() {
            // GetRadWindow().close();
            setTimeout(function () {
                GetRadWindow().close();
                window.top.location = document.location.href
                //document.location.href = document.location.href;
            }, 0);
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
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">--%>
    <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
    <asp:HiddenField ID="empleadoId" runat="server" Value="0" Visible="False" />
    <asp:HiddenField ID="serie" runat="server" Value="" />
    <asp:HiddenField ID="folio" runat="server" Value="0" />
    <asp:HiddenField ID="FolioUUID" runat="server" Value="" />
    <div class="ibox-content" style="border: solid 0px">
        <div class="row">
            <div class="col-sm-12 col-md-12 col-sm-12 b-r">
                <h1 class="m-t-none m-b">Generación de reparto de Aguinaldo</h1>
                <hr class="demo-separator" />
                <br />
            </div>
            <div class="col-md-12">&nbsp;</div>
            <div class="form-group row">
                <table style="width: 100%;" border="0">
                    <tr style="height: 30px;">
                        <td style="width: 8%;">
                            <label class="control-label">Ejercicio:</label>
                        </td>
                        <td style="width: 8%;">
                            <asp:Label ID="lblEjercicio" runat="server"></asp:Label>
                        </td>
                        <td style="width: 10%;">
                            <label class="control-label">Periodicidad:</label>
                        </td>
                        <td style="width: 15%;">
                            <asp:DropDownList ID="cmbPeriodicidad" AutoPostBack="true" Width="92%" runat="server">
                                <asp:ListItem Text="--Seleccione--" Value="0" />
                                <asp:ListItem Text="Semanal" Value="1" />
                                <asp:ListItem Text="Catorcenal" Value="2" />
                                <asp:ListItem Text="Quincenal" Value="3" />
                            </asp:DropDownList>
                        </td>
                        <td style="width: 59%;">
                            <asp:RequiredFieldValidator ID="valPeriodicidad" runat="server" ControlToValidate="cmbPeriodicidad" ValidationGroup="vgPeriodicidad" InitialValue="0" CssClass="item" ForeColor="Red" ErrorMessage="Selecciona un periodo de pago." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="form-group row">
                <div class="col-md-12 text-right">
                    <asp:LinkButton ID="lnkReporte" runat="server">Ver Reporte Nómina</asp:LinkButton>
                </div>
            </div>
            <div class="row">&nbsp;</div>
        </div>
        <div class="row">
            <div class="col-lg-3 col-md-3 col-sm-3">
                <telerik:RadButton ID="btnGeneraNomina" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Generar Nómina" Font-Bold="false" Width="100%" Height="50px">
                </telerik:RadButton>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3">
                <telerik:RadButton ID="btnModificacionDeNomina" RenderMode="Lightweight" Enabled="false" runat="server" Skin="Bootstrap" Text="Incidencias" Font-Bold="false" Width="100%" Height="50px">
                </telerik:RadButton>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3">
                <telerik:RadButton ID="btnBorrarNomina" RenderMode="Lightweight" Enabled="false" runat="server" Skin="Bootstrap" Text="Borrar Nómina" Font-Bold="false" Width="100%" Height="50px">
                </telerik:RadButton>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3">
                <telerik:RadButton ID="btnGenerarNominaElectronica" RenderMode="Lightweight" Enabled="false" runat="server" Skin="Bootstrap" Text="Generar Nómina Elect." Font-Bold="false" Width="100%" Height="50px">
                </telerik:RadButton>
            </div>
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-lg-3 col-md-3 col-sm-3">
                <telerik:RadButton ID="btnTimbrarNomina" RenderMode="Lightweight" Enabled="false" runat="server" Skin="Bootstrap" Text="Timbrar Nómina" Font-Bold="false" Width="100%" Height="50px">
                </telerik:RadButton>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3">
                <telerik:RadButton ID="btnGeneraTxtDispersion" RenderMode="Lightweight" Enabled="false" runat="server" Skin="Bootstrap" Text="Genera TXT Dispersion" Font-Bold="false" Width="100%" Height="50px">
                </telerik:RadButton>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3">
                <telerik:RadButton ID="btnGenerarPDF" RenderMode="Lightweight" Enabled="false" runat="server" Skin="Bootstrap" Text="Generar PDF's" Font-Bold="false" Width="100%" Height="50px">
                </telerik:RadButton>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3">
                <telerik:RadButton ID="btnEnvioComprobantes" RenderMode="Lightweight" Enabled="false" runat="server" Skin="Bootstrap" Text="Enviar Comprobantes" Font-Bold="false" Width="100%" Height="50px">
                </telerik:RadButton>
            </div>
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="demo-container size-narrow">
                <telerik:RadProgressManager ID="RadProgressManager1" runat="server" Skin="Bootstrap" />
                <telerik:RadProgressArea HeaderText="Favor de esperar..." RenderMode="Lightweight" Skin="Bootstrap" ID="RadProgressArea1" runat="server" Width="100%" />
                <br />
            </div>
        </div>
        <div class="row">&nbsp;</div>
        <asp:Panel runat="server" ID="panelDatos" Visible="false">
            <fieldset>
                <legend>
                    <asp:Label ID="lblTitulo" CssClass="control-label" runat="server" />
                </legend>
                <div class="form-group row">
                    <telerik:RadGrid ID="grdEmpleadosAguinaldo" runat="server" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="True" ShowFooter="True" ShowHeader="True" CellSpacing="0" GridLines="None" Skin="Bootstrap" ExportSettings-ExportOnlyData="False">
                        <PagerStyle Mode="NumericPages" />
                        <ExportSettings IgnorePaging="True" FileName="ReporteNominaEspcecialAguinaldo">
                            <Excel Format="Biff" />
                        </ExportSettings>
                        <MasterTableView NoMasterRecordsText="No hay registros para mostrar." DataKeyNames="IdEmpresa, NoEmpleado, RFC" CommandItemDisplay="Top">
                            <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="False" ShowExportToExcelButton="True" ShowExportToPdfButton="False" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn DataField="NoEmpleado" ItemStyle-HorizontalAlign="Left" HeaderText="No. Empleado" UniqueName="NoEmpleado">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Empleado" ItemStyle-HorizontalAlign="Left" HeaderText="Empleado" UniqueName="Empleado">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CuotaPeriodo" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Cuota Periodo" UniqueName="CuotaPeriodo">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TotalPercepciones" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Aguinaldo" UniqueName="TotalPercepciones">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TotalDucciones" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Impuesto" UniqueName="TotalDucciones">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Neto" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Neto" UniqueName="Neto">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FechaAlta" ItemStyle-HorizontalAlign="Left" HeaderText="Fecha Contrato" UniqueName="FechaAlta">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FormaPago" ItemStyle-HorizontalAlign="Left" HeaderText="Forma Pago" UniqueName="FormaPago">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Banco" ItemStyle-HorizontalAlign="Left" HeaderText="Banco" UniqueName="Banco">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EstatusContrato" ItemStyle-HorizontalAlign="Left" HeaderText="Estatus" UniqueName="EstatusContrato" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Generado" UniqueName="Generado">
                                    <ItemTemplate>
                                        <asp:Image ID="imgGenerado" Visible="false" runat="server" ImageUrl="~/images/action_check.gif" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="" UniqueName="Timbrado">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgTimbrado" Visible="false" runat="server" ImageUrl="~/images/action_check.gif"></asp:ImageButton>
                                        <asp:ImageButton ID="imgAlert" Visible="false" runat="server" ImageUrl="~/images/alert.png"></asp:ImageButton>
                                        <asp:ImageButton ID="imgXML" Visible="false" runat="server" CommandName="cmdXML" CommandArgument='<%# Eval("UUID") %>' ImageUrl="~/images/xml.png"></asp:ImageButton>
                                        <asp:ImageButton ID="imgPDF" Visible="false" runat="server" CommandName="cmdPDF" CommandArgument='<%# Eval("NoEmpleado") %>' ImageUrl="~/images/pdf.png"></asp:ImageButton>
                                        <telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="imgAlert" RelativeTo="Element" Position="BottomCenter" RenderInPageRoot="true" Text='<%#Eval("error")%>' ManualClose="true"></telerik:RadToolTip>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEnviar" Visible="false" runat="server" CommandName="cmdSend" CommandArgument='<%# Eval("NoEmpleado") %>' ImageUrl="~/images/envelope.jpg"></asp:ImageButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <%--<telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="Incidencias">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEditar" runat="server" Text="Editar" CommandArgument='<%# Eval("NoEmpleado") %>' TabIndex="-1"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>--%>
                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="Eliminar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" CausesValidation="false" CommandArgument='<% #Eval("NoEmpleado")%>' CommandName="cmdDelete" BorderStyle="None" ImageUrl="~/images/action_delete.gif" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </fieldset>
        </asp:Panel>
        <div class="row">&nbsp;</div>
    </div>
    <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rwConfirm" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
    <telerik:RadWindowManager ID="RadWindowManager1" VisibleStatusbar="false" runat="server">
        <Windows>
            <telerik:RadWindow ID="wndReporte" VisibleStatusbar="false" OnClientClose="CloseModal" runat="server" Style="z-index: 7001" BackColor="White" RenderMode="Lightweight" Skin="Bootstrap">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <%--</telerik:RadAjaxPanel>--%>
    <%--<telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>--%>
    <asp:Button ID="btnConfirmarGeneraNomina" Text="" Style="display: none;" runat="server" />
    <asp:Button ID="btnConfirmarBorrarNomina" Text="" Style="display: none;" runat="server" />
    <asp:Button ID="btnConfirmarGeneraNominaElectronica" Text="" Style="display: none;" runat="server" />
    <asp:Button ID="btnConfirmarTimbraNomina" Text="" Style="display: none;" runat="server" />
    <asp:Button ID="btnConfirmarGeneraPDF" Text="" Style="display: none;" runat="server" />
    <asp:Button ID="btnConfirmarEnvioPDF" Text="" Style="display: none;" runat="server" />
    <asp:Button ID="btnEliminarEmpleado" Text="" Style="display: none;" runat="server" />
</asp:Content>
