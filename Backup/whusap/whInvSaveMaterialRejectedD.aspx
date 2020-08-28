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
    Dim strrowcolor, vitem, vwarehouse, vlot, vcant, vreason, vcomm, vdisp, vwarehd, vitemd, vsupp, vmlot

    vitem = session("item")
    vwarehouse = session("ware")
    vlot = session("lot")
    vmlot = session("mlot")
    if vlot = "" then
        vlot = " "
    end if
    vcant = Request.Form("cant")
    vreason = Request.Form("reasonc")
    vcomm = Request.Form("obser")
    if vcomm = "" then
        vcomm = " "
    end if
    vdisp = Request.Form("rejectc")
    vwarehd = Request.Form("warehc")
    vitemd = Request.Form("itemc")
    vsupp = Request.Form("supplier")
    session("cant") = vcant
    session("vitemr") = vitemd
    Dim strSQL, objrs, Odbcon, strmsg
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
    vitem = "         " + vitem
    strSQL = "select  t$item, t$cwar, t$clot from baan.tticol118" & session("env") & " " & _
    " where t$item = '" + vitem + "'" & _
    " and t$cwar  = '" + vwarehouse + "'" & _
    " and t$clot  = '" + vlot + "'"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If Not objrs.EOF Then
        session("total") = vcant
        If (session("total") <= session("stock")) then
            strSQL = "update baan.tticol118" & session("env") & " " & _
            " set t$qtyr = t$qtyr + '" + convert.tostring(vcant) +"'" & _
            " where t$item = '" + vitem + "'" & _
            " and t$cwar  = '" + vwarehouse + "'" & _
            " and t$clot  = '" + vlot + "'"
            objrs = Server.CreateObject("ADODB.recordset")
            objrs.Open(strSQL, Odbcon)
            if (vdisp = 4) then
                Response.Redirect ("whInvLabelMaterialRejectedD.aspx?flag=")
            else
                Session("strmsg") = "Material Rejection Inside Warehouse was updated succesfully."
                Response.Redirect ("whInvMaterialRejectedD.aspx?flag=")
            end if
        Else
            Session("strmsg") = "Quantity exceed the stock."
            Response.Redirect ("whInvMaterialRejectedD.aspx?flag=")
        End if
    Else
        strSQL = "insert into baan.tticol118" & session("env") & " " & _
        "values('"+ vitem +"' ,'"+ vwarehouse +"', '"+ vlot +"' , '"+ convert.tostring(vcant) +"', '"+ vreason +"' , substr('" + vcomm + "', 1, 255), '"+ Session("user") +"' , sysdate+5/24, '"+ vdisp +"', '"+ vwarehd +"', '"+ vitemd +"', 2, ' ', '"+ vsupp +"', 0 ,0)"
        objrs = Server.CreateObject("ADODB.recordset")
        objrs.Open(strSQL, Odbcon)      
    End If
    
    if (vdisp = "4") then
        session("mlot") = trim(vmlot)
        session("item") = trim(vitem)
        session("ware") = trim(vwarehouse)
        session("lot") = trim(vlot)
        Response.Redirect ("whInvLabelMaterialRejectedD.aspx?flag=")         	
    else
        Session("strmsg") = "Material Rejection Inside Warehouse was updated succesfully."
        Response.Redirect ("whInvMaterialRejectedD.aspx?flag=")
    end if
%>
<!-- #include file="include/dbxconoff.inc" -->
</body>
</html>

