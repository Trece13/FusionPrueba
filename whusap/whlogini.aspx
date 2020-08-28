<html>
<link href="../basic.css" rel="stylesheet" type="text/css">
<head>

<script type="text/javascript">

function setFocus()
{
document.getElementById("user").focus();
}

</script>
<style type="text/css">
    .errorMsg
    {
        color: Black;
        font-weight: bold;
        font-size: medium;
    }
</style>
</head>
<%
Dim struser, strflg, strmsg  
Session("env") = "448"
Session("envt")="i"
Session("cia")="PPO"
strflg = Request.QueryString("flg")
if strflg = "Y" then
  struser = Session("user")
else
  struser = ""
  Session("Message") = ""
end if
%>
<meta name="viewport" content="width=300, user-scalable=no">
<body onload="setFocus()" bgcolor="#87CEEB">
<form name="frmord" method="post" action="whvalidarusri.aspx">
<table>
<tr>
<td><IMG SRC = "images/logophoenix_s.jpg" ></td>
<td></td><td><H2>User Access</H2></td>
</TABLE>
<table align="left" class="tableDefault4" width="30%" border="0" cellspacing="0" cellpadding="0">
<tr> 
  <td class="titulog2" colspan="2"><b>User</b></td>
  <td><input type="text" id="user" name="user" size="12" value=<%=struser%>></td>
</tr>
<tr> 
  <td class="titulog2" colspan="2"><b>Password</b></td>
  <td><input type="password" name="Pwd" size="13" /></td>
</tr>
<tr> 
<div>
<td class="errorMsg" colspan="3"><%=Session("Message")%></td>
</div>
</tr> 
	<td colspan="4" align="center">
		<input type="submit" name="btnLogin" value="  Access  ">
	</td>
</tr>
</table>
</form>
</body>
</html>