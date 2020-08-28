<%@ Page aspcompat=true debug=true %>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must login first, before use this session."
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
    strlote = ""
    'Session("lote")
    'stritem = Ucase(Session("item"))
    strdescr = ""
    'Session("descr")
    stralmubic = ""
    'Session("almubic")
    'stralmacen = Session("almacen")
    'strubicacion = Session("ubicacion")
    'strqty =Session("cantidad")
    'strunid = Session("undidad")
    strmsg = Session("strmsg")
Else
    stralmubic = ucase(trim(Request.Form("txtalmubic")))
    strlote = mid(ucase(trim(Request.Form("txtlote"))),1,9)
    'stralmacen = left(ucase(Request.Form("txtalmubic")),6)
    'strubicacion = mid(ucase(Request.Form("txtalmubic")),7,10)
    'stritem = Request.Form("txtitem")
    strmsg = ""

    Session("almubic") = stralmubic
    Session("lote") = strlote
    'Session("almacen") = stralmacen
    'Session("ubicacion") = strubicacion
    Session("item") = stritem
  
'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
    'Buscar Almacen de la ubicacion
    strSQL = "select t$cwar alm from baan.twhwmd300" & Session("env") & " where trim(t$loca)='" + stralmubic + "'"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If Not objrs.EOF Then
        stralmacen = objrs.Fields("alm").Value
        strubicacion = stralmubic
        Session("stralmacen") = stralmacen
        Session("strubicacion") = strubicacion

        'Buscar Secuencia del Conte
        strSQL = "select t$coun cont from baan.twhcol002" & Session("env") & " where trim(t$cwar)='" + Session("stralmacen") + "'"
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
        If Not objrs.EOF Then
            strconteo = objrs.Fields("cont").Value
            Session("conteo") = strconteo
        Else
            strmsg = "Doesn't Exist Count Active for Warehouse. '" + Session("stralmacen") + "'"
            Session("strmsg") = strmsg 
            Response.Redirect("whInvInventoryfp.aspx?flag=")
        End If 
    End if

    ' Validar que exista el almacen y la ubicacion
    strSQL = "select t$dsca from baan.twhwmd300" & Session("env") & " where T$CWAR='" + stralmacen + "' and t$loca='" + strubicacion + "'"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)

    ' Did we find anything?
    If Not objrs.EOF Then
        'Conseguir el Artículo asociado al lote
        strSQL = "select t$item item from baan.twhltc100" & session("env") & " where t$clot='" + strlote + "'"
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
        If Not objrs.EOF Then
            stritem = objrs.Fields("item").Value

            'Valida si el articulo maneja lote o no
            strSQL = "select ibd001.t$kltc clot, ibd001.t$cuni unit, ibd003.t$conv strqty from baan.ttcibd001" & Session("env") & _
            " ibd001 left join baan.ttcibd003" & Session("env") & " ibd003 on ibd003.t$item = ibd001.t$item " & _
            " and ibd003.t$basu = ibd001.t$cuni and ibd003.t$unit = 'PLT' " & _
            " where ibd001.t$item like '" + stritem + "'" 
            objrs=Server.CreateObject("ADODB.recordset")
            objrs.Open (strSQL, Odbcon)
            response.write(strSQL)
            response.end()
            If Not objrs.EOF Then
                strclot = objrs.Fields("clot").Value
                strqty = objrs.Fields("strqty").Value
            End If
        
            If (strclot = 1) then   
                ' Validar el lote
                Replace(Request("strlote"),",","")
                If (strlote <> "") Then
                    strSQL = "select t$item item from baan.twhltc100" & Session("env") & " where t$clot='" + strlote + "'"
                    objrs=Server.CreateObject("ADODB.recordset")
                    objrs.Open (strSQL, Odbcon)
                    If Not objrs.EOF Then
                        stritem = objrs.Fields("item").Value
                        'Session("item") = "         "+stritem
                        Session("item") = stritem
                        strSQL = "select t$dsca Descripcion, t$cuni unid from baan.ttcibd001" & Session("env") & " where t$item='" + stritem + "'"      
                        objrs=Server.CreateObject("ADODB.recordset")
                        objrs.Open (strSQL, Odbcon)
                        If Not objrs.EOF Then
                            strdescr = objrs.Fields("descripcion").Value
                            strunid = objrs.Fields("unid").Value
                            Session("descripcion") = strdescr
                            Session("unidad") = strunid
                        End If
                    Else
                        strmsg = "Lot Incorrect, please check"
                        Session("strmsg") = strmsg 
                        Response.Redirect("whInvInventoryfp.aspx?flag=") 
                    End If
                Else
                    'stritem = "         "+Ucase(stritem)
                    strSQL = "select t$dsca Descripcion, t$cuni unid from baan.ttcibd001" & Session("env") & " where t$kltc <> 1 and t$item='" + stritem + "'"      
                    objrs=Server.CreateObject("ADODB.recordset")
                    objrs.Open (strSQL, Odbcon)
                    If Not objrs.EOF Then
                        strdescr = objrs.Fields("descripcion").Value
                        strunid = objrs.Fields("unid").Value
                        Session("descripcion") = strdescr
                        Session("unidad") = strunid
                    Else
                        strmsg = " Item incorrect or doesn't use lot, please check"
                        Session("strmsg") = strmsg 
                        Response.Redirect("whInvInventoryfp.aspx?flag=") 
                    End If
                End If
            Else
                'Articulo que no maneja lote
                'stritem = "         "+Ucase(stritem)
                strSQL = "select t$dsca Descripcion, t$cuni unid from baan.ttcibd001" & Session("env") & " where t$kltc <> 1 and t$item='" + stritem + "'"      
                objrs=Server.CreateObject("ADODB.recordset")
                objrs.Open (strSQL, Odbcon)
                If Not objrs.EOF Then
                    strdescr = objrs.Fields("descripcion").Value
                    strunid = objrs.Fields("unid").Value
                    Session("descripcion") = strdescr
                    Session("unidad") = strunid
                Else
                    strmsg = " Item incorrect or doesn't use lot, please check"
                    Session("strmsg") = strmsg 
                    Response.Redirect("whInvInventoryfp.aspx?flag=") 
                End If
            End If
        Else
            strmsg = "That Lot doesn't have Item Asociated."
            Session("strmsg") = strmsg 
            Response.Redirect("whInvInventoryfp.aspx?flag=")         
        End If
    Else
        strmsg = "Location incorrect, please check"
        Session("strmsg") = strmsg 
        Response.Redirect("whInvInventoryfp.aspx?flag=") 
    End If

'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
End if
%>
<meta name="viewport" content="width=300, user-scalable=no">
<body onload="setFocus()" bgcolor="#87CEEB">
<form name="frmord" method="post" action="whupdInvInventoryfp.aspx">
<TABLE style="width: 35%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td><H3>Inventory Finish Product</H3></td>
</TABLE>
    <p style="width: 381px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
    </p>
<table align="left" class="tableDefault4" width="30%" border="0" cellspacing="0" cellpadding="0">

<tr> 
     <td class="style2"><b>Warehouse:</b></td><td class="titulog7"><%=stralmacen%></td>
</tr> 
<tr> 
     <td class="style2"><b>Location:</b></td><td class="titulog7"><%=strubicacion%></td>
</tr> 
<tr> 
     <td class="style2" height="10"><b>Lot:</b></td>
     <td class="style2"><%=strlote%></td>
</tr> 
<tr> 
     <td class="style2" colspan="1"><b>Item:</b></td>
     <td class="style2" colspan="2"><%=stritem%></td>
</tr> 
<tr> 
     <td class="style2" colspan="2"><%=strdescr%></td>
</tr> 
<tr> 
     <td class="style2" colspan="1"><b>Qty Count:</b></td>
     <!--  Valida la cantidad de artículos por pallets
     <td class="style2"><input type="text" id="txtqty" name="txtqty" size="10" value=' '></td>
     -->
     <td class="style2"><input type="text" id="txtqty" name="txtqty" size="10" value=<%=strqty%>></td>
</tr> 
<tr> 
     <td class="style2" colspan="1"><b>Unit:</b></td>
     <td class="style2"><%=strunid%></td>
</tr> 
<tr> 
     <td class="errorMsg" colspan="2" rowspan="2"><%=strmsg%></td>
</tr> 
<tr></tr>
<tr> 
    <td align="center" colspan="2"><input type="submit" name="btnLogin" value="  Continue  "></td>
</tr> 
</table>
</form>
</body>
</html>
