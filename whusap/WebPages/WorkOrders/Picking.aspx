<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="Picking.aspx.cs" Inherits="whusap.WebPages.WorkOrders.Picking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="http://code.jquery.com/jquery-2.1.1.min.js"></script>
    <script type="text/javascript" src="http://cdn.datatables.net/1.10.2/js/jquery.dataTables.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <style type="text/css">
        #MyEtiqueta
        {
            font-size: 14px;
        }
        #MyEtiqueta2 label
        {
            font-size: 15px;
        }
        
        #LblDated
        {
            font-size: 14px !important;
        }
        
        #LblReprintInd, #LblReprint
        {
            display: none;
        }
        .colorButton
        {
            background-color: #C0C0C0;
            background-position: center;
            font-size: 24px;
            height: 100%;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            padding: 5px 5px 5px 5px;
        }
        .hidebutton
        {
            display: none;
        }
        #LblError
        {
            color:Red;
            font-size:14px;
        }
        .colorButton2
        {
            background-color: #3399ff;
            background-position: center;
            font-size: 24px;
            height: 100%;
            width: 70%;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            text-align: left;
            padding: 5px 5px 5px 5px;
            color: white;
        }
        .table2
        {
            font-family: arial, sans-serif;
            border-collapse: collapse;
            width: 70%;
            font-size: 16px;
        }
        
        td, th
        {
            text-align: left;
            padding: 8px;
        }
        .hidetable
        {
            display: none;
        }
    </style>
    <form id="form1" class="container">
    <div class="form-group row">
        <label class="col-sm-2 col-form-label-lg hidebutton" for="txQuantity">
            Pallet ID</label>
        <div class="col-sm-4">
            <input type="text" class="hidebutton" id="txPalletID" placeholder="Pallet ID" data-method="ValidarQuantity">
        </div>
    </div>
    <br />
    <div class="form-group row">
        <input id="btnEnviar" type="button" class="hidebutton" value="START NEXT PICK" />&nbsp;&nbsp;&nbsp
    </div>
    <div id="MyEtiqueta2">
        <table class="table2">

            <tr>
                ADVS: <asp:Label ID="lblADVS" runat="server" CssClass=""></asp:Label>
            </tr>
            <tr>
                <td class="">
                    <label class="" id="Label1">
                        Pallet ID</label>
                </td>
                <td>
                    <asp:Label ID="lblPalletID" runat="server" CssClass=""></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPalletID" MaxLength="20" Style="text-transform: uppercase;" runat="server"
                        CssClass="form-control" onkeyup="validarPallet();" Font-Size="Medium"></asp:TextBox>
                    <asp:Button ID="Reload" runat="server" Text="Next Picking" OnClick="Reload_Click"
                        class="btn btn-primary btn-lg" />
                </td>
            </tr>
            <tr>
                <td class="">
                    <label class="">
                        Item</label>
                </td>
                <td class="">
                    <asp:Label ID="lblItemID" runat="server" CssClass=""></asp:Label>
                </td>
                <td class="">
                    <asp:Label ID="lblItemDesc" runat="server" CssClass=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="">
                    <label class="">
                        Lot</label>
                </td>
                <td class="">
                    <asp:Label ID="LblLotId" runat="server" CssClass=""></asp:Label>
                </td>
                <td class="">
                    <asp:Label ID="LblLotIdDesc" runat="server" CssClass=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="">
                    <label class="">
                        Warehouse</label>
                </td>
                <td class="">
                    <asp:Label ID="lblWarehouse" runat="server" CssClass=""></asp:Label>
                </td>
                <td class="">
                    <asp:Label ID="lblWareDescr" runat="server" CssClass=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="">
                    <label class="">
                        Location</label>
                </td>
                <td class="">
                    <asp:Label ID="lbllocation" runat="server" CssClass=""></asp:Label>
                </td>
                <td class="">
                    <%--<asp:TextBox ID="txtlocation" MaxLength="20" Style="text-transform: uppercase;" runat="server"
                        CssClass="form-control" onkeyup="validarLocation();" Font-Size="Medium"></asp:TextBox>--%>
                    <%--<asp:TextBox ID="txtlocation" MaxLength="20" runat="server"
                         Font-Size="Medium"></asp:TextBox>--%>
                    <input type="text" id="txtlocation" />
                </td>
            </tr>
            <tr>
                <td class="">
                    <label class="">
                        Quantity</label>
                </td>
                <td class="">
                    <asp:Label ID="lblQuantity" runat="server" CssClass=""></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblQuantityDesc" runat="server" CssClass=""></asp:Label>
                </td>
            </tr>
            <tr id="HideReason">
                <td class="">
                    <label class="">
                        Reason</label>
                </td>
                <td>
                    <%-- <asp:DropDownList ID="listRegrind" runat="server"  Width="100%" height="100%" CssClass="DropDownList"    AutoPostBack="false" Font-Size="Larger" OnSelectedIndexChanged="listRegrind_SelectedIndexChanged">
                    </asp:DropDownList>--%>
                    <select class="form-control form-control-lg" id="listCausal" tabindex="1">
                        <option value="0">Select Causal</option>
                        <option value="1">Wrong Lot</option>
                        <option value="2">Aisle Blocked</option>
                        <option value="3">Wrong Location</option>
                    </select>
                </td>
                <td>
                    <input id="bntChange" type="button" class="btn btn-primary btn-lg" onclick="IngresarCausales()"
                        value="CHANGE" />
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <input id="btnconfirPKG" type="button" class="btn btn-primary btn-lg" onclick="ShowCurrentTime()"
                        value="CONFIRM " />
                </td>
            </tr>
            <tr>
                <td>
                    <label id="LblError"></label>
                </td>
            </tr>
        </table>
    </div>
    <div class="hidetable">
        <div class="">
            <table border="1">
                <tr>
                    <th>
                        OORG
                    </th>
                    <th>
                        ORNO
                    </th>
                    <th>
                        OSET
                    </th>
                    <th>
                        PONO
                    </th>
                    <th>
                        SQNB
                    </th>
                    <th>
                        ADVS
                    </th>
                </tr>
                <tr>
                    <td class="">
                        <asp:Label ID="lblOORG" runat="server" CssClass=""></asp:Label>
                    </td>
                    <td class="">
                        <asp:Label ID="lblORNO" runat="server" CssClass=""></asp:Label>
                    </td>
                    <td class="">
                        <asp:Label ID="lblOSET" runat="server" CssClass=""></asp:Label>
                    </td>
                    <td class="">
                        <asp:Label ID="lblPONO" runat="server" CssClass=""></asp:Label>
                    </td>
                    <td class="">
                        <asp:Label ID="lblSQNB" runat="server" CssClass=""></asp:Label>
                    </td>
                    <td class="">
                        
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
    <script type="text/javascript">

        function EventoAjax(Method, Data, MethodSuccess) {
            $.ajax({
                type: "POST",
                url: "Picking.aspx/" + Method.trim(),
                data: Data,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: MethodSuccess
            })
        };

        var flagPallet = 0;
        var flaglocation = 0;

        function IdentificarControles() {


            MyEtiquetaOC = $('#MyEtiquetaOC');
            MyEtiqueta = $('#MyEtiqueta');

            //Formulario

            txPalletID = $('#txPalletID');
            listCausal = $('#listCausal');
            btnEnviar = $('#btnEnviar');


            lblItemID = $('#lblItemID');
            lblItemDesc = $('#lblItemDesc');

            LblQuantity = $('#LblQuantity');
            LblUnit = $('#LblUnit');

            LblLotId = $('#LblLotId');
            LblDate = $('#LblDate');
            LblReprint = $('#LblReprint');
            LblReprintInd = $('#LblReprintInd');
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

            return ' ' + monthNames[monthIndex] + '/' + day + '/' + year + ' ' + hours + ':' + minutes + ':' + seconds + ' ';
        }

        var FuncitionSuccesReprint = function (r) {
            console.log(r.d);
            MyEtiqueta.hide('slow');

            var MyObject = JSON.parse(r.d);
            if (MyObject.MyError == undefined) {

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

        var FuncitionSuccesQuery = function (r) {
            var MyObject = JSON.parse(r.d);

            if (MyObject.error == false) {
                MyEtiqueta.show('slow');
                lblPalletID.html(MyObject.PAID);
                lblWarehouse.html(MyObject.CWAR);
                lblItemID.html(MyObject.ITEM);
                LblLotId.html(MyObject.CLOT);
            }
            else {
                alert(MyObject.errorMsg);
            }

        }

        $(document).keypress(
         function validar(e) {
             var keycode = (e.keyCode ? e.keyCode : e.which);

             var _txt1 = document.getElementById("<%=lblPalletID.ClientID %>").innerHTML.toString();
             var _txt2 = $('#<%=txtPalletID.ClientID%>').val().toString();
             var result = _txt1.trim() == _txt2.trim() ? 1 : 2;

             if (keycode == '13') {
                 if (result === 1) {
                     var flagPallet = 1;
                     document.getElementById("txtlocation").disabled = false;
                     document.getElementById("txtlocation").focus();
                     return false;
                 }
                 else {
                     alert('Pallet Id not equal to the selected pallet');
                     document.getElementById("txtlocation").value = "";
                     document.getElementById("txtlocation").disabled = true;
                     //JUANC
                     return false;
                 }
             }
         });
        //        var PalletIDSucces = function (r) {
        //            if (r.d) {
        //                alert(r.d);

        //            }
        //            else if (!r.d) {
        //                alert(r.d);
        //            }
        //        }
        //         public static bool VerificarLocate(string CWAR,string LOCA)
        //        public static bool VerificarPalletID(string PAID)

        function pulsar2(e) {
            //tecla = (document.all) ? e.keyCode : e.which;
            var _txt3 = document.getElementById("<%=lbllocation.ClientID %>").innerHTML.toString();
            var _txt4 = $('#txtlocation').val().toString();

            var result = _txt3.trim() == _txt4.trim() ? 1 : 2;

            if (tecla == 13)
            //alert("presion la tecla");
                validar2(result);
            return false;

        }

        function validar2(num) {

            //            if (num === 1) {

            //                HideReason.style.display = "none";
            //                document.getElementById("bntChange").disabled = true;
            //                document.getElementById("txtlocation").disabled = true;
            //                document.getElementById('btnconfirPKG').disabled = false;

            //                return false;
            //            }
            //            else {
            //                document.getElementById('btnconfirPKG').disabled = true;
            //                Method = "VerificarLocate"
            //                Data = "{'CWAR':'" + $('#Contenido_lblWarehouse').html().trim() + "','LOCA':'" + $('#txtlocation').val().trim() + "'}";
            //                EventoAjax(Method, Data, PalletIDSuccess)
            //                //JUAN C
            //                return false;
            //            }
        }

        var PalletIDSuccess = function (r) {
            var MyObj = JSON.parse(r.d);

            if (MyObj.error == false) {

                
                HideReason.style.display = "";
                $('#LblError').html("");

                $('#Contenido_LblLotId').html(MyObj.LOT.toString())
                $('#Contenido_lblWarehouse').html(MyObj.WRH.toString())
                $('#Contenido_lblWareDescr').html(MyObj.DESCWRH.toString())
                $('#Contenido_lbllocation').html(MyObj.LOCA.toString())
                $('#Contenido_lblQuantity').html(MyObj.QTY.toString())
                $('#Contenido_lblQuantityDesc').html(MyObj.UN.toString())

                document.getElementById("bntChange").disabled = true;
            }
            else if (MyObj.error == true) {
                HideReason.style.display = "none";
                $('#LblError').html(MyObj.errorMsg);
                document.getElementById("bntChange").disabled = true;
            }
        }

        function ShowCurrentTime() {
            $.ajax({
                type: "POST",
                url: "Picking.aspx/Click_confirPKG",
                data: "{'PAID_OLD':'" + $('#Contenido_lblPalletID').html() + "','PAID':'" + $("#<%=txtPalletID.ClientID%>")[0].value.toUpperCase() + "', 'LOCA':'" + $('#txtlocation').val().toUpperCase() + "','OORG':'" + document.getElementById("<%=lblOORG.ClientID %>").innerHTML.toString() + "','ORNO':'" + document.getElementById("<%=lblORNO.ClientID %>").innerHTML.toString() + "','OSET':'" + document.getElementById("<%=lblOSET.ClientID %>").innerHTML.toString() + "','PONO':'" + document.getElementById("<%=lblPONO.ClientID %>").innerHTML.toString() + "' ,'SQNB':'" + document.getElementById("<%=lblSQNB.ClientID %>").innerHTML.toString() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d == true) {
                        alert("Information saved successfully");
                        //window.location = "/WebPages/Login/whMenuI.aspx";
                        $('#Contenido_lblPalletID').html("");
                        $('#Contenido_lblItemID').html("");
                        $('#Contenido_LblLotId').html("");
                        $('#Contenido_lblWarehouse').html("");
                        $('#Contenido_lbllocation').html("");
                        $('#Contenido_lblQuantity').html("");
                        $('#Contenido_lblItemDesc').html("");
                        $('#Contenido_lblWareDescr').html("");
                        $('#Contenido_lblQuantityDesc').html("");
                        $('#Contenido_txtPalletID').val("");
                        $('#txtlocation').val("");
                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccess(response) {
            alert(response.d);
        }


        function IngresarCausales() {
            $.ajax({
                type: "POST",
                url: "Picking.aspx/Click_confirCausal",
                data: "{'PAID':'" + document.getElementById("<%=lblPalletID.ClientID %>").innerHTML.toString() + "','Causal':'" + document.getElementById("listCausal").value + "' ,'txtPallet':'" + $("#<%=txtPalletID.ClientID%>")[0].value + "','OORG':'" + document.getElementById("<%=lblOORG.ClientID %>").innerHTML.toString() + "','ORNO':'" + document.getElementById("<%=lblORNO.ClientID %>").innerHTML.toString() + "','OSET':'" + document.getElementById("<%=lblOSET.ClientID %>").innerHTML.toString() + "','PONO':'" + document.getElementById("<%=lblPONO.ClientID %>").innerHTML.toString() + "' ,'SQNB':'" + document.getElementById("<%=lblSQNB.ClientID %>").innerHTML.toString() + "','ADVS':'" + document.getElementById("<%=lblADVS.ClientID %>").innerHTML.toString() + "' ,'LOCA':'" + document.getElementById("<%=lbllocation.ClientID %>").innerHTML.toString() + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d == true) {
                        $('#txtlocation').removeAttr('disabled');
                        alert("Reason saved");
                        //window.location = "/WebPages/Login/whMenuI.aspx";
                        if ($('#txtlocation').val().trim().toUpperCase() != $('#Contenido_lbllocation').html().trim().toUpperCase() && $('#Contenido_txtPalletID').val().trim().toUpperCase() == $('#Contenido_lblPalletID').html().trim().toUpperCase()) {
                            document.getElementById('btnconfirPKG').disabled = false;
                        }
                    }
                    else {
                        $('#txtlocation').attr('disabled', 'disabled');
                    }
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function OnSuccess(response) {
            alert("Information not saved");
            window.location.href = "/WebPages/Login/whMenuI.aspx";
        }


        function delayTimer() {
            var timer;
            return function (fun, time) {
                clearTimeout(timer);
                timer = setTimeout(fun, time);
            };
        }

        var delayFunction = delayTimer();

        // Takes parameters to display
        function validarPallet() {

            var _txt1 = document.getElementById("<%=lblPalletID.ClientID %>").innerHTML.toString();
            var _txt2 = $('#<%=txtPalletID.ClientID%>').val().toString().toUpperCase();

            if (_txt2.length == 13) {
                var result = _txt1.trim() == _txt2.trim() ? 1 : 2;

                if (result == 1) {
                    var flagPallet = 1;
                    document.getElementById("txtlocation").disabled = false;
                    document.getElementById("txtlocation").focus();
                    return false;
                }
                else {
                    alert('Pallet Id not equal to the selected pallet');
                    document.getElementById("txtlocation").value = "";
                    document.getElementById("txtlocation").disabled = true;

                    Method = "VerificarPalletID"
                    Data = "{'PAID_NEW':'" + _txt2 + "', 'PAID_OLD':'" + $('#Contenido_lblPalletID').html() +"'}";
                    EventoAjax(Method, Data, PalletIDSuccess)

                    //JUANC
                    return false;
                }
            }
        }


        function validarLocation() {
            var _txt3 = document.getElementById("<%=lbllocation.ClientID %>").innerHTML.toString();
            var _txt4 = $('#txtlocation').val().toString().toUpperCase();

            if (_txt4.length > 0) {
                var result = _txt3.trim() == _txt4.trim() ? 1 : 2;

                //                validar2(result);
            }
        }

        $('#listCausal').change(function (a) {
            if (a.target.selectedIndex != 0) {
                document.getElementById("txtlocation").disabled = true;
                document.getElementById("bntChange").disabled = false;
                $('#txtlocation').focus();
            }
            else {
                document.getElementById("txtlocation").disabled = true;
                document.getElementById("bntChange").disabled = true;

                document.getElementById("txtlocation").value = "";
            }
        });

        var LocateSuccess = function (r) {
            if (r.d == true) {
                document.getElementById("btnconfirPKG").disabled = false;
            }
            else if (r.d == false) {
                document.getElementById("btnconfirPKG").disabled = true;
            }
        }

        var LocateSuccessD = function (r) {
            if (r.d == true) {
                HideReason.style.display = "";
                document.getElementById("btnconfirPKG").disabled = true;
            }
            else if (r.d == false) {
                document.getElementById("btnconfirPKG").disabled = true;
                HideReason.style.display = "none";
            }
        }

        

        $('#txtlocation').bind("change paste keyup", function (a) {

            Method = "VerificarLocate"
            Data = "{'CWAR':'" + $('#Contenido_lblWarehouse').html().trim().toUpperCase() + "','LOCA':'" + $('#txtlocation').val().trim().toUpperCase() + "'}";
//            if($('#Contenido_txtPalletID').val().trim().toUpperCase() == $('#Contenido_lblPalletID').html().trim().toUpperCase() && $('#txtlocation').val().trim().toUpperCase() == $('#Contenido_lbllocation').html().trim().toUpperCase())
//            {
//                document.getElementById('btnconfirPKG').disabled = false;
//            }
//            else if($('#Contenido_txtPalletID').val().trim().toUpperCase() != $('#Contenido_lblPalletID').html().trim().toUpperCase() && $('#txtlocation').val().trim().toUpperCase() == $('#Contenido_lbllocation').html().trim().toUpperCase())
//            {
//            }
//            else if($('#Contenido_txtPalletID').val().trim().toUpperCase() != $('#Contenido_lblPalletID').html().trim().toUpperCase() && $('#txtlocation').val().trim().toUpperCase() != $('#Contenido_lbllocation').html().trim().toUpperCase())
//            {
//                document.getElementById('btnconfirPKG').disabled = false;
//                EventoAjax(Method, Data, LocateSuccess)
//            }
//            else if ($('#Contenido_txtPalletID').val().trim().toUpperCase() == $('#Contenido_lblPalletID').html().trim().toUpperCase() && $('#txtlocation').val().trim().toUpperCase() != $('#Contenido_lbllocation').html().trim().toUpperCase()) 
//            { 
//            }  


            if ($('#txtlocation').val().trim().toUpperCase() == $('#Contenido_lbllocation').html().trim().toUpperCase() && $('#Contenido_txtPalletID').val().trim().toUpperCase() == $('#Contenido_lblPalletID').html().trim().toUpperCase()) {

                HideReason.style.display = "none";
                document.getElementById("bntChange").disabled = true;
                document.getElementById("txtlocation").disabled = false;
                document.getElementById('btnconfirPKG').disabled = false;
            }
            else if ($('#txtlocation').val().trim().toUpperCase() != $('#Contenido_lbllocation').html().trim().toUpperCase() && $('#Contenido_txtPalletID').val().trim().toUpperCase() == $('#Contenido_lblPalletID').html().trim().toUpperCase()) {

                
                document.getElementById("txtlocation").disabled = false;
                document.getElementById('btnconfirPKG').disabled = true;
                EventoAjax(Method, Data, LocateSuccessD)
            }
            else {
                document.getElementById('btnconfirPKG').disabled = false;
                //Method = "VerificarLocate"
                //Data = "{'CWAR':'" + $('#Contenido_lblWarehouse').html().trim().toUpperCase() + "','LOCA':'" + $('#txtlocation').val().trim().toUpperCase() + "'}";
                EventoAjax(Method, Data, LocateSuccess)
                //JUAN C
                return false;
            }
        });

        $(document).ready(function () {
            HideReason.style.display = "none";
            document.getElementById("bntChange").disabled = true;
            document.getElementById("txtlocation").disabled = true;
            document.getElementById("<%=txtPalletID.ClientID %>").focus();
            document.getElementById('btnconfirPKG').disabled = true;


        });

    </script>
</asp:Content>
