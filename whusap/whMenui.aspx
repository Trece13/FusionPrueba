<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must login first, before use this session."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>
<HTML>
<link href="../basic.css" rel="stylesheet" type="text/css">
<HEAD>
  <TITLE>Bodega</TITLE>
    <style type="text/css">
        .style1
        {
            width: 351px;
        }
        .style2
        {
            width: 121px;
        }
        .style3
        {
            height: 31px;
        }
        .errorMsg
        {
            color: Black;
            font-weight: bold;
            font-size: medium;
        }
</style>
    </style>
</HEAD>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB">
<FORM>
<table>
<tr>
<div>
<td class="style2"><IMG SRC = "images/logophoenix_s.jpg" style="width: 116px"></td>
</div>
<td class="style1"><H3>Mobile Aplications - Baan Fusion</H3></td>
</TR>
<tr>
<td align="left" class="style2" width="70%" ><a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a></td>
</TR>
<tr>
<td class="style2"><b>User </b>:</td>
<td class="style1"><b><%=Session("user")%></b></td>
</tr>
<tr>
<td class="style2"><b>Name </b>:</td>
<td class="style1" colspan="2"><b><%=Session("username")%></b></td>
</tr>
</TABLE>
<%
Dim strorden, strSQL, objrs, Odbcon, strmsg, strsess, strboton
'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
'Validar opciones del usuario y el orden
strSQL = "select  col303.t$orde orden, trim(col302.t$prog) programa, trim(col302.t$boto) boton" & _
    " from    baan.ttccol303" & session("env") & " col303 inner join baan.ttccol302" & session("env") & " col302 on col302.t$SESS = col303.t$SESS " & _
    " where    upper(trim(col303.t$user))= '" + session("user") + "'" & _
    " and col303.t$orde <> 0 order by col303.t$orde "
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)


'Modificacion Edwing Loaiza
' Para la migracion al nuevo sitio es necesario enviar los datos de la sesion
' se envian en una cadena
' sesVar: Arreglo con las variables de sesion
' urlParametro: valores a encadenar en el vinculo

    Dim sesVar(3) As String
   sesVar(0) = Session("user")
   sesVar(1) = Session("logok")
   sesVar(2) = Session("username")
    Dim valor As Integer = 0
    Dim numCliclo As Integer
    Dim urlParametro As String = ""
   For numCliclo = LBound(sesVar) to UBound(sesVar)
      valor = valor + 1
      urlParametro = urlParametro & "Valor" & valor & "=" & sesVar(numCliclo)
      urlParametro = urlParametro & "&"
   Next
   
   urlParametro = Left(urlParametro, Len(urlParametro) - 1)
' Fin Modificacion


Do While Not objrs.EOF
	strorden = objrs.Fields("orden").Value
    strsess = objrs.Fields("programa").Value
    strboton = objrs.Fields("boton").Value

    'La cadena buscada puede variar de acuerdo a la ruta

        If InStr(strsess, "fusionp/") Then
            strsess = strsess + "?" + urlParametro
        End If

    
    If ((strorden mod 4) = 1) then
%>
    <table>
    <tr>


        <td align="center" class="style3"><a href="<%=strsess%>"><img src="<%=strboton%>"></a></td>
<%
    End If
    If ((strorden mod 4) = 2) then
%>
        <td align="center" class="style3"><a href="<%=strsess%>"><img src="<%=strboton%>"></a></td>
<%
    End If
    If ((strorden mod 4) = 3) then
%>
        <td align="center" class="style3"><a href="<%=strsess%>"><img src="<%=strboton%>"></a></td>
<%
    End If
    If ((strorden mod 4) = 0) then
%>
        <td align="center" class="style3"><a href="<%=strsess%>"><img src="<%=strboton%>"></a></td>
    </tr>
    </table>
<%
    End If
objrs.MoveNext
Loop
'Modificacion Edwing Loaiza
'07.08.2016
'Se crea una cookie para manejo de seguridad entre las aplicaciones

Response.Cookies("userAppInit")("initForm") ="true"
' Fin de Modificación
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
</form>
</body>
</html>