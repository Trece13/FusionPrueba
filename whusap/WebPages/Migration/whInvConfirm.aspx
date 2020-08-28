<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvConfirm.aspx.cs" Inherits="whusap.WebPages.Migration.whInvConfirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        var _idioma = '<%= _idioma %>';
        var numeroRegistros = <%= _validaRecepcion.Rows.Count %>

        function validarFormulario() { 
            var validConfirm = false;
            for (var i = 0; i < numeroRegistros; i++) {
                
                var slConfirm = $('#slConfirm-' + i).val();

                if (slConfirm == "true") {
                    validConfirm = true;
                }
            }

            if (!validConfirm) {
                alert(_idioma == "INGLES" ? "Please select items to confirm." : "Por favor seleccione articulos para confirmar");
            }
            
            return validConfirm;
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table border="0">
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblMovement" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:DropDownList runat="server" ID="slMovement" CssClass="TextBoxBig">
                        <asp:ListItem Text="Purchase" Value="2" />
                        <asp:ListItem Text="Sales" Value="1" />
                        <asp:ListItem Text="Manufacturing" Value="4" />
                        <asp:ListItem Text="Supplies" Value="21" />
                        <asp:ListItem Text="Transfer" Value="22" />
                    </asp:DropDownList>
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblOrder" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtOrder" CssClass="TextBoxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <hr />  
                <asp:Button Text="" runat="server" ID="btnConsultar" OnClick="btnConsultar_Click" CssClass="ButtonsSendSave" style="height:30px;" />
            </td>
        </tr>
    </table>

    <div runat="server" id="divBtnGuardar" visible="false">
        <hr />
        <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" OnClientClick="return validarFormulario();" CssClass="ButtonsSendSave"/>
    </div>

    <div runat="server" id="divTable" style="max-height:500px; overflow-y:scroll;">

    </div>

    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />

</asp:Content>
