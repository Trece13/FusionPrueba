<%@ Page aspcompat=true debug=true %>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "Debe loguearse antes de ejecutar esta opcion."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>
<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<style type="text/css">
        .style2
        {
            color: Black;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-style: normal;
            text-align: Left;
            height: 10px;
        }
    </style>
<head>

<script type="text/javascript">

function setFocus()
{
document.getElementById("txtqty").focus();
}

</script>

</head>

<%
Dim strlote, stritem, strdescr, strunid, strqty, strrowcolor, strSQL, objrs, Odbcon
Dim strclot, strmsg, strflag, stralmubic, stralmacen, strubicacion,strqtyp, strqtya, strconteo

strflag = Request.QueryString("flag")
If strflag = "Y" then
    strlote = Session("lote")
    'stritem = Ucase(Session("item"))
    stritem = Request.Form("txtitem")
    strdescr = Session("descr")
    stralmubic = Session("almubic")
    strmsg = Session("strmsg")
Else
    stralmubic = ucase(trim(Request.Form("txtalmubic")))
    strlote = ucase(trim(Request.Form("txtlote")))
    stritem = Request.Form("txtitem")
    strmsg = ""

    Session("almubic") = stralmubic
    Session("lote") = strlote
    Session("item") = stritem
  
'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
'Almacen Definido por el Usuario.
strSQL = "select t$cwar bodega from baan.tticol127" & session("env") & " where upper(t$user)= '" + Session("user") + "'" 
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
        If Not objrs.EOF Then
            stralmacen = objrs.Fields("bodega").Value
        Else
            strmsg = "Usuario no tiene definido el almacen, por favor comuniquese con el administrador."
            Session("strmsg") = strmsg 
            Response.Redirect("whInvtransfers.aspx?flag=") 
        End if

    'Buscar Almacen de la ubicacion
    strSQL = "select t$cwar alm from baan.twhwmd300" & Session("env") & " where trim(t$loca)='" + stralmubic + "'"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If Not objrs.EOF Then
        'stralmacen = objrs.Fields("alm").Value
        strubicacion = stralmubic
        Session("stralmacen") = stralmacen
        Session("strubicacion") = strubicacion

        'Buscar Secuencia del Conteo
        strSQL = "select t$coun cont from baan.twhcol002" & Session("env") & " where trim(t$cwar)='" + Session("stralmacen") + "'"
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
        If Not objrs.EOF Then
            strconteo = objrs.Fields("cont").Value
            Session("conteo") = strconteo
        Else
            strmsg = "No existe conteo activo para la bodega. '" + Session("stralmacen") + "'"
            Session("strmsg") = strmsg 
            Response.Redirect("whInvInventory.aspx?flag=")  
        End If 
    End if

    ' Validar que exista el almacen y la ubicacion
    strSQL = "select t$dsca from baan.twhwmd300" & Session("env") & " where T$CWAR='" + stralmacen + "' and t$loca='" + strubicacion + "'"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)

    ' Did we find anything?
    If Not objrs.EOF Then
        'Valida si el articulo maneja lote o no
        strSQL = "select ibd001.t$kltc clot, ibd001.t$cuni unit, ibd003.t$conv strqty from baan.ttcibd001" & Session("env") & _
        " ibd001 left join baan.ttcibd003" & Session("env") & " ibd003 on ibd003.t$item = ibd001.t$item " & _
        " and ibd003.t$basu = ibd001.t$cuni and ibd003.t$unit = 'CJ ' " & _
        " where trim(ibd001.t$item) like '" + stritem + "'" 
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
        If Not objrs.EOF Then
            strclot = objrs.Fields("clot").Value
            strqty = objrs.Fields("strqty").Value
        End If
        
        If (strclot = 1) then   
            ' Validar el lote
            Replace(Request("strlote"),",","")

                strSQL = "select t$item item from baan.twhltc100" & Session("env") & " where t$clot='" + strlote + "'"
                objrs=Server.CreateObject("ADODB.recordset")
                objrs.Open (strSQL, Odbcon)
                If Not objrs.EOF Then
                    'stritem = objrs.Fields("item").Value
                    Session("item") = "         "+stritem
                    strSQL = "select t$dsca Descripcion, t$cuni unid from baan.ttcibd001" & Session("env") & " where trim(t$item)='" + stritem + "'"      
                    objrs=Server.CreateObject("ADODB.recordset")
                    objrs.Open (strSQL, Odbcon)
                    If Not objrs.EOF Then
                        strdescr = objrs.Fields("descripcion").Value
                        strunid = objrs.Fields("unid").Value
                        Session("descripcion") = strdescr
                        Session("unidad") = strunid
                    End If
                Else
                    strmsg = "Lote no existe en los registros de baan."
                    Session("strmsg") = strmsg 
                    Response.Redirect("whInvInventory.aspx?flag=") 
                End If
        Else
            'Articulo que no maneja lote
            stritem = "         "+Ucase(stritem)
            strSQL = "select t$dsca Descripcion, t$cuni unid from baan.ttcibd001" & Session("env") & " where t$kltc <> 1 and t$item='" + stritem + "'"      
            objrs=Server.CreateObject("ADODB.recordset")
            objrs.Open (strSQL, Odbcon)
            If Not objrs.EOF Then
                strdescr = objrs.Fields("descripcion").Value
                strunid = objrs.Fields("unid").Value
                Session("descripcion") = strdescr
                Session("unidad") = strunid
            Else
                strmsg = "Articulo incorrecto, por favor verifique."
                Session("strmsg") = strmsg 
                Response.Redirect("whInvInventory.aspx?flag=") 
            End If
        End If
    Else
        strmsg = "Ubicacion incorrecta, por favor verifique."
        Session("strmsg") = strmsg 
        Response.Redirect("whInvInventory.aspx?flag=") 
    End If

'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
End if
%>
<meta name="viewport" content="width=300, user-scalable=no">
<body onload="setFocus()" bgcolor="#87CEEB">
<form name="frmord" method="post" action="whupdInvInventory.aspx">
<TABLE style="width: 35%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td><H3>Inventario Materia Prima</H3></td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_cerrarsesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<table align="left" class="tableDefault4" width="30%" border="0" cellspacing="0" cellpadding="0">

<tr> 
     <td class="style2"><b>Bodega:</b></td><td class="titulog7"><%=stralmacen%></td>
</tr> 
<tr> 
     <td class="style2"><b>Ubicación:</b></td><td class="titulog7"><%=strubicacion%></td>
</tr> 
<tr> 
     <td class="style2" height="10"><b>Lote:</b></td>
     <td class="style2"><%=strlote%></td>
</tr> 
<tr> 
     <td class="style2" colspan="1"><b>Articulo:</b></td>
     <td class="style2" colspan="2"><%=stritem%></td>
</tr> 
<tr> 
     <td class="style2" colspan="2"><%=strdescr%></td>
</tr> 
<tr> 
     <td class="style2" colspan="1"><b>Cant. Contada:</b></td>
     <!--  Valida la cantidad de artículos por pallets
     <td class="style2"><input type="text" id="txtqty" name="txtqty" size="10" value=' '></td>
     -->
     <td class="style2"><input type="text" id="txtqty" name="txtqty" size="10" value=<%=strqty%>></td>
     <input type="hidden" id="mlote" name="mlote" size="10" value=<%=strclot%>>
</tr> 
<tr> 
     <td class="style2" colspan="1"><b>Unidad:</b></td>
     <td class="style2"><%=strunid%></td>
</tr> 
<tr> 
     <td class="errorMsg" colspan="2" rowspan="2"><%=strmsg%></td>
</tr> 
<tr></tr>
<tr> 
    <td align="center" colspan="2"><input type="submit" name="btnLogin" value="  Continuar  "></td>
</tr> 
</table>
</form>
</body>
</html>
