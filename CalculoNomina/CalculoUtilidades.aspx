<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="CalculoUtilidades.aspx.vb" Inherits="CalculoNomina.CalculoUtilidades" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function confirmCallbackFnGenerarNomina(arg) {
            if (arg)
            {
                __doPostBack("<%=btnConfirmarGeneraNomina.UniqueID %>", "");
            }
        }
        function confirmCallbackFnBorrarNomina(arg) {
            if (arg)
            {
                __doPostBack("<%=btnConfirmarBorrarNomina.UniqueID %>", "");
            }
        }
        function confirmCallbackFnGeneraNominaElectronica(arg) {
            if (arg)
            {
                __doPostBack("<%=btnConfirmarGeneraNominaElectronica.UniqueID %>", "");
            }
        }
        function confirmCallbackFnGeneraPDF(arg) {
            if (arg)
            {
                __doPostBack("<%=btnConfirmarGeneraPDF.UniqueID %>", "");
            }
        }
        function confirmCallbackFnEnvioPDF(arg) {
            if (arg)
            {
                __doPostBack("<%=btnConfirmarEnvioPDF.UniqueID %>", "");
            }
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
    <div class="ibox-content" style="border: solid 0px">
        <div class="row">
            <div class="col-sm-12 col-md-12 col-sm-12 b-r">
                <h1 class="m-t-none m-b">Generación de reparto de utilidades</h1>
                <hr class="demo-separator" />
                <br />
            </div>
            <div class="col-md-12">&nbsp;</div>
            <div class="col-md-12">&nbsp;</div>
            <div class="col-md-12">
                <div class="col-lg-2 col-md-2 col-sm-2">
                    <div class="form-group">
                        <telerik:RadButton ID="btnGeneraNomina" RenderMode="Lightweight" runat="server" Width="115px" Height="50px">
                            <image isbackgroundimage="True" imageurl="images/GENERANOM.jpg"></image>
                        </telerik:RadButton>
                    </div>
                </div>
                <%--<div class="col-lg-2 col-md-2 col-sm-2">
                    <div class="form-group">
                        <telerik:RadButton ID="btnModificacionDeNomina" RenderMode="Lightweight" runat="server" Width="115px" Height="50px">
                            <image isbackgroundimage="True" imageurl="images/INCIDENCIAS.jpg"></image>
                        </telerik:RadButton>
                    </div>
                </div>--%>
                <div class="col-lg-2 col-md-2 col-sm-2">
                    <div class="form-group">
                        <telerik:RadButton ID="btnBorrarNomina" RenderMode="Lightweight" runat="server" Width="115px" Height="50px">
                            <image isbackgroundimage="True" imageurl="images/BORRARNOM.jpg"></image>
                        </telerik:RadButton>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2">
                    <div class="form-group">
                        <telerik:RadButton ID="btnGenerarNominaElectronica" RenderMode="Lightweight" runat="server" Width="115px" Height="50px">
                            <image isbackgroundimage="True" imageurl="images/GENOMELECT.jpg"></image>
                        </telerik:RadButton>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2">
                    <div class="form-group">
                        <telerik:RadButton ID="btnTimbrarNomina" RenderMode="Lightweight" runat="server" Width="115px" Height="50px">
                            <image isbackgroundimage="True" imageurl="images/TIMBRARNOM.jpg"></image>
                        </telerik:RadButton>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2">
                    <div class="form-group">
                        <telerik:RadButton ID="btnGenerarPDF" RenderMode="Lightweight" runat="server" Width="115px" Height="50px">
                            <image isbackgroundimage="True" imageurl="images/GENPDF.jpg"></image>
                        </telerik:RadButton>
                    </div>
                </div>
                <div class="col-lg-2 col-md-2 col-sm-2">
                    <div class="form-group"></div>
                </div>
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
        <div class="row text-right">
            <telerik:RadButton ID="btnEnvioComprobantes" RenderMode="Lightweight" Enabled="false" Text="Enviar Comprobantes" Width="160px" runat="server">
            </telerik:RadButton>
        </div>
        <asp:Panel runat="server" ID="panelDatos" Visible="false">
            <fieldset>
                <legend>
                    <asp:Label ID="lblTitulo" CssClass="control-label" runat="server" />
                </legend>
                <div class="form-group row">
                    <table style="width: 100%;" border="0">
                        <tr style="height: 30px;">
                            <td style="width: 10%;">
                                <label class="control-label">Ejercicio:</label>
                            </td>
                            <td style="width: 10%;">
                                <asp:Label ID="lblEjercicio" runat="server"></asp:Label>
                            </td>
                            <td style="width: 80%;">&nbsp;</td>
                        </tr>
                    </table>
                </div>
                <%--<div class="form-group row">
                    <div class="col-md-12 text-right">
                        <asp:LinkButton ID="lnkReporte" runat="server">Ver Reporte Nómina</asp:LinkButton>
                    </div>
                </div>--%>
                <div class="form-group row">
                    <telerik:RadGrid ID="grdEmpleadosPTU" runat="server" AutoGenerateColumns="False" AllowPaging="False" AllowSorting="True" ShowFooter="True" ShowHeader="True" CellSpacing="0" GridLines="None" Skin="Bootstrap" ExportSettings-ExportOnlyData="False">
                        <pagerstyle mode="NumericPages" />
                        <exportsettings ignorepaging="True" filename="ReporteNominaEspcecialPTU">
                            <Excel Format="Biff" />
                        </exportsettings>
                        <mastertableview nomasterrecordstext="No hay registros para mostrar." datakeynames="NoEmpleado, IdContrato, IdEmpresa, IdEjercicio, TipoNomina" commanditemdisplay="Top">
                            <CommandItemSettings ShowAddNewRecordButton="False" ShowExportToExcelButton="True" ShowExportToPdfButton="False" ExportToPdfText="Exportar a pdf" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn DataField="NoEmpleado" ItemStyle-HorizontalAlign="Left" HeaderText="No. Empleado" UniqueName="NoEmpleado">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Empleado" ItemStyle-HorizontalAlign="Left" HeaderText="Empleado" UniqueName="Empleado">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TotalPercepciones" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Total Percepciones" UniqueName="TotalPercepciones">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TotalDucciones" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Total Ducciones" UniqueName="TotalDucciones">
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn DataField="Neto" DataFormatString="{0:C}" ItemStyle-HorizontalAlign="Right" HeaderText="Neto" UniqueName="Neto">
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridBoundColumn DataField="MetodoPago" ItemStyle-HorizontalAlign="Left" HeaderText="Método Pago" UniqueName="MetodoPago">
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
                                        <asp:ImageButton ID="imgXML" Visible="false" runat="server" CommandName="cmdXML" CommandArgument='<%# Eval("NoEmpleado") %>' ImageUrl="~/images/xml.png"></asp:ImageButton>
                                        <asp:ImageButton ID="imgPDF" Visible="false" runat="server" CommandName="cmdPDF" CommandArgument='<%# Eval("NoEmpleado") %>' ImageUrl="~/images/pdf.png"></asp:ImageButton>
                                        <%--<telerik:RadToolTip ID="RadToolTip1" runat="server" TargetControlID="imgAlert" RelativeTo="Element" Position="BottomCenter" RenderInPageRoot="true" Text='<%#Eval("error")%>' ManualClose="true"></telerik:RadToolTip>--%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEnviar" Visible="false" runat="server" CommandName="cmdSend" CommandArgument='<%# Eval("NoEmpleado") %>' ImageUrl="~/images/envelope.jpg"></asp:ImageButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="Incidencias">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEditar" runat="server" Text="Editar" CommandArgument='<%# Eval("NoEmpleado") %>' TabIndex="-1"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn AllowFiltering="false" HeaderText="" UniqueName="Eliminar">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEliminar" runat="server" CausesValidation="false" CommandArgument='<% #Eval("NoEmpleado")%>' CommandName="cmdDelete" BorderStyle="None" ImageUrl="~/images/action_delete.gif" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </mastertableview>
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
                <telerik:RadWindow ID="RadWindow1" VisibleStatusbar="false" OnClientClose="CloseModal" runat="server" Style="z-index: 7001" BackColor="White" RenderMode="Lightweight" Skin="Bootstrap">
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
