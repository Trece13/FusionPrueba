<%@ Page aspcompat=true debug=true%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must login firts, before use this session."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
Dim strSQL, Odbcon, objrs
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
	strSQL = "insert into baan.ttccol301" & session("env") & " (t$user,t$fein,t$come,t$refcntd,t$refcntu) values('" & Session("user") & "',sysdate+5/24,'Hours Accounting Abs/Ind',0,0)"
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
document.getElementById("machine").focus();
};

function CheckLength() {
    var lenght = document.getElementById("machine").value.length;
    var str = document.getElementById("machine").value;
    if (document.getElementById("machine").value != "") {
        if (document.getElementById("machine").value.length < 4 || document.getElementById("machine").value.length > 6) {
            alert("Machine Code maximum 6 characters")
            document.getElementById("machine").focus();
            document.getElementById("machine").value = "";
            return false;
        }
    }
};

function devolver_datos() {
    var indice = document.frmord.type.selectedIndex
    var valor = document.frmord.type.options[indice].value
    var textoEscogido = document.frmord.type.options[indice].text
    document.frmord.valortype.value = valor
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
        .errorMsg
        {
            color: Black;
            font-weight: bold;
            font-size: medium;
        }
</style>
    </style>
</head>

<%
Dim strmachine, strlen, strflag, strmsg  

strflag = Request.QueryString("flag")
if strflag = "Y" then
  strmsg = ""
  strmachine = ""
else
  strmachine  = ""
  strmsg = Session("strmsg")
end if

%>
<meta name="viewport" content="width=300, user-scalable=no">
<body onload="setFocus()" bgcolor="#87CEEB">
<form name="frmord" method="post" action="whInvTime2ai.aspx">
<TABLE style="width: 35%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H3>Abscent-Indirect Hours Accounting </H3>
    </td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<table align="left" class="tableDefault4" border="0" cellspacing="0" 
    cellpadding="0">
<tr>
<td class="style2"><b>User </b>:</td>
<td class="style2" colspan="2"><b><%=Session("user")%></b></td>
</tr>
<tr>
<td class="style2"><b>Name </b>:</td>
<td class="style2" colspan="2"><b><%=Session("username")%></b></td>
</tr>
<tr> 
     <td class="style2"><b>Machine</b></td>
     <td class="style2">
         <input type="text" id="machine" name="machine" size="9" style="width: 131px; height: 23px;" onblur = "CheckLength();" value=<%=strmachine%>></td>
         <td><input type='hidden' name='valortype' value=''/></td>
</tr>
<tr> 
  <td class="style2"><b>Type</b></td>
    <td class="style3">
         <select name="type" onblur='devolver_datos()' style="width: 120px">
              <option value="3" selected>Abscent</option>
              <option value="2">Indirect</option>
         </select></td>
</tr>
<tr> 
<td class="errorMsg" colspan="2"><%=strmsg%></td>
</tr> 
    <td colspan="2" align="center">
      <input type="submit" name="btnLogin" value="  Query  ">
    </td>
</tr>
</table>
</form>
</body>
</html>
