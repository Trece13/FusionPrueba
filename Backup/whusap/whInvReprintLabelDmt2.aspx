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
            height: 23px;
            width: 239px;
            margin-left: 0px;
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
        .style1
        {
            height: 5px;
        }
        .style2
        {
            height: 14px;
        }
        .style3
        {
            width: 80px;
        }
        .style4
        {
            height: 14px;
            width: 80px;
        }
    </style>

<script type="text/javascript">
    function printDiv(divID) {
        //PRINT LOCAL HOUR
        var d = new Date();
        var x = document.getElementById("date");
        var h = addZero(d.getHours());
        var m = addZero(d.getMinutes());
        var s = addZero(d.getSeconds());
        //x.innerHTML = d.toUTCString();
        x.innerHTML = d.toLocaleString();

        //Get the HTML of div
        var divElements = document.getElementById(divID).innerHTML;
        //Get the HTML of whole page
        var oldPage = document.body.innerHTML;
        //Reset the page's HTML with div's HTML only
        document.body.innerHTML = "<html><head><title></title></head><body>" + divElements + "</body>";
        //Print Page
        window.print();
        //Restore orignal HTML
        document.body.innerHTML = oldPage;
    }

function addZero(i) {
        if (i < 10) {
            i = "0" + i;
        }
        return i;
};
</script>

</head>
<meta name="viewport" content="width=300, user-scalable=no">
<body bgcolor="#87CEEB">
<div id="label">
<form id="form1" runat="server" style="width: 5.8in; height: 3.8in">
<%
Dim strord, strsec, strrowcolor, strSQL, strSQL1, objrs, Odbcon, stritem, strdesci, struni, strfecha
Dim strmsg, strflag, strqty, strart, strobse, strreason, strcant, strmaquina, strdescr

Session("url") = "1"
strflag = Request.QueryString("flag")
if strflag = "Y" then
    strord = ucase(trim(Request.Form("orden")))
    strsec = ucase(trim(Request.Form("seq")))
   	strmsg = Session("strmsg")
else
    strord = ucase(trim(Request.Form("orden")))
    strsec = ucase(trim(Request.Form("seq")))
	Session("strord")  = strord 
end if
	'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
strord = mid(strord,1,9)

strSQL = "select rtrim(col100.t$pdno) orden, trim(col100.t$item) item, trim(ibd001.t$dsca) desci, trim(ibd001.t$cuni) unidad, col100.t$qana cantidad, " & _ 
" col100.t$seqn secu, sfc010.t$mcno maquina, mcs005.t$dsca descr, col100.t$date fecha, col100.t$obse obse from baan.tticol100" & session("env") & " col100 " & _
" inner join baan.ttcibd001" & session("env") & " ibd001 on ibd001.t$item = col100.t$item " & _
" inner join baan.ttcmcs005" & session("env") & " mcs005 on mcs005.t$cdis = col100.t$cdis " & _
" inner join baan.ttisfc010" & session("env") & " sfc010 on sfc010.t$pdno = col100.t$pdno and sfc010.t$item = col100.t$item and sfc010.t$opno = 10 " & _
" where col100.t$pono = 0 and col100.t$pdno='" + strord + "'" & _
" and col100.t$seqn ='" + Convert.tostring(strsec) + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
    stritem = objrs.Fields("item").Value
    strdesci = objrs.Fields("desci").Value
    struni = objrs.Fields("unidad").Value
    strqty = objrs.Fields("cantidad").Value
    strmaquina = objrs.Fields("maquina").Value
    strdescr = objrs.Fields("descr").Value
    strfecha = objrs.Fields("fecha").Value
    strobse = objrs.Fields("obse").Value
    Session("strmsg") = "Order was printed successfully."
%>
<table align="left" style="width: 5.8in; height: 3.8in" border="1" cellspacing="0" cellpadding="0">
<tr>
    <td colspan="2" style="font-size: small; color: #000000" align="center" class="style1">DEFECTIVE MATERIAL TAG</td>
    <td style="font-size: small; color: #000000;" align="center" class="style1">DMT NUMBER</td>
</tr>
<tr>  
    <td colspan="2" style="font-size: small; color: #000000;" align="center" class="style1">[ ] IN HOUSE REJECTION</td>
    <td style="font-size: small; color: #000000;" align="center" class="style10"><%=strord%>-0-<%=strsec%></td> 
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center" class="style1">Last Oper Com.</td> 
    <td style="font-size: small; color: #000000;" align="center" class="style1">Product Code</td>  
    <td style="font-size: small; color: #000000;" align="center" class="style1">Total Qty Rejected</td> 
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center" class="style4"><%=strmaquina%></td> 
    <td style="font-size: x-small; color: #000000;" align="center" class="style2"><%=stritem%></td> 
    <td style="font-size: small; color: #000000;" align="center" class="style10"><%=strqty%></td> 
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center" class="style1">Workorder</td>
    <td style="font-size: small; color: #000000;" align="center" class="style1">Product Description</td> 
    <td style="font-size: small; color: #000000;" align="center" class="style1">Date</td> 
</tr>
<tr>

    <td style="font-size: small; color: #000000;" align="center" class="style1"><%=strord%></td>
    <td style="font-size: small; color: #000000;" align="center" class="style1"><%=strdesci%></td>  
    <td style="font-size: small; color: #000000;" align="center" class="style1">
    <asp:Label ID="Label1" runat="server" Text="Date" Font-Bold="True" Font-Names="Arial" Font-Size="x-small" ForeColor="Black"></asp:Label></td>
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center" class="style4">Printed By</td> 
    <td style="font-size: small; color: #000000;" align="center" class="style5">Reason</td> 
    <td style="font-size: small; color: #000000;" align="center" class="style5"><%=strdescr%></td> 
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center" class="style2"><%=session("user")%></td> 
    <td colspan="2" rowspan="2" style="font-size: small; color: #000000;"><%=strobse%></td>
</tr>
<tr> 
    <td class="style2">
    </td>
</tr>
</table>
<!-- #include file="include/dbxconoff.inc" -->
<%
Else
    strSQL = "select rtrim(col101.t$pdno) orden, trim(col101.t$item) item, trim(ibd001.t$dsca) desci, trim(ibd001.t$cuni) unidad, col101.t$qana cantidad, " & _ 
    " col101.t$seqn secu, sfc010.t$mcno maquina, mcs005.t$dsca descr, col101.t$date fecha, col101.t$obse obse from baan.tticol101" & session("env") & " col101 " & _
    " inner join baan.ttcibd001" & session("env") & " ibd001 on ibd001.t$item = col101.t$item " & _
    " inner join baan.ttcmcs005" & session("env") & " mcs005 on mcs005.t$cdis = col101.t$cdis " & _
    " inner join baan.ttisfc010" & session("env") & " sfc010 on sfc010.t$pdno = col101.t$pdno and sfc010.t$item = col101.t$item and sfc010.t$opno = 10 " & _
    " where col101.t$pono = 0 and col101.t$pdno='" + strord + "'" & _
    " and col101.t$seqn ='" + Convert.tostring(strsec) + "'"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If Not objrs.EOF Then
        stritem = objrs.Fields("item").Value
        strdesci = objrs.Fields("desci").Value
        struni = objrs.Fields("unidad").Value
        strqty = objrs.Fields("cantidad").Value
        strmaquina = objrs.Fields("maquina").Value
        strdescr = objrs.Fields("descr").Value
        strfecha = objrs.Fields("fecha").Value
        strobse = objrs.Fields("obse").Value
        Session("strmsg") = "Order was printed successfully."
        'Desconectar a base de datos
%>
<table align="left" style="width: 5.8in; height: 3.8in" border="1" cellspacing="0" cellpadding="0">
<tr>
    <td colspan="2" style="font-size: small; color: #000000" align="center" class="style1">DEFECTIVE MATERIAL TAG</td>
    <td style="font-size: small; color: #000000;" align="center" class="style1">DMT NUMBER</td>
</tr>
<tr>  
    <td colspan="2" style="font-size: small; color: #000000;" align="center" class="style1">[ ] IN HOUSE REJECTION</td>
    <td style="font-size: small; color: #000000;" align="center" class="style10"><%=strord%>-0-<%=strsec%></td> 
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center" class="style1">Last Oper Com.</td> 
    <td style="font-size: small; color: #000000;" align="center" class="style1">Product Code</td>  
    <td style="font-size: small; color: #000000;" align="center" class="style1">Total Qty Rejected</td> 
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center" class="style4"><%=strmaquina%></td> 
    <td style="font-size: x-small; color: #000000;" align="center" class="style2"><%=stritem%></td> 
    <td style="font-size: small; color: #000000;" align="center" class="style10"><%=strqty%></td> 
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center" class="style1">Workorder</td>
    <td style="font-size: small; color: #000000;" align="center" class="style1">Product Description</td> 
    <td style="font-size: small; color: #000000;" align="center" class="style1">Date</td> 
</tr>
<tr>

    <td style="font-size: small; color: #000000;" align="center" class="style1"><%=strord%></td>
    <td style="font-size: small; color: #000000;" align="center" class="style1"><%=strdesci%></td>  
    <td style="font-size: small; color: #000000;" align="center" class="style1">
    <asp:Label ID="date" runat="server" Text="Date" Font-Bold="True" Font-Names="Arial" Font-Size="x-small" ForeColor="Black"></asp:Label></td>
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center" class="style4">Printed By</td> 
    <td style="font-size: small; color: #000000;" align="center" class="style5">Reason</td> 
    <td style="font-size: small; color: #000000;" align="center" class="style5"><%=strdescr%></td> 
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center" class="style2"><%=session("user")%></td> 
    <td colspan="2" rowspan="2" style="font-size: small; color: #000000;"><%=strobse%></td>
</tr>
<tr> 
    <td class="style2">
    </td>
</tr>
<!-- #include file="include/dbxconoff.inc" -->
<%
    Else
        Session("strmsg") = "Order doesn't register finish product to MRB."
	    Response.Redirect("whInvReprintLabelDmt.aspx?flag=") 
    End If
End If
%>
</table>
</form>
</div>
<div>
<table align="left" style="width: 5.8in; height: .25in" border="1" cellspacing="0" cellpadding="0">
<tr>
    <td><a href="#" onclick="javascript:printDiv('label')" style="color: #000000; font-size: medium">Print</a></td>
    <td><a href="whInvReprintLabelDmt.aspx"><img src="images/btn_Exit.jpg"/></a></td>
</tr>
</table>
</div>
</body>
</html>
