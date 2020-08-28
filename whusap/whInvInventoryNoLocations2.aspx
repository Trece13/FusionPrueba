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
Dim strubicacion, strlote, stritem, strdescr, strunid, strqty, strrowcolor, strSQL, objrs, Odbcon
Dim strclot, strmsg, strflag, stralm, stralmacen, strqtyp, strqtya, strconteo

strflag = Request.QueryString("flag")
If strflag = "Y" then
    strmsg = Session("strmsg")
Else
    stralm = ucase(trim(Request.Form("txtalm")))
    strlote = ucase(trim(Request.Form("txtlote")))
    stritem = Request.Form("txtitem")
    strmsg = Session("strmsg")
    Session("alm") = stralm
    Session("lote") = strlote
    Session("item") = stritem
  
'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
    'Valida Almacen maneje ubicaciones
    strSQL = "select t$cwar alm, t$sloc loc from baan.twhwmd200" & Session("env") & " where trim(t$cwar)='" + stralm + "'"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If Not objrs.EOF Then
        stralmacen = objrs.Fields("alm").Value
        strubicacion = objrs.Fields("loc").Value
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
            Response.Redirect("whInvInventoryNoLocations.aspx?flag=") 
        End If 
    End if

If (strubicacion = 2) then

    ' Validar que exista el almacen
    strSQL = "select t$cwar from baan.twhwmd200" & Session("env") & " where t$cwar='" + stralm + "'"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)

    ' Did we find anything?
    If Not objrs.EOF Then
        'Valida si el articulo maneja lote o no
        strSQL = "select ibd001.t$kltc clot, ibd001.t$cuni unit, ibd003.t$conv strqty from baan.ttcibd001" & Session("env") & _
        " ibd001 left join baan.ttcibd003" & Session("env") & " ibd003 on ibd003.t$item = ibd001.t$item " & _
        " and ibd003.t$basu = ibd001.t$cuni and ibd003.t$unit = 'PLT' " & _
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
            If (strlote <> "") Then
                strSQL = "select t$item item from baan.twhltc100" & Session("env") & " where t$clot='" + strlote + "'"
                objrs=Server.CreateObject("ADODB.recordset")
                objrs.Open (strSQL, Odbcon)
                If Not objrs.EOF Then
                    stritem = objrs.Fields("item").Value
                    Session("item") = "         "+stritem
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
                    Response.Redirect("whInvInventoryNoLocations.aspx?flag=")  
                End If
            Else
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
                    strmsg = " Item incorrect or doesn't use lot, please check"
                    Session("strmsg") = strmsg 
                    Response.Redirect("whInvInventoryNoLocations.aspx?flag=")  
                End If
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
                strmsg = " Item incorrect or doesn't use lot, please check"
                Session("strmsg") = strmsg 
                Response.Redirect("whInvInventoryNoLocations.aspx?flag=")  
            End If
        End If
    Else
        strmsg = "Warehouse doesn't exist, please check"
        Session("strmsg") = strmsg 
        Response.Redirect("whInvInventoryNoLocations.aspx?flag=") 
    End If

'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
else
    strmsg = "Warehouse have Locations, use inventory raw material or inventory finish product."
    Session("strmsg") = strmsg 
    Response.Redirect("whInvInventoryNoLocations.aspx?flag=") 
End if    
End if
%>
<meta name="viewport" content="width=300, user-scalable=no">
<body onload="setFocus()" bgcolor="#87CEEB">
<form name="frmord" method="post" action="whupdInvInventoryNoLocations.aspx">
<TABLE style="width: 35%">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td><H3>Inventory Not Locations</H3></td>
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
