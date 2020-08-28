<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="whInvLabelMaterialRejectedD.aspx.cs" Inherits="whusap.WebPages.Labels.whInvLabelMaterialRejectedD" %>

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
    function addZero(i) {
            if (i < 10) {
                i = "0" + i;
            }
            return i;
        };

    function printDiv(divID) {



        var monthNames = [
                    "1", "2", "3",
                    "4", "5", "6", "7",
                    "8", "9", "10",
                    "11", "12"
                  ];

        //PRINT LOCAL HOUR
        var d = new Date();
        var LbdDate = lblDate = document.getElementById("lblDate");
        LbdDate.innerHTML =  monthNames[d.getMonth()] +
            "/" +
            d.getDate() +
            "/" +
            d.getFullYear() +
            " - " +
            addZero(d.getHours()) +
            ":" +
            addZero(d.getMinutes()) +
            ":" +
            addZero(d.getSeconds());
        

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

    


</script>
</head>
<body onfocus="window.close();">
  <form id="form1" runat="server">
    <div id="Label" style="width: 5.8in; height: 3.8in">

    <table align="left" style="width: 5.8in; height: 3.8in" border="1" cellspacing="0" cellpadding="0">
    <tr>
        <td colspan="5" class="style4">
              <img runat="server" id="imgItem" hspace="80" vspace="5" style="width: 4in; height: .5in;"/>
        </td>
    </tr> 
    <tr>
         <td colspan="5" class="td" align="center"><asp:Label ID="lblArticulo" runat="server" ></asp:Label></td>
    </tr>
    <tr>
        <td align="center" class="style5" colspan = 2>TOTAL QTY REJECTED</td>
        <td class="style1" colspan="3">
              <img runat="server" id="imgQuantity" alt="" hspace="60" vspace="5" style="width: 2in; height: .3in;"/>
        </td>
    </tr>
    <tr>
        <td align="center" class="style6" colspan = 2><asp:Label ID="lblQuantity" runat="server" 
                  style="font-size: small" ></asp:Label></td>
        <td align="center" class="style6" colspan = 3>Unit - <asp:Label ID="lblUnit" runat="server" ></asp:Label></td>
    </tr>
    <tr>
        <td align="center" class="style6" colspan = 2>Work Order</td>
        <td align="center" class="style6" colspan = 3>Date</td>
    </tr>
    <tr>
        <td align="center" class="style6" colspan = 2><asp:Label ID="lblOrder" runat="server" 
                  style="font-size: small" ></asp:Label></td>
        <td class="td" align="left" colspan = 3><asp:Label ID="lblDate" runat="server" 
                  style="font-size: small" ></asp:Label></td>
    </tr>
    <tr>
        <td class="td" align="center">Printed By</td>
        <td class="td" align="center"><asp:Label ID="lblUser" runat="server" ></asp:Label></td>
        <td class="td" align="center"></td>
        <td class="td" align="center">ORIGIN</td>
        <td class="td" align="center"></td>
    </tr>
    <tr>
        <td class="td" align="left" colspan="5">
            <asp:Label ID="Label1" runat="server" 
                       style="font-size: small" Text="Comment:"></asp:Label>
            <asp:Label ID="LblComment" runat="server" 
                       style="font-size: small"  ></asp:Label>
        </td>
        
    </tr>
</table>
</div>
</form>
</body>

</html>
