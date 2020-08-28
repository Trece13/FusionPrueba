<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "Debe loguearse antes de ejecutar esta opcion."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>

<html>
<head>
	<title>Inventory</title>
<link href="../basic.css" rel="stylesheet" type="text/css"></head>
<meta name="viewport" content="width=300, user-scalable=no">
<body style="width: 292px" bgcolor="#87CEEB">

<%
Dim strlote, stritem, strdescr, strunid, strrowcolor, strSQL, objrs, Odbcon, strmsg, strmsg1, stralmacen, strubicacion, strseq, stralmubic, strqpend, strqdpu 
Dim strqty, strqtyord, strqtyrec, strnqtyr, strnqty, strconteo, strmlot

strqty = Request.Form("txtqty")
Session("cantidad") = strqty 
strmsg  = ""
strseq =  0
Session("strmsg") =""
Session("strmsg1") ="" 
strmlot = Request.Form("mlote")

if strqty < 0 then
     Session("strmsg") = "Cantidad ingresada no puede ser menor que cero, por favor verifique."
     Response.Redirect("whInvInventory.aspx?flag=")
end if

if (strmlot <> 1) then
    session("lote") = " "
end if

'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
'Almacen Definido por el Usuario.
strSQL = "select t$cwar bodega from baan.tticol127" & session("env") & " where upper(t$user)= '" + Session("user") + "'" 
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
        If Not objrs.EOF Then
            stralmacen = objrs.Fields("bodega").Value
        Else
            strmsg = "Usuario no tiene definido el almacen, por favor comuniquese con el administrador."
            Session("strmsg") = strmsg 
            Response.Redirect("whInvtransfers.aspx?flag=") 
        End if

'Buscar Almacen de la ubicacion
strSQL = "select t$cwar alm from baan.twhwmd300" & Session("env") & " where trim(t$loca)='" + Session("almubic") + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
    'stralmacen = objrs.Fields("alm").Value
    strubicacion = Session("almubic")
    Session("almacen") = stralmacen
    Session("ubicacion") = strubicacion

    'Buscar Secuencia del Conte
    strSQL = "select t$coun cont from baan.twhcol002" & Session("env") & " where trim(t$cwar)='" + Session("stralmacen") + "'"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If Not objrs.EOF Then
        strconteo = objrs.Fields("cont").Value
        Session("conteo") = strconteo
    Else
        strmsg = "No existe conteo activo para la bodega. '" + Session("stralmacen") + "'"
        Session("strmsg") = strmsg 
        Response.Redirect("whInvInventory.aspx?flag=") 
    End If 
End if


Session("item") = Trim(Session("item"))
Session("item") = "         "+Ucase(Session("item"))
' Verificar si ya existe un registro para esta orden
IF (Session("lote") <> "") then
strSQL = "select nvl(max(T$SQNB),0) Seq from baan.twhcol015" & Session("env") & " where T$CWAR='" + Session("almacen") + "' and t$loca='" + Session("ubicacion") + "' and T$PDNO='" + Session("lote") + "' and t$mitm = '" + Session("item") + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
  strseq = objrs.Fields("seq").Value + 1
else
  strseq = 1
end if
'strqty = replace(strqty,",","")
    strSQL = "insert into baan.twhcol015" & Session("env") & " " & _
    " values(" + Cstr(strseq) + ",'" + Session("almacen") + "','" + Session("ubicacion") + "','" + Session("lote") + "','" + Session("item") + "','" + Session("descripcion") + "'," + Cstr(strqty) + ",'" + Session("unidad") + "',sysdate+5/24,' ',2,0,0,'" + Cstr(Session("conteo")) + "')"

    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    'Desconectar a base de datos
Else
strSQL = "select nvl(max(T$SQNB),0) Seq from baan.twhcol015" & Session("env") & " where T$CWAR='" + Session("almacen") + "' and t$loca='" + Session("ubicacion") + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
  strseq = objrs.Fields("seq").Value + 1
else
  strseq = 1
end if
    strSQL = "insert into baan.twhcol015" & Session("env") & " " & _
    " values(" + Cstr(strseq) + ",'" + Session("almacen") + "','" + Session("ubicacion") + "',' ','" + Session("item") + "','" + Session("descripcion") + "'," + Cstr(strqty) + ",'" + Session("unidad") + "',sysdate+5/24,' ',2,0,0,'" + Cstr(Session("conteo")) + "')"

    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    'Desconectar a base de datos
End if
strmsg = "Conteo salvado."
Session("strmsg") = strmsg 
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
Response.Redirect("whInvInventory.aspx?flag=") 
%>
</body>
</html>
