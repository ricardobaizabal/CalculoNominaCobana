<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="Ejercicios.aspx.vb" Inherits="CalculoNomina.Ejercicios" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
        <div class="row">
            <div class="col-lg-12">
                <div class="form-group">
                    <div class="col-lg-6">&nbsp;</div>
                    <div class="col-lg-1">
                        <label class="control-label">Ejercicio:</label>
                    </div>
                    <div class="col-lg-2">
                        <telerik:RadNumericTextBox ID="txtEjercicio" runat="server" Value="0" MinValue="0" Width="100px">
                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                        </telerik:RadNumericTextBox>
                    </div>
                    <div class="col-lg-3">
                        <telerik:RadButton ID="btnSave" runat="server" Skin="Bootstrap" Width="120px" Text="Guadar Ejercicio"></telerik:RadButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="form-group">
                    <br />
                    <br />
                    <h3 class="m-t-none m-b">Ejercicios</h3>
                    <hr class="demo-separator" />
                    <br />
                    <telerik:RadGrid ID="GridEjercicio" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="50"
                        CellSpacing="0" GridLines="None" Skin="Simple">
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                        </ClientSettings>
                        <MasterTableView DataKeyNames="id" NoMasterRecordsText="No hay registros para mostrar.">
                            <CommandItemSettings ExportToPdfText="Export to PDF" />
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="CheckBoxTemplateColumn" ItemStyle-Width="20">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="itemcheckbox" runat="server" AutoPostBack="True" OnCheckedChanged="ToggleRowSelection" Checked='<%# IIf(Eval("activo") Is DBNull.Value, "False", Eval("activo"))%>' />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>

                                <telerik:GridBoundColumn DataField="id" ItemStyle-HorizontalAlign="Left" HeaderText="Folio" UniqueName="folio">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="Descripcion" DataField="annio" SortExpression="annio" UniqueName="annio">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("annio") %>' CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column" HeaderText="Seleccionado" UniqueName="TemplateColumn">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="seleccionado" runat="server" CausesValidation="false"
                                            CommandArgument='<% #Eval("id")%>' CommandName="cmdseleccionado" BorderStyle="None"
                                            ImageUrl="~/images/action_check.gif" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
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
                    </telerik:RadGrid>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">&nbsp;</div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <div class="col-lg-10">&nbsp;</div>
                    <div class="col-lg-2">
                        <telerik:RadButton ID="btnModificarEjercicio" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Guadar Activo" Width="120px"></telerik:RadButton>
                    </div>
                </div>
            </div>
        </div>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Bootstrap" EnableShadow="true" Animation="None">
        </telerik:RadWindowManager>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
