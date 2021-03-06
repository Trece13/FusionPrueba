﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MDMasterPage.Master" AutoEventWireup="true" CodeBehind="whInvConfirmedRegrind.aspx.cs" Inherits="whusap.WebPages.Balance.whInvConfirmedRegrind" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Encabezado" runat="server">
    <script src="<%= ResolveUrl("~/Scripts/jquery-1.4.1.min.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        $("input[id$=Contenido_txtQuantity]").focus(function () {
            $("div[id$=Contenido_tooltip]").show();
        });

        $("input[id$=Contenido_txtQuantity]").blur(function () {
            $("div[id$=Contenido_tooltip]").hide();
        });

    </script>
    <style type="text/css">
        .style3
        {
            height: 22px;
        }
        .style4
        {
            width: 80%;
            height: 22px;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <span style="vertical-align: middle" />

    <div style="height: 15px" align="center">
    </div>

    <div align="center" id="divTabla" runat="server" style="width: 80%">
        <table style="width: 80%" >
            <tr>
                <td class="style8">&nbsp;</td>
                <td style="width: 120px">&nbsp;</td>
                <td class="rTableCellError" style="width: 50%" height="350px">&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <span style="vertical-align: middle" />
                    <span class="style2" style="vertical-align: middle;">
                        <b style="font-size: 11px">
                            <asp:Label ID="lblRegrind" runat="server"></asp:Label>
                        </b>
                    </span>
                </td>
                <td style="width: 120px">
                    <span style="vertical-align: middle;">
                        <asp:TextBox ID="txtSQNB" runat="server" MaxLength="15" Width="180px" CssClass="TextBox" TabIndex="5" Height="30px" ToolTip="Current Weight" Font-Names="Consolas" Font-Size="14pt"></asp:TextBox>
                    </span>
                </td>
                <td class="rTableCellError" height="90%" style="width: 350px; text-align: left;" align="left">
                    <span style="vertical-align: middle; text-align: left;" />
                    <asp:Label ID="lblIngreso" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center;">
                    <hr />  
                    <asp:Button ID="btnQuery" runat="server" Text="Query" CssClass="ButtonsSendSave" Width="107px" Height="24px" OnClick="btnQuery_Click" TabIndex="6" />
                </td>
            </tr>
        </table>

        <div style="padding: 1%; height: 35px; vertical-align: middle; width: 50%; text-align: center;" align="center">
            
            <span style="vertical-align: middle" />
        </div>

        <div style="padding: 1%; height: 20px; vertical-align: middle; width: 70%; text-align: center;" align="center">
            <asp:Label ID="lblResult" runat="server" CssClass="style2" Font-Bold="True" Font-Italic="True"></asp:Label>
            <br />
            <asp:Label ID="lblError" runat="server" Text="" CssClass="lblMessage" Width="80%" Height="20px" Visible="false"></asp:Label>
        </div>

        <br />

        <div id="printerDiv" runat="server" style="color: Red;">
            <span style="vertical-align: middle" />
            <table style="width: 60%" border="1px">
                <tr>
                    <td class="style8">
                        <span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px"> 
                                <asp:Label ID="lblDescWorkOrder" runat="server"></asp:Label>
                            </b>
                        </span>
                    </td>
                    <td style="width: 80%">
                        <asp:Label ID="lblWorkOrder" runat="server" Text=""  CssClass="style2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px"> 
                                <asp:Label ID="lblWarehouse" runat="server"></asp:Label>
                            </b>
                        </span>
                    </td>
                    <td class="style4">
                        <asp:Label ID="lblWareh" runat="server" Text=""  CssClass="style2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="vertical-align: middle" />
                        <span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px">
                                <asp:Label ID="lblRegrind2" runat="server"></asp:Label>
                            </b>
                        </span>
                    </td>
                    <td style="width: 120px">
                        <asp:Label ID="lblRegrindSequence" runat="server" Text="" CssClass="style2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="vertical-align: middle" />
                        <span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px">
                                <asp:Label ID="lblDescRegrindCode" runat="server" />
                            </b>
                        </span>
                    </td>
                    <td style="width: 120px">
                        <asp:Label ID="lblRegrindCode" runat="server" Text="" CssClass="style2"></asp:Label>
                        <br />
                        <asp:Label ID="lblRegrindDesc" runat="server" Text="" CssClass="style2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="vertical-align: middle" />
                        <span class="style2" style="vertical-align: middle;">
                            <b style="font-size: 11px">
                                <asp:Label ID="lblQuantityConfirm" runat="server" />
                            </b>
                        </span>
                    </td>
                    <td style="width: 120px">
                       <asp:Label ID="lblQuantity" runat="server" Text="" CssClass="style2"></asp:Label>
                        &nbsp;
                       <asp:Label ID="lblUnit" runat="server" Text="" CssClass="style2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center;">
                        <hr />
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="ButtonsSendSave" Width="107px" Height="24px" TabIndex="6" Visible="false" onclick="btnSave_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <asp:HiddenField ID="hidden" runat="server" />
    <asp:HiddenField ID="hOrdenMachine" runat="server" />
    </span></span></span>
</asp:Content>
