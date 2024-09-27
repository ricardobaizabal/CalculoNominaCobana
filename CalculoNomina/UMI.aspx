<%@ Page Language="vb" MasterPageFile="~/MasterPage.Master" AutoEventWireup="false" CodeBehind="UMI.aspx.vb" Inherits="CalculoNomina.UMI" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.QuickStart" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function GridCreated(sender, args) {
            $('.rgDataDiv').removeAttr('style');
            $('.rgDataDiv').attr('style', 'overflow-x: scroll;');
        }
    </script>
    <style type="text/css">
        .rgMasterTable {
            width: 100%;
            table-layout: inherit;
            empty-cells: show;
            margin-left: 0px;
        }

        #ctl00_ContentPlaceHolder1_grdUMI {
            margin-left: 10px !important;
        }

        /*.container {
            width: 1300px !important;
        }*/
    </style>
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="col-lg-12 b-r">
        <asp:Panel ID="Panel1" runat="server" Width="100%" HorizontalAlign="NotSet" Visible="true">
            <fieldset>
                <table style="width: 100%;" border="0">
                    <tr>
                        <td>
                            <br />
                            <br />
                            <h3 class="m-t-none m-b">UMI - Unidad de Mixta Infonavit</h3>
                            <hr class="demo-separator" />
                            <br />
                            <telerik:RadGrid ID="grdUMI" runat="server" AutoGenerateColumns="False" AllowPaging="false" PageSize="10" Width="100%"
                                CellSpacing="0" GridLines="None" Skin="Bootstrap" AllowAutomaticDeletes="True"
                                AllowAutomaticInserts="True" AllowAutomaticUpdates="True">
                                <ClientSettings Scrolling-AllowScroll="true" AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <ClientEvents OnGridCreated="GridCreated" />
                                </ClientSettings>
                                <MasterTableView AllowMultiColumnSorting="true" AllowSorting="true" DataKeyNames="id" NoMasterRecordsText="No hay registros para mostrar." Name="UMI" Width="100%">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="AÑO" DataField="anio" UniqueName="anio">
                                            <HeaderStyle Width="65px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="ENE" UniqueName="Enero">
                                            <HeaderStyle Width="65px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtEnero" Text='<%#Eval("enero")%>' DataFormatString="{0:C}" runat="server" Width="62px" Style="text-align: right"
                                                    OnTextChanged="ActualizarUMI" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="FEB" UniqueName="Febrero">
                                            <HeaderStyle Width="65px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtFebrero" Text='<%#Eval("febrero")%>' DataFormatString="{0:C}" runat="server" Width="62px" Style="text-align: right"
                                                    OnTextChanged="ActualizarUMI" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="MAR" UniqueName="Marzo">
                                            <HeaderStyle Width="65px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtMarzo" Text='<%#Eval("marzo")%>' DataFormatString="{0:C}" runat="server" Width="62px" Style="text-align: right"
                                                    OnTextChanged="ActualizarUMI" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="ABR" UniqueName="Abril">
                                            <HeaderStyle Width="65px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtAbril" Text='<%#Eval("abril")%>' DataFormatString="{0:C}" runat="server" Width="62px" Style="text-align: right"
                                                    OnTextChanged="ActualizarUMI" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="MAY" UniqueName="Mayo">
                                            <HeaderStyle Width="65px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtMayo" Text='<%#Eval("mayo")%>' DataFormatString="{0:C}" runat="server" Width="62px" Style="text-align: right"
                                                    OnTextChanged="ActualizarUMI" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="JUN" UniqueName="Junio">
                                            <HeaderStyle Width="65px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtJunio" Text='<%#Eval("junio")%>' DataFormatString="{0:C}" runat="server" Width="62px" Style="text-align: right"
                                                    OnTextChanged="ActualizarUMI" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="JUL" UniqueName="Julio">
                                            <HeaderStyle Width="65px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtJulio" Text='<%#Eval("julio")%>' DataFormatString="{0:C}" runat="server" Width="62px" Style="text-align: right"
                                                    OnTextChanged="ActualizarUMI" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="AGO" UniqueName="Agosto">
                                            <HeaderStyle Width="65px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtAgosto" Text='<%#Eval("agosto")%>' runat="server" Width="62px" Style="text-align: right"
                                                    OnTextChanged="ActualizarUMI" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="SEPT" UniqueName="Septiembre">
                                            <HeaderStyle Width="65px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtSeptiembre" Text='<%#Eval("septiembre")%>' DataFormatString="{0:C}" runat="server" Width="62px" Style="text-align: right"
                                                    OnTextChanged="ActualizarUMI" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="OCT" UniqueName="Octubre">
                                            <HeaderStyle Width="65px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtOctubre" Text='<%#Eval("octubre")%>' DataFormatString="{0:C}" runat="server" Width="62px" Style="text-align: right"
                                                    OnTextChanged="ActualizarUMI" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="NOV" UniqueName="Noviembre">
                                            <HeaderStyle Width="65px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtNoviembre" Text='<%#Eval("noviembre")%>' DataFormatString="{0:C}" runat="server" Width="62px" Style="text-align: right"
                                                    OnTextChanged="ActualizarUMI" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="DIC" UniqueName="Diciembre">
                                            <HeaderStyle Width="65px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtDiciembre" Text='<%#Eval("diciembre")%>' DataFormatString="{0:C}" runat="server" Width="62px" Style="text-align: right"
                                                    OnTextChanged="ActualizarUMI" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <%--<telerik:GridTemplateColumn HeaderText="Mens" UniqueName="Mensual">
                                            <HeaderStyle Width="65px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtMensual" Text='<%#Eval("mensual")%>' DataFormatString="{0:C}" runat="server" Width="73px" Style="text-align: right"
                                                    OnTextChanged="ActualizarUMI" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>--%>

                                        <%--<telerik:GridTemplateColumn HeaderText="Anual" UniqueName="Anual">
                                            <HeaderStyle Width="65px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtAnual" Text='<%#Eval("anual")%>' DataFormatString="{0:C}" runat="server" Width="73px" Style="text-align: right"
                                                    OnTextChanged="ActualizarUMI" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>--%>

                                        <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" HeaderText="Eliminar" UniqueName="TemplateColumn">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="eliminar" runat="server" CausesValidation="false"
                                                    CommandArgument='<% #Eval("id")%>' CommandName="cmdDelete" BorderStyle="None"
                                                    ImageUrl="~/images/action_delete.gif" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="62px" />
                                        </telerik:GridTemplateColumn>
                                    </Columns>
                                </MasterTableView>
                                <ClientSettings AllowKeyboardNavigation="true"></ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%;" border="0">
                    <tr>
                        <td style="height: 15px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: left !important;">
                            <telerik:RadButton ID="btnAgregar" AutoPostBack="true" RenderMode="Lightweight" runat="server" Visible="true" Width="120px" Skin="Bootstrap" Text="Agregar Año"></telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px;">&nbsp;</td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
    <br />
    <div class="col-lg-12 b-r">
        <asp:Panel ID="Panel2" runat="server" Width="100%" HorizontalAlign="NotSet" Visible="false">
            <fieldset>
                <br />
                <table style="width: 100%;" border="0">
                    <tr>
                        <td colspan="3" style="height: 5px;">
                            <asp:Label ID="lblNombre" runat="server" CssClass="item" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 4%">
                            <label>Año</label>
                        </td>
                        <td style="width: 22%">
                            <telerik:RadNumericTextBox ID="txtAnio" runat="server" Width="65%" CssClass="AgregaAño" NumberFormat-DecimalDigits="0">
                            </telerik:RadNumericTextBox>&nbsp;
                            <asp:RequiredFieldValidator ID="valAnio" runat="server" ControlToValidate="txtAnio" ValidationGroup="vgAnio" CssClass="item" ForeColor="Red" Text="Requerido"></asp:RequiredFieldValidator>
                        </td>
                        <td style="text-align: left">
                            <telerik:RadButton ID="btnSave" AutoPostBack="true" RenderMode="Lightweight" runat="server" ValidationGroup="vgAnio" Skin="Bootstrap" Text="Guardar"></telerik:RadButton>
                            &nbsp;&nbsp;&nbsp;
                            <telerik:RadButton ID="btnCancel" AutoPostBack="true" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Cancelar"></telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="height: 5px;">
                            <asp:HiddenField ID="InsertOrUpdate" runat="server" Value="0" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </asp:Panel>
    </div>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
