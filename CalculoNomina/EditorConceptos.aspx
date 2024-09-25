<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="EditorConceptos.aspx.vb" Inherits="CalculoNomina.EditorConceptos" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">

        <div class="col-lg-12">
            <div class="row">
                <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />

                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Tipo:</label>

                                <div class="col-lg-8">
                                    <asp:DropDownList ID="cmbTipo" runat="server" Width="300px">
<%--                                        <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                        <asp:ListItem Value="1">Percepciones</asp:ListItem>
                                        <asp:ListItem Value="1">Deducciones</asp:ListItem>--%>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                     <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Catalogo Sat</label>

                                <div class="col-lg-8">
                                    <asp:DropDownList ID="cmbTipoConcepto" runat="server" Width="300px"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Clave:</label>

                                <div class="col-lg-8">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtClave" Width="300px"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-4 control-label">Concepto:</label>

                                <div class="col-lg-8">
                                    <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtConcepto" Width="300px"></telerik:RadTextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-lg-9">
                                     <telerik:RadButton ID="btnSalir" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Regresar Listado"></telerik:RadButton>
                                </div>

                                <div class="col-lg-3">
                                    <telerik:RadButton ID="btnSave" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Guadar"></telerik:RadButton>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>
            </div>
        </div>

        <%--  <div class="col-sm-6 b-r">
            <h1 class="m-t-none m-b">Concepto</h1>
            <hr class="demo-separator" />
            <br />
            <div class="form-group">
                <label>Tipo:</label><br />

            </div>
            <div class="form-group">
                <label>Clave:</label><br />
                <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtClave" Width="300px"></telerik:RadTextBox><br />
            </div>
            <div class="form-group">
                <label>Concepto:</label><br />
                <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtConcepto" Width="300px"></telerik:RadTextBox><br />
            </div>
            <div class="button-wrappers">
                
            </div>
        </div>--%>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>


