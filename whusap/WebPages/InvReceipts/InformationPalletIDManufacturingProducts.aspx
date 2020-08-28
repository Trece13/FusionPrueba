<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="InformationPalletIDManufacturingProducts.aspx.cs" Inherits="whusap.WebPages.InvReceipts.InformationPalletIDManufacturingProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
<style>
        #lblError
        {
            font-size : 15px;
            color : Red;
        }
</style>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txPalletID">
            PalletID</label>
        <div class="col-sm-4">
            <input type="text" class="form-control form-control-lg" id="txPalletID" placeholder="PalletID">
        </div>
        <label id="lblPalletID" for="txPalletID">
        </label>
    </div>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txItem">
            Work Order</label>
        <div class="col-sm-4">
            <label id="lblWorkOrder" for="txWorkOrder">
        </label>
        </div>
    </div>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txItem">
            Item</label>
        <div class="col-sm-4">
            <label id="lblItem" for="txItem">
        </label>
        </div>
        <label id="lblItemDsca" for="txItem">
        </label>
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


    <script>
        var ktlc = ""; 
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
            //            //Get the HTML of div
            //            var divElements = document.getElementById(divID).innerHTML;
            //            //Get the HTML of whole page
            //            var oldPage = document.body.innerHTML;
            //            //Reset the page's HTML with div's HTML only
            //            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body></html>";
            //            //Print Page
            //            window.print();
            //            //Restore orignal HTML
            //            document.body.innerHTML = oldPage;
            //            window.close();
            //            return true;

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

            //$('#txItem').prop("disabled", true); 
            $('#txLot').prop("disabled", true);
            $('#txWarehouse').prop("disabled", true);
            $('#txLocation').prop("disabled", true);
            $('#txQuantity').prop("disabled", true);
            $('#btnSave').prop("disabled", true);

        };
        BloquearComponentes();

        var SuccesVerificarPalletID = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
                $('#lblWorkOrder').html("");
                $('#lblItem').html("");
                $('#lblItemDsca').html("");
                $('#txWarehouse').val("");
                $('#txLocation').val("");
                $('#txQuantity').val("");
                $('#lblQuantity').html("");
                $('#txWarehouse').prop("disabled", true);
                $('#txLocation').prop("disabled", true);
                $('#txQuantity').prop("disabled", true);
            }
            else {
                $('#lblWorkOrder').html(MyObj.pdno);
                $('#lblItem').html(MyObj.mitm);
                $('#lblItemDsca').html(MyObj.dsca);
                $('#lblQuantity').html(MyObj.cuni)
                $('#txWarehouse').prop("disabled", false);
                $('#lblError').html("");
                
            }
        };

        var SuccesVerificarItem = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                    ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
                BloquearComponentes();
            }
            if (MyObj.Error == false) {
                $('#lblError').html("");
                lblItem.html(MyObj.dsca);
                lblQuantity.html(MyObj.cuni);
                ktlc = MyObj.kltc;
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
            }
            if (MyObj.Error == false) {
                $('#lblError').html("");
                $('#txWarehouse').prop("disabled", false);
            }
        }

        var SuccesVerificarWarehouse = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
                $('#txLocation').prop("disabled", true);
                $('#txQuantity').prop("disabled", true);
                $('#txLocation').val("");
                $('#txQuantity').val("");
            }
            if (MyObj.Error == false) {
                $('#lblError').html("");
                lblWarehouse.html(MyObj.DESCRIPCION)
                if (MyObj.sloc == "1") {
                    $('#txLocation').prop("disabled", false);
                    $('#txQuantity').prop("disabled", true);
                }
                else {
                    $('#txQuantity').prop("disabled", false);
                    $('#txLocation').prop("disabled", true);
                }
            }
        }

        var SuccesVerificarLocation = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
                $('#txQuantity').prop("disabled", true);
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
                if ($('#txQuantity').val() > 0 && $('#txQuantity').val() <= parseInt(MyObj.stks, 10)) {
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

            if (MyObject.Error == false) {
                $('#txPalletID').val("");
                $('#lblWorkOrder').html("");
                $('#lblItem').html("");
                $('#lblItemDsca').html("");
                $('#txWarehouse').val("");
                $('#txLocation').val("");
                $('#txQuantity').val("");
                $('#lblQuantity').html("");
                $('#btnSave').prop("disabled", true);
                alert("Registration was successful");

            }
            else {
                console.log("El registro no se realizo");
                alert(MyObject.errorMsg);
            }

        }


        var VerificarPalletID = function () {
            $('#btnSave').prop("disabled", true);
            var Data = "{'PAID':'" + $('#txPalletID').val() + "'}";
            sendAjax("VerificarPalletID", Data, SuccesVerificarPalletID)
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
            var Data = "{'CWAR':'" + $('#txWarehouse').val() + "','ITEM':'" + $('#lblItem').html() + "','LOCA':'" + $('#txLocation').val() + "','CLOT':'" + (ktlc == "1" ? $('#lblWorkOrder').html() : " ") + "'}";
            sendAjax("VerificarQuantity", Data, SuccesVerificarQuantity)
        }

        var Click_Save = function () {

            var Data = "{'PAID':'" + $('#txPalletID').val() + "','ORNO':'" + $('#lblWorkOrder').html() + "','ITEM':'" + $('#lblItem').html() + "','CWAR':'" + $('#txWarehouse').val() + "','LOCA':'" + $('#txLocation').val() + "','UNIT':'" + $('#lblQuantity').html() + "','QTYS':'" + $('#txQuantity').val() + "'}";
            sendAjax("Click_Save", Data, SuccesClick_Save);

        }

        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "InformationPalletIDManufacturingProducts.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }


        $('#txPalletID').bind('paste keyup', function () {
            stoper();
            timer = setTimeout("VerificarPalletID()", 1000);
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
