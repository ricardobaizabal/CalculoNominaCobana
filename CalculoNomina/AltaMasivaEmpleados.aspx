﻿<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="AltaMasivaEmpleados.aspx.vb" Inherits="CalculoNomina.AltaMasivaEmpleados" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btnAlta {
            margin-left: 0px !important;
            padding: 0px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="panelListaEmpleados" runat="server">
        <div class="ibox-content" style="border: solid 0px; min-height: auto !important;">
            <div class="row">
                <div class="col-sm-12 col-md-12 col-sm-12 b-r">
                    <h1 class="m-t-none m-b">Alta Masiva de Empleados</h1>
                    <hr class="demo-separator" />
                    <br />
                    <asp:Label ID="lblArchivo" Font-Bold="true" runat="server">Archivo:</asp:Label>
                    <asp:FileUpload ID="ImportarFile" Enabled="true" runat="server" Skin="Bootstrap" /><br />
                    <telerik:RadButton ID="btnDescargarCSV" RenderMode="Lightweight" Enabled="true" runat="server" Skin="Bootstrap" Text="Descargar Plantilla csv" Font-Bold="false" Width="180px" CssClass="rbPrimaryButton">
                    </telerik:RadButton>
                    &nbsp;&nbsp;&nbsp;
                    <telerik:RadButton ID="btnCargarEmpleados" RenderMode="Lightweight" Enabled="true" runat="server" Skin="Bootstrap" Text="Cargar Empleados" Font-Bold="false" Width="180px" CssClass="rbPrimaryButton">
                    </telerik:RadButton>
                    <br /><br />
                    <!-- Panel para mostrar los que se agregaron -->
                    <asp:Panel ID="panelSuccess" runat="server" Visible="False" CssClass="alert alert-success">
                        <h4>Se agregaron con Exito:</h4>
                        <asp:Label ID="lblEmpleadosSuccess" runat="server" Visible="False" Font-Bold="true"></asp:Label>
                        <asp:Literal ID="litSuccess" runat="server"></asp:Literal>
                        <br />
                    </asp:Panel>
                    <br />
                    <!-- Panel para mostrar errores -->
                    <asp:Panel ID="panelErrores" runat="server" Visible="False" CssClass="alert alert-danger">
                        <asp:Label ID="lblEmpleadosError" runat="server" Visible="False" Font-Bold="true"></asp:Label>
                        <br />
                        <br />
                        <h4>Las siguientes filas obtienen errores:</h4>
                        <asp:Literal ID="litErrores" runat="server"></asp:Literal>
                        <br />
                    </asp:Panel>
                    <!-- Panel para mostrar errores -->
                    <asp:Panel ID="panelErroresCatalogos" runat="server" Visible="False" CssClass="alert alert-danger">
                        <asp:Label ID="lblEmpleadosErrorCatalogos" runat="server" Visible="False" Font-Bold="true"></asp:Label>
                        <br />
                        <h4>Las siguientes filas obtienen errores, los empleados si cumplen con los demas campos requeridos se dieron de alta correctamente solo modificar esos datos en cada uno de ellos:</h4>
                        <asp:Literal ID="litErroresCatalogo" runat="server"></asp:Literal>
                        <br />
                    </asp:Panel>
                    <br />
                    <asp:Label ID="lblInfo0" runat="server" Visible="True" Font-Bold="true">Guardar y subir el archivo en formato .csv</asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblInfo" runat="server" Visible="True" Font-Bold="true" ForeColor="Orange">Notas importantes:</asp:Label>
                    <br />
                    <br />
                    <asp:Label ID="lblInfo1" runat="server" Visible="True" Font-Bold="true">Las siguientes columnas son datos requeridos:</asp:Label>
                    <br />
                    <asp:Label ID="lblInfo2" runat="server" Visible="True">1. Nombre<br />2. Apellido Paterno <br />3. Apellido Materno<br />4. Sexo<br />5. Fecha Nacimiento - Formato de fecha: Día/Mes/Año (dd/mm/aaaa)<br />6. RFC<br />7. Curp<br />8. NSS<br />9. Municipio<br />10. Codigo Postal<br />11. Estado<br />12. País<br />13. Estado Civil<br />14. Cliente<br />15. Sueldos y Salarios<br />16. Excendente<br />17. Fecha de alta - Formato de fecha: Día/Mes/Año (dd/mm/aaaa)<br />18. Departamento<br />19. Puesto<br />20. SB (salario Base)<br />21. SDI (Salario Diario Integrado)<br />22. Riesgo de Puesto<br />23. Régimen de contratación<br />24. Periodo de Pago<br />25. Tipo de Nómina<br />26. Régimen Fiscal<br />27. Tipo de Jornada<br />28. Tipo de Contrato</asp:Label>
                    <br />
                    <br />
                    <%--<asp:Label ID="lblInfo3" runat="server" Visible="True" Font-Bold="true">El formato de fecha es Dia/Mes/Año.</asp:Label>--%>
                </div>
            </div>
            <br />
        </div>

        <div class="form-group row">
            <asp:Label ID="Mensaje_Resultado" runat="server" Visible="false"></asp:Label>
            <telerik:RadGrid ID="GridErrors" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" AllowSorting="true" ExportSettings-ExportOnlyData="true" CellSpacing="0" GridLines="None" Skin="Simple" Visible="false">
                <PagerStyle Mode="NumericPages"></PagerStyle>
                <ExportSettings IgnorePaging="true" FileName="Empleados">
                    <Excel Format="ExcelML" />
                </ExportSettings>
                <ClientSettings AllowColumnsReorder="True" ReorderColumnsOnClient="True">
                </ClientSettings>
                <MasterTableView NoMasterRecordsText="No hay registros para mostrar." CommandItemDisplay="Top">
                    <CommandItemSettings ShowExportToExcelButton="true" ShowAddNewRecordButton="false" ShowRefreshButton="false" ShowExportToPdfButton="false" ExportToExcelText="Exportar a excel"></CommandItemSettings>
                    <Columns>
                        <telerik:GridBoundColumn DataField="clave_empleado" HeaderText="Clave Empleado" UniqueName="clave_empleado"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Error_excedente" HeaderText="Error Excedente" UniqueName="Error_excedente"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Error_sueldos_salarios" HeaderText="Error Sueldos y Salarios" UniqueName="Error_sueldos_salarios"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Error_sexo" HeaderText="Error Sexo" UniqueName="Error_sexo"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Error_cliente" HeaderText="Error Cliente" UniqueName="Error_cliente"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Error_estadocivil" HeaderText="Error Estado Civil" UniqueName="Error_estadocivil"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Error_estado" HeaderText="Error Estado" UniqueName="Error_estado"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Error_municipio" HeaderText="Error Municipio" UniqueName="Error_municipio"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Error_puesto" HeaderText="Error puesto de trabajo" UniqueName="Error_puesto"></telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="Error_periodo" HeaderText="Error Periodo de Pago" UniqueName="Error_periodo"></telerik:GridBoundColumn>
                    </Columns>
                </MasterTableView>
            </telerik:RadGrid>
        </div>
    </asp:Panel>
    <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
</asp:Content>
