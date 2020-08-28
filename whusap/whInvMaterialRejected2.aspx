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
    function setFocus() {
        document.getElementById("orden").focus();
    };

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
        //var regex = /^[0-9]+[0-9]$/;
        var digitada = field.value;
        var original = document.getElementById("cantc").value;
        var pos = document.getElementById("posc").value
        var regex = /^([0-9])*$/;
        var re = new RegExp(regex);
        if (field.value.match(re)) {
            if (pos != 0) {
                if (Number(digitada) > Number(original)) {
                    alert("Quantity greater than Actual Quantity. (" + original + ")");
                    this.focus();
                    field.value = 0;
                }
            }
        }
        else {
            this.focus();
            field.value = 0;
            alert("Only numbers here, doesn't allowed decimals.");
        }
    };

    function devolver_datosr() {
        var indicer = document.frminq.reason.selectedIndex
        var valorr = document.frminq.reason.options[indicer].value
        var textoEscogidor = document.frminq.reason.options[indicer].text
        document.frminq.reasonc.value = valorr
    };

    function devolver_datosm() {
        var indicer = document.frminq.material.selectedIndex;
        var valorr = document.frminq.material.options[indicer].value;
        var textoEscogidor = document.frminq.material.options[indicer].text;
        //document.frminq.posc.value = textoEscogidor.substring(0, 3);
        var value = document.getElementById("material").value;
        var split = value.split("|");
        var v1 = split[0];
        var v2 = split[1];
        var v3 = split[2];
        var v4 = split[3];
        var v5 = split[4];
        var v6 = split[5];
        var v7 = split[6];
        var v8 = split[7];

        document.frminq.materialc.value = v1;
        document.frminq.unidadc.value = v2;
        document.frminq.maquinac.value = v3;
        document.frminq.posc.value = v4;
        document.frminq.cantc.value = v5;
        document.frminq.turnoc.focus();
        var v10 = v1.substring(0, 10);
        var v9 = v10.substring(10, 1);
        if (v8 != 1) {
            document.frminq.lot.disabled = true;
            document.getElementById("lotem").value = v8;
        }
        else {
            document.frminq.lot.disabled = false;
            document.frminq.lotem.value = v8;
        }
        if (v9 != "        O") {
            document.frminq.supplier.disabled = false;
            document.frminq.nombres.disabled = true;
            document.frminq.supplier.value = v6;
            document.frminq.nombres.value = v7;
        }
        else {
            document.frminq.supplier.disabled = true;
            document.frminq.nombres.disabled = true;
            document.frminq.supplier.value = "";
            document.frminq.nombres.value = "";
        }
    };

    function devolver_datosrej() {
        var indicer = document.frminq.reject.selectedIndex
        var valorr = document.frminq.reject.options[indicer].value
        var textoEscogidor = document.frminq.reject.options[indicer].text
        document.frminq.rejectc.value = valorr
    };

    function devolver_datosrc() {
        var indicer = document.frminq.disposition.selectedIndex
        var valorr = document.frminq.disposition.options[indicer].value
        var textoEscogidor = document.frminq.disposition.options[indicer].text
        document.frminq.dispositionc.value = valorr
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
    .style2
    {
        height: 38px;
        width: 700px;
    }
    .style3
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 56px;
    }
    .style4
    {
        height: 38px;
        width: 56px;
    }
    .style7
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 78px;
    }
    .style8
    {
        height: 38px;
        width: 78px;
    }
    .style9
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 200px;
    }
    .style10
    {
        height: 38px;
        width: 200px;
    }
    #nombres
    {
        width: 197px;
    }
    #unidadc
    {
        width: 70px;
    }
    #maquinac
    {
        width: 83px;
    }
</style>
</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" style="width: 292px">
<TABLE style="width: 100%; height: 17px">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H3>Material Rejected</H3></td>
</tr>
</TABLE>
    <p style="width: 800px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
     </p>
<form name="frminq" method="post" action="whInvSaveMaterialRejected.aspx" 
    style="height: 285px; width: 600px">
<table align="left" class="style2" border="1" cellspacing="0" 
    cellpadding="0">
<tr>
<td colspan="4" align="center" class="style2" style="height: 50px">
<input type="submit" name="btnLogin" value="Save Item  "/>
</td>
</tr>
<%
Dim strorden, strrowcolor, strSQL, objrs, Odbcon, strmsg, strflag, strart, strdesca, stroper, strtar, strdesct, strunidad, strartp, strpos, strcantp, i, stritems
strorden = mid(ucase(trim(Request.Form("orden"))),1,9)
session("orden") = strorden
strflag = Request.QueryString("flag")
if strflag = "Y" then
  strmsg = Session("strmsg")
else
  strmsg = Session("strmsg")
end if

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
    <td class="style1"><b>User:</b></td>
    <td class="style3" colspan="3"><b><%=Session("user")%></b></td>
</tr>
<tr bgcolor="#FF0000">
    <td class="style1"><b>Name:</b></td>
    <td class="style1" colspan="3"><b><%=Session("username")%></b></td>
</tr>
<tr bgcolor="#FF0000"> 
	<td class="style1"><b>Order:</b></td>
	<td class="style1" colspan="3"><%=strorden%></td>
</tr>
<tr  bgcolor="#FF0000">
    <td class="style1"><b>Material</b></td>
    <td class="style9"><b>Unit</b></td>
    <td class="style7"><b>Machine</b></td>
    <td class="style1"><b>Shift</b></td>
</tr>
<%
strrowcolor = "#ffffff"
strSQL = "select rec.t$pdno Orden, rec.t$pono Pos, rec.t$cwar Almacen, rec.t$clot Lote, rec.t$sitm Articulo, art.t$dsca Descripcion, art.t$cuni Unidad, rec.t$qucs Cant " & _
"from baan.tticst001" & session("env") & " rec, baan.ttcibd001" & session("env") & " art, baan.ttisfc001" & session("env") & " sfc " & _ 
"where rec.t$sitm = art.t$item and rec.t$pdno = sfc.t$pdno and sfc.t$osta in('5','7') and sfc.t$pdno='" + Session("orden") + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
i = 0
If Not objrs.EOF Then       
    strartp = objrs.Fields("Articulo").Value
    strcantp = objrs.Fields("Cant").Value
    If strrowcolor = "#87CEEB" Then 
        strrowcolor = "#D3D3D3" 
    Else 
        strrowcolor = "#87CEEB" 
    End if                   
%>
<tr bgcolor="<%=strrowcolor%>">
    <td class="style2">
    <select id="material" name="material" onblur='devolver_datosm()' size="1" 
            style="width: 250px">
    <%
     Dim strSQLt, objrst, Odbcont
     'Abre la conexión con la base de datos a través de una conexión ODBC
    %>
    <!-- #include file="include/dbxcononusat.inc" -->
    <%
    strSQLt = "select ' ' Orden, 'Select Material ...' Articulo, ' ' Descripciona, 0 posicion, 0 Operacion, 0 Tarea, ' ' Descripciont, ' ' Unidad, ' ' Maquina, 0 Cant, ' ' Proveedor, ' ' Nombre, 0 mlote from dual union " & _
    "select  sfc.t$pdno Orden, cst001.t$sitm Articulo, art.t$dsca Descripciona, cst001.t$pono posicion, prod.t$opno Operacion, prod.t$tano Tarea, oper.t$dsca Descripciont, art.t$cuni Unidad, prod.t$mcno Maquina, cst001.t$qucs Cant, ipu.t$otbp Proveedor, com.t$nama Nombre, art.t$kltc mlote " & _
    " from baan.ttisfc001" & Session("env") & " sfc inner join baan.tticst001" & Session("env") & " cst001 on cst001.t$pdno = sfc.t$pdno " & _
    " inner join baan.ttcibd001" & Session("env") & " art on art.t$item = cst001.t$sitm " & _
    " left join baan.ttdipu001" & Session("env") & " ipu on ipu.t$item = cst001.t$sitm " & _
    " left join baan.ttccom100" & Session("env") & " com on com.t$bpid = ipu.t$otbp " & _
    " inner join baan.ttisfc010" & Session("env") & " prod on prod.t$pdno = sfc.t$pdno and prod.t$opno = 10 " & _
    " inner join baan.ttirou003" & Session("env") & " oper on oper.t$tano = prod.t$tano " & _
    " where sfc.t$osta in('5','7') " & _
    " and sfc.t$pdno='" + Session("orden") + "'" & _
    " union " & _
    " select  sfc.t$pdno Orden, sfc.t$mitm Articulo, art.t$dsca Descripciona, 0 posicion, 0 Operacion, 0 Tarea, ' ' Descripciont, art.t$cuni Unidad, prod.t$mcno Maquina, 0 Cant, ipu.t$otbp proveedor, com.t$nama Nombre, art.t$kltc mlote " & _
    " from baan.ttisfc001" & Session("env") & " sfc " & _
    " inner join baan.ttcibd001" & Session("env") & " art on art.t$item = sfc.t$mitm " & _
    " left join baan.ttdipu001" & Session("env") & " ipu on ipu.t$item = sfc.t$mitm " & _
    " left join baan.ttccom100" & Session("env") & " com on com.t$bpid = ipu.t$otbp " & _
    " inner join baan.ttisfc010" & Session("env") & " prod on prod.t$pdno = sfc.t$pdno and prod.t$opno = 10 " & _
    " where sfc.t$osta in('5','7') " & _
    " and sfc.t$pdno='" + Session("orden") + "'" & _
    " order by posicion "
    objrst=Server.CreateObject("ADODB.recordset")
    objrst.Open (strSQLt, Odbcont)
    Do While Not objrst.EOF
        Response.Write("<option value='" & objrst.Fields("Articulo").Value & "|" & objrst.Fields("Unidad").Value & "|" & objrst.Fields("Maquina").Value & "|" & objrst.Fields("posicion").Value & "|" & objrst.Fields("Cant").Value & "|" & objrst.Fields("Proveedor").Value & "|" & objrst.Fields("Nombre").Value & "|" & objrst.Fields("mlote").Value &"'>" & objrst.Fields("posicion").Value & " - " & objrst.Fields("Articulo").Value & " - " & objrst.Fields("Descripciona").Value & " - " & objrst.Fields("Cant").Value &"</option>")
    objrst.MoveNext
    Loop
    %>
    </select></td>
    <td><input id="unidadc" name="unidadc" readonly="readonly"/></td>
    <td><input id="maquinac" name="maquinac" readonly="readonly"/></td>
    <td class="style4"><input id="turnoc" name="turnoc" /></td>
    <tr bgcolor="#FF0000">
        <td class="style1"><b>Reason</b></td>
        <td class="style9"><b>Quantity</b></td>
        <td class="style7"><b>Reject Type</b></td>
        <td class="style1"><b>Exact Reason</b></td>
    </tr>
    </td>
    <!-- #include file="include/dbxconofft.inc" -->
    <td class="style2">
    <select name="reason" onblur='devolver_datosr()' size="1" style="width: 250px">
    <%
     Dim strSQLh, objrsh, Odbconh
     'Abre la conexión con la base de datos a través de una conexión ODBC
    %>
    <!-- #include file="include/dbxcononusah.inc" -->
    <%
    strSQLh = "select reason, descr from ( select 'Select Reason ...' reason, ' ' descr from dual union select t$cdis reason, t$dsca descr from baan.ttcmcs005" & session("env") & _
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
        <td class="style10"><input id="cant" name="cant" align="left" 
                onchange="validar_num(this, '<%=strcantp%>')"/></td>
    <td class="style8">
        <select name="reject" style="width: 92px"  onblur='devolver_datosrej()'> 
              <option value="" selected >Select Reject Type ...</option>
              <option value="1">Supplied</option>
              <option value="2">Internal</option>
              <option value="3">Return</option>
        </select>
    </td>
    <td class="style2" rowspan=3>
        <textarea id="obser" name="obser" 
            style="overflow: auto; width: 168px; height: 82px;" cols="20" rows="1"></textarea></td>

    <tr bgcolor="#FF0000">
        <td class="style1"><b>Lot</b></td>
        <td class="style9"><b>Supplier</b></td>
        <td class="style7"><b>Disposition</b></td>
    </tr>
    <td class="style2"><input id="lot" name="lot" align="left"/></td>
    <td class="style10"><input id="supplier" name="supplier" align="left"/>
    <input id="nombres" name="nombres" align="left" /></td>
    <td class="style8">
    <select name="disposition" id="disposition"  onchange='devolver_datosrc()' size="1">
        <option value="" selected >Select Disposition Reason ...</option>
        <option value="2">Recycle</option>
        <option value="3">Regrind</option>
        <option value="4">Review</option>
        <option value="5">Good</option>
    </select>
</td>

    <input type='hidden' name='orden' value='<%=strorden%>'/>
    <input type='hidden' name='reasonc' value='NA'/>
    <input type='hidden' name='materialc' value='NA'/>
    <input type='hidden' id='posc' name='posc' value='NA'/>
    <input type='hidden' id="cantc" name="cantc" value='0'>
    <input type='hidden' name='rejectc' value='NA'/>
    <input type='hidden' name='dispositionc' value='0'/>
    <input type='hidden' name='itemc' value='NA'/>
    <input type='hidden' name='supplierc' value='NA'/>
    <input type='hidden' id="lotem"  name='lotem' value='0'/>
</tr>
<%      
else
    Session("strmsg") = "Order doesn't exist or the status is not active, release or completed."
	Response.Redirect("whInvMaterialRejected.aspx?flag=") 
End if
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
  
else
	Session("strmsg") = "Order doesn't exist or the status is not active, release or completed."
    Response.Redirect("whInvMaterialRejected.aspx?flag=")
End if
%>
</table>
</form>
</body>
</html>
