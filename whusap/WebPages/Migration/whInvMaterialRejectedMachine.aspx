<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvMaterialRejectedMachine.aspx.cs" Inherits="whusap.WebPages.Migration.whInvMaterialRejected" %>

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

        function validarShift(field) {
            var valor = field.value.toUpperCase();
            if (valor == 'A' || valor == 'B' || valor == 'C' || valor == 'D') {
            }
            else {
                alert(_idioma == "INGLES" ? "Values must be A, B C or D" : "El valor debe ser A, B, C o D");
                this.focus();
                field.value = '';
            }
        }

        function validarCantidad(field) {
            var cantidad = field.value;
            var quant = $('#hdfQuantity').val();
            var regex = /\d*[0-9]*[.]?[0-9]*$/;
            var re = new RegExp(regex);
            if (field.value.match(re)) {
                if ((cantidad < 0)) {
                    alert(_idioma == "INGLES" ? 'Quantity can not be minor or equals zero' : 'Cantidad no puede ser menor o igual a cero');
                    this.focus();
                    field.value = 0;
                } else {
                    if (parseFloat(cantidad) > parseFloat(quant)) {
                        alert(_idioma == "INGLES" ? 'Quantity higher than Quantities delivered to WO' : 'Cantidad ingresada es mas grande que la cantidad disponible para la orden');
                        this.focus();
                        field.value = 0;
                    }
                }
            }
            else {
                this.focus();
                field.value = 0;
                alert(_idioma == "INGLES" ? "Only numbers here" : "Solo número en este campo");
            }
        };

        function validarFormulario() {
            debugger
            var validate = true;
            var mensaje = "";

            var slItem = $('#slItems').val();
            var txtQty = $('#txtQty').val();
            var slLot = $('#slLot').val();
            var txtShift = $('#txtShift').val();
            var txtComments = $('#txtExactReasons').val();

            if (slItem.trim() == "") {
                validate = false;
                mensaje += _idioma == "INGLES" ? "-Item\n" : "-Articulo\n";
            }

            if (txtQty.trim() == "") {
                validate = false;
                mensaje += _idioma == "INGLES" ? "-Quantity\n" : "-Cantidad\n";
            }

            if (slLot == null) {
                validate = false;
                mensaje += _idioma == "INGLES" ? "-Lot\n" : "-Lote\n";
            }

            if (txtShift.trim() == "") {
                validate = false;
                mensaje += _idioma == "INGLES" ? "-Shift\n" : "-Cambio\n";
            }

            if (txtComments.trim() == "") {
                validate = false;
                mensaje += _idioma == "INGLES" ? "-Exact Reason\n" : "-Razón exacta\n";
            }

            debugger;
            if (!validate) {
                alert((_idioma == "INGLES" ? "Please, enter this fields:\n" : "Por favor ingrese los siguientes campos:\n") + mensaje);
            }

            return validate;
        };


        function addZero(i) {
            if (i < 10) {
                i = "0" + i;

            }
            return i;
        }; 



        function printDiv(divID) {

            var monthNames = [
                "1", "2", "3",
                "4", "5", "6", "7",
                "8", "9", "10",
                "11", "12"
              ];

            //PRINT LOCAL HOUR
            var d = new Date();
            var LbdDate = $("#Contenido_lblFecha");
            LbdDate.text(

                monthNames[d.getMonth()] +
                "/" +
                d.getDate() +
                "/" +
                d.getFullYear()+
                " - "+
                addZero(d.getHours()) +
                ":" +
                addZero(d.getMinutes()) +
                ":" +
                addZero(d.getSeconds())
                
                );
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table border="0">
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

    <div runat="server" id="divTable" visible="false" style="overflow-y:scroll; width:150%;">
        <hr />
        <table class='table table-bordered' style='max-width:1200px;font-size:13px; border:3px solid; border-style:outset; text-align:center;'>
            <tr style='font-weight:bold; background-color:lightgray;'>
                <td><asp:Label ID="lblOrder" runat="server" /></td>
                <td colspan="8"><asp:Label ID="lblValueOrder" runat="server" /></td>
            </tr>
            <tr style='font-weight:bold; background-color:white;'>
                <td><asp:Label ID="lblItem" runat="server" /></td>
                <td><asp:Label ID="lblDescription" runat="server" /></td>
                <td><asp:Label ID="lblQty" runat="server" /></td>
                <td><asp:Label ID="lblUnit" runat="server" /></td>
                <td><asp:Label ID="lblLot" runat="server" /></td>
                <td><asp:Label ID="lblShift" runat="server" /></td>
                <td><asp:Label ID="lblReason" runat="server" /></td>
                <td><asp:Label ID="lblRejectedType" runat="server" /></td>
                <td><asp:Label ID="lblExactReason" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:DropDownList runat="server" ID="slItems" CssClass="TextBox" style="width:150px;" ClientIDMode="Static" OnSelectedIndexChanged="slItems_OnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
                <td><asp:TextBox runat="server" ID="txtDescription" CssClass="TextBox" style="width:150px;" ClientIDMode="Static" ReadOnly="true"/></td>
                <td><asp:TextBox runat="server" TextMode="Number" step="any" ID="txtQty" CssClass="TextBox" style="width:40px;" ClientIDMode="Static" onblur="validarCantidad(this);"/></td>
                <td><asp:TextBox runat="server" ID="txtUnit" CssClass="TextBox" style="width:40px;" ClientIDMode="Static" ReadOnly="true" /></td>
                <td><asp:DropDownList runat="server" ID="slLot" CssClass="TextBox" style="width:100px;" ClientIDMode="Static"></asp:DropDownList></td>
                <td><asp:TextBox runat="server" ID="txtShift" CssClass="TextBox" style="width:25px;" ClientIDMode="Static" maxlength="1" onblur="validarShift(this);" /></td>
                <td><asp:DropDownList runat="server" ID="slReason" CssClass="TextBox" style="width:100px;" ClientIDMode="Static"></asp:DropDownList></td>
                <td><asp:DropDownList runat="server" ID="slRejectType" CssClass="TextBox" style="width:100px;" ClientIDMode="Static"></asp:DropDownList></td>
                <td><textarea rows="3" runat="server" id="txtExactReasons" CssClass="TextBox" style="width:150px;" ClientIDMode="Static"></textarea></td>
            </tr>
        </table>

    </div>
    <table runat="server" id="divBotones" visible="False" style="margin-bottom:10px; text-align:center; font-weight:bold;" cellspacing="0" cellpadding="0">
        <tr>
            <td><button class="btn btn-primary btn-lg" type="button"  onclick="javascript:printDiv('divLabel')">Print</button></td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <div runat="server" id="divLabel" clientidmode="Static" visible="false">
        
        <table style="font-size:small; font-weight:bold; text-align:center; width: 5.8in; height: 3.8in" border="1" cellspacing="0" cellpadding="0">
            <tr>
                <td><asp:Label runat="server" ID="lblDmtNumber"></asp:Label></td>
                <td ><asp:Label runat="server" ID="LblSqnb"></asp:Label></td>
                <td><img src="~/images/logophoenix_login.jpg" runat="server" id="imgCBSqnb" alt="" 
                        hspace="60" vspace="5" style="width: 2in; height: .5in;  margin:0px;" /></td>
            </tr>
            <tr>
                <td colspan="3"><img src="~/images/logophoenix_login.jpg" runat="server" 
                        id="imgCBMitm" alt="" hspace="60" vspace="5" 
                        style="width: 4in; height: .5in; margin:0px;"/></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="Label1"></asp:Label></td>
                <td colspan="2"><asp:Label runat="server" ID="lblDsca"></asp:Label></td>
            </tr>
            <tr>
                <td style="border-bottom:0px;"><asp:Label Text="" runat="server" ID="lblDescLot" />
                    <br />
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="imgBCClot" alt="" 
                        hspace="60" vspace="5" style="width: 2in; height: .5in;  margin:0px;"/></td>
                <td >
                    <asp:Label runat="server" ID="lblRejected"></asp:Label> &nbsp;<asp:Label ID="lblValueQuantity" runat="server"></asp:Label> &nbsp;<asp:Label ID="lblQtdl" runat="server"></asp:Label>
                </td>
                <td><img src="~/images/logophoenix_login.jpg" runat="server" id="imgBCQtdl" alt="" 
                        hspace="60" vspace="5" style="width: 2in; height: .5in;  margin:0px;"/></td>
            </tr>
            <tr>
                <td style="border-top:0px;">
                    <asp:Label ID="Label10" runat="server"></asp:Label>
                </td>
                <td >
                    <asp:Label runat="server" ID="Label11" /> -
                    <asp:Label ID="lblPdno" runat="server" />
                </td>
                <td><img src="~/images/logophoenix_login.jpg" runat="server" id="imgBCPdno" alt="" 
                        hspace="60" vspace="5" style="width: 2in; height: .5in;  margin:0px;"/></td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPrintedBy" runat="server" />:
                    <asp:Label ID="lblValuePrintedBy" runat="server" />
                </td>
                <td >
                    <asp:Label ID="lblFecha" runat="server" />
                </td>
                <td >
                    <asp:Label ID="lblMachinetitle" runat="server" />:
                    <asp:Label ID="lblMachine" runat="server" />
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="Label3" runat="server" /></td>
                <td colspan="2"><asp:Label ID="lblValueReason" runat="server" /></td>
            </tr>
            <tr>
                <td colspan="3" rowspan="2">
                    <asp:Label ID="lblComments" runat="server" />: 
                    <asp:Label ID="lblValueComments" runat="server" />
                </td>
            </tr>
        </table>
        <hr />
    </div>
    <asp:HiddenField runat="server" ID="hdfQuantity" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />

</asp:Content>

