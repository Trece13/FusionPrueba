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
        .style1
        {
            height: 8px;
        }
        .style2
        {
            height: 23px;
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
Dim strord, strrowcolor, strSQL, strSQL1, objrs, Odbcon, stritem, strdesci, struni, strfecha
Dim strmsg, strflag, strqty, strart, strware, strlote, strcant, strfactorkg, strcantot, strartr, strmlot

Session("url") = "1"
strflag = Request.QueryString("flag")
if strflag = "Y" then
	strord = session("orden")
    strart = session("item")
    strartr = session("vitemr")
    strartr = trim(strartr)
    strware = session("ware")
    strlote = session("lot")
    strlote = trim(strlote)
    strcant = session("cant")
    strmlot = session("mlot")
   	strmsg = Session("strmsg")
else
  	strord = session("orden")
    strart = session("item")
    strartr = session("vitemr")
    strartr = trim(strartr)
    strware = session("ware")
    strlote = session("lot")
    strlote = trim(strlote)
    strcant = session("cant")
    strmlot = session("mlot")
	Session("strord")  = strord 
end if
	'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%

strord = mid(strord,1,9)

if (strmlot = 1) then
    strSQL = "select trim(col118.t$item) item, trim(dbi001.t$dsca) desci, trim(dbi001.t$cuni) unidad, col118.t$qtyr cantidad, " & _ 
    " col118.t$clot lote, mcs005.t$dsca descr, substr(col118.t$obse,1,255) comentario, col020.t$pent factorkg " & _
    " from baan.tticol118" & session("env") & " col118 " & _
    " inner join baan.ttcibd001" & session("env") & " ibd001 on ibd001.t$item = col118.t$item " & _
    " inner join baan.ttcibd001" & session("env") & " dbi001 on dbi001.t$item = col118.t$ritm " & _
    " inner join baan.ttccol020" & session("env") & " col020 on col020.t$item = col118.t$item " & _
    " inner join baan.ttcmcs005" & session("env") & " mcs005 on mcs005.t$cdis = col118.t$cdis " & _
    " where trim(col118.t$item)=trim('" + strart + "')" & _
    " and trim(col118.t$clot)='" + strlote + "'" & _
    " and col118.t$cwar='" + strware + "'"
else
        strSQL = "select trim(col118.t$item) item, trim(dbi001.t$dsca) desci, trim(dbi001.t$cuni) unidad, col118.t$qtyr cantidad, " & _ 
    " col118.t$clot lote, mcs005.t$dsca descr, substr(col118.t$obse,1,255) comentario, col020.t$pent factorkg " & _
    " from baan.tticol118" & session("env") & " col118 " & _
    " inner join baan.ttcibd001" & session("env") & " ibd001 on ibd001.t$item = col118.t$item " & _
    " inner join baan.ttcibd001" & session("env") & " dbi001 on dbi001.t$item = col118.t$ritm " & _
    " inner join baan.ttccol020" & session("env") & " col020 on col020.t$item = col118.t$item " & _
    " inner join baan.ttcmcs005" & session("env") & " mcs005 on mcs005.t$cdis = col118.t$cdis " & _
    " where trim(col118.t$item)=trim('" + strart + "')" & _
    " and col118.t$cwar='" + strware + "'"
end if
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
    stritem = objrs.Fields("item").Value
    strdesci = objrs.Fields("desci").Value
    struni = objrs.Fields("unidad").Value
    strqty = objrs.Fields("cantidad").Value
    strfactorkg = objrs.Fields("factorkg").Value
    strcantot = formatnumber((strqty * strfactorkg) / 2.20462000, 2)

    Session("strmsg") = "Order was printed and saved successfully."
    'Desconectar a base de datos
%>
<table align="left" style="width: 5.8in; height: 3.8in" border="1" cellspacing="0" cellpadding="0">
<tr>
    <td colspan="2">
        <img align="middle" alt="Barcode Generator TEC-IT" hspace="0" 
            src="/Barcode/BarcodeHandler.ashx?data=<%=strartr%>&amp;code=Code128&amp;dpi=96" 
            style="width: 4in; height: .5in; position: relative; top: 0px; left: 64px;" 
            vspace="0" /> </td>
</tr>
<tr>
    <td colspan="2" style="font-size: small; color: #000000;" align="center" class="style1"><%=strdesci%></td>  
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center" class="style1">Total Qty Rejected</td> 
    <td>
        <img align="middle" alt="Barcode Generator TEC-IT" hspace="0" 
            src="/Barcode/BarcodeHandler.ashx?data=<%=strcantot%>&amp;code=Code128&amp;dpi=96" 
            style="width: 1in; height: .5in; position: relative; top: 0px; left: 80px;" 
            vspace="0" /> </td>
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center" class="style20"><%=strcantot%></td> 
    <td style="font-size: small; color: #000000;" align="center" class="style20">Unit - <%=struni%></td> 
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center" class="style17">Workorder</td>
    <td style="font-size: small; color: #000000;" align="center" class="style17">Date</td> 
</tr>
<tr>

    <td style="font-size: small; color: #000000;" align="center" class="style1"><%=strord%></td>
    <td style="font-size: small; color: #000000;" align="center" class="style1">
    <asp:Label ID="date" runat="server" Text="Date" Font-Bold="True" Font-Names="Arial" Font-Size="x-small" ForeColor="Black"></asp:Label></td>
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center" class="style1">Printed By</td> 
    <td style="font-size: small; color: #000000;" align="center" class="style1">ORIGIN</td> 
</tr>
<tr>  
    <td style="font-size: x-small; color: #000000;" align="center" class="style1"><%=session("user")%></td> 
    <td style="font-size: x-small; color: #000000;" align="center" class="style1"></td> 
</tr>
<!-- #include file="include/dbxconoff.inc" -->
<%
else
    Session("strmsg") = "Order doesn't register to MRB."
	Response.Redirect("whInvMaterialRejectedD.aspx?flag=") 
End if
%>
</table>
</form>
</div>
<div>
<table align="left" style="width: 5.8in; height: .25in" border="1" cellspacing="0" cellpadding="0">
<tr>
    <td><a href="#" onclick="javascript:printDiv('label')" style="color: #000000; font-size: medium">Print</a></td>
    <td><a href="whInvMaterialRejectedD.aspx"><img src="images/btn_Exit.jpg"/></a></td>
</tr>
</table>
</div>
</body>
</html>
