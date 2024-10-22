<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="FiniquitosGeneradosSemanal.aspx.vb" Inherits="CalculoNomina.FiniquitosGeneradosSemanal" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        <%--function confirmCallbackFnGenerarNomina(arg) {
            if (arg) {
                __doPostBack("<%=btnConfirmarGeneraNomina.UniqueID %>", "");
            }
        }--%>
        function confirmCallbackFnGeneraNominaElectronica(arg) {
            if (arg) {
                __doPostBack("<%=btnConfirmarGeneraNominaElectronica.UniqueID %>", "");
            }
        }
        function confirmCallbackFnTimbrarNomina(arg) {
            if (arg) {
                __doPostBack("<%=btnConfirmarTimbraNomina.UniqueID %>", "");
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
        function OpenWindow(empresaid, ejercicioid, tiponomina) {
            var oWnd = radopen("RptNominaEspecial.aspx?e=" + empresaid + "&ej=" + ejercicioid + "&p=0&t=" + tiponomina, "wndReporte");
            oWnd.set_modal(true);
            oWnd.set_centerIfModal(true);
            oWnd.setSize(1024, 768);
            oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
            oWnd.set_autoSize(false);
            oWnd.set_title("Reporte Nómina Extraordinaria - Finiquitos")
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
    <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
    <asp:HiddenField ID="empleadoId" runat="server" Value="0" Visible="False" />
    <asp:HiddenField ID="serie" runat="server" Value="" />
    <asp:HiddenField ID="folio" runat="server" Value="0" />
    <div class="ibox-content" style="border: solid 0px">
        <div class="row">
            <div class="col-md-12">
                <h1 class="m-t-none m-b">Finiquitos semanal generados</h1>
                <hr class="demo-separator" />
                <br />
            </div>
            <div class="col-md-12">
                <table style="width: 100%;" border="0">
                    <tr style="height: 30px;">
                        <td style="width: 8%;">
                            <label class="control-label">Ejercicio:</label>
                        </td>
                        <td style="width: 8%;">
                            <asp:Label ID="lblEjercicio" runat="server"></asp:Label>
                        </td>
                        <td style="width: 20%;">
                            <label class="control-label">Seleccionar Periodo</label>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbPeriodo" runat="server" AutoPostBack="true" Width="300px"></telerik:RadComboBox>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 text-right">
                <asp:LinkButton ID="lnkReporte" Visible="false" runat="server">Ver Reporte Nómina</asp:LinkButton>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">&nbsp;</div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="col-lg-2 col-md-2 col-sm-2">
                    <div class="form-group">
                        <telerik:RadButton ID="btnGenerarNominaElectronica" RenderMode="Lightweight" Enabled="false" runat="server" Width="141px" Height="50px">
                            <Image IsBackgroundImage="true" ImageUrl="images/GENERAR-NOM-ELECT.png"></Image>
                        </telerik:RadButton>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2">
                    <div class="form-group">
                        <telerik:RadButton ID="btnTimbrarNomina" RenderMode="Lightweight" Enabled="false" runat="server" Width="141px" Height="50px">
                            <Image IsBackgroundImage="true" ImageUrl="images/TIMBRAR-NOM.png"></Image>
                        </telerik:RadButton>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2">
                    <div class="form-group">
                        <telerik:RadButton ID="btnGenerarPDF" RenderMode="Lightweight" Enabled="false" runat="server" Width="141px" Height="50px">
                            <Image IsBackgroundImage="true" ImageUrl="images/GENERAR-COMPROBANTES.png"></Image>
                        </telerik:RadButton>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2">
                    <div class="form-group">
                        <telerik:RadButton ID="btnEnvioComprobantes" RenderMode="Lightweight" Enabled="false" runat="server" Width="141px" Height="50px">
                            <Image IsBackgroundImage="true" ImageUrl="images/ENVIAR-COMPROBANTES.png"></Image>
                        </telerik:RadButton>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2">
                    <div class="form-group">&nbsp;</div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2">
                    <div class="form-group">&nbsp;</div>
                </div>
            </div>
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-12">
                <div class="demo-container size-narrow">
                    <telerik:RadProgressManager ID="RadProgressManager1" runat="server" Skin="Bootstrap" />
                    <telerik:RadProgressArea HeaderText="Favor de esperar..." RenderMode="Lightweight" Skin="Bootstrap" ID="RadProgressArea1" runat="server" Width="100%" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:Panel runat="server" ID="panelDatos" Visible="true">
                    <div class="row">
                        <div class="col-md-12">
                            <telerik:RadGrid ID="grdEmpleados" runat="server" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="True" ShowFooter="True" ShowHeader="True" CellSpacing="0" GridLines="None" Skin="Bootstrap" ExportSettings-ExportOnlyData="False">
                                <PagerStyle Mode="NumericPages" />
                                <ExportSettings IgnorePaging="True" FileName="ReporteNominaEspcecialAguinaldo">
                                    <Excel Format="Biff" />
                                </ExportSettings>
                                <MasterTableView NoMasterRecordsText="No hay registros para mostrar." DataKeyNames="NoEmpleado, IdContrato, IdCliente, Ejercicio, TipoNomina, RFC" CommandItemDisplay="Top">
                                    <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="False" ShowExportToExcelButton="false" ShowExportToPdfButton="False" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="IdContrato" ItemStyle-HorizontalAlign="Left" HeaderText="No. Contrato" UniqueName="IdContrato">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="NoEmpleado" ItemStyle-HorizontalAlign="Left" HeaderText="No. Empleado" UniqueName="NoEmpleado">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Empleado" ItemStyle-HorizontalAlign="Left" HeaderText="Empleado" UniqueName="Empleado">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CuotaPeriodo" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Cuota Periodo" UniqueName="CuotaPeriodo">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TotalPercepciones" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Percepciones" UniqueName="TotalPercepciones">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TotalDucciones" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Deducciones" UniqueName="TotalDucciones">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="Neto" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Neto" UniqueName="Neto">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FechaAlta" ItemStyle-HorizontalAlign="Left" HeaderText="Fecha Contrato" UniqueName="FechaAlta">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FechaBaja" ItemStyle-HorizontalAlign="Left" HeaderText="Fecha Baja" UniqueName="FechaBaja">
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
                                                <%--<asp:ImageButton ID="imgPDF" Visible="false" runat="server" CommandName="cmdPDF" CommandArgument='<%# Eval("NoEmpleado") %>' ImageUrl="~/images/pdf.png"></asp:ImageButton>--%>
                                                <asp:ImageButton ID="imgPDFTimbrado" Visible="false" runat="server" CommandName="cmdPDFTimbrado" CommandArgument='<%# Eval("UUID") %>' ImageUrl="~/images/down.png"></asp:ImageButton>
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
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="Finiquito">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkFiniquito" runat="server" Text="Finiquito" CommandArgument='<%# Eval("NoEmpleado").ToString() & "|" & Eval("IdMovimiento").ToString() & "|" & Eval("IdCliente").ToString() %>' CommandName="cmdFiniquito" CausesValidation="false">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="Renuncia">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkRenuncia" runat="server" Text="Renuncia" CommandArgument='<%# Eval("NoEmpleado").ToString() & "|" & Eval("IdMovimiento").ToString() & "|" & Eval("IdCliente").ToString() %>' CommandName="cmdRenuncia" CausesValidation="false">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
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

    <asp:Button ID="btnConfirmarGeneraNominaElectronica" Text="" Style="display: none;" runat="server" />
    <asp:Button ID="btnConfirmarTimbraNomina" Text="" Style="display: none;" runat="server" />
    <asp:Button ID="btnConfirmarGeneraPDF" Text="" Style="display: none;" runat="server" />
    <asp:Button ID="btnConfirmarEnvioPDF" Text="" Style="display: none;" runat="server" />

</asp:Content>
