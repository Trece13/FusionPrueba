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
	strSQL = "insert into baan.ttccol301" & session("env") & " (t$user,t$fein,t$come,t$refcntd,t$refcntu) values('" & Session("user") & "',sysdate+5/24,'Roll Announce 2',0,0)"
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
document.getElementById("orden").focus();
};

function CheckLength() {
    var lenght = document.getElementById("orden").value.length;
    var str = document.getElementById("orden").value;
    if (document.getElementById("orden").value != "") {
        if (document.getElementById("orden").value.length < 9 || document.getElementById("orden").value.length > 9) {
            alert("Remember 9 characters")
            document.getElementById("orden").focus();
            document.getElementById("orden").value = "";
            return false;
        }
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
Dim strroll, strorden, stritem, strcant, strlen, stranun, strware, strpos, strqty, strflag, strmsg  

strflag = Request.QueryString("flag")
if strflag = "Y" then
  strmsg = ""
  strroll = ucase(Request.Form("rollo"))
  strorden = ucase(Request.Form("orden"))
else
  strroll = ucase(Request.Form("rollo"))
  strorden = ucase(Request.Form("orden"))
  strmsg = Session("strmsg")
end if
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
	strSQL = "select t$pdno, t$mitm, t$qtdl, t$pro1 from baan.tticol022" & session("env") & _ 
    " where trim(t$sqnb)= trim('" + strroll + "')"
	objrs = Server.CreateObject("ADODB.Recordset")
	objrs.Open (strSQL, Odbcon)

    If Not objrs.EOF Then
        stranun = objrs.Fields("t$pro1").Value
        if (stranun = 2) then
            Session("strmsg") = "Roll has not been announced."
            Response.Redirect("whInvRollAnnounce.aspx?flag=")              
        end if
        'strorden = objrs.Fields("t$pdno").Value
        stritem = trim(objrs.Fields("t$mitm").Value)
        strcant = objrs.Fields("t$qtdl").Value

        'Buscar Datos Orden e Item
        strSQL = "select  t$pdno orden, t$pono posicion, t$sitm item, t$cwar wareh from baan.tticst001" & Session("env") & _
        " where trim(t$sitm)= trim('" + stritem + "')" & _
        " and t$pdno ='" + strorden + "'"
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
        If Not objrs.EOF Then
            strware = objrs.Fields("wareh").Value
            strpos = objrs.Fields("posicion").Value
            strqty = replace(strqty,",","")
        Else
            strmsg = "Order and Item doesn't exist."
            Session("strmsg") = strmsg 
            Response.Redirect("whInvRollAnnounce.aspx?flag=") 
        End if
    Else
        Session("strmsg") = "Roll Number doesn't exist."
        Response.Redirect("whInvRollAnnounce.aspx?flag=")  
    End If

'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->

<meta name="viewport" content="width=300, user-scalable=no">
<body onload="setFocus()" bgcolor="#87CEEB">
<form name="frmord" method="post" action="whInvupdateRollAnnounce.aspx">
<TABLE style="width: 35%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H3>Rolls Consumption</H3>
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
     <td class="style2"><b>Roll Number</b></td>
     <td class="style2">
         <input type="text" id="rolln" name="rolln" size="9" style="width: 131px; height: 23px;" onchange = "CheckLength();" value="<%=strroll%>"></td>
</tr>

<tr> 
     <td class="style2"><b>Work Order</b></td>
     <td class="style2">
         <input type="text" id="orden" name="orden" size="9" style="width: 131px; height: 23px;" onchange = "CheckLength();" value="<%=strorden%>"></td>
</tr>
<tr> 
     <td class="style2"><b>Item</b></td>
     <td class="style2">
         <input type="text" id="item" name="item" size="9" style="width: 131px; height: 23px;" value="<%=stritem%>" readonly="readonly"></td>
</tr>
<tr> 
     <td class="style2"><b>Quantity</b></td>
     <td class="style2">
     <input type="text" id="cant" name="cant" size="9" style="width: 131px; height: 23px;" value="<%=strcant%>" readonly="readonly"></td>
</tr>
<tr> 
<td class="errorMsg" colspan="2"><%=strmsg%></td>
</tr> 
    <td colspan="2" align="center">
      <input type="submit" name="btnLogin" value="  Register  ">
    </td>
</tr>
</table>
</form>
</body>
</html>
