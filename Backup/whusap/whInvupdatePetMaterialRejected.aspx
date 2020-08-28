<%@ Page aspcompat=true Debug="true"%>
<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must login first, before use this session."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>

<html>
<head>
<title>Transferencias</title>
<link href="../basic.css" rel="stylesheet" type="text/css"></head>
<meta name="viewport" content="width=300, user-scalable=no">
<body> 

<%

Dim strord, stritem, strlot, strmlote, strrowcolor, strSQL, objrs, Odbcon, strmsg, strpos, strqty, strware

stritem = ucase(Request.Form("item"))
strlot = ucase(Request.Form("lot"))
strord = ucase(Request.Form("orden"))
strqty = Request.Form("cant")
strqty = Replace(strqty,",",".")
Session("stritem") = "         "+stritem
Session("strord") = strord
Session("strqty") = strqty
strmsg  = ""

'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%

If strqty = 0 then
     Session("strmsg") = "Quantity enter must be higher than zero, please check."
     Response.Redirect("whInvPetMaterialRejected.aspx?flag=") 
End If

If strord = "" then
     Session("strmsg") = "Must enter Work Order, please check."
     Response.Redirect("whInvPetMaterialRejected.aspx?flag=")  
End If

If stritem = "" then
     Session("strmsg") = "Must enter Item, please check."
     Response.Redirect("whInvPetMaterialRejected.aspx?flag=") 
End If

'Validar si el item maneja lotes
strSQL = "select  t$kltc mlote from baan.ttcibd001" & Session("env") & _
" where trim(t$item)='" + stritem + "'" 
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
    strmlote = objrs.Fields("mlote").Value

    If (strmlote = 1) then
        'Validar que el lote exista
         strSQL = "select  t$clot from baan.twhltc100" & Session("env") & _
        " where trim(t$item)='" + stritem + "'" & _
        " and trim(t$clot) ='" + strlot +"'"
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
        If objrs.EOF Then   
            Session("strmsg") = "Lot doesn't exist, please check."
            Response.Redirect("whInvPetMaterialRejected.aspx?flag=")    
        End If  
    Else
        strlot = " "                  
    End If            
End If

'Buscar Datos Orden
strSQL = "select rtrim(cst001.t$pdno) ordenf from baan.tticst001" & session("env") & " cst001 inner join baan.ttisfc001" & session("env") & " sfc001 " & _
" on cst001.t$pdno = sfc001.t$pdno " & _
" where sfc001.t$osta in('5','7','9') and cst001.t$pdno='" + strord + "'" 
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then

    'Buscar Datos Item
    strSQL = "select  t$pdno orden, t$pono pos, t$sitm item, t$cwar wareh from baan.tticst001" & Session("env") & _
    " where trim(t$sitm)='" + stritem + "'" & _
    " and t$pdno ='" + strord + "'"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If Not objrs.EOF Then
        strware = objrs.Fields("wareh").Value
        strpos = objrs.Fields("pos").Value
        strqty = replace(strqty,",","")

        strSQL = "select t$orno, t$pono, t$item from baan.tticol080" & Session("env") & "" & _
        " where t$orno = '" + strord + "'" & _
        " and t$pono = '" +  Cstr(strpos) + "'" & _
        " and trim(t$item) = trim('" +  stritem + "')"
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
        If objrs.EOF Then
            strSQL = "insert into baan.tticol080" & Session("env") & " (t$orno,t$pono,t$item,t$cwar,t$qune,t$logn,t$date,t$proc,t$clot, t$refcntd,t$refcntu) " & _
            " values('" + Session("strord") + "','"  +  Cstr(strpos) + "','" + Session("stritem") + "','" + strware + "','" + strqty + "','" + Session("user") + "',sysdate+5/24,2,'" + strlot + "',0,0)"
        Else
            strSQL = "update baan.tticol080" & Session("env") & "" & _
            " set t$qune  = t$qune + '"  + strqty + "'" & _
            " where t$orno = '" +  strord + "'" & _
            " and t$pono = '" +  Cstr(strpos) + "'" & _
            " and trim(t$item) = trim('" +  stritem + "')"
        End If
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
        strmsg = "Material Consumption saved succesfully"
        Session("strmsg") = strmsg 
        Response.Redirect("whInvPetMaterialRejected.aspx?flag=") 
    Else
        strmsg = "Order doesn't have material items registered."
        Session("strmsg") = strmsg 
        Response.Redirect("whInvPetMaterialRejected.aspx?flag=") 
    End if
Else
    strmsg = "Order To, doesn't exist or the status is not active, release or completed."
    Session("strmsg") = strmsg 
    Response.Redirect("whInvPetMaterialRejected.aspx?flag=") 
End if
'Desconectar a base de datos
%>
<!-- #include file="include/dbxconoff.inc" -->
<%
%>
</body>
</html>
