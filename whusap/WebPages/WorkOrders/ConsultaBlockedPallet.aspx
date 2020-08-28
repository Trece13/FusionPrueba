<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ConsultaBlockedPallet.aspx.cs" Inherits="whusap.WebPages.WorkOrders.ConsultaBlockedPallet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <style>
        #DivTable
        {
            margin-bottom: 100px;
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
        .sorting_1
        {
            display:none;
        }
        #ColSort
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
                url: "ConsultaBlockedPallet.aspx/" + WebMethod,
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
<%--    <div id="divForm">
        <div class="form-group row ">
            <label class="col-sm-2 col-form-label-lg" for="txCustomer" id="lblCustomer">
                Customer
            </label>
            <div class="col-sm-4">
                <input type="text" class="form-control form-control-lg col-sm-12" id="txCustomer"
                    placeholder="Customer" onkeyup="myFunction()" />
            </div>
            <label class="col-sm-2 col-form-label-lg    " for="txCustomer" id="lblDecCustomer">
            </label>
        </div>
        <div class="form-group row ">
            <label class="col-sm-2 col-form-label-lg" for="txPalletId" id="Label1">
                To Date
            </label>
            <div class="col-sm-4">
                <input type="date" class="form-control form-control-lg col-sm-12" id="txToDate" placeholder="MM/DD/YYYY">
            </div>
        </div>
        <div class="form-group row" id="divQueryAction">
            <div class="col-sm-2">
            </div>
            <div class="col-sm-4">
                <input type="button" class="btn btn-primary btn-lg col-sm-7 " id="btnQuery" value="Query" />
            </div>
        </div>
    </div>--%>
    <br />
    <label id="lblMsg">
    </label>
    <br />
    <div id="DivTable">
        <div class="form-group row" id="MyRegisterCustomer">
<%--            <table class="table display" id="example" class="display" cellspacing="0" width="100%">
                <thead>
                    <tr>
                        <th scope="col">
                            Sales Order
                        </th>
                        <th scope="col">
                            Set
                        </th>
                        <th scope="col">
                            Position
                        </th>
                        <th scope="col">
                            Sequence
                        </th>
                        <th scope="col">
                            Item
                        </th>
                        <th scope="col">
                            Description
                        </th>
                        <th scope="col">
                            Sales Qlt
                        </th>
                        <th scope="col">
                            Sales Unit
                        </th>
                        <th scope="col">
                            Stock Qty
                        </th>
                        <th scope="col">
                            Stock Unit
                        </th>
                        <th scope="col">
                            Planet Del Date
                        </th>
                        <th scope="col">
                            Deliver
                        </th>
                    </tr>
                </thead>
                <tbody>
                </tbody>
            </table>--%>
        </div>
        <div class="form-group row" id="div2">
            <div class="col-sm-10">
            </div>
<%--            <div class="col-sm-2">
                <input type="button" class="btn btn-primary btn-sm col-sm-12 flotante" id="btnSave"
                    value="Save" />
            </div>--%>
        </div>
    </div>
    </form>
    <script>
        $(document).ready(function () {
            $('#MyTable').dataTable({
                columnDefs: [
                              { targets: 'no-sort', orderable: false }
                            ]
             });
            ClickQuery();
            setInterval(function () { paginaSiguinete() }, <%=CicloPaginacion %>);
        });
    </script>
    <script>
        var next = true;
        function paginaSiguinete() {
            if (next) {
                $("#MyTable_next").click();
                if ($("#MyTable_next").hasClass("disabled")) {
                    next = false;
                }
            }
            else {
                $("#MyTable_previous").click();
                if ($("#MyTable_previous").hasClass("disabled")) {
                    next = true;
                }
            }
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
            ClickQuery();
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


        function ClickQuerySuccess(r) {

            var EncavezadoTable = "<table class='table display' id='MyTable' cellspacing='0' width='100%'>" +
                                    "<thead>" +
                                    "<tr>" +
                                    "<th scope='col' id='ColSort'></th>" +
                                    "<th scope='col'>Pallet ID</th>" +
                                    "<th scope='col'>Warehouse</th>" +
                                    "<th scope='col'>Location</th>" +
                                    "<th scope='col'>Item/th>" +
                                    "<th scope='col'>Description</th>" +
                                    "<th scope='col'>Quantity</th>" +
                                    "<th scope='col'>Unit</th>" +
                                    "</tr>" +
                                    "</thead>" +
                                    "<tbody>";
            var PieTable = "</tbody></table >" + "<script>$('#MyTable').dataTable();<" + "/script><style>#MyTable_wrapper{width: 100%} .paginate_button{font-size:14px; margin-left:5px;} #MyTable_length{font-size:12px; margin-rigth:5px;} #MyTable_filter{font-size:12px; margin-rigth:5px;} <" + "/style>";
            var CuerpoTable = "";

            $('.table').remove();
            $('#MyTable_wrapper').remove();
            Mylist = JSON.parse(r.d);
            var i = 0;
            if (Mylist.length > 0) {
                //alert(Mylist);
                Mylist.forEach(function (item) {
                    var DatePDDT = new Date(item.PDDT);

                    var day = DatePDDT.getDate();
                    var month = DatePDDT.getMonth() + 1;
                    var year = DatePDDT.getFullYear();
                    var id = "MyRow" + i;  


                    newRow = '<tr id=' + id + '>' +
                                    '<td>' + item.PAID + '</td>' +
                                    '<td>' + item.PAID + '</td>' +
                                    '<td>' + item.CWAR + '</td>' +
                                    '<td>' + item.LOCA + '</td>' +
                                    '<td>' + item.ITEM + '</td>' +
                                    '<td>' + item.DSCA + '</td>' +
                                    '<td>' + item.QTYC + '</td>' +
                                    '<td>' + item.CUNI + '</td>' +
                                  '</tr>';
                    CuerpoTable += newRow
                    i++;
                });
                $('#MyRegisterCustomer').append(EncavezadoTable + CuerpoTable + PieTable);
                $('#lblMsg').html("");
            }
            else {
                //alert(Mylist)
                $('#lblMsg').html("There are no records for this customer");
            }
            //alert("ClikSaveSuccess");
            console.log(Mylist);
        }

        function ClickSaveSuccess(r) {
            $('.table tbody tr').remove();
            MylistInsert = JSON.parse(r.d);
            if (MylistInsert.length > 0) {
                MylistInsert.forEach(function (x) {
                    if (x == false) {
                        $('.table tbody tr').remove();
                        $('#lblMsg').html("Some record was not inserted");
                    }
                    else {
                        $('.table tbody tr').remove();
                        $('#lblMsg').html("All records were insert");
                    }
                });
            }
            else {
                $('.table tbody tr').remove();
                $('#lblMsg').html("The insertion was not done");
            }
        }

        var ClickSave = function () {
            //var Data = "{'ORNO':'" + OrdenID + "','PONO':'" + Position + "','SEQNR':'" + SEQNR + "'}";
            strLstEnviar = JSON.stringify(lstEnviar);
            var Data = "{'LstJson':'" + strLstEnviar + "'}";
            WebMethod = "ClickSave";
            sendAjax(WebMethod, Data, ClickSaveSuccess, false);
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


    </script>
</asp:Content>