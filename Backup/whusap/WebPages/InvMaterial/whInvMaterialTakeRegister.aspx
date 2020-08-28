<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvMaterialTakeRegister.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvMaterialTakeRegister" Theme="Cuadriculas" %>
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
            width: 93%;
            height: 149px;
        }
        .style8
        {
            border-left: 0.5px solid #405579;
            border-right: 1px solid #405579;
            border-top: 1px solid #405579;
            border-bottom: 1px solid #405579;
            background-color: #CFD8FA;
            float: left;
            height: 18px;
            overflow: hidden;
            padding: 3px 1.8%;
            width: 14%;
        }
        .style9
        {
            border-left: 0.5px solid #F0F0F0;
            border-right: 1px solid #F0F0F0;
            border-top: 1px solid #F0F0F0;
            border-bottom: 1px solid #F0F0F0;
            float: left;
            overflow: hidden;
            padding: 1.3px 1.8% 3.2px 1%;
            width: 25%;
            height: 18px;            
            vertical-align: middle;
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
              var parametrosEnviar = "{ 'valor': '" + args + "', 'tipo':'" + val + "'}";

              $.ajax({
                  type: "POST",
                  url: "whInvMaterialTake.aspx/validaInfo",
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

                              var objControl = document.getElementById('<%= txtLote.ClientID %>');
                              objControl.disabled = false;

                              document.getElementById('<%=txtQuantity.ClientID %>').focus();
                              if (tValue != "1" && tValue != undefined) {
                                  objControl.disabled = true;
                                  setTimeout(function () { objControl.focus() }, 10);
                                  objControl.focus();
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

    <div align="center" id="divZona" runat="server" 
        style="width:100%; height: 40px; vertical-align: top;" >      
        <div class="rTable" style="width:100%; height: 40px; ">
        <div class="rTableBody" style="width:100%; height: 30px;">
        <div class="rTableCell" style="background-color: #CFD8FA; width:10%; height: 30x; "><span class="style2" 
                style="vertical-align: middle; width:50%; height: 25px;"><b>Enter Zone Code</b></span> </div>
        <div class="rTableCell" style="Width:40px; height: 30px;" align="left">
            <asp:TextBox ID="txtZone" runat="server" CausesValidation="True" MaxLength="15" 
                            Width="40px" CssClass="TextBox" TabIndex="1" 
                ToolTip="Enter Zone code" Height="20px"></asp:TextBox> </div>
        <div class="rTableCell" style="height: 30px; "><asp:Button ID="btnSearchZone" runat="server" Text="Search Zone" CssClass="ButtonsSendSave" 
                         Width="82px" Height="20px" onclick="btnSearchZone_Click" TabIndex="6" /> </div>
        <div class="rTableCell" style="width:20%; height: 30px;" align="left"> <asp:Label ID="lblZone" runat="server" CssClass="style2"></asp:Label><b style="font-size:11px" /></div>
        </div>
        </div>

    </div>
    <span style="vertical-align:middle" />
    <div style="height:15px" align="center"></div>
    
    <div align="center" id="divTabla" runat="server" style="width:82%" 
    Visible="false">
        <table class="style16">
            <tr>
                <td class="style8">
                <span class="style2" style="vertical-align: middle; width:123%"><b style="font-size:11px">Label Id  </b></span>
                </td>
                <td class="style9">
                <span style="vertical-align:middle" >
             <asp:TextBox ID="txtLabelId" runat="server" CausesValidation="True" MaxLength="15" 
                    Width="120px" CssClass="TextBox" TabIndex="1" 
                    ToolTip="Enter warehouse code"></asp:TextBox>
                    
             &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="ButtonsSendSave" 
                         Width="52px" Height="20px" onclick="btnSearch_Click" TabIndex="6" /> 
    

           </span>
                </td>
                <td class="rTableCellError" height="90%">
                <asp:Label ID="Label1" runat="server" CssClass="style2"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style8">
                <span class="style2" style="vertical-align: middle; width:3%"><b style="font-size:11px">WareHouse  </b></span>
                </td>
                <td class="style9">
                <span style="vertical-align:middle" >
             <asp:TextBox ID="txtCwar" runat="server" CausesValidation="True" MaxLength="9" 
                    Width="84px" CssClass="TextBox" TabIndex="2" 
                    ToolTip="Enter warehouse code" Enabled="False"></asp:TextBox>
           </span>
                </td>
                <td class="rTableCellError" height="90%">
                <asp:Label ID="lblCwar" runat="server" CssClass="style2"></asp:Label>
                </td>
            </tr>           
            <tr>
                <td class="style8">
                <span class="style2" style="vertical-align: middle; width:3%"><b style="font-size:11px">Item  </b></span>
                </td>
                <td class="style9">
                <span style="vertical-align:middle;" >
              <asp:TextBox ID="txtItem" runat="server" CausesValidation="True" MaxLength="18" 
                           Width="145px" CssClass="TextBox" TabIndex="3" 
                           ToolTip="Enter Item Code" Enabled="False"></asp:TextBox>
           </span>
                </td>
                <td class="rTableCellError" height="90%">
                <span style="vertical-align:middle" >
             <asp:Label ID="lblItem" runat="server" CssClass="style2"></asp:Label>
             </span>
                </td>
            </tr>
            <tr>
                <td class="style8">
           <span class="style15" style="vertical-align: middle; width:134%"><b style="font-size:11px">Lot Code  </b></span>
                </td>
                <td class="style9">
           <span style="vertical-align:middle;" >
              <asp:TextBox ID="txtLote" runat="server" CausesValidation="True" MaxLength="16" 
                           Width="145px" CssClass="TextBox" TabIndex="4" 
                           ToolTip="Enter Lot Code" Enabled="False"></asp:TextBox>
           </span>
                </td>
                <td class="rTableCellError" height="90%">
           <span style="vertical-align:middle" />
             <asp:Label ID="lblLotCode" runat="server" CssClass="style2"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style8">
           <span class="style2" style="vertical-align: middle;"><b style="font-size:11px">Quantity  </b></span>
                </td>
                <td class="style9">
           <span style="vertical-align:middle;">
              <asp:TextBox ID="txtQuantity" runat="server" CausesValidation="True" MaxLength="15" 
                           Width="70px" CssClass="TextBox" TabIndex="5" 
                           ToolTip="Current Quantity"></asp:TextBox>
           </span>
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
    <div style="padding: 1%; height:20px; vertical-align:middle; width:50%; text-align: center;" 
        align="center">
        <asp:Label ID="lblError" runat="server" Text="" CssClass="style2"></asp:Label>
    </div>
    <div style="padding: 1%; height:35px; vertical-align:middle; width:50%; text-align: center;" 
        align="center">
        <asp:Button ID="btnSend" runat="server" Text="Save" CssClass="ButtonsSendSave" 
             Width="107px" Height="24px" onclick="btnSend_Click" TabIndex="6" /> 
    </div>

</div>
    
    <asp:HiddenField ID="hidden" runat="server" />
    <asp:HiddenField ID="hi_indLote" runat="server" />
    <asp:HiddenField ID="hi_descItem" runat="server" />
    <asp:HiddenField ID="hi_unityItem" runat="server" />
</asp:Content>
