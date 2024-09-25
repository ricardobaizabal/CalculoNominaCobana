<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="EditorEmpleado.aspx.vb" Inherits="CalculoNomina.EditorEmpleado" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="col-lg-12">
            <div class="row">
                <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />

                <div class="row">
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Clave Empleado</label>

                                <div class="col-lg-8">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtClaveEmp" Width="300px" class="form-control" Enabled="false"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Fecha de ingreso</label>

                                <div class="col-lg-8">
                                    <telerik:RadDatePicker ID="calFechaIngreso" CultureInfo="Español (México)" DateInput-DateFormat="dd/MM/yyyy" runat="server"></telerik:RadDatePicker>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-2 control-label">Nombre</label>

                                <div class="col-lg-10">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtNombre" Width="700px" class="form-control"></telerik:RadTextBox>
                                    <%-- <input type="email" placeholder="Email" class="form-control">--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-2 control-label">Domicilio</label>

                                <div class="col-lg-10">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtDomicilio" Width="700px" class="form-control"></telerik:RadTextBox>
                                    <%--                                <input type="email" placeholder="Email" class="form-control">--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Colonia</label>

                                <div class="col-lg-8">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtColonia" Width="300px" class="form-control"></telerik:RadTextBox>
                                    <%--  <input type="email" placeholder="Email" class="form-control">--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">RFC</label>

                                <div class="col-lg-8">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtrfc" Width="250px" class="form-control"></telerik:RadTextBox>
                                    <%--                                    <input type="email" placeholder="Email" class="form-control">--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Ciudad</label>

                                <div class="col-lg-8">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtCiudad" Width="300px" class="form-control"></telerik:RadTextBox>
                                    <%--<input type="email" placeholder="Email" class="form-control">--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">CURP</label>

                                <div class="col-lg-8">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtCurp" Width="250px" class="form-control"></telerik:RadTextBox>
                                    <%--                                    <input type="email" placeholder="Email" class="form-control">--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Estado</label>

                                <div class="col-lg-8">
                                    <asp:DropDownList ID="cmbEstado" runat="server" Width="300px"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">IMSS</label>

                                <div class="col-lg-8">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtImss" Width="250px" class="form-control"></telerik:RadTextBox>
                                    <%--                                    <input type="email" placeholder="Email" class="form-control">--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Pais</label>

                                <div class="col-lg-8">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtPais" Width="300px" class="form-control"></telerik:RadTextBox>
                                    <%--                                    <input type="email" placeholder="Email" class="form-control">--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Tipo Contrato</label>

                                <div class="col-lg-8">
                                    <asp:DropDownList ID="cmbTipoContrato" runat="server" Width="250px"></asp:DropDownList>
                                    <%--  <input type="email" placeholder="Email" class="form-control">--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Codigo postal</label>

                                <div class="col-lg-8">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtCp" Width="300px" class="form-control"></telerik:RadTextBox>
                                    <%--                                    <input type="email" placeholder="Email" class="form-control">--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Periodicidad Pago</label>

                                <div class="col-lg-8">
                                    <asp:DropDownList ID="cmbPeriodicidadP" runat="server" Width="250px"></asp:DropDownList>
                                    <%--<input type="email" placeholder="Email" class="form-control">--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Telefono</label>

                                <div class="col-lg-8">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtTelefono" Width="300px" class="form-control"></telerik:RadTextBox>
                                    <%--                                    <input type="email" placeholder="Email" class="form-control">--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Puesto</label>

                                <div class="col-lg-8">
                                    <asp:DropDownList ID="cmbPuesto" runat="server" Width="250px"></asp:DropDownList>
                                    <%--  <input type="email" placeholder="Email" class="form-control">--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Correo</label>

                                <div class="col-lg-8">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtCorreo" Width="300px" class="form-control"></telerik:RadTextBox>
                                    <%--                                    <input type="email" placeholder="Email" class="form-control">--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Riesgo del puesto</label>

                                <div class="col-lg-8">
                                    <asp:DropDownList ID="cmbRiesgo" runat="server" Width="250px"></asp:DropDownList>
                                    <%--   <input type="email" placeholder="Email" class="form-control">--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Metodo de pago</label>

                                <div class="col-lg-8">
                                    <asp:DropDownList ID="cmbMetodoPago" runat="server" Width="300px"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Tipo de jornada</label>

                                <div class="col-lg-8">
                                    <asp:DropDownList ID="cmbTipoj" runat="server" Width="250px"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Clabe</label>

                                <div class="col-lg-8">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtClabe" Width="300px" class="form-control"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Regimen de contratacion</label>

                                <div class="col-lg-8">
                                    <asp:DropDownList ID="cmbRegimen" runat="server" Width="250px"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Num Cuenta</label>

                                <div class="col-lg-8">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtNumCuenta" Width="300px" class="form-control"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Banco</label>

                                <div class="col-lg-8">
                                    <asp:DropDownList ID="cmbBanco" runat="server" Width="250px"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="ibox-content">
                        <div class="row">
                            <div class="col-lg-8">
                                <div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-lg-7 control-label">Salario Diario</label>

                                                <div class="col-lg-4">
                                                    <telerik:RadNumericTextBox ID="txtSueldoDiario" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="5" Width="100px" AutoPostBack="true"></telerik:RadNumericTextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-lg-7 control-label">Asimilado total semanal</label>

                                                <div class="col-lg-4">
                                                    <telerik:RadNumericTextBox ID="txtAsimiladoTotalS" runat="server" Value="0" MinValue="0" Width="100px">
                                                        <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                                    </telerik:RadNumericTextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-lg-7 control-label">Factor de integracion</label>

                                                <div class="col-lg-4">
                                                    <telerik:RadNumericTextBox ID="txtFactorIntegracion" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="5" Width="100px" AutoPostBack="true"></telerik:RadNumericTextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-lg-7 control-label">% Impto. cedular est.</label>

                                                <div class="col-lg-4">
                                                    <telerik:RadNumericTextBox ID="txtImptoCedular" runat="server" Value="0" MinValue="0" Width="100px">
                                                        <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                                    </telerik:RadNumericTextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-lg-7 control-label">Salario Diario Integrado</label>

                                                <div class="col-lg-4">
                                                    <telerik:RadNumericTextBox ID="txtSueldoDiarioIntegrado" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="5" Width="100px" Enabled="false"></telerik:RadNumericTextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-lg-7 control-label">Descansos x semana</label>

                                                <div class="col-lg-4">
                                                    <telerik:RadNumericTextBox ID="txtDescansosxSemana" runat="server" Value="0" MinValue="0" Width="100px">
                                                        <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                                    </telerik:RadNumericTextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-8">
                                    <div>
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="col-lg-7 control-label">Pago x hora</label>

                                                    <div class="col-lg-4">
                                                        <telerik:RadNumericTextBox ID="txtPagoxHora" runat="server" Value="0" MinValue="0" Width="100px">
                                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="col-lg-7 control-label">Integrado tipo</label>

                                                    <div class="col-lg-4">
                                                        <telerik:RadNumericTextBox ID="txtIntegradoTipo" runat="server" Value="0" MinValue="0" Width="100px">
                                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="col-lg-7 control-label">Factor de comision</label>

                                                    <div class="col-lg-4">
                                                        <telerik:RadNumericTextBox ID="txtFactorComision" runat="server" Value="0" MinValue="0" Width="100px">
                                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="col-lg-7 control-label">Prom. cuota variable</label>

                                                    <div class="col-lg-4">
                                                        <telerik:RadNumericTextBox ID="txtPromCuotaV" runat="server" Value="0" MinValue="0" Width="100px">
                                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="col-lg-7 control-label">Factor destajo &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>

                                                    <div class="col-lg-4">
                                                        <telerik:RadNumericTextBox ID="txtFactorDestajo" runat="server" Value="0" MinValue="0" Width="100px">
                                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="col-lg-7 control-label">Promedio percep. variables</label>

                                                    <div class="col-lg-4">
                                                        <telerik:RadNumericTextBox ID="txtPromedioPercepV" runat="server" Value="0" MinValue="0" Width="100px">
                                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <label class="col-lg-7 control-label">Horas al dia</label>

                                                    <div class="col-lg-4">
                                                        <telerik:RadNumericTextBox ID="txtHorasDia" runat="server" Value="0" MinValue="0" Width="100px">
                                                            <NumberFormat GroupSeparator="" DecimalDigits="0" AllowRounding="true" KeepNotRoundedValue="false" />
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <p class="text-center">
                                        <a href=""><i class="fa fa-sign-in big-icon"></i></a>
                                    </p>
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
