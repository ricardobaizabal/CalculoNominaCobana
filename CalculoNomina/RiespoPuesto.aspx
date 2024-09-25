<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="RiespoPuesto.aspx.vb" Inherits="CalculoNomina.RiespoPuesto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <%--        <div class="col-sm-6 b-r">
            <h1 class="m-t-none m-b">Riesgo del puesto</h1>
            <hr class="demo-separator" />
            <br />
            <div class="form-group">
                <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
                <label>Nombre:</label>
                <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtNombre" Width="300px" class="form-control"></telerik:RadTextBox><br />
            </div>
            <div class="button-wrappers">
                <telerik:RadButton ID="btnSave" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Guadar"></telerik:RadButton>
            </div>
        </div>--%>
        <div class="col-lg-12 b-r">
            <br />
            <br />
            <h3 class="m-t-none m-b">Riesgo del puesto</h3>
            <hr class="demo-separator" />
            <br />
            <telerik:RadGrid ID="GridRiesgoPuesto" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="50"
                CellSpacing="0" GridLines="None" Skin="Simple">
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                </ClientSettings>
                <MasterTableView DataKeyNames="id"
                    NoMasterRecordsText="No hay registros para mostrar.">
                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="id" ItemStyle-HorizontalAlign="Left" HeaderText="Folio" UniqueName="folio">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="nombre" ItemStyle-HorizontalAlign="Left" HeaderText="Descripción" UniqueName="name">
                        </telerik:GridBoundColumn>
                        <%--<telerik:GridTemplateColumn HeaderText="Descripcion" DataField="Descripcion" SortExpression="nombre" UniqueName="nombre">
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
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
