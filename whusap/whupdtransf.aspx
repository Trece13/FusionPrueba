<%@ Page aspcompat=true Debug="true"%>
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
<head>
	<title>WH Transfers</title>
<link href="../basic.css" rel="stylesheet" type="text/css"></head>
<meta name="viewport" content="width=300, user-scalable=no">
<body> 

<%

Dim strord, stritem, strdescr, strund, strrowcolor, strSQL, objrs, Odbcon, strmsg, stralmacend, strubicaciond, strseq, stralmubicd,strqtyt
Dim strqty, strqtyord, strqtyrec, strnqtyr, strnqty
strqtyt = 0
stralmubicd = ucase(Request.Form("txtalmubicd"))
stralmacend = left(ucase(Request.Form("txtalmubicd")),6)
strubicaciond = mid(ucase(Request.Form("txtalmubicd")),7,10)
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

if strqtyt = 0 then
     Session("strmsg") = "Registered quantity cannot be zero. Please try again with right information."
     Response.Redirect("whtransf3.aspx?flag=Y") 
end if

if stralmubicd = Session("stralmubic") then
     Session("strmsg") = "Location To cannot be the same Location From. Please try again with right information."
     Response.Redirect("whtransf3.aspx?flag=Y") 
End if

'Conect to database and execute sp
%>
<!-- #include file="include/dbxconon.inc" -->
<%

strSQL = "select t$cwar, t$loca, t$loct, t$btrr " & _
" from baan.twhwmd300" & Session("env") & " where t$cwar='" + Session("stralmacend") + "' and t$loca='" + Session("strubicaciond")  + "'"

' Validar alamcen-ubicacion destino
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
' Validar que exista localizacion, que sea tipo 5-carga y que no este bloqueado
	if objrs.Fields("t$loct").value <> 5 then
		Session("strmsg") = "Location To Type registered (" + Session("strubicaciond") + ") is not bulk type. Please try again with right information."
		Response.Redirect("whtransf3.aspx?flag=Y") 
	end if
	if objrs.Fields("t$btrr").value <> 2 then
		Session("strmsg") = "Location to code registered (" + Session("strubicaciond") + ") is locked. Please try again with right information."
		Response.Redirect("whtransf3.aspx?flag=Y") 
	end if
' Validar que la cantidad a transferir no sea mayor a la cantidad disponible
  if formatnumber(strqtyt,2)-Session("strqtydisp") > 0 then
    Session("strmsg") = "Quantity to be transfer (" + strqty + ") is greater than available quantity (" + Session("strqtyd") + "). Please try again with right information."
    Response.Redirect("whtransf3.aspx?flag=Y") 
'response.write(Session("strqtyd"))
  end if
else
  strmsg = "Warehouse - To Location doesn´t exist. Please try again with right information."
  Session("strmsg") = strmsg 
  Response.Redirect("whtransf3.aspx?flag=Y") 
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

strqty = replace(strqty,",","")
strSQL = "insert into baan.twhcol010" & Session("env") & " (t$clot,t$sqnb,t$mitm,t$dsca,t$cwor,t$loor,t$cwde,t$lode,t$qtdl,t$cuni,t$date,t$mess,t$user,t$refcntd,t$refcntu) " & _
" values('" + Session("strord") + "','"  +  Cstr(strseq) + "','" + Session("stritem") + "','" + Session("strdescr")+ "','" + Session("stralmacen") + "','" + Session("strubicacion") + "','" + Session("stralmacend") + "','" + Session("strubicaciond") + "',"+ Cstr(Session("strqtyt")) + ",'" + Session("strund") +  "',sysdate+5/24,' ','" + Session("user") + "',0,0)"
'response.write(strSQL) 
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)

'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
Response.Redirect("whtransferencias.aspx?flag=") 
%>
</body>
</html>
