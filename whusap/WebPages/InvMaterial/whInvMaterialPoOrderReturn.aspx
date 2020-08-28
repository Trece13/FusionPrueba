<%@ Page Title="" Language="C#" MasterPageFile="../../MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="whInvMaterialPoOrderReturn.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvMaterialPoOrderReturn"
    EnableViewStateMac="false" Theme="Cuadriculas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
     <style>
     .mGrid
     {
         width:100% !important;
     }
      #ShowModal
        {
            display:none;
        }
        #btnEnviar
        {
               display:none;
        }
        #ShowModalMsg
        {
            display:none;
        }
        .form-group
        {
            margin-bottom: 0.5rem;
        }
        
        #MyEtiqueta
        {
            display: none;
            padding-left: 50px;
        }
        
        #MyEtiquetaOC
        {
            display: none;
        }
        
        #lblError
        {
            color: Red;
            font-size: 24px;
        }
     
     </style>
      <script type="text/javascript">
          function printDiv(divID) {

              var monthNames = [
                "1", "2", "3",
                "4", "5", "6", "7",
                "8", "9", "10",
                "11", "12"
              ];

              //PRINT LOCAL HOUR
              var d = new Date();
              var LbdDate = $("#Lbldate");
              var getDate = monthNames[d.getMonth()] +
                "/" +
                d.getDate() +
                "/" +
                d.getFullYear() +
                " " +
                d.getHours() +
                ":" +
                d.getMinutes() +
                ":" +
                d.getSeconds();



              LbdDate.html(
              getDate
                );
              //            //Get the HTML of div
              //            var divElements = document.getElementById(divID).innerHTML;
              //            //Get the HTML of whole page
              //            var oldPage = document.body.innerHTML;
              //            //Reset the page's HTML with div's HTML only
              //            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body></html>";
              //            //Print Page
              //            window.print();
              //            //Restore orignal HTML
              //            document.body.innerHTML = oldPage;
              //            window.close();
              //            return true;

              var mywindow = window.open('', 'PRINT', 'height=400,width=600');

              mywindow.document.write('<html><head><title>' + document.title + '</title>');
              mywindow.document.write('</head><body >');
              //mywindow.document.write('<h1>' + document.title + '</h1>');
              mywindow.document.write(document.getElementById(divID).innerHTML);
              mywindow.document.write('</body></html>');

              mywindow.document.close(); // necessary for IE >= 10
              mywindow.focus(); // necessary for IE >= 10*/

              mywindow.print();
              mywindow.close();

              return true;
          };
        
    </script>
     <script type="text/javascript">
        var _idioma = '<%= _idioma %>';

       // $("#MyEtiquetaOC")
        
        function validaPaid(val) {
            //var parametrosEnviar = "{ 'valor':'" + args.value + "', 'quantityToReturn': '" + quantityToReturn + "'}";
            // var quantityToReturn = $("#Contenido_grdRecords_toReturn_" + rowIndex).val();
            var returnOrder = document.getElementById('<%=txtReturnOrder.ClientID %>').value;
            var e = document.getElementById('<%=dropDownPosition.ClientID %>');
            var position = e.options[e.selectedIndex].value;
            if (position == "") {

                alert("Please Select Position First");
                return false;
            }
            var parametrosEnviar = "{ 'palletID': '" + val + "','position':'" + position + "','returnOrder':'" + returnOrder + "'}";
            //alert(" pId " + args.value + " toReturn " + quantityToReturn);
            var objSend = document.getElementById('<%=btnSend.ClientID %>');
            objSend.disabled = true;
            $.ajax({
                type: "POST",
                url: "whInvMaterialPoOrderReturn.aspx/vallidatePalletID",
                data: parametrosEnviar,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        }
        function insertData() {


            var OORG = 2;
            var PALLET = document.getElementById('<%=txtPalletIDValue.ClientID %>').value;
            var ORNO = document.getElementById('<%=txtReturnOrder.ClientID %>').value;
            var ITEM =  document.getElementById('<%=lblItemValue1.ClientID %>').value;
            var LOT = document.getElementById('<%=lbllotValue.ClientID %>').textContent;
            var e = document.getElementById('<%=dropDownPosition.ClientID %>');
            var PONO = e.options[e.selectedIndex].value;
           
            var QUANTITY =document.getElementById('<%=lblqtyValue1.ClientID %>').value;
            var UNIT =  document.getElementById('<%=lblUnitValue1.ClientID %>').value;
          
            var STUN = document.getElementById('<%=lblstun.ClientID %>').value;


            var Data = "{'OORG':'" + OORG + "', 'ORNO':'" + ORNO + "',  'ITEM':'" + ITEM + "',  'PONO':'" + PONO + "',  'LOT':'" + LOT + "',  'QUANTITY':'" + QUANTITY + "',  'STUN':'" + STUN + "','PALLET':'" + PALLET + "','UNIT':'" + UNIT + "'}";
            sendAjax("InsertarReseiptRawMaterial", Data, InsertSucces, false);


        }


        var InsertSucces = function (r) {

            MyObject = JSON.parse(r.d);


            if (MyObject.error == false) {
                //Etiqueta Sin orden de compra


                // Etiqueta orden de compra

                $("#MyEtiqueta").show()
                CBPurchaseOrder = $('#Contenido_CBPurchaseOrder');
                CBItem = $('#Contenido_CBItem');
                CBLot = $('#Contenido_CBLot');
                CBQuantity = $('#Contenido_CBQuantity');
                CBUnit = $('#Contenido_CBUnit');
                LblDate = $('#LblDate');
                LblReprint = $('#LblReprint');

                LblPurchaseOC = $('#LblPurchaseOC');
                LblItemOC = $('#LblItemOC');
                LblLotOC = $('#LblLotOC');
                LblUnitOC = $('#LblUnitOC');
                LblQuantityOC = $('#LblQuantityOC');
                Lbluser = $('#Lbluser');

                CBPurchaseOrder.attr("src", MyObject.ORNO_URL);
                CBItem.attr("src", MyObject.ITEM_URL);
                CBLot.attr("src", MyObject.CLOT_URL);
                if (MyObject.CLOT_URL == "") {
                    CBLot.hide();
                }
                else {
                    CBLot.show();
                }
                CBQuantity.attr("src", MyObject.QTYC_URL);
                CBUnit.attr("src", MyObject.UNIC_URL);

                LblPurchaseOC.html(MyObject.ORNO);
                LblItemOC.html(MyObject.ITEM);
                LblLotOC.html(MyObject.CLOT);
                LblUnitOC.html(MyObject.UNIT);
                LblQuantityOC.html(MyObject.QTYC);
                Lbluser.html(MyObject.LOGN);

                if (MyObject.OORG != "2" && MyObject != undefined) {
                    //btnMyEtiqueta.show();
                }
                else if (MyObject != undefined) {
                    //btnMyEtiqueta.show();
                }

              //   DeshabilitarLimpiarControles();
                printDiv('MyEtiqueta');


            }
            else {
                console.log("El registro no se realizo");
                alert(MyObject.errorMsg);
            }


        }
        function test() {

            alert('test');

        }

        function OnSuccess(response) {

            if (response != null && response.d != null) {
                var data = response.d;


                if (data.length <= 70) {
                    alert(data)
                    return false;
                }
                //alert(typeof (data)); //it comes out to be string 
                //we need to parse it to JSON 
                data = $.parseJSON(data);
                document.getElementById('<%=lblItemValue.ClientID %>').textContent = data[0].item
                document.getElementById('<%=lblItemValue1.ClientID %>').value = data[0].item

                document.getElementById('<%=lbllocationValue.ClientID %>').textContent = data[0].current_location;
                document.getElementById('<%=lbllocationValue1.ClientID %>').value = data[0].current_location;

                document.getElementById('<%=lblqtyValue.ClientID %>').textContent = data[0].current_qty;
                document.getElementById('<%=lblqtyValue1.ClientID %>').value = data[0].current_qty;

                document.getElementById('<%=lblItemValue.ClientID %>').textContent = data[0].description;
                document.getElementById('<%=lblItemValue1.ClientID %>').value = data[0].description;

                document.getElementById('<%=lblUnitValue.ClientID %>').textContent = data[0].unit;
                document.getElementById('<%=lblUnitValue1.ClientID %>').value = data[0].unit;

                document.getElementById('<%=lbllotValue.ClientID %>').textContent = data[0].lot;
                document.getElementById('<%=lbllotValue1.ClientID %>').value = data[0].lot;


                $("#btnEnviar").show();
                //                alert(data.subject);
                //                alert(data.description);
            } else {
                alert("Pallet Does not Exists!!");
            }




        }
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True" EnableCdn="true" EnablePageMethods="true" EnablePartialRendering="true">
    </asp:ScriptManager>
    <table border="0">
        <tr>
            <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblreturnorder" runat="server" /></strong>
            </td>
            <td style="width: 250px; padding: 5px;">
                <asp:TextBox ID="txtReturnOrder" class="form-control form-control-lg" runat="server"
                    CausesValidation="True" MaxLength="9" CssClass="TextBoxBig" TabIndex="1" ToolTip="Enter Return Order number"></asp:TextBox>
                <span>
                    <asp:RegularExpressionValidator ID="minlenght" runat="server" ControlToValidate="txtReturnOrder"
                        ErrorMessage="Remember 9 characters" ValidationExpression="^[a-zA-Z0-9'@&#.\s]{9,9}$"
                        CssClass="errorMsg" SetFocusOnError="True" Display="Dynamic" />
                </span>
            </td>
         
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <hr />
                <asp:Button ID="btnSend" runat="server" Text="Query"  CssClass="ButtonsSendSave" onClick="btnSend_Click"/>
            </td>

        </tr>
    </table>
     <div class="style3" style="width: 80%">
        <div id="HeaderGrid" style="width: 80%; height: 40px; margin-left: 40px;margin-top:10px;" runat="server">
            <span style="text-align: right;">
            <input type="button" class="btn btn-primary btn-lg" id="btnEnviar" value="Confirm" />
            <%-- <asp:Button class="btn btn-primary btn-lg" runat="server" ID="btnSalir" OnClick="btnExit_Click" style="margin-left:5px;"
                    AutoPostBack="true" />--%>
            </span>
        </div>
        <br />
        </div>
        <br />
       <div class="col-sm-6">
       <asp:Table ID="tblReturnInfo" runat="server" Height="123px" Width="567px" Visible= false >
       <asp:TableRow ID="TableRow1" runat="server">
            <asp:TableCell ID="TableCell1" runat="server" style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                 <strong>
                    <asp:Label class="" ID="lblreturnordergrid" Text="Return Order" runat="server" style="font-size:13px;" /></strong>
            </asp:TableCell>
            <asp:TableCell ID="TableCell2" runat="server">
                <strong>
                    <asp:Label class="" ID="lblreturnorderValue" runat="server" /></strong>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow2" runat="server">
            <asp:TableCell ID="TableCell3" runat="server" style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                 <strong>
                    <asp:Label class="" ID="lblSupplier" Text="Supplier" runat="server" style="font-size:13px;" />
                         <asp:HiddenField ID="lblstun" runat="server"  />
                    
                    </strong>
            </asp:TableCell>
            <asp:TableCell ID="TableCell4" runat="server">
                <strong>
                    <asp:Label class="" ID="lblSupplierValue" runat="server" /></strong>
            </asp:TableCell>
        </asp:TableRow>
           <asp:TableRow ID="TableRow3" runat="server">
            <asp:TableCell ID="TableCell5" runat="server" style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                 <strong>
                    <asp:Label class="" ID="lblposiiton" Text="Position" runat="server" style="font-size:13px;" /></strong>
            </asp:TableCell>
            <asp:TableCell ID="TableCell6" runat="server">
                <strong>
                 <asp:DropDownList runat="server" ID="dropDownPosition" CssClass="TextBoxBig"  style="width: 80%" ></asp:DropDownList>
                    

                      
                    </strong>
            </asp:TableCell>
        </asp:TableRow>
           <asp:TableRow ID="TableRow4" runat="server">
            <asp:TableCell ID="TableCell7" runat="server" style="text-align: left; padding: 5px 5px 5px; width: 200px;">
                 <strong>
                    <asp:Label class="" ID="lblpallet" Text="Pallet" runat="server" style="font-size:13px;" /></strong>
            </asp:TableCell>
            <asp:TableCell ID="TableCell18" runat="server">
                <strong>
                  <asp:TextBox ID="txtPalletIDValue" class="form-control form-control-lg" runat="server" style="width: 80%;margin-top:5px;"
                    CausesValidation="True" MaxLength="20" CssClass="TextBoxBig" TabIndex="1" ToolTip="Enter pallet Id" onChange="validaPaid(this.value);"></asp:TextBox>

                   
                   </strong>
            </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow ID="TableRow5" runat="server">
            <asp:TableCell ID="TableCell9" runat="server" style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                 <strong>
                    <asp:Label class="" ID="lblItem" Text="Item" runat="server" style="font-size:13px;" /></strong>
            </asp:TableCell>
            <asp:TableCell ID="TableCel20" runat="server">
                <strong>
                  <asp:Label class="" ID="lblItemValue"  runat="server" style="padding-left:15px;"/>
                  <asp:HiddenField ID="lblItemValue1" runat="server"  />

                     <asp:Label class="" ID="lblItemDescValue"  runat="server" style="padding-left:15px;"/>
                      <asp:HiddenField ID="lblItemDescValue1" runat="server"  />

                    </strong>
            </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow ID="TableRow6" runat="server">
            <asp:TableCell ID="TableCell8" runat="server" style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                 <strong>
                    <asp:Label class="" ID="lbllot" Text="Lot" runat="server" style="font-size:13px;" /></strong>
            </asp:TableCell>
            <asp:TableCell ID="TableCell10" runat="server">
                <strong>
                    <asp:Label class="" ID="lbllotValue" runat="server" />
                     <asp:HiddenField ID="lbllotValue1" runat="server"  />
                    </strong>
            </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow ID="TableRow7" runat="server">
            <asp:TableCell ID="TableCell11" runat="server" style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                 <strong>
                    <asp:Label class="" ID="lbllocation" Text="Location" runat="server" style="font-size:13px;" /></strong>
            </asp:TableCell>
            <asp:TableCell ID="TableCell12" runat="server">
                <strong>
                    <asp:Label class="" ID="lbllocationValue"  runat="server" />
                      <asp:HiddenField ID="lbllocationValue1" runat="server"  />
                    </strong>
            </asp:TableCell>
        </asp:TableRow>
         <asp:TableRow ID="TableRow8" runat="server">
            <asp:TableCell ID="TableCell13" runat="server" style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                 <strong>
                    <asp:Label class="" ID="lblqty" Text="Quantity" runat="server" style="font-size:13px;" /></strong>
            </asp:TableCell>
            <asp:TableCell ID="TableCell14" runat="server">
                <strong>
                    <asp:Label class="" ID="lblqtyValue"  runat="server" />
                     <asp:HiddenField ID="lblqtyValue1" runat="server"  />

                     <asp:Label class="" ID="lblUnitValue"  runat="server" style="padding-left:15px;"/>
                       <asp:HiddenField ID="lblUnitValue1" runat="server"  />

                    </strong>
            </asp:TableCell>
        </asp:TableRow>
        </asp:Table>
        </div>
          <div class="col-sm-6">
            <asp:GridView ID="grdRecords" runat="server" AutoGenerateColumns="False"
                    SkinID="Default">
                    <Columns>
                        <asp:BoundField HeaderText="Order Number" DataField="ret_order">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Position" DataField="position">
                            <ItemStyle HorizontalAlign="Left" Width="15%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField HeaderText="Item Code" DataField="item">
                            <ItemStyle HorizontalAlign="Left" Width="15%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField HeaderText="Description" DataField="description">
                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField HeaderText="PI.Re Date" DataField="date_pl_receive">
                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField HeaderText="Expected" DataField="qstr">
                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                           <asp:BoundField HeaderText="Unit" DataField="unit">
                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                          <asp:BoundField HeaderText="Warehouse" DataField="warehouse">
                            <ItemStyle HorizontalAlign="Left" Width="10%" />
                            <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                        </asp:BoundField>
                     </Columns>

                </asp:GridView>

          </div>

        <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-weight:bold; font-size:15px;" />    
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-weight:bold; font-size:15px;" />    
    <div id="MyEtiqueta">
        <table style="border:1px solid;">
           <tr>
                <td colspan="2">
                    <center>
                        <label><b>
                            PURCHASE ORDER RETURN</b></label>
                        <label  id="LblPurchaseOC">
                        </label>
                    </center>
                </td>
            </tr>
            <tr>
                <td style="border-right:1px solid;border-top:1px solid;">
                <label><b> ORDER </b></label><br />
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBPurchaseOrder" alt=""
                        hspace="60" vspace="5" style="width: 2in; height: .5in;" />
                </td>
                <td  style="border-top:1px solid;"><label><b> ITEM </b></label><br />
                    <img src="~/images/logophoenix_login.jpg" runat="server" id="CBItem" alt=""
                        hspace="60" vspace="5" style="width: 2in; height: .5in;" /><br />
                         <label  id="LblItemOC">
                        </label>
                </td>
            </tr>
         <tr>
                <td colspan="2" style="border-right:1px solid;border-top:1px solid;">
                    <center>
                        <label><b>
                            QUANTITY</b>
                            <img src="~/images/logophoenix_login.jpg" runat="server" id="CBQuantity" alt="" hspace="60"
                        vspace="5" style="width: 1in; height: .5in;" /><br />
                            <label id="LblQuantityOC" font-size: 11px">
                    </label>
                   
                        <label style="display: none" id="LblUnitOC">
                        </label>
                    </center>
                </td>
            </tr>
            
            <tr>
                <td style="border-right:1px solid;border-top:1px solid;">
                    <center>
                        <label>
                            <b>USER</b></label><br />
                        <label id="Lbluser">
                        </label>
                    </center>
                </td>
                <td style="border-right:1px solid;border-top:1px solid;">
                    <center>
                        <label>
                           <b>DATE</b> </label><br />
                        <label  id="Lbldate">
                        </label>
                    </center>
                </td>
            </tr>
          
            
           
        </table>
    </div>

 
 
    <script type="text/javascript">
        function sendAjax(WebMethod, Data, FuncitionSucces, asyncMode) {
            var options = {
                type: "POST",
                url: "whInvMaterialPoOrderReturn.aspx/" + WebMethod,
                data: Data,
                contentType: "application/json; charset=utf-8",
                async: asyncMode != undefined ? asyncMode : true,
                dataType: "json",
                success: FuncitionSucces
            };
            $.ajax(options);

            WebMethod = "";
        }


        $('#btnEnviar').click(function () {

            insertData();


        });
    </script>

    </asp:Content>