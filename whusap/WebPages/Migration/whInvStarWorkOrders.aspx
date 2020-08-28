<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvStarWorkOrders.aspx.cs" Inherits="whusap.WebPages.Migration.whInvStarWorkOrders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table border="0">
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                    <asp:Label runat="server" ID="lblWorkOrder" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtWorkOrder" CssClass="TextBoxBig" ClientIDMode="Static" />
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <hr />  
                <asp:Button Text="" runat="server" ID="btnConsultar" OnClick="btnConsultar_Click" CssClass="ButtonsSendSave" style="height:30px;" />
            </td>
        </tr>
    </table>

    <div runat="server" id="divTable"  visible="false">
        <hr />
        <table class='table table-bordered' style='font-size:13px; border:3px solid; border-style:outset; text-align:center; font-weight:bold;'>
            <tr style='background-color: lightgray;'>
                <td><asp:Label ID="lblOrder" runat="server" /></td>
                <td><asp:Label ID="lblValueOrder" runat="server" /></td>
                <td><asp:Button ID="btnSaveOrder" OnClick="btnSaveOrder_Click" runat="server" CssClass="ButtonsSendSave" /></td>
            </tr>
            <tr style='background-color: white;'>
                <td><asp:Label ID="lblMachine" runat="server" /></td>
                <td><asp:Label ID="lblStatus" runat="server" /></td>
                <td><asp:Label ID="lblAction" runat="server" /></td>
            </tr>
            <tr>
                <td><asp:Label ID="lblValueMachine" runat="server" /></td>
                <td><asp:Label ID="lblValueStatus" runat="server" /></td>
                <td>
                    <asp:DropDownList runat="server" ID="slAction" CssClass="TextBoxBig" >
                    </asp:DropDownList>
                </td>
            </tr>
        </table>

    </div>


    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />

</asp:Content>