<%@ Page aspcompat=true Debug=true %>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "Debe loguearse antes de ejecutar esta opcion."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>
<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<head>

<script type="text/javascript">

function setFocus()
{
document.getElementById("txtalmubicd").focus();
}

</script>

</head>

<%
Dim strord, stritem, strdescr, strund, strqtyt, strrowcolor, strSQL, objrs, Odbcon, strmsg, strflag, stralmubic, stralmubicd
Dim strfactor, stralmacen, strubicacion,strqtyp, strqtydisp, cantplt

'strqty = 0

strflag = Request.QueryString("flag")
if strflag = "Y" then

stralmubicd = Session("stralmubicd") 
strqtyt = ""
strmsg = Session("strmsg")
else

stralmubic = ucase(Request.Form("txtalmubic"))
Session("stralmubic") = ucase(Request.Form("txtalmubic"))
'Session("stralmacen") = left(ucase(stralmubic),6)
Session("stralmacen") = ucase(Request.Form("txtalmacen"))
'Session("strubicacion") = Mid(ucase(stralmubic),7,10)
Session("strubicacion") = ucase(stralmubic)
Session("strord") = ucase(Request.Form("txtord"))
strord = Session("strord")
stritem = ucase(Request.Form("txtitem"))
strfactor = Session("factor")

'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
IF (strord = "") then
    Session("strord") = " "
Else
    Session("strord") = strord
End if

'Buscar Almacen de la ubicacion
'strSQL = "select t$cwar alm from baan.twhwmd300" & Session("env") & " where trim(t$loca)='" + stralmubic + "'"
'objrs=Server.CreateObject("ADODB.recordset")
'objrs.Open (strSQL, Odbcon)
'If Not objrs.EOF Then
'    stralmacen = objrs.Fields("alm").Value
'    strubicacion = stralmubic
'    Session("stralmacen") = stralmacen
'    Session("strubicacion") = strubicacion
'End if

'Validar Almacen
strSQL = "select t$cwar alm from baan.twhwmd200" & Session("env") & " where trim(t$cwar)='" + Session("stralmacen") + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If objrs.EOF Then
    strmsg = "Almacen no existe, por favor revisar."
    Session("strmsg") = strmsg 
    Response.Redirect("whInvtransfers2.aspx?flag=Y") 
End if

Session("item") = "         "+stritem
strSQL = "select t$cwar, t$loca, t$loct, t$btri, " & _
" (select nvl(sum(T$STKS),0)  from baan.twhinr140" & Session("env") & " where t$cwar=w.t$cwar and t$loca=w.t$loca and t$clot='" + Session("strord") + "'" & _
" and t$item = '" + Session("item") + "' ) qtydisp " & _ 
" from baan.twhwmd300" & Session("env") & " w where t$cwar='" + Session("stralmacen") + "' and w.t$loca='" + Session("strubicacion")  + "'" 
objrs = Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)

' Did we find anything?
If Not objrs.EOF Then
' Validar que exista localizacion, que sea tipo 5-carga y que no este bloqueado
     if objrs.Fields("t$loct").value <> 5 then
       Session("strmsg") = "Ubiacion (" + Session("strubicacion") + ") no es de carga, por favor revise."
       Response.Redirect("whInvtransfers2.aspx?flag=Y") 
     end if
     if objrs.Fields("t$btri").value <> 2 then
       Session("strmsg") = "Ubicacion (" + Session("strubicacion") + ") bloqueada, por favor verifique."
       Response.Redirect("whInvtransfers2.aspx?flag=Y") 
     end if
    Session("strqtydisp") = objrs.Fields("qtydisp").value
    if (isdbnull(strfactor)) then
        cantplt = Math.Round((objrs.Fields("qtydisp").value / 1), 2)
    else
        cantplt = Math.Round((objrs.Fields("qtydisp").value / strfactor), 2)
    end if
    if formatnumber(Session("strqtydisp")) = 0 then
       Session("strmsg") = "Articulo no disponible en esa ubicacion para transferir."
       Response.Redirect("whInvtransfers2.aspx?flag=Y") 
    end if
else
   'strmsg = "Bodega-Ubicacion no existe, por favor revisar."
   strmsg = "Ubicacion no existe, por favor revisar."
  Session("strmsg") = strmsg 
  Response.Redirect("whInvtransfers2.aspx?flag=Y") 
end if

'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%

end if

%>
<meta name="viewport" content="width=300, user-scalable=no">
<body onload="setFocus()" bgcolor="#87CEEB">
<form name="frmord" method="post" action="whInvupdtransfers.aspx">
<TABLE style="width: 35%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td><H3>Transferencias</H3></td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_cerrarsesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<table align="left" class="tableDefault4" width="25%" border="0" cellspacing="0" cellpadding="0">
<tr> 
     <td class="titulog6" height="10"><b>Lote:</b></td>
     <td class="titulog7"><%=Session("strord")%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Articulo:</b></td>
</tr> 
<tr> 
     <td class="titulog7" colspan="3"><%=Session("stritem")%></td>
</tr> 
<tr> 
     <td class="titulog7" colspan="3"><%=Session("strdescr")%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Almacen:</b></td>
     <td class="titulog7"><%=ucase(Session("stralmacen"))%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Ubicacion Origen:</b></td>
     <td class="titulog7"><%=Session("stralmubic")%></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Cant Disponible:</b></td>
     <td class="titulog7"><%=Session("strqtydisp")%></td>
     <td class="titulog7"><%=Session("strund")%></td>
</tr> 
<tr>
      <td class="titulog6" colspan="1"><b>Cantidad Disponible:</b></td>
     <td class="titulog7"><%=cantplt%></td>
</tr>
<tr> 
     <td class="titulog6" colspan="1"><b>Ubicacion Destino:</b></td>
     <td class="titulog7"><input type="text" id="txtalmubicd" name="txtalmubicd" size="10" value=<%=stralmubicd%>></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Cantidad a Transferir:</b></td>
     <td class="titulog7"><input type="text" name="txtqtyt" size="10" value=<%=strqtyt%>></td>
</tr> 
<tr> 
     <td class="errorMsg" colspan="3" rowspan="2"><%=strmsg%></td>
</tr> 
<tr> 
</tr> 
<tr> 
    <td align="center" colspan="3">
      <input type="submit" name="btnLogin" value="  Actualizar  ">
    </td>
</tr> 
</table>
</form>
</body>
</html>
