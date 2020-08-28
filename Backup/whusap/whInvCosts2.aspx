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
    function validar_cant(field, factor, cantm, cantr) {
        var dividendo = field.value;
        var residuo = 0;
        var cantmx = cantm;
        var canrg = cantr;
        var totalcant = parseFloat(dividendo) + parseFloat(cantr)
        var total = cantmx - totalcant
        var regex = /^-?\d*[0-9]*[,.]?[0-9]*$/;
        var re = new RegExp(regex);
        if (field.value.match(re)) {
            residuo = dividendo % factor;
            if (total < 0) {
                alert("Quantity exceed the quanity by shift");
                this.focus();
                field.value = 0;
            }
            if ((dividendo % factor) != 0) {
                alert("Is not an order quantity multiple");
            }
        }
        else {
            this.focus();
            field.value = 0;
            alert("Only numbers here");
        }
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
            width: 797px;
        }
    .style3
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 94px;
    }
    .style5
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 380px;
    }
    .style9
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 116px;
    }
    .style11
    {
        color: Black;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 206px;
    }
    .style12
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 206px;
    }
    </style>
</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" style="width: 292px">
<TABLE style="width: 100%; height: 17px">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H3>Material Request</H3></td>
</tr>
</TABLE>
    <p style="width: 800px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
     </p>
<form name="frminq" method="post" action="whInvSaveCost.aspx" 
    style="height: 131px; width: 800px">
<table align="left" class="tableDefault4" border="1" cellspacing="0" 
    cellpadding="0">
<tr>
<td colspan="9" align="center">
<input type="submit" name="btnLogin" value="Save Order  "/>
</td>
</tr>
<%
    Dim strorden, strrowcolor, strSQL, objrs, Odbcon, strmsg, strart, struni, strpos, strmlote, strcante, strcantr, strcantregt
    Dim strmachine, strshift, strissw, stralm, strfactor, strcantd, strcantmax, strcantreg, strdesc, i
strorden = mid(ucase(trim(Request.Form("orden"))),1,9)
session("orden") = strorden
'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%

'Validar Orden
strSQL = "select rtrim(cst001.t$pdno) orden from baan.tticst001" & session("env") & " cst001, baan.ttisfc001" & session("env") & " sfc001 " & _
"where cst001.t$pdno = sfc001.t$pdno and t$osta in('5','7','9') and cst001.t$pdno='" + Session("orden") + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
        strorden = objrs.Fields("orden").Value
        
        'Valido en que turno estoy
        strSQL = "select  col111.t$mcno machine, col111.t$shif shift from baan.tticol111" & Session("env") & " col111 " & _
        " inner join baan.ttisfc010" & Session("env") & " sfc010 on sfc010.t$mcno = col111.t$mcno " & _
        " where   to_char(col111.t$stat-5/24, 'YYYYMMDDHH24MISS') <= to_char(sysdate, 'YYYYMMDDHH24MISS') " & _
        " and     to_char(col111.t$fint-5/24, 'YYYYMMDDHH24MISS') >= to_char(sysdate, 'YYYYMMDDHH24MISS') " & _
        " and     sfc010.t$pdno = '" + strorden + "'"  
        objrs = Server.CreateObject("ADODB.recordset")
        objrs.Open(strSQL, Odbcon)
        If Not objrs.EOF Then
            strmachine = objrs.Fields("machine").Value
            strshift = trim(objrs.Fields("shift").Value)
        End If
        
%>
<tr bgcolor="#FF0000">
    <td class="style12"><b>User</b>:</td>
    <td class="style3"><b><%=Session("user")%></b></td>
</tr>
<tr bgcolor="#FF0000">
    <td class="style12"><b>Name</b>:</td>
    <td class="style3"><b><%=Session("username")%></b></td>
</tr>
<tr bgcolor="#FF0000"> 
	<td class="style12"><b>Order:</b></td>
	<td class="style1" colspan="2"><%=strorden%></td>
</tr>
<tr  bgcolor="#FF0000">
    <td class="style12"><b>Item</b></td>
    <td class="style5"><b>Description</b></td>
    <td class="style1"><b>Actual Quantity</b></td>
    <td class="style1"><b>To Issue by Warehousing</b></td>
    <td class="style9"><b>To Issue</b></td>
    <td class="style1"><b>Unit</b></td>
</tr>
</tr>
<%
    strrowcolor = "#ffffff"
    If (strshift = "") then
        Session("strmsg") = "Machine doesn't scheduled per this shift."
        Response.Redirect("whInvCosts.aspx?flag=")  
    Else
        'Carga los datos de la lista de materiales que en el campo notas camban tenga el valor 1 
        strSQL = "select rec.t$pdno Orden, rec.t$pono Posicion, rec.t$cwar Almacen, rec.t$clot Lote, rec.t$sitm Articulo, art.t$dsca Descripcion, " & _
        "(select case when cant_max is null then 0 else cant_max end from webuser.maxquantity_per_shift"&session("env")& " where orden = rec.t$pdno and articulo = rec.t$sitm and maquina = sfc010.t$mcno) cant_max, " & _
        "(select case when cant_reg is null then 0 else cant_reg end from webuser.maxquantity_per_shift"&session("env")& " where orden = rec.t$pdno and articulo = rec.t$sitm and maquina = sfc010.t$mcno) cant_reg, " & _
        "art.t$cuni Unidad, rec.t$ques Cant_Est, (select sum(cer.t$qucs) from baan.tticst001" & session("env") & " cer where cer.t$pdno='" + Session("orden") + "' and cer.t$sitm = rec.t$sitm) Act_Cant, " & _
        "rec.t$iswh iss_wareh, ibd200.t$oqmf factor, case when cst.t$qune is null then 0 else cst.t$qune end cantd " & _
        "from baan.tticst001" & session("env") & " rec " & _
        "inner join baan.ttcibd001" & session("env") & " art on rec.t$sitm = art.t$item " & _
        "inner join baan.ttcibd200" & session("env") & " ibd200 on art.t$item = ibd200.t$item " & _ 
        "inner join baan.ttisfc001" & session("env") & " sfc on rec.t$pdno = sfc.t$pdno " & _  
        "inner join baan.ttisfc010" & session("env") & " sfc010 on sfc010.t$pdno = sfc.t$pdno and sfc010.t$opno = 10 " & _
        "left join baan.tticol080" & session("env") & " cst on cst.t$orno = rec.t$pdno and cst.t$pono = rec.t$pono " & _
        "where sfc.t$osta in('5','7','9') and rec.t$nnts = 1 " & _
        " and rec.t$pdno='" + Session("orden") + "'" & _
        " and (select turno from webuser.maxquantity_per_shift"&session("env")& " where orden = rec.t$pdno and maquina = sfc010.t$mcno group by turno) ='" + strshift + "'"
        objrs = Server.CreateObject("ADODB.recordset")
        objrs.Open(strSQL, Odbcon)
        i = 0
        Do While Not objrs.EOF
            Session("strmsg") = " "
            strorden = objrs.Fields("Orden").Value
            strpos = objrs.Fields("Posicion").Value
            strart = objrs.Fields("Articulo").Value
            strdesc = objrs.Fields("Descripcion").Value
            stralm = objrs.Fields("Almacen").Value
            struni = objrs.Fields("Unidad").Value
            strmlote = objrs.Fields("Lote").Value
            strcante = objrs.Fields("Cant_Est").Value
            strcantr = objrs.Fields("Act_Cant").Value
            strissw = objrs.Fields("iss_wareh").Value
            strfactor = objrs.Fields("factor").Value
            strorden = Session("orden")
            if (isdbnull(objrs.Fields("cant_max").Value)) then              
                strcantmax = 999999
            else
                strcantmax = objrs.Fields("cant_max").Value 
            end if
            if (isdbnull(objrs.Fields("cant_reg").Value)) then 
                strcantreg = 0 
            else 
                strcantreg = objrs.Fields("cant_reg").Value 
            end if
            if (isdbnull(objrs.Fields("cantd").Value)) then 
                strcantd = 0 
            else 
                strcantd = objrs.Fields("cantd").Value 
            end if
            strcantregt = strcantreg + strcantd
        
            If strrowcolor = "#87CEEB" Then
                strrowcolor = "#D3D3D3"
            Else
                strrowcolor = "#87CEEB"
            End If
%>
<tr bgcolor="<%=strrowcolor%>">
    <td class="style11"><%=strart%> </td>
    <td class="style2"><%=strdesc%></td>
    <td class="style2" align="right"><%=strcantr%></td>
    <td class="style2" align="right"><%=strissw%></td>
    <td class="style2">
        <input name="cant" size="10" 
            style="font-size: 10px; font-family: Verdana, Arial, Helvetica, sans-serif; font-style: normal; margin-left: 0px;" 
            value='<%=strcantd%>' onblur="validar_cant(this, <%=strfactor%>, <%=strcantmax%>, <%=strcantreg%>);"/></td>
    <td class="style2"><%=struni%></td>
    <input type='hidden' name='orden' value='<%=strorden%>'/>
	<input type='hidden' name='art' value='<%=strart%>'/>
    <input type='hidden' name='uni' value='<%=struni%>'/>
    <input type='hidden' name='pos' value='<%=strpos%>'/>
    <input type='hidden' name='canto' value='<%=strcante%>'/>
    <input type='hidden' name='alm' value='<%=stralm%>'/>
</tr>
<%      
        i = i + 1  
        objrs.MoveNext              
        Loop        
        If (i = 0) then
            Session("strmsg") = "Order doesn't exist or the status is not active, release or completed. Or the materials doesn't have the correct settings."
            Response.Redirect("whInvCosts.aspx?flag=")   
        End if 
    End if
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
else
	Session("strmsg") = "Order doesn't exist or the status is not active, release or completed."
	Response.Redirect("whInvCosts.aspx?flag=") 
End if
%>
</table>
</form>
</body>
</html>
