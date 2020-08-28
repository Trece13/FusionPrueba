<%
if	Session("logok") <> "OKYes" then
	Session("Message")= "You must login first, before use this session."
	Response.Redirect ("whlogini.aspx?flg=Y")
End if
%>
<HTML>
<link href="../basic.css" rel="stylesheet" type="text/css">
<HEAD>
  <TITLE>Bodega</TITLE>
    <style type="text/css">
        .style1
        {
            width: 171px;
        }
        .style2
        {
            width: 121px;
        }
        .style3
        {
            height: 31px;
        }
    </style>
</HEAD>
<!--
<meta name="viewport" content="width=600, user-scalable=no">
-->
<body bgcolor="#87CEEB">
<FORM>
<table>
<tr>
<div>
<td class="style2"><IMG SRC = "images/logophoenix_s.jpg" style="width: 116px"></td>
</div>
<td class="style1"><H3>Warehouse Movements</H3></td>
</TR>
<tr>
<td align="left" class="style2" width="70%" ><a href="whLogoffi.aspx"><img src="Images/btn_closesesion.jpg"></a></td>
</TR>
<tr>
<td class="style2">User:</td>
<td class="style1"><b><%=Session("user")%></b></td>
</TR>
</TABLE>
<table>
<tr> 
    <%  If InStr(Session("opciones"), "RECEP") <> 0 Then%>
	    <td align="center" class="style3"><a href="whInvReceipts.aspx?flag=Y"><img src="images/btn_Receipts.jpg"></a></td>
    <% end if %>
    <%  If InStr(Session("opciones"), "UBIC") <> 0 Then%>
	    <td align="center" class="style3"><a href="whInvLocations.aspx?flag=Y"><img src="images/btn_locations.jpg"></a></td>
    <% end if %>
</tr> 
<tr> 
	<% If InStr(Session("opciones"), "SUGER") <> 0 Then%>
	    <td align="center" class="style3"><a href="whInvAdvices.aspx?flag=Y"><img src="images/btn_oadvice.jpg"></a></td>
    <% end if %>
    <% If InStr(Session("opciones"), "RECOL") <> 0 Then%>
	    <td align="center" class="style3"><a href="whInvPicking.aspx?flag=Y"><img src="images/btn_picking.jpg"></a></td>
    <% end if %>
</tr> 
<tr> 
    <% If InStr(Session("opciones"), "CONFR") <> 0 Then%>
	    <td align="center" class="style3"><a href="whInvConfirm.aspx?flag=Y"><img src="images/btn_Confirm.jpg"></a></td>
    <% end if %>
    <% If InStr(Session("opciones"), "TRANSF") <> 0 Then%>
        <td align="center" class="style3"><a href="whInvtransfers.aspx?flag=Y"><img src="images/btn_transfer.jpg"></a></td>
    <% end if %>
</tr>
<tr>
    <% if InStr(Session("opciones"),"UBICA")<>0 then %>
	    <td align="center"><a href="whsugubicacion.aspx"><img src="Images/btn_sugerencia.jpg"></a></td>
	<% end if %>
</tr> 
<tr> 
    <% If InStr(Session("opciones"), "INVFI") <> 0 Then%>
        <td align="center" class="style3"><a href="whInvInventory.aspx?flag=Y"><img src="images/btn_inventoryrm.jpg"></a></td>
    <% end if %>
    <% If InStr(Session("opciones"), "INVFI") <> 0 Then%>
        <td align="center" class="style3"><a href="whInvInventoryfp.aspx?flag=Y"><img src="images/btn_inventory.jpg"></a></td>
    <% end if %>
</tr> 
<tr>
    <% If InStr(Session("opciones"), "INVFI") <> 0 Then%>
        <td align="center" class="style3"><a href="whInvInventoryNoLocations.aspx?flag=Y"><img src="images/btn_inventorynolocations.jpg"></a></td>
    <% end if %>
	<% if InStr(Session("opciones"),"CONFI")<>0 then %>
	    <td align="center"><a href="whconfirmRecep.aspx"><img src="Images/btn_Confirmar.jpg"></a></td> 
	<% end if %>
</tr>
<tr> 
	<% if InStr(Session("opciones"),"ANUNC")<>0 then %>
	    <td align="center"><a href="whanuncioord.aspx"><img src="Images/btn_anuncio.jpg"></a></td> 
	<% end if %>
	<% if InStr(Session("opciones"),"ANUNP")<>0 then %>
	    <td align="center"><a href="whanuncioordq.aspx"><img src="Images/btn_anuncioi.jpg"></a></td> 
	<% end if %>
</tr> 
<tr> 

    <% If InStr(Session("opciones"), "TIME") <> 0 Then%>
        <td align="center" class="style3"><a href="whInvTime.aspx?flag=Y"><img src="images/btn_time.jpg"></a></td>
    <% end if %>
    <% If InStr(Session("opciones"), "TIMEAI") <> 0 Then%>
	    <td align="center"><a href="whInvTimeai.aspx"><img src="Images/btn_timeai.jpg"></a></td>
	<% end if %>
</tr> 
<tr> 
    <% If InStr(Session("opciones"), "SUPPR") <> 0 Then%>
	    <td align="center"><a href="whInvSuppliesRequest.aspx"><img src="Images/btn_suppr.jpg"></a></td>
	<% end if %>
        <% If InStr(Session("opciones"), "MATER") <> 0 Then%>
	    <td align="center"><a href="whInvMaterialRejected.aspx"><img src="Images/btn_Materialrejected.jpg"></a></td>
	<% end if %>
</tr>
<tr>
    <% If InStr(Session("opciones"), "LINEC") <> 0 Then%>
	    <td align="center"><a href="whInvLineClearance.aspx"><img src="Images/btn_linec.jpg"></a></td>
	<% end if %>
    <% If InStr(Session("opciones"), "ITEMR") <> 0 Then%>
	    <td align="center"><a href="whInvItemRejected.aspx"><img src="Images/btn_itemrejected.jpg"></a></td>
	<% end if %>
</tr>
<tr> 
    <% If InStr(Session("opciones"), "ITEMRR") <> 0 Then%>
	    <td align="center"><a href="whInvItemRejectedReview.aspx"><img src="Images/btn_itemrejectedr.jpg"></a></td>
	<% end if %>
    <% If InStr(Session("opciones"), "CLOSEO") <> 0 Then%>
	    <td align="center"><a href="whInvCloseOrders.aspx"><img src="Images/btn_closeorder.jpg"></a></td>
	<% end if %>
</tr> 
<tr> 
    <% If InStr(Session("opciones"), "START") <> 0 Then%>
	    <td align="center"><a href="whInvStartWorkorders.aspx"><img src="Images/btn_startfinish.jpg"></a></td>
	<% end if %>
    <% If InStr(Session("opciones"), "CHANGE") <> 0 Then%>
	    <td align="center"><a href="whInvChangeMaterial.aspx"><img src="Images/btn_changematerial.jpg"></a></td>
	<% end if %>
</tr> 
<tr>
    <% If InStr(Session("opciones"), "COMPC") <> 0 Then%>
        <td align="center" class="style3"><a href="whInvCosts.aspx?flag=Y"><img src="images/btn_costs.jpg"></a></td>
    <% end if %>
    <% If InStr(Session("opciones"), "LABELS") <> 0 Then%>
        <td align="center"><a href="whInvlabel.aspx"><img src="Images/btn_printtag.jpg"></a></td>
	<% end if %>
</tr>
<tr>
    <% If InStr(Session("opciones"), "LABELR") <> 0 Then%>
	    <td align="center"><a href="whInvLabelRegrind.aspx"><img src="Images/btn_printlabels.jpg"></a></td>
	<% end if %>
    <% if InStr(Session("opciones"),"LABELP")<> 0 then %>
	    <td align="center"><a href="whInvLabelPalletTags.aspx"><img src="Images/btn_printpallettag.jpg"></a></td>
	<% end if %>
</tr>
<tr>
    <% if InStr(Session("opciones"),"CONSL")<> 0 then %>
	    <td align="center"><a href="whInvArticulo.aspx"><img src="Images/btn_consultalote.jpg"></a></td>
	<% end if %>
</tr>
<tr>
    <% if InStr(Session("opciones"),"CONSU")<>0 then %>
	    <td align="center"><a href="whInvUbicacion.aspx"><img src="Images/btn_consultaubic.jpg"></a></td>
	<% end if %>
    <% if InStr(Session("opciones"),"CONSA")<>0 then %>
	    <td align="center"><a href="whInvAlmArticulo.aspx"><img src="Images/btn_consultaarticulo.JPG"></a></td>
	<% end if %>
</tr>
</table>
</form>
</body>
</html>