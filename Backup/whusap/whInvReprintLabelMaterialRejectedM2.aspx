<%@ Page AspCompat="true" Debug="true" %>

<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must authenticate before you can execute this option."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>
<html>
<head>
    <link href="../basic.css" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=300, user-scalable=no" />
    <title></title>
    <style type="text/css">
        * {
            color: #7F7F7F;
            font-family: Arial,sans-serif;
            font-size: 12px;
            font-weight: 700;
            height: 23px;
            width: 161px;
            margin-left: 0px;
        }
        #config
        {
            overflow: auto;
            margin-bottom: 10px;
        }
        .config
        {
            float: left;
            width: 200px;
            height: 250px;
            border: 1px solid #000;
            margin-left: 10px;
        }
        .config .title
        {
            font-weight: bold;
            text-align: center;
        }
        .config .barcode2D, #miscCanvas
        {
            display: none;
        }
        #submit
        {
            clear: both;
        }
        #barcodeTarget, #canvasTarget
        {
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
        };

        function addZero(i) {
            if (i < 10) {
                i = "0" + i;
            }
            return i;
        };
    </script>
</head>
<body bgcolor="#87CEEB">
    <div id="label">
        <form id="form1" runat="server" style="width: 5.8in; height: 3.8in">
<%
Dim strord, strlote, strseqn, strflag, sSql, objrs, Odbcon, strmsg 
Dim strpono, stritem, strobse, struser, strfecha, strqty, strmcno, strmess, strdsca, strunidad

Session("url") = "1"
strflag = Request.QueryString("flag")
If strflag = "Y" then
    strord = ucase(trim(Request.Form("orden")))
    strlote = ucase(trim(Request.Form("lot")))
    strseqn = ucase(trim(Request.Form("seq")))
   	strmsg = Session("strmsg")
Else
    strord = ucase(trim(Request.Form("orden")))
    strlote = ucase(trim(Request.Form("lot")))
    strseqn = ucase(trim(Request.Form("seq")))
	Session("strord")  = strord 
End If
	'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
strord = mid(strord,1,9)

If (strlote="") Then
    sSql = "SELECT col101.t$pdno AS orden, col101.t$pono AS Posicion, col101.t$seqn AS Secuencia, trim(col101.t$item) AS Item," & _
        "       col101.t$obse AS Observation, col101.t$clot AS Lote, col101.t$logr AS Usuario, col101.t$date AS Fecha," & _
        "       col101.t$qtyr AS Cantidad, col101.t$mcno AS Machine, col101.t$mess AS Message, art.t$dsca Description, trim(ibd001.t$cuni) AS Unidad " & _
        "FROM baan.tticol101" & session("env") & " col101 " & _
        "INNER JOIN baan.ttcibd001" & session("env") & " art ON trim(art.t$item) = trim(col101.t$item) " & _
        "INNER JOIN baan.ttcibd001" & session("env") & " ibd001 ON trim(ibd001.t$item) = trim(col101.t$item) " & _
        "WHERE trim(col101.t$pdno) = '" & strord & "' and" & _
        "      col101.t$seqn = " & strseqn
Else
    sSql = "SELECT col101.t$pdno AS orden, col101.t$pono AS Posicion, col101.t$seqn AS Secuencia, trim(col101.t$item) AS Item," & _
        "       col101.t$obse AS Observation, col101.t$clot AS Lote, col101.t$logr AS Usuario, col101.t$date AS Fecha," & _
        "       col101.t$qtyr AS Cantidad, col101.t$mcno AS Machine, col101.t$mess AS Message, art.t$dsca Description, trim(ibd001.t$cuni) AS Unidad " & _
        "FROM baan.tticol101" & session("env") & " col101 " & _
        "INNER JOIN baan.ttcibd001" & session("env") & " art ON trim(art.t$item) = trim(col101.t$item) " & _
        "INNER JOIN baan.ttcibd001" & session("env") & " ibd001 ON trim(ibd001.t$item) = trim(col101.t$item) " & _
        "WHERE trim(col101.t$pdno) = '" & strord & "' and" & _
        "      trim(col101.t$clot) = '" & strlote & "' and" & _
        "      col101.t$seqn = " & strseqn
End If
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (sSql, Odbcon)
If Not objrs.EOF Then
    strpono = objrs.Fields("Posicion").Value
    stritem = objrs.Fields("Item").Value
    strobse = objrs.Fields("Observation").Value
    struser = objrs.Fields("Usuario").Value
    strfecha = objrs.Fields("Fecha").Value
    strqty = objrs.Fields("Cantidad").Value
    strmcno = objrs.Fields("Machine").Value
    strmess = objrs.Fields("Message").Value
    strdsca = objrs.Fields("Description").Value
    strunidad = objrs.Fields("Unidad").Value
    if (strlote = "") then
        strlote = " "
    end if
    Session("strmsg") = "Order was printed successfully."
%>
            <table align="left" style="width: 5.8in; height: 3.8in" border="1" cellspacing="0" cellpadding="0">
                <tr>
                    <td style="font-size: small; color: #000000" align="center" class="style3">DMT - NUMBER</td>
                    <td style="font-size: small; color: #000000" align="center" class="style3"><%=strord%>-<%=strpono%>-<%=strseqn%></td>
                    <td>
                        <img align="middle" alt="Barcode Generator TEC-IT" hspace="0" 
                            src="/Barcode/BarcodeHandler.ashx?data=<%=strord%>-<%=strpono%>-<%=strseqn%>&amp;code=Code128&amp;dpi=96" 
                            style="width: 2in; height: .5in; position: relative; top: 0px; left: 10px;" 
                            vspace="0" /> </td>
                </tr>
                <tr>  
                    <td colspan="3">
                        <img align="middle" alt="Barcode Generator TEC-IT" hspace="0" 
                            src="/Barcode/BarcodeHandler.ashx?data=<%=stritem%>&amp;code=Code128&amp;dpi=96" 
                            style="width: 4in; height: .5in; position: relative; top: 0px; left: 64px;" 
                            vspace="0" /> </td>
                </tr>
                <tr>  
                    <td style="font-size: small; color: #000000;" align="center" class="style4">Description</td> 
                    <td colspan="2" style="font-size: small; color: #000000;" class="style4"><%=strdsca%></td>  
                </tr>
                <tr>  
                    <td rowspan="2"style="font-size: small; color: #000000;" align="center"> 
                        <div>
                            <span style="font-size: small; color: #000000;">Lot</span>
                        </div>
                        <div>
                            <img align="middle" alt="Barcode Generator TEC-IT" hspace="0" 
                                    src="/Barcode/BarcodeHandler.ashx?data=<%=strlote%>&amp;code=Code128&amp;dpi=96" 
                                    style="width: 1in; height: .5in; position: relative; top: 0px; left:10px;" 
                                    vspace="0" />
                        </div>
                    </td>
                    <td style="font-size: small; color: #000000;" align="center" class="style2">Rejected Qty - <%=strqty%>-<%=strunidad%></td> 
                    <td>
                        <img align="middle" alt="Barcode Generator TEC-IT" hspace="0" 
                            src="/Barcode/BarcodeHandler.ashx?data=<%=strqty%>&amp;code=Code128&amp;dpi=96" 
                            style="width: 2in; height: .5in; position: relative; top: 0px; left:10px;" 
                            vspace="0" /> </td>
                </tr>
                <tr>
                <%--<td style="font-size: small; color: #000000;" align="center" class="style2">Disposition - <%=strdispo%></td>--%>
                    <td style="font-size: small; color: #000000;" align="center" class="style2">WorkOrder - <%=strord%></td> 
                    <td>
                        <img align="middle" alt="Barcode Generator TEC-IT" hspace="0" 
                            src="/Barcode/BarcodeHandler.ashx?data=<%=strord%>&amp;code=Code128&amp;dpi=96" 
                            style="width: 2in; height: .5in; position: relative; top: 0px; left:10px;" 
                            vspace="0" /> </td>
                </tr>
                <tr>
                    <td style="font-size: small; color: #000000;" align="center" class="style5">Printed By - <%=session("user")%></td>
                    <td style="font-size: small; color: #000000;" align="center" class="style5">
                        <asp:Label ID="date" runat="server" Text="Date" Font-Bold="True" Font-Names="Arial" Font-Size="small" ForeColor="Black">
                        </asp:Label> - <%= strfecha %>
                    </td>
                    <td style="font-size: small; color: #000000;" align="center" class="style2">Machine - <%=strmcno%></td>
                </tr>
                <tr>  
                    <td style="font-size: small; color: #000000;" align="center" class="style6">Reason</td> 
                    <td colspan="2" style="font-size: small; color: #000000;" align="center" class="style6"><%=strmess%></td> 
                </tr>
                <tr>  
                    <td colspan="3" rowspan="2" style="font-size: small; color: #000000;" class="style7">Comments - <%=strobse%></td>
                </tr>
            </table>
<!-- #include file="include/dbxconoff.inc" -->
<%
Else
    Session("strmsg") = "Order wasn't printed successfully."
    'Session("strmsg") = sSql
End If
%>
        </form>
    </div>
    <div>
        <table align="left" style="width: 5.8in; height: .25in" border="1" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <a href="#" onclick="javascript:printDiv('label')" style="color: #000000; font-size: medium">Print</a>
                </td>
                <td>
                    <a href="whInvReprintLabelMaterialRejectedM.aspx"><img alt="Exit" 
                        src="images/btn_Exit.jpg" style="margin-bottom: 0px" /></a>
                </td>
            </tr>
        </table>
    </div>
</body>
</html>