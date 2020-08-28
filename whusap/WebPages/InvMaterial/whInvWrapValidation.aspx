<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="whInvWrapValidation.aspx.cs" Inherits="whusap.WebPages.InvMaterial.whInvWrapValidation" %>

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
    <span style="vertical-align: middle" />
    <div style="height: 15px" align="center">
    </div>
    <table border="0">
        <tr>
            <td style="text-align: left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                    <b style="font-size: 11px">
                        <asp:Label ID="lblDescPalletId" runat="server" />
                    </b></span>
            </td>
            <td style="width: 250px; padding: 5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox ID="txtSQNB" runat="server" MaxLength="15" Width="180px" CssClass="TextBox"
                        TabIndex="5" Height="30px" ToolTip="Current Weight" Font-Names="Consolas" Font-Size="14pt"></asp:TextBox>
                </span>
            </td>
            <td style="text-align: left;">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <hr />
                 <asp:Button ID="btnSend" runat="server" Text="Save" CssClass="ButtonsSendSave" Width="107px" Height="24px" OnClick="btnSend_Click" TabIndex="6" />
                <!--<asp:Button ID="Button1" runat="server" Text="Query" OnClick="btnSend_Click" CssClass="ButtonsSendSave" />-->
            </td>
        </tr>
    </table>
    <div align="center" id="divTabla" runat="server" style="width: 80%">
        <div style="padding: 1%; height: 20px; vertical-align: middle; width: 70%; text-align: center;"
            align="center">
            <asp:Label ID="lblError" runat="server" CssClass="lblMessage" Width="80%"
                Height="20px" Visible="False" Font-Size="Medium"></asp:Label>
        </div>
        <div style="padding: 1%; height: 35px; vertical-align: middle; width: 50%; text-align: center;"
            align="center">
           
            <span style="vertical-align: middle" />
        </div>
    </div>
    
    <asp:HiddenField ID="hidden" runat="server" />
    <asp:HiddenField ID="hOrdenMachine" runat="server" />
    </span></span>
</asp:Content>
