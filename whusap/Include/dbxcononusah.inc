<%
' Conectar a base de datos de Oracle
Odbconh = Server.CreateObject("ADODB.Connection")
Odbconh.ConnectionString = "Driver={Oracle in OraClient11g_home1};Dbq=PBAAN12;UID=webuser;PWD=s3st2m1s" 
Odbconh.Open()
%>