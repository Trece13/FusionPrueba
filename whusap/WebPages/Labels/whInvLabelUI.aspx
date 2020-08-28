<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="whInvLabelUI.aspx.cs" Inherits="whusap.WebPages.Labels.whInvLabelUI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=300, user-scalable=no" />
    <style type="text/css">
      .td{
           font-size: medium; 
           color: #000000; 
           font-family: Arial; 
           width: 4.8in;
           text-align:center;
        } 
      #config{
          overflow: auto;
          margin-bottom: 10px;
      }
      .config{
          float: left;
          width: 200px;
          height: 250px;
          border: 1px solid #000;
          margin-left: 10px;
      }
      .config .title{
          font-weight: bold;
          text-align: center;
      }
      .config .barcode2D,
      #miscCanvas{
        display: none;
      }
      #submit{
          clear: both;
      }
      #barcodeTarget,
      #canvasTarget{
        margin-top: 20px;
      }        
        
        .style1
        {
            width: 4.52in;
            height: 51px;
        }
        .style2
        {
            font-size: medium;
            color: #000000;
            font-family: Arial;
            width: 4.52in;
            text-align: center;
        }
        
        .style3
        {
            font-size: medium;
            color: #000000;
            font-family: Arial;
            text-align: center;
        }
        
        .style4
        {
            height: 76px;
        }
        .style5
        {
            font-size: medium;
            color: #000000;
            font-family: Arial;
            width: 4.8in;
            text-align: center;
            height: 51px;
        }
        .style6
        {
            font-size: medium;
            color: #000000;
            font-family: Arial;
            width: 4.8in;
            text-align: center;
            height: 33px;
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
    };

    function addZero(i) {
        if (i < 10) {
            i = "0" + i;
        }
        return i;
    };


</script>
</head>
<body>
  <form id="form1" runat="server">
    <div id="Label" style="width: 6in; height: 4in">
        <fieldset>
            <table align="left" style="width: 6in; height: 4in;" border="1" cellspacing="0" cellpadding="0">
                <tr>
                    <td colspan="2" style="font-weight:bold; text-align:center;"><asp:Label runat="server" ID="lblEncabezado"></asp:Label></td>
                </tr>
                <tr>
                    <td style="width:50%;text-align:right; border-right:0px; padding:2px;"><asp:Label Text="" runat="server" ID="lblReprint" /></td>
                    <td style="width:30%; border-left:0px; padding:2px;">
                        <asp:Label Text="" runat="server" ID="lblHora" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblTruck"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Text="" runat="server" ID="lblTruckId" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblSource"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Text="" runat="server" ID="lblSourceWarehouse" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblDestination"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Text="" runat="server" ID="lblDestinationWarehouse" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblDateID"></asp:Label>
                    </td>
                    <td>
                        <asp:Label Text="" runat="server" ID="lblDateUID" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center;">
                        <img src="~/images/logophoenix_login.jpg" runat="server" id="imgUniqueID" alt="" hspace="60" vspace="5" style="width: 3in; height: .6in;"/>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
</form>
</body>
</html>