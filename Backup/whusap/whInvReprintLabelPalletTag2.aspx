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
            height: 28px;
            width: 267px;
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
Dim strord, strseq, strsec, strrowcolor, strSQL, strSQL1, objrs, Odbcon, stritem, strdesci, struni, strmaq, strfecha, strweight, strdel
Dim strmsg, strflag, strmachine, struser, strqtyplt, sec, sec1, weight

Session("url") = "1"
strflag = Request.QueryString("flag")
if strflag = "Y" then
	strord = ucase(trim(Request.Form("orden")))
    strseq = ucase(trim(Request.Form("seq")))
    strseq = strord+"-"+strseq
	strmsg = Session("strmsg")
else
	strord = ucase(trim(Request.Form("orden")))
    strseq = ucase(trim(Request.Form("seq")))
    strseq = strord+"-"+strseq
end if
	'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
    <%
'Validar que la orden exista en la tabla de secuencias
strSQL = "select col022.t$pdno orden, col022.t$sqnb secuence, col022.t$qtdl weight, col022.t$date-5/24 fecha, col022.t$mitm item, ibd001.t$dsca desci, ibd003.t$conv Qtyplt, " & _
" ibd001.t$cuni unidad, sfc010.t$mcno maquina, col022.t$logn usuario, col022.t$dele borrado from baan.tticol022" & Session("env") & " col022 " & _
" inner join baan.ttcibd001" & session("env") & " ibd001 on ibd001.t$item = col022.t$mitm inner join baan.ttisfc010" & session("env") & " sfc010 on sfc010.t$pdno = col022.t$pdno " & _
" left join baan.ttcibd003" & Session("env") & " ibd003 on ibd003.t$basu = ibd001.t$cuni and ibd003.t$item = ibd001.t$item and ibd003.t$unit = 'PLT'" & _
" where col022.t$pdno = '" + strord + "'" & _
" and col022.t$sqnb = '" + strseq + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
	strdel = objrs.Fields("borrado").Value
    if strdel = 1 then
        Session("strmsg") = "Pallet tag was deleted, cannot be reprinted."
	    Response.Redirect("whInvReprintLabelPalletTag.aspx?flag=")         
    end if
	strord = objrs.Fields("orden").Value
    sec = trim(objrs.Fields("secuence").Value)
    sec1 = mid(sec,11,3)
    weight =  objrs.Fields("weight").Value
    weight = Replace(weight,",",".")
    stritem = trim(objrs.Fields("item").Value)
    strdesci = objrs.Fields("desci").Value
    struni = objrs.Fields("unidad").Value
    strmaq = objrs.Fields("maquina").Value
    strfecha = objrs.Fields("fecha").Value
    struser = objrs.Fields("usuario").Value
    strqtyplt = objrs.Fields("Weight").Value

    strSQL = "update baan.tticol022" & Session("env") & " col022 " & _
    " set t$dlrp = sysdate+5/24, t$norp = t$norp + 1" & _
    " where col022.t$pdno = '" + strord + "'" & _
    " and col022.t$sqnb = '" + strseq + "'"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
        

    'Desconectar a base de datos
%>
<table align="left" style="width: 5.8in; height: 3.8in" border="1" cellspacing="0" cellpadding="0">
    <!--
<tr><td colspan="2" align="center"><IMG SRC = "images/logophoenix_s.jpg" hspace="10" vspace="10" ></td></tr>
-->
<tr>
    <td colspan="2"        
        style="font-size: small; color: #000000; font-family: Arial; width: 5in;" 
        align="center" ><%=strdesci%>
    </td>
    <td        
        style="font-size: x-small; color: #000000; font-family: Arial; width: 5in;" 
        align="center" >MADE IN: DUBLIN, VA
    </td>
</tr>
<tr>
    <td colspan="3">       
        <img src='/Barcode/Barcodehandler.ashx?data=<%=stritem%>&code=Code128&dpi=96' 
            alt='Barcode Generator TEC-IT' hspace="80" vspace="5" 
            style="width: 4in; height: .5in;"/>
    </td>
</tr>
<tr>
    <td colspan="3"        
        style="font-size: medium; color: #000000; font-family: Arial; width: 5in;" 
        align="center" ><%=stritem%>
    </td>    
</tr>
<tr>
    <td colspan="3">       
        <img src='/Barcode/Barcodehandler.ashx?data=<%=sec%>&code=Code128&dpi=96' 
            alt='Barcode Generator TEC-IT' hspace="80" vspace="5" 
            style="width: 4in; height: .5in;"/></td>
</tr> 
<tr>  
    <td style="font-size: small; color: #000000;" align="center">Work Order Lot</td> 
    <td style="font-size: small; color: #000000;" align="center">Pallet Number</td>  
    <td style="font-size: small; color: #000000;" align="center">Inspector Initial</td>  
</tr>
<tr>  
    <td style="font-size: large; color: #000000;" align="center"><%=strord%></td> 
    <td style="font-size: large; color: #000000;" align="center"><%=sec1%></td> 
    <td style="font-size: large; color: #000000;" align="center"></td>    
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center">Date</td> 
    <td style="font-size: small; color: #000000;" align="center">Shift</td>  
    <td style="font-size: small; color: #000000;" align="center">Case Per Pallet</td>  
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center"><%=strfecha%></td>   
    <td style="font-size: medium; color: #000000;" align="center">A, B, C, D</td>
    <td style="font-size: large; color: #000000;" align="center"><%=strqtyplt%></td>
</tr>
<tr>  
    <td style="font-size: small; color: #000000; " colspan=3 >Reprinted by : <%=session("user")%> </td>
</tr>  
<!-- #include file="include/dbxconoff.inc" -->
<%
Else
	Session("strmsg") = "Order doesn't be created, first print pallet tag."
	Response.Redirect("whInvReprintLabelPalletTag.aspx?flag=") 
End if
%>
</table>
</html>
</body>
</div>
<div>
<table align="left" style="width: 5.8in; height: .25in" border="1" cellspacing="0" cellpadding="0">
<tr>
<td>
<!--
<a href="javascript:window.print()" style="color: #000000">Print</a>
-->
<a href="#" onclick="javascript:printDiv('label')" style="color: #000000; font-size: medium">Print</a></td>
<td><a href="whInvReprintLabelPalletTag.aspx"><img src="images/btn_Exit.jpg"></a>
</td>
</tr>
</table>
</div>
</form>

