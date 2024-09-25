<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Deducciones.aspx.vb" Inherits="CalculoNomina.Deducciones" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />

    <style>
         .dropClaveSAT
         {
             width: 230px !important;
             overflow: hidden;
             white-space: nowrap;
             text-overflow: ellipsis;
             border: solid 1px #cccccc;
             border-radius: 3px;
             height: 35px;
         }

         .dropClaveSAT:active, .dropClaveSAT:focus {
            box-shadow: 0px 0px 10px #005aff80;
            border: solid 1px #005aff80;
         }

         .dropClaveSAT option 
         {
             width: 230px !important;
             overflow: hidden;
             white-space: nowrap;
             text-overflow: ellipsis;
         }

         .InputTextLarge {
            width: 230px !important;
         }

     </style>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="col-lg-12 b-r">
            <br />
            <br />
            <h3 class="m-t-none m-b">Deducciones</h3>
            <hr class="demo-separator" />
            <br />
            <telerik:RadGrid ID="GridDeducciones" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="20"
                CellSpacing="0" GridLines="None" Skin="Simple">
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                </ClientSettings>
                <MasterTableView DataKeyNames="Cvo"
                    NoMasterRecordsText="No hay registros para mostrar.">
                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                    <Columns>
                        <telerik:GridTemplateColumn DataField="Cvo" ItemStyle-HorizontalAlign="Left" HeaderText="Folio" UniqueName="cvo">
                             <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("Cvo") %>' CommandName="cmdEdit" Text='<%# Eval("Cvo") %>' CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="Clave" ItemStyle-HorizontalAlign="Left" HeaderText="Clave" UniqueName="clave">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Concepto" ItemStyle-HorizontalAlign="Left" HeaderText="Concepto" UniqueName="name">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="ClaveSAT" ItemStyle-HorizontalAlign="Left" HeaderText="Clave SAT" UniqueName="clave_sat">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Descripcion" ItemStyle-HorizontalAlign="Left" HeaderText="Descripcion SAT" UniqueName="desc_sat">
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" HeaderText="Eliminar" UniqueName="TemplateColumn">
                            <ItemTemplate>
                                <asp:ImageButton ID="eliminar" runat="server" CausesValidation="false"
                                CommandArgument='<% #Eval("Cvo")%>' CommandName="cmdDelete" BorderStyle="None"
                                ImageUrl="~/images/action_delete.gif" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </telerik:GridTemplateColumn>
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

        <asp:Panel ID="panelDeducciones" runat="server" Visible="False">
            <asp:HiddenField ID="FiniquitoId" runat="server" Value="0" />
                <table border="0" cellpadding="3" width="100%">
                    <tr style="vertical-align: top;">
                        <td colspan="8" class="ulwrapperblue">
                            <asp:Label ID="Label8" runat="server" CssClass="TitleBaja" Font-Bold="True" Text="Sección de Deducciones"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 10px;">
                        <td colspan="7">&nbsp;</td>
                    </tr>
                    <tr style="vertical-align: top;">
                        <td style="width: 35%;">
                            <asp:Label ID="lblConcepto" runat="server" CssClass="item" Font-Bold="True" Text="Concepto:"></asp:Label><br />
                            <telerik:RadTextBox ID="txtConcepto" runat="server" ValidationGroup="vCodigo" CssClass="InputTextLarge"></telerik:RadTextBox><br /><br />
                            <asp:Label ID="lblSat" runat="server" CssClass="item" Font-Bold="True" Text="Clave SAT:"></asp:Label><br />
                            <asp:DropDownList ID="OptClaveSAT" runat="server" CssClass="item dropClaveSAT" Font-Bold="True">
                             </asp:DropDownList>
                        </td>
                        <td style="width: 65%;">
                            <asp:Label ID="lblClave" runat="server" CssClass="item" Font-Bold="True" Text="Clave:"></asp:Label><br />
                            <telerik:RadTextBox ID="txtClave" runat="server" ValidationGroup="vPuesto"></telerik:RadTextBox>
                        </td>                
                     </tr>
                     <tr style="vertical-align: top;">                     
                        <td style="width: 25%;">&nbsp;</td>
                     </tr>             
                 </table>
                 <table style="width: 100%;">
                    <tr style="vertical-align: top;">
                        <td width="100%" align="left">
                            <asp:Button ID="btnSaveDeduccion" runat="server" Text="Guardar" CssClass="rbPrimaryButton" CausesValidation="False" />&nbsp;&nbsp;
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="rbPrimaryButton" CausesValidation="False" />&nbsp;
                            <asp:Label ID="lblMensajeDeduccion" runat="server" CssClass="item"></asp:Label>
                        </td>
                    </tr>
                 </table>
            </asp:Panel>

    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
