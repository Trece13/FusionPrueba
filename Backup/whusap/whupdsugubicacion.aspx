<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must authenticate before you can execute this option."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
'if InStr(Session("opciones"),"UBICA")=0 then
'	Response.Redirect("wh" & Session("cia") & "Menu" & Session("envt") & ".aspx")
'End if
%>
<html>
<head>
	<title>Update Location</title>
<link href="../basic.css" rel="stylesheet" type="text/css"></head>
<meta name="viewport" content="width=300, user-scalable=no">
<body> 

<%
Dim strord, strordc, stritem, strdescr, strund, strrowcolor, strSQL, objrs, Odbcon, strmsg, strmsg1, stralmacen, strubicacion, strseq, stralmubic, strqpend, strqdpu 
Dim strqty, strqtyord, strqtyrec, strnqtyr, strnqty, strSQL1, strclot

strord = ucase(trim(Request.Form("txtorden")))
strordc = ucase(trim(Request.Form("txtorden")))
strord = mid(strord,1,9)
stritem = Request.Form("txtitem")
strdescr = Request.Form("txtdescr")
stralmubic = ucase(Request.Form("txtalmubic"))
'stralmacen = left(ucase(Request.Form("txtalmubic")),6)
'strubicacion = mid(ucase(Request.Form("txtalmubic")),7,10)
strqty = Request.Form("strqty")
if strqty = "" then
	strqty=formatnumber(0,2)
else
	strqty=formatnumber(strqty,2)	
end if
strund = Request.Form("txtund")
Session("strord") = strord 
Session("stritem") = stritem
Session("strdescr") = strdescr
Session("stralmubic") = stralmubic
'Session("stralmacen") = stralmacen
'Session("strubicacion") = strubicacion
Session("strqty") = strqty
Session("strund") = strund

strmsg  = ""
strseq =  0
Session("strmsg") =""
Session("strmsg1") ="" 

if strqty = 0 then
     Session("strmsg") = "Registered quantity cannot be zero. Please try again with right information."
     Response.Redirect("whsugubicacion2.aspx?flag=Y") 
end if

'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
'Buscar Almacen de la ubicacion
strSQL = "select t$cwar alm from baan.twhwmd300" & Session("env") & " where trim(t$loca)='" + stralmubic + "'"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If Not objrs.EOF Then
        stralmacen = objrs.Fields("alm").Value
        strubicacion = stralmubic
        Session("stralmacen") = stralmacen
        Session("strubicacion") = strubicacion
    End if

' Validar la ubicacion del producto (INS o REC)
strSQL = "select distinct T$ORGN, T$ITEM from baan.tqmptc011" & Session("env") & " where T$ORGN=4 and trim(T$ITEM)='" + stritem + "'" & _
    " union all select distinct b.T$ORGN, a.T$ITEM from baan.tqmptc020" & Session("env") & " a join baan.tqmptc011" & Session("env") & " b on a.t$qitg=b.t$qitg " & _
    " where trim(a.T$ITEM)='" + stritem + "'" 
 
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If Not objrs.EOF Then
        Session("strtloc") = "INS"
    else
        Session("strtloc") = "REC"
    End if
'Valida si el articulo maneja lote o no
strSQL = "select t$kltc clot from baan.ttcibd001" & Session("env")
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If Not objrs.EOF Then
        strclot = objrs.Fields("clot").Value
    End If
If (strclot <> 1) then    
strSQL = "select o.t$pdno Orden, o.t$mitm Item, rtrim(a.T$DSCA) Descr, a.T$CUNI Unid, " & _
    "o.t$qrdr-o.t$qdlv-(select nvl(sum(T$QTDL),0) from baan.tticol030" & Session("env") & " where T$PDNO=o.t$pdno and T$MITM=o.t$mitm)  Qty, " & _
    "(select nvl(sum(inr140.T$stks),0) from baan.twhinr140" & Session("env") & " inr140 where trim(inr140.t$loca)='" + Session("strtloc") +"' and inr140.t$cwar='" + Session("stralmacen") + "' and inr140.T$ITEM=o.t$mitm) " & _
    " - (select nvl(sum(col030.T$QTDL),0) from baan.tticol030" & Session("env") & " col030 where col030.T$PDNO=o.t$pdno) Qtypend  " & _
    "from baan.ttisfc001" & Session("env") & " o join baan.ttcibd001" & Session("env") & " a on o.T$MITM = a.T$ITEM " & _
    " inner join baan.tticol022" & Session("env") & " col022 on col022.t$pdno = o.t$pdno where col022.t$pdno = '" + strord + "'" & _
    " and col022.t$sqnb = '" + strordc + "'" & _
    " and col022.t$pro2 = '2'"
else
strSQL = "select o.t$pdno Orden, o.t$mitm Item, rtrim(a.T$DSCA) Descr, a.T$CUNI Unid, " & _
    "o.t$qrdr-o.t$qdlv-(select nvl(sum(T$QTDL),0) from baan.tticol030" & Session("env") & " where T$PDNO=o.t$pdno and T$MITM=o.t$mitm)  Qty, " & _
    "(select nvl(sum(inr140.T$stks),0) from baan.twhinr140" & Session("env") & " inr140 where trim(inr140.t$loca)='" + Session("strtloc") +"' and inr140.t$cwar='" + Session("stralmacen") + "' and inr140.T$clot=o.t$pdno and inr140.T$ITEM=o.t$mitm) " & _
    " - (select nvl(sum(col030.T$QTDL),0) from baan.tticol030" & Session("env") & " col030 where col030.T$PDNO=o.t$pdno) Qtypend  " & _
    "from baan.ttisfc001" & Session("env") & " o join baan.ttcibd001" & Session("env") & " a on o.T$MITM = a.T$ITEM " & _
    " inner join baan.tticol022" & Session("env") & " col022 on col022.t$pdno = o.t$pdno where col022.t$pdno = '" + strord + "'" & _
    " and col022.t$sqnb = '" + strordc + "'" & _
    " and col022.t$pro2 = '2'"
End if
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)

    If Not objrs.EOF Then
        Session("strqpend") = formatnumber(objrs.Fields("Qtypend").Value,2)
        strqdpu = formatnumber(Session("strqpend") - Session("strqtya"),2)
    else
        Session("strqpend") = 0.00
    End if

    If Not objrs.EOF then
        strSQL = "select t$pdno Ord, T$MITM Item,  T$CWAR Almac from baan.ttisfc001" & Session("env") & " where T$PDNO='" + strord + "'"
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)

        If Not objrs.EOF Then
            ' Validar que el almacen sea el mismo de la orden
            if trim(objrs.Fields("Almac").Value) <> trim(stralmacen) then
                Session("strmsg") = "Warehouse code (" + stralmacen + ") is not acccording the warehouse order (" + strord + "). Please try again with right information."
                Response.Redirect("whsugubicacion2.aspx?flag=Y") 
            end if
            ' Validar que exista ubicacion, que sea tipo 5-carga y que no este bloqueado 
            strSQL = "select T$CWAR Alm,  nvl(t$loca,' ') Loc, t$inlo Inb, nvl(t$loct,0) TipoLoc,  nvl(t$binb,0) Bloq " & _
                "from  baan.twhwmd300" & Session("env") & " where trim(T$CWAR)='" +  stralmacen + "' and trim(T$LOCA)='" + strubicacion + "'"
	            objrs=Server.CreateObject("ADODB.recordset")
                objrs.Open (strSQL, Odbcon)
            If objrs.EOF Then
                Session("strmsg") = "Location registered (" + strubicacion + ") doesn´t exist in the warehouse. Please try again with right information."
                Response.Redirect("whsugubicacion2.aspx?flag=Y") 
            else
    	        if objrs.Fields("Tipoloc").value <> 5 then
	    	        Session("strmsg") = "Location Type registered (" + strubicacion + ") is not bulk type. Please try again with right information."
		            Response.Redirect("whsugubicacion2.aspx?flag=Y") 
	            end if
	            if objrs.Fields("Inb").value <> 1 then
		            Session("strmsg") = "Location Type registered (" + strubicacion + ") doesn´t allow do an inbound. Please try again with right information."
		            Response.Redirect("whsugubicacion2.aspx?flag=Y") 
	            end if
	            if objrs.Fields("Bloq").value <> 2 then
    		        Session("strmsg") = "Location code registered (" + strubicacion + ") is locked. Please try again with right information."
	    	        Response.Redirect("whsugubicacion2.aspx?flag=Y") 
	            end if

                ' Validar que la cantidad no sea mayor a la cantidad pendiente 
                if formatnumber(strqty,2)-Session("strqpend") > 0 then
                    Session("strmsg") = "Suggested quantity (" + strqty + ") greater that announced quantity (" + Session("strqpend") + ").  Please try again with right information."

                    Response.Redirect("whsugubicacion.aspx?flag=Y") 
                end if
    
                ' Validar que la cantidad ingresada no sea mayor a la cantidad disponible por ubicar
                if formatnumber(strqty,2)-formatnumber(strqdpu,2) > 0 then
                    Session("strmsg") = "Order - " + strord  + " , suggested quantity (" + strqty + ")  is greater than available quantity to be located (" + strqdpu + ")."
                    Response.Redirect("whsugubicacion.aspx?flag=Y") 
                    'response.write(Session("strqpend"))
                end if

                ' Verificar si ya existe un registro para esta orden
                strSQL = "select nvl(max(t$sqnb),0) Seq from baan.tticol030" & Session("env") & " where T$PDNO='" + strord + "'"
        
                    objrs=Server.CreateObject("ADODB.recordset")
                    objrs.Open (strSQL, Odbcon)

                        If Not objrs.EOF Then
                            strseq = objrs.Fields("Seq").Value + 1
                        else
                            strseq =  1
                        end if
                        strqty = replace(strqty,".","")
                        strqty = replace(strqty,",",".")

                        strSQL = "insert into baan.tticol030" & Session("env") & " (t$pdno,t$mitm,t$dsca,t$cwar,t$loca,t$qtdl,t$cuni,t$sqnb,t$date,t$mess,t$user,t$refcntd,t$refcntu) " & _
                            " values('" + strord + "','         " + stritem + "','" + strdescr + "','" + stralmacen + "','" + strubicacion + "'," + Cstr(strqty) + ",'" + strund + "'," + Cstr(strseq) + ",sysdate+5/24,' ','" + Session("user") + "',0,0)"

                            objrs=Server.CreateObject("ADODB.recordset")
                            objrs.Open (strSQL, Odbcon)

                        strSQL1 = "update baan.tticol022" & Session("env") & " set t$pro2 = 1, t$log2 =  '" + Session("user") + "', " & _
                            " t$datu = sysdate+5/24, t$qtd2 = " + strqty + "," & _
                            " t$loca = trim('" + stralmubic + "')" & _
		                    " where t$pdno = '" + strord + "'" & _
		                    " and    t$sqnb = '" + strordc + "'"

                            objrs=Server.CreateObject("ADODB.recordset")
                            objrs.Open (strSQL1, Odbcon)

                            Session("strmsg") = "Advice Saved succesfully"
            end if 
        End if
    else
        Session("strmsg") = "Production Order doesn´t exist or already located. Please try again with right information."
        Response.Redirect("whsugubicacion.aspx?flag=Y") 
    End if
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
Response.Redirect("whsugubicacion.aspx?flag=Y") 
%>
</body>
</html>
