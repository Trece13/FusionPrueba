<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "Debe loguearse antes de ejecutar esta opcion."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
'if InStr(Session("opciones"),"CONSL")=0 then
'	Response.Redirect("wh" & Session("cia") & "Menu" & Session("envt") & ".aspx?flg=Y")
'End if
%>
<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<head>

</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" >
<TABLE width="25%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H3>Consulta Inventario por Lote</H3></td>
</tr>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_cerrarsesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<form name="frminq" method="post" action="whInvArticulo.aspx">
<table align="left" class="tableDefault4" width="25%" border="1" cellspacing="0" cellpadding="0">
<%
Dim stralmacen, strdesalmacen, strtqty, strlote, strarticulo, strdesarticulo, strdescr, strund, strrowcolor, strSQL, objrs, Odbcon, strmsg

strlote = ucase(trim(Request.Form("lote")))
'stralmacen = ucase(trim(Request.Form("almacen")))
'Session("almacen") = stralmacen 
Session("lote") = strlote 

'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
'Almacen Definido por el Usuario
    strSQL = "select t$cwar bodega from baan.tticol127" & session("env") & " where upper(t$user)= '" + Session("user") + "'" 
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If Not objrs.EOF Then
        stralmacen = objrs.Fields("bodega").Value
        Session("almacen") = stralmacen 
    Else
        strmsg = "Usuario no tiene definido el almacen, por favor comuniquese con el administrador."
        Session("strmsg") = strmsg 
        Response.Redirect("whInvArticulo.aspx?flag=Y") 
    End if

' Validar almacen
strSQL = "select rtrim(t$dsca) despalmacen from baan.ttcmcs003" & Session("env") & " where t$cwar='" + Session("almacen") + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
	strdesalmacen = objrs.Fields("despalmacen").Value
' Validar articulo, debe existir el lote y tener asociado el articulo
	strSQL = "select T$MITM Articulo from baan.ttisfc001" & Session("env") & " where t$pdno='" + Session("lote") + "'"
	objrs=Server.CreateObject("ADODB.recordset")
	objrs.Open (strSQL, Odbcon)
	If Not objrs.EOF Then
		strarticulo = trim(objrs.Fields("articulo").Value)
		Session("articulo") = strarticulo
		strSQL = "select rtrim(t$dsca) desparticulo from baan.ttcibd001" & Session("env") & " where trim(t$item)='" + Session("articulo") + "'"
		objrs=Server.CreateObject("ADODB.recordset")
		objrs.Open (strSQL, Odbcon)
		If Not objrs.EOF Then
			strdesarticulo = objrs.Fields("desparticulo").Value
			strSQL = "select sum(t$stks) tCant from baan.twhinr140" & Session("env") & " where t$cwar='" + Session("almacen") + "' and trim(t$item)='"+ Session("articulo") + "'"
			objrs=Server.CreateObject("ADODB.recordset")
			objrs.Open (strSQL, Odbcon)
			if Not objrs.EOF Then
				strtqty = objrs.Fields("tCant").Value
			End if
%>
<tr> 
	<td><b>Bodega:</b></td>
	<td colspan="3"><%=stralmacen%> - <%=strdesalmacen%></td>
</tr>
<tr> 
    <td><b>Articulo:</b></td>
    <td colspan="3"><%=strdesarticulo%></td>
</tr>
<tr> 
    <td colspan="2"><b>Inventario Total:</b></td>
    <td colspan="2" align="Right"><b><%=strtqty%></b></td>
</tr>
<tr>
	<td></td>
</tr>
<tr>
    <td class="titulog4" width="35%"><b>Ubicacion</b></td>
    <td class="titulog4" width="35%"><b>Lote Numero</b></td>
    <td class="titulog4" width="30%"><b>Cantidad</b></td>
</tr>
<%
			strrowcolor = "#ffffff"
			strSQL = "select t$loca Ubicacion, t$clot Lote, t$stks Cant  from baan.twhinr140" & Session("env") & " where t$cwar='" + Session("almacen") + "' and trim(t$item)='"+ Session("articulo") + "'"
			objrs=Server.CreateObject("ADODB.recordset")
			objrs.Open (strSQL, Odbcon)
			Do While Not objrs.EOF
				If strrowcolor = "#ffffff" Then 
					strrowcolor = "#D3D3D3" 
				Else 
					strrowcolor = "#ffffff" 
				End if
%>
<tr bgcolor="<%=strrowcolor%>">
	<td><%=objrs.Fields("Ubicacion").Value%></td>
	<td><%=objrs.Fields("Lote").Value%></td>
	<td align="Right"><%=objrs.Fields("Cant").Value%></td>
</tr>
<%
			objrs.MoveNext
			Loop
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
		else
			Session("strmsg") = "Articulo no existe."
			Response.Redirect("whInvArticulo.aspx?flag=Y") 
		end if
	else
			Session("strmsg") = "Lote no existe."
			Response.Redirect("whInvArticulo.aspx?flag=Y") 
	end if
else
	Session("strmsg") = "Bodega no existe."
	Response.Redirect("whInvArticulo.aspx?flag=Y") 
End if
%>
<tr> 
	<td colspan="2" align="center"><input type="submit" name="btnLogin" value=" Nueva  Consulta "></td>
</tr>
</table>
</form>
</body>
</html>
