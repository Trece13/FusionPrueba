<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must login first, before use this session."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>
<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<script type="text/javascript">
    function validar_cant(field, exis, sug) {
        var error = "";
        var ingresada = 0;
        var regex = /^\d*[0-9]*[,.]?[0-9]*$/;
        var re = new RegExp(regex);

        // Validar ingrese solo digitos enteros
        if (field.value.match(re)) {
            ingresada = field.value;
            //if (isNaN(field.value)) {
            //    field.value = 0;
            //}
            if (sug >= ingresada) {
                      
            }
            else {
                var r = alert("Quantity enter doesn't correspond with quantity original.  Please check");
                field.value = "";
                this.focus();
            }
            if (r == false) {
                field.value = "";
                //  this.focus();
            }
        } else {
            this.focus();
            field.value = 0;
            alert("Only number here");
        }
    };

    function validar_ubicacion(field, ubicac) {
        var ubicacion = ubicac;
        var ubicacion1 = field.value;
        var ubicatemp = ubicacion1.toUpperCase();
        if (ubicatemp == ubicacion) {
            
        }else {
            var r = alert("Location incorrect, please check");
            this.focus();
            field.value = "";
        }
    };

    function validar_lote(field, lot) {
        var lote = lot;
        var lote2 = field.value;
        var lote1 = lote2.substr(0, 9);
        var lotetemp = lote1.toUpperCase();
        if (lotetemp == lote) {

        } else {
            var r = alert("Lot incorrect, please check");
            this.focus();
            field.value = "";
        }
    };
       
</script>
<head>

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
    .style3
    {
        color: Black;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 66px;
    }
    .style4
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 66px;
    }
    </style>

</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" style="width: 292px">
<TABLE style="width: 35%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td><H3>Item Picking</H3></td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<form name="frminq" method="post" action="whInvsaveadvice.aspx" 
    style="height: 123px; width: 382px">
<table align="left" class="tableDefault4" border="1" cellspacing="0" 
    cellpadding="0">
<tr>
<td colspan="4" align="center">
<input type="submit" name="btnLogin" value="Save Picking Data  ">
</td>
</tr>
<%
Dim strsugerencia, strorigen, strrowcolor, strSQL, objrs, Odbcon, strmsg, strorden, strart, struni, strubi, strbloq
Dim strfuen, strlote, strubicacion, stralmacen, strconj, strpos, strsec, strser, strcant_ped, strcant_sug, strvalm, strvubi
Dim vconj, vpos, vser, valm, vubica1, vsec

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

'Validar Outbound
strSQL = "select rtrim(t$orno) sugerencia, t$cwar almacen from baan.twhinh225" & session("env") & " inb where " & _
"t$orno ='" + Session("sugerencia") + "'" + " and t$rlsd = 2 " & _
"and t$oset ='" + vconj + "'" + "and t$pono ='" + vpos + "'" + "and t$sern ='" + vser + "'" + "and t$seqn ='" + vsec + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
    strsugerencia = objrs.Fields("sugerencia").Value
    strvalm = objrs.Fields("almacen").Value
    
    'Validar Almacen Maneje Ubicaciones
    strSQL = "select t$sloc mubica from baan.twhwmd200" & session("env") & " inb where " & _
        "t$cwar ='" + strvalm + "'"
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
        strvubi = objrs.Fields("mubica").Value
        If (strvubi <> 2) Then
%>
<tr bgcolor="#FF0000">
    <td class="style1"><b>User:</b></td>
    <td class="style4" colspan="3"><b><%=Session("user")%></b></td>
</tr>
<tr bgcolor="#FF0000">
    <td class="style1"><b>Name:</b></td>
    <td class="style4" colspan="3"><b><%=Session("username")%></b></td>
</tr>
<%
        strrowcolor = "#ffffff"
        strSQL = "select t$orno Orden, inb.t$item Articulo, art.t$dsca Descripcion, t$atun Unidad, t$clot Lote, t$astk Cant_Pedida, " & _
        "t$astr Cant_Sugerida, inb.t$oorg Fuente, inb.t$oset Conjunto, inb.t$pono Posicion, inb.t$sern Serial, inb.t$seqn Secuencia, inb.t$clot Lote, inb.t$cwar Almacen, t$loca Ubicacion " & _
        "from baan.twhinh225" & session("env") & " inb, baan.ttcibd001" & session("env") & " art " & _ 
        "where inb.t$item = art.t$item and t$orno='" + Session("sugerencia") + "'" + " and t$rlsd = 2 " & _
        "and t$oset ='" + vconj + "'" + "and t$pono ='" + vpos + "'" + "and t$sern ='" + vser + "'" + "and t$seqn ='" + vsec + "'"
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
        Do While Not objrs.EOF
            strsugerencia = objrs.Fields("Orden").Value
            strart = objrs.Fields("Articulo").Value
            struni = objrs.Fields("Unidad").Value
            strlote = trim(objrs.Fields("Lote").Value)
            stralmacen = trim(objrs.Fields("Almacen").Value) 
            strubicacion = trim(objrs.Fields("Ubicacion").Value)
            strcant_ped = objrs.Fields("Cant_Pedida").Value
            strcant_sug = objrs.Fields("Cant_Sugerida").Value
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
    <td class="style1"><b>Order</b></td>
    <td class="style1" colspan="3"><%=strsugerencia%><%=strconj%><%=strpos%><%=strsec%></td>
</tr>
<tr bgcolor="#FF0000">
    <td class="style1"><b>Item</b></td>
    <td class="style1" colspan="3"><%=objrs.Fields("Articulo").Value%> <%=objrs.Fields("Descripcion").Value%></td>
</tr>

<tr bgcolor="<%=strrowcolor%>">
    <td class="style2"><b>Advice Qty</b></td>
    <td class="style3"><b>Unit</b></td>
    <td class="style3"><b>Warehouse</b></td>
    <td class="style2"><b>Location</b></td>
    <td class="style2"><b>Lot</b></td>
</tr>

<tr bgcolor="#FF0000">
    <td class="style1"><b><%=strcant_sug%></b></td>
    <td class="style1"><b><%=struni%></b></td>
    <td class="style1"><b><%=stralmacen%></b></td>
    <td class="style1"><b><%=strubicacion%></b></td>
    <td class="style1"><b><%=strlote%></b></td>
</tr>
<tr bgcolor="<%=strrowcolor%>">
    <td><input type='text' name="cant" size="10" 
            style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" 
            onchange='validar_cant(this, "<%=strcant_ped%>", "<%=strcant_sug%>");'/></td>
    <td class="style3"><%=objrs.Fields("Unidad").Value%></td>
    <td class="style3"><%=objrs.Fields("Almacen").Value%></td>
    <td><input type='text' id="ubi" name="ubica" size="20" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" onchange='validar_ubicacion(this, "<%=strubicacion%>");'/></td>
    <td><input type='text' id="lot" name="lote" size="20" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;"  onchange='validar_lote(this, "<%=strlote%>");'/></td>
    <td><input type='hidden' name='sugerencia' value='<%=strsugerencia%>'/></td>
	<td><input type='hidden' name='art' value='<%=strart%>'/></td>
    <td><input type='hidden' name='lot' value='<%=strlote%>'/></td>
    <td><input type='hidden' name='alm' value='<%=stralmacen%>'/></td>
    <td><input type='hidden' name='ubi' value='<%=strubicacion%>'/></td>
    <td><input type='hidden' name='uni' value='<%=struni%>'/></td>
    <td><input type='hidden' name='fuen' value='<%=strfuen%>'/></td>
    <td><input type='hidden' name='conj' value='<%=strconj%>'/></td>
    <td><input type='hidden' name='pos' value='<%=strpos%>'/></td>
    <td><input type='hidden' name='sec' value='<%=strsec%>'/></td>
    <td><input type='hidden' name='ser' value='<%=strser%>'/></td>
    <td><input type='hidden' name='ori' value='<%=strorigen%>'/></td>
</tr>
<%
        objrs.MoveNext
		Loop
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
    Else
	    Session("strmsg") = "Warehouse doesn't use locations."
    	Response.Redirect("whInvAdvices.aspx?flag=Y") 
    End If
else
	Session("strmsg") = "Order doesn't exist."
	Response.Redirect("whInvAdvices.aspx?flag=Y") 
End if
%>
</table>
</form>
</body>
</html>
