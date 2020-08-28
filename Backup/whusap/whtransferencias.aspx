<%@ Page aspcompat=true debug=true%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must authenticate before you can execute this option."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
'if InStr(Session("opciones"),"TRANS")=0 then
'	Response.Redirect("WH" & Session("cia") & "Menu" & Session("envt") & ".aspx?flg=Y")
'End if
Dim strSQL, Odbcon, objrs
%>
<!-- #include file="include/dbxconon.inc" -->
<%
	strSQL = "insert into baan.ttccol301" & Session("env") & " (t$user,t$fein,t$come,t$refcntd,t$refcntu) values('" & Session("user") & "',sysdate+5/24,'Transferencias',0,0)"
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
    .errorMsg
    {
        color: Black;
        font-weight: bold;
        font-size: medium;
    }
</style>
</head>
<%
Dim strord, strlen, strflag, strmsg  

strflag = Request.QueryString("flag")
if strflag = "Y" then
  strord = Session("strord")
  strmsg = Session("strmsg")
else
  strord = ""
  Session("strmsg") = ""
  strmsg  = ""
end if

%>
<meta name="viewport" content="width=300, user-scalable=no">
<body onload="setFocus()">
<form name="frmord" method="post" action="whtransf2.aspx">
<table>
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H2>WH Transfers</H2></td>
</TABLE>
<table align="left" class="tableDefault4" width="30%" border="0" cellspacing="0" cellpadding="0">
<tr> 
     <td class="titulog2" colspan="2"><b>Lot Number:</b></td>
     <td><input type="text" name="txtorden" size="9" value=<%=strord%>></td>
</tr>
<tr> 
<td class="errorMsg" colspan="2" rowspan="2"><%=strmsg%></td>
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
