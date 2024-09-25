<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ModificacionNominaSemanalNormal.aspx.vb" Inherits="CalculoNomina.ModificacionNominaSemanalNormal" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
</head>
<body>
    <div class="col-lg-12">
        <div class="row">
            <div class="row">
                <div class="col-md-12">
                    <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-lg-6 control-label">Ejercicio</label>

                            <div class="col-lg-6">
                                <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtEjercicio" Width="300px" class="form-control" Enabled="false"></telerik:RadTextBox>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-2">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-lg-6 control-label">No.periodo</label>

                            <div class="col-lg-6">
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
                            <label class="col-lg-6 control-label">Dias</label>

                            <div class="col-lg-6">
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

                <div class="col-md-3">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-lg-7 control-label">Clave Empleado</label>

                            <div class="col-lg-5">
                                <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtNumEmpleado" Width="232px" class="form-control" Enabled="false"></telerik:RadTextBox>
                            </div>
                        </div>
                    </div>
                </div>

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

                <div class="col-md-5">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-lg-3 control-label">Puesto</label>

                            <div class="col-lg-9">
                                <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtPuesto" Width="270px" class="form-control" Enabled="false"></telerik:RadTextBox>
                            </div>
                        </div>
                    </div>
                </div>

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

                <div class="col-md-5">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label class="col-lg-5 control-label">Regimen Contrato</label>

                            <div class="col-lg-7">
                                <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtRegimencontrat" Width="205px" class="form-control" Enabled="false"></telerik:RadTextBox>
                            </div>
                        </div>
                    </div>
                </div>

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
                            <telerik:RadGrid ID="GridPercepciones" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="50"
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
    </div>
</body>
</html>
