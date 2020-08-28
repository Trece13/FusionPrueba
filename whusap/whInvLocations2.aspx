<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "Debe loguearse antes de ejecutar esta opcion."
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
            if (exis >= ingresada) {

            }
            else {
                var r = alert("Usted esta solicitando mas de la cantidad original.  Por favor verifique");
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
            alert("Solo se permiten numeros ");
        }
    };

    function validar_alm(field) {
        var error = "";
        var ubicacion = field.value;
        //var ubicacion1 = ubicacion.substring(6, 16);
        var ubicacion2 = ubicacion.toUpperCase();
        //var almacen = ubicacion.substring(0, 6);
        var almacen = document.getElementById("alm").value;
        var almacen1 = almacen.toUpperCase();
        var almacenv = document.getElementById("alm").value;
        if (almacenv == almacen1) {
            //ubicacion = ubicacion.substring(5, 4);
            document.frminq.Msg.value = "1";
            document.frminq.almt.value = almacen1;
            document.frminq.ubit.value = ubicacion2;
            document.frminq.action = "whInvLocations2.aspx";
            document.frminq.submit();
        }
        else {
            var r = alert("Almacen incorrecto.  Por favor verifique");
            field.value = 0;
            this.focus();
        }
    };
            
</script>
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
            alert("Ubicacion incorrecta, por favor verifique.");
        </script>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" style="width: 292px">
        <TABLE style="width: 130%">
        <tr>
        <td><IMG SRC = "images/logophoenix_s.jpg" ></td>
        <td><H3>Ubicacion Articulos</H3></td>
        </TABLE>
        <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_cerrarsesion.jpg"></a>
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
        <td colspan="4" align="center"><input type="submit" name="btnLogin" value="Salvar Ubicacion  "/></td>
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
            <td class="style1"><b>Usuario:</b></td>
            <td class="style1" colspan="3"><b><%=Session("user")%></b></td>
            </tr>
            <tr bgcolor="#FF0000">
            <td class="style1"><b>Name:</b></td>
            <td class="style1" colspan="3"><b><%=Session("username")%></b></td>
            </tr>
            <%
            strrowcolor = "#ffffff"
            strSQL = "select t$orno Orden, inb.t$item Articulo, art.t$dsca Descripcion, art.t$cuni Unidad, t$clot Lote, t$astk Cant_Pedida, inb.t$cwar Almacen, " & _
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
            <td class="style1"><b>Orden</b></td>
            <td class="style1" colspan="3"><%=objrs.Fields("Orden").Value%></td>
            </tr>
            <tr bgcolor="<%=strrowcolor%>">
            <td class="style2"><b>Articulo</b></td>
            <td class="style2" colspan="3"><%=objrs.Fields("Articulo").Value%> <%=objrs.Fields("Descripcion").Value%></td>
            </tr>

            <tr bgcolor="<%=strrowcolor%>">
            <td class="style4"><b>Cantidad</b></td>
            <td class="style3"><b>Unidad</b></td>
            <td class="style4"><b>Ubicacion</b></td>
            <td class="style4"><b>Lote</b></td>
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
        strSQL = "SELECT t$loca FROM baan.twhwmd300" & Session("env") & " where t$binb = 2 and t$loca = '" + Ubica1 + "'" + " and t$cwar = '" + Almacen + "'"
        objrs = Server.CreateObject("ADODB.Recordset")
        objrs.Open(strSQL, Odbcon)
        If Not objrs.EOF Then
            %>
            <script language="javascript">
                alert("Ubicacion bloqueada, por favor verifique");
            </script>
            <body bgcolor="#87CEEB" style="width: 292px">
            <TABLE style="width: 35%">
            <tr>
            <td><IMG SRC = "images/logophoenix_s.jpg" ></td>
            <td><H3>Ubicacion Articulos</H3></td>
            </TABLE>
            <p style="width: 381px; height: 28px">
            <a href="whLogoffi.aspx"><img src="Images/btn_cerrarsesion.jpg"></a>
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
            <input type="submit" name="btnLogin" value="Salvar Ubicacion  "/>
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
                <td class="style1"><b>Usuario:</b></td>
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
                    <td class="style1"><b>Orden</b></td>
                    <td class="style1" colspan="3"><%=objrs.Fields("Orden").Value%></td>
                    </tr>
                    <tr bgcolor="<%=strrowcolor%>">
                    <td class="style2"><b>Articulo</b></td>
                    <td class="style2" colspan="3"><%=objrs.Fields("Articulo").Value%> <%=objrs.Fields("Descripcion").Value%></td>
                    </tr>
                    <tr bgcolor="<%=strrowcolor%>">
                    <td class="style2"><b>Cantidad</b></td>
                    <td class="style3"><b>Unidad</b></td>
                    <td class="style4"><b>Ubicacion</b></td>
                    <td class="style4"><b>Lote</b></td>
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
	            Session("strmsg") = "Orden no existe o la cantidad es menos de lo recibido"
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
            <td><H3>Ubicacion Articulos</H3></td>
            </TABLE>
            <p style="width: 381px; height: 28px">
            <a href="whLogoffi.aspx"><img src="Images/btn_cerrarsesion.jpg"></a>
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
            <input type="submit" name="btnLogin" value="Salvar Ubicacion  "/>
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
                <td class="style1"><b>Usuario:</b></td>
                <td class="style1" colspan="3"><b><%=Session("user")%></b></td>
                </tr>
                <tr bgcolor="#FF0000">
                <td class="style1"><b>Name:</b></td>
                <td class="style1" colspan="3"><b><%=Session("username")%></b></td>
                </tr>
                <%
                strrowcolor = "#ffffff"
                strSQL = "select t$orno Orden, inb.t$item Articulo, art.t$dsca Descripcion, art.t$cuni Unidad, t$clot Lote, t$astk Cant_Pedida, inb.t$cwar Almacen, " & _
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
                    <td class="style1"><b>Orden</b></td>
                    <td class="style1" colspan="3"><%=objrs.Fields("Orden").Value%></td>
                    </tr>
                    <tr bgcolor="<%=strrowcolor%>">
                    <td class="style2"><b>Articulo</b></td>
                    <td class="style2" colspan="3"><%=objrs.Fields("Articulo").Value%> <%=objrs.Fields("Descripcion").Value%></td>
                    </tr>
                    <tr bgcolor="<%=strrowcolor%>">
                    <td class="style2"><b>Cantidad</b></td>
                    <td class="style3"><b>Unidad</b></td>
                    <td class="style4"><b>Ubicacion</b></td>
                    <td class="style4"><b>Lote</b></td>
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
	            Session("strmsg") = "Orden no existe o la cantidad es menos de lo recibido"
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
Else
    %>
    <body bgcolor="#87CEEB" style="width: 292px">
    <TABLE style="width: 35%">
    <tr>
    <td><IMG SRC = "images/logophoenix_s.jpg" ></td>
    <td><H3>Ubicacion Articulos</H3></td>
    </TABLE>
    <p style="width: 381px; height: 28px">
    <a href="whLogoffi.aspx"><img src="Images/btn_cerrarsesion.jpg"></a>
    <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p><form name="frminq" method="post" action="whInvsavelocation.aspx" style="height: 131px; width: 382px">
    <input type="hidden" name="Msg" value=""/>
    <input type="hidden" name="cont" value="0"/>
    <input type="hidden" name="almt" value=""/>
    <input type="hidden" name="ubit" value=""/>
    <input type="hidden" name="ocompra" value="<%=strocompra%>"/>
    <input type='hidden' name='valororigen' value="<%=strorigen%>"/>
    <table align="left" class="tableDefault4" border="1" cellspacing="0" cellpadding="0">
    <tr>
    <td colspan="4" align="center">
    <input type="submit" name="btnLogin" value="Salvar Ubicacion  "/>
    </td>
    </tr>
    <%
    'Conect to database and execute sp 4
    %>
    <!-- #include file="include/dbxcononusa.inc" -->
    <%
    'Validar Orden primera vez que ingresa
    strSQL = "select rtrim(t$orno) ocompra from baan.twhinh210" & Session("env") & " inb, baan.twhwmd300" & Session("env") & " ubi where inb.t$loca = ubi.t$loca and inb.t$cwar = ubi.t$cwar and " & _
    "t$rcno ='" + Session("ocompra") + "'" + " and t$oorg = '" + Session("origen") +"'" + " and t$rstk >= t$qadv and ubi.t$binb = 2 "
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If Not objrs.EOF Then
        strocompra = objrs.Fields("ocompra").Value
        %>
        <tr bgcolor="#FF0000">
        <td class="style1"><b>Usuario:</b></td>
        <td class="style1" colspan="3"><b><%=Session("user")%></b></td>
        </tr>
        <tr bgcolor="#FF0000">
        <td class="style1"><b>Name:</b></td>
        <td class="style1" colspan="3"><b><%=Session("username")%></b></td>
        </tr>
        <%
        strrowcolor = "#ffffff"
        strSQL = "select t$orno Orden, inb.t$item Articulo, art.t$dsca Descripcion, art.t$cuni Unidad, t$clot Lote, t$astk Cant_Pedida, inb.t$cwar Almacen, " & _
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
        <td class="style1"><b>Orden</b></td>
        <td class="style1" colspan="3"><%=objrs.Fields("Orden").Value%></td>
        </tr>
        <tr bgcolor="<%=strrowcolor%>">
        <td class="style2"><b>Articulo</b></td>
        <td class="style2" colspan="3"><%=objrs.Fields("Articulo").Value%> <%=objrs.Fields("Descripcion").Value%></td>
        </tr>
    
        <tr bgcolor="<%=strrowcolor%>">
        <td class="style2"><b>Cantidad</b></td>
        <td class="style3"><b>Unidad</b></td>
        <td class="style4"><b>Ubicacion</b></td>
        <td class="style4"><b>Lote</b></td>
        </tr>
        <tr bgcolor="<%=strrowcolor%>">
        <td><input type='text' id="cant" name="cant" size="10" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;" onchange='validar_cant(this, <%=strcant_ped%>, <%=strcant_sug%>, <%=i%>);'/></td>
        <td class="style3"><%=objrs.Fields("Unidad").Value%></td>
        <td><input type='text' name="ubica" size="25" style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal;"/></td>
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
	    Session("strmsg") = "Orden no existe o la cantidad es menos de lo recibido"
	    Response.Redirect("whInvLocations.aspx?flag=") 
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
