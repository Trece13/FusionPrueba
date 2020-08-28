<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvLabel.aspx.cs" Inherits="whusap.WebPages.Balance.whInvLabel" Theme="Cuadriculas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <style type="text/css">
        .style7
        {
            font-family: Arial;
            font-size: xx-small;
        }
    </style>
 <script src="<%= ResolveUrl("~/Scripts/jquery-1.4.1.min.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        $("input[id$=Contenido_txtQuantity]").focus(function () {
            $("div[id$=Contenido_tooltip]").show();
        });

        $("input[id$=Contenido_txtQuantity]").blur(function () {
            $("div[id$=Contenido_tooltip]").hide();
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <span style="vertical-align:middle" />
  <div style="height:15px" align="center"></div>

    <div align="center" id="divTabla" runat="server" style="width:80%" >
        <table style="width: 80%">
            <tr>
                <td class="style8">
           <span class="style2" style="vertical-align: middle;"><b style="font-size:11px">Machine </b></span>
                </td>
                <td style="width: 120px">
           <span style="vertical-align:middle;" >

               <asp:DropDownList ID="listMachine" runat="server" Width="145px" Height="20px"
                        CssClass="DropDownList" TabIndex="4" 
                           ToolTip="Select one item from list" 
                        onselectedindexchanged="listMachine_SelectedIndexChanged" AutoPostBack="true" >
               </asp:DropDownList>

           </span>
                </td>
                <td class="rTableCellError" style="width:50%" height="350px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td >
                   <span style="vertical-align:middle" />
                   <span class="style2" style="vertical-align: middle;"><b style="font-size:11px">Weight  </b></span>
                </td>
                <td style="width: 120px">
                   
                   <div id="tooltip"><span class="style7"><em>*Max 4 posiciones decimales</em></span></div>
                   <span style="vertical-align:middle;" >
                      <asp:TextBox ID="txtQuantity" runat="server" MaxLength="15" 
                                   Width="160px" CssClass="TextBox" TabIndex="5" Height="45px" 
                        ToolTip="Current Weight" Font-Names="Consolas" Font-Size="14pt" ></asp:TextBox>
                      </span></td>
                <td class="rTableCellError" height="90%" style="width:350px; text-align: left;" 
                    align="left">
                    <span style="vertical-align:middle; text-align: left;" />
                      <asp:RegularExpressionValidator ID="validateReturn" runat="server" 
                                                      ControlToValidate="txtQuantity" 
                                                      ErrorMessage="Only numbers allowed and Quantity must be greater than zero" 
                                                      SetFocusOnError="true" 
                                                      ValidationExpression="[0-9]+(\.[0-9]{1,4})?" Display="Dynamic" 
                                                      ForeColor="Red" Font-Names="Arial" 
                        Font-Size="9" Font-Italic="True" CssClass="errorMsg" Font-Bold="false"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
    <div style="padding: 1%; height:20px; vertical-align:middle; width:50%; text-align: center;" 
        align="center">
        <asp:Label ID="lblError" runat="server" Text="" CssClass="lblMessage" Visible="false"></asp:Label>
    </div>
    <div style="padding: 1%; height:35px; vertical-align:middle; width:50%; text-align: center;" 
        align="center">
        <asp:Button ID="btnSend" runat="server" Text="Save" CssClass="ButtonsSendSave" 
             Width="107px" Height="24px" onclick="btnSend_Click" TabIndex="6" /> 

    <span style="vertical-align:middle" />
    </div>
</div>
<asp:HiddenField ID="hidden" runat="server" />
    <span style="vertical-align:middle" />
<asp:HiddenField ID="hOrdenMachine" runat="server" />
</asp:Content>
