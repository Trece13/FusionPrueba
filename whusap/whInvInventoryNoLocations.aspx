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
	strSQL = "insert into baan.ttccol301" & Session("env") & " (t$user,t$fein,t$come,t$refcntd,t$refcntu) values('" & Session("user") & "',sysdate+5/24,'Inventory No Locations',0,0)"
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
document.getElementById("txtalm").focus();
}

function validar_almubi(field) {
    var ubic;
    ubic = field.value;
    if (ubic == "") {
        alert("You must enter the Location field");
        field.value = "";
        field.focus();
    }
};

function validar_item(field) {
    var itm;
    itm = field.value;
    if (itm == "") {
        alert("You must enter Item field");
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

function FocusItem() {
    document.getElementById("txtitem").focus();
};

function FocusLot() {
    document.getElementById("txtlote").focus();
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
Dim stralmacen, strlote, stritem, strlen, strflag, strmsg, strmsg1

strflag = Request.QueryString("flag")

if strflag = "Y" then
  stralmacen = ""
  strlote = ""
  stritem = ""
  strmsg = ""
else
  stralmacen = ""
  strlote = ""
  stritem = ""
  strmsg = Session("strmsg")
end if

%>
<meta name="viewport" content="width=300, user-scalable=no">
<body onload="setFocus()" bgcolor="#87CEEB">
<form name="frmord" method="post" action="whInvInventoryNoLocations2.aspx">
<TABLE style="width: 35%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td><H3>Inventory Not Locations</H3></td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<table align="left" class="tableDefault4" width="25%" border="0" cellspacing="0" cellpadding="0">
<tr>
<td class="titulog2"><b>User </b>:</td>
<td class="titulog2" colspan="2"><b><%=Session("user")%></b></td>
</tr>
<tr>
<td class="titulog2"><b>Name </b>:</td>
<td class="titulog2" colspan="2"><b><%=Session("username")%></b></td>
</tr>
<tr> 
     <td class="style2"><b>Warehouse:</b></td>
     <td class="style2""><input type="text" id="txtalm" name="txtalm" size="10" onchange = "FocusItem();" value='<%=stralmacen%>'></td>
</tr> 
<tr> 
	 <td class="style2"><b>Item:</b></td>
	 <td class="style2"><input type="text" id="txtitem" name="txtitem" size="10" onchange = "FocusLot();" value='<%=stritem%>'></td>
</tr> 
<tr> 
	 <td class="style2"><b>Lot:</b></td>
	 <td class="style2"><input type="text" id="txtlote" name="txtlote" size="10" value='<%=strlote%>'></td>
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
