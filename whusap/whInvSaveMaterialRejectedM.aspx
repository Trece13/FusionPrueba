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
    Dim strrowcolor, vorden, vcant, vcantc, vreason, vart, vpos, vlot, vobse, vmaquina, vturno, vunidad, vreject, vdispo
    Dim orden, cant, reason, art, secuencia, pos, maquina, turno, unidad, reject, dispo, proc, vmlote
    Dim i, j, k
    vorden = Request.Form("orden")
    vart = Request.Form("art")
    vpos = Request.Form("posc")
    vcant = Request.Form("cant")
    vreason = Request.Form("reasonc")
    vturno = Request.Form("shift")
    vunidad = Request.Form("unidad")
    vlot = ucase(Request.Form("lot"))
    vmlote = Request.Form("lotem")
    if (vlot = "") then
        vlot = " "
    end if
    vobse = Request.Form("obser")
    vreject = Request.Form("rejectc")
    vdispo = Request.Form("rejectc")
    if (vdispo = 4) then
        proc = 2
    else
        proc = 1
    end if
    Dim strSQL, objrs, Odbcon, strmsg
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
            orden = vorden
            pos = vpos
            art = vart
            cant = vcant
            reason = vreason
            turno = vturno
            unidad = vunidad
            reject = vreject
            dispo = vdispo
    strSQL = "select t$pdno, t$mcno from baan.tticol011" & session("env") & " " & _
            " where t$pdno = '" + vorden + "'"
            objrs=Server.CreateObject("ADODB.recordset")
            objrs.Open (strSQL, Odbcon)
            If Not objrs.EOF Then
                If (isdbnull(objrs.Fields("t$pdno").Value)) then
                    maquina = " "
                End If
                maquina = objrs.Fields("t$mcno").Value
            Else
               maquina = " " 
            End If

If (Int(vmlote) = 1) then
            'Validar el lote digitado
            strSQL = "select  ltc100.t$item item, ltc100.t$clot lote, ltc100.t$quam cant from baan.twhltc100" & session("env") & " ltc100 " & _
            "where ltc100.t$item = '" + convert.tostring(vart) + "' " & _
            "and upper(ltc100.t$clot) = '" + convert.tostring(vlot) + "'"
            objrs=Server.CreateObject("ADODB.recordset")
            objrs.Open (strSQL, Odbcon)
            If Not objrs.EOF then

                strSQL = "select  orden, pos, max(secu) from (select t$seqn secu, t$pdno orden, t$pono pos from baan.tticol100" & session("env") & " " & _
                " union select t$seqn secu, t$pdno orden, t$pono pos from baan.tticol101" & session("env") & ")" & _
                " where orden = '" + orden + "'" & _
                " and pos  = '" + vpos + "'" & _
                " group by orden, pos"
                objrs=Server.CreateObject("ADODB.recordset")
                objrs.Open (strSQL, Odbcon)
                If Not objrs.EOF Then
                    If (isdbnull(objrs.Fields("max(secu)").Value)) then
                        secuencia = 1
                    Else
                        secuencia = objrs.Fields("max(secu)").Value
                        secuencia = secuencia + 1
                    End If
                Else
                    secuencia = 1
                End If

                session("orden") = orden
                session("secuencia") = secuencia
                session("pos") = pos
                session("item") = art
                session("reason") = reason
                session("cant") = cant
                session("lot") = vlot
                session("obse") = mid(vobse, 1, 255)
                session("machine") = maquina
                session("dispo") = vdispo
                session("unidad") = vunidad
                session("mlote") = vmlote
                vobse = session("obse")
'"values('"+ orden +"' ,'"+ convert.tostring(pos) +"', '"+ convert.tostring(secuencia) +"' , '"+ maquina +"' , '"+ turno +"', '"+ art +"' , "+ convert.tostring(cant) +" , '"+ reason +"' , '"+ reject +"', '"+ vlot +"', substr('" + vobse + "', 1, 255), '"+ Session("user") +"' ,sysdate+5/24, '"+ convert.tostring(dispo) +"', 'NONE', sysdate+5/24, '"+ convert.tostring(proc) +"', ' ', 0 ,0)"
                If orden <> "" Then
                    strSQL = "insert into baan.tticol100" & session("env") & " " & _
                    "values('"+ orden +"' ,'"+ convert.tostring(pos) +"', '"+ convert.tostring(secuencia) +"' , '"+ maquina +"' , '"+ turno +"', '"+ art +"' , "+ convert.tostring(cant) +" , '"+ reason +"' , '"+ reject +"', '"+ vlot +"', substr('" + vobse + "', 1, 255), '"+ Session("user") +"' ,sysdate+5/24, 4, 'NONE', sysdate+5/24, '"+ convert.tostring(proc) +"', ' ', 0 ,0)"
                    objrs = Server.CreateObject("ADODB.recordset")
                    objrs.Open(strSQL, Odbcon)
                End If
            Else
                Session("strmsg") = "Lot doesn't exits for this item. Cannot Continue."
                Response.Redirect ("whInvMaterialRejectedM.aspx?flag=")
            End If
Else
    strSQL = "select  orden, pos, max(secu) from (select t$seqn secu, t$pdno orden, t$pono pos from baan.tticol100" & session("env") & " " & _
                " union select t$seqn secu, t$pdno orden, t$pono pos from baan.tticol101" & session("env") & ")" & _
                " where orden = '" + orden + "'" & _
                " and pos  = '" + vpos + "'" & _
                " group by orden, pos"
                objrs=Server.CreateObject("ADODB.recordset")
                objrs.Open (strSQL, Odbcon)
                If Not objrs.EOF Then
                    If (isdbnull(objrs.Fields("max(secu)").Value)) then
                        secuencia = 1
                    Else
                        secuencia = objrs.Fields("max(secu)").Value
                        secuencia = secuencia + 1
                    End If
                Else
                    secuencia = 1
                End If

                session("orden") = orden
                session("pos") = pos
                session("secuencia") = secuencia
                session("item") = art
                session("reason") = reason
                session("cant") = cant
                session("lot") = vlot
                session("obse") = mid(vobse, 1, 255)
                session("machine") = maquina
                session("dispo") = vdispo
                session("unidad") = vunidad
                session("mlote") = vmlote
                vobse = session("obse")
'"values('"+ orden +"' ,'"+ convert.tostring(pos) +"', '"+ convert.tostring(secuencia) +"' , '"+ maquina +"' , '"+ turno +"', '"+ art +"' , "+ convert.tostring(cant) +" , '"+ reason +"' , '"+ reject +"', '"+ vlot +"', substr('" + vobse + "', 1, 255), '"+ Session("user") +"' ,sysdate+5/24, '"+ convert.tostring(dispo) +"', 'NONE', sysdate+5/24, '"+ convert.tostring(proc) +"', ' ', 0 ,0)"
                If orden <> "" Then
                    strSQL = "insert into baan.tticol100" & session("env") & " " & _
                    "values('"+ orden +"' ,'"+ convert.tostring(pos) +"', '"+ convert.tostring(secuencia) +"' , '"+ maquina +"' , '"+ turno +"', '"+ art +"' , "+ convert.tostring(cant) +" , '"+ reason +"' , '"+ reject +"', '"+ vlot +"', substr('" + vobse + "', 1, 255), '"+ Session("user") +"' ,sysdate+5/24, 4, 'NONE', sysdate+5/24, '"+ convert.tostring(proc) +"', ' ', 0 ,0)"
                    objrs = Server.CreateObject("ADODB.recordset")
                    objrs.Open(strSQL, Odbcon)
                End If
End if
'        End If
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
Session("strmsg") = "Material Rejected was saved succesfully."
Response.Redirect ("whInvLabelMaterialRejectedM.aspx?flag=")
%>
</body>
</html>

