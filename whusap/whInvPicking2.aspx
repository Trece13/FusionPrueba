<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "Debe loguearse antes de ejecutar esta opcion.."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>
<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<head>

<script type="text/javascript">
    function deshabilitar_lote(desactivar, field, lote1) {
        var valor = lote1;
        if ((valor) == 1) {
        }
        else
        {
            document.getElementById(desactivar).disabled = true;
            document.getElementById(desactivar).value = ' ';
            alert("Item doesn't use lot, don't use this field.")
        }
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
        width: 250px;
    }
        .style2
        {
        color: Black;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 250px;
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
    .style4
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 17px;
    }
    .style5
    {
        color: Black;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        }
    .style6
    {
    }
    .style7
    {
        color: Black;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 17px;
    }
    </style>
</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" style="width: 292px">
<TABLE style="width: 35%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td><H3>Finalizar Recoleccion</H3></td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_cerrarsesion.jpg"/></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"/></a>
     </p>
<form name="frminq" method="post" action="whInvsavepicking.aspx" 
    style="height: 131px; width: 382px">
<table align="left" class="tableDefault4" border="1" cellspacing="0" 
    cellpadding="0">
<tr>
<td colspan="2" align="center">
<input type="submit" name="btnLogin" value="   Finalizar Recoleccion  "/>
</td>
</tr>
<%
Dim strsugerencia, strrowcolor, strSQL, objrs, Odbcon, strmsg, strorden, strart, struni, strrecep
Dim strfuen, strconj, strpos, strsec, strmlote
strsugerencia = ucase(trim(Request.Form("sugerencia")))
session("sugerencia") = strsugerencia
    'Conect to database and execute spC:\Users\Md\Documents\Desarrollo Fusion\Ultimo Desarrollo Fusion\DAL\twhcol080.cs
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%

'Validar Recepcion
strSQL = "select rtrim(t$orno) sugerencia from baan.twhcol080" & session("env") & " where t$orno='" + Session("sugerencia") + "'" + " and t$proc = 2 "
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
	strsugerencia = objrs.Fields("sugerencia").Value
%>
<tr bgcolor="#FF0000">
    <td class="style4"><b>Usuario:</b></td>
    <td class="style1"><b><%=Session("user")%></b></td>
</tr>
<tr bgcolor="#FF0000">
    <td class="style4"><b>Name:</b></td>
    <td class="style1"><b><%=Session("username")%></b></td>
</tr>
<%
strrowcolor = "#ffffff"
    strSQL = "select t$orno Orden, t$sour Fuente " & _
    "from baan.twhcol080" & session("env") & " " & _ 
    "where t$orno='" + Session("sugerencia") + "'" + " and t$proc = 2 " & _
    "group by t$orno, t$sour "
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    Do While Not objrs.EOF
    Session("strmsg") = " "
    strsugerencia = objrs.Fields("Orden").Value
    
    If strrowcolor = "#87CEEB" Then 
        strrowcolor = "#D3D3D3" 
    Else 
        strrowcolor = "#87CEEB" 
    End if                   
%>
<tr bgcolor="<%=strrowcolor%>">
    <td class="style7"><b>Orden</b></td>
    <td class="style2"><%=objrs.Fields("Orden").Value%></td>
</tr>

<tr bgcolor="<%=strrowcolor%>">
    <td class="style5" colspan="2"><b>Cantidad</b></td>
</tr>
<tr bgcolor="<%=strrowcolor%>">
    <td class="style6" colspan="2"><input name="cant" size="10" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" onchange="deshabilitar_lote('lote', this, <%=strmlote%>)"/></td>
    <td><input type='hidden' name='sugerencia' value='<%=strsugerencia%>'/></td>
    <td><input type='hidden' name='fuen' value='<%=objrs.Fields("Fuente").Value%>'/></td>
</tr>
<%
			    objrs.MoveNext
			    Loop
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
else
    Session("strmsg") = "Recoleccion no existe o ya fue finalizada."
	Response.Redirect("whInvPicking.aspx?flag=") 
End if
%>
</table>
</form>
</body>
</html>
