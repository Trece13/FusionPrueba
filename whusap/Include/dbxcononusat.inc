<%
' Conectar a base de datos de Oracle
Odbcont = Server.CreateObject("ADODB.Connection")
Odbcont.ConnectionString = "Driver={Oracle in OraClient11g_home1};Dbq=PBAAN12;UID=webuser;PWD=s3st2m1s" 
Odbcont.Open()
%>