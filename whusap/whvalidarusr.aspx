<%@ Page aspcompat=true%>
<%
Dim userid, userpwd, strSQL, Odbcon, objrs
%>
<!-- #include file="include/dbxconon.inc" -->
<%
'Get User identification 
userid = ucase(rtrim(Request.Form("User")))
userpwd = Request.Form("Pwd")
Session("user")=userid

' Validar usuario
strSQL = "select trim(t$pass) pswd,trim(t$nama) Nombre from baan.ttccol300" & trim(Session("env")) & " where upper(trim(T$USER))='" & userid & "'"

objrs = Server.CreateObject("ADODB.Recordset")
objrs.Open (strSQL, Odbcon)
'response.write(strSQL)
'response.end

'Check UserId and Passowrd
If objrs.EOF Then
	Session("Message")= "Invalid user. Please try again."
	Response.Redirect ("whPPOlogin" & trim(Session("envt")) & ".aspx?flg=Y")
else
	if userpwd <> objrs.Fields("pswd").Value then
		Session("Message")= "Invalid password. Please try again."
		Response.Redirect ("whPPOlogin" & trim(Session("envt")) & ".aspx?flg=Y")
	else
		Session("logok") = "whPPOYes"
		Session("username") = objrs.Fields("Nombre").Value
		'Update history
		strSQL = "update baan.ttccol300" & trim(Session("env")) & " set T$UFIN=sysdate+5/24 where upper(trim(T$USER))='" & userid & "'"
		objrs = Server.CreateObject("ADODB.Recordset")
		objrs.Open (strSQL, Odbcon)
		strSQL = "select upper(Sess) Opciones from baan.Usuario_WH_Sesiones" & trim(Session("env")) & " where upper(trim(usuario))='" & userid & "'" 
		objrs = Server.CreateObject("ADODB.Recordset")
		objrs.Open (strSQL, Odbcon)
		If not objrs.EOF Then
			Session("opciones")=objrs.Fields("Opciones").Value
		end if
		Response.Redirect("whPPOMenu" & trim(Session("envt")) & ".aspx")
	end if 

'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
End If
%>