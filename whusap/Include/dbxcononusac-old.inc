<%
' Conectar a base de datos de Oracle

Odbconc = Server.CreateObject("ADODB.Connection")
Odbconc.ConnectionString = "DRIVER={Microsoft ODBC for Oracle};Server=MEXBAAN;UID=consulta;PWD=general" 
Odbconc.Open()
%>
