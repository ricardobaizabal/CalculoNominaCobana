<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="TipoContrato.aspx.vb" Inherits="CalculoNomina.TipoContrato" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="col-lg-12 b-r">
            <br />
            <br />
            <h3 class="m-t-none m-b">Tipos Contratos</h3>
            <hr class="demo-separator" />
            <br />
            <telerik:RadGrid ID="GridTipoContrato" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="50"
                CellSpacing="0" GridLines="None" Skin="Simple">
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                </ClientSettings>
                <MasterTableView DataKeyNames="id"
                    NoMasterRecordsText="No hay registros para mostrar.">
                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="id" ItemStyle-HorizontalAlign="Left" HeaderText="Folio" UniqueName="folio">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="nombre" ItemStyle-HorizontalAlign="Left" HeaderText="Descripcion" UniqueName="name">
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridTemplateColumn HeaderText="Descripcion" DataField="nombre" SortExpression="nombre" UniqueName="nombre">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("nombre") %>' CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>--%>
                        <%-- <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column"
                                    HeaderText="Eliminar" UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="eliminar" runat="server" CausesValidation="false"
                                            CommandArgument='<% #Eval("id")%>' CommandName="cmdDelete" BorderStyle="None"
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
                    <legend style="padding-right: 10px; padding-left: 10px; color: Black; font-weight: bold; font-size: 12px">Listado de Regimen Contratacion</legend>
                   
                </fieldset>
            </div>
        </div>--%>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
