<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="whInvInventory.aspx.cs" Inherits="whusap.WebPages.Inventarios.whInvInventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div align="center" id="divWarehouse" runat="server" style="width: 100%" visible="true">
        <table style="width: 100%">
            <tr>
                <td class="style8"  style="width: 90px">
                    <span class="style2" style="vertical-align: middle;">
                        <b style="font-size: 11px">
                            <asp:Label ID="lblDescWarehouse" runat="server" />
                        </b>
                    </span>
                </td>
                <td>
                    <asp:Label ID="lblWarehouse" runat="server" Text=""></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblstrclot" runat="server" Text="" Visible="false"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblstrconteo" runat="server" Text="" Visible="false"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblstrseq" runat="server" Text="" Visible="false"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="lblIngreso" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
    </div>

    <div align="center" id="divTabla" runat="server" style="width: 100%">
        <table style="width: 100%">
            <tr>
                <td class="style8" style="width: 90px">
                    <span class="style2" style="vertical-align: middle;">
                        <b style="font-size: 11px">
                            <asp:Label ID="lblLocation" runat="server" />
                        </b>
                    </span>
                </td>
                <td>
                    <asp:TextBox ID="txtlocation" runat="server" AutoPostBack="True" OnTextChanged="txtlocation_TextChanged"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style8">
                    <span class="style2" style="vertical-align: middle;">
                        <b style="font-size: 11px">
                            <asp:Label ID="lblItem" runat="server" />
                        </b>
                    </span>
                </td>
                <td>
                    <asp:TextBox ID="txtItem" runat="server" AutoPostBack="True" OnTextChanged="txtItem_TextChanged"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblDescItem" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>

    <div align="center" id="divLote" runat="server" style="width: 100%" visible="true">
        <table style="width: 100%">
            <tr>
                <td class="style8"  style="width: 90px">
                    <span class="style2" style="vertical-align: middle;">
                        <b style="font-size: 11px">
                            <asp:Label ID="lblLot" runat="server" />
                        </b>
                    </span>
                </td>
                <td>
                    <asp:TextBox ID="txtLot" runat="server" AutoPostBack="True" OnTextChanged="txtLot_TextChanged"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>

    <div align="center" id="divCantidad" runat="server" style="width: 100%" visible="true">
        <table style="width: 100%">
            <tr>
                <td class="style8"  style="width: 90px">
                    <span class="style2" style="vertical-align: middle;">
                        <b style="font-size: 11px">
                            <asp:Label ID="lblQty" runat="server" />
                        </b>
                    </span>
                </td>
                <td>
                    <div id="tooltip">
                        <span class="style2">
                            <em><asp:Label ID="lblMaximumDigits" runat="server" /></em>
                        </span>
                    </div>
                    <asp:TextBox ID="txtCantidad" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="validateReturn" runat="server" ControlToValidate="txtCantidad"
                        ErrorMessage="Only numbers allowed and Quantity must be greater than zero" SetFocusOnError="true"
                        ValidationExpression="[0-9]+(\.[0-9]{1,4})?" Display="Dynamic" ForeColor="Red"
                        Font-Names="Arial" Font-Size="9" Font-Italic="True" CssClass="errorMsg" Font-Bold="false">
                    </asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <span class="style2" style="vertical-align: middle;">
                        <b style="font-size: 11px">
                            <asp:Label ID="lblUnit" runat="server" />
                        </b>
                    </span>
                </td>
                <td>
                    <asp:Label ID="lblUnity" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>

    <div style="padding: 1%; height: 20px; vertical-align: middle; width: 70%; text-align: center;" align="center">
        <asp:Label ID="lblResult" runat="server" CssClass="style2" Font-Bold="True" Font-Italic="True"></asp:Label>
        <br />
        <asp:Label ID="lblError" runat="server" Text="" CssClass="lblMessage" Width="80%" Height="20px" Visible="false"></asp:Label>
    </div>
    <div style="padding: 1%; height: 35px; vertical-align: middle; width: 50%; text-align: center;" align="center">
        <asp:Button ID="btnContinue" runat="server" Text="Continue" CssClass="ButtonsSendSave" Width="107px" Height="24px" TabIndex="6" Visible="false" OnClick="btnContinue_Click" />
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="ButtonsSendSave" Width="107px" Height="24px" TabIndex="7" Visible="false" OnClick="btnSave_Click" />
        <span style="vertical-align: middle" />
    </div>

</asp:Content>
