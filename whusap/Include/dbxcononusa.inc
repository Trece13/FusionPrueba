<%
' Conectar a base de datos de Oracle
Odbcon = Server.CreateObject("ADODB.Connection")
Odbcon.ConnectionString = "Driver={Oracle en OraClient11g_home1};Dbq=PBAAN12;UID=webuser;PWD=s3st2m1s" 
Odbcon.Open()
%>