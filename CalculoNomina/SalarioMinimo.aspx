<%@ Page Language="vb" MasterPageFile="~/MasterPage.Master" AutoEventWireup="false" CodeBehind="SalarioMinimo.aspx.vb" Inherits="CalculoNomina.SalarioMinimo" %>

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

        #ctl00_ContentPlaceHolder1_grdSalarioMinimo {
            margin-left: 10px !important;
        }

        .container {
            width: 1300px !important;
        }
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
                            <h3 class="m-t-none m-b">S.M. - Salario Mínimo</h3>
                            <hr class="demo-separator" />
                            <br />
                            <telerik:RadGrid ID="grdSalarioMinimo" runat="server" AutoGenerateColumns="False" AllowPaging="false" PageSize="10" Width="100%"
                                CellSpacing="0" GridLines="None" Skin="Bootstrap" AllowAutomaticDeletes="True"
                                AllowAutomaticInserts="True" AllowAutomaticUpdates="True">
                                <ClientSettings Scrolling-AllowScroll="true" AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    <ClientEvents OnGridCreated="GridCreated" />
                                </ClientSettings>
                                <MasterTableView AllowMultiColumnSorting="true" AllowSorting="true" DataKeyNames="id" NoMasterRecordsText="No hay registros para mostrar." Name="SalarioMinimo" Width="100%">
                                    <Columns>
                                        
                                        <telerik:GridBoundColumn HeaderText="AÑO" DataField="anio" UniqueName="anio">
                                            <HeaderStyle Width="65px" />
                                        </telerik:GridBoundColumn>
                                        
                                        <telerik:GridTemplateColumn HeaderText="" UniqueName="">
                                            <HeaderStyle Width="65px" />
                                            <ItemTemplate>
                                                <label>S.M.</label>
                                                <br />
                                                <label class="bajar">S.M.F.</label>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>
                                        
                                        <telerik:GridTemplateColumn HeaderText="ENE" UniqueName="Enero">
                                            <HeaderStyle Width="75px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtEnero_SM" Text='<%#Eval("enero_sm")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                                <br />
                                                <telerik:RadNumericTextBox ID="txtEnero_SMF" Text='<%#Eval("enero_smf")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" CssClass="bajar" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="FEB" UniqueName="Febrero">
                                            <HeaderStyle Width="75px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtFebrero_SM" Text='<%#Eval("febrero_sm")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                                <br />
                                                <telerik:RadNumericTextBox ID="txtFebrero_SMF" Text='<%#Eval("febrero_smf")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" CssClass="bajar" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="MAR" UniqueName="Marzo">
                                            <HeaderStyle Width="75px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtMarzo_SM" Text='<%#Eval("marzo_sm")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                                <br />
                                                <telerik:RadNumericTextBox ID="txtMarzo_SMF" Text='<%#Eval("marzo_smf")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" CssClass="bajar" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="ABR" UniqueName="Abril">
                                            <HeaderStyle Width="75px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtAbril_SM" Text='<%#Eval("abril_sm")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                                <br />
                                                <telerik:RadNumericTextBox ID="txtAbril_SMF" Text='<%#Eval("abril_smf")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" CssClass="bajar" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="MAY" UniqueName="Mayo">
                                            <HeaderStyle Width="75px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtMayo_SM" Text='<%#Eval("mayo_sm")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                                <br />
                                                <telerik:RadNumericTextBox ID="txtMayo_SMF" Text='<%#Eval("mayo_smf")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" CssClass="bajar" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="JUN" UniqueName="Junio">
                                            <HeaderStyle Width="75px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtJunio_SM" Text='<%#Eval("junio_sm")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                                <br />
                                                <telerik:RadNumericTextBox ID="txtJunio_SMF" Text='<%#Eval("junio_smf")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" CssClass="bajar" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="JUL" UniqueName="Julio">
                                            <HeaderStyle Width="75px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtJulio_SM" Text='<%#Eval("julio_sm")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                                <br />
                                                <telerik:RadNumericTextBox ID="txtJulio_SMF" Text='<%#Eval("julio_smf")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" CssClass="bajar" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="AGO" UniqueName="Agosto">
                                            <HeaderStyle Width="75px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtAgosto_SM" Text='<%#Eval("agosto_sm")%>' runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                                <br />
                                                <telerik:RadNumericTextBox ID="txtAgosto_SMF" Text='<%#Eval("agosto_smf")%>' runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" CssClass="bajar" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="SEPT" UniqueName="Septiembre">
                                            <HeaderStyle Width="75px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtSeptiembre_SM" Text='<%#Eval("septiembre_sm")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                                <br />
                                                <telerik:RadNumericTextBox ID="txtSeptiembre_SMF" Text='<%#Eval("septiembre_smf")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" CssClass="bajar" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="OCT" UniqueName="Octubre">
                                            <HeaderStyle Width="75px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtOctubre_SM" Text='<%#Eval("octubre_sm")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                                <br />
                                                <telerik:RadNumericTextBox ID="txtOctubre_SMF" Text='<%#Eval("octubre_smf")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" CssClass="bajar" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="NOV" UniqueName="Noviembre">
                                            <HeaderStyle Width="75px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtNoviembre_SM" Text='<%#Eval("noviembre_sm")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                                <br />
                                                <telerik:RadNumericTextBox ID="txtNoviembre_SMF" Text='<%#Eval("noviembre_smf")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" CssClass="bajar" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn HeaderText="DIC" UniqueName="Diciembre">
                                            <HeaderStyle Width="75px" />
                                            <ItemTemplate>
                                                <telerik:RadNumericTextBox ID="txtDiciembre_SM" Text='<%#Eval("diciembre_sm")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                                <br />
                                                <telerik:RadNumericTextBox ID="txtDiciembre_SMF" Text='<%#Eval("diciembre_smf")%>' DataFormatString="{0:C}" runat="server" Width="50px" Style="text-align: right"
                                                    OnTextChanged="ActualizarSalarioMinimo" Type="Currency" CssClass="bajar" AutoPostBack="true" NumberFormat-GroupSeparator="," MinValue="0">
                                                </telerik:RadNumericTextBox>
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>

                                        <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" HeaderText="Eliminar" UniqueName="TemplateColumn">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="eliminar" runat="server" CausesValidation="false"
                                                    CommandArgument='<% #Eval("id")%>' CommandName="cmdDelete" BorderStyle="None"
                                                    ImageUrl="~/images/action_delete.gif" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </telerik:GridTemplateColumn>

                                    </Columns>
                                </MasterTableView>
                                <ClientSettings AllowKeyboardNavigation="true"></ClientSettings>
                            </telerik:RadGrid>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <label style="margin-left: 2%;">S.M.: Salario Mínimo</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label style="margin-left: 2%;">S.M.F.: Salario Mínimo Frontera</label>
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
