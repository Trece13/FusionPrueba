<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MDMasterPage.Master" CodeBehind="whInvGenerateUI.aspx.cs" Inherits="whusap.WebPages.InvTrans.whInvGenerateUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table border="0">
        <tr>
           <td style="font-weight:bold; text-align:left;">
                <asp:Label ID="lblPagina" Text="" runat="server" />
                <hr />  
           </td>
           <td style="text-align:right; font-weight:normal;">
                <asp:Label ID="lblHora" Text="" runat="server" />
                <hr />  
           </td>
           
        </tr>
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;"><asp:Label runat="server" ID="lblOperator"></asp:Label></b></span>
            </td>
            <td style="width: 250px">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtOperator" CssClass="TextboxBig" Enabled="false" style="width:100%;" />  
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;"><asp:Label runat="server" ID="lblTruck"></asp:Label></b></span>
            </td>
            <td style="width: 250px">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtTruckID" CssClass="TextboxBig" 
                    style="width:100%;" MaxLength="10" />  
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;"><asp:Label runat="server" ID="lblSource"></asp:Label></b></span>
            </td>
            <td style="width: 250px">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtSourceWareHouse" CssClass="TextboxBig" 
                    style="width:100%;" MaxLength="6"/>
                </span>
            </td>
        </tr>
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;"><asp:Label runat="server" ID="lblDestination"></asp:Label></b></span>
            </td>
            <td style="width: 250px">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtDestinationWareHouse" 
                    CssClass="TextboxBig" style="width:100%;" MaxLength="6"/>    
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <hr />  
                <asp:Button Text="" runat="server" ID="btnGenerateUI" CssClass="ButtonsSendSave" style="height:30px;" OnClick="btnGenerateUI_Click" />
            </td>
        </tr>
        <tr id="divUniqueIdentifier" runat="server">
            <td colspan="2" style="text-align:center;">
                <asp:Textbox runat="server" ID="txtUI" CssClass="TextboxBig" Enabled="false" style="border-color:Green; width:100%; text-align:center; font-weight:bold;" />    
            </td>
        </tr>
    </table>
    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px;" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-weight:bold; font-size:15px;" />       
</asp:Content>