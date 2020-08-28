<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvSpareDelivery.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvSpareDelivery" EnableViewStateMac="false"  Theme="Cuadriculas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <script language="javascript" src="../../Scripts/script.js" type="text/javascript"></script> 
   <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True" EnableCdn="true" EnablePageMethods="true" EnablePartialRendering="true"></asp:ScriptManager>
   <script type="text/javascript">   
      function validaInfo(args, val, obj, objCon) {
          if (args === "") {
              return;
          }

          var objSend = document.getElementById('<%=btnSave.ClientID %>');
        //  obj = args;
          var parametrosEnviar = "{ 'valor': '" + args + "', 'tipo':'" + val + "', 'orden':' " + document.getElementById('<%=txtWorkOrder.ClientID %>').value + "'  }"; 
          if (val == 2)
              parametrosEnviar = "{ 'valor': '" + args  + "', 'tipo':'" + val + "', 'orden':'" + document.getElementById('<%=txtWorkOrder.ClientID %>').value + "' }";
          debugger;
          // objSend.disabled = true;
          $.ajax({
              type: "POST",
              url: "whInvSpareDelivery.aspx/validaInfo",
              data: parametrosEnviar,
              contentType: "application/json; charset=utf-8",
              dataType: "json",
              success: function (msg) {
                  if (msg.d != "") {
                      if (msg.d.indexOf("BAAN") > -1) {
                          var objControl = document.getElementById(objCon.id);
                          objControl.value = "";
                          objControl.focus();
                          alert(msg.d);
                          return false;
                      }
                      else {
                          updateRow(obj, msg.d, val);
                      }
                  }
              },
              error: function (msg) {
                  var objControl = document.getElementById(objCon.id);
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

      function updateRow(rowIndex, objData, tipo) {
          var values = objData.split('|');
          var grd = document.getElementById('<%= grdRecords.ClientID %>');
          if (tipo == 1)
          {
            var fValue = values[0];
            var sValue = values[1];

            document.getElementById('<%= hiddenIDesc.ClientID %>').value = fValue + "|" + sValue;

            grd.rows[rowIndex + 1].cells[2].childNodes[1].innerText = fValue;
            grd.rows[rowIndex + 1].cells[5].childNodes[1].innerHTML = sValue;
          }
        if (tipo == 2) {
            var fValue = values[0];
            var sValue = values[1];
            document.getElementById('<%= hiddenLDesc.ClientID %>').value = sValue;

            grd.rows[rowIndex + 1].cells[4].childNodes[1].innerText = sValue;
        }

    }
   </script>


    <div align="center" id="divZona" runat="server" 
            style="width:100%; height: 40px; vertical-align: top;" >      
            <div class="rTable" style="width:100%; height: 40px; ">
              <div class="rTableBody" style="width:80%; height: 30px;">
                <div class="rTableCell" style="background-color: #CFD8FA; width:15%; height:20px; ">
                    <span class="style2" style="vertical-align:bottom; width:50%; height: 25px; text-align: right;"><b>Work Order</b></span> </div>
                <div class="rTableCell" style="Width:130px; height: 30px;" align="left">
                    <asp:TextBox 
                           ID="txtWorkOrder" runat="server" CausesValidation="True" MaxLength="9" 
                        Width="120px" CssClass="TextBox" TabIndex="1" 
                        ToolTip="Enter work order number"  Height="20px">
                    </asp:TextBox>
                </div>
                <div class="rTableCell" style="height:30px; width: 90px;" align="left">
                    <asp:Button ID="btnSend" runat="server" Text="Continue" CssClass="ButtonsSendSave" 
                             Width="82px" Height="20px" TabIndex="6" onclick="btnSend_Click" /> </div>
            <div class="rTableCell" style="width:60%; height: 30px;" align="left">
             <span>
             <asp:RegularExpressionValidator ID="minlenght" runat="server"
                        ControlToValidate="txtWorkOrder" ErrorMessage="Remember 9 characters"
                        ValidationExpression="^[a-zA-Z0-9'@&#.\s]{9,9}$" 
                        CssClass="errorMsg" SetFocusOnError="True" Display="Dynamic" />

             <asp:RequiredFieldValidator ID="RequiredField" runat="server" 
                                         ErrorMessage="Work Order is required" SetFocusOnError="False" 
                                         Display="Dynamic" ControlToValidate="txtWorkOrder" 
                                         CssClass="errorMsg" Enabled="False">
             </asp:RequiredFieldValidator>
         
             <asp:CustomValidator ID="OrderError" runat="server" 
                                  ErrorMessage="Order doesn't exist or the status is active, release or completed."
                                  SetFocusOnError="True"
                                  CssClass="errorMsg">
             </asp:CustomValidator>
         </span>
    </div>
            </div>
            </div>
        </div>

        <div align="center" style="width: 50%; height:15%">
          <span>
             <br /><br />
             <div id="printResult" style="width:80%" runat="server"  align="center">
                <span style="text-align:right">
                    <asp:Label ID="lblResult" runat="server" CssClass="style2" Font-Bold="True" 
                     Font-Italic="True"></asp:Label>
                </span>
                <br /><br />
             </div>
          </span>
        </div>
        <div style="height:5px" align="center"></div>
        <div style="width: 90%; top: 10px;">
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <asp:GridView ID="grdRecords" runat="server" AutoGenerateColumns="False" 
                    DataKeyNames="ID"  SkinID="Default" Width="98%" HorizontalAlign="Center" 
                        onrowdatabound="grdRecords_RowDataBound" 
                        onrowdeleting="grdRecords_RowDeleting">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="SNo">
                                <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                                <ItemStyle Width="20px"></ItemStyle> 
                                </asp:BoundField>
                            <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="HeaderGrid" 
                                              ControlStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                   <asp:TextBox ID="txtItem" runat="server" Width="90px" MaxLength="15" 
                                                CausesValidation="True" CssClass="TextBox" />
                                </ItemTemplate>
                                <ControlStyle Width="100px"></ControlStyle>
                                <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                                <ItemStyle Width="20px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="HeaderGrid" 
                                               ControlStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                 <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" CausesValidation="True" />
                                 </ItemTemplate>
                                 <ControlStyle Width="55%"></ControlStyle>
                                 <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                                 <ItemStyle Width="55%"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location" HeaderStyle-CssClass="HeaderGrid" 
                                               ControlStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                 <ItemTemplate>
                                    <asp:TextBox ID="txtLocation" runat="server" Width="70px" MaxLength="15" 
                                                 CausesValidation="True" CssClass="TextBox" />
                                 </ItemTemplate>
                                 <ControlStyle Width="70px"></ControlStyle>
                                 <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                                 <ItemStyle Width="30px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="WareHouse" HeaderStyle-CssClass="HeaderGrid" 
                                               ControlStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                 <ItemTemplate>
                                    <asp:Label ID="lblWareHouse" runat="server" CausesValidation="True" />
                                 </ItemTemplate>
                                 <ControlStyle Width="20%"></ControlStyle>
                                 <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                                 <ItemStyle Width="20%"></ItemStyle>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Unit" HeaderStyle-CssClass="HeaderGrid" 
                                               ControlStyle-Width="20%" ItemStyle-HorizontalAlign="Left">
                                 <ItemTemplate>
                                    <asp:Label ID="lblUnit" runat="server" CausesValidation="True" />
                                 </ItemTemplate>
                                 <ControlStyle Width="10%"></ControlStyle>
                                 <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                                 <ItemStyle Width="10%"></ItemStyle>
                            </asp:TemplateField>

 
                            <asp:TemplateField HeaderText="Quantity to Delivery" HeaderStyle-CssClass="HeaderGrid" 
                        ControlStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                   <asp:TextBox ID="toReturn" runat="server" Width="70px" MaxLength="15" 
                                                CausesValidation="True" CssClass="TextBox" Text="0" ></asp:TextBox>
 
                                   <asp:RegularExpressionValidator ID="validateReturn" runat="server" 
                                                       ControlToValidate="toReturn" ErrorMessage="Only numbers allowed" SetFocusOnError="true" 
                                                       ValidationExpression="^[0-9]{1,11}(?:\.[0-9]{1,4})?$" Display="Dynamic" 
                                                       ForeColor="Red" Font-Names="Arial" Font-Size="9" Font-Italic="True" />
 
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                        Display="Dynamic" ErrorMessage="Quantity must be greater than zero" Operator="GreaterThan" 
                                        SetFocusOnError="True" ValueToCompare="0" ForeColor="Red" Font-Names="Arial" Font-Size="9" Font-Italic="True" ControlToValidate="toReturn"></asp:CompareValidator>
 
                                </ItemTemplate>
                                <ControlStyle Width="90px"></ControlStyle>
                                <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                                <ItemStyle Width="40px"></ItemStyle>
                            </asp:TemplateField>
                            <asp:CommandField ButtonType="Button" DeleteText="Delete Row" 
                                ShowCancelButton="False" ShowDeleteButton="True">
                            <ControlStyle CssClass="ButtonsSendSave" />
                            <HeaderStyle CssClass="HeaderGrid" Wrap="False" />
                            <ItemStyle Wrap="False" />
                            </asp:CommandField>
                        </Columns>
                        <HeaderStyle HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F8FB" Font-Names="Arial" Font-Size="9pt" 
                        ForeColor="#1B1B1B" VerticalAlign="Middle" />
                    </asp:GridView>
                    <span style="vertical-align:middle">
                    <asp:HiddenField ID="hiddenIDesc" runat="server" />
                    <asp:HiddenField ID="hiddenLDesc" runat="server" />
                    </span>
                </ContentTemplate>
            </asp:UpdatePanel>

          <div id="divOptButtons" runat="server" style="width:80%; height:50px; vertical-align: middle; top: 10px;" align="center">

              <div class="rTableBody" style="width:60%; height: 30px;">
                <div class="rTableCell" style="Width:80px; height: 30px;" align="left">
                    <asp:Button ID="btnAddRow" runat="server" 
                      Text="Add Row" Visible="true" 
                      CssClass="ButtonsSendSave" Width="90px" Height="20px" 
                        onclick="btnAddRow_Click" />
                </div>
                <div class="rTableCell" style="Width:120px; height: 30px;" align="left">
                  <asp:Button ID="btnContinue" runat="server" 
                        Text="Continue to Save" Visible="False" 
                        CssClass="ButtonsSendSave" Width="120px" Height="20px" 
                        onclick="btnContinue_Click"  />
                </div>
            </div>

          </div>

          <div id="FooterGrid" style="width:80%; height:50px; vertical-align: middle; padding-top: 15px;" 
                runat="server" align="center">
             <span style="text-align:right; vertical-align: middle;">
                <asp:Button ID="btnSave" runat="server" 
                      Text="Save Items" Visible="False" 
                      CssClass="ButtonsSendSave" Width="90px" Height="20px" 
                  onclick="btnSave_Click" />
             </span>
          </div>
        </div>

    
</asp:Content>
