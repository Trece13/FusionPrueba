<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "Debe loguearse antes de ejecutar esta opcion."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>
<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<head>

<script type="text/javascript">

    function deshabilitar_lote(desactivar, field, lote1, i) {
        var valor = lote1;
        var lote1 = field.value;
        if (i == 0) {
            var cant = document.frminq.cant[0].value;
        }
        else {
            var cant = document.frminq.cant[i].value;
        }
        if (cant != "") {
            if ((valor) == 1) {
                if (lote1 == "0" || lote1 == "" || lote1 == " " || lote1 == "  " || lote1 == "   ") {
                    alert("Item have lot control.  You must insert lot.")
                    //field.value = '';
                    if (i == 0) {
                        document.frminq.lote[0].focus(); //field.focus();
                    }
                    else {
                        document.frminq.lote[i].focus(); //field.focus();
                    }
                }
            }
            else {
                //alert("Item doesn't use lots - don't save information here.")
                if (i == 0) {
                    document.frminq.lote[0].focus();  //field.value = '';
                }
                else {
                    document.frminq.lote[i].focus();  //field.value = '';
                }
            }
        }
        else {
            if (i == 0) {
                document.frminq.cant[0].focus();
            }
            else {
                document.frminq.cant[i].focus();
            }
        }            
    }
    
    function Convertir(field, i){
        document.frminq.cant[i].value = ConvertirFormato(document.frminq.cant[i].value);
    }

    function ConvertirFormato(pValor) {
        var valor = format("#,##0.####", pValor);
        return valor;
    }

    window.format = function (b, a) {
        if (!b || isNaN(+a)) return a;
        var a = b.charAt(0) == "-" ? -a : +a, j = a < 0 ? a = -a : 0, e = b.match(/[^\d\-\+#]/g), h = e && e[e.length - 1] || ",", e = e && e[1] && e[0] || ".", b = b.split(h), a = a.toFixed(b[1] && b[1].length), a = +a + "", d = b[1] && b[1].lastIndexOf("0"), c = a.split(",");
        if (!c[1] || c[1] && c[1].length <= d) a = (+a).toFixed(d + 1); d = b[0].split(e); b[0] = d.join(""); var f = b[0] && b[0].indexOf("0");
        if (f > -1) for (; c[0].length < b[0].length - f; ) c[0] = "0" + c[0]; else+c[0] == 0 && (c[0] = ""); a = a.split(","); a[0] = c[0]; if (c = d[1] && d[d.length -
1].length) { for (var d = a[0], f = "", k = d.length % c, g = 0, i = d.length; g < i; g++) f += d.charAt(g), !((g - k + 1) % c) && g < i - c && (f += e); a[0] = f } a[1] = b[1] && a[1] ? h + a[1] : ""; return (j ? "-" : "") + a[0] + a[1]
    }

    function Valida_Hablador(field) {
        var valorh = field;
        if (valorh == "0" || valorh == "" || valorh == " " || valorh == "  " || valorh == "   ") {
            alert("Field pallet must have value")
            field.focus();
        }
    }

    function checklot(mlot, i, field) {
    debugger
        var mlot1 = mlot
        var j = document.getElementById("j1").value;
        if (i == 0) {
            var cant = field.value;  //document.frminq.cant[i].value;
        }
        else {
                var cant = document.frminq.cant[i].value;
        }
        if (cant != "") {
            if (mlot1 == 1) {
                //alert("Item have lot control.  You must insert lot.")
                if ((i == 0) && (j==1)) {
                    document.frminq.lote.disabled = false;  //document.getElementById("lote1")[i].disabled = false;
                    document.frminq.lote.focus();  //document.getElementById("lote1")[i].focus();
                }
                else {
                    document.frminq.lote[i].disabled = false;  //document.getElementById("lote1")[i].disabled = false;
                    document.frminq.lote[i].focus();  //document.getElementById("lote1")[i].focus();
                }
            }
            else {
                if (i == 0) {
                    document.frminq.lote.disabled = true;  //document.getElementById("lote1")[i].disabled = true;
                    document.frminq.habl1.focus();  //document.getElementById("habl1")[i].focus();
                }
                else {
                    document.frminq.lote[i].disabled = true;  //document.getElementById("lote1")[i].disabled = true;
                    document.frminq.habl1[i].focus();  //document.getElementById("habl1")[i].focus();
                }
            }
        }
        else {
            document.frminq.cant[i].focus();
        } 
    }

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
    .style3
    {
        color: Black;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 18px;
    }
    .style4
    {
        color: Black;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 85px;
    }
    .style5
    {
        width: 85px;
    }
    </style>
</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" onload="" style="width: 292px">
<TABLE style="width: 35%; height: 17px">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H3>Recepcion Articulo</H3></td>
</tr>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_cerrarsesion.jpg"/></a>
        <a href="whMenui.aspx"><img src="images/btn_mainmenu.JPG"/></a>
     </p>
<form name="frminq" method="post" action="whInvsavereceipt.aspx" 
    style="height: 131px; width: 382px">
<table align="left" class="tableDefault4" border="1" cellspacing="0" 
    cellpadding="0">
<tr>
<td colspan="4" align="center">
<input type="submit" name="btnLogin" value="Salvar Recepcion  "/>
</td>
</tr>
<%
Dim strrecepcion, strrowcolor, strSQL, objrs, Odbcon, strmsg, strorden, strart, struni, strrecep, strloteo
Dim strfuen, strconj, strpos, strsec, strmlote, i, j
strrecepcion = ucase(trim(Request.Form("recep")))
session("recep") = strrecepcion
'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%

'Validar Recepcion
strSQL = "select rtrim(t$rcno) recepcion from baan.twhinh310" & Session("env") & " where t$rcno='" + Session("recep") + "'" + " and t$conf = 2 "
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
	strrecepcion = objrs.Fields("recepcion").Value
%>
<tr bgcolor="#FF0000">
    <td class="style1"><b>Usuario:</b></td>
    <td class="style1" colspan="3"><b><%=Session("user")%></b></td>
</tr>
<tr bgcolor="#FF0000">
    <td class="style1"><b>Nombre :</b></td>
    <td class="style1" colspan="3"><b><%=Session("username")%></b></td>
</tr>
<tr bgcolor="#FF0000"> 
	<td class="style1"><b>Receipt:</b></td>
	<td class="style1" colspan="3"><%=strrecepcion%></td>
</tr>
<%
strrowcolor = "#ffffff"
strSQL = "select t$orno Orden, rec.t$item Articulo, art.t$dsca Descripcion, t$rcun Unidad " & _
"from baan.twhinh210" & Session("env") & " rec, baan.ttcibd001" & Session("env") & " art " & _ 
"where rec.t$item = art.t$item and t$rcno='" + Session("recep") + "'" + " and t$conf = 2 "
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
i = 0

If Not objrs.EOF Then

    strSQL = "select count(*) temp " & _
    "from baan.twhinh210" & Session("env") & " rec, baan.ttcibd001" & Session("env") & " art " & _ 
    "where rec.t$item = art.t$item and t$rcno='" + Session("recep") + "'" + " and t$conf = 2 "
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If Not objrs.EOF Then
        j = objrs.Fields("temp").Value
    Else
        j = 0
    End if

    
    strSQL = "select t$orno Orden, rec.t$item Articulo, art.t$dsca Descripcion, t$rcun Unidad, " & _
    "t$oorg Fuente, t$oset Conjunto , t$pono Posicion, t$seqn Secuencia, t$kltc Mlote, t$clot Loteo " & _
    "from baan.twhinh210" & Session("env") & " rec, baan.ttcibd001" & Session("env") & " art " & _ 
    "where rec.t$item = art.t$item and t$rcno='" + Session("recep") + "'" + " and t$conf = 2 " & _
    " order by t$oorg, t$orno, t$oset, t$pono, t$seqn "
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    Do While Not objrs.EOF

        Session("strmsg") = " "
        strorden = objrs.Fields("Orden").Value
        strart = objrs.Fields("Articulo").Value
        struni = objrs.Fields("Unidad").Value
        strfuen = objrs.Fields("Fuente").Value
        strconj = objrs.Fields("Conjunto").Value
        strpos = objrs.Fields("Posicion").Value
        strsec = objrs.Fields("Secuencia").Value
        strmlote = objrs.Fields("Mlote").Value
        strloteo = objrs.Fields("loteo").Value
        strrecep = Session("recep")

        If strrowcolor = "#87CEEB" Then 
            strrowcolor = "#D3D3D3" 
        Else 
            strrowcolor = "#87CEEB" 
        End if                   
%>
<tr bgcolor="<%=strrowcolor%>">
    <td class="style2"><b>Orden</b></td>
    <td class="style2" colspan="3"><%=objrs.Fields("Orden").Value%></td>
</tr>
<tr bgcolor="<%=strrowcolor%>">
    <td class="style2"><b>Articulo</b></td>
    <td class="style2" colspan="3"><%=objrs.Fields("Articulo").Value%> <%=objrs.Fields("Descripcion").Value%></td>
</tr>

<tr bgcolor="<%=strrowcolor%>">
    <td class="style2"><b>Cantidad</b></td>
    <td class="style3"><b>Un</b></td>
    <td class="style2"><b>Lote</b></td>
    <td class="style4"><b>Cant Identificadores</b></td>
</tr>
<tr bgcolor="<%=strrowcolor%>">
    <td><input id="cant" name="cant" size="10" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" onchange="checklot(<%=strmlote%>, <%=i%>, this);"/></td>
    <td class="style3"><%=objrs.Fields("Unidad").Value%></td>
    <td><input id="lote" name="lote" size="10" 
            style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal; width: 141px;" 
            value="<%=strloteo%>" disabled="disabled"/></td>
    <td class="style5"><input id="habl1" name="habl" size="10" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" /></td>
    <input type='hidden' name='recep' value='<%=strrecep%>'/>
    <input type='hidden' name='orden' value='<%=strorden%>'/>
	<input type='hidden' name='art' value='<%=strart%>'/>
    <input type='hidden' name='uni' value='<%=struni%>'/>
    <input type='hidden' name='fuen' value='<%=strfuen%>'/>
    <input type='hidden' name='conj' value='<%=strconj%>'/>
    <input type='hidden' name='pos' value='<%=strpos%>'/>
    <input type='hidden' name='sec' value='<%=strsec%>'/>
    <input type='hidden' name='mlot' value='<%=strmlote%>'/>
    <input type='hidden' id='j1' name='j1' value='<%=j%>'/>
</tr>
<%            
        i = i + 1 
		objrs.MoveNext
		Loop       
else
    Session("strmsg") = "Recepcion ya confirmada or no existe."
	Response.Redirect("whInvReceipts.aspx?flag=") 
End if
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
  
else
	Session("strmsg") = "Recepcion no existe."
	Response.Redirect("whInvReceipts.aspx?flag=") 
End if
%>
</table>
</form>
</body>
</html>
