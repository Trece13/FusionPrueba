<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="whInvReprintReceiptRawMaterial.aspx.cs" Inherits="whusap.WebPages.InvReceipts.whInvReprintReceiptRawMaterial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <script type="text/javascript">
        function printDiv(divID) {

            var monthNames = [
                "1", "2", "3",
                "4", "5", "6", "7",
                "8", "9", "10",
                "11", "12"
              ];

            //PRINT LOCAL HOUR
            var d = new Date();
            var LbdDate = $("#LblDate");
            LbdDate.html(
                monthNames[d.getMonth()] +
                "/" +
                d.getDate() +
                "/" +
                d.getFullYear() +
                " " +
                d.getHours() +
                ":" +
                d.getMinutes() +
                ":" +
                d.getSeconds()
                );

            var mywindow = window.open('', 'PRINT', 'height=400,width=600');

            mywindow.document.write('<html><head><title>' + document.title + '</title>');
            mywindow.document.write('</head><body >');
            mywindow.document.write(document.getElementById(divID).innerHTML);
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/

            mywindow.print();
            mywindow.close();

            return true;
        };
    </script>
    <style type="text/css">
        #MyEtiquetaOC, #MyEtiqueta
        {
            display: none;
        }
        
        #btnMyEtiquetaOC, #btnMyEtiqueta
        {
            display: none;
        }
    </style>
    <form id="form1" class="container">
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txQuantity">
            Pallet ID</label>
        <div class="col-sm-4">
            <input type="text" class="form-control form-control-lg" id="txPalletID" placeholder="Pallet ID"
                data-method="ValidarQuantity">
        </div>
        <label id="lblUnis" for="txQuantity">
        </label>
    </div>
    <div class="form-group row">
        <input id="btnEnviar" type="button" class="btn btn-primary btn-lg" value="Query" />&nbsp
        <button id="btnMyEtiqueta" class="btn btn-primary btn-lg" type="button" onclick="javascript:printDiv('MyEtiqueta')">
            Print</button>&nbsp
    </div>
    </form>
    <div id="MyEtiqueta">
        <table style="margin:auto">
            <tr>
                <td colspan="4">
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBPalletNO" alt="" hspace="60"
                        vspace="5" style="width: 4in; height: .5in;" />
                </td>
            </tr>
            <tr>
                <td>
                    <label style = "font-size : 14px">
                        ITEM</label>
                </td>
                <td>
                    <label id="lblItemID" style="display:none">
                    </label>
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBItem" alt="" hspace="60"
                        vspace="5" style="width: 3in; height: .5in;" />
                </td>
  
            </tr>
            <tr>
                <td></td>
                <td><label id="lblItemDesc" style="font-size : 14px">
                    </label></td>
            </tr>

            <tr>
                <td>
                    <label style = "font-size : 14px">
                        QUANTITY</label>
                </td>
                <td>
                    <label id="LblQuantity" style="display:none;font-size : 11px">
                    </label>
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBQuantity" alt="" hspace="60"
                        vspace="5" style="width: 1.5in; height: .5in;" />

                        <label style = "font-size : 14px" id="lblUnit"/>
                        
                </td>
            </tr>
             <tr>
                <td>
                </td>
                <td>
                      <label id="LblUnit" style="display:none">
                    </label>  
                </td>
            </tr>
            <tr>
                <td>
                    <label style = "font-size : 14px">
                        LOT</label>
                </td>
                <td>
                    <label id="LblLotId" style="display:none; font-size : 11px">
                    </label>
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBLot" alt="" hspace="60"
                        vspace="5" style="width: 3in; height: .5in;" />
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td>
                    <label style = "font-size : 14px">
                        RECEIPT DATE:</label>
                </td>
                <td>
                    <label id="LblDate" style = "font-size : 14px">
                    </label>
                </td>
                <td>
                    <label style = "font-size : 14px">
                        REPRINT:</label><label id="LblReprint" style = "font-size : 14px">
                    </label>
                </td>
            </tr>
        </table>
    </div>
    <div id="MyEtiquetaOC">
        <table>
            <tr>
                <td colspan="2">
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBPurchaseOrder" alt=""
                        hspace="60" vspace="5" style="width: 2in; height: .5in;" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <center>
                        <label>
                            PURCHASE ORDER</label>
                        <label style="display: none" id="LblPurchaseOC">
                        </label>
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <center>
                        <label>
                            ITEM</label>
                        <label style="display: none" id="LblItemOC">
                        </label>
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <center>
                        <label>
                            LOT</label>
                        <label style="display: none" id="LblLotOC">
                        </label>
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <center>
                        <label>
                            UNIT
                        </label>
                        <label style="display: none" id="LblUnitOC">
                        </label>
                    </center>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <center>
                        <label>
                            QUANTITY
                        </label>
                        <label style="display: none" id="LblQuantityOC">
                        </label>
                    </center>
                </td>
            </tr>
        </table>
    </div>
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo"
        crossorigin="anonymous"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"
        integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1"
        crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"
        integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM"
        crossorigin="anonymous"></script>
    <script type="text/javascript">

        function sendAjax(WebMethod, Data, FuncitionSucces) {
            var options = {
                type: "POST",
                url: "whInvReprintReceiptRawMaterial.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

        }

        function IdentificarControles() {


            MyEtiquetaOC = $('#MyEtiquetaOC');
            MyEtiqueta = $('#MyEtiqueta');

            //Formulario

            txPalletID = $('#txPalletID');

            btnEnviar = $('#btnEnviar');

            btnMyEtiqueta = $('#btnMyEtiqueta');
            //btnMyEtiquetaOC = $('#btnMyEtiquetaOC');

            //Etiqueta OC

            Contenido_CBPurchaseOrder = $('#Contenido_CBPurchaseOrder');
            LblPurchaseOC = $('#LblPurchaseOC');

            Contenido_CBItem = $('#Contenido_CBItem');
            LblItemOC = $('#LblItemOC');

            Contenido_CBLot = $('#Contenido_CBLot');
            lbllotoc = $('#lbllotoc');

            Contenido_CBQuantity = $('#Contenido_CBQuantity');
            LblUnitOC = $('#LblUnitOC');

            Contenido_CBUnit = $('#Contenido_CBUnit');
            LblQuantityOC = $('#LblQuantityOC');

            lblUnit = $('#lblUnit');
            //Etiqueta NO OC

            Contenido_CBPalletNO = $('#Contenido_CBPalletNO');

            lblItemID = $('#lblItemID');
            lblItemDesc = $('#lblItemDesc');

            LblQuantity = $('#LblQuantity');
            LblUnit = $('#LblUnit');

            LblLotId = $('#LblLotId');
            LblDate = $('#LblDate');
            LblReprint = $('#LblReprint');

        }

        var FuncitionSuccesReprint = function (r) {
            MyEtiqueta.hide('slow');
            MyEtiquetaOC.hide('slow');

            if (r.d != "The Pallet ID does not exist") {
                var MyObject = JSON.parse(r.d);

                Contenido_CBPurchaseOrder.attr("src", MyObject.ORNO_URL);
                LblPurchaseOC.html(MyObject.ORNO);

                Contenido_CBItem.attr("src", MyObject.ITEM_URL);
                LblItemOC.html(MyObject.ITEM);

                Contenido_CBLot.attr("src", MyObject.CLOT_URL)
                if (MyObject.CLOT_URL == "") {
                    Contenido_CBLot.hide();
                }
                lbllotoc.html(MyObject.CLOT);

                Contenido_CBQuantity.attr("src", MyObject.QTYC_URL)
                LblUnitOC.html(MyObject.UNIT);

                Contenido_CBUnit.attr("src", MyObject.UNIC_URL)
                LblQuantityOC.html(MyObject.QTYC);
                lblUnit.html(MyObject.UNIT);
                //Etiqueta NO OC

                Contenido_CBPalletNO.attr("src", MyObject.PAID_URL)

                lblItemID.html(MyObject.ITEM);
                lblItemDesc.html(MyObject.DSCA);

                LblQuantity.html(MyObject.QTYC);
                LblUnit.html(MyObject.UNIT);

                LblLotId.html(MyObject.CLOT);
                //LblDate.html();
                LblReprint.html(MyObject.NPRT);

                if (MyObject.OORG.trim() == "2") {
                    MyEtiqueta.show('slow');
                    //MyEtiqueta.hide('slow');

                    //btnMyEtiqueta.hide('slow');
                    btnMyEtiqueta.show('slow');
                }
                else {
                    MyEtiqueta.show('slow');
                    //MyEtiquetaOC.hide('slow');


                    btnMyEtiqueta.show('slow');
                    //btnMyEtiquetaOC.hide('slow');
                }
            }
            else {
                MyEtiqueta.hide('slow');
                //MyEtiquetaOC.hide('slow');
                btnMyEtiqueta.hide('slow');
                //btnMyEtiquetaOC.hide('slow');
                alert(r.d);
            }

        }



        $(function () {

            IdentificarControles();

            btnEnviar.click(function () {
                var Data = "{'PAID':'" + txPalletID.val().toUpperCase() + "'}";
                sendAjax("ReprintLabel", Data, FuncitionSuccesReprint)
            });


        });


    </script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"
        integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1"
        crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"
        integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM"
        crossorigin="anonymous"></script>
        <script src="https://code.jquery.com/jquery-3.1.1.min.js">
</asp:Content>
