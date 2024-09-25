<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProgressBar.aspx.vb" Inherits="CalculoNomina.ProgressBar" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<title>Server-side tracking with RadProgressBar</title>
        <style type="text/css">
            .ruFileProgress{
                display:none !important;
            }
            .ruTimeSpeed {
                display:none !important;
            }
        </style>
	</head>
	<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />
    <div class="demo-container size-narrow">
        <telerik:RadButton RenderMode="Lightweight" ID="buttonSubmit" runat="server" Text="Start Processing" OnClick="buttonSubmit_Click">
        </telerik:RadButton>
        <telerik:RadProgressManager ID="RadProgressManager1" runat="server" />
        <telerik:RadProgressArea HeaderText="Favor de esperar..." RenderMode="Lightweight" ID="RadProgressArea1" runat="server" Width="500px" />
    </div>
    </form>
</body>
</html>