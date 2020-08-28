<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="whInvPrintLabelPhysical.aspx.cs" Inherits="whusap.WebPages.Labels.whInvPrintLabelPhysical" %>

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
        }
                
        .style2
        {
            font-size: 10pt;
            color: #000000;
        }
                
        .style3
        {
            width: 25%;
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

    <table align="left" style="width: 5.8in; height: 3.5in" border="1" cellspacing="0" cellpadding="0">
       <tr align="center" style="height:5%;">
          <td colspan="3" 
               style="font-size: medium; color: #000000; font-weight:bold; height: 1px" 
               align="center"></td>
       </tr>

       <tr align="center" style="height:5%;">
          <td colspan="3" 
               style="font-size: medium; color: #000000; font-weight:bold; height: 10%" 
               align="center">LABEL FOR PHYSICAL INVENTORY</td>
       </tr>
       <tr style="height:24%;">
          <td class="style1" colspan="3" align="center">
              <img runat="server" id="imgLabelid" alt="" hspace="60" vspace="5" style="width: 2in; height: .6in;"/>
          </td>
       </tr>
       <tr style="height:29%;">
          <td class="style1" colspan="3" align="center" style="height:50px">
              <img runat="server" id="imgItem" alt="" hspace="60" vspace="5" style="width: 2in; height: .6in;"/>
          </td>
       </tr>
       <tr style="height:10%;">
          <td class="td" colspan="3" align="center" style="height:5%;">
              <asp:Label ID="lblArticulo" runat="server" ></asp:Label>
          </td>
       </tr>
       <tr align="center" style="height:5%;">
          <td colspan="3" align="center" class="style2">QUANTITY :
              <asp:Label ID="lblQuantity" runat="server" ></asp:Label>&nbsp;
              <asp:Label ID="lblUnity" runat="server" ></asp:Label>&nbsp;<img runat="server" id="imgCant" alt="" hspace="60" vspace="5" 
                  style="width: 2in; height: .3in;"/></td>
       </tr>
       <tr align="center" style="height:4%;">
          <td style="font-size:10pt; color: #000000; font-weight: bold; width:15%;" align="center">
              <img runat="server" id="imgLot" alt="" hspace="60" vspace="5" 
                  style="width: 2in; height: .3in;"/></td>
          <td style="font-size:10pt; color: #000000; font-weight: bold; width:10%;" align="center">
              DATE</td>
          <td style="font-size:10pt; color: #000000; font-weight: bold; " align="center">
              <asp:Label ID="lblDate" runat="server"  Width="30px"></asp:Label>
           </td>
       </tr>

    </table>
    
    </div>
    </form>
</body>
</html>
