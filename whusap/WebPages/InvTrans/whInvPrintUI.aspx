<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MDMasterPage.Master" CodeBehind="whInvPrintUI.aspx.cs" Inherits="whusap.WebPages.InvTrans.whInvPrintUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        function printTag() {
            if ($("#Contenido_printerDiv").exists()) {
                $("#Contenido_printerDiv").width(400);
                $("#Contenido_printerDiv").height(400);
                $("#Contenido_printerDiv").html("<iframe src='../Labels/whInvPrintLabel.aspx' width='100%'; height='100%';></iframe>"); //onload='this.contentWindow.print();
                $("#Contenido_printerDiv").html(""); //onload='this.contentWindow.print();
            }
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table>
        <tr>
           <td colspan="2" style="font-weight:bold; text-align:center;">
                <asp:Label Text="Unique ID" runat="server" />
               <hr />   
           </td>
        </tr>
         <tr>
            <td style="text-align:center;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;" >
                <b style="font-size: 11px;"><asp:Label runat="server" ID="lblScan"></asp:Label></b></span>
            </td>
            <td style="width: 250px">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtUniqueID" CssClass="TextboxBig" style="width:100%;" ClientIDMode="Static"/>    
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <hr />  
                <asp:Button Text="" runat="server" ID="btnPrint" OnClick="btnPrint_Click" CssClass="ButtonsSendSave" style="height:30px; width:30%" />
            </td>
        </tr>
    </table>
    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px;" />    
</asp:Content>
 