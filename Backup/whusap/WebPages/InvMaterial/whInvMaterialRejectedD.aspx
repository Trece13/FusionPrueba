<%@ Page Title="" Language="C#" MasterPageFile="../../MDMasterPage.Master" AutoEventWireup="true" EnableViewStateMac="false" CodeBehind="whInvMaterialRejectedD.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvMaterialRejectedD" Theme="Cuadriculas" %>
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
        if (val == "") {
            return;
        }
        var parametrosEnviar = "{ 'Fila': '" + JSON.stringify(args) + "', 'valor':'" +val + "'}";
        var objSend = document.getElementById('<%=btnSave.ClientID %>');
        objSend.disabled = true;
        $.ajax({
            type: "POST",
            url: "whInvMaterialRejectedD.aspx/validaExistLot",
            data: parametrosEnviar,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (msg) {
                if (msg.d != "") {
                    var objControl = document.getElementById(obj.id);
                    objControl.value = "";
                    objControl.focus();
                    alert(msg.d);
                    return false;
                } else { objSend.disabled = false; }
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

     <div><span class="style2">
        
     <b>Item  </b></span><span style="vertical-align:middle" >
         <asp:TextBox 
                ID="txtItem" runat="server" CausesValidation="True" MaxLength="47" 
             Width="200px" CssClass="TextBox" TabIndex="1" 
             ToolTip="Enter item code"></asp:TextBox></span></div>

         <asp:RequiredFieldValidator ID="RequiredField" runat="server" 
             ErrorMessage="Item code is required" SetFocusOnError="False" Display="Dynamic" 
        ControlToValidate="txtItem" CssClass="errorMsg" Enabled="False"></asp:RequiredFieldValidator>
                   
     <div><span class="style2"><b>Lot  </b></span><span style="vertical-align:middle" >
         <asp:TextBox 
                ID="txtLot" runat="server" CausesValidation="True" MaxLength="20" 
             Width="100px" CssClass="TextBox" TabIndex="2" 
             ToolTip="Enter lot code"></asp:TextBox></span></div>

        <asp:RequiredFieldValidator ID="RequiredFieldLot" runat="server" 
             ErrorMessage="Lot code is required" SetFocusOnError="False" Display="Dynamic" 
        ControlToValidate="txtLot" CssClass="errorMsg" Enabled="False"></asp:RequiredFieldValidator>

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
                <asp:Button ID="printLabel" runat="server" height="24px"
                    Text="Print Tag" Visible="False" onclick="printLabel_Click" 
                   CssClass="ButtonsSendSave" />
              </span>
           </div>
       </span></div>

       <div id="printerDiv" ></div>

       <div class="style3" style="width:80%" >
           
          <div id="HeaderGrid" style="width:80%; height:40px" runat="server">

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
               onrowdatabound="grdRecords_RowDataBound" SkinID="Default">
                       <Columns>
                           <asp:BoundField HeaderText="Item" DataField="item" >
                           <ItemStyle HorizontalAlign="Center" Width="15%" />
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                           </asp:BoundField>
                           <asp:BoundField HeaderText="Description" DataField="Desci" >
                           <ItemStyle HorizontalAlign="Left" Width="40%" />
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                           </asp:BoundField>
                           <asp:BoundField HeaderText="Warehouse" DataField="warehouse" >
                           <ItemStyle HorizontalAlign="Center" />
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                           </asp:BoundField>
                           <asp:BoundField HeaderText="Lot" DataField="lot" >
                           <ItemStyle HorizontalAlign="Center" />
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                           </asp:BoundField>
                           <asp:BoundField HeaderText="Actual Qty" DataField="stock" DataFormatString="{0}" Visible="False">
                           <ItemStyle HorizontalAlign="Center" Width="10%" />
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                           </asp:BoundField>
                           <asp:TemplateField>
                          <ItemTemplate>
                             <asp:HiddenField ID="ActualQty" runat="server" 
                                    Value='<%# Eval("stock") %>' Visible="False" />
                         </ItemTemplate>
                       </asp:TemplateField>
                           <asp:BoundField HeaderText="Unit" DataField="unidad" >
                           <ItemStyle HorizontalAlign="Center" Width="10%" />
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                           </asp:BoundField>
                           <asp:TemplateField HeaderText="To Return" HeaderStyle-CssClass="HeaderColor" 
                       ControlStyle-Width="10%">
                        <ItemTemplate>
                            <asp:TextBox ID="toReturn" runat="server" Width="10%" MaxLength="8" 
                             CausesValidation="True" CssClass="TextBox" Font-Names="Calibri" Font-Size="Small" />

                             <asp:RangeValidator ID="validateQuantity" runat="server" Type="Integer" ControlToValidate="toReturn" 
                                                 ErrorMessage="Quantity to return cannot be greater than Actual Quantity"
                                                 Display="Dynamic" ForeColor="Red" Font-Names="Arial" Font-Size="9" 
                                                 Font-Italic="True" SetFocusOnError="true"  MaximumValue="0" MinimumValue="0" />

                                   <asp:RegularExpressionValidator ID="validateReturn" runat="server" 
                             ControlToValidate="toReturn" ErrorMessage="Only numbers allowed" SetFocusOnError="true" 
                            ValidationExpression="(^([0-9]*\d*\d{1}?\d*)$)" Display="Dynamic" 
                             ForeColor="Red" Font-Names="Arial" Font-Size="9" Font-Italic="True"></asp:RegularExpressionValidator>
                        </ItemTemplate>
                        <ControlStyle Width="70px"></ControlStyle>
                        <HeaderStyle  CssClass="HeaderColor" HorizontalAlign="Center" />
                        <ItemStyle Width="10px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Disposition" HeaderStyle-CssClass="HeaderColor" 
                       ControlStyle-Width="15%" HeaderStyle-Width="10%">
                           <ItemTemplate>
                                   <asp:DropDownList ID="Dispoid" runat="server" Width="70%" Font-Names="Calibri" 
                                       Font-Size="Small" onselectedindexchanged="Dispoid_SelectedIndexChanged" AutoPostBack="True">
                                       <asp:ListItem Value="1">Select Disposition Reason</asp:ListItem>
                                       <asp:ListItem Value="2">Return to Vendor</asp:ListItem>
                                       <asp:ListItem Value="3">Return to Stock</asp:ListItem>
                                       <asp:ListItem Value="4">Regrind</asp:ListItem>
                                       <asp:ListItem Value="5">Recycle</asp:ListItem>
                                   </asp:DropDownList>
                               </ItemTemplate>
                               <ControlStyle Width="80px"></ControlStyle>
                               <HeaderStyle  CssClass="HeaderColor" HorizontalAlign="Center" />
                               <ItemStyle Width="10px"></ItemStyle>
                           </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason"  HeaderStyle-CssClass="HeaderColor" 
                       ControlStyle-Width="15%" HeaderStyle-Width="10%">
                               <ItemTemplate>
                                   <asp:DropDownList ID="Reasonid" runat="server" Width="20%" Font-Names="Calibri" 
                                       Font-Size="Small">
                                   </asp:DropDownList>
                               </ItemTemplate>
                               <ControlStyle Width="80px"></ControlStyle>
                               <HeaderStyle  CssClass="HeaderColor" HorizontalAlign="Center" />
                               <ItemStyle Width="10px"></ItemStyle>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Supplier" HeaderStyle-CssClass="HeaderColor" 
                       ControlStyle-Width="15%" HeaderStyle-Width="10%">
                               <ItemTemplate>
                                   <asp:DropDownList ID="Supplier" runat="server" Font-Names="Calibri" 
                                       Font-Size="Small" Enabled="False">
                                   </asp:DropDownList>
                               </ItemTemplate>
                               <ControlStyle Width="80px"></ControlStyle>
                               <HeaderStyle  CssClass="HeaderColor" HorizontalAlign="Center" />
                               <ItemStyle Width="10px"></ItemStyle>
                           </asp:TemplateField>
                           <asp:BoundField DataField="mlot" HeaderText="Lote" ReadOnly="True" 
                       ShowHeader="False" Visible="False" />
                           <asp:TemplateField HeaderText="Stock Warehouse">
                           <ControlStyle Width="80px"></ControlStyle>
                               <ItemTemplate>
                           <asp:DropDownList ID="Stockwareh" runat="server" Font-Names="Calibri" 
                                       Font-Size="Small" Width="70%" Enabled="False">
                                   </asp:DropDownList>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Regrind Item">
                           <ControlStyle Width="80px"></ControlStyle>
                               <ItemTemplate>
                                   <asp:DropDownList ID="Regrind" runat="server" Font-Names="Calibri" 
                                       Font-Size="Small" Width="70%" Enabled="False">
                                   </asp:DropDownList>
                               </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Comments">
                               <EditItemTemplate>
                                   <asp:TextBox ID="Comments" runat="server" Font-Names="Calibri" 
                                       Font-Size="Small" TextMode="MultiLine">Comments</asp:TextBox>
                               </EditItemTemplate>
                               <ItemTemplate>
                                   <asp:TextBox ID="Comments" runat="server" Font-Names="Calibri" 
                                       Font-Size="Small" TextMode="MultiLine"></asp:TextBox>
                               </ItemTemplate>
                           </asp:TemplateField>
                       </Columns>
                       <RowStyle BackColor="#F7F8FB" Font-Names="Arial" Font-Size="9pt" 
                   ForeColor="#1B1B1B" VerticalAlign="Middle" />
                   </asp:GridView>
               </ContentTemplate>
           </asp:UpdatePanel>

       </div>
    
</asp:Content>
