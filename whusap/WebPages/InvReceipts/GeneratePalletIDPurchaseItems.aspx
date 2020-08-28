<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="GeneratePalletIDPurchaseItems.aspx.cs" Inherits="whusap.WebPages.InvReceipts.GeneratePalletIDPurchaseItems" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <style>
        #MyEtiquetaOC,#MyEtiqueta
        {
            display: none;
        }
        #LblUnitOC
        {
            font-size:14px;
        }
        #lblError
        {
            font-size:14px;
            color:Red;
        }
        
        label
        {
            font-size:14px;
        }
    </style>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txItem">
            Item</label>
        <div class="col-sm-4">
            <input type="text" class="form-control form-control-lg" id="txItem" placeholder="Item">
        </div>
        <label id="lblItem" for="txItem">
        </label>
    </div>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txLot">
            Lot</label>
        <div class="col-sm-4">
            <input type="text" class="form-control form-control-lg" id="txLot" placeholder="Lot">
        </div>
    </div>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txQuantity">
            Warehouse</label>
        <div class="col-sm-4">
            <input type="text" class="form-control form-control-lg" id="txWarehouse" placeholder="Warehouse">
        </div>
        <label id="lblWarehouse" for="txWarehouse">
        </label>
    </div>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txLocation">
            Location</label>
        <div class="col-sm-4">
            <input type="text" class="form-control form-control-lg" id="txLocation" placeholder="Location">
        </div>
    </div>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txQuantity">
            Quantity</label>
        <div class="col-sm-4">
            <input type="text" class="form-control form-control-lg" id="txQuantity" placeholder="Quantity">
        </div>
        <label id="lblQuantity" for="txQuantity">
        </label>
    </div>
    <div class="form-group row">
        <input id="btnSave" type="button" class="btn btn-primary btn-lg" value="SAVE" />
    </div>
    <div class="form-group row">
        <label id="lblError">
        </label>
    </div>
    <div class="form-group row">
        <div id="MyEtiqueta">
            <table style="margin: auto">
                <tr>
                    <td colspan="4">
                        <img src="~/images/logophoenix_login.jpg" runat="server" id="CBPalletNO" alt="" hspace="60"
                            vspace="5" style="width: 4in; height: .5in;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="font-size: 14px">
                            ITEM</label>
                    </td>
                    <td>
                        <label id="lblItemID    " style="display: none; font-size: 11px">
                        </label>
                        <img src="~/images/logophoenix_login.jpg" runat="server" id="CBItem" alt="" hspace="60"
                            vspace="5" style="width: 3in; height: .5in;" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="center">
                        <label id="lblItemDesc" style="font-size: 14px">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="font-size: 14px">
                            QUANTITY</label>
                    </td>
                    <td>
                        <label id="Label1" style="display: none; font-size: 11px">
                        </label>
                        <img src="~/images/logophoenix_login.jpg" runat="server" id="CBQuantity" alt="" hspace="60"
                            vspace="5" style="width: 1in; height: .5in;" />
                        <label id="LblUnitOC">
                            
                        </label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="center">
                        <label id="LblUnit" style="font-size: 11px">
                        </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="font-size: 14px">
                            LOT</label>
                    </td>
                    <td>
                        <label id="LblLotId" style="display: none; font-size: 11px">
                        </label>
                        <img src="~/images/logophoenix_login.jpg" runat="server" id="CBLot" alt="" hspace="60"
                            vspace="5" style="width: 3in; height: .5in;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label style="font-size: 14px">
                            RECEIPT DATE</label>
                    </td>
                    <td>
                        <label id="LblDate" style="font-size: 14px">
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
                                UNIT</label>
                        </center>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <img src="~/images/logophoenix_login.jpg" runat="server" id="CBUnit" alt="" hspace="60"
                            vspace="5" style="width: 2in; height: .5in;" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <center>
                            <label>
                                QUANTITY</label>
                            <label style="display: none" id="LblQuantityOC">
                            </label>
                        </center>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <script>
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
            //mywindow.document.write('<h1>' + document.title + '</h1>');
            mywindow.document.write(document.getElementById(divID).innerHTML);
            mywindow.document.write('</body></html>');

            mywindow.document.close(); // necessary for IE >= 10
            mywindow.focus(); // necessary for IE >= 10*/

            mywindow.print();
            mywindow.close();

            return true;
        };
        
        var timer;
        function stoper() {

            clearTimeout(timer);
        }


        function IniciarComponentes() {

            txItem = $('#txItem');
            txLot = $('#txLot');
            txWarehouse = $('#txWarehouse');
            txLocation = $('#txLocation');
            txQuantity = $('#txQuantity');

            lblItem = $('#lblItem');
            lblWarehouse = $('#lblWarehouse');
            lblQuantity = $('#lblQuantity');

            btnSave = $('#btnSave');

        };

        IniciarComponentes();


        function BloquearComponentes() {

            $('#txLot').prop("disabled", true);
            $('#txWarehouse').prop("disabled", true);
            $('#txLocation').prop("disabled", true);
            $('#txQuantity').prop("disabled", true);
                $('#btnSave').prop("disabled", true);

        };
        BloquearComponentes();
        var SuccesVerificarItem = function (r) {
            BloquearComponentes();
            $('#lblWarehouse').html("");
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                $('#txLot').val("");
                $('#txWarehouse').val("");
                $('#txLocation').val("");
                $('#txQuantity').val("");
                $('#lblItem').html("");
                $('#lblWarehouse').html("");
                $('#lblQuantity').html("");

                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
                BloquearComponentes();
            }
            if (MyObj.Error == false) {
                $('#lblError').html("");

                $('#txLot').val("");
                $('#txWarehouse').val("");
                $('#txLocation').val("");
                $('#txQuantity').val("");
                $('lblItem').html("");
                $('lblWarehouse').html("");
                $('lblQuantity').html("");

                lblItem.html(MyObj.dsca);
                lblQuantity.html(MyObj.cuni);
                if (MyObj.kltc == "1") {
                    $('#txLot').prop("disabled", false);
                }
                else {
                    $('#txLot').prop("disabled", true);
                    $('#txWarehouse').prop("disabled", false);
                    $('#txLocation').prop("disabled", true);
                    $('#txQuantity').prop("disabled", true);
                }
            }

        }

        var SuccesVerificarLote = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
                $('#txWarehouse').prop("disabled", true);
                $('#txLocation').prop("disabled", true);
                $('#txQuantity').prop("disabled", true);

                $('#txWarehouse').val("");
                $('#txLocation').val("");
                $('#txQuantity').val("");
                $('lblWarehouse').html("");
            }
            if (MyObj.Error == false) {

                $('#lblError').html("");
                $('#txWarehouse').prop("disabled", false);

                $('#txWarehouse').val("");
                $('#txLocation').val("");
                $('#txQuantity').val("");
                $('lblWarehouse').html("");
            }
        }

        var SuccesVerificarWarehouse = function (r) {
            var MyObj = JSON.parse(r.d);
            $('#txLocation').val("");
            $('#txQuantity').val("");
            $('#txLocation').prop("disabled", true);
            $('#txQuantity').prop("disabled", true);
            if (MyObj.Error == true) {
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
                $('#txLocation').prop("disabled", true);
                $('#txQuantity').prop("disabled", true);

                $('#txLocation').val("");
                $('#txQuantity').val("");
                $('#lblWarehouse').html("");
            }
            if (MyObj.Error == false) {
                $('#lblError').html("");

                $('#lblWarehouse').html(MyObj.dsca)
                if (MyObj.sloc == "1") {
                    $('#txLocation').prop("disabled", false);
                    $('#txQuantity').prop("disabled", true);
                }
                else {
                    $('#txQuantity').prop("disabled", false);


                }
            }
        }

        var SuccesVerificarLocation = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
                $('#txQuantity').prop("disabled", true);
                $('#txQuantity').val("");
            }
            if (MyObj.Error == false) {
                $('#lblError').html("");
                $('#txQuantity').prop("disabled", false);
            }
        }

        var SuccesVerificarQuantity = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
            }
            if (MyObj.Error == false) {
                $('#lblError').html("");
                if (parseInt($('#txQuantity').val()) > 0 && parseInt($('#txQuantity').val()) <= parseInt(MyObj.stks, 10)) {
                    $('#btnSave').prop("disabled", false);
                    
                }
                else {
                    $('#btnSave').prop("disabled", true);
                    ImprimirMensaje(MyObj.TypeMsgJs, MyObj.SuccessMsg);
                }
            }
        }


        function ImprimirMensaje(type, msg) {
            switch (type) {
                case "alert":
                    alert(msg);
                    break;
                case "console":
                    console.log(msg);
                    break;
                case "label":
                    $('#lblError').html(msg);
                    break;
            }
        }
        var SuccesClick_Save = function (r) {
            MyObject = JSON.parse(r.d);

            if (MyObject.error == false) {
                //Etiqueta Sin orden de compra

                $('#Contenido_CBPalletNO').attr("src", MyObject.PAID_URL);
                $('#Contenido_lblItemID').html(MyObject.ITEM);
                $('#Contenido_lblItemDesc').html(MyObject.DSCA);
                $('#Contenido_LblQuantity').html(MyObject.QTYC);
                $('#Contenido_LblUnit').html(MyObject.UNIC);
                $('#Contenido_LblLotId').html(MyObject.CLOT);

                $('#Contenido_CBPurchaseOrder').attr("src", MyObject.ORNO_URL);
                $('#Contenido_CBItem').attr("src", MyObject.ITEM_URL);
                $("#Contenido_CBLot").attr("src", MyObject.CLOT_URL);
                if (MyObject.CLOT_URL == "") {
                    $('#Contenido_CBLot').hide();
                }
                else {
                    $('#Contenido_CBLot').show();
                }
                $('#Contenido_CBQuantity').attr("src", MyObject.QTYC_URL);
                $('#Contenido_CBUnit').attr("src", MyObject.UNIC_URL);

                $('#Contenido_LblPurchaseOC').html(MyObject.ORNO);
                $('#Contenido_LblItemOC').html(MyObject.ITEM);
                $('#Contenido_LblLotOC').html(MyObject.CLOT);
                $('#LblUnitOC').html(MyObject.UNIT);
                $('#Contenido_LblQuantityOC').html(MyObject.QTYC);

                $('#txItem').val("");
                $('#txLot').val("");
                $('#txWarehouse').val("");
                $('#txLocation').val("");
                $('#txQuantity').val("");
                $('#lblItem').html("");
                $('#lblWarehouse').html("");
                $('#lblQuantity').html("");
                $('#lblError').html("");
                if (MyObject.OORG != "2" && MyObject != undefined) {
                    //btnMyEtiqueta.show();
                }
                else if (MyObject != undefined) {
                    //btnMyEtiqueta.show();
                }

                //DeshabilitarLimpiarControles();
                printDiv('MyEtiqueta');
                $('#btnSave').prop("disabled", true);

            }
            else {
                console.log("El registro no se realizo");
                alert(MyObject.errorMsg);
            }

        }


        var VerificarItem = function () {
            $('#btnSave').prop("disabled", true);
            var Data = "{'ITEM':'" + $('#txItem').val() + "'}";
            sendAjax("VerificarItem", Data, SuccesVerificarItem)
        }

        var VerificarLote = function () {
            $('#btnSave').prop("disabled", true);
            var Data = "{'ITEM':'" + $('#txItem').val() + "','CLOT':'" + $('#txLot').val() + "'}";
            sendAjax("VerificarLote", Data, SuccesVerificarLote)
        }

        var VerificarWarehouse = function () {
            $('#btnSave').prop("disabled", true);
            var Data = "{'CWAR':'" + $('#txWarehouse').val() + "'}";
            sendAjax("VerificarWarehouse", Data, SuccesVerificarWarehouse)
        }

        var VerificarLocation = function () {
            $('#btnSave').prop("disabled", true);
            var Data = "{'CWAR':'" + $('#txWarehouse').val() + "','LOCA':'" + $('#txLocation').val() + "'}";
            sendAjax("VerificarLocation", Data, SuccesVerificarLocation)
        }

        var VerificarQuantity = function () {
            $('#btnSave').prop("disabled", true);
            var Data = "{'CWAR':'" + $('#txWarehouse').val() + "','ITEM':'" + $('#txItem').val() + "','CLOT':'" + $('#txLot').val() + "','LOCA':'" + $('#txLocation').val() + "'}";
            sendAjax("VerificarQuantity", Data, SuccesVerificarQuantity)
        }

        var Click_Save = function () {

            var Data = "{'CWAR':'" + $('#txWarehouse').val().trim() + "','ITEM':'" + $('#txItem').val().trim().toUpperCase() + "','CLOT':'" + $('#txLot').val().trim() + "','LOCA':'" + $('#txLocation').val().trim() + "','QTYS':'" + $('#txQuantity').val().trim() + "','UNIT':'" + $('#lblQuantity').html() + "'}";
            sendAjax("Click_Save", Data, SuccesClick_Save);

        }

        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "GeneratePalletIDPurchaseItems.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }


        txItem.bind('paste keyup', function () {
            stoper();
            timer = setTimeout("VerificarItem()", 1000);
        });

        txLot.bind('paste keyup', function () {
            stoper();
            timer = setTimeout("VerificarLote()", 1000);
        });

        txWarehouse.bind('paste keyup', function () {
            stoper();
            timer = setTimeout("VerificarWarehouse()", 1000);
        });

        txLocation.bind('paste keyup', function () {
            stoper();
            timer = setTimeout("VerificarLocation()", 1000);
        });

        txQuantity.bind('paste keyup', function () {
            stoper();
            timer = setTimeout("VerificarQuantity()", 1000);
        });

        btnSave.bind('click', function () {
            Click_Save();
        });

    </script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js"
        integrity="sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1"
        crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"
        integrity="sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM"
        crossorigin="anonymous"></script>
    <script src="https://code.jquery.com/jquery-3.1.1.min.js"></script>
</asp:Content>
