<%@ Page aspcompat=true debug=true%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must authenticate before you can execute this option."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
'if InStr(Session("opciones"),"ANUNC")=0 then
'	Response.Redirect("wh" & Session("cia") & "Menu" & Session("envt") & ".aspx?flg=Y")
'End if
%>
<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<head>

<script type="text/javascript">

</script>

</head>

<%
Dim strord, strordc, stritem, strdescr, strund, strqty, strqtyplt, strrowcolor, strSQL, objrs, Odbcon 
Dim strmsg, strflag, stralm, strdesal, strsts, strtotqty,strtotqtyent,strqtypend, strqtyplt1, strordl, strquan, impreso, anunciado

Session("url") = "1"
strflag = Request.QueryString("flag")
if strflag = "Y" then
	strord = Session("strord")
	stritem = Session("stritem")
	strdescr = Session("strdescr")
	strqty =Session("strqty")
	strund = Session("strund")
	strmsg = Session("strmsg")
	stralm = Session("stralm")
	strdesal = Session("strdesal")
	strsts = Session("strsts")
	strtotqty = Session("strtotqty") 
	strtotqtyent = Session("strtotqtyent") 
	strqtypend = Session("strqtypend") 
else
	strord = ucase(trim(Request.Form("txtorden")))
	Session("strord")  = strord 
    strordc  = strord

	'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
strord = mid(strord,1,9)
strordl = strordc.length()
anunciado = ""
impreso = ""

If (strordl = 13 ) Then

strSQL = "select * from baan.tticol022" & Session("env") & " where t$pdno = '" + strord + "' and t$sqnb= '" + strordc + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
impreso = "no"
If Not objrs.EOF Then
impreso = "si"
strSQL = "select * from baan.tticol022" & Session("env") & " where t$qtdl = 0 and t$pdno = '" + strord + "' and t$sqnb= '" + strordc + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
anunciado = "si"
If Not objrs.EOF Then
    anunciado = "no"
	strSQL = "select o.t$pdno Orden, o.t$mitm Item, rtrim(a.t$dsca) Descr, a.t$cuni Unid, o.t$osta stord, o.t$cwar Almacen, w.t$dsca DespAl, " & _
	"o.t$qrdr-o.t$qdlv-o.t$qtdl-(select nvl(sum(t$qtdl),0) from baan.tticol020" & Session("env") & " where T$PDNO=o.t$pdno and t$mitm=o.t$mitm)  Qtypend , " & _
    "f.t$conv Qtyplt, " & _
	"t$qrdr totqty, t$qdlv+(select nvl(sum(t$qtdl),0) from baan.tticol020" & Session("env") & " where t$pdno=o.t$pdno and t$mitm=o.t$mitm) TotqtyEnt " & _
	"from baan.ttisfc001" & Session("env") & " o join baan.ttcibd001" & Session("env") & " a on o.t$mitm = a.t$item " & _
	"left join baan.ttcibd003" & Session("env") & " f on f.t$basu = a.t$cuni and f.t$item = a.t$item and f.t$unit = 'PLT'" & _
    "join  baan.ttcmcs003" & Session("env") & " w on w.t$cwar=o.t$cwar where o.t$pdno = '" + strord + "'"
    
	objrs=Server.CreateObject("ADODB.recordset")
	objrs.Open (strSQL, Odbcon)

	' Did we find anything?
	If Not objrs.EOF Then
		stritem =  objrs.Fields("item").Value
		strdescr =  objrs.Fields("descr").Value
'		strqty =  0
        strqtyplt1 =  objrs.Fields("Qtyplt").Value
        
        If isdbnull(strqtyplt1) then
            Session("strmsg") = "Item doesn't have conversion factor, please contact Administrator."
			Response.Redirect("whanuncioord.aspx?flag=Y") 
        else
		    strqtyplt =  formatnumber(objrs.Fields("Qtyplt").Value,3)
        end if
		strtotqty =  formatnumber(objrs.Fields("totqty").Value,3)
		strtotqtyent =  formatnumber(objrs.Fields("TotqtyEnt").Value,3)
		strqtypend =  formatnumber(objrs.Fields("Qtypend").Value,3)
		Session("strtotqty")  = strtotqty * 1.1
		Session("strtotqtyent")  = strtotqtyent
		Session("strqtypend")  = strqtypend
		strund =  objrs.Fields("unid").Value
		stralm = objrs.Fields("Almacen").Value
		strdesal = objrs.Fields("DespAl").Value
		strsts = objrs.Fields("stord").Value
		if (strsts = 5 or strsts = 7) then
		else
			if strqtyplt = "" then
				Session("strmsg") = "Doesn't Exist Conversión Factor, please contact Administrator."
				Response.Redirect("whanuncioord.aspx?flag=Y") 
			else
				Session("strmsg") = "Order is not active. Please try again with right information."
				Response.Redirect("whanuncioord.aspx?flag=Y") 
			end if
		end if   
	else
		 if strord <> "" then
			strmsg = "Production Order doesn´t exist. Please try again with right information."
		 else
			strmsg = "You must enter a production order code."
		 end if   
		Session("strmsg") = strmsg 
		Response.Redirect("whanuncioord.aspx?flag=Y") 
	end if
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
else
    if anunciado = "no" then
        strmsg = "Pallete Tag wasn't printed."
    else
        strmsg = "Pallete was announced already."
    end if
    Session("strmsg") = strmsg 
	Response.Redirect("whanuncioord.aspx?flag=Y") 
end if
else
    if impreso = "no" then
        strmsg = "Pallete Tag wasn't printed."
    else
        strmsg = "Pallete was announced already."
    end if
    Session("strmsg") = strmsg 
	Response.Redirect("whanuncioord.aspx?flag=Y") 
end if
else
    strmsg = "Order number incomplete, must have secuence."
    Session("strmsg") = strmsg 
	Response.Redirect("whanuncioord.aspx?flag=Y") 
end if
end if
%>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB">
<form name="frmord" method="post" action="whupdanuncioord.aspx">
<table>
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H2>Production Orders</H2></td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<table align="left" class="tableDefault4" width="28%" border="0" cellspacing="0" cellpadding="0">

<tr> 
     <td class="titulog6" height="10"><b>Production Order No.:</b></td>
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
     <td class="titulog6" ><b>Warehouse:</b></td>
     <td class="titulog7" ><%=stralm%></td>
</tr> 
<tr> 
     <td class="titulog7" colspan="4"><%=strdesal%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Total:</b></td>
     <td class="titulog7"><%=strtotqty%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Delivered:</b></td>
     <td class="titulog7"><%=strtotqtyent%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Pending:</b></td>
     <td class="titulog7"><%=strqtypend%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Announced:</b></td>
     <td class="titulog7"><%=strqtyplt%></td>
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
	<input type="hidden" name="txtalm" value="<%=stralm%>">
	<input type="hidden" name="txtund" value="<%=strund%>">
    <input type="hidden" name="txtordenc" value="<%=strordc%>">
    <input type="hidden" name="txtqty" value="<%=strqtyplt%>">
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
