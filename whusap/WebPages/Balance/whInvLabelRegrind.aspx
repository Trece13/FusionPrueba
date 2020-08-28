<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="whInvLabelRegrind.aspx.cs" Inherits="whusap.WebPages.Balance.whInvLabelRegrind"
    Theme="Cuadriculas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <style type="text/css">
        .style7
        {
            font-family: Arial;
            font-size: xx-small;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True" EnablePageMethods="true">
    </asp:ScriptManager>
    <br />
    <table border="0">
        <tr>
            <td style="text-align: left;">
                <span class="style2"><b>
                    <asp:Label ID="lblWorkOrder" runat="server" />
                </b></span>
            </td>
            <td style="width: 250px; padding: 5px;">
                <span style="vertical-align: middle">
                    <asp:TextBox ID="txtWorkOrder" runat="server" CausesValidation="True" CssClass="TextBoxBig"></asp:TextBox>
                    <asp:Label ID="lblIngreso" runat="server" Text="" Visible="false"></asp:Label>
                </span>
            </td>
            <td style="text-align: left;">
                <asp:RegularExpressionValidator ID="minlenght" runat="server" ControlToValidate="txtWorkOrder"
                    ErrorMessage="Remember 9 characters" ValidationExpression="^[a-zA-Z0-9'@&#.\s]{9,9}$"
                    CssClass="errorMsg" SetFocusOnError="True" Display="Dynamic">
                </asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredField" runat="server" ErrorMessage="Work Order is required"
                    ControlToValidate="txtWorkOrder" SetFocusOnError="False" CssClass="errorMsg"
                    Enabled="false">
                </asp:RequiredFieldValidator>
                <asp:CustomValidator ID="OrderError" runat="server" ErrorMessage="Order doesn't exist or don´t have items"
                    SetFocusOnError="True" CssClass="errorMsg">
                </asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <hr />
                <asp:Button ID="btnSend" runat="server" Text="Query" OnClick="btnSend_Click" CssClass="ButtonsSendSave" />
            </td>
        </tr>
    </table>
    <div>
        <span>
            
            <br />
            <div id="printResult" style="width: 80%" runat="server" align="center">
                <span style="text-align: right">
                    <asp:Label ID="lblResult" runat="server" CssClass="style2" Font-Bold="True" Font-Italic="True"></asp:Label>
                    <br />
                    <span style="vertical-align: middle" />
                    <asp:Label ID="lblError" runat="server" Text="" CssClass="lblMessage" Visible="false"></asp:Label>
                </span>
            </div>
        </span>
    </div>
    <br />
    <br />
    <div id="printerDiv" runat="server">
        <span style="vertical-align: middle" />
        <table style="width: 60%">
            <tr>
                <td class="style8">
                    <span class="style2" style="vertical-align: middle;"><b style="font-size: 11px">
                        <asp:Label ID="lblRegrind" runat="server" />
                    </b></span>
                </td>
                <td style="width: 120px">
                    <span style="vertical-align: middle;">
                        <asp:DropDownList ID="listRegrind" runat="server" Width="250px" Height="20px" CssClass="DropDownList"
                            TabIndex="4" ToolTip="Select one item from list" OnSelectedIndexChanged="listRegrind_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </span>
                </td>
                <td class="rTableCellError" style="width: 50%" height="350px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <span style="vertical-align: middle" /><span class="style2" style="vertical-align: middle;">
                        <b style="font-size: 11px">
                            <asp:Label ID="lblToIssue" runat="server" />
                        </b></span>
                </td>
                <td style="width: 120px">
                    <div id="tooltip">
                        <span class="style7"><em>
                            <asp:Label ID="lblMaxDigits" runat="server" /></em> </span>
                    </div>
                    <span style="vertical-align: middle;">
                        <asp:TextBox ID="txtQuantity" runat="server" MaxLength="15" Width="160px" CssClass="TextBox"
                            TabIndex="5" Height="45px" ToolTip="Current Weight" Font-Names="Consolas" Font-Size="14pt"></asp:TextBox>
                    </span>
                </td>
                <td class="rTableCellError" height="90%" style="width: 350px; text-align: left;"
                    align="left">
                    <span style="vertical-align: middle; text-align: left;" />
                    <asp:RegularExpressionValidator ID="validateReturn" runat="server" ControlToValidate="txtQuantity"
                        ErrorMessage="Only numbers allowed and Quantity must be greater than zero" SetFocusOnError="true"
                        ValidationExpression="[0-9]+(\.[0-9]{1,4})?" Display="Dynamic" ForeColor="Red"
                        Font-Names="Arial" Font-Size="9" Font-Italic="True" CssClass="errorMsg" Font-Bold="false"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
    </div>
    <div style="padding: 1%; height: 35px; vertical-align: middle; width: 50%; text-align: center;"
        align="center">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="ButtonsSendSave" Width="107px"
            Height="24px" OnClick="btnSave_Click" TabIndex="6" Visible="false" />
        <span style="vertical-align: middle" />
    </div>
    <asp:HiddenField ID="hidden" runat="server" />
    <asp:HiddenField ID="hi_indBodega" runat="server" />
    <asp:HiddenField ID="hi_indLote" runat="server" />
    <asp:HiddenField ID="hi_descItem" runat="server" />
    <asp:HiddenField ID="hi_unityItem" runat="server" />
    <asp:HiddenField ID="hi_machineItem" runat="server" />
    </span></span></span>
</asp:Content>
