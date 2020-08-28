<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvReprintLabelMaterialRejectedD.aspx.cs" Inherits="whusap.WebPages.Migration.whInvReprintLabelMaterialRejectedD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        function printDiv(divID) {
            //PRINT LOCAL HOUR

            var d = new Date();
            var x = document.getElementById("lblValueDate");
            var h = addZero(d.getHours());
            var m = addZero(d.getMinutes());
            var s = addZero(d.getSeconds());
            //x.innerHTML = d.toUTCString();
            x.innerHTML = d.toLocaleString();

            //Get the HTML of div
            var divElements = document.getElementById(divID).innerHTML;
            //Get the HTML of whole page
            var oldPage = document.body.innerHTML;
            //Reset the page's HTML with div's HTML only
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
            //Print Page
            window.print();
            //Restore orignal HTML
            document.body.innerHTML = oldPage;
        };

        function addZero(i) {
            if (i < 10) {
                i = "0" + i;
            }
            return i;
        };

        function validarOrden() {
            if (document.getElementById("txtOrder").value != "") {
                if (document.getElementById("txtOrder").value.length < 9 || document.getElementById("txtOrder").value.length > 9) {
                    alert(_idioma == "INGLES" ? "Please use this format WORKORDER, remember only 9 characters"
                    : "Por favor use el forma de order de trabajo, solo 9 caracteres.");
                    document.getElementById("txtOrder").focus();
                    document.getElementById("txtOrder").value = "";
                    return false;
                }
            }
            else {
                document.getElementById("txtOrder").focus();
                document.getElementById("txtOrder").value = "";
            }
        };

        function validarFormulario() {
            debugger
            var validate = true;
            var mensaje = "";

            var txtItem = $('#txtItem').val();
            var txtLot = $('#txtLot').val();
            var txtWarehouse = $('#txtWarehouse').val();
            var txtQuantity = $('#txtQuantity').val();

            if (txtItem.trim() == "") {
                validate = false;
                mensaje += _idioma == "INGLES" ? "-Item\n" : "-Articulo\n";
            }

            if (txtLot.trim() == "") {
                validate = false;
                mensaje += _idioma == "INGLES" ? "-Lot\n" : "-Lote\n";
            }

            if (txtWarehouse.trim() == "") {
                validate = false;
                mensaje += _idioma == "INGLES" ? "-Warehouse\n" : "-Almacen\n";
            }

            if (txtQuantity.trim() == "") {
                validate = false;
                mensaje += _idioma == "INGLES" ? "-Quantity\n" : "-Cantidad\n";
            } else if (parseFloat(txtQuantity) <= 0) {
                validate = false;
                mensaje += _idioma == "INGLES" ? "-A valid number for Quantity\n" : "-Un número valida para cantidad\n";
            }

            debugger;
            if (!validate) {
                alert((_idioma == "INGLES" ? "Please, enter this fields:\n" : "Por favor ingrese los siguientes campos:\n") + mensaje);
            }

            return validate;
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table border="0">
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblItem" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtItem" CssClass="TextBoxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblLot" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtLot" CssClass="TextBoxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblWarehouse" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtWarehouse" CssClass="TextBoxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblQuantity" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtQuantity" CssClass="TextBoxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <hr />  
                <asp:Button Text="" runat="server" ID="btnConsultar" OnClick="btnConsultar_Click" OnClientClick="return validarFormulario();" CssClass="ButtonsSendSave" style="height:30px;" />
            </td>
        </tr>
    </table>

    <div runat="server" id="divBotones" style="text-align:left;" visible="false">
        <hr />
         <a href="#" runat="server" id="linkPrint" onclick="javascript:printDiv('divTable')" style="color: #000000; font-size: medium"><button class="buttonMenu" runat="server" id="btnPrint" style="width:25%;"></button></a>
    </div>

    <div runat="server" id="divTable" clientidmode="Static" visible="false">
        <table style="font-size:small; font-weight:bold; text-align:center; width: 5.8in; height: 3.8in" border="1" cellspacing="0" cellpadding="0">
            <tr>
                <td colspan='2'><img src="~/images/logophoenix_login.jpg" runat="server" id="imgItem" alt="" hspace="60" vspace="5" style="width: 4in; height: .5in; margin:0px;"/></td>
            </tr>
            <tr>
                <td colspan='2'><asp:Label ID="lblItemTab" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="lblTotalQty"></asp:Label></td>
                <td><img src="~/images/logophoenix_login.jpg" runat="server" id="imgTotalQyt" alt="" hspace="60" vspace="5" style="width: 1in; height: .5in;  margin:0px;" /></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="lblValueTotalQty" /></td>
                <td><asp:Label runat="server" ID="lblUnit" /> - <asp:Label runat="server" ID="lblValueUnit" /></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="lblWorkOrder" /></td>
                <td><asp:Label runat="server" ID="lblDate" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Label runat="server" ID="lblValueDate" ClientIDMode="Static" /></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="lblPrintedBy" /></td>
                <td><asp:Label runat="server" ID="lblCopy" /></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="lblValuePrintedBy" /></td>
                <td></td>
            </tr>
        </table>
        <hr />
    </div>


    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />

</asp:Content>