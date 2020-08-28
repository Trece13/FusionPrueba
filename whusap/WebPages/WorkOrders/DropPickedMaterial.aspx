<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="DropPickedMaterial.aspx.cs" Inherits="whusap.WebPages.WorkOrders.DropPickedMaterial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    <style>
        .InputIncorrecto
        {
            border-bottom: solid 3px red;
            color: Red;
        }
        .InputIncorrecto:focus
        {
            color: red;
        }
        .InputCorrecto
        {
            border-bottom: solid 3px green;
            color: green;
        }
        .InputCorrecto:focus
        {
            color: green;
        }
        #DetallePallet
        {
            display: none;
        }
        #LblEtiqueta
        {
            display: none;
        }
        #diseño
        {
            width: 6in;
            height: 4in;
            border: solid 1px black;
        }
        .datoEti
        {
            justify-content: center;
        }
        #txPalletID
        {
            width: 300px;
        }
        #centrado
        {
            margin-top: 120px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <form id="form1" class="container">
    <div id="divForm">
        <div class="form-group row ">
            <label class="col-sm-2 col-form-label-lg" for="txCustomer" id="lblpalletID">
                Pallet ID
            </label>
            <div class="col-sm-6">
                <input type="text" class="form-control form-control-lg col-sm-12" id="txPalletID"
                    placeholder="Pallet ID" onkeyup="SearchPalletIDTimer()" />
            </div>
        </div>
        <div id="DetallePallet">
            <div class="form-group row ">
                <label class="col-sm-2 col-form-label-lg" for="txCustomer" id="lblItem">
                    Item
                </label>
                <div class="col-sm-3">
                    <label class="col-sm-12 col-form-label-lg" for="txCustomer" id="lblItemID">
                    </label>
                </div>
                <div class="col-sm-3">
                    <label class="col-sm-12 col-form-label-lg" for="txCustomer" id="lblItemDesc">
                    </label>
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-2 col-form-label-lg" for="txCustomer" id="lblQuantity">
                    Quantity
                </label>
                <div class="col-sm-3">
                    <label class="col-sm-12 col-form-label-lg" id="LblQuantityVal">
                    </label>
                </div>
                <div class="col-sm-3">
                    <label class="col-sm-12 col-form-label-lg" id="LblQuantityUnit">
                    </label>
                </div>
            </div>
            <div class="form-group row" id="divQueryAction">
                <div class="col-sm-2">
                </div>
                <div class="col-sm-4">
                    <input type="button" class="btn btn-primary btn-lg col-sm-7 " id="btnDropTagPick"
                        value="Drop & Tag Pick" onclick="ClickDropTagPick()" />
                </div>
            </div>
        </div>
    </div>
    <div id="LblEtiqueta">
        <div id="diseño" style="width: 6in; height: 4in; border: solid 1px black;">
            <div id="centrado" style="margin-top: 120px;">
                <div class="form-group row datoEti" style="padding-left: 200px;">
                    <label class="col-sm-2 col-form-label-lg" id="Label1" style="font-size:42px">
                        Work Order: <label class="col-sm-12 col-form-label-lg" id="lblWorkOrder" style="font-size:42px"></label>
                    </label>
                    <div class="col-sm-3">
                        
                    </div>
                </div>
                <div class="form-group row datoEti" style="padding-left: 200px;">
                    <label class="col-sm-2 col-form-label-lg" for="txCustomer" id="Label4" style="font-size:42px">
                        Machine: <label class="col-sm-12 col-form-label-lg" id="lblMachine" style="font-size:42px"></label>
                    </label>
                    <div class="col-sm-3">
                        
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <label id="lblMsg">
    </label>
    </form>
    <script>


        var timer;
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

        function addZero(i) {
            if (i < 10) {
                i = "0" + i;
            }
            return i;
        };
        function stoper() {
            clearTimeout(timer);
        }

        function SearchPalletIDTimer() {
            stoper();
            timer = setTimeout("SearchPalletID()", 2000);
        }

        $(function () {
            IniciarComponentes();
        })

        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "DropPickedMaterial.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: asyncMode != undefined ? asyncMode : true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }

        function IniciarComponentes() {

            txPalletID = $('#txPalletID');
            lblItemID = $('#lblItemID');
            lblItemDesc = $('#lblItemDesc');
            LblQuantityVal = $('#LblQuantityVal');
            LblQuantityUnit = $('#LblQuantityUnit');
            btnDropTagPick = $('#btnDropTagPick');
            DetallePallet = $('#DetallePallet');

            LblEtiqueta = $('#LblEtiqueta');
            lblWorkOrder = $('#lblWorkOrder');
            lblMachine = $('#lblMachine');

        }

        var SearchPalletID = function () {
            //var Data = "{'key':'" + value + "'}";
            var Data = "{'PalletID':'" + $('#txPalletID').val().toUpperCase() + "'}";
            WebMethod = "SearchPalletID";
            sendAjax(WebMethod, Data, SearchPalletIDSuccess, false);
        }

        var ClickDropTagPick = function () {
            //var Data = "{'key':'" + value + "'}";
            var Data = "{'PalletID':'" + $('#txPalletID').val().toUpperCase() + "'}";
            WebMethod = "ClickDropTagPick";
            sendAjax(WebMethod, Data, ClickDropTagPickSuccess, false);
        }

        function SearchPalletIDSuccess(r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                txPalletID.addClass("InputIncorrecto");
                txPalletID.removeClass("InputCorrecto");
                //                lblDecCustomer.html("");
                //                if (MyObj.TipeMsgJs == "alert") {
                //                    alert(MyObj.ErrorMsg);
                //                }
                //                else if (MyObj.TipeMsgJs == "lbl") {
                ////                    $('#lblMsg').html(MyObj.ErrorMsg);
                //                }
                if (MyObj.TipeMsgJs == "alert") {
                    alert(MyObj.ErrorMsg);
                }
                DetallePallet.hide();
            }
            else {

                txPalletID.addClass("InputCorrecto");
                txPalletID.removeClass("InputIncorrecto");
                //                lblDecCustomer.html(MyObj.NAMA);
                //                $('#lblMsg').html("");


                lblItemID.html(MyObj.ITEM);
                lblItemDesc.html(MyObj.DSCA);
                LblQuantityVal.html(MyObj.QTYT);
                LblQuantityUnit.html(MyObj.UNIT);
                lblWorkOrder.html(MyObj.ORNO);
                lblMachine.html(MyObj.MCNO);
                DetallePallet.show();
            }
        }

        function ClickDropTagPickSuccess(r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                if (MyObj.TipeMsgJs == "alert") {
                    alert(MyObj.ErrorMsg);
                }
            }
            else {
                txPalletID.removeClass("InputIncorrecto");
                txPalletID.removeClass("InputCorrecto");
                DetallePallet.hide();
                txPalletID.val("");
                lblItemID.html("");
                lblItemDesc.html("");
                LblQuantityVal.html("");
                LblQuantityUnit.html("");

                if (MyObj.TipeMsgJs == "alert") {
                    alert(MyObj.SuccessMsg);
                }

                printDiv('LblEtiqueta');

            }
        }
    </script>
</asp:Content>
