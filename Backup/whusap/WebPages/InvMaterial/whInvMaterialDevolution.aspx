<%@ Page Title="" Language="C#" MasterPageFile="../../MDMasterPage.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="whInvMaterialDevolution.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvMaterialDevolution" Theme="Cuadriculas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <style type="text/css">
        .style3
        {
            width: 768px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">


   <script language="javascript" src="../../Scripts/script.js" type="text/javascript"></script> 
   <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True" EnableCdn="true" EnablePageMethods="true" EnablePartialRendering="true"></asp:ScriptManager>

<script type="text/javascript">
    window.onload = function () {
        var e = document.getElementById("printerDiv");
    };

    function limpiar(obj) {
                var objControl = document.getElementById(obj.id);
               // objControl.value = "";
                objControl.focus();
                return;
    }

    function validaLot(args, val, obj) {
        if (args.value == "") {
            return;
        }
        var parametrosEnviar = "{ 'Fila': '" + JSON.stringify(val) + "', 'valor':'" + obj + "'}";
        var objSend = document.getElementById('<%=btnSave.ClientID %>');
        objSend.disabled = true;
        $.ajax({
            type: "POST",
            url: "whInvMaterialDevolution.aspx/validaExistLot",
            data: parametrosEnviar,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d != "") {
                    var objControl = document.getElementById(args.id);
                    objControl.value = "";
                    objControl.focus();
                    alert(msg.d);
                    return false;
                } else { objSend.disabled = false; }
            },
            error: function (msg) {
                var objControl = document.getElementById(args.id);
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

  function validarCantidadMaxima(args, val, opcion) {
      if (args.value === "" || val.value === "") {
          return;
      }

      var rowSelected = "";
      var valor = "";
      var lote = "";

      if (opcion === 0) {
          rowSelected = document.getElementById(args.id);
          valor = rowSelected.value; //"1000";
          lote = document.getElementById(args.id.replace("toReturn", "toLot")).value;
      }
      else if (opcion === 1) {
          rowSelected = document.getElementById(args.id);
          valor = document.getElementById(args.id.replace("toLot", "toReturn")).value; //"1000";
          lote = rowSelected.value;
      }
      else
          return;
      var parametrosEnviar = "{ 'fila': '" + JSON.stringify(val) + "', 'valor':'" + valor + "', 'lote':'" + lote + "'}";
      var objSend = document.getElementById('<%=btnSave.ClientID %>');
      objSend.disabled = true;
      $.ajax({
          type: "POST",
          url: "whInvMaterialDevolution.aspx/validarCantidades",
          data: parametrosEnviar,
          contentType: "application/json; charset=utf-8",
          dataType: "json",
          success: function (msg) {
              if (msg.d != "") {
                  var objControl = document.getElementById(args.id);

                  if (objControl.type === "select-one") {
                      objControl.selectedIndex = 0;
                      objControl.focus();
                  }
                  else {
                      objControl.value = "";
                      objControl.focus();
                  }
                  alert(msg.d);
                  return false;
              } else { objSend.disabled = false; }
          },
          error: function (msg) {
              var objControl = document.getElementById(args.id);
              var objSend = document.getElementById('<%=btnSave.ClientID %>');
              objControl.value = "  ";
              objControl.focus();
              alert(msg.d);
              return false;
          }
      });
  }

</script>
     <div><span class="style2"><b>Work Order  </b></span><span style="vertical-align:middle" >
         <asp:TextBox 
                ID="txtWorkOrder" runat="server" CausesValidation="True" MaxLength="9" 
             Width="84px" CssClass="TextBox" TabIndex="1" 
             ToolTip="Enter work order number"></asp:TextBox></span></div>
           <span>
               <asp:RegularExpressionValidator ID="minlenght" runat="server"
                    ControlToValidate="txtWorkOrder" ErrorMessage="Remember 9 characters"
                    ValidationExpression="^[a-zA-Z0-9'@&#.\s]{9,9}$" 
         CssClass="errorMsg" SetFocusOnError="True" Display="Dynamic" /></span>

         <asp:RequiredFieldValidator ID="RequiredField" runat="server" 
             ErrorMessage="Work Order is required" SetFocusOnError="False" Display="Dynamic" 
        ControlToValidate="txtWorkOrder" CssClass="errorMsg" Enabled="False"></asp:RequiredFieldValidator>

       <asp:CustomValidator ID="OrderError" runat="server" 
           ErrorMessage="Order doesn't exist or the status is active, release or completed."
           SetFocusOnError="True"
           CssClass="errorMsg"></asp:CustomValidator>

   <div><span>
       <asp:Button ID="btnSend" runat="server" Text="Query" Width="70px" Height="20px" 
           onclick="btnSend_Click" CssClass="ButtonsSendSave"  />
       <br />
       <br />
           <div id="printResult" style="width:80%" runat="server"  align="center">
              <span style="text-align:right">
                  <asp:Label ID="lblResult" runat="server" CssClass="style2" Font-Bold="True" 
                   Font-Italic="True"></asp:Label>
              </span>
              <br />
              <br />
              <span style="text-align:center">
                <asp:Button ID="printLabel" runat="server" height="30px"
                    Text="Print Work Order Tags" Visible="False" onclick="printLabel_Click" 
                   CssClass="ButtonsSendSave" />
              </span>
           </div>
       </span></div>

       <div id="printerDiv" ></div>

       <div class="style3" style="width:80%" >
           
          <div id="HeaderGrid" style="width:80%; height:40px" runat="server">
              <span><asp:Label ID="lblOrder" runat="server" CssClass="lblMessage" 
                  Height="18px" Width="15%"></asp:Label></span>
              <span style="text-align:right;" >
               <asp:Button ID="btnSave" runat="server"
                   Text="Save Items" Visible="False" onclick="btnSave_Click" 
                   CssClass="ButtonsSendSave" Width="90px" Height="20px" />
               </span>
           </div>
           <br />
           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>
           <asp:GridView ID="grdRecords" runat="server" AutoGenerateColumns="False" 
               DataKeyNames="ID" onrowdatabound="grdRecords_RowDataBound" SkinID="Default" >
                       <Columns>
                           <asp:BoundField HeaderText="Position" DataField="pos" >
                           <ItemStyle HorizontalAlign="Center" Width="10%" />
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                           </asp:BoundField>
                           <asp:BoundField HeaderText="Item" DataField="ARTICULO" >
                           <ItemStyle HorizontalAlign="Center" Width="15%" />
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                           </asp:BoundField>
                           <asp:BoundField HeaderText="Description" DataField="Descripcion" >
                           <ItemStyle HorizontalAlign="Left" Width="40%" />
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                           </asp:BoundField>
                           <asp:BoundField HeaderText="Warehouse" DataField="Almacen" >
                           <ItemStyle HorizontalAlign="Center" />
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                           </asp:BoundField>
                           <asp:BoundField HeaderText="Actual Qty" DataField="cant" DataFormatString="{0}">
                           <ItemStyle HorizontalAlign="Center" Width="10%" />
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                           </asp:BoundField>
                           <asp:BoundField HeaderText="Unit" DataField="unidad" >
                           <ItemStyle HorizontalAlign="Center" Width="10%" />
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                           </asp:BoundField>
                           <asp:TemplateField HeaderText="To Return" HeaderStyle-CssClass="HeaderColor" 
                       ControlStyle-Width="10%">
                               <ItemTemplate>
                                   <asp:TextBox ID="toReturn" runat="server" Width="12%" MaxLength="12" 
                             CausesValidation="True" CssClass="TextBox" />

                             <asp:RangeValidator ID="validateQuantity" runat="server" Type="Double" ControlToValidate="toReturn" 
                                                 ErrorMessage="Quantity to return cannot be greater than Actual Quantity"
                                                 Display="Dynamic" ForeColor="Red" Font-Names="Arial" Font-Size="9" 
                                                 Font-Italic="True" SetFocusOnError="true"  MaximumValue="0" MinimumValue="0" />

                                   <asp:RegularExpressionValidator ID="validateReturn" runat="server" 
                             ControlToValidate="toReturn" ErrorMessage="Only numbers allowed" SetFocusOnError="true" 
                             ValidationExpression="[0-9]+(\.[0-9]{1,4})?" Display="Dynamic" 
                             ForeColor="Red" Font-Names="Arial" Font-Size="9" Font-Italic="True"></asp:RegularExpressionValidator>
                            <%--ValidationExpression="(^([0-9]*\d*\d{1}?\d*)$)" Display="Dynamic" --%>
                               </ItemTemplate>
                               <ControlStyle Width="70px"></ControlStyle>
                               <HeaderStyle  CssClass="HeaderColor" HorizontalAlign="Center" />
                               <ItemStyle Width="10px"></ItemStyle>
                           </asp:TemplateField>


                       <asp:TemplateField HeaderText="To Lot" HeaderStyle-CssClass="HeaderGrid" 
                   ControlStyle-Width="15%" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                           <ItemTemplate>
                               <asp:DropDownList ID="toLot" runat="server" CausesValidation="true" Width="15%" 
                         MaxLength="15" CssClass="TextBox" />

<%--                               <asp:TextBox ID="toLot" runat="server" CausesValidation="true" Width="10%" 
                         MaxLength="15" CssClass="TextBox" />
                               <asp:CustomValidator ID="validateLotExist" runat="server" 
                                          ErrorMessage="Lot Code doesn´t exist for this item, please check"  
                                          Display="Dynamic" ForeColor="Red" Font-Names="Arial" 
                         Font-Size="9" Font-Italic="True" SetFocusOnError="true" 
                         ControlToValidate="toLot"></asp:CustomValidator>--%>
                               <%--
                     <asp:CompareValidator ID="validateLotSecond" runat="server" ErrorMessage="Lot Code was not on Work Order, please check" ControlToValidate="toReturn" ValueToCompare=<%# Eval("Lote1") %>
                          Display="Dynamic" ForeColor="Red" Font-Names="Arial" Font-Size="9" Font-Italic="True" SetFocusOnError="true" Operator="NotEqual">
                     </asp:CompareValidator>--%>
                           </ItemTemplate>
                           <ControlStyle Width="90px"></ControlStyle>
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                           <ItemStyle Width="10px"></ItemStyle>
                       </asp:TemplateField>
                       <asp:TemplateField>
                          <ItemTemplate>
                             <asp:HiddenField ID="LOTE" runat="server" 
                                    Value='<%# Eval("LOTE") %>' Visible="False" />
                         </ItemTemplate>
                       </asp:TemplateField>
                       <asp:BoundField DataField="mlot" HeaderText="Lote" ReadOnly="True" 
                   ShowHeader="False" Visible="False" />
                       </Columns>
                   <HeaderStyle HorizontalAlign="Center" Height="20px" />
                       <RowStyle BackColor="#F7F8FB" Font-Names="Arial" Font-Size="9pt" 
                   ForeColor="#1B1B1B" VerticalAlign="Middle" />
               </asp:GridView>
               </ContentTemplate>
           </asp:UpdatePanel>

       </div>
    
</asp:Content>
