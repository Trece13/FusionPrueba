<%@ Page aspcompat=true %>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must authenticate before you can execute this option."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>
<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<head>
    <style>
        * {
          color:#7F7F7F;
          font-family:Arial,sans-serif;
          font-size:12px;
          font-weight:700;
            height: 23px;
            width: 233px;
            margin-left: 0px;
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
        .style2
        {
            height: 4px;
        }
        .style3
        {
            height: 5px;
        }
        .style5
        {
            height: 5px;
            width: 201px;
        }
    </style>

<script type="text/javascript">
    function printDiv(divID) {
        //PRINT LOCAL HOUR
        var d = new Date();
        var x = document.getElementById("date");
        var h = addZero(d.getHours());
        var m = addZero(d.getMinutes());
        var s = addZero(d.getSeconds());
        //x.innerHTML = d.toUTCString();
        x.innerHTML = d.toLocaleString();

        //Get the HTML of div
        var divElements = document.getElementById(divID).innerHTML;
        //Get the HTML of whole page
        var oldPage = document.body.innerHTML;
        //Reset the page's HTML with div's HTML only
        document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
        //Print Page
        window.print();
        //Restore orignal HTML
        document.body.innerHTML = oldPage;
    }

    function addZero(i) {
        if (i < 10) {
            i = "0" + i;
        }
        return i;
    };
</script>

</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB">
<div id="label">
<form id="form1" runat="server" style="width: 5.8in; height: 3.8in">
<%
Dim strord, strsec, strrowcolor, strSQL, strSQL1, objrs, Odbcon, stritem, strdesci, struni, strfecha
Dim strmsg, strflag, strqty, strart, strobse, strreason, strcant, strlote, strdescr, strware, strloca, strcomm, strdispo

Session("url") = "1"
strflag = Request.QueryString("flag")
if strflag = "Y" then
    strart = session("item")
    strart = trim(strart)
    strobse = session("obse")
    strobse = trim(strobse)
    strreason = session("reason")
    strcant = session("cant")
    strlote = session("lote")
    strlote = trim(strlote)
    strware = session("warehouse")
    strloca = session("location")
   	strmsg = Session("strmsg")
else
    strart = session("item")
    strart =  trim(strart)
    strobse = session("obse")
    strobse = trim(strobse)
    strreason = session("reason")
    strcant = session("cant")
    strlote = session("lote")
    strlote = trim(strlote)
    strware = session("warehouse")
    strloca = session("location")

end if
	'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
strord = mid(strord,1,9)
if (strloca = "" and strlote="") then
    strSQL = "select trim(col116.t$item) item, trim(ibd001.t$dsca) desci, trim(ibd001.t$cuni) unidad, col116.t$qtyr cantidad, " & _ 
    " col116.t$clot lote, mcs005.t$dsca descr, substr(col116.t$obse,1,255) comentario, " & _
    " case when col116.t$disp = '2' then 'Recycle' " & _
    " when col116.t$disp = '3' then 'Regrind' " & _
    " when col116.t$disp = '4' then 'Review' " & _
    " when col116.t$disp = '5' then 'Good' end disposicion " & _
    " from baan.tticol116" & session("env") & " col116 " & _
    " inner join baan.ttcibd001" & session("env") & " ibd001 on ibd001.t$item = col116.t$item " & _
    " inner join baan.ttcmcs005" & session("env") & " mcs005 on mcs005.t$cdis = col116.t$cdis " & _
    " where trim(col116.t$item)='" + strart + "'" & _
    " and col116.t$cwar='" + strware + "'"
else
if (strloca = "") then
    strSQL = "select trim(col116.t$item) item, trim(ibd001.t$dsca) desci, trim(ibd001.t$cuni) unidad, col116.t$qtyr cantidad, " & _ 
    " col116.t$clot lote, mcs005.t$dsca descr, substr(col116.t$obse,1,255) comentario, " & _
    " case when col116.t$disp = '2' then 'Recycle' " & _
    " when col116.t$disp = '3' then 'Regrind' " & _
    " when col116.t$disp = '4' then 'Review' " & _
    " when col116.t$disp = '5' then 'Good' end disposicion " & _
    " from baan.tticol116" & session("env") & " col116 " & _
    " inner join baan.ttcibd001" & session("env") & " ibd001 on ibd001.t$item = col116.t$item " & _
    " inner join baan.ttcmcs005" & session("env") & " mcs005 on mcs005.t$cdis = col116.t$cdis " & _
    " where trim(col116.t$item)='" + strart + "'" & _
    " and trim(col116.t$clot)='" + strlote + "'" & _
    " and col116.t$cwar='" + strware + "'"
else
    if (strlote = "") then
        strSQL = "select trim(col116.t$item) item, trim(ibd001.t$dsca) desci, trim(ibd001.t$cuni) unidad, col116.t$qtyr cantidad, " & _ 
        " col116.t$clot lote, mcs005.t$dsca descr, substr(col116.t$obse,1,255) comentario, " & _
        " case when col116.t$disp = '2' then 'Recycle' " & _
        " when col116.t$disp = '3' then 'Regrind' " & _
        " when col116.t$disp = '4' then 'Review' " & _
        " when col116.t$disp = '5' then 'Good' end disposicion " & _
        " from baan.tticol116" & session("env") & " col116 " & _
        " inner join baan.ttcibd001" & session("env") & " ibd001 on ibd001.t$item = col116.t$item " & _
        " inner join baan.ttcmcs005" & session("env") & " mcs005 on mcs005.t$cdis = col116.t$cdis " & _
        " where trim(col116.t$item)='" + strart + "'" & _
        " and col116.t$cwar='" + strware + "'" & _
        " and col116.t$loca='" + strloca + "'"
    else
        strSQL = "select trim(col116.t$item) item, trim(ibd001.t$dsca) desci, trim(ibd001.t$cuni) unidad, col116.t$qtyr cantidad, " & _ 
        " col116.t$clot lote, mcs005.t$dsca descr, substr(col116.t$obse,1,255) comentario, " & _
        " case when col116.t$disp = '2' then 'Recycle' " & _
        " when col116.t$disp = '3' then 'Regrind' " & _
        " when col116.t$disp = '4' then 'Review' " & _
        " when col116.t$disp = '5' then 'Good' end disposicion " & _
        " from baan.tticol116" & session("env") & " col116 " & _
        " inner join baan.ttcibd001" & session("env") & " ibd001 on ibd001.t$item = col116.t$item " & _
        " inner join baan.ttcmcs005" & session("env") & " mcs005 on mcs005.t$cdis = col116.t$cdis " & _
        " where trim(col116.t$item)='" + strart + "'" & _
        " and trim(col116.t$clot)='" + strlote + "'" & _
        " and col116.t$cwar='" + strware + "'" & _
        " and col116.t$loca='" + strloca + "'"
    end if
end if
end if
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
    strdesci = objrs.Fields("desci").Value
    struni = objrs.Fields("unidad").Value
    strdescr = objrs.Fields("descr").Value
    strcomm = objrs.Fields("comentario").Value
    strcomm = trim(strcomm)
    strdispo = objrs.Fields("disposicion").Value
    Session("strmsg") = "Order was printed and saved successfully."
    'Desconectar a base de datos
%>
<table align="left" style="width: 5.8in; height: 3.8in" border="1" cellspacing="0" cellpadding="0">
<tr>
    <td class="style2"></td>
    <td style="font-size: small; color: #000000" align="center" class="style2">DEFECTIVE MATERIAL TAG</td>
    <td class="style2"></td>
</tr>
<tr>  
    <td colspan="3">
        <img align="middle" alt="Barcode Generator TEC-IT" hspace="0" 
            src="/Barcode/BarcodeHandler.ashx?data=<%=strart%>&amp;code=Code128&amp;dpi=96" 
            style="width: 4in; height: .3in; position: relative; top: 0px; left: 64px;" 
            vspace="0" /> </td>
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center" class="style2">Description</td> 
    <td colspan="2" style="font-size: small; color: #000000;" align="center" 
        class="style2"><%=strdesci%></td>  
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center" class="style2">Disposition - <%=strdispo%></td>
    <td style="font-size: small; color: #000000;" align="center" class="style2">Rejected Qty - <%=strcant%></td> 
    <td>
        <img align="middle" alt="Barcode Generator TEC-IT" hspace="0" 
            src="/Barcode/BarcodeHandler.ashx?data=<%=strcant%>&amp;code=Code128&amp;dpi=96" 
            style="width: 1in; height: .3in; position: relative; top: 0px; left:16px;" 
            vspace="0" /> </td>
</tr>
<tr>

    <td style="font-size: small; color: #000000;" align="center" class="style2">Printed By - <%=session("user")%></td>
    <td colspan="2" style="font-size: small; color: #000000;" align="center" 
        class="style2">
    <asp:Label ID="date" runat="server" Text="Date" Font-Bold="True" Font-Names="Arial" Font-Size="small" ForeColor="Black"></asp:Label></td>
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center" class="style2">Lot - <%=strlote%></td> 
    <td style="font-size: small; color: #000000;" align="center" class="style2">Reason</td> 
    <td style="font-size: small; color: #000000;" align="center" class="style2"><%=strdescr%></td> 
</tr>
<tr>  
    <td colspan="3" style="font-size: small; color: #000000;" 
        class="style2">Comments - <%=strcomm%></td>
</tr>

<!-- #include file="include/dbxconoff.inc" -->
<%
else
    Session("strmsg") = "Order doesn't register to MRB."
	Response.Redirect("whInvMaterialRejectedW.aspx?flag=") 
End if
%>
</table>

</form>
</div>
<div>
<table align="left" style="width: 5.8in; height: .25in" border="1" cellspacing="0" cellpadding="0">
<tr>
    <td><a href="#" onclick="javascript:printDiv('label')" style="color: #000000; font-size: medium">Print</a></td>
    <td><a href="whInvMaterialRejectedW.aspx"><img src="images/btn_Exit.jpg"/></a></td>
</tr>
</table>
</div>
</body>
</html>
