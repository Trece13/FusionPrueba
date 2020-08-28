<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must login first, before use this session."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>
<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<head>

<script type="text/javascript">

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
            text-align: Right;
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
         .tableDefault4
        {
            width: 377px;
        }
    </style>
</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" style="width: 292px">
<TABLE style="width: 35%; height: 17px">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H3>Close Orders</H3></td>
</tr>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"/></a>
        <a href="whMenui.aspx"><img src="images/btn_mainmenu.JPG"/></a>
     </p>
<form name="frminq" method="post" action="whInvSaveCloseOrders.aspx" 
    style="height: 131px; width: 382px">
<table align="left" class="tableDefault4" border="1" cellspacing="0" 
    cellpadding="0">
<tr>
<td colspan="4" align="center">
<input type="submit" name="btnLogin" value="Save Orders  "/>
</td>
</tr>
<%
Dim strrowcolor, strSQL, objrs, Odbcon, strmsg, strorden, strart, strdesca, Cont, Contc
Dim strfuen, strconj, strpos, strsec, strmlote, i
strorden = ucase(trim(Request.Form("orden")))
session("order") = strorden
'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
'Valida que la orden no se haya cerrado anteriormente
strrowcolor = "#ffffff"
strSQL = "select count(t$pdno) Contadorc " & _
"from baan.tticol105" & Session("env") & _ 
" where t$pdno = '" + Session("order") + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
Contc = objrs.Fields("Contadorc").Value

If (Contc = 0) Then
    'Validar Orden con estatus completado
    strSQL = "select t$pdno orden from baan.ttisfc001" & Session("env") & " where t$pdno='" + session("order") + "'" + " and t$osta = 9 "
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)

    If Not objrs.EOF Then
	    strorden = objrs.Fields("orden").Value
%>
<tr bgcolor="#FF0000">
    <td class="style1"><b>User:</b></td>
    <td class="style1"><b><%=Session("user")%></b></td>
</tr>
<%
        'Valida que no tenga nada en cuarentena
        strrowcolor = "#ffffff"
        strSQL = "select count(t$pdno) Contador " & _
        "from baan.tticol100" & Session("env") & _ 
        " where t$proc = 2 and t$pdno = '" + Session("order") + "'"
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
        Cont = objrs.Fields("Contador").Value

        If (Cont = "0") Then

            strSQL = "select sfc001.t$pdno Orden, sfc001.t$mitm Articulo, ibd001.t$dsca Descripcion, sfc001.t$qrdr Cantidad " & _
            "from baan.ttisfc001" & Session("env") & " sfc001 inner join baan.ttcibd001" & Session("env") & _
            " ibd001 on ibd001.t$item = sfc001.t$mitm " & _
            " where sfc001.t$pdno='" + Session("order") + "'"
            objrs=Server.CreateObject("ADODB.recordset")
            objrs.Open (strSQL, Odbcon)
            Do While Not objrs.EOF
            Session("strmsg") = " "
            strorden = objrs.Fields("Orden").Value  
            strart = objrs.Fields("Articulo").Value
            strdesca = objrs.Fields("Descripcion").Value
%>
<tr bgcolor="<%=strrowcolor%>">
    <td class="style2"><b>Order</b></td>
    <td class="style2" colspan="3"><%=objrs.Fields("Orden").Value%></td>
</tr>
<tr bgcolor="<%=strrowcolor%>">
    <td class="style2"><b>Item</b></td>
    <td class="style2" colspan="3"><%=objrs.Fields("Articulo").Value%> <%=objrs.Fields("Descripcion").Value%></td>
</tr>

<tr bgcolor="<%=strrowcolor%>">
    <td class="style2"><b>Quantity Ordered</b></td>
    <td class="style2" colspan="3"><%=objrs.Fields("Cantidad").Value%></td>
</tr>
<input type='hidden' name='orden' value='<%=strorden%>'/>
<%      
            i = i + 1    
			objrs.MoveNext
			Loop
        else
            Session("strmsg") = "Order have items rejected."
	        Response.Redirect("whInvCloseOrders.aspx?flag=")  
        End if
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
    else
	    Session("strmsg") = "Order status not completed."
	    Response.Redirect("whInvCloseOrders.aspx?flag=") 
    End if
else
    Session("strmsg") = "Order was Closed already."
	Response.Redirect("whInvCloseOrders.aspx?flag=")  
End if
%>
</table>
</form>
</body>
</html>
