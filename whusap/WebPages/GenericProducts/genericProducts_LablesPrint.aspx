<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="genericProducts_LablesPrint.aspx.cs" Inherits="whusap.WebPages.GenericProducts.genericProducts_LablesPrint"  Theme="Cuadriculas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True" EnablePageMethods="true">
    </asp:ScriptManager>
    <br />

    <div>
        <span class="style2"><b>Work Order</b></span>
        <span style="vertical-align:middle" >
           <asp:TextBox ID="txtWorkOrder" runat="server" CausesValidation="True" MaxLength="9" Height="20px" 
               Width="84px" CssClass="TextBox"></asp:TextBox>
        </span>
    </div>
    <span>
        <asp:RegularExpressionValidator ID="minlenght" runat="server"
             ControlToValidate="txtWorkOrder" ErrorMessage="Remember 9 characters"
             ValidationExpression="^[a-zA-Z0-9'@&#.\s]{9,9}$" 
             CssClass="errorMsg" SetFocusOnError="True" Display="Dynamic" > </asp:RegularExpressionValidator>
    </span>

    <asp:RequiredFieldValidator ID="RequiredField" runat="server" 
        ErrorMessage="Work Order is required" ControlToValidate="txtWorkOrder" 
        SetFocusOnError="False" CssClass="errorMsg" Enabled="false"> </asp:RequiredFieldValidator>

    <asp:CustomValidator ID="OrderError" runat="server" 
        ErrorMessage="Order doesn't exist or don´t have items"
        SetFocusOnError="True"
        CssClass="errorMsg"> </asp:CustomValidator>
    
    <div>
       <asp:Button ID="btnSend" runat="server" Text="Consultar" 
                        onclick="btnSend_Click" CssClass="ButtonsSendSave" Width="80px" />
       <br />
       <div id="printResult" style="width:80%" runat="server"  align="center">
              <span><asp:Label ID="lblOrder" runat="server" CssClass="lblMessage" 
                  Height="18px" Width="15%"></asp:Label></span>
              <span style="text-align:right;" >
          <span style="text-align:right">
            <asp:Label ID="lblResult" runat="server" CssClass="style2" Font-Bold="True" Font-Italic="True"></asp:Label>
            <br />
            <span style="vertical-align:middle" />
               <asp:Label ID="lblError" runat="server" Text="" CssClass="lblMessage" Visible="false"></asp:Label>
            </span>
          </span>
       </div>
       <br />
       <br />

    <div id="printerDiv" runat="server" style="color:Red;" >

    <span style="vertical-align:middle" />
        <table style="width: 90%">
           <tr>
             <td>
                <asp:GridView ID="grdRecords" runat="server" AutoGenerateColumns="False" 
                    BorderStyle="Solid" BorderWidth="1px" Width="100%" BorderColor="#F1F3F8" 
                    Font-Names="Arial" Font-Size="8pt" DataKeyNames="ID" onrowcommand="grdRecords_RowCommand" 
                    onrowdatabound="grdRecords_RowDataBound" SkinID="Default" >
                    <Columns>
                         <asp:BoundField HeaderText="Orden" DataField="ORDEN" >
                           <ItemStyle HorizontalAlign="Center"  Width="5%"/>
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                         </asp:BoundField>
                         <asp:BoundField HeaderText="Item" DataField="ITEM" >
                           <ItemStyle HorizontalAlign="Center" Width="20%" />
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                         </asp:BoundField>
                         <asp:BoundField HeaderText="Descripcion" DataField="DESCI" >
                           <ItemStyle HorizontalAlign="Left" Width="40%" />
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                         </asp:BoundField>
                         <asp:BoundField HeaderText="UnxCj" DataField="FACTOR" >
                           <ItemStyle HorizontalAlign="Center" Width="15%" />
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                         </asp:BoundField>
                         <asp:TemplateField HeaderText="Iniciar en" HeaderStyle-CssClass="HeaderColor" ControlStyle-Width="10%">
                           <ItemTemplate>
                              <asp:TextBox ID="nSerie" runat="server" Width="15%" MaxLength="12" CausesValidation="True" CssClass="TextBox" Text="0001" />
                                <asp:RegularExpressionValidator ID="validateSerie" runat="server" 
                                 ControlToValidate="nSerie" ErrorMessage="Solo Números (ej. 0001)" SetFocusOnError="true" 
                                 ValidationExpression="[0-9]{4,4}" Display="Dynamic" ValidationGroup='<%# "Group_" + Container.DataItemIndex %>'
                                 ForeColor="Red" Font-Names="Arial" Font-Size="8" Font-Italic="True" ></asp:RegularExpressionValidator>
<%--                              <asp:RequiredFieldValidator EnableClientScript="False" id="RFVtonSerie" runat="server" ControlToValidate="nSerie"
                               ForeColor="Red" Font-Names="Arial" Font-Size="8" Font-Italic="True" Display="Dynamic" Text="<p>Se requiere al menos un caracter" ValidationGroup='<%# "Group_" + Container.DataItemIndex %>'></asp:RequiredFieldValidator>--%>
                           </ItemTemplate>
                           <ControlStyle Width="70px"></ControlStyle>
                           <HeaderStyle  CssClass="HeaderColor" HorizontalAlign="Center" />
                           <ItemStyle Width="10px"></ItemStyle>
                         </asp:TemplateField>

                         <asp:TemplateField HeaderText="Cnt. Imprimir" HeaderStyle-CssClass="HeaderColor" ControlStyle-Width="10%">
                           <ItemTemplate>
                              <asp:TextBox ID="toPrint" runat="server" Width="15%" MaxLength="12" CausesValidation="True" CssClass="TextBox" />
                                <asp:RegularExpressionValidator ID="validatePrint" runat="server" 
                                 ControlToValidate="toPrint" ErrorMessage="Solo Números" SetFocusOnError="true" 
                                 ValidationExpression="[1-9]{1,3}" Display="Dynamic" ValidationGroup='<%# "Group_" + Container.DataItemIndex %>'
                                 ForeColor="Red" Font-Names="Arial" Font-Size="8" Font-Italic="True" ></asp:RegularExpressionValidator>
                              <asp:RequiredFieldValidator EnableClientScript="False" id="RFVtoPrint" runat="server" ControlToValidate="toPrint"
                               ForeColor="Red" Font-Names="Arial" Font-Size="8" Font-Italic="True" Display="Dynamic" Text="<p>Se requiere al menos un caracter" ValidationGroup='<%# "Group_" + Container.DataItemIndex %>'></asp:RequiredFieldValidator>
                           </ItemTemplate>
                           <ControlStyle Width="90px"></ControlStyle>
                           <HeaderStyle  CssClass="HeaderColor" HorizontalAlign="Center" />
                           <ItemStyle Width="10px"></ItemStyle>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Tipo Producto" HeaderStyle-CssClass="HeaderGrid" ControlStyle-Width="15%" 
                         HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                           <ItemTemplate>
                              <asp:DropDownList ID="ddltypeLabel" runat="server" CausesValidation="true" Width="17%" 
                                                MaxLength="15" CssClass="TextBox" >
                                  <asp:ListItem Value="0">-- Seleccione --</asp:ListItem>
                                  <asp:ListItem Value="Label_genericSemiFinish">SemiFinalizado</asp:ListItem>
                                  <asp:ListItem Value="Label_genericDecorated">Decorados</asp:ListItem>
                               </asp:DropDownList>
                               <asp:RequiredFieldValidator InitialValue="0" ID="Req_ID" Display="Dynamic" 
                                ValidationGroup='<%# "Group_" + Container.DataItemIndex %>' 
                                   runat="server" ControlToValidate="ddltypeLabel" ForeColor="Red" Font-Names="Arial" Font-Size="8" Font-Italic="True" 
                                 ErrorMessage="Seleccione un tipo"></asp:RequiredFieldValidator>
                           </ItemTemplate>
                           <ControlStyle Width="90px"></ControlStyle>
                           <HeaderStyle  CssClass="HeaderGrid" HorizontalAlign="Center" />
                           <ItemStyle Width="10px"></ItemStyle>
                         </asp:TemplateField>
                         <asp:TemplateField ControlStyle-Height="100%">
                             <ItemTemplate>
                                <asp:Button ID="btnPrint" runat="server" Text="Imprimir" CssClass="ButtonsSendSave" 
                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" 
                                            CommandName="btnPrint_Click" CausesValidation="True" ValidationGroup='<%# "Group_" + Container.DataItemIndex %>' />
                             </ItemTemplate>
                             <ControlStyle Width="70px"></ControlStyle>
                             <ItemStyle Width="10px"></ItemStyle>
                         </asp:TemplateField>
                    </Columns>
                    <RowStyle BackColor="#F7F8FB" Font-Names="Arial" Font-Size="9pt" 
                      ForeColor="#1B1B1B" VerticalAlign="Middle" />
                 </asp:GridView>
             </td>
           </tr>
        </table>
    </div>
        </span></span>
</asp:Content>
