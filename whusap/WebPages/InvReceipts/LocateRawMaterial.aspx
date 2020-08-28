<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="LocateRawMaterial.aspx.cs" Inherits="whusap.WebPages.InvReceipts.LocateRawMaterial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <style type="text/css">
        #MyEtiqueta
        {
            font-size: 14px;
            display:none
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
        <input id="btnQuery" type="button" class="btn btn-primary btn-lg" value="Query" />
    </div>
    </form>
    <div id="MyEtiqueta">
        <table>
            <tr>
                <td>
                    <label>
                        Pallet ID</label>
                </td>
                <td>
                    <label id="lblPalletID">
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Warehouse</label>
                </td>
                <td>
                    <label id="lblWarehouse">
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        ITEM</label>
                </td>
                <td>
                    <label id="lblItemID">
                    </label>
                </td>
                <td>
                    <label id="lblItemDesc">
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Lot</label>
                </td>
                <td>
                    <label id="LblLotId">
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Location</label>
                </td>
                <td>
                    <input type="text" id="txLoca" />
                </td>
            </tr>
        </table>
        <div class="form-group row">
            <input id="btnLocalizar" type="button" class="btn btn-primary btn-lg" value="Locate" />
        </div>
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
                url: "LocateRawMaterial.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

        }

        function IdentificarControles() {


            //Botones
            btnQuery = $('#btnQuery');
            btnLocalizar = $('#btnLocalizar');
            //Etiqueta

            MyEtiqueta   = $('#MyEtiqueta');

            //Formulario

            txPalletID = $('#txPalletID');
            lblPalletID = $('#lblPalletID');
            lblWarehouse = $('#lblWarehouse');
            lblItemID = $('#lblItemID');
            LblLotId = $('#LblLotId');
            txLoca = $('#txLoca');


        }

        var FuncitionSuccesLocate = function (r) {
            //console.log(r.d);
            var MyObject = JSON.parse(r.d);
            if (MyObject.error == false) {
                alert("Successful process");
                $('#MyEtiqueta').hide();
            }
            else {

                alert(MyObject.errorMsg);
            }

        }

        var FuncitionSuccesQuery = function (r) {
            var MyObject = JSON.parse(r.d);

            if (MyObject.error == false) {
                MyEtiqueta.show('slow');
                $('#txLoca').val("");
                lblPalletID.html(MyObject.PAID);
                lblWarehouse.html(MyObject.CWAR);
                lblItemID.html(MyObject.ITEM);
                LblLotId.html(MyObject.CLOT);

                if (MyObject.SLOC == "1") {
                    $('#txLoca').prop('disabled', false);
                }
                else {
                    $('#txLoca').prop('disabled', true);
                }
            }
            else {
                alert(MyObject.errorMsg);
                $('#MyEtiqueta').hide();
            }

        }


        $(function () {

            IdentificarControles();

            btnQuery.click(function () {

                var Data = "{'PAID':'" + txPalletID.val().toUpperCase() + "'}";
                sendAjax("Click_Query", Data, FuncitionSuccesQuery);

            });

            btnLocalizar.click(function () {
                var Data = "{'PAID':'" + txPalletID.val().toUpperCase() + "',  'CWAR':'" + lblWarehouse.html() + "',  'LOCA':'" + txLoca.val().toUpperCase() + "'}"
                sendAjax("Click_Locate", Data, FuncitionSuccesLocate)
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
