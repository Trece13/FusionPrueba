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
            height: 28px;
            width: 268px;
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
<form name="frmord" style="width: 5.8in; height: 3.8in">
<%
Dim strord, strqty, strsec, strrowcolor, strSQL, strSQL1, objrs, objrs1, Odbcon, stritem, strdesci, struni, strmaq, strfecha, strqtyplt, strqtyord
Dim strqtyordp, strqtyord1, strqtyplt1, strqtyanu, entero, entero1
Dim strmsg, strflag, sec, sec1, weight, relleno, cantidada, sec2, sec3, sec4, sec5, sec6, sec7, sec8, sec9, strdel

Session("url") = "1"
strflag = Request.QueryString("flag")
if strflag = "Y" then
	strord = ucase(trim(Request.Form("txtorden")))
    strqty = ucase(trim(Request.Form("txtqty")))
	strmsg = Session("strmsg")
else
	strord = ucase(trim(Request.Form("txtorden")))
    strqty = ucase(trim(Request.Form("txtqty")))
	Session("strord")  = strord 
end if
	'Conect to database and execute sp
%>
<!-- #include file="include/dbxcononusa.inc" -->
<%
strord = mid(strord,1,9)

strSQL = "select rtrim(sfc001.t$pdno) orden, trim(sfc001.t$mitm) item, trim(ibd001.t$dsca) desci, trim(ibd001.t$cuni) unidad, trim(sfc010.t$mcno) maquina, ibd003.t$conv Qtyplt, sfc001.t$qrdr QtyOrd, sfc001.t$qdlv QtyAnu from baan.ttisfc001" & session("env") & " " & _
" sfc001 inner join baan.ttcibd001" & session("env") & " ibd001 on ibd001.t$item = sfc001.t$mitm inner join baan.ttisfc010" & session("env") & " sfc010 on sfc010.t$pdno = sfc001.t$pdno " & _
"left join baan.ttcibd003" & Session("env") & " ibd003 on ibd003.t$basu = ibd001.t$cuni and ibd003.t$item = ibd001.t$item and ibd003.t$unit = 'PLT'" & _
" where sfc001.t$osta in('5','7','9') and sfc001.t$pdno='" + strord + "'"
objrs=Server.CreateObject("ADODB.recordset")
objrs.Open (strSQL, Odbcon)
If Not objrs.EOF Then
    stritem = objrs.Fields("item").Value
    strdesci = objrs.Fields("desci").Value
    struni = objrs.Fields("unidad").Value
    strmaq = objrs.Fields("maquina").Value
    strqtyplt = objrs.Fields("Qtyplt").Value
    if (isdbnull(strqtyplt) or strqtyplt = "0") then
        strqtyplt = 1
    end if
    strqtyord = objrs.Fields("Qtyord").Value
    strqtyord1 = objrs.Fields("Qtyord").Value
    strqtyanu = objrs.Fields("QtyAnu").Value
    strqtyord = (strqtyord * 1.02) - strqtyanu
    strqtyordp = (strqtyord1 * 1.02) / strqtyplt
    entero = int(strqtyordp)
    if strqtyordp > entero then 
        strqtyordp = entero + 1
    end if

    if (strqty > strqtyplt) then
        Session("strmsg") = "Amount entered should not be greater than the amount of the full pallet. Please Check1."
	    Response.Redirect("whInvLabelPalletTagsPartial.aspx?flag=") 
    end if
    if (isdbnull(strqtyord) or strqtyord = "0") then
        strqtyord = 1
    end if

    if (strqty >= strqtyord) then
        Session("strmsg") = "Quantity must be less than " & Int(strqtyord) & "."
	    Response.Redirect("whInvLabelPalletTagsPartial.aspx?flag=")     
    end if
    
    'strSQL1 = "select count(t$pdno) cantidad1 from baan.tticol022" & Session("env") & " where t$dele = 2 and t$pdno = '" + strord + "'"
    strSQL1 = "select count(t$pdno) cantidad1 from baan.tticol022" & Session("env") & " where t$pdno = '" + strord + "'"
        objrs1=Server.CreateObject("ADODB.recordset")
        objrs1.Open (strSQL1, Odbcon)
        If  Not objrs1.EOF Then
            sec = objrs1.Fields("cantidad1").Value
        End If

    strSQL = "select t$sqnb secuence, sysdate fecha, t$dele borrado from baan.tticol022" & Session("env") & " where t$pdno = '" + strord + "'" & _
        " and t$sqnb = ( select max(t$sqnb) from baan.tticol022" & Session("env") & " where t$pdno = '" + strord + "' )"
    objrs=Server.CreateObject("ADODB.recordset")
    objrs.Open (strSQL, Odbcon)
    If (objrs.EOF) Then
        sec1 = "001"
        sec8 = 1
        sec = strord+"-"+"001"
        session("sec") = sec
        
        strSQL = "select sysdate fecha from dual"
        objrs=Server.CreateObject("ADODB.recordset")
        objrs.Open (strSQL, Odbcon)
        if not objrs.EOF Then
            strfecha = objrs.Fields("fecha").Value
        end if
        
            strSQL1 = "insert into baan.tticol022" & Session("env") & " (t$pdno,t$sqnb,t$proc,t$logn, t$date,t$mitm, t$qtdl,t$cuni,t$pro1, t$pro2, t$log1, t$datc, t$qtd1, t$log2, t$datu, t$qtd2, t$loca, t$norp, t$dlrp, t$dele, t$logd, t$datd, t$refcntd,t$refcntu) " & _
		    " values('" + strord + "','" + sec + "',2,'" + Session("user") + "',sysdate+5/24,'" + "         "+stritem + "', 0,'" + struni + "', 2, 2, 'NONE',sysdate+5/24,0, 'NONE', sysdate+5/24, 0, ' ', 1, sysdate+5/24,2,'NONE',sysdate+5/24,0,0)" 
    	    objrs=Server.CreateObject("ADODB.recordset")
	    	objrs.Open (strSQL1, Odbcon)
    Else
        'sec = mid(objrs.Fields("secuence").Value,11,3)
        strdel = objrs.Fields("borrado").Value
        sec = sec + 1
        sec1 = sec
        strfecha = objrs.Fields("fecha").Value

        If sec > strqtyordp then
        entero1 = Int(strqtyord)
            Session("strmsg") = "Number of Cases cannot exceed (" + formatnumber(entero1,0) + "). Please contact the Supervisor."
	        Response.Redirect("whInvLabelPalletTagsPartial.aspx?flag=Y")  
        Else
            If sec < 10 then
                relleno = "00"
                sec = relleno+convert.tostring(sec)
                sec3 = val(sec)
                sec2 = sec3 - 1
                if sec2 = 99 then
                    relleno = "0"
                end if
                sec2 = relleno+convert.tostring(sec2)
                sec4 = strord+"-"+convert.tostring(sec2)
                sec8 = sec2
            Else
                if sec > 99 then
                    relleno = ""
                else
                    relleno = "0"
                end if
                sec = relleno+convert.tostring(sec)
                sec3 = val(sec)
                sec2 = sec3 - 1
                if sec2 = 99 then
                    relleno = "0"
                end if
                if sec2 = 9 then
                    relleno = "00"
                end if
                sec2 = relleno+convert.tostring(sec2)
                sec4 = strord+"-"+convert.tostring(sec2)
                sec8 = sec2
            End if
            sec =  strord+"-"+convert.tostring(sec)

            'Validar que el anterior pallet este anunciado
            strSQL1 = "select t$qtdl cantidad from baan.tticol022" & Session("env") & " " & _
            "where t$pdno = '" + strord + "' " & _
            "and t$sqnb = '" + sec4 + "'" 
       	    objrs=Server.CreateObject("ADODB.recordset")
	    	objrs.Open (strSQL1, Odbcon)
            If Not objrs.EOF Then
                cantidada = objrs.Fields("cantidad").Value
                if cantidada = 0 then
                    strSQL = "select t$sqnb secuence, sysdate fecha, t$dele borrado from baan.tticol022" & Session("env") & " where t$pdno = '" + strord + "' and t$qtdl = 0" & _
                        " and t$sqnb = ( select max(t$sqnb) from baan.tticol022" & Session("env") & " where t$pdno = '" + strord + "' )"
                    objrs=Server.CreateObject("ADODB.recordset")
                    objrs.Open (strSQL, Odbcon)
                    If (isdbnull(objrs.Fields("secuence").Value)) Then
                        sec = "001"
                        session("sec") = strord+"-"+convert.tostring(sec)
                    Else
                        strdel = objrs.Fields("borrado").Value
                        sec5 = mid(objrs.Fields("secuence").Value,11,3)
                        sec6 = val(sec5)
                        If sec6 < 10 then
                            relleno = "00"
                        Else
                            if sec6 > 99 then
                                relleno = ""
                            else
                                relleno = "0"
                            end if
                        End if
                        sec7 = relleno+convert.tostring(sec6)                            
                        sec8 = sec6
                        sec9 = mid(sec,11,3)
                        if (sec9 <> sec7) then
                            sec = strord+"-"+relleno+convert.tostring(sec8)
                        end if
                        session("sec") =  sec
                        Session("strmsg") = "Previous palette has not been announced. Please announce the pallet first."
    	                Response.Redirect("whInvLabelPalletTagsPartial.aspx?flag=Y") 
                    End If
                else                    
                    strSQL = "select t$sqnb secuence, sysdate fecha, t$dele borrado from baan.tticol022" & Session("env") & " where t$pdno = '" + strord + "' and t$qtdl <> 0" & _
                        " and t$sqnb = ( select max(t$sqnb) from baan.tticol022" & Session("env") & " where t$pdno = '" + strord + "' )"
                    objrs=Server.CreateObject("ADODB.recordset")
                    objrs.Open (strSQL, Odbcon)
                    If (objrs.EOF) Then
                        sec = 1
                        'sec8 = relleno+convert.tostring(sec)
                        'session("sec") = strord+"'"+sec8
                    Else
                        sec5 = mid(objrs.Fields("secuence").Value,11,3)
                        If sec5 = "001" then
                            sec6 = "2"
                        Else
                            sec6 = val(sec5) + 1
                        End If
                        If sec6 < 10 then
                            relleno = "00"
                        Else
                            if sec6 > 99 then
                                relleno = ""
                            else
                                relleno = "0"
                            end if
                        End if
                        sec7 = relleno+convert.tostring(sec6)                            
                        sec8 = sec6
                        sec9 = mid(sec,11,3)
                        if (sec9 <> sec7) then
                            sec = strord+"-"+relleno+convert.tostring(sec8)
                        end if
                        session("sec") =  sec
                    End If
                    
                    strSQL1 = "insert into baan.tticol022" & Session("env") & " (t$pdno,t$sqnb,t$proc,t$logn, t$date,t$mitm, t$qtdl,t$cuni,t$pro1, t$pro2, t$log1, t$datc, t$qtd1, t$log2, t$datu, t$qtd2, t$loca, t$norp, t$dlrp, t$dele, t$logd, t$datd, t$refcntd,t$refcntu) " & _
		    	    " values('" + strord + "','" + session("sec") + "',2,'" + Session("user") + "',sysdate+5/24,'" + "         "+stritem + "', 0,'" + struni + "', 2, 2, 'NONE',sysdate+5/24,0, 'NONE', sysdate+5/24, 0, ' ', 1,sysdate+5/24,2, 'NONE',sysdate+5/24,0,0)" 
    	            objrs=Server.CreateObject("ADODB.recordset")
	    	        objrs.Open (strSQL1, Odbcon)
                end if               
            End if
        End if
    End if
    'session("sec") =  sec
    session("qty") =  strqty
    'Desconectar a base de datos
%>
<table align="left" style="width: 5.8in; height: 3.8in" border="1" cellspacing="0" cellpadding="0">
    <!--
<tr><td colspan="2" align="center"><IMG SRC = "images/logophoenix_s.jpg" hspace="10" vspace="10" ></td></tr>
-->
<tr>
    <td colspan="2"        
        style="font-size: medium; color: #000000; font-family: Arial; width: 5in;" 
        align="center" ><%=strdesci%>
    </td>
    <td        
        style="font-size: x-small; color: #000000; font-family: Arial; width: 5in;" 
        align="center" >MADE IN: DUBLIN, VA
    </td>
</tr>
<tr>
    <td colspan="3">       
        <img src='/Barcode/Barcodehandler.ashx?data=<%=stritem%>&code=Code128&dpi=96' 
            alt='Barcode Generator TEC-IT' hspace="80" vspace="5" 
            style="width: 4in; height: .5in;"/>
    </td>
</tr>
<tr>
    <td colspan="3"        
        style="font-size: medium; color: #000000; font-family: Arial; width: 5in;" 
        align="center" ><%=stritem%>
    </td>    
</tr>
<tr>
    <td colspan="3">       
        <img src='/Barcode/Barcodehandler.ashx?data=<%=sec%>&code=Code128&dpi=96' 
            alt='Barcode Generator TEC-IT' hspace="80" vspace="5" 
            style="width: 4in; height: .5in;"/></td>
</tr> 
<tr>  
    <td style="font-size: small; color: #000000;" align="center">Work Order Lot</td> 
    <td style="font-size: small; color: #000000;" align="center">Pallet Number</td>  
    <td style="font-size: small; color: #000000;" align="center">Inspector Initial</td>  
</tr>
<tr>  
    <td style="font-size: large; color: #000000;" align="center"><%=strord%></td> 
    <td style="font-size: large; color: #000000;" align="center"><%=sec8%></td> 
    <td style="font-size: large; color: #000000;" align="center"></td>    
</tr>
<tr>  
    <td style="font-size: small; color: #000000;" align="center">Date</td> 
    <td style="font-size: small; color: #000000;" align="center">Shift</td>  
    <td style="font-size: small; color: #000000;" align="center">Case Per Pallet</td>  
</tr>
<tr>  
<td style="font-size: small; color: #000000;" align="center" class="style1">
    <asp:Label ID="date" runat="server" Text="Date" Font-Bold="True" Font-Names="Arial" Font-Size="small" ForeColor="Black"></asp:Label></td>
    <td style="font-size: medium; color: #000000;" align="center">A, B, C, D</td>
    <td style="font-size: large; color: #000000;" align="center"><%=strqty%></td>
</tr>
<!-- #include file="include/dbxconoff.inc" -->
<%
else
    Session("strmsg") = "Order doesn't exist or the status is not active, release or completed."
	Response.Redirect("whInvLabelPalletTagsPartial.aspx?flag=") 
End if
%>
</table>
</form>
</div>
<div>
<table align="left" style="width: 5.8in; height: .25in" border="1" cellspacing="0" cellpadding="0">
<tr>
<td>
<!--
<a href="javascript:window.print()" style="color: #000000">Print</a>
-->
<a href="#" onclick="javascript:printDiv('label')" style="color: #000000; font-size: medium">Print</a>
</td>
<td><a href="whanuncioordq.aspx?flag=Y"><img src="images/btn_Exit.jpg"></a>
</td>
</tr>
</table>
</div>
</body>
</html>
