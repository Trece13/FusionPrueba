<%@ Page aspcompat=true %>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must authenticate before you can execute this option."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
'if InStr(Session("opciones"),"CONFI")=0 then
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
Dim strord, strordc, stritem, strdescr, strund, strqty, strrowcolor, strSQL
Dim objrs, Odbcon, strmsg, strflag, stralm, strdesal, strsts, strtotqty,strtotqtyent,strqtypend, strqtyplt, stranun, strdele

Session("url") = "1"
strflag = Request.QueryString("flag")
if strflag = "Y" then
	strord = Session("strord")
    strordc = Session("strordc")
	stritem = Session("stritem")
	strdescr = Session("strdescr")
	strqty =Session("strqty")
    strqty = replace(strqty,",",".")
	strund = Session("strund")
	strmsg = Session("strmsg")
	stralm = Session("stralm")
	strdesal = Session("strdesal")
	strsts = Session("strsts")
	strtotqty = Session("strtotqty") 
	strtotqtyent = Session("strtotqtyent") 
	strqtypend = Session("strqtypend") 
    strdele = Session("strdele")
else
	strord = mid((ucase(trim(Request.Form("txtorden")))),1,9)
    strordc = ucase(trim(Request.Form("txtorden")))
	Session("strord")  = strord 

	'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
	strSQL = "select o.t$pdno Orden, o.t$mitm Item, rtrim(a.T$DSCA) Descr, a.T$CUNI Unid, o.t$osta stord, o.t$cwar Almacen, w.t$dsca DespAl, col022.t$qtdl anunciada, " & _
	" t$qrdr totqty, o.t$qtdl-(select nvl(sum(T$QTDL),0) from baan.tticol025" & Session("env") & " where T$PDNO=o.t$pdno and T$MITM=o.t$mitm) Qtypend , " & _
	" t$qdlv+(select nvl(sum(T$QTDL),0) from baan.tticol025" & Session("env") & " where T$PDNO=o.t$pdno and T$MITM=o.t$mitm) TotqtyEnt, f.t$conv Qtyplt, col022.t$dele Dele   " & _
	" from baan.ttisfc001" & Session("env") & " o join baan.ttcibd001" & Session("env") & " a on o.T$MITM = a.T$ITEM " & _
	" join baan.ttcmcs003" & Session("env") & " w on w.t$cwar=o.t$cwar " & _
	" inner join baan.tticol022" & Session("env") & " col022 on col022.t$pdno = o.t$pdno " & _
	" left join baan.ttcibd003" & Session("env") & " f on f.t$basu = a.t$cuni and f.t$item = a.t$item and f.t$unit = 'PLT'" & _
	" where col022.t$pro1 = 2 and substr(col022.t$sqnb,1,9) = '" + strord + "'" & _
	" and col022.t$sqnb = '" + strordc + "'"

	objrs=Server.CreateObject("ADODB.recordset")
	objrs.Open (strSQL, Odbcon)

	' Did we find anything?
	If Not objrs.EOF Then
		stritem =  objrs.Fields("item").Value
		strdescr =  objrs.Fields("descr").Value
'		strqty =  0
		strqtyplt =  formatnumber(objrs.Fields("anunciada").Value,3)
		strtotqty =  objrs.Fields("totqty").Value
		strtotqtyent =  objrs.Fields("TotqtyEnt").Value
		strqtypend =  objrs.Fields("Qtypend").Value
		Session("strtotqty")  = strtotqty
		Session("strtotqtyent")  = strtotqtyent
		Session("strqtypend")  = strqtypend
		strund =  objrs.Fields("unid").Value
		stralm = objrs.Fields("Almacen").Value
		strdesal = objrs.Fields("DespAl").Value
		strsts = objrs.Fields("stord").Value
        stranun = objrs.Fields("anunciada").Value
        session("stranunc") = stranun
        strdele = objrs.Fields("Dele").Value

        if (strdele <> 4) then
			 Session("strmsg") = "Pallet has not been wrapped."
			 Response.Redirect("whconfirmRecep.aspx?flag=Y") 
		end if

        if (stranun = 0) then
		    strmsg = "Pallet has not been announced."
		    Session("strmsg") = strmsg 
		    Response.Redirect("whconfirmRecep.aspx?flag=Y") 
        end if 

		if (strsts = 5 or strsts = 7) then
		else
			 Session("strmsg") = "Order is not active. Please try again with right information."
			 Response.Redirect("whconfirmRecep.aspx?flag=Y") 
		end if   

        
	else
		strSQL = "select o.t$pdno Orden, o.t$mitm Item, rtrim(a.T$DSCA) Descr, a.T$CUNI Unid, o.t$osta stord, o.t$cwar Almacen, w.t$dsca DespAl, col022.t$qtdl anunciada, " & _
		" t$qrdr totqty, o.t$qtdl-(select nvl(sum(T$QTDL),0) from baan.tticol025" & Session("env") & " where T$PDNO=o.t$pdno and T$MITM=o.t$mitm) Qtypend , " & _
		" t$qdlv+(select nvl(sum(T$QTDL),0) from baan.tticol025" & Session("env") & " where T$PDNO=o.t$pdno and T$MITM=o.t$mitm) TotqtyEnt, f.t$conv Qtyplt, col022.t$dele Dele  " & _
		" from baan.ttisfc001" & Session("env") & " o join baan.ttcibd001" & Session("env") & " a on o.T$MITM = a.T$ITEM " & _
		" join baan.ttcmcs003" & Session("env") & " w on w.t$cwar=o.t$cwar " & _
		" inner join baan.tticol022" & Session("env") & " col022 on col022.t$pdno = o.t$pdno " & _
		" left join baan.ttcibd003" & Session("env") & " f on f.t$basu = a.t$cuni and f.t$item = a.t$item and f.t$unit = 'PLT'" & _
		" where col022.t$pro1 = 1 and col022.t$qtdl <> 0 and substr(col022.t$sqnb,1,9) = '" + strord + "'" & _
		" and col022.t$sqnb = '" + strordc + "'"
		
		objrs=Server.CreateObject("ADODB.recordset")
		objrs.Open (strSQL, Odbcon)
	    
		If Not objrs.EOF Then
            
            strdele = objrs.Fields("Dele").Value
            if (strdele <> 4) then
			     Session("strmsg") = "Pallet has not been wrapped."
			     Response.Redirect("whconfirmRecep.aspx?flag=Y")
		    end if

            stranun = objrs.Fields("anunciada").Value
            if (stranun = 0) then
                strmsg = "Pallet has not been announced."
            else
			    strmsg = "Pallet was already confirmed."
            end if

			Session("strmsg") = strms
			Response.Redirect("whconfirmRecep.aspx?flag=Y")
               
        else

            strmsg = "Pallet has not been announced."
			Session("strmsg") = strmsg 
			Response.Redirect("whconfirmRecep.aspx?flag=Y")
              
		end if

	end if
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
end if
%>
<meta name="viewport" content="width=300, user-scalable=no">
<body  bgcolor="#87CEEB">
<form name="frmord" method="post" action="whupdconfirmRecep.aspx">
<table>
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H2>Confirm Receipt</H2></td>
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
     <td class="titulog6" colspan="1"><b>To Receive:</b></td>
     <td class="titulog7"><%=strqtypend%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Confirmed:</b></td>
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
    <input type="hidden" name="txtordenc" value="<%=strordc%>">
	<input type="hidden" name="txtitem" value="<%=stritem%>">
	<input type="hidden" name="txtdescr" value="<%=strdescr%>">
	<input type="hidden" name="txtalm" value="<%=stralm%>">
	<input type="hidden" name="txtund" value="<%=strund%>">
	<input type="hidden" name="txtqty" value="<%=strqtyplt%>">
</tr> 
<tr> 
	<td height="8" colspan="2">
	<div align="center"><input type="submit" name="btnLogin" value="  OK  "></div>
	</td>
</tr> 
</table>
</div>
</form>
</body>
</html>
