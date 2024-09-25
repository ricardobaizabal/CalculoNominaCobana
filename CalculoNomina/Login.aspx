<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="CalculoNomina.Login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <meta charset="utf-8" />
    <title>Sistema - Cálculo de Nómina</title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <!-- Script references -->
    <script src="js/jquery-1.10.2.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script type="text/javascript">
        function Load(sender, args) {
            pw = sender;
            pw.focus();
            pw.blur();
        }
    </script>
    <%--CSS--%>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/Login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        
            <div id="loginModal" class="modal show" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-body">
                            <div class="row">
                                <telerik:RadTextBox RenderMode="Lightweight" ID="txtEmail" Skin="Bootstrap" Width="100%" EmptyMessage="Usuario" runat="server"></telerik:RadTextBox>
                            </div>
                            <div class="form-group">&nbsp;</div>
                            <div class="row">
                                <telerik:RadTextBox RenderMode="Lightweight" ID="txtContrasena" Skin="Bootstrap" Width="100%" EmptyMessage="********" TextMode="Password" runat="server">
                                    <ClientEvents OnLoad="Load" />
                                </telerik:RadTextBox>
                            </div>
                            <div class="form-group">&nbsp;</div>
                            <div class="form-group text-center">
                                <telerik:RadButton ID="btnLogin" RenderMode="Lightweight" runat="server" Width="100px" Skin="Bootstrap" CssClass="rbPrimaryButton" Text="Entrar"></telerik:RadButton>
                            </div>
                            <div class="form-group text-left">
                                <asp:CheckBox ID="chkRemember" runat="server" Visible="false" Text="Recordar mis datos" />
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-md-12">
                                <div>
                                    <asp:Label ID="lblMensaje" ForeColor="Red" CssClass="item" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Label ID="lblError" Visible="false" runat="server"></asp:Label>

            <telerik:RadWindowManager ID="rwAlerta" runat="server" Skin="Bootstrap" EnableShadow="false" Localization-OK="Aceptar" Localization-Cancel="Cancelar" RenderMode="Lightweight"></telerik:RadWindowManager>
        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        </telerik:RadAjaxPanel>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
        </telerik:RadAjaxLoadingPanel>
    </form>
</body>
