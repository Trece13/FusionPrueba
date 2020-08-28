<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvLineClearance.aspx.cs" Inherits="whusap.WebPages.LineClearance.whInvLineClearance" Theme="Cuadriculas"%>
<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True" EnablePageMethods="true">
    </asp:ScriptManager>

    <script type="text/javascript">

        function limpiar(obj) {
            var objControl = document.getElementById(obj.id);
            objControl.value = "";
            objControl.focus();
            return;
        }


        function validaInfo(args, val, obj) {
            if (args === "") {
                return;
            }

            var objSend = document.getElementById('<%=btnSave.ClientID %>');
            
            var parametrosEnviar = "{ 'valor': '" + args + "', 'tipo':'" + val + "', 'orden':' " + document.getElementById('<%=txtWorkOrderTo.ClientID %>').value + "'  }"; 
            if (val == 2)
                parametrosEnviar = "{ 'valor': '" + args + "', 'tipo':'" + val + "', 'orden':'" + document.getElementById('<%=txtWorkOrderFrom.ClientID %>').value + "' }";

            // objSend.disabled = true;
            $.ajax({
                type: "POST",
                url: "whInvLineClearance.aspx/validaInfo",
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
                    }
                },
                error: function (msg) {
                    var objControl = document.getElementById(obj.id);
                    objControl.value = "";
                    objControl.focus();
                    alert("System Execution Error on Query : " + msg.d);
                    return false;
                }
            });
        }

        $.fn.exists = function () {
            return this.length !== 0;
        }
    
    </script>

     <div style="border: 0px solid #2A3B57; width: 19%; height:18px" align="center"><span class="style2" style="vertical-align: middle; width: 1%"><b>Work Order's  </b></span>
          <span style="vertical-align:middle" />
     </div>

     <div class="rTable" align="center">
       <div class="rTableRow" style="height: 18px">
         <div class="rTableCellHead" style="vertical-align: middle; width:10%; height: 18px">
           <span class="style2" style="vertical-align: middle; width:8%"><b style="font-size:11px">From  </b></span>
         </div>
         <div class="rTableCellLeft" style="width:12%;  height: 20px"" align="left">
           <span style="vertical-align:middle" >
             <asp:TextBox ID="txtWorkOrderFrom" runat="server" MaxLength="9" 
                    Width="84px" CssClass="TextBox" TabIndex="1" 
                    ToolTip="Enter work order number" ></asp:TextBox>
           </span>
         </div>
         <div class="rTableCellError">
           <span style="vertical-align:middle" >
          <asp:RegularExpressionValidator ID="minlenght" runat="server" ControlToValidate="txtWorkOrderFrom" 
                                           ErrorMessage="Remember 9 characters"
                                           ValidationExpression="^[a-zA-Z0-9'@&#.\s]{9,9}$" 
                                           CssClass="errorMsg" SetFocusOnError="True" 
                                           Display="Dynamic" />
          <asp:RequiredFieldValidator ID="RequiredField" runat="server" ErrorMessage="Work Order [ From ] is required" 
                                       ControlToValidate="txtWorkOrderFrom" 
                 CssClass="errorMsg" InitialValue="" Enabled="False"></asp:RequiredFieldValidator>

          <asp:CustomValidator ID="OrderError" runat="server" 
           ErrorMessage="Order doesn't exist or the status is active, release or completed."
           SetFocusOnError="True"
           CssClass="errorMsg"></asp:CustomValidator>
       </span>
     </div>
       </div>  

       <div class="rTableRow" style="height: 18px">
         <div class="rTableCellHead" style="vertical-align: middle; width:10%; height: 18px">
           <span class="style2" style="vertical-align: middle; width:12%"><b style="font-size:11px">To  </b></span>
         </div>
         <div class="rTableCellLeft" style="width:12%; height: 20px" align="left" >
           <span style="vertical-align:middle;" >
              <asp:TextBox ID="txtWorkOrderTo" runat="server" MaxLength="9" 
                           Width="84px" CssClass="TextBox" TabIndex="2"  
                           ToolTip="Enter work order number" CausesValidation="True"></asp:TextBox>
           </span>
         </div>
         <div class="rTableCellError">
           <span style="vertical-align:middle" >
             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                             ControlToValidate="txtWorkOrderTo" 
                                             ErrorMessage="Remember 9 characters"
                                             ValidationExpression="^[a-zA-Z0-9'@&#.\s]{9,9}$" 
                                             CssClass="errorMsg"  
                                             Display="Dynamic" />

             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Work Order [To] is required" 
                                         ControlToValidate="txtWorkOrderTo" 
                 CssClass="errorMsg" InitialValue="" Enabled="False"></asp:RequiredFieldValidator>
    
             <asp:CustomValidator ID="CustomValidator1" runat="server" 
                       ErrorMessage="Order doesn't exist or the status is active, release or completed."
                    CssClass="errorMsg"></asp:CustomValidator>

           </span>
        </div>
       </div>  
     </div>
     <div style="height:15px" align="center"></div>
     <div><span>
       <asp:Button ID="btnSend" runat="server" Text="Query" CssClass="ButtonsSendSave" 
             onclick="btnSend_Click" Width="70px" Height="20px" /> 
       <br />
       <br />
          <div id="printResult" style="width:80%" runat="server"  align="center">
              <span style="text-align:right">
                  <asp:Label ID="lblResult" runat="server" CssClass="style2" Font-Bold="True" 
                   Font-Italic="True"></asp:Label>
              </span>
              <br />
           </div>
     </span></div>
     <div id="HeaderGrid" style="width:80%" runat="server">
        <span style="text-align:right">
           <asp:Button ID="btnSave" runat="server" 
                 Text="Save Items" Visible="False" 
                 CssClass="ButtonsSendSave" Width="90px" Height="20px" 
             onclick="btnSave_Click" />
        </span>
     </div>
     <div style="height:10px" align="center"></div>

       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
           <asp:GridView ID="grdRecords" runat="server" AutoGenerateColumns="False" 
               DataKeyNames="ID" onrowdatabound="grdRecords_RowDataBound" SkinID="Default" Width="75%" >
                   <Columns>
                       <asp:BoundField HeaderText="Item" DataField="ARTICULO" >
                       <ItemStyle HorizontalAlign="Center" Width="15%" />
                       <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField HeaderText="Description" DataField="DESCRIPCION" >
                       <ItemStyle HorizontalAlign="Left" Width="40%" />
                       <HeaderStyle CssClass="HeaderGrid" HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField HeaderText="Actual Qty" DataField="ACT_CANT" DataFormatString="{0}">
                       <ItemStyle HorizontalAlign="Center" Width="10%" />
                       <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                       </asp:BoundField>
                       <asp:BoundField HeaderText="Unit" DataField="UNIDAD" >
                       <ItemStyle HorizontalAlign="Center" Width="10%" />
                       <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                       </asp:BoundField>

                       <asp:TemplateField HeaderText="Quantity to Line Clearance WH" HeaderStyle-CssClass="HeaderGrid" 
                   ControlStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                           <ItemTemplate>
                              <asp:TextBox ID="toReturn" runat="server" Width="10%" MaxLength="15" 
                                           CausesValidation="True" CssClass="TextBox" />

                              <asp:RegularExpressionValidator ID="validateReturn" runat="server" 
                                                  ControlToValidate="toReturn" ErrorMessage="Only numbers allowed" SetFocusOnError="true" 
                                                  ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,4})?$" Display="Dynamic" 
                                                  ForeColor="Red" Font-Names="Arial" Font-Size="9" Font-Italic="True" />

                              <asp:CompareValidator ID="validateQuantity" runat="server" 
                                                    Operator="GreaterThan" Display="Dynamic" Type="Double"
                                                    SetFocusOnError="true" 
                                                    ErrorMessage="Quantity to return cannot be greater than Actual Quantity"
                                                    ForeColor="Red" Font-Names="Arial" Font-Size="9" 
                                   Font-Italic="True" Enabled="False"></asp:CompareValidator>


<%--                              <asp:RangeValidator ID="validateQuantity" runat="server" ControlToValidate="toReturn" 
                                                  ErrorMessage="Quantity to return cannot be greater than Actual Quantity"
                                                  Display="Dynamic" ForeColor="Red" Font-Names="Arial" Font-Size="9" 
                                                  Font-Italic="True" SetFocusOnError="true"  MinimumValue="1" />--%>

                           </ItemTemplate>
                           <ControlStyle Width="70px"></ControlStyle>
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
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
                                    Value='<%# Eval("IND_LOTE") %>' Visible="False" />
                         </ItemTemplate>
                       </asp:TemplateField>
                       <asp:BoundField DataField="mlot" HeaderText="Lote" ReadOnly="True" 
                   ShowHeader="False" Visible="False" />
                   </Columns>
                   <HeaderStyle HorizontalAlign="Center" />
                       <RowStyle BackColor="#F7F8FB" Font-Names="Arial" Font-Size="9pt" 
                   ForeColor="#1B1B1B" VerticalAlign="Middle" />
               </asp:GridView>
               <span style="vertical-align:middle" />
               <asp:HiddenField ID="hidden" runat="server" />
           </ContentTemplate>
       </asp:UpdatePanel>

</asp:Content>
