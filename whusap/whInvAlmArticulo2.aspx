<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "Debe autenticarse primero para ejecutar esta opcion."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
'if InStr(Session("opciones"),"CONSA")=0 then
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
<td></td><td><H3>Inventario por Bodega - Articulo</H3></td>
</tr>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_cerrarsesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<form name="frminq" method="post" action="whInvAlmArticulo.aspx">
<table align="left" class="tableDefault4" width="25%" border="1" cellspacing="0" cellpadding="0">
<%
Dim stralmacen, strdesalmacen, strtqty, strlote, strarticulo, strdesarticulo, strdescr, strunidad , strrowcolor, strSQL, objrs, Odbcon, strmsg

strarticulo = ucase(trim(Request.Form("articulo")))
'stralmacen = ucase(trim(Request.Form("almacen")))
'Session("almacen") = stralmacen 
Session("articulo") = strarticulo

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
        Response.Redirect("whInvAlmArticulo.aspx?flag=Y") 
    End if

' Validar almacen
strSQL = "select rtrim(t$dsca) despalmacen from baan.ttcmcs003" & Session("env") & " where t$cwar='" + Session("almacen") + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
	strdesalmacen = objrs.Fields("despalmacen").Value
' Validar articulo
	strSQL = "select rtrim(t$dsca) desparticulo,t$cuni unidad from baan.ttcibd001" & Session("env") & " where trim(t$item)='" + Session("articulo") + "'"
	objrs=Server.CreateObject("ADODB.recordset")
	objrs.Open (strSQL, Odbcon)
	If Not objrs.EOF Then
	strdesarticulo = objrs.Fields("desparticulo").Value
	strunidad = objrs.Fields("unidad").Value
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
    <td><b>Unidad:</b></td>
    <td colspan="3"><%=strunidad%></td>
</tr>
<tr>
	<td></td>
</tr>
<tr>
    <td class="titulog4" width="30%"><b>Ubicacion</b></td>
    <td class="titulog4" width="30%"><b>Lote</b></td>
    <td class="titulog4" width="40%"><b>Cantidad</b></td>
</tr>

<%
		strrowcolor = "#ffffff"
		strdesarticulo = objrs.Fields("desparticulo").Value
		strSQL = "select t$cwar Almacen, t$loca Ubicacion, t$clot Lote, t$stks Cant from baan.twhinr140" & Session("env") & " where t$cwar='" + Session("almacen") + "' and trim(t$item)='"+ Session("articulo") + "'"
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
			Session("strmsg") = "El articulo no existe."
			Response.Redirect("whInvAlmArticulo.aspx?flag=Y") 
	end if
else
	Session("strmsg") = "La bodega no existe."
	Response.Redirect("whInvAlmArticulo.aspx?flag=Y") 
End if
%>
<tr> 
  <td colspan="2" align="center"><input type="submit" name="btnLogin" value=" Nueva  Consulta"></td>
</tr>
</table>
</form>
</body>
</html>
