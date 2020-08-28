<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You login firsts, before use this session."
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
    Dim strrowcolor, vcant, vrecep, vlote, vhabl, vorden, vart, vuni, vfuen, vconj, vpos, vsec
    Dim recep, orden, art, cant, cant1, lote, habl, uni, fuen, conj, pos, sec
    Dim i, j, k
    vrecep = Split(Request.Form("recep"), ",")
    vorden = Split(Request.Form("orden"), ",")
    vart = Split(Request.Form("art"), ",")
    vuni = Split(Request.Form("uni"), ",")
    vcant = Split(Request.Form("cant"), ",")
    vlote = Split(Request.Form("lote"), ",")
    vhabl = Split(Request.Form("habl"), ",")
    vfuen = Split(Request.Form("fuen"), ",")
    vconj = Split(Request.Form("conj"), ",")
    vpos = Split(Request.Form("pos"), ",")
    vsec = Split(Request.Form("sec"), ",")

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
    For i = 0 To UBound(vlote)
        items(i, 2) = vlote(i)
    Next i
    For i = 0 To UBound(vhabl)
        items(i, 3) = vhabl(i)
    Next i
    For i = 0 To UBound(vorden)
        items(i, 4) = vorden(i)
    Next i
    For i = 0 To UBound(vart)
        items(i, 5) = vart(i)
    Next i
    For i = 0 To UBound(vuni)
        items(i, 6) = vuni(i)
    Next i
    For i = 0 To UBound(vrecep)
        items(i, 7) = vrecep(i)
    Next i
    For i = 0 To UBound(vfuen)
        items(i, 8) = vfuen(i)
    Next i
    For i = 0 To UBound(vconj)
        items(i, 9) = vconj(i)
    Next i
    For i = 0 To UBound(vpos)
        items(i, 10) = vpos(i)
    Next i
    For i = 0 To UBound(vsec)
        items(i, 11) = vsec(i)
    Next i
    Dim strSQL, objrs, Odbcon, strmsg
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
    For i = 0 To j - 1
    If items(i, 1) <> "" Then
        If items(i, 3) <> "" Then
            recep = items(i, 7)
            orden = items(i, 4)
            art = items(i, 5)
            cant = items(i, 1)
            lote = items(i, 2)
            if lote = "" then
                lote = " "
            else
                lote = items(i, 2)
                lote = trim(lote)
            end if
            habl = items(i, 3)
            uni = items(i, 6)
            fuen = items(i,8)
            conj = items(i, 9)
            pos = items(i, 10)
            sec = items(i,11)

            strSQL = "select * " & _
                 "from baan.twhcol080" & Session("env") & " " & _ 
                 "where t$sour='" + fuen + "'" & _
                 "and t$orno='" + orden + "'" & _
                 "and t$conj='" + conj + "'" & _
                 "and t$pono='" + pos + "'" & _
                 "and t$sqnb='" + sec + "'" & _
                 "and t$sern='0' " & _
                 "and t$orig = 1 "
                 objrs=Server.CreateObject("ADODB.recordset")
                 objrs.Open (strSQL, Odbcon)
            If Not objrs.EOF Then
                strSQL =  "update baan.twhcol080" & Session("env") & " " & _
                      "set t$qana=" + convert.tostring(cant) + " ," & _
                      "t$clot = upper('"+ lote +"') ," & _
                      "t$habl='"+ habl +"' ," & _
                      "t$logn='" + Session("user") +"' " & _
                      "where t$sour='" + fuen + "'" & _
                      "and t$orno='" + orden + "'" & _
                      "and t$conj='" + conj + "'" & _
                      "and t$pono='" + pos + "'" & _
                      "and t$sqnb='" + sec + "'"  & _
                      "and t$sern='0' " & _
                      "and t$orig = 1 "
                      objrs=Server.CreateObject("ADODB.recordset")
                      objrs.Open (strSQL, Odbcon)
            Else
                strSQL = "insert into baan.twhcol080" & Session("env") & " " & _
                     "values('"+ fuen +"' ,'"+ orden +"' , '"+ conj +"' , '"+ pos +"' ,'"+ sec +"' ,'0', '"+ recep +"' ,' ' ,' ' ,'"+ art +"' , " & _
                     "" + convert.tostring(cant) +" , '" + uni + "' , upper('" + lote + "') ,'" + Session("user") + "' , '"+ habl +"' ,0 ,sysdate+5/24 ,sysdate+5/24 ,2 ,2 ,2 ,1 ,0 ,0 )" 
                    objrs=Server.CreateObject("ADODB.recordset")
                    objrs.Open (strSQL, Odbcon)
            End If                     
        Else
            Session("strmsg") = "Receipt doesn't have Pallet Data. Please try Again."
            Response.Redirect ("whInvReceipts.aspx?")
        End If
    Else
        Session("strmsg") = "Receipt doesn't have Quantity.  Please try Again."
        Response.Redirect ("whInvReceipts.aspx?")
    End If
Next i
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
Session("recep") = ""
Session("strmsg") = "Receipt saved sucessfully."
Response.Redirect ("whInvReceipts.aspx?flag=Y")
%>
</body>
</html>

