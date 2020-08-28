<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "Debe loguearse antes de ejecutar esta opcion."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
'if InStr(Session("opciones"),"CONSU")=0 then
'	Response.Redirect("wh" & Session("cia") & "Menu" & Session("envt") & ".aspx?flg=Y")
'End if
%>
<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<head>

</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB">
<TABLE width="25%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H3>Consulta Inventario por ubicacion</H3></td>
</tr>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_cerrarsesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<form name="frminq" method="post" action="whInvUbicacion.aspx">
<table align="left" class="tableDefault4" width="25%" border="1" cellspacing="0" cellpadding="0">
<%
Dim stralmacen, strdesalmacen, strubicacion , strdesubicacion, strarticulo, strdesarticulo, strdescr, strund, strrowcolor, strSQL, objrs, Odbcon, strmsg

strubicacion = ucase(trim(Request.Form("ubicacion")))
'stralmacen = ucase(trim(Request.Form("almacen")))
'Session("almacen") = stralmacen 
Session("ubicacion") = strubicacion

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
        Response.Redirect("whInvUbicacion.aspx?flag=Y") 
    End if

' Validar almacen
strSQL = "select rtrim(t$dsca) desalmacen from baan.ttcmcs003" & Session("env") & " where t$cwar='" + Session("almacen") + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
strdesalmacen = objrs.Fields("desalmacen").Value
' Validar Ubicacion
strSQL = "select rtrim(t$dsca) desubicacion from baan.twhwmd300" & Session("env") & " where t$cwar='" + Session("almacen") + "' and trim(t$loca)='" + Session("ubicacion") + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
strdesubicacion = objrs.Fields("desubicacion").Value
%>
<tr> 
	<td><b>Bodega:</b></td>
	<td colspan="3"><%=stralmacen%> - <%=strdesalmacen%></td>
</tr>
<tr> 
     <td><b>Ubicacion:</b></td>
     <td colspan="3"><%=strdesubicacion%></td>
</tr>
<tr>
  <td></td>
</tr>
<tr>
    <td class="titulog4" width="25%"><b>Articulo</b></td>
    <td class="titulog4" width="45%"><b>Descripcion</b></td>
    <td class="titulog4" width="10%"><b>Lote</b></td>
    <td class="titulog4" width="20%"><b>Cantidad</b></td>
</tr>
<%

strrowcolor = "#ffffff"
strSQL = "select ltrim(rtrim(w.t$item)) Item, rtrim(a.t$dsca) Descrip, w.t$clot lote, w.t$stks Cant " & _
" from baan.twhinr140" & Session("env") & " w join baan.ttcibd001" & Session("env") & " a on w.t$item=a.t$item " & _
" where w.t$cwar ='" + Session("almacen") + "' and t$loca='"+ Session("ubicacion") + "'"
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
		<td><%=objrs.Fields("Item").Value%></td>
		<td><%=objrs.Fields("Descrip").Value%></td>
		<td><%=objrs.Fields("lote").Value%></td>
		<td align="Right"><%=objrs.Fields("Cant").Value%></td>
	</tr>
	<tr> 
		<td colspan="2" align="center"><input type="submit" name="btnLogin" value=" Nueva Consulta "></td>
	</tr>
<%
objrs.MoveNext
Loop

'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
else
       Session("strmsg") = "La ubicacion no existe."
       Response.Redirect("whInvUbicacion.aspx?flag=Y") 
end if
else
       Session("strmsg") = "La bodega no existe."
       Response.Redirect("whInvUbicacion.aspx?flag=Y") 
End if
%>
</table>
</form>
</body>
</html>
