<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="PeriodosSemanales.aspx.vb" Inherits="CalculoNomina.PeriodosSemanales" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
    <%--<style type="text/css">
        .rcbScroll {
            max-height: 380px !important;
        }
        .rcbSlide {
            max-height: 380px !important;
        }
    </style>--%>
    <script type="text/javascript">
        function confirmCallbackFn(arg) {
            if (arg) //the user clicked OK
            {
                __doPostBack("<%=HiddenButton.UniqueID %>", "");
            }
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
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
    <asp:HiddenField ID="ejercicioId" runat="server" Value="0" Visible="False" />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="ibox-content" style="border: solid 0px">
            <div class="row">
                <div class="col-lg-12">
                    <h1 class="m-t-none m-b">Periodos Semanales</h1>
                    <hr class="demo-separator" />
                    <br />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 10%;">
                                <label class="control-label">Cliente</label>
                            </td>
                            <td style="width: 30%;">
                                <telerik:RadComboBox ID="cmbCliente" runat="server" Filter="StartsWith" OnClientFocus="clearFilters" Width="95%" AutoPostBack="true"></telerik:RadComboBox>
                            </td>
                            <td>
                                <telerik:RadButton ID="btnConsultar" RenderMode="Lightweight" runat="server" Skin="Bootstrap" CssClass="rbPrimaryButton" Text="Consultar"></telerik:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: right;">
                                <telerik:RadButton ID="btnAgregar" RenderMode="Lightweight" runat="server" Skin="Bootstrap" CssClass="rbPrimaryButton" CausesValidation="false" Text="Agregar"></telerik:RadButton>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <telerik:RadGrid ID="GridPeriodosSemanales" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="100"
                                    CellSpacing="0" GridLines="None" Skin="Bootstrap">
                                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    </ClientSettings>
                                    <MasterTableView DataKeyNames="IdPeriodo" NoMasterRecordsText="No hay registros para mostrar.">
                                        <Columns>
                                            <telerik:GridTemplateColumn HeaderText="Editar" SortExpression="Editar" UniqueName="Editar">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("IdPeriodo") %>' CommandName="cmdEdit" ImageUrl="~/images/action_edit.png" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridBoundColumn DataField="NoPeriodo" ItemStyle-HorizontalAlign="Left" HeaderText="No. Periodo" UniqueName="NoPeriodo" ItemStyle-Width="120px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Cliente" ItemStyle-HorizontalAlign="Left" HeaderText="Cliente" UniqueName="Cliente" ItemStyle-Width="120px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Ejercicio" ItemStyle-HorizontalAlign="Left" HeaderText="Ejercicio" UniqueName="Ejercicio" ItemStyle-Width="120px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Fechainicial" ItemStyle-HorizontalAlign="Left" HeaderText="Fecha Inicial" DataFormatString="{0:dd/MM/yyyy}" UniqueName="Fechainicial" ItemStyle-Width="120px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="Fechafinal" ItemStyle-HorizontalAlign="Left" HeaderText="Fecha Final" DataFormatString="{0:dd/MM/yyyy}" UniqueName="Fechafinal" ItemStyle-Width="120px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FechaPago" ItemStyle-HorizontalAlign="Left" HeaderText="Fecha Pago" UniqueName="FechaPago" ItemStyle-Width="120px">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Inicio de mes" SortExpression="InicioMesBit" UniqueName="InicioMesBit">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgInicioMesBit" runat="server" ImageUrl="~/images/arrow.gif" Visible='<% #Eval("InicioMesBit")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Fin de mes" SortExpression="FinMesBit" UniqueName="FinMesBit">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgFinMesBit" runat="server" ImageUrl="~/images/arrow.gif" Visible='<% #Eval("FinMesBit")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Inicio de ejercicio" SortExpression="InicioEjercicioBit" UniqueName="InicioEjercicioBit">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgInicioEjercicioBit" runat="server" ImageUrl="~/images/arrow.gif" Visible='<% #Eval("InicioEjercicioBit")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="135px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Fin de ejercicio" SortExpression="FinEjercicioBit" UniqueName="FinEjercicioBit">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgFinEjercicioBit" runat="server" ImageUrl="~/images/arrow.gif" Visible='<% #Eval("FinEjercicioBit")%>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="120px" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="Eliminar" Visible="false">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgEliminar" runat="server" CausesValidation="false" CommandArgument='<% #Eval("IdPeriodo")%>' CommandName="cmdDelete" BorderStyle="None" ImageUrl="~/images/action_delete.gif" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">&nbsp;</td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <telerik:RadWindowManager ID="rwConfirmEliminaPeriodo" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
        <asp:Button ID="HiddenButton" Text="" Style="display: none;" runat="server" />
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%"></telerik:RadAjaxLoadingPanel>
</asp:Content>
