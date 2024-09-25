<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="TablaISPTDiaria.aspx.vb" Inherits="CalculoNomina.TablaISPTDiaria" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.QuickStart" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="col-lg-12 b-r">
            <br />
            <br />
            <h3 class="m-t-none m-b">Tarifa Diaria de ISPT</h3>
            <hr class="demo-separator" />
            <br />
            <telerik:RadGrid ID="GridTarifaDiaria" runat="server" AutoGenerateColumns="False" AllowPaging="false" PageSize="11"
                CellSpacing="0" GridLines="None" Skin="Bootstrap" OnBatchEditCommand="GridTarifaDiaria_EditCommand" AllowAutomaticDeletes="True"
            AllowAutomaticInserts="True" OnItemUpdated="GridTarifaDiaria_ItemUpdated"  AllowAutomaticUpdates="True" DataSourceID ="SqlDataSource1" >
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True"  >
                </ClientSettings>
                <MasterTableView CommandItemDisplay="Bottom"  DataKeyNames="IdTarifa" DataSourceID="SqlDataSource1" HorizontalAlign="NotSet"  
                    NoMasterRecordsText="No hay registros para mostrar." EditMode="Batch" AutoGenerateColumns="False" >
                    <CommandItemSettings ShowAddNewRecordButton ="false" />
                    <CommandItemSettings SaveChangesText ="Guardar cambios" />
                    <CommandItemSettings CancelChangesText ="Cancelar" />
                    <CommandItemSettings ShowRefreshButton ="false" />
                    <CommandItemSettings ExportToPdfText="Export to PDF" />

                    <BatchEditingSettings EditType="Cell" />
                    <SortExpressions>
                        <telerik:GridSortExpression FieldName="IdTarifa" SortOrder="Descending" />
                    </SortExpressions>
                    
                    <Columns>
                        <%--<telerik:GridTemplateColumn HeaderText="Folio" DataField="IdTarifa" SortExpression="IdTarifa" UniqueName="IdTarifa">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("IdTarifa") %>' CommandName="cmdEdit" Text='<%# Eval("IdTarifa") %>' CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>--%>
                        <telerik:GridBoundColumn DataField="pLimiteInferior" ItemStyle-HorizontalAlign="Left" SortExpression="pLimiteInferior" HeaderText="Limite Inferior" UniqueName="pLimiteInferior">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="pLimiteSuperior" ItemStyle-HorizontalAlign="Left" SortExpression="pLimiteSuperior" HeaderText="Limite Superior" UniqueName="pLimiteSuperior">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="pCuotaFija" ItemStyle-HorizontalAlign="Left" SortExpression ="pCuotaFija" HeaderText="Cuota Fija" UniqueName="pCuotaFija">
                        </telerik:GridBoundColumn>
                         <telerik:GridBoundColumn DataField="pPorcSobreExcli" ItemStyle-HorizontalAlign="Left" HeaderText="% Excedente" SortExpression ="pPorcSobreExcli" UniqueName="pPorcSobreExcli">
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                 <ClientSettings AllowKeyboardNavigation="true"></ClientSettings>
            </telerik:RadGrid>
            <br />
            <telerik:RadButton ID="btnAgregar" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Agregar"></telerik:RadButton>
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>

     <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=localhost; Initial Catalog=nomilink_cobana; Persist Security Info=True; Trusted_Connection=yes; Max Pool Size=360"
        SelectCommand ="SELECT IdTarifa,  isnull(LimiteInferior,0)as pLimiteInferior, isnull(LimiteSuperior,0)as pLimiteSuperior, isnull(CuotaFija,0)as pCuotaFija, isnull(PorcSobreExcli,0)as pPorcSobreExcli FROM tblTarifaDiaria order by IdTarifa"
        UpdateCommand=" update tblTarifaDiaria set  LimiteInferior=@pLimiteInferior, LimiteSuperior=@pLimiteSuperior, CuotaFija=@pCuotaFija, PorcSobreExcli=@pPorcSobreExcli  WHERE  IdTarifa=@IdTarifa">
        <UpdateParameters>
            <asp:Parameter Name="IdTarifa" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="pLimiteInferior" Type="Decimal"></asp:Parameter>
            <asp:Parameter Name="pLimiteSuperior" Type="Decimal"></asp:Parameter>
            <asp:Parameter Name="pCuotaFija" Type="Decimal"></asp:Parameter>
            <asp:Parameter Name="pPorcSobreExcli" Type="Decimal"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

