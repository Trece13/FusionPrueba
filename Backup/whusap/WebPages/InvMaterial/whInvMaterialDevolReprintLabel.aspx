<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvMaterialDevolReprintLabel.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvMaterialDevolReprintLabel" %>
<%@ PreviousPageType VirtualPath="~/WebPages/InvMaterial/whInvMaterialDevolution.aspx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True" EnableCdn="true" EnablePageMethods="true" EnablePartialRendering="true"></asp:ScriptManager>
   <script type="text/javascript">
       $.fn.exists = function () {
           return this.length !== 0;
       }

       function printTag() {
           if ($("#Contenido_printerDiv").exists()) {
               $("#Contenido_printerDiv").width(400);
               $("#Contenido_printerDiv").height(400);
               $("#Contenido_printerDiv").html("<iframe src='../Labels/whInvPrintLabel.aspx' class='printIFrame' width='100%'; height='100%'; onload='this.contentWindow.print();'></iframe>"); //
               $("#Contenido_printerDiv").focus();
               setTimeout(stopTimer, 10000);
               

           }
       }

       function stopTimer() {
           $("#Contenido_printerDiv").html("");
       }
   </script>


    <div><span class="style2"><b>Work Order  </b></span><span style="vertical-align:middle" >
        <asp:TextBox 
               ID="txtWorkOrder" runat="server" CausesValidation="False" MaxLength="9" 
            Width="84px" CssClass="TextBox" 
            ToolTip="Enter work order number"></asp:TextBox>
    </span></div>

    <span>
       <asp:RegularExpressionValidator ID="minlenght" runat="server"
                ControlToValidate="txtWorkOrder" ErrorMessage="Remember 9 characters"
                ValidationExpression="^[a-zA-Z0-9'@&#.\s]{9,9}$" 
                 CssClass="errorMsg" SetFocusOnError="false" Display="Dynamic" />

       <asp:RequiredFieldValidator ID="RequiredField" runat="server" 
            ErrorMessage="Work Order is required" SetFocusOnError="false"
       ControlToValidate="txtWorkOrder" CssClass="errorMsg" Enabled="false"></asp:RequiredFieldValidator>

       <asp:CustomValidator ID="OrderError" runat="server" 
          ErrorMessage="Order doesn't exist or the status is active, release or completed."
          SetFocusOnError="True"
          CssClass="errorMsg"></asp:CustomValidator>
    </span>
   <div style="height:6%; vertical-align:middle; padding-top: 5px;">
     <span>
        <asp:Button ID="btnSend" runat="server" Text="Query" CssClass="ButtonsSendSave" 
            onclick="btnSend_Click" height="60%"/>
      </span>
    </div>
    <br />
   <div style="width:80%; height:5%" >       
     <div id="HeaderGrid" style="width:80%" runat="server">
        <span><asp:Label ID="lblOrder" runat="server" CssClass="lblMessage"></asp:Label></span>
     </div>
   </div>

   <div style="width:70%" align="left">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" >
    <ContentTemplate>

      <asp:GridView ID="grdRecords" runat="server" AutoGenerateColumns="False" 
          BorderStyle="Solid" BorderWidth="1px" Width="82%" BorderColor="#F1F3F8" 
          Font-Names="Arial" Font-Size="9pt" DataKeyNames="ID" 
                onrowcommand="grdRecords_RowCommand" onrowdatabound="grdRecords_RowDataBound" >
                  <Columns>
                      <asp:BoundField HeaderText="Position" DataField="t$pono" >
                      <ItemStyle HorizontalAlign="Center" Width="10%" />
                      <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                      </asp:BoundField>
                      <asp:BoundField HeaderText="Item" DataField="t$item" >
                      <ItemStyle HorizontalAlign="Center" Width="15%" />
                      <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                      </asp:BoundField>
                      <asp:BoundField HeaderText="Description" DataField="Descripcion" >
                      <ItemStyle HorizontalAlign="Left" Width="40%" />
                      <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                      </asp:BoundField>
                      <asp:BoundField HeaderText="Warehouse" DataField="t$cwar" >
                      <ItemStyle HorizontalAlign="Center" />
                      <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                      </asp:BoundField>
                      <asp:BoundField HeaderText="Unit" DataField="unidad" >
                      <ItemStyle HorizontalAlign="Center" Width="10%" />
                      <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                      </asp:BoundField>
                      <asp:BoundField HeaderText="Return Qty." DataField="T$REQT" DataFormatString="{0}">
                      <ItemStyle HorizontalAlign="Center" Width="8%" />
                      <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                      </asp:BoundField>
                      <asp:BoundField DataField="T$CLOT" HeaderText="Lot Code" >
                      <ItemStyle HorizontalAlign="Center"  Width="5%"/>
                      <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                      </asp:BoundField>
                      <asp:TemplateField ControlStyle-Height="100%">
                          <ItemTemplate>
                             <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="ButtonsSendSave" 
                                         CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                         CommandName="btnPrint_Click" CausesValidation="True" />
                          </ItemTemplate>
                          <ControlStyle Width="70px"></ControlStyle>
                          <ItemStyle Width="10px"></ItemStyle>
                      </asp:TemplateField>
                  </Columns>
                  <RowStyle BackColor="#F7F8FB" Font-Names="Arial" Font-Size="9pt" 
                    ForeColor="#1B1B1B" VerticalAlign="Middle" />
      </asp:GridView>
    </ContentTemplate>                      

   </asp:UpdatePanel>
   </div>
   <div id="printerDiv" runat="server" enableviewstate="False" ></div>
</asp:Content>
