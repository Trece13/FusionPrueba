<%@ Page aspcompat=true Debug="true"%>
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
<head>
	<title>Update received quantity production order.</title>
<link href="../basic.css" rel="stylesheet" type="text/css"></head>
<meta name="viewport" content="width=480, user-scalable=no">
<body> 
<%
Dim strord, strordc, stritem, strdescr, strund, strrowcolor, strSQL, strSQL1, objrs, Odbcon, strmsg, stralm
Dim strqty, strqtyord, strqtyrec, strnqtyr, strnqty

if Session("url") = "1" then
	strord = ucase(trim(Request.Form("txtorden")))
    strordc = ucase(trim(Request.Form("txtordenc")))
	stritem = Request.Form("txtitem")
	strdescr = Request.Form("txtdescr")
	strqty = Request.Form("txtqty")
	strund = Request.Form("txtund")
	stralm = Request.Form("txtalm")
else
	strord = ucase(trim(Request.Form("txtorden")))
    strordc = ucase(trim(Request.Form("txtordenc")))
	stritem = Request.Form("txtitem")
	strdescr = Request.Form("txtdescr")
	strqty = Request.Form("txtqty")
	strund = Request.Form("txtund")
	stralm = Request.Form("txtalm")

end if

strmsg  = ""
Session("strmsg")=""
'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%

strSQL = "select o.t$pdno Orden, o.t$mitm Item,  o.t$qrdr qtyord, o.t$qdlv+o.t$qtdl+ " & _
"(select nvl(sum(T$QTDL),0) from baan.tticol020" & Session("env") & " where T$PDNO=o.t$pdno and T$MITM=o.t$mitm)  qtyrec " & _
"from baan.ttisfc001" & Session("env") & " o where o.t$pdno = '" + strord + "'"

objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)

' Did we find anything?
If Not objrs.EOF Then
   strqtyord =  objrs.Fields("qtyord").Value
   strqtyrec =  objrs.Fields("qtyrec").Value
else
   strqtyord = 0.000
   strqtyrec = 0.000
End if

if strqty = "" then
     strqty = 0
     strmsg = "Announced quantity cannot be zero. Please try again with right information."
     Session("strmsg") = strmsg 
     Response.Redirect("whanuncioord2a.aspx?flag=Y") 
end if

    strnqty = strqtyrec + strqty

if strqty = 0 then
     strmsg = "Announced quantity cannot be zero. Please try again with right information."
     Session("strmsg") = strmsg 
     Response.Redirect("whanuncioord2a.aspx?flag=Y") 
end if

if strqty < 0 then
     strmsg = "Negative quantities are not allowed to be registered in this device."
     Session("strmsg") = strmsg 
     Response.Redirect("whanuncioord2a.aspx?flag=Y") 
end if

' Verificar si ya existe un registro para esta orden en la tabla ticol022
strSQL = "select t$pdno, t$sqnb from baan.tticol022" & Session("env") & " where T$PDNO='" + strord + "' and t$sqnb ='" + strordc +"'"

objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)

If Not objrs.EOF Then
    strqty = replace(strqty,",","")

    strSQL1 = "update baan.tticol022" & Session("env") & " set t$proc = 1, t$qtdl = '" + strqty + "'" & _
        " where t$pdno = '" + strord + "'" & _
        " and t$sqnb = '" + strordc + "'"

	'strSQL1 = "insert into baan.tticol022" & Session("env") & " (t$pdno,t$sqnb,t$proc,t$logn, t$date,t$mitm, t$qtdl,t$cuni,t$log1,t$datc,t$qtd1,t$pro1, t$log2, t$datu, t$qtd2, t$pro2, t$loca, t$refcntd,t$refcntu) " & _
	'		" values('" + strord + "','" + strordc + "',2,'" + Session("user") + "',sysdate+5/24,'" + stritem + "'," + strqty + ",'" + strund + "','NONE', sysdate+5/24, 0, 2, 'NONE', sysdate+5/24, 0, 2, ' ', 0,0)" 

		objrs=Server.CreateObject("ADODB.recordset")
		objrs.Open (strSQL1, Odbcon)

        ' Verificar si ya existe un registro para esta orden
    'strSQL = "select t$qtdl from baan.tticol020" & Session("env") & " where T$PDNO='" + strord + "'"

    'objrs=Server.CreateObject("ADODB.recordset")
    'objrs.Open (strSQL, Odbcon)


    'If Not objrs.EOF Then
    '    strnqtyr = formatnumber(objrs.Fields("t$qtdl").Value) + strqty
    '    strSQL = "update baan.tticol020" & Session("env") & " set t$qtdl = t$qtdl+" + strqty + ", t$date=sysdate+5/24 where T$PDNO='" + strord + "'"
    'else  
        strSQL = "insert into baan.tticol020" & Session("env") & " (t$pdno,t$mitm,t$dsca,t$qtdl,t$cuni,t$date,t$mess,t$refcntd,t$refcntu,t$user) " & _
	        " values('" + strord + "','" + stritem + "','" + strdescr + "'," + strqty + ",'" + strund + "',sysdate+5/24,' ',0,0,'" + Session("user") + "')" 
    'end if
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
else
	strmsg = "Order was already processed, with that secuence."
	Session("strmsg") = strmsg 
	Response.Redirect("whanuncioordq.aspx?flag=Y") 
end if
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
strmsg = "Order was succesfully insert."
Session("strmsg") = strmsg 
Response.Redirect("whInvLabelPalletTagsPartial.aspx?flag=Y")  
%>
</body>
</html>
