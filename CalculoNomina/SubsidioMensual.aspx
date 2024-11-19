<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="SubsidioMensual.aspx.vb" Inherits="CalculoNomina.SubsidioMensual" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="col-lg-12 b-r">
            <br />
            <br />
            <h2 class="m-t-none m-b">TABLA SUBSIDIO MENSUAL</h2>
            <hr class="demo-separator" />
            <br />
            <telerik:RadGrid ID="GridSubsidioMensual" runat="server" AutoGenerateColumns="false" AllowPaging="false" PageSize="50"
                CellSpacing="0" GridLines="None" Skin="Bootstrap" AllowAutomaticDeletes="true" AllowSorting="true"
                AllowAutomaticInserts="True" OnItemUpdated="GridSubsidioMensual_ItemUpdated" AllowAutomaticUpdates="True" DataSourceID="SubsidioMensual">
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                </ClientSettings>
                <MasterTableView CommandItemDisplay="Bottom" DataKeyNames="IdTablaSubsidio" DataSourceID="SubsidioMensual" HorizontalAlign="NotSet"
                    NoMasterRecordsText="No hay registros para mostrar." EditMode="Batch" AutoGenerateColumns="False">
                    <CommandItemSettings ShowAddNewRecordButton="false" />
                    <CommandItemSettings SaveChangesText="Guardar cambios" />
                    <CommandItemSettings CancelChangesText="Cancelar" />
                    <CommandItemSettings ShowRefreshButton="false" />
                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                    <BatchEditingSettings EditType="Cell" />
                    <%--<SortExpressions>
                        <telerik:GridSortExpression FieldName="IdTablaSubsidio" SortOrder="Descending" />
                    </SortExpressions>--%>
                    <Columns>
                        <telerik:GridBoundColumn DataField="pLimiteInferior" ItemStyle-HorizontalAlign="Left" HeaderText="Limite Inferior" UniqueName="pLimiteInferior" DataFormatString="{0:N2}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="pLimiteSuperior" ItemStyle-HorizontalAlign="Left" HeaderText="Limite Superior" UniqueName="LimiteSuperior" DataFormatString="{0:N2}">
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="pCuotaFija" ItemStyle-HorizontalAlign="Left" HeaderText="Subsidio" UniqueName="Subsidio" DataFormatString="{0:N2}">
                        </telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
                <ClientSettings AllowKeyboardNavigation="true"></ClientSettings>
            </telerik:RadGrid>
            <br />
            <telerik:RadButton ID="btnAgregar" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Visible="false" Text="Agregar"></telerik:RadButton>
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>

    <asp:SqlDataSource ID="SubsidioMensual" runat="server" ConnectionString="Data Source=.\SQLEXPRESS03; Initial Catalog=nomilink_cobana; Persist Security Info=True; Trusted_Connection=yes; Max Pool Size=200"
        SelectCommand="SELECT IdTablaSubsidio, isnull(LimiteInferior,0) as pLimiteInferior, isnull(LimiteSuperior,0) as pLimiteSuperior, isnull(Subsidio,0) as pCuotaFija FROM tblTablaSubsidioMensual order by LimiteInferior ASC"
        UpdateCommand="UPDATE tblTablaSubsidioMensual SET LimiteInferior=@pLimiteInferior, LimiteSuperior=@pLimiteSuperior, Subsidio=@pCuotaFija WHERE IdTablaSubsidio=@IdTablaSubsidio">
        <UpdateParameters>
            <asp:Parameter Name="IdTablaSubsidio" Type="Int32"></asp:Parameter>
            <asp:Parameter Name="pLimiteInferior" Type="Decimal"></asp:Parameter>
            <asp:Parameter Name="pLimiteSuperior" Type="Decimal"></asp:Parameter>
            <asp:Parameter Name="pCuotaFija" Type="Decimal"></asp:Parameter>
        </UpdateParameters>
    </asp:SqlDataSource>
</asp:Content>

