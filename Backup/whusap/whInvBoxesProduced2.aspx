<%@ Page aspcompat=true %>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must authenticate before you can execute this option."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>
<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<head>
    <style>
        * {
          color:#7F7F7F;
          font-family:Arial,sans-serif;
          font-size:12px;
          font-weight:700;
            height: 28px;
            width: 125px;
        }    
      #config{
          overflow: auto;
          margin-bottom: 10px;
      }
      .config{
          float: left;
          width: 200px;
          height: 250px;
          border: 1px solid #000;
          margin-left: 10px;
      }
      .config .title{
          font-weight: bold;
          text-align: center;
      }
      .config .barcode2D,
      #miscCanvas{
        display: none;
      }
      #submit{
          clear: both;
      }
      #barcodeTarget,
      #canvasTarget{
        margin-top: 20px;
      }        
    </style>

<script type="text/javascript">
</script>

</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body style="width: 292px" bgcolor="#87CEEB">
<%
Dim strord, strsec, strrowcolor, strSQL, strSQL1, objrs, Odbcon
Dim strmsg, strflag

Session("url") = "1"
strflag = Request.QueryString("flag")
if strflag = "Y" then
	strord = ucase(trim(Request.Form("orden")))
	strmsg = Session("strmsg")
else
	strord = ucase(trim(Request.Form("orden")))
	Session("strord")  = strord 
end if
	'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
strord = mid(strord,1,9)

'Validar que Orden esta activa
strSQL = "select col011.t$pdno orden from baan.tticol011" & session("env") & " col011 " & _
"where   col011.t$stat = '2' " & _
"and     col011.t$pdno ='" + strord + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then

    strSQL = "select rtrim(col115.t$pdno) orden from baan.tticol115" & session("env") & " col115 " & _
    " where col115.t$pdno='" + strord + "'"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If objrs.EOF Then
        strSQL1 = "insert into baan.tticol115" & Session("env") & " (t$pdno,t$qana,t$date,t$logn, t$mess, t$refcntd,t$refcntu) " & _
	        " values('" + strord + "',1, sysdate+5/24,'" + Session("user") + "', ' ', 0,0)" 
        	objrs=Server.CreateObject("ADODB.recordset")
	        objrs.Open (strSQL1, Odbcon)       
            Session("strmsg") = "Boxes Register Succesfully."
	        Response.Redirect("whInvBoxesProduced.aspx?flag=") 
    Else
        strSQL1 = "update baan.tticol115" & Session("env") & " set t$qana = t$qana + 1, t$date = sysdate+5/24 " & _
            " where t$pdno='" + strord + "'"
            objrs=Server.CreateObject("ADODB.recordset")
	        objrs.Open (strSQL1, Odbcon)
            Session("strmsg") = "Boxes Register Succesfully."
    	    Response.Redirect("whInvBoxesProduced.aspx?flag=")
    End if
Else
    Session("strmsg") = "Order doesn't be Active for that Machine."
    Response.Redirect("whInvBoxesProduced.aspx?flag=")
End if
    'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
%>
</body>
</html>
