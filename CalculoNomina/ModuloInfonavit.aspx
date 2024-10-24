﻿<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ModuloInfonavit.aspx.vb" Inherits="CalculoNomina.ModuloInfonavit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="col-lg-12 b-r">
            <br />
            <br />
            <h3 class="m-t-none m-b">  INFONAVIT </h3>
            <hr class="demo-separator" />
            <br />
               <asp:Label ID="lblbuscador" runat="server" CssClass="item" Font-Bold="True" Text="Buscar por cliente:"></asp:Label>&nbsp;&nbsp;
                
            <asp:DropDownList ID="ddCliente" runat="server" Width="200px"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnbuscador" runat="server" Text="Buscar" CssClass="rbPrimaryButton" Width="90px" CausesValidation="False" />
                <hr class="demo-separator" />
            <telerik:RadGrid ID="GridEmployees" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="15"
                CellSpacing="0" GridLines="None" Skin="Bootstrap">
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                </ClientSettings>
                <MasterTableView DataKeyNames="id"
                    NoMasterRecordsText="No hay registros para mostrar.">
                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                    <Columns>
                        <telerik:GridTemplateColumn AllowFiltering="False" HeaderStyle-HorizontalAlign="Center" HeaderText="Editar">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" ImageUrl="~/images/action_edit.png" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />

                        </telerik:GridTemplateColumn>
                    
                        <telerik:GridBoundColumn DataField="nombre" HeaderText="Nombre"  SortExpression="nombre" UniqueName="nombre">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="creditoinfonavit" HeaderText="No. Crédito" UniqueName="creditoinfonavit">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="inicio_descuento" HeaderText="Inicio Descuento" UniqueName="inicio_descuento" DataFormatString="{0:MM/dd/yyyy}">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="fin_descuento" HeaderText="Fin Descuento" UniqueName="fin_descuento" DataFormatString="{0:MM/dd/yyyy}">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="nombreDescuento" HeaderText="Tipo Descuento" UniqueName="nombreDescuento" DataFormatString="{0:d}">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="valor" HeaderText="Valor" UniqueName="valor" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
           
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
