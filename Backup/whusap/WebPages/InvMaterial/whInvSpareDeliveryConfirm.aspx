<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvSpareDeliveryConfirm.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvSpareDeliveryConfirm" EnableViewStateMac="false"  Theme="Cuadriculas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
   <script language="javascript" src="../../Scripts/script.js" type="text/javascript"></script> 
   <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True" EnableCdn="true" EnablePageMethods="true" EnablePartialRendering="true"></asp:ScriptManager>
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
                DataKeyNames="ID"  SkinID="Default" Width="98%" HorizontalAlign="Center">
                    <Columns>
                      <asp:BoundField HeaderText="Item" DataField="ARTICULO" >
                      <ItemStyle HorizontalAlign="Left" Width="15%" />
                      <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                      </asp:BoundField>
                      <asp:BoundField HeaderText="Description" DataField="DESCRIPCION" >
                      <ItemStyle HorizontalAlign="Left" Width="40%" />
                      <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                      </asp:BoundField>
                      <asp:BoundField HeaderText="Location" DataField="UBICACION" >
                      <ItemStyle HorizontalAlign="Left" Width="10%" />
                      <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                      </asp:BoundField>
                      <asp:BoundField HeaderText="WareHouse" DataField="BODEGA" >
                      <ItemStyle HorizontalAlign="Left" Width="10%" />
                      <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                      </asp:BoundField>
                      <asp:BoundField HeaderText="Quantity" DataField="CANTIDAD" >
                      <ItemStyle HorizontalAlign="Left" Width="10%" />
                      <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                      </asp:BoundField>
                      <asp:BoundField HeaderText="Unit" DataField="UNIDAD" >
                      <ItemStyle HorizontalAlign="Left" Width="10%" />
                      <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                      </asp:BoundField>
                      
                      <asp:TemplateField HeaderText="Confirm" HeaderStyle-CssClass="HeaderColor" 
                          ControlStyle-Width="70px" HeaderStyle-ForeColor="White">
                          <ItemTemplate>
                              <asp:DropDownList ID="OptionsList" runat="server" BackColor="#F4F5F7"
                                  CssClass="DropDownList" Width="5%" Height="20px">
                                  <asp:ListItem Value="2">No</asp:ListItem>
                                  <asp:ListItem Value="1">Yes</asp:ListItem>
                              </asp:DropDownList>
                          </ItemTemplate>
                        <ControlStyle Width="70px"></ControlStyle>
                          <HeaderStyle  CssClass="HeaderColor" HorizontalAlign="Center" />
                          <ItemStyle Width="10px"></ItemStyle>
                      </asp:TemplateField>
                      
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

      <div id="FooterGrid" style="width:80%; height:50px; vertical-align: middle; padding-top: 15px;" 
            runat="server" align="center">
         <span style="text-align:right; vertical-align: middle;">
            <asp:Button ID="btnSave" runat="server" 
                  Text="Save Items" CssClass="ButtonsSendSave" Width="90px" Height="20px" 
              onclick="btnSave_Click" />
         </span>
      </div>
    </div>
</asp:Content>
