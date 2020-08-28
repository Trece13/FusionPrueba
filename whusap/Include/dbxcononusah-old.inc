<%
' Conectar a base de datos de Oracle

Odbconh = Server.CreateObject("ADODB.Connection")
Odbconh.ConnectionString = "DRIVER={Microsoft ODBC for Oracle};Server=MEXBAAN;UID=consulta;PWD=general" 
Odbconh.Open()
%>
