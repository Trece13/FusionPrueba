<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ConsultaBlockedPalletEdit.aspx.cs" Inherits="whusap.WebPages.WorkOrders.ConsultaBlockedPalletEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <style>
        #DivTable
        {
            margin-bottom: 100px; background
        }
        .flotante
        {
            display: scroll;
            position: fixed;
            bottom: 50px;
            right: 10px;
            width: 100px;
        }
        
        #lblMsg
        {
            font-size: 20px;
            color: Red;
        }
        
        .sorting_1
        {
            display:none;
        }
        #ColSort
        {
            display:none;
            
        }
        .InputCorrecto
        {
            border-bottom: solid 5px green;
        }
        
        .InputIncorrecto
        {
            border-bottom: solid 5px red;
        }
        
         #MyTable_length
        {
            display:none;
        }
        
        
        .btn-danger
        {
            display:none;
        }
        .btn-success
        {
        }
    </style>
    <script>
        var timer;
        lstEnviar = [];
        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "ConsultaBlockedPalletEdit.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: asyncMode != undefined ? asyncMode : true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }

        function stoper() {
            clearTimeout(timer);
        }

        function myFunction() {
            stoper();
            timer = setTimeout("EnviarCustomer()", 500);
        }
    </script>
    <form id="form1" class="container">
    <br />
    <label id="lblMsg">
    </label>
    <br />
    <div id="DivTable">
        <div class="form-group row" id="MyRegisterCustomer">
        </div>
        <div class="form-group row" id="div2">
            <div class="col-sm-10">
            </div>
            <div class="col-sm-2">
                <input type="button" class="btn btn-primary btn-sm col-sm-12 flotante" id="btnSave"
                    value="Save" />
            </div>
        </div>
    </div>
    </form>
    <script>

        var tticol082 = [];
        function AgregrarPrioridad(idMyRow, BtnCancel, BtnSave) {

            $(BtnSave).hide();
            $(BtnCancel).show();
            Mylist.forEach(function (item) {
                    if (item.PAID.trim() == idMyRow.cells[0].innerHTML.trim()) {
                    if (tticol082.length > 0) {
                        if (verificarExistencia(tticol082, item.PAID.trim())) {

                        }
                        else {
                            item.PAID = idMyRow.cells[0].innerHTML.trim();
                            tticol082.push(item);
                        };
                    }
                    else {
                        item.PAID = idMyRow.cells[0].innerHTML.trim();
                        tticol082.push(item);
                    }
                }
            });
        }

        function DeshabilitarPrioridad(idMyRow, BtnSave, BtnCancel) {
            var i = 0;
           
            tticol082.forEach(function (item) {
                if (item.PAID.trim() == idMyRow.cells[0].innerHTML.trim()) {
                    indexEliminar = i;
                }
                i++;
            });
            tticol082.splice(indexEliminar, 1);
            $(BtnCancel).hide();
            $(BtnSave).show();
        }

        function verificarExistencia(Lista, Busqueda) {

            retorno = false;
            Lista.forEach(function (item82) {
                if (item82.PAID.trim() == Busqueda.trim()) {
                    retorno = true;
                }
            });
            return retorno;
        }
        function paginaSiguinete() {
            $('#MyTable_next').click()
        }
        function paginaAnterior() {
            $('#MyTable_previous').click()
        }

        function iniciarComponentes() {
            txCustomer = $('#txCustomer');
            txToDate = $('#txToDate');
            btnQuery = $('#btnQuery');
            btnSave = $('#btnSave');
            lblDecCustomer = $('#lblDecCustomer')
        }
        iniciarComponentes();

        btnQuery.click(function () {
            ClickQuery($('#DdPlant').val());
        });

        btnSave.click(function () {
            ClickSave();
        });

        var EnviarCustomer = function () {
            //var Data = "{'ORNO':'" + OrdenID + "','PONO':'" + Position + "','SEQNR':'" + SEQNR + "'}";
            var Data = "{'Customer':'" + txCustomer.val() + "'}";
            WebMethod = "KeyPressCustomer";
            sendAjax(WebMethod, Data, EnviarCustomerSuccess, false);
        }

        function EnviarCustomerSuccess(r) {
            var MyObj = JSON.parse(r.d);

            if (MyObj.Error == true) {
                txCustomer.addClass("InputIncorrecto");
                txCustomer.removeClass("InputCorrecto");
                lblDecCustomer.html("");
                if (MyObj.TipeMsgJs == "alert") {
                    alert(MyObj.ErrorMsg);
                }
                else if (MyObj.TipeMsgJs == "lbl") {
                    $('#lblMsg').html(MyObj.ErrorMsg);
                }
            }
            else {
                txCustomer.addClass("InputCorrecto");
                txCustomer.removeClass("InputIncorrecto");
                lblDecCustomer.html(MyObj.NAMA);
                $('#lblMsg').html("");
            }
        }

        var ClickQuery = function () {
            //var Data = "{'ORNO':'" + OrdenID + "','PONO':'" + Position + "','SEQNR':'" + SEQNR + "'}";
            var Data = "{}";
            WebMethod = "ClickQuery";
            sendAjax(WebMethod, Data, ClickQuerySuccess, false);
        }

        var SelectPlant = function () {
            //var Data = "{'ORNO':'" + OrdenID + "','PONO':'" + Position + "','SEQNR':'" + SEQNR + "'}";
            var Data = "{}";
            WebMethod = "SelectPlant";
            sendAjax(WebMethod, Data, SelectPlantSuccess, false);
        }

        function SelectPlantSuccess(r) {
            MylistPlant = JSON.parse(r.d);

            if (MylistPlant.length > 0) {

                MylistPlant.forEach(function (item) {
                    $('#DdPlant').append('<option value="' + item.TITE + '">' + item.TITE + " - " + item.DSCA + '</option>');
                });

            }
        };

        function ClickQuerySuccess(r) {

            var EncavezadoTable = "<table class='table display' id='MyTable' cellspacing='0' width='100%'>" +
                                    "<thead>" +
                                    "<tr>" +
                                    "<th scope='col'>Pallet ID</th>" +
                                    "<th scope='col'>Warehouse</th>" +
                                    "<th scope='col'>Location</th>" +
                                    "<th scope='col'>Item</th>" +
                                    "<th scope='col'>Description</th>" +
                                    "<th scope='col'>Quantity</th>" +
                                    "<th scope='col'>Unit</th>" +
                                    "</tr>" +
                                    "</thead>" +
                                    "<tbody>";
            var PieTable = "</tbody></table >" +
            "<script>$('#MyTable').dataTable();<" +
            "/script><style>#MyTable_wrapper{width: 100%} .paginate_button{font-size:14px; margin-left:5px;} #MyTable_length{font-size:12px; margin-rigth:5px;} #MyTable_filter{font-size:12px; margin-rigth:5px;} <" + "/style>";
            var CuerpoTable = "";

            $('.table').remove();
            $('#MyTable_wrapper').remove();
            Mylist = JSON.parse(r.d);
            var i = 0;
            if (Mylist.length > 0) {
                Mylist.forEach(function (item) {
                    var DatePDDT = new Date(item.PDDT);

                    var day = DatePDDT.getDate();
                    var month = DatePDDT.getMonth() + 1;
                    var year = DatePDDT.getFullYear();
                    var id = "MyRow" + i;
                    var idInput = "Myinput" + i;
                    var IdBtnEdit = "MyBtnEdit" + i;
                    var IdBtnCancel = "MyBtnCancel" + i;
                    var IdBtnSave = "MyBtnSave" + i;
                    newRow = '<tr id=' + id + '>' +
                                    '<td>' + item.PAID + '</td>' +
                                    '<td>' + item.CWAR + '</td>' +
                                    '<td>' + item.LOCA + '</td>' +
                                    '<td>' + item.ITEM + '</td>' +
                                    '<td>' + item.DSCA + '</td>' +
                                    '<td>' + item.QTYC + '</td>' +
                                    '<td>' + item.CUNI + '</td><td>';
                    //                    newRowEditButton = '<input type="button" id =' + IdBtnEdit + ' value="Edit" class="btn btn-primary bouton-image" onclick="HabilitarPrioridad(' + idInput + ',' + id + ',' + IdBtnCancel + ',' + IdBtnSave + ',' + IdBtnEdit + ')"></input>&nbsp';
                    newRowCancelButton = '<input type="button" id =' + IdBtnCancel + ' value="Cancel" class="btn btn-danger bouton-image"onclick="DeshabilitarPrioridad(' + id + ',' + IdBtnSave + ',' + IdBtnCancel + ')"></input>&nbsp';
                    newRowSaveButton = '<input type="button" id =' + IdBtnSave + ' value="Change State to Located" class="btn btn-success bouton-image" onclick="AgregrarPrioridad(' + id + ',' + IdBtnCancel + ',' + IdBtnSave + ')"></input>';
                    newRowEnd = '</td></tr>';



                    CuerpoTable += newRow + newRowCancelButton + newRowSaveButton + newRowEnd;
                    i++;
                });
                $('#MyRegisterCustomer').append(EncavezadoTable + CuerpoTable + PieTable);
                $('#lblMsg').html("");
            }
            else {
                $('#lblMsg').html("There are no records for this Plant.");
            }
            console.log(Mylist);
        }

        function ClickSaveSuccess(r) {
            $('.table tbody tr').remove();
            ClickQuery();
//            MylistInsert = JSON.parse(r.d);
//            if (MylistInsert.length > 0) {
//                MylistInsert.forEach(function (x) {
//                    if (x == false) {
//                        $('.table tbody tr').remove();
//                        $('#lblMsg').html("Some record was not inserted");
//                    }
//                    else {
//                        $('.table tbody tr').remove();
//                        $('#lblMsg').html("All records were insert");
//                    }
//                });
//            }
//            else {
//                $('.table tbody tr').remove();
//                $('#lblMsg').html("The insertion was not done");
//            }
        }

        var ClickSave = function () {
            //var Data = "{'ORNO':'" + OrdenID + "','PONO':'" + Position + "','SEQNR':'" + SEQNR + "'}";
            strLstEnviar = JSON.stringify(tticol082);
            var Data = "{'LstJson':'" + strLstEnviar + "'}";
            WebMethod = "ClickSave";
            sendAjax(WebMethod, Data, ClickSaveSuccess, false);
        }

        $(function () {
            //            $('#MyTable').DataTable();
            ClickQuery();
            //SelectPlant();
        });

    </script>
</asp:Content>
