<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Utilidades.aspx.vb" Inherits="CalculoNomina.Utilidades" %>

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
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="col-lg-12 b-r">
            <br />
            <br />
            <h3 class="m-t-none m-b">Periodos PTU</h3>
            <hr class="demo-separator" />
            <br />
            <telerik:RadGrid ID="GridPeriodosPTU" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="100" Width="60%"
                CellSpacing="0" GridLines="None" Skin="Bootstrap">
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                </ClientSettings>
                <MasterTableView DataKeyNames="IdPeriodo" NoMasterRecordsText="No hay registros para mostrar.">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="Folio" DataField="IdPeriodo" SortExpression="IdPeriodo" UniqueName="IdPeriodo">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("IdPeriodo") %>' CommandName="cmdEdit" Text='<%# Eval("IdPeriodo") %>' CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Fechainicial" ItemStyle-HorizontalAlign="Left" HeaderText="Fechainicial" DataFormatString="{0:dd/MM/yyyy}" UniqueName="Fechainicial">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="TemplateColumn">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImgEliminar" runat="server" CausesValidation="false" CommandArgument='<% #Eval("IdPeriodo")%>' CommandName="cmdDelete" BorderStyle="None" ImageUrl="~/images/action_delete.gif" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <br />
            <telerik:RadButton ID="btnAgregar" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Agregar"></telerik:RadButton>
        </div>
        <telerik:RadWindowManager ID="rwConfirmEliminaPeriodo" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
        <asp:Button ID="HiddenButton" Text="" Style="display: none;" runat="server" />
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%"></telerik:RadAjaxLoadingPanel>
</asp:Content>