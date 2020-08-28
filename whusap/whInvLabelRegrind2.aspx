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

    function devolver_datosr() {
        var indicer = document.frminq.regrind.selectedIndex
        var valorr = document.frminq.regrind.options[indicer].value
        var textoEscogidor = document.frminq.regrind.options[indicer].text
        document.frminq.regrindr.value = valorr
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
    .style10
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 308px;
    }
    .style11
    {
        width: 272px;
    }
    .style12
    {
        width: 308px;
    }
    </style>
</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" style="width: 609px">
<TABLE style="width: 381px; height: 28px"">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H3>Print Labels Regrind</H3></td>
</tr>
</TABLE>
    <p>
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
     </p>
<form name="frminq" method="post" action="whInvSaveLabelRegrind.aspx" style="width: 376px">
<table align="left" style="width: 381px; height: 28px" border="1" cellspacing="0" 
    cellpadding="0">
<tr>
<td colspan="2" align="center">
<input type="submit" name="btnLogin" value="Save Order  "/>
</td>
</tr>
<%
Dim strmachine, strorden, strrowcolor, strSQL, objrs, Odbcon, strmsg, i, strcantd, strfactor, strartr
strorden = ucase(Request.Form("orden"))

'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%

'Validar que Orden esta activa
'"where   to_char(col011.t$prdt-5/24, 'YYYYMMDDHH24MISS') <= to_char(sysdate, 'YYYYMMDDHH24MISS') " & _
'"and     to_char(col011.t$cmdt-5/24, 'YYYYMMDDHH24MISS') >= to_char(sysdate, 'YYYYMMDDHH24MISS') " & _
'"and     col011.t$stat = '2' " & _

strSQL = "select col011.t$mcno machine, col011.t$pdno orden from baan.tticol011" & session("env") & " col011 " & _
"where     col011.t$stat <> '1' " & _
"and     col011.t$pdno ='" + strorden + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
	strorden = objrs.Fields("orden").Value
    session("machine") = objrs.Fields("machine").Value

    'Validar Orden
    strSQL = "select rtrim(cst001.t$pdno) orden from baan.tticst001" & session("env") & " cst001, baan.ttisfc001" & session("env") & " sfc001 " & _
    "where cst001.t$pdno = sfc001.t$pdno and t$osta in('5','7','9') and cst001.t$pdno='" + strorden + "'"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If Not objrs.EOF Then
        strorden = objrs.Fields("orden").Value
        strrowcolor = "#ffffff"
        If strrowcolor = "#87CEEB" Then 
            strrowcolor = "#D3D3D3" 
        Else 
            strrowcolor = "#87CEEB" 
        End if    
%>
<tr bgcolor="<%=strrowcolor%>"></tr>
<tr bgcolor="#FF0000">
    <td class="style10"><b>User:</b></td>
    <td class="style3"><b><%=Session("user")%></b></td>
</tr>
<tr bgcolor="#FF0000">
    <td class="style10"><b>Name:</b></td>
    <td class="style3"><b><%=Session("username")%></b></td>
</tr>
<tr bgcolor="#FF0000"> 
	<td class="style10"><b>Order:</b></td>
	<td class="style1" colspan="2"><%=strorden%></td>
</tr>
<tr  bgcolor="#FF0000">
    <td class="style10"><b>Regrind</b></td>
    <td class="style9"><b>To Issue</b></td>
</tr>
<td class="style12">
    <select name="regrind" onblur='devolver_datosr()' size="1" style="width: 260px">
    <%
     Dim strSQLh, objrsh, Odbconh
     'Abre la conexión con la base de datos a través de una conexión ODBC
    %>
    <!-- #include file="include/dbxcononusah.inc" -->
    <%
    strSQLh = "select itemr, descar from (select '' itemr, 'A - Select the Regrind Item...' descar from dual union " & _
        " select  trim(cst001.t$sitm) itemr, trim(ibd001.t$dsca) descar from baan.tticst001" & session("env") & _
        " cst001 inner join baan.ttcibd001" & session("env") & " ibd001 on ibd001.t$item = cst001.t$sitm" & _
        " where ibd001.t$cpcl = 'RET'" & _
        " and cst001.t$pdno ='" + strorden + "')" & _
        " order by descar "
    objrsh=Server.CreateObject("ADODB.recordset")
    objrsh.Open (strSQLh, Odbconh)
    Do While Not objrsh.EOF 
        Response.Write("<option value='" & objrsh.Fields("itemr").Value & "'>" & objrsh.Fields("itemr").Value & " - " & objrsh.Fields("descar").Value & "</option>")
    objrsh.MoveNext
    Loop
    %>
    </select>
    <!-- #include file="include/dbxconoffh.inc" -->
</td>
<td class="style20"><input id="cant" name="cant" align="right" onchange="validar_num(this)"/></td>
<input type='hidden' name='regrindr' value='NA'/>
<input type='hidden' name='orden' value='<%=strorden%>'/>
<%      
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<% 
    else
        Session("strmsg") = "Order doesn't exist or the status is not active, release or completed."
	    Response.Redirect("whInvLabelRegrind.aspx?flag=") 
    End if
else
	'Session("strmsg") = "Machine doesn't scheduled per this shift."
    Session("strmsg") = "Order doesn't initiated for this machine."
	Response.Redirect("whInvLabelRegrind.aspx?flag=") 
End if
%>
</table>
</form>
</body>
</html>
