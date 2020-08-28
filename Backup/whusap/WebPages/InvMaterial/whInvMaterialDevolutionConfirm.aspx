<%@ Page Title="" Language="C#" MasterPageFile="../../MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvMaterialDevolutionConfirm.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvMaterialDevolutionConfirm" EnableViewStateMac="false"  Theme="Cuadriculas"%>


<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True" EnablePageMethods="true">
    </asp:ScriptManager>
    <br />
  

     <div><span class="style2"><b>Work Order  </b></span><span style="vertical-align:middle" >
         <asp:TextBox 
                ID="txtWorkOrder" runat="server" CausesValidation="True" MaxLength="9" 
             Width="84px" CssClass="TextBox"></asp:TextBox></span></div>
           <span>
               <asp:RegularExpressionValidator ID="minlenght" runat="server"
                    ControlToValidate="txtWorkOrder" ErrorMessage="Remember 9 characters"
                    ValidationExpression="^[a-zA-Z0-9'@&#.\s]{9,9}$" 
         CssClass="errorMsg" SetFocusOnError="True" Display="Dynamic" /></span>

         <asp:RequiredFieldValidator ID="RequiredField" runat="server" 
             ErrorMessage="Work Order is required" ControlToValidate="txtWorkOrder" 
             SetFocusOnError="False" CssClass="errorMsg" Enabled="false"></asp:RequiredFieldValidator>

       <asp:CustomValidator ID="OrderError" runat="server" 
           ErrorMessage="Order doesn't exist or don´t have items"
           SetFocusOnError="True"
           CssClass="errorMsg"></asp:CustomValidator>
   <div>
      <span>
       <asp:Button ID="btnSend" runat="server" Text="Query" 
                        onclick="btnSend_Click" CssClass="ButtonsSendSave" 
           Width="80px" />
       <br />
           <div id="printResult" style="width:80%" runat="server"  align="center">
              <span style="text-align:right">
                  <asp:Label ID="lblResult" runat="server" CssClass="style2" Font-Bold="True" 
                   Font-Italic="True"></asp:Label>
              </span>
              <br />
              <br />
              <div id="printerDiv" runat="server" style="background-color:White;color:Red;" >
              </div>
           </div>
      </span>
   </div>

       <div class="style3">
           
           <div id="HeaderGrid" style="width:80%" runat="server">
              <span><asp:Label ID="lblOrder" runat="server" Text="" CssClass="HeaderGrid"></asp:Label></span>
              <span style="text-align:right">
               <asp:Button ID="btnSave" runat="server" 
                   Text="Save Items" Visible="False" onclick="btnSave_Click" 
                   CssClass="ButtonsSendSave" Height="60%" />
               </span>
           </div>
           <br />
           <asp:GridView ID="grdRecords" runat="server" AutoGenerateColumns="False" 
               BorderStyle="Solid" BorderWidth="1px" Width="82%" BorderColor="#F1F3F8" Font-Names="Arial" 
                       Font-Size="9pt"
               DataKeyNames="IDRECORD" onrowdatabound="grdRecords_RowDataBound" >
               <Columns>
                   <asp:BoundField HeaderText="Position" DataField="pos" >
                   <ItemStyle HorizontalAlign="Center" Width="10%" />
                   <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                   </asp:BoundField>
                   <asp:BoundField HeaderText="Item" DataField="articulo" >
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
                   <asp:BoundField DataField="lot" HeaderText="Lot Code" >
                   <ItemStyle HorizontalAlign="Center"  Width="5%"/>
                   <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                   </asp:BoundField>

                   <asp:BoundField HeaderText="Return Qty." DataField="retQty" 
                       DataFormatString="{0}">
                   <ItemStyle HorizontalAlign="Center" Width="8%" />
                   <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                   </asp:BoundField>
                   <asp:BoundField HeaderText="Unit" DataField="unidad" >
                   <ItemStyle HorizontalAlign="Center" Width="10%" />
                   <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                   </asp:BoundField>
                   <asp:TemplateField HeaderText="Confirmed" HeaderStyle-CssClass="HeaderColor" 
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
                   <asp:TemplateField HeaderText="Confirmed" HeaderStyle-CssClass="HeaderColor" Visible="false" >
                       <ItemTemplate>
                           <asp:Label ID="prin" runat="server" Text="Label" Visible="false"></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
               </Columns>
               <RowStyle BackColor="#F7F8FB" Font-Names="Arial" Font-Size="9pt" 
                   ForeColor="Black" />
           </asp:GridView>
       </div>


</asp:Content>
