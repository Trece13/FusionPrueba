<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MDMasterPage.Master" CodeBehind="whInvAnuncioOrden.aspx.cs" Inherits="whusap.WebPages.InvAnunciosProd.whInvAnuncioOrden" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
     <table border="0">
        <tr>
            <td style="text-align:left;">
                <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                <b style="font-size: 11px;"><asp:Label runat="server" ID="lblNumeroOrden"></asp:Label></b></span>
            </td>
            <td style="width: 250px">
                <span style="vertical-align: middle;">
                    <asp:TextBox runat="server" ID="txtNumeroOrden" CssClass="TextboxBig" style="width:100%;" />  
                </span>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center;">
                <hr />  
                <asp:Button Text="" runat="server" ID="btnFindOrder" OnClick="btnFindOrder_Click" CssClass="ButtonsSendSave" style="height:30px;" />
            </td>
        </tr>
        <tr runat="server" id="trDataAditional" visible="false">
            <td colspan="2">
            <hr />
                <fieldset style="padding:5px; border:1px inset white;">
                    <table runat="server" id="tblDataAditional">
                    <tr>
                        <td style="text-align:left;">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px;"><asp:Label runat="server" ID="lblOrderData" /></b></span>
                        </td>
                        <td style="width: 250px; text-align:left; padding-left:20px;">
                            <span style="vertical-align: middle;">
                                <asp:Label runat="server" ID="lblOrdenFabricacion" CssClass="style2" style="width:100%; text-align:center;" Enabled="false" />    
                            </span>
                            <hr />
                        </td>
                    </tr>
                    <tr runat="server">
                        <td style="text-align:left;" rowspan="2">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px;">
                                <asp:Label runat="server" ID="lblItemDesc" /></b></span>
                        </td>
                        <td style="width: 250px; text-align:left; padding-left:20px;">
                            <span style="vertical-align: middle;">
                                <asp:Label runat="server" ID="lblItem" CssClass="style2" style="width:100%; text-align:center;" Enabled="false" />    
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 250px; text-align:left; padding-left:20px;">
                            <span style="vertical-align: middle;">
                                <asp:Label runat="server" ID="lblDescripcionItem" CssClass="style2" style="width:100%; text-align:center;" Enabled="false" />    
                            </span>
                            <hr />  
                        </td>
                    </tr>
                    <tr id="Tr1" runat="server">
                        <td style="text-align:left;" rowspan="2">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px;">
                                <asp:Label Text="" runat="server" ID="lblBodegaDesc" /></b></span>
                        </td>
                        <td style="width: 250px; text-align:left; padding-left:20px;">
                            <span style="vertical-align: middle;">
                                <asp:Label runat="server" ID="lblBodega" CssClass="style2" style="width:100%; text-align:center;" Enabled="false" />    
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 250px; text-align:left; padding-left:20px;">
                            <span style="vertical-align: middle;">
                                <asp:Label runat="server" ID="lblDescripcionBodega" CssClass="style2" style="width:100%; text-align:center;" Enabled="false" />    
                            </span>
                            <hr />  
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left;">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px;">
                                <asp:Label Text="" runat="server" ID="lblTotalDesc" /></b></span>
                        </td>
                        <td style="width: 250px; text-align:left; padding-left:20px;">
                            <span style="vertical-align: middle;">
                                <asp:Label runat="server" ID="lblTotal" CssClass="style2" style="width:100%; text-align:center;" Enabled="false" />    
                            </span>
                            <hr />  
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left;">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px;">
                                <asp:Label Text="" runat="server" ID="lblEntregadoDesc" /></b></span>
                        </td>
                        <td style="width: 250px; text-align:left; padding-left:20px;">
                            <span style="vertical-align: middle;">
                                <asp:Label runat="server" ID="lblEntregado" CssClass="style2" style="width:100%; text-align:center;" />    
                            </span>
                            <hr />  
                        </td>
                    </tr>
                    <tr runat="server" id="trPendiente">
                        <td style="text-align:left;">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px;">
                                <asp:Label Text="" runat="server" ID="lblPendienteDesc" /></b></span>
                        </td>
                        <td style="width: 250px; text-align:left; padding-left:20px;">
                            <span style="vertical-align: middle;">
                                <asp:Label runat="server" ID="lblPendiente" CssClass="style2" style="width:100%; text-align:center;" Enabled="false" />    
                            </span>
                            <hr />
                        </td>
                    </tr>
                    <tr runat="server" id="trAnunciado">
                        <td style="text-align:left;">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px;">
                                <asp:Label Text="" runat="server" ID="lblAnunciadoDesc" /></b></span>
                        </td>
                        <td style="width: 250px;  text-align:left; padding-left:20px;">
                            <span style="vertical-align: middle;">
                                <asp:Textbox runat="server" ID="txtAnunciado" CssClass="TextboxBig" style="width:100%; text-align:center;" />    
                                <asp:RegularExpressionValidator ID="validateReturn" runat="server" ControlToValidate="txtAnunciado"
                                ErrorMessage="Only numbers allowed and Quantity must be greater than zero" SetFocusOnError="true"
                                ValidationExpression="[0-9]+(\.[0-9]{1,4})?" Display="Dynamic" ForeColor="Red"
                                Font-Names="Arial" Font-Size="9" Font-Italic="True" CssClass="errorMsg" Font-Bold="false"></asp:RegularExpressionValidator>
                            </span>
                        </td>
                    </tr>
                    <tr runat="server" id="trPorConfirmar">
                        <td style="text-align:left;">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px;">
                                <asp:Label Text="" runat="server" ID="lblPorConfirmarDesc" /></b></span>
                        </td>
                        <td style="width: 250px;  text-align:left; padding-left:20px;">
                            <span style="vertical-align: middle;">
                                <asp:Label runat="server" ID="lblPorConfirmar" CssClass="style2" style="width:100%; text-align:center;" Enabled="false" />    
                            </span>
                            <hr />
                        </td>
                    </tr>
                    <tr runat="server" id="trConfirmado">
                        <td style="text-align:left;">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px;">
                                <asp:Label Text="" runat="server" ID="lblConfirmadoDesc" /></b></span>
                        </td>
                        <td style="width: 250px;  text-align:left; padding-left:20px;">
                            <span style="vertical-align: middle;">
                                <asp:Textbox runat="server" ID="txtConfirmado" CssClass="TextboxBig" style="width:100%; text-align:center;" />    
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtConfirmado"
                                ErrorMessage="Only numbers allowed and Quantity must be greater than zero" SetFocusOnError="true"
                                ValidationExpression="[0-9]+(\.[0-9]{1,4})?" Display="Dynamic" ForeColor="Red"
                                Font-Names="Arial" Font-Size="9" Font-Italic="True" CssClass="errorMsg" Font-Bold="false"></asp:RegularExpressionValidator>
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:left;">
                            <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px;">
                                <asp:Label Text="" runat="server" ID="lblUnidadDesc" /></b></span>
                        </td>
                        <td style="width: 250px; text-align:left; padding-left:20px;">
                            <span style="vertical-align: middle;">
                                <asp:Label runat="server" ID="lblUnidad" CssClass="style2" style="width:100%; text-align:center;" Enabled="false" />    
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align:center;">
                            <hr />  
                            <asp:Button Text="" runat="server" ID="btnSaveInformation" CssClass="ButtonsSendSave" OnClick="btnSaveInformation_Click" style="height:30px; width:30%" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            </td>
        </tr>
    </table>
   
    <asp:Label Text="" runat="server" ID="lblError" style="color:red; font-size:15px; font-weight:bold;" />
    <asp:Label Text="" runat="server" ID="lblConfirm" style="color:green; font-size:15px;" />
</asp:Content>
