<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "Debe loguearse antes de ejecutar esta opcion."
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
                var r = alert("Cantidad Ingresada no corresponde con la original.  Por favor revise.");
                field.value = "";
                this.focus();     
            }
            //else {
            //    var r = alert("Cantidad Ingresada no corresponde con la original.  Por favor revise.");
            //    field.value = "";
            //    this.focus();
            //}
            if (r == false) {
                field.value = "";
                //  this.focus();
            }
        } else {
            this.focus();
            field.value = 0;
            alert("Solo se permiten numeros ");
        }
    };

    function validar_ubicacion(field, ubicac) {
        var ubicacion = ubicac;
        var ubicacion1 = field.value;
        var ubicatemp = ubicacion1.toUpperCase();
        if (ubicatemp == ubicacion) {
            
        }else {
            var r = alert("Ubicacion incorrecta, por favor verifique");
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
            var r = alert("Lote incorrecto, por favor verifique");
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
            width: 340px;
        }
    .style3
    {
        color: Black;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 43px;
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
    .style5
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 68px;
    }
    .style6
    {
        color: Black;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 68px;
    }
    .style7
    {
        width: 68px;
    }
    .style8
    {
        color: Black;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 36px;
    }
    .style9
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 36px;
    }
    .style10
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 43px;
    }
    .style11
    {
        color: Black;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 80px;
    }
    .style12
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 80px;
    }
    .style13
    {
        width: 80px;
    }
    </style>

</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" style="width: 292px">
<TABLE style="width: 35%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td><H3>Recoleccion Articulo</H3></td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_cerrarsesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<form name="frminq" method="post" action="whInvsaveadvice.aspx" 
    style="height: 123px; width: 342px">
<table align="left" class="tableDefault4" border="1" cellspacing="0" 
    cellpadding="0">
<tr>
<td colspan="5" align="center">
<input type="submit" name="btnLogin" value="Salvar Datos Recoleccion  ">
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
    <td class="style5"><b>Usuario:</b></td>
    <td class="style4" colspan="4"><b><%=Session("user")%></b></td>
</tr>
<tr bgcolor="#FF0000">
    <td class="style5"><b>Name:</b></td>
    <td class="style4" colspan="4"><b><%=Session("username")%></b></td>
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
    <td class="style5"><b>Orden</b></td>
    <td class="style1" colspan="4"><%=strsugerencia%><%=strconj%><%=strpos%><%=strsec%></td>
</tr>
<tr bgcolor="#FF0000">
    <td class="style5"><b>Articulo</b></td>
    <td class="style1" colspan="4"><%=objrs.Fields("Articulo").Value%> <%=objrs.Fields("Descripcion").Value%></td>
</tr>

<tr bgcolor="<%=strrowcolor%>">
    <td class="style6"><b>Cant Sugerida</b></td>
    <td class="style8"><b>Un</b></td>
    <td class="style3"><b>Alm</b></td>
    <td class="style11"><b>Ubicacion</b></td>
    <td class="style2"><b>Lote</b></td>
</tr>

<tr bgcolor="#FF0000">
    <td class="style5"><b><%=strcant_sug%></b></td>
    <td class="style9"><b><%=struni%></b></td>
    <td class="style10"><b><%=stralmacen%></b></td>
    <td class="style12"><b><%=strubicacion%></b></td>
    <td class="style1"><b><%=strlote%></b></td>
</tr>
<tr bgcolor="<%=strrowcolor%>">
    <td class="style7"><input type='text' name="cant" size="10" 
            style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" 
            onchange='validar_cant(this, "<%=strcant_ped%>", "<%=strcant_sug%>");'/></td>
    <td class="style8"><%=objrs.Fields("Unidad").Value%></td>
    <td class="style3"><%=objrs.Fields("Almacen").Value%></td>
    <td class="style13"><input type='text' id="ubi" name="ubica" size="20" 
            style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal; width: 80px;" 
            onchange='validar_ubicacion(this, "<%=strubicacion%>");'/></td>
    <td><input type='text' id="lot" name="lote" size="20" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;"  onchange='validar_lote(this, "<%=strlote%>");'/></td>
    <input type='hidden' name='sugerencia' value='<%=strsugerencia%>'/>
	<input type='hidden' name='art' value='<%=strart%>'/>
    <input type='hidden' name='lot' value='<%=strlote%>'/>
    <input type='hidden' name='alm' value='<%=stralmacen%>'/>
    <input type='hidden' name='ubi' value='<%=strubicacion%>'/>
    <input type='hidden' name='uni' value='<%=struni%>'/>
    <input type='hidden' name='fuen' value='<%=strfuen%>'/>
    <input type='hidden' name='conj' value='<%=strconj%>'/>
    <input type='hidden' name='pos' value='<%=strpos%>'/>
    <input type='hidden' name='sec' value='<%=strsec%>'/>
    <input type='hidden' name='ser' value='<%=strser%>'/>
    <input type='hidden' name='ori' value='<%=strorigen%>'/>
</tr>
<%
        objrs.MoveNext
		Loop
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
    Else
	    Session("strmsg") = "Almacen no utiliza ubicaciones"
    	Response.Redirect("whInvAdvices.aspx?flag=Y") 
    End If
else
Session("strmsg") = "Orden no existe."
	Response.Redirect("whInvAdvices.aspx?flag=Y") 
End if
%>
</table>
</form>
</body>
</html>
