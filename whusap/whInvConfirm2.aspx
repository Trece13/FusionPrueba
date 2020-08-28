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
    function deshabilitar_lote(desactivar, field, lote1) {
        var valor = lote1;
        if ((valor) == 1) {
        }
        else
        {
            document.getElementById(desactivar).disabled = true;
            document.getElementById(desactivar).value = ' ';
            alert("Item doesn't use lots, don't use this field.")
        }
    };

    function devolver_datos_nueva() {
        var indicec = document.frminq.confirma.selectedIndex
        var valorc = document.frminq.confirma.options[indicec].value
        var textoEscogidoc = document.frminq.confirma.options[indicec].text
        document.frminq.valorconfirma.value = valorc
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
            width: 495px;
        }
    .style3
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 59px;
    }
    .style4
    {
        color: Black;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 220px;
    }
    .style5
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 220px;
    }
    .style6
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 253px;
    }
    .style7
    {
        color: Black;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 253px;
    }
</style>
</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" style="width: 292px">
<TABLE style="width: 35%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td><H3>Delivery Confirm</H3></td>
</TABLE>
    <p style="width: 500px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<form name="frminq" method="post" action="whInvsaveconfirm.aspx" 
    style="height: 131px; width: 500px">
<table align="left" class="tableDefault4" border="1" cellspacing="0" 
    cellpadding="0">
<tr>
<td colspan="4" align="center">
<input type="submit" name="btnLogin" value="Save Confirm  "/>
</td>
</tr>
<%
Dim strsugerencia, strorigen, strrowcolor, strSQL, objrs, Odbcon, strmsg, strorden, strart, struni, strcant, strubi, strbloq
Dim strfuen, strlote, strubicacion, strconj, strpos, strsec, strser, strcant_sug, strdesc
Dim vconj, vpos, vser, vsec, valm, vubica1
strsugerencia = Mid(ucase(trim(Request.Form("sugerencia"))),1,9)
session("sugerencia") = strsugerencia
strorigen = ucase(trim(Request.Form("valororigen")))
session("origen") = strorigen
vconj=Mid(ucase(trim(Request.Form("sugerencia"))),10,2)
vpos=Mid(ucase(trim(Request.Form("sugerencia"))),12,3)
vsec=Mid(ucase(trim(Request.Form("sugerencia"))),15,2)
vser=Mid(ucase(trim(Request.Form("sugerencia"))),17,2)
valm = Mid(UCase(Request("sugerencia")), 1, 6)
vubica1 = Mid(UCase(Request("Dato")), 7, 15)
'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%

'Validar Recepcion
strSQL = "select rtrim(t$orno) sugerencia from baan.twhcol080" & session("env") & " where t$orno='" + Session("sugerencia") + "'" + " and t$sour = '" + Session("origen") +"'" + " and t$conf = 2 and t$orig = 3"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
	strsugerencia = objrs.Fields("sugerencia").Value
%>
<tr bgcolor="#FF0000">
    <td class="style1" colspan="1"><b>User:</b></td>
    <td class="style1" colspan="3"><b><%=Session("user")%></b></td>
</tr>
<tr bgcolor="#FF0000">
    <td class="style1" colspan="1"><b>Name :</b></td>
    <td class="style1" colspan="3"><b><%=Session("username")%></b></td>
</tr>
<%
strrowcolor = "#ffffff"
    strSQL = "select t$orno Orden, mov.t$item Articulo, t$dsca Descripcion, t$qana Cantidad, t$clot Lote, art.t$cuni Unidad, t$sour Fuente, " & _
    "t$conj Conjunto, t$pono Posicion, t$sqnb Secuencia, t$sern Serial " & _
    "from baan.twhcol080" & session("env") & " mov, baan.ttcibd001" & session("env") & " art " & _ 
    "where mov.t$item = art.t$item and t$orno='" + Session("sugerencia") + "'" + " and t$sour = '" + Session("origen") +"'" + " and t$conf = 2 and t$orig = 3"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    Do While Not objrs.EOF
    Session("strmsg") = " "
    strsugerencia = objrs.Fields("Orden").Value
    strart = objrs.Fields("Articulo").Value
    strdesc = objrs.Fields("Descripcion").Value
    strcant = objrs.Fields("Cantidad").Value
    strlote = objrs.Fields("Lote").Value
    struni = objrs.Fields("Unidad").Value
    strfuen = objrs.Fields("Fuente").Value
    strconj = objrs.Fields("Conjunto").Value
    strpos = objrs.Fields("Posicion").Value
    strsec = objrs.Fields("Secuencia").Value
    strser = objrs.Fields("Serial").Value

    If strrowcolor = "#87CEEB" Then 
        strrowcolor = "#D3D3D3" 
    Else 
        strrowcolor = "#87CEEB" 
    End if                   
%>
<tr bgcolor="#FF0000">
    <td class="style1" colspan="1"><b>Order</b></td>
    <td class="style1" colspan="3"><%=objrs.Fields("Orden").Value%></td>
</tr>

<tr bgcolor="#FF0000">
    <td class="style6"><b>Item</b></td>
    <td class="style5"><b>Lot</b></td>
    <td class="style1"><b>Sugest Qty</b></td>
    <td class="style1"><b>Confirm</b></td>
</tr>
<tr bgcolor="<%=strrowcolor%>">
    <td class="style7"><%=objrs.Fields("Articulo").Value%>  <%=objrs.Fields("Descripcion").Value%></td>
    <td class="style4"><%=objrs.Fields("Lote").Value%></td>
    <td class="style2"><%=objrs.Fields("Cantidad").Value%>  <%=objrs.Fields("Unidad").Value%></td>
    <td class="style3" colspan="2">
        <select name="confirma" style="width: 57px">
              <option value="1">Yes</option>
              <option value="2" selected>No</option>
        </select>
    </td>
    <input type='hidden' name='sugerencia' value='<%=strsugerencia%>'/>
    <input type='hidden' name='art' value='<%=strart%>'/>
    <input type='hidden' name='uni' value='<%=struni%>'/>
    <input type='hidden' name='fuen' value='<%=strfuen%>'/>
    <input type='hidden' name='conj' value='<%=strconj%>'/>
    <input type='hidden' name='pos' value='<%=strpos%>'/>
    <input type='hidden' name='sec' value='<%=strsec%>'/>
    <input type='hidden' name='ser' value='<%=strser%>'/>
    <input type='hidden' name='lot' value='<%=strlote%>'/>
    <input type='hidden' name='valorconfirma' value='2'/>
</tr>
<%
			    objrs.MoveNext
			    Loop
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
else
    Session("strmsg") = "Delivery doesn't exist or already processed."
	Response.Redirect("whInvConfirm.aspx?flag=") 
End if
%>
</table>
</form>
</body>
</html>
