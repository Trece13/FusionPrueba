<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must login firts, before use this session."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>
<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<head>
<script type="text/javascript">
    function validar_cant(field, factor) {
        var dividendo = field.value;
        var residuo = 0;
        var regex = /^-?\d*[0-9]*[.]?[0-9]*$/;
        var re = new RegExp(regex);
        if (field.value.match(re)) {
            residuo = dividendo % factor;
            if ((dividendo % factor) != 0) {
                alert("Is not a order quantity multiple");
            }
        }
        else {
            this.focus();
            field.value = 0;
            alert("Only numbers here");
        }
    };

    function validar_num(field) {
        //var regex = /^-?\d*[0-9]*[,.]?[0-9]*$/;
        var regex = /^([0-9])*$/;
        var re = new RegExp(regex);
        if (field.value.match(re)) {
                    }
        else {
            this.focus();
            field.value = 0;
            alert("Only numbers here, doesn't allowed decimals");
        }
    };

    function devolver_datosr() {
        var indicer = document.frminq.reason.selectedIndex
        var valorr = document.frminq.reason.options[indicer].value
        var textoEscogidor = document.frminq.reason.options[indicer].text
        document.frminq.reasonc.value = valorr
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
         .tableDefault4
        {
            width: 1195px;
        height: 127px;
    }
    #Text1
    {
        width: 60px;
    }
    #Horas
    {
        width: 50px;
        margin-left: 1px;
    }
    .style15
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 120px;
    }
    .style19
    {
        height: 49px;
    }
    .style20
    {
        width: 1px;
    }
    .style21
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 1px;
    }
    .style22
    {
        width: 120px;
    }
    .style23
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 160px;
    }
    .style24
    {
        width: 160px;
    }
    .style25
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 108px;
    }
    .style26
    {
        width: 108px;
    }
    .style27
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 33px;
    }
    .style28
    {
        width: 33px;
    }
    #TextArea1
    {
        width: 490px;
    }
    #obser
    {
        width: 460px;
    }
    </style>
</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" style="width: 292px">
<TABLE style="width: 100%; height: 17px">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H3>Items Rejected</H3></td>
</tr>
</TABLE>
    <p style="width: 800px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
     </p>
<form name="frminq" method="post" action="whInvSaveItemRejected.aspx" 
    style="height: 131px; width: 1200px">
<table align="left" class="tableDefault4" border="1" cellspacing="0" 
    cellpadding="0">
<tr>
<td colspan="8" align="center" class="style19" style="height: 50px">
<input type="submit" name="btnLogin" value="Save Item  "/>
</td>
</tr>
<%
Dim strorden, strrowcolor, strSQL, objrs, Odbcon, strmsg, strart, strdesca, stroper, strtar, strdesct, strunidad, i
strorden = mid(ucase(trim(Request.Form("orden"))),1,9)
session("orden") = strorden
'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%

'Validar Orden
strSQL = "select rtrim(cst001.t$pdno) orden from baan.tticst001" & session("env") & " cst001, baan.ttisfc001" & session("env") & " sfc001 " & _
"where cst001.t$pdno = sfc001.t$pdno and t$osta in('5','7') and cst001.t$pdno='" + Session("orden") + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
	strorden = objrs.Fields("orden").Value
%>
<tr bgcolor="#FF0000">
    <td class="style15"><b>User:</b></td>
    <td class="style23"><b><%=Session("user")%></b></td>
</tr>
<tr bgcolor="#FF0000">
    <td class="style15"><b>Name:</b></td>
    <td class="style23"><b><%=Session("username")%></b></td>
</tr>
<tr bgcolor="#FF0000"> 
	<td class="style15"><b>Order:</b></td>
	<td class="style1" colspan="2"><%=strorden%></td>
</tr>
<tr  bgcolor="#FF0000">
    <td class="style15"><b>Item</b></td>
    <td class="style23"><b>Description</b></td>
    <td class="style27"><b>Unit</b></td>
    <td class="style25"><b>Reason</b></td>
    <td class="style21"><b>Quantity</b></td>
    <td class="style21"><b>Exact Reason</b></td>
</tr>
<%
strrowcolor = "#ffffff"
strSQL = "select rec.t$pdno Orden, rec.t$pono Pos, rec.t$cwar Almacen, rec.t$clot Lote, rec.t$sitm Articulo, art.t$dsca Descripcion, art.t$cuni Unidad " & _
"from baan.tticst001" & session("env") & " rec, baan.ttcibd001" & session("env") & " art, baan.ttisfc001" & session("env") & " sfc " & _ 
"where rec.t$sitm = art.t$item and rec.t$pdno = sfc.t$pdno and sfc.t$osta in('5','7') and sfc.t$pdno='" + Session("orden") + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
i = 0
If Not objrs.EOF Then
    strSQL = "select  sfc.t$pdno Orden, sfc.t$mitm Articulo, art.t$dsca Descripciona, prod.t$opno Operacion, prod.t$tano Tarea, oper.t$dsca Descripciont, art.t$cuni Unidad " & _
    " from baan.ttisfc001" & session("env") & " sfc inner join baan.ttcibd001" & session("env") & " art on art.t$item = sfc.t$mitm " & _
    " inner join baan.ttisfc010" & session("env") & " prod on prod.t$pdno = sfc.t$pdno and prod.t$opno = 10 " & _ 
    " inner join baan.ttirou003" & session("env") & " oper on oper.t$tano = prod.t$tano " & _
    " where sfc.t$osta in('5','7') " & _
    " and sfc.t$pdno='" + Session("orden") + "'"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    Do While Not objrs.EOF
    Session("strmsg") = " "
    strorden = objrs.Fields("Orden").Value
    strart = objrs.Fields("Articulo").Value
    strdesca = objrs.Fields("Descripciona").Value
    stroper = objrs.Fields("Operacion").Value
    strtar = objrs.Fields("Tarea").Value
    strdesct = objrs.Fields("Descripciont").Value
    strorden = Session("orden")
    strunidad = objrs.Fields("Unidad").Value

    If strrowcolor = "#87CEEB" Then 
        strrowcolor = "#D3D3D3" 
    Else 
        strrowcolor = "#87CEEB" 
    End if                   
%>
<tr bgcolor="<%=strrowcolor%>">
    <td class="style22"><%=objrs.Fields("Articulo").Value%> </td>
    <td class="style24"><%=objrs.Fields("Descripciona").Value%>       
    <td class="style28"><%=strunidad%></td>
    <td class="style26">
    <select name="reason" onblur='devolver_datosr()' size="1" style="width: 240px">
    <%
     Dim strSQLh, objrsh, Odbconh
     'Abre la conexión con la base de datos a través de una conexión ODBC
    %>
    <!-- #include file="include/dbxcononusah.inc" -->
    <%
    strSQLh = "select reason, descr from (select 'Select Reason...' reason, ' ' descr from dual union select t$cdis reason, t$dsca descr from baan.ttcmcs005" & session("env") & _
        " where t$rstp = 10 " & _
        " ) order by descr"
    objrsh=Server.CreateObject("ADODB.recordset")
    objrsh.Open (strSQLh, Odbconh)
    Do While Not objrsh.EOF
        Response.Write("<option value='" & objrsh.Fields("reason").Value & "'>" & objrsh.Fields("reason").Value & " - " & objrsh.Fields("descr").Value & "</option>")
    objrsh.MoveNext
    Loop
    %>
    </select>
    <!-- #include file="include/dbxconoffh.inc" -->
    </td>
    <td class="style20"><input id="cant" name="cant" align="right" onchange="validar_num(this)" value='0'/></td>
    <td><textarea id="obser" name="obser" rows="2" style="overflow: auto"></textarea></td>
    <input type='hidden' name='orden' value='<%=strorden%>'/>
	<input type='hidden' name='art' value='<%=strart%>'/>
    <input type='hidden' name='desca' value='<%=strdesca%>'/>
    <input type='hidden' name='reasonc' value='NA'/>
</tr>
<%      
                i = i + 1    
			    objrs.MoveNext
			    Loop
else
    Session("strmsg") = "Order doesn't exist or the status is active, release or completed."
	Response.Redirect("whInvItemRejected.aspx?flag=") 
End if
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
  
else
	Session("strmsg") = "Order doesn't exist or the status is active, release or completed."
	Response.Redirect("whInvItemRejected.aspx?flag=") 
End if
%>
</table>
</form>
</body>
</html>
