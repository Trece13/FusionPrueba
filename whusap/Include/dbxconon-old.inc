<%
' Conectar a base de datos de Oracle

Odbcon = Server.CreateObject("ADODB.Connection")
Odbcon.ConnectionString = "DRIVER={Microsoft ODBC for Oracle};Server=MEXBAAN;UID=consulta;PWD=general" 
Odbcon.Open()
%>
