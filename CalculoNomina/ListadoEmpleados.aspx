<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="ListadoEmpleados.aspx.vb" Inherits="CalculoNomina.ListadoEmpleados" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
    <script type="text/javascript">
        function openRadWindow(id) {
            var oWnd = radopen("ModificacionNominaSemanalNormal.aspx?id=" + id, "IncidenciasWindow");
            oWnd.SetWidth(1179);
            oWnd.SetHeight(698);
            oWnd.center();
        }
    </script>
    <script type="text/javascript">
        function OnRequestStart(target, arguments) {
            if ((arguments.get_eventTarget().indexOf("lnkEdit") > -1)) {
                arguments.set_enableAjax(false);
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1" ClientEvents-OnRequestStart="OnRequestStart">
        <div class="col-lg-12 b-r">
            <br />
            <br />
            <h3 class="m-t-none m-b">Listado de Empleados</h3>
            <hr class="demo-separator" />
            <br />
            <telerik:RadGrid ID="GridEmployees" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" AllowCustomPaging="False" AllowMultiRowSelection="false" AllowSorting="True"
                CellSpacing="0" GridLines="None" Skin="Bootstrap" Width="100%">
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                </ClientSettings>
                <MasterTableView DataKeyNames="id" Name="Employees"
                    NoMasterRecordsText="No hay registros para mostrar.">
                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                    <Columns>
                       <%-- <telerik:GridBoundColumn DataField="contratoid" HeaderText="Clave Contrato" UniqueName="contratoid"></telerik:GridBoundColumn>--%>
                        <telerik:GridBoundColumn DataField="id" HeaderText="Clave Emp" UniqueName="id"></telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn HeaderText="Nombre" DataField="nombre" SortExpression="nombre" UniqueName="nombre">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("nombre") %>' CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>

                       <%-- <telerik:GridBoundColumn DataField="municipio" HeaderText="Municipio" UniqueName="municipio">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="departamento" HeaderText="Departamento" UniqueName="departamento">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="no_imss" HeaderText="No. Seguro Social" UniqueName="no_imss">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="fecha_alta" HeaderText="Fecha alta" UniqueName="fecha_alta" DataFormatString="{0:d}">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="salario" HeaderText="Sueldo Mensual" UniqueName="salario" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Right">
                        </telerik:GridBoundColumn>

                        <telerik:GridBoundColumn DataField="estatus" HeaderText="Estatus" UniqueName="estatus">
                        </telerik:GridBoundColumn>--%>

                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </telerik:RadAjaxPanel>

    <telerik:RadWindow ID="IncidenciasWindow" runat="server" Modal="true" CenterIfModal="true" AutoSize="false" VisibleOnPageLoad="false" Behaviors="Close" Width="1150" Height="900">
        <ContentTemplate>

            <div class="row">
                <div class="row">
                    <div class="col-md-2">
                        <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
                        <asp:HiddenField ID="periodoId" runat="server" Value="0" Visible="False" />
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-7 control-label">Ejercicio</label>

                                <div class="col-lg-5">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtEjercicio" Width="300px" class="form-control" Enabled="false"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-10">&nbsp;</div>

                    <div class="col-md-12">&nbsp;</div>

                    <div class="col-md-2">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-7 control-label">No. periodo</label>

                                <div class="col-lg-5">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtNumPeriodo" Width="80px" class="form-control" Enabled="false"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-5 control-label">F. inicial</label>

                                <div class="col-lg-7">
                                    <telerik:RadDatePicker ID="calFechaInt" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server" Width="120px"></telerik:RadDatePicker>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">F. final</label>

                                <div class="col-lg-8">
                                    <telerik:RadDatePicker ID="calFechaFin" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server" Width="120px"></telerik:RadDatePicker>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-1">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-7 control-label">Dias</label>

                                <div class="col-lg-5">
                                    <telerik:RadNumericTextBox ID="txtDia" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="0" Width="50px"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-6 control-label">F. ingreso</label>

                                <div class="col-lg-6">
                                    <telerik:RadDatePicker ID="calFechaIngreso" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server" Width="120px"></telerik:RadDatePicker>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">&nbsp;</div>

                    <div class="col-md-2">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-7 control-label">No. Empleado</label>

                                <div class="col-lg-5">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtNumEmpleado" Width="100%" class="form-control" Enabled="false"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-1">&nbsp;</div>

                    <div class="col-md-9">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Nombre</label>

                                <div class="col-lg-8">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtNombre" Width="435px" class="form-control" Enabled="false"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-2">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-7 control-label">Puesto &nbsp;&nbsp;</label>

                                <div class="col-lg-5">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtPuesto" Width="270px" class="form-control" Enabled="false"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">&nbsp;</div>

                    <div class="col-md-4">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-3 control-label">Rrc</label>

                                <div class="col-lg-9">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtRrc" Width="200px" class="form-control" Enabled="false"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-3 control-label">Imss</label>

                                <div class="col-lg-9">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtImssNom" Width="150px" class="form-control" Enabled="false"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-2">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-7 control-label">Regimen Contrato</label>

                                <div class="col-lg-5">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtRegimencontrat" Width="205px" class="form-control" Enabled="false"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">&nbsp;</div>

                    <div class="col-md-4">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-3 control-label">Cta. Diaria</label>

                                <div class="col-lg-9">
                                    <telerik:RadNumericTextBox ID="txtCuotaDiaria1" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="0" Width="100px" AutoPostBack="false"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-3 control-label">Integ. imss</label>

                                <div class="col-lg-9">
                                    <telerik:RadNumericTextBox ID="txtIMSS1" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="2" Width="100px" AutoPostBack="false"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-lg-6">
                                <telerik:RadGrid ID="GridPercepciones" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="5"
                                    CellSpacing="0" GridLines="None" Skin="Bootstrap">
                                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    </ClientSettings>
                                    <MasterTableView DataKeyNames="id"
                                        NoMasterRecordsText="No hay registros para mostrar.">
                                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="id" ItemStyle-HorizontalAlign="Left" HeaderText="Folio" UniqueName="folio">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Descripcion" DataField="nombre" SortExpression="nombre" UniqueName="nombre">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("id") %>' CommandName="cmdEdit" Text='<%# Eval("nombre") %>' CausesValidation="false"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column"
                                                HeaderText="Eliminar" UniqueName="TemplateColumn">
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
                            <div class="col-lg-6">
                                <telerik:RadGrid ID="GridDeduciones" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="50"
                                    CellSpacing="0" GridLines="None" Skin="Bootstrap">
                                    <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                                    </ClientSettings>
                                    <MasterTableView DataKeyNames="IdDepartamento"
                                        NoMasterRecordsText="No hay registros para mostrar.">
                                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="IdDepartamento" ItemStyle-HorizontalAlign="Left" HeaderText="Folio" UniqueName="folio">
                                            </telerik:GridBoundColumn>
                                            <telerik:GridTemplateColumn HeaderText="Descripcion" DataField="Descripcion" SortExpression="nombre" UniqueName="nombre">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("IdDepartamento") %>' CommandName="cmdEdit" Text='<%# Eval("Descripcion") %>' CausesValidation="false"></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn FilterControlAltText="Filter TemplateColumn column"
                                                HeaderText="Eliminar" UniqueName="TemplateColumn">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="eliminar" runat="server" CausesValidation="false"
                                                        CommandArgument='<% #Eval("IdDepartamento")%>' CommandName="cmdDelete" BorderStyle="None"
                                                        ImageUrl="~/images/action_delete.gif" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                            <div class="col-lg-12">&nbsp;</div>
                            <div class="col-lg-6">
                                <div class="col-md-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-lg-6 control-label">Percepciones</label>

                                            <div class="col-lg-6">
                                                <telerik:RadNumericTextBox ID="txtPercepcione" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="0" Width="100px" AutoPostBack="false"></telerik:RadNumericTextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-lg-6 control-label">Gravado I.S.R.</label>

                                            <div class="col-lg-6">
                                                <telerik:RadNumericTextBox ID="txtGravadISR" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="0" Width="100px" AutoPostBack="false"></telerik:RadNumericTextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-lg-6 control-label">Exento I.S.R.</label>

                                            <div class="col-lg-6">
                                                <telerik:RadNumericTextBox ID="txtExentISR" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="0" Width="100px" AutoPostBack="false"></telerik:RadNumericTextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6">
                                <div class="col-md-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-lg-6 control-label">Deducciones</label>

                                            <div class="col-lg-6">
                                                <telerik:RadNumericTextBox ID="txtDeduccione" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="2" Width="100px" AutoPostBack="false"></telerik:RadNumericTextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-lg-6 control-label">Neto a pagar</label>

                                            <div class="col-lg-6">
                                                <telerik:RadNumericTextBox ID="txtNetoPagar1" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="2" Width="100px" AutoPostBack="false"></telerik:RadNumericTextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12">&nbsp;</div>
                            <div class="col-lg-12">
                                <div class="col-md-6">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="i-checks">
                                                &nbsp;
                                                <label>
                                                    <input type="checkbox"><i></i>Percepciones
                                                </label>
                                                &nbsp;
                                                <label>
                                                    <input type="checkbox"><i></i>Deducciones
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">&nbsp;</div>
                            </div>
                            <div class="col-lg-6">
                                <div class="col-md-6">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-lg-4 control-label">Concepto</label>

                                            <div class="col-lg-8">
                                                <asp:DropDownList ID="cmbConcepto" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-lg-2 control-label">&nbsp;</label>

                                            <div class="col-lg-10">
                                                <telerik:RadButton ID="btnAgregarC" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Agregar concep" Width="100%"></telerik:RadButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-lg-6 control-label">Unidad</label>

                                            <div class="col-lg-6">
                                                <telerik:RadNumericTextBox ID="txtUnidad1" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="0" Width="100px" AutoPostBack="false"></telerik:RadNumericTextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-lg-5 control-label">Importe</label>

                                            <div class="col-lg-7">
                                                <telerik:RadNumericTextBox ID="txtImportes" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="0" Width="100px" AutoPostBack="false"></telerik:RadNumericTextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-lg-6 control-label">&nbsp;</label>

                                            <div class="col-lg-6">
                                                <telerik:RadButton ID="btnQuitarC" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Quitar concepto"></telerik:RadButton>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="col-lg-6">
                                <div class="col-md-4">
                                    <div class="form-horizontal">
                                        <div class="form-group">

                                            <div class="col-lg-12">
                                                <telerik:RadButton ID="btnModif" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Modif. dias" Width="100%"></telerik:RadButton>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-horizontal">
                                        <div class="form-group">

                                            <div class="col-lg-12">
                                                <telerik:RadButton ID="btnAgregar" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Agregar trabaj." Width="100%"></telerik:RadButton>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-horizontal">
                                        <div class="form-group">

                                            <div class="col-lg-12">
                                                <telerik:RadButton ID="btnSalir" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Salir" Width="100%"></telerik:RadButton>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-horizontal">
                                        <div class="form-group">

                                            <div class="col-lg-12">
                                                <telerik:RadButton ID="btnicon" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text=">>"></telerik:RadButton>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-horizontal">
                                        <div class="form-group">

                                            <div class="col-lg-12">
                                                <telerik:RadButton ID="RadButton1" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Eliminar trabaj." Width="100%"></telerik:RadButton>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-horizontal">
                                        <div class="form-group">

                                            <div class="col-lg-12">
                                                &nbsp;

                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </telerik:RadWindow>

    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
