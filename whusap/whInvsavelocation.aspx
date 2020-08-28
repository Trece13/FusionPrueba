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
    Dim strrowcolor, vcant, vorden, vart, vuni, vubica, vubica1, valm, valm1, vfuen, vconj, vpos, vsec, vlot
    Dim orden, art, cant, ubica, alm, uni, fuen, conj, pos, sec, lot
    Dim i, j, k, lon

    vorden = Split(Request.Form("orden"), ",")
    vart = Split(Request.Form("art"), ",")
    vuni = Split(Request.Form("uni"), ",")
    vcant = Split(Request.Form("cant"), ",")
    'valm = mid(ucase(Request.Form("alm")),1,6)
    valm = Split(Request.Form("alm"), ",")
    lon = len(Request.Form("ubica"))
    'vubica1 = mid(ucase(Request.Form("ubica")),7,lon)
    vubica = Split(Request.Form("ubica"), ",")
    vfuen = Split(Request.Form("fuen"), ",")
    vconj = Split(Request.Form("conj"), ",")
    vpos = Split(Request.Form("pos"), ",")
    vsec = Split(Request.Form("sec"), ",")
    vlot = Split(Request.Form("lot"), ",")
    'response.write(vart)   
    j = UBound(vcant) + 1
    Dim items(j, 12)
    k = 0
    For i = 0 To UBound(vcant)
        items(i, 1) = vcant(i)
        If items(i, 1) <> "" Then
            If FormatNumber(items(i, 1), 0) <> 0 Then
                k = k + 1
            End If
        End If
    Next i
    For i = 0 To UBound(vubica)
        items(i, 2) = vubica(i)
    Next i
    For i = 0 To UBound(vorden)
        items(i, 3) = vorden(i)
    Next i
    For i = 0 To UBound(vart)
        items(i, 4) = vart(i)
    Next i
    For i = 0 To UBound(vuni)
        items(i, 5) = vuni(i)
    Next i
    For i = 0 To UBound(vfuen)
        items(i, 6) = vfuen(i)
    Next i
    For i = 0 To UBound(vconj)
        items(i, 7) = vconj(i)
    Next i
    For i = 0 To UBound(vpos)
        items(i, 8) = vpos(i)
    Next i
    For i = 0 To UBound(vsec)
        items(i, 9) = vsec(i)
    Next i
    For i = 0 To UBound(valm)
        items(i, 10) = valm(i)
    Next i
    For i = 0 To UBound(vlot)
        items(i, 11) = vlot(i)
    Next i
    Dim strSQL, objrs, Odbcon, strmsg
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
    For i = 0 To j - 1

        If items(i, 1) <> "" Then
            orden = items(i, 3)
            art = items(i, 4)
            cant = items(i, 1)
            ubica = items(i, 2)
            alm = items(i, 10)
            'alm = mid(items(i, 2), 1, 6)
            'ubica = mid(items(i, 2), 7, lon)
            uni = items(i, 5)
            fuen = items(i,6)
            conj = items(i, 7)
            pos = items(i, 8)
            sec = items(i,9)
            lot = items(i,11)
           strSQL = "select * " & _
                    "from baan.twhwmd300" & Session("env") & "  where t$cwar= upper('" + alm + "') and trim(t$loca)= upper('" + ubica + "')"
                    objrs=Server.CreateObject("ADODB.recordset")
                    objrs.Open (strSQL, Odbcon)

           If Not objrs.EOF Then        

              strSQL = "select * " & _
                    "from baan.twhcol080" & Session("env") & " " & _ 
                    "where t$sour='" + fuen + "'" & _
                    "and t$orno='" + orden + "'" & _
                    "and t$conj='" + conj + "'" & _
                    "and t$pono='" + pos + "'" & _
                    "and t$sqnb='" + sec + "'" & _
                    "and t$loca='" + ubica + "'" & _
                    "and t$clot='" + lot + "'" & _
                    "and t$orig = 2 "
                    objrs=Server.CreateObject("ADODB.recordset")
                    objrs.Open (strSQL, Odbcon)
                    If Not objrs.EOF Then
                        strSQL =  "update baan.twhcol080" & Session("env") & " " & _
                                  "set t$qana=t$qana + " + convert.tostring(cant) + " ," & _
                                  "t$cwar=upper('"+ alm +"') ," & _
                                  "t$loca=upper('"+ ubica +"') ," & _
                                  "t$logn='" + Session("user")  +"' , " & _
                                  "t$clot=upper('"+ lot +"') " & _
                                  "where t$sour='" + fuen + "'" & _
                                  "and t$orno='" + orden + "'" & _
                                  "and t$conj='" + conj + "'" & _
                                  "and t$pono='" + pos + "'" & _
                                  "and t$sqnb='" + sec + "'"           
                     Else
                        strSQL = "insert into baan.twhcol080" & Session("env") & " " & _
                                "values('"+ fuen +"' ,'"+ orden +"' , '"+ conj +"' , '"+ pos +"' ,'"+ sec +"' ,'0', ' ' ,upper('"+ alm +"') , upper('"+ ubica +"') ,'"+ art +"' , " & _
                                "" + cant +" , '" + uni + "' ,'" + lot + "' , '" + Session("user") + "' ,0 ,0 ,sysdate+5/24 ,sysdate+5/24 ,2 ,2 ,2 ,2 ,0 ,0 )"
                     End if               
                objrs=Server.CreateObject("ADODB.recordset")
                objrs.Open (strSQL, Odbcon)
            else
                Session("strmsg") = "Ubicacion o Almacen no existe, por favor verifique"
                Response.Redirect ("whInvLocations.aspx?flag=Y")
            End If
    End If
Next i
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
Session("ocompra") = ""
Session("strmsg") = "Orden Ubicada Satisfactoriamente."
Response.Redirect ("whInvLocations.aspx")
%>
</body>
</html>

