<%
' Conectar a base de datos de Oracle

Odbcont = Server.CreateObject("ADODB.Connection")
Odbcont.ConnectionString = "DRIVER={Microsoft ODBC for Oracle};Server=MEXBAAN;UID=consulta;PWD=general" 
Odbcont.Open()
%>
