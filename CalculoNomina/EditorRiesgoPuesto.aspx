<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MasterPage.Master" CodeBehind="EditorRiesgoPuesto.aspx.vb" Inherits="CalculoNomina.EditorRiesgoPuesto" %>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="styles.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Width="100%" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel1">
        <div class="col-sm-6 b-r">
            <h1 class="m-t-none m-b">Riesgo del puesto</h1>
            <hr class="demo-separator" />
            <br />
            <div class="form-group">
                <asp:HiddenField ID="registroId" runat="server" Value="0" Visible="False" />
                <label>Nombre:</label>
                <telerik:RadTextBox RenderMode="Lightweight" runat="server" ID="txtNombre" Width="300px" class="form-control"></telerik:RadTextBox><br />
            </div>
            <div class="button-wrappers">
                <telerik:RadButton ID="btnSave" RenderMode="Lightweight" runat="server" Skin="Bootstrap" Text="Guadar"></telerik:RadButton>
            </div>
        </div>
    </telerik:RadAjaxPanel>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Bootstrap" Width="100%">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>

