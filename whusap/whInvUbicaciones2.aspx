<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "Debe autenticarse primero para ejecutar esta opcion."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>
<html>
<head>

<link href="../basic.css" rel="stylesheet" type="text/css">
<script type="text/JavaScript">
    function validar_cant(field, exis, sug, contad) {
        var error = "";
        var ingresada = 0;
        var regex = /^\d*[0-9]*[,.]?[0-9]*$/;
        var re = new RegExp(regex);
        var secuencia = contad - 1;
        document.frminq.cont.value = secuencia;
        // Validar ingrese solo digitos enteros
        if (field.value.match(re)) {
            ingresada = field.value;
            if (isNaN(field.value)) {
                field.value = 0;
            }
            if ((exis - sug) >= ingresada) {

            }
            else {
                var r = alert("Esta solicitando más de la cantidad original. Por favor revise");
                field.value = 0;
                this.focus();
            }
            if (r == false) {
                field.value = "";
                //  this.focus();
            }
        } else {
            this.focus();
            field.value = 0;
            alert("Solo se permite ingresar numeros enteros");
        }
    };

    function validar_ubicacion(field) {
        var ubicacion = field.value;
        var almacen = ubicacion.substring(0, 6);
        var almacen1 = almacen.toUpperCase();
        var ubicacion1 = ubicacion.substring(6, 20);
        var ubicacion2 = ubicacion1.toUpperCase();
        var conObj = new ActiveXObject('ADODB.Connection');
        var connectionString = "Driver={Microsoft ODBC for Oracle};Server=colbaan;uid=webuser;pwd=s3st2m1s;"
        conObj.Open(connectionString);
        var rs = new ActiveXObject("ADODB.Recordset");
        rs.Open("SELECT t$loca FROM baan.twhwmd300510 where t$loca = '" + ubicacion2 + "' and t$cwar = '" + almacen1 + "'", conObj);
        if (!rs.eof) {

        } else {
            var r = alert("Ubicación o Almacén no validos, por favor verifique");
            field.value = 0;
            this.focus();
        }
        rs.close;
        conObj.close;
    };

    function validar_alm(field) {
        var error = "";
        var ubicacion = field.value;
        var ubicacion1 = ubicacion.substring(6, 16);
        var ubicacion2 = ubicacion1.toUpperCase();
        var almacen = ubicacion.substring(0, 6);
        var almacen1 = almacen.toUpperCase();
        var almacenv = document.getElementById("alm").value;
        if (almacenv == almacen1) {
            ubicacion = ubicacion.substring(5, 4);
            document.frminq.Msg.value = "1";
            document.frminq.almt.value = almacen1;
            document.frminq.ubit.value = ubicacion2;
            document.frminq.action = "whInvUbicaciones2.aspx";
            document.frminq.submit();
        }
        else {
            var r = alert("El Almacén no corresponde. Por favor revise");
            field.value = 0;
            this.focus();
        }
    };
            
</script>
<%
Dim Mensaje, strSQL, Ubica, Ubica1, Almacen, Cantidad, Ubicacion, objrs, Odbcon, strocompra, strorigen, i, j
Dim strrowcolor, strmsg, strorden, strart, struni, strcant_ped, strcant_sug, strubi, strbloq, arCant, arUbica
Dim strfuen, strconj, strpos, strsec, stralm, strdesc, vcant, vubica, Almacentemp, Ubicatemp
Dim Contador as Integer

strocompra = ucase(trim(Request.Form("ocompra")))
session("ocompra") = strocompra
strorigen = ucase(trim(Request.Form("valororigen")))
session("origen") = strorigen

Mensaje = Request("Msg")
Cantidad = Request("cant")
Ubicacion = Request("ubica")

If Mensaje <> "" Then
    Contador = Request("cont")
    Ubica = Replace(Request("Ubica"),",","")
    Almacentemp = Ucase(Mid(Ubica,1,6))
    Ubicatemp = Ucase(Mid(Ubica,7))
    Almacen = Request("almt")
    Ubica1 = Request("ubit")
    arCant = Split(Cantidad,",")
    arUbica = Split(Ubicacion,",")     

    'Conect to database and execute sp
    %>
    <!-- #include file="include/dbxconon.inc" -->
    <%
    'Valida que la Ubicación sea valida - 1
    strSQL = "SELECT t$loca FROM baan.twhwmd300510 where t$loca = '" + Ubica1 + "'" + "and t$cwar = '" + Almacen + "'"
    objrs = Server.CreateObject("ADODB.Recordset")
    objrs.Open(strSQL, Odbcon)
    If objrs.EOF Then
    %>      
        <script language="javascript">
            alert("Ubicacion no valida, por favor revise");
        </script>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" style="width: 292px">
        <TABLE style="width: 35%">
        <tr>
        <td><IMG SRC = "images/logophoenix_s.jpg" ></td>
        <td><H3>Ubicación de Producto</H3></td>
        </TABLE>
        <p style="width: 381px; height: 28px">
        <a href="whLogoff.aspx"><img src="Images/btn_cerrarsesion.jpg"></a>
        <a href="whMenu.aspx"><img src="images/btnMenuPpal.JPG"></a>
        </p>
        <form name="frminq" method="post" action="whInvGrabarUbicacion.aspx" 
        style="height: 131px; width: 382px">
        <input type="hidden" name="Msg" value=""/>
        <input type="hidden" name="cont" value="0"/>
        <input type="hidden" name="almt" value=""/>
        <input type="hidden" name="ubit" value=""/>
        <input type="hidden" name="ocompra" value="<%=strocompra%>"/>
        <input type='hidden' name='valororigen' value="<%=strorigen%>"/>
        <table align="left" class="tableDefault4" border="1" cellspacing="0" cellpadding="0">
        <tr>
        <td colspan="3" align="center"><input type="submit" name="btnLogin" value="Grabar Ubicación  "/></td>
        </tr>
        <%
        'Conect to database and execute sp
        %>
        <!-- #include file="include/dbxconon.inc" -->
        <%
        'Validar Orden
        strSQL = "select rtrim(t$orno) ocompra from baan.twhinh210510 inb, baan.twhwmd300510 ubi where inb.t$loca = ubi.t$loca and inb.t$cwar = ubi.t$cwar and " & _
        "t$rcno ='" + Session("ocompra") + "'" + " and t$oorg = '" + Session("origen") +"'" + " and t$rstk > t$qadv and ubi.t$binb = 2 "
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
        If Not objrs.EOF Then
	        strocompra = objrs.Fields("ocompra").Value
            %>
            <tr bgcolor="#FF0000">
            <td class="style1"><b>Usuario:</b></td>
            <td class="style1"><b><%=Session("user")%></b></td>
            </tr>
            <%
            strrowcolor = "#ffffff"
            strSQL = "select t$orno Orden, inb.t$item Articulo, art.t$dsca Descripcion, t$rcun Unidad, t$clot Lote, t$astk Cant_Pedida, inb.t$cwar Almacen, " & _
            "t$qadv Cant_Sugerida, inb.t$loca Ubicacion, ubi.t$binb Bloqueada, inb.t$oorg Fuente, inb.t$oset Conjunto, inb.t$pono Posicion, inb.t$seqn Secuencia " & _
            "from baan.twhinh210510 inb, baan.ttcibd001510 art, baan.twhwmd300510 ubi " & _ 
            "where inb.t$item = art.t$item and inb.t$loca = ubi.t$loca and inb.t$cwar = ubi.t$cwar and t$rcno='" + Session("ocompra") + "'" + " and t$oorg = '" + Session("origen") +"'" + " and t$rstk > t$qadv and ubi.t$binb = 2"
            objrs=Server.CreateObject("ADODB.recordset")
            objrs.Open (strSQL, Odbcon)
            i = 0
            Do While Not objrs.EOF
            i = i + 1
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
            If strrowcolor = "#87CEEB" Then 
                strrowcolor = "#D3D3D3" 
            Else 
                strrowcolor = "#87CEEB" 
            End if                   
            %>
            <tr bgcolor="#FF0000">
            <td class="style1"><b>Orden</b></td>
            <td class="style1" colspan="2"><%=objrs.Fields("Orden").Value%></td>
            </tr>
            <tr bgcolor="<%=strrowcolor%>">
            <td class="style2"><b>Artículo</b></td>
            <td class="style2" colspan="2"><%=objrs.Fields("Articulo").Value%> <%=objrs.Fields("Descripcion").Value%></td>
            </tr>

            <tr bgcolor="<%=strrowcolor%>">
            <td class="style2"><b>Cantidad</b></td>
            <td class="style3"><b>Un</b></td>
            <td class="style4"><b>Ubicación</b></td>
            </tr>
            <tr bgcolor="<%=strrowcolor%>">
            <td><input type='text' id="Text1" name="cant" size="10" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" onchange='validar_cant(this, <%=strcant_ped%>, <%=strcant_sug%>, <%=i%>);'
            value="<%=Trim(arCant(i-1))%>"/></td>
            <td class="style3"><%=objrs.Fields("Unidad").Value%></td>
            <td><input type='text' name="ubica" size="25" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" onblur='validar_alm(this);'
            value="<%=Trim(arUbica(i-1))%>"/></td>
            <td><input type='hidden' name='art' value='<%=strart%>'/></td>
            <td><input type='hidden' name='uni' value='<%=struni%>'/></td>
            <td><input type='hidden' name='fuen' value='<%=strfuen%>'/></td>
            <td><input type='hidden' name='conj' value='<%=strconj%>'/></td>
            <td><input type='hidden' name='pos' value='<%=strpos%>'/></td>
            <td><input type='hidden' name='sec' value='<%=strsec%>'/></td>
            <td><input type='hidden' name='ori' value='<%=strorigen%>'/></td>
            <td><input type='hidden' name='orden' value='<%=strorden%>'/></td>
            <td><input type='hidden' id='Hidden1' name='alm' value='<%=stralm%>'/></td>
            </tr>
            <%
			objrs.MoveNext
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
        strSQL = "SELECT t$loca FROM baan.twhwmd300510 where t$binb = 1 and t$loca = '" + Ubica1 + "'" + " and t$cwar = '" + Almacen + "'"
        objrs = Server.CreateObject("ADODB.Recordset")
        objrs.Open(strSQL, Odbcon)
        If Not objrs.EOF Then
            %>
            <script language="javascript">
                alert("Ubicacion bloqueada para inbound, por favor revise");
            </script>
            <body bgcolor="#87CEEB" style="width: 292px">
            <TABLE style="width: 35%">
            <tr>
            <td><IMG SRC = "images/logophoenix_s.jpg" ></td>
            <td><H3>Ubicación de Producto</H3></td>
            </TABLE>
            <p style="width: 381px; height: 28px">
            <a href="whLogoff.aspx"><img src="Images/btn_cerrarsesion.jpg"></a>
            <a href="whMenu.aspx"><img src="images/btnMenuPpal.JPG"></a>
            </p>
            <form name="frminq" method="post" action="whInvGrabarUbicacion.aspx" 
            style="height: 131px; width: 382px">
            <input type="hidden" name="Msg" value=""/>
            <input type="hidden" name="cont" value="0"/>
            <input type="hidden" name="almt" value=""/>
            <input type="hidden" name="ubit" value=""/>
            <input type="hidden" name="ocompra" value="<%=strocompra%>"/>
            <input type='hidden' name='valororigen' value="<%=strorigen%>"/>
            <table align="left" class="tableDefault4" border="1" cellspacing="0" cellpadding="0">
            <tr>
            <td colspan="3" align="center">
            <input type="submit" name="btnLogin" value="Grabar Ubicación  "/>
            </td>
            </tr>
            <%
            'Conect to database and execute sp
            %>
            <!-- #include file="include/dbxconon.inc" -->
            <%
            'Validar Orden
            strSQL = "select rtrim(t$orno) ocompra from baan.twhinh210510 inb, baan.twhwmd300510 ubi where inb.t$loca = ubi.t$loca and inb.t$cwar = ubi.t$cwar and " & _
            "t$rcno ='" + Session("ocompra") + "'" + " and t$oorg = '" + Session("origen") +"'" + " and t$rstk > t$qadv and ubi.t$binb = 2 "
            objrs=Server.CreateObject("ADODB.recordset")
            objrs.Open (strSQL, Odbcon)
            If Not objrs.EOF Then
	            strocompra = objrs.Fields("ocompra").Value
                %>
                <tr bgcolor="#FF0000">
                <td class="style1"><b>Usuario:</b></td>
                <td class="style1"><b><%=Session("user")%></b></td>
                </tr>
                <%
                strrowcolor = "#ffffff"
                strSQL = "select t$orno Orden, inb.t$item Articulo, art.t$dsca Descripcion, t$rcun Unidad, t$clot Lote, t$astk Cant_Pedida, inb.t$cwar Almacen, " & _
                "t$qadv Cant_Sugerida, inb.t$loca Ubicacion, ubi.t$binb Bloqueada, inb.t$oorg Fuente, inb.t$oset Conjunto, inb.t$pono Posicion, inb.t$seqn Secuencia " & _
                "from baan.twhinh210510 inb, baan.ttcibd001510 art, baan.twhwmd300510 ubi " & _ 
                "where inb.t$item = art.t$item and inb.t$loca = ubi.t$loca and inb.t$cwar = ubi.t$cwar and t$rcno='" + Session("ocompra") + "'" + " and t$oorg = '" + Session("origen") +"'" + " and t$rstk > t$qadv and ubi.t$binb = 2"
                objrs=Server.CreateObject("ADODB.recordset")
                objrs.Open (strSQL, Odbcon)
                i = 0
                Do While Not objrs.EOF
                    i = i + 1
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
            
                    If strrowcolor = "#87CEEB" Then 
                        strrowcolor = "#D3D3D3" 
                    Else 
                        strrowcolor = "#87CEEB" 
                    End if                   
                    %>
                    <tr bgcolor="#FF0000">
                    <td class="style1"><b>Orden</b></td>
                    <td class="style1" colspan="2"><%=objrs.Fields("Orden").Value%></td>
                    </tr>
                    <tr bgcolor="<%=strrowcolor%>">
                    <td class="style2"><b>Artículo</b></td>
                    <td class="style2" colspan="2"><%=objrs.Fields("Articulo").Value%> <%=objrs.Fields("Descripcion").Value%></td>
                    </tr>
                    <tr bgcolor="<%=strrowcolor%>">
                    <td class="style2"><b>Cantidad</b></td>
                    <td class="style3"><b>Un</b></td>
                    <td class="style4"><b>Ubicación</b></td>
                    </tr>
                    <tr bgcolor="<%=strrowcolor%>">
                    <td><input type='text' id="Text2" name="cant" size="10" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" onchange='validar_cant(this, <%=strcant_ped%>, <%=strcant_sug%>, <%=i%>);'
                    value="<%=Trim(arCant(i-1))%>"/></td>
                    <td class="style3"><%=objrs.Fields("Unidad").Value%></td>
                    <td><input type='text' name="ubica" size="25" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" onblur='validar_alm(this);'
                    value="<%=Trim(arUbica(i-1))%>"/></td>
                    <td><input type='hidden' name='art' value='<%=strart%>'/></td>
                    <td><input type='hidden' name='uni' value='<%=struni%>'/></td>
                    <td><input type='hidden' name='fuen' value='<%=strfuen%>'/></td>
                    <td><input type='hidden' name='conj' value='<%=strconj%>'/></td>
                    <td><input type='hidden' name='pos' value='<%=strpos%>'/></td>
                    <td><input type='hidden' name='sec' value='<%=strsec%>'/></td>
                    <td><input type='hidden' name='ori' value='<%=strorigen%>'/></td>
                    <td><input type='hidden' name='orden' value='<%=strorden%>'/></td>
                    <td><input type='hidden' id='Hidden2' name='alm' value='<%=stralm%>'/></td>
                    </tr>
                    <%
		        objrs.MoveNext
			    Loop
                'Desconectar a base de datos
                    %>
                    <!-- #include file="include/dbxconoff.inc" -->
                    <%
            Else
	            Session("strmsg") = "La Orden no Existe o la cantidad sugerida es menor a la cantidad recibida."
	            Response.Redirect("whInvUbicaciones.aspx?flag=") 
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
            <td><H3>Ubicación de Producto</H3></td>
            </TABLE>
            <p style="width: 381px; height: 28px">
            <a href="whLogoff.aspx"><img src="Images/btn_cerrarsesion.jpg"></a>
            <a href="whMenu.aspx"><img src="images/btnMenuPpal.JPG"></a>
            </p>
            <form name="frminq" method="post" action="whInvGrabarUbicacion.aspx" 
            style="height: 131px; width: 382px">
            <input type="hidden" name="Msg" value=""/>
            <input type="hidden" name="cont" value="0"/>
            <input type="hidden" name="almt" value=""/>
            <input type="hidden" name="ubit" value=""/>
            <input type="hidden" name="ocompra" value="<%=strocompra%>"/>
            <input type='hidden' name='valororigen' value="<%=strorigen%>"/>
            <table align="left" class="tableDefault4" border="1" cellspacing="0" cellpadding="0">
            <tr>
            <td colspan="3" align="center">
            <input type="submit" name="btnLogin" value="Grabar Ubicación  "/>
            </td>
            </tr>
            <%
            'Conect to database and execute sp
            %>
            <!-- #include file="include/dbxconon.inc" -->
            <%
            'Validar Orden
            strSQL = "select rtrim(t$orno) ocompra from baan.twhinh210510 inb, baan.twhwmd300510 ubi where inb.t$loca = ubi.t$loca and inb.t$cwar = ubi.t$cwar and " & _
            "t$rcno ='" + Session("ocompra") + "'" + " and t$oorg = '" + Session("origen") +"'" + " and t$rstk > t$qadv and ubi.t$binb = 2 "
            objrs=Server.CreateObject("ADODB.recordset")
            objrs.Open (strSQL, Odbcon)
            If Not objrs.EOF Then
	            strocompra = objrs.Fields("ocompra").Value
                %>
                <tr bgcolor="#FF0000">
                <td class="style1"><b>Usuario:</b></td>
                <td class="style1"><b><%=Session("user")%></b></td>
                </tr>
                <%
                strrowcolor = "#ffffff"
                strSQL = "select t$orno Orden, inb.t$item Articulo, art.t$dsca Descripcion, t$rcun Unidad, t$clot Lote, t$astk Cant_Pedida, inb.t$cwar Almacen, " & _
                "t$qadv Cant_Sugerida, inb.t$loca Ubicacion, ubi.t$binb Bloqueada, inb.t$oorg Fuente, inb.t$oset Conjunto, inb.t$pono Posicion, inb.t$seqn Secuencia " & _
                "from baan.twhinh210510 inb, baan.ttcibd001510 art, baan.twhwmd300510 ubi " & _ 
                "where inb.t$item = art.t$item and inb.t$loca = ubi.t$loca and inb.t$cwar = ubi.t$cwar and t$rcno='" + Session("ocompra") + "'" + " and t$oorg = '" + Session("origen") +"'" + " and t$rstk > t$qadv and ubi.t$binb = 2"
                objrs=Server.CreateObject("ADODB.recordset")
                objrs.Open (strSQL, Odbcon)
                i = 0
                Do While Not objrs.EOF
                    i = i + 1
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
            
                    If strrowcolor = "#87CEEB" Then 
                        strrowcolor = "#D3D3D3" 
                    Else 
                        strrowcolor = "#87CEEB" 
                    End if                   
                    %>
                    <tr bgcolor="#FF0000">
                    <td class="style1"><b>Orden</b></td>
                    <td class="style1" colspan="2"><%=objrs.Fields("Orden").Value%></td>
                    </tr>
                    <tr bgcolor="<%=strrowcolor%>">
                    <td class="style2"><b>Artículo</b></td>
                    <td class="style2" colspan="2"><%=objrs.Fields("Articulo").Value%> <%=objrs.Fields("Descripcion").Value%></td>
                    </tr>
                    <tr bgcolor="<%=strrowcolor%>">
                    <td class="style2"><b>Cantidad</b></td>
                    <td class="style3"><b>Un</b></td>
                    <td class="style4"><b>Ubicación</b></td>
                    </tr>
                    <tr bgcolor="<%=strrowcolor%>">
                    <td><input type='text' id="Text3" name="cant" size="10" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" onchange='validar_cant(this, <%=strcant_ped%>, <%=strcant_sug%>, <%=i%>);'
                    value="<%=Trim(arCant(i-1))%>"/></td>
                    <td class="style3"><%=objrs.Fields("Unidad").Value%></td>
                    <td><input type='text' name="ubica" size="25" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" onblur='validar_alm(this);'
                    value="<%=Trim(arUbica(i-1))%>"/></td>
                    <td><input type='hidden' name='art' value='<%=strart%>'/></td>
                    <td><input type='hidden' name='uni' value='<%=struni%>'/></td>
                    <td><input type='hidden' name='fuen' value='<%=strfuen%>'/></td>
                    <td><input type='hidden' name='conj' value='<%=strconj%>'/></td>
                    <td><input type='hidden' name='pos' value='<%=strpos%>'/></td>
                    <td><input type='hidden' name='sec' value='<%=strsec%>'/></td>
                    <td><input type='hidden' name='ori' value='<%=strorigen%>'/></td>
                    <td><input type='hidden' name='orden' value='<%=strorden%>'/></td>
                    <td><input type='hidden' id='Hidden3' name='alm' value='<%=stralm%>'/></td>
                    </tr>
                    <%
		        objrs.MoveNext
			    Loop
                'Desconectar a base de datos
                    %>
                    <!-- #include file="include/dbxconoff.inc" -->
                    <%
            Else
	            Session("strmsg") = "La Orden no Existe o la cantidad sugerida es menor a la cantidad recibida."
	            Response.Redirect("whInvUbicaciones.aspx?flag=") 
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
Else
    %>
    <body bgcolor="#87CEEB" style="width: 292px">
    <TABLE style="width: 35%">
    <tr>
    <td><IMG SRC = "images/logophoenix_s.jpg" ></td>
    <td><H3>Ubicación de Producto</H3></td>
    </TABLE>
    <p style="width: 381px; height: 28px">
    <a href="whLogoff.aspx"><img src="Images/btn_cerrarsesion.jpg"></a>
    <a href="whMenu.aspx"><img src="images/btnMenuPpal.JPG"></a>
    </p><form name="frminq" method="post" action="whInvGrabarUbicacion.aspx" style="height: 131px; width: 382px">
    <input type="hidden" name="Msg" value=""/>
    <input type="hidden" name="cont" value="0"/>
    <input type="hidden" name="almt" value=""/>
    <input type="hidden" name="ubit" value=""/>
    <input type="hidden" name="ocompra" value="<%=strocompra%>"/>
    <input type='hidden' name='valororigen' value="<%=strorigen%>"/>
    <table align="left" class="tableDefault4" border="1" cellspacing="0" cellpadding="0">
    <tr>
    <td colspan="3" align="center">
    <input type="submit" name="btnLogin" value="Grabar Ubicación  "/>
    </td>
    </tr>
    <%
    'Conect to database and execute sp
    %>
    <!-- #include file="include/dbxconon.inc" -->
    <%
    'Validar Orden primera vez que ingresa
    strSQL = "select rtrim(t$orno) ocompra from baan.twhinh210510 inb, baan.twhwmd300510 ubi where inb.t$loca = ubi.t$loca and inb.t$cwar = ubi.t$cwar and " & _
    "t$rcno ='" + Session("ocompra") + "'" + " and t$oorg = '" + Session("origen") +"'" + " and t$rstk > t$qadv and ubi.t$binb = 2 "
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If Not objrs.EOF Then
        strocompra = objrs.Fields("ocompra").Value
        %>
        <tr bgcolor="#FF0000">
        <td class="style1"><b>Usuario:</b></td>
        <td class="style1"><b><%=Session("user")%></b></td>
        </tr>
        <%
        strrowcolor = "#ffffff"
        strSQL = "select t$orno Orden, inb.t$item Articulo, art.t$dsca Descripcion, t$rcun Unidad, t$clot Lote, t$astk Cant_Pedida, inb.t$cwar Almacen, " & _
        "t$qadv Cant_Sugerida, inb.t$loca Ubicacion, ubi.t$binb Bloqueada, inb.t$oorg Fuente, inb.t$oset Conjunto, inb.t$pono Posicion, inb.t$seqn Secuencia " & _
        "from baan.twhinh210510 inb, baan.ttcibd001510 art, baan.twhwmd300510 ubi " & _ 
        "where inb.t$item = art.t$item and inb.t$loca = ubi.t$loca and inb.t$cwar = ubi.t$cwar and t$rcno='" + Session("ocompra") + "'" + " and t$oorg = '" + Session("origen") +"'" + " and t$rstk > t$qadv and ubi.t$binb = 2"
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
        i = 0
        Do While Not objrs.EOF
        i = i + 1
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
        
        If strrowcolor = "#87CEEB" Then 
            strrowcolor = "#D3D3D3" 
        Else 
            strrowcolor = "#87CEEB" 
        End if                   
        %>
        <tr bgcolor="#FF0000">
        <td class="style1"><b>Orden</b></td>
        <td class="style1" colspan="2"><%=objrs.Fields("Orden").Value%></td>
        </tr>
        <tr bgcolor="<%=strrowcolor%>">
        <td class="style2"><b>Artículo</b></td>
        <td class="style2" colspan="2"><%=objrs.Fields("Articulo").Value%> <%=objrs.Fields("Descripcion").Value%></td>
        </tr>
    
        <tr bgcolor="<%=strrowcolor%>">
        <td class="style2"><b>Cantidad</b></td>
        <td class="style3"><b>Un</b></td>
        <td class="style4"><b>Ubicación</b></td>
        </tr>
        <tr bgcolor="<%=strrowcolor%>">
        <td><input type='text' id="cant" name="cant" size="10" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" onchange='validar_cant(this, <%=strcant_ped%>, <%=strcant_sug%>, <%=i%>);'/></td>
        <td class="style3"><%=objrs.Fields("Unidad").Value%></td>
        <td><input type='text' name="ubica" size="25" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" onblur='validar_alm(this);'/></td>
        <td><input type='hidden' name='art' value='<%=strart%>'/></td>
        <td><input type='hidden' name='uni' value='<%=struni%>'/></td>
        <td><input type='hidden' name='fuen' value='<%=strfuen%>'/></td>
        <td><input type='hidden' name='conj' value='<%=strconj%>'/></td>
        <td><input type='hidden' name='pos' value='<%=strpos%>'/></td>
        <td><input type='hidden' name='sec' value='<%=strsec%>'/></td>
        <td><input type='hidden' name='ori' value='<%=strorigen%>'/></td>
        <td><input type='hidden' name='orden' value='<%=strorden%>'/></td>
        <td><input type='hidden' id='alm' name='alm' value='<%=stralm%>'/></td>
        </tr>
        <%
    	objrs.MoveNext
    	Loop
        'Desconectar a base de datos
        %>
        <!-- #include file="include/dbxconoff.inc" -->
        <%
    Else
	    Session("strmsg") = "La Orden no Existe o la cantidad sugerida es menor a la cantidad recibida."
	    Response.Redirect("whInvUbicaciones.aspx?flag=") 
    End if
    %>
    </table>
    </form>
    </body>
    <%
End if
%>

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
    }
    </style>

</head>
</html>
