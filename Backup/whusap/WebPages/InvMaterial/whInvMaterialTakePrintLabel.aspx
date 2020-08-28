<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvMaterialTakePrintLabel.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvMaterialTakePrintLabel" Theme="Cuadriculas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <style type="text/css">
        .style15
        {
            color: Black;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-style: normal;
            text-align: Left;
            height: 10px;
            width: 116%;
        }
        .style16
        {
            width: 60%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True" EnablePageMethods="true">
    </asp:ScriptManager>

      <script type="text/javascript">
         function validaInfo(args, val, objmsg, obj) {
             if (args == "") {
                 return;
             }
             var parametrosEnviar = "{ 'valor': '" + args + "', 'tipo':'" +val + "'}";

             $.ajax({
                 type: "POST",
                 url: "whInvMaterialTakePrintLabel.aspx/validaInfo",
                 data: parametrosEnviar,
                 contentType: "application/json; charset=utf-8",
                 dataType: "json",
                 success: function (msg) {
                     if (msg.d != "") {
                         if (msg.d.indexOf("BAAN") > -1) {
                             var objControl = document.getElementById(obj.id);
                             objControl.value = "";
                             objControl.focus();
                             alert(msg.d);
                             return false;
                         }
                         var objmess = document.getElementById(objmsg);
                         objmess.innerHTML = msg.d.trim(); ;

                         if (val == "2") {
                             var values = msg.d.split('|');

                             var fValue = values[0];
                             var sValue = values[1];
                             objmess.innerHTML = fValue;
                             document.getElementById('<%=hidden.ClientID %>').value = sValue;
                         }
                         if (val == "3") {
                             var values = msg.d.split('|');

                             var fValue = values[0];
                             var sValue = values[1];
                             var tValue = values[2];

                             var objmess = document.getElementById(objmsg);
                             objmess.innerHTML = fValue;

                             var objmess = document.getElementById('<%=lblUnity.ClientID %>');
                             objmess.innerHTML = sValue.trim();

                             document.getElementById('<%=hi_descItem.ClientID %>').value = fValue.trim();
                             document.getElementById('<%=hi_unityItem.ClientID %>').value = sValue.trim();
                             document.getElementById('<%=hi_indLote.ClientID %>').value = tValue.trim();
                             //alert(tValue);
                             var objControl = document.getElementById('<%= txtLote.ClientID %>');
                             objControl.disabled = false;
                             objControl.focus();
                             if (tValue != "1" && tValue != undefined) {
                                 objControl.disabled = true;
                                 document.getElementById('<%=txtQuantity.ClientID %>').focus();
                                 setTimeout(function () { objControl.focus() }, 2);

                                 return;
                             }
                             return;
                         }
                     }
                 },
                 error: function (msg) {
                     var objControl = document.getElementById(obj.id);
                     objControl.value = "";
                     objControl.focus();
                     alert(msg.d);
                     return false;
                 }
             });
       }
      
       $.fn.exists = function () {
           return this.length !== 0;
       }
     
       function printTag() {
           if ($("#Contenido_printerDiv").exists()) {
               $("#Contenido_printerDiv").width(400);
               $("#Contenido_printerDiv").height(400);
               $("#Contenido_printerDiv").html("<iframe src='../Labels/whInvPrintLabel.aspx' width='100%'; height='100%';></iframe>"); //onload='this.contentWindow.print();
               $("#Contenido_printerDiv").html(""); //onload='this.contentWindow.print();
      } 
  }
</script>
 
    <div align="left" style="width:60%; vertical-align:text-top;">


        <table class="style16">
            <tr id="trWareHouse" runat="server" visible="false">
                <td class="rTableCellHead" style="height:25px">
                <span style="vertical-align: middle; width:3%"><b style="font-size:11px">WareHouse  </b></span>
                </td>
                <td class="rTableCellLeft" style="width: 30%; height:18px;">
                  <asp:TextBox ID="txtCwar" runat="server" CausesValidation="True" MaxLength="9" 
                    Width="84px" CssClass="TextBox" TabIndex="1" 
                    ToolTip="Enter warehouse code"></asp:TextBox>
                </td>
                <td class="rTableCellError" height="90%">
                <asp:Label ID="lblCwar" runat="server" CssClass="style2"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="rTableCellHead" style="height:3%">
                <span class="style2" style="vertical-align: middle; width:3%"><b style="font-size:11px">Zone  </b></span>
                </td>
                <td class="rTableCellLeft" style="width: 30%; height:18px;">
                  <asp:TextBox ID="txtZone" runat="server" CausesValidation="True" MaxLength="9" 
                               Width="84px" CssClass="TextBox" TabIndex="2" 
                               ToolTip="Enter Zone Code"></asp:TextBox>
                </td>
                <td class="rTableCellError" height="90%">
                  <span style="vertical-align:middle" >
                    <asp:Label ID="LblZone" runat="server" CssClass="style2"></asp:Label>
                  </span>
                </td>
            </tr>
            <tr>
                <td class="rTableCellHead" style="height:3%">
                  <span class="style2" style="vertical-align: middle; width:3%"><b style="font-size:11px">Item  </b></span>
                </td>
                <td class="rTableCellLeft" style="width: 30%; height:18px;">
                  <asp:TextBox ID="txtItem" runat="server" CausesValidation="True" MaxLength="18" 
                               Width="124px" CssClass="TextBox" TabIndex="3" 
                               ToolTip="Enter Item Code"></asp:TextBox>
                </td>
                <td class="rTableCellError" height="90%">
                  <span style="vertical-align:middle" >
                    <asp:Label ID="lblItem" runat="server" CssClass="style2"></asp:Label>
                  </span>
                </td>
            </tr>
            <tr>
                <td class="rTableCellHead">
                  <span class="style2" style="vertical-align: middle; width:3%"><b style="font-size:11px">Lot Code  </b></span>
                </td>
                <td class="rTableCellLeft" style="width: 30%; height:18px;">
                   <asp:TextBox ID="txtLote" runat="server" CausesValidation="True" MaxLength="16" 
                                Width="124px" CssClass="TextBox" TabIndex="4" 
                                ToolTip="Enter Lot Code"></asp:TextBox>
                </td>
                <td class="rTableCellError" height="90%">
                  <span style="vertical-align:middle" />
                    <asp:Label ID="lblLotCode" runat="server" CssClass="style2"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="rTableCellHead">
                  <span style="vertical-align:middle" />
                  <span class="style2" style="vertical-align: middle; width:3%"><b style="font-size:11px">Quantity  </b></span>
                </td>
                <td class="rTableCellLeft" style="width: 30%; height:18px;">
                    <asp:TextBox ID="txtQuantity" runat="server" CausesValidation="True" MaxLength="15" 
                             Width="84px" CssClass="TextBox" TabIndex="5" 
                             ToolTip="Current Quantity"></asp:TextBox>
                </td>
                <td class="rTableCellError" height="90%">
                <span style="vertical-align:middle" />
                  <asp:Label ID="lblUnity" runat="server" CssClass="style2"></asp:Label>
                   <asp:RegularExpressionValidator ID="validateReturn" runat="server" 
                        ControlToValidate="txtQuantity" ErrorMessage="Only numbers allowed" SetFocusOnError="true" 
                        ValidationExpression="[0-9]+(\.[0-9]{1,4})?" Display="Dynamic" 
                        ForeColor="Red" Font-Names="Arial" Font-Size="9" Font-Italic="True"></asp:RegularExpressionValidator>
                
                </td>
            </tr>
        </table>




</div>
    
    
    <div style="padding: 1%; height:20px; vertical-align:middle; width:50%; text-align: center;" 
        align="center">
        <asp:Label ID="lblError" runat="server" Text="" CssClass="style2"></asp:Label>
    </div>
    <div style="padding: 1%; height:35px; vertical-align:middle; width:50%; text-align: center;" 
        align="center">
        <asp:Button ID="btnSend" runat="server" Text="Print" CssClass="ButtonsSendSave" 
             Width="107px" Height="24px" onclick="btnSend_Click" TabIndex="6" /> 
    </div>
    <asp:HiddenField ID="hidden" runat="server" />
    <asp:HiddenField ID="hi_indLote" runat="server" />
    <asp:HiddenField ID="hi_descItem" runat="server" />
    <asp:HiddenField ID="hi_unityItem" runat="server" />

</asp:Content>
