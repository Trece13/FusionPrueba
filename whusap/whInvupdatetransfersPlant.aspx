<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must login first, before use this session."
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

Dim strord, stritem, strdescr, strund, strrowcolor, strSQL, objrs, Odbcon, strmsg, stralmacend, strubicaciond, strseq, strubicd,strqtyt
Dim strqty, strqtyord, strqtyrec, strnqtyr, strnqty, strdesci, stralmo

stritem = ucase(Request.Form("txtart"))
stralmo = ucase(Request.Form("bodega"))
strubicd = ucase(Request.Form("txtalmubicd"))
strqtyt = Request.Form("txtqtyt")
strqtyt = replace(strqtyt, ",",".")
Session("stritem") = stritem
Session("stralmubicd") = strubicd
Session("strqtyt") = strqtyt
strmsg  = ""
strseq =  0
Session("strmsg") =""
Session("strdescr") = ""
Session("strund") = ""
Session("stralmacend") = ""
Session("strubicaciond") = ""
Session("stralmaceno") = stralmo

'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
'Validar que el campo Planta se haya seleccionado algun valor
If (stralmo = "NA") Then
    Session("strmsg") = "Plant must be selected."
    Response.Redirect("whInvtransfersPlant.aspx?flag=") 
End If

'Buscar Datos Articulo
strSQL = "select t$dsca desca, t$cuni unidad from baan.ttcibd001" & Session("env") & " where trim(t$item)='" + stritem + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
    strdesci = objrs.Fields("desca").Value
    strund = objrs.Fields("unidad").Value
    Session("strdescr") = strdesci
    Session("strund") = strund
End if

if strdesci = "" then
     Session("strmsg") = "Item doesn't exist, please check."
     Response.Redirect("whInvtransfersPlant.aspx?flag=") 
end if

'Buscar Almacen de la ubicacion
strSQL = "select t$cwar alm from baan.twhwmd300" & Session("env") & " where trim(t$loca)='" + strubicd + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
    stralmacend = objrs.Fields("alm").Value
    strubicaciond = strubicd
    Session("stralmacend") = stralmacend
    Session("strubicaciond") = strubicaciond
End if

if strqtyt = 0 then
     Session("strmsg") = "Quantity enter must be higher than zero, please check."
     Response.Redirect("whInvtransfersPlant.aspx?flag=") 
end if

' Validar almacen-ubicacion destino
strSQL = "select t$cwar, t$loca, t$loct, t$btrr " & _
" from baan.twhwmd300" & Session("env") & " where t$cwar='" + Session("stralmacend") + "' and t$loca='" + Session("strubicaciond")  + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
    ' Validar que exista localizacion, que sea tipo 5-carga y que no este bloqueado
	if objrs.Fields("t$loct").value <> 5 then
		Session("strmsg") = "Taret location (" + Session("strubicaciond") + ") doesn't use to be load, please check."
		Response.Redirect("whInvtransfersPlant.aspx?flag=") 
	end if
	if objrs.Fields("t$btrr").value <> 2 then
		Session("strmsg") = "Target location (" + Session("strubicaciond") + ") is block, please check."
		Response.Redirect("whInvtransfersPlantaspx?flag=") 
	end if
Else
  strmsg = "Target Location doesn't exist, please check."
  Session("strmsg") = strmsg 
  Response.Redirect("whInvtransfersPlant.aspx?flag=") 
End if

' Verificar si ya existe un registro para esta orden
strSQL = "select nvl(max(t$sqnb),0) Seq from baan.twhcol010" & Session("env")
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
    strseq = objrs.Fields("Seq").Value + 1
else
    strseq =  1
end if

strqty = replace(strqtyt,",","")
Session("stritem") = "         "+Session("stritem")
strSQL = "insert into baan.twhcol010" & Session("env") & " (t$clot,t$sqnb,t$mitm,t$dsca,t$cwor,t$loor,t$cwde,t$lode,t$qtdl,t$cuni,t$date,t$mess,t$user,t$refcntd,t$refcntu) " & _
" values(' ','"  +  Cstr(strseq) + "','" + Session("stritem") + "','" + Session("strdescr") + "','" + Session("stralmaceno") + "',' ','" + Session("stralmacend") + "','" + Session("strubicaciond") + "',"+ Cstr(strqty) + ",'" + Session("strund") +  "',sysdate+5/24,' ','" + Session("user") + "',0,0)"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
strmsg = "Transfer saved sucessfully"
Session("strmsg") = strmsg 
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
Response.Redirect("whInvtransfersPlant.aspx?flag=") 
%>
</body>
</html>
