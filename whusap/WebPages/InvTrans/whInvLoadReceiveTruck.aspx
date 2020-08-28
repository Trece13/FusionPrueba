<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MDMasterPage.Master" CodeBehind="whInvLoadReceiveTruck.aspx.cs" Inherits="whusap.WebPages.InvTrans.whInvLoadTruc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <table>
        <tr>
           <td colspan="2" style="font-weight:bold; text-align:center;">
                <asp:Label ID="lblNameForm" Text="" runat="server" />
               <hr />   
           </td>
        </tr>
        <tr>
            <td style="text-align:center;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;"><asp:Label runat="server" ID="lblScan"></asp:Label></b></span>
            </td>
            <td style="width: 250px">
                <span style="vertical-align: middle;">
                    <asp:Textbox runat="server" ID="txtUniqueID" CssClass="TextboxBig" style="width:100%;" OnTextChanged="ddlUniqueID_OnSelectedIndexChanged"  AutoPostBack="true">
                    </asp:Textbox>
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <hr />  
                
            </td>
        </tr>
        <tr runat="server" id="trDataAditional" visible="false">
            <td colspan="2">
                <fieldset>
                    <table runat="server" id="tblDataTruck">
                    <tr>
                        <td style="text-align:center;">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px;"><asp:Label runat="server" ID="lblPallet"></asp:Label></b></span>
                        </td>
                        <td style="width: 250px">
                            <span style="vertical-align: middle;">
                                <asp:TextBox runat="server" ID="txtPalletID" CssClass="TextboxBig" style="width:100%; text-align:center;" OnTextChanged="txtPalletID_OnTextChanged" AutoPostBack="true" />    
                            </span>
                        </td>
                    </tr>
                    <tr runat="server" id="trLocation">
                        <td style="text-align:center;">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px;">
                                <asp:Label ID="lblLocation" runat="server" /></b></span>
                        </td>
                        <td style="width: 250px">
                            <span style="vertical-align: middle;">
                                <asp:TextBox runat="server" ID="txtLocation" CssClass="TextboxBig" style="width:100%; text-align:center;" Enabled="false" />    
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px;"><asp:Label runat="server" ID="lblItem"></asp:Label></b></span>
                        </td>
                        <td style="width: 250px">
                            <span style="vertical-align: middle;">
                                <table style="padding:0px; margin:0px; width:100%;">
                                    <tr>
                                        <td style="width:80%;" runat="server" id="tdItem"><asp:TextBox runat="server" ID="txtItem" CssClass="TextboxBig" style="width:100%; text-align:center;" Enabled="false" /></td>
                                        <td style="width:20%;" runat="server" id="tdUnit"><asp:TextBox runat="server" ID="txtUnit" CssClass="TextboxBig" style="width:100%; text-align:center;" Enabled="false" /> </td>
                                    </tr>
                                </table>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px;"><asp:Label runat="server" ID="lblLot"></asp:Label></b></span>
                        </td>
                        <td style="width: 250px">
                            <span style="vertical-align: middle;">
                                <asp:TextBox runat="server" ID="txtLot" CssClass="TextboxBig" style="width:100%; text-align:center;" Enabled="false" />    
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:center;">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px;"><asp:Label runat="server" ID="lblQuantity"></asp:Label></b></span>
                        </td>
                        <td style="width: 250px">
                            <span style="vertical-align: middle;">
                                <asp:TextBox runat="server" ID="txtQuantity" CssClass="TextboxBig" style="width:100%; text-align:center;" />    
                                <asp:HiddenField runat="server" ID="hdfQuantity"/>
                                <asp:RegularExpressionValidator ID="validateReturn" runat="server" ControlToValidate="txtQuantity"
                                ErrorMessage="Only numbers allowed and Quantity must be greater than zero" SetFocusOnError="true"
                                ValidationExpression="[0-9]+(\,[0-9]{1,4})?" Display="Dynamic" ForeColor="Red"
                                Font-Names="Arial" Font-Size="9" Font-Italic="True" CssClass="errorMsg" Font-Bold="false">
                                </asp:RegularExpressionValidator>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:center;">
                            <hr />  
                            <asp:Button Text="Confirm Pallet" runat="server" ID="btnConfirmPalletLoad" CssClass="ButtonsSendSave" OnClick="btnConfirmPalletLoad_Click" style="height:30px; width:30%" />
                            <asp:Button Text="Confirm Pallet" runat="server" ID="btnConfirmPalletReceive" CssClass="ButtonsSendSave" OnClick="btnConfirmPalletReceive_Click" style="height:30px; width:30%" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            </td>
        </tr>
    </table>
    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-weight:bold; font-size:15px;" />    
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-weight:bold; font-size:15px;" />    
</asp:Content>