<%@ Page aspcompat=true debug=true%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must login fisrt, before use this session."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
Dim strSQL, Odbcon, objrs
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
	strSQL = "insert into baan.ttccol301" & session("env") & "(t$user,t$fein,t$come,t$refcntd,t$refcntu) values('" & Session("user") & "',sysdate+5/24,'Inbound Advice',0,0)"
	objrs = Server.CreateObject("ADODB.Recordset")
	objrs.Open (strSQL, Odbcon)
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->

<html>
<link href="css/basic.css" rel="stylesheet" type="text/css">
<head>
<script type="text/javascript">

function setFocus()
{
document.getElementById("sugerencia").focus();
};

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
        .style2
        {
            color: Black;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-style: normal;
            text-align: Left;
            height: 10px;
        }
        #Select1
        {
            width: 130px;
        }
        #origenes
        {
            width: 130px;
        }
        .errorMsg
        {
            color: Black;
            font-weight: bold;
            font-size: medium;
        }
        .tableDefault4
        {
            width: 310px;
        }
    </style>
    </style>
</head>

<%
Dim strsugerencia, strlen, strflag, strmsg  

strflag = Request.QueryString("flag")
if strflag = "Y" then
    strmsg  = Session("strmsg")
else
    strsugerencia = ""
    strmsg = ""
end if

%>
<meta name="viewport" content="width=300, user-scalable=no">
<body onload="setFocus()" bgcolor="#87CEEB">
<form name="frmord" method="post" action="whInvAdvices2.aspx">
<TABLE style="width: 35%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td><H3>Recoleccion Articulo</H3>
    </td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_cerrarsesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<table align="left" class="tableDefault4" border="0" cellspacing="0" 
    cellpadding="0">
<tr>
<td class="style2"><b>Usuario </b>:</td>
<td class="style2" colspan="2"><b><%=Session("user")%></b></td>
</tr>
<tr>
<td class="style2"><b>Nombre </b>:</td>
<td class="style2" colspan="2"><b><%=Session("username")%></b></td>
</tr>
<tr> 
     <td class="style2"><b>Sugerencia Numero</b></td>
     <td class="style2">
         <input type="text" id="sugerencia" name="sugerencia" size="9" 
             style="width: 170px; height: 23px;" value="<%=strsugerencia%>"/></td>
     <td><input type='hidden' name='valororigen' value='2'/></td>
</tr>
<tr> 
<td class="errorMsg" colspan="2"><%=strmsg%></td>
</tr> 
    <td colspan="2" align="center">
      <input type="submit" name="btnLogin" value="  Consultar  ">
    </td>
</table>
</form>
</body>
</html>
