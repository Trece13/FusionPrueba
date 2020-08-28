<%@ Page aspcompat=true %>
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
document.getElementById("txtalmacen").focus();
}

</script>
<style type="text/css">
        .style2
        {
            color: Black;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-style: normal;
            text-align: Left;
            height: 10px;
        }
</style>
</head>

<%
Dim strord, strart, stritem, strdescr, strund, strqty, strrowcolor, strSQL, objrs, Odbcon, strmsg, strflag, stralmubic, stralmacen, strubicacion, strqtyp, strqtydisp

'strqty = 0

strflag = Request.QueryString("flag")
if strflag = "Y" then
  strord = ucase(trim(Request.Form("txtorden")))
  strart = ucase(trim(Request.Form("txtart")))
  strdescr = Session("strdescr")
  stralmubic = Session("stralmubic")
  stralmacen = Session("stralmacen")
  strubicacion = Session("strubicacion")
  strqty =Session("strqty")
  strund = Session("strund")
  strmsg = Session("strmsg")
  strqtyp = Session("strqpend") 
  Session("strart")  = "         "+strart 
else

strord = ucase(trim(Request.Form("txtorden")))
strart = ucase(trim(Request.Form("txtart")))
Session("strord")  = strord 
Session("strart")  = "         "+strart 
strmsg  = " "

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
        Else
            strmsg = "Usuario no tiene definido el almacen, por favor comuniquese con el administrador."
            Session("strmsg") = strmsg 
            Response.Redirect("whInvtransfers.aspx?flag=") 
        End if

' Validar el lote
    Replace(Request("strord"),",","")
    If (strord <> "") Then
        strSQL = "select t$item item from baan.twhltc100" & session("env") & " where t$clot='" + strord + "'" & _
        " and t$item = '" + Session("strart") + "'"
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
            If Not objrs.EOF Then
                stritem = objrs.Fields("item").Value
                strSQL = "select ibd001.t$item Item, rtrim(ibd001.T$DSCA) Descr, ibd001.T$CUNI Unid, ibd003.t$conv factor " & _
                    "from baan.ttcibd001" & session("env") & " ibd001 left join baan.ttcibd003" & Session("env") & _
                    " ibd003 on ibd003.t$item = ibd001.t$item " & _
                    " and ibd003.t$basu = ibd001.t$cuni and ibd003.t$unit = 'CJ' " & _
                    " where ibd001.t$item = '" + session("strart") + "'"

                objrs=Server.CreateObject("ADODB.recordset")
                objrs.Open (strSQL, Odbcon)

                ' Did we find anything?
                If Not objrs.EOF Then
    	            Session("stritem") =  objrs.Fields("item").Value
	                Session("strdescr") =  objrs.Fields("descr").Value
	                Session("strund") =  objrs.Fields("unid").Value
                    Session("factor") =  objrs.Fields("factor").Value

                end if
            else
                strmsg = "Lote / Articulo incorrecto, por favor revise"
                Session("strmsg") = strmsg 
                Response.Redirect("whInvTransfers.aspx?flag=") 
            end if
    else
                strart = "         "+Ucase(strart)
                strSQL = "select ibd001.t$item item, ibd001.t$dsca descr, t$cuni unid, ibd003.t$conv factor from baan.ttcibd001" & session("env") & _
                " ibd001 left join baan.ttcibd003" & Session("env") & _
                " ibd003 on ibd003.t$item = ibd001.t$item " & _
                " and ibd003.t$basu = ibd001.t$cuni and ibd003.t$unit = 'CJ' " & _
                " where ibd001.t$kltc <> 1 and ibd001.t$item='" + strart + "'"      
                objrs=Server.CreateObject("ADODB.recordset")
                objrs.Open (strSQL, Odbcon)
                If Not objrs.EOF Then
                    Session("stritem") =  objrs.Fields("item").Value
	                Session("strdescr") =  objrs.Fields("descr").Value
	                Session("strund") =  objrs.Fields("unid").Value
                    Session("factor") =  objrs.Fields("factor").Value
                else
                    strmsg = "Articulo incorrecto o no utiliza lotes, por favor revise."
                    Session("strmsg") = strmsg 
                    Response.Redirect("whInvtransfers.aspx?flag=") 
                end if
    end if
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%

end if

%>
<meta name="viewport" content="width=300, user-scalable=no">
<body onload="setFocus()" bgcolor="#87CEEB">
<form name="frmord" method="post" action="whInvtransfers3.aspx">
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
     <td class="style2" height="10"><b>Lote:</b></td>
     <td class="style2"><%=strord%></td>
</tr> 
<tr> 
     <td class="style2" colspan="2"><b>Articulo:</b></td>
</tr> 
<tr> 
     <td class="style2" colspan="4"><%=Session("stritem")%></td>
</tr> 
<tr> 
     <td class="style2" colspan="4"><%=Session("strdescr")%></td>
</tr> 
<tr> 
     <td class="style2" colspan="1"><b>Almacen:</b></td>
     <td class="style2"><input type="text" id="txtalmacen" name="txtalmacen" size="15" value=<%=stralmacen%> readonly></td>
</tr> 
<tr> 
     <td class="style2" colspan="1"><b>Ubicacion Origen:</b></td>
     <td class="style2"><input type="text" id="txtalmubic" name="txtalmubic" size="15" value=<%=stralmubic%>></td>
     <td class="style2"><input type="hidden" name="txtitem" size="15" value=<%=Session("stritem")%>></td>
     <td class="style2"><input type="hidden" name="txtord" size="15" value=<%=Session("strord")%>></td>
</tr> 
<tr> 
<td class="errorMsg" colspan="5" rowspan="2"><%=strmsg%>
</tr> 
<tr> 
</tr> 
<tr> 
    <td height="8" colspan="2">
      <div align="center"><input type="submit" name="btnLogin" value="  Consultar  "></div>
    </td>
</tr> 
</table>
</div>
</form>
</body>
</html>
