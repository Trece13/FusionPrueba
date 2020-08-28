<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Label_genericSemiFinish.aspx.cs" Inherits="whusap.WebPages.Labels.GenericProducts.Label_genericSemiFinish" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
        
        .style5
        {
            width: 185px;
            font-family: Arial;
        }
        .style9
        {
            font-family: Arial;
        }
        .style13
        {
            width: 142px;
            height: 76px;
        }
        .style14
        {
            width: 185px;
        }
        .style15
        {
            font-size: medium;
            color: #000000;
            font-family: Arial;
            width: 518px;
            text-align: center;
            height: 1%;
        }
                
        .style16
        {
            text-align: left;
            width: 129px;
        }
                
        .style17
        {
            width: 179px;
        }
        .style18
        {
            height: 10%;
            width: 179px;
        }
        .style19
        {
            height: 5%;
            width: 179px;
        }
        .style20
        {
            width: 179px;
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

    <table align="left" style="width: 5.8in; height: 3.6in" border="1" cellspacing="0" cellpadding="0">
       <tr align="center">
          <td style="border-style: none; font-size: medium; color: #000000; font-weight:bold; " 
               align="center" class="style17">
              <img alt="" class="style13" height="25px" 
                  src="http://localhost:21797/images/logophoenix_s.jpg" width="25px" /></td>
          <td colspan="2" 
               style="border-style: none; font-size: medium; color: #000000; font-weight:bold; " 
               align="center">
              <asp:Label ID="lblArticulo" runat="server" Font-Names="Arial" Font-Size="12pt" ></asp:Label>              
          </td>
       </tr>

       <tr align="center" style="height:5%;">
          <td colspan="1" 
               style="border-style: none; font-size: medium; color: #000000; font-weight:bold; " 
               align="center" class="style18">
              <img runat="server" id="imgOrden" alt="" hspace="60" vspace="5" style="width: 2in; height: .6in;"/>
          </td>
          <td colspan="2" 
               style="border-width: 1px; border-color: #333333; border-style: solid none solid solid; font-size: medium; color: #000000; font-weight:bold; " 
               align="center" rowspan="3">
              <table width="95%" style="height: 98%">
                <tr>
                  <td class="style16"><strong>UNID X CAJA:</strong></td>
                  <td><asp:Label ID="lblUnit" runat="server" Width="60px" Font-Names="Arial" 
                          Font-Size="12pt"></asp:Label></td>
                </tr>
                <tr>
                  <td class="style16"><strong>CAJA N.</strong></td>
                  <td><asp:Label ID="lblConsec" runat="server" Width="60px" Font-Names="Arial" 
                          Font-Size="12pt"></asp:Label></td>
                </tr>
                <tr>
                  <td class="style16"><strong>COD.OPER.:</strong></td>
                  <td><asp:Label ID="lblCodOper" runat="server" Width="60px" Font-Names="Arial" 
                          Font-Size="12pt"></asp:Label></td>
                </tr>
                <tr>
                  <td class="style16"><strong>TURNO</strong></td>
                  <td><asp:Label ID="lblTurno" runat="server" Width="60px" Font-Names="Arial" 
                          Font-Size="12pt"></asp:Label></td>
                </tr>
                <tr>
                  <td class="style16"><strong>COD.ENCAR.:</strong></td>
                  <td><asp:Label ID="lblCodEncar" runat="server" Width="60px" Font-Names="Arial" 
                          Font-Size="12pt"></asp:Label></td>
                </tr>
                <tr>
                  <td class="style16"><strong>FECHA:</strong></td>
                  <td><asp:Label ID="lblFecha" runat="server" Width="60px" Font-Names="Arial" 
                          Font-Size="12pt"></asp:Label></td>
                </tr>
              </table>
          </td>
       </tr>

       <tr align="center" style="height:5%;">
          <td style="border-style: none; font-size: medium; color: #000000; font-weight:bold; " 
               align="center" class="style19">
              &nbsp;</td>

       </tr>
       <tr>
          <td style="border-style: none; font-size: medium; color: #000000; font-weight:bold; " align="center" class="style18">
              <img runat="server" id="imgItem" alt="" hspace="60" vspace="5" style="width: 2in; height: .6in;"/>
          </td>
       </tr>
       <tr>
          <td class="style17" align="center" style="border-style: none">
              &nbsp;<span class="style9"></span></td>
          <td class="style5" align="center" style="border-style: none">
              &nbsp;</td>
          <td class="style5" align="center" style="border-style: none">
              &nbsp;</td>
       </tr>
       <tr>
          <td class="style17" align="center" style="border-style: none">
              <asp:Label ID="lblDate" runat="server" Width="60px"></asp:Label>
          </td>
          <td class="style14" align="center" style="border-style: none">
              &nbsp;</td>
          <td class="style14" align="center" style="border-style: none">
              <asp:Label ID="lblTime" runat="server" ></asp:Label>
          </td>

       </tr>
       <tr><td class="style15" colspan="3" align="center" style="border-style: none">
              &nbsp;
           </td>
       </tr>
       <tr>
          <td class="style20" align="center" style="border-style: none">
              &nbsp;</td>
          <td class="style5" align="center" style="border-style: none">
              &nbsp;</td>
          <td class="style5" align="center" style="border-style: none">
              &nbsp;</td>
       </tr>

       <tr align="center" style="height:4%;">
          <td style="border-style: none;" align="center" 
               class="style17">
              &nbsp;</td>
          <td class="style5" align="center" style="border-style: none">
              &nbsp;</td>
          <td class="style5" align="center" style="border-style: none">
              &nbsp;</td>
       </tr>

    </table>
    
    </div>
    <div>
    
    </div>
    </form>
</body>
</html>
