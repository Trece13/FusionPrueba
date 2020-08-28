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
    Dim strrowcolor, vcant, vorden, vart, vuni, vubica, vubica1, valm, vfuen, vconj, vpos, vsec, vser, vlote
    Dim orden, art, cant, ubica, alm, uni, fuen, conj, pos, sec, ser, lot
    Dim i, j, k, lon

    vorden = Split(Request.Form("sugerencia"), ",")
    vart = Split(Request.Form("art"), ",")
    vuni = Split(Request.Form("uni"), ",")
    vcant = Split(Request.Form("cant"), ",")
    lon = len(Request.Form("ubica"))
    valm = Split(Request.Form("alm"), ",")
    vubica = Split(Request.Form("ubica"), ",")
    vfuen = Split(Request.Form("fuen"), ",")
    vconj = Split(Request.Form("conj"), ",")
    vpos = Split(Request.Form("pos"), ",")
    vsec = Split(Request.Form("sec"), ",")
    vser = Split(Request.Form("ser"), ",")
    vlote = Split(Request.Form("lot"), ",")        

    j = UBound(vcant) + 1
    Dim items(j, 13)
    k = 0
    For i = 0 To UBound(vcant)
        items(i, 1) = vcant(i)
        items(i, 1) = Replace(Request("vcant(i)"),",",".")
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
    For i = 0 To UBound(vser)
        items(i, 10) = vser(i)
    Next i
    For i = 0 To UBound(vlote)
        items(i, 11) = vlote(i)
    Next i
    For i = 0 To UBound(valm)
        items(i, 12) = valm(i)
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
            alm = items(i, 12)
            ubica = items(i, 2)
            If (ubica = "") then
                ubica = "0"
            End If
            uni = items(i, 5)
            fuen = items(i,6)
            conj = items(i, 7)
            pos = items(i, 8)
            sec = items(i,9)
            ser = items(i,10)
            lot = items(i,11)
            If (lot = "") then
                lot = "0"
            End If
            
           strSQL = "select * " & _
                    "from baan.twhcol080" & session("env") & " " & _ 
                    "where t$sour='" + fuen + "'" & _
                    "and t$orno='" + orden + "'" & _
                    "and t$conj='" + conj + "'" & _
                    "and t$pono='" + pos + "'" & _
                    "and t$sqnb='" + sec + "'" & _
                    "and t$sern='" + ser + "'"
                    objrs=Server.CreateObject("ADODB.recordset")
                    objrs.Open (strSQL, Odbcon)
                    If Not objrs.EOF Then
                        strSQL =  "update baan.twhcol080" & session("env") & " " & _
                                  "set t$qana =" + convert.tostring(cant) + " ," & _
                                  "t$cwar = upper('"+ alm +"') ," & _
                                  "t$loca = upper('"+ ubica +"') ," & _
                                  "t$clot = upper('"+ lot +"') ," & _
                                  "t$logn ='" + Session("user") +"' " & _
                                  "where t$sour='" + fuen + "'" & _
                                  "and t$orno='" + orden + "'" & _
                                  "and t$conj='" + conj + "'" & _
                                  "and t$pono='" + pos + "'" & _
                                  "and t$sqnb='" + sec + "'" & _
                                  "and t$sern='" + ser + "'"          
                     else
                        strSQL = "insert into baan.twhcol080" & session("env") & " " & _
                                "values('"+ fuen +"' ,'"+ orden +"' , '"+ conj +"' , '"+ pos +"' ,'"+ sec +"' ,'"+ ser +"' ,' ', upper('"+ alm +"') , upper('"+ ubica +"') ,'"+ art +"' , " & _
                                "" + cant +" , '" + uni + "' ,'" + lot + "'  , '" + Session("user") + "' ,0 ,0 ,sysdate+5/24 ,sysdate+5/24 ,2 ,2 ,2 ,3 ,0 ,0 )"
                        objrs=Server.CreateObject("ADODB.recordset")
                        objrs.Open (strSQL, Odbcon)
                     end if               
        End If
Next i
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
Session("sugerencia") = ""
Session("strmsg") = "Orden Salvada Satisfactoriamente."
Response.Redirect ("whInvAdvices.aspx?flag=Y")
%>
</body>
</html>

