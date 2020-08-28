<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must login first, before use this session."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>
<html>
<head>

<link href="../basic.css" rel="stylesheet" type="text/css">
<script type="text/JavaScript">
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
        var indicer = document.frminq.material.selectedIndex
        var valorr = document.frminq.material.options[indicer].value
        var textoEscogidor = document.frminq.material.options[indicer].text
        //document.frminq.posc.value = textoEscogidor.substring(0, 3);
        var value = document.getElementById("material").value;
        var split = value.split("|");
        var v1 = split[0];
        var v2 = split[1];
        var v3 = split[2];
        var v4 = split[3];
        var v5 = split[4];
        document.frminq.materialc.value = v1
        document.frminq.unidadc.value = v2
        document.frminq.maquinac.value = v3
        document.frminq.posc.value = v4
        document.frminq.cantc.value = v5
        document.getElementById("turnoc").focus();
        document.frminq.items.value = v1
        var v6 = v1.trim().substring(0, 1)
        if (v6 == "O") {
            document.frminq.supplier.disabled = false
        }
        else {
            document.frminq.supplier.disabled = true
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

    function devolver_datoss() {
        var original = document.getElementById("items").value;
        var indicer = document.frminq.supplier.selectedIndex
        var valors = document.frminq.supplier.options[indicer].value
        var textoEscogidor = document.frminq.supplier.options[indicer].text
        document.frminq.itemc.value = valors
        document.frminq.supplierc.value = valors
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
    }
    #unidadc
    {
        width: 46px;
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
    #maquinac
    {
        width: 60px;
        margin-right: 0px;
    }
    #turnoc
    {
        width: 52px;
    }
    .style6
    {
        height: 38px;
        width: 112px;
    }
    #cant
    {
        width: 60px;
        text-align: left;
    }
    .style9
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 25px;
    }
    .style10
    {
        height: 38px;
        width: 25px;
    }
    #supplier
    {
        width: 80px;
    }
    #disposition
    {
        width: 91px;
    }
</style>
</head>
<%
Dim Mensaje, strSQL, Ubica, Ubica1, Almacen, Cantidad, Ubicacion, objrs, Odbcon, strocompra, strorigen, i, j
Dim strrowcolor, strmsg, strorden, strart, struni, strcant_ped, strcant_sug, strubi, strbloq, arCant, arUbica, arLote
Dim strfuen, strconj, strpos, strsec, stralm, strdesc, vcant, vubica, Almacentemp, Ubicatemp, strlot
Dim Contador as Integer

strocompra = ucase(trim(Request.Form("ocompra")))
session("ocompra") = Mid(strocompra,1,9)
strorigen = ucase(trim(Request.Form("valororigen")))
session("origen") = strorigen

Mensaje = Request("Msg")
Cantidad = Request("cant")
Ubicacion = Request("ubica")
    %>
    <!-- #include file="include/dbxcononusa.inc" -->
    <%
    'Buscar Almacen de la ubicacion
    strSQL = "select t$cwar almo from baan.twhwmd300" & Session("env") & " where trim(t$loca)='" + strocompra + "'"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If Not objrs.EOF Then
        Almacentemp = objrs.Fields("almo").Value
        Ubica = strocompra
        Almacen = Almacentemp
        Ubica1 = strocompra
        arUbica = strocompra
    End if

    Contador = Request("cont")
    Ubica = Replace(Request("Ubica"),",","")
    Almacentemp = Ucase(Mid(Ubica,1,6))
    Ubicatemp = Ucase(Mid(Ubica,7))
    Almacen = Request("almt")
    Ubica1 = Request("ubit")
    arCant = Split(Cantidad,",")
    arUbica = Split(Ubicacion,",") 

If Mensaje <> "" Then
    'Conect to database and execute sp

    'Valida que la Ubicación sea valida - 1
    strSQL = "SELECT t$loca FROM baan.twhwmd300" & Session("env") & " where t$loca = '" + Ubica1 + "'" + "and t$cwar = '" + Almacen + "'"
    objrs = Server.CreateObject("ADODB.Recordset")
    objrs.Open(strSQL, Odbcon)
    If objrs.EOF Then
    %>      
        <script language="javascript">
            alert("Location incorrect, Please check.");
        </script>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" style="width: 292px">
        <TABLE style="width: 130%">
        <tr>
        <td><IMG SRC = "images/logophoenix_s.jpg" ></td>
        <td><H3>Item Location</H3></td>
        </TABLE>
        <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
        </p>
        <form name="frminq" method="post" action="whInvsavelocation.aspx" 
        style="height: 131px; width: 382px">
        <input type="hidden" name="Msg" value=""/>
        <input type="hidden" name="cont" value="0"/>
        <input type="hidden" name="almt" value=""/>
        <input type="hidden" name="ubit" value=""/>
        <input type="hidden" name="ocompra" value="<%=strocompra%>"/>
        <input type='hidden' name='valororigen' value="<%=strorigen%>"/>
        <table align="left" class="tableDefault4" border="1" cellspacing="0" cellpadding="0">
        <tr>
        <td colspan="4" align="center"><input type="submit" name="btnLogin" value="Save Location  "/></td>
        </tr>
        <%
        'Conect to database and execute sp
        %>
        <!-- #include file="include/dbxcononusa.inc" -->
        <%
        'Validar Orden
        strSQL = "select rtrim(t$orno) ocompra from baan.twhinh210" & Session("env") & " inb, baan.twhwmd300" & Session("env") & " ubi where inb.t$loca = ubi.t$loca and inb.t$cwar = ubi.t$cwar and " & _
        "t$rcno ='" + Session("ocompra") + "'" + " and t$oorg = '" + Session("origen") +"'" + " and t$rstk >= t$qadv and ubi.t$binb = 2 "
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
        If Not objrs.EOF Then
	        strocompra = objrs.Fields("ocompra").Value
            %>
            <tr bgcolor="#FF0000">
            <td class="style1"><b>User:</b></td>
            <td class="style1" colspan="3"><b><%=Session("user")%></b></td>
            </tr>
            <tr bgcolor="#FF0000">
            <td class="style1"><b>Name:</b></td>
            <td class="style1" colspan="3"><b><%=Session("username")%></b></td>
            </tr>
            <%
            strrowcolor = "#ffffff"
            strSQL = "select t$orno Orden, inb.t$item Articulo, art.t$dsca Descripcion, t$rcun Unidad, t$clot Lote, t$astk Cant_Pedida, inb.t$cwar Almacen, " & _
            "t$qadv Cant_Sugerida, inb.t$loca Ubicacion, ubi.t$binb Bloqueada, inb.t$oorg Fuente, inb.t$oset Conjunto, inb.t$pono Posicion, inb.t$seqn Secuencia " & _
            "from baan.twhinh210" & Session("env") & " inb, baan.ttcibd001" & Session("env") & " art, baan.twhwmd300" & Session("env") & " ubi " & _ 
            "where inb.t$item = art.t$item and inb.t$loca = ubi.t$loca and inb.t$cwar = ubi.t$cwar and t$rcno='" + Session("ocompra") + "'" + " and t$oorg = '" + Session("origen") +"'" + " and t$rstk >= t$qadv and ubi.t$binb = 2"
            objrs=Server.CreateObject("ADODB.recordset")
            objrs.Open (strSQL, Odbcon)
            i = 0
            Do While Not objrs.EOF
            strorden = objrs.Fields("Orden").Value
            strart = objrs.Fields("Articulo").Value
            struni = objrs.Fields("Unidad").Value
            strcant_ped = objrs.Fields("Cant_Pedida").Value
            strcant_sug = objrs.Fields("Cant_Sugerida").Value
            strubi = objrs.Fields("Ubicacion").Value
            stralm = objrs.Fields("Almacen").Value
            strbloq = objrs.Fields("Bloqueada").Value
            strfuen = objrs.Fields("Fuente").Value
            strconj = objrs.Fields("Conjunto").Value
            strpos = objrs.Fields("Posicion").Value
            strsec = objrs.Fields("Secuencia").Value
            strlot = objrs.Fields("Lote").Value

            If strrowcolor = "#87CEEB" Then 
                strrowcolor = "#D3D3D3" 
            Else 
                strrowcolor = "#87CEEB" 
            End if                   
            %>
            <tr bgcolor="#FF0000">
            <td class="style1"><b>Order</b></td>
            <td class="style1" colspan="3"><%=objrs.Fields("Orden").Value%></td>
            </tr>
            <tr bgcolor="<%=strrowcolor%>">
            <td class="style2"><b>Item</b></td>
            <td class="style2" colspan="3"><%=objrs.Fields("Articulo").Value%> <%=objrs.Fields("Descripcion").Value%></td>
            </tr>

            <tr bgcolor="<%=strrowcolor%>">
            <td class="style4"><b>Quantity</b></td>
            <td class="style3"><b>Unit</b></td>
            <td class="style4"><b>Location</b></td>
            <td class="style4"><b>Lot</b></td>
            </tr>
            <tr bgcolor="<%=strrowcolor%>">
            <td><input type='text' id="Text1" name="cant" size="10" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" onchange='validar_cant(this, <%=strcant_ped%>, <%=strcant_sug%>, <%=i%>);'
            value="<%=Trim(arCant(i-1))%>"/></td>
            <td class="style3"><%=objrs.Fields("Unidad").Value%></td>
            <td><input type='text' name="ubica" size="25" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" 
            value="<%=Trim(arUbica(i-1))%>"/></td>
            <td class="style3"><%=objrs.Fields("Lote").Value%></td>
            <td><input type='hidden' name='art' value='<%=strart%>'/></td>
            <td><input type='hidden' name='uni' value='<%=struni%>'/></td>
            <td><input type='hidden' name='fuen' value='<%=strfuen%>'/></td>
            <td><input type='hidden' name='conj' value='<%=strconj%>'/></td>
            <td><input type='hidden' name='pos' value='<%=strpos%>'/></td>
            <td><input type='hidden' name='sec' value='<%=strsec%>'/></td>
            <td><input type='hidden' name='ori' value='<%=strorigen%>'/></td>
            <td><input type='hidden' name='orden' value='<%=strorden%>'/></td>
            <td><input type='hidden' name='alm' value='<%=stralm%>'/></td>
            <td><input type='hidden' name='lot' value='<%=strlot%>'/></td>
            </tr>
            <%
			objrs.MoveNext
            i = i + 1
			Loop
            'Desconectar a base de datos
            %>
            <!-- #include file="include/dbxconoff.inc" -->
            <%
        End if
         %>
        </table>
        </form>
        </body>
    <%
    Else    
        'Valida que la Ubicación no esté bloqueada para inbound
        strSQL = "SELECT t$loca FROM baan.twhwmd300" & Session("env") & " where t$binb = 1 and t$loca = '" + Ubica1 + "'" + " and t$cwar = '" + Almacen + "'"
        objrs = Server.CreateObject("ADODB.Recordset")
        objrs.Open(strSQL, Odbcon)
        If Not objrs.EOF Then
            %>
            <script language="javascript">
                alert("Location is blocked, please check");
            </script>
            <body bgcolor="#87CEEB" style="width: 292px">
            <TABLE style="width: 35%">
            <tr>
            <td><IMG SRC = "images/logophoenix_s.jpg" ></td>
            <td><H3>Item Location1</H3></td>
            </TABLE>
            <p style="width: 381px; height: 28px">
            <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
            <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
            </p>
            <form name="frminq" method="post" action="whInvsavelocation.aspx" 
            style="height: 131px; width: 382px">
            <input type="hidden" name="Msg" value=""/>
            <input type="hidden" name="cont" value="0"/>
            <input type="hidden" name="almt" value=""/>
            <input type="hidden" name="ubit" value=""/>
            <input type="hidden" name="ocompra" value="<%=strocompra%>"/>
            <input type='hidden' name='valororigen' value="<%=strorigen%>"/>
            <table align="left" class="tableDefault4" border="1" cellspacing="0" cellpadding="0">
            <tr>
            <td colspan="4" align="center">
            <input type="submit" name="btnLogin" value="Save Location  "/>
            </td>
            </tr>
            <%
            'Conect to database and execute sp
            %>
            <!-- #include file="include/dbxcononusa.inc" -->
            <%
            'Validar Orden
            strSQL = "select rtrim(t$orno) ocompra from baan.twhinh210" & Session("env") & " inb, baan.twhwmd300" & Session("env") & " ubi where inb.t$loca = ubi.t$loca and inb.t$cwar = ubi.t$cwar and " & _
            "t$rcno ='" + Session("ocompra") + "'" + " and t$oorg = '" + Session("origen") +"'" + " and t$rstk >= t$qadv and ubi.t$binb = 2 "
            objrs=Server.CreateObject("ADODB.recordset")
            objrs.Open (strSQL, Odbcon)
            If Not objrs.EOF Then
	            strocompra = objrs.Fields("ocompra").Value
                %>
                <tr bgcolor="#FF0000">
                <td class="style1"><b>User:</b></td>
                <td class="style1" colspan="3"><b><%=Session("user")%></b></td>
                </tr>
                <tr bgcolor="#FF0000">
                <td class="style1"><b>Name:</b></td>
                <td class="style1" colspan="3"><b><%=Session("username")%></b></td>
                </tr>
                <%
                strrowcolor = "#ffffff"
                strSQL = "select t$orno Orden, inb.t$item Articulo, art.t$dsca Descripcion, t$scun Unidad, t$clot Lote, t$astk Cant_Pedida, inb.t$cwar Almacen, " & _
                "t$qadv Cant_Sugerida, inb.t$loca Ubicacion, ubi.t$binb Bloqueada, inb.t$oorg Fuente, inb.t$oset Conjunto, inb.t$pono Posicion, inb.t$seqn Secuencia " & _
                "from baan.twhinh210" & Session("env") & " inb, baan.ttcibd001" & Session("env") & " art, baan.twhwmd300" & Session("env") & " ubi " & _ 
                "where inb.t$item = art.t$item and inb.t$loca = ubi.t$loca and inb.t$cwar = ubi.t$cwar and t$rcno='" + Session("ocompra") + "'" + " and t$oorg = '" + Session("origen") +"'" + " and t$rstk >= t$qadv and ubi.t$binb = 2"
                objrs=Server.CreateObject("ADODB.recordset")
                objrs.Open (strSQL, Odbcon)
                i = 0
                Do While Not objrs.EOF
                    strorden = objrs.Fields("Orden").Value
                    strart = objrs.Fields("Articulo").Value
                    struni = objrs.Fields("Unidad").Value
                    strcant_ped = objrs.Fields("Cant_Pedida").Value
                    strcant_sug = objrs.Fields("Cant_Sugerida").Value
                    strubi = objrs.Fields("Ubicacion").Value
                    stralm = objrs.Fields("Almacen").Value
                    strbloq = objrs.Fields("Bloqueada").Value
                    strfuen = objrs.Fields("Fuente").Value
                    strconj = objrs.Fields("Conjunto").Value
                    strpos = objrs.Fields("Posicion").Value
                    strsec = objrs.Fields("Secuencia").Value
                    strlot = objrs.Fields("Lote").Value

                    If strrowcolor = "#87CEEB" Then 
                        strrowcolor = "#D3D3D3" 
                    Else 
                        strrowcolor = "#87CEEB" 
                    End if                   
                    %>
                    <tr bgcolor="#FF0000">
                    <td class="style1"><b>Order</b></td>
                    <td class="style1" colspan="3"><%=objrs.Fields("Orden").Value%></td>
                    </tr>
                    <tr bgcolor="<%=strrowcolor%>">
                    <td class="style2"><b>Item</b></td>
                    <td class="style2" colspan="3"><%=objrs.Fields("Articulo").Value%> <%=objrs.Fields("Descripcion").Value%></td>
                    </tr>
                    <tr bgcolor="<%=strrowcolor%>">
                    <td class="style2"><b>Quantity</b></td>
                    <td class="style3"><b>Unit</b></td>
                    <td class="style4"><b>Location</b></td>
                    <td class="style4"><b>Lot</b></td>
                    </tr>
                    <tr bgcolor="<%=strrowcolor%>">
                    <td><input type='text' id="Text2" name="cant" size="10" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" onchange='validar_cant(this, <%=strcant_ped%>, <%=strcant_sug%>, <%=i%>);'
                    value="<%=Trim(arCant(i-1))%>"/></td>
                    <td class="style3"><%=objrs.Fields("Unidad").Value%></td>
                    <td><input type='text' name="ubica" size="25" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;"
                    value="<%=Trim(arUbica(i-1))%>"/></td>
                    <td class="style3"><%=objrs.Fields("Lote").Value%></td>
                    <td><input type='hidden' name='art' value='<%=strart%>'/></td>
                    <td><input type='hidden' name='uni' value='<%=struni%>'/></td>
                    <td><input type='hidden' name='fuen' value='<%=strfuen%>'/></td>
                    <td><input type='hidden' name='conj' value='<%=strconj%>'/></td>
                    <td><input type='hidden' name='pos' value='<%=strpos%>'/></td>
                    <td><input type='hidden' name='sec' value='<%=strsec%>'/></td>
                    <td><input type='hidden' name='ori' value='<%=strorigen%>'/></td>
                    <td><input type='hidden' name='orden' value='<%=strorden%>'/></td>
                    <td><input type='hidden' name='alm' value='<%=stralm%>'/></td>
                    <td><input type='hidden' name='lot' value='<%=strlot%>'/></td>
                    </tr>
                    <%
		        objrs.MoveNext
                i = i + 1
			    Loop
                'Desconectar a base de datos
                    %>
                    <!-- #include file="include/dbxconoff.inc" -->
                    <%
            Else
	            Session("strmsg") = "Order doesn't exist or quantity less than received."
	            Response.Redirect("whInvLocations.aspx?flag=") 
            End if
                %>
                </table>
                </form>
                </body>
                <%
        Else
            %>

            <body bgcolor="#87CEEB" style="width: 292px">
            <TABLE style="width: 35%">
            <tr>
            <td><IMG SRC = "images/logophoenix_s.jpg" ></td>
            <td><H3>Item Location2</H3></td>
            </TABLE>
            <p style="width: 381px; height: 28px">
            <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
            <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
            </p>
            <form name="frminq" method="post" action="whInvsavelocation.aspx" 
            style="height: 131px; width: 382px">
            <input type="hidden" name="Msg" value=""/>
            <input type="hidden" name="cont" value="0"/>
            <input type="hidden" name="almt" value=""/>
            <input type="hidden" name="ubit" value=""/>
            <input type="hidden" name="ocompra" value="<%=strocompra%>"/>
            <input type='hidden' name='valororigen' value="<%=strorigen%>"/>
            <table align="left" class="tableDefault4" border="1" cellspacing="0" cellpadding="0">
            <tr>
            <td colspan="4" align="center">
            <input type="submit" name="btnLogin" value="Save Location  "/>
            </td>
            </tr>
            <%
            'Conect to database and execute sp 3
            %>
            <!-- #include file="include/dbxcononusa.inc" -->
            <%
            'Validar Orden
            strSQL = "select rtrim(t$orno) ocompra from baan.twhinh210" & Session("env") & " inb, baan.twhwmd300" & Session("env") & " ubi where inb.t$loca = ubi.t$loca and inb.t$cwar = ubi.t$cwar and " & _
            "t$rcno ='" + Session("ocompra") + "'" + " and t$oorg = '" + Session("origen") +"'" + " and t$rstk >= t$qadv and ubi.t$binb = 2 "
            objrs=Server.CreateObject("ADODB.recordset")
            objrs.Open (strSQL, Odbcon)
            If Not objrs.EOF Then
	            strocompra = objrs.Fields("ocompra").Value
                %>
                <tr bgcolor="#FF0000">
                <td class="style1"><b>User:</b></td>
                <td class="style1" colspan="3"><b><%=Session("user")%></b></td>
                </tr>
                <tr bgcolor="#FF0000">
                <td class="style1"><b>Name:</b></td>
                <td class="style1" colspan="3"><b><%=Session("username")%></b></td>
                </tr>
                <%
                strrowcolor = "#ffffff"
                strSQL = "select t$orno Orden, inb.t$item Articulo, art.t$dsca Descripcion, t$rcun Unidad, t$clot Lote, t$astk Cant_Pedida, inb.t$cwar Almacen, " & _
                "t$qadv Cant_Sugerida, inb.t$loca Ubicacion, ubi.t$binb Bloqueada, inb.t$oorg Fuente, inb.t$oset Conjunto, inb.t$pono Posicion, inb.t$seqn Secuencia " & _
                "from baan.twhinh210" & Session("env") & " inb, baan.ttcibd001" & Session("env") & " art, baan.twhwmd300" & Session("env") & " ubi " & _ 
                "where inb.t$item = art.t$item and inb.t$loca = ubi.t$loca and inb.t$cwar = ubi.t$cwar and t$rcno='" + Session("ocompra") + "'" + " and t$oorg = '" + Session("origen") +"'" + " and t$rstk >= t$qadv and ubi.t$binb = 2"
                objrs=Server.CreateObject("ADODB.recordset")
                objrs.Open (strSQL, Odbcon)
                i = 0
                Do While Not objrs.EOF
                    strorden = objrs.Fields("Orden").Value
                    strart = objrs.Fields("Articulo").Value
                    struni = objrs.Fields("Unidad").Value
                    strcant_ped = objrs.Fields("Cant_Pedida").Value
                    strcant_sug = objrs.Fields("Cant_Sugerida").Value
                    strubi = objrs.Fields("Ubicacion").Value
                    stralm = objrs.Fields("Almacen").Value
                    strbloq = objrs.Fields("Bloqueada").Value
                    strfuen = objrs.Fields("Fuente").Value
                    strconj = objrs.Fields("Conjunto").Value
                    strpos = objrs.Fields("Posicion").Value
                    strsec = objrs.Fields("Secuencia").Value
                    strlot = objrs.Fields("Lote").Value

                    If strrowcolor = "#87CEEB" Then 
                        strrowcolor = "#D3D3D3" 
                    Else 
                        strrowcolor = "#87CEEB" 
                    End if                   
                    %>
                    <tr bgcolor="#FF0000">
                    <td class="style1"><b>Order</b></td>
                    <td class="style1" colspan="3"><%=objrs.Fields("Orden").Value%></td>
                    </tr>
                    <tr bgcolor="<%=strrowcolor%>">
                    <td class="style2"><b>Item</b></td>
                    <td class="style2" colspan="3"><%=objrs.Fields("Articulo").Value%> <%=objrs.Fields("Descripcion").Value%></td>
                    </tr>
                    <tr bgcolor="<%=strrowcolor%>">
                    <td class="style2"><b>Quantity</b></td>
                    <td class="style3"><b>Unit</b></td>
                    <td class="style4"><b>Location</b></td>
                    <td class="style4"><b>Lot</b></td>
                    </tr>
                    <tr bgcolor="<%=strrowcolor%>">
                    <td><input type='text' id="Text3" name="cant" size="10" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" onchange='validar_cant(this, <%=strcant_ped%>, <%=strcant_sug%>, <%=i%>);'
                    value="<%=Trim(arCant(i-1))%>"/></td>
                    <td class="style3"><%=objrs.Fields("Unidad").Value%></td>
                    <td><input type='text' name="ubica" size="25" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;"
                    value="<%=Trim(arUbica(i-1))%>"/></td>
                    <td class="style3"><%=objrs.Fields("Lote").Value%></td>
                    <td><input type='hidden' name='art' value='<%=strart%>'/></td>
                    <td><input type='hidden' name='uni' value='<%=struni%>'/></td>
                    <td><input type='hidden' name='fuen' value='<%=strfuen%>'/></td>
                    <td><input type='hidden' name='conj' value='<%=strconj%>'/></td>
                    <td><input type='hidden' name='pos' value='<%=strpos%>'/></td>
                    <td><input type='hidden' name='sec' value='<%=strsec%>'/></td>
                    <td><input type='hidden' name='ori' value='<%=strorigen%>'/></td>
                    <td><input type='hidden' name='orden' value='<%=strorden%>'/></td>
                    <td><input type='hidden' name='alm' value='<%=stralm%>'/></td>
                    <td><input type='hidden' name='lot' value='<%=strlot%>'/></td>
                    </tr>
                    <%
		        objrs.MoveNext
                i = i + 1
			    Loop
                'Desconectar a base de datos
                    %>
                    <!-- #include file="include/dbxconoff.inc" -->
                    <%
            Else
	            Session("strmsg") = "Order doesn't exist or quantity less than received."
	            Response.Redirect("whInvLocations.aspx?flag=") 
            End if
                %>
                </table>
                </form>
                </body>
                <%
        End if   
    End if
    %>
    <%
End if
%>
</head>
</html>
