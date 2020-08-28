<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="whInvLabelRegrind.aspx.cs"
    Inherits="whusap.WebPages.Labels.whInvLabelRegrind" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta name="viewport" content="width=300, user-scalable=no" />
    <style type="text/css">
        .td
        {
            font-size: medium;
            color: #000000;
            font-family: Arial;
            width: 4.8in;
            text-align: center;
        }
        #config
        {
            overflow: auto;
            margin-bottom: 10px;
        }
        .config
        {
            float: left;
            width: 200px;
            height: 250px;
            border: 1px solid #000;
            margin-left: 10px;
        }
        .config .title
        {
            font-weight: bold;
            text-align: center;
        }
        .config .barcode2D, #miscCanvas
        {
            display: none;
        }
        #submit
        {
            clear: both;
        }
        #barcodeTarget, #canvasTarget
        {
            margin-top: 20px;
        }
        
        .style4
        {
            width: 262px;
        }
        .style5
        {
            font-family: Arial;
        }
        .style6
        {
            width: 260px;
        }
        .style8
        {
            width: 240px;
        }
        .style9
        {
            font-family: Arial;
        }
        .style10
        {
            width: 240px;
            font-family: Arial;
        }
        .style11
        {
            width: 260px;
            font-family: Arial;
        }
        .style12
        {
            width: 262px;
            font-family: Arial;
        }
    </style>
    <script type="text/javascript">
        function printDiv(divID) {
            var divElements = document.getElementById(divID).innerHTML;
            var oldPage = document.body.innerHTML;
            document.body.innerHTML = "<html";
            document.body.innerHTML += "><head";
            document.body.innerHTML += "><title";
            document.body.innerHTML += "></title"
            document.body.innerHTML += "></head";
            document.body.innerHTML += "><body";
            document.body.innerHTML += ">" + divElements;
            document.body.innerHTML += "</body>";

            window.print();
            document.body.innerHTML = oldPage;
            //setTimeout(window.close(),15000);
        };

        function addZero(i) {
            if (i < 10) {
                i = "0" + i;
            }
            return i;
        };       
    </script>
</head>
<body onfocus="window.close();">
    <form id="form1" runat="server">
    <div id="Label" style="width: 5.8in; height: 3.68in">
        <table align="left" style="width: 5.8in; height: 3.5in" border="1" cellspacing="0"
            cellpadding="0">
            <tr align="center" style="height: 2%;">
                <td colspan="3" style="font-size: medium; color: #000000; font-weight: bold; height: 1px"
                    align="center">
                </td>
            </tr>
            <tr align="center" style="height: 4%;">
                <td colspan="3" style="font-size: medium; color: #000000; font-weight: bold; height: 10%"
                    align="center">
                    <img runat="server" id="imgItem" alt="" hspace="60" vspace="5" style="width: 2in;
                        height: .6in;" />
                </td>
            </tr>
            <tr align="center" style="height: 5%;">
                <td colspan="3" style="font-size: medium; color: #000000; font-weight: bold; height: 5%"
                    align="center">
                    <asp:Label ID="lblArticulo" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="font-size: medium; color: #000000; font-weight: bold; height: 9%"
                    align="center">
                    <img runat="server" id="imgConsec" alt="" hspace="60" vspace="5" style="width: 2in;
                        height: .6in;" />
                </td>
            </tr>
            <tr>
                <td class="style8" align="center">
                    &nbsp;<span class="style9"><strong>Work Order</strong></span>
                </td>
                <td class="style5" align="center" colspan="2">
                    <strong>Quantity</strong>
                </td>
            </tr>
            <tr>
                <td class="style8" align="center">
                    <asp:Label ID="lblWorkOrderLot" runat="server"></asp:Label>
                </td>
                <td class="style6" align="center">
                    <asp:Label ID="lblCant" runat="server"></asp:Label>
                </td>
                <td class="style4" align="center">
                    <img runat="server" id="imgQuantity" alt="" hspace="40" vspace="3" style="width: 1.64in;
                        height: 0.32in;" />
                </td>
            </tr>
            <tr>
                <td class="td" colspan="3" align="center" style="height: 1%;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style10" align="center">
                    <strong id="Date">Date</strong>
                </td>
                <td class="style11" align="center">
                    <strong>Unit</strong>
                </td>
                <td class="style12" align="center">
                    <asp:Label ID="lblTitleReprinted" runat="server" Width="130px" Font-Bold="True" 
                        Text="Reprinted Date"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style8" align="center">
                    <asp:Label ID="lblDate" runat="server" Width="60px"></asp:Label>
                </td>
                <td class="style6" align="center">
                    <asp:Label ID="lblUnit" runat="server"></asp:Label>
                </td>
                <td class="style4" align="center">
                    <asp:Label ID="lblReprinted" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td" colspan="3" align="center" style="height: 1%;">
                    &nbsp;
                </td>
            </tr>
            <tr align="center" style="height: 4%;">
                <td class="style11" align="center">
                    <strong>Shift</strong>
                </td>
                <td class="style12" align="center">
                    <strong>Machine</strong>
                </td>
                <td class="style11" align="center">
                    <strong>Operator</strong>
                </td>
            </tr>
            <tr align="center" style="height: 4%;">
                <td style="font-size: 10pt; color: #000000; font-weight: bold;" align="center" class="style8">
                    A, B, C, D
                </td>
                <td class="style11" align="center">
                    <asp:Label ID="lblMachine" runat="server"></asp:Label>
                </td>
                <td class="style11" align="center">
                    <asp:Label ID="lblUsuario" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div>
    </div>
    </form>
</body>
</html>
