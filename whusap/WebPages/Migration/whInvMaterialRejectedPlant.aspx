<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvMaterialRejectedPlant.aspx.cs" Inherits="whusap.WebPages.Migration.whInvMaterialRejectedPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        var idioma = '<%= _idioma %>';
        var numeroRegistros = <%= _validarOrden.Rows.Count %>
        function validarOrden() {
            var regex = /^-?\d*[0-9]*[.]?[0-9]*$/;
            var re = new RegExp(regex);
            var numeroOrden = document.getElementById('txtNumeroOrden').value.trim();
            var orden = numeroOrden.substr(9, 1);
            var guion = numeroOrden.substr(9, 1);
            var pallet = numeroOrden.substr(10, 3);
            
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

        function validarShift(field) {
            var valor = field.value.toUpperCase();
            if (valor == 'A' || valor == 'B' || valor == 'C' || valor == 'D') {
            }
            else {
                alert(idioma == "INGLES" ? "Values must be A, B C or D" : "El valor debe ser A, B, C o D");
                this.focus();
                field.value = '';
            }
        }

        function validarFormulario() {
            debugger
            var validate = true;
            var mensaje = "";
            var dataSave = false;

            for (var i = 0; i < numeroRegistros; i++) {

                var txtShift = $('#txtShift-' + i).val().toUpperCase();
                var txtComments = $('#txtExactReason-' + i).val();

                if (txtShift.trim() != "") {
                    if (txtShift == 'A' || txtShift == 'B' || txtShift == 'C' || txtShift == 'D') {
                    } else {
                        validate = false;
                        mensaje += idioma == "INGLES" ? "-A valid value to field Shift - Row " + (i + 1) + "\n" : "-Un valor valido para el campo Cambio - Fila " + (i + 1) + "\n";
                    }

                    if (txtComments.trim() == "") {
                        validate = false;
                        mensaje += idioma == "INGLES" ? "-Exact reason - Row " + (i + 1) + "\n" : "-Razón exacta- Fila" + (i + 1) + "\n";
                    }
                }else
                {
                    if (txtComments.trim() != "") {
                        if (txtShift.trim() == "") 
                        {
                             validate = false;
                            mensaje += idioma == "INGLES" ? "-Shift- Row " + (i + 1) + "\n" : "-Cambio - Fila " + (i + 1) + "\n";
                        }else
                        {
                             if (txtShift == 'A' || txtShift == 'B' || txtShift == 'C' || txtShift == 'D') {
                            } else {
                                validate = false;
                                mensaje += idioma == "INGLES" ? "-A valid value to field Shift - Row " + (i + 1) + "\n" : "-Un valor valido para el campo Cambio - Fila " + (i + 1) + "\n";
                            }
                        }
                    }
                }

                if (txtShift.trim() != "" && txtComments.trim() != "")
                {
                    dataSave = true;
                }
            }

            debugger;
            if (!validate) {
                alert((idioma == "INGLES" ? "Please, enter this fields:\n" : "Por favor ingrese los siguientes campos:\n") + mensaje);
            }else if(!dataSave)
            {
                validate = false;
                alert(idioma == "INGLES" ? "Please, enter information to save" : "Por favor ingrese información para guardar");
            }

            return validate;
        
        }

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
            var LbdDate = $("#Contenido_lblFechaDate");
            LbdDate.text(

                monthNames[d.getMonth()] +
                "/" +
                d.getDate() +
                "/" +
                d.getFullYear() +
                " - " +
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
    

    <div runat="server" id="divTable" clientidmode="Static" visible="false"></div>

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
                <td><asp:Label runat="server" ID="lblDescription"></asp:Label></td>
                <td colspan="2"><asp:Label runat="server" ID="lblDsca"></asp:Label></td>
            </tr>
            <tr>
                <td style="border-bottom:0px;"><asp:Label runat="server" ID="lblTitleMachine" />-<asp:Label 
                        runat="server" ID="lblDescMachine" /></td>
                <td >
                    <asp:Label runat="server" ID="lblRejected"></asp:Label> -
                    <asp:Label ID="lblValueQuantity" runat="server"></asp:Label> &nbsp;<asp:Label ID="lblQtdl" runat="server"></asp:Label>
                </td>
                <td><img src="~/images/logophoenix_login.jpg" runat="server" id="imgBCQtdl" alt="" 
                        hspace="60" vspace="5" style="width: 2in; height: .5in;  margin:0px;"/></td>
            </tr>
            <tr>
                <td style="border-top:0px;">
                    <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
                <td >
                    <asp:Label runat="server" ID="Label1" /> -
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
                <td colspan="2">
                    <asp:Label ID="lblFecha" runat="server" />&nbsp
                    <asp:Label ID="lblFechaDate" runat="server" />&nbsp
                    
                </td>
            </tr>
            <tr>
                <td><asp:Label ID="lblReason" runat="server" /></td>
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

    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />

</asp:Content>


    

    