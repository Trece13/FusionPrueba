<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master"  AutoEventWireup="true" CodeBehind="whInvRollAnnounce.aspx.cs" Inherits="whusap.WebPages.Migration.whInvRollAnnounce" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        var _idioma = '<%= _idioma %>';
        function validarOrden() {
            if (document.getElementById("txtWorkOrder").value != "") {
                if (document.getElementById("txtWorkOrder").value.length < 9 || document.getElementById("txtWorkOrder").value.length > 9) {
                    alert(_idioma == "INGLES" ? "Please use this format WORKORDER, remember only 9 characters"
                    : "Por favor use el forma de order de trabajo, solo 9 caracteres.");
                    document.getElementById("txtWorkOrder").focus();
                    document.getElementById("txtWorkOrder").value = "";
                    return false;
                }
            }
            else {
                document.getElementById("txtWorkOrder").focus();
                document.getElementById("txtWorkOrder").value = "";
            }
        };

        function validarRoll() {
            var regex = /^-?\d*[0-9]*[.]?[0-9]*$/;
            var re = new RegExp(regex);
            var numeroOrden = document.getElementById('txtRollNumber').value.trim();
            var orden = numeroOrden.substr(9, 1);
            var guion = numeroOrden.substr(9, 1);
            var pallet = numeroOrden.substr(10, 3);
            if (pallet.match(re)) {
                if (numeroOrden != "") {
                    if (numeroOrden.length < 13 || numeroOrden.length > 13) {
                        alert(_idioma == "INGLES" ? "Please use this format WORKORDER-PALLETID, remember only 13 characters"
                                    : "Por favor use el formato ORDENTRABAJO-PALLETID, solo 13 caracteres");
                        document.getElementById("txtRollNumber").focus();
                        document.getElementById("txtRollNumber").value = "";
                    }
                    else {
                        if (guion != "-") {
                            alert(_idioma == "INGLES" ? "Please use this format WORKORDER-PALLETID, remember 9 characters workorder, simbol minus, 3 characters pallet id"
                                            : "Por favor use el formato ORDERNTRABAJO-PALLETID, recuerde 9 caracteres para la orden de trabajo, simbolo negativo, 3 caracteres para el Pallet ID.");
                            document.getElementById("txtRollNumber").focus();
                            document.getElementById("txtRollNumber").value = "";
                        }
                    }
                }
            }
            else {
                document.getElementById("txtRollNumber").focus();
                document.getElementById("txtRollNumber").value = "";
                alert(_idioma == "INGLES" ? "Only numbers allowed on pallet id"
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
                    <asp:Label runat="server" ID="lblRollNumber" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtRollNumber" onblur="validarRoll();" CssClass="TextBoxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblWorkOrder" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtWorkOrder" onblur="validarOrden();" CssClass="TextBoxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr runat="server" id="trItem" visible="false">
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblItem" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtItem" CssClass="TextBoxBig" ClientIDMode="Static" ReadOnly="true" />
                </span>
            </td>
        </tr>
        <tr runat="server" id="trQuantity" visible="false">
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblQuantity" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtQuantity" CssClass="TextBoxBig" ClientIDMode="Static" ReadOnly="true" />
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <hr />  
                <asp:Button Text="" runat="server" ID="btnConsultar" OnClick="btnConsultar_Click" CssClass="ButtonsSendSave" style="height:30px;" />
                <asp:Button Text="" runat="server" ID="btnRegister" OnClick="btnRegister_Click" CssClass="ButtonsSendSave" style="height:30px;" visible="false" />
            </td>
        </tr>
    </table>

    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:HiddenField runat="server" ID="hdfCWAR"/>
    <asp:HiddenField runat="server" ID="hdfPONO"/>
</asp:Content>