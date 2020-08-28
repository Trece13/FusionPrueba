<%
' Conectar a base de datos SQL
strconn = Server.CreateObject("ADODB.Connection")
strconn.ConnectionString="DRIVER={SQL Server};SERVER=sphoenixdb \sqlserver2012;DATABASE=phoenix;UID=monitor;PWD=monitoreo09"
strconn.Open
%>
