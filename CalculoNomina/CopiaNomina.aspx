<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="CopiaNomina.aspx.vb" Inherits="CalculoNomina.CopiaNomina" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="ibox-content">
            <div class="row">
                <div class="col-lg-8">
                   <%-- <h3 class="m-t-none m-b">Generacion De Nomina Normal</h3>
                    <telerik:RadGrid ID="GridPeriodosSemanales" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
                        CellSpacing="0" GridLines="None" Skin="Bootstrap">
                        <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                        </ClientSettings>
                        <MasterTableView DataKeyNames="IdPeriodo"
                            NoMasterRecordsText="No hay registros para mostrar.">
                            <CommandItemSettings ExportToPdfText="Export to PDF" />
                            <Columns>
                                <telerik:GridTemplateColumn HeaderText="Periodo" DataField="IdPeriodo" SortExpression="IdPeriodo" UniqueName="IdPeriodo">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("IdPeriodo") %>' CommandName="cmdEdit" Text='<%# Eval("IdPeriodo") %>' CausesValidation="false"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="Fechainicial" ItemStyle-HorizontalAlign="Left" HeaderText="Fechainicial" UniqueName="Fechainicial">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Fechafinal" ItemStyle-HorizontalAlign="Left" HeaderText="Fechafinal" UniqueName="Fechafinal">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Dias" ItemStyle-HorizontalAlign="Left" HeaderText="Dias" UniqueName="Dias">
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>--%>
                </div>
                <div class="col-lg-4">
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-6 control-label">No. Periodo</label>

                                <div class="col-lg-6">
                                    <telerik:RadNumericTextBox ID="txtPeriodo" runat="server" Value="0" MinValue="0" Width="100px" Enabled="false">
                                        <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                    </telerik:RadNumericTextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
                                <label class="col-lg-6 control-label">Fecha inicial</label>

                                <div class="col-lg-6">
                                    <telerik:RadDatePicker ID="calFechaInt" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-6 control-label">Fecha final</label>

                                <div class="col-lg-6">
                                    <telerik:RadDatePicker ID="calFechaFin" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-6 control-label">Dias</label>

                                <div class="col-lg-6">
                                    <telerik:RadNumericTextBox ID="txtDias" runat="server" Value="0" MinValue="0" Width="100px">
                                        <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                    </telerik:RadNumericTextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-6 control-label">&nbsp;&nbsp;&nbsp;</label>
                                <div class="col-lg-6">
                                    <telerik:RadButton ID="BtnGeneraNomina" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Genera nomina(calculo sin incidencias)" Width="100px"></telerik:RadButton>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-6 control-label">&nbsp;&nbsp;&nbsp;</label>
                                <div class="col-lg-6">
                                    <telerik:RadButton ID="BtnModificacionDeNomina" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Modificacion de nomina (inciidencias)" Width="100px"></telerik:RadButton>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-6 control-label">&nbsp;&nbsp;&nbsp;</label>
                                <div class="col-lg-6">
                                    <telerik:RadButton ID="BtnBorrarNomina" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Borrar nomina" Width="100px"></telerik:RadButton>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-6 control-label">&nbsp;&nbsp;&nbsp;</label>
                                <div class="col-lg-6">
                                    <telerik:RadButton ID="BtnGenerarNominaElectronica" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Generar nomina electronica(sobres)" Width="100px"></telerik:RadButton>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-6 control-label">&nbsp;&nbsp;&nbsp;</label>
                                <div class="col-lg-6">
                                    <telerik:RadButton ID="BtnTimbrarNominaSemanal" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Timbrar nomina" Width="100px"></telerik:RadButton>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-6 control-label">&nbsp;&nbsp;&nbsp;</label>
                                <div class="col-lg-6">
                                    <telerik:RadButton ID="BtnGenerarPdf" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Generar pdf's" Width="100px"></telerik:RadButton>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>


