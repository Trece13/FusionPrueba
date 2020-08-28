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
 
    function validar_num(field) {
        var regex = /^-?\d*[0-9]*[,.]?[0-9]*$/;
        var re = new RegExp(regex);
        if (field.value.match(re)) {
                    }
        else {
            this.focus();
            field.value = 0;
            alert("Only numbers here");
        }
    };

    function devolver_datosc(field, i) {
        var indicec = field.selectedIndex;
        //var valorc = field.options.value;
        var valorc = document.getElementById("comment")[indicec].value;
        var textoEscogidoc = field.options.text;
        if (i == 1) {
            document.frminq.vart1.value = valorc
        };
        if (i == 2) {
            document.frminq.vart2.value = valorc
        };
        if (i == 3) {
            document.frminq.vart3.value = valorc
        };
        if (i == 4) {
            document.frminq.vart4.value = valorc
        };
        if (i == 5) {
            document.frminq.vart5.value = valorc
        };
        if (i == 6) {
            document.frminq.vart6.value = valorc
        };
        if (i == 7) {
            document.frminq.vart7.value = valorc
        };
        if (i == 8) {
            document.frminq.vart8.value = valorc
        };
        if (i == 9) {
            document.frminq.vart9.value = valorc
        };
        if (i == 10) {
            document.frminq.vart10.value = valorc
        };
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
    .style8
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 77px;
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
    .style19
    {
        height: 49px;
    }
    .style21
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 55px;
    }
    .style29
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 31px;
    }
    .style30
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 815px;
    }
    .style32
    {
        width: 77px;
    }
    .style33
    {
        width: 815px;
    }
    </style>
</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" style="width: 292px">
<TABLE style="width: 100%; height: 17px">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H3>Supplies Request</H3></td>
</tr>
</TABLE>
    <p style="width: 800px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
     </p>
<form name="frminq" method="post" action="whInvSaveSuppliesRequest.aspx" style="height: 125px; width: 1149px">
<table align="left" class="tableDefault4" border="1" cellspacing="0" 
    cellpadding="0">
<tr>
<td colspan="8" align="center" class="style19" style="height: 50px">
<input type="submit" name="btnLogin" value="Save Order  "/>
</td>
</tr>
<%
Dim strmachine, strrowcolor, strSQL, objrs, Odbcon, strmsg, strtarea, strdesca, strtype, i, strseq

strmachine = mid(ucase(trim(Request.Form("machine"))),1,9)
session("machine") = strmachine
strtype = Request.Form("valortype")

'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%

'Validar Maquina
strSQL = "select rtrim(rou002.t$mcno) machine from baan.ttirou002" & session("env") & _
" rou002 where rtrim(rou002.t$mcno) ='" + Session("machine") + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
	strmachine = objrs.Fields("machine").Value
%>
<tr bgcolor="#FF0000">
    <td class="style29"><b>User:</b></td>
    <td class="style30"><b><%=Session("user")%></b></td>
</tr>
<tr bgcolor="#FF0000">
    <td class="style29"><b>Name:</b></td>
    <td class="style30"><b><%=Session("username")%></b></td>
</tr>
<tr bgcolor="#FF0000"> 
	<td class="style29"><b>Machine:</b></td>
	<td class="style1" colspan="2"><%=strmachine%></td>
</tr>
<tr  bgcolor="#FF0000">
    <td class="style29"><b>Sequence</b></td>
    <td class="style30"><b>Item</b></td>
    <td class="style8"><b>Quantity</b></td>
</tr>
<%
    strrowcolor = "#ffffff"
    ' Llenar matriz de 10 posiciones
    i = 1
    Do While i <=5      
        If strrowcolor = "#87CEEB" Then 
            strrowcolor = "#D3D3D3" 
        Else 
            strrowcolor = "#87CEEB" 
        End if   
                        
%>

    <tr bgcolor="<%=strrowcolor%>">
    <td class="style3"><%=i%></td>
    <td class="style3">
    <select name="comment" id="comment" onchange='devolver_datosc(this, <%=i%>)' size="1" style="width: 800px">
    <%
     Dim strSQLc, objrsc, Odbconc
     'Abre la conexión con la base de datos a través de una conexión ODBC --1
    %>
        <!-- #include file="include/dbxcononusac.inc" -->
    <%
    strSQLc = "select item, descr, wareh, unit from (select 'Select Item ...' item, ' ' descr, ' ' wareh, ' ' unit from dual union " & _ 
        "select  t$item item, t$dsca descr, t$cwar wareh, t$cuni unit from baan.ttcibd001" & session("env") & _
        " where t$ccde = '999' and t$csig = ' ') order by descr"
    objrsc=Server.CreateObject("ADODB.recordset")
    objrsc.Open (strSQLc, Odbconc)
    Do While Not objrsc.EOF
        Response.Write("<option value='" & objrsc.Fields("item").Value & "'>" &  objrsc.Fields("item").Value & "-" & objrsc.Fields("descr").Value & "-" & objrsc.Fields("wareh").Value & "-" & objrsc.Fields("unit").Value & "</option>")
    objrsc.MoveNext
    Loop
    %>
    </select>
  
    </td>
    <td><input type="text" id="Horas<%=i%>" name="Horas<%=i%>" align="right" onchange="validar_num(this)" value="0"/></td>
    <td><input type="text" name='machine<%=i%>' value='<%=strmachine%>'/></td>
    <td><input type="text" name='vart<%=i%>' value='0'/></td>
      <!-- #include file="include/dbxconoffc.inc" -->
<%      
    i = i + 1
    Loop   

else
    Session("strmsg") = "Machine doesn't exist."
	Response.Redirect("whInvSuppliesRequest.aspx?flag=") 
End if
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
</table>
</form>
</body>
</html>
