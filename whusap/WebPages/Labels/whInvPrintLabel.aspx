<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="whInvPrintLabel.aspx.cs" Inherits="whusap.WebPages.Labels.whInvPrintLabel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        
    </style>

    <script type="text/javascript">
        function printDiv(divID) {

            var monthNames = [
                "1", "2", "3",
                "4", "5", "6", "7",
                "8", "9", "10",
                "11", "12"
              ];

            //PRINT LOCAL HOUR
            var d = new Date();

            var LbdDate = document.getElementById("lblDate");
            LbdDate.innerText =
                monthNames[d.getMonth()] +
                "/" +
                d.getDate() +
                "/" +
                d.getFullYear() +
                " " +
                d.getHours() +
                ":" +
                d.getMinutes() +
                ":" +
                d.getSeconds()
                ;

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
    <div id="Label" style="width: 5.8in; height: 3.8in">

    <table align="left" style="width: 5.8in; height: 3.8in" border="1" cellspacing="0" cellpadding="0">
       <tr align="center" >
          <td colspan="5" style="font-size: medium; color: #000000;" align="center">MATERIAL RETURN TAG</td>
       </tr>
    <tr align="center" >
         <td align="center" class="td">PALLET ID</td>
          <td colspan="4" style="font-size: medium; color: #000000;" align="center">
          <img runat="server" id="imgPalletId" alt="" hspace="60" vspace="5" 
                  style="width: 2in; height: 0.4in;"/>
          </td>
       </tr>      
       <tr>
          <td align="center" class="td">WORK ORDER</td>
          <td align="center" class="td"><asp:Label ID="LblWorkOrder" runat="server" ></asp:Label></td>
          <td class="style1" colspan="3">
              <img runat="server" id="imgWorkOrder" alt="" hspace="60" vspace="5" style="width: 2in; height: .3in;"/>
          </td>
       </tr>
       <tr>
          <td align="center" class="td">LOT CODE</td>
          <td align="center" class="td"><asp:Label ID="lblLotCode" runat="server" ></asp:Label></td>
          <td class="style1" colspan="3">
              <img runat="server" id="imgLotCode" alt="" hspace="60" vspace="5" style="width: 2in; height: .3in;" />
          </td>
       </tr>
       <tr>
          <td colspan="5">
              <img runat="server" id="imgItem" hspace="80" vspace="5" style="width: 4in; height: .5in;"/>
          </td>
       </tr> 
       <tr>
         <td colspan="5" class="td" align="center"><asp:Label ID="lblItem" runat="server" ></asp:Label></td>
       </tr>
       <tr>
         <td colspan="5" class="td" align="center"><asp:Label ID="lblArticulo" runat="server" ></asp:Label></td>
       </tr>
       <tr>
          <td class="td" align="center">QUANTITY</td>
          <td class="td" align="center"><asp:Label ID="lblQuantity" runat="server" ></asp:Label></td>
          <td class="td" align="center"></td>
          <td class="td" align="center">UNIT</td>
          <td class="td" align="center"><asp:Label ID="lblUnit" runat="server" ></asp:Label></td>
       </tr>
       <tr>
          <td class="style3" align="center">DATE</td>
          <td class="td" align="left" width="90"><asp:Label ID="lblDate" runat="server" 
                  style="font-size: small" ></asp:Label></td>
          <td class="style2" align="center"><asp:Label ID="lblStatPrint" runat="server" style="font-size: small" ></asp:Label></td>
          <td class="style2" align="center">USER</td>
          <td class="td" align="center"><asp:Label ID="lblUser" runat="server" ></asp:Label></td>
       </tr>
    </table>
    
  </div>
 </form>
</body>
</html>
