<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true"
    CodeBehind="whInvLabelRegrindm.aspx.cs" Inherits="whusap.WebPages.Balance.whInvLabelRegrindm" %>

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
    <div>
        <span>
            <div id="printResult" style="width: 80%" runat="server" align="center">
                <span style="text-align: right">
                    <asp:Label ID="lblResult" runat="server" CssClass="style2" Font-Bold="True" Font-Italic="True"></asp:Label>
                    <br />
                    <span style="vertical-align: middle" />
                    <asp:Label ID="lblError" runat="server" Text="" CssClass="lblMessage" Visible="false"></asp:Label>
                </span>
            </div>
        </span>
        <br />
        <br />
    </div>
    <div id="printerDiv" runat="server">
        <span style="vertical-align: middle" />
        <table border="0">
            <tr>
                <td style="text-align: left;">
                    <span class="style2" style="vertical-align: middle;"><b style="font-size: 11px">
                        <asp:Label ID="lblMachine" runat="server" />
                    </b></span>
                </td>
                <td style="width: 250px; padding: 5px;">
                    <span style="vertical-align: middle" />
                    <asp:TextBox ID="txtMachine" runat="server" AutoPostBack="True" OnTextChanged="txtMachine_listMachine"
                        CssClass="TextBoxBig"></asp:TextBox>
                </td>
                <td style="text-align: left;">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center;">
                    <hr />
                    <asp:Button ID="btnChangeMac" runat="server" Text="Change Machine" CssClass="ButtonsSendSave"
                        Enabled="False" OnClick="btnChangeMac_Click" />
                    <asp:Label ID="lblIngreso" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table style="width: 60%">
            <tr>
                <td class="style8">
                    <span class="style2" style="vertical-align: middle;"><b style="font-size: 11px">
                        <asp:Label ID="lblRegrind" runat="server" />
                    </b></span>
                </td>
                <td style="width: 120px">
                    <span style="vertical-align: middle;">
                        <asp:DropDownList ID="listRegrind" runat="server" Width="250px" Height="20px" CssClass="TextBoxBig"
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
                            <asp:Label ID="Label3" runat="server" /><asp:Label ID="lblMaxDigits" runat="server" /></em>
                        </span>
                    </div>
                    <asp:TextBox ID="txtQuantity" runat="server" MaxLength="15" Width="160px" CssClass="TextBox"
                        TabIndex="5" Height="45px" ToolTip="Current Weight" Font-Names="Consolas" Font-Size="14pt"></asp:TextBox>
                <td class="rTableCellError" height="90%" style="width: 350px; text-align: left;"
                    align="left">
                    <span style="vertical-align: middle; text-align: left;" />
                    <asp:RegularExpressionValidator ID="ValidateQuantity" runat="server" ForeControl="red"
                        ErrorMessage="Only numbers allowed and Quantity must be greater than zero" SetFocusOnError="true"
                        ControlToValidate="txtQuantity" ValidationExpression="[0-9]+(\.[0-9]{1,4})?"
                        Display="Dynamic" ForeColor="Red" Font-Names="Arial"></asp:RegularExpressionValidator>
                </td>
            </tr>
        </table>
    </div>
    <div style="padding: 1%; height: 35px; vertical-align: middle; width: 50%; text-align: center;"
        align="center">
        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="ButtonsSendSave" Width="107px"
            Height="24px" OnClick="btnSave_Click" TabIndex="6" />
        <span style="vertical-align: middle" />
    </div>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="hOrdenMachine" runat="server" />
    <asp:HiddenField ID="hidden" runat="server" />
    <asp:HiddenField ID="hi_indBodega" runat="server" />
    <asp:HiddenField ID="hi_indLote" runat="server" />
    <asp:HiddenField ID="hi_descItem" runat="server" />
    <asp:HiddenField ID="hi_unityItem" runat="server" />
    <asp:HiddenField ID="hi_machineItem" runat="server" />
    </span></span></span>
</asp:Content>
