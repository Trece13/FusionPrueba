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
        var regex = "^\\d+$";
        var re = new RegExp(regex);
        if (field.value.match(re)) {
            if (field.value > 60) {
                alert("Minutes must be less than 60")
                field.focus();
                field.value = 0;
            }
                    }
        else {
            field.focus();
            field.value = 0;
            alert("Only numbers here");
        }
    };

    function validar_numh(field) {
        var regex = "^\\d+$";
        var re = new RegExp(regex);
        if (field.value.match(re)) {
            if (field.value > 12) {
                alert("Hours must be less than 12")
                field.focus();
                field.value = 0;
            }
        }
        else {
            field.focus();
            field.value = 0;
            alert("Only numbers here");
        }
    };

    function devolver_datosh() {
        var indiceh = document.frminq.type.selectedIndex
        var valorh = document.frminq.type.options[indiceh].value
        var textoEscogidoh = document.frminq.type.options[indiceh].text
        document.frminq.vhour.value = valorh
    };

    function devolver_datosc() {
        var indicec = document.frminq.comment.selectedIndex
        var valorc = document.frminq.comment.options[indicec].value
        var textoEscogidoc = document.frminq.comment.options[indicec].text
        document.frminq.vcomm.value = valorc
    };

    function devolver_datost() {
        var indicet = document.frminq.task.selectedIndex
        var valort = document.frminq.task.options[indicet].value
        var textoEscogidot = document.frminq.task.options[indicet].text
        document.frminq.tarea.value = valort
    };

    function check_value_labor(type) {
        var typed = type.value;
        var commd = comm.value;
        if (typed.value.length == 0) {
            alert("Labor Type doesn't selected");
        }
    };

    function devolver_datoshour() {
        var indiceh = document.frminq.Horas.selectedIndex
        var valorh = document.frminq.Horas.options[indiceh].value
        var textoEscogidoh = document.frminq.Horas.options[indiceh].text
        document.frminq.vhours.value = valorh
    };

    function devolver_datosminute() {
        var indiceh = document.frminq.Minutos.selectedIndex
        var valorh = document.frminq.Minutos.options[indiceh].value
        var textoEscogidoh = document.frminq.Minutos.options[indiceh].text
        document.frminq.vminutes.value = valorh
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
        width: 200px;
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
    .style15
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 170px;
    }
    .style19
    {
        height: 49px;
    }
    .style20
    {
        width: 55px;
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
    .style25
    {
        color: Black;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 40px;
    }
    .style26
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 40px;
    }
    .style27
    {
        color: White;
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 12px;
        font-style: normal;
        text-align: Left;
        height: 10px;
        width: 296px;
    }
    .style28
    {
        width: 296px;
    }
    #Minutos
    {
        width: 50px;
    }
    </style>
</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB" style="width: 292px">
<TABLE style="width: 100%; height: 17px">
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H3>Abscent-Indirect Hours Accounting</H3></td>
</tr>
</TABLE>
    <p style="width: 800px; height: 28px">
        <a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a>
        <a href="whMenui.aspx"><img src="images/btn_Mainmenu.JPG"></a>
     </p>
<form name="frminq" method="post" action="whInvSaveTimeai.aspx" 
    style="height: 131px; width: 1136px">
<table align="left" class="tableDefault4" border="1" cellspacing="0" 
    cellpadding="0">
<tr>
<td colspan="8" align="center" class="style19" style="width: 1100px; height: 50px">
<input type="submit" name="btnLogin" value="Save Order  "/>
</td>
</tr>
<%
Dim strmachine, strrowcolor, strSQL, objrs, Odbcon, strmsg, strtarea, strdesca, strtype, strhourlylt, i

strmachine = mid(ucase(trim(Request.Form("machine"))),1,9)
session("machine") = strmachine
strtype = Request.Form("valortype")
if strtype = "" then
    strtype = "3"
end if

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
    <td class="style15"><b>User:</b></td>
    <td class="style8"><b><%=Session("user")%></b></td>
</tr>
<tr bgcolor="#FF0000">
    <td class="style15"><b>Name:</b></td>
    <td class="style8"><b><%=Session("username")%></b></td>
</tr>
<tr bgcolor="#FF0000"> 
	<td class="style15"><b>Machine:</b></td>
	<td class="style1" colspan="2"><%=strmachine%></td>
</tr>
<tr  bgcolor="#FF0000">
    <td class="style26"><b>Task</b></td>
    <td class="style27"><b>Hourly Labor Type</b></td>
    <td class="style21"><b>Hours</b></td>
    <td class="style21"><b>Minutes</b></td>
    <td class="style8"><b>Comments</b></td>
</tr>
<%
    strrowcolor = "#ffffff"
    ' Valida las tareas asociadas a la maquina.
    strSQL = "select rou003.t$tano tarea, rou003.t$dsca Descripcion " & _
    "from baan.ttirou003" & session("env") & " rou003 " & _ 
    " where rou003.t$ckot = '" + strtype + "'"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If Not objrs.EOF Then
        Session("strmsg") = " "
        strtarea = objrs.Fields("Tarea").Value
        strdesca = objrs.Fields("Descripcion").Value

    If strrowcolor = "#87CEEB" Then 
        strrowcolor = "#D3D3D3" 
    Else 
        strrowcolor = "#87CEEB" 
    End if                   
%>
<tr bgcolor="<%=strrowcolor%>">
    <td class="style28">
    <select name="task" onblur='devolver_datost()' size="1" style="width: 300px">
    <%
     Dim strSQLt, objrst, Odbcont
     'Abre la conexión con la base de datos a través de una conexión ODBC
    %>
    <!-- #include file="include/dbxcononusat.inc" -->
    <%
    strSQLt = "select task, desct from (select 0 task, 'Select Task... ' desct, ' ' hourlylt from dual union select t$tano task, t$dsca desct, t$ckow hourlylt from baan.ttirou003" & session("env") & _
        " where t$ckot = '" + strtype + "'" & _
        " ) order by task"
    objrst=Server.CreateObject("ADODB.recordset")
    objrst.Open (strSQLt, Odbcont)
    Do While Not objrst.EOF
        Response.Write("<option value='" & objrst.Fields("task").Value & "'>" & objrst.Fields("task").Value & " - " & objrst.Fields("desct").Value & "</option>")
    objrst.MoveNext
    Loop
    %>
    </select>
    <!-- #include file="include/dbxconofft.inc" -->
    </td>
    <td class="style28">
    <select name="type" onblur='devolver_datosh()' size="1" style="width: 300px">
    <%
     Dim strSQLh, objrsh, Odbconh
     'Abre la conexión con la base de datos a través de una conexión ODBC
    %>
    <!-- #include file="include/dbxcononusah.inc" -->
    <%
    strSQLh = "select typeh, desct from (select ' ' typeh, 'Select Type... ' desct from dual union " & _
        " select t$ckow typeh, t$dsca desct from baan.ttihra130" & session("env") & _
        " where substr(trim(t$dsca),1,1) in ('A','P')) order by typeh" 
    objrsh=Server.CreateObject("ADODB.recordset")
    objrsh.Open (strSQLh, Odbconh)
    Do While Not objrsh.EOF
        Response.Write("<option value='" & objrsh.Fields("typeh").Value & "'>" & objrsh.Fields("typeh").Value & " - " & objrsh.Fields("desct").Value & "</option>")
    objrsh.MoveNext
    Loop
    %>
    </select>
    <!-- #include file="include/dbxconoffh.inc" -->
    </td>
    <td class="style28">
    <select id="Horas" name="Horas" onblur='devolver_datoshour()' size="1" style="width: 50px">
    <option value='0' selected>0</option>
    <option value='1'>1</option>
    <option value='2'>2</option>
    <option value='3'>3</option>
    <option value='4'>4</option>
    <option value='5'>5</option>
    <option value='6'>6</option>
    <option value='7'>7</option>
    <option value='8'>8</option>
    <option value='9'>9</option>
    <option value='10'>10</option>
    <option value='11'>11</option>
    <option value='12'>12</option>
    </select>
    </td>
    <td class="style28">
    <select id="Minutos" name="Minutos" onblur='devolver_datosminute()' size="1" style="width: 50px">
    <option value='0' selected>0</option>
    <option value='5'>5</option>
    <option value='10'>10</option>
    <option value='15'>15</option>
    <option value='20'>20</option>
    <option value='25'>25</option>
    <option value='30'>30</option>
    <option value='35'>35</option>
    <option value='40'>40</option>
    <option value='45'>45</option>
    <option value='50'>50</option>
    <option value='55'>55</option>
    </select>
    </td>
    <td>
    <select name="comment" onblur='devolver_datosc()' size="1" style="width: 280px">
    <%
     Dim strSQLc, objrsc, Odbconc
     'Abre la conexión con la base de datos a través de una conexión ODBC --1
    %>
    <!-- #include file="include/dbxcononusac.inc" -->
    <%
    strSQLc = "select code, comm from (select ' ' code, 'Select Comment... ' comm from dual union select t$code code, t$comm comm from baan.tticol077" & session("env") & _
        " ) order by code"
    objrsc=Server.CreateObject("ADODB.recordset")
    objrsc.Open (strSQLc, Odbconc)
    Do While Not objrsc.EOF
        Response.Write("<option value='" & objrsc.Fields("code").Value & "'>" & objrsc.Fields("comm").Value & "</option>")
    objrsc.MoveNext
    Loop
    %>
    </select>
    <!-- #include file="include/dbxconoffc.inc" -->
    </td>
    <input type='hidden' name='machine' value='<%=strmachine%>'/>
    <input type='hidden' name='tipo' value='<%=strtype%>'/>
    <input type='hidden' name='tarea' value=' '/>
    <input type='hidden' name='vhour' value='MM'/>
    <input type='hidden' name='vcomm' value=' '/>
    <input type='hidden' name='vhours' value='0'/>
    <input type='hidden' name='vminutes' value='0'/>
</tr>
<%      
    else
        Session("strmsg") = "Doesn't exist task of that type."
	    Response.Redirect("whInvTimeai.aspx?flag=") 
    End if
else
    Session("strmsg") = "Machine doesn't exist."
	Response.Redirect("whInvTimeai.aspx?flag=") 
End if
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
</table>
</form>

<form name="frminq1" style="height: 80px; width: 1000px">
<table align="left" class="tableDefault4" border="1" cellspacing="0" 
    cellpadding="0">
<%
Dim strorder, struser, strmachine1, strdescm, strlabor, strdescl, strhora, strcomm, strfecha, strtotalhoras
Dim strshift
strmachine1 = strmachine
session("machine") = strmachine
'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<% 
'Valido en que turno estoy para la maquina
strSQL = "select  col111.t$mcno machine, col111.t$shif shift, sfc010.t$pdno orden, rou002.t$dsca descr_machine from baan.tticol111" & Session("env") & " col111 " & _
    " inner join baan.ttisfc010" & Session("env") & " sfc010 on sfc010.t$mcno = col111.t$mcno " & _
    " inner join baan.ttirou002" & Session("env") & " rou002 on rou002.t$mcno = col111.t$mcno " & _
    " where   to_char(col111.t$stat-5/24, 'YYYYMMDDHH24MISS') <= to_char(sysdate, 'YYYYMMDDHH24MISS') " & _
    " and     to_char(col111.t$fint-5/24, 'YYYYMMDDHH24MISS') >= to_char(sysdate, 'YYYYMMDDHH24MISS') " & _
    " and     sfc010.t$mcno = '" + strmachine1 + "'"  
    objrs = Server.CreateObject("ADODB.recordset")
    objrs.Open(strSQL, Odbcon)
    IF Not objrs.EOF Then
        strmachine1 = objrs.Fields("machine").Value     
        strdescm = objrs.Fields("descr_machine").Value    
        strshift = objrs.Fields("shift").Value    
    End If
%>
<tr bgcolor="#FF0000">
<td class="style27"><b>Machine</b></td>
<td colspan="6" class="style1"><b><%=strmachine%><%=strdescm%></b></td>
</tr>
<tr  bgcolor="#FF0000">
    <td class="style1"><b>User</b></td>
    <td class="style1"><b>Order</b></td>
    <td class="style1"><b>Labor Type</b></td>
    <td class="style1"><b>Description</b></td>
    <td class="style1"><b>Time</b></td>
    <td class="style1"><b>Date</b></td>
    <td class="style1"><b>Comments</b></td>
</tr>
</tr>
<%
strrowcolor = "#ffffff"
'Carga los datos de las ordenes para ese turno
    strSQL = "select  machine, descr_machine, orden, tarifa, descripcion, fecha, usuario, coment, hora from ( " & _
        "select  col111.t$mcno machine, rou002.t$dsca descr_machine, col075.t$pdno orden, col075.t$cwtt tarifa, hra130.t$dsca descripcion, " & _
        "col075.t$hrea hora, col077.t$comm coment, col075.t$logn usuario, col075.t$date-5/24 fecha " & _
        "from    baan.tticol111" & session("env") & " col111 " & _
        "inner join baan.ttisfc010" & session("env") & " sfc010 on sfc010.t$mcno = col111.t$mcno " & _
        "inner join baan.ttirou002" & session("env") & " rou002 on rou002.t$mcno = col111.t$mcno " & _
        "inner join baan.tticol075" & session("env") & " col075 on sfc010.t$pdno = col075.t$pdno " & _
        "inner join baan.tticol077" & session("env") & " col077 on col077.t$code = col075.t$comm " & _
        "inner join baan.ttihra130" & session("env") & " hra130 on hra130.t$ckow = col075.t$cwtt " & _
        "where   to_char(col111.t$stat-5/24, 'YYYYMMDDHH24MISS') <= to_char(sysdate, 'YYYYMMDDHH24MISS') " & _
        "and     to_char(col111.t$fint-5/24, 'YYYYMMDDHH24MISS') >= to_char(sysdate, 'YYYYMMDDHH24MISS') " & _
        "and     to_char(col075.t$date-5/24, 'YYYYMMDDHH24MISS') between to_char(col111.t$stat-5/24, 'YYYYMMDDHH24MISS') " & _
        "and     to_char(col111.t$fint-5/24, 'YYYYMMDDHH24MISS') " & _
        "union " & _
        "select  col111.t$mcno machine, rou002.t$dsca descr_machine, col076.t$pdno orden, col076.t$cwtt tarifa, hra130.t$dsca descripcion,  " & _
        "col076.t$hrea hora, col077.t$comm coment, col076.t$logn usuario, col076.t$date-5/24 fecha " & _
        "from    baan.tticol111" & session("env") & " col111 " & _
        "inner join baan.ttisfc010" & session("env") & " sfc010 on sfc010.t$mcno = col111.t$mcno " & _
        "inner join baan.ttirou002" & session("env") & " rou002 on rou002.t$mcno = col111.t$mcno " & _ 
        "inner join baan.tticol076" & session("env") & " col076 on col076.t$pdno = sfc010.t$pdno " & _
        "inner join baan.tticol077" & session("env") & " col077 on col077.t$code = col076.t$comm " & _
        "inner join baan.ttihra130" & session("env") & " hra130 on hra130.t$ckow = col076.t$cwtt " & _
        "where   to_char(col111.t$stat-5/24, 'YYYYMMDDHH24MISS') <= to_char(sysdate, 'YYYYMMDDHH24MISS') " & _ 
        "and     to_char(col111.t$fint-5/24, 'YYYYMMDDHH24MISS') >= to_char(sysdate, 'YYYYMMDDHH24MISS') " & _
        "and     to_char(col076.t$date-5/24, 'YYYYMMDDHH24MISS') between to_char(col111.t$stat-5/24, 'YYYYMMDDHH24MISS') " & _
        "and     to_char(col111.t$fint-5/24, 'YYYYMMDDHH24MISS') " & _
        "union " & _
        "select  col085.t$mcno, rou002.t$dsca, '', col085.t$cwtt, hra130.t$dsca, col085.t$hrea, col077.t$comm, col085.t$logn, col085.t$date-5/24 " & _
        "from    baan.tticol085" & session("env") & " col085 " & _
        "inner join baan.ttirou002" & session("env") & " rou002 on rou002.t$mcno = col085.t$mcno " & _
        "inner join baan.ttihra130" & session("env") & " hra130 on hra130.t$ckow = col085.t$cwtt " & _
        "inner join baan.tticol077" & session("env") & " col077 on col077.t$code = col085.t$comm " & _
        "inner join baan.tticol111" & session("env") & " col111 on col111.t$mcno = col085.t$mcno " & _
        "where to_char(col111.t$stat-5/24, 'YYYYMMDDHH24MISS') <= to_char(sysdate, 'YYYYMMDDHH24MISS') " & _
        "and to_char(col111.t$fint-5/24, 'YYYYMMDDHH24MISS') >= to_char(sysdate, 'YYYYMMDDHH24MISS') " & _
        "and to_char(col085.t$date-5/24, 'YYYYMMDDHH24MISS') between to_char(col111.t$stat-5/24, 'YYYYMMDDHH24MISS') " & _
        "and to_char(col111.t$fint-5/24, 'YYYYMMDDHH24MISS') " & _
        "union " & _
        "select  col086.t$mcno, rou002.t$dsca, '', col086.t$cwtt, hra130.t$dsca, col086.t$hrea, col077.t$comm, col086.t$logn, col086.t$date-5/24 " & _
        "from    baan.tticol086" & session("env") & " col086 " & _
        "inner join baan.ttirou002" & session("env") & " rou002 on rou002.t$mcno = col086.t$mcno " & _
        "inner join baan.ttihra130" & session("env") & " hra130 on hra130.t$ckow = col086.t$cwtt " & _   
        "inner join baan.tticol077" & session("env") & " col077 on col077.t$code = col086.t$comm " & _
        "inner join baan.tticol111" & session("env") & " col111 on col111.t$mcno = col086.t$mcno " & _
        "where to_char(col111.t$stat-5/24, 'YYYYMMDDHH24MISS') <= to_char(sysdate, 'YYYYMMDDHH24MISS') " & _
        "and to_char(col111.t$fint-5/24, 'YYYYMMDDHH24MISS') >= to_char(sysdate, 'YYYYMMDDHH24MISS') " & _
        "and to_char(col086.t$date-5/24, 'YYYYMMDDHH24MISS') between to_char(col111.t$stat-5/24, 'YYYYMMDDHH24MISS') " & _
        "and to_char(col111.t$fint-5/24, 'YYYYMMDDHH24MISS')) " & _
        "where   machine = '" + strmachine1 + "'"
        objrs = Server.CreateObject("ADODB.recordset")
        objrs.Open(strSQL, Odbcon)
        i = 0
        Do While Not objrs.EOF
            Session("strmsg") = " "
            strorder = objrs.Fields("orden").Value
            struser = objrs.Fields("usuario").Value
            strmachine = objrs.Fields("machine").Value
            strdescm = objrs.Fields("descr_machine").Value
            strlabor = objrs.Fields("tarifa").Value
            strdescl = objrs.Fields("descripcion").Value
            strhora = objrs.Fields("hora").Value
            strcomm = objrs.Fields("coment").Value
            strfecha = objrs.Fields("fecha").Value
            strtotalhoras = strtotalhoras + strhora

            If strrowcolor = "#87CEEB" Then
                strrowcolor = "#D3D3D3"
            Else
                strrowcolor = "#87CEEB"
            End If
%>
<tr bgcolor="<%=strrowcolor%>">
    <td class="style2"><%=struser%> </td>
    <td class="style2"><%=strorder%></td>
    <td class="style2"><%=strlabor%></td>
    <td class="style2"><%=strdescl%></td>
    <td class="style2"><%=strhora%></td>
    <td class="style2"><%=strfecha%></td>
    <td class="style2"><%=strcomm%></td>
</tr>
<%      
        i = i + 1  
        objrs.MoveNext              
        Loop        
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<tr>
    <td></td>
    <td></td>
    <td></td>
    <td class="style2">Total Hours</td>
    <td class="style2"><%=strtotalhoras%></td>        
</tr>
<%
%>
</table>
</form>
</body>
</html>
