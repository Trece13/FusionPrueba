<%
' Conectar a base de datos de Oracle
Odbconc = Server.CreateObject("ADODB.Connection")
Odbconc.ConnectionString = "Driver={Oracle in OraClient11g_home1};Dbq=PBAAN12;UID=webuser;PWD=s3st2m1s" 
Odbconc.Open()
%>