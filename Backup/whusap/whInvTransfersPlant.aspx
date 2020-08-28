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
	strSQL = "insert into baan.ttccol301" & session("env") & " (t$user,t$fein,t$come,t$refcntd,t$refcntu) values('" & Session("user") & "',sysdate+5/24,'Transfers to Plant',0,0)"
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
document.getElementById("txtart").focus();
}

function devolver_datos() {
    var indice = document.frmord.planta.selectedIndex
    var valor = document.frmord.planta.options[indice].value
    var textoEscogido = document.frmord.planta.options[indice].text
    document.frmord.bodega.value = valor
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
    #txtalmubicd
    {
        width: 100px;
    }
    #txtqtyt
    {
        width: 80px;
    }
    #txtart
    {
        width: 150px;
    }
    .errorMsg
    {
        color: Black;
        font-weight: bold;
        font-size: medium;
    }
</style>
</head>
<%
Dim strord, strart, strlen, strflag, strmsg  

strflag = Request.QueryString("flag")
if strflag = "Y" then
  strart = Session("strart")
  strmsg = ""
else
  strart = ""
  strmsg = Session("strmsg")
end if

%>
<meta name="viewport" content="width=300, user-scalable=no">
<body onload="setFocus()" bgcolor="#87CEEB">
<form name="frmord" method="post" action="whInvupdatetransfersPlant.aspx">
<TABLE style="width: 35%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td><H3>Transfers to Plant</H3></td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<table align="left" class="tableDefault4" width="30%" border="0" cellspacing="0" cellpadding="0">
<tr>
<td class="titulog2"><b>User </b>:</td>
<td class="titulog2" colspan="2"><b><%=Session("user")%></b></td>
</tr>
<tr>
<td class="titulog2"><b>Name </b>:</td>
<td class="titulog2" colspan="2"><b><%=Session("username")%></b></td>
</tr>
<tr> 
     <td class="style2"><b>Plant:</b></td>
<td class="style12">
    <select name="planta" onblur='devolver_datos()' size="1" style="width: 260px">
    <%
     Dim strSQLh, objrsh, Odbconh
     'Abre la conexión con la base de datos a través de una conexión ODBC
    %>
    <!-- #include file="include/dbxcononusah.inc" -->
    <%
    strSQLh = "select planta, bodega from (select 'A' planta, 'Select Plant Tranfer From...' bodega from dual union " & _
        " select  trim(t$plan) planta, trim(t$cwar) bodega from baan.tticol004" & session("env") & _
        " ) order by planta "
    objrsh=Server.CreateObject("ADODB.recordset")
    objrsh.Open (strSQLh, Odbconh)
    Do While Not objrsh.EOF 
        Response.Write("<option value='" & objrsh.Fields("bodega").Value & "'>" & objrsh.Fields("planta").Value & " - " & objrsh.Fields("bodega").Value & "</option>")
    objrsh.MoveNext
    Loop
    %>
    </select>
    <!-- #include file="include/dbxconoffh.inc" -->
</td>
<input type='hidden' id='bodega' name='bodega' value='NA'/>
</tr>
<tr> 
     <td class="style2"><b>Item:</b></td>
     <td><input type="text" id="txtart" name="txtart" size="15" value=''</td>
</tr>
<tr> 
     <td class="titulog6" colspan="1"><b>Target Location:</b></td>
     <td class="titulog7"><input type="text" id="txtalmubicd" name="txtalmubicd" size="15" value=''></td>
</tr> 
<tr> 
     <td class="titulog6" colspan="1"><b>Transfer Qty:</b></td>
     <td class="titulog7"><input type="text" id="txtqtyt" name="txtqtyt" size="15" value=''></td>
</tr> 
<tr> 
<td class="errorMsg"><%=strmsg%></td>
</tr> 
<tr> 
    <td colspan="2" align="center">
      <input type="submit" name="btnLogin" value="  Transfer  ">
    </td>
</tr>
</table>
</form>
</body>
</html>
