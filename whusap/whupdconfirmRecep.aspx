<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must authenticate before you can execute this option."
	Response.Redirect ("wh" & Session("cia") & "login" & Session("envt") & ".aspx?flg=Y")
End if
'if InStr(Session("opciones"),"CONFI")=0 then
'	Response.Redirect("wh" & Session("cia") & "Menu" & Session("envt") & ".aspx")
'End if
%>
<html>
<head>
	<title>Update Confirm Receipt</title>
<link href="../basic.css" rel="stylesheet" type="text/css"></head>
<meta name="viewport" content="width=300, user-scalable=no">
<body> 

<%
Dim strord, stritem, strdescr, strund, strrowcolor, strSQL, strSQL1, objrs, Odbcon, strmsg, stralm, strqpndc 
Dim strqty, strqtyord, strqtyrec, strnqtyr, strnqty, strseq, strqtypend, strordc  

if Session("url") = "1" then
strord = ucase(trim(Request.Form("txtorden")))
strordc = ucase(trim(Request.Form("txtordenc")))
stritem = Request.Form("txtitem")
strdescr = Request.Form("txtdescr")
strqty = Request.Form("txtqty")
strund = Request.Form("txtund")
stralm = Request.Form("txtalm")

Session("strord") = strord 
Session("strordc") = strordc
Session("stritem") = stritem
Session("strdescr") = strdescr
Session("strqty") = strqty
Session("strund") = strund
Session("stralm") = stralm
strqtypend  = Session("strqtypend")

else

strord = Session("strord")
strordc = Session("strordc")
stritem = Session("stritem")
strdescr = Session("strdescr")
strqty = Session("strqty")
strund = Session("strund")
stralm = Session("stralm")

end if

'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
strord = mid(strord,1,9)
strSQL = "select o.t$pdno Orden, o.t$mitm Item,  o.t$qrdr qtyord, o.t$qtdl+ " & _
"(select nvl(sum(T$QTDL),0) from baan.tticol025" & Session("env") & " where T$PDNO=o.t$pdno and T$MITM=o.t$mitm)  qtypndconf " & _
"from baan.ttisfc001" & Session("env") & " o inner join baan.tticol022" & Session("env") & " col022 on col022.t$pdno = o.t$pdno " & _
" where col022.t$pdno = '" + strord + "'" & _
" and col022.t$sqnb = '" + strordc + "'" & _
" and col022.t$pro1 = '2'"

objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)

' Did we find anything?
If Not objrs.EOF Then
   strqtyord =  objrs.Fields("qtyord").Value
   strqpndc =  objrs.Fields("qtypndconf").Value
else
   strqtyord = 0.000
   strqpndc = 0.000
End if

strnqty = strqtyrec + strqty

if Session("url") = 1 then
  if (isdbnull(strqty) or strqty = "") then
    strqty = 0
  end if
  if strqty > strqtypend then
     strmsg = "Confirmed quantity (" + Cstr(strqty) + ") is greater that pending quantity to be comfirmed (" + Cstr(strqtypend) + "), Please try again with right information."
     Session("strmsg") = strmsg 
     Response.Redirect("whconfirmRecep2.aspx?flag=Y") 
  end if
end if

if strqty = 0 then
     strmsg = "Confirmed quantity can not be zero. Please try again with right information."
     Session("strmsg") = strmsg 
     Response.Redirect("whconfirmRecep2.aspx?flag=Y") 
end if

if strqty < 0 then
     strmsg = "Negative quantities are not allowed to be confirmed in this device."
     Session("strmsg") = strmsg 
     Response.Redirect("whconfirmRecep2.aspx?flag=Y") 
end if

If Not objrs.EOF Then

    ' Verificar si ya existe un registro para esta orden
    strSQL = "select nvl(max(t$sqnb),0) sqnb from baan.tticol025" & Session("env") & _
            " where t$pdno ='" + strord + "'" 

        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)

	strqty = formatnumber(strqty) 'Nuevo
      strqty = replace(strqty,",","")

    If Not objrs.EOF Then
        strseq = formatnumber(objrs.Fields("sqnb").Value) + 1
        strSQL = "insert into baan.tticol025" & Session("env") & " (t$pdno,t$sqnb,t$mitm,t$dsca,t$qtdl,t$cuni,t$date,t$mess,t$user,t$refcntd,t$refcntu) " & _
		    " values('" + strord + "'," + Cstr(strseq) + ",'" + stritem + "','" + strdescr + "'," + strqty + ",'" + strund + "',sysdate+5/24,' ','" + Session("user") + "',0,0)"
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)

        strSQL1 = "update baan.tticol022" & Session("env") & " set t$proc = 1, t$log1 =  '" + Session("user") + "', " & _
            " t$datc = sysdate+5/24, t$qtd1 = " + strqty + " , t$pro1 = 1 " & _
		    " where t$pdno = '" + strord + "'" & _
		    " and    t$sqnb = '" + strordc + "'"
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL1, Odbcon)
    end if

else

    strmsg = "Production Order and Secuence Pallete was already confirm."
    Session("strmsg") = strmsg 
    Response.Redirect("whconfirmRecep.aspx?flag=Y") 

end if
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
strmsg = "Production Order and Secuence Pallete was confirm succesfully."
Session("strmsg") = strmsg 
Response.Redirect("whconfirmRecep.aspx?flag=Y") 
%>

</body>
</html>
