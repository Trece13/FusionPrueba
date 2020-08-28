<%@ Page aspcompat=true %>
<%
Dim userid, userpwd, strSQL, Odbcon, objrs
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
'Get User identification 
userid = ucase(rtrim(Request.Form("User")))
userpwd = Request.Form("Pwd")
Session("user")=userid
Session("env")=448
' Validar usuario
    strSQL = "select trim(t$pass) pswd,trim(t$nama) Nombre from baan.ttccol300" & session("env") & " where upper(trim(T$USER))='" & userid & "'"
objrs = Server.CreateObject("ADODB.Recordset")
objrs.Open (strSQL, Odbcon)

'Check UserId and Passowrd
If objrs.EOF Then
        Session("Message") = "Invalid User. Please Try Again."
        Response.Redirect("whlogini.aspx?flg=Y")
else
	if userpwd <> objrs.Fields("pswd").Value then
            Session("Message") = "Invalid Password, try again."
            Response.Redirect("whlogini.aspx?flg=Y")
	else
		Session("logok") = "OKYes"
		Session("username") = objrs.Fields("Nombre").Value
		'Update history
            strSQL = "update baan.ttccol300" & Session("env") & " set T$UFIN=sysdate+5/24 where upper(trim(T$USER))='" & userid & "'"
		objrs = Server.CreateObject("ADODB.Recordset")
		objrs.Open (strSQL, Odbcon)
            Response.Redirect("WHMenui.aspx")
        End If

        'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
End If
%>