<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvSugUbicacionConOrigen.aspx.cs" Inherits="whusap.WebPages.whInvSugUbicacionConOrigen" %>

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
                <asp:Label runat="server" ID="lblOrigen" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:DropDownList runat="server" ID="slOrigen" CssClass="TextBoxBig"></asp:DropDownList>
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblNumeroOrden" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtNumeroOrden" CssClass="TextBoxBig" onblur="validarOrden();" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <hr />  
                <asp:Button Text="" runat="server" ID="btnConsultar" OnClick="btnConsultar_Click" CssClass="ButtonsSendSave" style="height:30px;" />
                <asp:DropDownList runat="server" ID="slPalletsID" Visible="false">
                    
                </asp:DropDownList>
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
            <tr style="background-color:White; font-weight:bold;">
                <td><b><asp:Label runat="server" ID="lblLocation"></asp:Label></b></td>
                <td><asp:TextBox runat="server" ID="txtLocation" CssClass="TextBox" OnTextChanged="txtLocation_OnTextChanged" AutoPostBack="true" /></td>
            </tr>
            <tr>
                <td><b><asp:Label runat="server" ID="lblWareHouse"></asp:Label></b></td>
                <td><asp:Label Text="" runat="server" ID="lblValueWareHouse" /></td>
            </tr>
            <tr>
                <td><b><asp:Label runat="server" ID="lblLocation2"></asp:Label></b></td>
                <td><asp:Label Text="" runat="server" ID="lblValueLocation2" /></td>
            </tr>
            <tr>
                <td><b><asp:Label runat="server" ID="lblPorAprobar"></asp:Label></b></td>
                <td><asp:Label Text="" runat="server" ID="lblValuePorAprobar" /></td>
            </tr>
            <tr>
                <td><b><asp:Label runat="server" ID="lblQuantity"></asp:Label></b></td>
                <td><asp:Label Text="" runat="server" ID="lblValueQuantity" /></td>
            </tr>
            <tr>
                <td><b><asp:Label runat="server" ID="lblQuantityPLT"></asp:Label></b></td>
                <td><asp:Label Text="" runat="server" ID="lblValueQuantityPLT" /></td>
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
    <asp:HiddenField  runat="server" ID="hdfNumeroOrdenPallet" />
    <hr />
</asp:Content>