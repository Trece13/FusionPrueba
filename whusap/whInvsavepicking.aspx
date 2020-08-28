<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "Debe loguearse antes de ejecutar esta opcion."
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
    Dim strrowcolor, vcant, vorden, vart, vuni, vubica, vubica1, valm, vfuen, vconj, vpos, vsec, vlote
    Dim orden, art, cant, ubica, alm, uni, fuen, conj, pos, sec, lot
    Dim i, j, k, lon

    vorden = Split(Request.Form("sugerencia"), ",")
    vcant = Split(Request.Form("cant"), ",")
    vfuen = Split(Request.Form("fuen"), ",")
    
    j = UBound(vcant) + 1
    Dim items(j, 4)
    k = 0
    For i = 0 To UBound(vcant)
        items(i, 1) = vcant(i)
        If items(i, 1) <> "" Then
            If FormatNumber(items(i, 1), 0) <> 0 Then
                k = k + 1
            End If
        End If
    Next i
    For i = 0 To UBound(vorden)
        items(i, 2) = vorden(i)
    Next i
    For i = 0 To UBound(vfuen)
        items(i, 3) = vfuen(i)
    Next i
    Dim strSQL, objrs, Odbcon, strmsg, strSQL1
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
    For i = 0 To j - 1
        If items(i, 1) <> "" Then
            orden = items(i, 2)
            cant = items(i, 1)
            fuen = items(i, 3)
            
            strSQL = "select * " & _
                    "from baan.twhcol080" & session("env") & " " & _ 
                    "where t$sour='" + fuen + "'" & _
                    "and t$orno='" + orden + "'" & _
                    "and t$conj='0'" & _  
                    "and t$pono='0'" & _   
                    "and t$sqnb='0'" & _
                    "and t$sern='0'" 
                    objrs=Server.CreateObject("ADODB.recordset")
                    objrs.Open (strSQL, Odbcon)
                    If Not objrs.EOF Then
                        strSQL =  "update baan.twhcol080"& session("env") & " " & _
                                  "set t$esti ='" + convert.tostring(cant) + "'," & _
                                  "t$logn ='" + Session("user") + "'" & _ 
                                  "where t$sour='" + fuen + "'" & _
                                  "and t$orno='" + orden + "'" & _
                                  "and t$conj='0'" & _  
                                  "and t$pono='0'" & _   
                                  "and t$sqnb='0'" & _      
                                  "and t$sern='0'" 
                        objrs=Server.CreateObject("ADODB.recordset")
                        objrs.Open (strSQL, Odbcon)
                        
                        'strSQL = "insert into baan.twhcol080" & session("env") & " " & _
                        '    "values('"+ fuen +"' ,'"+ orden +"' , '0' , '0' ,'0' ,'0' ,'0' , '0' ,' ' ,'0' , " & _
                        '    "0 , ' ' ,'0'  , '" + Session("user") + "' ,0 ,'"+ cant +"' ,sysdate+5/24 ,sysdate+5/24 ,2 ,2 ,2 ,4 ,0 ,0 )"
                        'objrs=Server.CreateObject("ADODB.recordset")
                        'objrs.Open (strSQL, Odbcon)

                        strSQL1 =  "update baan.twhcol080"& session("env") & " " & _
                                  "set t$conf = 1" & _
                                  "where t$sour='" + fuen + "'" & _
                                  "and t$orno='" + orden + "'" & _
                                  "and t$orig='3'" & _
                                  "and t$sour in ('1','4','21')"
                                  objrs=Server.CreateObject("ADODB.recordset")
                                  objrs.Open (strSQL1, Odbcon)
                     else
                        strSQL = "insert into baan.twhcol080" & session("env") & " " & _
                            "values('"+ fuen +"' ,'"+ orden +"' , '0' , '0' ,'0' ,'0' ,'0' , '0' ,' ' ,'0' , " & _
                            "0 , ' ' ,'0'  , '" + Session("user") + "' ,0 ,'"+ cant +"' ,sysdate+5/24 ,sysdate+5/24 ,2 ,2 ,2 ,4 ,0 ,0 )"

                        strSQL1 =  "update baan.twhcol080"& session("env") & " " & _
                                  "set t$conf = 1" & _
                                  "where t$sour='" + fuen + "'" & _
                                  "and t$orno='" + orden + "'" & _
                                  "and t$orig='3'"   
                                  objrs=Server.CreateObject("ADODB.recordset")
                                  objrs.Open (strSQL1, Odbcon)
                     end if               
                    objrs=Server.CreateObject("ADODB.recordset")
                    objrs.Open (strSQL, Odbcon)
                    Session("strmsg") = "Recoleccion Salvada Satisfactoriamente."
        End If
Next i
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
Session("sugerencia") = ""
Session("strmsg") = "Recoleccion Salvada Satisfactoriamente."
Response.Redirect ("whInvPicking.aspx?flag=")
%>
</body>
</html>

