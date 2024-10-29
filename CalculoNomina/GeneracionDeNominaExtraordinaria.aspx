<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="GeneracionDeNominaExtraordinaria.aspx.vb" Inherits="CalculoNomina.GeneracionDeNominaExtraordinaria" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
    <script type="text/javascript">
        function confirmCallbackFnGenerarNomina(arg) {
            if (arg) {
                __doPostBack("<%=btnConfirmarGeneraNomina.UniqueID %>", "");
            }
        }
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
        function OpenWindow(empresaid, ejercicioid, periodoid, periodicidad, esp) {
            var oWnd = radopen("RptNominaSemanal.aspx?e=" + empresaid + "&ej=" + ejercicioid + "&p=" + periodoid + "&periodicidad=" + periodicidad + "&especial=" + esp, "wndReporte");
            oWnd.set_modal(true);
            oWnd.set_centerIfModal(true);
            oWnd.setSize(1024, 768);
            oWnd.set_behaviors(Telerik.Web.UI.WindowBehaviors.Close);
            oWnd.set_autoSize(false);
            oWnd.set_title("Reporte Nómina Extraordinaria")
            oWnd.center();
            oWnd.show();
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
        function OnClientShow(sender, args) {
            setTimeout(function () {
                var pnum = $find('<%= WinImportarMontoIndividual.ContentContainer.FindControl("txtMontoIndividual").ClientID%>');
                pnum.focus();
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

        .btn-CPeriodo {
            margin-left: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">--%>
    <asp:HiddenField ID="nominaID" runat="server" Value="0" Visible="False" />
    <asp:HiddenField ID="periodoID" runat="server" Value="0" Visible="False" />
    <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
    <asp:HiddenField ID="serie" runat="server" Value="" />
    <asp:HiddenField ID="folio" runat="server" Value="0" />
    <asp:HiddenField ID="FolioUUID" runat="server" Value="" />
    <div class="ibox-content" style="border: solid 0px">
        <div class="row">
            <div class="col-md-12">
                <h1 class="m-t-none m-b">Generación de nómina extraordinaria</h1>
                <hr class="demo-separator" />
                <br />
                <div class="form-horizontal">
                    <div class="form-group">
                        <table style="width: 100%; border-collapse: separate; border-spacing: 10px;">
                            <tr>
                                <td style="width: 20%;">
                                    <label class="control-label">Seleccionar Cliente</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbCliente" runat="server" Filter="StartsWith" OnClientFocus="clearFilters" Width="500px" AutoPostBack="true"></telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    <label class="control-label">Seleccionar Periodicidad</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbPeriodicidad" runat="server" AutoPostBack="true" Width="300px"></telerik:RadComboBox>
                                    &nbsp;&nbsp;&nbsp;
                                    <telerik:RadButton ID="btnCrearPeriodoID" Text="Crear Periodo" runat="server" RenderMode="Lightweight" CssClass="rbPrimaryButton" CausesValidation="true"></telerik:RadButton>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    <label class="control-label">Seleccionar Periodo</label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbPeriodo" runat="server" AutoPostBack="true" Width="300px"></telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    <label class="control-label">Fecha Pago</label>
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="fchPago" runat="server" DateFormat="dd/MM/yyyy" MinDate="01/01/1900">
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%; vertical-align: top;">
                                    <label class="control-label">Observaciones</label>
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="txtObservaciones" runat="server" AutoPostBack="true" Width="50%" Height="80px"
                                        TextMode="MultiLine"
                                        Rows="2"
                                        Wrap="false"
                                        Text="">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;"></td>
                                <td>
                                    <telerik:RadButton ID="btnRegresar" Text="Regresar" runat="server" Skin="Bootstrap" RenderMode="Lightweight" CssClass="rbPrimaryButton" Width="90px">
                                    </telerik:RadButton>
                                    &nbsp;&nbsp;&nbsp;
                                    <telerik:RadButton ID="btnAgregarNominaE" runat="server" Text="Agregar Nómina" CssClass="rbPrimaryButton" CausesValidation="False">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-2">
                <div class="form-group">
                    <telerik:RadButton ID="btnModificacionDeNomina" RenderMode="Lightweight" Enabled="false" runat="server" Skin="Bootstrap" Text="Incidencias" Font-Bold="false" Width="100%" Height="50px" Visible="false">
                    </telerik:RadButton>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <telerik:RadButton ID="btnGeneraNomina" RenderMode="Lightweight" runat="server" Width="141px" Height="50px">
                        <Image IsBackgroundImage="true" ImageUrl="images/GENERAR-NOM.png"></Image>
                    </telerik:RadButton>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <telerik:RadButton ID="btnBorrarNomina" RenderMode="Lightweight" Enabled="false" runat="server" Width="141px" Height="50px">
                        <Image IsBackgroundImage="true" ImageUrl="images/BORRAR-NOM.png"></Image>
                    </telerik:RadButton>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <telerik:RadButton ID="btnGenerarNominaElectronica" RenderMode="Lightweight" Enabled="false" runat="server" Width="141px" Height="50px">
                        <Image IsBackgroundImage="true" ImageUrl="images/GENERAR-NOM-ELECT.png"></Image>
                    </telerik:RadButton>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <telerik:RadButton ID="btnTimbrarNominaSemanal" RenderMode="Lightweight" Enabled="false" runat="server" Width="141px" Height="50px">
                        <Image IsBackgroundImage="true" ImageUrl="images/TIMBRAR-NOM.png"></Image>
                    </telerik:RadButton>
                </div>
            </div>
            <div class="col-md-2">
                <div class="form-group">
                    <telerik:RadButton ID="btnGenerarPDF" RenderMode="Lightweight" Enabled="false" runat="server" Width="141px" Height="50px">
                        <Image IsBackgroundImage="true" ImageUrl="images/GENERAR-COMPROBANTES.png"></Image>
                    </telerik:RadButton>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <telerik:RadButton ID="btnGeneraTxtDispersion" RenderMode="Lightweight" Enabled="false" runat="server" Skin="Bootstrap" Text="Genera TXT Dispersion" Font-Bold="false" Width="100%" Height="50px" Visible="false">
                </telerik:RadButton>
            </div>
            <div class="col-md-2">
                <telerik:RadButton ID="btnDescargarPDFS" RenderMode="Lightweight" Enabled="false" runat="server" Width="141px" Height="50px">
                    <Image IsBackgroundImage="true" ImageUrl="images/DESCARGAR-PDF.png"></Image>
                </telerik:RadButton>
            </div>
            <div class="col-md-2">
                <telerik:RadButton ID="btnDescargarXMLS" RenderMode="Lightweight" Enabled="false" runat="server" Width="141px" Height="50px">
                    <Image IsBackgroundImage="true" ImageUrl="images/DESCARGAR-XML.png"></Image>
                </telerik:RadButton>
            </div>
            <div class="col-md-2">
                <telerik:RadButton ID="btnEnvioComprobantes" RenderMode="Lightweight" Enabled="false" runat="server" Width="141px" Height="50px">
                    <Image IsBackgroundImage="true" ImageUrl="images/ENVIAR-COMPROBANTES.png"></Image>
                </telerik:RadButton>
            </div>
            <div class="col-md-2">
                <telerik:RadButton ID="btnExportar" RenderMode="Lightweight" Enabled="false" runat="server" Width="141px" Height="50px">
                    <Image IsBackgroundImage="true" ImageUrl="images/EXPORTAR.png"></Image>
                </telerik:RadButton>
            </div>
            <div class="col-md-2">
                <telerik:RadButton ID="btnImportar" RenderMode="Lightweight" Enabled="false" runat="server" Width="141px" Height="50px">
                    <Image IsBackgroundImage="true" ImageUrl="images/IMPORTAR.png"></Image>
                </telerik:RadButton>
            </div>
        </div>
        <div class="row">&nbsp;</div>
        <div class="row">
            <div class="col-md-12">
                <telerik:RadProgressManager ID="RadProgressManager1" runat="server" Skin="Bootstrap" />
                <telerik:RadProgressArea HeaderText="Favor de esperar..." RenderMode="Lightweight" Skin="Bootstrap" ID="RadProgressArea1" runat="server" Width="100%" />
            </div>
        </div>
        <div class="row">&nbsp;</div>
        <asp:Panel runat="server" ID="panelDatos" Visible="false">
            <br />
            <fieldset>
                <legend>
                    <asp:Label ID="lblTitulo" CssClass="control-label" runat="server" />
                </legend>
                <div class="form-group row">
                    <table style="width: 100%;" border="0">
                        <tr style="height: 30px;">
                            <td style="width: 10%;">
                                <label class="control-label">Folio:</label>
                            </td>
                            <td style="width: 10%;">
                                <asp:Label ID="lblFolio" runat="server"></asp:Label>
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
                                <label class="control-label">Ejercicio:</label>
                            </td>
                            <td style="width: 10%;">
                                <asp:Label ID="lblEjercicio" runat="server"></asp:Label>
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
                    </table>
                </div>
                <div class="form-group row">
                    <div class="col-md-9 text-right">
                        <%--<asp:CheckBox ID="chkAplicaOrden" Text="Ordenar archivos por centro de costos" runat="server" />--%>
                    </div>
                    <div class="col-md-3 text-right">
                        <asp:LinkButton ID="lnkReporte" runat="server">Ver Reporte Nómina</asp:LinkButton>
                    </div>
                </div>
                <div class="form-group row">
                    <telerik:RadGrid ID="grdEmpleadosSemanal" runat="server" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" ShowFooter="True" ShowHeader="True" PageSize="100" CellSpacing="0" GridLines="None" Skin="Bootstrap" ExportSettings-ExportOnlyData="False">
                        <PagerStyle Mode="NumericPages" />
                        <ExportSettings IgnorePaging="True" ExportOnlyData="true" FileName="ReporteNominaExtraordinaria">
                            <Excel Format="Biff" />
                        </ExportSettings>
                        <MasterTableView NoMasterRecordsText="No hay registros para mostrar." DataKeyNames="NoEmpleado,RFC" CommandItemDisplay="Top">
                            <CommandItemSettings ShowRefreshButton="false" ShowAddNewRecordButton="False" ShowExportToExcelButton="true" ShowExportToPdfButton="False" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn DataField="NoEmpleado" ItemStyle-HorizontalAlign="Left" HeaderText="No. Empleado" UniqueName="NoEmpleado">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Empleado" ItemStyle-HorizontalAlign="Left" HeaderText="Empleado" UniqueName="Empleado">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TotalPercepciones" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Total Percepciones" UniqueName="TotalPercepciones">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TotalDucciones" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Total Ducciones" UniqueName="TotalDucciones">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Neto" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Neto" UniqueName="Neto">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="MetodoPago" ItemStyle-HorizontalAlign="Left" HeaderText="Método Pago" UniqueName="MetodoPago">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Banco" ItemStyle-HorizontalAlign="Left" HeaderText="Banco" UniqueName="Banco">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="EstatusContrato" ItemStyle-HorizontalAlign="Left" HeaderText="Estatus" UniqueName="EstatusContrato" AllowFiltering="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Importar Monto" UniqueName="ImportarIn" Exportable="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnImportarIn" Visible="True" runat="server" CommandName="cmdImportarM" CommandArgument='<%# Eval("NoEmpleado") %>' ImageUrl="~/images/up.png"></asp:ImageButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="Generado" UniqueName="Generado" Exportable="false">
                                    <ItemTemplate>
                                        <asp:Image ID="imgGenerado" Visible="false" runat="server" ImageUrl="~/images/action_check.gif" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="" UniqueName="Timbrado" Exportable="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgTimbrado" Visible="false" runat="server" ImageUrl="~/images/action_check.gif"></asp:ImageButton>
                                        <asp:ImageButton ID="imgAlert" Visible="false" runat="server" ImageUrl="~/images/alert.png"></asp:ImageButton>
                                        <asp:ImageButton ID="imgXML" Visible="false" runat="server" CommandName="cmdXML" CommandArgument='<%# Eval("UUID") %>' ImageUrl="~/images/xml.png"></asp:ImageButton>
                                        <asp:ImageButton ID="imgPDF" Visible="false" runat="server" CommandName="cmdPDF" CommandArgument='<%# Eval("NoEmpleado") %>' ImageUrl="~/images/pdf.png"></asp:ImageButton>
                                        <asp:ImageButton ID="imgPDFTimbrado" Visible="false" runat="server" CommandName="cmdPDFTimbrado" CommandArgument='<%# Eval("UUID") %>' ImageUrl="~/images/pdf.png"></asp:ImageButton>
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
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </fieldset>
        </asp:Panel>
        <div class="row">&nbsp;</div>
    </div>

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        <Windows>
            <telerik:RadWindow ID="wndReporte" runat="server">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>

    <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
    <telerik:RadWindowManager ID="rwConfirmGeneraNomina" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>

    <asp:Button ID="btnConfirmarGeneraNomina" Text="" Style="display: none;" runat="server" />
    <asp:Button ID="btnConfirmarBorrarNomina" Text="" Style="display: none;" runat="server" />
    <asp:Button ID="btnConfirmarGeneraNominaElectronica" Text="" Style="display: none;" runat="server" />
    <asp:Button ID="btnConfirmarTimbraNomina" Text="" Style="display: none;" runat="server" />
    <asp:Button ID="btnConfirmarGeneraPDF" Text="" Style="display: none;" runat="server" />
    <asp:Button ID="btnConfirmarEnvioPDF" Text="" Style="display: none;" runat="server" />

    <!-- Start Modal Agregar Periodo -->
    <telerik:RadWindow ID="WinPeriodoSave" Title="" runat="server" Modal="true" CenterIfModal="true"
        AutoSize="false" Behaviors="Close" VisibleOnPageLoad="False" Width="400px" Height="260px">
        <ContentTemplate>
            <table style="width: 98%; border-collapse: separate; border-spacing: 10px;">
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server">Fecha inicio:</asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="fchInicioPeriodo" runat="server" DateFormat="dd/MM/yyyy" MinDate="01/01/1900">
                        </telerik:RadDatePicker>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server">Fecha fin:</asp:Label>
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="fchFinPeriodo" runat="server" DateFormat="dd/MM/yyyy" MinDate="01/01/1900">
                        </telerik:RadDatePicker>
                    </td>
                </tr>
            </table>
            <div style="width: 100%; text-align: end; margin-top: 10px;">
                <asp:HiddenField ID="periodicidadid" Value="0" runat="server" />
                <telerik:RadButton ID="btnCancelarPeriodo" runat="server" Text="Salir" Skin="Bootstrap" RenderMode="Lightweight" CssClass="rbPrimaryButton" Width="85px"></telerik:RadButton>
                <telerik:RadButton ID="btnCrearPeriodoEspecial" runat="server" Text="Crear" Skin="Bootstrap" RenderMode="Lightweight" CssClass="rbPrimaryButton" Width="85px"></telerik:RadButton>
            </div>
            </div>
        </ContentTemplate>
        <Localization />
    </telerik:RadWindow>
    <!-- End Start Modal Agregar Periodo -->

    <!-- Start Modal Importar -->
    <telerik:RadWindow ID="WinImportarMonto" Title="" runat="server" Modal="true" CenterIfModal="true"
        AutoSize="false" Behaviors="Close" VisibleOnPageLoad="False" Width="410px" Height="260px">
        <ContentTemplate>
            <table style="width: 98%; border-collapse: separate; border-spacing: 10px;">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server">Archivo:</asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="ImportarFile" Enabled="true" runat="server" Skin="Bootstrap" />
                    </td>
                </tr>
            </table>
            <div style="width: 100%; text-align: center; margin-top: 10px;">
                <asp:HiddenField ID="HiddenField1" Value="0" runat="server" />
                <telerik:RadButton ID="btnEImportarCSV" runat="server" Text="Importar" Skin="Bootstrap" RenderMode="Lightweight" CssClass="rbPrimaryButton"></telerik:RadButton>
                <telerik:RadButton ID="btnSalirImportar" runat="server" Text="Salir" Skin="Bootstrap" RenderMode="Lightweight" CssClass="rbPrimaryButton"></telerik:RadButton>
            </div>
            </div>
        </ContentTemplate>
        <Localization />
    </telerik:RadWindow>
    <!-- End Start Modal Importar -->

    <!-- Start Modal Importar Individual-->
    <telerik:RadWindow ID="WinImportarMontoIndividual" Title="" runat="server" Modal="true" CenterIfModal="true"
        AutoSize="false" Behaviors="Close" OnClientShow="OnClientShow" VisibleOnPageLoad="False" Width="400px" Height="300px">
        <ContentTemplate>
            <table style="width: 100%; border-collapse: separate; border-spacing: 10px;">
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server">Monto:</asp:Label>
                    </td>
                    <td>
                        <%--<asp:TextBox ID="txtMontoIndividual" runat="server" Skin="Bootstrap"></asp:TextBox>--%>
                        <telerik:RadNumericTextBox RenderMode="Lightweight" runat="server" Skin="Bootstrap" ID="txtMontoIndividual" EmptyMessage="0.00" MinValue="0" NumberFormat-DecimalDigits="2"></telerik:RadNumericTextBox>
                    </td>
                </tr>
            </table>
            <div style="width: 100%; text-align: center; margin-top: 10px;">
                <asp:HiddenField ID="HiddenField2" Value="0" runat="server" />
                <telerik:RadButton ID="btnImportarIndividual" runat="server" Text="Importar" Skin="Bootstrap" RenderMode="Lightweight" CssClass="rbPrimaryButton" Width="90px">
                </telerik:RadButton>
                <telerik:RadButton ID="btnCancelarIndividual" runat="server" Text="Salir" Skin="Bootstrap" RenderMode="Lightweight" CssClass="rbPrimaryButton">
                </telerik:RadButton>
            </div>
            </div>
        </ContentTemplate>
        <Localization />
    </telerik:RadWindow>
    <!-- End Start Modal Importar Individual-->

</asp:Content>
