<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="DropPickedMaterialMFG.aspx.cs" Inherits="whusap.WebPages.WorkOrders.DropPickedMaterialMFG" %>

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
            display:none;
        }
        #lblMsg
        {
            display:none;
            font-size:14px;
            color:Red;
        }
        #txPalletID
        {
            width:300px;
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
            <div class="form-group row ">
                <label class="col-sm-2 col-form-label-lg"  id="Label1">
                    Work Order
                </label>
                <div class="col-sm-3">
                    <label class="col-sm-12 col-form-label-lg" id="lblWorkOrder">
                    </label>
                </div>
            </div>
            <div class="form-group row ">
                <label class="col-sm-2 col-form-label-lg" for="txCustomer" id="Label4">
                    Machine
                </label>
                <div class="col-sm-3">
                    <label class="col-sm-12 col-form-label-lg" id="lblMachine">
                    </label>
                </div>
            </div>
            <div class="form-group row" id="divQueryAction">
                <div class="col-sm-2">
                </div>
                <div class="col-sm-4">
                    <input type="button" class="btn btn-primary btn-lg col-sm-7 " id="btnDropTagPick"
                        value="Picked by MFG" onclick="ClickDropTagPick()" />
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
                url: "DropPickedMaterialMFG.aspx/" + WebMethod,
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

            lblWorkOrder = $('#lblWorkOrder');
            lblMachine = $('#lblMachine');
            lblMsg = $('#lblMsg');
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
            lblMsg.hide(500);
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
                DetallePallet.hide(); if (MyObj.Error == true) {
                    if (MyObj.TipeMsgJs == "alert") {
                        alert(MyObj.ErrorMsg);
                    } if (MyObj.TipeMsgJs == "lbl") {
                        lblMsg.html(MyObj.ErrorMsg);
                        lblMsg.show(500);
                    }
                }
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
            lblMsg.hide(500);
            var MyObj = JSON.parse(r.d);
            if (MyObj.Error == true) {
                if (MyObj.TipeMsgJs == "alert") {
                    alert(MyObj.ErrorMsg);
                }
            }
            else {
                txPalletID.removeClass("InputIncorrecto");
                txPalletID.removeClass("InputCorrecto");
                txPalletID.val("");
                lblItemID.html("");
                lblItemDesc.html("");
                LblQuantityVal.html("");
                LblQuantityUnit.html("");

                if (MyObj.TipeMsgJs == "alert") {
                    alert(MyObj.SuccessMsg);
                }
                DetallePallet.hide();
            }
        }
    </script>
</asp:Content>
