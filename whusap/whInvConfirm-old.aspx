<%@ Page aspcompat=true debug=true%>
<%
if	Session("logok") <> "OKYes" then
    Session("Message")= "You must login first, before use this session."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
Dim strSQL, Odbcon, objrs
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
	strSQL = "insert into baan.ttccol301" & session("env") & " (t$user,t$fein,t$come,t$refcntd,t$refcntu) values('" & Session("user") & "',sysdate+5/24,'Receipt Confirm',0,0)"
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

function devolver_datos() {
    var indice = document.frmord.origen.selectedIndex
    var valor = document.frmord.origen.options[indice].value
    var textoEscogido = document.frmord.origen.options[indice].text
    document.frmord.valororigen.value = valor
};
</script>
    <style type="text/css">
        .style1
        {
            color: White;
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
        .style3
        {
            color: Black;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-style: normal;
            text-align: Left;
            height: 10px;
            width: 131px;
        }
    </style>
</head>

<%
Dim strsugerencia, strlen, strflag, strmsg  

strflag = Request.QueryString("flag")
if strflag = "Y" then
    strmsg  = ""
else
    strsugerencia  = ""
    strmsg = Session("strmsg")
end if

%>

<body onload="setFocus()" bgcolor="#87CEEB">
<form name="frmord" method="post" action="whInvConfirm2.aspx">
<TABLE style="width: 35%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td><H3>Delivery Confirm</H3></td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<table align="left" class="tableDefault4" border="0" cellspacing="0" 
    cellpadding="0">
    <tr>
<td class="style2"><b>User</b>:</td>
<td class="style3"><b><%=Session("user")%></b></td>
    </tr>
<tr> 
     <td class="style2"><b>Movement Source</b></td>
     <td class="style3">
         <select name="origen" onblur='devolver_datos()' style="width: 129px">
              <option value="2" selected>Purchase</option>
              <option value="1">Sales</option>
              <option value="4">Manufacturing</option>
              <option value="22">Transfer</option>
         </select></td>
    </tr>
</tr>
<tr> 
     <td class="style2"><b>Advice Number</b></td>
     <td class="style3"><input type="text" name="sugerencia" size="9" 
             style="width: 131px; height: 23px;" value="<%=strsugerencia%>"/></td>
     <td><input type='hidden' name='valororigen' value='2'/></td>
</tr>
<tr> 
<td class="errorMsg" colspan="2"><%=strmsg%></td>
</tr> 
    <td colspan="2" align="center">
      <input type="submit" name="btnLogin" value="  Query  ">
    </td>
</table>
</form>
</body>
</html>
