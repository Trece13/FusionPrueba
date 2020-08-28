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
          height: 22px;
            width: 242px;
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
            height: 22px;
        }
    </style>

<script type="text/javascript">
    function printDiv(divID) {
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
    };

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
<form id="form1" runat="server" style="width: 5.8in; height: 3.8in">
<div id="label">
    <%
Dim strord, strseq, strsec, strrowcolor, strSQL, strSQL1, objrs, Odbcon, stritem, strdesci, struni, strmaq, strfecha, strweight
Dim strmsg, strflag, strmachine, struser, strqtyplt, sec, sec1, weight

Session("url") = "1"
strflag = Request.QueryString("flag")
if strflag = "Y" then
	strord = ucase(trim(Request.Form("orden")))
    strseq = ucase(trim(Request.Form("seq")))
    strweight = trim(Request.Form("weight"))
	strmsg = Session("strmsg")
else
	strord = ucase(trim(Request.Form("orden")))
    strseq = ucase(trim(Request.Form("seq")))
    strweight = trim(Request.Form("weight"))
	strmsg = Session("strmsg")
end if
	'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
    <%
'Buscar la Orden en las tablas ticol080 y ticol081
strSQL = "select item, desci, orden, cantidad, unidad, usuario, maquina, fecha " & _
" from ( " & _
" select  col080.t$item item, ibd001.t$dsca desci, col080.t$orno orden, col080.t$qune cantidad, ibd001.t$cuni unidad, " & _
" col080.t$logn usuario, sfc010.t$mcno maquina, col080.t$date fecha " & _
" from    baan.tticol080" & session("env") & " col080 " & _
" inner join baan.ttcibd001" & session("env") & " ibd001 on ibd001.t$item = col080.t$item " & _
" inner join baan.ttisfc010" & session("env") & " sfc010 on sfc010.t$pdno = col080.t$orno and sfc010.t$opno = 10 " & _
" union " & _
" select  col081.t$item item, ibd001.t$dsca desci, col081.t$orno orden, col081.t$qune cantidad, ibd001.t$cuni unidad, " & _
" col081.t$logn usuario, sfc010.t$mcno maquina, col081.t$date fecha " & _
" from    baan.tticol081" & session("env") & " col081 " & _
" inner join baan.ttcibd001" & session("env") & " ibd001 on ibd001.t$item = col081.t$item " & _
" inner join baan.ttisfc010" & session("env") & " sfc010 on sfc010.t$pdno = col081.t$orno and sfc010.t$opno = 10 )" & _  
" where orden = '" + strord + "'" & _
" and abs(cantidad) = abs('" + strweight + "')"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
	strord = objrs.Fields("orden").Value
    weight =  objrs.Fields("cantidad").Value
    weight = Replace(weight,",",".")
    stritem = trim(objrs.Fields("item").Value)
    strdesci = objrs.Fields("desci").Value
    struni = objrs.Fields("unidad").Value
    strmaq = objrs.Fields("maquina").Value
    strfecha = objrs.Fields("fecha").Value
    struser = objrs.Fields("usuario").Value

    'Desconectar a base de datos
%>
<table align="left" style="width: 5.8in; height: 3.8in" border="1" 
    cellspacing="0" cellpadding="0">
    <!--
<tr><td colspan="2" align="center"><IMG SRC = "images/logophoenix_s.jpg" hspace="10" vspace="10" ></td></tr>
-->
<tr>
    <td colspan="3">		
	<img src='/Barcode/Barcodehandler.ashx?data=<%=stritem%>&code=Code128&dpi=96' 
            alt='Barcode Generator TEC-IT' align="middle" hspace="80" vspace="5" 
            style="width: 4in; height: .5in;"/>
	</td>
</tr>
<tr>
    <td colspan="3"  style="font-size: medium; color: #000000; font-family: Arial; width: 5in;" align="center" ><%=strdesci%></td>
</tr>
<tr>
    <td colspan="3" class="style1">
        <img src='/Barcode/Barcodehandler.ashx?data=<%=strord%>&code=Code128&dpi=96' 
            alt='Barcode Generator TEC-IT' hspace="80" vspace="5" 
            style="width: 4in; height: .5in;"/></td>
</tr> 
<tr>  
    <td style="font-size: medium; color: #000000;" align="center">Work Order Lot</td> 
    <td style="font-size: medium; color: #000000;" align="center">Quantity</td> 
    <td style="font-size: medium; color: #000000;" align="center">Unit</td>      
</tr>
<tr>  
    <td style="font-size: medium; color: #000000;" align="center"><%=strord%></td> 
    <td style="font-size: medium; color: #000000;" align="center"><%=strweight%></td> 
    <td style="font-size: medium; color: #000000;" align="center"><%=struni%></td>  
</tr>
<tr>  
    <td style="font-size: medium; color: #000000;" align="center">Date</td> 
    <td style="font-size: medium; color: #000000;" align="center">Shift</td> 
    <td style="font-size: medium; color: #000000;" align="center">Operator</td>   
</tr>
<tr>     
    <td style="font-size: medium; color: #000000;" align="center"><%=strfecha%></td>  
    <td style="font-size: medium; color: #000000;" align="center">A, B, C, D</td> 
    <td style="font-size: medium; color: #000000;" align="center"><%=struser%></td> 
</tr>
<tr>  
    <td style="font-size: medium; color: #000000;" align="center"></td> 
    <td style="font-size: medium; color: #000000;" align="center">Machine</td>
    <td style="font-size: medium; color: #000000;" align="center"></td>   
</tr>
<tr>  
    <td style="font-size: large; color: #000000;" align="left"></td> 
    <td style="font-size: large; color: #000000;" align="center"><%=strmaq%></td>
    <td style="font-size: large; color: #000000;" align="left"></td>   
</tr>
<!-- #include file="include/dbxconoff.inc" -->
<%
Else
	Session("strmsg") = "Order doesn't be created, first print regrind pallet tag."
	Response.Redirect("whInvReprintLabelRegrind.aspx?flag=") 
End if
%>
</table>
</html>
<div>
<table align="left" style="width: 5.8in; height: .25in" border="1" cellspacing="0" cellpadding="0">
<tr>
<td>
<!--
<a href="javascript:window.print()" style="color: #000000">Print</a>
-->
<a href="#" onclick="javascript:printDiv('label')" style="color: #000000; font-size: medium">Print</a></td>
<td><a href="whInvReprintLabelRegrind.aspx"><img src="images/btn_Exit.jpg"></a>
</td>
</tr>
</table>
</div>
</form>

