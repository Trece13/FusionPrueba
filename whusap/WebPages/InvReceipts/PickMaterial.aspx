<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="PickMaterial.aspx.cs" Inherits="whusap.WebPages.InvReceipts.PickMaterial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
<style type="text/css">
    #MyEtiqueta label
    {
        font-size : 15px;
    }
    
    #LblDate
    {
        font-size:14px !important;
    }
    
    #LblReprintInd,#LblReprint
    {
        display:none;
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
        <input id="btnEnviar" type="button" class="btn btn-primary btn-lg" value="Pick" />&nbsp
    </div>
    </form>
    <div id="MyEtiqueta">
        <table>
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
                        QUANTITY</label>
                </td>
                <td>
                    <label id="LblQuantity">
                    </label>
                </td>
                <td>
                    <label id="LblUnit">
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        LOT</label>
                </td>
                <td>
                    <label id="LblLotId">
                    </label>
                </td>
            </tr>
            <t>
                <td>
                    <label>
                        RECEIPT DATE</label>
                </td>
                <td> &nbsp&nbsp
                    <label id="LblDate">
                    </label>
                    &nbsp&nbsp
                </td>
                <td>
                    <label id = "LblReprintInd">
                        REPRINT:</label>
                </td>
                <td>
                    <label id="LblReprint">
                    </label>
                </td>
            </tr>
        </table>
    </div>
    <style type="text/css">
        #MyEtiqueta
        {
            display: none;
        }
    </style>
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
                url: "PickMaterial.aspx/" + WebMethod,
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


            lblItemID = $('#lblItemID');
            lblItemDesc = $('#lblItemDesc');

            LblQuantity = $('#LblQuantity');
            LblUnit = $('#LblUnit');

            LblLotId = $('#LblLotId');
            LblDate = $('#LblDate');
            LblReprint = $('#LblReprint');
            LblReprintInd    = $('#LblReprintInd');
        }

        function formatDate(date) {
            var monthNames = [
                "1", "2", "3",
                "4", "5", "6", "7",
                "8", "9", "10",
                "11", "12"
              ];

                var day = date.getDate();
                var monthIndex = date.getMonth();
                var year = date.getFullYear();

                var hours = date.getHours();
                var minutes = date.getMinutes();
                var seconds = date.getSeconds();

                return ' ' + monthNames[monthIndex] + '/' +day +'/' + year+' '+hours+':'+minutes+':'+seconds+' ';
            }

            var FuncitionSuccesReprint = function (r) {
                console.log(r.d);
                MyEtiqueta.hide('slow');

                var MyObject = JSON.parse(r.d);
                if (MyObject.MyError == undefined) {
                    

                    //Etiqueta NO OC

                    lblItemID.html(MyObject.ITEM);
                    lblItemDesc.html(MyObject.DSCA);

                    LblQuantity.html(MyObject.QTYC);
                    LblUnit.html(MyObject.UNIC);

                    LblLotId.html(MyObject.CLOT);
                    LblDate.html(" " + formatDate(new Date()) + " ");
                    LblReprint.html(MyObject.NPRT);


                    if (parseInt(MyObject.NPRT, 10) > 1) {
                        LblReprintInd.show();
                        LblReprint.show();
                    }

                    MyEtiqueta.show('slow');
                    alert("Pickup Process Succesfully");

                }
                else if (MyObject.MyError != undefined) {
                    MyEtiqueta.hide('slow');
                    alert(MyObject.MyError);
                }

            }



        $(function () {

            IdentificarControles();

            btnEnviar.click(function () {
                var Data = "{'PAID':'" + txPalletID.val().toUpperCase() + "'}";
                sendAjax("Clic_Pick", Data, FuncitionSuccesReprint)
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
