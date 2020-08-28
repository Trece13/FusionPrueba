<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ConsultaEdit.aspx.cs" Inherits="whusap.WebPages.WorkOrders.ConsultaEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@9"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"
        integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN"
        crossorigin="anonymous">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <style>
        tr:hover {
          color: #0069d9;
        }
        .hidden
        {
            display:none;
        }
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
        <%--#ColSort
        {
            display:none;
            
        }--%>
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
        
        #MyTable_filter
        {
            display:none;
        }
        
        .btn-danger
        {
            display:none;
        }
<%--        .btn-success
        {
            display:none;
        }--%>
        #divMachine
        {
            display:none;
        }
        #divWarehouse
        {
            display:none;
        }
        #btnQuery
        
        {
            display:none !important;
        }
        th
        {
            border-bottom:none !important;
        }
        #MyRegisterCustomer
        {
            display:none;
        }
    </style>
    <script>
        var timer;
        lstEnviar = [];
        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "ConsultaEdit.aspx/" + WebMethod,
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
    <div id="divForm" class="m-5">
        <div class="form-group row ">
            <label id="divPlant" class="col-sm-2 col-form-label-lg" for="txCustomer" id="lblCustomer">
                Plant
            </label>
            <div class="col-sm-4">
                <select class="form-control form-control-lg col-sm-12" id="DdPlant" placeholder="Plant" onchange = "SelectWarehouse(this.value)">
                    <option value="0" selected>Select Plant</option>
                </select>
            </div>
            <label class="col-sm-2 col-form-label-lg" for="txPlant" id="lblDecCustomer">
            </label>
        </div>
        <div id="divWarehouse" class="form-group row ">
            <label class="col-sm-2 col-form-label-lg" for="txCustomer" id="Label1">
                Warehouse
            </label>
            <div class="col-sm-4">
                <select class="form-control form-control-lg col-sm-12" id="DdWarehouse" placeholder="Warehouse" onchange = "SelectMachine($('#DdPlant').val(),this.value)">
                    <option value="0" selected>Select Warehouse</option>
                </select>
            </div>
            <label class="col-sm-2 col-form-label-lg" for="txPlant" id="Label2">
            </label>
        </div>
        <div id="divMachine" class="form-group row ">
            <label class="col-sm-2 col-form-label-lg" for="txCustomer" id="Label3">
                Machine
            </label>
            <div class="col-sm-4">
                <select class="form-control form-control-lg col-sm-12" id="DdMachine" placeholder="Machine" onchange = "changeMachine()">
                    <option value="0" selected>Select Machine</option>
                </select>
            </div>
            <label class="col-sm-2 col-form-label-lg" for="txPlant" id="Label4">
            </label>
        </div>
        <%--        <div class="form-group row ">
            <label class="col-sm-2 col-form-label-lg" for="txPalletId" id="Label1">
                To Date
            </label>
            <div class="col-sm-4">
                <input type="date" class="form-control form-control-lg col-sm-12" id="txToDate" placeholder="MM/DD/YYYY">
            </div>
        </div>--%>
        <div class="form-group row" id="divQueryAction">
            <div class="col-sm-2">
            </div>
            <div class="col-sm-4">
                <input type="button" class="btn btn-primary btn-lg col-sm-7 " id="btnQuery" value="Query" />
            </div>
        </div>
    </div>
    <hr />
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
            <div class="col-sm-2 hidden">
                <input type="button" class="btn btn-primary btn-sm col-sm-12 flotante" id="btnSave"
                    value="Save" />
            </div>
        </div>
    </div>
    </form>
    <script>
        $(document).ready(function () {
            $('#MyTable').DataTable();
            SelectPlant();
        });
    </script>
    <script>
        modificando = false;
        var tticol082 = [];
        function HabilitarPrioridad(idMyInput, idMyRow, BtnCancel, BtnSave, BtnEdit) {
            //alert("Habiltar " + idMyInput);
            if (modificando == false) {
                modificando = true;
                console.log(idMyInput);
                $(idMyInput).removeAttr('disabled');
                $(BtnCancel).show();
                $(BtnSave).show();
                $(BtnEdit).hide();
            }
        }
        function AgregrarPrioridad(idMyInput, idMyRow, BtnCancel, BtnEdit, BtnSave) {
            //alert("Agregar " + idMyInput);
            if ($(idMyInput).val() == idMyRow.cells[0].innerHTML) {
                modificando = false;
                $(BtnCancel).hide();
                $(BtnSave).hide();
                $(BtnEdit).show();
            }
            else {
                tticol082 = [];
                modificando = true;


                console.log(idMyInput);
                $(idMyInput).attr('disabled', 'disabled');

                $(BtnCancel).hide();
                $(BtnSave).hide();
                $(BtnEdit).show();

                Mylist.forEach(function (item) {
                    if (item.ORNO.trim() == idMyRow.cells[1].innerHTML.trim() && item.ITEM.trim() == idMyRow.cells[2].innerHTML.trim() && item.TIME.trim() == idMyRow.cells[8].innerHTML.trim() && item.PRIO == idMyRow.cells[0].innerHTML.trim() && item.ADVS == idMyRow.cells[9].innerHTML.trim()) {
                        if (tticol082.length > 0) {
                            tticol082.forEach(function (item82) {
                                if (item82.ORNO.trim() == idMyRow.cells[1].innerHTML.trim() && item82.ITEM.trim() == idMyRow.cells[2].innerHTML.trim() && item82.TIME.trim() == idMyRow.cells[8].innerHTML.trim() && item82.PRIO == idMyRow.cells[0].innerHTML.trim()) {
                                    item82.PRIO = idMyRow.cells[0].innerHTML.trim();
                                    item82.PRIT = idMyInput.value;
                                }
                                else {
                                    item.PRIO = idMyRow.cells[0].innerHTML.trim();
                                    item.PRIT = idMyInput.value;
                                    tticol082.push(item);
                                }
                            });
                        }
                        else {
                            item.PRIO = idMyRow.cells[0].innerHTML.trim();
                            item.PRIT = idMyInput.value;
                            tticol082.push(item);
                        }
                    }
                });
            }
        }


        function AgregrarPrioridadSave(idMyInput, idMyRow,i) {
            //alert("Agregar " + idMyInput);

            $("#MyBtnSave"+i).hide(500);
            $("#loadIco"+i).show(500);
            $(".btnEdit").prop("disabled", true);
            if ($(idMyInput).val() == idMyRow.cells[0].innerHTML) {
                modificando = false;
            }
            else {
                tticol082 = [];
                modificando = true;

                Mylist.forEach(function (item) {
                    if (item.ORNO.trim() == idMyRow.cells[1].innerHTML.trim() && item.ITEM.trim() == idMyRow.cells[2].innerHTML.trim() && item.TIME.trim() == idMyRow.cells[8].innerHTML.trim() && item.PRIO == idMyRow.cells[0].innerHTML.trim() && item.ADVS == idMyRow.cells[9].innerHTML.trim()) {
                        if (tticol082.length > 0) {
                            tticol082.forEach(function (item82) {
                                if (item82.ORNO.trim() == idMyRow.cells[1].innerHTML.trim() && item82.ITEM.trim() == idMyRow.cells[2].innerHTML.trim() && item82.TIME.trim() == idMyRow.cells[8].innerHTML.trim() && item82.PRIO == idMyRow.cells[0].innerHTML.trim()) {
                                    item82.PRIO = idMyRow.cells[0].innerHTML.trim();
                                    item82.PRIT = idMyInput.value;
                                }
                                else {
                                    item.PRIO = idMyRow.cells[0].innerHTML.trim();
                                    item.PRIT = idMyInput.value;
                                    tticol082.push(item);
                                }
                            });
                        }
                        else {
                            item.PRIO = idMyRow.cells[0].innerHTML.trim();
                            item.PRIT = idMyInput.value;
                            tticol082.push(item);
                        }
                    }
                });
                btnSave.click();
            }
        }

        function DeshabilitarPrioridad(idMyInput, idMyRow, BtnEdit, BtnSave, BtnCancel) {
            //alert("Deshabiliatar " + idMyInput);
            modificando = false;
            console.log(idMyInput);
            $(idMyInput).attr('disabled', 'disabled');
            $(idMyInput).val(idMyRow.cells[0].innerHTML);

            $(BtnCancel).hide();
            $(BtnSave).hide();
            $(BtnEdit).show();
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
            tticol082 = [];
            ClickQuery($('#DdPlant').val(), ($('#DdWarehouse').val() == "0" ? "" : $('#DdWarehouse').val()), ($('#DdMachine').val() == "0" ? "" : $('#DdMachine').val()));
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

        var ClickQuery = function ( plant, warehouse, machine ) {
            //var Data = "{'ORNO':'" + OrdenID + "','PONO':'" + Position + "','SEQNR':'" + SEQNR + "'}";
            var Data = "{plant:'" + plant + "',warehouse:'" + warehouse + "',machine:'" + machine + "'}";
            WebMethod = "ClickQuery";
            sendAjax(WebMethod, Data, ClickQuerySuccess, false);
        }

        var SelectPlant = function () {
            //var Data = "{'ORNO':'" + OrdenID + "','PONO':'" + Position + "','SEQNR':'" + SEQNR + "'}";
            var Data = "{}";
            WebMethod = "SelectPlant";
            sendAjax(WebMethod, Data, SelectPlantSuccess, false);
        }

        var SelectWarehouse = function (idpland) {
            //alert(idpland);
            //var Data = "{'ORNO':'" + OrdenID + "','PONO':'" + Position + "','SEQNR':'" + SEQNR + "'}";
            $('#DdWarehouse').find('option[value!="0"]').remove();
            $('#DdMachine').find('option[value!="0"]').remove();
            var Data = "{'plant':'" + idpland + "'}";
            WebMethod = "SelectWarehouse";
            sendAjax(WebMethod, Data, SelectWarehouseSuccess, false);
        }

        var SelectMachine = function (idpland, warehouse) {
            $('#DdMachine').find('option[value!="0"]').remove();
            //alert(idpland + "+" + warehouse);
            //alert(idpland);
            //var Data = "{'ORNO':'" + OrdenID + "','PONO':'" + Position + "','SEQNR':'" + SEQNR + "'}";
            var Data = "{'plant':'" + idpland + "','warehouse':'" + warehouse + "'}";
            WebMethod = "SelectMachine";
            sendAjax(WebMethod, Data, SelectMachineSuccess, false);
        }

        var changeMachine = function () {
            if ($('#DdMachine').val() == "0") {
            }
            else {
                btnQuery.click();
            }
        }
        function SelectPlantSuccess(r) {
            MylistPlant = JSON.parse(r.d);

            if (MylistPlant.length > 0) {

                MylistPlant.forEach(function (item) {
                    $('#DdPlant').append('<option value="' + item.TITE + '">' + item.TITE + " - " + item.DSCA + '</option>');
                });

            }
        };

        function SelectWarehouseSuccess(r) {
            MylistWarehouse = JSON.parse(r.d);
            if ($('#DdPlant').val()=="0")
            {
                $('#btnQuery').hide(700);
            }
            else{
                $('#btnQuery').show(700);
                btnQuery.click();
            }

            if (MylistWarehouse.length > 0) {

                MylistWarehouse.forEach(function (item) {
                    $('#DdWarehouse').append('<option value="' + item.TITE + '">' + item.TITE + " - " + item.DSCA + '</option>');
                });
                $('#divWarehouse').show(700);
                $('#divMachine').hide(700);
            }
            else {
                $('#divWarehouse').hide(700);
                $('#divMachine').hide(700);
            }
        };

        function SelectMachineSuccess(r) {
            MylistWarehouse = JSON.parse(r.d);
            if ($('#DdWarehouse').val() == "0") {
            }
            else {
                btnQuery.click();
            }
            if (MylistWarehouse.length > 0) {

                MylistWarehouse.forEach(function (item) {
                    $('#DdMachine').append('<option value="' + item.MCNO + '">' + item.MCNO + " - " + item.DSCA + '</option>');
                });
                $('#divMachine').show(700);
            }
            else {
                $('#divMachine').hide(700);
            }
        };

        function ClickQuerySuccess(r) {

            var EncavezadoTable = "<table class='table display' id='MyTable' cellspacing='0' width='100%'>" +
                                    "<thead>" +
                                    "<tr>" +
                                    "<th scope='col' id='ColSort' style='color:white !important'>Priority</th>" +
                                    "<th scope='col'>Work Order</th>" +
                                    "<th scope='col'>Item</th>" +
                                    "<th scope='col'>Description</th>" +
                                    "<th scope='col'>Quantity</th>" +
                                    "<th scope='col'>Unit</th>" +
                                    "<th scope='col'>Warehouse</th>" +
                                    "<th scope='col'>Machine</th>" +
                                    "<th scope='col'>Request Time</th>" +
                                    "<th scope='col'>Priority</th>" +
                                    "</tr>" +
                                    "</thead>" +
                                    "<tbody id='bodytb'>";
            var PieTable = "</tbody></table >" +
            "<script>$('#MyTable').dataTable();<" +
            "/script><style>#MyTable_wrapper{width: 100%} .paginate_button{font-size:14px; margin-left:5px;} #MyTable_length{font-size:12px; margin-rigth:5px;} #MyTable_filter{font-size:12px; margin-rigth:5px;} <" + "/style>";
            var CuerpoTable = "";

            $('.table').remove();
            $('#MyTable_wrapper').remove();
            Mylist = JSON.parse(r.d);
            var i = 0;
            if (Mylist.length > 0) {
                //alert(Mylist);
                $('#MyRegisterCustomer').hide(100);
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
                    //                    OORG      
                    //                    ORNO      
                    //                    OSET      
                    //                    PONO      
                    //                    SQNB      
                    //                    ADVS      
                    //                    ITEM      
                    //                    QTYT      
                    //                    UNIT      
                    //                    CWAR      
                    //                    MCNO      
                    //                    TIME      
                    //                    PRIO      
                    //                    PICK      
                    //                    PAID      
                    //                    LOCA      
                    //                    LOGN      
                    //                    STAT      


                    newRow = '<tr id=' + id + '>' +
                                    '<td id="prio' + i + '" style="color:white !important">' + item.PRIO + '</td>' +
                                    '<td id="orno' + i + '">' + item.ORNO + '</td>' +
                                    '<td id="item' + i + '">' + item.ITEM + '</td>' +
                                    '<td id="dscai' + i + '">' + item.DSCAI + '</td>' +
                                    '<td id="qtyt' + i + '">' + item.QTYT + '</td>' +
                                    '<td id="unit' + i + '">' + item.UNIT + '</td>' +
                                    '<td id="cwar' + i + '">' + item.CWAR + '</td>' +
                                    '<td id="mcno' + i + '">' + item.MCNO + '</td>' +
                                    '<td id="time' + i + '">' + item.TIME + '</td>' +
                                    '<td style="display:none">' + item.ADVS + '</td>' +
                                    '<td style="width:200px">' +
                                    '<div class="input-group mb-3"><div class="input-group-prepend"><button class="btn btn-primary btnEdit" type="button" onclick="EnabledPono(' + idInput + ',' + i + ')"><i class="fa fa-pencil" aria-hidden="true"></i></button></div><input type="text" style="width:110px; class="form-control" placeholder="' + item.PRIO + '" aria-label="Example text with button addon" aria-describedby="button-addon1" id=' + idInput + ' value=' + item.PRIO + ' disabled></div>' +
                                    '<td style="width:200px"><input type="number" class = "MyPrio form-control hidden" style="width:110px; display: inline-block;" id= aux' + idInput + ' value=' + item.PRIO + ' disabled>';
                    //                    newRowEditButton = item.PICK.trim() == "" ? '<input type="button" id =' + IdBtnEdit + ' value="Edit" class="btn btn-primary bouton-image"  style="width:50px" onclick="HabilitarPrioridad(' + idInput + ',' + id + ',' + IdBtnCancel + ',' + IdBtnSave + ',' + IdBtnEdit + ')"></input>&nbsp' : '';
                    //                    newRowCancelButton = item.PICK.trim() == "" ? '<input type="button" id =' + IdBtnCancel + ' value="Cancel" class="btn btn-danger  bouton-image" style="width:50px" onclick="DeshabilitarPrioridad(' + idInput + ',' + id + ',' + IdBtnEdit + ',' + IdBtnSave + ',' + IdBtnCancel + ')"></input>&nbsp' : '';
                    //                    newRowSaveButton = item.PICK.trim() == "" ? '<input type="button" id =' + IdBtnSave + ' value="Save" class="btn btn-success bouton-image" style="width:50px" onclick="AgregrarPrioridad(' + idInput + ',' + id + ',' + IdBtnCancel + ',' + IdBtnEdit + ',' + IdBtnSave + ')"></input>' : '';
                    newRowSaveButton = item.PICK.trim() == "" ? '<i class="fa fa-spinner fa-pulse fa-lg fa-fw loadIco" style="display: none;" id="loadIco' + i + '"></i><input type="button"  id =' + IdBtnSave + ' value="Save" class="btn btn-success save-button" style="width:50px;display:none" onclick="AgregrarPrioridadSave(' + idInput + ',' + id + ',' + i +')" ></input>' : '';
                    newRowEnd = '</td></tr>';



                    //CuerpoTable += newRow + newRowEditButton + newRowCancelButton + newRowSaveButton + newRowEnd;
                    CuerpoTable += newRow + newRowSaveButton + newRowEnd;
                    i++;
                });
                $('#MyRegisterCustomer').append(EncavezadoTable + CuerpoTable + PieTable);
                $('#lblMsg').html("");
                $('#MyRegisterCustomer').show(1000);
            }
            else {
                //alert(Mylist)
                $('#lblMsg').html("There are no records for this Plant.");
            }
            //alert("ClikSaveSuccess");
            console.log(Mylist);
        }




        function ClickSaveSuccess(r) {
            $(".loadIco").hide(100);
            modificando = false;
            var tticol082 = [];
            Mylist = [];
            $('.table tbody tr').hide(50);
            $('.table').remove();
            $('.table tbody tr').remove();
            btnQuery.click();
//            MylistInsert = JSON.parse(r.d);
//            if (MylistInsert.length > 0) {
//                MylistInsert.forEach(function (x) {
//                    if (x == false) {
//                        $('.table tbody tr').hide(50);
//                        $('.table tbody tr').remove();
//                        $(".loadIco").hide(100);
//                        $('#lblMsg').html("Some record was not inserted");
//                    }
//                    else {
//                        $('.table tbody tr').hide(50);
//                        $('.table tbody tr').remove();
//                        $(".loadIco").hide(100);
//                        $('#lblMsg').html("All records were insert");
//                        
//                    }
//                });
//            }
//            else {
//                $('.table tbody tr').hide(50);
//                $('.table tbody tr').remove();
//                $('#lblMsg').html("The insertion was not done");
//                $(".loadIco").hide(100);
//            }
            
        }

        var ClickSave = function () {
            //var Data = "{'ORNO':'" + OrdenID + "','PONO':'" + Position + "','SEQNR':'" + SEQNR + "'}";
            strLstEnviar = JSON.stringify(tticol082);
            var Data = "{'LstJson':'" + strLstEnviar + "'}";
            WebMethod = "ClickSave";
            sendAjax(WebMethod, Data, ClickSaveSuccess, true);
        }

        $('.checkBoxSave').click(function () {
            if (!$(this).is(':checked')) {
                //return confirm("Are you sure?");
            }
        });

        var ClickSaveRow = function (i) {

            var idRow = "#MyRow" + i;
            var idInput = "#Myinput" + i;
            var idBtnSave = "#MyBtnSave" + i;
            var Row = $(idRow);
            var Input = $(idInput);
            var BtnSave = $(idBtnSave);


            var prio = $("#prio" + i).html().trim();
            var orno = $("#orno" + i).html().trim();
            var item = $("#item" + i).html().trim();
            var dsca = $("#dscai" + i).html().trim();
            var qtyt = $("#qtyt" + i).html().trim();
            var unit = $("#unit" + i).html().trim();
            var cwar = $("#cwar" + i).html().trim();
            var mcno = $("#mcno" + i).html().trim();
            var time = $("#time" + i).html().trim();


            console.log(Row);
            console.log(Input);
            console.log(prio);
            console.log(orno);
            console.log(item);
            console.log(dsca);
            console.log(qtyt);
            console.log(unit);
            console.log(cwar);
            console.log(mcno);
            console.log(time);


            strLstEnviar = JSON.stringify(tticol082);
            var Data = "{'LstJson':'" + strLstEnviar + "'}";
            WebMethod = "ClickSave";
            sendAjax(WebMethod, Data, ClickSaveSuccess, false);

            //console.lotimeg(BtnSave);
            //            strLstEnviar = JSON.stringify(tticol082);
            //            var Data = "{'LstJson':'" + strLstEnviar + "'}";
            //            WebMethod = "ClickSave";
            //            sendAjax(WebMethod, Data, ClickSaveSuccess, false);
        }

        $('.checkBoxSave').click(function () {
            if (!$(this).is(':checked')) {
                //return confirm("Are you sure?");
            }
        });

        function ClickMyCheckBox(MyRow, MyCheck) {
            Mylist.forEach(function (item) {
                if (item.ORNO.trim() == $(MyRow).find('td').eq(0).html().trim() && item.OSET == $(MyRow).find('td').eq(1).html().trim() && item.PONO == $(MyRow).find('td').eq(2).html().trim() && item.SEQN == $(MyRow).find('td').eq(3).html().trim() && item.ITEM.trim() == $(MyRow).find('td').eq(4).html().trim()) {
                    if ($(MyCheck).is(':checked')) {
                        lstEnviar.push(item);
                    }
                    else {
                        var i = lstEnviar.indexOf(item);
                        if (i !== -1) {
                            lstEnviar.splice(i, 1);
                        }
                    }
                }
                console.log(lstEnviar);
            });

        }

        function EnabledPono(a, row) {
            var rowsNum = $('#MyTable tbody tr').length;
            $("#" + a.id).prop("disabled", false);
            $("#" + a.id).focus();
            $("#MyBtnSave" + row).show(100);
            $("#" + a.id).val("");

            for (var i = 0; i < rowsNum; i++) {
                if (row != i) {

                    $("#Myinput" + i).prop("disabled", true);
                    $("#MyBtnSave" + i).hide(100);
                }
            }

        }

        function DisabledPono(a, i) {
            $("#" + a.id).prop("disabled", true);
            $("#" + a.id).val($("#aux" + a.id).val());
            $("#MyBtnSave" + i).show().addClass("hidden");
        }

        function changeValuePlant(a) {
                    
            if ($("#" + a.id).val().toString().trim() != $("#aux" + a.id).val().toString().trim()){
                setTimeout(function () { alert("El valor ha cambiado") }, 1000);
            }
        }

        function GuardarPrioridad(i) {
            var myRow = "MyRow" + i;
            var myInput = "Myinput" + i;

            if ($("#" + myInput).val().trim() == $("#aux" + myInput).val().trim()) {
                return;
            }
            else {
                alet("Hola");
            }
        }


    </script>
</asp:Content>
