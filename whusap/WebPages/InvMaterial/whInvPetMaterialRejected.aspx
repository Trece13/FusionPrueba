<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="whInvPetMaterialRejected.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvPetMaterialRejected" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <style>
        #MyEtiquetaOC, #MyEtiqueta
        {
            display: none;
        }
        
        #LblUnitOC
        {
            font-size: 14px;
        }
        
        #lblError
        {
            font-size: 14px;
            color: Red;
        }
        
        label
        {
            font-size: 14px;
        }
        
        #divDetalle
        {
            display:none;
        }
    </style>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txWorkorder">
            Work Order</label>
        <div class="col-sm-4">
            <input type="text" class="form-control form-control-lg" id="txWorkorder" placeholder="Work order">
        </div>
    </div>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txWorkorder">
            Pallet ID</label>
        <div class="col-sm-4">
            <input type="text" class="form-control form-control-lg" id="txPalletId" placeholder="Pallet ID">
        </div>
    </div>
    <div id="divDetalle">
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
            <label class="col-sm-2 col-form-label-lg" for="txItem">
                Warehouse</label>
            <div class="col-sm-4">
                <input type="text" class="form-control form-control-lg" id="txWarehouse" placeholder="Warehouse">
            </div>
            <label id="Label1" for="txWarehouse">
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
                Quantity</label>
            <div class="col-sm-4">
                <input type="numeric" class="form-control form-control-lg" id="txQuantity" placeholder="Quantity">
            </div>
            <label id="lblQuantity" for="txQuantity">
            </label>
        </div>
        <div class="form-group row">
            <input id="btnSave" type="button" class="btn btn-primary btn-lg" value="Register" />
        </div>
    </div>
    <div class="form-group row">
        <label id="lblError">
        </label>
    </div>
    <script>
        var timer;

        function stoper() {

            clearTimeout(timer);
        }


        function IniciarComponentes() {

            txItem = $('#txItem');
            txPalletId = $('#txPalletId');
            txLot = $('#txLot');
            txQuantity = $('#txQuantity');
            txWorkorder = $('#txWorkorder');
            lblItem = $('#lblItem');
            lblQuantity = $('#lblQuantity');

            btnSave = $('#btnSave');

        };

        IniciarComponentes();


        function BloquearComponentes() {
            $('#txPalletId').prop("disabled", true);
            $('#txItem').prop("disabled", true);
            $('#txLot').prop("disabled", true);
            $('#txQuantity').prop("disabled", true);
            $('#btnSave').prop("disabled", true);
            $('#txWarehouse').prop("disabled", true);

        };
        function BloquearComponentesItem() {

            $('#txLot').prop("disabled", true);
            $('#txQuantity').prop("disabled", true);
            $('#btnSave').prop("disabled", true);

        };
        BloquearComponentes();


        var SuccesVerificarWorkorder = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                $('#divDetalle').hide('slow');
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);

                $('#txPalletId').val("");
                $('#txItem').val("");
                $('#txLot').val("");
                $('#txQuantity').val("");


                $('#txPalletId').prop("disabled", true);
                $('#txItem').prop("disabled", true);
                $('#txLot').prop("disabled", true);
                $('#txWarehouse').prop("disabled", true);
                $('#txQuantity').prop("disabled", true);
                $('#btnSave').prop("disabled", true);
            }
            if (MyObj.Error == false) {
                $('#txPalletId').val("");
                $('#txItem').val("");
                $('#txLot').val("");
                $('#txQuantity').val("");
                $('#txWarehouse').val("");


                $('#txPalletId').prop("disabled", false);
                $('#txItem').prop("disabled", true);
                $('#txLot').prop("disabled", true);
                $('#txWarehouse').prop("disabled", true);
                $('#txQuantity').prop("disabled", true);
                $('#btnSave').prop("disabled", true);
                $('#lblError').html("");
            }
        }

        var SuccesVerificarPalletId = function (r) {
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                $('#divDetalle').hide('slow');
                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);

                $('#txItem').val("");
                $('#txLot').val("");
                $('#txQuantity').val("");
                $('#txWarehouse').val("");
                $('#lblItem').html("");
                $('#lblQuantity').html("");

                $('#txItem').prop("disabled", true);
                $('#txLot').prop("disabled", true);
                $('#txQuantity').prop("disabled", true);
                $('#btnSave').prop("disabled", true);
            }
            if (MyObj.Error == false) {
                
                $('#txItem').val(MyObj.item);
                $('#txLot').val(MyObj.clot);
                $('#txWarehouse').val(MyObj.cwar);
                $('#txQuantity').val("");
                $('#lblItem').html(MyObj.dsca);
                $('#lblQuantity').html(MyObj.cuni);

                $('#txItem').prop("disabled", true);
                $('#txLot').prop("disabled", true);
                $('#txQuantity').prop("disabled", false);
                $('#btnSave').prop("disabled", true);

                $('#lblError').html("");
                $('#divDetalle').show('slow');
            }
        }

        var SuccesVerificarItem = function (r) {
            BloquearComponentesItem();
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                $('#txLot').val("");
                $('#txQuantity').val("");
                $('#lblItem').html("");
                $('#lblQuantity').html("");

                ImprimirMensaje(MyObj.TypeMsgJs, MyObj.ErrorMsg);
                BloquearComponentesItem();
            }
            if (MyObj.Error == false) {
                $('#lblError').html("");

                $('#txLot').val("");
                $('#txQuantity').val("");
                $('lblItem').html("");
                $('lblQuantity').html("");

                lblItem.html(MyObj.dsca);
                lblQuantity.html(MyObj.cuni);
                if (MyObj.kltc == "1") {
                    $('#txLot').prop("disabled", false);

                } else {
                    $('#txLot').prop("disabled", true);
                    $('#txQuantity').prop("disabled", false);
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
                $('#divDetalle').hide('slow');
                alert(MyObject.SuccessMsg);
                BloquearComponentes();

                $('#txWorkorder').val("");
                $('#txPalletId').val("");
                $('#txWarehouse').val("");
                $('#txItem').val("");
                $('#txLot').val("");
                $('#txQuantity').val("");

            } else {
                $('#divDetalle').hide('slow');
                alert(MyObject.ErrorMsg);
                BloquearComponentes();

                $('#txWorkorder').val("");
                $('#txPalletId').val("");
                $('#txWarehouse').val("");
                $('#txItem').val("");
                $('#txLot').val("");
                $('#txQuantity').val("");

            }

        }

        var VerificarWorkorder = function () {
            $('#btnSave').prop("disabled", true);
            var Data = "{'PDNO':'" + $('#txWorkorder').val() + "'}";
            sendAjax("VerificarWorkorder", Data, SuccesVerificarWorkorder)
        }

        var VerificarPalletId = function () {
            $('#btnSave').prop("disabled", true);
            var Data = "{'PAID':'" + $('#txPalletId').val() + "'}";
            sendAjax("VerificarPalletId", Data, SuccesVerificarPalletId)
        }

        var VerificarItem = function () {
            $('#btnSave').prop("disabled", true);
            var Data = "{'ITEM':'" + $('#txItem').val() + "','PDNO':'" + $('#txWorkorder').val() + "'}";
            sendAjax("VerificarItem", Data, SuccesVerificarItem)
        }

        //            var VerificarLote = function () {
        //                $('#btnSave').prop("disabled", true);
        //                var Data = "{'ITEM':'" + $('#txItem').val() + "','CLOT':'" + $('#txLot').val() + "'}";
        //                sendAjax("VerificarLote", Data, SuccesVerificarLote)
        //            }

        var VerificarQuantity = function () {
            $('#btnSave').prop("disabled", true);

            if ($('#txQuantity').val().trim() != "") {
                MyCant = parseFloat($('#txQuantity').val().trim());
                if (MyCant != 0) {
                    $('#btnSave').prop("disabled", false);
                }
                else {
                    $('#btnSave').prop("disabled", true);
                }
            }
            else {
                $('#btnSave').prop("disabled", true);
            }


            //                var Data = "{'ITEM':'" + $('#txItem').val() + "','CLOT':'" + $('#txLot').val() + "'}";
            //                sendAjax("VerificarQuantity", Data, SuccesVerificarQuantity)

        }

        var Click_Save = function () {

            var Data = "{'PDNO':'" + $('#txWorkorder').val().trim().toUpperCase() + "','ITEM':'" + $('#txItem').val().trim().toUpperCase() + "','CLOT':'" + $('#txLot').val().trim() + "','QTYS':'" + $('#txQuantity').val().trim() + "','UNIT':'" + $('#lblQuantity').html() + "'}";
            sendAjax("Click_Save", Data, SuccesClick_Save);

        }

        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "whInvPetMaterialRejected.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }

        txWorkorder.bind('paste keyup', function () {
            stoper();
            timer = setTimeout("VerificarWorkorder()", 1000);
        });

        txPalletId.bind('paste keyup', function () {
            stoper();
            timer = setTimeout("VerificarPalletId()", 1000);
        });

        txItem.bind('paste keyup', function () {
            stoper();
            timer = setTimeout("VerificarItem()", 1000);
        });

        //            txLot.bind('paste keyup', function () {
        //                stoper();
        //                timer = setTimeout("VerificarLote()", 1000);
        //            });

        txQuantity.bind('paste keyup', function () {
            VerificarQuantity();
            //                stoper();
            //                timer = setTimeout("VerificarQuantity()", 1000);
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
