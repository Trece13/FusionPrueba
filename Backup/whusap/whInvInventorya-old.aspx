<%@ Page aspcompat=true debug=true %>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must login first, before use this session."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
Dim strSQL, Odbcon, objrs
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
	strSQL = "insert into baan.ttccol301" & session("env") & " (t$user,t$fein,t$come,t$refcntd,t$refcntu) values('" & Session("user") & "',sysdate+5/24,'Inventory',0,0)"
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
document.getElementById("txtlote").focus();
}

function validar_almubi(field) {
    var ubic;
    ubic = field.value;
    if (ubic == "") {
        alert("You must enter Warehouse - Location field");
        field.value = "";
        field.focus();
    }
};

function validar_item(field) {
    var itm;
    itm = field.value;
    if (itm == "") {
        alert("You must enter item field");
        field.value = "";
        field.focus();
    }
};

function validar_lote(field) {
    var lot;
    lot = field.value;
    if (lot != "") {
        document.getElementById("txtitem").disabled = true;
    }
    else {
        document.getElementById("txtitem").disabled = false;
    }
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
    </style>
</head>

<%
Dim stralmubic1, stralmacen, strubicacion, strlote, stritem, strlen, strflag, strmsg, strmsg1

strflag = Request.QueryString("flag")

if strflag = "Y" then
  stralmubic1 = ucase(trim(Request.Form("txtalmubic")))
  stralmacen = left(ucase(Request.Form("txtalmubic")),6)
  strubicacion = mid(ucase(Request.Form("txtalmubic")),7,10)
  stralmacen = Session("almacen")
  strubicacion = Session("ubicacion")
  strlote = Session("lote")
  stritem = Mid(Session("item"),12,32)
  strmsg = ""
else
  stralmubic1 = ucase(trim(Request.Form("txtalmubic")))
  stralmacen = left(ucase(Request.Form("txtalmubic")),6)
  strubicacion = mid(ucase(Request.Form("txtalmubic")),7,10)
  strlote = ""
  stritem = ""
  strmsg = Session("strmsg")
end if

%>

<body onload="setFocus()" bgcolor="#87CEEB">
<form name="frmord" method="post" action="whInvInventory2.aspx">
<TABLE style="width: 35%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td><H3>Inventory</H3></td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<table align="left" class="tableDefault4" width="25%" border="0" cellspacing="0" cellpadding="0">
<tr> 
	 <td class="style2"><b>Warehouse - Location :</b></td>
	 <td class="style2"><input type="text" name="txtalmubic" size="10" value='<%=stralmubic1%>'></td>
</tr> 
<tr> 
	 <td class="style2"><b>Lot:</b></td>
	 <td class="style2"><input type="text" name="txtlote" size="10" value='<%=strlote%>'></td>
</tr> 
<tr> 
	 <td class="style2"><b>Item:</b></td>
	 <td class="style2"><input type="text" id="txtitem" name="txtitem" size="10" value='<%=stritem%>'></td>
</tr> 
<tr> 
     <td class="errorMsg" colspan="2" rowspan="2"><%=strmsg%></td>
</tr> 
<tr></tr>
<tr> 
    <td align="center" colspan="2"><input type="submit" name="btnLogin" value=" Continue "></td>
</tr> 
</table>
</form>
</body>
</html>
