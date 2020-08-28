<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="WhInvDeletePallets.aspx.cs" Inherits="whusap.WebPages.WorkOrders.WhInvDeletePallets" %>

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
    
    #LblReprintInd,#LblReprint,#btnEnviar,#MyEtiqueta
    {
        display:none;
    }
    
    
</style>
    <form id="form1" class="container">
    <%--<div class="form-group row">
        <input id="state" type="button" class="btn btn-danger btn-lg" value="tticol022"/>&nbsp
    </div>--%>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg" for="txQuantity" id="mainlabel">
            Pallet ID</label>
        <div class="col-sm-4">
            <input type="text" class="form-control form-control-lg" id="txPalletID" placeholder="Pallet ID"
                data-method="ValidarQuantity">
        </div>
        <label id="lblUnis" for="txQuantity">
        </label>
    </div>
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
                    &nbsp&nbsp&nbsp&nbsp&nbsp
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
            </tr id="rowAnnounced">
                <tr>
                <td>
                    <label>
                    
                        Announced on&nbsp</label>
                </td>
                <td>
                    <label id="lblDateAnnounced">
                    </label>
                </td>
            </tr>

        </table>
    </div>
    <div class="form-group row">
        <input id="btnEnviar" type="button" class="btn btn-primary btn-lg" value="Delete" />&nbsp
        <input id="btnQuery" type="button" class="btn btn-primary btn-lg" value="Query" />&nbsp
    </div>
    </form>
    <style type="text/css">
        #MyEtiqueta
        {
<%--            display: none;--%>
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

        var FuncitionSuccesConsultaPalletID = function (res) {
            MyObj = JSON.parse(res.d);

            if (MyObj.Error) {
                if (MyObj.TypeMsgJs == "alert") {
                    alert(MyObj.ErrorMsg);
                    btnEnviar.hide();
                    btnQuery.show();
                    MyEtiqueta.hide();
                }
                else {
                }
            }
            else if (!MyObj.Error) {

                lblItemID.html(MyObj.mitm);
                lblItemDesc.html(MyObj.dsca);
                LblUnit.html(MyObj.cuni);
                LblQuantity.html(MyObj.qtdl);
                $('#lblDateAnnounced').html(MyObj.date);
                btnEnviar.show();
                btnQuery.hide();
                MyEtiqueta.show();
                
            }
        }

        function sendAjax(WebMethod, Data, FuncitionSucces) {
            var options = {
                type: "POST",
                url: "WhInvDeletePallets.aspx/" + WebMethod,
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
            btnQuery = $('#btnQuery');

            lblItemID = $('#lblItemID');
            lblItemDesc = $('#lblItemDesc');

            LblQuantity = $('#LblQuantity');
            LblUnit = $('#LblUnit');

            LblLotId = $('#LblLotId');
            LblDate = $('#LblDate');
            LblReprint = $('#LblReprint');
            LblReprintInd = $('#LblReprintInd');
            lblDateAnnounced = $('#lblDateAnnounced');
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
                    btnQuery.show();
                    btnEnviar.hide();
                    txPalletID.val("");
                }

            }



            $(function () {

                IdentificarControles();

                btnEnviar.click(function () {
                    var Data = "{'PAID':'" + txPalletID.val().toUpperCase() + "'}";
                    sendAjax("Click_Pick", Data, FuncitionSuccesReprint)
                });

                btnQuery.click(function () {
                    var Data = "{'PAID':'" + txPalletID.val().toUpperCase() + "'}";
                    sendAjax("Click_Query", Data, FuncitionSuccesConsultaPalletID)
                });

                if ('<%=active022%>' == 'False') {
                    $('#state').val("tticol042");
                    $('#state').removeClass("btn-danger");
                    $('#state').addClass("btn-success");
                    var Data = "{'state':'false'}";
                    sendAjax("changeState", Data);
                    $('#btnEnviar').val("Save");
                    $('#mainlabel').html('Regrind Pallet ID');
                    $('#rowAnnounced').show();
                    
                }
                else {
                    $('#state').val("tticol022");
                    $('#state').removeClass("btn-success");
                    $('#state').addClass("btn-danger");
                    var Data = "{'state':'true'}";
                    sendAjax("changeState", Data);
                    $('#btnEnviar').val("Delete");
                    $('#mainlabel').html('Pallet ID');
                    $('#rowAnnounced').hide();

                }
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
