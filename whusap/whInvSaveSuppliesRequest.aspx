<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must login firts, before use this session."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>
<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<head>

    <style type="text/css">
        .style1
        {
            color: White;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-style: normal;
            text-align: Left;
            height: 10px;
            width: auto
        }
        .style2
        {
            color: Black;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-style: normal;
            text-align: Left;
            height: 10px;
            }
        .style3
        {
            color: Black;
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-style: normal;
            text-align: Center;
            height: 10px;
            width: auto
        }  
        .errorMsg
        {
            color: Black;
            font-weight: bold;
            font-size: medium;
        }
</style>  
        </style>
</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body style="width: 292px" bgcolor="#87CEEB">
<%
    Dim strrowcolor, vmachine, vtarea, vtype, vhour, vhourc, vart, vtipo
    Dim machine, tarea, type, hour, art, tipo, strunit, strcwar, sec
    Dim i, j, k
    Dim strSQL, objrs, Odbcon, strmsg
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
i = 1
Do While i <=5  
    vmachine = Request.Form("machine"&i)
    vart = Request.Form("vart"&i)
    vhourc = Request.Form("Horas"&i)

     machine = vmachine
     hour = vhourc
     art = trim(vart)
     if (vart <> "0") then
        strSQL = "select t$cwar wareh, t$cuni unit from baan.ttcibd001" & session("env") & " " & _
                "where trim(t$item) = '" + art + "'"
                objrs=Server.CreateObject("ADODB.recordset")
                objrs.Open (strSQL, Odbcon)
                If Not objrs.EOF Then
	                strcwar = objrs.Fields("wareh").Value
                    strunit = objrs.Fields("unit").Value

                    strSQL = " select max(t$seqn) seq from baan.tticol095" & session("env") & " " & _
                        " where  t$mcno = '" + machine + "'" & _
                        " and t$item = '" + vart + "'" & _
                        " and t$cwar = '" + strcwar + "'"
                        objrs=Server.CreateObject("ADODB.recordset")
                        objrs.Open (strSQL, Odbcon)
                        If Not objrs.EOF Then
                        sec = objrs.Fields("seq").Value
                            If isdbnull(sec) Then
                                sec = 1
                            Else                                
                                sec = sec + 1
                            End If
                        Else
                            sec = 1
                        End If



                    strSQL = "insert into baan.tticol095" & session("env") & " " & _
                    "values('" + convert.tostring(machine) + "', '" + convert.tostring(sec) + "' , '" + convert.tostring(vart) +"', '" + convert.tostring(strcwar) +"', '" + convert.tostring(strunit) +"',  " + convert.tostring(hour) +", '" + Session("user") + "', sysdate+5/24, 2, ' ' , 0 , 0 )"                   
                    objrs=Server.CreateObject("ADODB.recordset")
                    objrs.Open (strSQL, Odbcon)
                End If
     end if
i = i + 1
Loop
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
Session("strmsg") = "Supplies were saved succesfully."
Response.Redirect ("whInvSuppliesRequest.aspx?flag=")
%>
</body>
</html>

