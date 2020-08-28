<%@ Page aspcompat=true debug=true%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "Debe loguearse antes de ejecutar esta opcion."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
'if InStr(Session("opciones"),"CONSU")=0 then
'	Response.Redirect("wh" & Session("cia") & "Menu" & Session("envt") & ".aspx?flg=Y")
'End if
Dim strSQL, Odbcon, objrs
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
	strSQL = "insert into baan.ttccol301" & Session("env") & " (t$user,t$fein,t$come,t$refcntd,t$refcntu) values('" & Session("user") & "',sysdate+5/24,'Inventario Ubicacion',0,0)"
	objrs = Server.CreateObject("ADODB.Recordset")
	objrs.Open (strSQL, Odbcon)
'Desconectar a base de datos
%>
<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<head>

<script type="text/javascript">

function setFocus()
{
    document.getElementById("ubicacion").focus();
}

</script>
<style type="text/css">
    .errorMsg
    {
        color: Black;
        font-weight: bold;
        font-size: medium;
    }
</style>
</head>
<%
Dim stralmacen, strubicacion, strlen, strflag, strmsg  

strflag = Request.QueryString("flag")
if strflag = "Y" then
  stralmacen = Session("almacen")
  strubicacion = Session("ubicacion")
  strmsg = Session("strmsg")
else
  stralmacen  = ""
  strubicacion = ""
  Session("strmsg") = ""
  strmsg  = ""
end if

%>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" onload="setFocus()">
<form name="frmord" method="post" action="whInvUbicacion2.aspx">
<TABLE width="25%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H3>Consulta Inventario por ubicacion</H3></td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_cerrarsesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<table align="left" class="tableDefault4" width="25%" border="0" cellspacing="0" cellpadding="0">
<tr>
<td class="titulog2"><b>Usuario </b>:</td>
<td class="titulog2" colspan="2"><b><%=Session("user")%></b></td>
</tr>
<tr>
<td class="titulog2"><b>Nombre </b>:</td>
<td class="titulog2" colspan="2"><b><%=Session("username")%></b></td>
</tr>
<tr> 
     <td class="titulog2" colspan="2"><b>Ubicacion:</b></td>
     <td><input type="text" name="ubicacion" id="ubicacion" size="9" value=<%=strubicacion%>></td>
</tr>
<tr> 
<td class="errorMsg" colspan="4" rowspan="2"><%=strmsg%></td>
</tr> 
<tr> 
</tr> 
<tr> 
</tr> 
    <td colspan="4" align="center">
      <input type="submit" name="btnLogin" value="  OK  ">
    </td>
</tr>
</table>
</form>
</body>
</html>
