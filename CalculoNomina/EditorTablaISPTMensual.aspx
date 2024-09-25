<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="EditorTablaISPTMensual.aspx.vb" Inherits="CalculoNomina.EditorTablaISPTMensual" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ibox-content">
        <div class="row">
            <div class="col-lg-8">
                <div>
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                 <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
                                <label class="col-lg-7 control-label">Limite inferior</label>

                                <div class="col-lg-5">
                                   <telerik:RadNumericTextBox ID="txtLimiteinferior" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="2" Width="100px"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-7 control-label">Limite superior</label>

                                <div class="col-lg-4">
                                    <telerik:RadNumericTextBox ID="txtLimitesuperior" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="2" Width="100px"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-7 control-label">Cuota fija</label>

                                <div class="col-lg-4">
                                    <telerik:RadNumericTextBox ID="txtCuotafija" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="2" Width="100px"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-7 control-label">%Sobre exc.limite inf.</label>

                                <div class="col-lg-4">
                                   <telerik:RadNumericTextBox ID="txtporclimiteinf" runat="server" Value="0" MinValue="0" NumberFormat-DecimalDigits="2" Width="100px"></telerik:RadNumericTextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-9 control-label">&nbsp;&nbsp;&nbsp;</label>

                                <div class="col-lg-3">
                                    <telerik:RadButton ID="btnSave" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Guadar"></telerik:RadButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                </div>
            </div>
        </div>
    </div>
</asp:Content>

