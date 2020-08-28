<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvReprintLabelRegrind.aspx.cs" Inherits="whusap.WebPages.Balance.whInvReprintLabelRegrind" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
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
                    &nbsp;</td>
                <td style="width: 120px">
                    &nbsp;</td>
                <td class="rTableCellError" style="width:50%" height="350px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td >
                   <span style="vertical-align:middle" />
                   <span class="style2" style="vertical-align: middle;"><b style="font-size:11px">Regrind Sequence</b></span>
                </td>
                <td style="width: 120px">
                   
                   <span style="vertical-align:middle;" >
                      <asp:TextBox ID="txtSQNB" runat="server" MaxLength="15" 
                                   Width="180px" CssClass="TextBox" TabIndex="5" Height="30px" 
                        ToolTip="Current Weight" Font-Names="Consolas" Font-Size="14pt" ></asp:TextBox>
                      </span></td>
                <td class="rTableCellError" height="90%" style="width:350px; text-align: left;" 
                    align="left">
                    <span style="vertical-align:middle; text-align: left;" />
                    <asp:Label ID="lblIngreso" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
    <div style="padding: 1%; height:20px; vertical-align:middle; width:80%; text-align: center;" 
        align="center">
        <asp:Label ID="lblError" runat="server" Text="" CssClass="lblMessage" Width="80%" Height="20px" Visible="false" ></asp:Label>
    </div>
    <div style="padding: 1%; height:35px; vertical-align:middle; width:80%; text-align: center;" 
        align="center">
        <asp:Button ID="btnSend" runat="server" Text="Continue" CssClass="ButtonsSendSave" 
             Width="107px" Height="24px" onclick="btnSend_Click" TabIndex="6" /> 

    <span style="vertical-align:middle" />
    </div>
</div>
<asp:HiddenField ID="hidden" runat="server" />
    <span style="vertical-align:middle" />
<asp:HiddenField ID="hOrdenMachine" runat="server" />
</asp:Content>
