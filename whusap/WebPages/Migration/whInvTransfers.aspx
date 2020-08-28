<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvTransfers.aspx.cs" Inherits="whusap.WebPages.Migration.whInvTransfers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table border="0">
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblNumeroLote" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtNumeroLote" CssClass="TextboxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblItem" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtItem" CssClass="TextboxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr runat="server" id="trDescription" visible="false">
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblDescription" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtDescription" CssClass="TextboxBig" ClientIDMode="Static" Enabled = "false" />
                </span>
            </td>
        </tr>
        <tr runat="server" id="trSource" visible="false">
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblSourceLocation" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtSourceLocation" CssClass="TextboxBig" ClientIDMode="Static" Enabled="false" />
                </span>
            </td>
        </tr>
        <tr runat="server" id="trUbicacion" visible="false"> 
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblUbicacion" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtUbicacion" CssClass="TextboxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr runat="server" id="trCantidadUnidad" visible="false">
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblCantidadUnidad" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtCantidadUnidad" CssClass="TextboxBig" ClientIDMode="Static" Enabled="false"/>
                </span>
            </td>
        </tr>
        <tr runat="server" id="trCantidad" visible="false">
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblCantidad" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtCantidad" CssClass="TextboxBig" ClientIDMode="Static" Enabled="false"/>
                </span>
            </td>
        </tr>
        <tr runat="server" id="trDestination" visible="false">
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblUbicacionDestino" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtUbicacionDestino" CssClass="TextboxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr runat="server" id="trCantidadTransferir" visible="false">
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblCantidadTrasnferir" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox TextMode="Number" runat="server" ID="txtCantidadTrasnferir" CssClass="TextboxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <hr />  
                <asp:Button Text="" runat="server" ID="btnConsultar" OnClick="btnConsultar_Click" CssClass="ButtonsSendSave" style="height:30px;" />
                <asp:Button Text="" runat="server" ID="btnConsultarSource" OnClick="btnConsultarSource_Click" CssClass="ButtonsSendSave" style="height:30px;" Visible="false" />
                <asp:Button Text="" runat="server" ID="btnActualizar" OnClick="btnActualizar_Click" CssClass="ButtonsSendSave" style="height:30px;" Visible="false" />
            </td>
        </tr>
    </table>

    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdfFactor"/>
    <asp:HiddenField runat="server" ID="hdfUnidad"/>
</asp:Content>