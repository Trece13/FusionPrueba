<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MDMasterPage.Master" CodeBehind="whInvCertificados.aspx.cs" Inherits="whusap.WebPages.InvProductos.whInvCertificados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
        <table border="0">
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblTipoDocumento" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:DropDownList runat="server" ID="ddlTipoDocumento" CssClass="TextboxBig" style="width:100%;">
                        <asp:ListItem Value = "ENVIO" Text="Envío" />
                        <asp:ListItem Value = "FACTURA" Text="Factura" />
                    </asp:DropDownList>  
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;"><asp:Label runat="server" ID="lblNumeroDocumento"></asp:Label></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtNumeroDocumento" CssClass="TextboxBig" style="width:100%;" />
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <hr />  
                <asp:Button Text="Generar" runat="server" ID="btnGenerateCertificate" OnClick="btnGenerateCertificate_Click" CssClass="ButtonsSendSave" style="height:30px;" />
            </td>
        </tr>
    </table>
    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" />
</asp:Content>