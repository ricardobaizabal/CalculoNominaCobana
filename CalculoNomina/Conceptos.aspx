<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Conceptos.aspx.vb" Inherits="CalculoNomina.Conceptos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="col-lg-12 b-r">
            <br />
            <br />
            <h3 class="m-t-none m-b">Listado de Concepto</h3>
            <hr class="demo-separator" />
            <br />
            <telerik:RadGrid ID="GridConcepto" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="50"
                CellSpacing="0" GridLines="None" Skin="Bootstrap">
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                </ClientSettings>
                <MasterTableView DataKeyNames="Cvo" NoMasterRecordsText="No hay registros para mostrar.">
                    <Columns>
                        <telerik:GridBoundColumn DataField="Cvo" ItemStyle-HorizontalAlign="Left" HeaderText="Folio" UniqueName="folio">
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridTemplateColumn HeaderText="Concepto" DataField="Concepto" SortExpression="Concepto" UniqueName="Concepto">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("Cvo") %>' CommandName="cmdEdit" Text='<%# Eval("Concepto") %>' CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridBoundColumn DataField="Concepto" ItemStyle-HorizontalAlign="Left" HeaderText="Concepto" UniqueName="concepto">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Clave" ItemStyle-HorizontalAlign="Left" HeaderText="Clave" UniqueName="clave">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Tipo" ItemStyle-HorizontalAlign="Left" HeaderText="Tipo" UniqueName="Tipo">
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridTemplateColumn HeaderText="Eliminar" UniqueName="TemplateColumn">
                            <ItemTemplate>
                                <asp:ImageButton ID="eliminar" runat="server" CausesValidation="false"
                                    CommandArgument='<% #Eval("Cvo")%>' CommandName="cmdDelete" BorderStyle="None"
                                    ImageUrl="~/images/action_delete.gif" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </telerik:GridTemplateColumn>--%>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
        <%--        <div class="demo-containers">
            <div class="demo-container wrapper">
                <fieldset style="border: solid 1px #588a4d; font: sans-serif; margin: 10px 0 0 0">
                    <legend style="padding-right: 10px; padding-left: 10px; color: Black; font-weight: bold; font-size: 12px">Listado de Concepto</legend>

                </fieldset>
            </div>
        </div>--%>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
