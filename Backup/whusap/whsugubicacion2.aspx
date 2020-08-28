<%@ Page aspcompat=true debug=true %>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must authenticate before you can execute this option."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
'if InStr(Session("opciones"),"UBICA")=0 then
'	Response.Redirect("wh" & Session("cia") & "Menu" & Session("envt") & ".aspx")
'End if
%>
<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<head>

<script type="text/javascript">

function setFocus()
{
document.getElementById("txtalmubic").focus();
}

</script>

</head>

<%
Dim strord, stritem, strdescr, strund, strqty, strrowcolor, strSQL, objrs, Odbcon, strmsg, strflag, stralmubic, stralmacen
Dim cantplt, strfactor, strubicacion,strqtyp, strqtya 

'strqty = 0

strflag = Request.QueryString("flag")
if strflag = "Y" then
  strord = mid(Session("strord"),1,9)
  stritem = Session("stritem")
  strdescr = Session("strdescr")
  stralmubic = Session("stralmubic")
  stralmacen = Session("stralmacen")
  strubicacion = Session("strubicacion")
  strqty =Session("strqty")
  strund = Session("strund")
  strmsg = Session("strmsg")
  strqtyp = Session("strqpend") 
  strqtya = formatnumber(Session("strqtya"),2)
else

strord = ucase(trim(Request.Form("txtorden")))
Session("strord")  = strord 

'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%

    strSQL = "select o.t$pdno Orden, trim(o.t$mitm) Item, rtrim(a.T$DSCA) Descr, a.T$CUNI Unid, ibd003.t$conv factor, " & _
    "nvl((select sum(t$stkt) from baan.twhinh211" & Session("env") & " where t$oorg=4 and t$loca='INS' and t$orno = o.t$pdno group by o.t$pdno),0) stpnd,  " & _
    " x.t$qtdl strqty from baan.ttisfc001" & Session("env") & " o join baan.ttcibd001" & Session("env") & " a on o.T$MITM = a.T$ITEM " & _
    "join baan.tticol022" & Session("env") & " x on x.t$pdno = o.t$pdno left join baan.ttcibd003" & Session("env") & " ibd003 on ibd003.t$item = a.t$item " & _
    " and ibd003.t$basu = a.t$cuni and ibd003.t$unit = 'PLT' " & _
    " where x.t$pro2 = 2 and x.t$pro1 = 1 and x.t$sqnb = '" + strord + "'"

    objrs = Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)

' Did we find anything?
If Not objrs.EOF Then
   stritem =  objrs.Fields("item").Value
   strdescr =  objrs.Fields("descr").Value
   strund =  objrs.Fields("unid").Value
   strqtya =  objrs.Fields("stpnd").Value
        strqty = objrs.Fields("strqty").Value
        Session("strqtya") = strqtya
    strfactor =  objrs.Fields("factor").Value
    if (isdbnull(strfactor)) then
        cantplt = Math.Round((strqty / 1), 2)
    else
        cantplt = Math.Round((strqty / strfactor), 2)
    end if
else
   if strord <> "" then
      strmsg = "Production Order doesn´t exist, already located or doesn't be confirmed. Please contact your Supervisor."
      Session("strmsg") = strmsg 
      Response.Redirect("whsugubicacion.aspx?flag=Y") 
   else
      strmsg = "You must enter a production order code."
      Session("strmsg") = strmsg 
      Response.Redirect("whsugubicacion.aspx?flag=Y") 
   end if   

  Session("strmsg") = strmsg 
  Response.Redirect("whsugubicacion.aspx?flag=Y") 

end if

'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
end if
%>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" onload="setFocus()">
<form name="frmord" method="post" action="whupdsugubicacion.aspx">
<table>
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H2>Inbound Advice</H2></td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<table align="left" class="tableDefault4" width="30%" border="0" cellspacing="0" cellpadding="0">

<tr> 
     <td class="titulog6" height="10"><b>Production Orders:</b></td>
     <td class="titulog7"><%=strord%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="2"><b>Item:</b></td>
</tr> 
<tr> 
     <td class="titulog7" colspan="4"><%=stritem%></td>
</tr> 
<tr> 
     <td class="titulog7" colspan="4"><%=strdescr%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Location:</b></td>
     <td class="titulog7"><input type="text" id="txtalmubic" name="txtalmubic" size="15" value=<%=stralmubic%>></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Warehouse:</b></td><td class="titulog7"><%=stralmacen%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Location:</b></td><td class="titulog7"><%=strubicacion%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>To Approve:</b></td>
     <td class="titulog7"><%=strqtya%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Quantity:</b></td>
     <td class="titulog7"><%=strqty%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Quantity in PLT:</b></td>
     <td class="titulog7"><%=cantplt%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Unit:</b></td>
     <td class="titulog7"><%=strund%></td>
</tr> 
<tr> 
<td class="errorMsg" colspan="5" rowspan="2"><%=strmsg%>
</tr> 

<tr> 
      <input type="hidden" name="txtorden" value="<%=strord%>">
      <input type="hidden" name="txtitem" value="<%=stritem%>">
      <input type="hidden" name="txtdescr" value="<%=strdescr%>">
      <input type="hidden" name="txtund" value="<%=strund%>">
      <input type="hidden" name="strqty" value="<%=strqty%>">
</tr> 
<tr> 
	<td height="8" colspan="2">
	<div align="center"><input type="submit" name="btnLogin" value="  OK  "></div>
	</TR>
	</td>
</tr> 
</table>
</div>
</form>
</body>
</html>
