<%@ Page aspcompat=true debug=true%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "Debe loguearse antes de ejecutar esta opcion."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
Dim strSQL, Odbcon, objrs
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
	strSQL = "insert into baan.ttccol301" & session("env") & " (t$user,t$fein,t$come,t$refcntd,t$refcntu) values('" & Session("user") & "',sysdate+5/24,'Transfers',0,0)"
	objrs = Server.CreateObject("ADODB.Recordset")
	objrs.Open (strSQL, Odbcon)
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->

<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<head>

<script type="text/javascript">

function setFocus()
{
document.getElementById("txtorden").focus();
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
        .errorMsg
        {
            color: Black;
            font-weight: bold;
            font-size: medium;
        }
    #txtorden
    {
        width: 150px;
    }
</style>
</head>
<%
Dim strord, strart, strlen, strflag, strmsg  

strflag = Request.QueryString("flag")
if strflag = "Y" then
  'strord = Session("strord")
  'strart = Session("strart")
   strord = ""
  strart = ""
  strmsg = ""
else
  strord = ""
  strart = ""
  strmsg = Session("strmsg")
end if

%>
<meta name="viewport" content="width=300, user-scalable=no">
<body onload="setFocus()" bgcolor="#87CEEB">
<form name="frmord" method="post" action="whInvtransfers2.aspx">
<table style="width: 35%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td><H3>Transferencias</H3></td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_cerrarsesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<table align="left" class="tableDefault4" width="30%" border="0" cellspacing="0" cellpadding="0">
<tr>
<td class="titulog2"><b>Usuario </b>:</td>
<td class="titulog2" colspan="2"><b><%=Session("user")%></b></td>
</tr>
<tr>
<td class="titulog2"><b>Nombre </b>:</td>
<td class="titulog2" colspan="2"><b><%=Session("username")%></b></td>
</tr>
<tr> 
     <td class="style2"><b>Lote Numero:</b></td>
     <td><input type="text" id="txtorden" name="txtorden" size="20" value=<%=strord%>></td>
</tr>
<tr> 
     <td class="style2"><b>Articulo:</b></td>
     <td><input type="text" name="txtart" size="30" value="<%=strart%>" style="width: 200px"></td>
</tr>
<tr> 
<td class="errorMsg"><%=strmsg%></td>
</tr> 
<tr> 
    <td colspan="2" align="center">
      <input type="submit" name="btnLogin" value="  Consultar  ">
    </td>
</tr>
</table>
</form>
</body>
</html>
