<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="TablaISPTMensual.aspx.vb" Inherits="CalculoNomina.TablaISPTMensual" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="col-lg-12 b-r">
            <br />
            <br />
            <h3 class="m-t-none m-b">Tarifa Mensual de ISPT</h3>
            <hr class="demo-separator" />
            <br />
            <telerik:RadGrid ID="GridTarifaMensual" runat="server" AutoGenerateColumns="False" AllowPaging="False" PageSize="11"
                CellSpacing="0" GridLines="None" Skin="Bootstrap">
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                </ClientSettings>
                <MasterTableView DataKeyNames="IdTarifa"
                    NoMasterRecordsText="No hay registros para mostrar.">
                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="Folio" DataField="IdTarifa" SortExpression="IdTarifa" UniqueName="IdTarifa">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("IdTarifa") %>' CommandName="cmdEdit" Text='<%# Eval("IdTarifa") %>' CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn DataField="LimiteInferior" ItemStyle-HorizontalAlign="Left" HeaderText="Limite Inferior" UniqueName="LimiteInferior">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="LimiteSuperior" ItemStyle-HorizontalAlign="Left" HeaderText="Limite Superior" UniqueName="LimiteSuperior">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="CuotaFija" ItemStyle-HorizontalAlign="Left" HeaderText="Cuota Fija" UniqueName="CuotaFija">
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="PorcSobreExcli" ItemStyle-HorizontalAlign="Left" HeaderText="" UniqueName="PorcSobreExcli">
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
            <br />
            <telerik:RadButton ID="btnAgregar" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Agregar"></telerik:RadButton>
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>

