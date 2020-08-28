<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="whInvMaterialRejectedWarehouse.aspx.cs" Inherits="whusap.WebPages.Migration.whInvMaterialRejectedWarehouse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <style type="text/css">
        .RightValidation
        {
            border-color: green;
        }
        .WrongValidation
        {
            border-color: red;
        }
        .LabelsError
        {
            color: Red;
            font-size: 15px;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">

        function ListaItems() {

            MyListaItems = JSON.parse('<%= LstItemsJSON%>');
            localStorage.setItem("MyListaItems", MyListaItems);
            console.log(MyListaItems);
            //return LsItems;
        }


        ListaItems();


        var _idioma = '<%= _idioma %>';
         

        function setReason(field) {
            var selectedText = $("#slReasons option:selected").text();

            $('#lblReasonDesc').val(selectedText);
        }

        function validarCantidad(field, stk, CantidadDevuelta) {
            var cantidad = parseInt(field.value);
            var stock = stk;
            var regex = /^-?\d*[0-9]*[.]?[0-9]*$/;
            var re = new RegExp(regex);
            if (field.value.match(re)) {
                if (((cantidad + CantidadDevuelta) > stock)) {
                    alert(_idioma == "INGLES" ? 'Quantity is greater that stock by item, warehouse and lot, only have ' + (stock - (CantidadDevuelta + cantidad)) : 'Cantidad mayor a la cantidad existente por item, almacen y lote, solo hay ' + (stock - (CantidadDevuelta + cantidad)));
                    this.focus();
                    field.value = 0;
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

            var slReason = $('#slReasons').val();
            var txtQuantity = $('#txtQuantity').val();
            var txtComments = $('#txtComments').val();

            if (slReason.trim() == "") {
                validate = false;
                mensaje += _idioma == "INGLES" ? "-Reason\n" : "-Razon\n";
            }

            if (txtQuantity.trim() == "") {
                validate = false;
                mensaje += _idioma == "INGLES" ? "-Quantity\n" : "-Cantidad\n";
            }

            if (txtComments.trim() == "") {
                validate = false;
                mensaje += _idioma == "INGLES" ? "-Comments\n" : "-Comentarios\n";
            }

            debugger;
            if (!validate) {
                alert((_idioma == "INGLES" ? "Please, enter this fields:\n" : "Por favor ingrese los siguientes campos:\n") + mensaje);
            }

            return validate;
        };

        function printDiv(divID) {
            //PRINT LOCAL HOUR

            var monthNames = [
                "1", "2", "3",
                "4", "5", "6", "7",
                "8", "9", "10",
                "11", "12"
              ];

            //PRINT LOCAL HOUR
            var d = new Date();
            var LbdDate = $("#lblDate");
            LbdDate.html(
                monthNames[d.getMonth()] +
                "/" +
                d.getDate() +
                "/" +
                d.getFullYear() +
                " "+
                d.getHours() +
                ":" +
                d.getMinutes() +
                ":" +
                d.getSeconds()
                );

//            var x = document.getElementById("lblDate");
//            var h = addZero(d.getHours());
//            var m = addZero(d.getMinutes());
//            var s = addZero(d.getSeconds());
//            //x.innerHTML = d.toUTCString();
//            x.innerHTML = d.toLocaleString();

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
        }

        function addZero(i) {
            if (i < 10) {
                i = "0" + i;
            }
            return i;
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table border="0">
        <tr>
            <td style="text-align: left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                    <b style="font-size: 11px;">
                        <asp:Label runat="server" ID="lblItem" /></b></span>
            </td>
            <td style="width: 250px; padding: 5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtItem" CssClass="TextBoxBig InputForm" ClientIDMode="Static"
                        OnTextChanged="txtItem_OnTextChanged"/>
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                    <b style="font-size: 11px;">
                        <asp:Label runat="server" ID="lblLot" tabindex="2"/></b></span>
            </td>
            <td style="width: 250px; padding: 5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtLot" CssClass="TextBoxBig InputForm" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                    <b style="font-size: 11px;">
                        <asp:Label runat="server" ID="lblWarehouse" /></b></span>
            </td>
            <td style="width: 250px; padding: 5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtWarehouse" CssClass="TextBoxBig InputForm" ClientIDMode="Static"/>
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align: left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                    <b style="font-size: 11px;">
                        <asp:Label runat="server" ID="lblLocation" /></b></span>
            </td>
            <td style="width: 250px; padding: 5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtLocation" CssClass="TextBoxBig InputForm" ClientIDMode="Static"
                        OnTextChanged="txtLocation_OnTextChanged" />
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <hr />
                <asp:Button Text="" runat="server" ID="btnConsultar" OnClick="btnConsultar_Click"
                    CssClass="ButtonsSendSave" Style="height: 30px;"/>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <hr />
                <label id="lblitemError" class="LabelsError">
                </label>
                </br>
                <label id="lblWarehouseError" class="LabelsError">
                </label>
                </br>
                <label id="lblLocationError" class="LabelsError">
                </label>
                </br>
                <label id="lblLotError" class="LabelsError">
                </label>
                </br>
            </td>
        </tr>
    </table>
    <div runat="server" id="divBtnGuardar" visible="false">
        <hr />
        <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" OnClientClick="return validarFormulario();"
            CssClass="ButtonsSendSave" />
            <input type = "text" id= "txSloc" runat = "server" style = " display:none"/>
    </div>
    <div runat="server" id="divTable">
    </div>
    <asp:Label Text="" runat="server" ID="lblError" Style="color: red; font-size: 15px;
        font-weight: bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" Style="color: green; font-size: 15px;
        font-weight: bold;" ClientIDMode="Static" />
    <table runat="server" id="divBotones" visible="false" style="margin-bottom: 10px;
        text-align: center; font-weight: bold;" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <button class="btn btn-primary btn-lg" type="button" onclick="javascript:printDiv('divLabel')">
                    Print</button>
            </td>
            <td>
                <asp:Button class="btn btn-primary btn-lg" runat="server" ID="btnSalir" OnClick="btnExit_Click"
                    AutoPostBack="true" />
            </td>
        </tr>
    </table>
    <div runat="server" id="divLabel" visible="false" clientidmode="Static" style="zoom: 80%;
        margin-bottom: 70px">
        <hr />
        <table style="width: 5.8in; height: 3.8in; text-align: center; font-weight: bold;"
            border="1" cellspacing="0" cellpadding="0">
            <tr>
                <td colspan="3">
                    <asp:Label ID="lblDefectiveMaterial" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="imgCodeItem" alt=""
                        hspace="60" vspace="5" style="width: 3in; height: .5in;" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblDescDescription"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:Label runat="server" ID="lblValueDescription"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Dispositon - Review
                </td>
                <td>
                    <asp:Label ID="lblDescRejectQty" runat="server" />
                </td>
                <td>
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="imgQty" alt="" hspace="60"
                        vspace="5" style="width: 1in; height: .3in;" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblDescPrintedBy"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:Label runat="server" ID="lblDate" ClientIDMode="static"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblValueLot"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lblDescReason"></asp:Label>
                </td>
                <td>
                    <asp:Label runat="server" ID="lblValueReason"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Label runat="server" ID="lblComments"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <input type="text" id="lblReasonDesc" name="lblReasonDesc" style='display: none;' />
    <asp:HiddenField runat="server" ID="hdfDescItem" />
    <script type="text/javascript">



        function IniciarControles() {

            txSloc = $('#Contenido_txSloc');
                
            txtitemError = '<%= lblitemError%>';
            txtWarehouseError = '<%= lblWarehouseError%>';
            txtLocationError = '<%= lblLocationError%>';
            txtLotError = '<%= lblLotError%>';




            lblitemError = $('#lblitemError');
            lblWarehouseError = $('#lblWarehouseError');
            lblLocationError = $('#lblLocationError');
            lblLotError = $('#lblLotError');

            btnConsultar = $('#Contenido_btnConsultar');
            txtItem = $('#txtItem');
            txtWarehouse = $('#txtWarehouse');
            txtLocation = $('#txtLocation');
            txtLot = $('#txtLot');


            txtLocation.attr('disabled', true);
            txtLocation.removeClass('InputForm');

            txtLot.attr('disabled', true);
            txtLot.removeClass('InputForm');

        }

        $(function () {

            function ListaWarehouses() {

                var options = {
                    type: "POST",
                    url: "whInvMaterialRejectedWarehouse.aspx/ListaWarehouses",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        MyListaWarehouses = JSON.parse(msg.d);
                        console.log(MyListaWarehouses);
                    }
                };
                $.ajax(options);
            }

            ListaWarehouses();

            function listaLotesPorItem(Item) {

                var options = {
                    type: "POST",
                    url: "whInvMaterialRejectedWarehouse.aspx/listaLotesPorItem",
                    data: "{'Item':'" + Item + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        MylistaLotesPorItem = JSON.parse(msg.d);
                        console.log(MylistaLotesPorItem);
                    }
                };
                $.ajax(options);
            }

            function ListaLocalizacionesPorWarehouses(Cwar) {

                var options = {
                    type: "POST",
                    url: "whInvMaterialRejectedWarehouse.aspx/LocalizacionesPorWarehouses",
                    data: "{'cwar':'" + Cwar + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        MyListaLocalizacionesPorWarehouses = JSON.parse(msg.d);
                        console.log(MyListaLocalizacionesPorWarehouses);
                    }
                };
                $.ajax(options);
            }


            IniciarControles();

            btnConsultar.attr("disabled", true);

            function ValidarInputsActivacionBoton() {
                var Inputs = $('.InputForm');

                if (Inputs.hasClass("WrongValidation") == true) {
                    btnConsultar.attr("disabled", true);
                    return;
                } else {
                    btnConsultar.attr("disabled", false);
                }

            }

            function ValidacionInput(status, MyInput) {
                status ? ValidacionCampoCorrecta(MyInput) : ValidacionCampoIorrecta(MyInput);
            }

            function ValidacionCampoCorrecta(MyInput) {
                $(MyInput).removeClass("WrongValidation");
                $(MyInput).addClass("RightValidation");
                ValidarInputsActivacionBoton();
            };

            function ValidacionCampoIorrecta(MyInput) {
                $(MyInput).removeClass("RightValidation");
                $(MyInput).addClass("WrongValidation");
                ValidarInputsActivacionBoton();
            };

            txtItem.bind("paste keyup change", function () {

                var MyItem = ValidaExistenciaItem(txtItem.val().toUpperCase().trim());

                if (MyItem != undefined) {

                    lblitemError.html("");

                    if (MyItem.Ktlc.trim() == "1") {
                        listaLotesPorItem(MyItem.Item.trim());
                        txtLot.addClass('InputForm');
                        txtLot.attr('disabled', false);
                        txtLot.addClass('WrongValidation');

                        ValidacionInput(true, $(txtItem));
                        lblLotError.html(txtLotError);  

                    }
                    else if (MyItem.Ktlc.trim() == "3") {
                        //listaLotesPorItem(MyItem.Item.trim());
                        txtLot.removeClass('InputForm');
                        txtLot.removeClass('WrongValidation');
                        txtLot.attr('disabled', true);
                        txtWarehouse.addClass('WrongValidation');

                        ValidacionInput(true, $(txtItem));
                        txtLot.val("")
                        lblLotError.html("");
                    }
                } else {

                    txtLot.val("");
                    txtLot.removeClass('InputForm');
                    txtLot.attr('disabled', true);
                    txtLot.removeClass('RightValidation');
                    txtLot.removeClass('WrongValidation');
                    ValidacionInput(false, $(txtItem));
                    lblitemError.html(txtitemError);
                    lblLotError.html("");

                }
            });

            txtWarehouse.bind("paste keyup change", function () {
                var MyWarehouse = ValidaExistenciaWarehouse(txtWarehouse.val().toUpperCase().trim());

                if (MyWarehouse != undefined) {
                    lblWarehouseError.html("");
                    txSloc.val(MyWarehouse.SLOC);
                    ValidacionInput(true, $(txtWarehouse));
                    if (MyWarehouse.SLOC == 1) {
                        ListaLocalizacionesPorWarehouses(MyWarehouse.CWAR);
                        txtLocation.addClass('InputForm');
                        txtLocation.attr('disabled', false);
                        txtLocation.addClass('WrongValidation');
                        lblWarehouseError.html("");
                        $('#lblError').html("");
                        lblLocationError.html(txtLocationError);
                    }
                    else {
                        txtLocation.attr('disabled', true);
                        txtLocation.removeClass('InputForm');
                        txtLocation.removeClass('RightValidation');
                        txtLocation.removeClass('WrongValidation');
                        lblLocationError.html("");

                    }

                } else {
                    ValidacionInput(false, $(txtWarehouse));
                    txtLocation.val("");
                    txtLocation.removeClass('InputForm');
                    txtLocation.attr('disabled', true);
                    txtLocation.removeClass('RightValidation');
                    txtLocation.removeClass('WrongValidation');
                    lblLocationError.html("");
                    lblWarehouseError.html(txtWarehouseError);
                }
            });

            txtLocation.bind("paste keyup change", function () {
                var MyLoc = ValidaExistenciaLocalizacion(txtLocation.val().toUpperCase().trim());
                if (MyLoc != undefined) {
                    ValidacionInput(true, $(txtLocation));
                    lblLocationError.html("");
                } else {
                    ValidacionInput(false, $(txtLocation));
                    lblLocationError.html(txtLocationError);
                }
            });

            txtLot.bind("paste keyup change", function () {

                var MyLot = ValidaExistenciaLote(txtLot.val().trim().toUpperCase().trim());
                if (MyLot != undefined) {
                    ValidacionInput(true, $(txtLot));
                    lblLotError.html("");
                } else {
                    ValidacionInput(false, $(txtLot));
                    lblLotError.html(txtLotError);
                }
            });


            function ValidaExistenciaItem(Item) {
                var register = undefined;
                MyListaItems.forEach(function (row) {
                    if (row.Item == Item) {
                        register = row;
                    }
                });
                //register = MyListaItems.find(row => row.Item == Item);
                return register;
            }

            function ValidaExistenciaWarehouse(Cwar) {
                var register = undefined;
                MyListaWarehouses.forEach(function (row) {

                    if (row.CWAR == Cwar) {
                        register = row;
                    }
                });

                //register = MyListaWarehouses.find(row => row.CWAR == Cwar);
                return register;
            }

            function ValidaExistenciaLocalizacion(Loc) {
                var register = undefined;
                MyListaLocalizacionesPorWarehouses.forEach(function (row) {

                    if (row == Loc) {
                        register = row;
                    }
                });
                //register = MyListaLocalizacionesPorWarehouses.find(row => row == Loc);
                return register;
            }

            function ValidaExistenciaLote(Lot) {
                var register = undefined;
                MylistaLotesPorItem.forEach(function (row) {

                    if (row == Lot) {
                        register = row;
                    }
                });
                //register = MylistaLotesPorItem.find(row => row == Lot);
                return register;
            }
        });
    </script>
</asp:Content>
