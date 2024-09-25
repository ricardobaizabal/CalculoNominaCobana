<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Puesto.aspx.vb" Inherits="CalculoNomina.Puesto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="col-lg-12 b-r">
            <br />
            <br />
            <h3 class="m-t-none m-b">Puestos</h3>
            <hr class="demo-separator" />
            <br />
            <telerik:RadGrid ID="GridPuesto" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="50"
                CellSpacing="0" GridLines="None" Skin="Simple">
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                </ClientSettings>
                <MasterTableView DataKeyNames="id"
                    NoMasterRecordsText="No hay registros para mostrar.">
                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                    <Columns>
                        <telerik:GridTemplateColumn DataField="id" ItemStyle-HorizontalAlign="Left"  HeaderText="Folio" UniqueName="folio">
                          <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("id") %>' CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="nombre" ItemStyle-HorizontalAlign="Left" HeaderText="Descripción" UniqueName="name">
                        </telerik:GridBoundColumn>
                           <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" HeaderText="Eliminar" UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="eliminar" runat="server" CausesValidation="false"
                                            CommandArgument='<% #Eval("id")%>' CommandName="cmdDelete" BorderStyle="None"
                                            ImageUrl="~/images/action_delete.gif" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                </telerik:GridTemplateColumn>
                       <%-- <telerik:GridTemplateColumn HeaderText="Descripcion" DataField="nombre" SortExpression="nombre" UniqueName="nombre">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("nombre") %>' CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>--%>
                        <%--                                <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column"
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
                 <table style="width: 100%;">
                                        <tr style="vertical-align: top;">
                                          
                                            <td width="25%" align="right">
                                                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="rbPrimaryButton" CausesValidation="False" />&nbsp;&nbsp;
                                            </td>
                                             
                                        </tr>
                                    </table>
        </div>
        <asp:Panel ID="panelPuesto" runat="server" Visible="False">
                                    <asp:HiddenField ID="FiniquitoId" runat="server" Value="0" />
                                    <table border="0" cellpadding="3" width="100%">
                                        <tr style="vertical-align: top;">
                                            <td colspan="8" class="ulwrapperblue">
                                                <asp:Label ID="Label8" runat="server" CssClass="TitleBaja" Font-Bold="True" Text="Sección de Puestos"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="height: 10px;">
                                            <td colspan="7">&nbsp;</td>
                                        </tr>
                                        <tr style="vertical-align: top;">
                                             <td style="width: 25%;">
                                        <asp:Label ID="lblCodigo" runat="server" CssClass="item" Font-Bold="True" Text="Codigo:"></asp:Label>
                                    </td>
                                    <td style="width: 25%;">
                                        <telerik:RadTextBox ID="txtCodigo" runat="server" ValidationGroup="vCodigo"></telerik:RadTextBox>
                                    </td>
                                                   <td style="width: 25%;">
                                        <asp:Label ID="lblPuesto" runat="server" CssClass="item" Font-Bold="True" Text="Puesto:"></asp:Label>
                                    </td>
                                    <td style="width: 25%;">
                                        <telerik:RadTextBox ID="txtPuesto" runat="server" ValidationGroup="vPuesto"></telerik:RadTextBox>
                                    </td>
                                        
                                            <td style="width: 25%;">&nbsp;</td>
                                          
                                        </tr>
                                        <tr style="vertical-align: top;">
                                        
                                          
                                            <td style="width: 25%;">&nbsp;</td>
                                        </tr>
                                    
                                     
                                    </table>
                                    <table style="width: 100%;">
                                        <tr style="vertical-align: top;">
                                            <td width="75%" align="left">
                                                <asp:Label ID="lblMensajepuesto" runat="server" CssClass="item"></asp:Label>
                                            </td>
                                            <td width="25%" align="right">
                                                <asp:Button ID="btnSavePuesto" runat="server" Text="Guardar" CssClass="rbPrimaryButton" CausesValidation="False" />&nbsp;&nbsp;
                                            </td>
                                             <td width="25%" align="right">
                                                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="rbPrimaryButton" CausesValidation="False" />&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
