<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvOpenFiles.aspx.cs" Inherits="whusap.WebPages.Archivos.whInvOpenFiles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True" EnablePageMethods="true">
    </asp:ScriptManager>

    <br />

    <div>
        <span>
            <div id="printResult" style="width:80%" runat="server"  align="center">
                <span style="text-align:right">
                    <asp:Label ID="lblResult" runat="server" CssClass="style2" Font-Bold="True" Font-Italic="True"></asp:Label>
                    <br />
                    <span style="vertical-align:middle" />
                    <asp:Label ID="lblError" runat="server" Text="" CssClass="lblMessage" Visible="false"></asp:Label>
                </span>
            </div>
        </span>
        <br />
        <br />
    </div>

    <div id="printerDiv" runat="server" style="color:Red;" >
        <span style="vertical-align:middle" />
        <table style="width: 60%">
            <tr>
                <td class="style8">
                    <span class="style2" style="vertical-align: middle;">
                        <b style="font-size:11px">
                            <asp:Label ID="lblMachine" runat="server" /> 
                        </b>
                    </span>
                </td>
                <td style="width: 120px">
                    <span>
                        <span style="vertical-align: middle">
                            <span style="vertical-align:middle" />
                            <asp:TextBox ID="txtMachine" runat="server" AutoPostBack="True" ontextchanged="txtMachine_listMachine" CssClass="TextboxBig"></asp:TextBox>
                        </span>
                    </span>
                </td>
                <td >
                    <asp:Button ID="btnChangeMac" runat="server" Text="Change Machine" CssClass="ButtonsSendSave" style="height:30px;" Enabled="False" onclick="btnChangeMac_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <span style="vertical-align:middle" />
                    <span class="style2" style="vertical-align: middle;">
                        <b style="font-size:11px">
                            &nbsp;<asp:Label ID="lblDirectory" runat="server" />
                        </b>
                    </span>
                </td>
                <td>
                    <span style="vertical-align:middle" />
                    <span style="vertical-align: middle">
                        <asp:TextBox CssClass="TextboxBig" ID="txtDirectory" runat="server" AutoPostBack="True" Enabled="False">C:\</asp:TextBox>
                    </span>
                </td>
                <td>
                    <span style="vertical-align:middle" />
                    <asp:Button ID="btnChangeDir" runat="server" Text="Change Directory" CssClass="ButtonsSendSave" style="height:30px;" Enabled="False" onclick="btnChangeDir_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align:center;">
                    <hr />
                    <asp:Button ID="btnSave" runat="server" Text="Open Machine File" CssClass="ButtonsSendSave" style="height:30px;" onclick="btnSave_Click" TabIndex="6" /> 
                </td>
            </tr>
        </table>
    </div>
    <div style="padding: 1%; height:35px; vertical-align:middle; width:50%; text-align: center;" align="center">
        
        <span style="vertical-align:middle" />
    </div>

</asp:Content>
