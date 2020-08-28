<%@ Page Title="" Language="C#" MasterPageFile="../../MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="InventoryAdjustment.aspx.cs" Inherits="whusap.WebPages.Inventarios.InventoryAdjustment"
    EnableViewStateMac="false" Theme="Cuadriculas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="https://code.jquery.com/jquery-3.3.1.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
     <script type="text/javascript">

         function validaPaid(val) {
             //var parametrosEnviar = "{ 'valor':'" + args.value + "', 'quantityToReturn': '" + quantityToReturn + "'}";
            // var quantityToReturn = $("#Contenido_grdRecords_toReturn_" + rowIndex).val();
             var parametrosEnviar = "{ 'palletID': '" + val + "'}";
             //alert(" pId " + args.value + " toReturn " + quantityToReturn);
             var objSend = document.getElementById('<%=btnSend.ClientID %>');
             objSend.disabled = true;
             $.ajax({
                 type: "POST",
                 url: "InventoryAdjustment.aspx/vallidatePalletID",
                 data: parametrosEnviar,
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (msg) {
                     if (msg.d != "") {
                         var objControl = document.getElementById('<%=txtPalletId.ClientID %>');
                         objControl.value = "";
                         alert(msg.d);
                         return false;
                     } else { objSend.disabled = false; }
                 },
                 error: function (msg) {
                     alert("This is msg " + msg.d);
                     return false;
                 }
             });
         }
         function validarAdjustQty(qty) {
             if (qty <= 0) {

                 alert("Adjustment quantity: cannot be zero (0)");
                // $("#txtAdjustmentQuantity").val('');
                 $("#txtAdjustmentQuantity").focus();
                 return false;
             }
             qtyExting = $("#Contenido_lblQuantityValue").text();
             if (parseInt(qty) > parseInt(qtyExting)) {

                 alert("Adjustment quantity shuould be less than existing Qty");
                 // $("#txtAdjustmentQuantity").val('');
                 $("#txtAdjustmentQuantity").focus();
                 return false;
             }

         }
      </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True" EnablePageMethods="true">
    </asp:ScriptManager>
    <br />

    <table border="0">
       
      <tr>
            <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblPalletId" Text="Pallet ID" runat="server" /></strong>
            </td>
            <td style="width: 250px; padding: 5px;">
                <asp:TextBox ID="txtPalletId" class="form-control form-control-lg" runat="server"
                    CausesValidation="True" MaxLength="20" CssClass="TextBoxBig" TabIndex="1" ToolTip="Enter pallet Id" onChange="validaPaid(this.value);"></asp:TextBox>
            </td>
            <td style="text-align: left;">
                <span>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPalletId"
                        ErrorMessage="Pallet Id lenght between 12 and 13 characteres." ValidationExpression="^[a-zA-Z0-9'@&#-.\/\s]{12,20}$"
                        CssClass="errorMsg" SetFocusOnError="True" Display="Dynamic" />
                </span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Pallet Id is required"
                    ControlToValidate="txtPalletId" SetFocusOnError="False" CssClass="errorMsg"
                    Enabled="false"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Pallet Id doesn't exist"
                    SetFocusOnError="True" CssClass="errorMsg"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <hr />
                <asp:Button ID="btnSend" runat="server" Text="Query" OnClick="btnSend_Click" CssClass="ButtonsSendSave" />
            </td>
        </tr>
    </table>

    <div class="style3" >
    <br />

    
    <asp:Table ID="tblPalletInfo" runat="server" Height="123px" Width="567px">
        <asp:TableRow ID="TableRow1" runat="server">
            <asp:TableCell ID="TableCell1" runat="server" style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                 <strong>
                    <asp:Label class="" ID="lblPalletId1" Text="Pallet ID" runat="server" style="font-size:13px;" /></strong>
            </asp:TableCell>
            <asp:TableCell ID="TableCell2" runat="server">
                <strong>
                    <asp:Label class="" ID="lblPalletId1Value" Text="Item" runat="server" /></strong>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow2" runat="server">
            <asp:TableCell ID="TableCell3" runat="server" style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                 <strong>
                    <asp:Label class="" ID="lblItem" Text="Item" runat="server" style="font-size:13px;" /></strong>
            </asp:TableCell>
            <asp:TableCell ID="TableCell4" runat="server">
                <strong>
                    <asp:Label class="" ID="lblItemValue" Text="Lot" runat="server" />
                     <asp:Label class="" ID="lblItemDescValue" Text="Item Desc" runat="server" style="padding-left:15px;"/>
                    </strong>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow3" runat="server">
            <asp:TableCell ID="TableCell5" runat="server" style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                 <strong>
                    <asp:Label class="" ID="lblWarehouse" Text="Warehouse" runat="server" style="font-size:13px;" /></strong>
            </asp:TableCell>
            <asp:TableCell ID="TableCell6" runat="server">
                <strong>
                    <asp:Label class="" ID="lblWarehouseValue" Text="warehouse" runat="server" />
                     <asp:Label class="" ID="lblWarehouseDescValue" Text="warehouse Description" runat="server" style="padding-left:15px;" />
                    </strong>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow4" runat="server">
            <asp:TableCell ID="TableCell7" runat="server" style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                 <strong>
                    <asp:Label class="" ID="lblLot" Text="Lot" runat="server" /></strong>
            </asp:TableCell>
            <asp:TableCell ID="TableCell8" runat="server">
                <strong>
                    <asp:Label class="" ID="lblLotValue" Text="Lot" runat="server" style="font-size:13px;" /></strong>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow5" runat="server">
            <asp:TableCell ID="TableCell9" runat="server" style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                 <strong>
                    <asp:Label class="" ID="lblLocation" Text="Location" runat="server" style="font-size:13px;" /></strong>
            </asp:TableCell>
            <asp:TableCell ID="TableCell10" runat="server">
                <strong>
                    <asp:Label class="" ID="lblLocationValue" Text="Location" runat="server" /></strong>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow6" runat="server">
            <asp:TableCell ID="TableCell11" runat="server" style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                 <strong>
                    <asp:Label class="" ID="lblQuantity" Text="Quantity" runat="server" style="font-size:13px;"  /></strong>
            </asp:TableCell>
            <asp:TableCell ID="TableCell12" runat="server">
                <strong>
                    <asp:Label class="" ID="lblQuantityValue" Text="Qty" runat="server" />
                     <asp:Label class="" ID="lblUnitValue" Text="Unit" runat="server" style="padding-left:15px;" />
                    </strong>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow7" runat="server">
            <asp:TableCell ID="TableCell13" runat="server" style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                 <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                <asp:Label runat="server" ID="lblAdjustmentQuantity" text = "Adjustment Quantity" style="font-size:13px;"  /></b></span>
            </asp:TableCell>
            <asp:TableCell ID="TableCell14" runat="server">
               <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtAdjustmentQuantity" CssClass="TextBoxBig" onChange="validarAdjustQty(this.value);" ClientIDMode="Static"  style="width: 80%" />
                     <asp:Label class="" ID="lblUnitValue1" Text="Unit" runat="server" style="padding-left:15px;" />
                     </span>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow8" runat="server">
            <asp:TableCell ID="TableCell15" runat="server" style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                 <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                <asp:Label runat="server" ID="lblReasonCode" text="Reason Code" style="font-size:13px;" /></b></span>
            </asp:TableCell>
            <asp:TableCell ID="TableCell16" runat="server" style="padding-top:5px;">
               <span style="vertical-align: middle;">
                    <asp:DropDownList runat="server" ID="dropDownReasonCodes" CssClass="TextBoxBig"></asp:DropDownList>
                </span>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow9" runat="server" style="padding-top:15px;">
            <asp:TableCell ID="TableCell17" runat="server" style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                 <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                <asp:Label runat="server" ID="lblCostCenter" text="Cost Center" style="font-size:13px;"  /></b></span>
            </asp:TableCell>
            <asp:TableCell ID="TableCell18" runat="server" style="padding-top:5px;">
               <span style="vertical-align: middle;">
                    <asp:DropDownList runat="server" ID="dropDownCostCenters" CssClass="TextBoxBig"></asp:DropDownList>
                </span>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow ID="TableRow10" runat="server">
            <asp:TableCell ID="TableCell19" runat="server" style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                 <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="ButtonsSendSave" />
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>


    <table border="0">
       
      <%--<tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <strong>
                    <asp:Label class="" ID="lblPalletId1" Text="Pallet ID" runat="server" /></strong>
            </td>
             <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblPalletId1Value" Text="Item" runat="server" /></strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <strong>
                    <asp:Label class="" ID="lblItem" Text="Item" runat="server" /></strong>
            </td>
             <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblItemValue" Text="Lot" runat="server" />
                     <asp:Label class="" ID="lblItemDescValue" Text="Item Desc" runat="server" />
                    </strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <strong>
                    <asp:Label class="" ID="lblWarehouse" Text="Warehouse" runat="server" /></strong>
            </td>
             <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblWarehouseValue" Text="warehouse" runat="server" />
                     <asp:Label class="" ID="lblWarehouseDescValue" Text="warehouse Description" runat="server" />
                    </strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <strong>
                    <asp:Label class="" ID="lblLot" Text="Lot" runat="server" /></strong>
            </td>
             <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblLotValue" Text="Pallet ID" runat="server" /></strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <strong>
                    <asp:Label class="" ID="lblLocation" Text="Location" runat="server" /></strong>
            </td>
             <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblLocationValue" Text="Pallet ID" runat="server" /></strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <strong>
                    <asp:Label class="" ID="lblQuantity" Text="Quantity" runat="server" /></strong>
            </td>
             <td style="text-align: left;">
                <strong>
                    <asp:Label class="" ID="lblQuantityValue" Text="Qty" runat="server" />
                     <asp:Label class="" ID="lblUnitValue" Text="Unit" runat="server" />
                    </strong>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                <asp:Label runat="server" ID="lblAdjustmentQuantity" text = "Adjustment Quantity" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtAdjustmentQuantity" CssClass="TextBoxBig" onblur="validarAdjustQty(this.value);" ClientIDMode="Static" />
                     <asp:Label class="" ID="Label1" Text="Unit" runat="server" />
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                <asp:Label runat="server" ID="lblReasonCode" text="Reason Code"/></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:DropDownList runat="server" ID="dropDownReasonCodes" CssClass="TextBoxBig"></asp:DropDownList>
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align: left; padding: 5px 0px 5px; width: 200px;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                <asp:Label runat="server" ID="lblCostCenter" text="Cost Center" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:DropDownList runat="server" ID="dropDownCostCenters" CssClass="TextBoxBig"></asp:DropDownList>
                </span>
            </td>
        </tr>--%>
        <%--<tr>
            <td colspan=2>
              <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="ButtonsSendSave" />
            </td>
        </tr>--%>
    </table>
        
    </div>
     <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
   
</asp:Content>
