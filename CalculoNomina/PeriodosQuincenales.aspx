<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="PeriodosQuincenales.aspx.vb" Inherits="CalculoNomina.PeriodosQuincenales" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
    <script type="text/javascript">
        function confirmCallbackFn(arg) {
            if (arg) //the user clicked OK
            {
                __doPostBack("<%=HiddenButton.UniqueID %>", "");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
    <asp:HiddenField ID="ejercicioId" runat="server" Value="0" Visible="False" />
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="col-lg-12 b-r">
            <br />
            <br />
            <h3 class="m-t-none m-b">Periodos Quincenales</h3>
            <hr class="demo-separator" />
            <br />
            <table style="width: 50%;">
                <tr>
                    <td style="text-align: right;">
                        <telerik:RadButton ID="btnAgregar" RenderMode="Lightweight" runat="server" Skin="Bootstrap" CssClass="rbPrimaryButton" Text="Agregar"></telerik:RadButton>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadGrid ID="GridPeriodosQuincenales" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="100" Width="100%"
                            CellSpacing="0" GridLines="None" Skin="Bootstrap">
                            <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                            </ClientSettings>
                            <MasterTableView DataKeyNames="IdPeriodo" NoMasterRecordsText="No hay registros para mostrar.">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="IdPeriodo" ItemStyle-HorizontalAlign="Left" HeaderText="No. Periodo" UniqueName="NoPeriodo">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Fechainicial" ItemStyle-HorizontalAlign="Left" HeaderText="Fecha Inicial" DataFormatString="{0:dd/MM/yyyy}" UniqueName="Fechainicial">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Fechafinal" ItemStyle-HorizontalAlign="Left" HeaderText="Fecha Final" DataFormatString="{0:dd/MM/yyyy}" UniqueName="Fechafinal">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FechaPago" ItemStyle-HorizontalAlign="Left" HeaderText="Fecha Pago" UniqueName="FechaPago">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Dias" ItemStyle-HorizontalAlign="Left" HeaderText="Dias" UniqueName="Dias">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn HeaderText="Editar" SortExpression="Editar" UniqueName="Editar">
                                        <ItemTemplate>
                                            <%--<asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("IdPeriodo") %>' CommandName="cmdEdit" Text="Editar" CausesValidation="false"></asp:LinkButton>--%>
                                            <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("IdPeriodo") %>' CommandName="cmdEdit" ImageUrl="~/images/action_edit.png" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="TemplateColumn">
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
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
        <telerik:RadWindowManager ID="rwConfirmEliminaPeriodo" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
        <asp:Button ID="HiddenButton" Text="" Style="display: none;" runat="server" />
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%"></telerik:RadAjaxLoadingPanel>
</asp:Content>