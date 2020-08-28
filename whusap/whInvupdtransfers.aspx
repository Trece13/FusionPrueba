<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "Debe loguearse antes de ejecutar esta opcion."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>

<html>
<head>
<title>Transferencias</title>
<link href="../basic.css" rel="stylesheet" type="text/css"></head>
<meta name="viewport" content="width=300, user-scalable=no">
<body> 

<%

Dim strord, stritem, strdescr, strund, strrowcolor, strSQL, objrs, Odbcon, strmsg, stralmacend, strubicaciond, strseq, stralmubicd,strqtyt
Dim strqty, strqtyord, strqtyrec, strnqtyr, strnqty
strqtyt = 0
stralmubicd = ucase(Request.Form("txtalmubicd"))
'stralmacend = left(ucase(Request.Form("txtalmubicd")),6)
'strubicaciond = mid(ucase(Request.Form("txtalmubicd")),7,10)
stralmacend = ucase(Session("stralmacen"))
strubicaciond = ucase(Request.Form("txtalmubicd"))
if Request.Form("txtqtyt") = "" 
  strqtyt = 0.00
else
  strqtyt = Formatnumber(Request.Form("txtqtyt"),2)
End if
Session("stralmubicd") = stralmubicd
Session("stralmacend") = stralmacend
Session("strubicaciond") = strubicaciond
Session("strqtyt") = strqtyt

strmsg  = ""
strseq =  0
Session("strmsg") =""

'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
'Buscar Almacen de la ubicacion
'strSQL = "select t$cwar alm from baan.twhwmd300" & Session("env") & " where trim(t$loca)='" + stralmubicd + "'"
'objrs=Server.CreateObject("ADODB.recordset")
'objrs.Open (strSQL, Odbcon)
'If Not objrs.EOF Then
'    stralmacend= objrs.Fields("alm").Value
'    strubicaciond = stralmubicd
'    Session("stralmacend") = stralmacend
'    Session("strubicaciond") = strubicaciond
'End if

if strqtyt = 0 then
     Session("strmsg") = "Cantidad ingresada debe ser mayor a cero, por favor verifique."
     Response.Redirect("whInvtransfers3.aspx?flag=Y") 
end if

if stralmubicd = Session("stralmubic") then
     Session("strmsg") = "Ubicacion Destino debe ser diferente a la Ubicacion Origen, por favor verifique."
     Response.Redirect("whInvtransfers3.aspx?flag=Y") 
End if

strSQL = "select t$cwar, t$loca, t$loct, t$btrr " & _
" from baan.twhwmd300" & Session("env") & " where t$cwar='" + Session("stralmacend") + "' and t$loca='" + Session("strubicaciond")  + "'"

' Validar alamcen-ubicacion destino
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
' Validar que exista localizacion, que sea tipo 5-carga y que no este bloqueado
	if objrs.Fields("t$loct").value <> 5 then
		Session("strmsg") = "Ubicacion Destino (" + Session("strubicaciond") + ") debe ser de carga, por favor verifique."
		Response.Redirect("whInvtransfers3.aspx?flag=Y") 
	end if
	if objrs.Fields("t$btrr").value <> 2 then
		Session("strmsg") = "Ubicacion Destino (" + Session("strubicaciond") + ") esta bloqueada, por favor verifique."
		Response.Redirect("whInvtransfers3.aspx?flag=Y") 
	end if
' Validar que la cantidad a transferir no sea mayor a la cantidad disponible
  if formatnumber(strqtyt,2)-Session("strqtydisp") > 0 then
    Session("strmsg") = "Cantidad a Transferir (" + strqty + ") mas grande que la cantidad disponible (" + Session("strqtyd") + "), por favor verifique."
    Response.Redirect("whInvtransfers3.aspx?flag=Y") 
end if

else
  strmsg = "Bodega-Ubicacion no existe, por favor revisar.."
  Session("strmsg") = strmsg 
  Response.Redirect("whInvtransfers3.aspx?flag=Y") 
End if

' Verificar si ya existe un registro para esta orden
strSQL = "select nvl(max(t$sqnb),0) Seq from baan.twhcol010" & Session("env") & " where t$clot='" + Session("strord") + "'"

objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)

If Not objrs.EOF Then
    strseq = objrs.Fields("Seq").Value + 1
else
    strseq =  1
end if

strqty = replace(strqtyt,",",".")
strSQL = "insert into baan.twhcol010" & Session("env") & " (t$clot,t$sqnb,t$mitm,t$dsca,t$cwor,t$loor,t$cwde,t$lode,t$qtdl,t$cuni,t$date,t$mess,t$user,t$refcntd,t$refcntu) " & _
" values('" + Session("strord") + "','"  +  Cstr(strseq) + "','" + Session("stritem") + "','" + Session("strdescr")+ "','" + Session("stralmacen") + "','" + Session("strubicacion") + "','" + Session("stralmacend") + "','" + Session("strubicaciond") + "',"+ Cstr(strqty) + ",'" + Session("strund") +  "',sysdate+5/24,' ','" + Session("user") + "',0,0)"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
strmsg = "Transferencia salvada satisfactoriamente"
Session("strmsg") = strmsg 
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
Response.Redirect("whInvtransfers.aspx?flag=") 
%>
</body>
</html>
