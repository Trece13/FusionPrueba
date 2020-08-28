<%@ Page aspcompat=true Debug=true %>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must authenticate before you can execute this option."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
'if InStr(Session("opciones"),"TRANS")=0 then
'	Response.Redirect("WH" & Session("cia") & "Menu" & Session("envt") & ".aspx?flg=Y")
'End if
%>
<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<head>

<script type="text/javascript">

function setFocus()
{
document.getElementById("txtalmubicd").focus();
}

</script>

</head>

<%
Dim strord, stritem, strdescr, strund, strqtyt, strrowcolor, strSQL, objrs, Odbcon, strmsg, strflag, stralmubic, stralmubicd, stralmacen, strubicacion,strqtyp, strqtydisp

'strqty = 0

strflag = Request.QueryString("flag")
if strflag = "Y" then

stralmubicd = Session("stralmubicd") 
strqtyt = Session("strqtyt")
strmsg = Session("strmsg")
else

stralmubic = ucase(Request.Form("txtalmubic"))
Session("stralmubic") = ucase(Request.Form("txtalmubic"))
Session("stralmacen") = left(ucase(stralmubic),6)
Session("strubicacion") = Mid(ucase(stralmubic),7,10)

'Conect to database and execute sp
%>
<!-- #include file="include/dbxconon.inc" -->
<%

strSQL = "select t$cwar, t$loca, t$loct, t$btri, " & _
" (select nvl(sum(T$STKS),0)  from baan.twhinr140" & Session("env") & " where t$cwar=w.t$cwar and t$loca=w.t$loca and t$clot='" + Session("strord") + "') qtydisp " & _ 
" from baan.twhwmd300" & Session("env") & " w where t$cwar='" + Session("stralmacen") + "' and w.t$loca='" + Session("strubicacion")  + "'"

'response.write(strSQL)
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)

' Did we find anything?
If Not objrs.EOF Then
' Validar que exista localizacion, que sea tipo 5-carga y que no este bloqueado
     if objrs.Fields("t$loct").value <> 5 then
       Session("strmsg") = "Location Type registered (" + Session("strubicacion") + ") is not bulk type. Please try again with right information."
       Response.Redirect("whtransf2.aspx?flag=Y") 
     end if
     if objrs.Fields("t$btri").value <> 2 then
       Session("strmsg") = "Location registered (" + Session("strubicacion") + ") is locked. Please try again with right information."
       Response.Redirect("whtransf2.aspx?flag=Y") 
     end if
    Session("strqtydisp") = objrs.Fields("qtydisp").value
    if formatnumber(Session("strqtydisp")) = 0 then
       Session("strmsg") = "Inventory are not available in this location to be transfered."
       Response.Redirect("whtransf2.aspx?flag=Y") 
    end if
else
   strmsg = "Warehouse and Location doesn´t exist. Please try again with right information."
  Session("strmsg") = strmsg 
  Response.Redirect("whtransf2.aspx?flag=Y") 
end if

'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%

end if

%>
<meta name="viewport" content="width=300, user-scalable=no">
<body onload="setFocus()">
<form name="frmord" method="post" action="whupdtransf.aspx">
<table width="25%">
<tr>
<td><img src = "images/logophoenix_s.jpg" /></td>
<td></td><td><h2>WH Transfers</h2></td>
</tr>
</table>
<table align="left" class="tableDefault4" width="25%" border="0" cellspacing="0" cellpadding="0">
<tr> 
     <td class="titulog6" height="10"><b>Lot:</b></td>
     <td class="titulog7"><%=Session("strord")%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Item:</b></td>
</tr> 
<tr> 
     <td class="titulog7" colspan="3"><%=Session("stritem")%></td>
</tr> 
<tr> 
     <td class="titulog7" colspan="3"><%=Session("strdescr")%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>From Location:</b></td>
     <td class="titulog7"><%=Session("stralmubic")%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Available Quantity:</b></td>
     <td class="titulog7"><%=Session("strqtydisp")%></td>
     <td class="titulog7"><%=Session("strund")%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>To Location:</b></td>
     <td class="titulog7"><input type="text" name="txtalmubicd" size="10" value=<%=stralmubicd%>></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Quantity to be transfer:</b></td>
     <td class="titulog7"><input type="text" name="txtqtyt" size="10" value=<%=strqtyt%>></td>
</tr> 
<tr> 
     <td class="errorMsg" colspan="3" rowspan="2"><%=strmsg%></td>
</tr> 
<tr> 
	<td height="8" colspan="2"><div align="center"><input type="submit" name="btnLogin" value="  OK  "></div></td>
	<td align="center"><a href="wh<%=Session("cia")%>Menu<%=Session("envt")%>.aspx"><img src="Images/btnMenuppal.jpg"></a></td>
</tr>
</table>
</form>
</body>
</html>
