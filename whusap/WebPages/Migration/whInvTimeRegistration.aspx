<%@ Page Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvTimeRegistration.aspx.cs" Inherits="whusap.WebPages.Migration.whInvTimeRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script type="text/javascript">
    </script>
    <style>
        #Contenido_divTable
        {
            margin-bottom:300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table border="0">
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;">
                <asp:Label runat="server" ID="lblMachine" /></b></span>
            </td>
            <td style="width: 250px; padding:5px;">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtMachine" CssClass="TextBoxBig" ClientIDMode="Static" />
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
    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px; font-weight:bold;" ClientIDMode="Static" />
    <div runat="server" id="divTable">

    </div>

    
</asp:Content>
