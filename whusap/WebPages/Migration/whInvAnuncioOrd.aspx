<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvAnuncioOrd.aspx.cs" Inherits="whusap.WebPages.Migration.whInvAnuncioOrd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        function validarOrden() {
            var regex = /^-?\d*[0-9]*[.]?[0-9]*$/;
            var re = new RegExp(regex);
            var numeroOrden = document.getElementById('txtNumeroOrden').value.trim();
            var orden = numeroOrden.substr(9, 1);
            var guion = numeroOrden.substr(9, 1);
            var pallet = numeroOrden.substr(10, 3);
            var idioma = '<%= _idioma %>';
            if (pallet.match(re)) {
                if (numeroOrden != "") {
                    if (numeroOrden.length < 13 || numeroOrden.length > 13) {
                        alert(idioma == "INGLES" ? "Please use this format WORKORDER-PALLETID, remember only 13 characters"
                                    : "Por favor use el formato ORDENTRABAJO-PALLETID, solo 13 caracteres");
                        document.getElementById("txtNumeroOrden").focus();
                        document.getElementById("txtNumeroOrden").value = "";
                    }
                    else {
                        if (guion != "-") {
                            alert(idioma == "INGLES" ? "Please use this format WORKORDER-PALLETID, remember 9 characters workorder, simbol minus, 3 characters pallet id"
                                            : "Por favor use el formato ORDERNTRABAJO-PALLETID, recuerde 9 caracteres para la orden de trabajo, simbolo negativo, 3 caracteres para el Pallet ID.");
                            document.getElementById("txtNumeroOrden").focus();
                            document.getElementById("txtNumeroOrden").value = "";
                        }
                    }
                }
            }
            else {
                document.getElementById("txtNumeroOrden").focus();
                document.getElementById("txtNumeroOrden").value = "";
                alert(idioma == "INGLES" ? "Only numbers allowed on pallet id"
                                : "Solo se permiten números en el Pallet ID");
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table border="0">
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblOrderNumber" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtOrderNumber" CssClass="TextBoxBig" ClientIDMode="Static" onblur="validarOrden()" />
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

    <div runat="server" id="divTable" visible="false">
        <hr />  
        <table class="table table-bordered" style='font-size:13px; border:3px solid; border-style:outset;'>
            <tr style='background-color: darkblue; color: white; font-weight:bold;'>
                <td><b><asp:Label runat="server" ID="lblOrden"></asp:Label></b></td>
                <td colspan="4"><asp:Label runat="server" ID="lblValueOrden" /></td>
            </tr>
            <tr style="background-color: lightgray;">
                <td><b><asp:Label runat="server" ID="lblArticulo"></asp:Label></b></td>
                <td colspan="4"><asp:Label runat="server" ID="lblValueArticulo" /></td>
            </tr>
            <tr>
                <td><b><asp:Label runat="server" ID="lblWareHouse"></asp:Label></b></td>
                <td><asp:Label Text="" runat="server" ID="lblValueWareHouse" /></td>
            </tr>
            <tr>
                <td><b><asp:Label runat="server" ID="lblTotal"></asp:Label></b></td>
                <td><asp:Label Text="" runat="server" ID="lblValueTotal" /></td>
            </tr>
            <tr>
                <td><b><asp:Label runat="server" ID="lblDelivered"></asp:Label></b></td>
                <td><asp:Label Text="" runat="server" ID="lblValueDelivered" /></td>
            </tr>
            <tr>
                <td><b><asp:Label runat="server" ID="lblPending"></asp:Label></b></td>
                <td><asp:Label Text="" runat="server" ID="lblValuePending" /></td>
            </tr>
            <tr>
                <td><b><asp:Label runat="server" ID="lblAnounced"></asp:Label></b></td>
                <td><asp:Label Text="" runat="server" ID="lblValueAnounced" />
                    <asp:TextBox runat="server" ID="txtAnounced" CssClass="TextBox" Visible="false" /></td>
            </tr>
            <tr>
                <td><b><asp:Label runat="server" ID="lblUnit"></asp:Label></b></td>
                <td><asp:Label Text="" runat="server" ID="lblValueUnit" /></td>
            </tr>
        </table>
        <div style="text-align:right;">
            <asp:Button Text="" runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="ButtonsSendSave" style="height:30px;" />
        </div>
    </div>

    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdfSqnb" />
</asp:Content>
